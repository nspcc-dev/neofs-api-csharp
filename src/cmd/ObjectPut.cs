using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Object;
using NeoFS.API.Session;
using NeoFS.API.State;
using NeoFS.Crypto;
using NeoFS.Utils;

namespace cmd
{
    partial class Program
    {

        static async Task ObjectPut(ObjectPutOptions opts)
        {
            const int ChunkSize = (int) (3.5 * Units.MB);

            byte[] cid;
            byte[] oid = Guid.NewGuid().Bytes();
            FileStream file;

            try
            {
                cid = Base58.Decode(opts.CID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong cid format: {0}", err.Message);
                return;
            }

            try
            {
                file = new FileStream(opts.File, FileMode.Open, FileAccess.Read);
            }
            catch (Exception err)
            {
                Console.WriteLine("can't open file: {0}", err.Message);
                return;
            }

            var key = privateKey.FromHex().LoadKey();
            var obj = NeoFS.API.Object.Object.Prepare(cid, oid, (ulong) file.Length, key);

            obj.SetPluginHeaders(opts.Expired, opts.File);

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            Token token = await channel.EstablishSession(oid, SingleForwardedTTL, key, opts.Debug);

            using (var put = new Service.ServiceClient(channel).Put())
            { // Send Object to node
                { // send header:
                    var req = obj.PrepareHeader(SingleForwardedTTL, token, key);

                    await put.RequestStream.WriteAsync(req);
                    //Console.WriteLine(put.ResponseAsync.Result);
                }

                double len = file.Length;

                {// send chunks:
                    byte[] chunk = new byte[ChunkSize];
                    int off = 0;

                    Console.WriteLine();
                    Console.Write("Write chunks: ");
                    using (var progress = new ProgressBar())
                    {
                        for (int num; ; off += num)
                        {
                            num = file.Read(chunk);
                            if (num == 0)
                            {
                                break;
                            }

                            var req = chunk.Take(num).PrepareChunk(SingleForwardedTTL, key);
                            await put.RequestStream.WriteAsync(req);

                            progress.Report((double) off / len);
                        }

                    }
                }

                await Task.Delay(TimeSpan.FromMilliseconds(100));
                Console.Write("Done!");

                put.ResponseAsync.Wait();
                await put.RequestStream.CompleteAsync();

                var res = put.ResponseAsync.Result;

                Console.WriteLine();
                Console.WriteLine("Object stored:");
                Console.WriteLine("URL: {0}", res.Address.ToURL());
                Console.WriteLine("CID: {0}", res.Address.CID.ToCID());
                Console.WriteLine("OID: {0}", res.Address.ObjectID.ToUUID());
            }

            Console.WriteLine();

            Console.WriteLine("Close file.");
            file.Close();

            //Console.WriteLine("Close connection.");
            //channel.ShutdownAsync().Wait();
        }
    }
}

using System;
using System.IO;
using Grpc.Core;
using NeoFS.Crypto;
using NeoFS.Utils;
using NeoFS.API.Service;
using System.Threading.Tasks;

namespace cmd
{
    partial class Program
    {
        const uint SingleForwardedTTL = 2;

        static async Task ObjectGet(GetOptions opts)
        {
            byte[] cid;
            Guid oid;
            FileStream file;

            var key = privateKey.FromHex().LoadKey();

            try
            {
                file = new FileStream(opts.Out, FileMode.Create, FileAccess.Write);
            }
            catch (Exception err)
            {
                Console.WriteLine("can't prepare file: {0}", err.Message);
                return;
            }

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
                oid = Guid.Parse(opts.OID.ToCharArray());
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong oid format: {0}", err.Message);
                return;
            }

            Console.WriteLine("Used host: {0}", opts.Host);

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            { // check that node healthy:
                var hReq = new NeoFS.API.State.HealthRequest();

                hReq.SetTTL(SingleForwardedTTL);
                hReq.SignHeader(key, opts.Debug);

                var cli = new NeoFS.API.State.Status.StatusClient(channel);

                var resp = cli.HealthCheck(hReq);
                Console.WriteLine("HealthResponse = {0}", resp);
            }

            var req = new NeoFS.API.Object.GetRequest
            {
                Address = new NeoFS.API.Refs.Address
                {
                    CID = Google.Protobuf.ByteString.CopyFrom(cid),
                    ObjectID = Google.Protobuf.ByteString.CopyFrom(oid.Bytes()),
                },
            };

            req.SetTTL(SingleForwardedTTL);
            req.SignHeader(key, opts.Debug);

            var client = new NeoFS.API.Object.Service.ServiceClient(channel);

            Console.WriteLine();

            using (var call = client.Get(req))
            {
                ProgressBar progress = null;

                double len = 0;
                double off = 0;

                while (await call.ResponseStream.MoveNext())
                {
                    var res = call.ResponseStream.Current;


                    if (res.Object != null)
                    {
                        len = (double)res.Object.SystemHeader.PayloadLength;

                        Console.WriteLine("Received object");
                        Console.WriteLine("PayloadLength = {0}", len);

                        Console.WriteLine("Headers:");
                        for (var i = 0; i < res.Object.Headers.Count; i++)
                        {
                            Console.WriteLine(res.Object.Headers[i]);
                        }


                        if (res.Object.Payload.Length > 0)
                        {
                            off += (double) res.Object.Payload.Length;
                            res.Object.Payload.WriteTo(file);
                        }

                        Console.Write("Receive chunks: ");
                        progress = new ProgressBar();
                    }
                    else if (res.Chunk != null && res.Chunk.Length > 0)
                    {
                        //Console.Write("#");
                        off += res.Chunk.Length;

                        res.Chunk.WriteTo(file);

                        if (progress != null)
                        {

                            progress.Report(off/ len);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMilliseconds(100));

                if (progress != null)
                {
                    progress.Dispose();
                }

                Console.Write("Done!");

                Console.WriteLine();
            }

            Console.WriteLine("Close file");
            file.Close();

            //Console.WriteLine("Shutdown connection.");
            //channel.ShutdownAsync().Wait();
        }
    }
}

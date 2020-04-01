using System;
using System.IO;
using Grpc.Core;
using NeoFS.Crypto;
using NeoFS.Utils;
using NeoFS.API.Service;
using System.Threading.Tasks;
using NeoFS.API.State;
using Google.Protobuf;
using NeoFS.API.Session;

namespace cmd
{
    partial class Program
    {
        static async Task ObjectDelete(ObjectDeleteOptions opts)
        {
            byte[] cid;
            Guid oid;

            var key = privateKey.FromHex().LoadKey();

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

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            var token = await channel.EstablishSession(oid, SingleForwardedTTL, key, opts.Debug);

            var req = new NeoFS.API.Object.DeleteRequest
            {
                Token = token,
                OwnerID = ByteString.CopyFrom(key.Address()),
                Address = new NeoFS.API.Refs.Address
                {
                    CID = Google.Protobuf.ByteString.CopyFrom(cid),
                    ObjectID = Google.Protobuf.ByteString.CopyFrom(oid.Bytes()),
                },
            };

            req.SetTTL(SingleForwardedTTL);
            req.SignHeader(key, opts.Debug);

            var res = new NeoFS.API.Object.Service.ServiceClient(channel).Delete(req);

            Console.WriteLine();

            Console.WriteLine("Result: {0}", res);
            
            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        static async Task ObjectHead(ObjectHeadOptions opts)
        {
            byte[] cid;
            Guid oid;

            var key = privateKey.FromHex().LoadKey();

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

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            var req = new NeoFS.API.Object.HeadRequest
            {
                FullHeaders = opts.Full,
                Address = new NeoFS.API.Refs.Address
                {
                    CID = Google.Protobuf.ByteString.CopyFrom(cid),
                    ObjectID = Google.Protobuf.ByteString.CopyFrom(oid.Bytes()),
                },
            };

            req.SetTTL(SingleForwardedTTL);
            req.SignHeader(key, opts.Debug);

            var res = new NeoFS.API.Object.Service.ServiceClient(channel).Head(req);

            Console.WriteLine();

            Console.WriteLine("Received object headers");
            Console.WriteLine("\nSystemHeaders\n" +
                "Version: {0}\n" +
                "PayloadLength: {1}\n" +
                "ObjectID: {2}\n" +
                "OwnerID: {3}\n" +
                "CID: {4}\n" +
                "CreatedAt: {5}\n",
                res.Object.SystemHeader.Version,
                res.Object.SystemHeader.PayloadLength,
                res.Object.SystemHeader.ID.ToUUID(),
                res.Object.SystemHeader.OwnerID.ToAddress(),
                res.Object.SystemHeader.CID.ToCID(),
                res.Object.SystemHeader.CreatedAt);

            if (opts.Full)
            {
                Console.WriteLine("Headers:");
                for (var i = 0; i < res.Object.Headers.Count; i++)
                {
                    Console.WriteLine(res.Object.Headers[i]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Meta:\n{0}", res.Meta);
            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        static async Task ObjectGet(ObjectGetOptions opts)
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

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

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
                            off += (double)res.Object.Payload.Length;
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

                            progress.Report(off / len);
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

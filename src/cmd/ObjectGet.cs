using System;
using System.IO;
using Grpc.Core;
using NeoFS.Crypto;
using NeoFS.API.Service;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cmd
{
    partial class Program
    {
        const uint SingleForwardedTTL = 2;

        const string privateKey = "307702010104201dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbba00a06082a8648ce3d030107a144034200041a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a6e3ae669b7a7126ebd9495ac304e44b89b1f3a3a85922c2b9b5aafa8acec98b1";

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
                hReq.SignHeader(key);

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
            req.SignHeader(key);

            var client = new NeoFS.API.Object.Service.ServiceClient(channel);

            Console.WriteLine("call GET");

            using (var call = client.Get(req))
            {

                while (await call.ResponseStream.MoveNext())
                {
                    var res = call.ResponseStream.Current;

                    if (res.Object != null)
                    {
                        Console.WriteLine("Received object");
                        Console.WriteLine("PayloadLength = {0}", res.Object.SystemHeader.PayloadLength);

                        Console.WriteLine("Headers:");
                        for (var i = 0; i < res.Object.Headers.Count; i++)
                        {
                            Console.WriteLine(res.Object.Headers[i]);
                        }


                        if (res.Object.Payload.Length > 0)
                        {
                            res.Object.Payload.WriteTo(file);
                        }
                        continue;
                    }
                    else if (res.Chunk != null && res.Chunk.Length > 0)
                    {
                        res.Chunk.WriteTo(file);
                    }
                }
            }

            Console.WriteLine("Close file");
            file.Close();

            Console.WriteLine("Shutdown connection.");
            channel.ShutdownAsync().Wait();
        }
    }
}

using System;
using System.IO;
using Grpc.Core;
using NeoFS.Crypto;
using NeoFS.API.Service;
using System.Text;

namespace cmd
{
    partial class Program
    {
        const uint SingleForwardedTTL = 2;

        const string privateKey = "307702010104201dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbba00a06082a8648ce3d030107a144034200041a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a6e3ae669b7a7126ebd9495ac304e44b89b1f3a3a85922c2b9b5aafa8acec98b1";

        static async void ObjectGet(GetOptions opts)
        {
            byte[] cid;
            Guid oid;

            var key = privateKey.FromHex().LoadKey();

            //var key = File.ReadAllBytes("/Users/kulikov/Projects/NSPCC/neofs-node/keys/user.key").LoadKey();

            //Console.WriteLine("{0} {1} {2}", opts.CID, opts.OID, opts.Out);

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

            //Console.WriteLine("{0} {1} {2} {3}", cid, oid, oid.GetType(), oid.GetHashCode());


            var channel = new Channel("s01.fs.nspcc.ru:8080", ChannelCredentials.Insecure);

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
                    ObjectID = Google.Protobuf.ByteString.CopyFrom(oid.ToByteArray()),
                },
            };

            req.SetTTL(SingleForwardedTTL);
            req.SignHeader(key);

            var client = new NeoFS.API.Object.Service.ServiceClient(channel);

            using (var call = client.Get(req))
            {
                Console.WriteLine("call GET");

                while (await call.ResponseStream.MoveNext())
                {
                    Console.WriteLine("{0}", call.ResponseStream.Current);
                }
            }


            channel.ShutdownAsync().Wait();
        }
    }
}

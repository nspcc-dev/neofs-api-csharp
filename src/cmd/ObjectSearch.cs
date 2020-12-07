using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using Neo;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Cryptography;

namespace cmd
{
    partial class Program
    {

        static async Task ObjectSearch(ObjectSearchOptions opts)
        {
            ContainerID cid;

            try
            {
                cid = ContainerID.FromBase58String(opts.CID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong cid format: {0}", err.Message);
                return;
            }

            var key = privateKey.HexToBytes().LoadPrivateKey();
            var client = new Client(opts.Host, key);

            var res = await client.SearchObject(cid, null, 2);

            Console.WriteLine("\nSearch results:");
            foreach (var item in res)
            {
                Console.WriteLine($"CID={cid.ToBase58String()} OID={item.ToBase58String()}");
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}
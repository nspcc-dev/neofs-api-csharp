using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Cryptography;

namespace cmd
{
    partial class Program
    {

        static async Task ObjectPut(ObjectPutOptions opts)
        {
            ContainerID cid;
            FileStream file;

            try
            {
                cid = ContainerID.FromBase58String(opts.CID);
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

            byte[] payload = new byte[] { };
            int count;
            do
            {
                var buffer = new byte[NeoFS.API.v2.Object.Object.ChunkSize];
                count = file.Read(buffer, 0, NeoFS.API.v2.Object.Object.ChunkSize);
                payload = payload.Concat(buffer).ToArray();
            } while (0 < count);
            var key = privateKey.FromHex().LoadPrivateKey();
            var client = new Client(opts.Host, key);

            //session token

            var obj = new NeoFS.API.v2.Object.Object
            {
                Header = new Header
                {
                    Version = new NeoFS.API.v2.Refs.Version
                    {
                        Major = 2,
                        Minor = 0,
                    },
                    ContainerId = cid,
                    OwnerId = key.ToOwnerID(),
                    CreationEpoch = 0,
                    PayloadLength = (ulong)payload.Length,
                    ObjectType = ObjectType.Regular

                },
                Payload = ByteString.CopyFrom(payload),
            };
            obj.SetVerificationFields(key);
            var oid = await client.PutObject(obj);
            Console.WriteLine();

            Console.WriteLine("Close file.");
            file.Close();
        }
    }
}

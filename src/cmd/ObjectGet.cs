using System;
using System.IO;
using Grpc.Core;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;
using System.Threading.Tasks;
using Google.Protobuf;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Object;

namespace cmd
{
    partial class Program
    {
        static async Task ObjectDelete(ObjectDeleteOptions opts)
        {
            ContainerID cid;
            ObjectID oid;

            var key = privateKey.FromHex().LoadPrivateKey();

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
                oid = ObjectID.FromBase58String(opts.OID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong oid format: {0}", err.Message);
                return;
            }
            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);
            var address = new Address
            {
                ObjectId = oid,
                ContainerId = cid,
            };
            var res = client.DeleteObject(address);

            Console.WriteLine();

            Console.WriteLine("Result: {0}", res);

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        static async Task ObjectHead(ObjectHeadOptions opts)
        {
            ContainerID cid;
            ObjectID oid;

            var key = privateKey.FromHex().LoadPrivateKey();

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
                oid = ObjectID.FromBase58String(opts.OID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong oid format: {0}", err.Message);
                return;
            }
            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);

            var address = new Address
            {
                ContainerId = cid,
                ObjectId = oid,
            };

            var obj = client.GetObjectHeader(address, false);

            Console.WriteLine();

            Console.WriteLine("Received object headers");
            Console.WriteLine("\nSystemHeaders\n" +
                "Version: {0}\n" +
                "PayloadLength: {1}\n" +
                "ObjectID: {2}\n" +
                "OwnerID: {3}\n" +
                "CID: {4}\n" +
                "CreatedAt: {5}\n",
                obj.Header.Version,
                obj.Header.PayloadLength,
                obj.Header.ContainerId.ToBase58String(),
                obj.Header.CreationEpoch,
                obj.Header.PayloadHash,
                obj.Header.ObjectType);
            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        static async Task ObjectGet(ObjectGetOptions opts)
        {
            ContainerID cid;
            ObjectID oid;
            FileStream file;

            var key = privateKey.FromHex().LoadPrivateKey();

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
                cid = ContainerID.FromBase58String(opts.CID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong cid format: {0}", err.Message);
                return;
            }
            try
            {
                oid = ObjectID.FromBase58String(opts.OID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong oid format: {0}", err.Message);
                return;
            }
            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);

            var address = new Address
            {
                ContainerId = cid,
                ObjectId = oid,
            };

            var obj = await client.GetObject(address);

            await file.WriteAsync(obj.Payload.ToByteArray(), 0, (int)obj.Header.PayloadLength);

            Console.WriteLine("Close file");
            file.Close();
        }
    }
}

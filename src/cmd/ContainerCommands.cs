using System;
using System.Threading.Tasks;
using Grpc.Core;
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Container;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Refs;
using Google.Protobuf;

namespace cmd
{
    partial class Program
    {
        static async Task ContainerPut(ContainerPutOptions opts)
        {
            var key = privateKey.FromHex().LoadPrivateKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);
            uint basicACL = 0;

            switch (opts.BasicACL)
            {
                case "public":
                    basicACL = (uint)BasicAcl.PublicBasicRule;
                    break;
                case "private":
                    basicACL = (uint)BasicAcl.PublicBasicRule;
                    break;
                case "readonly":
                    basicACL = (uint)BasicAcl.ReadOnlyBasicRule;
                    break;
                default:
                    basicACL = Convert.ToUInt32(opts.BasicACL, 16);
                    break;
            }

            var container = new Container
            {
                Version = new NeoFS.API.v2.Refs.Version
                {
                    Major = 2,
                    Minor = 1
                },
                OwnerId = key.ToOwnerID(),
                Nonce = ByteString.CopyFrom(Guid.NewGuid().Bytes()),
                BasicAcl = basicACL,
            };
            var sig = container.SignMessagePart(key);

            var cid = client.PutContainer(container, sig);

            Console.WriteLine();
            Console.WriteLine("Wait for container: {0}", cid);
            Console.WriteLine();


            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));

                try
                {
                    var get = client.GetContainer(cid);

                    Console.WriteLine("\n\nDone: \n");

                    get.Say();

                    return;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Not ready: {0}", err.Message);
                }
            }

            Console.WriteLine("\nCould not wait for container creation...");
        }

        static async Task ContainerList(ContainerListOptions opts)
        {
            var key = privateKey.FromHex().LoadPrivateKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);
            var cids = client.ListContainers(key.ToOwnerID());

            Console.WriteLine("\nUser [{0}] containers: \n", key.ToAddress());

            foreach (var item in cids)
            {
                Console.WriteLine("CID = {0}", item);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        static async Task ContainerGet(ContainerGetOptions opts)
        {
            var key = privateKey.FromHex().LoadPrivateKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);

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

            var container = client.GetContainer(cid);

            Console.WriteLine();
            Console.WriteLine("Container options:");
            Console.WriteLine("CID = {0}", opts.CID);
            Console.WriteLine("Nonce = {0}", container.Nonce.ToHex());
            Console.WriteLine("OwnerID = {0}", container.OwnerId.OwnerIDToAddress());
            Console.WriteLine("PlacementPolicy = {0}", container.PlacementPolicy.ToString());
            Console.WriteLine("ACL = {0}", container.BasicAcl.ToString("X"));
            Console.WriteLine("Attributes = {0}", container.Attributes.ToString());

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

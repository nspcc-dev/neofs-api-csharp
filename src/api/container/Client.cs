using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.Crypto;

namespace NeoFS.API.v2.Container
{
    public class Client : ContainerService.ContainerServiceClient
    {
        private Channel channel;
        Client(Channel chan) : base(chan)
        {
            this.channel = chan;
        }

        public GetResponse GetContainer(ByteString cid, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    ContainerId = new Refs.ContainerID
                    {
                        Value = cid,
                    }
                }
            };

            // req.SetTTL(ttl);
            // req.SignHeader(key, debug);

            return Get(req);
        }

        public GetResponse GetContainer(byte[] cid, uint ttl, ECDsa key, bool debug = false)
        {
            return GetContainer(
                ByteString.CopyFrom(cid),
                ttl, key, debug);
        }

        public PutResponse PutContainer(int size, uint basicACL, uint ttl, ECDsa key, bool debug = false)
        {

            var req = new PutRequest();

            return Put(req);
        }

        public ListResponse ListContainers(uint ttl, ECDsa key, bool debug = false)
        {
            var req = new ListRequest
            {
                Body = new ListRequest.Types.Body
                {
                    OwnerId = new Refs.OwnerID
                    {
                        Value = ByteString.CopyFrom(key.Address())
                    }
                }
            };

            return List(req);
        }
    }
}

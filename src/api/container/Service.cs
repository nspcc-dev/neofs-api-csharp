using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Service;
using NeoFS.Crypto;

namespace NeoFS.API.Container
{
    public sealed partial class ListRequest : IMeta, IVerify { }

    public static class ContainerExtension
    {
        public static ListResponse ListContainers(this Channel chan, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new ListRequest
            {
                OwnerID = ByteString.CopyFrom(key.Address()),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return new Service.ServiceClient(chan).List(req);
        }
    }
}

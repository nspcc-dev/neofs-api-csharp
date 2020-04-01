using System;
using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Service;
using NeoFS.Utils;
using NeoFS.Crypto;

namespace NeoFS.API.Container
{
    public sealed partial class GetRequest : IMeta, IVerify { }
    public sealed partial class PutRequest : IMeta, IVerify { }
    public sealed partial class ListRequest : IMeta, IVerify { }

    public static class ContainerExtension
    {
        public static GetResponse GetContainer(this Channel chan, ByteString cid, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new GetRequest
            {
                CID = cid,
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return new Service.ServiceClient(chan).Get(req);
        }

        public static GetResponse GetContainer(this Channel chan, byte[] cid, uint ttl, ECDsa key, bool debug = false)
        {
            return chan.GetContainer(
                ByteString.CopyFrom(cid),
                ttl, key, debug);
        }

        public static PutResponse PutContainer(this Channel chan, int size, uint ttl, ECDsa key, bool debug = false)
        {
            Netmap.PlacementRule rules;

            { // hardcoded placement rule:
                rules = new Netmap.PlacementRule { ReplFactor = 2 };

                var group = new Netmap.SFGroup();
                var selectors = new Google.Protobuf.Collections.RepeatedField<Netmap.Select>();

                selectors.Add(
                    new Netmap.Select
                    {
                        Count = 3,
                        Key = "Node",
                    });

                group.Selectors.Add(selectors);
                rules.SFGroups.Add(group);
            }

            var req = new PutRequest
            {
                Rules = rules,
                Group = new AccessGroup(),
                Capacity = Units.GB * (ulong) size,
                OwnerID = ByteString.CopyFrom(key.Address()),
                MessageID = ByteString.CopyFrom(new Guid().Bytes()),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return new Service.ServiceClient(chan).Put(req);
        }

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

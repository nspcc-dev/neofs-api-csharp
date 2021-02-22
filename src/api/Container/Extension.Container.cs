using Google.Protobuf;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Cryptography;
using System;

namespace NeoFS.API.v2.Container
{
    public partial class Container
    {
        private ContainerID _id;
        public ContainerID CalCulateAndGetID
        {
            get
            {
                if (_id is null)
                    _id = new ContainerID
                    {
                        Value = this.Sha256()
                    };
                return _id;
            }
        }

        public Guid NonceUUID
        {
            get
            {
                return new Guid(nonce_.ToByteArray());
            }
            set
            {
                nonce_ = ByteString.CopyFrom(value.ToByteArray());
            }
        }

        public static partial class Types
        {
            public sealed partial class Attribute
            {
                // SysAttributePrefix is a prefix of key to system attribute.
                public const string SysAttributePrefix = "__NEOFS__";

                // SysAttributeSubnet is a string ID of container's storage subnet.
                public const string SysAttributeSubnet = SysAttributePrefix + "SUBNET";
            }
        }
    }
}
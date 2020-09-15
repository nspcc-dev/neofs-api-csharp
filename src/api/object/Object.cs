using Google.Protobuf;
using System;
using System.Security.Cryptography;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.Object
{
    public partial class Object
    {
        public const string KeyRootObject = "ROOT_OBJECT";
        public const string KeyStorageGroup = "STORAGE_GROUP";

        public void SetPayload(byte[] payload)
        {
            Payload = ByteString.CopyFrom(payload);
            Header.PayloadLength = (ulong)payload.Length;
        }

        public void SetPluginHeaders(ulong expired)
        {
            expired = expired * 60 + (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            Header.Attributes.Add(
                new Header.Types.Attribute
                {
                    Key = "plugin",
                    Value = "sendneofs",
                });

            Header.Attributes.Add(
                new Header.Types.Attribute
                {
                    Key = "expired",
                    Value = Convert.ToString(expired),
                });
        }

        public static Object Prepare(byte[] cid, Guid oid, ulong size, ECDsa key)
        {
            return Prepare(cid, oid.Bytes(), size, key);
        }

        public static Object Prepare(byte[] cid, byte[] oid, ulong size, ECDsa key)
        {
            byte[] owner = key.Address();

            return new Object
            {
                Header = new Header
                {
                    // ID = ByteString.CopyFrom(oid),
                    ContainerId = new Refs.ContainerID
                    {
                        Value = ByteString.CopyFrom(cid),
                    },
                    OwnerId = new Refs.OwnerID
                    {
                        Value = ByteString.CopyFrom(owner),
                    },
                    PayloadLength = size,
                },
            };
        }
    }
}
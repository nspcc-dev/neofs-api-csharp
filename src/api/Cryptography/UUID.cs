using System;
using Google.Protobuf;
using Neo;
using Neo.Cryptography;

namespace NeoFS.API.v2.Cryptography
{
    public static class UUIDExtension
    {
        public static string ToCID(this ByteString id)
        {
            return Base58.Encode(id.ToByteArray());
        }

        public static Guid ToUUID(this ByteString id)
        {
            return Guid.Parse(id.ToByteArray().ToHexString());
        }

        public static ByteString ToByteString(this Guid id)
        {
            return ByteString.CopyFrom(id.Bytes());
        }

        public static byte[] Bytes(this Guid id)
        {
            if (id == null)
                return null;

            return id.ToString().Replace("-", String.Empty).HexToBytes();
        }
    }
}

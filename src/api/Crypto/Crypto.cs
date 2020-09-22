using Neo.Cryptography;
using Google.Protobuf;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Crypto
{
    public static class Crypto
    {
        public static ByteString Sha256(this IMessage data)
        {
            return ByteString.CopyFrom(Neo.Cryptography.Crypto.Hash256(data.ToByteArray()));
        }

        public static ByteString Sha256(this ByteString data)
        {
            return ByteString.CopyFrom(Neo.Cryptography.Crypto.Hash256(data.ToByteArray()));
        }

        public static Checksum Sha256Checksum(this IMessage data)
        {
            return new Checksum
            {
                Type = ChecksumType.Sha256,
                Sum = data.Sha256()
            };
        }

        public static Checksum Sha256Checksum(this ByteString data)
        {
            return new Checksum
            {
                Type = ChecksumType.Sha256,
                Sum = data.Sha256()
            };
        }
    }
}
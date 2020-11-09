using Google.Protobuf;
using NeoFS.API.v2.Refs;
using System.Buffers.Binary;

namespace NeoFS.API.v2.Cryptography
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

        public static ulong Murmur64(this byte[] value, uint seed)
        {
            using (Murmur3_128 murmur = new Murmur3_128(seed))
            {
                return BinaryPrimitives.ReadUInt64LittleEndian(murmur.ComputeHash(value));
            }
        }
    }
}
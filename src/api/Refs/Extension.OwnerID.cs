using Google.Protobuf;
using Neo.Cryptography;

namespace NeoFS.API.v2.Refs
{
    public partial class OwnerID
    {
        public const int ValueSize = 25;

        public static OwnerID FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 25) throw new System.InvalidOperationException("OwnerID must be a hash256");
            return new OwnerID
            {
                Value = ByteString.CopyFrom(bytes)
            };
        }

        public static OwnerID FromBase58String(string id)
        {
            return FromByteArray(Base58.Decode(id));
        }

        public string ToBase58String()
        {
            return Base58.Encode(Value.ToByteArray());
        }
    }
}

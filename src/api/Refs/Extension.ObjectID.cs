using Google.Protobuf;
using NeoFS.API.v2.Cryptography;

namespace NeoFS.API.v2.Refs
{
    public partial class ObjectID
    {
        //Hash256 to ObjectID
        public static ObjectID FromByteArray(byte[] hash)
        {
            if (hash.Length != 32) throw new System.InvalidOperationException("ObjectID must be a hash256");
            return new ObjectID
            {
                Value = ByteString.CopyFrom(hash)
            };
        }

        public static ObjectID FromBase58String(string id)
        {
            return FromByteArray(Base58.Decode(id));
        }

        public string ToBase58String()
        {
            return Base58.Encode(Value.ToByteArray());
        }
    }
}

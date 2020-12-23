using Google.Protobuf;
using NeoFS.API.v2.Cryptography;
using System;

namespace NeoFS.API.v2.Refs
{
    public sealed partial class Checksum
    {
        public bool Verify(ByteString data)
        {
            switch (type_)
            {
                case ChecksumType.Sha256:
                    {
                        return sum_ == data.Sha256();
                    }
                case ChecksumType.Tz:
                    {
                        //TODO
                        throw new NotImplementedException();
                    }
                default:
                    throw new InvalidOperationException(nameof(Verify) + " unsupported checksum type " + type_);
            }
        }
    }
}
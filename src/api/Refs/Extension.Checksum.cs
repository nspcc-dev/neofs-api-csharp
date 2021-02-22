using Google.Protobuf;
using NeoFS.API.v2.Cryptography;
using Neo;
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

        public string String()
        {
            return sum_.ToByteArray().ToHexString();
        }

        public void Parse(string str)
        {
            sum_ = ByteString.CopyFrom(str.HexToBytes());
            switch (sum_.Length)
            {
                case 32:
                    type_ = ChecksumType.Sha256;
                    break;
                case 64:
                    type_ = ChecksumType.Tz;
                    break;
                default:
                    throw new FormatException($"unsupported checksum length {sum_.Length}");
            }
        }
    }
}
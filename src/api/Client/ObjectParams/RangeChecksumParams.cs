using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using System.Collections.Generic;

namespace NeoFS.API.v2.Client.ObjectParams
{
    public class RangeChecksumParams
    {
        public Address Address;
        public bool Raw;
        public List<Range> Ranges;
        public ChecksumType Type;
        public byte[] Salt;
    }
}

using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Client.ObjectParams
{
    public class RangeDataParams
    {
        public Address Address;
        public bool Raw;
        public Range Range;
    }
}

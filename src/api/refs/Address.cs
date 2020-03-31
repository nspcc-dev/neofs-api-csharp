using System;
using NeoFS.Crypto;

namespace NeoFS.API.Refs
{
    public sealed partial class Address
    {
        public string ToURL()
        {
            return string.Format(
                "https://cdn.fs.neo.org/{0}/{1}",
                CID.ToCID(),
                ObjectID.ToUUID());
        }
    }
}

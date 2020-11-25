using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Session
{
    public partial class RequestMetaHeader
    {
        public static RequestMetaHeader Default
        {
            get
            {
                var meta = new RequestMetaHeader()
                {
                    Version = new Version
                    {
                        Major = 2,
                        Minor = 0,
                    },
                    Epoch = 0,
                    Ttl = 1,
                };
                return meta;
            }
        }
    }
}
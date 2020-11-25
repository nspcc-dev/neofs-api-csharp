using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Session
{
    public partial class ResponseMetaHeader
    {
        public static ResponseMetaHeader Default
        {
            get
            {
                var meta = new ResponseMetaHeader()
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
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Client
{
    public class CallOption
    {
        private Version version;
        private uint ttl;
        private ulong epoch;
        private XHeader[] xheaders;
        private SessionToken seesion;
        private BearerToken bearer;
    }
}
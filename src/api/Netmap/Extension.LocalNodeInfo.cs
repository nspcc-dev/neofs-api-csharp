using Google.Protobuf;
using NeoFS.API.v2.Session;

namespace NeoFS.API.v2.Netmap
{
    public partial class LocalNodeInfoRequest : IRequest
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class LocalNodeInfoResponse : IResponse
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }
}

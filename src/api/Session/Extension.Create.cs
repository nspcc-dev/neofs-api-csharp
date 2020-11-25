using Google.Protobuf;

namespace NeoFS.API.v2.Session
{
    public partial class CreateResponse : IResponse
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class CreateRequest : IRequest
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }
}

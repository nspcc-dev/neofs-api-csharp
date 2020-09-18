using Google.Protobuf;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.Accounting
{
    public partial class BalanceRequest : IRequest
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class BalanceResponse : IResponse
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }
}

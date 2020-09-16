using Google.Protobuf;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.Accounting
{
    public partial class BalanceRequest : IRequestSignable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class BalanceResponse : IResponseVerifiable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }
}

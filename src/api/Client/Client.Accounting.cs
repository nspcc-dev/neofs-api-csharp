using NeoFS.API.v2.Accounting;
using NeoFS.API.v2.Crypto;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public Decimal GetBalance(OwnerID owner)
        {
            var account_client = new AccountingService.AccountingServiceClient(channel);
            var req = new BalanceRequest
            {
                Body = new BalanceRequest.Types.Body
                {
                    OwnerId = owner,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = account_client.Balance(req);
            if (!resp.VerifyResponse())
                throw new System.FormatException("invalid balance response");
            return resp.Body.Balance;
        }

        public Decimal GetSelfBalance()
        {
            var w = key.ToOwnerID();
            return GetBalance(w);
        }
    }
}

using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.v2.Accounting;

namespace NeoFS.API.v2.Accounting
{
    public class Client : AccountingService.AccountingServiceClient
    {
        private Channel channel;
        Client(Channel chan) : base(chan)
        {
            channel = chan;
        }

        public BalanceResponse GetBalance(uint ttl, ECDsa key, bool debug = false)
        {
            var req = new BalanceRequest
            {
                Body = new BalanceRequest.Types.Body
                {
                    OwnerId = new Refs.OwnerID
                    {
                        Value = ByteString.CopyFrom(key.Address()),
                    },
                },
            };

            // req.SetTTL(ttl);
            // req.SignHeader(key, debug);

            return Balance(req);
        }
    }
}

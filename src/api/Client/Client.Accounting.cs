using Google.Protobuf;
using Neo;
using NeoFS.API.v2.Accounting;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public Accounting.Decimal GetBalance(OwnerID owner, CallOptions options = null)
        {
            var account_client = new AccountingService.AccountingServiceClient(channel);
            var req = new BalanceRequest
            {
                Body = new BalanceRequest.Types.Body
                {
                    OwnerId = owner,
                }
            };
            req.MetaHeader = options?.GetRequestMetaHeader() ?? DefaultCallOptions.GetRequestMetaHeader();
            req.SignRequest(key);
            Console.WriteLine($"balance request, meta={req.MetaHeader.ToByteArray().ToHexString()}, sig={req.VerifyHeader.MetaSignature.ToByteArray().ToHexString()}");
            var resp = account_client.Balance(req);
            if (!resp.VerifyResponse())
                throw new System.FormatException("invalid balance response");
            return resp.Body.Balance;
        }

        public Accounting.Decimal GetSelfBalance()
        {
            var w = key.ToOwnerID();
            return GetBalance(w);
        }
    }
}

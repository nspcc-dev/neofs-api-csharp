using System;
using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Service;
using NeoFS.Crypto;

namespace NeoFS.API.Accounting
{
    public sealed partial class BalanceRequest : IMeta, IVerify { }

    public static class AccountingExtension
    {
        public static BalanceResponse GetBalance(this Channel chan, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new BalanceRequest
            {
                OwnerID = ByteString.CopyFrom(key.Address()),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return new Accounting.AccountingClient(chan).Balance(req);
        }
    }
}

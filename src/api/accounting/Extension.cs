using System.IO;
using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
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
}

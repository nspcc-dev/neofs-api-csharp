using Grpc.Core;
using System.Security.Cryptography;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        private ECDsa key;
        private Channel channel;
        public Client(Channel chan, ECDsa k)
        {
            channel = chan;
            key = k;
        }
    }
}

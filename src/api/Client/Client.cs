using Grpc.Core;
using System.Security.Cryptography;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        private readonly ECDsa key;
        private readonly Channel channel;
        public Client(string host, ECDsa k)
        {
            channel = new Channel(host, ChannelCredentials.Insecure);
            key = k;
        }
    }
}

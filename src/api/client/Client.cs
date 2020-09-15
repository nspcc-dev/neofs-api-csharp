using System.Security.Cryptography;
using Grpc.Core;

namespace NeoFS.API
{
    public partial class Client
    {
        private ECDsa key;
        private Channel channel;
        Client(Channel chan, ECDsa k)
        {
            channel = chan;
            key = k;
        }
    }
}
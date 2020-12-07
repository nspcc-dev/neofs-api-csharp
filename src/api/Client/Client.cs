using Grpc.Core;
using System.Security.Cryptography;
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        private readonly ECDsa key;
        private readonly Channel channel;
        private SessionToken session;
        private BearerToken bearer;

        public Client(string host, ECDsa k)
        {
            channel = new Channel(host, ChannelCredentials.Insecure);
            key = k;
        }

        public CallOptions DefaultCallOptions
        {
            get
            {
                return new CallOptions
                {
                    Version = Version.SDKVersion(),
                    Ttl = 2,
                    Session = session,
                    Bearer = bearer,
                };
            }
        }
    }
}

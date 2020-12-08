using Grpc.Core;
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System;
using System.Security.Cryptography;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        const uint SearchObjectVersion = 1;
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
                    Version = Refs.Version.SDKVersion(),
                    Ttl = 2,
                    Session = session,
                    Bearer = bearer,
                };
            }
        }

        public CallOptions ApplyCustomOptions(CallOptions custom)
        {
            var options = DefaultCallOptions;
            if (custom is null) return options;
            if (custom.Version != null) options.Version = custom.Version;
            options.Ttl = custom.Ttl;
            options.Epoch = custom.Epoch;
            if (custom.XHeaders != null) options.XHeaders = custom.XHeaders;
            if (custom.Session != null) options.Session = custom.Session;
            if (custom.Bearer != null) options.Bearer = custom.Bearer;
            return options;
        }
    }
}

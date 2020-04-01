using System;
using System.Security.Cryptography;
using Grpc.Core;
using NeoFS.API.Service;

namespace NeoFS.API.State
{
    public sealed partial class DumpRequest : IMeta, IVerify { }
    public sealed partial class DumpVarsRequest : IMeta, IVerify { }
    public sealed partial class NetmapRequest : IMeta, IVerify { }
    public sealed partial class HealthRequest: IMeta, IVerify { }

    public static class StateExtension
    {
        public static Channel UsedHost(this Channel chan)
        {
            Console.WriteLine("\nUsed host: {0}", chan.Target);
            return chan;
        }

        public static HealthResponse GetHealth(this Channel chan, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new HealthRequest();

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            var cli = new Status.StatusClient(chan);

            return cli.HealthCheck(req);
        }
    }
}

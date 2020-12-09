using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Netmap;
using System;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public NodeInfo LocalNodeInfo(CallOptions options = null)
        {
            var netmap_client = new NetmapService.NetmapServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new LocalNodeInfoRequest
            {
                Body = new LocalNodeInfoRequest.Types.Body { }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);
            var resp = netmap_client.LocalNodeInfo(req);
            if (!resp.VerifyResponse())
                throw new FormatException(nameof(LocalNodeInfo) + " invalid LocalNodeInfo response");
            return resp.Body.NodeInfo;
        }

        public ulong Epoch(CallOptions options = null)
        {
            var netmap_client = new NetmapService.NetmapServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new LocalNodeInfoRequest
            {
                Body = new LocalNodeInfoRequest.Types.Body { }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);
            var resp = netmap_client.LocalNodeInfo(req);
            if (!resp.VerifyResponse())
                throw new FormatException(nameof(LocalNodeInfo) + " invalid LocalNodeInfo response");
            return resp.MetaHeader.Epoch;
        }
    }
}

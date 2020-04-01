using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Object;
using NeoFS.API.Service;
using NeoFS.API.Session;
using NeoFS.API.State;
using NeoFS.Crypto;
using NeoFS.Utils;
using NeoFS.API.Query;

namespace cmd
{
    partial class Program
    {

        static async Task ObjectSearch(ObjectSearchOptions opts)
        {
            byte[] cid;

            try
            {
                cid = Base58.Decode(opts.CID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong cid format: {0}", err.Message);
                return;
            }

            var key = privateKey.FromHex().LoadKey();
            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            MemoryStream buf = new MemoryStream();
            Query.Parse(opts.Query, opts.SG, opts.Root, opts.Debug).WriteTo(buf);

            var req = new SearchRequest
            {
                ContainerID = ByteString.CopyFrom(cid),
                Query = ByteString.CopyFrom(buf.ToArray()),
            };

            req.SetTTL(SingleForwardedTTL);
            req.SignHeader(key, opts.Debug);

            var res = new Service.ServiceClient(channel).Search(req);

            Console.WriteLine("\nSearch results:");
            while (await res.ResponseStream.MoveNext())
            {
                var items = res.ResponseStream.Current.Addresses;
                foreach (var item in items)
                {
                    Console.WriteLine("CID = {0}, OID = {1}",
                        item.CID.ToCID(),
                        item.ObjectID.ToUUID());
                }
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}
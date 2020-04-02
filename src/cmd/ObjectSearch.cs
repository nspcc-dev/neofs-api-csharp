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

            var res = channel.ObjectSearch(SingleForwardedTTL, key,
                query: opts.Query,
                cid: cid,
                sg: opts.SG,
                root: opts.Root,
                debug: opts.Debug);

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
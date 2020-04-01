using System;
using System.Threading.Tasks;
using Grpc.Core;
using NeoFS.API.Container;
using NeoFS.API.State;
using NeoFS.Crypto;
using NeoFS.Utils;

namespace cmd
{
    partial class Program
    {

        static async Task ContainerList(ContainerListOptions opts)
        {
            var key = privateKey.FromHex().LoadKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            var res = channel.ListContainers(SingleForwardedTTL, key, opts.Debug);

            Console.WriteLine("\nUser [{0}] containers: \n", key.ToAddress());

            foreach (var item in res.CID)
            {
                Console.WriteLine("CID = {0}", item.ToCID());
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

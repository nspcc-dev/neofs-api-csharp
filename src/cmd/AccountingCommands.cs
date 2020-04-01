using System;
using System.Threading.Tasks;
using Grpc.Core;
using NeoFS.API.Accounting;
using NeoFS.API.State;
using NeoFS.Crypto;
using NeoFS.Utils;

namespace cmd
{
    public partial class Program
    {
        static async Task AccountingBalance(AccountingBalanceOptions opts)
        {
            var key = privateKey.FromHex().LoadKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            Console.WriteLine();

            channel.GetBalance(SingleForwardedTTL, key, opts.Debug).Say();

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

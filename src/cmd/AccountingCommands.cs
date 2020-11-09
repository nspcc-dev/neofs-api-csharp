using System;
using System.Threading.Tasks;
using Grpc.Core;
using NeoFS.API.v2.Accounting;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;

namespace cmd
{
    public partial class Program
    {
        static async Task AccountingBalance(AccountingBalanceOptions opts)
        {
            var key = privateKey.FromHex().LoadPrivateKey();

            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);
            var client = new Client(channel, key);

            Console.WriteLine();

            client.GetBalance(key.ToOwnerID());

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

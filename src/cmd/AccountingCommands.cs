using System;
using System.Threading.Tasks;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;

namespace cmd
{
    public partial class Program
    {
        static async Task AccountingBalance(AccountingBalanceOptions opts)
        {
            var key = privateKey.FromHex().LoadPrivateKey();

            var client = new Client(opts.Host, key);

            Console.WriteLine();

            client.GetBalance(key.ToOwnerID());

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

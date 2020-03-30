using System;
using System.Threading.Tasks;

namespace cmd
{
    partial class Program
    {
        static async Task ObjectPut(PutOptions opts)
        {
            Console.WriteLine("{0} {1}", opts.CID, opts.File);

            await Task.Delay(TimeSpan.FromMilliseconds(100));

            return;
        }
    }
}

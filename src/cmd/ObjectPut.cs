using System;

namespace cmd
{
    partial class Program
    {
        static void ObjectPut(PutOptions opts)
        {
            Console.WriteLine("{0} {1}", opts.CID, opts.File);
        }
    }
}

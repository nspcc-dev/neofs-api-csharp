using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace cli
{
    partial class Program
    {
        static void ObjectPut(PutOptions opts)
        {
            Console.WriteLine("{0} {1}", opts.CID, opts.File);
        }
    }
}

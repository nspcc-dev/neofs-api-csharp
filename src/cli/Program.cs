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
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<PutOptions, GetOptions>(args)
              .WithParsed<PutOptions>(ObjectPut)
              .WithParsed<GetOptions>(ObjectGet)
              .WithNotParsed(HandleParseError);
            Console.ReadLine();
        }

        static void HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Parse error (do smth with that).");
        }
    }
}

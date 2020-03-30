using CommandLine;
using System;
using System.Collections.Generic;

namespace cmd
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

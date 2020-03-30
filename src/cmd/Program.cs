using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cmd
{
    partial class Program
    {
        public static async Task Main(string[] args)
        {
            await Parser.Default
                .ParseArguments<PutOptions, GetOptions>(args)
                .MapResult(
                    (PutOptions opts) => ObjectPut(opts),
                    (GetOptions opts) => ObjectGet(opts),
                    errs => HandleParseError(errs)
                );
                //.WithParsed<PutOptions>(ObjectPut)
                //.WithParsed<GetOptions>(ObjectGet)
                //.WithNotParsed(HandleParseError);
        }

        static async Task HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Parse error (do smth with that).");

            await Task.Delay(TimeSpan.FromMilliseconds(100));

            return;
        }
    }
}

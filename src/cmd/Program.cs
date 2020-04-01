using CommandLine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cmd
{
    partial class Program
    {
        const uint SingleForwardedTTL = 2;

        const string privateKey =
            "30770201010420347bc2bd9eb7b9f41a217a26dc5a3d2a3c25ece1c8bff1d5a146a" +
            "af4156e3436a00a06082a8648ce3d030107a14403420004b3622bf4017bdfe317c5" +
            "8aed5f4c753f206b7db896046fa7d774bbc4bf7f8dc2af9c7b29759df7f3d92052a" +
            "5b9bc545bcd31c6a7a3463e90c768a6c3e45b1036";

        //const string privateKey =
        //    "307702010104201dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930" +
        //    "fa1c5d90bbba00a06082a8648ce3d030107a144034200041a6c6fbbdf02ca351745" +
        //    "fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a6e3ae669b7a7126ebd9495a" +
        //    "c304e44b89b1f3a3a85922c2b9b5aafa8acec98b1";

        public static async Task Main(string[] args)
        {
            await Parser
                .Default
                .ParseArguments<
                    ObjectPutOptions,
                    ObjectGetOptions,
                    ObjectSearchOptions,
                    AccountingBalanceOptions,
                    ContainerGetOptions,
                    ContainerPutOptions,
                    ObjectHeadOptions,
                    ContainerListOptions >(args)
                .MapResult(
                    (ObjectPutOptions opts) => ObjectPut(opts),
                    (ObjectGetOptions opts) => ObjectGet(opts),
                    (ObjectHeadOptions opts) => ObjectHead(opts),
                    (ObjectSearchOptions opts) => ObjectSearch(opts),

                    (AccountingBalanceOptions opts) => AccountingBalance(opts),

                    (ContainerGetOptions opts) => ContainerGet(opts),
                    (ContainerPutOptions opts) => ContainerPut(opts),
                    (ContainerListOptions opts) => ContainerList(opts),

                    errs => Errors(errs)
                );
        }

        static async Task Errors(IEnumerable<Error> errs)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Grpc.Core;
using NeoFS.API.Object;
using NeoFS.API.State;
using NeoFS.Crypto;
using NeoFS.Utils;

namespace cmd
{

    #region StorageGroup:Options
    public static class SGOptions
    {
        // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
        // So they suggest to use hyphenation on such subcommands.
        [Verb("sg:get", HelpText = "get storage group from container")]
        public class Get
        {
            [Option("host",
                Default = "s01.fs.nspcc.ru:8080",
                Required = false,
                HelpText = "Host that would be used to fetch object from it")]
            public string Host { get; set; }

            [Option("cid",
                Required = true,
                HelpText = "Container ID, used to fetch storage group from it")]
            public string CID { get; set; }

            [Option("sgid",
                Required = true,
                HelpText = "StorageGroup ID")]
            public string SGID { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }

        // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
        // So they suggest to use hyphenation on such subcommands.
        [Verb("sg:get", HelpText = "put storage group into container")]
        public class Put
        {
            [Option("host",
                Default = "s01.fs.nspcc.ru:8080",
                Required = false,
                HelpText = "Host that would be used to fetch object from it")]
            public string Host { get; set; }

            [Option("cid",
                Required = true,
                HelpText = "Container ID, used to fetch storage group from it")]
            public string CID { get; set; }

            [Option("oids",
                Required = true,
                Separator = ',',
                HelpText = "ObjectID's to be used for creating SG")]
            public IEnumerable<string> OIDs { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }

        // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
        // So they suggest to use hyphenation on such subcommands.
        [Verb("sg:list", HelpText = "list storage groups in container")]
        public class List
        {
            [Option("host",
                Default = "s01.fs.nspcc.ru:8080",
                Required = false,
                HelpText = "Host that would be used to fetch object from it")]
            public string Host { get; set; }

            [Option("cid",
                Required = true,
                HelpText = "Container ID, used to fetch storage group from it")]
            public string CID { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }
    }
    #endregion StorageGroup:Options

    partial class Program
    {
        #region StorageGroup:List
        static async Task SGList(SGOptions.List opts)
        {
            byte[] cid;

            try
            {
                cid = Base58.Decode(opts.CID);
            }
            catch (Exception err)
            {
                Console.WriteLine("wrong cid format: {0}", err.Message);
                return;
            }

            var key = privateKey.FromHex().LoadKey();
            var channel = new Channel(opts.Host, ChannelCredentials.Insecure);

            channel.UsedHost().GetHealth(SingleForwardedTTL, key, opts.Debug).Say();

            // Search for StorageGroups:
            var res = channel.ObjectSearch(SingleForwardedTTL, key,
                query: null,
                cid: cid,
                sg: true,
                debug: opts.Debug);

            Console.WriteLine("\nList results:");
            while (await res.ResponseStream.MoveNext())
            {
                var items = res.ResponseStream.Current.Addresses;
                foreach (var item in items)
                {
                    Console.WriteLine("CID = {0}, OID = {1}",
                        item.CID.ToCID(),
                        item.ObjectID.ToUUID());
                }
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
        #endregion StorageGroup:List
    }
}

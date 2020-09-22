using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Google.Protobuf;
using Grpc.Core;

namespace cmd
{

    #region StorageGroup:Options
    public static class SGOptions
    {
        #region StorageGroup:Options:Get
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
            public Guid SGID { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }
        #endregion StorageGroup:Options:Get

        #region StorageGroup:Options:Delete
        // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
        // So they suggest to use hyphenation on such subcommands.
        [Verb("sg:delete", HelpText = "get storage group from container")]
        public class Delete
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
            public Guid SGID { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }
        #endregion StorageGroup:Options:Delete

        #region StorageGroup:Options:Put
        // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
        // So they suggest to use hyphenation on such subcommands.
        [Verb("sg:put", HelpText = "put storage group into container")]
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
            public IEnumerable<Guid> OIDs { get; set; }

            [Option('d', "debug",
                Default = false,
                Required = false,
                HelpText = "Debug mode will print out additional information after a compiling")]
            public bool Debug { get; set; }
        }
        #endregion StorageGroup:Options:Put

        #region StorageGroup:Options:List
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
        #endregion StorageGroup:Options:List
    }
    #endregion StorageGroup:Options

    partial class Program
    {
    }
}

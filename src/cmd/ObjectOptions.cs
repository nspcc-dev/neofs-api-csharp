using System;
using System.Collections.Generic;
using CommandLine;

namespace cmd
{
    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("object:search", HelpText = "search objects in container")]
    public class ObjectSearchOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch object from it")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, to search object inside container")]
        public string CID { get; set; }

        [Option("query",
            Separator = ',',
            Required = false,
            HelpText = "Query rule to be used for search (Example: --query a=b,b=a)")]
        public IEnumerable<string> Query { get; set; } = null;

        [Option("sg",
            Default = false,
            Required = false,
            HelpText = "Search only StorageGroup objects")]
        public bool SG { get; set; }

        [Option("root",
            Default = false,
            Required = false,
            HelpText = "Search only user's objects")]
        public bool Root { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("object:put", HelpText = "put file into the container")]
    public class ObjectPutOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch object from it")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, required for putting file into")]
        public string CID { get; set; }

        [Option('i', "in",
            Required = true,
            HelpText = "File path, that would be putting into container")]
        public string File { get; set; }

        [Option("plugin",
            Default = false,
            Required = false,
            HelpText = "Use NeoFS Plugin to prevent creation of StorageGroup")]
        public bool Plugin { get; set; }

        [Option('e', "expired",
            Default = (ulong)15,
            Required = false,
            HelpText = "Object expires in minutes, used with --plugin option")]
        public ulong Expired { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    [Verb("object:get", HelpText = "get file from the container")]
    public class ObjectGetOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch object from it")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, that would be getting the file from it")]
        public string CID { get; set; }

        [Option("oid",
            Required = true,
            HelpText = "Object ID, that would be getting from the container")]
        public string OID { get; set; }

        [Option('o', "out",
            Required = true,
            HelpText = "Path to file, were fetched file would be stored")]
        public string Out { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    [Verb("object:delete", HelpText = "remove stored object")]
    public class ObjectDeleteOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch object from it")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, that would be getting the file from it")]
        public string CID { get; set; }

        [Option("oid",
            Required = true,
            HelpText = "Object ID, that would be getting from the container")]
        public string OID { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    [Verb("object:head", HelpText = "receive object headers")]
    public class ObjectHeadOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch object from it")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, that would be getting the file from it")]
        public string CID { get; set; }

        [Option("oid",
            Required = true,
            HelpText = "Object ID, that would be getting from the container")]
        public string OID { get; set; }

        [Option("full",
            Default = false,
            Required = false,
            HelpText = "Fetch full headers")]
        public bool Full { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }
}

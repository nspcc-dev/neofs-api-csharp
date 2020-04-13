using CommandLine;

namespace cmd
{
    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("container:put", HelpText = "Create new container")]
    public class ContainerPutOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch containers list")]
        public string Host { get; set; }

        [Option("size",
            Default = 1,
            Required = false,
            HelpText = "Container capacity in GB")]
        public int Size { get; set; }

        [Option("basic-acl",
            Default = "private",
            HelpText = "Container basic ACL (one of predefined {public, private, readonly} or hex string)")]
        public string BasicACL { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }
    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("container:list", HelpText = "List user containers")]
    public class ContainerListOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch containers list")]
        public string Host { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("container:get", HelpText = "Get user container")]
    public class ContainerGetOptions
    {
        [Option("host",
            Default = "s01.fs.nspcc.ru:8080",
            Required = false,
            HelpText = "Host that would be used to fetch containers list")]
        public string Host { get; set; }

        [Option("cid",
            Required = true,
            HelpText = "Container ID, that would be fetched")]
        public string CID { get; set; }

        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }
}

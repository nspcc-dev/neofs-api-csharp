using CommandLine;

namespace cmd
{
    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("accounting:balance", HelpText = "Fetch account balance")]
    public class AccountingBalanceOptions
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
}

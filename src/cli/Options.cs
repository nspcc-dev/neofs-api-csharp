using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli
{
    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("put", HelpText = "put file into the container")]
    public class PutOptions
    {
        [Option("cid",
            Required =true,
            HelpText = "Container ID, required for putting file into")]
        public string CID { get; set; }
        
        [Option('f', "file",
            Required = true,
            HelpText = "File path, that would be putting into container")]
        public string File { get; set; }
        
        [Option('d', "debug",
            Default = false,
            Required = false,
            HelpText = "Debug mode will print out additional information after a compiling")]
        public bool Debug { get; set; }
    }

    // CommandLine currently does not support sub-commands out-of-the-box, see https://github.com/commandlineparser/commandline/issues/353
    // So they suggest to use hyphenation on such subcommands.
    [Verb("get", HelpText = "get file from the container")]
    public class GetOptions
    {
        [Option("cid",
            Required =true,
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
}

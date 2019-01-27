using CommandLine;
using PwnedPasswordCheckNet.Layouts;

namespace PwnedPasswordCheckNet
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Path to the password's csv file")]
        public string Path {get; set;}
        [Option('l', "layout", Required = true, HelpText = "Password's csv file layout")]
        public LayoutsEnum Layout {get; set;}
    }
}
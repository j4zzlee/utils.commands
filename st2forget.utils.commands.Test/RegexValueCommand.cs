using System;

namespace st2forget.utils.commands.Test
{
    public class RegexValueCommand : Command
    {
        public override string CommandName => "command:regex:test";
        public override string Description => "Only for testing";
        public string VersionRegex { get; set; }
        public int NumberRegex { get; set; }

        public RegexValueCommand()
        {
            AddArgument("version-regex", "v", "Only for testing", true, false, "^\\d+(\\.\\d+)*$");
            AddArgument("number-regex", "n", "Only for testing", true, false, "\\d+");
        }
        protected override void OnExecute()
        {
            Console.WriteLine("OK, Command has run.");
        }

        protected override ICommand Filter()
        {
            VersionRegex = ReadArgument<string>("version-regex");
            NumberRegex = ReadArgument<int>("number-regex");
            return this;
        }

        public string GetVersionRegex()
        {
            return VersionRegex;
        }

        public int GetNumberRegex()
        {
            return NumberRegex;
        }
    }
}
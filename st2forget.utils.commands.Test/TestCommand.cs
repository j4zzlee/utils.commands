using System;

namespace st2forget.utils.commands.Test
{
    public class TestCommand : Command
    {
        public TestCommand()
        {
            AddArgument("require", "r", "Version of project. e.g: v2.0.1", true);
            AddArgument("optional", "o", "optional");
            AddArgument("ounary", "ou", "Unary optional", false, true);
            AddArgument("runary", "ru", "Unary require", true, true);
        }
        public override string CommandName => "command:test";
        public override string Description => "Only for testing";
        protected override void OnExecute()
        {
            Console.WriteLine("OK, Command has run.");
        }

        protected override ICommand Filter()
        {
            var require = ReadArgument<string>("require");
            var optional = ReadArgument<string>("optional");
            var ounary = ReadArgument<bool>("ounary");
            var runary = ReadArgument<bool>("runary");

            Console.WriteLine($"Found migration-path: {require}");
            Console.WriteLine($"Found version: {optional}");
            Console.WriteLine($"Found ticket: {ounary}");
            Console.WriteLine($"Found ticket: {runary}");
            return this;
        }
    }
    
    public class ComplexValueCommand : Command
    {
        public override string CommandName => "command:complexvalue:test";
        public override string Description => "Only for testing";
        public string ComplexStringValue { get; set; }
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }

        public ComplexValueCommand()
        {
            AddArgument("str-value", "s", "Only for testing", true, false);
            AddArgument("int-value", "i", "Only for testing", true, false);
            AddArgument("bool-value", "b", "Only for testing", true, true);
        }
        protected override void OnExecute()
        {
            Console.WriteLine("OK, Command has run.");
        }

        protected override ICommand Filter()
        {
            ComplexStringValue = ReadArgument<string>("str-value");
            IntValue = ReadArgument<int>("int-value");
            BoolValue = ReadArgument<bool>("bool-value");
            return this;
        }

        public string GetStringValue()
        {
            return ComplexStringValue;
        }

        public int GetIntValue()
        {
            return IntValue;
        }

        public bool GetBoolValue()
        {
            return BoolValue;
        }
    }

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

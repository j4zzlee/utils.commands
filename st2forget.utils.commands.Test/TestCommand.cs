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
}

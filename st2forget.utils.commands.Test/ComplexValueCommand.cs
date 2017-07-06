using System;

namespace st2forget.utils.commands.Test
{
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
}
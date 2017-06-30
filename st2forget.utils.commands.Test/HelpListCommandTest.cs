using System;
using System.Collections.Generic;
using Xunit;

namespace st2forget.utils.commands.Test
{
    public class HelpListCommandTest
    {
        [Fact]
        public void Help()
        {
            var command = new HelpListCommand(new List<ICommand>{new TestCommand()});
            command.Help(); // not throwing anything
            Assert.True(true);
        }

        [Fact]
        public void Execute()
        {
            var command = new HelpListCommand(new List<ICommand> { new TestCommand() });
            command.Execute(); // not throwing anything
            Assert.True(true);
        }

        [Fact]
        public void ReadNullArguments()
        {
            var command = new HelpListCommand(new List<ICommand> { new TestCommand() });
            Assert.Throws<ArgumentNullException>(() => command.ReadArguments(null));
        }

        [Fact]
        public void ReadEmptyArguments()
        {
            var command = new HelpListCommand(new List<ICommand> { new TestCommand() });
            command.ReadArguments(new List<string>());
            Assert.True(true);
        }
    }
}
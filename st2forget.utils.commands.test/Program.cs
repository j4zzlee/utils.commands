using System;
using System.Collections.Generic;
using st2forget.console.utils;

namespace st2forget.utils.commands.test
{
    public class TestCommand : Command
    {
        public TestCommand()
        {
            AddArgument("version", "v", "Version of project. e.g: v2.0.1", true);
            AddArgument("migration-path", "p", "Path of migration folder. e.g: C:\\Users\\consoto\\Migrations");
            AddArgument("ticket", "t", "Ticket name. e.g: STF-111", true);
        }
        public override string CommandName => "command:test";
        public override string Description => "Only for testing";
        protected override void OnExecute()
        {
            Console.WriteLine("OK, Command has run.");
        }

        protected override ICommand Filter()
        {
            var migrationPath = ReadArgument<string>("migration-path");
            var version = ReadArgument<string>("version");
            var ticket = ReadArgument<string>("ticket");

            Console.WriteLine($"Found migration-path: {migrationPath}");
            Console.WriteLine($"Found version: {version}");
            Console.WriteLine($"Found ticket: {ticket}");
            return this;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var helpCommand = new HelpListCommand(new List<ICommand>
            {
                new TestCommand()
            });

            helpCommand.Execute();

            var testCommand = new TestCommand();
            testCommand.Help();
            testCommand.ReadArguments(new[]
            {
                "--help"
            });
            testCommand.Execute();

            try
            {
                testCommand.ReadArguments(new[]
                {
                    ""
                });
                testCommand.Execute();
            }
            catch (ArgumentException ex)
            {
                ex.Message.PrettyPrint(ConsoleColor.Red);
            }

            try
            {
                testCommand.ReadArguments(new[]
                {
                    "--version:1.3.2"
                });
                testCommand.Execute();
            }
            catch (ArgumentException ex)
            {
                ex.Message.PrettyPrint(ConsoleColor.Red);
            }

            try
            {
                testCommand.ReadArguments(new[]
                {
                    "--version:1.3.2",
                    "--ticket:abc"
                });
                testCommand.Execute();
            }
            catch (ArgumentException ex)
            {
                ex.Message.PrettyPrint(ConsoleColor.Red);
            }

            try
            {
                testCommand.ReadArguments(new[]
                {
                    "--v:1.3.2"
                });
                testCommand.Execute();
            }
            catch (ArgumentException ex)
            {
                ex.Message.PrettyPrint(ConsoleColor.Red);
            }

            try
            {
                testCommand.ReadArguments(new[]
                {
                    "-v:1.3.2",
                    "-t:abc"
                });
                testCommand.Execute();
            }
            catch (ArgumentException ex)
            {
                ex.Message.PrettyPrint(ConsoleColor.Red);
            }

            Console.ReadKey();
        }
    }
}
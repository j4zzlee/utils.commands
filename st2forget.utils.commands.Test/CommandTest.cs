using System;
using System.Collections.Generic;
using Xunit;

namespace st2forget.utils.commands.Test
{

    public class CommandTest
    {
        public static IEnumerable<object[]> MissingArgumentsData => new[]
        {
            // Missing runary
            new[] { new []{ "-r"}},
            new[] { new []{ "--r"}},
            new[] { new []{ "-r:1"}},
            new[] { new []{ "--r:1"}},
            new[] { new []{ "-require"}},
            new[] { new []{ "--require"}},
            new[] { new []{ "-require:1"}},
            new[] { new []{ "--require:1"}},
            // Missing require
            new[] { new []{ "-ru"}},
            new[] { new []{ "--ru"}},
            new[] { new []{ "-ru:1"}},
            new[] { new []{ "--ru:1"}},
            new[] { new []{ "-runary"}},
            new[] { new []{ "-runary:1"}},
            new[] { new []{ "--runary"}},
            new[] { new []{ "--runary:1"}},
            // Missing require, runary
            new[] { new []{ "-o"}},
            new[] { new []{ "-o:1"}},
            new[] { new []{ "--o"}},
            new[] { new []{ "--o:1"}},
            new[] { new []{ "-optional"}},
            new[] { new []{ "-optional:1"}},
            new[] { new []{ "--optional"}},
            new[] { new []{ "--optional:1"}},
            // One right, other wrong syntax
            new[] { new []{ "-r", "--ru"}},
            new[] { new []{ "-r:1", "--ru"}},
            new[] { new []{ "-r", "--ru:1"}},
            new[] { new []{ "-r:1", "--ru:1"}},

            new[] { new []{ "-r", "-runary"}},
            new[] { new []{ "-r:1", "-runary" } },
            new[] { new []{ "-r", "-runary:1" } },
            new[] { new []{ "-r:1", "-runary:1" } },

            new[] { new []{ "--r", "-ru"}},
            new[] { new []{ "--r:1", "-ru"}},
            new[] { new []{ "--r", "-ru:1"}},
            new[] { new []{ "--r:1", "-ru:1"}},
            
            new[] { new []{ "-require", "-ru"}},
            new[] { new []{ "-require:1", "-ru"}},
            new[] { new []{ "-require", "-ru:1"}},
            new[] { new []{ "-require:1", "-ru:1"}},

            // Both wrong syntax
            new[] { new []{ "-require", "-runary"}},
            new[] { new []{ "-require:1", "-runary" } },
            new[] { new []{ "-require", "-runary:1" } },
            new[] { new []{ "-require:1", "-runary:1" } },
        };

        public static IEnumerable<object[]> MissingValuesData => new[]
        {
            // Required fields is right, but does not have any value
            new[] {new[] {"-r", "-ru"}},
            new[] {new[] {"-r", "-ru:"}},
            new[] {new[] {"-r:", "-ru"}},
            new[] {new[] {"-r:", "-ru:"}},
            new[] {new[] {"--require", "-runary"}},
            new[] {new[] {"--require", "-runary:"}},
            new[] {new[] {"--require:", "-runary"}},
            new[] {new[] {"--require:", "-runary:"}},
        };

        public static IEnumerable<object[]> SuccessData => new[]
        {
            new[] {new[] {"-r:1", "-ru"}},
            new[] {new[] {"-r:1", "-ru:"}},
            new[] {new[] {"-r:1", "--runary"}},
            new[] {new[] {"-r:1", "--runary:"}},
            new[] {new[] {"--require:1", "-ru"}},
            new[] {new[] {"--require:1", "-ru:"}},
            new[] {new[] {"--require:1", "--runary"}},
            new[] {new[] {"--require:1", "--runary:"}}
        };

        [Fact]
        public void Help()
        {
            var command = new TestCommand();
            command.Help();
            Assert.True(true);
        }
        [Fact]
        public void ReadNullArguments()
        {
            var command = new TestCommand();
            Assert.Throws<ArgumentNullException>(() => command.ReadArguments(null));
        }

        [Fact]
        public void ReadEmptyArguments()
        {
            var command = new TestCommand();
            Assert.Throws<ArgumentException>(() => command.ReadArguments(new List<string>()));
        }

        [Theory]
        [MemberData(nameof(MissingArgumentsData))]
        public void MissingArguments(string[] args)
        {
            var command = new TestCommand();
            Assert.Throws<ArgumentException>(() => command.ReadArguments(args));
        }

        [Theory]
        [MemberData(nameof(MissingValuesData))]
        public void MissingValues(string[] args)
        {
            var command = new TestCommand();
            Assert.Throws<ArgumentException>(() => command.ReadArguments(args));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void Success(string[] args)
        {
            var command = new TestCommand();
            command.ReadArguments(args);
            command.Execute();
            Assert.True(true);
        }
    }
}

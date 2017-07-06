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

        public static IEnumerable<object[]> ComplexValueSuccessData => new[]
        {
            new[]
            {
                new[]
                {
                    "--str-value:C:\\Users\\BaoChau\\Source\\Repos\\st2forget.migrations\\st2forget.migrations.Tests\\MigrationTest",
                    "--int-value:3",
                    "--bool-value"
                }
                ,
                new[]
                {
                    "C:\\Users\\BaoChau\\Source\\Repos\\st2forget.migrations\\st2forget.migrations.Tests\\MigrationTest",
                    "3",
                    "True"
                }
            },
            new[]
            {
                new[]
                {
                    "-s:C:\\Users\\BaoChau\\Source\\Repos\\st2forget.migrations\\st2forget.migrations.Tests\\MigrationTest",
                    "-i:3",
                    "-b"
                },
                new[]
                {
                    "C:\\Users\\BaoChau\\Source\\Repos\\st2forget.migrations\\st2forget.migrations.Tests\\MigrationTest",
                    "3",
                    "True"
                }
            }
        };

        [Theory]
        [MemberData(nameof(ComplexValueSuccessData))]
        public void ComplexValueSuccessTest(string[] args, string[] expectedValues)
        {
            var command = new ComplexValueCommand();
            command.ReadArguments(args);

            Assert.Equal(command.ComplexStringValue, expectedValues[0]);
            Assert.Equal(command.IntValue.ToString(), expectedValues[1]);
            Assert.Equal(command.BoolValue.ToString(), expectedValues[2]);
        }

        public static IEnumerable<object[]> RegexSuccessTestCases => new[]
        {
            new[]
            {
                new[]
                {
                    "--version-regex:1",
                    "--number-regex:3"
                }
            },
            new[]
            {
                new[]
                {
                    "-v:1.0",
                    "-n:3"
                }
            },
            new[]
            {
                new[]
                {
                    "--version-regex:1.0.1",
                    "--number-regex:323"
                }
            },
            new[]
            {
                new[]
                {
                    "-v:1.0.0.1",
                    "-n:323"
                }
            }
        };

        [Theory]
        [MemberData(nameof(RegexSuccessTestCases))]
        public void RegexSuccessTest(string[] args)
        {
            var command = new RegexValueCommand();
            command.ReadArguments(args);
            command.Execute();
        }

        public static IEnumerable<object[]> RegexFailTestCases => new[]
        {
            new[]
            {
                new[]
                {
                    "--version-regex:1.",
                    "--number-regex:3"
                }
            },
            new[]
            {
                new[]
                {
                    "-v:1.0",
                    "-n:a"
                }
            }
        };

        [Theory]
        [MemberData(nameof(RegexFailTestCases))]
        public void RegexFailTest(string[] args)
        {
            var command = new RegexValueCommand();
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

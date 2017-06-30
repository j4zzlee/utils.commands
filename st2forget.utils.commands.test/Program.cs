using System;
using System.Collections.Generic;

namespace st2forget.utils.commands.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var helpCommand = new HelpListCommand(new List<ICommand>());
            helpCommand.Execute();
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class HelpCommand : ICommand
    {
        Application app;
        private const string line = "================================================";

        public string Name => "help";
        public string Help => "Краткая помощь по всем командам";

        public string GetDescription()
        {
            return "Выводит список  команд с краткой помощью";
        }

        public string[] Synonyms => new [] { "?" };


        public HelpCommand(Application app)
        {
            this.app = app;
        }
        public void Execute(params string[] parameters)
        {
            Console.WriteLine(line);

            foreach (ICommand cmd in app.Commands)
            {
                Console.WriteLine("{0}: {1}", cmd.Name, cmd.Help);
            }

            Console.WriteLine(line);
        }

        

    }

}

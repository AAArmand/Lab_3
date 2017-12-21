using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class HelpCommand : ICommand
    {
        private readonly Application _app;
        private const string Line = "================================================";

        public string GetName() { return "help"; }
        public string GetHelp() { return "Краткая помощь по всем командам"; }

        public string[] Synonyms => new string[] { "?" };
        public string GetDescription() { return "Выводит список  команд с краткой помощью"; }

        public HelpCommand(Application app) => this._app = app ?? throw new ArgumentNullException(nameof(app));

        public void Execute(params string[] parameters)
        {
            Console.WriteLine(Line);

            foreach (ICommand cmd in _app.Commands)
            {
                Console.WriteLine("{0}: {1}", cmd.GetName(), cmd.GetHelp());
            }

            Console.WriteLine(Line);
        }

        

    }

}

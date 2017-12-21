using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ExitCommand : ICommand
    {
        private readonly Application _app;

        public string Name => "exit"; public string Help => "Выход из программы";
        public string[] Synonyms => new string[] { "quit", "bye" };
        public string GetDescription() { return "Длинное и подробное описание команды выхода "; }

        public ExitCommand(Application app) {
            this._app = app;
        }

        public void Execute(params string[] parameters)
        {
            _app.Exit();
        }
    }
}

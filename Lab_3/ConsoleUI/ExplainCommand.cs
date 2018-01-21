using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ExplainCommand : ICommand
    {
        private readonly Application _app;

        public string Name => "explain"; public string Help => "Рассказать о команде или командах";
        public string[] Synonyms => new string[] { "elaborate" };
        public string GetDescription() { return "Выводит всю доступную информацию по команде или командам. Имена команд передаются как параметры"; }

        public ExplainCommand(Application app) => this._app = app ?? throw new ArgumentNullException(nameof(app));
        public void Execute(params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                ICommand cmd = _app.FindCommand(parameter);
                Console.WriteLine(Line);
                List<string> syns = new List<string>(cmd.Synonyms);
                if (cmd.Name== parameter)
                {
                    Console.WriteLine("{0}: {1}", cmd.Name, cmd.Help);
                }
                else
                {
                    Console.WriteLine("{0}: {1}", parameter, cmd.Help);
                    syns.Remove(parameter);
                    syns.Add(cmd.Name);
                }
                if (syns.Count > 0)
                {
                    Console.WriteLine("Синонимы: {0}", string.Join(", ", syns));
                }
                if (cmd.GetDescription() != string.Empty)
                {
                    Console.WriteLine(Line1);
                    Console.WriteLine(cmd.GetDescription());
                }
            }
            Console.WriteLine(Line);
        }

        private const string Line = "================================================";
        private const string Line1 = "...............................................";

    }

}

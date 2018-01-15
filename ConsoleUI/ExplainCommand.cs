using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ExplainCommand : ICommand
    {
        Application app;

        public string Name => "explain";
        public string Help => "Рассказать о команде или командах";

        public string GetDescription()
        {
            return "Выводит всю доступную информацию по команде или командам. Имена команд передаются как параметры";
        }

        public string[] Synonyms => new string[] { "elaborate" };


        public ExplainCommand(Application app)
        {
            this.app = app;
        }
        public void Execute(params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                ICommand cmd = app.FindCommand(parameter);
                Console.WriteLine(line);
                List<string> syns = new List<string>(cmd.Synonyms);
                if (cmd.Name == parameter)
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
                    Console.WriteLine(line1);
                    Console.WriteLine(cmd.GetDescription());
                }
            }
            Console.WriteLine(line);
        }

        private const string line = "================================================";
        private const string line1 = "...............................................";

    }

}

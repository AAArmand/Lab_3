using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class NotFoundCommand : ICommand
    {
        private string _name;

        public string Name => _name;

        public void SetName(string value) {
            _name = value;
        }

        public string Help => "команда не найдена";
        public string[] Synonyms => new string[] { };
        public string GetDescription() { return ""; }

        public void Execute(params string[] parameters)
        {
            Console.WriteLine("Команда `{0}`  не найдена ", Name);
        }
    }

}

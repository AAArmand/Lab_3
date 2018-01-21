using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class NotFoundCommand : ICommand
    {
        public string Name { get; set; }
        public string Help => "команда не найдена";

        public string GetDescription()
        {
            return "";
        }

        public string[] Synonyms => new string[] { };


        public void Execute(params string[] parameters)
        {
            Console.WriteLine("Команда `{0}`  не найдена ", Name);
        }
    }

}

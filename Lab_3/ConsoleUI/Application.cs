using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleUI
{
    public class Application
    {
        private readonly NotFoundCommand _notFound = new NotFoundCommand();
        private bool _keepRunning = true;
        readonly List<ICommand> _commands = new List<ICommand>();
        readonly Dictionary<string, ICommand> _commandMap = new Dictionary<string, ICommand>();

        public void Exit()
        {
            _keepRunning = false;
        }

        public ICommand FindCommand(string name)
        {
            if (_commandMap.ContainsKey(name))
            {
                return _commandMap[name];
            }
            NotFound.SetName(name);
            return NotFound;
        }

        public IList<ICommand> Commands => _commands;

        internal NotFoundCommand NotFound => _notFound;

        public void AddCommand(ICommand cmd)
        {
            _commands.Add(cmd);
            if (_commandMap.ContainsKey(cmd.Name))
            {
                throw new Exception($"Команда {cmd.Name} уже добавлена");
            }
            _commandMap.Add(cmd.Name, cmd);
            foreach (var s in cmd.Synonyms)
            {
                if (_commandMap.ContainsKey(s))
                {
                    Console.WriteLine("ERROR: Игнорирую синоним {0} для команды {1}, поскольку имя {0}  уже использовано", s, cmd.Name);
                    continue;
                }
                _commandMap.Add(s, cmd);
            }
        }
    
        public void Run(TextReader reader)
        {
            string[] cmdline, parameters;
            while (_keepRunning)
            {
                Console.Write("> ");
                var cmd = reader.ReadLine();
                if (cmd == null) 
                {
                    break;
                }
                cmdline = cmd.Split(
                    new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries
                );
                if (cmdline.Length == 0)
                {
                    continue;
                }

                parameters = new string[cmdline.Length - 1];
                Array.Copy(cmdline, 1, parameters, 0, cmdline.Length - 1);
                FindCommand(cmdline[0]).Execute(parameters);
            }
        }
    }
}

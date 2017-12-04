using ConsoleUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class ListCommand : ICommand{
        public static Dictionary<string, IShape> Shapes = new Dictionary<string, IShape>();
        public string Name { get { return "list"; } }

        public string Help { get { return "Выводит список фигур на картинке"; } }
        public string Description { get { return "Выводит список фигур на картинке, не принимает параметров"; } }
        public string[] Synonyms { get { return new string[] { "bill", "roll" }; } }
        

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 0) {
                    throw new FormatException("Команда не принимает параметров");
                }
                
                if (Shapes.Count == 0) {
                    throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }

                int i = 0;
                foreach (string shape in Shapes.Keys) {
                    Console.WriteLine("[{0}] {1}", i, shape);
                    i++;
                }

            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}

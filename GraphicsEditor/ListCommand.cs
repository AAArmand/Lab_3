using ConsoleUI;
using DrawablesUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class ListCommand : ICommand{
        private readonly Picture _picture;

        public string Name => "list"; public string Help => "Выводит список фигур на картинке";
        public string GetDescription() { return "Выводит список фигур на картинке, не принимает параметров"; }

        public string[] Synonyms => new string[] { "bill", "roll" };
        public ListCommand(Picture picture) {
            this._picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 0) {
                    throw new FormatException("Команда не принимает параметров");
                }
                
                if (!_picture.Shapes.Any()) {
                   throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }
                

                int i = 0;
                foreach (IShape shape in _picture.Shapes ) {
                    Console.WriteLine("[{0}] {1}", i, shape.Description);
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

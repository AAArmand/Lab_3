using ConsoleUI;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawablesUI;

namespace GraphicsEditor {
    class ColorCommand :CommandIndex, ICommand {
        public string Name { get { return "color"; } }

        public string Help { get { return "Изменяет цвет линий фигуры на заданный"; } }
        public string Description { get { return "Изменяет цвет линий фигуры. Первый параметр - название цвета, последующие - индексы фигур"; } }
        public string[] Synonyms { get { return new string[] { "coloring", "colouring" }; } }

        public ColorCommand(Picture picture) :base(picture) {        
        }

        

        public void Execute(params string[] parameters) {
            try {
                int[] indexes = ValidateIndexes(parameters);

                if (indexes!= null) {
                    int i = 0;
                    foreach (IShape shape in picture.Shapes) {
                        if (indexes.Contains(i)) {
                            shape.Format.Color = ColorTranslator.FromHtml(parameters[0]);
                        }
                        i++;
                    }
                    picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }
                
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (Exception) {
                Console.WriteLine("Цвет " + parameters[0] + " не существует");
            }
        }
    }
}

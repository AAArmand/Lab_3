using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class WidthCommand : CommandIndex, ICommand {
        public string Name => "width"; public string Help => "Изменяет ширину линий фигуры на заданный";
        public string GetDescription() { return "Изменяет ширину линий фигуры. Первый параметр - ширина, последующие - индексы фигур"; }

        public string[] Synonyms => new string[] { "breadth", "w" };

        public WidthCommand(Picture picture) : base(picture) { }

        public void Execute(params string[] parameters) {
            try {

                int[] indexes = ValidateIndexes(parameters);

                if (indexes != null) {
                    int i = 0;
                    foreach (IShape shape in Picture.Shapes) {
                        if (indexes.Contains(i)) {
                            shape.Format.Width = uint.Parse(parameters[0]);
                        }
                        i++;
                    }

                    Picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }

            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (FormatException) {
                Console.WriteLine("Вы ввели ширину в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве ширины");
            }
        }
    }
}

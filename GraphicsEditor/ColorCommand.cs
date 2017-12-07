using ConsoleUI;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawablesUI;

namespace GraphicsEditor {
    class ColorCommand : ICommand {
        private Picture picture;
        public string Name { get { return "color"; } }

        public string Help { get { return "Изменяет цвет линий фигуры на заданный"; } }
        public string Description { get { return "Изменяет цвет линий фигуры. Первый параметр - название цвета, последующие - индексы фигур"; } }
        public string[] Synonyms { get { return new string[] { "coloring", "colouring" }; } }

        public ColorCommand(Picture picture) {
            this.picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {

                List<int> indexes = new List<int>();
                int index;
                foreach (string parametr in parameters.Skip(1)) {
                    index = int.Parse(parametr);
                    if (index < 0) {
                        throw new ArgumentException("Индекс " + index + " не может быть отрицательным");
                    }

                    if (index >= picture.Shapes.Count()) {
                        throw new ArgumentException("Не существует фигуры с индексом " + index);
                    }

                    if (indexes.Contains(index)) {
                        throw new ArgumentException("Индекс " + index + " повторяется");
                    } else {
                        indexes.Add(index);
                    }
                }
                
                /* тут выбрасывается исключение, о том, 
                 что коллекцию изменили в процессе выполнения */
                int i = 0;
                foreach (IShape shape in picture.Shapes) {
                    if (indexes.Contains(i)) {
                        shape.Format.Color = ColorTranslator.FromHtml(parameters[0]);
                        picture.Add(i, shape);
                        picture.RemoveAt((i + 1));
                    }
                    i++;
                }
            } catch (FormatException) {
                Console.WriteLine("Вы ввели индексы в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (Exception) {
                Console.WriteLine("Цвет " + parameters[0] + " не существует");
            }
        }
    }
}

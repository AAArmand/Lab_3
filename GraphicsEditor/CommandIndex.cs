using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    abstract class CommandIndex {

        protected Picture Picture;

        protected CommandIndex(Picture picture) => this.Picture = picture ?? throw new ArgumentNullException(nameof(picture));

        protected int[] ValidateIndexes(string[] parameters) {
            try {
                List<int> indexes = new List<int>();

                foreach (string parametr in parameters)
                {
                    int index = int.Parse(parametr);

                    if (index < 0)
                    {
                        throw new ArgumentException("Индекс " + index + " не может быть отрицательным");
                    }

                    if (index >= Picture.Shapes.Count())
                    {
                        throw new ArgumentException("Не существует фигуры с индексом " + index);
                    }

                    if (indexes.Contains(index))
                    {
                        throw new ArgumentException("Индекс " + index + " повторяется");
                    }
                    else
                    {
                        indexes.Add(index);
                    }
                }

                return indexes.ToArray();

            } catch (FormatException) {
                Console.WriteLine("Вы ввели индексы в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
            return null;
        }
    }
}

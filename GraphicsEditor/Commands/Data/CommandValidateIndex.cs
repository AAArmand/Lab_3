using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEditor.Commands.Data {
    abstract class CommandIndex {
        protected readonly Picture Picture;
        protected CommandIndex(Picture picture) => Picture = picture ?? throw new ArgumentNullException(nameof(picture));

        protected int[] ValidateRepeatIndexes(int[] indexes) {
            try {
                foreach (int index in indexes) {
                    if (indexes.Contains(index)) {
                        throw new ArgumentException("Индекс " + index + " повторяется");
                    }
                }
                return indexes;
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }

            return null;
        }

        protected int[] ValidateStringIndexes(string[] parameters) {
            try {
                List<int> indexes = new List<int>();

                foreach (string parametr in parameters)
                {
                    int index = int.Parse(parametr);

                    if (index < 0)
                    {
                        throw new ArgumentException("Индекс " + index + " не может быть отрицательным");
                    }

                    if (index > Picture.ShapesIndexes.Last())
                    {
                        throw new ArgumentException("Не существует фигуры с индексом " + index);
                    }

                    indexes.Add(index);
                    
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

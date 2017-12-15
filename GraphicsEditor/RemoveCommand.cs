using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class RemoveCommand : ICommand {

        private Picture picture;
        public string Name { get { return "remove"; } }

        public string Help { get { return "Удаляет фигуры с картинки"; } }
        public string Description { get { return "Удаляет фигуры с картинки. Параметры команды — индексы элементов, которые нужно удалить с картинки"; } }
        public string[] Synonyms { get { return new string[] { "delete", "cut" }; } }

        public RemoveCommand(Picture picture) {
            this.picture = picture;
        }

        private void DecrimentArray(ref int[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i]--;
            }
        }

        private void DeleteShape(int[] indexes) {
            for (int i = 0; i < indexes.Length; i++) {
                picture.RemoveAt(indexes[i]);
                DecrimentArray(ref indexes);
            }
        }

        private int[] ValidateIndexes(string[] parameters) {
            try {
                List<int> indexes = new List<int>();
                int index;
                foreach (string parametr in parameters) {
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

                return indexes.ToArray();

            } catch (FormatException) {
                Console.WriteLine("Вы ввели индексы в неверном формате");
                return null;
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
                return null;
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
                return null;
            }

        }
        
        public void Execute(params string[] parameters) {
            try {
                int[] deleteIndexes = ValidateIndexes(parameters);
                if (deleteIndexes != null) {
                    Array.Sort(deleteIndexes);
                    DeleteShape(deleteIndexes);
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
 
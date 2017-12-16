using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class GroupCommand : ICommand {
        private Picture picture;
        public string Name { get { return "group"; } }

        public string Help { get { return "Группировка фигур"; } }
        public string Description { get { return "Переносит фигуры, идентификаторы которых перечислены в параметрах, в новую составную фигуру, которая добавляется на картинку"; } }
        public string[] Synonyms { get { return new string[] { "grouping", "gr" }; } }

        public GroupCommand(Picture picture) {
            this.picture = picture;
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
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
            return null;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters[0] == "") {
                    throw new FormatException("Команда должна принимать параметры");
                }

                if (picture.Shapes.Count() == 0) {
                    throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }

                int[] indexes = ValidateIndexes(parameters);

                if (indexes) {
                    CompoundShape compoundShape = new CompoundShape(picture, indexes);
                    picture.Add(compoundShape);
                } else {
                    throw new ArgumentException("Введите индексы заново");
                }
                
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}

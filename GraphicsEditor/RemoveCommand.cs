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

        public void Execute(params string[] parameters) {
            try {
                List<int> indexes = new List<int>();
                int index;
                foreach (string parametr in parameters) {
                    index = int.Parse(parametr);
                    if (index < 0) {
                        throw new ArgumentException("Индекс " + index + " не может быть отрицательным");
                    }

                    /*if (index >= picture.Shapes.Count) {
                        throw new ArgumentException("Не существует фигуры с индексом " + index);
                    }*/

                    if (indexes.Contains(index)) {
                        throw new ArgumentException("Индекс " + index + " повторяется");
                    } else {
                        indexes.Add(index);
                    }
                }

                
                Тут должно быть удаление по индексу
                 
                int i = 0;
                foreach (int deleteIndex in indexes) {
                    picture.RemoveAt(deleteIndex - i);
                    //ListCommand.Shapes.RemoveAt(deleteIndex - i);
                    i++;
                }

            } catch (FormatException) {
                Console.WriteLine("Вы ввели индексы в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } 
        }
    }
}
 
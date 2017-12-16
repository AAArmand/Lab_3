using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class RemoveCommand :CommandIndex, ICommand {
        
        public string Name { get { return "remove"; } }

        public string Help { get { return "Удаляет фигуры с картинки"; } }
        public string Description { get { return "Удаляет фигуры с картинки. Параметры команды — индексы элементов, которые нужно удалить с картинки"; } }
        public string[] Synonyms { get { return new string[] { "delete", "cut" }; } }

        public RemoveCommand(Picture picture):base(picture) {
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
 

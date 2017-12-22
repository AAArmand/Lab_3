using System;
using ConsoleUI;
using GraphicsEditor.Commands.Data;

namespace GraphicsEditor.Commands.ShapesDataCommands {
    class RemoveCommand : CommandIndex, ICommand {
        public string Name => "remove"; public string Help => "Удаляет фигуры с картинки";
        public string GetDescription() { return "Удаляет фигуры с картинки. Параметры команды — индексы элементов, которые нужно удалить с картинки"; }

        public string[] Synonyms => new string[] { "delete", "cut" };
        public RemoveCommand(Picture picture):base(picture) { }

        private void DecrimentArray(ref int[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i]--;
            }
        }

        public void DeleteShape(int[] indexes) {
            int dec = 0;
            foreach (int index in indexes) {
               Picture.RemoveAt(index - dec);
                dec++;
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
 

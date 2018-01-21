﻿using System;
using ConsoleUI;
using GraphicsEditor.Commands.Data;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class RemoveCommand : CommandValidateIndex, ICommand {
        public string Name => "remove"; public string Help => "Удаляет фигуры с картинки";
        public string GetDescription() { return "Удаляет фигуры с картинки. Параметры команды — индексы элементов, которые нужно удалить с картинки"; }

        public string[] Synonyms => new string[] { "delete", "cut" };
        public RemoveCommand(Picture picture):base(picture) { }

        private void DecrimentArray(ref int[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i]--;
            }
        }

        private void DeleteShape(int[] indexes) {
            for (int i = 0; i < indexes.Length; i++) {
                Picture.RemoveAt(indexes[i]);
                DecrimentArray(ref indexes);
            }
        }

        
        public void Execute(params string[] parameters) {
            try {
                int[] deleteIndexes = ValidateStringIndexes(parameters);
                indexes = ValidateRepeatIndexes(indexes);

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
 
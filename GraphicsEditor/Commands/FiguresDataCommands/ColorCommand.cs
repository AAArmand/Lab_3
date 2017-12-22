using System;
using System.Drawing;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class ColorCommand : CommandIndex, ICommand {
        public string Name => "color"; public string Help => "Изменяет цвет линий фигуры на заданный";
        public string GetDescription() { return "Изменяет цвет линий фигуры. Первый параметр - название цвета, последующие - индексы фигур"; }

        public string[] Synonyms => new[] { "coloring", "colouring" };
        public ColorCommand(Picture picture) :base(picture) { }


        public void Execute(params string[] parameters) {
            try {
                int[] indexes = ValidateIndexes(parameters);

                if (indexes!= null) {
                    foreach (int index in indexes) {
                        if (Picture.Figures[index] != null) {
                            Picture.Figures[index].Format.Color = ColorTranslator.FromHtml(parameters[0]);                          ;
                        }
                    }                
                    Picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }
                
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (Exception) {
                Console.WriteLine("Цвет " + parameters[0] + " не существует");
            }
        }
    }
}

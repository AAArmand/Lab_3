using System;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.ShapesDataCommands {
    class WidthCommand : CommandValidateIndex, ICommand {
        public string Name => "width"; public string Help => "Изменяет ширину линий фигуры на заданный";
        public string GetDescription() { return "Изменяет ширину линий фигуры. Первый параметр - ширина, последующие - индексы фигур"; }

        public string[] Synonyms => new string[] { "breadth", "w" };

        public WidthCommand(Picture picture) : base(picture) { }

        public void Execute(params string[] parameters) {
            try {

                int[] indexes = ValidateIndexes(parameters.Skip(1).ToArray());

                if (indexes != null) {
                    int i = 0;
                    foreach (IShape shape in Picture.Shapes) {
                        if (indexes.Contains(i)) {
                            shape.Format.Width = uint.Parse(parameters[0]);
                        }
                        i++;
                    }

                    Picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }

            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (FormatException) {
                Console.WriteLine("Вы ввели ширину в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве ширины");
            }
        }
    }
}

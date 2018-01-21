using ConsoleUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Commands.ShapesDataCommands {
    class ScaleCommand : CommandIndex, ICommand {
        public string Name => "scale";
        public string Help => "масштабирование";
        public string GetDescription() { return "преобраование сжатия/растяжения/инверсии"; }

        public string[] Synonyms => new string[] { "scaling", "sc" };

        public ScaleCommand(Picture picture) : base(picture) { }

        public void Execute(params string[] parameters) {
            try {
                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);
                int[] indexes = ValidateStringIndexes(parameters.Skip(2).ToArray());
                indexes = ValidateRepeatIndexes(indexes);

                if (indexes != null) {
                    int i = 0;
                    foreach (IShape shape in Picture.Shapes) {
                        if (indexes.Contains(i)) {
                            shape.Transform(Transformation.Scale(x, y));
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
                Console.WriteLine("Вы ввели координату в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве координаты");
            }
        }

    }
}

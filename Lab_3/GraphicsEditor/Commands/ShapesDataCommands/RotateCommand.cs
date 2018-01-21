using ConsoleUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Commands.ShapesDataCommands {
    class RotateCommand : CommandValidateIndex, ICommand {
        public string Name => "rotate";
        public string Help => "вращение фигуры";
        public string GetDescription() { return "вращение фигуры относительно вектора"; }

        public string[] Synonyms => new string[] { "rotation", "rot" };

        public RotateCommand(Picture picture) : base(picture) { }

        public void Execute(params string[] parameters) {
            try {
                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);
                PointF point = new PointF(x, y);
                float angle = float.Parse(parameters[2]);
                int[] indexes = ValidateIndexes(parameters.Skip(3).ToArray());

                if (parameters.Length < 3) {
                    throw new ArgumentNullException("Введите индексы фигур фигур");
                }

                if (indexes != null) {
                    int i = 0;
                    foreach (IShape shape in Picture.Shapes) {
                        if (i >= Picture.GetFigures().Count) {
                            throw new ArgumentException("Не существует фигуры с индексом " + i);
                        }

                        if (indexes.Contains(i)) {
                            shape.Transform(Transformation.RotateAt(angle, point));
                        }
                        i++;
                    }

                    Picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }

            } catch (ArgumentNullException error) {
                Console.WriteLine(error.Message);
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

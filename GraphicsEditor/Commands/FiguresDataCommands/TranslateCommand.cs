using System;
using System.Drawing;
using System.Linq;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands
{
    public class TranslateCommand
    {
        public string Name => "translate";
        public string Help => "Параллельный перенос фигуры";
        public string GetDescription() { return "Осуществляет параллельный перенос на вектор"; }

        public string[] Synonyms => new string[] { "trans", "translation" };

        public TranslateCommand(Picture picture) : base(picture) { }

        public void Execute(params string[] parameters)
        {
            try
            {
                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);
                PointF point = new PointF(x, y);

                if (parameters.Length < 3)
                {
                    throw new ArgumentNullException("Введите индексы фигур фигур");
                }

                int[] indexes = ValidateIndexes(parameters.Skip(2).ToArray());


                if (indexes != null)
                {
                    int i = 0;
                    foreach (IShape shape in Picture.Shapes)
                    {
                        if (i >= Picture.GetFigures().Count)
                        {
                            throw new ArgumentException("Не существует фигуры с индексом " + i);
                        }

                        if (indexes.Contains(i))
                        {
                            shape.Transform(Transformation.Translate(point));
                        }
                        i++;
                    }

                    Picture.OnChanged();
                }
                else
                {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }

            }
            catch (ArgumentNullException error)
            {
                Console.WriteLine(error.Message);
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Вы ввели координату в неверном формате");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Вы ввели слишком большое число в качестве координаты");
            }
        }
    }
}
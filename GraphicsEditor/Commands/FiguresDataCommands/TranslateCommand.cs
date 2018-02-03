using System;
using System.Drawing;
using System.IO;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands
{
    class TranslateCommand<TShape> : ICommand where TShape : IShape {
        public string Name => "translate";
        public string Help => "Параллельный перенос фигуры";
        public string GetDescription() { return "Осуществляет параллельный перенос на вектор"; }

        public string[] Synonyms => new string[] { "trans", "translation" };
        private readonly IContainer<IDrawable> _picture;

        public TranslateCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(params string[] parameters)
        {
            try {
                if (ValidationHelper.ParametersCountValidator(parameters, 3) &&
                    ValidationHelper.ContainsInContainerValidator<TShape>((IContainer<TShape>)_picture,
                        "Не нарисовано ни одной фигуры")) {

                    float x = float.Parse(parameters[0]);
                    float y = float.Parse(parameters[1]);
                    PointF point = new PointF(x, y);
                    uint[] index = IndexHelper.StringToIndexesOrFail(parameters[2]);

                    if (index != null) {
                        ShapeLocator<TShape> shape = ShapeLocator<TShape>.ParseOrFail(index, _picture);
                        if (shape != null) {
                            shape.Shape.Transform(Transformation.Translate(point));                      

                        } else {
                            throw new InvalidDataException("Повторите ввод индексов фигур");
                        }
                    }
                } else {
                    throw new ArgumentException("Введите индексы заново");
                }

                _picture.OnChanged();


            } catch (InvalidDataException error) {
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
using System;
using System.Collections.Generic;
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
    class RotateCommand<TShape> : ICommand where TShape : IShape
    {
        public string Name => "rotate";
        public string Help => "вращение фигуры";
        public string GetDescription() { return "вращение фигуры относительно вектора"; }
        private readonly IContainer<IDrawable> _picture;
        public string[] Synonyms => new []{ "rotation", "rot" };

        public RotateCommand(IContainer<IDrawable> picture) => _picture = picture;
        

        public void Execute(params string[] parameters)
        {
            try
            {
                if (ValidationHelper.ParametersCountValidator(parameters, 4) &&
                    ValidationHelper.ContainsInContainerValidator<TShape>((IContainer<TShape>) _picture,
                        "Не нарисовано ни одной фигуры"))
                {

                    float x = float.Parse(parameters[0]);
                    float y = float.Parse(parameters[1]);
                    PointF point = new PointF(x, y);
                    float angle = float.Parse(parameters[2]);
                    uint[] index = IndexHelper.StringToIndexesOrFail(parameters[3]);

                    if (index != null)
                    {
                        ShapeLocator<TShape> shape = ShapeLocator<TShape>.ParseOrFail(index, _picture);
                        if (shape != null)
                        {
                            shape.Shape.Transform(Transformation.RotateAt(angle, point));

                        }
                        else
                        {
                            throw new InvalidDataException("Повторите ввод индексов фигур");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Введите индексы заново");
                }

                _picture.OnChanged();
           

            }
            catch (InvalidDataException error)
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
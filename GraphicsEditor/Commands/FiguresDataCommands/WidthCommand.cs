using System;
using System.IO;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class WidthCommand<TShape> : ICommand where TShape : IShape 
        {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "width";
        public string Help => "Изменяет ширину линий фигуры на заданный";
        public string GetDescription() { return "Изменяет ширину линий фигуры. Первый параметр - ширина, последующие - индексы фигур"; }

        public string[] Synonyms => new string[] { "breadth", "w" };

        public WidthCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(params string[] parameters) {
            try {
                if (ValidationHelper.ParametersEmptyValidator(parameters) && ValidationHelper.ContainsInContainerValidator(_picture, "Не нарисовано ни 1 элемента")) {

                    uint[][] indexes = IndexHelper.StringArrayToIndexesOrFail(parameters);

                    if (indexes != null)
                    {
                        foreach (uint[] index in indexes)
                        {
                            try
                            {
                                var shape = ShapeLocator<TShape>.ParseOrFail(index, _picture);
                                if (shape != null)
                                {
                                    shape.Shape.Format.Width = uint.Parse(parameters[0]);
                                }
                                else
                                {
                                    throw new InvalidDataException("Повторите ввод индексов фигур");
                                }
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine(e.Message, IndexHelper.IndexesToString(index));
                            }
                        }
                        _picture.OnChanged();
                    }
                    else
                    {
                        throw new InvalidDataException("Повторите ввод индексов фигур");
                    }
                }
            } catch (InvalidDataException error) {
                Console.WriteLine(error.Message);
            } catch (FormatException) {
                Console.WriteLine("Вы ввели ширину в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве ширины");
            }
        }
    }
}

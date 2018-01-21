using System;
using System.Drawing;
using System.IO;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class ColorCommand<T>: ICommand where T : IDrawable,IShape      
        {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "color"; public string Help => "Изменяет цвет линий фигуры на заданный";
        public string GetDescription() { return "Изменяет цвет линий фигуры. Первый параметр - название цвета, последующие - индексы фигур"; }

        public string[] Synonyms => new[] { "coloring", "colouring" };
        public ColorCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(params string[] parameters) {
            try {
                if (ValidationHelper.ParametsEmptyValidator(parameters))
                {
                    uint[][] indexes = IndexHelper.StringToIndexesOrFail(parameters);

                    if (indexes != null && ValidationHelper.IndexesDistinctValidator(indexes))
                    {
                        foreach (uint[] index in indexes)
                        {
                            var shape = ShapeLocator<T>.ParseOrFail(index, _picture);
                            if (shape != null)
                            {
                                shape.Shape.Format.Color = ColorTranslator.FromHtml(parameters[0]);
                            }
                            else
                            {
                                throw new InvalidDataException("Повторите ввод индексов фигур");
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
            } catch (Exception) {
                Console.WriteLine("Цвет " + parameters[0] + " не существует");
            }
        }
    }
}

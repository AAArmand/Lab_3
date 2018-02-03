using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class RemoveCommand<T> : ICommand where T: IFigure 
        {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "remove";
        public string Help => "Удаляет фигуры с картинки";

        public string GetDescription()
        {
            return "Удаляет фигуры с картинки. Параметры команды — индексы элементов, которые нужно удалить с картинки";
        }

        public RemoveCommand(IContainer<IDrawable> picture) => _picture = picture;
        public string[] Synonyms => new string[] {"delete", "cut"};

        public void Execute(params string[] parameters)
        {
            try
            {
                if (ValidationHelper.ParametersEmptyValidator(parameters) && ValidationHelper.ContainsInContainerValidator<IFigure>(_picture, "Не нарисовано ни одной фигуры"))
                {
                    uint[][] indexes = IndexHelper.StringArrayToIndexesOrFail(parameters);

                    if (indexes != null) {                        
                        List<ShapeLocator<IFigure>> shapeLocators = new List<ShapeLocator<IFigure>>();

                        foreach (uint[] index in indexes) {
                            ShapeLocator<IFigure> shape = ShapeLocator<IFigure>.ParseOrFail(index, _picture);
                            if (shape != null) {
                                shapeLocators.Add(shape);
                            } else {
                                throw new InvalidDataException("Повторите ввод индексов фигур");
                            }
                        }
                        ShapeLocatorsHelper<IFigure>.ValidateShapeLocators(shapeLocators);

                        foreach (ShapeLocator<IFigure> shapeLocator in shapeLocators) {
                            shapeLocator.Parent.Remove(shapeLocator.Shape);
                        }                  
                        ShapeLocatorsHelper<IFigure>.ContainerChecker(shapeLocators);
                        _picture.OnChanged();
                    } else {
                        throw new ArgumentException("Введите индексы заново");
                    }
                }
            }           
            catch (NullReferenceException error)
            {
                Console.WriteLine(error.Message);
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
            }
        }

    }
}
 

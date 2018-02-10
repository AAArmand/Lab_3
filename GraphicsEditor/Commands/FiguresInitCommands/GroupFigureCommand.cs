using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresInitCommands {
    class GroupFigureCommand : ICommand
        {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "group";
        public string Help => "Группировка фигур";

        public string GetDescription()
        {
            return
                "Переносит фигуры, идентификаторы которых перечислены в параметрах, в новую составную фигуру, которая добавляется на картинку";
        }

        public string[] Synonyms => new string[] {"grouping", "gr"};

        public GroupFigureCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(params string[] parameters)
        {

            try
            {
                if (ValidationHelper.ParametersEmptyValidator(parameters) && ValidationHelper.ContainsInContainerValidator<IFigure>(_picture, "Не нарисовано ни одной фигуры"))
                {

                    uint[][] indexes = IndexHelper.StringArrayToIndexesOrFail(parameters);
                    if (indexes != null)
                    {

                        if (indexes.Length == 1)
                        {
                            
                            throw new InvalidDataException("Нельзя группировать 1 фигуру");
                        }
                        List<ShapeLocator<IFigure>> shapeLocators = new List<ShapeLocator<IFigure>>();

                        foreach (uint[] index in indexes)
                        {
                            ShapeLocator<IFigure> shape = ShapeLocator<IFigure>.ParseOrFail(index, _picture);
                            if (shape != null)
                            {
                                shapeLocators.Add(shape);
                            }
                            else
                            {
                                throw new InvalidDataException("Повторите ввод индексов фигур");
                            }
                        }
                        if (!ShapeLocatorsHelper<IFigure>.ParentEqualsChecker(shapeLocators))
                        {
                            throw new InvalidDataException(
                                "Нельзя группировать фигуры из разных сгриппированных фигур");
                        }

                        IContainer<IFigure> parent = shapeLocators.First().Parent;

                        if (parent.GetAll<IFigure>().Count == shapeLocators.Count && !parent.Equals(_picture))
                        {
                            throw new InvalidDataException(
                                "Введенные фигуры уже сгруппированы");
                        }

                        List<IFigure> figures = new List<IFigure>();                        
                        foreach (ShapeLocator<IFigure> shapeLocator in shapeLocators)
                        {
                            shapeLocator.Parent.Remove(shapeLocator.Shape);
                            figures.Add(shapeLocator.Shape);

                        }
                        parent.Add(new CompoundFigure(_picture, figures));
                        ShapeLocatorsHelper<IFigure>.ContainerChecker(shapeLocators);
                    }
                    else
                    {
                        throw new InvalidDataException("Введите индексы заново");
                    }
                }

            }
            catch (FormatException error)
            {
                Console.WriteLine(error.Message);
            }
            catch (InvalidDataException error)
            {
                Console.WriteLine(error.Message);
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine(error.Message);
            }           
        }
    }
}

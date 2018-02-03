using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands
{
    class UngroupCommand<TDrawable> : ICommand where TDrawable : IDrawable
    {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "ungroup";
        public string Help => "разгруппировка фигур";
        public string GetDescription() { return "разгруппировка фигур. Параметр - идентификатор составной фигуры, которую нужно разгруппировать"; }
        public string[] Synonyms => new[] {"ungroping", "ungr"};
        
        public UngroupCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(string[] parameters)
        {
            try {
                if (ValidationHelper.ParametersEmptyValidator(parameters)  && ValidationHelper.ContainsInContainerValidator<IContainer<TDrawable>>(_picture, "Не нарисовано ни одной сгруппированной фигуры")) {
                   
                    uint[][] indexes = IndexHelper.StringArrayToIndexesOrFail(parameters);

                    if (indexes != null) {                       
                        List<ShapeLocator<TDrawable>> shapeList = new List<ShapeLocator<TDrawable>>();
                        
                        foreach (uint[] index in indexes) {
                            ShapeLocator<TDrawable> shape = ShapeLocator<TDrawable>.ParseOrFail(index, _picture);

                            if (shape != null) {
                                shapeList.Add(shape);
                            } else {
                                throw new InvalidDataException("Повторите ввод индексов фигур");
                            }
                        }               
                        ShapeLocatorsHelper<TDrawable>.ValidateShapeLocators(shapeList);

                        for (int index = 0; index < shapeList.Count; index++) {
                            ShapeLocator<TDrawable> shape = shapeList[index];

                            if (ShapeLocatorsHelper<TDrawable>.IsContainer(shape.Shape)) {                                                   
                                IContainer<TDrawable> container = (IContainer<TDrawable>) shape.Shape;

                                foreach (TDrawable item in container.GetAll<TDrawable>())
                                {                                  
                                    ShapeLocator<TDrawable> ungroupItem = shapeList.FirstOrDefault(element => element.Shape.Equals(item)) ;
                                    if (ungroupItem != null && shapeList.IndexOf(ungroupItem) > index)
                                    {
                                        if (ungroupItem.GrandParent == (IContainer<TDrawable>)shape.Shape) {
                                            ungroupItem.GrandParent = shape.GrandParent;
                                        }
                                        if (ungroupItem.Parent == (IContainer<TDrawable>)shape.Shape) {
                                            ungroupItem.Parent = shape.Parent;
                                        }
                            
                                    }                                    
                                    shape.Parent.Add(item);
                                }
                                shape.Parent.Remove(shape.Shape);
                                ShapeLocatorsHelper<TDrawable>.ContainerChecker(shape);
                            } else {
                                if (shape.GrandParent == null) {                         
                                    throw new InvalidDataException(
                                        "Вы пытаетесь разгруппировать несгруппированную фигуру (введенный индекс: " +
                                        IndexHelper.IndexesToString(indexes[index]) + ")");
                                }
                                shape.Parent.Remove(shape.Shape);
                                shape.GrandParent.Add(shape.Shape);
                        
                                ShapeLocatorsHelper<TDrawable>.ContainerChecker(shape);
                            }
                        }                       
                    } else {
                        throw new InvalidDataException("Повторите ввод индексов фигур");
                    }
                }            
            } catch (InvalidDataException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}

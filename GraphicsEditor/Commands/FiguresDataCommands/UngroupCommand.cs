using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands //тут не забудь отсутп
{
    class UngroupCommand<T>/*тип без пробелов*/ : ICommand where T : IDrawable//тут везде двоеточия
    {
        private readonly IContainer<IDrawable> _picture;
        public string Name => "ungroup";// тут тоже везде отступы
        public string Help => "разгруппировка фигур";
        public string GetDescription() { return "разгруппировка фигур. Параметр - идентификатор составной фигуры, которую нужно разгруппировать"; }
        public string[] Synonyms => new[] {"ungroping", "ungr"};
        // между полями и конструктором 1 строчка отступа
        public UngroupCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(string[] parameters)
        {
            try {
                if (ValidationHelper.ParametsEmptyValidator(parameters)  && ValidationHelper.ContainsInContainerValidator<IContainer<T>>(_picture, "Не нарисовано ни одной сгруппированной фигуры")) {
                    // просек фишку, что все блоки кода вот в таком формате: ключевое слово пробел условие пробел скобка
                    uint[][] indexes = IndexHelper.StringToIndexesOrFail(parameters);

                    if (indexes != null && ValidationHelper.IndexesDistinctValidator(indexes)) {                       
                        List<ShapeLocator<T>> shapeList = new List<ShapeLocator<T>>();
                        // между объявлениями... логическими кусками тоже отступ
                        foreach (uint[] index in indexes) {
                            ShapeLocator<T> shape = ShapeLocator<T>.ParseOrFail(index, _picture);

                            if (shape != null) {
                                shapeList.Add(shape);
                            } else {
                                throw new InvalidDataException("Повторите ввод индексов фигур");
                            }
                        }// тут можно без отступа                   
                        ShapeHelper<T>.ValidateShapeLocators(shapeList);

                        for (int index = 0; index < shapeList.Count; index++) {
                            ShapeLocator<T> shape = shapeList[index];

                            if (shape.Shape.GetType().GetInterfaces().Contains(typeof(IContainer<T>))) {
                                shape.Parent.Remove(shape.Shape);                                 
                                IContainer<T> container = (IContainer<T>) shape.Shape;
                                foreach (T item in container.GetAll<T>())
                                {
                                    ShapeLocator<T> ungroupItem = shapeList.FirstOrDefault(element => element.Shape.Equals(item)) ;
                                    if (ungroupItem != null)
                                    {
                                        if (ungroupItem.GrandParent == (IContainer<T>) shape.Shape)
                                        {
                                            ungroupItem.GrandParent = shape.GrandParent;
                                        }
                                        if (ungroupItem.Parent == (IContainer<T>) shape.Shape)
                                        {
                                            ungroupItem.Parent = shape.Parent;
                                        }

                                    }
                                    shape.Parent.Add(item);
                                }
                            } else {
                                if (shape.GrandParent == null) {
                                    // Эту хуйню решарпер делает, хз, хочешь делай, хочешь нет (макс 80 символов в строке)
                                    throw new InvalidDataException(
                                        "Вы пытаетесь разгруппировать несгруппированную фигуру (введенный индекс: " +
                                        IndexHelper.IndexesToString(indexes[index]) + ")");
                                }
                                shape.Parent.Remove(shape.Shape);
                                shape.GrandParent.Add(shape.Shape);
                            }
                        }
                        ShapeHelper<T>.ContainerChecker(shapeList);
                    } else {
                        throw new InvalidDataException("Повторите ввод индексов фигур");
                    }
                }            
            } catch (InvalidDataException error) {
                Console.WriteLine(error.Message);
            } // Вот тут еще внимание обращай и удаляй ненужное
        }
    }
}

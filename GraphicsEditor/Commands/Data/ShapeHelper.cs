using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    // 1)почему нельзя заменить T на IDrawable
    // 2)что происходит вообще
    class ShapeHelper<T> where T : IDrawable
    {
        //почему это не вынесено в ShapeLocator
        public static void ValidateShapeLocators(List<ShapeLocator<T>> shapeLocators)
        {
            List<ShapeLocator<T>> locators = new List<ShapeLocator<T>>();
            locators = locators.Concat(shapeLocators).ToList();

            foreach (ShapeLocator<T> shapeLocator in locators)
            {
                if (shapeLocator.Shape.GetType().GetInterfaces().Contains(typeof(IContainer<T>)))
                {
                    shapeLocators.RemoveAll(shape => shape.Parent == (IContainer<T>)shapeLocator.Shape && !shape.Shape.GetType().GetInterfaces().Contains(typeof(IContainer<T>)));
                }

            }
        }

        public static void ContainerChecker(List<ShapeLocator<T>> shapeLocators)
        {
            foreach (ShapeLocator<T> shapeLocator in shapeLocators)
            {
                if (shapeLocator.GrandParent != null)
                {
                    if (shapeLocator.Parent.GetAll<T>().Count == 1)
                    {
                        shapeLocator.GrandParent.Add(shapeLocator.Parent.GetAll<T>().First());
                        shapeLocator.GrandParent.Remove((T) shapeLocator.Parent);
                        shapeLocator.Parent.Remove(shapeLocator.Parent.GetAll<T>().First());
                    }
                    if (!shapeLocator.Parent.GetAll<T>().Any())
                    {
                        shapeLocator.GrandParent.Remove((T) shapeLocator.Parent);
                    }
                }
            }
        }

        public static bool ParentEqualsChecker(List<ShapeLocator<T>> shapeLocators)
        {
            foreach (ShapeLocator<T> shapeLocatorMain in shapeLocators)
            {
                foreach (ShapeLocator<T> shapeLocator in shapeLocators)
                {
                    if (!Equals(shapeLocatorMain.Parent, shapeLocator.Parent))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    
    class ShapeLocatorsHelper<TDrawable> where TDrawable : IDrawable
    {
        public static void ValidateShapeLocators(List<ShapeLocator<TDrawable>> shapeLocators)
        {
            List<ShapeLocator<TDrawable>> locators = new List<ShapeLocator<TDrawable>>();
            locators = locators.Concat(shapeLocators).ToList();
            
            foreach (ShapeLocator<TDrawable> shapeLocator in locators)
            {               
                if (IsContainer(shapeLocator.Shape))
                {
                    shapeLocators.RemoveAll(shape => shape.Parent == (IContainer<TDrawable>)shapeLocator.Shape && !shape.Shape.GetType().GetInterfaces().Contains(typeof(IContainer<TDrawable>)));
                }

                var items = locators.Where(item => item.Parent == shapeLocator.Parent);
                if (shapeLocator.GrandParent != null && shapeLocator.Parent.GetAll<TDrawable>().Count == items.Count())
                {
                    shapeLocators.Remove(items.First());
                }
            }
        }

        public static void ContainerChecker(List<ShapeLocator<TDrawable>> shapeLocators)
        {
            foreach (ShapeLocator<TDrawable> shapeLocator in shapeLocators)
            {
                ContainerChecker(shapeLocator);
            }
        }

        public static void ContainerChecker(ShapeLocator<TDrawable> shapeLocator) {         
                if (shapeLocator.GrandParent != null) {
                    if (shapeLocator.Parent.GetAll<TDrawable>().Count == 1) {
                        shapeLocator.GrandParent.Add(shapeLocator.Parent.GetAll<TDrawable>().First());
                        shapeLocator.GrandParent.Remove((TDrawable)shapeLocator.Parent);
                    }else if (!shapeLocator.Parent.GetAll<TDrawable>().Any()) {
                        shapeLocator.GrandParent.Remove((TDrawable)shapeLocator.Parent);
                    }
                }           
        }

        public static bool ParentEqualsChecker(List<ShapeLocator<TDrawable>> shapeLocators)
        {
            foreach (ShapeLocator<TDrawable> shapeLocatorMain in shapeLocators)
            {
                foreach (ShapeLocator<TDrawable> shapeLocator in shapeLocators)
                {
                    if (!Equals(shapeLocatorMain.Parent, shapeLocator.Parent))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsContainer(TDrawable item)
        {
            return item.GetType().GetInterfaces().Contains(typeof(IContainer<TDrawable>));
        }
    }
}
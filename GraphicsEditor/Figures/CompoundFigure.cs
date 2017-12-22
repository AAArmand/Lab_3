using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Commands.ShapesDataCommands;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class CompoundFigure : Figure
    {  
        public List<IFigure> Figures { get; } = new List<IFigure>();

        public CompoundFigure(Picture picture, int[] indexes) {
            foreach (int index in indexes) {           
                if (picture.Figures[index] != null) {
                    Figures.Add(picture.Figures[index]);
                }
            }
            Array.Sort(indexes);
            RemoveCommand remove = new RemoveCommand(picture);
            remove.DeleteShape(indexes);
            Description.DescriptionText = "Составная фигура";
        }




        public override void Transform(Transformation trans)
        {

        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Format.Color, Format.Width);
            foreach (IFigure figure in Figures)
            {
                figure.Draw(drawer);
            }
        }


        public override string GenerateDescription(int[] indexes) {
            List<string> description = new List<string> {base.GenerateDescription(indexes)};
            Array.Resize(ref indexes, indexes.Length + 1);
            int lastElement = indexes.Length - 1;
            indexes[lastElement] = 0;

            foreach (IFigure figure in Figures) {
                description.Add(figure.GenerateDescription(indexes));
                indexes[lastElement]++;
            }

            return String.Join("\n", description);
        }

        public override string GenerateDescription(int index) {       
            return GenerateDescription(new[]{ index });          
        }

    }
}

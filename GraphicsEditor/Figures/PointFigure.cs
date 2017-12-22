using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class PointFigure : Figure {

        public PointF Сoordinates { get;}

        public PointFigure(float pointX, float pointY) {
            Сoordinates = new PointF (pointX, pointY);
            Description.DescriptionText = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }

        public override void Draw (IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }

        public override void Transform(Transformation trans) {

        }
    }
}

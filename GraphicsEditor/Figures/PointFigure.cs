using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class PointFigure : FigureDescribtion, IFigure {

        public PointF Сoordinates { get; set; }

        public PointFigure(float pointX, float pointY) {
            Сoordinates = new PointF (pointX, pointY);
            Description = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }

        public void Draw (IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }

        public void Transform(Transformation trans) {
            Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
        }
    }
}

using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;
using System.Drawing.Drawing2D;

namespace GraphicsEditor.Figures {
    class PointFigure : Figure {

        public PointF Сoordinates { get; set; }

        public PointFigure(float pointX, float pointY) {
            Сoordinates = new PointF(pointX, pointY);
            Description.DescriptionText = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }

        public PointFigure(PointF point) {
            Сoordinates = point;
            Description.DescriptionText = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }

        public override void Draw (IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }

        public override void Transform(Transformation trans) {
            Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
        }
    }
}

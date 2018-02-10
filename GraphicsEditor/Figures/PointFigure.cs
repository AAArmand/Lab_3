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
            SetDescription();
        }

        public override void SetDescription() {
            Description = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }
        public void Draw (IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }

        public void Transform(Transformation trans)
        {
            PointF[] points = { Сoordinates };
            trans.TransformMatrix.TransformPoints(points);
            Сoordinates = points[0];
            //Сoordinates = trans.TransformMatrix.Elements[0] > 1 ? Сoordinates : new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
            SetDescription();
        }
    }
}

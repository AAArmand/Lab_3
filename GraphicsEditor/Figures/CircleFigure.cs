using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class CircleFigure : Figure {
        private PointFigure DotOfCenter { get; }
        private float Diametr { get; set; }

        public CircleFigure(PointFigure center, float r) {
            DotOfCenter = center;
            Diametr = 2 * r;
            Description.DescriptionText = "Круг(" + center.Description.DescriptionText + ", " + "Радиус = " + r + ")";
        }

        public override void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            SizeF sizes = new SizeF(Diametr, Diametr);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, sizes, 0, 360, 0);
        }

<<<<<<< Updated upstream
        public override void Transform(Transformation trans) {
            DotOfCenter.Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
            if (trans.TransformMatrix.Elements[0] != 0) {
=======
        public void Transform(Transformation trans) {
            DotOfCenter.Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
            if (trans.TransformMatrix.Elements[0] != 0)
            {
>>>>>>> Stashed changes
                Diametr = trans.TransformMatrix.Elements[0];
            }
        }
    
    }
}

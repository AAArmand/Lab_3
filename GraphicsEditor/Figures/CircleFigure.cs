using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class CircleFigure : FigureDescribtion, IFigure{
        private PointFigure DotOfCenter { get; }
        private float Diametr { get; set; }

        public CircleFigure(PointFigure center, float r) {
            DotOfCenter = center;
            Diametr = 2 * r;
            Description = "Круг(" + center.Description + ", " + "Радиус = " + r + ")";
        }

        public override void SetDescription() {
            Description = "Круг(" + DotOfCenter.Description + ", " + "Радиус = " + Diametr / 2 + ")";
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            SizeF sizes = new SizeF(Diametr, Diametr);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, sizes, 0, 360, 0);
        }

        public void Transform(Transformation trans)
        {
            DotOfCenter.Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
            if (trans.TransformMatrix.Elements[0] > 1)
            {
                Diametr *= trans.TransformMatrix.Elements[0];
            }
            SetDescription();
        }
    
    }
}

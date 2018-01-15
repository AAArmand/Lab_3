using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class CircleFigure : FigureDescribtion, IFigure{
        private PointFigure DotOfCenter { get; }
        private float Diametr { get; }

        public CircleFigure(PointFigure center, float r) {
            DotOfCenter = center;
            Diametr = 2 * r;
            Description = "Круг(" + center.Description + ", " + "Радиус = " + r + ")";
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            SizeF sizes = new SizeF(Diametr, Diametr);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, sizes, 0, 360, 0);
        }

        public void Transform(Transformation trans) {   }
    
    }
}

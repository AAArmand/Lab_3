using DrawablesUI;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphicsEditor {
    class Circle : IShape {
        private Point DotOfCenter { get; set; }

        public string Description { get; private set; }

        private float Diametr { get; set; }

        public FormatInfo Format { get; set; }

        public Circle(Point center, float r) {
            DotOfCenter = center;
            Diametr = 2 * r;
            Description = "Круг(" + center.Description + ", " + "Радиус = " + r + ")";
            Format = new FormatInfo();
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            SizeF sizes = new SizeF(Diametr, Diametr);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, sizes, 0, 360, 0);
        }

        public void Transform(Transformation trans) {   }
    }
}

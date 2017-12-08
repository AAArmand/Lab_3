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

        public Circle(Point Center, float R) {
            DotOfCenter = Center;
            Diametr = 2 * R;
            Description = "Круг(" + Center.Description + ", " + "Радиус = " + R + ")";
            Format = new FormatInfo(Color.Black);
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            SizeF Sizes = new SizeF(Diametr, Diametr);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, Sizes, 0, 360, 0);
        }
    }
}

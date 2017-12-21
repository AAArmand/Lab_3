using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawablesUI;
using System.Drawing;

namespace GraphicsEditor {
    class Point : IShape {

        public PointF Сoordinates { get;}

        public string Description { get;}

        public FormatInfo Format { get; set; } = new FormatInfo();

        public Point(float pointX, float pointY) {
            Сoordinates = new PointF (pointX, pointY);
            Description = "Точка(" + Сoordinates.X + ", " + Сoordinates.Y + ")";
        }

        public void Draw (IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }

        public void Transform(Transformation trans) {

        }

    }
}

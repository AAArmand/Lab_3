using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class Line : IShape {
        private Point Beginning { get;}

        public string Description { get;}

        private Point End { get; }

        public FormatInfo Format { get; set; }

        public Line(Point first, Point second) {
            Beginning = first;
            End = second;
            Description = "Линия(" + first.Description + ", " + second.Description + ")";
            Format = new FormatInfo();
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawLine(Beginning.Сoordinates, End.Сoordinates);
        }

        public void Transform(Transformation trans) {

        }

    }
}

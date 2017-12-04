using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class Line : IShape {
        private Point Beginning { get; set; }

        private Point End{ get; set; }

        public FormatInfo Format { get; set; }

        public Line(Point first, Point second) {
            Beginning = first;
            End = second;
        }

        public void Draw(IDrawer drawer) {
            Format = new FormatInfo(Color.Black, 5);
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawLine(Beginning.Сoordinates, End.Сoordinates);
        }
        
    }
}

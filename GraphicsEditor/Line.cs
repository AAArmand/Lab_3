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

        public string Description { get; private set; }

        private Point End{ get; set; }

        public FormatInfo Format { get; set; }

        public Line(Point first, Point second) {
            Beginning = first;
            End = second;
            Description = "Линия(" + first.Description + ", " + second.Description + ")";
        }

        public void Draw(IDrawer drawer) {
            Format = Format ?? new FormatInfo();
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawLine(Beginning.Сoordinates, End.Сoordinates);
        }
        
    }
}

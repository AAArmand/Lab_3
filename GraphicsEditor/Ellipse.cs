using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor {
    class Ellipse : IShape, IDescription {
        private Point DotOfCenter { get; set; }

        public DescriptionData Description { get; private set; }

        private SizeF Sizes { get; set; }

        private float Rotate { get; set; }

        public FormatInfo Format { get; set; }

        public Ellipse(Point Center, float Width, float Height, float Angle) {
            DotOfCenter = Center;
            Sizes = new SizeF(Width, Height);
            Rotate = Angle;
            Description = new DescriptionData("Эллипс(" + Center.Description + ", " + 
                "Ось a = " + Height + ", " + "Ocь b = " + Width + ", " + 
                "Угол поворота = " + Angle + ")");
            Format = new FormatInfo();
        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, Sizes, 0, 360, Rotate);
        }

        public void Transform(Transformation trans) {

        }
        
    }
}

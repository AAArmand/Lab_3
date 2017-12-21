using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor {
    class Ellipse : IShape {
        private Point DotOfCenter { get;}

        public string Description { get;}

        private SizeF Sizes { get;}

        private float Rotate { get;}

        public FormatInfo Format { get; set; } = new FormatInfo();

        public Ellipse(Point center, float width, float height, float angle) {
            DotOfCenter = center;
            Sizes = new SizeF(width, height);
            Rotate = angle;
            Description = "Эллипс(" + center.Description + ", " + "Ось a = " + height + ", " + "Ocь b = " + width + ", " + "Угол поворота = " + angle + ")";
        }

        public void Draw(IDrawer drawer)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer)); 
            
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, Sizes, 0, 360, Rotate);
            
        }

        public void Transform(Transformation trans) {

        }
        
    }
}

using System;
using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class EllipseFigure : Figure {
        private PointFigure DotOfCenter { get;}

        private SizeF Sizes { get;}

        private float Rotate { get;}

        public EllipseFigure(PointFigure center, float width, float height, float angle) {
            DotOfCenter = center;
            Sizes = new SizeF(width, height);
            Rotate = angle;
            Description.DescriptionText = "Эллипс(" + center.Description.DescriptionText + ", " + "Ось a = " + height + ", " + "Ocь b = " + width + ", " + "Угол поворота = " + angle + ")";
        }

        public override void Draw(IDrawer drawer)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer)); 
            
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, Sizes, 0, 360, Rotate);
            
        }

        public override void Transform(Transformation trans) {

        }
    }
}

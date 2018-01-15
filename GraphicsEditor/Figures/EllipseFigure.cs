using System;
using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class EllipseFigure : FigureDescribtion, IFigure {
        private PointFigure DotOfCenter { get;}

        private SizeF Sizes { get;}

        private float Rotate { get;}

        public EllipseFigure(PointFigure center, float width, float height, float angle) {
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
            DotOfCenter.Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
//            Rotate = Math.Asin(trans.TransformMatrix.Elements[1]) * 180 / Math.PI;
//            if (trans.TransformMatrix.Elements[0] != 0)
//            {
//                Sizes = new SizeF(trans.TransformMatrix.Elements[0], trans.TransformMatrix.Elements[3]);
//            }
        }
    }
}

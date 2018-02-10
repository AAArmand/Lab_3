using System;
using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class EllipseFigure : FigureDescribtion, IFigure {
        private PointFigure DotOfCenter { get;}

        private SizeF Sizes { get; set; }

        private double Rotate { get; set; }

        public EllipseFigure(PointFigure center, float width, float height, float angle) {
            DotOfCenter = center;
            Sizes = new SizeF(width, height);
            Rotate = angle;
            SetDescription();
        }

        public void Draw(IDrawer drawer)
        {           
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawEllipseArc(DotOfCenter.Сoordinates, Sizes, 0, 360, (float)Rotate);  
        }

        public override void SetDescription() {
            Description = "Эллипс(" + DotOfCenter.Description + ", " + "Ось a = " + Sizes.Height + ", " + "Ocь b = " + Sizes.Width + ", " + "Угол поворота = " + Rotate + ")";
        }


        public void Transform(Transformation trans)
        {
            DotOfCenter.Сoordinates = new PointF(trans.TransformMatrix.OffsetX, trans.TransformMatrix.OffsetY);
            Rotate += Math.Asin(trans.TransformMatrix.Elements[1]) * 180 / Math.PI;

            if (trans.TransformMatrix.Elements[0] > 1)
            {
                Sizes = new SizeF(Sizes.Width * trans.TransformMatrix.Elements[0], Sizes.Height * trans.TransformMatrix.Elements[3]);

            }
            SetDescription();
        }
    }
}

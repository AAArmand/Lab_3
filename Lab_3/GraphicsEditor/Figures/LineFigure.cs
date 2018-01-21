using System;
using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class LineFigure : Figure {      
        private PointFigure Beginning { get; set; }
        private PointFigure End { get; set; }

        public LineFigure(PointFigure first, PointFigure second) {
            Beginning = first;
            End = second;
            Description.DescriptionText = "Линия(" + first.Description.DescriptionText + ", " + second.Description.DescriptionText + ")";
        }

        public override void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawLine(Beginning.Сoordinates, End.Сoordinates);
        }

        public override void Transform(Transformation trans) {
            PointF[] points = new PointF[] { Beginning.Сoordinates, End.Сoordinates };
            trans.TransformMatrix.TransformPoints(points);
            Beginning.Сoordinates = points[0];
            End.Сoordinates = points[1];
        }

    }
}

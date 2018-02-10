using System;
using System.Drawing;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class LineFigure : FigureDescribtion, IFigure {      
        private PointFigure Beginning { get; set; }
        private PointFigure End { get; set; }

        public LineFigure(PointFigure first, PointFigure second) {
            Beginning = first;
            End = second;
            SetDescription();
        }

        public override void SetDescription() {
            Description = "Линия(" + Beginning.Description + ", " + End.Description + ")";
        }
        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawLine(Beginning.Сoordinates, End.Сoordinates);
        }

        public void Transform(Transformation trans) {
            PointF[] points = { Beginning.Сoordinates, End.Сoordinates };
            trans.TransformMatrix.TransformPoints(points);
            Beginning.Сoordinates = points[0];
            End.Сoordinates = points[1];
            SetDescription();
        }

    }
}

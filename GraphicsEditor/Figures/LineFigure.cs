using System;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class LineFigure : Figure {      
        private PointFigure Beginning { get;}
        private PointFigure End { get; }

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

        }

    }
}

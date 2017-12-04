﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawablesUI;
using System.Drawing;

namespace GraphicsEditor {
    class Point : IShape {

        public PointF Сoordinates { get;  set; }

        public FormatInfo Format { get; set; }
        
        public Point(float PointX, float PointY) {
            Сoordinates = new PointF (PointX,PointY);        
        }

        public void Draw (IDrawer drawer) {
            Format = new FormatInfo(Color.Black, 5);
            drawer.SelectPen(Format.Color, Format.Width);
            drawer.DrawPoint(Сoordinates);         
        }
        
    }
}

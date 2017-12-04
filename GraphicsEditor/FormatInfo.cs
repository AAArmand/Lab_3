using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    public class FormatInfo {
        public System.Drawing.Color Color { get; set; }

        public uint Width { get; set; }

        public FormatInfo(System.Drawing.Color color, uint width) {
            Color = color;
            Width = width;
        }
    }
}

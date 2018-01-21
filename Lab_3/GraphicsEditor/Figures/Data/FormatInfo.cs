using System.Drawing;

namespace GraphicsEditor.Figures.Data {
    public class FormatInfo {
        public Color Color { get; set; } = Color.Black;

        public uint Width { get; set; } = 5;
        
        public FormatInfo(Color color, uint width) {
            Color = color;
            Width = width;
        }

        public FormatInfo(Color color) => Color = color;

        public FormatInfo(uint width) => Width = width;

        public FormatInfo() { }
    }
}

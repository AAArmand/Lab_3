using DrawablesUI;

namespace GraphicsEditor.Figures.Data.Interfaces {
    interface IShape : IDrawable
    {
        FormatInfo Format { get; set; }
  
        void Transform(GraphicsEditor.Transformation trans);
    }
}

using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures.Data
{
    abstract class Figure : IFigure
    {
        public FormatInfo Format { get; set; } = new FormatInfo();    
        public Description Description { get; } = new Description();

        public virtual void Draw(IDrawer drawer) {
            throw new System.NotImplementedException();
        }

        public virtual void Transform(Transformation trans) {
            throw new System.NotImplementedException();
        }

        public virtual string GenerateDescription(int index)
        {
            Description.SetIndex(index);
            return Description.Index + " " + Description.DescriptionText;
        }

        public virtual string GenerateDescription(int[] indexes)
        {
            Description.SetIndex(indexes);
            return Description.Index + " " + Description.DescriptionText;
        }
    }
}
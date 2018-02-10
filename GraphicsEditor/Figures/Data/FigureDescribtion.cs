using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures.Data
{
    abstract class FigureDescribtion
    {
        public FormatInfo Format { get; set; } = new FormatInfo();    
        public string Description { get; protected set; }

        public virtual string GenerateDescription(uint index)
        {
           
            return IndexHelper.IndexToString(index) + " " + Description;
        }

        public virtual string GenerateDescription(uint[] indexes)
        {     
            return IndexHelper.IndexesToString(indexes) + " " + Description;
        }

        public virtual void SetDescription() {
           
        }
    }
}
using DrawablesUI;

namespace GraphicsEditor.Figures.Data.Interfaces
{
    interface  IFigure : IShape
    {
        string Description { get; }

        string GenerateDescription(uint index);

        string GenerateDescription(uint[] indexes);

    }
}
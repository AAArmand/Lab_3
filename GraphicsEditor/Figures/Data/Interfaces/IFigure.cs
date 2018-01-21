namespace GraphicsEditor.Figures.Data.Interfaces
{
    interface IFigure : IShape
    {
        Description Description { get; }

        string GenerateDescription(int index);

        string GenerateDescription(int[] indexes);

    }
}
using System;

namespace GraphicsEditor.Figures.Data
{
    class Description
    {
        protected char[] Borders = {'[', ']'};
        protected const string Delimiter = ":";
        public string Index { get; protected set; }
        public string DescriptionText { get; set; }


        public void SetIndex(int index)
        {
            Index = Borders[0] + index.ToString() + Borders[1];

        }

        public void SetIndex(int[] indexes)
        {
            Index = Borders[0] + String.Join(Delimiter, indexes) + Borders[1];
        }

    }
}

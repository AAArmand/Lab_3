using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    abstract class Description {
        protected char[] Borders = { '[', ']' };
        protected const string Delimiter = ":";
        public string Index { get; protected set; }
        public string DescriptionText { get; protected set; }
        

        protected void SetIndex (int index) {
            Index = Borders[0] + index.ToString() + Borders[1];
        }

        abstract protected string GenerateDescription(int index);
    }
}

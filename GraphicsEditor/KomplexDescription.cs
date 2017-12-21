using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    abstract class KomplexDescription : Description{

        public void SetIndex(int[] indexes) {
            Index = Borders[0] + String.Join(Delimiter, indexes) + Borders[1];
        }  
    }
}

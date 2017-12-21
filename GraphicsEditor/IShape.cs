using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    interface IShape : IDrawable{
        FormatInfo Format { get; set; }

        void Transform(Transformation trans);
    }
}

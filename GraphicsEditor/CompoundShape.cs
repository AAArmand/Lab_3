using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class CompoundShape : KomplexDescription, IShape {      
        public FormatInfo Format { get; set; }
        protected List<IShape> Shapes { get; set; }

        public CompoundShape(Picture picture, int[] indexes) {
            Shapes = new List<IShape>();

            int i = 0;
            foreach (IShape shape in picture.Shapes) {
                if (indexes.Contains(i)) {
                    Shapes.Add(shape);
                }
            }

            DeleteShape(picture, indexes);
            DescriptionText = "Составная фигура";
            Format = new FormatInfo();
        }


        private void DecrimentArray(ref int[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i]--;
            }
        }

        private void DeleteShape(Picture picture, int[] indexes) {
            for (int i = 0; i < indexes.Length; i++) {
                picture.RemoveAt(indexes[i]);
                DecrimentArray(ref indexes);
            }
        }

       

        public void Transform(Transformation trans) {

        }

        public void Draw(IDrawer drawer) {
            drawer.SelectPen(Format.Color, Format.Width);
            foreach (IShape shape in Shapes) {
                shape.Draw(drawer);
            }
        }
    }
}

using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class CompoundShape : IShape {
        public List<IShape> Shapes { get; private set; }

        public FormatInfo Format { get; set; }

        public string Description { get; private set; }

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

        public CompoundShape(Picture picture, int[] indexes) {
            Shapes = new List<IShape>();

            int i = 0;
            foreach (IShape shape in picture.Shapes) {
                if (indexes.Contains(i)) {
                    Shapes.Add(shape);
                }
            }

            DeleteShape(picture, indexes);
            Description = "Составная фигура";
            Format = new FormatInfo();
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

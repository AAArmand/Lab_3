using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class Transformation {
        private Matrix TransformMatrix { get;}

        /// Возвращает преобразование поворота на угол angle вокруг точки (0,0)
        public static Transformation Rotate(float angle) {
            Transformation transformation = new Transformation();
            transformation.TransformMatrix.Rotate(angle);
            return transformation;
        }

        /// Возвращает преобразование параллельного переноса на вектор ((0,0), point)
        public static Transformation Translate(PointF point) {
            Transformation transformation = new Transformation();
            transformation.TransformMatrix.Translate(point.X, point.Y);
            return transformation;
        }

        /// Возвращает преобразование масштабирования с коэффициентами scaleX и scaleY
        /// относительно точки (0, 0).
        /// отрицательные значения параметров соответствуют инверсии
        public static Transformation Scale(float scaleX, float scaleY) {
            Transformation transformation = new Transformation();
            PointF point = new PointF(scaleX, scaleY);
            transformation.TransformMatrix.Scale(transformation[point].X, transformation[point].Y);
            return transformation;
        }

        /// Возвращает центральное аффинное преобразование, заданное матрицей 2x2
        public static Transformation FromMatrix(float[] matrix) {
            Transformation transformation = new Transformation
            {
                TransformMatrix = new Matrix(matrix[0], matrix[1], matrix[2], matrix[3], 0, 0)
            };
            return transformation;
        }

        /// Возвращает преобразование, получающееся последовательным применением
        /// преобразований a и b
        public static Transformation operator *(Transformation a, Transformation b) {
            Transformation transformation = new Transformation();
            transformation.TransformMatrix.Multiply(a.TransformMatrix);
            transformation.TransformMatrix.Multiply(b.TransformMatrix);
            return transformation;
        }

        /// Для любой точки плоскости возвращает её образ
        public PointF this[PointF point] => new PointF(Math.Abs(point.X), Math.Abs(point.Y));

        private Transformation() {
            TransformMatrix = new Matrix();
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawablesUI
{
    public class  GraphicsDrawer : IDrawer, IDisposable
    {
        private readonly Graphics graph; //инкапсулирует поверхность рисования
        private float pointWidth; //ширина точки рисования
        private Pen pen = new Pen(Color.Black, 1); //Определяет объект, используемый для рисования прямых линий и кривых

        public GraphicsDrawer(Graphics g)
        {
            graph = g;
            g.PageUnit = GraphicsUnit.Pixel; // задаем единицу измерения
            g.PageScale = 1; // масштаб единиц измерения
            g.CompositingQuality = CompositingQuality.HighQuality; // задаем высокое качество отрисовки
            g.SmoothingMode = SmoothingMode.AntiAlias; // задаем качество отрисовки данного объекта
        }

        public void SelectPen(Color color, uint width=1)
        {
            pen.Dispose(); // очищает объект pen от всех настроек
            pen = new Pen(color, width); // задаем новые настройки
            pointWidth = width / graph.PageScale; // задаем ширину кисти с учетом масштабов
        }

        public void DrawPoint(PointF point) 
        {
            using (var b = new SolidBrush(pen.Color))
            {
                graph.FillEllipse(b, new RectangleF(
                    new PointF(point.X - pointWidth/2, point.Y - pointWidth/2),
                    new SizeF(pointWidth, pointWidth)
                    ));
            }
        }

        public void DrawLine(PointF start, PointF end)
        {
            graph.DrawLine(pen, start, end);
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            graph.TranslateTransform(center.X, center.Y); // изменяем начало отсчета координат на заданные 
            graph.RotateTransform(rotate); // устанавливает поворот
            graph.TranslateTransform(-center.X, -center.Y);
            graph.DrawArc(pen,new RectangleF(
                new PointF(center.X - sizes.Width/2, center.Y - sizes.Height/2), sizes
                ), startAngle, endAngle); // Рисует дугу, которая является частью эллипса, заданного парой координат, шириной и высотой
            graph.ResetTransform(); // сбрасывает матрицу
        }

        public void Dispose()
        {
            pen.Dispose(); // очистка объекта
        }
    }
}
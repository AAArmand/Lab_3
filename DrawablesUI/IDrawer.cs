using System.Drawing;

namespace DrawablesUI
{
    public interface IDrawer
    {
        void SelectPen(Color color, uint width=1); //задает размеры и цвет "ручке"
        void DrawPoint(PointF point); //рисует точку с координатами x и y
        void DrawLine(PointF start, PointF end); //рисует линию от start до end
        void DrawEllipseArc(PointF center, SizeF size, 
            float startAngle=0, float endAngle=360, float rotate=0); //рисует эллипс
    }
}
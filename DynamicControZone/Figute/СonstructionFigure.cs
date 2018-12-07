using System.Drawing;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий построение основной структуры фигуры.
    /// </summary>
    public static class СonstructionFigure
    {
        /// <summary>
        /// Метод, выполняющий пострение структуры фигуры.
        /// </summary>
        /// <para name = "Start">Переменная, хранящая начальную координату фигуры.</para>
        /// <para name = "End">Переменная, хранящая конечную координату фигуры.</para>
        public static RectangleF ShowRectangleFloat(PointF Start, PointF End)
        {
            float _left = ((Start.X - End.X > 0) ? End.X : Start.X);
            float _down = ((Start.Y - End.Y > 0) ? Start.Y : End.Y);
            float _top = ((Start.Y - End.Y > 0) ? End.Y : Start.Y);
            float _right = ((Start.X - End.X > 0) ? Start.X : End.X);

            return RectangleF.FromLTRB(_left, _top, _right, _down);
        }

        /// <summary>
        /// Метод, выполняющий пострение структуры фигуры.
        /// </summary>
        /// <para name = "Start">Переменная, хранящая начальную координату фигуры.</para>
        /// <para name = "End">Переменная, хранящая конечную координату фигуры.</para>
        public static Rectangle ShowRectangleInt(PointF Start, PointF End)
        {
            int _left = (int)((Start.X - End.X > 0) ? End.X : Start.X);
            int _down = (int)((Start.Y - End.Y > 0) ? Start.Y : End.Y);
            int _top = (int)((Start.Y - End.Y > 0) ? End.Y : Start.Y);
            int _right = (int)((Start.X - End.X > 0) ? Start.X : End.X);

            return Rectangle.FromLTRB(_left, _top, _right, _down);
        }

        /// <summary>
        /// Метод, выполняющий пострение структуры опорных точек.
        /// </summary>
        /// <para name = "SelectPoint">Переменная, хранящая координату опорной точки.</para>
        /// <para name = "Width">Переменная, хранящая толщину линии.</para>
        public static RectangleF SelectFigure(PointF SelectPoint, float Width)
        {
            float _left = SelectPoint.X - 5 - Width / 2;
            float _down = SelectPoint.Y - 5 - Width / 2;
            float  _top = SelectPoint.Y + 5 + Width / 2;
            float _right = SelectPoint.X + 5 + Width / 2;

            return RectangleF.FromLTRB(_right, _down, _left, _top);
        }
    }
}

using System.Drawing;

namespace ProjectSettings
{
    /// <summary>
    /// Класс для сохранения альтернативных зон 
    /// </summary>
    public class RegionRectangle
    {
        /// <summary>
        /// Прямоугольник отражающий новую зону
        /// </summary>
        public RectangleF Rectangle { set; get; }

        /// <summary>
        /// Номер объекта
        /// </summary>
        public int IdObject { set; get; }

        /// <summary>
        /// Видимость
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Стартовая координата X
        /// </summary>
        public float StartX { set; get; }

        /// <summary>
        /// Стартовая координата Y
        /// </summary>
        public float StartY { set; get; }

        /// <summary>
        /// Ширина области
        /// </summary>
        public float Width { set; get; }

        /// <summary>
        /// Высота области
        /// </summary>
        public float Height { set; get; }
    }
}

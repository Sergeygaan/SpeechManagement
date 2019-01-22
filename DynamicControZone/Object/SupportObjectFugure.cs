using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyPaint
{
    /// <summary>
    /// Класс, являющийся опорными точками
    /// </summary>
    public class SupportObjectFugure
    {
        /// <summary>
        /// Метод, выполняющий действия над номером фигуры.
        /// </summary>
        public int IdFigure { get; set; }

        /// <summary>
        /// Метод, выполняющий действия над графическим представлением фигуры.
        /// </summary>
        public GraphicsPath Path { get; set; }

        /// <summary>
        /// Метод, выполняющий действия над номером опорной точки.
        /// </summary>
        public int ControlPointF { get; set; }

        /// <summary>
        /// Метод, выполняющий действия кистью.
        /// </summary>
        public Pen @Pen { get; set; }

        /// <summary>
        /// Метод, выполняющий создание опорной точки.
        /// </summary>
        /// <para name = "Pen">Переменная, хранящая кисть.</para>
        /// <para name = "Path">Переменная, хранящая графическое представление фигуры.</para>
        public SupportObjectFugure(Pen Pen, GraphicsPath Path)
        {
            this.Path = Path;
            this.Pen = Pen;
        }
    }
}

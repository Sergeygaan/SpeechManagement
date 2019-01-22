using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyPaint
{
    /// <summary>
    /// Класс, являющийся основным объектом построения
    /// </summary>
    public class ObjectFugure
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
        /// Метод, выполняющий возврат опорных точек.
        /// </summary>
        public List<SupportObjectFugure> SelectListFigure()
        {
            return _supportFigures;
        }

        /// <summary>
        /// Метод, выполняющий действия над точками в фигуре.
        /// </summary>
        public PointF[] PointSelect { get; set; }

        /// <summary>
        /// Метод, выполняющий действия над выбранной фигурой.
        /// </summary>
        public bool SelectFigure { get; set; } = false;

        /// <summary>
        /// Метод, выполняющий действия начальной координатой.
        /// </summary>
        public PointF FigureStart { get; set; } = new Point();

        /// <summary>
        /// Метод, выполняющий действия конечной координатой.
        /// </summary>
        public PointF FigureEnd { get; set; } = new Point();

        /// <summary>
        /// Переменная, хранящая список опорных точек.
        /// </summary>
        private List<SupportObjectFugure> _supportFigures = new List<SupportObjectFugure>();

        /// <summary>
        /// Метод, выполняющий действия кистью.
        /// </summary>
        public Pen @Pen { get; set; }

        /// <summary>
        /// Метод, выполняющий создание объекта.
        /// </summary>
        /// <para name = "Pen">Переменная, хранящая кисть.</para>
        /// <para name = "Path">Переменная, хранящая графическое представление фигуры.</para>
        /// <para name = "Brush">Переменная, хранящая заливки.</para>
        /// <para name = "CurrentFigure">Переменная, хранящая тип фигуры.</para>
        /// <para name = "Fill">Переменная, хранящая параметны заливки.</para>
        public ObjectFugure(Color linecolor, int thickness, DashStyle dashStyle, GraphicsPath path)
        {
            Path = path;

            Pen = new Pen(linecolor, thickness)
            {
                DashStyle = dashStyle
            };
        }

        public void DrawText()
        {
            if (Path.PointCount != 0)
            {
                RectangleF rectangle = СonstructionFigure.ShowRectangleFloat(Path.PathPoints[0], Path.PathPoints[2]);

                int fontSize;

                if ((int)rectangle.Height / 2 < (int)rectangle.Width / 2)
                {
                    fontSize = (int)rectangle.Height / 2;
                }
                else
                {
                    fontSize = (int)rectangle.Width / 2;
                }

                int x = (int)(rectangle.Left + rectangle.Width / 2 - fontSize * 0.5);
                int y = (int)(rectangle.Top + rectangle.Height / 2 - fontSize * 0.5);

                string stringText = IdFigure.ToString();
                FontFamily family = new FontFamily("Arial");
                int fontStyle = (int)FontStyle.Regular;
                int emSize = fontSize;
                Point origin = new Point(x, y);
                StringFormat format = StringFormat.GenericDefault;

                if (emSize > 0)
                {
                    Path.AddString(stringText,
                        family,
                        fontStyle,
                        emSize,
                        origin,
                        format);
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий добавление опорных точек в список.
        /// </summary>
        public void AddListFigure(SupportObjectFugure AddFigure)
        {
            _supportFigures.Add(AddFigure);
        }

        /// <summary>
        /// Метод, выполняющий перерисовку опорных точек.
        /// </summary>
        public void EditListFigure(int index, RectangleF Rectangles)
        {
            _supportFigures[index].Path.Reset();

            _supportFigures[index].Path.AddEllipse(Rectangles);
        }

        /// <summary>
        /// Метод, выполняющий удаление опорных точек.
        /// </summary>
        public void ClearListFigure()
        {
            _supportFigures.Clear();
        }
    }
}

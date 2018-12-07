using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint
{
    public class Drawing
    {
        /// <summary>
        /// Переменная, хранящая хранящая список со всеми фигурами.
        /// </summary>
        private List<ObjectFugure> _figures;

        /// <summary>
        /// Переменная, хранящая параметры кисти для отрисовки фигур.
        /// </summary>
        private Pen _penFigure;

        /// <summary>
        /// Переменная, хранящая зону отрисовки фигур.
        /// </summary>
        private Bitmap _bmp;

        /// <summary>
        /// Переменная, хранящая ширину зоны отрисовки.
        /// </summary>
        private int _widthDraw;

        /// <summary>
        /// Переменная, хранящая высоту зоны отрисовки.
        /// </summary>
        private int _heightDraw;

        /// <summary>
        /// Переменная, хранящая прямоугольник для выделения фигур.
        /// </summary>
        private Rectangle _rect;

        /// <summary>
        /// Переменная, хранящая значения о текущей выбранной фигуры.
        /// </summary>
        private int _currentfigure;

        /// <summary>
        /// Переменная, хранящая значения о сохранение проекта.
        /// </summary>
        private bool _saveProjectClear = false;

        /// <summary>
        /// Переменная, хранящая объекс для отрисовки фигур.
        /// </summary>
        private ObjectFugure _drawObject;

        /// <summary>
        /// Переменная, хранящая значения о заливки фигур.
        /// </summary>
        private bool _brushFill;

        /// <summary>
        /// Переменная, хранящая зону выделения.
        /// </summary>
        private RectangleF _rectangleF;

        /// <summary>
        /// Переменная, хранящая список с выделенными фигурами.
        /// </summary>

        public Drawing(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;
            _bmp = new Bitmap(Width, Height);
            _figures = new List<ObjectFugure>();
        }

        public void Paint(PaintEventArgs e, int Currentfigure)
        {

            e.Graphics.DrawImage(_bmp, 0, 0);

        }

        public void MouseMove(List<PointF> _points, MouseEventArgs e)
        {
            Rectangles.MouseMove(_points, e);
        }

        public void PaintFigure(PaintEventArgs e, List<PointF> Points)
        {
            Rectangles.PaintFigure(e, Points);
        }

        /// <summary>
        /// Метод, выполняющий сохранение фигур.
        /// </summary>
        /// <para name = "Currentfigure">Переменная, хранящая  текущую выбранную фигуру</para>
        /// <para name = "Points">Переменная, хранящая  координаты отрисовки фигуры</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки</para>
        //public void MouseUp(List<PointF> points, MouseEventArgs e, Color linecolor, int thickness, DashStyle dashstyle, Color brushcolor)
        public void MouseUp(List<PointF> point)
        {
            Color linecolor = Color.Red;
            Color brushcolor = Color.Red;

            DashStyle dashStyle = DashStyle.Solid;

            int thickness = 2;

            _drawObject = new ObjectFugure(linecolor, thickness, dashStyle, new GraphicsPath(), brushcolor, _currentfigure, _brushFill)
            {
                FigureStart = point[0],
                FigureEnd = point[1],
            };

            _drawObject.Path.AddRectangle(СonstructionFigure.ShowRectangleFloat(point[0], point[1]));

            _drawObject.nameRec();

            _figures.Add(_drawObject);

            RefreshBitmap();
        }

        public void Clear()
        {
            _figures.Clear();

            RefreshBitmap();

            GC.Collect();

        }

        /// <summary>
        /// Метод, выполняющий отрисовку всех фигур на рабочей области.
        /// </summary>
        public void RefreshBitmap()
        {
            if (_bmp != null) _bmp.Dispose();

            _bmp = new Bitmap(_widthDraw, _heightDraw);

            //Прорисовка всех объектов из списка

            using (Graphics DrawList = Graphics.FromImage(_bmp))
            {
                if (_saveProjectClear == true)
                {
                    DrawList.Clear(Color.White);
                    _saveProjectClear = false;
                }

                foreach (ObjectFugure DrawObject in _figures)
                {
                    DrawList.DrawPath(DrawObject.Pen, DrawObject.Path);

                    //if (DrawObject.Fill == true)
                    //{
                    //    DrawList.FillPath(DrawObject.Brush, DrawObject.Path);  //Заливка
                    //}

                    foreach (SupportObjectFugure SuppportObject in DrawObject.SelectListFigure())
                    {
                        DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);
                    }


                }
            }
        }

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "e">Переменная, хранящая  события отрисовки.</para>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки.</para>
        public void SupportPoint(List<ObjectFugure> SeleckResult)
        {
            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                if (SelectObject.SelectFigure == true)
                {
                    SelectObject.SelectFigure = false;
                    SelectObject.ClearListFigure();

                    Color ColorLine = Color.Black;

                    Rectangles.AddSupportPoint(SelectObject, ColorLine);
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий редактирование стилей для каждой фигуры.
        /// </summary>
        public void StyleFigure(Color linecolor, int thickness, DashStyle dashstyle)
        {
            _penFigure = new Pen(linecolor, thickness);
            _penFigure.DashStyle = dashstyle;

        }

        /// <summary>
        /// Метод, возвращающий зону выделения.
        /// </summary>
        public Rectangle SeparationZone()
        {
            return _rect;
        }

        /// <summary>
        /// Метод, возвращающий зону выделения.
        /// </summary>
        public Bitmap BitmapReturn()
        {
            return _bmp;
        }

        /// <summary>
        /// Метод, возвращяющий список со всеми фигурами.
        /// </summary>
        public List<ObjectFugure> FiguresList
        {
            get { return _figures; }
            set { _figures = value; }
        }
    }
}

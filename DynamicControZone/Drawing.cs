using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint
{
    public class Drawing : IDisposable
    {
        /// <summary>
        /// Метод, возвращяющий список со всеми фигурами.
        /// </summary>
        public List<ObjectFugure> FiguresList { get; set; }

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
        /// Переменная, хранящая значения о сохранение проекта.
        /// </summary>
        private bool _saveProjectClear = false;

        /// <summary>
        /// Переменная, хранящая список с выделенными фигурами.
        /// </summary>
        public Drawing(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;
            _bmp = new Bitmap(Width, Height);
            FiguresList = new List<ObjectFugure>();
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

            int thickness = 5;

            var _drawObject = new ObjectFugure(linecolor, thickness, dashStyle, new GraphicsPath())
            {
                FigureStart = point[0],
                FigureEnd = point[1],
                IdFigure = FiguresList.Count + 1
            };

            _drawObject.Path.AddRectangle(СonstructionFigure.ShowRectangleFloat(point[0], point[1]));

            _drawObject.DrawText();

            FiguresList.Add(_drawObject);

            RefreshBitmap();
        }

        public void Clear()
        {
            FiguresList.Clear();

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

            using (var DrawList = Graphics.FromImage(_bmp))
            {
                if (_saveProjectClear == true)
                {
                    DrawList.Clear(Color.White);
                    _saveProjectClear = false;
                }

                foreach (ObjectFugure DrawObject in FiguresList)
                {
                    DrawList.DrawPath(DrawObject.Pen, DrawObject.Path);

                    foreach (SupportObjectFugure SuppportObject in DrawObject.SelectListFigure())
                    {
                        DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);
                    }
                }
            }
        }

        /// <summary>
        /// Удаление фигуры по индексу
        /// </summary>
        /// <param name="index"></param>
        public void DeleteFigure(int index)
        {
            FiguresList.RemoveAt(index - 1);
            IDRecalculation();

            RefreshBitmap();

            GC.Collect();
        }

        /// <summary>
        /// Пересчитывает ID фигур в списке
        /// </summary>
        private void IDRecalculation()
        {
            int index = 1;

            foreach(var currentFigures in FiguresList)
            {
                currentFigures.IdFigure = index;

                currentFigures.PointSelect = currentFigures.Path.PathPoints;
                currentFigures.Path.Reset();
                currentFigures.Path.AddRectangle(СonstructionFigure.ShowRectangleFloat(currentFigures.PointSelect[0], currentFigures.PointSelect[2]));
                currentFigures.DrawText();

                index++;
            }
        }


        /// <summary>
        /// Метод, возвращающий зону выделения.
        /// </summary>
        public Bitmap BitmapReturn()
        {
            return _bmp;
        }

        public int ReturnCountFigures()
        {
            return FiguresList.Count;
        }

        public void Dispose()
        {
            _bmp.Dispose();
            FiguresList.Clear();
        }
    }
}

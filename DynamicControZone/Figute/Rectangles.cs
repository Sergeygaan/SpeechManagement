using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполнящий различные действия над прямоугольником.
    /// </summary>
    public static class Rectangles
    {
        /// <summary>
        /// Метод, выполняющий действие при перемещении мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        public static List<PointF> MouseMove(List<PointF> _points, MouseEventArgs e)
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points[1] = new PointF(e.Location.X, e.Location.Y);
            }

            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии отпукании мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public static List<PointF> MouseUp(List<PointF> _points, MouseEventArgs e, int Currentfigure)
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points[1] = new PointF(e.Location.X, e.Location.Y);

            }
            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public static void MouseDown(List<PointF> _points, MouseEventArgs e, int Currentfigure)
        {
            _points.Add(new PointF(e.Location.X, e.Location.Y));
            _points.Add(new PointF(e.Location.X, e.Location.Y));
        }

        /// <summary>
        /// Метод, выполняющий отрисовку прямоугольника при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение эллипса</para>
        public static void PaintFigure(PaintEventArgs e, List<PointF> Points)
        {
            Pen pen = new Pen(Color.Red, 1);

            e.Graphics.DrawRectangle(pen, СonstructionFigure.ShowRectangleInt(Points[0], Points[1]));
        }

        /// <summary>
        /// Метод, выполняющий сохранение прямоугольника.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        public static void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures)
        {
            //_addFigureRectangle = new AddRectangle();
            //_addFigureRectangle.AddFigure(DrawObject, Points, Figures);

            //Figures.Add(_addFigureRectangle.Output());
            //FiguresBuild.Add(_addFigureRectangle);
        }

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно построить опорные точки</para>
        public static void AddSupportPoint(ObjectFugure SelectObject, Color ColorLine)
        {
            for (int i = 0; i < 4; i++)
            {
                SupportObjectFugure supportObjectFugure = new SupportObjectFugure(new Pen(ColorLine, 1), new GraphicsPath());
                supportObjectFugure.Path.AddEllipse(СonstructionFigure.SelectFigure(SelectObject.Path.PathPoints[i], SelectObject.Pen.Width));
                supportObjectFugure.IdFigure = SelectObject.IdFigure;
                supportObjectFugure.ControlPointF = i;

                SelectObject.AddListFigure(supportObjectFugure);
            }
        }

        /// <summary>
        /// Метод, отвечающий за перемещение и масштабирование фигур.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно выполнять действия</para>
        /// <para name = "SupportObj">Переменная хранащая опорные точки выбранного объекта</para>
        /// <para name = "DeltaX">Переменная хранащая разницу по координате X</para>
        /// <para name = "DeltaY">Переменная хранащая разницу по координате Y</para>
        /// /// <para name = "EdipParametr">Объекта класса необходимый для выполнения масштабирования</para>
        public static void ScaleSelectFigure(ObjectFugure SelectObject, SupportObjectFugure SupportObj, int DeltaX, int DeltaY)
        {
            if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[2].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[2].Y != 0))
            {
                SelectObject.PointSelect = SelectObject.Path.PathPoints;
            }

            if (SelectObject.IdFigure == SupportObj.IdFigure)
            {
                switch (SupportObj.ControlPointF)
                {
                    case 0:

                        if (SelectObject.PointSelect[0].X + DeltaX < SelectObject.PointSelect[1].X)
                        {
                            SelectObject.PointSelect[0].X += DeltaX;
                        }

                        if (SelectObject.PointSelect[0].Y + DeltaY < SelectObject.PointSelect[3].Y)
                        {
                            SelectObject.PointSelect[0].Y += DeltaY;
                        }

                        break;

                    case 1:

                        if (SelectObject.PointSelect[2].X + DeltaX > SelectObject.PointSelect[0].X)
                        {
                            SelectObject.PointSelect[2].X += DeltaX;

                        }

                        if (SelectObject.PointSelect[0].Y + DeltaY < SelectObject.PointSelect[2].Y)
                        {
                            SelectObject.PointSelect[0].Y += DeltaY;
                        }

                        break;

                    case 2:

                        if (SelectObject.PointSelect[2].X + DeltaX > SelectObject.PointSelect[3].X)
                        {
                            SelectObject.PointSelect[2].X += DeltaX;
                        }

                        if (SelectObject.PointSelect[2].Y + DeltaY > SelectObject.PointSelect[1].Y)
                        {
                            SelectObject.PointSelect[2].Y += DeltaY;
                        }

                        break;

                    case 3:

                        if (SelectObject.PointSelect[0].X + DeltaX < SelectObject.PointSelect[2].X)
                        {
                            SelectObject.PointSelect[0].X += DeltaX;
                        }

                        if (SelectObject.PointSelect[2].Y + DeltaY > SelectObject.PointSelect[0].Y)
                        {
                            SelectObject.PointSelect[2].Y += DeltaY;
                        }

                        break;
                }

                SelectObject.Path.Reset();
                SelectObject.Path.AddRectangle(СonstructionFigure.ShowRectangleFloat(SelectObject.PointSelect[0], SelectObject.PointSelect[2]));

                SelectObject.DrawText();
            }

            EditObject.MoveObjectSupport(SelectObject, DeltaX, DeltaY);
        }

        /// <summary>
        /// Метод, выполняющий выделение фигуры
        /// </summary>
        /// <para name = "e">Переменная хранащая значение координат курсора мыши</para>
        /// <para name = "DrawObject">Переменная хранащая объект выделения</para>
        /// <para name = "SelectedFigures">Список выделенных объектов</para>
        public static void ScaleFigure(MouseEventArgs e, ObjectFugure DrawObject, List<ObjectFugure> SelectedFigures)
        {
            DrawObject.PointSelect = DrawObject.Path.PathPoints;
            DrawObject.SelectFigure = true;
            //DrawObject.Pen.Width += 1;
            SelectedFigures.Add(DrawObject);
        }
    }
}

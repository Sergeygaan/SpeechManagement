using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий выделение фигур.
    /// </summary>
    public class SelectDraw
    {
        /// <summary>
        /// Переменная, хранящая список с выделенными фигурами.
        /// </summary>
        private List<ObjectFugure> _selectedFigures = new List<ObjectFugure>();

        /// <summary>
        /// Переменная, хранящая выделенную фигуру.
        /// </summary>
        private SupportObjectFugure _supportObj;

        /// <summary>
        /// Переменная, хранящая тукущие координаты мыщи.
        /// </summary>
        private Point _oldPoint;

        /// <summary>
        /// Переменная, хранящая зону выделения.
        /// </summary>
        private RectangleF _rectangleF;

        /// <summary>
        /// Метод, выполняющий отмену выделения.
        /// </summary>
        public void MouseUp()
        {
            foreach (ObjectFugure SelectObject in _selectedFigures)
            {
                if (SelectObject != null)
                {
                    //SelectObject.Pen.Width -= 1;//Возвращаем ширину пера
                    SelectObject.ClearListFigure();
                    SelectObject.PointSelect = null;
                    SelectObject.SelectFigure = false;
                    //SelectObject. = null;//Убираем ссылку на объект
                    _supportObj = null;

                }
            }
            _selectedFigures.Clear();
        }

        public void MouseUpSupport()
        {
            if (_supportObj != null)
            {
                //_supportObj.Pen.Width -= 5;
                _supportObj = null;

            }
        }

        public void SavePoint(MouseEventArgs e)
        {
            _oldPoint = e.Location;

            if (_selectedFigures.Count != 0)
            {
                foreach (ObjectFugure selectObject in _selectedFigures)
                {
                    foreach (SupportObjectFugure supportObjecFigure in selectObject.SelectListFigure())
                    {

                        _rectangleF = supportObjecFigure.Path.GetBounds();

                        if (_rectangleF.Contains(e.Location))
                        {
                            _supportObj = supportObjecFigure;
                            // MessageBox.Show(_supportObj.ControlPointF.ToString());

                        }

                    }
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий выделение фигур.
        /// </summary>
        /// <para name = "e">Переменная, хранящая координаты мыши.</para>
        /// <para name = "Rect">Переменная, хранящая зону выделения.</para>
        /// <para name = "Figures">Переменная, хранящая список всех фигур.</para>
        /// <para name = "CurrentActions">Переменная, хранящая действие над выбранной фигурой.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая список действий.</para>
        public void MouseDown(MouseEventArgs e, List<ObjectFugure> Figures, int CurrentActions)
        {
            ////Запоминаем положение курсора
            _oldPoint = e.Location;

            //rectangles.ScaleFigure(e, Figures[CurrentActions], _selectedFigures);

            float figurestartX, figurestartY, figureendX, figureendY;

            if (_selectedFigures.Count == 0)
            {

                foreach (ObjectFugure DrawObject in Figures)
                {

                    figurestartX = DrawObject.FigureStart.X;
                    figurestartY = DrawObject.FigureStart.Y;

                    figureendX = DrawObject.FigureEnd.X;
                    figureendY = DrawObject.FigureEnd.Y;

                    // Получение области выделения
                    _rectangleF = DrawObject.Path.GetBounds();

                    if (figurestartX == figureendX)
                    {
                        _rectangleF.Inflate(10, 5);
                    }

                    if (figurestartY == figureendY)
                    {
                        _rectangleF.Inflate(5, 10);
                    }

                    switch (1)
                    {

                        case 1:

                            if (_rectangleF.Contains(e.Location))
                            {
                                Rectangles.ScaleFigure(e, DrawObject, _selectedFigures);
                                Rectangles.AddSupportPoint(DrawObject, Color.Green);
                            }

                            break;
                    }

                }
            }

        }

        /// <summary>
        /// Метод, выполняющий действия над выделенными фигурами.
        /// </summary>
        /// <para name = "e">Переменная, хранящая координаты мыши.</para>
        /// <para name = "CurrentActions">Переменная, хранящая действие над выбранной фигурой.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая список действий.</para>
        public void MouseMove(MouseEventArgs e, int CurrentActions)
        {
            //Считаем смещение курсора
            int deltaX, deltaY;

            deltaX = e.Location.X - _oldPoint.X;
            deltaY = e.Location.Y - _oldPoint.Y;

            foreach (ObjectFugure SelectObject in _selectedFigures)
            {
                //Масштабирование опорных точек
                if ((SelectObject != null) && (_supportObj != null))
                {
                    Rectangles.ScaleSelectFigure(SelectObject, _supportObj, deltaX, deltaY);

                }
                else
                {
                    if (SelectObject != null)
                    {
                        SelectObject.PointSelect = SelectObject.Path.PathPoints;

                        EditObject.MoveObject(SelectObject, deltaX, deltaY);
                    }
                }

                _oldPoint = e.Location;
            }

        }

        /// <summary>
        /// Метод, возвращающий список выделенных фигур.
        /// </summary>
        public List<ObjectFugure> SeleckResult()
        {
            return _selectedFigures;
        }
    }
}

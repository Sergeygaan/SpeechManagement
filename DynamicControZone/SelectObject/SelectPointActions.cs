using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполнящий выделение объектов точкой
    /// </summary>
    public class SelectPointActions : IDisposable
    {
        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();

        ///// <summary>
        ///// Переменная, хранящая класс для отрисовки и сохранения фигур.
        ///// </summary>
        //private DrawPaint _drawClass;

        /// <summary>
        /// Переменная, хранящая класс для выделения.
        /// </summary>
        private SelectDraw _selectClass;

        /// <summary>
        ///// Переменная, хранящая список классов для построения различных фигур.
        ///// </summary>
        //private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();

        ///// <summary>
        ///// Переменная, хранящая класс для выполнения различных действий
        ///// </summary>
        //private ParameterChanges _parameterChangesClass;

        //private СhangeMove _penMove;


        public SelectPointActions()
        {
            _selectClass = new SelectDraw();
        }

        /// <summary>
        /// Метод, выполняющий действие при перемещении мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public List<PointF> MouseMove(MouseEventArgs e, int Currentfigure, int CurrentActions)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_selectClass.SeleckResult().Count != 0)
                {
                    _selectClass.MouseMove(e, CurrentActions);
                }
            }

            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при отпускании мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public void MouseUp(MouseEventArgs e, List<ObjectFugure> FiguresList, int Currentfigure)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseDown(e, FiguresList, Currentfigure);
                }
                else
                {
                    _selectClass.MouseUpSupport();
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseUp();
                }
                else
                {
                    _selectClass.MouseUp();
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public void MouseDown(MouseEventArgs e, List<ObjectFugure> Figures, int Currentfigure)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseUp();
                    _selectClass.MouseDown(e, Figures, Currentfigure);
                }
                else
                {
                    _selectClass.SavePoint(e);
                }
            }
        }

        public void Dispose()
        {
            _points = null;
            _selectClass.Dispose();
        }
    }
}

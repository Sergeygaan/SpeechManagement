using System.Drawing.Drawing2D;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий изменение положения выделенных фигур.
    /// </summary>
    public static class EditObject
    {
        /// <summary>
        /// Метод, выполняющий перемещение объекта.
        /// </summary>
        /// <para name = "CurrObj">Переменная, хранящая объект перемещения.</para>
        /// <para name = "DeltaX">Переменная, хранящая значение дельта X.</para>
        /// <para name = "DeltaY">Переменная, хранящая значение дельта Y.</para>
        public static void MoveObject(ObjectFugure CurrObj, int DeltaX, int DeltaY)
        {
            CurrObj.Path.Transform(new Matrix(1, 0, 0, 1, DeltaX, DeltaY));

            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }

        /// <summary>
        /// Метод, выполняющий перемещение опорных точек.
        /// </summary>
        /// <para name = "CurrObj">Переменная, хранящая объект перемещения.</para>
        /// <para name = "DeltaX">Переменная, хранящая значение дельта X.</para>
        /// <para name = "DeltaY">Переменная, хранящая значение дельта Y.</para>
        public static void MoveObjectSupport(ObjectFugure CurrObj, int DeltaX, int DeltaY)
        {
            for (int i = 0; i < 4; i++)
            {
                CurrObj.EditListFigure(i, СonstructionFigure.SelectFigure(CurrObj.PointSelect[i], CurrObj.Pen.Width));
            }
        }
    }
}

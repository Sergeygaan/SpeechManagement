using Command.Magnif;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProjectSettings;

namespace WorkingScreen
{
    public class WorkObject
    {
        /// <summary>
        /// Дочерний класс
        /// </summary>
        public WorkObject ChildNumberObject;

        /// <summary>
        /// Родительский класс
        /// </summary>
        public WorkObject ParantNumberObject;

        /// <summary>
        /// Номер поколения
        /// </summary>
        public int GenerationNumber;

        /// <summary>
        /// Экранная лупа
        /// </summary>
        public Magnifier Magnifier = null;

        /// <summary>
        /// Видимость области
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Зоны для данный области
        /// </summary>
        public List<RegionRectangle> listRegionRectangle = new List<RegionRectangle>();

        public WorkObject()
        {
            Visible = true;
        }

        /// <summary>
        /// Отрисовка области
        /// </summary>
        /// <param name="e"></param>
        public void DrawingRectangle(PaintEventArgs e)
        {
            foreach (var currentRectangle in listRegionRectangle)
            {
                e.Graphics.DrawRectangle(new Pen(ProjectSettingsMain.ColorSquares), currentRectangle.Rectangle.X, currentRectangle.Rectangle.Y,
                                                    currentRectangle.Rectangle.Width, currentRectangle.Rectangle.Height);
            }
        }

        /// <summary>
        /// Добавление текста на область с номером данной области
        /// </summary>
        /// <param name="e"></param>
        public void DrawingText(PaintEventArgs e)
        {
            foreach (var currentRectangle in listRegionRectangle)
            {
                if (Visible)
                {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    float fontSize;

                    if ((int)currentRectangle.Rectangle.Height / 2 < (int)currentRectangle.Rectangle.Width / 2)
                    {
                        fontSize = (int)currentRectangle.Rectangle.Height / 2;
                    }
                    else
                    {
                        fontSize = (int)currentRectangle.Rectangle.Width / 2;
                    }

                    int x = (int)(currentRectangle.Rectangle.Left + currentRectangle.Rectangle.Width / 2 - fontSize * 0.6);
                    int y = (int)(currentRectangle.Rectangle.Top + currentRectangle.Rectangle.Height / 2 - fontSize * 0.6);

                    var stringIdObject = currentRectangle.IdObject.ToString();

                    e.Graphics.DrawString(stringIdObject, new Font("Arial", fontSize),
                        new SolidBrush(ProjectSettingsMain.ColorNumbers), new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Нахождение центра области
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Center(int index)
        {
            return new PointF(listRegionRectangle[index].Rectangle.Left + listRegionRectangle[index].Width / 2,
                            listRegionRectangle[index].Rectangle.Top + listRegionRectangle[index].Height / 2);
        }
    }
}

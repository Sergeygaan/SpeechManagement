using Command.Magnif;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProjectSettings;

namespace WorkingScreen
{
    public class WorkObject
    {
        public WorkObject ChildNumberObject;
        public WorkObject ParantNumberObject;

        public int GenerationNumber;

        public Magnifier Magnifier = null;

        public bool Visible { set; get; }

        public List<RegionRectangle> listRegionRectangle = new List<RegionRectangle>();

        public WorkObject()
        {
            Visible = true;
        }

        public void DrawingRectangle(PaintEventArgs e)
        {
            foreach (var currentRectangle in listRegionRectangle)
            {
                e.Graphics.DrawRectangle(new Pen(ProjectSettingsMain.ColorSquares), currentRectangle.Rectangle.X, currentRectangle.Rectangle.Y,
                                                    currentRectangle.Rectangle.Width, currentRectangle.Rectangle.Height);
            }
        }

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

                    //Font font = new Font("Arial", fontSize);

                    int x = (int)(currentRectangle.Rectangle.Left + currentRectangle.Rectangle.Width / 2 - fontSize * 0.6);
                    int y = (int)(currentRectangle.Rectangle.Top + currentRectangle.Rectangle.Height / 2 - fontSize * 0.6);

                    //var Center = new Point(x, y);

                    var stringIdObject = currentRectangle.IdObject.ToString();
                    e.Graphics.DrawString(stringIdObject, new Font("Arial", fontSize),
                        new SolidBrush(ProjectSettingsMain.ColorNumbers), new Point(x, y));
                }
            }
        }

        public PointF Center(int index)
        {
                return new PointF(listRegionRectangle[index].Rectangle.Left + listRegionRectangle[index].Width / 2,
                             listRegionRectangle[index].Rectangle.Top + listRegionRectangle[index].Height / 2);
        }
    }
}

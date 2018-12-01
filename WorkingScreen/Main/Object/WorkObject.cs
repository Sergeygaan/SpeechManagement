using Command.Magnif;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectSettings
{
    public class WorkObject
    {
        public class RegionRectangle
        {
            public RectangleF Rectangle { set; get; }

            public int IdObject { set; get; }

            public bool Visible { set; get; }

            public float StartX { set; get; }
            public float StartY { set; get; }

            public float Width { set; get; }
            public float Height { set; get; }
        }

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
                e.Graphics.DrawRectangle(new Pen(ProjectSettings.ColorSquares), currentRectangle.Rectangle.X, currentRectangle.Rectangle.Y,
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

                    float fontSize = currentRectangle.Rectangle.Height / 2;

                    Font font = new Font("Microsoft Sans Serif", fontSize);

                    int x = (int)(currentRectangle.Rectangle.Left + currentRectangle.Rectangle.Width / 2 - fontSize * 0.7);
                    int y = (int)(currentRectangle.Rectangle.Top + currentRectangle.Rectangle.Height / 2 - fontSize * 0.7);

                    var Center = new Point(x, y);

                    var stringIdObject = currentRectangle.IdObject.ToString();
                    e.Graphics.DrawString(stringIdObject, font, new SolidBrush(ProjectSettings.ColorNumbers), Center);
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

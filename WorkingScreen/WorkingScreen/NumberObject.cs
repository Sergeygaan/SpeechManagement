using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoiceControl
{
    class NumberObject
    {
        public class RegionRectangle
        {
            public Rectangle Rectangle { set; get; }

            public int IdObject { set; get; }

            public bool Visible { set; get; }

            public int StartX { set; get; }
            public int StartY { set; get; }

            public int Width { set; get; }
            public int Height { set; get; }
        }

        public NumberObject ChildNumberObject;
        public NumberObject ParantNumberObject;
        public bool Visible { set; get; }

        public List<RegionRectangle> listRegionRectangle = new List<RegionRectangle>();

        public NumberObject()
        {
            Visible = true;
        }


        public void DrawingRectangle(PaintEventArgs e)
        {
            foreach (var currentRectangle in listRegionRectangle)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Gray), currentRectangle.Rectangle);
            }
        }

        public void DrawingText(PaintEventArgs e)
        {
            foreach (var currentRectangle in listRegionRectangle)
            {
                if (Visible)
                {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    int fontSize = currentRectangle.Rectangle.Height / 2;

                    Font f = new Font("Microsoft Sans Serif", fontSize);

                    int x = (int)(currentRectangle.Rectangle.Left + currentRectangle.Rectangle.Width / 2 - fontSize * 0.7);
                    int y = (int)(currentRectangle.Rectangle.Top + currentRectangle.Rectangle.Height / 2 - fontSize * 0.7);

                    var Center = new Point(x, y);

                    var stringIdObject = currentRectangle.IdObject.ToString();
                    e.Graphics.DrawString(stringIdObject, f, new SolidBrush(Color.Gray), Center);
                }
            }
        }

        public Point Center(int index)
        {
                return new Point(listRegionRectangle[index].Rectangle.Left + listRegionRectangle[index].Width / 2,
                             listRegionRectangle[index].Rectangle.Top + listRegionRectangle[index].Height / 2);
            
        }

    }
}

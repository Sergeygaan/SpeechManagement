using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{
    class NumberObject
    {
        private PointF _startPoint;
        private PointF _endPoint;

        public List<NumberObject> _clild = new List<NumberObject>();

        public int StartX { get; }
        public int StartY { get; }

        private int _endtX;
        private int _endY;

        public int Width { get; }
        public int Height { get; }

        public Rectangle Rectangle { get; }

        public int IdObject { get; }
        
        public bool Active { get; }
        public bool Visible { set; get; }


        public NumberObject(int iDObject, int startX, int startY, int endtX, int endY)
        {
            IdObject = iDObject;

            StartX = startX;
            StartY = startY;

            _endtX = endtX;
            _endY = endY;

            Width = _endtX - StartX;
            Height = _endY - StartY;

            _startPoint = new PointF(StartX, StartY);
            _endPoint = new PointF(_endtX, _endY);

            Rectangle = new Rectangle(StartX, StartY, Width, Height);

            Visible = true;
        }

        public void DrawingRectangle(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), Rectangle);
        }

        public void DrawingText(PaintEventArgs e)
        {
            if (Visible)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                int fontSize = Rectangle.Height / 2;

                Font f = new Font("Microsoft Sans Serif", fontSize);

                int x = (int)(Rectangle.Left + Rectangle.Width / 2 - fontSize * 0.7);
                int y = (int)(Rectangle.Top + Rectangle.Height / 2 - fontSize * 0.7);

                var Center = new Point(x, y);

                var stringIdObject = IdObject.ToString();
                e.Graphics.DrawString(stringIdObject, f, new SolidBrush(Color.Gray), Center);
            }
        }

        public Point Center
        {
            get
            {
                return new Point(Rectangle.Left + Rectangle.Width / 2,
                             Rectangle.Top + Rectangle.Height / 2);
            }
        }

    }
}

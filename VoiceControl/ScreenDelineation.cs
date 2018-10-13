using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{
    public partial class ScreenDelineation : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        //private const int ExStyle = -20;

        //private const int Transparent = 0x20;

        //private const int Layered = 0x80000;

        private int initialStyle;

        private List<NumberObject> _baseNumberObject = new List<NumberObject>();
        private List<int> _indexObject = new List<int>();

        public ScreenDelineation()
        {
            InitializeComponent();
            initialStyle = GetWindowLong(Handle, -20);
            TransparencyKey = BackColor;
            //SetWindowLong(Handle, ExStyle, initialStyle | Layered | Transparent);
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_baseNumberObject.Count == 0)
            {
                DrawingDividingLines(_baseNumberObject, Width, Height);
            }

            DrawingZone(e, _baseNumberObject);
        }

        const int WM_NCHITTEST = 0x0084;
        const int HTTRANSPARENT = -1;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
                return;
            }
            base.WndProc(ref m);
        }

        private void DrawingZone(PaintEventArgs e, List<NumberObject> _numberObject)
        {
            foreach (var currentObject in _numberObject)
            {
                if (currentObject._clild.Count != 0)
                {
                    DrawingZone(e, currentObject._clild);
                }

                currentObject.DrawingRectangle(e);

                currentObject.DrawingText(e);
            }
        }

        //float startX, float startY, float endX, float endY, 
        private void DrawingDividingLines(List<NumberObject> _numberObject, int width, int height, int startX = 0, int startY = 0)
        {
            int saveStartX = startX;

            var stepWidth = width / 3;
            var stepHeight = height / 3;

            int endX = stepWidth + startX;
            int endY = stepHeight + startY;

            int id = 0;

            for (int stepX = 0; stepX < 3; stepX++)
            {
                for (int stepY = 0; stepY < 3; stepY++)
                {
                    id += 1;

                    _numberObject.Add(new NumberObject(id, startX, startY, endX, endY));

                    startX += stepWidth;
                    endX += stepWidth;
                }

                startX = saveStartX;
                endX = stepWidth + saveStartX;

                startY += stepHeight;
                endY += stepHeight;
            }
        }

        public void LeftOneClick(int number)
        {
            int index = number - 1;

            var currentObject = ScaleNumberObject();

            Cursor.Position = currentObject[index].Center;

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public void LeftDoubleClick(int number)
        {
            int index = number - 1;

            var currentObject = ScaleNumberObject();

            Cursor.Position = currentObject[index].Center;

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public void RightOneClick(int number)
        {
            int index = number - 1;

            var currentObject = ScaleNumberObject();

            Cursor.Position = currentObject[index].Center;

            mouse_event((uint)MouseEventFlags.RIGHTDOWN | (uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
        }

        public void ScaleNumber(int number)
        {
            // MessageBox.Show("scale" + " " + number.ToString());
            int index = number - 1;

            _indexObject.Add(index);
            var currentJbjeck = ScaleNumberObject();

            currentJbjeck[index]._clild.Clear();
            currentJbjeck[index].Visible = false;

            DrawingDividingLines(currentJbjeck[index]._clild, currentJbjeck[index].Width, currentJbjeck[index].Height,
                                 currentJbjeck[index].StartX, currentJbjeck[index].StartY);


            Invalidate();

        }

        public void EndNumber()
        {
            EndNumberObject();
         
            Invalidate();
        }

        private List<NumberObject> ScaleNumberObject()
        {
           var clildObject = _baseNumberObject;

            foreach (var currentIndex in _indexObject)
            {
                if (clildObject[currentIndex]._clild.Count != 0)
                {
                    clildObject = clildObject[currentIndex]._clild;
                }
            }

            return clildObject;
        }

        private void EndNumberObject()
        {
            var clildObject = _baseNumberObject;
            NumberObject currentNumberObject = null;

            foreach (var currentIndex in _indexObject)
            {
                currentNumberObject = clildObject[currentIndex];

                if (clildObject[currentIndex]._clild.Count != 0)
                {
                    clildObject = clildObject[currentIndex]._clild;
                }
            }
            if (currentNumberObject != null)
            {
                currentNumberObject.Visible = true;
            }

            clildObject.Clear();

            if (_indexObject.Count != 0)
            {
                _indexObject.Remove(_indexObject.Last());
            }
        }
    }
}

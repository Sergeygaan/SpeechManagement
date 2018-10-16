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
using static VoiceControl.NumberObject;

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

        private NumberObject _numberObject;

        private int initialStyle;

        public ScreenDelineation()
        {
            InitializeComponent();
  
            TransparencyKey = BackColor;
            //SetWindowLong(Handle, ExStyle, initialStyle | Layered | Transparent);
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            TopLevel = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            //Создание первого поколения
            _numberObject = new NumberObject();
            _numberObject.GenerationNumber = 1;

            //OnTopControl voiceControl = new OnTopControl(Handle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_numberObject.listRegionRectangle.Count == 0)
            {
                DrawingDividingLines(_numberObject, Width, Height);
            }

            DrawingZone(e, _numberObject);

            //if (!flag)
            //{
            //    flag = true;
            //    ScaleNumber(2);

            //    IncreaseMagnifier(5);

            //    //EndNumber();
            //}
        }

        bool flag = false;

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

        private void DrawingZone(PaintEventArgs e, NumberObject _currentObject)
        {

            if (_currentObject.ChildNumberObject != null)
            {
                DrawingZone(e, _currentObject.ChildNumberObject);
            }

            _currentObject.DrawingRectangle(e);

            _currentObject.DrawingText(e);
        }

        //float startX, float startY, float endX, float endY, 
        private void DrawingDividingLines(NumberObject _currentObject, int width, int height, int startX = 0, int startY = 0)
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

                    _currentObject.listRegionRectangle.Add(CreateRegionRectangle(id, startX, startY, endX, endY));

                    startX += stepWidth;
                    endX += stepWidth;
                }

                startX = saveStartX;
                endX = stepWidth + saveStartX;

                startY += stepHeight;
                endY += stepHeight;
            }
        }

        private RegionRectangle CreateRegionRectangle(int iDObject, int startX, int startY, int endtX, int endY)
        {
            var width = endtX - startX;
            var height = endY - startY;

            var Rectangle = new Rectangle(startX, startY, width, height);

            RegionRectangle regionRectangle = new RegionRectangle();

            regionRectangle.Rectangle = new Rectangle(startX, startY, width, height);

            regionRectangle.IdObject = iDObject;

            regionRectangle.Visible = true;

            regionRectangle.StartX = startX;
            regionRectangle.StartY = startY;

            regionRectangle.Width = width;
            regionRectangle.Height = height;

            return regionRectangle;
        }

        public void LeftOneClick(int number)
        {
            int index = number - 1;

            var currentObject = SearchChild(_numberObject);

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public void LeftDoubleClick(int number)
        {
            int index = number - 1;

            var currentObject = SearchChild(_numberObject);

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public void RightOneClick(int number)
        {
            int index = number - 1;

            var currentObject = SearchChild(_numberObject);

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.RIGHTDOWN | (uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
        }

        //Метод для масштабирования
        public void ScaleNumber(int number)
        {
            int index = number - 1;

            var currentNumberObject = SearchChild(_numberObject);

            var newChildNumberObject = new NumberObject
            {
                //Добавление родителя
                ParantNumberObject = currentNumberObject,
                GenerationNumber = currentNumberObject.GenerationNumber + 1
            };

            //Добавление потомка
            currentNumberObject.ChildNumberObject = newChildNumberObject;

            currentNumberObject.Visible = false;

            DrawingDividingLines(newChildNumberObject,
                                 currentNumberObject.listRegionRectangle[index].Width, currentNumberObject.listRegionRectangle[index].Height,
                                 currentNumberObject.listRegionRectangle[index].StartX, currentNumberObject.listRegionRectangle[index].StartY);

            Invalidate();
        }

        MagnifierForm magnifierForm = null;
        Magnifier magnifier = null;

        //Метод для масштабирования с применением лупы
        public void IncreaseMagnifier(int number)
        {
            if (magnifierForm == null)
            {
                int index = number - 1;

                var currentNumberObject = SearchChild(_numberObject);

                magnifierForm = new MagnifierForm();
                magnifierForm.Show();

                magnifier = new Magnifier(magnifierForm, currentNumberObject.listRegionRectangle[index], currentNumberObject.GenerationNumber);
            }
        }

        //Метод отменяет лупу
        public void EndMagnifier()
        {
            if (magnifier != null)
            {
                magnifier.Dispose();
                magnifier = null;
            }

            if (magnifierForm != null)
            {
                magnifierForm.Dispose();
                magnifierForm = null;
            }
        }

        public void EndNumber()
        {
            if(magnifierForm != null)
            {
                EndMagnifier();
            }

            if (SearchChild(_numberObject).ParantNumberObject != null)
            {
                var ParantNumberObject = SearchChild(_numberObject).ParantNumberObject;

                if (ParantNumberObject.ChildNumberObject != null)
                {
                    //currentNumberObject.
                    ParantNumberObject.ChildNumberObject = null;

                    ParantNumberObject.Visible = true;
                }
            }

            Invalidate();
        }

        private NumberObject SearchChild(NumberObject numberObject)
        {
            var local = numberObject;

            while(local.ChildNumberObject != null)
            {
                local = local.ChildNumberObject;
            }

            return local;
        }
    }
}

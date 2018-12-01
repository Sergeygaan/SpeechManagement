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

namespace ProjectSettings
{
    public partial class ScreenDelineation : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private Main _mainObject;

        private int _initialStyle;

        private OnTopControl _onTopControl;

        public ScreenDelineation()
        {
            InitializeComponent();
  
            TransparencyKey = BackColor;
         
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            TopLevel = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            _initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(this.Handle, -20, _initialStyle | 0x80000 | 0x20);

            Size resolution = Screen.PrimaryScreen.Bounds.Size;

            _mainObject = new Main(resolution.Width, resolution.Height);

            _onTopControl = new OnTopControl(Handle);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            _mainObject.OnPaint(e);
        }

        public void ApplyCommand(int indexCommand, int number)
        {
            _mainObject.ApplyCommand(indexCommand, number);

            Invalidate();
        }
    }
}

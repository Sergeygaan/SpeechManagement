using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WorkingScreen
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
            SetWindowLong(Handle, -20, _initialStyle | 0x80000 | 0x20);

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
            Refresh();

            _mainObject.ApplyCommand(indexCommand, number);

            Invalidate();
        }
    }
}

using MyPaint;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WorkingScreen;

namespace VoiceControl
{
    public partial class Settings : Form
    {
        ScreenDelineation _screenDelineation;

        public Settings(ScreenDelineation screenDelineation)
        {
            InitializeComponent();
            a();

            _screenDelineation = screenDelineation;
        }

        private void a()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawingZone drawingZone = new DrawingZone();
            drawingZone.ShowDialog();

            _screenDelineation.Refresh();
        }
    }
}

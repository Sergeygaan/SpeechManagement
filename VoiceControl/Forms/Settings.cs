using MyPaint;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VoiceControl
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            a();
        }

        private void a()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawingZone drawingZone = new DrawingZone();
            drawingZone.Show();
        }
    }
}

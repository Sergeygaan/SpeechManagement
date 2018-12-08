using MyPaint;
using ProjectSettings;
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

            ModeDraw.Checked = ProjectSettingsMain.Zone_DrawMethod;

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

        private void ModeDraw_MouseCaptureChanged(object sender, EventArgs e)
        {
            ProjectSettingsMain.Zone_DrawMethod = ModeDraw.Checked;

            if(ProjectSettingsMain.Zone_DrawMethod)
            {
                ProjectSettingsMain.Zone_TracingChangeDraw = true;
            }

            _screenDelineation.Refresh();
        }
    }
}

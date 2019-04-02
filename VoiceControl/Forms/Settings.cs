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
            
            _screenDelineation = screenDelineation;


            ModeDrawComboBox.SelectedIndex = ProjectSettingsMain.Zone_DrawMethod;
            ActiveCreatingNewAreaBotton(ProjectSettingsMain.Zone_DrawMethod);
        }

        private void ActiveCreatingNewAreaBotton(int mode)
        {
            if(mode == 0)
            {
                CreatingNewArea.Enabled = false;
            }
            else
            {
                CreatingNewArea.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjectSettingsMain.Zone_FlagVoiceControl = false;

            DrawingZone drawingZone = new DrawingZone();
            drawingZone.ShowDialog();

            _screenDelineation.Refresh();

            ProjectSettingsMain.Zone_FlagVoiceControl = true;
        }

        private void ModeDrawComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectSettingsMain.Zone_DrawMethod = ModeDrawComboBox.SelectedIndex;

            ProjectSettingsMain.Zone_TracingChangeDraw = true;

            _screenDelineation.Refresh();

            ActiveCreatingNewAreaBotton(ProjectSettingsMain.Zone_DrawMethod);
        }
    }
}

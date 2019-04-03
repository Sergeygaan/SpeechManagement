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
                CreatingAddRegion.Enabled = false;
                buttonDeleteRegion.Enabled = false;
                buttonEditRegion.Enabled = false;
                buttonSelectRegion.Enabled = false;
            }
            else
            {
                CreatingAddRegion.Enabled = true;
                buttonDeleteRegion.Enabled = true;
                buttonEditRegion.Enabled = true;
                buttonSelectRegion.Enabled = true;
            }
        }

        private void ModeDrawComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectSettingsMain.Zone_DrawMethod = ModeDrawComboBox.SelectedIndex;

            ProjectSettingsMain.Zone_TracingChangeDraw = true;

            _screenDelineation.Refresh();

            ActiveCreatingNewAreaBotton(ProjectSettingsMain.Zone_DrawMethod);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewRegion();
        }

        private void AddNewRegion(int index = -1)
        {
            ProjectSettingsMain.Zone_FlagVoiceControl = false;

            DrawingZone drawingZone = new DrawingZone(index);

            if (drawingZone.ShowDialog() == DialogResult.OK)
            {
                if (index == -1)
                {
                    AddRegionList();
                }
                else
                {
                    EditRegionList(index);
                }
            }

            ProjectSettingsMain.Zone_FlagVoiceControl = true;
        }

        /// <summary>
        /// Метод добавляет новую зону в список всех зон
        /// </summary>
        private void AddRegionList()
        {
            if (ProjectSettingsMain.Zone_GeneralList.Count < 8)
            {
                ProjectSettingsMain.Zone_GeneralList.Add(ProjectSettingsMain.Zone_NewList.GetRange(0, ProjectSettingsMain.Zone_NewList.Count));

                OutputRegionList();
            }
            else
            {
                MessageBox.Show("Альтернативных конфигураций не можект быть больше 8 штук");
            }
        }

        /// <summary>
        /// Метод изменяет созданную зону в список всех зон
        /// </summary>
        private void EditRegionList(int index)
        {
            ProjectSettingsMain.Zone_GeneralList[index] = ProjectSettingsMain.Zone_NewList.GetRange(0, ProjectSettingsMain.Zone_NewList.Count);

            OutputRegionList();
        }

        /// <summary>
        /// Отображение альтернативных зон в listBox
        /// </summary>
        private void OutputRegionList()
        {
            listBox.Items.Clear();

            for (int index = 0; index < ProjectSettingsMain.Zone_GeneralList.Count; index++)
            {
                listBox.Items.Add("Конфигурация " + (index + 1).ToString());
            }
        }

        /// <summary>
        /// Метод удаляет новую зону в список всех зон
        /// </summary>
        private void buttonDeleteRegion_Click(object sender, EventArgs e)
        {
            if(listBox.SelectedIndex != -1)
            {
                ProjectSettingsMain.Zone_GeneralList.RemoveAt(listBox.SelectedIndex);
                listBox.Items.RemoveAt(listBox.SelectedIndex);

                OutputRegionList();
            }
        }

        /// <summary>
        /// Метод применяет новую зону в список всех зон
        /// </summary>
        private void buttonSelectRegion_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                ProjectSettingsMain.Zone_CurrentList.Clear();

                ProjectSettingsMain.Zone_CurrentList = ProjectSettingsMain.Zone_GeneralList[listBox.SelectedIndex].GetRange(0, ProjectSettingsMain.Zone_GeneralList[listBox.SelectedIndex].Count);

                if (ProjectSettingsMain.Zone_CurrentList.Count != 0)
                {
                    ProjectSettingsMain.Zone_TracingChangeDraw = true;
                }

                _screenDelineation.Refresh();
            }
        }

        /// <summary>
        /// Метод открывает на редактирование выбранную зону
        /// </summary>
        private void buttonEditRegion_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            if (index != -1)
            {
                AddNewRegion(index);
            }
        }

        //private int CheckIndexElement()
        //{
        //    listBox.SelectedIndex
        //}
    }
}

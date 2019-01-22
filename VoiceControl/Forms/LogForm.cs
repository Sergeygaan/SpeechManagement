using System;
using System.Drawing;
using System.Windows.Forms;
using WorkingScreen;

namespace VoiceControl
{
    public partial class LogForm : Form
    {
        public delegate void MethodAppendLine(string text, Color color);

        /// <summary>
        /// Класс для распознания речи
        /// </summary>
        private SpeechRecognition speechRecognition;

        ScreenDelineation _screenDelineation;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public LogForm()
        {
            InitializeComponent();

            MethodAppendLine methodAppendLine = new MethodAppendLine(AppendLine);

            _screenDelineation = new ScreenDelineation();

            speechRecognition = new SpeechRecognition(methodAppendLine, _screenDelineation);
        }
        
        /// <summary>
        /// Вывод сообщения в лог
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void AppendLine(string text, Color color)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = text;
            listViewItem.BackColor = color;

            listView.Items.Add(listViewItem);

            if (listView.Items.Count > 2)
            {
                listView.Items[listView.Items.Count - 2].Focused = false;
                listView.Items[listView.Items.Count - 2].Focused = false;

                listView.Items[listView.Items.Count - 1].Focused = true;
                listView.Items[listView.Items.Count - 1].Focused = true;
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }

            if (listView.Items.Count > 100)
            {
                listView.Items.Clear();
            }
        }

        /// <summary>
        /// Отображения формы "Настройки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings formSettings = new Settings(_screenDelineation);
            formSettings.ShowDialog();
        }
    }
}

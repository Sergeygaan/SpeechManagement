using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoiceControl
{
    public partial class LogForm : Form
    {
        public delegate void MethodAppendLine(string text, Color color);

        private SpeechRecognition speechRecognition;

        public LogForm()
        {
            InitializeComponent();

            MethodAppendLine methodAppendLine = new MethodAppendLine(AppendLine);

            speechRecognition = new SpeechRecognition(methodAppendLine);
        }
        
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

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings formSettings = new Settings();
            formSettings.ShowDialog();
        }
    }
}

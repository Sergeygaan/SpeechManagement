using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class DrawingZone : Form
    {
        public DrawingZone()
        {
            InitializeComponent();

            //TransparencyKey = BackColor;

            StartPosition = FormStartPosition.CenterScreen;
           // TopMost = true;
            TopLevel = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            AllowTransparency = true;
            BackColor = Color.AliceBlue;//цвет фона  
            TransparencyKey = this.BackColor;//он же будет заменен на прозрачный цвет

            //textBox1.Multiline = true;
            //textBox1.Dock = DockStyle.Fill;

            // создаем элементы меню
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");
            // добавляем элементы в меню
            rightClickMenuStrip.Items.AddRange(new[] { copyMenuItem, pasteMenuItem });
            // ассоциируем контекстное меню с текстовым полем
            //textBox1.ContextMenuStrip = contextMenuStrip1;
            // устанавливаем обработчики событий для меню
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;

            Size resolution = Screen.PrimaryScreen.Bounds.Size;

            drawing = new Drawing(resolution.Width, resolution.Height);

        }

        // вставка текста
        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("paste");
        }
        // копирование текста
        void copyMenuItem_Click(object sender, EventArgs e)
        {
            // если выделен текст в текстовом поле, то копируем его в буфер
            //MessageBox.Show("copy");
            drawing.Clear();

            Refresh();
        }



        Drawing drawing;

        List<PointF> point = new List<PointF>();


        private void DrawingZone_MouseUp(object sender, MouseEventArgs e)
        {
            point.Add(new PointF(e.X, e.Y));

            drawing.MouseUp(point);

            Refresh();

            point.Clear();

        }

        private void DrawingZone_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStrip.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position 
                    }
                    break;
            }

            point.Add(new PointF(e.X, e.Y));
        }

        private void DrawingZone_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void DrawingZone_Paint(object sender, PaintEventArgs e)
        {
            drawing?.Paint(e, 1);
        }
    }
}

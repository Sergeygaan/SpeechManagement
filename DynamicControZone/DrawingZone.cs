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

            ToolStripMenuItem selectMenuItem = new ToolStripMenuItem("Выделить");
            // добавляем элементы в меню
            rightClickMenuStrip.Items.AddRange(new[] { copyMenuItem, pasteMenuItem, selectMenuItem });
            // ассоциируем контекстное меню с текстовым полем
            //textBox1.ContextMenuStrip = contextMenuStrip1;
            // устанавливаем обработчики событий для меню
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;
            selectMenuItem.Click += selectMenuItem_Click;

            Size resolution = Screen.PrimaryScreen.Bounds.Size;

            drawing = new Drawing(resolution.Width, resolution.Height);

        }

        // вставка текста
        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("paste");
        }

        // вставка текста
        void selectMenuItem_Click(object sender, EventArgs e)
        {
            select = !select;
        }

        // копирование текста
        void copyMenuItem_Click(object sender, EventArgs e)
        {
            // если выделен текст в текстовом поле, то копируем его в буфер
            //MessageBox.Show("copy");
            select = false;

            drawing.Clear();

            Refresh();
        }

        private SelectPointActions selectPointActions = new SelectPointActions();

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

            if (!select)
            {
                point.Add(new PointF(e.X, e.Y));
                point.Add(new PointF(e.X, e.Y));

                flagPaint = true;
            }
            else
            {
                selectPointActions.MouseDown(e, drawing.FiguresList, 1);
                //drawing.SavePoint(e);
            }

            Refresh();
        }

        bool flagPaint = false;
        bool select = false;

        private void DrawingZone_Paint(object sender, PaintEventArgs e)
        {
            if (!select)
            {
                if (flagPaint)
                {
                    drawing?.PaintFigure(e, point);
                }

                drawing?.Paint(e, 1);
            }
            else
            {
                drawing?.Paint(e, 1);

                //if (_selectClass.SeleckResult() != null)
                //{
                //drawing.SupportPoint(drawing.FiguresList);
                //}
            }

            drawing.RefreshBitmap();
        }

        Drawing drawing;

        List<PointF> point = new List<PointF>();

        private void button1_Click(object sender, EventArgs e)
        {
            //drawing.SelectObject(0);

            Refresh();

            select = !select;
        }

        private void DrawingZone_MouseUp(object sender, MouseEventArgs e)
        {
            if (!select)
            {
                drawing.MouseUp(point);

                Refresh();

                point.Clear();

                flagPaint = false;
            }
            else
            {
                selectPointActions.MouseUp(e, drawing.FiguresList, 0);
            }

            Refresh();
        }

        private void DrawingZone_MouseMove(object sender, MouseEventArgs e)
        {
            if (!select)
            {
                drawing.MouseMove(point, e);
            }
            else
            {
                selectPointActions.MouseMove(e, 0, 0);

            }

            Refresh();
        }
    }
}

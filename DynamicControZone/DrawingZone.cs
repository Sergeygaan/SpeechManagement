using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class DrawingZone : Form
    {
        SaveRegion saveRegion;

        Drawing drawing;


        private SelectPointActions selectPointActions = new SelectPointActions();


        private List<PointF> _listPoints = new List<PointF>();

        bool flagPaint = false;
        bool select = false;

        public DrawingZone()
        {
            InitializeComponent();

            saveRegion = new SaveRegion();

            StartPosition = FormStartPosition.CenterScreen;

            //TopMost = true;

            TopLevel = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            AllowTransparency = true;

            //BackColor = Color.AliceBlue;//цвет фона  
            //TransparencyKey = this.BackColor;//он же будет заменен на прозрачный цвет

            BackColor = Color.Black;
            Opacity = 0.65;
            AutoScaleMode = AutoScaleMode.None;

            // создаем элементы меню
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");

            ToolStripMenuItem selectMenuItem = new ToolStripMenuItem("Выделить");
            // добавляем элементы в меню
            AddRegion.Items.AddRange(new[] { copyMenuItem, pasteMenuItem, selectMenuItem });
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

        private void DrawingZone_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        AddRegion.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position 
                    }
                    break;

                case MouseButtons.Left:
                    {
                        if (!select)
                        {
                            _listPoints.Add(new PointF(e.X, e.Y));
                            _listPoints.Add(new PointF(e.X, e.Y));

                            flagPaint = true;
                        }
                        else
                        {
                            selectPointActions.MouseDown(e, drawing.FiguresList, 1);
                            //drawing.SavePoint(e);
                        }

                        Refresh();
                    }
                    break;
            }
        }

        private void DrawingZone_Paint(object sender, PaintEventArgs e)
        {
            if (!select)
            {
                if (flagPaint)
                {
                    drawing?.PaintFigure(e, _listPoints);
                }

                drawing?.Paint(e, 1);
            }
            else
            {
                drawing?.Paint(e, 1);
            }

            drawing.RefreshBitmap();
        }

        private void DrawingZone_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (!select)
                        {
                            if (CheckPoint())
                            {
                                drawing.MouseUp(_listPoints);

                                Refresh();
                            }

                            _listPoints.Clear();

                            flagPaint = false;
                        }
                        else
                        {
                            selectPointActions.MouseUp(e, drawing.FiguresList, 0);
                        }

                        Refresh();
                    }

                    break;
            }
            
        }

        /// <summary>
        /// Проверка на падение при близких координатах
        /// </summary>
        private bool CheckPoint()
        {
            if (_listPoints.Count != 2)
            {
                return false;
            }
            var sumX = Math.Abs((Math.Abs(_listPoints[0].X) - Math.Abs(_listPoints[1].X)));

            var sumY = Math.Abs((Math.Abs(_listPoints[0].Y) - Math.Abs(_listPoints[1].Y)));

            if (sumX < 50)
            {
                return false;
            }

            if (sumY < 50)
            {
                return false;
            }

            return true;
        }

        private void DrawingZone_MouseMove(object sender, MouseEventArgs e)
        {
            if (!select)
            {
                drawing.MouseMove(_listPoints, e);
            }
            else
            {
                selectPointActions.MouseMove(e, 0, 0);
            }

            Refresh();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveRegion.Save(drawing.FiguresList);
            DisposeProject();
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            DisposeProject();
        }

        private void DisposeProject()
        {
            drawing.Dispose();
            selectPointActions.Dispose();

            saveRegion = null;
            _listPoints = null;

            Close();
            Dispose();

            GC.Collect();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class DrawingZone : Form
    {
        SaveRegion saveRegion;

        public DrawingZone()
        {
            InitializeComponent();

            saveRegion = new SaveRegion();

            StartPosition = FormStartPosition.CenterScreen;

            // TopMost = true;

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

        private SelectPointActions selectPointActions = new SelectPointActions();

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
                    break;
            }
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


        private void DrawingZone_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
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

                    break;
            }
            
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveRegion.Save(drawing.FiguresList);
            Close();
        }

        private void EndButton_Click(object sender, EventArgs e)
        {

        }
    }
}

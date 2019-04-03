using ProjectSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class DrawingZone : Form
    {
        /// <summary>
        /// Класс для сохранения нарисованных альтернативных областей
        /// </summary>
        SaveRegion saveRegion;

        /// <summary>
        /// Класс для отрисовки новых областей
        /// </summary>
        private  Drawing _drawing;


        private SelectPointActions selectPointActions = new SelectPointActions();

        /// <summary>
        /// Список с сохранеными координатами мышки. Для построеня объектов
        /// </summary>
        private List<PointF> _listPoints = new List<PointF>();

        bool flagPaint = false;
        bool select = false;

        public DrawingZone(int index = -1)
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
            BackColor = Color.Black;
            Opacity = 0.65;
            AutoScaleMode = AutoScaleMode.None;

            Size resolution = Screen.PrimaryScreen.Bounds.Size;

            _drawing = new Drawing(resolution.Width, resolution.Height);

            if(index != -1)
            {
                LoadingAreasEditing(index);
                select = true;
            }
        }

        private void LoadingAreasEditing(int index)
        {
            foreach (var currentRegion in ProjectSettingsMain.Zone_GeneralList[index])
            {
                _listPoints.Add(new PointF(currentRegion.StartX, currentRegion.StartY));
                _listPoints.Add(new PointF(currentRegion.EndX, currentRegion.EndY));

                _drawing.MouseUp(_listPoints);

                _listPoints.Clear();
            }
        }


        #region Меню

        private void AddMenuItems()
        {
            AddRegion.Items.Clear();

            //Режим работы
            //Добавление
            ToolStripMenuItem MenuItem_AddFigure = new ToolStripMenuItem("Режим \"Добавление\"", null, new EventHandler(MenuItem_AddFigure_Click))
            {
                Checked = !select
            };

            //Выбор
            ToolStripMenuItem MenuItem_Select = new ToolStripMenuItem("Режим \"Редактирование\"", null, new EventHandler(MenuItem_Select_Click))
            {
                Checked = select
            };

            //Разделитель

            //Отменить
            ToolStripMenuItem MenuItem_Cancel = new ToolStripMenuItem("Отменить", null, new EventHandler(EndButton_Click));
            //Сохранить
            ToolStripMenuItem MenuItem_Save = new ToolStripMenuItem("Сохранить", null, new EventHandler(SaveButton_Click));

            //Разделитель

            //Удалить фигуру
            ToolStripMenuItem deleteFigure = new ToolStripMenuItem("Удалить фигуры");
            DeleteFigure_AddList(deleteFigure);

            AddRegion.Items.AddRange(new[] { MenuItem_AddFigure, MenuItem_Select });
            AddRegion.Items.Add(new ToolStripSeparator());
            AddRegion.Items.AddRange(new[] { MenuItem_Cancel, MenuItem_Save });
            AddRegion.Items.Add(new ToolStripSeparator());
            AddRegion.Items.AddRange(new[] { deleteFigure });
        }

        private void DeleteFigure_AddList(ToolStripMenuItem deleteFigure)
        {
            int countFigure = _drawing.FiguresList.Count;

            for(int index = 1; index <= countFigure; index++)
            {
                ToolStripMenuItem currentIndexFigure = new ToolStripMenuItem(index.ToString(), null, new EventHandler(MenuItem_DeleteFigure));
                deleteFigure.DropDownItems.Add(currentIndexFigure);
            }
        }


        #region Команды контекстного меню

        private void MenuItem_DeleteFigure(object sender, EventArgs e)
        {
            try
            {
                _drawing.DeleteFigure(Convert.ToInt16(sender.ToString()));
            }
            catch { }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            saveRegion.Save(_drawing.FiguresList);
            DisposeProject();
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            DisposeProject();
        }

        /// <summary>
        /// Режим добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_AddFigure_Click(object sender, EventArgs e)
        {
            select = false;

            selectPointActions.Deselect();
            Refresh();
        }

        /// <summary>
        /// режим редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Select_Click(object sender, EventArgs e)
        {
            select = true;
        }

        #endregion

        #endregion


        private void DrawingZone_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        AddMenuItems();

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
                            selectPointActions.MouseDown(e, _drawing.FiguresList, 1);
                        }

                        Refresh();
                    }

                    break;
            }
        }

        /// <summary>
        /// Перерисовка области
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingZone_Paint(object sender, PaintEventArgs e)
        {
            if (!select)
            {
                if (flagPaint)
                {
                    _drawing?.PaintFigure(e, _listPoints);
                }

                _drawing?.Paint(e, 1);
            }
            else
            {
                _drawing?.Paint(e, 1);
            }

            _drawing.RefreshBitmap();
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
                                _drawing.MouseUp(_listPoints);

                                Refresh();
                            }

                            _listPoints.Clear();

                            flagPaint = false;
                        }
                        else
                        {
                            selectPointActions.MouseUp(e, _drawing.FiguresList, 0);
                        }

                        Refresh();
                    }

                    break;


                    //case MouseButtons.Right:

                    //    if (select)
                    //    {
                    //        selectPointActions.MouseUp(e, _drawing.FiguresList, 0);

                    //        Refresh();
                    //    }

                    //    break;
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
                _drawing.MouseMove(_listPoints, e);
            }
            else
            {
                selectPointActions.MouseMove(e, 0, 0);
            }

            Refresh();
        }

        /// <summary>
        /// Очистка проекта
        /// </summary>
        private void DisposeProject()
        {
            _drawing.Dispose();
            selectPointActions.Dispose();

            saveRegion = null;
            _listPoints = null;

            Close();
            Dispose();

            GC.Collect();
        }
    }
}

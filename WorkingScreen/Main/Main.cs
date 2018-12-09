using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Command;
using ProjectSettings;

namespace WorkingScreen
{
    class Main
    {
        private WorkObject _workObject;

        private int _widthFrom;
        private int _heightForm;

        private List<ICommand> commands = new List<ICommand>();

        public Main(int widthFrom, int heightForm)
        {
            _widthFrom = widthFrom;
            _heightForm = heightForm;

            _workObject = new WorkObject
            {
                GenerationNumber = 1
            };

            commands.Add(new CommandLeftOneClick()); //0
            commands.Add(new CommandRightOneClick()); //1
            commands.Add(new CommandLeftDoubleClick()); //2
            commands.Add(new CommandScale());//3
            commands.Add(new CommandMagnifier()); //4
            commands.Add(new CommandLeftLeftDown()); //5
            commands.Add(new CommandLeftLeftUp()); //6
            commands.Add(new CommandScroll()); //7
            commands.Add(new CommandEnd()); //8

            Auxiliary.WorkObject = _workObject;
        }

        public void OnPaint(PaintEventArgs e)
        {
            DrawingZone(e, _workObject);
        }

        //Метод по отрисовке зон 
        private void DrawingZone(PaintEventArgs e, WorkObject _currentObject)
        {
            StartingDelimitationZone();

            if (_currentObject.ChildNumberObject != null)
            {
                DrawingZone(e, _currentObject.ChildNumberObject);
            }

            _currentObject.DrawingRectangle(e);

            _currentObject.DrawingText(e);
        }

        private void StartingDelimitationZone()
        {
            if(ProjectSettingsMain.Zone_TracingChangeDraw)
            {
                //Отменить все
                commands[8].Act(2);

                _workObject.listRegionRectangle.Clear();

                ProjectSettingsMain.Zone_TracingChangeDraw = false;

                GC.Collect();
            }

            if (_workObject.listRegionRectangle.Count == 0)
            {
                if (ProjectSettingsMain.Zone_DrawMethod == 0)
                {
                    Drawing.MethodMain(_workObject, _widthFrom, _heightForm);
                }
                else
                {
                    Drawing.MethodAlternative(_workObject, ProjectSettingsMain.Zone_ListRegionRectangle);
                }
            }
        }
     
        public void ApplyCommand(int indexCommand, int number)
        {
            commands[indexCommand].Act(number);   
        }
    }
}

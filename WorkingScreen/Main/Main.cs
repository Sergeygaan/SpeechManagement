using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main.Drawing;
using Command;

namespace VoiceControl
{
    class Main
    {
        private WorkObject _workObject;

        private Drawing _drawing;

        private int _widthFrom;
        private int _heightForm;

        private List<ICommand> commands = new List<ICommand>();

        public Main(int widthFrom, int heightForm)
        {
            _widthFrom = widthFrom;
            _heightForm = heightForm;

            _workObject = new WorkObject();
            _workObject.GenerationNumber = 1;

            _drawing = new Drawing();


            commands.Add(new CommandLeftOneClick());
            commands.Add(new CommandRightOneClick());
            commands.Add(new CommandLeftDoubleClick());
            commands.Add(new CommandScale(_drawing));
            commands.Add(new CommandMagnifier());
            commands.Add(new CommandEnd());

            Auxiliary.WorkObject = _workObject;
        }

        public void OnPaint(PaintEventArgs e)
        {
            DrawingZone(e, _workObject);

            //if(flag)
            //{
            //    ApplyCommand(3, 1);

            //    //ApplyCommand(5, 2);

            //    flag = false;
            //}
        }

        bool flag = true;

        //Метод по отрисовке зон 
        private void DrawingZone(PaintEventArgs e, WorkObject _currentObject)
        {
            if (_workObject.listRegionRectangle.Count == 0)
            {
                _drawing.DrawingDividingLines(_workObject, _widthFrom, _heightForm);
            }

            if (_currentObject.ChildNumberObject != null)
            {
                DrawingZone(e, _currentObject.ChildNumberObject);
            }

            _currentObject.DrawingRectangle(e);

            _currentObject.DrawingText(e);
        }

        public void ApplyCommand(int indexCommand, int number)
        {
            commands[indexCommand].Act(number - 1);
        }
    }
}

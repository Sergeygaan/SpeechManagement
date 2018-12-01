using Main.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSettings;
using static Command.Auxiliary;

namespace Command
{
    class CommandScale : ICommand
    {
        Drawing _drawing;

        public CommandScale(Drawing drawing)
        {
            _drawing = drawing;
        }

        public void Act(int index)
        {
            var currentNumberObject = SearchChild();

            var newChildNumberObject = new WorkObject
            {
                //Добавление родителя
                ParantNumberObject = currentNumberObject,
                GenerationNumber = currentNumberObject.GenerationNumber + 1
            };

            //Добавление потомка
            currentNumberObject.ChildNumberObject = newChildNumberObject;

            currentNumberObject.Visible = false;

            _drawing.DrawingDividingLines(newChildNumberObject,
                                 currentNumberObject.listRegionRectangle[index].Width, currentNumberObject.listRegionRectangle[index].Height,
                                 currentNumberObject.listRegionRectangle[index].StartX, currentNumberObject.listRegionRectangle[index].StartY);
        }
    }
}

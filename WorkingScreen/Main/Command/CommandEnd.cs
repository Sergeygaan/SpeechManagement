using Command.Magnif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Command.Auxiliary;

namespace Command
{
    class CommandEnd : ICommand
    {
        public void Act(int index)
        {
            var currentWorkObject = SearchChild();

            if (currentWorkObject.Magnifier!= null)
            {
                //Метод отменяет лупу
                currentWorkObject.Magnifier.Dispose();
                currentWorkObject.Magnifier = null;
            }

            if (SearchChild().ParantNumberObject != null)
            {
                var ParantNumberObject = SearchChild().ParantNumberObject;

                if (ParantNumberObject.ChildNumberObject != null)
                {
                    ParantNumberObject.ChildNumberObject = null;

                    ParantNumberObject.Visible = true;
                }
            }
        }
    }
}

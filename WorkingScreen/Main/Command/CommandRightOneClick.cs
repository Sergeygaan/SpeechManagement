using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandRightOneClick : ICommand
    {
        public void Act(int index)
        {
            var currentObject = SearchChild();

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.RIGHTDOWN | (uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
        }
    }
}

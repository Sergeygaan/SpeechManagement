using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandLeftDoubleClick : ICommand
    {
        public void Act(int index)
        {
            var currentObject = SearchChild();

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }
    }
}

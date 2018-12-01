using System.Drawing;
using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandScroll : ICommand
    {
        public void Act(int index)
        {
            int commandData;

            if (index == 0)
            {
                commandData = 500;
            }
            else
            {
                commandData = -500;
            }

            mouse_event((uint)MouseEventFlags.SCROLL, 0, 0, commandData, 0);
        }
    }
}

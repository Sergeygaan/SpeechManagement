using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandLeftOneClick : ICommand
    {
        public void Act(int index)
        {
            var currentObject = SearchChild();

            Cursor.Position = currentObject.Center(index);

            mouse_event((uint)MouseEventFlags.LEFTDOWN | (uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }
    }
}

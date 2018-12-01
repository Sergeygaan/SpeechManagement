using System.Drawing;
using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandLeftLeftDown : ICommand
    {
        public void Act(int index)
        {
            var currentObject = SearchChild();

            Cursor.Position = Point.Round(currentObject.Center(index));

            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
        }
    }
}

using System.Drawing;
using System.Windows.Forms;
using static Command.Auxiliary;

namespace Command
{
    class CommandLeftLeftUp : ICommand
    {
        public void Act(int index)
        {
            var currentObject = SearchChild();

            Cursor.Position = Point.Round(currentObject.Center(index));

            mouse_event((uint)MouseEventFlags.MOVE, 0, 0, 0, 0);

            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }
    }
}

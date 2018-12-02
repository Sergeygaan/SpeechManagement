using Command;
using System.Drawing;
using System.Windows.Forms;

namespace WorkingScreen
{
    class CommandLeftLeftDown : ICommand
    {
        public void Act(int index)
        {
            var currentObject = Auxiliary.SearchChild();

            Cursor.Position = Point.Round(currentObject.Center(index));

            Auxiliary.mouse_event((uint)Auxiliary.MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
        }
    }
}

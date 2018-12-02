using System.Drawing;
using System.Windows.Forms;

namespace WorkingScreen
{
    class CommandLeftLeftUp : ICommand
    {
        public void Act(int index)
        {
            var currentObject = Auxiliary.SearchChild();

            Cursor.Position = Point.Round(currentObject.Center(index));

            Auxiliary.mouse_event((uint)Auxiliary.MouseEventFlags.MOVE, 0, 0, 0, 0);

            Auxiliary.mouse_event((uint)Auxiliary.MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }
    }
}

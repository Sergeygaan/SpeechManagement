using Command;

namespace WorkingScreen
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

            Auxiliary.mouse_event((uint)Auxiliary.MouseEventFlags.SCROLL, 0, 0, commandData, 0);
        }
    }
}

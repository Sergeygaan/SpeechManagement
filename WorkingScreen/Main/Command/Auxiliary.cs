using System;
using System.Runtime.InteropServices;

namespace WorkingScreen
{
    static class Auxiliary
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, int dx, uint dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            SCROLL = 0x800
    }

        static public WorkObject WorkObject;

        static public WorkObject SearchChild()
        {
            var local = WorkObject;

            while (local.ChildNumberObject != null)
            {
                local = local.ChildNumberObject;
            }

            return local;
        }

    }
}

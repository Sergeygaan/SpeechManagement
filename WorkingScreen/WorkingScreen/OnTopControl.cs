using Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VoiceControl
{ 
    public class OnTopControl : IDisposable
    {
        /// <summary>
        /// Brings the specified window to the top of the Z order.
        /// If the window is a top-level window, it is activated. If the window is a child window, the top-level parent window associated with the child window is activated.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);


        //Fix 10317,7317,7309,7664,5258(AT),5099,5098,4810(A-3.1)
        public OnTopControl(IntPtr intPtrControl)
        {
            thread = new Thread(() =>
            {
                try
                {
                    //If its name begins with the prefix "Global\", the mutex is visible in all terminal server sessions.
                    //If its name begins with the prefix "Local\", the mutex is visible only in the terminal server session where it was created.
                    //If you do not specify a prefix when you create a named mutex, it takes the prefix "Local\".
                    //[refix 12169 & 12135] В каждой сессии свой набор окон, поэтому можно допустить параллельную работу в сессиях. Очевидно что нагрузка на ЦП несколько увеличится.
                    using (Mutex mutex = new Mutex(false, "B0F8707FA1E54755B2DB83A0FE31216C"))
                        while (true)
                        {
                            try
                            {
                                if (mutex.WaitOne(Timeout.Infinite, false))
                                {
                                    if (CommandMagnifier.magnifierForm == null)
                                    {
                                        BringWindowToTop(intPtrControl);
                                        mutex.ReleaseMutex();
                                    }
                                }
                            }
                            catch { /*А вот эту ошибку надо бы мониторить в подсистеме*/ }
                            Thread.Sleep(timerSleep);
                        }
                }
                catch (ThreadAbortException) { /*На аборте тихо выключаемся*/ }
                catch { /*А вот эту ошибку надо бы мониторить в подсистеме, поскольку это вылет из потока*/ }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public void Dispose()
        {
            thread.Abort();
            thread = null;
        }


        Thread thread;
        int timerSleep = 1000;
    }
}

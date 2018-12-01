using Command;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProjectSettings
{ 
    public class OnTopControl : IDisposable
    {
        private Thread _thread;
        private int _timerSleep = 1000;

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        public OnTopControl(IntPtr intPtrControl)
        {
            _thread = new Thread(() =>
            {
                try
                {
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
                            Thread.Sleep(_timerSleep);
                        }
                }
                catch (ThreadAbortException) { /*На аборте тихо выключаемся*/ }
                catch { /*А вот эту ошибку надо бы мониторить в подсистеме, поскольку это вылет из потока*/ }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Dispose()
        {
            _thread.Abort();
            _thread = null;
        }
    }
}

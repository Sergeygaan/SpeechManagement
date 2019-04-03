using MyPaint;
using System;
using System.Windows.Forms;

namespace VoiceControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogForm());

            //Application.Run(new DrawingZone());
        }
    }
}

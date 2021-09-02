using System;
using System.Windows.Forms;

namespace PianoDesign
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana başlangıç noktası
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

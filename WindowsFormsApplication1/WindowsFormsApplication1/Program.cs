using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobLoadBoard
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
            string path = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(path);
            if (process.Length>1)
            {
                MessageBox.Show("系统已经在运行！");
            }
            else
            {
                  Application.Run(new MainForm());
            }
          
        }
    }
}

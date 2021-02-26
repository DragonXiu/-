using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1.view
{
    class text_tm : TextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (LoadLibrary("msftedit") != IntPtr.Zero)
                {
                    prams.ExStyle |= 0x020;
                    // prams.
                    prams.ClassName = "RICHEDIT50W";
                }
                return prams;
            }
        }
    }
}

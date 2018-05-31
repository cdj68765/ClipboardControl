using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardControl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Mutex Pro = new Mutex(true, "ClipboardControl", out bool Close);
            if (!Close) return;
            /*   if (NativeMethods.EnumWindows((hWnd, lParam) =>
                        {
                            StringBuilder GetText = new StringBuilder(256);
                            NativeMethods.GetWindowTextW(hWnd, GetText, 256);
                            if (GetText.ToString().StartsWith("ClipboardControl"))
                            {
                                return false;
                            }

                            return true;
                        }, 0)) return;*/
            Application.Run(new Form1());
            Application.Exit();
        }
    }
}
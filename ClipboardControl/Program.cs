using System.Threading;
using System.Windows.Forms;

namespace ClipboardControl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var Pro = new Mutex(true, "ClipboardControl", out var Close);
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
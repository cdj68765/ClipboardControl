using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardMonitor
{
    internal class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {
            Mutex Pro = new Mutex(true, "ClipboardMonitor", out bool Close);
            if (!Close) return;
            string Temp = "";
            while (true)
            {
                string Tex = Clipboard.GetText().ToString();
                if (!string.IsNullOrWhiteSpace(Tex) && Temp != Tex)
                {
                    Thread NT = new Thread(() =>
                    {
                        try
                        {
                            Clipboard.Clear();
                            // Clipboard.SetText(Tex.ToString());
                            Clipboard.SetDataObject(Tex, false);
                            Temp = Tex;
                        }
                        catch (Exception)
                        {
                        }
                    });
                    NT.TrySetApartmentState(ApartmentState.STA);
                    NT.IsBackground = true;
                    NT.Start();
                    while (NT.IsAlive)
                    {
                        Thread.Sleep(1);
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
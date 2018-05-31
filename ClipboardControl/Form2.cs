using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClipboardControl
{
    public partial class Form2 : Form
    {
        private bool Onice = true;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x031D && Onice)
            {
                if (!Form1.start) return;
                var GetText = new StringBuilder(256);
                NativeMethods.GetWindowTextW(NativeMethods.GetForegroundWindow(IntPtr.Zero), GetText, 256);
                if (GetText.ToString().StartsWith("CAXA"))
                {
                    var Temp = ClipboardControl.GetText(ClipboardFormat.CF_UNICODETEXT);
                    if (!string.IsNullOrEmpty(Temp))
                    {
                        ClipboardControl.SetText(Temp);
                        Onice = false;
                        Console.WriteLine(Temp);
                    }

                }
            }
            else if (!Onice)
            {
                Onice = true;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        public Form2()
        {
            InitializeComponent();
            NativeMethods.AddClipboardFormatListener(this.Handle);
            Visible = false;
        }
    }
}

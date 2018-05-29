using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardControl
{
    internal class ClipboardControl
    {


        internal static void SetText(string text)
        {
            NativeMethods.OpenClipboard(IntPtr.Zero);
            NativeMethods.EmptyClipboard();
            NativeMethods.SetClipboardData(ClipboardFormat.CF_UNICODETEXT, Marshal.StringToHGlobalUni(text));
            NativeMethods.CloseClipboard();
        }

        internal static string GetText(int format)
        {
            string value = string.Empty;
            NativeMethods.OpenClipboard(IntPtr.Zero);
            if (NativeMethods.IsClipboardFormatAvailable(format))
            {
                IntPtr ptr = NativeMethods.GetClipboardData(format);
                if (ptr != IntPtr.Zero)
                {
                    value = Marshal.PtrToStringUni(ptr);
                }

                NativeMethods.CloseClipboard();
            }

            return value;
        }
    }

}


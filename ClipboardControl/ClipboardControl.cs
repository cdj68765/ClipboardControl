using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ClipboardControl
{
    internal class ClipboardControl
    {
        internal static void SetText(string text)
        {
            if (!NativeMethods.OpenClipboard(IntPtr.Zero))
            {
                SetText(text);
                Thread.Sleep(50);
                return;
            }
            NativeMethods.EmptyClipboard();
            NativeMethods.SetClipboardData(ClipboardFormat.CF_UNICODETEXT, Marshal.StringToCoTaskMemAuto(text));
            NativeMethods.CloseClipboard();
        }

        internal static string GetText(int format)
        {
            var value = string.Empty;
            NativeMethods.OpenClipboard(IntPtr.Zero);
            if (NativeMethods.IsClipboardFormatAvailable(format))
            {
                var ptr = NativeMethods.GetClipboardData(format);
                if (ptr != IntPtr.Zero) value = Marshal.PtrToStringUni(ptr);
            }

            NativeMethods.CloseClipboard();
            return value;
        }
    }
}
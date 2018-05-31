using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ClipboardControl
{
    internal class NativeMethods
    {
        public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

        //查找窗体
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName,
            string lpWindowName);

        [DllImport("user32")]
        public static extern bool EnumWindows(CallBack hwnd, int uCmd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString,
            int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString,
            int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow(IntPtr hWnd);

        #region Clipboard

        [DllImport("User32")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("User32")]
        internal static extern bool CloseClipboard();

        [DllImport("User32")]
        internal static extern bool EmptyClipboard();

        [DllImport("User32")]
        internal static extern bool IsClipboardFormatAvailable(int format);

        [DllImport("User32")]
        internal static extern IntPtr GetClipboardData(int uFormat);

        [DllImport("User32", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetClipboardData(int uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        internal static extern int GetClipboardFormatName(uint format, [Out] StringBuilder
            lpszFormatName, int cchMaxCount);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetOpenClipboardWindow();

        [DllImport("user32.dll")]
        internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

        #endregion Clipboard
    }
}
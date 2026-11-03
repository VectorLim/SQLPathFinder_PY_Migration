using System;
using System.Runtime.InteropServices;
using System.Text;

public static partial class Program
{
    private delegate bool EnumWindowsProc(IntPtr handle, IntPtr state);
    private delegate bool EnumChildWindowsProc(IntPtr handle, IntPtr state);
    [StructLayout(LayoutKind.Sequential)] private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
    [StructLayout(LayoutKind.Sequential)] private struct GUITHREADINFO
    {
        public int cbSize; public int flags; public IntPtr hwndActive; public IntPtr hwndFocus;
        public IntPtr hwndCapture; public IntPtr hwndMenuOwner; public IntPtr hwndMoveSize; public IntPtr hwndCaret; public RECT rcCaret;
    }

    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc callback, IntPtr state);
    [DllImport("user32.dll")] private static extern bool EnumChildWindows(IntPtr parent, EnumChildWindowsProc callback, IntPtr state);
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr handle, out uint processId);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr handle);
    [DllImport("user32.dll")] private static extern bool IsWindowEnabled(IntPtr handle);
    [DllImport("user32.dll")] private static extern IntPtr GetParent(IntPtr handle);
    [DllImport("user32.dll")] private static extern bool IsWindow(IntPtr handle);
    [DllImport("user32.dll")] private static extern bool IsChild(IntPtr parent, IntPtr child);
    [DllImport("user32.dll")] private static extern bool GetGUIThreadInfo(uint threadId, ref GUITHREADINFO info);
    [DllImport("user32.dll")] private static extern IntPtr GetWindow(IntPtr handle, uint command);
    [DllImport("user32.dll")] private static extern IntPtr GetAncestor(IntPtr handle, uint flags);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetWindowText(IntPtr handle, StringBuilder text, int maxCount);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetClassName(IntPtr handle, StringBuilder text, int maxCount);
    [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr handle, out RECT rect);
    [DllImport("user32.dll")] private static extern int GetDlgCtrlID(IntPtr handle);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongW")] private static extern int GetWindowLong(IntPtr handle, int index);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")] private static extern IntPtr GetWindowLongPtr(IntPtr handle, int index);
    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageTimeoutW")] private static extern IntPtr SendMessageTimeoutText(IntPtr handle, uint message, IntPtr wParam, [Out] StringBuilder lParam, uint flags, uint timeout, out IntPtr result);
    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageTimeoutW")] private static extern IntPtr SendMessageTimeoutValue(IntPtr handle, uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, out IntPtr result);
    [DllImport("user32.dll")] private static extern bool PostMessage(IntPtr handle, uint message, IntPtr wParam, IntPtr lParam);
    [DllImport("oleacc.dll")] private static extern int AccessibleObjectFromWindow(IntPtr handle, uint objectId, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object accessible);
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public static partial class Program
{
    private static WindowObservation ReadSourceEditor(NativeWindow window)
    {
        List<IntPtr> edits = FindMultilineEdits(window.Handle);
        if (edits.Count != 1)
            return new WindowObservation { Title = window.Title, Source = "", Selection = new SelectionResult { Text = "", Truncated = false }, Bounds = window.Bounds, HasEditor = true, DesignerValues = new List<string>(), DesignerStateValues = new List<string>() };
        bool truncated;
        return new WindowObservation
        {
            Title = window.Title,
            Source = ReadText(edits[0], out truncated),
            SourceTruncated = truncated,
            Selection = ReadSelection(edits[0]),
            FocusHandle = edits[0],
            Bounds = window.Bounds,
            HasEditor = true,
            DesignerValues = new List<string>(),
            DesignerStateValues = new List<string>()
        };
    }

    private static List<IntPtr> FindMultilineEdits(IntPtr parent)
    {
        List<IntPtr> edits = new List<IntPtr>();
        EnumChildWindows(parent, delegate (IntPtr handle, IntPtr state)
        {
            if (!IsWindowVisible(handle) || !IsNativeClass(GetClass(handle), "Edit")) return true;
            long style = GetWindowStyle(handle);
            if ((style & EsMultiline) == 0 || (style & EsPassword) != 0) return true;
            edits.Add(handle);
            return edits.Count < 4;
        }, IntPtr.Zero);
        return edits;
    }

    private static string ReadText(IntPtr handle, out bool truncated)
    {
        IntPtr lengthResult;
        IntPtr sent = SendMessageTimeoutValue(handle, 0x000e, IntPtr.Zero, IntPtr.Zero, 0x0002, 1500, out lengthResult);
        if (sent == IntPtr.Zero) { truncated = false; return ""; }
        int length = Math.Max(0, Math.Min(MaxText, lengthResult.ToInt32()));
        truncated = lengthResult.ToInt64() > MaxText;
        StringBuilder buffer = new StringBuilder(length + 1);
        IntPtr result;
        sent = SendMessageTimeoutText(handle, 0x000d, new IntPtr(buffer.Capacity), buffer, 0x0002, 1500, out result);
        return sent == IntPtr.Zero ? "" : buffer.ToString();
    }

    private static SelectionResult ReadSelection(IntPtr handle)
    {
        IntPtr start = Marshal.AllocHGlobal(sizeof(int));
        IntPtr end = Marshal.AllocHGlobal(sizeof(int));
        try
        {
            Marshal.WriteInt32(start, 0); Marshal.WriteInt32(end, 0);
            IntPtr result;
            IntPtr sent = SendMessageTimeoutValue(handle, 0x00b0, start, end, 0x0002, 1500, out result);
            if (sent == IntPtr.Zero) return new SelectionResult { Text = "", Truncated = false };
            return new SelectionResult { Text = "", Truncated = false, Start = Math.Max(0, Marshal.ReadInt32(start)), End = Math.Max(0, Marshal.ReadInt32(end)) };
        }
        catch (Exception) { return new SelectionResult { Text = "", Truncated = false }; }
        finally { Marshal.FreeHGlobal(start); Marshal.FreeHGlobal(end); }
    }
}
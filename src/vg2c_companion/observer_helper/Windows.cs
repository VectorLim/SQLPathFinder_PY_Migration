using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public static partial class Program
{
    private static List<NativeWindow> FindSourceEditors(List<NativeWindow> windows, IntPtr owner)
    {
        List<NativeWindow> result = new List<NativeWindow>();
        foreach (NativeWindow window in windows)
            if (window.Title != null && window.Title.StartsWith("Editor - [", StringComparison.OrdinalIgnoreCase)
                && (owner == IntPtr.Zero || IsOwnedBy(window.Handle, owner))) result.Add(window);
        return result;
    }

    private static Process FindTarget(int requestedPid, out string error)
    {
        error = null;
        if (requestedPid > 0)
        {
            try
            {
                Process process = Process.GetProcessById(requestedPid);
                return String.Equals(process.ProcessName, "sqlpathfinder3", StringComparison.OrdinalIgnoreCase) ? process : null;
            }
            catch (ArgumentException) { return null; }
        }
        Process[] processes = Process.GetProcessesByName("sqlpathfinder3");
        if (processes.Length > 1)
        {
            error = "Multiple SQLPathFinder processes are running; pass --pid to select one.";
            return null;
        }
        return processes.Length == 0 ? null : processes[0];
    }

    private static List<NativeWindow> FindTopWindows(int pid, IntPtr mainHandle)
    {
        List<NativeWindow> windows = new List<NativeWindow>();
        HashSet<long> handles = new HashSet<long>();
        EnumWindows(delegate (IntPtr handle, IntPtr state)
        {
            uint ownerPid;
            if (GetWindowThreadProcessId(handle, out ownerPid) == 0 || ownerPid != (uint)pid) return true;
            if (!IsWindowVisible(handle) || !handles.Add(handle.ToInt64())) return true;
            windows.Add(ReadWindow(handle));
            return windows.Count < 64;
        }, IntPtr.Zero);
        if (mainHandle != IntPtr.Zero && handles.Add(mainHandle.ToInt64())) windows.Add(ReadWindow(mainHandle));
        return windows;
    }

    private static NativeWindow FindDesigner(List<NativeWindow> windows, int pid, string document, IntPtr requestedHandle, out string error)
    {
        error = null;
        List<NativeWindow> candidates = new List<NativeWindow>();
        foreach (NativeWindow window in windows)
        {
            if (requestedHandle != IntPtr.Zero && window.Handle != requestedHandle) continue;
            if (!IsWindowVisible(window.Handle)) continue;
            if (!IsDesignerTitle(window.Title, document)) continue;
            uint ownerPid;
            if (GetWindowThreadProcessId(window.Handle, out ownerPid) == 0 || ownerPid != (uint)pid) continue;
            candidates.Add(window);
        }
        if (requestedHandle != IntPtr.Zero && candidates.Count == 0)
        {
            error = "The selected SQLPathFinder designer window is no longer available.";
            return null;
        }
        if (candidates.Count > 1)
        {
            error = "Multiple SQLPathFinder designer windows match this VG2 document.";
            return null;
        }
        return candidates.Count == 0 ? null : candidates[0];
    }

    private static TargetListResult ListTargets()
    {
        List<NativeTargetResult> targets = new List<NativeTargetResult>();
        foreach (Process process in Process.GetProcessesByName("sqlpathfinder3"))
        {
            try
            {
                if (process.HasExited) continue;
                foreach (NativeWindow window in FindDesignerCandidates(FindTopWindows(process.Id, process.MainWindowHandle), ""))
                    targets.Add(new NativeTargetResult
                    {
                        TargetPid = process.Id,
                        DesignerHwnd = window.Handle.ToInt64(),
                        DesignerWindowTitle = window.Title,
                        DocumentName = ExtractDocumentName(window.Title)
                    });
            }
            catch { }
        }
        return new TargetListResult { Status = "ok", Targets = targets };
    }

    private static List<NativeWindow> FindDesignerCandidates(List<NativeWindow> windows, string document)
    {
        List<NativeWindow> candidates = new List<NativeWindow>();
        foreach (NativeWindow window in windows)
            if (IsWindowVisible(window.Handle) && IsDesignerTitle(window.Title, document)) candidates.Add(window);
        return candidates;
    }

    private static bool IsDesignerTitle(string title, string document)
    {
        if (String.IsNullOrWhiteSpace(title) || title.IndexOf("Query Log", StringComparison.OrdinalIgnoreCase) >= 0)
            return false;
        if (title.IndexOf("SQLPathFinder", StringComparison.OrdinalIgnoreCase) < 0)
            return false;
        string actualDocument = ExtractDocumentName(title);
        return !String.IsNullOrWhiteSpace(actualDocument)
            && (String.IsNullOrWhiteSpace(document)
                || String.Equals(actualDocument, System.IO.Path.GetFileName(document), StringComparison.OrdinalIgnoreCase));
    }

    private static string ExtractDocumentName(string title)
    {
        int marker = title == null ? -1 : title.IndexOf(" - [", StringComparison.Ordinal);
        if (marker < 0) return "";
        string value = title.Substring(marker + 4).Trim().TrimEnd(']').Trim();
        int extension = value.LastIndexOf(".vg2", StringComparison.OrdinalIgnoreCase);
        if (extension < 0) return "";
        int end = extension + ".vg2".Length;
        string suffix = value.Substring(end).Trim();
        if (suffix.Length != 0 && !(suffix.StartsWith("(", StringComparison.Ordinal) && suffix.EndsWith(")", StringComparison.Ordinal)))
            return "";
        return value.Substring(0, end);
    }

    private static NativeWindow FindSourceEditor(List<NativeWindow> windows, IntPtr owner)
    {
        NativeWindow candidate = null;
        foreach (NativeWindow window in windows)
        {
            if (window.Title == null || !window.Title.StartsWith("Editor - [", StringComparison.OrdinalIgnoreCase)) continue;
            if (owner != IntPtr.Zero && !IsOwnedBy(window.Handle, owner)) continue;
            if (candidate != null) return null;
            candidate = window;
        }
        return candidate;
    }

    private static bool IsOwnedBy(IntPtr window, IntPtr owner)
    {
        IntPtr current = window;
        for (int depth = 0; depth < 8 && current != IntPtr.Zero; depth++)
        {
            current = GetWindow(current, GwOwner);
            if (current == owner) return true;
        }
        return GetAncestor(window, GaRootOwner) == owner;
    }

    private static NativeWindow ReadWindow(IntPtr handle)
    {
        return new NativeWindow { Handle = handle, Title = ReadWindowText(handle, 512), ClassName = GetClass(handle), Bounds = ReadBounds(handle) };
    }

    private static string ReadWindowText(IntPtr handle, int max)
    {
        StringBuilder text = new StringBuilder(max);
        return GetWindowText(handle, text, text.Capacity) == 0 ? "" : text.ToString();
    }

    private static string GetClass(IntPtr handle)
    {
        StringBuilder text = new StringBuilder(128);
        return GetClassName(handle, text, text.Capacity) == 0 ? "" : text.ToString();
    }

    private static bool IsNativeClass(string actual, string native)
    {
        if (String.Equals(actual, native, StringComparison.OrdinalIgnoreCase)) return true;
        string prefix = "WindowsForms10." + native + ".";
        return actual != null && actual.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
    }

    private static long GetWindowStyle(IntPtr handle)
    {
        return IntPtr.Size == 4 ? GetWindowLong(handle, GwlStyle) : GetWindowLongPtr(handle, GwlStyle).ToInt64();
    }

    private static int[] ReadBounds(IntPtr handle)
    {
        RECT rect;
        return GetWindowRect(handle, out rect) ? new[] { rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top } : null;
    }

    private static string DesignerDocument(string title)
    {
        int marker = title.LastIndexOf(" - [", StringComparison.Ordinal);
        return marker < 0 ? title : title.Substring(marker + 4).TrimEnd(']').ToLowerInvariant();
    }
}
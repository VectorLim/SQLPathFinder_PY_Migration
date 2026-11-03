using System;
using System.Collections.Generic;
using System.Diagnostics;

public static partial class Program
{
    private static RefreshResult Refresh(int requestedPid, IntPtr requestedHandle, string document)
    {
        if (requestedPid <= 0 || requestedHandle == IntPtr.Zero || String.IsNullOrWhiteSpace(document))
            return RefreshEmpty("inaccessible", "Select an exact SQLPathFinder process, designer window, and VG2 document.");
        string targetError;
        List<NativeWindow> windows;
        NativeWindow designer;
        Process process = FindRefreshTarget(requestedPid, requestedHandle, document, out targetError, out windows, out designer);
        if (targetError != null) return RefreshEmpty("ambiguous", targetError);
        if (process == null) return RefreshEmpty("missing", "SQLPathFinder3 (sqlpathfinder3) is not running.");
        try
        {
            if (process.HasExited) return RefreshEmpty("exited", "The selected SQLPathFinder process exited.", process.Id);
            List<NativeWindow> existingEditors = FindSourceEditors(windows, IntPtr.Zero);
            if (existingEditors.Count != 0)
                return RefreshEmpty("editor-open", "Close the existing Show Query editor, then press Update Python again.", process.Id, designer.Title);
            HashSet<long> existingWindows = new HashSet<long>();
            foreach (NativeWindow window in windows) existingWindows.Add(window.Handle.ToInt64());

            string actionError = null;
            System.Threading.Thread action = new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                try { InvokeShowQuery(designer.Handle); }
                catch (Exception ex) { actionError = ex.Message; }
            });
            action.IsBackground = true;
            action.SetApartmentState(System.Threading.ApartmentState.STA);
            action.Start();
            DateTime deadline = DateTime.UtcNow.AddMilliseconds(RefreshTimeoutMs);
            NativeWindow editor = null;
            while (DateTime.UtcNow < deadline)
            {
                if (process.HasExited) return RefreshEmpty("exited", "SQLPathFinder exited during Show Query refresh.", process.Id);
                List<NativeWindow> current = FindTopWindows(process.Id, process.MainWindowHandle);
                current.RemoveAll(delegate (NativeWindow window) { return existingWindows.Contains(window.Handle.ToInt64()); });
                editor = FindSourceEditor(current, IntPtr.Zero);
                if (editor != null) break;
                if (actionError != null) return RefreshEmpty("unsupported", actionError, process.Id, designer.Title);
                System.Threading.Thread.Sleep(100);
            }
            if (editor == null)
                return RefreshEmpty("timeout", actionError ?? "Show Query did not create a new editor.", process.Id, designer.Title);
            WindowObservation source;
            try { source = ReadSourceEditor(editor); }
            catch (Exception ex) { return FinishRefresh(editor, process.Id, designer.Title, RefreshEmpty("error", ex.Message, process.Id, designer.Title), action); }
            if (source.SourceTruncated)
                return FinishRefresh(editor, process.Id, designer.Title, RefreshEmpty("error", "Show Query source is larger than the supported limit.", process.Id, designer.Title), action);
            if (String.IsNullOrWhiteSpace(source.Source))
                return FinishRefresh(editor, process.Id, designer.Title, RefreshEmpty("empty", "Show Query created an empty or inaccessible source editor.", process.Id, designer.Title), action);
            uint ownerPid;
            if (GetWindowThreadProcessId(editor.Handle, out ownerPid) == 0 || ownerPid != (uint)process.Id)
                return RefreshEmpty("inaccessible", "The captured editor no longer belongs to SQLPathFinder.", process.Id, designer.Title);
            string designerFingerprint;
            try
            {
                designerFingerprint = Fingerprint(DesignerDocument(designer.Title), "", ReadDesigner(designer).DesignerStateValues);
            }
            catch (Exception ex)
            {
                return FinishRefresh(editor, process.Id, designer.Title, RefreshEmpty("error", ex.Message, process.Id, designer.Title), action);
            }
            RefreshResult result = new RefreshResult
            {
                TargetPid = process.Id,
                DesignerWindowTitle = designer.Title,
                DesignerFingerprint = designerFingerprint,
                SourceText = source.Source,
                Status = "ok"
            };
            return FinishRefresh(editor, process.Id, designer.Title, result, action);
        }
        catch (Exception ex)
        {
            return RefreshEmpty("error", ex.Message, process.Id);
        }
    }

    private static RefreshResult FinishRefresh(NativeWindow editor, int pid, string title, RefreshResult result, System.Threading.Thread action)
    {
        bool closed = CloseOwnedEditor(editor, pid);
        bool actionFinished = action == null || action.Join(2000);
        if (!closed)
            return RefreshEmpty("error", "The Show Query editor did not close; close it manually before trying again.", pid, title);
        if (!actionFinished)
            return RefreshEmpty("timeout", "Show Query did not finish closing its native editor; try again after it returns to SQLPathFinder.", pid, title);
        return result;
    }

    private static bool CloseOwnedEditor(NativeWindow editor, int pid)
    {
        if (editor == null || !IsWindow(editor.Handle)) return true;
        uint ownerPid;
        if (GetWindowThreadProcessId(editor.Handle, out ownerPid) == 0 || ownerPid != (uint)pid)
            return false;
        PostMessage(editor.Handle, WmClose, IntPtr.Zero, IntPtr.Zero);
        DateTime deadline = DateTime.UtcNow.AddMilliseconds(2000);
        while (DateTime.UtcNow < deadline && IsWindow(editor.Handle)) System.Threading.Thread.Sleep(50);
        return !IsWindow(editor.Handle);
    }

    private static Process FindRefreshTarget(int requestedPid, IntPtr requestedHandle, string document, out string error, out List<NativeWindow> windows, out NativeWindow designer)
    {
        error = null;
        windows = null;
        designer = null;
        if (requestedPid > 0)
        {
            Process process = FindTarget(requestedPid, out error);
            if (error != null || process == null) return process;
            windows = FindTopWindows(process.Id, process.MainWindowHandle);
            designer = FindDesigner(windows, process.Id, document, requestedHandle, out error);
            return error == null && designer != null ? process : null;
        }
        error = "An exact SQLPathFinder PID and designer window handle are required for refresh.";
        return null;
    }

    private static void InvokeShowQuery(IntPtr designerHandle)
    {
        // WinForms exposes submenu actions while closed; avoid popup focus/dismissal races.
        if (FindAndInvokeChildWindow(designerHandle, "SHOW QUERY")) return;
        object value;
        Guid iid = new Guid("618736e0-3c3d-11cf-810c-00aa00389b71");
        int hr = AccessibleObjectFromWindow(designerHandle, ObjidClient, ref iid, out value);
        Accessibility.IAccessible root = hr == 0 ? value as Accessibility.IAccessible : null;
        if (root != null && FindAndInvoke(root, "SHOW QUERY", 0)) return;
        throw new InvalidOperationException("Show Query is unavailable or disabled in the SQLPathFinder menu.");
    }

    private static bool FindAndInvoke(Accessibility.IAccessible accessible, string wanted, int depth)
    {
        AccessibleMatch match;
        if (!FindAccessible(accessible, wanted, depth, out match)) return false;
        match.Owner.accDoDefaultAction(match.Id);
        return true;
    }

    private static bool FindAccessible(Accessibility.IAccessible accessible, string wanted, int depth, out AccessibleMatch match)
    {
        match = null;
        if (accessible == null || depth > MaxAccessibleDepth) return false;
        int count = SafeAccessibleChildCount(accessible);
        for (int index = 1; index <= count; index++)
        {
            string name = NormalizeMenu(SafeAccessibleName(accessible, index));
            object child = SafeAccessibleChild(accessible, index);
            Accessibility.IAccessible nested = child as Accessibility.IAccessible;
            if (name == wanted && SafeAccessibleRole(accessible, index) == RoleMenuItem)
            {
                int state = SafeAccessibleState(accessible, index);
                if ((state & StateUnavailable) != 0) throw new InvalidOperationException("Show Query is disabled in the SQLPathFinder menu.");
                match = new AccessibleMatch { Owner = nested ?? accessible, Id = nested == null ? (object)index : 0 };
                return true;
            }
            if (nested != null && FindAccessible(nested, wanted, depth + 1, out match)) return true;
        }
        return false;
    }

    private static bool FindAndInvokeChildWindow(IntPtr parent, string wanted)
    {
        List<IntPtr> children = new List<IntPtr>();
        EnumChildWindows(parent, delegate (IntPtr handle, IntPtr state)
        {
            if (IsWindowVisible(handle)) children.Add(handle);
            return children.Count < 64;
        }, IntPtr.Zero);
        Guid iid = new Guid("618736e0-3c3d-11cf-810c-00aa00389b71");
        foreach (IntPtr handle in children)
        {
            object value;
            if (AccessibleObjectFromWindow(handle, ObjidClient, ref iid, out value) != 0) continue;
            Accessibility.IAccessible accessible = value as Accessibility.IAccessible;
            if (accessible != null && FindAndInvoke(accessible, wanted, 0)) return true;
        }
        return false;
    }

    private static string NormalizeMenu(string value)
    {
        if (String.IsNullOrWhiteSpace(value)) return "";
        string normalized = value.Replace("&", "").Trim();
        while (normalized.EndsWith(".", StringComparison.Ordinal)) normalized = normalized.Substring(0, normalized.Length - 1).Trim();
        return normalized.ToUpperInvariant();
    }
}
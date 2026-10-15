using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

[DataContract]
public sealed class SelectionResult
{
    [DataMember(Name = "text")] public string Text;
    [DataMember(Name = "truncated")] public bool Truncated;
    [DataMember(Name = "start", EmitDefaultValue = false)] public int? Start;
    [DataMember(Name = "end", EmitDefaultValue = false)] public int? End;
}

[DataContract]
public sealed class SnapshotResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "windowTitle")] public string WindowTitle;
    [DataMember(Name = "sourceText")] public string SourceText;
    [DataMember(Name = "sourceTruncated")] public bool SourceTruncated;
    [DataMember(Name = "selection")] public SelectionResult Selection;
    [DataMember(Name = "designerValues")] public List<string> DesignerValues;
    [DataMember(Name = "fingerprint")] public string Fingerprint;
    [DataMember(Name = "designerFingerprint", EmitDefaultValue = false)] public string DesignerFingerprint;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "designerBounds", EmitDefaultValue = false)] public int[] DesignerBounds;
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
    [DataMember(Name = "bounds", EmitDefaultValue = false)] public int[] Bounds;
}

[DataContract]
public sealed class RefreshResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "designerFingerprint")] public string DesignerFingerprint;
    [DataMember(Name = "sourceText")] public string SourceText;
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
}

[DataContract]
public sealed class NativeTargetResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "designerHwnd", EmitDefaultValue = false)] public long? DesignerHwnd;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "documentName")] public string DocumentName;
}

[DataContract]
public sealed class TargetListResult
{
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "targets")] public List<NativeTargetResult> Targets;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
}

public static class Program
{
    private const int MaxText = 200000;
    private const int MaxDesignerValues = 20;
    private const int MaxAccessibleDepth = 6;
    private const int StateSelected = 0x00000002;
    private const int StateFocused = 0x00000004;
    private const int StateUnavailable = 0x00000001;
    private const int RoleMenuItem = 12;
    private const uint ObjidClient = 0xfffffffc;
    private const int GwlStyle = -16;
    private const long EsMultiline = 0x0004;
    private const long EsPassword = 0x0020;
    private const uint WmClose = 0x0010;
    private const uint GwOwner = 4;
    private const uint GaRootOwner = 3;
    private const int RefreshTimeoutMs = 30000;

    private sealed class NativeWindow
    {
        public IntPtr Handle;
        public string Title;
        public string ClassName;
        public int[] Bounds;
    }

    private sealed class WindowObservation
    {
        public string Title;
        public string Source;
        public bool SourceTruncated;
        public SelectionResult Selection;
        public List<string> DesignerValues;
        public List<string> DesignerStateValues;
        public int[] Bounds;
        public bool HasEditor;
    }

    private sealed class SelectionRead
    {
        public List<string> Values = new List<string>();
    }

    private sealed class AccessibleMatch
    {
        public Accessibility.IAccessible Owner;
        public object Id;
    }

    public static int Main(string[] args)
    {
        int requestedPid = 0;
        long requestedHwnd = 0;
        string document = "";
        bool refresh = false;
        bool listTargets = false;
        for (int i = 0; i + 1 < args.Length; i++)
        {
            if (args[i] == "--pid") Int32.TryParse(args[i + 1], out requestedPid);
            if (args[i] == "--hwnd") Int64.TryParse(args[i + 1], out requestedHwnd);
            if (args[i] == "--document") document = args[i + 1];
        }
        foreach (string arg in args) if (arg == "--refresh") refresh = true;
        foreach (string arg in args) if (arg == "--targets") listTargets = true;
        if (listTargets)
        {
            TargetListResult targets = ListTargets();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TargetListResult));
            using (System.IO.Stream output = Console.OpenStandardOutput()) serializer.WriteObject(output, targets);
            return targets.Status == "ok" ? 0 : 2;
        }
        if (refresh)
        {
            RefreshResult result = Refresh(requestedPid, new IntPtr(requestedHwnd), document);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RefreshResult));
            using (System.IO.Stream output = Console.OpenStandardOutput()) serializer.WriteObject(output, result);
            return result.Status == "ok" ? 0 : 2;
        }
        SnapshotResult snapshot = Observe(requestedPid, new IntPtr(requestedHwnd));
        DataContractJsonSerializer snapshotSerializer = new DataContractJsonSerializer(typeof(SnapshotResult));
        using (System.IO.Stream output = Console.OpenStandardOutput()) snapshotSerializer.WriteObject(output, snapshot);
        return snapshot.Status == "ok" ? 0 : 2;
    }

    private static SnapshotResult Observe(int requestedPid, IntPtr requestedHandle)
    {
        string targetError;
        Process process = FindTarget(requestedPid, out targetError);
        if (targetError != null) return Empty("ambiguous", targetError);
        if (process == null) return Empty("missing", "SQLPathFinder3 (sqlpathfinder3) is not running.");
        try
        {
            if (process.HasExited) return Empty("exited", "The selected SQLPathFinder process exited.");
            List<NativeWindow> windows = FindTopWindows(process.Id, process.MainWindowHandle);
            NativeWindow main = FindDesigner(windows, process.Id, "", requestedHandle, out targetError);
            if (targetError != null) return Empty("ambiguous", targetError, process.Id);
            if (main == null) return Empty("inaccessible", "SQLPathFinder designer window is unavailable.", process.Id);
            NativeWindow editor = FindSourceEditor(windows, main.Handle);
            WindowObservation source = editor == null ? null : ReadSourceEditor(editor);
            WindowObservation designer = ReadDesigner(main);
            return new SnapshotResult
            {
                TargetPid = process.Id,
                WindowTitle = source == null ? main.Title : source.Title,
                SourceText = source == null ? "" : source.Source,
                SourceTruncated = source != null && source.SourceTruncated,
                Selection = source == null ? new SelectionResult { Text = "", Truncated = false } : source.Selection,
                DesignerValues = designer.DesignerValues,
                Bounds = source == null ? main.Bounds : source.Bounds,
                DesignerWindowTitle = main.Title,
                DesignerBounds = main.Bounds,
                Fingerprint = Fingerprint(source == null ? main.Title : source.Title, source == null ? "" : source.Source, new List<string>()),
                DesignerFingerprint = Fingerprint(main.Title, "", designer.DesignerStateValues),
                Status = "ok"
            };
        }
        catch (Exception ex)
        {
            return Empty("error", ex.Message, process.Id);
        }
    }

    private static RefreshResult Refresh(int requestedPid, IntPtr requestedHandle, string document)
    {
        if (requestedPid <= 0 || requestedHandle == IntPtr.Zero || String.IsNullOrWhiteSpace(document))
            return RefreshEmpty("inaccessible", "Select an exact SQLPathFinder process, designer window, and VG2 document.");
        string targetError;
        Process process = FindRefreshTarget(requestedPid, requestedHandle, document, out targetError);
        if (targetError != null) return RefreshEmpty("ambiguous", targetError);
        if (process == null) return RefreshEmpty("missing", "SQLPathFinder3 (sqlpathfinder3) is not running.");
        try
        {
            if (process.HasExited) return RefreshEmpty("exited", "The selected SQLPathFinder process exited.", process.Id);
            List<NativeWindow> windows = FindTopWindows(process.Id, process.MainWindowHandle);
            NativeWindow designer = FindDesigner(windows, process.Id, document, requestedHandle, out targetError);
            if (targetError != null) return RefreshEmpty("ambiguous", targetError, process.Id);
            if (designer == null) return RefreshEmpty("inaccessible", "Could not identify the SQLPathFinder designer for this VG2 document.", process.Id);
            List<NativeWindow> existingEditors = FindSourceEditors(windows, IntPtr.Zero);
            if (existingEditors.Count != 0)
                return RefreshEmpty("editor-open", "Close the existing Show Query editor, then press Update Python again.", process.Id, designer.Title);

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
                editor = FindSourceEditor(current, designer.Handle);
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
                designerFingerprint = Fingerprint(designer.Title, "", ReadDesigner(designer).DesignerStateValues);
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

    private static Process FindRefreshTarget(int requestedPid, IntPtr requestedHandle, string document, out string error)
    {
        error = null;
        if (requestedPid > 0)
        {
            Process process = FindTarget(requestedPid, out error);
            if (error != null || process == null) return process;
            List<NativeWindow> windows = FindTopWindows(process.Id, process.MainWindowHandle);
            NativeWindow designer = FindDesigner(windows, process.Id, document, requestedHandle, out error);
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
            Bounds = window.Bounds,
            HasEditor = true,
            DesignerValues = new List<string>(),
            DesignerStateValues = new List<string>()
        };
    }

    private static WindowObservation ReadDesigner(NativeWindow window)
    {
        return new WindowObservation
        {
            Title = window.Title, Source = "", SourceTruncated = false,
            Selection = new SelectionResult { Text = "", Truncated = false },
            DesignerValues = ReadDesignerSelection(window.Handle),
            DesignerStateValues = ReadDesignerState(window.Handle),
            Bounds = window.Bounds, HasEditor = false
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

    private static List<string> ReadDesignerSelection(IntPtr parent)
    {
        List<string> values = new List<string>();
        HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<IntPtr> trees = FindClassChildren(parent, "SysTreeView32", 4);
        foreach (IntPtr tree in trees)
        {
            SelectionRead read = new SelectionRead();
            foreach (string value in ThreadedSelection(tree, read))
            {
                if (seen.Add(value)) values.Add(value);
                if (values.Count >= MaxDesignerValues) return values;
            }
        }
        return values;
    }

    private static List<string> ThreadedSelection(IntPtr tree, SelectionRead read)
    {
        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
        {
            try
            {
                object value;
                Guid iid = new Guid("618736e0-3c3d-11cf-810c-00aa00389b71");
                int hr = AccessibleObjectFromWindow(tree, ObjidClient, ref iid, out value);
                if (hr != 0 || value == null) return;
                Accessibility.IAccessible accessible = value as Accessibility.IAccessible;
                if (accessible == null) return;
                bool actual = AppendAccessible(read.Values, accessible, accessible.accSelection);
                if (!actual) actual = AppendAccessible(read.Values, accessible, accessible.accFocus);
                if (!actual) ScanAccessibleStates(accessible, read.Values, 0);
                Marshal.ReleaseComObject(value);
            }
            catch (Exception) { }
        }));
        thread.IsBackground = true;
        thread.Start();
        if (!thread.Join(250)) return new List<string>();
        return read.Values;
    }

    private static bool AppendAccessible(List<string> values, Accessibility.IAccessible root, object selected)
    {
        if (selected == null || values.Count >= MaxDesignerValues) return false;
        Accessibility.IAccessible child = selected as Accessibility.IAccessible;
        object id = child == null ? selected : 0;
        if (child == null && Convert.ToInt32(id) == 0) return false;
        string name = child == null ? SafeAccessibleName(root, id) : SafeAccessibleName(child, 0);
        string value = child == null ? SafeAccessibleValue(root, id) : SafeAccessibleValue(child, 0);
        if (child != null && String.Equals(name, SafeAccessibleName(root, 0), StringComparison.OrdinalIgnoreCase)) return false;
        if (String.IsNullOrWhiteSpace(name)) return false;
        values.Add(Limit(name + (String.IsNullOrWhiteSpace(value) ? "" : "=" + value) + " focused", 1024));
        return true;
    }

    private static void ScanAccessibleStates(Accessibility.IAccessible accessible, List<string> values, int depth)
    {
        if (accessible == null || depth >= MaxAccessibleDepth || values.Count >= MaxDesignerValues) return;
        int count = SafeAccessibleChildCount(accessible);
        for (int index = 1; index <= count && index <= MaxDesignerValues && values.Count < MaxDesignerValues; index++)
        {
            object child = SafeAccessibleChild(accessible, index);
            int state = SafeAccessibleState(accessible, index);
            Accessibility.IAccessible nested = child as Accessibility.IAccessible;
            if (nested != null && state == 0) state = SafeAccessibleState(nested, 0);
            if ((state & (StateSelected | StateFocused)) != 0)
            {
                string name = SafeAccessibleName(accessible, index);
                string value = SafeAccessibleValue(accessible, index);
                if (nested != null && String.IsNullOrWhiteSpace(name)) name = SafeAccessibleName(nested, 0);
                if (nested != null && String.IsNullOrWhiteSpace(value)) value = SafeAccessibleValue(nested, 0);
                if (!String.IsNullOrWhiteSpace(name))
                    values.Add(Limit(name + (String.IsNullOrWhiteSpace(value) ? "" : "=" + value) + " focused", 1024));
            }
            if (nested != null)
            {
                ScanAccessibleStates(nested, values, depth + 1);
                Marshal.ReleaseComObject(child);
            }
        }
    }

    private static int SafeAccessibleChildCount(Accessibility.IAccessible accessible)
    {
        try { return Math.Max(0, Math.Min(accessible.accChildCount, MaxDesignerValues)); } catch (Exception) { return 0; }
    }

    private static object SafeAccessibleChild(Accessibility.IAccessible accessible, int index)
    {
        try { return accessible.get_accChild(index); } catch (Exception) { return null; }
    }

    private static int SafeAccessibleState(Accessibility.IAccessible accessible, int index)
    {
        try { return Convert.ToInt32(accessible.get_accState(index)); } catch (Exception) { return 0; }
    }

    private static int SafeAccessibleRole(Accessibility.IAccessible accessible, int index)
    {
        try { return Convert.ToInt32(accessible.get_accRole(index)); } catch (Exception) { return 0; }
    }

    private static string SafeAccessibleName(Accessibility.IAccessible accessible, object id)
    {
        try { return Convert.ToString(accessible.get_accName(id)) ?? ""; } catch (Exception) { return ""; }
    }

    private static string SafeAccessibleValue(Accessibility.IAccessible accessible, object id)
    {
        try { return Limit(Convert.ToString(accessible.get_accValue(id)), 512); } catch (Exception) { return ""; }
    }

    private static List<string> ReadDesignerState(IntPtr parent)
    {
        List<string> values = new List<string>();
        foreach (IntPtr handle in FindClassChildren(parent, "", 40))
        {
            string className = GetClass(handle);
            if (!IsNativeClass(className, "Edit") && !IsNativeClass(className, "ComboBox") && !IsNativeClass(className, "Button")) continue;
            if ((GetWindowStyle(handle) & EsPassword) != 0) continue;
            string value = ReadWindowText(handle, 512);
            if (!String.IsNullOrWhiteSpace(value)) values.Add(GetDlgCtrlID(handle).ToString() + "=" + value);
            if (values.Count >= MaxDesignerValues) break;
        }
        return values;
    }

    private static List<IntPtr> FindClassChildren(IntPtr parent, string wanted, int max)
    {
        List<IntPtr> result = new List<IntPtr>();
        EnumChildWindows(parent, delegate (IntPtr handle, IntPtr state)
        {
            if (IsWindowVisible(handle) && (String.IsNullOrEmpty(wanted) || IsNativeClass(GetClass(handle), wanted))) result.Add(handle);
            return result.Count < max;
        }, IntPtr.Zero);
        return result;
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

    private static SnapshotResult Empty(string status, string error, int pid = 0)
    {
        return new SnapshotResult
        {
            TargetPid = pid > 0 ? (int?)pid : null,
            WindowTitle = "", SourceText = "", Selection = new SelectionResult { Text = "", Truncated = false },
            DesignerValues = new List<string>(), Fingerprint = "", DesignerFingerprint = "", DesignerWindowTitle = "",
            Status = status, Error = error
        };
    }

    private static RefreshResult RefreshEmpty(string status, string error, int pid = 0, string title = "")
    {
        return new RefreshResult
        {
            TargetPid = pid > 0 ? (int?)pid : null,
            DesignerWindowTitle = title ?? "",
            SourceText = "",
            Status = status,
            Error = error
        };
    }

    private static string Limit(string value, int max)
    {
        if (value == null) return "";
        return value.Length > max ? value.Substring(0, max) : value;
    }

    private static string Fingerprint(string title, string source, List<string> values)
    {
        StringBuilder input = new StringBuilder(title ?? ""); input.Append('\n').Append(source ?? "");
        foreach (string value in values) input.Append('\n').Append(value);
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            foreach (byte item in bytes) result.Append(item.ToString("x2"));
            return result.ToString();
        }
    }

    private delegate bool EnumWindowsProc(IntPtr handle, IntPtr state);
    private delegate bool EnumChildWindowsProc(IntPtr handle, IntPtr state);
    [StructLayout(LayoutKind.Sequential)] private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }

    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc callback, IntPtr state);
    [DllImport("user32.dll")] private static extern bool EnumChildWindows(IntPtr parent, EnumChildWindowsProc callback, IntPtr state);
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr handle, out uint processId);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr handle);
    [DllImport("user32.dll")] private static extern bool IsWindow(IntPtr handle);
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

public static partial class Program
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
    private const long EsReadOnly = 0x0800;
    private const uint WmClose = 0x0010;
    private const uint TvmGetNextItem = 0x110a;
    private const uint TvmMapHtreeItemToAccId = 0x112b;
    private const int TvgnCaret = 9;
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
        public IntPtr FocusHandle;
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
        bool serve = false;
        for (int i = 0; i + 1 < args.Length; i++)
        {
            if (args[i] == "--pid") Int32.TryParse(args[i + 1], out requestedPid);
            if (args[i] == "--hwnd") Int64.TryParse(args[i + 1], out requestedHwnd);
            if (args[i] == "--document") document = args[i + 1];
        }
        foreach (string arg in args) if (arg == "--refresh") refresh = true;
        foreach (string arg in args) if (arg == "--targets") listTargets = true;
        foreach (string arg in args) if (arg == "--serve") serve = true;
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
        if (serve) return Serve();
        SnapshotResult snapshot = Observe(requestedPid, new IntPtr(requestedHwnd));
        DataContractJsonSerializer snapshotSerializer = new DataContractJsonSerializer(typeof(SnapshotResult));
        using (System.IO.Stream output = Console.OpenStandardOutput()) snapshotSerializer.WriteObject(output, snapshot);
        return snapshot.Status == "ok" ? 0 : 2;
    }

    private static int Serve()
    {
        using (System.IO.Stream output = Console.OpenStandardOutput())
        {
            string line;
            while ((line = Console.In.ReadLine()) != null)
            {
                if (String.Equals(line.Trim(), "exit", StringComparison.Ordinal)) break;
                string[] parts = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                int pid;
                long hwnd;
                SnapshotResult snapshot;
                if (parts.Length == 3 && parts[0] == "poll"
                    && Int32.TryParse(parts[1], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out pid)
                    && Int64.TryParse(parts[2], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out hwnd))
                {
                    try { snapshot = Observe(pid, new IntPtr(hwnd)); }
                    catch (Exception ex) { snapshot = Empty("error", ex.Message); }
                }
                else snapshot = Empty("error", "Unsupported serve request.");
                WriteSnapshotLine(snapshot, output);
            }
        }
        return 0;
    }

    private static void WriteSnapshotLine(SnapshotResult snapshot, System.IO.Stream output)
    {
        using (System.IO.MemoryStream buffer = new System.IO.MemoryStream())
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SnapshotResult));
            serializer.WriteObject(buffer, snapshot);
            byte[] payload = buffer.ToArray();
            output.Write(payload, 0, payload.Length);
            output.WriteByte(0x0A);
            output.Flush();
        }
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
                ActiveControl = ActiveControl(main.Handle, source),
                Bounds = source == null ? main.Bounds : source.Bounds,
                DesignerWindowTitle = main.Title,
                DesignerBounds = main.Bounds,
                Fingerprint = Fingerprint(source == null ? main.Title : source.Title, source == null ? "" : source.Source, new List<string>()),
                DesignerFingerprint = Fingerprint(DesignerDocument(main.Title), "", designer.DesignerStateValues),
                Status = "ok"
            };
        }
        catch (Exception ex)
        {
            return Empty("error", ex.Message, process.Id);
        }
    }

    private static SnapshotResult Empty(string status, string error, int pid = 0)
    {
        return new SnapshotResult
        {
            TargetPid = pid > 0 ? (int?)pid : null,
            WindowTitle = "", SourceText = "", Selection = new SelectionResult { Text = "", Truncated = false },
            DesignerValues = new List<string>(), ActiveControl = "unknown", Fingerprint = "", DesignerFingerprint = "", DesignerWindowTitle = "",
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
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static partial class Program
{
    private const int MaxAccessibleNodes = 400;

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

    private static string ActiveControl(IntPtr designer, WindowObservation source)
    {
        GUITHREADINFO info = new GUITHREADINFO();
        info.cbSize = Marshal.SizeOf(typeof(GUITHREADINFO));
        if (!GetGUIThreadInfo(0, ref info) || info.hwndFocus == IntPtr.Zero) return "unknown";
        if (source != null && source.FocusHandle != IntPtr.Zero &&
            (info.hwndFocus == source.FocusHandle || IsChild(source.FocusHandle, info.hwndFocus)))
            return "editor";
        if (info.hwndFocus == designer || IsChild(designer, info.hwndFocus)) return "designer";
        return "unknown";
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
                bool actual = AppendTreeCaret(read.Values, accessible, tree);
                if (!actual) actual = AppendAccessible(read.Values, accessible, accessible.accSelection);
                if (!actual) actual = AppendAccessible(read.Values, accessible, accessible.accFocus);
                                if (!actual)
                                {
                                    int remainingNodes = MaxAccessibleNodes;
                                    ScanAccessibleStates(accessible, read.Values, 0, ref remainingNodes);
                                }
                Marshal.ReleaseComObject(value);
            }
            catch (Exception) { }
        }));
        thread.IsBackground = true;
        thread.Start();
        if (!thread.Join(750)) return new List<string>();
        return read.Values;
    }

    private static bool AppendTreeCaret(List<string> values, Accessibility.IAccessible root, IntPtr tree)
    {
        IntPtr item;
        IntPtr result;
        if (SendMessageTimeoutValue(tree, TvmGetNextItem, new IntPtr(TvgnCaret), IntPtr.Zero, 0x0002, 200, out item) == IntPtr.Zero || item == IntPtr.Zero)
            return false;
        if (SendMessageTimeoutValue(tree, TvmMapHtreeItemToAccId, item, IntPtr.Zero, 0x0002, 200, out result) == IntPtr.Zero || result == IntPtr.Zero)
            return false;
        long childId = result.ToInt64();
        return childId > 0 && childId <= Int32.MaxValue && AppendAccessible(values, root, (int)childId);
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

    private static void ScanAccessibleStates(Accessibility.IAccessible accessible, List<string> values, int depth, ref int remainingNodes)
    {
        if (accessible == null || depth >= MaxAccessibleDepth || values.Count >= MaxDesignerValues || remainingNodes <= 0) return;
        int count = SafeAccessibleChildCount(accessible);
        for (int index = 1; index <= count && index <= MaxDesignerValues && values.Count < MaxDesignerValues && remainingNodes > 0; index++)
        {
            remainingNodes--;
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
                ScanAccessibleStates(nested, values, depth + 1, ref remainingNodes);
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
            if (IsNativeClass(className, "Edit") && (GetWindowStyle(handle) & EsReadOnly) != 0) continue;
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
            if (IsWindowVisible(handle) && EnabledWithin(handle, parent)
                && (String.IsNullOrEmpty(wanted) || IsNativeClass(GetClass(handle), wanted))) result.Add(handle);
            return result.Count < max;
        }, IntPtr.Zero);
        return result;
    }

    private static bool EnabledWithin(IntPtr handle, IntPtr parent)
    {
        // Disabled MDI ancestors keep visible trees whose caret belongs to the parent query.
        for (IntPtr current = handle; current != IntPtr.Zero && current != parent; current = GetParent(current))
            if (!IsWindowEnabled(current)) return false;
        return true;
    }
}
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        bool unowned = args.Length > 3 && args[3] == "unowned";
        bool nestedTree = args.Length > 3 && args[3] == "nested-tree";
        Form designer = new Designer(args[0], args[1], args[2], unowned, nestedTree || (args.Length > 3 && args[3] == "tree"), nestedTree);
        if (args.Length > 3 && args[3] == "existing")
            designer.Shown += delegate { designer.BeginInvoke((Action)delegate { ((Designer)designer).OpenEditor(); }); };
        if (args.Length > 3 && args[3] == "other-editor")
            designer.Shown += delegate
            {
                Designer other = new Designer("other.vg2", args[1], args[2], false, false);
                other.Show();
                other.BeginInvoke((Action)delegate { other.OpenEditor(); });
            };
        Application.Run(designer);
    }

    private sealed class Designer : Form
    {
        private readonly string sourcePath;
        private readonly string logPath;
        private readonly bool unowned;
        private readonly TreeView steps;
        private Form editor;

        internal Designer(string name, string source, string log, bool unowned, bool tree, bool nestedTree = false)
        {
            sourcePath = source;
            logPath = log;
            this.unowned = unowned;
            Text = "SQLPathFinder 3 - [" + name + " (L1)]";
            StartPosition = FormStartPosition.Manual;
            Location = new Point(-2000, -2000);
            Size = new Size(800, 500);
            ShowInTaskbar = false;
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem file = new ToolStripMenuItem("&File");
            file.DropDownOpening += delegate { File.AppendAllText(logPath, "menu-opened\n"); };
            ToolStripMenuItem query = new ToolStripMenuItem("Show &Query...");
            query.Click += delegate { OpenEditor(); };
            file.DropDownItems.Add(query);
            menu.Items.Add(file);
            MainMenuStrip = menu;
            Controls.Add(menu);
            if (tree)
            {
                TextBox description = new TextBox { ReadOnly = true, Dock = DockStyle.Bottom, Text = "First step description" };
                Controls.Add(description);
                if (nestedTree)
                {
                    Panel disabled = new Panel { Enabled = false, Dock = DockStyle.Right, Width = 200 };
                    TreeView stale = new TreeView { Dock = DockStyle.Fill };
                    stale.Nodes.Add("6. Pre/Post Query {inactive_parent}");
                    stale.SelectedNode = stale.Nodes[0];
                    disabled.Controls.Add(stale);
                    Controls.Add(disabled);
                }
                steps = new TreeView { Dock = DockStyle.Fill };
                steps.Nodes.Add("15.0. Text (SQLite)");
                steps.Nodes.Add("15.1. Next step");
                steps.AfterSelect += delegate { description.Text = steps.SelectedNode.Text; };
                Controls.Add(steps);
            }
            Shown += delegate { File.AppendAllText(logPath, "ready\n"); };
            if (tree) Shown += delegate
            {
                steps.SelectedNode = steps.Nodes[0];
                Timer timer = new Timer { Interval = 50 };
                timer.Tick += delegate
                {
                    if (!File.Exists(logPath + ".next")) return;
                    steps.SelectedNode = steps.Nodes[1];
                    Text = "SQLPathFinder 3 (Pre/Post Query - parent.vg2 (L0) - child - - 31154) - [" + name + " (L1)]";
                    timer.Stop(); timer.Dispose();
                };
                timer.Start();
            };
        }

        internal void OpenEditor()
        {
            if (editor != null) return;
            editor = new Form();
            editor.Text = "Editor - [Untitled]";
            editor.StartPosition = FormStartPosition.Manual;
            editor.Location = new Point(-1900, -1900);
            editor.ShowInTaskbar = false;
            editor.Controls.Add(new TextBox
            {
                Multiline = true, Dock = DockStyle.Fill,
                Text = File.ReadAllText(sourcePath), WordWrap = false
            });
            editor.Shown += delegate { File.AppendAllText(logPath, "created\n"); };
            editor.FormClosed += delegate { editor = null; File.AppendAllText(logPath, "closed\n"); };
            if (unowned) editor.Show();
            else editor.ShowDialog(this);
        }
    }
}

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
        Form designer = new Designer(args[0], args[1], args[2]);
        if (args.Length > 3 && args[3] == "existing")
            designer.Shown += delegate { designer.BeginInvoke((Action)delegate { ((Designer)designer).OpenEditor(); }); };
        if (args.Length > 3 && args[3] == "other-editor")
            designer.Shown += delegate
            {
                Designer other = new Designer("other.vg2", args[1], args[2]);
                other.Show();
                other.BeginInvoke((Action)delegate { other.OpenEditor(); });
            };
        Application.Run(designer);
    }

    private sealed class Designer : Form
    {
        private readonly string sourcePath;
        private readonly string logPath;

        internal Designer(string name, string source, string log)
        {
            sourcePath = source;
            logPath = log;
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
            Shown += delegate { File.AppendAllText(logPath, "ready\n"); };
        }

        internal void OpenEditor()
        {
            using (Form editor = new Form())
            {
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
                editor.FormClosed += delegate { File.AppendAllText(logPath, "closed\n"); };
                // Match SQLPathFinder's parameterless modal Show Query editor.
                editor.ShowDialog();
            }
        }
    }
}

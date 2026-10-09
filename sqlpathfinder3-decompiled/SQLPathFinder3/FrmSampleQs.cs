using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmSampleQs : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelpSQ")]
	private ToolStripMenuItem _mnuHelpSQ;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuExit")]
	private ToolStripMenuItem _mnuExit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TreeQs")]
	private TreeView _TreeQs;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("FileList")]
	private ListBox _FileList;

	private string InitPath;

	private const string l_MyTxt = "Select a folder to see available Queries or documentation. Double-Click a query or file to load it";

	private short MaxSamples;

	private string fPath;

	public virtual ToolStripMenuItem mnuHelpSQ
	{
		[CompilerGenerated]
		get
		{
			return _mnuHelpSQ;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpSQ_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHelpSQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHelpSQ = value;
			toolStripMenuItem = _mnuHelpSQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuExit
	{
		[CompilerGenerated]
		get
		{
			return _mnuExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExit_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuExit = value;
			toolStripMenuItem = _mnuExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MainMenu1")]
	public virtual MenuStrip MainMenu1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblDescription")]
	public virtual TextBox LblDescription
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual TreeView TreeQs
	{
		[CompilerGenerated]
		get
		{
			return _TreeQs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = TreeQs_AfterCollapse;
			TreeViewEventHandler value3 = TreeQs_AfterExpand;
			KeyPressEventHandler value4 = TreeQs_KeyPress;
			TreeNodeMouseClickEventHandler value5 = TreeQs_NodeClick;
			TreeView treeView = _TreeQs;
			if (treeView != null)
			{
				treeView.AfterCollapse -= value2;
				treeView.AfterExpand -= value3;
				treeView.KeyPress -= value4;
				treeView.NodeMouseClick -= value5;
			}
			_TreeQs = value;
			treeView = _TreeQs;
			if (treeView != null)
			{
				treeView.AfterCollapse += value2;
				treeView.AfterExpand += value3;
				treeView.KeyPress += value4;
				treeView.NodeMouseClick += value5;
			}
		}
	}

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ListBox FileList
	{
		[CompilerGenerated]
		get
		{
			return _FileList;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = FileList_DoubleClick;
			KeyPressEventHandler value3 = FileList_KeyPress;
			ListBox listBox = _FileList;
			if (listBox != null)
			{
				listBox.DoubleClick -= value2;
				listBox.KeyPress -= value3;
			}
			_FileList = value;
			listBox = _FileList;
			if (listBox != null)
			{
				listBox.DoubleClick += value2;
				listBox.KeyPress += value3;
			}
		}
	}

	[DebuggerNonUserCode]
	public FrmSampleQs()
	{
		base.Load += FrmSampleQs_Load;
		base.FormClosed += FrmSampleQs_FormClosed;
		InitPath = "";
		MaxSamples = 0;
		fPath = "";
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool Disposing)
	{
		if (Disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(Disposing);
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmSampleQs));
		this.MainMenu1 = new System.Windows.Forms.MenuStrip();
		this.mnuHelpSQ = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.LblDescription = new System.Windows.Forms.TextBox();
		this.TreeQs = new System.Windows.Forms.TreeView();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.FileList = new System.Windows.Forms.ListBox();
		this.MainMenu1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.MainMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuHelpSQ, this.mnuExit });
		this.MainMenu1.Location = new System.Drawing.Point(0, 0);
		this.MainMenu1.Name = "MainMenu1";
		this.MainMenu1.Size = new System.Drawing.Size(672, 28);
		this.MainMenu1.TabIndex = 11;
		this.mnuHelpSQ.Name = "mnuHelpSQ";
		this.mnuHelpSQ.Size = new System.Drawing.Size(55, 24);
		this.mnuHelpSQ.Text = "&Help";
		this.mnuExit.Name = "mnuExit";
		this.mnuExit.Size = new System.Drawing.Size(47, 24);
		this.mnuExit.Text = "&Exit";
		this.LblDescription.AcceptsReturn = true;
		this.LblDescription.BackColor = System.Drawing.SystemColors.Control;
		this.LblDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.LblDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.LblDescription.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.LblDescription.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
		this.LblDescription.Location = new System.Drawing.Point(0, 477);
		this.LblDescription.MaxLength = 0;
		this.LblDescription.Multiline = true;
		this.LblDescription.Name = "LblDescription";
		this.LblDescription.ReadOnly = true;
		this.LblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.LblDescription.Size = new System.Drawing.Size(672, 30);
		this.LblDescription.TabIndex = 2;
		this.LblDescription.TabStop = false;
		this.TreeQs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TreeQs.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TreeQs.FullRowSelect = true;
		this.TreeQs.HotTracking = true;
		this.TreeQs.Location = new System.Drawing.Point(0, 0);
		this.TreeQs.Name = "TreeQs";
		this.TreeQs.Size = new System.Drawing.Size(283, 473);
		this.TreeQs.TabIndex = 0;
		this.SplitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SplitContainer1.Location = new System.Drawing.Point(5, 35);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.TreeQs);
		this.SplitContainer1.Panel2.Controls.Add(this.FileList);
		this.SplitContainer1.Size = new System.Drawing.Size(660, 473);
		this.SplitContainer1.SplitterDistance = 283;
		this.SplitContainer1.TabIndex = 12;
		this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FileList.FormattingEnabled = true;
		this.FileList.ItemHeight = 16;
		this.FileList.Location = new System.Drawing.Point(0, 0);
		this.FileList.Name = "FileList";
		this.FileList.Size = new System.Drawing.Size(373, 473);
		this.FileList.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(672, 507);
		base.Controls.Add(this.LblDescription);
		base.Controls.Add(this.MainMenu1);
		base.Controls.Add(this.SplitContainer1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(11, 37);
		base.Name = "FrmSampleQs";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Select a Sample Query";
		this.MainMenu1.ResumeLayout(false);
		this.MainMenu1.PerformLayout();
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Load_FileList()
	{
		int num = 0;
		string text = "";
		try
		{
			FileList.Items.Clear();
			if (Operators.CompareString(Strings.UCase(fPath), Strings.UCase(InitPath), TextCompare: false) == 0)
			{
				return;
			}
			foreach (string file in MyProject.Computer.FileSystem.GetFiles(fPath, SearchOption.SearchTopLevelOnly))
			{
				string text2 = file;
				num = Strings.InStrRev(text2, "\\");
				if (num != 0)
				{
					text2 = Strings.Mid(text2, checked(num + 1));
				}
				if (Operators.CompareString(Strings.Mid(text2 + " ", 1, 1), "~", TextCompare: false) != 0)
				{
					FileList.Items.Add(text2);
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error loading Files: " + ex2.Message + ")", MsgBoxStyle.Exclamation, "File Error");
			ProjectData.ClearProjectError();
		}
		finally
		{
		}
	}

	public void DoNodeClick(ref TreeNode Node)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 332:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000b;
						case 3:
							goto IL_0013;
						case 4:
							goto IL_0020;
						case 5:
							goto IL_002f;
						case 6:
							goto IL_004c;
						case 7:
							goto IL_005a;
						case 8:
							goto IL_0063;
						case 9:
							goto IL_006c;
						case 10:
							goto IL_0080;
						case 11:
							goto IL_00a1;
						case 12:
							goto IL_00b1;
						case 14:
							goto IL_00c0;
						case 13:
						case 15:
						case 16:
						case 17:
							goto IL_00ce;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00b1:
					num2 = 12;
					Node.Collapse();
					goto IL_00ce;
					IL_00c0:
					num2 = 14;
					Node.Expand();
					goto IL_00ce;
					IL_00a1:
					num2 = 11;
					if (Node.IsExpanded)
					{
						goto IL_00b1;
					}
					goto IL_00c0;
					IL_00ce:
					num2 = 17;
					TreeQs.Focus();
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0020;
					IL_0020:
					num2 = 4;
					text = Strings.Trim(Node.Name);
					goto IL_002f;
					IL_002f:
					num2 = 5;
					if (Operators.CompareString(Strings.Right(text, 1), "\\", TextCompare: false) != 0)
					{
						goto IL_004c;
					}
					goto IL_005a;
					IL_004c:
					num2 = 6;
					text += "\\";
					goto IL_005a;
					IL_005a:
					num2 = 7;
					fPath = text;
					goto IL_0063;
					IL_0063:
					num2 = 8;
					Load_FileList();
					goto IL_006c;
					IL_006c:
					num2 = 9;
					LblDescription.Text = "Select a folder to see available Queries or documentation. Double-Click a query or file to load it";
					goto IL_0080;
					IL_0080:
					num2 = 10;
					if (Node != TreeQs.Nodes[0])
					{
						goto IL_00a1;
					}
					goto IL_00ce;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 332;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public short LoadSampleQuery(string MyPath, ref string FileName)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short num5 = default(short);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string lpFile = default(string);
		short result = default(short);
		int num6 = default(int);
		string text3 = default(string);
		string PackedExe = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				string text2;
				bool flag;
				int hwnd;
				string lpOperation;
				string lpParameters;
				int num7;
				short design_Mode;
				short num8;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					num5 = 0;
					goto IL_0007;
				case 1687:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
						case 3:
							break;
						case 1:
							goto IL_0563;
						default:
							goto end_IL_0001;
						}
						goto IL_04f1;
					}
					IL_0185:
					num2 = 24;
					MyProject.Forms.FrmMain.DoRunQuery_M();
					break;
					IL_02ac:
					num2 = 40;
					MyProject.Forms.FrmMain.DoRunQuery_M();
					break;
					IL_0289:
					num2 = 39;
					if (Operators.CompareString(text, "VGE", TextCompare: false) != 0 || Globals_Renamed.g_FrmIdx != 0)
					{
						break;
					}
					goto IL_02ac;
					IL_0563:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_0007;
					case 3:
						goto IL_000c;
					case 4:
						goto IL_0015;
					case 5:
						goto IL_001a;
					case 6:
						goto IL_001f;
					case 7:
						goto IL_0028;
					case 8:
						goto IL_0031;
					case 9:
						goto IL_003a;
					case 10:
						goto IL_0044;
					case 11:
						goto IL_004a;
					case 12:
						goto IL_0052;
					case 13:
						goto IL_0057;
					case 14:
						goto IL_006e;
					case 15:
						goto IL_0084;
					case 16:
						goto IL_0098;
					case 17:
						goto IL_00b2;
					case 18:
						goto IL_00f0;
					case 20:
						goto IL_010e;
					case 21:
						goto IL_0125;
					case 22:
						goto IL_0144;
					case 23:
						goto IL_0162;
					case 24:
						goto IL_0185;
					case 27:
						goto IL_01a0;
					case 28:
						goto IL_01a9;
					case 29:
						goto IL_01b3;
					case 30:
						goto IL_01d1;
					case 31:
						goto IL_01e5;
					case 32:
						goto IL_01ee;
					case 33:
						goto IL_0209;
					case 34:
						goto IL_021f;
					case 35:
						goto IL_022d;
					case 36:
						goto IL_0235;
					case 37:
						goto IL_024c;
					case 38:
						goto IL_026b;
					case 39:
						goto IL_0289;
					case 40:
						goto IL_02ac;
					case 45:
						goto IL_02cc;
					case 46:
						goto IL_0372;
					case 47:
						goto IL_0380;
					case 48:
						goto IL_03bc;
					case 50:
						goto IL_03c7;
					case 51:
						goto IL_03e0;
					case 52:
						goto IL_03f1;
					case 53:
						goto IL_0406;
					case 55:
						goto IL_040f;
					case 58:
						goto IL_0430;
					case 59:
						goto IL_04a3;
					case 60:
						goto IL_04b1;
					case 61:
						goto IL_04ca;
					case 63:
						goto IL_04d3;
					case 64:
						goto IL_04e8;
					case 67:
						goto IL_04f1;
					case 68:
						goto IL_0531;
					case 69:
						goto IL_053f;
					case 70:
						goto IL_0548;
					case 19:
					case 25:
					case 26:
					case 41:
					case 42:
					case 43:
					case 44:
					case 49:
					case 54:
					case 56:
					case 57:
					case 62:
					case 65:
					case 66:
					case 71:
					case 72:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 73:
						goto end_IL_0001_3;
					}
					goto default;
					IL_04f1:
					num2 = 67;
					Interaction.MsgBox("Error loading query : " + lpFile + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Load Error");
					goto IL_0531;
					IL_0531:
					num2 = 68;
					Information.Err().Clear();
					goto IL_053f;
					IL_053f:
					ProjectData.ClearProjectError();
					num3 = -3;
					goto IL_0548;
					IL_0548:
					num2 = 70;
					result = 0;
					break;
					IL_0007:
					num2 = 2;
					num6 = 0;
					goto IL_000c;
					IL_000c:
					num2 = 3;
					text = "";
					goto IL_0015;
					IL_0015:
					num2 = 4;
					num7 = 0;
					goto IL_001a;
					IL_001a:
					num2 = 5;
					num8 = 0;
					goto IL_001f;
					IL_001f:
					num2 = 6;
					text2 = "";
					goto IL_0028;
					IL_0028:
					num2 = 7;
					lpFile = "";
					goto IL_0031;
					IL_0031:
					num2 = 8;
					text3 = "";
					goto IL_003a;
					IL_003a:
					num2 = 9;
					PackedExe = "";
					goto IL_0044;
					IL_0044:
					num2 = 10;
					flag = false;
					goto IL_004a;
					IL_004a:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_0052;
					IL_0052:
					num2 = 12;
					result = 1;
					goto IL_0057;
					IL_0057:
					num2 = 13;
					lpFile = Strings.Trim(MyPath) + Strings.Trim(FileName);
					goto IL_006e;
					IL_006e:
					num2 = 14;
					text3 = Globals_Renamed.MyPCDir + Strings.Trim(FileName);
					goto IL_0084;
					IL_0084:
					num2 = 15;
					num5 = checked((short)Strings.InStrRev(lpFile, "."));
					goto IL_0098;
					IL_0098:
					num2 = 16;
					text = Strings.Trim(Strings.UCase(Strings.Mid(lpFile, checked(num5 + 1))));
					goto IL_00b2;
					IL_00b2:
					num2 = 17;
					if (Operators.CompareString(text, "VGQ", TextCompare: false) == 0 || Operators.CompareString(text, "VG2", TextCompare: false) == 0 || Operators.CompareString(text, "VGE", TextCompare: false) == 0)
					{
						goto IL_00f0;
					}
					goto IL_02cc;
					IL_010e:
					num2 = 20;
					num8 = MyProject.Forms.FrmMain.OpenQuery(lpFile, 1);
					goto IL_0125;
					IL_0125:
					num2 = 21;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle = text3;
					goto IL_0144;
					IL_02cc:
					num2 = 45;
					switch (text)
					{
					case "DOC":
					case "DOCX":
					case "XLS":
					case "RTF":
					case "PPT":
					case "PPTX":
					case "HTM":
					case "CSV":
					case "URL":
						goto IL_0372;
					}
					if (Operators.CompareString(text, "TXT", TextCompare: false) == 0)
					{
						goto IL_0372;
					}
					goto IL_03c7;
					IL_0144:
					num2 = 22;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveAs = true;
					goto IL_0162;
					IL_0372:
					num2 = 46;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0380;
					IL_0380:
					num2 = 47;
					hwnd = MyProject.Forms.FrmMain.Handle.ToInt32();
					lpOperation = "Open";
					lpParameters = "";
					num7 = Globals_Renamed.ShellExecute(hwnd, ref lpOperation, ref lpFile, ref lpParameters, ref Globals_Renamed.MyPCDir, 1);
					goto IL_03bc;
					IL_03bc:
					num2 = 48;
					result = 0;
					break;
					IL_03c7:
					num2 = 50;
					if (Operators.CompareString(text, "SPF", TextCompare: false) == 0)
					{
						goto IL_03e0;
					}
					goto IL_0430;
					IL_03e0:
					num2 = 51;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_03f1;
					}
					goto IL_040f;
					IL_03f1:
					num2 = 52;
					Interaction.MsgBox("You cannot load a Multi Script Sample Query (.spf) while building a Pre/Post Query", MsgBoxStyle.Information, "Invalid Action");
					goto IL_0406;
					IL_0406:
					num2 = 53;
					result = 0;
					break;
					IL_040f:
					num2 = 55;
					result = MyProject.Forms.FrmMain.Invoke_MQ_External(lpFile, text3, ref PackedExe);
					break;
					IL_0430:
					num2 = 58;
					if ((Operators.CompareString(text, ".VA", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(text), "VA", TextCompare: false) == 0) | (Operators.CompareString(text, "JSL", TextCompare: false) == 0) | (Operators.CompareString(text, "BAT", TextCompare: false) == 0) | (Operators.CompareString(text, "CMD", TextCompare: false) == 0) | (Operators.CompareString(text, "TXT", TextCompare: false) == 0))
					{
						goto IL_04a3;
					}
					goto IL_04d3;
					IL_04a3:
					num2 = 59;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_04b1;
					IL_04b1:
					num2 = 60;
					num7 = Interaction.Shell("Notepad.exe " + lpFile, AppWinStyle.NormalFocus);
					goto IL_04ca;
					IL_04ca:
					num2 = 61;
					result = 0;
					break;
					IL_04d3:
					num2 = 63;
					Interaction.MsgBox("SQLPathFinder cannot load this File..", MsgBoxStyle.Information, "Unrecognized Extension");
					goto IL_04e8;
					IL_04e8:
					num2 = 64;
					result = 0;
					break;
					IL_00f0:
					num2 = 18;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_010e;
					}
					if (design_Mode != 2)
					{
						break;
					}
					goto IL_01a0;
					IL_0162:
					num2 = 23;
					if (Operators.CompareString(text, "VGE", TextCompare: false) != 0 || Globals_Renamed.g_FrmIdx != 0)
					{
						break;
					}
					goto IL_0185;
					IL_01a0:
					num2 = 27;
					Globals_Renamed.QueryCancel = 0;
					goto IL_01a9;
					IL_01a9:
					num2 = 28;
					num6 = Globals_Renamed.g_FrmIdx;
					goto IL_01b3;
					IL_01b3:
					num2 = 29;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_01d1;
					IL_01d1:
					num2 = 30;
					if (Globals_Renamed.QueryCancel != 0)
					{
						break;
					}
					goto IL_01e5;
					IL_01e5:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_01ee;
					IL_01ee:
					num2 = 32;
					MyProject.Forms.FrmMain.FrmSQLQuery[num6].Dispose();
					goto IL_0209;
					IL_0209:
					num2 = 33;
					MyProject.Forms.FrmMain.FrmSQLQuery[num6] = null;
					goto IL_021f;
					IL_021f:
					num2 = 34;
					Information.Err().Clear();
					goto IL_022d;
					IL_022d:
					ProjectData.ClearProjectError();
					num3 = 3;
					goto IL_0235;
					IL_0235:
					num2 = 36;
					num8 = MyProject.Forms.FrmMain.OpenQuery(lpFile, 1);
					goto IL_024c;
					IL_024c:
					num2 = 37;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle = text3;
					goto IL_026b;
					IL_026b:
					num2 = 38;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveAs = true;
					goto IL_0289;
					end_IL_0001_2:
					break;
				}
				num2 = 72;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1687;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private void FileList_DoubleClick(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string FileName = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 181:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000b;
						case 4:
							goto IL_001f;
						case 5:
							goto IL_0028;
						case 6:
							goto IL_003c;
						case 7:
							goto IL_004d;
						case 8:
							goto IL_005a;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003c:
					num2 = 6;
					num5 = LoadSampleQuery(fPath, ref FileName);
					goto IL_004d;
					IL_004d:
					num2 = 7;
					Cursor.Current = Cursors.Default;
					goto IL_005a;
					IL_0028:
					num2 = 5;
					FileName = Conversions.ToString(FileList.SelectedItem);
					goto IL_003c;
					IL_005a:
					num2 = 8;
					if (num5 != 1)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if (BuildForm.Test_for_PPQ("load"))
					{
						goto end_IL_0001_3;
					}
					goto IL_001f;
					IL_001f:
					num2 = 4;
					FileName = "";
					goto IL_0028;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				mnuExit_Click(mnuExit, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 181;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FileList_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short num5 = default(short);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					num5 = checked((short)Strings.Asc(eventArgs.KeyChar));
					goto IL_0011;
				case 146:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0011;
						case 3:
							goto IL_001a;
						case 4:
							goto IL_0027;
						case 5:
							goto IL_003b;
						case 6:
							goto IL_004a;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0027:
					num2 = 4;
					FileList_DoubleClick(FileList, new EventArgs());
					goto IL_003b;
					IL_003b:
					num2 = 5;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_004a;
					IL_001a:
					num2 = 3;
					if (num5 == 13)
					{
						goto IL_0027;
					}
					goto IL_003b;
					IL_004a:
					num2 = 6;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0011:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001a;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				eventArgs.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 146;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void FrmSampleQs_Load(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		string[] DynArray = default(string[]);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						TreeQs.ImageList = MyProject.Forms.FrmMain.gImageList;
						int num3 = 0;
						text = "";
						int num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text2 = "";
						string text3 = "";
						TreeNode treeNode = null;
						InitPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\SampleQueries\\SQLPathFinder_Queries\\";
						text3 = InitPath + "SQLPATHFINDER.QUERIES1";
						num5 = (short)Strings.Len(InitPath);
						MaxSamples = 0;
						num4 = 0;
						MaxSamples = (short)num4;
						num3 = Microsoft.VisualBasic.FileSystem.FreeFile();
						Microsoft.VisualBasic.FileSystem.FileOpen(num3, text3, OpenMode.Input, OpenAccess.Read, OpenShare.Shared);
						text2 = Microsoft.VisualBasic.FileSystem.InputString(num3, (int)Microsoft.VisualBasic.FileSystem.LOF(num3));
						Microsoft.VisualBasic.FileSystem.FileClose(num3);
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						int num7 = General_Procedures.ParseAndFillArray(text2, "\r\n", ref DynArray);
						TreeQs.Nodes.Clear();
						TreeQs.Nodes.Add("SQLPathFinder Query Samples");
						TreeNode treeNode2 = TreeQs.Nodes[0];
						treeNode2.Name = Strings.UCase(InitPath);
						treeNode2.ImageKey = "openfldr";
						treeNode2.SelectedImageKey = "openfldr";
						int num8 = num7;
						for (num4 = 1; num4 <= num8; num4++)
						{
							string text4 = Strings.Trim(DynArray[num4]);
							if (Operators.CompareString(text4, "", TextCompare: false) == 0)
							{
								continue;
							}
							string text5 = Strings.Mid(text4, num5 + 1);
							num6 = (short)Strings.InStrRev(text5, "\\");
							string text6;
							string text7;
							if (num6 != 0)
							{
								text6 = Strings.UCase(InitPath) + Strings.UCase(Strings.Mid(text5, 1, num6 - 1));
								text7 = Strings.Mid(text5, num6 + 1);
							}
							else
							{
								text6 = Strings.UCase(InitPath);
								text7 = text5;
							}
							if (!((Operators.CompareString(Strings.UCase(text7), "Q", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(Strings.Right(text6, 2)), "\\Q", TextCompare: false) != 0)))
							{
								continue;
							}
							if (Operators.CompareString(text6, Strings.UCase(InitPath), TextCompare: false) == 0)
							{
								BuildForm.Add_Tree_Node(treeNode2, text7, Strings.UCase(text4), "", 22);
								continue;
							}
							if (treeNode == null || Operators.CompareString(treeNode.Name, text6, TextCompare: false) != 0)
							{
								treeNode = BuildForm.FindNodeByName(treeNode2, text6);
							}
							if (treeNode != null)
							{
								BuildForm.Add_Tree_Node(treeNode, text7, Strings.UCase(text4), "", 22);
							}
						}
						treeNode2.Expand();
						LblDescription.Text = "Select a folder to see available Queries or documentation. Double-Click a query or file to load it";
						DynArray = null;
						fPath = InitPath;
						Load_FileList();
						FileList.Height = SplitContainer1.Panel2.Height + 50;
						TreeQs.Height = SplitContainer1.Panel1.Height + 50;
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					case 924:
						num = -1;
						switch (num2)
						{
						case 2:
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								text = Conversion.ErrorToString();
							}
							Interaction.MsgBox("Error loading queries. (" + text + "). You must be on the Intel LAN to access Sample Queries. Contact Support if you need help accessing: " + InitPath, MsgBoxStyle.Critical, "Sample Queries Error");
							Information.Err().Clear();
							Cursor.Current = Cursors.Default;
							mnuExit_Click(mnuExit, new EventArgs());
							goto end_IL_0001;
						}
						break;
					}
					goto IL_03d2;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 924;
				continue;
			}
			break;
			IL_03d2:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmSampleQs_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					break;
				case 50:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
							goto end_IL_0001_3;
						}
						goto default;
					}
					end_IL_0001_2:
					break;
				}
				num2 = 2;
				Dispose();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 50;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuExit_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					break;
				case 50:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
							goto end_IL_0001_3;
						}
						goto default;
					}
					end_IL_0001_2:
					break;
				}
				num2 = 2;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 50;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuHelpSQ_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					break;
				case 54:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
							goto end_IL_0001_3;
						}
						goto default;
					}
					end_IL_0001_2:
					break;
				}
				num2 = 2;
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Sample+Queries");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 54;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TreeQs_AfterCollapse(object sender, TreeViewEventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		TreeNode node = default(TreeNode);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					node = e.Node;
					goto IL_000b;
				case 86:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000b;
						case 3:
							goto IL_0014;
						case 4:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					node.ImageKey = "closefldr";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				node.SelectedImageKey = "closefldr";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 86;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TreeQs_AfterExpand(object sender, TreeViewEventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		TreeNode node = default(TreeNode);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					node = e.Node;
					goto IL_000b;
				case 86:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000b;
						case 3:
							goto IL_0014;
						case 4:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					node.ImageKey = "openfldr";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				node.SelectedImageKey = "openfldr";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 86;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TreeQs_KeyPress(object sender, KeyPressEventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short num5 = default(short);
		int num = default(int);
		int num3 = default(int);
		TreeNode Node = default(TreeNode);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					num5 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_0011;
				case 136:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0011;
						case 3:
							goto IL_001e;
						case 4:
							goto IL_002d;
						case 5:
							goto IL_0036;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_001e:
					num2 = 3;
					Node = TreeQs.SelectedNode;
					goto IL_002d;
					IL_002d:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0036;
					IL_0011:
					num2 = 2;
					if (num5 != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_001e;
					IL_0036:
					num2 = 5;
					if (Node != null)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				DoNodeClick(ref Node);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 136;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TreeQs_NodeClick(object eventSender, TreeNodeMouseClickEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		TreeNode Node = default(TreeNode);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					Node = eventArgs.Node;
					goto IL_000b;
				case 87:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000b;
						case 3:
							goto IL_0014;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Node != null)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				DoNodeClick(ref Node);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 87;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}
}

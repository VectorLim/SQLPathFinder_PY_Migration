using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmBatch4 : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBSave")]
	private ToolStripMenuItem _mnuBSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBAdd")]
	private ToolStripMenuItem _mnuBAdd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBAddRun")]
	private ToolStripMenuItem _mnuBAddRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBHelp")]
	private ToolStripMenuItem _mnuBHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtSHGUIDir")]
	private ComboBox _TxtSHGUIDir;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private Button _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdAddRun")]
	private Button _CmdAddRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdAdd")]
	private Button _CmdAdd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse")]
	private Button _CmdBrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuExit")]
	private ToolStripMenuItem _mnuExit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSHGUIAll")]
	private ToolStripMenuItem _mnuSHGUIAll;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPySH")]
	private ToolStripMenuItem _mnuPySH;

	private bool l_isInit;

	public virtual ToolStripMenuItem mnuBSave
	{
		[CompilerGenerated]
		get
		{
			return _mnuBSave;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuBSave_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBSave = value;
			toolStripMenuItem = _mnuBSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuBAdd
	{
		[CompilerGenerated]
		get
		{
			return _mnuBAdd;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuBAdd_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBAdd;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBAdd = value;
			toolStripMenuItem = _mnuBAdd;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuBAddRun
	{
		[CompilerGenerated]
		get
		{
			return _mnuBAddRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuBAddRun_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBAddRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBAddRun = value;
			toolStripMenuItem = _mnuBAddRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuBSHGUI")]
	public virtual ToolStripMenuItem mnuBSHGUI
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuBHelp
	{
		[CompilerGenerated]
		get
		{
			return _mnuBHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuBHelp_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBHelp;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBHelp = value;
			toolStripMenuItem = _mnuBHelp;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MainMenu")]
	public virtual MenuStrip MainMenu
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLog")]
	public virtual ComboBox TxtLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbQueue")]
	public virtual ComboBox CmbQueue
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ComboBox TxtSHGUIDir
	{
		[CompilerGenerated]
		get
		{
			return _TxtSHGUIDir;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtSHGUIDir_SelectedIndexChanged;
			ComboBox comboBox = _TxtSHGUIDir;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			_TxtSHGUIDir = value;
			comboBox = _TxtSHGUIDir;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

	public virtual Button cmdClear
	{
		[CompilerGenerated]
		get
		{
			return _cmdClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClear_Click;
			Button button = _cmdClear;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdClear = value;
			button = _cmdClear;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtName")]
	public virtual TextBox TxtName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button CmdAddRun
	{
		[CompilerGenerated]
		get
		{
			return _CmdAddRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdAddRun_Click;
			Button button = _CmdAddRun;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdAddRun = value;
			button = _CmdAddRun;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdAdd
	{
		[CompilerGenerated]
		get
		{
			return _CmdAdd;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdAdd_Click;
			Button button = _CmdAdd;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdAdd = value;
			button = _CmdAdd;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtJob")]
	public virtual TextBox TxtJob
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button CmdBrowse
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse_Click;
			Button button = _CmdBrowse;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse = value;
			button = _CmdBrowse;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LblQ")]
	public virtual Label LblQ
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblSHDir")]
	public virtual Label LblSHDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblName")]
	public virtual Label lblName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label3")]
	public virtual Label Label3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblLog")]
	public virtual Label LblLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblJob")]
	public virtual Label LblJob
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label1")]
	public virtual Label Label1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuExit
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

	[field: AccessedThroughProperty("mnuOptions")]
	internal virtual ToolStripMenuItem mnuOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuUseTmp")]
	internal virtual ToolStripMenuItem mnuUseTmp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSHGUIAll
	{
		[CompilerGenerated]
		get
		{
			return _mnuSHGUIAll;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHGUIAll_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSHGUIAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSHGUIAll = value;
			toolStripMenuItem = _mnuSHGUIAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbSite")]
	internal virtual ComboBox cmbSite
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbMem")]
	internal virtual ComboBox cmbMem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbJMP")]
	internal virtual ComboBox cmbJMP
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbljmp")]
	internal virtual Label lbljmp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblType")]
	internal virtual Label lblType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbType")]
	internal virtual ComboBox cmbType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuPySH
	{
		[CompilerGenerated]
		get
		{
			return _mnuPySH;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPySH_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPySH;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPySH = value;
			toolStripMenuItem = _mnuPySH;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[DebuggerNonUserCode]
	public FrmBatch4()
	{
		base.FormClosed += FrmBatch4_FormClosed;
		base.Load += FrmBatch4_Load;
		base.FormClosing += FrmBatch4_FormClosing;
		l_isInit = false;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmBatch4));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdClear = new System.Windows.Forms.Button();
		this.CmdAddRun = new System.Windows.Forms.Button();
		this.CmdAdd = new System.Windows.Forms.Button();
		this.CmdBrowse = new System.Windows.Forms.Button();
		this.LblSHDir = new System.Windows.Forms.Label();
		this.MainMenu = new System.Windows.Forms.MenuStrip();
		this.mnuBSHGUI = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBSave = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBAdd = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBAddRun = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSHGUIAll = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUseTmp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPySH = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtLog = new System.Windows.Forms.ComboBox();
		this.CmbQueue = new System.Windows.Forms.ComboBox();
		this.TxtSHGUIDir = new System.Windows.Forms.ComboBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.TxtJob = new System.Windows.Forms.TextBox();
		this.LblQ = new System.Windows.Forms.Label();
		this.lblName = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.LblLog = new System.Windows.Forms.Label();
		this.LblJob = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.cmbSite = new System.Windows.Forms.ComboBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.cmbMem = new System.Windows.Forms.ComboBox();
		this.cmbJMP = new System.Windows.Forms.ComboBox();
		this.lbljmp = new System.Windows.Forms.Label();
		this.lblType = new System.Windows.Forms.Label();
		this.cmbType = new System.Windows.Forms.ComboBox();
		this.MainMenu.SuspendLayout();
		base.SuspendLayout();
		this.cmdClear.BackColor = System.Drawing.SystemColors.Control;
		this.cmdClear.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdClear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdClear.ImageIndex = 3;
		this.cmdClear.Location = new System.Drawing.Point(587, 146);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdClear.Size = new System.Drawing.Size(77, 48);
		this.cmdClear.TabIndex = 2;
		this.cmdClear.Text = "Clear";
		this.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear form entries");
		this.cmdClear.UseVisualStyleBackColor = false;
		this.CmdAddRun.BackColor = System.Drawing.SystemColors.Control;
		this.CmdAddRun.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdAddRun.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdAddRun.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdAddRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdAddRun.ImageIndex = 1;
		this.CmdAddRun.Location = new System.Drawing.Point(663, 100);
		this.CmdAddRun.Name = "CmdAddRun";
		this.CmdAddRun.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdAddRun.Size = new System.Drawing.Size(77, 48);
		this.CmdAddRun.TabIndex = 1;
		this.CmdAddRun.Text = "Add/Run";
		this.CmdAddRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdAddRun, "Add Job to ScriptHost and Run (Ctrl+R)");
		this.CmdAddRun.UseVisualStyleBackColor = false;
		this.CmdAdd.BackColor = System.Drawing.SystemColors.Control;
		this.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdAdd.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdAdd.ImageIndex = 0;
		this.CmdAdd.Location = new System.Drawing.Point(663, 146);
		this.CmdAdd.Name = "CmdAdd";
		this.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdAdd.Size = new System.Drawing.Size(77, 48);
		this.CmdAdd.TabIndex = 3;
		this.CmdAdd.Text = "Add";
		this.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdAdd, "Add job to ScriptHost (Ctrl+A)");
		this.CmdAdd.UseVisualStyleBackColor = false;
		this.CmdBrowse.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowse.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdBrowse.ImageIndex = 2;
		this.CmdBrowse.Location = new System.Drawing.Point(587, 100);
		this.CmdBrowse.Name = "CmdBrowse";
		this.CmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowse.Size = new System.Drawing.Size(77, 48);
		this.CmdBrowse.TabIndex = 0;
		this.CmdBrowse.Text = "Save";
		this.CmdBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdBrowse, "Save Query to ScriptHost");
		this.CmdBrowse.UseVisualStyleBackColor = false;
		this.LblSHDir.AutoSize = true;
		this.LblSHDir.BackColor = System.Drawing.SystemColors.Control;
		this.LblSHDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblSHDir.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblSHDir.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblSHDir.Location = new System.Drawing.Point(5, 294);
		this.LblSHDir.Name = "LblSHDir";
		this.LblSHDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblSHDir.Size = new System.Drawing.Size(117, 16);
		this.LblSHDir.TabIndex = 11;
		this.LblSHDir.Text = "ScriptHost Folder";
		this.ToolTip1.SetToolTip(this.LblSHDir, "SHGUI Base directory");
		this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuBSHGUI, this.mnuOptions, this.mnuBHelp });
		this.MainMenu.Location = new System.Drawing.Point(0, 0);
		this.MainMenu.Name = "MainMenu";
		this.MainMenu.Size = new System.Drawing.Size(746, 28);
		this.MainMenu.TabIndex = 16;
		this.MainMenu.Text = "ScriptHost";
		this.mnuBSHGUI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuBSave, this.mnuBAdd, this.mnuBAddRun, this.mnuSHGUIAll, this.mnuExit });
		this.mnuBSHGUI.Name = "mnuBSHGUI";
		this.mnuBSHGUI.Size = new System.Drawing.Size(92, 24);
		this.mnuBSHGUI.Text = "ScriptHost";
		this.mnuBSave.Name = "mnuBSave";
		this.mnuBSave.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.mnuBSave.Size = new System.Drawing.Size(369, 26);
		this.mnuBSave.Text = "Save Query to ScriptHost";
		this.mnuBAdd.Name = "mnuBAdd";
		this.mnuBAdd.ShortcutKeys = System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control;
		this.mnuBAdd.Size = new System.Drawing.Size(369, 26);
		this.mnuBAdd.Text = "Add Job to ScriptHost";
		this.mnuBAddRun.Name = "mnuBAddRun";
		this.mnuBAddRun.ShortcutKeys = System.Windows.Forms.Keys.R | System.Windows.Forms.Keys.Control;
		this.mnuBAddRun.Size = new System.Drawing.Size(369, 26);
		this.mnuBAddRun.Text = "Add Job to ScriptHost and Submit";
		this.mnuSHGUIAll.Name = "mnuSHGUIAll";
		this.mnuSHGUIAll.ShortcutKeys = System.Windows.Forms.Keys.U | System.Windows.Forms.Keys.Control;
		this.mnuSHGUIAll.Size = new System.Drawing.Size(369, 26);
		this.mnuSHGUIAll.Text = "ScriptHost Job Manager";
		this.mnuExit.Name = "mnuExit";
		this.mnuExit.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.mnuExit.Size = new System.Drawing.Size(369, 26);
		this.mnuExit.Text = "Exit";
		this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuUseTmp, this.mnuPySH });
		this.mnuOptions.Name = "mnuOptions";
		this.mnuOptions.Size = new System.Drawing.Size(75, 24);
		this.mnuOptions.Text = "Options";
		this.mnuUseTmp.Checked = true;
		this.mnuUseTmp.CheckOnClick = true;
		this.mnuUseTmp.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuUseTmp.Name = "mnuUseTmp";
		this.mnuUseTmp.Size = new System.Drawing.Size(653, 26);
		this.mnuUseTmp.Text = "Do not auto-save output files on ScriptHost (must explicitly save) - Uses Temp Folder";
		this.mnuPySH.CheckOnClick = true;
		this.mnuPySH.Name = "mnuPySH";
		this.mnuPySH.Size = new System.Drawing.Size(653, 26);
		this.mnuPySH.Text = "Use Python on ScriptHost";
		this.mnuBHelp.Name = "mnuBHelp";
		this.mnuBHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.mnuBHelp.Size = new System.Drawing.Size(84, 24);
		this.mnuBHelp.Text = "Help (F1)";
		this.TxtLog.BackColor = System.Drawing.Color.White;
		this.TxtLog.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TxtLog.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtLog.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtLog.Location = new System.Drawing.Point(135, 164);
		this.TxtLog.Name = "TxtLog";
		this.TxtLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtLog.Size = new System.Drawing.Size(443, 24);
		this.TxtLog.TabIndex = 5;
		this.CmbQueue.BackColor = System.Drawing.Color.White;
		this.CmbQueue.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmbQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbQueue.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmbQueue.ForeColor = System.Drawing.SystemColors.WindowText;
		this.CmbQueue.Location = new System.Drawing.Point(135, 196);
		this.CmbQueue.Name = "CmbQueue";
		this.CmbQueue.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmbQueue.Size = new System.Drawing.Size(231, 24);
		this.CmbQueue.TabIndex = 6;
		this.TxtSHGUIDir.BackColor = System.Drawing.Color.White;
		this.TxtSHGUIDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtSHGUIDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TxtSHGUIDir.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtSHGUIDir.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtSHGUIDir.Location = new System.Drawing.Point(135, 292);
		this.TxtSHGUIDir.Name = "TxtSHGUIDir";
		this.TxtSHGUIDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSHGUIDir.Size = new System.Drawing.Size(443, 24);
		this.TxtSHGUIDir.TabIndex = 10;
		this.TxtName.AcceptsReturn = true;
		this.TxtName.BackColor = System.Drawing.SystemColors.Window;
		this.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtName.Enabled = false;
		this.TxtName.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtName.Location = new System.Drawing.Point(135, 132);
		this.TxtName.MaxLength = 0;
		this.TxtName.Name = "TxtName";
		this.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtName.Size = new System.Drawing.Size(443, 23);
		this.TxtName.TabIndex = 5;
		this.TxtName.TabStop = false;
		this.TxtJob.AcceptsReturn = true;
		this.TxtJob.BackColor = System.Drawing.SystemColors.Window;
		this.TxtJob.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtJob.Enabled = false;
		this.TxtJob.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtJob.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtJob.Location = new System.Drawing.Point(135, 100);
		this.TxtJob.MaxLength = 0;
		this.TxtJob.Name = "TxtJob";
		this.TxtJob.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtJob.Size = new System.Drawing.Size(443, 23);
		this.TxtJob.TabIndex = 4;
		this.TxtJob.TabStop = false;
		this.LblQ.AutoSize = true;
		this.LblQ.BackColor = System.Drawing.SystemColors.Control;
		this.LblQ.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblQ.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblQ.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblQ.Location = new System.Drawing.Point(5, 198);
		this.LblQ.Name = "LblQ";
		this.LblQ.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblQ.Size = new System.Drawing.Size(77, 16);
		this.LblQ.TabIndex = 14;
		this.LblQ.Text = "Job Queue";
		this.lblName.AutoSize = true;
		this.lblName.BackColor = System.Drawing.SystemColors.Control;
		this.lblName.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblName.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblName.Location = new System.Drawing.Point(5, 134);
		this.lblName.Name = "lblName";
		this.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblName.Size = new System.Drawing.Size(70, 16);
		this.lblName.TabIndex = 10;
		this.lblName.Text = "Job Name";
		this.Label3.BackColor = System.Drawing.SystemColors.Control;
		this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label3.Location = new System.Drawing.Point(5, 32);
		this.Label3.Name = "Label3";
		this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label3.Size = new System.Drawing.Size(726, 52);
		this.Label3.TabIndex = 8;
		this.Label3.Text = resources.GetString("Label3.Text");
		this.LblLog.AutoSize = true;
		this.LblLog.BackColor = System.Drawing.SystemColors.Control;
		this.LblLog.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblLog.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblLog.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblLog.Location = new System.Drawing.Point(5, 166);
		this.LblLog.Name = "LblLog";
		this.LblLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblLog.Size = new System.Drawing.Size(59, 16);
		this.LblLog.TabIndex = 7;
		this.LblLog.Text = "Log File";
		this.LblJob.AutoSize = true;
		this.LblJob.BackColor = System.Drawing.SystemColors.Control;
		this.LblJob.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblJob.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblJob.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblJob.Location = new System.Drawing.Point(5, 102);
		this.LblJob.Name = "LblJob";
		this.LblJob.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblJob.Size = new System.Drawing.Size(57, 16);
		this.LblJob.TabIndex = 6;
		this.LblJob.Text = "Job File";
		this.Label1.BackColor = System.Drawing.SystemColors.Control;
		this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label1.Location = new System.Drawing.Point(320, 213);
		this.Label1.Name = "Label1";
		this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label1.Size = new System.Drawing.Size(7, 21);
		this.Label1.TabIndex = 5;
		this.Label1.Text = ":";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(5, 230);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(100, 16);
		this.Label2.TabIndex = 17;
		this.Label2.Text = "ScriptHost Site";
		this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSite.FormattingEnabled = true;
		this.cmbSite.Location = new System.Drawing.Point(135, 228);
		this.cmbSite.Name = "cmbSite";
		this.cmbSite.Size = new System.Drawing.Size(443, 24);
		this.cmbSite.TabIndex = 7;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(5, 262);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(126, 16);
		this.Label4.TabIndex = 19;
		this.Label4.Text = "ScriptHost Memory";
		this.cmbMem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMem.FormattingEnabled = true;
		this.cmbMem.Items.AddRange(new object[4] { "1GB", "2GB", "4GB", "8GB" });
		this.cmbMem.Location = new System.Drawing.Point(135, 260);
		this.cmbMem.Name = "cmbMem";
		this.cmbMem.Size = new System.Drawing.Size(231, 24);
		this.cmbMem.TabIndex = 8;
		this.cmbJMP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbJMP.FormattingEnabled = true;
		this.cmbJMP.Items.AddRange(new object[3] { "", "v14", "v17" });
		this.cmbJMP.Location = new System.Drawing.Point(457, 260);
		this.cmbJMP.Name = "cmbJMP";
		this.cmbJMP.Size = new System.Drawing.Size(121, 24);
		this.cmbJMP.TabIndex = 9;
		this.lbljmp.AutoSize = true;
		this.lbljmp.Location = new System.Drawing.Point(405, 262);
		this.lbljmp.Name = "lbljmp";
		this.lbljmp.Size = new System.Drawing.Size(34, 16);
		this.lbljmp.TabIndex = 22;
		this.lbljmp.Text = "JMP";
		this.lblType.AutoSize = true;
		this.lblType.Location = new System.Drawing.Point(405, 198);
		this.lblType.Name = "lblType";
		this.lblType.Size = new System.Drawing.Size(39, 16);
		this.lblType.TabIndex = 23;
		this.lblType.Text = "Type";
		this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbType.FormattingEnabled = true;
		this.cmbType.Location = new System.Drawing.Point(457, 196);
		this.cmbType.Name = "cmbType";
		this.cmbType.Size = new System.Drawing.Size(121, 24);
		this.cmbType.TabIndex = 24;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(746, 324);
		base.Controls.Add(this.cmbType);
		base.Controls.Add(this.lblType);
		base.Controls.Add(this.lbljmp);
		base.Controls.Add(this.cmbJMP);
		base.Controls.Add(this.cmbMem);
		base.Controls.Add(this.Label4);
		base.Controls.Add(this.cmbSite);
		base.Controls.Add(this.Label2);
		base.Controls.Add(this.TxtLog);
		base.Controls.Add(this.CmbQueue);
		base.Controls.Add(this.TxtSHGUIDir);
		base.Controls.Add(this.cmdClear);
		base.Controls.Add(this.TxtName);
		base.Controls.Add(this.CmdAddRun);
		base.Controls.Add(this.CmdAdd);
		base.Controls.Add(this.TxtJob);
		base.Controls.Add(this.CmdBrowse);
		base.Controls.Add(this.LblQ);
		base.Controls.Add(this.LblSHDir);
		base.Controls.Add(this.lblName);
		base.Controls.Add(this.Label3);
		base.Controls.Add(this.LblLog);
		base.Controls.Add(this.LblJob);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.MainMenu);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(3, 49);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmBatch4";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Run / Schedule Query on ScriptHost";
		this.MainMenu.ResumeLayout(false);
		this.MainMenu.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Add_Batch(string MyArg)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myJob = default(string);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		int num5 = default(int);
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
				case 1723:
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
							goto IL_000f;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0033;
						case 8:
							goto IL_003c;
						case 9:
							goto IL_0045;
						case 10:
							goto IL_005a;
						case 11:
							goto IL_006e;
						case 13:
							goto IL_0082;
						case 12:
						case 14:
						case 15:
							goto IL_0093;
						case 16:
							goto IL_00af;
						case 18:
							goto IL_00c3;
						case 17:
						case 19:
						case 20:
							goto IL_00d4;
						case 21:
							goto IL_0105;
						case 23:
							goto IL_0119;
						case 22:
						case 24:
						case 25:
							goto IL_012a;
						case 26:
							goto IL_0146;
						case 28:
							goto IL_015a;
						case 27:
						case 29:
						case 30:
							goto IL_016b;
						case 31:
							goto IL_0187;
						case 33:
							goto IL_019b;
						case 32:
						case 34:
						case 35:
							goto IL_01ac;
						case 36:
							goto IL_01c8;
						case 38:
							goto IL_01dc;
						case 37:
						case 39:
						case 40:
							goto IL_01ed;
						case 41:
							goto IL_0217;
						case 42:
							goto IL_0258;
						case 43:
							goto IL_026f;
						case 44:
						case 45:
							goto IL_028d;
						case 46:
							goto IL_02a2;
						case 47:
							goto IL_02b2;
						case 48:
							goto IL_02c7;
						case 49:
							goto IL_02dd;
						case 51:
							goto IL_02ea;
						case 52:
							goto IL_0300;
						case 54:
							goto IL_030d;
						case 55:
							goto IL_0323;
						case 57:
							goto IL_0330;
						case 58:
							goto IL_0346;
						case 50:
						case 53:
						case 56:
						case 59:
						case 60:
							goto IL_0351;
						case 61:
							goto IL_035b;
						case 62:
							goto IL_036d;
						case 63:
							goto IL_037a;
						case 64:
							goto IL_0388;
						case 65:
						case 66:
							goto IL_0399;
						case 67:
							goto IL_03cd;
						case 69:
							goto IL_047c;
						case 70:
							goto IL_0495;
						case 71:
						case 72:
							goto IL_04a8;
						case 68:
						case 73:
						case 74:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 75:
						case 76:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_03cd:
					num2 = 67;
					myJob = "va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder_batch.va\" \"" + text + "\" \"" + MyArg + "\" \"" + Strings.Trim(TxtName.Text) + "\" \"" + text2 + "\" \"" + text3 + "\" \"" + text4 + "\" \"" + text5 + "\"";
					break;
					IL_047c:
					num2 = 69;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_0495;
					}
					goto IL_04a8;
					IL_0399:
					num2 = 66;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_03cd;
					}
					goto IL_047c;
					IL_0495:
					num2 = 70;
					text2 += "\\";
					goto IL_04a8;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					myJob = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					text2 = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					text3 = "";
					goto IL_0033;
					IL_0033:
					num2 = 7;
					text4 = "";
					goto IL_003c;
					IL_003c:
					num2 = 8;
					text5 = "";
					goto IL_0045;
					IL_0045:
					num2 = 9;
					if (Test_Valid_Batch() != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_005a;
					IL_005a:
					num2 = 10;
					if (mnuUseTmp.Checked)
					{
						goto IL_006e;
					}
					goto IL_0082;
					IL_006e:
					num2 = 11;
					MyArg += "Y";
					goto IL_0093;
					IL_0082:
					num2 = 13;
					MyArg += "N";
					goto IL_0093;
					IL_0093:
					num2 = 15;
					if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
					{
						goto IL_00af;
					}
					goto IL_00c3;
					IL_00af:
					num2 = 16;
					MyArg += "Y";
					goto IL_00d4;
					IL_00c3:
					num2 = 18;
					MyArg += "N";
					goto IL_00d4;
					IL_00d4:
					num2 = 20;
					if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0 && Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_0105;
					}
					goto IL_0119;
					IL_04a8:
					num2 = 72;
					myJob = "\"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder_batch.py\" \"" + text + "\" \"" + MyArg + "\" \"" + Strings.Trim(TxtName.Text) + "\" \"" + text2 + "\" \"" + text3 + "\" \"" + text4 + "\" \"" + text5 + "\"";
					break;
					IL_0105:
					num2 = 21;
					MyArg += "Y";
					goto IL_012a;
					IL_0119:
					num2 = 23;
					MyArg += "N";
					goto IL_012a;
					IL_012a:
					num2 = 25;
					if (Operators.CompareString(Globals_Renamed.gEncodeFFS, "Y", TextCompare: false) == 0)
					{
						goto IL_0146;
					}
					goto IL_015a;
					IL_0146:
					num2 = 26;
					MyArg += "Y";
					goto IL_016b;
					IL_015a:
					num2 = 28;
					MyArg += "N";
					goto IL_016b;
					IL_016b:
					num2 = 30;
					if (Operators.CompareString(Globals_Renamed.gEncodeUTFBOM, "Y", TextCompare: false) == 0)
					{
						goto IL_0187;
					}
					goto IL_019b;
					IL_0187:
					num2 = 31;
					MyArg += "Y";
					goto IL_01ac;
					IL_019b:
					num2 = 33;
					MyArg += "N";
					goto IL_01ac;
					IL_01ac:
					num2 = 35;
					if (Operators.CompareString(Globals_Renamed.gConvertMAOUber, "Y", TextCompare: false) == 0)
					{
						goto IL_01c8;
					}
					goto IL_01dc;
					IL_01c8:
					num2 = 36;
					MyArg += "Y";
					goto IL_01ed;
					IL_01dc:
					num2 = 38;
					MyArg += "N";
					goto IL_01ed;
					IL_01ed:
					num2 = 40;
					text3 = "JobSize=" + CmbQueue.Text + ";ScriptType=" + cmbType.Text;
					goto IL_0217;
					IL_0217:
					num2 = 41;
					text3 = text3 + ";MemSize=" + cmbMem.Text + ";Site=" + cmbSite.Text;
					goto IL_0258;
					IL_0258:
					num2 = 42;
					if (cmbJMP.SelectedIndex > 0)
					{
						goto IL_026f;
					}
					goto IL_028d;
					IL_026f:
					num2 = 43;
					text3 = text3 + ";JMP=" + cmbJMP.Text;
					goto IL_028d;
					IL_028d:
					num2 = 45;
					text = Strings.Trim(TxtJob.Text);
					goto IL_02a2;
					IL_02a2:
					num2 = 46;
					text4 = TxtLog.Text;
					goto IL_02b2;
					IL_02b2:
					num2 = 47;
					text2 = Strings.UCase(TxtSHGUIDir.Text);
					goto IL_02c7;
					IL_02c7:
					num2 = 48;
					if (LikeOperator.LikeString(text2, "*PRODAT.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_02dd;
					}
					goto IL_02ea;
					IL_02dd:
					num2 = 49;
					text5 = "AT";
					goto IL_0351;
					IL_02ea:
					num2 = 51;
					if (LikeOperator.LikeString(text2, "*INTG.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_0300;
					}
					goto IL_030d;
					IL_0300:
					num2 = 52;
					text5 = "INTG";
					goto IL_0351;
					IL_030d:
					num2 = 54;
					if (LikeOperator.LikeString(text2, "*NSG.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_0323;
					}
					goto IL_0330;
					IL_0323:
					num2 = 55;
					text5 = "NSG";
					goto IL_0351;
					IL_0330:
					num2 = 57;
					if (LikeOperator.LikeString(text2, "*DEV.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_0346;
					}
					goto IL_0351;
					IL_0346:
					num2 = 58;
					text5 = "DEV";
					goto IL_0351;
					IL_0351:
					num2 = 60;
					text2 = "";
					goto IL_035b;
					IL_035b:
					num2 = 61;
					num5 = Strings.InStrRev(text, "\\");
					goto IL_036d;
					IL_036d:
					num2 = 62;
					if (num5 != 0)
					{
						goto IL_037a;
					}
					goto IL_0399;
					IL_037a:
					num2 = 63;
					text2 = Strings.Mid(text, 1, num5);
					goto IL_0388;
					IL_0388:
					num2 = 64;
					text = Strings.Mid(text, checked(num5 + 1));
					goto IL_0399;
					end_IL_0001_2:
					break;
				}
				num2 = 74;
				BuildSQL.RunQ(myJob, 0, "M");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1723;
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

	private short Test_Valid_Batch()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short result = default(short);
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
				case 132:
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
							goto IL_0031;
						case 4:
							goto IL_0045;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(TxtName.Text), "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0031;
					IL_0031:
					num2 = 3;
					Interaction.MsgBox("You must save the job to ScriptHost before you can add a job entry.", MsgBoxStyle.Exclamation, "Missing a Name for the Scheduled Job");
					goto IL_0045;
					IL_0045:
					num2 = 4;
					result = 0;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = 1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 132;
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

	private void CmdBrowse_Click(object eventSender, EventArgs eventArgs)
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
				case 61:
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
				mnuBSave_Click(mnuBSave, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 61;
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

	private void cmdClear_Click(object eventSender, EventArgs eventArgs)
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
					goto IL_000b;
				case 102:
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
							goto IL_001e;
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
					num2 = 2;
					TxtJob.Text = "";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					TxtName.Text = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				TxtLog.SelectedIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 102;
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

	private void FrmBatch4_FormClosed(object sender, FormClosedEventArgs e)
	{
		Dispose();
	}

	private void FrmBatch4_Load(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				Button MyButton;
				ComboBox cmbQueue;
				ComboBox TxtSHGUIDir;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 932:
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
							goto IL_000f;
						case 4:
							goto IL_002f;
						case 5:
							goto IL_004f;
						case 6:
							goto IL_006f;
						case 7:
							goto IL_008f;
						case 8:
							goto IL_00bd;
						case 9:
							goto IL_00c9;
						case 10:
							goto IL_00fa;
						case 12:
							goto IL_011f;
						case 11:
						case 13:
						case 14:
							goto IL_0141;
						case 15:
							goto IL_015d;
						case 17:
							goto IL_0171;
						case 16:
						case 18:
						case 19:
							goto IL_0182;
						case 20:
							goto IL_018c;
						case 21:
							goto IL_019c;
						case 22:
							goto IL_01b8;
						case 24:
							goto IL_01cc;
						case 23:
						case 25:
						case 26:
							goto IL_01dd;
						case 27:
							goto IL_01ed;
						case 28:
							goto IL_01fd;
						case 29:
							goto IL_0218;
						case 30:
							goto IL_023d;
						case 32:
						case 33:
							goto IL_0251;
						case 31:
						case 34:
							goto IL_025d;
						case 35:
							goto IL_0276;
						case 36:
							goto IL_028f;
						case 37:
							goto IL_029f;
						case 38:
							goto IL_02b0;
						case 39:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 40:
						case 41:
						case 42:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_028f:
					num2 = 36;
					TxtLog.SelectedIndex = 0;
					goto IL_029f;
					IL_029f:
					num2 = 37;
					if (Globals_Renamed.Design_Mode == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_02b0;
					IL_0276:
					num2 = 35;
					TxtLog.Items.Add("<jobname>_<entry>.log");
					goto IL_028f;
					IL_02b0:
					num2 = 38;
					if (!MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].IsJMPPresent())
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyButton = CmdBrowse;
					BuildForm.Set_Btn_Img(ref MyButton, "save");
					CmdBrowse = MyButton;
					goto IL_002f;
					IL_002f:
					num2 = 4;
					MyButton = CmdAddRun;
					BuildForm.Set_Btn_Img(ref MyButton, "run");
					CmdAddRun = MyButton;
					goto IL_004f;
					IL_004f:
					num2 = 5;
					MyButton = cmdClear;
					BuildForm.Set_Btn_Img(ref MyButton, "cancel");
					cmdClear = MyButton;
					goto IL_006f;
					IL_006f:
					num2 = 6;
					MyButton = CmdAdd;
					BuildForm.Set_Btn_Img(ref MyButton, "clock");
					CmdAdd = MyButton;
					goto IL_008f;
					IL_008f:
					num2 = 7;
					cmbQueue = CmbQueue;
					TxtSHGUIDir = this.TxtSHGUIDir;
					BuildForm.Init_SH_Controls(ref cmbQueue, ref TxtSHGUIDir);
					this.TxtSHGUIDir = TxtSHGUIDir;
					CmbQueue = cmbQueue;
					goto IL_00bd;
					IL_00bd:
					num2 = 8;
					Globals_Renamed.gSHType = "PY";
					goto IL_00c9;
					IL_00c9:
					num2 = 9;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_00fa;
					}
					goto IL_011f;
					IL_0251:
					num2 = 33;
					num5 = checked(num5 + 1);
					goto IL_0258;
					IL_011f:
					num2 = 12;
					TxtSHGUIDir = cmbType;
					BuildForm.Set_SH_Type(ref TxtSHGUIDir, "P");
					cmbType = TxtSHGUIDir;
					goto IL_0141;
					IL_00fa:
					num2 = 10;
					TxtSHGUIDir = cmbType;
					BuildForm.Set_SH_Type(ref TxtSHGUIDir, "V");
					cmbType = TxtSHGUIDir;
					goto IL_0141;
					IL_0141:
					num2 = 14;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0)
					{
						goto IL_015d;
					}
					goto IL_0171;
					IL_015d:
					num2 = 15;
					mnuPySH.Checked = false;
					goto IL_0182;
					IL_0171:
					num2 = 17;
					mnuPySH.Checked = true;
					goto IL_0182;
					IL_0182:
					num2 = 19;
					l_isInit = true;
					goto IL_018c;
					IL_018c:
					num2 = 20;
					CmbQueue.SelectedIndex = 0;
					goto IL_019c;
					IL_019c:
					num2 = 21;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_01b8;
					}
					goto IL_01cc;
					IL_01b8:
					num2 = 22;
					cmbMem.SelectedIndex = 1;
					goto IL_01dd;
					IL_01cc:
					num2 = 24;
					cmbMem.SelectedIndex = 0;
					goto IL_01dd;
					IL_01dd:
					num2 = 26;
					cmbSite.SelectedIndex = 0;
					goto IL_01ed;
					IL_01ed:
					num2 = 27;
					cmbJMP.SelectedIndex = 0;
					goto IL_01fd;
					IL_01fd:
					num2 = 28;
					num6 = checked(cmbSite.Items.Count - 1);
					num5 = 0;
					goto IL_0258;
					IL_0258:
					if (num5 <= num6)
					{
						goto IL_0218;
					}
					goto IL_025d;
					IL_0218:
					num2 = 29;
					if (Operators.ConditionalCompareObjectEqual(cmbSite.Items[num5], Globals_Renamed.gLastSHSite, TextCompare: false))
					{
						goto IL_023d;
					}
					goto IL_0251;
					IL_023d:
					num2 = 30;
					cmbSite.SelectedIndex = num5;
					goto IL_025d;
					IL_025d:
					num2 = 34;
					TxtLog.Items.Add("<jobname>.log");
					goto IL_0276;
					end_IL_0001_2:
					break;
				}
				num2 = 39;
				cmbJMP.SelectedIndex = 1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 932;
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

	private void FrmBatch4_FormClosing(object eventSender, FormClosingEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		bool cancel = default(bool);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				CloseReason closeReason;
				int num5;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					cancel = eventArgs.Cancel;
					goto IL_000b;
				case 191:
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
							goto IL_0015;
						case 4:
							goto IL_001a;
						case 5:
							goto IL_0023;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_005b;
						case 8:
							goto IL_006d;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0035:
					num2 = 6;
					if (Operators.CompareString(Strings.Trim(cmbSite.Text), "", TextCompare: false) != 0)
					{
						goto IL_005b;
					}
					goto IL_006d;
					IL_005b:
					num2 = 7;
					Globals_Renamed.gLastSHSite = cmbSite.Text;
					goto IL_006d;
					IL_0023:
					num2 = 5;
					Globals_Renamed.gSHGUIDir = TxtSHGUIDir.Text;
					goto IL_0035;
					IL_006d:
					num2 = 8;
					BuildForm.Save_DTRBuild_Ini("B");
					break;
					IL_000b:
					num2 = 2;
					closeReason = eventArgs.CloseReason;
					goto IL_0015;
					IL_0015:
					num2 = 3;
					num5 = 0;
					goto IL_001a;
					IL_001a:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0023;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				eventArgs.Cancel = cancel;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 191;
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

	public void mnuBHelp_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Running+Jobs+in+Batch+Using+ScriptHost");
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

	public void mnuBSave_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string TmpMidas = default(string);
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
						errsource = "FrmBatch - mnuBSave_Click";
						short num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						int num4 = 0;
						string text5 = "";
						TmpMidas = "";
						ComboBox TxtSHGuiDir = TxtSHGUIDir;
						bool num5 = BuildForm.ChkShguiBase(ref TxtSHGuiDir);
						TxtSHGUIDir = TxtSHGuiDir;
						if (!num5)
						{
							Interaction.MsgBox("Your ScriptHost Folder is Not accessible. (" + TxtSHGUIDir.Text + "). If you are connected to the Intel network and you have EAM privileges to access ScriptHost, then try starting ScriptHost to create the folder before retrying.", MsgBoxStyle.Exclamation, "ScriptHost Folder Issue");
							goto end_IL_0001;
						}
						if (Globals_Renamed.Design_Mode != 0)
						{
							BuildForm.TempSetSHSave("S", ref TmpMidas);
							text = BuildSQL.Generate_SQL(1, "", IsPacked: true);
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								break;
							}
							Globals_Renamed.gWorkQuery = "";
							BuildSQL.RunQuery(text, 5, 1, -99, "");
							text = Globals_Renamed.gWorkQuery;
							Globals_Renamed.gWorkQuery = "";
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								break;
							}
							text2 = Strings.Trim(TxtJob.Text);
							if (Operators.CompareString(text2, "", TextCompare: false) != 0)
							{
								num4 = Strings.InStrRev(text2, "\\");
								text5 = ((num4 == 0) ? TxtSHGUIDir.Text : (TxtSHGUIDir.Text + Strings.Mid(text2, 1, num4)));
								text2 = text2;
							}
							else
							{
								text5 = TxtSHGUIDir.Text;
								text2 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fLastPackedExe;
								if (Operators.CompareString(text2, "", TextCompare: false) != 0)
								{
									text2 = text5 + text2;
								}
							}
							BuildForm.GetSHGUIFile(text2, text5);
							if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "", TextCompare: false) == 0))
							{
								TxtName.Text = "";
								TxtJob.Text = "";
								break;
							}
							text2 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							text3 = "";
							if (Operators.CompareString(Strings.UCase(Strings.Mid(text2, 1, Strings.Len(TxtSHGUIDir.Text))), Strings.UCase(TxtSHGUIDir.Text), TextCompare: false) == 0)
							{
								TxtJob.Text = Strings.Trim(Strings.Mid(text2, Strings.Len(TxtSHGUIDir.Text) + 1));
								num4 = Strings.InStrRev(text2, ".");
								if (num4 != 0)
								{
									text3 = Strings.Mid(text2, 1, num4 - 1);
								}
								num4 = Strings.InStrRev(text3, "\\");
								if (num4 != 0)
								{
									text3 = Strings.Mid(text3, num4 + 1);
								}
								TxtName.Text = Strings.Trim(text3);
								if (BuildForm.SaveScriptExe(text, text2) == 0)
								{
									TxtName.Text = "";
									TxtJob.Text = "";
								}
								else if (Operators.CompareString(Strings.UCase(TxtJob.Text), Strings.UCase(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fLastPackedExe), TextCompare: false) != 0)
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fLastPackedExe = TxtJob.Text;
									MyProject.Forms.FrmMain.FrmSQLQuery[0].fSaveQuery = 1;
								}
							}
							else
							{
								Interaction.MsgBox("The Job must be placed in the ScriptHost folder or one of its sub-folders", MsgBoxStyle.Exclamation, "Invalid Path for Batch Job");
							}
						}
						else
						{
							Interaction.MsgBox("A Query must be loaded in order to be processed.", MsgBoxStyle.Exclamation, "No Query loaded");
						}
						break;
					}
					case 1130:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							break;
						default:
							goto end_IL_0001_2;
						}
						break;
					}
					BuildForm.TempSetSHSave("R", ref TmpMidas);
					break;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1130;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TxtSHGUIDir_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
				case 82:
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
				string myDir = Strings.UCase(TxtSHGUIDir.Text);
				ComboBox cmbsite = cmbSite;
				BuildForm.Init_SH_Sites(myDir, ref cmbsite);
				cmbSite = cmbsite;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 82;
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

	private void CmdAdd_Click(object sender, EventArgs e)
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
				case 55:
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
				Add_Batch("N");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void CmdAddRun_Click(object sender, EventArgs e)
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
				case 55:
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
				Add_Batch("Y");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void mnuBAddRun_Click(object sender, EventArgs e)
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
				case 55:
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
				Add_Batch("Y");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void mnuBAdd_Click(object sender, EventArgs e)
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
				case 55:
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
				Add_Batch("N");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void mnuExit_Click(object sender, EventArgs e)
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

	private void mnuSHGUIAll_Click(object sender, EventArgs e)
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
				case 49:
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
				BuildForm.Start_SHGUI();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 49;
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

	private void mnuPySH_Click(object sender, EventArgs e)
	{
		if (!l_isInit)
		{
			return;
		}
		if (mnuPySH.Checked)
		{
			Globals_Renamed.gSHType = "PY";
			if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
			{
				ComboBox cmbtype = cmbType;
				BuildForm.Set_SH_Type(ref cmbtype, "V");
				cmbType = cmbtype;
			}
			else
			{
				ComboBox cmbtype = cmbType;
				BuildForm.Set_SH_Type(ref cmbtype, "P");
				cmbType = cmbtype;
			}
		}
		else
		{
			Globals_Renamed.gSHType = "VA";
			ComboBox cmbtype = cmbType;
			BuildForm.Set_SH_Type(ref cmbtype, "V");
			cmbType = cmbtype;
		}
	}
}

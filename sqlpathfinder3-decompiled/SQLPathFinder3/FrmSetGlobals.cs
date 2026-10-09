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
public class FrmSetGlobals : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridGlobals")]
	private DataGridView _GridGlobals;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp")]
	private Button _cmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK")]
	private Button _cmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCancel")]
	private Button _cmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown")]
	private Button _cmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDelete")]
	private Button _cmdDelete;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOpen")]
	private ToolStripButton _mnuOpen;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSave")]
	private ToolStripButton _mnuSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuNew")]
	private ToolStripButton _mnuNew;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveAs")]
	private ToolStripButton _mnuSaveAs;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdbrowse")]
	private Button _cmdbrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdRefresh")]
	private Button _cmdRefresh;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuComm")]
	private ToolStripMenuItem _mnuComm;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuUnComm")]
	private ToolStripMenuItem _mnuUnComm;

	private int fRows;

	public string f_Mode;

	private string fTitle;

	private bool fSaveAs;

	private bool fDoSave;

	private bool fAddedFileGlobals;

	private bool IsLoading;

	private const int c_lvar = 0;

	private const int c_lval = 1;

	private const int c_lSHVal = 2;

	private const int c_width = 85;

	private Globals_Renamed.gVar_Type[] fGlobals;

	private int fNoGlobals;

	private bool fCancel;

	internal virtual DataGridView GridGlobals
	{
		[CompilerGenerated]
		get
		{
			return _GridGlobals;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellCancelEventHandler value2 = GridGlobals_CellBeginEdit;
			DataGridViewCellEventHandler value3 = GridGlobals_CellValueChanged;
			DataGridViewColumnEventHandler value4 = GridGlobals_ColumnWidthChanged;
			KeyEventHandler value5 = GridGlobals_KeyDown;
			DataGridView dataGridView = _GridGlobals;
			if (dataGridView != null)
			{
				dataGridView.CellBeginEdit -= value2;
				dataGridView.CellValueChanged -= value3;
				dataGridView.ColumnWidthChanged -= value4;
				dataGridView.KeyDown -= value5;
			}
			_GridGlobals = value;
			dataGridView = _GridGlobals;
			if (dataGridView != null)
			{
				dataGridView.CellBeginEdit += value2;
				dataGridView.CellValueChanged += value3;
				dataGridView.ColumnWidthChanged += value4;
				dataGridView.KeyDown += value5;
			}
		}
	}

	internal virtual Button cmdUp
	{
		[CompilerGenerated]
		get
		{
			return _cmdUp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp_Click;
			Button button = _cmdUp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdUp = value;
			button = _cmdUp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdOK
	{
		[CompilerGenerated]
		get
		{
			return _cmdOK;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOK_Click;
			Button button = _cmdOK;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdOK = value;
			button = _cmdOK;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdCancel
	{
		[CompilerGenerated]
		get
		{
			return _cmdCancel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCancel_Click;
			Button button = _cmdCancel;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdCancel = value;
			button = _cmdCancel;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdDown
	{
		[CompilerGenerated]
		get
		{
			return _cmdDown;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDown_Click;
			Button button = _cmdDown;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDown = value;
			button = _cmdDown;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdDelete
	{
		[CompilerGenerated]
		get
		{
			return _cmdDelete;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelete_Click;
			Button button = _cmdDelete;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDelete = value;
			button = _cmdDelete;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblTitle")]
	internal virtual Label lblTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStrip1")]
	internal virtual ToolStrip ToolStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton mnuOpen
	{
		[CompilerGenerated]
		get
		{
			return _mnuOpen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOpen_Click;
			ToolStripButton toolStripButton = _mnuOpen;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_mnuOpen = value;
			toolStripButton = _mnuOpen;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton mnuSave
	{
		[CompilerGenerated]
		get
		{
			return _mnuSave;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSave_Click;
			ToolStripButton toolStripButton = _mnuSave;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_mnuSave = value;
			toolStripButton = _mnuSave;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton mnuNew
	{
		[CompilerGenerated]
		get
		{
			return _mnuNew;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuNew_Click;
			ToolStripButton toolStripButton = _mnuNew;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_mnuNew = value;
			toolStripButton = _mnuNew;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton mnuSaveAs
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveAs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveAs_Click;
			ToolStripButton toolStripButton = _mnuSaveAs;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_mnuSaveAs = value;
			toolStripButton = _mnuSaveAs;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbglobalfile")]
	internal virtual ComboBox cmbglobalfile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdbrowse
	{
		[CompilerGenerated]
		get
		{
			return _cmdbrowse;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdbrowse_Click;
			Button button = _cmdbrowse;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdbrowse = value;
			button = _cmdbrowse;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdRefresh
	{
		[CompilerGenerated]
		get
		{
			return _cmdRefresh;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRefresh_Click;
			Button button = _cmdRefresh;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdRefresh = value;
			button = _cmdRefresh;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkHTTP")]
	internal virtual CheckBox chkHTTP
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuOpt")]
	internal virtual ToolStripMenuItem mnuOpt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuComm
	{
		[CompilerGenerated]
		get
		{
			return _mnuComm;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuComm_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuComm;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuComm = value;
			toolStripMenuItem = _mnuComm;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuUnComm
	{
		[CompilerGenerated]
		get
		{
			return _mnuUnComm;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuComm_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuUnComm;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuUnComm = value;
			toolStripMenuItem = _mnuUnComm;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public FrmSetGlobals()
	{
		base.FormClosed += FrmsetGlobals_FormClosed;
		base.FormClosing += FrmSetGlobals_FormClosing;
		base.Load += FrmsetGlobals_Load;
		base.Resize += FrmsetGlobals_Resize;
		fRows = 100;
		f_Mode = "G";
		fTitle = "";
		fSaveAs = true;
		fDoSave = false;
		fAddedFileGlobals = false;
		IsLoading = true;
		fGlobals = new Globals_Renamed.gVar_Type[101];
		fNoGlobals = -1;
		fCancel = true;
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmSetGlobals));
		this.GridGlobals = new System.Windows.Forms.DataGridView();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdDelete = new System.Windows.Forms.Button();
		this.lblTitle = new System.Windows.Forms.Label();
		this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
		this.mnuNew = new System.Windows.Forms.ToolStripButton();
		this.mnuOpen = new System.Windows.Forms.ToolStripButton();
		this.mnuSave = new System.Windows.Forms.ToolStripButton();
		this.mnuSaveAs = new System.Windows.Forms.ToolStripButton();
		this.cmbglobalfile = new System.Windows.Forms.ComboBox();
		this.cmdbrowse = new System.Windows.Forms.Button();
		this.cmdRefresh = new System.Windows.Forms.Button();
		this.chkHTTP = new System.Windows.Forms.CheckBox();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuComm = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUnComm = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)this.GridGlobals).BeginInit();
		this.ToolStrip1.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.GridGlobals.AllowUserToAddRows = false;
		this.GridGlobals.AllowUserToResizeRows = false;
		this.GridGlobals.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridGlobals.BackgroundColor = System.Drawing.Color.White;
		this.GridGlobals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridGlobals.Location = new System.Drawing.Point(5, 220);
		this.GridGlobals.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GridGlobals.Name = "GridGlobals";
		this.GridGlobals.RowHeadersWidth = 60;
		this.GridGlobals.Size = new System.Drawing.Size(628, 322);
		this.GridGlobals.StandardTab = true;
		this.GridGlobals.TabIndex = 0;
		this.cmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp.Location = new System.Drawing.Point(643, 220);
		this.cmdUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(81, 31);
		this.cmdUp.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move row up");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(643, 65);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(81, 31);
		this.cmdOK.TabIndex = 8;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(643, 101);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(81, 31);
		this.cmdCancel.TabIndex = 9;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown.Location = new System.Drawing.Point(643, 256);
		this.cmdDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(81, 31);
		this.cmdDown.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move row down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDelete.Location = new System.Drawing.Point(643, 306);
		this.cmdDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.Size = new System.Drawing.Size(80, 31);
		this.cmdDelete.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmdDelete, "Remove row");
		this.cmdDelete.UseVisualStyleBackColor = true;
		this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblTitle.Location = new System.Drawing.Point(5, 68);
		this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(628, 94);
		this.lblTitle.TabIndex = 6;
		this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuNew, this.mnuOpen, this.mnuSave, this.mnuSaveAs });
		this.ToolStrip1.Location = new System.Drawing.Point(0, 42);
		this.ToolStrip1.Name = "ToolStrip1";
		this.ToolStrip1.Size = new System.Drawing.Size(731, 44);
		this.ToolStrip1.TabIndex = 7;
		this.ToolStrip1.Text = "ToolStrip1";
		this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuNew.Image = (System.Drawing.Image)resources.GetObject("mnuNew.Image");
		this.mnuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.mnuNew.Name = "mnuNew";
		this.mnuNew.Size = new System.Drawing.Size(40, 38);
		this.mnuNew.Text = "&New";
		this.mnuNew.ToolTipText = "Clear All Variables";
		this.mnuOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuOpen.Image = (System.Drawing.Image)resources.GetObject("mnuOpen.Image");
		this.mnuOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.mnuOpen.Name = "mnuOpen";
		this.mnuOpen.Size = new System.Drawing.Size(40, 38);
		this.mnuOpen.Text = "&Open";
		this.mnuSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuSave.Image = (System.Drawing.Image)resources.GetObject("mnuSave.Image");
		this.mnuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.mnuSave.Name = "mnuSave";
		this.mnuSave.Size = new System.Drawing.Size(40, 38);
		this.mnuSave.Text = "&Save";
		this.mnuSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuSaveAs.Image = (System.Drawing.Image)resources.GetObject("mnuSaveAs.Image");
		this.mnuSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.mnuSaveAs.Name = "mnuSaveAs";
		this.mnuSaveAs.Size = new System.Drawing.Size(40, 38);
		this.mnuSaveAs.Text = "ToolStripButton1";
		this.mnuSaveAs.ToolTipText = "Save Variables As";
		this.cmbglobalfile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbglobalfile.FormattingEnabled = true;
		this.cmbglobalfile.Location = new System.Drawing.Point(5, 181);
		this.cmbglobalfile.Name = "cmbglobalfile";
		this.cmbglobalfile.Size = new System.Drawing.Size(628, 24);
		this.cmbglobalfile.TabIndex = 4;
		this.cmdbrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdbrowse.Location = new System.Drawing.Point(687, 182);
		this.cmdbrowse.Name = "cmdbrowse";
		this.cmdbrowse.Size = new System.Drawing.Size(37, 31);
		this.cmdbrowse.TabIndex = 7;
		this.cmdbrowse.Text = "...";
		this.ToolTip1.SetToolTip(this.cmdbrowse, "Browse for File Variables");
		this.cmdbrowse.UseVisualStyleBackColor = true;
		this.cmdRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdRefresh.Location = new System.Drawing.Point(643, 182);
		this.cmdRefresh.Name = "cmdRefresh";
		this.cmdRefresh.Size = new System.Drawing.Size(37, 31);
		this.cmdRefresh.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.cmdRefresh, "Refresh File Variables");
		this.cmdRefresh.UseVisualStyleBackColor = true;
		this.chkHTTP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chkHTTP.AutoSize = true;
		this.chkHTTP.Checked = true;
		this.chkHTTP.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHTTP.Location = new System.Drawing.Point(639, 154);
		this.chkHTTP.Name = "chkHTTP";
		this.chkHTTP.Size = new System.Drawing.Size(60, 21);
		this.chkHTTP.TabIndex = 5;
		this.chkHTTP.Text = "Http";
		this.ToolTip1.SetToolTip(this.chkHTTP, "Uncheck to load Globals from a File in a folder");
		this.chkHTTP.UseVisualStyleBackColor = true;
		this.MenuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuOpt });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
		this.MenuStrip1.Size = new System.Drawing.Size(731, 42);
		this.MenuStrip1.TabIndex = 10;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuOpt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuComm, this.mnuUnComm });
		this.mnuOpt.Name = "mnuOpt";
		this.mnuOpt.Size = new System.Drawing.Size(104, 38);
		this.mnuOpt.Text = "Options";
		this.mnuComm.Name = "mnuComm";
		this.mnuComm.Size = new System.Drawing.Size(511, 40);
		this.mnuComm.Tag = "C";
		this.mnuComm.Text = "Comment Variables (Add !)";
		this.mnuUnComm.Name = "mnuUnComm";
		this.mnuUnComm.Size = new System.Drawing.Size(511, 40);
		this.mnuUnComm.Tag = "U";
		this.mnuUnComm.Text = "Uncomment Variables (Remove leading !)";
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(731, 551);
		base.Controls.Add(this.chkHTTP);
		base.Controls.Add(this.cmdRefresh);
		base.Controls.Add(this.cmdbrowse);
		base.Controls.Add(this.cmbglobalfile);
		base.Controls.Add(this.ToolStrip1);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add(this.cmdDelete);
		base.Controls.Add(this.cmdDown);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.cmdUp);
		base.Controls.Add(this.GridGlobals);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmSetGlobals";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Global Variables";
		((System.ComponentModel.ISupportInitialize)this.GridGlobals).EndInit();
		this.ToolStrip1.ResumeLayout(false);
		this.ToolStrip1.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Add_Variables_To_Grid(int MyMode, int StartRow)
	{
		int num = 0;
		bool flag = false;
		checked
		{
			switch (MyMode)
			{
			case 0:
			{
				int gNoGlobals = Globals_Renamed.gNoGlobals;
				for (num = 0; num <= gNoGlobals; num++)
				{
					if (StartRow < fRows - 1)
					{
						StartRow++;
						GridGlobals.Rows[StartRow].Cells[0].Value = Globals_Renamed.gGlobals[num].Variable;
						GridGlobals.Rows[StartRow].Cells[1].Value = Globals_Renamed.gGlobals[num].Value;
						GridGlobals.Rows[StartRow].Cells[2].Value = Globals_Renamed.gGlobals[num].SHValue;
						GridGlobals.Rows[StartRow].Cells[0].Tag = Globals_Renamed.gGlobals[num].GMode;
						if (Operators.CompareString(Globals_Renamed.gGlobals[num].GMode, "F", TextCompare: false) == 0)
						{
							GridGlobals.Rows[StartRow].DefaultCellStyle.BackColor = Color.Honeydew;
						}
						continue;
					}
					flag = true;
					break;
				}
				break;
			}
			case 1:
			{
				int num2 = fNoGlobals;
				for (num = 0; num <= num2; num++)
				{
					if (StartRow < fRows - 1)
					{
						StartRow++;
						GridGlobals.Rows[StartRow].Cells[0].Value = fGlobals[num].Variable;
						GridGlobals.Rows[StartRow].Cells[1].Value = fGlobals[num].Value;
						GridGlobals.Rows[StartRow].Cells[2].Value = fGlobals[num].SHValue;
						GridGlobals.Rows[StartRow].Cells[0].Tag = fGlobals[num].GMode;
						if (Operators.CompareString(fGlobals[num].GMode, "F", TextCompare: false) == 0)
						{
							GridGlobals.Rows[StartRow].DefaultCellStyle.BackColor = Color.Honeydew;
						}
						continue;
					}
					flag = true;
					break;
				}
				break;
			}
			}
			if (flag)
			{
				Interaction.MsgBox("Note that not all Variables were loaded", MsgBoxStyle.Exclamation, "Grid Full");
			}
		}
	}

	public void SaveIt(bool MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		int num6 = default(int);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string left;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1398:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
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
								goto IL_001c;
							case 5:
								goto IL_0021;
							case 6:
								goto IL_002a;
							case 7:
								goto IL_0033;
							case 8:
								goto IL_003c;
							case 9:
								goto IL_004a;
							case 10:
								goto IL_005f;
							case 11:
								goto IL_0082;
							case 13:
								goto IL_009e;
							case 12:
							case 14:
							case 15:
								goto IL_00a9;
							case 16:
								goto IL_00d4;
							case 17:
								goto IL_00e0;
							case 19:
								goto IL_0112;
							case 20:
								goto IL_012c;
							case 21:
								goto IL_015d;
							case 22:
								goto IL_0176;
							case 23:
								goto IL_0186;
							case 24:
							case 25:
								goto IL_01d1;
							case 26:
								goto IL_01e3;
							case 27:
								goto IL_01ff;
							case 28:
								goto IL_020c;
							case 29:
								goto IL_0219;
							case 30:
								goto IL_022c;
							case 31:
								goto IL_0236;
							case 32:
								goto IL_0244;
							case 33:
								goto IL_025d;
							case 35:
								goto IL_0273;
							case 36:
								goto IL_0283;
							case 40:
								goto IL_0296;
							case 41:
								goto IL_02c8;
							case 42:
								goto IL_02e2;
							case 43:
								goto IL_0313;
							case 44:
								goto IL_0361;
							case 45:
							case 46:
								goto IL_0412;
							case 47:
								goto IL_0424;
							case 48:
								goto IL_042e;
							case 49:
								goto IL_043c;
							case 50:
								goto IL_0455;
							case 52:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 18:
							case 34:
							case 37:
							case 38:
							case 39:
							case 51:
							case 53:
							case 54:
							case 55:
							case 56:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0283:
						num2 = 36;
						fDoSave = false;
						goto end_IL_0001_3;
						IL_012c:
						num2 = 20;
						text = Strings.Trim(Conversions.ToString(GridGlobals.Rows[num5].Cells[0].Value));
						goto IL_015d;
						IL_0273:
						num2 = 35;
						Set_Query_In_Title(fTitle);
						goto IL_0283;
						IL_015d:
						num2 = 21;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_0176;
						}
						goto IL_01d1;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						text3 = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						num5 = 0;
						goto IL_0021;
						IL_0021:
						num2 = 5;
						text4 = "";
						goto IL_002a;
						IL_002a:
						num2 = 6;
						text = "";
						goto IL_0033;
						IL_0033:
						num2 = 7;
						text5 = ",";
						goto IL_003c;
						IL_003c:
						num2 = 8;
						GridGlobals.EndEdit();
						goto IL_004a;
						IL_004a:
						num2 = 9;
						if (fSaveAs || MyMode)
						{
							goto IL_005f;
						}
						goto IL_009e;
						IL_0176:
						num2 = 22;
						text4 = text4 + text5 + text;
						goto IL_0186;
						IL_009e:
						num2 = 13;
						text2 = fTitle;
						goto IL_00a9;
						IL_005f:
						num2 = 10;
						BuildForm.FileOpenSave("S", fTitle, "csv+", "Specify File to Store Variables", Globals_Renamed.gQueryDir);
						goto IL_0082;
						IL_0082:
						num2 = 11;
						text2 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
						goto IL_00a9;
						IL_00a9:
						num2 = 15;
						if (!((Operators.CompareString(text2, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(text2, "", TextCompare: false) != 0)))
						{
							goto end_IL_0001_3;
						}
						goto IL_00d4;
						IL_00d4:
						num2 = 16;
						text5 = BuildForm.GetFileDLM(text2);
						goto IL_00e0;
						IL_00e0:
						num2 = 17;
						left = f_Mode;
						if (Operators.CompareString(left, "E", TextCompare: false) == 0)
						{
							goto IL_0112;
						}
						if (Operators.CompareString(left, "G", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0296;
						IL_0186:
						num2 = 23;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + text5 + "\"", GridGlobals.Rows[num5].Cells[1].Value), "\""));
						goto IL_01d1;
						IL_0296:
						num2 = 40;
						text3 = "variable" + text5 + "value" + text5 + "sh_value";
						goto IL_02c8;
						IL_02c8:
						num2 = 41;
						num6 = GridGlobals.RowCount - 1;
						num5 = 0;
						goto IL_041b;
						IL_041b:
						if (num5 <= num6)
						{
							goto IL_02e2;
						}
						goto IL_0424;
						IL_0424:
						num2 = 47;
						fTitle = text2;
						goto IL_042e;
						IL_042e:
						num2 = 48;
						text4 = General_Procedures.Save_File_General(text3, text2);
						goto IL_043c;
						IL_043c:
						num2 = 49;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0455;
						IL_0455:
						num2 = 50;
						Interaction.MsgBox(text4, MsgBoxStyle.Exclamation, "Could not Save Variables");
						goto end_IL_0001_3;
						IL_02e2:
						num2 = 42;
						text = Strings.Trim(Conversions.ToString(GridGlobals.Rows[num5].Cells[0].Value));
						goto IL_0313;
						IL_0313:
						num2 = 43;
						if (Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num5].Cells[0].Tag, "F", TextCompare: false) && Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_0361;
						}
						goto IL_0412;
						IL_01d1:
						num2 = 25;
						num5++;
						goto IL_01da;
						IL_0361:
						num2 = 44;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + "\r\n\"" + text + "\"" + text5 + "\"", GridGlobals.Rows[num5].Cells[1].Value), "\""), text5), "\""), GridGlobals.Rows[num5].Cells[2].Value), "\""));
						goto IL_0412;
						IL_0412:
						num2 = 46;
						num5++;
						goto IL_041b;
						IL_0112:
						num2 = 19;
						num7 = GridGlobals.RowCount - 1;
						num5 = 0;
						goto IL_01da;
						IL_01da:
						if (num5 <= num7)
						{
							goto IL_012c;
						}
						goto IL_01e3;
						IL_01e3:
						num2 = 26;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_01ff;
						IL_01ff:
						num2 = 27;
						text4 = Strings.Mid(text4, 2);
						goto IL_020c;
						IL_020c:
						num2 = 28;
						text3 = Strings.Mid(text3, 2);
						goto IL_0219;
						IL_0219:
						num2 = 29;
						text4 = text4 + "\r\n" + text3;
						goto IL_022c;
						IL_022c:
						num2 = 30;
						fTitle = text2;
						goto IL_0236;
						IL_0236:
						num2 = 31;
						text3 = General_Procedures.Save_File_General(text4, text2);
						goto IL_0244;
						IL_0244:
						num2 = 32;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_025d;
						}
						goto IL_0273;
						IL_025d:
						num2 = 33;
						Interaction.MsgBox(text3, MsgBoxStyle.Exclamation, "Could not Save Variables");
						goto end_IL_0001_3;
						end_IL_0001_2:
						break;
					}
					num2 = 52;
					Set_Query_In_Title(fTitle);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1398;
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

	public void Set_Query_In_Title(string FileName)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		short num5 = default(short);
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
				case 424:
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
							goto IL_002f;
						case 6:
							goto IL_0038;
						case 7:
							goto IL_0054;
						case 9:
							goto IL_0065;
						case 8:
						case 10:
						case 11:
							goto IL_0074;
						case 13:
							goto IL_0082;
						case 14:
							goto IL_0094;
						case 15:
							goto IL_00a1;
						case 17:
							goto IL_00ab;
						case 16:
						case 18:
						case 19:
							goto IL_00ba;
						case 20:
							goto IL_00c4;
						case 12:
						case 21:
						case 22:
							goto IL_00cf;
						case 23:
							goto IL_00ec;
						case 25:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 24:
						case 26:
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c4:
					num2 = 20;
					fSaveAs = false;
					goto IL_00cf;
					IL_00cf:
					num2 = 22;
					if (Operators.CompareString(f_Mode, "E", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00ec;
					IL_00ba:
					num2 = 19;
					fTitle = FileName;
					goto IL_00c4;
					IL_00ec:
					num2 = 23;
					Text = "Editor - [" + text + "]";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (Operators.CompareString(FileName, "", TextCompare: false) == 0)
					{
						goto IL_002f;
					}
					goto IL_0082;
					IL_002f:
					num2 = 5;
					text = "Untitled";
					goto IL_0038;
					IL_0038:
					num2 = 6;
					if (Operators.CompareString(f_Mode, "E", TextCompare: false) == 0)
					{
						goto IL_0054;
					}
					goto IL_0065;
					IL_0054:
					num2 = 7;
					fTitle = "Last_Extract_File.csv";
					goto IL_0074;
					IL_0065:
					num2 = 9;
					fTitle = "globals.csv";
					goto IL_0074;
					IL_0074:
					num2 = 11;
					fSaveAs = true;
					goto IL_00cf;
					IL_0082:
					num2 = 13;
					num5 = checked((short)Strings.InStrRev(FileName, "\\"));
					goto IL_0094;
					IL_0094:
					num2 = 14;
					if (num5 == 0)
					{
						goto IL_00a1;
					}
					goto IL_00ab;
					IL_00a1:
					num2 = 15;
					text = FileName;
					goto IL_00ba;
					IL_00ab:
					num2 = 17;
					text = Strings.Mid(FileName, checked(num5 + 1));
					goto IL_00ba;
					end_IL_0001_2:
					break;
				}
				num2 = 25;
				Text = "Global Variables - [" + text + "]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 424;
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

	public void Set_Col_Width()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
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
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 199:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_000b;
							case 3:
								goto IL_001c;
							case 4:
								goto IL_0021;
							case 5:
								goto IL_0026;
							case 6:
								goto IL_003c;
							case 7:
								goto IL_005a;
							case 8:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 9:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0062:
						if (num5 > num6)
						{
							break;
						}
						goto IL_003c;
						IL_003c:
						num2 = 6;
						num7 += GridGlobals.Columns[num5].Width;
						goto IL_005a;
						IL_0026:
						num2 = 5;
						num6 = GridGlobals.ColumnCount - 2;
						num5 = 0;
						goto IL_0062;
						IL_005a:
						num2 = 7;
						num5++;
						goto IL_0062;
						IL_000b:
						num2 = 2;
						num8 = GridGlobals.Width - 85;
						goto IL_001c;
						IL_001c:
						num2 = 3;
						num5 = 0;
						goto IL_0021;
						IL_0021:
						num2 = 4;
						num7 = 0;
						goto IL_0026;
						end_IL_0001_2:
						break;
					}
					num2 = 8;
					GridGlobals.Columns[GridGlobals.ColumnCount - 1].Width = num8 - num7;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 199;
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

	private void FrmsetGlobals_FormClosed(object sender, FormClosedEventArgs e)
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

	private void FrmSetGlobals_FormClosing(object sender, FormClosingEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 222:
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
							goto IL_002b;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0059;
						case 10:
							goto IL_0069;
						case 11:
							goto IL_007b;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0059:
					num2 = 7;
					SaveIt(MyMode: false);
					goto end_IL_0001_3;
					IL_0069:
					num2 = 10;
					if (fCancel)
					{
						break;
					}
					goto IL_007b;
					IL_004d:
					num2 = 6;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0059;
					IL_007b:
					num2 = 11;
					e.Cancel = true;
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(f_Mode, "E", TextCompare: false) == 0)
					{
						goto IL_002b;
					}
					goto IL_0069;
					IL_002b:
					num2 = 4;
					if (!fDoSave)
					{
						goto end_IL_0001_3;
					}
					goto IL_0039;
					IL_0039:
					num2 = 5;
					num5 = (int)Interaction.MsgBox("Do you wish to save changes made on this form?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Save Changes?");
					goto IL_004d;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				fCancel = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 222;
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

	private void FrmsetGlobals_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string left;
					ComboBox MyCmb;
					Button MyButton;
					DataGridView MyGrid;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1821:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
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
								goto IL_0014;
							case 5:
								goto IL_0034;
							case 6:
								goto IL_0054;
							case 7:
								goto IL_0075;
							case 9:
								goto IL_00a6;
							case 10:
								goto IL_00c2;
							case 11:
								goto IL_00da;
							case 12:
								goto IL_00e4;
							case 13:
								goto IL_00f8;
							case 14:
								goto IL_010c;
							case 15:
								goto IL_012d;
							case 16:
								goto IL_0141;
							case 17:
								goto IL_0151;
							case 18:
								goto IL_0170;
							case 19:
								goto IL_018f;
							case 20:
								goto IL_01ae;
							case 21:
								goto IL_01ed;
							case 23:
								goto IL_0201;
							case 26:
								goto IL_0218;
							case 27:
								goto IL_0228;
							case 28:
								goto IL_0238;
							case 29:
								goto IL_024c;
							case 30:
								goto IL_0260;
							case 31:
								goto IL_027c;
							case 32:
								goto IL_0298;
							case 33:
								goto IL_02b5;
							case 34:
								goto IL_02c5;
							case 35:
								goto IL_02d5;
							case 36:
								goto IL_02e5;
							case 37:
								goto IL_02f5;
							case 38:
								goto IL_0305;
							case 39:
								goto IL_0324;
							case 8:
							case 22:
							case 24:
							case 25:
							case 40:
							case 41:
								goto IL_0346;
							case 42:
								goto IL_035c;
							case 43:
								goto IL_0382;
							case 44:
								goto IL_038e;
							case 45:
								goto IL_03a3;
							case 46:
								goto IL_03bf;
							case 47:
								goto IL_03db;
							case 48:
								goto IL_03ee;
							case 49:
								goto IL_040e;
							case 50:
								goto IL_041e;
							case 51:
								goto IL_042e;
							case 52:
								goto IL_043e;
							case 53:
								goto IL_044e;
							case 54:
								goto IL_0468;
							case 55:
								goto IL_047c;
							case 56:
								goto IL_048b;
							case 57:
								goto IL_049f;
							case 58:
								goto IL_04b3;
							case 59:
								goto IL_04c1;
							case 60:
								goto IL_04ee;
							case 62:
								goto IL_0522;
							case 63:
								goto IL_0536;
							case 64:
								goto IL_0542;
							case 65:
								goto IL_0550;
							case 66:
								goto IL_057d;
							case 67:
								goto IL_05aa;
							case 61:
							case 68:
							case 69:
								goto IL_05d8;
							case 70:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 71:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_057d:
						num2 = 66;
						GridGlobals.Columns[1].Width = (int)Math.Round((double)num5 * 0.4);
						goto IL_05aa;
						IL_05aa:
						num2 = 67;
						GridGlobals.Columns[2].Width = (int)Math.Round((double)num5 * 0.4);
						goto IL_05d8;
						IL_0550:
						num2 = 65;
						GridGlobals.Columns[0].Width = (int)Math.Round((double)num5 * 0.2);
						goto IL_057d;
						IL_05d8:
						num2 = 69;
						IsLoading = false;
						break;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num5 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						MyButton = cmdDelete;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						cmdDelete = MyButton;
						goto IL_0034;
						IL_0034:
						num2 = 5;
						MyButton = cmdUp;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						cmdUp = MyButton;
						goto IL_0054;
						IL_0054:
						num2 = 6;
						MyButton = cmdDown;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						cmdDown = MyButton;
						goto IL_0075;
						IL_0075:
						num2 = 7;
						left = f_Mode;
						if (Operators.CompareString(left, "G", TextCompare: false) == 0)
						{
							goto IL_00a6;
						}
						if (Operators.CompareString(left, "E", TextCompare: false) == 0)
						{
							goto IL_0218;
						}
						goto IL_0346;
						IL_035c:
						num2 = 42;
						GridGlobals.Columns[num6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
						goto IL_0382;
						IL_0218:
						num2 = 26;
						mnuComm.Visible = false;
						goto IL_0228;
						IL_0228:
						num2 = 27;
						mnuUnComm.Visible = false;
						goto IL_0238;
						IL_0238:
						num2 = 28;
						mnuOpen.ToolTipText = "Read variables from a File\"";
						goto IL_024c;
						IL_024c:
						num2 = 29;
						mnuSave.ToolTipText = "Save variables to a File";
						goto IL_0260;
						IL_0260:
						num2 = 30;
						num5 = GridGlobals.Top - cmbglobalfile.Top;
						goto IL_027c;
						IL_027c:
						num2 = 31;
						GridGlobals.Top = cmbglobalfile.Top - 5;
						goto IL_0298;
						IL_0298:
						num2 = 32;
						GridGlobals.Height += num5;
						goto IL_02b5;
						IL_02b5:
						num2 = 33;
						cmbglobalfile.Visible = false;
						goto IL_02c5;
						IL_02c5:
						num2 = 34;
						cmdbrowse.Visible = false;
						goto IL_02d5;
						IL_02d5:
						num2 = 35;
						cmdRefresh.Visible = false;
						goto IL_02e5;
						IL_02e5:
						num2 = 36;
						chkHTTP.Visible = false;
						goto IL_02f5;
						IL_02f5:
						num2 = 37;
						GridGlobals.ColumnCount = 2;
						goto IL_0305;
						IL_0305:
						num2 = 38;
						GridGlobals.Columns[0].HeaderText = "Variable";
						goto IL_0324;
						IL_0324:
						num2 = 39;
						GridGlobals.Columns[1].HeaderText = "Value";
						goto IL_0346;
						IL_00a6:
						num2 = 9;
						MyCmb = cmbglobalfile;
						BuildForm.Load_HTTP_Combo(ref MyCmb);
						cmbglobalfile = MyCmb;
						goto IL_00c2;
						IL_00c2:
						num2 = 10;
						Array.Clear(fGlobals, 0, fGlobals.Length);
						goto IL_00da;
						IL_00da:
						num2 = 11;
						fNoGlobals = -1;
						goto IL_00e4;
						IL_00e4:
						num2 = 12;
						mnuOpen.ToolTipText = "Read Memory variables from a File";
						goto IL_00f8;
						IL_00f8:
						num2 = 13;
						mnuSave.ToolTipText = "Save Memory variables to a File";
						goto IL_010c;
						IL_010c:
						num2 = 14;
						MyButton = cmdRefresh;
						BuildForm.Set_Btn_Img(ref MyButton, "refresh");
						cmdRefresh = MyButton;
						goto IL_012d;
						IL_012d:
						num2 = 15;
						cmbglobalfile.Text = Globals_Renamed.gGlobalsFile;
						goto IL_0141;
						IL_0141:
						num2 = 16;
						GridGlobals.ColumnCount = 3;
						goto IL_0151;
						IL_0151:
						num2 = 17;
						GridGlobals.Columns[0].HeaderText = "Variable";
						goto IL_0170;
						IL_0170:
						num2 = 18;
						GridGlobals.Columns[1].HeaderText = "Value";
						goto IL_018f;
						IL_018f:
						num2 = 19;
						GridGlobals.Columns[2].HeaderText = "ScriptHost Value";
						goto IL_01ae;
						IL_01ae:
						num2 = 20;
						if (Operators.CompareString(cmbglobalfile.Text, "", TextCompare: false) == 0 || LikeOperator.LikeString(Strings.LCase(cmbglobalfile.Text), "http*", CompareMethod.Binary))
						{
							goto IL_01ed;
						}
						goto IL_0201;
						IL_0382:
						num2 = 43;
						num6++;
						goto IL_0389;
						IL_0201:
						num2 = 23;
						chkHTTP.Checked = false;
						goto IL_0346;
						IL_01ed:
						num2 = 21;
						chkHTTP.Checked = true;
						goto IL_0346;
						IL_0346:
						num2 = 41;
						num7 = GridGlobals.ColumnCount - 1;
						num6 = 0;
						goto IL_0389;
						IL_0389:
						if (num6 <= num7)
						{
							goto IL_035c;
						}
						goto IL_038e;
						IL_038e:
						num2 = 44;
						GridGlobals.RowCount = fRows;
						goto IL_03a3;
						IL_03a3:
						num2 = 45;
						MyGrid = GridGlobals;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridGlobals = MyGrid;
						goto IL_03bf;
						IL_03bf:
						num2 = 46;
						MyGrid = GridGlobals;
						GridModule.Number_Grid(ref MyGrid);
						GridGlobals = MyGrid;
						goto IL_03db;
						IL_03db:
						num2 = 47;
						num5 = GridGlobals.Width - 85;
						goto IL_03ee;
						IL_03ee:
						num2 = 48;
						if (Operators.CompareString(f_Mode, "E", TextCompare: false) == 0)
						{
							goto IL_040e;
						}
						goto IL_0522;
						IL_040e:
						num2 = 49;
						cmdUp.Visible = false;
						goto IL_041e;
						IL_041e:
						num2 = 50;
						cmdDown.Visible = false;
						goto IL_042e;
						IL_042e:
						num2 = 51;
						cmdDelete.Visible = false;
						goto IL_043e;
						IL_043e:
						num2 = 52;
						cmdOK.Visible = false;
						goto IL_044e;
						IL_044e:
						num2 = 53;
						cmdCancel.Top = cmdOK.Top;
						goto IL_0468;
						IL_0468:
						num2 = 54;
						cmdCancel.Text = "Close";
						goto IL_047c;
						IL_047c:
						num2 = 55;
						Text = "Editor";
						goto IL_048b;
						IL_048b:
						num2 = 56;
						lblTitle.Text = "Create a Last Extract Timestamp File or edit an existing one. Click the NEW icon to initialize variables, and/or click the OPEN and SAVE icons to load a Timestamp File or to save your edits.";
						goto IL_049f;
						IL_049f:
						num2 = 57;
						mnuNew.ToolTipText = "Initialize a series of Timestamp Variables for use with SmartAppend or otherwise";
						goto IL_04b3;
						IL_04b3:
						num2 = 58;
						fTitle = "last_extract_file.csv";
						goto IL_04c1;
						IL_04c1:
						num2 = 59;
						GridGlobals.Columns[0].Width = (int)Math.Round((double)num5 * 0.25);
						goto IL_04ee;
						IL_04ee:
						num2 = 60;
						GridGlobals.Columns[1].Width = (int)Math.Round((double)num5 * 0.75);
						goto IL_05d8;
						IL_0522:
						num2 = 62;
						lblTitle.Text = "Enter global variables and their interactive and opt ScriptHost translations. Variables should not start with spf. When using variables, bound with <<< >>>,  e.g., <<<Lot>>>. File variables (green background) can be loaded from the web or a share, are not editable and are ignored when saving. To load file variables, specify path and click Refresh and to clear them, clear path and click Refresh. If nesting variables, note that translation occurs bottom up";
						goto IL_0536;
						IL_0536:
						num2 = 63;
						Add_Variables_To_Grid(0, -1);
						goto IL_0542;
						IL_0542:
						num2 = 64;
						fTitle = "Globals.csv";
						goto IL_0550;
						end_IL_0001_2:
						break;
					}
					num2 = 70;
					Set_Col_Width();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1821;
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

	private void FrmsetGlobals_Resize(object sender, EventArgs e)
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
				Set_Col_Width();
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

	private void cmdUp_Click(object sender, EventArgs e)
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
				case 66:
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
				DataGridView MyGrid = GridGlobals;
				GridModule.Grid_Up(ref MyGrid);
				GridGlobals = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 66;
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

	private void cmdDown_Click(object sender, EventArgs e)
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
				case 66:
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
				DataGridView MyGrid = GridGlobals;
				GridModule.Grid_Down(ref MyGrid);
				GridGlobals = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 66;
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

	private void cmdDelete_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					DataGridView MyGrid;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 388:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
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
								goto IL_0014;
							case 6:
								goto IL_003b;
							case 7:
								goto IL_0058;
							case 8:
								goto IL_0072;
							case 9:
								goto IL_00a7;
							case 11:
							case 12:
								goto IL_00d3;
							case 13:
								goto IL_00e2;
							case 14:
								goto IL_00fe;
							case 15:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 5:
							case 10:
							case 16:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0072:
						num2 = 8;
						if (Operators.ConditionalCompareObjectEqual(GridGlobals.Rows[num5].Cells[0].Tag, "F", TextCompare: false))
						{
							goto IL_00a7;
						}
						goto IL_00d3;
						IL_00a7:
						num2 = 9;
						Interaction.MsgBox("You cannot delete cells containing File Global Variables. E.g., row" + Conversions.ToString(num5 + 1) + ". Edit the external file instead", MsgBoxStyle.Exclamation, "Error");
						goto end_IL_0001_3;
						IL_0058:
						num2 = 7;
						num5 = GridGlobals.SelectedCells[num6].RowIndex;
						goto IL_0072;
						IL_00d3:
						num2 = 12;
						num6++;
						goto IL_00da;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num5 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						if (Information.IsNothing(GridGlobals.CurrentCell.RowIndex))
						{
							goto end_IL_0001_3;
						}
						goto IL_003b;
						IL_003b:
						num2 = 6;
						num7 = GridGlobals.SelectedCells.Count - 1;
						num6 = 0;
						goto IL_00da;
						IL_00da:
						if (num6 <= num7)
						{
							goto IL_0058;
						}
						goto IL_00e2;
						IL_00e2:
						num2 = 13;
						MyGrid = GridGlobals;
						GridModule.Grid_Delete_Multi(ref MyGrid);
						GridGlobals = MyGrid;
						goto IL_00fe;
						IL_00fe:
						num2 = 14;
						GridGlobals.RowCount = fRows;
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 15;
					MyGrid = GridGlobals;
					GridModule.Number_Grid(ref MyGrid);
					GridGlobals = MyGrid;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 388;
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

	private void GridGlobals_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (GridGlobals.CurrentRow.Index == -1 || Operators.ConditionalCompareObjectEqual(GridGlobals.CurrentRow.Cells[0].Tag, "F", TextCompare: false))
		{
			e.Cancel = true;
		}
	}

	private void GridGlobals_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (!IsLoading)
		{
			fDoSave = true;
			Application.DoEvents();
		}
	}

	private void GridGlobals_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
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
				Set_Col_Width();
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

	private void cmdCancel_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 188:
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
							goto IL_002b;
						case 5:
							goto IL_0044;
						case 6:
							goto IL_0058;
						case 7:
							goto IL_0064;
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0064:
					num2 = 7;
					fCancel = false;
					break;
					IL_0044:
					num2 = 5;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to close without saving variables?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Close?");
					goto IL_0058;
					IL_002b:
					num2 = 4;
					if (!fAddedFileGlobals && !fDoSave)
					{
						break;
					}
					goto IL_0044;
					IL_0058:
					num2 = 6;
					if (num5 != 7)
					{
						break;
					}
					goto IL_0064;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(f_Mode, "G", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_002b;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 188;
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

	private void cmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
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
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 591:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
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
								goto IL_0034;
							case 7:
								goto IL_0063;
							case 8:
								goto IL_007e;
							case 9:
								goto IL_008c;
							case 10:
								goto IL_00c1;
							case 12:
								goto IL_00e1;
							case 11:
							case 13:
							case 14:
								goto IL_00fe;
							case 15:
								goto IL_0117;
							case 16:
								goto IL_0154;
							case 17:
							case 18:
								goto IL_0193;
							case 19:
								goto IL_01a2;
							case 20:
								goto IL_01bf;
							case 21:
							case 22:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 23:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0117:
						num2 = 15;
						Globals_Renamed.gGlobals[Globals_Renamed.gNoGlobals].Value = Conversions.ToString(GridGlobals.Rows[num5].Cells[1].Value);
						goto IL_0154;
						IL_0154:
						num2 = 16;
						Globals_Renamed.gGlobals[Globals_Renamed.gNoGlobals].SHValue = Conversions.ToString(GridGlobals.Rows[num5].Cells[2].Value);
						goto IL_0193;
						IL_00fe:
						num2 = 14;
						Globals_Renamed.gGlobals[Globals_Renamed.gNoGlobals].Variable = text;
						goto IL_0117;
						IL_0193:
						num2 = 18;
						num5++;
						goto IL_019a;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						text = "";
						goto IL_0018;
						IL_0018:
						num2 = 4;
						BuildForm.Clear_Global_Vars(0);
						goto IL_0021;
						IL_0021:
						num2 = 5;
						num6 = fRows - 1;
						num5 = 0;
						goto IL_019a;
						IL_019a:
						if (num5 <= num6)
						{
							goto IL_0034;
						}
						goto IL_01a2;
						IL_01a2:
						num2 = 19;
						if (Operators.CompareString(f_Mode, "G", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_01bf;
						IL_01bf:
						num2 = 20;
						Globals_Renamed.gGlobalsFile = cmbglobalfile.Text;
						break;
						IL_0034:
						num2 = 6;
						text = Strings.Trim(Conversions.ToString(GridGlobals.Rows[num5].Cells[0].Value));
						goto IL_0063;
						IL_0063:
						num2 = 7;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_007e;
						}
						goto IL_0193;
						IL_007e:
						num2 = 8;
						Globals_Renamed.gNoGlobals++;
						goto IL_008c;
						IL_008c:
						num2 = 9;
						if (Operators.ConditionalCompareObjectEqual(GridGlobals.Rows[num5].Cells[0].Tag, "F", TextCompare: false))
						{
							goto IL_00c1;
						}
						goto IL_00e1;
						IL_00c1:
						num2 = 10;
						Globals_Renamed.gGlobals[Globals_Renamed.gNoGlobals].GMode = "F";
						goto IL_00fe;
						IL_00e1:
						num2 = 12;
						Globals_Renamed.gGlobals[Globals_Renamed.gNoGlobals].GMode = "M";
						goto IL_00fe;
						end_IL_0001_2:
						break;
					}
					num2 = 22;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 591;
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

	private void mnuSave_Click(object sender, EventArgs e)
	{
		SaveIt(MyMode: false);
	}

	private void mnuNew_Click(object sender, EventArgs e)
	{
		int num = 0;
		int num2 = 0;
		string left = f_Mode;
		if (Operators.CompareString(left, "G", TextCompare: false) != 0)
		{
			if (Operators.CompareString(left, "E", TextCompare: false) == 0)
			{
				num = (int)((!fDoSave) ? MsgBoxResult.Yes : Interaction.MsgBox("Are you sure you wish to initialize the Time Variables?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Initialize?"));
				checked
				{
					if (num == 6)
					{
						DataGridView MyGrid = GridGlobals;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridGlobals = MyGrid;
						DateTime now = DateAndTime.Now;
						int num3 = -1;
						string style = "yyyy-MM-dd HH:mm:ss";
						string style2 = "yyyy-MM-dd";
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(now, style);
						num2 = 15;
						do
						{
							if (unchecked(num2 != 75 && num2 != 105))
							{
								num3++;
								GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-" + Conversions.ToString(num2) + "m";
								GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("n", -1.0 * (double)num2, now), style);
							}
							num2 += 15;
						}
						while (num2 <= 120);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-1d";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -24.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-2d";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -48.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-7d";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -168.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-6h";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -6.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-8h";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -8.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-12h";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -12.0, now), style);
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-1d-Midnight";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -24.0, now), style2) + " 00:00:00";
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-2d-Midnight";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -48.0, now), style2) + " 00:00:00";
						num3++;
						GridGlobals.Rows[num3].Cells[0].Value = "Last_Date-7d-Midnight";
						GridGlobals.Rows[num3].Cells[1].Value = Strings.Format(DateAndTime.DateAdd("h", -168.0, now), style2) + " 00:00:00";
						Set_Query_In_Title("");
						fDoSave = false;
					}
				}
			}
		}
		else
		{
			num = (int)((!fDoSave && !fAddedFileGlobals) ? MsgBoxResult.Yes : Interaction.MsgBox("Are you sure you wish to clear all variables?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear?"));
			checked
			{
				if (num == 6)
				{
					DataGridView MyGrid = GridGlobals;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridGlobals = MyGrid;
					Set_Query_In_Title("");
					int num4 = fRows - 1;
					for (num2 = 0; num2 <= num4; num2++)
					{
						GridGlobals.Rows[num2].DefaultCellStyle.BackColor = Color.White;
					}
					fDoSave = false;
				}
			}
		}
		fDoSave = true;
	}

	private void mnuOpen_Click(object sender, EventArgs e)
	{
		int num = 7;
		int num2 = 0;
		bool flag = false;
		string text = "";
		object obj = null;
		string text2 = "";
		int num3 = 0;
		checked
		{
			int num4 = GridGlobals.RowCount - 1;
			for (num2 = 0; num2 <= num4; num2++)
			{
				if (Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num2].Cells[0].Value, "", TextCompare: false) || Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num2].Cells[1].Value, "", TextCompare: false))
				{
					flag = true;
					break;
				}
			}
			num = unchecked((int)((!flag || !fDoSave) ? MsgBoxResult.Yes : Interaction.MsgBox("Are you sure you wish to update variables?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Update?")));
			if (num != 6)
			{
				return;
			}
			BuildForm.FileOpenSave("O", "", "csv+", "Load Variables from a File", Globals_Renamed.gQueryDir);
			if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) != 0))
			{
				fTitle = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
				string left = f_Mode;
				if (Operators.CompareString(left, "G", TextCompare: false) != 0)
				{
					if (Operators.CompareString(left, "E", TextCompare: false) == 0)
					{
						text = BuildForm.GetFileDLM(fTitle);
						obj = new TextFieldParser(fTitle);
						NewLateBinding.LateSet(obj, null, "TextFieldType", new object[1] { FieldType.Delimited }, null, null);
						object instance = obj;
						object[] obj2 = new object[1] { text };
						object[] array = obj2;
						bool[] obj3 = new bool[1] { true };
						bool[] array2 = obj3;
						NewLateBinding.LateCall(instance, null, "SetDelimiters", obj2, null, null, obj3, IgnoreReturn: true);
						if (array2[0])
						{
							text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
						}
						NewLateBinding.LateSet(obj, null, "HasFieldsEnclosedInQuotes", new object[1] { true }, null, null);
						if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(obj, null, "EndOfData", new object[0], null, null, null))))
						{
							try
							{
								string[] array3 = (string[])NewLateBinding.LateGet(obj, null, "ReadFields", new object[0], null, null, null);
								int num5 = Information.UBound(array3);
								for (num2 = 0; num2 <= num5 && num2 < fRows; num2++)
								{
									GridGlobals.Rows[num2].Cells[0].Value = array3[num2];
								}
								array3 = null;
								if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(obj, null, "EndOfData", new object[0], null, null, null))))
								{
									array3 = (string[])NewLateBinding.LateGet(obj, null, "ReadFields", new object[0], null, null, null);
									int num6 = Information.UBound(array3);
									for (num2 = 0; num2 <= num6 && num2 < fRows; num2++)
									{
										GridGlobals.Rows[num2].Cells[1].Value = array3[num2];
									}
								}
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								Interaction.MsgBox("Error accessing: " + fTitle + ": " + ex2.Message, MsgBoxStyle.Exclamation, "Access Error");
								ProjectData.ClearProjectError();
							}
							finally
							{
								string[] array3 = null;
							}
						}
						NewLateBinding.LateCall(obj, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						NewLateBinding.LateCall(obj, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
					}
				}
				else
				{
					text2 = BuildForm.Get_Globals_in_File(fTitle, ref fGlobals, ref fNoGlobals, "M");
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, "Error Accessing File");
						return;
					}
					int num7 = fRows - 1;
					for (num2 = num7; num2 >= 0; num2 += -1)
					{
						if (Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num2].Cells[0].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num2].Cells[0].Tag, "F", TextCompare: false))
						{
							GridGlobals.Rows.RemoveAt(num2);
						}
					}
					GridGlobals.RowCount = fRows;
					num3 = -1;
					int num8 = fRows - 1;
					for (num2 = num8; num2 >= 0; num2 += -1)
					{
						if (Operators.ConditionalCompareObjectEqual(GridGlobals.Rows[num2].Cells[0].Tag, "F", TextCompare: false))
						{
							num3 = num2;
							break;
						}
					}
					Add_Variables_To_Grid(1, num3);
				}
				Set_Query_In_Title(fTitle);
			}
			fDoSave = false;
		}
	}

	private void mnuSaveAs_Click(object sender, EventArgs e)
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
				case 51:
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
				SaveIt(MyMode: true);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 51;
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

	private void cmdbrowse_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string myHTML = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 297:
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
							goto IL_0027;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_003f;
						case 8:
							goto IL_0054;
						case 9:
							goto IL_0075;
						case 10:
							goto IL_008e;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 12:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0054:
					num2 = 8;
					BuildForm.FileOpenSave("O", "", "csv+", "CSV File with Globals", Globals_Renamed.gQueryDir);
					goto IL_0075;
					IL_0075:
					num2 = 9;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_008e;
					IL_003f:
					num2 = 6;
					BuildForm.Get_HTTP_Help(myHTML, "Batch_Options.htm#retention");
					goto end_IL_0001_3;
					IL_008e:
					num2 = 10;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						break;
					}
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					myHTML = Strings.Trim(cmbglobalfile.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					text = "";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					num5 = 0;
					goto IL_002c;
					IL_002c:
					num2 = 5;
					if (chkHTTP.Checked)
					{
						goto IL_003f;
					}
					goto IL_0054;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				cmbglobalfile.Text = BuildForm.Strip_Add_MyPCDir("S", text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 297;
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

	private void cmdRefresh_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num5 = default(int);
		bool flag = default(bool);
		int num6 = default(int);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num7;
					DataGridView MyGrid;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1186:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001;
							}
							int num4 = unchecked(num + 1);
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
								goto IL_0027;
							case 5:
								goto IL_002c;
							case 6:
								goto IL_0031;
							case 7:
								goto IL_0044;
							case 8:
								goto IL_0049;
							case 9:
								goto IL_0063;
							case 10:
								goto IL_0079;
							case 12:
								goto IL_008c;
							case 13:
								goto IL_0096;
							case 14:
								goto IL_00a9;
							case 15:
								goto IL_00df;
							case 16:
							case 17:
								goto IL_00f7;
							case 18:
								goto IL_0105;
							case 19:
								goto IL_011a;
							case 21:
								goto IL_013d;
							case 22:
								goto IL_015a;
							case 23:
								goto IL_0176;
							case 24:
								goto IL_0180;
							case 25:
								goto IL_0195;
							case 26:
								goto IL_0200;
							case 27:
								goto IL_0219;
							case 28:
								goto IL_022a;
							case 29:
								goto IL_026a;
							case 30:
								goto IL_02aa;
							case 31:
								goto IL_02ea;
							case 33:
								goto IL_030c;
							case 32:
							case 35:
							case 36:
							case 37:
								goto IL_0317;
							case 34:
							case 38:
								goto IL_0329;
							case 39:
								goto IL_0334;
							case 40:
								goto IL_034a;
							case 42:
							case 43:
								goto IL_035c;
							case 44:
								goto IL_036c;
							case 45:
								goto IL_0381;
							case 46:
								goto IL_038d;
							case 48:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 11:
							case 20:
							case 41:
							case 47:
							case 49:
							case 50:
							case 51:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_036c:
						num2 = 44;
						GridGlobals.RowCount = fRows;
						goto IL_0381;
						IL_0381:
						num2 = 45;
						Add_Variables_To_Grid(1, -1);
						goto IL_038d;
						IL_035c:
						num2 = 43;
						GridGlobals.RowCount = 0;
						goto IL_036c;
						IL_038d:
						num2 = 46;
						MyGrid = GridGlobals;
						GridModule.Number_Grid(ref MyGrid);
						GridGlobals = MyGrid;
						goto end_IL_0001_3;
						IL_000b:
						num2 = 2;
						text = Strings.Trim(cmbglobalfile.Text);
						goto IL_001e;
						IL_001e:
						num2 = 3;
						text2 = "";
						goto IL_0027;
						IL_0027:
						num2 = 4;
						num5 = 0;
						goto IL_002c;
						IL_002c:
						num2 = 5;
						flag = false;
						goto IL_0031;
						IL_0031:
						num2 = 6;
						text = BuildForm.Strip_Add_MyPCDir("A2", text);
						goto IL_0044;
						IL_0044:
						num2 = 7;
						num6 = 0;
						goto IL_0049;
						IL_0049:
						num2 = 8;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_0063;
						}
						goto IL_013d;
						IL_0063:
						num2 = 9;
						num6 = unchecked((int)Interaction.MsgBox("Are you sure you wish to clear File Variables?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear?"));
						goto IL_0079;
						IL_0079:
						num2 = 10;
						if (num6 == 7)
						{
							goto end_IL_0001_3;
						}
						goto IL_008c;
						IL_008c:
						num2 = 12;
						fAddedFileGlobals = true;
						goto IL_0096;
						IL_0096:
						num2 = 13;
						num7 = fRows - 1;
						num5 = num7;
						goto IL_0100;
						IL_0100:
						if (num5 >= 0)
						{
							goto IL_00a9;
						}
						goto IL_0105;
						IL_0105:
						num2 = 18;
						GridGlobals.RowCount = fRows;
						goto IL_011a;
						IL_011a:
						num2 = 19;
						MyGrid = GridGlobals;
						GridModule.Number_Grid(ref MyGrid);
						GridGlobals = MyGrid;
						goto end_IL_0001_3;
						IL_00a9:
						num2 = 14;
						if (Operators.ConditionalCompareObjectEqual(GridGlobals.Rows[num5].Cells[0].Tag, "F", TextCompare: false))
						{
							goto IL_00df;
						}
						goto IL_00f7;
						IL_00df:
						num2 = 15;
						GridGlobals.Rows.RemoveAt(num5);
						goto IL_00f7;
						IL_00f7:
						num2 = 17;
						num5 += -1;
						goto IL_0100;
						IL_013d:
						num2 = 21;
						text2 = BuildForm.Get_Globals_in_File(text, ref fGlobals, ref fNoGlobals, "F");
						goto IL_015a;
						IL_015a:
						num2 = 22;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_0176;
						IL_0176:
						num2 = 23;
						fAddedFileGlobals = true;
						goto IL_0180;
						IL_0180:
						num2 = 24;
						num8 = fRows - 1;
						num5 = 0;
						goto IL_0320;
						IL_0320:
						if (num5 <= num8)
						{
							goto IL_0195;
						}
						goto IL_0329;
						IL_0195:
						num2 = 25;
						if (Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num5].Cells[0].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(GridGlobals.Rows[num5].Cells[0].Tag, "F", TextCompare: false))
						{
							goto IL_0200;
						}
						goto IL_0317;
						IL_0317:
						num2 = 37;
						num5++;
						goto IL_0320;
						IL_0200:
						num2 = 26;
						if (fNoGlobals <= 99)
						{
							goto IL_0219;
						}
						goto IL_030c;
						IL_0219:
						num2 = 27;
						fNoGlobals++;
						goto IL_022a;
						IL_022a:
						num2 = 28;
						fGlobals[fNoGlobals].Variable = Conversions.ToString(GridGlobals.Rows[num5].Cells[0].Value);
						goto IL_026a;
						IL_026a:
						num2 = 29;
						fGlobals[fNoGlobals].Value = Conversions.ToString(GridGlobals.Rows[num5].Cells[1].Value);
						goto IL_02aa;
						IL_02aa:
						num2 = 30;
						fGlobals[fNoGlobals].SHValue = Conversions.ToString(GridGlobals.Rows[num5].Cells[2].Value);
						goto IL_02ea;
						IL_02ea:
						num2 = 31;
						fGlobals[fNoGlobals].GMode = "M";
						goto IL_0317;
						IL_030c:
						num2 = 33;
						flag = true;
						goto IL_0329;
						IL_0329:
						num2 = 38;
						if (flag)
						{
							goto IL_0334;
						}
						goto IL_035c;
						IL_0334:
						num2 = 39;
						num6 = unchecked((int)Interaction.MsgBox("Too many File Variables to load. If you continue, you will erase Memory Variables. Do you wish to Continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Too Many Variables"));
						goto IL_034a;
						IL_034a:
						num2 = 40;
						if (num6 == 7)
						{
							goto end_IL_0001_3;
						}
						goto IL_035c;
						end_IL_0001_2:
						break;
					}
					num2 = 48;
					Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, "Error");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1186;
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

	private void GridGlobals_KeyDown(object sender, KeyEventArgs e)
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
				DataGridView MyGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 169:
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
							goto IL_001b;
						case 4:
							goto IL_0031;
						case 6:
							goto IL_003e;
						case 7:
							goto IL_0050;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0031:
					num2 = 4;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_003e:
					num2 = 6;
					if (e.KeyValue != 46)
					{
						goto end_IL_0001_3;
					}
					goto IL_0050;
					IL_001b:
					num2 = 3;
					MyGrid = (DataGridView)sender;
					GridModule.Grid_Row_Sel(ref MyGrid);
					sender = MyGrid;
					goto IL_0031;
					IL_0050:
					num2 = 7;
					cmdDelete_Click(cmdDelete, new EventArgs());
					break;
					IL_000b:
					num2 = 2;
					if (e.KeyValue == 117)
					{
						goto IL_001b;
					}
					goto IL_003e;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 169;
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

	private void mnuComm_Click(object sender, EventArgs e)
	{
		checked
		{
			int num = GridGlobals.Rows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				string text = Conversions.ToString(GridGlobals.Rows[i].Cells[0].Value);
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null), "C", TextCompare: false))
					{
						GridGlobals.Rows[i].Cells[0].Value = "!" + text;
					}
					else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null), "U", TextCompare: false) && Operators.CompareString(text, "!", TextCompare: false) == 0)
					{
						GridGlobals.Rows[i].Cells[0].Value = "";
					}
					else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null), "U", TextCompare: false) && Operators.CompareString(Strings.Mid(text, 1, 1), "!", TextCompare: false) == 0)
					{
						GridGlobals.Rows[i].Cells[0].Value = Strings.Mid(text, 2);
					}
				}
			}
		}
	}
}

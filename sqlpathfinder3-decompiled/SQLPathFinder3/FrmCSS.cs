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
public class FrmCSS : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridTblBorder")]
	private DataGridView _GridTblBorder;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridFormat")]
	private DataGridView _GridFormat;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel")]
	private Button _CmdDel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown")]
	private Button _CmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp")]
	private Button _CmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowseOut")]
	private Button _CmdBrowseOut;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuNew")]
	private ToolStripButton _mnuNew;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSave")]
	private ToolStripButton _mnuSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRun")]
	private ToolStripButton _mnuRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuNewQ")]
	private ToolStripMenuItem _mnuNewQ;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveQ")]
	private ToolStripMenuItem _mnuSaveQ;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRunQ")]
	private ToolStripMenuItem _mnuRunQ;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOpenF")]
	private ToolStripMenuItem _mnuOpenF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveF")]
	private ToolStripMenuItem _mnuSaveF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuTheme")]
	private ToolStripComboBox _mnuTheme;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveASF")]
	private ToolStripMenuItem _mnuSaveASF;

	public string f_ReportSpec;

	public string fTitle;

	public string f_ReportSpecName;

	private int f_MaxObj;

	private Globals_Renamed.Report_Format_Type[] CSSObj;

	private const string f_DefCSS = "sqlpathfinder_style_1.css";

	private const int Colf_fc = 2;

	[field: AccessedThroughProperty("LblTblBorder")]
	internal virtual Label LblTblBorder
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridTblBorder
	{
		[CompilerGenerated]
		get
		{
			return _GridTblBorder;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridTblBorder_CellDoubleClick;
			KeyEventHandler value3 = GridTblBorder_KeyDown;
			DataGridView dataGridView = _GridTblBorder;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick -= value2;
				dataGridView.KeyDown -= value3;
			}
			_GridTblBorder = value;
			dataGridView = _GridTblBorder;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick += value2;
				dataGridView.KeyDown += value3;
			}
		}
	}

	internal virtual DataGridView GridFormat
	{
		[CompilerGenerated]
		get
		{
			return _GridFormat;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridFormat_CellDoubleClick;
			KeyEventHandler value3 = GridFormat_KeyDown;
			DataGridView dataGridView = _GridFormat;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick -= value2;
				dataGridView.KeyDown -= value3;
			}
			_GridFormat = value;
			dataGridView = _GridFormat;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick += value2;
				dataGridView.KeyDown += value3;
			}
		}
	}

	internal virtual Button CmdDel
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel_Click;
			Button button = _CmdDel;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel = value;
			button = _CmdDel;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown_Click;
			Button button = _CmdDown;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown = value;
			button = _CmdDown;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp_Click;
			Button button = _CmdUp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp = value;
			button = _CmdUp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblFormatHdr")]
	internal virtual Label lblFormatHdr
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

	[field: AccessedThroughProperty("TxtCSS")]
	internal virtual TextBox TxtCSS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblCSS")]
	internal virtual Label LblCSS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdBrowseOut
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowseOut;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowseOut_Click;
			Button button = _CmdBrowseOut;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowseOut = value;
			button = _CmdBrowseOut;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuToolBar")]
	internal virtual ToolStrip mnuToolBar
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton mnuRun
	{
		[CompilerGenerated]
		get
		{
			return _mnuRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRun_Click;
			ToolStripButton toolStripButton = _mnuRun;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_mnuRun = value;
			toolStripButton = _mnuRun;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuFileInternal")]
	internal virtual ToolStripMenuItem mnuFileInternal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuNewQ
	{
		[CompilerGenerated]
		get
		{
			return _mnuNewQ;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuNew_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuNewQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuNewQ = value;
			toolStripMenuItem = _mnuNewQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSaveQ
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveQ;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSave_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveQ = value;
			toolStripMenuItem = _mnuSaveQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuRunQ
	{
		[CompilerGenerated]
		get
		{
			return _mnuRunQ;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRun_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRunQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRunQ = value;
			toolStripMenuItem = _mnuRunQ;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdCancel
	{
		[CompilerGenerated]
		get
		{
			return _CmdCancel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdCancel_Click;
			Button button = _CmdCancel;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdCancel = value;
			button = _CmdCancel;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdOK
	{
		[CompilerGenerated]
		get
		{
			return _CmdOK;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdOK_Click;
			Button button = _CmdOK;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdOK = value;
			button = _CmdOK;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuFileExternal")]
	internal virtual ToolStripMenuItem mnuFileExternal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuOpenF
	{
		[CompilerGenerated]
		get
		{
			return _mnuOpenF;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOpenF_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOpenF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOpenF = value;
			toolStripMenuItem = _mnuOpenF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSaveF
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveF;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveF_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveF = value;
			toolStripMenuItem = _mnuSaveF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColorDialog1")]
	internal virtual ColorDialog ColorDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FontDialog1")]
	internal virtual FontDialog FontDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox mnuTheme
	{
		[CompilerGenerated]
		get
		{
			return _mnuTheme;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuTheme_DropDownClosed;
			ToolStripComboBox toolStripComboBox = _mnuTheme;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.DropDownClosed -= value2;
			}
			_mnuTheme = value;
			toolStripComboBox = _mnuTheme;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.DropDownClosed += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColBdrBC")]
	internal virtual DataGridViewTextBoxColumn ColBdrBC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColBdrCollapse")]
	internal virtual DataGridViewComboBoxColumn ColBdrCollapse
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColBdrSt")]
	internal virtual DataGridViewComboBoxColumn ColBdrSt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColBdrWt")]
	internal virtual DataGridViewComboBoxColumn ColBdrWt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColBdrSp")]
	internal virtual DataGridViewComboBoxColumn ColBdrSp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSaveASF
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveASF;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveASF_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveASF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveASF = value;
			toolStripMenuItem = _mnuSaveASF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColFmtType")]
	internal virtual DataGridViewComboBoxColumn ColFmtType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtBC")]
	internal virtual DataGridViewTextBoxColumn ColFmtBC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtColor")]
	internal virtual DataGridViewTextBoxColumn ColFmtColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtFF")]
	internal virtual DataGridViewTextBoxColumn ColFmtFF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtFSz")]
	internal virtual DataGridViewTextBoxColumn ColFmtFSz
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtFS")]
	internal virtual DataGridViewTextBoxColumn ColFmtFS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtFW")]
	internal virtual DataGridViewTextBoxColumn ColFmtFW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtTA")]
	internal virtual DataGridViewComboBoxColumn ColFmtTA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtTD")]
	internal virtual DataGridViewTextBoxColumn ColFmtTD
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColFmtVA")]
	internal virtual DataGridViewComboBoxColumn ColFmtVA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmCSS()
	{
		base.FormClosing += FrmCSS_FormClosing;
		base.Invalidated += FrmCSS_Invalidated;
		base.Load += FrmCSS_Load;
		base.Resize += FrmCSS_Resize;
		f_ReportSpec = "";
		fTitle = "untitled.rss";
		f_ReportSpecName = "untitled";
		f_MaxObj = 0;
		CSSObj = new Globals_Renamed.Report_Format_Type[251];
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmCSS));
		this.LblTblBorder = new System.Windows.Forms.Label();
		this.GridTblBorder = new System.Windows.Forms.DataGridView();
		this.ColBdrBC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColBdrCollapse = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColBdrSt = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColBdrWt = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColBdrSp = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.GridFormat = new System.Windows.Forms.DataGridView();
		this.ColFmtType = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColFmtBC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtFSz = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtFW = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtTA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColFmtTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColFmtVA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.CmdDel = new System.Windows.Forms.Button();
		this.CmdDown = new System.Windows.Forms.Button();
		this.CmdUp = new System.Windows.Forms.Button();
		this.lblFormatHdr = new System.Windows.Forms.Label();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.TxtCSS = new System.Windows.Forms.TextBox();
		this.LblCSS = new System.Windows.Forms.Label();
		this.CmdBrowseOut = new System.Windows.Forms.Button();
		this.mnuToolBar = new System.Windows.Forms.ToolStrip();
		this.mnuNew = new System.Windows.Forms.ToolStripButton();
		this.mnuSave = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRun = new System.Windows.Forms.ToolStripButton();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuFileInternal = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuNewQ = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveQ = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRunQ = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuFileExternal = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOpenF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveASF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuTheme = new System.Windows.Forms.ToolStripComboBox();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.FontDialog1 = new System.Windows.Forms.FontDialog();
		((System.ComponentModel.ISupportInitialize)this.GridTblBorder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridFormat).BeginInit();
		this.mnuToolBar.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.LblTblBorder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblTblBorder.AutoSize = true;
		this.LblTblBorder.Location = new System.Drawing.Point(3, 470);
		this.LblTblBorder.Name = "LblTblBorder";
		this.LblTblBorder.Size = new System.Drawing.Size(411, 17);
		this.LblTblBorder.TabIndex = 35;
		this.LblTblBorder.Text = "Specify format of table surrounding report headers and columns";
		this.GridTblBorder.AllowUserToAddRows = false;
		this.GridTblBorder.AllowUserToDeleteRows = false;
		this.GridTblBorder.AllowUserToResizeColumns = false;
		this.GridTblBorder.AllowUserToResizeRows = false;
		this.GridTblBorder.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridTblBorder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridTblBorder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridTblBorder.Columns.AddRange(this.ColBdrBC, this.ColBdrCollapse, this.ColBdrSt, this.ColBdrWt, this.ColBdrSp);
		this.GridTblBorder.Location = new System.Drawing.Point(6, 494);
		this.GridTblBorder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.GridTblBorder.Name = "GridTblBorder";
		this.GridTblBorder.RowHeadersWidth = 50;
		this.GridTblBorder.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridTblBorder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
		this.GridTblBorder.Size = new System.Drawing.Size(711, 93);
		this.GridTblBorder.StandardTab = true;
		this.GridTblBorder.TabIndex = 6;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColBdrBC.DefaultCellStyle = dataGridViewCellStyle2;
		this.ColBdrBC.HeaderText = "Border-Color";
		this.ColBdrBC.Name = "ColBdrBC";
		this.ColBdrBC.ReadOnly = true;
		this.ColBdrBC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColBdrBC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColBdrBC.Width = 125;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColBdrCollapse.DefaultCellStyle = dataGridViewCellStyle3;
		this.ColBdrCollapse.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColBdrCollapse.DisplayStyleForCurrentCellOnly = true;
		this.ColBdrCollapse.DropDownWidth = 150;
		this.ColBdrCollapse.HeaderText = "Border-Collapse";
		this.ColBdrCollapse.Name = "ColBdrCollapse";
		this.ColBdrCollapse.Width = 125;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColBdrSt.DefaultCellStyle = dataGridViewCellStyle4;
		this.ColBdrSt.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColBdrSt.DisplayStyleForCurrentCellOnly = true;
		this.ColBdrSt.HeaderText = "Border-Style";
		this.ColBdrSt.Name = "ColBdrSt";
		this.ColBdrSt.Sorted = true;
		this.ColBdrSt.Width = 125;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColBdrWt.DefaultCellStyle = dataGridViewCellStyle5;
		this.ColBdrWt.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColBdrWt.DisplayStyleForCurrentCellOnly = true;
		this.ColBdrWt.HeaderText = "Border-Width";
		this.ColBdrWt.Name = "ColBdrWt";
		this.ColBdrWt.Width = 125;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColBdrSp.DefaultCellStyle = dataGridViewCellStyle6;
		this.ColBdrSp.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColBdrSp.DisplayStyleForCurrentCellOnly = true;
		this.ColBdrSp.HeaderText = "Border-Spacing";
		this.ColBdrSp.Name = "ColBdrSp";
		this.ColBdrSp.Sorted = true;
		this.ColBdrSp.Width = 115;
		this.GridFormat.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridFormat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.GridFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridFormat.Columns.AddRange(this.ColFmtType, this.ColFmtBC, this.ColFmtColor, this.ColFmtFF, this.ColFmtFSz, this.ColFmtFS, this.ColFmtFW, this.ColFmtTA, this.ColFmtTD, this.ColFmtVA);
		this.GridFormat.Location = new System.Drawing.Point(7, 178);
		this.GridFormat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.GridFormat.Name = "GridFormat";
		this.GridFormat.RowHeadersWidth = 50;
		this.GridFormat.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridFormat.Size = new System.Drawing.Size(709, 275);
		this.GridFormat.StandardTab = true;
		this.GridFormat.TabIndex = 2;
		this.ColFmtType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColFmtType.DisplayStyleForCurrentCellOnly = true;
		this.ColFmtType.DropDownWidth = 150;
		this.ColFmtType.Frozen = true;
		this.ColFmtType.HeaderText = "Format Type";
		this.ColFmtType.Items.AddRange("", "At-Top-of-Report", "At-Bot-of-Report", "Column-Data", "Column-Headers", "Column-Alt-Row", "At-Top-of-Col1", "At-Top-of-Col2", "At-Top-of-Col3", "At-Top-of-Col4", "At-Top-of-Col5", "At-Top-of-Col6", "At-Top-of-Col7", "At-Top-of-Col8", "At-Top-of-Col9", "At-Bot-of-Col1", "At-Bot-of-Col2", "At-Bot-of-Col3", "At-Bot-of-Col4", "At-Bot-of-Col5", "At-Bot-of-Col6", "At-Bot-of-Col7", "At-Bot-of-Col8", "At-Bot-of-Col9", "At-Top-of-Reporti", "At-Top-of-Coli", "JQX-All-IChart-Text");
		this.ColFmtType.Name = "ColFmtType";
		this.ColFmtType.Width = 115;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtBC.DefaultCellStyle = dataGridViewCellStyle8;
		this.ColFmtBC.HeaderText = "BackGround-Color";
		this.ColFmtBC.Name = "ColFmtBC";
		this.ColFmtBC.ReadOnly = true;
		this.ColFmtBC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtBC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtBC.Width = 94;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtColor.DefaultCellStyle = dataGridViewCellStyle9;
		this.ColFmtColor.HeaderText = "Font / Color";
		this.ColFmtColor.Name = "ColFmtColor";
		this.ColFmtColor.ReadOnly = true;
		this.ColFmtColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColFmtColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtColor.Width = 240;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtFF.DefaultCellStyle = dataGridViewCellStyle10;
		this.ColFmtFF.HeaderText = "Font-Family";
		this.ColFmtFF.Name = "ColFmtFF";
		this.ColFmtFF.ReadOnly = true;
		this.ColFmtFF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtFF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtFF.Visible = false;
		this.ColFmtFF.Width = 75;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtFSz.DefaultCellStyle = dataGridViewCellStyle11;
		this.ColFmtFSz.HeaderText = "Font-Size";
		this.ColFmtFSz.Name = "ColFmtFSz";
		this.ColFmtFSz.ReadOnly = true;
		this.ColFmtFSz.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtFSz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtFSz.Visible = false;
		this.ColFmtFSz.Width = 75;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtFS.DefaultCellStyle = dataGridViewCellStyle12;
		this.ColFmtFS.HeaderText = "Font-Style";
		this.ColFmtFS.Name = "ColFmtFS";
		this.ColFmtFS.ReadOnly = true;
		this.ColFmtFS.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtFS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtFS.Visible = false;
		this.ColFmtFS.Width = 75;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtFW.DefaultCellStyle = dataGridViewCellStyle13;
		this.ColFmtFW.HeaderText = "Font-Weight";
		this.ColFmtFW.Name = "ColFmtFW";
		this.ColFmtFW.ReadOnly = true;
		this.ColFmtFW.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtFW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtFW.Visible = false;
		this.ColFmtFW.Width = 75;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtTA.DefaultCellStyle = dataGridViewCellStyle14;
		this.ColFmtTA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColFmtTA.DisplayStyleForCurrentCellOnly = true;
		this.ColFmtTA.HeaderText = "Text-Align";
		this.ColFmtTA.Name = "ColFmtTA";
		this.ColFmtTA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtTA.Sorted = true;
		this.ColFmtTA.Width = 75;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtTD.DefaultCellStyle = dataGridViewCellStyle15;
		this.ColFmtTD.HeaderText = "Text-Decoration";
		this.ColFmtTD.Name = "ColFmtTD";
		this.ColFmtTD.ReadOnly = true;
		this.ColFmtTD.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtTD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFmtTD.Visible = false;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColFmtVA.DefaultCellStyle = dataGridViewCellStyle16;
		this.ColFmtVA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColFmtVA.DisplayStyleForCurrentCellOnly = true;
		this.ColFmtVA.HeaderText = "Vertical-Align";
		this.ColFmtVA.Name = "ColFmtVA";
		this.ColFmtVA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColFmtVA.Sorted = true;
		this.ColFmtVA.Width = 75;
		this.CmdDel.ImageIndex = 2;
		this.CmdDel.Location = new System.Drawing.Point(127, 153);
		this.CmdDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdDel.Name = "CmdDel";
		this.CmdDel.Size = new System.Drawing.Size(53, 24);
		this.CmdDel.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.CmdDel, "Delete row");
		this.CmdDel.UseVisualStyleBackColor = true;
		this.CmdDown.ImageIndex = 1;
		this.CmdDown.Location = new System.Drawing.Point(67, 153);
		this.CmdDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdDown.Name = "CmdDown";
		this.CmdDown.Size = new System.Drawing.Size(53, 24);
		this.CmdDown.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.CmdDown, "Move row down");
		this.CmdDown.UseVisualStyleBackColor = true;
		this.CmdUp.ImageIndex = 0;
		this.CmdUp.Location = new System.Drawing.Point(6, 153);
		this.CmdUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdUp.Name = "CmdUp";
		this.CmdUp.Size = new System.Drawing.Size(53, 24);
		this.CmdUp.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdUp, "Move row up");
		this.CmdUp.UseVisualStyleBackColor = true;
		this.lblFormatHdr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblFormatHdr.AutoSize = true;
		this.lblFormatHdr.Location = new System.Drawing.Point(5, 71);
		this.lblFormatHdr.Name = "lblFormatHdr";
		this.lblFormatHdr.Size = new System.Drawing.Size(272, 17);
		this.lblFormatHdr.TabIndex = 29;
		this.lblFormatHdr.Text = "Specify format of different report sections.";
		this.TxtCSS.Location = new System.Drawing.Point(115, 116);
		this.TxtCSS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.TxtCSS.Name = "TxtCSS";
		this.TxtCSS.Size = new System.Drawing.Size(501, 22);
		this.TxtCSS.TabIndex = 0;
		this.LblCSS.AutoSize = true;
		this.LblCSS.Location = new System.Drawing.Point(3, 119);
		this.LblCSS.Name = "LblCSS";
		this.LblCSS.Size = new System.Drawing.Size(88, 17);
		this.LblCSS.TabIndex = 39;
		this.LblCSS.Text = "Style Sheet: ";
		this.CmdBrowseOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.CmdBrowseOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.CmdBrowseOut.ImageIndex = 3;
		this.CmdBrowseOut.Location = new System.Drawing.Point(651, 113);
		this.CmdBrowseOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdBrowseOut.Name = "CmdBrowseOut";
		this.CmdBrowseOut.Size = new System.Drawing.Size(66, 28);
		this.CmdBrowseOut.TabIndex = 1;
		this.CmdBrowseOut.UseVisualStyleBackColor = true;
		this.mnuToolBar.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuNew, this.mnuSave, this.ToolStripSeparator1, this.mnuRun });
		this.mnuToolBar.Location = new System.Drawing.Point(0, 30);
		this.mnuToolBar.Name = "mnuToolBar";
		this.mnuToolBar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
		this.mnuToolBar.Size = new System.Drawing.Size(809, 27);
		this.mnuToolBar.TabIndex = 42;
		this.mnuToolBar.Text = "ToolStrip1";
		this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuNew.Image = (System.Drawing.Image)resources.GetObject("mnuNew.Image");
		this.mnuNew.ImageTransparentColor = System.Drawing.Color.Red;
		this.mnuNew.Name = "mnuNew";
		this.mnuNew.Size = new System.Drawing.Size(24, 24);
		this.mnuNew.Text = "Clear the Current Report Format";
		this.mnuSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuSave.Image = (System.Drawing.Image)resources.GetObject("mnuSave.Image");
		this.mnuSave.ImageTransparentColor = System.Drawing.Color.Red;
		this.mnuSave.Name = "mnuSave";
		this.mnuSave.Size = new System.Drawing.Size(24, 24);
		this.mnuSave.Text = "Save the Report Format File to SQLPathFinder";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 27);
		this.mnuRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuRun.Image = (System.Drawing.Image)resources.GetObject("mnuRun.Image");
		this.mnuRun.ImageTransparentColor = System.Drawing.Color.White;
		this.mnuRun.Name = "mnuRun";
		this.mnuRun.Size = new System.Drawing.Size(24, 24);
		this.mnuRun.Text = "ToolStripButton4";
		this.mnuRun.ToolTipText = "Generate the Report Style Sheet";
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuFileInternal, this.mnuFileExternal, this.mnuTheme });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Padding = new System.Windows.Forms.Padding(8, 1, 0, 1);
		this.MenuStrip1.Size = new System.Drawing.Size(809, 30);
		this.MenuStrip1.TabIndex = 43;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuFileInternal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuNewQ, this.mnuSaveQ, this.mnuRunQ, this.ToolStripSeparator2 });
		this.mnuFileInternal.Name = "mnuFileInternal";
		this.mnuFileInternal.Size = new System.Drawing.Size(108, 28);
		this.mnuFileInternal.Text = "&File (Internal)";
		this.mnuNewQ.Name = "mnuNewQ";
		this.mnuNewQ.Size = new System.Drawing.Size(399, 26);
		this.mnuNewQ.Text = "New";
		this.mnuSaveQ.Name = "mnuSaveQ";
		this.mnuSaveQ.Size = new System.Drawing.Size(399, 26);
		this.mnuSaveQ.Text = "Save Report Style Sheet in SQLPathFinder Query";
		this.mnuRunQ.Name = "mnuRunQ";
		this.mnuRunQ.Size = new System.Drawing.Size(399, 26);
		this.mnuRunQ.Text = "Run Report";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(396, 6);
		this.mnuFileExternal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuOpenF, this.mnuSaveF, this.mnuSaveASF });
		this.mnuFileExternal.Name = "mnuFileExternal";
		this.mnuFileExternal.Size = new System.Drawing.Size(111, 28);
		this.mnuFileExternal.Text = "File (&External)";
		this.mnuOpenF.Name = "mnuOpenF";
		this.mnuOpenF.Size = new System.Drawing.Size(324, 26);
		this.mnuOpenF.Text = "Open an External Report Format File";
		this.mnuSaveF.Name = "mnuSaveF";
		this.mnuSaveF.Size = new System.Drawing.Size(324, 26);
		this.mnuSaveF.Text = "Save Report Format Externally";
		this.mnuSaveASF.Name = "mnuSaveASF";
		this.mnuSaveASF.Size = new System.Drawing.Size(324, 26);
		this.mnuSaveASF.Text = "Save AS - Report Format Externally";
		this.mnuTheme.AutoCompleteCustomSource.AddRange(new string[2] { "Green Theme", "Blue Theme" });
		this.mnuTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.mnuTheme.Items.AddRange(new object[7] { "Themes", "Blue Theme", "Gold Theme", "Green Theme", "Grey Theme", "Grey Interactive Theme", "Light Blue Theme" });
		this.mnuTheme.Name = "mnuTheme";
		this.mnuTheme.Size = new System.Drawing.Size(161, 28);
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 5;
		this.CmdCancel.Location = new System.Drawing.Point(651, 51);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(66, 48);
		this.CmdCancel.TabIndex = 8;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.CmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdOK.ImageIndex = 4;
		this.CmdOK.Location = new System.Drawing.Point(578, 51);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(66, 48);
		this.CmdOK.TabIndex = 7;
		this.CmdOK.Text = "OK";
		this.CmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdOK.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(809, 659);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.mnuToolBar);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.CmdBrowseOut);
		base.Controls.Add(this.LblCSS);
		base.Controls.Add(this.TxtCSS);
		base.Controls.Add(this.LblTblBorder);
		base.Controls.Add(this.GridTblBorder);
		base.Controls.Add(this.GridFormat);
		base.Controls.Add(this.CmdDel);
		base.Controls.Add(this.CmdDown);
		base.Controls.Add(this.CmdUp);
		base.Controls.Add(this.lblFormatHdr);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "FrmCSS";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Define Report Style Sheet";
		((System.ComponentModel.ISupportInitialize)this.GridTblBorder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridFormat).EndInit();
		this.mnuToolBar.ResumeLayout(false);
		this.mnuToolBar.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Save_External(int MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string MyRptSpec = default(string);
		string iniFile = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				short num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 475:
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
							goto IL_001c;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_002a;
						case 8:
							goto IL_0042;
						case 9:
							goto IL_006b;
						case 10:
							goto IL_0094;
						case 12:
							goto IL_00a2;
						case 13:
							goto IL_00c4;
						case 11:
						case 14:
						case 15:
							goto IL_00cf;
						case 16:
							goto IL_00ee;
						case 17:
							goto IL_0107;
						case 20:
							goto IL_0126;
						case 19:
						case 21:
						case 22:
							goto IL_0132;
						case 23:
							goto IL_0147;
						case 24:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 18:
						case 25:
						case 26:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0107:
					num2 = 17;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0132;
					IL_0126:
					num2 = 20;
					text = fTitle;
					goto IL_0132;
					IL_00ee:
					num2 = 16;
					text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_0107;
					IL_0132:
					num2 = 22;
					if (General_Procedures.Save_SQL_Query(MyRptSpec, text) != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0147;
					IL_000b:
					num2 = 2;
					MyRptSpec = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					num5 = 0;
					goto IL_0021;
					IL_0021:
					num2 = 5;
					iniFile = "sqlpathfinder.rss";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					if (Save_Report(ref MyRptSpec) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0042;
					IL_0042:
					num2 = 8;
					if (Operators.CompareString(Strings.LCase(fTitle), "untitled.rss", TextCompare: false) == 0 || MyMode == 1)
					{
						goto IL_006b;
					}
					goto IL_0126;
					IL_006b:
					num2 = 9;
					if (MyMode == 1 && Operators.CompareString(Strings.LCase(fTitle), "untitled.rss", TextCompare: false) != 0)
					{
						goto IL_0094;
					}
					goto IL_00a2;
					IL_0147:
					num2 = 23;
					fTitle = text;
					break;
					IL_0094:
					num2 = 10;
					iniFile = fTitle;
					goto IL_00cf;
					IL_00a2:
					num2 = 12;
					if (Operators.CompareString(Strings.LCase(fTitle), "untitled.rss", TextCompare: false) == 0)
					{
						goto IL_00c4;
					}
					goto IL_00cf;
					IL_00c4:
					num2 = 13;
					iniFile = "sqlpathfinder.rss";
					goto IL_00cf;
					IL_00cf:
					num2 = 15;
					BuildForm.FileOpenSave("S", iniFile, "rss", "Report Format File", Globals_Renamed.gQueryDir);
					goto IL_00ee;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				Add_Query_To_Title();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 475;
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

	public void New_Theme()
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
				DataGridView MyGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 237:
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
							goto IL_0023;
						case 5:
							goto IL_002f;
						case 6:
							goto IL_004a;
						case 7:
							goto IL_0065;
						case 8:
							goto IL_006e;
						case 9:
							goto IL_0081;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0065:
					num2 = 7;
					Initialize_Formats();
					goto IL_006e;
					IL_006e:
					num2 = 8;
					TxtCSS.Text = "sqlpathfinder_style_1.css";
					goto IL_0081;
					IL_004a:
					num2 = 6;
					MyGrid = GridTblBorder;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridTblBorder = MyGrid;
					goto IL_0065;
					IL_0081:
					num2 = 9;
					Add_Query_To_Title();
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to clear format selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear Format?");
					goto IL_0023;
					IL_0023:
					num2 = 4;
					if (num5 != 6)
					{
						break;
					}
					goto IL_002f;
					IL_002f:
					num2 = 5;
					MyGrid = GridFormat;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridFormat = MyGrid;
					goto IL_004a;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				mnuTheme.Text = "Themes";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 237;
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

	public bool Check_Valid_Files()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		bool result = default(bool);
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
				case 212:
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
							goto IL_003e;
						case 6:
							goto IL_0047;
						case 7:
						case 8:
							goto IL_0057;
						case 9:
							goto IL_006f;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0047:
					num2 = 6;
					TxtCSS.Focus();
					goto IL_0057;
					IL_0057:
					num2 = 8;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_006f;
					IL_003e:
					num2 = 5;
					text = "You must specify a Report Style Sheet";
					goto IL_0047;
					IL_006f:
					num2 = 9;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (Operators.CompareString(Strings.Trim(TxtCSS.Text), "", TextCompare: false) == 0)
					{
						goto IL_003e;
					}
					goto IL_0057;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Interaction.MsgBox(text, MsgBoxStyle.Exclamation, "Cannot Save Report Format File");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 212;
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

	public int Open_Report(string MyRptSpec)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int result;
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
						result = 1;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmCSS - Open_Report";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						int num7 = -1;
						int num8 = -1;
						string text5 = "";
						string text6 = "";
						string text7 = "";
						bool flag = false;
						int num9 = 0;
						string[] array = Strings.Split(MyRptSpec, "\r\n");
						DataGridView MyGrid = GridFormat;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridFormat = MyGrid;
						MyGrid = GridTblBorder;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridTblBorder = MyGrid;
						int num10 = Information.UBound(array);
						string[] array2;
						for (num3 = 0; num3 <= num10; num3++)
						{
							array2 = Strings.Split(array[num3], "<\\\\>");
							text5 = Strings.Trim(Strings.UCase(array2[0]));
							if (Operators.CompareString(text5, "CSS", TextCompare: false) != 0)
							{
								if (Operators.CompareString(text5, "FORMAT", TextCompare: false) != 0)
								{
									continue;
								}
								text = Strings.Trim(array2[1]);
								text6 = Strings.UCase(text);
								text7 = text6;
								if (LikeOperator.LikeString(text6, "AT-TOP-OF-COL*", CompareMethod.Binary))
								{
									text6 = "AT-TOP-OF-COL";
								}
								else if (LikeOperator.LikeString(text6, "AT-TOP-OF-REPORT*", CompareMethod.Binary))
								{
									text6 = "AT-TOP-OF-REPORT";
								}
								else if (LikeOperator.LikeString(text6, "AT-BOT-OF-COL*", CompareMethod.Binary))
								{
									text6 = "AT-BOT-OF-COL";
								}
								switch (text6)
								{
								case "COLUMN-HEADERS":
								case "COLUMN-DATA":
								case "COLUMN-ALT-ROW":
								case "AT-TOP-OF-REPORT":
								case "AT-BOT-OF-REPORT":
								case "AT-TOP-OF-COL":
								case "AT-BOT-OF-COL":
								case "COLUMN-BORDER":
								case "JQX-ALL-ICHART-TEXT":
								{
									if (Operators.CompareString(text6, "COLUMN-BORDER", TextCompare: false) == 0)
									{
										num8++;
									}
									else
									{
										num7++;
										DataGridViewComboBoxColumn MyCombo = ColFmtType;
										BuildReport.Verify_Column(ref MyCombo, text);
										ColFmtType = MyCombo;
										GridFormat.Rows[num7].Cells[0].Value = text;
									}
									int num11 = Information.UBound(array2);
									for (num4 = 2; num4 <= num11; num4++)
									{
										text4 = array2[num4];
										num9 = Strings.InStr(text4, ":");
										if (num9 == 0)
										{
											continue;
										}
										text2 = Strings.LCase(Strings.Trim(Strings.Mid(text4, 1, num9 - 1)));
										text3 = Strings.Trim(Strings.Mid(text4 + " ", num9 + 1));
										flag = false;
										int num12 = f_MaxObj;
										for (num5 = 0; num5 <= num12; num5++)
										{
											if ((Operators.CompareString(CSSObj[num5].ObjType, "headers", TextCompare: false) == 0) & (Operators.CompareString(text2, Strings.Trim(Strings.LCase(CSSObj[num5].Value)), TextCompare: false) == 0))
											{
												text2 = Strings.LCase(CSSObj[num5].Name);
												flag = true;
												break;
											}
										}
										if (!flag)
										{
											continue;
										}
										if (Operators.CompareString(text6, "COLUMN-BORDER", TextCompare: false) == 0)
										{
											int num13 = GridTblBorder.ColumnCount - 1;
											for (num5 = 0; num5 <= num13; num5++)
											{
												if (Operators.CompareString(text2, Strings.Trim(Strings.LCase(GridTblBorder.Columns[num5].HeaderText)), TextCompare: false) == 0)
												{
													GridTblBorder.Rows[num8].Cells[num5].Value = Strings.LCase(text3);
													break;
												}
											}
											continue;
										}
										int num14 = GridFormat.ColumnCount - 1;
										for (num5 = 0; num5 <= num14; num5++)
										{
											if (Operators.CompareString(text2, Strings.LCase(GridFormat.Columns[num5].HeaderText), TextCompare: false) == 0)
											{
												GridFormat.Rows[num7].Cells[num5].Value = Strings.LCase(text3);
												break;
											}
										}
									}
									break;
								}
								}
							}
							else
							{
								TxtCSS.Text = Strings.Trim(array2[1]);
							}
						}
						if (Operators.ConditionalCompareObjectNotEqual(GridTblBorder.Rows[0].Cells[0].Value, "", TextCompare: false))
						{
							GridTblBorder[0, 0].Style.BackColor = ColorTranslator.FromHtml(Conversions.ToString(GridTblBorder.Rows[0].Cells[0].Value));
						}
						GridTblBorder.Rows[0].Height = 25;
						MyGrid = GridFormat;
						BuildChart.Format_Grid(ref MyGrid, 2);
						GridFormat = MyGrid;
						result = 0;
						array2 = null;
						array = null;
						goto end_IL_0001;
					}
					case 1636:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = 1;
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1636;
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
		return result;
	}

	public void Add_Query_To_Title()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string text = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text2;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 229:
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
							goto IL_0037;
						case 7:
							goto IL_0043;
						case 9:
							goto IL_0051;
						case 8:
						case 10:
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0037:
					num2 = 6;
					if (num5 == 0)
					{
						goto IL_0043;
					}
					goto IL_0051;
					IL_0043:
					num2 = 7;
					text = fTitle;
					break;
					IL_0021:
					num2 = 5;
					num5 = checked((short)Strings.InStrRev(fTitle, "\\"));
					goto IL_0037;
					IL_0051:
					num2 = 9;
					text = Strings.Mid(fTitle, checked(num5 + 1));
					break;
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
					text2 = "";
					goto IL_0021;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Text = "Report Style Sheet - [Internal=" + f_ReportSpecName + "] [External=" + text + "]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 229;
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

	public void Initialize_Formats()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string left = default(string);
		string text = default(string);
		string text2 = default(string);
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
				case 610:
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
							goto IL_0030;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0048;
						case 8:
							goto IL_0058;
						case 9:
							goto IL_006f;
						case 11:
							goto IL_007f;
						case 12:
							goto IL_0097;
						case 14:
							goto IL_00a4;
						case 15:
							goto IL_00bc;
						case 17:
							goto IL_00c9;
						case 18:
							goto IL_00e1;
						case 20:
							goto IL_00ee;
						case 21:
							goto IL_0106;
						case 10:
						case 13:
						case 16:
						case 19:
						case 22:
						case 23:
							goto IL_0111;
						case 24:
							goto IL_012c;
						case 27:
							goto IL_014b;
						case 28:
							goto IL_0158;
						case 29:
							goto IL_0166;
						case 26:
						case 30:
						case 31:
							goto IL_0171;
						case 32:
							goto IL_018d;
						case 33:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 25:
						case 34:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0166:
					num2 = 29;
					Add_Query_To_Title();
					goto IL_0171;
					IL_0171:
					num2 = 31;
					GridTblBorder.Rows[0].Height = 25;
					goto IL_018d;
					IL_0158:
					num2 = 28;
					if (num5 == 0)
					{
						goto IL_0166;
					}
					goto IL_0171;
					IL_018d:
					num2 = 32;
					MyGrid = GridFormat;
					GridModule.Number_Grid(ref MyGrid);
					GridFormat = MyGrid;
					break;
					IL_000b:
					num2 = 2;
					left = Strings.UCase(mnuTheme.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					text = "goldtheme.rss";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					text2 = "";
					goto IL_0030;
					IL_0030:
					num2 = 5;
					num5 = 0;
					goto IL_0035;
					IL_0035:
					num2 = 6;
					GridFormat.Rows.Clear();
					goto IL_0048;
					IL_0048:
					num2 = 7;
					GridFormat.RowCount = 50;
					goto IL_0058;
					IL_0058:
					num2 = 8;
					if (Operators.CompareString(left, "GREEN THEME", TextCompare: false) == 0)
					{
						goto IL_006f;
					}
					goto IL_007f;
					IL_006f:
					num2 = 9;
					text = "greentheme.rss";
					goto IL_0111;
					IL_007f:
					num2 = 11;
					if (Operators.CompareString(left, "BLUE THEME", TextCompare: false) == 0)
					{
						goto IL_0097;
					}
					goto IL_00a4;
					IL_0097:
					num2 = 12;
					text = "darkbluetheme.rss";
					goto IL_0111;
					IL_00a4:
					num2 = 14;
					if (Operators.CompareString(left, "LIGHT BLUE THEME", TextCompare: false) == 0)
					{
						goto IL_00bc;
					}
					goto IL_00c9;
					IL_00bc:
					num2 = 15;
					text = "bluetheme.rss";
					goto IL_0111;
					IL_00c9:
					num2 = 17;
					if (Operators.CompareString(left, "GREY THEME", TextCompare: false) == 0)
					{
						goto IL_00e1;
					}
					goto IL_00ee;
					IL_00e1:
					num2 = 18;
					text = "greytheme.rss";
					goto IL_0111;
					IL_00ee:
					num2 = 20;
					if (Operators.CompareString(left, "GREY INTERACTIVE THEME", TextCompare: false) == 0)
					{
						goto IL_0106;
					}
					goto IL_0111;
					IL_0106:
					num2 = 21;
					text = "grey_interactive.rss";
					goto IL_0111;
					IL_0111:
					num2 = 23;
					text2 = General_Procedures.OpenReadFileContents(Globals_Renamed.MySchemaDir + "\\" + text);
					goto IL_012c;
					IL_012c:
					num2 = 24;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_014b;
					IL_014b:
					num2 = 27;
					num5 = Open_Report(text2);
					goto IL_0158;
					end_IL_0001_2:
					break;
				}
				num2 = 33;
				GridTblBorder.CurrentCell = GridTblBorder[1, 0];
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 610;
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

	public short Save_Report(ref string MyRptSpec)
	{
		int try0001_dispatch = -1;
		short result;
		int num2 = default(int);
		string errsource = default(string);
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
						result = 1;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmCSS - Save_Report";
						MyRptSpec = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						int num7 = 0;
						string text9 = "";
						string text10 = "";
						short num8 = 0;
						string text11 = Strings.Trim(TxtCSS.Text);
						if (!Check_Valid_Files())
						{
							goto end_IL_0001;
						}
						num3 = this.GridFormat.ColumnCount;
						num3++;
						MyRptSpec = "Type<\\\\>Key";
						int num9 = num3 - 2;
						for (num4 = 1; num4 <= num9; num4++)
						{
							MyRptSpec = MyRptSpec + "<\\\\>COL" + Strings.Trim(Conversions.ToString(num4));
						}
						MyRptSpec = MyRptSpec + "\r\nTYPE<\\\\>CSS" + BuildReport.Add_Delimiter(num3, 2);
						MyRptSpec = MyRptSpec + "\r\nCSS<\\\\>" + text11 + BuildReport.Add_Delimiter(num3, 2);
						DataGridView GridFormat = this.GridFormat;
						BuildChart.Save_CSS_Grid(ref GridFormat, ref CSSObj, f_MaxObj, num3, ref MyRptSpec);
						this.GridFormat = GridFormat;
						text2 = "";
						text3 = "";
						num6 = 0;
						int num10 = GridTblBorder.ColumnCount - 1;
						for (num5 = 0; num5 <= num10; num5++)
						{
							text2 = Conversions.ToString(GridTblBorder.Rows[0].Cells[num5].Value);
							if (Operators.CompareString(text2, "", TextCompare: false) != 0)
							{
								text3 = text3 + "<\\\\>" + BuildReport.Get_CSS_Attr(GridTblBorder.Columns[num5].HeaderText, f_MaxObj, ref CSSObj) + ":" + text2;
								num6++;
							}
						}
						MyRptSpec = MyRptSpec + "\r\nFORMAT<\\\\>COLUMN-BORDER" + text3 + BuildReport.Add_Delimiter(num3, 2 + num6);
						result = 0;
						goto end_IL_0001;
					}
					case 594:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = 1;
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 594;
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
		return result;
	}

	private void FrmCSS_FormClosing(object sender, FormClosingEventArgs e)
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
				case 64:
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
				Array.Clear(CSSObj, 0, CSSObj.Length);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 64;
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

	private void FrmCSS_Invalidated(object sender, InvalidateEventArgs e)
	{
	}

	private void FrmCSS_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "FrmCSS - Form_Load";
					Button MyButton = CmdCancel;
					BuildForm.Set_Btn_Img(ref MyButton, "cancel");
					CmdCancel = MyButton;
					MyButton = CmdOK;
					BuildForm.Set_Btn_Img(ref MyButton, "ok");
					CmdOK = MyButton;
					MyButton = CmdBrowseOut;
					BuildForm.Set_Btn_Img(ref MyButton, "browse");
					CmdBrowseOut = MyButton;
					MyButton = CmdUp;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp = MyButton;
					MyButton = CmdDown;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown = MyButton;
					MyButton = CmdDel;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel = MyButton;
					mnuTheme.SelectedIndex = 0;
					BuildReport.Load_CSSObj(ref CSSObj, ref f_MaxObj);
					TxtCSS.Text = "sqlpathfinder_style_1.css";
					GridTblBorder.RowCount = 1;
					GridTblBorder.Rows[0].HeaderCell.Value = "1";
					DataGridViewComboBoxColumn Cmb = ColFmtTA;
					BuildReport.Initialize_Grid_Format_Values_1("text-align", ref Cmb, f_MaxObj, ref CSSObj);
					ColFmtTA = Cmb;
					Cmb = ColFmtVA;
					BuildReport.Initialize_Grid_Format_Values_1("valign", ref Cmb, f_MaxObj, ref CSSObj);
					ColFmtVA = Cmb;
					Cmb = ColBdrCollapse;
					BuildReport.Initialize_Grid_Format_Values_1("border-collapse", ref Cmb, f_MaxObj, ref CSSObj);
					ColBdrCollapse = Cmb;
					Cmb = ColBdrSt;
					BuildReport.Initialize_Grid_Format_Values_1("border-style", ref Cmb, f_MaxObj, ref CSSObj);
					ColBdrSt = Cmb;
					Cmb = ColBdrWt;
					BuildReport.Initialize_Grid_Format_Values_1("border-width", ref Cmb, f_MaxObj, ref CSSObj);
					ColBdrWt = Cmb;
					Cmb = ColBdrSp;
					BuildReport.Initialize_Grid_Format_Values_1("border-spacing", ref Cmb, f_MaxObj, ref CSSObj);
					ColBdrSp = Cmb;
					Initialize_Formats();
					Add_Query_To_Title();
					if (Operators.CompareString(f_ReportSpec, "", TextCompare: false) != 0)
					{
						Open_Report(f_ReportSpec);
					}
					base.Height = checked(GridTblBorder.Top + GridTblBorder.Height + 50);
					Form_Resize();
					goto end_IL_0001;
				}
				case 684:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						Cursor.Current = Cursors.Default;
						Close();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 684;
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

	private void CmdUp_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridFormat;
				GridModule.Grid_Up(ref MyGrid);
				GridFormat = MyGrid;
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

	private void CmdDown_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridFormat;
				GridModule.Grid_Down(ref MyGrid);
				GridFormat = MyGrid;
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

	private void CmdDel_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridFormat;
				BuildReport.Grid_Row_Del(ref MyGrid);
				GridFormat = MyGrid;
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

	private void CmdBrowseOut_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string fileName = default(string);
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
				case 182:
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
							goto IL_0019;
						case 4:
							goto IL_0036;
						case 5:
							goto IL_004d;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0019:
					num2 = 3;
					BuildForm.FileOpenSave("S", fileName, "css", "Report Style Sheet", "");
					goto IL_0036;
					IL_0036:
					num2 = 4;
					fileName = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_004d;
					IL_000b:
					num2 = 2;
					fileName = TxtCSS.Text;
					goto IL_0019;
					IL_004d:
					num2 = 5;
					if (Operators.CompareString(fileName, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				TxtCSS.Text = BuildForm.Strip_Add_MyPCDir("S", fileName);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 182;
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

	private void mnuNew_Click(object sender, EventArgs e)
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
				New_Theme();
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

	private void mnuSave_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string MyRptSpec = default(string);
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
				case 123:
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
							goto IL_0018;
						case 5:
							goto IL_0024;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = Save_Report(ref MyRptSpec);
					goto IL_0024;
					IL_000b:
					num2 = 2;
					MyRptSpec = "";
					goto IL_0013;
					IL_0024:
					num2 = 5;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				f_ReportSpec = MyRptSpec;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 123;
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

	private void mnuRun_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string MyRptSpec = default(string);
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
							goto IL_0013;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_0024;
						case 6:
							goto IL_0031;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0018:
					num2 = 4;
					num5 = Save_Report(ref MyRptSpec);
					goto IL_0024;
					IL_0024:
					num2 = 5;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0031;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0031:
					num2 = 6;
					MyRptSpec = "<OPTIONS>\r\n/REPORT=HTML-RUN\r\n/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\n" + MyRptSpec;
					break;
					IL_000b:
					num2 = 2;
					MyRptSpec = "";
					goto IL_0013;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				BuildSQL.RunQuery(MyRptSpec, 9, 1, -99, MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile);
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

	private void CmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string MyRptSpec = default(string);
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
				case 140:
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
							goto IL_0018;
						case 5:
							goto IL_0024;
						case 6:
							goto IL_0031;
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0018:
					num2 = 4;
					num5 = Save_Report(ref MyRptSpec);
					goto IL_0024;
					IL_0024:
					num2 = 5;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0031;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0031:
					num2 = 6;
					f_ReportSpec = MyRptSpec;
					break;
					IL_000b:
					num2 = 2;
					MyRptSpec = "";
					goto IL_0013;
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
				try0001_dispatch = 140;
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

	private void CmdCancel_Click(object sender, EventArgs e)
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

	private void mnuOpenF_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "FrmCSS - mnuOpenF_Click";
					string iniFile = "";
					int num3 = 0;
					string text = "";
					short num4 = 0;
					BuildForm.FileOpenSave("O", iniFile, "rss", "Report Format File", Globals_Renamed.gQueryDir);
					iniFile = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					if (Operators.CompareString(iniFile, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					num3 = (int)Interaction.MsgBox("Loading the new format file will clear previous report selections. Are you sure you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Do you wish to continue?");
					if (num3 != 7)
					{
						text = General_Procedures.OpenReadFileContents(iniFile);
						if (Operators.CompareString(text, "", TextCompare: false) != 0 && checked((short)Open_Report(text)) == 0)
						{
							fTitle = iniFile;
							Add_Query_To_Title();
							mnuTheme.Text = "Themes";
						}
					}
					goto end_IL_0001;
				}
				case 274:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 274;
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

	private void mnuSaveF_Click(object sender, EventArgs e)
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
				Save_External(0);
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

	private void Process_Color()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int[] customColors = default(int[]);
		int rowIndex = default(int);
		int columnIndex = default(int);
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
				case 518:
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
							goto IL_0032;
						case 5:
							goto IL_0067;
						case 7:
							goto IL_0080;
						case 8:
							goto IL_00b5;
						case 9:
						case 10:
							goto IL_00f1;
						case 11:
							goto IL_0101;
						case 12:
							goto IL_0112;
						case 13:
							goto IL_012c;
						case 14:
							goto IL_0167;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0101:
					num2 = 11;
					ColorDialog1.CustomColors = customColors;
					goto IL_0112;
					IL_0112:
					num2 = 12;
					if (ColorDialog1.ShowDialog() != DialogResult.OK)
					{
						goto end_IL_0001_3;
					}
					goto IL_012c;
					IL_00f1:
					num2 = 10;
					ColorDialog1.FullOpen = true;
					goto IL_0101;
					IL_012c:
					num2 = 13;
					GridTblBorder.Rows[rowIndex].Cells[columnIndex].Value = Strings.LCase(ColorTranslator.ToHtml(ColorDialog1.Color));
					goto IL_0167;
					IL_000b:
					num2 = 2;
					columnIndex = GridTblBorder.CurrentCell.ColumnIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					rowIndex = GridTblBorder.CurrentCell.RowIndex;
					goto IL_0032;
					IL_0032:
					num2 = 4;
					customColors = new int[2]
					{
						Information.RGB(167, 201, 66),
						Information.RGB(234, 242, 211)
					};
					goto IL_0067;
					IL_0067:
					num2 = 5;
					if (rowIndex == -1 || columnIndex != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0080;
					IL_0167:
					num2 = 14;
					GridTblBorder[columnIndex, rowIndex].Style.BackColor = ColorDialog1.Color;
					break;
					IL_0080:
					num2 = 7;
					if (Operators.ConditionalCompareObjectNotEqual(GridTblBorder.Rows[rowIndex].Cells[columnIndex].Value, "", TextCompare: false))
					{
						goto IL_00b5;
					}
					goto IL_00f1;
					IL_00b5:
					num2 = 8;
					ColorDialog1.Color = ColorTranslator.FromHtml(Conversions.ToString(GridTblBorder.Rows[rowIndex].Cells[columnIndex].Value));
					goto IL_00f1;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				GridTblBorder.CurrentCell = GridTblBorder[1, rowIndex];
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 518;
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

	private void GridTblBorder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
				Process_Color();
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

	private void GridFormat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
				case 105:
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
				DataGridView MyGrid = GridFormat;
				FontDialog FontDialog = FontDialog1;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 2);
				ColorDialog1 = ColorDialog;
				FontDialog1 = FontDialog;
				GridFormat = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 105;
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

	private void mnuSaveASF_Click(object sender, EventArgs e)
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
				Save_External(1);
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

	private void FrmCSS_Resize(object sender, EventArgs e)
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
				Form_Resize();
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

	public void Form_Resize()
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 790:
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
								goto IL_0014;
							case 4:
								goto IL_0026;
							case 5:
								goto IL_0038;
							case 6:
								goto IL_0056;
							case 7:
								goto IL_006f;
							case 8:
								goto IL_0096;
							case 9:
								goto IL_00ca;
							case 10:
								goto IL_00da;
							case 11:
								goto IL_0107;
							case 12:
								goto IL_0134;
							case 13:
								goto IL_0161;
							case 14:
								goto IL_018e;
							case 15:
								goto IL_01bc;
							case 16:
								goto IL_01cc;
							case 17:
								goto IL_01f9;
							case 18:
								goto IL_0226;
							case 19:
								goto IL_0253;
							case 20:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 21:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_01f9:
						num2 = 17;
						GridTblBorder.Columns[1].Width = (int)Math.Round((double)num5 * 0.18);
						goto IL_0226;
						IL_0226:
						num2 = 18;
						GridTblBorder.Columns[2].Width = (int)Math.Round((double)num5 * 0.18);
						goto IL_0253;
						IL_01cc:
						num2 = 16;
						GridTblBorder.Columns[0].Width = (int)Math.Round((double)num5 * 0.19);
						goto IL_01f9;
						IL_0253:
						num2 = 19;
						GridTblBorder.Columns[3].Width = (int)Math.Round((double)num5 * 0.18);
						break;
						IL_000b:
						num2 = 2;
						num6 = base.Width;
						goto IL_0014;
						IL_0014:
						num2 = 3;
						GridFormat.Width = num6 - 30;
						goto IL_0026;
						IL_0026:
						num2 = 4;
						GridTblBorder.Width = num6 - 30;
						goto IL_0038;
						IL_0038:
						num2 = 5;
						CmdBrowseOut.Left = num6 - (CmdBrowseOut.Width + 20);
						goto IL_0056;
						IL_0056:
						num2 = 6;
						CmdCancel.Left = CmdBrowseOut.Left;
						goto IL_006f;
						IL_006f:
						num2 = 7;
						CmdOK.Left = CmdCancel.Left - (CmdOK.Width + 5);
						goto IL_0096;
						IL_0096:
						num2 = 8;
						TxtCSS.Width = GridFormat.Width - (CmdBrowseOut.Width + 30 + LblCSS.Width);
						goto IL_00ca;
						IL_00ca:
						num2 = 9;
						num5 = GridFormat.Width;
						goto IL_00da;
						IL_00da:
						num2 = 10;
						GridFormat.Columns[0].Width = (int)Math.Round((double)num5 * 0.21);
						goto IL_0107;
						IL_0107:
						num2 = 11;
						GridFormat.Columns[1].Width = (int)Math.Round((double)num5 * 0.2);
						goto IL_0134;
						IL_0134:
						num2 = 12;
						GridFormat.Columns[2].Width = (int)Math.Round((double)num5 * 0.17);
						goto IL_0161;
						IL_0161:
						num2 = 13;
						GridFormat.Columns[7].Width = (int)Math.Round((double)num5 * 0.13);
						goto IL_018e;
						IL_018e:
						num2 = 14;
						GridFormat.Columns[9].Width = (int)Math.Round((double)num5 * 0.15);
						goto IL_01bc;
						IL_01bc:
						num2 = 15;
						num5 = GridTblBorder.Width;
						goto IL_01cc;
						end_IL_0001_2:
						break;
					}
					num2 = 20;
					GridTblBorder.Columns[4].Width = (int)Math.Round((double)num5 * 0.18);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 790;
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

	private void mnuTheme_DropDownClosed(object sender, EventArgs e)
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
				case 104:
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
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 4:
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(mnuTheme.Text, "Themes", TextCompare: false) != 0)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				New_Theme();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 104;
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

	private void GridFormat_KeyDown(object sender, KeyEventArgs e)
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
				FontDialog FontDialog;
				ColorDialog ColorDialog;
				DataGridView MyGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 330:
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
							goto IL_0041;
						case 7:
							goto IL_0053;
						case 8:
							goto IL_0067;
						case 10:
							goto IL_0077;
						case 11:
							goto IL_00b3;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0067:
					num2 = 8;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0077:
					num2 = 10;
					if ((GridFormat.CurrentCell.ColumnIndex != 1 && GridFormat.CurrentCell.ColumnIndex != 2) || e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_00b3;
					IL_0053:
					num2 = 7;
					CmdDel_Click(CmdDel, new EventArgs());
					goto IL_0067;
					IL_00b3:
					num2 = 11;
					MyGrid = (DataGridView)sender;
					FontDialog = FontDialog1;
					ColorDialog = ColorDialog1;
					BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 2);
					ColorDialog1 = ColorDialog;
					FontDialog1 = FontDialog;
					sender = MyGrid;
					break;
					IL_000b:
					num2 = 2;
					if (e.KeyValue == 117)
					{
						goto IL_001b;
					}
					goto IL_0041;
					IL_001b:
					num2 = 3;
					MyGrid = (DataGridView)sender;
					GridModule.Grid_Row_Sel(ref MyGrid);
					sender = MyGrid;
					goto IL_0031;
					IL_0031:
					num2 = 4;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0041:
					num2 = 6;
					if (e.KeyValue == 46)
					{
						goto IL_0053;
					}
					goto IL_0077;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 330;
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

	private void GridTblBorder_KeyDown(object sender, KeyEventArgs e)
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
				case 148:
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
							goto IL_0055;
						case 4:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null), 0, TextCompare: false) || e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0055;
					IL_0055:
					num2 = 3;
					Process_Color();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 148;
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

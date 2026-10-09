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
internal class frmSQLfilter : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("chkUp")]
	private CheckBox _chkUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdPaste")]
	private Button _cmdPaste;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdClear")]
	private Button _CmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbColIG")]
	private ComboBox _cmbColIG;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtBox2")]
	private ComboBox _TxtBox2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtBox1")]
	private ComboBox _TxtBox1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("ChkPrompt")]
	private CheckBox _ChkPrompt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdHelp")]
	private Button _CmdHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCal2")]
	private Button _CmdCal2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCal1")]
	private Button _CmdCal1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Label1")]
	private Label _Label1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GrdList")]
	private DataGridView _GrdList;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPasteGrid")]
	private ToolStripMenuItem _mnuPasteGrid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearGrid")]
	private ToolStripMenuItem _mnuClearGrid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbTxt")]
	private ComboBox _CmbTxt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp")]
	private ToolStripMenuItem _mnuHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEditGrdList")]
	private ToolStripMenuItem _mnuEditGrdList;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCopy2")]
	private ToolStripMenuItem _mnuCopy2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Combo1")]
	private ComboBox _Combo1;

	private short gSpecialTest;

	private const short l_TotalRows = 1000;

	private short ll_ObjectType;

	private string ll_DBType;

	private string ll_DbNameType;

	private string ll_PromptTxt;

	private string ll_PromptDef;

	private string ll_Last1;

	private string ll_Last2;

	private bool ll_IsLoaded;

	private string ll_TTColID;

	private Globals_Renamed.AliasArray_Type[] g_AliasArray;

	private short g_NoAlias;

	private short f_ROW;

	private short f_COL;

	private short f_TrueRow;

	private bool CmbBoxKey;

	private bool AllowIncExtract;

	public string ll_KeepTrueDT;

	private bool ll_NoPrompt;

	public virtual CheckBox chkUp
	{
		[CompilerGenerated]
		get
		{
			return _chkUp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkUp_CheckedChanged;
			KeyPressEventHandler value3 = ChkPrompt_KeyPress;
			EventHandler value4 = chkUp_GotFocus;
			EventHandler value5 = chkUp_LostFocus;
			CheckBox checkBox = _chkUp;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
				checkBox.KeyPress -= value3;
				checkBox.GotFocus -= value4;
				checkBox.LostFocus -= value5;
			}
			_chkUp = value;
			checkBox = _chkUp;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
				checkBox.KeyPress += value3;
				checkBox.GotFocus += value4;
				checkBox.LostFocus += value5;
			}
		}
	}

	[field: AccessedThroughProperty("cmdLoadGrid")]
	public virtual Button cmdLoadGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdPaste
	{
		[CompilerGenerated]
		get
		{
			return _cmdPaste;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPaste_Click;
			Button button = _cmdPaste;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdPaste = value;
			button = _cmdPaste;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdClear
	{
		[CompilerGenerated]
		get
		{
			return _CmdClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClear_Click;
			Button button = _CmdClear;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdClear = value;
			button = _CmdClear;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual ComboBox cmbColIG
	{
		[CompilerGenerated]
		get
		{
			return _cmbColIG;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbcolIG_SelectedIndexChanged;
			EventHandler value3 = cmbcolIG_DropDown;
			ComboBox comboBox = _cmbColIG;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.DropDown -= value3;
			}
			_cmbColIG = value;
			comboBox = _cmbColIG;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.DropDown += value3;
			}
		}
	}

	public virtual ComboBox TxtBox2
	{
		[CompilerGenerated]
		get
		{
			return _TxtBox2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtBox2_SelectedIndexChanged;
			EventHandler value3 = TxtBox2_DropDown;
			EventHandler value4 = TxtBox2_Enter;
			KeyPressEventHandler value5 = txtbox2_KeyPress;
			ComboBox comboBox = _TxtBox2;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.DropDown -= value3;
				comboBox.Enter -= value4;
				comboBox.KeyPress -= value5;
			}
			_TxtBox2 = value;
			comboBox = _TxtBox2;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.DropDown += value3;
				comboBox.Enter += value4;
				comboBox.KeyPress += value5;
			}
		}
	}

	public virtual ComboBox TxtBox1
	{
		[CompilerGenerated]
		get
		{
			return _TxtBox1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtBox1_Enter;
			KeyPressEventHandler value3 = txtbox1_KeyPress;
			ComboBox comboBox = _TxtBox1;
			if (comboBox != null)
			{
				comboBox.Enter -= value2;
				comboBox.KeyPress -= value3;
			}
			_TxtBox1 = value;
			comboBox = _TxtBox1;
			if (comboBox != null)
			{
				comboBox.Enter += value2;
				comboBox.KeyPress += value3;
			}
		}
	}

	public virtual CheckBox ChkPrompt
	{
		[CompilerGenerated]
		get
		{
			return _ChkPrompt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ChkPrompt_CheckStateChanged;
			KeyPressEventHandler value3 = ChkPrompt_KeyPress;
			EventHandler value4 = chkUp_GotFocus;
			EventHandler value5 = chkUp_LostFocus;
			CheckBox checkBox = _ChkPrompt;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged -= value2;
				checkBox.KeyPress -= value3;
				checkBox.GotFocus -= value4;
				checkBox.LostFocus -= value5;
			}
			_ChkPrompt = value;
			checkBox = _ChkPrompt;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged += value2;
				checkBox.KeyPress += value3;
				checkBox.GotFocus += value4;
				checkBox.LostFocus += value5;
			}
		}
	}

	[field: AccessedThroughProperty("TxtEscape")]
	public virtual TextBox TxtEscape
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button CmdHelp
	{
		[CompilerGenerated]
		get
		{
			return _CmdHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHelp_Click;
			Button button = _CmdHelp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdHelp = value;
			button = _CmdHelp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdOK
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
			EventHandler value2 = cmdOK_Click;
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

	public virtual Button CmdCancel
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

	public virtual Button CmdCal2
	{
		[CompilerGenerated]
		get
		{
			return _CmdCal2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdcal2_Click;
			Button button = _CmdCal2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdCal2 = value;
			button = _CmdCal2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdCal1
	{
		[CompilerGenerated]
		get
		{
			return _CmdCal1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdcal1_Click;
			Button button = _CmdCal1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdCal1 = value;
			button = _CmdCal1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblColIG")]
	public virtual Label lblColIG
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblEscape")]
	public virtual Label LblEscape
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblbox2")]
	public virtual Label lblbox2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbldesc")]
	public virtual Label lbldesc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblbox1")]
	public virtual Label lblbox1
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

	[field: AccessedThroughProperty("Label2")]
	public virtual Label Label2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Label Label1
	{
		[CompilerGenerated]
		get
		{
			return _Label1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Label1_Click;
			Label label = _Label1;
			if (label != null)
			{
				label.Click -= value2;
			}
			_Label1 = value;
			label = _Label1;
			if (label != null)
			{
				label.Click += value2;
			}
		}
	}

	internal virtual DataGridView GrdList
	{
		[CompilerGenerated]
		get
		{
			return _GrdList;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GrdList_Click;
			KeyEventHandler value3 = GrdList_KeyDown;
			KeyPressEventHandler value4 = GrdList_KeyPress;
			EventHandler value5 = GrdList_Resize;
			ScrollEventHandler value6 = GrdList_Scroll;
			DataGridView dataGridView = _GrdList;
			if (dataGridView != null)
			{
				dataGridView.Click -= value2;
				dataGridView.KeyDown -= value3;
				dataGridView.KeyPress -= value4;
				dataGridView.Resize -= value5;
				dataGridView.Scroll -= value6;
			}
			_GrdList = value;
			dataGridView = _GrdList;
			if (dataGridView != null)
			{
				dataGridView.Click += value2;
				dataGridView.KeyDown += value3;
				dataGridView.KeyPress += value4;
				dataGridView.Resize += value5;
				dataGridView.Scroll += value6;
			}
		}
	}

	[field: AccessedThroughProperty("ContextMenuGrdList")]
	internal virtual ContextMenuStrip ContextMenuGrdList
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuPasteGrid
	{
		[CompilerGenerated]
		get
		{
			return _mnuPasteGrid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPasteGrid_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPasteGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPasteGrid = value;
			toolStripMenuItem = _mnuPasteGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuClearGrid
	{
		[CompilerGenerated]
		get
		{
			return _mnuClearGrid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClearGrid_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClearGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClearGrid = value;
			toolStripMenuItem = _mnuClearGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GrdListCOlumn")]
	internal virtual DataGridViewTextBoxColumn GrdListCOlumn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ComboBox CmbTxt
	{
		[CompilerGenerated]
		get
		{
			return _CmbTxt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmbTxt_DropDown;
			KeyPressEventHandler value3 = CmbTxt_KeyPress;
			EventHandler value4 = CmbTxt_Leave;
			MouseEventHandler value5 = CmbTxt_MouseDown;
			EventHandler value6 = CmbTxt_SelectedIndexChanged;
			ComboBox comboBox = _CmbTxt;
			if (comboBox != null)
			{
				comboBox.DropDown -= value2;
				comboBox.KeyPress -= value3;
				comboBox.Leave -= value4;
				comboBox.MouseDown -= value5;
				comboBox.SelectedIndexChanged -= value6;
			}
			_CmbTxt = value;
			comboBox = _CmbTxt;
			if (comboBox != null)
			{
				comboBox.DropDown += value2;
				comboBox.KeyPress += value3;
				comboBox.Leave += value4;
				comboBox.MouseDown += value5;
				comboBox.SelectedIndexChanged += value6;
			}
		}
	}

	[field: AccessedThroughProperty("MnuMain")]
	internal virtual MenuStrip MnuMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuHelp
	{
		[CompilerGenerated]
		get
		{
			return _mnuHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelp_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHelp;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHelp = value;
			toolStripMenuItem = _mnuHelp;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuEditGrdList
	{
		[CompilerGenerated]
		get
		{
			return _mnuEditGrdList;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuEditGrdList_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuEditGrdList;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuEditGrdList = value;
			toolStripMenuItem = _mnuEditGrdList;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtIncrement")]
	internal virtual TextBox TxtIncrement
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuCopy2
	{
		[CompilerGenerated]
		get
		{
			return _mnuCopy2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCopy2_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCopy2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCopy2 = value;
			toolStripMenuItem = _mnuCopy2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblinc")]
	internal virtual Label lblinc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ComboBox Combo1
	{
		[CompilerGenerated]
		get
		{
			return _Combo1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Combo1_SelectedIndexChanged;
			EventHandler value3 = Combo1_DropDown;
			EventHandler value4 = Combo1_DropDownClosed;
			ComboBox comboBox = _Combo1;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.DropDown -= value3;
				comboBox.DropDownClosed -= value4;
			}
			_Combo1 = value;
			comboBox = _Combo1;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.DropDown += value3;
				comboBox.DropDownClosed += value4;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCloseGrid")]
	internal virtual Button cmdCloseGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmSQLfilter()
	{
		base.Activated += frmSQLfilter_Activated;
		base.Load += frmSQLfilter_Load;
		base.FormClosed += frmSQLfilter_FormClosed;
		gSpecialTest = 0;
		ll_ObjectType = 0;
		ll_DBType = "0";
		ll_DbNameType = "";
		ll_PromptTxt = "";
		ll_PromptDef = "";
		ll_Last1 = "";
		ll_Last2 = "";
		ll_IsLoaded = false;
		ll_TTColID = "";
		g_AliasArray = new Globals_Renamed.AliasArray_Type[134];
		g_NoAlias = -1;
		f_ROW = 0;
		f_COL = 0;
		f_TrueRow = -1;
		CmbBoxKey = false;
		AllowIncExtract = true;
		ll_KeepTrueDT = "";
		ll_NoPrompt = false;
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
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.chkUp = new System.Windows.Forms.CheckBox();
		this.cmdLoadGrid = new System.Windows.Forms.Button();
		this.ChkPrompt = new System.Windows.Forms.CheckBox();
		this.LblEscape = new System.Windows.Forms.Label();
		this.cmdPaste = new System.Windows.Forms.Button();
		this.CmdClear = new System.Windows.Forms.Button();
		this.CmdCal2 = new System.Windows.Forms.Button();
		this.CmdCal1 = new System.Windows.Forms.Button();
		this.TxtIncrement = new System.Windows.Forms.TextBox();
		this.lblinc = new System.Windows.Forms.Label();
		this.cmbColIG = new System.Windows.Forms.ComboBox();
		this.TxtBox2 = new System.Windows.Forms.ComboBox();
		this.TxtBox1 = new System.Windows.Forms.ComboBox();
		this.TxtEscape = new System.Windows.Forms.TextBox();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.Combo1 = new System.Windows.Forms.ComboBox();
		this.lblColIG = new System.Windows.Forms.Label();
		this.lblbox2 = new System.Windows.Forms.Label();
		this.lbldesc = new System.Windows.Forms.Label();
		this.lblbox1 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.GrdList = new System.Windows.Forms.DataGridView();
		this.GrdListCOlumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ContextMenuGrdList = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuPasteGrid = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCopy2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClearGrid = new System.Windows.Forms.ToolStripMenuItem();
		this.CmdHelp = new System.Windows.Forms.Button();
		this.CmbTxt = new System.Windows.Forms.ComboBox();
		this.MnuMain = new System.Windows.Forms.MenuStrip();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEditGrdList = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdCloseGrid = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.GrdList).BeginInit();
		this.ContextMenuGrdList.SuspendLayout();
		this.MnuMain.SuspendLayout();
		base.SuspendLayout();
		this.chkUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chkUp.BackColor = System.Drawing.SystemColors.Control;
		this.chkUp.Checked = true;
		this.chkUp.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkUp.Cursor = System.Windows.Forms.Cursors.Default;
		this.chkUp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkUp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.chkUp.Location = new System.Drawing.Point(285, 73);
		this.chkUp.Name = "chkUp";
		this.chkUp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.chkUp.Size = new System.Drawing.Size(80, 20);
		this.chkUp.TabIndex = 16;
		this.chkUp.Text = "Caps";
		this.ToolTip1.SetToolTip(this.chkUp, "Enter values in Upper case");
		this.chkUp.UseVisualStyleBackColor = false;
		this.chkUp.Visible = false;
		this.cmdLoadGrid.BackColor = System.Drawing.SystemColors.Control;
		this.cmdLoadGrid.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdLoadGrid.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdLoadGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdLoadGrid.Location = new System.Drawing.Point(268, 2);
		this.cmdLoadGrid.Name = "cmdLoadGrid";
		this.cmdLoadGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdLoadGrid.Size = new System.Drawing.Size(61, 40);
		this.cmdLoadGrid.TabIndex = 13;
		this.cmdLoadGrid.Text = "Show Grid";
		this.ToolTip1.SetToolTip(this.cmdLoadGrid, "Load Grid With Help Values After Running Help Query (i.e., click ?)");
		this.cmdLoadGrid.UseVisualStyleBackColor = false;
		this.cmdLoadGrid.Visible = false;
		this.ChkPrompt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ChkPrompt.BackColor = System.Drawing.SystemColors.Control;
		this.ChkPrompt.Cursor = System.Windows.Forms.Cursors.Default;
		this.ChkPrompt.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ChkPrompt.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ChkPrompt.Location = new System.Drawing.Point(285, 49);
		this.ChkPrompt.Name = "ChkPrompt";
		this.ChkPrompt.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.ChkPrompt.Size = new System.Drawing.Size(105, 21);
		this.ChkPrompt.TabIndex = 15;
		this.ChkPrompt.Text = "Prompt";
		this.ToolTip1.SetToolTip(this.ChkPrompt, "Prompt for values at run-time");
		this.ChkPrompt.UseVisualStyleBackColor = false;
		this.LblEscape.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblEscape.AutoSize = true;
		this.LblEscape.BackColor = System.Drawing.SystemColors.Control;
		this.LblEscape.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblEscape.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblEscape.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblEscape.Location = new System.Drawing.Point(282, 107);
		this.LblEscape.Name = "LblEscape";
		this.LblEscape.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblEscape.Size = new System.Drawing.Size(62, 18);
		this.LblEscape.TabIndex = 19;
		this.LblEscape.Text = "Escape";
		this.ToolTip1.SetToolTip(this.LblEscape, "Escape Character for LIKE Filter");
		this.LblEscape.Visible = false;
		this.cmdPaste.BackColor = System.Drawing.SystemColors.Control;
		this.cmdPaste.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdPaste.Enabled = false;
		this.cmdPaste.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdPaste.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdPaste.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdPaste.ImageIndex = 2;
		this.cmdPaste.Location = new System.Drawing.Point(216, 2);
		this.cmdPaste.Name = "cmdPaste";
		this.cmdPaste.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdPaste.Size = new System.Drawing.Size(52, 40);
		this.cmdPaste.TabIndex = 12;
		this.cmdPaste.Text = "Paste";
		this.cmdPaste.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.cmdPaste, "Paste Clipboard contents to first empty row in grid");
		this.cmdPaste.UseVisualStyleBackColor = false;
		this.CmdClear.BackColor = System.Drawing.SystemColors.Control;
		this.CmdClear.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdClear.Enabled = false;
		this.CmdClear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdClear.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdClear.ImageIndex = 0;
		this.CmdClear.Location = new System.Drawing.Point(164, 2);
		this.CmdClear.Name = "CmdClear";
		this.CmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdClear.Size = new System.Drawing.Size(52, 40);
		this.CmdClear.TabIndex = 11;
		this.CmdClear.Text = "Clear";
		this.CmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdClear, "Clear the Grid");
		this.CmdClear.UseVisualStyleBackColor = false;
		this.CmdCal2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCal2.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCal2.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCal2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCal2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCal2.ImageIndex = 3;
		this.CmdCal2.Location = new System.Drawing.Point(360, 231);
		this.CmdCal2.Name = "CmdCal2";
		this.CmdCal2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCal2.Size = new System.Drawing.Size(27, 22);
		this.CmdCal2.TabIndex = 7;
		this.CmdCal2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdCal2, "Get Valid Values");
		this.CmdCal2.UseVisualStyleBackColor = false;
		this.CmdCal1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCal1.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCal1.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCal1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCal1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCal1.ImageIndex = 3;
		this.CmdCal1.Location = new System.Drawing.Point(360, 176);
		this.CmdCal1.Name = "CmdCal1";
		this.CmdCal1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCal1.Size = new System.Drawing.Size(27, 22);
		this.CmdCal1.TabIndex = 2;
		this.CmdCal1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdCal1, "Get Valid Values");
		this.CmdCal1.UseVisualStyleBackColor = false;
		this.TxtIncrement.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtIncrement.Location = new System.Drawing.Point(355, 205);
		this.TxtIncrement.Name = "TxtIncrement";
		this.TxtIncrement.Size = new System.Drawing.Size(40, 26);
		this.TxtIncrement.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.TxtIncrement, "Number of filter values to process at a time (1 to 999) ");
		this.TxtIncrement.Visible = false;
		this.lblinc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblinc.AutoSize = true;
		this.lblinc.Location = new System.Drawing.Point(362, 155);
		this.lblinc.Name = "lblinc";
		this.lblinc.Size = new System.Drawing.Size(27, 18);
		this.lblinc.TabIndex = 26;
		this.lblinc.Text = "Inc";
		this.ToolTip1.SetToolTip(this.lblinc, "Number of filter values to process at a time (1 to 999) ");
		this.lblinc.Visible = false;
		this.cmbColIG.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbColIG.BackColor = System.Drawing.SystemColors.Window;
		this.cmbColIG.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbColIG.DropDownWidth = 190;
		this.cmbColIG.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbColIG.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbColIG.Location = new System.Drawing.Point(0, 230);
		this.cmbColIG.Name = "cmbColIG";
		this.cmbColIG.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbColIG.Size = new System.Drawing.Size(112, 26);
		this.cmbColIG.TabIndex = 5;
		this.cmbColIG.Text = "1";
		this.cmbColIG.Visible = false;
		this.TxtBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBox2.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtBox2.DropDownWidth = 400;
		this.TxtBox2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBox2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtBox2.Location = new System.Drawing.Point(-1, 231);
		this.TxtBox2.Name = "TxtBox2";
		this.TxtBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBox2.Size = new System.Drawing.Size(347, 26);
		this.TxtBox2.TabIndex = 4;
		this.TxtBox2.Visible = false;
		this.TxtBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBox1.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBox1.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtBox1.DropDownWidth = 400;
		this.TxtBox1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBox1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtBox1.Location = new System.Drawing.Point(0, 176);
		this.TxtBox1.Name = "TxtBox1";
		this.TxtBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBox1.Size = new System.Drawing.Size(347, 26);
		this.TxtBox1.TabIndex = 0;
		this.TxtBox1.Visible = false;
		this.TxtEscape.AcceptsReturn = true;
		this.TxtEscape.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtEscape.BackColor = System.Drawing.SystemColors.Window;
		this.TxtEscape.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtEscape.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtEscape.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtEscape.Location = new System.Drawing.Point(285, 123);
		this.TxtEscape.MaxLength = 0;
		this.TxtEscape.Name = "TxtEscape";
		this.TxtEscape.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtEscape.Size = new System.Drawing.Size(20, 26);
		this.TxtEscape.TabIndex = 18;
		this.TxtEscape.Visible = false;
		this.CmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdOK.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdOK.Location = new System.Drawing.Point(0, 2);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdOK.Size = new System.Drawing.Size(52, 40);
		this.CmdOK.TabIndex = 8;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = false;
		this.CmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCancel.Location = new System.Drawing.Point(52, 2);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCancel.Size = new System.Drawing.Size(60, 40);
		this.CmdCancel.TabIndex = 9;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = false;
		this.Combo1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Combo1.BackColor = System.Drawing.SystemColors.Window;
		this.Combo1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Combo1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Combo1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Combo1.Location = new System.Drawing.Point(0, 123);
		this.Combo1.Name = "Combo1";
		this.Combo1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Combo1.Size = new System.Drawing.Size(267, 26);
		this.Combo1.TabIndex = 17;
		this.lblColIG.AutoSize = true;
		this.lblColIG.BackColor = System.Drawing.Color.Transparent;
		this.lblColIG.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblColIG.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblColIG.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblColIG.Location = new System.Drawing.Point(119, 230);
		this.lblColIG.Name = "lblColIG";
		this.lblColIG.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblColIG.Size = new System.Drawing.Size(177, 18);
		this.lblColIG.TabIndex = 21;
		this.lblColIG.Text = "Col No(s) separated by ;";
		this.lblColIG.Visible = false;
		this.lblbox2.AutoSize = true;
		this.lblbox2.BackColor = System.Drawing.Color.Transparent;
		this.lblbox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblbox2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblbox2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblbox2.Location = new System.Drawing.Point(-1, 212);
		this.lblbox2.Name = "lblbox2";
		this.lblbox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblbox2.Size = new System.Drawing.Size(57, 18);
		this.lblbox2.TabIndex = 17;
		this.lblbox2.Text = "lblbox2";
		this.lblbox2.Visible = false;
		this.lbldesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lbldesc.BackColor = System.Drawing.SystemColors.Control;
		this.lbldesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbldesc.Cursor = System.Windows.Forms.Cursors.Default;
		this.lbldesc.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldesc.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
		this.lbldesc.Location = new System.Drawing.Point(0, 320);
		this.lbldesc.Name = "lbldesc";
		this.lbldesc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbldesc.Size = new System.Drawing.Size(396, 174);
		this.lbldesc.TabIndex = 18;
		this.lblbox1.AutoSize = true;
		this.lblbox1.BackColor = System.Drawing.Color.Transparent;
		this.lblbox1.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblbox1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblbox1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblbox1.Location = new System.Drawing.Point(0, 155);
		this.lblbox1.Name = "lblbox1";
		this.lblbox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblbox1.Size = new System.Drawing.Size(57, 18);
		this.lblbox1.TabIndex = 16;
		this.lblbox1.Text = "lblbox1";
		this.lblbox1.Visible = false;
		this.Label3.AutoSize = true;
		this.Label3.BackColor = System.Drawing.Color.Transparent;
		this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label3.Location = new System.Drawing.Point(-1, 102);
		this.Label3.Name = "Label3";
		this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label3.Size = new System.Drawing.Size(70, 18);
		this.Label3.TabIndex = 15;
		this.Label3.Text = "Operator";
		this.Label2.AutoSize = true;
		this.Label2.BackColor = System.Drawing.Color.Transparent;
		this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label2.Location = new System.Drawing.Point(0, 49);
		this.Label2.Name = "Label2";
		this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label2.Size = new System.Drawing.Size(61, 18);
		this.Label2.TabIndex = 14;
		this.Label2.Text = "Column";
		this.Label1.AutoSize = true;
		this.Label1.BackColor = System.Drawing.Color.White;
		this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label1.Location = new System.Drawing.Point(0, 69);
		this.Label1.Name = "Label1";
		this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label1.Size = new System.Drawing.Size(52, 20);
		this.Label1.TabIndex = 13;
		this.Label1.Text = "label1";
		this.GrdList.AllowUserToAddRows = false;
		this.GrdList.AllowUserToDeleteRows = false;
		this.GrdList.AllowUserToResizeColumns = false;
		this.GrdList.AllowUserToResizeRows = false;
		this.GrdList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrdList.BackgroundColor = System.Drawing.Color.White;
		this.GrdList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		this.GrdList.ColumnHeadersHeight = 4;
		this.GrdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.GrdList.ColumnHeadersVisible = false;
		this.GrdList.Columns.AddRange(this.GrdListCOlumn);
		this.GrdList.ContextMenuStrip = this.ContextMenuGrdList;
		this.GrdList.Location = new System.Drawing.Point(0, 176);
		this.GrdList.Name = "GrdList";
		this.GrdList.ReadOnly = true;
		this.GrdList.RowHeadersVisible = false;
		this.GrdList.RowHeadersWidth = 4;
		this.GrdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GrdList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.GrdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GrdList.Size = new System.Drawing.Size(346, 115);
		this.GrdList.StandardTab = true;
		this.GrdList.TabIndex = 1;
		this.GrdListCOlumn.HeaderText = "";
		this.GrdListCOlumn.MinimumWidth = 6;
		this.GrdListCOlumn.Name = "GrdListCOlumn";
		this.GrdListCOlumn.ReadOnly = true;
		this.GrdListCOlumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.GrdListCOlumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.GrdListCOlumn.Width = 95;
		this.ContextMenuGrdList.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuGrdList.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuPasteGrid, this.mnuCopy2, this.mnuClearGrid });
		this.ContextMenuGrdList.Name = "ContextMenuGrdList";
		this.ContextMenuGrdList.Size = new System.Drawing.Size(527, 100);
		this.mnuPasteGrid.Name = "mnuPasteGrid";
		this.mnuPasteGrid.Size = new System.Drawing.Size(526, 32);
		this.mnuPasteGrid.Text = "Paste Clipboard Contents to First Empty Row in the Grid";
		this.mnuCopy2.Name = "mnuCopy2";
		this.mnuCopy2.Size = new System.Drawing.Size(526, 32);
		this.mnuCopy2.Text = "Copy Selected Cells";
		this.mnuClearGrid.Name = "mnuClearGrid";
		this.mnuClearGrid.Size = new System.Drawing.Size(526, 32);
		this.mnuClearGrid.Text = "Clear the Grid";
		this.CmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.CmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdHelp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdHelp.ImageIndex = 1;
		this.CmdHelp.Location = new System.Drawing.Point(112, 2);
		this.CmdHelp.Name = "CmdHelp";
		this.CmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdHelp.Size = new System.Drawing.Size(52, 40);
		this.CmdHelp.TabIndex = 10;
		this.CmdHelp.Text = "Help";
		this.CmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdHelp.UseVisualStyleBackColor = false;
		this.CmbTxt.FormattingEnabled = true;
		this.CmbTxt.Location = new System.Drawing.Point(0, 259);
		this.CmbTxt.Name = "CmbTxt";
		this.CmbTxt.Size = new System.Drawing.Size(212, 26);
		this.CmbTxt.TabIndex = 6;
		this.CmbTxt.Visible = false;
		this.MnuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
		this.MnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuHelp, this.mnuEditGrdList });
		this.MnuMain.Location = new System.Drawing.Point(0, 0);
		this.MnuMain.Name = "MnuMain";
		this.MnuMain.Size = new System.Drawing.Size(322, 24);
		this.MnuMain.TabIndex = 24;
		this.MnuMain.Visible = false;
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.mnuHelp.Size = new System.Drawing.Size(188, 20);
		this.mnuHelp.Text = "ToolStripMenuItem1";
		this.mnuEditGrdList.Name = "mnuEditGrdList";
		this.mnuEditGrdList.ShortcutKeys = System.Windows.Forms.Keys.F2;
		this.mnuEditGrdList.Size = new System.Drawing.Size(58, 20);
		this.mnuEditGrdList.Text = "Edit";
		this.cmdCloseGrid.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdCloseGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCloseGrid.Location = new System.Drawing.Point(328, 2);
		this.cmdCloseGrid.Name = "cmdCloseGrid";
		this.cmdCloseGrid.Size = new System.Drawing.Size(61, 40);
		this.cmdCloseGrid.TabIndex = 14;
		this.cmdCloseGrid.Text = "Close Grid";
		this.cmdCloseGrid.UseVisualStyleBackColor = false;
		this.cmdCloseGrid.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(397, 498);
		base.Controls.Add(this.cmdCloseGrid);
		base.Controls.Add(this.lblinc);
		base.Controls.Add(this.TxtIncrement);
		base.Controls.Add(this.MnuMain);
		base.Controls.Add(this.cmdPaste);
		base.Controls.Add(this.CmbTxt);
		base.Controls.Add(this.cmdLoadGrid);
		base.Controls.Add(this.cmbColIG);
		base.Controls.Add(this.chkUp);
		base.Controls.Add(this.CmdClear);
		base.Controls.Add(this.CmdHelp);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.TxtBox2);
		base.Controls.Add(this.TxtBox1);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.ChkPrompt);
		base.Controls.Add(this.TxtEscape);
		base.Controls.Add(this.CmdCal2);
		base.Controls.Add(this.CmdCal1);
		base.Controls.Add(this.Combo1);
		base.Controls.Add(this.lblColIG);
		base.Controls.Add(this.lblbox2);
		base.Controls.Add(this.lblbox1);
		base.Controls.Add(this.LblEscape);
		base.Controls.Add(this.Label3);
		base.Controls.Add(this.Label2);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.GrdList);
		base.Controls.Add(this.lbldesc);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.WindowText;
		base.Location = new System.Drawing.Point(255, 194);
		base.MainMenuStrip = this.MnuMain;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmSQLfilter";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Row Filter";
		((System.ComponentModel.ISupportInitialize)this.GrdList).EndInit();
		this.ContextMenuGrdList.ResumeLayout(false);
		this.MnuMain.ResumeLayout(false);
		this.MnuMain.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void set_Button_default(string MyOperator)
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
				case 151:
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
							goto IL_004b;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!LikeOperator.LikeString(Strings.LCase(MyOperator), "*in", CompareMethod.Binary) && Operators.CompareString(Strings.LCase(MyOperator), "like list", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(MyOperator), "*regex list", CompareMethod.Binary))
					{
						break;
					}
					goto IL_004b;
					IL_004b:
					num2 = 3;
					base.AcceptButton = null;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				base.AcceptButton = CmdOK;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 151;
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

	public void Load_Text_Boxes()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string[] DynArray = default(string[]);
		short num5 = default(short);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string delim = default(string);
		int num6 = default(int);
		string right = default(string);
		short gNoOperators = default(short);
		short gNoOperators2 = default(short);
		short num7 = default(short);
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
					case 2207:
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
							case 4:
								goto IL_0033;
							case 5:
								goto IL_003b;
							case 6:
								goto IL_0044;
							case 7:
								goto IL_004d;
							case 8:
								goto IL_0056;
							case 9:
								goto IL_005b;
							case 10:
								goto IL_0072;
							case 11:
								goto IL_0078;
							case 12:
								goto IL_0082;
							case 13:
								goto IL_0090;
							case 14:
								goto IL_00a0;
							case 15:
								goto IL_00b0;
							case 16:
								goto IL_00c3;
							case 17:
								goto IL_00d7;
							case 18:
								goto IL_00eb;
							case 19:
								goto IL_0106;
							case 20:
								goto IL_0138;
							case 21:
								goto IL_0151;
							case 22:
								goto IL_016a;
							case 23:
								goto IL_0183;
							case 24:
							case 25:
								goto IL_019e;
							case 26:
								goto IL_01df;
							case 28:
								goto IL_01ed;
							case 27:
							case 29:
							case 30:
								goto IL_01f8;
							case 31:
								goto IL_0207;
							case 32:
								goto IL_022c;
							case 33:
								goto IL_0251;
							case 34:
							case 35:
								goto IL_0278;
							case 37:
								goto IL_0291;
							case 38:
								goto IL_02a4;
							case 39:
								goto IL_02bc;
							case 41:
								goto IL_02cc;
							case 42:
								goto IL_02e4;
							case 44:
								goto IL_02f4;
							case 45:
								goto IL_0321;
							case 47:
								goto IL_0331;
							case 48:
								goto IL_035e;
							case 50:
								goto IL_036e;
							case 51:
								goto IL_0386;
							case 53:
								goto IL_0396;
							case 54:
								goto IL_03ae;
							case 56:
								goto IL_03be;
							case 57:
								goto IL_03d6;
							case 59:
								goto IL_03e6;
							case 60:
								goto IL_03fe;
							case 62:
								goto IL_040e;
							case 63:
								goto IL_0426;
							case 65:
								goto IL_0436;
							case 66:
								goto IL_044e;
							case 68:
								goto IL_045b;
							case 69:
								goto IL_0473;
							case 71:
								goto IL_0480;
							case 72:
								goto IL_0498;
							case 74:
								goto IL_04a5;
							case 75:
								goto IL_04bd;
							case 77:
								goto IL_04cb;
							case 40:
							case 43:
							case 46:
							case 49:
							case 52:
							case 55:
							case 58:
							case 61:
							case 64:
							case 67:
							case 70:
							case 73:
							case 76:
							case 78:
							case 79:
								goto IL_04d6;
							case 80:
								goto IL_04e5;
							case 81:
								goto IL_050a;
							case 82:
								goto IL_052f;
							case 83:
							case 84:
								goto IL_0556;
							case 36:
							case 85:
							case 86:
								goto IL_056a;
							case 87:
								goto IL_057b;
							case 88:
							case 89:
								goto IL_058e;
							case 90:
								goto IL_05b4;
							case 91:
								goto IL_05c8;
							case 92:
								goto IL_05dc;
							case 93:
								goto IL_05f0;
							case 94:
								goto IL_05fa;
							case 95:
								goto IL_060a;
							case 96:
								goto IL_0614;
							case 97:
								goto IL_0627;
							case 98:
								goto IL_0638;
							case 99:
								goto IL_0645;
							case 100:
								goto IL_065e;
							case 101:
								goto IL_0677;
							case 102:
								goto IL_0690;
							case 103:
								goto IL_06a0;
							case 104:
								goto IL_06b1;
							case 105:
								goto IL_06c2;
							case 106:
							case 107:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 3:
							case 108:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_06c2:
						num2 = 105;
						cmdLoadGrid.Enabled = false;
						break;
						IL_0645:
						num2 = 99;
						TxtBox1.Items.Add(DynArray[num5]);
						goto IL_065e;
						IL_06b1:
						num2 = 104;
						TxtBox2.Text = text;
						goto IL_06c2;
						IL_065e:
						num2 = 100;
						TxtBox2.Items.Add(DynArray[num5]);
						goto IL_0677;
						IL_000b:
						num2 = 2;
						if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary))
						{
							goto end_IL_0001_3;
						}
						goto IL_0033;
						IL_0033:
						num2 = 4;
						text2 = "";
						goto IL_003b;
						IL_003b:
						num2 = 5;
						text = "";
						goto IL_0044;
						IL_0044:
						num2 = 6;
						text3 = "";
						goto IL_004d;
						IL_004d:
						num2 = 7;
						delim = "";
						goto IL_0056;
						IL_0056:
						num2 = 8;
						num5 = 0;
						goto IL_005b;
						IL_005b:
						num2 = 9;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						goto IL_0072;
						IL_0072:
						num2 = 10;
						num6 = 0;
						goto IL_0078;
						IL_0078:
						num2 = 11;
						right = "DS";
						goto IL_0082;
						IL_0082:
						num2 = 12;
						text2 = Strings.UCase(Globals_Renamed.currdatatypetmp);
						goto IL_0090;
						IL_0090:
						num2 = 13;
						text3 = TxtBox1.Text;
						goto IL_00a0;
						IL_00a0:
						num2 = 14;
						text = TxtBox2.Text;
						goto IL_00b0;
						IL_00b0:
						num2 = 15;
						if (General_Procedures.IsDateDT(text2))
						{
							goto IL_00c3;
						}
						goto IL_058e;
						IL_00c3:
						num2 = 16;
						TxtBox1.Items.Clear();
						goto IL_00d7;
						IL_00d7:
						num2 = 17;
						TxtBox2.Items.Clear();
						goto IL_00eb;
						IL_00eb:
						num2 = 18;
						if (Operators.CompareString(text2, "D", TextCompare: false) == 0)
						{
							goto IL_0106;
						}
						goto IL_0291;
						IL_0106:
						num2 = 19;
						if ((Operators.CompareString(ll_DBType, "0", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "1", TextCompare: false) == 0))
						{
							goto IL_0138;
						}
						goto IL_019e;
						IL_0138:
						num2 = 20;
						TxtBox1.Items.Add("@START_OF_WW@(YYYYWW)");
						goto IL_0151;
						IL_0151:
						num2 = 21;
						TxtBox1.Items.Add("@END_OF_WW@(YYYYWW)");
						goto IL_016a;
						IL_016a:
						num2 = 22;
						TxtBox2.Items.Add("@START_OF_WW@(YYYYWW)");
						goto IL_0183;
						IL_0183:
						num2 = 23;
						TxtBox2.Items.Add("@END_OF_WW@(YYYYWW)");
						goto IL_019e;
						IL_019e:
						num2 = 25;
						if (((double)ll_ObjectType == Conversions.ToDouble("1")) | (Operators.CompareString(ll_DbNameType, "SQLite", TextCompare: false) == 0) | (Operators.CompareString(text2, "H", TextCompare: false) == 0))
						{
							goto IL_01df;
						}
						goto IL_01ed;
						IL_01df:
						num2 = 26;
						right = "DQ";
						goto IL_01f8;
						IL_01ed:
						num2 = 28;
						right = "DO";
						goto IL_01f8;
						IL_01f8:
						num2 = 30;
						gNoOperators = Globals_Renamed.gNoOperators;
						num5 = 0;
						goto IL_0282;
						IL_0282:
						if (num5 <= gNoOperators)
						{
							goto IL_0207;
						}
						goto IL_056a;
						IL_0207:
						num2 = 31;
						if (Operators.CompareString(Globals_Renamed.gOperators[num5].DB_Type, right, TextCompare: false) == 0)
						{
							goto IL_022c;
						}
						goto IL_0278;
						IL_022c:
						num2 = 32;
						TxtBox1.Items.Add(Globals_Renamed.gOperators[num5].Name);
						goto IL_0251;
						IL_0251:
						num2 = 33;
						TxtBox2.Items.Add(Globals_Renamed.gOperators[num5].Name);
						goto IL_0278;
						IL_0278:
						num2 = 35;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0282;
						IL_0291:
						num2 = 37;
						if (General_Procedures.IsDateDT(text2, 1))
						{
							goto IL_02a4;
						}
						goto IL_056a;
						IL_02a4:
						num2 = 38;
						if (Operators.CompareString(text2, "G", TextCompare: false) == 0)
						{
							goto IL_02bc;
						}
						goto IL_02cc;
						IL_02bc:
						num2 = 39;
						right = "DT";
						goto IL_04d6;
						IL_02cc:
						num2 = 41;
						if (Operators.CompareString(text2, "H", TextCompare: false) == 0)
						{
							goto IL_02e4;
						}
						goto IL_02f4;
						IL_02e4:
						num2 = 42;
						right = "DQ";
						goto IL_04d6;
						IL_02f4:
						num2 = 44;
						if (Operators.CompareString(text2, "S", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.currhelptmp, "", TextCompare: false) == 0)
						{
							goto IL_0321;
						}
						goto IL_0331;
						IL_0677:
						num2 = 101;
						CmbTxt.Items.Add(DynArray[num5]);
						goto IL_0690;
						IL_0321:
						num2 = 45;
						right = "DN";
						goto IL_04d6;
						IL_0331:
						num2 = 47;
						if (Operators.CompareString(text2, "S", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.currhelptmp, "", TextCompare: false) != 0)
						{
							goto IL_035e;
						}
						goto IL_036e;
						IL_0690:
						num2 = 102;
						num5 = (short)unchecked(num5 + 1);
						goto IL_069a;
						IL_035e:
						num2 = 48;
						right = "DN2";
						goto IL_04d6;
						IL_036e:
						num2 = 50;
						if (Operators.CompareString(text2, "U", TextCompare: false) == 0)
						{
							goto IL_0386;
						}
						goto IL_0396;
						IL_0386:
						num2 = 51;
						right = "DU";
						goto IL_04d6;
						IL_0396:
						num2 = 53;
						if (Operators.CompareString(text2, "M", TextCompare: false) == 0)
						{
							goto IL_03ae;
						}
						goto IL_03be;
						IL_03ae:
						num2 = 54;
						right = "DM";
						goto IL_04d6;
						IL_03be:
						num2 = 56;
						if (Operators.CompareString(text2, "O", TextCompare: false) == 0)
						{
							goto IL_03d6;
						}
						goto IL_03e6;
						IL_03d6:
						num2 = 57;
						right = "DM2";
						goto IL_04d6;
						IL_03e6:
						num2 = 59;
						if (Operators.CompareString(text2, "L", TextCompare: false) == 0)
						{
							goto IL_03fe;
						}
						goto IL_040e;
						IL_03fe:
						num2 = 60;
						right = "DL";
						goto IL_04d6;
						IL_040e:
						num2 = 62;
						if (Operators.CompareString(text2, "P", TextCompare: false) == 0)
						{
							goto IL_0426;
						}
						goto IL_0436;
						IL_0426:
						num2 = 63;
						right = "DP";
						goto IL_04d6;
						IL_0436:
						num2 = 65;
						if (Operators.CompareString(text2, "J", TextCompare: false) == 0)
						{
							goto IL_044e;
						}
						goto IL_045b;
						IL_044e:
						num2 = 66;
						right = "DJ";
						goto IL_04d6;
						IL_045b:
						num2 = 68;
						if (Operators.CompareString(text2, "K", TextCompare: false) == 0)
						{
							goto IL_0473;
						}
						goto IL_0480;
						IL_0473:
						num2 = 69;
						right = "DK";
						goto IL_04d6;
						IL_0480:
						num2 = 71;
						if (Operators.CompareString(text2, "V", TextCompare: false) == 0)
						{
							goto IL_0498;
						}
						goto IL_04a5;
						IL_0498:
						num2 = 72;
						right = "DV";
						goto IL_04d6;
						IL_04a5:
						num2 = 74;
						if (Operators.CompareString(text2, "W", TextCompare: false) == 0)
						{
							goto IL_04bd;
						}
						goto IL_04cb;
						IL_04bd:
						num2 = 75;
						right = "DW";
						goto IL_04d6;
						IL_04cb:
						num2 = 77;
						right = "DS";
						goto IL_04d6;
						IL_04d6:
						num2 = 79;
						gNoOperators2 = Globals_Renamed.gNoOperators;
						num5 = 0;
						goto IL_0560;
						IL_0560:
						if (num5 <= gNoOperators2)
						{
							goto IL_04e5;
						}
						goto IL_056a;
						IL_04e5:
						num2 = 80;
						if (Operators.CompareString(Globals_Renamed.gOperators[num5].DB_Type, right, TextCompare: false) == 0)
						{
							goto IL_050a;
						}
						goto IL_0556;
						IL_050a:
						num2 = 81;
						TxtBox1.Items.Add(Globals_Renamed.gOperators[num5].Name);
						goto IL_052f;
						IL_052f:
						num2 = 82;
						TxtBox2.Items.Add(Globals_Renamed.gOperators[num5].Name);
						goto IL_0556;
						IL_0556:
						num2 = 84;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0560;
						IL_056a:
						num2 = 86;
						TxtBox1.Text = text3;
						goto IL_057b;
						IL_057b:
						num2 = 87;
						TxtBox2.Text = text;
						goto IL_058e;
						IL_058e:
						num2 = 89;
						if (Operators.CompareString(Strings.Mid(Globals_Renamed.currhelptmp, 1, 1), "~", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_05b4;
						IL_05b4:
						num2 = 90;
						TxtBox1.Items.Clear();
						goto IL_05c8;
						IL_05c8:
						num2 = 91;
						TxtBox2.Items.Clear();
						goto IL_05dc;
						IL_05dc:
						num2 = 92;
						CmbTxt.Items.Clear();
						goto IL_05f0;
						IL_05f0:
						num2 = 93;
						delim = "/";
						goto IL_05fa;
						IL_05fa:
						num2 = 94;
						if (General_Procedures.IsDateDT(text2))
						{
							goto IL_060a;
						}
						goto IL_0614;
						IL_060a:
						num2 = 95;
						delim = "//";
						goto IL_0614;
						IL_0614:
						num2 = 96;
						num6 = General_Procedures.ParseAndFillArray(Globals_Renamed.currhelptmp, delim, ref DynArray);
						goto IL_0627;
						IL_0627:
						num2 = 97;
						DynArray[1] = Strings.Mid(DynArray[1], 2);
						goto IL_0638;
						IL_0638:
						num2 = 98;
						num7 = (short)num6;
						num5 = 1;
						goto IL_069a;
						IL_069a:
						if (num5 <= num7)
						{
							goto IL_0645;
						}
						goto IL_06a0;
						IL_06a0:
						num2 = 103;
						TxtBox1.Text = text3;
						goto IL_06b1;
						end_IL_0001_2:
						break;
					}
					num2 = 107;
					DynArray = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2207;
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

	public void Load_Temp_Table()
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmSQLFilter - Load_Temp_Table";
						short num3 = 0;
						short num4 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						short num5 = 0;
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						int num6 = 0;
						short num7 = 0;
						short num8 = 0;
						string text9 = "";
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Load_Tree_Alias_To_Array("A", ref g_NoAlias, ref g_AliasArray, Do_Valid_Inc: false);
						text9 = TxtBox1.Text;
						short num9 = g_NoAlias;
						for (num3 = 0; num3 <= num9; num3 = (short)unchecked(num3 + 1))
						{
							if ((Operators.CompareString(Strings.Mid(g_AliasArray[num3].Alias_Renamed, 1, 1), "U", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(g_AliasArray[num3].Alias_Renamed), "ALL", TextCompare: false) != 0))
							{
								if (Operators.CompareString(Strings.UCase(g_AliasArray[num3].Alias_Renamed), Strings.UCase(Globals_Renamed.currDataAny), TextCompare: false) == 0)
								{
									break;
								}
								text = General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[num3].Alias_Renamed);
								text3 = "CSV:" + g_AliasArray[num3].Alias_Renamed;
								text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", text3));
								num7 = (short)Strings.InStrRev(text4, ".tab");
								if (num7 != 0)
								{
									text4 = Strings.Mid(text4, 1, num7 - 1);
								}
								if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) == 0)
								{
									text4 = "";
								}
								num8++;
								if (num8 == 1)
								{
									TxtBox1.Items.Clear();
								}
								TxtBox1.Items.Add(text4 + " {" + text + "}");
							}
						}
						if (num8 >= 1)
						{
							TxtBox1.Text = text9;
						}
						Array.Clear(Globals_Renamed.g_QColumns, 0, Globals_Renamed.g_QColumns.Length);
						Globals_Renamed.g_NoQColumns = -1;
						num4 = -1;
						if (num8 > 0)
						{
							num5 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
							short num10 = num5;
							for (num3 = 0; num3 <= num10; num3 = (short)unchecked(num3 + 1))
							{
								text6 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value));
								if ((Operators.CompareString(("  " + text6).Substring(("  " + text6).Length - 2), ".*", TextCompare: false) != 0 || Strings.InStr(Strings.LCase(text6), "regex") != 0) && !BuildForm.IsColPattern(text6))
								{
									text5 = Strings.LCase(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[0].Value)));
									if ((Operators.CompareString(Strings.Mid(Strings.LCase(text5), 1, 2), "f(", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value)), "", TextCompare: false) != 0))
									{
										text5 = Strings.LCase(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value)));
									}
									object[] array;
									DataGridViewCell dataGridViewCell;
									bool[] array2;
									object obj = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[2]).Value }, null, null, array2 = new bool[1] { true });
									if (array2[0])
									{
										dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
									}
									text7 = Conversions.ToString(obj);
									object obj2 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[5]).Value }, null, null, array2 = new bool[1] { true });
									if (array2[0])
									{
										dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
									}
									text8 = Conversions.ToString(obj2);
									if (Operators.CompareString(text8, "STACK", TextCompare: false) == 0)
									{
										num6++;
									}
									if (Operators.CompareString(text5, "", TextCompare: false) != 0 && Operators.CompareString(text7, "N", TextCompare: false) != 0 && Operators.CompareString(text7, "N:S", TextCompare: false) != 0 && Operators.CompareString(text7, "--", TextCompare: false) != 0)
									{
										if (Operators.CompareString(text8, "STACK", TextCompare: false) == 0 && num6 == 1)
										{
											num4++;
											Globals_Renamed.g_QColumns[num4].ColAlias = General_Procedures.Strip_Alias(1, text6);
											text = General_Procedures.Get_Obj_Alias("T->F", Globals_Renamed.g_QColumns[num4].ColAlias);
											num7 = (short)Strings.InStr(text, " ");
											if (num7 != 0)
											{
												text = Strings.Trim(Strings.Mid(text, 1, num7 - 1));
											}
											Globals_Renamed.g_QColumns[num4].Header = text + "->_Data_";
											Globals_Renamed.g_QColumns[num4].ColID = Globals_Renamed.g_QColumns[num4].ColAlias + "->_Data_";
											num4++;
											Globals_Renamed.g_QColumns[num4].ColAlias = General_Procedures.Strip_Alias(1, text6);
											Globals_Renamed.g_QColumns[num4].Header = text + "->_Label_";
											Globals_Renamed.g_QColumns[num4].ColID = Globals_Renamed.g_QColumns[num4].ColAlias + "->_Label_";
										}
										else if (Operators.CompareString(text8, "STACK", TextCompare: false) != 0)
										{
											num4++;
											Globals_Renamed.g_QColumns[num4].Header = text5;
											Globals_Renamed.g_QColumns[num4].ColAlias = General_Procedures.Strip_Alias(1, text6);
											text = General_Procedures.Get_Obj_Alias("T->F", Globals_Renamed.g_QColumns[num4].ColAlias);
											num7 = (short)Strings.InStr(text, " ");
											if (num7 != 0)
											{
												text = Strings.Trim(Strings.Mid(text, 1, num7 - 1));
											}
											Globals_Renamed.g_QColumns[num4].Header = text + "->" + Globals_Renamed.g_QColumns[num4].Header;
											Globals_Renamed.g_QColumns[num4].ColID = Globals_Renamed.g_QColumns[num4].ColAlias + "->{" + Strings.LCase(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[12].Value))) + "}";
										}
									}
								}
							}
						}
						Globals_Renamed.g_NoQColumns = num4;
						goto end_IL_0001;
					}
					case 2197:
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
					goto IL_08cb;
				}
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 2197;
				continue;
			}
			break;
			IL_08cb:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Load_Temp_Table_Columns()
	{
		string text = "FrmSQLFilter - Load_Temp_table_Columns";
		short num = 0;
		string text2 = "";
		bool flag = false;
		string text3 = "";
		string text4 = "";
		bool flag2 = false;
		text2 = General_Procedures.Get_Obj_Alias("F->T", General_Procedures.Get_Node_Value(TxtBox1.Text));
		if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "IN TEMP(*", CompareMethod.Binary))
		{
			flag2 = true;
		}
		if (flag2)
		{
			CmbTxt.Items.Clear();
			CmbTxt.Items.Add("");
		}
		else
		{
			text3 = TxtBox2.Text;
		}
		flag = true;
		short g_NoQColumns = Globals_Renamed.g_NoQColumns;
		checked
		{
			for (num = 0; num <= g_NoQColumns; num = (short)unchecked(num + 1))
			{
				if (Operators.CompareString(text2, Globals_Renamed.g_QColumns[num].ColAlias, TextCompare: false) == 0)
				{
					if (flag)
					{
						TxtBox2.Items.Clear();
					}
					flag = false;
					text4 = Globals_Renamed.g_QColumns[num].Header;
					if (flag2)
					{
						CmbTxt.Items.Add(text4);
					}
					else
					{
						TxtBox2.Items.Add(text4);
					}
				}
			}
			if (!flag2)
			{
				TxtBox2.Text = text3;
			}
		}
	}

	public void Swap_lblBox()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int top = default(int);
		int top2 = default(int);
		int top3 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int top4 = default(int);
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
				case 629:
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
							goto IL_0014;
						case 5:
							goto IL_0019;
						case 6:
							goto IL_001e;
						case 7:
							goto IL_0023;
						case 8:
							goto IL_0028;
						case 9:
							goto IL_0036;
						case 10:
							goto IL_0046;
						case 11:
							goto IL_0054;
						case 12:
							goto IL_005a;
						case 14:
							goto IL_0065;
						case 15:
							goto IL_006c;
						case 13:
						case 16:
						case 17:
							goto IL_0073;
						case 18:
							goto IL_0082;
						case 19:
							goto IL_0092;
						case 20:
							goto IL_00a0;
						case 21:
							goto IL_00a6;
						case 23:
							goto IL_00b1;
						case 24:
							goto IL_00b8;
						case 22:
						case 25:
						case 26:
							goto IL_00bf;
						case 27:
							goto IL_00d6;
						case 28:
							goto IL_00e7;
						case 29:
							goto IL_00f8;
						case 30:
							goto IL_0109;
						case 31:
							goto IL_011a;
						case 32:
							goto IL_012b;
						case 34:
							goto IL_0140;
						case 35:
							goto IL_0151;
						case 36:
							goto IL_0162;
						case 37:
							goto IL_0173;
						case 38:
							goto IL_0184;
						case 39:
							goto IL_0195;
						case 40:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 33:
						case 41:
						case 42:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0173:
					num2 = 37;
					CmdCal1.Top = top;
					goto IL_0184;
					IL_0184:
					num2 = 38;
					TxtBox1.Top = top;
					goto IL_0195;
					IL_0162:
					num2 = 36;
					lblbox2.Top = top2;
					goto IL_0173;
					IL_0195:
					num2 = 39;
					lblbox1.Top = top3;
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					top = 0;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					top4 = 0;
					goto IL_001e;
					IL_001e:
					num2 = 6;
					top3 = 0;
					goto IL_0023;
					IL_0023:
					num2 = 7;
					top2 = 0;
					goto IL_0028;
					IL_0028:
					num2 = 8;
					num5 = TxtBox1.Top;
					goto IL_0036;
					IL_0036:
					num2 = 9;
					num6 = TxtBox2.Top;
					goto IL_0046;
					IL_0046:
					num2 = 10;
					if (num5 < num6)
					{
						goto IL_0054;
					}
					goto IL_0065;
					IL_0054:
					num2 = 11;
					top = num5;
					goto IL_005a;
					IL_005a:
					num2 = 12;
					top4 = num6;
					goto IL_0073;
					IL_0065:
					num2 = 14;
					top = num6;
					goto IL_006c;
					IL_006c:
					num2 = 15;
					top4 = num5;
					goto IL_0073;
					IL_0073:
					num2 = 17;
					num5 = lblbox1.Top;
					goto IL_0082;
					IL_0082:
					num2 = 18;
					num6 = lblbox2.Top;
					goto IL_0092;
					IL_0092:
					num2 = 19;
					if (num5 < num6)
					{
						goto IL_00a0;
					}
					goto IL_00b1;
					IL_00a0:
					num2 = 20;
					top3 = num5;
					goto IL_00a6;
					IL_00a6:
					num2 = 21;
					top2 = num6;
					goto IL_00bf;
					IL_00b1:
					num2 = 23;
					top3 = num6;
					goto IL_00b8;
					IL_00b8:
					num2 = 24;
					top2 = num5;
					goto IL_00bf;
					IL_00bf:
					num2 = 26;
					if (ChkPrompt.CheckState == CheckState.Checked)
					{
						goto IL_00d6;
					}
					goto IL_0140;
					IL_00d6:
					num2 = 27;
					TxtBox2.Top = top;
					goto IL_00e7;
					IL_00e7:
					num2 = 28;
					CmdCal2.Top = top;
					goto IL_00f8;
					IL_00f8:
					num2 = 29;
					lblbox2.Top = top3;
					goto IL_0109;
					IL_0109:
					num2 = 30;
					TxtBox1.Top = top4;
					goto IL_011a;
					IL_011a:
					num2 = 31;
					CmdCal1.Top = top4;
					goto IL_012b;
					IL_012b:
					num2 = 32;
					lblbox1.Top = top2;
					goto end_IL_0001_3;
					IL_0140:
					num2 = 34;
					TxtBox2.Top = top4;
					goto IL_0151;
					IL_0151:
					num2 = 35;
					CmdCal2.Top = top4;
					goto IL_0162;
					end_IL_0001_2:
					break;
				}
				num2 = 40;
				GrdList.Top = top;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 629;
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

	private string Assign_Row_Filter_Value()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string result = default(string);
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
						errsource = "frmSQLFilter - Assign_Row_Filter_Value";
						string text = "";
						short num3 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						short num4 = 0;
						int num5 = 0;
						if (((Operators.CompareString(Globals_Renamed.currdatatypetmp, "C", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currdatatypetmp, "S", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currdatatypetmp, "I", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currdatatypetmp, "Q", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currdatatypetmp, "X", TextCompare: false) == 0) | General_Procedures.IsDateDT(Globals_Renamed.currdatatypetmp, 1)) & ((Strings.InStr(Strings.LCase(Combo1.Text), " file") == 0) & (Strings.InStr(Strings.LCase(Combo1.Text), " group") == 0) & (Strings.InStr(Strings.LCase(Combo1.Text), "temp") == 0)))
						{
							text2 = "'";
							text3 = "'";
						}
						else
						{
							text2 = "";
							text3 = "";
						}
						text = "";
						short num6 = (short)(GrdList.RowCount - 1);
						for (num3 = 0; num3 <= num6; num3 = (short)unchecked(num3 + 1))
						{
							if (!Operators.ConditionalCompareObjectNotEqual(GrdList.Rows[num3].Cells[0].Value, "", TextCompare: false))
							{
								continue;
							}
							int num7;
							if (General_Procedures.IsDateDT(Globals_Renamed.currdatatypetmp, 1))
							{
								Type typeFromHandle = typeof(Strings);
								DataGridViewCell dataGridViewCell;
								object[] obj = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value };
								object[] array = obj;
								bool[] obj2 = new bool[1] { true };
								bool[] array2 = obj2;
								object source = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
								if (array2[0])
								{
									dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
								}
								if (!Conversions.ToBoolean(LikeOperator.LikeObject(source, "*DATE*", CompareMethod.Binary)))
								{
									Type typeFromHandle2 = typeof(Strings);
									object[] obj3 = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value };
									array = obj3;
									bool[] obj4 = new bool[1] { true };
									array2 = obj4;
									object source2 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj3, null, null, obj4);
									if (array2[0])
									{
										dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
									}
									if (!Conversions.ToBoolean(LikeOperator.LikeObject(source2, "*SPF_FN$LWW*", CompareMethod.Binary)))
									{
										Type typeFromHandle3 = typeof(Strings);
										object[] obj5 = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value };
										array = obj5;
										bool[] obj6 = new bool[1] { true };
										array2 = obj6;
										object source3 = NewLateBinding.LateGet(null, typeFromHandle3, "UCase", obj5, null, null, obj6);
										if (array2[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
										}
										if (!Conversions.ToBoolean(LikeOperator.LikeObject(source3, "*TIMESTAMP*", CompareMethod.Binary)))
										{
											Type typeFromHandle4 = typeof(Strings);
											object[] obj7 = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value };
											array = obj7;
											bool[] obj8 = new bool[1] { true };
											array2 = obj8;
											object source4 = NewLateBinding.LateGet(null, typeFromHandle4, "UCase", obj7, null, null, obj8);
											if (array2[0])
											{
												dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
											}
											if (!Conversions.ToBoolean(LikeOperator.LikeObject(source4, "*ADD_*", CompareMethod.Binary)))
											{
												Type typeFromHandle5 = typeof(Strings);
												object[] obj9 = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value };
												array = obj9;
												bool[] obj10 = new bool[1] { true };
												array2 = obj10;
												object source5 = NewLateBinding.LateGet(null, typeFromHandle5, "UCase", obj9, null, null, obj10);
												if (array2[0])
												{
													dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
												}
												if (!Conversions.ToBoolean(LikeOperator.LikeObject(source5, "*NOW()*", CompareMethod.Binary)))
												{
													object source6 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (dataGridViewCell = GrdList.Rows[num3].Cells[0]).Value }, null, null, array2 = new bool[1] { true });
													if (array2[0])
													{
														dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
													}
													num7 = (Conversions.ToBoolean(LikeOperator.LikeObject(source6, "*@SPF-FOR-LOOP*", CompareMethod.Binary)) ? 1 : 0);
													goto IL_04be;
												}
											}
										}
									}
								}
								num7 = 1;
							}
							else
							{
								num7 = 0;
							}
							goto IL_04be;
							IL_04be:
							if (Conversions.ToBoolean(unchecked((byte)num7) != 0))
							{
								text2 = "";
								text3 = "";
							}
							if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*between", CompareMethod.Binary))
							{
								GrdList.Rows[num3].Cells[0].Value = text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3;
								continue;
							}
							if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*like", CompareMethod.Binary))
							{
								text4 = General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest);
								if (Operators.CompareString(Strings.Trim(TxtEscape.Text), "", TextCompare: false) == 0 && Strings.InStr(text4, "%") == 0 && Strings.InStr(text4, "_") == 0 && !LikeOperator.LikeString(text4, "*<<<*>>>*", CompareMethod.Binary))
								{
									text4 += "%";
								}
								text = text2 + text4 + text3;
								if (Operators.CompareString(Strings.Trim(TxtEscape.Text), "", TextCompare: false) != 0)
								{
									text = text + " ESCAPE '" + Strings.Trim(TxtEscape.Text) + "'";
								}
								continue;
							}
							if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*null", CompareMethod.Binary))
							{
								text = "";
								continue;
							}
							num5++;
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								if (gSpecialTest == 0 || gSpecialTest == 3 || gSpecialTest == 4 || gSpecialTest == 5 || gSpecialTest == 6 || gSpecialTest == 7 || LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*in file", CompareMethod.Binary))
								{
									text = text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3;
									continue;
								}
								num4 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), ",");
								if (num4 == 0)
								{
									GrdList.Rows[num3].Cells[0].Value = Operators.ConcatenateObject(GrdList.Rows[num3].Cells[0].Value, ",");
									num4 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), ",");
								}
								text = "(" + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), 1, num4 - 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3 + "," + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), num4 + 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3 + ")";
							}
							else if (gSpecialTest == 0 || gSpecialTest == 3 || gSpecialTest == 4 || gSpecialTest == 5 || gSpecialTest == 6 || gSpecialTest == 7 || LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*in file", CompareMethod.Binary))
							{
								text = ((Operators.CompareString(Strings.LCase(Combo1.Text), "in", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Combo1.Text), "like list", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Combo1.Text), "not in", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Combo1.Text), "(+) in", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*regex list", CompareMethod.Binary)) ? (text + "," + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3) : (text + "</comma\\>" + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3));
							}
							else
							{
								num4 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), ",");
								if (num4 == 0)
								{
									GrdList.Rows[num3].Cells[0].Value = Operators.ConcatenateObject(GrdList.Rows[num3].Cells[0].Value, ",");
									num4 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), ",");
								}
								text = text + ",(" + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), 1, num4 - 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3 + "," + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value), num4 + 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + text3 + ")";
							}
						}
						if (num5 == 1 && Operators.CompareString(Strings.LCase(Combo1.Text), "in", TextCompare: false) == 0 && (gSpecialTest == 4 || gSpecialTest == 5 || gSpecialTest == 6 || gSpecialTest == 7))
						{
							text += "</comma\\>";
						}
						result = text;
						goto end_IL_0001;
					}
					case 3316:
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
					goto IL_0d2a;
				}
				end_IL_0001:;
			}
			catch (object obj11) when (obj11 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj11);
				try0001_dispatch = 3316;
				continue;
			}
			break;
			IL_0d2a:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private void ChkPrompt_CheckStateChanged(object eventSender, EventArgs eventArgs)
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
				case 452:
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
							goto IL_0022;
						case 4:
							goto IL_0031;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_0052;
						case 7:
							goto IL_0065;
						case 8:
							goto IL_0079;
						case 9:
						case 10:
							goto IL_008f;
						case 12:
							goto IL_00aa;
						case 13:
							goto IL_00ba;
						case 14:
							goto IL_00c9;
						case 15:
							goto IL_00dd;
						case 16:
							goto IL_00f1;
						case 17:
							goto IL_0106;
						case 18:
						case 19:
							goto IL_011d;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
						case 21:
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00f1:
					num2 = 16;
					TxtBox1.Text = ll_Last1;
					goto IL_0106;
					IL_0106:
					num2 = 17;
					TxtBox2.Text = ll_Last2;
					goto IL_011d;
					IL_00dd:
					num2 = 15;
					ll_PromptDef = TxtBox1.Text;
					goto IL_00f1;
					IL_011d:
					num2 = 19;
					Set_Row_Filter_Controls("SET", Combo1.Text);
					break;
					IL_000b:
					num2 = 2;
					if (ChkPrompt.CheckState == CheckState.Checked)
					{
						goto IL_0022;
					}
					goto IL_00aa;
					IL_0022:
					num2 = 3;
					chkUp.CheckState = CheckState.Unchecked;
					goto IL_0031;
					IL_0031:
					num2 = 4;
					if (ll_IsLoaded)
					{
						goto IL_003f;
					}
					goto IL_008f;
					IL_003f:
					num2 = 5;
					ll_Last1 = TxtBox1.Text;
					goto IL_0052;
					IL_0052:
					num2 = 6;
					ll_Last2 = TxtBox2.Text;
					goto IL_0065;
					IL_0065:
					num2 = 7;
					TxtBox1.Text = ll_PromptDef;
					goto IL_0079;
					IL_0079:
					num2 = 8;
					TxtBox2.Text = ll_PromptTxt;
					goto IL_008f;
					IL_008f:
					num2 = 10;
					Set_Row_Filter_Controls("CLEAR", "");
					goto end_IL_0001_3;
					IL_00aa:
					num2 = 12;
					chkUp.CheckState = CheckState.Checked;
					goto IL_00ba;
					IL_00ba:
					num2 = 13;
					if (ll_IsLoaded)
					{
						goto IL_00c9;
					}
					goto IL_011d;
					IL_00c9:
					num2 = 14;
					ll_PromptTxt = TxtBox2.Text;
					goto IL_00dd;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(Combo1.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 452;
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

	private void cmbcolIG_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
				case 58:
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
				ComboBox comboBox = cmbColIG;
				BuildForm.HG_cmbcolIG_Clk(ref comboBox);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 58;
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

	private void cmbcolIG_DropDown(object eventSender, EventArgs eventArgs)
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
				case 68:
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
				ComboBox comboBox = cmbColIG;
				ComboBox TxtBox = TxtBox1;
				BuildForm.HG_cmbcolIG_DD(ref comboBox, ref TxtBox);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 68;
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

	private void cmdcal1_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		bool flag = default(bool);
		string text3 = default(string);
		string ll_DisplayName = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string myHlpQ;
				string myDT;
				string myOpr;
				ComboBox TxtBox;
				ComboBox comboBox;
				Button CmdLoadGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 523:
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
							goto IL_0046;
						case 7:
							goto IL_0078;
						case 8:
							goto IL_008e;
						case 9:
							goto IL_00a6;
						case 10:
							goto IL_00ab;
						case 11:
						case 12:
						case 13:
							goto IL_00c1;
						case 14:
							goto IL_00fc;
						case 15:
							goto IL_0106;
						case 17:
							goto IL_0114;
						case 18:
							goto IL_011e;
						case 16:
						case 19:
						case 20:
							goto IL_0129;
						case 21:
							goto IL_0133;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00fc:
					num2 = 14;
					text = "S2";
					goto IL_0106;
					IL_0106:
					num2 = 15;
					text2 = "";
					goto IL_0129;
					IL_0133:
					num2 = 21;
					myHlpQ = text2;
					myDT = text;
					myOpr = Combo1.Text;
					TxtBox = TxtBox1;
					comboBox = cmbColIG;
					CmdLoadGrid = cmdLoadGrid;
					BuildForm.HG_Fltr_Hlp_2(myHlpQ, myDT, myOpr, ref TxtBox, ref comboBox, ref CmdLoadGrid, 1, Globals_Renamed.currDataAny, ll_ObjectType);
					cmdLoadGrid = CmdLoadGrid;
					cmbColIG = comboBox;
					TxtBox1 = TxtBox;
					break;
					IL_0114:
					num2 = 17;
					text = Globals_Renamed.currdatatypetmp;
					goto IL_011e;
					IL_000b:
					num2 = 2;
					flag = true;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text3 = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					ll_DisplayName = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					if (ll_ObjectType == 0 || ll_ObjectType == 2 || ll_ObjectType == 4)
					{
						goto IL_0046;
					}
					goto IL_00c1;
					IL_011e:
					num2 = 18;
					text2 = Globals_Renamed.currhelptmp;
					goto IL_0129;
					IL_0129:
					num2 = 20;
					if (!flag)
					{
						break;
					}
					goto IL_0133;
					IL_0046:
					num2 = 6;
					ll_DisplayName = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "VIEW:" + Globals_Renamed.currDataAny);
					goto IL_0078;
					IL_0078:
					num2 = 7;
					text3 = BuildSQL.Check_XEUS_View(ll_DisplayName, ll_DBType, "");
					goto IL_008e;
					IL_008e:
					num2 = 8;
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						goto IL_00a6;
					}
					goto IL_00c1;
					IL_00a6:
					num2 = 9;
					flag = false;
					goto IL_00ab;
					IL_00ab:
					num2 = 10;
					Interaction.MsgBox(text3, MsgBoxStyle.Exclamation, "Database Node Issue");
					goto IL_00c1;
					IL_00c1:
					num2 = 13;
					if (Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "S", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(Globals_Renamed.currhelptmp), "YYYY-MM-DD HH24:MI:SS", TextCompare: false) == 0)
					{
						goto IL_00fc;
					}
					goto IL_0114;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				Refresh();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 523;
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

	private void cmdcal2_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myDT = default(string);
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
				case 176:
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
							goto IL_0045;
						case 5:
							goto IL_0051;
						case 4:
						case 6:
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "S", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(Globals_Renamed.currhelptmp), "YYYY-MM-DD HH24:MI:SS", TextCompare: false) == 0)
					{
						goto IL_0045;
					}
					goto IL_0051;
					IL_0051:
					num2 = 5;
					myDT = Globals_Renamed.currdatatypetmp;
					break;
					IL_0045:
					num2 = 3;
					myDT = "S2";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				TxtBox2.Text = BuildForm.HG_CmpxpCal(TxtBox2.Text, myDT, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 176;
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

	private void CmdCancel_Click(object eventSender, EventArgs eventArgs)
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
				case 100:
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
							goto IL_0018;
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
					Cursor.Current = Cursors.Default;
					goto IL_0018;
					IL_0018:
					num2 = 3;
					Close();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Refresh();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 100;
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
				mnuClearGrid_Click(mnuClearGrid, new EventArgs());
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

	private void cmdHelp_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Filtering+Data");
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

	private void cmdOK_Click(object eventSender, EventArgs eventArgs)
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
				checked
				{
					string text5;
					string text;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "frmSQLFilter - CmdOK_Click";
						text = "";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						bool flag = false;
						int num7 = 0;
						int num8 = 0;
						text5 = "";
						long num9 = 0L;
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveQuery = 1;
						if (ChkPrompt.CheckState == CheckState.Checked && Operators.CompareString(Strings.Trim(TxtBox2.Text), "", TextCompare: false) == 0)
						{
							Interaction.MsgBox("You must specify a prompt text when prompting for input", MsgBoxStyle.Exclamation, "Missing Prompt Text");
							TxtBox2.Focus();
							TxtBox2.SelectionStart = 0;
							TxtBox2.SelectionLength = 0;
							goto end_IL_0001;
						}
						text5 = Strings.Trim(TxtIncrement.Text);
						if (Operators.CompareString(text5, "", TextCompare: false) == 0 || (!LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*temp*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*group*", CompareMethod.Binary)))
						{
							goto IL_01ee;
						}
						num9 = ((Operators.CompareString(ll_DbNameType, "Mongo", TextCompare: false) != 0) ? 999 : 1000000);
						if (!LikeOperator.LikeString(text5, "*<<<*>>>*", CompareMethod.Binary) && (!Versioned.IsNumeric(text5) || Conversions.ToLong(text5) < 1 || Conversions.ToLong(text5) > num9))
						{
							Interaction.MsgBox("You must specify a numeric number of filter items to process, between 1 and " + Conversions.ToString(num9), MsgBoxStyle.Exclamation, "Invalid Item Process Count");
							TxtIncrement.Focus();
							goto end_IL_0001;
						}
						text5 = "^^^^^" + text5;
						goto IL_01ee;
					}
					case 4932:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								goto end_IL_0001_2;
							}
							break;
						}
						IL_122f:
						if (Operators.CompareString(Globals_Renamed.currkeytypetmp, "Y", TextCompare: false) == 0)
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[7].Value = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ImageKey.Images[3];
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[7].Tag = ".";
						}
						goto IL_12eb;
						IL_01ee:
						if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in temp(bt*", CompareMethod.Binary))
						{
							int num7 = -1;
							short num10 = (short)(GrdList.RowCount - 1);
							for (short num3 = 0; num3 <= num10; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.CompareString(Strings.Trim(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0)
								{
									num7++;
								}
							}
							int num8 = 1;
							if (num8 != num7)
							{
								Interaction.MsgBox("Was expecting two date columns for your In Temp(BT->D|T) filter expression. Please correct.", MsgBoxStyle.Exclamation, "Invalid In Temp Filter");
								goto end_IL_0001;
							}
							short num4 = 0;
							goto IL_0567;
						}
						if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in temp(*)", CompareMethod.Binary))
						{
							if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in temp(*=*)", CompareMethod.Binary))
							{
								Interaction.MsgBox("In Temp(OR=;..) or In Temp(*=*) operators are not supported using the Legacy Engine", MsgBoxStyle.Exclamation, "Invalid Filter");
								goto end_IL_0001;
							}
							int num7 = -1;
							short num11 = (short)(GrdList.RowCount - 1);
							for (short num3 = 0; num3 <= num11; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.CompareString(Strings.Trim(Conversions.ToString(GrdList.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0)
								{
									num7++;
								}
							}
							int num8 = ((gSpecialTest == 4) ? 3 : ((gSpecialTest != 6) ? General_Procedures.Count_Character(Globals_Renamed.currtxttmp, ";") : 2));
							if (num8 != num7)
							{
								Interaction.MsgBox("You have an inconsistency in the number of In Temp filters, as well as columns that make up your filter expression. Please correct.", MsgBoxStyle.Exclamation, "Invalid Filter");
								goto end_IL_0001;
							}
							short num4 = 0;
							goto IL_0567;
						}
						if (!LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*temp", CompareMethod.Binary))
						{
							if (General_Procedures.IsDateDT(Globals_Renamed.currdatatypetmp) && ChkPrompt.CheckState == CheckState.Unchecked)
							{
								short num6 = (short)Strings.InStr(TxtBox1.Text, "==");
								if (num6 != 0)
								{
									TxtBox1.Text = Strings.Mid(TxtBox1.Text, num6 + 2);
								}
								num6 = (short)Strings.InStr(TxtBox2.Text, "==");
								if (num6 != 0)
								{
									TxtBox2.Text = Strings.Mid(TxtBox2.Text, num6 + 2);
								}
							}
							if ((ChkPrompt.CheckState == CheckState.Unchecked) & LikeOperator.LikeString(Strings.LCase(Combo1.Text), "* group*", CompareMethod.Binary) & (Operators.CompareString(Strings.Trim(TxtBox1.Text), "", TextCompare: false) != 0))
							{
								TxtBox1.Text = TxtBox1.Text + text5 + ":" + Strings.Trim(cmbColIG.Text);
							}
						}
						goto IL_0567;
						IL_0c51:
						if ((Operators.CompareString(Strings.LCase(Combo1.Text), "like list", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(Combo1.Text), "like group", TextCompare: false) == 0) && Conversions.ToBoolean(Strings.Trim(Conversions.ToString(Operators.CompareString(TxtEscape.Text, "", TextCompare: false) != 0))))
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[3].Value = Combo1.Text + " [" + Strings.Mid(Strings.Trim(TxtEscape.Text), 1, 1) + "]";
						}
						else if (Operators.CompareString(Strings.LCase(Combo1.Text), "in temp()", TextCompare: false) == 0)
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[3].Value = "In Temp(" + ll_KeepTrueDT + ")";
						}
						else
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[3].Value = Combo1.Text;
						}
						text = Assign_Row_Filter_Value();
						if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*between", CompareMethod.Binary))
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = RuntimeHelpers.GetObjectValue(GrdList.Rows[0].Cells[0].Value);
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = RuntimeHelpers.GetObjectValue(GrdList.Rows[1].Cells[0].Value);
						}
						else if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*temp*", CompareMethod.Binary))
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = RuntimeHelpers.GetObjectValue(GrdList.Rows[0].Cells[0].Value);
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = RuntimeHelpers.GetObjectValue(GrdList.Rows[1].Cells[0].Value);
						}
						else
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = text;
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = "";
						}
						goto IL_122f;
						IL_0567:
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in group(or=*)", CompareMethod.Binary))
						{
							Interaction.MsgBox("In Group(OR=;..) operators are not supported using the Legacy Engine", MsgBoxStyle.Exclamation, "Invalid Filter");
							goto end_IL_0001;
						}
						if ((ChkPrompt.CheckState == CheckState.Unchecked) | LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*null", CompareMethod.Binary))
						{
							if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*temp", CompareMethod.Binary))
							{
								DataGridView MyGrid = GrdList;
								GridModule.Clear_A_Grid(ref MyGrid);
								if ((Operators.CompareString(Strings.Trim(TxtBox1.Text), "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(TxtBox2.Text), "", TextCompare: false) != 0))
								{
									GrdList.Rows[0].Cells[0].Value = TxtBox1.Text + text5 + " : " + TxtBox2.Text;
									GrdList.Rows[1].Cells[0].Value = ll_TTColID;
								}
							}
							else if (LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*between", CompareMethod.Binary))
							{
								DataGridView MyGrid = GrdList;
								GridModule.Clear_A_Grid(ref MyGrid);
								if (Operators.CompareString(TxtBox1.Text, "", TextCompare: false) != 0 && (Operators.CompareString(TxtBox2.Text, "", TextCompare: false) != 0 || LikeOperator.LikeString(Strings.Replace(Strings.Trim(TxtBox1.Text), "'", "", 1, -1, CompareMethod.Text), "<<<*>>>", CompareMethod.Binary)))
								{
									GrdList.Rows[0].Cells[0].Value = TxtBox1.Text;
									GrdList.Rows[1].Cells[0].Value = TxtBox2.Text;
								}
							}
							else if (Operators.CompareString(Strings.LCase(Combo1.Text), "in", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in temp(*)", CompareMethod.Binary) && Operators.CompareString(Strings.LCase(Combo1.Text), "not in", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Combo1.Text), "like list", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(Combo1.Text), "*regex list", CompareMethod.Binary) && Operators.CompareString(Strings.LCase(Combo1.Text), "(+) in", TextCompare: false) != 0)
							{
								DataGridView MyGrid = GrdList;
								GridModule.Clear_A_Grid(ref MyGrid);
								if (Operators.CompareString(TxtBox1.Text, "", TextCompare: false) != 0)
								{
									GrdList.Rows[0].Cells[0].Value = TxtBox1.Text;
								}
							}
							short num5 = 1;
							short num12 = (short)(GrdList.RowCount - 1);
							for (short num3 = 0; num3 <= num12; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.ConditionalCompareObjectNotEqual(GrdList.Rows[num3].Cells[0].Value, "", TextCompare: false))
								{
									num5 = 0;
									break;
								}
							}
							if ((num5 == 1) & (Operators.CompareString(Strings.LCase(Combo1.Text), "is null", TextCompare: false) != 0) & (Operators.CompareString(Strings.LCase(Combo1.Text), "is not null", TextCompare: false) != 0) & (Operators.CompareString(Strings.LCase(Combo1.Text), "(+) is null", TextCompare: false) != 0) & (Operators.CompareString(Strings.LCase(Combo1.Text), "(+) is not null", TextCompare: false) != 0))
							{
								goto IL_12eb;
							}
							if (!LikeOperator.LikeString(Strings.LCase(Combo1.Text), "in temp(*)", CompareMethod.Binary))
							{
								goto IL_0c51;
							}
							if (Operators.CompareString(Strings.Trim(TxtBox1.Text), "", TextCompare: false) == 0)
							{
								goto IL_12eb;
							}
							string text2 = TxtBox1.Text + text5;
							string text3 = " : ";
							ll_TTColID = "";
							bool flag = true;
							short num13 = (short)(GrdList.RowCount - 1);
							for (short num3 = 0; num3 <= num13; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.ConditionalCompareObjectNotEqual(GrdList.Rows[num3].Cells[0].Value, "", TextCompare: false))
								{
									string text4 = Conversions.ToString(GrdList.Rows[num3].Cells[0].Value);
									flag = false;
									short g_NoQColumns = Globals_Renamed.g_NoQColumns;
									for (short num4 = 0; num4 <= g_NoQColumns; num4 = (short)unchecked(num4 + 1))
									{
										if (Operators.CompareString(Strings.LCase(text4), Strings.LCase(Globals_Renamed.g_QColumns[num4].Header), TextCompare: false) == 0)
										{
											ll_TTColID = ll_TTColID + ";" + Globals_Renamed.g_QColumns[num4].ColID;
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										break;
									}
									text2 = text2 + text3 + text4;
									text3 = ";";
								}
							}
							if (!flag)
							{
								Interaction.MsgBox("One of the columns specified in the Grid does not match a Column Header in the Temp Table you are referencing. Please correct!", MsgBoxStyle.Exclamation, "Invalid Column");
								goto end_IL_0001;
							}
							if (Operators.CompareString(ll_TTColID, "", TextCompare: false) != 0)
							{
								ll_TTColID = Strings.Mid(ll_TTColID, 2);
							}
							short num14 = (short)(GrdList.RowCount - 1);
							for (short num3 = 0; num3 <= num14; num3 = (short)unchecked(num3 + 1))
							{
								GrdList.Rows[num3].Cells[0].Value = "";
							}
							GrdList.Rows[0].Cells[0].Value = text2;
							GrdList.Rows[1].Cells[0].Value = ll_TTColID;
							goto IL_0c51;
						}
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[3].Value = Combo1.Text;
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = "&PROMPT&";
						ll_PromptTxt = Strings.Trim(TxtBox2.Text);
						ll_PromptDef = Strings.Trim(TxtBox1.Text);
						if ((Operators.CompareString(ll_PromptTxt, "", TextCompare: false) == 0) & (Operators.CompareString(ll_PromptDef, "", TextCompare: false) == 0))
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = "";
						}
						else
						{
							ll_PromptTxt = General_Procedures.StripQuotesAndDates(ll_PromptTxt, "C", 0);
							if (!LikeOperator.LikeString(Strings.UCase(Strings.Trim(ll_PromptDef)), "*SELECT*FROM*", CompareMethod.Binary))
							{
								ll_PromptDef = General_Procedures.StripQuotesAndDates(ll_PromptDef, "C", 0);
							}
							else
							{
								ll_PromptDef = BuildForm.Replace_Amp_Carat(ll_PromptDef, "E");
							}
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = ll_PromptTxt + "^" + ll_PromptDef;
						}
						goto IL_122f;
						IL_12eb:
						Cursor.Current = Cursors.Default;
						Close();
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Refresh();
						goto end_IL_0001_2;
					}
					goto IL_137a;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4932;
				continue;
			}
			break;
			IL_137a:
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

	private void cmdPaste_Click(object eventSender, EventArgs eventArgs)
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
				mnuPasteGrid_Click(mnuPasteGrid, new EventArgs());
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

	private void Combo1_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
				case 257:
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
							goto IL_002d;
						case 5:
							goto IL_0046;
						case 6:
							goto IL_0055;
						case 7:
							goto IL_0064;
						case 9:
							goto IL_0071;
						case 8:
						case 10:
						case 11:
							goto IL_007c;
						case 12:
							goto IL_0093;
						case 13:
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0071:
					num2 = 9;
					Load_Text_Boxes();
					goto IL_007c;
					IL_007c:
					num2 = 11;
					if (ChkPrompt.CheckState != CheckState.Unchecked)
					{
						break;
					}
					goto IL_0093;
					IL_0064:
					num2 = 7;
					Load_Temp_Table();
					goto IL_007c;
					IL_0093:
					num2 = 12;
					Set_Row_Filter_Controls("SET", text);
					break;
					IL_000b:
					num2 = 2;
					text = Combo1.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(text);
					goto IL_002d;
					IL_002d:
					num2 = 4;
					if (LikeOperator.LikeString(Strings.UCase(text), "*TEMP*", CompareMethod.Binary))
					{
						goto IL_0046;
					}
					goto IL_0071;
					IL_0046:
					num2 = 5;
					ChkPrompt.CheckState = CheckState.Unchecked;
					goto IL_0055;
					IL_0055:
					num2 = 6;
					Set_Row_Filter_Controls("SET", text);
					goto IL_0064;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				set_Button_default(text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 257;
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

	private void Combo1_DropDown(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string operator_Renamed = default(string);
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
				case 79:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					operator_Renamed = Combo1.Text;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(operator_Renamed);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 79;
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

	private void frmSQLfilter_Activated(object eventSender, EventArgs eventArgs)
	{
		if (!ll_IsLoaded)
		{
			ll_IsLoaded = true;
			if (ChkPrompt.CheckState == CheckState.Checked)
			{
				TxtBox2.Focus();
				TxtBox2.SelectionStart = 0;
				TxtBox2.SelectionLength = 0;
			}
		}
	}

	private void frmSQLfilter_Load(object eventSender, EventArgs eventArgs)
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "frmSQLFilter - Form_Load";
						Button MyButton = CmdHelp;
						BuildForm.Set_Btn_Img(ref MyButton, "helpb");
						CmdHelp = MyButton;
						MyButton = CmdClear;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						CmdClear = MyButton;
						MyButton = cmdPaste;
						BuildForm.Set_Btn_Img(ref MyButton, "paste_color");
						cmdPaste = MyButton;
						MyButton = CmdCal1;
						BuildForm.Set_Btn_Img(ref MyButton, "helpy2");
						CmdCal1 = MyButton;
						MyButton = CmdCal2;
						BuildForm.Set_Btn_Img(ref MyButton, "helpy2");
						CmdCal2 = MyButton;
						string text = "";
						short num3 = 0;
						int num4 = 0;
						string text2 = "";
						int num5 = 0;
						string text3 = "";
						ll_IsLoaded = false;
						ll_PromptTxt = "";
						ll_PromptDef = "";
						ll_Last1 = "";
						ll_Last2 = "";
						ll_TTColID = "";
						GrdList.RowCount = 1000;
						GrdList.Columns[0].Width = GrdList.Width;
						if (Operators.CompareString(Globals_Renamed.currDataAny, "All", TextCompare: false) != 0)
						{
							text3 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + Globals_Renamed.currDataAny);
							ll_DbNameType = General_Procedures.Get_Node_Value(text3);
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Database_Node_Plus(ref Globals_Renamed.currDataAny, ref ll_DBType, ref ll_ObjectType);
							string source = Strings.UCase(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "VIEW:" + Globals_Renamed.currDataAny));
							if (Operators.CompareString(ll_DBType, "5", TextCompare: false) == 0 || Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0)
							{
								if (Operators.CompareString(ll_DbNameType, "Oracle", TextCompare: false) != 0 && Operators.CompareString(ll_DbNameType, "Teradata", TextCompare: false) != 0 && Operators.CompareString(ll_DbNameType, "SQLServer", TextCompare: false) != 0 && Operators.CompareString(ll_DbNameType, "IBI-DaaS", TextCompare: false) != 0 && Operators.CompareString(ll_DbNameType, "Mongo", TextCompare: false) != 0)
								{
									AllowIncExtract = false;
								}
								if (Operators.CompareString(ll_DbNameType, "SQLite", TextCompare: false) == 0)
								{
									ll_ObjectType = 1;
								}
							}
							if (LikeOperator.LikeString(source, "*HBASE*", CompareMethod.Binary))
							{
								AllowIncExtract = false;
							}
						}
						else
						{
							ll_DBType = "5";
							ll_ObjectType = 1;
							ll_DbNameType = "SQLite";
							AllowIncExtract = false;
						}
						text3 = "";
						if ((Operators.CompareString(Globals_Renamed.currdatatypetmp, "D", TextCompare: false) == 0) & ((Operators.CompareString(ll_DbNameType, "SQLite", TextCompare: false) == 0) | ((double)ll_ObjectType == Conversions.ToDouble("1"))))
						{
							Globals_Renamed.currdatatypetmp = "H";
						}
						else if ((Operators.CompareString(Globals_Renamed.currdatatypetmp, "T", TextCompare: false) == 0) & ((Operators.CompareString(ll_DbNameType, "Teradata", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0)))
						{
							Globals_Renamed.currdatatypetmp = "G";
						}
						if ((Operators.CompareString(Strings.Mid(Globals_Renamed.currtxttmp, 1, 1), "(", TextCompare: false) == 0) & (Strings.InStr(Globals_Renamed.currtxttmp, ")") != 0) & (Strings.InStr(Globals_Renamed.currtxttmp, "test_name,") != 0) & (Strings.InStr(Globals_Renamed.currtxttmp, "structure_name)") != 0))
						{
							gSpecialTest = 1;
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*test_name_and_structure*", CompareMethod.Binary))
						{
							gSpecialTest = 1;
						}
						else if ((Operators.CompareString(Strings.Mid(Globals_Renamed.currtxttmp, 1, 1), "(", TextCompare: false) == 0) & (Strings.InStr(Globals_Renamed.currtxttmp, ")") != 0) & (Strings.InStr(Globals_Renamed.currtxttmp, "lot") != 0) & (Strings.InStr(Globals_Renamed.currtxttmp, "wafer") != 0) & (Strings.InStr(Globals_Renamed.currtxttmp, ",") != 0))
						{
							gSpecialTest = 2;
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*lot_wafer_key*", CompareMethod.Binary))
						{
							gSpecialTest = 2;
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*midas_visual_id_filteronly", CompareMethod.Binary) || LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*imbigdata_visual_id_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 3;
							if (Operators.CompareString(Globals_Renamed.curroprtmp, "=", TextCompare: false) == 0)
							{
								Globals_Renamed.curroprtmp = "In";
							}
							AllowIncExtract = false;
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*midas_fuse_filteronly", CompareMethod.Binary) || LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*sort_lot_wafer3_die_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 4;
							AllowIncExtract = false;
							if (Operators.CompareString(Globals_Renamed.curroprtmp, "=", TextCompare: false) == 0)
							{
								Globals_Renamed.curroprtmp = "In";
							}
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*sort_lot_wafer3_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 5;
							if (Operators.CompareString(Globals_Renamed.curroprtmp, "=", TextCompare: false) == 0)
							{
								Globals_Renamed.curroprtmp = "In";
							}
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*ctr_testvalue_filteronly", CompareMethod.Binary) | LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*counter_test_filteronly*", CompareMethod.Binary) | LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*str_testvalue_filteronly", CompareMethod.Binary) | LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*raw_testvalue_filteronly", CompareMethod.Binary) | LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*ptr2_testvalue_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 6;
							AllowIncExtract = false;
							ll_NoPrompt = true;
							if (Operators.CompareString(Globals_Renamed.curroprtmp, "=", TextCompare: false) == 0)
							{
								Globals_Renamed.curroprtmp = "In";
							}
						}
						else if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*ptr_fail-pattern_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 7;
							AllowIncExtract = false;
							ll_NoPrompt = true;
						}
						else
						{
							gSpecialTest = 0;
						}
						if (Operators.CompareString(Strings.Mid(Strings.LCase(Globals_Renamed.curroprtmp), 1, 11), "like list [", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(Globals_Renamed.curroprtmp), 1, 12), "like group [", TextCompare: false) == 0)
						{
							if (Operators.CompareString(Strings.Mid(Strings.LCase(Globals_Renamed.curroprtmp), 1, 11), "like list [", TextCompare: false) == 0)
							{
								TxtEscape.Text = Strings.Mid(Globals_Renamed.curroprtmp, 12, 1);
								Globals_Renamed.curroprtmp = "Like List";
							}
							else
							{
								TxtEscape.Text = Strings.Mid(Globals_Renamed.curroprtmp, 13, 1);
								Globals_Renamed.curroprtmp = "Like group";
							}
						}
						bool flag = false;
						Form Form_Name = this;
						BuildForm.Set_Form_Position(ref Form_Name);
						base.Top = (int)Math.Round((double)Screen.PrimaryScreen.Bounds.Height / 2.0 - (double)base.Height / 2.0);
						Label1.Text = Globals_Renamed.currtxttmp;
						if (Label1.Width > chkUp.Left - 17)
						{
							Label1.AutoSize = false;
							Label1.Width = chkUp.Left - 17;
						}
						text2 = Strings.UCase(Globals_Renamed.currdatatypetmp);
						Combo1.Items.Clear();
						if ((gSpecialTest == 1) | (gSpecialTest == 2))
						{
							Combo1.Items.Add("In");
							Combo1.Items.Add("Not In");
							Combo1.Items.Add("In File");
						}
						else if (gSpecialTest == 3)
						{
							Combo1.Items.Add("In");
							Combo1.Items.Add("In Group");
							Combo1.Items.Add("In Temp");
						}
						else if (gSpecialTest == 4)
						{
							Combo1.Items.Add("In");
							Combo1.Items.Add("In Group(=N;=N;=N;=N)");
							Combo1.Items.Add("In Temp(=N;=N;=N;=N)");
						}
						else if (gSpecialTest == 5)
						{
							Combo1.Items.Add("In");
							Combo1.Items.Add("In Group(=N;=N)");
							Combo1.Items.Add("In Temp(=N;=N)");
						}
						else if (gSpecialTest == 6)
						{
							Combo1.Items.Add("In");
						}
						else if (gSpecialTest == 7)
						{
							Combo1.Items.Add("=");
						}
						else if (Operators.CompareString(ll_DbNameType, "iMBigData", TextCompare: false) == 0)
						{
							ll_NoPrompt = true;
							if (Operators.CompareString(Globals_Renamed.currdatatypetmp, "S", TextCompare: false) == 0)
							{
								Combo1.Items.Add("=");
							}
							else
							{
								Combo1.Items.Add("=");
								Combo1.Items.Add("In");
								Combo1.Items.Add("In Group");
								Combo1.Items.Add("In Temp");
							}
						}
						else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*::*filteronly", CompareMethod.Binary))
						{
							Combo1.Items.Add("=");
							ll_NoPrompt = true;
						}
						else if (Operators.CompareString(ll_DbNameType, "Mongo", TextCompare: false) == 0)
						{
							int gNoOperators = Globals_Renamed.gNoOperators;
							for (num4 = 0; num4 <= gNoOperators; num4++)
							{
								if (Operators.CompareString(Strings.Mid(Globals_Renamed.gOperators[num4].DB_Type, 1, 1), "M", TextCompare: false) == 0 && (Operators.CompareString(Globals_Renamed.gOperators[num4].Type, text2, TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.gOperators[num4].Type, "E", TextCompare: false) == 0))
								{
									Combo1.Items.Add(Globals_Renamed.gOperators[num4].Name);
								}
							}
						}
						else
						{
							int gNoOperators2 = Globals_Renamed.gNoOperators;
							for (num4 = 0; num4 <= gNoOperators2; num4++)
							{
								if (((Operators.CompareString(Globals_Renamed.gOperators[num4].DB_Type, "FO", TextCompare: false) == 0 && Operators.CompareString(ll_DbNameType, "Oracle", TextCompare: false) == 0) || Operators.CompareString(Globals_Renamed.gOperators[num4].DB_Type, "FO", TextCompare: false) != 0) && Operators.CompareString(Strings.Mid(Globals_Renamed.gOperators[num4].DB_Type, 1, 1), "F", TextCompare: false) == 0 && ((Operators.CompareString(text2, "E", TextCompare: false) != 0 && (Operators.CompareString(Globals_Renamed.gOperators[num4].Type, text2, TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.gOperators[num4].Type, "A", TextCompare: false) == 0)) || (Operators.CompareString(text2, "E", TextCompare: false) == 0 && Operators.CompareString(text2, Globals_Renamed.gOperators[num4].Type, TextCompare: false) == 0)) && (Operators.CompareString(ll_DbNameType, "Teradata", TextCompare: false) != 0 || Operators.CompareString(Globals_Renamed.gOperators[num4].Name, "!=", TextCompare: false) != 0))
								{
									Combo1.Items.Add(Globals_Renamed.gOperators[num4].Name);
								}
							}
						}
						Combo1.SelectedIndex = 0;
						if (Operators.CompareString(Globals_Renamed.curroprtmp, "", TextCompare: false) != 0)
						{
							num3 = 0;
							int num6 = Combo1.Items.Count - 1;
							for (num4 = 0; num4 <= num6; num4++)
							{
								if (Operators.CompareString(Combo1.Items[num4].ToString(), Globals_Renamed.curroprtmp, TextCompare: false) == 0)
								{
									num3 = 1;
									break;
								}
							}
							if (num3 == 0)
							{
								Combo1.Items.Add(Globals_Renamed.curroprtmp);
							}
							Combo1.SelectedIndex = num4;
							if (Strings.InStr(Globals_Renamed.currvaluetmp, "&PROMPT&") != 0)
							{
								num5 = Strings.InStr(Globals_Renamed.currvaluetmp, "^");
								if (num5 != 0)
								{
									ll_PromptTxt = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 9, num5 - 9));
									ll_PromptDef = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, num5 + 1));
									TxtBox2.Text = ll_PromptTxt;
									TxtBox1.Text = BuildForm.Replace_Amp_Carat(ll_PromptDef, "D");
									Globals_Renamed.currvaluetmp = "&PROMPT&";
								}
								ChkPrompt.CheckState = CheckState.Checked;
							}
							else
							{
								ChkPrompt.CheckState = CheckState.Unchecked;
								if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*like", CompareMethod.Binary))
								{
									num4 = Strings.InStr(Globals_Renamed.currvaluetmp, "' ESCAPE '");
									if (num4 != 0)
									{
										TxtEscape.Text = Strings.Mid(Globals_Renamed.currvaluetmp, num4 + 10, 1);
										Globals_Renamed.currvaluetmp = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num4 - 1);
									}
								}
								if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*temp*", CompareMethod.Binary))
								{
									TextBox txtIncrement;
									string MyIncrement = (txtIncrement = TxtIncrement).Text;
									BuildSQL.Chk_Temp_Inc("T", ref Globals_Renamed.currvaluetmp, ref MyIncrement);
									txtIncrement.Text = MyIncrement;
								}
								else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*group*", CompareMethod.Binary))
								{
									TextBox txtIncrement;
									string MyIncrement = (txtIncrement = TxtIncrement).Text;
									BuildSQL.Chk_Temp_Inc("G", ref Globals_Renamed.currvaluetmp, ref MyIncrement);
									txtIncrement.Text = MyIncrement;
								}
								Load_Row_Filter_Value(Globals_Renamed.currvaluetmp);
								ll_Last1 = TxtBox1.Text;
								ll_Last2 = TxtBox2.Text;
								text3 = Strings.Trim(Label1.Text);
								if (Operators.CompareString(Strings.UCase(General_Procedures.Set_Get_Fn("G", "F", Label1.Text, "")), "N", TextCompare: false) == 0)
								{
									if (Strings.InStr(text3, ".") != 0)
									{
										text3 = General_Procedures.Strip_Column(text3, 1);
									}
									text3 = Strings.UCase(Strings.Mid(text3, 1, 1)) + Strings.Mid(text3, 2);
									ll_PromptTxt = "Value for " + text3;
								}
								else if (Strings.InStr(Label1.Text, ";") == 0)
								{
									Label1.Text = "Expression";
									Label1.AutoSize = true;
									ll_PromptTxt = "Value for Expression";
								}
							}
						}
						Load_Text_Boxes();
						text = Combo1.Text;
						lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(text);
						ChkPrompt_CheckStateChanged(ChkPrompt, new EventArgs());
						set_Button_default(Globals_Renamed.curroprtmp);
						if ((gSpecialTest >= 3) & (gSpecialTest <= 7))
						{
							lbldesc.Text = Globals_Renamed.currprompttmp;
						}
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					case 4450:
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
					goto IL_1198;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4450;
				continue;
			}
			break;
			IL_1198:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmSQLfilter_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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
				case 68:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					BuildForm.Invoke_CSVViewer3(0, "HELP2");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Dispose();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 68;
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

	private void Load_Row_Filter_Value(string Curr_Value)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
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
						errsource = "frmSQLFilter - Load_Row_Filter_Value";
						string MyCol = "";
						long num3 = 0L;
						long num4 = 0L;
						long num5 = 0L;
						bool flag = false;
						string text = "";
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						if (Operators.CompareString(Strings.Trim(Curr_Value), "", TextCompare: false) != 0)
						{
							if (gSpecialTest == 0 || gSpecialTest == 3 || gSpecialTest == 4 || gSpecialTest == 5 || gSpecialTest == 6 || gSpecialTest == 7)
							{
								if (LikeOperator.LikeString(Strings.UCase(Globals_Renamed.curroprtmp), "*TEMP*", CompareMethod.Binary))
								{
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, "!!!!!!!!!", ref DynArray);
								}
								else if (((Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "D", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in group(bt->d)", TextCompare: false) != 0)) || ((Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "V", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in group(bt->d)", TextCompare: false) != 0)) || ((Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "T", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in group(bt->t)", TextCompare: false) != 0)) || ((Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "H", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in group(bt->t)", TextCompare: false) != 0)) || ((Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "L", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in group(bt->t)", TextCompare: false) != 0)) || General_Procedures.IsDateDT(Globals_Renamed.currdatatypetmp, 2))
								{
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, ";", ref DynArray);
								}
								else if (Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "E", TextCompare: false) == 0)
								{
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, "$%^}", ref DynArray);
								}
								else if (Strings.InStr(Curr_Value, "</comma\\>") != 0)
								{
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, "</comma\\>", ref DynArray);
								}
								else if ((Operators.CompareString(Globals_Renamed.curroprtmp, "=", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.curroprtmp, "<>", TextCompare: false) == 0) | LikeOperator.LikeString(Strings.UCase(Globals_Renamed.curroprtmp), "*LIKE", CompareMethod.Binary))
								{
									if (Strings.InStr(Curr_Value, ",") != 0)
									{
										Curr_Value = Strings.Replace(Curr_Value, ",", "</comma\\>", 1, -1, CompareMethod.Text);
										flag = true;
									}
									else
									{
										flag = false;
									}
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, ",", ref DynArray);
									if (flag)
									{
										long num6 = num4;
										for (num5 = 1L; num5 <= num6; num5++)
										{
											DynArray[(int)num5] = Strings.Replace(DynArray[(int)num5], "</comma\\>", ",", 1, -1, CompareMethod.Text);
										}
									}
								}
								else
								{
									num4 = General_Procedures.ParseAndFillArray(Curr_Value, ",", ref DynArray);
								}
							}
							else
							{
								num4 = General_Procedures.ParseAndFillArray(Curr_Value, "),(", ref DynArray);
							}
							long num7 = num4;
							for (num5 = 1L; num5 <= num7; num5++)
							{
								if (Operators.CompareString(DynArray[(int)num5], "''", TextCompare: false) != 0)
								{
									DynArray[(int)num5] = General_Procedures.StripQuotesAndDates(DynArray[(int)num5], Globals_Renamed.currdatatypetmp, gSpecialTest);
								}
								if (Strings.InStr(Strings.LCase(Globals_Renamed.curroprtmp), "temp") == 0)
								{
									GrdList.Rows[(int)(num5 - 1)].Cells[0].Value = DynArray[(int)num5];
								}
							}
						}
						if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*temp", CompareMethod.Binary))
						{
							num3 = Strings.InStr(DynArray[1], ":");
							if (num3 != 0)
							{
								TxtBox1.Text = Strings.Trim(Strings.Mid(DynArray[1], 1, (int)(num3 - 1)));
								TxtBox2.Text = Strings.Trim(Strings.Mid(DynArray[1], (int)(num3 + 1)));
							}
							else
							{
								TxtBox1.Text = Strings.Trim(DynArray[1]);
								TxtBox2.Text = "";
							}
							ll_TTColID = DynArray[2];
						}
						else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "in temp(*)", CompareMethod.Binary))
						{
							num3 = Strings.InStr(DynArray[1], ":");
							if (num3 != 0)
							{
								TxtBox1.Text = Strings.Trim(Strings.Mid(DynArray[1], 1, (int)(num3 - 1)));
								text = Strings.Trim(Strings.Mid(DynArray[1], (int)(num3 + 1)));
								string[] array = Strings.Split(text, ";");
								long num8 = Information.UBound(array);
								for (num5 = 0L; num5 <= num8; num5++)
								{
									GrdList.Rows[(int)num5].Cells[0].Value = Strings.Trim(array[(int)num5]);
								}
							}
							ll_TTColID = DynArray[2];
						}
						else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*between", CompareMethod.Binary))
						{
							TxtBox1.Text = DynArray[1];
							TxtBox2.Text = DynArray[2];
						}
						else if (Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "in", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "like list", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Globals_Renamed.curroprtmp), "(+) in", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "*regex list", CompareMethod.Binary))
						{
							if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.curroprtmp), "* group*", CompareMethod.Binary) & (Operators.CompareString(Strings.Trim(DynArray[1]), "", TextCompare: false) != 0))
							{
								BuildForm.Extract_IG_ColNo(ref DynArray[1], ref MyCol);
								cmbColIG.Text = MyCol;
							}
							TxtBox1.Text = DynArray[1];
						}
						goto end_IL_0001;
					}
					case 1735:
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
					goto IL_06fd;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1735;
				continue;
			}
			break;
			IL_06fd:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Set_Row_Filter_Controls(string MyOption, string Operator_Renamed)
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "frmSQLFilter - Set_Row_Filter_Controls";
						string text = "";
						short num3 = 0;
						string text2 = "yyyy-mm-dd hh24:mi:ss";
						string text3 = "";
						GrdList.Top = TxtBox1.Top;
						text = Globals_Renamed.currdatatypetmp;
						chkUp.Visible = true;
						text3 = ((Operators.CompareString(Strings.Mid(Globals_Renamed.currhelptmp, 1, 1), "~", TextCompare: false) != 0) ? "" : " or Select from list");
						if (Operators.CompareString(ll_DbNameType, "iMBigData", TextCompare: false) == 0 || ll_NoPrompt)
						{
							ChkPrompt.Visible = false;
							chkUp.Top = Label1.Top;
						}
						else
						{
							ChkPrompt.Visible = true;
						}
						Swap_lblBox();
						string left = Strings.UCase(MyOption);
						if (Operators.CompareString(left, "CLEAR", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "SET", TextCompare: false) != 0)
							{
								goto end_IL_0001;
							}
							if (Strings.InStr(Strings.UCase(Operator_Renamed), "(+)") != 0)
							{
								Operator_Renamed = Strings.Mid(Operator_Renamed, 5);
							}
							if (Strings.InStr(Strings.UCase(Operator_Renamed), "NULL") != 0)
							{
								Operator_Renamed = "NULL";
							}
							else if (Operators.CompareString(Strings.Left(Strings.UCase(Operator_Renamed), 4), "NOT ", TextCompare: false) == 0)
							{
								Operator_Renamed = Strings.Mid(Operator_Renamed, 5);
							}
							CmdCal1.Visible = true;
							lblColIG.Visible = false;
							cmbColIG.Visible = false;
							TxtIncrement.Visible = false;
							lblinc.Visible = false;
							if (LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "IN GROUP(*", CompareMethod.Binary))
							{
								Operator_Renamed = "IN GROUP(";
							}
							else if (LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "IN TEMP(*=*)", CompareMethod.Binary))
							{
								Operator_Renamed = "IN TEMP(";
							}
							else if (!LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "IN TEMP(*)", CompareMethod.Binary) && LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "*TEMP", CompareMethod.Binary))
							{
								Operator_Renamed = "IN TEMP";
							}
							switch (Strings.UCase(Operator_Renamed))
							{
							case "BETWEEN":
								switch (text)
								{
								case "D":
								case "T":
								case "H":
								case "G":
								case "U":
								case "M":
								case "O":
								case "L":
								case "P":
								case "V":
								case "W":
								case "J":
									CmdCal2.Visible = true;
									if (Operators.CompareString(text, "D", TextCompare: false) == 0 || Operators.CompareString(text, "V", TextCompare: false) == 0)
									{
										text2 = "dd-mon-yyyy hh24:mi:ss";
									}
									lblbox1.Text = "Start Date (" + text2 + ")";
									lblbox2.Text = "End Date (" + text2 + ")";
									break;
								case "S":
									if (Operators.CompareString(Globals_Renamed.currhelptmp, "", TextCompare: false) != 0)
									{
										lblbox1.Text = "Start Date (" + Globals_Renamed.currhelptmp + ")";
										lblbox2.Text = "End Date (" + Globals_Renamed.currhelptmp + ")";
									}
									else
									{
										lblbox1.Text = "Start Date (YYYYMMDDhh24miss)";
										lblbox2.Text = "End Date (YYYYMMDDhh24miss)";
									}
									break;
								default:
									CmdCal2.Visible = false;
									lblbox1.Text = "Start Value" + text3;
									lblbox2.Text = "End Value" + text3;
									break;
								}
								if (Operators.CompareString(text, "E", TextCompare: false) == 0)
								{
									CmdCal2.Visible = true;
								}
								GrdList.Visible = false;
								TxtBox1.Visible = true;
								TxtBox2.Visible = true;
								lblbox1.Visible = true;
								lblbox2.Visible = true;
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								break;
							case "IN":
							case "LIKE LIST":
							case "(+) IN":
							case "REGEX LIST":
							case "NOT REGEX LIST":
								CmdCal2.Visible = false;
								GrdList.Visible = true;
								TxtBox1.Visible = false;
								TxtBox2.Visible = false;
								lblbox1.Visible = true;
								CmdClear.Enabled = true;
								cmdPaste.Enabled = true;
								if (gSpecialTest == 1)
								{
									lblbox1.Text = "Test Name and Structure separated by commas";
								}
								else if (gSpecialTest == 2)
								{
									lblbox1.Text = "Lot and Wafer ID separated by commas";
								}
								else if (gSpecialTest == 4)
								{
									if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*fuse*", CompareMethod.Binary))
									{
										lblbox1.Text = "Lot7, 3 digit wafer, X and Y locations separated by commas";
									}
									else
									{
										lblbox1.Text = "Lot with wildcard, wafer3, X, Y separated by commas";
									}
								}
								else if (gSpecialTest == 6)
								{
									if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*ctr_testvalue_filteronly*", CompareMethod.Binary))
									{
										lblbox1.Text = "Test Pattern (%=all), categorizing, distinct value separated by commas";
									}
									else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*counter_test_filteronly*", CompareMethod.Binary))
									{
										lblbox1.Text = "One Operation + counters separated by commas. E.g., 7721,4285,4286,R42";
									}
									else
									{
										lblbox1.Text = "Test Pattern (%=wild) and Reg Ex value for Result separated by commas";
									}
								}
								else if (gSpecialTest == 7)
								{
									if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*ptr_fail-pattern_filteronly*", CompareMethod.Binary))
									{
										lblbox1.Text = "Regular Expression (e.g., ^ABC(D|E|F).*GHI$)";
									}
								}
								else if (gSpecialTest == 5)
								{
									lblbox1.Text = "Lot and wafer3 separated by commas";
								}
								else
								{
									lblbox1.Text = "Value(s)" + text3;
								}
								lblbox2.Visible = false;
								if (Operators.CompareString(Strings.UCase(Operator_Renamed), "LIKE LIST", TextCompare: false) == 0)
								{
									LblEscape.Visible = true;
									TxtEscape.Visible = true;
								}
								else
								{
									LblEscape.Visible = false;
									TxtEscape.Visible = false;
								}
								break;
							case "IN FILE":
							case "LIKE FILE":
							case "IN GROUP":
							case "LIKE GROUP":
							case "IN GROUP(":
							case "REGEX GROUP":
							case "NOT-IN GROUP":
							case "NOT-REGEX GROUP":
								CmdCal2.Visible = false;
								GrdList.Visible = false;
								TxtBox1.Visible = true;
								TxtBox2.Visible = false;
								lblbox1.Visible = true;
								lblbox1.Text = "Path to File with values";
								lblbox2.Visible = false;
								TxtIncrement.Visible = false;
								lblinc.Visible = false;
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								if (Operators.CompareString(Strings.UCase(Operator_Renamed), "LIKE GROUP", TextCompare: false) == 0)
								{
									LblEscape.Visible = true;
									TxtEscape.Visible = true;
									if (AllowIncExtract)
									{
										TxtIncrement.Visible = true;
										TxtIncrement.Top = TxtBox2.Top;
										lblinc.Visible = true;
										lblinc.Top = lblbox2.Top;
									}
								}
								else if ((Operators.CompareString(Strings.UCase(Operator_Renamed), "IN GROUP", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Operator_Renamed), "REGEX GROUP", TextCompare: false) == 0) && Operators.CompareString(ll_DbNameType, "Mongo", TextCompare: false) == 0)
								{
									TxtIncrement.Visible = true;
									TxtIncrement.Top = TxtBox2.Top;
									lblinc.Visible = true;
									lblinc.Top = lblbox2.Top;
								}
								else if (AllowIncExtract && (Operators.CompareString(Strings.UCase(Operator_Renamed), "IN GROUP", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Operator_Renamed), "IN GROUP(", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Operator_Renamed), "LIKE GROUP", TextCompare: false) == 0) && !LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "IN GROUP(BT*)", CompareMethod.Binary))
								{
									TxtIncrement.Visible = true;
									TxtIncrement.Top = TxtBox2.Top;
									lblinc.Visible = true;
									lblinc.Top = lblbox2.Top;
								}
								else
								{
									TxtIncrement.Visible = false;
									lblinc.Visible = false;
									TxtIncrement.Text = "";
								}
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								if (LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "* GROUP*", CompareMethod.Binary))
								{
									lblColIG.Visible = true;
									cmbColIG.Visible = true;
								}
								if (Operators.CompareString(ll_DbNameType, "Mongo", TextCompare: false) == 0)
								{
									ChkPrompt.Visible = false;
									ChkPrompt.CheckState = CheckState.Unchecked;
								}
								break;
							case "IN TEMP":
								if (AllowIncExtract)
								{
									TxtIncrement.Visible = true;
									TxtIncrement.Top = TxtBox1.Top;
									lblinc.Visible = true;
									lblinc.Top = lblbox1.Top;
								}
								else
								{
									TxtIncrement.Visible = false;
									lblinc.Visible = false;
									TxtIncrement.Text = "";
								}
								CmdCal1.Visible = false;
								CmdCal2.Visible = false;
								GrdList.Visible = false;
								TxtBox1.Visible = true;
								TxtBox2.Visible = true;
								lblbox1.Visible = true;
								lblbox2.Visible = false;
								lblbox1.Text = "Temp Table And Cols";
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								lblColIG.Visible = false;
								cmbColIG.Visible = false;
								ChkPrompt.Visible = false;
								ChkPrompt.CheckState = CheckState.Unchecked;
								chkUp.Visible = false;
								break;
							case "IN TEMP()":
							case "IN TEMP(BT->D)":
							case "IN TEMP(BT->T)":
							case "IN TEMP(":
								if (AllowIncExtract && !LikeOperator.LikeString(Strings.UCase(Operator_Renamed), "IN TEMP(BT*)", CompareMethod.Binary))
								{
									TxtIncrement.Visible = true;
									lblinc.Visible = true;
									CmdCal1.Visible = false;
									lblinc.Top = CmdCal1.Top;
									TxtIncrement.Top = lblbox2.Top - 5;
								}
								else
								{
									TxtIncrement.Visible = false;
									lblinc.Visible = false;
								}
								CmdCal1.Visible = false;
								CmdCal2.Visible = false;
								GrdList.Top = lblbox2.Top - 5;
								GrdList.Visible = true;
								TxtBox1.Visible = true;
								TxtBox2.Visible = false;
								lblbox1.Visible = true;
								lblbox2.Visible = false;
								CmdClear.Enabled = true;
								lblbox1.Text = "Temp Table And Cols";
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								cmdPaste.Enabled = false;
								lblColIG.Visible = false;
								cmbColIG.Visible = false;
								ChkPrompt.Visible = false;
								ChkPrompt.CheckState = CheckState.Unchecked;
								chkUp.Visible = false;
								break;
							case "LIKE":
								CmdCal2.Visible = false;
								GrdList.Visible = false;
								TxtBox1.Visible = true;
								TxtBox2.Visible = false;
								lblbox1.Visible = true;
								lblbox1.Text = "Pattern And Opt Escape Char";
								lblbox2.Visible = false;
								LblEscape.Visible = true;
								TxtEscape.Visible = true;
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								break;
							case "NULL":
								CmdCal2.Visible = false;
								GrdList.Visible = false;
								TxtBox1.Visible = false;
								TxtBox2.Visible = false;
								lblbox1.Visible = false;
								lblbox1.Text = "";
								lblbox2.Visible = false;
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								ChkPrompt.Visible = false;
								CmdCal1.Visible = false;
								break;
							default:
								switch (text)
								{
								case "D":
								case "V":
									lblbox1.Text = "Date (dd-mmm-yyyy hh:mi:ss)";
									break;
								case "T":
								case "H":
								case "G":
								case "U":
								case "M":
								case "O":
								case "L":
								case "P":
								case "W":
								case "J":
									lblbox1.Text = "Date (yyyy-mm-dd hh:mi:ss)";
									break;
								case "S":
									if (Operators.CompareString(Globals_Renamed.currhelptmp, "", TextCompare: false) != 0)
									{
										lblbox1.Text = "Date (" + Globals_Renamed.currhelptmp + ")";
									}
									else
									{
										lblbox1.Text = "Date (YYYYMMDDhh24miss)";
									}
									break;
								default:
									lblbox1.Text = "Value" + text3;
									break;
								}
								if (gSpecialTest == 1)
								{
									lblbox1.Text = "Test Name and Structure separated by comma";
								}
								else if (gSpecialTest == 2)
								{
									lblbox1.Text = "Lot and Wafer ID separated by comma";
								}
								else if (gSpecialTest == 7 && LikeOperator.LikeString(Strings.LCase(Globals_Renamed.currtxttmp), "*ptr_fail-pattern_filteronly*", CompareMethod.Binary))
								{
									lblbox1.Text = "Enter a Regular Expression (e.g., ^ABC(D|E|F).*GHI$)";
								}
								CmdCal2.Visible = false;
								TxtBox1.Visible = true;
								TxtBox2.Visible = false;
								lblbox1.Visible = true;
								GrdList.Visible = false;
								lblbox2.Visible = false;
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
								CmdClear.Enabled = false;
								cmdPaste.Enabled = false;
								break;
							}
							goto end_IL_0001;
						}
						CmdCal2.Visible = false;
						GrdList.Visible = false;
						lblbox2.Text = "Prompt Text";
						lblbox1.Text = "Default Value";
						lblbox1.Visible = true;
						lblbox2.Visible = true;
						TxtBox1.Visible = true;
						TxtBox2.Visible = true;
						CmdCal1.Visible = true;
						TxtIncrement.Visible = false;
						lblinc.Visible = false;
						if (ll_IsLoaded)
						{
							TxtBox2.Focus();
							TxtBox2.SelectionStart = 0;
							TxtBox2.SelectionLength = 0;
						}
						LblEscape.Visible = false;
						TxtEscape.Visible = false;
						lblColIG.Visible = false;
						cmbColIG.Visible = false;
						CmdClear.Enabled = false;
						cmdPaste.Enabled = false;
						goto end_IL_0001_2;
					}
					case 6194:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_1868;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6194;
				continue;
			}
			break;
			IL_1868:
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

	private void Label1_Click(object eventSender, EventArgs eventArgs)
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
				case 60:
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
				lbldesc.Text = Globals_Renamed.currprompttmp;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 60;
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

	private void TxtBox1_Enter(object sender, EventArgs e)
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
				case 89:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (ChkPrompt.CheckState != CheckState.Checked)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				lbldesc.Text = General_Procedures.Get_UI("date-prompt");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 89;
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

	private void txtbox1_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
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
				case 215:
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
							goto IL_003d;
						case 6:
							goto IL_0044;
						case 7:
							goto IL_005a;
						case 5:
						case 8:
						case 9:
							goto IL_0064;
						case 10:
							goto IL_0074;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005a:
					num2 = 7;
					num5 = General_Procedures.ToUpper(num5);
					goto IL_0064;
					IL_0064:
					num2 = 9;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_0074;
					IL_0044:
					num2 = 6;
					if (chkUp.CheckState == CheckState.Checked)
					{
						goto IL_005a;
					}
					goto IL_0064;
					IL_0074:
					num2 = 10;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0011:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary))
					{
						goto IL_003d;
					}
					goto IL_0044;
					IL_003d:
					num2 = 4;
					num5 = 0;
					goto IL_0064;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				eventArgs.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 215;
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

	private void TxtBox2_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string left = default(string);
		short g_NoQColumns = default(short);
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
				case 362:
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
							goto IL_005a;
						case 6:
							goto IL_0069;
						case 7:
							goto IL_0076;
						case 8:
							goto IL_0083;
						case 9:
							goto IL_00a6;
						case 11:
						case 12:
							goto IL_00c3;
						case 10:
						case 13:
							goto IL_00d0;
						case 14:
							goto IL_00ed;
						case 15:
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d0:
					num2 = 13;
					if (Operators.CompareString(ll_TTColID, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00ed;
					IL_00ed:
					num2 = 14;
					TxtBox1.Text = "";
					break;
					IL_00a6:
					num2 = 9;
					ll_TTColID = Globals_Renamed.g_QColumns[num5].ColID;
					goto IL_00d0;
					IL_00c3:
					num2 = 12;
					num5 = checked((short)unchecked(num5 + 1));
					goto IL_00cb;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					left = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (!(LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary) & LikeOperator.LikeString(Strings.UCase(TxtBox1.Text), "*{*}", CompareMethod.Binary)))
					{
						break;
					}
					goto IL_005a;
					IL_005a:
					num2 = 5;
					left = TxtBox2.Text;
					goto IL_0069;
					IL_0069:
					num2 = 6;
					ll_TTColID = "";
					goto IL_0076;
					IL_0076:
					num2 = 7;
					g_NoQColumns = Globals_Renamed.g_NoQColumns;
					num5 = 0;
					goto IL_00cb;
					IL_00cb:
					if (num5 <= g_NoQColumns)
					{
						goto IL_0083;
					}
					goto IL_00d0;
					IL_0083:
					num2 = 8;
					if (Operators.CompareString(left, Globals_Renamed.g_QColumns[num5].Header, TextCompare: false) == 0)
					{
						goto IL_00a6;
					}
					goto IL_00c3;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 362;
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

	private void TxtBox2_DropDown(object eventSender, EventArgs eventArgs)
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
				case 206:
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
							goto IL_0048;
						case 5:
							goto IL_0054;
						case 6:
							goto IL_0077;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0048:
					num2 = 3;
					Load_Temp_Table_Columns();
					break;
					IL_0054:
					num2 = 5;
					if (!LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0077;
					IL_000b:
					num2 = 2;
					if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary) & LikeOperator.LikeString(Strings.UCase(TxtBox1.Text), "*{*}", CompareMethod.Binary))
					{
						goto IL_0048;
					}
					goto IL_0054;
					IL_0077:
					num2 = 6;
					TxtBox2.Items.Clear();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 206;
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

	private void TxtBox2_Enter(object eventSender, EventArgs eventArgs)
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
				case 90:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (ChkPrompt.CheckState != CheckState.Checked)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				lbldesc.Text = "Enter prompt text for your column. The word 'Enter' will be added to the beginning of your text";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 90;
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

	private void txtbox2_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
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
				case 215:
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
							goto IL_003d;
						case 6:
							goto IL_0044;
						case 7:
							goto IL_005a;
						case 5:
						case 8:
						case 9:
							goto IL_0064;
						case 10:
							goto IL_0074;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005a:
					num2 = 7;
					num5 = General_Procedures.ToUpper(num5);
					goto IL_0064;
					IL_0064:
					num2 = 9;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_0074;
					IL_0044:
					num2 = 6;
					if (chkUp.CheckState == CheckState.Checked)
					{
						goto IL_005a;
					}
					goto IL_0064;
					IL_0074:
					num2 = 10;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0011:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "*TEMP", CompareMethod.Binary))
					{
						goto IL_003d;
					}
					goto IL_0044;
					IL_003d:
					num2 = 4;
					num5 = 0;
					goto IL_0064;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				eventArgs.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 215;
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

	private void mnuPasteGrid_Click(object sender, EventArgs e)
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
				case 71:
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
				DataGridView Grid = GrdList;
				BuildForm.HG_mnuPaste2(ref Grid, 1000);
				GrdList = Grid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 71;
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

	private void mnuClearGrid_Click(object sender, EventArgs e)
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
				case 58:
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
				DataGridView Grid = GrdList;
				BuildForm.HG_mnuClear2(ref Grid);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 58;
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

	private void GrdList_Click(object sender, EventArgs e)
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
				ComboBox MyCombo;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 193:
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
							goto IL_0026;
						case 5:
							goto IL_0066;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (GrdList.CurrentRow.Index == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0026;
					IL_0026:
					num2 = 4;
					MyGrid = GrdList;
					MyCombo = CmbTxt;
					GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_ROW, ref f_COL, 1);
					CmbTxt = MyCombo;
					GrdList = MyGrid;
					goto IL_0066;
					IL_0066:
					num2 = 5;
					CmbTxt.SelectionLength = 0;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				CmbTxt.SelectionStart = Strings.Len(CmbTxt.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 193;
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

	private void GrdList_KeyDown(object sender, KeyEventArgs e)
	{
		f_TrueRow = checked((short)GrdList.CurrentCell.RowIndex);
	}

	private void GrdList_KeyPress(object sender, KeyPressEventArgs e)
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
				case 122:
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
				DataGridView GrdList = this.GrdList;
				ComboBox CmbTxt = this.CmbTxt;
				BuildForm.HG_GrdList_KP(ref GrdList, ref CmbTxt, ref e, chkUp.Checked, ref CmbBoxKey, ref f_TrueRow, ref f_ROW, ref f_COL);
				this.CmbTxt = CmbTxt;
				this.GrdList = GrdList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 122;
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

	private void GrdList_Resize(object sender, EventArgs e)
	{
		GrdList.Columns[0].Width = GrdList.Width;
	}

	private void chkUp_CheckedChanged(object sender, EventArgs e)
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
							goto IL_001c;
						case 4:
							goto IL_002a;
						case 5:
							goto IL_0048;
						case 7:
							goto IL_005a;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002a:
					num2 = 4;
					TxtBox1.SelectionStart = Strings.Len(TxtBox1.Text);
					goto IL_0048;
					IL_0048:
					num2 = 5;
					TxtBox1.SelectionLength = 0;
					goto end_IL_0001_3;
					IL_001c:
					num2 = 3;
					TxtBox1.Focus();
					goto IL_002a;
					IL_005a:
					num2 = 7;
					if (!GrdList.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if (TxtBox1.Visible)
					{
						goto IL_001c;
					}
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				GrdList.Focus();
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

	private void CmbTxt_DropDown(object sender, EventArgs e)
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
				case 210:
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
							goto IL_004c;
						case 5:
							goto IL_0058;
						case 6:
							goto IL_007b;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_007b:
					num2 = 6;
					CmbTxt.Items.Clear();
					break;
					IL_004c:
					num2 = 3;
					Load_Temp_Table_Columns();
					break;
					IL_000b:
					num2 = 2;
					if (LikeOperator.LikeString(Strings.UCase(Combo1.Text), "IN TEMP(*)", CompareMethod.Binary) && LikeOperator.LikeString(Strings.UCase(TxtBox1.Text), "*{*}", CompareMethod.Binary))
					{
						goto IL_004c;
					}
					goto IL_0058;
					IL_0058:
					num2 = 5;
					if (!LikeOperator.LikeString(Strings.UCase(Combo1.Text), "IN TEMP(*)", CompareMethod.Binary))
					{
						break;
					}
					goto IL_007b;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 210;
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

	private void CmbTxt_KeyPress(object sender, KeyPressEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 184:
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
							goto IL_001a;
						case 4:
							goto IL_0027;
						case 5:
							goto IL_0030;
						case 7:
							goto IL_0047;
						case 8:
							goto IL_005d;
						case 6:
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
					IL_0030:
					num2 = 5;
					CmbTxt_SelectedIndexChanged(RuntimeHelpers.GetObjectValue(sender), new EventArgs());
					break;
					IL_0047:
					num2 = 7;
					if (chkUp.CheckState != CheckState.Checked)
					{
						break;
					}
					goto IL_005d;
					IL_0027:
					num2 = 4;
					CmbBoxKey = false;
					goto IL_0030;
					IL_005d:
					num2 = 8;
					num5 = General_Procedures.ToUpper(num5);
					break;
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (num5 == 13)
					{
						goto IL_0027;
					}
					goto IL_0047;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				e.KeyChar = Strings.Chr(num5);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 184;
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

	private void CmbTxt_Leave(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					errsource = "FrmRSQLFilter - CmbTxt_Leave";
					ProjectData.ClearProjectError();
					num2 = 2;
					GrdList.Rows[f_ROW].Cells[f_COL].Value = CmbTxt.Text;
					GrdList.Refresh();
					CmbTxt.Visible = false;
					CmbBoxKey = false;
					goto end_IL_0001;
				case 153:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						CmbBoxKey = false;
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 153;
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

	private void GrdList_Scroll(object sender, ScrollEventArgs e)
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
				GrdList.Focus();
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

	private void CmbTxt_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && GrdList.Visible)
		{
			CmbTxt_Leave(RuntimeHelpers.GetObjectValue(sender), e);
		}
	}

	private void CmbTxt_SelectedIndexChanged(object sender, EventArgs e)
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
						case 3:
							goto IL_001a;
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
					if (CmbBoxKey)
					{
						goto end_IL_0001_3;
					}
					goto IL_001a;
					IL_001a:
					num2 = 3;
					CmbTxt_Leave(CmbTxt, new EventArgs());
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				GrdList.Focus();
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

	private void mnuEditGrdList_Click(object sender, EventArgs e)
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
				case 88:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!GrdList.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				GrdList_Click(GrdList, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 88;
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

	private void mnuHelp_Click(object sender, EventArgs e)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Filtering+Data");
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

	private void mnuCopy2_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GrdList;
				BuildForm.HG_Copy2(ref MyGrid);
				GrdList = MyGrid;
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

	private void ChkPrompt_KeyPress(object sender, KeyPressEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		short num6 = default(short);
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
							goto IL_001a;
						case 4:
							goto IL_0020;
						case 5:
							goto IL_002d;
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0058;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 10:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002d:
					num2 = 5;
					num5 = Conversions.ToInteger(NewLateBinding.LateGet(sender, null, "CheckState", new object[0], null, null, null));
					goto IL_004b;
					IL_004b:
					num2 = 6;
					if (num5 != 1)
					{
						break;
					}
					goto IL_0058;
					IL_0020:
					num2 = 4;
					if (num6 != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_002d;
					IL_0058:
					num2 = 7;
					NewLateBinding.LateSet(sender, null, "checked", new object[1] { false }, null, null);
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num6 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_001a;
					IL_001a:
					num2 = 3;
					num5 = -99;
					goto IL_0020;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				NewLateBinding.LateSet(sender, null, "checked", new object[1] { true }, null, null);
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

	private void chkUp_GotFocus(object sender, EventArgs e)
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
				base.AcceptButton = null;
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

	private void chkUp_LostFocus(object sender, EventArgs e)
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
				set_Button_default(Combo1.Text);
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

	private void Combo1_DropDownClosed(object sender, EventArgs e)
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
				case 129:
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
							goto IL_001c;
						case 5:
							goto IL_002d;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (TxtBox1.Visible)
					{
						goto IL_001c;
					}
					goto IL_002d;
					IL_001c:
					num2 = 3;
					TxtBox1.Focus();
					goto end_IL_0001_3;
					IL_002d:
					num2 = 5;
					if (!GrdList.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				GrdList.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 129;
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

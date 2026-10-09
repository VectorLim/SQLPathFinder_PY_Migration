using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class frmgetdata : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private Button _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdPaste")]
	private Button _cmdPaste;

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
	[AccessedThroughProperty("GrdList")]
	private DataGridView _GrdList;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPaste1")]
	private ToolStripMenuItem _mnuPaste1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClear1")]
	private ToolStripMenuItem _mnuClear1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("ChkUp")]
	private CheckBox _ChkUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbTxt")]
	private ComboBox _CmbTxt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEditGrdList")]
	private ToolStripMenuItem _mnuEditGrdList;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp")]
	private ToolStripMenuItem _mnuHelp;

	public short ll_ObjectType;

	public string ll_Mode;

	public string ll_Tag;

	private string MyColumn;

	private string MyOperator;

	private string MyDT;

	private string MyPrompt;

	private string MySQLHelp;

	private short gSpecialTest;

	private short f_Row;

	private short f_Col;

	private short f_TrueRow;

	private bool CmbBoxKey;

	private const short l_TotalRows = 1000;

	[field: AccessedThroughProperty("MainMenu1")]
	public virtual MenuStrip MainMenu1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdLoadGrid")]
	public virtual Button cmdLoadGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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
			KeyPressEventHandler value2 = txtbox2_KeyPress;
			ComboBox comboBox = _TxtBox2;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
			}
			_TxtBox2 = value;
			comboBox = _TxtBox2;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
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
			KeyPressEventHandler value2 = txtbox1_KeyPress;
			ComboBox comboBox = _TxtBox1;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
			}
			_TxtBox1 = value;
			comboBox = _TxtBox1;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
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

	[field: AccessedThroughProperty("lblbox1")]
	public virtual Label lblbox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblcolIG")]
	public virtual Label lblcolIG
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

	[field: AccessedThroughProperty("lblPrompt")]
	public virtual Label lblPrompt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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
			ScrollEventHandler value5 = GrdList_Scroll1;
			DataGridView dataGridView = _GrdList;
			if (dataGridView != null)
			{
				dataGridView.Click -= value2;
				dataGridView.KeyDown -= value3;
				dataGridView.KeyPress -= value4;
				dataGridView.Scroll -= value5;
			}
			_GrdList = value;
			dataGridView = _GrdList;
			if (dataGridView != null)
			{
				dataGridView.Click += value2;
				dataGridView.KeyDown += value3;
				dataGridView.KeyPress += value4;
				dataGridView.Scroll += value5;
			}
		}
	}

	[field: AccessedThroughProperty("ContextMenuGrdList")]
	internal virtual ContextMenuStrip ContextMenuGrdList
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuPaste1
	{
		[CompilerGenerated]
		get
		{
			return _mnuPaste1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPaste1_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPaste1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPaste1 = value;
			toolStripMenuItem = _mnuPaste1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuClear1
	{
		[CompilerGenerated]
		get
		{
			return _mnuClear1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClear1_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClear1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClear1 = value;
			toolStripMenuItem = _mnuClear1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual CheckBox ChkUp
	{
		[CompilerGenerated]
		get
		{
			return _ChkUp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = ChkUp_KeyPress;
			EventHandler value3 = ChkUp_GotFocus;
			EventHandler value4 = ChkUp_LostFocus;
			CheckBox checkBox = _ChkUp;
			if (checkBox != null)
			{
				checkBox.KeyPress -= value2;
				checkBox.GotFocus -= value3;
				checkBox.LostFocus -= value4;
			}
			_ChkUp = value;
			checkBox = _ChkUp;
			if (checkBox != null)
			{
				checkBox.KeyPress += value2;
				checkBox.GotFocus += value3;
				checkBox.LostFocus += value4;
			}
		}
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
			KeyPressEventHandler value2 = CmbTxt_KeyPress;
			EventHandler value3 = CmbTxt_Leave;
			EventHandler value4 = CmbTxt_SelectedIndexChanged;
			ComboBox comboBox = _CmbTxt;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
				comboBox.Leave -= value3;
				comboBox.SelectedIndexChanged -= value4;
			}
			_CmbTxt = value;
			comboBox = _CmbTxt;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
				comboBox.Leave += value3;
				comboBox.SelectedIndexChanged += value4;
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

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewTextBoxColumn Column1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCloseGrid")]
	internal virtual Button cmdCloseGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmgetdata()
	{
		base.Load += frmgetdata_Load;
		base.FormClosing += frmgetdata_FormClosing;
		base.FormClosed += frmgetdata_FormClosed;
		ll_ObjectType = -99;
		ll_Mode = "N";
		ll_Tag = "";
		MyColumn = "";
		MyOperator = "";
		MyDT = "";
		MyPrompt = "";
		MySQLHelp = "";
		gSpecialTest = 0;
		f_Row = 0;
		f_Col = 0;
		f_TrueRow = -1;
		CmbBoxKey = false;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdLoadGrid = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.cmdPaste = new System.Windows.Forms.Button();
		this.CmdCal2 = new System.Windows.Forms.Button();
		this.CmdCal1 = new System.Windows.Forms.Button();
		this.MainMenu1 = new System.Windows.Forms.MenuStrip();
		this.mnuEditGrdList = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.ContextMenuGrdList = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuPaste1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClear1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmbColIG = new System.Windows.Forms.ComboBox();
		this.TxtBox2 = new System.Windows.Forms.ComboBox();
		this.TxtBox1 = new System.Windows.Forms.ComboBox();
		this.TxtEscape = new System.Windows.Forms.TextBox();
		this.CmdHelp = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.lblbox1 = new System.Windows.Forms.Label();
		this.lblcolIG = new System.Windows.Forms.Label();
		this.lbldesc = new System.Windows.Forms.Label();
		this.LblEscape = new System.Windows.Forms.Label();
		this.lblbox2 = new System.Windows.Forms.Label();
		this.lblPrompt = new System.Windows.Forms.Label();
		this.GrdList = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ChkUp = new System.Windows.Forms.CheckBox();
		this.CmbTxt = new System.Windows.Forms.ComboBox();
		this.cmdCloseGrid = new System.Windows.Forms.Button();
		this.MainMenu1.SuspendLayout();
		this.ContextMenuGrdList.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GrdList).BeginInit();
		base.SuspendLayout();
		this.cmdLoadGrid.BackColor = System.Drawing.SystemColors.Control;
		this.cmdLoadGrid.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdLoadGrid.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdLoadGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdLoadGrid.Location = new System.Drawing.Point(266, 0);
		this.cmdLoadGrid.Name = "cmdLoadGrid";
		this.cmdLoadGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdLoadGrid.Size = new System.Drawing.Size(52, 40);
		this.cmdLoadGrid.TabIndex = 11;
		this.cmdLoadGrid.Text = "Show Grid";
		this.ToolTip1.SetToolTip(this.cmdLoadGrid, "Load a Grid With Help Values After Running Help Query (i.e., click ?)");
		this.cmdLoadGrid.UseVisualStyleBackColor = false;
		this.cmdLoadGrid.Visible = false;
		this.cmdClear.BackColor = System.Drawing.SystemColors.Control;
		this.cmdClear.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdClear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdClear.ImageIndex = 0;
		this.cmdClear.Location = new System.Drawing.Point(162, 0);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdClear.Size = new System.Drawing.Size(52, 40);
		this.cmdClear.TabIndex = 9;
		this.cmdClear.Text = "Clear";
		this.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear the Grid");
		this.cmdClear.UseVisualStyleBackColor = false;
		this.cmdPaste.BackColor = System.Drawing.SystemColors.Control;
		this.cmdPaste.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdPaste.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdPaste.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdPaste.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdPaste.ImageIndex = 2;
		this.cmdPaste.Location = new System.Drawing.Point(214, 0);
		this.cmdPaste.Name = "cmdPaste";
		this.cmdPaste.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdPaste.Size = new System.Drawing.Size(52, 40);
		this.cmdPaste.TabIndex = 10;
		this.cmdPaste.Text = "Paste";
		this.cmdPaste.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.cmdPaste, "Paste clipboard contents to grid");
		this.cmdPaste.UseVisualStyleBackColor = false;
		this.CmdCal2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCal2.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCal2.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCal2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCal2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCal2.ImageIndex = 3;
		this.CmdCal2.Location = new System.Drawing.Point(336, 236);
		this.CmdCal2.Name = "CmdCal2";
		this.CmdCal2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCal2.Size = new System.Drawing.Size(27, 23);
		this.CmdCal2.TabIndex = 5;
		this.CmdCal2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdCal2, "Get Valid Values if Available");
		this.CmdCal2.UseVisualStyleBackColor = false;
		this.CmdCal1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCal1.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCal1.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCal1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCal1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCal1.ImageIndex = 3;
		this.CmdCal1.Location = new System.Drawing.Point(336, 181);
		this.CmdCal1.Name = "CmdCal1";
		this.CmdCal1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCal1.Size = new System.Drawing.Size(28, 23);
		this.CmdCal1.TabIndex = 2;
		this.CmdCal1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.CmdCal1, "Get Valid Values if Available");
		this.CmdCal1.UseVisualStyleBackColor = false;
		this.MainMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuEditGrdList, this.mnuHelp });
		this.MainMenu1.Location = new System.Drawing.Point(0, 0);
		this.MainMenu1.Name = "MainMenu1";
		this.MainMenu1.Size = new System.Drawing.Size(320, 24);
		this.MainMenu1.TabIndex = 23;
		this.MainMenu1.Visible = false;
		this.mnuEditGrdList.Name = "mnuEditGrdList";
		this.mnuEditGrdList.ShortcutKeys = System.Windows.Forms.Keys.F2;
		this.mnuEditGrdList.Size = new System.Drawing.Size(49, 20);
		this.mnuEditGrdList.Text = "Edit";
		this.mnuEditGrdList.Visible = false;
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.Size = new System.Drawing.Size(55, 20);
		this.mnuHelp.Text = "Help";
		this.mnuHelp.Visible = false;
		this.ContextMenuGrdList.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuGrdList.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuPaste1, this.mnuClear1 });
		this.ContextMenuGrdList.Name = "ContextMenuGrdList";
		this.ContextMenuGrdList.Size = new System.Drawing.Size(421, 52);
		this.mnuPaste1.Name = "mnuPaste1";
		this.mnuPaste1.Size = new System.Drawing.Size(420, 24);
		this.mnuPaste1.Text = "&Paste Clipboard Contents to First Empty Row in Grid";
		this.mnuClear1.Name = "mnuClear1";
		this.mnuClear1.Size = new System.Drawing.Size(420, 24);
		this.mnuClear1.Text = "&Clear the Grid";
		this.cmbColIG.BackColor = System.Drawing.SystemColors.Window;
		this.cmbColIG.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbColIG.DropDownWidth = 190;
		this.cmbColIG.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbColIG.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbColIG.Location = new System.Drawing.Point(0, 235);
		this.cmbColIG.Name = "cmbColIG";
		this.cmbColIG.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbColIG.Size = new System.Drawing.Size(112, 24);
		this.cmbColIG.TabIndex = 4;
		this.cmbColIG.Text = "1";
		this.TxtBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBox2.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtBox2.DropDownWidth = 400;
		this.TxtBox2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBox2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtBox2.Location = new System.Drawing.Point(0, 236);
		this.TxtBox2.Name = "TxtBox2";
		this.TxtBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBox2.Size = new System.Drawing.Size(332, 24);
		this.TxtBox2.TabIndex = 3;
		this.TxtBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBox1.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBox1.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtBox1.DropDownWidth = 400;
		this.TxtBox1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBox1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtBox1.Location = new System.Drawing.Point(0, 181);
		this.TxtBox1.Name = "TxtBox1";
		this.TxtBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBox1.Size = new System.Drawing.Size(332, 24);
		this.TxtBox1.TabIndex = 0;
		this.TxtEscape.AcceptsReturn = true;
		this.TxtEscape.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtEscape.BackColor = System.Drawing.SystemColors.Window;
		this.TxtEscape.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtEscape.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtEscape.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtEscape.Location = new System.Drawing.Point(301, 104);
		this.TxtEscape.MaxLength = 0;
		this.TxtEscape.Name = "TxtEscape";
		this.TxtEscape.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtEscape.Size = new System.Drawing.Size(20, 23);
		this.TxtEscape.TabIndex = 14;
		this.TxtEscape.Visible = false;
		this.CmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.CmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdHelp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdHelp.ImageIndex = 1;
		this.CmdHelp.Location = new System.Drawing.Point(110, 0);
		this.CmdHelp.Name = "CmdHelp";
		this.CmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdHelp.Size = new System.Drawing.Size(52, 40);
		this.CmdHelp.TabIndex = 8;
		this.CmdHelp.Text = "Help";
		this.CmdHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdHelp.UseVisualStyleBackColor = false;
		this.CmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdOK.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdOK.Location = new System.Drawing.Point(0, 0);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdOK.Size = new System.Drawing.Size(52, 40);
		this.CmdOK.TabIndex = 6;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = false;
		this.CmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCancel.Location = new System.Drawing.Point(52, 0);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCancel.Size = new System.Drawing.Size(60, 40);
		this.CmdCancel.TabIndex = 7;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = false;
		this.lblbox1.AutoSize = true;
		this.lblbox1.BackColor = System.Drawing.SystemColors.Control;
		this.lblbox1.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblbox1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblbox1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblbox1.Location = new System.Drawing.Point(1, 163);
		this.lblbox1.Name = "lblbox1";
		this.lblbox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblbox1.Size = new System.Drawing.Size(81, 16);
		this.lblbox1.TabIndex = 22;
		this.lblbox1.Text = "Enter Value";
		this.lblcolIG.AutoSize = true;
		this.lblcolIG.BackColor = System.Drawing.SystemColors.Control;
		this.lblcolIG.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblcolIG.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblcolIG.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblcolIG.Location = new System.Drawing.Point(120, 240);
		this.lblcolIG.Name = "lblcolIG";
		this.lblcolIG.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblcolIG.Size = new System.Drawing.Size(162, 16);
		this.lblcolIG.TabIndex = 18;
		this.lblcolIG.Text = "Col No(s) separated by ;";
		this.lbldesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lbldesc.BackColor = System.Drawing.SystemColors.Control;
		this.lbldesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbldesc.Cursor = System.Windows.Forms.Cursors.Default;
		this.lbldesc.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldesc.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
		this.lbldesc.Location = new System.Drawing.Point(0, 311);
		this.lbldesc.Name = "lbldesc";
		this.lbldesc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbldesc.Size = new System.Drawing.Size(372, 173);
		this.lbldesc.TabIndex = 14;
		this.LblEscape.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblEscape.AutoSize = true;
		this.LblEscape.BackColor = System.Drawing.SystemColors.Control;
		this.LblEscape.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblEscape.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblEscape.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblEscape.Location = new System.Drawing.Point(299, 87);
		this.LblEscape.Name = "LblEscape";
		this.LblEscape.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblEscape.Size = new System.Drawing.Size(55, 16);
		this.LblEscape.TabIndex = 17;
		this.LblEscape.Text = "Escape";
		this.LblEscape.Visible = false;
		this.lblbox2.AutoSize = true;
		this.lblbox2.BackColor = System.Drawing.Color.Transparent;
		this.lblbox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblbox2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblbox2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblbox2.Location = new System.Drawing.Point(1, 220);
		this.lblbox2.Name = "lblbox2";
		this.lblbox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblbox2.Size = new System.Drawing.Size(129, 16);
		this.lblbox2.TabIndex = 16;
		this.lblbox2.Text = "Enter Ending Value";
		this.lblbox2.Visible = false;
		this.lblPrompt.BackColor = System.Drawing.Color.Transparent;
		this.lblPrompt.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblPrompt.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
		this.lblPrompt.Location = new System.Drawing.Point(1, 46);
		this.lblPrompt.Name = "lblPrompt";
		this.lblPrompt.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblPrompt.Size = new System.Drawing.Size(244, 104);
		this.lblPrompt.TabIndex = 15;
		this.lblPrompt.Text = "lblPrompt";
		this.GrdList.AllowUserToResizeColumns = false;
		this.GrdList.AllowUserToResizeRows = false;
		this.GrdList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrdList.BackgroundColor = System.Drawing.Color.White;
		this.GrdList.ColumnHeadersHeight = 4;
		this.GrdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.GrdList.ColumnHeadersVisible = false;
		this.GrdList.Columns.AddRange(this.Column1);
		this.GrdList.Location = new System.Drawing.Point(2, 179);
		this.GrdList.Name = "GrdList";
		this.GrdList.RowHeadersVisible = false;
		this.GrdList.RowHeadersWidth = 4;
		this.GrdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GrdList.RowTemplate.Height = 24;
		this.GrdList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.GrdList.Size = new System.Drawing.Size(328, 113);
		this.GrdList.StandardTab = true;
		this.GrdList.TabIndex = 1;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column1.DefaultCellStyle = dataGridViewCellStyle;
		this.Column1.HeaderText = "";
		this.Column1.MinimumWidth = 6;
		this.Column1.Name = "Column1";
		this.Column1.ReadOnly = true;
		this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column1.Width = 95;
		this.ChkUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ChkUp.AutoSize = true;
		this.ChkUp.Checked = true;
		this.ChkUp.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ChkUp.Location = new System.Drawing.Point(301, 46);
		this.ChkUp.Name = "ChkUp";
		this.ChkUp.Size = new System.Drawing.Size(67, 20);
		this.ChkUp.TabIndex = 13;
		this.ChkUp.Text = "CAPS";
		this.ChkUp.UseVisualStyleBackColor = true;
		this.CmbTxt.FormattingEnabled = true;
		this.CmbTxt.Location = new System.Drawing.Point(0, 269);
		this.CmbTxt.Name = "CmbTxt";
		this.CmbTxt.Size = new System.Drawing.Size(186, 24);
		this.CmbTxt.TabIndex = 27;
		this.CmbTxt.Visible = false;
		this.cmdCloseGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCloseGrid.Location = new System.Drawing.Point(318, 0);
		this.cmdCloseGrid.Name = "cmdCloseGrid";
		this.cmdCloseGrid.Size = new System.Drawing.Size(52, 40);
		this.cmdCloseGrid.TabIndex = 12;
		this.cmdCloseGrid.Text = "Close Grid";
		this.cmdCloseGrid.UseVisualStyleBackColor = false;
		this.cmdCloseGrid.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(372, 485);
		base.Controls.Add(this.cmdCloseGrid);
		base.Controls.Add(this.CmbTxt);
		base.Controls.Add(this.ChkUp);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.MainMenu1);
		base.Controls.Add(this.cmdLoadGrid);
		base.Controls.Add(this.cmdClear);
		base.Controls.Add(this.cmdPaste);
		base.Controls.Add(this.cmbColIG);
		base.Controls.Add(this.TxtBox2);
		base.Controls.Add(this.TxtBox1);
		base.Controls.Add(this.CmdHelp);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.GrdList);
		base.Controls.Add(this.CmdCal2);
		base.Controls.Add(this.CmdCal1);
		base.Controls.Add(this.lblbox1);
		base.Controls.Add(this.lblcolIG);
		base.Controls.Add(this.lbldesc);
		base.Controls.Add(this.lblbox2);
		base.Controls.Add(this.lblPrompt);
		base.Controls.Add(this.TxtEscape);
		base.Controls.Add(this.LblEscape);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.Black;
		base.Location = new System.Drawing.Point(255, 194);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmgetdata";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Prompt For Values";
		this.MainMenu1.ResumeLayout(false);
		this.MainMenu1.PerformLayout();
		this.ContextMenuGrdList.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GrdList).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void set_Button_default()
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
				case 141:
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
							goto IL_0041;
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
					if (!(LikeOperator.LikeString(Strings.LCase(MyOperator), "*in", CompareMethod.Binary) | (Operators.CompareString(Strings.LCase(MyOperator), "like list", TextCompare: false) == 0)))
					{
						break;
					}
					goto IL_0041;
					IL_0041:
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
				try0001_dispatch = 141;
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
				ComboBox comboBox = cmbColIG;
				BuildForm.HG_cmbcolIG_Clk(ref comboBox);
				cmbColIG = comboBox;
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
				case 85:
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
				TxtBox1 = TxtBox;
				cmbColIG = comboBox;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 85;
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
		short num5 = default(short);
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
				case 248:
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
							goto IL_0020;
						case 5:
							goto IL_002e;
						case 4:
						case 6:
						case 7:
							goto IL_0034;
						case 8:
							goto IL_0047;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002e:
					num2 = 5;
					num5 = 2;
					goto IL_0034;
					IL_0034:
					num2 = 7;
					text = Conversions.ToString(lblPrompt.Tag);
					goto IL_0047;
					IL_0020:
					num2 = 3;
					num5 = ll_ObjectType;
					goto IL_0034;
					IL_0047:
					num2 = 8;
					text = General_Procedures.Strip_Alias(1, text);
					break;
					IL_000b:
					num2 = 2;
					if (ll_ObjectType != -99)
					{
						goto IL_0020;
					}
					goto IL_002e;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				string mySQLHelp = MySQLHelp;
				string myDT = MyDT;
				string myOperator = MyOperator;
				ComboBox TxtBox = TxtBox1;
				ComboBox comboBox = cmbColIG;
				Button CmdLoadGrid = cmdLoadGrid;
				BuildForm.HG_Fltr_Hlp_2(mySQLHelp, myDT, myOperator, ref TxtBox, ref comboBox, ref CmdLoadGrid, checked((short)Math.Round(Conversion.Val(RuntimeHelpers.GetObjectValue(base.Tag)))), text, num5);
				cmdLoadGrid = CmdLoadGrid;
				cmbColIG = comboBox;
				TxtBox1 = TxtBox;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 248;
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
				TxtBox2.Text = BuildForm.HG_CmpxpCal(TxtBox2.Text, MyDT, "");
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
				case 83:
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
					Globals_Renamed.currinputstrtmp = "CANCEL";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 83;
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
				mnuClear1_Click(mnuClear1, new EventArgs());
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
		string[] arySrc = default(string[]);
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
						errsource = "frmgetdata - CmdOK_Click";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text = "";
						arySrc = (string[])Utils.CopyArray(arySrc, new string[2]);
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string MyCol = "";
						Globals_Renamed.currinputstrtmp = "";
						if (General_Procedures.IsDateDT(MyDT))
						{
							num6 = (short)Strings.InStr(TxtBox1.Text, "==");
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
						TxtBox1.Text = General_Procedures.StripQuotesAndDates(TxtBox1.Text, MyDT, 0);
						if (LikeOperator.LikeString(Strings.LCase(MyOperator), "in group(*", CompareMethod.Binary))
						{
							MyOperator = "in group(";
						}
						if (Operators.CompareString(ll_Mode, "N", TextCompare: false) == 0)
						{
							string PostCol;
							switch (MyDT)
							{
							case "c":
							case "q":
							case "x":
								text2 = "'";
								PostCol = "'";
								break;
							default:
								text2 = "";
								PostCol = "";
								break;
							}
							switch (MyOperator)
							{
							case "between":
							case "not between":
							case "(+) between":
							case "(+) not between":
								TxtBox2.Text = General_Procedures.StripQuotesAndDates(TxtBox2.Text, MyDT, 0);
								BuildForm.Assign_Pre_Post_Col(2, MyDT, TxtBox1.Text, ref text2, ref PostCol);
								Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + text2 + TxtBox1.Text + PostCol + " AND ";
								BuildForm.Assign_Pre_Post_Col(2, MyDT, TxtBox2.Text, ref text2, ref PostCol);
								Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + text2 + TxtBox2.Text + PostCol;
								break;
							case "$between":
							case "$not $between":
								TxtBox2.Text = General_Procedures.StripQuotesAndDates(TxtBox2.Text, MyDT, 0);
								BuildForm.Assign_Pre_Post_Col(2, MyDT, TxtBox1.Text, ref text2, ref PostCol);
								Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + text2 + TxtBox1.Text + PostCol;
								if (Operators.CompareString(MyOperator, "$between", TextCompare: false) == 0)
								{
									Globals_Renamed.currinputstrtmp += ", '$lte' : ";
								}
								else
								{
									Globals_Renamed.currinputstrtmp += ", '$gt' : ";
								}
								BuildForm.Assign_Pre_Post_Col(2, MyDT, TxtBox2.Text, ref text2, ref PostCol);
								Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + text2 + TxtBox2.Text + PostCol;
								break;
							case "like":
							case "not like":
							case "(+) like":
							case "(+) not like":
								if ((Operators.CompareString(Strings.Trim(TxtEscape.Text), "", TextCompare: false) == 0) & (Strings.InStr(TxtBox1.Text, "%") == 0) & (Strings.InStr(TxtBox1.Text, "_") == 0))
								{
									TxtBox1.Text += "%";
								}
								Globals_Renamed.currinputstrtmp = text2 + TxtBox1.Text + PostCol;
								if (Operators.CompareString(Strings.Trim(TxtEscape.Text), "", TextCompare: false) != 0)
								{
									Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + " ESCAPE '" + Strings.Trim(TxtEscape.Text) + "'";
								}
								break;
							case "is null":
							case "is not null":
								Globals_Renamed.currinputstrtmp = "";
								break;
							case "$in":
							case "$not $in":
							case "regex list":
							case "not regex list":
							{
								text = ((!LikeOperator.LikeString(MyOperator, "*in*", CompareMethod.Binary)) ? "" : "\r\n    ");
								short num8 = (short)(GrdList.RowCount - 1);
								for (num5 = 0; num5 <= num8; num5 = (short)unchecked(num5 + 1))
								{
									if (Operators.ConditionalCompareObjectNotEqual(GrdList.Rows[num5].Cells[0].Value, "", TextCompare: false))
									{
										BuildForm.Assign_Pre_Post_Col(1, MyDT, Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ref text2, ref PostCol);
										text3 = ((Operators.CompareString(text3, "", TextCompare: false) != 0) ? (text3 + text + "," + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol) : (text + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol));
									}
								}
								if (LikeOperator.LikeString(MyOperator, "*regex*", CompareMethod.Binary))
								{
									MyCol = Conversions.ToString(lblPrompt.Tag);
									text3 = BuildForm.Process_Regex_List(MyCol, text3);
								}
								Globals_Renamed.currinputstrtmp = text3;
								break;
							}
							case "in":
							case "not in":
							case "like list":
							case "(+) in":
							{
								text3 = ((Operators.CompareString(MyOperator, "like list", TextCompare: false) != 0) ? "(" : "");
								short num7 = (short)(GrdList.RowCount - 1);
								for (num5 = 0; num5 <= num7; num5 = (short)unchecked(num5 + 1))
								{
									if (Operators.ConditionalCompareObjectNotEqual(GrdList.Rows[num5].Cells[0].Value, "", TextCompare: false))
									{
										BuildForm.Assign_Pre_Post_Col(1, MyDT, Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ref text2, ref PostCol);
										if (Operators.CompareString(text3, "(", TextCompare: false) == 0)
										{
											if (gSpecialTest == 0 || gSpecialTest == 4 || gSpecialTest == 5)
											{
												text3 = text3 + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol;
											}
											else
											{
												num6 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ",");
												if (num6 == 0)
												{
													GrdList.Rows[num5].Cells[0].Value = Operators.ConcatenateObject(GrdList.Rows[num5].Cells[0].Value, ",");
													num6 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ",");
												}
												text3 = "((" + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), 1, num6 - 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol + "," + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), num6 + 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol + ")";
											}
										}
										else if (gSpecialTest == 0)
										{
											text3 = text3 + "," + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol;
										}
										else if (gSpecialTest == 4 || gSpecialTest == 5)
										{
											text3 = text3 + ";" + text2 + General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol;
										}
										else
										{
											num6 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ",");
											if (num6 == 0)
											{
												GrdList.Rows[num5].Cells[0].Value = Operators.ConcatenateObject(GrdList.Rows[num5].Cells[0].Value, ",");
												num6 = (short)Strings.InStr(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), ",");
											}
											text3 = text3 + ",(" + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), 1, num6 - 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol + "," + text2 + General_Procedures.StripQuotesAndDates(Strings.Mid(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), num6 + 1), Globals_Renamed.currdatatypetmp, gSpecialTest) + PostCol + ")";
										}
									}
								}
								if (Operators.CompareString(MyOperator, "like list", TextCompare: false) == 0)
								{
									MyCol = Conversions.ToString(lblPrompt.Tag);
									text3 = BuildForm.Process_Like_List(MyCol, text3, Strings.Trim(TxtEscape.Text));
								}
								else
								{
									text3 += ")";
								}
								Globals_Renamed.currinputstrtmp = text3;
								break;
							}
							case "in group":
							case "like group":
							case "in group (":
								text4 = Strings.Trim(TxtBox1.Text);
								if (Operators.CompareString(text4, "", TextCompare: false) != 0)
								{
									text4 = text4 + ":" + Strings.Trim(cmbColIG.Text);
									BuildForm.Extract_IG_ColNo(ref text4, ref MyCol);
								}
								if (Operators.CompareString(MyOperator, "in group(", TextCompare: false) == 0)
								{
									Globals_Renamed.currinputstrtmp = text2 + text4 + "\",\"" + MyCol + PostCol;
								}
								else if (Operators.CompareString(MyOperator, "like group", TextCompare: false) == 0)
								{
									text = ((Operators.CompareString(Strings.Trim(TxtEscape.Text), "", TextCompare: false) == 0) ? "" : ("@esc@=" + TxtEscape.Text));
									Globals_Renamed.currinputstrtmp = text2 + text4 + "\",\"" + MyCol + text + PostCol;
								}
								else
								{
									Globals_Renamed.currinputstrtmp = text2 + text4 + "\",\"" + MyCol + PostCol;
								}
								break;
							default:
								BuildForm.Assign_Pre_Post_Col(2, MyDT, TxtBox1.Text, ref text2, ref PostCol);
								Globals_Renamed.currinputstrtmp = text2 + TxtBox1.Text + PostCol;
								break;
							}
							arySrc = null;
						}
						else
						{
							switch (MyOperator)
							{
							case "in":
							case "like list":
							{
								short num9 = (short)(GrdList.RowCount - 1);
								for (num5 = 0; num5 <= num9; num5 = (short)unchecked(num5 + 1))
								{
									if (Operators.CompareString(Strings.Trim(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value)), "", TextCompare: false) != 0)
									{
										Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + ";" + Strings.Trim(General_Procedures.StripQuotesAndDates(Conversions.ToString(GrdList.Rows[num5].Cells[0].Value), MyDT, 0));
									}
								}
								if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "", TextCompare: false) != 0)
								{
									Globals_Renamed.currinputstrtmp = Strings.Mid(Globals_Renamed.currinputstrtmp, 2);
								}
								break;
							}
							case "between":
								Globals_Renamed.currinputstrtmp = TxtBox1.Text + ";" + General_Procedures.StripQuotesAndDates(TxtBox2.Text, MyDT, 0);
								break;
							case "in group":
							case "like group":
							case "in group (":
								text4 = Strings.Trim(TxtBox1.Text);
								if (Operators.CompareString(text4, "", TextCompare: false) != 0)
								{
									text4 = text4 + ":" + Strings.Trim(cmbColIG.Text);
									BuildForm.Extract_IG_ColNo(ref text4, ref MyCol);
								}
								Globals_Renamed.currinputstrtmp = text4 + ";" + MyCol;
								break;
							default:
								Globals_Renamed.currinputstrtmp = TxtBox1.Text;
								break;
							}
						}
						Globals_Renamed.currinputstrtmp = BuildForm.Replace_Globals(Globals_Renamed.currinputstrtmp);
						Close();
						goto end_IL_0001;
					}
					case 5009:
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
					goto IL_13c7;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5009;
				continue;
			}
			break;
			IL_13c7:
			throw ProjectData.CreateProjectError(-2146828237);
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
				mnuPaste1_Click(mnuPaste1, new EventArgs());
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

	private void frmgetdata_Load(object eventSender, EventArgs eventArgs)
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
						errsource = "frmGetData - Form_Load";
						Button MyButton = CmdHelp;
						BuildForm.Set_Btn_Img(ref MyButton, "helpb");
						CmdHelp = MyButton;
						MyButton = cmdClear;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						cmdClear = MyButton;
						MyButton = cmdPaste;
						BuildForm.Set_Btn_Img(ref MyButton, "paste_color");
						cmdPaste = MyButton;
						MyButton = CmdCal1;
						BuildForm.Set_Btn_Img(ref MyButton, "helpy2");
						CmdCal1 = MyButton;
						MyButton = CmdCal2;
						BuildForm.Set_Btn_Img(ref MyButton, "helpy2");
						CmdCal2 = MyButton;
						int num3 = 0;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						string text = "";
						int num4 = 0;
						string text2 = "";
						short num5 = 0;
						string text3 = "";
						string text4 = "";
						GrdList.RowCount = 1000;
						GrdList.Columns[0].Width = GrdList.Width;
						string text5 = "0";
						base.Top = (int)Math.Round((double)Screen.PrimaryScreen.Bounds.Height / 2.0 - (double)base.Height / 2.0);
						Form Form_Name = this;
						BuildForm.Set_Form_Position(ref Form_Name);
						ChkUp.Left = TxtEscape.Left;
						num3 = General_Procedures.ParseAndFillArray(Globals_Renamed.currinputstrtmp, "^", ref DynArray);
						MyColumn = Strings.LCase(DynArray[2]);
						if ((Operators.CompareString(Strings.Mid(MyColumn, 1, 1), "(", TextCompare: false) == 0) & (Strings.InStr(MyColumn, ")") != 0) & (Strings.InStr(MyColumn, "test_name,") != 0) & (Strings.InStr(MyColumn, "structure_name)") != 0))
						{
							gSpecialTest = 1;
						}
						else if ((Operators.CompareString(Strings.Mid(MyColumn, 1, 1), "(", TextCompare: false) == 0) & (Strings.InStr(MyColumn, ")") != 0) & (Strings.InStr(MyColumn, "lot,") != 0) & (Strings.InStr(MyColumn, "wafer") != 0) & (Strings.InStr(MyColumn, ",") != 0))
						{
							gSpecialTest = 2;
						}
						else if (Operators.CompareString(Strings.LCase(MyColumn), "xxx.l7;xxx.w3;xxx.x;xxx.y", TextCompare: false) == 0)
						{
							gSpecialTest = 4;
						}
						else if (LikeOperator.LikeString(Strings.LCase(MyColumn), "*sort_lot_wafer3_filteronly", CompareMethod.Binary))
						{
							gSpecialTest = 5;
						}
						else
						{
							gSpecialTest = 0;
						}
						MyOperator = Strings.LCase(DynArray[3]);
						MyDT = Strings.LCase(DynArray[4]);
						MyPrompt = DynArray[5];
						MySQLHelp = DynArray[6];
						MySQLHelp = BuildForm.Replace_Amp_Carat(MySQLHelp, "D");
						text2 = DynArray[7];
						Text = Text + " (" + MyOperator + ")";
						lblPrompt.Text = "Enter " + MyPrompt + " (" + MyOperator + ")";
						lblPrompt.Tag = MyColumn;
						Set_Row_Filter_Controls("SET");
						if (Operators.CompareString(Strings.Mid(MySQLHelp, 1, 1), "?", TextCompare: false) == 0)
						{
							if (BuildForm.FormIsLoaded("FrmSQLQuerya", 1) | (Globals_Renamed.Design_Mode != 0))
							{
								if (LikeOperator.LikeString(MySQLHelp, "?:{*}:*", CompareMethod.Binary))
								{
									num5 = (short)Strings.InStrRev(MySQLHelp, ":");
									text3 = Strings.Trim(Strings.Mid(MySQLHelp, num5 + 1));
									text5 = Strings.Mid(MySQLHelp, 1, num5 - 1);
									text5 = (Globals_Renamed.g_LastTblIdx = General_Procedures.Get_Node_Value(text5));
								}
								else if (Operators.CompareString(MySQLHelp, "?", TextCompare: false) == 0)
								{
									text5 = Globals_Renamed.g_LastTblIdx;
									text3 = "";
								}
								else
								{
									text5 = "0";
									text3 = Strings.Mid(MySQLHelp, 2);
								}
								if (Operators.CompareString(text5, "", TextCompare: false) == 0)
								{
									MySQLHelp = "";
								}
								else
								{
									MySQLHelp = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Column_Help(MyColumn, text3, (short)Math.Round(Conversion.Val(text5)));
								}
							}
							else
							{
								MySQLHelp = "";
							}
						}
						CmbTxt.Items.Clear();
						if (Operators.CompareString(Strings.Mid(MySQLHelp, 1, 1), "~", TextCompare: false) == 0)
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							text4 = "/";
							if (General_Procedures.IsDateDT(MyDT))
							{
								text4 = "//";
							}
							num3 = General_Procedures.ParseAndFillArray(MySQLHelp, text4, ref DynArray);
							text4 = "";
							DynArray[1] = Strings.Mid(DynArray[1], 2);
							int num6 = num3;
							for (num4 = 1; num4 <= num6; num4++)
							{
								TxtBox1.Items.Add(DynArray[num4]);
								TxtBox2.Items.Add(DynArray[num4]);
								CmbTxt.Items.Add(DynArray[num4]);
							}
							cmdLoadGrid.Enabled = false;
						}
						DynArray = null;
						if (Operators.CompareString(MyDT, "d", TextCompare: false) == 0)
						{
							int gNoOperators = Globals_Renamed.gNoOperators;
							for (num4 = 0; num4 <= gNoOperators; num4++)
							{
								if (Operators.CompareString(Globals_Renamed.gOperators[num4].DB_Type, "DO", TextCompare: false) == 0)
								{
									TxtBox1.Items.Add(Globals_Renamed.gOperators[num4].Name);
									TxtBox2.Items.Add(Globals_Renamed.gOperators[num4].Name);
								}
							}
						}
						else if (General_Procedures.IsDateDT(MyDT, 1))
						{
							text4 = ((Operators.CompareString(MyDT, "g", TextCompare: false) == 0) ? "DT" : ((Operators.CompareString(MyDT, "h", TextCompare: false) == 0) ? "DQ" : ((Operators.CompareString(MyDT, "u", TextCompare: false) == 0) ? "DU" : ((Operators.CompareString(MyDT, "m", TextCompare: false) == 0) ? "DM" : ((Operators.CompareString(MyDT, "s", TextCompare: false) == 0) ? "DN" : ((Operators.CompareString(MyDT, "o", TextCompare: false) == 0) ? "DM2" : ((Operators.CompareString(MyDT, "l", TextCompare: false) == 0) ? "DL" : ((Operators.CompareString(MyDT, "p", TextCompare: false) == 0) ? "DP" : ((Operators.CompareString(MyDT, "j", TextCompare: false) == 0) ? "DJ" : ((Operators.CompareString(MyDT, "k", TextCompare: false) == 0) ? "DK" : ((Operators.CompareString(MyDT, "v", TextCompare: false) == 0) ? "DV" : ((Operators.CompareString(MyDT, "w", TextCompare: false) != 0) ? "DS" : "DW"))))))))))));
							int gNoOperators2 = Globals_Renamed.gNoOperators;
							for (num4 = 0; num4 <= gNoOperators2; num4++)
							{
								if (Operators.CompareString(Globals_Renamed.gOperators[num4].DB_Type, text4, TextCompare: false) == 0)
								{
									TxtBox1.Items.Add(Globals_Renamed.gOperators[num4].Name);
									TxtBox2.Items.Add(Globals_Renamed.gOperators[num4].Name);
								}
							}
						}
						else if (Operators.CompareString(MyDT, "s", TextCompare: false) == 0)
						{
							int gNoOperators3 = Globals_Renamed.gNoOperators;
							for (num4 = 0; num4 <= gNoOperators3; num4++)
							{
								if (Operators.CompareString(Globals_Renamed.gOperators[num4].DB_Type, "DN", TextCompare: false) == 0)
								{
									TxtBox1.Items.Add(Globals_Renamed.gOperators[num4].Name);
									TxtBox2.Items.Add(Globals_Renamed.gOperators[num4].Name);
								}
							}
						}
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							if (Operators.CompareString(MyOperator, "between", TextCompare: false) == 0)
							{
								num5 = (short)Strings.InStr(text2, ";");
								if (num5 != 0)
								{
									TxtBox1.Text = Strings.Trim(Strings.Mid(text2, 1, num5 - 1));
									TxtBox2.Text = Strings.Trim(Strings.Mid(text2, num5 + 1));
								}
								else
								{
									TxtBox1.Text = text2;
								}
							}
							else if ((Operators.CompareString(MyOperator, "in", TextCompare: false) == 0) | (Operators.CompareString(MyOperator, "not in", TextCompare: false) == 0) | (Operators.CompareString(MyOperator, "like list", TextCompare: false) == 0) | (Operators.CompareString(MyOperator, "(+) in", TextCompare: false) == 0))
							{
								GrdList.Rows[0].Cells[0].Value = text2;
							}
							else
							{
								TxtBox1.Text = text2;
							}
						}
						if (MyColumn.Length >= 16 && Operators.CompareString(MyColumn.Substring(MyColumn.Length - 16), "[$ep_$raw_$date]", TextCompare: false) == 0 && Operators.CompareString(MyOperator, "between", TextCompare: false) == 0 && Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							TxtBox1.Text = "000000000000";
							TxtBox2.Text = "30001231235959";
							TxtBox1.Items.Add(DateAndTime.Now.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")).Trim() + "000000");
							TxtBox1.Items.Add(DateTime.FromOADate(DateAndTime.Now.ToOADate() - 1.0).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) + "000000");
							TxtBox1.Items.Add(DateTime.FromOADate(DateAndTime.Now.ToOADate() - 7.0).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) + "000000");
							TxtBox1.Items.Add(DateAndTime.Now.ToString("yyyyMM", CultureInfo.CreateSpecificCulture("en-US")).Trim() + "01000000");
							TxtBox2.Items.Add("30001231235959");
							TxtBox2.Items.Add(DateAndTime.Now.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")).Trim() + "235959");
							TxtBox2.Items.Add(DateTime.FromOADate(DateAndTime.Now.ToOADate() - 1.0).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) + "235959");
							TxtBox2.Items.Add(DateTime.FromOADate(DateAndTime.Now.ToOADate() - 7.0).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) + "235959");
						}
						set_Button_default();
						if (Operators.CompareString(MyDT, "d", TextCompare: false) == 0 || Operators.CompareString(MyDT, "v", TextCompare: false) == 0)
						{
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text = "01-" + DateAndTime.Now.ToString("MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")) + " 00:00:00";
								TxtBox1.Text = text;
							}
							else
							{
								TxtBox1.Text = text2;
							}
							TxtBox2.Text = DateAndTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
						}
						else if (General_Procedures.IsDateDT(MyDT, 1))
						{
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text = DateAndTime.Now.ToString("yyyy-MM", CultureInfo.CreateSpecificCulture("en-US")).Trim() + "-01 00:00:00";
								TxtBox1.Text = text;
							}
							else
							{
								TxtBox1.Text = text2;
							}
							TxtBox2.Text = DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
						}
						else if (Operators.CompareString(MyDT, "s", TextCompare: false) == 0)
						{
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text = DateAndTime.Now.ToString("yyyyMM", CultureInfo.CreateSpecificCulture("en-US")).Trim() + "01000000";
								TxtBox1.Text = text;
							}
							else
							{
								TxtBox1.Text = text2;
							}
							TxtBox2.Text = DateAndTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CreateSpecificCulture("en-US"));
						}
						lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(Strings.Replace(MyOperator, "$", "", 1, -1, CompareMethod.Text));
						Cursor.Current = Cursors.Default;
						DynArray = null;
						if (Operators.CompareString(ll_Mode, "I", TextCompare: false) == 0 && Operators.CompareString(ll_Tag, "", TextCompare: false) != 0)
						{
							switch (MyOperator)
							{
							case "between":
							case "in group":
							case "like group":
							case "in group (":
								DynArray = Strings.Split(ll_Tag, ";");
								TxtBox1.Text = DynArray[0];
								if (Information.UBound(DynArray) >= 1)
								{
									if (Operators.CompareString(MyOperator, "between", TextCompare: false) == 0)
									{
										TxtBox2.Text = DynArray[1];
									}
									else if (Versioned.IsNumeric(DynArray[1]))
									{
										cmbColIG.Text = DynArray[1];
									}
								}
								DynArray = null;
								break;
							case "in":
							case "like list":
							{
								DynArray = Strings.Split(ll_Tag, ";");
								int num7 = Information.UBound(DynArray);
								for (num4 = 0; num4 <= num7; num4++)
								{
									if (Operators.CompareString(Strings.Trim(DynArray[num4]), "", TextCompare: false) != 0)
									{
										GrdList.Rows[num4].Cells[0].Value = DynArray[num4];
									}
								}
								DynArray = null;
								break;
							}
							default:
								TxtBox1.Text = ll_Tag;
								break;
							}
						}
						BuildForm.Invoke_CSVViewer3(0, "HELP2");
						Globals_Renamed.currinputstrtmp = "CANCEL";
						goto end_IL_0001;
					}
					case 4562:
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
					goto IL_1208;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4562;
				continue;
			}
			break;
			IL_1208:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmgetdata_FormClosing(object eventSender, FormClosingEventArgs eventArgs)
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
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					cancel = eventArgs.Cancel;
					goto IL_000b;
				case 110:
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
							goto IL_0028;
						case 5:
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
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
					if (eventArgs.CloseReason == CloseReason.UserClosing)
					{
						break;
					}
					goto IL_0028;
					IL_0028:
					num2 = 4;
					Globals_Renamed.currinputstrtmp = "CANCEL";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				eventArgs.Cancel = cancel;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 110;
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

	private void frmgetdata_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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

	private void GrdList_Scroll(object eventSender, EventArgs eventArgs)
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

	private void Set_Row_Filter_Controls(string MyOption)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
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
					text = "frmgetdata - Set_Row_Filter_Controls";
					text2 = "";
					short num3 = 0;
					string text3 = "";
					string text4 = "yyyy-mm-dd hh24:mi:ss";
					text3 = MyOperator;
					string left = Strings.UCase(MyOption);
					if (Operators.CompareString(left, "CLEAR", TextCompare: false) != 0)
					{
						if (Operators.CompareString(left, "SET", TextCompare: false) != 0)
						{
							goto end_IL_0001;
						}
						text2 = " - Case = SET";
						if (Strings.InStr(text3, "(+)") != 0)
						{
							text3 = Strings.Mid(text3, 5);
						}
						if (Operators.CompareString(Strings.UCase(Strings.Mid(text3 + "    ", 1, 4)), "NOT ", TextCompare: false) == 0)
						{
							text3 = Strings.Mid(text3, 5);
						}
						lblcolIG.Visible = false;
						cmbColIG.Visible = false;
						cmdPaste.Enabled = false;
						cmdClear.Enabled = false;
						switch (MyDT)
						{
						case "d":
						case "t":
						case "g":
						case "h":
						case "u":
						case "m":
						case "o":
						case "l":
						case "p":
						case "v":
						case "s":
						case "w":
						case "j":
						case "k":
							cmdLoadGrid.Enabled = false;
							break;
						}
						switch (Strings.UCase(text3))
						{
						case "BETWEEN":
						case "$BETWEEN":
						case "$NOT $BETWEEN":
							text2 = " - Case = SET ; BETWEEN";
							switch (MyDT)
							{
							case "d":
							case "t":
							case "g":
							case "h":
							case "u":
							case "m":
							case "o":
							case "l":
							case "p":
							case "v":
							case "w":
							case "j":
							case "k":
								CmdCal1.Visible = true;
								CmdCal2.Visible = true;
								if (Operators.CompareString(MyDT, "d", TextCompare: false) == 0 || Operators.CompareString(MyDT, "v", TextCompare: false) == 0)
								{
									text4 = "dd-mon-yyyy hh24:mi:ss";
								}
								lblbox1.Text = "Enter Start Date (" + text4 + ")";
								lblbox2.Text = "Enter End Date (" + text4 + ")";
								break;
							default:
								CmdCal2.Visible = false;
								lblbox1.Text = "Enter Starting Value";
								lblbox2.Text = "Enter Ending Value";
								break;
							}
							TxtBox1.Visible = true;
							TxtBox2.Visible = true;
							lblbox1.Visible = true;
							lblbox2.Visible = true;
							GrdList.Visible = false;
							LblEscape.Visible = false;
							TxtEscape.Visible = false;
							break;
						case "IN":
						case "LIKE LIST":
						case "(+) IN":
						case "$IN":
						case "$NOT $IN":
						case "REGEX LIST":
						case "NOT REGEX LIST":
							text2 = " - Case = SET ; IN";
							CmdCal2.Visible = false;
							GrdList.Visible = true;
							TxtBox1.Visible = false;
							TxtBox2.Visible = false;
							lblbox1.Visible = true;
							lblbox1.Text = "Enter Value(s)";
							if (gSpecialTest == 1)
							{
								lblbox1.Text = "Enter Test Name and Structure separated by commas";
							}
							else if (gSpecialTest == 2)
							{
								lblbox1.Text = "Enter Lot and Wafer ID separated by commas";
							}
							else if (gSpecialTest == 4)
							{
								lblbox1.Text = "Enter Lot7, 3 digit wafer, X and Y site separated by commas";
							}
							else if (gSpecialTest == 5)
							{
								lblbox1.Text = "Enter Lot with wildcard and wafer3 separated by commas";
							}
							lblbox2.Visible = false;
							if (Operators.CompareString(Strings.UCase(MyOperator), "LIKE LIST", TextCompare: false) == 0)
							{
								LblEscape.Visible = true;
								TxtEscape.Visible = true;
								GrdList.Rows[0].Cells[0].Value = "%";
							}
							else
							{
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
							}
							cmdPaste.Enabled = true;
							cmdClear.Enabled = true;
							break;
						case "LIKE":
							text2 = " - Case = SET ; LIKE";
							CmdCal2.Visible = false;
							GrdList.Visible = false;
							TxtBox1.Visible = true;
							TxtBox2.Visible = false;
							lblbox1.Visible = true;
							lblbox1.Text = "Enter Pattern and Optional Escape Char";
							lblbox2.Visible = false;
							LblEscape.Visible = true;
							TxtEscape.Visible = true;
							break;
						default:
						{
							text2 = " - Case = SET ; Else";
							string myDT = MyDT;
							if (Operators.CompareString(myDT, "d", TextCompare: false) == 0 || Operators.CompareString(myDT, "v", TextCompare: false) == 0)
							{
								CmdCal1.Visible = true;
								lblbox1.Text = "Enter Date in format dd-Mon-yyyy hh24:mi:ss";
							}
							if (gSpecialTest == 1)
							{
								lblbox1.Text = "Enter Test Name and Structure separated by commas";
							}
							else if (gSpecialTest == 2)
							{
								lblbox1.Text = "Enter Lot and Wafer ID separated by commas";
							}
							else if (gSpecialTest == 4)
							{
								lblbox1.Text = "Enter Lot7, 3 digit wafer, X and Y site separated by commas";
							}
							else if (gSpecialTest == 5)
							{
								lblbox1.Text = "Enter Lot with wildcard and wafer3 separated by commas";
							}
							CmdCal2.Visible = false;
							GrdList.Visible = false;
							TxtBox1.Visible = true;
							TxtBox2.Visible = false;
							lblbox1.Visible = true;
							lblbox2.Visible = false;
							if (Operators.CompareString(Strings.UCase(MyOperator), "LIKE GROUP", TextCompare: false) == 0)
							{
								LblEscape.Visible = true;
								TxtEscape.Visible = true;
							}
							else
							{
								LblEscape.Visible = false;
								TxtEscape.Visible = false;
							}
							if (LikeOperator.LikeString(Strings.UCase(text3), "* GROUP*", CompareMethod.Binary))
							{
								lblcolIG.Visible = true;
								cmbColIG.Visible = true;
							}
							break;
						}
						}
						goto end_IL_0001;
					}
					text2 = " - Case = CLEAR";
					CmdCal1.Visible = false;
					CmdCal2.Visible = false;
					GrdList.Visible = false;
					TxtBox1.Visible = false;
					TxtBox2.Visible = false;
					lblbox1.Visible = false;
					lblbox2.Visible = false;
					LblEscape.Visible = false;
					TxtEscape.Visible = false;
					lblcolIG.Visible = false;
					cmbColIG.Visible = false;
					cmdPaste.Enabled = false;
					cmdClear.Enabled = false;
					goto end_IL_0001_2;
				}
				case 3297:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, text + text2, Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_0d17;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3297;
				continue;
			}
			break;
			IL_0d17:
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

	private void txtbox1_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
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
				case 150:
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
							goto IL_0030;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_0048;
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
					IL_0030:
					num2 = 4;
					num5 = General_Procedures.ToUpper(num5);
					goto IL_0039;
					IL_0039:
					num2 = 5;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_0048;
					IL_001a:
					num2 = 3;
					if (ChkUp.CheckState == CheckState.Checked)
					{
						goto IL_0030;
					}
					goto IL_0039;
					IL_0048:
					num2 = 6;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.Asc(eventArgs.KeyChar));
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
				try0001_dispatch = 150;
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
				case 150:
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
							goto IL_0030;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_0048;
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
					IL_0030:
					num2 = 4;
					num5 = General_Procedures.ToUpper(num5);
					goto IL_0039;
					IL_0039:
					num2 = 5;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_0048;
					IL_001a:
					num2 = 3;
					if (ChkUp.CheckState == CheckState.Checked)
					{
						goto IL_0030;
					}
					goto IL_0039;
					IL_0048:
					num2 = 6;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.Asc(eventArgs.KeyChar));
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
				try0001_dispatch = 150;
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

	private void mnuPaste1_Click(object sender, EventArgs e)
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

	private void mnuClear1_Click(object sender, EventArgs e)
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
				DataGridView Grid = GrdList;
				BuildForm.HG_mnuClear2(ref Grid);
				GrdList = Grid;
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
					GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 1);
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
				BuildForm.HG_GrdList_KP(ref GrdList, ref CmbTxt, ref e, ChkUp.Checked, ref CmbBoxKey, ref f_TrueRow, ref f_Row, ref f_Col);
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
					if (ChkUp.CheckState != CheckState.Checked)
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
					errsource = "FrmGetData - CmbTxt_Leave";
					ProjectData.ClearProjectError();
					num2 = 2;
					GrdList.Rows[f_Row].Cells[f_Col].Value = CmbTxt.Text;
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

	private void GrdList_Scroll1(object sender, ScrollEventArgs e)
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

	private void ChkUp_KeyPress(object sender, KeyPressEventArgs e)
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

	private void ChkUp_GotFocus(object sender, EventArgs e)
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

	private void ChkUp_LostFocus(object sender, EventArgs e)
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
				set_Button_default();
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
}

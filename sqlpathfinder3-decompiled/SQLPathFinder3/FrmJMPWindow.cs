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
public class FrmJMPWindow : Form
{
	private IContainer components;

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
	[AccessedThroughProperty("LstChart")]
	private ListBox _LstChart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse2")]
	private Button _CmdBrowse2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse1")]
	private Button _CmdBrowse1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRR")]
	private ToolStripMenuItem _mnuRR;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("optPP")]
	private RadioButton _optPP;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("optSP")]
	private RadioButton _optSP;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("optemail")]
	private RadioButton _optemail;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("optfile")]
	private RadioButton _optfile;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse3")]
	private Button _CmdBrowse3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridChart")]
	private DataGridView _GridChart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridFilter")]
	private DataGridView _GridFilter;

	private string MyDragSource;

	public string f_JSLScript;

	public string f_ScriptType;

	public string f_Charts;

	public string f_ChartsActual;

	public string MyTableData;

	private const string DLMObj = "@@@";

	private const string DLMCols = ";";

	private const string DLMRows = "|";

	private const int ActualCount = 1000;

	private string[] l_ChartsActual;

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblChart")]
	internal virtual Label LblChart
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

	internal virtual ListBox LstChart
	{
		[CompilerGenerated]
		get
		{
			return _LstChart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DragEventHandler value2 = LstChart_DragDrop;
			DragEventHandler value3 = LstChart_DragEnter;
			MouseEventHandler value4 = LstChart_MouseDown;
			ListBox listBox = _LstChart;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragEnter -= value3;
				listBox.MouseDown -= value4;
			}
			_LstChart = value;
			listBox = _LstChart;
			if (listBox != null)
			{
				listBox.DragDrop += value2;
				listBox.DragEnter += value3;
				listBox.MouseDown += value4;
			}
		}
	}

	[field: AccessedThroughProperty("LblTitle")]
	internal virtual Label LblTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkReverse")]
	internal virtual CheckBox ChkReverse
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHTMOut")]
	internal virtual Label LblHTMOut
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdBrowse2
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse2_Click;
			Button button = _CmdBrowse2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse2 = value;
			button = _CmdBrowse2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdBrowse1
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse1_Click;
			Button button = _CmdBrowse1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse1 = value;
			button = _CmdBrowse1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtCSSOut")]
	internal virtual TextBox TxtCSSOut
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtHTMOut")]
	internal virtual TextBox TxtHTMOut
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHTMCSS")]
	internal virtual Label LblHTMCSS
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

	[field: AccessedThroughProperty("mnuRClick")]
	internal virtual ContextMenuStrip mnuRClick
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuColSpan")]
	internal virtual ToolStripComboBox mnuColSpan
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkEmbedCSS")]
	internal virtual CheckBox ChkEmbedCSS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuCW")]
	internal virtual MenuStrip mnuCW
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

	internal virtual ToolStripMenuItem mnuRR
	{
		[CompilerGenerated]
		get
		{
			return _mnuRR;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRR;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRR = value;
			toolStripMenuItem = _mnuRR;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual RadioButton optPP
	{
		[CompilerGenerated]
		get
		{
			return _optPP;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = optfile_Click;
			RadioButton radioButton = _optPP;
			if (radioButton != null)
			{
				radioButton.Click -= value2;
			}
			_optPP = value;
			radioButton = _optPP;
			if (radioButton != null)
			{
				radioButton.Click += value2;
			}
		}
	}

	internal virtual RadioButton optSP
	{
		[CompilerGenerated]
		get
		{
			return _optSP;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = optfile_Click;
			RadioButton radioButton = _optSP;
			if (radioButton != null)
			{
				radioButton.Click -= value2;
			}
			_optSP = value;
			radioButton = _optSP;
			if (radioButton != null)
			{
				radioButton.Click += value2;
			}
		}
	}

	internal virtual RadioButton optemail
	{
		[CompilerGenerated]
		get
		{
			return _optemail;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = optfile_Click;
			RadioButton radioButton = _optemail;
			if (radioButton != null)
			{
				radioButton.Click -= value2;
			}
			_optemail = value;
			radioButton = _optemail;
			if (radioButton != null)
			{
				radioButton.Click += value2;
			}
		}
	}

	internal virtual RadioButton optfile
	{
		[CompilerGenerated]
		get
		{
			return _optfile;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = optfile_Click;
			RadioButton radioButton = _optfile;
			if (radioButton != null)
			{
				radioButton.Click -= value2;
			}
			_optfile = value;
			radioButton = _optfile;
			if (radioButton != null)
			{
				radioButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnudisplaywin")]
	internal virtual ToolStripMenuItem mnudisplaywin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblAttach")]
	internal virtual Label lblAttach
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblrole")]
	internal virtual Label lblrole
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdBrowse3
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse3_Click;
			Button button = _CmdBrowse3;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse3 = value;
			button = _CmdBrowse3;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtAttach")]
	internal virtual TextBox TxtAttach
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbrole")]
	internal virtual ComboBox cmbrole
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SplitContainer2")]
	internal virtual SplitContainer SplitContainer2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridChart
	{
		[CompilerGenerated]
		get
		{
			return _GridChart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GridChart_DoubleClick;
			DragEventHandler value3 = GridChart_DragDrop;
			DragEventHandler value4 = GridChart_DragEnter;
			DragEventHandler value5 = GridChart_DragOver;
			MouseEventHandler value6 = GridChart_MouseDown;
			DataGridView dataGridView = _GridChart;
			if (dataGridView != null)
			{
				dataGridView.DoubleClick -= value2;
				dataGridView.DragDrop -= value3;
				dataGridView.DragEnter -= value4;
				dataGridView.DragOver -= value5;
				dataGridView.MouseDown -= value6;
			}
			_GridChart = value;
			dataGridView = _GridChart;
			if (dataGridView != null)
			{
				dataGridView.DoubleClick += value2;
				dataGridView.DragDrop += value3;
				dataGridView.DragEnter += value4;
				dataGridView.DragOver += value5;
				dataGridView.MouseDown += value6;
			}
		}
	}

	[field: AccessedThroughProperty("Col0")]
	internal virtual DataGridViewTextBoxColumn Col0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col1")]
	internal virtual DataGridViewTextBoxColumn Col1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col2")]
	internal virtual DataGridViewTextBoxColumn Col2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col3")]
	internal virtual DataGridViewTextBoxColumn Col3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col4")]
	internal virtual DataGridViewTextBoxColumn Col4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col5")]
	internal virtual DataGridViewTextBoxColumn Col5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col6")]
	internal virtual DataGridViewTextBoxColumn Col6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col7")]
	internal virtual DataGridViewTextBoxColumn Col7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Col8")]
	internal virtual DataGridViewTextBoxColumn Col8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridFilter
	{
		[CompilerGenerated]
		get
		{
			return _GridFilter;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewColumnEventHandler value2 = GridFilter_ColumnWidthChanged;
			EventHandler value3 = GridFilter_DoubleClick;
			DragEventHandler value4 = GridFilter_DragDrop;
			DragEventHandler value5 = GridFilter_DragEnter;
			DragEventHandler value6 = GridFilter_DragOver;
			MouseEventHandler value7 = GridFilter_MouseDown;
			EventHandler value8 = GridFilter_Resize;
			DataGridView dataGridView = _GridFilter;
			if (dataGridView != null)
			{
				dataGridView.ColumnWidthChanged -= value2;
				dataGridView.DoubleClick -= value3;
				dataGridView.DragDrop -= value4;
				dataGridView.DragEnter -= value5;
				dataGridView.DragOver -= value6;
				dataGridView.MouseDown -= value7;
				dataGridView.Resize -= value8;
			}
			_GridFilter = value;
			dataGridView = _GridFilter;
			if (dataGridView != null)
			{
				dataGridView.ColumnWidthChanged += value2;
				dataGridView.DoubleClick += value3;
				dataGridView.DragDrop += value4;
				dataGridView.DragEnter += value5;
				dataGridView.DragOver += value6;
				dataGridView.MouseDown += value7;
				dataGridView.Resize += value8;
			}
		}
	}

	[field: AccessedThroughProperty("ColFilter2")]
	internal virtual DataGridViewTextBoxColumn ColFilter2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmJMPWindow()
	{
		base.Load += FrmDisplay_Load;
		base.Resize += FrmJMPWindow_Resize;
		MyDragSource = "";
		f_JSLScript = "";
		f_ScriptType = "";
		f_Charts = "";
		f_ChartsActual = "";
		MyTableData = "";
		l_ChartsActual = new string[1001];
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
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.lblAttach = new System.Windows.Forms.Label();
		this.lblrole = new System.Windows.Forms.Label();
		this.CmdBrowse3 = new System.Windows.Forms.Button();
		this.TxtAttach = new System.Windows.Forms.TextBox();
		this.cmbrole = new System.Windows.Forms.ComboBox();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.optPP = new System.Windows.Forms.RadioButton();
		this.optSP = new System.Windows.Forms.RadioButton();
		this.optemail = new System.Windows.Forms.RadioButton();
		this.optfile = new System.Windows.Forms.RadioButton();
		this.LblHTMOut = new System.Windows.Forms.Label();
		this.TxtHTMOut = new System.Windows.Forms.TextBox();
		this.LblHTMCSS = new System.Windows.Forms.Label();
		this.TxtCSSOut = new System.Windows.Forms.TextBox();
		this.ChkEmbedCSS = new System.Windows.Forms.CheckBox();
		this.CmdBrowse2 = new System.Windows.Forms.Button();
		this.ChkReverse = new System.Windows.Forms.CheckBox();
		this.LblTitle = new System.Windows.Forms.Label();
		this.CmdBrowse1 = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.LstChart = new System.Windows.Forms.ListBox();
		this.LblChart = new System.Windows.Forms.Label();
		this.mnuCW = new System.Windows.Forms.MenuStrip();
		this.mnuOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRR = new System.Windows.Forms.ToolStripMenuItem();
		this.mnudisplaywin = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.mnuRClick = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuColSpan = new System.Windows.Forms.ToolStripComboBox();
		this.GridFilter = new System.Windows.Forms.DataGridView();
		this.ColFilter2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GridChart = new System.Windows.Forms.DataGridView();
		this.Col8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		this.mnuCW.SuspendLayout();
		this.mnuRClick.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridFilter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
		this.SplitContainer2.Panel1.SuspendLayout();
		this.SplitContainer2.Panel2.SuspendLayout();
		this.SplitContainer2.SuspendLayout();
		base.SuspendLayout();
		this.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer1.Panel1.Controls.Add(this.lblAttach);
		this.SplitContainer1.Panel1.Controls.Add(this.lblrole);
		this.SplitContainer1.Panel1.Controls.Add(this.CmdBrowse3);
		this.SplitContainer1.Panel1.Controls.Add(this.TxtAttach);
		this.SplitContainer1.Panel1.Controls.Add(this.cmbrole);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
		this.SplitContainer1.Panel1.Controls.Add(this.LblHTMOut);
		this.SplitContainer1.Panel1.Controls.Add(this.TxtHTMOut);
		this.SplitContainer1.Panel1.Controls.Add(this.LblHTMCSS);
		this.SplitContainer1.Panel1.Controls.Add(this.TxtCSSOut);
		this.SplitContainer1.Panel1.Controls.Add(this.ChkEmbedCSS);
		this.SplitContainer1.Panel1.Controls.Add(this.CmdBrowse2);
		this.SplitContainer1.Panel1.Controls.Add(this.ChkReverse);
		this.SplitContainer1.Panel1.Controls.Add(this.LblTitle);
		this.SplitContainer1.Panel1.Controls.Add(this.CmdBrowse1);
		this.SplitContainer1.Panel1.Controls.Add(this.CmdCancel);
		this.SplitContainer1.Panel1.Controls.Add(this.CmdOK);
		this.SplitContainer1.Panel1.Controls.Add(this.LstChart);
		this.SplitContainer1.Panel1.Controls.Add(this.LblChart);
		this.SplitContainer1.Panel1.Controls.Add(this.mnuCW);
		this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
		this.SplitContainer1.Size = new System.Drawing.Size(611, 552);
		this.SplitContainer1.SplitterDistance = 305;
		this.SplitContainer1.TabIndex = 0;
		this.lblAttach.AutoSize = true;
		this.lblAttach.Location = new System.Drawing.Point(305, 240);
		this.lblAttach.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lblAttach.Name = "lblAttach";
		this.lblAttach.Size = new System.Drawing.Size(69, 13);
		this.lblAttach.TabIndex = 21;
		this.lblAttach.Text = "Attachments:";
		this.lblAttach.Visible = false;
		this.lblrole.AutoSize = true;
		this.lblrole.Location = new System.Drawing.Point(186, 240);
		this.lblrole.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lblrole.Name = "lblrole";
		this.lblrole.Size = new System.Drawing.Size(37, 13);
		this.lblrole.TabIndex = 20;
		this.lblrole.Text = "Roles:";
		this.lblrole.Visible = false;
		this.CmdBrowse3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowse3.Location = new System.Drawing.Point(536, 254);
		this.CmdBrowse3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.CmdBrowse3.Name = "CmdBrowse3";
		this.CmdBrowse3.Size = new System.Drawing.Size(59, 23);
		this.CmdBrowse3.TabIndex = 9;
		this.ToolTip1.SetToolTip(this.CmdBrowse3, "Browse for Attachments");
		this.CmdBrowse3.UseVisualStyleBackColor = true;
		this.TxtAttach.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAttach.Location = new System.Drawing.Point(305, 258);
		this.TxtAttach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.TxtAttach.Name = "TxtAttach";
		this.TxtAttach.Size = new System.Drawing.Size(203, 20);
		this.TxtAttach.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.TxtAttach, "Opt. Comma delimited list of Files to Attach");
		this.TxtAttach.Visible = false;
		this.cmbrole.FormattingEnabled = true;
		this.cmbrole.Items.AddRange(new object[12]
		{
			"", "1216_MAOData", "1217_MAOData", "1266_MAOData", "1268_MAOData", "1269_MAOData", "1270_MAOData", "1272_MAOData", "1273_MAOData", "1274_MAOData",
			"DETD_MARS", "TMGUSER_MAOATM"
		});
		this.cmbrole.Location = new System.Drawing.Point(186, 258);
		this.cmbrole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.cmbrole.Name = "cmbrole";
		this.cmbrole.Size = new System.Drawing.Size(108, 21);
		this.cmbrole.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.cmbrole, "Opt. Comma delimited list of Valid Rialto Roles");
		this.cmbrole.Visible = false;
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.optPP);
		this.GroupBox1.Controls.Add(this.optSP);
		this.GroupBox1.Controls.Add(this.optemail);
		this.GroupBox1.Controls.Add(this.optfile);
		this.GroupBox1.Location = new System.Drawing.Point(188, 69);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(315, 35);
		this.GroupBox1.TabIndex = 16;
		this.GroupBox1.TabStop = false;
		this.optPP.AutoSize = true;
		this.optPP.Location = new System.Drawing.Point(203, 13);
		this.optPP.Name = "optPP";
		this.optPP.Size = new System.Drawing.Size(79, 17);
		this.optPP.TabIndex = 3;
		this.optPP.Text = "PowerPoint";
		this.optPP.UseVisualStyleBackColor = true;
		this.optSP.AutoSize = true;
		this.optSP.Location = new System.Drawing.Point(117, 13);
		this.optSP.Name = "optSP";
		this.optSP.Size = new System.Drawing.Size(77, 17);
		this.optSP.TabIndex = 2;
		this.optSP.Text = "SharePoint";
		this.optSP.UseVisualStyleBackColor = true;
		this.optemail.AutoSize = true;
		this.optemail.Location = new System.Drawing.Point(58, 13);
		this.optemail.Name = "optemail";
		this.optemail.Size = new System.Drawing.Size(50, 17);
		this.optemail.TabIndex = 1;
		this.optemail.Text = "Email";
		this.optemail.UseVisualStyleBackColor = true;
		this.optfile.AutoSize = true;
		this.optfile.Checked = true;
		this.optfile.Location = new System.Drawing.Point(7, 13);
		this.optfile.Name = "optfile";
		this.optfile.Size = new System.Drawing.Size(41, 17);
		this.optfile.TabIndex = 1;
		this.optfile.TabStop = true;
		this.optfile.Text = "File";
		this.optfile.UseVisualStyleBackColor = true;
		this.LblHTMOut.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHTMOut.Location = new System.Drawing.Point(188, 111);
		this.LblHTMOut.Name = "LblHTMOut";
		this.LblHTMOut.Size = new System.Drawing.Size(319, 44);
		this.LblHTMOut.TabIndex = 12;
		this.LblHTMOut.Text = "HTML Output File or SharePoint Path, or Email address  list preceded by token 'EMAIL:', or path to a text file containing email addresses, one address per line and with a column header";
		this.TxtHTMOut.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHTMOut.Location = new System.Drawing.Point(186, 164);
		this.TxtHTMOut.Name = "TxtHTMOut";
		this.TxtHTMOut.Size = new System.Drawing.Size(320, 20);
		this.TxtHTMOut.TabIndex = 2;
		this.TxtHTMOut.Text = "SQLPathFinder.htm";
		this.LblHTMCSS.AutoSize = true;
		this.LblHTMCSS.Location = new System.Drawing.Point(186, 195);
		this.LblHTMCSS.Name = "LblHTMCSS";
		this.LblHTMCSS.Size = new System.Drawing.Size(88, 13);
		this.LblHTMCSS.TabIndex = 13;
		this.LblHTMCSS.Text = "CSS Style Sheet:";
		this.TxtCSSOut.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCSSOut.Location = new System.Drawing.Point(186, 213);
		this.TxtCSSOut.Name = "TxtCSSOut";
		this.TxtCSSOut.Size = new System.Drawing.Size(318, 20);
		this.TxtCSSOut.TabIndex = 4;
		this.TxtCSSOut.Text = "sqlpathfinder_style_1.css";
		this.ChkEmbedCSS.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ChkEmbedCSS.AutoSize = true;
		this.ChkEmbedCSS.Location = new System.Drawing.Point(401, 195);
		this.ChkEmbedCSS.Name = "ChkEmbedCSS";
		this.ChkEmbedCSS.Size = new System.Drawing.Size(105, 17);
		this.ChkEmbedCSS.TabIndex = 5;
		this.ChkEmbedCSS.Text = "Embed in Report";
		this.ToolTip1.SetToolTip(this.ChkEmbedCSS, "Embed style sheet in report ");
		this.ChkEmbedCSS.UseVisualStyleBackColor = true;
		this.ChkEmbedCSS.Visible = false;
		this.CmdBrowse2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowse2.ImageIndex = 2;
		this.CmdBrowse2.Location = new System.Drawing.Point(537, 210);
		this.CmdBrowse2.Name = "CmdBrowse2";
		this.CmdBrowse2.Size = new System.Drawing.Size(59, 23);
		this.CmdBrowse2.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.CmdBrowse2, "Browse");
		this.CmdBrowse2.UseVisualStyleBackColor = true;
		this.ChkReverse.AutoSize = true;
		this.ChkReverse.Location = new System.Drawing.Point(10, 262);
		this.ChkReverse.Name = "ChkReverse";
		this.ChkReverse.Size = new System.Drawing.Size(139, 17);
		this.ChkReverse.TabIndex = 10;
		this.ChkReverse.Text = "Reverse Grids in display";
		this.ChkReverse.UseVisualStyleBackColor = true;
		this.LblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblTitle.AutoSize = true;
		this.LblTitle.Location = new System.Drawing.Point(10, 29);
		this.LblTitle.Name = "LblTitle";
		this.LblTitle.Size = new System.Drawing.Size(348, 13);
		this.LblTitle.TabIndex = 6;
		this.LblTitle.Text = "Drag and position Objects onto Display. Remove Objects bounded by ** ";
		this.CmdBrowse1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowse1.ImageIndex = 2;
		this.CmdBrowse1.Location = new System.Drawing.Point(537, 162);
		this.CmdBrowse1.Name = "CmdBrowse1";
		this.CmdBrowse1.Size = new System.Drawing.Size(59, 23);
		this.CmdBrowse1.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdBrowse1, "Browse");
		this.CmdBrowse1.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 1;
		this.CmdCancel.Location = new System.Drawing.Point(537, 74);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(60, 41);
		this.CmdCancel.TabIndex = 14;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdOK.ImageIndex = 0;
		this.CmdOK.Location = new System.Drawing.Point(537, 28);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(60, 41);
		this.CmdOK.TabIndex = 13;
		this.CmdOK.Text = "OK";
		this.CmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdOK.UseVisualStyleBackColor = true;
		this.LstChart.AllowDrop = true;
		this.LstChart.FormattingEnabled = true;
		this.LstChart.Location = new System.Drawing.Point(10, 80);
		this.LstChart.Name = "LstChart";
		this.LstChart.Size = new System.Drawing.Size(170, 160);
		this.LstChart.TabIndex = 0;
		this.LblChart.AutoSize = true;
		this.LblChart.Location = new System.Drawing.Point(10, 63);
		this.LblChart.Name = "LblChart";
		this.LblChart.Size = new System.Drawing.Size(140, 13);
		this.LblChart.TabIndex = 1;
		this.LblChart.Text = "Chart/Table/Report Objects";
		this.mnuCW.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuCW.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuOpt });
		this.mnuCW.Location = new System.Drawing.Point(0, 0);
		this.mnuCW.Name = "mnuCW";
		this.mnuCW.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
		this.mnuCW.Size = new System.Drawing.Size(607, 24);
		this.mnuCW.TabIndex = 15;
		this.mnuCW.Text = "MenuStrip1";
		this.mnuOpt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuRR, this.mnudisplaywin });
		this.mnuOpt.Name = "mnuOpt";
		this.mnuOpt.Size = new System.Drawing.Size(61, 20);
		this.mnuOpt.Text = "Options";
		this.mnuRR.Name = "mnuRR";
		this.mnuRR.Size = new System.Drawing.Size(247, 22);
		this.mnuRR.Text = "HTML Page Refresh Rate {No}";
		this.mnuRR.ToolTipText = "For HTML reports on a SharePoint or web site,set an optional page refresh rate";
		this.mnudisplaywin.CheckOnClick = true;
		this.mnudisplaywin.Name = "mnudisplaywin";
		this.mnudisplaywin.Size = new System.Drawing.Size(247, 22);
		this.mnudisplaywin.Text = "Do not display report to Terminal";
		this.mnuRClick.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuRClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuColSpan });
		this.mnuRClick.Name = "mnuRClick";
		this.mnuRClick.Size = new System.Drawing.Size(182, 31);
		this.mnuRClick.Text = "Enter a Column Span";
		this.mnuColSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.mnuColSpan.Items.AddRange(new object[10] { "Colspan", "Colspan=1", "Colspan=2", "Colspan=3", "Colspan=4", "Colspan=5", "Colspan=6", "Colspan=7", "Colspan=8", "Colspan=9" });
		this.mnuColSpan.Name = "mnuColSpan";
		this.mnuColSpan.Size = new System.Drawing.Size(121, 23);
		this.GridFilter.AllowDrop = true;
		this.GridFilter.AllowUserToAddRows = false;
		this.GridFilter.AllowUserToDeleteRows = false;
		this.GridFilter.AllowUserToResizeColumns = false;
		this.GridFilter.AllowUserToResizeRows = false;
		this.GridFilter.BackgroundColor = System.Drawing.Color.White;
		this.GridFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.GridFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridFilter.Columns.AddRange(this.ColFilter2);
		this.GridFilter.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridFilter.Location = new System.Drawing.Point(0, 0);
		this.GridFilter.MultiSelect = false;
		this.GridFilter.Name = "GridFilter";
		this.GridFilter.RowHeadersVisible = false;
		this.GridFilter.RowHeadersWidth = 62;
		this.GridFilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.GridFilter.Size = new System.Drawing.Size(113, 239);
		this.GridFilter.StandardTab = true;
		this.GridFilter.TabIndex = 12;
		this.ColFilter2.HeaderText = "A";
		this.ColFilter2.MinimumWidth = 10;
		this.ColFilter2.Name = "ColFilter2";
		this.ColFilter2.ReadOnly = true;
		this.ColFilter2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColFilter2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColFilter2.Width = 150;
		this.GridChart.AllowDrop = true;
		this.GridChart.AllowUserToAddRows = false;
		this.GridChart.AllowUserToDeleteRows = false;
		this.GridChart.AllowUserToResizeRows = false;
		this.GridChart.BackgroundColor = System.Drawing.Color.White;
		this.GridChart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.GridChart.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
		this.GridChart.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		this.GridChart.ColumnHeadersHeight = 34;
		this.GridChart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.GridChart.Columns.AddRange(this.Col0, this.Col1, this.Col2, this.Col3, this.Col4, this.Col5, this.Col6, this.Col7, this.Col8);
		this.GridChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridChart.Location = new System.Drawing.Point(0, 0);
		this.GridChart.MultiSelect = false;
		this.GridChart.Name = "GridChart";
		this.GridChart.RowHeadersWidth = 50;
		this.GridChart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.GridChart.Size = new System.Drawing.Size(486, 239);
		this.GridChart.StandardTab = true;
		this.GridChart.TabIndex = 11;
		this.Col8.HeaderText = "I";
		this.Col8.MinimumWidth = 50;
		this.Col8.Name = "Col8";
		this.Col8.ReadOnly = true;
		this.Col8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col8.Width = 50;
		this.Col7.HeaderText = "H";
		this.Col7.MinimumWidth = 50;
		this.Col7.Name = "Col7";
		this.Col7.ReadOnly = true;
		this.Col7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col7.Width = 50;
		this.Col6.HeaderText = "G";
		this.Col6.MinimumWidth = 50;
		this.Col6.Name = "Col6";
		this.Col6.ReadOnly = true;
		this.Col6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col6.Width = 50;
		this.Col5.HeaderText = "F";
		this.Col5.MinimumWidth = 50;
		this.Col5.Name = "Col5";
		this.Col5.ReadOnly = true;
		this.Col5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col5.Width = 50;
		this.Col4.HeaderText = "E";
		this.Col4.MinimumWidth = 50;
		this.Col4.Name = "Col4";
		this.Col4.ReadOnly = true;
		this.Col4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col4.Width = 50;
		this.Col3.HeaderText = "D";
		this.Col3.MinimumWidth = 50;
		this.Col3.Name = "Col3";
		this.Col3.ReadOnly = true;
		this.Col3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col3.Width = 50;
		this.Col2.HeaderText = "C";
		this.Col2.MinimumWidth = 50;
		this.Col2.Name = "Col2";
		this.Col2.ReadOnly = true;
		this.Col2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col2.Width = 50;
		this.Col1.HeaderText = "B";
		this.Col1.MinimumWidth = 50;
		this.Col1.Name = "Col1";
		this.Col1.ReadOnly = true;
		this.Col1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col1.Width = 50;
		this.Col0.HeaderText = "A";
		this.Col0.MinimumWidth = 50;
		this.Col0.Name = "Col0";
		this.Col0.ReadOnly = true;
		this.Col0.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col0.Width = 50;
		this.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer2.Name = "SplitContainer2";
		this.SplitContainer2.Panel1.Controls.Add(this.GridChart);
		this.SplitContainer2.Panel2.Controls.Add(this.GridFilter);
		this.SplitContainer2.Size = new System.Drawing.Size(611, 243);
		this.SplitContainer2.SplitterDistance = 490;
		this.SplitContainer2.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(611, 552);
		base.Controls.Add(this.SplitContainer1);
		base.Name = "FrmJMPWindow";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Create Window Layout";
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel1.PerformLayout();
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.mnuCW.ResumeLayout(false);
		this.mnuCW.PerformLayout();
		this.mnuRClick.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridFilter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridChart).EndInit();
		this.SplitContainer2.Panel1.ResumeLayout(false);
		this.SplitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
		this.SplitContainer2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public void Get_Opt(string MyOut)
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
				case 352:
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
							goto IL_002f;
						case 6:
						case 7:
							goto IL_0045;
						case 8:
							goto IL_006a;
						case 10:
							goto IL_007f;
						case 11:
							goto IL_00a5;
						case 13:
							goto IL_00b8;
						case 14:
							goto IL_00de;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 12:
						case 15:
						case 17:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a5:
					num2 = 11;
					optemail.Checked = true;
					goto end_IL_0001_3;
					IL_00b8:
					num2 = 13;
					if (!LikeOperator.LikeString(MyOut, "*.ppt", CompareMethod.Binary) && !LikeOperator.LikeString(MyOut, "*.pptx", CompareMethod.Binary))
					{
						break;
					}
					goto IL_00de;
					IL_007f:
					num2 = 10;
					if (LikeOperator.LikeString(MyOut, "email:*", CompareMethod.Binary) || LikeOperator.LikeString(MyOut, "email-a:*", CompareMethod.Binary))
					{
						goto IL_00a5;
					}
					goto IL_00b8;
					IL_006a:
					num2 = 8;
					optSP.Checked = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					MyOut = Strings.LCase(Strings.Trim(MyOut));
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (Operators.CompareString(MyOut, "", TextCompare: false) == 0)
					{
						goto IL_002f;
					}
					goto IL_0045;
					IL_002f:
					num2 = 4;
					optfile.Checked = true;
					goto end_IL_0001_3;
					IL_0045:
					num2 = 7;
					if (LikeOperator.LikeString(MyOut, "http:*", CompareMethod.Binary) || LikeOperator.LikeString(MyOut, "https:*", CompareMethod.Binary))
					{
						goto IL_006a;
					}
					goto IL_007f;
					IL_00de:
					num2 = 14;
					optPP.Checked = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				optfile.Checked = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 352;
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

	public void Set_Opt()
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
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1087:
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
							goto IL_0029;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0047;
						case 7:
							goto IL_0056;
						case 8:
							goto IL_0066;
						case 10:
							goto IL_0094;
						case 11:
							goto IL_00a8;
						case 12:
							goto IL_00b8;
						case 13:
							goto IL_00cc;
						case 15:
							goto IL_00e6;
						case 16:
							goto IL_00fa;
						case 17:
							goto IL_010e;
						case 18:
							goto IL_011e;
						case 19:
							goto IL_012e;
						case 20:
							goto IL_013e;
						case 21:
							goto IL_014e;
						case 23:
							goto IL_0161;
						case 24:
							goto IL_0175;
						case 26:
							goto IL_018c;
						case 27:
							goto IL_01a0;
						case 28:
							goto IL_01b4;
						case 29:
							goto IL_01c8;
						case 32:
							goto IL_01df;
						case 33:
							goto IL_01ef;
						case 34:
							goto IL_01ff;
						case 35:
							goto IL_020f;
						case 36:
							goto IL_021f;
						case 37:
							goto IL_0233;
						case 39:
							goto IL_024d;
						case 40:
							goto IL_0261;
						case 41:
							goto IL_0275;
						case 42:
							goto IL_0289;
						case 43:
							goto IL_0299;
						case 45:
							goto IL_02af;
						case 46:
							goto IL_02c3;
						case 48:
							goto IL_02da;
						case 49:
							goto IL_02ee;
						case 50:
							goto IL_0302;
						case 51:
							goto IL_0316;
						case 52:
							goto IL_0326;
						case 53:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 14:
						case 22:
						case 25:
						case 30:
						case 31:
						case 38:
						case 44:
						case 47:
						case 54:
						case 55:
						case 56:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_018c:
					num2 = 26;
					if (!optPP.Checked)
					{
						goto end_IL_0001_3;
					}
					goto IL_01a0;
					IL_01a0:
					num2 = 27;
					LblHTMOut.Text = "Specify PPT File to write images to and optional Template file with rectangular shapes to hold images. Leave template blank to create PPT with 1 image per slide";
					goto IL_01b4;
					IL_0175:
					num2 = 24;
					LblHTMOut.Text = "Enter SharePoint Path. E.g.,https://sharepoint.amr.ith.intel.com/sites/.../SPF.htm";
					goto end_IL_0001_3;
					IL_01b4:
					num2 = 28;
					LblHTMCSS.Text = "PPT Template File (opt)";
					goto IL_01c8;
					IL_000b:
					num2 = 2;
					CmdBrowse2.Visible = true;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					lblAttach.Visible = false;
					goto IL_0029;
					IL_0029:
					num2 = 4;
					TxtAttach.Visible = false;
					goto IL_0038;
					IL_0038:
					num2 = 5;
					lblrole.Visible = false;
					goto IL_0047;
					IL_0047:
					num2 = 6;
					cmbrole.Visible = false;
					goto IL_0056;
					IL_0056:
					num2 = 7;
					CmdBrowse3.Visible = false;
					goto IL_0066;
					IL_0066:
					num2 = 8;
					left = f_ScriptType;
					if (Operators.CompareString(left, "HTML", TextCompare: false) == 0)
					{
						goto IL_0094;
					}
					if (Operators.CompareString(left, "JMP", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_01df;
					IL_01c8:
					num2 = 29;
					ChkEmbedCSS.Visible = false;
					goto end_IL_0001_3;
					IL_01df:
					num2 = 32;
					ChkEmbedCSS.Visible = false;
					goto IL_01ef;
					IL_01ef:
					num2 = 33;
					TxtCSSOut.Visible = false;
					goto IL_01ff;
					IL_01ff:
					num2 = 34;
					LblHTMCSS.Visible = false;
					goto IL_020f;
					IL_020f:
					num2 = 35;
					CmdBrowse2.Visible = false;
					goto IL_021f;
					IL_021f:
					num2 = 36;
					if (optfile.Checked)
					{
						goto IL_0233;
					}
					goto IL_024d;
					IL_0233:
					num2 = 37;
					LblHTMOut.Text = "Optionally Specify your output file as a Journal file (.JRN), HTML file (.HTM), Image file (.PNG), RTF File (.RTF) or Text File (.TXT)";
					goto end_IL_0001_3;
					IL_024d:
					num2 = 39;
					if (optemail.Checked)
					{
						goto IL_0261;
					}
					goto IL_02af;
					IL_0261:
					num2 = 40;
					LblHTMOut.Text = "Enter Email addresses optionally preceded by token EMAIL: or EMAIL-A:, (e.g., EMAIL:j.doe@intel.com,self) or file with column header and 1 address per line";
					goto IL_0275;
					IL_0275:
					num2 = 41;
					LblHTMCSS.Text = "Email Subject (opt)";
					goto IL_0289;
					IL_0289:
					num2 = 42;
					TxtCSSOut.Visible = true;
					goto IL_0299;
					IL_0299:
					num2 = 43;
					LblHTMCSS.Visible = true;
					goto end_IL_0001_3;
					IL_02af:
					num2 = 45;
					if (optSP.Checked)
					{
						goto IL_02c3;
					}
					goto IL_02da;
					IL_02c3:
					num2 = 46;
					LblHTMOut.Text = "Enter SharePoint Path. E.g.,https://sharepoint.amr.ith.intel.com/sites/.../SPF.htm";
					goto end_IL_0001_3;
					IL_02da:
					num2 = 48;
					if (!optPP.Checked)
					{
						goto end_IL_0001_3;
					}
					goto IL_02ee;
					IL_02ee:
					num2 = 49;
					LblHTMOut.Text = "Specify PPT File to write images to and optional Template file with rectangular shapes to hold images. Leave template blank to create PPT with 1 image per slide";
					goto IL_0302;
					IL_0302:
					num2 = 50;
					LblHTMCSS.Text = "PPT Template File (opt)";
					goto IL_0316;
					IL_0316:
					num2 = 51;
					TxtCSSOut.Visible = true;
					goto IL_0326;
					IL_0326:
					num2 = 52;
					LblHTMCSS.Visible = true;
					break;
					IL_0094:
					num2 = 10;
					LblHTMCSS.Text = "CSS Style Sheet";
					goto IL_00a8;
					IL_00a8:
					num2 = 11;
					ChkEmbedCSS.Visible = true;
					goto IL_00b8;
					IL_00b8:
					num2 = 12;
					if (optfile.Checked)
					{
						goto IL_00cc;
					}
					goto IL_00e6;
					IL_00cc:
					num2 = 13;
					LblHTMOut.Text = "Enter HTML Output File Name and Path";
					goto end_IL_0001_3;
					IL_00e6:
					num2 = 15;
					if (optemail.Checked)
					{
						goto IL_00fa;
					}
					goto IL_0161;
					IL_00fa:
					num2 = 16;
					LblHTMOut.Text = "Enter Email addresses optionally preceded by token EMAIL: or EMAIL-A:, (e.g., EMAIL:j.doe@intel.com,self) or file with column header and 1 address per line";
					goto IL_010e;
					IL_010e:
					num2 = 17;
					lblAttach.Visible = true;
					goto IL_011e;
					IL_011e:
					num2 = 18;
					TxtAttach.Visible = true;
					goto IL_012e;
					IL_012e:
					num2 = 19;
					lblrole.Visible = true;
					goto IL_013e;
					IL_013e:
					num2 = 20;
					cmbrole.Visible = true;
					goto IL_014e;
					IL_014e:
					num2 = 21;
					CmdBrowse3.Visible = true;
					goto end_IL_0001_3;
					IL_0161:
					num2 = 23;
					if (optSP.Checked)
					{
						goto IL_0175;
					}
					goto IL_018c;
					end_IL_0001_2:
					break;
				}
				num2 = 53;
				CmdBrowse2.Visible = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1087;
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

	public void Get_Text2(DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string MyLabel = default(string);
		int num = default(int);
		int num3 = default(int);
		int num5 = default(int);
		int rowIndex = default(int);
		int columnIndex = default(int);
		string text = default(string);
		string MyCS = default(string);
		string text2 = default(string);
		string text3 = default(string);
		FrmText frmText = default(FrmText);
		frmfilename frmfilename2 = default(frmfilename);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					MyLabel = "";
					goto IL_000a;
				case 1651:
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
							goto IL_000a;
						case 3:
							goto IL_0013;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0033;
						case 8:
							goto IL_0052;
						case 9:
							goto IL_0061;
						case 10:
							goto IL_0071;
						case 12:
							goto IL_008a;
						case 13:
							goto IL_00b2;
						case 14:
							goto IL_00cb;
						case 15:
							goto IL_00e0;
						case 16:
							goto IL_0108;
						case 17:
							goto IL_0112;
						case 18:
							goto IL_011e;
						case 19:
							goto IL_012a;
						case 20:
							goto IL_0135;
						case 21:
							goto IL_0145;
						case 22:
							goto IL_016e;
						case 23:
							goto IL_018c;
						case 24:
						case 25:
							goto IL_01c1;
						case 27:
							goto IL_01d2;
						case 28:
							goto IL_01fd;
						case 29:
							goto IL_0213;
						case 30:
							goto IL_0220;
						case 32:
							goto IL_022e;
						case 33:
							goto IL_023b;
						case 31:
						case 34:
						case 35:
							goto IL_0246;
						case 36:
							goto IL_0253;
						case 37:
							goto IL_0266;
						case 38:
							goto IL_0274;
						case 40:
							goto IL_0286;
						case 39:
						case 41:
						case 42:
							goto IL_0291;
						case 43:
							goto IL_02c3;
						case 44:
							goto IL_02f5;
						case 45:
							goto IL_02ff;
						case 46:
							goto IL_030a;
						case 47:
							goto IL_0329;
						case 48:
							goto IL_033f;
						case 49:
							goto IL_034d;
						case 50:
							goto IL_0387;
						case 52:
							goto IL_03a7;
						case 51:
						case 53:
						case 54:
							goto IL_03cc;
						case 57:
							goto IL_03fb;
						case 58:
						case 67:
							goto IL_0418;
						case 59:
							goto IL_042d;
						case 60:
							goto IL_044a;
						case 62:
							goto IL_0469;
						case 63:
							goto IL_048c;
						case 65:
							goto IL_04b5;
						case 66:
							goto IL_04e9;
						case 69:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
						case 26:
						case 55:
						case 56:
						case 61:
						case 64:
						case 68:
						case 70:
						case 71:
						case 72:
						case 73:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_033f:
					num2 = 48;
					if (num5 != 0)
					{
						goto IL_034d;
					}
					goto IL_03a7;
					IL_034d:
					num2 = 49;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Value = text + Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, num5));
					goto IL_0387;
					IL_0329:
					num2 = 47;
					num5 = Strings.InStrRev(Globals_Renamed.currvaluetmp, ":");
					goto IL_033f;
					IL_0387:
					num2 = 50;
					Globals_Renamed.currvaluetmp = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 1, checked(num5 - 1)));
					goto IL_03cc;
					IL_000a:
					num2 = 2;
					MyCS = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text2 = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					text = "";
					goto IL_002a;
					IL_002a:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0033;
					IL_0033:
					num2 = 7;
					if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0052;
					IL_0052:
					num2 = 8;
					columnIndex = MyGrid.CurrentCell.ColumnIndex;
					goto IL_0061;
					IL_0061:
					num2 = 9;
					rowIndex = MyGrid.CurrentCell.RowIndex;
					goto IL_0071;
					IL_0071:
					num2 = 10;
					if (columnIndex == -1 || rowIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_008a;
					IL_008a:
					num2 = 12;
					text3 = Conversions.ToString(MyGrid.Rows[rowIndex].Cells[columnIndex].Value);
					goto IL_00b2;
					IL_00b2:
					num2 = 13;
					if (LikeOperator.LikeString(text3, "Text:*", CompareMethod.Binary))
					{
						goto IL_00cb;
					}
					goto IL_01d2;
					IL_00cb:
					num2 = 14;
					BuildChart.Get_CS_and_Label(f_ScriptType, text3, ref MyCS, ref MyLabel);
					goto IL_00e0;
					IL_00e0:
					num2 = 15;
					text2 = Conversions.ToString(MyGrid.Rows[rowIndex].Cells[columnIndex].Tag);
					goto IL_0108;
					IL_0108:
					num2 = 16;
					frmText = new FrmText();
					goto IL_0112;
					IL_0112:
					num2 = 17;
					frmText.f_Data = text2;
					goto IL_011e;
					IL_011e:
					num2 = 18;
					frmText.f_cs = MyCS;
					goto IL_012a;
					IL_012a:
					num2 = 19;
					frmText.ShowDialog();
					goto IL_0135;
					IL_0135:
					num2 = 20;
					if (frmText.f_OK)
					{
						goto IL_0145;
					}
					goto IL_01c1;
					IL_0145:
					num2 = 21;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Tag = frmText.f_Data;
					goto IL_016e;
					IL_016e:
					num2 = 22;
					if (Operators.CompareString(frmText.f_cs, "", TextCompare: false) != 0)
					{
						goto IL_018c;
					}
					goto IL_01c1;
					IL_018c:
					num2 = 23;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Value = "Text:" + frmText.f_cs;
					goto IL_01c1;
					IL_01c1:
					num2 = 25;
					frmText.Dispose();
					goto end_IL_0001_3;
					IL_01d2:
					num2 = 27;
					if (LikeOperator.LikeString(text3, "Images:*", CompareMethod.Binary) || LikeOperator.LikeString(text3, "JMP-HTML:*", CompareMethod.Binary))
					{
						goto IL_01fd;
					}
					goto IL_03fb;
					IL_03a7:
					num2 = 52;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Value = text;
					goto IL_03cc;
					IL_03fb:
					num2 = 57;
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0418;
					IL_0418:
					num2 = 58;
					BuildChart.Get_CS_and_Label(f_ScriptType, text3, ref MyCS, ref MyLabel);
					goto IL_042d;
					IL_042d:
					num2 = 59;
					MyCS = Strings.Trim(Interaction.InputBox("Enter a column span for this element from 1 to 9. Enter NONE to remove column spanning", "Specify a Column Span", MyCS));
					goto IL_044a;
					IL_044a:
					num2 = 60;
					if (Operators.CompareString(MyCS, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0469;
					IL_0469:
					num2 = 62;
					if (Operators.CompareString(Strings.Trim(Strings.UCase(MyCS)), "NONE", TextCompare: false) == 0)
					{
						goto IL_048c;
					}
					goto IL_04b5;
					IL_048c:
					num2 = 63;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Value = MyLabel;
					goto end_IL_0001_3;
					IL_04b5:
					num2 = 65;
					if (Versioned.IsNumeric(MyCS) && ((Conversions.ToInteger(MyCS) >= 1) & (Conversions.ToInteger(MyCS) <= 9)))
					{
						break;
					}
					goto IL_04e9;
					IL_03cc:
					num2 = 54;
					MyGrid.Rows[rowIndex].Cells[columnIndex].Tag = Globals_Renamed.currvaluetmp;
					goto end_IL_0001_3;
					IL_04e9:
					num2 = 66;
					Interaction.MsgBox("You entered an invalid column span number. The value should be between 1 and 9 or set to None. Do try again", MsgBoxStyle.Exclamation, "Invalid Entry");
					goto IL_0418;
					IL_01fd:
					num2 = 28;
					if (LikeOperator.LikeString(text3, "Images:*", CompareMethod.Binary))
					{
						goto IL_0213;
					}
					goto IL_022e;
					IL_0213:
					num2 = 29;
					Globals_Renamed.currinputstrtmp = "C";
					goto IL_0220;
					IL_0220:
					num2 = 30;
					text = "Images";
					goto IL_0246;
					IL_022e:
					num2 = 32;
					Globals_Renamed.currinputstrtmp = "D";
					goto IL_023b;
					IL_023b:
					num2 = 33;
					text = "JMP-HTML";
					goto IL_0246;
					IL_0246:
					num2 = 35;
					Globals_Renamed.currvaluetmp = "";
					goto IL_0253;
					IL_0253:
					num2 = 36;
					num5 = Strings.InStrRev(text3, ":");
					goto IL_0266;
					IL_0266:
					num2 = 37;
					if (num5 != 0)
					{
						goto IL_0274;
					}
					goto IL_0286;
					IL_0274:
					num2 = 38;
					text2 = Strings.Mid(text3, num5);
					goto IL_0291;
					IL_0286:
					num2 = 40;
					text2 = ":1";
					goto IL_0291;
					IL_0291:
					num2 = 42;
					if (Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[rowIndex].Cells[columnIndex].Tag, "", TextCompare: false))
					{
						goto IL_02c3;
					}
					goto IL_02f5;
					IL_02c3:
					num2 = 43;
					Globals_Renamed.currvaluetmp = Conversions.ToString(Operators.ConcatenateObject(MyGrid.Rows[rowIndex].Cells[columnIndex].Tag, text2));
					goto IL_02f5;
					IL_02f5:
					num2 = 44;
					frmfilename2 = new frmfilename();
					goto IL_02ff;
					IL_02ff:
					num2 = 45;
					frmfilename2.ShowDialog();
					goto IL_030a;
					IL_030a:
					num2 = 46;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0329;
					end_IL_0001_2:
					break;
				}
				num2 = 69;
				MyGrid.Rows[rowIndex].Cells[columnIndex].Value = MyLabel + ":" + MyCS;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1651;
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

	public string Get_Display_Label(string MyTrue)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string result = default(string);
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
				case 256:
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
							goto IL_0010;
						case 4:
							goto IL_0027;
						case 7:
							goto IL_0035;
						case 8:
							goto IL_0048;
						case 6:
						case 9:
						case 10:
							goto IL_0053;
						case 11:
							goto IL_0059;
						case 12:
							goto end_IL_0001_2;
						case 14:
						case 15:
							goto IL_0099;
						default:
							goto end_IL_0001;
						case 5:
						case 13:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0053:
					num2 = 10;
					num5 = 0;
					goto IL_0059;
					IL_0059:
					num2 = 11;
					if (Operators.CompareString(Strings.UCase(l_ChartsActual[num5]), MyTrue, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0099;
					IL_0048:
					num2 = 8;
					MyTrue = Strings.UCase(MyTrue);
					goto IL_0053;
					IL_0099:
					num2 = 15;
					num5 = checked(num5 + 1);
					if (num5 > 999)
					{
						goto end_IL_0001_3;
					}
					goto IL_0059;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					if (Operators.CompareString(MyTrue, "", TextCompare: false) == 0)
					{
						goto IL_0027;
					}
					goto IL_0035;
					IL_0027:
					num2 = 4;
					result = "";
					goto end_IL_0001_3;
					IL_0035:
					num2 = 7;
					result = "** " + MyTrue + " **";
					goto IL_0048;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = Conversions.ToString(LstChart.Items[num5]);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 256;
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

	public string Get_True_Label(string MyDisplay)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string result = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				Type typeFromHandle;
				object[] obj;
				ListBox.ObjectCollection items;
				int index;
				object[] array;
				bool[] obj2;
				bool[] array2;
				object left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 278:
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
							goto IL_0010;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_003c;
						case 7:
							goto end_IL_0001_2;
						case 9:
						case 10:
							goto IL_00c3;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cc:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_003c;
					IL_003c:
					num2 = 6;
					typeFromHandle = typeof(Strings);
					obj = new object[1] { (items = LstChart.Items)[index = num5] };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					left = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
					if (array2[0])
					{
						items[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (Operators.ConditionalCompareObjectEqual(left, MyDisplay, TextCompare: false))
					{
						break;
					}
					goto IL_00c3;
					IL_001e:
					num2 = 5;
					num6 = checked(LstChart.Items.Count - 1);
					num5 = 0;
					goto IL_00cc;
					IL_00c3:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_00cc;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					result = MyDisplay;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					MyDisplay = Strings.UCase(MyDisplay);
					goto IL_001e;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = l_ChartsActual[num5];
				break;
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 278;
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

	public void LstBoxDragDrop(ref DataGridView MyGrid, ref DragEventArgs e, ref string MyDragSource, ref ListBox MyListBox)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
						string text = "";
						int num3 = 0;
						string text2 = "";
						int num4 = -1;
						int num5 = -1;
						if (LikeOperator.LikeString(MyDragSource, "GRID*", CompareMethod.Binary))
						{
							text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
							num3 = Strings.InStr(text, "-");
							if (num3 == 0)
							{
								goto end_IL_0001;
							}
							text2 = Strings.Mid(text, 1, num3 - 1);
							num4 = Conversions.ToInteger(text2);
							text2 = Strings.Mid(text, num3 + 1);
							num5 = Conversions.ToInteger(text2);
							MyGrid.Rows[num4].Cells[num5].Value = "";
							MyGrid.Rows[num4].Cells[num5].Tag = "";
						}
						MyDragSource = "";
						Pack_All_Grid(ref MyGrid);
						goto end_IL_0001;
					}
					case 292:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, "FrmJMPWindow - LstBoxDragDrop", Information.Err().Description);
							Information.Err().Clear();
							MyDragSource = "";
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 292;
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

	public void Pack_Col_Left(int RowIdx, ref DataGridView MyGrid)
	{
		int num = 0;
		int num2 = 0;
		checked
		{
			int num3 = MyGrid.ColumnCount - 1;
			int num4 = 0;
			int num5 = 0;
			int num6 = num5;
			int num7 = num3;
			for (num = num6; num <= num7; num++)
			{
				if (!Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[RowIdx].Cells[num].Value, "", TextCompare: false))
				{
					continue;
				}
				num4 = 0;
				int num8 = num - 1;
				int num9 = num5;
				for (num2 = num8; num2 >= num9; num2 += -1)
				{
					if (Operators.ConditionalCompareObjectEqual(MyGrid.Rows[RowIdx].Cells[num2].Value, "", TextCompare: false))
					{
						num4++;
					}
				}
				if (num4 != 0)
				{
					MyGrid.Rows[RowIdx].Cells[num - num4].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[RowIdx].Cells[num].Value);
					MyGrid.Rows[RowIdx].Cells[num - num4].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[RowIdx].Cells[num].Tag);
					MyGrid.Rows[RowIdx].Cells[num].Value = "";
					MyGrid.Rows[RowIdx].Cells[num].Tag = "";
				}
			}
		}
	}

	public void Pack_All_Cols_Left(ref DataGridView MyGrid)
	{
		checked
		{
			int num = MyGrid.RowCount - 1;
			int num2 = 0;
			int num3 = num;
			for (num2 = 0; num2 <= num3; num2++)
			{
				Pack_Col_Left(num2, ref MyGrid);
			}
		}
	}

	public void Pack_Row_Up(int RowIdx, ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int index = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int num10 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num9;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 521:
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
								goto IL_0019;
							case 6:
								goto IL_001e;
							case 7:
								goto IL_0023;
							case 8:
								goto IL_0030;
							case 9:
								goto IL_0035;
							case 10:
								goto IL_0042;
							case 11:
								goto IL_0074;
							case 12:
							case 13:
								goto IL_007f;
							case 14:
								goto IL_008a;
							case 15:
								goto IL_009b;
							case 16:
								goto IL_00aa;
							case 17:
								goto IL_00f2;
							case 18:
								goto IL_013a;
							case 19:
								goto IL_0161;
							case 20:
								goto IL_0188;
							default:
								goto end_IL_0001;
							case 21:
							case 22:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0042:
						num2 = 10;
						if (Operators.ConditionalCompareObjectEqual(MyGrid.Rows[num5].Cells[index].Value, "", TextCompare: false))
						{
							goto IL_0074;
						}
						goto IL_007f;
						IL_0074:
						num2 = 11;
						num6++;
						goto IL_007f;
						IL_0188:
						num2 = 20;
						num7++;
						goto IL_0191;
						IL_007f:
						num2 = 13;
						num5 += -1;
						goto IL_0086;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num7 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						num6 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						index = 0;
						goto IL_001e;
						IL_001e:
						num2 = 6;
						num8 = 0;
						goto IL_0023;
						IL_0023:
						num2 = 7;
						num8 = MyGrid.ColumnCount - 1;
						goto IL_0030;
						IL_0030:
						num2 = 8;
						num6 = 0;
						goto IL_0035;
						IL_0035:
						num2 = 9;
						num9 = RowIdx - 1;
						num5 = num9;
						goto IL_0086;
						IL_0086:
						if (num5 >= 0)
						{
							goto IL_0042;
						}
						goto IL_008a;
						IL_008a:
						num2 = 14;
						if (num6 <= 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_009b;
						IL_009b:
						num2 = 15;
						num10 = num8;
						num7 = 0;
						goto IL_0191;
						IL_0191:
						if (num7 > num10)
						{
							goto end_IL_0001_2;
						}
						goto IL_00aa;
						IL_00aa:
						num2 = 16;
						MyGrid.Rows[RowIdx - num6].Cells[num7].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[RowIdx].Cells[num7].Value);
						goto IL_00f2;
						IL_00f2:
						num2 = 17;
						MyGrid.Rows[RowIdx - num6].Cells[num7].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[RowIdx].Cells[num7].Tag);
						goto IL_013a;
						IL_013a:
						num2 = 18;
						MyGrid.Rows[RowIdx].Cells[num7].Value = "";
						goto IL_0161;
						IL_0161:
						num2 = 19;
						MyGrid.Rows[RowIdx].Cells[num7].Tag = "";
						goto IL_0188;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 521;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_2:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Pack_All_Rows_Up(ref DataGridView MyGrid)
	{
		int num = 0;
		int num2 = 0;
		checked
		{
			num = MyGrid.RowCount - 1;
			int num3 = num;
			for (num2 = 1; num2 <= num3; num2++)
			{
				Pack_Row_Up(num2, ref MyGrid);
			}
		}
	}

	public void Pack_All_Grid(ref DataGridView MyGrid)
	{
		if (Operators.CompareString(Strings.UCase(MyGrid.Name), "GRIDCHART", TextCompare: false) == 0)
		{
			Pack_All_Cols_Left(ref MyGrid);
			Pack_All_Rows_Up(ref MyGrid);
		}
		else
		{
			Pack_All_Rows_Up(ref MyGrid);
		}
	}

	public void GridDragDrop(ref DataGridView MyGrid, ref DataGridView MyOtherGrid, ref DragEventArgs e, ref string MyDragSource, ref ListBox MyListBox)
	{
		int try0001_dispatch = -1;
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
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					int num3 = 0;
					if ((Operators.CompareString(MyDragSource, "FILTER", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(MyGrid.Name), "GRIDCHART", TextCompare: false) == 0))
					{
						MyDragSource = "";
						goto end_IL_0001;
					}
					int num4 = -1;
					int num5 = -1;
					int num6 = -1;
					int num7 = -1;
					string text = "";
					string text2 = "";
					int num8 = 0;
					bool flag = false;
					if (Operators.CompareString(MyDragSource, Strings.UCase(MyGrid.Name), TextCompare: false) == 0)
					{
						flag = true;
					}
					Point p = new Point(e.X, e.Y);
					Point point = MyGrid.PointToClient(p);
					num4 = MyGrid.HitTest(point.X, point.Y).RowIndex;
					num5 = MyGrid.HitTest(point.X, point.Y).ColumnIndex;
					if (LikeOperator.LikeString(MyDragSource, "GRID*", CompareMethod.Binary))
					{
						text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
						num8 = Strings.InStr(text, "-");
						if (num8 == 0)
						{
							goto end_IL_0001;
						}
						checked
						{
							text2 = Strings.Mid(text, 1, num8 - 1);
							num6 = Conversions.ToInteger(text2);
							text2 = Strings.Mid(text, num8 + 1);
							num7 = Conversions.ToInteger(text2);
						}
						if (flag && num4 == num6 && num5 == num7)
						{
							goto end_IL_0001;
						}
					}
					if (num4 == -1 || num5 == -1)
					{
						goto end_IL_0001;
					}
					checked
					{
						if (Operators.ConditionalCompareObjectEqual(MyGrid.Rows[num4].Cells[num5].Value, "", TextCompare: false))
						{
							if ((Operators.CompareString(MyDragSource, "CHART", TextCompare: false) == 0) | (Operators.CompareString(MyDragSource, "FILTER", TextCompare: false) == 0))
							{
								MyGrid.Rows[num4].Cells[num5].Value = Conversions.ToString(e.Data.GetData(DataFormats.Text));
								if ((Operators.CompareString(MyDragSource, "CHART", TextCompare: false) == 0) | (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0))
								{
									MyGrid.Rows[num4].Cells[num5].Tag = Get_True_Label(Conversions.ToString(e.Data.GetData(DataFormats.Text)));
								}
								else
								{
									MyGrid.Rows[num4].Cells[num5].Tag = Get_True_Label(Conversions.ToString(e.Data.GetData(DataFormats.Text)));
								}
							}
							else if (flag)
							{
								MyGrid.Rows[num4].Cells[num5].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num6].Cells[num7].Value);
								MyGrid.Rows[num4].Cells[num5].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num6].Cells[num7].Tag);
							}
							else
							{
								MyGrid.Rows[num4].Cells[num5].Value = RuntimeHelpers.GetObjectValue(MyOtherGrid.Rows[num6].Cells[num7].Value);
								MyGrid.Rows[num4].Cells[num5].Tag = RuntimeHelpers.GetObjectValue(MyOtherGrid.Rows[num6].Cells[num7].Tag);
							}
						}
						else
						{
							if (!Operators.ConditionalCompareObjectEqual(MyGrid.Rows[num4].Cells[MyGrid.ColumnCount - 1].Value, "", TextCompare: false))
							{
								Interaction.MsgBox("The Object could not be inserted into the current cell as it is not possible to shift row values to the right", MsgBoxStyle.Exclamation, "Could not Insert Value");
								goto end_IL_0001;
							}
							int num9 = MyGrid.ColumnCount - 1;
							int num10 = num5 + 1;
							for (num3 = num9; num3 >= num10; num3 += -1)
							{
								MyGrid.Rows[num4].Cells[num3].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num3 - 1].Value);
								MyGrid.Rows[num4].Cells[num3].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num3 - 1].Tag);
							}
							text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
							if (LikeOperator.LikeString(MyDragSource, "GRID*", CompareMethod.Binary))
							{
								if (flag)
								{
									if (num4 == num6)
									{
										num7++;
									}
									MyGrid.Rows[num4].Cells[num5].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num6].Cells[num7].Value);
									MyGrid.Rows[num4].Cells[num5].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num6].Cells[num7].Tag);
								}
								else
								{
									MyGrid.Rows[num4].Cells[num5].Value = RuntimeHelpers.GetObjectValue(MyOtherGrid.Rows[num6].Cells[num7].Value);
									MyGrid.Rows[num4].Cells[num5].Tag = RuntimeHelpers.GetObjectValue(MyOtherGrid.Rows[num6].Cells[num7].Tag);
								}
							}
							else
							{
								MyGrid.Rows[num4].Cells[num5].Value = Conversions.ToString(e.Data.GetData(DataFormats.Text));
								if (LikeOperator.LikeString(MyDragSource, "*CHART*", CompareMethod.Binary) | (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0))
								{
									MyGrid.Rows[num4].Cells[num5].Tag = Get_True_Label(Conversions.ToString(e.Data.GetData(DataFormats.Text)));
								}
								else
								{
									MyGrid.Rows[num4].Cells[num5].Tag = Get_True_Label(Conversions.ToString(e.Data.GetData(DataFormats.Text)));
								}
							}
						}
						if (!((Operators.CompareString(MyDragSource, "CHART", TextCompare: false) == 0) | (Operators.CompareString(MyDragSource, "FILTER", TextCompare: false) == 0)))
						{
							if (flag)
							{
								MyGrid.Rows[num6].Cells[num7].Value = "";
								MyGrid.Rows[num6].Cells[num7].Tag = "";
							}
							else
							{
								MyOtherGrid.Rows[num6].Cells[num7].Value = "";
								MyOtherGrid.Rows[num6].Cells[num7].Tag = "";
							}
						}
						MyDragSource = "";
						Pack_All_Grid(ref MyGrid);
						Pack_All_Grid(ref MyOtherGrid);
						goto end_IL_0001;
					}
				}
				case 2134:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "GridModule - GridDragDrop", Information.Err().Description);
						Information.Err().Clear();
						MyDragSource = "";
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2134;
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

	public void InitiateDragDrop(ref DataGridView MyGrid, ref MouseEventArgs e)
	{
		int try0001_dispatch = -1;
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
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					int num3 = -1;
					int num4 = -1;
					if (MyGrid.RowCount != 0)
					{
						num3 = MyGrid.HitTest(e.X, e.Y).RowIndex;
						num4 = MyGrid.HitTest(e.X, e.Y).ColumnIndex;
						if (num3 > -1)
						{
							MyGrid.DoDragDrop(Conversions.ToString(num3) + "-" + Conversions.ToString(num4), DragDropEffects.Move);
						}
					}
					goto end_IL_0001;
				}
				case 179:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "FrmJMPWindow - InitiateDragDrop", Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 179;
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

	public void GridDragOver(ref DataGridView MyGrid, ref DragEventArgs e, string MyDragSource)
	{
		int try0001_dispatch = -1;
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
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					int num3 = -1;
					int num4 = -1;
					Point p = new Point(e.X, e.Y);
					Point point = MyGrid.PointToClient(p);
					num3 = MyGrid.HitTest(point.X, point.Y).RowIndex;
					num4 = MyGrid.HitTest(point.X, point.Y).ColumnIndex;
					if (num3 == -1 || num4 == -1)
					{
						e.Effect = DragDropEffects.None;
						goto end_IL_0001;
					}
					MyGrid.CurrentCell = MyGrid[num4, num3];
					e.Effect = DragDropEffects.Move;
					goto end_IL_0001;
				}
				case 202:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "GridModule - GridDragOver", Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 202;
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
		int num9 = default(int);
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
					case 258:
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
								goto IL_0019;
							case 6:
								goto IL_001e;
							case 7:
								goto IL_0045;
							case 8:
								goto IL_0063;
							case 9:
								goto IL_0072;
							case 10:
								goto IL_0084;
							case 11:
								goto IL_0091;
							case 12:
								goto IL_00ad;
							default:
								goto end_IL_0001;
							case 13:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_00b4:
						if (num5 > num6)
						{
							goto end_IL_0001_2;
						}
						goto IL_0091;
						IL_0091:
						num2 = 11;
						GridChart.Columns[num5].Width = num7;
						goto IL_00ad;
						IL_0084:
						num2 = 10;
						num6 = num8 - 1;
						num5 = 0;
						goto IL_00b4;
						IL_00ad:
						num2 = 12;
						num5++;
						goto IL_00b4;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num9 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						num7 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						num8 = 0;
						goto IL_001e;
						IL_001e:
						num2 = 6;
						GridFilter.Columns[0].Width = GridFilter.Width - 10;
						goto IL_0045;
						IL_0045:
						num2 = 7;
						num9 = GridChart.Width - GridChart.RowHeadersWidth - 25;
						goto IL_0063;
						IL_0063:
						num2 = 8;
						num8 = GridChart.ColumnCount;
						goto IL_0072;
						IL_0072:
						num2 = 9;
						num7 = (int)Math.Round((double)num9 / (double)num8);
						goto IL_0084;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 258;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_2:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmDisplay_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num9 = default(int);
		string MyCS = default(string);
		string MyLabel = default(string);
		int num10 = default(int);
		string text = default(string);
		string left = default(string);
		string[] array = default(string[]);
		int num11 = default(int);
		int num12 = default(int);
		int num13 = default(int);
		int num14 = default(int);
		int num15 = default(int);
		int num16 = default(int);
		string[] array2 = default(string[]);
		string[] array3 = default(string[]);
		int num17 = default(int);
		string[] array4 = default(string[]);
		int num18 = default(int);
		int num19 = default(int);
		int num20 = default(int);
		int num21 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Button MyButton;
					int num8;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 5366:
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
								goto IL_002b;
							case 4:
								goto IL_004b;
							case 5:
								goto IL_006b;
							case 6:
								goto IL_008b;
							case 7:
								goto IL_00a7;
							case 8:
								goto IL_00c7;
							case 9:
								goto IL_00cc;
							case 10:
								goto IL_00d2;
							case 11:
								goto IL_00d8;
							case 12:
								goto IL_00de;
							case 13:
								goto IL_00e8;
							case 14:
								goto IL_00f2;
							case 15:
								goto IL_00f8;
							case 16:
								goto IL_0102;
							case 17:
								goto IL_010c;
							case 18:
								goto IL_0129;
							case 20:
								goto IL_013c;
							case 21:
								goto IL_0159;
							case 22:
								goto IL_016d;
							case 23:
								goto IL_0181;
							case 24:
								goto IL_0191;
							case 19:
							case 25:
							case 26:
								goto IL_01a2;
							case 27:
								goto IL_01b8;
							case 28:
								goto IL_01cc;
							case 29:
								goto IL_01e2;
							case 30:
								goto IL_01f3;
							case 31:
								goto IL_020e;
							case 32:
								goto IL_0226;
							case 33:
							case 34:
								goto IL_0237;
							case 35:
								goto IL_023c;
							case 36:
								goto IL_0259;
							case 37:
								goto IL_0272;
							case 38:
								goto IL_028b;
							case 39:
							case 40:
								goto IL_02a6;
							case 41:
								goto IL_02bc;
							case 42:
								goto IL_02c2;
							case 43:
								goto IL_02d3;
							case 44:
								goto IL_02e5;
							case 45:
								goto IL_02eb;
							case 46:
								goto IL_0301;
							case 47:
								goto IL_0312;
							case 48:
								goto IL_032d;
							case 49:
								goto IL_0336;
							case 50:
							case 51:
								goto IL_0348;
							case 52:
							case 53:
								goto IL_0359;
							case 54:
								goto IL_035e;
							case 55:
								goto IL_036f;
							case 56:
								goto IL_0380;
							case 57:
								goto IL_039a;
							case 58:
								goto IL_03b1;
							case 59:
								goto IL_03dd;
							case 60:
								goto IL_0409;
							case 61:
								goto IL_0418;
							case 62:
								goto IL_042a;
							case 63:
								goto IL_0444;
							case 64:
								goto IL_045b;
							case 65:
								goto IL_0487;
							case 66:
								goto IL_04b3;
							case 67:
								goto IL_04c2;
							case 68:
								goto IL_04d4;
							case 69:
								goto IL_04eb;
							case 70:
								goto IL_0518;
							case 71:
								goto IL_0527;
							case 72:
								goto IL_053e;
							case 73:
								goto IL_055a;
							case 74:
								goto IL_0576;
							case 75:
								goto IL_0585;
							case 76:
								goto IL_05aa;
							case 77:
								goto IL_05c1;
							case 78:
								goto IL_05d8;
							case 79:
								goto IL_05f3;
							case 80:
							case 81:
							case 82:
								goto IL_0607;
							case 83:
								goto IL_0627;
							case 84:
								goto IL_062d;
							case 85:
								goto IL_0644;
							case 86:
								goto IL_0657;
							case 87:
								goto IL_066e;
							case 88:
								goto IL_0681;
							case 89:
								goto IL_0698;
							case 90:
								goto IL_06a6;
							case 91:
								goto IL_06d1;
							case 92:
								goto IL_06ea;
							case 93:
								goto IL_06fa;
							case 94:
							case 95:
							case 96:
								goto IL_0704;
							case 97:
								goto IL_071b;
							case 98:
								goto IL_0729;
							case 99:
								goto IL_0750;
							case 100:
								goto IL_0777;
							case 101:
							case 102:
							case 103:
								goto IL_0781;
							case 104:
								goto IL_079b;
							case 105:
								goto IL_07a9;
							case 106:
								goto IL_07d3;
							case 107:
								goto IL_07f0;
							case 108:
								goto IL_0809;
							case 109:
								goto IL_0819;
							case 110:
							case 111:
							case 112:
								goto IL_0823;
							case 113:
								goto IL_083a;
							case 114:
								goto IL_0843;
							case 115:
								goto IL_086d;
							case 116:
								goto IL_0889;
							case 117:
								goto IL_089a;
							case 118:
							case 119:
							case 120:
								goto IL_08a4;
							case 121:
								goto IL_08bb;
							case 122:
								goto IL_08c4;
							case 123:
								goto IL_08ee;
							case 124:
								goto IL_090a;
							case 125:
								goto IL_091b;
							case 126:
							case 127:
							case 128:
								goto IL_0925;
							case 129:
								goto IL_093f;
							case 130:
								goto IL_094b;
							case 131:
								goto IL_0978;
							case 135:
								goto IL_098a;
							case 136:
								goto IL_099b;
							case 137:
								goto IL_09b2;
							case 138:
								goto IL_09c8;
							case 139:
								goto IL_09de;
							case 141:
								goto IL_09eb;
							case 132:
							case 133:
							case 134:
							case 140:
							case 142:
							case 143:
							case 144:
								goto IL_09f6;
							case 145:
								goto IL_0a14;
							case 146:
								goto IL_0a2d;
							case 147:
								goto IL_0a45;
							case 148:
								goto IL_0a5e;
							case 149:
								goto IL_0a76;
							case 150:
								goto IL_0a95;
							case 151:
								goto IL_0aae;
							case 152:
								goto IL_0af5;
							case 154:
								goto IL_0b34;
							case 155:
								goto IL_0b53;
							case 156:
								goto IL_0b6c;
							case 157:
								goto IL_0bb3;
							case 159:
								goto IL_0bf2;
							case 160:
								goto IL_0c11;
							case 161:
								goto IL_0c2b;
							case 162:
								goto IL_0c72;
							case 164:
								goto IL_0cb2;
							case 165:
								goto IL_0ccd;
							case 166:
								goto IL_0ce9;
							case 167:
								goto IL_0cfd;
							case 168:
								goto IL_0d29;
							case 153:
							case 158:
							case 163:
							case 169:
							case 170:
								goto IL_0d63;
							case 171:
								goto IL_0d78;
							case 172:
								goto IL_0d81;
							case 173:
								goto IL_0d96;
							case 174:
							case 175:
								goto IL_0da1;
							case 176:
								goto IL_0dc1;
							case 177:
								goto IL_0ddc;
							case 178:
								goto IL_0df4;
							case 179:
								goto IL_0e0d;
							case 180:
								goto IL_0e25;
							case 181:
								goto IL_0e44;
							case 182:
								goto IL_0e5d;
							case 183:
								goto IL_0ea4;
							case 185:
								goto IL_0ee3;
							case 186:
								goto IL_0f02;
							case 187:
								goto IL_0f1b;
							case 188:
								goto IL_0f62;
							case 190:
								goto IL_0fa1;
							case 191:
								goto IL_0fc0;
							case 192:
								goto IL_0fda;
							case 193:
								goto IL_1021;
							case 195:
								goto IL_1061;
							case 196:
								goto IL_107c;
							case 197:
								goto IL_1098;
							case 198:
								goto IL_10ac;
							case 199:
								goto IL_10d8;
							case 184:
							case 189:
							case 194:
							case 200:
							case 201:
								goto IL_1112;
							case 202:
								goto IL_1127;
							case 203:
								goto IL_1130;
							case 204:
								goto IL_1145;
							case 205:
							case 206:
							case 207:
								goto IL_1152;
							case 208:
								goto IL_115f;
							case 209:
								goto IL_1177;
							case 210:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 211:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0418:
						num2 = 61;
						num5++;
						goto IL_0421;
						IL_03b1:
						num2 = 58;
						GridChart.Rows[num5].Cells[num6].Value = "";
						goto IL_03dd;
						IL_0412:
						if (num6 <= num7)
						{
							goto IL_03b1;
						}
						goto IL_0418;
						IL_03dd:
						num2 = 59;
						GridChart.Rows[num5].Cells[num6].Tag = "";
						goto IL_0409;
						IL_000b:
						num2 = 2;
						MyButton = CmdCancel;
						BuildForm.Set_Btn_Img(ref MyButton, "cancel");
						CmdCancel = MyButton;
						goto IL_002b;
						IL_002b:
						num2 = 3;
						MyButton = CmdOK;
						BuildForm.Set_Btn_Img(ref MyButton, "ok");
						CmdOK = MyButton;
						goto IL_004b;
						IL_004b:
						num2 = 4;
						MyButton = CmdBrowse1;
						BuildForm.Set_Btn_Img(ref MyButton, "browse");
						CmdBrowse1 = MyButton;
						goto IL_006b;
						IL_006b:
						num2 = 5;
						MyButton = CmdBrowse2;
						BuildForm.Set_Btn_Img(ref MyButton, "browse");
						CmdBrowse2 = MyButton;
						goto IL_008b;
						IL_008b:
						num2 = 6;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
						{
							goto IL_00a7;
						}
						goto IL_00c7;
						IL_00a7:
						num2 = 7;
						MyButton = CmdBrowse3;
						BuildForm.Set_Btn_Img(ref MyButton, "browse");
						CmdBrowse3 = MyButton;
						goto IL_00c7;
						IL_00c7:
						num2 = 8;
						num5 = 0;
						goto IL_00cc;
						IL_00cc:
						num2 = 9;
						num6 = 0;
						goto IL_00d2;
						IL_00d2:
						num2 = 10;
						num8 = 0;
						goto IL_00d8;
						IL_00d8:
						num2 = 11;
						num9 = 0;
						goto IL_00de;
						IL_00de:
						num2 = 12;
						MyCS = "";
						goto IL_00e8;
						IL_00e8:
						num2 = 13;
						MyLabel = "";
						goto IL_00f2;
						IL_00f2:
						num2 = 14;
						num10 = 0;
						goto IL_00f8;
						IL_00f8:
						num2 = 15;
						text = "N";
						goto IL_0102;
						IL_0102:
						num2 = 16;
						left = "";
						goto IL_010c;
						IL_010c:
						num2 = 17;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
						{
							goto IL_0129;
						}
						goto IL_013c;
						IL_0129:
						num2 = 18;
						ChkEmbedCSS.Visible = true;
						goto IL_01a2;
						IL_013c:
						num2 = 20;
						if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0)
						{
							goto IL_0159;
						}
						goto IL_01a2;
						IL_0159:
						num2 = 21;
						TxtHTMOut.Text = "";
						goto IL_016d;
						IL_016d:
						num2 = 22;
						TxtCSSOut.Text = "";
						goto IL_0181;
						IL_0181:
						num2 = 23;
						mnuOpt.Visible = false;
						goto IL_0191;
						IL_0191:
						num2 = 24;
						optSP.Enabled = false;
						goto IL_01a2;
						IL_01a2:
						num2 = 26;
						array = Strings.Split(f_Charts, ",");
						goto IL_01b8;
						IL_01b8:
						num2 = 27;
						LstChart.Items.Clear();
						goto IL_01cc;
						IL_01cc:
						num2 = 28;
						if (Information.UBound(array) >= 0)
						{
							goto IL_01e2;
						}
						goto IL_0237;
						IL_01e2:
						num2 = 29;
						num11 = Information.UBound(array);
						num5 = 0;
						goto IL_022f;
						IL_022f:
						if (num5 <= num11)
						{
							goto IL_01f3;
						}
						goto IL_0237;
						IL_01f3:
						num2 = 30;
						if (Operators.CompareString(array[num5], "", TextCompare: false) != 0)
						{
							goto IL_020e;
						}
						goto IL_0226;
						IL_020e:
						num2 = 31;
						LstChart.Items.Add(array[num5]);
						goto IL_0226;
						IL_0226:
						num2 = 32;
						num5++;
						goto IL_022f;
						IL_0237:
						num2 = 34;
						array = null;
						goto IL_023c;
						IL_023c:
						num2 = 35;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
						{
							goto IL_0259;
						}
						goto IL_02a6;
						IL_0259:
						num2 = 36;
						LstChart.Items.Add("Text:");
						goto IL_0272;
						IL_0272:
						num2 = 37;
						LstChart.Items.Add("Images:1");
						goto IL_028b;
						IL_028b:
						num2 = 38;
						LstChart.Items.Add("JMP-HTML:1");
						goto IL_02a6;
						IL_02a6:
						num2 = 40;
						array = Strings.Split(f_ChartsActual, ",");
						goto IL_02bc;
						IL_02bc:
						num2 = 41;
						num5 = 0;
						goto IL_02c2;
						IL_02c2:
						num2 = 42;
						l_ChartsActual[num5] = "";
						goto IL_02d3;
						IL_02d3:
						num2 = 43;
						num5++;
						if (num5 <= 999)
						{
							goto IL_02c2;
						}
						goto IL_02e5;
						IL_02e5:
						num2 = 44;
						num6 = -1;
						goto IL_02eb;
						IL_02eb:
						num2 = 45;
						if (Information.UBound(array) >= 0)
						{
							goto IL_0301;
						}
						goto IL_0359;
						IL_0301:
						num2 = 46;
						num12 = Information.UBound(array);
						num5 = 0;
						goto IL_0351;
						IL_0351:
						if (num5 <= num12)
						{
							goto IL_0312;
						}
						goto IL_0359;
						IL_0312:
						num2 = 47;
						if (Operators.CompareString(array[num5], "", TextCompare: false) != 0)
						{
							goto IL_032d;
						}
						goto IL_0348;
						IL_032d:
						num2 = 48;
						num6++;
						goto IL_0336;
						IL_0336:
						num2 = 49;
						l_ChartsActual[num6] = array[num5];
						goto IL_0348;
						IL_0348:
						num2 = 51;
						num5++;
						goto IL_0351;
						IL_0359:
						num2 = 53;
						array = null;
						goto IL_035e;
						IL_035e:
						num2 = 54;
						GridChart.RowCount = 50;
						goto IL_036f;
						IL_036f:
						num2 = 55;
						GridFilter.RowCount = 50;
						goto IL_0380;
						IL_0380:
						num2 = 56;
						num13 = GridChart.RowCount - 1;
						num5 = 0;
						goto IL_0421;
						IL_0421:
						if (num5 <= num13)
						{
							goto IL_039a;
						}
						goto IL_042a;
						IL_042a:
						num2 = 62;
						num14 = GridFilter.RowCount - 1;
						num5 = 0;
						goto IL_04cb;
						IL_04cb:
						if (num5 <= num14)
						{
							goto IL_0444;
						}
						goto IL_04d4;
						IL_04d4:
						num2 = 68;
						num15 = GridChart.RowCount - 1;
						num5 = 0;
						goto IL_0521;
						IL_0521:
						if (num5 <= num15)
						{
							goto IL_04eb;
						}
						goto IL_0527;
						IL_0527:
						num2 = 71;
						num16 = GridChart.ColumnCount - 1;
						num5 = 0;
						goto IL_057f;
						IL_057f:
						if (num5 <= num16)
						{
							goto IL_053e;
						}
						goto IL_0585;
						IL_0585:
						num2 = 75;
						if (Operators.CompareString(Strings.Trim(f_JSLScript), "", TextCompare: false) != 0)
						{
							goto IL_05aa;
						}
						goto IL_1152;
						IL_05aa:
						num2 = 76;
						array2 = Strings.Split(f_JSLScript, "@@@");
						goto IL_05c1;
						IL_05c1:
						num2 = 77;
						if (Information.UBound(array2) >= 0)
						{
							goto IL_05d8;
						}
						goto IL_0607;
						IL_05d8:
						num2 = 78;
						if (Operators.CompareString(array2[0], "1", TextCompare: false) == 0)
						{
							goto IL_05f3;
						}
						goto IL_0607;
						IL_05f3:
						num2 = 79;
						ChkReverse.Checked = true;
						goto IL_0607;
						IL_0607:
						num2 = 82;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
						{
							goto IL_0627;
						}
						goto IL_098a;
						IL_0627:
						num2 = 83;
						num9 = 3;
						goto IL_062d;
						IL_062d:
						num2 = 84;
						if (Information.UBound(array2) >= 1)
						{
							goto IL_0644;
						}
						goto IL_0657;
						IL_0644:
						num2 = 85;
						TxtHTMOut.Text = array2[1];
						goto IL_0657;
						IL_0657:
						num2 = 86;
						if (Information.UBound(array2) >= 2)
						{
							goto IL_066e;
						}
						goto IL_0681;
						IL_066e:
						num2 = 87;
						TxtCSSOut.Text = array2[2];
						goto IL_0681;
						IL_0681:
						num2 = 88;
						if (Information.UBound(array2) >= 3)
						{
							goto IL_0698;
						}
						goto IL_0704;
						IL_0698:
						num2 = 89;
						text = Strings.UCase(array2[3]);
						goto IL_06a6;
						IL_06a6:
						num2 = 90;
						if (Operators.CompareString(text, "E->N", TextCompare: false) == 0 || Operators.CompareString(text, "E->Y", TextCompare: false) == 0)
						{
							goto IL_06d1;
						}
						goto IL_0704;
						IL_0409:
						num2 = 60;
						num6++;
						goto IL_0412;
						IL_06d1:
						num2 = 91;
						if (Operators.CompareString(text, "E->Y", TextCompare: false) == 0)
						{
							goto IL_06ea;
						}
						goto IL_06fa;
						IL_06ea:
						num2 = 92;
						ChkEmbedCSS.Checked = true;
						goto IL_06fa;
						IL_06fa:
						num2 = 93;
						num9 = 4;
						goto IL_0704;
						IL_0704:
						num2 = 96;
						if (Information.UBound(array2) >= 4)
						{
							goto IL_071b;
						}
						goto IL_0781;
						IL_071b:
						num2 = 97;
						text = Strings.UCase(array2[4]);
						goto IL_0729;
						IL_0729:
						num2 = 98;
						if (LikeOperator.LikeString(Strings.Mid(text + "    ", 1, 4), "RR->", CompareMethod.Binary))
						{
							goto IL_0750;
						}
						goto IL_0781;
						IL_0750:
						num2 = 99;
						mnuRR.Text = General_Procedures.Set_Node_Value(mnuRR.Text, Strings.Mid(text, 5));
						goto IL_0777;
						IL_0777:
						num2 = 100;
						num9 = 5;
						goto IL_0781;
						IL_0781:
						num2 = 103;
						if (Information.UBound(array2) >= 5)
						{
							goto IL_079b;
						}
						goto IL_0823;
						IL_079b:
						num2 = 104;
						text = Strings.UCase(array2[5]);
						goto IL_07a9;
						IL_07a9:
						num2 = 105;
						if (Operators.CompareString(Strings.Mid(text + "   ", 1, 3), "B->", TextCompare: false) == 0)
						{
							goto IL_07d3;
						}
						goto IL_0823;
						IL_07d3:
						num2 = 106;
						left = Strings.UCase(Strings.Mid(text + " ", 4, 1));
						goto IL_07f0;
						IL_07f0:
						num2 = 107;
						if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
						{
							goto IL_0809;
						}
						goto IL_0819;
						IL_0809:
						num2 = 108;
						mnudisplaywin.Checked = true;
						goto IL_0819;
						IL_0819:
						num2 = 109;
						num9 = 6;
						goto IL_0823;
						IL_0823:
						num2 = 112;
						if (Information.UBound(array2) >= 6)
						{
							goto IL_083a;
						}
						goto IL_08a4;
						IL_083a:
						num2 = 113;
						text = array2[6];
						goto IL_0843;
						IL_0843:
						num2 = 114;
						if (Operators.CompareString(Strings.Mid(text + "      ", 1, 6), "EM-A->", TextCompare: false) == 0)
						{
							goto IL_086d;
						}
						goto IL_08a4;
						IL_086d:
						num2 = 115;
						left = Strings.Trim(Strings.Mid(text + " ", 7));
						goto IL_0889;
						IL_0889:
						num2 = 116;
						TxtAttach.Text = left;
						goto IL_089a;
						IL_089a:
						num2 = 117;
						num9 = 7;
						goto IL_08a4;
						IL_08a4:
						num2 = 120;
						if (Information.UBound(array2) >= 7)
						{
							goto IL_08bb;
						}
						goto IL_0925;
						IL_08bb:
						num2 = 121;
						text = array2[7];
						goto IL_08c4;
						IL_08c4:
						num2 = 122;
						if (Operators.CompareString(Strings.Mid(text + "      ", 1, 6), "EM-S->", TextCompare: false) == 0)
						{
							goto IL_08ee;
						}
						goto IL_0925;
						IL_08ee:
						num2 = 123;
						left = Strings.Trim(Strings.Mid(text + " ", 7));
						goto IL_090a;
						IL_090a:
						num2 = 124;
						cmbrole.Text = left;
						goto IL_091b;
						IL_091b:
						num2 = 125;
						num9 = 8;
						goto IL_0925;
						IL_0925:
						num2 = 128;
						if (Information.UBound(array2) >= 8)
						{
							goto IL_093f;
						}
						goto IL_09f6;
						IL_093f:
						num2 = 129;
						text = array2[8];
						goto IL_094b;
						IL_094b:
						num2 = 130;
						if (Operators.CompareString(Strings.Mid(text + "     ", 1, 5), "SEC->", TextCompare: false) == 0)
						{
							goto IL_0978;
						}
						goto IL_09f6;
						IL_0978:
						num2 = 131;
						num9 = 9;
						goto IL_09f6;
						IL_098a:
						num2 = 135;
						f_ScriptType = "JMP";
						goto IL_099b;
						IL_099b:
						num2 = 136;
						if (Information.UBound(array2) == 4)
						{
							goto IL_09b2;
						}
						goto IL_09eb;
						IL_09b2:
						num2 = 137;
						TxtHTMOut.Text = array2[1];
						goto IL_09c8;
						IL_09c8:
						num2 = 138;
						TxtCSSOut.Text = array2[2];
						goto IL_09de;
						IL_09de:
						num2 = 139;
						num9 = 3;
						goto IL_09f6;
						IL_09eb:
						num2 = 141;
						num9 = 1;
						goto IL_09f6;
						IL_09f6:
						num2 = 144;
						if (Information.UBound(array2) >= num9)
						{
							goto IL_0a14;
						}
						goto IL_0da1;
						IL_0a14:
						num2 = 145;
						array3 = Strings.Split(array2[num9], "|");
						goto IL_0a2d;
						IL_0a2d:
						num2 = 146;
						num17 = Information.UBound(array3);
						num5 = 0;
						goto IL_0d8d;
						IL_0d8d:
						if (num5 <= num17)
						{
							goto IL_0a45;
						}
						goto IL_0d96;
						IL_0d96:
						num2 = 173;
						array3 = null;
						goto IL_0da1;
						IL_0a45:
						num2 = 147;
						array4 = Strings.Split(array3[num5], ";");
						goto IL_0a5e;
						IL_0a5e:
						num2 = 148;
						num18 = Information.UBound(array4);
						num6 = 0;
						goto IL_0d6f;
						IL_0d6f:
						if (num6 <= num18)
						{
							goto IL_0a76;
						}
						goto IL_0d78;
						IL_0d78:
						num2 = 171;
						array4 = null;
						goto IL_0d81;
						IL_0d81:
						num2 = 172;
						num5++;
						goto IL_0d8d;
						IL_0a76:
						num2 = 149;
						if (LikeOperator.LikeString(array4[num6], "Text:*:*", CompareMethod.Binary))
						{
							goto IL_0a95;
						}
						goto IL_0b34;
						IL_0a95:
						num2 = 150;
						num10 = Strings.InStr(6, array4[num6], ":");
						goto IL_0aae;
						IL_0aae:
						num2 = 151;
						GridChart.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_0af5;
						IL_0af5:
						num2 = 152;
						GridChart.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_0d63;
						IL_0b34:
						num2 = 154;
						if (LikeOperator.LikeString(array4[num6], "Images:*:*", CompareMethod.Binary))
						{
							goto IL_0b53;
						}
						goto IL_0bf2;
						IL_0b53:
						num2 = 155;
						num10 = Strings.InStr(8, array4[num6], ":");
						goto IL_0b6c;
						IL_0b6c:
						num2 = 156;
						GridChart.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_0bb3;
						IL_0bb3:
						num2 = 157;
						GridChart.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_0d63;
						IL_0bf2:
						num2 = 159;
						if (LikeOperator.LikeString(array4[num6], "JMP-HTML:*:*", CompareMethod.Binary))
						{
							goto IL_0c11;
						}
						goto IL_0cb2;
						IL_0c11:
						num2 = 160;
						num10 = Strings.InStr(10, array4[num6], ":");
						goto IL_0c2b;
						IL_0c2b:
						num2 = 161;
						GridChart.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_0c72;
						IL_0c72:
						num2 = 162;
						GridChart.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_0d63;
						IL_0cb2:
						num2 = 164;
						BuildChart.Get_CS_and_Label(f_ScriptType, array4[num6], ref MyCS, ref MyLabel);
						goto IL_0ccd;
						IL_0ccd:
						num2 = 165;
						if (Operators.CompareString(MyCS, "", TextCompare: false) != 0)
						{
							goto IL_0ce9;
						}
						goto IL_0cfd;
						IL_0ce9:
						num2 = 166;
						MyCS = ":" + MyCS;
						goto IL_0cfd;
						IL_0cfd:
						num2 = 167;
						GridChart.Rows[num5].Cells[num6].Tag = MyLabel;
						goto IL_0d29;
						IL_0d29:
						num2 = 168;
						GridChart.Rows[num5].Cells[num6].Value = Get_Display_Label(MyLabel) + MyCS;
						goto IL_0d63;
						IL_0d63:
						num2 = 170;
						num6++;
						goto IL_0d6f;
						IL_0da1:
						num2 = 175;
						if (Information.UBound(array2) >= num9 + 1)
						{
							goto IL_0dc1;
						}
						goto IL_1152;
						IL_0dc1:
						num2 = 176;
						array3 = Strings.Split(array2[num9 + 1], "|");
						goto IL_0ddc;
						IL_0ddc:
						num2 = 177;
						num19 = Information.UBound(array3);
						num5 = 0;
						goto IL_113c;
						IL_113c:
						if (num5 <= num19)
						{
							goto IL_0df4;
						}
						goto IL_1145;
						IL_1145:
						num2 = 204;
						array3 = null;
						goto IL_1152;
						IL_0df4:
						num2 = 178;
						array4 = Strings.Split(array3[num5], ";");
						goto IL_0e0d;
						IL_0e0d:
						num2 = 179;
						num20 = Information.UBound(array4);
						num6 = 0;
						goto IL_111e;
						IL_111e:
						if (num6 <= num20)
						{
							goto IL_0e25;
						}
						goto IL_1127;
						IL_1127:
						num2 = 202;
						array4 = null;
						goto IL_1130;
						IL_1130:
						num2 = 203;
						num5++;
						goto IL_113c;
						IL_0e25:
						num2 = 180;
						if (LikeOperator.LikeString(array4[num6], "Text:*:*", CompareMethod.Binary))
						{
							goto IL_0e44;
						}
						goto IL_0ee3;
						IL_0e44:
						num2 = 181;
						num10 = Strings.InStr(6, array4[num6], ":");
						goto IL_0e5d;
						IL_0e5d:
						num2 = 182;
						GridFilter.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_0ea4;
						IL_0ea4:
						num2 = 183;
						GridFilter.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_1112;
						IL_0ee3:
						num2 = 185;
						if (LikeOperator.LikeString(array4[num6], "Images:*:*", CompareMethod.Binary))
						{
							goto IL_0f02;
						}
						goto IL_0fa1;
						IL_0f02:
						num2 = 186;
						num10 = Strings.InStr(8, array4[num6], ":");
						goto IL_0f1b;
						IL_0f1b:
						num2 = 187;
						GridFilter.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_0f62;
						IL_0f62:
						num2 = 188;
						GridFilter.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_1112;
						IL_0fa1:
						num2 = 190;
						if (LikeOperator.LikeString(array4[num6], "JMP-HTML:*:*", CompareMethod.Binary))
						{
							goto IL_0fc0;
						}
						goto IL_1061;
						IL_0fc0:
						num2 = 191;
						num10 = Strings.InStr(10, array4[num6], ":");
						goto IL_0fda;
						IL_0fda:
						num2 = 192;
						GridFilter.Rows[num5].Cells[num6].Tag = Strings.Trim(Strings.Mid(array4[num6] + " ", num10 + 1));
						goto IL_1021;
						IL_1021:
						num2 = 193;
						GridFilter.Rows[num5].Cells[num6].Value = Strings.Mid(array4[num6], 1, num10 - 1);
						goto IL_1112;
						IL_1061:
						num2 = 195;
						BuildChart.Get_CS_and_Label(f_ScriptType, array4[num6], ref MyCS, ref MyLabel);
						goto IL_107c;
						IL_107c:
						num2 = 196;
						if (Operators.CompareString(MyCS, "", TextCompare: false) != 0)
						{
							goto IL_1098;
						}
						goto IL_10ac;
						IL_1098:
						num2 = 197;
						MyCS = ":" + MyCS;
						goto IL_10ac;
						IL_10ac:
						num2 = 198;
						GridFilter.Rows[num5].Cells[num6].Tag = MyLabel;
						goto IL_10d8;
						IL_10d8:
						num2 = 199;
						GridFilter.Rows[num5].Cells[num6].Value = Get_Display_Label(MyLabel) + MyCS;
						goto IL_1112;
						IL_1112:
						num2 = 201;
						num6++;
						goto IL_111e;
						IL_1152:
						num2 = 207;
						Set_Col_Width();
						goto IL_115f;
						IL_115f:
						num2 = 208;
						Get_Opt(TxtHTMOut.Text);
						goto IL_1177;
						IL_1177:
						num2 = 209;
						Set_Opt();
						break;
						IL_053e:
						num2 = 72;
						GridChart.Columns[num5].SortMode = DataGridViewColumnSortMode.NotSortable;
						goto IL_055a;
						IL_055a:
						num2 = 73;
						GridChart.Columns[num5].Resizable = DataGridViewTriState.True;
						goto IL_0576;
						IL_0576:
						num2 = 74;
						num5++;
						goto IL_057f;
						IL_04eb:
						num2 = 69;
						GridChart.Rows[num5].HeaderCell.Value = (num5 + 1).ToString();
						goto IL_0518;
						IL_0518:
						num2 = 70;
						num5++;
						goto IL_0521;
						IL_0444:
						num2 = 63;
						num21 = GridFilter.ColumnCount - 1;
						num6 = 0;
						goto IL_04bc;
						IL_04bc:
						if (num6 <= num21)
						{
							goto IL_045b;
						}
						goto IL_04c2;
						IL_04c2:
						num2 = 67;
						num5++;
						goto IL_04cb;
						IL_045b:
						num2 = 64;
						GridFilter.Rows[num5].Cells[num6].Value = "";
						goto IL_0487;
						IL_0487:
						num2 = 65;
						GridFilter.Rows[num5].Cells[num6].Tag = "";
						goto IL_04b3;
						IL_04b3:
						num2 = 66;
						num6++;
						goto IL_04bc;
						IL_039a:
						num2 = 57;
						num7 = GridChart.ColumnCount - 1;
						num6 = 0;
						goto IL_0412;
						end_IL_0001_2:
						break;
					}
					num2 = 210;
					Cursor.Current = Cursors.Default;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5366;
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

	private void GridFilter_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		Set_Col_Width();
	}

	private void GridFilter_DoubleClick(object sender, EventArgs e)
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
				case 56:
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
				Get_Text2(GridFilter);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 56;
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

	private void GridFilter_DragDrop(object sender, DragEventArgs e)
	{
		DataGridView MyGrid = (DataGridView)sender;
		DataGridView MyOtherGrid = GridChart;
		ref string myDragSource = ref MyDragSource;
		ListBox MyListBox = LstChart;
		GridDragDrop(ref MyGrid, ref MyOtherGrid, ref e, ref myDragSource, ref MyListBox);
		LstChart = MyListBox;
		GridChart = MyOtherGrid;
		sender = MyGrid;
	}

	private void GridFilter_DragEnter(object sender, DragEventArgs e)
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
				case 56:
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
				e.Effect = e.AllowedEffect;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 56;
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

	private void GridFilter_DragOver(object sender, DragEventArgs e)
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
				case 75:
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
				DataGridView MyGrid = GridFilter;
				GridDragOver(ref MyGrid, ref e, MyDragSource);
				GridFilter = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 75;
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

	private void GridFilter_MouseDown(object sender, MouseEventArgs e)
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
				case 127:
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
							goto IL_0028;
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
					if (!((e.Button == MouseButtons.Left) & (e.Clicks == 1)))
					{
						goto end_IL_0001_3;
					}
					goto IL_0028;
					IL_0028:
					num2 = 3;
					MyDragSource = "GRIDFILTER";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView MyGrid = GridFilter;
				InitiateDragDrop(ref MyGrid, ref e);
				GridFilter = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 127;
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

	private void GridFilter_Resize(object sender, EventArgs e)
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

	private void GridChart_DoubleClick(object sender, EventArgs e)
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
				case 56:
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
				Get_Text2(GridChart);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 56;
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

	private void GridChart_DragDrop(object sender, DragEventArgs e)
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
				case 108:
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
				DataGridView MyGrid = (DataGridView)sender;
				DataGridView MyOtherGrid = GridFilter;
				ref string myDragSource = ref MyDragSource;
				ListBox MyListBox = LstChart;
				GridDragDrop(ref MyGrid, ref MyOtherGrid, ref e, ref myDragSource, ref MyListBox);
				LstChart = MyListBox;
				GridFilter = MyOtherGrid;
				sender = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 108;
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

	private void GridChart_DragEnter(object sender, DragEventArgs e)
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
				case 56:
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
				e.Effect = e.AllowedEffect;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 56;
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

	private void GridChart_DragOver(object sender, DragEventArgs e)
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
				checked
				{
					DataGridView MyGrid;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 668:
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
								goto IL_002d;
							case 4:
								goto IL_0089;
							case 6:
								goto IL_00aa;
							case 7:
								goto IL_011e;
							case 5:
							case 8:
							case 9:
								goto IL_013a;
							case 10:
								goto IL_0197;
							case 12:
								goto IL_01b9;
							case 13:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 11:
							case 14:
							case 15:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_013a:
						num2 = 9;
						if (e.X <= PointToScreen(new Point(SplitContainer1.Panel2.Location.X, SplitContainer1.Panel2.Location.Y)).X + 75)
						{
							goto IL_0197;
						}
						goto IL_01b9;
						IL_0197:
						num2 = 10;
						GridChart.FirstDisplayedScrollingColumnIndex -= 1;
						goto end_IL_0001_3;
						IL_011e:
						num2 = 7;
						GridChart.FirstDisplayedScrollingRowIndex += 1;
						goto IL_013a;
						IL_01b9:
						num2 = 12;
						if (e.X < PointToScreen(new Point(SplitContainer1.Panel2.Location.X + GridChart.Width, SplitContainer1.Panel2.Location.X + GridChart.Width)).X - 55)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_000b:
						num2 = 2;
						MyGrid = GridChart;
						GridDragOver(ref MyGrid, ref e, MyDragSource);
						GridChart = MyGrid;
						goto IL_002d;
						IL_002d:
						num2 = 3;
						if (e.Y <= PointToScreen(new Point(SplitContainer1.Panel2.Location.X, SplitContainer1.Panel2.Location.Y)).Y + 30)
						{
							goto IL_0089;
						}
						goto IL_00aa;
						IL_0089:
						num2 = 4;
						GridChart.FirstDisplayedScrollingRowIndex -= 1;
						goto IL_013a;
						IL_00aa:
						num2 = 6;
						if (e.Y >= PointToScreen(new Point(SplitContainer1.Panel2.Location.X + GridChart.Width, SplitContainer1.Panel2.Location.Y + GridChart.Height)).Y - 55)
						{
							goto IL_011e;
						}
						goto IL_013a;
						end_IL_0001_2:
						break;
					}
					num2 = 13;
					GridChart.FirstDisplayedScrollingColumnIndex += 1;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 668;
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

	private void LstChart_DragDrop(object sender, DragEventArgs e)
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
				ref string myDragSource;
				DataGridView MyGrid;
				ListBox MyListBox;
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
							goto IL_0025;
						case 5:
							goto IL_005f;
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
					if (Operators.CompareString(MyDragSource, "GRIDCHART", TextCompare: false) == 0)
					{
						goto IL_0025;
					}
					goto IL_005f;
					IL_0025:
					num2 = 3;
					MyGrid = GridChart;
					myDragSource = ref MyDragSource;
					MyListBox = LstChart;
					LstBoxDragDrop(ref MyGrid, ref e, ref myDragSource, ref MyListBox);
					LstChart = MyListBox;
					GridChart = MyGrid;
					goto end_IL_0001_3;
					IL_005f:
					num2 = 5;
					if (Operators.CompareString(MyDragSource, "GRIDFILTER", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				MyGrid = GridFilter;
				ref string myDragSource2 = ref MyDragSource;
				MyListBox = LstChart;
				LstBoxDragDrop(ref MyGrid, ref e, ref myDragSource2, ref MyListBox);
				LstChart = MyListBox;
				GridFilter = MyGrid;
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

	private void LstChart_DragEnter(object sender, DragEventArgs e)
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
				case 56:
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
				e.Effect = e.AllowedEffect;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 56;
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

	private void LstChart_MouseDown(object sender, MouseEventArgs e)
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
				case 159:
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
							goto IL_002d;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (LstChart.IndexFromPoint(e.X, e.Y) == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_002d;
					IL_002d:
					num2 = 4;
					MyDragSource = "CHART";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				LstChart.DoDragDrop(LstChart.Items[LstChart.IndexFromPoint(e.X, e.Y)].ToString(), DragDropEffects.Move);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 159;
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

	private void GridChart_MouseDown(object sender, MouseEventArgs e)
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
							goto IL_002a;
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
					if (e.Button != MouseButtons.Left || e.Clicks != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_002a;
					IL_002a:
					num2 = 3;
					MyDragSource = "GRIDCHART";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView MyGrid = GridChart;
				InitiateDragDrop(ref MyGrid, ref e);
				GridChart = MyGrid;
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

	private void CmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string MyCS = default(string);
		string MyLabel = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		int num7 = default(int);
		bool flag = default(bool);
		bool flag2 = default(bool);
		bool flag3 = default(bool);
		bool flag4 = default(bool);
		bool flag5 = default(bool);
		string text6 = default(string);
		string value = default(string);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		int num11 = default(int);
		int num12 = default(int);
		int num14 = default(int);
		int num15 = default(int);
		int num16 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Type typeFromHandle;
					object[] obj;
					bool[] obj2;
					object source;
					int num13;
					Type typeFromHandle3;
					object[] obj5;
					bool[] obj6;
					object source3;
					DataGridViewCell dataGridViewCell;
					object[] array;
					bool[] array2;
					int num17;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 5832:
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
								goto IL_001d;
							case 6:
								goto IL_0026;
							case 7:
								goto IL_002f;
							case 8:
								goto IL_0038;
							case 9:
								goto IL_0041;
							case 10:
								goto IL_004b;
							case 11:
								goto IL_0055;
							case 12:
								goto IL_005b;
							case 13:
								goto IL_0061;
							case 14:
								goto IL_0067;
							case 15:
								goto IL_006d;
							case 16:
								goto IL_0073;
							case 17:
								goto IL_0079;
							case 18:
								goto IL_0094;
							case 19:
								goto IL_00af;
							case 20:
								goto IL_00ce;
							case 21:
								goto IL_00ed;
							case 22:
								goto IL_00f9;
							case 23:
								goto IL_010c;
							case 24:
								goto IL_0127;
							case 26:
								goto IL_0140;
							case 25:
							case 27:
							case 28:
								goto IL_014b;
							case 29:
								goto IL_0157;
							case 30:
								goto IL_016a;
							case 31:
								goto IL_0185;
							case 33:
								goto IL_019e;
							case 32:
							case 34:
							case 35:
								goto IL_01a9;
							case 36:
								goto IL_01dd;
							case 37:
								goto IL_01e3;
							case 38:
								goto IL_022b;
							case 39:
								goto IL_0231;
							case 40:
								goto IL_027e;
							case 41:
								goto IL_0284;
							case 42:
								goto IL_0353;
							case 43:
								goto IL_0359;
							case 44:
								goto IL_0393;
							case 45:
								goto IL_03a7;
							case 46:
								goto IL_03bb;
							case 48:
								goto IL_040d;
							case 51:
								goto IL_041e;
							case 52:
								goto IL_0435;
							case 53:
								goto IL_04a7;
							case 55:
								goto IL_04b4;
							case 56:
								goto IL_04d8;
							case 58:
								goto IL_04e5;
							case 59:
								goto IL_04f3;
							case 62:
								goto IL_051e;
							case 63:
								goto IL_0535;
							case 64:
								goto IL_0543;
							case 66:
								goto IL_0550;
							case 67:
								goto IL_055e;
							case 65:
							case 68:
							case 69:
								goto IL_0569;
							case 70:
								goto IL_0599;
							case 73:
								goto IL_05b6;
							case 74:
								goto IL_05ca;
							case 75:
								goto IL_060b;
							case 76:
								goto IL_0619;
							case 78:
								goto IL_0626;
							case 79:
								goto IL_0634;
							case 47:
							case 49:
							case 50:
							case 54:
							case 57:
							case 60:
							case 61:
							case 71:
							case 72:
							case 77:
							case 80:
							case 81:
							case 82:
							case 83:
								goto IL_0642;
							case 84:
								goto IL_068e;
							case 85:
								goto IL_06a7;
							case 86:
								goto IL_06c1;
							case 87:
								goto IL_07e2;
							case 89:
							case 90:
								goto IL_07ec;
							case 88:
							case 91:
								goto IL_07fe;
							case 93:
								goto IL_080b;
							case 92:
							case 94:
								goto IL_081a;
							case 95:
								goto IL_082b;
							case 96:
								goto IL_0844;
							case 97:
								goto IL_085e;
							case 98:
								goto IL_097f;
							case 100:
							case 101:
								goto IL_0989;
							case 99:
							case 102:
								goto IL_099b;
							case 104:
								goto IL_09a8;
							case 103:
							case 105:
							case 106:
								goto IL_09b9;
							case 107:
								goto IL_09c4;
							case 109:
								goto IL_09d1;
							case 110:
								goto IL_09f2;
							case 108:
							case 111:
							case 112:
								goto IL_09fd;
							case 113:
								goto IL_0a16;
							case 115:
							case 116:
								goto IL_0a2f;
							case 117:
								goto IL_0a43;
							case 119:
								goto IL_0a55;
							case 118:
							case 120:
							case 121:
								goto IL_0a64;
							case 122:
								goto IL_0a88;
							case 123:
								goto IL_0aac;
							case 124:
								goto IL_0acc;
							case 125:
								goto IL_0ad6;
							case 126:
								goto IL_0aea;
							case 127:
								goto IL_0af4;
							case 128:
								goto IL_0b0f;
							case 129:
								goto IL_0b31;
							case 130:
								goto IL_0b4f;
							case 131:
								goto IL_0b5c;
							case 132:
								goto IL_0b73;
							case 133:
								goto IL_0b80;
							case 134:
								goto IL_0b9e;
							case 135:
								goto IL_0bab;
							case 136:
								goto IL_0be5;
							case 137:
							case 138:
								goto IL_0c06;
							case 139:
								goto IL_0c24;
							case 140:
								goto IL_0c31;
							case 141:
								goto IL_0c6b;
							case 142:
							case 143:
								goto IL_0c8c;
							case 144:
								goto IL_0caa;
							case 145:
							case 146:
								goto IL_0cc8;
							case 147:
								goto IL_0ce4;
							case 148:
								goto IL_0cf1;
							case 149:
								goto IL_0d0e;
							case 150:
								goto IL_0ddd;
							case 151:
								goto IL_0e16;
							case 154:
								goto IL_0e8f;
							case 155:
								goto IL_0ecb;
							case 156:
								goto IL_0f08;
							case 157:
								goto IL_0f24;
							case 158:
								goto IL_0f38;
							case 152:
							case 153:
							case 159:
							case 160:
								goto IL_0f80;
							case 161:
								goto IL_0f95;
							case 162:
								goto IL_0fb1;
							case 163:
								goto IL_0fc1;
							case 164:
								goto IL_0fd1;
							case 166:
								goto IL_0fee;
							case 165:
							case 167:
							case 168:
							case 169:
								goto IL_100f;
							case 170:
								goto IL_1021;
							case 171:
								goto IL_103d;
							case 172:
								goto IL_1059;
							case 173:
								goto IL_1066;
							case 174:
								goto IL_1083;
							case 175:
								goto IL_1152;
							case 176:
								goto IL_118b;
							case 179:
								goto IL_1204;
							case 180:
								goto IL_1240;
							case 181:
								goto IL_127d;
							case 182:
								goto IL_1299;
							case 183:
								goto IL_12ad;
							case 177:
							case 178:
							case 184:
							case 185:
								goto IL_12f5;
							case 186:
								goto IL_130a;
							case 187:
								goto IL_1326;
							case 188:
								goto IL_1336;
							case 189:
								goto IL_1346;
							case 191:
								goto IL_1363;
							case 190:
							case 192:
							case 193:
							case 194:
								goto IL_1384;
							case 195:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 114:
							case 196:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0c06:
						num2 = 138;
						f_JSLScript = f_JSLScript + text + "@@@";
						goto IL_0c24;
						IL_0c24:
						num2 = 139;
						text = "EM-S->";
						goto IL_0c31;
						IL_0be5:
						num2 = 136;
						text += Strings.Trim(TxtAttach.Text);
						goto IL_0c06;
						IL_0c31:
						num2 = 140;
						if (optemail.Checked && Operators.CompareString(Strings.Trim(cmbrole.Text), "", TextCompare: false) != 0)
						{
							goto IL_0c6b;
						}
						goto IL_0c8c;
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
						MyCS = "";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						MyLabel = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text2 = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						text3 = "";
						goto IL_0038;
						IL_0038:
						num2 = 8;
						text = "E->N";
						goto IL_0041;
						IL_0041:
						num2 = 9;
						text4 = "";
						goto IL_004b;
						IL_004b:
						num2 = 10;
						text5 = "";
						goto IL_0055;
						IL_0055:
						num2 = 11;
						num7 = 0;
						goto IL_005b;
						IL_005b:
						num2 = 12;
						flag = true;
						goto IL_0061;
						IL_0061:
						num2 = 13;
						flag2 = false;
						goto IL_0067;
						IL_0067:
						num2 = 14;
						flag3 = false;
						goto IL_006d;
						IL_006d:
						num2 = 15;
						flag4 = false;
						goto IL_0073;
						IL_0073:
						num2 = 16;
						flag5 = false;
						goto IL_0079;
						IL_0079:
						num2 = 17;
						text6 = BuildForm.Replace_Globals(Strings.Trim(TxtHTMOut.Text));
						goto IL_0094;
						IL_0094:
						num2 = 18;
						value = BuildForm.Replace_Globals(Strings.Trim(TxtCSSOut.Text));
						goto IL_00af;
						IL_00af:
						num2 = 19;
						TxtCSSOut.Text = Strings.Trim(TxtCSSOut.Text);
						goto IL_00ce;
						IL_00ce:
						num2 = 20;
						TxtHTMOut.Text = Strings.Trim(TxtHTMOut.Text);
						goto IL_00ed;
						IL_00ed:
						num2 = 21;
						text4 = Strings.LCase(text6);
						goto IL_00f9;
						IL_00f9:
						num2 = 22;
						num7 = Strings.InStrRev(text4, ".");
						goto IL_010c;
						IL_010c:
						num2 = 23;
						if ((double)num7 != Conversions.ToDouble(""))
						{
							goto IL_0127;
						}
						goto IL_0140;
						IL_0127:
						num2 = 24;
						text4 = Strings.Trim(Strings.Mid(text4, num7 + 1));
						goto IL_014b;
						IL_0140:
						num2 = 26;
						text4 = "";
						goto IL_014b;
						IL_014b:
						num2 = 28;
						text5 = Strings.LCase(value);
						goto IL_0157;
						IL_0157:
						num2 = 29;
						num7 = Strings.InStrRev(text5, ".");
						goto IL_016a;
						IL_016a:
						num2 = 30;
						if ((double)num7 != Conversions.ToDouble(""))
						{
							goto IL_0185;
						}
						goto IL_019e;
						IL_0185:
						num2 = 31;
						text5 = Strings.Trim(Strings.Mid(text5, num7 + 1));
						goto IL_01a9;
						IL_019e:
						num2 = 33;
						text5 = "";
						goto IL_01a9;
						IL_01a9:
						num2 = 35;
						if (Operators.CompareString(TxtCSSOut.Text, "", TextCompare: false) != 0 && Operators.CompareString(text5, "css", TextCompare: false) != 0)
						{
							goto IL_01dd;
						}
						goto IL_01e3;
						IL_0f38:
						num2 = 158;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + ";", GridChart.Rows[num5].Cells[num6].Tag), MyCS));
						goto IL_0f80;
						IL_01dd:
						num2 = 36;
						flag = false;
						goto IL_01e3;
						IL_01e3:
						num2 = 37;
						if (Operators.CompareString(TxtHTMOut.Text, "", TextCompare: false) != 0 && (LikeOperator.LikeString(text6.ToLower(), "*http://*", CompareMethod.Binary) || LikeOperator.LikeString(text6, "*https://*", CompareMethod.Binary)))
						{
							goto IL_022b;
						}
						goto IL_0231;
						IL_0c6b:
						num2 = 141;
						text += Strings.Trim(cmbrole.Text);
						goto IL_0c8c;
						IL_0c8c:
						num2 = 143;
						f_JSLScript = f_JSLScript + text + "@@@";
						goto IL_0caa;
						IL_022b:
						num2 = 38;
						flag2 = true;
						goto IL_0231;
						IL_0231:
						num2 = 39;
						if (Operators.CompareString(TxtHTMOut.Text, "", TextCompare: false) != 0 && (LikeOperator.LikeString(text6.ToLower(), "email:*", CompareMethod.Binary) || LikeOperator.LikeString(text6.ToLower(), "email-a:*", CompareMethod.Binary)))
						{
							goto IL_027e;
						}
						goto IL_0284;
						IL_0caa:
						num2 = 144;
						f_JSLScript += "SEC->Y@@@";
						goto IL_0cc8;
						IL_0cc8:
						num2 = 146;
						num8 = GridChart.RowCount - 1;
						num5 = 0;
						goto IL_1019;
						IL_027e:
						num2 = 40;
						flag3 = true;
						goto IL_0284;
						IL_0284:
						num2 = 41;
						if ((Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0 && (Operators.CompareString(text4, "htm", TextCompare: false) == 0 || Operators.CompareString(text4, "html", TextCompare: false) == 0)) || (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0 && ((Operators.CompareString(text4, "jrn", TextCompare: false) == 0) | (Operators.CompareString(text4, "htm", TextCompare: false) == 0) | (Operators.CompareString(text4, "html", TextCompare: false) == 0) | (Operators.CompareString(text4, "jmp", TextCompare: false) == 0) | (Operators.CompareString(text4, "txt", TextCompare: false) == 0) | (Operators.CompareString(text4, "png", TextCompare: false) == 0) | (Operators.CompareString(text4, "rtf", TextCompare: false) == 0))))
						{
							goto IL_0353;
						}
						goto IL_0359;
						IL_1021:
						num2 = 170;
						f_JSLScript += "@@@";
						goto IL_103d;
						IL_103d:
						num2 = 171;
						num9 = GridFilter.RowCount - 1;
						num5 = 0;
						goto IL_138e;
						IL_1019:
						if (num5 <= num8)
						{
							goto IL_0ce4;
						}
						goto IL_1021;
						IL_138e:
						if (num5 > num9)
						{
							break;
						}
						goto IL_1059;
						IL_0353:
						num2 = 42;
						flag4 = true;
						goto IL_0359;
						IL_0359:
						num2 = 43;
						if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0 && (optfile.Checked || optSP.Checked))
						{
							goto IL_0393;
						}
						goto IL_03a7;
						IL_1059:
						num2 = 172;
						text3 = "";
						goto IL_1066;
						IL_1066:
						num2 = 173;
						num10 = GridFilter.ColumnCount - 1;
						num6 = 0;
						goto IL_1301;
						IL_0393:
						num2 = 44;
						TxtCSSOut.Text = "";
						goto IL_03a7;
						IL_03a7:
						num2 = 45;
						if (optPP.Checked)
						{
							goto IL_03bb;
						}
						goto IL_041e;
						IL_03bb:
						num2 = 46;
						if (!LikeOperator.LikeString(text4, "ppt*", CompareMethod.Binary) || (!LikeOperator.LikeString(text5, "ppt*", CompareMethod.Binary) && Operators.CompareString(TxtCSSOut.Text, "", TextCompare: false) != 0) || flag3 || flag2)
						{
							goto IL_040d;
						}
						goto IL_0642;
						IL_1301:
						if (num6 <= num10)
						{
							goto IL_1083;
						}
						goto IL_130a;
						IL_1326:
						num2 = 187;
						text3 = Strings.Mid(text3, 2);
						goto IL_1336;
						IL_130a:
						num2 = 186;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_1326;
						}
						goto IL_1384;
						IL_1336:
						num2 = 188;
						if (num5 == 0)
						{
							goto IL_1346;
						}
						goto IL_1363;
						IL_040d:
						num2 = 48;
						text2 = "PowerPoint files and Templates must have an extension of .ppt, or the Template file should be empty (to create a new PowerPoint file)";
						goto IL_0642;
						IL_041e:
						num2 = 51;
						if (optemail.Checked)
						{
							goto IL_0435;
						}
						goto IL_051e;
						IL_0435:
						num2 = 52;
						if (Strings.InStr(text6.ToLower(), "@intel.com") == 0 && Strings.InStr(text6.ToLower(), "self") == 0 && (LikeOperator.LikeString(text4, "htm*", CompareMethod.Binary) || LikeOperator.LikeString(text4, "ppt*", CompareMethod.Binary) || flag2 || Operators.CompareString(TxtHTMOut.Text, "", TextCompare: false) == 0))
						{
							goto IL_04a7;
						}
						goto IL_04b4;
						IL_1346:
						num2 = 189;
						f_JSLScript += text3;
						goto IL_1384;
						IL_1363:
						num2 = 191;
						f_JSLScript = f_JSLScript + "|" + text3;
						goto IL_1384;
						IL_1384:
						num2 = 194;
						num5++;
						goto IL_138e;
						IL_1083:
						num2 = 174;
						if (Conversions.ToBoolean(Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0 && (Conversions.ToBoolean(LikeOperator.LikeObject(GridFilter.Rows[num5].Cells[num6].Value, "Text:*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(GridFilter.Rows[num5].Cells[num6].Value, "Images:*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(GridFilter.Rows[num5].Cells[num6].Value, "JMP-HTML:*", CompareMethod.Binary)))))
						{
							goto IL_1152;
						}
						goto IL_1204;
						IL_0f24:
						num2 = 157;
						MyCS = ":" + MyCS;
						goto IL_0f38;
						IL_04a7:
						num2 = 53;
						text2 = "An Email distribution should either be a comma delimited list of email addresses or a file with addresses. The file should not have a ppt, pptx, htm, or html extension.";
						goto IL_0642;
						IL_04b4:
						num2 = 55;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0 && !flag)
						{
							goto IL_04d8;
						}
						goto IL_04e5;
						IL_0ddd:
						num2 = 150;
						if (Operators.ConditionalCompareObjectNotEqual(GridChart.Rows[num5].Cells[num6].Tag, "", TextCompare: false))
						{
							goto IL_0e16;
						}
						goto IL_0f80;
						IL_04d8:
						num2 = 56;
						text2 = "An Email distribution should either reference a style sheet with an extension of .css or reference no style sheet";
						goto IL_0642;
						IL_04e5:
						num2 = 58;
						if (!flag3)
						{
							goto IL_04f3;
						}
						goto IL_0642;
						IL_04f3:
						num2 = 59;
						TxtHTMOut.Text = "EMAIL:" + TxtHTMOut.Text;
						goto IL_0642;
						IL_051e:
						num2 = 62;
						if (optSP.Checked)
						{
							goto IL_0535;
						}
						goto IL_05b6;
						IL_0535:
						num2 = 63;
						if (!flag2)
						{
							goto IL_0543;
						}
						goto IL_0550;
						IL_0543:
						num2 = 64;
						text2 = "You must specify a SharePoint URL to publish to SharePoint";
						goto IL_0569;
						IL_0550:
						num2 = 66;
						if (!flag)
						{
							goto IL_055e;
						}
						goto IL_0569;
						IL_055e:
						num2 = 67;
						text2 = "A SharePoint distribution should either reference a style sheet with an extension of .css or reference no style sheet";
						goto IL_0569;
						IL_0569:
						num2 = 69;
						if (LikeOperator.LikeString(Strings.LCase(text6), "*https://intel.sharepoint.com/*", CompareMethod.Binary) && Operators.CompareString(text4, "aspx", TextCompare: false) != 0)
						{
							goto IL_0599;
						}
						goto IL_0642;
						IL_0e16:
						num2 = 151;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + ";", GridChart.Rows[num5].Cells[num6].Value), ":"), GridChart.Rows[num5].Cells[num6].Tag));
						goto IL_0f80;
						IL_0599:
						num2 = 70;
						Interaction.MsgBox("Note that HTML reports must have an extension of \".aspx\" to run Javascript code on SharePoint online. Static HTML reports will display properly with an \".htm\" extension", MsgBoxStyle.Information, "SharePoint Online");
						goto IL_0642;
						IL_05b6:
						num2 = 73;
						if (optfile.Checked)
						{
							goto IL_05ca;
						}
						goto IL_0642;
						IL_05ca:
						num2 = 74;
						if (Strings.InStr(text6.ToLower(), "@intel.com") == 0 && !flag2 && Operators.CompareString(TxtHTMOut.Text, "", TextCompare: false) != 0 && !flag3)
						{
							goto IL_060b;
						}
						goto IL_0642;
						IL_0e8f:
						num2 = 154;
						if (Operators.ConditionalCompareObjectNotEqual(GridChart.Rows[num5].Cells[num6].Value, "", TextCompare: false))
						{
							goto IL_0ecb;
						}
						goto IL_0f80;
						IL_0f08:
						num2 = 156;
						if (Operators.CompareString(MyCS, "", TextCompare: false) != 0)
						{
							goto IL_0f24;
						}
						goto IL_0f38;
						IL_0f80:
						num2 = 160;
						num6++;
						goto IL_0f8c;
						IL_060b:
						num2 = 75;
						if (!flag4)
						{
							goto IL_0619;
						}
						goto IL_0626;
						IL_0619:
						num2 = 76;
						text2 = "Only certain file extensions are allowed for File distribtion";
						goto IL_0642;
						IL_0626:
						num2 = 78;
						if (!flag)
						{
							goto IL_0634;
						}
						goto IL_0642;
						IL_0634:
						num2 = 79;
						text2 = "A File distribution should either reference a style sheet with an extension of .css or reference no style sheet";
						goto IL_0642;
						IL_0642:
						num2 = 83;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0 && Operators.CompareString(text2, "", TextCompare: false) == 0 && (optPP.Checked || optemail.Checked))
						{
							goto IL_068e;
						}
						goto IL_09d1;
						IL_1152:
						num2 = 175;
						if (Operators.ConditionalCompareObjectNotEqual(GridFilter.Rows[num5].Cells[num6].Tag, "", TextCompare: false))
						{
							goto IL_118b;
						}
						goto IL_12f5;
						IL_118b:
						num2 = 176;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + ";", GridFilter.Rows[num5].Cells[num6].Value), ":"), GridFilter.Rows[num5].Cells[num6].Tag));
						goto IL_12f5;
						IL_1204:
						num2 = 179;
						if (Operators.ConditionalCompareObjectNotEqual(GridFilter.Rows[num5].Cells[num6].Value, "", TextCompare: false))
						{
							goto IL_1240;
						}
						goto IL_12f5;
						IL_068e:
						num2 = 84;
						num11 = GridChart.RowCount - 1;
						num5 = 0;
						goto IL_0812;
						IL_0812:
						if (num5 <= num11)
						{
							goto IL_06a7;
						}
						goto IL_081a;
						IL_06a7:
						num2 = 85;
						num12 = GridChart.ColumnCount - 1;
						num6 = 0;
						goto IL_07f5;
						IL_07f5:
						if (num6 <= num12)
						{
							goto IL_06c1;
						}
						goto IL_07fe;
						IL_06c1:
						num2 = 86;
						typeFromHandle = typeof(Strings);
						obj = new object[1] { (dataGridViewCell = GridChart.Rows[num5].Cells[num6]).Value };
						array = obj;
						obj2 = new bool[1] { true };
						array2 = obj2;
						source = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
						if (array2[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (!Conversions.ToBoolean(LikeOperator.LikeObject(source, "I:*", CompareMethod.Binary)))
						{
							Type typeFromHandle2 = typeof(Strings);
							object[] obj3 = new object[1] { (dataGridViewCell = GridChart.Rows[num5].Cells[num6]).Value };
							array = obj3;
							bool[] obj4 = new bool[1] { true };
							array2 = obj4;
							object source2 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj3, null, null, obj4);
							if (array2[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							num13 = (Conversions.ToBoolean(LikeOperator.LikeObject(source2, "CI:*", CompareMethod.Binary)) ? 1 : 0);
						}
						else
						{
							num13 = 1;
						}
						if (Conversions.ToBoolean(unchecked((byte)num13) != 0))
						{
							goto IL_07e2;
						}
						goto IL_07ec;
						IL_127d:
						num2 = 181;
						if (Operators.CompareString(MyCS, "", TextCompare: false) != 0)
						{
							goto IL_1299;
						}
						goto IL_12ad;
						IL_1240:
						num2 = 180;
						BuildChart.Get_CS_and_Label(f_ScriptType, Conversions.ToString(GridFilter.Rows[num5].Cells[num6].Value), ref MyCS, ref MyLabel);
						goto IL_127d;
						IL_12ad:
						num2 = 183;
						text3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text3 + ";", GridFilter.Rows[num5].Cells[num6].Tag), MyCS));
						goto IL_12f5;
						IL_0cf1:
						num2 = 148;
						num14 = GridChart.ColumnCount - 1;
						num6 = 0;
						goto IL_0f8c;
						IL_0ce4:
						num2 = 147;
						text3 = "";
						goto IL_0cf1;
						IL_12f5:
						num2 = 185;
						num6++;
						goto IL_1301;
						IL_1299:
						num2 = 182;
						MyCS = ":" + MyCS;
						goto IL_12ad;
						IL_07e2:
						num2 = 87;
						flag5 = true;
						goto IL_07fe;
						IL_07fe:
						num2 = 91;
						if (!flag5)
						{
							goto IL_080b;
						}
						goto IL_081a;
						IL_081a:
						num2 = 94;
						if (!flag5)
						{
							goto IL_082b;
						}
						goto IL_09b9;
						IL_082b:
						num2 = 95;
						num15 = GridFilter.RowCount - 1;
						num5 = 0;
						goto IL_09af;
						IL_09af:
						if (num5 <= num15)
						{
							goto IL_0844;
						}
						goto IL_09b9;
						IL_0844:
						num2 = 96;
						num16 = GridFilter.ColumnCount - 1;
						num6 = 0;
						goto IL_0992;
						IL_0992:
						if (num6 <= num16)
						{
							goto IL_085e;
						}
						goto IL_099b;
						IL_085e:
						num2 = 97;
						typeFromHandle3 = typeof(Strings);
						obj5 = new object[1] { (dataGridViewCell = GridFilter.Rows[num5].Cells[num6]).Value };
						array = obj5;
						obj6 = new bool[1] { true };
						array2 = obj6;
						source3 = NewLateBinding.LateGet(null, typeFromHandle3, "UCase", obj5, null, null, obj6);
						if (array2[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (!Conversions.ToBoolean(LikeOperator.LikeObject(source3, "I:*", CompareMethod.Binary)))
						{
							Type typeFromHandle4 = typeof(Strings);
							object[] obj7 = new object[1] { (dataGridViewCell = GridFilter.Rows[num5].Cells[num6]).Value };
							array = obj7;
							bool[] obj8 = new bool[1] { true };
							array2 = obj8;
							object source4 = NewLateBinding.LateGet(null, typeFromHandle4, "UCase", obj7, null, null, obj8);
							if (array2[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							num17 = (Conversions.ToBoolean(LikeOperator.LikeObject(source4, "CI:*", CompareMethod.Binary)) ? 1 : 0);
						}
						else
						{
							num17 = 1;
						}
						if (Conversions.ToBoolean(unchecked((byte)num17) != 0))
						{
							goto IL_097f;
						}
						goto IL_0989;
						IL_0f95:
						num2 = 161;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_0fb1;
						}
						goto IL_100f;
						IL_0f8c:
						if (num6 <= num14)
						{
							goto IL_0d0e;
						}
						goto IL_0f95;
						IL_0fc1:
						num2 = 163;
						if (num5 == 0)
						{
							goto IL_0fd1;
						}
						goto IL_0fee;
						IL_100f:
						num2 = 169;
						num5++;
						goto IL_1019;
						IL_0fee:
						num2 = 166;
						f_JSLScript = f_JSLScript + "|" + text3;
						goto IL_100f;
						IL_0fd1:
						num2 = 164;
						f_JSLScript += text3;
						goto IL_100f;
						IL_0fb1:
						num2 = 162;
						text3 = Strings.Mid(text3, 2);
						goto IL_0fc1;
						IL_097f:
						num2 = 98;
						flag5 = true;
						goto IL_099b;
						IL_099b:
						num2 = 102;
						if (!flag5)
						{
							goto IL_09a8;
						}
						goto IL_09b9;
						IL_09a8:
						num2 = 104;
						num5++;
						goto IL_09af;
						IL_0989:
						num2 = 101;
						num6++;
						goto IL_0992;
						IL_09b9:
						num2 = 106;
						if (flag5)
						{
							goto IL_09c4;
						}
						goto IL_09fd;
						IL_09c4:
						num2 = 107;
						text2 = "You cannot send interactive reports or charts to Email nor PowerPoint";
						goto IL_09fd;
						IL_080b:
						num2 = 93;
						num5++;
						goto IL_0812;
						IL_07ec:
						num2 = 90;
						num6++;
						goto IL_07f5;
						IL_09d1:
						num2 = 109;
						if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0 && flag2)
						{
							goto IL_09f2;
						}
						goto IL_09fd;
						IL_0d0e:
						num2 = 149;
						if (Conversions.ToBoolean(Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0 && (Conversions.ToBoolean(LikeOperator.LikeObject(GridChart.Rows[num5].Cells[num6].Value, "Text:*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(GridChart.Rows[num5].Cells[num6].Value, "Images:*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(GridChart.Rows[num5].Cells[num6].Value, "JMP-HTML:*", CompareMethod.Binary)))))
						{
							goto IL_0ddd;
						}
						goto IL_0e8f;
						IL_09f2:
						num2 = 110;
						text2 = "The Chart-in-JMP utility does not yet support SharePoint publishes";
						goto IL_09fd;
						IL_09fd:
						num2 = 112;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_0a16;
						}
						goto IL_0a2f;
						IL_0a16:
						num2 = 113;
						Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, "Invalid Data Entry");
						goto end_IL_0001_3;
						IL_0a2f:
						num2 = 116;
						if (ChkReverse.Checked)
						{
							goto IL_0a43;
						}
						goto IL_0a55;
						IL_0a43:
						num2 = 117;
						f_JSLScript = "1@@@";
						goto IL_0a64;
						IL_0a55:
						num2 = 119;
						f_JSLScript = "0@@@";
						goto IL_0a64;
						IL_0a64:
						num2 = 121;
						f_JSLScript = f_JSLScript + TxtHTMOut.Text + "@@@";
						goto IL_0a88;
						IL_0a88:
						num2 = 122;
						f_JSLScript = f_JSLScript + TxtCSSOut.Text + "@@@";
						goto IL_0aac;
						IL_0aac:
						num2 = 123;
						if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
						{
							goto IL_0acc;
						}
						goto IL_0cc8;
						IL_0acc:
						num2 = 124;
						text = "E->N";
						goto IL_0ad6;
						IL_0ad6:
						num2 = 125;
						if (ChkEmbedCSS.Checked)
						{
							goto IL_0aea;
						}
						goto IL_0af4;
						IL_0aea:
						num2 = 126;
						text = "E->Y";
						goto IL_0af4;
						IL_0af4:
						num2 = 127;
						f_JSLScript = f_JSLScript + text + "@@@";
						goto IL_0b0f;
						IL_0b0f:
						num2 = 128;
						text = "RR->" + General_Procedures.Get_Node_Value(mnuRR.Text);
						goto IL_0b31;
						IL_0b31:
						num2 = 129;
						f_JSLScript = f_JSLScript + text + "@@@";
						goto IL_0b4f;
						IL_0b4f:
						num2 = 130;
						text = "B->N";
						goto IL_0b5c;
						IL_0b5c:
						num2 = 131;
						if (mnudisplaywin.Checked)
						{
							goto IL_0b73;
						}
						goto IL_0b80;
						IL_0b73:
						num2 = 132;
						text = "B->Y";
						goto IL_0b80;
						IL_0b80:
						num2 = 133;
						f_JSLScript = f_JSLScript + text + "@@@";
						goto IL_0b9e;
						IL_0b9e:
						num2 = 134;
						text = "EM-A->";
						goto IL_0bab;
						IL_0bab:
						num2 = 135;
						if (optemail.Checked && Operators.CompareString(Strings.Trim(TxtAttach.Text), "", TextCompare: false) != 0)
						{
							goto IL_0be5;
						}
						goto IL_0c06;
						IL_0ecb:
						num2 = 155;
						BuildChart.Get_CS_and_Label(f_ScriptType, Conversions.ToString(GridChart.Rows[num5].Cells[num6].Value), ref MyCS, ref MyLabel);
						goto IL_0f08;
						end_IL_0001_2:
						break;
					}
					num2 = 195;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj9) when (obj9 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj9);
				try0001_dispatch = 5832;
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

	private void CmdBrowse2_Click(object sender, EventArgs e)
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
				case 367:
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
							goto IL_003a;
						case 5:
							goto IL_004d;
						case 7:
							goto IL_006e;
						case 10:
							goto IL_008f;
						case 11:
							goto IL_00ac;
						case 6:
						case 8:
						case 9:
						case 12:
						case 13:
							goto IL_00cb;
						case 14:
							goto IL_00e3;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ac:
					num2 = 11;
					BuildForm.FileOpenSave("O", text, "ppt", "Choose a PowerPoint Template File", "");
					goto IL_00cb;
					IL_00cb:
					num2 = 13;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00e3;
					IL_008f:
					num2 = 10;
					if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_00ac;
					}
					goto IL_00cb;
					IL_00e3:
					num2 = 14;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text = Strings.Trim(TxtCSSOut.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (Operators.CompareString(f_ScriptType, "HTML", TextCompare: false) == 0)
					{
						goto IL_003a;
					}
					goto IL_008f;
					IL_003a:
					num2 = 4;
					if (optPP.Checked)
					{
						goto IL_004d;
					}
					goto IL_006e;
					IL_004d:
					num2 = 5;
					BuildForm.FileOpenSave("O", text, "ppt", "Choose a PowerPoint Template File", "");
					goto IL_00cb;
					IL_006e:
					num2 = 7;
					BuildForm.FileOpenSave("O", text, "css", "Choose a Style Sheet for this report", "");
					goto IL_00cb;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				TxtCSSOut.Text = BuildForm.Strip_Add_MyPCDir("S", text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 367;
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

	private void CmdBrowse1_Click(object sender, EventArgs e)
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
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 550:
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
						case 6:
							goto IL_0058;
						case 8:
							goto IL_0078;
						case 11:
							goto IL_009e;
						case 12:
							goto IL_00b2;
						case 13:
							goto IL_00d0;
						case 14:
							goto IL_00e8;
						case 15:
							goto IL_0100;
						case 18:
							goto IL_0127;
						case 20:
							goto IL_013f;
						case 5:
						case 7:
						case 9:
						case 10:
						case 17:
						case 19:
						case 21:
						case 22:
							goto IL_015e;
						case 23:
							goto IL_0176;
						case 24:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0127:
					num2 = 18;
					if (!optSP.Checked)
					{
						goto IL_013f;
					}
					goto IL_015e;
					IL_013f:
					num2 = 20;
					BuildForm.FileOpenSave("S", fileName, "ppt", "PowerPoint File", "");
					goto IL_015e;
					IL_0100:
					num2 = 15;
					TxtHTMOut.Text = BuildForm.Strip_Add_MyPCDir("S", fileName);
					goto end_IL_0001_3;
					IL_015e:
					num2 = 22;
					fileName = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_0176;
					IL_000b:
					num2 = 2;
					fileName = TxtHTMOut.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					if (optfile.Checked)
					{
						goto IL_002d;
					}
					goto IL_009e;
					IL_002d:
					num2 = 4;
					left = f_ScriptType;
					if (Operators.CompareString(left, "HTML", TextCompare: false) == 0)
					{
						goto IL_0058;
					}
					if (Operators.CompareString(left, "JMP", TextCompare: false) == 0)
					{
						goto IL_0078;
					}
					goto IL_015e;
					IL_0176:
					num2 = 23;
					if (Operators.CompareString(fileName, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0078:
					num2 = 8;
					BuildForm.FileOpenSave("S", fileName, "jrn", "HTML/Journal/Doc/RTF/Image/Text Report File", "");
					goto IL_015e;
					IL_0058:
					num2 = 6;
					BuildForm.FileOpenSave("S", fileName, "htm", "HTML Report File", "");
					goto IL_015e;
					IL_009e:
					num2 = 11;
					if (optemail.Checked)
					{
						goto IL_00b2;
					}
					goto IL_0127;
					IL_00b2:
					num2 = 12;
					BuildForm.FileOpenSave("O", fileName, "csv+", "Email Address File", "");
					goto IL_00d0;
					IL_00d0:
					num2 = 13;
					fileName = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00e8;
					IL_00e8:
					num2 = 14;
					if (Operators.CompareString(fileName, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0100;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				TxtHTMOut.Text = BuildForm.Strip_Add_MyPCDir("S", fileName);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 550;
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

	private void FrmJMPWindow_Resize(object sender, EventArgs e)
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

	private void mnuRR_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
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
				case 114:
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
					oVal = General_Procedures.Get_Node_Value(mnuRR.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					BuildForm.SetOutputOpt("R", ref oVal);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				mnuRR.Text = General_Procedures.Set_Node_Value(mnuRR.Text, oVal);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 114;
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

	private void optfile_Click(object sender, EventArgs e)
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
				Set_Opt();
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

	private void CmdBrowse3_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int MyCount = default(int);
		string FileNames = default(string);
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
				case 130:
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
							goto IL_002e;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0013:
					num2 = 3;
					MyCount = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					BuildForm.File_Multi(Conversions.ToShort("0"), ref FileNames, ref MyCount);
					goto IL_002e;
					IL_000b:
					num2 = 2;
					FileNames = "";
					goto IL_0013;
					IL_002e:
					num2 = 5;
					if (Operators.CompareString(FileNames, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				TxtAttach.Text = FileNames;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 130;
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

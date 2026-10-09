using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmUtil : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdHelp")]
	private Button _cmdHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdViewXL")]
	private Button _CmdViewXL;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptXL_Import")]
	private RadioButton _OptXL_Import;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptXL_Load")]
	private RadioButton _OptXL_Load;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCancel")]
	private Button _cmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK")]
	private Button _cmdOK;

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
	[AccessedThroughProperty("CmdBrowse0")]
	private Button _CmdBrowse0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse3")]
	private Button _CmdBrowse3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdHelp2")]
	private Button _CmdHelp2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbOpt")]
	private ComboBox _CmbOpt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOpt2")]
	private ToolStripComboBox _mnuOpt2;

	public string fUtilPath;

	public string fVaFileName;

	public string fHelpType;

	private int f_HeightAdd;

	private string f_TmpL;

	private string f_TmpNL;

	private string f_TmpOld;

	private bool isInit;

	[field: AccessedThroughProperty("TxtOutXL")]
	public virtual ComboBox TxtOutXL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdHelp
	{
		[CompilerGenerated]
		get
		{
			return _cmdHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHelp_Click;
			Button button = _cmdHelp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdHelp = value;
			button = _cmdHelp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdViewXL
	{
		[CompilerGenerated]
		get
		{
			return _CmdViewXL;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdViewXL_Click;
			Button button = _CmdViewXL;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdViewXL = value;
			button = _CmdViewXL;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual RadioButton OptXL_Import
	{
		[CompilerGenerated]
		get
		{
			return _OptXL_Import;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptXL_Import_CheckedChanged;
			RadioButton radioButton = _OptXL_Import;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptXL_Import = value;
			radioButton = _OptXL_Import;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	public virtual RadioButton OptXL_Load
	{
		[CompilerGenerated]
		get
		{
			return _OptXL_Load;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptXL_Load_CheckedChanged;
			RadioButton radioButton = _OptXL_Load;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptXL_Load = value;
			radioButton = _OptXL_Load;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("FrmOpt")]
	public virtual GroupBox FrmOpt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdCancel
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
			EventHandler value2 = CmdCancel_Click;
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

	public virtual Button cmdOK
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

	public virtual Button CmdBrowse2
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

	public virtual Button CmdBrowse1
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

	public virtual Button CmdBrowse0
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse0_Click;
			Button button = _CmdBrowse0;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse0 = value;
			button = _CmdBrowse0;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblUtil")]
	public virtual Label lblUtil
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblXLMac")]
	public virtual Label lblXLMac
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblXLWS")]
	public virtual Label lblXLWS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblXLIn")]
	public virtual Label lblXLIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblXLOut")]
	public virtual Label lblXLOut
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblcsv")]
	public virtual Label lblcsv
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtInXL")]
	internal virtual ComboBox TxtInXL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkOption")]
	internal virtual CheckBox chkOption
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXLSheet")]
	internal virtual ComboBox TxtXLSheet
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtCSV")]
	internal virtual ComboBox TxtCSV
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtOther1")]
	internal virtual ComboBox TxtOther1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblOther1")]
	internal virtual Label LblOther1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblOther2")]
	internal virtual Label LblOther2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtOther2")]
	internal virtual ComboBox TxtOther2
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

	internal virtual Button CmdHelp2
	{
		[CompilerGenerated]
		get
		{
			return _CmdHelp2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdHelp2_Click;
			Button button = _CmdHelp2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdHelp2 = value;
			button = _CmdHelp2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ComboBox CmbOpt
	{
		[CompilerGenerated]
		get
		{
			return _CmbOpt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = CmbOpt_KeyPress;
			EventHandler value3 = CmbOpt_SelectedIndexChanged;
			ComboBox comboBox = _CmbOpt;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
				comboBox.SelectedIndexChanged -= value3;
			}
			_CmbOpt = value;
			comboBox = _CmbOpt;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
				comboBox.SelectedIndexChanged += value3;
			}
		}
	}

	[field: AccessedThroughProperty("TxtXLMacro")]
	internal virtual ComboBox TxtXLMacro
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuoptstoolstrip")]
	internal virtual MenuStrip mnuoptstoolstrip
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuopts")]
	internal virtual ToolStripMenuItem mnuopts
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnulabel1")]
	internal virtual ToolStripMenuItem mnulabel1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuopt1")]
	internal virtual ToolStripComboBox mnuopt1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnulabel2")]
	internal virtual ToolStripMenuItem mnulabel2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox mnuOpt2
	{
		[CompilerGenerated]
		get
		{
			return _mnuOpt2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOpt2_TextChanged;
			EventHandler value3 = mnuOpt2_Click;
			ToolStripComboBox toolStripComboBox = _mnuOpt2;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.TextChanged -= value2;
				toolStripComboBox.Click -= value3;
			}
			_mnuOpt2 = value;
			toolStripComboBox = _mnuOpt2;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.TextChanged += value2;
				toolStripComboBox.Click += value3;
			}
		}
	}

	[field: AccessedThroughProperty("mnulabel3")]
	internal virtual ToolStripMenuItem mnulabel3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuopt3")]
	internal virtual ToolStripComboBox mnuopt3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnulabel4")]
	internal virtual ToolStripMenuItem mnulabel4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuopt4")]
	internal virtual ToolStripComboBox mnuopt4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("OrderedParallelMenuItem")]
	internal virtual ToolStripMenuItem OrderedParallelMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ParallelThreadsMenuItem")]
	internal virtual ToolStripMenuItem ParallelThreadsMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ParallelThreadsComboBox1")]
	internal virtual ToolStripComboBox ParallelThreadsComboBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public FrmUtil()
	{
		base.Load += FrmUtil_Load;
		base.FormClosed += FrmUtil_FormClosed;
		fUtilPath = "";
		fVaFileName = "";
		fHelpType = "P";
		f_HeightAdd = 40;
		f_TmpL = "20";
		f_TmpNL = "5";
		f_TmpOld = "";
		isInit = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmUtil));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.CmdViewXL = new System.Windows.Forms.Button();
		this.CmdHelp2 = new System.Windows.Forms.Button();
		this.TxtOutXL = new System.Windows.Forms.ComboBox();
		this.cmdHelp = new System.Windows.Forms.Button();
		this.FrmOpt = new System.Windows.Forms.GroupBox();
		this.OptXL_Import = new System.Windows.Forms.RadioButton();
		this.OptXL_Load = new System.Windows.Forms.RadioButton();
		this.chkOption = new System.Windows.Forms.CheckBox();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.CmdBrowse2 = new System.Windows.Forms.Button();
		this.CmdBrowse1 = new System.Windows.Forms.Button();
		this.CmdBrowse0 = new System.Windows.Forms.Button();
		this.lblUtil = new System.Windows.Forms.Label();
		this.lblXLMac = new System.Windows.Forms.Label();
		this.lblXLWS = new System.Windows.Forms.Label();
		this.lblXLIn = new System.Windows.Forms.Label();
		this.lblXLOut = new System.Windows.Forms.Label();
		this.lblcsv = new System.Windows.Forms.Label();
		this.TxtInXL = new System.Windows.Forms.ComboBox();
		this.TxtXLSheet = new System.Windows.Forms.ComboBox();
		this.TxtCSV = new System.Windows.Forms.ComboBox();
		this.TxtOther1 = new System.Windows.Forms.ComboBox();
		this.LblOther1 = new System.Windows.Forms.Label();
		this.LblOther2 = new System.Windows.Forms.Label();
		this.TxtOther2 = new System.Windows.Forms.ComboBox();
		this.CmdBrowse3 = new System.Windows.Forms.Button();
		this.CmbOpt = new System.Windows.Forms.ComboBox();
		this.TxtXLMacro = new System.Windows.Forms.ComboBox();
		this.mnuoptstoolstrip = new System.Windows.Forms.MenuStrip();
		this.mnuopts = new System.Windows.Forms.ToolStripMenuItem();
		this.mnulabel1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuopt1 = new System.Windows.Forms.ToolStripComboBox();
		this.mnulabel2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOpt2 = new System.Windows.Forms.ToolStripComboBox();
		this.mnulabel3 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuopt3 = new System.Windows.Forms.ToolStripComboBox();
		this.mnulabel4 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuopt4 = new System.Windows.Forms.ToolStripComboBox();
		this.OrderedParallelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ParallelThreadsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ParallelThreadsComboBox1 = new System.Windows.Forms.ToolStripComboBox();
		this.FrmOpt.SuspendLayout();
		this.mnuoptstoolstrip.SuspendLayout();
		base.SuspendLayout();
		this.CmdViewXL.BackColor = System.Drawing.SystemColors.Control;
		this.CmdViewXL.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdViewXL.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdViewXL.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdViewXL.Location = new System.Drawing.Point(526, 384);
		this.CmdViewXL.Name = "CmdViewXL";
		this.CmdViewXL.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdViewXL.Size = new System.Drawing.Size(54, 47);
		this.CmdViewXL.TabIndex = 11;
		this.CmdViewXL.Text = "View Excel Source";
		this.ToolTip1.SetToolTip(this.CmdViewXL, "View the Excel Source File");
		this.CmdViewXL.UseVisualStyleBackColor = false;
		this.CmdHelp2.ForeColor = System.Drawing.Color.Black;
		this.CmdHelp2.Location = new System.Drawing.Point(487, 512);
		this.CmdHelp2.Name = "CmdHelp2";
		this.CmdHelp2.Size = new System.Drawing.Size(26, 25);
		this.CmdHelp2.TabIndex = 15;
		this.CmdHelp2.Text = "?";
		this.ToolTip1.SetToolTip(this.CmdHelp2, "Help");
		this.CmdHelp2.UseVisualStyleBackColor = true;
		this.CmdHelp2.Visible = false;
		this.TxtOutXL.BackColor = System.Drawing.SystemColors.Window;
		this.TxtOutXL.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtOutXL.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtOutXL.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtOutXL.Location = new System.Drawing.Point(7, 244);
		this.TxtOutXL.Name = "TxtOutXL";
		this.TxtOutXL.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtOutXL.Size = new System.Drawing.Size(507, 24);
		this.TxtOutXL.TabIndex = 6;
		this.cmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdHelp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdHelp.Location = new System.Drawing.Point(525, 107);
		this.cmdHelp.Name = "cmdHelp";
		this.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdHelp.Size = new System.Drawing.Size(60, 34);
		this.cmdHelp.TabIndex = 19;
		this.cmdHelp.Text = "Help";
		this.cmdHelp.UseVisualStyleBackColor = false;
		this.FrmOpt.BackColor = System.Drawing.SystemColors.Control;
		this.FrmOpt.Controls.Add(this.OptXL_Import);
		this.FrmOpt.Controls.Add(this.OptXL_Load);
		this.FrmOpt.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FrmOpt.ForeColor = System.Drawing.SystemColors.ControlText;
		this.FrmOpt.Location = new System.Drawing.Point(7, 106);
		this.FrmOpt.Name = "FrmOpt";
		this.FrmOpt.Padding = new System.Windows.Forms.Padding(0);
		this.FrmOpt.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.FrmOpt.Size = new System.Drawing.Size(507, 44);
		this.FrmOpt.TabIndex = 16;
		this.FrmOpt.TabStop = false;
		this.OptXL_Import.BackColor = System.Drawing.SystemColors.Control;
		this.OptXL_Import.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptXL_Import.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptXL_Import.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptXL_Import.Location = new System.Drawing.Point(216, 16);
		this.OptXL_Import.Name = "OptXL_Import";
		this.OptXL_Import.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptXL_Import.Size = new System.Drawing.Size(243, 23);
		this.OptXL_Import.TabIndex = 3;
		this.OptXL_Import.TabStop = true;
		this.OptXL_Import.Text = "Import CSV files to WorkSheets";
		this.OptXL_Import.UseVisualStyleBackColor = false;
		this.OptXL_Load.BackColor = System.Drawing.SystemColors.Control;
		this.OptXL_Load.Checked = true;
		this.OptXL_Load.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptXL_Load.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptXL_Load.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptXL_Load.Location = new System.Drawing.Point(7, 16);
		this.OptXL_Load.Name = "OptXL_Load";
		this.OptXL_Load.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptXL_Load.Size = new System.Drawing.Size(137, 17);
		this.OptXL_Load.TabIndex = 2;
		this.OptXL_Load.TabStop = true;
		this.OptXL_Load.Text = "Load CSV file";
		this.OptXL_Load.UseVisualStyleBackColor = false;
		this.chkOption.AutoSize = true;
		this.chkOption.Checked = true;
		this.chkOption.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOption.Location = new System.Drawing.Point(7, 90);
		this.chkOption.Name = "chkOption";
		this.chkOption.Size = new System.Drawing.Size(94, 20);
		this.chkOption.TabIndex = 0;
		this.chkOption.Text = "chkOption";
		this.chkOption.UseVisualStyleBackColor = true;
		this.chkOption.Visible = false;
		this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCancel.Location = new System.Drawing.Point(525, 74);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdCancel.Size = new System.Drawing.Size(60, 34);
		this.cmdCancel.TabIndex = 18;
		this.cmdCancel.Text = "Close";
		this.cmdCancel.UseVisualStyleBackColor = false;
		this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdOK.Location = new System.Drawing.Point(525, 40);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdOK.Size = new System.Drawing.Size(60, 34);
		this.cmdOK.TabIndex = 17;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = false;
		this.CmdBrowse2.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowse2.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowse2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowse2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowse2.Location = new System.Drawing.Point(525, 304);
		this.CmdBrowse2.Name = "CmdBrowse2";
		this.CmdBrowse2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowse2.Size = new System.Drawing.Size(62, 25);
		this.CmdBrowse2.TabIndex = 9;
		this.CmdBrowse2.Text = "...";
		this.CmdBrowse2.UseVisualStyleBackColor = false;
		this.CmdBrowse1.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowse1.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowse1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowse1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowse1.Location = new System.Drawing.Point(525, 244);
		this.CmdBrowse1.Name = "CmdBrowse1";
		this.CmdBrowse1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowse1.Size = new System.Drawing.Size(62, 25);
		this.CmdBrowse1.TabIndex = 7;
		this.CmdBrowse1.Text = "...";
		this.CmdBrowse1.UseVisualStyleBackColor = false;
		this.CmdBrowse0.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowse0.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowse0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowse0.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowse0.Location = new System.Drawing.Point(525, 184);
		this.CmdBrowse0.Name = "CmdBrowse0";
		this.CmdBrowse0.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowse0.Size = new System.Drawing.Size(62, 25);
		this.CmdBrowse0.TabIndex = 5;
		this.CmdBrowse0.Text = "...";
		this.CmdBrowse0.UseVisualStyleBackColor = false;
		this.lblUtil.BackColor = System.Drawing.Color.Transparent;
		this.lblUtil.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblUtil.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblUtil.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblUtil.Location = new System.Drawing.Point(8, 40);
		this.lblUtil.Name = "lblUtil";
		this.lblUtil.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblUtil.Size = new System.Drawing.Size(506, 69);
		this.lblUtil.TabIndex = 15;
		this.lblUtil.Text = "Load file to Excel, or import files to worksheets. For import, separate files and sheets by commas, and optionally specify Macro to run.";
		this.lblXLMac.AutoSize = true;
		this.lblXLMac.BackColor = System.Drawing.SystemColors.Control;
		this.lblXLMac.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblXLMac.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblXLMac.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblXLMac.Location = new System.Drawing.Point(7, 404);
		this.lblXLMac.Name = "lblXLMac";
		this.lblXLMac.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblXLMac.Size = new System.Drawing.Size(309, 16);
		this.lblXLMac.TabIndex = 11;
		this.lblXLMac.Text = "Excel Macro to Run after Import (case sensitive)";
		this.lblXLWS.AutoSize = true;
		this.lblXLWS.BackColor = System.Drawing.SystemColors.Control;
		this.lblXLWS.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblXLWS.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblXLWS.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblXLWS.Location = new System.Drawing.Point(7, 344);
		this.lblXLWS.Name = "lblXLWS";
		this.lblXLWS.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblXLWS.Size = new System.Drawing.Size(321, 16);
		this.lblXLWS.TabIndex = 9;
		this.lblXLWS.Text = "Excel WorkSheets for CSV Import (case sensitive)";
		this.lblXLIn.AutoSize = true;
		this.lblXLIn.BackColor = System.Drawing.SystemColors.Control;
		this.lblXLIn.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblXLIn.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblXLIn.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblXLIn.Location = new System.Drawing.Point(7, 284);
		this.lblXLIn.Name = "lblXLIn";
		this.lblXLIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblXLIn.Size = new System.Drawing.Size(412, 16);
		this.lblXLIn.TabIndex = 7;
		this.lblXLIn.Text = "Excel Source File for Import (no space nor special char in name)";
		this.lblXLOut.AutoSize = true;
		this.lblXLOut.BackColor = System.Drawing.SystemColors.Control;
		this.lblXLOut.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblXLOut.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblXLOut.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblXLOut.Location = new System.Drawing.Point(7, 224);
		this.lblXLOut.Name = "lblXLOut";
		this.lblXLOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblXLOut.Size = new System.Drawing.Size(329, 16);
		this.lblXLOut.TabIndex = 3;
		this.lblXLOut.Text = "Excel File to Create [Default is SQLPathFinder.xlsx]";
		this.lblcsv.AutoSize = true;
		this.lblcsv.BackColor = System.Drawing.SystemColors.Control;
		this.lblcsv.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblcsv.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblcsv.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblcsv.Location = new System.Drawing.Point(7, 164);
		this.lblcsv.Name = "lblcsv";
		this.lblcsv.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblcsv.Size = new System.Drawing.Size(115, 16);
		this.lblcsv.TabIndex = 1;
		this.lblcsv.Text = "CSV File to Load";
		this.TxtInXL.FormattingEnabled = true;
		this.TxtInXL.Location = new System.Drawing.Point(7, 304);
		this.TxtInXL.Name = "TxtInXL";
		this.TxtInXL.Size = new System.Drawing.Size(507, 24);
		this.TxtInXL.TabIndex = 8;
		this.TxtXLSheet.FormattingEnabled = true;
		this.TxtXLSheet.Location = new System.Drawing.Point(7, 364);
		this.TxtXLSheet.Name = "TxtXLSheet";
		this.TxtXLSheet.Size = new System.Drawing.Size(507, 24);
		this.TxtXLSheet.TabIndex = 10;
		this.TxtCSV.FormattingEnabled = true;
		this.TxtCSV.Location = new System.Drawing.Point(7, 184);
		this.TxtCSV.Name = "TxtCSV";
		this.TxtCSV.Size = new System.Drawing.Size(507, 24);
		this.TxtCSV.TabIndex = 4;
		this.TxtOther1.FormattingEnabled = true;
		this.TxtOther1.Location = new System.Drawing.Point(7, 484);
		this.TxtOther1.Name = "TxtOther1";
		this.TxtOther1.Size = new System.Drawing.Size(507, 24);
		this.TxtOther1.TabIndex = 13;
		this.TxtOther1.Visible = false;
		this.LblOther1.AutoSize = true;
		this.LblOther1.Location = new System.Drawing.Point(7, 464);
		this.LblOther1.Name = "LblOther1";
		this.LblOther1.Size = new System.Drawing.Size(63, 16);
		this.LblOther1.TabIndex = 19;
		this.LblOther1.Text = "lblother1";
		this.LblOther1.Visible = false;
		this.LblOther2.AutoSize = true;
		this.LblOther2.Location = new System.Drawing.Point(7, 524);
		this.LblOther2.Name = "LblOther2";
		this.LblOther2.Size = new System.Drawing.Size(71, 16);
		this.LblOther2.TabIndex = 20;
		this.LblOther2.Text = "LblOther2";
		this.LblOther2.Visible = false;
		this.TxtOther2.FormattingEnabled = true;
		this.TxtOther2.Location = new System.Drawing.Point(7, 544);
		this.TxtOther2.Name = "TxtOther2";
		this.TxtOther2.Size = new System.Drawing.Size(507, 24);
		this.TxtOther2.TabIndex = 14;
		this.TxtOther2.Visible = false;
		this.CmdBrowse3.Location = new System.Drawing.Point(525, 544);
		this.CmdBrowse3.Name = "CmdBrowse3";
		this.CmdBrowse3.Size = new System.Drawing.Size(62, 25);
		this.CmdBrowse3.TabIndex = 16;
		this.CmdBrowse3.Text = "...";
		this.CmdBrowse3.UseVisualStyleBackColor = true;
		this.CmbOpt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmbOpt.FormattingEnabled = true;
		this.CmbOpt.Location = new System.Drawing.Point(421, 86);
		this.CmbOpt.Name = "CmbOpt";
		this.CmbOpt.Size = new System.Drawing.Size(92, 24);
		this.CmbOpt.TabIndex = 1;
		this.CmbOpt.Visible = false;
		this.TxtXLMacro.FormattingEnabled = true;
		this.TxtXLMacro.Location = new System.Drawing.Point(7, 424);
		this.TxtXLMacro.Name = "TxtXLMacro";
		this.TxtXLMacro.Size = new System.Drawing.Size(507, 24);
		this.TxtXLMacro.TabIndex = 12;
		this.mnuoptstoolstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuoptstoolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuopts });
		this.mnuoptstoolstrip.Location = new System.Drawing.Point(0, 0);
		this.mnuoptstoolstrip.Name = "mnuoptstoolstrip";
		this.mnuoptstoolstrip.Size = new System.Drawing.Size(589, 28);
		this.mnuoptstoolstrip.TabIndex = 22;
		this.mnuoptstoolstrip.Text = "Options";
		this.mnuopts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.mnulabel1, this.mnulabel2, this.mnulabel3, this.mnulabel4, this.OrderedParallelMenuItem, this.ParallelThreadsMenuItem });
		this.mnuopts.Name = "mnuopts";
		this.mnuopts.Size = new System.Drawing.Size(75, 24);
		this.mnuopts.Text = "Options";
		this.mnuopts.Visible = false;
		this.mnulabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuopt1 });
		this.mnulabel1.Name = "mnulabel1";
		this.mnulabel1.Size = new System.Drawing.Size(403, 26);
		this.mnulabel1.Text = "Default Value if Old Col Missing in New File";
		this.mnulabel1.Visible = false;
		this.mnuopt1.Name = "mnuopt1";
		this.mnuopt1.Size = new System.Drawing.Size(121, 28);
		this.mnulabel2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuOpt2 });
		this.mnulabel2.Name = "mnulabel2";
		this.mnulabel2.Size = new System.Drawing.Size(403, 26);
		this.mnulabel2.Text = "Rows to write to CSV file at a time (Chunk Size)";
		this.mnulabel2.Visible = false;
		this.mnuOpt2.DropDownWidth = 150;
		this.mnuOpt2.Name = "mnuOpt2";
		this.mnuOpt2.Size = new System.Drawing.Size(121, 28);
		this.mnulabel3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuopt3 });
		this.mnulabel3.Name = "mnulabel3";
		this.mnulabel3.Size = new System.Drawing.Size(403, 26);
		this.mnulabel3.Text = "Visual ID Field Name";
		this.mnulabel3.Visible = false;
		this.mnuopt3.Name = "mnuopt3";
		this.mnuopt3.Size = new System.Drawing.Size(121, 28);
		this.mnulabel4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuopt4 });
		this.mnulabel4.Name = "mnulabel4";
		this.mnulabel4.Size = new System.Drawing.Size(403, 26);
		this.mnulabel4.Text = "Signal Operation (Exclude operations after)";
		this.mnulabel4.Visible = false;
		this.mnuopt4.Name = "mnuopt4";
		this.mnuopt4.Size = new System.Drawing.Size(121, 28);
		this.OrderedParallelMenuItem.Checked = true;
		this.OrderedParallelMenuItem.CheckOnClick = true;
		this.OrderedParallelMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
		this.OrderedParallelMenuItem.Name = "OrderedParallelMenuItem";
		this.OrderedParallelMenuItem.Size = new System.Drawing.Size(403, 26);
		this.OrderedParallelMenuItem.Text = "Ordered Append Inside Parallel Execution";
		this.OrderedParallelMenuItem.Visible = false;
		this.ParallelThreadsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.ParallelThreadsComboBox1 });
		this.ParallelThreadsMenuItem.Name = "ParallelThreadsMenuItem";
		this.ParallelThreadsMenuItem.Size = new System.Drawing.Size(403, 26);
		this.ParallelThreadsMenuItem.Text = "Parallel Threads";
		this.ParallelThreadsMenuItem.Visible = false;
		this.ParallelThreadsComboBox1.Name = "ParallelThreadsComboBox1";
		this.ParallelThreadsComboBox1.Size = new System.Drawing.Size(121, 28);
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(589, 574);
		base.Controls.Add(this.TxtXLMacro);
		base.Controls.Add(this.CmbOpt);
		base.Controls.Add(this.CmdHelp2);
		base.Controls.Add(this.CmdBrowse3);
		base.Controls.Add(this.TxtOther2);
		base.Controls.Add(this.LblOther2);
		base.Controls.Add(this.LblOther1);
		base.Controls.Add(this.TxtOther1);
		base.Controls.Add(this.TxtCSV);
		base.Controls.Add(this.TxtXLSheet);
		base.Controls.Add(this.chkOption);
		base.Controls.Add(this.TxtInXL);
		base.Controls.Add(this.TxtOutXL);
		base.Controls.Add(this.cmdHelp);
		base.Controls.Add(this.CmdViewXL);
		base.Controls.Add(this.FrmOpt);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.CmdBrowse2);
		base.Controls.Add(this.CmdBrowse1);
		base.Controls.Add(this.CmdBrowse0);
		base.Controls.Add(this.lblUtil);
		base.Controls.Add(this.lblXLMac);
		base.Controls.Add(this.lblXLWS);
		base.Controls.Add(this.lblXLIn);
		base.Controls.Add(this.lblXLOut);
		base.Controls.Add(this.lblcsv);
		base.Controls.Add(this.mnuoptstoolstrip);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(3, 29);
		base.MainMenuStrip = this.mnuoptstoolstrip;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmUtil";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.FrmOpt.ResumeLayout(false);
		this.mnuoptstoolstrip.ResumeLayout(false);
		this.mnuoptstoolstrip.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public string Browse_Folder(string MyTitle, string MyDefDir)
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
				case 343:
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
							goto IL_002a;
						case 5:
							goto IL_0033;
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0063;
						case 8:
							goto IL_0082;
						case 9:
							goto IL_0099;
						case 10:
							goto IL_00b7;
						case 11:
							goto IL_00cb;
						case 12:
							goto IL_00ed;
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
					IL_00b7:
					num2 = 10;
					text = Strings.Trim(text) + "\\";
					goto IL_00cb;
					IL_00cb:
					num2 = 11;
					if (Operators.CompareString(Strings.UCase(text), Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00ed;
					IL_0099:
					num2 = 9;
					if (Operators.CompareString(Strings.Right(text, 1), "\\", TextCompare: false) != 0)
					{
						goto IL_00b7;
					}
					goto IL_00cb;
					IL_00ed:
					num2 = 12;
					text = ".";
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.CompareString(MyDefDir, "", TextCompare: false) == 0)
					{
						goto IL_002a;
					}
					goto IL_0033;
					IL_002a:
					num2 = 4;
					MyDefDir = Globals_Renamed.MyPCDir;
					goto IL_0033;
					IL_0033:
					num2 = 5;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = MyTitle;
					goto IL_004b;
					IL_004b:
					num2 = 6;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = MyDefDir;
					goto IL_0063;
					IL_0063:
					num2 = 7;
					if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() != DialogResult.OK)
					{
						break;
					}
					goto IL_0082;
					IL_0082:
					num2 = 8;
					text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
					goto IL_0099;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 343;
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
		return text;
	}

	public void Add_Cmb(string ValidTxt, ref ComboBox MyCMB)
	{
		int num = 0;
		string text = "";
		text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, ValidTxt, "", 5000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
		if (Operators.CompareString(text, "", TextCompare: false) == 0)
		{
			return;
		}
		string[] array = Strings.Split(text, ",");
		int num2 = Information.UBound(array);
		for (num = 0; num <= num2; num = checked(num + 1))
		{
			array[num] = Strings.Trim(array[num]);
			if (Operators.CompareString(array[num], "", TextCompare: false) != 0)
			{
				MyCMB.Items.Add(Strings.Replace(array[num], "<comma>", ",", 1, -1, CompareMethod.Text));
			}
		}
		array = null;
	}

	public void Def_Cmb(string ValidTxt, ref ComboBox MyCMB)
	{
		string text = "";
		text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, ValidTxt, "", 5000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
		if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(MyCMB.Text, "", TextCompare: false) == 0)
		{
			MyCMB.Text = text;
		}
	}

	public int Process_Shortcuts(string MyQ, string MyPath, string MyDesc, string MyExe)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		int result;
		try
		{
			result = 0;
			if (Operators.CompareString(MyDesc, "", TextCompare: false) == 0)
			{
				MyDesc = "SQLPathFinder Query Execution Shortcut";
			}
			if (Operators.CompareString(MyExe, "", TextCompare: false) == 0)
			{
				MyExe = "%userprofile%\\my programs\\sqlpathfinder3\\sqlpathfinder3.exe";
			}
			WshShell val = (WshShell)new WshShellClass();
			IWshShortcut val2 = null;
			val2 = (IWshShortcut)((IWshShell3)val).CreateShortcut(MyPath);
			IWshShortcut val3 = val2;
			val3.TargetPath = MyExe;
			val3.WindowStyle = 6;
			val3.Description = MyDesc;
			val3.WorkingDirectory = "%temp%";
			val3.IconLocation = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\report.ico,0";
			val3.Arguments = "\"" + MyQ + "\" /RUN /MINIMIZE";
			val3.Save();
			Interaction.MsgBox("Shortcut Created", MsgBoxStyle.Information, "Success!");
			val3 = null;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			result = 1;
			Interaction.MsgBox("Could not create Shortcut:\r\n" + ex2.Message, MsgBoxStyle.Exclamation, "Shortcut Error");
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public void Chk_Cmb()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
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
				case 928:
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
							goto IL_003d;
						case 5:
							goto IL_004c;
						case 6:
							goto IL_005b;
						case 7:
							goto IL_006a;
						case 8:
							goto IL_0079;
						case 9:
							goto IL_0096;
						case 10:
							goto IL_00aa;
						case 11:
							goto IL_00c9;
						case 12:
							goto IL_0106;
						case 13:
							goto IL_011a;
						case 14:
							goto IL_012e;
						case 15:
							goto IL_0147;
						case 16:
							goto IL_0160;
						case 17:
							goto IL_0170;
						case 18:
							goto IL_0180;
						case 19:
							goto IL_019b;
						case 20:
							goto IL_01ab;
						case 21:
							goto IL_01bb;
						case 22:
							goto IL_01cb;
						case 25:
							goto IL_01e4;
						case 26:
							goto IL_01f8;
						case 27:
							goto IL_0214;
						case 28:
							goto IL_0228;
						case 29:
							goto IL_023c;
						case 30:
							goto IL_0255;
						case 31:
							goto IL_026e;
						case 34:
							goto IL_028b;
						case 35:
							goto IL_029e;
						case 36:
							goto IL_02b9;
						case 38:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 23:
						case 24:
						case 32:
						case 33:
						case 37:
						case 39:
						case 40:
						case 41:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_026e:
					num2 = 31;
					TxtOther1.Items.Add("500");
					goto end_IL_0001_3;
					IL_028b:
					num2 = 34;
					if (!BuildForm.IsUtil("EXCEL"))
					{
						goto end_IL_0001_3;
					}
					goto IL_029e;
					IL_0255:
					num2 = 30;
					TxtOther1.Items.Add("1000");
					goto IL_026e;
					IL_029e:
					num2 = 35;
					if (Operators.CompareString(left, "Version2", TextCompare: false) < 0)
					{
						break;
					}
					goto IL_02b9;
					IL_000b:
					num2 = 2;
					left = CmbOpt.Text.Replace(" ", "");
					goto IL_0028;
					IL_0028:
					num2 = 3;
					if (BuildForm.IsUtil("SMART-APPEND"))
					{
						goto IL_003d;
					}
					goto IL_028b;
					IL_003d:
					num2 = 4;
					mnulabel2.Visible = false;
					goto IL_004c;
					IL_004c:
					num2 = 5;
					mnuOpt2.Visible = false;
					goto IL_005b;
					IL_005b:
					num2 = 6;
					mnulabel1.Visible = false;
					goto IL_006a;
					IL_006a:
					num2 = 7;
					mnuopt1.Visible = false;
					goto IL_0079;
					IL_0079:
					num2 = 8;
					if (Operators.CompareString(left, "Version3", TextCompare: false) >= 0)
					{
						goto IL_0096;
					}
					goto IL_01e4;
					IL_0096:
					num2 = 9;
					LblOther1.Text = "Abort if Old file not Found";
					goto IL_00aa;
					IL_00aa:
					num2 = 10;
					TxtOther1.Text = Strings.UCase(TxtOther1.Text);
					goto IL_00c9;
					IL_00c9:
					num2 = 11;
					if (Operators.CompareString(TxtOther1.Text, "Y", TextCompare: false) != 0 && Operators.CompareString(TxtOther1.Text, "N", TextCompare: false) != 0)
					{
						goto IL_0106;
					}
					goto IL_011a;
					IL_02b9:
					num2 = 36;
					lblXLOut.Text = "Excel File to Create [Default is SQLPathFinder.xlsx]";
					goto end_IL_0001_3;
					IL_0106:
					num2 = 12;
					TxtOther1.Text = "N";
					goto IL_011a;
					IL_011a:
					num2 = 13;
					TxtOther1.Items.Clear();
					goto IL_012e;
					IL_012e:
					num2 = 14;
					TxtOther1.Items.Add("Y");
					goto IL_0147;
					IL_0147:
					num2 = 15;
					TxtOther1.Items.Add("N");
					goto IL_0160;
					IL_0160:
					num2 = 16;
					mnuopt1.Visible = true;
					goto IL_0170;
					IL_0170:
					num2 = 17;
					mnulabel1.Visible = true;
					goto IL_0180;
					IL_0180:
					num2 = 18;
					if (Operators.CompareString(left, "Version4", TextCompare: false) < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_019b;
					IL_019b:
					num2 = 19;
					mnuopt1.Visible = false;
					goto IL_01ab;
					IL_01ab:
					num2 = 20;
					mnulabel1.Visible = false;
					goto IL_01bb;
					IL_01bb:
					num2 = 21;
					mnuOpt2.Visible = true;
					goto IL_01cb;
					IL_01cb:
					num2 = 22;
					mnulabel2.Visible = true;
					goto end_IL_0001_3;
					IL_01e4:
					num2 = 25;
					LblOther1.Text = "Rows in old file to process at a time if file is large, or -1 to process in one go";
					goto IL_01f8;
					IL_01f8:
					num2 = 26;
					if (!Versioned.IsNumeric(TxtOther1.Text))
					{
						goto IL_0214;
					}
					goto IL_0228;
					IL_0214:
					num2 = 27;
					TxtOther1.Text = "-1";
					goto IL_0228;
					IL_0228:
					num2 = 28;
					TxtOther1.Items.Clear();
					goto IL_023c;
					IL_023c:
					num2 = 29;
					TxtOther1.Items.Add("-1");
					goto IL_0255;
					end_IL_0001_2:
					break;
				}
				num2 = 38;
				lblXLOut.Text = "Excel File to Create [Default is SQLPathFinder.xls]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 928;
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

	public string Get_Node_H(string MySite, string MyPattern, int MyMode, string ALLOne)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		FrmNodeSel frmNodeSel = default(FrmNodeSel);
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
				case 280:
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
							goto IL_0019;
						case 5:
							goto IL_0023;
						case 6:
							goto IL_0031;
						case 7:
							goto IL_003b;
						case 8:
							goto IL_0045;
						case 9:
							goto IL_004f;
						case 10:
							goto IL_005a;
						case 11:
							goto IL_007d;
						case 12:
							goto IL_008e;
						case 13:
							goto IL_009c;
						case 14:
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
					IL_007d:
					num2 = 11;
					num5 = Strings.InStr(text, ",");
					goto IL_008e;
					IL_008e:
					num2 = 12;
					if (num5 <= 1)
					{
						break;
					}
					goto IL_009c;
					IL_005a:
					num2 = 10;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(ALLOne)), "ONE", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_007d;
					IL_009c:
					num2 = 13;
					text = Strings.Trim(Strings.Mid(text, 1, checked(num5 - 1)));
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					frmNodeSel = new FrmNodeSel();
					goto IL_0019;
					IL_0019:
					num2 = 4;
					frmNodeSel.f_OutNode = MySite;
					goto IL_0023;
					IL_0023:
					num2 = 5;
					frmNodeSel.f_DBType = "5";
					goto IL_0031;
					IL_0031:
					num2 = 6;
					frmNodeSel.f_Mode = MyMode;
					goto IL_003b;
					IL_003b:
					num2 = 7;
					frmNodeSel.f_Pattern = MyPattern;
					goto IL_0045;
					IL_0045:
					num2 = 8;
					frmNodeSel.ShowDialog();
					goto IL_004f;
					IL_004f:
					num2 = 9;
					text = frmNodeSel.f_OutNode;
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				frmNodeSel.Dispose();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 280;
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
		return text;
	}

	public void Browse_Dir(short MyIndex)
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
						string text = "";
						string FileNames = "";
						string text2 = "";
						short num3 = 0;
						int MyCount = 0;
						int num4 = 0;
						int num5 = 0;
						string text3 = "";
						int num6 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						switch (MyIndex)
						{
						case 0:
							if (BuildForm.IsUtil("EMAIL") | BuildForm.IsUtil("ZIP") | BuildForm.IsUtil("COPY") | BuildForm.IsUtil("SHAREPOINT") | BuildForm.IsUtil("SQLITE-LOAD") | BuildForm.IsUtil("ROBOCOPY"))
							{
								FileNames = "";
								if (BuildForm.IsUtil("ROBOCOPY"))
								{
									num4 = 2;
								}
								BuildForm.File_Multi((short)num4, ref FileNames, ref MyCount);
								text = FileNames;
							}
							else if (BuildForm.IsUtil("GET-FILES"))
							{
								MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Source Folder";
								if (Operators.CompareString(TxtCSV.Text, "", TextCompare: false) != 0)
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = TxtCSV.Text;
								}
								else
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = Globals_Renamed.MyPCDir;
								}
								if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
								{
									TxtCSV.Text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
								}
							}
							else if (BuildForm.IsUtil("APPEND") | BuildForm.IsUtil("SMART-APPEND"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "CSV  File to Append to", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("UPDATE-TIME"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "CSV File to Update With Last Job Start Time", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("XML-TO-CSV") || BuildForm.IsUtil("GET-TCA"))
							{
								BuildForm.FileOpenSave("O", "", "xml", "XML  Input File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("TDX-TO-CSV"))
							{
								FileNames = Browse_Folder("Locate TDX Source Folder", BuildForm.Replace_Globals("<<<spf-tdx-data1>>>"));
								if (Operators.CompareString(FileNames, "", TextCompare: false) != 0)
								{
									text = FileNames;
								}
							}
							else if (BuildForm.IsUtil("PROMPT-INPUT"))
							{
								BuildForm.FileOpenSave("O", "", "ini", "INI  File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("R"))
							{
								BuildForm.FileOpenSave("O", "", "r", "R Script to run", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("EXCEL-TO-CSV"))
							{
								BuildForm.FileOpenSave("O", "", "xls", "XLS  File to Convert", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("UNZIP"))
							{
								BuildForm.FileOpenSave("O", "", "zip", "ZIP  File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("WEB-COPY"))
							{
								BuildForm.Get_HTTP_Help(Strings.Trim(TxtCSV.Text), "Batch_Options.htm#retention");
							}
							else if (BuildForm.IsUtil("SITE-LOOP") || BuildForm.IsUtil("GET-SITE-TIME"))
							{
								TxtCSV.Text = Get_Node_H(TxtCSV.Text, "", 2, "ALL");
							}
							else if (BuildForm.IsUtil("MONGODB-IMPORT") || BuildForm.IsUtil("MONGODB-EXTRACT"))
							{
								TxtCSV.Text = Get_Node_H(TxtCSV.Text, "*mongo*", 1, "ONE");
							}
							else if (BuildForm.IsUtil("CREATE-QUERY-SHORTCUT"))
							{
								BuildForm.FileOpenSave("O", "", "vg2", "Find a SQLPathFinder Query ", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("PYTHON"))
							{
								BuildForm.FileOpenSave("O", "", "py", "Python Script to Run", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("ROWS-IN-FILE"))
							{
								BuildForm.FileOpenSave("O", "", "csvpz+", "Find CSV,TAB,Parquet,ZIP File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else
							{
								BuildForm.FileOpenSave("O", "", "csv+", "Find CSV or TAB File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							break;
						case 1:
							if (BuildForm.IsUtil("JOIN"))
							{
								BuildForm.File_Multi(0, ref FileNames, ref MyCount);
								text = FileNames;
								break;
							}
							if (BuildForm.IsUtil("BEGIN-HPC"))
							{
								BuildForm.FileOpenSave("O", "", "txt", "File Storing Required Input Files", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
								break;
							}
							if (BuildForm.IsUtil("ROBOCOPY"))
							{
								MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Source Folder";
								if (Operators.CompareString(TxtOutXL.Text, "", TextCompare: false) != 0)
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = TxtOutXL.Text;
								}
								else
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = Globals_Renamed.MyPCDir;
								}
								if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
								{
									TxtOutXL.Text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
								}
								break;
							}
							if (BuildForm.IsUtil("SHAREPOINT"))
							{
								BuildForm.Get_HTTP_Help(TxtOutXL.Text, "https://intel.sharepoint.com/");
								goto end_IL_0001;
							}
							num3 = 0;
							if (Operators.CompareString(Strings.Trim(TxtOutXL.Text), "", TextCompare: false) != 0)
							{
								MyProject.Forms.FrmMain.CMDialog1Open.FileName = Strings.Trim(TxtOutXL.Text);
								MyProject.Forms.FrmMain.CMDialog1Save.FileName = Strings.Trim(TxtOutXL.Text);
								if (TxtOutXL.Text.Length >= 4 && Operators.CompareString(Strings.UCase(TxtOutXL.Text.Substring(TxtOutXL.Text.Length - 3)), "JSL", TextCompare: false) == 0)
								{
									num3 = 1;
								}
							}
							if (BuildForm.IsUtil("COPY") | BuildForm.IsUtil("STACK") | BuildForm.IsUtil("RUN-LOOP") | BuildForm.IsUtil("RENAME"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "Save File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("FILE-COMPARE"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "Find CSV or TAB File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("IDEAL-TABLE-CONFIG") || BuildForm.IsUtil("AUTO-COMMONALITY"))
							{
								BuildForm.FileOpenSave("O", "", "csv", "Find Field-Pattern Action File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("TDX-TO-CSV"))
							{
								FileNames = Browse_Folder("Locate Output Folder", "");
								if (Operators.CompareString(FileNames, "", TextCompare: false) != 0)
								{
									text = FileNames;
								}
							}
							else if (BuildForm.IsUtil("APPEND"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "Source File or File Pattern for Append", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("MONGODB-EXTRACT"))
							{
								BuildForm.FileOpenSave("S", "", "csv", "CSV Output File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("SMART-APPEND"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "Source File for Append", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("MONGODB-IMPORT"))
							{
								BuildForm.FileOpenSave("O", "", "csv+++", "File to Import", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("VALUE-IN-FILE") || BuildForm.IsUtil("AGE-OF-FILE") || BuildForm.IsUtil("DATE-OF-FILE") || BuildForm.IsUtil("ROWS-IN-FILE"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File to Access", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("VERIFY-ROLE"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File with User List", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("XML-TO-CSV") || BuildForm.IsUtil("WEB-COPY") || BuildForm.IsUtil("EXCEL-TO-CSV") || BuildForm.IsUtil("PROMPT-INPUT") || BuildForm.IsUtil("GET-TCA"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "CSV File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("GET-AQUA"))
							{
								BuildForm.FileOpenSave("S", "", "aqua", "CSV File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("CSV-TO-XML"))
							{
								BuildForm.FileOpenSave("S", "", "xml", "XML  File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("SQLITE-LOAD"))
							{
								BuildForm.FileOpenSave("S", "", "sdb", "SQLite Database to Use or to Create (.sdb)", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("ZIP"))
							{
								BuildForm.FileOpenSave("S", "", "zip", "ZIP  File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("CSV-TO-HTML"))
							{
								BuildForm.FileOpenSave("S", "", "htm", "HTML  File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("CONVERT-IMBIGDATA-FUNCTIONS"))
							{
								BuildForm.FileOpenSave("S", "", "txt", "imBigData Function File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("EMAIL"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File With Email Addresses", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("JMP"))
							{
								MyProject.Forms.FrmMain.CMDialog1Save.FileName = "";
								MyProject.Forms.FrmMain.CMDialog1Save.InitialDirectory = Globals_Renamed.MyPCDir;
								MyProject.Forms.FrmMain.CMDialog1Save.CheckPathExists = true;
								MyProject.Forms.FrmMain.CMDialog1Save.Title = "Save JMP File or Locate JSL Script";
								MyProject.Forms.FrmMain.CMDialog1Save.Filter = "JMP Files (*.jmp)|*.jmp;*.JMP |JSL Scripts (*.jsl)|*.jsl;*.JSL|AllFiles (*.*)|*.*";
								if (num3 == 1)
								{
									MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "jsl";
									MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 2;
								}
								else
								{
									MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "jmp";
									MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 1;
								}
								text = ((MyProject.Forms.FrmMain.CMDialog1Save.ShowDialog() != DialogResult.OK) ? "CANCEL" : MyProject.Forms.FrmMain.CMDialog1Save.FileName);
							}
							else if (BuildForm.IsUtil("CREATE-QUERY-SHORTCUT"))
							{
								BuildForm.FileOpenSave("S", "", "lnk", "Create Query Shortcut", Globals_Renamed.gQueryDir);
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else
							{
								BuildForm.FileOpenSave("S", "", "xls", "Save Excel File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							break;
						case 2:
							if (BuildForm.IsUtil("JOIN"))
							{
								BuildForm.FileOpenSave("S", TxtInXL.Text, "csv+", "CSV Join File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
								break;
							}
							if (BuildForm.IsUtil("BEGIN-HPC"))
							{
								BuildForm.FileOpenSave("O", "", "txt", "File Storing Required Output Files", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
								break;
							}
							if (BuildForm.IsUtil("IDEAL-TABLE-CONFIG") || BuildForm.IsUtil("AUTO-COMMONALITY"))
							{
								BuildForm.FileOpenSave("S", TxtInXL.Text, "ide", "Ideal File to Create", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
								break;
							}
							if (BuildForm.IsUtil("MONGODB-EXTRACT"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File with Fields to Export", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
								break;
							}
							if (BuildForm.IsUtil("ROBOCOPY"))
							{
								MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Destination Folder";
								if (Operators.CompareString(TxtInXL.Text, "", TextCompare: false) != 0)
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = TxtInXL.Text;
								}
								else
								{
									MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = Globals_Renamed.MyPCDir;
								}
								if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
								{
									TxtInXL.Text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
								}
								break;
							}
							if (BuildForm.IsUtil("STACK"))
							{
								Start_XL(TxtCSV.Text);
								goto end_IL_0001;
							}
							if (BuildForm.IsUtil("EMAIL"))
							{
								BuildForm.FileOpenSave("O", "", "txt+", "Find Email Message File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("CSV-TO-XML"))
							{
								BuildForm.FileOpenSave("O", "", "xsl", "Find XSL Style Sheet", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("TDX-TO-CSV"))
							{
								BuildForm.FileOpenSave("O", "", "xml", "Find TDX Conversion Spec", BuildForm.Replace_Globals("<<<spf-tdx-config>>>"));
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("GET-FILES"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "CSV File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else if (BuildForm.IsUtil("VALUE-IN-FILE"))
							{
								text2 = "";
								FileNames = Strings.Trim(TxtCSV.Text);
								if (Operators.CompareString(FileNames, "", TextCompare: false) != 0)
								{
									if (Strings.InStr(FileNames, "\\") == 0)
									{
										FileNames = Globals_Renamed.MyPCDir + Strings.Trim(FileNames);
									}
									if (!MyProject.Computer.FileSystem.FileExists(FileNames))
									{
										text2 = "Cannot Access CSV File";
									}
									if (Operators.CompareString(text2, "", TextCompare: false) != 0)
									{
										Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, "Cannot Load Headers");
										text2 = "";
										goto end_IL_0001;
									}
									string currentFile = FileNames;
									ComboBox MyCombo = TxtInXL;
									General_Procedures.LoadCSVHeaders(currentFile, ref MyCombo, 1);
									TxtInXL = MyCombo;
									goto end_IL_0001;
								}
								text2 = "No CSV File Name assigned";
							}
							else if (BuildForm.IsUtil("EXCEL-TO-CSV"))
							{
								Start_XL(TxtCSV.Text);
							}
							else
							{
								BuildForm.FileOpenSave("O", "", "xls", "Find Excel File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							break;
						case 3:
							if (BuildForm.IsUtil("EMAIL"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File With Email Addresses", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("BEGIN-HPC"))
							{
								BuildForm.FileOpenSave("O", "", "txt", "File Storing Optional Output Files", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("MONGODB-EXTRACT"))
							{
								BuildForm.FileOpenSave("O", "", "txt", "File With MongoDB _ids to search. First column must store header _id", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("AUTO-COMMONALITY"))
							{
								BuildForm.FileOpenSave("S", "", "csv+", "Output CSV File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							else
							{
								BuildForm.FileOpenSave("O", "", "csv+", "Find CSV File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							break;
						case 4:
							if (BuildForm.IsUtil("EMAIL"))
							{
								BuildForm.FileOpenSave("O", "", "csv+", "File With Email Addresses", "");
								text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else if (BuildForm.IsUtil("AUTO-COMMONALITY"))
							{
								BuildForm.FileOpenSave("S", "", "htm", "HTML Output File", "");
								text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
							break;
						}
						if ((Operators.CompareString(text, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(text, "", TextCompare: false) == 0))
						{
							break;
						}
						if (!BuildForm.IsUtil("CREATE-QUERY-SHORTCUT"))
						{
							text = BuildForm.Strip_Add_MyPCDir("S", text);
						}
						switch (MyIndex)
						{
						case 0:
							TxtCSV.Text = text;
							goto end_IL_0001;
						case 1:
							TxtOutXL.Text = text;
							goto end_IL_0001;
						case 2:
							TxtInXL.Text = text;
							goto end_IL_0001;
						case 3:
							if (!BuildForm.IsUtil("EMAIL"))
							{
								break;
							}
							TxtOther1.Text = text;
							goto end_IL_0001;
						}
						if (MyIndex == 3 && (BuildForm.IsUtil("MONGODB-EXTRACT") || BuildForm.IsUtil("BEGIN-HPC")))
						{
							TxtXLSheet.Text = text;
						}
						else if (MyIndex == 4 && BuildForm.IsUtil("EMAIL"))
						{
							TxtXLMacro.Text = text;
						}
						else if (MyIndex == 4 && BuildForm.IsUtil("AUTO-COMMONALITY"))
						{
							TxtXLSheet.Text = text;
						}
						else if (MyIndex == 3 && BuildForm.IsUtil("AUTO-COMMONALITY"))
						{
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								TxtOther2.Text = text;
							}
							else
							{
								TxtOther2.Text = "None";
							}
						}
						else
						{
							TxtOther2.Text = text;
						}
						break;
					}
					case 6120:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								break;
							default:
								goto IL_181e;
							}
							break;
						}
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6120;
				continue;
			}
			break;
			IL_181e:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Start_XL(string MyFile)
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
					ProjectData.ClearProjectError();
					num2 = 2;
					MyFile = Strings.Trim(MyFile);
					if (Operators.CompareString(MyFile, "", TextCompare: false) != 0)
					{
						if (Strings.InStr(MyFile, "\\") == 0)
						{
							MyFile = ".\\" + MyFile;
						}
						int hwnd = MyProject.Forms.FrmMain.Handle.ToInt32();
						string lpOperation = "Open";
						string lpParameters = "";
						int num3 = Globals_Renamed.ShellExecute(hwnd, ref lpOperation, ref MyFile, ref lpParameters, ref Globals_Renamed.MyPCDir, 1);
						if (num3 <= 32)
						{
							Interaction.MsgBox("Error accessing input file. Check file path", MsgBoxStyle.Critical, "File Error");
						}
					}
					goto end_IL_0001;
				case 205:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error Starting Application. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "App Error");
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 205;
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
					Globals_Renamed.currvaluetmp = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Close();
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
							goto IL_0025;
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
					if (Operators.CompareString(fHelpType, "P", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0025;
					IL_0025:
					num2 = 3;
					BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Process+Multiple+Queries+Form");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Adding+Utilities");
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

	private void cmdOK_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string MyOpr = default(string);
		int num5 = default(int);
		bool flag = default(bool);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text12 = default(string);
		string text13 = default(string);
		string MyOpr2 = default(string);
		string text14 = default(string);
		int num6 = default(int);
		string text15 = default(string);
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
				case 15196:
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
							goto IL_0073;
						case 11:
							goto IL_00a5;
						case 12:
							goto IL_00d7;
						case 13:
							goto IL_0109;
						case 14:
							goto IL_011e;
						case 15:
							goto IL_013c;
						case 16:
							goto IL_0151;
						case 17:
							goto IL_016f;
						case 18:
							goto IL_0184;
						case 19:
							goto IL_01a2;
						case 20:
							goto IL_01b7;
						case 21:
							goto IL_01d5;
						case 22:
							goto IL_01ea;
						case 23:
							goto IL_0208;
						case 24:
							goto IL_021d;
						case 25:
							goto IL_023b;
						case 26:
							goto IL_0250;
						case 27:
							goto IL_026e;
						case 28:
							goto IL_027b;
						case 29:
							goto IL_0291;
						case 30:
							goto IL_02bc;
						case 31:
							goto IL_02d1;
						case 34:
							goto IL_02eb;
						case 35:
							goto IL_0308;
						case 36:
							goto IL_031d;
						case 39:
							goto IL_0337;
						case 40:
							goto IL_0355;
						case 41:
							goto IL_036a;
						case 44:
							goto IL_0382;
						case 45:
							goto IL_0394;
						case 46:
							goto IL_03a1;
						case 51:
							goto IL_03be;
						case 52:
							goto IL_03e9;
						case 53:
							goto IL_03fb;
						case 56:
							goto IL_0415;
						case 57:
							goto IL_0440;
						case 58:
							goto IL_0452;
						case 61:
							goto IL_046c;
						case 62:
							goto IL_0497;
						case 63:
							goto IL_04a9;
						case 66:
							goto IL_04c0;
						case 67:
							goto IL_04eb;
						case 68:
							goto IL_04fd;
						case 33:
						case 38:
						case 43:
						case 48:
						case 49:
						case 50:
						case 55:
						case 60:
						case 65:
						case 70:
						case 71:
							goto IL_0512;
						case 72:
							goto IL_0526;
						case 73:
							goto IL_053a;
						case 75:
							goto IL_0548;
						case 74:
						case 76:
						case 77:
						case 78:
							goto IL_0555;
						case 79:
							goto IL_056b;
						case 80:
							goto IL_05a5;
						case 81:
							goto IL_05ba;
						case 83:
						case 84:
							goto IL_05d0;
						case 86:
							goto IL_0676;
						case 87:
							goto IL_068c;
						case 89:
							goto IL_06ab;
						case 90:
							goto IL_06b7;
						case 91:
							goto IL_06d0;
						case 93:
						case 94:
							goto IL_06f1;
						case 95:
							goto IL_070a;
						case 96:
							goto IL_0716;
						case 97:
							goto IL_0741;
						case 98:
							goto IL_0756;
						case 100:
						case 101:
						case 102:
							goto IL_076e;
						case 103:
							goto IL_07f6;
						case 104:
							goto IL_080b;
						case 106:
						case 107:
							goto IL_0821;
						case 109:
							goto IL_08b6;
						case 110:
							goto IL_08c9;
						case 112:
							goto IL_0914;
						case 113:
							goto IL_092a;
						case 114:
							goto IL_094f;
						case 115:
							goto IL_0964;
						case 117:
						case 118:
							goto IL_097a;
						case 119:
							goto IL_0986;
						case 120:
							goto IL_099f;
						case 121:
							goto IL_09a9;
						case 122:
							goto IL_09be;
						case 124:
							goto IL_0a3a;
						case 125:
							goto IL_0a50;
						case 126:
							goto IL_0ac9;
						case 127:
							goto IL_0ade;
						case 129:
						case 130:
							goto IL_0af4;
						case 132:
							goto IL_0b5f;
						case 133:
							goto IL_0b75;
						case 135:
							goto IL_0bc3;
						case 136:
							goto IL_0bf2;
						case 137:
							goto IL_0c1a;
						case 139:
							goto IL_0c2d;
						case 140:
							goto IL_0c9b;
						case 141:
							goto IL_0cb3;
						case 138:
						case 143:
						case 144:
							goto IL_0ccb;
						case 146:
							goto IL_0d27;
						case 147:
							goto IL_0d3d;
						case 149:
							goto IL_0dac;
						case 150:
							goto IL_0dc2;
						case 152:
							goto IL_0e22;
						case 153:
							goto IL_0e3b;
						case 154:
							goto IL_0e53;
						case 155:
							goto IL_0e6b;
						case 156:
							goto IL_0e87;
						case 157:
							goto IL_0e94;
						case 158:
							goto IL_0ee0;
						case 160:
						case 161:
							goto IL_0eff;
						case 162:
							goto IL_0f17;
						case 163:
							goto IL_0f2f;
						case 164:
							goto IL_0f7b;
						case 165:
							goto IL_0f93;
						case 168:
							goto IL_0fad;
						case 169:
							goto IL_0fd5;
						case 167:
						case 171:
						case 172:
							goto IL_0ff3;
						case 173:
							goto IL_1014;
						case 174:
							goto IL_1021;
						case 175:
							goto IL_1042;
						case 176:
							goto IL_104f;
						case 178:
							goto IL_1136;
						case 179:
							goto IL_114f;
						case 180:
							goto IL_115e;
						case 181:
							goto IL_118c;
						case 182:
							goto IL_11a4;
						case 184:
						case 185:
							goto IL_11bd;
						case 187:
							goto IL_1228;
						case 188:
							goto IL_1241;
						case 189:
							goto IL_1250;
						case 190:
							goto IL_127e;
						case 191:
							goto IL_1296;
						case 194:
							goto IL_12b3;
						case 195:
							goto IL_12db;
						case 196:
							goto IL_12f3;
						case 199:
							goto IL_130d;
						case 200:
							goto IL_133b;
						case 201:
							goto IL_1353;
						case 193:
						case 198:
						case 203:
						case 204:
							goto IL_136b;
						case 206:
							goto IL_1403;
						case 207:
							goto IL_1419;
						case 209:
							goto IL_145a;
						case 210:
							goto IL_1470;
						case 212:
							goto IL_14ce;
						case 213:
							goto IL_14e4;
						case 215:
							goto IL_1542;
						case 216:
							goto IL_1558;
						case 218:
							goto IL_15b6;
						case 219:
							goto IL_15cc;
						case 221:
							goto IL_161c;
						case 222:
							goto IL_1635;
						case 223:
							goto IL_1644;
						case 224:
							goto IL_1682;
						case 225:
							goto IL_169a;
						case 227:
						case 228:
							goto IL_16b3;
						case 230:
							goto IL_1703;
						case 231:
							goto IL_171c;
						case 232:
							goto IL_1736;
						case 233:
							goto IL_1745;
						case 234:
							goto IL_1754;
						case 235:
							goto IL_1763;
						case 236:
							goto IL_1790;
						case 237:
							goto IL_17a8;
						case 239:
						case 240:
							goto IL_17c1;
						case 241:
							goto IL_17cb;
						case 242:
							goto IL_17f8;
						case 243:
							goto IL_1810;
						case 245:
						case 246:
							goto IL_1829;
						case 247:
							goto IL_1866;
						case 248:
							goto IL_187e;
						case 250:
						case 251:
							goto IL_1897;
						case 252:
							goto IL_18af;
						case 253:
							goto IL_18c7;
						case 254:
							goto IL_18e3;
						case 256:
							goto IL_18f5;
						case 257:
							goto IL_1925;
						case 255:
						case 259:
						case 260:
							goto IL_1943;
						case 261:
							goto IL_195f;
						case 263:
							goto IL_1970;
						case 264:
							goto IL_199d;
						case 262:
						case 266:
						case 267:
							goto IL_19bb;
						case 269:
							goto IL_1a9e;
						case 270:
							goto IL_1ab4;
						case 272:
							goto IL_1b12;
						case 273:
							goto IL_1b2b;
						case 274:
							goto IL_1b4d;
						case 275:
							goto IL_1b8d;
						case 276:
							goto IL_1ba5;
						case 279:
							goto IL_1bc2;
						case 280:
							goto IL_1bfa;
						case 281:
							goto IL_1c12;
						case 284:
							goto IL_1c2c;
						case 285:
							goto IL_1c74;
						case 286:
							goto IL_1c8c;
						case 278:
						case 283:
						case 288:
						case 289:
							goto IL_1ca4;
						case 290:
							goto IL_1cc0;
						case 291:
							goto IL_1ccf;
						case 293:
							goto IL_1cea;
						case 294:
							goto IL_1d06;
						case 295:
							goto IL_1d15;
						case 296:
							goto IL_1d22;
						case 297:
							goto IL_1d3f;
						case 298:
							goto IL_1d5b;
						case 292:
						case 299:
						case 300:
							goto IL_1d69;
						case 301:
							goto IL_1e38;
						case 302:
							goto IL_1e57;
						case 303:
						case 304:
							goto IL_1e7a;
						case 305:
							goto IL_1e9b;
						case 307:
							goto IL_1ec2;
						case 308:
							goto IL_1edb;
						case 309:
							goto IL_1f03;
						case 310:
							goto IL_1f1b;
						case 312:
						case 313:
							goto IL_1f34;
						case 314:
							goto IL_1f5c;
						case 315:
							goto IL_1f74;
						case 317:
						case 318:
							goto IL_1f8d;
						case 319:
							goto IL_1fa5;
						case 320:
							goto IL_2048;
						case 321:
							goto IL_2064;
						case 324:
							goto IL_208d;
						case 325:
							goto IL_20a6;
						case 326:
							goto IL_20be;
						case 327:
							goto IL_20d6;
						case 329:
							goto IL_218e;
						case 330:
							goto IL_21a7;
						case 331:
							goto IL_21c8;
						case 332:
							goto IL_21d5;
						case 334:
							goto IL_227e;
						case 335:
							goto IL_22a3;
						case 337:
							goto IL_2301;
						case 338:
							goto IL_231a;
						case 340:
							goto IL_23a5;
						case 341:
							goto IL_23be;
						case 342:
							goto IL_23c8;
						case 343:
							goto IL_2405;
						case 345:
							goto IL_241c;
						case 346:
							goto IL_244e;
						case 347:
							goto IL_2466;
						case 344:
						case 349:
						case 350:
							goto IL_247e;
						case 352:
							goto IL_24eb;
						case 353:
							goto IL_2501;
						case 355:
							goto IL_2555;
						case 356:
							goto IL_256e;
						case 357:
							goto IL_257d;
						case 358:
							goto IL_2586;
						case 359:
							goto IL_258e;
						case 360:
							goto IL_25dd;
						case 362:
						case 363:
							goto IL_25ea;
						case 361:
						case 364:
							goto IL_25f8;
						case 365:
							goto IL_2609;
						case 366:
							goto IL_2621;
						case 368:
						case 369:
							goto IL_263a;
						case 371:
							goto IL_26b6;
						case 372:
							goto IL_26d8;
						case 373:
							goto IL_270b;
						case 374:
							goto IL_2723;
						case 376:
						case 377:
							goto IL_273c;
						case 379:
							goto IL_278c;
						case 380:
							goto IL_27a2;
						case 382:
							goto IL_2800;
						case 383:
							goto IL_2825;
						case 385:
							goto IL_2875;
						case 386:
							goto IL_288b;
						case 388:
							goto IL_28e9;
						case 389:
							goto IL_2902;
						case 390:
							goto IL_2962;
						case 391:
							goto IL_297a;
						case 393:
						case 394:
							goto IL_2993;
						case 396:
							goto IL_29f1;
						case 397:
							goto IL_2a19;
						case 398:
							goto IL_2a23;
						case 399:
							goto IL_2a3f;
						case 400:
							goto IL_2a5b;
						case 401:
							goto IL_2a68;
						case 402:
							goto IL_2a7e;
						case 403:
							goto IL_2a96;
						case 404:
						case 405:
							goto IL_2ab1;
						case 406:
							goto IL_2abe;
						case 408:
							goto IL_2b22;
						case 409:
							goto IL_2b3b;
						case 410:
							goto IL_2b72;
						case 411:
							goto IL_2b8a;
						case 413:
						case 414:
							goto IL_2ba3;
						case 416:
							goto IL_2bf3;
						case 417:
							goto IL_2c0c;
						case 418:
							goto IL_2c1b;
						case 419:
							goto IL_2c37;
						case 420:
							goto IL_2c44;
						case 422:
							goto IL_2cb1;
						case 423:
							goto IL_2cc7;
						case 425:
							goto IL_2d17;
						case 426:
							goto IL_2d30;
						case 427:
							goto IL_2d48;
						case 428:
							goto IL_2d61;
						case 429:
							goto IL_2d88;
						case 430:
							goto IL_2da0;
						case 432:
						case 433:
							goto IL_2db9;
						case 435:
							goto IL_2e44;
						case 436:
							goto IL_2e73;
						case 437:
							goto IL_2e89;
						case 438:
							goto IL_2ea1;
						case 440:
							goto IL_2ef2;
						case 443:
							goto IL_2f34;
						case 444:
							goto IL_2f4d;
						case 445:
							goto IL_2f75;
						case 446:
							goto IL_2f8d;
						case 448:
						case 449:
							goto IL_2fa6;
						case 450:
							goto IL_2fce;
						case 451:
							goto IL_2fe6;
						case 453:
						case 454:
							goto IL_2fff;
						case 455:
							goto IL_3030;
						case 456:
							goto IL_3048;
						case 458:
						case 459:
							goto IL_3061;
						case 460:
							goto IL_309d;
						case 461:
							goto IL_30b5;
						case 463:
						case 464:
							goto IL_30ce;
						case 465:
							goto IL_311a;
						case 466:
							goto IL_3132;
						case 468:
						case 469:
							goto IL_314b;
						case 470:
							goto IL_3163;
						case 472:
							goto IL_31e3;
						case 473:
							goto IL_31fa;
						case 475:
							goto IL_3267;
						case 476:
							goto IL_3283;
						case 477:
							goto IL_329b;
						case 480:
							goto IL_32b5;
						case 481:
							goto IL_32d1;
						case 482:
							goto IL_32e9;
						case 479:
						case 484:
						case 485:
							goto IL_3301;
						case 85:
						case 108:
						case 111:
						case 123:
						case 131:
						case 134:
						case 145:
						case 148:
						case 151:
						case 177:
						case 186:
						case 205:
						case 208:
						case 211:
						case 214:
						case 217:
						case 220:
						case 229:
						case 268:
						case 271:
						case 306:
						case 322:
						case 323:
						case 328:
						case 333:
						case 336:
						case 339:
						case 351:
						case 354:
						case 370:
						case 378:
						case 381:
						case 384:
						case 387:
						case 395:
						case 407:
						case 415:
						case 421:
						case 424:
						case 434:
						case 439:
						case 441:
						case 442:
						case 471:
						case 474:
						case 486:
						case 487:
						case 488:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 32:
						case 37:
						case 42:
						case 47:
						case 54:
						case 59:
						case 64:
						case 69:
						case 82:
						case 88:
						case 92:
						case 99:
						case 105:
						case 116:
						case 128:
						case 142:
						case 159:
						case 166:
						case 170:
						case 183:
						case 192:
						case 197:
						case 202:
						case 226:
						case 238:
						case 244:
						case 249:
						case 258:
						case 265:
						case 277:
						case 282:
						case 287:
						case 311:
						case 316:
						case 348:
						case 367:
						case 375:
						case 392:
						case 412:
						case 431:
						case 447:
						case 452:
						case 457:
						case 462:
						case 467:
						case 478:
						case 483:
						case 489:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_2bf3:
					num2 = 416;
					if (BuildForm.IsUtil("EXCEL-TO-CSV"))
					{
						goto IL_2c0c;
					}
					goto IL_2cb1;
					IL_2c0c:
					num2 = 417;
					text = Strings.UCase(text);
					goto IL_2c1b;
					IL_2ba3:
					num2 = 414;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_2c1b:
					num2 = 418;
					if (Operators.CompareString(text, "Y", TextCompare: false) != 0)
					{
						goto IL_2c37;
					}
					goto IL_2c44;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					flag = false;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					text3 = "N";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					text4 = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					text5 = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					text6 = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					text7 = "";
					goto IL_0041;
					IL_0041:
					num2 = 9;
					text8 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "err1", "", 1000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
					goto IL_0073;
					IL_0073:
					num2 = 10;
					text9 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "err2", "", 1000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
					goto IL_00a5;
					IL_00a5:
					num2 = 11;
					text10 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "err3", "", 1000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
					goto IL_00d7;
					IL_00d7:
					num2 = 12;
					text11 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "err5", "", 1000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
					goto IL_0109;
					IL_0109:
					num2 = 13;
					text2 = Strings.Trim(TxtCSV.Text);
					goto IL_011e;
					IL_011e:
					num2 = 14;
					text2 = Strings.Trim(Strings.Replace(text2, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_013c;
					IL_013c:
					num2 = 15;
					MyOpr = Strings.Trim(TxtOutXL.Text);
					goto IL_0151;
					IL_0151:
					num2 = 16;
					MyOpr = Strings.Trim(Strings.Replace(MyOpr, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_016f;
					IL_016f:
					num2 = 17;
					text12 = Strings.Trim(TxtInXL.Text);
					goto IL_0184;
					IL_0184:
					num2 = 18;
					text12 = Strings.Trim(Strings.Replace(text12, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_01a2;
					IL_01a2:
					num2 = 19;
					text = Strings.Trim(TxtXLSheet.Text);
					goto IL_01b7;
					IL_01b7:
					num2 = 20;
					text = Strings.Trim(Strings.Replace(text, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_01d5;
					IL_01d5:
					num2 = 21;
					text13 = Strings.Trim(TxtXLMacro.Text);
					goto IL_01ea;
					IL_01ea:
					num2 = 22;
					text13 = Strings.Trim(Strings.Replace(text13, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_0208;
					IL_0208:
					num2 = 23;
					MyOpr2 = Strings.Trim(TxtOther1.Text);
					goto IL_021d;
					IL_021d:
					num2 = 24;
					MyOpr2 = Strings.Trim(Strings.Replace(MyOpr2, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_023b;
					IL_023b:
					num2 = 25;
					text14 = Strings.Trim(TxtOther2.Text);
					goto IL_0250;
					IL_0250:
					num2 = 26;
					text14 = Strings.Trim(Strings.Replace(text14, "\"", "", 1, -1, CompareMethod.Text));
					goto IL_026e;
					IL_026e:
					num2 = 27;
					Globals_Renamed.currvaluetmp = "";
					goto IL_027b;
					IL_027b:
					num2 = 28;
					if (BuildForm.IsUtil("CREATE-QUERY-SHORTCUT"))
					{
						goto IL_0291;
					}
					goto IL_03be;
					IL_0291:
					num2 = 29;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0 || Operators.CompareString(MyOpr, "", TextCompare: false) == 0)
					{
						goto IL_02bc;
					}
					goto IL_02eb;
					IL_2c37:
					num2 = 419;
					text = "N";
					goto IL_2c44;
					IL_02eb:
					num2 = 34;
					if (!MyProject.Computer.FileSystem.FileExists(text2))
					{
						goto IL_0308;
					}
					goto IL_0337;
					IL_0308:
					num2 = 35;
					Interaction.MsgBox("Could not locate Query", MsgBoxStyle.Exclamation, "Missing Query");
					goto IL_031d;
					IL_031d:
					num2 = 36;
					TxtCSV.Focus();
					goto end_IL_0001_3;
					IL_0337:
					num2 = 39;
					if (!LikeOperator.LikeString(MyOpr.ToLower(), "*.lnk", CompareMethod.Binary))
					{
						goto IL_0355;
					}
					goto IL_0382;
					IL_0355:
					num2 = 40;
					Interaction.MsgBox("The Query Shortcut must have an extension of .lnk", MsgBoxStyle.Exclamation, "Invalid Extension");
					goto IL_036a;
					IL_036a:
					num2 = 41;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_0382:
					num2 = 44;
					num5 = Process_Shortcuts(text2, MyOpr, text12, text);
					goto IL_0394;
					IL_0394:
					num2 = 45;
					if (num5 != 0)
					{
						goto IL_03a1;
					}
					goto IL_0512;
					IL_03a1:
					num2 = 46;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_02bc:
					num2 = 30;
					Interaction.MsgBox("You must specify a Shortcut and a Query Path", MsgBoxStyle.Exclamation, "Missing Info");
					goto IL_02d1;
					IL_02d1:
					num2 = 31;
					TxtCSV.Focus();
					goto end_IL_0001_3;
					IL_03be:
					num2 = 51;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0 && Operators.CompareString(text8, "", TextCompare: false) != 0)
					{
						goto IL_03e9;
					}
					goto IL_0415;
					IL_2c44:
					num2 = 420;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"0\" \"0\" \"N\" \"" + text + "\"";
					break;
					IL_03e9:
					num2 = 52;
					Interaction.MsgBox(text8, MsgBoxStyle.Exclamation, "Misisng Item");
					goto IL_03fb;
					IL_03fb:
					num2 = 53;
					TxtCSV.Focus();
					goto end_IL_0001_3;
					IL_0415:
					num2 = 56;
					if (Operators.CompareString(MyOpr, "", TextCompare: false) == 0 && Operators.CompareString(text9, "", TextCompare: false) != 0)
					{
						goto IL_0440;
					}
					goto IL_046c;
					IL_2cb1:
					num2 = 422;
					if (BuildForm.IsUtil("WAIT-FILE"))
					{
						goto IL_2cc7;
					}
					goto IL_2d17;
					IL_0440:
					num2 = 57;
					Interaction.MsgBox(text9, MsgBoxStyle.Exclamation, "Missing Item");
					goto IL_0452;
					IL_0452:
					num2 = 58;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_046c:
					num2 = 61;
					if (Operators.CompareString(text12, "", TextCompare: false) == 0 && Operators.CompareString(text10, "", TextCompare: false) != 0)
					{
						goto IL_0497;
					}
					goto IL_04c0;
					IL_2cc7:
					num2 = 423;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_0497:
					num2 = 62;
					Interaction.MsgBox(text10, MsgBoxStyle.Exclamation, "Missing Item");
					goto IL_04a9;
					IL_04a9:
					num2 = 63;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_04c0:
					num2 = 66;
					if (Operators.CompareString(text13, "", TextCompare: false) == 0 && Operators.CompareString(text11, "", TextCompare: false) != 0)
					{
						goto IL_04eb;
					}
					goto IL_0512;
					IL_2d17:
					num2 = 425;
					if (BuildForm.IsUtil("WEB-COPY"))
					{
						goto IL_2d30;
					}
					goto IL_2e44;
					IL_04eb:
					num2 = 67;
					Interaction.MsgBox(text11, MsgBoxStyle.Exclamation, "Missing Item");
					goto IL_04fd;
					IL_04fd:
					num2 = 68;
					TxtXLMacro.Focus();
					goto end_IL_0001_3;
					IL_0512:
					num2 = 71;
					if (chkOption.Visible)
					{
						goto IL_0526;
					}
					goto IL_0555;
					IL_0526:
					num2 = 72;
					if (chkOption.Checked)
					{
						goto IL_053a;
					}
					goto IL_0548;
					IL_053a:
					num2 = 73;
					text3 = "Y";
					goto IL_0555;
					IL_0548:
					num2 = 75;
					text3 = "N";
					goto IL_0555;
					IL_0555:
					num2 = 78;
					if (BuildForm.IsUtil("EMAIL"))
					{
						goto IL_056b;
					}
					goto IL_0676;
					IL_056b:
					num2 = 79;
					if (Operators.CompareString(MyOpr, "", TextCompare: false) == 0 && Operators.CompareString(MyOpr2, "", TextCompare: false) == 0 && Operators.CompareString(text13, "", TextCompare: false) == 0)
					{
						goto IL_05a5;
					}
					goto IL_05d0;
					IL_2d30:
					num2 = 426;
					text5 = Strings.Trim(CmbOpt.Text);
					goto IL_2d48;
					IL_2d48:
					num2 = 427;
					text5 = " \"" + text5 + "\" ";
					goto IL_2d61;
					IL_05a5:
					num2 = 80;
					Interaction.MsgBox("Specify a TO, CC, or BCC email distribution list", MsgBoxStyle.Exclamation, "Missing Data");
					goto IL_05ba;
					IL_05ba:
					num2 = 81;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_05d0:
					num2 = 84;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text + "\" \"" + text12 + "\" \"" + text13 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\" \"" + text3 + "\" \"<<<spf-email-type>>>\"";
					break;
					IL_0676:
					num2 = 86;
					if (BuildForm.IsUtil("IF-THEN"))
					{
						goto IL_068c;
					}
					goto IL_08b6;
					IL_068c:
					num2 = 87;
					if (!Chk_Opr(ref MyOpr, TxtOutXL))
					{
						goto end_IL_0001_3;
					}
					goto IL_06ab;
					IL_06ab:
					num2 = 89;
					MyOpr = Strings.UCase(MyOpr);
					goto IL_06b7;
					IL_06b7:
					num2 = 90;
					if (Operators.CompareString(MyOpr2, "", TextCompare: false) != 0)
					{
						goto IL_06d0;
					}
					goto IL_06f1;
					IL_06d0:
					num2 = 91;
					if (!Chk_Opr(ref MyOpr2, TxtOther1))
					{
						goto end_IL_0001_3;
					}
					goto IL_06f1;
					IL_06f1:
					num2 = 94;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_070a;
					}
					goto IL_076e;
					IL_070a:
					num2 = 95;
					text = Strings.UCase(text);
					goto IL_0716;
					IL_0716:
					num2 = 96;
					if (Operators.CompareString(text, "AND", TextCompare: false) != 0 && Operators.CompareString(text, "OR", TextCompare: false) != 0)
					{
						goto IL_0741;
					}
					goto IL_076e;
					IL_2d61:
					num2 = 428;
					if (!Versioned.IsNumeric(text) | (Strings.InStr(text, ".") != 0))
					{
						goto IL_2d88;
					}
					goto IL_2db9;
					IL_0741:
					num2 = 97;
					Interaction.MsgBox("Expecting either an AND or an OR in field AND/OR", MsgBoxStyle.Exclamation, "AND/OR Expected");
					goto IL_0756;
					IL_0756:
					num2 = 98;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_076e:
					num2 = 102;
					if ((Operators.CompareString(text, "", TextCompare: false) != 0 || Operators.CompareString(text13, "", TextCompare: false) != 0 || Operators.CompareString(MyOpr2, "", TextCompare: false) != 0 || Operators.CompareString(text14, "", TextCompare: false) != 0) && (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text13, "", TextCompare: false) == 0 || Operators.CompareString(MyOpr2, "", TextCompare: false) == 0 || Operators.CompareString(text14, "", TextCompare: false) == 0))
					{
						goto IL_07f6;
					}
					goto IL_0821;
					IL_2da0:
					num2 = 430;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_2db9:
					num2 = 433;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text3 + "\"" + text5 + "\"" + text + "\"";
					break;
					IL_2e44:
					num2 = 435;
					if (BuildForm.IsUtil("UPDATE-TIME") | BuildForm.IsUtil("GET-SITE-TIME") | BuildForm.IsUtil("SITE-LOOP"))
					{
						goto IL_2e73;
					}
					goto IL_2f34;
					IL_2d88:
					num2 = 429;
					Interaction.MsgBox("The ReadTimeOut value must be an Integer (e.g., 120).", MsgBoxStyle.Exclamation, "Invalid TimeOut");
					goto IL_2da0;
					IL_2e73:
					num2 = 436;
					if (BuildForm.IsUtil("SITE-LOOP"))
					{
						goto IL_2e89;
					}
					goto IL_2ef2;
					IL_2e89:
					num2 = 437;
					num6 = Conversions.ToInteger(ParallelThreadsComboBox1.SelectedItem);
					goto IL_2ea1;
					IL_2ea1:
					num2 = 438;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + Conversions.ToString(num6) + "\"";
					break;
					IL_0821:
					num2 = 107;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\"";
					break;
					IL_07f6:
					num2 = 103;
					Interaction.MsgBox("You entered a partial second If-Then condition. Either specify no second condition or enter a complete second condition", MsgBoxStyle.Exclamation, "Partial 2nd Condition");
					goto IL_080b;
					IL_080b:
					num2 = 104;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_08b6:
					num2 = 109;
					if (BuildForm.IsUtil("START-MACRO"))
					{
						goto IL_08c9;
					}
					goto IL_0914;
					IL_08c9:
					num2 = 110;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + text3 + "\"";
					break;
					IL_0914:
					num2 = 112;
					if (BuildForm.IsUtil("RUN-LOOP"))
					{
						goto IL_092a;
					}
					goto IL_0a3a;
					IL_092a:
					num2 = 113;
					if (!LikeOperator.LikeString(text12, "*<<<*>>>*", CompareMethod.Binary) && !Versioned.IsNumeric(text12))
					{
						goto IL_094f;
					}
					goto IL_097a;
					IL_2ef2:
					num2 = 440;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\"";
					break;
					IL_094f:
					num2 = 114;
					Interaction.MsgBox("You must specify the number of rows in the input file to process at a time", MsgBoxStyle.Exclamation, "Missing Rows");
					goto IL_0964;
					IL_0964:
					num2 = 115;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_097a:
					num2 = 118;
					text = Strings.UCase(text);
					goto IL_0986;
					IL_0986:
					num2 = 119;
					if (Operators.CompareString(text, "Y", TextCompare: false) != 0)
					{
						goto IL_099f;
					}
					goto IL_09a9;
					IL_099f:
					num2 = 120;
					text = "N";
					goto IL_09a9;
					IL_09a9:
					num2 = 121;
					num6 = Conversions.ToInteger(ParallelThreadsComboBox1.SelectedItem);
					goto IL_09be;
					IL_09be:
					num2 = 122;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + Conversions.ToString(num6) + "\"";
					break;
					IL_0a3a:
					num2 = 124;
					if (BuildForm.IsUtil("ROWS-IN-FILE"))
					{
						goto IL_0a50;
					}
					goto IL_0b5f;
					IL_0a50:
					num2 = 125;
					if (LikeOperator.LikeString(Strings.LCase(text2), "*.zip", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.LCase(text12), "*.csv", CompareMethod.Binary) && !Conversions.ToBoolean(Strings.LCase(Conversions.ToString(LikeOperator.LikeString(text12, "*.tab", CompareMethod.Binary)))) && !LikeOperator.LikeString(Strings.LCase(text12), "*.parquet", CompareMethod.Binary) && !LikeOperator.LikeString(text12, "*<<<*>>>*", CompareMethod.Binary))
					{
						goto IL_0ac9;
					}
					goto IL_0af4;
					IL_2f34:
					num2 = 443;
					if (BuildForm.IsUtil("FOR-LOOP"))
					{
						goto IL_2f4d;
					}
					goto IL_31e3;
					IL_2f4d:
					num2 = 444;
					if (!LikeOperator.LikeString(text2, "*<<<*>>>*", CompareMethod.Binary) && !General_Procedures.IsPosInteger(text2))
					{
						goto IL_2f75;
					}
					goto IL_2fa6;
					IL_2a3f:
					num2 = 399;
					text4 = Strings.Replace(text4, "\"", "'", 1, -1, CompareMethod.Text);
					goto IL_2a5b;
					IL_2f75:
					num2 = 445;
					Interaction.MsgBox("Specify a positive integer for start value.", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_2f8d;
					IL_0ac9:
					num2 = 126;
					Interaction.MsgBox("You must specify a valid file (.csv, .tab, .parquet) in the zip to check if the Input file has a .zip extesnsion.", MsgBoxStyle.Exclamation, "Missing File");
					goto IL_0ade;
					IL_0ade:
					num2 = 127;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_0af4:
					num2 = 130;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text3 + "\" \"" + text12 + "\"";
					break;
					IL_0b5f:
					num2 = 132;
					if (BuildForm.IsUtil("DATE-OF-FILE"))
					{
						goto IL_0b75;
					}
					goto IL_0bc3;
					IL_0b75:
					num2 = 133;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_0bc3:
					num2 = 135;
					if (BuildForm.IsUtil("VALUE-IN-FILE") | BuildForm.IsUtil("AGE-OF-FILE") | BuildForm.IsUtil("VERIFY-ROLE"))
					{
						goto IL_0bf2;
					}
					goto IL_0d27;
					IL_0bf2:
					num2 = 136;
					if (Operators.CompareString(text12, "", TextCompare: false) == 0 && BuildForm.IsUtil("AGE-OF-FILE"))
					{
						goto IL_0c1a;
					}
					goto IL_0c2d;
					IL_2f8d:
					num2 = 446;
					TxtCSV.Focus();
					goto end_IL_0001_3;
					IL_0c1a:
					num2 = 137;
					text12 = "HOURS";
					goto IL_0ccb;
					IL_0c2d:
					num2 = 139;
					if (BuildForm.IsUtil("AGE-OF-FILE") & (Operators.CompareString(Strings.UCase(text12), "HOURS", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text12), "DAYS", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text12), "MINUTES", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text12), "SECONDS", TextCompare: false) != 0))
					{
						goto IL_0c9b;
					}
					goto IL_0ccb;
					IL_0c9b:
					num2 = 140;
					Interaction.MsgBox("You must specify a valid time unit for the age computation. Valid values are Days, Hours, Minutes, or Seconds", MsgBoxStyle.Exclamation, "Invalid Time Units");
					goto IL_0cb3;
					IL_0cb3:
					num2 = 141;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_0ccb:
					num2 = 144;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_0d27:
					num2 = 146;
					if (BuildForm.IsUtil("IDEAL-TABLE-CONFIG"))
					{
						goto IL_0d3d;
					}
					goto IL_0dac;
					IL_0d3d:
					num2 = 147;
					Globals_Renamed.currvaluetmp = "{PYSCRIPT:" + Globals_Renamed.currhelptmp + "} \"" + fVaFileName + "\" \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_0dac:
					num2 = 149;
					if (BuildForm.IsUtil("CONVERT-IMBIGDATA-FUNCTIONS"))
					{
						goto IL_0dc2;
					}
					goto IL_0e22;
					IL_0dc2:
					num2 = 150;
					Globals_Renamed.currvaluetmp = "{PYSCRIPT:" + Globals_Renamed.currhelptmp + "} \"" + fVaFileName + "\" \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_0e22:
					num2 = 152;
					if (BuildForm.IsUtil("AUTO-COMMONALITY"))
					{
						goto IL_0e3b;
					}
					goto IL_1136;
					IL_0e3b:
					num2 = 153;
					text4 = Strings.Trim(mnuopt1.Text);
					goto IL_0e53;
					IL_0e53:
					num2 = 154;
					text5 = Strings.Trim(mnuOpt2.Text);
					goto IL_0e6b;
					IL_0e6b:
					num2 = 155;
					if (Operators.CompareString(text5, "", TextCompare: false) == 0)
					{
						goto IL_0e87;
					}
					goto IL_0e94;
					IL_0e87:
					num2 = 156;
					text5 = "Feature_Selection";
					goto IL_0e94;
					IL_0e94:
					num2 = 157;
					switch (text5)
					{
					case "Ideal_Forest":
					case "Feature_Selection":
					case "Decision_Tree_1_Surrogate":
						goto IL_0eff;
					}
					if (Operators.CompareString(text5, "LEMS-RCA", TextCompare: false) != 0)
					{
						goto IL_0ee0;
					}
					goto IL_0eff;
					IL_2fa6:
					num2 = 449;
					if (!LikeOperator.LikeString(MyOpr, "*<<<*>>>*", CompareMethod.Binary) && !General_Procedures.IsPosInteger(MyOpr))
					{
						goto IL_2fce;
					}
					goto IL_2fff;
					IL_0ee0:
					num2 = 158;
					Interaction.MsgBox("The Options-> Algorithm setting must either equal Ideal_Forest, Feature_Selection, Decision_Tree_1_Surrogate or LEMS-RCA", MsgBoxStyle.Exclamation, "Invalid Algorithm");
					goto end_IL_0001_3;
					IL_0eff:
					num2 = 161;
					text6 = Strings.Trim(mnuopt3.Text);
					goto IL_0f17;
					IL_0f17:
					num2 = 162;
					text7 = Strings.Trim(mnuopt4.Text);
					goto IL_0f2f;
					IL_0f2f:
					num2 = 163;
					if (Operators.CompareString(Strings.UCase(text13), "CATTS", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text13), "KITCHENSINK", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text13), "OTHER", TextCompare: false) != 0)
					{
						goto IL_0f7b;
					}
					goto IL_0fad;
					IL_2a5b:
					num2 = 400;
					text5 = "";
					goto IL_2a68;
					IL_2fce:
					num2 = 450;
					Interaction.MsgBox("Specify a positive integer for End value.", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_2fe6;
					IL_0f7b:
					num2 = 164;
					Interaction.MsgBox("Set Data Type To one Of CATTS, KitchenSink Or Other", MsgBoxStyle.Exclamation, "Invalid Type");
					goto IL_0f93;
					IL_0f93:
					num2 = 165;
					TxtXLMacro.Focus();
					goto end_IL_0001_3;
					IL_0fad:
					num2 = 168;
					if (Operators.CompareString(text4, "", TextCompare: false) == 0 || !General_Procedures.IsPosInteger(text4))
					{
						goto IL_0fd5;
					}
					goto IL_0ff3;
					IL_2fe6:
					num2 = 451;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_0ff3:
					num2 = 172;
					if (Operators.CompareString(Strings.Trim(MyOpr2), "", TextCompare: false) == 0)
					{
						goto IL_1014;
					}
					goto IL_1021;
					IL_1014:
					num2 = 173;
					MyOpr2 = "None";
					goto IL_1021;
					IL_1021:
					num2 = 174;
					if (Operators.CompareString(Strings.Trim(text14), "", TextCompare: false) == 0)
					{
						goto IL_1042;
					}
					goto IL_104f;
					IL_1042:
					num2 = 175;
					MyOpr2 = "None";
					goto IL_104f;
					IL_104f:
					num2 = 176;
					Globals_Renamed.currvaluetmp = "{PYSCRIPT:" + Globals_Renamed.currhelptmp + "} \"" + fVaFileName + "\" \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"  \"" + text + "\" \"" + text13 + "\" \"" + text4 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\"  \"" + text5 + "\" \"" + text6 + "\" \"" + text7 + "\" \"<<<spf-instance>>>\"";
					break;
					IL_0fd5:
					num2 = 169;
					Interaction.MsgBox("Minimum Variable Importance % (In Options menu) must be a positive Integer", MsgBoxStyle.Exclamation, "Invalid Count");
					goto end_IL_0001_3;
					IL_1136:
					num2 = 178;
					if (BuildForm.IsUtil("GET-FILES"))
					{
						goto IL_114f;
					}
					goto IL_1228;
					IL_114f:
					num2 = 179;
					MyOpr = Strings.UCase(MyOpr);
					goto IL_115e;
					IL_115e:
					num2 = 180;
					if (Operators.CompareString(MyOpr, "Y", TextCompare: false) != 0 && Operators.CompareString(MyOpr, "N", TextCompare: false) != 0)
					{
						goto IL_118c;
					}
					goto IL_11bd;
					IL_2fff:
					num2 = 454;
					if (Versioned.IsNumeric(text2) && Versioned.IsNumeric(MyOpr) && Conversions.ToLong(MyOpr) < Conversions.ToLong(text2))
					{
						goto IL_3030;
					}
					goto IL_3061;
					IL_118c:
					num2 = 181;
					Interaction.MsgBox("Specify Y Or N To search subfolders", MsgBoxStyle.Exclamation, "Invalid Value");
					goto IL_11a4;
					IL_11a4:
					num2 = 182;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_11bd:
					num2 = 185;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text3 + "\"";
					break;
					IL_1228:
					num2 = 187;
					if (BuildForm.IsUtil("FILE-COMPARE"))
					{
						goto IL_1241;
					}
					goto IL_1403;
					IL_1241:
					num2 = 188;
					text12 = Strings.UCase(text12);
					goto IL_1250;
					IL_1250:
					num2 = 189;
					if (Operators.CompareString(text12, "Y", TextCompare: false) != 0 && Operators.CompareString(text12, "N", TextCompare: false) != 0)
					{
						goto IL_127e;
					}
					goto IL_12b3;
					IL_2a68:
					num2 = 401;
					if (BuildForm.IsUtil("PYTHON"))
					{
						goto IL_2a7e;
					}
					goto IL_2ab1;
					IL_127e:
					num2 = 190;
					Interaction.MsgBox("Specify a Y Or N For a Case Insensitive search", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_1296;
					IL_1296:
					num2 = 191;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_12b3:
					num2 = 194;
					if (Operators.CompareString(text, "", TextCompare: false) != 0 && !Versioned.IsNumeric(text))
					{
						goto IL_12db;
					}
					goto IL_130d;
					IL_2a7e:
					num2 = 402;
					text5 = Strings.Trim(CmbOpt.Text);
					goto IL_2a96;
					IL_12db:
					num2 = 195;
					Interaction.MsgBox("The Max differences must be empty Or numeric", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_12f3;
					IL_12f3:
					num2 = 196;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_130d:
					num2 = 199;
					if (Operators.CompareString(MyOpr2, "Y", TextCompare: false) != 0 && Operators.CompareString(MyOpr2, "N", TextCompare: false) != 0)
					{
						goto IL_133b;
					}
					goto IL_136b;
					IL_3030:
					num2 = 455;
					Interaction.MsgBox("Start value must be <= End Value", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_3048;
					IL_133b:
					num2 = 200;
					Interaction.MsgBox("Specify Y Or N For Notification", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_1353;
					IL_1353:
					num2 = 201;
					TxtOther1.Focus();
					goto end_IL_0001_3;
					IL_136b:
					num2 = 204;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text3 + "\" \"" + text + "\" \"" + text13 + "\" \"" + MyOpr2 + "\"";
					break;
					IL_1403:
					num2 = 206;
					if (BuildForm.IsUtil("SHAREPOINT-DELETE"))
					{
						goto IL_1419;
					}
					goto IL_145a;
					IL_1419:
					num2 = 207;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\"";
					break;
					IL_145a:
					num2 = 209;
					if (BuildForm.IsUtil("COPY"))
					{
						goto IL_1470;
					}
					goto IL_14ce;
					IL_1470:
					num2 = 210;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text3 + "\"";
					break;
					IL_14ce:
					num2 = 212;
					if (BuildForm.IsUtil("ZIP"))
					{
						goto IL_14e4;
					}
					goto IL_1542;
					IL_14e4:
					num2 = 213;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_1542:
					num2 = 215;
					if (BuildForm.IsUtil("UNZIP"))
					{
						goto IL_1558;
					}
					goto IL_15b6;
					IL_1558:
					num2 = 216;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_15b6:
					num2 = 218;
					if (BuildForm.IsUtil("RENAME"))
					{
						goto IL_15cc;
					}
					goto IL_161c;
					IL_15cc:
					num2 = 219;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_161c:
					num2 = 221;
					if (BuildForm.IsUtil("DELETE"))
					{
						goto IL_1635;
					}
					goto IL_1703;
					IL_1635:
					num2 = 222;
					MyOpr = Strings.UCase(MyOpr);
					goto IL_1644;
					IL_1644:
					num2 = 223;
					if ((Operators.CompareString(MyOpr, "", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "N", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "Y", TextCompare: false) != 0))
					{
						goto IL_1682;
					}
					goto IL_16b3;
					IL_1682:
					num2 = 224;
					Interaction.MsgBox("Specify Y To Not prompt For deletion, If * .* Is specified. Otherwise specify N Or leave blank", MsgBoxStyle.Exclamation, "Incorrect Delete Option");
					goto IL_169a;
					IL_169a:
					num2 = 225;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_16b3:
					num2 = 228;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_1703:
					num2 = 230;
					if (BuildForm.IsUtil("BEGIN-HPC"))
					{
						goto IL_171c;
					}
					goto IL_1a9e;
					IL_171c:
					num2 = 231;
					text4 = Strings.Mid(CmbOpt.Text, 9);
					goto IL_1736;
					IL_1736:
					num2 = 232;
					MyOpr2 = Strings.UCase(MyOpr2);
					goto IL_1745;
					IL_1745:
					num2 = 233;
					text13 = Strings.UCase(text13);
					goto IL_1754;
					IL_1754:
					num2 = 234;
					text14 = Strings.UCase(text14);
					goto IL_1763;
					IL_1763:
					num2 = 235;
					if ((Operators.CompareString(text13, "N", TextCompare: false) != 0) & (Operators.CompareString(text13, "Y", TextCompare: false) != 0))
					{
						goto IL_1790;
					}
					goto IL_17c1;
					IL_1790:
					num2 = 236;
					Interaction.MsgBox("Specify Y to display HPC output or N otherwise", MsgBoxStyle.Exclamation, "Invalid Option");
					goto IL_17a8;
					IL_17a8:
					num2 = 237;
					TxtXLMacro.Focus();
					goto end_IL_0001_3;
					IL_17c1:
					num2 = 240;
					text7 = text13;
					goto IL_17cb;
					IL_17cb:
					num2 = 241;
					if ((Operators.CompareString(MyOpr2, "N", TextCompare: false) != 0) & (Operators.CompareString(MyOpr2, "Y", TextCompare: false) != 0))
					{
						goto IL_17f8;
					}
					goto IL_1829;
					IL_17f8:
					num2 = 242;
					Interaction.MsgBox("Specify Y to copy logs local or N otherwise", MsgBoxStyle.Exclamation, "Invalid Option");
					goto IL_1810;
					IL_1810:
					num2 = 243;
					TxtOther1.Focus();
					goto end_IL_0001_3;
					IL_1829:
					num2 = 246;
					if (Operators.CompareString(text14, "HTTP", TextCompare: false) != 0 && Operators.CompareString(text14, "ROBOCOPY-SHARE", TextCompare: false) != 0 && Operators.CompareString(text14, "ROBOCOPY-DIRECT", TextCompare: false) != 0)
					{
						goto IL_1866;
					}
					goto IL_1897;
					IL_3048:
					num2 = 456;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_3061:
					num2 = 459;
					if (!LikeOperator.LikeString(text12, "*<<<*>>>*", CompareMethod.Binary) && (!Versioned.IsNumeric(text12) || !(Conversion.Val(text12) >= 0.0)))
					{
						goto IL_309d;
					}
					goto IL_30ce;
					IL_1866:
					num2 = 247;
					Interaction.MsgBox("Invalid setting specified for how data should be returned to the client", MsgBoxStyle.Exclamation, "Invalid Option");
					goto IL_187e;
					IL_187e:
					num2 = 248;
					TxtOther2.Focus();
					goto end_IL_0001_3;
					IL_1897:
					num2 = 251;
					text5 = Strings.Trim(mnuopt1.Text);
					goto IL_18af;
					IL_18af:
					num2 = 252;
					text6 = Strings.Trim(mnuOpt2.Text);
					goto IL_18c7;
					IL_18c7:
					num2 = 253;
					if (Operators.CompareString(text5, "", TextCompare: false) == 0)
					{
						goto IL_18e3;
					}
					goto IL_18f5;
					IL_18e3:
					num2 = 254;
					text5 = Conversions.ToString(32);
					goto IL_1943;
					IL_18f5:
					num2 = 256;
					if (!General_Procedures.IsPosInteger(text5) || Conversions.ToInteger(text5) > 128 || Conversions.ToInteger(text5) < 1)
					{
						goto IL_1925;
					}
					goto IL_1943;
					IL_2a96:
					num2 = 403;
					text5 = " \"" + text5 + "\"";
					goto IL_2ab1;
					IL_2ab1:
					num2 = 405;
					text3 = "N";
					goto IL_2abe;
					IL_1943:
					num2 = 260;
					if (Operators.CompareString(text6, "", TextCompare: false) == 0)
					{
						goto IL_195f;
					}
					goto IL_1970;
					IL_195f:
					num2 = 261;
					text6 = Conversions.ToString(8);
					goto IL_19bb;
					IL_1970:
					num2 = 263;
					if (!General_Procedures.IsPosInteger(text6) || Conversions.ToInteger(text6) > 48 || Conversions.ToInteger(text6) < 1)
					{
						goto IL_199d;
					}
					goto IL_19bb;
					IL_309d:
					num2 = 460;
					Interaction.MsgBox("Specify a positive integer for Step value.", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_30b5;
					IL_30b5:
					num2 = 461;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_19bb:
					num2 = 267;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + MyOpr2 + "\" \"" + text3 + "\" \"" + text4 + "\" \"" + text5 + "\" \"" + text6 + "\" \"" + text14 + "\" \"" + text7 + "\"";
					break;
					IL_199d:
					num2 = 264;
					Interaction.MsgBox("Timeout must be a positive integer and between 1 and 48 hours", MsgBoxStyle.Exclamation, "Invalid Timeout");
					goto end_IL_0001_3;
					IL_1925:
					num2 = 257;
					Interaction.MsgBox("Peak Memory must be a positive integer and between 1 and 128 GB", MsgBoxStyle.Exclamation, "Invalid memory");
					goto end_IL_0001_3;
					IL_1a9e:
					num2 = 269;
					if (BuildForm.IsUtil("APPEND"))
					{
						goto IL_1ab4;
					}
					goto IL_1b12;
					IL_1ab4:
					num2 = 270;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_1b12:
					num2 = 272;
					if (BuildForm.IsUtil("SMART-APPEND"))
					{
						goto IL_1b2b;
					}
					goto IL_1ec2;
					IL_1b2b:
					num2 = 273;
					text5 = CmbOpt.Text.Replace(" ", "");
					goto IL_1b4d;
					IL_1b4d:
					num2 = 274;
					if (Operators.CompareString(text5, "Version3", TextCompare: false) >= 0 && Operators.CompareString(Strings.Mid(MyOpr + "  ", 1, 2), "\\\\", TextCompare: false) == 0)
					{
						goto IL_1b8d;
					}
					goto IL_1bc2;
					IL_30ce:
					num2 = 464;
					if (!LikeOperator.LikeString(text, "*<<<*>>>*", CompareMethod.Binary) && Operators.CompareString(text, "1", TextCompare: false) != 0 && Operators.CompareString(text, "2", TextCompare: false) != 0 && Operators.CompareString(text, "3", TextCompare: false) != 0)
					{
						goto IL_311a;
					}
					goto IL_314b;
					IL_1b8d:
					num2 = 275;
					Interaction.MsgBox("Do Not process the New File from a Remote share As this may Not perform. The New file should be local.", MsgBoxStyle.Exclamation, "New File Local");
					goto IL_1ba5;
					IL_1ba5:
					num2 = 276;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_1bc2:
					num2 = 279;
					if (Operators.CompareString(text5, "Version3", TextCompare: false) < 0 && !LikeOperator.LikeString(MyOpr2, "*<<<*>>>*", CompareMethod.Binary) && !Versioned.IsNumeric(MyOpr2))
					{
						goto IL_1bfa;
					}
					goto IL_1c2c;
					IL_2abe:
					num2 = 406;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + text4 + "\" \"" + text3 + "\" \"<<<SPF-APP-SERVER>>>\"" + text5;
					break;
					IL_2825:
					num2 = 383;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\"";
					break;
					IL_1bfa:
					num2 = 280;
					Interaction.MsgBox("Specify the number Of rows To process at a time", MsgBoxStyle.Exclamation, "Invalid Rows");
					goto IL_1c12;
					IL_1c12:
					num2 = 281;
					TxtOther1.Focus();
					goto end_IL_0001_3;
					IL_1c2c:
					num2 = 284;
					if (Operators.CompareString(text5, "Version3", TextCompare: false) >= 0 && Operators.CompareString(Strings.UCase(MyOpr2), "Y", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(MyOpr2), "N", TextCompare: false) != 0)
					{
						goto IL_1c74;
					}
					goto IL_1ca4;
					IL_22a3:
					num2 = 335;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_311a:
					num2 = 465;
					Interaction.MsgBox("Specify a Loop Suffix equal to 1, 2, or 3", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_3132;
					IL_1c74:
					num2 = 285;
					Interaction.MsgBox("Enter Y Or N To Abort Job If Old file does Not exist", MsgBoxStyle.Exclamation, "Invalid Option");
					goto IL_1c8c;
					IL_1c8c:
					num2 = 286;
					TxtOther1.Focus();
					goto end_IL_0001_3;
					IL_1ca4:
					num2 = 289;
					if (Operators.CompareString(text5, "Version3", TextCompare: false) == 0)
					{
						goto IL_1cc0;
					}
					goto IL_1cea;
					IL_1cc0:
					num2 = 290;
					MyOpr2 = Strings.UCase(MyOpr2);
					goto IL_1ccf;
					IL_1ccf:
					num2 = 291;
					text4 = Strings.Trim(mnuopt1.Text);
					goto IL_1d69;
					IL_1cea:
					num2 = 293;
					if (Operators.CompareString(text5, "Version4", TextCompare: false) == 0)
					{
						goto IL_1d06;
					}
					goto IL_1d69;
					IL_1d06:
					num2 = 294;
					MyOpr2 = Strings.UCase(MyOpr2);
					goto IL_1d15;
					IL_1d15:
					num2 = 295;
					text4 = "";
					goto IL_1d22;
					IL_1d22:
					num2 = 296;
					text6 = Strings.UCase(Strings.Trim(mnuOpt2.Text));
					goto IL_1d3f;
					IL_1d3f:
					num2 = 297;
					if (Operators.CompareString(text6, "Y", TextCompare: false) != 0)
					{
						goto IL_1d5b;
					}
					goto IL_1d69;
					IL_1d5b:
					num2 = 298;
					text6 = "N";
					goto IL_1d69;
					IL_1d69:
					num2 = 300;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + text3 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\" \"" + Strings.Trim(CmbOpt.Text) + "\" \"<<<spf-hadoop-server>>>\" \"" + text4 + "\"";
					goto IL_1e38;
					IL_1e38:
					num2 = 301;
					if (Operators.CompareString(text5, "Version4", TextCompare: false) >= 0)
					{
						goto IL_1e57;
					}
					goto IL_1e7a;
					IL_1e57:
					num2 = 302;
					Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + " \"\" \"" + text6 + "\"";
					goto IL_1e7a;
					IL_1e7a:
					num2 = 304;
					text15 = (OrderedParallelMenuItem.Checked ? "Y" : "N");
					goto IL_1e9b;
					IL_314b:
					num2 = 469;
					num6 = Conversions.ToInteger(ParallelThreadsComboBox1.SelectedItem);
					goto IL_3163;
					IL_3163:
					num2 = 470;
					Globals_Renamed.currvaluetmp = "{" + Globals_Renamed.currhelptmp + "} \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + Conversions.ToString(num6) + "\"";
					break;
					IL_3132:
					num2 = 466;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_1e9b:
					num2 = 305;
					Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + " \"\" \"" + text15 + "\"";
					break;
					IL_1ec2:
					num2 = 307;
					if (BuildForm.IsUtil("ROBOCOPY"))
					{
						goto IL_1edb;
					}
					goto IL_208d;
					IL_1edb:
					num2 = 308;
					if (!LikeOperator.LikeString(text, "*<<<*>>>*", CompareMethod.Binary) && !Versioned.IsNumeric(text))
					{
						goto IL_1f03;
					}
					goto IL_1f34;
					IL_31e3:
					num2 = 472;
					if (OptXL_Load.Checked)
					{
						goto IL_31fa;
					}
					goto IL_3267;
					IL_1f03:
					num2 = 309;
					Interaction.MsgBox("Specify a numeric number Of copy retries", MsgBoxStyle.Exclamation, "Invalid Retries");
					goto IL_1f1b;
					IL_1f1b:
					num2 = 310;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_1f34:
					num2 = 313;
					if (!LikeOperator.LikeString(text13, "*<<<*>>>*", CompareMethod.Binary) && !Versioned.IsNumeric(text13))
					{
						goto IL_1f5c;
					}
					goto IL_1f8d;
					IL_31fa:
					num2 = 473;
					Globals_Renamed.currvaluetmp = fUtilPath + "LoadExcel.va \"" + text2 + "\" \"" + MyOpr + "\" \"" + CmbOpt.Text + "\" \"" + text3 + "\"";
					break;
					IL_1f5c:
					num2 = 314;
					Interaction.MsgBox("Specify a numeric wait time In seconds between retries", MsgBoxStyle.Exclamation, "Invalid Time");
					goto IL_1f74;
					IL_1f74:
					num2 = 315;
					TxtXLMacro.Focus();
					goto end_IL_0001_3;
					IL_1f8d:
					num2 = 318;
					text4 = Strings.Trim(mnuopt1.Text);
					goto IL_1fa5;
					IL_1fa5:
					num2 = 319;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + text3 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\"";
					goto IL_2048;
					IL_2048:
					num2 = 320;
					if (Operators.CompareString(text4, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_2064;
					IL_2064:
					num2 = 321;
					Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + " \"" + text4 + "\"";
					break;
					IL_208d:
					num2 = 324;
					if (BuildForm.IsUtil("MONGODB-EXTRACT"))
					{
						goto IL_20a6;
					}
					goto IL_218e;
					IL_20a6:
					num2 = 325;
					text4 = Strings.Trim(mnuopt1.Text);
					goto IL_20be;
					IL_20be:
					num2 = 326;
					text5 = Strings.Trim(mnuOpt2.Text);
					goto IL_20d6;
					IL_20d6:
					num2 = 327;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\" \"" + text4 + "\" \"" + text5 + "\"";
					break;
					IL_218e:
					num2 = 329;
					if (BuildForm.IsUtil("MONGODB-IMPORT"))
					{
						goto IL_21a7;
					}
					goto IL_227e;
					IL_21a7:
					num2 = 330;
					if (Operators.CompareString(Strings.Trim(text14), "", TextCompare: false) == 0)
					{
						goto IL_21c8;
					}
					goto IL_21d5;
					IL_21c8:
					num2 = 331;
					text14 = " ";
					goto IL_21d5;
					IL_21d5:
					num2 = 332;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\" \"" + MyOpr2 + "\" \"" + text14 + "\" \"" + text3 + "\" ";
					break;
					IL_227e:
					num2 = 334;
					if (BuildForm.IsUtil("CSV-TO-XML") || BuildForm.IsUtil("CSV-TO-HTML"))
					{
						goto IL_22a3;
					}
					goto IL_2301;
					IL_3267:
					num2 = 475;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_3283;
					}
					goto IL_32b5;
					IL_2301:
					num2 = 337;
					if (BuildForm.IsUtil("TDX-TO-CSV"))
					{
						goto IL_231a;
					}
					goto IL_23a5;
					IL_231a:
					num2 = 338;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text3 + "\" \"" + text13 + "\"";
					break;
					IL_23a5:
					num2 = 340;
					if (BuildForm.IsUtil("SQLITE-LOAD"))
					{
						goto IL_23be;
					}
					goto IL_24eb;
					IL_23be:
					num2 = 341;
					text4 = MyOpr;
					goto IL_23c8;
					IL_23c8:
					num2 = 342;
					if (Operators.CompareString(text4, "", TextCompare: false) != 0 && Strings.InStr(text4, "\\") == 0 && Strings.InStr(text4, ".") == 0)
					{
						goto IL_2405;
					}
					goto IL_241c;
					IL_3283:
					num2 = 476;
					Interaction.MsgBox("Specify the Excel WorkSheet(s) to import to.", MsgBoxStyle.Exclamation, "Missing Data");
					goto IL_329b;
					IL_329b:
					num2 = 477;
					TxtXLSheet.Focus();
					goto end_IL_0001_3;
					IL_2405:
					num2 = 343;
					text4 += ".sdb";
					goto IL_247e;
					IL_241c:
					num2 = 345;
					if ((Operators.CompareString(MyOpr, "", TextCompare: false) == 0) | !LikeOperator.LikeString(Strings.LCase(MyOpr), "*.sdb", CompareMethod.Binary))
					{
						goto IL_244e;
					}
					goto IL_247e;
					IL_244e:
					num2 = 346;
					Interaction.MsgBox("You must specify a SQLite DB With an extension Of sdb. E.g., MyDB.sdb", MsgBoxStyle.Exclamation, "Missing SQLite DB");
					goto IL_2466;
					IL_2466:
					num2 = 347;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_247e:
					num2 = 350;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + text4 + "\" \"" + text12 + "\" \"" + text3 + "\"";
					break;
					IL_24eb:
					num2 = 352;
					if (BuildForm.IsUtil("STACK"))
					{
						goto IL_2501;
					}
					goto IL_2555;
					IL_2501:
					num2 = 353;
					Globals_Renamed.currvaluetmp = fUtilPath + "StackResults.va \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_2555:
					num2 = 355;
					if (BuildForm.IsUtil("JOIN"))
					{
						goto IL_256e;
					}
					goto IL_26b6;
					IL_256e:
					num2 = 356;
					text13 = Strings.UCase(text13);
					goto IL_257d;
					IL_257d:
					num2 = 357;
					flag = true;
					goto IL_2586;
					IL_2586:
					num2 = 358;
					num5 = 1;
					goto IL_258e;
					IL_258e:
					num2 = 359;
					if ((Operators.CompareString(Strings.Mid(text13 + "    ", num5, 1), "Y", TextCompare: false) != 0) & (Operators.CompareString(Strings.Mid(text13 + "    ", num5, 1), "N", TextCompare: false) != 0))
					{
						goto IL_25dd;
					}
					goto IL_25ea;
					IL_25dd:
					num2 = 360;
					flag = false;
					goto IL_25f8;
					IL_25ea:
					num2 = 363;
					num5 = checked(num5 + 1);
					if (num5 <= 4)
					{
						goto IL_258e;
					}
					goto IL_25f8;
					IL_25f8:
					num2 = 364;
					if (!flag)
					{
						goto IL_2609;
					}
					goto IL_263a;
					IL_2609:
					num2 = 365;
					Interaction.MsgBox("You must specify valid Join options. Assign 4 Y/N flags - (1) whether To append To output, (2) whether To Set missing values In output, (3) whether To delete the join column In the primary file, And (4) whether To outer join files. E.g., NYYY", MsgBoxStyle.Exclamation, "Invalid Join Options");
					goto IL_2621;
					IL_2621:
					num2 = 366;
					TxtXLMacro.Focus();
					goto end_IL_0001_3;
					IL_263a:
					num2 = 369;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\" \"" + text + "\" \"" + text13 + "\"";
					break;
					IL_26b6:
					num2 = 371;
					if (Operators.CompareString(Globals_Renamed.currhelptmp, "SHAREPOINT", TextCompare: false) == 0)
					{
						goto IL_26d8;
					}
					goto IL_278c;
					IL_26d8:
					num2 = 372;
					if (Operators.CompareString(MyOpr, "", TextCompare: false) == 0 || Operators.CompareString(MyOpr.ToLower(), "http://", TextCompare: false) == 0)
					{
						goto IL_270b;
					}
					goto IL_273c;
					IL_32b5:
					num2 = 480;
					if (Operators.CompareString(text12, "", TextCompare: false) == 0)
					{
						goto IL_32d1;
					}
					goto IL_3301;
					IL_273c:
					num2 = 377;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"\" \"\" ";
					break;
					IL_270b:
					num2 = 373;
					Interaction.MsgBox("Missing a web address to publish to.", MsgBoxStyle.Exclamation, "Missing Web Address");
					goto IL_2723;
					IL_2723:
					num2 = 374;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					IL_278c:
					num2 = 379;
					if (BuildForm.IsUtil("JMP"))
					{
						goto IL_27a2;
					}
					goto IL_2800;
					IL_27a2:
					num2 = 380;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text3 + "\"";
					break;
					IL_2800:
					num2 = 382;
					if (BuildForm.IsUtil("XML-TO-CSV") || BuildForm.IsUtil("PROMPT-INPUT"))
					{
						goto IL_2825;
					}
					goto IL_2875;
					IL_32d1:
					num2 = 481;
					Interaction.MsgBox("Specify an Excel Template file to Import CSV files to.", MsgBoxStyle.Exclamation, "Missing Data");
					goto IL_32e9;
					IL_2875:
					num2 = 385;
					if (BuildForm.IsUtil("GET-TCA"))
					{
						goto IL_288b;
					}
					goto IL_28e9;
					IL_288b:
					num2 = 386;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text3 + "\"";
					break;
					IL_28e9:
					num2 = 388;
					if (BuildForm.IsUtil("GET-AQUA"))
					{
						goto IL_2902;
					}
					goto IL_29f1;
					IL_2902:
					num2 = 389;
					if (Operators.CompareString(Strings.UCase(text12), "DEFAULT", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text12), "GER", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text12), "AMR", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text12), "GAR", TextCompare: false) != 0)
					{
						goto IL_2962;
					}
					goto IL_2993;
					IL_32e9:
					num2 = 482;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_3301:
					num2 = 485;
					Globals_Renamed.currvaluetmp = fUtilPath + "ImportExcel.va \"" + text12 + "\" \"" + MyOpr + "\" \"" + text2 + "\" \"" + text + "\" \"\" \"" + text13 + "\" \"" + CmbOpt.Text + "\" \"" + text3 + "\"";
					break;
					IL_2a19:
					num2 = 397;
					text4 = MyOpr;
					goto IL_2a23;
					IL_2962:
					num2 = 390;
					Interaction.MsgBox("Enter a valid Aqua server (AMR,GAR or GER) Or choose the Default Server", MsgBoxStyle.Exclamation, "Invalid Server");
					goto IL_297a;
					IL_297a:
					num2 = 391;
					TxtInXL.Focus();
					goto end_IL_0001_3;
					IL_2993:
					num2 = 394;
					Globals_Renamed.currvaluetmp = fUtilPath + fVaFileName + " \"" + text2 + "\" \"" + MyOpr + "\" \"" + text12 + "\"";
					break;
					IL_29f1:
					num2 = 396;
					if (BuildForm.IsUtil("R") || BuildForm.IsUtil("PYTHON"))
					{
						goto IL_2a19;
					}
					goto IL_2b22;
					IL_2a23:
					num2 = 398;
					if (Operators.CompareString(text4, "", TextCompare: false) != 0)
					{
						goto IL_2a3f;
					}
					goto IL_2a5b;
					IL_2b22:
					num2 = 408;
					if (BuildForm.IsUtil("FILE-ATTRIBUTES"))
					{
						goto IL_2b3b;
					}
					goto IL_2bf3;
					IL_2b3b:
					num2 = 409;
					if ((Operators.CompareString(Strings.UCase(MyOpr), "READONLY", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(MyOpr), "READWRITE", TextCompare: false) != 0))
					{
						goto IL_2b72;
					}
					goto IL_2ba3;
					IL_2b72:
					num2 = 410;
					Interaction.MsgBox("Specify a valid File Attribute to Set.", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_2b8a;
					IL_2b8a:
					num2 = 411;
					TxtOutXL.Focus();
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 488;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 15196;
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

	private bool Chk_Opr(ref string MyOpr, ComboBox MyTxtBox)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 326:
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
							goto IL_001a;
						case 5:
							goto IL_00ed;
						case 6:
							goto IL_0101;
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
					IL_001a:
					num2 = 4;
					if (!((Operators.CompareString(MyOpr, "EQ", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "EQS", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "NE", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "NES", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "GT", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "GTS", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "LT", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "LTS", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "GE", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "GES", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "LE", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "LES", TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					goto IL_00ed;
					IL_00ed:
					num2 = 5;
					Interaction.MsgBox("Specify a valid filter. (I.e., EQ, EQS, NE, NES, GT, GTS, LT, LTS, GE, GES, LE, LES)", MsgBoxStyle.Exclamation, "Invalid Data");
					goto IL_0101;
					IL_000f:
					num2 = 3;
					MyOpr = Strings.UCase(MyOpr);
					goto IL_001a;
					IL_0101:
					num2 = 6;
					MyTxtBox.Focus();
					break;
					IL_000b:
					num2 = 2;
					result = true;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 326;
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

	private void CmdViewXL_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 703:
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
							goto IL_001d;
						case 6:
							goto IL_003e;
						case 8:
							goto IL_004e;
						case 9:
							goto IL_0060;
						case 11:
							goto IL_0071;
						case 12:
							goto IL_0087;
						case 13:
							goto IL_009b;
						case 14:
							goto IL_00ba;
						case 15:
							goto IL_00d3;
						case 16:
							goto IL_00fe;
						case 19:
							goto IL_0118;
						case 20:
							goto IL_0128;
						case 21:
							goto IL_0141;
						case 22:
							goto IL_0151;
						case 23:
							goto IL_0170;
						case 24:
							goto IL_0189;
						case 25:
							goto IL_01b4;
						case 26:
							goto IL_01c7;
						case 27:
							goto IL_01d5;
						case 28:
							goto IL_01f4;
						case 32:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 10:
						case 17:
						case 18:
						case 29:
						case 30:
						case 31:
						case 33:
						case 34:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003e:
					num2 = 6;
					Browse_Dir(4);
					goto end_IL_0001_3;
					IL_01b4:
					num2 = 25;
					num5 = Strings.InStrRev(text, "\\");
					goto IL_01c7;
					IL_0189:
					num2 = 24;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0 || Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_01b4;
					IL_01c7:
					num2 = 26;
					if (num5 != 0)
					{
						goto IL_01d5;
					}
					goto IL_01f4;
					IL_000b:
					num2 = 2;
					num6 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = 0;
					goto IL_001d;
					IL_001d:
					num2 = 5;
					if (BuildForm.IsUtil("EMAIL") || BuildForm.IsUtil("AUTO-COMMONALITY"))
					{
						goto IL_003e;
					}
					goto IL_004e;
					IL_01d5:
					num2 = 27;
					text = Strings.Trim(Strings.Mid(text + " ", checked(num5 + 1)));
					goto IL_01f4;
					IL_004e:
					num2 = 8;
					if (BuildForm.IsUtil("BEGIN-HPC"))
					{
						goto IL_0060;
					}
					goto IL_0071;
					IL_0060:
					num2 = 9;
					Browse_Dir(3);
					goto end_IL_0001_3;
					IL_0071:
					num2 = 11;
					if (!BuildForm.IsUtil("TDX-TO-CSV"))
					{
						break;
					}
					goto IL_0087;
					IL_0087:
					num2 = 12;
					if (chkOption.Checked)
					{
						goto IL_009b;
					}
					goto IL_0118;
					IL_009b:
					num2 = 13;
					BuildForm.FileOpenSave("O", "", "csv", "File With patterns", text);
					goto IL_00ba;
					IL_00ba:
					num2 = 14;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00d3;
					IL_00d3:
					num2 = 15;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0 || Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00fe;
					IL_01f4:
					num2 = 28;
					TxtXLSheet.Text = text;
					goto end_IL_0001_3;
					IL_00fe:
					num2 = 16;
					TxtXLSheet.Text = text;
					goto end_IL_0001_3;
					IL_0118:
					num2 = 19;
					text = TxtCSV.Text;
					goto IL_0128;
					IL_0128:
					num2 = 20;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0141;
					}
					goto IL_0151;
					IL_0141:
					num2 = 21;
					text = BuildForm.Replace_Globals("<<<spf-tdx-data1>>>");
					goto IL_0151;
					IL_0151:
					num2 = 22;
					BuildForm.FileOpenSave("O", "", "xml.gz", "TDX File", text);
					goto IL_0170;
					IL_0170:
					num2 = 23;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0189;
					end_IL_0001_2:
					break;
				}
				num2 = 32;
				Start_XL(TxtInXL.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 703;
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

	private void FrmUtil_Load(object eventSender, EventArgs eventArgs)
	{
		string text = "";
		if (BuildForm.IsUtil("AUTO-COMMONALITY"))
		{
			Text = "Utilities - Screen and Visualize Features";
		}
		else
		{
			Text = "Utilities - " + Globals_Renamed.currhelptmp;
		}
		checked
		{
			base.Height = 290 + f_HeightAdd;
			if (Operators.CompareString(fUtilPath, "", TextCompare: false) == 0)
			{
				fUtilPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\";
			}
			if (Operators.CompareString(Strings.UCase(Globals_Renamed.currhelptmp), "EXCEL", TextCompare: false) != 0)
			{
				chkOption.Left = lblUtil.Left;
				chkOption.Top = cmdHelp.Top + 10;
				CmbOpt.Top = chkOption.Top;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "title", "", 1000, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblUtil.Text = text;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label1", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblcsv.Text = text;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label2", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblXLOut.Text = text;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label3", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblXLIn.Text = text;
				lblXLIn.Visible = true;
				TxtInXL.Visible = true;
			}
			else
			{
				lblXLIn.Visible = false;
				TxtInXL.Visible = false;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label4", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblXLWS.Text = text;
			}
			else
			{
				lblXLWS.Visible = false;
				TxtXLSheet.Visible = false;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label5", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				lblXLMac.Text = text;
			}
			else
			{
				lblXLMac.Visible = false;
				TxtXLMacro.Visible = false;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label6", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				LblOther1.Text = text;
				LblOther1.Visible = true;
				TxtOther1.Visible = true;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "label7", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				LblOther2.Text = text;
				LblOther2.Visible = true;
				TxtOther2.Visible = true;
			}
			text = "";
			text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "opt", "True", 10, Globals_Renamed.MySchemaDir + "\\utilities.ini")));
			if (Operators.CompareString(Strings.UCase(text), "FALSE", TextCompare: false) == 0)
			{
				FrmOpt.Visible = false;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "check1", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				chkOption.Text = text;
				chkOption.Visible = true;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "cmbopt1", "", 500, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				CmbOpt.Items.Clear();
				CmbOpt.Visible = true;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "height", "", 50, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "", TextCompare: false) != 0 && Versioned.IsNumeric(text))
			{
				base.Height = Conversions.ToInteger(text) + f_HeightAdd;
			}
			text = "";
			text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "optparallel", "-1", 50, Globals_Renamed.MySchemaDir + "\\utilities.ini"));
			if (Operators.CompareString(text, "-1", TextCompare: false) != 0 && Versioned.IsNumeric(text))
			{
				mnuopts.Visible = true;
				ParallelThreadsMenuItem.Visible = true;
				ParallelThreadsComboBox1.Visible = true;
				ParallelThreadsComboBox1.Items.Clear();
				ParallelThreadsComboBox1.Items.Add("0");
				int processorCount = Environment.ProcessorCount;
				for (int i = 1; i <= processorCount; i++)
				{
					ParallelThreadsComboBox1.Items.Add($"{i}");
				}
				if (Operators.CompareString(ParallelThreadsComboBox1.Text, "", TextCompare: false) == 0)
				{
					ParallelThreadsComboBox1.SelectedItem = "0";
				}
				else
				{
					ParallelThreadsComboBox1.SelectedItem = ParallelThreadsComboBox1.Text;
				}
				ParallelThreadsComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			}
			text = "";
			text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "showorderedparallelmenuitem", "False", 10, Globals_Renamed.MySchemaDir + "\\utilities.ini")));
			if (Operators.CompareString(Strings.UCase(text), "TRUE", TextCompare: false) == 0)
			{
				OrderedParallelMenuItem.Visible = true;
			}
			else
			{
				OrderedParallelMenuItem.Visible = false;
			}
			text = "";
			text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "orderedparallel", "False", 10, Globals_Renamed.MySchemaDir + "\\utilities.ini")));
			if (Operators.CompareString(Strings.UCase(text), "TRUE", TextCompare: false) == 0)
			{
				OrderedParallelMenuItem.Checked = true;
			}
			else
			{
				OrderedParallelMenuItem.Checked = false;
			}
			text = "";
			text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "button", "", 5, Globals_Renamed.MySchemaDir + "\\utilities.ini")));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				if (Operators.CompareString(Strings.Mid(text + " ", 1, 1), "F", TextCompare: false) == 0)
				{
					CmdBrowse0.Visible = false;
				}
				if (Operators.CompareString(Strings.Mid(text + "  ", 2, 1), "F", TextCompare: false) == 0)
				{
					CmdBrowse1.Visible = false;
				}
				if (Operators.CompareString(Strings.Mid(text + "   ", 3, 1), "F", TextCompare: false) == 0)
				{
					CmdBrowse2.Visible = false;
				}
				if (Operators.CompareString(Strings.Mid(text + "    ", 4, 1), "F", TextCompare: false) == 0)
				{
					CmdViewXL.Visible = false;
				}
			}
			ComboBox MyCMB = TxtCSV;
			Add_Cmb("valid1", ref MyCMB);
			TxtCSV = MyCMB;
			MyCMB = TxtOutXL;
			Add_Cmb("valid2", ref MyCMB);
			TxtOutXL = MyCMB;
			MyCMB = TxtInXL;
			Add_Cmb("valid3", ref MyCMB);
			TxtInXL = MyCMB;
			MyCMB = TxtXLSheet;
			Add_Cmb("valid4", ref MyCMB);
			TxtXLSheet = MyCMB;
			MyCMB = TxtXLMacro;
			Add_Cmb("valid5", ref MyCMB);
			TxtXLMacro = MyCMB;
			MyCMB = TxtOther1;
			Add_Cmb("valid6", ref MyCMB);
			TxtOther1 = MyCMB;
			MyCMB = TxtOther2;
			Add_Cmb("valid7", ref MyCMB);
			TxtOther2 = MyCMB;
			MyCMB = TxtCSV;
			Def_Cmb("default1", ref MyCMB);
			TxtCSV = MyCMB;
			MyCMB = TxtOutXL;
			Def_Cmb("default2", ref MyCMB);
			TxtOutXL = MyCMB;
			MyCMB = TxtInXL;
			Def_Cmb("default3", ref MyCMB);
			TxtInXL = MyCMB;
			MyCMB = TxtXLSheet;
			Def_Cmb("default4", ref MyCMB);
			TxtXLSheet = MyCMB;
			MyCMB = TxtXLMacro;
			Def_Cmb("default5", ref MyCMB);
			TxtXLMacro = MyCMB;
			MyCMB = TxtOther1;
			Def_Cmb("default6", ref MyCMB);
			TxtOther1 = MyCMB;
			MyCMB = TxtOther2;
			Def_Cmb("default7", ref MyCMB);
			TxtOther2 = MyCMB;
			MyCMB = CmbOpt;
			Add_Cmb("cmbopt1", ref MyCMB);
			CmbOpt = MyCMB;
			MyCMB = CmbOpt;
			Def_Cmb("defaultopt1", ref MyCMB);
			CmbOpt = MyCMB;
			text = "";
			text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currhelptmp, "add_share", "", 3, Globals_Renamed.MySchemaDir + "\\utilities.ini")));
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				if (Operators.CompareString(Strings.Mid(text + " ", 1, 1), "T", TextCompare: false) == 0)
				{
					MyCMB = TxtCSV;
					BuildForm.Add_Shares(ref MyCMB);
					TxtCSV = MyCMB;
				}
				if (Operators.CompareString(Strings.Mid(text + "  ", 2, 1), "T", TextCompare: false) == 0)
				{
					MyCMB = TxtOutXL;
					BuildForm.Add_Shares(ref MyCMB);
					TxtOutXL = MyCMB;
				}
				if (Operators.CompareString(Strings.Mid(text + "   ", 3, 1), "T", TextCompare: false) == 0)
				{
					MyCMB = TxtInXL;
					BuildForm.Add_Shares(ref MyCMB);
					TxtInXL = MyCMB;
				}
			}
			if (BuildForm.IsUtil("EXCEL"))
			{
				if (OptXL_Import.Checked)
				{
					base.Height = 476 + f_HeightAdd;
					lblXLIn.Visible = true;
					TxtInXL.Visible = true;
				}
				else
				{
					lblXLIn.Visible = false;
					TxtInXL.Visible = false;
				}
				lblXLWS.Visible = true;
				TxtXLSheet.Visible = true;
				lblXLMac.Visible = true;
				TxtXLMacro.Visible = true;
				chkOption.Text = "Continue If Job Fails";
				chkOption.Visible = true;
				CmbOpt.Items.Clear();
				CmbOpt.Items.Add("Version 1");
				CmbOpt.Items.Add("Version 2");
				CmbOpt.Visible = true;
				Chk_Cmb();
			}
			else if (BuildForm.IsUtil("UPDATE-TIME") || BuildForm.IsUtil("START-MACRO") || BuildForm.IsUtil("SHAREPOINT-DELETE"))
			{
				lblXLOut.Visible = false;
				TxtOutXL.Visible = false;
				TxtOutXL.Enabled = false;
			}
			else if (BuildForm.IsUtil("SITE-LOOP") || BuildForm.IsUtil("GET-SITE-TIME"))
			{
				lblXLOut.Visible = false;
				TxtOutXL.Visible = false;
				CmdBrowse0.Text = "Nodes";
			}
			else if (BuildForm.IsUtil("VALUE-IN-FILE"))
			{
				Button MyButton = CmdBrowse2;
				BuildForm.Set_Btn_Img(ref MyButton, "refresh");
				CmdBrowse2 = MyButton;
			}
			else if (BuildForm.IsUtil("BEGIN-HPC"))
			{
				CmdViewXL.Top = TxtXLSheet.Top;
				CmdViewXL.Height = CmdBrowse3.Height;
				CmdViewXL.Text = "...";
				CmdViewXL.Width = CmdBrowse3.Width;
				CmdViewXL.Visible = true;
				ToolTip1.SetToolTip(CmdViewXL, "");
				mnuopts.Visible = true;
				mnuopt1.Items.Clear();
				mnuOpt2.Items.Clear();
				mnuopt1.Items.Add("32");
				mnuopt1.Items.Add("64");
				mnuopt1.Items.Add("128");
				mnuOpt2.Items.Add("8");
				mnuOpt2.Items.Add("12");
				mnuOpt2.Items.Add("24");
				mnuOpt2.Items.Add("32");
				mnuOpt2.Items.Add("48");
				if (Operators.CompareString(mnuOpt2.Text, "", TextCompare: false) == 0)
				{
					mnuOpt2.Text = Conversions.ToString(8);
				}
				if (Operators.CompareString(mnuopt1.Text, "", TextCompare: false) == 0)
				{
					mnuopt1.Text = Conversions.ToString(32);
				}
				mnulabel2.Text = "Timeout/hours";
				mnulabel2.Visible = true;
				mnulabel1.Text = "Peak memory/GB (<128)";
				mnulabel1.Visible = true;
				CmdBrowse3.Visible = false;
			}
			else if (BuildForm.IsUtil("EMAIL"))
			{
				if (Operators.CompareString(TxtOutXL.Text, "", TextCompare: false) == 0 && Operators.CompareString(TxtOther1.Text, "", TextCompare: false) == 0 && Operators.CompareString(TxtXLMacro.Text, "", TextCompare: false) == 0)
				{
					TxtOutXL.Text = "self";
				}
				if (Operators.CompareString(TxtInXL.Text, "", TextCompare: false) == 0)
				{
					TxtInXL.Text = "";
				}
				CmdBrowse3.Top = TxtOther1.Top;
				CmdBrowse3.Visible = true;
				CmdViewXL.Top = TxtXLMacro.Top;
				CmdViewXL.Height = CmdBrowse3.Height;
				CmdViewXL.Text = "...";
				CmdViewXL.Width = CmdBrowse3.Width;
				CmdViewXL.Visible = true;
				ToolTip1.SetToolTip(CmdViewXL, "Browse");
				ToolTip1.SetToolTip(CmdViewXL, "");
			}
			else if (BuildForm.IsUtil("TDX-TO-CSV") || BuildForm.IsUtil("AUTO-COMMONALITY"))
			{
				CmdViewXL.Top = TxtXLSheet.Top;
				CmdViewXL.Height = CmdBrowse3.Height;
				CmdViewXL.Text = "...";
				CmdViewXL.Width = CmdBrowse3.Width;
				CmdViewXL.Visible = true;
				ToolTip1.SetToolTip(CmdViewXL, "");
				if (BuildForm.IsUtil("AUTO-COMMONALITY"))
				{
					mnuopts.Visible = true;
					mnuOpt2.Items.Clear();
					mnuOpt2.Items.Add("Ideal_Forest");
					mnuOpt2.Items.Add("Feature_Selection");
					mnuOpt2.Items.Add("Decision_Tree_1_Surrogate");
					mnuOpt2.Items.Add("LEMS-RCA");
					mnulabel2.Text = "Algorithm";
					mnulabel2.Visible = true;
					mnulabel1.Text = "Minimum Variable Importance % (1-100)";
					mnulabel1.Visible = true;
					mnulabel3.Visible = true;
					mnuopt3.Items.Clear();
					mnuopt3.Items.Add("");
					mnuopt3.Items.Add("visual_id_or_die_index");
					mnulabel4.Visible = true;
					if (Operators.CompareString(Strings.LCase(mnuOpt2.Text), "ideal_forest", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(mnuOpt2.Text), "decision_tree_1_surrogate", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(mnuOpt2.Text), "lems-rca", TextCompare: false) != 0)
					{
						mnuOpt2.Text = "Feature_Selection";
					}
					if (Operators.CompareString(mnuopt1.Text, "", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(mnuOpt2.Text), "lems-rca", TextCompare: false) == 0)
					{
						mnuopt1.Text = f_TmpL;
					}
					else if (Operators.CompareString(mnuopt1.Text, "", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(mnuOpt2.Text), "lems-rca", TextCompare: false) != 0)
					{
						mnuopt1.Text = f_TmpNL;
					}
					if (Operators.CompareString(Strings.LCase(mnuOpt2.Text), "lems-rca", TextCompare: false) == 0)
					{
						mnulabel1.Text = "Minimum Variable Importance % (1-100)";
					}
					CmdHelp2.Left = CmdBrowse3.Left;
					CmdHelp2.Top = TxtOther1.Top;
					CmdHelp2.Text = "...";
					CmdHelp2.Width = CmdBrowse3.Width;
					CmdHelp2.Height = CmdBrowse3.Height;
					ToolTip1.SetToolTip(CmdHelp2, "");
					CmdHelp2.Visible = true;
					f_TmpOld = mnuOpt2.Text;
				}
			}
			else if (BuildForm.IsUtil("JOIN"))
			{
				if (Operators.CompareString(TxtXLMacro.Text, "", TextCompare: false) == 0)
				{
					TxtXLMacro.Text = "NNYY";
				}
			}
			else if (BuildForm.IsUtil("RENAME"))
			{
				Text = "Utilities - RENAME-MOVE";
			}
			else if (BuildForm.IsUtil("SMART-APPEND"))
			{
				mnuopts.Visible = true;
				if (Operators.CompareString(TxtOther1.Text, "", TextCompare: false) == 0)
				{
					TxtOther1.Text = "-1";
				}
				CmdHelp2.Visible = true;
				mnulabel2.Text = "Convert Headers to Uppercase";
				mnuOpt2.Items.Clear();
				mnuOpt2.Items.Add("N");
				mnuOpt2.Items.Add("Y");
				Chk_Cmb();
				OrderedParallelMenuItem.Visible = true;
			}
			else if (BuildForm.IsUtil("ROBOCOPY"))
			{
				if (Operators.CompareString(TxtXLMacro.Text, "", TextCompare: false) == 0)
				{
					TxtXLMacro.Text = "30";
				}
				if (Operators.CompareString(TxtOther2.Text, "", TextCompare: false) == 0)
				{
					TxtOther2.Text = "N";
				}
				CmdBrowse3.Visible = false;
				mnulabel1.Text = "Comma Delimited list of Custom Pass Exit Codes";
				mnuopts.Visible = true;
				mnuopt1.Visible = true;
				mnuOpt2.Visible = false;
				mnulabel1.Visible = true;
				mnulabel2.Visible = false;
			}
			else if (BuildForm.IsUtil("MONGODB-EXTRACT"))
			{
				mnulabel1.Text = "Column Fill Percentage Threshold (1-100)";
				mnulabel1.Visible = true;
				mnuopts.Visible = true;
				TxtXLSheet.Items.Add("{'visual_id':'761U5A5300041'}");
				TxtXLSheet.Items.Add("{'entity' : {'$regex' : '^PLM'}}");
				TxtXLSheet.Items.Add("{'entity' : {'$regex' : '_A$'}}");
				TxtXLSheet.Items.Add("{'entity' : {'$regex' : 'something'}}");
				TxtXLSheet.Items.Add("{'entity' : {'$regex' : '^plm', '$options': 'i' }}");
				TxtXLSheet.Items.Add("{'_id':{'$ne' : '_id'}}");
				TxtXLSheet.Items.Add("{'score': { '$gte': 70, '$lte': 90 }}");
				TxtXLSheet.Items.Add("{'out_date': {'$gte': ISODate('2013-01-01T00:00:00.0Z'), '$lt': ISODate('2013-02-01T00:00:00.0Z')}}");
				TxtXLSheet.Items.Add("{ 'lot': { '$in': [ 'x', 'y', 'z'] } }");
				TxtXLSheet.Items.Add("{'$and':[{'price': { '$ne': 1.99 }}, {'price': { '$exists': true}}]}");
				TxtXLSheet.Items.Add("{'$and':[{'$or':[{'operation':'7721'},{'operation':'7731'}]},{'lot':'12345678'}]}");
				CmdBrowse3.Top = TxtXLSheet.Top;
				CmdBrowse3.Visible = true;
				CmdViewXL.Visible = false;
				Button MyButton = CmdBrowse0;
				BuildForm.Set_Btn_Img(ref MyButton, "node");
				CmdBrowse0 = MyButton;
				ToolTip1.SetToolTip(CmdBrowse0, "Node");
			}
			else if (BuildForm.IsUtil("MONGODB-IMPORT"))
			{
				CmdBrowse3.Visible = false;
				CmdViewXL.Visible = false;
				Button MyButton = CmdBrowse0;
				BuildForm.Set_Btn_Img(ref MyButton, "node");
				CmdBrowse0 = MyButton;
				ToolTip1.SetToolTip(CmdBrowse0, "Node");
			}
			else if (BuildForm.IsUtil("STACK"))
			{
				ToolTip1.SetToolTip(CmdBrowse2, "View Columns in Input CSV File");
				CmdBrowse2.Text = "View";
			}
			else if (BuildForm.IsUtil("CREATE-QUERY-SHORTCUT"))
			{
				cmdHelp.Visible = false;
				TxtInXL.Text = "SQLPathFinder Query Execution Shortcut";
				TxtXLSheet.Text = "%userprofile%\\my programs\\sqlpathfinder3\\sqlpathfinder3.exe";
				TxtXLSheet.Items.Add(Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder3.exe");
				Text = "Create Query Shortcut";
			}
			else if (BuildForm.IsUtil("WEB-COPY"))
			{
				MyCMB = TxtCSV;
				BuildForm.Load_HTTP_Combo(ref MyCMB);
				TxtCSV = MyCMB;
			}
			isInit = false;
		}
	}

	private void FrmUtil_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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

	private void OptXL_Load_CheckedChanged(object sender, EventArgs e)
	{
		base.Height = checked(290 + f_HeightAdd);
		lblXLIn.Visible = false;
		TxtInXL.Visible = false;
		TxtCSV.Focus();
	}

	private void OptXL_Import_CheckedChanged(object sender, EventArgs e)
	{
		base.Height = checked(476 + f_HeightAdd);
		lblXLIn.Visible = true;
		TxtInXL.Visible = true;
		TxtCSV.Focus();
	}

	private void CmdBrowse0_Click(object sender, EventArgs e)
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
				Browse_Dir(0);
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

	private void CmdBrowse1_Click(object sender, EventArgs e)
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
				Browse_Dir(1);
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

	private void CmdBrowse2_Click(object sender, EventArgs e)
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
				Browse_Dir(2);
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

	private void CmdHelp2_Click(object sender, EventArgs e)
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
				case 281:
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
							goto IL_0048;
						case 6:
							goto IL_0060;
						case 7:
							goto IL_008a;
						case 8:
							goto IL_009f;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0060:
					num2 = 6;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0 || Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_008a;
					IL_009f:
					num2 = 8;
					TxtOther1.Text = text;
					goto end_IL_0001_3;
					IL_0048:
					num2 = 5;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0060;
					IL_008a:
					num2 = 7;
					text = BuildForm.Strip_Add_MyPCDir("S", text);
					goto IL_009f;
					IL_000b:
					num2 = 2;
					if (!BuildForm.IsUtil("AUTO-COMMONALITY"))
					{
						break;
					}
					goto IL_001e;
					IL_001e:
					num2 = 3;
					text = "";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					BuildForm.FileOpenSave("O", "", "ini", "Chart Config File", "");
					goto IL_0048;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Interaction.MsgBox(General_Procedures.Get_UI("util-update-time"), MsgBoxStyle.Information, "Update Site Time");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 281;
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
				Browse_Dir(3);
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

	private void CmbOpt_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar = '\0';
	}

	private void CmbOpt_SelectedIndexChanged(object sender, EventArgs e)
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
				case 91:
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
					if (!BuildForm.IsUtil("SMART-APPEND") && !BuildForm.IsUtil("EXCEL"))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Chk_Cmb();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 91;
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

	private void mnuOpt2_TextChanged(object sender, EventArgs e)
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
				case 334:
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
							goto IL_002c;
						case 4:
							goto IL_004d;
						case 5:
							goto IL_0060;
						case 6:
							goto IL_0074;
						case 8:
							goto IL_008a;
						case 9:
							goto IL_00c1;
						case 10:
							goto IL_00d5;
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
					IL_0074:
					num2 = 6;
					f_TmpOld = mnuOpt2.Text;
					goto end_IL_0001_3;
					IL_008a:
					num2 = 8;
					if (Operators.CompareString(mnuOpt2.Text, "LEMS-RCA", TextCompare: false) == 0 || Operators.CompareString(f_TmpOld, "LEMS-RCA", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00c1;
					IL_0060:
					num2 = 5;
					mnuopt1.Text = f_TmpL;
					goto IL_0074;
					IL_00d5:
					num2 = 10;
					mnuopt1.Text = f_TmpNL;
					break;
					IL_000b:
					num2 = 2;
					if (!BuildForm.IsUtil("AUTO-COMMONALITY") || isInit)
					{
						goto end_IL_0001_3;
					}
					goto IL_002c;
					IL_00c1:
					num2 = 9;
					f_TmpL = mnuopt1.Text;
					goto IL_00d5;
					IL_002c:
					num2 = 3;
					if (Operators.CompareString(mnuOpt2.Text, "LEMS-RCA", TextCompare: false) == 0)
					{
						goto IL_004d;
					}
					goto IL_008a;
					IL_004d:
					num2 = 4;
					f_TmpNL = mnuopt1.Text;
					goto IL_0060;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				f_TmpOld = mnuOpt2.Text;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 334;
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

	private void mnuOpt2_Click(object sender, EventArgs e)
	{
	}
}

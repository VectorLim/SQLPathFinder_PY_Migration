using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class frmconfigure : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowseJMP")]
	private Button _CmdBrowseJMP;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowseDir")]
	private Button _CmdBrowseDir;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdHelp")]
	private Button _CmdHelp;

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
	[AccessedThroughProperty("TabConfig")]
	private TabControl _TabConfig;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridColor")]
	private DataGridView _GridColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbColor")]
	private ComboBox _CmbColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdClearAll")]
	private Button _CmdClearAll;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDefColor")]
	private Button _CmdDefColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAssocTab")]
	private Button _cmdAssocTab;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdQDir")]
	private Button _cmdQDir;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdIcon")]
	private Button _cmdIcon;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("radioR")]
	private RadioButton _radioR;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("radiojmp")]
	private RadioButton _radiojmp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("radioPy")]
	private RadioButton _radioPy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("radiopy3")]
	private RadioButton _radiopy3;

	private string l_InPCDir;

	private string l_InSPFCmdWin;

	private short f_row;

	private short f_col;

	private bool CmbBoxKey;

	private short f_TrueRow;

	private string l_jmp;

	private string l_R;

	private string l_Py;

	private string l_XL;

	private string l_Py3;

	private string l_which;

	private string l_WhichPath;

	private string l_WhichPrompt;

	private string lMyPyPath;

	private string lMyPyPath3;

	[field: AccessedThroughProperty("TxtJMP")]
	public virtual ComboBox TxtJMP
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button CmdBrowseJMP
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowseJMP;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowseJMP_Click;
			Button button = _CmdBrowseJMP;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowseJMP = value;
			button = _CmdBrowseJMP;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button CmdBrowseDir
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowseDir;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowseDir_Click;
			Button button = _CmdBrowseDir;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowseDir = value;
			button = _CmdBrowseDir;
			if (button != null)
			{
				button.Click += value2;
			}
		}
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

	[field: AccessedThroughProperty("lblPCDir")]
	public virtual Label lblPCDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TabControl TabConfig
	{
		[CompilerGenerated]
		get
		{
			return _TabConfig;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TabConfig_SelectedIndexChanged;
			TabControl tabControl = _TabConfig;
			if (tabControl != null)
			{
				tabControl.SelectedIndexChanged -= value2;
			}
			_TabConfig = value;
			tabControl = _TabConfig;
			if (tabControl != null)
			{
				tabControl.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TabConfigData")]
	internal virtual TabPage TabConfigData
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabConfigColor")]
	internal virtual TabPage TabConfigColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblGridColor")]
	internal virtual Label LblGridColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridColor
	{
		[CompilerGenerated]
		get
		{
			return _GridColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GridColor_Click;
			KeyEventHandler value3 = GridColor_KeyDown;
			KeyPressEventHandler value4 = GridColor_KeyPress;
			ScrollEventHandler value5 = GridColor_Scroll;
			DataGridView dataGridView = _GridColor;
			if (dataGridView != null)
			{
				dataGridView.Click -= value2;
				dataGridView.KeyDown -= value3;
				dataGridView.KeyPress -= value4;
				dataGridView.Scroll -= value5;
			}
			_GridColor = value;
			dataGridView = _GridColor;
			if (dataGridView != null)
			{
				dataGridView.Click += value2;
				dataGridView.KeyDown += value3;
				dataGridView.KeyPress += value4;
				dataGridView.Scroll += value5;
			}
		}
	}

	internal virtual ComboBox CmbColor
	{
		[CompilerGenerated]
		get
		{
			return _CmbColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DrawItemEventHandler value2 = CmbColor_DrawItem;
			KeyPressEventHandler value3 = CmbColor_KeyPress;
			EventHandler value4 = CmbColor_Leave;
			EventHandler value5 = CmbColor_SelectedIndexChanged;
			ComboBox comboBox = _CmbColor;
			if (comboBox != null)
			{
				comboBox.DrawItem -= value2;
				comboBox.KeyPress -= value3;
				comboBox.Leave -= value4;
				comboBox.SelectedIndexChanged -= value5;
			}
			_CmbColor = value;
			comboBox = _CmbColor;
			if (comboBox != null)
			{
				comboBox.DrawItem += value2;
				comboBox.KeyPress += value3;
				comboBox.Leave += value4;
				comboBox.SelectedIndexChanged += value5;
			}
		}
	}

	[field: AccessedThroughProperty("ColAlias")]
	internal virtual DataGridViewTextBoxColumn ColAlias
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColColor")]
	internal virtual DataGridViewTextBoxColumn ColColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdClearAll
	{
		[CompilerGenerated]
		get
		{
			return _CmdClearAll;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdClearAll_Click;
			Button button = _CmdClearAll;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdClearAll = value;
			button = _CmdClearAll;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDefColor
	{
		[CompilerGenerated]
		get
		{
			return _CmdDefColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDefColor_Click;
			Button button = _CmdDefColor;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDefColor = value;
			button = _CmdDefColor;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CmbMidas")]
	internal virtual ComboBox CmbMidas
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblMidas")]
	internal virtual Label LblMidas
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabAdv")]
	internal virtual TabPage TabAdv
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblqDir")]
	internal virtual Label lblqDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdAssocTab
	{
		[CompilerGenerated]
		get
		{
			return _cmdAssocTab;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAssocTab_Click;
			Button button = _cmdAssocTab;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdAssocTab = value;
			button = _cmdAssocTab;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdQDir
	{
		[CompilerGenerated]
		get
		{
			return _cmdQDir;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdQDir_Click;
			Button button = _cmdQDir;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdQDir = value;
			button = _cmdQDir;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtPCDir")]
	internal virtual ComboBox TxtPCDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdIcon
	{
		[CompilerGenerated]
		get
		{
			return _cmdIcon;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_Click;
			Button button = _cmdIcon;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdIcon = value;
			button = _cmdIcon;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkOrclRetry")]
	internal virtual CheckBox chkOrclRetry
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual RadioButton radioR
	{
		[CompilerGenerated]
		get
		{
			return _radioR;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = radiojmp_CheckedChanged;
			RadioButton radioButton = _radioR;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_radioR = value;
			radioButton = _radioR;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	internal virtual RadioButton radiojmp
	{
		[CompilerGenerated]
		get
		{
			return _radiojmp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = radiojmp_CheckedChanged;
			RadioButton radioButton = _radiojmp;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_radiojmp = value;
			radioButton = _radiojmp;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	internal virtual RadioButton radioPy
	{
		[CompilerGenerated]
		get
		{
			return _radioPy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = radiojmp_CheckedChanged;
			RadioButton radioButton = _radioPy;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_radioPy = value;
			radioButton = _radioPy;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("radioXL")]
	internal virtual RadioButton radioXL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkpydebug")]
	internal virtual CheckBox chkpydebug
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkwebnext")]
	internal virtual CheckBox chkwebnext
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkMinPath")]
	internal virtual CheckBox chkMinPath
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual RadioButton radiopy3
	{
		[CompilerGenerated]
		get
		{
			return _radiopy3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = radiojmp_CheckedChanged;
			RadioButton radioButton = _radiopy3;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_radiopy3 = value;
			radioButton = _radiopy3;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chksqlnet")]
	internal virtual CheckBox chksqlnet
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtQDir")]
	internal virtual ComboBox TxtQDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbEmail")]
	internal virtual ComboBox cmbEmail
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblemail")]
	internal virtual Label lblemail
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkencode")]
	internal virtual CheckBox chkencode
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkencodeutfbom")]
	internal virtual CheckBox chkencodeutfbom
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkConvertMAOUber")]
	internal virtual CheckBox chkConvertMAOUber
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmconfigure()
	{
		base.Load += frmconfigure_Load;
		base.FormClosed += frmconfigure_FormClosed;
		base.Resize += frmconfigure_Resize;
		l_InPCDir = "";
		l_InSPFCmdWin = Globals_Renamed.gSPFCmdWin;
		f_row = 0;
		f_col = 0;
		CmbBoxKey = false;
		f_TrueRow = -1;
		l_jmp = "";
		l_R = "";
		l_Py = "";
		l_XL = "";
		l_Py3 = "";
		l_which = "JMP";
		l_WhichPath = "C:\\Program Files\\SAS\\";
		l_WhichPrompt = "Find the JMP Exe";
		lMyPyPath = Strings.Trim(General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PYTHON", MyProject.Application.Info.DirectoryPath + "\\Python\\python.exe", 512, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini"));
		lMyPyPath3 = Strings.Trim(General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PYTHON3", MyProject.Application.Info.DirectoryPath + "\\Python3\\python.exe", 512, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini"));
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.frmconfigure));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.lblPCDir = new System.Windows.Forms.Label();
		this.CmdDefColor = new System.Windows.Forms.Button();
		this.CmdClearAll = new System.Windows.Forms.Button();
		this.lblqDir = new System.Windows.Forms.Label();
		this.cmdAssocTab = new System.Windows.Forms.Button();
		this.cmdIcon = new System.Windows.Forms.Button();
		this.chkwebnext = new System.Windows.Forms.CheckBox();
		this.chkMinPath = new System.Windows.Forms.CheckBox();
		this.chksqlnet = new System.Windows.Forms.CheckBox();
		this.lblemail = new System.Windows.Forms.Label();
		this.chkencode = new System.Windows.Forms.CheckBox();
		this.chkencodeutfbom = new System.Windows.Forms.CheckBox();
		this.chkConvertMAOUber = new System.Windows.Forms.CheckBox();
		this.LblMidas = new System.Windows.Forms.Label();
		this.CmdBrowseJMP = new System.Windows.Forms.Button();
		this.CmdBrowseDir = new System.Windows.Forms.Button();
		this.TxtJMP = new System.Windows.Forms.ComboBox();
		this.CmdHelp = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.TabConfig = new System.Windows.Forms.TabControl();
		this.TabConfigData = new System.Windows.Forms.TabPage();
		this.TxtQDir = new System.Windows.Forms.ComboBox();
		this.radiopy3 = new System.Windows.Forms.RadioButton();
		this.radioXL = new System.Windows.Forms.RadioButton();
		this.radioPy = new System.Windows.Forms.RadioButton();
		this.radioR = new System.Windows.Forms.RadioButton();
		this.radiojmp = new System.Windows.Forms.RadioButton();
		this.TxtPCDir = new System.Windows.Forms.ComboBox();
		this.cmdQDir = new System.Windows.Forms.Button();
		this.CmbMidas = new System.Windows.Forms.ComboBox();
		this.TabConfigColor = new System.Windows.Forms.TabPage();
		this.CmbColor = new System.Windows.Forms.ComboBox();
		this.LblGridColor = new System.Windows.Forms.Label();
		this.GridColor = new System.Windows.Forms.DataGridView();
		this.ColAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TabAdv = new System.Windows.Forms.TabPage();
		this.cmbEmail = new System.Windows.Forms.ComboBox();
		this.chkpydebug = new System.Windows.Forms.CheckBox();
		this.chkOrclRetry = new System.Windows.Forms.CheckBox();
		this.TabConfig.SuspendLayout();
		this.TabConfigData.SuspendLayout();
		this.TabConfigColor.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridColor).BeginInit();
		this.TabAdv.SuspendLayout();
		base.SuspendLayout();
		this.lblPCDir.AutoSize = true;
		this.lblPCDir.BackColor = System.Drawing.Color.Transparent;
		this.lblPCDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblPCDir.Font = new System.Drawing.Font("Arial", 8.25f);
		this.lblPCDir.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblPCDir.Location = new System.Drawing.Point(6, 7);
		this.lblPCDir.Name = "lblPCDir";
		this.lblPCDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblPCDir.Size = new System.Drawing.Size(74, 16);
		this.lblPCDir.TabIndex = 11;
		this.lblPCDir.Text = "Work Path";
		this.ToolTip1.SetToolTip(this.lblPCDir, "Set work folder. Output will by default be written here");
		this.CmdDefColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDefColor.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdDefColor.Location = new System.Drawing.Point(415, 75);
		this.CmdDefColor.Name = "CmdDefColor";
		this.CmdDefColor.Size = new System.Drawing.Size(61, 35);
		this.CmdDefColor.TabIndex = 3;
		this.CmdDefColor.Text = "Default";
		this.ToolTip1.SetToolTip(this.CmdDefColor, "Assign Default Colors");
		this.CmdDefColor.UseVisualStyleBackColor = true;
		this.CmdClearAll.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdClearAll.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdClearAll.Location = new System.Drawing.Point(415, 36);
		this.CmdClearAll.Name = "CmdClearAll";
		this.CmdClearAll.Size = new System.Drawing.Size(61, 35);
		this.CmdClearAll.TabIndex = 2;
		this.CmdClearAll.Text = "Clear";
		this.ToolTip1.SetToolTip(this.CmdClearAll, "Clear Color Selections");
		this.CmdClearAll.UseVisualStyleBackColor = true;
		this.lblqDir.AutoSize = true;
		this.lblqDir.Location = new System.Drawing.Point(6, 72);
		this.lblqDir.Name = "lblqDir";
		this.lblqDir.Size = new System.Drawing.Size(80, 16);
		this.lblqDir.TabIndex = 21;
		this.lblqDir.Text = "Query Path";
		this.ToolTip1.SetToolTip(this.lblqDir, "Set Query Folder. Queries will by default be written here");
		this.cmdAssocTab.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdAssocTab.Location = new System.Drawing.Point(423, 385);
		this.cmdAssocTab.Name = "cmdAssocTab";
		this.cmdAssocTab.Size = new System.Drawing.Size(50, 34);
		this.cmdAssocTab.TabIndex = 17;
		this.cmdAssocTab.Text = ".tab";
		this.ToolTip1.SetToolTip(this.cmdAssocTab, "Associate .TAB Files with Excel");
		this.cmdAssocTab.UseVisualStyleBackColor = true;
		this.cmdIcon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdIcon.BackColor = System.Drawing.SystemColors.Control;
		this.cmdIcon.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdIcon.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdIcon.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdIcon.Location = new System.Drawing.Point(287, 385);
		this.cmdIcon.Name = "cmdIcon";
		this.cmdIcon.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdIcon.Size = new System.Drawing.Size(115, 34);
		this.cmdIcon.TabIndex = 16;
		this.cmdIcon.Text = "Icon/Associate";
		this.ToolTip1.SetToolTip(this.cmdIcon, "Create SQLPathFinder icon on desktop and associate vg2, vg, vge, vgec files with SQLPathFinder");
		this.cmdIcon.UseVisualStyleBackColor = false;
		this.chkwebnext.AutoSize = true;
		this.chkwebnext.Location = new System.Drawing.Point(6, 63);
		this.chkwebnext.Name = "chkwebnext";
		this.chkwebnext.Size = new System.Drawing.Size(191, 20);
		this.chkwebnext.TabIndex = 7;
		this.chkwebnext.Text = "Use Next Interactive Web ";
		this.ToolTip1.SetToolTip(this.chkwebnext, "Test use of Next Interactive Web Site");
		this.chkwebnext.UseVisualStyleBackColor = true;
		this.chkMinPath.AutoSize = true;
		this.chkMinPath.Location = new System.Drawing.Point(239, 103);
		this.chkMinPath.Name = "chkMinPath";
		this.chkMinPath.Size = new System.Drawing.Size(176, 20);
		this.chkMinPath.TabIndex = 11;
		this.chkMinPath.Text = "Minimize PATH Variable";
		this.ToolTip1.SetToolTip(this.chkMinPath, "Minimize the OS PATH Environment Variable");
		this.chkMinPath.UseVisualStyleBackColor = true;
		this.chksqlnet.AutoSize = true;
		this.chksqlnet.Location = new System.Drawing.Point(6, 103);
		this.chksqlnet.Name = "chksqlnet";
		this.chksqlnet.Size = new System.Drawing.Size(181, 20);
		this.chksqlnet.TabIndex = 12;
		this.chksqlnet.Text = "Use SPF Query Window";
		this.ToolTip1.SetToolTip(this.chksqlnet, "Use SPF's vs. Window's Command Window");
		this.chksqlnet.UseVisualStyleBackColor = true;
		this.lblemail.AutoSize = true;
		this.lblemail.Location = new System.Drawing.Point(6, 143);
		this.lblemail.Name = "lblemail";
		this.lblemail.Size = new System.Drawing.Size(92, 16);
		this.lblemail.TabIndex = 15;
		this.lblemail.Text = "Email Option:";
		this.ToolTip1.SetToolTip(this.lblemail, "Email Option for Interactive Queries");
		this.chkencode.AutoSize = true;
		this.chkencode.Location = new System.Drawing.Point(239, 162);
		this.chkencode.Name = "chkencode";
		this.chkencode.Size = new System.Drawing.Size(157, 20);
		this.chkencode.TabIndex = 16;
		this.chkencode.Text = "Full Scan to Encode";
		this.ToolTip1.SetToolTip(this.chkencode, "Scans entire source files to determine data encoding");
		this.chkencode.UseVisualStyleBackColor = true;
		this.chkencodeutfbom.AutoSize = true;
		this.chkencodeutfbom.Location = new System.Drawing.Point(6, 203);
		this.chkencodeutfbom.Name = "chkencodeutfbom";
		this.chkencodeutfbom.Size = new System.Drawing.Size(183, 20);
		this.chkencodeutfbom.TabIndex = 17;
		this.chkencodeutfbom.Text = "Set Encode UTF-8-BOM";
		this.ToolTip1.SetToolTip(this.chkencodeutfbom, "Set ext engine encoding to UTF-8-BOM");
		this.chkencodeutfbom.UseVisualStyleBackColor = true;
		this.chkConvertMAOUber.AutoSize = true;
		this.chkConvertMAOUber.Location = new System.Drawing.Point(239, 23);
		this.chkConvertMAOUber.Name = "chkConvertMAOUber";
		this.chkConvertMAOUber.Size = new System.Drawing.Size(216, 20);
		this.chkConvertMAOUber.TabIndex = 21;
		this.chkConvertMAOUber.Text = "Convert MAO to UBER Nodes";
		this.ToolTip1.SetToolTip(this.chkConvertMAOUber, "Check to convert MAO to UBER nodes where possible");
		this.chkConvertMAOUber.UseVisualStyleBackColor = true;
		this.LblMidas.AutoSize = true;
		this.LblMidas.Location = new System.Drawing.Point(6, 238);
		this.LblMidas.Name = "LblMidas";
		this.LblMidas.Size = new System.Drawing.Size(92, 16);
		this.LblMidas.TabIndex = 18;
		this.LblMidas.Text = "MIDAS Driver";
		this.CmdBrowseJMP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowseJMP.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowseJMP.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowseJMP.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowseJMP.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowseJMP.Location = new System.Drawing.Point(443, 170);
		this.CmdBrowseJMP.Name = "CmdBrowseJMP";
		this.CmdBrowseJMP.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowseJMP.Size = new System.Drawing.Size(34, 23);
		this.CmdBrowseJMP.TabIndex = 10;
		this.CmdBrowseJMP.Text = "...";
		this.CmdBrowseJMP.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdBrowseJMP.UseVisualStyleBackColor = false;
		this.CmdBrowseDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowseDir.BackColor = System.Drawing.SystemColors.Control;
		this.CmdBrowseDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdBrowseDir.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdBrowseDir.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdBrowseDir.Location = new System.Drawing.Point(443, 22);
		this.CmdBrowseDir.Name = "CmdBrowseDir";
		this.CmdBrowseDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdBrowseDir.Size = new System.Drawing.Size(34, 23);
		this.CmdBrowseDir.TabIndex = 2;
		this.CmdBrowseDir.Text = "...";
		this.CmdBrowseDir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdBrowseDir.UseVisualStyleBackColor = false;
		this.TxtJMP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtJMP.BackColor = System.Drawing.SystemColors.Window;
		this.TxtJMP.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtJMP.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtJMP.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtJMP.Location = new System.Drawing.Point(6, 170);
		this.TxtJMP.Name = "TxtJMP";
		this.TxtJMP.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtJMP.Size = new System.Drawing.Size(430, 24);
		this.TxtJMP.TabIndex = 9;
		this.CmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.CmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdHelp.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdHelp.Location = new System.Drawing.Point(140, 410);
		this.CmdHelp.Name = "CmdHelp";
		this.CmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdHelp.Size = new System.Drawing.Size(60, 34);
		this.CmdHelp.TabIndex = 15;
		this.CmdHelp.Text = "Help";
		this.CmdHelp.UseVisualStyleBackColor = false;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCancel.Location = new System.Drawing.Point(73, 410);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCancel.Size = new System.Drawing.Size(60, 34);
		this.CmdCancel.TabIndex = 14;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = false;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdOK.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdOK.Location = new System.Drawing.Point(6, 410);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdOK.Size = new System.Drawing.Size(60, 34);
		this.CmdOK.TabIndex = 13;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = false;
		this.TabConfig.Controls.Add(this.TabConfigData);
		this.TabConfig.Controls.Add(this.TabConfigColor);
		this.TabConfig.Controls.Add(this.TabAdv);
		this.TabConfig.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabConfig.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TabConfig.Location = new System.Drawing.Point(0, 0);
		this.TabConfig.Name = "TabConfig";
		this.TabConfig.SelectedIndex = 0;
		this.TabConfig.Size = new System.Drawing.Size(489, 468);
		this.TabConfig.TabIndex = 0;
		this.TabConfigData.Controls.Add(this.TxtQDir);
		this.TabConfigData.Controls.Add(this.radiopy3);
		this.TabConfigData.Controls.Add(this.radioXL);
		this.TabConfigData.Controls.Add(this.radioPy);
		this.TabConfigData.Controls.Add(this.radioR);
		this.TabConfigData.Controls.Add(this.radiojmp);
		this.TabConfigData.Controls.Add(this.cmdIcon);
		this.TabConfigData.Controls.Add(this.TxtPCDir);
		this.TabConfigData.Controls.Add(this.cmdQDir);
		this.TabConfigData.Controls.Add(this.cmdAssocTab);
		this.TabConfigData.Controls.Add(this.lblqDir);
		this.TabConfigData.Controls.Add(this.CmbMidas);
		this.TabConfigData.Controls.Add(this.LblMidas);
		this.TabConfigData.Controls.Add(this.lblPCDir);
		this.TabConfigData.Controls.Add(this.CmdBrowseJMP);
		this.TabConfigData.Controls.Add(this.CmdBrowseDir);
		this.TabConfigData.Controls.Add(this.TxtJMP);
		this.TabConfigData.Location = new System.Drawing.Point(4, 25);
		this.TabConfigData.Name = "TabConfigData";
		this.TabConfigData.Padding = new System.Windows.Forms.Padding(3);
		this.TabConfigData.Size = new System.Drawing.Size(481, 439);
		this.TabConfigData.TabIndex = 0;
		this.TabConfigData.Text = "Paths/Drivers";
		this.TabConfigData.UseVisualStyleBackColor = true;
		this.TxtQDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtQDir.FormattingEnabled = true;
		this.TxtQDir.Location = new System.Drawing.Point(6, 91);
		this.TxtQDir.Name = "TxtQDir";
		this.TxtQDir.Size = new System.Drawing.Size(430, 24);
		this.TxtQDir.TabIndex = 23;
		this.radiopy3.AutoSize = true;
		this.radiopy3.Location = new System.Drawing.Point(180, 142);
		this.radiopy3.Name = "radiopy3";
		this.radiopy3.Size = new System.Drawing.Size(85, 20);
		this.radiopy3.TabIndex = 22;
		this.radiopy3.TabStop = true;
		this.radiopy3.Text = "Python 3";
		this.radiopy3.UseVisualStyleBackColor = true;
		this.radioXL.AutoSize = true;
		this.radioXL.Location = new System.Drawing.Point(336, 142);
		this.radioXL.Name = "radioXL";
		this.radioXL.Size = new System.Drawing.Size(95, 20);
		this.radioXL.TabIndex = 8;
		this.radioXL.TabStop = true;
		this.radioXL.Text = "Excel Path";
		this.radioXL.UseVisualStyleBackColor = true;
		this.radioPy.AutoSize = true;
		this.radioPy.Location = new System.Drawing.Point(76, 142);
		this.radioPy.Name = "radioPy";
		this.radioPy.Size = new System.Drawing.Size(85, 20);
		this.radioPy.TabIndex = 6;
		this.radioPy.TabStop = true;
		this.radioPy.Text = "Python 2";
		this.radioPy.UseVisualStyleBackColor = true;
		this.radioR.AutoSize = true;
		this.radioR.Location = new System.Drawing.Point(280, 142);
		this.radioR.Name = "radioR";
		this.radioR.Size = new System.Drawing.Size(39, 20);
		this.radioR.TabIndex = 7;
		this.radioR.Text = "R";
		this.radioR.UseVisualStyleBackColor = true;
		this.radiojmp.AutoSize = true;
		this.radiojmp.Checked = true;
		this.radiojmp.Location = new System.Drawing.Point(6, 142);
		this.radiojmp.Name = "radiojmp";
		this.radiojmp.Size = new System.Drawing.Size(55, 20);
		this.radiojmp.TabIndex = 5;
		this.radiojmp.TabStop = true;
		this.radiojmp.Text = "JMP";
		this.radiojmp.UseVisualStyleBackColor = true;
		this.TxtPCDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPCDir.FormattingEnabled = true;
		this.TxtPCDir.Location = new System.Drawing.Point(6, 23);
		this.TxtPCDir.Name = "TxtPCDir";
		this.TxtPCDir.Size = new System.Drawing.Size(430, 24);
		this.TxtPCDir.TabIndex = 1;
		this.cmdQDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdQDir.Location = new System.Drawing.Point(443, 90);
		this.cmdQDir.Name = "cmdQDir";
		this.cmdQDir.Size = new System.Drawing.Size(34, 23);
		this.cmdQDir.TabIndex = 4;
		this.cmdQDir.Text = "...";
		this.cmdQDir.UseVisualStyleBackColor = true;
		this.CmbMidas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmbMidas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbMidas.FormattingEnabled = true;
		this.CmbMidas.Location = new System.Drawing.Point(6, 257);
		this.CmbMidas.Name = "CmbMidas";
		this.CmbMidas.Size = new System.Drawing.Size(430, 24);
		this.CmbMidas.TabIndex = 12;
		this.TabConfigColor.Controls.Add(this.CmdDefColor);
		this.TabConfigColor.Controls.Add(this.CmdClearAll);
		this.TabConfigColor.Controls.Add(this.CmbColor);
		this.TabConfigColor.Controls.Add(this.LblGridColor);
		this.TabConfigColor.Controls.Add(this.GridColor);
		this.TabConfigColor.Location = new System.Drawing.Point(4, 25);
		this.TabConfigColor.Name = "TabConfigColor";
		this.TabConfigColor.Padding = new System.Windows.Forms.Padding(3);
		this.TabConfigColor.Size = new System.Drawing.Size(481, 439);
		this.TabConfigColor.TabIndex = 1;
		this.TabConfigColor.Text = "Colors";
		this.TabConfigColor.UseVisualStyleBackColor = true;
		this.CmbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.CmbColor.DropDownWidth = 200;
		this.CmbColor.FormattingEnabled = true;
		this.CmbColor.Location = new System.Drawing.Point(47, 154);
		this.CmbColor.Name = "CmbColor";
		this.CmbColor.Size = new System.Drawing.Size(165, 23);
		this.CmbColor.TabIndex = 2;
		this.CmbColor.Visible = false;
		this.LblGridColor.AutoSize = true;
		this.LblGridColor.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblGridColor.Location = new System.Drawing.Point(4, 14);
		this.LblGridColor.Name = "LblGridColor";
		this.LblGridColor.Size = new System.Drawing.Size(246, 16);
		this.LblGridColor.TabIndex = 1;
		this.LblGridColor.Text = "Set Background color for View Aliases";
		this.GridColor.AllowUserToAddRows = false;
		this.GridColor.AllowUserToDeleteRows = false;
		this.GridColor.AllowUserToResizeColumns = false;
		this.GridColor.AllowUserToResizeRows = false;
		this.GridColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridColor.BackgroundColor = System.Drawing.Color.White;
		this.GridColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.GridColor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.GridColor.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridColor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridColor.Columns.AddRange(this.ColAlias, this.ColColor);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.GridColor.DefaultCellStyle = dataGridViewCellStyle2;
		this.GridColor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.GridColor.Location = new System.Drawing.Point(4, 36);
		this.GridColor.MultiSelect = false;
		this.GridColor.Name = "GridColor";
		this.GridColor.ReadOnly = true;
		this.GridColor.RowHeadersWidth = 50;
		this.GridColor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridColor.RowTemplate.Height = 24;
		this.GridColor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.GridColor.Size = new System.Drawing.Size(405, 273);
		this.GridColor.StandardTab = true;
		this.GridColor.TabIndex = 1;
		this.ColAlias.HeaderText = "View Aliases";
		this.ColAlias.MinimumWidth = 6;
		this.ColAlias.Name = "ColAlias";
		this.ColAlias.ReadOnly = true;
		this.ColAlias.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColAlias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColAlias.Width = 140;
		this.ColColor.HeaderText = "Back Color";
		this.ColColor.MinimumWidth = 6;
		this.ColColor.Name = "ColColor";
		this.ColColor.ReadOnly = true;
		this.ColColor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColColor.Width = 140;
		this.TabAdv.Controls.Add(this.chkConvertMAOUber);
		this.TabAdv.Controls.Add(this.chkencodeutfbom);
		this.TabAdv.Controls.Add(this.chkencode);
		this.TabAdv.Controls.Add(this.lblemail);
		this.TabAdv.Controls.Add(this.cmbEmail);
		this.TabAdv.Controls.Add(this.chksqlnet);
		this.TabAdv.Controls.Add(this.chkMinPath);
		this.TabAdv.Controls.Add(this.chkpydebug);
		this.TabAdv.Controls.Add(this.chkwebnext);
		this.TabAdv.Controls.Add(this.chkOrclRetry);
		this.TabAdv.Location = new System.Drawing.Point(4, 25);
		this.TabAdv.Name = "TabAdv";
		this.TabAdv.Size = new System.Drawing.Size(481, 439);
		this.TabAdv.TabIndex = 2;
		this.TabAdv.Text = "Advanced";
		this.TabAdv.UseVisualStyleBackColor = true;
		this.cmbEmail.BackColor = System.Drawing.Color.WhiteSmoke;
		this.cmbEmail.FormattingEnabled = true;
		this.cmbEmail.Items.AddRange(new object[3] { "SMTP Auth", "Outlook", "SMTP" });
		this.cmbEmail.Location = new System.Drawing.Point(6, 162);
		this.cmbEmail.Name = "cmbEmail";
		this.cmbEmail.Size = new System.Drawing.Size(166, 24);
		this.cmbEmail.TabIndex = 14;
		this.cmbEmail.Text = "SMTP Auth";
		this.chkpydebug.AutoSize = true;
		this.chkpydebug.Location = new System.Drawing.Point(239, 63);
		this.chkpydebug.Name = "chkpydebug";
		this.chkpydebug.Size = new System.Drawing.Size(111, 20);
		this.chkpydebug.TabIndex = 8;
		this.chkpydebug.Text = "Debug Mode";
		this.chkpydebug.UseVisualStyleBackColor = true;
		this.chkOrclRetry.AutoSize = true;
		this.chkOrclRetry.Location = new System.Drawing.Point(6, 23);
		this.chkOrclRetry.Name = "chkOrclRetry";
		this.chkOrclRetry.Size = new System.Drawing.Size(190, 20);
		this.chkOrclRetry.TabIndex = 5;
		this.chkOrclRetry.Text = "Retry Oracle connections";
		this.chkOrclRetry.UseVisualStyleBackColor = true;
		base.AcceptButton = this.CmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(489, 468);
		base.Controls.Add(this.CmdHelp);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.TabConfig);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.WindowText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(518, 135);
		base.Name = "frmconfigure";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Configure SQLPathFinder";
		this.TabConfig.ResumeLayout(false);
		this.TabConfigData.ResumeLayout(false);
		this.TabConfigData.PerformLayout();
		this.TabConfigColor.ResumeLayout(false);
		this.TabConfigColor.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridColor).EndInit();
		this.TabAdv.ResumeLayout(false);
		this.TabAdv.PerformLayout();
		base.ResumeLayout(false);
	}

	public void DoRadio(string MyMode)
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
					goto IL_000c;
				case 1619:
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
							goto IL_000c;
						case 4:
							goto IL_003a;
						case 5:
							goto IL_004d;
						case 7:
							goto IL_0066;
						case 8:
							goto IL_0079;
						case 10:
							goto IL_008f;
						case 11:
							goto IL_00a3;
						case 13:
							goto IL_00ba;
						case 14:
							goto IL_00ce;
						case 16:
							goto IL_00e5;
						case 17:
							goto IL_00f9;
						case 20:
							goto IL_0114;
						case 21:
							goto IL_0131;
						case 23:
							goto IL_014b;
						case 24:
							goto IL_0168;
						case 26:
							goto IL_0182;
						case 27:
							goto IL_019f;
						case 29:
							goto IL_01b6;
						case 30:
							goto IL_01d3;
						case 32:
							goto IL_01ea;
						case 33:
							goto IL_0207;
						case 22:
						case 25:
						case 28:
						case 31:
						case 34:
						case 35:
							goto IL_021c;
						case 36:
							goto IL_0230;
						case 37:
							goto IL_0247;
						case 38:
							goto IL_025c;
						case 39:
							goto IL_026a;
						case 40:
							goto IL_0278;
						case 41:
							goto IL_0286;
						case 42:
							goto IL_029f;
						case 43:
							goto IL_02b8;
						case 45:
							goto IL_02d7;
						case 46:
							goto IL_02eb;
						case 47:
							goto IL_0300;
						case 48:
							goto IL_030e;
						case 49:
							goto IL_031c;
						case 50:
							goto IL_032a;
						case 52:
							goto IL_035d;
						case 53:
							goto IL_0371;
						case 54:
							goto IL_0386;
						case 55:
							goto IL_0394;
						case 56:
							goto IL_03a2;
						case 57:
							goto IL_03b0;
						case 59:
							goto IL_03e3;
						case 60:
							goto IL_03f7;
						case 61:
							goto IL_040c;
						case 62:
							goto IL_041a;
						case 63:
							goto IL_0428;
						case 64:
							goto IL_0436;
						case 66:
							goto IL_0469;
						case 67:
							goto IL_0480;
						case 68:
							goto IL_0495;
						case 69:
							goto IL_04a3;
						case 70:
							goto IL_04b1;
						case 71:
							goto IL_04bf;
						case 72:
							goto IL_04d8;
						case 73:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 9:
						case 12:
						case 15:
						case 18:
						case 19:
						case 44:
						case 51:
						case 58:
						case 65:
						case 74:
						case 75:
						case 76:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ba:
					num2 = 13;
					if (radiopy3.Checked)
					{
						goto IL_00ce;
					}
					goto IL_00e5;
					IL_00ce:
					num2 = 14;
					l_Py3 = TxtJMP.Text;
					goto end_IL_0001_3;
					IL_00a3:
					num2 = 11;
					l_Py = TxtJMP.Text;
					goto end_IL_0001_3;
					IL_00e5:
					num2 = 16;
					if (!radioXL.Checked)
					{
						goto end_IL_0001_3;
					}
					goto IL_00f9;
					IL_000c:
					num2 = 2;
					left = Strings.UCase(MyMode);
					if (Operators.CompareString(left, "GET", TextCompare: false) == 0)
					{
						goto IL_003a;
					}
					if (Operators.CompareString(left, "SET", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0114;
					IL_00f9:
					num2 = 17;
					l_XL = TxtJMP.Text;
					goto end_IL_0001_3;
					IL_0114:
					num2 = 20;
					if (Operators.CompareString(l_which, "JMP", TextCompare: false) == 0)
					{
						goto IL_0131;
					}
					goto IL_014b;
					IL_0131:
					num2 = 21;
					l_jmp = TxtJMP.Text;
					goto IL_021c;
					IL_014b:
					num2 = 23;
					if (Operators.CompareString(l_which, "R", TextCompare: false) == 0)
					{
						goto IL_0168;
					}
					goto IL_0182;
					IL_0168:
					num2 = 24;
					l_R = TxtJMP.Text;
					goto IL_021c;
					IL_0182:
					num2 = 26;
					if (Operators.CompareString(l_which, "PY", TextCompare: false) == 0)
					{
						goto IL_019f;
					}
					goto IL_01b6;
					IL_019f:
					num2 = 27;
					l_Py = TxtJMP.Text;
					goto IL_021c;
					IL_01b6:
					num2 = 29;
					if (Operators.CompareString(l_which, "PY3", TextCompare: false) == 0)
					{
						goto IL_01d3;
					}
					goto IL_01ea;
					IL_01d3:
					num2 = 30;
					l_Py3 = TxtJMP.Text;
					goto IL_021c;
					IL_01ea:
					num2 = 32;
					if (Operators.CompareString(l_which, "XL", TextCompare: false) == 0)
					{
						goto IL_0207;
					}
					goto IL_021c;
					IL_0207:
					num2 = 33;
					l_XL = TxtJMP.Text;
					goto IL_021c;
					IL_021c:
					num2 = 35;
					TxtJMP.Items.Clear();
					goto IL_0230;
					IL_0230:
					num2 = 36;
					if (radiojmp.Checked)
					{
						goto IL_0247;
					}
					goto IL_02d7;
					IL_0247:
					num2 = 37;
					TxtJMP.Text = l_jmp;
					goto IL_025c;
					IL_025c:
					num2 = 38;
					l_which = "JMP";
					goto IL_026a;
					IL_026a:
					num2 = 39;
					l_WhichPrompt = "Find the JMP Exe";
					goto IL_0278;
					IL_0278:
					num2 = 40;
					l_WhichPath = "C:\\Program Files\\SAS\\";
					goto IL_0286;
					IL_0286:
					num2 = 41;
					TxtJMP.Items.Add("C:\\Program Files\\SAS\\JMPPRO\\12\\jmp.exe");
					goto IL_029f;
					IL_029f:
					num2 = 42;
					TxtJMP.Items.Add("C:\\Program Files\\SAS\\JMPPRO\\14\\jmp.exe");
					goto IL_02b8;
					IL_02b8:
					num2 = 43;
					TxtJMP.Items.Add("C:\\Program Files\\SAS\\JMPPRO\\17\\jmp.exe");
					goto end_IL_0001_3;
					IL_02d7:
					num2 = 45;
					if (radioR.Checked)
					{
						goto IL_02eb;
					}
					goto IL_035d;
					IL_02eb:
					num2 = 46;
					TxtJMP.Text = l_R;
					goto IL_0300;
					IL_0300:
					num2 = 47;
					l_which = "R";
					goto IL_030e;
					IL_030e:
					num2 = 48;
					l_WhichPrompt = "Find the R RTerm.exe Exe";
					goto IL_031c;
					IL_031c:
					num2 = 49;
					l_WhichPath = "C:\\Program Files\\R\\";
					goto IL_032a;
					IL_032a:
					num2 = 50;
					TxtJMP.Items.Add(MyProject.Application.Info.DirectoryPath + "\\R\\R-Latest\\bin\\x64\\RTerm.exe");
					goto end_IL_0001_3;
					IL_035d:
					num2 = 52;
					if (radioPy.Checked)
					{
						goto IL_0371;
					}
					goto IL_03e3;
					IL_0371:
					num2 = 53;
					TxtJMP.Text = l_Py;
					goto IL_0386;
					IL_0386:
					num2 = 54;
					l_which = "PY";
					goto IL_0394;
					IL_0394:
					num2 = 55;
					l_WhichPrompt = "Find the Python-2 Exe";
					goto IL_03a2;
					IL_03a2:
					num2 = 56;
					l_WhichPath = "C:\\";
					goto IL_03b0;
					IL_03b0:
					num2 = 57;
					TxtJMP.Items.Add(MyProject.Application.Info.DirectoryPath + "\\Python\\python.exe");
					goto end_IL_0001_3;
					IL_03e3:
					num2 = 59;
					if (radiopy3.Checked)
					{
						goto IL_03f7;
					}
					goto IL_0469;
					IL_03f7:
					num2 = 60;
					TxtJMP.Text = l_Py3;
					goto IL_040c;
					IL_040c:
					num2 = 61;
					l_which = "PY3";
					goto IL_041a;
					IL_041a:
					num2 = 62;
					l_WhichPrompt = "Find the Python-3 Exe";
					goto IL_0428;
					IL_0428:
					num2 = 63;
					l_WhichPath = "C:\\";
					goto IL_0436;
					IL_0436:
					num2 = 64;
					TxtJMP.Items.Add(MyProject.Application.Info.DirectoryPath + "\\Python3\\python.exe");
					goto end_IL_0001_3;
					IL_0469:
					num2 = 66;
					if (!radioXL.Checked)
					{
						goto end_IL_0001_3;
					}
					goto IL_0480;
					IL_0480:
					num2 = 67;
					TxtJMP.Text = l_XL;
					goto IL_0495;
					IL_0495:
					num2 = 68;
					l_which = "XL";
					goto IL_04a3;
					IL_04a3:
					num2 = 69;
					l_WhichPrompt = "Find the Excel Exe";
					goto IL_04b1;
					IL_04b1:
					num2 = 70;
					l_WhichPath = "C:\\Program Files\\Microsoft Office\\root";
					goto IL_04bf;
					IL_04bf:
					num2 = 71;
					TxtJMP.Items.Add("Automatic");
					goto IL_04d8;
					IL_04d8:
					num2 = 72;
					TxtJMP.Items.Add("C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.EXE");
					break;
					IL_003a:
					num2 = 4;
					if (radiojmp.Checked)
					{
						goto IL_004d;
					}
					goto IL_0066;
					IL_004d:
					num2 = 5;
					l_jmp = TxtJMP.Text;
					goto end_IL_0001_3;
					IL_0066:
					num2 = 7;
					if (radioR.Checked)
					{
						goto IL_0079;
					}
					goto IL_008f;
					IL_0079:
					num2 = 8;
					l_R = TxtJMP.Text;
					goto end_IL_0001_3;
					IL_008f:
					num2 = 10;
					if (radioPy.Checked)
					{
						goto IL_00a3;
					}
					goto IL_00ba;
					end_IL_0001_2:
					break;
				}
				num2 = 73;
				TxtJMP.Items.Add("C:\\Program Files (x86)\\Microsoft Office\\root\\Office16\\EXCEL.EXE");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1619;
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

	public void Resize_Form()
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
				case 163:
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
							goto IL_001d;
						case 5:
							goto IL_003f;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					GridColor.Focus();
					goto IL_001d;
					IL_001d:
					num2 = 4;
					num5 = checked((int)Math.Round((double)(GridColor.Width - 70) / 3.0));
					goto IL_003f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_003f:
					num2 = 5;
					GridColor.Columns[0].Width = num5;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				GridColor.Columns[1].Width = checked(num5 * 2);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 163;
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

	public bool Oracle_64_bit_Drv(string MyDriver)
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
							goto IL_001f;
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
					if (!LikeOperator.LikeString(MyDriver, "SQLPlus (64 bit*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_001f;
					IL_001f:
					num2 = 3;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = false;
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
		return result;
	}

	public bool Oracle_64_bit_Exe()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool result = default(bool);
		string directory = default(string);
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
				case 121:
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
							goto IL_002c;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					directory = MyProject.Application.Info.DirectoryPath + "\\oracle\\instantclient_19_17";
					goto IL_002c;
					IL_002c:
					num2 = 4;
					if (!MyProject.Computer.FileSystem.DirectoryExists(directory))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 121;
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

	public string Find_Excel()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
							goto IL_0013;
						case 4:
							goto IL_0034;
						case 5:
							goto IL_005e;
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
					BuildForm.FileOpenSave("O", "", "exe", "Find Excel", "C:\\Program Files (x86)\\Microsoft Office");
					goto IL_0034;
					IL_0034:
					num2 = 4;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_005e;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_005e:
					num2 = 5;
					result = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				MyProject.Forms.FrmMain.CMDialog1Open.FileName = "";
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
		return result;
	}

	private void CmdBrowseDir_Click(object eventSender, EventArgs eventArgs)
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
				case 201:
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
							goto IL_0027;
						case 4:
							goto IL_0049;
						case 5:
							goto IL_0066;
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
					IL_0027:
					num2 = 3;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = TxtPCDir.Text;
					goto IL_0049;
					IL_0049:
					num2 = 4;
					if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() != DialogResult.OK)
					{
						break;
					}
					goto IL_0066;
					IL_000b:
					num2 = 2;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Locate a Work Directory";
					goto IL_0027;
					IL_0066:
					num2 = 5;
					TxtPCDir.Text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 201;
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Configuring");
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
		string text = default(string);
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string name;
					DataGridView MyGrid;
					string text2;
					ref string lpString;
					string lpFileName;
					string lpKeyName;
					ref string lpString2;
					string lpApplicationName;
					int num5;
					switch (try0001_dispatch)
					{
					default:
					{
						short num3 = 0;
						text = Strings.Trim(TxtPCDir.Text);
						name = Strings.Trim(TxtQDir.Text);
						short num4 = 0;
						num5 = 0;
						text2 = "";
						bool flag = false;
						bool flag2 = false;
						ProjectData.ClearProjectError();
						num2 = 2;
						string text3 = "frmConfigure - CmdOK_Click";
						Cursor.Current = Cursors.WaitCursor;
						text = Environment.ExpandEnvironmentVariables(text);
						name = Environment.ExpandEnvironmentVariables(name);
						if (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text, "\\", TextCompare: false) == 0)
						{
							Interaction.MsgBox("Do Set additional configuration options. Make sure you specified a valid work folder", MsgBoxStyle.Critical, "Configuration Data Missing");
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						if (Operators.CompareString(text.Substring(text.Length - 1), "\\", TextCompare: false) == 0)
						{
							text = Strings.Mid(text, 1, Strings.Len(text) - 1);
						}
						if (Operators.CompareString(text.ToUpper(), MyProject.Application.Info.DirectoryPath.ToUpper(), TextCompare: false) == 0)
						{
							Interaction.MsgBox("You should Not Set your work folder To be the same As your SQLPathFinder install directory.", MsgBoxStyle.Critical, "Invalid Path");
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						if (Operators.CompareString(name, "", TextCompare: false) != 0 && !General_Procedures.MakeDirectory(name, DoQuiet: true))
						{
							Interaction.MsgBox("Note that your Query Folder does Not exist Or could Not be created. Please change.", MsgBoxStyle.Critical, "Invalid Path");
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						if (!General_Procedures.MakeDirectory(text, DoQuiet: true))
						{
							Interaction.MsgBox("Note that your Work Folder does Not exist Or could Not be created. Please change.", MsgBoxStyle.Critical, "Invalid Path");
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						if (!Oracle_64_bit_Exe())
						{
							Interaction.MsgBox("You must install the Oracle 64 bit client driver. To install the Oracle drivers, Choose menu Option \"Tools -> Update Oracle Drivers\"", MsgBoxStyle.Exclamation, "Missing Oracle 64 bit Driver");
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						if (chksqlnet.Checked)
						{
							Globals_Renamed.gSPFCmdWin = "Y";
						}
						else
						{
							Globals_Renamed.gSPFCmdWin = "N";
						}
						if (Operators.CompareString(l_InSPFCmdWin, Globals_Renamed.gSPFCmdWin, TextCompare: false) != 0)
						{
							flag = true;
						}
						Globals_Renamed.MyPCDir = text;
						if (Operators.CompareString(Globals_Renamed.MyPCDir.Substring(Globals_Renamed.MyPCDir.Length - 1), "\\", TextCompare: false) != 0)
						{
							Globals_Renamed.MyPCDir += "\\";
						}
						if (Operators.CompareString(l_InPCDir, Strings.UCase(Strings.Trim(Globals_Renamed.MyPCDir)), TextCompare: false) != 0)
						{
							flag2 = true;
						}
						if (((Operators.CompareString(l_InSPFCmdWin, "N", TextCompare: false) != 0 || Globals_Renamed.MSAccess_Connected != 1) && (Operators.CompareString(l_InSPFCmdWin, "Y", TextCompare: false) != 0 || MyProject.Forms.FrmMain.FrmCmdSimf == null || MyProject.Forms.FrmMain.FrmCmdSimf.IsDisposed)) || (!flag2 && !flag))
						{
							goto IL_033a;
						}
						num5 = unchecked((int)Interaction.MsgBox("Whenever you change your work path Or switch To a New type Of Query Window, you must restart your SQL Session. This will cancel any jobs that are currently running. Shall SQLPathFinder close the SQL session Or Return you To the Configuration form?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Close SQL Session?"));
						if (num5 == 6)
						{
							BuildForm.Close_SQL_Emulator(l_InSPFCmdWin);
							goto IL_033a;
						}
						Globals_Renamed.MyPCDir = l_InPCDir;
						Globals_Renamed.gSPFCmdWin = l_InSPFCmdWin;
						goto end_IL_0001_2;
					}
					case 1981:
						{
							num = -1;
							switch (num2)
							{
							case 2:
							case 3:
							case 4:
								Cursor.Current = Cursors.Default;
								Interaction.MsgBox("You specified an invalid work folder, (" + text + "). " + Conversion.ErrorToString() + ".", MsgBoxStyle.Critical, "Invalid Path");
								Information.Err().Clear();
								goto end_IL_0001_2;
							}
							break;
						}
						IL_033a:
						ProjectData.ClearProjectError();
						num2 = 3;
						ProjectData.ClearProjectError();
						num2 = 4;
						FileSystem.ChDrive(text);
						FileSystem.ChDir(text);
						DoRadio("Get");
						Globals_Renamed.MyJMPPath = l_jmp;
						Globals_Renamed.gMyRPath = l_R;
						lMyPyPath = l_Py;
						lMyPyPath3 = l_Py3;
						Globals_Renamed.MyExcelPath = l_XL;
						Globals_Renamed.gQueryDir = name;
						if (Operators.CompareString(Globals_Renamed.gQueryDir, "", TextCompare: false) == 0)
						{
							Globals_Renamed.gQueryDir = Globals_Renamed.MyPCDir;
						}
						else if (Operators.CompareString(Globals_Renamed.gQueryDir.Substring(Globals_Renamed.gQueryDir.Length - 1), "\\", TextCompare: false) != 0)
						{
							Globals_Renamed.gQueryDir += "\\";
						}
						Globals_Renamed.gMidasDriver = CmbMidas.Text;
						if (Operators.CompareString(Strings.Trim(Strings.UCase(cmbEmail.Text)), "OUTLOOK", TextCompare: false) == 0)
						{
							Globals_Renamed.gEmailOutlook = "Y";
						}
						else if (Operators.CompareString(Strings.Trim(Strings.UCase(cmbEmail.Text)), "SMTP", TextCompare: false) == 0)
						{
							Globals_Renamed.gEmailOutlook = "S";
						}
						else
						{
							Globals_Renamed.gEmailOutlook = "N";
						}
						if (chkOrclRetry.Checked)
						{
							Globals_Renamed.gOrclRetry = "Y";
						}
						else
						{
							Globals_Renamed.gOrclRetry = "N";
						}
						if (chkpydebug.Checked)
						{
							Globals_Renamed.gPyDebug = "Y";
						}
						else
						{
							Globals_Renamed.gPyDebug = "N";
						}
						if (chkMinPath.Checked)
						{
							Globals_Renamed.gUseMinPath = "Y";
						}
						else
						{
							Globals_Renamed.gUseMinPath = "N";
						}
						if (chkwebnext.Checked)
						{
							Globals_Renamed.gWebNext = "Y";
						}
						else
						{
							Globals_Renamed.gWebNext = "N";
						}
						if (chkencode.Checked)
						{
							Globals_Renamed.gEncodeFFS = "Y";
						}
						else
						{
							Globals_Renamed.gEncodeFFS = "N";
						}
						if (chkencodeutfbom.Checked)
						{
							Globals_Renamed.gEncodeUTFBOM = "Y";
						}
						else
						{
							Globals_Renamed.gEncodeUTFBOM = "N";
						}
						if (chkConvertMAOUber.Checked)
						{
							Globals_Renamed.gConvertMAOUber = "Y";
						}
						else
						{
							Globals_Renamed.gConvertMAOUber = "N";
						}
						text2 = MyProject.Application.Info.DirectoryPath;
						if (Operators.CompareString(Strings.Mid(text2 + "\\\\", 1, 2), "\\\\", TextCompare: false) != 0)
						{
							FileSystem.ChDrive(text2);
							FileSystem.ChDir(text2);
						}
						MyGrid = GridColor;
						GridModule.Commit_Grid_Color(ref MyGrid);
						GridColor = MyGrid;
						if (Globals_Renamed.Design_Mode > 0)
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Update_All_Grid_Colors();
						}
						text2 = "";
						num5 = BuildForm.Copy_TNSNames(Globals_Renamed.MyPCDir, IsQuiet: false, ref text2);
						if (num5 == 1)
						{
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						BuildForm.Save_DTRBuild_Ini("C");
						lpApplicationName = "SQLPATHFINDER";
						lpKeyName = "PYTHON";
						lpString = ref lMyPyPath;
						lpFileName = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini";
						num5 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpString, ref lpFileName);
						lpFileName = "SQLPATHFINDER";
						lpKeyName = "PYTHON3";
						lpString2 = ref lMyPyPath3;
						lpApplicationName = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini";
						num5 = Globals_Renamed.WritePrivateProfileString(ref lpFileName, ref lpKeyName, ref lpString2, ref lpApplicationName);
						if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
						{
							MyProject.Forms.FrmMain.mnuSQLPos.Visible = true;
						}
						else
						{
							MyProject.Forms.FrmMain.mnuSQLPos.Visible = false;
						}
						Cursor.Current = Cursors.Default;
						Close();
						goto end_IL_0001_2;
					}
					goto IL_07fb;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1981;
				continue;
			}
			break;
			IL_07fb:
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

	private void frmconfigure_Load(object eventSender, EventArgs eventArgs)
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
					errsource = "frmConfigure - Form_Load";
					string name = Strings.Trim(Interaction.Environ("TEMP"));
					name = Strings.Trim(Environment.ExpandEnvironmentVariables(name));
					if (Operators.CompareString(name, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(name, Strings.Len(name), 1), "\\", TextCompare: false) != 0)
					{
						name += "\\";
						name += "SQLPathFinder_Temp\\";
					}
					TxtPCDir.Items.Add(name);
					l_InPCDir = Strings.UCase(Strings.Trim(Globals_Renamed.MyPCDir));
					TxtQDir.Items.Add("%userprofile%\\OneDrive - Intel Corporation\\Documents\\SQLPathFinder_Data");
					CmbMidas.Items.Add("UBER");
					CmbMidas.Items.Add("Crystal Ball");
					CmbMidas.Items.Add(".Net");
					CmbMidas.Text = Globals_Renamed.gMidasDriver;
					if ((Operators.CompareString(Globals_Renamed.gEmailOutlook, "Y", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.gEmailOutlook, "O", TextCompare: false) == 0))
					{
						cmbEmail.Text = "Outlook";
					}
					else if (Operators.CompareString(Globals_Renamed.gEmailOutlook, "S", TextCompare: false) == 0)
					{
						cmbEmail.Text = "SMTP";
					}
					else
					{
						cmbEmail.Text = "SMTP Auth";
					}
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						chksqlnet.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gOrclRetry, "Y", TextCompare: false) == 0)
					{
						chkOrclRetry.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0)
					{
						chkpydebug.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gUseMinPath, "Y", TextCompare: false) == 0)
					{
						chkMinPath.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
					{
						chkwebnext.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gEncodeFFS, "Y", TextCompare: false) == 0)
					{
						chkencode.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gConvertMAOUber, "Y", TextCompare: false) == 0)
					{
						chkConvertMAOUber.Checked = true;
					}
					if (Operators.CompareString(Globals_Renamed.gEncodeUTFBOM, "Y", TextCompare: false) == 0)
					{
						chkencodeutfbom.Checked = true;
					}
					TxtPCDir.Text = Globals_Renamed.MyPCDir;
					TxtQDir.Text = Globals_Renamed.gQueryDir;
					TxtJMP.Items.Clear();
					TxtJMP.Items.Add("C:\\Program Files\\SAS\\JMPPRO\\12\\jmp.exe");
					TxtJMP.Items.Add("C:\\Program Files\\SAS\\JMPPRO\\14\\jmp.exe");
					l_jmp = Globals_Renamed.MyJMPPath;
					TxtJMP.Text = l_jmp;
					l_R = Globals_Renamed.gMyRPath;
					l_Py = lMyPyPath;
					l_Py3 = lMyPyPath3;
					l_XL = Globals_Renamed.MyExcelPath;
					DataGridView MyGrid = GridColor;
					ComboBox CmbColor = this.CmbColor;
					GridModule.Init_Grid_For_Colors(ref MyGrid, ref CmbColor);
					this.CmbColor = CmbColor;
					GridColor = MyGrid;
					Cursor.Current = Cursors.Default;
					goto end_IL_0001;
				}
				case 963:
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
				goto IL_03f9;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 963;
				continue;
			}
			break;
			IL_03f9:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmconfigure_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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

	private void CmdBrowseJMP_Click(object sender, EventArgs e)
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
				case 190:
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
							goto IL_002e;
						case 4:
							goto IL_0056;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					BuildForm.FileOpenSave("O", "", "exe", l_WhichPrompt, l_WhichPath);
					goto IL_002e;
					IL_002e:
					num2 = 3;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0056;
					IL_0056:
					num2 = 4;
					TxtJMP.Text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				MyProject.Forms.FrmMain.CMDialog1Open.FileName = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 190;
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

	private void CmbColor_DrawItem(object sender, DrawItemEventArgs e)
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
				case 52:
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
				GridModule.CmbDrawItem(ref sender, e);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 52;
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

	private void CmbColor_KeyPress(object sender, KeyPressEventArgs e)
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
					num5 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_0011;
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
							goto IL_0011;
						case 3:
							goto IL_001a;
						case 4:
							goto IL_0028;
						case 5:
							goto IL_0035;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_001a:
					num2 = 3;
					if (!CmbBoxKey)
					{
						goto end_IL_0001_3;
					}
					goto IL_0028;
					IL_0028:
					num2 = 4;
					if (num5 != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0011:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001a;
					IL_0035:
					num2 = 5;
					CmbBoxKey = false;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				CmbColor_SelectedIndexChanged(CmbColor, new EventArgs());
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

	private void CmbColor_Leave(object sender, EventArgs e)
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
				{
					errsource = "FrmConfig - CmbColor_Leave";
					ProjectData.ClearProjectError();
					num2 = 2;
					GridColor.Rows[f_row].Cells[f_col].Value = CmbColor.Text;
					DataGridViewCellStyle defaultCellStyle = GridColor.Rows[f_row].DefaultCellStyle;
					object obj = CmbColor.Items[CmbColor.SelectedIndex];
					defaultCellStyle.BackColor = ((obj != null) ? ((Color)obj) : default(Color));
					GridColor.Refresh();
					CmbColor.Visible = false;
					CmbBoxKey = false;
					goto end_IL_0001;
				}
				case 233:
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
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 233;
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

	private void CmbColor_SelectedIndexChanged(object sender, EventArgs e)
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
					CmbColor_Leave(CmbColor, new EventArgs());
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				GridColor.Focus();
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

	private void GridColor_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
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
							goto IL_003c;
						case 4:
							goto IL_006e;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					num5 = Conversions.ToShort(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null));
					goto IL_003c;
					IL_003c:
					num2 = 3;
					num6 = Conversions.ToShort(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "rowIndex", new object[0], null, null, null));
					goto IL_006e;
					IL_006e:
					num2 = 4;
					if (!(num6 >= 0 && num5 == 1))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				DataGridView MyGrid = GridColor;
				ComboBox MyCombo = CmbColor;
				GridModule.ComboColor_Click(ref MyGrid, ref MyCombo, ref f_row, ref f_col);
				CmbColor = MyCombo;
				GridColor = MyGrid;
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

	private void GridColor_KeyDown(object sender, KeyEventArgs e)
	{
		f_TrueRow = Conversions.ToShort(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "RowIndex", new object[0], null, null, null));
	}

	private void GridColor_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (Operators.ConditionalCompareObjectLess(NewLateBinding.LateGet(sender, null, "RowCount", new object[0], null, null, null), 1, TextCompare: false))
		{
			return;
		}
		short num = Conversions.ToShort(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null));
		short num2 = Conversions.ToShort(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "RowIndex", new object[0], null, null, null));
		short num3 = checked((short)Strings.Asc(e.KeyChar));
		if (num3 == 13)
		{
			CmbBoxKey = true;
			if (f_TrueRow == -1)
			{
				f_TrueRow = num2;
			}
			if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectNotEqual(f_TrueRow, Operators.SubtractObject(NewLateBinding.LateGet(sender, null, "RowCount", new object[0], null, null, null), 1), TextCompare: false), f_TrueRow != num2)))
			{
				object[] array = new object[1];
				object[] array2 = new object[1];
				ref short reference = ref f_TrueRow;
				array2[0] = reference;
				object[] array3 = array2;
				bool[] obj = new bool[1] { true };
				bool[] array4 = obj;
				object instance = NewLateBinding.LateGet(sender, null, "Rows", array2, null, null, obj);
				if (array4[0])
				{
					reference = (short)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(short));
				}
				object[] array5;
				bool[] array6;
				object obj2 = NewLateBinding.LateGet(instance, null, "Cells", array5 = new object[1] { num }, null, null, array6 = new bool[1] { true });
				if (array6[0])
				{
					num = (short)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array5[0]), typeof(short));
				}
				array[0] = obj2;
				NewLateBinding.LateSet(sender, null, "CurrentCell", array, null, null);
			}
			GridColor_Click(RuntimeHelpers.GetObjectValue(sender), new EventArgs());
		}
		f_TrueRow = -1;
	}

	private void frmconfigure_Resize(object sender, EventArgs e)
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
				Resize_Form();
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

	private void GridColor_Scroll(object sender, ScrollEventArgs e)
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
				GridColor.Focus();
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

	private void CmdClearAll_Click(object sender, EventArgs e)
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 234:
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
							goto IL_0028;
						case 6:
							goto IL_0034;
						case 7:
							goto IL_004a;
						case 8:
							goto IL_0074;
						case 9:
							goto IL_0098;
						default:
							goto end_IL_0001;
						case 10:
						case 11:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_004a:
					num2 = 7;
					GridColor.Rows[num5].Cells[1].Value = "White";
					goto IL_0074;
					IL_0074:
					num2 = 8;
					GridColor.Rows[num5].DefaultCellStyle.BackColor = Color.White;
					goto IL_0098;
					IL_00a1:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_004a;
					IL_0098:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_00a1;
					IL_000b:
					num2 = 2;
					num7 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num7 = (int)Interaction.MsgBox("Are you sure you want to clear your color selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear Colors?");
					goto IL_0028;
					IL_0028:
					num2 = 5;
					if (num7 != 6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0034;
					IL_0034:
					num2 = 6;
					num6 = checked(GridColor.RowCount - 1);
					num5 = 0;
					goto IL_00a1;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 234;
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

	private void CmdDefColor_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		Color[] array = default(Color[]);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num7;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1032:
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
							goto IL_0024;
						case 7:
							goto IL_0033;
						case 8:
							goto IL_0042;
						case 9:
							goto IL_0051;
						case 10:
							goto IL_0061;
						case 11:
							goto IL_0071;
						case 12:
							goto IL_0081;
						case 13:
							goto IL_0091;
						case 14:
							goto IL_00a1;
						case 15:
							goto IL_00b1;
						case 16:
							goto IL_00c2;
						case 17:
							goto IL_00d3;
						case 18:
							goto IL_00e4;
						case 19:
							goto IL_00f5;
						case 20:
							goto IL_0106;
						case 21:
							goto IL_0117;
						case 22:
							goto IL_0128;
						case 23:
							goto IL_0139;
						case 24:
							goto IL_014a;
						case 25:
							goto IL_015b;
						case 26:
							goto IL_016c;
						case 27:
							goto IL_017d;
						case 28:
							goto IL_018e;
						case 29:
							goto IL_019f;
						case 30:
							goto IL_01b0;
						case 31:
							goto IL_01c1;
						case 32:
							goto IL_01d2;
						case 33:
							goto IL_01e3;
						case 34:
							goto IL_01f4;
						case 35:
							goto IL_0205;
						case 36:
							goto IL_0216;
						case 37:
							goto IL_0227;
						case 38:
							goto IL_0238;
						case 39:
							goto IL_0249;
						case 40:
							goto IL_025a;
						case 41:
							goto IL_026b;
						case 42:
							goto IL_027c;
						case 43:
							goto IL_0291;
						case 44:
							goto IL_02a1;
						case 45:
							goto IL_02a7;
						case 46:
							goto IL_02d0;
						case 47:
							goto IL_031b;
						default:
							goto end_IL_0001;
						case 48:
						case 49:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_02a7:
					num2 = 45;
					GridColor.Rows[num5].DefaultCellStyle.BackColor = array[num5];
					goto IL_02d0;
					IL_02d0:
					num2 = 46;
					GridColor.Rows[num5].Cells[1].Value = GridColor.Rows[num5].DefaultCellStyle.BackColor.Name;
					goto IL_031b;
					IL_02a1:
					num2 = 44;
					num5 = 0;
					goto IL_02a7;
					IL_031b:
					num2 = 47;
					num5 = checked(num5 + 1);
					if (num5 > 35)
					{
						goto end_IL_0001_2;
					}
					goto IL_02a7;
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
					num7 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					array = new Color[37];
					goto IL_0024;
					IL_0024:
					num2 = 6;
					array[0] = Color.White;
					goto IL_0033;
					IL_0033:
					num2 = 7;
					array[1] = Color.BlanchedAlmond;
					goto IL_0042;
					IL_0042:
					num2 = 8;
					array[2] = Color.Honeydew;
					goto IL_0051;
					IL_0051:
					num2 = 9;
					array[3] = Color.PaleTurquoise;
					goto IL_0061;
					IL_0061:
					num2 = 10;
					array[4] = Color.MistyRose;
					goto IL_0071;
					IL_0071:
					num2 = 11;
					array[5] = Color.PaleGoldenrod;
					goto IL_0081;
					IL_0081:
					num2 = 12;
					array[6] = Color.LightGreen;
					goto IL_0091;
					IL_0091:
					num2 = 13;
					array[7] = Color.LightBlue;
					goto IL_00a1;
					IL_00a1:
					num2 = 14;
					array[8] = Color.LightPink;
					goto IL_00b1;
					IL_00b1:
					num2 = 15;
					array[9] = Color.Beige;
					goto IL_00c2;
					IL_00c2:
					num2 = 16;
					array[10] = Color.Moccasin;
					goto IL_00d3;
					IL_00d3:
					num2 = 17;
					array[11] = Color.LightCyan;
					goto IL_00e4;
					IL_00e4:
					num2 = 18;
					array[12] = Color.Aqua;
					goto IL_00f5;
					IL_00f5:
					num2 = 19;
					array[13] = Color.DarkSalmon;
					goto IL_0106;
					IL_0106:
					num2 = 20;
					array[14] = Color.Wheat;
					goto IL_0117;
					IL_0117:
					num2 = 21;
					array[15] = Color.Orange;
					goto IL_0128;
					IL_0128:
					num2 = 22;
					array[16] = Color.Khaki;
					goto IL_0139;
					IL_0139:
					num2 = 23;
					array[17] = Color.Peru;
					goto IL_014a;
					IL_014a:
					num2 = 24;
					array[18] = Color.BurlyWood;
					goto IL_015b;
					IL_015b:
					num2 = 25;
					array[19] = Color.NavajoWhite;
					goto IL_016c;
					IL_016c:
					num2 = 26;
					array[20] = Color.White;
					goto IL_017d;
					IL_017d:
					num2 = 27;
					array[21] = Color.White;
					goto IL_018e;
					IL_018e:
					num2 = 28;
					array[22] = Color.White;
					goto IL_019f;
					IL_019f:
					num2 = 29;
					array[23] = Color.White;
					goto IL_01b0;
					IL_01b0:
					num2 = 30;
					array[24] = Color.White;
					goto IL_01c1;
					IL_01c1:
					num2 = 31;
					array[25] = Color.White;
					goto IL_01d2;
					IL_01d2:
					num2 = 32;
					array[26] = Color.White;
					goto IL_01e3;
					IL_01e3:
					num2 = 33;
					array[27] = Color.White;
					goto IL_01f4;
					IL_01f4:
					num2 = 34;
					array[28] = Color.White;
					goto IL_0205;
					IL_0205:
					num2 = 35;
					array[29] = Color.White;
					goto IL_0216;
					IL_0216:
					num2 = 36;
					array[30] = Color.White;
					goto IL_0227;
					IL_0227:
					num2 = 37;
					array[31] = Color.White;
					goto IL_0238;
					IL_0238:
					num2 = 38;
					array[32] = Color.PeachPuff;
					goto IL_0249;
					IL_0249:
					num2 = 39;
					array[33] = Color.BurlyWood;
					goto IL_025a;
					IL_025a:
					num2 = 40;
					array[34] = Color.PaleGreen;
					goto IL_026b;
					IL_026b:
					num2 = 41;
					array[35] = Color.Thistle;
					goto IL_027c;
					IL_027c:
					num2 = 42;
					num6 = (int)Interaction.MsgBox("Do you wish to override your current color selections and use SQLPathFinder's default selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Use Default Colors?");
					goto IL_0291;
					IL_0291:
					num2 = 43;
					if (num6 != 6)
					{
						goto end_IL_0001_2;
					}
					goto IL_02a1;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1032;
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

	private void cmdAssocTab_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 436:
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
							goto IL_0028;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0047;
						case 7:
							goto IL_0055;
						case 8:
							goto IL_0085;
						case 9:
							goto IL_00a8;
						case 10:
							goto IL_00d3;
						case 11:
							goto IL_00eb;
						case 12:
						case 13:
							goto IL_010a;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a8:
					num2 = 9;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0)
					{
						goto IL_00d3;
					}
					goto IL_00eb;
					IL_00d3:
					num2 = 10;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00eb;
					IL_0085:
					num2 = 8;
					BuildForm.FileOpenSave("O", "", "exe", l_WhichPrompt, l_WhichPath);
					goto IL_00a8;
					IL_00eb:
					num2 = 11;
					MyProject.Forms.FrmMain.CMDialog1Open.FileName = "";
					goto IL_010a;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num5 = (int)Interaction.MsgBox("Do you wish to associate .tab files with Excel?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Associate Tab Files");
					goto IL_0028;
					IL_0028:
					num2 = 4;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0038;
					IL_0038:
					num2 = 5;
					radioXL.Checked = true;
					goto IL_0047;
					IL_0047:
					num2 = 6;
					text = Strings.Trim(l_XL);
					goto IL_0055;
					IL_0055:
					num2 = 7;
					if (Operators.CompareString(Strings.UCase(text), "AUTOMATIC", TextCompare: false) == 0 || Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0085;
					}
					goto IL_010a;
					IL_010a:
					num2 = 13;
					if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text), "AUTOMATIC", TextCompare: false) != 0)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				BuildSQL.RunQ("\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\AssocTabExcel.bat \"" + text + "\"", 0, "M");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 436;
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

	private void cmdQDir_Click(object sender, EventArgs e)
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
				case 201:
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
							goto IL_0027;
						case 4:
							goto IL_0049;
						case 5:
							goto IL_0066;
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
					IL_0027:
					num2 = 3;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = TxtQDir.Text;
					goto IL_0049;
					IL_0049:
					num2 = 4;
					if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() != DialogResult.OK)
					{
						break;
					}
					goto IL_0066;
					IL_000b:
					num2 = 2;
					MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Locate a Query Directory";
					goto IL_0027;
					IL_0066:
					num2 = 5;
					TxtQDir.Text = MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 201;
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

	private void cmdIcon_Click(object sender, EventArgs e)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		string text = "";
		try
		{
			string text2 = Strings.Trim(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + "\\SQLPathFinder3.lnk";
			WshShell val = (WshShell)new WshShellClass();
			IWshShortcut val2 = null;
			val2 = (IWshShortcut)((IWshShell3)val).CreateShortcut(text2);
			IWshShortcut val3 = val2;
			val3.TargetPath = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\StartSQLPathFinder.bat";
			val3.WindowStyle = 6;
			val3.Description = "SQLPathFinder";
			val3.WorkingDirectory = Strings.Trim(MyProject.Application.Info.DirectoryPath);
			val3.IconLocation = Application.ExecutablePath + ", 0";
			val3.Arguments = string.Empty;
			val3.Save();
			val3 = null;
			text = "SQLPathFinder Shortcut Created on your desktop";
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Could not create SQLPathFinder Shortcut:\r\n" + ex2.Message, MsgBoxStyle.Exclamation, "Shortcut Error");
			ProjectData.ClearProjectError();
		}
		try
		{
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.vg2").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.vgq").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.vge").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.vgec").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.spf").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\.spfsql").SetValue("", "SPFEXE", RegistryValueKind.String);
			MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\classes\\SPFEXE\\shell\\open\\command").SetValue("", "\"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SQLPathFinder3.exe\" \"%1\" \"%2\" %*", RegistryValueKind.String);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				text += " and ";
			}
			text += "Vg2, vgq, vge, vgec and spf files were associated with SQLPathFinder.";
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Interaction.MsgBox("Could not associate .vg2, .vge, .vgec and .spf files with SQLPathFinder:\r\n" + ex4.Message, MsgBoxStyle.Exclamation, "Association Error");
			ProjectData.ClearProjectError();
		}
		finally
		{
		}
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			Interaction.MsgBox(text, MsgBoxStyle.Information, "Status");
		}
	}

	private void TabConfig_SelectedIndexChanged(object sender, EventArgs e)
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
				case 98:
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
							goto IL_001f;
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
					if (TabConfig.SelectedIndex != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_001f;
					IL_001f:
					num2 = 3;
					Resize_Form();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				TabConfig.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 98;
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

	private void radiojmp_CheckedChanged(object sender, EventArgs e)
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
				DoRadio("SET");
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
}

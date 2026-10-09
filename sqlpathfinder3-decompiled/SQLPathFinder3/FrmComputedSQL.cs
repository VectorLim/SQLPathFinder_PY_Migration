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
internal class FrmComputedSQL : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdSet")]
	private Button _cmdSet;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCopy")]
	private Button _cmdCopy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("lstLevel")]
	private ComboBox _lstLevel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("txtComment")]
	private TextBox _txtComment;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdHelp")]
	private Button _CmdHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Text2")]
	private TextBox _Text2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbDataType")]
	private ComboBox _CmbDataType;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdsummary")]
	private Button _cmdsummary;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Combo2")]
	private ComboBox _Combo2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("lstgeneral")]
	private ListBox _lstgeneral;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdoperators")]
	private Button _cmdoperators;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdfunctions")]
	private Button _cmdfunctions;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdcolumns")]
	private Button _cmdcolumns;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Command5")]
	private Button _Command5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmd2")]
	private Button _cmd2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdexpr")]
	private Button _cmdexpr;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdConcat")]
	private Button _cmdConcat;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtExpression")]
	private TextBox _TxtExpression;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAdd")]
	private Button _cmdAdd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdclose")]
	private Button _cmdclose;

	private string GlobalListColumn;

	private string l_DBType;

	private string l_CompExpr;

	private string l_CompExpr_List;

	private int l_CompExpr_DT;

	private string l_CompExpr_Name;

	private string l_Alias;

	private Globals_Renamed.AliasArray_Type[] g_AliasArray;

	private short g_NoAlias;

	private bool fSaveExpr;

	private bool ListenExpr;

	private const string l_MongoErrMsg = "Mongo Databases do not yet support summary expressions. To implement summary expressions, add the summary functions to columns on the Columns Tab and build the filter expressions at level Summary + 1";

	public virtual Button cmdSet
	{
		[CompilerGenerated]
		get
		{
			return _cmdSet;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSet_Click;
			Button button = _cmdSet;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdSet = value;
			button = _cmdSet;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdCopy
	{
		[CompilerGenerated]
		get
		{
			return _cmdCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCopy_Click;
			Button button = _cmdCopy;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdCopy = value;
			button = _cmdCopy;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual ComboBox lstLevel
	{
		[CompilerGenerated]
		get
		{
			return _lstLevel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lstLevel_SelectedIndexChanged;
			KeyPressEventHandler value3 = lstLevel_KeyPress;
			ComboBox comboBox = _lstLevel;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.KeyPress -= value3;
			}
			_lstLevel = value;
			comboBox = _lstLevel;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.KeyPress += value3;
			}
		}
	}

	public virtual TextBox txtComment
	{
		[CompilerGenerated]
		get
		{
			return _txtComment;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtComment_TextChanged;
			TextBox textBox = _txtComment;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			_txtComment = value;
			textBox = _txtComment;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lbldesc")]
	public virtual TextBox lbldesc
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

	public virtual TextBox Text2
	{
		[CompilerGenerated]
		get
		{
			return _Text2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = Text2_KeyPress;
			EventHandler value3 = Text2_TextChanged;
			TextBox textBox = _Text2;
			if (textBox != null)
			{
				textBox.KeyPress -= value2;
				textBox.TextChanged -= value3;
			}
			_Text2 = value;
			textBox = _Text2;
			if (textBox != null)
			{
				textBox.KeyPress += value2;
				textBox.TextChanged += value3;
			}
		}
	}

	public virtual ComboBox CmbDataType
	{
		[CompilerGenerated]
		get
		{
			return _CmbDataType;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmbDataType_SelectedIndexChanged;
			ComboBox comboBox = _CmbDataType;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			_CmbDataType = value;
			comboBox = _CmbDataType;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

	public virtual Button cmdsummary
	{
		[CompilerGenerated]
		get
		{
			return _cmdsummary;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdsummary_Click;
			Button button = _cmdsummary;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdsummary = value;
			button = _cmdsummary;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual ComboBox Combo2
	{
		[CompilerGenerated]
		get
		{
			return _Combo2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Combo2_SelectedIndexChanged;
			ComboBox comboBox = _Combo2;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			_Combo2 = value;
			comboBox = _Combo2;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

	public virtual ListBox lstgeneral
	{
		[CompilerGenerated]
		get
		{
			return _lstgeneral;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lstgeneral_SelectedIndexChanged;
			EventHandler value3 = lstgeneral_DoubleClick;
			KeyPressEventHandler value4 = lstgeneral_KeyPress;
			ListBox listBox = _lstgeneral;
			if (listBox != null)
			{
				listBox.SelectedIndexChanged -= value2;
				listBox.DoubleClick -= value3;
				listBox.KeyPress -= value4;
			}
			_lstgeneral = value;
			listBox = _lstgeneral;
			if (listBox != null)
			{
				listBox.SelectedIndexChanged += value2;
				listBox.DoubleClick += value3;
				listBox.KeyPress += value4;
			}
		}
	}

	public virtual Button cmdoperators
	{
		[CompilerGenerated]
		get
		{
			return _cmdoperators;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdoperators_Click;
			Button button = _cmdoperators;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdoperators = value;
			button = _cmdoperators;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdfunctions
	{
		[CompilerGenerated]
		get
		{
			return _cmdfunctions;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdfunctions_Click;
			Button button = _cmdfunctions;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdfunctions = value;
			button = _cmdfunctions;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdcolumns
	{
		[CompilerGenerated]
		get
		{
			return _cmdcolumns;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdColumns_Click;
			Button button = _cmdcolumns;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdcolumns = value;
			button = _cmdcolumns;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button Command5
	{
		[CompilerGenerated]
		get
		{
			return _Command5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Command5_Click;
			Button button = _Command5;
			if (button != null)
			{
				button.Click -= value2;
			}
			_Command5 = value;
			button = _Command5;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmd2
	{
		[CompilerGenerated]
		get
		{
			return _cmd2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmd2_Click;
			Button button = _cmd2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmd2 = value;
			button = _cmd2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdexpr
	{
		[CompilerGenerated]
		get
		{
			return _cmdexpr;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdexpr_Click;
			Button button = _cmdexpr;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdexpr = value;
			button = _cmdexpr;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdConcat
	{
		[CompilerGenerated]
		get
		{
			return _cmdConcat;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmd2_Click;
			Button button = _cmdConcat;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdConcat = value;
			button = _cmdConcat;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual TextBox TxtExpression
	{
		[CompilerGenerated]
		get
		{
			return _TxtExpression;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtExpression_TextChanged;
			TextBox textBox = _TxtExpression;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			_TxtExpression = value;
			textBox = _TxtExpression;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	public virtual Button cmdAdd
	{
		[CompilerGenerated]
		get
		{
			return _cmdAdd;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdadd_Click;
			Button button = _cmdAdd;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdAdd = value;
			button = _cmdAdd;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdclose
	{
		[CompilerGenerated]
		get
		{
			return _cmdclose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
			Button button = _cmdclose;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdclose = value;
			button = _cmdclose;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblFilter")]
	public virtual Label lblFilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label4")]
	public virtual Label Label4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label8")]
	public virtual Label Label8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label6")]
	public virtual Label Label6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblgeneral")]
	public virtual Label lblgeneral
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblexpression")]
	public virtual Label lblexpression
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

	[DebuggerNonUserCode]
	public FrmComputedSQL()
	{
		base.FormClosing += FrmComputedSQL_FormClosing;
		base.Load += FrmComputedSQL_Load;
		base.FormClosed += FrmComputedSQL_FormClosed;
		GlobalListColumn = "";
		l_DBType = "";
		l_CompExpr = "";
		l_CompExpr_List = "";
		l_CompExpr_DT = 0;
		l_CompExpr_Name = "";
		l_Alias = "";
		g_AliasArray = new Globals_Renamed.AliasArray_Type[134];
		g_NoAlias = -1;
		fSaveExpr = false;
		ListenExpr = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmComputedSQL));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdSet = new System.Windows.Forms.Button();
		this.cmdCopy = new System.Windows.Forms.Button();
		this.Text2 = new System.Windows.Forms.TextBox();
		this.CmbDataType = new System.Windows.Forms.ComboBox();
		this.cmdsummary = new System.Windows.Forms.Button();
		this.Combo2 = new System.Windows.Forms.ComboBox();
		this.cmdoperators = new System.Windows.Forms.Button();
		this.cmdfunctions = new System.Windows.Forms.Button();
		this.cmdcolumns = new System.Windows.Forms.Button();
		this.Command5 = new System.Windows.Forms.Button();
		this.cmd2 = new System.Windows.Forms.Button();
		this.cmdexpr = new System.Windows.Forms.Button();
		this.cmdConcat = new System.Windows.Forms.Button();
		this.cmdAdd = new System.Windows.Forms.Button();
		this.cmdclose = new System.Windows.Forms.Button();
		this.CmdHelp = new System.Windows.Forms.Button();
		this.lstLevel = new System.Windows.Forms.ComboBox();
		this.txtComment = new System.Windows.Forms.TextBox();
		this.lbldesc = new System.Windows.Forms.TextBox();
		this.lstgeneral = new System.Windows.Forms.ListBox();
		this.TxtExpression = new System.Windows.Forms.TextBox();
		this.lblFilter = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.lblgeneral = new System.Windows.Forms.Label();
		this.lblexpression = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.cmdSet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdSet.BackColor = System.Drawing.SystemColors.Control;
		this.cmdSet.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdSet.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdSet.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdSet.Location = new System.Drawing.Point(45, 363);
		this.cmdSet.Name = "cmdSet";
		this.cmdSet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdSet.Size = new System.Drawing.Size(36, 29);
		this.cmdSet.TabIndex = 11;
		this.cmdSet.Text = "Set";
		this.ToolTip1.SetToolTip(this.cmdSet, "Adds a copied formula to the expression text box");
		this.cmdSet.UseVisualStyleBackColor = false;
		this.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdCopy.BackColor = System.Drawing.SystemColors.Control;
		this.cmdCopy.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdCopy.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdCopy.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCopy.Location = new System.Drawing.Point(4, 363);
		this.cmdCopy.Name = "cmdCopy";
		this.cmdCopy.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdCopy.Size = new System.Drawing.Size(36, 29);
		this.cmdCopy.TabIndex = 10;
		this.cmdCopy.Text = "Copy";
		this.ToolTip1.SetToolTip(this.cmdCopy, "Copies the current expression. Useful if you need tro create several similar expressions");
		this.cmdCopy.UseVisualStyleBackColor = false;
		this.Text2.AcceptsReturn = true;
		this.Text2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Text2.BackColor = System.Drawing.SystemColors.Window;
		this.Text2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.Text2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Text2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Text2.Location = new System.Drawing.Point(4, 19);
		this.Text2.MaxLength = 30;
		this.Text2.Name = "Text2";
		this.Text2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Text2.Size = new System.Drawing.Size(297, 26);
		this.Text2.TabIndex = 0;
		this.ToolTip1.SetToolTip(this.Text2, "Provide a Column Header");
		this.CmbDataType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmbDataType.BackColor = System.Drawing.SystemColors.Window;
		this.CmbDataType.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbDataType.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmbDataType.ForeColor = System.Drawing.SystemColors.WindowText;
		this.CmbDataType.Location = new System.Drawing.Point(4, 64);
		this.CmbDataType.Name = "CmbDataType";
		this.CmbDataType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmbDataType.Size = new System.Drawing.Size(297, 26);
		this.CmbDataType.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.CmbDataType, "Select a DataType");
		this.cmdsummary.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdsummary.BackColor = System.Drawing.SystemColors.Control;
		this.cmdsummary.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdsummary.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdsummary.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdsummary.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdsummary.Location = new System.Drawing.Point(4, 465);
		this.cmdsummary.Name = "cmdsummary";
		this.cmdsummary.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdsummary.Size = new System.Drawing.Size(80, 29);
		this.cmdsummary.TabIndex = 24;
		this.cmdsummary.Text = "Summary";
		this.ToolTip1.SetToolTip(this.cmdsummary, "Load Statistical Functions");
		this.cmdsummary.UseVisualStyleBackColor = false;
		this.Combo2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Combo2.BackColor = System.Drawing.SystemColors.Window;
		this.Combo2.Cursor = System.Windows.Forms.Cursors.Default;
		this.Combo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Combo2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Combo2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Combo2.Location = new System.Drawing.Point(63, 500);
		this.Combo2.Name = "Combo2";
		this.Combo2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Combo2.Size = new System.Drawing.Size(102, 26);
		this.Combo2.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.Combo2, "Specify Object Alias for Computed Column");
		this.cmdoperators.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdoperators.BackColor = System.Drawing.SystemColors.Control;
		this.cmdoperators.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdoperators.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdoperators.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdoperators.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdoperators.Location = new System.Drawing.Point(4, 431);
		this.cmdoperators.Name = "cmdoperators";
		this.cmdoperators.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdoperators.Size = new System.Drawing.Size(78, 29);
		this.cmdoperators.TabIndex = 27;
		this.cmdoperators.Text = "Operators";
		this.ToolTip1.SetToolTip(this.cmdoperators, "Load Comparison Operators");
		this.cmdoperators.UseVisualStyleBackColor = false;
		this.cmdfunctions.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdfunctions.BackColor = System.Drawing.SystemColors.Control;
		this.cmdfunctions.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdfunctions.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdfunctions.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdfunctions.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdfunctions.Location = new System.Drawing.Point(85, 431);
		this.cmdfunctions.Name = "cmdfunctions";
		this.cmdfunctions.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdfunctions.Size = new System.Drawing.Size(80, 29);
		this.cmdfunctions.TabIndex = 18;
		this.cmdfunctions.Text = "Functions";
		this.ToolTip1.SetToolTip(this.cmdfunctions, "Load SQL Functions");
		this.cmdfunctions.UseVisualStyleBackColor = false;
		this.cmdcolumns.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdcolumns.BackColor = System.Drawing.SystemColors.Control;
		this.cmdcolumns.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdcolumns.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdcolumns.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdcolumns.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdcolumns.Location = new System.Drawing.Point(85, 465);
		this.cmdcolumns.Name = "cmdcolumns";
		this.cmdcolumns.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdcolumns.Size = new System.Drawing.Size(80, 29);
		this.cmdcolumns.TabIndex = 21;
		this.cmdcolumns.Text = "Columns";
		this.ToolTip1.SetToolTip(this.cmdcolumns, "Load Selected Columns");
		this.cmdcolumns.UseVisualStyleBackColor = false;
		this.Command5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Command5.BackColor = System.Drawing.SystemColors.Control;
		this.Command5.Cursor = System.Windows.Forms.Cursors.Default;
		this.Command5.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.Command5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Command5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Command5.Location = new System.Drawing.Point(85, 363);
		this.Command5.Name = "Command5";
		this.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Command5.Size = new System.Drawing.Size(80, 29);
		this.Command5.TabIndex = 12;
		this.Command5.Text = "Clear";
		this.ToolTip1.SetToolTip(this.Command5, "Clear Expression");
		this.Command5.UseVisualStyleBackColor = false;
		this.cmd2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmd2.BackColor = System.Drawing.SystemColors.Control;
		this.cmd2.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmd2.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmd2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmd2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmd2.Location = new System.Drawing.Point(45, 397);
		this.cmd2.Name = "cmd2";
		this.cmd2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmd2.Size = new System.Drawing.Size(36, 29);
		this.cmd2.TabIndex = 14;
		this.cmd2.Tag = "doublequote";
		this.cmd2.Text = "\"";
		this.ToolTip1.SetToolTip(this.cmd2, "Double quote mark");
		this.cmd2.UseVisualStyleBackColor = false;
		this.cmdexpr.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdexpr.BackColor = System.Drawing.SystemColors.Control;
		this.cmdexpr.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdexpr.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdexpr.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdexpr.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdexpr.Location = new System.Drawing.Point(85, 397);
		this.cmdexpr.Name = "cmdexpr";
		this.cmdexpr.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdexpr.Size = new System.Drawing.Size(80, 29);
		this.cmdexpr.TabIndex = 15;
		this.cmdexpr.Text = "<expr>";
		this.ToolTip1.SetToolTip(this.cmdexpr, "Highlight next <expr> token in expression");
		this.cmdexpr.UseVisualStyleBackColor = false;
		this.cmdConcat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdConcat.BackColor = System.Drawing.SystemColors.Control;
		this.cmdConcat.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdConcat.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdConcat.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdConcat.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdConcat.Location = new System.Drawing.Point(4, 397);
		this.cmdConcat.Name = "cmdConcat";
		this.cmdConcat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdConcat.Size = new System.Drawing.Size(36, 29);
		this.cmdConcat.TabIndex = 13;
		this.cmdConcat.Text = " || ";
		this.ToolTip1.SetToolTip(this.cmdConcat, "Concatenate Strings");
		this.cmdConcat.UseVisualStyleBackColor = false;
		this.cmdAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdAdd.BackColor = System.Drawing.SystemColors.Control;
		this.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdAdd.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdAdd.Location = new System.Drawing.Point(369, 35);
		this.cmdAdd.Name = "cmdAdd";
		this.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdAdd.Size = new System.Drawing.Size(69, 35);
		this.cmdAdd.TabIndex = 8;
		this.cmdAdd.Text = "Add";
		this.ToolTip1.SetToolTip(this.cmdAdd, "Add expression to Query");
		this.cmdAdd.UseVisualStyleBackColor = false;
		this.cmdclose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdclose.BackColor = System.Drawing.SystemColors.Control;
		this.cmdclose.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdclose.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmdclose.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdclose.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdclose.Location = new System.Drawing.Point(369, 0);
		this.cmdclose.Name = "cmdclose";
		this.cmdclose.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdclose.Size = new System.Drawing.Size(69, 35);
		this.cmdclose.TabIndex = 7;
		this.cmdclose.Text = "Close";
		this.ToolTip1.SetToolTip(this.cmdclose, "Close form");
		this.cmdclose.UseVisualStyleBackColor = false;
		this.CmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.CmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.CmdHelp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdHelp.Location = new System.Drawing.Point(369, 70);
		this.CmdHelp.Name = "CmdHelp";
		this.CmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdHelp.Size = new System.Drawing.Size(69, 35);
		this.CmdHelp.TabIndex = 9;
		this.CmdHelp.Text = "Help";
		this.CmdHelp.UseVisualStyleBackColor = false;
		this.lstLevel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstLevel.BackColor = System.Drawing.SystemColors.Window;
		this.lstLevel.Cursor = System.Windows.Forms.Cursors.Default;
		this.lstLevel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lstLevel.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lstLevel.Items.AddRange(new object[11]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"Summary"
		});
		this.lstLevel.Location = new System.Drawing.Point(175, 326);
		this.lstLevel.Name = "lstLevel";
		this.lstLevel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lstLevel.Size = new System.Drawing.Size(262, 26);
		this.lstLevel.TabIndex = 4;
		this.lstLevel.Text = "0";
		this.txtComment.AcceptsReturn = true;
		this.txtComment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtComment.BackColor = System.Drawing.SystemColors.Window;
		this.txtComment.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.txtComment.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtComment.ForeColor = System.Drawing.SystemColors.WindowText;
		this.txtComment.Location = new System.Drawing.Point(4, 297);
		this.txtComment.MaxLength = 0;
		this.txtComment.Name = "txtComment";
		this.txtComment.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtComment.Size = new System.Drawing.Size(434, 26);
		this.txtComment.TabIndex = 3;
		this.lbldesc.AcceptsReturn = true;
		this.lbldesc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lbldesc.BackColor = System.Drawing.SystemColors.ScrollBar;
		this.lbldesc.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.lbldesc.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldesc.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
		this.lbldesc.Location = new System.Drawing.Point(4, 530);
		this.lbldesc.MaxLength = 0;
		this.lbldesc.Multiline = true;
		this.lbldesc.Name = "lbldesc";
		this.lbldesc.ReadOnly = true;
		this.lbldesc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbldesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.lbldesc.Size = new System.Drawing.Size(434, 78);
		this.lbldesc.TabIndex = 28;
		this.lbldesc.TabStop = false;
		this.lbldesc.Text = "lbldesc";
		this.lstgeneral.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstgeneral.BackColor = System.Drawing.SystemColors.Window;
		this.lstgeneral.Cursor = System.Windows.Forms.Cursors.Default;
		this.lstgeneral.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lstgeneral.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lstgeneral.ItemHeight = 18;
		this.lstgeneral.Location = new System.Drawing.Point(175, 378);
		this.lstgeneral.Name = "lstgeneral";
		this.lstgeneral.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lstgeneral.Size = new System.Drawing.Size(262, 148);
		this.lstgeneral.Sorted = true;
		this.lstgeneral.TabIndex = 5;
		this.TxtExpression.AcceptsReturn = true;
		this.TxtExpression.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtExpression.BackColor = System.Drawing.SystemColors.Window;
		this.TxtExpression.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtExpression.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtExpression.ForeColor = System.Drawing.Color.Black;
		this.TxtExpression.Location = new System.Drawing.Point(4, 112);
		this.TxtExpression.MaxLength = 0;
		this.TxtExpression.Multiline = true;
		this.TxtExpression.Name = "TxtExpression";
		this.TxtExpression.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtExpression.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.TxtExpression.Size = new System.Drawing.Size(434, 164);
		this.TxtExpression.TabIndex = 2;
		this.lblFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblFilter.AutoSize = true;
		this.lblFilter.BackColor = System.Drawing.SystemColors.Control;
		this.lblFilter.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblFilter.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblFilter.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblFilter.Location = new System.Drawing.Point(4, 329);
		this.lblFilter.Name = "lblFilter";
		this.lblFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblFilter.Size = new System.Drawing.Size(175, 18);
		this.lblFilter.TabIndex = 35;
		this.lblFilter.Text = "Create Column at Level:";
		this.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label4.AutoSize = true;
		this.Label4.BackColor = System.Drawing.SystemColors.Control;
		this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label4.Location = new System.Drawing.Point(4, 281);
		this.Label4.Name = "Label4";
		this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label4.Size = new System.Drawing.Size(88, 18);
		this.Label4.TabIndex = 34;
		this.Label4.Text = "Description";
		this.Label8.AutoSize = true;
		this.Label8.BackColor = System.Drawing.Color.Transparent;
		this.Label8.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label8.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label8.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Label8.Location = new System.Drawing.Point(4, 3);
		this.Label8.Name = "Label8";
		this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label8.Size = new System.Drawing.Size(111, 19);
		this.Label8.TabIndex = 33;
		this.Label8.Text = "Column Name";
		this.Label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Label6.AutoSize = true;
		this.Label6.BackColor = System.Drawing.Color.Transparent;
		this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label6.Location = new System.Drawing.Point(4, 503);
		this.Label6.Name = "Label6";
		this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label6.Size = new System.Drawing.Size(57, 18);
		this.Label6.TabIndex = 31;
		this.Label6.Text = "Object:";
		this.lblgeneral.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblgeneral.AutoSize = true;
		this.lblgeneral.BackColor = System.Drawing.Color.Transparent;
		this.lblgeneral.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblgeneral.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblgeneral.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblgeneral.Location = new System.Drawing.Point(175, 359);
		this.lblgeneral.Name = "lblgeneral";
		this.lblgeneral.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblgeneral.Size = new System.Drawing.Size(69, 18);
		this.lblgeneral.TabIndex = 32;
		this.lblgeneral.Text = "Columns";
		this.lblexpression.AutoSize = true;
		this.lblexpression.BackColor = System.Drawing.Color.Transparent;
		this.lblexpression.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblexpression.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblexpression.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblexpression.Location = new System.Drawing.Point(4, 96);
		this.lblexpression.Name = "lblexpression";
		this.lblexpression.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblexpression.Size = new System.Drawing.Size(89, 19);
		this.lblexpression.TabIndex = 30;
		this.lblexpression.Text = "Expression";
		this.Label1.AutoSize = true;
		this.Label1.BackColor = System.Drawing.Color.Transparent;
		this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.Color.Black;
		this.Label1.Location = new System.Drawing.Point(4, 48);
		this.Label1.Name = "Label1";
		this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label1.Size = new System.Drawing.Size(83, 19);
		this.Label1.TabIndex = 29;
		this.Label1.Text = "Data Type";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.cmdclose;
		base.ClientSize = new System.Drawing.Size(441, 611);
		base.Controls.Add(this.cmdSet);
		base.Controls.Add(this.cmdCopy);
		base.Controls.Add(this.lstLevel);
		base.Controls.Add(this.txtComment);
		base.Controls.Add(this.lbldesc);
		base.Controls.Add(this.CmdHelp);
		base.Controls.Add(this.Text2);
		base.Controls.Add(this.CmbDataType);
		base.Controls.Add(this.cmdsummary);
		base.Controls.Add(this.Combo2);
		base.Controls.Add(this.lstgeneral);
		base.Controls.Add(this.cmdoperators);
		base.Controls.Add(this.cmdfunctions);
		base.Controls.Add(this.cmdcolumns);
		base.Controls.Add(this.Command5);
		base.Controls.Add(this.cmd2);
		base.Controls.Add(this.cmdexpr);
		base.Controls.Add(this.cmdConcat);
		base.Controls.Add(this.TxtExpression);
		base.Controls.Add(this.cmdAdd);
		base.Controls.Add(this.cmdclose);
		base.Controls.Add(this.lblFilter);
		base.Controls.Add(this.Label4);
		base.Controls.Add(this.Label8);
		base.Controls.Add(this.Label6);
		base.Controls.Add(this.lblgeneral);
		base.Controls.Add(this.lblexpression);
		base.Controls.Add(this.Label1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.WindowText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(188, 129);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmComputedSQL";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Computed Columns";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public string Replace_CompExpr(string MyMode, string MyCol, string FunctionType)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		int num2 = default(int);
		int num = default(int);
		string result;
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string find;
					string replacement;
					string text2;
					short g_NoQColumns3;
					switch (try0001_dispatch)
					{
					default:
					{
						errsource = "FrmComputedSQL - Replace_CompExpr";
						string text = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						find = "";
						replacement = "";
						text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						result = "";
						MyMode = Strings.UCase(MyMode);
						FunctionType = Strings.UCase(FunctionType);
						if (Operators.CompareString(MyMode, "ID", TextCompare: false) != 0)
						{
							goto IL_0396;
						}
						text3 = lstLevel.Text;
						if ((Operators.CompareString(Strings.UCase(text3), "SUMMARY", TextCompare: false) == 0) & (Operators.CompareString(FunctionType, "NONE", TextCompare: false) == 0))
						{
							text2 = "You have created a non-summary expression at the Summary level.";
						}
						else
						{
							text4 = Strings.LCase(General_Procedures.Get_Obj_Alias("F->T", Combo2.Text));
							if (Operators.CompareString(Strings.UCase(text3), "SUMMARY", TextCompare: false) == 0)
							{
								text3 = "100";
							}
							else if (Operators.CompareString(Strings.UCase(Strings.Mid(text3 + "          ", 1, 10)), "SUMMARY + ", TextCompare: false) == 0)
							{
								text3 = Conversions.ToString(100 + Conversions.ToInteger(Strings.Mid(text3, 11, 1)));
							}
							if (Operators.CompareString(text4, "all", TextCompare: false) == 0)
							{
								text3 = Conversions.ToString(200.0 + Conversion.Val(text3));
							}
							if (Operators.CompareString(text4, "all", TextCompare: false) != 0)
							{
								short g_NoQColumns = Globals_Renamed.g_NoQColumns;
								for (num3 = 0; num3 <= g_NoQColumns; num3 = (short)unchecked(num3 + 1))
								{
									if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num3].ColAlias), text4, TextCompare: false) != 0)
									{
										text5 = "{" + Globals_Renamed.g_QColumns[num3].Header + "}";
										if (Strings.InStr(MyCol, text5) != 0)
										{
											text2 = "Column " + text5 + " belonging to object alias (" + General_Procedures.Get_Obj_Alias("T->F", Globals_Renamed.g_QColumns[num3].ColAlias) + ") cannot be used to create expressions with different object aliases, (i.e., " + General_Procedures.Get_Obj_Alias("T->F", text4) + " in this case).";
											break;
										}
									}
								}
							}
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								short g_NoQColumns2 = Globals_Renamed.g_NoQColumns;
								for (num3 = 0; num3 <= g_NoQColumns2; num3 = (short)unchecked(num3 + 1))
								{
									if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num3].ColAlias), text4, TextCompare: false) == 0)
									{
										text5 = "{" + Globals_Renamed.g_QColumns[num3].Header + "}";
										if (Strings.InStr(MyCol, text5) != 0 && Conversions.ToShort(text3) < Conversions.ToShort(Globals_Renamed.g_QColumns[num3].level))
										{
											text2 = "Column " + text5 + " is at a higher level (" + Globals_Renamed.g_QColumns[num3].level + ") than the computed expression being built (" + text3 + "). Computed Expression must use columns at the same or lower levels.";
											break;
										}
									}
								}
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									goto IL_0396;
								}
							}
						}
						goto IL_04f6;
					}
					case 1341:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								result = "@ERROR@";
								Information.Err().Clear();
								goto end_IL_0001;
							}
							break;
						}
						IL_0396:
						g_NoQColumns3 = Globals_Renamed.g_NoQColumns;
						for (short num3 = 0; num3 <= g_NoQColumns3; num3 = (short)unchecked(num3 + 1))
						{
							string left = MyMode;
							if (Operators.CompareString(left, "ID", TextCompare: false) != 0)
							{
								if (Operators.CompareString(left, "HDR", TextCompare: false) == 0)
								{
									find = "{" + Globals_Renamed.g_QColumns[num3].ColID + "}";
									replacement = "{" + Globals_Renamed.g_QColumns[num3].Header + "}";
								}
							}
							else
							{
								find = "{" + Globals_Renamed.g_QColumns[num3].Header + "}";
								replacement = "{" + Globals_Renamed.g_QColumns[num3].ColID + "}";
							}
							MyCol = Strings.Replace(MyCol, find, replacement, 1, -1, CompareMethod.Text);
						}
						if (Operators.CompareString(MyMode, "ID", TextCompare: false) == 0)
						{
							MyCol = Strings.Replace(MyCol, "{col", "\0\0\0", 1, -1, CompareMethod.Text);
							if (LikeOperator.LikeString(MyCol, "*{*}*", CompareMethod.Binary))
							{
								string text5 = General_Procedures.Get_Node_Value(MyCol);
								text2 = "There appears to be an unknown token bounded by squiggly brackets {..} in the computed expression, i.e., {" + text5 + "}. Please remove.";
								goto IL_04f6;
							}
							MyCol = Strings.Replace(MyCol, "\0\0\0", "{col", 1, -1, CompareMethod.Text);
							result = MyCol;
							goto end_IL_0001;
						}
						result = MyCol;
						goto end_IL_0001;
						IL_04f6:
						Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, "Problem With Computed Expression");
						result = "@ERROR@";
						goto end_IL_0001;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1341;
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

	public void Load_QColumns()
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						errsource = "FrmComputedSQL - Load_QColumns";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						bool flag = false;
						short num6 = 0;
						string text = "";
						short num7 = 0;
						int num8 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						num3 = -1;
						Array.Clear(Globals_Renamed.g_QColumns, 0, Globals_Renamed.g_QColumns.Length);
						Globals_Renamed.g_NoQColumns = -1;
						switch (Globals_Renamed.currinttmp)
						{
						case 4:
						case 7:
						{
							text = ((Globals_Renamed.currinttmp != 4) ? "R" : "JMP");
							string[] ColArr = new string[0];
							if (BuildChart.Get_JMP_or_R_Columns(text, Globals_Renamed.currlisttmp, ref ColArr))
							{
								short num10 = (short)Information.UBound(ColArr);
								for (num4 = 1; num4 <= num10; num4 = (short)unchecked(num4 + 1))
								{
									Globals_Renamed.g_QColumns[num4 - 1].Column = ColArr[num4];
									Globals_Renamed.g_QColumns[num4 - 1].Header = ColArr[num4];
									Globals_Renamed.g_QColumns[num4 - 1].Show = "Y";
									Globals_Renamed.g_QColumns[num4 - 1].level = "0";
									if (Globals_Renamed.currinttmp == 4)
									{
										Globals_Renamed.g_QColumns[num4 - 1].ColAlias = "JMP";
									}
									else
									{
										Globals_Renamed.g_QColumns[num4 - 1].ColAlias = "R";
									}
								}
								if (Information.UBound(ColArr) >= 1)
								{
									Globals_Renamed.g_NoQColumns = (short)(Information.UBound(ColArr) - 1);
								}
							}
							ColArr = null;
							break;
						}
						case 8:
						{
							string[] array = Strings.Split(Globals_Renamed.currlisttmp, ",");
							short num11 = (short)Information.UBound(array);
							for (num4 = 0; num4 <= num11; num4 = (short)unchecked(num4 + 1))
							{
								num8 = Strings.InStrRev(array[num4], " (");
								text = ((num8 == 0) ? Strings.Trim(array[num4]) : Strings.Trim(Strings.Mid(array[num4], 1, num8 - 1)));
								Globals_Renamed.g_QColumns[num4].Column = text;
								Globals_Renamed.g_QColumns[num4].Header = text;
								Globals_Renamed.g_QColumns[num4].Show = "Y";
								Globals_Renamed.g_QColumns[num4].level = "0";
								Globals_Renamed.g_QColumns[num4].ColAlias = "R";
							}
							if (Information.UBound(array) >= 0)
							{
								Globals_Renamed.g_NoQColumns = (short)Information.UBound(array);
							}
							array = null;
							break;
						}
						case 6:
						case 10:
						case 11:
						{
							string[] lCSVCols = new string[0];
							if (General_Procedures.GetCSVHeaders(Globals_Renamed.currlisttmp, ref lCSVCols) == 0)
							{
								short num12 = (short)Information.UBound(lCSVCols);
								for (num4 = 0; num4 <= num12; num4 = (short)unchecked(num4 + 1))
								{
									if (Globals_Renamed.currinttmp == 10)
									{
										num8 = Strings.InStr(lCSVCols[num4], ":");
										if (num8 != 0)
										{
											lCSVCols[num4] = Strings.Trim(Strings.Mid(lCSVCols[num4] + " ", num8 + 1));
										}
									}
									Globals_Renamed.g_QColumns[num4].Column = lCSVCols[num4];
									Globals_Renamed.g_QColumns[num4].Header = lCSVCols[num4];
									Globals_Renamed.g_QColumns[num4].Show = "Y";
									Globals_Renamed.g_QColumns[num4].level = "0";
									Globals_Renamed.g_QColumns[num4].ColAlias = "REPORT";
								}
								if (Information.UBound(lCSVCols) >= 1)
								{
									Globals_Renamed.g_NoQColumns = (short)Information.UBound(lCSVCols);
								}
							}
							lCSVCols = null;
							break;
						}
						default:
						{
							num5 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
							short num9 = num5;
							for (num4 = 0; num4 <= num9; num4 = (short)unchecked(num4 + 1))
							{
								text = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[11].Value));
								text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[2].Value);
								if (Operators.CompareString(text3, "--", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text.Substring(text.Length - 2), ".*", TextCompare: false) != 0 && !BuildForm.IsColPattern(text))
								{
									text2 = Strings.LCase(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[1].Value)));
									if (Operators.CompareString(text2, "", TextCompare: false) != 0)
									{
										num3++;
										Globals_Renamed.g_QColumns[num3].Header = text2;
										Globals_Renamed.g_QColumns[num3].Column = text;
										Globals_Renamed.g_QColumns[num3].ColAlias = General_Procedures.Strip_Alias(1, Globals_Renamed.g_QColumns[num3].Column);
										Globals_Renamed.g_QColumns[num3].Sort = Strings.UCase(General_Procedures.Set_Get_Fn("G", "C", Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[0].Value), ""));
										Globals_Renamed.g_QColumns[num3].Show = text3;
										Globals_Renamed.g_QColumns[num3].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[4].Value);
										Globals_Renamed.g_QColumns[num3].Pivot = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[5].Value);
										Globals_Renamed.g_QColumns[num3].List = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[10].Value);
										Globals_Renamed.g_QColumns[num3].ColID = Strings.LCase(Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[12].Value)));
										num7 = General_Procedures.SummPlusLevel(Globals_Renamed.g_QColumns[num3].List);
										if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num3].Statistics), "expr", TextCompare: false) == 0)
										{
											Globals_Renamed.g_QColumns[num3].level = "100";
										}
										else if (num7 > 0)
										{
											Globals_Renamed.g_QColumns[num3].level = Conversions.ToString(100 + num7);
										}
										else if (Operators.CompareString(Strings.UCase(Strings.Mid(Globals_Renamed.g_QColumns[num3].List, 1, Strings.Len("@IF@"))), "@IF@", TextCompare: false) == 0)
										{
											text4 = Strings.Mid(Globals_Renamed.g_QColumns[num3].List, Strings.Len("@IF@") + 1);
											if ((Operators.CompareString(Strings.Mid(text4 + " ", 1, 1), "0", TextCompare: false) < 0) | (Operators.CompareString(Strings.Mid(text4 + " ", 1, 1), "9", TextCompare: false) > 0))
											{
												text4 = "1";
											}
											Globals_Renamed.g_QColumns[num3].level = Conversions.ToString(unchecked((int)Conversions.ToShort(text4)));
										}
										else if ((Globals_Renamed.currinttmp == 1 || Globals_Renamed.currinttmp == 0) && (LikeOperator.LikeString(Strings.UCase(text), "*SELECT* FROM*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(text), "* OVER*PARTITION *BY*", CompareMethod.Binary)))
										{
											Globals_Renamed.g_QColumns[num3].Sort = "Y";
											Globals_Renamed.g_QColumns[num3].level = "0";
										}
										else
										{
											Globals_Renamed.g_QColumns[num3].level = "0";
										}
										if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num3].ColAlias), "all", TextCompare: false) == 0)
										{
											Globals_Renamed.g_QColumns[num3].level = Conversions.ToString(200 + Conversions.ToInteger(Globals_Renamed.g_QColumns[num3].level));
										}
										if (Operators.CompareString(Globals_Renamed.g_QColumns[num3].Sort, "Y", TextCompare: false) == 0)
										{
											Globals_Renamed.g_QColumns[num3].level = Conversions.ToString(Conversion.Val(Globals_Renamed.g_QColumns[num3].level) + 1.0);
										}
									}
								}
							}
							Globals_Renamed.g_NoQColumns = num3;
							break;
						}
						}
						goto end_IL_0001;
					}
					case 2648:
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
					goto IL_0a8e;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2648;
				continue;
			}
			break;
			IL_0a8e:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string Get_Grid_Attrib(string MyMode, string InHdr, ref string MyTrueCol)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string result;
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
						errsource = "FrmComputedSQL - Get_Grid_Attrib";
						result = "";
						short index = 0;
						short num3 = 0;
						short num4 = 0;
						bool flag = false;
						string text = "";
						string text2 = "";
						MyTrueCol = "";
						MyMode = Strings.Trim(Strings.UCase(MyMode));
						string left = MyMode;
						if (Operators.CompareString(left, "C", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "L", TextCompare: false) == 0)
							{
								index = 10;
							}
						}
						else
						{
							index = 9;
						}
						flag = false;
						num4 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
						short num5 = num4;
						for (num3 = 0; num3 <= num5; num3 = (short)unchecked(num3 + 1))
						{
							object[] array;
							DataGridViewCell dataGridViewCell;
							bool[] array2;
							object left2 = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1]).Value }, null, null, array2 = new bool[1] { true });
							if (array2[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							if (Operators.ConditionalCompareObjectEqual(left2, Strings.LCase(InHdr), TextCompare: false))
							{
								text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[index].Value);
								if (Operators.CompareString(MyMode, "L", TextCompare: false) == 0)
								{
									MyTrueCol = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value);
								}
								flag = true;
								break;
							}
							if (Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value, "", TextCompare: false))
							{
								text = General_Procedures.Strip_Alias(0, text);
								text = General_Procedures.Strip_Column(text, 1);
								if (LikeOperator.LikeString(Strings.UCase(General_Procedures.Get_Obj_Alias("F->T", Combo2.Text)), "ALL*", CompareMethod.Binary))
								{
									text = General_Procedures.Strip_CSV_Square(text, 1);
								}
								if (Operators.CompareString(text, InHdr, TextCompare: false) == 0)
								{
									text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[index].Value);
									if (Operators.CompareString(MyMode, "L", TextCompare: false) == 0)
									{
										MyTrueCol = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value);
									}
									flag = true;
									break;
								}
							}
						}
						if (flag)
						{
							string left3 = MyMode;
							result = ((Operators.CompareString(left3, "C", TextCompare: false) != 0) ? text2 : ((Operators.CompareString(Strings.Mid(text2, 1, 3), "***", TextCompare: false) != 0) ? text2 : Strings.Trim(Strings.Mid(text2 + " ", 4))));
						}
						else if (Operators.CompareString(MyMode, "C", TextCompare: false) == 0)
						{
							result = "Unknown";
						}
						goto end_IL_0001;
					}
					case 1005:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = "";
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1005;
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

	public void Assign_LstLevel()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string str = default(string);
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
				case 225:
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
							goto IL_004f;
						case 5:
							goto IL_0079;
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
					IL_001e:
					num2 = 3;
					if (Operators.CompareString(Strings.Trim(Strings.UCase(Strings.Mid(str, 1, Strings.Len("@IF@")))), "@IF@", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_004f;
					IL_004f:
					num2 = 4;
					lstLevel.Text = Strings.Trim(Strings.UCase(Strings.Mid(str, checked(Strings.Len("@IF@") + 1))));
					goto IL_0079;
					IL_000b:
					num2 = 2;
					str = GlobalListColumn + "               ";
					goto IL_001e;
					IL_0079:
					num2 = 5;
					if (Operators.CompareString(lstLevel.Text, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				lstLevel.Text = "1";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 225;
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

	public void Set_DataType(string MyDT, short MyMode)
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
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 2659:
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
							goto IL_0023;
						case 7:
							goto IL_0062;
						case 9:
							goto IL_0362;
						case 10:
							goto IL_037b;
						case 11:
							goto IL_0394;
						case 12:
							goto IL_03ad;
						case 13:
							goto IL_03c6;
						case 14:
							goto IL_03df;
						case 15:
							goto IL_03f8;
						case 16:
							goto IL_040f;
						case 19:
							goto IL_0430;
						case 21:
							goto IL_0446;
						case 23:
							goto IL_0459;
						case 25:
							goto IL_046c;
						case 27:
							goto IL_047f;
						case 29:
							goto IL_0492;
						case 31:
							goto IL_04a5;
						case 33:
							goto IL_04b8;
						case 36:
							goto IL_04d2;
						case 38:
							goto IL_051a;
						case 39:
							goto IL_0533;
						case 40:
							goto IL_054c;
						case 42:
							goto IL_0568;
						case 44:
							goto IL_057b;
						case 46:
							goto IL_058e;
						case 48:
							goto IL_05a1;
						case 51:
							goto IL_05bb;
						case 53:
							goto IL_0603;
						case 54:
							goto IL_061c;
						case 55:
							goto IL_0635;
						case 57:
							goto IL_0651;
						case 59:
							goto IL_0664;
						case 61:
							goto IL_0677;
						case 63:
							goto IL_068a;
						case 66:
							goto IL_06a4;
						case 68:
							goto IL_06bf;
						case 69:
							goto IL_06d8;
						case 70:
							goto IL_06f1;
						case 71:
							goto IL_070a;
						case 72:
							goto IL_0723;
						case 73:
							goto IL_073c;
						case 74:
							goto IL_0755;
						case 75:
							goto IL_076e;
						case 76:
							goto IL_078f;
						case 78:
							goto IL_079d;
						case 79:
							goto IL_07d0;
						case 77:
						case 80:
						case 81:
							goto IL_07dd;
						case 82:
							goto IL_07f6;
						case 83:
							goto IL_080f;
						case 84:
							goto IL_0828;
						case 85:
							goto IL_0841;
						case 88:
							goto IL_085f;
						case 89:
							goto IL_087a;
						case 90:
							goto end_IL_0001_2;
						case 92:
						case 93:
							goto IL_08bc;
						default:
							goto end_IL_0001;
						case 6:
						case 8:
						case 17:
						case 18:
						case 20:
						case 22:
						case 24:
						case 26:
						case 28:
						case 30:
						case 32:
						case 34:
						case 35:
						case 37:
						case 41:
						case 43:
						case 45:
						case 47:
						case 49:
						case 50:
						case 52:
						case 56:
						case 58:
						case 60:
						case 62:
						case 64:
						case 65:
						case 67:
						case 86:
						case 87:
						case 91:
						case 94:
						case 95:
						case 96:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_03df:
					num2 = 14;
					CmbDataType.Items.Add("Cleansed String");
					goto IL_03f8;
					IL_03f8:
					num2 = 15;
					if (!(MyMode == 1 || MyMode == 3 || MyMode == 5))
					{
						goto end_IL_0001_3;
					}
					goto IL_040f;
					IL_03c6:
					num2 = 13;
					CmbDataType.Items.Add("Quoted String");
					goto IL_03df;
					IL_040f:
					num2 = 16;
					CmbDataType.Items.Add("Expression");
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
					MyDT = Strings.UCase(MyDT);
					goto IL_0023;
					IL_0023:
					num2 = 5;
					switch (MyMode)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 5:
					case 6:
						break;
					case 4:
						goto IL_04d2;
					case 7:
					case 8:
					case 10:
						goto IL_05bb;
					case 9:
						goto IL_06a4;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0062;
					IL_06a4:
					num2 = 66;
					left = MyDT;
					if (Operators.CompareString(left, "LOAD", TextCompare: false) == 0)
					{
						goto IL_06bf;
					}
					goto IL_085f;
					IL_085f:
					num2 = 88;
					num6 = checked(CmbDataType.Items.Count - 1);
					num5 = 0;
					goto IL_08c3;
					IL_08c3:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_087a;
					IL_087a:
					num2 = 89;
					if (Operators.CompareString(MyDT, Strings.UCase(CmbDataType.Items[num5].ToString()), TextCompare: false) == 0)
					{
						break;
					}
					goto IL_08bc;
					IL_08bc:
					num2 = 93;
					num5 = checked(num5 + 1);
					goto IL_08c3;
					IL_06bf:
					num2 = 68;
					CmbDataType.Items.Add("Starts With");
					goto IL_06d8;
					IL_06d8:
					num2 = 69;
					CmbDataType.Items.Add("Ends With");
					goto IL_06f1;
					IL_06f1:
					num2 = 70;
					CmbDataType.Items.Add("Contains");
					goto IL_070a;
					IL_070a:
					num2 = 71;
					CmbDataType.Items.Add("Not Starts With");
					goto IL_0723;
					IL_0723:
					num2 = 72;
					CmbDataType.Items.Add("In File");
					goto IL_073c;
					IL_073c:
					num2 = 73;
					CmbDataType.Items.Add("Starts/Ends With (%)");
					goto IL_0755;
					IL_0755:
					num2 = 74;
					CmbDataType.Items.Add("Regex");
					goto IL_076e;
					IL_076e:
					num2 = 75;
					if (Operators.CompareString(Strings.LCase(Globals_Renamed.currDataAny), "all", TextCompare: false) == 0)
					{
						goto IL_078f;
					}
					goto IL_079d;
					IL_078f:
					num2 = 76;
					text = "SQLite";
					goto IL_07dd;
					IL_079d:
					num2 = 78;
					text = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + Globals_Renamed.currDataAny);
					goto IL_07d0;
					IL_07d0:
					num2 = 79;
					text = General_Procedures.Get_Node_Value(text);
					goto IL_07dd;
					IL_07dd:
					num2 = 81;
					if (Operators.CompareString(text, "Mongo", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_07f6;
					IL_07f6:
					num2 = 82;
					CmbDataType.Items.Add("Regex File");
					goto IL_080f;
					IL_080f:
					num2 = 83;
					CmbDataType.Items.Add("Function File");
					goto IL_0828;
					IL_0828:
					num2 = 84;
					CmbDataType.Items.Add("Not Regex");
					goto IL_0841;
					IL_0841:
					num2 = 85;
					CmbDataType.Items.Add("Not Regex File");
					goto end_IL_0001_3;
					IL_05bb:
					num2 = 51;
					switch (MyDT)
					{
					case "LOAD":
						break;
					case "C":
						goto IL_0651;
					case "N":
						goto IL_0664;
					case "D":
						goto IL_0677;
					default:
						goto IL_068a;
					}
					goto IL_0603;
					IL_068a:
					num2 = 63;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_0677:
					num2 = 61;
					CmbDataType.SelectedIndex = 2;
					goto end_IL_0001_3;
					IL_0664:
					num2 = 59;
					CmbDataType.SelectedIndex = 1;
					goto end_IL_0001_3;
					IL_0651:
					num2 = 57;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_0603:
					num2 = 53;
					CmbDataType.Items.Add("Character");
					goto IL_061c;
					IL_061c:
					num2 = 54;
					CmbDataType.Items.Add("Numeric");
					goto IL_0635;
					IL_0635:
					num2 = 55;
					CmbDataType.Items.Add("Date");
					goto end_IL_0001_3;
					IL_04d2:
					num2 = 36;
					switch (MyDT)
					{
					case "LOAD":
						break;
					case "CN":
						goto IL_0568;
					case "CO":
						goto IL_057b;
					case "NC":
						goto IL_058e;
					default:
						goto IL_05a1;
					}
					goto IL_051a;
					IL_05a1:
					num2 = 48;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_058e:
					num2 = 46;
					CmbDataType.SelectedIndex = 2;
					goto end_IL_0001_3;
					IL_057b:
					num2 = 44;
					CmbDataType.SelectedIndex = 1;
					goto end_IL_0001_3;
					IL_0568:
					num2 = 42;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_051a:
					num2 = 38;
					CmbDataType.Items.Add("Character - Nominal");
					goto IL_0533;
					IL_0533:
					num2 = 39;
					CmbDataType.Items.Add("Character - Ordinal");
					goto IL_054c;
					IL_054c:
					num2 = 40;
					CmbDataType.Items.Add("Numeric - Continuous");
					goto end_IL_0001_3;
					IL_0062:
					num2 = 7;
					switch (MyDT)
					{
					case "LOAD":
						break;
					case "C":
						goto IL_0430;
					case "N":
						goto IL_0446;
					case "D":
					case "T":
					case "G":
					case "H":
					case "U":
					case "M":
					case "O":
					case "L":
					case "P":
					case "V":
					case "W":
						goto IL_0459;
					case "F":
						goto IL_046c;
					case "Q":
						goto IL_047f;
					case "X":
						goto IL_0492;
					case "E":
						goto IL_04a5;
					default:
						goto IL_04b8;
					}
					goto IL_0362;
					IL_04b8:
					num2 = 33;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_04a5:
					num2 = 31;
					CmbDataType.SelectedIndex = 6;
					goto end_IL_0001_3;
					IL_0492:
					num2 = 29;
					CmbDataType.SelectedIndex = 5;
					goto end_IL_0001_3;
					IL_047f:
					num2 = 27;
					CmbDataType.SelectedIndex = 4;
					goto end_IL_0001_3;
					IL_046c:
					num2 = 25;
					CmbDataType.SelectedIndex = 3;
					goto end_IL_0001_3;
					IL_0459:
					num2 = 23;
					CmbDataType.SelectedIndex = 2;
					goto end_IL_0001_3;
					IL_0446:
					num2 = 21;
					CmbDataType.SelectedIndex = 1;
					goto end_IL_0001_3;
					IL_0430:
					num2 = 19;
					CmbDataType.SelectedIndex = 0;
					goto end_IL_0001_3;
					IL_0362:
					num2 = 9;
					CmbDataType.Items.Add("Character");
					goto IL_037b;
					IL_037b:
					num2 = 10;
					CmbDataType.Items.Add("Numeric");
					goto IL_0394;
					IL_0394:
					num2 = 11;
					CmbDataType.Items.Add("Date");
					goto IL_03ad;
					IL_03ad:
					num2 = 12;
					CmbDataType.Items.Add("Float");
					goto IL_03c6;
					end_IL_0001_2:
					break;
				}
				num2 = 90;
				CmbDataType.SelectedIndex = num5;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2659;
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

	public void Load_CColumns()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		string text = default(string);
		short num5 = default(short);
		string text2 = default(string);
		short num6 = default(short);
		short g_NoQColumns = default(short);
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
					case 2086:
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
								goto IL_0026;
							case 7:
								goto IL_002f;
							case 8:
								goto IL_0042;
							case 9:
								goto IL_0055;
							case 10:
								goto IL_0066;
							case 11:
								goto IL_0070;
							case 13:
								goto IL_007c;
							case 14:
								goto IL_0096;
							case 15:
								goto IL_00a0;
							case 17:
								goto IL_00ac;
							case 18:
								goto IL_00d2;
							case 19:
								goto IL_00dc;
							case 21:
								goto IL_00e9;
							case 22:
								goto IL_010d;
							case 23:
								goto IL_014f;
							case 25:
								goto IL_016b;
							case 26:
								goto IL_0192;
							case 28:
								goto IL_019c;
							case 29:
								goto IL_01d5;
							case 31:
								goto IL_01fa;
							case 24:
							case 27:
							case 30:
							case 32:
							case 33:
								goto IL_0201;
							case 34:
								goto IL_021a;
							case 12:
							case 16:
							case 20:
							case 35:
							case 36:
							case 37:
								goto IL_022b;
							case 38:
								goto IL_023c;
							case 40:
								goto IL_02f2;
							case 42:
								goto IL_0334;
							case 43:
								goto IL_0359;
							case 44:
								goto IL_0375;
							case 45:
								goto IL_03ae;
							case 46:
								goto IL_03b8;
							case 47:
								goto IL_040e;
							case 49:
								goto IL_041c;
							case 48:
							case 50:
							case 51:
								goto IL_0427;
							case 52:
								goto IL_04e3;
							case 53:
								goto IL_051f;
							case 54:
								goto IL_055a;
							case 55:
								goto IL_057c;
							case 56:
								goto IL_05b4;
							case 58:
								goto IL_05f0;
							case 59:
								goto IL_0628;
							case 57:
							case 60:
							case 61:
								goto IL_0661;
							case 64:
								goto IL_0692;
							case 39:
							case 41:
							case 62:
							case 63:
							case 65:
							case 66:
							case 67:
								goto IL_06b9;
							case 68:
								goto IL_06c9;
							case 69:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 70:
							case 71:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_03ae:
						num2 = 45;
						left = "Y";
						goto IL_03b8;
						IL_03b8:
						num2 = 46;
						if (Operators.CompareString(text, "ALL", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Pivot, "Row", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Pivot, "Stack", TextCompare: false) != 0)
						{
							goto IL_040e;
						}
						goto IL_0427;
						IL_0628:
						num2 = 59;
						lstgeneral.Items.Add(":_label_" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + ":");
						goto IL_0661;
						IL_0661:
						num2 = 61;
						text2 = text2 + "<" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + ">";
						goto IL_06b9;
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
						left = "";
						goto IL_0021;
						IL_0021:
						num2 = 5;
						num6 = 0;
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text2 = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						lblgeneral.Text = "Available Columns";
						goto IL_0042;
						IL_0042:
						num2 = 8;
						lstgeneral.Items.Clear();
						goto IL_0055;
						IL_0055:
						num2 = 9;
						if (Globals_Renamed.currinttmp == 4)
						{
							goto IL_0066;
						}
						goto IL_007c;
						IL_0066:
						num2 = 10;
						text = "JMP";
						goto IL_0070;
						IL_0070:
						num2 = 11;
						num6 = 0;
						goto IL_022b;
						IL_007c:
						num2 = 13;
						if ((Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
						{
							goto IL_0096;
						}
						goto IL_00ac;
						IL_0096:
						num2 = 14;
						text = "R";
						goto IL_00a0;
						IL_00a0:
						num2 = 15;
						num6 = 0;
						goto IL_022b;
						IL_00ac:
						num2 = 17;
						if (Globals_Renamed.currinttmp == 6 || Globals_Renamed.currinttmp == 10 || Globals_Renamed.currinttmp == 11)
						{
							goto IL_00d2;
						}
						goto IL_00e9;
						IL_0692:
						num2 = 64;
						lstgeneral.Items.Add(Globals_Renamed.g_QColumns[num5].Header);
						goto IL_06b9;
						IL_040e:
						num2 = 47;
						left = "N";
						goto IL_0427;
						IL_00e9:
						num2 = 21;
						text = Strings.UCase(General_Procedures.Get_Obj_Alias("F->T", Combo2.SelectedItem.ToString()));
						goto IL_010d;
						IL_010d:
						num2 = 22;
						if ((Operators.CompareString(lstLevel.Text, "0", TextCompare: false) >= 0) & (Operators.CompareString(lstLevel.Text, "9", TextCompare: false) <= 0))
						{
							goto IL_014f;
						}
						goto IL_016b;
						IL_014f:
						num2 = 23;
						num6 = (short)Conversions.ToInteger(lstLevel.Text);
						goto IL_0201;
						IL_016b:
						num2 = 25;
						if (Operators.CompareString(Strings.UCase(lstLevel.Text), "SUMMARY", TextCompare: false) == 0)
						{
							goto IL_0192;
						}
						goto IL_019c;
						IL_0192:
						num2 = 26;
						num6 = 100;
						goto IL_0201;
						IL_019c:
						num2 = 28;
						if (Operators.CompareString(Strings.UCase(Strings.Mid(lstLevel.Text + "          ", 1, 10)), "SUMMARY + ", TextCompare: false) == 0)
						{
							goto IL_01d5;
						}
						goto IL_01fa;
						IL_01d5:
						num2 = 29;
						num6 = (short)(100 + Conversions.ToInteger(Strings.Mid(lstLevel.Text, 11, 1)));
						goto IL_0201;
						IL_01fa:
						num2 = 31;
						num6 = 1;
						goto IL_0201;
						IL_0201:
						num2 = 33;
						if (Operators.CompareString(text, "ALL", TextCompare: false) == 0)
						{
							goto IL_021a;
						}
						goto IL_022b;
						IL_021a:
						num2 = 34;
						num6 = (short)(200 + num6);
						goto IL_022b;
						IL_00d2:
						num2 = 18;
						text = "REPORT";
						goto IL_00dc;
						IL_00dc:
						num2 = 19;
						num6 = 0;
						goto IL_022b;
						IL_022b:
						num2 = 37;
						g_NoQColumns = Globals_Renamed.g_NoQColumns;
						num5 = 0;
						goto IL_06c1;
						IL_06c1:
						if (num5 <= g_NoQColumns)
						{
							goto IL_023c;
						}
						goto IL_06c9;
						IL_06c9:
						num2 = 68;
						if (Globals_Renamed.currinttmp != 5)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_023c:
						num2 = 38;
						if (!LikeOperator.LikeString(Strings.UCase(Globals_Renamed.currlisttmp), "*_IMBIGDATA", CompareMethod.Binary) && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Show, "Y:F", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Show, "N:F", TextCompare: false) != 0 && (Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num5].Show + "   ", 1, 3), "Y:F", TextCompare: false) != 0 || num6 <= 0) && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Show, "N:F1", TextCompare: false) != 0)
						{
							goto IL_02f2;
						}
						goto IL_06b9;
						IL_0427:
						num2 = 51;
						if (unchecked((short)(0 - (((Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num5].ColAlias), text, TextCompare: false) == 0) | ((Operators.CompareString(text, "ALL", TextCompare: false) == 0) & (Operators.CompareString(left, "N", TextCompare: false) != 0))) ? 1 : 0)) & ((short)(0 - ((Conversions.ToShort(Globals_Renamed.g_QColumns[num5].level) <= num6) ? 1 : 0)) & ~((short)(0 - (((num6 > 100) & (Operators.CompareString(left, "N", TextCompare: false) == 0)) ? 1 : 0)) & (short)(0 - ((Conversions.ToDouble(Globals_Renamed.g_QColumns[num5].level) < 100.0) ? 1 : 0))))) != 0)
						{
							goto IL_04e3;
						}
						goto IL_06b9;
						IL_04e3:
						num2 = 52;
						if (Operators.CompareString(text, "ALL", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Pivot, "Stack", TextCompare: false) == 0)
						{
							goto IL_051f;
						}
						goto IL_0692;
						IL_06b9:
						num2 = 67;
						num5 = (short)unchecked(num5 + 1);
						goto IL_06c1;
						IL_055a:
						num2 = 54;
						if (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) == 0)
						{
							goto IL_057c;
						}
						goto IL_05f0;
						IL_051f:
						num2 = 53;
						if (Strings.InStr(text2, "<" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + ">") == 0)
						{
							goto IL_055a;
						}
						goto IL_06b9;
						IL_02f2:
						num2 = 40;
						if (Operators.CompareString(l_DBType, "iMBigData", TextCompare: false) != 0 || Operators.CompareString(Globals_Renamed.g_QColumns[num5].Header, "all-fields", TextCompare: false) != 0)
						{
							goto IL_0334;
						}
						goto IL_06b9;
						IL_057c:
						num2 = 55;
						lstgeneral.Items.Add(":" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + "._data_:");
						goto IL_05b4;
						IL_0334:
						num2 = 42;
						if (num6 > 100 || Operators.CompareString(text, "ALL", TextCompare: false) == 0)
						{
							goto IL_0359;
						}
						goto IL_041c;
						IL_05b4:
						num2 = 56;
						lstgeneral.Items.Add(":" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + "._label_:");
						goto IL_0661;
						IL_041c:
						num2 = 49;
						left = "Y";
						goto IL_0427;
						IL_0359:
						num2 = 43;
						left = Strings.Mid(Globals_Renamed.g_QColumns[num5].Show, 1, 1);
						goto IL_0375;
						IL_0375:
						num2 = 44;
						if (num6 > 100 && num6 < 200 && Operators.CompareString(Globals_Renamed.g_QColumns[num5].Show, "N:S", TextCompare: false) == 0)
						{
							goto IL_03ae;
						}
						goto IL_03b8;
						IL_05f0:
						num2 = 58;
						lstgeneral.Items.Add(":_data_" + Strings.LCase(Globals_Renamed.g_QColumns[num5].ColAlias) + ":");
						goto IL_0628;
						end_IL_0001_2:
						break;
					}
					num2 = 69;
					lstgeneral.Items.Add("[MyPivotedColumn(s)]");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2086;
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

	public string Strip_JS_Comment(string MyExpr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		bool flag = default(bool);
		string result = default(string);
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
					case 482:
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
								goto IL_0010;
							case 4:
								goto IL_0015;
							case 5:
								goto IL_001a;
							case 8:
								goto IL_0029;
							case 9:
								goto IL_0039;
							case 10:
								goto IL_004a;
							case 11:
								goto IL_0061;
							case 13:
								goto IL_0099;
							case 14:
								goto IL_00b0;
							case 16:
								goto IL_00c2;
							case 17:
								goto IL_00d8;
							case 19:
								goto IL_00e5;
							case 20:
								goto IL_00fb;
							case 22:
								goto IL_010e;
							case 23:
								goto IL_0125;
							case 24:
								goto IL_012b;
							case 6:
							case 7:
							case 12:
							case 15:
							case 18:
							case 21:
							case 25:
							case 26:
								goto IL_0137;
							case 27:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 28:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_012b:
						num2 = 24;
						MyExpr = "ERROR";
						goto IL_0137;
						IL_00d8:
						num2 = 17;
						MyExpr = "";
						goto IL_0137;
						IL_00c2:
						num2 = 16;
						if (num5 == 1 && num6 == 0)
						{
							goto IL_00d8;
						}
						goto IL_00e5;
						IL_00e5:
						num2 = 19;
						if (num5 > 1 && num6 == 0)
						{
							goto IL_00fb;
						}
						goto IL_010e;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num6 = 0;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						flag = false;
						goto IL_001a;
						IL_001a:
						num2 = 5;
						MyExpr = Strings.Trim(MyExpr);
						goto IL_0137;
						IL_0137:
						num2 = 7;
						if (Strings.InStr(MyExpr, "/*") == 0 || flag)
						{
							break;
						}
						goto IL_0029;
						IL_0125:
						num2 = 23;
						flag = true;
						goto IL_012b;
						IL_0029:
						num2 = 8;
						num5 = Strings.InStr(MyExpr, "/*");
						goto IL_0039;
						IL_0039:
						num2 = 9;
						num6 = Strings.InStr(MyExpr, "*/");
						goto IL_004a;
						IL_004a:
						num2 = 10;
						if (num5 > 1 && num6 > num5)
						{
							goto IL_0061;
						}
						goto IL_0099;
						IL_00fb:
						num2 = 20;
						MyExpr = Strings.Mid(MyExpr, 1, num5 - 1);
						goto IL_0137;
						IL_0061:
						num2 = 11;
						MyExpr = Strings.Mid(Conversions.ToString(1), num5 - 1) + Strings.RTrim(Strings.Mid(MyExpr + " ", num6 + 2));
						goto IL_0137;
						IL_0099:
						num2 = 13;
						if (num5 == 1 && num6 > num5)
						{
							goto IL_00b0;
						}
						goto IL_00c2;
						IL_010e:
						num2 = 22;
						if (num5 > 1 && num6 < num5)
						{
							goto IL_0125;
						}
						goto IL_0137;
						IL_00b0:
						num2 = 14;
						MyExpr = Strings.Mid(MyExpr, num6 + 2);
						goto IL_0137;
						end_IL_0001_2:
						break;
					}
					num2 = 27;
					result = MyExpr;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 482;
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

	private void cmdadd_Click(object eventSender, EventArgs eventArgs)
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
					string text8;
					string text9;
					string text13;
					string text14;
					string text16;
					string text5;
					string text6;
					string text10;
					string text7;
					string text2;
					string text4;
					string text15;
					switch (try0001_dispatch)
					{
					default:
					{
						Globals_Renamed.QueryCancel = 0;
						if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0 && Globals_Renamed.currinttmp == 1)
						{
							Interaction.MsgBox("For Mongo Databases, computed Expressions cannot be created at the Filter Tab. To filter by an expression, create it at the Columns Tab and then add the expression to the Filters Tab", MsgBoxStyle.Exclamation, "Mongo DB Expressions");
							goto end_IL_0001;
						}
						if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0 && Operators.ConditionalCompareObjectEqual(lstLevel.SelectedItem, "Summary", TextCompare: false))
						{
							Interaction.MsgBox("Mongo Databases do not yet support summary expressions. To implement summary expressions, add the summary functions to columns on the Columns Tab and build the filter expressions at level Summary + 1", MsgBoxStyle.Exclamation, "Mongo DB Expressions");
							goto end_IL_0001;
						}
						string text = "";
						text2 = "";
						string text3 = "None";
						text4 = "";
						text5 = "";
						text6 = "";
						text7 = "";
						text8 = "";
						text9 = "";
						short num3 = 0;
						text10 = "";
						string text11 = "";
						string text12 = "";
						short num4 = 0;
						text13 = "";
						text14 = "";
						text15 = "";
						text16 = "";
						int num5 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						string text17 = "frmComputedSQL - cmdadd_Click";
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveQuery = 1;
						string text18 = "";
						text4 = Strings.Trim(TxtExpression.Text);
						text7 = Strings.UCase(text4);
						if (((Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 1 || Globals_Renamed.currinttmp == 9) && Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0 && LikeOperator.LikeString(GlobalListColumn, "@IF@".ToLower() + "*", CompareMethod.Binary)) & (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) == 0) & (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "TEXT", TextCompare: false) == 0 || Operators.CompareString(l_Alias, "ILV->", TextCompare: false) == 0))
						{
							if (Operators.CompareString(l_Alias, "ILV->", TextCompare: false) == 0)
							{
								GlobalListColumn = "ILV";
							}
							else
							{
								GlobalListColumn = "CSV";
							}
						}
						if (Operators.CompareString(Strings.Mid(Strings.UCase(GlobalListColumn) + "  ", 1, 2), "H=", TextCompare: false) == 0 && Operators.CompareString(lstLevel.Text, "0", TextCompare: false) > 0)
						{
							Interaction.MsgBox("For this View, computed columns can only be created at Level 0", MsgBoxStyle.Exclamation, "Invalid Expression");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if (Globals_Renamed.currinttmp == 3)
						{
							text4 = Strings.Replace(text4, Globals_Renamed.CRLF, " ", 1, -1, CompareMethod.Text);
							Globals_Renamed.currtxttmp = text4;
							Close();
							goto end_IL_0001;
						}
						if ((Globals_Renamed.currinttmp == 4) | (Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
						{
							text4 = Strings.Replace(text4, "\"", "'", 1, -1, CompareMethod.Text);
							goto IL_0411;
						}
						if (Globals_Renamed.currinttmp == 9)
						{
							text4 = Strings.Replace(text4, "\"", "'", 1, -1, CompareMethod.Text);
							text4 = Strings.Replace(text4, "\r\n", " ", 1, -1, CompareMethod.Text);
							text4 = BuildForm.Handle_Squiggly(text4);
							if (Operators.CompareString(text4, "ERROR", TextCompare: false) == 0)
							{
								TxtExpression.Focus();
								Globals_Renamed.QueryCancel = -1;
								goto end_IL_0001;
							}
							goto IL_0411;
						}
						text4 = Strings.Replace(text4, "\"", "'", 1, -1, CompareMethod.Text);
						text4 = Strings.Replace(text4, Globals_Renamed.CRLF, "!!!!!", 1, -1, CompareMethod.Text);
						if (Globals_Renamed.currinttmp != 1 && Globals_Renamed.currinttmp != 0)
						{
							goto IL_0411;
						}
						text4 = BuildForm.Handle_Squiggly(text4);
						if (Operators.CompareString(text4, "ERROR", TextCompare: false) != 0)
						{
							goto IL_0411;
						}
						TxtExpression.Focus();
						Globals_Renamed.QueryCancel = -1;
						goto end_IL_0001_2;
					}
					case 10065:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Interaction.MsgBox("Error building a Computed Expression. " + Conversion.ErrorToString() + ". Please retry.", MsgBoxStyle.Critical, "Unexpected Error");
								Information.Err().Clear();
								Globals_Renamed.QueryCancel = -1;
								goto end_IL_0001_2;
							}
							break;
						}
						IL_0411:
						if (Globals_Renamed.currinttmp != 9)
						{
							text8 = ((Operators.CompareString(CmbDataType.Text, "Cleansed String", TextCompare: false) != 0) ? Strings.LCase(Strings.Mid(CmbDataType.Text, 1, 1)) : "X");
							if ((Operators.CompareString(l_DBType, "SQLServer", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "IBI-DaaS", TextCompare: false) == 0) && Operators.CompareString(text8, "d", TextCompare: false) == 0)
							{
								text8 = "t";
							}
							else if ((Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "g";
							}
							else if ((Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "h";
							}
							else if ((Operators.CompareString(l_DBType, "Hadoop", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "u";
							}
							else if ((Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "o";
							}
							else if ((Operators.CompareString(l_DBType, "MySQL", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "l";
							}
							else if ((Operators.CompareString(l_DBType, "SAPHana", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "p";
							}
							else if ((Operators.CompareString(l_DBType, "Postgres", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "v";
							}
							else if ((Operators.CompareString(l_DBType, "iMBigData", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "s";
							}
							else if ((Operators.CompareString(l_DBType, "DUCKDB", TextCompare: false) == 0) & (Operators.CompareString(text8, "d", TextCompare: false) == 0))
							{
								text8 = "w";
							}
						}
						if (Globals_Renamed.currinttmp == 0)
						{
							text5 = Strings.Trim(Text2.Text);
							text9 = text4;
						}
						else if (Globals_Renamed.currinttmp == 4 || Globals_Renamed.currinttmp == 7 || Globals_Renamed.currinttmp == 8 || Globals_Renamed.currinttmp == 10)
						{
							text5 = Strings.Trim(Text2.Text);
						}
						else if (Globals_Renamed.currinttmp == 1)
						{
							text5 = text4;
						}
						else if (Globals_Renamed.currinttmp == 9)
						{
							text5 = Strings.Trim(Text2.Text);
							text9 = "Column-Pattern->[[]]";
						}
						else
						{
							text5 = "";
						}
						text6 = Strings.Replace(Strings.Trim(txtComment.Text), "\"", "'", 1, -1, CompareMethod.Text);
						if (Globals_Renamed.currinttmp != 4 && Globals_Renamed.currinttmp != 6 && Globals_Renamed.currinttmp != 10 && Globals_Renamed.currinttmp != 7 && Globals_Renamed.currinttmp != 8)
						{
							l_Alias = General_Procedures.Get_Obj_Alias("F->T", Combo2.SelectedItem.ToString()) + "->";
						}
						if ((Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 4 || Globals_Renamed.currinttmp == 7 || Globals_Renamed.currinttmp == 8 || (Globals_Renamed.currinttmp == 9 && Operators.CompareString(l_DBType, "Mongo", TextCompare: false) != 0) || Globals_Renamed.currinttmp == 10) & (Operators.CompareString(text5, "", TextCompare: false) == 0))
						{
							Interaction.MsgBox("You must specify a Computed Column Name!", MsgBoxStyle.Exclamation, "Missing a Column Header");
							Text2.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							Interaction.MsgBox("You must specify a Computed Expression!", MsgBoxStyle.Exclamation, "Missing the Computed Expression");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if (Globals_Renamed.currinttmp != 10)
						{
							goto IL_097a;
						}
						text10 = Strip_JS_Comment(text4);
						if (Operators.CompareString(text10, "ERROR", TextCompare: false) == 0)
						{
							Interaction.MsgBox("An End of Comment Token (*/) was found before a Start of comment token (/*) in the JavaScript expressions.", MsgBoxStyle.Exclamation, "Invalid Aggregate Expression");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if ((!LikeOperator.LikeString(Strings.LCase(text5), "$ratio*", CompareMethod.Binary) || Strings.InStr(text10, "spf_fn$ratio") != 0) && (Strings.InStr(text10, "spf_fn$ratio") == 0 || LikeOperator.LikeString(Strings.LCase(text5), "$ratio*", CompareMethod.Binary)))
						{
							goto IL_097a;
						}
						Interaction.MsgBox("For JavaScript Ratio expressions, the column header must start with \"$ratio\" and the expression must contain function spf_fn$ratio in lowercase", MsgBoxStyle.Exclamation, "Invalid Aggregate Expression");
						TxtExpression.Focus();
						Globals_Renamed.QueryCancel = -1;
						goto end_IL_0001_2;
						IL_097a:
						if (Globals_Renamed.currinttmp == 9 && Strings.InStr(text4, "|<>|") == 0)
						{
							Interaction.MsgBox("The Column Replace expression must contain the token |<>| to represent where columns will be inserted.", MsgBoxStyle.Exclamation, "Missing Token");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if (Globals_Renamed.currinttmp == 9 && !BuildForm.Test_Search_Pat(CmbDataType.Text, text6))
						{
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if ((Globals_Renamed.currinttmp == 4) | (Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
						{
							Globals_Renamed.currDataAny = text4;
							if (Globals_Renamed.currinttmp == 4)
							{
								Globals_Renamed.currdatatypetmp = "CN";
								if (LikeOperator.LikeString(CmbDataType.Text, "Numeric*", CompareMethod.Binary))
								{
									Globals_Renamed.currdatatypetmp = "NC";
								}
								else if (LikeOperator.LikeString(CmbDataType.Text, "*Ordinal", CompareMethod.Binary))
								{
									Globals_Renamed.currdatatypetmp = "CO";
								}
							}
							else
							{
								Globals_Renamed.currdatatypetmp = Strings.Mid(CmbDataType.Text + " ", 1, 1);
							}
							Globals_Renamed.currtxttmp = text5;
							Close();
							goto end_IL_0001;
						}
						text2 = "None";
						if (Strings.InStr(text7, "PARTITION") == 0 && Strings.InStr(text7, "REGEXP") == 0)
						{
							if (Strings.InStr(text7, "SUM(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "COUNT(") != 0)
							{
								if (!LikeOperator.LikeString(Strings.LTrim(text7), "(NVL((SELECT COUNT(*) FROM INSP_DEFECT Y98, UDB.CLASS Y99 WHERE *", CompareMethod.Binary))
								{
									text2 = "Expr";
								}
							}
							else if (Strings.InStr(text7, "AVG(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "MIN(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "MAX(") != 0)
							{
								text2 = "Expr";
							}
							else if ((Strings.InStr(text7, "STDDEV(") != 0) | (Strings.InStr(text7, "STDDEV_POP(") != 0) | (Strings.InStr(text7, "STDDEV_SAMP(") != 0))
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "STDEV(") != 0)
							{
								text2 = "Expr";
							}
							else if ((Strings.InStr(text7, "VARIANCE(") != 0) | (Strings.InStr(text7, "VAR_POP(") != 0) | (Strings.InStr(text7, "VAR_SAMP(") != 0))
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "VAR(") != 0)
							{
								text2 = "Expr";
							}
							else if ((Strings.InStr(text7, "CORR(") != 0) | (Strings.InStr(text7, "KURTOSIS(") != 0) | (Strings.InStr(text7, "SKEW(") != 0))
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "RANGE(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "WITHIN GROUP") != 0)
							{
								text2 = "Expr";
							}
							else if ((Strings.InStr(text7, "P50(") != 0) | (Strings.InStr(text7, "P25(") != 0) | (Strings.InStr(text7, "P75(") != 0) | (Strings.InStr(text7, "P01(") != 0) | (Strings.InStr(text7, "P05(") != 0) | (Strings.InStr(text7, "P10(") != 0))
							{
								text2 = "Expr";
							}
							else if ((Strings.InStr(text7, "P90(") != 0) | (Strings.InStr(text7, "P95(") != 0) | (Strings.InStr(text7, "P99(") != 0) | (Strings.InStr(text7, "P65(") != 0) | (Strings.InStr(text7, "P80(") != 0))
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "MODE(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "GROUP_CONCAT(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "COLLECT_SET(") != 0)
							{
								text2 = "Expr";
							}
							else if (Strings.InStr(text7, "TOTAL(") != 0)
							{
								text2 = "Expr";
							}
						}
						if (Globals_Renamed.currinttmp == 6 || Globals_Renamed.currinttmp == 10)
						{
							goto IL_1062;
						}
						if ((Operators.CompareString(text2, "None", TextCompare: false) == 0) & (Operators.CompareString(GlobalListColumn, "", TextCompare: false) == 0) & (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) == 0) & (Operators.CompareString(l_Alias, "All->", TextCompare: false) != 0) & (Operators.CompareString(l_Alias, "ILV->", TextCompare: false) != 0) & (Operators.CompareString(l_Alias, "SQL->", TextCompare: false) != 0) & (Operators.CompareString(l_Alias, "TXT->", TextCompare: false) != 0))
						{
							string text12 = "";
							short num4 = (short)Strings.InStr(l_Alias, "->");
							if (num4 != 0)
							{
								text12 = Strings.Mid(l_Alias, 1, num4 - 1);
							}
							if (Operators.CompareString(text12, "", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "VIEW:" + text12)), "DUAL " + Strings.UCase(text12), TextCompare: false) == 0)
							{
								GlobalListColumn = "dual";
							}
							if (Operators.CompareString(GlobalListColumn, "", TextCompare: false) == 0)
							{
								Interaction.MsgBox(General_Procedures.Get_UI("errce1"), MsgBoxStyle.Information, "Setting Expression Level to 1");
								lstLevel.Text = "1";
							}
						}
						text4 = Replace_CompExpr("ID", text4, text2);
						if (Operators.CompareString(text4, "@ERROR@", TextCompare: false) != 0)
						{
							goto IL_1062;
						}
						TxtExpression.Focus();
						Globals_Renamed.QueryCancel = -1;
						goto end_IL_0001_2;
						IL_14ed:
						if (Globals_Renamed.currinttmp == 10)
						{
							goto IL_167c;
						}
						if ((Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(lstLevel.Text), "SUMMARY", TextCompare: false) == 0) & ((Operators.CompareString(text2, "None", TextCompare: false) == 0) | (Operators.CompareString(text2, "x", TextCompare: false) == 0)))
						{
							Interaction.MsgBox("For summary expressions, (i.e., Column level = Summary), you must specify a summary function such as SUM, MIN, MAX...!", MsgBoxStyle.Exclamation, "Invalid Summary Expression");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if ((Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(lstLevel.Text), "SUMMARY", TextCompare: false) != 0) & ((Operators.CompareString(text2, "Expr", TextCompare: false) == 0) | (Operators.CompareString(text2, "s", TextCompare: false) == 0)))
						{
							Interaction.MsgBox("For non-summary expressions, (i.e., Column Level is not equal to Summary), you cannot specify an aggregate function such as SUM, MIN, MAX...!", MsgBoxStyle.Exclamation, "Invalid Expression");
							TxtExpression.Focus();
							Globals_Renamed.QueryCancel = -1;
							goto end_IL_0001;
						}
						if (Globals_Renamed.currinttmp != 5 || !((Operators.CompareString(text2, "None", TextCompare: false) == 0) | (Operators.CompareString(text2, "x", TextCompare: false) == 0) | (Strings.InStr(Strings.LCase(text4), Strings.LCase("[MyPivotedColumn(s)]")) == 0)))
						{
							goto IL_167c;
						}
						Interaction.MsgBox("For All-Sources Summary expressions against a Pivot-Value column, the expression must contain both summary functions and the string[MyPivotedColumn(s)]!", MsgBoxStyle.Exclamation, "Invalid Expression");
						TxtExpression.Focus();
						Globals_Renamed.QueryCancel = -1;
						goto end_IL_0001_2;
						IL_167c:
						if (Globals_Renamed.currinttmp == 0)
						{
							if (Operators.CompareString(Strings.Mid(Strings.UCase(lstLevel.Text) + "          ", 1, 10), "SUMMARY + ", TextCompare: false) == 0)
							{
								GlobalListColumn = "s+" + Strings.Mid(lstLevel.Text, 11, 1);
							}
							else if (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) > 0)
							{
								GlobalListColumn = Strings.LCase("@IF@" + Strings.Trim(lstLevel.Text));
							}
							if (Operators.CompareString(text2, "None", TextCompare: false) == 0)
							{
								text6 = "***" + text6;
							}
							if (Operators.CompareString(text2, "None", TextCompare: false) != 0)
							{
								GlobalListColumn = "";
							}
							string text11 = Strings.UCase(Strings.Mid(text8, 1, 1));
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								text7 = ((!LikeOperator.LikeString(Strings.UCase(text9), "*SELECT * FROM *", CompareMethod.Binary)) ? (General_Procedures.Set_Get_Fn("S", "C", text2, l_Alias) + text9) : (General_Procedures.Set_Get_Fn("S", "C", text2, l_Alias) + text5));
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[0].Value = text7;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[1].Value = text5;
								if (Operators.CompareString(text2, "None", TextCompare: false) != 0)
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = text2;
								}
								else if (Operators.CompareString(text2, "None", TextCompare: false) == 0 && Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value, "Expr", TextCompare: false))
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = text2;
								}
								text7 = BuildForm.Assign_Col_Level("C", text2, GlobalListColumn, l_Alias);
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[6].Value = text7;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[8].Value = text11;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[9].Value = text6;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[10].Value = GlobalListColumn;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = l_Alias + text4;
								Close();
							}
							else
							{
								string text3 = "None";
								if (Operators.CompareString(text13, "", TextCompare: false) == 0)
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Columns_Grid(text5, text4, l_Alias, "", text2, text3, text11, text6, GlobalListColumn, text9, "", "Y");
								}
								else if (Operators.CompareString(text16, "", TextCompare: false) != 0)
								{
									int num6 = Conversions.ToInteger(text14);
									for (int num5 = 1; num5 <= num6; num5++)
									{
										string text12 = "";
										text12 = General_Procedures.Get_Ini_Data("parse_delimited_substr", l_DBType.ToLower(), "", 2000, Globals_Renamed.MySchemaDir + "\\spf_functions.ini");
										text12 = Strings.Replace(text12, "<>", text13, 1, -1, CompareMethod.Text);
										text12 = Strings.Replace(text12, "<dlm>", text15, 1, -1, CompareMethod.Text);
										text12 = Strings.Replace(text12, "<occ>", Conversions.ToString(num5), 1, -1, CompareMethod.Text);
										text12 = Strings.Replace(text12, "<4>", text16, 1, -1, CompareMethod.Text);
										if (Operators.CompareString(text12, "", TextCompare: false) != 0)
										{
											MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Columns_Grid(text5.Trim() + Conversions.ToString(num5).Trim(), text12, l_Alias, "", text2, text3, text11, text6, GlobalListColumn, text5.Trim() + Conversions.ToString(num5).Trim(), "", "Y");
										}
									}
								}
								else
								{
									int num7 = Conversions.ToInteger(text14);
									for (int num5 = 1; num5 <= num7; num5++)
									{
										string text12 = "";
										text12 = General_Procedures.Get_Ini_Data("parse_delimited", l_DBType.ToLower(), "", 2000, Globals_Renamed.MySchemaDir + "\\spf_functions.ini");
										text12 = Strings.Replace(text12, "<>", text13, 1, -1, CompareMethod.Text);
										text12 = Strings.Replace(text12, "<dlm>", text15, 1, -1, CompareMethod.Text);
										text12 = Strings.Replace(text12, "<occ>", Conversions.ToString(num5), 1, -1, CompareMethod.Text);
										if (Operators.CompareString(text12, "", TextCompare: false) != 0)
										{
											MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Columns_Grid(text5.Trim() + Conversions.ToString(num5).Trim(), text12, l_Alias, "", text2, text3, text11, text6, GlobalListColumn, text5.Trim() + Conversions.ToString(num5).Trim(), "", "Y");
										}
									}
								}
								TxtExpression.Text = "";
								Text2.Text = "";
								txtComment.Text = "";
								Set_DataType("", Globals_Renamed.currinttmp);
								FrmSQLQuerya obj = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx];
								DataGridView MyGrid = obj.GridQuery;
								GridModule.Set_Grid_TopRow(ref MyGrid);
								obj.GridQuery = MyGrid;
								Load_QColumns();
								Load_CColumns();
							}
						}
						else if (Globals_Renamed.currinttmp == 9)
						{
							if (Operators.CompareString(Strings.Mid(Strings.UCase(lstLevel.Text) + "          ", 1, 10), "SUMMARY + ", TextCompare: false) == 0)
							{
								GlobalListColumn = "s+" + Strings.Mid(lstLevel.Text, 11, 1);
							}
							else if (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) > 0)
							{
								GlobalListColumn = Strings.LCase("@IF@" + Strings.Trim(lstLevel.Text));
							}
							text7 = General_Procedures.Set_Get_Fn("S", "C", text2, l_Alias) + text9;
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[0].Value = text7;
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[1].Value = text5;
							if (Operators.CompareString(text2, "None", TextCompare: false) != 0)
							{
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = text2;
							}
							else if (Operators.CompareString(text2, "None", TextCompare: false) == 0 && Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value, "Expr", TextCompare: false))
							{
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value = text2;
							}
							text7 = BuildForm.Assign_Col_Level("C", text2, GlobalListColumn, l_Alias);
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[6].Value = text7;
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[10].Value = GlobalListColumn;
							Globals_Renamed.currdatatypetmp = Globals_Renamed.currdatatypetmp + "<;>" + CmbDataType.Text + "<;>" + text6 + "<;>" + text4 + "]]";
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value = l_Alias + Globals_Renamed.currdatatypetmp;
							Globals_Renamed.gPurgeColPatternFiles = true;
							Close();
						}
						else if (Globals_Renamed.currinttmp == 1)
						{
							text2 = ((Operators.CompareString(text2, "None", TextCompare: false) != 0) ? "s" : "x");
							string text18 = General_Procedures.Set_Get_Fn("S", "F", text2, l_Alias);
							if ((Operators.CompareString(text2, "x", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(Strings.Mid(lstLevel.Text + "          ", 1, 10)), "SUMMARY + ", TextCompare: false) == 0))
							{
								GlobalListColumn = "s+" + Strings.Mid(lstLevel.Text, 11, 1);
							}
							else if ((Operators.CompareString(text2, "x", TextCompare: false) == 0) & (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) > 0))
							{
								GlobalListColumn = Strings.LCase("@IF@" + Strings.Trim(lstLevel.Text));
							}
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[10].Value = l_Alias + text4;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[6].Value = BuildForm.Assign_Col_Level("F", text8 + text2, GlobalListColumn, l_Alias);
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[8].Value = text8 + text2;
								if (Operators.CompareString(text2, "x", TextCompare: false) == 0)
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[9].Value = GlobalListColumn;
								}
								else
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[9].Value = "";
								}
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[2].Value = text18 + text5;
								if (Operators.CompareString(Strings.UCase(text8), "E", TextCompare: false) == 0)
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[7].Value = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ImageKey.Images[2];
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[7].Tag = "E";
								}
								Close();
							}
							else
							{
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Filters_Grid(text18 + Strings.LCase(text5), text4, l_Alias, "", text8, text2, GlobalListColumn, "");
								Set_DataType("", Globals_Renamed.currinttmp);
								TxtExpression.Text = "";
							}
							Close();
						}
						else if (Globals_Renamed.currinttmp == 5)
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[7].Tag = text4;
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[7].Value = "Expr";
							Close();
						}
						else if (Globals_Renamed.currinttmp == 6 || Globals_Renamed.currinttmp == 11)
						{
							Globals_Renamed.currDataAny = text4;
							Globals_Renamed.currtxttmp = text2;
							Close();
						}
						else if (Globals_Renamed.currinttmp == 10)
						{
							text4 = Strings.Replace(text4, "\"", "'", 1, -1, CompareMethod.Text);
							text4 = Strings.Replace(text4, Globals_Renamed.CRLF, "!!!!!", 1, -1, CompareMethod.Text);
							text4 = "{'" + text5 + "':" + text4 + "}";
							Globals_Renamed.currDataAny = text4;
							Close();
						}
						fSaveExpr = false;
						goto end_IL_0001_2;
						IL_1062:
						if ((Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 9) && LikeOperator.LikeString(Strings.UCase(text4), "*SPF_FN$PARSE_DELIMITED_SUBSTR*", CompareMethod.Binary))
						{
							if (Operators.CompareString(l_DBType, "Teradata", TextCompare: false) != 0)
							{
								Interaction.MsgBox("The SPF_FN$Parse_Delimited_Substr function is only supported for Teradata databases.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
								Globals_Renamed.QueryCancel = -1;
							}
							else if (LikeOperator.LikeString(Strings.Replace(text7, " ", "", 1, -1, CompareMethod.Text), "SPF_FN$PARSE_DELIMITED_SUBSTR(*,*,*,*)", CompareMethod.Binary))
							{
								string text12 = Strings.Mid(text4, 31, Strings.Len(text4) - 31);
								short num4 = (short)Strings.InStr(text12, "(");
								text12 = Strings.Trim(Strings.Mid(text12, num4 + 1));
								num4 = (short)Strings.InStr(text12, ",");
								text13 = Strings.Trim(Strings.Mid(text12, 1, num4 - 1));
								text12 = Strings.Trim(Strings.Mid(text12, num4 + 1));
								num4 = (short)Strings.InStr(text12, ",");
								text14 = Strings.Trim(Strings.Mid(text12, 1, num4 - 1));
								text12 = Strings.Trim(Strings.Mid(text12, num4 + 1));
								num4 = (short)Strings.InStr(text12, ",");
								text15 = Strings.Trim(Strings.Mid(text12, 1, num4 - 1));
								text15 = Strings.Trim(Strings.Replace(text15, "'", "", 1, -1, CompareMethod.Text));
								text16 = Strings.Trim(Strings.Mid(text12, num4 + 1));
								if (Operators.CompareString(text13, "", TextCompare: false) != 0 && Operators.CompareString(text14, "", TextCompare: false) != 0 && Versioned.IsNumeric(text14) && Operators.CompareString(text15, "", TextCompare: false) != 0 && Operators.CompareString(text16, "", TextCompare: false) != 0 && Versioned.IsNumeric(text16))
								{
									if (Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0)
									{
										text15 = "'" + text15 + "'";
									}
									goto IL_14ed;
								}
								Interaction.MsgBox("There was an issue with arguments of function SPF_FN$Parse_Delimited_Substr. The function must contain 4 arguments - the column, the number of columns to create, the delimiter, the size of the string. Please correct before continuing.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
								Globals_Renamed.QueryCancel = -1;
							}
							else
							{
								Interaction.MsgBox("The SPF_FN$Parse_Delimited_Substr function can only be used on its own and must contain 4 arguments - the column, the number of columns to create, the delimiter, and the size. Please correct before continuing.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
								Globals_Renamed.QueryCancel = -1;
							}
							goto end_IL_0001;
						}
						if ((Globals_Renamed.currinttmp != 0 && Globals_Renamed.currinttmp != 9) || !LikeOperator.LikeString(Strings.UCase(text4), "*SPF_FN$PARSE_DELIMITED*", CompareMethod.Binary))
						{
							goto IL_14ed;
						}
						if (!((Operators.CompareString(l_DBType, "Oracle", TextCompare: false) == 0) | (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0) | (Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0)))
						{
							Interaction.MsgBox("The SPF_FN$Parse_Delimited function is only supported for Oracle, SQLite or Teradata databases.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
							Globals_Renamed.QueryCancel = -1;
						}
						else if (LikeOperator.LikeString(Strings.Replace(text7, " ", "", 1, -1, CompareMethod.Text), "SPF_FN$PARSE_DELIMITED(*,*,*)", CompareMethod.Binary))
						{
							string text12 = Strings.Mid(text4, 23, Strings.Len(text4) - 23);
							short num4 = (short)Strings.InStr(text12, "(");
							text12 = Strings.Trim(Strings.Mid(text12, num4 + 1));
							num4 = (short)Strings.InStr(text12, ",");
							text13 = Strings.Trim(Strings.Mid(text12, 1, num4 - 1));
							text12 = Strings.Trim(Strings.Mid(text12, num4 + 1));
							num4 = (short)Strings.InStr(text12, ",");
							text14 = Strings.Trim(Strings.Mid(text12, 1, num4 - 1));
							text15 = Strings.Trim(Strings.Mid(text12, num4 + 1));
							text15 = Strings.Trim(Strings.Replace(text15, "'", "", 1, -1, CompareMethod.Text));
							if (Operators.CompareString(text13, "", TextCompare: false) != 0 && Operators.CompareString(text14, "", TextCompare: false) != 0 && Versioned.IsNumeric(text14) && Operators.CompareString(text15, "", TextCompare: false) != 0)
							{
								if ((Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0) | (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0))
								{
									text15 = "'" + text15 + "'";
								}
								goto IL_14ed;
							}
							Interaction.MsgBox("There was an issue with arguments of the SPF_FN$Parse_Delimited function. The function must contain 3 arguments - the column, the number of columns to create, and the delimiter. Please correct before continuing.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
							Globals_Renamed.QueryCancel = -1;
						}
						else
						{
							Interaction.MsgBox("The SPF_FN$Parse_Delimited function can only be used on its own and must contain 3 arguments - the column, the number of columns to create, and the delimiter. Please correct before continuing.", MsgBoxStyle.Exclamation, "Invalid Use of Function");
							Globals_Renamed.QueryCancel = -1;
						}
						goto end_IL_0001_2;
					}
					goto IL_2787;
				}
				end_IL_0001_2:;
			}
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 10065;
				continue;
			}
			break;
			IL_2787:
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

	private void cmdClose_Click(object eventSender, EventArgs eventArgs)
	{
		int num = 0;
		Globals_Renamed.QueryCancel = 0;
		if (fSaveExpr)
		{
			num = (int)Interaction.MsgBox("Do you wish to save your changes?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Save Changes?");
			if (num == 6)
			{
				cmdadd_Click(RuntimeHelpers.GetObjectValue(eventSender), new EventArgs());
				if (Globals_Renamed.QueryCancel != 0)
				{
					return;
				}
			}
		}
		Close();
	}

	private void CmdColumns_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string errsource = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_000a;
				case 149:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_0061;
						default:
							goto end_IL_0001;
						}
						goto IL_0035;
					}
					IL_001b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0024;
					IL_0024:
					num2 = 5;
					TxtExpression.Focus();
					goto end_IL_0001_2;
					IL_0012:
					num2 = 3;
					Load_CColumns();
					goto IL_001b;
					IL_0061:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_000a;
					case 3:
						goto IL_0012;
					case 4:
						goto IL_001b;
					case 5:
						goto IL_0024;
					case 7:
						goto IL_0035;
					case 8:
						goto end_IL_0001_3;
					default:
						goto end_IL_0001;
					case 6:
					case 9:
						goto end_IL_0001_2;
					}
					goto default;
					IL_0035:
					num2 = 7;
					Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
					break;
					IL_000a:
					num2 = 2;
					errsource = "frmComputedSQL - CmdColumns_Click";
					goto IL_0012;
					end_IL_0001_3:
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
				try0001_dispatch = 149;
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

	private void cmdCopy_Click(object eventSender, EventArgs eventArgs)
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
							goto IL_002f;
						case 4:
							goto IL_0042;
						case 5:
							goto IL_0050;
						case 6:
							goto IL_0063;
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
					IL_0042:
					num2 = 4;
					l_CompExpr_List = GlobalListColumn;
					goto IL_0050;
					IL_0050:
					num2 = 5;
					l_CompExpr_DT = CmbDataType.SelectedIndex;
					goto IL_0063;
					IL_002f:
					num2 = 3;
					l_CompExpr = TxtExpression.Text;
					goto IL_0042;
					IL_0063:
					num2 = 6;
					l_CompExpr_Name = Text2.Text;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(TxtExpression.Text), "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_002f;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				TxtExpression.Focus();
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

	private void cmdexpr_Click(object eventSender, EventArgs eventArgs)
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
				case 174:
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
						case 4:
							goto IL_0031;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_0050;
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
					IL_0031:
					num2 = 4;
					TxtExpression.Focus();
					goto IL_003f;
					IL_003f:
					num2 = 5;
					TxtExpression.SelectionStart = checked(num5 - 1);
					goto IL_0050;
					IL_0025:
					num2 = 3;
					if (num5 == 0)
					{
						break;
					}
					goto IL_0031;
					IL_0050:
					num2 = 6;
					TxtExpression.SelectionLength = Strings.Len("<expr>");
					break;
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.InStr(TxtExpression.Text, "<expr>"));
					goto IL_0025;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 174;
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

	private void cmdfunctions_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short nofunctions = default(short);
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
				case 335:
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
							goto IL_0031;
						case 5:
							goto IL_0041;
						case 6:
							goto IL_00d2;
						case 7:
						case 8:
							goto IL_00f7;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0041:
					num2 = 5;
					if (!((Globals_Renamed.currinttmp == 1 || Globals_Renamed.currinttmp == 6) & LikeOperator.LikeString(Strings.UCase(Globals_Renamed.Functions[num5].Name), "*PARTITION BY*", CompareMethod.Binary)) & !((Globals_Renamed.currinttmp == 6) & (LikeOperator.LikeString(Strings.UCase(Globals_Renamed.Functions[num5].Name), "SPF_*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(Globals_Renamed.Functions[num5].Name), "&PROMPT&*", CompareMethod.Binary))))
					{
						goto IL_00d2;
					}
					goto IL_00f7;
					IL_00d2:
					num2 = 6;
					lstgeneral.Items.Add(Globals_Renamed.Functions[num5].Name);
					goto IL_00f7;
					IL_00fe:
					if (num5 > nofunctions)
					{
						break;
					}
					goto IL_0041;
					IL_00f7:
					num2 = 8;
					num5 = checked((short)unchecked(num5 + 1));
					goto IL_00fe;
					IL_000b:
					num2 = 2;
					lblgeneral.Text = "Functions";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					lstgeneral.Items.Clear();
					goto IL_0031;
					IL_0031:
					num2 = 4;
					nofunctions = Globals_Renamed.nofunctions;
					num5 = 0;
					goto IL_00fe;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 335;
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
				case 450:
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
							goto IL_0022;
						case 5:
							goto IL_0036;
						case 6:
							goto IL_0045;
						case 7:
							goto IL_0086;
						case 8:
							goto IL_00c7;
						case 11:
							goto IL_00d8;
						case 12:
							goto IL_00fe;
						case 13:
							goto IL_0113;
						case 14:
							goto IL_0120;
						case 15:
							goto IL_012d;
						case 16:
							goto IL_013a;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
						case 17:
						case 18:
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00fe:
					num2 = 12;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to remove the Report Summary Expression?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Remove the All-Sources Summary Expression?");
					goto IL_0113;
					IL_0113:
					num2 = 13;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0120;
					IL_013a:
					num2 = 16;
					Close();
					goto end_IL_0001_3;
					IL_0120:
					num2 = 14;
					Globals_Renamed.currDataAny = "";
					goto IL_012d;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Globals_Renamed.currinttmp == 5)
					{
						goto IL_0022;
					}
					goto IL_00d8;
					IL_0022:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to remove the All-Sources Summary Expression?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Remove the All-Sources Summary Expression?");
					goto IL_0036;
					IL_0036:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0045;
					IL_0045:
					num2 = 6;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[7].Tag = "";
					goto IL_0086;
					IL_0086:
					num2 = 7;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[7].Value = "None";
					goto IL_00c7;
					IL_00c7:
					num2 = 8;
					Close();
					goto end_IL_0001_3;
					IL_00d8:
					num2 = 11;
					if (Globals_Renamed.currinttmp != 6 && Globals_Renamed.currinttmp != 10 && Globals_Renamed.currinttmp != 11)
					{
						break;
					}
					goto IL_00fe;
					IL_012d:
					num2 = 15;
					Globals_Renamed.currtxttmp = "";
					goto IL_013a;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Computed+Columns");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 450;
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

	private void cmdoperators_Click(object eventSender, EventArgs eventArgs)
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
				short currinttmp;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1214:
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
							goto IL_0031;
						case 6:
							goto IL_0051;
						case 8:
							goto IL_006e;
						case 9:
							goto IL_0086;
						case 10:
							goto IL_009f;
						case 11:
							goto IL_00b8;
						case 12:
							goto IL_00d1;
						case 13:
							goto IL_00ea;
						case 14:
							goto IL_0103;
						case 15:
							goto IL_011c;
						case 16:
							goto IL_0135;
						case 17:
							goto IL_014e;
						case 18:
							goto IL_0167;
						case 19:
							goto IL_0181;
						case 20:
							goto IL_019a;
						case 21:
							goto IL_01b3;
						case 24:
							goto IL_01d4;
						case 25:
							goto IL_01ed;
						case 26:
							goto IL_0206;
						case 27:
							goto IL_021f;
						case 28:
							goto IL_0238;
						case 29:
							goto IL_0251;
						case 30:
							goto IL_0271;
						case 31:
							goto IL_028a;
						case 32:
							goto IL_02a3;
						case 33:
							goto IL_02bc;
						case 34:
							goto IL_02d5;
						case 35:
							goto IL_02ee;
						case 37:
							goto IL_030e;
						case 38:
							goto IL_0327;
						case 39:
							goto IL_0340;
						case 40:
							goto IL_0359;
						case 41:
							goto IL_0372;
						case 42:
							goto IL_038b;
						case 43:
							goto IL_03a4;
						case 44:
							goto IL_03bd;
						case 7:
						case 22:
						case 23:
						case 36:
						case 45:
						case 46:
						case 47:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 48:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0167:
					num2 = 18;
					if (!((Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8)))
					{
						break;
					}
					goto IL_0181;
					IL_0181:
					num2 = 19;
					lstgeneral.Items.Add("**");
					goto IL_019a;
					IL_014e:
					num2 = 17;
					lstgeneral.Items.Add("|");
					goto IL_0167;
					IL_019a:
					num2 = 20;
					lstgeneral.Items.Add("%%");
					goto IL_01b3;
					IL_000b:
					num2 = 2;
					lblgeneral.Text = "Comparison Operators";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					lstgeneral.Items.Clear();
					goto IL_0031;
					IL_0031:
					num2 = 4;
					if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0051;
					IL_0051:
					num2 = 6;
					currinttmp = Globals_Renamed.currinttmp;
					if (currinttmp == 4 || (uint)(currinttmp - 7) <= 1u)
					{
						goto IL_006e;
					}
					goto IL_01d4;
					IL_01b3:
					num2 = 21;
					lstgeneral.Items.Add("^");
					break;
					IL_01d4:
					num2 = 24;
					lstgeneral.Items.Add("=");
					goto IL_01ed;
					IL_01ed:
					num2 = 25;
					lstgeneral.Items.Add(">=");
					goto IL_0206;
					IL_0206:
					num2 = 26;
					lstgeneral.Items.Add("<=");
					goto IL_021f;
					IL_021f:
					num2 = 27;
					lstgeneral.Items.Add(">");
					goto IL_0238;
					IL_0238:
					num2 = 28;
					lstgeneral.Items.Add("<");
					goto IL_0251;
					IL_0251:
					num2 = 29;
					if (Operators.CompareString(l_DBType, "iMBigData", TextCompare: false) == 0)
					{
						goto IL_0271;
					}
					goto IL_030e;
					IL_0271:
					num2 = 30;
					lstgeneral.Items.Add("==");
					goto IL_028a;
					IL_028a:
					num2 = 31;
					lstgeneral.Items.Add("===");
					goto IL_02a3;
					IL_02a3:
					num2 = 32;
					lstgeneral.Items.Add("!=");
					goto IL_02bc;
					IL_02bc:
					num2 = 33;
					lstgeneral.Items.Add("!==");
					goto IL_02d5;
					IL_02d5:
					num2 = 34;
					lstgeneral.Items.Add("&&");
					goto IL_02ee;
					IL_02ee:
					num2 = 35;
					lstgeneral.Items.Add("||");
					break;
					IL_030e:
					num2 = 37;
					lstgeneral.Items.Add("like");
					goto IL_0327;
					IL_0327:
					num2 = 38;
					lstgeneral.Items.Add("Between");
					goto IL_0340;
					IL_0340:
					num2 = 39;
					lstgeneral.Items.Add("<>");
					goto IL_0359;
					IL_0359:
					num2 = 40;
					lstgeneral.Items.Add("Not In");
					goto IL_0372;
					IL_0372:
					num2 = 41;
					lstgeneral.Items.Add("Not Between");
					goto IL_038b;
					IL_038b:
					num2 = 42;
					lstgeneral.Items.Add("Not Like");
					goto IL_03a4;
					IL_03a4:
					num2 = 43;
					lstgeneral.Items.Add("Is NULL");
					goto IL_03bd;
					IL_03bd:
					num2 = 44;
					lstgeneral.Items.Add("Is NOT NULL");
					break;
					IL_006e:
					num2 = 8;
					lstgeneral.Items.Add("&");
					goto IL_0086;
					IL_0086:
					num2 = 9;
					lstgeneral.Items.Add("==");
					goto IL_009f;
					IL_009f:
					num2 = 10;
					lstgeneral.Items.Add("=");
					goto IL_00b8;
					IL_00b8:
					num2 = 11;
					lstgeneral.Items.Add(">");
					goto IL_00d1;
					IL_00d1:
					num2 = 12;
					lstgeneral.Items.Add(">=");
					goto IL_00ea;
					IL_00ea:
					num2 = 13;
					lstgeneral.Items.Add("<");
					goto IL_0103;
					IL_0103:
					num2 = 14;
					lstgeneral.Items.Add("<=");
					goto IL_011c;
					IL_011c:
					num2 = 15;
					lstgeneral.Items.Add("!");
					goto IL_0135;
					IL_0135:
					num2 = 16;
					lstgeneral.Items.Add("!=");
					goto IL_014e;
					end_IL_0001_2:
					break;
				}
				num2 = 47;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1214;
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

	private void cmdSet_Click(object eventSender, EventArgs eventArgs)
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
				case 269:
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
							goto IL_002d;
						case 4:
							goto IL_0041;
						case 5:
							goto IL_0055;
						case 6:
							goto IL_0069;
						case 8:
							goto IL_00a8;
						case 7:
						case 9:
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
					IL_0055:
					num2 = 5;
					CmbDataType.SelectedIndex = l_CompExpr_DT;
					goto IL_0069;
					IL_0069:
					num2 = 6;
					if ((Operators.CompareString(Strings.Trim(l_CompExpr_List), "", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(GlobalListColumn), "", TextCompare: false) != 0))
					{
						break;
					}
					goto IL_00a8;
					IL_0041:
					num2 = 4;
					Text2.Text = l_CompExpr_Name;
					goto IL_0055;
					IL_00a8:
					num2 = 8;
					GlobalListColumn = l_CompExpr_List;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(l_CompExpr), "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_002d;
					IL_002d:
					num2 = 3;
					TxtExpression.SelectedText = l_CompExpr;
					goto IL_0041;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 269;
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

	private void cmdsummary_Click(object eventSender, EventArgs eventArgs)
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
				case 1695:
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
							goto IL_0031;
						case 6:
							goto IL_0050;
						case 7:
							goto IL_0068;
						case 8:
							goto IL_0080;
						case 9:
							goto IL_0098;
						case 10:
							goto IL_00b1;
						case 11:
							goto IL_00f7;
						case 12:
						case 13:
							goto IL_0112;
						case 14:
							goto IL_012b;
						case 15:
							goto IL_0144;
						case 16:
							goto IL_01a0;
						case 17:
							goto IL_01b9;
						case 18:
							goto IL_01d2;
						case 19:
							goto IL_01f2;
						case 20:
							goto IL_020b;
						case 21:
							goto IL_0224;
						case 22:
							goto IL_023d;
						case 23:
							goto IL_0256;
						case 24:
							goto IL_026f;
						case 25:
							goto IL_0288;
						case 26:
							goto IL_02a1;
						case 27:
							goto IL_02ba;
						case 28:
							goto IL_02d3;
						case 29:
							goto IL_02ec;
						case 30:
							goto IL_0305;
						case 31:
							goto IL_031e;
						case 32:
							goto IL_0337;
						case 35:
							goto IL_0358;
						case 36:
							goto IL_0378;
						case 37:
							goto IL_0391;
						case 38:
							goto IL_03aa;
						case 39:
							goto IL_03c3;
						case 40:
							goto IL_03dc;
						case 41:
							goto IL_03f5;
						case 42:
							goto IL_040e;
						case 44:
							goto IL_042d;
						case 45:
							goto IL_044d;
						case 46:
							goto IL_0466;
						case 47:
							goto IL_047f;
						case 48:
							goto IL_0498;
						case 49:
							goto IL_04b1;
						case 50:
							goto IL_04ca;
						case 52:
							goto IL_04e6;
						case 53:
							goto IL_0503;
						case 54:
							goto IL_051c;
						case 33:
						case 34:
						case 43:
						case 51:
						case 55:
						case 56:
							goto IL_0536;
						case 57:
							goto IL_0553;
						case 58:
							goto IL_056c;
						case 59:
						case 60:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 61:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_02d3:
					num2 = 28;
					lstgeneral.Items.Add("P95");
					goto IL_02ec;
					IL_02ec:
					num2 = 29;
					lstgeneral.Items.Add("P99");
					goto IL_0305;
					IL_02ba:
					num2 = 27;
					lstgeneral.Items.Add("P90");
					goto IL_02d3;
					IL_0305:
					num2 = 30;
					lstgeneral.Items.Add("MODE");
					goto IL_031e;
					IL_000b:
					num2 = 2;
					lblgeneral.Text = "Summary Functions";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					lstgeneral.Items.Clear();
					goto IL_0031;
					IL_0031:
					num2 = 4;
					if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0050;
					IL_0050:
					num2 = 6;
					lstgeneral.Items.Add("Avg");
					goto IL_0068;
					IL_0068:
					num2 = 7;
					lstgeneral.Items.Add("Sum");
					goto IL_0080;
					IL_0080:
					num2 = 8;
					lstgeneral.Items.Add("Count");
					goto IL_0098;
					IL_0098:
					num2 = 9;
					lstgeneral.Items.Add("Count(*)");
					goto IL_00b1;
					IL_00b1:
					num2 = 10;
					if (Operators.CompareString(l_DBType, "TEXT", TextCompare: false) != 0 && Operators.CompareString(l_DBType, "JavaScript", TextCompare: false) != 0 && Operators.CompareString(l_DBType, "JavaScript2", TextCompare: false) != 0)
					{
						goto IL_00f7;
					}
					goto IL_0112;
					IL_031e:
					num2 = 31;
					lstgeneral.Items.Add("Total");
					goto IL_0337;
					IL_0337:
					num2 = 32;
					lstgeneral.Items.Add("Group_Concat(<expr>,';')");
					goto IL_0536;
					IL_00f7:
					num2 = 11;
					lstgeneral.Items.Add("Count(Distinct <expr>)");
					goto IL_0112;
					IL_0112:
					num2 = 13;
					lstgeneral.Items.Add("Min");
					goto IL_012b;
					IL_012b:
					num2 = 14;
					lstgeneral.Items.Add("Max");
					goto IL_0144;
					IL_0144:
					num2 = 15;
					if (Operators.CompareString(l_DBType, "TEXT", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "SQLServer", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "JavaScript2", TextCompare: false) == 0)
					{
						goto IL_01a0;
					}
					goto IL_0358;
					IL_0536:
					num2 = 56;
					if (Operators.CompareString(l_DBType, "Oracle", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0553;
					IL_0553:
					num2 = 57;
					lstgeneral.Items.Add("PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY <expr>)");
					goto IL_056c;
					IL_056c:
					num2 = 58;
					lstgeneral.Items.Add("LISTAGG( <expr> , ',') WITHIN GROUP (ORDER BY <expr>)");
					break;
					IL_0358:
					num2 = 35;
					if (Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0)
					{
						goto IL_0378;
					}
					goto IL_042d;
					IL_0378:
					num2 = 36;
					lstgeneral.Items.Add("Stddev_Samp");
					goto IL_0391;
					IL_0391:
					num2 = 37;
					lstgeneral.Items.Add("Stddev_Pop");
					goto IL_03aa;
					IL_03aa:
					num2 = 38;
					lstgeneral.Items.Add("Var_Samp");
					goto IL_03c3;
					IL_03c3:
					num2 = 39;
					lstgeneral.Items.Add("Var_Pop");
					goto IL_03dc;
					IL_03dc:
					num2 = 40;
					lstgeneral.Items.Add("Kurtosis");
					goto IL_03f5;
					IL_03f5:
					num2 = 41;
					lstgeneral.Items.Add("Skew");
					goto IL_040e;
					IL_040e:
					num2 = 42;
					lstgeneral.Items.Add("Corr");
					goto IL_0536;
					IL_042d:
					num2 = 44;
					if (Operators.CompareString(l_DBType, "Hadoop", TextCompare: false) == 0)
					{
						goto IL_044d;
					}
					goto IL_04e6;
					IL_044d:
					num2 = 45;
					lstgeneral.Items.Add("APPX_Median");
					goto IL_0466;
					IL_0466:
					num2 = 46;
					lstgeneral.Items.Add("STDDev");
					goto IL_047f;
					IL_047f:
					num2 = 47;
					lstgeneral.Items.Add("Stddev_Pop");
					goto IL_0498;
					IL_0498:
					num2 = 48;
					lstgeneral.Items.Add("Variance");
					goto IL_04b1;
					IL_04b1:
					num2 = 49;
					lstgeneral.Items.Add("Var_Pop");
					goto IL_04ca;
					IL_04ca:
					num2 = 50;
					lstgeneral.Items.Add("Group_Concat(<expr>,';')");
					goto IL_0536;
					IL_04e6:
					num2 = 52;
					if (Operators.CompareString(l_DBType, "Oracle", TextCompare: false) == 0)
					{
						goto IL_0503;
					}
					goto IL_0536;
					IL_0503:
					num2 = 53;
					lstgeneral.Items.Add("STDDev");
					goto IL_051c;
					IL_051c:
					num2 = 54;
					lstgeneral.Items.Add("Variance");
					goto IL_0536;
					IL_01a0:
					num2 = 16;
					lstgeneral.Items.Add("STDev");
					goto IL_01b9;
					IL_01b9:
					num2 = 17;
					lstgeneral.Items.Add("Var");
					goto IL_01d2;
					IL_01d2:
					num2 = 18;
					if (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0)
					{
						goto IL_01f2;
					}
					goto IL_0536;
					IL_01f2:
					num2 = 19;
					lstgeneral.Items.Add("P01");
					goto IL_020b;
					IL_020b:
					num2 = 20;
					lstgeneral.Items.Add("P05");
					goto IL_0224;
					IL_0224:
					num2 = 21;
					lstgeneral.Items.Add("P10");
					goto IL_023d;
					IL_023d:
					num2 = 22;
					lstgeneral.Items.Add("P25");
					goto IL_0256;
					IL_0256:
					num2 = 23;
					lstgeneral.Items.Add("P50");
					goto IL_026f;
					IL_026f:
					num2 = 24;
					lstgeneral.Items.Add("P65");
					goto IL_0288;
					IL_0288:
					num2 = 25;
					lstgeneral.Items.Add("P80");
					goto IL_02a1;
					IL_02a1:
					num2 = 26;
					lstgeneral.Items.Add("P75");
					goto IL_02ba;
					end_IL_0001_2:
					break;
				}
				num2 = 60;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1695;
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

	private void Combo2_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string value = default(string);
		short num5 = default(short);
		short num6 = default(short);
		string alias_Renamed = default(string);
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
					case 1711:
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
								goto IL_0027;
							case 4:
								goto IL_0082;
							case 5:
								goto IL_009f;
							case 6:
								goto IL_00ac;
							case 7:
								goto IL_00be;
							case 8:
								goto IL_00d4;
							case 9:
								goto IL_00f2;
							case 11:
							case 12:
								goto IL_0111;
							case 10:
							case 13:
							case 14:
								goto IL_0123;
							case 15:
								goto IL_01b5;
							case 17:
								goto IL_01cd;
							case 16:
							case 18:
							case 19:
								goto IL_01e2;
							case 20:
								goto IL_01ff;
							case 22:
								goto IL_0213;
							case 23:
								goto IL_0230;
							case 25:
								goto IL_0244;
							case 26:
								goto IL_0261;
							case 28:
								goto IL_0275;
							case 29:
								goto IL_0292;
							case 31:
								goto IL_02a6;
							case 32:
								goto IL_02c3;
							case 34:
								goto IL_02d7;
							case 35:
								goto IL_02f4;
							case 37:
								goto IL_0308;
							case 38:
								goto IL_0325;
							case 40:
								goto IL_0339;
							case 41:
								goto IL_0356;
							case 43:
								goto IL_036a;
							case 44:
								goto IL_0387;
							case 46:
								goto IL_039b;
							case 47:
								goto IL_03b8;
							case 49:
								goto IL_03cc;
							case 50:
								goto IL_03e9;
							case 52:
								goto IL_03fd;
							case 53:
								goto IL_041a;
							case 55:
								goto IL_042e;
							case 56:
								goto IL_044b;
							case 58:
								goto IL_045f;
							case 59:
								goto IL_047c;
							case 61:
								goto IL_0490;
							case 62:
								goto IL_04ad;
							case 64:
								goto IL_04c1;
							case 65:
								goto IL_04de;
							case 67:
								goto IL_04ef;
							case 68:
								goto IL_050c;
							case 70:
								goto IL_051d;
							case 71:
								goto IL_053a;
							case 21:
							case 24:
							case 27:
							case 30:
							case 33:
							case 36:
							case 39:
							case 42:
							case 45:
							case 48:
							case 51:
							case 54:
							case 57:
							case 60:
							case 63:
							case 66:
							case 69:
							case 72:
							case 73:
								goto IL_0549;
							case 74:
							case 75:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 76:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_044b:
						num2 = 56;
						BuildForm.Load_Functions("ibi_daas_func.dat");
						goto IL_0549;
						IL_045f:
						num2 = 58;
						if (Operators.CompareString(l_DBType, "Denodo", TextCompare: false) == 0)
						{
							goto IL_047c;
						}
						goto IL_0490;
						IL_042e:
						num2 = 55;
						if (Operators.CompareString(l_DBType, "IBI-DaaS", TextCompare: false) == 0)
						{
							goto IL_044b;
						}
						goto IL_045f;
						IL_047c:
						num2 = 59;
						BuildForm.Load_Functions("denodo_func.dat");
						goto IL_0549;
						IL_000b:
						num2 = 2;
						if (Combo2.SelectedIndex == -1)
						{
							break;
						}
						goto IL_0027;
						IL_0027:
						num2 = 3;
						if (Operators.CompareString(l_DBType, "JMP", TextCompare: false) != 0 && Operators.CompareString(l_DBType, "R", TextCompare: false) != 0 && Operators.CompareString(l_DBType, "JavaScript", TextCompare: false) != 0 && Operators.CompareString(l_DBType, "JavaScript2", TextCompare: false) != 0)
						{
							goto IL_0082;
						}
						goto IL_0123;
						IL_0490:
						num2 = 61;
						if (Operators.CompareString(l_DBType, "Snowflake", TextCompare: false) == 0)
						{
							goto IL_04ad;
						}
						goto IL_04c1;
						IL_04ad:
						num2 = 62;
						BuildForm.Load_Functions("snowflake_func.dat");
						goto IL_0549;
						IL_04c1:
						num2 = 64;
						if (Operators.CompareString(l_DBType, "JavaScript", TextCompare: false) == 0)
						{
							goto IL_04de;
						}
						goto IL_04ef;
						IL_0082:
						num2 = 4;
						value = General_Procedures.Get_Obj_Alias("F->T", Combo2.SelectedItem.ToString());
						goto IL_009f;
						IL_009f:
						num2 = 5;
						l_DBType = "Oracle";
						goto IL_00ac;
						IL_00ac:
						num2 = 6;
						num5 = (short)(g_NoAlias - 1);
						num6 = 0;
						goto IL_011b;
						IL_011b:
						if (num6 <= num5)
						{
							goto IL_00be;
						}
						goto IL_0123;
						IL_00be:
						num2 = 7;
						alias_Renamed = g_AliasArray[num6].Alias_Renamed;
						goto IL_00d4;
						IL_00d4:
						num2 = 8;
						if (Operators.CompareString(Strings.UCase(value), Strings.UCase(alias_Renamed), TextCompare: false) == 0)
						{
							goto IL_00f2;
						}
						goto IL_0111;
						IL_00f2:
						num2 = 9;
						l_DBType = g_AliasArray[num6].DBType;
						goto IL_0123;
						IL_0111:
						num2 = 12;
						num6 = (short)unchecked(num6 + 1);
						goto IL_011b;
						IL_0123:
						num2 = 14;
						if (Operators.CompareString(l_DBType, "SQLServer", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "TEXT", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "IBI-DaaS", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "JavaScript", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "JavaScript2", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "iMBigData", TextCompare: false) == 0)
						{
							goto IL_01b5;
						}
						goto IL_01cd;
						IL_04de:
						num2 = 65;
						BuildForm.Load_Functions("js_func.dat");
						goto IL_0549;
						IL_04ef:
						num2 = 67;
						if (Operators.CompareString(l_DBType, "JavaScript2", TextCompare: false) == 0)
						{
							goto IL_050c;
						}
						goto IL_051d;
						IL_050c:
						num2 = 68;
						BuildForm.Load_Functions("js2_func.dat");
						goto IL_0549;
						IL_051d:
						num2 = 70;
						if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0)
						{
							goto IL_053a;
						}
						goto IL_0549;
						IL_053a:
						num2 = 71;
						BuildForm.Load_Functions("mongo_func.dat");
						goto IL_0549;
						IL_0549:
						num2 = 73;
						CmdColumns_Click(cmdcolumns, new EventArgs());
						break;
						IL_01cd:
						num2 = 17;
						cmdConcat.Text = " || ";
						goto IL_01e2;
						IL_01b5:
						num2 = 15;
						cmdConcat.Text = " + ";
						goto IL_01e2;
						IL_01e2:
						num2 = 19;
						if (Operators.CompareString(l_DBType, "TEXT", TextCompare: false) == 0)
						{
							goto IL_01ff;
						}
						goto IL_0213;
						IL_01ff:
						num2 = 20;
						BuildForm.Load_Functions("csv_func.dat");
						goto IL_0549;
						IL_0213:
						num2 = 22;
						if (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0)
						{
							goto IL_0230;
						}
						goto IL_0244;
						IL_0230:
						num2 = 23;
						BuildForm.Load_Functions("sqlite_func.dat");
						goto IL_0549;
						IL_0244:
						num2 = 25;
						if (Operators.CompareString(l_DBType, "MySQL", TextCompare: false) == 0)
						{
							goto IL_0261;
						}
						goto IL_0275;
						IL_0261:
						num2 = 26;
						BuildForm.Load_Functions("mysql_func.dat");
						goto IL_0549;
						IL_0275:
						num2 = 28;
						if (Operators.CompareString(l_DBType, "SAPHana", TextCompare: false) == 0)
						{
							goto IL_0292;
						}
						goto IL_02a6;
						IL_0292:
						num2 = 29;
						BuildForm.Load_Functions("saphana_func.dat");
						goto IL_0549;
						IL_02a6:
						num2 = 31;
						if (Operators.CompareString(l_DBType, "Postgres", TextCompare: false) == 0)
						{
							goto IL_02c3;
						}
						goto IL_02d7;
						IL_02c3:
						num2 = 32;
						BuildForm.Load_Functions("postgres_func.dat");
						goto IL_0549;
						IL_02d7:
						num2 = 34;
						if (Operators.CompareString(l_DBType, "Hadoop", TextCompare: false) == 0)
						{
							goto IL_02f4;
						}
						goto IL_0308;
						IL_02f4:
						num2 = 35;
						BuildForm.Load_Functions("impala_func.dat");
						goto IL_0549;
						IL_0308:
						num2 = 37;
						if (Operators.CompareString(l_DBType, "Oracle", TextCompare: false) == 0)
						{
							goto IL_0325;
						}
						goto IL_0339;
						IL_0325:
						num2 = 38;
						BuildForm.Load_Functions("oracle_func.dat");
						goto IL_0549;
						IL_0339:
						num2 = 40;
						if (Operators.CompareString(l_DBType, "DUCKDB", TextCompare: false) == 0)
						{
							goto IL_0356;
						}
						goto IL_036a;
						IL_0356:
						num2 = 41;
						BuildForm.Load_Functions("duckdb_func.dat");
						goto IL_0549;
						IL_036a:
						num2 = 43;
						if (Operators.CompareString(l_DBType, "SQLServer", TextCompare: false) == 0)
						{
							goto IL_0387;
						}
						goto IL_039b;
						IL_0387:
						num2 = 44;
						BuildForm.Load_Functions("sqlserver_func.dat");
						goto IL_0549;
						IL_039b:
						num2 = 46;
						if (Operators.CompareString(l_DBType, "JMP", TextCompare: false) == 0)
						{
							goto IL_03b8;
						}
						goto IL_03cc;
						IL_03b8:
						num2 = 47;
						BuildForm.Load_Functions("jmp_func.dat");
						goto IL_0549;
						IL_03cc:
						num2 = 49;
						if (Operators.CompareString(l_DBType, "R", TextCompare: false) == 0)
						{
							goto IL_03e9;
						}
						goto IL_03fd;
						IL_03e9:
						num2 = 50;
						BuildForm.Load_Functions("r_func.dat");
						goto IL_0549;
						IL_03fd:
						num2 = 52;
						if (Operators.CompareString(l_DBType, "Teradata", TextCompare: false) == 0)
						{
							goto IL_041a;
						}
						goto IL_042e;
						IL_041a:
						num2 = 53;
						BuildForm.Load_Functions("teradata_func.dat");
						goto IL_0549;
						end_IL_0001_2:
						break;
					}
					num2 = 75;
					Refresh();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1711;
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

	private void Command5_Click(object eventSender, EventArgs eventArgs)
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
				case 112:
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
					TxtExpression.Text = "";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				TxtExpression.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 112;
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

	private void FrmComputedSQL_FormClosing(object sender, FormClosingEventArgs e)
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
				case 189:
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
						case 5:
							goto IL_0027;
						case 6:
							goto IL_0037;
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
					IL_0019:
					num2 = 3;
					e.Cancel = true;
					break;
					IL_0027:
					num2 = 5;
					if (Globals_Renamed.currinttmp != 5)
					{
						break;
					}
					goto IL_0037;
					IL_000b:
					num2 = 2;
					if (Globals_Renamed.QueryCancel != 0)
					{
						goto IL_0019;
					}
					goto IL_0027;
					IL_0037:
					num2 = 6;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.CurrentCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery[0, Globals_Renamed.currrowtmp];
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Globals_Renamed.QueryCancel = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 189;
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

	private void FrmComputedSQL_Load(object eventSender, EventArgs eventArgs)
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
						errsource = "FrmComputedSQL - Form_Load";
						short num3 = 0;
						short num4 = 0;
						string text = "";
						int num5 = 0;
						short num6 = 0;
						GlobalListColumn = "";
						l_CompExpr = "";
						l_CompExpr_List = "";
						l_Alias = "";
						Set_DataType("LOAD", Globals_Renamed.currinttmp);
						lbldesc.Text = "Generate a Computed Expression";
						cmdsummary.Enabled = false;
						if (Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 1)
						{
							num3 = 1;
							do
							{
								lstLevel.Items.Add("Summary + " + Conversions.ToString(unchecked((int)num3)));
								num3 = (short)unchecked(num3 + 1);
							}
							while (num3 <= 9);
						}
						Load_QColumns();
						Combo2.Items.Clear();
						if (Globals_Renamed.currinttmp == 4)
						{
							lblFilter.Visible = false;
							lstLevel.Visible = false;
							txtComment.Visible = false;
							Label4.Visible = false;
							l_DBType = "JMP";
							Combo2.Items.Add("JMP");
							Combo2.SelectedIndex = 0;
							TxtExpression.Height = 124;
						}
						else if ((Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
						{
							lblFilter.Visible = false;
							lstLevel.Visible = false;
							txtComment.Visible = false;
							Label4.Visible = false;
							l_DBType = "R";
							Combo2.Items.Add("R");
							Combo2.SelectedIndex = 0;
							TxtExpression.Height = 124;
						}
						else if ((Globals_Renamed.currinttmp == 5) | (Globals_Renamed.currinttmp == 6))
						{
							g_NoAlias = 1;
							l_Alias = "All->";
							g_AliasArray[0].Alias_Renamed = "All";
							if (Globals_Renamed.currinttmp == 5 && MyProject.Forms.FrmMain.mnuJoinDuckDB.Checked)
							{
								l_DBType = "DUCKDB";
							}
							else
							{
								l_DBType = "SQLite";
							}
							g_AliasArray[0].DBType = l_DBType;
							Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[0].Alias_Renamed));
							Combo2.Enabled = false;
							if (Combo2.Items.Count > 0)
							{
								Combo2.SelectedIndex = 0;
							}
							lstLevel.Text = "Summary";
							lstLevel.Enabled = false;
							CmdHelp.Text = "Delete";
							ToolTip1.SetToolTip(CmdHelp, "Remove the Summary Expression from the Query");
						}
						else if (Globals_Renamed.currinttmp == 10 || Globals_Renamed.currinttmp == 11)
						{
							g_NoAlias = 1;
							l_Alias = "All->";
							g_AliasArray[0].Alias_Renamed = "All";
							if (Globals_Renamed.currinttmp == 10)
							{
								l_DBType = "JavaScript";
							}
							else
							{
								l_DBType = "JavaScript2";
							}
							g_AliasArray[0].DBType = l_DBType;
							Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[0].Alias_Renamed));
							Combo2.Enabled = false;
							if (Combo2.Items.Count > 0)
							{
								Combo2.SelectedIndex = 0;
							}
							lstLevel.Text = "Summary";
							lstLevel.Enabled = false;
							if (Globals_Renamed.currinttmp == 10)
							{
								cmdsummary.Enabled = false;
							}
							CmdHelp.Text = "Delete";
							ToolTip1.SetToolTip(CmdHelp, "Remove Summary Expression from Query");
						}
						else if (Globals_Renamed.currinttmp == 3)
						{
							lblFilter.Visible = false;
							lstLevel.Visible = false;
							if (Operators.CompareString(Globals_Renamed.currDataAny, "", TextCompare: false) == 0)
							{
								l_Alias = "";
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Load_Tree_Alias_To_Array("E", ref g_NoAlias, ref g_AliasArray, Do_Valid_Inc: false);
								short num7 = (short)(g_NoAlias - 1);
								for (num3 = 0; num3 <= num7; num3 = (short)unchecked(num3 + 1))
								{
									if ((Operators.CompareString(Strings.Mid(g_AliasArray[num3].Alias_Renamed, 1, 1), "U", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(g_AliasArray[num3].Alias_Renamed), "ALL", TextCompare: false) != 0))
									{
										Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[num3].Alias_Renamed));
									}
								}
							}
							else
							{
								g_NoAlias = 1;
								l_Alias = Globals_Renamed.currDataAny + "->";
								g_AliasArray[0].Alias_Renamed = Globals_Renamed.currDataAny;
								if (Operators.CompareString(Strings.UCase(Globals_Renamed.currDataAny), "ALL", TextCompare: false) != 0)
								{
									l_DBType = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + Globals_Renamed.currDataAny);
									l_DBType = General_Procedures.Get_Node_Value(l_DBType);
								}
								else if (MyProject.Forms.FrmMain.mnuJoinDuckDB.Checked)
								{
									l_DBType = "DUCKDB";
								}
								else
								{
									l_DBType = "SQLite";
								}
								g_AliasArray[0].DBType = l_DBType;
								Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[0].Alias_Renamed));
							}
							Combo2.SelectedIndex = 0;
						}
						else
						{
							l_Alias = "";
							bool addSQLite_Step = false;
							if ((Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 1) && Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) != 0)
							{
								addSQLite_Step = true;
							}
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Load_Tree_Alias_To_Array("E", ref g_NoAlias, ref g_AliasArray, Do_Valid_Inc: false, addSQLite_Step);
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								g_NoAlias = 1;
								l_Alias = Globals_Renamed.currDataAny + "->";
								g_AliasArray[0].Alias_Renamed = Globals_Renamed.currDataAny;
								if (Operators.CompareString(Strings.LCase(Globals_Renamed.currDataAny), "all", TextCompare: false) == 0)
								{
									if (MyProject.Forms.FrmMain.mnuJoinDuckDB.Checked)
									{
										l_DBType = "DUCKDB";
									}
									else
									{
										l_DBType = "SQLite";
									}
								}
								else
								{
									l_DBType = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + Globals_Renamed.currDataAny);
									l_DBType = General_Procedures.Get_Node_Value(l_DBType);
								}
								if (Operators.CompareString(l_DBType, "iMBigData", TextCompare: false) == 0)
								{
									lstLevel.Enabled = false;
								}
								g_AliasArray[0].DBType = l_DBType;
								Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[0].Alias_Renamed));
								l_Alias = Globals_Renamed.currDataAny + "->";
								Combo2.Enabled = false;
							}
							else
							{
								short num8 = (short)(g_NoAlias - 1);
								for (num3 = 0; num3 <= num8; num3 = (short)unchecked(num3 + 1))
								{
									text = g_AliasArray[num3].Alias_Renamed;
									if (Operators.CompareString(Strings.Mid(g_AliasArray[num3].Alias_Renamed, 1, 1), "U", TextCompare: false) != 0)
									{
										Combo2.Items.Add(General_Procedures.Get_Obj_Alias("T->F", g_AliasArray[num3].Alias_Renamed));
									}
								}
							}
							if (Combo2.Items.Count > 0)
							{
								Combo2.SelectedIndex = 0;
							}
						}
						if (Globals_Renamed.currinttmp == 1)
						{
							Text2.Text = "Filter Expression";
							Label8.Text = "Create a Filter Expression";
							Text2.Enabled = false;
							Label8.Enabled = false;
						}
						else if (Globals_Renamed.currinttmp == 3)
						{
							Label8.Text = "Select a Column Value";
							Text2.Text = "Filter Column";
							Text2.Enabled = false;
							Label8.Enabled = false;
						}
						if (Globals_Renamed.currinttmp == 3)
						{
							if (LikeOperator.LikeString(Globals_Renamed.currtxttmp, "*->*", CompareMethod.Binary))
							{
								Globals_Renamed.currtxttmp = General_Procedures.Strip_Alias(0, Globals_Renamed.currtxttmp);
							}
							TxtExpression.Text = Globals_Renamed.currtxttmp;
							TxtExpression.Height += 40;
							txtComment.Visible = false;
							Label4.Visible = false;
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "", TextCompare: false) != 0)
							{
								cmdAdd.Text = "Modify";
							}
						}
						else if (Globals_Renamed.currinttmp != 4 && Globals_Renamed.currinttmp != 5 && Globals_Renamed.currinttmp != 6 && Globals_Renamed.currinttmp != 7 && Globals_Renamed.currinttmp != 8 && Globals_Renamed.currinttmp != 10 && Globals_Renamed.currinttmp != 11 && Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
						{
							GlobalListColumn = Globals_Renamed.currlisttmp;
							unchecked
							{
								if (Globals_Renamed.currinttmp == 0 || Globals_Renamed.currinttmp == 9)
								{
									text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[4].Value);
									num4 = General_Procedures.SummPlusLevel(Globals_Renamed.currlisttmp);
									if (num4 > 0)
									{
										lstLevel.Text = "Summary + " + Conversions.ToString((int)num4);
									}
									else if (Operators.CompareString(Strings.UCase(text), "EXPR", TextCompare: false) == 0)
									{
										lstLevel.Text = "Summary";
									}
								}
								else if (Globals_Renamed.currinttmp == 1)
								{
									text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[8].Value);
									num4 = General_Procedures.SummPlusLevel(Globals_Renamed.currlisttmp);
									if (num4 > 0)
									{
										lstLevel.Text = "Summary + " + Conversions.ToString((int)num4);
									}
									else if (Operators.CompareString(Strings.UCase(Strings.Mid(text, 2, 1)), "S", TextCompare: false) == 0)
									{
										lstLevel.Text = "Summary";
									}
								}
							}
						}
						if (Globals_Renamed.currinttmp == 3)
						{
							Set_DataType("E", Globals_Renamed.currinttmp);
							CmbDataType.Enabled = false;
							if ((Operators.CompareString(Globals_Renamed.currlvltmp, "0", TextCompare: false) > 0) & (Operators.CompareString(Globals_Renamed.currlvltmp, "9", TextCompare: false) < 0))
							{
								lstLevel.Text = "1";
							}
						}
						else if ((Globals_Renamed.currinttmp == 4) | (Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
						{
							Set_DataType(Globals_Renamed.currdatatypetmp, Globals_Renamed.currinttmp);
						}
						else if (Globals_Renamed.currinttmp == 5 || Globals_Renamed.currinttmp == 6 || Globals_Renamed.currinttmp == 10 || Globals_Renamed.currinttmp == 11)
						{
							Set_DataType("E", Globals_Renamed.currinttmp);
							CmbDataType.Enabled = false;
						}
						else if (Globals_Renamed.currinttmp != 9)
						{
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								Set_DataType(Globals_Renamed.currdatatypetmp, Globals_Renamed.currinttmp);
							}
							else
							{
								Set_DataType("", Globals_Renamed.currinttmp);
							}
						}
						switch (Globals_Renamed.currinttmp)
						{
						case 0:
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								GlobalListColumn = Globals_Renamed.currlisttmp;
								Assign_LstLevel();
								Text2.Text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[1].Value);
								if (Operators.CompareString(Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[9].Value), 1, 3), "***", TextCompare: false) == 0)
								{
									txtComment.Text = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[9].Value), 4);
								}
								else
								{
									txtComment.Text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[9].Value);
								}
								num6 = (short)Strings.InStr(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), "->");
								if (num6 != 0)
								{
									l_Alias = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), 1, num6 + 1);
									text = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), num6 + 2);
								}
								else
								{
									l_Alias = "";
									text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value);
								}
								text = Strings.Replace(text, "!!!!!", Globals_Renamed.CRLF, 1, -1, CompareMethod.Text);
								TxtExpression.Text = Replace_CompExpr("HDR", text, "");
								TxtExpression.Text = BuildForm.Repl_Squiggly(TxtExpression.Text, 0);
								cmdAdd.Text = "Modify";
								TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
							}
							break;
						case 9:
						{
							cmdAdd.Text = "Modify";
							Label1.Text = "Pattern Operator";
							Label4.Text = "Search Pattern";
							lblexpression.Text = "Replace Expression";
							cmdsummary.Enabled = false;
							Assign_LstLevel();
							Text2.Text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[1].Value);
							num6 = (short)Strings.InStr(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), "->");
							if (num6 != 0)
							{
								l_Alias = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), 1, num6 + 1);
								text = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value), num6 + 2);
							}
							else
							{
								l_Alias = "";
								text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[11].Value);
							}
							string[] array = Strings.Split(text, "<;>");
							Globals_Renamed.currdatatypetmp = array[0];
							num3 = 1;
							do
							{
								Globals_Renamed.currdatatypetmp = Globals_Renamed.currdatatypetmp + "<;>" + array[num3];
								num3 = (short)unchecked(num3 + 1);
							}
							while (num3 <= 2);
							Set_DataType(array[3], Globals_Renamed.currinttmp);
							txtComment.Text = array[4];
							TxtExpression.Text = array[5];
							num6 = (short)Strings.InStrRev(TxtExpression.Text, "]]");
							if (num6 > 1)
							{
								TxtExpression.Text = Strings.Mid(TxtExpression.Text, 1, num6 - 1);
							}
							TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
							array = null;
							array = null;
							TxtExpression.Text = Strings.Replace(TxtExpression.Text, "!!!!!", Globals_Renamed.CRLF, 1, -1, CompareMethod.Text);
							TxtExpression.Text = Replace_CompExpr("HDR", TxtExpression.Text, "");
							TxtExpression.Text = BuildForm.Repl_Squiggly(TxtExpression.Text, 0);
							break;
						}
						case 1:
							txtComment.Visible = false;
							Label4.Visible = false;
							TxtExpression.Height += 20;
							lstLevel.Top -= 7;
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "MODIFY", TextCompare: false) == 0)
							{
								GlobalListColumn = Globals_Renamed.currlisttmp;
								num6 = (short)Strings.InStr(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[10].Value), "->");
								if (num6 != 0)
								{
									l_Alias = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[10].Value), 1, num6 + 1);
									text = Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[10].Value), num6 + 2);
								}
								else
								{
									l_Alias = "";
									text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[Globals_Renamed.currrowtmp].Cells[10].Value);
								}
								text = Strings.Replace(text, "!!!!!", Globals_Renamed.CRLF, 1, -1, CompareMethod.Text);
								text = Replace_CompExpr("HDR", text, "");
								TxtExpression.Text = text;
								TxtExpression.Text = BuildForm.Repl_Squiggly(TxtExpression.Text, 0);
								cmdAdd.Text = "Modify";
								Assign_LstLevel();
							}
							TxtExpression.TabIndex = 1;
							TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
							break;
						case 4:
						case 7:
						case 8:
							GlobalListColumn = "";
							if (Operators.CompareString(Globals_Renamed.currtxttmp, "", TextCompare: false) != 0)
							{
								cmdAdd.Text = "Modify";
								text = Globals_Renamed.currDataAny;
								text = Strings.Replace(text, "\"", "'", 1, -1, CompareMethod.Text);
								TxtExpression.Text = text;
								Text2.Text = Globals_Renamed.currtxttmp;
								if (Globals_Renamed.currinttmp == 8)
								{
									Globals_Renamed.currtxttmp = "";
								}
							}
							break;
						case 5:
							Text2.Text = "All Sources Expression";
							Text2.Enabled = false;
							txtComment.Enabled = false;
							text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[Globals_Renamed.currrowtmp].Cells[7].Tag);
							text = Strings.Replace(text, "!!!!!", Globals_Renamed.CRLF, 1, -1, CompareMethod.Text);
							TxtExpression.Text = Replace_CompExpr("HDR", text, "");
							cmdAdd.Text = "Modify";
							TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
							break;
						case 6:
						case 10:
						case 11:
							if (Globals_Renamed.currinttmp == 6 || Globals_Renamed.currinttmp == 11)
							{
								Text2.Text = "All Sources Expression";
								Text2.Enabled = false;
								text = Globals_Renamed.currDataAny;
							}
							else
							{
								text = "";
								if (Operators.CompareString(Globals_Renamed.currDataAny, "", TextCompare: false) != 0)
								{
									text = General_Procedures.Get_Node_Value(Globals_Renamed.currDataAny);
									num6 = (short)Strings.InStr(text, ":");
									if (num6 != 0)
									{
										Text2.Text = Strings.Replace(Strings.Mid(text, 1, num6 - 1), "'", "", 1, -1, CompareMethod.Text);
										text = Strings.Mid(text, num6 + 1);
									}
								}
								else
								{
									text = "function (agg, curr, col, row) {";
									text += "\r\n/*";
									text += "\r\nARGS:";
									text += "\r\n----";
									text += "\r\nagg : Aggregated value";
									text += "\r\ncurr: Current value";
									text += "\r\ncol : Col #";
									text += "\r\nrow : Row Obj. Refer cols row.lot or row['lot']";
									text += "\r\nE.g.,";
									text += "\r\nvar tt=curr+parseInt(row['qty']);return agg+tt;";
									text += "\r\n...or for summed ratios such as yield ...";
									text += "\r\n<$$>=spf_fn$ratio('Yld',agg,row.n,row.o,100,2); return <$$>;";
									text += "\r\n where ...";
									text += "\r\n  Yld=Label; row.n=top col; row.o=bot col;";
									text += "\r\n  100=multiplier; 2=sig figs; <$$> is required";
									text += "\r\n  Computed Col Name MUST start with $ratio";
									text += "\r\n*/";
									text += "\r\n\r\n}";
								}
								cmdoperators.Enabled = false;
							}
							txtComment.Enabled = false;
							text = Strings.Replace(text, "!!!!!", Globals_Renamed.CRLF, 1, -1, CompareMethod.Text);
							TxtExpression.Text = text;
							cmdAdd.Text = "Modify";
							TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
							TxtExpression.Font = new Font("Courier New", 8f, FontStyle.Regular);
							break;
						}
						Load_CColumns();
						ListenExpr = true;
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					case 6898:
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
					goto IL_1b28;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6898;
				continue;
			}
			break;
			IL_1b28:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmComputedSQL_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
	{
		Array.Clear(Globals_Renamed.Functions, 0, Globals_Renamed.Functions.Length);
		Globals_Renamed.nofunctions = 0;
		Dispose();
	}

	private void lstgeneral_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
						errsource = "frmComputedSQL - lstgeneral_Click";
						string text = "";
						string MyTrueCol = "";
						short num3 = 0;
						if (lstgeneral.SelectedIndex == -1)
						{
							goto end_IL_0001;
						}
						text = lstgeneral.Text;
						if (Operators.CompareString(lblgeneral.Text, "Functions", TextCompare: false) == 0)
						{
							short nofunctions = Globals_Renamed.nofunctions;
							for (num3 = 0; num3 <= nofunctions; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.CompareString(Globals_Renamed.Functions[num3].Name, text, TextCompare: false) == 0)
								{
									lbldesc.Text = Globals_Renamed.Functions[num3].Description;
									break;
								}
							}
						}
						else if (Operators.CompareString(lblgeneral.Text, "Summary Functions", TextCompare: false) == 0)
						{
							if (LikeOperator.LikeString(Strings.UCase(text), "PERCENTILE_CONT*", CompareMethod.Binary))
							{
								text = "PERCENTILE_CONT";
							}
							else if (LikeOperator.LikeString(Strings.UCase(text), "LISTAGG*", CompareMethod.Binary))
							{
								text = "LISTAGG";
							}
							else if (LikeOperator.LikeString(Strings.UCase(text), "GROUP_CONCAT*", CompareMethod.Binary))
							{
								text = "GROUP_CONCAT";
							}
							else if (LikeOperator.LikeString(Strings.UCase(text), "COUNT(*)", CompareMethod.Binary))
							{
								text = "COUNT";
							}
							lbldesc.Text = General_Procedures.Get_UI(Strings.LCase(text));
						}
						else if (Operators.CompareString(lblgeneral.Text, "Comparison Operators", TextCompare: false) == 0)
						{
							lbldesc.Text = BuildForm.Get_Operator_Comment_SQL(text);
						}
						else if (Operators.CompareString(lblgeneral.Text, "Available Columns", TextCompare: false) == 0)
						{
							if (Conversions.ToBoolean(Operators.AndObject(Globals_Renamed.currinttmp == 5, Operators.CompareObjectEqual(lstgeneral.SelectedItem, "[MyPivotedColumn(s)]", TextCompare: false))))
							{
								lbldesc.Text = "This column allows you to reference all columns created by a pivot operation. You can apply a summary expression to the pivoted columns via this column reference. E.g.,Sum([MyPivotedColumn(s)]) / Sum ({Total_Tested}) * 100";
							}
							else
							{
								lbldesc.Text = Get_Grid_Attrib("C", text, ref MyTrueCol);
							}
						}
						else
						{
							lbldesc.Text = "";
						}
						goto end_IL_0001;
					}
					case 638:
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
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 638;
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

	private void lstgeneral_DoubleClick(object eventSender, EventArgs eventArgs)
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
						errsource = "frmComputedSQL - lstgeneral_DblClick";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						string text = "";
						string MyTrueCol = "";
						if (Operators.CompareString(lblgeneral.Text, "Functions", TextCompare: false) == 0)
						{
							TxtExpression.SelectedText = " " + lstgeneral.Text + " ";
							num3 = (short)Strings.InStr(TxtExpression.Text, "<expr>");
							if (num3 != 0)
							{
								TxtExpression.SelectionStart = num3 - 1;
								TxtExpression.SelectionLength = Strings.Len("<expr>");
							}
						}
						else if (Operators.CompareString(lblgeneral.Text, "Summary Functions", TextCompare: false) == 0)
						{
							if ((Operators.CompareString(Strings.UCase(lstgeneral.Text), "COUNT(*)", TextCompare: false) == 0) | LikeOperator.LikeString(Strings.UCase(lstgeneral.Text), "PERCENTILE*", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(lstgeneral.Text), "LISTAGG*", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(lstgeneral.Text), "GROUP_CONCAT*", CompareMethod.Binary) | (Operators.CompareString(Strings.UCase(lstgeneral.Text), "COUNT(DISTINCT <EXPR>)", TextCompare: false) == 0))
							{
								TxtExpression.SelectedText = " " + lstgeneral.Text + " ";
								if (LikeOperator.LikeString(Strings.UCase(lstgeneral.Text), "PERCENTILE*", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(lstgeneral.Text), "GROUP_CONCAT*", CompareMethod.Binary) | (Operators.CompareString(Strings.UCase(lstgeneral.Text), "COUNT(DISTINCT <EXPR>)", TextCompare: false) == 0))
								{
									TxtExpression.SelectionStart = Strings.InStr(TxtExpression.Text, "<expr>") - 1;
									TxtExpression.SelectionLength = Strings.Len("<expr>");
								}
								else
								{
									TxtExpression.SelectionStart = Strings.Len(TxtExpression.Text);
								}
							}
							else
							{
								TxtExpression.SelectedText = " " + lstgeneral.Text + "( <expr> ) ";
								TxtExpression.SelectionStart = Strings.InStr(TxtExpression.Text, "<expr>") - 1;
								TxtExpression.SelectionLength = Strings.Len("<expr>");
							}
						}
						else if (Operators.CompareString(lblgeneral.Text, "Available Columns", TextCompare: false) == 0)
						{
							text = lstgeneral.Text;
							if (Globals_Renamed.currinttmp == 3)
							{
								if (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) != 0)
								{
									TxtExpression.SelectedText = " " + text + " ";
								}
								else
								{
									GlobalListColumn = Get_Grid_Attrib("L", text, ref MyTrueCol);
									MyTrueCol = General_Procedures.Strip_Alias(0, MyTrueCol);
									TxtExpression.SelectedText = " " + MyTrueCol + " ";
								}
							}
							else if (Globals_Renamed.currinttmp == 4)
							{
								GlobalListColumn = "";
								TxtExpression.SelectedText = " :Name(\"" + text + "\") ";
							}
							else if ((Globals_Renamed.currinttmp == 7) | (Globals_Renamed.currinttmp == 8))
							{
								GlobalListColumn = "";
								TxtExpression.SelectedText = " " + text;
							}
							else if (Globals_Renamed.currinttmp == 6)
							{
								GlobalListColumn = "";
								TxtExpression.SelectedText = " [" + text + "] ";
							}
							else if (Globals_Renamed.currinttmp == 10)
							{
								GlobalListColumn = "";
								TxtExpression.SelectedText = " row['" + text + "']";
							}
							else if (Globals_Renamed.currinttmp == 11)
							{
								GlobalListColumn = "";
								TxtExpression.SelectedText = " " + text;
							}
							else if (Globals_Renamed.currinttmp == 5)
							{
								if (Conversions.ToBoolean(Operators.AndObject(Globals_Renamed.currinttmp == 5, Operators.CompareObjectEqual(lstgeneral.SelectedItem, "[MyPivotedColumn(s)]", TextCompare: false))))
								{
									TxtExpression.SelectedText = " " + text + " ";
								}
								else
								{
									TxtExpression.SelectedText = " {" + text + "} ";
								}
							}
							else if (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) != 0)
							{
								GlobalListColumn = "";
								if (LikeOperator.LikeString(text, ":_data_*:", CompareMethod.Binary) || LikeOperator.LikeString(text, ":_label_*:", CompareMethod.Binary))
								{
									TxtExpression.SelectedText = " " + Strings.Replace(text, ":", "", 1, -1, CompareMethod.Text) + " ";
								}
								else
								{
									TxtExpression.SelectedText = " {" + text + "} ";
								}
							}
							else
							{
								MyTrueCol = "";
								GlobalListColumn = Get_Grid_Attrib("L", text, ref MyTrueCol);
								MyTrueCol = Strings.UCase(MyTrueCol);
								if (((Globals_Renamed.currinttmp == 0) | (Globals_Renamed.currinttmp == 1)) && Operators.CompareString(Strings.LCase(Combo2.Text), "all sources", TextCompare: false) == 0 && LikeOperator.LikeString(GlobalListColumn, "s+*", CompareMethod.Binary))
								{
									GlobalListColumn = "";
								}
								if ((Globals_Renamed.currinttmp == 1) & (Operators.CompareString(lstLevel.Text, "0", TextCompare: false) == 0) & ((Strings.InStr(MyTrueCol, ") OVER (") != 0) | (Strings.InStr(MyTrueCol, "PARTITION BY") != 0) | (Strings.InStr(MyTrueCol, "ORDER BY") != 0)))
								{
									num5 = (short)Interaction.MsgBox(General_Procedures.Get_UI("errann1"), MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Potential Analytical Function");
									if (num5 == 6)
									{
										lstLevel.Text = "1";
									}
								}
								if (LikeOperator.LikeString(text, ":*._data_:", CompareMethod.Binary) || LikeOperator.LikeString(text, ":*._label_:", CompareMethod.Binary))
								{
									TxtExpression.SelectedText = " " + Strings.Replace(text, ":", "", 1, -1, CompareMethod.Text) + " ";
								}
								else
								{
									TxtExpression.SelectedText = " {" + text + "} ";
								}
							}
							if (Strings.InStr(TxtExpression.Text, "<expr>") != 0)
							{
								TxtExpression.SelectionStart = Strings.InStr(TxtExpression.Text, "<expr>") - 1;
								TxtExpression.SelectionLength = Strings.Len("<expr>");
							}
						}
						else
						{
							TxtExpression.SelectedText = " " + lstgeneral.Text + " ";
						}
						TxtExpression.Focus();
						goto end_IL_0001;
					}
					case 2085:
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
					goto IL_085b;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2085;
				continue;
			}
			break;
			IL_085b:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void lstLevel_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
				case 440:
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
							goto IL_002f;
						case 4:
							goto IL_003e;
						case 5:
							goto IL_005a;
						case 6:
							goto IL_0069;
						case 7:
							goto IL_0078;
						case 8:
							goto IL_0087;
						case 11:
							goto IL_00a1;
						case 12:
							goto IL_00b1;
						case 13:
							goto IL_00c1;
						case 14:
							goto IL_00de;
						case 9:
						case 10:
						case 15:
						case 16:
						case 17:
							goto IL_00f1;
						case 18:
							goto IL_0113;
						case 19:
							goto IL_0128;
						case 20:
							goto IL_0137;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0113:
					num2 = 18;
					CmdColumns_Click(cmdcolumns, new EventArgs());
					goto IL_0128;
					IL_0128:
					num2 = 19;
					if (!ListenExpr)
					{
						break;
					}
					goto IL_0137;
					IL_00f1:
					num2 = 17;
					if (Operators.CompareString(lblgeneral.Text, "Available Columns", TextCompare: false) == 0)
					{
						goto IL_0113;
					}
					goto IL_0128;
					IL_0137:
					num2 = 20;
					fSaveExpr = true;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(lstLevel.SelectedItem.ToString(), "Summary", TextCompare: false) == 0)
					{
						goto IL_002f;
					}
					goto IL_00a1;
					IL_002f:
					num2 = 3;
					cmdsummary.Enabled = true;
					goto IL_003e;
					IL_003e:
					num2 = 4;
					if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0)
					{
						goto IL_005a;
					}
					goto IL_00f1;
					IL_005a:
					num2 = 5;
					cmdfunctions.Enabled = false;
					goto IL_0069;
					IL_0069:
					num2 = 6;
					cmdcolumns.Enabled = false;
					goto IL_0078;
					IL_0078:
					num2 = 7;
					cmdsummary.Enabled = false;
					goto IL_0087;
					IL_0087:
					num2 = 8;
					Interaction.MsgBox("Mongo Databases do not yet support summary expressions. To implement summary expressions, add the summary functions to columns on the Columns Tab and build the filter expressions at level Summary + 1", MsgBoxStyle.Exclamation, "Mongo DB Limitations");
					goto IL_00f1;
					IL_00a1:
					num2 = 11;
					cmdsummary.Enabled = false;
					goto IL_00b1;
					IL_00b1:
					num2 = 12;
					cmdfunctions.Enabled = true;
					goto IL_00c1;
					IL_00c1:
					num2 = 13;
					if (Operators.CompareString(l_DBType, "Mongo", TextCompare: false) == 0)
					{
						goto IL_00de;
					}
					goto IL_00f1;
					IL_00de:
					num2 = 14;
					cmdcolumns.Enabled = true;
					goto IL_00f1;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				Refresh();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 440;
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

	private void lstLevel_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
	{
		short num = checked((short)Strings.Asc(eventArgs.KeyChar));
		num = 0;
		eventArgs.KeyChar = Strings.Chr(num);
		if (num == 0)
		{
			eventArgs.Handled = true;
		}
	}

	private void Text2_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
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
				case 246:
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
						case 5:
							goto IL_0088;
						case 4:
						case 6:
						case 7:
							goto IL_008d;
						case 8:
							goto IL_009c;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0088:
					num2 = 5;
					num5 = 0;
					goto IL_008d;
					IL_008d:
					num2 = 7;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_009c;
					IL_001a:
					num2 = 3;
					if (!(num5 == 8 || num5 == 127 || num5 == 95 || num5 == 36 || (num5 >= 48 && num5 <= 57) || (num5 >= 97 && num5 <= 122) || (num5 >= 65 && num5 <= 90) || num5 == 1 || num5 == 3 || num5 == 22 || num5 == 24))
					{
						goto IL_0088;
					}
					goto IL_008d;
					IL_009c:
					num2 = 8;
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
				num2 = 9;
				eventArgs.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 246;
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

	private void cmd2_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string selectedText = default(string);
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
							goto IL_0028;
						case 4:
							goto IL_0075;
						case 5:
						case 6:
							goto IL_007f;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0028:
					num2 = 3;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "text", new object[0], null, null, null), "\"", TextCompare: false) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null), "doublequote", TextCompare: false))
					{
						goto IL_0075;
					}
					goto IL_007f;
					IL_007f:
					num2 = 6;
					TxtExpression.SelectedText = selectedText;
					break;
					IL_000b:
					num2 = 2;
					selectedText = Conversions.ToString(NewLateBinding.LateGet(sender, null, "text", new object[0], null, null, null));
					goto IL_0028;
					IL_0075:
					num2 = 4;
					selectedText = "</dq/>";
					goto IL_007f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				TxtExpression.Focus();
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

	private void TxtExpression_TextChanged(object sender, EventArgs e)
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
					if (!ListenExpr)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				fSaveExpr = true;
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

	private void txtComment_TextChanged(object sender, EventArgs e)
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
					if (!ListenExpr)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				fSaveExpr = true;
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

	private void CmbDataType_SelectedIndexChanged(object sender, EventArgs e)
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
					if (!ListenExpr)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				fSaveExpr = true;
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

	private void Text2_TextChanged(object sender, EventArgs e)
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
					if (!ListenExpr)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				fSaveExpr = true;
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

	private void lstgeneral_KeyPress(object sender, KeyPressEventArgs e)
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
				case 92:
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
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (num5 != 13)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				lstgeneral_DoubleClick(e, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 92;
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

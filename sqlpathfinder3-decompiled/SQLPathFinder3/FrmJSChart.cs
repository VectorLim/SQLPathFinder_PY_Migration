using System;
using System.Collections;
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
public class FrmJSChart : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdRemove")]
	private Button _CmdRemove;

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
	[AccessedThroughProperty("LstX")]
	private ListBox _LstX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdX")]
	private Button _CmdX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdY")]
	private Button _CmdY;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TabChart")]
	private TabControl _TabChart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private Button _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp")]
	private Button _CmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDelete")]
	private Button _cmdDelete;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown")]
	private Button _cmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridY")]
	private DataGridView _GridY;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowseIn")]
	private Button _CmdBrowseIn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstBy")]
	private ListBox _LstBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstGroup")]
	private ListBox _LstGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBy")]
	private Button _cmdBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdGroup")]
	private Button _cmdGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDelete1")]
	private Button _cmdDelete1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown1")]
	private Button _cmdDown1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp1")]
	private Button _cmdUp1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridWhere")]
	private DataGridView _GridWhere;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdRefresh")]
	private Button _cmdRefresh;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnucolorset")]
	private ToolStripMenuItem _mnucolorset;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnucolorclear")]
	private ToolStripMenuItem _mnucolorclear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAddR")]
	private Button _cmdAddR;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuexpr")]
	private ToolStripMenuItem _mnuexpr;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbFilterChart")]
	private ToolStripComboBox _cmbFilterChart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRange")]
	private ToolStripMenuItem _mnuRange;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBackCOlor")]
	private Button _cmdBackCOlor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuX")]
	private ToolStripMenuItem _mnuX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGroup")]
	private ToolStripMenuItem _mnuGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBy")]
	private ToolStripMenuItem _mnuBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuY")]
	private ToolStripMenuItem _mnuY;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRemove")]
	private ToolStripMenuItem _mnuRemove;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClear")]
	private ToolStripMenuItem _mnuClear;

	public string f_TemplateType2;

	public string fActual;

	public string fDisplay;

	public string fName;

	private string f_MyPre;

	private string f_MyPost;

	private const int l_NoOptsTabcols = -1;

	public string LastChartTable;

	public bool f_OK;

	private string f_Y_Map;

	private string l_DragSource;

	private int l_DragIdx;

	private string fRange;

	private int f_WRows;

	private const short Colw_And = 0;

	private const short Colw_Pareno = 1;

	private const short Colw_Col = 2;

	private const short Colw_Opr = 3;

	private const short Colw_Val = 4;

	private const short Colw_ParenC = 5;

	private const short cSum = 2;

	private const short cAxis = 3;

	private const short cpstyle = 4;

	private const short cFillC = 6;

	private const short cLineC = 7;

	private const short cTTC = 14;

	private const short clSort = 15;

	private const short colleg = 16;

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabOptions")]
	internal virtual TabPage TabOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label31")]
	internal virtual Label Label31
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label30")]
	internal virtual Label Label30
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabTitles")]
	internal virtual TabPage TabTitles
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkGridY")]
	internal virtual CheckBox ChkGridY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkGridX")]
	internal virtual CheckBox ChkGridX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2Axis")]
	internal virtual TextBox TxtY2Axis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYAxis")]
	internal virtual TextBox TxtYAxis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXAxis")]
	internal virtual TextBox TxtXAxis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtTitle")]
	internal virtual TextBox TxtTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblY2")]
	internal virtual Label LblY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblY")]
	internal virtual Label LblY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblX")]
	internal virtual Label LblX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblTitle")]
	internal virtual Label LblTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabCols")]
	internal virtual TabPage TabCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdRemove
	{
		[CompilerGenerated]
		get
		{
			return _CmdRemove;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdRemove_Click;
			Button button = _CmdRemove;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdRemove = value;
			button = _CmdRemove;
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

	internal virtual ListBox LstX
	{
		[CompilerGenerated]
		get
		{
			return _LstX;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DragEventHandler value2 = LstX_DragDrop;
			DragEventHandler value3 = LstX_DragOver;
			MouseEventHandler value4 = LstX_MouseDown;
			EventHandler value5 = LstX_GotFocus;
			ListBox listBox = _LstX;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragOver -= value3;
				listBox.MouseDown -= value4;
				listBox.GotFocus -= value5;
			}
			_LstX = value;
			listBox = _LstX;
			if (listBox != null)
			{
				listBox.DragDrop += value2;
				listBox.DragOver += value3;
				listBox.MouseDown += value4;
				listBox.GotFocus += value5;
			}
		}
	}

	internal virtual Button CmdX
	{
		[CompilerGenerated]
		get
		{
			return _CmdX;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdX_Click;
			Button button = _CmdX;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdX = value;
			button = _CmdX;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdY
	{
		[CompilerGenerated]
		get
		{
			return _CmdY;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdY_Click;
			Button button = _CmdY;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdY = value;
			button = _CmdY;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LblCols")]
	internal virtual Label LblCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LstColumns")]
	internal virtual ListBox LstColumns
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblInFile")]
	internal virtual Label LblInFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TabControl TabChart
	{
		[CompilerGenerated]
		get
		{
			return _TabChart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TabChart_Click;
			TabControl tabControl = _TabChart;
			if (tabControl != null)
			{
				tabControl.Click -= value2;
			}
			_TabChart = value;
			tabControl = _TabChart;
			if (tabControl != null)
			{
				tabControl.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TextBox27")]
	internal virtual TextBox TextBox27
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox24")]
	internal virtual ComboBox ComboBox24
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox25")]
	internal virtual ComboBox ComboBox25
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox28")]
	internal virtual TextBox TextBox28
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label36")]
	internal virtual Label Label36
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox29")]
	internal virtual TextBox TextBox29
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox26")]
	internal virtual ComboBox ComboBox26
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox27")]
	internal virtual ComboBox ComboBox27
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox30")]
	internal virtual TextBox TextBox30
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label37")]
	internal virtual Label Label37
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox31")]
	internal virtual TextBox TextBox31
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox28")]
	internal virtual ComboBox ComboBox28
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox29")]
	internal virtual ComboBox ComboBox29
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox32")]
	internal virtual TextBox TextBox32
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label38")]
	internal virtual Label Label38
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label39")]
	internal virtual Label Label39
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox33")]
	internal virtual TextBox TextBox33
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox30")]
	internal virtual ComboBox ComboBox30
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox31")]
	internal virtual ComboBox ComboBox31
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox34")]
	internal virtual TextBox TextBox34
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label40")]
	internal virtual Label Label40
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox32")]
	internal virtual ComboBox ComboBox32
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label41")]
	internal virtual Label Label41
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox1")]
	internal virtual CheckBox CheckBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label42")]
	internal virtual Label Label42
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox2")]
	internal virtual CheckBox CheckBox2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox3")]
	internal virtual CheckBox CheckBox3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox4")]
	internal virtual CheckBox CheckBox4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox5")]
	internal virtual CheckBox CheckBox5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label43")]
	internal virtual Label Label43
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox7")]
	internal virtual CheckBox CheckBox7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox8")]
	internal virtual CheckBox CheckBox8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox9")]
	internal virtual CheckBox CheckBox9
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckBox15")]
	internal virtual CheckBox CheckBox15
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label44")]
	internal virtual Label Label44
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label45")]
	internal virtual Label Label45
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label46")]
	internal virtual Label Label46
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox35")]
	internal virtual TextBox TextBox35
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox36")]
	internal virtual TextBox TextBox36
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox37")]
	internal virtual TextBox TextBox37
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox38")]
	internal virtual TextBox TextBox38
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label47")]
	internal virtual Label Label47
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label48")]
	internal virtual Label Label48
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label49")]
	internal virtual Label Label49
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label50")]
	internal virtual Label Label50
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GrpTitle")]
	internal virtual GroupBox GrpTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdClear
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

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label7")]
	internal virtual Label Label7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	internal virtual DataGridView GridY
	{
		[CompilerGenerated]
		get
		{
			return _GridY;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = GridY_MouseDoubleClick;
			KeyEventHandler value3 = GridY_KeyDown;
			DataGridView dataGridView = _GridY;
			if (dataGridView != null)
			{
				dataGridView.MouseDoubleClick -= value2;
				dataGridView.KeyDown -= value3;
			}
			_GridY = value;
			dataGridView = _GridY;
			if (dataGridView != null)
			{
				dataGridView.MouseDoubleClick += value2;
				dataGridView.KeyDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("TxtIn")]
	internal virtual TextBox TxtIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdBrowseIn
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowseIn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowseIn_Click;
			Button button = _CmdBrowseIn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowseIn = value;
			button = _CmdBrowseIn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtY2High")]
	internal virtual TextBox TxtY2High
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYHigh")]
	internal virtual TextBox TxtYHigh
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2Low")]
	internal virtual TextBox TxtY2Low
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYLow")]
	internal virtual TextBox TxtYLow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXHigh")]
	internal virtual TextBox TxtXHigh
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXLow")]
	internal virtual TextBox TxtXLow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHigh")]
	internal virtual Label LblHigh
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblLow")]
	internal virtual Label LblLow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkLogY")]
	internal virtual CheckBox ChkLogY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkLogX")]
	internal virtual CheckBox ChkLogX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXAxis")]
	internal virtual Label LblXAxis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblYAxis")]
	internal virtual Label LblYAxis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbXRotate")]
	internal virtual ComboBox CmbXRotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblRotate")]
	internal virtual Label LblRotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblLog")]
	internal virtual Label LblLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbFrameY")]
	internal virtual ComboBox CmbFrameY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbFrameX")]
	internal virtual ComboBox CmbFrameX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXSize")]
	internal virtual Label LblXSize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColorDialog1")]
	internal virtual ColorDialog ColorDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2Incr")]
	internal virtual TextBox TxtY2Incr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYIncr")]
	internal virtual TextBox TxtYIncr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblInc")]
	internal virtual Label LblInc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbYRotate")]
	internal virtual ComboBox CmbYRotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ListBox LstBy
	{
		[CompilerGenerated]
		get
		{
			return _LstBy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = LstBy_GotFocus;
			ListBox listBox = _LstBy;
			if (listBox != null)
			{
				listBox.GotFocus -= value2;
			}
			_LstBy = value;
			listBox = _LstBy;
			if (listBox != null)
			{
				listBox.GotFocus += value2;
			}
		}
	}

	internal virtual ListBox LstGroup
	{
		[CompilerGenerated]
		get
		{
			return _LstGroup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = LstGroup_GotFocus;
			ListBox listBox = _LstGroup;
			if (listBox != null)
			{
				listBox.GotFocus -= value2;
			}
			_LstGroup = value;
			listBox = _LstGroup;
			if (listBox != null)
			{
				listBox.GotFocus += value2;
			}
		}
	}

	internal virtual Button cmdBy
	{
		[CompilerGenerated]
		get
		{
			return _cmdBy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBy_Click;
			Button button = _cmdBy;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdBy = value;
			button = _cmdBy;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdGroup
	{
		[CompilerGenerated]
		get
		{
			return _cmdGroup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGroup_Click;
			Button button = _cmdGroup;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdGroup = value;
			button = _cmdGroup;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdDelete1
	{
		[CompilerGenerated]
		get
		{
			return _cmdDelete1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelete1_Click;
			Button button = _cmdDelete1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDelete1 = value;
			button = _cmdDelete1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdDown1
	{
		[CompilerGenerated]
		get
		{
			return _cmdDown1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDown1_Click;
			Button button = _cmdDown1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDown1 = value;
			button = _cmdDown1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdUp1
	{
		[CompilerGenerated]
		get
		{
			return _cmdUp1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp1_Click;
			Button button = _cmdUp1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdUp1 = value;
			button = _cmdUp1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual DataGridView GridWhere
	{
		[CompilerGenerated]
		get
		{
			return _GridWhere;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = GridWhere_KeyDown;
			DataGridView dataGridView = _GridWhere;
			if (dataGridView != null)
			{
				dataGridView.KeyDown -= value2;
			}
			_GridWhere = value;
			dataGridView = _GridWhere;
			if (dataGridView != null)
			{
				dataGridView.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("ContextGridY")]
	internal virtual ContextMenuStrip ContextGridY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnucolorset
	{
		[CompilerGenerated]
		get
		{
			return _mnucolorset;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnucolorset_Click;
			ToolStripMenuItem toolStripMenuItem = _mnucolorset;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnucolorset = value;
			toolStripMenuItem = _mnucolorset;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnucolorclear
	{
		[CompilerGenerated]
		get
		{
			return _mnucolorclear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnucolorclear_Click;
			ToolStripMenuItem toolStripMenuItem = _mnucolorclear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnucolorclear = value;
			toolStripMenuItem = _mnucolorclear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColAnd")]
	internal virtual DataGridViewComboBoxColumn ColAnd
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColParenO")]
	internal virtual DataGridViewComboBoxColumn ColParenO
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColGrid")]
	internal virtual DataGridViewComboBoxColumn ColGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColOprGrid")]
	internal virtual DataGridViewComboBoxColumn ColOprGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColValGrid")]
	internal virtual DataGridViewTextBoxColumn ColValGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColParenC")]
	internal virtual DataGridViewComboBoxColumn ColParenC
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

	[field: AccessedThroughProperty("cmbperRow")]
	internal virtual ComboBox cmbperRow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbY2Rotate")]
	internal virtual ComboBox cmbY2Rotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkLogY2")]
	internal virtual CheckBox chkLogY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdAddR
	{
		[CompilerGenerated]
		get
		{
			return _cmdAddR;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAddR_Click;
			Button button = _cmdAddR;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdAddR = value;
			button = _cmdAddR;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuexpr
	{
		[CompilerGenerated]
		get
		{
			return _mnuexpr;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuexpr_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuexpr;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuexpr = value;
			toolStripMenuItem = _mnuexpr;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("OptionsToolStripMenuItem")]
	internal virtual ToolStripMenuItem OptionsToolStripMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnureportsource")]
	internal virtual ToolStripMenuItem mnureportsource
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbXDT")]
	internal virtual ComboBox cmbXDT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblxType")]
	internal virtual Label lblxType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblFmt")]
	internal virtual Label lblFmt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbXFmt")]
	internal virtual ComboBox cmbXFmt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label10")]
	internal virtual Label Label10
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label11")]
	internal virtual Label Label11
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label6")]
	internal virtual Label Label6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblpad")]
	internal virtual Label lblpad
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtPadB")]
	internal virtual TextBox TxtPadB
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtPadR")]
	internal virtual TextBox TxtPadR
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtPadT")]
	internal virtual TextBox TxtPadT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtPadL")]
	internal virtual TextBox TxtPadL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2GStp")]
	internal virtual TextBox TxtY2GStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYGStp")]
	internal virtual TextBox TxtYGStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXGStp")]
	internal virtual TextBox TxtXGStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2TStp")]
	internal virtual TextBox TxtY2TStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYTStp")]
	internal virtual TextBox TxtYTStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXTStp")]
	internal virtual TextBox TxtXTStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtY2LStp")]
	internal virtual TextBox TxtY2LStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtYLStp")]
	internal virtual TextBox TxtYLStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXLStp")]
	internal virtual TextBox TxtXLStp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtXIncr")]
	internal virtual TextBox TxtXIncr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label14")]
	internal virtual Label Label14
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label13")]
	internal virtual Label Label13
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label12")]
	internal virtual Label Label12
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkonTicY2")]
	internal virtual CheckBox ChkonTicY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label15")]
	internal virtual Label Label15
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkonTicY")]
	internal virtual CheckBox ChkonTicY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ChkonTicX")]
	internal virtual CheckBox ChkonTicX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnufilterchart")]
	internal virtual ToolStripMenuItem mnufilterchart
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox cmbFilterChart
	{
		[CompilerGenerated]
		get
		{
			return _cmbFilterChart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbFilterChart_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = _cmbFilterChart;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			_cmbFilterChart = value;
			toolStripComboBox = _cmbFilterChart;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuRange
	{
		[CompilerGenerated]
		get
		{
			return _mnuRange;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRange_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRange;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRange = value;
			toolStripMenuItem = _mnuRange;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtBackColor")]
	internal virtual TextBox TxtBackColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblbackcolor")]
	internal virtual Label lblbackcolor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdBackCOlor
	{
		[CompilerGenerated]
		get
		{
			return _cmdBackCOlor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBackCOlor_Click;
			Button button = _cmdBackCOlor;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdBackCOlor = value;
			button = _cmdBackCOlor;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ContextCols")]
	internal virtual ContextMenuStrip ContextCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuX
	{
		[CompilerGenerated]
		get
		{
			return _mnuX;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdX_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuX = value;
			toolStripMenuItem = _mnuX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuGroup
	{
		[CompilerGenerated]
		get
		{
			return _mnuGroup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGroup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuGroup;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuGroup = value;
			toolStripMenuItem = _mnuGroup;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuBy
	{
		[CompilerGenerated]
		get
		{
			return _mnuBy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBy_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBy = value;
			toolStripMenuItem = _mnuBy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuY
	{
		[CompilerGenerated]
		get
		{
			return _mnuY;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdY_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuY;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuY = value;
			toolStripMenuItem = _mnuY;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ContextXGroupBy")]
	internal virtual ContextMenuStrip ContextXGroupBy
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuRemove
	{
		[CompilerGenerated]
		get
		{
			return _mnuRemove;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdRemove_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRemove;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRemove = value;
			toolStripMenuItem = _mnuRemove;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuClear
	{
		[CompilerGenerated]
		get
		{
			return _mnuClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClear_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClear = value;
			toolStripMenuItem = _mnuClear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbYFmt")]
	internal virtual ComboBox cmbYFmt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbY2Fmt")]
	internal virtual ComboBox cmbY2Fmt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chklegend")]
	internal virtual CheckBox chklegend
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Columns")]
	internal virtual DataGridViewTextBoxColumn Columns
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColHdr")]
	internal virtual DataGridViewTextBoxColumn ColHdr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColSumm")]
	internal virtual DataGridViewComboBoxColumn ColSumm
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAxis")]
	internal virtual DataGridViewComboBoxColumn ColAxis
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColStyle")]
	internal virtual DataGridViewComboBoxColumn ColStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColLW")]
	internal virtual DataGridViewComboBoxColumn ColLW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colfillcolor")]
	internal virtual DataGridViewTextBoxColumn colfillcolor
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

	[field: AccessedThroughProperty("colsymtype")]
	internal virtual DataGridViewComboBoxColumn colsymtype
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colsymsize")]
	internal virtual DataGridViewComboBoxColumn colsymsize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coldash")]
	internal virtual DataGridViewComboBoxColumn coldash
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colopacity")]
	internal virtual DataGridViewComboBoxColumn colopacity
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("collabels")]
	internal virtual DataGridViewComboBoxColumn collabels
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colconnect")]
	internal virtual DataGridViewComboBoxColumn colconnect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colTT")]
	internal virtual DataGridViewTextBoxColumn colTT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colsort")]
	internal virtual DataGridViewComboBoxColumn colsort
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colLEGFMT")]
	internal virtual DataGridViewTextBoxColumn colLEGFMT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmJSChart()
	{
		base.Load += FrmChart_Load;
		base.Resize += FrmJSChart_Resize;
		f_TemplateType2 = "";
		fActual = "";
		fDisplay = "";
		fName = "";
		f_MyPre = "";
		f_MyPost = "";
		LastChartTable = "";
		f_OK = false;
		f_Y_Map = "";
		l_DragSource = "";
		l_DragIdx = -1;
		fRange = "";
		f_WRows = 100;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmJSChart));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.CmdRemove = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.CmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdDelete = new System.Windows.Forms.Button();
		this.CmdBrowseIn = new System.Windows.Forms.Button();
		this.cmdDelete1 = new System.Windows.Forms.Button();
		this.cmdDown1 = new System.Windows.Forms.Button();
		this.cmdUp1 = new System.Windows.Forms.Button();
		this.cmdRefresh = new System.Windows.Forms.Button();
		this.chkHTTP = new System.Windows.Forms.CheckBox();
		this.cmdAddR = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.LblInc = new System.Windows.Forms.Label();
		this.LblRotate = new System.Windows.Forms.Label();
		this.LblLog = new System.Windows.Forms.Label();
		this.LblLow = new System.Windows.Forms.Label();
		this.LblHigh = new System.Windows.Forms.Label();
		this.Label5 = new System.Windows.Forms.Label();
		this.LblXSize = new System.Windows.Forms.Label();
		this.lblxType = new System.Windows.Forms.Label();
		this.lblpad = new System.Windows.Forms.Label();
		this.Label15 = new System.Windows.Forms.Label();
		this.cmdBackCOlor = new System.Windows.Forms.Button();
		this.CmbXRotate = new System.Windows.Forms.ComboBox();
		this.ChkLogY = new System.Windows.Forms.CheckBox();
		this.ChkLogX = new System.Windows.Forms.CheckBox();
		this.TxtXLow = new System.Windows.Forms.TextBox();
		this.TxtY2High = new System.Windows.Forms.TextBox();
		this.TxtXHigh = new System.Windows.Forms.TextBox();
		this.TxtY2Low = new System.Windows.Forms.TextBox();
		this.TxtYHigh = new System.Windows.Forms.TextBox();
		this.TxtYLow = new System.Windows.Forms.TextBox();
		this.ChkGridY = new System.Windows.Forms.CheckBox();
		this.ChkGridX = new System.Windows.Forms.CheckBox();
		this.CmbFrameY = new System.Windows.Forms.ComboBox();
		this.CmbFrameX = new System.Windows.Forms.ComboBox();
		this.TxtYIncr = new System.Windows.Forms.TextBox();
		this.TxtY2Incr = new System.Windows.Forms.TextBox();
		this.CmbYRotate = new System.Windows.Forms.ComboBox();
		this.CmdY = new System.Windows.Forms.Button();
		this.CmdX = new System.Windows.Forms.Button();
		this.TabOptions = new System.Windows.Forms.TabPage();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label7 = new System.Windows.Forms.Label();
		this.Label31 = new System.Windows.Forms.Label();
		this.Label30 = new System.Windows.Forms.Label();
		this.TabTitles = new System.Windows.Forms.TabPage();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.chklegend = new System.Windows.Forms.CheckBox();
		this.TxtBackColor = new System.Windows.Forms.TextBox();
		this.lblbackcolor = new System.Windows.Forms.Label();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbY2Fmt = new System.Windows.Forms.ComboBox();
		this.cmbYFmt = new System.Windows.Forms.ComboBox();
		this.ChkonTicY2 = new System.Windows.Forms.CheckBox();
		this.ChkonTicY = new System.Windows.Forms.CheckBox();
		this.ChkonTicX = new System.Windows.Forms.CheckBox();
		this.Label14 = new System.Windows.Forms.Label();
		this.Label13 = new System.Windows.Forms.Label();
		this.Label12 = new System.Windows.Forms.Label();
		this.TxtY2GStp = new System.Windows.Forms.TextBox();
		this.TxtYGStp = new System.Windows.Forms.TextBox();
		this.TxtXGStp = new System.Windows.Forms.TextBox();
		this.TxtY2TStp = new System.Windows.Forms.TextBox();
		this.TxtYTStp = new System.Windows.Forms.TextBox();
		this.TxtXTStp = new System.Windows.Forms.TextBox();
		this.TxtY2LStp = new System.Windows.Forms.TextBox();
		this.TxtYLStp = new System.Windows.Forms.TextBox();
		this.TxtXLStp = new System.Windows.Forms.TextBox();
		this.lblFmt = new System.Windows.Forms.Label();
		this.cmbXFmt = new System.Windows.Forms.ComboBox();
		this.cmbXDT = new System.Windows.Forms.ComboBox();
		this.chkLogY2 = new System.Windows.Forms.CheckBox();
		this.cmbY2Rotate = new System.Windows.Forms.ComboBox();
		this.TxtXIncr = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.LblXAxis = new System.Windows.Forms.Label();
		this.LblYAxis = new System.Windows.Forms.Label();
		this.GrpTitle = new System.Windows.Forms.GroupBox();
		this.Label10 = new System.Windows.Forms.Label();
		this.Label11 = new System.Windows.Forms.Label();
		this.Label9 = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.TxtPadB = new System.Windows.Forms.TextBox();
		this.TxtPadR = new System.Windows.Forms.TextBox();
		this.TxtPadT = new System.Windows.Forms.TextBox();
		this.TxtPadL = new System.Windows.Forms.TextBox();
		this.TxtY2Axis = new System.Windows.Forms.TextBox();
		this.TxtYAxis = new System.Windows.Forms.TextBox();
		this.TxtXAxis = new System.Windows.Forms.TextBox();
		this.TxtTitle = new System.Windows.Forms.TextBox();
		this.LblY2 = new System.Windows.Forms.Label();
		this.LblY = new System.Windows.Forms.Label();
		this.LblX = new System.Windows.Forms.Label();
		this.LblTitle = new System.Windows.Forms.Label();
		this.ContextGridY = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnucolorset = new System.Windows.Forms.ToolStripMenuItem();
		this.mnucolorclear = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuexpr = new System.Windows.Forms.ToolStripMenuItem();
		this.TabCols = new System.Windows.Forms.TabPage();
		this.Label4 = new System.Windows.Forms.Label();
		this.cmbperRow = new System.Windows.Forms.ComboBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.GridWhere = new System.Windows.Forms.DataGridView();
		this.ColAnd = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColParenO = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColGrid = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColOprGrid = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColValGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColParenC = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.cmdBy = new System.Windows.Forms.Button();
		this.cmdGroup = new System.Windows.Forms.Button();
		this.LstBy = new System.Windows.Forms.ListBox();
		this.ContextXGroupBy = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
		this.LstGroup = new System.Windows.Forms.ListBox();
		this.TxtIn = new System.Windows.Forms.TextBox();
		this.GridY = new System.Windows.Forms.DataGridView();
		this.LstX = new System.Windows.Forms.ListBox();
		this.LblCols = new System.Windows.Forms.Label();
		this.LstColumns = new System.Windows.Forms.ListBox();
		this.ContextCols = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuX = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGroup = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBy = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuY = new System.Windows.Forms.ToolStripMenuItem();
		this.LblInFile = new System.Windows.Forms.Label();
		this.TabChart = new System.Windows.Forms.TabControl();
		this.TextBox27 = new System.Windows.Forms.TextBox();
		this.ComboBox24 = new System.Windows.Forms.ComboBox();
		this.ComboBox25 = new System.Windows.Forms.ComboBox();
		this.TextBox28 = new System.Windows.Forms.TextBox();
		this.Label36 = new System.Windows.Forms.Label();
		this.TextBox29 = new System.Windows.Forms.TextBox();
		this.ComboBox26 = new System.Windows.Forms.ComboBox();
		this.ComboBox27 = new System.Windows.Forms.ComboBox();
		this.TextBox30 = new System.Windows.Forms.TextBox();
		this.Label37 = new System.Windows.Forms.Label();
		this.TextBox31 = new System.Windows.Forms.TextBox();
		this.ComboBox28 = new System.Windows.Forms.ComboBox();
		this.ComboBox29 = new System.Windows.Forms.ComboBox();
		this.TextBox32 = new System.Windows.Forms.TextBox();
		this.Label38 = new System.Windows.Forms.Label();
		this.Label39 = new System.Windows.Forms.Label();
		this.TextBox33 = new System.Windows.Forms.TextBox();
		this.ComboBox30 = new System.Windows.Forms.ComboBox();
		this.ComboBox31 = new System.Windows.Forms.ComboBox();
		this.TextBox34 = new System.Windows.Forms.TextBox();
		this.Label40 = new System.Windows.Forms.Label();
		this.ComboBox32 = new System.Windows.Forms.ComboBox();
		this.Label41 = new System.Windows.Forms.Label();
		this.CheckBox1 = new System.Windows.Forms.CheckBox();
		this.Label42 = new System.Windows.Forms.Label();
		this.CheckBox2 = new System.Windows.Forms.CheckBox();
		this.CheckBox3 = new System.Windows.Forms.CheckBox();
		this.CheckBox4 = new System.Windows.Forms.CheckBox();
		this.CheckBox5 = new System.Windows.Forms.CheckBox();
		this.Label43 = new System.Windows.Forms.Label();
		this.CheckBox7 = new System.Windows.Forms.CheckBox();
		this.CheckBox8 = new System.Windows.Forms.CheckBox();
		this.CheckBox9 = new System.Windows.Forms.CheckBox();
		this.CheckBox15 = new System.Windows.Forms.CheckBox();
		this.Label44 = new System.Windows.Forms.Label();
		this.Label45 = new System.Windows.Forms.Label();
		this.Label46 = new System.Windows.Forms.Label();
		this.TextBox35 = new System.Windows.Forms.TextBox();
		this.TextBox36 = new System.Windows.Forms.TextBox();
		this.TextBox37 = new System.Windows.Forms.TextBox();
		this.TextBox38 = new System.Windows.Forms.TextBox();
		this.Label47 = new System.Windows.Forms.Label();
		this.Label48 = new System.Windows.Forms.Label();
		this.Label49 = new System.Windows.Forms.Label();
		this.Label50 = new System.Windows.Forms.Label();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnureportsource = new System.Windows.Forms.ToolStripMenuItem();
		this.mnufilterchart = new System.Windows.Forms.ToolStripMenuItem();
		this.cmbFilterChart = new System.Windows.Forms.ToolStripComboBox();
		this.mnuRange = new System.Windows.Forms.ToolStripMenuItem();
		this.Columns = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColHdr = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColSumm = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAxis = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColLW = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colfillcolor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colsymtype = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colsymsize = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.coldash = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colopacity = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.collabels = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colconnect = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colsort = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colLEGFMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TabTitles.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		this.GrpTitle.SuspendLayout();
		this.ContextGridY.SuspendLayout();
		this.TabCols.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridWhere).BeginInit();
		this.ContextXGroupBy.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridY).BeginInit();
		this.ContextCols.SuspendLayout();
		this.TabChart.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdOK.ImageIndex = 3;
		this.cmdOK.Location = new System.Drawing.Point(653, 7);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(77, 49);
		this.cmdOK.TabIndex = 21;
		this.cmdOK.Text = "OK";
		this.cmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdOK, "Commit Changes");
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdCancel.ImageIndex = 0;
		this.cmdCancel.Location = new System.Drawing.Point(744, 7);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(77, 49);
		this.cmdCancel.TabIndex = 22;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdCancel, "Cancel changes");
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.CmdRemove.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdRemove.ImageIndex = 1;
		this.CmdRemove.Location = new System.Drawing.Point(716, 83);
		this.CmdRemove.Margin = new System.Windows.Forms.Padding(4);
		this.CmdRemove.Name = "CmdRemove";
		this.CmdRemove.Size = new System.Drawing.Size(83, 49);
		this.CmdRemove.TabIndex = 14;
		this.CmdRemove.Text = "Remove";
		this.CmdRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdRemove, "Remove the selected columns");
		this.CmdRemove.UseVisualStyleBackColor = true;
		this.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdClear.ImageIndex = 1;
		this.cmdClear.Location = new System.Drawing.Point(716, 143);
		this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(83, 49);
		this.cmdClear.TabIndex = 15;
		this.cmdClear.Text = "Clear";
		this.cmdClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear all column selections");
		this.cmdClear.UseVisualStyleBackColor = true;
		this.CmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp.ImageIndex = 5;
		this.CmdUp.Location = new System.Drawing.Point(721, 324);
		this.CmdUp.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp.Name = "CmdUp";
		this.CmdUp.Size = new System.Drawing.Size(77, 32);
		this.CmdUp.TabIndex = 17;
		this.ToolTip1.SetToolTip(this.CmdUp, "Move Up");
		this.CmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown.ImageIndex = 6;
		this.cmdDown.Location = new System.Drawing.Point(721, 358);
		this.cmdDown.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(77, 32);
		this.cmdDown.TabIndex = 18;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDelete.ImageIndex = 7;
		this.cmdDelete.Location = new System.Drawing.Point(721, 438);
		this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.Size = new System.Drawing.Size(77, 32);
		this.cmdDelete.TabIndex = 20;
		this.ToolTip1.SetToolTip(this.cmdDelete, "Delete Row");
		this.cmdDelete.UseVisualStyleBackColor = true;
		this.CmdBrowseIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowseIn.ImageIndex = 8;
		this.CmdBrowseIn.Location = new System.Drawing.Point(713, 5);
		this.CmdBrowseIn.Margin = new System.Windows.Forms.Padding(4);
		this.CmdBrowseIn.Name = "CmdBrowseIn";
		this.CmdBrowseIn.Size = new System.Drawing.Size(41, 33);
		this.CmdBrowseIn.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdBrowseIn, "Locate CSV File");
		this.CmdBrowseIn.UseVisualStyleBackColor = true;
		this.cmdDelete1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDelete1.ImageIndex = 7;
		this.cmdDelete1.Location = new System.Drawing.Point(721, 583);
		this.cmdDelete1.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDelete1.Name = "cmdDelete1";
		this.cmdDelete1.Size = new System.Drawing.Size(77, 32);
		this.cmdDelete1.TabIndex = 24;
		this.ToolTip1.SetToolTip(this.cmdDelete1, "Delete Row");
		this.cmdDelete1.UseVisualStyleBackColor = true;
		this.cmdDown1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown1.ImageIndex = 6;
		this.cmdDown1.Location = new System.Drawing.Point(721, 537);
		this.cmdDown1.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDown1.Name = "cmdDown1";
		this.cmdDown1.Size = new System.Drawing.Size(77, 32);
		this.cmdDown1.TabIndex = 23;
		this.ToolTip1.SetToolTip(this.cmdDown1, "Move Down");
		this.cmdDown1.UseVisualStyleBackColor = true;
		this.cmdUp1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp1.ImageIndex = 5;
		this.cmdUp1.Location = new System.Drawing.Point(721, 503);
		this.cmdUp1.Margin = new System.Windows.Forms.Padding(4);
		this.cmdUp1.Name = "cmdUp1";
		this.cmdUp1.Size = new System.Drawing.Size(77, 32);
		this.cmdUp1.TabIndex = 22;
		this.ToolTip1.SetToolTip(this.cmdUp1, "Move Up");
		this.cmdUp1.UseVisualStyleBackColor = true;
		this.cmdRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdRefresh.Location = new System.Drawing.Point(760, 5);
		this.cmdRefresh.Name = "cmdRefresh";
		this.cmdRefresh.Size = new System.Drawing.Size(41, 33);
		this.cmdRefresh.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.cmdRefresh, "Refresh Data");
		this.cmdRefresh.UseVisualStyleBackColor = true;
		this.chkHTTP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chkHTTP.AutoSize = true;
		this.chkHTTP.Location = new System.Drawing.Point(642, 8);
		this.chkHTTP.Name = "chkHTTP";
		this.chkHTTP.Size = new System.Drawing.Size(59, 21);
		this.chkHTTP.TabIndex = 2;
		this.chkHTTP.Text = "Web";
		this.ToolTip1.SetToolTip(this.chkHTTP, "Get data from web");
		this.chkHTTP.UseVisualStyleBackColor = true;
		this.cmdAddR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdAddR.Location = new System.Drawing.Point(721, 392);
		this.cmdAddR.Name = "cmdAddR";
		this.cmdAddR.Size = new System.Drawing.Size(75, 32);
		this.cmdAddR.TabIndex = 19;
		this.cmdAddR.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdAddR, "Add Row");
		this.cmdAddR.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.Location = new System.Drawing.Point(721, 33);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(35, 17);
		this.Label1.TabIndex = 10;
		this.Label1.Text = "Grid";
		this.ToolTip1.SetToolTip(this.Label1, "Add Grid Lines");
		this.LblInc.AutoSize = true;
		this.LblInc.Location = new System.Drawing.Point(228, 31);
		this.LblInc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInc.Name = "LblInc";
		this.LblInc.Size = new System.Drawing.Size(26, 17);
		this.LblInc.TabIndex = 91;
		this.LblInc.Text = "Inc";
		this.ToolTip1.SetToolTip(this.LblInc, "Enter Axis Increment");
		this.LblRotate.AutoSize = true;
		this.LblRotate.Location = new System.Drawing.Point(445, 31);
		this.LblRotate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRotate.Name = "LblRotate";
		this.LblRotate.Size = new System.Drawing.Size(50, 17);
		this.LblRotate.TabIndex = 89;
		this.LblRotate.Text = "Rotate";
		this.ToolTip1.SetToolTip(this.LblRotate, "Rotate labels (or tic-marks)");
		this.LblLog.AutoSize = true;
		this.LblLog.Location = new System.Drawing.Point(756, 33);
		this.LblLog.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLog.Name = "LblLog";
		this.LblLog.Size = new System.Drawing.Size(32, 17);
		this.LblLog.TabIndex = 88;
		this.LblLog.Text = "Log";
		this.ToolTip1.SetToolTip(this.LblLog, "Use Log Scale for Axis");
		this.LblLow.AutoSize = true;
		this.LblLow.Location = new System.Drawing.Point(99, 31);
		this.LblLow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLow.Name = "LblLow";
		this.LblLow.Size = new System.Drawing.Size(33, 17);
		this.LblLow.TabIndex = 79;
		this.LblLow.Text = "Low";
		this.ToolTip1.SetToolTip(this.LblLow, "Enter lower Axis Range");
		this.LblHigh.AutoSize = true;
		this.LblHigh.Location = new System.Drawing.Point(162, 31);
		this.LblHigh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHigh.Name = "LblHigh";
		this.LblHigh.Size = new System.Drawing.Size(37, 17);
		this.LblHigh.TabIndex = 80;
		this.LblHigh.Text = "High";
		this.ToolTip1.SetToolTip(this.LblHigh, "Enter higher Axis Range");
		this.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(603, 55);
		this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(62, 17);
		this.Label5.TabIndex = 8;
		this.Label5.Text = "Frame-Y";
		this.ToolTip1.SetToolTip(this.Label5, "Set vertical width of plot");
		this.LblXSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblXSize.AutoSize = true;
		this.LblXSize.Location = new System.Drawing.Point(603, 21);
		this.LblXSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXSize.Name = "LblXSize";
		this.LblXSize.Size = new System.Drawing.Size(66, 17);
		this.LblXSize.TabIndex = 7;
		this.LblXSize.Text = "Frame-X:";
		this.ToolTip1.SetToolTip(this.LblXSize, "Set horizontal width of plot");
		this.lblxType.AutoSize = true;
		this.lblxType.Location = new System.Drawing.Point(542, 33);
		this.lblxType.Name = "lblxType";
		this.lblxType.Size = new System.Drawing.Size(69, 17);
		this.lblxType.TabIndex = 99;
		this.lblxType.Text = "Axis Type";
		this.ToolTip1.SetToolTip(this.lblxType, "Displays as date, sequentially (basic) or linearly by X value order");
		this.lblpad.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblpad.Location = new System.Drawing.Point(605, 93);
		this.lblpad.Name = "lblpad";
		this.lblpad.Size = new System.Drawing.Size(55, 40);
		this.lblpad.TabIndex = 15;
		this.lblpad.Text = "Chart Pad";
		this.ToolTip1.SetToolTip(this.lblpad, "May need to set L to 50+ if X Axis is rotaed or horizontal and long");
		this.Label15.Location = new System.Drawing.Point(507, 14);
		this.Label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label15.Name = "Label15";
		this.Label15.Size = new System.Drawing.Size(32, 38);
		this.Label15.TabIndex = 116;
		this.Label15.Text = "On TIC";
		this.ToolTip1.SetToolTip(this.Label15, "Labels on TIC");
		this.cmdBackCOlor.Location = new System.Drawing.Point(242, 25);
		this.cmdBackCOlor.Name = "cmdBackCOlor";
		this.cmdBackCOlor.Size = new System.Drawing.Size(30, 22);
		this.cmdBackCOlor.TabIndex = 44;
		this.cmdBackCOlor.Text = "...";
		this.ToolTip1.SetToolTip(this.cmdBackCOlor, "Set Chart Background Color");
		this.cmdBackCOlor.UseVisualStyleBackColor = true;
		this.CmbXRotate.FormattingEnabled = true;
		this.CmbXRotate.Items.AddRange(new object[5] { "", "0", "-30", "-45", "-90" });
		this.CmbXRotate.Location = new System.Drawing.Point(440, 57);
		this.CmbXRotate.Margin = new System.Windows.Forms.Padding(4);
		this.CmbXRotate.Name = "CmbXRotate";
		this.CmbXRotate.Size = new System.Drawing.Size(62, 24);
		this.CmbXRotate.TabIndex = 16;
		this.ChkLogY.AutoSize = true;
		this.ChkLogY.Location = new System.Drawing.Point(763, 97);
		this.ChkLogY.Margin = new System.Windows.Forms.Padding(4);
		this.ChkLogY.Name = "ChkLogY";
		this.ChkLogY.Size = new System.Drawing.Size(18, 17);
		this.ChkLogY.TabIndex = 32;
		this.ChkLogY.UseVisualStyleBackColor = true;
		this.ChkLogX.AutoSize = true;
		this.ChkLogX.Location = new System.Drawing.Point(763, 59);
		this.ChkLogX.Margin = new System.Windows.Forms.Padding(4);
		this.ChkLogX.Name = "ChkLogX";
		this.ChkLogX.Size = new System.Drawing.Size(18, 17);
		this.ChkLogX.TabIndex = 21;
		this.ChkLogX.UseVisualStyleBackColor = true;
		this.TxtXLow.Location = new System.Drawing.Point(88, 57);
		this.TxtXLow.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXLow.Name = "TxtXLow";
		this.TxtXLow.Size = new System.Drawing.Size(57, 22);
		this.TxtXLow.TabIndex = 10;
		this.TxtXLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtY2High.Location = new System.Drawing.Point(153, 135);
		this.TxtY2High.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2High.Name = "TxtY2High";
		this.TxtY2High.Size = new System.Drawing.Size(57, 22);
		this.TxtY2High.TabIndex = 34;
		this.TxtY2High.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtXHigh.Location = new System.Drawing.Point(153, 57);
		this.TxtXHigh.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXHigh.Name = "TxtXHigh";
		this.TxtXHigh.Size = new System.Drawing.Size(57, 22);
		this.TxtXHigh.TabIndex = 11;
		this.TxtXHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtY2Low.Location = new System.Drawing.Point(88, 135);
		this.TxtY2Low.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Low.Name = "TxtY2Low";
		this.TxtY2Low.Size = new System.Drawing.Size(57, 22);
		this.TxtY2Low.TabIndex = 33;
		this.TxtY2Low.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtYHigh.Location = new System.Drawing.Point(153, 94);
		this.TxtYHigh.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYHigh.Name = "TxtYHigh";
		this.TxtYHigh.Size = new System.Drawing.Size(57, 22);
		this.TxtYHigh.TabIndex = 23;
		this.TxtYHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtYLow.Location = new System.Drawing.Point(88, 94);
		this.TxtYLow.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYLow.Name = "TxtYLow";
		this.TxtYLow.Size = new System.Drawing.Size(57, 22);
		this.TxtYLow.TabIndex = 22;
		this.TxtYLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ChkGridY.AutoSize = true;
		this.ChkGridY.Location = new System.Drawing.Point(735, 97);
		this.ChkGridY.Margin = new System.Windows.Forms.Padding(4);
		this.ChkGridY.Name = "ChkGridY";
		this.ChkGridY.Size = new System.Drawing.Size(18, 17);
		this.ChkGridY.TabIndex = 31;
		this.ChkGridY.UseVisualStyleBackColor = true;
		this.ChkGridX.AutoSize = true;
		this.ChkGridX.Location = new System.Drawing.Point(735, 60);
		this.ChkGridX.Margin = new System.Windows.Forms.Padding(4);
		this.ChkGridX.Name = "ChkGridX";
		this.ChkGridX.Size = new System.Drawing.Size(18, 17);
		this.ChkGridX.TabIndex = 20;
		this.ChkGridX.UseVisualStyleBackColor = true;
		this.CmbFrameY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmbFrameY.FormattingEnabled = true;
		this.CmbFrameY.Items.AddRange(new object[3] { "", "400", "800" });
		this.CmbFrameY.Location = new System.Drawing.Point(683, 52);
		this.CmbFrameY.Margin = new System.Windows.Forms.Padding(4);
		this.CmbFrameY.Name = "CmbFrameY";
		this.CmbFrameY.Size = new System.Drawing.Size(101, 24);
		this.CmbFrameY.TabIndex = 5;
		this.CmbFrameY.Text = "500";
		this.CmbFrameX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmbFrameX.FormattingEnabled = true;
		this.CmbFrameX.Items.AddRange(new object[3] { "", "850", "1000" });
		this.CmbFrameX.Location = new System.Drawing.Point(683, 18);
		this.CmbFrameX.Margin = new System.Windows.Forms.Padding(4);
		this.CmbFrameX.Name = "CmbFrameX";
		this.CmbFrameX.Size = new System.Drawing.Size(101, 24);
		this.CmbFrameX.TabIndex = 4;
		this.CmbFrameX.Text = "500";
		this.TxtYIncr.Location = new System.Drawing.Point(218, 94);
		this.TxtYIncr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYIncr.Name = "TxtYIncr";
		this.TxtYIncr.Size = new System.Drawing.Size(47, 22);
		this.TxtYIncr.TabIndex = 24;
		this.TxtY2Incr.Location = new System.Drawing.Point(218, 135);
		this.TxtY2Incr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Incr.Name = "TxtY2Incr";
		this.TxtY2Incr.Size = new System.Drawing.Size(47, 22);
		this.TxtY2Incr.TabIndex = 35;
		this.CmbYRotate.FormattingEnabled = true;
		this.CmbYRotate.Items.AddRange(new object[5] { "", "0", "-30", "-45", "-90" });
		this.CmbYRotate.Location = new System.Drawing.Point(439, 95);
		this.CmbYRotate.Margin = new System.Windows.Forms.Padding(4);
		this.CmbYRotate.Name = "CmbYRotate";
		this.CmbYRotate.Size = new System.Drawing.Size(62, 24);
		this.CmbYRotate.TabIndex = 28;
		this.CmdY.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdY.ImageIndex = 6;
		this.CmdY.Location = new System.Drawing.Point(269, 263);
		this.CmdY.Margin = new System.Windows.Forms.Padding(4);
		this.CmdY.Name = "CmdY";
		this.CmdY.Size = new System.Drawing.Size(101, 49);
		this.CmdY.TabIndex = 10;
		this.CmdY.Text = "Y";
		this.CmdY.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdY.UseVisualStyleBackColor = true;
		this.CmdX.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdX.Location = new System.Drawing.Point(269, 83);
		this.CmdX.Margin = new System.Windows.Forms.Padding(4);
		this.CmdX.Name = "CmdX";
		this.CmdX.Size = new System.Drawing.Size(101, 49);
		this.CmdX.TabIndex = 7;
		this.CmdX.Text = "X";
		this.CmdX.UseVisualStyleBackColor = true;
		this.TabOptions.Location = new System.Drawing.Point(4, 25);
		this.TabOptions.Margin = new System.Windows.Forms.Padding(4);
		this.TabOptions.Name = "TabOptions";
		this.TabOptions.Size = new System.Drawing.Size(813, 627);
		this.TabOptions.TabIndex = 2;
		this.TabOptions.Text = "Options";
		this.TabOptions.UseVisualStyleBackColor = true;
		this.Label8.Location = new System.Drawing.Point(0, 0);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(100, 23);
		this.Label8.TabIndex = 0;
		this.Label7.Location = new System.Drawing.Point(0, 0);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(100, 23);
		this.Label7.TabIndex = 0;
		this.Label31.Location = new System.Drawing.Point(0, 0);
		this.Label31.Name = "Label31";
		this.Label31.Size = new System.Drawing.Size(100, 23);
		this.Label31.TabIndex = 0;
		this.Label30.Location = new System.Drawing.Point(0, 0);
		this.Label30.Name = "Label30";
		this.Label30.Size = new System.Drawing.Size(100, 23);
		this.Label30.TabIndex = 0;
		this.TabTitles.Controls.Add(this.GroupBox2);
		this.TabTitles.Controls.Add(this.GroupBox1);
		this.TabTitles.Controls.Add(this.GrpTitle);
		this.TabTitles.Location = new System.Drawing.Point(4, 25);
		this.TabTitles.Margin = new System.Windows.Forms.Padding(4);
		this.TabTitles.Name = "TabTitles";
		this.TabTitles.Padding = new System.Windows.Forms.Padding(4);
		this.TabTitles.Size = new System.Drawing.Size(813, 627);
		this.TabTitles.TabIndex = 1;
		this.TabTitles.Text = "Titles/Axes";
		this.TabTitles.UseVisualStyleBackColor = true;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.chklegend);
		this.GroupBox2.Controls.Add(this.cmdBackCOlor);
		this.GroupBox2.Controls.Add(this.TxtBackColor);
		this.GroupBox2.Controls.Add(this.lblbackcolor);
		this.GroupBox2.Location = new System.Drawing.Point(11, 352);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(794, 100);
		this.GroupBox2.TabIndex = 90;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Other";
		this.chklegend.AutoSize = true;
		this.chklegend.Location = new System.Drawing.Point(12, 56);
		this.chklegend.Name = "chklegend";
		this.chklegend.Size = new System.Drawing.Size(111, 21);
		this.chklegend.TabIndex = 46;
		this.chklegend.Text = "Hide Legend";
		this.chklegend.UseVisualStyleBackColor = true;
		this.TxtBackColor.Location = new System.Drawing.Point(140, 25);
		this.TxtBackColor.Name = "TxtBackColor";
		this.TxtBackColor.Size = new System.Drawing.Size(100, 22);
		this.TxtBackColor.TabIndex = 43;
		this.lblbackcolor.AutoSize = true;
		this.lblbackcolor.Location = new System.Drawing.Point(12, 25);
		this.lblbackcolor.Name = "lblbackcolor";
		this.lblbackcolor.Size = new System.Drawing.Size(125, 17);
		this.lblbackcolor.TabIndex = 0;
		this.lblbackcolor.Text = "Background Color:";
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.cmbY2Fmt);
		this.GroupBox1.Controls.Add(this.cmbYFmt);
		this.GroupBox1.Controls.Add(this.ChkonTicY2);
		this.GroupBox1.Controls.Add(this.Label15);
		this.GroupBox1.Controls.Add(this.ChkonTicY);
		this.GroupBox1.Controls.Add(this.ChkonTicX);
		this.GroupBox1.Controls.Add(this.Label14);
		this.GroupBox1.Controls.Add(this.Label13);
		this.GroupBox1.Controls.Add(this.Label12);
		this.GroupBox1.Controls.Add(this.TxtY2GStp);
		this.GroupBox1.Controls.Add(this.TxtYGStp);
		this.GroupBox1.Controls.Add(this.TxtXGStp);
		this.GroupBox1.Controls.Add(this.TxtY2TStp);
		this.GroupBox1.Controls.Add(this.TxtYTStp);
		this.GroupBox1.Controls.Add(this.TxtXTStp);
		this.GroupBox1.Controls.Add(this.TxtY2LStp);
		this.GroupBox1.Controls.Add(this.TxtYLStp);
		this.GroupBox1.Controls.Add(this.TxtXLStp);
		this.GroupBox1.Controls.Add(this.lblFmt);
		this.GroupBox1.Controls.Add(this.cmbXFmt);
		this.GroupBox1.Controls.Add(this.cmbXDT);
		this.GroupBox1.Controls.Add(this.lblxType);
		this.GroupBox1.Controls.Add(this.chkLogY2);
		this.GroupBox1.Controls.Add(this.cmbY2Rotate);
		this.GroupBox1.Controls.Add(this.ChkGridY);
		this.GroupBox1.Controls.Add(this.CmbYRotate);
		this.GroupBox1.Controls.Add(this.ChkGridX);
		this.GroupBox1.Controls.Add(this.TxtY2Incr);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.TxtYIncr);
		this.GroupBox1.Controls.Add(this.TxtXIncr);
		this.GroupBox1.Controls.Add(this.LblInc);
		this.GroupBox1.Controls.Add(this.CmbXRotate);
		this.GroupBox1.Controls.Add(this.LblRotate);
		this.GroupBox1.Controls.Add(this.LblLog);
		this.GroupBox1.Controls.Add(this.ChkLogY);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.ChkLogX);
		this.GroupBox1.Controls.Add(this.LblXAxis);
		this.GroupBox1.Controls.Add(this.LblYAxis);
		this.GroupBox1.Controls.Add(this.TxtXLow);
		this.GroupBox1.Controls.Add(this.TxtY2High);
		this.GroupBox1.Controls.Add(this.TxtXHigh);
		this.GroupBox1.Controls.Add(this.TxtY2Low);
		this.GroupBox1.Controls.Add(this.TxtYHigh);
		this.GroupBox1.Controls.Add(this.LblLow);
		this.GroupBox1.Controls.Add(this.LblHigh);
		this.GroupBox1.Controls.Add(this.TxtYLow);
		this.GroupBox1.Location = new System.Drawing.Point(11, 166);
		this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Size = new System.Drawing.Size(794, 170);
		this.GroupBox1.TabIndex = 89;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Axes";
		this.cmbY2Fmt.DropDownWidth = 175;
		this.cmbY2Fmt.FormattingEnabled = true;
		this.cmbY2Fmt.Items.AddRange(new object[2] { "", "Decimals=6" });
		this.cmbY2Fmt.Location = new System.Drawing.Point(618, 132);
		this.cmbY2Fmt.Name = "cmbY2Fmt";
		this.cmbY2Fmt.Size = new System.Drawing.Size(106, 24);
		this.cmbY2Fmt.TabIndex = 41;
		this.cmbYFmt.DropDownWidth = 175;
		this.cmbYFmt.FormattingEnabled = true;
		this.cmbYFmt.Items.AddRange(new object[2] { "", "Decimals=6" });
		this.cmbYFmt.Location = new System.Drawing.Point(618, 94);
		this.cmbYFmt.Name = "cmbYFmt";
		this.cmbYFmt.Size = new System.Drawing.Size(106, 24);
		this.cmbYFmt.TabIndex = 30;
		this.ChkonTicY2.AutoSize = true;
		this.ChkonTicY2.Location = new System.Drawing.Point(514, 135);
		this.ChkonTicY2.Name = "ChkonTicY2";
		this.ChkonTicY2.Size = new System.Drawing.Size(18, 17);
		this.ChkonTicY2.TabIndex = 40;
		this.ChkonTicY2.UseVisualStyleBackColor = true;
		this.ChkonTicY.AutoSize = true;
		this.ChkonTicY.Location = new System.Drawing.Point(514, 97);
		this.ChkonTicY.Margin = new System.Windows.Forms.Padding(4);
		this.ChkonTicY.Name = "ChkonTicY";
		this.ChkonTicY.Size = new System.Drawing.Size(18, 17);
		this.ChkonTicY.TabIndex = 29;
		this.ChkonTicY.UseVisualStyleBackColor = true;
		this.ChkonTicX.AutoSize = true;
		this.ChkonTicX.Location = new System.Drawing.Point(514, 60);
		this.ChkonTicX.Margin = new System.Windows.Forms.Padding(4);
		this.ChkonTicX.Name = "ChkonTicX";
		this.ChkonTicX.Size = new System.Drawing.Size(18, 17);
		this.ChkonTicX.TabIndex = 17;
		this.ChkonTicX.UseVisualStyleBackColor = true;
		this.Label14.Location = new System.Drawing.Point(387, 14);
		this.Label14.Name = "Label14";
		this.Label14.Size = new System.Drawing.Size(45, 38);
		this.Label14.TabIndex = 114;
		this.Label14.Text = "Grid Step";
		this.Label13.Location = new System.Drawing.Point(332, 14);
		this.Label13.Name = "Label13";
		this.Label13.Size = new System.Drawing.Size(40, 38);
		this.Label13.TabIndex = 113;
		this.Label13.Text = "TIC Step";
		this.Label12.Location = new System.Drawing.Point(274, 14);
		this.Label12.Name = "Label12";
		this.Label12.Size = new System.Drawing.Size(45, 38);
		this.Label12.TabIndex = 112;
		this.Label12.Text = "Label Step";
		this.TxtY2GStp.Location = new System.Drawing.Point(383, 135);
		this.TxtY2GStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2GStp.Name = "TxtY2GStp";
		this.TxtY2GStp.Size = new System.Drawing.Size(47, 22);
		this.TxtY2GStp.TabIndex = 38;
		this.TxtYGStp.Location = new System.Drawing.Point(383, 94);
		this.TxtYGStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYGStp.Name = "TxtYGStp";
		this.TxtYGStp.Size = new System.Drawing.Size(47, 22);
		this.TxtYGStp.TabIndex = 27;
		this.TxtXGStp.Location = new System.Drawing.Point(383, 57);
		this.TxtXGStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXGStp.Name = "TxtXGStp";
		this.TxtXGStp.Size = new System.Drawing.Size(47, 22);
		this.TxtXGStp.TabIndex = 15;
		this.TxtY2TStp.Location = new System.Drawing.Point(328, 135);
		this.TxtY2TStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2TStp.Name = "TxtY2TStp";
		this.TxtY2TStp.Size = new System.Drawing.Size(47, 22);
		this.TxtY2TStp.TabIndex = 37;
		this.TxtYTStp.Location = new System.Drawing.Point(328, 94);
		this.TxtYTStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYTStp.Name = "TxtYTStp";
		this.TxtYTStp.Size = new System.Drawing.Size(47, 22);
		this.TxtYTStp.TabIndex = 26;
		this.TxtXTStp.Location = new System.Drawing.Point(328, 57);
		this.TxtXTStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXTStp.Name = "TxtXTStp";
		this.TxtXTStp.Size = new System.Drawing.Size(47, 22);
		this.TxtXTStp.TabIndex = 14;
		this.TxtY2LStp.Location = new System.Drawing.Point(274, 135);
		this.TxtY2LStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2LStp.Name = "TxtY2LStp";
		this.TxtY2LStp.Size = new System.Drawing.Size(47, 22);
		this.TxtY2LStp.TabIndex = 36;
		this.TxtYLStp.Location = new System.Drawing.Point(274, 94);
		this.TxtYLStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYLStp.Name = "TxtYLStp";
		this.TxtYLStp.Size = new System.Drawing.Size(47, 22);
		this.TxtYLStp.TabIndex = 25;
		this.TxtXLStp.Location = new System.Drawing.Point(273, 57);
		this.TxtXLStp.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXLStp.Name = "TxtXLStp";
		this.TxtXLStp.Size = new System.Drawing.Size(47, 22);
		this.TxtXLStp.TabIndex = 13;
		this.lblFmt.AutoSize = true;
		this.lblFmt.Location = new System.Drawing.Point(623, 33);
		this.lblFmt.Name = "lblFmt";
		this.lblFmt.Size = new System.Drawing.Size(52, 17);
		this.lblFmt.TabIndex = 102;
		this.lblFmt.Text = "Format";
		this.cmbXFmt.DropDownWidth = 175;
		this.cmbXFmt.FormattingEnabled = true;
		this.cmbXFmt.Items.AddRange(new object[6] { "", "D:yyyy-MM-dd", "D:yyyy-MM-dd HH", "D:yyyy-MM-dd HH:mm:ss", "D:dd-MMM-yyyy", "D:MMM yyyy" });
		this.cmbXFmt.Location = new System.Drawing.Point(618, 57);
		this.cmbXFmt.Name = "cmbXFmt";
		this.cmbXFmt.Size = new System.Drawing.Size(106, 24);
		this.cmbXFmt.TabIndex = 19;
		this.cmbXDT.DropDownWidth = 150;
		this.cmbXDT.FormattingEnabled = true;
		this.cmbXDT.Items.AddRange(new object[10] { "", "basic", "linear", "date-year", "date-month", "date-day", "date-hour", "date-minute", "date-second", "date-millisecond" });
		this.cmbXDT.Location = new System.Drawing.Point(544, 57);
		this.cmbXDT.Name = "cmbXDT";
		this.cmbXDT.Size = new System.Drawing.Size(62, 24);
		this.cmbXDT.TabIndex = 18;
		this.chkLogY2.AutoSize = true;
		this.chkLogY2.Location = new System.Drawing.Point(763, 135);
		this.chkLogY2.Name = "chkLogY2";
		this.chkLogY2.Size = new System.Drawing.Size(18, 17);
		this.chkLogY2.TabIndex = 42;
		this.chkLogY2.UseVisualStyleBackColor = true;
		this.cmbY2Rotate.FormattingEnabled = true;
		this.cmbY2Rotate.Items.AddRange(new object[5] { "", "0", "-30", "-45", "-90" });
		this.cmbY2Rotate.Location = new System.Drawing.Point(439, 134);
		this.cmbY2Rotate.Name = "cmbY2Rotate";
		this.cmbY2Rotate.Size = new System.Drawing.Size(62, 24);
		this.cmbY2Rotate.TabIndex = 39;
		this.TxtXIncr.Location = new System.Drawing.Point(218, 57);
		this.TxtXIncr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXIncr.Name = "TxtXIncr";
		this.TxtXIncr.Size = new System.Drawing.Size(47, 22);
		this.TxtXIncr.TabIndex = 12;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(12, 137);
		this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(55, 17);
		this.Label3.TabIndex = 87;
		this.Label3.Text = "Y2-Axis";
		this.LblXAxis.AutoSize = true;
		this.LblXAxis.Location = new System.Drawing.Point(12, 57);
		this.LblXAxis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXAxis.Name = "LblXAxis";
		this.LblXAxis.Size = new System.Drawing.Size(47, 17);
		this.LblXAxis.TabIndex = 1;
		this.LblXAxis.Text = "X-Axis";
		this.LblYAxis.AutoSize = true;
		this.LblYAxis.Location = new System.Drawing.Point(12, 98);
		this.LblYAxis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYAxis.Name = "LblYAxis";
		this.LblYAxis.Size = new System.Drawing.Size(47, 17);
		this.LblYAxis.TabIndex = 0;
		this.LblYAxis.Text = "Y-Axis";
		this.GrpTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrpTitle.Controls.Add(this.Label10);
		this.GrpTitle.Controls.Add(this.Label11);
		this.GrpTitle.Controls.Add(this.Label9);
		this.GrpTitle.Controls.Add(this.Label6);
		this.GrpTitle.Controls.Add(this.lblpad);
		this.GrpTitle.Controls.Add(this.TxtPadB);
		this.GrpTitle.Controls.Add(this.TxtPadR);
		this.GrpTitle.Controls.Add(this.TxtPadT);
		this.GrpTitle.Controls.Add(this.TxtPadL);
		this.GrpTitle.Controls.Add(this.CmbFrameY);
		this.GrpTitle.Controls.Add(this.CmbFrameX);
		this.GrpTitle.Controls.Add(this.Label5);
		this.GrpTitle.Controls.Add(this.LblXSize);
		this.GrpTitle.Controls.Add(this.TxtY2Axis);
		this.GrpTitle.Controls.Add(this.TxtYAxis);
		this.GrpTitle.Controls.Add(this.TxtXAxis);
		this.GrpTitle.Controls.Add(this.TxtTitle);
		this.GrpTitle.Controls.Add(this.LblY2);
		this.GrpTitle.Controls.Add(this.LblY);
		this.GrpTitle.Controls.Add(this.LblX);
		this.GrpTitle.Controls.Add(this.LblTitle);
		this.GrpTitle.Location = new System.Drawing.Point(11, 9);
		this.GrpTitle.Margin = new System.Windows.Forms.Padding(4);
		this.GrpTitle.Name = "GrpTitle";
		this.GrpTitle.Padding = new System.Windows.Forms.Padding(4);
		this.GrpTitle.Size = new System.Drawing.Size(794, 149);
		this.GrpTitle.TabIndex = 76;
		this.GrpTitle.TabStop = false;
		this.GrpTitle.Text = "Titles / Size";
		this.Label10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label10.AutoSize = true;
		this.Label10.Location = new System.Drawing.Point(724, 123);
		this.Label10.Name = "Label10";
		this.Label10.Size = new System.Drawing.Size(21, 17);
		this.Label10.TabIndex = 19;
		this.Label10.Text = "B:";
		this.Label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label11.AutoSize = true;
		this.Label11.Location = new System.Drawing.Point(724, 92);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(21, 17);
		this.Label11.TabIndex = 18;
		this.Label11.Text = "T:";
		this.Label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(659, 123);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(22, 17);
		this.Label9.TabIndex = 17;
		this.Label9.Text = "R:";
		this.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(659, 92);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(20, 17);
		this.Label6.TabIndex = 16;
		this.Label6.Text = "L:";
		this.TxtPadB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPadB.Location = new System.Drawing.Point(748, 120);
		this.TxtPadB.Name = "TxtPadB";
		this.TxtPadB.Size = new System.Drawing.Size(36, 22);
		this.TxtPadB.TabIndex = 9;
		this.TxtPadB.Text = "0";
		this.TxtPadR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPadR.Location = new System.Drawing.Point(683, 120);
		this.TxtPadR.Name = "TxtPadR";
		this.TxtPadR.Size = new System.Drawing.Size(36, 22);
		this.TxtPadR.TabIndex = 7;
		this.TxtPadR.Text = "50";
		this.TxtPadT.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPadT.Location = new System.Drawing.Point(748, 89);
		this.TxtPadT.Name = "TxtPadT";
		this.TxtPadT.Size = new System.Drawing.Size(36, 22);
		this.TxtPadT.TabIndex = 8;
		this.TxtPadT.Text = "0";
		this.TxtPadL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPadL.Location = new System.Drawing.Point(683, 89);
		this.TxtPadL.Name = "TxtPadL";
		this.TxtPadL.Size = new System.Drawing.Size(36, 22);
		this.TxtPadL.TabIndex = 6;
		this.TxtPadL.Text = "50";
		this.TxtY2Axis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtY2Axis.Location = new System.Drawing.Point(88, 111);
		this.TxtY2Axis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Axis.Name = "TxtY2Axis";
		this.TxtY2Axis.Size = new System.Drawing.Size(478, 22);
		this.TxtY2Axis.TabIndex = 3;
		this.TxtYAxis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtYAxis.Location = new System.Drawing.Point(88, 81);
		this.TxtYAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYAxis.Name = "TxtYAxis";
		this.TxtYAxis.Size = new System.Drawing.Size(478, 22);
		this.TxtYAxis.TabIndex = 2;
		this.TxtXAxis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtXAxis.Location = new System.Drawing.Point(88, 52);
		this.TxtXAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXAxis.Name = "TxtXAxis";
		this.TxtXAxis.Size = new System.Drawing.Size(478, 22);
		this.TxtXAxis.TabIndex = 1;
		this.TxtTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTitle.Location = new System.Drawing.Point(88, 22);
		this.TxtTitle.Margin = new System.Windows.Forms.Padding(4);
		this.TxtTitle.Name = "TxtTitle";
		this.TxtTitle.Size = new System.Drawing.Size(478, 22);
		this.TxtTitle.TabIndex = 0;
		this.LblY2.AutoSize = true;
		this.LblY2.Location = new System.Drawing.Point(12, 116);
		this.LblY2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblY2.Name = "LblY2";
		this.LblY2.Size = new System.Drawing.Size(63, 17);
		this.LblY2.TabIndex = 6;
		this.LblY2.Text = "Y-2 Axis:";
		this.LblY.AutoSize = true;
		this.LblY.Location = new System.Drawing.Point(12, 86);
		this.LblY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblY.Name = "LblY";
		this.LblY.Size = new System.Drawing.Size(50, 17);
		this.LblY.TabIndex = 5;
		this.LblY.Text = "Y Axis:";
		this.LblX.AutoSize = true;
		this.LblX.Location = new System.Drawing.Point(12, 54);
		this.LblX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblX.Name = "LblX";
		this.LblX.Size = new System.Drawing.Size(58, 17);
		this.LblX.TabIndex = 4;
		this.LblX.Text = "X  Axis :";
		this.LblTitle.AutoSize = true;
		this.LblTitle.Location = new System.Drawing.Point(12, 26);
		this.LblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTitle.Name = "LblTitle";
		this.LblTitle.Size = new System.Drawing.Size(39, 17);
		this.LblTitle.TabIndex = 1;
		this.LblTitle.Text = "Title:";
		this.ContextGridY.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextGridY.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnucolorset, this.mnucolorclear, this.mnuexpr });
		this.ContextGridY.Name = "ContextMenuStrip1";
		this.ContextGridY.Size = new System.Drawing.Size(245, 76);
		this.mnucolorset.Name = "mnucolorset";
		this.mnucolorset.Size = new System.Drawing.Size(244, 24);
		this.mnucolorset.Text = "Set Color";
		this.mnucolorclear.Name = "mnucolorclear";
		this.mnucolorclear.Size = new System.Drawing.Size(244, 24);
		this.mnucolorclear.Text = "Clear Color";
		this.mnuexpr.Name = "mnuexpr";
		this.mnuexpr.Size = new System.Drawing.Size(244, 24);
		this.mnuexpr.Text = "Edit Summary Expression";
		this.TabCols.Controls.Add(this.cmdAddR);
		this.TabCols.Controls.Add(this.Label4);
		this.TabCols.Controls.Add(this.cmbperRow);
		this.TabCols.Controls.Add(this.cmdRefresh);
		this.TabCols.Controls.Add(this.Label2);
		this.TabCols.Controls.Add(this.chkHTTP);
		this.TabCols.Controls.Add(this.cmdDelete1);
		this.TabCols.Controls.Add(this.cmdDown1);
		this.TabCols.Controls.Add(this.cmdUp1);
		this.TabCols.Controls.Add(this.GridWhere);
		this.TabCols.Controls.Add(this.cmdBy);
		this.TabCols.Controls.Add(this.cmdGroup);
		this.TabCols.Controls.Add(this.LstBy);
		this.TabCols.Controls.Add(this.LstGroup);
		this.TabCols.Controls.Add(this.CmdBrowseIn);
		this.TabCols.Controls.Add(this.TxtIn);
		this.TabCols.Controls.Add(this.GridY);
		this.TabCols.Controls.Add(this.cmdDelete);
		this.TabCols.Controls.Add(this.cmdDown);
		this.TabCols.Controls.Add(this.CmdUp);
		this.TabCols.Controls.Add(this.cmdClear);
		this.TabCols.Controls.Add(this.CmdRemove);
		this.TabCols.Controls.Add(this.LstX);
		this.TabCols.Controls.Add(this.CmdX);
		this.TabCols.Controls.Add(this.CmdY);
		this.TabCols.Controls.Add(this.LblCols);
		this.TabCols.Controls.Add(this.LstColumns);
		this.TabCols.Controls.Add(this.LblInFile);
		this.TabCols.Location = new System.Drawing.Point(4, 25);
		this.TabCols.Margin = new System.Windows.Forms.Padding(4);
		this.TabCols.Name = "TabCols";
		this.TabCols.Padding = new System.Windows.Forms.Padding(4);
		this.TabCols.Size = new System.Drawing.Size(813, 627);
		this.TabCols.TabIndex = 0;
		this.TabCols.Text = "Columns";
		this.TabCols.UseVisualStyleBackColor = true;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(15, 36);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(99, 17);
		this.Label4.TabIndex = 61;
		this.Label4.Text = "Plots per Row:";
		this.cmbperRow.FormattingEnabled = true;
		this.cmbperRow.Items.AddRange(new object[10] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
		this.cmbperRow.Location = new System.Drawing.Point(138, 36);
		this.cmbperRow.Name = "cmbperRow";
		this.cmbperRow.Size = new System.Drawing.Size(134, 24);
		this.cmbperRow.TabIndex = 5;
		this.cmbperRow.Text = "1";
		this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(8, 482);
		this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(50, 17);
		this.Label2.TabIndex = 59;
		this.Label2.Text = "Where";
		this.GridWhere.AllowUserToAddRows = false;
		this.GridWhere.AllowUserToDeleteRows = false;
		this.GridWhere.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridWhere.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridWhere.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridWhere.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridWhere.Columns.AddRange(this.ColAnd, this.ColParenO, this.ColGrid, this.ColOprGrid, this.ColValGrid, this.ColParenC);
		this.GridWhere.Location = new System.Drawing.Point(8, 503);
		this.GridWhere.Margin = new System.Windows.Forms.Padding(4);
		this.GridWhere.Name = "GridWhere";
		this.GridWhere.RowHeadersWidth = 51;
		this.GridWhere.RowTemplate.Height = 24;
		this.GridWhere.Size = new System.Drawing.Size(687, 116);
		this.GridWhere.StandardTab = true;
		this.GridWhere.TabIndex = 21;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColAnd.DefaultCellStyle = dataGridViewCellStyle2;
		this.ColAnd.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAnd.DisplayStyleForCurrentCellOnly = true;
		this.ColAnd.DropDownWidth = 50;
		this.ColAnd.HeaderText = "And";
		this.ColAnd.Items.AddRange("", "And", "Or");
		this.ColAnd.MinimumWidth = 6;
		this.ColAnd.Name = "ColAnd";
		this.ColAnd.Width = 30;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColParenO.DefaultCellStyle = dataGridViewCellStyle3;
		this.ColParenO.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColParenO.DisplayStyleForCurrentCellOnly = true;
		this.ColParenO.DropDownWidth = 50;
		this.ColParenO.HeaderText = "(";
		this.ColParenO.Items.AddRange("", "(", "((", "(((");
		this.ColParenO.MinimumWidth = 6;
		this.ColParenO.Name = "ColParenO";
		this.ColParenO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColParenO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.ColParenO.Width = 30;
		this.ColGrid.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColGrid.DisplayStyleForCurrentCellOnly = true;
		this.ColGrid.DropDownWidth = 200;
		this.ColGrid.HeaderText = "Column";
		this.ColGrid.MinimumWidth = 6;
		this.ColGrid.Name = "ColGrid";
		this.ColGrid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColGrid.Width = 125;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColOprGrid.DefaultCellStyle = dataGridViewCellStyle4;
		this.ColOprGrid.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColOprGrid.DisplayStyleForCurrentCellOnly = true;
		this.ColOprGrid.DropDownWidth = 100;
		this.ColOprGrid.HeaderText = "";
		this.ColOprGrid.Items.AddRange("", "=", "!=", ">", "<", ">=", "<=", "in", "like");
		this.ColOprGrid.MinimumWidth = 6;
		this.ColOprGrid.Name = "ColOprGrid";
		this.ColOprGrid.Width = 60;
		this.ColValGrid.HeaderText = "Value";
		this.ColValGrid.MinimumWidth = 6;
		this.ColValGrid.Name = "ColValGrid";
		this.ColValGrid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColValGrid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColValGrid.Width = 125;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColParenC.DefaultCellStyle = dataGridViewCellStyle5;
		this.ColParenC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColParenC.DisplayStyleForCurrentCellOnly = true;
		this.ColParenC.DropDownWidth = 50;
		this.ColParenC.HeaderText = ")";
		this.ColParenC.Items.AddRange("", ")", "))", ")))");
		this.ColParenC.MinimumWidth = 6;
		this.ColParenC.Name = "ColParenC";
		this.ColParenC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColParenC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.ColParenC.Width = 30;
		this.cmdBy.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdBy.Location = new System.Drawing.Point(269, 203);
		this.cmdBy.Margin = new System.Windows.Forms.Padding(4);
		this.cmdBy.Name = "cmdBy";
		this.cmdBy.Size = new System.Drawing.Size(101, 49);
		this.cmdBy.TabIndex = 9;
		this.cmdBy.Text = "By";
		this.cmdBy.UseVisualStyleBackColor = true;
		this.cmdGroup.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdGroup.Location = new System.Drawing.Point(269, 143);
		this.cmdGroup.Margin = new System.Windows.Forms.Padding(4);
		this.cmdGroup.Name = "cmdGroup";
		this.cmdGroup.Size = new System.Drawing.Size(101, 49);
		this.cmdGroup.TabIndex = 8;
		this.cmdGroup.Text = "Group";
		this.cmdGroup.UseVisualStyleBackColor = true;
		this.LstBy.AllowDrop = true;
		this.LstBy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LstBy.ContextMenuStrip = this.ContextXGroupBy;
		this.LstBy.FormattingEnabled = true;
		this.LstBy.HorizontalScrollbar = true;
		this.LstBy.ItemHeight = 16;
		this.LstBy.Location = new System.Drawing.Point(383, 203);
		this.LstBy.Margin = new System.Windows.Forms.Padding(4);
		this.LstBy.Name = "LstBy";
		this.LstBy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstBy.Size = new System.Drawing.Size(315, 52);
		this.LstBy.TabIndex = 13;
		this.ContextXGroupBy.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextXGroupBy.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuRemove, this.mnuClear });
		this.ContextXGroupBy.Name = "ContextXGroupBy";
		this.ContextXGroupBy.Size = new System.Drawing.Size(133, 52);
		this.mnuRemove.Name = "mnuRemove";
		this.mnuRemove.Size = new System.Drawing.Size(132, 24);
		this.mnuRemove.Text = "Remove";
		this.mnuClear.Name = "mnuClear";
		this.mnuClear.Size = new System.Drawing.Size(132, 24);
		this.mnuClear.Text = "Clear";
		this.LstGroup.AllowDrop = true;
		this.LstGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LstGroup.ContextMenuStrip = this.ContextXGroupBy;
		this.LstGroup.FormattingEnabled = true;
		this.LstGroup.HorizontalScrollbar = true;
		this.LstGroup.ItemHeight = 16;
		this.LstGroup.Location = new System.Drawing.Point(383, 143);
		this.LstGroup.Margin = new System.Windows.Forms.Padding(4);
		this.LstGroup.Name = "LstGroup";
		this.LstGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstGroup.Size = new System.Drawing.Size(315, 52);
		this.LstGroup.TabIndex = 12;
		this.TxtIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtIn.Location = new System.Drawing.Point(138, 8);
		this.TxtIn.Margin = new System.Windows.Forms.Padding(4);
		this.TxtIn.Name = "TxtIn";
		this.TxtIn.Size = new System.Drawing.Size(490, 22);
		this.TxtIn.TabIndex = 1;
		this.GridY.AllowUserToAddRows = false;
		this.GridY.AllowUserToDeleteRows = false;
		this.GridY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridY.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridY.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.GridY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridY.Columns.AddRange(this.Columns, this.ColHdr, this.ColSumm, this.ColAxis, this.ColStyle, this.ColLW, this.colfillcolor, this.ColColor, this.colsymtype, this.colsymsize, this.coldash, this.colopacity, this.collabels, this.colconnect, this.colTT, this.colsort, this.colLEGFMT);
		this.GridY.Location = new System.Drawing.Point(8, 324);
		this.GridY.Margin = new System.Windows.Forms.Padding(4);
		this.GridY.Name = "GridY";
		this.GridY.RowHeadersWidth = 50;
		this.GridY.Size = new System.Drawing.Size(687, 149);
		this.GridY.StandardTab = true;
		this.GridY.TabIndex = 16;
		this.LstX.AllowDrop = true;
		this.LstX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LstX.ContextMenuStrip = this.ContextXGroupBy;
		this.LstX.FormattingEnabled = true;
		this.LstX.HorizontalScrollbar = true;
		this.LstX.ItemHeight = 16;
		this.LstX.Location = new System.Drawing.Point(383, 83);
		this.LstX.Margin = new System.Windows.Forms.Padding(4);
		this.LstX.Name = "LstX";
		this.LstX.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstX.Size = new System.Drawing.Size(315, 52);
		this.LstX.TabIndex = 11;
		this.LblCols.AutoSize = true;
		this.LblCols.Location = new System.Drawing.Point(8, 65);
		this.LblCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCols.Name = "LblCols";
		this.LblCols.Size = new System.Drawing.Size(62, 17);
		this.LblCols.TabIndex = 4;
		this.LblCols.Text = "Columns";
		this.LstColumns.ContextMenuStrip = this.ContextCols;
		this.LstColumns.FormattingEnabled = true;
		this.LstColumns.HorizontalScrollbar = true;
		this.LstColumns.ItemHeight = 16;
		this.LstColumns.Location = new System.Drawing.Point(8, 83);
		this.LstColumns.Margin = new System.Windows.Forms.Padding(4);
		this.LstColumns.Name = "LstColumns";
		this.LstColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstColumns.Size = new System.Drawing.Size(245, 228);
		this.LstColumns.TabIndex = 6;
		this.ContextCols.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextCols.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuX, this.mnuGroup, this.mnuBy, this.mnuY });
		this.ContextCols.Name = "ContextCols";
		this.ContextCols.Size = new System.Drawing.Size(120, 100);
		this.mnuX.Name = "mnuX";
		this.mnuX.Size = new System.Drawing.Size(119, 24);
		this.mnuX.Text = "X";
		this.mnuGroup.Name = "mnuGroup";
		this.mnuGroup.Size = new System.Drawing.Size(119, 24);
		this.mnuGroup.Text = "Group";
		this.mnuBy.Name = "mnuBy";
		this.mnuBy.Size = new System.Drawing.Size(119, 24);
		this.mnuBy.Text = "By";
		this.mnuY.Name = "mnuY";
		this.mnuY.Size = new System.Drawing.Size(119, 24);
		this.mnuY.Text = "Y";
		this.LblInFile.AutoSize = true;
		this.LblInFile.Location = new System.Drawing.Point(15, 8);
		this.LblInFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInFile.Name = "LblInFile";
		this.LblInFile.Size = new System.Drawing.Size(100, 17);
		this.LblInFile.TabIndex = 1;
		this.LblInFile.Text = "Input CSV File:";
		this.TabChart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabChart.Controls.Add(this.TabCols);
		this.TabChart.Controls.Add(this.TabTitles);
		this.TabChart.Controls.Add(this.TabOptions);
		this.TabChart.Location = new System.Drawing.Point(0, 49);
		this.TabChart.Margin = new System.Windows.Forms.Padding(4);
		this.TabChart.Name = "TabChart";
		this.TabChart.Padding = new System.Drawing.Point(30, 3);
		this.TabChart.SelectedIndex = 0;
		this.TabChart.Size = new System.Drawing.Size(821, 656);
		this.TabChart.TabIndex = 0;
		this.TextBox27.Location = new System.Drawing.Point(433, 313);
		this.TextBox27.Name = "TextBox27";
		this.TextBox27.Size = new System.Drawing.Size(76, 22);
		this.TextBox27.TabIndex = 43;
		this.ComboBox24.FormattingEnabled = true;
		this.ComboBox24.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.ComboBox24.Location = new System.Drawing.Point(366, 313);
		this.ComboBox24.Name = "ComboBox24";
		this.ComboBox24.Size = new System.Drawing.Size(63, 25);
		this.ComboBox24.Sorted = true;
		this.ComboBox24.TabIndex = 42;
		this.ComboBox24.Text = "Black";
		this.ComboBox25.FormattingEnabled = true;
		this.ComboBox25.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.ComboBox25.Location = new System.Drawing.Point(308, 313);
		this.ComboBox25.Name = "ComboBox25";
		this.ComboBox25.Size = new System.Drawing.Size(52, 25);
		this.ComboBox25.Sorted = true;
		this.ComboBox25.TabIndex = 41;
		this.ComboBox25.Text = "Solid";
		this.TextBox28.Location = new System.Drawing.Point(257, 313);
		this.TextBox28.Name = "TextBox28";
		this.TextBox28.Size = new System.Drawing.Size(45, 22);
		this.TextBox28.TabIndex = 40;
		this.Label36.AutoSize = true;
		this.Label36.Location = new System.Drawing.Point(196, 313);
		this.Label36.Name = "Label36";
		this.Label36.Size = new System.Drawing.Size(59, 13);
		this.Label36.TabIndex = 39;
		this.Label36.Text = "Ref Line 1:";
		this.TextBox29.Location = new System.Drawing.Point(433, 285);
		this.TextBox29.Name = "TextBox29";
		this.TextBox29.Size = new System.Drawing.Size(76, 22);
		this.TextBox29.TabIndex = 38;
		this.ComboBox26.FormattingEnabled = true;
		this.ComboBox26.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.ComboBox26.Location = new System.Drawing.Point(366, 285);
		this.ComboBox26.Name = "ComboBox26";
		this.ComboBox26.Size = new System.Drawing.Size(63, 25);
		this.ComboBox26.Sorted = true;
		this.ComboBox26.TabIndex = 37;
		this.ComboBox26.Text = "Black";
		this.ComboBox27.FormattingEnabled = true;
		this.ComboBox27.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.ComboBox27.Location = new System.Drawing.Point(308, 285);
		this.ComboBox27.Name = "ComboBox27";
		this.ComboBox27.Size = new System.Drawing.Size(52, 25);
		this.ComboBox27.Sorted = true;
		this.ComboBox27.TabIndex = 36;
		this.ComboBox27.Text = "Solid";
		this.TextBox30.Location = new System.Drawing.Point(257, 285);
		this.TextBox30.Name = "TextBox30";
		this.TextBox30.Size = new System.Drawing.Size(45, 22);
		this.TextBox30.TabIndex = 35;
		this.Label37.AutoSize = true;
		this.Label37.Location = new System.Drawing.Point(196, 285);
		this.Label37.Name = "Label37";
		this.Label37.Size = new System.Drawing.Size(59, 13);
		this.Label37.TabIndex = 34;
		this.Label37.Text = "Ref Line 1:";
		this.TextBox31.Location = new System.Drawing.Point(433, 254);
		this.TextBox31.Name = "TextBox31";
		this.TextBox31.Size = new System.Drawing.Size(76, 22);
		this.TextBox31.TabIndex = 33;
		this.ComboBox28.FormattingEnabled = true;
		this.ComboBox28.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.ComboBox28.Location = new System.Drawing.Point(366, 254);
		this.ComboBox28.Name = "ComboBox28";
		this.ComboBox28.Size = new System.Drawing.Size(63, 25);
		this.ComboBox28.Sorted = true;
		this.ComboBox28.TabIndex = 32;
		this.ComboBox28.Text = "Black";
		this.ComboBox29.FormattingEnabled = true;
		this.ComboBox29.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.ComboBox29.Location = new System.Drawing.Point(308, 254);
		this.ComboBox29.Name = "ComboBox29";
		this.ComboBox29.Size = new System.Drawing.Size(52, 25);
		this.ComboBox29.Sorted = true;
		this.ComboBox29.TabIndex = 31;
		this.ComboBox29.Text = "Solid";
		this.TextBox32.Location = new System.Drawing.Point(257, 254);
		this.TextBox32.Name = "TextBox32";
		this.TextBox32.Size = new System.Drawing.Size(45, 22);
		this.TextBox32.TabIndex = 30;
		this.Label38.AutoSize = true;
		this.Label38.Location = new System.Drawing.Point(196, 254);
		this.Label38.Name = "Label38";
		this.Label38.Size = new System.Drawing.Size(59, 13);
		this.Label38.TabIndex = 29;
		this.Label38.Text = "Ref Line 1:";
		this.Label39.AutoSize = true;
		this.Label39.Location = new System.Drawing.Point(433, 205);
		this.Label39.Name = "Label39";
		this.Label39.Size = new System.Drawing.Size(60, 13);
		this.Label39.TabIndex = 28;
		this.Label39.Text = "Description";
		this.TextBox33.Location = new System.Drawing.Point(432, 225);
		this.TextBox33.Name = "TextBox33";
		this.TextBox33.Size = new System.Drawing.Size(76, 22);
		this.TextBox33.TabIndex = 27;
		this.ComboBox30.FormattingEnabled = true;
		this.ComboBox30.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.ComboBox30.Location = new System.Drawing.Point(365, 225);
		this.ComboBox30.Name = "ComboBox30";
		this.ComboBox30.Size = new System.Drawing.Size(63, 25);
		this.ComboBox30.Sorted = true;
		this.ComboBox30.TabIndex = 26;
		this.ComboBox30.Text = "Black";
		this.ComboBox31.FormattingEnabled = true;
		this.ComboBox31.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.ComboBox31.Location = new System.Drawing.Point(307, 225);
		this.ComboBox31.Name = "ComboBox31";
		this.ComboBox31.Size = new System.Drawing.Size(52, 25);
		this.ComboBox31.Sorted = true;
		this.ComboBox31.TabIndex = 25;
		this.ComboBox31.Text = "Solid";
		this.TextBox34.Location = new System.Drawing.Point(256, 225);
		this.TextBox34.Name = "TextBox34";
		this.TextBox34.Size = new System.Drawing.Size(45, 22);
		this.TextBox34.TabIndex = 24;
		this.Label40.AutoSize = true;
		this.Label40.Location = new System.Drawing.Point(195, 225);
		this.Label40.Name = "Label40";
		this.Label40.Size = new System.Drawing.Size(59, 13);
		this.Label40.TabIndex = 23;
		this.Label40.Text = "Ref Line 1:";
		this.ComboBox32.FormattingEnabled = true;
		this.ComboBox32.Items.AddRange(new object[3] { "All", "None", "2" });
		this.ComboBox32.Location = new System.Drawing.Point(156, 483);
		this.ComboBox32.Name = "ComboBox32";
		this.ComboBox32.Size = new System.Drawing.Size(80, 25);
		this.ComboBox32.TabIndex = 13;
		this.ComboBox32.Text = "All";
		this.ComboBox32.Visible = false;
		this.Label41.AutoSize = true;
		this.Label41.Location = new System.Drawing.Point(24, 483);
		this.Label41.Name = "Label41";
		this.Label41.Size = new System.Drawing.Size(99, 13);
		this.Label41.TabIndex = 22;
		this.Label41.Text = "X-Axis Label Levels";
		this.Label41.Visible = false;
		this.CheckBox1.AutoSize = true;
		this.CheckBox1.Location = new System.Drawing.Point(22, 456);
		this.CheckBox1.Name = "CheckBox1";
		this.CheckBox1.Size = new System.Drawing.Size(113, 17);
		this.CheckBox1.TabIndex = 12;
		this.CheckBox1.Text = "Show Data Labels";
		this.CheckBox1.UseVisualStyleBackColor = true;
		this.CheckBox1.Visible = false;
		this.Label42.AutoSize = true;
		this.Label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Label42.Location = new System.Drawing.Point(3, 427);
		this.Label42.Name = "Label42";
		this.Label42.Size = new System.Drawing.Size(44, 13);
		this.Label42.TabIndex = 20;
		this.Label42.Text = "Labels";
		this.Label42.Visible = false;
		this.CheckBox2.AutoSize = true;
		this.CheckBox2.Location = new System.Drawing.Point(99, 358);
		this.CheckBox2.Name = "CheckBox2";
		this.CheckBox2.Size = new System.Drawing.Size(81, 17);
		this.CheckBox2.TabIndex = 11;
		this.CheckBox2.Text = "Minor Ticks";
		this.CheckBox2.UseVisualStyleBackColor = true;
		this.CheckBox3.AutoSize = true;
		this.CheckBox3.Location = new System.Drawing.Point(99, 335);
		this.CheckBox3.Name = "CheckBox3";
		this.CheckBox3.Size = new System.Drawing.Size(81, 17);
		this.CheckBox3.TabIndex = 10;
		this.CheckBox3.Text = "Major Ticks";
		this.CheckBox3.UseVisualStyleBackColor = true;
		this.CheckBox4.AutoSize = true;
		this.CheckBox4.Location = new System.Drawing.Point(99, 248);
		this.CheckBox4.Name = "CheckBox4";
		this.CheckBox4.Size = new System.Drawing.Size(81, 17);
		this.CheckBox4.TabIndex = 7;
		this.CheckBox4.Text = "Minor Ticks";
		this.CheckBox4.UseVisualStyleBackColor = true;
		this.CheckBox5.AutoSize = true;
		this.CheckBox5.Location = new System.Drawing.Point(19, 247);
		this.CheckBox5.Name = "CheckBox5";
		this.CheckBox5.Size = new System.Drawing.Size(81, 17);
		this.CheckBox5.TabIndex = 6;
		this.CheckBox5.Text = "Major Ticks";
		this.CheckBox5.UseVisualStyleBackColor = true;
		this.Label43.AutoSize = true;
		this.Label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Label43.Location = new System.Drawing.Point(8, 314);
		this.Label43.Name = "Label43";
		this.Label43.Size = new System.Drawing.Size(46, 13);
		this.Label43.TabIndex = 15;
		this.Label43.Text = "Y-Axis:";
		this.CheckBox7.AutoSize = true;
		this.CheckBox7.Location = new System.Drawing.Point(19, 358);
		this.CheckBox7.Name = "CheckBox7";
		this.CheckBox7.Size = new System.Drawing.Size(74, 17);
		this.CheckBox7.TabIndex = 9;
		this.CheckBox7.Text = "Minor Grid";
		this.CheckBox7.UseVisualStyleBackColor = true;
		this.CheckBox8.AutoSize = true;
		this.CheckBox8.Location = new System.Drawing.Point(19, 335);
		this.CheckBox8.Name = "CheckBox8";
		this.CheckBox8.Size = new System.Drawing.Size(74, 17);
		this.CheckBox8.TabIndex = 8;
		this.CheckBox8.Text = "Major Grid";
		this.CheckBox8.UseVisualStyleBackColor = true;
		this.CheckBox9.AutoSize = true;
		this.CheckBox9.Location = new System.Drawing.Point(99, 225);
		this.CheckBox9.Name = "CheckBox9";
		this.CheckBox9.Size = new System.Drawing.Size(74, 17);
		this.CheckBox9.TabIndex = 5;
		this.CheckBox9.Text = "Minor Grid";
		this.CheckBox9.UseVisualStyleBackColor = true;
		this.CheckBox15.AutoSize = true;
		this.CheckBox15.Location = new System.Drawing.Point(19, 224);
		this.CheckBox15.Name = "CheckBox15";
		this.CheckBox15.Size = new System.Drawing.Size(74, 17);
		this.CheckBox15.TabIndex = 4;
		this.CheckBox15.Text = "Major Grid";
		this.CheckBox15.UseVisualStyleBackColor = true;
		this.Label44.AutoSize = true;
		this.Label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Label44.Location = new System.Drawing.Point(8, 201);
		this.Label44.Name = "Label44";
		this.Label44.Size = new System.Drawing.Size(46, 13);
		this.Label44.TabIndex = 10;
		this.Label44.Text = "X-Axis:";
		this.Label45.AutoSize = true;
		this.Label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Label45.Location = new System.Drawing.Point(3, 171);
		this.Label45.Name = "Label45";
		this.Label45.Size = new System.Drawing.Size(247, 13);
		this.Label45.TabIndex = 9;
		this.Label45.Text = "Gridlines / Tickmarks / Reference Lines:  ";
		this.Label46.AutoSize = true;
		this.Label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Label46.Location = new System.Drawing.Point(3, 9);
		this.Label46.Name = "Label46";
		this.Label46.Size = new System.Drawing.Size(38, 13);
		this.Label46.TabIndex = 8;
		this.Label46.Text = "Titles";
		this.TextBox35.Location = new System.Drawing.Point(104, 113);
		this.TextBox35.Name = "TextBox35";
		this.TextBox35.Size = new System.Drawing.Size(394, 22);
		this.TextBox35.TabIndex = 3;
		this.TextBox36.Location = new System.Drawing.Point(104, 87);
		this.TextBox36.Name = "TextBox36";
		this.TextBox36.Size = new System.Drawing.Size(394, 22);
		this.TextBox36.TabIndex = 2;
		this.TextBox37.Location = new System.Drawing.Point(104, 61);
		this.TextBox37.Name = "TextBox37";
		this.TextBox37.Size = new System.Drawing.Size(394, 22);
		this.TextBox37.TabIndex = 1;
		this.TextBox38.Location = new System.Drawing.Point(104, 35);
		this.TextBox38.Name = "TextBox38";
		this.TextBox38.Size = new System.Drawing.Size(394, 22);
		this.TextBox38.TabIndex = 0;
		this.Label47.AutoSize = true;
		this.Label47.Location = new System.Drawing.Point(8, 116);
		this.Label47.Name = "Label47";
		this.Label47.Size = new System.Drawing.Size(79, 13);
		this.Label47.TabIndex = 6;
		this.Label47.Text = "Second Y Axis:";
		this.Label48.AutoSize = true;
		this.Label48.Location = new System.Drawing.Point(8, 90);
		this.Label48.Name = "Label48";
		this.Label48.Size = new System.Drawing.Size(39, 13);
		this.Label48.TabIndex = 5;
		this.Label48.Text = "Y Axis:";
		this.Label49.AutoSize = true;
		this.Label49.Location = new System.Drawing.Point(8, 64);
		this.Label49.Name = "Label49";
		this.Label49.Size = new System.Drawing.Size(93, 13);
		this.Label49.TabIndex = 4;
		this.Label49.Text = "X (Category) Axis :";
		this.Label50.AutoSize = true;
		this.Label50.Location = new System.Drawing.Point(8, 38);
		this.Label50.Name = "Label50";
		this.Label50.Size = new System.Drawing.Size(58, 13);
		this.Label50.TabIndex = 1;
		this.Label50.Text = "Chart Title:";
		this.MenuStrip1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.MenuStrip1.AutoSize = false;
		this.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.OptionsToolStripMenuItem });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 1);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Size = new System.Drawing.Size(632, 28);
		this.MenuStrip1.TabIndex = 13;
		this.MenuStrip1.Text = "MenuStrip1";
		this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnureportsource, this.mnufilterchart, this.mnuRange });
		this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
		this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
		this.OptionsToolStripMenuItem.Text = "Options";
		this.mnureportsource.CheckOnClick = true;
		this.mnureportsource.Name = "mnureportsource";
		this.mnureportsource.Size = new System.Drawing.Size(307, 26);
		this.mnureportsource.Text = "Use an Interactive Report Source";
		this.mnufilterchart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.cmbFilterChart });
		this.mnufilterchart.Name = "mnufilterchart";
		this.mnufilterchart.Size = new System.Drawing.Size(307, 26);
		this.mnufilterchart.Text = "Filter Chart on Series Click";
		this.cmbFilterChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFilterChart.DropDownWidth = 175;
		this.cmbFilterChart.Name = "cmbFilterChart";
		this.cmbFilterChart.Size = new System.Drawing.Size(175, 28);
		this.mnuRange.Name = "mnuRange";
		this.mnuRange.Size = new System.Drawing.Size(307, 26);
		this.mnuRange.Text = "Range Selector";
		this.Columns.Frozen = true;
		this.Columns.HeaderText = "Y (Numeric)";
		this.Columns.MinimumWidth = 6;
		this.Columns.Name = "Columns";
		this.Columns.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Columns.ToolTipText = "Specify the Y-Variables";
		this.Columns.Width = 125;
		this.ColHdr.HeaderText = "Title";
		this.ColHdr.MinimumWidth = 6;
		this.ColHdr.Name = "ColHdr";
		this.ColHdr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColHdr.ToolTipText = "Enter a label for the variable. Will show in the Key";
		this.ColHdr.Width = 125;
		this.ColSumm.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColSumm.DropDownWidth = 150;
		this.ColSumm.HeaderText = "Summary";
		this.ColSumm.Items.AddRange("", "Avg", "Sum", "Count", "Count(*)", "Count Distinct", "Max", "Min", "StDev", "Var", "P50", "Expr");
		this.ColSumm.MinimumWidth = 6;
		this.ColSumm.Name = "ColSumm";
		this.ColSumm.ToolTipText = "Future - Add a Summary Statistic";
		this.ColSumm.Width = 90;
		this.ColAxis.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAxis.HeaderText = "Axis";
		this.ColAxis.Items.AddRange("", "x1y1", "x1y2");
		this.ColAxis.MinimumWidth = 6;
		this.ColAxis.Name = "ColAxis";
		this.ColAxis.ToolTipText = "Specify what Y-Axis to plot the variable";
		this.ColAxis.Width = 75;
		this.ColStyle.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColStyle.DropDownWidth = 150;
		this.ColStyle.HeaderText = "Plot Style";
		this.ColStyle.Items.AddRange("", "area", "column", "line", "scatter", "spider-spline", "spider-splinearea", "spider-column", "spline", "splinearea", "stackedarea", "stackedcolumn", "stackedcolumn100", "stackedline", "stackedline100");
		this.ColStyle.MinimumWidth = 6;
		this.ColStyle.Name = "ColStyle";
		this.ColStyle.ToolTipText = "Specify a plot style for the variable.Default is Boxes";
		this.ColStyle.Width = 90;
		this.ColLW.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColLW.HeaderText = "Line Width";
		this.ColLW.Items.AddRange("", "1", ".5", "2", "3", "4", "5", "6", "7", "8", "9", "10");
		this.ColLW.MinimumWidth = 6;
		this.ColLW.Name = "ColLW";
		this.ColLW.Width = 125;
		this.colfillcolor.HeaderText = "Fill Color";
		this.colfillcolor.MinimumWidth = 6;
		this.colfillcolor.Name = "colfillcolor";
		this.colfillcolor.ReadOnly = true;
		this.colfillcolor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colfillcolor.ToolTipText = "Set the Fill Color. Double-click to set";
		this.colfillcolor.Width = 125;
		this.ColColor.HeaderText = "Line Color";
		this.ColColor.MinimumWidth = 6;
		this.ColColor.Name = "ColColor";
		this.ColColor.ReadOnly = true;
		this.ColColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColColor.ToolTipText = "Set the line Color. Double-click to set";
		this.ColColor.Width = 125;
		this.colsymtype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colsymtype.HeaderText = "Symbol Type";
		this.colsymtype.Items.AddRange("", "circle", "diamond", "square", "triangle_down", "triangle_left", "triangle_right", "triangle_up");
		this.colsymtype.MinimumWidth = 6;
		this.colsymtype.Name = "colsymtype";
		this.colsymtype.Width = 125;
		this.colsymsize.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colsymsize.HeaderText = "Symbol Size";
		this.colsymsize.Items.AddRange("", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20");
		this.colsymsize.MinimumWidth = 6;
		this.colsymsize.Name = "colsymsize";
		this.colsymsize.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colsymsize.Width = 125;
		this.coldash.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.coldash.HeaderText = "Dash Style";
		this.coldash.Items.AddRange("", "4,4", "2,2");
		this.coldash.MinimumWidth = 6;
		this.coldash.Name = "coldash";
		this.coldash.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.coldash.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.coldash.Width = 125;
		this.colopacity.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colopacity.HeaderText = "Opacity";
		this.colopacity.Items.AddRange("", ".9", ".8", ".7", ".6", ".5", ".4", ".3", ".2", ".1");
		this.colopacity.MinimumWidth = 6;
		this.colopacity.Name = "colopacity";
		this.colopacity.ToolTipText = "Transparency";
		this.colopacity.Width = 125;
		this.collabels.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.collabels.HeaderText = "Labels";
		this.collabels.Items.AddRange("", "Y", "N");
		this.collabels.MinimumWidth = 6;
		this.collabels.Name = "collabels";
		this.collabels.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.collabels.ToolTipText = "Show Series Labels";
		this.collabels.Width = 125;
		this.colconnect.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colconnect.HeaderText = "Connect Missing";
		this.colconnect.Items.AddRange("", "skip", "connect", "zero");
		this.colconnect.MinimumWidth = 6;
		this.colconnect.Name = "colconnect";
		this.colconnect.ToolTipText = "Connect missing points for line series";
		this.colconnect.Width = 125;
		this.colTT.HeaderText = "Tool Tip";
		this.colTT.MinimumWidth = 6;
		this.colTT.Name = "colTT";
		this.colTT.ReadOnly = true;
		this.colTT.Width = 125;
		this.colsort.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colsort.HeaderText = "Sort";
		this.colsort.Items.AddRange("", "Asc", "Desc");
		this.colsort.MinimumWidth = 6;
		this.colsort.Name = "colsort";
		this.colsort.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colsort.ToolTipText = "Sort Variable";
		this.colsort.Width = 75;
		this.colLEGFMT.HeaderText = "Legend";
		this.colLEGFMT.MinimumWidth = 6;
		this.colLEGFMT.Name = "colLEGFMT";
		this.colLEGFMT.ReadOnly = true;
		this.colLEGFMT.Width = 125;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(827, 704);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.TabChart);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmJSChart";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Charting";
		this.TabTitles.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.GrpTitle.ResumeLayout(false);
		this.GrpTitle.PerformLayout();
		this.ContextGridY.ResumeLayout(false);
		this.TabCols.ResumeLayout(false);
		this.TabCols.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridWhere).EndInit();
		this.ContextXGroupBy.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridY).EndInit();
		this.ContextCols.ResumeLayout(false);
		this.TabChart.ResumeLayout(false);
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
	}

	public string Add_Mode(string Token, int MyCol)
	{
		string text = "";
		int num = 0;
		string text2 = "";
		string text3 = Token + "=";
		checked
		{
			int num2 = GridY.RowCount - 1;
			for (num = 0; num <= num2; num++)
			{
				if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num].Cells[0].Value)), "", TextCompare: false) != 0)
				{
					text2 = Conversions.ToString(GridY.Rows[num].Cells[MyCol].Value);
					text3 = text3 + text + text2;
					text = ";";
				}
			}
			return text3;
		}
	}

	public void Populate_Columns3(string File1, int MyMode, bool LoadOptCols, bool lClearData)
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
					int num3;
					string[] lCSVCols;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmJSChart - Populate_Columns3";
						num3 = 0;
						lCSVCols = new string[0];
						int num4 = 0;
						int num5 = 0;
						string text = "";
						int num6 = 0;
						string text2 = "";
						string text3 = "";
						if (Operators.CompareString(File1, "", TextCompare: false) == 0)
						{
							goto end_IL_0001;
						}
						if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(File1)), "HTTP*://*", CompareMethod.Binary))
						{
							text = BuildForm.Get_First_Line_Web(File1);
							chkHTTP.Checked = true;
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
								goto end_IL_0001;
							}
							num6 = Strings.InStrRev(File1, ".");
							if (num6 != 0)
							{
								text3 = Strings.Mid(File1, num6);
							}
							text2 = Globals_Renamed.MyPCDir + "r_" + Globals_Renamed.gSPFCache + text3;
							num3 = General_Procedures.Save_SQL_Query(text, text2);
							num3 = General_Procedures.GetCSVHeaders(text2, ref lCSVCols, MakeHdrsLowerCase: false);
							goto IL_0133;
						}
						num3 = General_Procedures.GetCSVHeaders(File1, ref lCSVCols, MakeHdrsLowerCase: false);
						chkHTTP.Checked = false;
						goto IL_0133;
					}
					case 897:
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
						IL_0133:
						if (num3 == 0)
						{
							int num7 = Information.UBound(lCSVCols);
							for (int num4 = 0; num4 <= num7; num4++)
							{
								lCSVCols[num4] = Strings.LCase(BuildForm.Test_Valid_jqw_Col(lCSVCols[num4]));
							}
							LstColumns.Items.Clear();
							if (lClearData)
							{
								Clear_Controls();
							}
							TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
							int num8 = Information.UBound(lCSVCols);
							for (int num4 = 0; num4 <= num8; num4++)
							{
								LstColumns.Items.Add(lCSVCols[num4]);
								ColGrid.Items.Add(lCSVCols[num4]);
							}
							if (LoadOptCols)
							{
								int num9 = TabOptions.Controls.Count - 1;
								for (int num4 = 0; num4 <= num9; num4++)
								{
									if (TabOptions.Controls[num4].Tag != null && Operators.ConditionalCompareObjectEqual(TabOptions.Controls[num4].Tag, "COMBOBOXC", TextCompare: false))
									{
										object obj = null;
										obj = TabOptions.Controls[num4];
										NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Clear", new object[0], null, null, null, IgnoreReturn: true);
										int num10 = Information.UBound(lCSVCols);
										for (int num5 = 1; num5 <= num10; num5++)
										{
											NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Add", new object[1] { Strings.Trim(lCSVCols[num5]) }, null, null, null, IgnoreReturn: true);
										}
									}
								}
							}
							lCSVCols = null;
						}
						else
						{
							Interaction.MsgBox("Could not locate the input CSV File: " + File1 + ". Your column selections were preserved but you will not be able to make significant edits to the plot design", MsgBoxStyle.Exclamation, "Could not locate CSV File");
							if (MyMode == 1)
							{
								TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
							}
						}
						goto end_IL_0001_2;
					}
					goto IL_03b7;
				}
				end_IL_0001_2:;
			}
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 897;
				continue;
			}
			break;
			IL_03b7:
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

	public void Clear_Where_Grid()
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
						errsource = "FrmJSChart - Clear_Where_Grid";
						short num3 = 0;
						short num4 = 0;
						ColGrid.Items.Clear();
						ColGrid.Items.Add("");
						short num5 = (short)(GridWhere.RowCount - 1);
						for (num3 = 0; num3 <= num5; num3 = (short)unchecked(num3 + 1))
						{
							short num6 = (short)(GridWhere.ColumnCount - 1);
							for (num4 = 0; num4 <= num6; num4 = (short)unchecked(num4 + 1))
							{
								if (num4 == 3)
								{
									GridWhere.Rows[num3].Cells[num4].Value = "=";
								}
								else
								{
									GridWhere.Rows[num3].Cells[num4].Value = "";
								}
							}
						}
						GridWhere.CurrentCell = GridWhere[2, 0];
						goto end_IL_0001;
					}
					case 294:
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
				try0001_dispatch = 294;
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

	public void Clear_Controls()
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
				int num5;
				int num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 618:
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
							goto IL_0027;
						case 6:
							goto IL_003a;
						case 7:
							goto IL_0049;
						case 8:
							goto IL_005c;
						case 9:
							goto IL_006f;
						case 10:
							goto IL_0079;
						case 11:
							goto IL_008d;
						case 12:
							goto IL_00a1;
						case 13:
							goto IL_00b5;
						case 14:
							goto IL_00c9;
						case 15:
							goto IL_00dd;
						case 16:
							goto IL_00f1;
						case 17:
							goto IL_0105;
						case 18:
							goto IL_0119;
						case 19:
							goto IL_012d;
						case 20:
							goto IL_0141;
						case 21:
							goto IL_0155;
						case 22:
							goto IL_0169;
						case 23:
							goto IL_017d;
						case 24:
							goto IL_0191;
						case 25:
							goto IL_01a1;
						case 26:
							goto IL_01b1;
						case 27:
							goto IL_01c1;
						case 28:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 29:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01a1:
					num2 = 25;
					ChkGridY.Checked = false;
					goto IL_01b1;
					IL_01b1:
					num2 = 26;
					ChkLogX.Checked = false;
					goto IL_01c1;
					IL_0191:
					num2 = 24;
					ChkGridX.Checked = false;
					goto IL_01a1;
					IL_01c1:
					num2 = 27;
					ChkLogY.Checked = false;
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
					LstColumns.Items.Clear();
					goto IL_0027;
					IL_0027:
					num2 = 5;
					LstX.Items.Clear();
					goto IL_003a;
					IL_003a:
					num2 = 6;
					GridY.RowCount = 0;
					goto IL_0049;
					IL_0049:
					num2 = 7;
					LstBy.Items.Clear();
					goto IL_005c;
					IL_005c:
					num2 = 8;
					LstGroup.Items.Clear();
					goto IL_006f;
					IL_006f:
					num2 = 9;
					Clear_Where_Grid();
					goto IL_0079;
					IL_0079:
					num2 = 10;
					TxtIn.Text = "";
					goto IL_008d;
					IL_008d:
					num2 = 11;
					TxtTitle.Text = "";
					goto IL_00a1;
					IL_00a1:
					num2 = 12;
					TxtXAxis.Text = "";
					goto IL_00b5;
					IL_00b5:
					num2 = 13;
					TxtYAxis.Text = "";
					goto IL_00c9;
					IL_00c9:
					num2 = 14;
					TxtY2Axis.Text = "";
					goto IL_00dd;
					IL_00dd:
					num2 = 15;
					TxtXLow.Text = "";
					goto IL_00f1;
					IL_00f1:
					num2 = 16;
					TxtYLow.Text = "";
					goto IL_0105;
					IL_0105:
					num2 = 17;
					TxtY2Low.Text = "";
					goto IL_0119;
					IL_0119:
					num2 = 18;
					TxtXHigh.Text = "";
					goto IL_012d;
					IL_012d:
					num2 = 19;
					TxtYHigh.Text = "";
					goto IL_0141;
					IL_0141:
					num2 = 20;
					TxtY2High.Text = "";
					goto IL_0155;
					IL_0155:
					num2 = 21;
					TxtYIncr.Text = "";
					goto IL_0169;
					IL_0169:
					num2 = 22;
					TxtY2Incr.Text = "";
					goto IL_017d;
					IL_017d:
					num2 = 23;
					TxtXIncr.Text = "";
					goto IL_0191;
					end_IL_0001_2:
					break;
				}
				num2 = 28;
				chkLogY2.Checked = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 618;
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

	public void Get_Set_Where(string MyMode, ref string MyWValue)
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
						errsource = "FrmJSChart - Get_Set_Where";
						int num3 = 0;
						int num4 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						int num5 = -1;
						bool flag = false;
						MyMode = Strings.Trim(Strings.UCase(MyMode));
						string left = MyMode;
						if (Operators.CompareString(left, "G", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "S", TextCompare: false) != 0 || Operators.CompareString(Strings.Trim(MyWValue), "", TextCompare: false) == 0)
							{
								goto end_IL_0001;
							}
							string[] array = Strings.Split(MyWValue, "~~~");
							int num6 = Information.UBound(array);
							for (num3 = 0; num3 <= num6; num3++)
							{
								string[] array2 = Strings.Split(array[num3], ";");
								flag = false;
								int num7 = ColGrid.Items.Count - 1;
								for (num4 = 0; num4 <= num7; num4++)
								{
									if (Operators.ConditionalCompareObjectEqual(ColGrid.Items[num4], array2[2], TextCompare: false))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									ColGrid.Items.Add(array2[2]);
								}
								num5++;
								GridWhere.Rows[num5].Cells[0].Value = array2[0];
								GridWhere.Rows[num5].Cells[1].Value = array2[1];
								GridWhere.Rows[num5].Cells[2].Value = array2[2];
								GridWhere.Rows[num5].Cells[3].Value = array2[3];
								GridWhere.Rows[num5].Cells[4].Value = array2[4];
								GridWhere.Rows[num5].Cells[5].Value = array2[5];
								array2 = null;
							}
							array = null;
							goto end_IL_0001;
						}
						MyWValue = "";
						int num8 = GridWhere.RowCount - 1;
						for (num3 = 0; num3 <= num8; num3++)
						{
							text4 = Conversions.ToString(GridWhere.Rows[num3].Cells[1].Value);
							text6 = Conversions.ToString(GridWhere.Rows[num3].Cells[0].Value);
							text = Conversions.ToString(GridWhere.Rows[num3].Cells[2].Value);
							text2 = Conversions.ToString(GridWhere.Rows[num3].Cells[3].Value);
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text2 = "=";
							}
							text3 = Conversions.ToString(GridWhere.Rows[num3].Cells[4].Value);
							text5 = Conversions.ToString(GridWhere.Rows[num3].Cells[5].Value);
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text3, "", TextCompare: false) != 0)
							{
								MyWValue = MyWValue + "~~~" + text6 + ";" + text4 + ";" + text + ";" + text2 + ";" + text3 + ";" + text5;
							}
						}
						if (Operators.CompareString(MyWValue, "", TextCompare: false) != 0)
						{
							MyWValue = Strings.Mid(MyWValue, Strings.Len("~~~") + 1);
						}
						goto end_IL_0001_2;
					}
					case 1126:
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
					goto IL_049c;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1126;
				continue;
			}
			break;
			IL_049c:
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

	public void Set_Index_List(ref ListBox LstAny, int MyIdx)
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
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 233:
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
								goto IL_001a;
							case 4:
								goto IL_0034;
							case 6:
								goto IL_0042;
							case 7:
								goto IL_0060;
							case 9:
								goto IL_0070;
							case 10:
								goto IL_0088;
							case 5:
							case 8:
							case 11:
							case 12:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 13:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0060:
						num2 = 7;
						LstAny.SelectedIndex = MyIdx - 1;
						break;
						IL_0070:
						num2 = 9;
						if (LstAny.Items.Count <= 0)
						{
							break;
						}
						goto IL_0088;
						IL_0042:
						num2 = 6;
						if (LstAny.Items.Count - 1 >= MyIdx - 1)
						{
							goto IL_0060;
						}
						goto IL_0070;
						IL_0088:
						num2 = 10;
						LstAny.SelectedIndex = 0;
						break;
						IL_000b:
						num2 = 2;
						LstAny.SelectedItems.Clear();
						goto IL_001a;
						IL_001a:
						num2 = 3;
						if (LstAny.Items.Count - 1 >= MyIdx)
						{
							goto IL_0034;
						}
						goto IL_0042;
						IL_0034:
						num2 = 4;
						LstAny.SelectedIndex = MyIdx;
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 12;
					LstAny.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 233;
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

	public void Init_Controls()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num4 = default(int);
		int num5 = default(int);
		string MyWValue = default(string);
		string text = default(string);
		string errsource = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		int num8 = default(int);
		int columnCount = default(int);
		int num10 = default(int);
		string[] array = default(string[]);
		int num11 = default(int);
		string[] array2 = default(string[]);
		int num12 = default(int);
		int num13 = default(int);
		int num14 = default(int);
		int num15 = default(int);
		string[] array3 = default(string[]);
		string[] array4 = default(string[]);
		int num16 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					int num7;
					int num9;
					string text5;
					string text6;
					string text7;
					DataGridView MyGrid;
					CheckBox MyCK;
					string MyVal;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 4577:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
								break;
							case 1:
								goto IL_0eed;
							default:
								goto end_IL_0001;
							}
							goto IL_0eb6;
						}
						IL_0d9a:
						num2 = 162;
						num4 = num5;
						goto IL_0da4;
						IL_0da4:
						num2 = 163;
						cmbFilterChart.Items.Add(MyWValue + " {" + text + "}");
						goto IL_0ddf;
						IL_0ea2:
						num2 = 181;
						Information.Err().Clear();
						goto end_IL_0001_2;
						IL_0eed:
						num6 = unchecked(num + 1);
						num = 0;
						switch (num6)
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
						case 6:
							goto IL_002d;
						case 7:
							goto IL_0032;
						case 8:
							goto IL_003b;
						case 9:
							goto IL_0044;
						case 10:
							goto IL_004a;
						case 11:
							goto IL_0050;
						case 12:
							goto IL_0056;
						case 13:
							goto IL_005c;
						case 14:
							goto IL_0066;
						case 15:
							goto IL_0070;
						case 16:
							goto IL_007a;
						case 17:
							goto IL_008a;
						case 18:
							goto IL_0090;
						case 19:
							goto IL_009f;
						case 20:
							goto IL_00ae;
						case 21:
							goto IL_00bd;
						case 22:
							goto IL_00d6;
						case 23:
							goto IL_00e6;
						case 24:
							goto IL_00f5;
						case 25:
							goto IL_0104;
						case 26:
							goto IL_0120;
						case 27:
							goto IL_0133;
						case 28:
							goto IL_014d;
						case 29:
							goto IL_0162;
						case 30:
							goto IL_0178;
						case 31:
							goto IL_0192;
						case 32:
							goto IL_01ae;
						case 33:
							goto IL_01bb;
						case 34:
							goto IL_01cc;
						case 35:
							goto IL_01d5;
						case 36:
							goto IL_01e4;
						case 37:
							goto IL_0210;
						case 38:
							goto IL_023c;
						case 39:
							goto IL_026a;
						case 40:
						case 41:
							goto IL_0293;
						case 42:
						case 43:
							goto IL_02a7;
						case 44:
							goto IL_02ad;
						case 45:
						case 46:
							goto IL_02c1;
						case 47:
						case 48:
							goto IL_02c9;
						case 49:
							goto IL_02e5;
						case 50:
							goto IL_02f4;
						case 51:
							goto IL_030d;
						case 52:
							goto IL_0320;
						case 53:
							goto IL_0332;
						case 54:
							goto IL_034b;
						case 55:
							goto IL_035a;
						case 57:
							goto IL_0363;
						case 58:
							goto IL_037c;
						case 56:
						case 59:
						case 60:
							goto IL_0393;
						case 61:
							goto IL_03a2;
						case 62:
							goto IL_03bb;
						case 63:
							goto IL_03ce;
						case 64:
							goto IL_03e0;
						case 65:
							goto IL_03f9;
						case 66:
							goto IL_0408;
						case 67:
						case 68:
							goto IL_0410;
						case 69:
							goto IL_041f;
						case 70:
							goto IL_0438;
						case 71:
							goto IL_044b;
						case 72:
							goto IL_045d;
						case 73:
							goto IL_0476;
						case 74:
							goto IL_0485;
						case 75:
						case 76:
							goto IL_048d;
						case 77:
							goto IL_049c;
						case 78:
							goto IL_04b5;
						case 79:
						case 80:
							goto IL_04c8;
						case 81:
							goto IL_04d7;
						case 82:
							goto IL_0518;
						case 83:
						case 84:
							goto IL_052b;
						case 85:
							goto IL_053e;
						case 86:
							goto IL_055b;
						case 87:
							goto IL_0569;
						case 88:
							goto IL_0582;
						case 89:
							goto IL_059b;
						case 90:
							goto IL_05b4;
						case 91:
							goto IL_05cd;
						case 92:
							goto IL_05e6;
						case 93:
							goto IL_05ff;
						case 94:
							goto IL_0618;
						case 95:
							goto IL_0631;
						case 96:
							goto IL_064a;
						case 97:
							goto IL_0663;
						case 98:
							goto IL_067c;
						case 99:
							goto IL_0695;
						case 100:
							goto IL_06ae;
						case 101:
							goto IL_06c7;
						case 102:
							goto IL_06e9;
						case 103:
							goto IL_06fd;
						case 104:
							goto IL_0716;
						case 105:
							goto IL_072f;
						case 106:
							goto IL_0748;
						case 107:
							goto IL_0761;
						case 108:
							goto IL_077a;
						case 109:
							goto IL_0793;
						case 110:
							goto IL_07ac;
						case 111:
							goto IL_07c5;
						case 112:
							goto IL_07de;
						case 113:
							goto IL_0808;
						case 114:
							goto IL_0832;
						case 115:
							goto IL_085c;
						case 116:
							goto IL_086b;
						case 117:
							goto IL_0884;
						case 118:
							goto IL_0894;
						case 119:
							goto IL_08be;
						case 120:
							goto IL_08e8;
						case 121:
							goto IL_0912;
						case 122:
							goto IL_093c;
						case 123:
							goto IL_0966;
						case 124:
							goto IL_097f;
						case 125:
							goto IL_0998;
						case 126:
							goto IL_09b1;
						case 127:
							goto IL_09ca;
						case 128:
							goto IL_09e3;
						case 129:
							goto IL_09ff;
						case 130:
							goto IL_0a1b;
						case 131:
							goto IL_0a37;
						case 132:
							goto IL_0a53;
						case 133:
							goto IL_0a6f;
						case 134:
							goto IL_0a99;
						case 135:
							goto IL_0ab0;
						case 136:
							goto IL_0ada;
						case 137:
							goto IL_0af1;
						case 138:
							goto IL_0b0d;
						case 139:
							goto IL_0b32;
						case 140:
							goto IL_0b49;
						case 141:
							goto IL_0b65;
						case 142:
							goto IL_0b8a;
						case 143:
							goto IL_0ba1;
						case 144:
							goto IL_0bbd;
						case 145:
							goto IL_0be2;
						case 146:
							goto IL_0bf9;
						case 147:
							goto IL_0c15;
						case 148:
							goto IL_0c28;
						case 149:
							goto IL_0c31;
						case 150:
							goto IL_0c3e;
						case 151:
							goto IL_0c47;
						case 152:
							goto IL_0c80;
						case 153:
							goto IL_0c9a;
						case 154:
							goto IL_0cb4;
						case 155:
							goto IL_0ccc;
						case 156:
							goto IL_0cff;
						case 157:
							goto IL_0d12;
						case 158:
							goto IL_0d25;
						case 159:
							goto IL_0d46;
						case 160:
							goto IL_0d63;
						case 161:
							goto IL_0d6f;
						case 162:
							goto IL_0d9a;
						case 163:
							goto IL_0da4;
						case 165:
							goto IL_0dd2;
						case 164:
						case 166:
						case 167:
						case 168:
							goto IL_0ddf;
						case 169:
							goto IL_0df4;
						case 170:
							goto IL_0dfd;
						case 171:
							goto IL_0e06;
						case 172:
							goto IL_0e17;
						case 173:
							goto IL_0e2b;
						case 174:
						case 175:
							goto IL_0e40;
						case 176:
						case 177:
							goto IL_0e50;
						case 178:
							goto IL_0e59;
						case 179:
							goto IL_0e7e;
						case 180:
						case 181:
							goto IL_0ea2;
						case 183:
							goto IL_0eb6;
						case 184:
							goto end_IL_0001_3;
						default:
							goto end_IL_0001;
						case 182:
						case 185:
							goto end_IL_0001_2;
						}
						goto default;
						IL_0eb6:
						num2 = 183;
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						break;
						IL_000a:
						num2 = 2;
						errsource = "FrmJSChart - Init_Controls";
						goto IL_0012;
						IL_0012:
						num2 = 3;
						text2 = "";
						goto IL_001b;
						IL_001b:
						num2 = 4;
						MyWValue = "";
						goto IL_0024;
						IL_0024:
						num2 = 5;
						text = "";
						goto IL_002d;
						IL_002d:
						num2 = 6;
						num4 = 0;
						goto IL_0032;
						IL_0032:
						num2 = 7;
						text3 = "";
						goto IL_003b;
						IL_003b:
						num2 = 8;
						text4 = "";
						goto IL_0044;
						IL_0044:
						num2 = 9;
						num7 = 0;
						goto IL_004a;
						IL_004a:
						num2 = 10;
						num8 = 0;
						goto IL_0050;
						IL_0050:
						num2 = 11;
						num5 = 0;
						goto IL_0056;
						IL_0056:
						num2 = 12;
						num9 = 0;
						goto IL_005c;
						IL_005c:
						num2 = 13;
						text5 = "";
						goto IL_0066;
						IL_0066:
						num2 = 14;
						text6 = "";
						goto IL_0070;
						IL_0070:
						num2 = 15;
						text7 = "";
						goto IL_007a;
						IL_007a:
						num2 = 16;
						columnCount = GridY.ColumnCount;
						goto IL_008a;
						IL_008a:
						num2 = 17;
						num10 = 0;
						goto IL_0090;
						IL_0090:
						num2 = 18;
						text2 = BuildChart.Get_StdCtrl("IN-FILE");
						goto IL_009f;
						IL_009f:
						num2 = 19;
						Populate_Columns3(text2, 1, LoadOptCols: false, lClearData: true);
						goto IL_00ae;
						IL_00ae:
						num2 = 20;
						text2 = BuildChart.Get_StdCtrl("KEEP-UNUSED-CHARTS");
						goto IL_00bd;
						IL_00bd:
						num2 = 21;
						if (Operators.CompareString(text2, "1", TextCompare: false) == 0)
						{
							goto IL_00d6;
						}
						goto IL_00e6;
						IL_00d6:
						num2 = 22;
						mnureportsource.Checked = true;
						goto IL_00e6;
						IL_00e6:
						num2 = 23;
						text3 = BuildChart.Get_StdCtrl("FILTER-CHARTS-PARENT");
						goto IL_00f5;
						IL_00f5:
						num2 = 24;
						text2 = BuildChart.Get_StdCtrl("Y");
						goto IL_0104;
						IL_0104:
						num2 = 25;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_0120;
						}
						goto IL_02c9;
						IL_0120:
						num2 = 26;
						array = Strings.Split(text2, "</tr>");
						goto IL_0133;
						IL_0133:
						num2 = 27;
						if (Information.UBound(array) >= 0)
						{
							goto IL_014d;
						}
						goto IL_02c1;
						IL_014d:
						num2 = 28;
						num11 = Information.UBound(array);
						num8 = 0;
						goto IL_02b6;
						IL_02b6:
						if (num8 <= num11)
						{
							goto IL_0162;
						}
						goto IL_02c1;
						IL_0162:
						num2 = 29;
						array2 = Strings.Split(array[num8], ";");
						goto IL_0178;
						IL_0178:
						num2 = 30;
						if (Information.UBound(array2) >= 0)
						{
							goto IL_0192;
						}
						goto IL_02a7;
						IL_0192:
						num2 = 31;
						GridY.RowCount += 1;
						goto IL_01ae;
						IL_01ae:
						num2 = 32;
						num10 = Information.UBound(array2);
						goto IL_01bb;
						IL_01bb:
						num2 = 33;
						if (columnCount < num10 + 1)
						{
							goto IL_01cc;
						}
						goto IL_01d5;
						IL_01cc:
						num2 = 34;
						num10 = columnCount - 1;
						goto IL_01d5;
						IL_01d5:
						num2 = 35;
						num12 = num10;
						num5 = 0;
						goto IL_029c;
						IL_029c:
						if (num5 <= num12)
						{
							goto IL_01e4;
						}
						goto IL_02a7;
						IL_01e4:
						num2 = 36;
						GridY.Rows[num8].Cells[num5].Value = array2[num5];
						goto IL_0210;
						IL_0210:
						num2 = 37;
						GridY.Rows[num8].Cells[num5].Tag = "";
						goto IL_023c;
						IL_023c:
						num2 = 38;
						if ((num5 == 7 || num5 == 6) && Operators.CompareString(Strings.Trim(array2[num5]), "", TextCompare: false) != 0)
						{
							goto IL_026a;
						}
						goto IL_0293;
						IL_0ddf:
						num2 = 168;
						num8++;
						goto IL_0deb;
						IL_0dd2:
						num2 = 165;
						text4 = text;
						goto IL_0ddf;
						IL_026a:
						num2 = 39;
						GridY[num5, num8].Style.BackColor = ColorTranslator.FromHtml(array2[num5]);
						goto IL_0293;
						IL_0293:
						num2 = 41;
						num5++;
						goto IL_029c;
						IL_02a7:
						num2 = 43;
						array2 = null;
						goto IL_02ad;
						IL_02ad:
						num2 = 44;
						num8++;
						goto IL_02b6;
						IL_02c1:
						num2 = 46;
						array = null;
						goto IL_02c9;
						IL_02c9:
						num2 = 48;
						MyGrid = GridY;
						GridModule.Number_Grid(ref MyGrid);
						GridY = MyGrid;
						goto IL_02e5;
						IL_02e5:
						num2 = 49;
						text2 = BuildChart.Get_StdCtrl("X");
						goto IL_02f4;
						IL_02f4:
						num2 = 50;
						if (Strings.InStr(text2, ";") != 0)
						{
							goto IL_030d;
						}
						goto IL_0363;
						IL_030d:
						num2 = 51;
						array = Strings.Split(text2, ";");
						goto IL_0320;
						IL_0320:
						num2 = 52;
						num13 = Information.UBound(array);
						num8 = 0;
						goto IL_0354;
						IL_0354:
						if (num8 <= num13)
						{
							goto IL_0332;
						}
						goto IL_035a;
						IL_035a:
						num2 = 55;
						array = null;
						goto IL_0393;
						IL_0332:
						num2 = 53;
						LstX.Items.Add(array[num8]);
						goto IL_034b;
						IL_034b:
						num2 = 54;
						num8++;
						goto IL_0354;
						IL_0363:
						num2 = 57;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_037c;
						}
						goto IL_0393;
						IL_037c:
						num2 = 58;
						LstX.Items.Add(text2);
						goto IL_0393;
						IL_0393:
						num2 = 60;
						text2 = BuildChart.Get_StdCtrl("BY");
						goto IL_03a2;
						IL_03a2:
						num2 = 61;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_03bb;
						}
						goto IL_0410;
						IL_03bb:
						num2 = 62;
						array = Strings.Split(text2, ";");
						goto IL_03ce;
						IL_03ce:
						num2 = 63;
						num14 = Information.UBound(array);
						num8 = 0;
						goto IL_0402;
						IL_0402:
						if (num8 <= num14)
						{
							goto IL_03e0;
						}
						goto IL_0408;
						IL_0408:
						num2 = 66;
						array = null;
						goto IL_0410;
						IL_03e0:
						num2 = 64;
						LstBy.Items.Add(array[num8]);
						goto IL_03f9;
						IL_03f9:
						num2 = 65;
						num8++;
						goto IL_0402;
						IL_0410:
						num2 = 68;
						text2 = BuildChart.Get_StdCtrl("GROUP-BY");
						goto IL_041f;
						IL_041f:
						num2 = 69;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_0438;
						}
						goto IL_048d;
						IL_0438:
						num2 = 70;
						array = Strings.Split(text2, ";");
						goto IL_044b;
						IL_044b:
						num2 = 71;
						num15 = Information.UBound(array);
						num8 = 0;
						goto IL_047f;
						IL_047f:
						if (num8 <= num15)
						{
							goto IL_045d;
						}
						goto IL_0485;
						IL_0485:
						num2 = 74;
						array = null;
						goto IL_048d;
						IL_045d:
						num2 = 72;
						LstGroup.Items.Add(array[num8]);
						goto IL_0476;
						IL_0476:
						num2 = 73;
						num8++;
						goto IL_047f;
						IL_048d:
						num2 = 76;
						MyWValue = BuildChart.Get_StdCtrl("CHART-WHERE");
						goto IL_049c;
						IL_049c:
						num2 = 77;
						if (Operators.CompareString(MyWValue, "N/A", TextCompare: false) != 0)
						{
							goto IL_04b5;
						}
						goto IL_04c8;
						IL_04b5:
						num2 = 78;
						Get_Set_Where("S", ref MyWValue);
						goto IL_04c8;
						IL_04c8:
						num2 = 80;
						text2 = BuildChart.Get_StdCtrl("CHART-ITEMS-PER-ROW");
						goto IL_04d7;
						IL_04d7:
						num2 = 81;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(text2, "1", TextCompare: false) >= 0 && Operators.CompareString(text2, "9", TextCompare: false) <= 0)
						{
							goto IL_0518;
						}
						goto IL_052b;
						IL_0e50:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0e59;
						IL_0e59:
						num2 = 178;
						if (Operators.CompareString(TxtBackColor.Text, "", TextCompare: false) != 0)
						{
							goto IL_0e7e;
						}
						goto IL_0ea2;
						IL_0518:
						num2 = 82;
						cmbperRow.Text = text2;
						goto IL_052b;
						IL_052b:
						num2 = 84;
						fRange = BuildChart.Get_StdCtrl("GRID-BACK-FRONT");
						goto IL_053e;
						IL_053e:
						num2 = 85;
						if (Operators.CompareString(fRange, "N/A", TextCompare: false) == 0)
						{
							goto IL_055b;
						}
						goto IL_0569;
						IL_055b:
						num2 = 86;
						fRange = "";
						goto IL_0569;
						IL_0569:
						num2 = 87;
						TxtTitle.Text = BuildChart.Get_StdCtrl("TITLE");
						goto IL_0582;
						IL_0582:
						num2 = 88;
						TxtXAxis.Text = BuildChart.Get_StdCtrl("X-AXIS");
						goto IL_059b;
						IL_059b:
						num2 = 89;
						TxtYAxis.Text = BuildChart.Get_StdCtrl("Y-AXIS");
						goto IL_05b4;
						IL_05b4:
						num2 = 90;
						TxtY2Axis.Text = BuildChart.Get_StdCtrl("Y-AXIS-2");
						goto IL_05cd;
						IL_05cd:
						num2 = 91;
						TxtXLow.Text = BuildChart.Get_StdCtrl("X-LOW");
						goto IL_05e6;
						IL_05e6:
						num2 = 92;
						TxtXHigh.Text = BuildChart.Get_StdCtrl("X-HIGH");
						goto IL_05ff;
						IL_05ff:
						num2 = 93;
						TxtYLow.Text = BuildChart.Get_StdCtrl("Y-LOW");
						goto IL_0618;
						IL_0618:
						num2 = 94;
						TxtYHigh.Text = BuildChart.Get_StdCtrl("Y-HIGH");
						goto IL_0631;
						IL_0631:
						num2 = 95;
						TxtY2Low.Text = BuildChart.Get_StdCtrl("Y2-LOW");
						goto IL_064a;
						IL_064a:
						num2 = 96;
						TxtY2High.Text = BuildChart.Get_StdCtrl("Y2-HIGH");
						goto IL_0663;
						IL_0663:
						num2 = 97;
						TxtYIncr.Text = BuildChart.Get_StdCtrl("Y-INC");
						goto IL_067c;
						IL_067c:
						num2 = 98;
						TxtY2Incr.Text = BuildChart.Get_StdCtrl("Y2-INC");
						goto IL_0695;
						IL_0695:
						num2 = 99;
						TxtXIncr.Text = BuildChart.Get_StdCtrl("X-INC");
						goto IL_06ae;
						IL_06ae:
						num2 = 100;
						TxtBackColor.Text = BuildChart.Get_StdCtrl("LEGENDBG");
						goto IL_06c7;
						IL_06c7:
						num2 = 101;
						if (Operators.CompareString(TxtBackColor.Text, "N/A", TextCompare: false) == 0)
						{
							goto IL_06e9;
						}
						goto IL_06fd;
						IL_06e9:
						num2 = 102;
						TxtBackColor.Text = "";
						goto IL_06fd;
						IL_06fd:
						num2 = 103;
						TxtXLStp.Text = BuildChart.Get_StdCtrl("X-MAJOR-GRID");
						goto IL_0716;
						IL_0716:
						num2 = 104;
						TxtYLStp.Text = BuildChart.Get_StdCtrl("Y-MAJOR-GRID");
						goto IL_072f;
						IL_072f:
						num2 = 105;
						TxtY2LStp.Text = BuildChart.Get_StdCtrl("FRAME-X");
						goto IL_0748;
						IL_0748:
						num2 = 106;
						TxtXTStp.Text = BuildChart.Get_StdCtrl("X-MINOR-GRID");
						goto IL_0761;
						IL_0761:
						num2 = 107;
						TxtYTStp.Text = BuildChart.Get_StdCtrl("Y-MINOR-GRID");
						goto IL_077a;
						IL_077a:
						num2 = 108;
						TxtY2TStp.Text = BuildChart.Get_StdCtrl("FRAME-Y");
						goto IL_0793;
						IL_0793:
						num2 = 109;
						TxtXGStp.Text = BuildChart.Get_StdCtrl("X-MAJOR-TICKS");
						goto IL_07ac;
						IL_07ac:
						num2 = 110;
						TxtYGStp.Text = BuildChart.Get_StdCtrl("Y-MAJOR-TICKS");
						goto IL_07c5;
						IL_07c5:
						num2 = 111;
						TxtY2GStp.Text = BuildChart.Get_StdCtrl("FRAME-COLOR");
						goto IL_07de;
						IL_07de:
						num2 = 112;
						MyCK = ChkonTicX;
						MyVal = BuildChart.Get_StdCtrl("X-MINOR-TICKS");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkonTicX = MyCK;
						goto IL_0808;
						IL_0808:
						num2 = 113;
						MyCK = ChkonTicY;
						MyVal = BuildChart.Get_StdCtrl("Y-MINOR-TICKS");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkonTicY = MyCK;
						goto IL_0832;
						IL_0832:
						num2 = 114;
						MyCK = ChkonTicY2;
						MyVal = BuildChart.Get_StdCtrl("FRAME-MARKER");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkonTicY2 = MyCK;
						goto IL_085c;
						IL_085c:
						num2 = 115;
						text2 = BuildChart.Get_StdCtrl("KEY-POS");
						goto IL_086b;
						IL_086b:
						num2 = 116;
						if (Operators.CompareString(text2, "1", TextCompare: false) == 0)
						{
							goto IL_0884;
						}
						goto IL_0894;
						IL_0884:
						num2 = 117;
						chklegend.Checked = true;
						goto IL_0894;
						IL_0894:
						num2 = 118;
						MyCK = ChkGridX;
						MyVal = BuildChart.Get_StdCtrl("GRID-X");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkGridX = MyCK;
						goto IL_08be;
						IL_08be:
						num2 = 119;
						MyCK = ChkGridY;
						MyVal = BuildChart.Get_StdCtrl("GRID-Y");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkGridY = MyCK;
						goto IL_08e8;
						IL_08e8:
						num2 = 120;
						MyCK = ChkLogX;
						MyVal = BuildChart.Get_StdCtrl("LOG-X");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkLogX = MyCK;
						goto IL_0912;
						IL_0912:
						num2 = 121;
						MyCK = ChkLogY;
						MyVal = BuildChart.Get_StdCtrl("LOG-Y");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkLogY = MyCK;
						goto IL_093c;
						IL_093c:
						num2 = 122;
						MyCK = chkLogY2;
						MyVal = BuildChart.Get_StdCtrl("LOG-Y2");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						chkLogY2 = MyCK;
						goto IL_0966;
						IL_0966:
						num2 = 123;
						CmbXRotate.Text = BuildChart.Get_StdCtrl("X-AXIS-ROTATE");
						goto IL_097f;
						IL_097f:
						num2 = 124;
						CmbYRotate.Text = BuildChart.Get_StdCtrl("Y-AXIS-ROTATE");
						goto IL_0998;
						IL_0998:
						num2 = 125;
						cmbY2Rotate.Text = BuildChart.Get_StdCtrl("Y2-AXIS-ROTATE");
						goto IL_09b1;
						IL_09b1:
						num2 = 126;
						cmbXDT.Text = BuildChart.Get_StdCtrl("X-MAX");
						goto IL_09ca;
						IL_09ca:
						num2 = 127;
						TxtPadL.Text = BuildChart.Get_StdCtrl("Y-MAX");
						goto IL_09e3;
						IL_09e3:
						num2 = 128;
						TxtPadR.Text = BuildChart.Get_StdCtrl("Y-MIN");
						goto IL_09ff;
						IL_09ff:
						num2 = 129;
						TxtPadT.Text = BuildChart.Get_StdCtrl("Y-2-MAX");
						goto IL_0a1b;
						IL_0a1b:
						num2 = 130;
						TxtPadB.Text = BuildChart.Get_StdCtrl("Y-2-MIN");
						goto IL_0a37;
						IL_0a37:
						num2 = 131;
						CmbFrameX.Text = BuildChart.Get_StdCtrl("SIZE-X");
						goto IL_0a53;
						IL_0a53:
						num2 = 132;
						CmbFrameY.Text = BuildChart.Get_StdCtrl("SIZE-Y");
						goto IL_0a6f;
						IL_0a6f:
						num2 = 133;
						if (Operators.CompareString(Strings.Trim(CmbFrameX.Text), "", TextCompare: false) == 0)
						{
							goto IL_0a99;
						}
						goto IL_0ab0;
						IL_0a99:
						num2 = 134;
						CmbFrameX.Text = "500";
						goto IL_0ab0;
						IL_0ab0:
						num2 = 135;
						if (Operators.CompareString(Strings.Trim(CmbFrameY.Text), "", TextCompare: false) == 0)
						{
							goto IL_0ada;
						}
						goto IL_0af1;
						IL_0ada:
						num2 = 136;
						CmbFrameY.Text = "500";
						goto IL_0af1;
						IL_0af1:
						num2 = 137;
						cmbXFmt.Text = BuildChart.Get_StdCtrl("PSEUDO-X");
						goto IL_0b0d;
						IL_0b0d:
						num2 = 138;
						if (Operators.CompareString(cmbXFmt.Text, "N/A", TextCompare: false) == 0)
						{
							goto IL_0b32;
						}
						goto IL_0b49;
						IL_0b32:
						num2 = 139;
						cmbXFmt.Text = "";
						goto IL_0b49;
						IL_0b49:
						num2 = 140;
						cmbYFmt.Text = BuildChart.Get_StdCtrl("Y-FMT");
						goto IL_0b65;
						IL_0b65:
						num2 = 141;
						if (Operators.CompareString(cmbYFmt.Text, "N/A", TextCompare: false) == 0)
						{
							goto IL_0b8a;
						}
						goto IL_0ba1;
						IL_0b8a:
						num2 = 142;
						cmbYFmt.Text = "";
						goto IL_0ba1;
						IL_0ba1:
						num2 = 143;
						cmbY2Fmt.Text = BuildChart.Get_StdCtrl("Y2-FMT");
						goto IL_0bbd;
						IL_0bbd:
						num2 = 144;
						if (Operators.CompareString(cmbY2Fmt.Text, "N/A", TextCompare: false) == 0)
						{
							goto IL_0be2;
						}
						goto IL_0bf9;
						IL_0be2:
						num2 = 145;
						cmbY2Fmt.Text = "";
						goto IL_0bf9;
						IL_0bf9:
						num2 = 146;
						cmbFilterChart.Items.Add("");
						goto IL_0c15;
						IL_0c15:
						num2 = 147;
						cmbFilterChart.SelectedIndex = 0;
						goto IL_0c28;
						IL_0c28:
						num2 = 148;
						num4 = 0;
						goto IL_0c31;
						IL_0c31:
						num2 = 149;
						text4 = "";
						goto IL_0c3e;
						IL_0c3e:
						num2 = 150;
						num5 = 0;
						goto IL_0c47;
						IL_0c47:
						num2 = 151;
						if (Operators.CompareString(fDisplay, "", TextCompare: false) != 0 && Operators.CompareString(fActual, "", TextCompare: false) != 0)
						{
							goto IL_0c80;
						}
						goto IL_0e50;
						IL_0e7e:
						num2 = 179;
						TxtBackColor.BackColor = ColorTranslator.FromHtml(TxtBackColor.Text);
						goto IL_0ea2;
						IL_0c80:
						num2 = 152;
						array3 = Strings.Split(fDisplay, ",");
						goto IL_0c9a;
						IL_0c9a:
						num2 = 153;
						array4 = Strings.Split(fActual, ",");
						goto IL_0cb4;
						IL_0cb4:
						num2 = 154;
						num16 = Information.UBound(array3);
						num8 = 0;
						goto IL_0deb;
						IL_0deb:
						if (num8 <= num16)
						{
							goto IL_0ccc;
						}
						goto IL_0df4;
						IL_0df4:
						num2 = 169;
						array3 = null;
						goto IL_0dfd;
						IL_0dfd:
						num2 = 170;
						array4 = null;
						goto IL_0e06;
						IL_0e06:
						num2 = 171;
						if (num4 != 0)
						{
							goto IL_0e17;
						}
						goto IL_0e40;
						IL_0e17:
						num2 = 172;
						cmbFilterChart.SelectedIndex = num4;
						goto IL_0e2b;
						IL_0e2b:
						num2 = 173;
						mnufilterchart.Checked = true;
						goto IL_0e40;
						IL_0e40:
						num2 = 175;
						fName = text4;
						goto IL_0e50;
						IL_0ccc:
						num2 = 155;
						if (Operators.CompareString(Strings.Mid(array3[num8] + "   ", 1, 3), "CI:", TextCompare: false) == 0)
						{
							goto IL_0cff;
						}
						goto IL_0ddf;
						IL_0cff:
						num2 = 156;
						MyWValue = Strings.Mid(array3[num8], 4);
						goto IL_0d12;
						IL_0d12:
						num2 = 157;
						text = Strings.Mid(array4[num8], 4);
						goto IL_0d25;
						IL_0d25:
						num2 = 158;
						text = Strings.LCase(Strings.Replace(text, ".GIF", "", 1, -1, CompareMethod.Text));
						goto IL_0d46;
						IL_0d46:
						num2 = 159;
						if (Operators.CompareString(MyWValue, fName, TextCompare: false) != 0)
						{
							goto IL_0d63;
						}
						goto IL_0dd2;
						IL_0d63:
						num2 = 160;
						num5++;
						goto IL_0d6f;
						IL_0d6f:
						num2 = 161;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(text, text3, TextCompare: false) == 0)
						{
							goto IL_0d9a;
						}
						goto IL_0da4;
						end_IL_0001_3:
						break;
					}
					num2 = 184;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4577;
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

	public void Add_ListBox(ref ListBox DestList)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyCol = default(string);
		int num5 = default(int);
		string MyDT = default(string);
		int num6 = default(int);
		int num7 = default(int);
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
								goto IL_000f;
							case 4:
								goto IL_0018;
							case 5:
								goto IL_0021;
							case 6:
								goto IL_0026;
							case 7:
								goto IL_0035;
							case 9:
								goto IL_0047;
							case 10:
								goto IL_0062;
							case 11:
								goto IL_008d;
							case 12:
								goto IL_009a;
							case 13:
								goto IL_00b2;
							case 14:
								goto IL_00c7;
							case 15:
								goto IL_00d9;
							case 16:
								goto IL_00e8;
							case 17:
								goto IL_00fc;
							case 18:
								goto IL_011b;
							case 20:
								goto IL_0131;
							case 21:
								goto IL_014d;
							case 19:
							case 22:
							case 23:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 8:
							case 24:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00b2:
						num2 = 13;
						if (!Check_Same_Var(ref DestList, MyCol))
						{
							goto IL_00c7;
						}
						goto IL_00d9;
						IL_00c7:
						num2 = 14;
						DestList.Items.Add(MyCol);
						goto IL_00d9;
						IL_009a:
						num2 = 12;
						MyCol = f_MyPre + MyCol + f_MyPost;
						goto IL_00b2;
						IL_00d9:
						num2 = 15;
						num5++;
						goto IL_00e0;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						MyCol = "";
						goto IL_0018;
						IL_0018:
						num2 = 4;
						MyDT = "";
						goto IL_0021;
						IL_0021:
						num2 = 5;
						num6 = -1;
						goto IL_0026;
						IL_0026:
						num2 = 6;
						num6 = LstColumns.SelectedIndex;
						goto IL_0035;
						IL_0035:
						num2 = 7;
						if (num6 == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_0047;
						IL_0047:
						num2 = 9;
						num7 = LstColumns.SelectedIndices.Count - 1;
						num5 = 0;
						goto IL_00e0;
						IL_00e0:
						if (num5 <= num7)
						{
							goto IL_0062;
						}
						goto IL_00e8;
						IL_00e8:
						num2 = 16;
						LstColumns.SelectedItems.Clear();
						goto IL_00fc;
						IL_00fc:
						num2 = 17;
						if (LstColumns.Items.Count - 1 > num6)
						{
							goto IL_011b;
						}
						goto IL_0131;
						IL_011b:
						num2 = 18;
						LstColumns.SelectedIndex = num6 + 1;
						break;
						IL_0131:
						num2 = 20;
						if (LstColumns.Items.Count <= 0)
						{
							break;
						}
						goto IL_014d;
						IL_014d:
						num2 = 21;
						LstColumns.SelectedIndex = 0;
						break;
						IL_0062:
						num2 = 10;
						MyCol = Conversions.ToString(LstColumns.Items[LstColumns.SelectedIndices[num5]]);
						goto IL_008d;
						IL_008d:
						num2 = 11;
						BuildForm.Strip_Col_DT(ref MyCol, ref MyDT);
						goto IL_009a;
						end_IL_0001_2:
						break;
					}
					num2 = 23;
					LstColumns.Focus();
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
	}

	private void cmdCancel_Click(object sender, EventArgs e)
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
				case 63:
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
					f_OK = false;
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
				try0001_dispatch = 63;
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
					string prompt;
					string title;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmJSChart - CmdOK_Click";
						int num3 = 0;
						int num4 = 0;
						string text = "";
						prompt = "";
						title = "";
						int num5 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						bool flag = false;
						string text11 = "";
						bool flag2 = false;
						int num6 = 0;
						string MyWValue = "";
						bool flag3 = false;
						text6 = Strings.Trim(TxtIn.Text);
						if (Operators.CompareString(text6, "", TextCompare: false) == 0)
						{
							prompt = "You must specify an Input File on the Columns Tab.";
							goto IL_16ff;
						}
						if (LstX.Items.Count == 0)
						{
							prompt = "No X Columns were assigned on the Columns Tab.";
							goto IL_16ff;
						}
						text7 = "N";
						int num7 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num7; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0 && (Operators.ConditionalCompareObjectEqual(GridY.Rows[num3].Cells[3].Value, "", TextCompare: false) || Operators.ConditionalCompareObjectEqual(GridY.Rows[num3].Cells[3].Value, "x1y1", TextCompare: false)))
							{
								text7 = "Y";
								break;
							}
						}
						if (Operators.CompareString(text7, "N", TextCompare: false) == 0)
						{
							prompt = "You must select at least one plot to be on the x1y1 axis";
							goto IL_16ff;
						}
						flag = false;
						int num8 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num8; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							prompt = "No Y Columns were assigned on the Columns Tab.";
							goto IL_16ff;
						}
						num4 = 0;
						int num9 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num9; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[15].Value)), "", TextCompare: false) != 0)
							{
								num4++;
							}
						}
						if (num4 > 1)
						{
							prompt = "You are only allowed to sort by one variable.";
							goto IL_16ff;
						}
						num4 = 0;
						num6 = 0;
						int num10 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num10; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0)
							{
								num6++;
								if (Operators.ConditionalCompareObjectNotEqual(GridY.Rows[num3].Cells[2].Value, "", TextCompare: false))
								{
									num4++;
									flag2 = true;
								}
							}
						}
						if (num4 != 0 && num4 != num6)
						{
							prompt = "All Y variables must eiher have Summary expressions or none must have Summary expressions";
							goto IL_16ff;
						}
						text9 = "";
						text7 = "";
						text8 = "";
						int num11 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num11; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0 && LikeOperator.LikeString(Strings.LCase(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[4].Value))), "spider*", CompareMethod.Binary))
							{
								text7 = "T";
								text9 = Strings.LCase(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[4].Value)));
								break;
							}
						}
						if (Operators.CompareString(text7, "T", TextCompare: false) == 0)
						{
							int num12 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num12; num3++)
							{
								if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0)
								{
									if (Operators.CompareString(Strings.LCase(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[3].Value))), "x1y2", TextCompare: false) == 0)
									{
										prompt = "Spider charts can only occur on the X1/Y1 axis";
										text7 = "F";
										break;
									}
									if (Operators.CompareString(text9, Strings.LCase(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[4].Value))), TextCompare: false) != 0)
									{
										prompt = "You cannot mix Spider charts with other types of charts including different kinds of spider charts";
										text7 = "F";
										break;
									}
								}
							}
							if (Operators.CompareString(text7, "F", TextCompare: false) == 0)
							{
								goto IL_16ff;
							}
						}
						text9 = "";
						text7 = "T";
						text8 = "";
						int num13 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num13; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[3].Value))), "x1y2", TextCompare: false) == 0)
							{
								text8 = Conversions.ToString(GridY.Rows[num3].Cells[4].Value);
								if (Operators.CompareString(text8, "", TextCompare: false) == 0)
								{
									text8 = "column";
								}
								if (Operators.CompareString(text9, "", TextCompare: false) == 0)
								{
									text9 = text8;
								}
								else if (Operators.CompareString(text9, text8, TextCompare: false) != 0)
								{
									text7 = "F";
									break;
								}
							}
						}
						if (Operators.CompareString(text7, "F", TextCompare: false) == 0)
						{
							prompt = "For the x1y2 Axis only, series types must be the same. E.g., on Axis x1y2, you have defined series \"" + text9 + "\" and series \"" + text8 + "\"";
							goto IL_16ff;
						}
						flag = false;
						int num14 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num14; num3++)
						{
							if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)), "=", TextCompare: false) == 0)
							{
								flag = true;
							}
							else if (Operators.CompareString(Strings.Mid(Strings.Trim(Conversions.ToString(GridY.Rows[num3].Cells[0].Value)) + " ", 1, 1), "=", TextCompare: false) == 0)
							{
								if (Operators.ConditionalCompareObjectEqual(GridY.Rows[num3].Cells[4].Value, "", TextCompare: false))
								{
									GridY.Rows[num3].Cells[4].Value = "line";
								}
								if (flag2 && Operators.ConditionalCompareObjectEqual(GridY.Rows[num3].Cells[2].Value, "", TextCompare: false))
								{
									GridY.Rows[num3].Cells[2].Value = "Max";
								}
							}
						}
						if (flag)
						{
							prompt = "You cannot have a Y value equal to \"=\".";
							goto IL_16ff;
						}
						if (mnureportsource.Checked && (LstGroup.Items.Count >= 1 || LstBy.Items.Count >= 1))
						{
							prompt = "For Interactive Report Sources (menu Options -> Use an Interactive Report Source) , you cannot specify GROUP nor BY columns.";
							goto IL_16ff;
						}
						Get_Set_Where("G", ref MyWValue);
						if (Strings.InStr(Strings.UCase(MyWValue), "@@SPF-CHART-WHERE-X@@") != 0)
						{
							flag3 = true;
						}
						if (flag3 && mnureportsource.Checked)
						{
							prompt = "A report cannot both be sourced by a Report (Option \"Use an Interactive Report Source\" checked) and a Chart (Where caluse value of \"@@SPF-CHART-WHERE-X@@\" present). Please choose one or the other or none";
							goto IL_16ff;
						}
						BuildChart.Set_StdCtrl("IN-FILE", text6);
						text6 = Strings.Trim(cmbperRow.Text);
						if (Operators.CompareString(text6, "1", TextCompare: false) >= 0 && Operators.CompareString(text6, "9", TextCompare: false) <= 0)
						{
							BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", text6);
						}
						else
						{
							BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", "1");
						}
						text = "";
						text6 = "";
						if (LstX.Items.Count > 0)
						{
							int num15 = LstX.Items.Count - 1;
							for (num3 = 0; num3 <= num15; num3++)
							{
								text6 = Conversions.ToString(Operators.ConcatenateObject(text6 + text, LstX.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("X", text6);
						text = "";
						text6 = "";
						int num16 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num16; num3++)
						{
							if (Operators.ConditionalCompareObjectNotEqual(GridY.Rows[num3].Cells[0].Value, "", TextCompare: false))
							{
								if (Operators.CompareString(text6, "", TextCompare: false) != 0)
								{
									text6 += "</tr>";
								}
								int num17 = GridY.ColumnCount - 1;
								for (num4 = 0; num4 <= num17; num4++)
								{
									text6 = Conversions.ToString(Operators.ConcatenateObject(text6 + text, GridY.Rows[num3].Cells[num4].Value));
									text = ";";
								}
							}
						}
						text6 = Strings.Replace(text6, "</tr>;", "</tr>", 1, -1, CompareMethod.Text);
						BuildChart.Set_StdCtrl("Y", text6);
						text11 = "";
						text11 = Add_Mode("#SQLVAR", 0);
						text11 = text11 + "\r\n" + Add_Mode("#SQLHDR", 1);
						text11 = text11 + "\r\n" + Add_Mode("#SQLSUM", 2);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLA", 3);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLS", 4);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLW", 5);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLC", 7);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLF", 6);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPST", 8);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPSZ", 9);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPDS", 10);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPOP", 11);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLB", 12);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPCM", 13);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPTT", 14);
						text11 = text11 + "\r\n" + Add_Mode("#SQLSORT", 15);
						text11 = text11 + "\r\n" + Add_Mode("#SQLPLEG", 16);
						BuildChart.Set_StdCtrl("MODE", text11);
						text = "";
						text6 = "";
						if (LstBy.Items.Count > 0)
						{
							int num18 = LstBy.Items.Count - 1;
							for (num3 = 0; num3 <= num18; num3++)
							{
								text6 = Conversions.ToString(Operators.ConcatenateObject(text6 + text, LstBy.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("BY", text6);
						text = "";
						text6 = "";
						if (LstGroup.Items.Count > 0)
						{
							int num19 = LstGroup.Items.Count - 1;
							for (num3 = 0; num3 <= num19; num3++)
							{
								text6 = Conversions.ToString(Operators.ConcatenateObject(text6 + text, LstGroup.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("GROUP-BY", text6);
						BuildChart.Set_StdCtrl("CHART-WHERE", MyWValue);
						BuildChart.Set_StdCtrl("TITLE", Strings.Trim(TxtTitle.Text));
						BuildChart.Set_StdCtrl("X-AXIS", Strings.Trim(TxtXAxis.Text));
						BuildChart.Set_StdCtrl("Y-AXIS", Strings.Trim(TxtYAxis.Text));
						BuildChart.Set_StdCtrl("Y-AXIS-2", Strings.Trim(TxtY2Axis.Text));
						BuildChart.Set_StdCtrl("X-LOW", Strings.Trim(TxtXLow.Text));
						BuildChart.Set_StdCtrl("X-HIGH", Strings.Trim(TxtXHigh.Text));
						BuildChart.Set_StdCtrl("Y-LOW", Strings.Trim(TxtYLow.Text));
						BuildChart.Set_StdCtrl("Y-HIGH", Strings.Trim(TxtYHigh.Text));
						BuildChart.Set_StdCtrl("Y2-LOW", Strings.Trim(TxtY2Low.Text));
						BuildChart.Set_StdCtrl("Y2-HIGH", Strings.Trim(TxtY2High.Text));
						BuildChart.Set_StdCtrl("Y-INC", Strings.Trim(TxtYIncr.Text));
						BuildChart.Set_StdCtrl("Y2-INC", Strings.Trim(TxtY2Incr.Text));
						BuildChart.Set_StdCtrl("X-INC", Strings.Trim(TxtXIncr.Text));
						BuildChart.Set_StdCtrl("LEGENDBG", Strings.Trim(TxtBackColor.Text));
						BuildChart.Set_StdCtrl("GRID-BACK-FRONT", Strings.Trim(fRange));
						text6 = "";
						CheckBox MyCK = ChkGridX;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkGridX = MyCK;
						BuildChart.Set_StdCtrl("GRID-X", text6);
						text6 = "";
						MyCK = ChkGridY;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkGridY = MyCK;
						BuildChart.Set_StdCtrl("GRID-Y", text6);
						text6 = "";
						MyCK = ChkLogX;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkLogX = MyCK;
						BuildChart.Set_StdCtrl("LOG-X", text6);
						text6 = "";
						MyCK = ChkLogY;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkLogY = MyCK;
						BuildChart.Set_StdCtrl("LOG-Y", text6);
						text6 = "";
						MyCK = chkLogY2;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						chkLogY2 = MyCK;
						BuildChart.Set_StdCtrl("LOG-Y2", text6);
						BuildChart.Set_StdCtrl("X-AXIS-ROTATE", Strings.Trim(CmbXRotate.Text));
						BuildChart.Set_StdCtrl("Y-AXIS-ROTATE", Strings.Trim(CmbYRotate.Text));
						BuildChart.Set_StdCtrl("Y2-AXIS-ROTATE", Strings.Trim(cmbY2Rotate.Text));
						BuildChart.Set_StdCtrl("X-MAX", Strings.Trim(cmbXDT.Text));
						BuildChart.Set_StdCtrl("Y-MAX", Strings.Trim(TxtPadL.Text));
						BuildChart.Set_StdCtrl("Y-MIN", Strings.Trim(TxtPadR.Text));
						BuildChart.Set_StdCtrl("Y-2-MAX", Strings.Trim(TxtPadT.Text));
						BuildChart.Set_StdCtrl("Y-2-MIN", Strings.Trim(TxtPadB.Text));
						BuildChart.Set_StdCtrl("X-MAJOR-GRID", Strings.Trim(TxtXLStp.Text));
						BuildChart.Set_StdCtrl("Y-MAJOR-GRID", Strings.Trim(TxtYLStp.Text));
						BuildChart.Set_StdCtrl("FRAME-X", Strings.Trim(TxtY2LStp.Text));
						BuildChart.Set_StdCtrl("X-MINOR-GRID", Strings.Trim(TxtXTStp.Text));
						BuildChart.Set_StdCtrl("Y-MINOR-GRID", Strings.Trim(TxtYTStp.Text));
						BuildChart.Set_StdCtrl("FRAME-Y", Strings.Trim(TxtY2TStp.Text));
						BuildChart.Set_StdCtrl("X-MAJOR-TICKS", Strings.Trim(TxtXGStp.Text));
						BuildChart.Set_StdCtrl("Y-MAJOR-TICKS", Strings.Trim(TxtYGStp.Text));
						BuildChart.Set_StdCtrl("FRAME-COLOR", Strings.Trim(TxtY2GStp.Text));
						text6 = "";
						MyCK = ChkonTicX;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkonTicX = MyCK;
						BuildChart.Set_StdCtrl("X-MINOR-TICKS", text6);
						text6 = "";
						MyCK = ChkonTicY;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkonTicY = MyCK;
						BuildChart.Set_StdCtrl("Y-MINOR-TICKS", text6);
						text6 = "";
						MyCK = ChkonTicY2;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						ChkonTicY2 = MyCK;
						BuildChart.Set_StdCtrl("FRAME-MARKER", text6);
						text6 = "";
						MyCK = chklegend;
						BuildChart.Set_CK_Box(ref MyCK, ref text6);
						chklegend = MyCK;
						BuildChart.Set_StdCtrl("KEY-POS", text6);
						if (Operators.CompareString(Strings.Trim(CmbFrameX.Text), "", TextCompare: false) == 0)
						{
							CmbFrameX.Text = "500";
						}
						if (Operators.CompareString(Strings.Trim(CmbFrameY.Text), "", TextCompare: false) == 0)
						{
							CmbFrameY.Text = "500";
						}
						BuildChart.Set_StdCtrl("SIZE-X", Strings.Trim(CmbFrameX.Text));
						BuildChart.Set_StdCtrl("SIZE-Y", Strings.Trim(CmbFrameY.Text));
						BuildChart.Set_StdCtrl("PSEUDO-X", Strings.Trim(cmbXFmt.Text));
						BuildChart.Set_StdCtrl("Y-FMT", Strings.Trim(cmbYFmt.Text));
						BuildChart.Set_StdCtrl("Y2-FMT", Strings.Trim(cmbY2Fmt.Text));
						if (mnureportsource.Checked)
						{
							BuildChart.Set_StdCtrl("KEEP-UNUSED-CHARTS", "1");
						}
						else
						{
							BuildChart.Set_StdCtrl("KEEP-UNUSED-CHARTS", "0");
						}
						if (mnufilterchart.Checked && Operators.CompareString(cmbFilterChart.Text, "", TextCompare: false) != 0)
						{
							text6 = cmbFilterChart.Text;
							num5 = Strings.InStrRev(text6, "{");
							if (num5 != 0)
							{
								text6 = Strings.Mid(text6 + " ", num5 + 1);
								num5 = Strings.InStrRev(text6, "}");
								if (num5 >= 1)
								{
									text6 = Strings.Mid(text6, 1, num5 - 1);
								}
							}
							BuildChart.Set_StdCtrl("FILTER-CHARTS-PARENT", text6);
						}
						else
						{
							BuildChart.Set_StdCtrl("FILTER-CHARTS-PARENT", "");
						}
						if (flag3)
						{
							BuildChart.Set_StdCtrl("FILTER-CHARTS-CHILD", fName);
						}
						else
						{
							BuildChart.Set_StdCtrl("FILTER-CHARTS-CHILD", "");
						}
						text = "";
						TabPage tabOptions = TabOptions;
						num3 = 0;
						do
						{
							IEnumerator enumerator = tabOptions.Controls.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Control control = (Control)enumerator.Current;
								if ((control is CheckBox) & (Operators.CompareString(Strings.UCase(control.Name), "CONTROL" + Conversions.ToString(num3), TextCompare: false) == 0))
								{
									if (((CheckBox)control).Checked)
									{
										Globals_Renamed.gOptOptions[num3].Selected = "1";
									}
									else
									{
										Globals_Renamed.gOptOptions[num3].Selected = "0";
									}
									break;
								}
								if ((control is ComboBox) & (Operators.CompareString(Strings.UCase(control.Name), "CONTROL" + Conversions.ToString(num3), TextCompare: false) == 0))
								{
									Globals_Renamed.gOptOptions[num3].Selected = control.Text;
									break;
								}
							}
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
							num3++;
						}
						while (num3 <= 25);
						LastChartTable = TxtIn.Text;
						f_OK = true;
						Close();
						goto end_IL_0001;
					}
					case 5953:
						{
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
						IL_16ff:
						Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, title);
						goto end_IL_0001;
					}
					goto IL_1777;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5953;
				continue;
			}
			break;
			IL_1777:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdRemove_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int myIdx = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				ListBox LstAny;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 548:
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
						case 6:
							goto IL_0016;
						case 7:
							goto IL_001b;
						case 8:
							goto IL_0029;
						case 4:
						case 5:
						case 9:
							goto IL_004e;
						case 10:
							goto IL_0069;
						case 11:
							goto IL_0077;
						case 14:
							goto IL_0097;
						case 15:
							goto IL_009d;
						case 16:
							goto IL_00ac;
						case 12:
						case 13:
						case 17:
							goto IL_00d2;
						case 18:
							goto IL_00ee;
						case 19:
							goto IL_00fc;
						case 22:
							goto IL_011c;
						case 23:
							goto IL_0122;
						case 24:
							goto IL_0131;
						case 20:
						case 21:
						case 25:
							goto IL_0157;
						case 26:
							goto IL_0173;
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 28:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0016:
					num2 = 6;
					num5 = 0;
					goto IL_001b;
					IL_001b:
					num2 = 7;
					myIdx = LstX.SelectedIndex;
					goto IL_0029;
					IL_00ac:
					num2 = 16;
					LstBy.Items.RemoveAt(LstBy.SelectedIndices[0]);
					goto IL_00d2;
					IL_0029:
					num2 = 8;
					LstX.Items.RemoveAt(LstX.SelectedIndices[0]);
					goto IL_004e;
					IL_000b:
					num2 = 2;
					myIdx = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = -1;
					goto IL_004e;
					IL_004e:
					num2 = 5;
					if (LstX.SelectedIndices.Count > 0)
					{
						goto IL_0016;
					}
					goto IL_0069;
					IL_0069:
					num2 = 10;
					if (num5 == 0)
					{
						goto IL_0077;
					}
					goto IL_00d2;
					IL_0077:
					num2 = 11;
					LstAny = LstX;
					Set_Index_List(ref LstAny, myIdx);
					LstX = LstAny;
					goto IL_00d2;
					IL_00d2:
					num2 = 13;
					if (LstBy.SelectedIndices.Count > 0)
					{
						goto IL_0097;
					}
					goto IL_00ee;
					IL_00ee:
					num2 = 18;
					if (num5 == 2)
					{
						goto IL_00fc;
					}
					goto IL_0157;
					IL_00fc:
					num2 = 19;
					LstAny = LstBy;
					Set_Index_List(ref LstAny, myIdx);
					LstBy = LstAny;
					goto IL_0157;
					IL_0157:
					num2 = 21;
					if (LstGroup.SelectedIndices.Count > 0)
					{
						goto IL_011c;
					}
					goto IL_0173;
					IL_0173:
					num2 = 26;
					if (num5 != 3)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_011c:
					num2 = 22;
					num5 = 3;
					goto IL_0122;
					IL_0122:
					num2 = 23;
					myIdx = LstGroup.SelectedIndex;
					goto IL_0131;
					IL_0131:
					num2 = 24;
					LstGroup.Items.RemoveAt(LstGroup.SelectedIndices[0]);
					goto IL_0157;
					IL_0097:
					num2 = 14;
					num5 = 2;
					goto IL_009d;
					IL_009d:
					num2 = 15;
					myIdx = LstBy.SelectedIndex;
					goto IL_00ac;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				LstAny = LstGroup;
				Set_Index_List(ref LstAny, myIdx);
				LstGroup = LstAny;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 548;
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

	private void CmdY_Click(object sender, EventArgs e)
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
				DataGridView GridY = this.GridY;
				ListBox lstcolumns = LstColumns;
				BuildChart.Click_Y_For_Grid(ref GridY, ref lstcolumns, DoNumber: true);
				LstColumns = lstcolumns;
				this.GridY = GridY;
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

	private bool Check_Same_Var(ref ListBox DestList, string MyData)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string right = default(string);
		int num6 = default(int);
		bool result = default(bool);
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
				object obj3;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 294:
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
							goto IL_001d;
						case 6:
							goto IL_0027;
						case 7:
							goto IL_0041;
						case 8:
							goto IL_00ae;
						case 9:
							goto end_IL_0001_2;
						case 11:
						case 12:
							goto IL_00cb;
						default:
							goto end_IL_0001;
						case 10:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0041:
					num2 = 7;
					typeFromHandle = typeof(Strings);
					obj = new object[1] { (items = DestList.Items)[index = num5] };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					obj3 = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
					if (array2[0])
					{
						items[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					right = Conversions.ToString(obj3);
					goto IL_00ae;
					IL_00cb:
					num2 = 12;
					num5 = checked(num5 + 1);
					goto IL_00d4;
					IL_00d4:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0041;
					IL_00ae:
					num2 = 8;
					if (Operators.CompareString(MyData, right, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_00cb;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					right = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					result = false;
					goto IL_001d;
					IL_001d:
					num2 = 5;
					MyData = Strings.UCase(MyData);
					goto IL_0027;
					IL_0027:
					num2 = 6;
					num6 = checked(DestList.Items.Count - 1);
					num5 = 0;
					goto IL_00d4;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj4) when (obj4 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj4);
				try0001_dispatch = 294;
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

	private void CmdX_Click(object sender, EventArgs e)
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
				case 138:
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
					if (LstX.Items.Count < 1)
					{
						break;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					Interaction.MsgBox("You have exceeded the maximum allowable number of X variables for this object.", MsgBoxStyle.Exclamation, "Exceeded Maximum Allowable Variables");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				ListBox DestList = LstX;
				Add_ListBox(ref DestList);
				LstX = DestList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 138;
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

	private void TabChart_Click(object sender, EventArgs e)
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
				case 106:
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
					if (Operators.CompareString(Strings.UCase(TabChart.SelectedTab.Name), "TABTITLES", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				TxtTitle.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 106;
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

	private void FrmChart_Load(object sender, EventArgs e)
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
				TabPage TabOptions;
				ToolTip ToolTip;
				Button MyButton;
				TabPage TabCols;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 701:
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
							goto IL_0029;
						case 4:
							goto IL_0047;
						case 5:
							goto IL_0065;
						case 6:
							goto IL_0083;
						case 7:
							goto IL_00a1;
						case 8:
							goto IL_00bf;
						case 9:
							goto IL_00dd;
						case 10:
							goto IL_00fc;
						case 11:
							goto IL_011b;
						case 12:
							goto IL_013a;
						case 13:
							goto IL_0159;
						case 14:
							goto IL_0178;
						case 15:
							goto IL_018d;
						case 16:
							goto IL_019b;
						case 17:
							goto IL_01a9;
						case 18:
							goto IL_01c9;
						case 19:
							goto IL_01d3;
						case 20:
							goto IL_01e3;
						case 21:
							goto IL_01ed;
						case 22:
							goto IL_0230;
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01e3:
					num2 = 20;
					Init_Controls();
					goto IL_01ed;
					IL_01ed:
					num2 = 21;
					TabCols = this.TabCols;
					TabOptions = this.TabOptions;
					ToolTip = ToolTip1;
					BuildChart.Init_Options(-1, ref TabCols, ref TabOptions, ref ToolTip);
					ToolTip1 = ToolTip;
					this.TabOptions = TabOptions;
					this.TabCols = TabCols;
					goto IL_0230;
					IL_01d3:
					num2 = 19;
					TabChart.SelectedIndex = 0;
					goto IL_01e3;
					IL_0230:
					num2 = 22;
					Resize_Form();
					break;
					IL_000b:
					num2 = 2;
					MyButton = cmdCancel;
					BuildForm.Set_Btn_Img(ref MyButton, "cancel");
					cmdCancel = MyButton;
					goto IL_0029;
					IL_0029:
					num2 = 3;
					MyButton = cmdOK;
					BuildForm.Set_Btn_Img(ref MyButton, "ok");
					cmdOK = MyButton;
					goto IL_0047;
					IL_0047:
					num2 = 4;
					MyButton = CmdBrowseIn;
					BuildForm.Set_Btn_Img(ref MyButton, "browse");
					CmdBrowseIn = MyButton;
					goto IL_0065;
					IL_0065:
					num2 = 5;
					MyButton = CmdRemove;
					BuildForm.Set_Btn_Img(ref MyButton, "clear");
					CmdRemove = MyButton;
					goto IL_0083;
					IL_0083:
					num2 = 6;
					MyButton = cmdClear;
					BuildForm.Set_Btn_Img(ref MyButton, "clear");
					cmdClear = MyButton;
					goto IL_00a1;
					IL_00a1:
					num2 = 7;
					MyButton = CmdUp;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp = MyButton;
					goto IL_00bf;
					IL_00bf:
					num2 = 8;
					MyButton = cmdDown;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					cmdDown = MyButton;
					goto IL_00dd;
					IL_00dd:
					num2 = 9;
					MyButton = cmdDelete;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					cmdDelete = MyButton;
					goto IL_00fc;
					IL_00fc:
					num2 = 10;
					MyButton = cmdUp1;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					cmdUp1 = MyButton;
					goto IL_011b;
					IL_011b:
					num2 = 11;
					MyButton = cmdDown1;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					cmdDown1 = MyButton;
					goto IL_013a;
					IL_013a:
					num2 = 12;
					MyButton = cmdDelete1;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					cmdDelete1 = MyButton;
					goto IL_0159;
					IL_0159:
					num2 = 13;
					MyButton = cmdRefresh;
					BuildForm.Set_Btn_Img(ref MyButton, "refresh");
					cmdRefresh = MyButton;
					goto IL_0178;
					IL_0178:
					num2 = 14;
					GridWhere.RowCount = f_WRows;
					goto IL_018d;
					IL_018d:
					num2 = 15;
					f_MyPre = "";
					goto IL_019b;
					IL_019b:
					num2 = 16;
					f_MyPost = "";
					goto IL_01a9;
					IL_01a9:
					num2 = 17;
					TabCols = this.TabOptions;
					BuildChart.Add_Labels_To_Options(ref TabCols, 2, 6, 18);
					this.TabOptions = TabCols;
					goto IL_01c9;
					IL_01c9:
					num2 = 18;
					Clear_Controls();
					goto IL_01d3;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 701;
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

	private void cmdClear_Click(object sender, EventArgs e)
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
				case 255:
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
							goto IL_0053;
						case 5:
							goto IL_0067;
						case 6:
							goto IL_0073;
						case 7:
							goto IL_0086;
						case 8:
							goto IL_0099;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0053:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear all X, Group and By selections?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Clear All Column Selections?");
					goto IL_0067;
					IL_0067:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0073;
					IL_0099:
					num2 = 8;
					LstGroup.Items.Clear();
					break;
					IL_0073:
					num2 = 6;
					LstX.Items.Clear();
					goto IL_0086;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (LstX.Items.Count <= 0 && LstBy.Items.Count <= 0 && LstGroup.Items.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0053;
					IL_0086:
					num2 = 7;
					LstBy.Items.Clear();
					goto IL_0099;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Clear_Where_Grid();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 255;
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

	private void LstX_DragDrop(object sender, DragEventArgs e)
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
				case 84:
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
				ListBox MyListBox = LstX;
				GridModule.List_Drag_Drop("DD", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstX = MyListBox;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 84;
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

	private void LstX_DragOver(object sender, DragEventArgs e)
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
				case 84:
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
				ListBox MyListBox = LstX;
				GridModule.List_Drag_Drop("DO", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstX = MyListBox;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 84;
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

	private void LstX_MouseDown(object sender, MouseEventArgs e)
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
				case 84:
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
				ListBox MyListBox = LstX;
				GridModule.List_Drag_Drop("MD", ref MyListBox, ref l_DragSource, ref l_DragIdx);
				LstX = MyListBox;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 84;
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
				DataGridView MyGrid = GridY;
				GridModule.Grid_Up(ref MyGrid);
				GridY = MyGrid;
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
				DataGridView MyGrid = GridY;
				GridModule.Grid_Down(ref MyGrid);
				GridY = MyGrid;
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
				case 95:
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
					MyGrid = GridY;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					GridY = MyGrid;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				MyGrid = GridY;
				GridModule.Number_Grid(ref MyGrid);
				GridY = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 95;
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

	private void CmdBrowseIn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string left = default(string);
		bool lClearData = default(bool);
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
				case 370:
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
							goto IL_0031;
						case 7:
							goto IL_0036;
						case 8:
							goto IL_004c;
						case 10:
							goto IL_0061;
						case 11:
							goto IL_007f;
						case 12:
							goto IL_0097;
						case 13:
							goto IL_00af;
						case 14:
							goto IL_00c8;
						case 15:
							goto IL_00e1;
						case 16:
							goto IL_00ef;
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 19:
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c8:
					num2 = 14;
					num5 = (int)Interaction.MsgBox("Do you wish to clear selections before refreshing the form [No]?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear Prior Selections?");
					goto IL_00e1;
					IL_00e1:
					num2 = 15;
					if (num5 != 6)
					{
						break;
					}
					goto IL_00ef;
					IL_00af:
					num2 = 13;
					if (Operators.CompareString(left, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_00c8;
					IL_00ef:
					num2 = 16;
					lClearData = true;
					break;
					IL_000b:
					num2 = 2;
					text = Strings.Trim(TxtIn.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					left = "";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					num5 = 0;
					goto IL_002c;
					IL_002c:
					num2 = 5;
					lClearData = false;
					goto IL_0031;
					IL_0031:
					num2 = 6;
					left = text;
					goto IL_0036;
					IL_0036:
					num2 = 7;
					if (chkHTTP.CheckState == CheckState.Checked)
					{
						goto IL_004c;
					}
					goto IL_0061;
					IL_004c:
					num2 = 8;
					BuildForm.Get_HTTP_Help(text, "Batch_Options.htm#retention");
					goto end_IL_0001_3;
					IL_0061:
					num2 = 10;
					BuildForm.FileOpenSave("O", text, "csv+", "Find CSV File", "");
					goto IL_007f;
					IL_007f:
					num2 = 11;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0097;
					IL_0097:
					num2 = 12;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00af;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				Populate_Columns3(text, 0, LoadOptCols: true, lClearData);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 370;
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

	private void Process_Mouse_KB(int X, int Y)
	{
		int columnIndex = GridY.CurrentCell.ColumnIndex;
		int rowIndex = GridY.CurrentCell.RowIndex;
		if (columnIndex == -1 || rowIndex == -1)
		{
			return;
		}
		checked
		{
			switch (columnIndex)
			{
			case 6:
			case 7:
			{
				if (X == -1 && Y == -1)
				{
					X = (int)Math.Round((double)GridY.Width / 2.0);
					Y = (int)Math.Round((double)GridY.Height / 2.0);
				}
				Point position = new Point(X, Y);
				mnuexpr.Visible = false;
				mnucolorclear.Visible = true;
				mnucolorset.Visible = true;
				ContextGridY.Show(GridY, position);
				break;
			}
			case 0:
			{
				if (X == -1 && Y == -1)
				{
					X = 60;
					Y = (int)Math.Round((double)GridY.Height / 2.0);
				}
				Point position2 = new Point(X, Y);
				mnuexpr.Visible = true;
				mnucolorclear.Visible = false;
				mnucolorset.Visible = false;
				ContextGridY.Show(GridY, position2);
				break;
			}
			case 14:
			case 16:
			{
				FrmJS frmJS = new FrmJS();
				if (GridY.Rows[rowIndex].Cells[columnIndex].Value == null)
				{
					GridY.Rows[rowIndex].Cells[columnIndex].Value = "";
				}
				frmJS.fData = Conversions.ToString(GridY.Rows[rowIndex].Cells[columnIndex].Value);
				if (columnIndex == 14)
				{
					frmJS.fType = "TOOLTIP";
				}
				else
				{
					frmJS.fType = "LEGEND";
				}
				frmJS.ShowDialog();
				GridY.Rows[rowIndex].Cells[columnIndex].Value = frmJS.fData;
				frmJS.Dispose();
				break;
			}
			}
		}
	}

	private void GridY_MouseDoubleClick(object sender, MouseEventArgs e)
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
				case 62:
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
				Process_Mouse_KB(e.X, e.Y);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 62;
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

	private void cmdGroup_Click(object sender, EventArgs e)
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
				case 138:
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
					if (LstGroup.Items.Count < 1)
					{
						break;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					Interaction.MsgBox("You have exceeded the maximum allowable number of Group variables for this object.", MsgBoxStyle.Exclamation, "Exceeded Maximum Allowable Variables");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				ListBox DestList = LstGroup;
				Add_ListBox(ref DestList);
				LstGroup = DestList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 138;
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

	private void cmdBy_Click(object sender, EventArgs e)
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
				case 138:
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
					if (LstBy.Items.Count < 1)
					{
						break;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					Interaction.MsgBox("You have exceeded the maximum allowable number of By variables for this object.", MsgBoxStyle.Exclamation, "Exceeded Maximum Allowable Variables");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				ListBox DestList = LstBy;
				Add_ListBox(ref DestList);
				LstBy = DestList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 138;
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

	private void cmdUp1_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridWhere;
				GridModule.Grid_Up(ref MyGrid);
				GridWhere = MyGrid;
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

	private void cmdDown1_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridWhere;
				GridModule.Grid_Down(ref MyGrid);
				GridWhere = MyGrid;
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

	private void cmdDelete1_Click(object sender, EventArgs e)
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
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					MyGrid = GridWhere;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					GridWhere = MyGrid;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				GridWhere.RowCount = f_WRows;
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

	private void cmdRefresh_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		bool lClearData = default(bool);
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
				case 220:
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
							goto IL_0035;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0052;
						case 8:
							goto IL_0057;
						case 9:
							goto IL_006f;
						case 10:
							goto IL_007d;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0057:
					num2 = 8;
					num5 = (int)Interaction.MsgBox("Do you wish to clear selections before refreshing the form [No]?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear Prior Selections?");
					goto IL_006f;
					IL_006f:
					num2 = 9;
					if (num5 != 6)
					{
						break;
					}
					goto IL_007d;
					IL_0052:
					num2 = 7;
					lClearData = false;
					goto IL_0057;
					IL_007d:
					num2 = 10;
					lClearData = true;
					break;
					IL_000b:
					num2 = 2;
					text = Strings.Trim(TxtIn.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0035;
					}
					goto IL_004d;
					IL_0035:
					num2 = 4;
					Interaction.MsgBox("No file specified for the refresh.", MsgBoxStyle.Information, "Nothing to do");
					goto end_IL_0001_3;
					IL_004d:
					num2 = 6;
					num5 = 0;
					goto IL_0052;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Populate_Columns3(text, 0, LoadOptCols: true, lClearData);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 220;
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

	private void mnucolorset_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
							goto IL_001e;
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
					columnIndex = this.GridY.CurrentCell.ColumnIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (columnIndex != 6 && columnIndex != 7)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView GridY = this.GridY;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Chart_Set_Color(ref GridY, ref ColorDialog, columnIndex, columnIndex);
				ColorDialog1 = ColorDialog;
				this.GridY = GridY;
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

	private void mnucolorclear_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
							goto IL_000b;
						case 3:
							goto IL_001e;
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
					columnIndex = this.GridY.CurrentCell.ColumnIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (columnIndex != 6 && columnIndex != 7)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView GridY = this.GridY;
				BuildChart.Chart_Clear_Color(ref GridY, columnIndex, columnIndex);
				this.GridY = GridY;
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

	private void cmdAddR_Click(object sender, EventArgs e)
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
				case 97:
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
					GridY.RowCount = checked(GridY.RowCount + 1);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				DataGridView MyGrid = GridY;
				GridModule.Number_Grid(ref MyGrid);
				GridY = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 97;
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

	private void mnuexpr_Click(object sender, EventArgs e)
	{
		string text = "";
		string text2 = "";
		int num = 0;
		text = Conversions.ToString(GridY.CurrentCell.Value);
		text2 = Conversions.ToString(GridY.Rows[GridY.CurrentCell.RowIndex].Cells[2].Value);
		num = ((!mnureportsource.Checked) ? 11 : 11);
		MyProject.Forms.FrmMain.LoadComputedColumnForm(checked((short)num), text2, text, "C", TxtIn.Text, -1);
		GridY.Rows[GridY.CurrentCell.RowIndex].Cells[0].Value = Globals_Renamed.currDataAny;
		if (Operators.CompareString(Globals_Renamed.currtxttmp, "Expr", TextCompare: false) == 0)
		{
			GridY.Rows[GridY.CurrentCell.RowIndex].Cells[2].Value = "Expr";
		}
		else
		{
			GridY.Rows[GridY.CurrentCell.RowIndex].Cells[2].Value = "";
		}
		cmdOK.Focus();
		GridY.Refresh();
		GridY.Focus();
		Globals_Renamed.currtxttmp = "";
		Globals_Renamed.currDataAny = "";
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 337:
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
								goto IL_0019;
							case 4:
								goto IL_0044;
							case 5:
								goto IL_006f;
							case 6:
								goto IL_009a;
							case 7:
								goto IL_00c5;
							case 8:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 9:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_006f:
						num2 = 5;
						GridWhere.Columns[2].Width = (int)Math.Round(0.27 * (double)num5);
						goto IL_009a;
						IL_009a:
						num2 = 6;
						GridWhere.Columns[3].Width = (int)Math.Round(0.12 * (double)num5);
						goto IL_00c5;
						IL_0044:
						num2 = 4;
						GridWhere.Columns[1].Width = (int)Math.Round(0.08 * (double)num5);
						goto IL_006f;
						IL_00c5:
						num2 = 7;
						GridWhere.Columns[4].Width = (int)Math.Round(0.265 * (double)num5);
						break;
						IL_000b:
						num2 = 2;
						num5 = GridWhere.Width;
						goto IL_0019;
						IL_0019:
						num2 = 3;
						GridWhere.Columns[0].Width = (int)Math.Round(0.09 * (double)num5);
						goto IL_0044;
						end_IL_0001_2:
						break;
					}
					num2 = 8;
					GridWhere.Columns[5].Width = (int)Math.Round(0.08 * (double)num5);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 337;
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

	private void FrmJSChart_Resize(object sender, EventArgs e)
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

	private void cmbFilterChart_SelectedIndexChanged(object sender, EventArgs e)
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
							goto IL_002a;
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
					if (Operators.CompareString(cmbFilterChart.Text, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_002a;
					IL_002a:
					num2 = 3;
					mnufilterchart.Checked = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				mnufilterchart.Checked = false;
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

	private void mnuRange_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmJS frmJS = default(FrmJS);
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
				case 128:
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
							goto IL_0021;
						case 5:
							goto IL_002e;
						case 6:
							goto IL_0037;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0021:
					num2 = 4;
					frmJS.fType = "RANGE";
					goto IL_002e;
					IL_002e:
					num2 = 5;
					frmJS.ShowDialog();
					goto IL_0037;
					IL_0013:
					num2 = 3;
					frmJS.fData = fRange;
					goto IL_0021;
					IL_0037:
					num2 = 6;
					fRange = frmJS.fData;
					break;
					IL_000b:
					num2 = 2;
					frmJS = new FrmJS();
					goto IL_0013;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				frmJS.Dispose();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 128;
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

	private void cmdBackCOlor_Click(object sender, EventArgs e)
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
				case 282:
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
						case 5:
							goto IL_004a;
						case 6:
							goto IL_0059;
						case 7:
							goto IL_006f;
						case 8:
							goto IL_00b0;
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
					IL_0059:
					num2 = 6;
					if (ColorDialog1.ShowDialog() != DialogResult.OK)
					{
						break;
					}
					goto IL_006f;
					IL_006f:
					num2 = 7;
					TxtBackColor.Text = "#" + ColorDialog1.Color.ToArgb().ToString("X8").Substring(2, 6);
					goto IL_00b0;
					IL_004a:
					num2 = 5;
					ColorDialog1.FullOpen = true;
					goto IL_0059;
					IL_00b0:
					num2 = 8;
					TxtBackColor.BackColor = ColorDialog1.Color;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(TxtBackColor.Text, "", TextCompare: false) != 0)
					{
						goto IL_002a;
					}
					goto IL_004a;
					IL_002a:
					num2 = 3;
					ColorDialog1.Color = ColorTranslator.FromHtml(TxtBackColor.Text);
					goto IL_004a;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 282;
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

	private void LstX_GotFocus(object sender, EventArgs e)
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
				case 73:
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
					LstBy.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				LstGroup.ClearSelected();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 73;
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

	private void LstBy_GotFocus(object sender, EventArgs e)
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
				case 73:
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
					LstX.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				LstGroup.ClearSelected();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 73;
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

	private void LstGroup_GotFocus(object sender, EventArgs e)
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
				case 73:
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
					LstX.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				LstBy.ClearSelected();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 73;
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

	private void GridWhere_KeyDown(object sender, KeyEventArgs e)
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
					cmdDelete1_Click(cmdDelete1, new EventArgs());
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

	private void GridY_KeyDown(object sender, KeyEventArgs e)
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
				case 233:
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
							goto IL_0064;
						case 10:
							goto IL_0071;
						case 11:
							goto IL_0084;
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
					IL_0064:
					num2 = 8;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0071:
					num2 = 10;
					if (e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0084;
					IL_0050:
					num2 = 7;
					cmdDelete_Click(cmdDelete, new EventArgs());
					goto IL_0064;
					IL_0084:
					num2 = 11;
					Process_Mouse_KB(-1, -1);
					break;
					IL_000b:
					num2 = 2;
					if (e.KeyValue == 117)
					{
						goto IL_001b;
					}
					goto IL_003e;
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
					IL_003e:
					num2 = 6;
					if (e.KeyValue == 46)
					{
						goto IL_0050;
					}
					goto IL_0071;
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
				try0001_dispatch = 233;
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

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
public class FrmGnuChart : Form
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
	[AccessedThroughProperty("mnuSetColor")]
	private ToolStripMenuItem _mnuSetColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearColor")]
	private ToolStripMenuItem _mnuClearColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuX")]
	private ToolStripMenuItem _mnuX;

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

	private string f_MyPre;

	private string f_MyPost;

	private const int l_NoOptsTabcols = 1;

	public string LastChartTable;

	public bool f_OK;

	private string f_Y_Map;

	private string l_DragSource;

	private int l_DragIdx;

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

	[field: AccessedThroughProperty("LblFormTitle")]
	internal virtual Label LblFormTitle
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
			ListBox listBox = _LstX;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragOver -= value3;
				listBox.MouseDown -= value4;
			}
			_LstX = value;
			listBox = _LstX;
			if (listBox != null)
			{
				listBox.DragDrop += value2;
				listBox.DragOver += value3;
				listBox.MouseDown += value4;
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

	[field: AccessedThroughProperty("GrpGrid")]
	internal virtual GroupBox GrpGrid
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

	[field: AccessedThroughProperty("ChkKeyBox")]
	internal virtual CheckBox ChkKeyBox
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
			KeyEventHandler value2 = GridY_KeyDown;
			MouseEventHandler value3 = GridY_MouseDoubleClick;
			DataGridView dataGridView = _GridY;
			if (dataGridView != null)
			{
				dataGridView.KeyDown -= value2;
				dataGridView.MouseDoubleClick -= value3;
			}
			_GridY = value;
			dataGridView = _GridY;
			if (dataGridView != null)
			{
				dataGridView.KeyDown += value2;
				dataGridView.MouseDoubleClick += value3;
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

	[field: AccessedThroughProperty("LblLegendPos")]
	internal virtual Label LblLegendPos
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbLegendPos")]
	internal virtual ComboBox cmbLegendPos
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblOrient")]
	internal virtual Label LblOrient
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbKeyOrient")]
	internal virtual ComboBox CmbKeyOrient
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblGridBF")]
	internal virtual Label lblGridBF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbGridBF")]
	internal virtual ComboBox CmbGridBF
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

	[field: AccessedThroughProperty("CmbKeyWidth")]
	internal virtual ComboBox CmbKeyWidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblKeyWidth")]
	internal virtual Label LblKeyWidth
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

	[field: AccessedThroughProperty("ContextMenuColEdit")]
	internal virtual ContextMenuStrip ContextMenuColEdit
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSetColor
	{
		[CompilerGenerated]
		get
		{
			return _mnuSetColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSetColor_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSetColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSetColor = value;
			toolStripMenuItem = _mnuSetColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuClearColor
	{
		[CompilerGenerated]
		get
		{
			return _mnuClearColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClearColor_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClearColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClearColor = value;
			toolStripMenuItem = _mnuClearColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbFont")]
	internal virtual ComboBox CmbFont
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

	[field: AccessedThroughProperty("TxtXIncr")]
	internal virtual TextBox TxtXIncr
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

	[field: AccessedThroughProperty("ColColor")]
	internal virtual DataGridViewTextBoxColumn ColColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ContextMenuCols")]
	internal virtual ContextMenuStrip ContextMenuCols
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

	[field: AccessedThroughProperty("ContextMenuY")]
	internal virtual ContextMenuStrip ContextMenuY
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

	public FrmGnuChart()
	{
		base.Load += FrmChart_Load;
		f_TemplateType2 = "";
		f_MyPre = "";
		f_MyPost = "";
		LastChartTable = "";
		f_OK = false;
		f_Y_Map = "";
		l_DragSource = "";
		l_DragIdx = -1;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmGnuChart));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.CmdRemove = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.CmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdDelete = new System.Windows.Forms.Button();
		this.CmdBrowseIn = new System.Windows.Forms.Button();
		this.CmbXRotate = new System.Windows.Forms.ComboBox();
		this.ChkLogY = new System.Windows.Forms.CheckBox();
		this.ChkLogX = new System.Windows.Forms.CheckBox();
		this.TxtXLow = new System.Windows.Forms.TextBox();
		this.TxtY2High = new System.Windows.Forms.TextBox();
		this.TxtXHigh = new System.Windows.Forms.TextBox();
		this.TxtY2Low = new System.Windows.Forms.TextBox();
		this.TxtYHigh = new System.Windows.Forms.TextBox();
		this.TxtYLow = new System.Windows.Forms.TextBox();
		this.CmbGridBF = new System.Windows.Forms.ComboBox();
		this.cmbLegendPos = new System.Windows.Forms.ComboBox();
		this.ChkGridY = new System.Windows.Forms.CheckBox();
		this.ChkKeyBox = new System.Windows.Forms.CheckBox();
		this.ChkGridX = new System.Windows.Forms.CheckBox();
		this.CmbKeyWidth = new System.Windows.Forms.ComboBox();
		this.CmbFrameY = new System.Windows.Forms.ComboBox();
		this.CmbFrameX = new System.Windows.Forms.ComboBox();
		this.TxtXIncr = new System.Windows.Forms.TextBox();
		this.TxtYIncr = new System.Windows.Forms.TextBox();
		this.TxtY2Incr = new System.Windows.Forms.TextBox();
		this.CmdY = new System.Windows.Forms.Button();
		this.CmdX = new System.Windows.Forms.Button();
		this.TabOptions = new System.Windows.Forms.TabPage();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label7 = new System.Windows.Forms.Label();
		this.Label31 = new System.Windows.Forms.Label();
		this.Label30 = new System.Windows.Forms.Label();
		this.TabTitles = new System.Windows.Forms.TabPage();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.LblInc = new System.Windows.Forms.Label();
		this.LblRotate = new System.Windows.Forms.Label();
		this.LblLog = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.LblXAxis = new System.Windows.Forms.Label();
		this.LblYAxis = new System.Windows.Forms.Label();
		this.LblLow = new System.Windows.Forms.Label();
		this.LblHigh = new System.Windows.Forms.Label();
		this.GrpTitle = new System.Windows.Forms.GroupBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.CmbFont = new System.Windows.Forms.ComboBox();
		this.Label5 = new System.Windows.Forms.Label();
		this.LblXSize = new System.Windows.Forms.Label();
		this.TxtY2Axis = new System.Windows.Forms.TextBox();
		this.TxtYAxis = new System.Windows.Forms.TextBox();
		this.TxtXAxis = new System.Windows.Forms.TextBox();
		this.TxtTitle = new System.Windows.Forms.TextBox();
		this.LblY2 = new System.Windows.Forms.Label();
		this.LblY = new System.Windows.Forms.Label();
		this.LblX = new System.Windows.Forms.Label();
		this.LblTitle = new System.Windows.Forms.Label();
		this.GrpGrid = new System.Windows.Forms.GroupBox();
		this.LblKeyWidth = new System.Windows.Forms.Label();
		this.lblGridBF = new System.Windows.Forms.Label();
		this.LblOrient = new System.Windows.Forms.Label();
		this.CmbKeyOrient = new System.Windows.Forms.ComboBox();
		this.LblLegendPos = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.TabCols = new System.Windows.Forms.TabPage();
		this.TxtIn = new System.Windows.Forms.TextBox();
		this.GridY = new System.Windows.Forms.DataGridView();
		this.Columns = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColHdr = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColSumm = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAxis = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColLW = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.LstX = new System.Windows.Forms.ListBox();
		this.ContextMenuY = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
		this.LblCols = new System.Windows.Forms.Label();
		this.LstColumns = new System.Windows.Forms.ListBox();
		this.ContextMenuCols = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuX = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuY = new System.Windows.Forms.ToolStripMenuItem();
		this.LblInFile = new System.Windows.Forms.Label();
		this.LblFormTitle = new System.Windows.Forms.Label();
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
		this.ContextMenuColEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuSetColor = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClearColor = new System.Windows.Forms.ToolStripMenuItem();
		this.TabTitles.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		this.GrpTitle.SuspendLayout();
		this.GrpGrid.SuspendLayout();
		this.TabCols.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridY).BeginInit();
		this.ContextMenuY.SuspendLayout();
		this.ContextMenuCols.SuspendLayout();
		this.TabChart.SuspendLayout();
		this.ContextMenuColEdit.SuspendLayout();
		base.SuspendLayout();
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdOK.ImageIndex = 3;
		this.cmdOK.Location = new System.Drawing.Point(653, 4);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(77, 50);
		this.cmdOK.TabIndex = 26;
		this.cmdOK.Text = "OK";
		this.cmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdOK, "Commit Changes");
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdCancel.ImageIndex = 0;
		this.cmdCancel.Location = new System.Drawing.Point(744, 4);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(77, 50);
		this.cmdCancel.TabIndex = 27;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdCancel, "Cancel changes");
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.CmdRemove.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdRemove.ImageIndex = 1;
		this.CmdRemove.Location = new System.Drawing.Point(721, 119);
		this.CmdRemove.Margin = new System.Windows.Forms.Padding(4);
		this.CmdRemove.Name = "CmdRemove";
		this.CmdRemove.Size = new System.Drawing.Size(77, 50);
		this.CmdRemove.TabIndex = 7;
		this.CmdRemove.Text = "Remove";
		this.CmdRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdRemove, "Remove the selected columns");
		this.CmdRemove.UseVisualStyleBackColor = true;
		this.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdClear.ImageIndex = 1;
		this.cmdClear.Location = new System.Drawing.Point(721, 174);
		this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(77, 50);
		this.cmdClear.TabIndex = 8;
		this.cmdClear.Text = "Clear";
		this.cmdClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear all column selections");
		this.cmdClear.UseVisualStyleBackColor = true;
		this.CmdUp.ImageIndex = 5;
		this.CmdUp.Location = new System.Drawing.Point(721, 366);
		this.CmdUp.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp.Name = "CmdUp";
		this.CmdUp.Size = new System.Drawing.Size(77, 32);
		this.CmdUp.TabIndex = 10;
		this.ToolTip1.SetToolTip(this.CmdUp, "Move Up");
		this.CmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.ImageIndex = 6;
		this.cmdDown.Location = new System.Drawing.Point(721, 405);
		this.cmdDown.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(77, 32);
		this.cmdDown.TabIndex = 11;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdDelete.ImageIndex = 7;
		this.cmdDelete.Location = new System.Drawing.Point(723, 444);
		this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.Size = new System.Drawing.Size(77, 32);
		this.cmdDelete.TabIndex = 12;
		this.ToolTip1.SetToolTip(this.cmdDelete, "Delete Row");
		this.cmdDelete.UseVisualStyleBackColor = true;
		this.CmdBrowseIn.ImageIndex = 8;
		this.CmdBrowseIn.Location = new System.Drawing.Point(716, 30);
		this.CmdBrowseIn.Margin = new System.Windows.Forms.Padding(4);
		this.CmdBrowseIn.Name = "CmdBrowseIn";
		this.CmdBrowseIn.Size = new System.Drawing.Size(83, 33);
		this.CmdBrowseIn.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.CmdBrowseIn, "Locate CSV File");
		this.CmdBrowseIn.UseVisualStyleBackColor = true;
		this.CmbXRotate.FormattingEnabled = true;
		this.CmbXRotate.Items.AddRange(new object[8] { "", "0", "-30", "-45", "-90", "45 offset character -4.0, -1.5", "45 offset character -0.8, -1.8", "30 offset character -4.0, -1.5" });
		this.CmbXRotate.Location = new System.Drawing.Point(580, 39);
		this.CmbXRotate.Margin = new System.Windows.Forms.Padding(4);
		this.CmbXRotate.Name = "CmbXRotate";
		this.CmbXRotate.Size = new System.Drawing.Size(160, 24);
		this.CmbXRotate.TabIndex = 18;
		this.ToolTip1.SetToolTip(this.CmbXRotate, "Rotate X-labels (or tic-marks)");
		this.ChkLogY.AutoSize = true;
		this.ChkLogY.Location = new System.Drawing.Point(477, 76);
		this.ChkLogY.Margin = new System.Windows.Forms.Padding(4);
		this.ChkLogY.Name = "ChkLogY";
		this.ChkLogY.Size = new System.Drawing.Size(39, 21);
		this.ChkLogY.TabIndex = 22;
		this.ChkLogY.Text = "Y";
		this.ToolTip1.SetToolTip(this.ChkLogY, "Use a Log Scale for the Y-Axis");
		this.ChkLogY.UseVisualStyleBackColor = true;
		this.ChkLogX.AutoSize = true;
		this.ChkLogX.Location = new System.Drawing.Point(477, 39);
		this.ChkLogX.Margin = new System.Windows.Forms.Padding(4);
		this.ChkLogX.Name = "ChkLogX";
		this.ChkLogX.Size = new System.Drawing.Size(39, 21);
		this.ChkLogX.TabIndex = 17;
		this.ChkLogX.Text = "X";
		this.ToolTip1.SetToolTip(this.ChkLogX, "Use a Log Scale for the X-Axis");
		this.ChkLogX.UseVisualStyleBackColor = true;
		this.TxtXLow.Location = new System.Drawing.Point(157, 39);
		this.TxtXLow.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXLow.Name = "TxtXLow";
		this.TxtXLow.Size = new System.Drawing.Size(57, 22);
		this.TxtXLow.TabIndex = 14;
		this.TxtXLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtXLow, "Enter lower X-Range");
		this.TxtY2High.Location = new System.Drawing.Point(263, 116);
		this.TxtY2High.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2High.Name = "TxtY2High";
		this.TxtY2High.Size = new System.Drawing.Size(57, 22);
		this.TxtY2High.TabIndex = 24;
		this.TxtY2High.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtY2High, "Enter higher range for 2nd Y-Axis");
		this.TxtXHigh.Location = new System.Drawing.Point(263, 39);
		this.TxtXHigh.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXHigh.Name = "TxtXHigh";
		this.TxtXHigh.Size = new System.Drawing.Size(57, 22);
		this.TxtXHigh.TabIndex = 15;
		this.TxtXHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtXHigh, "Enter higher Y-Range");
		this.TxtY2Low.Location = new System.Drawing.Point(157, 117);
		this.TxtY2Low.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Low.Name = "TxtY2Low";
		this.TxtY2Low.Size = new System.Drawing.Size(57, 22);
		this.TxtY2Low.TabIndex = 23;
		this.TxtY2Low.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtY2Low, "Enter lower range for 2nd Y-Axis");
		this.TxtYHigh.Location = new System.Drawing.Point(263, 76);
		this.TxtYHigh.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYHigh.Name = "TxtYHigh";
		this.TxtYHigh.Size = new System.Drawing.Size(57, 22);
		this.TxtYHigh.TabIndex = 20;
		this.TxtYHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtYHigh, "Enter higher Y-Range");
		this.TxtYLow.Location = new System.Drawing.Point(157, 76);
		this.TxtYLow.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYLow.Name = "TxtYLow";
		this.TxtYLow.Size = new System.Drawing.Size(57, 22);
		this.TxtYLow.TabIndex = 19;
		this.TxtYLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.TxtYLow, "Enter lower Y-Range");
		this.CmbGridBF.FormattingEnabled = true;
		this.CmbGridBF.Items.AddRange(new object[3] { "", "back", "front" });
		this.CmbGridBF.Location = new System.Drawing.Point(11, 139);
		this.CmbGridBF.Margin = new System.Windows.Forms.Padding(4);
		this.CmbGridBF.Name = "CmbGridBF";
		this.CmbGridBF.Size = new System.Drawing.Size(165, 25);
		this.CmbGridBF.TabIndex = 9;
		this.ToolTip1.SetToolTip(this.CmbGridBF, "Place Grid Lines behind or in front of plot");
		this.cmbLegendPos.FormattingEnabled = true;
		this.cmbLegendPos.Items.AddRange(new object[15]
		{
			"", "default", "top left", "bottom left", "center left", "top right", "bottom right", "center right", "outside center right", "outside top right",
			"outside top center", "outside bottom center", "outside bottom left", "outside center left", "outside top left"
		});
		this.cmbLegendPos.Location = new System.Drawing.Point(364, 144);
		this.cmbLegendPos.Margin = new System.Windows.Forms.Padding(4);
		this.cmbLegendPos.Name = "cmbLegendPos";
		this.cmbLegendPos.Size = new System.Drawing.Size(280, 25);
		this.cmbLegendPos.TabIndex = 13;
		this.cmbLegendPos.Text = "top right";
		this.ToolTip1.SetToolTip(this.cmbLegendPos, "Specify the position of the Legend or Key");
		this.ChkGridY.AutoSize = true;
		this.ChkGridY.Location = new System.Drawing.Point(15, 86);
		this.ChkGridY.Margin = new System.Windows.Forms.Padding(4);
		this.ChkGridY.Name = "ChkGridY";
		this.ChkGridY.Size = new System.Drawing.Size(70, 21);
		this.ChkGridY.TabIndex = 8;
		this.ChkGridY.Text = "Y Grid";
		this.ToolTip1.SetToolTip(this.ChkGridY, "Add Y Grid Lines");
		this.ChkGridY.UseVisualStyleBackColor = true;
		this.ChkKeyBox.AutoSize = true;
		this.ChkKeyBox.Location = new System.Drawing.Point(263, 52);
		this.ChkKeyBox.Margin = new System.Windows.Forms.Padding(4);
		this.ChkKeyBox.Name = "ChkKeyBox";
		this.ChkKeyBox.Size = new System.Drawing.Size(53, 21);
		this.ChkKeyBox.TabIndex = 10;
		this.ChkKeyBox.Text = "Box";
		this.ToolTip1.SetToolTip(this.ChkKeyBox, "Add a box around the legend");
		this.ChkKeyBox.UseVisualStyleBackColor = true;
		this.ChkGridX.AutoSize = true;
		this.ChkGridX.Location = new System.Drawing.Point(15, 52);
		this.ChkGridX.Margin = new System.Windows.Forms.Padding(4);
		this.ChkGridX.Name = "ChkGridX";
		this.ChkGridX.Size = new System.Drawing.Size(70, 21);
		this.ChkGridX.TabIndex = 7;
		this.ChkGridX.Text = "X Grid";
		this.ToolTip1.SetToolTip(this.ChkGridX, "Add X Grid Lines");
		this.ChkGridX.UseVisualStyleBackColor = true;
		this.CmbKeyWidth.FormattingEnabled = true;
		this.CmbKeyWidth.Items.AddRange(new object[11]
		{
			"", "-1", "-2", "-3", "-4", "-5", "-7", "-9", "-10", "-15",
			"-25"
		});
		this.CmbKeyWidth.Location = new System.Drawing.Point(432, 48);
		this.CmbKeyWidth.Margin = new System.Windows.Forms.Padding(4);
		this.CmbKeyWidth.Name = "CmbKeyWidth";
		this.CmbKeyWidth.Size = new System.Drawing.Size(207, 25);
		this.CmbKeyWidth.TabIndex = 11;
		this.ToolTip1.SetToolTip(this.CmbKeyWidth, "Shrink the legend width by specifying negative numbers. Grow it by specifying positive numbers");
		this.CmbFrameY.FormattingEnabled = true;
		this.CmbFrameY.Items.AddRange(new object[2] { "", "800" });
		this.CmbFrameY.Location = new System.Drawing.Point(649, 57);
		this.CmbFrameY.Margin = new System.Windows.Forms.Padding(4);
		this.CmbFrameY.Name = "CmbFrameY";
		this.CmbFrameY.Size = new System.Drawing.Size(101, 24);
		this.CmbFrameY.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.CmbFrameY, "Set vertical width of plot");
		this.CmbFrameX.FormattingEnabled = true;
		this.CmbFrameX.Items.AddRange(new object[2] { "", "1000" });
		this.CmbFrameX.Location = new System.Drawing.Point(649, 23);
		this.CmbFrameX.Margin = new System.Windows.Forms.Padding(4);
		this.CmbFrameX.Name = "CmbFrameX";
		this.CmbFrameX.Size = new System.Drawing.Size(101, 24);
		this.CmbFrameX.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.CmbFrameX, "Set horizontal width of plot");
		this.TxtXIncr.Location = new System.Drawing.Point(368, 39);
		this.TxtXIncr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXIncr.Name = "TxtXIncr";
		this.TxtXIncr.Size = new System.Drawing.Size(57, 22);
		this.TxtXIncr.TabIndex = 16;
		this.ToolTip1.SetToolTip(this.TxtXIncr, "Enter X Increment");
		this.TxtYIncr.Location = new System.Drawing.Point(368, 76);
		this.TxtYIncr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYIncr.Name = "TxtYIncr";
		this.TxtYIncr.Size = new System.Drawing.Size(57, 22);
		this.TxtYIncr.TabIndex = 21;
		this.ToolTip1.SetToolTip(this.TxtYIncr, "Enter Y Increment");
		this.TxtY2Incr.Location = new System.Drawing.Point(368, 116);
		this.TxtY2Incr.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Incr.Name = "TxtY2Incr";
		this.TxtY2Incr.Size = new System.Drawing.Size(57, 22);
		this.TxtY2Incr.TabIndex = 25;
		this.ToolTip1.SetToolTip(this.TxtY2Incr, "Enter Increment for 2nd Y-axis");
		this.CmdY.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdY.ImageIndex = 6;
		this.CmdY.Location = new System.Drawing.Point(269, 251);
		this.CmdY.Margin = new System.Windows.Forms.Padding(4);
		this.CmdY.Name = "CmdY";
		this.CmdY.Size = new System.Drawing.Size(101, 49);
		this.CmdY.TabIndex = 5;
		this.CmdY.Text = "Y";
		this.CmdY.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdY.UseVisualStyleBackColor = true;
		this.CmdX.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdX.Location = new System.Drawing.Point(269, 119);
		this.CmdX.Margin = new System.Windows.Forms.Padding(4);
		this.CmdX.Name = "CmdX";
		this.CmdX.Size = new System.Drawing.Size(101, 49);
		this.CmdX.TabIndex = 4;
		this.CmdX.Text = "X ->";
		this.CmdX.UseVisualStyleBackColor = true;
		this.TabOptions.Location = new System.Drawing.Point(4, 25);
		this.TabOptions.Margin = new System.Windows.Forms.Padding(4);
		this.TabOptions.Name = "TabOptions";
		this.TabOptions.Size = new System.Drawing.Size(813, 564);
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
		this.TabTitles.Controls.Add(this.GroupBox1);
		this.TabTitles.Controls.Add(this.GrpTitle);
		this.TabTitles.Controls.Add(this.GrpGrid);
		this.TabTitles.Location = new System.Drawing.Point(4, 25);
		this.TabTitles.Margin = new System.Windows.Forms.Padding(4);
		this.TabTitles.Name = "TabTitles";
		this.TabTitles.Padding = new System.Windows.Forms.Padding(4);
		this.TabTitles.Size = new System.Drawing.Size(813, 564);
		this.TabTitles.TabIndex = 1;
		this.TabTitles.Text = "Titles/Axes";
		this.TabTitles.UseVisualStyleBackColor = true;
		this.GroupBox1.Controls.Add(this.TxtY2Incr);
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
		this.GroupBox1.Location = new System.Drawing.Point(8, 390);
		this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Size = new System.Drawing.Size(759, 153);
		this.GroupBox1.TabIndex = 89;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Axes";
		this.LblInc.AutoSize = true;
		this.LblInc.Location = new System.Drawing.Point(373, 17);
		this.LblInc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInc.Name = "LblInc";
		this.LblInc.Size = new System.Drawing.Size(31, 17);
		this.LblInc.TabIndex = 91;
		this.LblInc.Text = "Incr";
		this.LblRotate.AutoSize = true;
		this.LblRotate.Location = new System.Drawing.Point(576, 17);
		this.LblRotate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRotate.Name = "LblRotate";
		this.LblRotate.Size = new System.Drawing.Size(96, 17);
		this.LblRotate.TabIndex = 89;
		this.LblRotate.Text = "Rotate Labels";
		this.LblLog.AutoSize = true;
		this.LblLog.Location = new System.Drawing.Point(463, 17);
		this.LblLog.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLog.Name = "LblLog";
		this.LblLog.Size = new System.Drawing.Size(67, 17);
		this.LblLog.TabIndex = 88;
		this.LblLog.Text = "LogScale";
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(36, 119);
		this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(55, 17);
		this.Label3.TabIndex = 87;
		this.Label3.Text = "Y2-Axis";
		this.LblXAxis.AutoSize = true;
		this.LblXAxis.Location = new System.Drawing.Point(36, 39);
		this.LblXAxis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXAxis.Name = "LblXAxis";
		this.LblXAxis.Size = new System.Drawing.Size(47, 17);
		this.LblXAxis.TabIndex = 1;
		this.LblXAxis.Text = "X-Axis";
		this.LblYAxis.AutoSize = true;
		this.LblYAxis.Location = new System.Drawing.Point(36, 80);
		this.LblYAxis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYAxis.Name = "LblYAxis";
		this.LblYAxis.Size = new System.Drawing.Size(47, 17);
		this.LblYAxis.TabIndex = 0;
		this.LblYAxis.Text = "Y-Axis";
		this.LblLow.AutoSize = true;
		this.LblLow.Location = new System.Drawing.Point(145, 17);
		this.LblLow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLow.Name = "LblLow";
		this.LblLow.Size = new System.Drawing.Size(79, 17);
		this.LblLow.TabIndex = 79;
		this.LblLow.Text = "Low Range";
		this.LblHigh.AutoSize = true;
		this.LblHigh.Location = new System.Drawing.Point(251, 17);
		this.LblHigh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHigh.Name = "LblHigh";
		this.LblHigh.Size = new System.Drawing.Size(83, 17);
		this.LblHigh.TabIndex = 80;
		this.LblHigh.Text = "High Range";
		this.GrpTitle.Controls.Add(this.Label4);
		this.GrpTitle.Controls.Add(this.CmbFont);
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
		this.GrpTitle.Size = new System.Drawing.Size(760, 149);
		this.GrpTitle.TabIndex = 76;
		this.GrpTitle.TabStop = false;
		this.GrpTitle.Text = "Titles / Labels / Frame Size";
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(577, 98);
		this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(36, 17);
		this.Label4.TabIndex = 12;
		this.Label4.Text = "Font";
		this.CmbFont.FormattingEnabled = true;
		this.CmbFont.Items.AddRange(new object[4] { "Arial,11", "Arial,12", "Arial,8", "FreeSans,11" });
		this.CmbFont.Location = new System.Drawing.Point(649, 95);
		this.CmbFont.Margin = new System.Windows.Forms.Padding(4);
		this.CmbFont.Name = "CmbFont";
		this.CmbFont.Size = new System.Drawing.Size(101, 24);
		this.CmbFont.Sorted = true;
		this.CmbFont.TabIndex = 6;
		this.CmbFont.Text = "Arial,11";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(577, 63);
		this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(62, 17);
		this.Label5.TabIndex = 8;
		this.Label5.Text = "Frame-Y";
		this.LblXSize.AutoSize = true;
		this.LblXSize.Location = new System.Drawing.Point(577, 27);
		this.LblXSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXSize.Name = "LblXSize";
		this.LblXSize.Size = new System.Drawing.Size(66, 17);
		this.LblXSize.TabIndex = 7;
		this.LblXSize.Text = "Frame-X:";
		this.TxtY2Axis.Location = new System.Drawing.Point(128, 111);
		this.TxtY2Axis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Axis.Name = "TxtY2Axis";
		this.TxtY2Axis.Size = new System.Drawing.Size(432, 22);
		this.TxtY2Axis.TabIndex = 3;
		this.TxtYAxis.Location = new System.Drawing.Point(128, 81);
		this.TxtYAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYAxis.Name = "TxtYAxis";
		this.TxtYAxis.Size = new System.Drawing.Size(432, 22);
		this.TxtYAxis.TabIndex = 2;
		this.TxtXAxis.Location = new System.Drawing.Point(128, 52);
		this.TxtXAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXAxis.Name = "TxtXAxis";
		this.TxtXAxis.Size = new System.Drawing.Size(432, 22);
		this.TxtXAxis.TabIndex = 1;
		this.TxtTitle.Location = new System.Drawing.Point(128, 22);
		this.TxtTitle.Margin = new System.Windows.Forms.Padding(4);
		this.TxtTitle.Name = "TxtTitle";
		this.TxtTitle.Size = new System.Drawing.Size(432, 22);
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
		this.LblTitle.Size = new System.Drawing.Size(77, 17);
		this.LblTitle.TabIndex = 1;
		this.LblTitle.Text = "Chart Title:";
		this.GrpGrid.BackColor = System.Drawing.Color.Transparent;
		this.GrpGrid.Controls.Add(this.CmbKeyWidth);
		this.GrpGrid.Controls.Add(this.LblKeyWidth);
		this.GrpGrid.Controls.Add(this.lblGridBF);
		this.GrpGrid.Controls.Add(this.CmbGridBF);
		this.GrpGrid.Controls.Add(this.LblOrient);
		this.GrpGrid.Controls.Add(this.CmbKeyOrient);
		this.GrpGrid.Controls.Add(this.LblLegendPos);
		this.GrpGrid.Controls.Add(this.cmbLegendPos);
		this.GrpGrid.Controls.Add(this.Label2);
		this.GrpGrid.Controls.Add(this.ChkGridY);
		this.GrpGrid.Controls.Add(this.ChkKeyBox);
		this.GrpGrid.Controls.Add(this.ChkGridX);
		this.GrpGrid.Controls.Add(this.Label1);
		this.GrpGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GrpGrid.Location = new System.Drawing.Point(8, 183);
		this.GrpGrid.Margin = new System.Windows.Forms.Padding(4);
		this.GrpGrid.Name = "GrpGrid";
		this.GrpGrid.Padding = new System.Windows.Forms.Padding(4);
		this.GrpGrid.Size = new System.Drawing.Size(763, 186);
		this.GrpGrid.TabIndex = 75;
		this.GrpGrid.TabStop = false;
		this.GrpGrid.Text = "Gridlines / Legend";
		this.LblKeyWidth.AutoSize = true;
		this.LblKeyWidth.Location = new System.Drawing.Point(373, 52);
		this.LblKeyWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblKeyWidth.Name = "LblKeyWidth";
		this.LblKeyWidth.Size = new System.Drawing.Size(48, 17);
		this.LblKeyWidth.TabIndex = 18;
		this.LblKeyWidth.Text = "Width:";
		this.lblGridBF.AutoSize = true;
		this.lblGridBF.Location = new System.Drawing.Point(16, 114);
		this.lblGridBF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblGridBF.Name = "lblGridBF";
		this.lblGridBF.Size = new System.Drawing.Size(62, 17);
		this.lblGridBF.TabIndex = 17;
		this.lblGridBF.Text = "Position:";
		this.LblOrient.AutoSize = true;
		this.LblOrient.Location = new System.Drawing.Point(259, 98);
		this.LblOrient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOrient.Name = "LblOrient";
		this.LblOrient.Size = new System.Drawing.Size(82, 17);
		this.LblOrient.TabIndex = 15;
		this.LblOrient.Text = "Orientation:";
		this.CmbKeyOrient.FormattingEnabled = true;
		this.CmbKeyOrient.Items.AddRange(new object[3] { "", "vertical", "horizontal" });
		this.CmbKeyOrient.Location = new System.Drawing.Point(364, 96);
		this.CmbKeyOrient.Margin = new System.Windows.Forms.Padding(4);
		this.CmbKeyOrient.Name = "CmbKeyOrient";
		this.CmbKeyOrient.Size = new System.Drawing.Size(280, 25);
		this.CmbKeyOrient.TabIndex = 12;
		this.LblLegendPos.AutoSize = true;
		this.LblLegendPos.Location = new System.Drawing.Point(259, 148);
		this.LblLegendPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLegendPos.Name = "LblLegendPos";
		this.LblLegendPos.Size = new System.Drawing.Size(66, 17);
		this.LblLegendPos.TabIndex = 13;
		this.LblLegendPos.Text = "Location:";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(260, 23);
		this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(92, 17);
		this.Label2.TabIndex = 11;
		this.Label2.Text = "Legend / Key";
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.Location = new System.Drawing.Point(0, 23);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(64, 17);
		this.Label1.TabIndex = 10;
		this.Label1.Text = "Gridlines";
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
		this.TabCols.Size = new System.Drawing.Size(813, 564);
		this.TabCols.TabIndex = 0;
		this.TabCols.Text = "Columns";
		this.TabCols.UseVisualStyleBackColor = true;
		this.TxtIn.Location = new System.Drawing.Point(139, 32);
		this.TxtIn.Margin = new System.Windows.Forms.Padding(4);
		this.TxtIn.Name = "TxtIn";
		this.TxtIn.Size = new System.Drawing.Size(559, 22);
		this.TxtIn.TabIndex = 1;
		this.GridY.AllowUserToAddRows = false;
		this.GridY.AllowUserToDeleteRows = false;
		this.GridY.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridY.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridY.Columns.AddRange(this.Columns, this.ColHdr, this.ColSumm, this.ColAxis, this.ColStyle, this.ColLW, this.ColColor);
		this.GridY.Location = new System.Drawing.Point(12, 366);
		this.GridY.Margin = new System.Windows.Forms.Padding(4);
		this.GridY.Name = "GridY";
		this.GridY.Size = new System.Drawing.Size(687, 185);
		this.GridY.StandardTab = true;
		this.GridY.TabIndex = 9;
		this.Columns.Frozen = true;
		this.Columns.HeaderText = "Y (Numeric)";
		this.Columns.Name = "Columns";
		this.Columns.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Columns.ToolTipText = "Specify the Y-Variables";
		this.ColHdr.HeaderText = "Title";
		this.ColHdr.Name = "ColHdr";
		this.ColHdr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColHdr.ToolTipText = "Enter a label for the variable. Will show in the Key";
		this.ColSumm.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColSumm.HeaderText = "Summary";
		this.ColSumm.Items.AddRange("", "Avg", "Sum", "Count", "Count(*)", "Count Distinct", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColSumm.Name = "ColSumm";
		this.ColSumm.ToolTipText = "Future - Add a Summary Statistic";
		this.ColSumm.Width = 90;
		this.ColAxis.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAxis.HeaderText = "Axis";
		this.ColAxis.Items.AddRange("", "x1y1", "x1y2");
		this.ColAxis.Name = "ColAxis";
		this.ColAxis.ToolTipText = "Specify what Y-Axis to plot the variable";
		this.ColAxis.Width = 75;
		this.ColStyle.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColStyle.HeaderText = "Plot Style";
		this.ColStyle.Items.AddRange("", "lines", "linespoints", "points", "dots", "boxes", "ranges", "filledcurves x1", "candlesticks");
		this.ColStyle.Name = "ColStyle";
		this.ColStyle.ToolTipText = "Specify a plot style for the variable.Default is Boxes";
		this.ColStyle.Width = 90;
		this.ColLW.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColLW.HeaderText = "Line Width";
		this.ColLW.Items.AddRange("", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10");
		this.ColLW.Name = "ColLW";
		this.ColColor.HeaderText = "Color";
		this.ColColor.Name = "ColColor";
		this.ColColor.ReadOnly = true;
		this.ColColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColColor.ToolTipText = "Set the line or Fill Color. Double-click to set";
		this.LstX.AllowDrop = true;
		this.LstX.ContextMenuStrip = this.ContextMenuY;
		this.LstX.FormattingEnabled = true;
		this.LstX.HorizontalScrollbar = true;
		this.LstX.ItemHeight = 16;
		this.LstX.Location = new System.Drawing.Point(383, 123);
		this.LstX.Margin = new System.Windows.Forms.Padding(4);
		this.LstX.Name = "LstX";
		this.LstX.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstX.Size = new System.Drawing.Size(315, 180);
		this.LstX.TabIndex = 6;
		this.ContextMenuY.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuY.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuRemove, this.mnuClear });
		this.ContextMenuY.Name = "ContextMenuY";
		this.ContextMenuY.Size = new System.Drawing.Size(133, 52);
		this.mnuRemove.Name = "mnuRemove";
		this.mnuRemove.Size = new System.Drawing.Size(132, 24);
		this.mnuRemove.Text = "Remove";
		this.mnuClear.Name = "mnuClear";
		this.mnuClear.Size = new System.Drawing.Size(132, 24);
		this.mnuClear.Text = "Clear";
		this.LblCols.AutoSize = true;
		this.LblCols.Location = new System.Drawing.Point(8, 102);
		this.LblCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCols.Name = "LblCols";
		this.LblCols.Size = new System.Drawing.Size(62, 17);
		this.LblCols.TabIndex = 4;
		this.LblCols.Text = "Columns";
		this.LstColumns.ContextMenuStrip = this.ContextMenuCols;
		this.LstColumns.FormattingEnabled = true;
		this.LstColumns.HorizontalScrollbar = true;
		this.LstColumns.ItemHeight = 16;
		this.LstColumns.Location = new System.Drawing.Point(8, 119);
		this.LstColumns.Margin = new System.Windows.Forms.Padding(4);
		this.LstColumns.Name = "LstColumns";
		this.LstColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstColumns.Size = new System.Drawing.Size(245, 180);
		this.LstColumns.TabIndex = 3;
		this.ContextMenuCols.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuCols.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuX, this.mnuY });
		this.ContextMenuCols.Name = "ContextMenuCols";
		this.ContextMenuCols.Size = new System.Drawing.Size(88, 52);
		this.mnuX.Name = "mnuX";
		this.mnuX.Size = new System.Drawing.Size(87, 24);
		this.mnuX.Text = "X";
		this.mnuY.Name = "mnuY";
		this.mnuY.Size = new System.Drawing.Size(87, 24);
		this.mnuY.Text = "Y";
		this.LblInFile.AutoSize = true;
		this.LblInFile.Location = new System.Drawing.Point(15, 32);
		this.LblInFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInFile.Name = "LblInFile";
		this.LblInFile.Size = new System.Drawing.Size(100, 17);
		this.LblInFile.TabIndex = 1;
		this.LblInFile.Text = "Input CSV File:";
		this.LblFormTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblFormTitle.AutoSize = true;
		this.LblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.LblFormTitle.Location = new System.Drawing.Point(9, 9);
		this.LblFormTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblFormTitle.Name = "LblFormTitle";
		this.LblFormTitle.Size = new System.Drawing.Size(192, 17);
		this.LblFormTitle.TabIndex = 26;
		this.LblFormTitle.Text = "Plot Data from a CSV File";
		this.TabChart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabChart.Controls.Add(this.TabCols);
		this.TabChart.Controls.Add(this.TabTitles);
		this.TabChart.Controls.Add(this.TabOptions);
		this.TabChart.Location = new System.Drawing.Point(0, 49);
		this.TabChart.Margin = new System.Windows.Forms.Padding(4);
		this.TabChart.Name = "TabChart";
		this.TabChart.Padding = new System.Drawing.Point(30, 3);
		this.TabChart.SelectedIndex = 0;
		this.TabChart.Size = new System.Drawing.Size(821, 593);
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
		this.ContextMenuColEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuColEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuSetColor, this.mnuClearColor });
		this.ContextMenuColEdit.Name = "ContextMenuColEdit";
		this.ContextMenuColEdit.Size = new System.Drawing.Size(153, 52);
		this.mnuSetColor.Name = "mnuSetColor";
		this.mnuSetColor.Size = new System.Drawing.Size(152, 24);
		this.mnuSetColor.Text = "Set Color";
		this.mnuClearColor.Name = "mnuClearColor";
		this.mnuClearColor.Size = new System.Drawing.Size(152, 24);
		this.mnuClearColor.Text = "Clear Color";
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(827, 641);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.TabChart);
		base.Controls.Add(this.LblFormTitle);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmGnuChart";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Charting";
		this.TabTitles.ResumeLayout(false);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.GrpTitle.ResumeLayout(false);
		this.GrpTitle.PerformLayout();
		this.GrpGrid.ResumeLayout(false);
		this.GrpGrid.PerformLayout();
		this.TabCols.ResumeLayout(false);
		this.TabCols.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridY).EndInit();
		this.ContextMenuY.ResumeLayout(false);
		this.ContextMenuCols.ResumeLayout(false);
		this.TabChart.ResumeLayout(false);
		this.ContextMenuColEdit.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Populate_Columns(string File1, int MyMode)
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
					errsource = "FrmgnuChart - Populate_Columns";
					int num3 = 0;
					string[] lCSVCols = new string[0];
					if (Operators.CompareString(File1, "", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					if (General_Procedures.GetCSVHeaders(File1, ref lCSVCols) == 0)
					{
						Clear_Controls();
						TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
						LstColumns.Items.AddRange(lCSVCols);
						lCSVCols = null;
					}
					else
					{
						Interaction.MsgBox("Could not locate CSV File: " + File1 + ". Your selections were preserved but you will not be able to make significant edits", MsgBoxStyle.Exclamation, "CSV File");
						if (MyMode == 1)
						{
							TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
						}
					}
					goto end_IL_0001;
				}
				case 246:
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
				try0001_dispatch = 246;
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
				case 628:
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
							goto IL_0083;
						case 11:
							goto IL_0097;
						case 12:
							goto IL_00ab;
						case 13:
							goto IL_00bf;
						case 14:
							goto IL_00d3;
						case 15:
							goto IL_00e7;
						case 16:
							goto IL_00fb;
						case 17:
							goto IL_010f;
						case 18:
							goto IL_0123;
						case 19:
							goto IL_0137;
						case 20:
							goto IL_014b;
						case 21:
							goto IL_015f;
						case 22:
							goto IL_016f;
						case 23:
							goto IL_017f;
						case 24:
							goto IL_0193;
						case 25:
							goto IL_01a3;
						case 26:
							goto IL_01b7;
						case 27:
							goto IL_01c7;
						case 28:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 29:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01a3:
					num2 = 25;
					CmbKeyOrient.Text = "";
					goto IL_01b7;
					IL_01b7:
					num2 = 26;
					ChkLogX.Checked = false;
					goto IL_01c7;
					IL_0193:
					num2 = 24;
					ChkKeyBox.Checked = false;
					goto IL_01a3;
					IL_01c7:
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
					TxtIn.Text = "";
					goto IL_005c;
					IL_005c:
					num2 = 8;
					TxtTitle.Text = "";
					goto IL_006f;
					IL_006f:
					num2 = 9;
					TxtXAxis.Text = "";
					goto IL_0083;
					IL_0083:
					num2 = 10;
					TxtYAxis.Text = "";
					goto IL_0097;
					IL_0097:
					num2 = 11;
					TxtY2Axis.Text = "";
					goto IL_00ab;
					IL_00ab:
					num2 = 12;
					TxtXLow.Text = "";
					goto IL_00bf;
					IL_00bf:
					num2 = 13;
					TxtYLow.Text = "";
					goto IL_00d3;
					IL_00d3:
					num2 = 14;
					TxtY2Low.Text = "";
					goto IL_00e7;
					IL_00e7:
					num2 = 15;
					TxtXHigh.Text = "";
					goto IL_00fb;
					IL_00fb:
					num2 = 16;
					TxtYHigh.Text = "";
					goto IL_010f;
					IL_010f:
					num2 = 17;
					TxtY2High.Text = "";
					goto IL_0123;
					IL_0123:
					num2 = 18;
					TxtYIncr.Text = "";
					goto IL_0137;
					IL_0137:
					num2 = 19;
					TxtY2Incr.Text = "";
					goto IL_014b;
					IL_014b:
					num2 = 20;
					TxtXIncr.Text = "";
					goto IL_015f;
					IL_015f:
					num2 = 21;
					ChkGridX.Checked = false;
					goto IL_016f;
					IL_016f:
					num2 = 22;
					ChkGridY.Checked = false;
					goto IL_017f;
					IL_017f:
					num2 = 23;
					CmbGridBF.Text = "";
					goto IL_0193;
					end_IL_0001_2:
					break;
				}
				num2 = 28;
				cmbLegendPos.Text = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 628;
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
						errsource = "FrmgnuChart - Init_Controls";
						string text = "";
						string text2 = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						string text3 = "";
						string text4 = "";
						string text5 = "";
						text = BuildChart.Get_StdCtrl("IN-FILE");
						Populate_Columns(text, 1);
						text = BuildChart.Get_StdCtrl("Y");
						string[] array;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							array = Strings.Split(text, "</tr>");
							if (Information.UBound(array) >= 0)
							{
								int num7 = Information.UBound(array);
								for (num4 = 0; num4 <= num7; num4++)
								{
									string[] array2 = Strings.Split(array[num4], ";");
									if (Information.UBound(array2) >= 0)
									{
										GridY.RowCount += 1;
										int num8 = Information.UBound(array2);
										for (num5 = 0; num5 <= num8; num5++)
										{
											GridY.Rows[num4].Cells[num5].Value = array2[num5];
											GridY.Rows[num4].Cells[num5].Tag = "";
											if ((num5 == 6) & (Operators.CompareString(Strings.Trim(array2[num5]), "", TextCompare: false) != 0))
											{
												GridY[num5, num4].Style.BackColor = ColorTranslator.FromHtml(array2[num5]);
											}
										}
									}
									array2 = null;
								}
							}
							array = null;
						}
						DataGridView MyGrid = GridY;
						GridModule.Number_Grid(ref MyGrid);
						GridY = MyGrid;
						text = BuildChart.Get_StdCtrl("X");
						array = Strings.Split(text, ";");
						int num9 = Information.UBound(array);
						for (num4 = 0; num4 <= num9; num4++)
						{
							LstX.Items.Add(array[num4]);
						}
						array = null;
						TxtTitle.Text = BuildChart.Get_StdCtrl("TITLE");
						TxtXAxis.Text = BuildChart.Get_StdCtrl("X-AXIS");
						TxtYAxis.Text = BuildChart.Get_StdCtrl("Y-AXIS");
						TxtY2Axis.Text = BuildChart.Get_StdCtrl("Y-AXIS-2");
						TxtXLow.Text = BuildChart.Get_StdCtrl("X-LOW");
						TxtXHigh.Text = BuildChart.Get_StdCtrl("X-HIGH");
						TxtYLow.Text = BuildChart.Get_StdCtrl("Y-LOW");
						TxtYHigh.Text = BuildChart.Get_StdCtrl("Y-HIGH");
						TxtY2Low.Text = BuildChart.Get_StdCtrl("Y2-LOW");
						TxtY2High.Text = BuildChart.Get_StdCtrl("Y2-HIGH");
						TxtYIncr.Text = BuildChart.Get_StdCtrl("Y-INC");
						TxtY2Incr.Text = BuildChart.Get_StdCtrl("Y2-INC");
						TxtXIncr.Text = BuildChart.Get_StdCtrl("X-INC");
						CheckBox MyCK = ChkGridX;
						string MyVal = BuildChart.Get_StdCtrl("GRID-X");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkGridX = MyCK;
						MyCK = ChkGridY;
						MyVal = BuildChart.Get_StdCtrl("GRID-Y");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkGridY = MyCK;
						MyCK = ChkLogX;
						MyVal = BuildChart.Get_StdCtrl("LOG-X");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkLogX = MyCK;
						MyCK = ChkLogY;
						MyVal = BuildChart.Get_StdCtrl("LOG-Y");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkLogY = MyCK;
						MyCK = ChkKeyBox;
						MyVal = BuildChart.Get_StdCtrl("KEY-BOX");
						BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
						ChkKeyBox = MyCK;
						cmbLegendPos.Text = BuildChart.Get_StdCtrl("KEY-POS");
						CmbKeyOrient.Text = BuildChart.Get_StdCtrl("KEY-ORIENT");
						CmbKeyWidth.Text = BuildChart.Get_StdCtrl("KEY-WIDTH");
						CmbGridBF.Text = BuildChart.Get_StdCtrl("GRID-BACK-FRONT");
						CmbXRotate.Text = BuildChart.Get_StdCtrl("X-AXIS-ROTATE");
						CmbFrameX.Text = BuildChart.Get_StdCtrl("SIZE-X");
						CmbFrameY.Text = BuildChart.Get_StdCtrl("SIZE-Y");
						CmbFont.Text = BuildChart.Get_StdCtrl("GIF-FONT");
						goto end_IL_0001;
					}
					case 1258:
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
				try0001_dispatch = 1258;
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
					string title;
					string text2;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmgnuChart - CmdOK_Click";
						int num3 = 0;
						int num4 = 0;
						string text = "";
						text2 = "";
						title = "";
						int num5 = 0;
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						string text11 = "";
						bool flag = false;
						string text12 = "";
						text7 = Strings.Trim(TxtIn.Text);
						if (Operators.CompareString(text7, "", TextCompare: false) == 0)
						{
							text2 = "You must specify an Input File on the Columns Tab.";
							goto IL_0c3e;
						}
						if (GridY.RowCount >= 1 && Operators.CompareString(Strings.Mid(Strings.Trim(Conversions.ToString(GridY.Rows[0].Cells[0].Value)) + " ", 1, 1), "=", TextCompare: false) == 0)
						{
							text2 = "The first plot variable cannot be a constant (starting with an equal sign). Use the Grid arrow buttons to move it out of the first position";
							goto IL_0c3e;
						}
						BuildChart.Set_StdCtrl("IN-FILE", text7);
						num5 = Strings.InStrRev(BuildForm.Replace_Globals(text7), ".");
						if (num5 != 0)
						{
							text8 = Strings.UCase(Strings.Trim(Strings.Mid(BuildForm.Replace_Globals(text7) + " ", num5 + 1)));
							if (Operators.CompareString(text8, "TAB", TextCompare: false) == 0)
							{
								BuildChart.Set_StdCtrl("DELIMITER", "\\t");
							}
							else
							{
								BuildChart.Set_StdCtrl("DELIMITER", ",");
							}
						}
						else
						{
							BuildChart.Set_StdCtrl("DELIMITER", ",");
						}
						if (LstX.Items.Count == 0)
						{
							text2 = "No X Columns were assigned on the Columns Tab.";
							goto IL_0c3e;
						}
						text = "";
						text7 = "";
						if (LstX.Items.Count > 0)
						{
							int num6 = LstX.Items.Count - 1;
							for (num3 = 0; num3 <= num6; num3++)
							{
								text7 = Conversions.ToString(Operators.ConcatenateObject(text7 + text, LstX.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("X", text7);
						if (GridY.RowCount <= 0)
						{
							text2 = "No Y Columns were assigned on the Columns Tab.";
							goto IL_0c3e;
						}
						text = "";
						text7 = "";
						int num7 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num7; num3++)
						{
							if (Operators.ConditionalCompareObjectNotEqual(GridY.Rows[num3].Cells[0].Value, "", TextCompare: false))
							{
								if (Operators.CompareString(text7, "", TextCompare: false) != 0)
								{
									text7 += "</tr>";
								}
								int num8 = GridY.ColumnCount - 1;
								for (num4 = 0; num4 <= num8; num4++)
								{
									text7 = Conversions.ToString(Operators.ConcatenateObject(text7 + text, GridY.Rows[num3].Cells[num4].Value));
									text = ";";
								}
							}
						}
						text7 = Strings.Replace(text7, "</tr>;", "</tr>", 1, -1, CompareMethod.Text);
						BuildChart.Set_StdCtrl("Y", text7);
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
						text7 = "";
						CheckBox MyCK = ChkGridX;
						BuildChart.Set_CK_Box(ref MyCK, ref text7);
						ChkGridX = MyCK;
						BuildChart.Set_StdCtrl("GRID-X", text7);
						text7 = "";
						MyCK = ChkGridY;
						BuildChart.Set_CK_Box(ref MyCK, ref text7);
						ChkGridY = MyCK;
						BuildChart.Set_StdCtrl("GRID-Y", text7);
						text7 = "";
						MyCK = ChkLogX;
						BuildChart.Set_CK_Box(ref MyCK, ref text7);
						ChkLogX = MyCK;
						BuildChart.Set_StdCtrl("LOG-X", text7);
						text7 = "";
						MyCK = ChkLogY;
						BuildChart.Set_CK_Box(ref MyCK, ref text7);
						ChkLogY = MyCK;
						BuildChart.Set_StdCtrl("LOG-Y", text7);
						text7 = "";
						MyCK = ChkKeyBox;
						BuildChart.Set_CK_Box(ref MyCK, ref text7);
						ChkKeyBox = MyCK;
						BuildChart.Set_StdCtrl("KEY-BOX", text7);
						BuildChart.Set_StdCtrl("KEY-POS", Strings.Trim(cmbLegendPos.Text), DoLower: true);
						BuildChart.Set_StdCtrl("KEY-ORIENT", Strings.Trim(CmbKeyOrient.Text), DoLower: true);
						BuildChart.Set_StdCtrl("KEY-WIDTH", Strings.Trim(CmbKeyWidth.Text));
						BuildChart.Set_StdCtrl("GRID-BACK-FRONT", Strings.Trim(CmbGridBF.Text), DoLower: true);
						BuildChart.Set_StdCtrl("X-AXIS-ROTATE", Strings.Trim(CmbXRotate.Text));
						BuildChart.Set_StdCtrl("SIZE-X", Strings.Trim(CmbFrameX.Text));
						BuildChart.Set_StdCtrl("SIZE-Y", Strings.Trim(CmbFrameY.Text));
						if (Operators.CompareString(CmbFont.Text.Trim(), "", TextCompare: false) == 0)
						{
							CmbFont.Text = "Arial,11";
						}
						BuildChart.Set_StdCtrl("GIF-FONT", Strings.Trim(CmbFont.Text));
						text12 = "";
						int num9 = GridY.RowCount - 1;
						for (num3 = 0; num3 <= num9; num3++)
						{
							if (Operators.ConditionalCompareObjectNotEqual(GridY.Rows[num3].Cells[2].Value, "", TextCompare: false))
							{
								text12 = "Y";
								break;
							}
						}
						if ((Operators.CompareString(text12, "", TextCompare: false) == 0) & (LstX.Items.Count >= 2))
						{
							text12 = "Y";
						}
						if (Operators.CompareString(text12, "Y", TextCompare: false) == 0)
						{
							text12 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("#SQLVAR=", LstX.Items[0]), ";"));
							if (LstX.Items.Count >= 2)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12, LstX.Items[1]));
							}
							int num10 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num10; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[0].Value));
							}
							text12 += "\r\n#SQLHDR=;";
							int num11 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num11; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[1].Value));
							}
							text12 += "\r\n#SQLSUM=;";
							int num12 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num12; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[2].Value));
							}
							text12 += "\r\n#SQLPLA=;";
							int num13 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num13; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[3].Value));
							}
							text12 += "\r\n#SQLPLS=;";
							int num14 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num14; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[4].Value));
							}
							text12 += "\r\n#SQLPLW=;";
							int num15 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num15; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[5].Value));
							}
							text12 += "\r\n#SQLPLC=;";
							int num16 = GridY.RowCount - 1;
							for (num3 = 0; num3 <= num16; num3++)
							{
								text12 = Conversions.ToString(Operators.ConcatenateObject(text12 + ";", GridY.Rows[num3].Cells[6].Value));
							}
						}
						BuildChart.Set_StdCtrl("MODE", text12);
						text = "";
						num3 = 0;
						do
						{
							TabPage tabPage = ((num3 > 1) ? TabOptions : TabCols);
							IEnumerator enumerator = tabPage.Controls.GetEnumerator();
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
								}
								if ((control is ComboBox) & (Operators.CompareString(Strings.UCase(control.Name), "CONTROL" + Conversions.ToString(num3), TextCompare: false) == 0))
								{
									Globals_Renamed.gOptOptions[num3].Selected = control.Text;
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
					case 3200:
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
						IL_0c3e:
						Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, title);
						goto end_IL_0001;
					}
					goto IL_0cb6;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3200;
				continue;
			}
			break;
			IL_0cb6:
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 218:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
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
					IL_0069:
					num2 = 10;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
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
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				ListBox LstAny = LstX;
				Set_Index_List(ref LstAny, myIdx);
				LstX = LstAny;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 218;
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
				case 170:
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
					if (LstX.Items.Count < 2)
					{
						break;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					Interaction.MsgBox("You are only allowed " + Conversions.ToString(2) + " X variable.", MsgBoxStyle.Information, "Only " + Conversions.ToString(2) + " X Limit");
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
				try0001_dispatch = 170;
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
							goto IL_010a;
						case 12:
							goto IL_0118;
						case 13:
							goto IL_0139;
						case 14:
							goto IL_0143;
						case 15:
							goto IL_0153;
						case 16:
							goto IL_015d;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0143:
					num2 = 14;
					TabChart.SelectedIndex = 0;
					goto IL_0153;
					IL_0153:
					num2 = 15;
					Init_Controls();
					goto IL_015d;
					IL_0139:
					num2 = 13;
					Clear_Controls();
					goto IL_0143;
					IL_015d:
					num2 = 16;
					TabCols = this.TabCols;
					TabOptions = this.TabOptions;
					ToolTip = ToolTip1;
					BuildChart.Init_Options(1, ref TabCols, ref TabOptions, ref ToolTip);
					ToolTip1 = ToolTip;
					this.TabOptions = TabOptions;
					this.TabCols = TabCols;
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
					f_MyPre = "";
					goto IL_010a;
					IL_010a:
					num2 = 11;
					f_MyPost = "";
					goto IL_0118;
					IL_0118:
					num2 = 12;
					TabCols = this.TabOptions;
					BuildChart.Add_Labels_To_Options(ref TabCols, 2, 19, 16);
					this.TabOptions = TabCols;
					goto IL_0139;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				Cursor.Current = Cursors.Default;
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
							goto IL_000f;
						case 4:
							goto IL_002a;
						case 5:
							goto IL_003e;
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
					IL_000f:
					num2 = 3;
					if (LstX.Items.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_002a;
					IL_002a:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear all selections?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Clear?");
					goto IL_003e;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_003e:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				LstX.Items.Clear();
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
		int num5 = default(int);
		int columnIndex = default(int);
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
				case 272:
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
							goto IL_0023;
						case 5:
							goto IL_0037;
						case 6:
							goto IL_0052;
						case 7:
							goto IL_006d;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0037:
					num2 = 5;
					MyGrid = GridY;
					GridModule.Grid_Delete(ref MyGrid);
					GridY = MyGrid;
					goto IL_0052;
					IL_0052:
					num2 = 6;
					MyGrid = GridY;
					GridModule.Number_Grid(ref MyGrid);
					GridY = MyGrid;
					goto IL_006d;
					IL_0023:
					num2 = 4;
					num5 = GridY.CurrentCell.RowIndex;
					goto IL_0037;
					IL_006d:
					num2 = 7;
					if (!Conversions.ToBoolean(Operators.AndObject(num5 > 0, Operators.CompareObjectEqual(GridY.Rows[num5].Cells[2].Value, "", TextCompare: false))))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					columnIndex = GridY.CurrentCell.ColumnIndex;
					goto IL_0023;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				GridY.CurrentCell = GridY[columnIndex, checked(num5 - 1)];
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 272;
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
		string left = default(string);
		int num5 = default(int);
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
							goto IL_001e;
						case 4:
							goto IL_0027;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_0031;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_0065;
						case 9:
							goto IL_007c;
						case 10:
							goto IL_0095;
						case 11:
							goto IL_00ab;
						case 13:
						case 14:
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_007c:
					num2 = 9;
					if (Operators.CompareString(left, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0095;
					IL_0095:
					num2 = 10;
					num5 = (int)Interaction.MsgBox("Loading the new file will clear selections. Are you sure you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Continue?");
					goto IL_00ab;
					IL_0065:
					num2 = 8;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_007c;
					IL_00ab:
					num2 = 11;
					if (num5 != 7)
					{
						break;
					}
					goto end_IL_0001_3;
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
					left = text;
					goto IL_0031;
					IL_0031:
					num2 = 6;
					BuildForm.FileOpenSave("O", text, "csv+", "Find CSV File", "");
					goto IL_004e;
					IL_004e:
					num2 = 7;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0065;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				Populate_Columns(text, 0);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
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
	}

	private void mnuClearColor_Click(object sender, EventArgs e)
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
				DataGridView GridY = this.GridY;
				BuildChart.Chart_Clear_Color(ref GridY, 6, 6);
				this.GridY = GridY;
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

	private void mnuSetColor_Click(object sender, EventArgs e)
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
				case 87:
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
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Chart_Set_Color(ref GridY, ref ColorDialog, 6, 6);
				ColorDialog1 = ColorDialog;
				this.GridY = GridY;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 87;
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
		int num5 = default(int);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				DataGridView GridY;
				ContextMenuStrip ContextMenuColEdit;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 296:
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
							goto IL_002f;
						case 6:
							goto IL_003f;
						case 7:
							goto IL_0067;
						case 8:
							goto IL_0087;
						case 9:
							goto IL_00a7;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a7:
					num2 = 9;
					GridY = (DataGridView)sender;
					ContextMenuColEdit = this.ContextMenuColEdit;
					BuildChart.Chart_Color_MouseUp(ref GridY, ref ContextMenuColEdit, 6, 6, num5, num6);
					this.ContextMenuColEdit = ContextMenuColEdit;
					sender = GridY;
					break;
					IL_0067:
					num2 = 7;
					num5 = checked((int)Math.Round((double)this.GridY.Width * 0.75));
					goto IL_0087;
					IL_003f:
					num2 = 6;
					if (e.KeyValue != 13 || this.GridY.CurrentCell.ColumnIndex != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0067;
					IL_0087:
					num2 = 8;
					num6 = checked((int)Math.Round((double)this.GridY.Height / 2.0));
					goto IL_00a7;
					IL_000b:
					num2 = 2;
					if (e.KeyValue == 46)
					{
						goto IL_001b;
					}
					goto IL_003f;
					IL_001b:
					num2 = 3;
					cmdDelete_Click(cmdDelete, new EventArgs());
					goto IL_002f;
					IL_002f:
					num2 = 4;
					e.Handled = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 296;
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
					goto IL_000b;
				case 125:
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
					if (this.GridY.CurrentCell.ColumnIndex != 6)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				DataGridView GridY = (DataGridView)sender;
				ContextMenuStrip ContextMenuColEdit = this.ContextMenuColEdit;
				BuildChart.Chart_Color_MouseUp(ref GridY, ref ContextMenuColEdit, 6, 6, e.X, e.Y);
				this.ContextMenuColEdit = ContextMenuColEdit;
				sender = GridY;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 125;
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

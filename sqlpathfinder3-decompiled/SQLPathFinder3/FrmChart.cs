using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmChart : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstGroup")]
	private ListBox _LstGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdGroup")]
	private Button _CmdGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtInFile")]
	private ComboBox _TxtInFile;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdRemove")]
	private Button _CmdRemove;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdExtra")]
	private Button _cmdExtra;

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
	[AccessedThroughProperty("cmdBy")]
	private Button _cmdBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstBy")]
	private ListBox _LstBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstX")]
	private ListBox _LstX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LstY")]
	private ListBox _LstY;

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
	[AccessedThroughProperty("GridWhere")]
	private DataGridView _GridWhere;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdColHelp")]
	private Button _CmdColHelp;

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
	[AccessedThroughProperty("TxtPerRow")]
	private ComboBox _TxtPerRow;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBrowse")]
	private Button _cmdBrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdVal")]
	private Button _CmdVal;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnusetColor")]
	private ToolStripMenuItem _mnusetColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearColor")]
	private ToolStripMenuItem _mnuClearColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridTheme")]
	private DataGridView _GridTheme;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDel2")]
	private Button _cmdDel2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmddown2")]
	private Button _cmddown2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp2")]
	private Button _cmdUp2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear2")]
	private Button _cmdClear2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuThemeStrip")]
	private ToolStripMenuItem _mnuThemeStrip;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuLoadTheme")]
	private ToolStripMenuItem _mnuLoadTheme;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveTheme")]
	private ToolStripMenuItem _mnuSaveTheme;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuXCompute")]
	private ToolStripMenuItem _mnuXCompute;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuDataLabels")]
	private ToolStripMenuItem _mnuDataLabels;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp")]
	private ToolStripMenuItem _mnuHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdRefresh")]
	private Button _cmdRefresh;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRemove")]
	private ToolStripMenuItem _mnuRemove;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClear")]
	private ToolStripMenuItem _mnuClear;

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
	[AccessedThroughProperty("mnuBy")]
	private ToolStripMenuItem _mnuBy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGroup")]
	private ToolStripMenuItem _mnuGroup;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuVal")]
	private ToolStripMenuItem _mnuVal;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRefreshTheme")]
	private ToolStripMenuItem _mnuRefreshTheme;

	private const int f_NoThemeRows = 50;

	private string f_ComputedX;

	private string f_ThemeFile;

	public string f_TemplateType2;

	private string f_MyPre;

	private string f_MyPost;

	private const int l_NoOptsTabcols = 1;

	private string[] MyTableArr;

	public string LastChartTable;

	public bool f_OK;

	public string f_ColCaseInsensitive;

	private string MyTitleTabVisible;

	private string f_ExtraData;

	private string f_Y_Map;

	private string l_DragSource;

	private int l_DragIdx;

	private int f_WRows;

	private string f_WhereMode;

	private const short Colw_And = 0;

	private const short Colw_Pareno = 1;

	private const short Colw_Col = 2;

	private const short Colw_Opr = 3;

	private const short Colw_Val = 4;

	private const short Colw_Col2 = 5;

	private const short Colw_ParenC = 6;

	private int fYMax;

	private int fXMax;

	private int fBYMax;

	private int fGroupMax;

	private bool IsRecursive;

	private string fYBound;

	private string fSpecialSetup;

	public string f_Pseudo;

	private string f_CustomGroup;

	private string f_SpecialChart;

	private const int t_col = 0;

	private const int t_fill = 4;

	private string l_Generalval2;

	private string f_QuoteCols;

	private string f_StartEndContain;

	private string lMyPqtCSV;

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
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

	[field: AccessedThroughProperty("Y_Minor_Ticks")]
	internal virtual CheckBox Y_Minor_Ticks
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Y_Major_Ticks")]
	internal virtual CheckBox Y_Major_Ticks
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("X_Minor_Ticks")]
	internal virtual CheckBox X_Minor_Ticks
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("X_Major_Ticks")]
	internal virtual CheckBox X_Major_Ticks
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

	[field: AccessedThroughProperty("Y_Minor_Grid")]
	internal virtual CheckBox Y_Minor_Grid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Y_Major_Grid")]
	internal virtual CheckBox Y_Major_Grid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("X_Minor_Grid")]
	internal virtual CheckBox X_Minor_Grid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("X_Major_Grid")]
	internal virtual CheckBox X_Major_Grid
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

	[field: AccessedThroughProperty("LblOption1")]
	internal virtual Label LblOption1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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
			DragEventHandler value2 = LstGroup_DragDrop;
			DragEventHandler value3 = LstGroup_DragOver;
			MouseEventHandler value4 = LstGroup_MouseDown;
			EventHandler value5 = LstGroup_GotFocus;
			ListBox listBox = _LstGroup;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragOver -= value3;
				listBox.MouseDown -= value4;
				listBox.GotFocus -= value5;
			}
			_LstGroup = value;
			listBox = _LstGroup;
			if (listBox != null)
			{
				listBox.DragDrop += value2;
				listBox.DragOver += value3;
				listBox.MouseDown += value4;
				listBox.GotFocus += value5;
			}
		}
	}

	internal virtual Button CmdGroup
	{
		[CompilerGenerated]
		get
		{
			return _CmdGroup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdGroup_Click;
			Button button = _CmdGroup;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdGroup = value;
			button = _CmdGroup;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ComboBox TxtInFile
	{
		[CompilerGenerated]
		get
		{
			return _TxtInFile;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtInFile_SelectedValueChanged;
			ComboBox comboBox = _TxtInFile;
			if (comboBox != null)
			{
				comboBox.SelectedValueChanged -= value2;
			}
			_TxtInFile = value;
			comboBox = _TxtInFile;
			if (comboBox != null)
			{
				comboBox.SelectedValueChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LblOption0")]
	internal virtual Label LblOption0
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

	[field: AccessedThroughProperty("LblRoles")]
	internal virtual Label LblRoles
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdExtra
	{
		[CompilerGenerated]
		get
		{
			return _cmdExtra;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExtra_Click;
			Button button = _cmdExtra;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdExtra = value;
			button = _cmdExtra;
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
			DragEventHandler value2 = LstBy_DragDrop;
			DragEventHandler value3 = LstBy_DragOver;
			MouseEventHandler value4 = LstBy_MouseDown;
			EventHandler value5 = LstBy_GotFocus;
			ListBox listBox = _LstBy;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragOver -= value3;
				listBox.MouseDown -= value4;
				listBox.GotFocus -= value5;
			}
			_LstBy = value;
			listBox = _LstBy;
			if (listBox != null)
			{
				listBox.DragDrop += value2;
				listBox.DragOver += value3;
				listBox.MouseDown += value4;
				listBox.GotFocus += value5;
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

	internal virtual ListBox LstY
	{
		[CompilerGenerated]
		get
		{
			return _LstY;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DragEventHandler value2 = LstY_DragDrop;
			DragEventHandler value3 = LstY_DragOver;
			MouseEventHandler value4 = LstY_MouseDown;
			EventHandler value5 = LstY_GotFocus;
			ListBox listBox = _LstY;
			if (listBox != null)
			{
				listBox.DragDrop -= value2;
				listBox.DragOver -= value3;
				listBox.MouseDown -= value4;
				listBox.GotFocus -= value5;
			}
			_LstY = value;
			listBox = _LstY;
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

	[field: AccessedThroughProperty("CmbY")]
	internal virtual ComboBox CmbY
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

	[field: AccessedThroughProperty("XRlineColor1")]
	internal virtual ComboBox XRlineColor1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineStyle1")]
	internal virtual ComboBox XRlineStyle1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRLineVal1")]
	internal virtual TextBox XRLineVal1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXRefLine1")]
	internal virtual Label LblXRefLine1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineTxt4")]
	internal virtual TextBox XRlineTxt4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineColor4")]
	internal virtual ComboBox XRlineColor4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineStyle4")]
	internal virtual ComboBox XRlineStyle4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRLineVal4")]
	internal virtual TextBox XRLineVal4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXRefLine4")]
	internal virtual Label LblXRefLine4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineTxt3")]
	internal virtual TextBox XRlineTxt3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineColor3")]
	internal virtual ComboBox XRlineColor3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineStyle3")]
	internal virtual ComboBox XRlineStyle3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRLineVal3")]
	internal virtual TextBox XRLineVal3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXRefLine3")]
	internal virtual Label LblXRefLine3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineTxt2")]
	internal virtual TextBox XRlineTxt2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineColor2")]
	internal virtual ComboBox XRlineColor2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineStyle2")]
	internal virtual ComboBox XRlineStyle2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRLineVal2")]
	internal virtual TextBox XRLineVal2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblXRefLine2")]
	internal virtual Label LblXRefLine2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblRefLineDesc")]
	internal virtual Label LblRefLineDesc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("XRlineTxt1")]
	internal virtual TextBox XRlineTxt1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("YRlineTxt4")]
	internal virtual TextBox YRlineTxt4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineColor4")]
	internal virtual ComboBox YRlineColor4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineStyle4")]
	internal virtual ComboBox YRlineStyle4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRLineVal4")]
	internal virtual TextBox YRLineVal4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineTxt3")]
	internal virtual TextBox YRlineTxt3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineColor3")]
	internal virtual ComboBox YRlineColor3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineStyle3")]
	internal virtual ComboBox YRlineStyle3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRLineVal3")]
	internal virtual TextBox YRLineVal3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblYRefLine3")]
	internal virtual Label LblYRefLine3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineTxt2")]
	internal virtual TextBox YRlineTxt2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineColor2")]
	internal virtual ComboBox YRlineColor2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineStyle2")]
	internal virtual ComboBox YRlineStyle2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRLineVal2")]
	internal virtual TextBox YRLineVal2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblYRefLine2")]
	internal virtual Label LblYRefLine2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineTxt1")]
	internal virtual TextBox YRlineTxt1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineColor1")]
	internal virtual ComboBox YRlineColor1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRlineStyle1")]
	internal virtual ComboBox YRlineStyle1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("YRLineVal1")]
	internal virtual TextBox YRLineVal1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblYRefLine1")]
	internal virtual Label LblYRefLine1
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

	[field: AccessedThroughProperty("FrameY")]
	internal virtual TextBox FrameY
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

	[field: AccessedThroughProperty("FrameX")]
	internal virtual TextBox FrameX
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

	[field: AccessedThroughProperty("FrameMarker")]
	internal virtual TextBox FrameMarker
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblFMarker")]
	internal virtual Label lblFMarker
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

	[field: AccessedThroughProperty("GrpFrame")]
	internal virtual GroupBox GrpFrame
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

	[field: AccessedThroughProperty("LblYRefLine4")]
	internal virtual Label LblYRefLine4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FrameColor")]
	internal virtual ComboBox FrameColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GrpRowLegend")]
	internal virtual GroupBox GrpRowLegend
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLMarker1")]
	internal virtual CheckBox RLMarker1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLColor1")]
	internal virtual CheckBox RLColor1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLCol1")]
	internal virtual ComboBox RLCol1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label19")]
	internal virtual Label Label19
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label21")]
	internal virtual Label Label21
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLMarker2")]
	internal virtual CheckBox RLMarker2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLColor2")]
	internal virtual CheckBox RLColor2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RLCol2")]
	internal virtual ComboBox RLCol2
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

	[field: AccessedThroughProperty("LstOther")]
	internal virtual ListBox LstOther
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblRow")]
	internal virtual Label LblRow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("LblWhere")]
	internal virtual Label LblWhere
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdColHelp
	{
		[CompilerGenerated]
		get
		{
			return _CmdColHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdColHelp_Click;
			Button button = _CmdColHelp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdColHelp = value;
			button = _CmdColHelp;
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

	internal virtual ComboBox TxtPerRow
	{
		[CompilerGenerated]
		get
		{
			return _TxtPerRow;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtPerRow_SelectedValueChanged;
			ComboBox comboBox = _TxtPerRow;
			if (comboBox != null)
			{
				comboBox.SelectedValueChanged -= value2;
			}
			_TxtPerRow = value;
			comboBox = _TxtPerRow;
			if (comboBox != null)
			{
				comboBox.SelectedValueChanged += value2;
			}
		}
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

	internal virtual Button cmdBrowse
	{
		[CompilerGenerated]
		get
		{
			return _cmdBrowse;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBrowse_Click;
			Button button = _cmdBrowse;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdBrowse = value;
			button = _cmdBrowse;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Lbl1Image")]
	internal virtual Label Lbl1Image
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Chk1Image")]
	internal virtual CheckBox Chk1Image
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLOY")]
	internal virtual ComboBox TxtLOY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtHiY")]
	internal virtual ComboBox TxtHiY
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GrpRLegend")]
	internal virtual GroupBox GrpRLegend
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbLSize")]
	internal virtual ComboBox CmbLSize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbllx")]
	internal virtual Label lbllx
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbLBG")]
	internal virtual ComboBox CmbLBG
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label23")]
	internal virtual Label Label23
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLTitle")]
	internal virtual TextBox TxtLTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label24")]
	internal virtual Label Label24
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbLCols")]
	internal virtual ComboBox CmbLCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbllcols")]
	internal virtual Label lbllcols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbLPos")]
	internal virtual ComboBox CmbLPos
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label26")]
	internal virtual Label Label26
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox1")]
	internal virtual ComboBox ComboBox1
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

	[field: AccessedThroughProperty("CheckBox6")]
	internal virtual CheckBox CheckBox6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox2")]
	internal virtual ComboBox ComboBox2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label16")]
	internal virtual Label Label16
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBox1")]
	internal virtual TextBox TextBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label17")]
	internal virtual Label Label17
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox3")]
	internal virtual ComboBox ComboBox3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label18")]
	internal virtual Label Label18
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboBox4")]
	internal virtual ComboBox ComboBox4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label20")]
	internal virtual Label Label20
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbLKSize")]
	internal virtual ComboBox CmbLKSize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblly")]
	internal virtual Label lblly
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdVal
	{
		[CompilerGenerated]
		get
		{
			return _CmdVal;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdVal_Click;
			Button button = _CmdVal;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdVal = value;
			button = _CmdVal;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LstVal")]
	internal virtual ListBox LstVal
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

	[field: AccessedThroughProperty("YRTxtAdj1")]
	internal virtual TextBox YRTxtAdj1
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

	internal virtual ToolStripMenuItem mnusetColor
	{
		[CompilerGenerated]
		get
		{
			return _mnusetColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSetColor_Click;
			ToolStripMenuItem toolStripMenuItem = _mnusetColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnusetColor = value;
			toolStripMenuItem = _mnusetColor;
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

	[field: AccessedThroughProperty("ColorDialog1")]
	internal virtual ColorDialog ColorDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TabTheme")]
	internal virtual TabPage TabTheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridTheme
	{
		[CompilerGenerated]
		get
		{
			return _GridTheme;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = GridTheme_KeyDown;
			MouseEventHandler value3 = GridTheme_MouseDoubleClick;
			DataGridView dataGridView = _GridTheme;
			if (dataGridView != null)
			{
				dataGridView.KeyDown -= value2;
				dataGridView.MouseDoubleClick -= value3;
			}
			_GridTheme = value;
			dataGridView = _GridTheme;
			if (dataGridView != null)
			{
				dataGridView.KeyDown += value2;
				dataGridView.MouseDoubleClick += value3;
			}
		}
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdDel2
	{
		[CompilerGenerated]
		get
		{
			return _cmdDel2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDel2_Click;
			Button button = _cmdDel2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDel2 = value;
			button = _cmdDel2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmddown2
	{
		[CompilerGenerated]
		get
		{
			return _cmddown2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmddown2_Click;
			Button button = _cmddown2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmddown2 = value;
			button = _cmddown2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdUp2
	{
		[CompilerGenerated]
		get
		{
			return _cmdUp2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp2_Click;
			Button button = _cmdUp2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdUp2 = value;
			button = _cmdUp2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdClear2
	{
		[CompilerGenerated]
		get
		{
			return _cmdClear2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClear2_Click;
			Button button = _cmdClear2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdClear2 = value;
			button = _cmdClear2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuThemeStrip
	{
		[CompilerGenerated]
		get
		{
			return _mnuThemeStrip;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuThemeStrip_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuThemeStrip;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuThemeStrip = value;
			toolStripMenuItem = _mnuThemeStrip;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuLoadTheme
	{
		[CompilerGenerated]
		get
		{
			return _mnuLoadTheme;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLoadTheme_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuLoadTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuLoadTheme = value;
			toolStripMenuItem = _mnuLoadTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSaveTheme
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveTheme;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveTheme_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveTheme = value;
			toolStripMenuItem = _mnuSaveTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuStdTheme")]
	internal virtual ToolStripComboBox mnuStdTheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuComputations")]
	internal virtual ToolStripMenuItem mnuComputations
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuXCompute
	{
		[CompilerGenerated]
		get
		{
			return _mnuXCompute;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuXCompute_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuXCompute;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuXCompute = value;
			toolStripMenuItem = _mnuXCompute;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuDataLabels
	{
		[CompilerGenerated]
		get
		{
			return _mnuDataLabels;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuDataLabels_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuDataLabels;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuDataLabels = value;
			toolStripMenuItem = _mnuDataLabels;
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

	[field: AccessedThroughProperty("Col2Grid")]
	internal virtual DataGridViewComboBoxColumn Col2Grid
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

	[field: AccessedThroughProperty("TxtIncY")]
	internal virtual ComboBox TxtIncY
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

	[field: AccessedThroughProperty("TxtHiY2")]
	internal virtual ComboBox TxtHiY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLoY2")]
	internal virtual ComboBox TxtLoY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtIncX")]
	internal virtual ComboBox TxtIncX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtHiX")]
	internal virtual ComboBox TxtHiX
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLoX")]
	internal virtual ComboBox TxtLoX
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

	[field: AccessedThroughProperty("TabOptions")]
	internal virtual TabPage TabOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("mnuKeepUnusedCharts")]
	internal virtual ToolStripMenuItem mnuKeepUnusedCharts
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

	[field: AccessedThroughProperty("ContextMenuXYByGroup")]
	internal virtual ContextMenuStrip ContextMenuXYByGroup
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
			EventHandler value2 = CmdGroup_Click;
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

	internal virtual ToolStripMenuItem mnuVal
	{
		[CompilerGenerated]
		get
		{
			return _mnuVal;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdVal_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuVal;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuVal = value;
			toolStripMenuItem = _mnuVal;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblType")]
	internal virtual Label lblType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblrot")]
	internal virtual Label lblrot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbXType")]
	internal virtual ComboBox cmbXType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbXRot")]
	internal virtual ComboBox cmbXRot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbYRot")]
	internal virtual ComboBox cmbYRot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbY2Rot")]
	internal virtual ComboBox cmbY2Rot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtIncY2")]
	internal virtual ComboBox TxtIncY2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuRefreshTheme
	{
		[CompilerGenerated]
		get
		{
			return _mnuRefreshTheme;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRefreshTheme_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRefreshTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRefreshTheme = value;
			toolStripMenuItem = _mnuRefreshTheme;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn3")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbSym")]
	internal virtual DataGridViewComboBoxColumn cmbSym
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmblinetype")]
	internal virtual DataGridViewComboBoxColumn cmblinetype
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewComboBoxColumn3")]
	internal virtual DataGridViewComboBoxColumn DataGridViewComboBoxColumn3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn4")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbYType")]
	internal virtual ComboBox cmbYType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbY2Type")]
	internal virtual ComboBox cmbY2Type
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmChart()
	{
		base.FormClosed += FrmChart_FormClosed;
		base.Load += FrmChart_Load;
		f_ComputedX = "";
		f_ThemeFile = "sqlpathfinder.rtheme";
		f_TemplateType2 = "";
		f_MyPre = "";
		f_MyPost = "";
		LastChartTable = "";
		f_OK = false;
		f_ColCaseInsensitive = "N";
		MyTitleTabVisible = "Y";
		f_ExtraData = "";
		f_Y_Map = "";
		l_DragSource = "";
		l_DragIdx = -1;
		f_WRows = 100;
		f_WhereMode = "N/A";
		fYMax = 1000000;
		fXMax = 1000000;
		fBYMax = 1000000;
		fGroupMax = 1000000;
		IsRecursive = false;
		fYBound = "";
		fSpecialSetup = "N/A";
		f_Pseudo = "N/A";
		f_CustomGroup = "";
		f_SpecialChart = "";
		l_Generalval2 = "";
		f_QuoteCols = "";
		f_StartEndContain = "";
		lMyPqtCSV = "spf_parquet_temp_" + Strings.Trim(Globals_Renamed.gSPFCache) + ".tab";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmChart));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.CmdRemove = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.CmdColHelp = new System.Windows.Forms.Button();
		this.CmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdDelete = new System.Windows.Forms.Button();
		this.TxtPerRow = new System.Windows.Forms.ComboBox();
		this.cmdBrowse = new System.Windows.Forms.Button();
		this.TxtTitle = new System.Windows.Forms.TextBox();
		this.Lbl1Image = new System.Windows.Forms.Label();
		this.ComboBox1 = new System.Windows.Forms.ComboBox();
		this.CheckBox6 = new System.Windows.Forms.CheckBox();
		this.ComboBox2 = new System.Windows.Forms.ComboBox();
		this.TextBox1 = new System.Windows.Forms.TextBox();
		this.ComboBox3 = new System.Windows.Forms.ComboBox();
		this.ComboBox4 = new System.Windows.Forms.ComboBox();
		this.CmbLSize = new System.Windows.Forms.ComboBox();
		this.CmbLBG = new System.Windows.Forms.ComboBox();
		this.TxtLTitle = new System.Windows.Forms.TextBox();
		this.CmbLCols = new System.Windows.Forms.ComboBox();
		this.CmbLPos = new System.Windows.Forms.ComboBox();
		this.CmbLKSize = new System.Windows.Forms.ComboBox();
		this.YRTxtAdj1 = new System.Windows.Forms.TextBox();
		this.cmdDel2 = new System.Windows.Forms.Button();
		this.cmddown2 = new System.Windows.Forms.Button();
		this.cmdUp2 = new System.Windows.Forms.Button();
		this.cmdClear2 = new System.Windows.Forms.Button();
		this.chkHTTP = new System.Windows.Forms.CheckBox();
		this.cmdRefresh = new System.Windows.Forms.Button();
		this.CmbY = new System.Windows.Forms.ComboBox();
		this.CmdY = new System.Windows.Forms.Button();
		this.CmdX = new System.Windows.Forms.Button();
		this.cmdBy = new System.Windows.Forms.Button();
		this.cmdExtra = new System.Windows.Forms.Button();
		this.TabTitles = new System.Windows.Forms.TabPage();
		this.GrpRLegend = new System.Windows.Forms.GroupBox();
		this.lblly = new System.Windows.Forms.Label();
		this.lbllx = new System.Windows.Forms.Label();
		this.Label23 = new System.Windows.Forms.Label();
		this.Label24 = new System.Windows.Forms.Label();
		this.lbllcols = new System.Windows.Forms.Label();
		this.Label26 = new System.Windows.Forms.Label();
		this.GrpRowLegend = new System.Windows.Forms.GroupBox();
		this.Label21 = new System.Windows.Forms.Label();
		this.RLMarker2 = new System.Windows.Forms.CheckBox();
		this.RLColor2 = new System.Windows.Forms.CheckBox();
		this.RLCol2 = new System.Windows.Forms.ComboBox();
		this.Label19 = new System.Windows.Forms.Label();
		this.RLMarker1 = new System.Windows.Forms.CheckBox();
		this.RLColor1 = new System.Windows.Forms.CheckBox();
		this.RLCol1 = new System.Windows.Forms.ComboBox();
		this.GrpTitle = new System.Windows.Forms.GroupBox();
		this.cmbYType = new System.Windows.Forms.ComboBox();
		this.cmbY2Rot = new System.Windows.Forms.ComboBox();
		this.TxtIncY2 = new System.Windows.Forms.ComboBox();
		this.cmbYRot = new System.Windows.Forms.ComboBox();
		this.lblType = new System.Windows.Forms.Label();
		this.lblrot = new System.Windows.Forms.Label();
		this.cmbXType = new System.Windows.Forms.ComboBox();
		this.cmbXRot = new System.Windows.Forms.ComboBox();
		this.TxtHiX = new System.Windows.Forms.ComboBox();
		this.TxtLoX = new System.Windows.Forms.ComboBox();
		this.TxtIncX = new System.Windows.Forms.ComboBox();
		this.TxtHiY2 = new System.Windows.Forms.ComboBox();
		this.TxtLoY2 = new System.Windows.Forms.ComboBox();
		this.TxtIncY = new System.Windows.Forms.ComboBox();
		this.LblInc = new System.Windows.Forms.Label();
		this.TxtHiY = new System.Windows.Forms.ComboBox();
		this.TxtLOY = new System.Windows.Forms.ComboBox();
		this.LblHigh = new System.Windows.Forms.Label();
		this.LblLow = new System.Windows.Forms.Label();
		this.TxtY2Axis = new System.Windows.Forms.TextBox();
		this.TxtYAxis = new System.Windows.Forms.TextBox();
		this.TxtXAxis = new System.Windows.Forms.TextBox();
		this.LblY2 = new System.Windows.Forms.Label();
		this.LblY = new System.Windows.Forms.Label();
		this.LblX = new System.Windows.Forms.Label();
		this.LblTitle = new System.Windows.Forms.Label();
		this.GrpGrid = new System.Windows.Forms.GroupBox();
		this.LblYRefLine4 = new System.Windows.Forms.Label();
		this.YRlineTxt4 = new System.Windows.Forms.TextBox();
		this.YRlineColor4 = new System.Windows.Forms.ComboBox();
		this.YRlineStyle4 = new System.Windows.Forms.ComboBox();
		this.YRLineVal4 = new System.Windows.Forms.TextBox();
		this.YRlineTxt3 = new System.Windows.Forms.TextBox();
		this.YRlineColor3 = new System.Windows.Forms.ComboBox();
		this.YRlineStyle3 = new System.Windows.Forms.ComboBox();
		this.YRLineVal3 = new System.Windows.Forms.TextBox();
		this.LblYRefLine3 = new System.Windows.Forms.Label();
		this.YRlineTxt2 = new System.Windows.Forms.TextBox();
		this.YRlineColor2 = new System.Windows.Forms.ComboBox();
		this.YRlineStyle2 = new System.Windows.Forms.ComboBox();
		this.YRLineVal2 = new System.Windows.Forms.TextBox();
		this.LblYRefLine2 = new System.Windows.Forms.Label();
		this.YRlineTxt1 = new System.Windows.Forms.TextBox();
		this.YRlineColor1 = new System.Windows.Forms.ComboBox();
		this.YRlineStyle1 = new System.Windows.Forms.ComboBox();
		this.YRLineVal1 = new System.Windows.Forms.TextBox();
		this.LblYRefLine1 = new System.Windows.Forms.Label();
		this.XRlineTxt4 = new System.Windows.Forms.TextBox();
		this.XRlineColor4 = new System.Windows.Forms.ComboBox();
		this.XRlineStyle4 = new System.Windows.Forms.ComboBox();
		this.XRLineVal4 = new System.Windows.Forms.TextBox();
		this.LblXRefLine4 = new System.Windows.Forms.Label();
		this.XRlineTxt3 = new System.Windows.Forms.TextBox();
		this.XRlineColor3 = new System.Windows.Forms.ComboBox();
		this.XRlineStyle3 = new System.Windows.Forms.ComboBox();
		this.XRLineVal3 = new System.Windows.Forms.TextBox();
		this.LblXRefLine3 = new System.Windows.Forms.Label();
		this.XRlineTxt2 = new System.Windows.Forms.TextBox();
		this.XRlineColor2 = new System.Windows.Forms.ComboBox();
		this.XRlineStyle2 = new System.Windows.Forms.ComboBox();
		this.XRLineVal2 = new System.Windows.Forms.TextBox();
		this.LblXRefLine2 = new System.Windows.Forms.Label();
		this.LblRefLineDesc = new System.Windows.Forms.Label();
		this.XRlineTxt1 = new System.Windows.Forms.TextBox();
		this.XRlineColor1 = new System.Windows.Forms.ComboBox();
		this.XRlineStyle1 = new System.Windows.Forms.ComboBox();
		this.XRLineVal1 = new System.Windows.Forms.TextBox();
		this.LblXRefLine1 = new System.Windows.Forms.Label();
		this.Y_Minor_Ticks = new System.Windows.Forms.CheckBox();
		this.Y_Major_Ticks = new System.Windows.Forms.CheckBox();
		this.X_Minor_Ticks = new System.Windows.Forms.CheckBox();
		this.X_Major_Ticks = new System.Windows.Forms.CheckBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.Y_Minor_Grid = new System.Windows.Forms.CheckBox();
		this.Y_Major_Grid = new System.Windows.Forms.CheckBox();
		this.X_Minor_Grid = new System.Windows.Forms.CheckBox();
		this.X_Major_Grid = new System.Windows.Forms.CheckBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.GrpFrame = new System.Windows.Forms.GroupBox();
		this.FrameColor = new System.Windows.Forms.ComboBox();
		this.FrameMarker = new System.Windows.Forms.TextBox();
		this.lblFMarker = new System.Windows.Forms.Label();
		this.Label11 = new System.Windows.Forms.Label();
		this.FrameY = new System.Windows.Forms.TextBox();
		this.Label9 = new System.Windows.Forms.Label();
		this.FrameX = new System.Windows.Forms.TextBox();
		this.Label6 = new System.Windows.Forms.Label();
		this.TabCols = new System.Windows.Forms.TabPage();
		this.TxtInFile = new System.Windows.Forms.ComboBox();
		this.Chk1Image = new System.Windows.Forms.CheckBox();
		this.LblWhere = new System.Windows.Forms.Label();
		this.GridWhere = new System.Windows.Forms.DataGridView();
		this.ColAnd = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColParenO = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColGrid = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColOprGrid = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColValGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col2Grid = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColParenC = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.LblRow = new System.Windows.Forms.Label();
		this.LblOption1 = new System.Windows.Forms.Label();
		this.LstGroup = new System.Windows.Forms.ListBox();
		this.ContextMenuXYByGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
		this.CmdGroup = new System.Windows.Forms.Button();
		this.LblOption0 = new System.Windows.Forms.Label();
		this.LblFormTitle = new System.Windows.Forms.Label();
		this.LblRoles = new System.Windows.Forms.Label();
		this.LstBy = new System.Windows.Forms.ListBox();
		this.LstX = new System.Windows.Forms.ListBox();
		this.LstY = new System.Windows.Forms.ListBox();
		this.LblCols = new System.Windows.Forms.Label();
		this.LstColumns = new System.Windows.Forms.ListBox();
		this.ContextMenuCols = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuX = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuY = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuBy = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGroup = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuVal = new System.Windows.Forms.ToolStripMenuItem();
		this.LblInFile = new System.Windows.Forms.Label();
		this.LstOther = new System.Windows.Forms.ListBox();
		this.CmdVal = new System.Windows.Forms.Button();
		this.LstVal = new System.Windows.Forms.ListBox();
		this.TabChart = new System.Windows.Forms.TabControl();
		this.TabTheme = new System.Windows.Forms.TabPage();
		this.Label3 = new System.Windows.Forms.Label();
		this.GridTheme = new System.Windows.Forms.DataGridView();
		this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmbSym = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.cmblinetype = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.DataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TabOptions = new System.Windows.Forms.TabPage();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label7 = new System.Windows.Forms.Label();
		this.Label31 = new System.Windows.Forms.Label();
		this.Label30 = new System.Windows.Forms.Label();
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
		this.Label15 = new System.Windows.Forms.Label();
		this.Label16 = new System.Windows.Forms.Label();
		this.Label17 = new System.Windows.Forms.Label();
		this.Label18 = new System.Windows.Forms.Label();
		this.Label20 = new System.Windows.Forms.Label();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuThemeStrip = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuLoadTheme = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveTheme = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuStdTheme = new System.Windows.Forms.ToolStripComboBox();
		this.mnuRefreshTheme = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuComputations = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuXCompute = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuDataLabels = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuKeepUnusedCharts = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.ContextMenuColEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnusetColor = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClearColor = new System.Windows.Forms.ToolStripMenuItem();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.cmbY2Type = new System.Windows.Forms.ComboBox();
		this.TabTitles.SuspendLayout();
		this.GrpRLegend.SuspendLayout();
		this.GrpRowLegend.SuspendLayout();
		this.GrpTitle.SuspendLayout();
		this.GrpGrid.SuspendLayout();
		this.GrpFrame.SuspendLayout();
		this.TabCols.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridWhere).BeginInit();
		this.ContextMenuXYByGroup.SuspendLayout();
		this.ContextMenuCols.SuspendLayout();
		this.TabChart.SuspendLayout();
		this.TabTheme.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridTheme).BeginInit();
		this.MenuStrip1.SuspendLayout();
		this.ContextMenuColEdit.SuspendLayout();
		base.SuspendLayout();
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdOK.ImageIndex = 3;
		this.cmdOK.Location = new System.Drawing.Point(625, 10);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(77, 50);
		this.cmdOK.TabIndex = 24;
		this.cmdOK.Text = "OK";
		this.cmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdOK, "Commit Changes");
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdCancel.ImageIndex = 0;
		this.cmdCancel.Location = new System.Drawing.Point(625, 65);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(77, 50);
		this.cmdCancel.TabIndex = 25;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdCancel, "Cancel changes");
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.CmdRemove.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdRemove.ImageIndex = 1;
		this.CmdRemove.Location = new System.Drawing.Point(625, 149);
		this.CmdRemove.Margin = new System.Windows.Forms.Padding(4);
		this.CmdRemove.Name = "CmdRemove";
		this.CmdRemove.Size = new System.Drawing.Size(77, 50);
		this.CmdRemove.TabIndex = 17;
		this.CmdRemove.Text = "Remove";
		this.CmdRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdRemove, "Remove the selected columns");
		this.CmdRemove.UseVisualStyleBackColor = true;
		this.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdClear.ImageIndex = 1;
		this.cmdClear.Location = new System.Drawing.Point(625, 207);
		this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(77, 50);
		this.cmdClear.TabIndex = 18;
		this.cmdClear.Text = "Clear";
		this.cmdClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear all column selections");
		this.cmdClear.UseVisualStyleBackColor = true;
		this.CmdColHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdColHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdColHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdColHelp.ImageIndex = 4;
		this.CmdColHelp.Location = new System.Drawing.Point(103, 119);
		this.CmdColHelp.Margin = new System.Windows.Forms.Padding(4);
		this.CmdColHelp.Name = "CmdColHelp";
		this.CmdColHelp.Size = new System.Drawing.Size(39, 25);
		this.CmdColHelp.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.CmdColHelp, "Important note on the _Alli_ column");
		this.CmdColHelp.UseVisualStyleBackColor = true;
		this.CmdUp.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdUp.ImageIndex = 5;
		this.CmdUp.Location = new System.Drawing.Point(629, 533);
		this.CmdUp.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp.Name = "CmdUp";
		this.CmdUp.Size = new System.Drawing.Size(77, 32);
		this.CmdUp.TabIndex = 22;
		this.ToolTip1.SetToolTip(this.CmdUp, "Move Up");
		this.CmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.cmdDown.ImageIndex = 6;
		this.cmdDown.Location = new System.Drawing.Point(629, 566);
		this.cmdDown.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(77, 32);
		this.cmdDown.TabIndex = 23;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.cmdDelete.ImageIndex = 7;
		this.cmdDelete.Location = new System.Drawing.Point(629, 613);
		this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.Size = new System.Drawing.Size(77, 32);
		this.cmdDelete.TabIndex = 24;
		this.ToolTip1.SetToolTip(this.cmdDelete, "Delete Row");
		this.cmdDelete.UseVisualStyleBackColor = true;
		this.TxtPerRow.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.TxtPerRow.FormattingEnabled = true;
		this.TxtPerRow.Items.AddRange(new object[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "-1" });
		this.TxtPerRow.Location = new System.Drawing.Point(183, 85);
		this.TxtPerRow.Margin = new System.Windows.Forms.Padding(4);
		this.TxtPerRow.Name = "TxtPerRow";
		this.TxtPerRow.Size = new System.Drawing.Size(421, 24);
		this.TxtPerRow.TabIndex = 5;
		this.TxtPerRow.Text = "1";
		this.ToolTip1.SetToolTip(this.TxtPerRow, "How many plots do you want per row on the results window. set to -1 to disable");
		this.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.cmdBrowse.AutoEllipsis = true;
		this.cmdBrowse.Location = new System.Drawing.Point(564, 47);
		this.cmdBrowse.Margin = new System.Windows.Forms.Padding(4);
		this.cmdBrowse.Name = "cmdBrowse";
		this.cmdBrowse.Size = new System.Drawing.Size(41, 27);
		this.cmdBrowse.TabIndex = 4;
		this.cmdBrowse.Text = "...";
		this.ToolTip1.SetToolTip(this.cmdBrowse, "Browse");
		this.cmdBrowse.UseVisualStyleBackColor = true;
		this.cmdBrowse.Visible = false;
		this.TxtTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTitle.Location = new System.Drawing.Point(97, 17);
		this.TxtTitle.Margin = new System.Windows.Forms.Padding(4);
		this.TxtTitle.Name = "TxtTitle";
		this.TxtTitle.Size = new System.Drawing.Size(302, 22);
		this.TxtTitle.TabIndex = 0;
		this.ToolTip1.SetToolTip(this.TxtTitle, "Title. Use <<<spf_by_col1>>> or <<<spf_by_col2>>> to reference values of By variable 1 and 2 respectively");
		this.Lbl1Image.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Lbl1Image.AutoSize = true;
		this.Lbl1Image.Location = new System.Drawing.Point(636, 378);
		this.Lbl1Image.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Lbl1Image.Name = "Lbl1Image";
		this.Lbl1Image.Size = new System.Drawing.Size(58, 17);
		this.Lbl1Image.TabIndex = 50;
		this.Lbl1Image.Text = "1 Image";
		this.ToolTip1.SetToolTip(this.Lbl1Image, "Check to Create By Charts in a Single image");
		this.Lbl1Image.Visible = false;
		this.ComboBox1.FormattingEnabled = true;
		this.ComboBox1.Items.AddRange(new object[6] { "", "1", ".8", ".6", ".4", ".2" });
		this.ComboBox1.Location = new System.Drawing.Point(465, 27);
		this.ComboBox1.Name = "ComboBox1";
		this.ComboBox1.Size = new System.Drawing.Size(46, 25);
		this.ComboBox1.TabIndex = 11;
		this.ComboBox1.Text = "1";
		this.ToolTip1.SetToolTip(this.ComboBox1, "Size Factor for Legend");
		this.CheckBox6.AutoSize = true;
		this.CheckBox6.Location = new System.Drawing.Point(429, 59);
		this.CheckBox6.Name = "CheckBox6";
		this.CheckBox6.Size = new System.Drawing.Size(57, 17);
		this.CheckBox6.TabIndex = 9;
		this.CheckBox6.Text = "Border";
		this.ToolTip1.SetToolTip(this.CheckBox6, "Add Border to the legend");
		this.CheckBox6.UseVisualStyleBackColor = true;
		this.ComboBox2.FormattingEnabled = true;
		this.ComboBox2.Location = new System.Drawing.Point(237, 57);
		this.ComboBox2.Name = "ComboBox2";
		this.ComboBox2.Size = new System.Drawing.Size(174, 25);
		this.ComboBox2.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.ComboBox2, "Background color of legend");
		this.TextBox1.Location = new System.Drawing.Point(238, 22);
		this.TextBox1.Name = "TextBox1";
		this.TextBox1.Size = new System.Drawing.Size(173, 22);
		this.TextBox1.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.TextBox1, "Enter a title for the legend");
		this.ComboBox3.FormattingEnabled = true;
		this.ComboBox3.Items.AddRange(new object[11]
		{
			"", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.ComboBox3.Location = new System.Drawing.Point(64, 54);
		this.ComboBox3.Name = "ComboBox3";
		this.ComboBox3.Size = new System.Drawing.Size(77, 25);
		this.ComboBox3.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.ComboBox3, "Set number of columns in legend");
		this.ComboBox4.DropDownWidth = 200;
		this.ComboBox4.FormattingEnabled = true;
		this.ComboBox4.Items.AddRange(new object[4] { "right", "top", "left", "bottom" });
		this.ComboBox4.Location = new System.Drawing.Point(64, 21);
		this.ComboBox4.Name = "ComboBox4";
		this.ComboBox4.Size = new System.Drawing.Size(77, 25);
		this.ComboBox4.TabIndex = 2;
		this.ComboBox4.Text = "right";
		this.ToolTip1.SetToolTip(this.ComboBox4, "Set position of legend");
		this.CmbLSize.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CmbLSize.FormattingEnabled = true;
		this.CmbLSize.Items.AddRange(new object[10] { "", "1", "0.9", "0.8", "0.7", "0.6", "0.5", "0.4", "0.3", "0.2" });
		this.CmbLSize.Location = new System.Drawing.Point(620, 27);
		this.CmbLSize.Margin = new System.Windows.Forms.Padding(4);
		this.CmbLSize.Name = "CmbLSize";
		this.CmbLSize.Size = new System.Drawing.Size(60, 24);
		this.CmbLSize.TabIndex = 59;
		this.CmbLSize.Text = "0.8";
		this.ToolTip1.SetToolTip(this.CmbLSize, "Size Factor for Legend Title");
		this.CmbLBG.FormattingEnabled = true;
		this.CmbLBG.Items.AddRange(new object[79]
		{
			"transparent", "white", "aliceblue", "antiquewhite", "aquamarine2", "aquamarine4", "azure", "beige", "bisque3", "blueviolet",
			"burlywood", "bisque", "black", "blanchedalmond", "blue", "brown", "cadetblue3", "coral", "cornflowerblue", "cornsilk",
			"cyan", "darkcyan", "darkgoldenrod1", "darkgray", "darkgreen", "darkolivegreen", "darkorange", "darkorchid", "darkred", "darksalmon",
			"darkseagreen", "darkseagreen3", "deepskyblue", "firebrick", "gold", "goldenrod", "gray", "green", "honeydew", "khaki",
			"lavenderblush", "lemonchiffon", "lightblue", "lightgoldenrod", "lightyellow", "linen", "magneta", "midnightblue", "mintcream", "mistyrose",
			"moccasin", "navajowhite", "navy", "orange", "orchid", "palegoldenrod", "palegreen", "peachpuff", "pink", "purple",
			"red", "rosybrown", "royalblue", "salmon", "seagreen", "seashell", "skyblue", "snow", "springgreen", "steelblue",
			"tan", "thistle", "tomato", "turquoise", "violet", "wheat", "whitesmoke", "yellow", "yellowgreen"
		});
		this.CmbLBG.Location = new System.Drawing.Point(316, 57);
		this.CmbLBG.Margin = new System.Windows.Forms.Padding(4);
		this.CmbLBG.Name = "CmbLBG";
		this.CmbLBG.Size = new System.Drawing.Size(231, 24);
		this.CmbLBG.TabIndex = 61;
		this.CmbLBG.Text = "transparent";
		this.ToolTip1.SetToolTip(this.CmbLBG, "Background color of legend");
		this.TxtLTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLTitle.Location = new System.Drawing.Point(317, 22);
		this.TxtLTitle.Margin = new System.Windows.Forms.Padding(4);
		this.TxtLTitle.Name = "TxtLTitle";
		this.TxtLTitle.Size = new System.Drawing.Size(229, 22);
		this.TxtLTitle.TabIndex = 58;
		this.ToolTip1.SetToolTip(this.TxtLTitle, "Enter a title for the legend");
		this.CmbLCols.FormattingEnabled = true;
		this.CmbLCols.Items.AddRange(new object[11]
		{
			"", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.CmbLCols.Location = new System.Drawing.Point(85, 52);
		this.CmbLCols.Margin = new System.Windows.Forms.Padding(4);
		this.CmbLCols.Name = "CmbLCols";
		this.CmbLCols.Size = new System.Drawing.Size(101, 24);
		this.CmbLCols.TabIndex = 60;
		this.ToolTip1.SetToolTip(this.CmbLCols, "Set number of columns in legend");
		this.CmbLPos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmbLPos.DropDownWidth = 200;
		this.CmbLPos.FormattingEnabled = true;
		this.CmbLPos.Items.AddRange(new object[4] { "right", "top", "left", "bottom" });
		this.CmbLPos.Location = new System.Drawing.Point(85, 21);
		this.CmbLPos.Margin = new System.Windows.Forms.Padding(4);
		this.CmbLPos.Name = "CmbLPos";
		this.CmbLPos.Size = new System.Drawing.Size(101, 24);
		this.CmbLPos.TabIndex = 57;
		this.CmbLPos.Text = "right";
		this.ToolTip1.SetToolTip(this.CmbLPos, "Set position of legend");
		this.CmbLKSize.FormattingEnabled = true;
		this.CmbLKSize.Items.AddRange(new object[10] { "", "1", "0.9", "0.8", "0.7", "0.6", "0.5", "0.4", "0.3", "0.2" });
		this.CmbLKSize.Location = new System.Drawing.Point(617, 54);
		this.CmbLKSize.Margin = new System.Windows.Forms.Padding(4);
		this.CmbLKSize.Name = "CmbLKSize";
		this.CmbLKSize.Size = new System.Drawing.Size(61, 24);
		this.CmbLKSize.TabIndex = 62;
		this.CmbLKSize.Text = "0.9";
		this.ToolTip1.SetToolTip(this.CmbLKSize, "Size Factor for Legend Key");
		this.YRTxtAdj1.Location = new System.Drawing.Point(664, 196);
		this.YRTxtAdj1.Margin = new System.Windows.Forms.Padding(4);
		this.YRTxtAdj1.Name = "YRTxtAdj1";
		this.YRTxtAdj1.Size = new System.Drawing.Size(32, 23);
		this.YRTxtAdj1.TabIndex = 40;
		this.YRTxtAdj1.Text = "-.5";
		this.ToolTip1.SetToolTip(this.YRTxtAdj1, "Y-Adjustment for Text. - is up. E.g., -.5 usually works");
		this.YRTxtAdj1.Visible = false;
		this.cmdDel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDel2.ImageIndex = 7;
		this.cmdDel2.Location = new System.Drawing.Point(627, 201);
		this.cmdDel2.Margin = new System.Windows.Forms.Padding(4);
		this.cmdDel2.Name = "cmdDel2";
		this.cmdDel2.Size = new System.Drawing.Size(77, 32);
		this.cmdDel2.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmdDel2, "Delete");
		this.cmdDel2.UseVisualStyleBackColor = true;
		this.cmddown2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmddown2.ImageIndex = 6;
		this.cmddown2.Location = new System.Drawing.Point(627, 139);
		this.cmddown2.Margin = new System.Windows.Forms.Padding(4);
		this.cmddown2.Name = "cmddown2";
		this.cmddown2.Size = new System.Drawing.Size(77, 32);
		this.cmddown2.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.cmddown2, "Move Row Down");
		this.cmddown2.UseVisualStyleBackColor = true;
		this.cmdUp2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp2.ImageIndex = 5;
		this.cmdUp2.Location = new System.Drawing.Point(627, 103);
		this.cmdUp2.Margin = new System.Windows.Forms.Padding(4);
		this.cmdUp2.Name = "cmdUp2";
		this.cmdUp2.Size = new System.Drawing.Size(77, 32);
		this.cmdUp2.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.cmdUp2, "Move Row Up");
		this.cmdUp2.UseVisualStyleBackColor = true;
		this.cmdClear2.ImageIndex = 1;
		this.cmdClear2.Location = new System.Drawing.Point(627, 252);
		this.cmdClear2.Margin = new System.Windows.Forms.Padding(4);
		this.cmdClear2.Name = "cmdClear2";
		this.cmdClear2.Size = new System.Drawing.Size(77, 32);
		this.cmdClear2.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.cmdClear2, "Clear");
		this.cmdClear2.UseVisualStyleBackColor = true;
		this.chkHTTP.AutoSize = true;
		this.chkHTTP.Location = new System.Drawing.Point(497, 13);
		this.chkHTTP.Name = "chkHTTP";
		this.chkHTTP.Size = new System.Drawing.Size(59, 21);
		this.chkHTTP.TabIndex = 1;
		this.chkHTTP.Text = "Web";
		this.ToolTip1.SetToolTip(this.chkHTTP, "Get data from web");
		this.chkHTTP.UseVisualStyleBackColor = true;
		this.cmdRefresh.Location = new System.Drawing.Point(566, 10);
		this.cmdRefresh.Name = "cmdRefresh";
		this.cmdRefresh.Size = new System.Drawing.Size(41, 27);
		this.cmdRefresh.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.cmdRefresh, "Refresh Data");
		this.cmdRefresh.UseVisualStyleBackColor = true;
		this.CmbY.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmbY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbY.DropDownWidth = 200;
		this.CmbY.FormattingEnabled = true;
		this.CmbY.Location = new System.Drawing.Point(263, 217);
		this.CmbY.Margin = new System.Windows.Forms.Padding(4);
		this.CmbY.Name = "CmbY";
		this.CmbY.Size = new System.Drawing.Size(100, 24);
		this.CmbY.Sorted = true;
		this.CmbY.TabIndex = 8;
		this.CmdY.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdY.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdY.ImageIndex = 5;
		this.CmdY.Location = new System.Drawing.Point(263, 149);
		this.CmdY.Margin = new System.Windows.Forms.Padding(4);
		this.CmdY.Name = "CmdY";
		this.CmdY.Size = new System.Drawing.Size(101, 49);
		this.CmdY.TabIndex = 9;
		this.CmdY.Text = "Y";
		this.CmdY.UseVisualStyleBackColor = true;
		this.CmdX.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdX.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdX.ImageIndex = 6;
		this.CmdX.Location = new System.Drawing.Point(263, 263);
		this.CmdX.Margin = new System.Windows.Forms.Padding(4);
		this.CmdX.Name = "CmdX";
		this.CmdX.Size = new System.Drawing.Size(101, 49);
		this.CmdX.TabIndex = 10;
		this.CmdX.Text = "X";
		this.CmdX.UseVisualStyleBackColor = true;
		this.cmdBy.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.cmdBy.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdBy.ImageIndex = 2;
		this.cmdBy.Location = new System.Drawing.Point(263, 378);
		this.cmdBy.Margin = new System.Windows.Forms.Padding(4);
		this.cmdBy.Name = "cmdBy";
		this.cmdBy.Size = new System.Drawing.Size(101, 49);
		this.cmdBy.TabIndex = 11;
		this.cmdBy.Text = "By";
		this.cmdBy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdBy.UseVisualStyleBackColor = true;
		this.cmdExtra.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdExtra.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdExtra.ImageKey = "(none)";
		this.cmdExtra.Location = new System.Drawing.Point(625, 263);
		this.cmdExtra.Margin = new System.Windows.Forms.Padding(4);
		this.cmdExtra.Name = "cmdExtra";
		this.cmdExtra.Size = new System.Drawing.Size(77, 50);
		this.cmdExtra.TabIndex = 19;
		this.cmdExtra.Text = "Extra";
		this.cmdExtra.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdExtra.UseVisualStyleBackColor = true;
		this.TabTitles.Controls.Add(this.GrpRLegend);
		this.TabTitles.Controls.Add(this.GrpRowLegend);
		this.TabTitles.Controls.Add(this.GrpTitle);
		this.TabTitles.Controls.Add(this.GrpGrid);
		this.TabTitles.Controls.Add(this.GrpFrame);
		this.TabTitles.Location = new System.Drawing.Point(4, 25);
		this.TabTitles.Margin = new System.Windows.Forms.Padding(4);
		this.TabTitles.Name = "TabTitles";
		this.TabTitles.Padding = new System.Windows.Forms.Padding(4);
		this.TabTitles.Size = new System.Drawing.Size(717, 665);
		this.TabTitles.TabIndex = 1;
		this.TabTitles.Text = "Titles/Grid";
		this.TabTitles.UseVisualStyleBackColor = true;
		this.GrpRLegend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrpRLegend.Controls.Add(this.CmbLKSize);
		this.GrpRLegend.Controls.Add(this.lblly);
		this.GrpRLegend.Controls.Add(this.CmbLSize);
		this.GrpRLegend.Controls.Add(this.lbllx);
		this.GrpRLegend.Controls.Add(this.CmbLBG);
		this.GrpRLegend.Controls.Add(this.Label23);
		this.GrpRLegend.Controls.Add(this.TxtLTitle);
		this.GrpRLegend.Controls.Add(this.Label24);
		this.GrpRLegend.Controls.Add(this.CmbLCols);
		this.GrpRLegend.Controls.Add(this.lbllcols);
		this.GrpRLegend.Controls.Add(this.CmbLPos);
		this.GrpRLegend.Controls.Add(this.Label26);
		this.GrpRLegend.Location = new System.Drawing.Point(5, 568);
		this.GrpRLegend.Margin = new System.Windows.Forms.Padding(4);
		this.GrpRLegend.Name = "GrpRLegend";
		this.GrpRLegend.Padding = new System.Windows.Forms.Padding(4);
		this.GrpRLegend.Size = new System.Drawing.Size(707, 98);
		this.GrpRLegend.TabIndex = 78;
		this.GrpRLegend.TabStop = false;
		this.GrpRLegend.Text = "Legend";
		this.GrpRLegend.Visible = false;
		this.lblly.AutoSize = true;
		this.lblly.Location = new System.Drawing.Point(563, 57);
		this.lblly.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblly.Name = "lblly";
		this.lblly.Size = new System.Drawing.Size(48, 17);
		this.lblly.TabIndex = 12;
		this.lblly.Text = "KSize:";
		this.lbllx.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbllx.AutoSize = true;
		this.lbllx.Location = new System.Drawing.Point(563, 27);
		this.lbllx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lbllx.Name = "lbllx";
		this.lbllx.Size = new System.Drawing.Size(48, 17);
		this.lbllx.TabIndex = 10;
		this.lbllx.Text = "TSize:";
		this.Label23.AutoSize = true;
		this.Label23.Location = new System.Drawing.Point(217, 57);
		this.Label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label23.Name = "Label23";
		this.Label23.Size = new System.Drawing.Size(88, 17);
		this.Label23.TabIndex = 7;
		this.Label23.Text = "Background:";
		this.Label24.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Label24.AutoSize = true;
		this.Label24.Location = new System.Drawing.Point(217, 25);
		this.Label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label24.Name = "Label24";
		this.Label24.Size = new System.Drawing.Size(91, 17);
		this.Label24.TabIndex = 5;
		this.Label24.Text = "Legend Title:";
		this.lbllcols.AutoSize = true;
		this.lbllcols.Location = new System.Drawing.Point(8, 52);
		this.lbllcols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lbllcols.Name = "lbllcols";
		this.lbllcols.Size = new System.Drawing.Size(66, 17);
		this.lbllcols.TabIndex = 3;
		this.lbllcols.Text = "Columns:";
		this.Label26.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label26.AutoSize = true;
		this.Label26.Location = new System.Drawing.Point(8, 22);
		this.Label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label26.Name = "Label26";
		this.Label26.Size = new System.Drawing.Size(62, 17);
		this.Label26.TabIndex = 1;
		this.Label26.Text = "Position:";
		this.GrpRowLegend.Controls.Add(this.Label21);
		this.GrpRowLegend.Controls.Add(this.RLMarker2);
		this.GrpRowLegend.Controls.Add(this.RLColor2);
		this.GrpRowLegend.Controls.Add(this.RLCol2);
		this.GrpRowLegend.Controls.Add(this.Label19);
		this.GrpRowLegend.Controls.Add(this.RLMarker1);
		this.GrpRowLegend.Controls.Add(this.RLColor1);
		this.GrpRowLegend.Controls.Add(this.RLCol1);
		this.GrpRowLegend.Location = new System.Drawing.Point(5, 566);
		this.GrpRowLegend.Margin = new System.Windows.Forms.Padding(4);
		this.GrpRowLegend.Name = "GrpRowLegend";
		this.GrpRowLegend.Padding = new System.Windows.Forms.Padding(4);
		this.GrpRowLegend.Size = new System.Drawing.Size(707, 89);
		this.GrpRowLegend.TabIndex = 77;
		this.GrpRowLegend.TabStop = false;
		this.GrpRowLegend.Text = "Row Legend";
		this.Label21.AutoSize = true;
		this.Label21.Location = new System.Drawing.Point(15, 59);
		this.Label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label21.Name = "Label21";
		this.Label21.Size = new System.Drawing.Size(59, 17);
		this.Label21.TabIndex = 7;
		this.Label21.Text = "Column:";
		this.RLMarker2.AutoSize = true;
		this.RLMarker2.Location = new System.Drawing.Point(613, 55);
		this.RLMarker2.Margin = new System.Windows.Forms.Padding(4);
		this.RLMarker2.Name = "RLMarker2";
		this.RLMarker2.Size = new System.Drawing.Size(74, 21);
		this.RLMarker2.TabIndex = 6;
		this.RLMarker2.Text = "Marker";
		this.RLMarker2.UseVisualStyleBackColor = true;
		this.RLColor2.AutoSize = true;
		this.RLColor2.Location = new System.Drawing.Point(483, 55);
		this.RLColor2.Margin = new System.Windows.Forms.Padding(4);
		this.RLColor2.Name = "RLColor2";
		this.RLColor2.Size = new System.Drawing.Size(63, 21);
		this.RLColor2.TabIndex = 5;
		this.RLColor2.Text = "Color";
		this.RLColor2.UseVisualStyleBackColor = true;
		this.RLCol2.FormattingEnabled = true;
		this.RLCol2.Location = new System.Drawing.Point(85, 53);
		this.RLCol2.Margin = new System.Windows.Forms.Padding(4);
		this.RLCol2.Name = "RLCol2";
		this.RLCol2.Size = new System.Drawing.Size(359, 24);
		this.RLCol2.TabIndex = 4;
		this.Label19.AutoSize = true;
		this.Label19.Location = new System.Drawing.Point(15, 27);
		this.Label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label19.Name = "Label19";
		this.Label19.Size = new System.Drawing.Size(59, 17);
		this.Label19.TabIndex = 3;
		this.Label19.Text = "Column:";
		this.RLMarker1.AutoSize = true;
		this.RLMarker1.Location = new System.Drawing.Point(613, 23);
		this.RLMarker1.Margin = new System.Windows.Forms.Padding(4);
		this.RLMarker1.Name = "RLMarker1";
		this.RLMarker1.Size = new System.Drawing.Size(74, 21);
		this.RLMarker1.TabIndex = 2;
		this.RLMarker1.Text = "Marker";
		this.RLMarker1.UseVisualStyleBackColor = true;
		this.RLColor1.AutoSize = true;
		this.RLColor1.Location = new System.Drawing.Point(483, 23);
		this.RLColor1.Margin = new System.Windows.Forms.Padding(4);
		this.RLColor1.Name = "RLColor1";
		this.RLColor1.Size = new System.Drawing.Size(63, 21);
		this.RLColor1.TabIndex = 1;
		this.RLColor1.Text = "Color";
		this.RLColor1.UseVisualStyleBackColor = true;
		this.RLCol1.FormattingEnabled = true;
		this.RLCol1.Location = new System.Drawing.Point(85, 21);
		this.RLCol1.Margin = new System.Windows.Forms.Padding(4);
		this.RLCol1.Name = "RLCol1";
		this.RLCol1.Size = new System.Drawing.Size(359, 24);
		this.RLCol1.TabIndex = 0;
		this.GrpTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrpTitle.Controls.Add(this.cmbY2Type);
		this.GrpTitle.Controls.Add(this.cmbYType);
		this.GrpTitle.Controls.Add(this.cmbY2Rot);
		this.GrpTitle.Controls.Add(this.TxtIncY2);
		this.GrpTitle.Controls.Add(this.cmbYRot);
		this.GrpTitle.Controls.Add(this.lblType);
		this.GrpTitle.Controls.Add(this.lblrot);
		this.GrpTitle.Controls.Add(this.cmbXType);
		this.GrpTitle.Controls.Add(this.cmbXRot);
		this.GrpTitle.Controls.Add(this.TxtHiX);
		this.GrpTitle.Controls.Add(this.TxtLoX);
		this.GrpTitle.Controls.Add(this.TxtIncX);
		this.GrpTitle.Controls.Add(this.TxtHiY2);
		this.GrpTitle.Controls.Add(this.TxtLoY2);
		this.GrpTitle.Controls.Add(this.TxtIncY);
		this.GrpTitle.Controls.Add(this.LblInc);
		this.GrpTitle.Controls.Add(this.TxtHiY);
		this.GrpTitle.Controls.Add(this.TxtLOY);
		this.GrpTitle.Controls.Add(this.LblHigh);
		this.GrpTitle.Controls.Add(this.LblLow);
		this.GrpTitle.Controls.Add(this.TxtY2Axis);
		this.GrpTitle.Controls.Add(this.TxtYAxis);
		this.GrpTitle.Controls.Add(this.TxtXAxis);
		this.GrpTitle.Controls.Add(this.TxtTitle);
		this.GrpTitle.Controls.Add(this.LblY2);
		this.GrpTitle.Controls.Add(this.LblY);
		this.GrpTitle.Controls.Add(this.LblX);
		this.GrpTitle.Controls.Add(this.LblTitle);
		this.GrpTitle.Location = new System.Drawing.Point(5, 4);
		this.GrpTitle.Margin = new System.Windows.Forms.Padding(4);
		this.GrpTitle.Name = "GrpTitle";
		this.GrpTitle.Padding = new System.Windows.Forms.Padding(4);
		this.GrpTitle.Size = new System.Drawing.Size(704, 137);
		this.GrpTitle.TabIndex = 76;
		this.GrpTitle.TabStop = false;
		this.GrpTitle.Text = "Titles";
		this.cmbYType.FormattingEnabled = true;
		this.cmbYType.Location = new System.Drawing.Point(406, 71);
		this.cmbYType.Name = "cmbYType";
		this.cmbYType.Size = new System.Drawing.Size(53, 24);
		this.cmbYType.TabIndex = 75;
		this.cmbY2Rot.DropDownWidth = 100;
		this.cmbY2Rot.FormattingEnabled = true;
		this.cmbY2Rot.Location = new System.Drawing.Point(646, 102);
		this.cmbY2Rot.Name = "cmbY2Rot";
		this.cmbY2Rot.Size = new System.Drawing.Size(53, 24);
		this.cmbY2Rot.TabIndex = 74;
		this.TxtIncY2.DropDownWidth = 100;
		this.TxtIncY2.FormattingEnabled = true;
		this.TxtIncY2.Location = new System.Drawing.Point(586, 102);
		this.TxtIncY2.Name = "TxtIncY2";
		this.TxtIncY2.Size = new System.Drawing.Size(53, 24);
		this.TxtIncY2.TabIndex = 73;
		this.cmbYRot.DropDownWidth = 100;
		this.cmbYRot.FormattingEnabled = true;
		this.cmbYRot.Location = new System.Drawing.Point(646, 71);
		this.cmbYRot.Name = "cmbYRot";
		this.cmbYRot.Size = new System.Drawing.Size(53, 24);
		this.cmbYRot.TabIndex = 72;
		this.lblType.AutoSize = true;
		this.lblType.Location = new System.Drawing.Point(414, 14);
		this.lblType.Name = "lblType";
		this.lblType.Size = new System.Drawing.Size(40, 17);
		this.lblType.TabIndex = 71;
		this.lblType.Text = "Type";
		this.lblrot.AutoSize = true;
		this.lblrot.Location = new System.Drawing.Point(651, 14);
		this.lblrot.Name = "lblrot";
		this.lblrot.Size = new System.Drawing.Size(30, 17);
		this.lblrot.TabIndex = 70;
		this.lblrot.Text = "Rot";
		this.cmbXType.DropDownWidth = 100;
		this.cmbXType.FormattingEnabled = true;
		this.cmbXType.Location = new System.Drawing.Point(406, 43);
		this.cmbXType.Name = "cmbXType";
		this.cmbXType.Size = new System.Drawing.Size(53, 24);
		this.cmbXType.TabIndex = 69;
		this.cmbXRot.DropDownWidth = 100;
		this.cmbXRot.FormattingEnabled = true;
		this.cmbXRot.Location = new System.Drawing.Point(646, 43);
		this.cmbXRot.Name = "cmbXRot";
		this.cmbXRot.Size = new System.Drawing.Size(53, 24);
		this.cmbXRot.TabIndex = 68;
		this.TxtHiX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHiX.DropDownWidth = 100;
		this.TxtHiX.FormattingEnabled = true;
		this.TxtHiX.Location = new System.Drawing.Point(526, 43);
		this.TxtHiX.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHiX.Name = "TxtHiX";
		this.TxtHiX.Size = new System.Drawing.Size(53, 24);
		this.TxtHiX.TabIndex = 5;
		this.TxtLoX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLoX.DropDownWidth = 100;
		this.TxtLoX.FormattingEnabled = true;
		this.TxtLoX.Location = new System.Drawing.Point(466, 43);
		this.TxtLoX.Margin = new System.Windows.Forms.Padding(4);
		this.TxtLoX.Name = "TxtLoX";
		this.TxtLoX.Size = new System.Drawing.Size(53, 24);
		this.TxtLoX.TabIndex = 4;
		this.TxtIncX.DropDownWidth = 100;
		this.TxtIncX.FormattingEnabled = true;
		this.TxtIncX.Location = new System.Drawing.Point(586, 43);
		this.TxtIncX.Margin = new System.Windows.Forms.Padding(4);
		this.TxtIncX.Name = "TxtIncX";
		this.TxtIncX.Size = new System.Drawing.Size(53, 24);
		this.TxtIncX.TabIndex = 6;
		this.TxtHiY2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHiY2.DropDownWidth = 100;
		this.TxtHiY2.FormattingEnabled = true;
		this.TxtHiY2.Location = new System.Drawing.Point(526, 102);
		this.TxtHiY2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHiY2.Name = "TxtHiY2";
		this.TxtHiY2.Size = new System.Drawing.Size(53, 24);
		this.TxtHiY2.TabIndex = 11;
		this.TxtLoY2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLoY2.DropDownWidth = 100;
		this.TxtLoY2.FormattingEnabled = true;
		this.TxtLoY2.Location = new System.Drawing.Point(466, 102);
		this.TxtLoY2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtLoY2.Name = "TxtLoY2";
		this.TxtLoY2.Size = new System.Drawing.Size(53, 24);
		this.TxtLoY2.TabIndex = 10;
		this.TxtIncY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtIncY.DropDownWidth = 100;
		this.TxtIncY.FormattingEnabled = true;
		this.TxtIncY.Location = new System.Drawing.Point(586, 71);
		this.TxtIncY.Margin = new System.Windows.Forms.Padding(4);
		this.TxtIncY.Name = "TxtIncY";
		this.TxtIncY.Size = new System.Drawing.Size(53, 24);
		this.TxtIncY.TabIndex = 9;
		this.LblInc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblInc.AutoSize = true;
		this.LblInc.Location = new System.Drawing.Point(595, 14);
		this.LblInc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInc.Name = "LblInc";
		this.LblInc.Size = new System.Drawing.Size(26, 17);
		this.LblInc.TabIndex = 67;
		this.LblInc.Text = "Inc";
		this.TxtHiY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHiY.DropDownWidth = 100;
		this.TxtHiY.FormattingEnabled = true;
		this.TxtHiY.Location = new System.Drawing.Point(526, 71);
		this.TxtHiY.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHiY.Name = "TxtHiY";
		this.TxtHiY.Size = new System.Drawing.Size(53, 24);
		this.TxtHiY.TabIndex = 8;
		this.TxtLOY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLOY.DropDownWidth = 100;
		this.TxtLOY.FormattingEnabled = true;
		this.TxtLOY.Location = new System.Drawing.Point(466, 71);
		this.TxtLOY.Margin = new System.Windows.Forms.Padding(4);
		this.TxtLOY.Name = "TxtLOY";
		this.TxtLOY.Size = new System.Drawing.Size(53, 24);
		this.TxtLOY.TabIndex = 7;
		this.LblHigh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblHigh.AutoSize = true;
		this.LblHigh.Location = new System.Drawing.Point(533, 14);
		this.LblHigh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHigh.Name = "LblHigh";
		this.LblHigh.Size = new System.Drawing.Size(37, 17);
		this.LblHigh.TabIndex = 8;
		this.LblHigh.Text = "High";
		this.LblLow.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblLow.AutoSize = true;
		this.LblLow.Location = new System.Drawing.Point(475, 14);
		this.LblLow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblLow.Name = "LblLow";
		this.LblLow.Size = new System.Drawing.Size(33, 17);
		this.LblLow.TabIndex = 7;
		this.LblLow.Text = "Low";
		this.TxtY2Axis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtY2Axis.Location = new System.Drawing.Point(97, 102);
		this.TxtY2Axis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtY2Axis.Name = "TxtY2Axis";
		this.TxtY2Axis.Size = new System.Drawing.Size(302, 22);
		this.TxtY2Axis.TabIndex = 3;
		this.TxtYAxis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtYAxis.Location = new System.Drawing.Point(97, 71);
		this.TxtYAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtYAxis.Name = "TxtYAxis";
		this.TxtYAxis.Size = new System.Drawing.Size(302, 22);
		this.TxtYAxis.TabIndex = 2;
		this.TxtXAxis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtXAxis.Location = new System.Drawing.Point(97, 43);
		this.TxtXAxis.Margin = new System.Windows.Forms.Padding(4);
		this.TxtXAxis.Name = "TxtXAxis";
		this.TxtXAxis.Size = new System.Drawing.Size(302, 22);
		this.TxtXAxis.TabIndex = 1;
		this.LblY2.AutoSize = true;
		this.LblY2.Location = new System.Drawing.Point(7, 106);
		this.LblY2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblY2.Name = "LblY2";
		this.LblY2.Size = new System.Drawing.Size(58, 17);
		this.LblY2.TabIndex = 6;
		this.LblY2.Text = "Y2 Axis:";
		this.LblY.AutoSize = true;
		this.LblY.Location = new System.Drawing.Point(7, 78);
		this.LblY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblY.Name = "LblY";
		this.LblY.Size = new System.Drawing.Size(50, 17);
		this.LblY.TabIndex = 5;
		this.LblY.Text = "Y Axis:";
		this.LblX.AutoSize = true;
		this.LblX.Location = new System.Drawing.Point(7, 46);
		this.LblX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblX.Name = "LblX";
		this.LblX.Size = new System.Drawing.Size(54, 17);
		this.LblX.TabIndex = 4;
		this.LblX.Text = "X Axis :";
		this.LblTitle.AutoSize = true;
		this.LblTitle.Location = new System.Drawing.Point(7, 18);
		this.LblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTitle.Name = "LblTitle";
		this.LblTitle.Size = new System.Drawing.Size(77, 17);
		this.LblTitle.TabIndex = 1;
		this.LblTitle.Text = "Chart Title:";
		this.GrpGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrpGrid.BackColor = System.Drawing.Color.Transparent;
		this.GrpGrid.Controls.Add(this.YRTxtAdj1);
		this.GrpGrid.Controls.Add(this.LblYRefLine4);
		this.GrpGrid.Controls.Add(this.YRlineTxt4);
		this.GrpGrid.Controls.Add(this.YRlineColor4);
		this.GrpGrid.Controls.Add(this.YRlineStyle4);
		this.GrpGrid.Controls.Add(this.YRLineVal4);
		this.GrpGrid.Controls.Add(this.YRlineTxt3);
		this.GrpGrid.Controls.Add(this.YRlineColor3);
		this.GrpGrid.Controls.Add(this.YRlineStyle3);
		this.GrpGrid.Controls.Add(this.YRLineVal3);
		this.GrpGrid.Controls.Add(this.LblYRefLine3);
		this.GrpGrid.Controls.Add(this.YRlineTxt2);
		this.GrpGrid.Controls.Add(this.YRlineColor2);
		this.GrpGrid.Controls.Add(this.YRlineStyle2);
		this.GrpGrid.Controls.Add(this.YRLineVal2);
		this.GrpGrid.Controls.Add(this.LblYRefLine2);
		this.GrpGrid.Controls.Add(this.YRlineTxt1);
		this.GrpGrid.Controls.Add(this.YRlineColor1);
		this.GrpGrid.Controls.Add(this.YRlineStyle1);
		this.GrpGrid.Controls.Add(this.YRLineVal1);
		this.GrpGrid.Controls.Add(this.LblYRefLine1);
		this.GrpGrid.Controls.Add(this.XRlineTxt4);
		this.GrpGrid.Controls.Add(this.XRlineColor4);
		this.GrpGrid.Controls.Add(this.XRlineStyle4);
		this.GrpGrid.Controls.Add(this.XRLineVal4);
		this.GrpGrid.Controls.Add(this.LblXRefLine4);
		this.GrpGrid.Controls.Add(this.XRlineTxt3);
		this.GrpGrid.Controls.Add(this.XRlineColor3);
		this.GrpGrid.Controls.Add(this.XRlineStyle3);
		this.GrpGrid.Controls.Add(this.XRLineVal3);
		this.GrpGrid.Controls.Add(this.LblXRefLine3);
		this.GrpGrid.Controls.Add(this.XRlineTxt2);
		this.GrpGrid.Controls.Add(this.XRlineColor2);
		this.GrpGrid.Controls.Add(this.XRlineStyle2);
		this.GrpGrid.Controls.Add(this.XRLineVal2);
		this.GrpGrid.Controls.Add(this.LblXRefLine2);
		this.GrpGrid.Controls.Add(this.LblRefLineDesc);
		this.GrpGrid.Controls.Add(this.XRlineTxt1);
		this.GrpGrid.Controls.Add(this.XRlineColor1);
		this.GrpGrid.Controls.Add(this.XRlineStyle1);
		this.GrpGrid.Controls.Add(this.XRLineVal1);
		this.GrpGrid.Controls.Add(this.LblXRefLine1);
		this.GrpGrid.Controls.Add(this.Y_Minor_Ticks);
		this.GrpGrid.Controls.Add(this.Y_Major_Ticks);
		this.GrpGrid.Controls.Add(this.X_Minor_Ticks);
		this.GrpGrid.Controls.Add(this.X_Major_Ticks);
		this.GrpGrid.Controls.Add(this.Label2);
		this.GrpGrid.Controls.Add(this.Y_Minor_Grid);
		this.GrpGrid.Controls.Add(this.Y_Major_Grid);
		this.GrpGrid.Controls.Add(this.X_Minor_Grid);
		this.GrpGrid.Controls.Add(this.X_Major_Grid);
		this.GrpGrid.Controls.Add(this.Label1);
		this.GrpGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GrpGrid.Location = new System.Drawing.Point(5, 148);
		this.GrpGrid.Margin = new System.Windows.Forms.Padding(4);
		this.GrpGrid.Name = "GrpGrid";
		this.GrpGrid.Padding = new System.Windows.Forms.Padding(4);
		this.GrpGrid.Size = new System.Drawing.Size(707, 325);
		this.GrpGrid.TabIndex = 75;
		this.GrpGrid.TabStop = false;
		this.GrpGrid.Text = "Gridlines / Tickmarks / Reference Lines  ";
		this.LblYRefLine4.AutoSize = true;
		this.LblYRefLine4.Location = new System.Drawing.Point(249, 286);
		this.LblYRefLine4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYRefLine4.Name = "LblYRefLine4";
		this.LblYRefLine4.Size = new System.Drawing.Size(77, 17);
		this.LblYRefLine4.TabIndex = 64;
		this.LblYRefLine4.Text = "Ref Line 4:";
		this.YRlineTxt4.Location = new System.Drawing.Point(575, 287);
		this.YRlineTxt4.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineTxt4.Name = "YRlineTxt4";
		this.YRlineTxt4.Size = new System.Drawing.Size(123, 23);
		this.YRlineTxt4.TabIndex = 52;
		this.YRlineColor4.FormattingEnabled = true;
		this.YRlineColor4.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.YRlineColor4.Location = new System.Drawing.Point(485, 287);
		this.YRlineColor4.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineColor4.Name = "YRlineColor4";
		this.YRlineColor4.Size = new System.Drawing.Size(83, 25);
		this.YRlineColor4.Sorted = true;
		this.YRlineColor4.TabIndex = 51;
		this.YRlineColor4.Text = "Black";
		this.YRlineStyle4.FormattingEnabled = true;
		this.YRlineStyle4.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.YRlineStyle4.Location = new System.Drawing.Point(408, 287);
		this.YRlineStyle4.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineStyle4.Name = "YRlineStyle4";
		this.YRlineStyle4.Size = new System.Drawing.Size(68, 25);
		this.YRlineStyle4.Sorted = true;
		this.YRlineStyle4.TabIndex = 50;
		this.YRlineStyle4.Text = "Solid";
		this.YRLineVal4.Location = new System.Drawing.Point(331, 287);
		this.YRLineVal4.Margin = new System.Windows.Forms.Padding(4);
		this.YRLineVal4.Name = "YRLineVal4";
		this.YRLineVal4.Size = new System.Drawing.Size(68, 23);
		this.YRLineVal4.TabIndex = 49;
		this.YRlineTxt3.Location = new System.Drawing.Point(575, 255);
		this.YRlineTxt3.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineTxt3.Name = "YRlineTxt3";
		this.YRlineTxt3.Size = new System.Drawing.Size(123, 23);
		this.YRlineTxt3.TabIndex = 48;
		this.YRlineColor3.FormattingEnabled = true;
		this.YRlineColor3.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.YRlineColor3.Location = new System.Drawing.Point(485, 255);
		this.YRlineColor3.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineColor3.Name = "YRlineColor3";
		this.YRlineColor3.Size = new System.Drawing.Size(83, 25);
		this.YRlineColor3.Sorted = true;
		this.YRlineColor3.TabIndex = 47;
		this.YRlineColor3.Text = "Black";
		this.YRlineStyle3.FormattingEnabled = true;
		this.YRlineStyle3.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.YRlineStyle3.Location = new System.Drawing.Point(408, 255);
		this.YRlineStyle3.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineStyle3.Name = "YRlineStyle3";
		this.YRlineStyle3.Size = new System.Drawing.Size(68, 25);
		this.YRlineStyle3.Sorted = true;
		this.YRlineStyle3.TabIndex = 46;
		this.YRlineStyle3.Text = "Solid";
		this.YRLineVal3.Location = new System.Drawing.Point(331, 255);
		this.YRLineVal3.Margin = new System.Windows.Forms.Padding(4);
		this.YRLineVal3.Name = "YRLineVal3";
		this.YRLineVal3.Size = new System.Drawing.Size(68, 23);
		this.YRLineVal3.TabIndex = 45;
		this.LblYRefLine3.AutoSize = true;
		this.LblYRefLine3.Location = new System.Drawing.Point(249, 255);
		this.LblYRefLine3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYRefLine3.Name = "LblYRefLine3";
		this.LblYRefLine3.Size = new System.Drawing.Size(77, 17);
		this.LblYRefLine3.TabIndex = 54;
		this.LblYRefLine3.Text = "Ref Line 3:";
		this.YRlineTxt2.Location = new System.Drawing.Point(575, 224);
		this.YRlineTxt2.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineTxt2.Name = "YRlineTxt2";
		this.YRlineTxt2.Size = new System.Drawing.Size(123, 23);
		this.YRlineTxt2.TabIndex = 44;
		this.YRlineColor2.FormattingEnabled = true;
		this.YRlineColor2.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.YRlineColor2.Location = new System.Drawing.Point(485, 224);
		this.YRlineColor2.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineColor2.Name = "YRlineColor2";
		this.YRlineColor2.Size = new System.Drawing.Size(83, 25);
		this.YRlineColor2.Sorted = true;
		this.YRlineColor2.TabIndex = 43;
		this.YRlineColor2.Text = "Black";
		this.YRlineStyle2.FormattingEnabled = true;
		this.YRlineStyle2.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.YRlineStyle2.Location = new System.Drawing.Point(408, 224);
		this.YRlineStyle2.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineStyle2.Name = "YRlineStyle2";
		this.YRlineStyle2.Size = new System.Drawing.Size(68, 25);
		this.YRlineStyle2.Sorted = true;
		this.YRlineStyle2.TabIndex = 42;
		this.YRlineStyle2.Text = "Solid";
		this.YRLineVal2.Location = new System.Drawing.Point(331, 224);
		this.YRLineVal2.Margin = new System.Windows.Forms.Padding(4);
		this.YRLineVal2.Name = "YRLineVal2";
		this.YRLineVal2.Size = new System.Drawing.Size(68, 23);
		this.YRLineVal2.TabIndex = 41;
		this.LblYRefLine2.AutoSize = true;
		this.LblYRefLine2.Location = new System.Drawing.Point(249, 224);
		this.LblYRefLine2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYRefLine2.Name = "LblYRefLine2";
		this.LblYRefLine2.Size = new System.Drawing.Size(77, 17);
		this.LblYRefLine2.TabIndex = 49;
		this.LblYRefLine2.Text = "Ref Line 2:";
		this.YRlineTxt1.Location = new System.Drawing.Point(575, 196);
		this.YRlineTxt1.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineTxt1.Name = "YRlineTxt1";
		this.YRlineTxt1.Size = new System.Drawing.Size(84, 23);
		this.YRlineTxt1.TabIndex = 39;
		this.YRlineColor1.FormattingEnabled = true;
		this.YRlineColor1.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.YRlineColor1.Location = new System.Drawing.Point(485, 196);
		this.YRlineColor1.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineColor1.Name = "YRlineColor1";
		this.YRlineColor1.Size = new System.Drawing.Size(83, 25);
		this.YRlineColor1.Sorted = true;
		this.YRlineColor1.TabIndex = 38;
		this.YRlineColor1.Text = "Black";
		this.YRlineStyle1.FormattingEnabled = true;
		this.YRlineStyle1.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.YRlineStyle1.Location = new System.Drawing.Point(408, 196);
		this.YRlineStyle1.Margin = new System.Windows.Forms.Padding(4);
		this.YRlineStyle1.Name = "YRlineStyle1";
		this.YRlineStyle1.Size = new System.Drawing.Size(68, 25);
		this.YRlineStyle1.Sorted = true;
		this.YRlineStyle1.TabIndex = 37;
		this.YRlineStyle1.Text = "Solid";
		this.YRLineVal1.Location = new System.Drawing.Point(331, 196);
		this.YRLineVal1.Margin = new System.Windows.Forms.Padding(4);
		this.YRLineVal1.Name = "YRLineVal1";
		this.YRLineVal1.Size = new System.Drawing.Size(68, 23);
		this.YRLineVal1.TabIndex = 36;
		this.LblYRefLine1.AutoSize = true;
		this.LblYRefLine1.Location = new System.Drawing.Point(249, 196);
		this.LblYRefLine1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblYRefLine1.Name = "LblYRefLine1";
		this.LblYRefLine1.Size = new System.Drawing.Size(77, 17);
		this.LblYRefLine1.TabIndex = 44;
		this.LblYRefLine1.Text = "Ref Line 1:";
		this.XRlineTxt4.Location = new System.Drawing.Point(575, 148);
		this.XRlineTxt4.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineTxt4.Name = "XRlineTxt4";
		this.XRlineTxt4.Size = new System.Drawing.Size(123, 23);
		this.XRlineTxt4.TabIndex = 35;
		this.XRlineColor4.FormattingEnabled = true;
		this.XRlineColor4.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.XRlineColor4.Location = new System.Drawing.Point(485, 148);
		this.XRlineColor4.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineColor4.Name = "XRlineColor4";
		this.XRlineColor4.Size = new System.Drawing.Size(83, 25);
		this.XRlineColor4.Sorted = true;
		this.XRlineColor4.TabIndex = 34;
		this.XRlineColor4.Text = "Black";
		this.XRlineStyle4.FormattingEnabled = true;
		this.XRlineStyle4.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.XRlineStyle4.Location = new System.Drawing.Point(408, 148);
		this.XRlineStyle4.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineStyle4.Name = "XRlineStyle4";
		this.XRlineStyle4.Size = new System.Drawing.Size(68, 25);
		this.XRlineStyle4.Sorted = true;
		this.XRlineStyle4.TabIndex = 33;
		this.XRlineStyle4.Text = "Solid";
		this.XRLineVal4.Location = new System.Drawing.Point(331, 148);
		this.XRLineVal4.Margin = new System.Windows.Forms.Padding(4);
		this.XRLineVal4.Name = "XRLineVal4";
		this.XRLineVal4.Size = new System.Drawing.Size(68, 23);
		this.XRLineVal4.TabIndex = 32;
		this.LblXRefLine4.AutoSize = true;
		this.LblXRefLine4.Location = new System.Drawing.Point(249, 148);
		this.LblXRefLine4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXRefLine4.Name = "LblXRefLine4";
		this.LblXRefLine4.Size = new System.Drawing.Size(77, 17);
		this.LblXRefLine4.TabIndex = 39;
		this.LblXRefLine4.Text = "Ref Line 4:";
		this.XRlineTxt3.Location = new System.Drawing.Point(575, 118);
		this.XRlineTxt3.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineTxt3.Name = "XRlineTxt3";
		this.XRlineTxt3.Size = new System.Drawing.Size(123, 23);
		this.XRlineTxt3.TabIndex = 31;
		this.XRlineColor3.FormattingEnabled = true;
		this.XRlineColor3.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.XRlineColor3.Location = new System.Drawing.Point(485, 117);
		this.XRlineColor3.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineColor3.Name = "XRlineColor3";
		this.XRlineColor3.Size = new System.Drawing.Size(83, 25);
		this.XRlineColor3.Sorted = true;
		this.XRlineColor3.TabIndex = 30;
		this.XRlineColor3.Text = "Black";
		this.XRlineStyle3.FormattingEnabled = true;
		this.XRlineStyle3.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.XRlineStyle3.Location = new System.Drawing.Point(408, 117);
		this.XRlineStyle3.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineStyle3.Name = "XRlineStyle3";
		this.XRlineStyle3.Size = new System.Drawing.Size(68, 25);
		this.XRlineStyle3.Sorted = true;
		this.XRlineStyle3.TabIndex = 29;
		this.XRlineStyle3.Text = "Solid";
		this.XRLineVal3.Location = new System.Drawing.Point(331, 118);
		this.XRLineVal3.Margin = new System.Windows.Forms.Padding(4);
		this.XRLineVal3.Name = "XRLineVal3";
		this.XRLineVal3.Size = new System.Drawing.Size(68, 23);
		this.XRLineVal3.TabIndex = 28;
		this.LblXRefLine3.AutoSize = true;
		this.LblXRefLine3.Location = new System.Drawing.Point(249, 118);
		this.LblXRefLine3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXRefLine3.Name = "LblXRefLine3";
		this.LblXRefLine3.Size = new System.Drawing.Size(77, 17);
		this.LblXRefLine3.TabIndex = 34;
		this.LblXRefLine3.Text = "Ref Line 3:";
		this.XRlineTxt2.Location = new System.Drawing.Point(575, 89);
		this.XRlineTxt2.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineTxt2.Name = "XRlineTxt2";
		this.XRlineTxt2.Size = new System.Drawing.Size(123, 23);
		this.XRlineTxt2.TabIndex = 27;
		this.XRlineColor2.FormattingEnabled = true;
		this.XRlineColor2.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.XRlineColor2.Location = new System.Drawing.Point(485, 89);
		this.XRlineColor2.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineColor2.Name = "XRlineColor2";
		this.XRlineColor2.Size = new System.Drawing.Size(83, 25);
		this.XRlineColor2.Sorted = true;
		this.XRlineColor2.TabIndex = 26;
		this.XRlineColor2.Text = "Black";
		this.XRlineStyle2.FormattingEnabled = true;
		this.XRlineStyle2.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.XRlineStyle2.Location = new System.Drawing.Point(408, 89);
		this.XRlineStyle2.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineStyle2.Name = "XRlineStyle2";
		this.XRlineStyle2.Size = new System.Drawing.Size(68, 25);
		this.XRlineStyle2.Sorted = true;
		this.XRlineStyle2.TabIndex = 25;
		this.XRlineStyle2.Text = "Solid";
		this.XRLineVal2.Location = new System.Drawing.Point(331, 89);
		this.XRLineVal2.Margin = new System.Windows.Forms.Padding(4);
		this.XRLineVal2.Name = "XRLineVal2";
		this.XRLineVal2.Size = new System.Drawing.Size(68, 23);
		this.XRLineVal2.TabIndex = 24;
		this.LblXRefLine2.AutoSize = true;
		this.LblXRefLine2.Location = new System.Drawing.Point(249, 89);
		this.LblXRefLine2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXRefLine2.Name = "LblXRefLine2";
		this.LblXRefLine2.Size = new System.Drawing.Size(77, 17);
		this.LblXRefLine2.TabIndex = 29;
		this.LblXRefLine2.Text = "Ref Line 2:";
		this.LblRefLineDesc.AutoSize = true;
		this.LblRefLineDesc.Location = new System.Drawing.Point(576, 34);
		this.LblRefLineDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRefLineDesc.Name = "LblRefLineDesc";
		this.LblRefLineDesc.Size = new System.Drawing.Size(43, 17);
		this.LblRefLineDesc.TabIndex = 28;
		this.LblRefLineDesc.Text = "Label";
		this.XRlineTxt1.Location = new System.Drawing.Point(575, 59);
		this.XRlineTxt1.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineTxt1.Name = "XRlineTxt1";
		this.XRlineTxt1.Size = new System.Drawing.Size(123, 23);
		this.XRlineTxt1.TabIndex = 23;
		this.XRlineColor1.FormattingEnabled = true;
		this.XRlineColor1.Items.AddRange(new object[8] { "Black", "Blue", "Green", "Magneta", "Orange", "Purple", "Red", "Yellow" });
		this.XRlineColor1.Location = new System.Drawing.Point(485, 59);
		this.XRlineColor1.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineColor1.Name = "XRlineColor1";
		this.XRlineColor1.Size = new System.Drawing.Size(83, 25);
		this.XRlineColor1.Sorted = true;
		this.XRlineColor1.TabIndex = 22;
		this.XRlineColor1.Text = "Black";
		this.XRlineStyle1.FormattingEnabled = true;
		this.XRlineStyle1.Items.AddRange(new object[3] { "Dashed", "Dotted", "Solid" });
		this.XRlineStyle1.Location = new System.Drawing.Point(408, 59);
		this.XRlineStyle1.Margin = new System.Windows.Forms.Padding(4);
		this.XRlineStyle1.Name = "XRlineStyle1";
		this.XRlineStyle1.Size = new System.Drawing.Size(68, 25);
		this.XRlineStyle1.Sorted = true;
		this.XRlineStyle1.TabIndex = 21;
		this.XRlineStyle1.Text = "Solid";
		this.XRLineVal1.Location = new System.Drawing.Point(331, 59);
		this.XRLineVal1.Margin = new System.Windows.Forms.Padding(4);
		this.XRLineVal1.Name = "XRLineVal1";
		this.XRLineVal1.Size = new System.Drawing.Size(68, 23);
		this.XRLineVal1.TabIndex = 20;
		this.LblXRefLine1.AutoSize = true;
		this.LblXRefLine1.Location = new System.Drawing.Point(249, 59);
		this.LblXRefLine1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblXRefLine1.Name = "LblXRefLine1";
		this.LblXRefLine1.Size = new System.Drawing.Size(77, 17);
		this.LblXRefLine1.TabIndex = 23;
		this.LblXRefLine1.Text = "Ref Line 1:";
		this.Y_Minor_Ticks.AutoSize = true;
		this.Y_Minor_Ticks.Location = new System.Drawing.Point(121, 219);
		this.Y_Minor_Ticks.Margin = new System.Windows.Forms.Padding(4);
		this.Y_Minor_Ticks.Name = "Y_Minor_Ticks";
		this.Y_Minor_Ticks.Size = new System.Drawing.Size(102, 21);
		this.Y_Minor_Ticks.TabIndex = 19;
		this.Y_Minor_Ticks.Text = "Minor Ticks";
		this.Y_Minor_Ticks.UseVisualStyleBackColor = true;
		this.Y_Major_Ticks.AutoSize = true;
		this.Y_Major_Ticks.Location = new System.Drawing.Point(15, 219);
		this.Y_Major_Ticks.Margin = new System.Windows.Forms.Padding(4);
		this.Y_Major_Ticks.Name = "Y_Major_Ticks";
		this.Y_Major_Ticks.Size = new System.Drawing.Size(102, 21);
		this.Y_Major_Ticks.TabIndex = 18;
		this.Y_Major_Ticks.Text = "Major Ticks";
		this.Y_Major_Ticks.UseVisualStyleBackColor = true;
		this.X_Minor_Ticks.AutoSize = true;
		this.X_Minor_Ticks.Location = new System.Drawing.Point(121, 87);
		this.X_Minor_Ticks.Margin = new System.Windows.Forms.Padding(4);
		this.X_Minor_Ticks.Name = "X_Minor_Ticks";
		this.X_Minor_Ticks.Size = new System.Drawing.Size(102, 21);
		this.X_Minor_Ticks.TabIndex = 15;
		this.X_Minor_Ticks.Text = "Minor Ticks";
		this.X_Minor_Ticks.UseVisualStyleBackColor = true;
		this.X_Major_Ticks.AutoSize = true;
		this.X_Major_Ticks.Location = new System.Drawing.Point(15, 86);
		this.X_Major_Ticks.Margin = new System.Windows.Forms.Padding(4);
		this.X_Major_Ticks.Name = "X_Major_Ticks";
		this.X_Major_Ticks.Size = new System.Drawing.Size(102, 21);
		this.X_Major_Ticks.TabIndex = 14;
		this.X_Major_Ticks.Text = "Major Ticks";
		this.X_Major_Ticks.UseVisualStyleBackColor = true;
		this.Label2.AutoSize = true;
		this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label2.Location = new System.Drawing.Point(0, 167);
		this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(51, 17);
		this.Label2.TabIndex = 15;
		this.Label2.Text = "Y-Axis:";
		this.Y_Minor_Grid.AutoSize = true;
		this.Y_Minor_Grid.Location = new System.Drawing.Point(121, 193);
		this.Y_Minor_Grid.Margin = new System.Windows.Forms.Padding(4);
		this.Y_Minor_Grid.Name = "Y_Minor_Grid";
		this.Y_Minor_Grid.Size = new System.Drawing.Size(96, 21);
		this.Y_Minor_Grid.TabIndex = 17;
		this.Y_Minor_Grid.Text = "Minor Grid";
		this.Y_Minor_Grid.UseVisualStyleBackColor = true;
		this.Y_Major_Grid.AutoSize = true;
		this.Y_Major_Grid.Location = new System.Drawing.Point(15, 193);
		this.Y_Major_Grid.Margin = new System.Windows.Forms.Padding(4);
		this.Y_Major_Grid.Name = "Y_Major_Grid";
		this.Y_Major_Grid.Size = new System.Drawing.Size(96, 21);
		this.Y_Major_Grid.TabIndex = 16;
		this.Y_Major_Grid.Text = "Major Grid";
		this.Y_Major_Grid.UseVisualStyleBackColor = true;
		this.X_Minor_Grid.AutoSize = true;
		this.X_Minor_Grid.Location = new System.Drawing.Point(121, 59);
		this.X_Minor_Grid.Margin = new System.Windows.Forms.Padding(4);
		this.X_Minor_Grid.Name = "X_Minor_Grid";
		this.X_Minor_Grid.Size = new System.Drawing.Size(96, 21);
		this.X_Minor_Grid.TabIndex = 13;
		this.X_Minor_Grid.Text = "Minor Grid";
		this.X_Minor_Grid.UseVisualStyleBackColor = true;
		this.X_Major_Grid.AutoSize = true;
		this.X_Major_Grid.Location = new System.Drawing.Point(15, 58);
		this.X_Major_Grid.Margin = new System.Windows.Forms.Padding(4);
		this.X_Major_Grid.Name = "X_Major_Grid";
		this.X_Major_Grid.Size = new System.Drawing.Size(96, 21);
		this.X_Major_Grid.TabIndex = 12;
		this.X_Major_Grid.Text = "Major Grid";
		this.X_Major_Grid.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.Location = new System.Drawing.Point(0, 30);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(51, 17);
		this.Label1.TabIndex = 10;
		this.Label1.Text = "X-Axis:";
		this.GrpFrame.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.GrpFrame.Controls.Add(this.FrameColor);
		this.GrpFrame.Controls.Add(this.FrameMarker);
		this.GrpFrame.Controls.Add(this.lblFMarker);
		this.GrpFrame.Controls.Add(this.Label11);
		this.GrpFrame.Controls.Add(this.FrameY);
		this.GrpFrame.Controls.Add(this.Label9);
		this.GrpFrame.Controls.Add(this.FrameX);
		this.GrpFrame.Controls.Add(this.Label6);
		this.GrpFrame.Location = new System.Drawing.Point(5, 489);
		this.GrpFrame.Margin = new System.Windows.Forms.Padding(4);
		this.GrpFrame.Name = "GrpFrame";
		this.GrpFrame.Padding = new System.Windows.Forms.Padding(4);
		this.GrpFrame.Size = new System.Drawing.Size(707, 65);
		this.GrpFrame.TabIndex = 74;
		this.GrpFrame.TabStop = false;
		this.GrpFrame.Text = "Frame Format";
		this.FrameColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.FrameColor.FormattingEnabled = true;
		this.FrameColor.Items.AddRange(new object[78]
		{
			"white", "aliceblue", "antiquewhite", "aquamarine2", "aquamarine4", "azure", "beige", "bisque3", "blueviolet", "burlywood",
			"bisque", "black", "blanchedalmond", "blue", "brown", "cadetblue3", "coral", "cornflowerblue", "cornsilk", "cyan",
			"darkcyan", "darkgoldenrod1", "darkgray", "darkgreen", "darkolivegreen", "darkorange", "darkorchid", "darkred", "darksalmon", "darkseagreen",
			"darkseagreen3", "deepskyblue", "firebrick", "gold", "goldenrod", "gray", "green", "honeydew", "khaki", "lavenderblush",
			"lemonchiffon", "lightblue", "lightgoldenrod", "lightyellow", "linen", "magneta", "midnightblue", "mintcream", "mistyrose", "moccasin",
			"navajowhite", "navy", "orange", "orchid", "palegoldenrod", "palegreen", "peachpuff", "pink", "purple", "red",
			"rosybrown", "royalblue", "salmon", "seagreen", "seashell", "skyblue", "snow", "springgreen", "steelblue", "tan",
			"thistle", "tomato", "turquoise", "violet", "wheat", "whitesmoke", "yellow", "yellowgreen"
		});
		this.FrameColor.Location = new System.Drawing.Point(368, 30);
		this.FrameColor.Margin = new System.Windows.Forms.Padding(4);
		this.FrameColor.Name = "FrameColor";
		this.FrameColor.Size = new System.Drawing.Size(157, 24);
		this.FrameColor.TabIndex = 55;
		this.FrameMarker.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.FrameMarker.Location = new System.Drawing.Point(645, 30);
		this.FrameMarker.Margin = new System.Windows.Forms.Padding(4);
		this.FrameMarker.Name = "FrameMarker";
		this.FrameMarker.Size = new System.Drawing.Size(43, 22);
		this.FrameMarker.TabIndex = 56;
		this.lblFMarker.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblFMarker.AutoSize = true;
		this.lblFMarker.Location = new System.Drawing.Point(549, 33);
		this.lblFMarker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblFMarker.Name = "lblFMarker";
		this.lblFMarker.Size = new System.Drawing.Size(87, 17);
		this.lblFMarker.TabIndex = 71;
		this.lblFMarker.Text = "Marker Size:";
		this.Label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Label11.AutoSize = true;
		this.Label11.Location = new System.Drawing.Point(309, 33);
		this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(45, 17);
		this.Label11.TabIndex = 69;
		this.Label11.Text = "Color:";
		this.FrameY.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.FrameY.Location = new System.Drawing.Point(184, 30);
		this.FrameY.Margin = new System.Windows.Forms.Padding(4);
		this.FrameY.Name = "FrameY";
		this.FrameY.Size = new System.Drawing.Size(105, 22);
		this.FrameY.TabIndex = 54;
		this.Label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(159, 33);
		this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(21, 17);
		this.Label9.TabIndex = 67;
		this.Label9.Text = "Y:";
		this.FrameX.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.FrameX.Location = new System.Drawing.Point(35, 30);
		this.FrameX.Margin = new System.Windows.Forms.Padding(4);
		this.FrameX.Name = "FrameX";
		this.FrameX.Size = new System.Drawing.Size(105, 22);
		this.FrameX.TabIndex = 53;
		this.Label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(15, 33);
		this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(21, 17);
		this.Label6.TabIndex = 65;
		this.Label6.Text = "X:";
		this.TabCols.Controls.Add(this.cmdRefresh);
		this.TabCols.Controls.Add(this.chkHTTP);
		this.TabCols.Controls.Add(this.TxtInFile);
		this.TabCols.Controls.Add(this.Lbl1Image);
		this.TabCols.Controls.Add(this.Chk1Image);
		this.TabCols.Controls.Add(this.cmdBrowse);
		this.TabCols.Controls.Add(this.TxtPerRow);
		this.TabCols.Controls.Add(this.cmdDelete);
		this.TabCols.Controls.Add(this.cmdDown);
		this.TabCols.Controls.Add(this.CmdUp);
		this.TabCols.Controls.Add(this.CmdColHelp);
		this.TabCols.Controls.Add(this.LblWhere);
		this.TabCols.Controls.Add(this.GridWhere);
		this.TabCols.Controls.Add(this.LblRow);
		this.TabCols.Controls.Add(this.cmdClear);
		this.TabCols.Controls.Add(this.LblOption1);
		this.TabCols.Controls.Add(this.LstGroup);
		this.TabCols.Controls.Add(this.CmdGroup);
		this.TabCols.Controls.Add(this.LblOption0);
		this.TabCols.Controls.Add(this.LblFormTitle);
		this.TabCols.Controls.Add(this.CmdRemove);
		this.TabCols.Controls.Add(this.LblRoles);
		this.TabCols.Controls.Add(this.cmdExtra);
		this.TabCols.Controls.Add(this.cmdCancel);
		this.TabCols.Controls.Add(this.cmdOK);
		this.TabCols.Controls.Add(this.cmdBy);
		this.TabCols.Controls.Add(this.LstBy);
		this.TabCols.Controls.Add(this.LstX);
		this.TabCols.Controls.Add(this.LstY);
		this.TabCols.Controls.Add(this.CmdX);
		this.TabCols.Controls.Add(this.CmdY);
		this.TabCols.Controls.Add(this.LblCols);
		this.TabCols.Controls.Add(this.LstColumns);
		this.TabCols.Controls.Add(this.LblInFile);
		this.TabCols.Controls.Add(this.CmbY);
		this.TabCols.Controls.Add(this.LstOther);
		this.TabCols.Controls.Add(this.CmdVal);
		this.TabCols.Controls.Add(this.LstVal);
		this.TabCols.Location = new System.Drawing.Point(4, 25);
		this.TabCols.Margin = new System.Windows.Forms.Padding(4);
		this.TabCols.Name = "TabCols";
		this.TabCols.Padding = new System.Windows.Forms.Padding(4);
		this.TabCols.Size = new System.Drawing.Size(717, 665);
		this.TabCols.TabIndex = 0;
		this.TabCols.Text = "Columns";
		this.TabCols.UseVisualStyleBackColor = true;
		this.TxtInFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtInFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TxtInFile.FormattingEnabled = true;
		this.TxtInFile.Location = new System.Drawing.Point(183, 47);
		this.TxtInFile.Margin = new System.Windows.Forms.Padding(4);
		this.TxtInFile.Name = "TxtInFile";
		this.TxtInFile.Size = new System.Drawing.Size(421, 24);
		this.TxtInFile.TabIndex = 3;
		this.Chk1Image.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Chk1Image.AutoSize = true;
		this.Chk1Image.Location = new System.Drawing.Point(654, 402);
		this.Chk1Image.Margin = new System.Windows.Forms.Padding(4);
		this.Chk1Image.Name = "Chk1Image";
		this.Chk1Image.Size = new System.Drawing.Size(18, 17);
		this.Chk1Image.TabIndex = 20;
		this.Chk1Image.UseVisualStyleBackColor = true;
		this.Chk1Image.Visible = false;
		this.LblWhere.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblWhere.AutoSize = true;
		this.LblWhere.Location = new System.Drawing.Point(9, 510);
		this.LblWhere.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblWhere.Name = "LblWhere";
		this.LblWhere.Size = new System.Drawing.Size(122, 17);
		this.LblWhere.TabIndex = 41;
		this.LblWhere.Text = "Where Filter (Opt)";
		this.GridWhere.AllowUserToAddRows = false;
		this.GridWhere.AllowUserToDeleteRows = false;
		this.GridWhere.Anchor = System.Windows.Forms.AnchorStyles.None;
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
		this.GridWhere.Columns.AddRange(this.ColAnd, this.ColParenO, this.ColGrid, this.ColOprGrid, this.ColValGrid, this.Col2Grid, this.ColParenC);
		this.GridWhere.Location = new System.Drawing.Point(8, 535);
		this.GridWhere.Margin = new System.Windows.Forms.Padding(4);
		this.GridWhere.Name = "GridWhere";
		this.GridWhere.RowHeadersWidth = 51;
		this.GridWhere.RowTemplate.Height = 24;
		this.GridWhere.Size = new System.Drawing.Size(613, 116);
		this.GridWhere.StandardTab = true;
		this.GridWhere.TabIndex = 21;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColAnd.DefaultCellStyle = dataGridViewCellStyle2;
		this.ColAnd.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAnd.DisplayStyleForCurrentCellOnly = true;
		this.ColAnd.DropDownWidth = 50;
		this.ColAnd.HeaderText = "&";
		this.ColAnd.Items.AddRange("", "&", "|");
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
		this.ColOprGrid.MinimumWidth = 6;
		this.ColOprGrid.Name = "ColOprGrid";
		this.ColOprGrid.Width = 60;
		this.ColValGrid.HeaderText = "Value";
		this.ColValGrid.MinimumWidth = 6;
		this.ColValGrid.Name = "ColValGrid";
		this.ColValGrid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColValGrid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColValGrid.Width = 125;
		this.Col2Grid.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.Col2Grid.DisplayStyleForCurrentCellOnly = true;
		this.Col2Grid.HeaderText = "Table 2";
		this.Col2Grid.Items.AddRange("", " ");
		this.Col2Grid.MinimumWidth = 6;
		this.Col2Grid.Name = "Col2Grid";
		this.Col2Grid.Visible = false;
		this.Col2Grid.Width = 125;
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
		this.LblRow.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblRow.AutoSize = true;
		this.LblRow.Location = new System.Drawing.Point(4, 90);
		this.LblRow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRow.Name = "LblRow";
		this.LblRow.Size = new System.Drawing.Size(100, 17);
		this.LblRow.TabIndex = 39;
		this.LblRow.Text = "Plots Per Row:";
		this.LblOption1.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblOption1.AutoSize = true;
		this.LblOption1.Location = new System.Drawing.Point(375, 459);
		this.LblOption1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOption1.Name = "LblOption1";
		this.LblOption1.Size = new System.Drawing.Size(101, 17);
		this.LblOption1.TabIndex = 19;
		this.LblOption1.Text = "Option 1 Label";
		this.LblOption1.Visible = false;
		this.LstGroup.AllowDrop = true;
		this.LstGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstGroup.ContextMenuStrip = this.ContextMenuXYByGroup;
		this.LstGroup.FormattingEnabled = true;
		this.LstGroup.HorizontalScrollbar = true;
		this.LstGroup.ItemHeight = 16;
		this.LstGroup.Location = new System.Drawing.Point(375, 460);
		this.LstGroup.Margin = new System.Windows.Forms.Padding(4);
		this.LstGroup.Name = "LstGroup";
		this.LstGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstGroup.Size = new System.Drawing.Size(245, 68);
		this.LstGroup.TabIndex = 16;
		this.LstGroup.Visible = false;
		this.ContextMenuXYByGroup.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuXYByGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuRemove, this.mnuClear });
		this.ContextMenuXYByGroup.Name = "ContextMenuXYByGroup";
		this.ContextMenuXYByGroup.Size = new System.Drawing.Size(133, 52);
		this.mnuRemove.Name = "mnuRemove";
		this.mnuRemove.Size = new System.Drawing.Size(132, 24);
		this.mnuRemove.Text = "Remove";
		this.mnuClear.Name = "mnuClear";
		this.mnuClear.Size = new System.Drawing.Size(132, 24);
		this.mnuClear.Text = "Clear";
		this.CmdGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdGroup.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdGroup.ImageIndex = 4;
		this.CmdGroup.Location = new System.Drawing.Point(265, 460);
		this.CmdGroup.Margin = new System.Windows.Forms.Padding(4);
		this.CmdGroup.Name = "CmdGroup";
		this.CmdGroup.Size = new System.Drawing.Size(101, 49);
		this.CmdGroup.TabIndex = 12;
		this.CmdGroup.Text = "Group";
		this.CmdGroup.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdGroup.UseVisualStyleBackColor = true;
		this.CmdGroup.Visible = false;
		this.LblOption0.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblOption0.AutoSize = true;
		this.LblOption0.Location = new System.Drawing.Point(8, 439);
		this.LblOption0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOption0.Name = "LblOption0";
		this.LblOption0.Size = new System.Drawing.Size(101, 17);
		this.LblOption0.TabIndex = 18;
		this.LblOption0.Tag = "";
		this.LblOption0.Text = "Option 0 Label";
		this.LblOption0.Visible = false;
		this.LblFormTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.LblFormTitle.Location = new System.Drawing.Point(4, 10);
		this.LblFormTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblFormTitle.Name = "LblFormTitle";
		this.LblFormTitle.Size = new System.Drawing.Size(472, 31);
		this.LblFormTitle.TabIndex = 26;
		this.LblFormTitle.Text = "Label2";
		this.LblRoles.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblRoles.AutoSize = true;
		this.LblRoles.Location = new System.Drawing.Point(375, 124);
		this.LblRoles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRoles.Name = "LblRoles";
		this.LblRoles.Size = new System.Drawing.Size(44, 17);
		this.LblRoles.TabIndex = 15;
		this.LblRoles.Text = "Roles";
		this.LstBy.AllowDrop = true;
		this.LstBy.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstBy.ContextMenuStrip = this.ContextMenuXYByGroup;
		this.LstBy.FormattingEnabled = true;
		this.LstBy.HorizontalScrollbar = true;
		this.LstBy.ItemHeight = 16;
		this.LstBy.Location = new System.Drawing.Point(375, 378);
		this.LstBy.Margin = new System.Windows.Forms.Padding(4);
		this.LstBy.Name = "LstBy";
		this.LstBy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstBy.Size = new System.Drawing.Size(245, 68);
		this.LstBy.TabIndex = 15;
		this.LstX.AllowDrop = true;
		this.LstX.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstX.ContextMenuStrip = this.ContextMenuXYByGroup;
		this.LstX.FormattingEnabled = true;
		this.LstX.HorizontalScrollbar = true;
		this.LstX.ItemHeight = 16;
		this.LstX.Location = new System.Drawing.Point(375, 263);
		this.LstX.Margin = new System.Windows.Forms.Padding(4);
		this.LstX.Name = "LstX";
		this.LstX.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstX.Size = new System.Drawing.Size(245, 100);
		this.LstX.TabIndex = 14;
		this.LstY.AllowDrop = true;
		this.LstY.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstY.ContextMenuStrip = this.ContextMenuXYByGroup;
		this.LstY.FormattingEnabled = true;
		this.LstY.HorizontalScrollbar = true;
		this.LstY.ItemHeight = 16;
		this.LstY.Location = new System.Drawing.Point(375, 149);
		this.LstY.Margin = new System.Windows.Forms.Padding(4);
		this.LstY.Name = "LstY";
		this.LstY.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstY.Size = new System.Drawing.Size(245, 100);
		this.LstY.TabIndex = 13;
		this.LblCols.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LblCols.AutoSize = true;
		this.LblCols.Location = new System.Drawing.Point(8, 124);
		this.LblCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCols.Name = "LblCols";
		this.LblCols.Size = new System.Drawing.Size(62, 17);
		this.LblCols.TabIndex = 4;
		this.LblCols.Text = "Columns";
		this.LstColumns.ContextMenuStrip = this.ContextMenuCols;
		this.LstColumns.FormattingEnabled = true;
		this.LstColumns.HorizontalScrollbar = true;
		this.LstColumns.ItemHeight = 16;
		this.LstColumns.Location = new System.Drawing.Point(8, 149);
		this.LstColumns.Margin = new System.Windows.Forms.Padding(4);
		this.LstColumns.Name = "LstColumns";
		this.LstColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstColumns.Size = new System.Drawing.Size(245, 260);
		this.LstColumns.TabIndex = 7;
		this.ContextMenuCols.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuCols.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuX, this.mnuY, this.mnuBy, this.mnuGroup, this.mnuVal });
		this.ContextMenuCols.Name = "ContextMenuCols";
		this.ContextMenuCols.Size = new System.Drawing.Size(120, 124);
		this.mnuX.Name = "mnuX";
		this.mnuX.Size = new System.Drawing.Size(119, 24);
		this.mnuX.Text = "X";
		this.mnuY.Name = "mnuY";
		this.mnuY.Size = new System.Drawing.Size(119, 24);
		this.mnuY.Text = "Y";
		this.mnuBy.Name = "mnuBy";
		this.mnuBy.Size = new System.Drawing.Size(119, 24);
		this.mnuBy.Text = "By";
		this.mnuGroup.Name = "mnuGroup";
		this.mnuGroup.Size = new System.Drawing.Size(119, 24);
		this.mnuGroup.Text = "Group";
		this.mnuGroup.Visible = false;
		this.mnuVal.Name = "mnuVal";
		this.mnuVal.Size = new System.Drawing.Size(119, 24);
		this.mnuVal.Text = "Value";
		this.mnuVal.Visible = false;
		this.LblInFile.AutoSize = true;
		this.LblInFile.Location = new System.Drawing.Point(4, 50);
		this.LblInFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblInFile.Name = "LblInFile";
		this.LblInFile.Size = new System.Drawing.Size(83, 17);
		this.LblInFile.TabIndex = 1;
		this.LblInFile.Text = "Input Table:";
		this.LstOther.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstOther.FormattingEnabled = true;
		this.LstOther.ItemHeight = 16;
		this.LstOther.Location = new System.Drawing.Point(8, 463);
		this.LstOther.Margin = new System.Windows.Forms.Padding(4);
		this.LstOther.Name = "LstOther";
		this.LstOther.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.LstOther.Size = new System.Drawing.Size(245, 180);
		this.LstOther.TabIndex = 16;
		this.LstOther.Visible = false;
		this.CmdVal.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.CmdVal.Location = new System.Drawing.Point(263, 149);
		this.CmdVal.Margin = new System.Windows.Forms.Padding(4);
		this.CmdVal.Name = "CmdVal";
		this.CmdVal.Size = new System.Drawing.Size(101, 49);
		this.CmdVal.TabIndex = 52;
		this.CmdVal.Text = "Value";
		this.CmdVal.UseVisualStyleBackColor = true;
		this.CmdVal.Visible = false;
		this.LstVal.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.LstVal.FormattingEnabled = true;
		this.LstVal.ItemHeight = 16;
		this.LstVal.Location = new System.Drawing.Point(375, 149);
		this.LstVal.Margin = new System.Windows.Forms.Padding(4);
		this.LstVal.Name = "LstVal";
		this.LstVal.Size = new System.Drawing.Size(245, 52);
		this.LstVal.TabIndex = 51;
		this.LstVal.Visible = false;
		this.TabChart.Controls.Add(this.TabCols);
		this.TabChart.Controls.Add(this.TabTitles);
		this.TabChart.Controls.Add(this.TabTheme);
		this.TabChart.Controls.Add(this.TabOptions);
		this.TabChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabChart.Location = new System.Drawing.Point(0, 28);
		this.TabChart.Margin = new System.Windows.Forms.Padding(4);
		this.TabChart.Name = "TabChart";
		this.TabChart.Padding = new System.Drawing.Point(30, 3);
		this.TabChart.SelectedIndex = 0;
		this.TabChart.Size = new System.Drawing.Size(725, 694);
		this.TabChart.TabIndex = 0;
		this.TabTheme.Controls.Add(this.cmdClear2);
		this.TabTheme.Controls.Add(this.Label3);
		this.TabTheme.Controls.Add(this.cmdDel2);
		this.TabTheme.Controls.Add(this.cmddown2);
		this.TabTheme.Controls.Add(this.cmdUp2);
		this.TabTheme.Controls.Add(this.GridTheme);
		this.TabTheme.Location = new System.Drawing.Point(4, 25);
		this.TabTheme.Margin = new System.Windows.Forms.Padding(4);
		this.TabTheme.Name = "TabTheme";
		this.TabTheme.Size = new System.Drawing.Size(717, 665);
		this.TabTheme.TabIndex = 3;
		this.TabTheme.Text = "Themes";
		this.TabTheme.UseVisualStyleBackColor = true;
		this.Label3.Location = new System.Drawing.Point(4, 25);
		this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(603, 75);
		this.Label3.TabIndex = 58;
		this.Label3.Text = "Optiionally Specify a Chart Theme. This will be applied depending on what elements become the Chart legend. E.g., Y variables in a plot or levels of a  Group Variable.";
		this.GridTheme.AllowUserToAddRows = false;
		this.GridTheme.AllowUserToDeleteRows = false;
		this.GridTheme.AllowUserToResizeColumns = false;
		this.GridTheme.AllowUserToResizeRows = false;
		this.GridTheme.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridTheme.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridTheme.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.GridTheme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridTheme.Columns.AddRange(this.DataGridViewTextBoxColumn3, this.cmbSym, this.cmblinetype, this.DataGridViewComboBoxColumn3, this.DataGridViewTextBoxColumn4);
		this.GridTheme.Location = new System.Drawing.Point(1, 103);
		this.GridTheme.Margin = new System.Windows.Forms.Padding(4);
		this.GridTheme.Name = "GridTheme";
		this.GridTheme.RowHeadersWidth = 51;
		this.GridTheme.RowTemplate.Height = 24;
		this.GridTheme.Size = new System.Drawing.Size(617, 217);
		this.GridTheme.StandardTab = true;
		this.GridTheme.TabIndex = 0;
		this.DataGridViewTextBoxColumn3.HeaderText = "Color";
		this.DataGridViewTextBoxColumn3.MinimumWidth = 6;
		this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
		this.DataGridViewTextBoxColumn3.ReadOnly = true;
		this.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn3.Width = 85;
		this.cmbSym.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.cmbSym.DisplayStyleForCurrentCellOnly = true;
		this.cmbSym.DropDownWidth = 175;
		this.cmbSym.HeaderText = "Symbol";
		this.cmbSym.MinimumWidth = 6;
		this.cmbSym.Name = "cmbSym";
		this.cmbSym.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cmbSym.Width = 70;
		this.cmblinetype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.cmblinetype.DisplayStyleForCurrentCellOnly = true;
		this.cmblinetype.HeaderText = "Line Type";
		this.cmblinetype.MinimumWidth = 6;
		this.cmblinetype.Name = "cmblinetype";
		this.cmblinetype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cmblinetype.Width = 75;
		this.DataGridViewComboBoxColumn3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.DataGridViewComboBoxColumn3.DisplayStyleForCurrentCellOnly = true;
		this.DataGridViewComboBoxColumn3.HeaderText = "Line Width";
		this.DataGridViewComboBoxColumn3.Items.AddRange("", "1", "2", "3", "4", "5", "6", "7", "8", "9");
		this.DataGridViewComboBoxColumn3.MinimumWidth = 6;
		this.DataGridViewComboBoxColumn3.Name = "DataGridViewComboBoxColumn3";
		this.DataGridViewComboBoxColumn3.Width = 75;
		this.DataGridViewTextBoxColumn4.HeaderText = "Fill";
		this.DataGridViewTextBoxColumn4.MinimumWidth = 6;
		this.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
		this.DataGridViewTextBoxColumn4.ReadOnly = true;
		this.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn4.Width = 85;
		this.TabOptions.Location = new System.Drawing.Point(4, 25);
		this.TabOptions.Margin = new System.Windows.Forms.Padding(4);
		this.TabOptions.Name = "TabOptions";
		this.TabOptions.Size = new System.Drawing.Size(717, 665);
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
		this.Label15.AutoSize = true;
		this.Label15.Location = new System.Drawing.Point(422, 26);
		this.Label15.Name = "Label15";
		this.Label15.Size = new System.Drawing.Size(30, 13);
		this.Label15.TabIndex = 10;
		this.Label15.Text = "Size:";
		this.Label16.AutoSize = true;
		this.Label16.Location = new System.Drawing.Point(163, 57);
		this.Label16.Name = "Label16";
		this.Label16.Size = new System.Drawing.Size(68, 13);
		this.Label16.TabIndex = 7;
		this.Label16.Text = "Background:";
		this.Label17.AutoSize = true;
		this.Label17.Location = new System.Drawing.Point(163, 24);
		this.Label17.Name = "Label17";
		this.Label17.Size = new System.Drawing.Size(69, 13);
		this.Label17.TabIndex = 5;
		this.Label17.Text = "Legend Title:";
		this.Label18.AutoSize = true;
		this.Label18.Location = new System.Drawing.Point(6, 54);
		this.Label18.Name = "Label18";
		this.Label18.Size = new System.Drawing.Size(50, 13);
		this.Label18.TabIndex = 3;
		this.Label18.Text = "Columns:";
		this.Label20.AutoSize = true;
		this.Label20.Location = new System.Drawing.Point(6, 22);
		this.Label20.Name = "Label20";
		this.Label20.Size = new System.Drawing.Size(47, 13);
		this.Label20.TabIndex = 1;
		this.Label20.Text = "Position:";
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuThemeStrip, this.mnuComputations, this.mnuHelp });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Size = new System.Drawing.Size(725, 28);
		this.MenuStrip1.TabIndex = 1;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuThemeStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuLoadTheme, this.mnuSaveTheme, this.mnuStdTheme, this.mnuRefreshTheme });
		this.mnuThemeStrip.Name = "mnuThemeStrip";
		this.mnuThemeStrip.Size = new System.Drawing.Size(68, 24);
		this.mnuThemeStrip.Text = "&Theme";
		this.mnuLoadTheme.Name = "mnuLoadTheme";
		this.mnuLoadTheme.Size = new System.Drawing.Size(289, 26);
		this.mnuLoadTheme.Text = "Load Theme ...";
		this.mnuSaveTheme.Name = "mnuSaveTheme";
		this.mnuSaveTheme.Size = new System.Drawing.Size(289, 26);
		this.mnuSaveTheme.Text = "Save Theme Externally ...";
		this.mnuStdTheme.DropDownWidth = 250;
		this.mnuStdTheme.Name = "mnuStdTheme";
		this.mnuStdTheme.Size = new System.Drawing.Size(121, 28);
		this.mnuStdTheme.Text = "Sample Themes";
		this.mnuRefreshTheme.Name = "mnuRefreshTheme";
		this.mnuRefreshTheme.Size = new System.Drawing.Size(289, 26);
		this.mnuRefreshTheme.Text = "Load Selected Sample Theme";
		this.mnuComputations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuXCompute, this.mnuDataLabels, this.mnuKeepUnusedCharts });
		this.mnuComputations.Name = "mnuComputations";
		this.mnuComputations.Size = new System.Drawing.Size(162, 24);
		this.mnuComputations.Text = "&Special Axes Options";
		this.mnuXCompute.Name = "mnuXCompute";
		this.mnuXCompute.Size = new System.Drawing.Size(224, 26);
		this.mnuXCompute.Text = "Add X Computation";
		this.mnuXCompute.Visible = false;
		this.mnuDataLabels.Name = "mnuDataLabels";
		this.mnuDataLabels.Size = new System.Drawing.Size(224, 26);
		this.mnuDataLabels.Text = "Add Y Data Labels";
		this.mnuDataLabels.Visible = false;
		this.mnuKeepUnusedCharts.CheckOnClick = true;
		this.mnuKeepUnusedCharts.Name = "mnuKeepUnusedCharts";
		this.mnuKeepUnusedCharts.Size = new System.Drawing.Size(224, 26);
		this.mnuKeepUnusedCharts.Text = "Keep Unused Charts";
		this.mnuKeepUnusedCharts.ToolTipText = "Check to Keep Unused Charts. Improves Performance when there are many Charts";
		this.mnuKeepUnusedCharts.Visible = false;
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.Size = new System.Drawing.Size(55, 24);
		this.mnuHelp.Text = "&Help";
		this.ContextMenuColEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuColEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnusetColor, this.mnuClearColor });
		this.ContextMenuColEdit.Name = "ContextMenuColEdit";
		this.ContextMenuColEdit.Size = new System.Drawing.Size(153, 52);
		this.mnusetColor.Name = "mnusetColor";
		this.mnusetColor.Size = new System.Drawing.Size(152, 24);
		this.mnusetColor.Text = "Set Color";
		this.mnuClearColor.Name = "mnuClearColor";
		this.mnuClearColor.Size = new System.Drawing.Size(152, 24);
		this.mnuClearColor.Text = "Clear Color";
		this.cmbY2Type.FormattingEnabled = true;
		this.cmbY2Type.Location = new System.Drawing.Point(406, 102);
		this.cmbY2Type.Name = "cmbY2Type";
		this.cmbY2Type.Size = new System.Drawing.Size(51, 24);
		this.cmbY2Type.TabIndex = 76;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(725, 722);
		base.Controls.Add(this.TabChart);
		base.Controls.Add(this.MenuStrip1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmChart";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Charting";
		this.TabTitles.ResumeLayout(false);
		this.GrpRLegend.ResumeLayout(false);
		this.GrpRLegend.PerformLayout();
		this.GrpRowLegend.ResumeLayout(false);
		this.GrpRowLegend.PerformLayout();
		this.GrpTitle.ResumeLayout(false);
		this.GrpTitle.PerformLayout();
		this.GrpGrid.ResumeLayout(false);
		this.GrpGrid.PerformLayout();
		this.GrpFrame.ResumeLayout(false);
		this.GrpFrame.PerformLayout();
		this.TabCols.ResumeLayout(false);
		this.TabCols.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridWhere).EndInit();
		this.ContextMenuXYByGroup.ResumeLayout(false);
		this.ContextMenuCols.ResumeLayout(false);
		this.TabChart.ResumeLayout(false);
		this.TabTheme.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridTheme).EndInit();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		this.ContextMenuColEdit.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Populate_Combo(ref ComboBox MyCombo, string MyItems, string MyDLM = ",")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string[] array = default(string[]);
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
				case 216:
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
							goto IL_001e;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0041;
						case 8:
							goto IL_0050;
						case 9:
							goto IL_0060;
						case 10:
							goto IL_0079;
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
					IL_0082:
					if (num5 > num6)
					{
						break;
					}
					goto IL_0060;
					IL_0060:
					num2 = 9;
					MyCombo.Items.Add(Strings.Trim(array[num5]));
					goto IL_0079;
					IL_0050:
					num2 = 8;
					num6 = Information.UBound(array);
					num5 = 0;
					goto IL_0082;
					IL_0079:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_0082;
					IL_000b:
					num2 = 2;
					array = null;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					MyItems = Strings.Trim(MyItems);
					goto IL_001e;
					IL_001e:
					num2 = 5;
					if (Operators.CompareString(MyItems, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0035:
					num2 = 6;
					array = Strings.Split(MyItems, MyDLM);
					goto IL_0041;
					IL_0041:
					num2 = 7;
					MyCombo.Items.Clear();
					goto IL_0050;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				array = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 216;
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

	public void Set_Std_Theme(string Mysection)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myFile = default(string);
		int num5 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 393:
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
							goto IL_0037;
						case 5:
							goto IL_0049;
						case 6:
							goto IL_0053;
						case 7:
							goto IL_0058;
						case 8:
							goto IL_0061;
						case 9:
							goto IL_006f;
						case 10:
							goto IL_0085;
						case 12:
						case 13:
						case 14:
							goto IL_009c;
						case 15:
							goto IL_00b0;
						case 16:
							goto IL_00c1;
						case 17:
							goto IL_00d4;
						case 18:
							goto IL_00e7;
						case 19:
							goto IL_00fa;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 11:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d4:
					num2 = 17;
					Add_Theme_Element("symbol", myFile, 1, DoColor: false, Mysection);
					goto IL_00e7;
					IL_00e7:
					num2 = 18;
					Add_Theme_Element("linetype", myFile, 2, DoColor: false, Mysection);
					goto IL_00fa;
					IL_00c1:
					num2 = 16;
					Add_Theme_Element("color", myFile, 0, DoColor: true, Mysection);
					goto IL_00d4;
					IL_00fa:
					num2 = 19;
					Add_Theme_Element("linewidth", myFile, 3, DoColor: false, Mysection);
					break;
					IL_000b:
					num2 = 2;
					if ((Operators.CompareString(Mysection, "Sample Themes", TextCompare: false) == 0) | (Operators.CompareString(Mysection, "", TextCompare: false) == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_0037;
					IL_0037:
					num2 = 4;
					myFile = Globals_Renamed.MySchemaDir + "\\R_Themes.ini";
					goto IL_0049;
					IL_0049:
					num2 = 5;
					Mysection = Strings.LCase(Mysection);
					goto IL_0053;
					IL_0053:
					num2 = 6;
					num5 = 0;
					goto IL_0058;
					IL_0058:
					num2 = 7;
					text = "";
					goto IL_0061;
					IL_0061:
					num2 = 8;
					if (AnyTheme())
					{
						goto IL_006f;
					}
					goto IL_009c;
					IL_006f:
					num2 = 9;
					num5 = (int)Interaction.MsgBox("Loading a theme clears previous selections. Continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Continue?");
					goto IL_0085;
					IL_0085:
					num2 = 10;
					if (num5 == 7)
					{
						goto end_IL_0001_3;
					}
					goto IL_009c;
					IL_009c:
					num2 = 14;
					GridTheme.Rows.Clear();
					goto IL_00b0;
					IL_00b0:
					num2 = 15;
					GridTheme.RowCount = 50;
					goto IL_00c1;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				Add_Theme_Element("fill", myFile, 4, DoColor: true, Mysection);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 393;
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

	public bool AnyTheme()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
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
					case 254:
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
								goto IL_002f;
							case 7:
								goto IL_0045;
							case 8:
								goto IL_007b;
							case 10:
							case 11:
								goto IL_0083;
							case 9:
							case 12:
								goto IL_0092;
							case 14:
								goto IL_009e;
							default:
								goto end_IL_0001;
							case 13:
							case 15:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0092:
						num2 = 12;
						if (flag)
						{
							goto end_IL_0001_2;
						}
						goto IL_009e;
						IL_009e:
						num2 = 14;
						num5++;
						goto IL_00a7;
						IL_007b:
						num2 = 8;
						flag = true;
						goto IL_0092;
						IL_0083:
						num2 = 11;
						num6++;
						goto IL_008c;
						IL_000b:
						num2 = 2;
						flag = false;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num5 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						num6 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						num7 = GridTheme.RowCount - 1;
						num5 = 0;
						goto IL_00a7;
						IL_00a7:
						if (num5 > num7)
						{
							goto end_IL_0001_2;
						}
						goto IL_002f;
						IL_002f:
						num2 = 6;
						num8 = GridTheme.ColumnCount - 1;
						num6 = 0;
						goto IL_008c;
						IL_008c:
						if (num6 <= num8)
						{
							goto IL_0045;
						}
						goto IL_0092;
						IL_0045:
						num2 = 7;
						if (Operators.ConditionalCompareObjectNotEqual(GridTheme.Rows[num5].Cells[num6].Value, "", TextCompare: false))
						{
							goto IL_007b;
						}
						goto IL_0083;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 254;
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
		return flag;
	}

	public void Set_Y_Grid_Val(ref string MyMode, int YCol, string MyS, string MyE, bool AddQ = false, bool AColon = false)
	{
		string text = "";
		if (AddQ)
		{
			text = "'";
		}
		string text2 = "";
		string text3 = "";
		checked
		{
			int num = GridTheme.RowCount - 1;
			for (int i = 0; i <= num; i++)
			{
				string text4 = Strings.Trim(Conversions.ToString(GridTheme.Rows[i].Cells[YCol].Value));
				if (AColon)
				{
					int num2 = Strings.InStr(text4, ">");
					if (num2 != 0)
					{
						text4 = Strings.Trim(Strings.Mid(text4, 1, num2 - 1));
					}
				}
				if (Operators.CompareString(text4, "", TextCompare: false) != 0)
				{
					text3 = text3 + text2 + text + text4 + text;
					text2 = ",";
				}
			}
			if (Operators.CompareString(text3, "", TextCompare: false) != 0)
			{
				if (Operators.CompareString(MyMode, "", TextCompare: false) != 0)
				{
					MyMode += "\r\n";
				}
				if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) != 0)
				{
					MyMode += ",";
				}
				MyMode = MyMode + MyS + text3 + MyE;
			}
		}
	}

	public string Title_Col_Case(string MyTitle)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text = default(string);
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
						errsource = "FrmChart - Title_Col_Case";
						text = MyTitle;
						string text2 = "";
						int num3 = 0;
						if (Strings.InStr(MyTitle, "{") == 0 || (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) != 0 && !LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary)))
						{
							goto end_IL_0001;
						}
						if (LikeOperator.LikeString(MyTitle, "*{}*", CompareMethod.Binary))
						{
							text = "$ERR$ - You have an empty column variable \"{}\" in your chart title.";
							goto end_IL_0001;
						}
						int num4 = 0;
						string text3 = "";
						string text4 = "";
						int num5 = 0;
						string[] array = Strings.Split(MyTitle, "{");
						int num6 = Information.UBound(array);
						for (num4 = 1; num4 <= num6; num4++)
						{
							num3 = Strings.InStr(array[num4], "}");
							if (num3 > 1)
							{
								text3 = Strings.Trim(Strings.LCase(Strings.Mid(array[num4], 1, num3 - 1)));
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text4 = "";
									int num7 = LstColumns.Items.Count - 1;
									for (num5 = 0; num5 <= num7; num5++)
									{
										text2 = Conversions.ToString(LstColumns.Items[num5]);
										num3 = Strings.InStrRev(text2, " (");
										if (num3 != 0)
										{
											text2 = Strings.Trim(Strings.Mid(text2, 1, num3 - 1));
										}
										if (Operators.CompareString(text3, Strings.LCase(text2), TextCompare: false) == 0)
										{
											text4 = text2;
											break;
										}
									}
									if (Operators.CompareString(text4, "", TextCompare: false) != 0)
									{
										text = Strings.Replace(text, "{" + text3 + "}", "{" + text4 + "}", 1, -1, CompareMethod.Text);
										continue;
									}
									text = "$ERR$ - You have specified an invalid column variable in your chart title: {" + text3 + "}";
									break;
								}
								text = "$ERR$ - You have an empty column variable \"{}\" in your chart title.";
								break;
							}
							text = "$ERR$ - You have specified an opening squiggly bracket \"{\" for a column variable but no closing bracket \"}\" in your chart title";
							break;
						}
						array = null;
						goto end_IL_0001_2;
					}
					case 585:
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
					goto IL_027f;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 585;
				continue;
			}
			break;
			IL_027f:
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
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
					string text3;
					bool allowAllCols;
					bool r_Replace;
					string text4;
					bool flag;
					bool makeHdrsLowerCase;
					string text5;
					int num3;
					string text2;
					string[] lCSVCols;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmChart - Populate_Columns3";
						Cursor = Cursors.WaitCursor;
						num3 = 0;
						lCSVCols = new string[0];
						int num4 = 0;
						int num5 = 0;
						string text = "";
						int num6 = 0;
						text2 = "";
						text3 = "";
						allowAllCols = false;
						r_Replace = false;
						text4 = "";
						flag = false;
						bool flag2 = false;
						makeHdrsLowerCase = false;
						if (Operators.CompareString(File1, "", TextCompare: false) != 0)
						{
							text5 = Globals_Renamed.MyPCDir + lMyPqtCSV;
							if (!LikeOperator.LikeString(Strings.Trim(Strings.LCase(File1)), "*.parquet", CompareMethod.Binary))
							{
								goto IL_00e4;
							}
							flag = true;
							if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
							{
								goto IL_00e4;
							}
							Cursor = Cursors.Default;
							Interaction.MsgBox("Parquet files are only supported for Python Plots.", MsgBoxStyle.Exclamation, "Parquet not supported");
						}
						goto end_IL_0001;
					}
					case 1877:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								Cursor = Cursors.Default;
								goto end_IL_0001;
							}
							break;
						}
						IL_00e4:
						if (Operators.CompareString(f_ColCaseInsensitive, "Y", TextCompare: false) == 0)
						{
							makeHdrsLowerCase = true;
						}
						if (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0 || Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0)
						{
							r_Replace = true;
						}
						if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
						{
							allowAllCols = true;
						}
						if (Operators.CompareString(f_QuoteCols, "Y", TextCompare: false) == 0)
						{
							text4 = "'";
							r_Replace = false;
						}
						if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(File1)), "HTTP*://*", CompareMethod.Binary))
						{
							if (flag)
							{
								Cursor = Cursors.Default;
								Interaction.MsgBox("At this time, you cannot access Parquet files from the web.", MsgBoxStyle.Exclamation, "Parquet not supported");
								goto end_IL_0001_2;
							}
							string text = BuildForm.Get_First_Line_Web(File1);
							chkHTTP.Checked = true;
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								int num6 = Strings.InStrRev(File1, ".");
								if (num6 != 0)
								{
									text3 = Strings.Mid(File1, num6);
								}
								text2 = Globals_Renamed.MyPCDir + "r_" + Globals_Renamed.gSPFCache + text3;
								num3 = General_Procedures.Save_SQL_Query(text, text2);
								num3 = General_Procedures.GetCSVHeaders(text2, ref lCSVCols, makeHdrsLowerCase, r_Replace, allowAllCols);
								goto IL_029c;
							}
						}
						else
						{
							text2 = File1;
							if (!flag)
							{
								goto IL_027d;
							}
							if (Get_Parquet_Columns(text2, text5))
							{
								text2 = text5;
								goto IL_027d;
							}
						}
						goto end_IL_0001;
						IL_027d:
						num3 = General_Procedures.GetCSVHeaders(text2, ref lCSVCols, makeHdrsLowerCase, r_Replace, allowAllCols);
						chkHTTP.Checked = false;
						goto IL_029c;
						IL_029c:
						if (num3 == 0)
						{
							LstColumns.Items.Clear();
							if (lClearData)
							{
								LstY.Items.Clear();
								LstX.Items.Clear();
								LstBy.Items.Clear();
								LstGroup.Items.Clear();
								LstVal.Items.Clear();
								RLCol1.Items.Clear();
								RLCol2.Items.Clear();
								if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) != 0)
								{
									Clear_Where_Grid();
								}
								if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) != 0)
								{
									ColGrid.Items.Clear();
									ColGrid.Items.Add("");
								}
							}
							TxtInFile.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
							LstColumns.Items.AddRange(lCSVCols);
							if (LoadOptCols)
							{
								int num7 = TabOptions.Controls.Count - 1;
								for (int num4 = 0; num4 <= num7; num4++)
								{
									if (TabOptions.Controls[num4].Tag != null && Operators.ConditionalCompareObjectEqual(TabOptions.Controls[num4].Tag, "COMBOBOXC", TextCompare: false))
									{
										object obj = null;
										obj = TabOptions.Controls[num4];
										NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Clear", new object[0], null, null, null, IgnoreReturn: true);
										object instance = NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null);
										object[] obj2 = new object[1] { lCSVCols };
										object[] array = obj2;
										bool[] obj3 = new bool[1] { true };
										bool[] array2 = obj3;
										NewLateBinding.LateCall(instance, null, "AddRange", obj2, null, null, obj3, IgnoreReturn: true);
										if (array2[0])
										{
											lCSVCols = (string[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string[]));
										}
									}
								}
							}
							if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) != 0)
							{
								if (Operators.CompareString(text4, "", TextCompare: false) != 0)
								{
									int num8 = Information.UBound(lCSVCols);
									for (int num4 = 0; num4 <= num8; num4++)
									{
										lCSVCols[num4] = text4 + lCSVCols[num4] + text4;
									}
								}
								ColGrid.Items.AddRange(lCSVCols);
							}
							lCSVCols = null;
							if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) && !LikeOperator.LikeString(LblFormTitle.Text, "3D*", CompareMethod.Binary) && !LikeOperator.LikeString(LblFormTitle.Text, "JS-*", CompareMethod.Binary) && !LikeOperator.LikeString(LblFormTitle.Text, "*WaferMap*", CompareMethod.Binary) && !LikeOperator.LikeString(LblFormTitle.Text, "*Pareto*", CompareMethod.Binary) && !LikeOperator.LikeString(LblFormTitle.Text, "*Overlay*", CompareMethod.Binary) && LstColumns.Items.Count > 0)
							{
								if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) != 0 || (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0 && Operators.CompareString(f_StartEndContain, "Y", TextCompare: false) == 0))
								{
									LstColumns.Items.Add("StartingWith:");
									LstColumns.Items.Add("EndingWith:");
									LstColumns.Items.Add("Containing:");
								}
								LstColumns.SelectedIndex = 0;
								LastChartTable = TxtInFile.Text;
							}
						}
						else
						{
							Interaction.MsgBox("Could not locate the input CSV File: " + File1 + ". Your column selections were preserved but you will not be able to make significant edits to the plot design", MsgBoxStyle.Exclamation, "Could not locate CSV File");
							if (MyMode == 1)
							{
								TxtInFile.Text = BuildForm.Strip_Add_MyPCDir("S", File1);
							}
						}
						Cursor = Cursors.Default;
						goto end_IL_0001;
					}
					goto IL_078b;
				}
				end_IL_0001:;
			}
			catch (object obj4) when (obj4 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj4);
				try0001_dispatch = 1877;
				continue;
			}
			break;
			IL_078b:
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

	public bool Get_Parquet_Columns(string MyFile, string tempfile)
	{
		bool result = true;
		try
		{
			string text = General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\" && \"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\spf_parquet_to_csv.py\" -i \"" + MyFile + "\" -o " + tempfile + " -a headers", 1, 1, 0);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				Interaction.MsgBox("Error generating Column Headers. (" + text + ").", MsgBoxStyle.OkOnly, "Conversion Error");
				result = false;
			}
			else if (!File.Exists(tempfile))
			{
				Interaction.MsgBox("Error generating Column Headers.", MsgBoxStyle.OkOnly, "Conversion Error");
				result = false;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error generating Column Headers. (" + ex2.Message + ").", MsgBoxStyle.OkOnly, "Conversion Error");
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
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
						errsource = "FrmSChart - Get_Set_Where";
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
						if ((Operators.CompareString(MyMode, "S", TextCompare: false) == 0) | (Operators.CompareString(MyMode, "I", TextCompare: false) == 0))
						{
							if ((Operators.CompareString(f_WhereMode, "1", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "2", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0))
							{
								GridWhere.Columns[1].Visible = false;
								GridWhere.Columns[6].Visible = false;
								GridWhere.Columns[0].Visible = false;
								ColOprGrid.Items.Clear();
								ColOprGrid.Items.Add("=");
								if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
								{
									GridWhere.Columns[4].Visible = false;
									GridWhere.Columns[5].Visible = true;
									text3 = TxtPerRow.Text;
									TxtPerRow.Items.Clear();
									int num6 = TxtInFile.Items.Count - 1;
									for (num3 = 0; num3 <= num6; num3++)
									{
										TxtPerRow.Items.Add(RuntimeHelpers.GetObjectValue(TxtInFile.Items[num3]));
									}
									TxtPerRow.Text = text3;
									text3 = "";
								}
							}
							else if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0)
							{
								ColOprGrid.Items.Clear();
								ColOprGrid.Items.Add("");
								ColOprGrid.Items.Add("==");
								ColOprGrid.Items.Add("!=");
								ColOprGrid.Items.Add(">");
								ColOprGrid.Items.Add("<");
								ColOprGrid.Items.Add(">=");
								ColOprGrid.Items.Add("<=");
								ColOprGrid.Items.Add("StartsWith");
								ColOprGrid.Items.Add("EndsWith");
								ColOprGrid.Items.Add("Contains");
								ColOprGrid.Items.Add("Not StartsWith");
								ColOprGrid.Items.Add("Not EndsWith");
								ColOprGrid.Items.Add("Not Contains");
								ColGrid.Items.Clear();
								ColGrid.Items.Add("");
								ColGrid.Items.Add("Modeling-Type");
								ColGrid.Items.Add("Column-Header");
								ColGrid.Items.Add("Column-Index");
								GridWhere.Rows[0].Cells[2].Value = "Modeling-Type";
								GridWhere.Rows[0].Cells[3].Value = "==";
								GridWhere.Rows[0].Cells[4].Value = "Continuous";
							}
							else
							{
								ColOprGrid.Items.Clear();
								ColOprGrid.Items.Add("");
								ColOprGrid.Items.Add("==");
								if ((Operators.CompareString(f_TemplateType2, "JMP", TextCompare: false) == 0) | (Operators.CompareString(f_TemplateType2, "JSL", TextCompare: false) == 0))
								{
									ColOprGrid.Items.Add("=");
									ColOprGrid.Items.Add("!=");
									ColOprGrid.Items.Add(">");
									ColOprGrid.Items.Add("<");
									ColOprGrid.Items.Add(">=");
									ColOprGrid.Items.Add("<=");
									ColOprGrid.Items.Add("Is Missing");
									ColOprGrid.Items.Add("Is Not Missing");
								}
								else if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
								{
									ColOprGrid.Items.Add("!=");
									ColOprGrid.Items.Add(">");
									ColOprGrid.Items.Add("<");
									ColOprGrid.Items.Add(">=");
									ColOprGrid.Items.Add("<=");
									ColOprGrid.Items.Add("in");
									ColOprGrid.Items.Add("not in");
									ColOprGrid.Items.Add("Contains");
									ColOprGrid.Items.Add("Is Not Missing");
								}
								else
								{
									ColOprGrid.Items.Add("%in%");
									ColOprGrid.Items.Add("!=");
									ColOprGrid.Items.Add(">");
									ColOprGrid.Items.Add("<");
									ColOprGrid.Items.Add(">=");
									ColOprGrid.Items.Add("<=");
									ColOprGrid.Items.Add("StartsWith");
									ColOprGrid.Items.Add("EndsWith");
									ColOprGrid.Items.Add("Contains");
									ColOprGrid.Items.Add("Not StartsWith");
									ColOprGrid.Items.Add("Not EndsWith");
									ColOprGrid.Items.Add("Not Contains");
								}
							}
						}
						string left = MyMode;
						if (Operators.CompareString(left, "G", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "S", TextCompare: false) != 0 || Operators.CompareString(Strings.Trim(MyWValue), "", TextCompare: false) == 0)
							{
								goto end_IL_0001;
							}
							string[] array = Strings.Split(MyWValue, "~~~");
							int num7 = Information.UBound(array);
							for (num3 = 0; num3 <= num7; num3++)
							{
								string[] array2 = Strings.Split(array[num3], ";");
								flag = false;
								int num8 = ColGrid.Items.Count - 1;
								for (num4 = 0; num4 <= num8; num4++)
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
								if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
								{
									flag = false;
									int num9 = Col2Grid.Items.Count - 1;
									for (num4 = 0; num4 <= num9; num4++)
									{
										if (Operators.ConditionalCompareObjectEqual(Col2Grid.Items[num4], array2[4], TextCompare: false))
										{
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										Col2Grid.Items.Add(array2[4]);
									}
								}
								num5++;
								GridWhere.Rows[num5].Cells[0].Value = array2[0];
								GridWhere.Rows[num5].Cells[1].Value = array2[1];
								GridWhere.Rows[num5].Cells[2].Value = array2[2];
								GridWhere.Rows[num5].Cells[3].Value = array2[3];
								if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
								{
									GridWhere.Rows[num5].Cells[5].Value = array2[4];
									GridWhere.Rows[num5].Cells[4].Value = "";
								}
								else
								{
									GridWhere.Rows[num5].Cells[5].Value = "";
									GridWhere.Rows[num5].Cells[4].Value = array2[4];
								}
								GridWhere.Rows[num5].Cells[6].Value = array2[5];
								array2 = null;
							}
							array = null;
							goto end_IL_0001;
						}
						MyWValue = "";
						int num10 = GridWhere.RowCount - 1;
						for (num3 = 0; num3 <= num10; num3++)
						{
							text4 = Conversions.ToString(GridWhere.Rows[num3].Cells[1].Value);
							text6 = Conversions.ToString(GridWhere.Rows[num3].Cells[0].Value);
							text = Conversions.ToString(GridWhere.Rows[num3].Cells[2].Value);
							text2 = Conversions.ToString(GridWhere.Rows[num3].Cells[3].Value);
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text2 = ((!((Operators.CompareString(f_WhereMode, "0", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0))) ? "=" : "==");
							}
							text3 = ((Operators.CompareString(f_WhereMode, "4", TextCompare: false) != 0) ? Conversions.ToString(GridWhere.Rows[num3].Cells[4].Value) : Conversions.ToString(GridWhere.Rows[num3].Cells[5].Value));
							if (Operators.CompareString(text2, "%in%", TextCompare: false) == 0 && Operators.CompareString(text3, "", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.LCase(Strings.Trim(text3)), "c(*)", CompareMethod.Binary))
							{
								text3 = ((!LikeOperator.LikeString(Strings.Trim(text3), "(*)", CompareMethod.Binary)) ? ("c(" + text3 + ")") : ("c" + text3));
							}
							text5 = Conversions.ToString(GridWhere.Rows[num3].Cells[6].Value);
							if (((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text3, "", TextCompare: false) != 0)) | ((Operators.CompareString(text, "", TextCompare: false) != 0) & LikeOperator.LikeString(Strings.UCase(text2), "*MISSING", CompareMethod.Binary)))
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
					case 3505:
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
					goto IL_0de7;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3505;
				continue;
			}
			break;
			IL_0de7:
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
						errsource = "FrmChart - Clear_Where_Grid";
						short num3 = 0;
						short num4 = 0;
						if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) == 0)
						{
							goto end_IL_0001;
						}
						short num5 = (short)(GridWhere.RowCount - 1);
						for (num3 = 0; num3 <= num5; num3 = (short)unchecked(num3 + 1))
						{
							short num6 = (short)(GridWhere.ColumnCount - 1);
							for (num4 = 0; num4 <= num6; num4 = (short)unchecked(num4 + 1))
							{
								if (num4 == 3)
								{
									if (Operators.CompareString(f_WhereMode, "0", TextCompare: false) == 0)
									{
										GridWhere.Rows[num3].Cells[num4].Value = "==";
									}
									else if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0)
									{
										GridWhere.Rows[num3].Cells[num4].Value = "";
									}
									else
									{
										GridWhere.Rows[num3].Cells[num4].Value = "=";
									}
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
					case 435:
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
				try0001_dispatch = 435;
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

	public bool DataType_Check(string InDT, string ChkDT, string MyAxis)
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
				case 270:
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
							goto IL_0022;
						case 6:
							goto IL_002c;
						case 7:
							goto IL_0043;
						case 8:
							goto IL_004c;
						case 9:
							goto IL_007f;
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
					IL_0043:
					num2 = 7;
					text = " Numeric ";
					goto IL_004c;
					IL_004c:
					num2 = 8;
					if (!((Operators.CompareString(ChkDT, "A", TextCompare: false) == 0) | (Operators.CompareString(ChkDT, "I", TextCompare: false) == 0) | (Operators.CompareString(InDT, ChkDT, TextCompare: false) == 0)))
					{
						break;
					}
					goto IL_007f;
					IL_002c:
					num2 = 6;
					if (Operators.CompareString(ChkDT, "N", TextCompare: false) == 0)
					{
						goto IL_0043;
					}
					goto IL_004c;
					IL_007f:
					num2 = 9;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = " Character ";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					ChkDT = Strings.UCase(ChkDT);
					goto IL_0022;
					IL_0022:
					num2 = 5;
					InDT = Strings.UCase(InDT);
					goto IL_002c;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Interaction.MsgBox("Only" + text + "data types are allowed for " + MyAxis + " variables", MsgBoxStyle.Exclamation, "Invalid Type");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 270;
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

	public void Load_Tables(string MyInFile)
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
				ComboBox MyCombo;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 164:
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
							goto IL_0030;
						case 4:
							goto IL_004a;
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
					MyCombo = TxtInFile;
					BuildChart.Load_JMP_Tables_To_Combo(ref MyCombo, ref MyTableArr, f_TemplateType2);
					TxtInFile = MyCombo;
					goto IL_0030;
					IL_0030:
					num2 = 3;
					MyCombo = TxtInFile;
					BuildChart.Update_Combo_Chart_Table(ref MyCombo, MyInFile);
					TxtInFile = MyCombo;
					goto IL_004a;
					IL_004a:
					num2 = 4;
					if (Operators.CompareString(TxtInFile.Text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Populate_Columns();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 164;
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

	public void Set_Ref_Line(ref string MyOut, ref TextBox TxtVal, ref ComboBox CmbStyle, ref ComboBox CmbColor, ref TextBox TxtDesc)
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
				case 211:
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
							goto IL_0026;
						case 4:
							goto IL_003d;
						case 5:
							goto IL_0054;
						case 6:
							goto IL_006c;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003d:
					num2 = 4;
					MyOut = MyOut + ";" + CmbStyle.Text;
					goto IL_0054;
					IL_0054:
					num2 = 5;
					MyOut = MyOut + ";" + CmbColor.Text;
					goto IL_006c;
					IL_0026:
					num2 = 3;
					MyOut = MyOut + ";" + TxtVal.Text;
					goto IL_003d;
					IL_006c:
					num2 = 6;
					MyOut = MyOut + ";" + TxtDesc.Text;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(TxtVal.Text, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0026;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				MyOut += ";;;;";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 211;
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

	public void Update_Ref_Line(int StartIdx, ref string[] SplitArray, ref TextBox TxtVal, ref ComboBox CmbStyle, ref ComboBox CmbColor, ref TextBox TxtDesc)
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
							goto IL_000f;
						case 4:
							goto IL_0022;
						case 6:
							goto IL_006d;
						case 8:
							goto IL_007e;
						case 10:
							goto IL_0090;
						case 12:
							goto IL_00a3;
						case 5:
						case 7:
						case 9:
						case 11:
						case 13:
						case 14:
							goto IL_00b6;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_007e:
					num2 = 8;
					CmbStyle.Text = SplitArray[num5];
					goto IL_00b6;
					IL_006d:
					num2 = 6;
					TxtVal.Text = SplitArray[num5];
					goto IL_00b6;
					IL_0090:
					num2 = 10;
					CmbColor.Text = SplitArray[num5];
					goto IL_00b6;
					IL_00b6:
					num2 = 14;
					num5 = checked(num5 + 1);
					goto IL_00bd;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = checked(StartIdx + 3);
					num5 = StartIdx;
					goto IL_00bd;
					IL_00bd:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0022;
					IL_0022:
					num2 = 4;
					switch (num5)
					{
					case 0:
					case 4:
					case 8:
					case 12:
						break;
					case 1:
					case 5:
					case 9:
					case 13:
						goto IL_007e;
					case 2:
					case 6:
					case 10:
					case 14:
						goto IL_0090;
					case 3:
					case 7:
					case 11:
					case 15:
						goto IL_00a3;
					default:
						goto IL_00b6;
					}
					goto IL_006d;
					IL_00a3:
					num2 = 12;
					TxtDesc.Text = SplitArray[num5];
					goto IL_00b6;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 278;
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

	public void Populate_Columns()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string MyCol = default(string);
		string MyDT = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string text2 = default(string);
		string expression = default(string);
		int num7 = default(int);
		string[] array = default(string[]);
		int num8 = default(int);
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
				case 1278:
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
							goto IL_005e;
						case 11:
							goto IL_0068;
						case 12:
							goto IL_00a0;
						case 13:
							goto IL_00ba;
						case 14:
							goto IL_00d2;
						case 15:
							goto IL_0102;
						case 16:
							goto IL_010f;
						case 17:
							goto IL_0122;
						case 18:
							goto IL_0136;
						case 19:
							goto IL_014a;
						case 20:
							goto IL_015e;
						case 21:
							goto IL_017b;
						case 22:
							goto IL_018f;
						case 23:
						case 24:
							goto IL_01aa;
						case 25:
							goto IL_01bf;
						case 26:
							goto IL_01ce;
						case 27:
							goto IL_01e4;
						case 28:
							goto IL_01f1;
						case 29:
							goto IL_020e;
						case 30:
							goto IL_0251;
						case 31:
							goto IL_0267;
						case 32:
							goto IL_027d;
						case 33:
							goto IL_028f;
						case 34:
						case 35:
							goto IL_0297;
						case 36:
							goto IL_02a6;
						case 37:
							goto IL_02c5;
						case 38:
							goto IL_030e;
						case 39:
							goto IL_0327;
						case 40:
							goto IL_0340;
						case 42:
							goto IL_035c;
						case 43:
							goto IL_0379;
						case 44:
							goto IL_0396;
						case 46:
							goto IL_03b3;
						case 41:
						case 45:
						case 47:
						case 48:
						case 49:
							goto IL_03ce;
						case 50:
							goto IL_03de;
						case 53:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 51:
						case 52:
						case 54:
						case 55:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_020e:
					num2 = 29;
					ColGrid.Items.Add(text + MyCol + text + " (" + MyDT + ")");
					goto IL_0251;
					IL_0251:
					num2 = 30;
					RLCol1.Items.Add(MyCol);
					goto IL_0267;
					IL_01f1:
					num2 = 28;
					if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) != 0)
					{
						goto IL_020e;
					}
					goto IL_0251;
					IL_0267:
					num2 = 31;
					RLCol2.Items.Add(MyCol);
					goto IL_027d;
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
					text2 = "";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					expression = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					MyCol = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					MyDT = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					text = "";
					goto IL_0041;
					IL_0041:
					num2 = 9;
					if (Operators.CompareString(f_QuoteCols, "Y", TextCompare: false) == 0)
					{
						goto IL_005e;
					}
					goto IL_0068;
					IL_005e:
					num2 = 10;
					text = "'";
					goto IL_0068;
					IL_0068:
					num2 = 11;
					if (!((Operators.CompareString(TxtInFile.Text, "", TextCompare: false) != 0) & (Information.UBound(MyTableArr) != -1)))
					{
						break;
					}
					goto IL_00a0;
					IL_00a0:
					num2 = 12;
					text2 = Strings.Trim(Strings.UCase(TxtInFile.Text));
					goto IL_00ba;
					IL_00ba:
					num2 = 13;
					num7 = Information.UBound(MyTableArr);
					num5 = 0;
					goto IL_029e;
					IL_029e:
					if (num5 <= num7)
					{
						goto IL_00d2;
					}
					goto IL_02a6;
					IL_02a6:
					num2 = 36;
					if (LstColumns.Items.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_02c5;
					IL_02c5:
					num2 = 37;
					if (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0 && (Operators.CompareString(f_SpecialChart, "RWAFERMAP", TextCompare: false) != 0 || Operators.CompareString(f_SpecialChart, "3DSURFACE", TextCompare: false) != 0))
					{
						goto IL_030e;
					}
					goto IL_035c;
					IL_027d:
					num2 = 32;
					num6 = checked(num6 + 1);
					goto IL_0286;
					IL_0297:
					num2 = 35;
					num5 = checked(num5 + 1);
					goto IL_029e;
					IL_030e:
					num2 = 38;
					LstColumns.Items.Add("StartingWith:");
					goto IL_0327;
					IL_0327:
					num2 = 39;
					LstColumns.Items.Add("EndingWith:");
					goto IL_0340;
					IL_0340:
					num2 = 40;
					LstColumns.Items.Add("Containing:");
					goto IL_03ce;
					IL_035c:
					num2 = 42;
					if (Operators.CompareString(f_TemplateType2, "JMP", TextCompare: false) == 0)
					{
						goto IL_0379;
					}
					goto IL_03ce;
					IL_0379:
					num2 = 43;
					if (Operators.CompareString(f_Pseudo, "Y", TextCompare: false) == 0)
					{
						goto IL_0396;
					}
					goto IL_03b3;
					IL_0396:
					num2 = 44;
					LstColumns.Items.Add("::_AllCol1_");
					goto IL_03ce;
					IL_03b3:
					num2 = 46;
					LstColumns.Items.Add("::_All1_");
					goto IL_03ce;
					IL_03ce:
					num2 = 49;
					LstColumns.SelectedIndex = 0;
					goto IL_03de;
					IL_03de:
					num2 = 50;
					LastChartTable = TxtInFile.Text;
					goto end_IL_0001_3;
					IL_00d2:
					num2 = 14;
					if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(MyTableArr[num5])), text2 + ";*", CompareMethod.Binary))
					{
						goto IL_0102;
					}
					goto IL_0297;
					IL_0102:
					num2 = 15;
					expression = MyTableArr[num5];
					goto IL_010f;
					IL_010f:
					num2 = 16;
					array = Strings.Split(expression, ";");
					goto IL_0122;
					IL_0122:
					num2 = 17;
					LstColumns.Items.Clear();
					goto IL_0136;
					IL_0136:
					num2 = 18;
					RLCol1.Items.Clear();
					goto IL_014a;
					IL_014a:
					num2 = 19;
					RLCol2.Items.Clear();
					goto IL_015e;
					IL_015e:
					num2 = 20;
					if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) != 0)
					{
						goto IL_017b;
					}
					goto IL_01aa;
					IL_017b:
					num2 = 21;
					ColGrid.Items.Clear();
					goto IL_018f;
					IL_018f:
					num2 = 22;
					ColGrid.Items.Add("");
					goto IL_01aa;
					IL_01aa:
					num2 = 24;
					num8 = Information.UBound(array);
					num6 = 1;
					goto IL_0286;
					IL_0286:
					if (num6 <= num8)
					{
						goto IL_01bf;
					}
					goto IL_028f;
					IL_028f:
					num2 = 33;
					array = null;
					goto IL_0297;
					IL_01bf:
					num2 = 25;
					MyCol = Strings.Trim(array[num6]);
					goto IL_01ce;
					IL_01ce:
					num2 = 26;
					LstColumns.Items.Add(MyCol);
					goto IL_01e4;
					IL_01e4:
					num2 = 27;
					BuildForm.Strip_Col_DT(ref MyCol, ref MyDT);
					goto IL_01f1;
					end_IL_0001_2:
					break;
				}
				num2 = 53;
				LstColumns.Items.Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1278;
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

	public void Populate_Columns_2()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string item = default(string);
		int num5 = default(int);
		string[] array = default(string[]);
		int num6 = default(int);
		string text = default(string);
		string expression = default(string);
		int num7 = default(int);
		int num8 = default(int);
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
				case 498:
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
							goto IL_006f;
						case 10:
							goto IL_0089;
						case 11:
							goto IL_00a1;
						case 12:
							goto IL_00d1;
						case 13:
							goto IL_00de;
						case 14:
							goto IL_00f1;
						case 15:
							goto IL_0105;
						case 16:
							goto IL_011e;
						case 17:
							goto IL_0130;
						case 18:
							goto IL_013f;
						case 19:
							goto IL_0155;
						case 20:
							goto IL_0164;
						case 21:
						case 22:
							goto IL_016c;
						default:
							goto end_IL_0001;
						case 23:
						case 24:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_013f:
					num2 = 18;
					Col2Grid.Items.Add(item);
					goto IL_0155;
					IL_0155:
					num2 = 19;
					num5 = checked(num5 + 1);
					goto IL_015e;
					IL_0130:
					num2 = 17;
					item = Strings.Trim(array[num5]);
					goto IL_013f;
					IL_016c:
					num2 = 22;
					num6 = checked(num6 + 1);
					goto IL_0173;
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
					text = "";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					expression = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					item = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					text2 = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					if (!((Operators.CompareString(TxtPerRow.Text, "", TextCompare: false) != 0) & (Information.UBound(MyTableArr) != -1)))
					{
						goto end_IL_0001_2;
					}
					goto IL_006f;
					IL_006f:
					num2 = 9;
					text = Strings.Trim(Strings.UCase(TxtPerRow.Text));
					goto IL_0089;
					IL_0089:
					num2 = 10;
					num7 = Information.UBound(MyTableArr);
					num6 = 0;
					goto IL_0173;
					IL_0173:
					if (num6 > num7)
					{
						goto end_IL_0001_2;
					}
					goto IL_00a1;
					IL_00a1:
					num2 = 11;
					if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(MyTableArr[num6])), text + ";*", CompareMethod.Binary))
					{
						goto IL_00d1;
					}
					goto IL_016c;
					IL_00d1:
					num2 = 12;
					expression = MyTableArr[num6];
					goto IL_00de;
					IL_00de:
					num2 = 13;
					array = Strings.Split(expression, ";");
					goto IL_00f1;
					IL_00f1:
					num2 = 14;
					Col2Grid.Items.Clear();
					goto IL_0105;
					IL_0105:
					num2 = 15;
					Col2Grid.Items.Add("");
					goto IL_011e;
					IL_011e:
					num2 = 16;
					num8 = Information.UBound(array);
					num5 = 1;
					goto IL_015e;
					IL_015e:
					if (num5 <= num8)
					{
						goto IL_0130;
					}
					goto IL_0164;
					IL_0164:
					num2 = 20;
					array = null;
					goto IL_016c;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 498;
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

	public void InitCmbCtrl(string MyName, ref ComboBox MyCmb)
	{
		string left = BuildChart.Get_StdCtrl(MyName);
		if (Operators.CompareString(left, "N/A", TextCompare: false) == 0)
		{
			MyCmb.Visible = false;
			return;
		}
		MyCmb.Visible = true;
		MyCmb.Text = left;
	}

	public void Load_Theme(string MyTheme, ref object mycmb)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string[] array2 = default(string[]);
		int num6 = default(int);
		string expression = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				object instance;
				object[] array;
				ref string reference;
				object[] array3;
				bool[] obj;
				bool[] array4;
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
							goto IL_000f;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_003c;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0060;
						case 8:
							goto IL_0082;
						case 9:
							goto IL_00f6;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0060:
					num2 = 7;
					if (Operators.CompareString(Strings.Trim(Conversions.ToString(num5)), "", TextCompare: false) != 0)
					{
						goto IL_0082;
					}
					goto IL_00f6;
					IL_0082:
					num2 = 8;
					instance = NewLateBinding.LateGet(mycmb, null, "Items", new object[0], null, null, null);
					array = new object[1];
					reference = ref array2[num5];
					array[0] = reference;
					array3 = array;
					obj = new bool[1] { true };
					array4 = obj;
					NewLateBinding.LateCall(instance, null, "Add", array, null, null, obj, IgnoreReturn: true);
					if (array4[0])
					{
						reference = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(string));
					}
					goto IL_00f6;
					IL_00ff:
					if (num5 > num6)
					{
						break;
					}
					goto IL_0060;
					IL_00f6:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_00ff;
					IL_000b:
					num2 = 2;
					array2 = null;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					expression = General_Procedures.Get_Ini_Data("chart-theme", MyTheme, "", 5000, Globals_Renamed.MySchemaDir + "\\R_Themes.ini");
					goto IL_003c;
					IL_003c:
					num2 = 5;
					array2 = Strings.Split(expression, ",");
					goto IL_004d;
					IL_004d:
					num2 = 6;
					num6 = Information.UBound(array2);
					num5 = 0;
					goto IL_00ff;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				array2 = null;
				break;
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
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
						errsource = "FrmChart - Init_Controls";
						string text = "";
						string text2 = "";
						string text3 = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						int num7 = 0;
						string text4 = "";
						string text5 = "";
						string text6 = "";
						LblFormTitle.Text = BuildChart.Get_StdCtrl("FORM-TITLE");
						if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) || Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0)
						{
							f_QuoteCols = Strings.Trim(Strings.UCase(BuildChart.Get_StdCtrl("QUOTE-COLS")));
							if (Operators.CompareString(f_QuoteCols, "Y", TextCompare: false) == 0)
							{
								f_MyPre = "'";
								f_MyPost = "'";
							}
						}
						text = BuildChart.Get_StdCtrl("START-END-CONTAIN");
						if (Operators.CompareString(Strings.UCase(text), "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text), "N", TextCompare: false) == 0)
						{
							f_StartEndContain = Strings.UCase(text);
						}
						if (Operators.CompareString(f_StartEndContain, "Y", TextCompare: false) == 0)
						{
							CmdColHelp.Visible = true;
						}
						else if (Operators.CompareString(f_StartEndContain, "N", TextCompare: false) == 0)
						{
							CmdColHelp.Visible = false;
						}
						if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
						{
							text = BuildChart.Get_StdCtrl("IN-FILE");
							Populate_Columns3(text, 1, LoadOptCols: false, lClearData: true);
						}
						text = BuildChart.Get_StdCtrl("Y-DATA-LABELS");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							mnuDataLabels.Visible = true;
							if (Operators.CompareString(text, "1", TextCompare: false) == 0)
							{
								mnuDataLabels.Checked = true;
							}
						}
						text = BuildChart.Get_StdCtrl("KEEP-UNUSED-CHARTS");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							mnuKeepUnusedCharts.Visible = true;
							if (Operators.CompareString(text, "1", TextCompare: false) == 0)
							{
								mnuKeepUnusedCharts.Checked = true;
							}
						}
						text = BuildChart.Get_StdCtrl("SWITCH-Y-Z");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0 && Operators.CompareString(text, "Y", TextCompare: false) == 0)
						{
							LblY2.Text = "Z Axis";
						}
						text = BuildChart.Get_StdCtrl("X-COMPUTED");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							f_ComputedX = text;
							mnuXCompute.Visible = true;
						}
						else
						{
							f_ComputedX = "N/A";
						}
						text = BuildChart.Get_StdCtrl("CHART-THEME");
						if ((LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) || Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) && Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							object mycmb = mnuStdTheme;
							Load_Theme("list", ref mycmb);
							mnuStdTheme = (ToolStripComboBox)mycmb;
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								if (Strings.InStr(text, "</tr>") != 0 || Strings.InStr(text, ";") != 0)
								{
									string[] array = Strings.Split(text, "</tr>");
									string[] array2;
									if (Information.UBound(array) >= 0)
									{
										int num8 = Information.UBound(array);
										for (num5 = 0; num5 <= num8; num5++)
										{
											array2 = Strings.Split(array[num5], ";");
											if (Information.UBound(array2) >= 0)
											{
												int num9 = Information.UBound(array2);
												for (num6 = 0; num6 <= num9; num6++)
												{
													GridTheme.Rows[num5].Cells[num6].Value = array2[num6];
													GridTheme.Rows[num5].Cells[num6].Tag = "";
													if ((num6 == 0 || num6 == 4) & (Operators.CompareString(Strings.Trim(array2[num6]), "", TextCompare: false) != 0))
													{
														GridTheme[num6, num5].Style.BackColor = ColorTranslator.FromHtml(array2[num6]);
													}
												}
											}
											array2 = null;
										}
									}
									array = null;
									array2 = null;
								}
								else
								{
									Set_Std_Theme(text);
								}
							}
						}
						else
						{
							IEnumerator enumerator = TabTheme.Controls.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Control control = (Control)enumerator.Current;
								control.Visible = false;
							}
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
							mnuThemeStrip.Visible = false;
						}
						if (Operators.CompareString(f_TemplateType2, "JMP", TextCompare: false) == 0)
						{
							mnuComputations.Visible = false;
						}
						text = BuildChart.Get_StdCtrl("BY-BUTTON");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							cmdBy.Text = text;
							mnuBy.Text = text;
						}
						text = BuildChart.Get_StdCtrl("X-BUTTON");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							CmdX.Text = text;
							mnuX.Text = text;
						}
						text = BuildChart.Get_StdCtrl("X-DATA-TYPE");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							CmdX.Tag = text;
						}
						text = BuildChart.Get_StdCtrl("USE-R-CONDITION");
						switch (text)
						{
						default:
							if (Operators.CompareString(text, "2", TextCompare: false) != 0)
							{
								Lbl1Image.Visible = true;
								Chk1Image.Visible = true;
								CheckBox MyCK = Chk1Image;
								BuildChart.Get_CK_Box(ref MyCK, ref text);
								Chk1Image = MyCK;
								break;
							}
							goto case "N/A";
						case "N/A":
						case null:
						case "":
							Chk1Image.Visible = false;
							Chk1Image.Checked = false;
							break;
						}
						if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
						{
							text = BuildChart.Get_StdCtrl("X-HIGH");
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								Chk1Image.Visible = true;
								Lbl1Image.Visible = true;
								Chk1Image.Enabled = true;
							}
							if (Operators.CompareString(text, "PNG", TextCompare: false) == 0)
							{
								CheckBox MyCK = Chk1Image;
								string MyVal = "1";
								BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
								Chk1Image = MyCK;
							}
						}
						text = BuildChart.Get_StdCtrl("Y-BUTTON");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							CmdY.Text = text;
							mnuY.Text = text;
						}
						text = BuildChart.Get_StdCtrl("Y-DATA-TYPE");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							CmdY.Tag = text;
						}
						text = BuildChart.Get_StdCtrl("Y-COUNT");
						if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "0", TextCompare: false) > 0) & (Operators.CompareString(text, "9999", TextCompare: false) < 0))
						{
							fYMax = Conversions.ToInteger(text);
						}
						text = BuildChart.Get_StdCtrl("BY-COUNT");
						if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "0", TextCompare: false) > 0) & (Operators.CompareString(text, "9999", TextCompare: false) < 0))
						{
							fBYMax = Conversions.ToInteger(text);
						}
						text = BuildChart.Get_StdCtrl("X-COUNT");
						if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "0", TextCompare: false) > 0) & (Operators.CompareString(text, "9999", TextCompare: false) < 0))
						{
							fXMax = Conversions.ToInteger(text);
						}
						text = BuildChart.Get_StdCtrl("GROUP-COUNT");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0 && Operators.CompareString(text, "0", TextCompare: false) > 0 && Operators.CompareString(text, "9999", TextCompare: false) < 0 && Versioned.IsNumeric(text))
						{
							fGroupMax = Conversions.ToInteger(text);
						}
						text = BuildChart.Get_StdCtrl("Y-PREFIX");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							fYBound = text;
						}
						text = BuildChart.Get_StdCtrl("GENERAL_VALUE");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							CmdVal.Visible = true;
							LstVal.Visible = true;
							mnuVal.Visible = true;
							LstY.Height = LstVal.Height;
							LstX.Height = LstY.Height;
							CmdY.Top = (int)Math.Round((double)(CmdVal.Top + CmdVal.Height) + (double)CmdVal.Height / 2.0);
							LstY.Top = CmdY.Top;
							CmdX.Top = (int)Math.Round((double)(CmdY.Top + CmdY.Height) + (double)CmdY.Height / 2.0);
							LstX.Top = CmdX.Top;
							text2 = BuildChart.Get_StdCtrl("VALUE-BUTTON");
							if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "N/A", TextCompare: false) != 0))
							{
								CmdVal.Text = text2;
								mnuVal.Text = text2;
								if (Operators.CompareString(text2.ToLower(), "to date", TextCompare: false) == 0)
								{
									ToolTip1.SetToolTip(CmdVal, "Convert Variable to a Date");
									CmdVal.Text = "Convert to Date";
								}
							}
						}
						text = BuildChart.Get_StdCtrl("CHART-ITEMS-PER-ROW");
						switch (text)
						{
						case "N/A":
							TxtPerRow.Items.Clear();
							text = BuildChart.Get_StdCtrl("PSEUDO-COL");
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								TxtPerRow.Visible = false;
								LblRow.Visible = false;
								break;
							}
							text2 = BuildChart.Get_StdCtrl("PSEUDO-COL-LABEL");
							if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0)
							{
								LblRow.Text = text2;
							}
							else
							{
								LblRow.Text = "Column Name";
							}
							TxtPerRow.Text = text;
							ToolTip1.SetToolTip(LblRow, "");
							break;
						default:
							if (((Operators.CompareString(text, " 1", TextCompare: false) >= 0) & (Operators.CompareString(text, "9", TextCompare: false) <= 0)) || Operators.CompareString(Strings.Trim(text), "-1", TextCompare: false) == 0)
							{
								TxtPerRow.Text = text;
							}
							break;
						case null:
						case "":
							break;
						}
						text = BuildChart.Get_StdCtrl("EXTRA-BUTTON");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							cmdExtra.Visible = false;
						}
						else
						{
							cmdExtra.Text = text;
							text2 = BuildChart.Get_StdCtrl("EXTRA-TOOLTIP");
							if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "N/A", TextCompare: false) != 0))
							{
								ToolTip1.SetToolTip(cmdExtra, text2);
							}
							text2 = Strings.UCase(BuildChart.Get_StdCtrl("EXTRA-XY"));
							if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "N/A", TextCompare: false) != 0))
							{
								cmdExtra.Tag = Strings.Trim(Strings.UCase(text2));
							}
							else
							{
								cmdExtra.Tag = "";
							}
							if (Operators.CompareString(Strings.LCase(text), "custom group", TextCompare: false) == 0)
							{
								f_ExtraData = "";
								f_CustomGroup = BuildChart.Get_StdCtrl("CUSTOM-GROUP");
								if ((Operators.CompareString(Strings.UCase(f_CustomGroup), "N/A", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(f_CustomGroup), "NA", TextCompare: false) == 0))
								{
									f_CustomGroup = "";
								}
								f_CustomGroup = BuildChart.Strip_Custom_Col(f_CustomGroup, ref l_Generalval2);
							}
							else
							{
								f_CustomGroup = "";
								f_ExtraData = text;
								num7 = Strings.InStrRev(f_ExtraData, "/");
								if (num7 != 0)
								{
									f_ExtraData = Strings.Trim(Strings.Mid(f_ExtraData, 1, num7 - 1));
								}
								if (Operators.CompareString(f_ExtraData, "Asc", TextCompare: false) == 0)
								{
									f_ExtraData = "Ascending";
								}
								else if (Operators.CompareString(f_ExtraData, "Desc", TextCompare: false) == 0)
								{
									f_ExtraData = "Descending";
								}
								f_ExtraData = " ** " + f_ExtraData;
							}
						}
						text = BuildChart.Get_StdCtrl("GROUP-BUTTON");
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							CmdGroup.Text = text;
							mnuGroup.Text = text;
						}
						CmbY.Items.Clear();
						f_Y_Map = BuildChart.Get_StdCtrl("Y-FUNCTIONS-MAP");
						text = BuildChart.Get_StdCtrl("Y-FUNCTIONS");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								CmbY.Visible = true;
								string[] array = Strings.Split(text, "|");
								int num10 = Information.UBound(array);
								for (num5 = 0; num5 <= num10; num5++)
								{
									CmbY.Items.Add(array[num5]);
								}
								CmbY.Text = Conversions.ToString(CmbY.Items[0]);
								array = null;
							}
						}
						else
						{
							CmbY.Visible = false;
						}
						LstOther.Items.Clear();
						text = BuildChart.Get_StdCtrl("LIST-OTHER");
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							LstOther.Visible = true;
							string[] array = Strings.Split(text, "|");
							int num11 = Information.UBound(array);
							for (num5 = 0; num5 <= num11; num5++)
							{
								LstOther.Items.Add(array[num5]);
							}
							array = null;
							GrpGrid.Visible = false;
							GrpFrame.Visible = false;
							GrpRowLegend.Visible = false;
						}
						text = Strings.UCase(Strings.Trim(BuildChart.Get_StdCtrl("WHERE-MODE")));
						f_WhereMode = text;
						if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) == 0)
						{
							GridWhere.Visible = false;
							LblWhere.Visible = false;
							CmdUp.Visible = false;
							cmdDown.Visible = false;
							cmdDelete.Visible = false;
						}
						else
						{
							GridWhere.RowCount = f_WRows;
							Clear_Where_Grid();
							text2 = BuildChart.Get_StdCtrl("CHART-WHERE");
							if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0)
							{
								IsRecursive = true;
								if (Conversions.ToDouble(f_WhereMode) == 4.0)
								{
									Populate_Columns_2();
								}
								Get_Set_Where("S", ref text2);
								IsRecursive = false;
							}
						}
						if ((Operators.CompareString(f_WhereMode, "1", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "2", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0))
						{
							GridWhere.Top = LstColumns.Top;
							GridWhere.Height = LstColumns.Height - 15;
							LstColumns.Visible = false;
							CmdUp.Top = GridWhere.Top;
							cmdDown.Top = CmdUp.Top + 36;
							cmdDelete.Top = CmdUp.Top + 76;
							LblRoles.Visible = false;
							LblWhere.Visible = false;
							cmdClear.Visible = false;
							CmdRemove.Visible = false;
							CmdColHelp.Visible = false;
							mnuRemove.Visible = false;
							mnuClear.Visible = false;
							if (Operators.CompareString(f_WhereMode, "1", TextCompare: false) == 0)
							{
								LblCols.Text = "Set Unique Column Variable Names to use within For";
							}
							else if (Operators.CompareString(f_WhereMode, "2", TextCompare: false) == 0)
							{
								LblCols.Text = "Rename Column Names";
							}
							else if (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0)
							{
								LblCols.Text = "Set Criteria for selecting columns from a table";
								TxtPerRow.Visible = false;
							}
							else if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
							{
								LblCols.Text = "Choose how the Selected Tables Should be Joined";
								LblInFile.Text = "Table 1";
								GridWhere.Columns[2].HeaderText = "Table 1";
							}
						}
						text = BuildChart.Get_StdCtrl("Y");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							LstY.Visible = false;
							CmdY.Visible = false;
							mnuY.Visible = false;
						}
						else if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(text, ";");
							int num12 = Information.UBound(array);
							for (num5 = 0; num5 <= num12; num5++)
							{
								LstY.Items.Add(array[num5]);
							}
							array = null;
						}
						text = BuildChart.Get_StdCtrl("X");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							LstX.Visible = false;
							CmdX.Visible = false;
							mnuX.Visible = false;
							LstY.Height = LstColumns.Height;
						}
						else if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(text, ";");
							int num13 = Information.UBound(array);
							for (num5 = 0; num5 <= num13; num5++)
							{
								LstX.Items.Add(array[num5]);
							}
							array = null;
						}
						text = BuildChart.Get_StdCtrl("BY");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							LstBy.Visible = false;
							cmdBy.Visible = false;
							mnuBy.Visible = false;
						}
						else if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(text, ";");
							int num14 = Information.UBound(array);
							for (num5 = 0; num5 <= num14; num5++)
							{
								LstBy.Items.Add(array[num5]);
							}
							array = null;
						}
						text = BuildChart.Get_StdCtrl("GROUP-BY");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							LstGroup.Visible = false;
							CmdGroup.Visible = false;
							mnuGroup.Visible = false;
						}
						else
						{
							LstGroup.Visible = true;
							CmdGroup.Visible = true;
							mnuGroup.Visible = true;
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(text, ";");
								int num15 = Information.UBound(array);
								for (num5 = 0; num5 <= num15; num5++)
								{
									LstGroup.Items.Add(array[num5]);
								}
								array = null;
							}
						}
						text = BuildChart.Get_StdCtrl("GENERAL_VALUE");
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							LstVal.Visible = false;
							CmdVal.Visible = false;
							mnuVal.Visible = false;
						}
						else if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(text, ";");
							int num16 = Information.UBound(array);
							for (num5 = 0; num5 <= num16; num5++)
							{
								LstVal.Items.Add(array[num5]);
							}
							array = null;
							text2 = BuildChart.Get_StdCtrl("VALUE-BUTTON");
							if (Operators.CompareString(text2, "", TextCompare: false) != 0)
							{
								CmdVal.Text = text2;
								mnuVal.Text = text2;
							}
						}
						MyTitleTabVisible = Strings.UCase(BuildChart.Get_StdCtrl("TITLE-TAB-VISIBLE"));
						if ((Operators.CompareString(MyTitleTabVisible, "N", TextCompare: false) == 0) | (Operators.CompareString(MyTitleTabVisible, "NO", TextCompare: false) == 0))
						{
							IEnumerator enumerator2 = TabTitles.Controls.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								Control control2 = (Control)enumerator2.Current;
								control2.Visible = false;
							}
							if (enumerator2 is IDisposable)
							{
								(enumerator2 as IDisposable).Dispose();
							}
						}
						else
						{
							TxtTitle.Text = BuildChart.Get_StdCtrl("TITLE");
							text4 = BuildChart.Get_StdCtrl("X-AXIS");
							if (Operators.CompareString(text4, "N/A", TextCompare: false) == 0)
							{
								TxtXAxis.Visible = false;
								LblX.Visible = false;
							}
							else
							{
								TxtXAxis.Visible = true;
								LblX.Visible = true;
								TxtXAxis.Text = text4;
							}
							text5 = BuildChart.Get_StdCtrl("Y-AXIS");
							if (Operators.CompareString(text5, "N/A", TextCompare: false) == 0)
							{
								TxtYAxis.Visible = false;
								LblY.Visible = false;
							}
							else
							{
								TxtYAxis.Text = text5;
								TxtYAxis.Visible = true;
								LblY.Visible = true;
							}
							text6 = BuildChart.Get_StdCtrl("Y-AXIS-2");
							if (Operators.CompareString(text6, "N/A", TextCompare: false) == 0)
							{
								TxtY2Axis.Visible = false;
								LblY2.Visible = false;
							}
							else
							{
								TxtY2Axis.Text = text6;
								TxtY2Axis.Visible = true;
								LblY2.Visible = true;
							}
							if ((Operators.CompareString(text4, "N/A", TextCompare: false) == 0) & (Operators.CompareString(text5, "N/A", TextCompare: false) == 0) & (Operators.CompareString(text6, "N/A", TextCompare: false) == 0))
							{
								num4 = (int)Math.Round((double)GrpTitle.Height / 2.0);
								GrpTitle.Height = num4;
								GrpGrid.Top = (int)Math.Round((double)GrpGrid.Top - (double)num4 / 1.5);
								GrpFrame.Top = (int)Math.Round((double)GrpFrame.Top - (double)num4 / 4.0);
								LblLow.Visible = false;
								LblHigh.Visible = false;
							}
							ComboBox MyCmb = TxtHiY;
							InitCmbCtrl("Y-MAX", ref MyCmb);
							TxtHiY = MyCmb;
							MyCmb = TxtLOY;
							InitCmbCtrl("Y-MIN", ref MyCmb);
							TxtLOY = MyCmb;
							MyCmb = TxtHiX;
							InitCmbCtrl("X-MAX", ref MyCmb);
							TxtHiX = MyCmb;
							MyCmb = TxtLoX;
							InitCmbCtrl("X-MIN", ref MyCmb);
							TxtLoX = MyCmb;
							MyCmb = TxtHiY2;
							InitCmbCtrl("Y-2-MAX", ref MyCmb);
							TxtHiY2 = MyCmb;
							MyCmb = TxtLoY2;
							InitCmbCtrl("Y-2-MIN", ref MyCmb);
							TxtLoY2 = MyCmb;
							MyCmb = TxtIncY;
							InitCmbCtrl("Y-INC", ref MyCmb);
							TxtIncY = MyCmb;
							MyCmb = TxtIncY2;
							InitCmbCtrl("Y2-INC", ref MyCmb);
							TxtIncY2 = MyCmb;
							MyCmb = TxtIncX;
							InitCmbCtrl("X-INC", ref MyCmb);
							TxtIncX = MyCmb;
							MyCmb = cmbXRot;
							InitCmbCtrl("X-AXIS-ROTATE", ref MyCmb);
							cmbXRot = MyCmb;
							MyCmb = cmbYRot;
							InitCmbCtrl("Y-AXIS-ROTATE", ref MyCmb);
							cmbYRot = MyCmb;
							MyCmb = cmbY2Rot;
							InitCmbCtrl("Y2-AXIS-ROTATE", ref MyCmb);
							cmbY2Rot = MyCmb;
							text = BuildChart.Get_StdCtrl("X-AXIS-ROTATE");
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								lblrot.Visible = false;
							}
							MyCmb = cmbXType;
							InitCmbCtrl("LOG-X", ref MyCmb);
							cmbXType = MyCmb;
							MyCmb = cmbYType;
							InitCmbCtrl("LOG-Y", ref MyCmb);
							cmbYType = MyCmb;
							MyCmb = cmbY2Type;
							InitCmbCtrl("LOG-Y2", ref MyCmb);
							cmbY2Type = MyCmb;
							text = BuildChart.Get_StdCtrl("LOG-X");
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								lblType.Visible = false;
							}
							CheckBox MyCK = X_Major_Grid;
							string MyVal = BuildChart.Get_StdCtrl("X-MAJOR-GRID");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							X_Major_Grid = MyCK;
							MyCK = X_Minor_Grid;
							MyVal = BuildChart.Get_StdCtrl("X-MINOR-GRID");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							X_Minor_Grid = MyCK;
							MyCK = Y_Major_Grid;
							MyVal = BuildChart.Get_StdCtrl("Y-MAJOR-GRID");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							Y_Major_Grid = MyCK;
							MyCK = Y_Minor_Grid;
							MyVal = BuildChart.Get_StdCtrl("Y-MINOR-GRID");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							Y_Minor_Grid = MyCK;
							MyCK = X_Major_Ticks;
							MyVal = BuildChart.Get_StdCtrl("X-MAJOR-TICKS");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							X_Major_Ticks = MyCK;
							MyCK = X_Minor_Ticks;
							MyVal = BuildChart.Get_StdCtrl("X-MINOR-TICKS");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							X_Minor_Ticks = MyCK;
							MyCK = Y_Major_Ticks;
							MyVal = BuildChart.Get_StdCtrl("Y-MAJOR-TICKS");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							Y_Major_Ticks = MyCK;
							MyCK = Y_Minor_Ticks;
							MyVal = BuildChart.Get_StdCtrl("Y-MINOR-TICKS");
							BuildChart.Get_CK_Box(ref MyCK, ref MyVal);
							Y_Minor_Ticks = MyCK;
							if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) || Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0)
							{
								text2 = BuildChart.Get_StdCtrl("LEGENDTITLE");
								if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0 && Operators.CompareString(text2, "*N/A*", TextCompare: false) != 0)
								{
									TxtLTitle.Text = text2;
								}
								if (Operators.CompareString(text2, "*N/A*", TextCompare: false) == 0)
								{
									GrpRLegend.Visible = false;
								}
								else
								{
									text = BuildChart.Get_StdCtrl("LEGENDPOS");
									if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
									{
										CmbLPos.Text = text;
									}
									text = BuildChart.Get_StdCtrl("LEGENDSIZE");
									if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
									{
										CmbLSize.Text = text;
									}
									text = BuildChart.Get_StdCtrl("LEGENDBG");
									if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
									{
										CmbLBG.Text = text;
									}
									text = BuildChart.Get_StdCtrl("LEGENDCOLS");
									if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
									{
										CmbLCols.Text = text;
									}
									text = BuildChart.Get_StdCtrl("LEGENDKSIZE");
									if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
									{
										CmbLKSize.Text = text;
									}
								}
							}
							text = BuildChart.Get_StdCtrl("ROW-LEGEND");
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(text, ";");
								int num17 = Information.UBound(array);
								for (num5 = 0; num5 <= num17; num5++)
								{
									switch (num5)
									{
									case 0:
										RLCol1.Text = array[num5];
										break;
									case 1:
										if (Operators.CompareString(array[num5], "1", TextCompare: false) == 0)
										{
											RLColor1.Checked = true;
										}
										break;
									case 2:
										if (Operators.CompareString(array[num5], "1", TextCompare: false) == 0)
										{
											RLMarker1.Checked = true;
										}
										break;
									case 3:
										RLCol2.Text = array[num5];
										break;
									case 4:
										if (Operators.CompareString(array[num5], "1", TextCompare: false) == 0)
										{
											RLColor2.Checked = true;
										}
										break;
									case 5:
										if (Operators.CompareString(array[num5], "1", TextCompare: false) == 0)
										{
											RLMarker2.Checked = true;
										}
										break;
									}
								}
								array = null;
							}
							if (LikeOperator.LikeString(text, "N/A*", CompareMethod.Binary))
							{
								GrpRowLegend.Visible = false;
							}
							text = BuildChart.Get_StdCtrl("X-AXIS-REF-LINE");
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(text, ";");
								TextBox TxtVal = XRLineVal1;
								MyCmb = XRlineStyle1;
								ComboBox CmbColor = XRlineColor1;
								TextBox TxtDesc = XRlineTxt1;
								Update_Ref_Line(0, ref array, ref TxtVal, ref MyCmb, ref CmbColor, ref TxtDesc);
								XRlineTxt1 = TxtDesc;
								XRlineColor1 = CmbColor;
								XRlineStyle1 = MyCmb;
								XRLineVal1 = TxtVal;
								TxtDesc = XRLineVal2;
								CmbColor = XRlineStyle2;
								MyCmb = XRlineColor2;
								TxtVal = XRlineTxt2;
								Update_Ref_Line(4, ref array, ref TxtDesc, ref CmbColor, ref MyCmb, ref TxtVal);
								XRlineTxt2 = TxtVal;
								XRlineColor2 = MyCmb;
								XRlineStyle2 = CmbColor;
								XRLineVal2 = TxtDesc;
								TxtVal = XRLineVal3;
								MyCmb = XRlineStyle3;
								CmbColor = XRlineColor3;
								TxtDesc = XRlineTxt3;
								Update_Ref_Line(8, ref array, ref TxtVal, ref MyCmb, ref CmbColor, ref TxtDesc);
								XRlineTxt3 = TxtDesc;
								XRlineColor3 = CmbColor;
								XRlineStyle3 = MyCmb;
								XRLineVal3 = TxtVal;
								TxtDesc = XRLineVal4;
								CmbColor = XRlineStyle4;
								MyCmb = XRlineColor4;
								TxtVal = XRlineTxt4;
								Update_Ref_Line(12, ref array, ref TxtDesc, ref CmbColor, ref MyCmb, ref TxtVal);
								XRlineTxt4 = TxtVal;
								XRlineColor4 = MyCmb;
								XRlineStyle4 = CmbColor;
								XRLineVal4 = TxtDesc;
								array = null;
							}
							if (LikeOperator.LikeString(text, "N/A*", CompareMethod.Binary))
							{
								XRLineVal1.Visible = false;
								XRLineVal2.Visible = false;
								XRLineVal3.Visible = false;
								XRLineVal4.Visible = false;
								XRlineStyle1.Visible = false;
								XRlineStyle2.Visible = false;
								XRlineStyle3.Visible = false;
								XRlineStyle4.Visible = false;
								XRlineColor1.Visible = false;
								XRlineColor2.Visible = false;
								XRlineColor3.Visible = false;
								XRlineColor4.Visible = false;
								XRlineTxt1.Visible = false;
								XRlineTxt2.Visible = false;
								XRlineTxt3.Visible = false;
								XRlineTxt4.Visible = false;
								LblXRefLine1.Visible = false;
								LblXRefLine2.Visible = false;
								LblXRefLine3.Visible = false;
								LblXRefLine4.Visible = false;
								LblRefLineDesc.Top = YRlineTxt1.Top - 25;
							}
							text = BuildChart.Get_StdCtrl("Y-AXIS-REF-LINE");
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(text, ";");
								TextBox TxtVal = YRLineVal1;
								MyCmb = YRlineStyle1;
								ComboBox CmbColor = YRlineColor1;
								TextBox TxtDesc = YRlineTxt1;
								Update_Ref_Line(0, ref array, ref TxtVal, ref MyCmb, ref CmbColor, ref TxtDesc);
								YRlineTxt1 = TxtDesc;
								YRlineColor1 = CmbColor;
								YRlineStyle1 = MyCmb;
								YRLineVal1 = TxtVal;
								TxtDesc = YRLineVal2;
								CmbColor = YRlineStyle2;
								MyCmb = YRlineColor2;
								TxtVal = YRlineTxt2;
								Update_Ref_Line(4, ref array, ref TxtDesc, ref CmbColor, ref MyCmb, ref TxtVal);
								YRlineTxt2 = TxtVal;
								YRlineColor2 = MyCmb;
								YRlineStyle2 = CmbColor;
								YRLineVal2 = TxtDesc;
								TxtVal = YRLineVal3;
								MyCmb = YRlineStyle3;
								CmbColor = YRlineColor3;
								TxtDesc = YRlineTxt3;
								Update_Ref_Line(8, ref array, ref TxtVal, ref MyCmb, ref CmbColor, ref TxtDesc);
								YRlineTxt3 = TxtDesc;
								YRlineColor3 = CmbColor;
								YRlineStyle3 = MyCmb;
								YRLineVal3 = TxtVal;
								TxtDesc = YRLineVal4;
								CmbColor = YRlineStyle4;
								MyCmb = YRlineColor4;
								TxtVal = YRlineTxt4;
								Update_Ref_Line(12, ref array, ref TxtDesc, ref CmbColor, ref MyCmb, ref TxtVal);
								YRlineTxt4 = TxtVal;
								YRlineColor4 = MyCmb;
								YRlineStyle4 = CmbColor;
								YRLineVal4 = TxtDesc;
								array = null;
								if ((Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) | (Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0))
								{
									text2 = BuildChart.Get_StdCtrl("Y-TXT-ADJ-1");
									if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0)
									{
										YRTxtAdj1.Text = text2;
									}
								}
							}
							if (LikeOperator.LikeString(text, "N/A*", CompareMethod.Binary))
							{
								YRLineVal1.Visible = false;
								YRLineVal2.Visible = false;
								YRLineVal3.Visible = false;
								YRLineVal4.Visible = false;
								YRTxtAdj1.Visible = false;
								YRlineStyle1.Visible = false;
								YRlineStyle2.Visible = false;
								YRlineStyle3.Visible = false;
								YRlineStyle4.Visible = false;
								YRlineColor1.Visible = false;
								YRlineColor2.Visible = false;
								YRlineColor3.Visible = false;
								YRlineColor4.Visible = false;
								YRlineTxt1.Visible = false;
								YRlineTxt2.Visible = false;
								YRlineTxt3.Visible = false;
								YRlineTxt4.Visible = false;
								LblYRefLine1.Visible = false;
								LblYRefLine2.Visible = false;
								LblYRefLine3.Visible = false;
								LblYRefLine4.Visible = false;
								LblRefLineDesc.Visible = false;
							}
							text = BuildChart.Get_StdCtrl("FRAME-X");
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								FrameX.Text = text;
							}
							text = BuildChart.Get_StdCtrl("FRAME-Y");
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								FrameY.Text = text;
							}
							text = ((Operators.CompareString(f_TemplateType2, "JMP", TextCompare: false) != 0) ? BuildChart.Get_StdCtrl("FRAME-COLOR2") : BuildChart.Get_StdCtrl("FRAME-COLOR"));
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								FrameColor.Text = text;
							}
							text = BuildChart.Get_StdCtrl("FRAME-MARKER");
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								FrameMarker.Visible = false;
								lblFMarker.Visible = false;
							}
							else if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								FrameMarker.Text = text;
							}
							text = BuildChart.Get_StdCtrl("FRAME-COLOR-LIST");
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								MyCmb = FrameColor;
								Populate_Combo(ref MyCmb, text);
								FrameColor = MyCmb;
							}
							if ((Operators.CompareString(FrameX.Text, "N/A", TextCompare: false) == 0) & (Operators.CompareString(FrameY.Text, "N/A", TextCompare: false) == 0) & (Operators.CompareString(FrameColor.Text, "N/A", TextCompare: false) == 0) & (Operators.CompareString(FrameMarker.Text, "N/A", TextCompare: false) == 0))
							{
								GrpFrame.Visible = false;
							}
							text = Strings.UCase(BuildChart.Get_StdCtrl("GRID-FRAME-VISIBLE"));
							if ((Operators.CompareString(text, "N", TextCompare: false) == 0) | (Operators.CompareString(text, "NO", TextCompare: false) == 0))
							{
								GrpGrid.Visible = false;
							}
						}
						fSpecialSetup = BuildChart.Get_StdCtrl("SPECIAL-SETUP");
						if (Operators.CompareString(fSpecialSetup, "N/A", TextCompare: false) != 0)
						{
							LblCols.Visible = false;
							CmdColHelp.Visible = false;
							LblRoles.Visible = false;
							CmdRemove.Visible = false;
							cmdClear.Visible = false;
							mnuRemove.Visible = false;
							mnuClear.Visible = false;
							base.Height = 300;
							if (Operators.CompareString(fSpecialSetup, "1", TextCompare: false) == 0)
							{
								LblInFile.Text = "Display Text";
								TxtInFile.DropDownStyle = ComboBoxStyle.DropDown;
								TxtInFile.Text = BuildChart.Get_StdCtrl("TITLE-OTHER");
								TxtInFile.Width = cmdOK.Left - 25;
								TxtInFile.Left = LblInFile.Left;
								LblOption0.Top = LstColumns.Top;
								LblOption1.Top = LstColumns.Top;
							}
							else if (Operators.CompareString(fSpecialSetup, "2", TextCompare: false) == 0)
							{
								TxtPerRow.Items.Clear();
								TxtPerRow.Visible = true;
								TxtPerRow.Text = BuildChart.Get_StdCtrl("TITLE-OTHER");
								LblRow.Visible = true;
								LblRow.Text = BuildChart.Get_StdCtrl("PSEUDO-COL-LABEL");
							}
							LstColumns.Visible = false;
						}
						goto end_IL_0001;
					}
					case 10926:
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
					goto IL_2ae4;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 10926;
				continue;
			}
			break;
			IL_2ae4:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Add_ListBox(ref ListBox DestList, string MyExtraData = "")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyCol = default(string);
		string Is_Alli_ = default(string);
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
					case 691:
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
								goto IL_003e;
							case 10:
								goto IL_0050;
							case 12:
							case 13:
								goto IL_0069;
							case 14:
								goto IL_0087;
							case 15:
								goto IL_00b2;
							case 16:
								goto IL_00bf;
							case 17:
								goto IL_0110;
							case 18:
								goto IL_0129;
							case 20:
								goto IL_0145;
							case 19:
							case 21:
							case 22:
								goto IL_014d;
							case 23:
								goto IL_0168;
							case 24:
							case 25:
								goto IL_0182;
							case 26:
								goto IL_0191;
							case 27:
								goto IL_01a5;
							case 28:
								goto IL_01c4;
							case 30:
								goto IL_01da;
							case 31:
								goto IL_01f6;
							case 29:
							case 32:
							case 33:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 9:
							case 11:
							case 34:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0129:
						num2 = 18;
						MyCol = f_MyPre + MyCol + f_MyPost;
						goto IL_014d;
						IL_0145:
						num2 = 20;
						MyCol = Is_Alli_;
						goto IL_014d;
						IL_0110:
						num2 = 17;
						if (Operators.CompareString(Is_Alli_, "", TextCompare: false) == 0)
						{
							goto IL_0129;
						}
						goto IL_0145;
						IL_014d:
						num2 = 22;
						if (!Check_Same_Var(ref DestList, MyCol + MyExtraData))
						{
							goto IL_0168;
						}
						goto IL_0182;
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
						Is_Alli_ = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						num6 = LstColumns.SelectedIndex;
						goto IL_003e;
						IL_003e:
						num2 = 8;
						if (num6 == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_0050;
						IL_0050:
						num2 = 10;
						if (Check_Alli_(ref DestList, ref Is_Alli_))
						{
							goto end_IL_0001_3;
						}
						goto IL_0069;
						IL_0069:
						num2 = 13;
						num7 = LstColumns.SelectedIndices.Count - 1;
						num5 = 0;
						goto IL_0189;
						IL_0189:
						if (num5 <= num7)
						{
							goto IL_0087;
						}
						goto IL_0191;
						IL_0191:
						num2 = 26;
						LstColumns.SelectedItems.Clear();
						goto IL_01a5;
						IL_01a5:
						num2 = 27;
						if (LstColumns.Items.Count - 1 > num6)
						{
							goto IL_01c4;
						}
						goto IL_01da;
						IL_01c4:
						num2 = 28;
						LstColumns.SelectedIndex = num6 + 1;
						break;
						IL_01da:
						num2 = 30;
						if (LstColumns.Items.Count <= 0)
						{
							break;
						}
						goto IL_01f6;
						IL_01f6:
						num2 = 31;
						LstColumns.SelectedIndex = 0;
						break;
						IL_0087:
						num2 = 14;
						MyCol = Conversions.ToString(LstColumns.Items[LstColumns.SelectedIndices[num5]]);
						goto IL_00b2;
						IL_00b2:
						num2 = 15;
						BuildForm.Strip_Col_DT(ref MyCol, ref MyDT);
						goto IL_00bf;
						IL_00bf:
						num2 = 16;
						if (Operators.CompareString(Strings.UCase(DestList.Name), "LSTX", TextCompare: false) != 0 || Operators.CompareString(Is_Alli_, "", TextCompare: false) != 0 || DataType_Check(MyDT, Conversions.ToString(CmdX.Tag), "X"))
						{
							goto IL_0110;
						}
						goto IL_0182;
						IL_0168:
						num2 = 23;
						DestList.Items.Add(MyCol + MyExtraData);
						goto IL_0182;
						IL_0182:
						num2 = 25;
						num5++;
						goto IL_0189;
						end_IL_0001_2:
						break;
					}
					num2 = 33;
					LstColumns.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 691;
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
					bool flag2;
					string text11;
					string text3;
					string text2;
					string text4;
					string text10;
					string MyDT2;
					string text9;
					string MyDT;
					string text8;
					string text;
					int num3;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmChart - CmdOK_Click";
						num3 = 0;
						int num4 = 0;
						text = "";
						text2 = "";
						text3 = "";
						int num5 = 0;
						text4 = "";
						int num6 = 0;
						string text5 = "";
						string text6 = "";
						string text7 = "";
						text8 = "";
						text9 = "";
						MyDT = "";
						MyDT2 = "";
						text10 = "";
						bool flag = false;
						flag2 = false;
						int num7 = 0;
						text11 = "";
						string text12 = "";
						string left = "N";
						text4 = ((Operators.CompareString(fSpecialSetup, "1", TextCompare: false) != 0) ? Strings.Trim(TxtInFile.Text) : TxtInFile.Text);
						text3 = "Missing Data";
						text10 = BuildChart.Get_StdCtrl("X-OPT");
						if ((LstX.Items.Count == 0) & (Operators.CompareString(Strings.UCase(text10), "Y", TextCompare: false) != 0))
						{
							text2 = "No X Columns were assigned on the Columns Tab.";
						}
						else
						{
							text9 = BuildChart.Get_StdCtrl("SWITCH-Y-Z");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								left = Strings.Trim(Strings.UCase(text9));
							}
							text9 = BuildChart.Get_StdCtrl("Y");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0 && LstY.Items.Count == 0)
							{
								text2 = ((Operators.CompareString(left, "Y", TextCompare: false) != 0) ? "No Y Columns were assigned on the Columns Tab." : "No Z Columns were assigned on the Columns Tab.");
							}
							else
							{
								text10 = BuildChart.Get_StdCtrl("GROUP-OPT");
								if ((LstGroup.Items.Count == 0) & (Operators.CompareString(Strings.UCase(text10), "N", TextCompare: false) == 0))
								{
									text2 = "No " + CmdGroup.Text + " column(s) were assigned on the Columns Tab.";
								}
								else
								{
									text9 = BuildChart.Get_StdCtrl("GENERAL_VALUE");
									text10 = BuildChart.Get_StdCtrl("VALUE-OPT");
									if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0 && ((LstVal.Items.Count == 0) & (Operators.CompareString(Strings.UCase(text10), "Y", TextCompare: false) != 0)))
									{
										text2 = ((Operators.CompareString(left, "Y", TextCompare: false) != 0) ? "No \"VALUE\" items were assigned on the Columns Tab." : "No Y columns were assigned on the Columns Tab.");
									}
									else
									{
										text9 = BuildChart.Get_StdCtrl("USE-R-CONDITION");
										if (((Operators.CompareString(text9, "", TextCompare: false) != 0) & (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)) && Operators.CompareString(text9, "2", TextCompare: false) != 0)
										{
											flag2 = true;
										}
										if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
										{
											if (Operators.CompareString(text4, "", TextCompare: false) == 0)
											{
												text2 = "You must specify an Input File on the Columns Tab.";
												goto IL_241d;
											}
											BuildChart.Set_StdCtrl("IN-FILE", text4);
											num5 = Strings.InStrRev(BuildForm.Replace_Globals(text4), ".");
											if (num5 != 0)
											{
												text9 = Strings.UCase(Strings.Trim(Strings.Mid(BuildForm.Replace_Globals(text4) + " ", num5 + 1)));
												if (Operators.CompareString(text9, "TAB", TextCompare: false) == 0)
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
										}
										if (Operators.CompareString(text4, "", TextCompare: false) == 0)
										{
											text2 = ((Operators.CompareString(fSpecialSetup, "1", TextCompare: false) != 0) ? "You must specify an Input File on the Columns Tab." : "You must specify the Text to display.");
										}
										else
										{
											if (!((Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0) | (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0)))
											{
												goto IL_05bf;
											}
											text8 = Strings.Trim(TxtHiX.Text);
											text9 = Strings.Trim(TxtLoX.Text);
											if ((Operators.CompareString(text8, "", TextCompare: false) == 0 && Operators.CompareString(text9, "", TextCompare: false) != 0) || (Operators.CompareString(text8, "", TextCompare: false) != 0 && Operators.CompareString(text9, "", TextCompare: false) == 0) || (Operators.CompareString(text8, "*", TextCompare: false) == 0 && Operators.CompareString(text9, "*", TextCompare: false) != 0) || (Operators.CompareString(text9, "*", TextCompare: false) == 0 && Operators.CompareString(text8, "*", TextCompare: false) != 0))
											{
												text2 = "When assigning the X axis minimum and maximum limits, you must set both the low and high values.";
											}
											else
											{
												text8 = Strings.Trim(TxtHiY.Text);
												text9 = Strings.Trim(TxtLOY.Text);
												if ((Operators.CompareString(text8, "", TextCompare: false) != 0 || Operators.CompareString(text9, "", TextCompare: false) == 0) && (Operators.CompareString(text8, "", TextCompare: false) == 0 || Operators.CompareString(text9, "", TextCompare: false) != 0) && (Operators.CompareString(text8, "*", TextCompare: false) != 0 || Operators.CompareString(text9, "*", TextCompare: false) == 0) && (Operators.CompareString(text9, "*", TextCompare: false) != 0 || Operators.CompareString(text8, "*", TextCompare: false) == 0))
												{
													goto IL_05bf;
												}
												text2 = "When assigning the Y axis minimum and maximum limits, you must set both the low and high values.";
											}
										}
									}
								}
							}
						}
						goto IL_241d;
					}
					case 9311:
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
						IL_241d:
						Interaction.MsgBox(text2, MsgBoxStyle.Exclamation, text3);
						goto end_IL_0001;
						IL_05bf:
						if (Operators.CompareString(f_SpecialChart, "RBARCHART", TextCompare: false) != 0 && Operators.CompareString(f_SpecialChart, "ROVERLAY", TextCompare: false) != 0)
						{
							goto IL_07fa;
						}
						if (Operators.CompareString(f_SpecialChart, "RBARCHART", TextCompare: false) == 0 && LstGroup.Items.Count > 0 && LstY.Items.Count > 1)
						{
							text2 = "For R Barcharts, you can only specify a Grouping variable for the case of a single Y variable";
						}
						else if (Operators.CompareString(f_SpecialChart, "ROVERLAY", TextCompare: false) == 0 && LstGroup.Items.Count > 0 && LstY.Items.Count > 2)
						{
							text2 = "For R Overlap plots, you can only specify a Grouping variable for at most 2 Y variables";
						}
						else
						{
							int num7 = 0;
							int num8 = LstY.Items.Count - 1;
							for (num3 = 0; num3 <= num8; num3++)
							{
								if (Strings.InStr(Conversions.ToString(LstY.Items[num3]), " ->") != 0)
								{
									num7++;
								}
							}
							if (!((num7 == 0) | (num7 == LstY.Items.Count)))
							{
								text2 = "For R Barcharts, you must either configure a summary statistic for every Y variable or for no Y variables";
							}
							else if (Strings.InStr(Conversions.ToString(LstY.Items[0]), "->") != 0 && Chk1Image.Checked)
							{
								text2 = "For R Barcharts, you cannot apply column summaries and check the 1 image check box. Please adjust the query accordingly.";
							}
							else if (mnuDataLabels.Checked && LstBy.Items.Count > 0 && LstGroup.Items.Count > 0)
							{
								text2 = "You cannot assign data labels for Bar Charts where both By and Group variables are assigned.";
							}
							else
							{
								if (Strings.InStr(Conversions.ToString(LstY.Items[0]), "->") == 0 || !mnuDataLabels.Checked || LstGroup.Items.Count <= 0)
								{
									goto IL_07fa;
								}
								text2 = "You cannot assign data labels for Bar Charts where both a summary variable and a Group variables are assigned.";
							}
						}
						goto IL_241d;
						IL_0aa4:
						text2 = "You can only join columns which have the same datatype. Please modify your join conditions.";
						goto IL_241d;
						IL_07fa:
						if (Operators.CompareString(f_SpecialChart, "ROVERLAY", TextCompare: false) == 0 && LstY.Items.Count < 2)
						{
							text2 = "For R-Overlay plots, you must assign at least 2 Y variables. The last variable is assigned to the Y2 axis.";
							goto IL_241d;
						}
						if ((Operators.CompareString(MyTitleTabVisible, "N", TextCompare: false) != 0) & (Operators.CompareString(MyTitleTabVisible, "NO", TextCompare: false) != 0))
						{
							text11 = Title_Col_Case(Strings.Trim(TxtTitle.Text));
							if (LikeOperator.LikeString(text11, "$ERR$ - *", CompareMethod.Binary))
							{
								text2 = Strings.Mid(text11, 9);
								goto IL_241d;
							}
						}
						if (Operators.CompareString(f_CustomGroup, "", TextCompare: false) != 0 && (LstGroup.Items.Count == 0 || Operators.ConditionalCompareObjectNotEqual(LstGroup.Items[0], "my.group.col.wm", TextCompare: false)))
						{
							f_CustomGroup = "";
							l_Generalval2 = "";
						}
						if (Operators.CompareString(fSpecialSetup, "1", TextCompare: false) == 0)
						{
							if (Operators.CompareString(Text, "Text", TextCompare: false) == 0)
							{
								text4 = Strings.Replace(text4, "<TS>", "\" || mdyhms(today()) || \"", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "<LD>", "\" || Long Date( Today()) || \"", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "<SD>", "\" || Short Date( Today()) || \"", 1, -1, CompareMethod.Text);
							}
							BuildChart.Set_StdCtrl("TITLE-OTHER", text4);
						}
						else if (Operators.CompareString(fSpecialSetup, "2", TextCompare: false) == 0)
						{
							text8 = TxtPerRow.Text;
							BuildChart.Set_StdCtrl("TITLE-OTHER", text8);
						}
						if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
						{
							int num9 = f_WRows - 1;
							for (num3 = 0; num3 <= num9; num3++)
							{
								text8 = Conversions.ToString(GridWhere.Rows[num3].Cells[2].Value);
								text9 = Conversions.ToString(GridWhere.Rows[num3].Cells[5].Value);
								if (!((Operators.CompareString(text8, "", TextCompare: false) != 0) & (Operators.CompareString(text9, "' Then", TextCompare: false) != 0)))
								{
									continue;
								}
								BuildForm.Strip_Col_DT(ref text8, ref MyDT);
								BuildForm.Strip_Col_DT(ref text9, ref MyDT2);
								if (Operators.CompareString(Strings.UCase(MyDT), Strings.UCase(MyDT2), TextCompare: false) == 0)
								{
									continue;
								}
								goto IL_0aa4;
							}
						}
						text9 = BuildChart.Get_StdCtrl("X");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							text = "";
							text8 = "";
							if (LstX.Items.Count > 0)
							{
								int num10 = LstX.Items.Count - 1;
								for (num3 = 0; num3 <= num10; num3++)
								{
									text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, LstX.Items[num3]));
									text = ";";
								}
							}
							BuildChart.Set_StdCtrl("X", text8);
							MyDT = BuildChart.Get_StdCtrl("PSEUDO-X");
							if (Operators.CompareString(MyDT, "N/A", TextCompare: false) != 0)
							{
								text8 = Strings.Replace(text8, f_MyPre, "", 1, -1, CompareMethod.Text);
								text8 = Strings.Replace(text8, f_MyPost, "", 1, -1, CompareMethod.Text);
								string[] array = Strings.Split(text8, ";");
								text8 = "";
								int num11 = Information.UBound(array);
								for (num3 = 0; num3 <= num11; num3++)
								{
									MyDT2 = Strings.Trim(array[num3]);
									bool flag = false;
									int num12 = LstColumns.Items.Count - 1;
									for (int num4 = 0; num4 <= num12; num4++)
									{
										if (Conversions.ToBoolean(LikeOperator.LikeObject(LstColumns.Items[num4], MyDT2 + " (*)", CompareMethod.Binary)))
										{
											MyDT2 = f_MyPre + MyDT2 + f_MyPost;
											int num5 = Strings.InStrRev(Conversions.ToString(LstColumns.Items[num4]), " (");
											if (num5 != 0)
											{
												MyDT2 += Strings.Mid(Conversions.ToString(LstColumns.Items[num4]), num5);
											}
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										MyDT2 = f_MyPre + MyDT2 + f_MyPost;
									}
									text8 = text8 + ";" + MyDT2;
								}
								array = null;
								if (Operators.CompareString(text8, "", TextCompare: false) != 0)
								{
									text8 = Strings.Mid(text8, 2);
								}
								BuildChart.Set_StdCtrl("PSEUDO-X", text8);
							}
						}
						text9 = BuildChart.Get_StdCtrl("Y");
						text = "";
						text8 = "";
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							if (LstY.Items.Count > 0)
							{
								int num13 = LstY.Items.Count - 1;
								for (num3 = 0; num3 <= num13; num3++)
								{
									text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, LstY.Items[num3]));
									text = ";";
								}
							}
							BuildChart.Set_StdCtrl("Y", text8);
						}
						text9 = BuildChart.Get_StdCtrl("Y-DATA-LABELS");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							if (mnuDataLabels.Checked)
							{
								BuildChart.Set_StdCtrl("Y-DATA-LABELS", "1");
							}
							else
							{
								BuildChart.Set_StdCtrl("Y-DATA-LABELS", "0");
							}
						}
						text9 = BuildChart.Get_StdCtrl("KEEP-UNUSED-CHARTS");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							if (mnuKeepUnusedCharts.Checked)
							{
								BuildChart.Set_StdCtrl("KEEP-UNUSED-CHARTS", "1");
							}
							else
							{
								BuildChart.Set_StdCtrl("KEEP-UNUSED-CHARTS", "0");
							}
						}
						text9 = BuildChart.Get_StdCtrl("X-COMPUTED");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							if (Conversions.ToBoolean(Operators.CompareString(f_ComputedX, "", TextCompare: false) != 0 && (LstX.Items.Count != 1 || (LstX.Items.Count == 1 && Conversions.ToBoolean(Operators.NotObject(LikeOperator.LikeObject(NewLateBinding.LateGet(LstX.Items[0], null, "tolower", new object[0], null, null, null), "r.ce.*", CompareMethod.Binary)))))))
							{
								f_ComputedX = "";
							}
							BuildChart.Set_StdCtrl("X-COMPUTED", f_ComputedX);
						}
						text9 = BuildChart.Get_StdCtrl("GENERAL_VALUE");
						text10 = BuildChart.Get_StdCtrl("VALUE-OPT");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							text = "";
							text8 = "";
							if (LstVal.Items.Count > 0)
							{
								int num14 = LstVal.Items.Count - 1;
								for (num3 = 0; num3 <= num14; num3++)
								{
									text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, LstVal.Items[num3]));
									text = ";";
								}
							}
							BuildChart.Set_StdCtrl("GENERAL_VALUE", text8);
							BuildChart.Set_StdCtrl("GENERAL_VALUE2", text8);
						}
						text9 = BuildChart.Get_StdCtrl("CHART-THEME");
						text8 = "";
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							text8 = "";
							text = "";
							MyDT2 = "";
							int num15 = GridTheme.RowCount - 1;
							for (num3 = 0; num3 <= num15; num3++)
							{
								if (Operators.ConditionalCompareObjectNotEqual(GridTheme.Rows[num3].Cells[0].Value, "", TextCompare: false) || (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0 && (Operators.ConditionalCompareObjectNotEqual(GridTheme.Rows[num3].Cells[1].Value, "", TextCompare: false) || Operators.ConditionalCompareObjectNotEqual(GridTheme.Rows[num3].Cells[2].Value, "", TextCompare: false))))
								{
									if (Operators.CompareString(text8, "", TextCompare: false) != 0)
									{
										text8 += "</tr>";
									}
									int num16 = GridTheme.ColumnCount - 1;
									for (int num4 = 0; num4 <= num16; num4++)
									{
										text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, GridTheme.Rows[num3].Cells[num4].Value));
										text = ";";
									}
								}
							}
							if (Operators.CompareString(text8, "", TextCompare: false) != 0)
							{
								text8 = Strings.Replace(text8, "</tr>;", "</tr>", 1, -1, CompareMethod.Text);
							}
							BuildChart.Set_StdCtrl("CHART-THEME", text8);
							string text12 = "";
							if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
							{
								Set_Y_Grid_Val(ref text12, 0, "COL=", "", AddQ: true);
								Set_Y_Grid_Val(ref text12, 1, "SYM=", "", AddQ: true);
								Set_Y_Grid_Val(ref text12, 2, "LTY=", "", AddQ: true);
							}
							else
							{
								Set_Y_Grid_Val(ref text12, 0, "col=c(", ")", AddQ: true);
								if (Operators.CompareString(f_SpecialChart, "RWAFERD", TextCompare: false) != 0)
								{
									Set_Y_Grid_Val(ref text12, 1, "pch=c(", ")", AddQ: false, AColon: true);
									Set_Y_Grid_Val(ref text12, 2, "lty=c(", ")", AddQ: true);
									Set_Y_Grid_Val(ref text12, 3, "lwd=c(", ")");
									Set_Y_Grid_Val(ref text12, 4, "fill=c(", ")", AddQ: true);
								}
							}
							BuildChart.Set_StdCtrl("MODE", text12);
						}
						text = "";
						text8 = "";
						if (LstBy.Items.Count > 0)
						{
							int num17 = LstBy.Items.Count - 1;
							for (num3 = 0; num3 <= num17; num3++)
							{
								text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, LstBy.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("BY", text8);
						if (flag2)
						{
							BuildChart.Set_StdCtrl("R-BY-2", text8);
						}
						text = "";
						text8 = "";
						if (LstGroup.Items.Count > 0)
						{
							int num18 = LstGroup.Items.Count - 1;
							for (num3 = 0; num3 <= num18; num3++)
							{
								text8 = Conversions.ToString(Operators.ConcatenateObject(text8 + text, LstGroup.Items[num3]));
								text = ";";
							}
						}
						BuildChart.Set_StdCtrl("GROUP-BY", text8);
						BuildChart.Set_StdCtrl("R-GROUP-BY", text8);
						if (Operators.CompareString(l_Generalval2, "", TextCompare: false) != 0 && Operators.CompareString(f_CustomGroup, "", TextCompare: false) != 0)
						{
							BuildChart.Set_StdCtrl("CUSTOM-GROUP", l_Generalval2 + "~~~" + f_CustomGroup);
						}
						else
						{
							BuildChart.Set_StdCtrl("CUSTOM-GROUP", "");
						}
						text9 = BuildChart.Get_StdCtrl("CHART-ITEMS-PER-ROW");
						if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
						{
							text8 = TxtPerRow.Text;
							if ((Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0 || Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) && Chk1Image.Checked && LstY.Items.Count > 1)
							{
								if (Operators.CompareString(Strings.Trim(text8), "-1", TextCompare: false) != 0)
								{
									Interaction.MsgBox("For R plots with a single image, the Plots per Row setting is ignored for multiple y variables", MsgBoxStyle.Information, "Plots");
								}
								BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", "-1");
							}
							else if ((Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0 || Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) && Chk1Image.Checked && Operators.CompareString(Strings.Trim(text8), "-1", TextCompare: false) == 0)
							{
								BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", "-1");
							}
							else if (Versioned.IsNumeric(text8) && Conversions.ToInteger(text8) >= 1 && Conversions.ToInteger(text8) <= 9)
							{
								BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", text8);
							}
							else
							{
								BuildChart.Set_StdCtrl("CHART-ITEMS-PER-ROW", "1");
							}
						}
						else
						{
							text9 = BuildChart.Get_StdCtrl("PSEUDO-COL");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = TxtPerRow.Text;
								BuildChart.Set_StdCtrl("PSEUDO-COL", text8);
							}
						}
						if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) != 0)
						{
							Get_Set_Where("G", ref text8);
							BuildChart.Set_StdCtrl("CHART-WHERE", text8);
						}
						if ((Operators.CompareString(MyTitleTabVisible, "N", TextCompare: false) != 0) & (Operators.CompareString(MyTitleTabVisible, "NO", TextCompare: false) != 0))
						{
							BuildChart.Set_StdCtrl("TITLE", text11);
							BuildChart.Set_StdCtrl("X-AXIS", Strings.Trim(TxtXAxis.Text));
							BuildChart.Set_StdCtrl("Y-AXIS", Strings.Trim(TxtYAxis.Text));
							BuildChart.Set_StdCtrl("Y-AXIS-2", Strings.Trim(TxtY2Axis.Text));
							BuildChart.Set_StdCtrl("Y-MAX", Strings.Trim(TxtHiY.Text));
							BuildChart.Set_StdCtrl("Y-MIN", Strings.Trim(TxtLOY.Text));
							BuildChart.Set_StdCtrl("Y-2-MAX", Strings.Trim(TxtHiY2.Text));
							BuildChart.Set_StdCtrl("Y-2-MIN", Strings.Trim(TxtLoY2.Text));
							BuildChart.Set_StdCtrl("X-MAX", Strings.Trim(TxtHiX.Text));
							BuildChart.Set_StdCtrl("X-MIN", Strings.Trim(TxtLoX.Text));
							BuildChart.Set_StdCtrl("Y-INC", Strings.Trim(TxtIncY.Text));
							BuildChart.Set_StdCtrl("Y2-INC", Strings.Trim(TxtIncY2.Text));
							BuildChart.Set_StdCtrl("X-INC", Strings.Trim(TxtIncX.Text));
							BuildChart.Set_StdCtrl("LOG-X", Strings.Trim(cmbXType.Text));
							BuildChart.Set_StdCtrl("LOG-Y", Strings.Trim(cmbYType.Text));
							BuildChart.Set_StdCtrl("LOG-Y2", Strings.Trim(cmbY2Type.Text));
							BuildChart.Set_StdCtrl("X-AXIS-ROTATE", Strings.Trim(cmbXRot.Text));
							BuildChart.Set_StdCtrl("Y-AXIS-ROTATE", Strings.Trim(cmbYRot.Text));
							BuildChart.Set_StdCtrl("Y2-AXIS-ROTATE", Strings.Trim(cmbY2Rot.Text));
							if (flag2)
							{
								text8 = "";
								CheckBox MyCK = Chk1Image;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								Chk1Image = MyCK;
								BuildChart.Set_StdCtrl("USE-R-CONDITION", text8);
							}
							if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0 && Chk1Image.Checked)
							{
								BuildChart.Set_StdCtrl("X-HIGH", "PNG");
							}
							else
							{
								BuildChart.Set_StdCtrl("X-HIGH", "HTM");
							}
							text9 = BuildChart.Get_StdCtrl("X-MAJOR-GRID");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = X_Major_Grid;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								X_Major_Grid = MyCK;
								BuildChart.Set_StdCtrl("X-MAJOR-GRID", text8);
							}
							text9 = BuildChart.Get_StdCtrl("X-MINOR-GRID");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = X_Minor_Grid;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								X_Minor_Grid = MyCK;
								BuildChart.Set_StdCtrl("X-MINOR-GRID", text8);
							}
							text9 = BuildChart.Get_StdCtrl("Y-MAJOR-GRID");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = Y_Major_Grid;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								Y_Major_Grid = MyCK;
								BuildChart.Set_StdCtrl("Y-MAJOR-GRID", text8);
							}
							text9 = BuildChart.Get_StdCtrl("Y-MINOR-GRID");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = Y_Minor_Grid;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								Y_Minor_Grid = MyCK;
								BuildChart.Set_StdCtrl("Y-MINOR-GRID", text8);
							}
							text9 = BuildChart.Get_StdCtrl("X-MAJOR-TICKS");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = X_Major_Ticks;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								X_Major_Ticks = MyCK;
								BuildChart.Set_StdCtrl("X-MAJOR-TICKS", text8);
							}
							text9 = BuildChart.Get_StdCtrl("X-MINOR-TICKS");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = X_Minor_Ticks;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								X_Minor_Ticks = MyCK;
								BuildChart.Set_StdCtrl("X-MINOR-TICKS", text8);
							}
							text9 = BuildChart.Get_StdCtrl("Y-MAJOR-TICKS");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = Y_Major_Ticks;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								Y_Major_Ticks = MyCK;
								BuildChart.Set_StdCtrl("Y-MAJOR-TICKS", text8);
							}
							text9 = BuildChart.Get_StdCtrl("Y-MINOR-TICKS");
							if (Operators.CompareString(text9, "N/A", TextCompare: false) != 0)
							{
								text8 = "";
								CheckBox MyCK = Y_Minor_Ticks;
								BuildChart.Set_CK_Box(ref MyCK, ref text8);
								Y_Minor_Ticks = MyCK;
								BuildChart.Set_StdCtrl("Y-MINOR-TICKS", text8);
							}
							if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) | (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0))
							{
								text9 = BuildChart.Get_StdCtrl("LEGENDBORDER");
								text9 = BuildChart.Get_StdCtrl("LEGENDTITLE");
								BuildChart.Set_StdCtrl("LEGENDTITLE", Strings.Trim(TxtLTitle.Text));
								text9 = BuildChart.Get_StdCtrl("LEGENDPOS");
								BuildChart.Set_StdCtrl("LEGENDPOS", Strings.Trim(CmbLPos.Text));
								text9 = BuildChart.Get_StdCtrl("LEGENDSIZE");
								BuildChart.Set_StdCtrl("LEGENDSIZE", Strings.Trim(CmbLSize.Text));
								text9 = BuildChart.Get_StdCtrl("LEGENDBG");
								BuildChart.Set_StdCtrl("LEGENDBG", Strings.Trim(CmbLBG.Text));
								text9 = BuildChart.Get_StdCtrl("LEGENDCOLS");
								BuildChart.Set_StdCtrl("LEGENDCOLS", Strings.Trim(CmbLCols.Text));
								text9 = BuildChart.Get_StdCtrl("LEGENDKSIZE");
								BuildChart.Set_StdCtrl("LEGENDKSIZE", Strings.Trim(CmbLKSize.Text));
							}
							BuildChart.Set_StdCtrl("ROW-LEGEND", "");
							MyDT = "";
							if (Operators.CompareString(RLCol1.Text, "", TextCompare: false) != 0 && Operators.CompareString(RLCol1.Text, "N/A", TextCompare: false) != 0)
							{
								MyDT = RLCol1.Text;
								text8 = ((!RLColor1.Checked) ? "0" : "1");
								MyDT = MyDT + ";" + text8;
								text8 = "";
								text8 = ((!RLMarker1.Checked) ? "0" : "1");
								MyDT = MyDT + ";" + text8;
							}
							if (Operators.CompareString(RLCol2.Text, "", TextCompare: false) != 0 && Operators.CompareString(RLCol2.Text, "N/A", TextCompare: false) != 0)
							{
								if (Operators.CompareString(MyDT, "", TextCompare: false) == 0)
								{
									MyDT = ";;";
								}
								MyDT = MyDT + ";" + RLCol2.Text;
								text8 = ((!RLColor2.Checked) ? "0" : "1");
								MyDT = MyDT + ";" + text8;
								text8 = "";
								text8 = ((!RLMarker2.Checked) ? "0" : "1");
								MyDT = MyDT + ";" + text8;
							}
							BuildChart.Set_StdCtrl("ROW-LEGEND", MyDT);
							BuildChart.Set_StdCtrl("X-AXIS-REF-LINE", "");
							text8 = "";
							TextBox TxtVal = XRLineVal1;
							ComboBox CmbStyle = XRlineStyle1;
							ComboBox CmbColor = XRlineColor1;
							TextBox TxtDesc = XRlineTxt1;
							Set_Ref_Line(ref text8, ref TxtVal, ref CmbStyle, ref CmbColor, ref TxtDesc);
							XRlineTxt1 = TxtDesc;
							XRlineColor1 = CmbColor;
							XRlineStyle1 = CmbStyle;
							XRLineVal1 = TxtVal;
							TxtDesc = XRLineVal2;
							CmbColor = XRlineStyle2;
							CmbStyle = XRlineColor2;
							TxtVal = XRlineTxt2;
							Set_Ref_Line(ref text8, ref TxtDesc, ref CmbColor, ref CmbStyle, ref TxtVal);
							XRlineTxt2 = TxtVal;
							XRlineColor2 = CmbStyle;
							XRlineStyle2 = CmbColor;
							XRLineVal2 = TxtDesc;
							TxtVal = XRLineVal3;
							CmbStyle = XRlineStyle3;
							CmbColor = XRlineColor3;
							TxtDesc = XRlineTxt3;
							Set_Ref_Line(ref text8, ref TxtVal, ref CmbStyle, ref CmbColor, ref TxtDesc);
							XRlineTxt3 = TxtDesc;
							XRlineColor3 = CmbColor;
							XRlineStyle3 = CmbStyle;
							XRLineVal3 = TxtVal;
							TxtDesc = XRLineVal4;
							CmbColor = XRlineStyle4;
							CmbStyle = XRlineColor4;
							TxtVal = XRlineTxt4;
							Set_Ref_Line(ref text8, ref TxtDesc, ref CmbColor, ref CmbStyle, ref TxtVal);
							XRlineTxt4 = TxtVal;
							XRlineColor4 = CmbStyle;
							XRlineStyle4 = CmbColor;
							XRLineVal4 = TxtDesc;
							text8 = Strings.Mid(text8, 2);
							BuildChart.Set_StdCtrl("X-AXIS-REF-LINE", text8);
							BuildChart.Set_StdCtrl("Y-AXIS-REF-LINE", "");
							text8 = "";
							TxtVal = YRLineVal1;
							CmbStyle = YRlineStyle1;
							CmbColor = YRlineColor1;
							TxtDesc = YRlineTxt1;
							Set_Ref_Line(ref text8, ref TxtVal, ref CmbStyle, ref CmbColor, ref TxtDesc);
							YRlineTxt1 = TxtDesc;
							YRlineColor1 = CmbColor;
							YRlineStyle1 = CmbStyle;
							YRLineVal1 = TxtVal;
							TxtDesc = YRLineVal2;
							CmbColor = YRlineStyle2;
							CmbStyle = YRlineColor2;
							TxtVal = YRlineTxt2;
							Set_Ref_Line(ref text8, ref TxtDesc, ref CmbColor, ref CmbStyle, ref TxtVal);
							YRlineTxt2 = TxtVal;
							YRlineColor2 = CmbStyle;
							YRlineStyle2 = CmbColor;
							YRLineVal2 = TxtDesc;
							TxtVal = YRLineVal3;
							CmbStyle = YRlineStyle3;
							CmbColor = YRlineColor3;
							TxtDesc = YRlineTxt3;
							Set_Ref_Line(ref text8, ref TxtVal, ref CmbStyle, ref CmbColor, ref TxtDesc);
							YRlineTxt3 = TxtDesc;
							YRlineColor3 = CmbColor;
							YRlineStyle3 = CmbStyle;
							YRLineVal3 = TxtVal;
							TxtDesc = YRLineVal4;
							CmbColor = YRlineStyle4;
							CmbStyle = YRlineColor4;
							TxtVal = YRlineTxt4;
							Set_Ref_Line(ref text8, ref TxtDesc, ref CmbColor, ref CmbStyle, ref TxtVal);
							YRlineTxt4 = TxtVal;
							YRlineColor4 = CmbStyle;
							YRlineStyle4 = CmbColor;
							YRLineVal4 = TxtDesc;
							text8 = Strings.Mid(text8, 2);
							BuildChart.Set_StdCtrl("Y-AXIS-REF-LINE", text8);
							if ((Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) | (Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0))
							{
								text8 = YRTxtAdj1.Text;
								BuildChart.Set_StdCtrl("Y-TXT-ADJ-1", text8);
							}
							BuildChart.Set_StdCtrl("FRAME-X", FrameX.Text);
							BuildChart.Set_StdCtrl("FRAME-Y", FrameY.Text);
							if (Operators.CompareString(f_TemplateType2, "JMP", TextCompare: false) == 0)
							{
								BuildChart.Set_StdCtrl("FRAME-COLOR", FrameColor.Text);
							}
							else
							{
								BuildChart.Set_StdCtrl("FRAME-COLOR2", FrameColor.Text);
							}
							BuildChart.Set_StdCtrl("FRAME-MARKER", FrameMarker.Text);
						}
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
						LastChartTable = TxtInFile.Text;
						f_OK = true;
						Close();
						goto end_IL_0001;
					}
					goto IL_2495;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 9311;
				continue;
			}
			break;
			IL_2495:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmChart_FormClosed(object sender, FormClosedEventArgs e)
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
				MyTableArr = null;
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
				case 878:
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
							goto IL_0181;
						case 30:
							goto IL_01a1;
						case 31:
							goto IL_01a7;
						case 32:
							goto IL_01b6;
						case 28:
						case 29:
						case 33:
							goto IL_01dc;
						case 34:
							goto IL_01f8;
						case 35:
							goto IL_0206;
						case 38:
							goto IL_0226;
						case 39:
							goto IL_022c;
						case 40:
							goto IL_023b;
						case 36:
						case 37:
						case 41:
							goto IL_0261;
						case 42:
							goto IL_027d;
						case 43:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 44:
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
					LstY.Items.RemoveAt(LstY.SelectedIndices[0]);
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
					if (LstY.SelectedIndices.Count > 0)
					{
						goto IL_0097;
					}
					goto IL_00ee;
					IL_00ee:
					num2 = 18;
					if (num5 == 1)
					{
						goto IL_00fc;
					}
					goto IL_0157;
					IL_00fc:
					num2 = 19;
					LstAny = LstY;
					Set_Index_List(ref LstAny, myIdx);
					LstY = LstAny;
					goto IL_0157;
					IL_0157:
					num2 = 21;
					if (LstBy.SelectedIndices.Count > 0)
					{
						goto IL_011c;
					}
					goto IL_0173;
					IL_0173:
					num2 = 26;
					if (num5 == 2)
					{
						goto IL_0181;
					}
					goto IL_01dc;
					IL_0181:
					num2 = 27;
					LstAny = LstBy;
					Set_Index_List(ref LstAny, myIdx);
					LstBy = LstAny;
					goto IL_01dc;
					IL_01dc:
					num2 = 29;
					if (LstGroup.SelectedIndices.Count > 0)
					{
						goto IL_01a1;
					}
					goto IL_01f8;
					IL_01f8:
					num2 = 34;
					if (num5 == 3)
					{
						goto IL_0206;
					}
					goto IL_0261;
					IL_0206:
					num2 = 35;
					LstAny = LstGroup;
					Set_Index_List(ref LstAny, myIdx);
					LstGroup = LstAny;
					goto IL_0261;
					IL_0261:
					num2 = 37;
					if (LstVal.SelectedIndices.Count > 0)
					{
						goto IL_0226;
					}
					goto IL_027d;
					IL_027d:
					num2 = 42;
					if (num5 != 4)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0226:
					num2 = 38;
					num5 = 4;
					goto IL_022c;
					IL_022c:
					num2 = 39;
					myIdx = LstVal.SelectedIndex;
					goto IL_023b;
					IL_023b:
					num2 = 40;
					LstVal.Items.RemoveAt(LstVal.SelectedIndices[0]);
					goto IL_0261;
					IL_01a1:
					num2 = 30;
					num5 = 3;
					goto IL_01a7;
					IL_01a7:
					num2 = 31;
					myIdx = LstGroup.SelectedIndex;
					goto IL_01b6;
					IL_01b6:
					num2 = 32;
					LstGroup.Items.RemoveAt(LstGroup.SelectedIndices[0]);
					goto IL_01dc;
					IL_011c:
					num2 = 22;
					num5 = 2;
					goto IL_0122;
					IL_0122:
					num2 = 23;
					myIdx = LstBy.SelectedIndex;
					goto IL_0131;
					IL_0131:
					num2 = 24;
					LstBy.Items.RemoveAt(LstBy.SelectedIndices[0]);
					goto IL_0157;
					IL_0097:
					num2 = 14;
					num5 = 1;
					goto IL_009d;
					IL_009d:
					num2 = 15;
					myIdx = LstY.SelectedIndex;
					goto IL_00ac;
					end_IL_0001_2:
					break;
				}
				num2 = 43;
				LstAny = LstVal;
				Set_Index_List(ref LstAny, myIdx);
				LstVal = LstAny;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 878;
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
		int num5 = default(int);
		string MyCol = default(string);
		string text = default(string);
		string Is_Alli_ = default(string);
		string MyDT = default(string);
		int num7 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		int num9 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					bool num6;
					bool num8;
					ListBox DestList;
					bool num10;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1070:
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
								goto IL_0023;
							case 7:
								goto IL_0035;
							case 8:
								goto IL_003e;
							case 9:
								goto IL_0047;
							case 10:
								goto IL_0051;
							case 11:
								goto IL_005b;
							case 12:
								goto IL_0066;
							case 13:
								goto IL_0071;
							case 14:
								goto IL_0090;
							case 15:
								goto IL_009b;
							case 16:
								goto IL_00a5;
							case 19:
								goto IL_00d1;
							case 20:
								goto IL_0103;
							case 18:
							case 22:
							case 23:
								goto IL_0133;
							case 24:
								goto IL_0150;
							case 25:
								goto IL_015b;
							case 26:
							case 27:
								goto IL_0168;
							case 28:
								goto IL_0186;
							case 29:
								goto IL_01b1;
							case 30:
								goto IL_01be;
							case 31:
								goto IL_01d3;
							case 32:
								goto IL_01e5;
							case 33:
								goto IL_01fe;
							case 35:
								goto IL_0212;
							case 34:
							case 36:
							case 37:
								goto IL_021a;
							case 38:
								goto IL_0249;
							case 39:
								goto IL_025f;
							case 40:
								goto IL_028d;
							case 42:
								goto IL_02b1;
							case 43:
								goto IL_02e9;
							case 44:
								goto IL_0317;
							case 41:
							case 45:
							case 46:
							case 47:
								goto IL_0337;
							case 48:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 6:
							case 17:
							case 21:
							case 49:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0337:
						num2 = 47;
						num5++;
						goto IL_033e;
						IL_02e9:
						num2 = 43;
						DestList = LstY;
						num6 = Check_Same_Var(ref DestList, MyCol + text);
						LstY = DestList;
						if (!num6)
						{
							goto IL_0317;
						}
						goto IL_0337;
						IL_02b1:
						num2 = 42;
						if (Operators.CompareString(Is_Alli_, "", TextCompare: false) != 0 || DataType_Check(MyDT, Conversions.ToString(CmdY.Tag), "Y"))
						{
							goto IL_02e9;
						}
						goto IL_0337;
						IL_0317:
						num2 = 44;
						LstY.Items.Add(MyCol + text);
						goto IL_0337;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num7 = -1;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						num7 = LstColumns.SelectedIndex;
						goto IL_0023;
						IL_0023:
						num2 = 5;
						if (num7 == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_0035;
						IL_0035:
						num2 = 7;
						MyCol = "";
						goto IL_003e;
						IL_003e:
						num2 = 8;
						text2 = "";
						goto IL_0047;
						IL_0047:
						num2 = 9;
						MyDT = "";
						goto IL_0051;
						IL_0051:
						num2 = 10;
						text = "";
						goto IL_005b;
						IL_005b:
						num2 = 11;
						text3 = f_MyPre;
						goto IL_0066;
						IL_0066:
						num2 = 12;
						text4 = f_MyPost;
						goto IL_0071;
						IL_0071:
						num2 = 13;
						if (Operators.ConditionalCompareObjectEqual(cmdExtra.Tag, "Y", TextCompare: false))
						{
							goto IL_0090;
						}
						goto IL_009b;
						IL_0090:
						num2 = 14;
						text = f_ExtraData;
						goto IL_009b;
						IL_009b:
						num2 = 15;
						Is_Alli_ = "";
						goto IL_00a5;
						IL_00a5:
						num2 = 16;
						DestList = LstY;
						num8 = Check_Alli_(ref DestList, ref Is_Alli_);
						LstY = DestList;
						if (num8)
						{
							goto end_IL_0001_3;
						}
						goto IL_00d1;
						IL_00d1:
						num2 = 19;
						if (LstColumns.SelectedIndices.Count + LstY.Items.Count > fYMax)
						{
							goto IL_0103;
						}
						goto IL_0133;
						IL_0103:
						num2 = 20;
						Interaction.MsgBox("You have exceeded the maximum allowable number of Y variables (" + Conversions.ToString(fYMax) + ").", MsgBoxStyle.Exclamation, "Exceeded Max");
						goto end_IL_0001_3;
						IL_0133:
						num2 = 23;
						if (Operators.CompareString(fYBound, "", TextCompare: false) != 0)
						{
							goto IL_0150;
						}
						goto IL_0168;
						IL_0150:
						num2 = 24;
						text3 = fYBound;
						goto IL_015b;
						IL_015b:
						num2 = 25;
						text4 = fYBound;
						goto IL_0168;
						IL_0168:
						num2 = 27;
						num9 = LstColumns.SelectedIndices.Count - 1;
						num5 = 0;
						goto IL_033e;
						IL_033e:
						if (num5 > num9)
						{
							break;
						}
						goto IL_0186;
						IL_0186:
						num2 = 28;
						MyCol = Conversions.ToString(LstColumns.Items[LstColumns.SelectedIndices[num5]]);
						goto IL_01b1;
						IL_01b1:
						num2 = 29;
						BuildForm.Strip_Col_DT(ref MyCol, ref MyDT);
						goto IL_01be;
						IL_01be:
						num2 = 30;
						text2 = Strings.Trim(CmbY.Text);
						goto IL_01d3;
						IL_01d3:
						num2 = 31;
						text2 = BuildChart.Map_In_Out(f_Y_Map, text2);
						goto IL_01e5;
						IL_01e5:
						num2 = 32;
						if (Operators.CompareString(Is_Alli_, "", TextCompare: false) == 0)
						{
							goto IL_01fe;
						}
						goto IL_0212;
						IL_01fe:
						num2 = 33;
						MyCol = text3 + MyCol + text4;
						goto IL_021a;
						IL_0212:
						num2 = 35;
						MyCol = Is_Alli_;
						goto IL_021a;
						IL_021a:
						num2 = 37;
						if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text2), "DATA", TextCompare: false) != 0))
						{
							goto IL_0249;
						}
						goto IL_02b1;
						IL_0249:
						num2 = 38;
						MyCol = Strings.Replace(text2, "<>", MyCol, 1, -1, CompareMethod.Text);
						goto IL_025f;
						IL_025f:
						num2 = 39;
						DestList = LstY;
						num10 = Check_Same_Var(ref DestList, MyCol + text);
						LstY = DestList;
						if (!num10)
						{
							goto IL_028d;
						}
						goto IL_0337;
						IL_028d:
						num2 = 40;
						LstY.Items.Add(MyCol + text);
						goto IL_0337;
						end_IL_0001_2:
						break;
					}
					num2 = 48;
					LstColumns.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1070;
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

	private bool Check_Alli_(ref ListBox DestList, ref string Is_Alli_)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		string text2 = default(string);
		int num7 = default(int);
		bool flag = default(bool);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Type typeFromHandle;
					object[] obj;
					ListBox.ObjectCollection items;
					bool[] obj2;
					object obj3;
					Type typeFromHandle2;
					object[] obj4;
					ListBox lstColumns;
					bool[] obj5;
					object obj6;
					Type typeFromHandle3;
					object[] obj7;
					ListBox.SelectedObjectCollection selectedItems;
					int index;
					object[] array;
					bool[] obj8;
					bool[] array2;
					object obj9;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1803:
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
								goto IL_0019;
							case 5:
								goto IL_0022;
							case 6:
								goto IL_0027;
							case 7:
								goto IL_002b;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_0048;
							case 10:
								goto IL_0059;
							case 11:
								goto IL_00c4;
							case 12:
								goto IL_00de;
							case 13:
								goto IL_0111;
							case 14:
								goto IL_014f;
							case 15:
								goto IL_019a;
							case 16:
								goto IL_01af;
							case 18:
							case 19:
								goto IL_01bb;
							case 20:
								goto IL_01f5;
							case 21:
								goto IL_020e;
							case 23:
								goto IL_0220;
							case 28:
								goto IL_0231;
							case 29:
								goto IL_0255;
							case 32:
								goto IL_0273;
							case 33:
								goto IL_0284;
							case 34:
								goto IL_02f6;
							case 35:
								goto IL_0326;
							case 36:
								goto IL_0361;
							case 40:
								goto IL_036e;
							case 41:
								goto IL_0392;
							case 38:
							case 39:
							case 43:
							case 44:
							case 45:
								goto IL_039c;
							case 22:
							case 25:
							case 26:
							case 27:
							case 30:
							case 31:
							case 37:
							case 42:
							case 46:
							case 47:
								goto IL_03af;
							case 48:
								goto IL_03bf;
							case 49:
								goto IL_03d8;
							case 50:
								goto IL_03f0;
							case 52:
								goto IL_03fc;
							case 53:
								goto IL_0417;
							case 54:
								goto IL_0486;
							case 56:
								goto IL_04d0;
							case 57:
								goto IL_050b;
							case 61:
								goto IL_0517;
							case 62:
								goto IL_053b;
							case 55:
							case 59:
							case 60:
							case 64:
							case 65:
							case 66:
								goto IL_0547;
							case 51:
							case 58:
							case 63:
							case 67:
							case 68:
							case 69:
								goto IL_055c;
							case 70:
								goto IL_0566;
							case 71:
								goto IL_0595;
							case 73:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 17:
							case 24:
							case 72:
							case 74:
							case 75:
							case 76:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0417:
						num2 = 53;
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
						text = Conversions.ToString(obj3);
						goto IL_0486;
						IL_039c:
						num2 = 45;
						num5++;
						goto IL_03a5;
						IL_0550:
						if (num5 <= num6)
						{
							goto IL_0417;
						}
						goto IL_055c;
						IL_0595:
						num2 = 71;
						Interaction.MsgBox("Column patterns cannot be mixed with regular columns", MsgBoxStyle.Exclamation, "Patterns");
						goto end_IL_0001_3;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						text = "";
						goto IL_0019;
						IL_0019:
						num2 = 4;
						text2 = "";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						num7 = 0;
						goto IL_0027;
						IL_0027:
						num2 = 6;
						flag = false;
						goto IL_002b;
						IL_002b:
						num2 = 7;
						Is_Alli_ = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						num7 = LstColumns.SelectedItems.Count;
						goto IL_0048;
						IL_0048:
						num2 = 9;
						if (num7 == 1)
						{
							goto IL_0059;
						}
						goto IL_0273;
						IL_0059:
						num2 = 10;
						typeFromHandle2 = typeof(Strings);
						obj4 = new object[1] { (lstColumns = LstColumns).SelectedItem };
						array = obj4;
						obj5 = new bool[1] { true };
						array2 = obj5;
						obj6 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj4, null, null, obj5);
						if (array2[0])
						{
							lstColumns.SelectedItem = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						text = Strings.Trim(Conversions.ToString(obj6));
						goto IL_00c4;
						IL_0517:
						num2 = 61;
						if (LikeOperator.LikeString(text, "*(::_ALL*1_)*", CompareMethod.Binary) | LikeOperator.LikeString(text, "*(::_ALL*2_)*", CompareMethod.Binary))
						{
							goto IL_053b;
						}
						goto IL_0547;
						IL_0486:
						num2 = 54;
						switch (f_TemplateType2)
						{
						case "R":
						case "HTML":
						case "HTMLPY":
							break;
						case "JMP":
							goto IL_0517;
						default:
							goto IL_0547;
						}
						goto IL_04d0;
						IL_00c4:
						num2 = 11;
						text2 = Strings.Trim(Conversions.ToString(LstColumns.SelectedItem));
						goto IL_00de;
						IL_00de:
						num2 = 12;
						if (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0 || LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
						{
							goto IL_0111;
						}
						goto IL_0231;
						IL_053b:
						num2 = 62;
						flag = true;
						goto IL_055c;
						IL_0231:
						num2 = 28;
						if (LikeOperator.LikeString(text, "::_ALL*1_", CompareMethod.Binary) | LikeOperator.LikeString(text, "::_ALL*2_", CompareMethod.Binary))
						{
							goto IL_0255;
						}
						goto IL_03af;
						IL_0255:
						num2 = 29;
						Is_Alli_ = "Eval(" + text + ")";
						goto IL_03af;
						IL_0111:
						num2 = 13;
						if ((Operators.CompareString(text, "STARTINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "ENDINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "CONTAINING:", TextCompare: false) == 0))
						{
							goto IL_014f;
						}
						goto IL_03af;
						IL_014f:
						num2 = 14;
						if ((Operators.CompareString(f_SpecialChart, "RBARCHART", TextCompare: false) == 0 || Operators.CompareString(f_SpecialChart, "ROVERLAY", TextCompare: false) == 0) && Operators.CompareString(CmbY.Text, "Data", TextCompare: false) != 0)
						{
							goto IL_019a;
						}
						goto IL_01bb;
						IL_050b:
						num2 = 57;
						flag = true;
						goto IL_055c;
						IL_04d0:
						num2 = 56;
						if ((Operators.CompareString(text, "STARTINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "ENDINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "CONTAINING:", TextCompare: false) == 0))
						{
							goto IL_050b;
						}
						goto IL_0547;
						IL_019a:
						num2 = 15;
						Interaction.MsgBox("BarCharts only support StartingWith:, EndingWith: and Containing: Operators for non summary expressions", MsgBoxStyle.Information, "Not Supported");
						goto IL_01af;
						IL_01af:
						num2 = 16;
						flag = true;
						goto end_IL_0001_3;
						IL_01bb:
						num2 = 19;
						Is_Alli_ = Interaction.InputBox("Select all Columns " + Strings.Trim(Conversions.ToString(LstColumns.SelectedItem)) + " the following pattern...");
						goto IL_01f5;
						IL_01f5:
						num2 = 20;
						if (Operators.CompareString(Is_Alli_, "", TextCompare: false) != 0)
						{
							goto IL_020e;
						}
						goto IL_0220;
						IL_020e:
						num2 = 21;
						Is_Alli_ = text2 + Is_Alli_;
						goto IL_03af;
						IL_0220:
						num2 = 23;
						flag = true;
						goto end_IL_0001_3;
						IL_0273:
						num2 = 32;
						num8 = num7 - 1;
						num5 = 0;
						goto IL_03a5;
						IL_03a5:
						if (num5 <= num8)
						{
							goto IL_0284;
						}
						goto IL_03af;
						IL_0284:
						num2 = 33;
						typeFromHandle3 = typeof(Strings);
						obj7 = new object[1] { (selectedItems = LstColumns.SelectedItems)[index = num5] };
						array = obj7;
						obj8 = new bool[1] { true };
						array2 = obj8;
						obj9 = NewLateBinding.LateGet(null, typeFromHandle3, "UCase", obj7, null, null, obj8);
						if (array2[0])
						{
							selectedItems[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						text = Conversions.ToString(obj9);
						goto IL_02f6;
						IL_055c:
						num2 = 69;
						if (!flag)
						{
							goto end_IL_0001_3;
						}
						goto IL_0566;
						IL_0547:
						num2 = 66;
						num5++;
						goto IL_0550;
						IL_02f6:
						num2 = 34;
						if (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0 || LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
						{
							goto IL_0326;
						}
						goto IL_036e;
						IL_0566:
						num2 = 70;
						if (!((Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) | LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary)))
						{
							break;
						}
						goto IL_0595;
						IL_036e:
						num2 = 40;
						if (LikeOperator.LikeString(text, "::_ALL*1_", CompareMethod.Binary) | LikeOperator.LikeString(text, "::_ALL*2_", CompareMethod.Binary))
						{
							goto IL_0392;
						}
						goto IL_039c;
						IL_0392:
						num2 = 41;
						flag = true;
						goto IL_03af;
						IL_0326:
						num2 = 35;
						if ((Operators.CompareString(text, "STARTINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "ENDINGWITH:", TextCompare: false) == 0) | (Operators.CompareString(text, "CONTAINING:", TextCompare: false) == 0))
						{
							goto IL_0361;
						}
						goto IL_039c;
						IL_0361:
						num2 = 36;
						flag = true;
						goto IL_03af;
						IL_03af:
						num2 = 47;
						if (!flag)
						{
							goto IL_03bf;
						}
						goto IL_055c;
						IL_03bf:
						num2 = 48;
						if (Operators.CompareString(Is_Alli_, "", TextCompare: false) != 0)
						{
							goto IL_03d8;
						}
						goto IL_03fc;
						IL_03d8:
						num2 = 49;
						if (DestList.Items.Count > 0)
						{
							goto IL_03f0;
						}
						goto IL_055c;
						IL_03f0:
						num2 = 50;
						flag = true;
						goto IL_055c;
						IL_03fc:
						num2 = 52;
						num6 = DestList.Items.Count - 1;
						num5 = 0;
						goto IL_0550;
						end_IL_0001_2:
						break;
					}
					num2 = 73;
					Interaction.MsgBox("_Alli_ or _AllColi_ columns cannot be mixed with regular columns", MsgBoxStyle.Exclamation, "Patterns");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj10) when (obj10 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj10);
				try0001_dispatch = 1803;
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
		return flag;
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
		string myExtraData = default(string);
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
							goto IL_007b;
						case 5:
						case 6:
							goto IL_0096;
						case 8:
							goto IL_00a9;
						case 9:
							goto IL_00b1;
						case 10:
							goto IL_00d0;
						case 11:
							goto IL_00da;
						case 12:
							goto IL_010c;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 13:
						case 14:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_007b:
					num2 = 3;
					Interaction.MsgBox("You can only specify one x variable with x-computed expressions", MsgBoxStyle.Information, "Invalid");
					goto end_IL_0001_3;
					IL_0096:
					num2 = 6;
					if (Check_Col_Pattern())
					{
						goto end_IL_0001_3;
					}
					goto IL_00a9;
					IL_00b1:
					num2 = 9;
					if (Operators.ConditionalCompareObjectEqual(cmdExtra.Tag, "X", TextCompare: false))
					{
						goto IL_00d0;
					}
					goto IL_00da;
					IL_00a9:
					num2 = 8;
					myExtraData = "";
					goto IL_00b1;
					IL_000b:
					num2 = 2;
					if (Conversions.ToBoolean(Operators.CompareString(f_TemplateType2, "JSL", TextCompare: false) != 0 && LstX.Items.Count == 1 && Conversions.ToBoolean(LikeOperator.LikeObject(NewLateBinding.LateGet(LstX.Items[0], null, "tolower", new object[0], null, null, null), "r.ce.*", CompareMethod.Binary))))
					{
						goto IL_007b;
					}
					goto IL_0096;
					IL_00da:
					num2 = 11;
					if (checked(LstColumns.SelectedIndices.Count + LstX.Items.Count) <= fXMax)
					{
						break;
					}
					goto IL_010c;
					IL_010c:
					num2 = 12;
					Interaction.MsgBox("You have exceeded the maximum allowable number of X variables (" + Conversions.ToString(fXMax) + ").", MsgBoxStyle.Exclamation, "Exceeded Max");
					goto end_IL_0001_3;
					IL_00d0:
					num2 = 10;
					myExtraData = f_ExtraData;
					goto IL_00da;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				ListBox DestList = LstX;
				Add_ListBox(ref DestList, myExtraData);
				LstX = DestList;
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
				case 216:
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
							goto IL_001c;
						case 5:
							goto IL_004d;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Check_Col_Pattern())
					{
						goto end_IL_0001_3;
					}
					goto IL_001c;
					IL_001c:
					num2 = 4;
					if (checked(LstColumns.SelectedIndices.Count + LstBy.Items.Count) <= fBYMax)
					{
						break;
					}
					goto IL_004d;
					IL_004d:
					num2 = 5;
					Interaction.MsgBox("You have exceeded the maximum allowable number of BY variables (" + Conversions.ToString(fBYMax) + ").", MsgBoxStyle.Exclamation, "Exceeded Maxi");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				ListBox DestList = LstBy;
				Add_ListBox(ref DestList);
				LstBy = DestList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 216;
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

	private void UpdateCmbData(ref ComboBox MyCmb, string MyMode)
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
							goto IL_001b;
						case 5:
							goto IL_006b;
						case 6:
							goto IL_007f;
						case 7:
							goto IL_0093;
						case 8:
							goto IL_00a7;
						case 9:
							goto IL_00bb;
						case 10:
							goto IL_00d0;
						case 12:
							goto IL_00e6;
						case 13:
							goto IL_00fb;
						case 14:
							goto IL_0110;
						case 15:
							goto IL_0125;
						case 16:
							goto IL_013a;
						case 18:
							goto IL_0150;
						case 19:
							goto IL_0165;
						case 20:
							goto IL_017a;
						case 22:
							goto IL_0192;
						case 23:
							goto IL_01a7;
						case 24:
							goto IL_01bc;
						case 25:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 11:
						case 17:
						case 21:
						case 26:
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a7:
					num2 = 8;
					MyCmb.Items.Add("orange");
					goto IL_00bb;
					IL_00bb:
					num2 = 9;
					MyCmb.Items.Add("red");
					goto IL_00d0;
					IL_0093:
					num2 = 7;
					MyCmb.Items.Add("green");
					goto IL_00a7;
					IL_00d0:
					num2 = 10;
					MyCmb.Text = "black";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					MyCmb.Items.Clear();
					goto IL_001b;
					IL_001b:
					num2 = 3;
					switch (Strings.UCase(MyMode))
					{
					case "C":
						break;
					case "S":
						goto IL_00e6;
					case "R":
						goto IL_0150;
					case "X":
						goto IL_0192;
					default:
						goto end_IL_0001_3;
					}
					goto IL_006b;
					IL_0192:
					num2 = 22;
					MyCmb.Items.Add("category");
					goto IL_01a7;
					IL_01a7:
					num2 = 23;
					MyCmb.Items.Add("date");
					goto IL_01bc;
					IL_01bc:
					num2 = 24;
					MyCmb.Items.Add("linear");
					break;
					IL_0150:
					num2 = 18;
					MyCmb.Items.Add("-30");
					goto IL_0165;
					IL_0165:
					num2 = 19;
					MyCmb.Items.Add("-45");
					goto IL_017a;
					IL_017a:
					num2 = 20;
					MyCmb.Items.Add("-90");
					goto end_IL_0001_3;
					IL_00e6:
					num2 = 12;
					MyCmb.Items.Add("dash");
					goto IL_00fb;
					IL_00fb:
					num2 = 13;
					MyCmb.Items.Add("dashdot");
					goto IL_0110;
					IL_0110:
					num2 = 14;
					MyCmb.Items.Add("dot");
					goto IL_0125;
					IL_0125:
					num2 = 15;
					MyCmb.Items.Add("solid");
					goto IL_013a;
					IL_013a:
					num2 = 16;
					MyCmb.Text = "solid";
					goto end_IL_0001_3;
					IL_006b:
					num2 = 5;
					MyCmb.Items.Add("black");
					goto IL_007f;
					IL_007f:
					num2 = 6;
					MyCmb.Items.Add("blue");
					goto IL_0093;
					end_IL_0001_2:
					break;
				}
				num2 = 25;
				MyCmb.Items.Add("log");
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

	private void FrmChart_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object instance = default(object);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		int num11 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Button MyButton;
					string left;
					ComboBox MyCmb;
					object mycmb;
					TabPage TabOptions;
					TabPage TabOptions2;
					ToolTip ToolTip;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 6940:
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
								goto IL_00ab;
							case 8:
								goto IL_00cb;
							case 9:
								goto IL_00eb;
							case 10:
								goto IL_010c;
							case 11:
								goto IL_012d;
							case 12:
								goto IL_014e;
							case 13:
								goto IL_016f;
							case 14:
								goto IL_0190;
							case 15:
								goto IL_01b1;
							case 16:
								goto IL_01c0;
							case 17:
								goto IL_01c6;
							case 18:
								goto IL_01cc;
							case 19:
								goto IL_01ed;
							case 21:
								goto IL_021c;
							case 22:
								goto IL_022a;
							case 23:
								goto IL_0238;
							case 24:
								goto IL_024c;
							case 25:
								goto IL_0265;
							case 26:
								goto IL_027e;
							case 27:
								goto IL_0292;
							case 28:
								goto IL_02ac;
							case 29:
								goto IL_02bc;
							case 30:
								goto IL_02cc;
							case 31:
								goto IL_02dc;
							case 33:
								goto IL_02f2;
							case 34:
								goto IL_0300;
							case 35:
								goto IL_030e;
							case 36:
								goto IL_031f;
							case 37:
								goto IL_032f;
							case 38:
								goto IL_0349;
							case 39:
								goto IL_0363;
							case 40:
								goto IL_037d;
							case 41:
								goto IL_038d;
							case 42:
								goto IL_03b6;
							case 43:
								goto IL_03d0;
							case 44:
								goto IL_03e0;
							case 46:
								goto IL_03f8;
							case 47:
								goto IL_0408;
							case 45:
							case 48:
							case 49:
								goto IL_0419;
							case 50:
								goto IL_0436;
							case 51:
								goto IL_0446;
							case 52:
								goto IL_0456;
							case 53:
								goto IL_046f;
							case 54:
								goto IL_0488;
							case 55:
							case 56:
								goto IL_04a3;
							case 57:
								goto IL_04c3;
							case 58:
								goto IL_04dd;
							case 59:
								goto IL_04ed;
							case 60:
								goto IL_0501;
							case 61:
								goto IL_0511;
							case 62:
								goto IL_0521;
							case 63:
								goto IL_0535;
							case 64:
								goto IL_0549;
							case 65:
								goto IL_055d;
							case 66:
								goto IL_0576;
							case 67:
								goto IL_058a;
							case 68:
								goto IL_059e;
							case 69:
								goto IL_05b7;
							case 70:
								goto IL_05d0;
							case 71:
								goto IL_05e9;
							case 72:
								goto IL_05fd;
							case 73:
								goto IL_0611;
							case 74:
								goto IL_0625;
							case 75:
								goto IL_063e;
							case 76:
								goto IL_0657;
							case 77:
								goto IL_066b;
							case 78:
								goto IL_0685;
							case 79:
								goto IL_069f;
							case 80:
								goto IL_06b9;
							case 81:
								goto IL_06db;
							case 82:
								goto IL_06fd;
							case 83:
								goto IL_071f;
							case 84:
								goto IL_0741;
							case 85:
								goto IL_0763;
							case 86:
								goto IL_0785;
							case 87:
								goto IL_07a7;
							case 88:
								goto IL_07c9;
							case 89:
								goto IL_07eb;
							case 90:
								goto IL_080d;
							case 91:
								goto IL_082f;
							case 92:
								goto IL_0851;
							case 93:
								goto IL_0873;
							case 94:
								goto IL_0895;
							case 95:
								goto IL_08b7;
							case 96:
								goto IL_08d9;
							case 97:
								goto IL_0900;
							case 98:
								goto IL_0927;
							case 99:
								goto IL_0949;
							case 100:
								goto IL_096b;
							case 101:
								goto IL_098d;
							case 102:
								goto IL_09af;
							case 103:
								goto IL_09d1;
							case 104:
								goto IL_09f3;
							case 105:
								goto IL_0a07;
							case 20:
							case 32:
							case 106:
							case 107:
							case 108:
								goto IL_0a26;
							case 109:
								goto IL_0a36;
							case 110:
								goto IL_0a40;
							case 111:
								goto IL_0a75;
							case 112:
								goto IL_0a9c;
							case 113:
								goto IL_0ac3;
							case 114:
								goto IL_0ae7;
							case 116:
								goto IL_0afb;
							case 117:
								goto IL_0b1f;
							case 119:
								goto IL_0b33;
							case 120:
								goto IL_0b57;
							case 122:
								goto IL_0b6b;
							case 123:
								goto IL_0b8f;
							case 125:
								goto IL_0ba3;
							case 126:
								goto IL_0bc7;
							case 128:
								goto IL_0bdb;
							case 129:
								goto IL_0c02;
							case 131:
								goto IL_0c19;
							case 132:
								goto IL_0c40;
							case 134:
								goto IL_0c57;
							case 135:
								goto IL_0c7e;
							case 137:
								goto IL_0c92;
							case 138:
								goto IL_0cb9;
							case 140:
								goto IL_0ccd;
							case 141:
								goto IL_0cf4;
							case 115:
							case 118:
							case 121:
							case 124:
							case 127:
							case 130:
							case 133:
							case 136:
							case 139:
							case 142:
							case 143:
								goto IL_0d06;
							case 144:
								goto IL_0d1d;
							case 145:
								goto IL_0d39;
							case 146:
								goto IL_0d50;
							case 147:
								goto IL_0d6c;
							case 148:
								goto IL_0d83;
							case 149:
								goto IL_0da6;
							case 150:
								goto IL_0dc2;
							case 151:
								goto IL_0dde;
							case 152:
								goto IL_0dfa;
							case 153:
								goto IL_0e16;
							case 154:
								goto IL_0e32;
							case 155:
								goto IL_0e4e;
							case 156:
								goto IL_0e6a;
							case 158:
								goto IL_0e8c;
							case 159:
								goto IL_0ee8;
							case 160:
								goto IL_0f04;
							case 161:
								goto IL_0f20;
							case 162:
								goto IL_0f3c;
							case 164:
								goto IL_0f5e;
							case 165:
								goto IL_0f81;
							case 166:
								goto IL_0f98;
							case 167:
								goto IL_0faf;
							case 168:
								goto IL_0fc6;
							case 169:
								goto IL_0fdd;
							case 170:
								goto IL_0ff9;
							case 157:
							case 163:
							case 171:
							case 172:
								goto IL_1016;
							case 173:
								goto IL_1036;
							case 174:
								goto IL_1049;
							case 175:
							case 176:
								goto IL_105e;
							case 177:
								goto IL_1078;
							case 178:
								goto IL_10b8;
							case 180:
								goto IL_10d0;
							case 181:
								goto IL_10f3;
							case 182:
								goto IL_110f;
							case 183:
								goto IL_112b;
							case 184:
								goto IL_1147;
							case 185:
								goto IL_1163;
							case 186:
								goto IL_117f;
							case 187:
								goto IL_119b;
							case 188:
								goto IL_11b5;
							case 189:
								goto IL_11f5;
							case 179:
							case 190:
							case 191:
								goto IL_1208;
							case 192:
								goto IL_1228;
							case 193:
								goto IL_1245;
							case 194:
								goto IL_128b;
							case 195:
								goto IL_12ad;
							case 196:
								goto IL_12fa;
							case 197:
								goto IL_1303;
							case 198:
								goto IL_131d;
							case 199:
								goto IL_134f;
							case 200:
								goto IL_136e;
							case 201:
								goto IL_13bf;
							case 202:
							case 203:
								goto IL_13d3;
							case 204:
								goto IL_13e8;
							case 205:
								goto IL_140b;
							case 206:
								goto IL_143a;
							case 207:
								goto IL_1469;
							case 208:
							case 209:
								goto IL_149a;
							case 210:
								goto IL_14d2;
							case 211:
								goto IL_1501;
							case 212:
								goto IL_1530;
							case 213:
								goto IL_155f;
							case 214:
								goto IL_158e;
							case 215:
								goto IL_15bd;
							case 217:
								goto IL_15f2;
							case 218:
								goto IL_163f;
							case 219:
								goto IL_166e;
							case 220:
								goto IL_169d;
							case 221:
								goto IL_16bd;
							case 223:
								goto IL_16f0;
							case 216:
							case 222:
							case 224:
							case 225:
							case 226:
								goto IL_1721;
							case 227:
								goto IL_173e;
							case 228:
								goto IL_1751;
							case 229:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 230:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_12fa:
						num2 = 196;
						instance = null;
						goto IL_1303;
						IL_1303:
						num2 = 197;
						instance = this.TabOptions.Controls[num5];
						goto IL_131d;
						IL_13d3:
						num2 = 203;
						num5++;
						goto IL_13df;
						IL_131d:
						num2 = 198;
						NewLateBinding.LateCall(NewLateBinding.LateGet(instance, null, "Items", new object[0], null, null, null), null, "Clear", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_134f;
						IL_000b:
						num2 = 2;
						MyButton = cmdCancel;
						BuildForm.Set_Btn_Img(ref MyButton, "cancel");
						cmdCancel = MyButton;
						goto IL_002b;
						IL_002b:
						num2 = 3;
						MyButton = cmdOK;
						BuildForm.Set_Btn_Img(ref MyButton, "ok");
						cmdOK = MyButton;
						goto IL_004b;
						IL_004b:
						num2 = 4;
						MyButton = CmdUp;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						CmdUp = MyButton;
						goto IL_006b;
						IL_006b:
						num2 = 5;
						MyButton = cmdDown;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						cmdDown = MyButton;
						goto IL_008b;
						IL_008b:
						num2 = 6;
						MyButton = cmdDelete;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						cmdDelete = MyButton;
						goto IL_00ab;
						IL_00ab:
						num2 = 7;
						MyButton = CmdRemove;
						BuildForm.Set_Btn_Img(ref MyButton, "clear");
						CmdRemove = MyButton;
						goto IL_00cb;
						IL_00cb:
						num2 = 8;
						MyButton = cmdClear;
						BuildForm.Set_Btn_Img(ref MyButton, "clear");
						cmdClear = MyButton;
						goto IL_00eb;
						IL_00eb:
						num2 = 9;
						MyButton = CmdColHelp;
						BuildForm.Set_Btn_Img(ref MyButton, "helpb");
						CmdColHelp = MyButton;
						goto IL_010c;
						IL_010c:
						num2 = 10;
						MyButton = cmdUp2;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						cmdUp2 = MyButton;
						goto IL_012d;
						IL_012d:
						num2 = 11;
						MyButton = cmddown2;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						cmddown2 = MyButton;
						goto IL_014e;
						IL_014e:
						num2 = 12;
						MyButton = cmdDel2;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						cmdDel2 = MyButton;
						goto IL_016f;
						IL_016f:
						num2 = 13;
						MyButton = cmdClear2;
						BuildForm.Set_Btn_Img(ref MyButton, "clear");
						cmdClear2 = MyButton;
						goto IL_0190;
						IL_0190:
						num2 = 14;
						MyButton = cmdRefresh;
						BuildForm.Set_Btn_Img(ref MyButton, "refresh");
						cmdRefresh = MyButton;
						goto IL_01b1;
						IL_01b1:
						num2 = 15;
						num6 = GridWhere.Width;
						goto IL_01c0;
						IL_01c0:
						num2 = 16;
						num5 = 0;
						goto IL_01c6;
						IL_01c6:
						num2 = 17;
						num7 = 0;
						goto IL_01cc;
						IL_01cc:
						num2 = 18;
						TabOptions = this.TabOptions;
						BuildChart.Add_Labels_To_Options(ref TabOptions, 2, 23, 6);
						this.TabOptions = TabOptions;
						goto IL_01ed;
						IL_01ed:
						num2 = 19;
						left = f_TemplateType2;
						if (Operators.CompareString(left, "JSL", TextCompare: false) == 0 || Operators.CompareString(left, "JMP", TextCompare: false) == 0)
						{
							goto IL_021c;
						}
						goto IL_02f2;
						IL_134f:
						num2 = 199;
						num8 = LstColumns.Items.Count - 1;
						num7 = 0;
						goto IL_13cb;
						IL_02f2:
						num2 = 33;
						f_MyPre = "";
						goto IL_0300;
						IL_0300:
						num2 = 34;
						f_MyPost = "";
						goto IL_030e;
						IL_030e:
						num2 = 35;
						GridTheme.RowCount = 50;
						goto IL_031f;
						IL_031f:
						num2 = 36;
						YRTxtAdj1.Visible = true;
						goto IL_032f;
						IL_032f:
						num2 = 37;
						ToolTip1.SetToolTip(CmdColHelp, "Note on the StartingWith:, EndingWith:, and Containing: selections");
						goto IL_0349;
						IL_0349:
						num2 = 38;
						ToolTip1.SetToolTip(TxtTitle, "Reference column values by bounding with {}. E.g., {column}");
						goto IL_0363;
						IL_0363:
						num2 = 39;
						if (LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
						{
							goto IL_037d;
						}
						goto IL_03f8;
						IL_037d:
						num2 = 40;
						TxtInFile.DropDownStyle = ComboBoxStyle.DropDown;
						goto IL_038d;
						IL_038d:
						num2 = 41;
						TxtInFile.Width -= cmdBrowse.Width + 10;
						goto IL_03b6;
						IL_03b6:
						num2 = 42;
						cmdBrowse.Top = TxtInFile.Top;
						goto IL_03d0;
						IL_03d0:
						num2 = 43;
						cmdBrowse.Visible = true;
						goto IL_03e0;
						IL_03e0:
						num2 = 44;
						LblInFile.Text = "Input File";
						goto IL_0419;
						IL_03f8:
						num2 = 46;
						cmdRefresh.Visible = false;
						goto IL_0408;
						IL_0408:
						num2 = 47;
						chkHTTP.Visible = false;
						goto IL_0419;
						IL_0419:
						num2 = 49;
						if (Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0)
						{
							goto IL_0436;
						}
						goto IL_04a3;
						IL_0436:
						num2 = 50;
						GrpRLegend.Visible = true;
						goto IL_0446;
						IL_0446:
						num2 = 51;
						GrpRowLegend.Visible = false;
						goto IL_0456;
						IL_0456:
						num2 = 52;
						ColParenO.Items.Add("!(");
						goto IL_046f;
						IL_046f:
						num2 = 53;
						ColParenO.Items.Add("!((");
						goto IL_0488;
						IL_0488:
						num2 = 54;
						ColParenO.Items.Add("!(((");
						goto IL_04a3;
						IL_04a3:
						num2 = 56;
						if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
						{
							goto IL_04c3;
						}
						goto IL_0a26;
						IL_04c3:
						num2 = 57;
						YRlineTxt1.Width = YRlineTxt2.Width;
						goto IL_04dd;
						IL_04dd:
						num2 = 58;
						YRTxtAdj1.Visible = false;
						goto IL_04ed;
						IL_04ed:
						num2 = 59;
						Label11.Text = "Fill:";
						goto IL_0501;
						IL_0501:
						num2 = 60;
						GrpRLegend.Visible = true;
						goto IL_0511;
						IL_0511:
						num2 = 61;
						GrpRowLegend.Visible = true;
						goto IL_0521;
						IL_0521:
						num2 = 62;
						lbllx.Text = "X Loc";
						goto IL_0535;
						IL_0535:
						num2 = 63;
						lblly.Text = "Y Loc";
						goto IL_0549;
						IL_0549:
						num2 = 64;
						CmbLSize.Items.Clear();
						goto IL_055d;
						IL_055d:
						num2 = 65;
						CmbLSize.Items.Add("-.14");
						goto IL_0576;
						IL_0576:
						num2 = 66;
						CmbLSize.Text = "";
						goto IL_058a;
						IL_058a:
						num2 = 67;
						CmbLKSize.Items.Clear();
						goto IL_059e;
						IL_059e:
						num2 = 68;
						CmbLKSize.Items.Add("1.5");
						goto IL_05b7;
						IL_05b7:
						num2 = 69;
						CmbLKSize.Items.Add("-.7");
						goto IL_05d0;
						IL_05d0:
						num2 = 70;
						CmbLKSize.Items.Add(".5");
						goto IL_05e9;
						IL_05e9:
						num2 = 71;
						CmbLKSize.Text = "";
						goto IL_05fd;
						IL_05fd:
						num2 = 72;
						lbllcols.Text = "Show:";
						goto IL_0611;
						IL_0611:
						num2 = 73;
						CmbLCols.Items.Clear();
						goto IL_0625;
						IL_0625:
						num2 = 74;
						CmbLCols.Items.Add("True");
						goto IL_063e;
						IL_063e:
						num2 = 75;
						CmbLCols.Items.Add("False");
						goto IL_0657;
						IL_0657:
						num2 = 76;
						CmbLCols.Text = "True";
						goto IL_066b;
						IL_066b:
						num2 = 77;
						ToolTip1.SetToolTip(CmbLCols, "Show Legend");
						goto IL_0685;
						IL_0685:
						num2 = 78;
						ToolTip1.SetToolTip(CmbLSize, "X Increment for Legend Location");
						goto IL_069f;
						IL_069f:
						num2 = 79;
						ToolTip1.SetToolTip(CmbLKSize, "Y Increment for Legend Location");
						goto IL_06b9;
						IL_06b9:
						num2 = 80;
						MyCmb = YRlineColor1;
						UpdateCmbData(ref MyCmb, "C");
						YRlineColor1 = MyCmb;
						goto IL_06db;
						IL_06db:
						num2 = 81;
						MyCmb = YRlineColor2;
						UpdateCmbData(ref MyCmb, "C");
						YRlineColor2 = MyCmb;
						goto IL_06fd;
						IL_06fd:
						num2 = 82;
						MyCmb = YRlineColor3;
						UpdateCmbData(ref MyCmb, "C");
						YRlineColor3 = MyCmb;
						goto IL_071f;
						IL_071f:
						num2 = 83;
						MyCmb = YRlineColor4;
						UpdateCmbData(ref MyCmb, "C");
						YRlineColor4 = MyCmb;
						goto IL_0741;
						IL_0741:
						num2 = 84;
						MyCmb = XRlineColor1;
						UpdateCmbData(ref MyCmb, "C");
						XRlineColor1 = MyCmb;
						goto IL_0763;
						IL_0763:
						num2 = 85;
						MyCmb = XRlineColor2;
						UpdateCmbData(ref MyCmb, "C");
						XRlineColor2 = MyCmb;
						goto IL_0785;
						IL_0785:
						num2 = 86;
						MyCmb = XRlineColor3;
						UpdateCmbData(ref MyCmb, "C");
						XRlineColor3 = MyCmb;
						goto IL_07a7;
						IL_07a7:
						num2 = 87;
						MyCmb = XRlineColor4;
						UpdateCmbData(ref MyCmb, "C");
						XRlineColor4 = MyCmb;
						goto IL_07c9;
						IL_07c9:
						num2 = 88;
						MyCmb = YRlineStyle1;
						UpdateCmbData(ref MyCmb, "S");
						YRlineStyle1 = MyCmb;
						goto IL_07eb;
						IL_07eb:
						num2 = 89;
						MyCmb = YRlineStyle2;
						UpdateCmbData(ref MyCmb, "S");
						YRlineStyle2 = MyCmb;
						goto IL_080d;
						IL_080d:
						num2 = 90;
						MyCmb = YRlineStyle3;
						UpdateCmbData(ref MyCmb, "S");
						YRlineStyle3 = MyCmb;
						goto IL_082f;
						IL_082f:
						num2 = 91;
						MyCmb = YRlineStyle4;
						UpdateCmbData(ref MyCmb, "S");
						YRlineStyle4 = MyCmb;
						goto IL_0851;
						IL_0851:
						num2 = 92;
						MyCmb = XRlineStyle1;
						UpdateCmbData(ref MyCmb, "S");
						XRlineStyle1 = MyCmb;
						goto IL_0873;
						IL_0873:
						num2 = 93;
						MyCmb = XRlineStyle2;
						UpdateCmbData(ref MyCmb, "S");
						XRlineStyle2 = MyCmb;
						goto IL_0895;
						IL_0895:
						num2 = 94;
						MyCmb = XRlineStyle3;
						UpdateCmbData(ref MyCmb, "S");
						XRlineStyle3 = MyCmb;
						goto IL_08b7;
						IL_08b7:
						num2 = 95;
						MyCmb = XRlineStyle4;
						UpdateCmbData(ref MyCmb, "S");
						XRlineStyle4 = MyCmb;
						goto IL_08d9;
						IL_08d9:
						num2 = 96;
						mycmb = cmbSym;
						Load_Theme("plotly_sym", ref mycmb);
						cmbSym = (DataGridViewComboBoxColumn)mycmb;
						goto IL_0900;
						IL_0900:
						num2 = 97;
						mycmb = cmblinetype;
						Load_Theme("plotly_linetype", ref mycmb);
						cmblinetype = (DataGridViewComboBoxColumn)mycmb;
						goto IL_0927;
						IL_0927:
						num2 = 98;
						MyCmb = cmbXType;
						UpdateCmbData(ref MyCmb, "X");
						cmbXType = MyCmb;
						goto IL_0949;
						IL_0949:
						num2 = 99;
						MyCmb = cmbYType;
						UpdateCmbData(ref MyCmb, "X");
						cmbYType = MyCmb;
						goto IL_096b;
						IL_096b:
						num2 = 100;
						MyCmb = cmbY2Type;
						UpdateCmbData(ref MyCmb, "X");
						cmbY2Type = MyCmb;
						goto IL_098d;
						IL_098d:
						num2 = 101;
						MyCmb = cmbXRot;
						UpdateCmbData(ref MyCmb, "R");
						cmbXRot = MyCmb;
						goto IL_09af;
						IL_09af:
						num2 = 102;
						MyCmb = cmbYRot;
						UpdateCmbData(ref MyCmb, "R");
						cmbYRot = MyCmb;
						goto IL_09d1;
						IL_09d1:
						num2 = 103;
						MyCmb = cmbY2Rot;
						UpdateCmbData(ref MyCmb, "R");
						cmbY2Rot = MyCmb;
						goto IL_09f3;
						IL_09f3:
						num2 = 104;
						Lbl1Image.Text = "Image";
						goto IL_0a07;
						IL_0a07:
						num2 = 105;
						ToolTip1.SetToolTip(Lbl1Image, "Check to Save Chart as Image. Currently disabled");
						goto IL_0a26;
						IL_021c:
						num2 = 21;
						f_MyPre = ":Name(\"";
						goto IL_022a;
						IL_022a:
						num2 = 22;
						f_MyPost = "\")";
						goto IL_0238;
						IL_0238:
						num2 = 23;
						FrameColor.Items.Clear();
						goto IL_024c;
						IL_024c:
						num2 = 24;
						FrameColor.Items.Add("Light Yellow");
						goto IL_0265;
						IL_0265:
						num2 = 25;
						FrameColor.Items.Add("White");
						goto IL_027e;
						IL_027e:
						num2 = 26;
						FrameColor.Text = "Light Yellow";
						goto IL_0292;
						IL_0292:
						num2 = 27;
						YRlineTxt1.Width = YRlineTxt2.Width;
						goto IL_02ac;
						IL_02ac:
						num2 = 28;
						YRTxtAdj1.Visible = false;
						goto IL_02bc;
						IL_02bc:
						num2 = 29;
						mnuHelp.Visible = false;
						goto IL_02cc;
						IL_02cc:
						num2 = 30;
						cmdRefresh.Visible = false;
						goto IL_02dc;
						IL_02dc:
						num2 = 31;
						chkHTTP.Visible = false;
						goto IL_0a26;
						IL_0a26:
						num2 = 108;
						TabChart.SelectedIndex = 0;
						goto IL_0a36;
						IL_0a36:
						num2 = 109;
						Init_Controls();
						goto IL_0a40;
						IL_0a40:
						num2 = 110;
						if ((Operators.CompareString(f_TemplateType2, "HTML", TextCompare: false) == 0) | (Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0))
						{
							goto IL_0a75;
						}
						goto IL_10d0;
						IL_0a75:
						num2 = 111;
						mycmb = cmbSym;
						Load_Theme("r_sym", ref mycmb);
						cmbSym = (DataGridViewComboBoxColumn)mycmb;
						goto IL_0a9c;
						IL_0a9c:
						num2 = 112;
						mycmb = cmblinetype;
						Load_Theme("r_linetype", ref mycmb);
						cmblinetype = (DataGridViewComboBoxColumn)mycmb;
						goto IL_0ac3;
						IL_0ac3:
						num2 = 113;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "JS-*", CompareMethod.Binary))
						{
							goto IL_0ae7;
						}
						goto IL_0afb;
						IL_0ae7:
						num2 = 114;
						f_SpecialChart = "JS-";
						goto IL_0d06;
						IL_0afb:
						num2 = 116;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*BARCHART*", CompareMethod.Binary))
						{
							goto IL_0b1f;
						}
						goto IL_0b33;
						IL_0b1f:
						num2 = 117;
						f_SpecialChart = "RBARCHART";
						goto IL_0d06;
						IL_0b33:
						num2 = 119;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*STRIPMAP*", CompareMethod.Binary))
						{
							goto IL_0b57;
						}
						goto IL_0b6b;
						IL_0b57:
						num2 = 120;
						f_SpecialChart = "RSTRIPMAP";
						goto IL_0d06;
						IL_0b6b:
						num2 = 122;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*OVERLAY*", CompareMethod.Binary))
						{
							goto IL_0b8f;
						}
						goto IL_0ba3;
						IL_0b8f:
						num2 = 123;
						f_SpecialChart = "ROVERLAY";
						goto IL_0d06;
						IL_0ba3:
						num2 = 125;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*PARETO*", CompareMethod.Binary))
						{
							goto IL_0bc7;
						}
						goto IL_0bdb;
						IL_0bc7:
						num2 = 126;
						f_SpecialChart = "RPARETO";
						goto IL_0d06;
						IL_0bdb:
						num2 = 128;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*WAFERMAP*", CompareMethod.Binary))
						{
							goto IL_0c02;
						}
						goto IL_0c19;
						IL_0c02:
						num2 = 129;
						f_SpecialChart = "RWAFERMAP";
						goto IL_0d06;
						IL_0c19:
						num2 = 131;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*WAFER-DEFECT*R*", CompareMethod.Binary))
						{
							goto IL_0c40;
						}
						goto IL_0c57;
						IL_0c40:
						num2 = 132;
						f_SpecialChart = "RWAFERD";
						goto IL_0d06;
						IL_0c57:
						num2 = 134;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*HISTOGRAM*", CompareMethod.Binary))
						{
							goto IL_0c7e;
						}
						goto IL_0c92;
						IL_0c7e:
						num2 = 135;
						f_SpecialChart = "RHISTOGRAM";
						goto IL_0d06;
						IL_0c92:
						num2 = 137;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "*XY PLOT*", CompareMethod.Binary))
						{
							goto IL_0cb9;
						}
						goto IL_0ccd;
						IL_0cb9:
						num2 = 138;
						f_SpecialChart = "RXY";
						goto IL_0d06;
						IL_0ccd:
						num2 = 140;
						if (LikeOperator.LikeString(Strings.UCase(LblFormTitle.Text), "3D*", CompareMethod.Binary))
						{
							goto IL_0cf4;
						}
						goto IL_0d06;
						IL_0cf4:
						num2 = 141;
						f_SpecialChart = "3DSURFACE";
						goto IL_0d06;
						IL_0d06:
						num2 = 143;
						TxtLOY.Items.Clear();
						goto IL_0d1d;
						IL_0d1d:
						num2 = 144;
						TxtLOY.Items.Add("0");
						goto IL_0d39;
						IL_0d39:
						num2 = 145;
						TxtLoY2.Items.Clear();
						goto IL_0d50;
						IL_0d50:
						num2 = 146;
						TxtLoY2.Items.Add("0");
						goto IL_0d6c;
						IL_0d6c:
						num2 = 147;
						TxtHiY.Items.Clear();
						goto IL_0d83;
						IL_0d83:
						num2 = 148;
						if (Operators.CompareString(f_SpecialChart, "RWAFERD", TextCompare: false) == 0)
						{
							goto IL_0da6;
						}
						goto IL_0e8c;
						IL_0da6:
						num2 = 149;
						TxtLOY.Items.Add("-150000");
						goto IL_0dc2;
						IL_0dc2:
						num2 = 150;
						TxtLoX.Items.Add("-150000");
						goto IL_0dde;
						IL_0dde:
						num2 = 151;
						TxtHiX.Items.Add("150000");
						goto IL_0dfa;
						IL_0dfa:
						num2 = 152;
						TxtHiY.Items.Add("150000");
						goto IL_0e16;
						IL_0e16:
						num2 = 153;
						TxtLOY.Items.Add("-150");
						goto IL_0e32;
						IL_0e32:
						num2 = 154;
						TxtLoX.Items.Add("-150");
						goto IL_0e4e;
						IL_0e4e:
						num2 = 155;
						TxtHiX.Items.Add("150");
						goto IL_0e6a;
						IL_0e6a:
						num2 = 156;
						TxtHiY.Items.Add("150");
						goto IL_1016;
						IL_0e8c:
						num2 = 158;
						if (Operators.CompareString(f_SpecialChart, "RBARCHART", TextCompare: false) != 0 && Operators.CompareString(f_SpecialChart, "RHISTOGRAM", TextCompare: false) != 0 && Operators.CompareString(f_SpecialChart, "ROVERLAY", TextCompare: false) != 0 && Operators.CompareString(f_SpecialChart, "3DSURFACE", TextCompare: false) != 0)
						{
							goto IL_0ee8;
						}
						goto IL_0f5e;
						IL_13cb:
						if (num7 <= num8)
						{
							goto IL_136e;
						}
						goto IL_13d3;
						IL_136e:
						num2 = 200;
						NewLateBinding.LateCall(NewLateBinding.LateGet(instance, null, "Items", new object[0], null, null, null), null, "Add", new object[1] { Strings.Trim(Conversions.ToString(LstColumns.Items[num7])) }, null, null, null, IgnoreReturn: true);
						goto IL_13bf;
						IL_13bf:
						num2 = 201;
						num7++;
						goto IL_13cb;
						IL_0ee8:
						num2 = 159;
						TxtLOY.Items.Add("AutoMin");
						goto IL_0f04;
						IL_0f04:
						num2 = 160;
						TxtLOY.Items.Add("*");
						goto IL_0f20;
						IL_0f20:
						num2 = 161;
						TxtHiY.Items.Add("AutoMax");
						goto IL_0f3c;
						IL_0f3c:
						num2 = 162;
						TxtHiY.Items.Add("*");
						goto IL_1016;
						IL_0f5e:
						num2 = 164;
						if (Operators.CompareString(f_SpecialChart, "3DSURFACE", TextCompare: false) == 0)
						{
							goto IL_0f81;
						}
						goto IL_1016;
						IL_0f81:
						num2 = 165;
						TxtLoX.Items.Clear();
						goto IL_0f98;
						IL_0f98:
						num2 = 166;
						TxtHiX.Items.Clear();
						goto IL_0faf;
						IL_0faf:
						num2 = 167;
						TxtLOY.Items.Clear();
						goto IL_0fc6;
						IL_0fc6:
						num2 = 168;
						TxtHiY.Items.Clear();
						goto IL_0fdd;
						IL_0fdd:
						num2 = 169;
						TxtLoY2.Items.Add("AutoMinZ");
						goto IL_0ff9;
						IL_0ff9:
						num2 = 170;
						TxtHiY2.Items.Add("AutoMaxZ");
						goto IL_1016;
						IL_1016:
						num2 = 172;
						if (Operators.CompareString(f_SpecialChart, "RSTRIPMAP", TextCompare: false) != 0)
						{
							goto IL_1036;
						}
						goto IL_105e;
						IL_1036:
						num2 = 173;
						TxtIncY.Visible = false;
						goto IL_1049;
						IL_1049:
						num2 = 174;
						TxtIncX.Visible = false;
						goto IL_105e;
						IL_105e:
						num2 = 176;
						num9 = GridTheme.ColumnCount - 1;
						num5 = 1;
						goto IL_10c4;
						IL_10c4:
						if (num5 <= num9)
						{
							goto IL_1078;
						}
						goto IL_1208;
						IL_1078:
						num2 = 177;
						GridTheme.Columns[num5].Width = (int)Math.Round((double)(GridTheme.Width - 40) / (double)GridTheme.ColumnCount);
						goto IL_10b8;
						IL_10b8:
						num2 = 178;
						num5++;
						goto IL_10c4;
						IL_10d0:
						num2 = 180;
						if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
						{
							goto IL_10f3;
						}
						goto IL_1208;
						IL_10f3:
						num2 = 181;
						TxtLOY.Items.Add("AutoMin");
						goto IL_110f;
						IL_110f:
						num2 = 182;
						TxtHiY.Items.Add("AutoMax");
						goto IL_112b;
						IL_112b:
						num2 = 183;
						TxtLoX.Items.Add("AutoMin");
						goto IL_1147;
						IL_1147:
						num2 = 184;
						TxtHiX.Items.Add("AutoMax");
						goto IL_1163;
						IL_1163:
						num2 = 185;
						TxtLoY2.Items.Add("AutoMin");
						goto IL_117f;
						IL_117f:
						num2 = 186;
						TxtHiY2.Items.Add("AutoMax");
						goto IL_119b;
						IL_119b:
						num2 = 187;
						num10 = GridTheme.ColumnCount - 1;
						num5 = 1;
						goto IL_1201;
						IL_1201:
						if (num5 <= num10)
						{
							goto IL_11b5;
						}
						goto IL_1208;
						IL_11b5:
						num2 = 188;
						GridTheme.Columns[num5].Width = (int)Math.Round((double)(GridTheme.Width - 40) / (double)GridTheme.ColumnCount);
						goto IL_11f5;
						IL_11f5:
						num2 = 189;
						num5++;
						goto IL_1201;
						IL_1208:
						num2 = 191;
						if (Operators.CompareString(f_WhereMode, "0", TextCompare: false) != 0)
						{
							goto IL_1228;
						}
						goto IL_1245;
						IL_1228:
						num2 = 192;
						LblOption0.Top = LblOption1.Top;
						goto IL_1245;
						IL_1245:
						num2 = 193;
						TabOptions = TabCols;
						TabOptions2 = this.TabOptions;
						ToolTip = ToolTip1;
						BuildChart.Init_Options(1, ref TabOptions, ref TabOptions2, ref ToolTip);
						ToolTip1 = ToolTip;
						this.TabOptions = TabOptions2;
						TabCols = TabOptions;
						goto IL_128b;
						IL_128b:
						num2 = 194;
						num11 = this.TabOptions.Controls.Count - 1;
						num5 = 0;
						goto IL_13df;
						IL_13df:
						if (num5 <= num11)
						{
							goto IL_12ad;
						}
						goto IL_13e8;
						IL_13e8:
						num2 = 204;
						if (Operators.CompareString(Text, "Text", TextCompare: false) == 0)
						{
							goto IL_140b;
						}
						goto IL_149a;
						IL_140b:
						num2 = 205;
						TxtInFile.Text = Strings.Replace(TxtInFile.Text, "\" || mdyhms(today()) || \"", "<TS>", 1, -1, CompareMethod.Text);
						goto IL_143a;
						IL_143a:
						num2 = 206;
						TxtInFile.Text = Strings.Replace(TxtInFile.Text, "\" || Long Date( Today()) || \"", "<LD>", 1, -1, CompareMethod.Text);
						goto IL_1469;
						IL_1469:
						num2 = 207;
						TxtInFile.Text = Strings.Replace(TxtInFile.Text, "\" || Short Date( Today()) || \"", "<SD>", 1, -1, CompareMethod.Text);
						goto IL_149a;
						IL_149a:
						num2 = 209;
						if ((Operators.CompareString(f_WhereMode, "0", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "3", TextCompare: false) == 0))
						{
							goto IL_14d2;
						}
						goto IL_15f2;
						IL_14d2:
						num2 = 210;
						GridWhere.Columns[0].Width = (int)Math.Round(0.09 * (double)num6);
						goto IL_1501;
						IL_1501:
						num2 = 211;
						GridWhere.Columns[1].Width = (int)Math.Round(0.08 * (double)num6);
						goto IL_1530;
						IL_1530:
						num2 = 212;
						GridWhere.Columns[2].Width = (int)Math.Round(0.25 * (double)num6);
						goto IL_155f;
						IL_155f:
						num2 = 213;
						GridWhere.Columns[3].Width = (int)Math.Round(0.12 * (double)num6);
						goto IL_158e;
						IL_158e:
						num2 = 214;
						GridWhere.Columns[4].Width = (int)Math.Round(0.25 * (double)num6);
						goto IL_15bd;
						IL_15bd:
						num2 = 215;
						GridWhere.Columns[6].Width = (int)Math.Round(0.08 * (double)num6);
						goto IL_1721;
						IL_15f2:
						num2 = 217;
						if ((Operators.CompareString(f_WhereMode, "1", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "2", TextCompare: false) == 0) | (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0))
						{
							goto IL_163f;
						}
						goto IL_1721;
						IL_163f:
						num2 = 218;
						GridWhere.Columns[2].Width = (int)Math.Round(0.3 * (double)num6);
						goto IL_166e;
						IL_166e:
						num2 = 219;
						GridWhere.Columns[3].Width = (int)Math.Round(0.27 * (double)num6);
						goto IL_169d;
						IL_169d:
						num2 = 220;
						if (Operators.CompareString(f_WhereMode, "4", TextCompare: false) == 0)
						{
							goto IL_16bd;
						}
						goto IL_16f0;
						IL_16bd:
						num2 = 221;
						GridWhere.Columns[5].Width = (int)Math.Round(0.3 * (double)num6);
						goto IL_1721;
						IL_16f0:
						num2 = 223;
						GridWhere.Columns[4].Width = (int)Math.Round(0.3 * (double)num6);
						goto IL_1721;
						IL_1721:
						num2 = 226;
						if (LikeOperator.LikeString(f_SpecialChart, "JS-*", CompareMethod.Binary))
						{
							goto IL_173e;
						}
						goto IL_1751;
						IL_173e:
						num2 = 227;
						FrameColor.Enabled = false;
						goto IL_1751;
						IL_1751:
						num2 = 228;
						Cursor = Cursors.Default;
						break;
						IL_12ad:
						num2 = 195;
						if (this.TabOptions.Controls[num5].Tag != null && Operators.ConditionalCompareObjectEqual(this.TabOptions.Controls[num5].Tag, "COMBOBOXC", TextCompare: false))
						{
							goto IL_12fa;
						}
						goto IL_13d3;
						end_IL_0001_2:
						break;
					}
					num2 = 229;
					Application.DoEvents();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6940;
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

	private void TxtInFile_SelectedValueChanged(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string MyWValue = default(string);
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
				case 579:
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
						case 6:
							goto IL_0039;
						case 7:
							goto IL_0055;
						case 9:
							goto IL_0064;
						case 10:
							goto IL_008a;
						case 11:
							goto IL_009f;
						case 12:
							goto IL_00af;
						case 13:
							goto IL_00cc;
						case 14:
							goto IL_00d6;
						case 15:
						case 16:
							goto IL_00e9;
						case 17:
							goto IL_00f3;
						case 18:
							goto IL_0107;
						case 19:
							goto IL_011b;
						case 20:
							goto IL_012f;
						case 21:
							goto IL_0143;
						case 22:
							goto IL_0157;
						case 23:
							goto IL_016b;
						case 25:
							goto IL_0183;
						case 26:
							goto IL_0198;
						case 29:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 24:
						case 27:
						case 28:
						case 30:
						case 31:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_016b:
					num2 = 23;
					RLCol2.Text = "";
					goto end_IL_0001_3;
					IL_0183:
					num2 = 25;
					TxtInFile.Text = LastChartTable;
					goto IL_0198;
					IL_0157:
					num2 = 22;
					RLCol1.Text = "";
					goto IL_016b;
					IL_0198:
					num2 = 26;
					Populate_Columns();
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyWValue = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (Operators.CompareString(fSpecialSetup, "1", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0039;
					IL_0039:
					num2 = 6;
					if (Operators.CompareString(LastChartTable, "", TextCompare: false) == 0)
					{
						goto IL_0055;
					}
					goto IL_0064;
					IL_0055:
					num2 = 7;
					Populate_Columns();
					goto end_IL_0001_3;
					IL_0064:
					num2 = 9;
					if (Operators.CompareString(TxtInFile.Text, LastChartTable, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_008a;
					IL_008a:
					num2 = 10;
					num5 = (int)Interaction.MsgBox("Do you want to select a new table and clear current selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear Selections?");
					goto IL_009f;
					IL_009f:
					num2 = 11;
					if (num5 == 6)
					{
						goto IL_00af;
					}
					goto IL_0183;
					IL_00af:
					num2 = 12;
					if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) != 0)
					{
						goto IL_00cc;
					}
					goto IL_00e9;
					IL_00cc:
					num2 = 13;
					Clear_Where_Grid();
					goto IL_00d6;
					IL_00d6:
					num2 = 14;
					Get_Set_Where("I", ref MyWValue);
					goto IL_00e9;
					IL_00e9:
					num2 = 16;
					Populate_Columns();
					goto IL_00f3;
					IL_00f3:
					num2 = 17;
					LstX.Items.Clear();
					goto IL_0107;
					IL_0107:
					num2 = 18;
					LstY.Items.Clear();
					goto IL_011b;
					IL_011b:
					num2 = 19;
					LstBy.Items.Clear();
					goto IL_012f;
					IL_012f:
					num2 = 20;
					LstGroup.Items.Clear();
					goto IL_0143;
					IL_0143:
					num2 = 21;
					LstVal.Items.Clear();
					goto IL_0157;
					end_IL_0001_2:
					break;
				}
				num2 = 29;
				Populate_Columns();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 579;
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

	private void CmdGroup_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
					bool num5;
					ListBox DestList;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 718:
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
								goto IL_001e;
							case 5:
								goto IL_0022;
							case 6:
								goto IL_0027;
							case 7:
								goto IL_0030;
							case 8:
								goto IL_004e;
							case 9:
								goto IL_007f;
							case 12:
								goto IL_00d7;
							case 15:
								goto IL_0101;
							case 16:
								goto IL_0111;
							case 18:
								goto IL_0124;
							case 19:
								goto IL_013f;
							case 20:
								goto IL_016a;
							case 21:
								goto IL_0191;
							case 22:
								goto IL_01a7;
							case 23:
								goto IL_01b3;
							case 24:
								goto IL_01c7;
							case 25:
								goto IL_01e6;
							case 27:
								goto IL_01fc;
							case 28:
								goto IL_0218;
							case 26:
							case 29:
							case 30:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 3:
							case 10:
							case 11:
							case 13:
							case 14:
							case 17:
							case 31:
							case 32:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_016a:
						num2 = 20;
						DestList = LstGroup;
						num5 = Check_Same_Var(ref DestList, text);
						LstGroup = DestList;
						if (!num5)
						{
							goto IL_0191;
						}
						goto IL_01a7;
						IL_0191:
						num2 = 21;
						LstGroup.Items.Add(text);
						goto IL_01a7;
						IL_013f:
						num2 = 19;
						text = Conversions.ToString(LstOther.Items[LstOther.SelectedIndices[num6]]);
						goto IL_016a;
						IL_01a7:
						num2 = 22;
						num6++;
						goto IL_01ae;
						IL_000b:
						num2 = 2;
						if (Check_Col_Pattern())
						{
							goto end_IL_0001_3;
						}
						goto IL_001e;
						IL_001e:
						num2 = 4;
						num6 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 5;
						num7 = -1;
						goto IL_0027;
						IL_0027:
						num2 = 6;
						text = "";
						goto IL_0030;
						IL_0030:
						num2 = 7;
						if (LstOther.Items.Count == 0)
						{
							goto IL_004e;
						}
						goto IL_0101;
						IL_004e:
						num2 = 8;
						if (LstColumns.SelectedIndices.Count + LstGroup.Items.Count > fGroupMax)
						{
							goto IL_007f;
						}
						goto IL_00d7;
						IL_007f:
						num2 = 9;
						Interaction.MsgBox("You have exceeded the maximum allowable number of " + CmdGroup.Text + " variables (" + Conversions.ToString(fGroupMax) + ").", MsgBoxStyle.Exclamation, "Exceeded Max");
						goto end_IL_0001_3;
						IL_00d7:
						num2 = 12;
						DestList = LstGroup;
						Add_ListBox(ref DestList);
						LstGroup = DestList;
						goto end_IL_0001_3;
						IL_0101:
						num2 = 15;
						num7 = LstOther.SelectedIndex;
						goto IL_0111;
						IL_0111:
						num2 = 16;
						if (num7 == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_0124;
						IL_0124:
						num2 = 18;
						num8 = LstOther.SelectedIndices.Count - 1;
						num6 = 0;
						goto IL_01ae;
						IL_01ae:
						if (num6 <= num8)
						{
							goto IL_013f;
						}
						goto IL_01b3;
						IL_01b3:
						num2 = 23;
						LstOther.SelectedItems.Clear();
						goto IL_01c7;
						IL_01c7:
						num2 = 24;
						if (LstOther.Items.Count - 1 > num7)
						{
							goto IL_01e6;
						}
						goto IL_01fc;
						IL_01e6:
						num2 = 25;
						LstOther.SelectedIndex = num7 + 1;
						break;
						IL_01fc:
						num2 = 27;
						if (LstOther.Items.Count <= 0)
						{
							break;
						}
						goto IL_0218;
						IL_0218:
						num2 = 28;
						LstOther.SelectedIndex = 0;
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 30;
					LstOther.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 718;
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
				case 381:
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
							goto IL_007d;
						case 5:
							goto IL_0091;
						case 6:
							goto IL_00a0;
						case 7:
							goto IL_00b3;
						case 8:
							goto IL_00c6;
						case 9:
							goto IL_00d9;
						case 10:
							goto IL_00ed;
						case 11:
							goto IL_0101;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d9:
					num2 = 9;
					LstGroup.Items.Clear();
					goto IL_00ed;
					IL_00ed:
					num2 = 10;
					LstVal.Items.Clear();
					goto IL_0101;
					IL_00c6:
					num2 = 8;
					LstBy.Items.Clear();
					goto IL_00d9;
					IL_0101:
					num2 = 11;
					if (Operators.CompareString(f_WhereMode, "N/A", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (!((LstX.Items.Count > 0) | (LstY.Items.Count > 0) | (LstBy.Items.Count > 0) | (LstGroup.Items.Count > 0) | (LstVal.Items.Count > 0)))
					{
						goto end_IL_0001_3;
					}
					goto IL_007d;
					IL_007d:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear all Column selections?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Clear Selections?");
					goto IL_0091;
					IL_0091:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_00a0;
					IL_00a0:
					num2 = 6;
					LstX.Items.Clear();
					goto IL_00b3;
					IL_00b3:
					num2 = 7;
					LstY.Items.Clear();
					goto IL_00c6;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				Clear_Where_Grid();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 381;
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

	private void cmdExtra_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int index = default(int);
		string text = default(string);
		string left = default(string);
		int num5 = default(int);
		string left2 = default(string);
		int num6 = default(int);
		FrmRWMap frmRWMap = default(FrmRWMap);
		int num7 = default(int);
		int num8 = default(int);
		string text2 = default(string);
		int num9 = default(int);
		string text3 = default(string);
		string text4 = default(string);
		int num10 = default(int);
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
					case 1939:
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
								goto IL_0032;
							case 4:
								goto IL_003b;
							case 5:
								goto IL_0040;
							case 6:
								goto IL_005b;
							case 7:
							case 8:
								goto IL_0077;
							case 9:
								goto IL_0080;
							case 10:
								goto IL_00b2;
							case 11:
								goto IL_00d1;
							case 12:
								goto IL_00e1;
							case 13:
								goto IL_00f1;
							case 14:
								goto IL_010d;
							case 15:
								goto IL_0139;
							case 16:
								goto IL_0148;
							case 17:
								goto IL_0153;
							case 18:
								goto IL_0168;
							case 19:
								goto IL_017d;
							case 20:
								goto IL_0188;
							case 21:
								goto IL_01cc;
							case 22:
								goto IL_01e0;
							case 23:
								goto IL_01f9;
							case 25:
								goto IL_0211;
							case 26:
								goto IL_022e;
							case 27:
								goto IL_0247;
							case 28:
								goto IL_025b;
							case 32:
								goto IL_027a;
							case 33:
								goto IL_0284;
							case 34:
								goto IL_028a;
							case 35:
								goto IL_0290;
							case 36:
								goto IL_0296;
							case 37:
								goto IL_02a0;
							case 38:
								goto IL_02aa;
							case 39:
								goto IL_02b0;
							case 40:
								goto IL_02ba;
							case 41:
								goto IL_02c4;
							case 42:
								goto IL_02e3;
							case 43:
								goto IL_02ed;
							case 45:
								goto IL_0307;
							case 46:
								goto IL_0326;
							case 47:
								goto IL_0330;
							case 44:
							case 48:
							case 50:
							case 51:
								goto IL_0351;
							case 52:
								goto IL_0360;
							case 53:
								goto IL_0379;
							case 54:
								goto IL_0395;
							case 56:
								goto IL_03b0;
							case 57:
								goto IL_03cc;
							case 55:
							case 58:
							case 59:
								goto IL_03e4;
							case 60:
								goto IL_03f7;
							case 61:
								goto IL_0405;
							case 62:
								goto IL_0425;
							case 64:
								goto IL_043f;
							case 63:
							case 65:
							case 66:
								goto IL_044a;
							case 67:
								goto IL_0465;
							case 68:
								goto IL_0476;
							case 69:
								goto IL_0495;
							case 70:
								goto IL_04b3;
							case 71:
								goto IL_04cc;
							case 72:
								goto IL_04d6;
							case 73:
								goto IL_04ef;
							case 74:
							case 76:
							case 77:
								goto IL_0503;
							case 78:
								goto IL_0519;
							case 80:
								goto IL_0524;
							case 79:
							case 81:
							case 82:
								goto IL_052c;
							case 83:
								goto IL_053f;
							case 84:
								goto IL_0558;
							case 86:
								goto IL_0574;
							case 85:
							case 87:
							case 88:
								goto IL_058d;
							case 75:
							case 89:
								goto IL_059f;
							case 90:
								goto IL_05b8;
							case 92:
								goto IL_05cf;
							case 93:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 24:
							case 29:
							case 30:
							case 31:
							case 49:
							case 91:
							case 94:
							case 95:
							case 96:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0558:
						num2 = 84;
						LstX.Items[index] = text;
						goto IL_058d;
						IL_0574:
						num2 = 86;
						LstY.Items[index] = text;
						goto IL_058d;
						IL_053f:
						num2 = 83;
						if (Operators.CompareString(left, "X", TextCompare: false) == 0)
						{
							goto IL_0558;
						}
						goto IL_0574;
						IL_058d:
						num2 = 88;
						num5++;
						goto IL_0596;
						IL_000b:
						num2 = 2;
						if (Operators.CompareString(Strings.LCase(cmdExtra.Text), "custom group", TextCompare: false) == 0)
						{
							goto IL_0032;
						}
						goto IL_027a;
						IL_0032:
						num2 = 3;
						left2 = "";
						goto IL_003b;
						IL_003b:
						num2 = 4;
						num6 = 0;
						goto IL_0040;
						IL_0040:
						num2 = 5;
						if (LstGroup.Items.Count > 0)
						{
							goto IL_005b;
						}
						goto IL_0077;
						IL_005b:
						num2 = 6;
						left2 = Conversions.ToString(LstGroup.Items[0]);
						goto IL_0077;
						IL_0077:
						num2 = 8;
						frmRWMap = new FrmRWMap();
						goto IL_0080;
						IL_0080:
						num2 = 9;
						if (Operators.CompareString(l_Generalval2, "", TextCompare: false) == 0 && LstVal.Items.Count > 0)
						{
							goto IL_00b2;
						}
						goto IL_00d1;
						IL_059f:
						num2 = 89;
						if (Operators.CompareString(left, "X", TextCompare: false) == 0)
						{
							goto IL_05b8;
						}
						goto IL_05cf;
						IL_00b2:
						num2 = 10;
						l_Generalval2 = Conversions.ToString(LstVal.Items[0]);
						goto IL_00d1;
						IL_00d1:
						num2 = 11;
						frmRWMap.f_Column = l_Generalval2;
						goto IL_00e1;
						IL_00e1:
						num2 = 12;
						frmRWMap.f_Value = f_CustomGroup;
						goto IL_00f1;
						IL_00f1:
						num2 = 13;
						num7 = LstColumns.Items.Count - 1;
						num6 = 0;
						goto IL_0142;
						IL_0142:
						if (num6 <= num7)
						{
							goto IL_010d;
						}
						goto IL_0148;
						IL_0148:
						num2 = 16;
						frmRWMap.ShowDialog();
						goto IL_0153;
						IL_0153:
						num2 = 17;
						f_CustomGroup = Strings.Trim(frmRWMap.f_Value);
						goto IL_0168;
						IL_0168:
						num2 = 18;
						l_Generalval2 = Strings.Trim(frmRWMap.f_Column);
						goto IL_017d;
						IL_017d:
						num2 = 19;
						frmRWMap.Dispose();
						goto IL_0188;
						IL_0188:
						num2 = 20;
						if (Operators.CompareString(f_CustomGroup, "", TextCompare: false) != 0 && (Operators.CompareString(left2, "my.group.col.wm", TextCompare: false) != 0 || LstGroup.Items.Count > 1))
						{
							goto IL_01cc;
						}
						goto IL_0211;
						IL_05b8:
						num2 = 90;
						LstX.SelectedItems.Clear();
						goto end_IL_0001_3;
						IL_05cf:
						num2 = 92;
						if (Operators.CompareString(left, "Y", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_01cc:
						num2 = 21;
						LstGroup.Items.Clear();
						goto IL_01e0;
						IL_01e0:
						num2 = 22;
						LstGroup.Items.Add("my.group.col.wm");
						goto IL_01f9;
						IL_01f9:
						num2 = 23;
						Interaction.MsgBox("A custom group was added to the Group list box", MsgBoxStyle.Information, "Custom Group");
						goto end_IL_0001_3;
						IL_0211:
						num2 = 25;
						if (Operators.CompareString(f_CustomGroup, "", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_022e;
						IL_022e:
						num2 = 26;
						if (Operators.CompareString(left2, "my.group.col.wm", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0247;
						IL_0247:
						num2 = 27;
						LstGroup.Items.Clear();
						goto IL_025b;
						IL_025b:
						num2 = 28;
						Interaction.MsgBox("The group list box was cleared as no custom grouping was defined", MsgBoxStyle.Information, "Custom Group");
						goto end_IL_0001_3;
						IL_010d:
						num2 = 14;
						frmRWMap.Cmbcol.Items.Add(RuntimeHelpers.GetObjectValue(LstColumns.Items[num6]));
						goto IL_0139;
						IL_0139:
						num2 = 15;
						num6++;
						goto IL_0142;
						IL_027a:
						num2 = 32;
						left = "Y";
						goto IL_0284;
						IL_0284:
						num2 = 33;
						num5 = 0;
						goto IL_028a;
						IL_028a:
						num2 = 34;
						index = -1;
						goto IL_0290;
						IL_0290:
						num2 = 35;
						num8 = -1;
						goto IL_0296;
						IL_0296:
						num2 = 36;
						text = "";
						goto IL_02a0;
						IL_02a0:
						num2 = 37;
						text2 = "";
						goto IL_02aa;
						IL_02aa:
						num2 = 38;
						num9 = 0;
						goto IL_02b0;
						IL_02b0:
						num2 = 39;
						text3 = "";
						goto IL_02ba;
						IL_02ba:
						num2 = 40;
						text4 = "";
						goto IL_02c4;
						IL_02c4:
						num2 = 41;
						if (Operators.ConditionalCompareObjectEqual(cmdExtra.Tag, "X", TextCompare: false))
						{
							goto IL_02e3;
						}
						goto IL_0307;
						IL_02e3:
						num2 = 42;
						left = "X";
						goto IL_02ed;
						IL_02ed:
						num2 = 43;
						num8 = LstX.SelectedIndices.Count - 1;
						goto IL_0351;
						IL_0307:
						num2 = 45;
						if (!Operators.ConditionalCompareObjectEqual(cmdExtra.Tag, "Y", TextCompare: false))
						{
							goto end_IL_0001_3;
						}
						goto IL_0326;
						IL_0326:
						num2 = 46;
						left = "Y";
						goto IL_0330;
						IL_0330:
						num2 = 47;
						num8 = LstY.SelectedIndices.Count - 1;
						goto IL_0351;
						IL_0351:
						num2 = 51;
						num10 = num8;
						num5 = 0;
						goto IL_0596;
						IL_0596:
						if (num5 <= num10)
						{
							goto IL_0360;
						}
						goto IL_059f;
						IL_0360:
						num2 = 52;
						if (Operators.CompareString(left, "X", TextCompare: false) == 0)
						{
							goto IL_0379;
						}
						goto IL_03b0;
						IL_0379:
						num2 = 53;
						text = Conversions.ToString(LstX.SelectedItems[num5]);
						goto IL_0395;
						IL_0395:
						num2 = 54;
						index = LstX.SelectedIndices[num5];
						goto IL_03e4;
						IL_03b0:
						num2 = 56;
						text = Conversions.ToString(LstY.SelectedItems[num5]);
						goto IL_03cc;
						IL_03cc:
						num2 = 57;
						index = LstY.SelectedIndices[num5];
						goto IL_03e4;
						IL_03e4:
						num2 = 59;
						num9 = Strings.InStrRev(text, " ** ");
						goto IL_03f7;
						IL_03f7:
						num2 = 60;
						if (num9 != 0)
						{
							goto IL_0405;
						}
						goto IL_043f;
						IL_0405:
						num2 = 61;
						text2 = Strings.Trim(Strings.Mid(text, num9 + Strings.Len(" ** ") - 1));
						goto IL_0425;
						IL_0425:
						num2 = 62;
						text = Strings.Trim(Strings.Mid(text, 1, num9 - 1));
						goto IL_044a;
						IL_043f:
						num2 = 64;
						text2 = "";
						goto IL_044a;
						IL_044a:
						num2 = 66;
						num9 = Strings.InStr(cmdExtra.Text, "/");
						goto IL_0465;
						IL_0465:
						num2 = 67;
						if (num9 != 0)
						{
							goto IL_0476;
						}
						goto IL_059f;
						IL_0476:
						num2 = 68;
						text3 = Strings.Trim(Strings.Mid(cmdExtra.Text, 1, num9 - 1));
						goto IL_0495;
						IL_0495:
						num2 = 69;
						text4 = Strings.Trim(Strings.Mid(cmdExtra.Text, num9 + 1));
						goto IL_04b3;
						IL_04b3:
						num2 = 70;
						if (Operators.CompareString(text3, "Asc", TextCompare: false) == 0)
						{
							goto IL_04cc;
						}
						goto IL_04d6;
						IL_04cc:
						num2 = 71;
						text3 = "Ascending";
						goto IL_04d6;
						IL_04d6:
						num2 = 72;
						if (Operators.CompareString(text4, "Desc", TextCompare: false) == 0)
						{
							goto IL_04ef;
						}
						goto IL_0503;
						IL_04ef:
						num2 = 73;
						text4 = "Descending";
						goto IL_0503;
						IL_0503:
						num2 = 77;
						if (Operators.CompareString(text2, text3, TextCompare: false) == 0)
						{
							goto IL_0519;
						}
						goto IL_0524;
						IL_0519:
						num2 = 78;
						text2 = text4;
						goto IL_052c;
						IL_0524:
						num2 = 80;
						text2 = text3;
						goto IL_052c;
						IL_052c:
						num2 = 82;
						text = text + " ** " + text2;
						goto IL_053f;
						end_IL_0001_2:
						break;
					}
					num2 = 93;
					LstY.SelectedItems.Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1939;
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

	private void LstY_DragDrop(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstY;
				GridModule.List_Drag_Drop("DD", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstY = MyListBox;
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

	private void LstY_DragOver(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstY;
				GridModule.List_Drag_Drop("DO", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstY = MyListBox;
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

	private void LstY_MouseDown(object sender, MouseEventArgs e)
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
				ListBox MyListBox = LstY;
				GridModule.List_Drag_Drop("MD", ref MyListBox, ref l_DragSource, ref l_DragIdx);
				LstY = MyListBox;
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

	private void LstBy_DragDrop(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstBy;
				GridModule.List_Drag_Drop("DD", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstBy = MyListBox;
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

	private void LstBy_DragOver(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstBy;
				GridModule.List_Drag_Drop("DO", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstBy = MyListBox;
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

	private void LstBy_MouseDown(object sender, MouseEventArgs e)
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
				ListBox MyListBox = LstBy;
				GridModule.List_Drag_Drop("MD", ref MyListBox, ref l_DragSource, ref l_DragIdx);
				LstBy = MyListBox;
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

	private void LstGroup_DragDrop(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstGroup;
				GridModule.List_Drag_Drop("DD", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstGroup = MyListBox;
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

	private void LstGroup_DragOver(object sender, DragEventArgs e)
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
				ListBox MyListBox = LstGroup;
				GridModule.List_Drag_Drop("DO", ref MyListBox, ref l_DragSource, ref l_DragIdx, e);
				LstGroup = MyListBox;
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

	private void LstGroup_MouseDown(object sender, MouseEventArgs e)
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
				ListBox MyListBox = LstGroup;
				GridModule.List_Drag_Drop("MD", ref MyListBox, ref l_DragSource, ref l_DragIdx);
				LstGroup = MyListBox;
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

	private void CmdColHelp_Click(object sender, EventArgs e)
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
				case 160:
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
							goto IL_003b;
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
					if (!LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary) && Operators.CompareString(f_TemplateType2, "R", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_003b;
					IL_003b:
					num2 = 3;
					Interaction.MsgBox(General_Procedures.Get_UI("_alli_r"), MsgBoxStyle.Information, "Column Patterns");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Interaction.MsgBox(General_Procedures.Get_UI("_alli_"), MsgBoxStyle.Information, "Help on ::_Alli_ Reference");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 160;
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

	private void TxtPerRow_SelectedValueChanged(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyWValue = default(string);
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
				case 228:
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
						case 6:
							goto IL_003d;
						case 7:
							goto IL_0046;
						case 8:
							goto IL_005a;
						case 9:
							goto IL_0066;
						case 10:
							goto IL_0070;
						case 11:
							goto IL_0081;
						case 12:
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0066:
					num2 = 9;
					Clear_Where_Grid();
					goto IL_0070;
					IL_0070:
					num2 = 10;
					Get_Set_Where("I", ref MyWValue);
					goto IL_0081;
					IL_005a:
					num2 = 8;
					if (num5 != 6)
					{
						break;
					}
					goto IL_0066;
					IL_0081:
					num2 = 11;
					Populate_Columns_2();
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyWValue = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if ((Operators.CompareString(f_WhereMode, "4", TextCompare: false) != 0) | IsRecursive)
					{
						goto end_IL_0001_3;
					}
					goto IL_003d;
					IL_003d:
					num2 = 6;
					IsRecursive = true;
					goto IL_0046;
					IL_0046:
					num2 = 7;
					num5 = (int)Interaction.MsgBox("Do you want to select a new table and clear current selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear Selections?");
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				IsRecursive = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 228;
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

	private void cmdBrowse_Click(object sender, EventArgs e)
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
							goto IL_007e;
						case 13:
							goto IL_00a0;
						case 12:
						case 14:
						case 15:
							goto IL_00bf;
						case 16:
							goto IL_00d7;
						case 17:
							goto IL_00ef;
						case 18:
							goto IL_0108;
						case 19:
							goto IL_0121;
						case 20:
							goto IL_012f;
						case 21:
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 23:
						case 24:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0108:
					num2 = 18;
					num5 = (int)Interaction.MsgBox("Do you wish to clear selections before refreshing the form [No]?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear Selections?");
					goto IL_0121;
					IL_0121:
					num2 = 19;
					if (num5 != 6)
					{
						break;
					}
					goto IL_012f;
					IL_00ef:
					num2 = 17;
					if (Operators.CompareString(left, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0108;
					IL_012f:
					num2 = 20;
					lClearData = true;
					break;
					IL_000b:
					num2 = 2;
					text = Strings.Trim(TxtInFile.Text);
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
					if (Operators.CompareString(f_TemplateType2, "HTMLPY", TextCompare: false) == 0)
					{
						goto IL_007e;
					}
					goto IL_00a0;
					IL_007e:
					num2 = 11;
					BuildForm.FileOpenSave("O", text, "csv+p", "Find CSV/TAB/Parquet File", "");
					goto IL_00bf;
					IL_00a0:
					num2 = 13;
					BuildForm.FileOpenSave("O", text, "csv+", "Find CSV/TAB File", "");
					goto IL_00bf;
					IL_00bf:
					num2 = 15;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00d7;
					IL_00d7:
					num2 = 16;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00ef;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				Populate_Columns3(text, 0, LoadOptCols: true, lClearData);
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

	public bool Check_Col_Pattern()
	{
		bool result = false;
		checked
		{
			if ((Operators.CompareString(f_TemplateType2, "R", TextCompare: false) == 0) | LikeOperator.LikeString(f_TemplateType2, "HTML*", CompareMethod.Binary))
			{
				string text = "";
				int num = 0;
				int count = LstColumns.SelectedItems.Count;
				int num2 = count - 1;
				for (num = 0; num <= num2; num++)
				{
					text = Conversions.ToString(LstColumns.SelectedItems[num]);
					if ((Operators.CompareString(text, "StartingWith:", TextCompare: false) == 0) | (Operators.CompareString(text, "EndingWith:", TextCompare: false) == 0) | (Operators.CompareString(text, "Containing:", TextCompare: false) == 0))
					{
						result = true;
						Interaction.MsgBox("You can only specify search patterns for Y variables", MsgBoxStyle.Information, "Invalid");
						break;
					}
				}
			}
			return result;
		}
	}

	private void CmdVal_Click(object sender, EventArgs e)
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
				case 180:
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
							goto IL_0019;
						case 5:
							goto IL_0045;
						case 7:
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Check_Col_Pattern())
					{
						goto end_IL_0001_3;
					}
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (checked(LstColumns.SelectedIndices.Count + LstVal.Items.Count) <= 1)
					{
						break;
					}
					goto IL_0045;
					IL_0045:
					num2 = 5;
					Interaction.MsgBox("You have exceeded the maximum allowable number of Value variables (1).", MsgBoxStyle.Exclamation, "Exceeded Max");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				ListBox DestList = LstVal;
				Add_ListBox(ref DestList);
				LstVal = DestList;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 180;
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
				DataGridView GridY = GridTheme;
				BuildChart.Chart_Clear_Color(ref GridY, 0, 4);
				GridTheme = GridY;
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
				DataGridView GridY = GridTheme;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Chart_Set_Color(ref GridY, ref ColorDialog, 0, 4);
				ColorDialog1 = ColorDialog;
				GridTheme = GridY;
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

	private void cmddown2_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridTheme;
				GridModule.Grid_Down(ref MyGrid);
				GridTheme = MyGrid;
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

	private void cmdUp2_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridTheme;
				GridModule.Grid_Up(ref MyGrid);
				GridTheme = MyGrid;
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

	private void cmdDel2_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowCount = default(int);
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
							goto IL_000b;
						case 3:
							goto IL_0019;
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
					rowCount = GridTheme.RowCount;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					MyGrid = GridTheme;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					GridTheme = MyGrid;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				GridTheme.RowCount = rowCount;
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

	private void cmdClear2_Click(object sender, EventArgs e)
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
				case 134:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear this theme?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Clear Theme?");
					goto IL_0023;
					IL_0023:
					num2 = 4;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_002f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_002f:
					num2 = 5;
					GridTheme.Rows.Clear();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				GridTheme.RowCount = 50;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 134;
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

	private void mnuLoadTheme_Click(object sender, EventArgs e)
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
				case 489:
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
							goto IL_0039;
						case 6:
							goto IL_0055;
						case 7:
							goto IL_006f;
						case 8:
							goto IL_007d;
						case 9:
							goto IL_0092;
						case 11:
						case 12:
						case 13:
							goto IL_00a9;
						case 14:
							goto IL_00bd;
						case 15:
							goto IL_00ce;
						case 16:
							goto IL_00e6;
						case 17:
							goto IL_00f5;
						case 18:
							goto IL_00ff;
						case 19:
							goto IL_0116;
						case 20:
							goto IL_012d;
						case 21:
							goto IL_0144;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 23:
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0116:
					num2 = 19;
					Add_Theme_Element("symbol", text, 1, DoColor: false);
					goto IL_012d;
					IL_012d:
					num2 = 20;
					Add_Theme_Element("linetype", text, 2, DoColor: false);
					goto IL_0144;
					IL_00ff:
					num2 = 18;
					Add_Theme_Element("color", text, 0, DoColor: true);
					goto IL_0116;
					IL_0144:
					num2 = 21;
					Add_Theme_Element("linewidth", text, 3, DoColor: false);
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					BuildForm.FileOpenSave("O", "", "rtheme", "Load an R Theme File", "");
					goto IL_0039;
					IL_0039:
					num2 = 5;
					text = Strings.Trim(MyProject.Forms.FrmMain.CMDialog1Open.FileName);
					goto IL_0055;
					IL_0055:
					num2 = 6;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_006f;
					IL_006f:
					num2 = 7;
					if (AnyTheme())
					{
						goto IL_007d;
					}
					goto IL_00a9;
					IL_007d:
					num2 = 8;
					num5 = (int)Interaction.MsgBox("Loading the new theme will clear previous selections. Do you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Continue?");
					goto IL_0092;
					IL_0092:
					num2 = 9;
					if (num5 == 7)
					{
						goto end_IL_0001_3;
					}
					goto IL_00a9;
					IL_00a9:
					num2 = 13;
					GridTheme.Rows.Clear();
					goto IL_00bd;
					IL_00bd:
					num2 = 14;
					GridTheme.RowCount = 50;
					goto IL_00ce;
					IL_00ce:
					num2 = 15;
					if (Strings.InStr(text, "\\") == 0)
					{
						goto IL_00e6;
					}
					goto IL_00f5;
					IL_00e6:
					num2 = 16;
					text = Globals_Renamed.gQueryDir + text;
					goto IL_00f5;
					IL_00f5:
					num2 = 17;
					f_ThemeFile = text;
					goto IL_00ff;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				Add_Theme_Element("fill", text, 4, DoColor: true);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 489;
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

	public void Add_Theme_Element(string MyElement, string MyFile, int MyCol, bool DoColor, string Mysection = "theme")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		string text2 = default(string);
		string[] array = default(string[]);
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
				case 389:
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
							goto IL_0034;
						case 7:
							goto IL_004f;
						case 8:
							goto IL_0061;
						case 9:
							goto IL_0075;
						case 10:
							goto IL_0084;
						case 11:
							goto IL_009d;
						case 12:
							goto IL_00a4;
						case 13:
							goto IL_00cb;
						case 14:
							goto IL_00d6;
						case 15:
						case 16:
						case 17:
							goto IL_00fc;
						case 18:
							goto IL_010e;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cb:
					num2 = 13;
					if (DoColor)
					{
						goto IL_00d6;
					}
					goto IL_00fc;
					IL_00d6:
					num2 = 14;
					GridTheme[MyCol, num5].Style.BackColor = ColorTranslator.FromHtml(text);
					goto IL_00fc;
					IL_00a4:
					num2 = 12;
					GridTheme.Rows[num5].Cells[MyCol].Value = text;
					goto IL_00cb;
					IL_00fc:
					num2 = 17;
					num6 = checked(num6 + 1);
					goto IL_0105;
					IL_000b:
					num2 = 2;
					num5 = -1;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text2 = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num6 = 0;
					goto IL_001d;
					IL_001d:
					num2 = 5;
					text2 = General_Procedures.Get_Ini_Data(Mysection, MyElement, "", 5000, MyFile);
					goto IL_0034;
					IL_0034:
					num2 = 6;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_004f;
					IL_004f:
					num2 = 7;
					array = Strings.Split(text2, ",");
					goto IL_0061;
					IL_0061:
					num2 = 8;
					num7 = Information.UBound(array);
					num6 = 0;
					goto IL_0105;
					IL_0105:
					if (num6 <= num7)
					{
						goto IL_0075;
					}
					goto IL_010e;
					IL_010e:
					num2 = 18;
					array = null;
					break;
					IL_0075:
					num2 = 9;
					text = Strings.Trim(array[num6]);
					goto IL_0084;
					IL_0084:
					num2 = 10;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_009d;
					}
					goto IL_00fc;
					IL_009d:
					num2 = 11;
					num5 = checked(num5 + 1);
					goto IL_00a4;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				array = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 389;
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

	private void mnuSaveTheme_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string lpString = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string lpKeyName = default(string);
		string lpFileName = default(string);
		int num8 = default(int);
		int num9 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string lpApplicationName;
					int num7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 758:
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
								goto IL_002b;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_003d;
							case 10:
								goto IL_004f;
							case 11:
								goto IL_0072;
							case 12:
								goto IL_0090;
							case 14:
								goto IL_00ae;
							case 15:
								goto IL_00c7;
							case 16:
								goto IL_00d8;
							case 17:
								goto IL_00e3;
							case 18:
								goto IL_00fd;
							case 19:
								goto IL_0108;
							case 21:
								goto IL_0129;
							case 23:
								goto IL_0136;
							case 25:
								goto IL_0143;
							case 27:
								goto IL_0150;
							case 29:
								goto IL_015d;
							case 20:
							case 22:
							case 24:
							case 26:
							case 28:
							case 30:
							case 31:
								goto IL_016a;
							case 32:
								goto IL_0180;
							case 33:
								goto IL_01ac;
							case 34:
								goto IL_01c5;
							case 35:
								goto IL_01d8;
							case 36:
								goto IL_01e4;
							case 37:
								goto IL_01fd;
							case 38:
								goto IL_020a;
							case 39:
							case 40:
								goto IL_0225;
							default:
								goto end_IL_0001;
							case 13:
							case 41:
							case 42:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_01ac:
						num2 = 33;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_01c5;
						}
						goto IL_01d8;
						IL_01c5:
						num2 = 34;
						lpString = lpString + "," + text;
						goto IL_01d8;
						IL_0180:
						num2 = 32;
						text = Conversions.ToString(GridTheme.Rows[num5].Cells[num6].Value);
						goto IL_01ac;
						IL_01d8:
						num2 = 35;
						num5++;
						goto IL_01df;
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
						text = "";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						lpString = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						num7 = 0;
						goto IL_002b;
						IL_002b:
						num2 = 7;
						lpKeyName = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						lpFileName = "";
						goto IL_003d;
						IL_003d:
						num2 = 9;
						if (!AnyTheme())
						{
							goto end_IL_0001_2;
						}
						goto IL_004f;
						IL_004f:
						num2 = 10;
						BuildForm.FileOpenSave("S", f_ThemeFile, "rtheme", "R Theme File", Globals_Renamed.gQueryDir);
						goto IL_0072;
						IL_0072:
						num2 = 11;
						lpFileName = Strings.Trim(MyProject.Forms.FrmMain.CMDialog1Save.FileName);
						goto IL_0090;
						IL_0090:
						num2 = 12;
						if (Operators.CompareString(lpFileName, "CANCEL", TextCompare: false) == 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_00ae;
						IL_00ae:
						num2 = 14;
						if (Strings.InStr(lpFileName, "\\") == 0)
						{
							goto IL_00c7;
						}
						goto IL_00d8;
						IL_00c7:
						num2 = 15;
						lpFileName = Globals_Renamed.gQueryDir + lpFileName;
						goto IL_00d8;
						IL_00d8:
						num2 = 16;
						f_ThemeFile = lpFileName;
						goto IL_00e3;
						IL_00e3:
						num2 = 17;
						num8 = GridTheme.ColumnCount - 1;
						num6 = 0;
						goto IL_022e;
						IL_022e:
						if (num6 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_00fd;
						IL_00fd:
						num2 = 18;
						lpString = "";
						goto IL_0108;
						IL_0108:
						num2 = 19;
						switch (num6)
						{
						case 0:
							break;
						case 1:
							goto IL_0136;
						case 2:
							goto IL_0143;
						case 3:
							goto IL_0150;
						case 4:
							goto IL_015d;
						default:
							goto IL_016a;
						}
						goto IL_0129;
						IL_015d:
						num2 = 29;
						lpKeyName = "fill";
						goto IL_016a;
						IL_0150:
						num2 = 27;
						lpKeyName = "linewidth";
						goto IL_016a;
						IL_0143:
						num2 = 25;
						lpKeyName = "linetype";
						goto IL_016a;
						IL_0136:
						num2 = 23;
						lpKeyName = "symbol";
						goto IL_016a;
						IL_0129:
						num2 = 21;
						lpKeyName = "color";
						goto IL_016a;
						IL_016a:
						num2 = 31;
						num9 = GridTheme.RowCount - 1;
						num5 = 0;
						goto IL_01df;
						IL_01df:
						if (num5 <= num9)
						{
							goto IL_0180;
						}
						goto IL_01e4;
						IL_01e4:
						num2 = 36;
						if (Operators.CompareString(lpString, "", TextCompare: false) != 0)
						{
							goto IL_01fd;
						}
						goto IL_0225;
						IL_01fd:
						num2 = 37;
						lpString = Strings.Mid(lpString, 2);
						goto IL_020a;
						IL_020a:
						num2 = 38;
						lpApplicationName = "theme";
						num7 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpString, ref lpFileName);
						goto IL_0225;
						IL_0225:
						num2 = 40;
						num6++;
						goto IL_022e;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 758;
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

	private void mnuThemeStrip_Click(object sender, EventArgs e)
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
				mnuStdTheme.Text = "Sample Themes";
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

	private void mnuXCompute_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		int num7 = default(int);
		FrmComputedSQL frmComputedSQL = default(FrmComputedSQL);
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
					case 1090:
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
								goto IL_001d;
							case 6:
								goto IL_0092;
							case 7:
								goto IL_009a;
							case 8:
								goto IL_00a6;
							case 9:
								goto IL_00b2;
							case 10:
								goto IL_00bf;
							case 11:
								goto IL_00cc;
							case 12:
								goto IL_00e7;
							case 14:
								goto IL_010e;
							case 15:
								goto IL_0140;
							case 13:
							case 16:
								goto IL_014c;
							case 17:
								goto IL_0168;
							case 18:
								goto IL_017b;
							case 19:
								goto IL_019a;
							case 20:
								goto IL_01b5;
							case 21:
								goto IL_01d0;
							case 22:
								goto IL_01ee;
							case 23:
								goto IL_01fc;
							case 24:
								goto IL_0211;
							case 25:
								goto IL_021f;
							case 26:
							case 27:
							case 28:
							case 29:
								goto IL_023b;
							case 30:
								goto IL_0245;
							case 31:
								goto IL_0250;
							case 32:
								goto IL_025b;
							case 33:
								goto IL_026d;
							case 34:
								goto IL_028c;
							case 35:
								goto IL_02ad;
							case 36:
								goto IL_02c4;
							case 37:
								goto IL_02d8;
							case 38:
								goto IL_02f1;
							case 39:
							case 40:
								goto IL_0310;
							case 41:
								goto IL_031d;
							case 42:
								goto IL_032a;
							case 43:
								goto IL_0333;
							case 44:
								goto IL_0340;
							case 46:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 45:
							case 47:
							case 48:
							case 49:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_02f1:
						num2 = 38;
						f_ComputedX = Globals_Renamed.currtxttmp + ":" + Globals_Renamed.currDataAny;
						goto IL_0310;
						IL_0310:
						num2 = 40;
						Globals_Renamed.currDataAny = "";
						goto IL_031d;
						IL_02d8:
						num2 = 37;
						LstX.Items.Add(Globals_Renamed.currtxttmp);
						goto IL_02f1;
						IL_031d:
						num2 = 41;
						Globals_Renamed.currdatatypetmp = "";
						goto IL_032a;
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
						num6 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						if (!Conversions.ToBoolean(LstX.Items.Count == 0 || (LstX.Items.Count == 1 && Conversions.ToBoolean(LikeOperator.LikeObject(NewLateBinding.LateGet(LstX.Items[0], null, "tolower", new object[0], null, null, null), "r.ce.*", CompareMethod.Binary)))))
						{
							break;
						}
						goto IL_0092;
						IL_0333:
						num2 = 43;
						Globals_Renamed.currtxttmp = "";
						goto IL_0340;
						IL_010e:
						num2 = 14;
						Globals_Renamed.currlisttmp = Conversions.ToString(Operators.ConcatenateObject(Globals_Renamed.currlisttmp + ",", LstColumns.Items[num5]));
						goto IL_0140;
						IL_0140:
						num2 = 15;
						num5++;
						goto IL_0147;
						IL_0340:
						num2 = 44;
						Globals_Renamed.currlisttmp = "";
						goto end_IL_0001_3;
						IL_032a:
						num2 = 42;
						Globals_Renamed.currinttmp = 0;
						goto IL_0333;
						IL_0092:
						num2 = 6;
						Globals_Renamed.currinttmp = 8;
						goto IL_009a;
						IL_009a:
						num2 = 7;
						Globals_Renamed.currtxttmp = "";
						goto IL_00a6;
						IL_00a6:
						num2 = 8;
						Globals_Renamed.currdatatypetmp = "";
						goto IL_00b2;
						IL_00b2:
						num2 = 9;
						Globals_Renamed.currDataAny = "";
						goto IL_00bf;
						IL_00bf:
						num2 = 10;
						Globals_Renamed.currlisttmp = "";
						goto IL_00cc;
						IL_00cc:
						num2 = 11;
						num7 = LstColumns.Items.Count - 1;
						num5 = 0;
						goto IL_0147;
						IL_0147:
						if (num5 <= num7)
						{
							goto IL_00e7;
						}
						goto IL_014c;
						IL_00e7:
						num2 = 12;
						if (!Operators.ConditionalCompareObjectEqual(LstColumns.Items[num5], "StartingWith:", TextCompare: false))
						{
							goto IL_010e;
						}
						goto IL_014c;
						IL_014c:
						num2 = 16;
						if (Operators.CompareString(Globals_Renamed.currlisttmp, "", TextCompare: false) != 0)
						{
							goto IL_0168;
						}
						goto IL_017b;
						IL_0168:
						num2 = 17;
						Globals_Renamed.currlisttmp = Strings.Mid(Globals_Renamed.currlisttmp, 2);
						goto IL_017b;
						IL_017b:
						num2 = 18;
						if (LstX.Items.Count == 1)
						{
							goto IL_019a;
						}
						goto IL_023b;
						IL_019a:
						num2 = 19;
						text = Conversions.ToString(LstX.Items[0]);
						goto IL_01b5;
						IL_01b5:
						num2 = 20;
						if (LikeOperator.LikeString(text.ToLower(), "r.ce.*", CompareMethod.Binary))
						{
							goto IL_01d0;
						}
						goto IL_023b;
						IL_01d0:
						num2 = 21;
						Globals_Renamed.currtxttmp = Conversions.ToString(LstX.Items[0]);
						goto IL_01ee;
						IL_01ee:
						num2 = 22;
						Globals_Renamed.currDataAny = f_ComputedX;
						goto IL_01fc;
						IL_01fc:
						num2 = 23;
						num6 = Strings.InStr(Globals_Renamed.currDataAny, ":");
						goto IL_0211;
						IL_0211:
						num2 = 24;
						if (num6 != 0)
						{
							goto IL_021f;
						}
						goto IL_023b;
						IL_021f:
						num2 = 25;
						Globals_Renamed.currDataAny = Strings.Mid(Globals_Renamed.currDataAny, num6 + 1);
						goto IL_023b;
						IL_023b:
						num2 = 29;
						frmComputedSQL = new FrmComputedSQL();
						goto IL_0245;
						IL_0245:
						num2 = 30;
						frmComputedSQL.ShowDialog();
						goto IL_0250;
						IL_0250:
						num2 = 31;
						frmComputedSQL.Dispose();
						goto IL_025b;
						IL_025b:
						num2 = 32;
						Globals_Renamed.currtxttmp = Strings.Trim(Globals_Renamed.currtxttmp);
						goto IL_026d;
						IL_026d:
						num2 = 33;
						if (Operators.CompareString(Globals_Renamed.currtxttmp, "", TextCompare: false) != 0)
						{
							goto IL_028c;
						}
						goto IL_0310;
						IL_028c:
						num2 = 34;
						if (!LikeOperator.LikeString(Globals_Renamed.currtxttmp.ToLower(), "r.ce.*", CompareMethod.Binary))
						{
							goto IL_02ad;
						}
						goto IL_02c4;
						IL_02ad:
						num2 = 35;
						Globals_Renamed.currtxttmp = "r.ce." + Globals_Renamed.currtxttmp;
						goto IL_02c4;
						IL_02c4:
						num2 = 36;
						LstX.Items.Clear();
						goto IL_02d8;
						end_IL_0001_2:
						break;
					}
					num2 = 46;
					Interaction.MsgBox("You are only allowed to create a single X computed variable. You can use the R paste function to concatenate more than one column.", MsgBoxStyle.Exclamation, "Invalid");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1090;
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

	private void mnuDataLabels_Click(object sender, EventArgs e)
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
				case 109:
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
					if (!mnuDataLabels.Checked)
					{
						break;
					}
					goto IL_001c;
					IL_001c:
					num2 = 3;
					mnuDataLabels.Checked = false;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				mnuDataLabels.Checked = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 109;
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Integrating+R+into+SQLPathFinder+Jobs");
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
					num5 = (int)Interaction.MsgBox("Do you wish to clear selections before refreshing the form [No]?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear Selections?");
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
					text = Strings.Trim(TxtInFile.Text);
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
							goto IL_0019;
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
					LstY.ClearSelected();
					goto IL_0019;
					IL_0019:
					num2 = 3;
					LstBy.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				LstGroup.ClearSelected();
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

	private void LstY_GotFocus(object sender, EventArgs e)
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
							goto IL_0019;
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
					LstX.ClearSelected();
					goto IL_0019;
					IL_0019:
					num2 = 3;
					LstBy.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				LstGroup.ClearSelected();
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
							goto IL_0019;
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
					LstX.ClearSelected();
					goto IL_0019;
					IL_0019:
					num2 = 3;
					LstY.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				LstGroup.ClearSelected();
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
							goto IL_0019;
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
					LstX.ClearSelected();
					goto IL_0019;
					IL_0019:
					num2 = 3;
					LstY.ClearSelected();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				LstBy.ClearSelected();
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

	private void GridTheme_KeyDown(object sender, KeyEventArgs e)
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
				DataGridView MyGrid;
				ContextMenuStrip ContextMenuColEdit;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 484:
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
							goto IL_00b5;
						case 12:
							goto IL_00bb;
						case 13:
							goto IL_00d7;
						case 15:
							goto IL_010d;
						case 14:
						case 16:
						case 17:
							goto IL_0115;
						case 18:
							goto IL_013f;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00bb:
					num2 = 12;
					if (GridTheme.CurrentCell.ColumnIndex == 4)
					{
						goto IL_00d7;
					}
					goto IL_010d;
					IL_00d7:
					num2 = 13;
					num5 = Conversions.ToInteger(Operators.MultiplyObject(NewLateBinding.LateGet(sender, null, "Width", new object[0], null, null, null), 0.75));
					goto IL_0115;
					IL_00b5:
					num2 = 11;
					num5 = -1;
					goto IL_00bb;
					IL_010d:
					num2 = 15;
					num5 = 50;
					goto IL_0115;
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
					IL_0053:
					num2 = 7;
					cmdDel2_Click(cmdDel2, new EventArgs());
					goto IL_0067;
					IL_0067:
					num2 = 8;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0077:
					num2 = 10;
					if ((GridTheme.CurrentCell.ColumnIndex != 0 && GridTheme.CurrentCell.ColumnIndex != 4) || e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_00b5;
					IL_013f:
					num2 = 18;
					MyGrid = (DataGridView)sender;
					ContextMenuColEdit = this.ContextMenuColEdit;
					BuildChart.Chart_Color_MouseUp(ref MyGrid, ref ContextMenuColEdit, 0, 4, num5, num6);
					this.ContextMenuColEdit = ContextMenuColEdit;
					sender = MyGrid;
					break;
					IL_0115:
					num2 = 17;
					num6 = Conversions.ToInteger(Operators.DivideObject(NewLateBinding.LateGet(sender, null, "Height", new object[0], null, null, null), 2));
					goto IL_013f;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 484;
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

	private void GridTheme_MouseDoubleClick(object sender, MouseEventArgs e)
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
					if (GridTheme.CurrentCell.ColumnIndex != 0 && GridTheme.CurrentCell.ColumnIndex != 4)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				DataGridView GridY = GridTheme;
				ContextMenuStrip ContextMenuColEdit = this.ContextMenuColEdit;
				BuildChart.Chart_Color_MouseUp(ref GridY, ref ContextMenuColEdit, 0, 4, e.X, e.Y);
				this.ContextMenuColEdit = ContextMenuColEdit;
				GridTheme = GridY;
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

	private void mnuRefreshTheme_Click(object sender, EventArgs e)
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
					if (Operators.CompareString(mnuStdTheme.Text, "", TextCompare: false) == 0 && Operators.CompareString(mnuStdTheme.Text, "Sample Themes", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Set_Std_Theme(mnuStdTheme.Text);
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

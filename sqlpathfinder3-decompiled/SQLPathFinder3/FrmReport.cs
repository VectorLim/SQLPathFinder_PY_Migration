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
public class FrmReport : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TabRpt")]
	private TabControl _TabRpt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TabMain")]
	private TabPage _TabMain;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowseIn")]
	private Button _CmdBrowseIn;

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
	[AccessedThroughProperty("GridSort")]
	private DataGridView _GridSort;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridRptHdr")]
	private DataGridView _GridRptHdr;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel2")]
	private Button _CmdDel2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown2")]
	private Button _CmdDown2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp2")]
	private Button _CmdUp2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel1")]
	private Button _CmdDel1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown1")]
	private Button _CmdDown1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp1")]
	private Button _CmdUp1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridCols")]
	private DataGridView _GridCols;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridTopOf")]
	private DataGridView _GridTopOf;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdRight4")]
	private Button _CmdRight4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdLeft4")]
	private Button _CmdLeft4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel3")]
	private Button _CmdDel3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown3")]
	private Button _CmdDown3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp3")]
	private Button _CmdUp3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel4")]
	private Button _CmdDel4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown4")]
	private Button _CmdDown4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp4")]
	private Button _CmdUp4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridColRule")]
	private DataGridView _GridColRule;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel5")]
	private Button _CmdDel5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown5")]
	private Button _CmdDown5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp5")]
	private Button _CmdUp5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDel6")]
	private Button _CmdDel6;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown6")]
	private Button _CmdDown6;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp6")]
	private Button _CmdUp6;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridRowRule")]
	private DataGridView _GridRowRule;

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
	[AccessedThroughProperty("cmdbrowsecss")]
	private Button _cmdbrowsecss;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MenuStrip1")]
	private MenuStrip _MenuStrip1;

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
	[AccessedThroughProperty("mnuOpenF")]
	private ToolStripMenuItem _mnuOpenF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveF")]
	private ToolStripMenuItem _mnuSaveF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEnableColSort")]
	private ToolStripMenuItem _mnuEnableColSort;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmbCol")]
	private ComboBox _CmbCol;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdEditVar")]
	private Button _cmdEditVar;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuAtTop")]
	private ToolStripMenuItem _mnuAtTop;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAddR")]
	private Button _cmdAddR;

	public string f_ReportSpec;

	public string fTitle;

	public string f_ReportSpecName;

	private bool IsStartUp;

	private int f_MaxObj;

	private int f_MinCols;

	private const int fStartAtBotOf = 4;

	private Globals_Renamed.Report_Format_Type[] CSSObj;

	private const int ColCR_fc = 5;

	private const int ColRR_fc = 4;

	private const string MyHTMLOutFile = "SQLPathFinder.htm";

	private short f_Row;

	private short f_Col;

	private short CMbIdx;

	private const short cGridSortR = 15;

	private const short cGridRptHdrR = 10;

	private const short cGridColRuleR = 100;

	internal virtual TabControl TabRpt
	{
		[CompilerGenerated]
		get
		{
			return _TabRpt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TabRpt_Click;
			TabControl tabControl = _TabRpt;
			if (tabControl != null)
			{
				tabControl.Click -= value2;
			}
			_TabRpt = value;
			tabControl = _TabRpt;
			if (tabControl != null)
			{
				tabControl.Click += value2;
			}
		}
	}

	internal virtual TabPage TabMain
	{
		[CompilerGenerated]
		get
		{
			return _TabMain;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TabMain_Enter;
			TabPage tabPage = _TabMain;
			if (tabPage != null)
			{
				tabPage.Enter -= value2;
			}
			_TabMain = value;
			tabPage = _TabMain;
			if (tabPage != null)
			{
				tabPage.Enter += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TabCols")]
	internal virtual TabPage TabCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblCSVIn")]
	internal virtual Label LblCSVIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtCSVIn")]
	internal virtual TextBox TxtCSVIn
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

	internal virtual DataGridView GridSort
	{
		[CompilerGenerated]
		get
		{
			return _GridSort;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellValidatingEventHandler value2 = GridSort_CellValidating;
			KeyEventHandler value3 = GridSort_KeyDown;
			DataGridView dataGridView = _GridSort;
			if (dataGridView != null)
			{
				dataGridView.CellValidating -= value2;
				dataGridView.KeyDown -= value3;
			}
			_GridSort = value;
			dataGridView = _GridSort;
			if (dataGridView != null)
			{
				dataGridView.CellValidating += value2;
				dataGridView.KeyDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("LblSortCols")]
	internal virtual Label LblSortCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHdr")]
	internal virtual Label LblHdr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHeader")]
	internal virtual Label LblHeader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridRptHdr
	{
		[CompilerGenerated]
		get
		{
			return _GridRptHdr;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = GridRptHdr_KeyDown;
			DataGridView dataGridView = _GridRptHdr;
			if (dataGridView != null)
			{
				dataGridView.KeyDown -= value2;
			}
			_GridRptHdr = value;
			dataGridView = _GridRptHdr;
			if (dataGridView != null)
			{
				dataGridView.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TabRules")]
	internal virtual TabPage TabRules
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdDel2
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel2_Click;
			Button button = _CmdDel2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel2 = value;
			button = _CmdDel2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown2
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown2_Click;
			Button button = _CmdDown2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown2 = value;
			button = _CmdDown2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp2
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp2_Click;
			Button button = _CmdUp2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp2 = value;
			button = _CmdUp2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDel1
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel1_Click;
			Button button = _CmdDel1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel1 = value;
			button = _CmdDel1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown1
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown1_Click;
			Button button = _CmdDown1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown1 = value;
			button = _CmdDown1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp1
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp1_Click;
			Button button = _CmdUp1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp1 = value;
			button = _CmdUp1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridCols
	{
		[CompilerGenerated]
		get
		{
			return _GridCols;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridCols_CellContentDoubleClick;
			DataGridViewCellMouseEventHandler value3 = GridCols_ColumnHeaderMouseDoubleClick;
			EventHandler value4 = GridCols_CurrentCellDirtyStateChanged;
			KeyEventHandler value5 = GridCols_KeyDown;
			DataGridView dataGridView = _GridCols;
			if (dataGridView != null)
			{
				dataGridView.CellContentDoubleClick -= value2;
				dataGridView.ColumnHeaderMouseDoubleClick -= value3;
				dataGridView.CurrentCellDirtyStateChanged -= value4;
				dataGridView.KeyDown -= value5;
			}
			_GridCols = value;
			dataGridView = _GridCols;
			if (dataGridView != null)
			{
				dataGridView.CellContentDoubleClick += value2;
				dataGridView.ColumnHeaderMouseDoubleClick += value3;
				dataGridView.CurrentCellDirtyStateChanged += value4;
				dataGridView.KeyDown += value5;
			}
		}
	}

	internal virtual DataGridView GridTopOf
	{
		[CompilerGenerated]
		get
		{
			return _GridTopOf;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellMouseEventHandler value2 = GridTopOf_RowHeaderMouseDoubleClick;
			EventHandler value3 = GridTopOf_RowHeadersWidthChanged;
			KeyEventHandler value4 = GridTopOf_KeyDown;
			DataGridView dataGridView = _GridTopOf;
			if (dataGridView != null)
			{
				dataGridView.RowHeaderMouseDoubleClick -= value2;
				dataGridView.RowHeadersWidthChanged -= value3;
				dataGridView.KeyDown -= value4;
			}
			_GridTopOf = value;
			dataGridView = _GridTopOf;
			if (dataGridView != null)
			{
				dataGridView.RowHeaderMouseDoubleClick += value2;
				dataGridView.RowHeadersWidthChanged += value3;
				dataGridView.KeyDown += value4;
			}
		}
	}

	[field: AccessedThroughProperty("LblTopOf")]
	internal virtual Label LblTopOf
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblCols")]
	internal virtual Label LblCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdRight4
	{
		[CompilerGenerated]
		get
		{
			return _CmdRight4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdRight4_Click;
			Button button = _CmdRight4;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdRight4 = value;
			button = _CmdRight4;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdLeft4
	{
		[CompilerGenerated]
		get
		{
			return _CmdLeft4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdLeft4_Click;
			Button button = _CmdLeft4;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdLeft4 = value;
			button = _CmdLeft4;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDel3
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel3_Click;
			Button button = _CmdDel3;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel3 = value;
			button = _CmdDel3;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown3
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown3_Click;
			Button button = _CmdDown3;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown3 = value;
			button = _CmdDown3;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp3
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp3_Click;
			Button button = _CmdUp3;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp3 = value;
			button = _CmdUp3;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDel4
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel4_Click;
			Button button = _CmdDel4;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel4 = value;
			button = _CmdDel4;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown4
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown4_Click;
			Button button = _CmdDown4;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown4 = value;
			button = _CmdDown4;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp4
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp4_Click;
			Button button = _CmdUp4;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp4 = value;
			button = _CmdUp4;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LblRuleColHdr")]
	internal virtual Label LblRuleColHdr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridColRule
	{
		[CompilerGenerated]
		get
		{
			return _GridColRule;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridColRule_CellDoubleClick;
			EventHandler value3 = GridColRule_Click;
			ScrollEventHandler value4 = GridColRule_Scroll;
			KeyEventHandler value5 = GridColRule_KeyDown;
			DataGridView dataGridView = _GridColRule;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick -= value2;
				dataGridView.Click -= value3;
				dataGridView.Scroll -= value4;
				dataGridView.KeyDown -= value5;
			}
			_GridColRule = value;
			dataGridView = _GridColRule;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick += value2;
				dataGridView.Click += value3;
				dataGridView.Scroll += value4;
				dataGridView.KeyDown += value5;
			}
		}
	}

	internal virtual Button CmdDel5
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel5_Click;
			Button button = _CmdDel5;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel5 = value;
			button = _CmdDel5;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown5
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown5_Click;
			Button button = _CmdDown5;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown5 = value;
			button = _CmdDown5;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp5
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp5_Click;
			Button button = _CmdUp5;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp5 = value;
			button = _CmdUp5;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDel6
	{
		[CompilerGenerated]
		get
		{
			return _CmdDel6;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDel6_Click;
			Button button = _CmdDel6;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDel6 = value;
			button = _CmdDel6;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDown6
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown6;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown6_Click;
			Button button = _CmdDown6;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown6 = value;
			button = _CmdDown6;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdUp6
	{
		[CompilerGenerated]
		get
		{
			return _CmdUp6;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp6_Click;
			Button button = _CmdUp6;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdUp6 = value;
			button = _CmdUp6;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual DataGridView GridRowRule
	{
		[CompilerGenerated]
		get
		{
			return _GridRowRule;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridRowRule_CellDoubleClick;
			EventHandler value3 = GridRowRule_Click;
			ScrollEventHandler value4 = GridRowRule_Scroll;
			KeyEventHandler value5 = GridRowRule_KeyDown;
			DataGridView dataGridView = _GridRowRule;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick -= value2;
				dataGridView.Click -= value3;
				dataGridView.Scroll -= value4;
				dataGridView.KeyDown -= value5;
			}
			_GridRowRule = value;
			dataGridView = _GridRowRule;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick += value2;
				dataGridView.Click += value3;
				dataGridView.Scroll += value4;
				dataGridView.KeyDown += value5;
			}
		}
	}

	[field: AccessedThroughProperty("LblRulRowHdr")]
	internal virtual Label LblRulRowHdr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuRpt")]
	internal virtual ToolStrip mnuRpt
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

	[field: AccessedThroughProperty("ColTopOfText")]
	internal virtual DataGridViewTextBoxColumn ColTopOfText
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdbrowsecss
	{
		[CompilerGenerated]
		get
		{
			return _cmdbrowsecss;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdbrowsecss_Click;
			Button button = _cmdbrowsecss;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdbrowsecss = value;
			button = _cmdbrowsecss;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Txtcss")]
	internal virtual TextBox Txtcss
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblCSS")]
	internal virtual Label lblCSS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MenuStrip MenuStrip1
	{
		[CompilerGenerated]
		get
		{
			return _MenuStrip1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MenuStrip1_Click;
			MenuStrip menuStrip = _MenuStrip1;
			if (menuStrip != null)
			{
				menuStrip.Click -= value2;
			}
			_MenuStrip1 = value;
			menuStrip = _MenuStrip1;
			if (menuStrip != null)
			{
				menuStrip.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuFile")]
	internal virtual ToolStripMenuItem mnuFile
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

	[field: AccessedThroughProperty("FileExternalToolStripMenuItem")]
	internal virtual ToolStripMenuItem FileExternalToolStripMenuItem
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

	[field: AccessedThroughProperty("ColLeft")]
	internal virtual DataGridViewTextBoxColumn ColLeft
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

	[field: AccessedThroughProperty("FontDialog1")]
	internal virtual FontDialog FontDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblColSpan")]
	internal virtual Label LblColSpan
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbColSpan")]
	internal virtual ComboBox CmbColSpan
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuRptOptions")]
	internal virtual ToolStripMenuItem mnuRptOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuEnableColSort
	{
		[CompilerGenerated]
		get
		{
			return _mnuEnableColSort;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuEnableColSort_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuEnableColSort;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuEnableColSort = value;
			toolStripMenuItem = _mnuEnableColSort;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuEnableColDrillDown")]
	internal virtual ToolStripMenuItem mnuEnableColDrillDown
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuEnableDynFilter")]
	internal virtual ToolStripMenuItem mnuEnableDynFilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ComboBox CmbCol
	{
		[CompilerGenerated]
		get
		{
			return _CmbCol;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = CmbCol_KeyPress;
			EventHandler value3 = CmbCol_Leave;
			ComboBox comboBox = _CmbCol;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
				comboBox.Leave -= value3;
			}
			_CmbCol = value;
			comboBox = _CmbCol;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
				comboBox.Leave += value3;
			}
		}
	}

	internal virtual Button cmdEditVar
	{
		[CompilerGenerated]
		get
		{
			return _cmdEditVar;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdeditvar_Click;
			Button button = _cmdEditVar;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdEditVar = value;
			button = _cmdEditVar;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColRuleRTest")]
	internal virtual DataGridViewTextBoxColumn ColRuleRTest
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleROpr")]
	internal virtual DataGridViewComboBoxColumn ColRuleROpr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRVal")]
	internal virtual DataGridViewTextBoxColumn ColRuleRVal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRBC")]
	internal virtual DataGridViewTextBoxColumn ColRuleRBC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRColor")]
	internal virtual DataGridViewTextBoxColumn ColRuleRColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRFF")]
	internal virtual DataGridViewTextBoxColumn ColRuleRFF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRFSz")]
	internal virtual DataGridViewTextBoxColumn ColRuleRFSz
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRFS")]
	internal virtual DataGridViewTextBoxColumn ColRuleRFS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRFW")]
	internal virtual DataGridViewTextBoxColumn ColRuleRFW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRTA")]
	internal virtual DataGridViewComboBoxColumn ColRuleRTA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRTD")]
	internal virtual DataGridViewTextBoxColumn ColRuleRTD
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleRVA")]
	internal virtual DataGridViewComboBoxColumn ColRuleRVA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCDisplay")]
	internal virtual DataGridViewTextBoxColumn ColRuleCDisplay
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCTest")]
	internal virtual DataGridViewTextBoxColumn ColRuleCTest
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCOpr")]
	internal virtual DataGridViewComboBoxColumn ColRuleCOpr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCVal")]
	internal virtual DataGridViewTextBoxColumn ColRuleCVal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCBC")]
	internal virtual DataGridViewTextBoxColumn ColRuleCBC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCColor")]
	internal virtual DataGridViewTextBoxColumn ColRuleCColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCFF")]
	internal virtual DataGridViewTextBoxColumn ColRuleCFF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCFSz")]
	internal virtual DataGridViewTextBoxColumn ColRuleCFSz
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCFS")]
	internal virtual DataGridViewTextBoxColumn ColRuleCFS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCFW")]
	internal virtual DataGridViewTextBoxColumn ColRuleCFW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCTA")]
	internal virtual DataGridViewComboBoxColumn ColRuleCTA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCTD")]
	internal virtual DataGridViewTextBoxColumn ColRuleCTD
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRuleCVA")]
	internal virtual DataGridViewComboBoxColumn ColRuleCVA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColName")]
	internal virtual DataGridViewComboBoxColumn ColName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColSort")]
	internal virtual DataGridViewComboBoxColumn ColSort
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColRS")]
	internal virtual DataGridViewComboBoxColumn ColRS
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuAtTop
	{
		[CompilerGenerated]
		get
		{
			return _mnuAtTop;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuAtTop_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuAtTop;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuAtTop = value;
			toolStripMenuItem = _mnuAtTop;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
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

	[field: AccessedThroughProperty("mnuPreProc")]
	internal virtual ToolStripMenuItem mnuPreProc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColCol")]
	internal virtual DataGridViewComboBoxColumn ColCol
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

	[field: AccessedThroughProperty("ColAlign")]
	internal virtual DataGridViewComboBoxColumn ColAlign
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColWSF")]
	internal virtual DataGridViewTextBoxColumn ColWSF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot1")]
	internal virtual DataGridViewComboBoxColumn ColAtBot1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot2")]
	internal virtual DataGridViewComboBoxColumn ColAtBot2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot3")]
	internal virtual DataGridViewComboBoxColumn ColAtBot3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot4")]
	internal virtual DataGridViewComboBoxColumn ColAtBot4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot5")]
	internal virtual DataGridViewComboBoxColumn ColAtBot5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot6")]
	internal virtual DataGridViewComboBoxColumn ColAtBot6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot7")]
	internal virtual DataGridViewComboBoxColumn ColAtBot7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot8")]
	internal virtual DataGridViewComboBoxColumn ColAtBot8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColAtBot9")]
	internal virtual DataGridViewComboBoxColumn ColAtBot9
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmReport()
	{
		base.FormClosing += FrmReport_FormClosing;
		base.Load += FrmReport_Load;
		base.Resize += FrmReport_Resize;
		f_ReportSpec = "";
		fTitle = "untitled.vgs";
		f_ReportSpecName = "untitled";
		IsStartUp = true;
		f_MaxObj = 0;
		f_MinCols = 0;
		CSSObj = new Globals_Renamed.Report_Format_Type[501];
		f_Row = -1;
		f_Col = -1;
		CMbIdx = -1;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmReport));
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		this.TabRpt = new System.Windows.Forms.TabControl();
		this.TabMain = new System.Windows.Forms.TabPage();
		this.GridRptHdr = new System.Windows.Forms.DataGridView();
		this.ColLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmdEditVar = new System.Windows.Forms.Button();
		this.cmdbrowsecss = new System.Windows.Forms.Button();
		this.Txtcss = new System.Windows.Forms.TextBox();
		this.lblCSS = new System.Windows.Forms.Label();
		this.CmdDel2 = new System.Windows.Forms.Button();
		this.CmdDown2 = new System.Windows.Forms.Button();
		this.CmdUp2 = new System.Windows.Forms.Button();
		this.CmdDel1 = new System.Windows.Forms.Button();
		this.CmdDown1 = new System.Windows.Forms.Button();
		this.CmdUp1 = new System.Windows.Forms.Button();
		this.LblHeader = new System.Windows.Forms.Label();
		this.LblHdr = new System.Windows.Forms.Label();
		this.LblSortCols = new System.Windows.Forms.Label();
		this.GridSort = new System.Windows.Forms.DataGridView();
		this.ColName = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColSort = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColRS = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.CmdBrowseIn = new System.Windows.Forms.Button();
		this.TxtCSVIn = new System.Windows.Forms.TextBox();
		this.LblCSVIn = new System.Windows.Forms.Label();
		this.TabCols = new System.Windows.Forms.TabPage();
		this.cmdAddR = new System.Windows.Forms.Button();
		this.LblColSpan = new System.Windows.Forms.Label();
		this.CmbColSpan = new System.Windows.Forms.ComboBox();
		this.CmdRight4 = new System.Windows.Forms.Button();
		this.CmdLeft4 = new System.Windows.Forms.Button();
		this.CmdDel3 = new System.Windows.Forms.Button();
		this.CmdDown3 = new System.Windows.Forms.Button();
		this.CmdUp3 = new System.Windows.Forms.Button();
		this.CmdDel4 = new System.Windows.Forms.Button();
		this.CmdDown4 = new System.Windows.Forms.Button();
		this.CmdUp4 = new System.Windows.Forms.Button();
		this.LblCols = new System.Windows.Forms.Label();
		this.LblTopOf = new System.Windows.Forms.Label();
		this.GridTopOf = new System.Windows.Forms.DataGridView();
		this.ColTopOfText = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GridCols = new System.Windows.Forms.DataGridView();
		this.ColCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColHdr = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColAlign = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColWSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColAtBot1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColAtBot9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.TabRules = new System.Windows.Forms.TabPage();
		this.CmbCol = new System.Windows.Forms.ComboBox();
		this.LblRulRowHdr = new System.Windows.Forms.Label();
		this.GridRowRule = new System.Windows.Forms.DataGridView();
		this.ColRuleRTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleROpr = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColRuleRVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRBC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRFSz = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRFW = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRTA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColRuleRTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleRVA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.LblRuleColHdr = new System.Windows.Forms.Label();
		this.GridColRule = new System.Windows.Forms.DataGridView();
		this.ColRuleCDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCOpr = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColRuleCVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCBC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCFSz = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCFW = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCTA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColRuleCTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColRuleCVA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.CmdDel6 = new System.Windows.Forms.Button();
		this.CmdDown6 = new System.Windows.Forms.Button();
		this.CmdUp6 = new System.Windows.Forms.Button();
		this.CmdDel5 = new System.Windows.Forms.Button();
		this.CmdDown5 = new System.Windows.Forms.Button();
		this.CmdUp5 = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.mnuRpt = new System.Windows.Forms.ToolStrip();
		this.mnuNew = new System.Windows.Forms.ToolStripButton();
		this.mnuSave = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRun = new System.Windows.Forms.ToolStripButton();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuNewQ = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveQ = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRunQ = new System.Windows.Forms.ToolStripMenuItem();
		this.FileExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOpenF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRptOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEnableColDrillDown = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEnableColSort = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEnableDynFilter = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAtTop = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPreProc = new System.Windows.Forms.ToolStripMenuItem();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.FontDialog1 = new System.Windows.Forms.FontDialog();
		this.TabRpt.SuspendLayout();
		this.TabMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridRptHdr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridSort).BeginInit();
		this.TabCols.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridTopOf).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridCols).BeginInit();
		this.TabRules.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridRowRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridColRule).BeginInit();
		this.mnuRpt.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TabRpt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabRpt.Controls.Add(this.TabMain);
		this.TabRpt.Controls.Add(this.TabCols);
		this.TabRpt.Controls.Add(this.TabRules);
		this.TabRpt.Location = new System.Drawing.Point(0, 64);
		this.TabRpt.Margin = new System.Windows.Forms.Padding(4);
		this.TabRpt.Name = "TabRpt";
		this.TabRpt.SelectedIndex = 0;
		this.TabRpt.Size = new System.Drawing.Size(995, 678);
		this.TabRpt.TabIndex = 0;
		this.TabMain.Controls.Add(this.GridRptHdr);
		this.TabMain.Controls.Add(this.cmdEditVar);
		this.TabMain.Controls.Add(this.cmdbrowsecss);
		this.TabMain.Controls.Add(this.Txtcss);
		this.TabMain.Controls.Add(this.lblCSS);
		this.TabMain.Controls.Add(this.CmdDel2);
		this.TabMain.Controls.Add(this.CmdDown2);
		this.TabMain.Controls.Add(this.CmdUp2);
		this.TabMain.Controls.Add(this.CmdDel1);
		this.TabMain.Controls.Add(this.CmdDown1);
		this.TabMain.Controls.Add(this.CmdUp1);
		this.TabMain.Controls.Add(this.LblHeader);
		this.TabMain.Controls.Add(this.LblHdr);
		this.TabMain.Controls.Add(this.LblSortCols);
		this.TabMain.Controls.Add(this.GridSort);
		this.TabMain.Controls.Add(this.CmdBrowseIn);
		this.TabMain.Controls.Add(this.TxtCSVIn);
		this.TabMain.Controls.Add(this.LblCSVIn);
		this.TabMain.Location = new System.Drawing.Point(4, 25);
		this.TabMain.Margin = new System.Windows.Forms.Padding(4);
		this.TabMain.Name = "TabMain";
		this.TabMain.Padding = new System.Windows.Forms.Padding(4);
		this.TabMain.Size = new System.Drawing.Size(987, 649);
		this.TabMain.TabIndex = 0;
		this.TabMain.Text = "Main";
		this.TabMain.UseVisualStyleBackColor = true;
		this.GridRptHdr.AllowUserToAddRows = false;
		this.GridRptHdr.AllowUserToDeleteRows = false;
		this.GridRptHdr.AllowUserToResizeColumns = false;
		this.GridRptHdr.AllowUserToResizeRows = false;
		this.GridRptHdr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridRptHdr.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridRptHdr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridRptHdr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridRptHdr.Columns.AddRange(this.ColLeft);
		this.GridRptHdr.Location = new System.Drawing.Point(17, 497);
		this.GridRptHdr.Margin = new System.Windows.Forms.Padding(4);
		this.GridRptHdr.Name = "GridRptHdr";
		this.GridRptHdr.RowHeadersWidth = 55;
		this.GridRptHdr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GridRptHdr.Size = new System.Drawing.Size(873, 105);
		this.GridRptHdr.StandardTab = true;
		this.GridRptHdr.TabIndex = 10;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.ColLeft.DefaultCellStyle = dataGridViewCellStyle2;
		this.ColLeft.HeaderText = "Report Header";
		this.ColLeft.MinimumWidth = 6;
		this.ColLeft.Name = "ColLeft";
		this.ColLeft.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColLeft.Width = 575;
		this.cmdEditVar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdEditVar.ImageIndex = 8;
		this.cmdEditVar.Location = new System.Drawing.Point(903, 85);
		this.cmdEditVar.Margin = new System.Windows.Forms.Padding(4);
		this.cmdEditVar.Name = "cmdEditVar";
		this.cmdEditVar.Size = new System.Drawing.Size(72, 31);
		this.cmdEditVar.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.cmdEditVar, "Change to a Global or Macro Variable");
		this.cmdEditVar.UseVisualStyleBackColor = true;
		this.cmdbrowsecss.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdbrowsecss.ImageIndex = 3;
		this.cmdbrowsecss.Location = new System.Drawing.Point(903, 137);
		this.cmdbrowsecss.Margin = new System.Windows.Forms.Padding(4);
		this.cmdbrowsecss.Name = "cmdbrowsecss";
		this.cmdbrowsecss.Size = new System.Drawing.Size(71, 31);
		this.cmdbrowsecss.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.cmdbrowsecss, "Choose a Report Style Sheet");
		this.cmdbrowsecss.UseVisualStyleBackColor = true;
		this.Txtcss.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtcss.Location = new System.Drawing.Point(151, 139);
		this.Txtcss.Margin = new System.Windows.Forms.Padding(4);
		this.Txtcss.Name = "Txtcss";
		this.Txtcss.Size = new System.Drawing.Size(739, 22);
		this.Txtcss.TabIndex = 3;
		this.lblCSS.AutoSize = true;
		this.lblCSS.Location = new System.Drawing.Point(13, 143);
		this.lblCSS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblCSS.Name = "lblCSS";
		this.lblCSS.Size = new System.Drawing.Size(84, 17);
		this.lblCSS.TabIndex = 15;
		this.lblCSS.Text = "Style Sheet:";
		this.CmdDel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel2.ImageIndex = 2;
		this.CmdDel2.Location = new System.Drawing.Point(903, 567);
		this.CmdDel2.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel2.Name = "CmdDel2";
		this.CmdDel2.Size = new System.Drawing.Size(72, 31);
		this.CmdDel2.TabIndex = 13;
		this.ToolTip1.SetToolTip(this.CmdDel2, "Delete row");
		this.CmdDel2.UseVisualStyleBackColor = true;
		this.CmdDown2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown2.ImageIndex = 1;
		this.CmdDown2.Location = new System.Drawing.Point(903, 527);
		this.CmdDown2.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown2.Name = "CmdDown2";
		this.CmdDown2.Size = new System.Drawing.Size(72, 31);
		this.CmdDown2.TabIndex = 12;
		this.ToolTip1.SetToolTip(this.CmdDown2, "Move row down");
		this.CmdDown2.UseVisualStyleBackColor = true;
		this.CmdUp2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp2.ImageIndex = 0;
		this.CmdUp2.Location = new System.Drawing.Point(903, 497);
		this.CmdUp2.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp2.Name = "CmdUp2";
		this.CmdUp2.Size = new System.Drawing.Size(72, 31);
		this.CmdUp2.TabIndex = 11;
		this.ToolTip1.SetToolTip(this.CmdUp2, "Move row up");
		this.CmdUp2.UseVisualStyleBackColor = true;
		this.CmdDel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel1.ImageIndex = 2;
		this.CmdDel1.Location = new System.Drawing.Point(903, 329);
		this.CmdDel1.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel1.Name = "CmdDel1";
		this.CmdDel1.Size = new System.Drawing.Size(72, 31);
		this.CmdDel1.TabIndex = 9;
		this.ToolTip1.SetToolTip(this.CmdDel1, "Delete row");
		this.CmdDel1.UseVisualStyleBackColor = true;
		this.CmdDown1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown1.ImageIndex = 1;
		this.CmdDown1.Location = new System.Drawing.Point(903, 293);
		this.CmdDown1.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown1.Name = "CmdDown1";
		this.CmdDown1.Size = new System.Drawing.Size(72, 31);
		this.CmdDown1.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.CmdDown1, "Move row down");
		this.CmdDown1.UseVisualStyleBackColor = true;
		this.CmdUp1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp1.ImageIndex = 0;
		this.CmdUp1.Location = new System.Drawing.Point(903, 263);
		this.CmdUp1.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp1.Name = "CmdUp1";
		this.CmdUp1.Size = new System.Drawing.Size(72, 31);
		this.CmdUp1.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.CmdUp1, "Move row up");
		this.CmdUp1.UseVisualStyleBackColor = true;
		this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHeader.Location = new System.Drawing.Point(13, 438);
		this.LblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHeader.Name = "LblHeader";
		this.LblHeader.Size = new System.Drawing.Size(863, 55);
		this.LblHeader.TabIndex = 10;
		this.LblHeader.Text = resources.GetString("LblHeader.Text");
		this.LblHdr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHdr.Location = new System.Drawing.Point(13, 14);
		this.LblHdr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHdr.Name = "LblHdr";
		this.LblHdr.Size = new System.Drawing.Size(956, 39);
		this.LblHdr.TabIndex = 8;
		this.LblHdr.Text = "Specify the input CSV file to report, and a Report style sheet. Also choose the Sort columns for the report, and enter a report header";
		this.LblSortCols.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblSortCols.Location = new System.Drawing.Point(13, 219);
		this.LblSortCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblSortCols.Name = "LblSortCols";
		this.LblSortCols.Size = new System.Drawing.Size(877, 40);
		this.LblSortCols.TabIndex = 7;
		this.LblSortCols.Text = "Select Report Sort Columns and order and whether to combine adjacent cells in the same column qith the same value (RowSpan)";
		this.GridSort.AllowUserToAddRows = false;
		this.GridSort.AllowUserToDeleteRows = false;
		this.GridSort.AllowUserToResizeColumns = false;
		this.GridSort.AllowUserToResizeRows = false;
		this.GridSort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridSort.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridSort.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.GridSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridSort.Columns.AddRange(this.ColName, this.ColSort, this.ColRS);
		this.GridSort.Location = new System.Drawing.Point(17, 263);
		this.GridSort.Margin = new System.Windows.Forms.Padding(4);
		this.GridSort.Name = "GridSort";
		this.GridSort.RowHeadersWidth = 55;
		this.GridSort.Size = new System.Drawing.Size(873, 150);
		this.GridSort.StandardTab = true;
		this.GridSort.TabIndex = 6;
		this.ColName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColName.DisplayStyleForCurrentCellOnly = true;
		this.ColName.DropDownWidth = 200;
		this.ColName.HeaderText = "Column";
		this.ColName.MinimumWidth = 6;
		this.ColName.Name = "ColName";
		this.ColName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColName.Width = 235;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColSort.DefaultCellStyle = dataGridViewCellStyle4;
		this.ColSort.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColSort.DisplayStyleForCurrentCellOnly = true;
		this.ColSort.HeaderText = "Sort Order";
		this.ColSort.Items.AddRange("", "Asc", "Desc");
		this.ColSort.MinimumWidth = 6;
		this.ColSort.Name = "ColSort";
		this.ColSort.Width = 125;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRS.DefaultCellStyle = dataGridViewCellStyle5;
		this.ColRS.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRS.DisplayStyleForCurrentCellOnly = true;
		this.ColRS.HeaderText = "Row Span";
		this.ColRS.Items.AddRange("", "Y", "N");
		this.ColRS.MinimumWidth = 6;
		this.ColRS.Name = "ColRS";
		this.ColRS.Width = 125;
		this.CmdBrowseIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowseIn.ImageIndex = 3;
		this.CmdBrowseIn.Location = new System.Drawing.Point(903, 53);
		this.CmdBrowseIn.Margin = new System.Windows.Forms.Padding(4);
		this.CmdBrowseIn.Name = "CmdBrowseIn";
		this.CmdBrowseIn.Size = new System.Drawing.Size(72, 31);
		this.CmdBrowseIn.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.CmdBrowseIn, "Bowse for a CSV File");
		this.CmdBrowseIn.UseVisualStyleBackColor = true;
		this.TxtCSVIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCSVIn.Location = new System.Drawing.Point(151, 70);
		this.TxtCSVIn.Margin = new System.Windows.Forms.Padding(4);
		this.TxtCSVIn.Name = "TxtCSVIn";
		this.TxtCSVIn.ReadOnly = true;
		this.TxtCSVIn.Size = new System.Drawing.Size(739, 22);
		this.TxtCSVIn.TabIndex = 0;
		this.LblCSVIn.AutoSize = true;
		this.LblCSVIn.Location = new System.Drawing.Point(13, 71);
		this.LblCSVIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCSVIn.Name = "LblCSVIn";
		this.LblCSVIn.Size = new System.Drawing.Size(111, 17);
		this.LblCSVIn.TabIndex = 0;
		this.LblCSVIn.Text = "CSV / Tab Input:";
		this.TabCols.Controls.Add(this.cmdAddR);
		this.TabCols.Controls.Add(this.LblColSpan);
		this.TabCols.Controls.Add(this.CmbColSpan);
		this.TabCols.Controls.Add(this.CmdRight4);
		this.TabCols.Controls.Add(this.CmdLeft4);
		this.TabCols.Controls.Add(this.CmdDel3);
		this.TabCols.Controls.Add(this.CmdDown3);
		this.TabCols.Controls.Add(this.CmdUp3);
		this.TabCols.Controls.Add(this.CmdDel4);
		this.TabCols.Controls.Add(this.CmdDown4);
		this.TabCols.Controls.Add(this.CmdUp4);
		this.TabCols.Controls.Add(this.LblCols);
		this.TabCols.Controls.Add(this.LblTopOf);
		this.TabCols.Controls.Add(this.GridTopOf);
		this.TabCols.Controls.Add(this.GridCols);
		this.TabCols.Location = new System.Drawing.Point(4, 25);
		this.TabCols.Margin = new System.Windows.Forms.Padding(4);
		this.TabCols.Name = "TabCols";
		this.TabCols.Padding = new System.Windows.Forms.Padding(4);
		this.TabCols.Size = new System.Drawing.Size(987, 649);
		this.TabCols.TabIndex = 1;
		this.TabCols.Text = "Columns";
		this.TabCols.UseVisualStyleBackColor = true;
		this.cmdAddR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdAddR.Location = new System.Drawing.Point(908, 502);
		this.cmdAddR.Margin = new System.Windows.Forms.Padding(4);
		this.cmdAddR.Name = "cmdAddR";
		this.cmdAddR.Size = new System.Drawing.Size(72, 31);
		this.cmdAddR.TabIndex = 11;
		this.cmdAddR.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdAddR, "Add Row");
		this.cmdAddR.UseVisualStyleBackColor = true;
		this.LblColSpan.AutoSize = true;
		this.LblColSpan.Location = new System.Drawing.Point(153, 318);
		this.LblColSpan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblColSpan.Name = "LblColSpan";
		this.LblColSpan.Size = new System.Drawing.Size(584, 17);
		this.LblColSpan.TabIndex = 23;
		this.LblColSpan.Text = "Optionally specify a special character in column headers to trigger column header spanning";
		this.ToolTip1.SetToolTip(this.LblColSpan, resources.GetString("LblColSpan.ToolTip"));
		this.CmbColSpan.FormattingEnabled = true;
		this.CmbColSpan.Items.AddRange(new object[2] { "", "@" });
		this.CmbColSpan.Location = new System.Drawing.Point(11, 311);
		this.CmbColSpan.Margin = new System.Windows.Forms.Padding(4);
		this.CmbColSpan.Name = "CmbColSpan";
		this.CmbColSpan.Size = new System.Drawing.Size(125, 24);
		this.CmbColSpan.TabIndex = 4;
		this.CmdRight4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdRight4.ImageIndex = 7;
		this.CmdRight4.Location = new System.Drawing.Point(908, 431);
		this.CmdRight4.Margin = new System.Windows.Forms.Padding(4);
		this.CmdRight4.Name = "CmdRight4";
		this.CmdRight4.Size = new System.Drawing.Size(72, 31);
		this.CmdRight4.TabIndex = 9;
		this.ToolTip1.SetToolTip(this.CmdRight4, "Move column to the right");
		this.CmdRight4.UseVisualStyleBackColor = true;
		this.CmdLeft4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdLeft4.ImageIndex = 6;
		this.CmdLeft4.Location = new System.Drawing.Point(908, 400);
		this.CmdLeft4.Margin = new System.Windows.Forms.Padding(4);
		this.CmdLeft4.Name = "CmdLeft4";
		this.CmdLeft4.Size = new System.Drawing.Size(72, 31);
		this.CmdLeft4.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.CmdLeft4, "Move column to the left");
		this.CmdLeft4.UseVisualStyleBackColor = true;
		this.CmdDel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel3.ImageIndex = 2;
		this.CmdDel3.Location = new System.Drawing.Point(908, 134);
		this.CmdDel3.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel3.Name = "CmdDel3";
		this.CmdDel3.Size = new System.Drawing.Size(72, 31);
		this.CmdDel3.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdDel3, "Delete row");
		this.CmdDel3.UseVisualStyleBackColor = true;
		this.CmdDown3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown3.ImageIndex = 1;
		this.CmdDown3.Location = new System.Drawing.Point(908, 98);
		this.CmdDown3.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown3.Name = "CmdDown3";
		this.CmdDown3.Size = new System.Drawing.Size(72, 31);
		this.CmdDown3.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.CmdDown3, "Move row down");
		this.CmdDown3.UseVisualStyleBackColor = true;
		this.CmdUp3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp3.ImageIndex = 0;
		this.CmdUp3.Location = new System.Drawing.Point(908, 68);
		this.CmdUp3.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp3.Name = "CmdUp3";
		this.CmdUp3.Size = new System.Drawing.Size(72, 31);
		this.CmdUp3.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.CmdUp3, "Move row up");
		this.CmdUp3.UseVisualStyleBackColor = true;
		this.CmdDel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel4.ImageIndex = 2;
		this.CmdDel4.Location = new System.Drawing.Point(908, 466);
		this.CmdDel4.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel4.Name = "CmdDel4";
		this.CmdDel4.Size = new System.Drawing.Size(72, 31);
		this.CmdDel4.TabIndex = 10;
		this.ToolTip1.SetToolTip(this.CmdDel4, "Delete row");
		this.CmdDel4.UseVisualStyleBackColor = true;
		this.CmdDown4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown4.ImageIndex = 1;
		this.CmdDown4.Location = new System.Drawing.Point(908, 372);
		this.CmdDown4.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown4.Name = "CmdDown4";
		this.CmdDown4.Size = new System.Drawing.Size(72, 31);
		this.CmdDown4.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.CmdDown4, "Move row down");
		this.CmdDown4.UseVisualStyleBackColor = true;
		this.CmdUp4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp4.ImageIndex = 0;
		this.CmdUp4.Location = new System.Drawing.Point(908, 341);
		this.CmdUp4.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp4.Name = "CmdUp4";
		this.CmdUp4.Size = new System.Drawing.Size(72, 31);
		this.CmdUp4.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.CmdUp4, "Move row up");
		this.CmdUp4.UseVisualStyleBackColor = true;
		this.LblCols.Location = new System.Drawing.Point(15, 258);
		this.LblCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCols.Name = "LblCols";
		this.LblCols.Size = new System.Drawing.Size(883, 49);
		this.LblCols.TabIndex = 3;
		this.LblCols.Text = resources.GetString("LblCols.Text");
		this.LblTopOf.Location = new System.Drawing.Point(15, 17);
		this.LblTopOf.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTopOf.Name = "LblTopOf";
		this.LblTopOf.Size = new System.Drawing.Size(881, 47);
		this.LblTopOf.TabIndex = 2;
		this.LblTopOf.Text = resources.GetString("LblTopOf.Text");
		this.GridTopOf.AllowUserToAddRows = false;
		this.GridTopOf.AllowUserToDeleteRows = false;
		this.GridTopOf.AllowUserToResizeColumns = false;
		this.GridTopOf.AllowUserToResizeRows = false;
		this.GridTopOf.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridTopOf.BackgroundColor = System.Drawing.Color.White;
		this.GridTopOf.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridTopOf.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.GridTopOf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridTopOf.Columns.AddRange(this.ColTopOfText);
		this.GridTopOf.Location = new System.Drawing.Point(11, 68);
		this.GridTopOf.Margin = new System.Windows.Forms.Padding(4);
		this.GridTopOf.Name = "GridTopOf";
		this.GridTopOf.RowHeadersWidth = 150;
		this.GridTopOf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GridTopOf.Size = new System.Drawing.Size(892, 166);
		this.GridTopOf.StandardTab = true;
		this.GridTopOf.TabIndex = 0;
		this.ColTopOfText.HeaderText = "Text";
		this.ColTopOfText.MinimumWidth = 6;
		this.ColTopOfText.Name = "ColTopOfText";
		this.ColTopOfText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColTopOfText.Width = 250;
		this.GridCols.AllowUserToAddRows = false;
		this.GridCols.AllowUserToDeleteRows = false;
		this.GridCols.AllowUserToResizeColumns = false;
		this.GridCols.AllowUserToResizeRows = false;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.GridCols.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
		this.GridCols.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridCols.BackgroundColor = System.Drawing.Color.White;
		this.GridCols.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridCols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.GridCols.ColumnHeadersHeight = 55;
		this.GridCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.GridCols.Columns.AddRange(this.ColCol, this.ColHdr, this.ColAlign, this.ColWSF, this.ColAtBot1, this.ColAtBot2, this.ColAtBot3, this.ColAtBot4, this.ColAtBot5, this.ColAtBot6, this.ColAtBot7, this.ColAtBot8, this.ColAtBot9);
		this.GridCols.Location = new System.Drawing.Point(11, 343);
		this.GridCols.Margin = new System.Windows.Forms.Padding(4);
		this.GridCols.Name = "GridCols";
		this.GridCols.RowHeadersWidth = 60;
		this.GridCols.Size = new System.Drawing.Size(893, 257);
		this.GridCols.StandardTab = true;
		this.GridCols.TabIndex = 5;
		this.ColCol.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColCol.DisplayStyleForCurrentCellOnly = true;
		this.ColCol.DropDownWidth = 200;
		this.ColCol.Frozen = true;
		this.ColCol.HeaderText = "Column";
		this.ColCol.MinimumWidth = 6;
		this.ColCol.Name = "ColCol";
		this.ColCol.Width = 125;
		this.ColHdr.HeaderText = "Header";
		this.ColHdr.MinimumWidth = 6;
		this.ColHdr.Name = "ColHdr";
		this.ColHdr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColHdr.Width = 125;
		this.ColAlign.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAlign.DisplayStyleForCurrentCellOnly = true;
		this.ColAlign.HeaderText = "Alignment";
		this.ColAlign.Items.AddRange("", "top-left", "top-center", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-center", "bottom-right");
		this.ColAlign.MinimumWidth = 6;
		this.ColAlign.Name = "ColAlign";
		this.ColAlign.Width = 90;
		this.ColWSF.HeaderText = "Format";
		this.ColWSF.MinimumWidth = 6;
		this.ColWSF.Name = "ColWSF";
		this.ColWSF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColWSF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColWSF.ToolTipText = "Enter width using w=n; Sig. Fig. using s=n;  data type for sort  using d=n; e=% to add%; Header case using h=u; Exclude filter using f=n. Separate by ; . E.g., w=10 ; s=2";
		this.ColWSF.Width = 90;
		this.ColAtBot1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot1.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot1.DropDownWidth = 90;
		this.ColAtBot1.HeaderText = "At Bottom of";
		this.ColAtBot1.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot1.MinimumWidth = 6;
		this.ColAtBot1.Name = "ColAtBot1";
		this.ColAtBot1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot1.Width = 70;
		this.ColAtBot2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot2.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot2.DropDownWidth = 90;
		this.ColAtBot2.HeaderText = "At Bottom of";
		this.ColAtBot2.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot2.MinimumWidth = 6;
		this.ColAtBot2.Name = "ColAtBot2";
		this.ColAtBot2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot2.Width = 70;
		this.ColAtBot3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot3.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot3.DropDownWidth = 90;
		this.ColAtBot3.HeaderText = "At Bottom of";
		this.ColAtBot3.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot3.MinimumWidth = 6;
		this.ColAtBot3.Name = "ColAtBot3";
		this.ColAtBot3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot3.Width = 70;
		this.ColAtBot4.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot4.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot4.DropDownWidth = 90;
		this.ColAtBot4.HeaderText = "At Bottom of";
		this.ColAtBot4.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot4.MinimumWidth = 6;
		this.ColAtBot4.Name = "ColAtBot4";
		this.ColAtBot4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot4.Width = 70;
		this.ColAtBot5.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot5.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot5.DropDownWidth = 90;
		this.ColAtBot5.HeaderText = "At Bottom of";
		this.ColAtBot5.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot5.MinimumWidth = 6;
		this.ColAtBot5.Name = "ColAtBot5";
		this.ColAtBot5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot5.Width = 70;
		this.ColAtBot6.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot6.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot6.DropDownWidth = 90;
		this.ColAtBot6.HeaderText = "At Bottom of";
		this.ColAtBot6.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot6.MinimumWidth = 6;
		this.ColAtBot6.Name = "ColAtBot6";
		this.ColAtBot6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot6.Width = 70;
		this.ColAtBot7.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot7.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot7.DropDownWidth = 90;
		this.ColAtBot7.HeaderText = "At Bottom of";
		this.ColAtBot7.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot7.MinimumWidth = 6;
		this.ColAtBot7.Name = "ColAtBot7";
		this.ColAtBot7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot7.Width = 70;
		this.ColAtBot8.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot8.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot8.DropDownWidth = 90;
		this.ColAtBot8.HeaderText = "At Bottom of";
		this.ColAtBot8.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot8.MinimumWidth = 6;
		this.ColAtBot8.Name = "ColAtBot8";
		this.ColAtBot8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot8.Width = 70;
		this.ColAtBot9.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot9.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot9.DropDownWidth = 90;
		this.ColAtBot9.HeaderText = "At Bottom of";
		this.ColAtBot9.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Total", "Var", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99", "Mode");
		this.ColAtBot9.MinimumWidth = 6;
		this.ColAtBot9.Name = "ColAtBot9";
		this.ColAtBot9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColAtBot9.Width = 70;
		this.TabRules.Controls.Add(this.CmbCol);
		this.TabRules.Controls.Add(this.LblRulRowHdr);
		this.TabRules.Controls.Add(this.GridRowRule);
		this.TabRules.Controls.Add(this.LblRuleColHdr);
		this.TabRules.Controls.Add(this.GridColRule);
		this.TabRules.Controls.Add(this.CmdDel6);
		this.TabRules.Controls.Add(this.CmdDown6);
		this.TabRules.Controls.Add(this.CmdUp6);
		this.TabRules.Controls.Add(this.CmdDel5);
		this.TabRules.Controls.Add(this.CmdDown5);
		this.TabRules.Controls.Add(this.CmdUp5);
		this.TabRules.Location = new System.Drawing.Point(4, 25);
		this.TabRules.Margin = new System.Windows.Forms.Padding(4);
		this.TabRules.Name = "TabRules";
		this.TabRules.Size = new System.Drawing.Size(987, 649);
		this.TabRules.TabIndex = 2;
		this.TabRules.Text = "Rules";
		this.TabRules.UseVisualStyleBackColor = true;
		this.CmbCol.DropDownWidth = 250;
		this.CmbCol.FormattingEnabled = true;
		this.CmbCol.Location = new System.Drawing.Point(68, 144);
		this.CmbCol.Margin = new System.Windows.Forms.Padding(4);
		this.CmbCol.Name = "CmbCol";
		this.CmbCol.Size = new System.Drawing.Size(156, 24);
		this.CmbCol.TabIndex = 28;
		this.CmbCol.Visible = false;
		this.LblRulRowHdr.Location = new System.Drawing.Point(7, 351);
		this.LblRulRowHdr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRulRowHdr.Name = "LblRulRowHdr";
		this.LblRulRowHdr.Size = new System.Drawing.Size(965, 49);
		this.LblRulRowHdr.TabIndex = 27;
		this.LblRulRowHdr.Text = "Specify Row Rules that affect the format of an entire row based on the value of a Test column. Use the % wildcard to reference columns that start or end with certain characters";
		this.GridRowRule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridRowRule.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridRowRule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.GridRowRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridRowRule.Columns.AddRange(this.ColRuleRTest, this.ColRuleROpr, this.ColRuleRVal, this.ColRuleRBC, this.ColRuleRColor, this.ColRuleRFF, this.ColRuleRFSz, this.ColRuleRFS, this.ColRuleRFW, this.ColRuleRTA, this.ColRuleRTD, this.ColRuleRVA);
		this.GridRowRule.Location = new System.Drawing.Point(11, 406);
		this.GridRowRule.Margin = new System.Windows.Forms.Padding(4);
		this.GridRowRule.Name = "GridRowRule";
		this.GridRowRule.RowHeadersWidth = 70;
		this.GridRowRule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridRowRule.Size = new System.Drawing.Size(893, 203);
		this.GridRowRule.StandardTab = true;
		this.GridRowRule.TabIndex = 4;
		this.ColRuleRTest.Frozen = true;
		this.ColRuleRTest.HeaderText = "Test Column";
		this.ColRuleRTest.MinimumWidth = 6;
		this.ColRuleRTest.Name = "ColRuleRTest";
		this.ColRuleRTest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRTest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRTest.Width = 125;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleROpr.DefaultCellStyle = dataGridViewCellStyle10;
		this.ColRuleROpr.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleROpr.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleROpr.Frozen = true;
		this.ColRuleROpr.HeaderText = "Operator";
		this.ColRuleROpr.Items.AddRange("EQ", "EQS", "BT", "BTS", "GE", "GES", "GT", "GTS", "LE", "LES", "LT", "LTS", "NBT", "NBTS", "NE", "NES");
		this.ColRuleROpr.MinimumWidth = 6;
		this.ColRuleROpr.Name = "ColRuleROpr";
		this.ColRuleROpr.Width = 90;
		this.ColRuleRVal.Frozen = true;
		this.ColRuleRVal.HeaderText = "Value";
		this.ColRuleRVal.MinimumWidth = 6;
		this.ColRuleRVal.Name = "ColRuleRVal";
		this.ColRuleRVal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRVal.Width = 125;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleRBC.DefaultCellStyle = dataGridViewCellStyle11;
		this.ColRuleRBC.HeaderText = "BackGround-Color";
		this.ColRuleRBC.MinimumWidth = 6;
		this.ColRuleRBC.Name = "ColRuleRBC";
		this.ColRuleRBC.ReadOnly = true;
		this.ColRuleRBC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRBC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRBC.Width = 105;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleRColor.DefaultCellStyle = dataGridViewCellStyle12;
		this.ColRuleRColor.HeaderText = "Font / Color";
		this.ColRuleRColor.MinimumWidth = 6;
		this.ColRuleRColor.Name = "ColRuleRColor";
		this.ColRuleRColor.ReadOnly = true;
		this.ColRuleRColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRColor.Width = 145;
		this.ColRuleRFF.HeaderText = "Font-Family";
		this.ColRuleRFF.MinimumWidth = 6;
		this.ColRuleRFF.Name = "ColRuleRFF";
		this.ColRuleRFF.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRFF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRFF.Visible = false;
		this.ColRuleRFF.Width = 75;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleRFSz.DefaultCellStyle = dataGridViewCellStyle13;
		this.ColRuleRFSz.HeaderText = "Font-Size";
		this.ColRuleRFSz.MinimumWidth = 6;
		this.ColRuleRFSz.Name = "ColRuleRFSz";
		this.ColRuleRFSz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRFSz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRFSz.Visible = false;
		this.ColRuleRFSz.Width = 75;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleRFS.DefaultCellStyle = dataGridViewCellStyle14;
		this.ColRuleRFS.HeaderText = "Font-Style";
		this.ColRuleRFS.MinimumWidth = 6;
		this.ColRuleRFS.Name = "ColRuleRFS";
		this.ColRuleRFS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRFS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRFS.Visible = false;
		this.ColRuleRFS.Width = 75;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleRFW.DefaultCellStyle = dataGridViewCellStyle15;
		this.ColRuleRFW.HeaderText = "Font-Weight";
		this.ColRuleRFW.MinimumWidth = 6;
		this.ColRuleRFW.Name = "ColRuleRFW";
		this.ColRuleRFW.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRFW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRFW.Visible = false;
		this.ColRuleRFW.Width = 75;
		this.ColRuleRTA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleRTA.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleRTA.HeaderText = "Text-Align";
		this.ColRuleRTA.MinimumWidth = 6;
		this.ColRuleRTA.Name = "ColRuleRTA";
		this.ColRuleRTA.Sorted = true;
		this.ColRuleRTA.Width = 75;
		this.ColRuleRTD.HeaderText = "Text-Decoration";
		this.ColRuleRTD.MinimumWidth = 6;
		this.ColRuleRTD.Name = "ColRuleRTD";
		this.ColRuleRTD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleRTD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleRTD.Visible = false;
		this.ColRuleRTD.Width = 125;
		this.ColRuleRVA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleRVA.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleRVA.HeaderText = "Vertical-Align";
		this.ColRuleRVA.MinimumWidth = 6;
		this.ColRuleRVA.Name = "ColRuleRVA";
		this.ColRuleRVA.Sorted = true;
		this.ColRuleRVA.Width = 75;
		this.LblRuleColHdr.Location = new System.Drawing.Point(11, 6);
		this.LblRuleColHdr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRuleColHdr.Name = "LblRuleColHdr";
		this.LblRuleColHdr.Size = new System.Drawing.Size(969, 38);
		this.LblRuleColHdr.TabIndex = 1;
		this.LblRuleColHdr.Text = resources.GetString("LblRuleColHdr.Text");
		this.GridColRule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridColRule.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridColRule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
		this.GridColRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridColRule.Columns.AddRange(this.ColRuleCDisplay, this.ColRuleCTest, this.ColRuleCOpr, this.ColRuleCVal, this.ColRuleCBC, this.ColRuleCColor, this.ColRuleCFF, this.ColRuleCFSz, this.ColRuleCFS, this.ColRuleCFW, this.ColRuleCTA, this.ColRuleCTD, this.ColRuleCVA);
		this.GridColRule.Location = new System.Drawing.Point(11, 50);
		this.GridColRule.Margin = new System.Windows.Forms.Padding(4);
		this.GridColRule.Name = "GridColRule";
		this.GridColRule.RowHeadersWidth = 70;
		this.GridColRule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridColRule.Size = new System.Drawing.Size(893, 293);
		this.GridColRule.StandardTab = true;
		this.GridColRule.TabIndex = 0;
		this.ColRuleCDisplay.Frozen = true;
		this.ColRuleCDisplay.HeaderText = "Display Column";
		this.ColRuleCDisplay.MinimumWidth = 6;
		this.ColRuleCDisplay.Name = "ColRuleCDisplay";
		this.ColRuleCDisplay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCDisplay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCDisplay.Width = 90;
		this.ColRuleCTest.Frozen = true;
		this.ColRuleCTest.HeaderText = "Test Column";
		this.ColRuleCTest.MinimumWidth = 6;
		this.ColRuleCTest.Name = "ColRuleCTest";
		this.ColRuleCTest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCTest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCTest.Width = 95;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCOpr.DefaultCellStyle = dataGridViewCellStyle17;
		this.ColRuleCOpr.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleCOpr.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleCOpr.HeaderText = "Operator";
		this.ColRuleCOpr.Items.AddRange("EQ", "EQS", "BT", "BTS", "GE", "GES", "GT", "GTS", "LE", "LES", "LT", "LTS", "NBT", "NBTS", "NE", "NES");
		this.ColRuleCOpr.MinimumWidth = 6;
		this.ColRuleCOpr.Name = "ColRuleCOpr";
		this.ColRuleCOpr.Width = 90;
		this.ColRuleCVal.HeaderText = "Value";
		this.ColRuleCVal.MinimumWidth = 6;
		this.ColRuleCVal.Name = "ColRuleCVal";
		this.ColRuleCVal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCVal.Width = 80;
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCBC.DefaultCellStyle = dataGridViewCellStyle18;
		this.ColRuleCBC.HeaderText = "BackGround-Color";
		this.ColRuleCBC.MinimumWidth = 6;
		this.ColRuleCBC.Name = "ColRuleCBC";
		this.ColRuleCBC.ReadOnly = true;
		this.ColRuleCBC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCBC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCBC.Width = 125;
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCColor.DefaultCellStyle = dataGridViewCellStyle19;
		this.ColRuleCColor.HeaderText = "Font / Color";
		this.ColRuleCColor.MinimumWidth = 6;
		this.ColRuleCColor.Name = "ColRuleCColor";
		this.ColRuleCColor.ReadOnly = true;
		this.ColRuleCColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCColor.Width = 85;
		this.ColRuleCFF.HeaderText = "Font-Family";
		this.ColRuleCFF.MinimumWidth = 6;
		this.ColRuleCFF.Name = "ColRuleCFF";
		this.ColRuleCFF.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFF.Visible = false;
		this.ColRuleCFF.Width = 75;
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFSz.DefaultCellStyle = dataGridViewCellStyle20;
		this.ColRuleCFSz.HeaderText = "Font-Size";
		this.ColRuleCFSz.MinimumWidth = 6;
		this.ColRuleCFSz.Name = "ColRuleCFSz";
		this.ColRuleCFSz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFSz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFSz.Visible = false;
		this.ColRuleCFSz.Width = 75;
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFS.DefaultCellStyle = dataGridViewCellStyle21;
		this.ColRuleCFS.HeaderText = "Font-Style";
		this.ColRuleCFS.MinimumWidth = 6;
		this.ColRuleCFS.Name = "ColRuleCFS";
		this.ColRuleCFS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFS.Visible = false;
		this.ColRuleCFS.Width = 75;
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFW.DefaultCellStyle = dataGridViewCellStyle22;
		this.ColRuleCFW.HeaderText = "Font-Weight";
		this.ColRuleCFW.MinimumWidth = 6;
		this.ColRuleCFW.Name = "ColRuleCFW";
		this.ColRuleCFW.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFW.Visible = false;
		this.ColRuleCFW.Width = 75;
		this.ColRuleCTA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleCTA.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleCTA.HeaderText = "Text-Align";
		this.ColRuleCTA.MinimumWidth = 6;
		this.ColRuleCTA.Name = "ColRuleCTA";
		this.ColRuleCTA.Sorted = true;
		this.ColRuleCTA.Width = 75;
		this.ColRuleCTD.HeaderText = "Text-Decoration";
		this.ColRuleCTD.MinimumWidth = 6;
		this.ColRuleCTD.Name = "ColRuleCTD";
		this.ColRuleCTD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCTD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCTD.Visible = false;
		this.ColRuleCTD.Width = 125;
		this.ColRuleCVA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColRuleCVA.DisplayStyleForCurrentCellOnly = true;
		this.ColRuleCVA.HeaderText = "Vertical-Align";
		this.ColRuleCVA.MinimumWidth = 6;
		this.ColRuleCVA.Name = "ColRuleCVA";
		this.ColRuleCVA.Sorted = true;
		this.ColRuleCVA.Width = 75;
		this.CmdDel6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel6.ImageIndex = 2;
		this.CmdDel6.Location = new System.Drawing.Point(908, 471);
		this.CmdDel6.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel6.Name = "CmdDel6";
		this.CmdDel6.Size = new System.Drawing.Size(72, 31);
		this.CmdDel6.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.CmdDel6, "Delete row");
		this.CmdDel6.UseVisualStyleBackColor = true;
		this.CmdDown6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown6.ImageIndex = 1;
		this.CmdDown6.Location = new System.Drawing.Point(908, 436);
		this.CmdDown6.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown6.Name = "CmdDown6";
		this.CmdDown6.Size = new System.Drawing.Size(72, 31);
		this.CmdDown6.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.CmdDown6, "Move row down");
		this.CmdDown6.UseVisualStyleBackColor = true;
		this.CmdUp6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp6.ImageIndex = 0;
		this.CmdUp6.Location = new System.Drawing.Point(908, 405);
		this.CmdUp6.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp6.Name = "CmdUp6";
		this.CmdUp6.Size = new System.Drawing.Size(72, 31);
		this.CmdUp6.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.CmdUp6, "Move row up");
		this.CmdUp6.UseVisualStyleBackColor = true;
		this.CmdDel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel5.ImageIndex = 2;
		this.CmdDel5.Location = new System.Drawing.Point(911, 114);
		this.CmdDel5.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDel5.Name = "CmdDel5";
		this.CmdDel5.Size = new System.Drawing.Size(72, 31);
		this.CmdDel5.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdDel5, "Delete row");
		this.CmdDel5.UseVisualStyleBackColor = true;
		this.CmdDown5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown5.ImageIndex = 1;
		this.CmdDown5.Location = new System.Drawing.Point(911, 79);
		this.CmdDown5.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown5.Name = "CmdDown5";
		this.CmdDown5.Size = new System.Drawing.Size(72, 31);
		this.CmdDown5.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.CmdDown5, "Move row down");
		this.CmdDown5.UseVisualStyleBackColor = true;
		this.CmdUp5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp5.ImageIndex = 0;
		this.CmdUp5.Location = new System.Drawing.Point(911, 48);
		this.CmdUp5.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp5.Name = "CmdUp5";
		this.CmdUp5.Size = new System.Drawing.Size(72, 31);
		this.CmdUp5.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.CmdUp5, "Move row up");
		this.CmdUp5.UseVisualStyleBackColor = true;
		this.mnuRpt.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuRpt.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuNew, this.mnuSave, this.ToolStripSeparator1, this.mnuRun });
		this.mnuRpt.Location = new System.Drawing.Point(0, 28);
		this.mnuRpt.Name = "mnuRpt";
		this.mnuRpt.Size = new System.Drawing.Size(1084, 27);
		this.mnuRpt.TabIndex = 5;
		this.mnuRpt.Text = "ToolStrip1";
		this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuNew.Image = (System.Drawing.Image)resources.GetObject("mnuNew.Image");
		this.mnuNew.ImageTransparentColor = System.Drawing.Color.Red;
		this.mnuNew.Name = "mnuNew";
		this.mnuNew.Size = new System.Drawing.Size(29, 24);
		this.mnuNew.Text = "ToolStripButton1";
		this.mnuNew.ToolTipText = "Clear the Report Design";
		this.mnuSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuSave.Image = (System.Drawing.Image)resources.GetObject("mnuSave.Image");
		this.mnuSave.ImageTransparentColor = System.Drawing.Color.Red;
		this.mnuSave.Name = "mnuSave";
		this.mnuSave.Size = new System.Drawing.Size(29, 24);
		this.mnuSave.Text = "Save Report Spec in SQLPathFinder Query";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 27);
		this.mnuRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuRun.Image = (System.Drawing.Image)resources.GetObject("mnuRun.Image");
		this.mnuRun.ImageTransparentColor = System.Drawing.Color.Red;
		this.mnuRun.Name = "mnuRun";
		this.mnuRun.Size = new System.Drawing.Size(29, 24);
		this.mnuRun.Text = "ToolStripButton1";
		this.mnuRun.ToolTipText = "Generate the Report";
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuFile, this.FileExternalToolStripMenuItem, this.mnuRptOptions });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Size = new System.Drawing.Size(1084, 28);
		this.MenuStrip1.TabIndex = 6;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuNewQ, this.mnuSaveQ, this.mnuRunQ });
		this.mnuFile.Name = "mnuFile";
		this.mnuFile.Size = new System.Drawing.Size(110, 24);
		this.mnuFile.Text = "File (Internal)";
		this.mnuNewQ.Name = "mnuNewQ";
		this.mnuNewQ.Size = new System.Drawing.Size(366, 26);
		this.mnuNewQ.Text = "New";
		this.mnuNewQ.ToolTipText = "Clear the Report Design";
		this.mnuSaveQ.Name = "mnuSaveQ";
		this.mnuSaveQ.Size = new System.Drawing.Size(366, 26);
		this.mnuSaveQ.Text = "Save Report Spec in SQLPathFinder Query";
		this.mnuRunQ.Name = "mnuRunQ";
		this.mnuRunQ.Size = new System.Drawing.Size(366, 26);
		this.mnuRunQ.Text = "Run Report";
		this.FileExternalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuOpenF, this.mnuSaveF });
		this.FileExternalToolStripMenuItem.Name = "FileExternalToolStripMenuItem";
		this.FileExternalToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
		this.FileExternalToolStripMenuItem.Text = "File (External)";
		this.mnuOpenF.Name = "mnuOpenF";
		this.mnuOpenF.Size = new System.Drawing.Size(482, 26);
		this.mnuOpenF.Text = "Open an External Report Spec File";
		this.mnuSaveF.Name = "mnuSaveF";
		this.mnuSaveF.Size = new System.Drawing.Size(482, 26);
		this.mnuSaveF.Text = "Save Report Spec to a File External to SQLPathFinder Query";
		this.mnuRptOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuEnableColDrillDown, this.mnuEnableColSort, this.mnuEnableDynFilter, this.mnuAtTop, this.mnuPreProc });
		this.mnuRptOptions.Name = "mnuRptOptions";
		this.mnuRptOptions.Size = new System.Drawing.Size(124, 24);
		this.mnuRptOptions.Text = "Report Options";
		this.mnuEnableColDrillDown.CheckOnClick = true;
		this.mnuEnableColDrillDown.Name = "mnuEnableColDrillDown";
		this.mnuEnableColDrillDown.Size = new System.Drawing.Size(379, 26);
		this.mnuEnableColDrillDown.Text = "Enable Report Drill-Down";
		this.mnuEnableColSort.AutoToolTip = true;
		this.mnuEnableColSort.CheckOnClick = true;
		this.mnuEnableColSort.Name = "mnuEnableColSort";
		this.mnuEnableColSort.Size = new System.Drawing.Size(379, 26);
		this.mnuEnableColSort.Text = "Enable  Sorting of Columns";
		this.mnuEnableColSort.ToolTipText = "Dynamically Sort Columns (Incompatible with row/col spanning and dynamic filtering)";
		this.mnuEnableDynFilter.AutoToolTip = true;
		this.mnuEnableDynFilter.CheckOnClick = true;
		this.mnuEnableDynFilter.Name = "mnuEnableDynFilter";
		this.mnuEnableDynFilter.Size = new System.Drawing.Size(379, 26);
		this.mnuEnableDynFilter.Text = "Enable Filtering of Rows";
		this.mnuEnableDynFilter.ToolTipText = "Dynamically Filter Rows. (Incompatible with Row/Col spanning and Column Sorting)";
		this.mnuAtTop.Name = "mnuAtTop";
		this.mnuAtTop.Size = new System.Drawing.Size(379, 26);
		this.mnuAtTop.Text = "Do At Top of Column Drilldown Summaries";
		this.mnuPreProc.Checked = true;
		this.mnuPreProc.CheckOnClick = true;
		this.mnuPreProc.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuPreProc.Name = "mnuPreProc";
		this.mnuPreProc.Size = new System.Drawing.Size(379, 26);
		this.mnuPreProc.Text = "Do Not Pre-Process CSV/TAB File";
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdOK.ImageIndex = 4;
		this.CmdOK.Location = new System.Drawing.Point(1003, 87);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(73, 49);
		this.CmdOK.TabIndex = 14;
		this.CmdOK.Text = "OK";
		this.CmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 5;
		this.CmdCancel.Location = new System.Drawing.Point(1003, 142);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(73, 49);
		this.CmdCancel.TabIndex = 15;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(1084, 745);
		base.ControlBox = false;
		base.Controls.Add(this.mnuRpt);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.TabRpt);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmReport";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SQLPathFinder Report Writer";
		this.TabRpt.ResumeLayout(false);
		this.TabMain.ResumeLayout(false);
		this.TabMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridRptHdr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridSort).EndInit();
		this.TabCols.ResumeLayout(false);
		this.TabCols.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridTopOf).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridCols).EndInit();
		this.TabRules.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridRowRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridColRule).EndInit();
		this.mnuRpt.ResumeLayout(false);
		this.mnuRpt.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Grid_Row_DelR(ref DataGridView MyGrid, int MyRC)
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
							goto IL_0014;
						case 4:
							goto IL_0024;
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
					BuildReport.Grid_Row_Del(ref MyGrid);
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (MyGrid.RowCount >= MyRC)
					{
						goto end_IL_0001_3;
					}
					goto IL_0024;
					IL_0024:
					num2 = 4;
					MyGrid.RowCount = MyRC;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				GridModule.Number_Grid(ref MyGrid);
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

	public bool Chk_Col_Rule(string MyDisplay, string MyTest)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
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
				case 271:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001f;
						case 5:
							goto IL_002f;
						case 6:
							goto IL_0043;
						case 8:
							goto IL_004a;
						case 9:
							goto IL_007c;
						case 11:
							goto IL_0084;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 10:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004a:
					num2 = 8;
					if (num5 != 0 && num6 != 0 && Operators.CompareString(MyDisplay.ToUpper().Trim(), MyTest.ToUpper().Trim(), TextCompare: false) == 0)
					{
						goto IL_007c;
					}
					goto IL_0084;
					IL_0084:
					num2 = 11;
					if (num5 != 0 && num6 != 0 && ((num5 == 1 && num6 == 1) || (num5 == MyDisplay.Length && num6 == MyTest.Length)))
					{
						break;
					}
					goto end_IL_0001_3;
					IL_007c:
					num2 = 9;
					result = true;
					goto end_IL_0001_3;
					IL_0043:
					num2 = 6;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = Strings.InStr(MyDisplay, "%");
					goto IL_001f;
					IL_001f:
					num2 = 4;
					num6 = Strings.InStr(MyTest, "%");
					goto IL_002f;
					IL_002f:
					num2 = 5;
					if (num5 == 0 || num6 == 0)
					{
						goto IL_0043;
					}
					goto IL_004a;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 271;
				continue;
			}
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

	public string Distinct_Cols_Grid(ref DataGridView MyGrid, int MyCol)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				Type typeFromHandle;
				object[] obj;
				DataGridViewCell dataGridViewCell;
				bool[] obj2;
				object obj3;
				Type typeFromHandle2;
				object[] obj4;
				DataGridViewRowHeaderCell headerCell;
				object[] array;
				bool[] obj5;
				bool[] array2;
				object obj6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 648:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_002f;
						case 6:
							goto IL_0034;
						case 7:
							goto IL_003d;
						case 8:
							goto IL_0057;
						case 9:
							goto IL_0066;
						case 10:
							goto IL_00e2;
						case 11:
							goto IL_00f8;
						case 13:
							goto IL_0107;
						case 14:
							goto IL_0120;
						case 17:
							goto IL_0132;
						case 12:
						case 15:
						case 16:
						case 18:
						case 19:
							goto IL_01b1;
						case 20:
							goto IL_01ca;
						case 21:
							goto IL_01ee;
						case 22:
						case 23:
							goto IL_0201;
						default:
							goto end_IL_0001;
						case 4:
						case 24:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0132:
					num2 = 17;
					typeFromHandle = typeof(Strings);
					obj = new object[1] { (dataGridViewCell = MyGrid.Rows[num5].Cells[MyCol]).Value };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					obj3 = NewLateBinding.LateGet(null, typeFromHandle, "LCase", obj, null, null, obj2);
					if (array2[0])
					{
						dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					text = Strings.Trim(Conversions.ToString(obj3));
					goto IL_01b1;
					IL_0201:
					num2 = 23;
					num5 = checked(num5 + 1);
					goto IL_020a;
					IL_0120:
					num2 = 14;
					text = "";
					goto IL_01b1;
					IL_01ee:
					num2 = 21;
					text2 = text2 + text + ",";
					goto IL_0201;
					IL_000b:
					num2 = 2;
					text2 = ",";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (MyGrid.Rows.Count == 0)
					{
						goto end_IL_0001_2;
					}
					goto IL_002f;
					IL_002f:
					num2 = 5;
					num5 = 0;
					goto IL_0034;
					IL_0034:
					num2 = 6;
					text = "";
					goto IL_003d;
					IL_003d:
					num2 = 7;
					num6 = checked(MyGrid.Rows.Count - 1);
					num5 = 0;
					goto IL_020a;
					IL_020a:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0057;
					IL_0057:
					num2 = 8;
					if (MyCol == -1)
					{
						goto IL_0066;
					}
					goto IL_0132;
					IL_0066:
					num2 = 9;
					typeFromHandle2 = typeof(Strings);
					obj4 = new object[1] { (headerCell = GridTopOf.Rows[num5].HeaderCell).Value };
					array = obj4;
					obj5 = new bool[1] { true };
					array2 = obj5;
					obj6 = NewLateBinding.LateGet(null, typeFromHandle2, "LCase", obj4, null, null, obj5);
					if (array2[0])
					{
						headerCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					text = Strings.Trim(Conversions.ToString(obj6));
					goto IL_00e2;
					IL_01ca:
					num2 = 20;
					if (Strings.InStr(text2, "," + text + ",") == 0)
					{
						goto IL_01ee;
					}
					goto IL_0201;
					IL_01b1:
					num2 = 19;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_01ca;
					}
					goto IL_0201;
					IL_00e2:
					num2 = 10;
					if (LikeOperator.LikeString(text, "at top of {*", CompareMethod.Binary))
					{
						goto IL_00f8;
					}
					goto IL_0107;
					IL_00f8:
					num2 = 11;
					text = General_Procedures.Get_Node_Value(text);
					goto IL_01b1;
					IL_0107:
					num2 = 13;
					if (Operators.CompareString(text, "at top of", TextCompare: false) == 0)
					{
						goto IL_0120;
					}
					goto IL_01b1;
					end_IL_0001:
					break;
				}
			}
			catch (object obj7) when (obj7 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj7);
				try0001_dispatch = 648;
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
		return text2;
	}

	public void GridClick(ref DataGridView MyGrid, short MyIdx)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int columnIndex = default(int);
		int rowIndex = default(int);
		bool flag = default(bool);
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
				case 451:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002a;
						case 5:
							goto IL_002f;
						case 6:
							goto IL_0034;
						case 8:
							goto IL_004b;
						case 9:
							goto IL_0062;
						case 11:
							goto IL_006b;
						case 12:
							goto IL_007f;
						case 10:
						case 13:
						case 14:
							goto IL_0086;
						case 15:
							goto IL_0094;
						case 16:
							goto IL_00a8;
						case 17:
							goto IL_00c1;
						case 18:
							goto IL_00dd;
						case 19:
							goto IL_0108;
						case 20:
							goto IL_0117;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 22:
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c1:
					num2 = 17;
					num5 = checked(ColCol.Items.Count - 1);
					num6 = 5;
					goto IL_0111;
					IL_0111:
					if (num6 <= num5)
					{
						goto IL_00dd;
					}
					goto IL_0117;
					IL_00a8:
					num2 = 16;
					CmbCol.Items.Add("");
					goto IL_00c1;
					IL_0117:
					num2 = 20;
					CMbIdx = MyIdx;
					break;
					IL_000b:
					num2 = 2;
					columnIndex = MyGrid.CurrentCell.ColumnIndex;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					rowIndex = MyGrid.CurrentCell.RowIndex;
					goto IL_002a;
					IL_002a:
					num2 = 4;
					flag = false;
					goto IL_002f;
					IL_002f:
					num2 = 5;
					num6 = 0;
					goto IL_0034;
					IL_0034:
					num2 = 6;
					if (rowIndex == -1 || columnIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_004b;
					IL_004b:
					num2 = 8;
					if (MyIdx == 0 && (columnIndex == 0 || columnIndex == 1))
					{
						goto IL_0062;
					}
					goto IL_006b;
					IL_00dd:
					num2 = 18;
					CmbCol.Items.Add(RuntimeHelpers.GetObjectValue(ColCol.Items[num6]));
					goto IL_0108;
					IL_0062:
					num2 = 9;
					flag = true;
					goto IL_0086;
					IL_006b:
					num2 = 11;
					if (MyIdx == 1 && columnIndex == 0)
					{
						goto IL_007f;
					}
					goto IL_0086;
					IL_0108:
					num2 = 19;
					num6 = checked(num6 + 1);
					goto IL_0111;
					IL_007f:
					num2 = 12;
					flag = true;
					goto IL_0086;
					IL_0086:
					num2 = 14;
					if (!flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_0094;
					IL_0094:
					num2 = 15;
					CmbCol.Items.Clear();
					goto IL_00a8;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				ComboBox MyCombo = CmbCol;
				GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 2);
				CmbCol = MyCombo;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 451;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Refresh_Combo()
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
					if (!CmbCol.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				CmbCol_Leave(CmbCol, new EventArgs());
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

	public string Make_Tokens_UC(string MyStr)
	{
		MyStr = Strings.Replace(MyStr, "<ts>", "<TS>", 1, -1, CompareMethod.Text);
		MyStr = Strings.Replace(MyStr, "<day>", "<DAY>", 1, -1, CompareMethod.Text);
		MyStr = Strings.Replace(MyStr, "<dow>", "<DOW>", 1, -1, CompareMethod.Text);
		MyStr = Strings.Replace(MyStr, "<ww>", "<WW>", 1, -1, CompareMethod.Text);
		MyStr = Strings.Replace(MyStr, "<sp>", "<SP>", 1, -1, CompareMethod.Text);
		MyStr = General_Procedures.Rep_Prob_Quote(MyStr);
		return MyStr;
	}

	public bool Check_Valid_Files(ref int NoCols)
	{
		int try0001_dispatch = -1;
		bool result;
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
						result = false;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmReport - Check_Valid_Files";
						NoCols = 0;
						int num3 = 0;
						string text = "";
						if (Operators.CompareString(Strings.Trim(TxtCSVIn.Text), "", TextCompare: false) == 0)
						{
							text = "You must specify a CSV File to process";
							TxtCSVIn.Focus();
						}
						else if (Operators.CompareString(Strings.Trim(Txtcss.Text), "", TextCompare: false) == 0)
						{
							text = "You must specify a Report Style Sheet";
							Txtcss.Focus();
						}
						else
						{
							int num4 = GridCols.RowCount - 1;
							for (num3 = 0; num3 <= num4; num3++)
							{
								if (Operators.ConditionalCompareObjectNotEqual(GridCols.Rows[num3].Cells[0].Value, "", TextCompare: false))
								{
									NoCols++;
								}
							}
							if (NoCols == 0)
							{
								text = "You must select at least one column to report";
							}
						}
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							result = true;
						}
						else
						{
							Interaction.MsgBox(text, MsgBoxStyle.Exclamation, "Cannot Create a Report Specification File");
						}
						goto end_IL_0001;
					}
					case 348:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = false;
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 348;
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

	public int Open_Report(string MyRptSpec)
	{
		int try0001_dispatch = -1;
		int result;
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
						errsource = "FrmReport - Open_Report";
						string[] lCSVCols = new string[0];
						int num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						int num4 = -1;
						int num5 = -1;
						int num6 = -1;
						int num7 = 3;
						int num8 = -1;
						int num9 = -1;
						int num10 = 0;
						int num11 = 0;
						int num12 = 0;
						int num13 = 0;
						int num14 = 0;
						int num15 = 0;
						int num16 = 0;
						string[] array = Strings.Split(MyRptSpec, "\r\n");
						num13 = 1;
						do
						{
							text2 = ((num13 != 1) ? "CSS" : "INPUT-FILE");
							num10 = Strings.Len(text2 + "<\\\\>");
							int num17 = Information.UBound(array);
							for (num12 = 0; num12 <= num17; num12++)
							{
								if (Operators.CompareString(Strings.UCase(Strings.Mid(array[num12], 1, num10)), text2 + "<\\\\>", TextCompare: false) == 0)
								{
									text = Strings.Mid(array[num12] + " ", num10 + 1);
									num11 = Strings.InStr(text, "<\\\\>");
									text3 = ((num11 == 0) ? Strings.Trim(text) : Strings.Trim(Strings.Mid(text, 1, num11 - 1)));
									if (num13 == 1)
									{
										TxtCSVIn.Text = text3;
									}
									else
									{
										text4 = text3;
									}
									break;
								}
							}
							num13++;
						}
						while (num13 <= 2);
						text = "";
						text2 = "";
						text3 = "";
						if (Operators.CompareString(TxtCSVIn.Text, "", TextCompare: false) != 0)
						{
							num3 = General_Procedures.GetCSVHeaders(TxtCSVIn.Text, ref lCSVCols);
							if (num3 == 1)
							{
								Interaction.MsgBox("Note that the CSV File you are reporting on was not found or has no columns. Data for the last report design will be shown but you will be unable to make significant updates", MsgBoxStyle.Exclamation, "Missing CSV Data");
							}
							Clear_All_Grids();
							Txtcss.Text = text4;
							GridCols.RowCount = 1999;
							if (num3 == 0)
							{
								int num18 = Information.UBound(lCSVCols);
								for (num12 = 0; num12 <= num18; num12++)
								{
									ColName.Items.Add(lCSVCols[num12]);
									ColCol.Items.Add(lCSVCols[num12]);
								}
							}
							int num19 = Information.UBound(array);
							string[] array2;
							for (num12 = 0; num12 <= num19; num12++)
							{
								if (!(!LikeOperator.LikeString(Strings.UCase(array[num12]), "INPUT-FILE,*", CompareMethod.Binary) & !LikeOperator.LikeString(Strings.UCase(array[num12]), "CSS,*", CompareMethod.Binary)))
								{
									continue;
								}
								array2 = Strings.Split(array[num12], "<\\\\>");
								text5 = Strings.Trim(Strings.UCase(array2[0]));
								if (LikeOperator.LikeString(text5, "AT-TOP-OF-COL*", CompareMethod.Binary))
								{
									text5 = "AT-TOP-OF-COL";
								}
								else if (LikeOperator.LikeString(text5, "AT-BOT-OF-COL*", CompareMethod.Binary))
								{
									text5 = "AT-BOT-OF-COL";
								}
								else if (LikeOperator.LikeString(text5, "COLUMN-RULE*", CompareMethod.Binary))
								{
									text5 = "COLUMN-RULE";
								}
								else if (LikeOperator.LikeString(text5, "ROW-RULE*", CompareMethod.Binary))
								{
									text5 = "ROW-RULE";
								}
								switch (text5)
								{
								case "DYNAMICSORT":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuEnableColSort.Checked = true;
									}
									break;
								case "DRILLDOWN":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuEnableColDrillDown.Checked = true;
									}
									break;
								case "ATTOPDRILLDOWN":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuAtTop.Checked = true;
									}
									break;
								case "DYNAMICFILTER":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuEnableDynFilter.Checked = true;
									}
									break;
								case "NOPREPROCESS":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuPreProc.Checked = true;
									}
									else
									{
										mnuPreProc.Checked = false;
									}
									break;
								case "COLSPAN":
									CmbColSpan.Text = Strings.Trim(array2[1]);
									break;
								case "SORT":
								{
									num4++;
									text = Strings.Trim(array2[1]);
									DataGridViewComboBoxColumn MyCombo = ColName;
									BuildReport.Verify_Column(ref MyCombo, text);
									ColName = MyCombo;
									GridSort.Rows[num4].Cells[0].Value = text;
									GridSort.Rows[num4].Cells[1].Value = Strings.Trim(array2[2]);
									GridSort.Rows[num4].Cells[2].Value = Strings.UCase(Strings.Trim(array2[3]));
									break;
								}
								case "AT-TOP-OF-REPORT":
									num5++;
									GridRptHdr.Rows[num5].Cells[0].Value = array2[2];
									break;
								case "AT-TOP-OF-COL":
									num6++;
									text = Strings.Trim(array2[1]);
									GridTopOf.Rows[num6].HeaderCell.Value = "At Top Of {" + text + "}";
									GridTopOf.Rows[num6].Cells[0].Value = array2[2];
									break;
								case "COLUMN-DATA":
								{
									int num25 = Information.UBound(array2);
									for (num13 = 2; num13 <= num25; num13++)
									{
										text = Strings.Trim(array2[num13]);
										if (Operators.CompareString(text, "", TextCompare: false) == 0)
										{
											break;
										}
										DataGridViewComboBoxColumn MyCombo = ColCol;
										BuildReport.Verify_Column(ref MyCombo, text);
										ColCol = MyCombo;
										GridCols.Rows[num13 - 2].Cells[0].Value = text;
										num15++;
									}
									if (num15 >= ColCol.Items.Count - 5)
									{
										GridCols.RowCount = num15;
									}
									else
									{
										GridCols.RowCount = ColCol.Items.Count - 5;
									}
									break;
								}
								case "COLUMN-HEADERS":
								case "COLUMN-ALIGNMENT":
								case "COLUMN-FORMAT":
								{
									num10 = ((Operators.CompareString(text5, "COLUMN-HEADERS", TextCompare: false) == 0) ? 1 : ((Operators.CompareString(text5, "COLUMN-ALIGNMENT", TextCompare: false) != 0) ? 3 : 2));
									int num26 = Information.UBound(array2);
									for (num13 = 2; num13 <= num26; num13++)
									{
										text = Strings.Trim(array2[num13]);
										if (Operators.CompareString(text, "", TextCompare: false) != 0)
										{
											GridCols.Rows[num13 - 2].Cells[num10].Value = text;
										}
									}
									break;
								}
								case "AT-BOT-OF-COL":
								case "AT-BOT-OF-REPORT":
								{
									num7++;
									if (Operators.CompareString(text5, "AT-BOT-OF-COL", TextCompare: false) == 0)
									{
										text = Strings.Trim(array2[1]);
										GridCols.Columns[num7].HeaderText = "At Bottom of {" + text + "}";
									}
									else
									{
										GridCols.Columns[num7].HeaderText = "At Bottom of {Report}";
									}
									int num24 = Information.UBound(array2);
									for (num13 = 2; num13 <= num24; num13++)
									{
										text = array2[num13];
										num11 = Strings.InStr(text, ":");
										if (num11 != 0)
										{
											text2 = Strings.UCase(Strings.Trim(Strings.Mid(text, 1, num11)));
											text3 = Strings.Trim(Strings.Mid(text + " ", num11 + 1));
											switch (text2)
											{
											case "TEXT:":
												GridCols.Rows[num13 - 2].Cells[num7].Value = "Text:";
												GridCols.Rows[num13 - 2].Cells[num7].Tag = text3;
												break;
											case "EXP2:":
												GridCols.Rows[num13 - 2].Cells[num7].Value = "Expr:";
												GridCols.Rows[num13 - 2].Cells[num7].Tag = text3;
												break;
											case "EXP1:":
												GridCols.Rows[num13 - 2].Cells[num7].Value = text3;
												break;
											}
										}
									}
									break;
								}
								case "COLUMN-RULE":
								{
									num8++;
									text = Strings.Trim(array2[1]);
									GridColRule.Rows[num8].Cells[0].Value = text;
									text = Strings.Trim(array2[2]);
									GridColRule.Rows[num8].Cells[1].Value = text;
									if (LikeOperator.LikeString(Strings.Trim(Strings.LCase(array2[4])), "background-color:*", CompareMethod.Binary))
									{
										GridColRule.Rows[num8].Cells[2].Value = "EQS";
										GridColRule.Rows[num8].Cells[3].Value = array2[3];
										num16 = 4;
									}
									else
									{
										text = Strings.Trim(array2[3]);
										if (Operators.CompareString(text, "", TextCompare: false) == 0)
										{
											text = "EQS";
										}
										else
										{
											DataGridViewComboBoxColumn MyCombo = ColRuleCOpr;
											BuildReport.Verify_Column(ref MyCombo, text);
											ColRuleCOpr = MyCombo;
										}
										GridColRule.Rows[num8].Cells[2].Value = text;
										GridColRule.Rows[num8].Cells[3].Value = array2[4];
										num16 = 5;
									}
									int num27 = num16;
									int num28 = Information.UBound(array2);
									for (num13 = num27; num13 <= num28; num13++)
									{
										text = array2[num13];
										num11 = Strings.InStr(text, ":");
										if (num11 == 0)
										{
											continue;
										}
										text2 = Strings.LCase(Strings.Trim(Strings.Mid(text, 1, num11 - 1)));
										text3 = Strings.Trim(Strings.Mid(text + " ", num11 + 1));
										int num29 = f_MaxObj;
										for (num14 = 0; num14 <= num29; num14++)
										{
											if (Operators.CompareString(text2, CSSObj[num14].Value, TextCompare: false) == 0)
											{
												text2 = Strings.LCase(CSSObj[num14].Name);
												break;
											}
										}
										int num30 = GridColRule.ColumnCount - 1;
										for (num14 = 0; num14 <= num30; num14++)
										{
											if (Operators.CompareString(text2, Strings.LCase(GridColRule.Columns[num14].HeaderText), TextCompare: false) == 0)
											{
												GridColRule.Rows[num8].Cells[num14].Value = Strings.LCase(text3);
												break;
											}
										}
									}
									break;
								}
								case "ROW-RULE":
								{
									num9++;
									text = Strings.Trim(array2[1]);
									GridRowRule.Rows[num9].Cells[0].Value = text;
									if (LikeOperator.LikeString(Strings.Trim(Strings.LCase(array2[3])), "background-color:*", CompareMethod.Binary))
									{
										num16 = 3;
										GridRowRule.Rows[num9].Cells[1].Value = "EQS";
										GridRowRule.Rows[num9].Cells[2].Value = array2[2];
									}
									else
									{
										num16 = 4;
										text = Strings.Trim(array2[2]);
										if (Operators.CompareString(text, "", TextCompare: false) == 0)
										{
											text = "EQS";
										}
										else
										{
											DataGridViewComboBoxColumn MyCombo = ColRuleROpr;
											BuildReport.Verify_Column(ref MyCombo, text);
											ColRuleROpr = MyCombo;
										}
										GridRowRule.Rows[num9].Cells[1].Value = text;
										GridRowRule.Rows[num9].Cells[2].Value = array2[3];
									}
									int num20 = num16;
									int num21 = Information.UBound(array2);
									for (num13 = num20; num13 <= num21; num13++)
									{
										text = array2[num13];
										num11 = Strings.InStr(text, ":");
										if (num11 == 0)
										{
											continue;
										}
										text2 = Strings.LCase(Strings.Trim(Strings.Mid(text, 1, num11 - 1)));
										text3 = Strings.Trim(Strings.Mid(text + " ", num11 + 1));
										int num22 = f_MaxObj;
										for (num14 = 0; num14 <= num22; num14++)
										{
											if ((Operators.CompareString(CSSObj[num14].ObjType, "headers", TextCompare: false) == 0) & (Operators.CompareString(text2, CSSObj[num14].Value, TextCompare: false) == 0))
											{
												text2 = Strings.LCase(CSSObj[num14].Name);
												break;
											}
										}
										int num23 = GridRowRule.ColumnCount - 1;
										for (num14 = 0; num14 <= num23; num14++)
										{
											if (Operators.CompareString(text2, Strings.LCase(GridRowRule.Columns[num14].HeaderText), TextCompare: false) == 0)
											{
												GridRowRule.Rows[num9].Cells[num14].Value = Strings.LCase(text3);
												break;
											}
										}
									}
									break;
								}
								}
							}
							DataGridView MyGrid = GridColRule;
							BuildChart.Format_Grid(ref MyGrid, 5);
							GridColRule = MyGrid;
							MyGrid = GridRowRule;
							BuildChart.Format_Grid(ref MyGrid, 4);
							GridRowRule = MyGrid;
							if (mnuAtTop.Checked)
							{
								Disable_AT_Top();
							}
							result = 0;
							lCSVCols = null;
							array2 = null;
						}
						else
						{
							Interaction.MsgBox("No CSV File was found in the report specification file. Please try another file", MsgBoxStyle.Exclamation, "Missing CSV File");
						}
						goto end_IL_0001;
					}
					case 4711:
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
				try0001_dispatch = 4711;
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
				Text = "SQLPathFinder Report Writer - [Internal=" + f_ReportSpecName + "] [External=" + text + "]";
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
					int num8;
					int num9;
					string text10;
					string text11;
					string text12;
					string text13;
					string text14;
					string text15;
					string text16;
					string text17;
					string text18;
					string text19;
					int NoCols;
					int num10;
					int num11;
					int num12;
					int num13;
					int num14;
					string text8;
					string text6;
					int num15;
					string text7;
					int num17;
					int num6;
					string text3;
					string text5;
					int num5;
					string text4;
					string text;
					string text2;
					short num7;
					int num21;
					int num3;
					switch (try0001_dispatch)
					{
					default:
					{
						result = 1;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmReport - Save_Report";
						MyRptSpec = "";
						NoCols = 0;
						num3 = 0;
						int num4 = 0;
						num5 = 0;
						text = "";
						text2 = "";
						text3 = "";
						text4 = "";
						text5 = "";
						text6 = "";
						text7 = "";
						text8 = "";
						num6 = 0;
						string text9 = "";
						num7 = 0;
						num8 = 0;
						num9 = 0;
						text10 = "Report";
						text11 = Strings.Trim(TxtCSVIn.Text);
						text12 = "SQLPathFinder.htm";
						text13 = Strings.Trim(Txtcss.Text);
						text14 = Strings.Trim(CmbColSpan.Text);
						text15 = "N";
						text16 = "";
						text17 = "";
						text18 = "";
						text19 = "";
						if (mnuEnableColDrillDown.Checked)
						{
							text15 = "Y";
						}
						if (mnuEnableColSort.Checked)
						{
							text16 = "Y";
						}
						if (mnuEnableDynFilter.Checked)
						{
							text17 = "Y";
						}
						if (mnuAtTop.Checked)
						{
							text18 = "Y";
						}
						if (mnuPreProc.Checked)
						{
							text19 = "Y";
						}
						if (GridTopOf.Rows.Count <= 0)
						{
							goto IL_01d9;
						}
						DataGridView MyGrid = GridTopOf;
						string obj = Distinct_Cols_Grid(ref MyGrid, -1);
						GridTopOf = MyGrid;
						text = obj;
						MyGrid = GridSort;
						string obj2 = Distinct_Cols_Grid(ref MyGrid, 0);
						GridSort = MyGrid;
						text2 = obj2;
						if (LikeOperator.LikeString(text2, text + "*", CompareMethod.Binary))
						{
							goto IL_01d9;
						}
						num7 = 1;
						Interaction.MsgBox("Column order in the \"Sort\" columns grid must match column order in the \"At Top Of\" columns grid where columns overlap.", MsgBoxStyle.Exclamation, "Incompatible Selections");
						goto end_IL_0001;
					}
					case 4612:
						{
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
						IL_02e3:
						if (!Check_Valid_Files(ref NoCols))
						{
							goto end_IL_0001_2;
						}
						if (NoCols < f_MinCols)
						{
							NoCols = f_MinCols;
						}
						NoCols += 2;
						MyRptSpec = "Type<\\\\>Key";
						num10 = NoCols - 2;
						for (num3 = 1; num3 <= num10; num3++)
						{
							MyRptSpec = MyRptSpec + "<\\\\>COL" + Strings.Trim(Conversions.ToString(num3));
						}
						MyRptSpec = MyRptSpec + "\r\nTYPE<\\\\>HTML" + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nINPUT-FILE<\\\\>" + text11 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nOUTPUT-FILE<\\\\>" + text12 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nCSS<\\\\>" + text13 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nCOLSPAN<\\\\>" + text14 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nDRILLDOWN<\\\\>" + text15 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nDYNAMICSORT<\\\\>" + text16 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nDYNAMICFILTER<\\\\>" + text17 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nATTOPDRILLDOWN<\\\\>" + text18 + BuildReport.Add_Delimiter(NoCols, 2);
						MyRptSpec = MyRptSpec + "\r\nNOPREPROCESS<\\\\>" + text19 + BuildReport.Add_Delimiter(NoCols, 2);
						num11 = GridSort.RowCount - 1;
						for (num3 = 0; num3 <= num11; num3++)
						{
							if (Operators.ConditionalCompareObjectNotEqual(GridSort.Rows[num3].Cells[0].Value, "", TextCompare: false) && Operators.CompareString(Strings.Mid(Conversions.ToString(GridSort.Rows[num3].Cells[0].Value), 1, 1), "=", TextCompare: false) != 0)
							{
								text = Conversions.ToString(GridSort.Rows[num3].Cells[0].Value);
								text2 = Conversions.ToString(GridSort.Rows[num3].Cells[1].Value);
								text3 = Conversions.ToString(GridSort.Rows[num3].Cells[2].Value);
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = "Asc";
								}
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = "N";
								}
								MyRptSpec = MyRptSpec + "\r\nSORT<\\\\>" + text + "<\\\\>" + text2 + "<\\\\>" + text3 + BuildReport.Add_Delimiter(NoCols, 4);
							}
						}
						num12 = GridRptHdr.RowCount - 1;
						for (num3 = 0; num3 <= num12; num3++)
						{
							text = Conversions.ToString(GridRptHdr.Rows[num3].Cells[0].Value);
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								text = Make_Tokens_UC(text);
								MyRptSpec = MyRptSpec + "\r\nAT-TOP-OF-REPORT<\\\\><\\\\>" + text + BuildReport.Add_Delimiter(NoCols, 3);
							}
						}
						num13 = GridTopOf.RowCount - 1;
						for (num3 = 0; num3 <= num13; num3++)
						{
							text = Conversions.ToString(GridTopOf.Rows[num3].HeaderCell.Value);
							if (Operators.CompareString(Strings.LCase(text), "at top of", TextCompare: false) == 0)
							{
								continue;
							}
							text = General_Procedures.Get_Node_Value(text);
							text2 = Get_Sort_Col_No(text);
							text3 = Conversions.ToString(GridTopOf.Rows[num3].Cells[0].Value);
							if ((Operators.CompareString(text2, "-1", TextCompare: false) != 0) & (Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text3, "", TextCompare: false) != 0))
							{
								if (Conversions.ToInteger(text2) > num8)
								{
									num8 = Conversions.ToInteger(text2);
								}
								text3 = Make_Tokens_UC(text3);
								MyRptSpec = MyRptSpec + "\r\nAT-TOP-OF-COL" + text2 + "<\\\\>" + text + "<\\\\>" + text3 + BuildReport.Add_Delimiter(NoCols, 3);
							}
						}
						text = "";
						text2 = "";
						text3 = "";
						text4 = "";
						text5 = "";
						text6 = "";
						text8 = "";
						num6 = 0;
						num14 = GridCols.RowCount - 1;
						for (num3 = 0; num3 <= num14; num3++)
						{
							text = Conversions.ToString(GridCols.Rows[num3].Cells[0].Value);
							text2 = Conversions.ToString(GridCols.Rows[num3].Cells[1].Value);
							text3 = Conversions.ToString(GridCols.Rows[num3].Cells[2].Value);
							text8 = Conversions.ToString(GridCols.Rows[num3].Cells[3].Value);
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text, 1, 1), "=", TextCompare: false) != 0)
							{
								num6++;
								text4 = text4 + "<\\\\>" + text;
								text5 = text5 + "<\\\\>" + text2;
								text6 = text6 + "<\\\\>" + text3;
								text7 = text7 + "<\\\\>" + text8;
							}
						}
						num6 += 2;
						MyRptSpec = MyRptSpec + "\r\nCOLUMN-DATA<\\\\>" + text4 + BuildReport.Add_Delimiter(NoCols, num6);
						MyRptSpec = MyRptSpec + "\r\nCOLUMN-HEADERS<\\\\>" + text5 + BuildReport.Add_Delimiter(NoCols, num6);
						MyRptSpec = MyRptSpec + "\r\nCOLUMN-ALIGNMENT<\\\\>" + text6 + BuildReport.Add_Delimiter(NoCols, num6);
						MyRptSpec = MyRptSpec + "\r\nCOLUMN-FORMAT<\\\\>" + text7 + BuildReport.Add_Delimiter(NoCols, num6);
						text = "";
						text2 = "";
						text3 = "";
						text4 = "";
						text5 = "";
						text6 = "";
						text7 = "";
						num5 = 0;
						num15 = GridCols.ColumnCount - 1;
						for (int num4 = 4; num4 <= num15; num4++)
						{
							text4 = "";
							text7 = GridCols.Columns[num4].HeaderText;
							if (Operators.CompareString(Strings.LCase(text7), "at bottom of", TextCompare: false) == 0)
							{
								continue;
							}
							text7 = General_Procedures.Get_Node_Value(text7);
							string text9;
							if (Operators.CompareString(Strings.LCase(text7), "report", TextCompare: false) == 0)
							{
								text9 = "-REPORT";
							}
							else
							{
								text9 = Get_Sort_Col_No(text7);
								if (Conversions.ToInteger(text9) > num9)
								{
									num9 = Conversions.ToInteger(text9);
									text10 = text7;
								}
								text9 = "-COL" + text9;
							}
							if (Operators.CompareString(Strings.LCase(text7), "report", TextCompare: false) == 0)
							{
								text7 = "";
							}
							if (Operators.CompareString(text9, "-COL-1", TextCompare: false) == 0)
							{
								continue;
							}
							text9 = "AT-BOT-OF" + text9;
							int num16 = GridCols.RowCount - 1;
							for (num3 = 0; num3 <= num16; num3++)
							{
								text = Conversions.ToString(GridCols.Rows[num3].Cells[0].Value);
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									text2 = Conversions.ToString(GridCols.Rows[num3].Cells[num4].Value);
									switch (Strings.UCase(text2))
									{
									case "TEXT:":
										text3 = Conversions.ToString(Operators.ConcatenateObject("TEXT:", GridCols.Rows[num3].Cells[num4].Tag));
										break;
									case "EXPR:":
										text3 = Conversions.ToString(Operators.ConcatenateObject("EXP2:", GridCols.Rows[num3].Cells[num4].Tag));
										break;
									case null:
									case "":
										text3 = "";
										break;
									default:
										text3 = Conversions.ToString(Operators.ConcatenateObject("EXP1:", GridCols.Rows[num3].Cells[num4].Value));
										break;
									}
									text4 = text4 + "<\\\\>" + text3;
								}
							}
							if (Operators.CompareString(text9, "AT-BOT-OF-REPORT", TextCompare: false) == 0)
							{
								text = "";
							}
							MyRptSpec = MyRptSpec + "\r\n" + text9 + "<\\\\>" + text7 + text4 + BuildReport.Add_Delimiter(NoCols, num6);
						}
						if (unchecked(Operators.CompareString(text15, "Y", TextCompare: false) == 0 && num9 > num8))
						{
							Interaction.MsgBox("For Drill-Down reports, the highest \"At Top of\" break must be the same or greater than the highest At Bottom of break. Please either add an \"At Top of " + text10 + "\" break or remove the Drill-Down requirement.", MsgBoxStyle.Exclamation, "Invalid Report Condition");
							goto end_IL_0001_2;
						}
						num6 = 0;
						num17 = GridColRule.RowCount - 1;
						num3 = 0;
						while (true)
						{
							if (num3 <= num17)
							{
								text = Conversions.ToString(GridColRule.Rows[num3].Cells[0].Value);
								text2 = Conversions.ToString(GridColRule.Rows[num3].Cells[1].Value);
								text3 = Conversions.ToString(GridColRule.Rows[num3].Cells[2].Value);
								text4 = Conversions.ToString(GridColRule.Rows[num3].Cells[3].Value);
								if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text, 1, 1), "=", TextCompare: false) != 0)
								{
									if (!Chk_Col_Rule(text, text2))
									{
										Interaction.MsgBox("Column patterns for a column rule must be identical if used in both the display and the test column. (Display Pattern=" + text + ", and Test Pattern=" + text2 + ")", MsgBoxStyle.Exclamation, "Invalid Column Patterns Used in a Column Rule");
										break;
									}
									num6++;
									MyRptSpec = MyRptSpec + "\r\nCOLUMN-RULE" + Conversions.ToString(num6) + "<\\\\>" + text + "<\\\\>" + text2 + "<\\\\>" + text3 + "<\\\\>" + text4;
									text4 = "";
									text5 = "";
									num5 = 0;
									int num18 = GridColRule.ColumnCount - 1;
									for (int num4 = 4; num4 <= num18; num4++)
									{
										text4 = Conversions.ToString(GridColRule.Rows[num3].Cells[num4].Value);
										if (Operators.CompareString(text4, "", TextCompare: false) != 0)
										{
											text5 = text5 + "<\\\\>" + BuildReport.Get_CSS_Attr(GridColRule.Columns[num4].HeaderText, f_MaxObj, ref CSSObj) + ":" + text4;
											num5++;
										}
									}
									MyRptSpec = MyRptSpec + text5 + BuildReport.Add_Delimiter(NoCols, 5 + num5);
								}
								num3++;
								continue;
							}
							num6 = 0;
							int num19 = GridRowRule.RowCount - 1;
							for (num3 = 0; num3 <= num19; num3++)
							{
								text = Conversions.ToString(GridRowRule.Rows[num3].Cells[0].Value);
								text2 = Conversions.ToString(GridRowRule.Rows[num3].Cells[1].Value);
								text3 = Conversions.ToString(GridRowRule.Rows[num3].Cells[2].Value);
								if (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text2, "", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text, 1, 1), "=", TextCompare: false) == 0)
								{
									continue;
								}
								num6++;
								MyRptSpec = MyRptSpec + "\r\nROW-RULE" + Conversions.ToString(num6) + "<\\\\>" + text + "<\\\\>" + text2 + "<\\\\>" + text3;
								text4 = "";
								text5 = "";
								num5 = 0;
								int num20 = GridRowRule.ColumnCount - 1;
								for (int num4 = 3; num4 <= num20; num4++)
								{
									text4 = Conversions.ToString(GridRowRule.Rows[num3].Cells[num4].Value);
									if (Operators.CompareString(text4, "", TextCompare: false) != 0)
									{
										text5 = text5 + "<\\\\>" + BuildReport.Get_CSS_Attr(GridRowRule.Columns[num4].HeaderText, f_MaxObj, ref CSSObj) + ":" + text4;
										num5++;
									}
								}
								MyRptSpec = MyRptSpec + text5 + BuildReport.Add_Delimiter(NoCols, 4 + num5);
							}
							result = 0;
							break;
						}
						goto end_IL_0001;
						IL_01d9:
						text = "";
						text2 = "";
						if (!(mnuEnableColSort.Checked | mnuEnableDynFilter.Checked))
						{
							goto IL_02e3;
						}
						num7 = 0;
						num21 = GridSort.RowCount - 1;
						for (num3 = 0; num3 <= num21; num3++)
						{
							if (Operators.ConditionalCompareObjectNotEqual(GridSort.Rows[num3].Cells[0].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridSort.Rows[num3].Cells[2].Value, "Y", TextCompare: false))
							{
								num7 = 1;
								break;
							}
						}
						if (num7 == 0 && Operators.CompareString(CmbColSpan.Text, "", TextCompare: false) != 0)
						{
							num7 = 1;
						}
						if (num7 != 1)
						{
							goto IL_02e3;
						}
						Interaction.MsgBox("SQLPathFinder does not allow Row/Column spanning with dynamic column sorting nor dynamic row filtering (Report Options).", MsgBoxStyle.Exclamation, "Incompatible Selections");
						goto end_IL_0001;
					}
					goto IL_123a;
				}
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 4612;
				continue;
			}
			break;
			IL_123a:
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_2:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public string Get_Sort_Col_No(string MyCol)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string result = default(string);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Type typeFromHandle;
					object[] obj;
					DataGridViewCell dataGridViewCell;
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
					case 391:
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
								goto IL_001e;
							case 6:
								goto IL_0028;
							case 7:
								goto IL_002d;
							case 8:
								goto IL_0046;
							case 9:
								goto IL_007e;
							case 10:
								goto IL_0087;
							case 11:
								goto end_IL_0001_2;
							case 13:
							case 14:
							case 15:
								goto IL_0120;
							default:
								goto end_IL_0001;
							case 12:
							case 16:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_007e:
						num2 = 9;
						num5++;
						goto IL_0087;
						IL_0087:
						num2 = 10;
						typeFromHandle = typeof(Strings);
						obj = new object[1] { (dataGridViewCell = GridSort.Rows[num6].Cells[0]).Value };
						array = obj;
						obj2 = new bool[1] { true };
						array2 = obj2;
						left = NewLateBinding.LateGet(null, typeFromHandle, "LCase", obj, null, null, obj2);
						if (array2[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (Operators.ConditionalCompareObjectEqual(left, MyCol, TextCompare: false))
						{
							break;
						}
						goto IL_0120;
						IL_0046:
						num2 = 8;
						if (Operators.ConditionalCompareObjectNotEqual(GridSort.Rows[num6].Cells[0].Value, "", TextCompare: false))
						{
							goto IL_007e;
						}
						goto IL_0120;
						IL_0120:
						num2 = 15;
						num6++;
						goto IL_0129;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num5 = 0;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						result = Conversions.ToString(-1);
						goto IL_001e;
						IL_001e:
						num2 = 5;
						MyCol = Strings.LCase(MyCol);
						goto IL_0028;
						IL_0028:
						num2 = 6;
						num5 = 0;
						goto IL_002d;
						IL_002d:
						num2 = 7;
						num7 = GridSort.RowCount - 1;
						num6 = 0;
						goto IL_0129;
						IL_0129:
						if (num6 > num7)
						{
							goto end_IL_0001_3;
						}
						goto IL_0046;
						end_IL_0001_2:
						break;
					}
					num2 = 11;
					result = Strings.Trim(Conversions.ToString(num5));
					break;
				}
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 391;
				continue;
			}
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

	public void Load_Grid_Format_Values(string MyKey, ref DataGridViewComboBoxColumn Cmb1, ref DataGridViewComboBoxColumn Cmb2)
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
				case 345:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001e;
						case 5:
							goto IL_002d;
						case 6:
							goto IL_003c;
						case 7:
							goto IL_0050;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_0075;
						case 10:
							goto IL_00b9;
						case 11:
							goto IL_00da;
						case 12:
						case 13:
							goto IL_00fd;
						default:
							goto end_IL_0001;
						case 14:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_00b9:
					num2 = 10;
					Cmb1.Items.Add(CSSObj[num5].Value);
					goto IL_00da;
					IL_00da:
					num2 = 11;
					Cmb2.Items.Add(CSSObj[num5].Value);
					goto IL_00fd;
					IL_0075:
					num2 = 9;
					if ((Operators.CompareString(CSSObj[num5].ObjType, "values", TextCompare: false) == 0) & (Operators.CompareString(CSSObj[num5].Name, MyKey, TextCompare: false) == 0))
					{
						goto IL_00b9;
					}
					goto IL_00fd;
					IL_00fd:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_0104;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyKey = Strings.LCase(Strings.Trim(MyKey));
					goto IL_001e;
					IL_001e:
					num2 = 4;
					Cmb1.Items.Clear();
					goto IL_002d;
					IL_002d:
					num2 = 5;
					Cmb2.Items.Clear();
					goto IL_003c;
					IL_003c:
					num2 = 6;
					Cmb1.Items.Add("");
					goto IL_0050;
					IL_0050:
					num2 = 7;
					Cmb2.Items.Add("");
					goto IL_0064;
					IL_0064:
					num2 = 8;
					num6 = f_MaxObj;
					num5 = 1;
					goto IL_0104;
					IL_0104:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0075;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 345;
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

	public void Resize_Form()
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
					case 459:
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
								goto IL_0018;
							case 5:
								goto IL_001d;
							case 6:
								goto IL_0022;
							case 7:
								goto IL_0028;
							case 8:
								goto IL_0052;
							case 9:
								goto IL_0085;
							case 10:
								goto IL_00ae;
							case 11:
								goto IL_00b4;
							case 12:
								goto IL_00c6;
							case 13:
								goto IL_00d4;
							case 14:
								goto IL_00fb;
							case 15:
								goto IL_010a;
							case 16:
								goto IL_011c;
							case 17:
								goto IL_0122;
							case 18:
								goto IL_0130;
							case 19:
								goto IL_0157;
							default:
								goto end_IL_0001;
							case 20:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0157:
						num2 = 19;
						num5++;
						goto IL_0160;
						IL_00d4:
						num2 = 13;
						GridColRule.Columns[num5].Width = (int)Math.Round((double)num6 / (double)num7);
						goto IL_00fb;
						IL_0130:
						num2 = 18;
						GridRowRule.Columns[num5].Width = (int)Math.Round((double)num6 / (double)num7);
						goto IL_0157;
						IL_00fb:
						num2 = 14;
						num5++;
						goto IL_0104;
						IL_000b:
						num2 = 2;
						Refresh_Combo();
						goto IL_0014;
						IL_0014:
						num2 = 3;
						num6 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num5 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						num7 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						num8 = 70;
						goto IL_0028;
						IL_0028:
						num2 = 7;
						GridSort.Columns[0].Width = GridSort.Width - 280;
						goto IL_0052;
						IL_0052:
						num2 = 8;
						GridTopOf.Columns[0].Width = GridTopOf.Width - 19 - GridTopOf.RowHeadersWidth;
						goto IL_0085;
						IL_0085:
						num2 = 9;
						GridRptHdr.Columns[num5].Width = GridRptHdr.Width - 80;
						goto IL_00ae;
						IL_00ae:
						num2 = 10;
						num7 = 6;
						goto IL_00b4;
						IL_00b4:
						num2 = 11;
						num6 = GridColRule.Width - num8;
						goto IL_00c6;
						IL_00c6:
						num2 = 12;
						num9 = num7 - 1;
						num5 = 0;
						goto IL_0104;
						IL_0104:
						if (num5 <= num9)
						{
							goto IL_00d4;
						}
						goto IL_010a;
						IL_010a:
						num2 = 15;
						num6 = GridRowRule.Width - num8;
						goto IL_011c;
						IL_011c:
						num2 = 16;
						num7 = 5;
						goto IL_0122;
						IL_0122:
						num2 = 17;
						num10 = num7 - 1;
						num5 = 0;
						goto IL_0160;
						IL_0160:
						if (num5 > num10)
						{
							goto end_IL_0001_2;
						}
						goto IL_0130;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 459;
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

	public void Clear_All_Grids()
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
				checked
				{
					DataGridView MyGrid;
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
								goto IL_0022;
							case 5:
								goto IL_003d;
							case 6:
								goto IL_0058;
							case 7:
								goto IL_0073;
							case 8:
								goto IL_008e;
							case 9:
								goto IL_00a9;
							case 10:
								goto IL_00c5;
							case 11:
								goto IL_00db;
							case 12:
								goto IL_00fa;
							case 13:
								goto IL_0106;
							case 14:
								goto IL_011c;
							case 15:
								goto IL_0140;
							case 16:
								goto IL_014c;
							case 17:
								goto IL_0160;
							case 18:
								goto IL_0179;
							case 19:
								goto IL_018d;
							case 20:
								goto IL_01a6;
							case 21:
								goto IL_01bf;
							case 22:
								goto IL_01d8;
							case 23:
								goto IL_01f1;
							case 24:
								goto IL_020a;
							case 25:
								goto IL_0214;
							case 26:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 27:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0140:
						num2 = 15;
						num5++;
						goto IL_0147;
						IL_00db:
						num2 = 11;
						GridCols.Columns[num5].HeaderText = "At Bottom of";
						goto IL_00fa;
						IL_011c:
						num2 = 14;
						GridTopOf.Rows[num5].HeaderCell.Value = "At Top of";
						goto IL_0140;
						IL_00fa:
						num2 = 12;
						num5++;
						goto IL_0101;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						Txtcss.Text = "sqlpathfinder_style_1.css";
						goto IL_0022;
						IL_0022:
						num2 = 4;
						MyGrid = GridRptHdr;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridRptHdr = MyGrid;
						goto IL_003d;
						IL_003d:
						num2 = 5;
						MyGrid = GridSort;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridSort = MyGrid;
						goto IL_0058;
						IL_0058:
						num2 = 6;
						MyGrid = GridTopOf;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridTopOf = MyGrid;
						goto IL_0073;
						IL_0073:
						num2 = 7;
						MyGrid = GridCols;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridCols = MyGrid;
						goto IL_008e;
						IL_008e:
						num2 = 8;
						MyGrid = GridColRule;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridColRule = MyGrid;
						goto IL_00a9;
						IL_00a9:
						num2 = 9;
						MyGrid = GridRowRule;
						GridModule.Clear_A_Grid(ref MyGrid);
						GridRowRule = MyGrid;
						goto IL_00c5;
						IL_00c5:
						num2 = 10;
						num6 = GridCols.ColumnCount - 1;
						num5 = 4;
						goto IL_0101;
						IL_0101:
						if (num5 <= num6)
						{
							goto IL_00db;
						}
						goto IL_0106;
						IL_0106:
						num2 = 13;
						num7 = GridTopOf.RowCount - 1;
						num5 = 0;
						goto IL_0147;
						IL_0147:
						if (num5 <= num7)
						{
							goto IL_011c;
						}
						goto IL_014c;
						IL_014c:
						num2 = 16;
						ColName.Items.Clear();
						goto IL_0160;
						IL_0160:
						num2 = 17;
						ColName.Items.Add("");
						goto IL_0179;
						IL_0179:
						num2 = 18;
						ColCol.Items.Clear();
						goto IL_018d;
						IL_018d:
						num2 = 19;
						ColCol.Items.Add("");
						goto IL_01a6;
						IL_01a6:
						num2 = 20;
						ColCol.Items.Add("Starts With:");
						goto IL_01bf;
						IL_01bf:
						num2 = 21;
						ColCol.Items.Add("Ends With:");
						goto IL_01d8;
						IL_01d8:
						num2 = 22;
						ColCol.Items.Add("Contains:");
						goto IL_01f1;
						IL_01f1:
						num2 = 23;
						ColCol.Items.Add("Starts/Ends With (%):");
						goto IL_020a;
						IL_020a:
						num2 = 24;
						Initialize_Formats();
						goto IL_0214;
						IL_0214:
						num2 = 25;
						GridCols.RowCount = 0;
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 26;
					fTitle = "untitled.vgs";
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

	public void Initialize_Formats()
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
				case 1202:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0034;
						case 4:
							goto IL_005d;
						case 5:
							goto IL_0086;
						case 6:
							goto IL_00b0;
						case 7:
							goto IL_00d9;
						case 8:
							goto IL_0102;
						case 9:
							goto IL_012b;
						case 10:
							goto IL_0156;
						case 11:
							goto IL_0180;
						case 12:
							goto IL_01aa;
						case 13:
							goto IL_01d4;
						case 14:
							goto IL_01ff;
						case 15:
							goto IL_0229;
						case 16:
							goto IL_0253;
						case 17:
							goto IL_027d;
						case 18:
							goto IL_02a7;
						case 19:
							goto IL_02d1;
						case 20:
							goto IL_02fb;
						case 21:
							goto IL_0325;
						case 22:
							goto IL_034f;
						case 23:
							goto IL_0379;
						case 24:
							goto IL_03a3;
						case 25:
							goto IL_03cd;
						case 26:
							goto IL_03f7;
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 28:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_03a3:
					num2 = 24;
					GridRowRule.Rows[2].Cells[7].Value = "normal";
					goto IL_03cd;
					IL_03cd:
					num2 = 25;
					GridRowRule.Rows[2].Cells[8].Value = "bold";
					goto IL_03f7;
					IL_0379:
					num2 = 23;
					GridRowRule.Rows[2].Cells[4].Value = "white";
					goto IL_03a3;
					IL_03f7:
					num2 = 26;
					MyGrid = GridColRule;
					BuildChart.Format_Grid(ref MyGrid, 5);
					GridColRule = MyGrid;
					break;
					IL_000b:
					num2 = 2;
					GridColRule.Rows[0].Cells[4].Value = "red";
					goto IL_0034;
					IL_0034:
					num2 = 3;
					GridColRule.Rows[0].Cells[5].Value = "white";
					goto IL_005d;
					IL_005d:
					num2 = 4;
					GridColRule.Rows[0].Cells[8].Value = "normal";
					goto IL_0086;
					IL_0086:
					num2 = 5;
					GridColRule.Rows[0].Cells[9].Value = "bold";
					goto IL_00b0;
					IL_00b0:
					num2 = 6;
					GridColRule.Rows[1].Cells[4].Value = "orange";
					goto IL_00d9;
					IL_00d9:
					num2 = 7;
					GridColRule.Rows[1].Cells[5].Value = "white";
					goto IL_0102;
					IL_0102:
					num2 = 8;
					GridColRule.Rows[1].Cells[8].Value = "normal";
					goto IL_012b;
					IL_012b:
					num2 = 9;
					GridColRule.Rows[1].Cells[9].Value = "bold";
					goto IL_0156;
					IL_0156:
					num2 = 10;
					GridColRule.Rows[2].Cells[4].Value = "lightgreen";
					goto IL_0180;
					IL_0180:
					num2 = 11;
					GridColRule.Rows[2].Cells[5].Value = "white";
					goto IL_01aa;
					IL_01aa:
					num2 = 12;
					GridColRule.Rows[2].Cells[8].Value = "normal";
					goto IL_01d4;
					IL_01d4:
					num2 = 13;
					GridColRule.Rows[2].Cells[9].Value = "bold";
					goto IL_01ff;
					IL_01ff:
					num2 = 14;
					GridRowRule.Rows[0].Cells[3].Value = "red";
					goto IL_0229;
					IL_0229:
					num2 = 15;
					GridRowRule.Rows[0].Cells[4].Value = "white";
					goto IL_0253;
					IL_0253:
					num2 = 16;
					GridRowRule.Rows[0].Cells[7].Value = "normal";
					goto IL_027d;
					IL_027d:
					num2 = 17;
					GridRowRule.Rows[0].Cells[8].Value = "bold";
					goto IL_02a7;
					IL_02a7:
					num2 = 18;
					GridRowRule.Rows[1].Cells[3].Value = "orange";
					goto IL_02d1;
					IL_02d1:
					num2 = 19;
					GridRowRule.Rows[1].Cells[4].Value = "white";
					goto IL_02fb;
					IL_02fb:
					num2 = 20;
					GridRowRule.Rows[1].Cells[7].Value = "normal";
					goto IL_0325;
					IL_0325:
					num2 = 21;
					GridRowRule.Rows[1].Cells[8].Value = "bold";
					goto IL_034f;
					IL_034f:
					num2 = 22;
					GridRowRule.Rows[2].Cells[3].Value = "lightgreen";
					goto IL_0379;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				MyGrid = GridRowRule;
				BuildChart.Format_Grid(ref MyGrid, 4);
				GridRowRule = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1202;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmReport_FormClosing(object sender, FormClosingEventArgs e)
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

	private void FrmReport_Load(object sender, EventArgs e)
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
					errsource = "FrmReport - Form_Load";
					int num3 = 0;
					Button MyButton = CmdBrowseIn;
					BuildForm.Set_Btn_Img(ref MyButton, "browse");
					CmdBrowseIn = MyButton;
					MyButton = cmdEditVar;
					BuildForm.Set_Btn_Img(ref MyButton, "edit");
					cmdEditVar = MyButton;
					MyButton = cmdbrowsecss;
					BuildForm.Set_Btn_Img(ref MyButton, "browse");
					cmdbrowsecss = MyButton;
					MyButton = CmdUp1;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp1 = MyButton;
					MyButton = CmdDown1;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown1 = MyButton;
					MyButton = CmdDel1;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel1 = MyButton;
					MyButton = CmdUp2;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp2 = MyButton;
					MyButton = CmdDown2;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown2 = MyButton;
					MyButton = CmdDel2;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel2 = MyButton;
					MyButton = CmdUp3;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp3 = MyButton;
					MyButton = CmdDown3;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown3 = MyButton;
					MyButton = CmdDel3;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel3 = MyButton;
					MyButton = CmdUp4;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp4 = MyButton;
					MyButton = CmdDown4;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown4 = MyButton;
					MyButton = CmdDel4;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel4 = MyButton;
					MyButton = CmdLeft4;
					BuildForm.Set_Btn_Img(ref MyButton, "left");
					CmdLeft4 = MyButton;
					MyButton = CmdRight4;
					BuildForm.Set_Btn_Img(ref MyButton, "right");
					CmdRight4 = MyButton;
					MyButton = CmdUp5;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp5 = MyButton;
					MyButton = CmdDown5;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown5 = MyButton;
					MyButton = CmdDel5;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel5 = MyButton;
					MyButton = CmdUp6;
					BuildForm.Set_Btn_Img(ref MyButton, "up");
					CmdUp6 = MyButton;
					MyButton = CmdDown6;
					BuildForm.Set_Btn_Img(ref MyButton, "down");
					CmdDown6 = MyButton;
					MyButton = CmdDel6;
					BuildForm.Set_Btn_Img(ref MyButton, "delete");
					CmdDel6 = MyButton;
					f_MinCols = checked(GridColRule.ColumnCount - 1);
					BuildReport.Load_CSSObj(ref CSSObj, ref f_MaxObj);
					GridSort.RowCount = 15;
					GridRptHdr.RowCount = 10;
					GridColRule.RowCount = 100;
					GridRowRule.RowCount = 100;
					GridTopOf.RowCount = 50;
					DataGridView MyGrid = GridSort;
					GridModule.Number_Grid(ref MyGrid);
					GridSort = MyGrid;
					MyGrid = GridRptHdr;
					GridModule.Number_Grid(ref MyGrid);
					GridRptHdr = MyGrid;
					MyGrid = GridColRule;
					GridModule.Number_Grid(ref MyGrid);
					GridColRule = MyGrid;
					MyGrid = GridRowRule;
					GridModule.Number_Grid(ref MyGrid);
					GridRowRule = MyGrid;
					DataGridViewComboBoxColumn Cmb = ColRuleCTA;
					DataGridViewComboBoxColumn Cmb2 = ColRuleRTA;
					Load_Grid_Format_Values("text-align", ref Cmb, ref Cmb2);
					ColRuleRTA = Cmb2;
					ColRuleCTA = Cmb;
					Cmb2 = ColRuleCVA;
					Cmb = ColRuleRVA;
					Load_Grid_Format_Values("valign", ref Cmb2, ref Cmb);
					ColRuleRVA = Cmb;
					ColRuleCVA = Cmb2;
					Clear_All_Grids();
					TxtCSVIn.Text = "";
					Add_Query_To_Title();
					if (Operators.CompareString(f_ReportSpec, "", TextCompare: false) != 0)
					{
						Open_Report(f_ReportSpec);
					}
					MyGrid = GridCols;
					GridModule.Number_Grid(ref MyGrid);
					GridCols = MyGrid;
					IsStartUp = false;
					goto end_IL_0001;
				}
				case 1178:
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
				try0001_dispatch = 1178;
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

	private void CmdBrowseIn_Click(object sender, EventArgs e)
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
					errsource = "FrmReport - CmdBrowseIn_Click";
					string text = Strings.Trim(TxtCSVIn.Text);
					string[] lCSVCols = new string[0];
					short num3 = 0;
					string text2 = "";
					string text3 = "";
					bool flag = false;
					string MyMissData = "";
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					text2 = text;
					BuildForm.FileOpenSave("O", text, "csv+", "Find CSV File", "");
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						num5 = (int)Interaction.MsgBox("Loading the new file normally clears your report design. Do you wish to proceed and start a blank report (Yes), to cancel the action (Cancel) or to proceed but preserve the current report definition even if it may be incompatible with the new file (No)?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Load File and clear (Yes), Load File and Preserve Design (No) or Cancel Action (Cancel)?");
						if (num5 == 2)
						{
							goto end_IL_0001;
						}
					}
					else
					{
						num5 = 6;
					}
					if (General_Procedures.GetCSVHeaders(text, ref lCSVCols) != 0)
					{
						goto end_IL_0001;
					}
					TxtCSVIn.Text = BuildForm.Strip_Add_MyPCDir("S", text);
					checked
					{
						DataGridView MyGrid;
						switch (num5)
						{
						case 6:
						{
							Clear_All_Grids();
							GridCols.RowCount = Information.UBound(lCSVCols) + 1;
							int num11 = Information.UBound(lCSVCols);
							for (num4 = 0; num4 <= num11; num4++)
							{
								ColName.Items.Add(lCSVCols[num4]);
								ColCol.Items.Add(lCSVCols[num4]);
								GridCols.Rows[num4].Cells[0].Value = lCSVCols[num4];
								GridCols.Rows[num4].Cells[1].Value = Strings.Replace(Strings.StrConv(lCSVCols[num4], VbStrConv.ProperCase), "_", " ", 1, -1, CompareMethod.Text);
								GridCols.Rows[num4].Cells[2].Value = "middle-left";
							}
							break;
						}
						case 7:
						{
							MyGrid = GridCols;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 0, ref MyMissData, 1);
							GridCols = MyGrid;
							MyGrid = GridColRule;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 0, ref MyMissData, 0);
							GridColRule = MyGrid;
							MyGrid = GridColRule;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 1, ref MyMissData, 0);
							GridColRule = MyGrid;
							MyGrid = GridRowRule;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 0, ref MyMissData, 0);
							GridRowRule = MyGrid;
							num6 = ColName.Items.Count;
							num7 = ColCol.Items.Count;
							int num8 = num6 - 1;
							for (num4 = num8; num4 >= 1; num4 += -1)
							{
								ColName.Items.RemoveAt(num4);
							}
							int num9 = num7 - 1;
							for (num4 = num9; num4 >= 5; num4 += -1)
							{
								ColCol.Items.RemoveAt(num4);
							}
							int num10 = Information.UBound(lCSVCols);
							for (num4 = 0; num4 <= num10; num4++)
							{
								ColName.Items.Add(lCSVCols[num4]);
								ColCol.Items.Add(lCSVCols[num4]);
							}
							if (Operators.CompareString(MyMissData, "", TextCompare: false) != 0)
							{
								Interaction.MsgBox("Note that the following columns were not found in the report file. Please correct your query to avoid errors when you run the report: (" + MyMissData + ").", MsgBoxStyle.Exclamation, "Missing Columns");
							}
							break;
						}
						}
						lCSVCols = null;
						MyGrid = GridCols;
						GridModule.Number_Grid(ref MyGrid);
						GridCols = MyGrid;
						goto end_IL_0001;
					}
				}
				case 1006:
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
				try0001_dispatch = 1006;
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

	private void CmdUp1_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridSort;
				GridModule.Grid_Up(ref MyGrid);
				GridSort = MyGrid;
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

	private void CmdDown1_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridSort;
				GridModule.Grid_Down(ref MyGrid);
				GridSort = MyGrid;
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

	private void CmdUp2_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridRptHdr;
				GridModule.Grid_Up(ref MyGrid);
				GridRptHdr = MyGrid;
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

	private void CmdDown2_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridRptHdr;
				GridModule.Grid_Down(ref MyGrid);
				GridRptHdr = MyGrid;
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

	private void CmdDel1_Click(object sender, EventArgs e)
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
				case 69:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				DataGridView MyGrid = GridSort;
				Grid_Row_DelR(ref MyGrid, 15);
				GridSort = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 69;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdDel2_Click(object sender, EventArgs e)
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
				case 69:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				DataGridView MyGrid = GridRptHdr;
				Grid_Row_DelR(ref MyGrid, 10);
				GridRptHdr = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 69;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdUp4_Click(object sender, EventArgs e)
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
				case 101:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (GridCols.RowCount > 0)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView MyGrid = GridCols;
				GridModule.Grid_Up(ref MyGrid);
				GridCols = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 101;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdUp3_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string value = default(string);
		int rowIndex = default(int);
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
							goto IL_0013;
						case 4:
							goto IL_0027;
						case 6:
							goto IL_003c;
						case 7:
							goto IL_0060;
						case 8:
							goto IL_00a2;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003c:
					num2 = 6;
					value = Conversions.ToString(GridTopOf.Rows[rowIndex].HeaderCell.Value);
					goto IL_0060;
					IL_0060:
					num2 = 7;
					GridTopOf.Rows[rowIndex].HeaderCell.Value = RuntimeHelpers.GetObjectValue(GridTopOf.Rows[checked(rowIndex - 1)].HeaderCell.Value);
					goto IL_00a2;
					IL_0027:
					num2 = 4;
					if (rowIndex <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_003c;
					IL_00a2:
					num2 = 8;
					GridTopOf.Rows[checked(rowIndex - 1)].HeaderCell.Value = value;
					break;
					IL_000b:
					num2 = 2;
					value = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					rowIndex = GridTopOf.CurrentCell.RowIndex;
					goto IL_0027;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				DataGridView MyGrid = GridTopOf;
				GridModule.Grid_Up(ref MyGrid);
				GridTopOf = MyGrid;
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

	private void CmdDown3_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowIndex = default(int);
		string value = default(string);
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
					case 299:
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
								goto IL_0013;
							case 4:
								goto IL_0027;
							case 6:
								goto IL_004d;
							case 7:
								goto IL_0071;
							case 8:
								goto IL_00b3;
							case 9:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 5:
							case 10:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00b3:
						num2 = 8;
						GridTopOf.Rows[rowIndex + 1].HeaderCell.Value = value;
						break;
						IL_004d:
						num2 = 6;
						value = Conversions.ToString(GridTopOf.Rows[rowIndex].HeaderCell.Value);
						goto IL_0071;
						IL_0027:
						num2 = 4;
						if (rowIndex < 0 || rowIndex == GridTopOf.RowCount - 1)
						{
							goto end_IL_0001_3;
						}
						goto IL_004d;
						IL_0071:
						num2 = 7;
						GridTopOf.Rows[rowIndex].HeaderCell.Value = RuntimeHelpers.GetObjectValue(GridTopOf.Rows[rowIndex + 1].HeaderCell.Value);
						goto IL_00b3;
						IL_000b:
						num2 = 2;
						value = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						rowIndex = GridTopOf.CurrentCell.RowIndex;
						goto IL_0027;
						end_IL_0001_2:
						break;
					}
					num2 = 9;
					DataGridView MyGrid = GridTopOf;
					GridModule.Grid_Down(ref MyGrid);
					GridTopOf = MyGrid;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 299;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdDown4_Click(object sender, EventArgs e)
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
				case 101:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (GridCols.RowCount > 0)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView MyGrid = GridCols;
				GridModule.Grid_Down(ref MyGrid);
				GridCols = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 101;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdDel3_Click(object sender, EventArgs e)
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
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
							goto IL_000b;
						case 3:
							goto IL_000f;
						case 4:
							goto IL_002a;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_0077;
						case 7:
							goto IL_009a;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_003f:
					num2 = 5;
					if (Conversions.ToBoolean(Operators.NotObject(LikeOperator.LikeObject(GridTopOf.Rows[num5].HeaderCell.Value, "At Top of%", CompareMethod.Binary))))
					{
						goto IL_0077;
					}
					goto IL_009a;
					IL_0077:
					num2 = 6;
					GridTopOf.Rows[num5].HeaderCell.Value = "At Top of";
					goto IL_009a;
					IL_00a0:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_003f;
					IL_009a:
					num2 = 7;
					num5 = checked(num5 + 1);
					goto IL_00a0;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyGrid = GridTopOf;
					BuildReport.Grid_Row_Del(ref MyGrid);
					GridTopOf = MyGrid;
					goto IL_002a;
					IL_002a:
					num2 = 4;
					num6 = checked(GridTopOf.RowCount - 1);
					num5 = 0;
					goto IL_00a0;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 215;
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

	private void CmdDel4_Click(object sender, EventArgs e)
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
				case 101:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (GridCols.RowCount > 0)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				DataGridView MyGrid = GridCols;
				BuildReport.Grid_Row_Del(ref MyGrid);
				GridCols = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 101;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdLeft4_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int index = default(int);
		int num6 = default(int);
		string text = default(string);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text2;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 825:
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
								goto IL_0029;
							case 5:
								goto IL_002d;
							case 6:
								goto IL_0036;
							case 7:
								goto IL_003f;
							case 8:
								goto IL_0044;
							case 9:
								goto IL_0049;
							case 10:
								goto IL_005d;
							case 11:
								goto IL_0065;
							case 12:
								goto IL_0078;
							case 13:
								goto IL_0092;
							case 14:
								goto IL_00bf;
							case 15:
								goto IL_00ec;
							case 16:
								goto IL_013a;
							case 17:
								goto IL_0162;
							case 18:
								goto IL_01b0;
							case 19:
								goto IL_01d8;
							case 20:
								goto IL_01ea;
							case 21:
								goto IL_0206;
							case 22:
								goto IL_0237;
							case 23:
								goto IL_0253;
							case 24:
								goto IL_026e;
							case 25:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 3:
							case 26:
							case 27:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0162:
						num2 = 17;
						GridCols.Rows[num5].Cells[index].Tag = RuntimeHelpers.GetObjectValue(GridCols.Rows[num5].Cells[num6].Tag);
						goto IL_01b0;
						IL_01b0:
						num2 = 18;
						GridCols.Rows[num5].Cells[num6].Tag = text;
						goto IL_01d8;
						IL_013a:
						num2 = 16;
						GridCols.Rows[num5].Cells[num6].Value = text;
						goto IL_0162;
						IL_01d8:
						num2 = 19;
						num5++;
						goto IL_01e1;
						IL_000b:
						num2 = 2;
						if (GridCols.RowCount <= 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0029;
						IL_0029:
						num2 = 4;
						num6 = 0;
						goto IL_002d;
						IL_002d:
						num2 = 5;
						text = "";
						goto IL_0036;
						IL_0036:
						num2 = 6;
						text2 = "";
						goto IL_003f;
						IL_003f:
						num2 = 7;
						index = 0;
						goto IL_0044;
						IL_0044:
						num2 = 8;
						num5 = 0;
						goto IL_0049;
						IL_0049:
						num2 = 9;
						num6 = GridCols.CurrentCell.ColumnIndex;
						goto IL_005d;
						IL_005d:
						num2 = 10;
						index = num6 - 1;
						goto IL_0065;
						IL_0065:
						num2 = 11;
						if (num6 < 5)
						{
							goto end_IL_0001_3;
						}
						goto IL_0078;
						IL_0078:
						num2 = 12;
						num7 = GridCols.RowCount - 1;
						num5 = 0;
						goto IL_01e1;
						IL_01e1:
						if (num5 <= num7)
						{
							goto IL_0092;
						}
						goto IL_01ea;
						IL_01ea:
						num2 = 20;
						text = GridCols.Columns[index].HeaderText;
						goto IL_0206;
						IL_0206:
						num2 = 21;
						GridCols.Columns[index].HeaderText = GridCols.Columns[num6].HeaderText;
						goto IL_0237;
						IL_0237:
						num2 = 22;
						GridCols.Columns[num6].HeaderText = text;
						goto IL_0253;
						IL_0253:
						num2 = 23;
						GridCols.Columns[num6].Selected = false;
						goto IL_026e;
						IL_026e:
						num2 = 24;
						GridCols.Columns[index].Selected = true;
						break;
						IL_0092:
						num2 = 13;
						text = Conversions.ToString(GridCols.Rows[num5].Cells[index].Value);
						goto IL_00bf;
						IL_00bf:
						num2 = 14;
						text2 = Conversions.ToString(GridCols.Rows[num5].Cells[index].Tag);
						goto IL_00ec;
						IL_00ec:
						num2 = 15;
						GridCols.Rows[num5].Cells[index].Value = RuntimeHelpers.GetObjectValue(GridCols.Rows[num5].Cells[num6].Value);
						goto IL_013a;
						end_IL_0001_2:
						break;
					}
					num2 = 25;
					GridCols.CurrentCell = GridCols.Rows[0].Cells[index];
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 825;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdRight4_Click(object sender, EventArgs e)
	{
		if (GridCols.RowCount <= 0)
		{
			return;
		}
		int num = 0;
		string text = "";
		string text2 = "";
		int num2 = 0;
		int num3 = 0;
		num = GridCols.CurrentCell.ColumnIndex;
		checked
		{
			num2 = num + 1;
			if ((num >= 4) & (num <= GridCols.ColumnCount - 2))
			{
				int num4 = GridCols.RowCount - 1;
				for (num3 = 0; num3 <= num4; num3++)
				{
					text = Conversions.ToString(GridCols.Rows[num3].Cells[num2].Value);
					text2 = Conversions.ToString(GridCols.Rows[num3].Cells[num2].Tag);
					GridCols.Rows[num3].Cells[num2].Value = RuntimeHelpers.GetObjectValue(GridCols.Rows[num3].Cells[num].Value);
					GridCols.Rows[num3].Cells[num].Value = text;
					GridCols.Rows[num3].Cells[num2].Tag = RuntimeHelpers.GetObjectValue(GridCols.Rows[num3].Cells[num].Tag);
					GridCols.Rows[num3].Cells[num].Tag = text;
				}
				text = GridCols.Columns[num2].HeaderText;
				GridCols.Columns[num2].HeaderText = GridCols.Columns[num].HeaderText;
				GridCols.Columns[num].HeaderText = text;
				GridCols.Columns[num].Selected = false;
				GridCols.Columns[num2].Selected = true;
				GridCols.CurrentCell = GridCols.Rows[0].Cells[num2];
			}
		}
	}

	private void GridTopOf_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		FrmAtBot frmAtBot = default(FrmAtBot);
		int num5 = default(int);
		int index = default(int);
		string text2 = default(string);
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
					case 755:
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
								goto IL_0039;
							case 8:
								goto IL_0042;
							case 9:
								goto IL_0051;
							case 10:
								goto IL_0066;
							case 11:
								goto IL_007b;
							case 12:
								goto IL_0095;
							case 13:
								goto IL_00ac;
							case 14:
								goto IL_00d8;
							case 15:
								goto IL_00f1;
							case 16:
							case 17:
								goto IL_010a;
							case 18:
								goto IL_0119;
							case 19:
								goto IL_013e;
							case 20:
								goto IL_0157;
							case 21:
								goto IL_0181;
							case 22:
								goto IL_019e;
							case 23:
								goto IL_01c2;
							case 25:
							case 26:
								goto IL_01d8;
							case 24:
							case 27:
							case 28:
								goto IL_01e9;
							case 29:
								goto IL_01f4;
							case 30:
								goto IL_0212;
							case 31:
								goto IL_021e;
							case 32:
							case 33:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 34:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00d8:
						num2 = 14;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_00f1;
						}
						goto IL_010a;
						IL_00f1:
						num2 = 15;
						frmAtBot.CmbAt.Items.Add(text);
						goto IL_010a;
						IL_00ac:
						num2 = 13;
						text = Conversions.ToString(GridSort.Rows[num5].Cells[0].Value);
						goto IL_00d8;
						IL_010a:
						num2 = 17;
						num5++;
						goto IL_0113;
						IL_000b:
						num2 = 2;
						index = 0;
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
						text2 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						index = GridTopOf.CurrentCell.RowIndex;
						goto IL_0039;
						IL_0039:
						num2 = 7;
						frmAtBot = new FrmAtBot();
						goto IL_0042;
						IL_0042:
						num2 = 8;
						frmAtBot.Text = "At Top Of";
						goto IL_0051;
						IL_0051:
						num2 = 9;
						frmAtBot.LblHeader.Text = "Choose a Sort Column for this subtitle";
						goto IL_0066;
						IL_0066:
						num2 = 10;
						frmAtBot.CmbAt.Items.Clear();
						goto IL_007b;
						IL_007b:
						num2 = 11;
						frmAtBot.CmbAt.Items.Add("");
						goto IL_0095;
						IL_0095:
						num2 = 12;
						num6 = GridSort.RowCount - 1;
						num5 = 0;
						goto IL_0113;
						IL_0113:
						if (num5 <= num6)
						{
							goto IL_00ac;
						}
						goto IL_0119;
						IL_0119:
						num2 = 18;
						text2 = Conversions.ToString(GridTopOf.Rows[index].HeaderCell.Value);
						goto IL_013e;
						IL_013e:
						num2 = 19;
						if (LikeOperator.LikeString(text2, "*{*}", CompareMethod.Binary))
						{
							goto IL_0157;
						}
						goto IL_01e9;
						IL_0157:
						num2 = 20;
						text2 = General_Procedures.Get_Node_Value(Conversions.ToString(GridTopOf.Rows[index].HeaderCell.Value));
						goto IL_0181;
						IL_0181:
						num2 = 21;
						num7 = frmAtBot.CmbAt.Items.Count - 1;
						num5 = 0;
						goto IL_01e1;
						IL_01e1:
						if (num5 <= num7)
						{
							goto IL_019e;
						}
						goto IL_01e9;
						IL_019e:
						num2 = 22;
						if (Operators.ConditionalCompareObjectEqual(text2, frmAtBot.CmbAt.Items[num5], TextCompare: false))
						{
							goto IL_01c2;
						}
						goto IL_01d8;
						IL_01c2:
						num2 = 23;
						frmAtBot.CmbAt.SelectedIndex = num5;
						goto IL_01e9;
						IL_01d8:
						num2 = 26;
						num5++;
						goto IL_01e1;
						IL_01e9:
						num2 = 28;
						frmAtBot.ShowDialog();
						goto IL_01f4;
						IL_01f4:
						num2 = 29;
						if (Operators.CompareString(frmAtBot.f_SortCol, "CANCEL", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0212;
						IL_0212:
						num2 = 30;
						text = frmAtBot.f_SortCol;
						goto IL_021e;
						IL_021e:
						num2 = 31;
						GridTopOf.Rows[index].HeaderCell.Value = "At Top of" + text;
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 33;
					frmAtBot.Dispose();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 755;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridCols_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
					errsource = "FrmReport - GridCols_CellContentDoubleClick";
					int rowIndex = GridCols.CurrentCell.RowIndex;
					int columnIndex = GridCols.CurrentCell.ColumnIndex;
					string text = "";
					if (rowIndex == -1 || columnIndex < 4)
					{
						goto end_IL_0001;
					}
					if (Operators.ConditionalCompareObjectEqual(GridCols.CurrentCell.Value, "Expr:", TextCompare: false))
					{
						text = Conversions.ToString(GridCols.CurrentCell.Tag);
						MyProject.Forms.FrmMain.LoadComputedColumnForm(6, "MODIFY", text, "C", TxtCSVIn.Text, -1);
						GridCols.CurrentCell.Tag = Globals_Renamed.currDataAny;
						Refresh();
						Globals_Renamed.currtxttmp = "";
						Globals_Renamed.currDataAny = "";
					}
					else if (Operators.ConditionalCompareObjectEqual(GridCols.CurrentCell.Value, "Text:", TextCompare: false))
					{
						text = Conversions.ToString(GridCols.CurrentCell.Tag);
						text = Interaction.InputBox("Enter Text to display at the bottom of a column or the report. Enter DEL to remove the existing text expression. Bound columns by { } to have their value substituted. E.g., Sum for Operation: {Operation}", "Enter a Text Expression", text);
						if (Operators.CompareString(Strings.UCase(text), "DEL", TextCompare: false) == 0)
						{
							GridCols.CurrentCell.Tag = "";
						}
						else if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							GridCols.CurrentCell.Tag = text;
						}
					}
					goto end_IL_0001;
				}
				case 449:
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
				try0001_dispatch = 449;
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

	private void GridCols_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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
						errsource = "FrmReport - GridCols_ColumnHeaderMouseDoubleClick";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						string text = "";
						string text2 = "";
						int num6 = -1;
						if (GridCols.RowCount <= 0)
						{
							goto end_IL_0001;
						}
						num3 = e.ColumnIndex;
						num4 = GridCols.CurrentCell.RowIndex;
						if (num3 < 0)
						{
							goto end_IL_0001;
						}
						if (num3 >= 4)
						{
							FrmAtBot frmAtBot = new FrmAtBot();
							frmAtBot.LblHeader.Text = "Choose a Sort Column or the Report attribute for this summary";
							frmAtBot.CmbAt.Items.Clear();
							frmAtBot.CmbAt.Items.Add("");
							frmAtBot.CmbAt.Items.Add("Report");
							int num7 = GridSort.RowCount - 1;
							for (num5 = 0; num5 <= num7; num5++)
							{
								text = Conversions.ToString(GridSort.Rows[num5].Cells[0].Value);
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									frmAtBot.CmbAt.Items.Add(text);
								}
							}
							text2 = GridCols.Columns[num3].HeaderText;
							if (LikeOperator.LikeString(text2, "*{*}", CompareMethod.Binary))
							{
								text2 = General_Procedures.Get_Node_Value(text2);
								int num8 = frmAtBot.CmbAt.Items.Count - 1;
								for (num5 = 0; num5 <= num8; num5++)
								{
									if (Operators.ConditionalCompareObjectEqual(text2, frmAtBot.CmbAt.Items[num5], TextCompare: false))
									{
										frmAtBot.CmbAt.SelectedIndex = num5;
										break;
									}
								}
							}
							frmAtBot.ShowDialog();
							if (Operators.CompareString(frmAtBot.f_SortCol, "CANCEL", TextCompare: false) != 0)
							{
								text = frmAtBot.f_SortCol;
								GridCols.Columns[num3].HeaderText = "At Bottom of" + text;
							}
							frmAtBot.Dispose();
						}
						num6 = -1;
						int num9 = GridCols.RowCount - 1;
						for (num5 = 0; num5 <= num9; num5++)
						{
							if (Operators.ConditionalCompareObjectEqual(GridCols.Rows[num5].Cells[num3].Value, "", TextCompare: false))
							{
								num6 = num5;
								break;
							}
						}
						if (num6 == -1)
						{
							GridCols.CurrentCell = GridCols[3, num4];
						}
						else
						{
							GridCols.CurrentCell = GridCols[num3, num6];
						}
						goto end_IL_0001;
					}
					case 769:
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
				try0001_dispatch = 769;
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

	private void GridSort_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		int rowIndex = default(int);
		string left2 = default(string);
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
				case 491:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001e;
						case 5:
							goto IL_0026;
						case 6:
							goto IL_002f;
						case 7:
							goto IL_0039;
						case 8:
							goto IL_0043;
						case 10:
							goto IL_0055;
						case 11:
							goto IL_0069;
						case 13:
							goto IL_008e;
						case 15:
							goto IL_009b;
						case 16:
							goto IL_00c7;
						case 17:
							goto IL_00e0;
						case 18:
							goto IL_010b;
						case 19:
							goto IL_0137;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 9:
						case 12:
						case 14:
						case 21:
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c7:
					num2 = 16;
					if (Operators.CompareString(left, "", TextCompare: false) == 0)
					{
						goto IL_00e0;
					}
					goto IL_010b;
					IL_00e0:
					num2 = 17;
					GridSort.Rows[rowIndex].Cells[1].Value = "Asc";
					goto IL_010b;
					IL_009b:
					num2 = 15;
					left = Conversions.ToString(GridSort.Rows[rowIndex].Cells[1].Value);
					goto IL_00c7;
					IL_010b:
					num2 = 18;
					left = Conversions.ToString(GridSort.Rows[rowIndex].Cells[2].Value);
					goto IL_0137;
					IL_000b:
					num2 = 2;
					if (IsStartUp)
					{
						goto end_IL_0001_3;
					}
					goto IL_001e;
					IL_001e:
					num2 = 4;
					left2 = "";
					goto IL_0026;
					IL_0026:
					num2 = 5;
					left = "";
					goto IL_002f;
					IL_002f:
					num2 = 6;
					columnIndex = e.ColumnIndex;
					goto IL_0039;
					IL_0039:
					num2 = 7;
					rowIndex = e.RowIndex;
					goto IL_0043;
					IL_0043:
					num2 = 8;
					if (rowIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0055;
					IL_0055:
					num2 = 10;
					left2 = Strings.Trim(Conversions.ToString(e.FormattedValue));
					goto IL_0069;
					IL_0069:
					num2 = 11;
					if (columnIndex != 0 || Operators.CompareString(left2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_008e;
					IL_0137:
					num2 = 19;
					if (Operators.CompareString(left, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_008e:
					num2 = 13;
					if (columnIndex != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_009b;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				GridSort.Rows[rowIndex].Cells[2].Value = "Y";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 491;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TabRpt_Click(object sender, EventArgs e)
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
					Resize_Form();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Refresh();
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

	private void CmdUp5_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridColRule;
				GridModule.Grid_Up(ref MyGrid);
				GridColRule = MyGrid;
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

	private void CmdDown5_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridColRule;
				GridModule.Grid_Down(ref MyGrid);
				GridColRule = MyGrid;
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

	private void CmdDel5_Click(object sender, EventArgs e)
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
				case 69:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				DataGridView MyGrid = GridColRule;
				Grid_Row_DelR(ref MyGrid, 100);
				GridColRule = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 69;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdUp6_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridRowRule;
				GridModule.Grid_Up(ref MyGrid);
				GridRowRule = MyGrid;
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

	private void CmdDown6_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridRowRule;
				GridModule.Grid_Down(ref MyGrid);
				GridRowRule = MyGrid;
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

	private void CmdDel6_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridRowRule;
				BuildReport.Grid_Row_Del(ref MyGrid);
				GridRowRule = MyGrid;
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

	private void TabMain_Enter(object sender, EventArgs e)
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
				TxtCSVIn.Focus();
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

	private void mnuNew_Click(object sender, EventArgs e)
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
							goto IL_000f;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_0038;
						case 7:
							goto IL_0041;
						case 8:
							goto IL_004a;
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
					IL_0038:
					num2 = 6;
					Clear_All_Grids();
					goto IL_0041;
					IL_0041:
					num2 = 7;
					Initialize_Formats();
					goto IL_004a;
					IL_002c:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0038;
					IL_004a:
					num2 = 8;
					TxtCSVIn.Text = "";
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					Refresh_Combo();
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to clear all selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear Form Selections?");
					goto IL_002c;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Add_Query_To_Title();
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

	private void GridTopOf_RowHeadersWidthChanged(object sender, EventArgs e)
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
				GridTopOf.Columns[0].Width = checked(GridTopOf.Width - GridTopOf.RowHeadersWidth - 19);
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

	private void cmdbrowsecss_Click(object sender, EventArgs e)
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
				case 187:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003b;
						case 5:
							goto IL_0052;
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
					BuildForm.FileOpenSave("O", text, "css", "Choose a Style Sheet for this report", "");
					goto IL_003b;
					IL_003b:
					num2 = 4;
					text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0052;
					IL_000b:
					num2 = 2;
					text = Strings.Trim(Txtcss.Text);
					goto IL_001e;
					IL_0052:
					num2 = 5;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				Txtcss.Text = BuildForm.Strip_Add_MyPCDir("S", text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 187;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
							goto IL_0021;
						case 6:
							goto IL_002d;
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
					Refresh_Combo();
					goto IL_0021;
					IL_0021:
					num2 = 5;
					num5 = Save_Report(ref MyRptSpec);
					goto IL_002d;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_002d:
					num2 = 6;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					MyRptSpec = "";
					goto IL_0013;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
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

	private void FrmReport_Resize(object sender, EventArgs e)
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

	private void mnuRun_Click(object sender, EventArgs e)
	{
		string MyRptSpec = "";
		short num = 0;
		Refresh_Combo();
		if (Save_Report(ref MyRptSpec) == 0)
		{
			MyRptSpec = "<OPTIONS>\r\n/REPORT=HTML-RUN\r\n/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\n" + MyRptSpec;
			BuildSQL.RunQuery(MyRptSpec, 9, 1, -99, MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile);
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
					errsource = "FrmReport - mnuOpenF_Click";
					Refresh_Combo();
					string iniFile = "";
					int num3 = 0;
					string text = "";
					short num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					string text2 = "";
					string text3 = "";
					string text4 = "";
					string text5 = "";
					string text6 = "";
					string text7 = "";
					string text8 = "";
					int num10 = 0;
					bool flag = false;
					BuildForm.FileOpenSave("O", iniFile, "vgs", "SQLPathFinder Report Specification File", Globals_Renamed.gQueryDir);
					iniFile = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					if (Operators.CompareString(iniFile, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					num3 = (int)Interaction.MsgBox("Loading the new file will clear previous report selections. Are you sure you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Do you wish to continue?");
					if (num3 != 7)
					{
						text = General_Procedures.OpenReadFileContents(iniFile);
						if (Operators.CompareString(text, "", TextCompare: false) != 0 && checked((short)Open_Report(text)) == 0)
						{
							Add_Query_To_Title();
						}
					}
					goto end_IL_0001;
				}
				case 322:
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
				try0001_dispatch = 322;
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
		string text = default(string);
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
				case 322:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_0034;
						case 7:
							goto IL_0055;
						case 8:
							goto IL_0076;
						case 9:
							goto IL_008e;
						case 12:
							goto IL_00ad;
						case 11:
						case 13:
						case 14:
							goto IL_00b9;
						case 15:
							goto IL_00ce;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 10:
						case 17:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ad:
					num2 = 12;
					text = fTitle;
					goto IL_00b9;
					IL_00b9:
					num2 = 14;
					if (General_Procedures.Save_SQL_Query(MyRptSpec, text) != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_00ce;
					IL_008e:
					num2 = 9;
					if (Operators.CompareString(text, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00b9;
					IL_00ce:
					num2 = 15;
					fTitle = text;
					break;
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
					if (Save_Report(ref MyRptSpec) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0034;
					IL_0034:
					num2 = 6;
					if (Operators.CompareString(Strings.LCase(fTitle), "untitled.vgs", TextCompare: false) == 0)
					{
						goto IL_0055;
					}
					goto IL_00ad;
					IL_0055:
					num2 = 7;
					BuildForm.FileOpenSave("S", "sqlpathfinder.vgs", "vgs", "SQLPathFinder Report Spec File", Globals_Renamed.gQueryDir);
					goto IL_0076;
					IL_0076:
					num2 = 8;
					text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_008e;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				Add_Query_To_Title();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 322;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridColRule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
				DataGridView MyGrid = GridColRule;
				FontDialog FontDialog = FontDialog1;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 5);
				ColorDialog1 = ColorDialog;
				FontDialog1 = FontDialog;
				GridColRule = MyGrid;
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

	private void GridRowRule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
				DataGridView MyGrid = GridRowRule;
				FontDialog FontDialog = FontDialog1;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 4);
				ColorDialog1 = ColorDialog;
				FontDialog1 = FontDialog;
				GridRowRule = MyGrid;
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

	private void GridCols_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 291:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0039;
						case 7:
							goto IL_0054;
						case 8:
							goto IL_0068;
						case 10:
							goto IL_007e;
						case 11:
							goto IL_0098;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 9:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0068:
					num2 = 8;
					if (rowIndex == -1 || columnIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_007e;
					IL_0098:
					num2 = 11;
					GridCols.CurrentCell = GridCols[columnIndex, checked(rowIndex + 1)];
					break;
					IL_0054:
					num2 = 7;
					rowIndex = GridCols.CurrentCell.RowIndex;
					goto IL_0068;
					IL_007e:
					num2 = 10;
					if (rowIndex >= checked(GridCols.RowCount - 1))
					{
						goto end_IL_0001_3;
					}
					goto IL_0098;
					IL_000b:
					num2 = 2;
					if (!GridCols.IsCurrentCellDirty)
					{
						goto end_IL_0001_3;
					}
					goto IL_0026;
					IL_0026:
					num2 = 4;
					columnIndex = GridCols.CurrentCell.ColumnIndex;
					goto IL_0039;
					IL_0039:
					num2 = 5;
					if (columnIndex == 1 || columnIndex == 3 || columnIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0054;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				GridCols.CurrentCell = GridCols[columnIndex, rowIndex];
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 291;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuEnableColSort_Click(object sender, EventArgs e)
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
					if (!Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "checked", new object[0], null, null, null)))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Interaction.MsgBox("Remember to set the Data Type of numeric columns to \"d=n\" in the \"Format\" Column of the report grid on the Columns tab to ensure correct column sorting.", MsgBoxStyle.Information, "Remember to Set Numeric Data Types");
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

	private void GridColRule_Click(object sender, EventArgs e)
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
				GridClick(ref MyGrid, 0);
				sender = MyGrid;
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

	private void CmbCol_KeyPress(object sender, KeyPressEventArgs e)
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
				case 149:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0038;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_001a:
					num2 = 3;
					if (num5 != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 4;
					if (CMbIdx != 0)
					{
						break;
					}
					goto IL_0038;
					IL_000b:
					num2 = 2;
					num5 = checked((short)Strings.Asc(e.KeyChar));
					goto IL_001a;
					IL_0038:
					num2 = 5;
					GridColRule.Focus();
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				GridRowRule.Focus();
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
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmbCol_Leave(object sender, EventArgs e)
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
					errsource = "FrmReport - CmbCol_Leave";
					ProjectData.ClearProjectError();
					num2 = 2;
					if (!((f_Row == -1) | (f_Col == -1)))
					{
						if (CMbIdx == 0)
						{
							GridColRule.Rows[f_Row].Cells[f_Col].Value = CmbCol.Text;
							GridColRule.Refresh();
						}
						else
						{
							GridRowRule.Rows[f_Row].Cells[f_Col].Value = CmbCol.Text;
							GridRowRule.Refresh();
						}
						CmbCol.Visible = false;
						f_Row = -1;
						f_Col = -1;
					}
					goto end_IL_0001;
				case 268:
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
				try0001_dispatch = 268;
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

	private void MenuStrip1_Click(object sender, EventArgs e)
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
				Refresh_Combo();
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

	private void GridColRule_Scroll(object sender, ScrollEventArgs e)
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
				Refresh_Combo();
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

	private void GridRowRule_Click(object sender, EventArgs e)
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
				GridClick(ref MyGrid, 1);
				sender = MyGrid;
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

	private void GridRowRule_Scroll(object sender, ScrollEventArgs e)
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
				Refresh_Combo();
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

	private void cmdeditvar_Click(object sender, EventArgs e)
	{
		if (Operators.CompareString(TxtCSVIn.Text, "", TextCompare: false) != 0)
		{
			Globals_Renamed.currvaluetmp = TxtCSVIn.Text;
			Globals_Renamed.currinputstrtmp = "G";
			frmfilename frmfilename2 = new frmfilename();
			frmfilename2.ShowDialog();
			if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0))
			{
				TxtCSVIn.Text = Globals_Renamed.currvaluetmp;
			}
			Globals_Renamed.currvaluetmp = "";
		}
		else
		{
			Interaction.MsgBox("You must first design a report before you can Assign a Global replacement variable.", MsgBoxStyle.Information, "Cannot Yet Assign a Global Variable");
		}
	}

	private void mnuAtTop_Click(object sender, EventArgs e)
	{
		int num = 0;
		bool flag = true;
		int num2 = 0;
		if (mnuAtTop.Checked)
		{
			mnuAtTop.Checked = false;
			Set_Rpt_Opt(MyVal: true);
			GridTopOf.Enabled = true;
			return;
		}
		checked
		{
			int num3 = GridTopOf.RowCount - 1;
			for (num = 0; num <= num3; num++)
			{
				if (Operators.ConditionalCompareObjectNotEqual(GridTopOf.Rows[num].Cells[0].Value, "", TextCompare: false))
				{
					flag = false;
					break;
				}
			}
		}
		if (!flag)
		{
			num2 = (int)Interaction.MsgBox("Setting the At Top of Summary Option will clear the At Top of Column Grid. Do you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear the At Top of Column Grid?");
			if (num2 == 6)
			{
				flag = true;
			}
		}
		if (flag)
		{
			mnuAtTop.Checked = true;
			Disable_AT_Top();
		}
	}

	public void Set_Rpt_Opt(bool MyVal)
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
							goto IL_001a;
						case 4:
							goto IL_0029;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0047;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0029:
					num2 = 4;
					mnuEnableColSort.Checked = false;
					goto IL_0038;
					IL_0038:
					num2 = 5;
					mnuEnableColSort.Enabled = MyVal;
					goto IL_0047;
					IL_001a:
					num2 = 3;
					mnuEnableColDrillDown.Enabled = MyVal;
					goto IL_0029;
					IL_0047:
					num2 = 6;
					mnuEnableDynFilter.Checked = false;
					break;
					IL_000b:
					num2 = 2;
					mnuEnableColDrillDown.Checked = false;
					goto IL_001a;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				mnuEnableDynFilter.Enabled = MyVal;
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

	public void Disable_AT_Top()
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
							goto IL_000f;
						case 4:
							goto IL_002a;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_0062;
						case 7:
							goto IL_006d;
						case 8:
							goto IL_0088;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0088:
					num2 = 8;
					GridTopOf.Enabled = false;
					break;
					IL_003f:
					num2 = 5;
					GridTopOf.Rows[num5].HeaderCell.Value = "At Top of";
					goto IL_0062;
					IL_006d:
					num2 = 7;
					GridTopOf.CurrentCell = GridTopOf[0, 0];
					goto IL_0088;
					IL_0062:
					num2 = 6;
					num5 = checked(num5 + 1);
					goto IL_0068;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyGrid = GridTopOf;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridTopOf = MyGrid;
					goto IL_002a;
					IL_002a:
					num2 = 4;
					num6 = checked(GridTopOf.RowCount - 1);
					num5 = 0;
					goto IL_0068;
					IL_0068:
					if (num5 <= num6)
					{
						goto IL_003f;
					}
					goto IL_006d;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Set_Rpt_Opt(MyVal: false);
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
					GridCols.RowCount = checked(GridCols.RowCount + 1);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				DataGridView MyGrid = GridCols;
				GridModule.Number_Grid(ref MyGrid);
				GridCols = MyGrid;
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

	private void GridSort_KeyDown(object sender, KeyEventArgs e)
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
					CmdDel1_Click(CmdDel1, new EventArgs());
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

	private void GridRptHdr_KeyDown(object sender, KeyEventArgs e)
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
				case 157:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0021;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_0041;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0021:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to delete the highlighted rows?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Delete Rows?");
					goto IL_0035;
					IL_0035:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0041;
					IL_000f:
					num2 = 3;
					if (e.KeyValue != 46)
					{
						goto end_IL_0001_3;
					}
					goto IL_0021;
					IL_0041:
					num2 = 6;
					CmdDel2_Click(CmdDel2, new EventArgs());
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 157;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridTopOf_KeyDown(object sender, KeyEventArgs e)
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
				case 157:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0021;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_0041;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0021:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to delete the highlighted rows?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Delete Rows?");
					goto IL_0035;
					IL_0035:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0041;
					IL_000f:
					num2 = 3;
					if (e.KeyValue != 46)
					{
						goto end_IL_0001_3;
					}
					goto IL_0021;
					IL_0041:
					num2 = 6;
					CmdDel3_Click(CmdDel3, new EventArgs());
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 157;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridCols_KeyDown(object sender, KeyEventArgs e)
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
					CmdDel4_Click(CmdDel4, new EventArgs());
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

	private void GridColRule_KeyDown(object sender, KeyEventArgs e)
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
				case 460:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_00bf;
						case 12:
							goto IL_00d8;
						case 14:
							goto IL_00e9;
						case 15:
							goto IL_0125;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 13:
						case 17:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d8:
					num2 = 12;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0125:
					num2 = 15;
					MyGrid = (DataGridView)sender;
					FontDialog = FontDialog1;
					ColorDialog = ColorDialog1;
					BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 5);
					ColorDialog1 = ColorDialog;
					FontDialog1 = FontDialog;
					sender = MyGrid;
					break;
					IL_00bf:
					num2 = 11;
					MyGrid = (DataGridView)sender;
					GridClick(ref MyGrid, 0);
					sender = MyGrid;
					goto IL_00d8;
					IL_00e9:
					num2 = 14;
					if ((GridColRule.CurrentCell.ColumnIndex != 4 && GridColRule.CurrentCell.ColumnIndex != 5) || e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0125;
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
					CmdDel5_Click(CmdDel5, new EventArgs());
					goto IL_0067;
					IL_0067:
					num2 = 8;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0077:
					num2 = 10;
					if ((GridColRule.CurrentCell.ColumnIndex == 0 || GridColRule.CurrentCell.ColumnIndex == 1) && (e.KeyValue == 13 || e.KeyValue == 115))
					{
						goto IL_00bf;
					}
					goto IL_00e9;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 460;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridRowRule_KeyDown(object sender, KeyEventArgs e)
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
				case 513:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_00ac;
						case 12:
							goto IL_00c5;
						case 14:
							goto IL_00d6;
						case 15:
							goto IL_015a;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 13:
						case 17:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_015a:
					num2 = 15;
					MyGrid = (DataGridView)sender;
					FontDialog = FontDialog1;
					ColorDialog = ColorDialog1;
					BuildChart.Grid_Format_DC(ref MyGrid, ref FontDialog, ref ColorDialog, 4);
					ColorDialog1 = ColorDialog;
					FontDialog1 = FontDialog;
					sender = MyGrid;
					break;
					IL_00ac:
					num2 = 11;
					MyGrid = (DataGridView)sender;
					GridClick(ref MyGrid, 1);
					sender = MyGrid;
					goto IL_00c5;
					IL_00d6:
					num2 = 14;
					if ((!Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null), 3, TextCompare: false) && !Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null), 4, TextCompare: false)) || e.KeyValue != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_015a;
					IL_00c5:
					num2 = 12;
					e.Handled = true;
					goto end_IL_0001_3;
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
					CmdDel6_Click(CmdDel6, new EventArgs());
					goto IL_0067;
					IL_0067:
					num2 = 8;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0077:
					num2 = 10;
					if (GridRowRule.CurrentCell.ColumnIndex == 0 && (e.KeyValue == 13 || e.KeyValue == 115))
					{
						goto IL_00ac;
					}
					goto IL_00d6;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 513;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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

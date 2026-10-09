using System;
using System.Collections;
using System.Collections.Generic;
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
public class FrmReporti : Form
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
	[AccessedThroughProperty("TabCols")]
	private TabPage _TabCols;

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
	[AccessedThroughProperty("CmbCol")]
	private ComboBox _CmbCol;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdEditVar")]
	private Button _cmdEditVar;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAddR")]
	private Button _cmdAddR;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDel0")]
	private Button _cmdDel0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown0")]
	private Button _cmdDown0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp0")]
	private Button _cmdUp0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridCSV")]
	private DataGridView _GridCSV;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridColRule")]
	private DataGridView _GridColRule;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbCSV")]
	private ComboBox _cmbCSV;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_KeysColsOrderUpBtn")]
	private Button _CC_KeysColsOrderUpBtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_KeysColsOrderDownBtn")]
	private Button _CC_KeysColsOrderDownBtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_KeyAddBtn")]
	private Button _CC_KeyAddBtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_KeyRemoveBtn")]
	private Button _CC_KeyRemoveBtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_NewGuidBtn")]
	private Button _CC_NewGuidBtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CC_HeaderTxtBx")]
	private TextBox _CC_HeaderTxtBx;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCC")]
	private ToolStripMenuItem _mnuCC;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdMacroGuid")]
	private Button _cmdMacroGuid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAddCom")]
	private Button _cmdAddCom;

	public string f_ReportSpec;

	public string fTitle;

	public string f_ReportSpecName;

	public string fActual;

	public string fDisplay;

	private bool IsStartUp;

	private int f_MaxObj;

	private int f_MinCols;

	private Globals_Renamed.Report_Format_Type[] CSSObj;

	private int fMenuItems;

	private const int ColCR_fc = 5;

	private const int ColRR_fc = 4;

	private const string MyHTMLOutFile = "SQLPathFinder.htm";

	private short f_Row;

	private short f_Col;

	private const short cGridSortR = 15;

	private const short cGridRptHdrR = 10;

	private const short cGridColRuleR = 100;

	private const short cGridCSVR = 3;

	private const short cGridCSVChart = 9;

	private const int cColDT = 4;

	private const int cColWidth = 5;

	private const int cColFmt = 6;

	private const int cColSort = 7;

	private const int cColFilter = 8;

	private const int cColHide = 9;

	private const int cColOFmt = 10;

	private const int cColWhere = 11;

	private const int cColEditType = 13;

	private const short fStartAtBotOf = 15;

	private const int Colf_fc = 2;

	private const string fspfcomment = "1:spf$comment$-";

	private Guid CC_RepGUID;

	private bool CC_Enabled;

	private string cc_enabled_ver;

	private string CC_ServiceURL;

	private string CC_CustomData;

	private string CC_Keys;

	private string CC_HeaderName;

	private string CC_HeaderWidth;

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
			TabControlEventHandler value3 = TabRpt_Selected;
			TabControl tabControl = _TabRpt;
			if (tabControl != null)
			{
				tabControl.Click -= value2;
				tabControl.Selected -= value3;
			}
			_TabRpt = value;
			tabControl = _TabRpt;
			if (tabControl != null)
			{
				tabControl.Click += value2;
				tabControl.Selected += value3;
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

	internal virtual TabPage TabCols
	{
		[CompilerGenerated]
		get
		{
			return _TabCols;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TabCols_Enter;
			TabPage tabPage = _TabCols;
			if (tabPage != null)
			{
				tabPage.Enter -= value2;
			}
			_TabCols = value;
			tabPage = _TabCols;
			if (tabPage != null)
			{
				tabPage.Enter += value2;
			}
		}
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
			EventHandler value2 = GridCols_CurrentCellDirtyStateChanged;
			MouseEventHandler value3 = GridCols_MouseDoubleClick;
			KeyEventHandler value4 = GridCols_KeyDown;
			DataGridView dataGridView = _GridCols;
			if (dataGridView != null)
			{
				dataGridView.CurrentCellDirtyStateChanged -= value2;
				dataGridView.MouseDoubleClick -= value3;
				dataGridView.KeyDown -= value4;
			}
			_GridCols = value;
			dataGridView = _GridCols;
			if (dataGridView != null)
			{
				dataGridView.CurrentCellDirtyStateChanged += value2;
				dataGridView.MouseDoubleClick += value3;
				dataGridView.KeyDown += value4;
			}
		}
	}

	[field: AccessedThroughProperty("LblCols")]
	internal virtual Label LblCols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	internal virtual Button cmdDel0
	{
		[CompilerGenerated]
		get
		{
			return _cmdDel0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDel0_Click;
			Button button = _cmdDel0;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDel0 = value;
			button = _cmdDel0;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdDown0
	{
		[CompilerGenerated]
		get
		{
			return _cmdDown0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDown0_Click;
			Button button = _cmdDown0;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDown0 = value;
			button = _cmdDown0;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdUp0
	{
		[CompilerGenerated]
		get
		{
			return _cmdUp0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp0_Click;
			Button button = _cmdUp0;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdUp0 = value;
			button = _cmdUp0;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual DataGridView GridCSV
	{
		[CompilerGenerated]
		get
		{
			return _GridCSV;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GridCSV_Click;
			ScrollEventHandler value3 = GridCSV_Scroll;
			KeyEventHandler value4 = GridCSV_KeyDown;
			DataGridView dataGridView = _GridCSV;
			if (dataGridView != null)
			{
				dataGridView.Click -= value2;
				dataGridView.Scroll -= value3;
				dataGridView.KeyDown -= value4;
			}
			_GridCSV = value;
			dataGridView = _GridCSV;
			if (dataGridView != null)
			{
				dataGridView.Click += value2;
				dataGridView.Scroll += value3;
				dataGridView.KeyDown += value4;
			}
		}
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
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

	[field: AccessedThroughProperty("TxtTopof")]
	internal virtual TextBox TxtTopof
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblTopof")]
	internal virtual Label lblTopof
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbTopOf")]
	internal virtual ComboBox cmbTopOf
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ComboBox cmbCSV
	{
		[CompilerGenerated]
		get
		{
			return _cmbCSV;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = cmbCSV_KeyPress;
			EventHandler value3 = cmbCSV_Leave;
			ComboBox comboBox = _cmbCSV;
			if (comboBox != null)
			{
				comboBox.KeyPress -= value2;
				comboBox.Leave -= value3;
			}
			_cmbCSV = value;
			comboBox = _cmbCSV;
			if (comboBox != null)
			{
				comboBox.KeyPress += value2;
				comboBox.Leave += value3;
			}
		}
	}

	[field: AccessedThroughProperty("mnuFilterCharts")]
	internal virtual ToolStripMenuItem mnuFilterCharts
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuPage")]
	internal virtual ToolStripMenuItem mnuPage
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Tab_CC")]
	internal virtual TabPage Tab_CC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CC_NoteLbl")]
	internal virtual Label CC_NoteLbl
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

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
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

	internal virtual Button CC_KeysColsOrderUpBtn
	{
		[CompilerGenerated]
		get
		{
			return _CC_KeysColsOrderUpBtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CC_KeysColsOrderUpBtn_Click;
			Button button = _CC_KeysColsOrderUpBtn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CC_KeysColsOrderUpBtn = value;
			button = _CC_KeysColsOrderUpBtn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CC_KeysColsOrderDownBtn
	{
		[CompilerGenerated]
		get
		{
			return _CC_KeysColsOrderDownBtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CC_KeysColsOrderDownBtn_Click;
			Button button = _CC_KeysColsOrderDownBtn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CC_KeysColsOrderDownBtn = value;
			button = _CC_KeysColsOrderDownBtn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CC_KeyAddBtn
	{
		[CompilerGenerated]
		get
		{
			return _CC_KeyAddBtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CC_KeyAddBtn_Click;
			Button button = _CC_KeyAddBtn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CC_KeyAddBtn = value;
			button = _CC_KeyAddBtn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CC_KeyRemoveBtn
	{
		[CompilerGenerated]
		get
		{
			return _CC_KeyRemoveBtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CC_KeyRemoveBtn_Click;
			Button button = _CC_KeyRemoveBtn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CC_KeyRemoveBtn = value;
			button = _CC_KeyRemoveBtn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CC_SrcColsListView")]
	internal virtual ListView CC_SrcColsListView
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CC_KeysColsListView")]
	internal virtual ListView CC_KeysColsListView
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnHeader1")]
	internal virtual ColumnHeader ColumnHeader1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnHeader2")]
	internal virtual ColumnHeader ColumnHeader2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GrpCC")]
	internal virtual GroupBox GrpCC
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CC_NewGuidBtn
	{
		[CompilerGenerated]
		get
		{
			return _CC_NewGuidBtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CC_NewGuidBtn_Click;
			Button button = _CC_NewGuidBtn;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CC_NewGuidBtn = value;
			button = _CC_NewGuidBtn;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CC_GuidLbl")]
	internal virtual Label CC_GuidLbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CC_GuidTxtBx")]
	internal virtual TextBox CC_GuidTxtBx
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CC_WidthMTxtBx")]
	internal virtual MaskedTextBox CC_WidthMTxtBx
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CC_WidthLbl")]
	internal virtual Label CC_WidthLbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TextBox CC_HeaderTxtBx
	{
		[CompilerGenerated]
		get
		{
			return _CC_HeaderTxtBx;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = CC_HeaderTxtBx_Validating;
			TextBox textBox = _CC_HeaderTxtBx;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			_CC_HeaderTxtBx = value;
			textBox = _CC_HeaderTxtBx;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CC_HeaderLbl")]
	internal virtual Label CC_HeaderLbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CommentsCap_CustomData_lbl")]
	internal virtual Label CommentsCap_CustomData_lbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CommentsCap_ServiceURL_lbl")]
	internal virtual Label CommentsCap_ServiceURL_lbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuCC
	{
		[CompilerGenerated]
		get
		{
			return _mnuCC;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCC_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCC;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCC = value;
			toolStripMenuItem = _mnuCC;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CmbCCURL")]
	internal virtual ComboBox CmbCCURL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdMacroGuid
	{
		[CompilerGenerated]
		get
		{
			return _cmdMacroGuid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMacroGuid_Click;
			Button button = _cmdMacroGuid;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdMacroGuid = value;
			button = _cmdMacroGuid;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbCCData")]
	internal virtual ComboBox cmbCCData
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colcsv")]
	internal virtual DataGridViewTextBoxColumn colcsv
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colID")]
	internal virtual DataGridViewTextBoxColumn colID
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coljoin")]
	internal virtual DataGridViewTextBoxColumn coljoin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coljoin2")]
	internal virtual DataGridViewTextBoxColumn coljoin2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coltwidth")]
	internal virtual DataGridViewTextBoxColumn coltwidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colTHeight")]
	internal virtual DataGridViewTextBoxColumn colTHeight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colstsht")]
	internal virtual DataGridViewTextBoxColumn colstsht
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colnestht")]
	internal virtual DataGridViewTextBoxColumn colnestht
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colht")]
	internal virtual DataGridViewTextBoxColumn colht
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colchart")]
	internal virtual DataGridViewTextBoxColumn colchart
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colpageopt")]
	internal virtual DataGridViewTextBoxColumn colpageopt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colpageinit")]
	internal virtual DataGridViewTextBoxColumn colpageinit
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colautorowht")]
	internal virtual DataGridViewTextBoxColumn colautorowht
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colrowht")]
	internal virtual DataGridViewTextBoxColumn colrowht
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colgrp")]
	internal virtual DataGridViewTextBoxColumn colgrp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colshowfilter")]
	internal virtual DataGridViewTextBoxColumn colshowfilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GridOptionsToolStripMenuItem")]
	internal virtual ToolStripMenuItem GridOptionsToolStripMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuGridWidth")]
	internal virtual ToolStripMenuItem mnuGridWidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuRowHt")]
	internal virtual ToolStripMenuItem mnuRowHt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuColWidths")]
	internal virtual ToolStripMenuItem mnuColWidths
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuSumStatus")]
	internal virtual ToolStripMenuItem mnuSumStatus
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuAutoSize")]
	internal virtual ToolStripMenuItem mnuAutoSize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnupin")]
	internal virtual ToolStripMenuItem mnupin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuclearfilter")]
	internal virtual ToolStripMenuItem mnuclearfilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuHidecols")]
	internal virtual ToolStripMenuItem mnuHidecols
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuexport")]
	internal virtual ToolStripMenuItem mnuexport
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnucolresize")]
	internal virtual ToolStripMenuItem mnucolresize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnucolreorder")]
	internal virtual ToolStripMenuItem mnucolreorder
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuthemelabel")]
	internal virtual ToolStripMenuItem mnuthemelabel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnutheme")]
	internal virtual ToolStripComboBox mnutheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnualtrows")]
	internal virtual ToolStripMenuItem mnualtrows
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnumousehover")]
	internal virtual ToolStripMenuItem mnumousehover
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdAddCom
	{
		[CompilerGenerated]
		get
		{
			return _cmdAddCom;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAddCom_Click;
			Button button = _cmdAddCom;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdAddCom = value;
			button = _cmdAddCom;
			if (button != null)
			{
				button.Click += value2;
			}
		}
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

	[field: AccessedThroughProperty("colHA")]
	internal virtual DataGridViewComboBoxColumn colHA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coldt")]
	internal virtual DataGridViewComboBoxColumn coldt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colwidth")]
	internal virtual DataGridViewTextBoxColumn colwidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colformat")]
	internal virtual DataGridViewTextBoxColumn colformat
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colsort1")]
	internal virtual DataGridViewCheckBoxColumn colsort1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colfilter")]
	internal virtual DataGridViewComboBoxColumn colfilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colhide")]
	internal virtual DataGridViewCheckBoxColumn colhide
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

	[field: AccessedThroughProperty("colwhereopr")]
	internal virtual DataGridViewComboBoxColumn colwhereopr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colwhereval")]
	internal virtual DataGridViewTextBoxColumn colwhereval
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coledittype")]
	internal virtual DataGridViewComboBoxColumn coledittype
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("coleditval")]
	internal virtual DataGridViewTextBoxColumn coleditval
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

	[field: AccessedThroughProperty("colAtBot2")]
	internal virtual DataGridViewComboBoxColumn colAtBot2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colAtBot3")]
	internal virtual DataGridViewComboBoxColumn colAtBot3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colAtBot4")]
	internal virtual DataGridViewComboBoxColumn colAtBot4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colAtBot5")]
	internal virtual DataGridViewComboBoxColumn colAtBot5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colAtBot6")]
	internal virtual DataGridViewComboBoxColumn colAtBot6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colAtBot7")]
	internal virtual DataGridViewComboBoxColumn colAtBot7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmReporti()
	{
		base.FormClosing += FrmReport_FormClosing;
		base.Load += FrmReport_Load;
		base.Resize += FrmReport_Resize;
		f_ReportSpec = "";
		fTitle = "untitled.vgs";
		f_ReportSpecName = "untitled";
		fActual = "";
		fDisplay = "";
		IsStartUp = true;
		f_MaxObj = 0;
		f_MinCols = 0;
		CSSObj = new Globals_Renamed.Report_Format_Type[501];
		fMenuItems = 0;
		f_Row = -1;
		f_Col = -1;
		CC_Enabled = false;
		cc_enabled_ver = "";
		CC_ServiceURL = "";
		CC_CustomData = "";
		CC_Keys = "";
		CC_HeaderName = "";
		CC_HeaderWidth = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmReporti));
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
		this.TabRpt = new System.Windows.Forms.TabControl();
		this.TabMain = new System.Windows.Forms.TabPage();
		this.cmbCSV = new System.Windows.Forms.ComboBox();
		this.cmdDel0 = new System.Windows.Forms.Button();
		this.cmdDown0 = new System.Windows.Forms.Button();
		this.cmdUp0 = new System.Windows.Forms.Button();
		this.GridCSV = new System.Windows.Forms.DataGridView();
		this.colcsv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coljoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coljoin2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coltwidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colstsht = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colnestht = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colht = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colchart = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colpageopt = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colpageinit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colautorowht = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colrowht = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colgrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colshowfilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmdEditVar = new System.Windows.Forms.Button();
		this.CmdDel2 = new System.Windows.Forms.Button();
		this.CmdDown2 = new System.Windows.Forms.Button();
		this.CmdUp2 = new System.Windows.Forms.Button();
		this.CmdDel1 = new System.Windows.Forms.Button();
		this.CmdDown1 = new System.Windows.Forms.Button();
		this.CmdUp1 = new System.Windows.Forms.Button();
		this.LblHeader = new System.Windows.Forms.Label();
		this.GridRptHdr = new System.Windows.Forms.DataGridView();
		this.ColLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.LblHdr = new System.Windows.Forms.Label();
		this.LblSortCols = new System.Windows.Forms.Label();
		this.GridSort = new System.Windows.Forms.DataGridView();
		this.ColName = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColSort = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.CmdBrowseIn = new System.Windows.Forms.Button();
		this.TabCols = new System.Windows.Forms.TabPage();
		this.GridCols = new System.Windows.Forms.DataGridView();
		this.ColCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColHdr = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColAlign = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colHA = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.coldt = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colwidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colformat = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colsort1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.colfilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colhide = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.ColWSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colwhereopr = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colwhereval = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coledittype = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.coleditval = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColAtBot1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colAtBot7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.cmdAddCom = new System.Windows.Forms.Button();
		this.cmbTopOf = new System.Windows.Forms.ComboBox();
		this.lblTopof = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.TxtTopof = new System.Windows.Forms.TextBox();
		this.cmdAddR = new System.Windows.Forms.Button();
		this.LblColSpan = new System.Windows.Forms.Label();
		this.CmbColSpan = new System.Windows.Forms.ComboBox();
		this.CmdDel4 = new System.Windows.Forms.Button();
		this.CmdDown4 = new System.Windows.Forms.Button();
		this.CmdUp4 = new System.Windows.Forms.Button();
		this.LblCols = new System.Windows.Forms.Label();
		this.TabRules = new System.Windows.Forms.TabPage();
		this.CmbCol = new System.Windows.Forms.ComboBox();
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
		this.CmdDel5 = new System.Windows.Forms.Button();
		this.CmdDown5 = new System.Windows.Forms.Button();
		this.CmdUp5 = new System.Windows.Forms.Button();
		this.Tab_CC = new System.Windows.Forms.TabPage();
		this.GrpCC = new System.Windows.Forms.GroupBox();
		this.cmbCCData = new System.Windows.Forms.ComboBox();
		this.cmdMacroGuid = new System.Windows.Forms.Button();
		this.CC_NewGuidBtn = new System.Windows.Forms.Button();
		this.CC_GuidLbl = new System.Windows.Forms.Label();
		this.CC_GuidTxtBx = new System.Windows.Forms.TextBox();
		this.CC_WidthMTxtBx = new System.Windows.Forms.MaskedTextBox();
		this.CC_WidthLbl = new System.Windows.Forms.Label();
		this.CC_HeaderTxtBx = new System.Windows.Forms.TextBox();
		this.CC_HeaderLbl = new System.Windows.Forms.Label();
		this.CommentsCap_CustomData_lbl = new System.Windows.Forms.Label();
		this.CommentsCap_ServiceURL_lbl = new System.Windows.Forms.Label();
		this.CmbCCURL = new System.Windows.Forms.ComboBox();
		this.CC_KeysColsListView = new System.Windows.Forms.ListView();
		this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
		this.CC_SrcColsListView = new System.Windows.Forms.ListView();
		this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.Label5 = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.CC_KeysColsOrderUpBtn = new System.Windows.Forms.Button();
		this.CC_KeysColsOrderDownBtn = new System.Windows.Forms.Button();
		this.CC_KeyAddBtn = new System.Windows.Forms.Button();
		this.CC_KeyRemoveBtn = new System.Windows.Forms.Button();
		this.CC_NoteLbl = new System.Windows.Forms.Label();
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
		this.mnuFilterCharts = new System.Windows.Forms.ToolStripMenuItem();
		this.FileExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOpenF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRptOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPreProc = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPage = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCC = new System.Windows.Forms.ToolStripMenuItem();
		this.GridOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGridWidth = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRowHt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuColWidths = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSumStatus = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAutoSize = new System.Windows.Forms.ToolStripMenuItem();
		this.mnupin = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuclearfilter = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHidecols = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuexport = new System.Windows.Forms.ToolStripMenuItem();
		this.mnucolresize = new System.Windows.Forms.ToolStripMenuItem();
		this.mnucolreorder = new System.Windows.Forms.ToolStripMenuItem();
		this.mnualtrows = new System.Windows.Forms.ToolStripMenuItem();
		this.mnumousehover = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuthemelabel = new System.Windows.Forms.ToolStripMenuItem();
		this.mnutheme = new System.Windows.Forms.ToolStripComboBox();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.FontDialog1 = new System.Windows.Forms.FontDialog();
		this.TabRpt.SuspendLayout();
		this.TabMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridCSV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridRptHdr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridSort).BeginInit();
		this.TabCols.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridCols).BeginInit();
		this.TabRules.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridColRule).BeginInit();
		this.Tab_CC.SuspendLayout();
		this.GrpCC.SuspendLayout();
		this.mnuRpt.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TabRpt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabRpt.Controls.Add(this.TabMain);
		this.TabRpt.Controls.Add(this.TabCols);
		this.TabRpt.Controls.Add(this.TabRules);
		this.TabRpt.Controls.Add(this.Tab_CC);
		this.TabRpt.Location = new System.Drawing.Point(0, 64);
		this.TabRpt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabRpt.Name = "TabRpt";
		this.TabRpt.SelectedIndex = 0;
		this.TabRpt.Size = new System.Drawing.Size(995, 634);
		this.TabRpt.TabIndex = 0;
		this.TabMain.Controls.Add(this.cmbCSV);
		this.TabMain.Controls.Add(this.cmdDel0);
		this.TabMain.Controls.Add(this.cmdDown0);
		this.TabMain.Controls.Add(this.cmdUp0);
		this.TabMain.Controls.Add(this.GridCSV);
		this.TabMain.Controls.Add(this.cmdEditVar);
		this.TabMain.Controls.Add(this.CmdDel2);
		this.TabMain.Controls.Add(this.CmdDown2);
		this.TabMain.Controls.Add(this.CmdUp2);
		this.TabMain.Controls.Add(this.CmdDel1);
		this.TabMain.Controls.Add(this.CmdDown1);
		this.TabMain.Controls.Add(this.CmdUp1);
		this.TabMain.Controls.Add(this.LblHeader);
		this.TabMain.Controls.Add(this.GridRptHdr);
		this.TabMain.Controls.Add(this.LblHdr);
		this.TabMain.Controls.Add(this.LblSortCols);
		this.TabMain.Controls.Add(this.GridSort);
		this.TabMain.Controls.Add(this.CmdBrowseIn);
		this.TabMain.Location = new System.Drawing.Point(4, 25);
		this.TabMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabMain.Name = "TabMain";
		this.TabMain.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabMain.Size = new System.Drawing.Size(987, 605);
		this.TabMain.TabIndex = 0;
		this.TabMain.Text = "Main";
		this.TabMain.UseVisualStyleBackColor = true;
		this.cmbCSV.FormattingEnabled = true;
		this.cmbCSV.Location = new System.Drawing.Point(376, 112);
		this.cmbCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.cmbCSV.Name = "cmbCSV";
		this.cmbCSV.Size = new System.Drawing.Size(121, 24);
		this.cmbCSV.TabIndex = 23;
		this.cmbCSV.Visible = false;
		this.cmdDel0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDel0.ImageIndex = 2;
		this.cmdDel0.Location = new System.Drawing.Point(903, 146);
		this.cmdDel0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdDel0.Name = "cmdDel0";
		this.cmdDel0.Size = new System.Drawing.Size(72, 31);
		this.cmdDel0.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.cmdDel0, "Delete row");
		this.cmdDel0.UseVisualStyleBackColor = true;
		this.cmdDown0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown0.ImageIndex = 1;
		this.cmdDown0.Location = new System.Drawing.Point(903, 112);
		this.cmdDown0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdDown0.Name = "cmdDown0";
		this.cmdDown0.Size = new System.Drawing.Size(72, 31);
		this.cmdDown0.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.cmdDown0, "Move row down");
		this.cmdDown0.UseVisualStyleBackColor = true;
		this.cmdUp0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp0.ImageIndex = 0;
		this.cmdUp0.Location = new System.Drawing.Point(903, 80);
		this.cmdUp0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdUp0.Name = "cmdUp0";
		this.cmdUp0.Size = new System.Drawing.Size(72, 31);
		this.cmdUp0.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.cmdUp0, "Move row up");
		this.cmdUp0.UseVisualStyleBackColor = true;
		this.GridCSV.AllowUserToAddRows = false;
		this.GridCSV.AllowUserToDeleteRows = false;
		this.GridCSV.AllowUserToResizeColumns = false;
		this.GridCSV.AllowUserToResizeRows = false;
		this.GridCSV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridCSV.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridCSV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridCSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridCSV.Columns.AddRange(this.colcsv, this.colID, this.coljoin, this.coljoin2, this.coltwidth, this.colTHeight, this.colstsht, this.colnestht, this.colht, this.colchart, this.colpageopt, this.colpageinit, this.colautorowht, this.colrowht, this.colgrp, this.colshowfilter);
		this.GridCSV.Location = new System.Drawing.Point(17, 50);
		this.GridCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.GridCSV.MultiSelect = false;
		this.GridCSV.Name = "GridCSV";
		this.GridCSV.RowHeadersWidth = 55;
		this.GridCSV.RowTemplate.Height = 24;
		this.GridCSV.Size = new System.Drawing.Size(873, 137);
		this.GridCSV.StandardTab = true;
		this.GridCSV.TabIndex = 1;
		this.colcsv.HeaderText = "File";
		this.colcsv.MinimumWidth = 6;
		this.colcsv.Name = "colcsv";
		this.colcsv.ReadOnly = true;
		this.colcsv.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colcsv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colcsv.Width = 350;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.colID.DefaultCellStyle = dataGridViewCellStyle2;
		this.colID.HeaderText = "ID";
		this.colID.MinimumWidth = 6;
		this.colID.Name = "colID";
		this.colID.ReadOnly = true;
		this.colID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colID.Width = 50;
		this.coljoin.HeaderText = "Join Col1";
		this.coljoin.MinimumWidth = 6;
		this.coljoin.Name = "coljoin";
		this.coljoin.ReadOnly = true;
		this.coljoin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.coljoin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coljoin.Width = 125;
		this.coljoin2.HeaderText = "Join Col2";
		this.coljoin2.MinimumWidth = 6;
		this.coljoin2.Name = "coljoin2";
		this.coljoin2.ReadOnly = true;
		this.coljoin2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.coljoin2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coljoin2.Width = 125;
		this.coltwidth.HeaderText = "Width";
		this.coltwidth.MinimumWidth = 6;
		this.coltwidth.Name = "coltwidth";
		this.coltwidth.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.coltwidth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coltwidth.Width = 125;
		this.colTHeight.HeaderText = "Height";
		this.colTHeight.MinimumWidth = 6;
		this.colTHeight.Name = "colTHeight";
		this.colTHeight.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colTHeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colTHeight.Width = 125;
		this.colstsht.HeaderText = "Status Height";
		this.colstsht.MinimumWidth = 6;
		this.colstsht.Name = "colstsht";
		this.colstsht.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colstsht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colstsht.Width = 125;
		this.colnestht.HeaderText = "Nested Height";
		this.colnestht.MinimumWidth = 6;
		this.colnestht.Name = "colnestht";
		this.colnestht.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colnestht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colnestht.Width = 125;
		this.colht.HeaderText = "Column Height";
		this.colht.MinimumWidth = 6;
		this.colht.Name = "colht";
		this.colht.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colht.Width = 125;
		this.colchart.HeaderText = "Chart to Filter";
		this.colchart.MinimumWidth = 6;
		this.colchart.Name = "colchart";
		this.colchart.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colchart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colchart.Visible = false;
		this.colchart.Width = 6;
		this.colpageopt.HeaderText = "Page Opts";
		this.colpageopt.MinimumWidth = 6;
		this.colpageopt.Name = "colpageopt";
		this.colpageopt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colpageopt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colpageopt.Width = 125;
		this.colpageinit.HeaderText = "Page Init";
		this.colpageinit.MinimumWidth = 6;
		this.colpageinit.Name = "colpageinit";
		this.colpageinit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colpageinit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colpageinit.Width = 125;
		this.colautorowht.HeaderText = "Auto Row Height";
		this.colautorowht.MinimumWidth = 6;
		this.colautorowht.Name = "colautorowht";
		this.colautorowht.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colautorowht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colautorowht.Width = 125;
		this.colrowht.HeaderText = "Row Height";
		this.colrowht.MinimumWidth = 6;
		this.colrowht.Name = "colrowht";
		this.colrowht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colrowht.Width = 125;
		this.colgrp.HeaderText = "Group";
		this.colgrp.MinimumWidth = 6;
		this.colgrp.Name = "colgrp";
		this.colgrp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colgrp.Width = 125;
		this.colshowfilter.HeaderText = "Show Filter";
		this.colshowfilter.MinimumWidth = 6;
		this.colshowfilter.Name = "colshowfilter";
		this.colshowfilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colshowfilter.Width = 125;
		this.cmdEditVar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdEditVar.ImageIndex = 8;
		this.cmdEditVar.Location = new System.Drawing.Point(941, 47);
		this.cmdEditVar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdEditVar.Name = "cmdEditVar";
		this.cmdEditVar.Size = new System.Drawing.Size(35, 31);
		this.cmdEditVar.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmdEditVar, "Change to a Global or Macro Variable");
		this.cmdEditVar.UseVisualStyleBackColor = true;
		this.CmdDel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel2.ImageIndex = 2;
		this.CmdDel2.Location = new System.Drawing.Point(903, 548);
		this.CmdDel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDel2.Name = "CmdDel2";
		this.CmdDel2.Size = new System.Drawing.Size(72, 31);
		this.CmdDel2.TabIndex = 14;
		this.ToolTip1.SetToolTip(this.CmdDel2, "Delete row");
		this.CmdDel2.UseVisualStyleBackColor = true;
		this.CmdDown2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown2.ImageIndex = 1;
		this.CmdDown2.Location = new System.Drawing.Point(903, 510);
		this.CmdDown2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDown2.Name = "CmdDown2";
		this.CmdDown2.Size = new System.Drawing.Size(72, 31);
		this.CmdDown2.TabIndex = 13;
		this.ToolTip1.SetToolTip(this.CmdDown2, "Move row down");
		this.CmdDown2.UseVisualStyleBackColor = true;
		this.CmdUp2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp2.ImageIndex = 0;
		this.CmdUp2.Location = new System.Drawing.Point(903, 478);
		this.CmdUp2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdUp2.Name = "CmdUp2";
		this.CmdUp2.Size = new System.Drawing.Size(72, 31);
		this.CmdUp2.TabIndex = 12;
		this.ToolTip1.SetToolTip(this.CmdUp2, "Move row up");
		this.CmdUp2.UseVisualStyleBackColor = true;
		this.CmdDel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel1.ImageIndex = 2;
		this.CmdDel1.Location = new System.Drawing.Point(903, 313);
		this.CmdDel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDel1.Name = "CmdDel1";
		this.CmdDel1.Size = new System.Drawing.Size(72, 31);
		this.CmdDel1.TabIndex = 10;
		this.ToolTip1.SetToolTip(this.CmdDel1, "Delete row");
		this.CmdDel1.UseVisualStyleBackColor = true;
		this.CmdDown1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown1.ImageIndex = 1;
		this.CmdDown1.Location = new System.Drawing.Point(903, 274);
		this.CmdDown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDown1.Name = "CmdDown1";
		this.CmdDown1.Size = new System.Drawing.Size(72, 31);
		this.CmdDown1.TabIndex = 9;
		this.ToolTip1.SetToolTip(this.CmdDown1, "Move row down");
		this.CmdDown1.UseVisualStyleBackColor = true;
		this.CmdUp1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp1.ImageIndex = 0;
		this.CmdUp1.Location = new System.Drawing.Point(903, 242);
		this.CmdUp1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdUp1.Name = "CmdUp1";
		this.CmdUp1.Size = new System.Drawing.Size(72, 31);
		this.CmdUp1.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.CmdUp1, "Move row up");
		this.CmdUp1.UseVisualStyleBackColor = true;
		this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHeader.Location = new System.Drawing.Point(13, 414);
		this.LblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHeader.Name = "LblHeader";
		this.LblHeader.Size = new System.Drawing.Size(863, 60);
		this.LblHeader.TabIndex = 10;
		this.LblHeader.Text = resources.GetString("LblHeader.Text");
		this.GridRptHdr.AllowUserToAddRows = false;
		this.GridRptHdr.AllowUserToDeleteRows = false;
		this.GridRptHdr.AllowUserToResizeColumns = false;
		this.GridRptHdr.AllowUserToResizeRows = false;
		this.GridRptHdr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridRptHdr.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridRptHdr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.GridRptHdr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridRptHdr.Columns.AddRange(this.ColLeft);
		this.GridRptHdr.Location = new System.Drawing.Point(17, 478);
		this.GridRptHdr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GridRptHdr.Name = "GridRptHdr";
		this.GridRptHdr.RowHeadersWidth = 50;
		this.GridRptHdr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GridRptHdr.Size = new System.Drawing.Size(873, 98);
		this.GridRptHdr.StandardTab = true;
		this.GridRptHdr.TabIndex = 11;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.ColLeft.DefaultCellStyle = dataGridViewCellStyle4;
		this.ColLeft.HeaderText = "Report Header";
		this.ColLeft.MinimumWidth = 6;
		this.ColLeft.Name = "ColLeft";
		this.ColLeft.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColLeft.Width = 575;
		this.LblHdr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHdr.Location = new System.Drawing.Point(13, 11);
		this.LblHdr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHdr.Name = "LblHdr";
		this.LblHdr.Size = new System.Drawing.Size(947, 36);
		this.LblHdr.TabIndex = 8;
		this.LblHdr.Text = "Specify input CSV or TAB files to report and Report Grid Attributes. Files 2 - n serve as nested tables in report connected by Join Column(s) and accessed via drilldown";
		this.LblSortCols.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblSortCols.AutoSize = true;
		this.LblSortCols.Location = new System.Drawing.Point(13, 213);
		this.LblSortCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblSortCols.Name = "LblSortCols";
		this.LblSortCols.Size = new System.Drawing.Size(278, 17);
		this.LblSortCols.TabIndex = 7;
		this.LblSortCols.Text = "Select Report Sort Columns and Sort order";
		this.GridSort.AllowUserToAddRows = false;
		this.GridSort.AllowUserToDeleteRows = false;
		this.GridSort.AllowUserToResizeColumns = false;
		this.GridSort.AllowUserToResizeRows = false;
		this.GridSort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridSort.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridSort.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
		this.GridSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridSort.Columns.AddRange(this.ColName, this.ColSort);
		this.GridSort.Location = new System.Drawing.Point(17, 242);
		this.GridSort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GridSort.Name = "GridSort";
		this.GridSort.RowHeadersWidth = 55;
		this.GridSort.Size = new System.Drawing.Size(873, 148);
		this.GridSort.StandardTab = true;
		this.GridSort.TabIndex = 7;
		this.ColName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColName.DisplayStyleForCurrentCellOnly = true;
		this.ColName.DropDownWidth = 200;
		this.ColName.HeaderText = "Column";
		this.ColName.MinimumWidth = 6;
		this.ColName.Name = "ColName";
		this.ColName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColName.Width = 235;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColSort.DefaultCellStyle = dataGridViewCellStyle6;
		this.ColSort.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColSort.DisplayStyleForCurrentCellOnly = true;
		this.ColSort.HeaderText = "Sort Order";
		this.ColSort.Items.AddRange("", "Asc", "Desc");
		this.ColSort.MinimumWidth = 6;
		this.ColSort.Name = "ColSort";
		this.ColSort.Width = 125;
		this.CmdBrowseIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowseIn.ImageIndex = 3;
		this.CmdBrowseIn.Location = new System.Drawing.Point(901, 47);
		this.CmdBrowseIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdBrowseIn.Name = "CmdBrowseIn";
		this.CmdBrowseIn.Size = new System.Drawing.Size(35, 31);
		this.CmdBrowseIn.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.CmdBrowseIn, "Browse for a CSV File");
		this.CmdBrowseIn.UseVisualStyleBackColor = true;
		this.TabCols.Controls.Add(this.GridCols);
		this.TabCols.Controls.Add(this.cmdAddCom);
		this.TabCols.Controls.Add(this.cmbTopOf);
		this.TabCols.Controls.Add(this.lblTopof);
		this.TabCols.Controls.Add(this.Label3);
		this.TabCols.Controls.Add(this.Label2);
		this.TabCols.Controls.Add(this.TxtTopof);
		this.TabCols.Controls.Add(this.cmdAddR);
		this.TabCols.Controls.Add(this.LblColSpan);
		this.TabCols.Controls.Add(this.CmbColSpan);
		this.TabCols.Controls.Add(this.CmdDel4);
		this.TabCols.Controls.Add(this.CmdDown4);
		this.TabCols.Controls.Add(this.CmdUp4);
		this.TabCols.Controls.Add(this.LblCols);
		this.TabCols.Location = new System.Drawing.Point(4, 25);
		this.TabCols.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabCols.Name = "TabCols";
		this.TabCols.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabCols.Size = new System.Drawing.Size(987, 605);
		this.TabCols.TabIndex = 1;
		this.TabCols.Text = "Columns";
		this.TabCols.UseVisualStyleBackColor = true;
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
		this.GridCols.Columns.AddRange(this.ColCol, this.ColHdr, this.ColAlign, this.colHA, this.coldt, this.colwidth, this.colformat, this.colsort1, this.colfilter, this.colhide, this.ColWSF, this.colwhereopr, this.colwhereval, this.coledittype, this.coleditval, this.ColAtBot1, this.colAtBot2, this.colAtBot3, this.colAtBot4, this.colAtBot5, this.colAtBot6, this.colAtBot7);
		this.GridCols.Location = new System.Drawing.Point(5, 167);
		this.GridCols.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GridCols.Name = "GridCols";
		this.GridCols.RowHeadersWidth = 60;
		this.GridCols.Size = new System.Drawing.Size(893, 428);
		this.GridCols.StandardTab = true;
		this.GridCols.TabIndex = 3;
		this.ColCol.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColCol.DisplayStyleForCurrentCellOnly = true;
		this.ColCol.DropDownWidth = 200;
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
		this.ColAlign.HeaderText = "Cell Align";
		this.ColAlign.Items.AddRange("", "left", "right", "center");
		this.ColAlign.MinimumWidth = 6;
		this.ColAlign.Name = "ColAlign";
		this.ColAlign.ToolTipText = "Column Cell Alignment";
		this.ColAlign.Width = 65;
		this.colHA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colHA.DisplayStyleForCurrentCellOnly = true;
		this.colHA.HeaderText = "Head Align";
		this.colHA.Items.AddRange("", "left", "right", "center");
		this.colHA.MinimumWidth = 6;
		this.colHA.Name = "colHA";
		this.colHA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colHA.ToolTipText = "Column header Alignment";
		this.colHA.Width = 65;
		this.coldt.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.coldt.DisplayStyleForCurrentCellOnly = true;
		this.coldt.HeaderText = "Data Type";
		this.coldt.Items.AddRange("", "date", "float", "string", "number");
		this.coldt.MinimumWidth = 6;
		this.coldt.Name = "coldt";
		this.coldt.ToolTipText = "Column Data Type. Important for correct Sorting/Filtering";
		this.coldt.Width = 70;
		this.colwidth.HeaderText = "Width";
		this.colwidth.MinimumWidth = 6;
		this.colwidth.Name = "colwidth";
		this.colwidth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colwidth.ToolTipText = "Enter column width in pixels or %. E.g., 100 or 15%";
		this.colwidth.Width = 60;
		this.colformat.HeaderText = "Format";
		this.colformat.MinimumWidth = 6;
		this.colformat.Name = "colformat";
		this.colformat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colformat.ToolTipText = "Format. E.g., p2 = add % + round to 2; c2 = $ + round to 2; f3 = float + round to 3 digits; d0 = thousands separator; S = Sortable Date format; dd-MMM-yyyy HH:mm:ss for date format";
		this.colformat.Width = 65;
		this.colsort1.FalseValue = "0";
		this.colsort1.HeaderText = "Sort";
		this.colsort1.MinimumWidth = 6;
		this.colsort1.Name = "colsort1";
		this.colsort1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colsort1.ToolTipText = "Column is Sortable";
		this.colsort1.TrueValue = "1";
		this.colsort1.Width = 50;
		this.colfilter.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colfilter.DisplayStyleForCurrentCellOnly = true;
		this.colfilter.HeaderText = "Filter";
		this.colfilter.Items.AddRange("chklist", "list", "input", "number", "range", "none");
		this.colfilter.MinimumWidth = 6;
		this.colfilter.Name = "colfilter";
		this.colfilter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colfilter.ToolTipText = "Use Chcklist/list for selections. Use Input for manual filter of strings & number/range for numbers";
		this.colfilter.Width = 70;
		this.colhide.FalseValue = "0";
		this.colhide.HeaderText = "Hide";
		this.colhide.MinimumWidth = 6;
		this.colhide.Name = "colhide";
		this.colhide.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colhide.ToolTipText = "Hide Column";
		this.colhide.TrueValue = "1";
		this.colhide.Width = 50;
		this.ColWSF.HeaderText = "Other Format";
		this.ColWSF.MinimumWidth = 6;
		this.ColWSF.Name = "ColWSF";
		this.ColWSF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColWSF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColWSF.ToolTipText = "Other prop. Header case:  h=u (u,l); Cell vertical Align: v=t; Separate by ;";
		this.ColWSF.Width = 65;
		this.colwhereopr.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colwhereopr.DisplayStyleForCurrentCellOnly = true;
		this.colwhereopr.HeaderText = "Where Opr";
		this.colwhereopr.Items.AddRange("", "=", "!=", "In", ">", ">=", "<", "<=", "Starts With", "Contains", "Not Contains", "Between");
		this.colwhereopr.MinimumWidth = 6;
		this.colwhereopr.Name = "colwhereopr";
		this.colwhereopr.Width = 65;
		this.colwhereval.HeaderText = "Where Val";
		this.colwhereval.MinimumWidth = 6;
		this.colwhereval.Name = "colwhereval";
		this.colwhereval.ToolTipText = "Filter values. Do not bound values in single quotes. E.g., 1 or A,B";
		this.colwhereval.Width = 125;
		this.coledittype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.coledittype.DisplayStyleForCurrentCellOnly = true;
		this.coledittype.HeaderText = "Edit Type";
		this.coledittype.Items.AddRange("", "textbox", "checkbox", "combobox", "datetimeinput");
		this.coledittype.MinimumWidth = 6;
		this.coledittype.Name = "coledittype";
		this.coledittype.ToolTipText = "Type of Control for an Editable column";
		this.coledittype.Width = 65;
		this.coleditval.HeaderText = "Edit Values";
		this.coleditval.MinimumWidth = 6;
		this.coleditval.Name = "coleditval";
		this.coleditval.ToolTipText = "Comma delimited list of valid values each bounded by single quotes. E.g., 'A','2'";
		this.coleditval.Width = 125;
		this.ColAtBot1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColAtBot1.DisplayStyleForCurrentCellOnly = true;
		this.ColAtBot1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ColAtBot1.HeaderText = "Sum";
		this.ColAtBot1.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.ColAtBot1.MinimumWidth = 6;
		this.ColAtBot1.Name = "ColAtBot1";
		this.ColAtBot1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColAtBot1.ToolTipText = "Column Summary";
		this.ColAtBot1.Width = 70;
		this.colAtBot2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot2.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot2.HeaderText = "Sum";
		this.colAtBot2.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot2.MinimumWidth = 6;
		this.colAtBot2.Name = "colAtBot2";
		this.colAtBot2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot2.ToolTipText = "Column Summary";
		this.colAtBot2.Width = 70;
		this.colAtBot3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot3.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot3.HeaderText = "Sum";
		this.colAtBot3.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot3.MinimumWidth = 6;
		this.colAtBot3.Name = "colAtBot3";
		this.colAtBot3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot3.ToolTipText = "Column Summary";
		this.colAtBot3.Width = 70;
		this.colAtBot4.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot4.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot4.HeaderText = "Sum";
		this.colAtBot4.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot4.MinimumWidth = 6;
		this.colAtBot4.Name = "colAtBot4";
		this.colAtBot4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot4.ToolTipText = "Column Summary";
		this.colAtBot4.Width = 70;
		this.colAtBot5.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot5.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot5.HeaderText = "Sum";
		this.colAtBot5.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot5.MinimumWidth = 6;
		this.colAtBot5.Name = "colAtBot5";
		this.colAtBot5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot5.ToolTipText = "Column Summary";
		this.colAtBot5.Width = 70;
		this.colAtBot6.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot6.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot6.HeaderText = "Sum";
		this.colAtBot6.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot6.MinimumWidth = 6;
		this.colAtBot6.Name = "colAtBot6";
		this.colAtBot6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot6.ToolTipText = "Column Summary";
		this.colAtBot6.Width = 70;
		this.colAtBot7.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colAtBot7.DisplayStyleForCurrentCellOnly = true;
		this.colAtBot7.HeaderText = "Sum";
		this.colAtBot7.Items.AddRange("", "Text:", "Expr:", "Avg", "Sum", "Count", "Max", "Min", "StDev", "Var", "Product", "P01", "P05", "P10", "P25", "P50", "P65", "P75", "P80", "P90", "P95", "P99");
		this.colAtBot7.MinimumWidth = 6;
		this.colAtBot7.Name = "colAtBot7";
		this.colAtBot7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colAtBot7.ToolTipText = "Column Summary";
		this.colAtBot7.Width = 70;
		this.cmdAddCom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdAddCom.Location = new System.Drawing.Point(904, 305);
		this.cmdAddCom.Name = "cmdAddCom";
		this.cmdAddCom.Size = new System.Drawing.Size(72, 31);
		this.cmdAddCom.TabIndex = 33;
		this.cmdAddCom.Text = "+ Edit";
		this.ToolTip1.SetToolTip(this.cmdAddCom, "Add Editable Comment Field");
		this.cmdAddCom.UseVisualStyleBackColor = true;
		this.cmbTopOf.FormattingEnabled = true;
		this.cmbTopOf.Location = new System.Drawing.Point(9, 70);
		this.cmbTopOf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.cmbTopOf.Name = "cmbTopOf";
		this.cmbTopOf.Size = new System.Drawing.Size(165, 24);
		this.cmbTopOf.TabIndex = 0;
		this.lblTopof.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblTopof.Location = new System.Drawing.Point(9, 5);
		this.lblTopof.Name = "lblTopof";
		this.lblTopof.Size = new System.Drawing.Size(975, 39);
		this.lblTopof.TabIndex = 32;
		this.lblTopof.Text = resources.GetString("lblTopof.Text");
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(197, 44);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(309, 17);
		this.Label3.TabIndex = 29;
		this.Label3.Text = "Drill Down Text. Reference Sort Column using {}";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(9, 44);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(146, 17);
		this.Label2.TabIndex = 28;
		this.Label2.Text = "Drilldown Sort Column";
		this.TxtTopof.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTopof.Location = new System.Drawing.Point(197, 70);
		this.TxtTopof.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTopof.Name = "TxtTopof";
		this.TxtTopof.Size = new System.Drawing.Size(777, 22);
		this.TxtTopof.TabIndex = 1;
		this.TxtTopof.Text = "Data for {}";
		this.cmdAddR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdAddR.Location = new System.Drawing.Point(904, 273);
		this.cmdAddR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdAddR.Name = "cmdAddR";
		this.cmdAddR.Size = new System.Drawing.Size(72, 31);
		this.cmdAddR.TabIndex = 7;
		this.cmdAddR.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdAddR, "Add Row");
		this.cmdAddR.UseVisualStyleBackColor = true;
		this.LblColSpan.AutoSize = true;
		this.LblColSpan.Location = new System.Drawing.Point(197, 138);
		this.LblColSpan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblColSpan.Name = "LblColSpan";
		this.LblColSpan.Size = new System.Drawing.Size(584, 17);
		this.LblColSpan.TabIndex = 23;
		this.LblColSpan.Text = "Optionally specify a special character in column headers to trigger column header spanning";
		this.ToolTip1.SetToolTip(this.LblColSpan, resources.GetString("LblColSpan.ToolTip"));
		this.CmbColSpan.FormattingEnabled = true;
		this.CmbColSpan.Items.AddRange(new object[2] { "", "@" });
		this.CmbColSpan.Location = new System.Drawing.Point(8, 134);
		this.CmbColSpan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmbColSpan.Name = "CmbColSpan";
		this.CmbColSpan.Size = new System.Drawing.Size(167, 24);
		this.CmbColSpan.TabIndex = 2;
		this.CmdDel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel4.ImageIndex = 2;
		this.CmdDel4.Location = new System.Drawing.Point(904, 241);
		this.CmdDel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDel4.Name = "CmdDel4";
		this.CmdDel4.Size = new System.Drawing.Size(72, 31);
		this.CmdDel4.TabIndex = 6;
		this.ToolTip1.SetToolTip(this.CmdDel4, "Delete row");
		this.CmdDel4.UseVisualStyleBackColor = true;
		this.CmdDown4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown4.ImageIndex = 1;
		this.CmdDown4.Location = new System.Drawing.Point(904, 201);
		this.CmdDown4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDown4.Name = "CmdDown4";
		this.CmdDown4.Size = new System.Drawing.Size(72, 31);
		this.CmdDown4.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.CmdDown4, "Move row down");
		this.CmdDown4.UseVisualStyleBackColor = true;
		this.CmdUp4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp4.ImageIndex = 0;
		this.CmdUp4.Location = new System.Drawing.Point(904, 167);
		this.CmdUp4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdUp4.Name = "CmdUp4";
		this.CmdUp4.Size = new System.Drawing.Size(72, 31);
		this.CmdUp4.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.CmdUp4, "Move row up");
		this.CmdUp4.UseVisualStyleBackColor = true;
		this.LblCols.AutoSize = true;
		this.LblCols.Location = new System.Drawing.Point(9, 111);
		this.LblCols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCols.Name = "LblCols";
		this.LblCols.Size = new System.Drawing.Size(252, 17);
		this.LblCols.TabIndex = 3;
		this.LblCols.Text = "Specify report columns and properties.";
		this.TabRules.Controls.Add(this.CmbCol);
		this.TabRules.Controls.Add(this.LblRuleColHdr);
		this.TabRules.Controls.Add(this.GridColRule);
		this.TabRules.Controls.Add(this.CmdDel5);
		this.TabRules.Controls.Add(this.CmdDown5);
		this.TabRules.Controls.Add(this.CmdUp5);
		this.TabRules.Location = new System.Drawing.Point(4, 25);
		this.TabRules.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabRules.Name = "TabRules";
		this.TabRules.Size = new System.Drawing.Size(987, 605);
		this.TabRules.TabIndex = 2;
		this.TabRules.Text = "Rules";
		this.TabRules.UseVisualStyleBackColor = true;
		this.CmbCol.DropDownWidth = 250;
		this.CmbCol.FormattingEnabled = true;
		this.CmbCol.Location = new System.Drawing.Point(68, 144);
		this.CmbCol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmbCol.Name = "CmbCol";
		this.CmbCol.Size = new System.Drawing.Size(156, 24);
		this.CmbCol.TabIndex = 28;
		this.CmbCol.Visible = false;
		this.LblRuleColHdr.Location = new System.Drawing.Point(11, 6);
		this.LblRuleColHdr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRuleColHdr.Name = "LblRuleColHdr";
		this.LblRuleColHdr.Size = new System.Drawing.Size(969, 38);
		this.LblRuleColHdr.TabIndex = 1;
		this.LblRuleColHdr.Text = resources.GetString("LblRuleColHdr.Text");
		this.GridColRule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridColRule.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridColRule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.GridColRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridColRule.Columns.AddRange(this.ColRuleCDisplay, this.ColRuleCTest, this.ColRuleCOpr, this.ColRuleCVal, this.ColRuleCBC, this.ColRuleCColor, this.ColRuleCFF, this.ColRuleCFSz, this.ColRuleCFS, this.ColRuleCFW, this.ColRuleCTA, this.ColRuleCTD, this.ColRuleCVA);
		this.GridColRule.Location = new System.Drawing.Point(11, 50);
		this.GridColRule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GridColRule.Name = "GridColRule";
		this.GridColRule.RowHeadersWidth = 60;
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
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCOpr.DefaultCellStyle = dataGridViewCellStyle10;
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
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCBC.DefaultCellStyle = dataGridViewCellStyle11;
		this.ColRuleCBC.HeaderText = "BackGround-Color";
		this.ColRuleCBC.MinimumWidth = 6;
		this.ColRuleCBC.Name = "ColRuleCBC";
		this.ColRuleCBC.ReadOnly = true;
		this.ColRuleCBC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCBC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCBC.Width = 125;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCColor.DefaultCellStyle = dataGridViewCellStyle12;
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
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFSz.DefaultCellStyle = dataGridViewCellStyle13;
		this.ColRuleCFSz.HeaderText = "Font-Size";
		this.ColRuleCFSz.MinimumWidth = 6;
		this.ColRuleCFSz.Name = "ColRuleCFSz";
		this.ColRuleCFSz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFSz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFSz.Visible = false;
		this.ColRuleCFSz.Width = 75;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFS.DefaultCellStyle = dataGridViewCellStyle14;
		this.ColRuleCFS.HeaderText = "Font-Style";
		this.ColRuleCFS.MinimumWidth = 6;
		this.ColRuleCFS.Name = "ColRuleCFS";
		this.ColRuleCFS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColRuleCFS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColRuleCFS.Visible = false;
		this.ColRuleCFS.Width = 75;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.ColRuleCFW.DefaultCellStyle = dataGridViewCellStyle15;
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
		this.ColRuleCVA.Width = 125;
		this.CmdDel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDel5.ImageIndex = 2;
		this.CmdDel5.Location = new System.Drawing.Point(911, 114);
		this.CmdDel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDel5.Name = "CmdDel5";
		this.CmdDel5.Size = new System.Drawing.Size(72, 31);
		this.CmdDel5.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.CmdDel5, "Delete row");
		this.CmdDel5.UseVisualStyleBackColor = true;
		this.CmdDown5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown5.ImageIndex = 1;
		this.CmdDown5.Location = new System.Drawing.Point(911, 79);
		this.CmdDown5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdDown5.Name = "CmdDown5";
		this.CmdDown5.Size = new System.Drawing.Size(72, 31);
		this.CmdDown5.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.CmdDown5, "Move row down");
		this.CmdDown5.UseVisualStyleBackColor = true;
		this.CmdUp5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp5.ImageIndex = 0;
		this.CmdUp5.Location = new System.Drawing.Point(911, 48);
		this.CmdUp5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdUp5.Name = "CmdUp5";
		this.CmdUp5.Size = new System.Drawing.Size(72, 31);
		this.CmdUp5.TabIndex = 1;
		this.ToolTip1.SetToolTip(this.CmdUp5, "Move row up");
		this.CmdUp5.UseVisualStyleBackColor = true;
		this.Tab_CC.Controls.Add(this.GrpCC);
		this.Tab_CC.Controls.Add(this.CC_NoteLbl);
		this.Tab_CC.Location = new System.Drawing.Point(4, 25);
		this.Tab_CC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.Tab_CC.Name = "Tab_CC";
		this.Tab_CC.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.Tab_CC.Size = new System.Drawing.Size(987, 605);
		this.Tab_CC.TabIndex = 3;
		this.Tab_CC.Text = "Capture Comments";
		this.Tab_CC.UseVisualStyleBackColor = true;
		this.GrpCC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GrpCC.Controls.Add(this.cmbCCData);
		this.GrpCC.Controls.Add(this.cmdMacroGuid);
		this.GrpCC.Controls.Add(this.CC_NewGuidBtn);
		this.GrpCC.Controls.Add(this.CC_GuidLbl);
		this.GrpCC.Controls.Add(this.CC_GuidTxtBx);
		this.GrpCC.Controls.Add(this.CC_WidthMTxtBx);
		this.GrpCC.Controls.Add(this.CC_WidthLbl);
		this.GrpCC.Controls.Add(this.CC_HeaderTxtBx);
		this.GrpCC.Controls.Add(this.CC_HeaderLbl);
		this.GrpCC.Controls.Add(this.CommentsCap_CustomData_lbl);
		this.GrpCC.Controls.Add(this.CommentsCap_ServiceURL_lbl);
		this.GrpCC.Controls.Add(this.CmbCCURL);
		this.GrpCC.Controls.Add(this.CC_KeysColsListView);
		this.GrpCC.Controls.Add(this.CC_SrcColsListView);
		this.GrpCC.Controls.Add(this.Label5);
		this.GrpCC.Controls.Add(this.Label4);
		this.GrpCC.Controls.Add(this.Label1);
		this.GrpCC.Controls.Add(this.CC_KeysColsOrderUpBtn);
		this.GrpCC.Controls.Add(this.CC_KeysColsOrderDownBtn);
		this.GrpCC.Controls.Add(this.CC_KeyAddBtn);
		this.GrpCC.Controls.Add(this.CC_KeyRemoveBtn);
		this.GrpCC.Enabled = false;
		this.GrpCC.Location = new System.Drawing.Point(4, 66);
		this.GrpCC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GrpCC.Name = "GrpCC";
		this.GrpCC.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.GrpCC.Size = new System.Drawing.Size(964, 529);
		this.GrpCC.TabIndex = 36;
		this.GrpCC.TabStop = false;
		this.cmbCCData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbCCData.FormattingEnabled = true;
		this.cmbCCData.Location = new System.Drawing.Point(111, 50);
		this.cmbCCData.Name = "cmbCCData";
		this.cmbCCData.Size = new System.Drawing.Size(833, 24);
		this.cmbCCData.TabIndex = 1;
		this.cmdMacroGuid.Location = new System.Drawing.Point(619, 114);
		this.cmdMacroGuid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdMacroGuid.Name = "cmdMacroGuid";
		this.cmdMacroGuid.Size = new System.Drawing.Size(100, 28);
		this.cmdMacroGuid.TabIndex = 6;
		this.cmdMacroGuid.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMacroGuid, "Replace GUID with a Macro/Global Variable");
		this.cmdMacroGuid.UseVisualStyleBackColor = true;
		this.CC_NewGuidBtn.Location = new System.Drawing.Point(505, 114);
		this.CC_NewGuidBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_NewGuidBtn.Name = "CC_NewGuidBtn";
		this.CC_NewGuidBtn.Size = new System.Drawing.Size(100, 28);
		this.CC_NewGuidBtn.TabIndex = 5;
		this.CC_NewGuidBtn.TabStop = false;
		this.CC_NewGuidBtn.Text = "New GUID";
		this.CC_NewGuidBtn.UseVisualStyleBackColor = true;
		this.CC_GuidLbl.AutoSize = true;
		this.CC_GuidLbl.Location = new System.Drawing.Point(49, 118);
		this.CC_GuidLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.CC_GuidLbl.Name = "CC_GuidLbl";
		this.CC_GuidLbl.Size = new System.Drawing.Size(54, 17);
		this.CC_GuidLbl.TabIndex = 45;
		this.CC_GuidLbl.Text = "GUID : ";
		this.CC_GuidTxtBx.Location = new System.Drawing.Point(111, 114);
		this.CC_GuidTxtBx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_GuidTxtBx.Name = "CC_GuidTxtBx";
		this.CC_GuidTxtBx.ReadOnly = true;
		this.CC_GuidTxtBx.Size = new System.Drawing.Size(377, 22);
		this.CC_GuidTxtBx.TabIndex = 4;
		this.CC_WidthMTxtBx.HidePromptOnLeave = true;
		this.CC_WidthMTxtBx.Location = new System.Drawing.Point(457, 82);
		this.CC_WidthMTxtBx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_WidthMTxtBx.Mask = "000";
		this.CC_WidthMTxtBx.Name = "CC_WidthMTxtBx";
		this.CC_WidthMTxtBx.Size = new System.Drawing.Size(31, 22);
		this.CC_WidthMTxtBx.TabIndex = 3;
		this.CC_WidthMTxtBx.Text = "150";
		this.CC_WidthLbl.AutoSize = true;
		this.CC_WidthLbl.Location = new System.Drawing.Point(399, 85);
		this.CC_WidthLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.CC_WidthLbl.Name = "CC_WidthLbl";
		this.CC_WidthLbl.Size = new System.Drawing.Size(48, 17);
		this.CC_WidthLbl.TabIndex = 41;
		this.CC_WidthLbl.Text = "Width:";
		this.CC_HeaderTxtBx.Location = new System.Drawing.Point(111, 82);
		this.CC_HeaderTxtBx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_HeaderTxtBx.Name = "CC_HeaderTxtBx";
		this.CC_HeaderTxtBx.Size = new System.Drawing.Size(243, 22);
		this.CC_HeaderTxtBx.TabIndex = 2;
		this.CC_HeaderTxtBx.Text = "Comments";
		this.ToolTip1.SetToolTip(this.CC_HeaderTxtBx, "Please provide the header for column \r\nwhich will capture comments from end user \r\nin report");
		this.CC_HeaderLbl.AutoSize = true;
		this.CC_HeaderLbl.Location = new System.Drawing.Point(43, 85);
		this.CC_HeaderLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.CC_HeaderLbl.Name = "CC_HeaderLbl";
		this.CC_HeaderLbl.Size = new System.Drawing.Size(59, 17);
		this.CC_HeaderLbl.TabIndex = 39;
		this.CC_HeaderLbl.Text = "Header:";
		this.CommentsCap_CustomData_lbl.AutoSize = true;
		this.CommentsCap_CustomData_lbl.Location = new System.Drawing.Point(8, 54);
		this.CommentsCap_CustomData_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.CommentsCap_CustomData_lbl.Name = "CommentsCap_CustomData_lbl";
		this.CommentsCap_CustomData_lbl.Size = new System.Drawing.Size(93, 17);
		this.CommentsCap_CustomData_lbl.TabIndex = 38;
		this.CommentsCap_CustomData_lbl.Text = "Custom Data:";
		this.CommentsCap_ServiceURL_lbl.AutoSize = true;
		this.CommentsCap_ServiceURL_lbl.Location = new System.Drawing.Point(8, 22);
		this.CommentsCap_ServiceURL_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.CommentsCap_ServiceURL_lbl.Name = "CommentsCap_ServiceURL_lbl";
		this.CommentsCap_ServiceURL_lbl.Size = new System.Drawing.Size(91, 17);
		this.CommentsCap_ServiceURL_lbl.TabIndex = 37;
		this.CommentsCap_ServiceURL_lbl.Text = "Service URL:";
		this.CmbCCURL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmbCCURL.FormattingEnabled = true;
		this.CmbCCURL.Location = new System.Drawing.Point(111, 18);
		this.CmbCCURL.Name = "CmbCCURL";
		this.CmbCCURL.Size = new System.Drawing.Size(833, 24);
		this.CmbCCURL.TabIndex = 0;
		this.CC_KeysColsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.ColumnHeader2 });
		this.CC_KeysColsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.CC_KeysColsListView.HideSelection = false;
		this.CC_KeysColsListView.Location = new System.Drawing.Point(509, 266);
		this.CC_KeysColsListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_KeysColsListView.Name = "CC_KeysColsListView";
		this.CC_KeysColsListView.Size = new System.Drawing.Size(316, 250);
		this.CC_KeysColsListView.TabIndex = 10;
		this.CC_KeysColsListView.UseCompatibleStateImageBehavior = false;
		this.CC_KeysColsListView.View = System.Windows.Forms.View.Details;
		this.ColumnHeader2.Text = "";
		this.CC_SrcColsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.ColumnHeader1 });
		this.CC_SrcColsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.CC_SrcColsListView.HideSelection = false;
		this.CC_SrcColsListView.Location = new System.Drawing.Point(41, 266);
		this.CC_SrcColsListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_SrcColsListView.Name = "CC_SrcColsListView";
		this.CC_SrcColsListView.Size = new System.Drawing.Size(316, 250);
		this.CC_SrcColsListView.TabIndex = 7;
		this.CC_SrcColsListView.UseCompatibleStateImageBehavior = false;
		this.CC_SrcColsListView.View = System.Windows.Forms.View.Details;
		this.ColumnHeader1.Text = "";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(505, 242);
		this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(98, 17);
		this.Label5.TabIndex = 46;
		this.Label5.Text = "Key Columns :";
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(37, 242);
		this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(117, 17);
		this.Label4.TabIndex = 45;
		this.Label4.Text = "Report Columns :";
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(37, 219);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(370, 17);
		this.Label1.TabIndex = 44;
		this.Label1.Text = "Add Report Column(s) as Keys to identify a row uniquely :";
		this.CC_KeysColsOrderUpBtn.Location = new System.Drawing.Point(845, 359);
		this.CC_KeysColsOrderUpBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_KeysColsOrderUpBtn.Name = "CC_KeysColsOrderUpBtn";
		this.CC_KeysColsOrderUpBtn.Size = new System.Drawing.Size(100, 28);
		this.CC_KeysColsOrderUpBtn.TabIndex = 11;
		this.CC_KeysColsOrderUpBtn.UseVisualStyleBackColor = true;
		this.CC_KeysColsOrderDownBtn.Location = new System.Drawing.Point(845, 451);
		this.CC_KeysColsOrderDownBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_KeysColsOrderDownBtn.Name = "CC_KeysColsOrderDownBtn";
		this.CC_KeysColsOrderDownBtn.Size = new System.Drawing.Size(100, 28);
		this.CC_KeysColsOrderDownBtn.TabIndex = 12;
		this.CC_KeysColsOrderDownBtn.UseVisualStyleBackColor = true;
		this.CC_KeyAddBtn.Location = new System.Drawing.Point(384, 359);
		this.CC_KeyAddBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_KeyAddBtn.Name = "CC_KeyAddBtn";
		this.CC_KeyAddBtn.Size = new System.Drawing.Size(100, 28);
		this.CC_KeyAddBtn.TabIndex = 8;
		this.CC_KeyAddBtn.Text = ">>";
		this.CC_KeyAddBtn.UseVisualStyleBackColor = true;
		this.CC_KeyRemoveBtn.Location = new System.Drawing.Point(384, 451);
		this.CC_KeyRemoveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CC_KeyRemoveBtn.Name = "CC_KeyRemoveBtn";
		this.CC_KeyRemoveBtn.Size = new System.Drawing.Size(100, 28);
		this.CC_KeyRemoveBtn.TabIndex = 9;
		this.CC_KeyRemoveBtn.Text = "<<";
		this.CC_KeyRemoveBtn.UseVisualStyleBackColor = true;
		this.CC_NoteLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CC_NoteLbl.Location = new System.Drawing.Point(9, 10);
		this.CC_NoteLbl.Name = "CC_NoteLbl";
		this.CC_NoteLbl.Size = new System.Drawing.Size(969, 39);
		this.CC_NoteLbl.TabIndex = 37;
		this.CC_NoteLbl.Text = "Optionally enable Comments capture feature for the report. Provide the information needed below.";
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
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuFile, this.mnuFilterCharts, this.FileExternalToolStripMenuItem, this.mnuRptOptions });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
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
		this.mnuFilterCharts.Name = "mnuFilterCharts";
		this.mnuFilterCharts.Size = new System.Drawing.Size(101, 24);
		this.mnuFilterCharts.Text = "Filter Charts";
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
		this.mnuRptOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuPreProc, this.mnuPage, this.mnuCC, this.GridOptionsToolStripMenuItem, this.mnuthemelabel });
		this.mnuRptOptions.Name = "mnuRptOptions";
		this.mnuRptOptions.Size = new System.Drawing.Size(124, 24);
		this.mnuRptOptions.Text = "Report Options";
		this.mnuPreProc.CheckOnClick = true;
		this.mnuPreProc.Name = "mnuPreProc";
		this.mnuPreProc.Size = new System.Drawing.Size(322, 26);
		this.mnuPreProc.Text = "Pre-Process CSV/TAB File";
		this.mnuPage.CheckOnClick = true;
		this.mnuPage.Name = "mnuPage";
		this.mnuPage.Size = new System.Drawing.Size(322, 26);
		this.mnuPage.Text = "Pageable Top Grid";
		this.mnuCC.CheckOnClick = true;
		this.mnuCC.Name = "mnuCC";
		this.mnuCC.Size = new System.Drawing.Size(322, 26);
		this.mnuCC.Text = "Comments Capture";
		this.GridOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.mnuGridWidth, this.mnuRowHt, this.mnuColWidths, this.mnuSumStatus, this.mnuAutoSize, this.mnupin, this.mnuclearfilter, this.mnuHidecols, this.mnuexport, this.mnucolresize,
			this.mnucolreorder, this.mnualtrows, this.mnumousehover
		});
		this.GridOptionsToolStripMenuItem.Name = "GridOptionsToolStripMenuItem";
		this.GridOptionsToolStripMenuItem.Size = new System.Drawing.Size(322, 26);
		this.GridOptionsToolStripMenuItem.Text = "Show/Enable Grid Toolbar Options";
		this.mnuGridWidth.Checked = true;
		this.mnuGridWidth.CheckOnClick = true;
		this.mnuGridWidth.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuGridWidth.Name = "mnuGridWidth";
		this.mnuGridWidth.Size = new System.Drawing.Size(246, 26);
		this.mnuGridWidth.Text = "Set Grid Width";
		this.mnuRowHt.Checked = true;
		this.mnuRowHt.CheckOnClick = true;
		this.mnuRowHt.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuRowHt.Name = "mnuRowHt";
		this.mnuRowHt.Size = new System.Drawing.Size(246, 26);
		this.mnuRowHt.Text = "Set Row Height";
		this.mnuColWidths.Checked = true;
		this.mnuColWidths.CheckOnClick = true;
		this.mnuColWidths.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuColWidths.Name = "mnuColWidths";
		this.mnuColWidths.Size = new System.Drawing.Size(246, 26);
		this.mnuColWidths.Text = "Get Column Widths";
		this.mnuSumStatus.Checked = true;
		this.mnuSumStatus.CheckOnClick = true;
		this.mnuSumStatus.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuSumStatus.Name = "mnuSumStatus";
		this.mnuSumStatus.Size = new System.Drawing.Size(246, 26);
		this.mnuSumStatus.Text = "Summary Bar";
		this.mnuAutoSize.Checked = true;
		this.mnuAutoSize.CheckOnClick = true;
		this.mnuAutoSize.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuAutoSize.Name = "mnuAutoSize";
		this.mnuAutoSize.Size = new System.Drawing.Size(246, 26);
		this.mnuAutoSize.Text = "Autosize Columns";
		this.mnupin.Checked = true;
		this.mnupin.CheckOnClick = true;
		this.mnupin.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnupin.Name = "mnupin";
		this.mnupin.Size = new System.Drawing.Size(246, 26);
		this.mnupin.Text = "Pin/Unpin Column 1";
		this.mnuclearfilter.Checked = true;
		this.mnuclearfilter.CheckOnClick = true;
		this.mnuclearfilter.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuclearfilter.Name = "mnuclearfilter";
		this.mnuclearfilter.Size = new System.Drawing.Size(246, 26);
		this.mnuclearfilter.Text = "Clear Filters";
		this.mnuHidecols.Checked = true;
		this.mnuHidecols.CheckOnClick = true;
		this.mnuHidecols.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuHidecols.Name = "mnuHidecols";
		this.mnuHidecols.Size = new System.Drawing.Size(246, 26);
		this.mnuHidecols.Text = "Hide Columns";
		this.mnuexport.Checked = true;
		this.mnuexport.CheckOnClick = true;
		this.mnuexport.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuexport.Name = "mnuexport";
		this.mnuexport.Size = new System.Drawing.Size(246, 26);
		this.mnuexport.Text = "Export Data";
		this.mnucolresize.Checked = true;
		this.mnucolresize.CheckOnClick = true;
		this.mnucolresize.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnucolresize.Name = "mnucolresize";
		this.mnucolresize.Size = new System.Drawing.Size(246, 26);
		this.mnucolresize.Text = "Column Resize";
		this.mnucolreorder.Checked = true;
		this.mnucolreorder.CheckOnClick = true;
		this.mnucolreorder.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnucolreorder.Name = "mnucolreorder";
		this.mnucolreorder.Size = new System.Drawing.Size(246, 26);
		this.mnucolreorder.Text = "Column Reorder";
		this.mnualtrows.Checked = true;
		this.mnualtrows.CheckOnClick = true;
		this.mnualtrows.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnualtrows.Name = "mnualtrows";
		this.mnualtrows.Size = new System.Drawing.Size(246, 26);
		this.mnualtrows.Text = "Alternating Rows";
		this.mnumousehover.Checked = true;
		this.mnumousehover.CheckOnClick = true;
		this.mnumousehover.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnumousehover.Name = "mnumousehover";
		this.mnumousehover.Size = new System.Drawing.Size(246, 26);
		this.mnumousehover.Text = "Mouse Hover Highlight";
		this.mnuthemelabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnutheme });
		this.mnuthemelabel.Name = "mnuthemelabel";
		this.mnuthemelabel.Size = new System.Drawing.Size(322, 26);
		this.mnuthemelabel.Text = "Report Theme";
		this.mnutheme.Items.AddRange(new object[9] { "", "Arctic", "Classic", "Dark", "Dark Blue", "Energy Blue", "Light", "ui-Redmond", "ui-Sunny" });
		this.mnutheme.Name = "mnutheme";
		this.mnutheme.Size = new System.Drawing.Size(121, 28);
		this.mnutheme.Sorted = true;
		this.mnutheme.Text = "Classic";
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdOK.ImageIndex = 4;
		this.CmdOK.Location = new System.Drawing.Point(1003, 87);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(73, 49);
		this.CmdOK.TabIndex = 15;
		this.CmdOK.Text = "OK";
		this.CmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 5;
		this.CmdCancel.Location = new System.Drawing.Point(1003, 142);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(73, 49);
		this.CmdCancel.TabIndex = 16;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(1084, 703);
		base.ControlBox = false;
		base.Controls.Add(this.mnuRpt);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.TabRpt);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmReporti";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SQLPathFinder Report Writer";
		this.TabRpt.ResumeLayout(false);
		this.TabMain.ResumeLayout(false);
		this.TabMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridCSV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridRptHdr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridSort).EndInit();
		this.TabCols.ResumeLayout(false);
		this.TabCols.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridCols).EndInit();
		this.TabRules.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridColRule).EndInit();
		this.Tab_CC.ResumeLayout(false);
		this.GrpCC.ResumeLayout(false);
		this.GrpCC.PerformLayout();
		this.mnuRpt.ResumeLayout(false);
		this.mnuRpt.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Set_CC_Enabled_ver(string MyVal)
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
						case 3:
							goto IL_0014;
						case 4:
							goto IL_002e;
						case 5:
							goto IL_003d;
						case 6:
							goto IL_004c;
						case 7:
							goto IL_005b;
						case 8:
							goto IL_006a;
						case 10:
							goto IL_007d;
						case 11:
							goto IL_008d;
						case 12:
							goto IL_009d;
						case 13:
							goto IL_00ad;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008d:
					num2 = 11;
					CC_WidthMTxtBx.Visible = true;
					goto IL_009d;
					IL_009d:
					num2 = 12;
					CC_HeaderLbl.Visible = true;
					goto IL_00ad;
					IL_007d:
					num2 = 10;
					CC_HeaderTxtBx.Visible = true;
					goto IL_008d;
					IL_00ad:
					num2 = 13;
					CC_WidthLbl.Visible = true;
					break;
					IL_000b:
					num2 = 2;
					cc_enabled_ver = MyVal;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(cc_enabled_ver, "2", TextCompare: false) == 0)
					{
						goto IL_002e;
					}
					goto IL_007d;
					IL_002e:
					num2 = 4;
					CC_HeaderTxtBx.Visible = false;
					goto IL_003d;
					IL_003d:
					num2 = 5;
					CC_WidthMTxtBx.Visible = false;
					goto IL_004c;
					IL_004c:
					num2 = 6;
					CC_HeaderLbl.Visible = false;
					goto IL_005b;
					IL_005b:
					num2 = 7;
					CC_WidthLbl.Visible = false;
					goto IL_006a;
					IL_006a:
					num2 = 8;
					cmdAddCom.Visible = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				cmdAddCom.Visible = false;
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

	public string MyYN(bool MyVal)
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
				case 77:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					result = "Y";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (MyVal)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				result = "N";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 77;
				continue;
			}
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

	public void SetGridOpts(ref string MyOpts, string Mode)
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
				case 1473:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0043;
						case 6:
							goto IL_005f;
						case 7:
							goto IL_007b;
						case 8:
							goto IL_0097;
						case 9:
							goto IL_00b3;
						case 10:
							goto IL_00d0;
						case 11:
							goto IL_00ed;
						case 12:
							goto IL_010a;
						case 13:
							goto IL_0127;
						case 14:
							goto IL_0144;
						case 15:
							goto IL_0161;
						case 16:
							goto IL_017e;
						case 17:
							goto IL_019b;
						case 19:
							goto IL_01be;
						case 20:
							goto IL_01ca;
						case 21:
							goto IL_01f7;
						case 22:
							goto IL_0207;
						case 23:
							goto IL_0234;
						case 24:
							goto IL_0244;
						case 25:
							goto IL_0271;
						case 26:
							goto IL_0281;
						case 27:
							goto IL_02ae;
						case 28:
							goto IL_02be;
						case 29:
							goto IL_02eb;
						case 30:
							goto IL_02fb;
						case 31:
							goto IL_0328;
						case 32:
							goto IL_0338;
						case 33:
							goto IL_0365;
						case 34:
							goto IL_0375;
						case 35:
							goto IL_03a2;
						case 36:
							goto IL_03b2;
						case 37:
							goto IL_03e1;
						case 38:
							goto IL_03f1;
						case 39:
							goto IL_0420;
						case 40:
							goto IL_0430;
						case 41:
							goto IL_045f;
						case 42:
							goto IL_046f;
						case 43:
							goto IL_049e;
						case 44:
							goto IL_04ae;
						case 45:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 18:
						case 46:
						case 47:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_049e:
					num2 = 43;
					mnualtrows.Checked = false;
					goto IL_04ae;
					IL_04ae:
					num2 = 44;
					if (Strings.Len(MyOpts) >= 13 && Operators.CompareString(Strings.Mid(MyOpts, 13, 1), "N", TextCompare: false) == 0)
					{
						break;
					}
					goto end_IL_0001_3;
					IL_017e:
					num2 = 16;
					MyOpts += MyYN(mnualtrows.Checked);
					goto IL_019b;
					IL_019b:
					num2 = 17;
					MyOpts += MyYN(mnumousehover.Checked);
					goto end_IL_0001_3;
					IL_000c:
					num2 = 2;
					left = Strings.UCase(Mode);
					if (Operators.CompareString(left, "G", TextCompare: false) == 0)
					{
						goto IL_003a;
					}
					if (Operators.CompareString(left, "S", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_01be;
					IL_003a:
					num2 = 4;
					MyOpts = "";
					goto IL_0043;
					IL_01be:
					num2 = 19;
					MyOpts = Strings.UCase(MyOpts);
					goto IL_01ca;
					IL_01ca:
					num2 = 20;
					if (Strings.Len(MyOpts) >= 1 && Operators.CompareString(Strings.Mid(MyOpts, 1, 1), "N", TextCompare: false) == 0)
					{
						goto IL_01f7;
					}
					goto IL_0207;
					IL_0043:
					num2 = 5;
					MyOpts += MyYN(mnuGridWidth.Checked);
					goto IL_005f;
					IL_01f7:
					num2 = 21;
					mnuGridWidth.Checked = false;
					goto IL_0207;
					IL_0207:
					num2 = 22;
					if (Strings.Len(MyOpts) >= 2 && Operators.CompareString(Strings.Mid(MyOpts, 2, 1), "N", TextCompare: false) == 0)
					{
						goto IL_0234;
					}
					goto IL_0244;
					IL_005f:
					num2 = 6;
					MyOpts += MyYN(mnuRowHt.Checked);
					goto IL_007b;
					IL_0234:
					num2 = 23;
					mnuRowHt.Checked = false;
					goto IL_0244;
					IL_0244:
					num2 = 24;
					if (Strings.Len(MyOpts) >= 3 && Operators.CompareString(Strings.Mid(MyOpts, 3, 1), "N", TextCompare: false) == 0)
					{
						goto IL_0271;
					}
					goto IL_0281;
					IL_007b:
					num2 = 7;
					MyOpts += MyYN(mnuColWidths.Checked);
					goto IL_0097;
					IL_0271:
					num2 = 25;
					mnuColWidths.Checked = false;
					goto IL_0281;
					IL_0281:
					num2 = 26;
					if (Strings.Len(MyOpts) >= 4 && Operators.CompareString(Strings.Mid(MyOpts, 4, 1), "N", TextCompare: false) == 0)
					{
						goto IL_02ae;
					}
					goto IL_02be;
					IL_0097:
					num2 = 8;
					MyOpts += MyYN(mnuSumStatus.Checked);
					goto IL_00b3;
					IL_02ae:
					num2 = 27;
					mnuSumStatus.Checked = false;
					goto IL_02be;
					IL_02be:
					num2 = 28;
					if (Strings.Len(MyOpts) >= 5 && Operators.CompareString(Strings.Mid(MyOpts, 5, 1), "N", TextCompare: false) == 0)
					{
						goto IL_02eb;
					}
					goto IL_02fb;
					IL_00b3:
					num2 = 9;
					MyOpts += MyYN(mnuAutoSize.Checked);
					goto IL_00d0;
					IL_02eb:
					num2 = 29;
					mnuAutoSize.Checked = false;
					goto IL_02fb;
					IL_02fb:
					num2 = 30;
					if (Strings.Len(MyOpts) >= 6 && Operators.CompareString(Strings.Mid(MyOpts, 6, 1), "N", TextCompare: false) == 0)
					{
						goto IL_0328;
					}
					goto IL_0338;
					IL_00d0:
					num2 = 10;
					MyOpts += MyYN(mnupin.Checked);
					goto IL_00ed;
					IL_0328:
					num2 = 31;
					mnupin.Checked = false;
					goto IL_0338;
					IL_0338:
					num2 = 32;
					if (Strings.Len(MyOpts) >= 7 && Operators.CompareString(Strings.Mid(MyOpts, 7, 1), "N", TextCompare: false) == 0)
					{
						goto IL_0365;
					}
					goto IL_0375;
					IL_00ed:
					num2 = 11;
					MyOpts += MyYN(mnuclearfilter.Checked);
					goto IL_010a;
					IL_0365:
					num2 = 33;
					mnuclearfilter.Checked = false;
					goto IL_0375;
					IL_0375:
					num2 = 34;
					if (Strings.Len(MyOpts) >= 8 && Operators.CompareString(Strings.Mid(MyOpts, 8, 1), "N", TextCompare: false) == 0)
					{
						goto IL_03a2;
					}
					goto IL_03b2;
					IL_010a:
					num2 = 12;
					MyOpts += MyYN(mnuHidecols.Checked);
					goto IL_0127;
					IL_03a2:
					num2 = 35;
					mnuHidecols.Checked = false;
					goto IL_03b2;
					IL_03b2:
					num2 = 36;
					if (Strings.Len(MyOpts) >= 9 && Operators.CompareString(Strings.Mid(MyOpts, 9, 1), "N", TextCompare: false) == 0)
					{
						goto IL_03e1;
					}
					goto IL_03f1;
					IL_0127:
					num2 = 13;
					MyOpts += MyYN(mnuexport.Checked);
					goto IL_0144;
					IL_03e1:
					num2 = 37;
					mnuexport.Checked = false;
					goto IL_03f1;
					IL_03f1:
					num2 = 38;
					if (Strings.Len(MyOpts) >= 10 && Operators.CompareString(Strings.Mid(MyOpts, 10, 1), "N", TextCompare: false) == 0)
					{
						goto IL_0420;
					}
					goto IL_0430;
					IL_0144:
					num2 = 14;
					MyOpts += MyYN(mnucolresize.Checked);
					goto IL_0161;
					IL_0420:
					num2 = 39;
					mnucolresize.Checked = false;
					goto IL_0430;
					IL_0430:
					num2 = 40;
					if (Strings.Len(MyOpts) >= 11 && Operators.CompareString(Strings.Mid(MyOpts, 11, 1), "N", TextCompare: false) == 0)
					{
						goto IL_045f;
					}
					goto IL_046f;
					IL_0161:
					num2 = 15;
					MyOpts += MyYN(mnucolreorder.Checked);
					goto IL_017e;
					IL_045f:
					num2 = 41;
					mnucolreorder.Checked = false;
					goto IL_046f;
					IL_046f:
					num2 = 42;
					if (Strings.Len(MyOpts) >= 12 && Operators.CompareString(Strings.Mid(MyOpts, 12, 1), "N", TextCompare: false) == 0)
					{
						goto IL_049e;
					}
					goto IL_04ae;
					end_IL_0001_2:
					break;
				}
				num2 = 45;
				mnumousehover.Checked = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1473;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string Get_Candidate_Drilldown_column()
	{
		int num = 0;
		int num2 = 0;
		string text = "";
		bool flag = false;
		string text2 = "";
		string text3 = "";
		string right = "";
		string text4 = "";
		string text5 = "";
		int num3 = 0;
		string result = "";
		text2 = Conversions.ToString(GridCSV.Rows[0].Cells[1].Value);
		checked
		{
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				int num4 = GridSort.RowCount - 1;
				for (num = 0; num <= num4; num++)
				{
					if (Operators.CompareString(text2 + ":", Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(GridSort.Rows[num].Cells[0].Value, "    ")), 1, Strings.Len(text2 + ":")), TextCompare: false) == 0)
					{
						text3 = Conversions.ToString(GridSort.Rows[num].Cells[0].Value);
						right = Strings.Mid(text3, Strings.Len(text2 + ":") + 1);
						break;
					}
				}
				if (Operators.CompareString(text3, "", TextCompare: false) != 0)
				{
					int num5 = GridCSV.RowCount - 1;
					for (num2 = 1; num2 <= num5; num2++)
					{
						text4 = Conversions.ToString(GridCSV.Rows[num2].Cells[1].Value);
						flag = false;
						if (Operators.CompareString(text4, "", TextCompare: false) != 0)
						{
							int num6 = GridSort.RowCount - 1;
							for (num = 0; num <= num6; num++)
							{
								text = Conversions.ToString(GridSort.Rows[num].Cells[0].Value);
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									num3 = Strings.InStr(text, ":");
									if (Operators.CompareString(Strings.Mid(text, 1, num3), text4 + ":", TextCompare: false) == 0)
									{
										flag = Operators.CompareString(Strings.Mid(text, num3 + 1), right, TextCompare: false) == 0;
										break;
									}
								}
							}
						}
						else
						{
							flag = true;
						}
						if (!flag)
						{
							break;
						}
					}
					if (flag)
					{
						result = Strings.Replace(text3, text2 + ":", "", 1, -1, CompareMethod.Text);
					}
				}
			}
			return result;
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
				case 437:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 21:
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a8:
					num2 = 16;
					CmbCol.Items.Add("");
					goto IL_00c1;
					IL_00c1:
					num2 = 17;
					num5 = checked(ColCol.Items.Count - 1);
					num6 = 5;
					goto IL_0111;
					IL_0094:
					num2 = 15;
					CmbCol.Items.Clear();
					goto IL_00a8;
					IL_0111:
					if (num6 > num5)
					{
						break;
					}
					goto IL_00dd;
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
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				ComboBox MyCombo = CmbCol;
				GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 2);
				CmbCol = MyCombo;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 437;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
						case 4:
							goto IL_0030;
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
					if (CmbCol.Visible)
					{
						goto IL_001c;
					}
					goto IL_0030;
					IL_001c:
					num2 = 3;
					CmbCol_Leave(CmbCol, new EventArgs());
					goto IL_0030;
					IL_0030:
					num2 = 4;
					if (!cmbCSV.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				cmbCSV_Leave(cmbCSV, new EventArgs());
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
					case 227:
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
				try0001_dispatch = 227;
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
						Clear_All_Grids();
						GridCols.RowCount = 1999;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmReport - Open_Report";
						string[] lCSVCols = new string[0];
						int num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						int num4 = -1;
						int num5 = -1;
						int num6 = -1;
						int num7 = -1;
						int num8 = 0;
						int num9 = 0;
						int num10 = 0;
						int num11 = 0;
						int num12 = 0;
						int num13 = 0;
						int num14 = 0;
						int num15 = 0;
						int num16 = -1;
						string text5 = "";
						int num17 = -1;
						string[] array = Strings.Split(MyRptSpec, "\r\n");
						num11 = -1;
						int num18 = Information.UBound(array);
						for (num10 = 0; num10 <= num18; num10++)
						{
							if (!LikeOperator.LikeString(Strings.UCase(array[num10]), "INPUT-FILE*", CompareMethod.Binary))
							{
								continue;
							}
							string[] array2 = Strings.Split(array[num10], "<\\\\>");
							text4 = Strings.Trim(Strings.UCase(array2[0]));
							if (Operators.CompareString(array2[1], "", TextCompare: false) == 0)
							{
								continue;
							}
							num11++;
							int num19 = GridCSV.ColumnCount - 1;
							for (num12 = 0; num12 <= num19; num12++)
							{
								text = array2[num12 + 1];
								if (num12 >= 4 && num12 <= 8 && Operators.CompareString(Strings.UCase(Strings.Mid(text + "     ", 1, 5)), "AUTO:", TextCompare: false) == 0)
								{
									text = "Auto";
								}
								GridCSV.Rows[num11].Cells[num12].Value = text;
							}
						}
						num16 = num11 + 1;
						text = "";
						text2 = "";
						text3 = "";
						if (num16 > 0)
						{
							text = "";
							int num20 = num16;
							for (num11 = 1; num11 <= num20; num11++)
							{
								num3 = General_Procedures.GetCSVHeaders(Conversions.ToString(GridCSV.Rows[num11 - 1].Cells[0].Value), ref lCSVCols);
								text5 = Conversions.ToString(GridCSV.Rows[num11 - 1].Cells[1].Value);
								if (num3 == 1)
								{
									text = "M";
								}
								if (num3 == 0)
								{
									int num21 = Information.UBound(lCSVCols);
									for (num10 = 0; num10 <= num21; num10++)
									{
										ColName.Items.Add(text5 + ":" + lCSVCols[num10]);
										ColCol.Items.Add(text5 + ":" + lCSVCols[num10]);
									}
								}
							}
							if (Operators.CompareString(text, "M", TextCompare: false) == 0)
							{
								Interaction.MsgBox("Note that one or more of the CSV Files you are reporting on was not found or has no columns. Data for the last report design will be shown but you will be unable to make significant updates", MsgBoxStyle.Exclamation, "Missing CSV Data");
							}
							int num22 = Information.UBound(array);
							DataGridView GridFormat;
							string[] array2;
							for (num10 = 0; num10 <= num22; num10++)
							{
								if (LikeOperator.LikeString(Strings.UCase(array[num10]), "INPUT-FILE*", CompareMethod.Binary))
								{
									continue;
								}
								array2 = Strings.Split(array[num10], "<\\\\>");
								text4 = Strings.Trim(Strings.UCase(array2[0]));
								if (LikeOperator.LikeString(text4, "COLUMN-RULE*", CompareMethod.Binary))
								{
									text4 = "COLUMN-RULE";
								}
								switch (text4)
								{
								case "PREPROCESS":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuPreProc.Checked = true;
									}
									break;
								case "TOOLBAROPTS":
								{
									string MyOpts = Strings.UCase(Strings.Trim(array2[1]));
									SetGridOpts(ref MyOpts, "S");
									break;
								}
								case "THEME":
									mnutheme.Text = Strings.Trim(array2[1]);
									break;
								case "COLSPAN":
									CmbColSpan.Text = Strings.Trim(array2[1]);
									break;
								case "PAGEABLE1":
									if (Operators.CompareString(Strings.UCase(Strings.Trim(array2[1])), "Y", TextCompare: false) == 0)
									{
										mnuPage.Checked = true;
									}
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
									break;
								}
								case "AT-TOP-OF-COLI":
									cmbTopOf.Text = array2[1];
									TxtTopof.Text = array2[2];
									break;
								case "AT-TOP-OF-REPORTI":
									num5++;
									GridRptHdr.Rows[num5].Cells[0].Value = array2[2];
									break;
								case "COLUMN-DATA":
								{
									int num25 = Information.UBound(array2);
									for (num11 = 2; num11 <= num25; num11++)
									{
										text = Strings.Trim(array2[num11]);
										if (Operators.CompareString(text, "", TextCompare: false) == 0)
										{
											break;
										}
										DataGridViewComboBoxColumn MyCombo = ColCol;
										BuildReport.Verify_Column(ref MyCombo, text);
										ColCol = MyCombo;
										GridCols.Rows[num11 - 2].Cells[0].Value = text;
										num14++;
									}
									if (num14 >= ColCol.Items.Count - 5)
									{
										GridCols.RowCount = num14;
									}
									else
									{
										GridCols.RowCount = ColCol.Items.Count - 5;
									}
									break;
								}
								case "COLUMN-HEADERS":
								case "COLUMN-ALIGNMENT":
								case "COLHDR-ALIGNMENT":
								case "COLUMN-DATATYPE":
								case "COLUMN-WIDTH":
								case "COLUMN-SORT":
								case "COLUMN-FILTER":
								case "COLUMN-FORMAT2":
								case "COLUMN-FORMAT":
								case "COLUMN-HIDE":
								case "COLUMN-SUMMARY":
								case "COLUMN-WHERE-OPR":
								case "COLUMN-WHERE-VAL":
								case "COLUMN-EDIT-TYPE":
								case "COLUMN-EDIT-VAL":
								{
									num8 = -1;
									switch (text4)
									{
									case "COLUMN-HEADERS":
										num8 = 1;
										break;
									case "COLUMN-ALIGNMENT":
										num8 = 2;
										break;
									case "COLHDR-ALIGNMENT":
										num8 = 3;
										break;
									case "COLUMN-DATATYPE":
										num8 = 4;
										break;
									case "COLUMN-WIDTH":
										num8 = 5;
										break;
									case "COLUMN-FORMAT2":
										num8 = 6;
										break;
									case "COLUMN-SORT":
										num8 = 7;
										break;
									case "COLUMN-FILTER":
										num8 = 8;
										break;
									case "COLUMN-HIDE":
										num8 = 9;
										break;
									case "COLUMN-SUMMARY":
										num8 = 15 + num13;
										num13++;
										break;
									case "COLUMN-FORMAT":
										num8 = 10;
										break;
									case "COLUMN-WHERE-OPR":
										num8 = 11;
										break;
									case "COLUMN-WHERE-VAL":
										num8 = 12;
										break;
									case "COLUMN-EDIT-TYPE":
										num8 = 13;
										break;
									case "COLUMN-EDIT-VAL":
										num8 = 14;
										break;
									}
									int num24 = Information.UBound(array2);
									for (num11 = 2; num11 <= num24; num11++)
									{
										text = Strings.Trim(array2[num11]);
										if (Operators.CompareString(text, "", TextCompare: false) == 0)
										{
											continue;
										}
										if (Operators.CompareString(text4, "COLUMN-SUMMARY", TextCompare: false) == 0 && (Operators.CompareString(Strings.Mid(Strings.UCase(text) + "     ", 1, 5), "EXPR:", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.UCase(text) + "     ", 1, 5), "TEXT:", TextCompare: false) == 0))
										{
											GridCols.Rows[num11 - 2].Cells[num8].Value = Strings.Mid(text + "     ", 1, 5);
											if (Strings.Len(text) > 5)
											{
												GridCols.Rows[num11 - 2].Cells[num8].Tag = Strings.Mid(text, 6);
											}
											else
											{
												GridCols.Rows[num11 - 2].Cells[num8].Tag = "";
											}
										}
										else
										{
											GridCols.Rows[num11 - 2].Cells[num8].Value = text;
										}
									}
									break;
								}
								case "COLUMN-RULE":
								{
									num7++;
									text = Strings.Trim(array2[1]);
									GridColRule.Rows[num7].Cells[0].Value = text;
									text = Strings.Trim(array2[2]);
									GridColRule.Rows[num7].Cells[1].Value = text;
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
									GridColRule.Rows[num7].Cells[2].Value = text;
									GridColRule.Rows[num7].Cells[3].Value = array2[4];
									num15 = 5;
									ref Globals_Renamed.Report_Format_Type[] cSSObj = ref CSSObj;
									int num23 = f_MaxObj;
									GridFormat = GridColRule;
									BuildChart.Load_CSS_Grid(ref array2, ref cSSObj, num23, ref GridFormat, num7, num15);
									GridColRule = GridFormat;
									break;
								}
								case "CC-ENABLED":
								case "CC-ENABLED2":
								{
									CC_Enabled = true;
									if (Operators.CompareString(text4, "CC-ENABLED", TextCompare: false) == 0)
									{
										Set_CC_Enabled_ver("1");
									}
									else
									{
										Set_CC_Enabled_ver("2");
									}
									GrpCC.Enabled = CC_Enabled;
									if (LikeOperator.LikeString(array2[1], "*<<<*>>>*", CompareMethod.Binary))
									{
										CC_GuidTxtBx.Text = array2[1];
									}
									else
									{
										CC_RepGUID = new Guid(array2[1]);
										CC_GuidTxtBx.Text = CC_RepGUID.ToString("B");
									}
									CC_ServiceURL = array2[2];
									CC_CustomData = array2[3];
									CC_Keys = array2[4];
									if (Operators.CompareString(text4, "CC-ENABLED", TextCompare: false) == 0)
									{
										CC_HeaderName = array2[5];
										CC_HeaderWidth = array2[6];
									}
									else
									{
										CC_HeaderName = "";
										CC_HeaderWidth = "";
									}
									CmbCCURL.Text = CC_ServiceURL;
									cmbCCData.Text = CC_CustomData;
									CC_HeaderTxtBx.Text = CC_HeaderName;
									CC_WidthMTxtBx.Text = CC_HeaderWidth;
									mnuCC.Checked = true;
									string[] array3 = CC_Keys.Split(',');
									char[] trimChars = new char[1] { '\'' };
									string[] array4 = array3;
									for (int i = 0; i < array4.Length; i++)
									{
										string text6 = array4[i];
										text6 = text6.Trim(trimChars);
										CC_KeysColsListView.Items.Add(text6, text6, "");
									}
									array3 = null;
									trimChars = null;
									break;
								}
								}
							}
							GridFormat = GridColRule;
							BuildChart.Format_Grid(ref GridFormat, 5);
							GridColRule = GridFormat;
							result = 0;
							lCSVCols = null;
							array2 = null;
						}
						else
						{
							Interaction.MsgBox("No CSV Files were found in the report specification file. Please try another file", MsgBoxStyle.Exclamation, "Missing CSV File");
							GridCols.RowCount = 0;
						}
						goto end_IL_0001;
					}
					case 4713:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Clear_All_Grids();
							GridCols.RowCount = 0;
							result = 1;
							goto end_IL_0001;
						}
						break;
					}
					goto IL_129f;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4713;
				continue;
			}
			break;
			IL_129f:
			throw ProjectData.CreateProjectError(-2146828237);
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
					bool flag;
					string text12;
					string text13;
					string text14;
					string[] array;
					string[] array2;
					string[] array3;
					string[] array4;
					int num15;
					string text15;
					ArrayList arrayList;
					List<string> list;
					string text17;
					string MyOpts;
					int num17;
					int num10;
					int num19;
					string text7;
					string text8;
					int num20;
					int NoCols;
					int num21;
					int num22;
					string text2;
					string text4;
					string text6;
					int num4;
					string text5;
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
						int num3 = 0;
						num4 = 0;
						int num5 = 0;
						int num6 = 0;
						int num7 = 0;
						string text = "";
						int num8 = -1;
						text2 = "";
						string text3 = "";
						text4 = "";
						text5 = "";
						text6 = "";
						text7 = "";
						text8 = "";
						string text9 = "";
						int num9 = 0;
						num10 = 0;
						string text10 = "";
						int num11 = 0;
						int num12 = 0;
						flag = false;
						int num13 = 0;
						int num14 = 0;
						string text11 = "Report";
						text12 = "";
						text13 = "SQLPathFinder.htm";
						text14 = Strings.Trim(CmbColSpan.Text);
						array = new string[3];
						array2 = new string[3];
						array3 = new string[3] { "700", "350", "200" };
						array4 = new string[3] { "400", "250", "100" };
						num15 = 25;
						text15 = "";
						arrayList = new ArrayList();
						list = new List<string>();
						string text16 = "";
						text17 = "";
						if (mnuPreProc.Checked)
						{
							text17 = "Y";
						}
						MyOpts = "";
						SetGridOpts(ref MyOpts, "G");
						text2 = "";
						text3 = "";
						if (!Check_Valid_Files(ref NoCols))
						{
							goto end_IL_0001;
						}
						if (Operators.CompareString(cmbTopOf.Text, "", TextCompare: false) == 0)
						{
							goto IL_023a;
						}
						text12 = Get_Candidate_Drilldown_column();
						if (Operators.CompareString(text12, "", TextCompare: false) == 0)
						{
							Interaction.MsgBox("You have requested use of a drill down Sort column but no candidate sort column exists. The drill down sort column must exist in all report tables and be the first column sorted in each table. You must either define such a column or remove the drill down sort column request.", MsgBoxStyle.Exclamation, "Invalid Drill Down Sort Column");
							goto end_IL_0001;
						}
						if (Operators.CompareString(cmbTopOf.Text, text12, TextCompare: false) == 0)
						{
							goto IL_023a;
						}
						Interaction.MsgBox("Note that your drill down sort column (" + cmbTopOf.Text + ") is different than the candidate drill down sort column (" + text12 + "). The drill down sort column must exist in all report tables and be the first column sorted in each table. Please correct", MsgBoxStyle.Exclamation, "Invalid Drill Down Sort Column");
						goto end_IL_0001_2;
					}
					case 10213:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								result = 1;
								goto end_IL_0001_2;
							}
							break;
						}
						IL_023a:
						text2 = "";
						if (fMenuItems > 0)
						{
							int num16 = fMenuItems;
							for (int num3 = 1; num3 <= num16; num3++)
							{
								if ((mnuFilterCharts.DropDownItems[num3 - 1] as ToolStripMenuItem).Checked)
								{
									text2 = Conversions.ToString(Operators.ConcatenateObject(text2 + ",", mnuFilterCharts.DropDownItems[num3 - 1].Tag));
									flag = true;
								}
							}
						}
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							text2 = Strings.Mid(text2, 2);
						}
						GridCSV.Rows[0].Cells[9].Value = text2;
						text2 = "";
						if (Operators.CompareString(text12, "", TextCompare: false) != 0 && flag)
						{
							Interaction.MsgBox("Sorry, but at this time you cannot design a report that Filters Charts and uses Drilldown Sort columns. Please remove one of these options.", MsgBoxStyle.Exclamation, "Invalid Options");
							goto end_IL_0001;
						}
						text4 = "N";
						if (!flag)
						{
							goto IL_0469;
						}
						num17 = GridCols.RowCount - 1;
						for (num4 = 0; num4 <= num17; num4++)
						{
							object[] array5;
							DataGridViewCell dataGridViewCell;
							bool[] array6;
							object obj = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[4]).Value }, null, null, array6 = new bool[1] { true });
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							text2 = Strings.Trim(Conversions.ToString(obj));
							if (Operators.CompareString(text2, "STRING", TextCompare: false) != 0 && Operators.CompareString(text2, "DATE", TextCompare: false) != 0)
							{
								text4 = "Y";
								break;
							}
						}
						if (Operators.CompareString(text4, "N", TextCompare: false) != 0)
						{
							goto IL_0469;
						}
						num10 = unchecked((int)Interaction.MsgBox("Warning: You have assigned Filter Charts to this report but not set any numeric column datatypes. Note that if you treat a NUMERIC column as a string in a Chart you could get incorrect results. Continue saving report?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Chart Filter Datatype Check"));
						if (num10 != 7)
						{
							goto IL_0469;
						}
						goto end_IL_0001_2;
						IL_0469:
						if (CC_Enabled)
						{
							if (string.IsNullOrEmpty(CmbCCURL.Text))
							{
								Interaction.MsgBox("Comments capture is enabled. Please provide a Valid Service URL that will handle Comment Data Entry", MsgBoxStyle.Exclamation, "Comments Capture");
								goto end_IL_0001;
							}
							if (CC_KeysColsListView.Items.Count == 0)
							{
								Interaction.MsgBox("You have indicated that you want to capture comments but have not identified any columns that uniquely identify a row in the report for comment lookup", MsgBoxStyle.Exclamation, "Invalid Key Columns for Comments Capture");
								goto end_IL_0001;
							}
							text4 = "";
							text4 = ValidateKeyCols();
							if (Operators.CompareString(text4, "", TextCompare: false) != 0)
							{
								Interaction.MsgBox(text4 + ". Please correct", MsgBoxStyle.Exclamation, "Invalid Key Columns for Comments Capture");
								goto end_IL_0001;
							}
							if (Operators.CompareString(cc_enabled_ver, "2", TextCompare: false) == 0)
							{
								text4 = "NOTOK";
								text5 = "";
								int num18 = GridCols.RowCount - 1;
								for (num4 = 0; num4 <= num18; num4++)
								{
									object[] array5;
									DataGridViewCell dataGridViewCell;
									bool[] array6;
									object obj2 = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[0]).Value }, null, null, array6 = new bool[1] { true });
									if (array6[0])
									{
										dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
									}
									text2 = Strings.Trim(Conversions.ToString(obj2));
									if (Operators.CompareString(Strings.Mid(text2 + "               ", 1, Strings.Len("1:spf$comment$-")), "1:spf$comment$-", TextCompare: false) == 0)
									{
										text4 = "OK";
										Type typeFromHandle = typeof(Strings);
										object[] obj3 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[13]).Value };
										array5 = obj3;
										bool[] obj4 = new bool[1] { true };
										array6 = obj4;
										object obj5 = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj3, null, null, obj4);
										if (array6[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
										}
										text6 = Strings.Trim(Conversions.ToString(obj5));
										object obj6 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[4]).Value }, null, null, array6 = new bool[1] { true });
										if (array6[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
										}
										text7 = Strings.Trim(Conversions.ToString(obj6));
										object obj7 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[14]).Value }, null, null, array6 = new bool[1] { true });
										if (array6[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
										}
										text8 = Strings.Trim(Conversions.ToString(obj7));
										if (Operators.CompareString(text15, "", TextCompare: false) != 0)
										{
											text15 += ",";
										}
										text15 = text15 + "'" + Strings.Mid(text2, 3) + "(" + Strings.LCase(text7) + ")'";
										if (string.IsNullOrEmpty(text6) || Operators.CompareString(text6, "", TextCompare: false) == 0)
										{
											text5 = "For Comment field: " + text2 + " in row " + Conversion.Str(num4 + 1) + ", no Edit Type was specfied.";
											break;
										}
										if (Operators.CompareString(Strings.UCase(text6), "DATETIMEINPUT", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text7), "DATE", TextCompare: false) != 0)
										{
											text5 = "For Comment field: " + text2 + " in row " + Conversion.Str(num4 + 1) + ", you can only specify a DATETIMEINPUT Edit Type for fields of datatype: DATE.";
											break;
										}
									}
								}
								if (Operators.CompareString(text4, "NOTOK", TextCompare: false) == 0)
								{
									Interaction.MsgBox("You have configured comments capture but not specified any comments columns. Comments columns begin with: \"1:spf$comment$-\", and are selected from the column drop-down. Please correct", MsgBoxStyle.Exclamation, "Missing Comments Columns");
									goto end_IL_0001;
								}
								if (Operators.CompareString(text5, "", TextCompare: false) != 0)
								{
									Interaction.MsgBox(text5, MsgBoxStyle.Exclamation, "Invalid Option");
									goto end_IL_0001;
								}
								goto IL_0a6a;
							}
							goto IL_0a6a;
						}
						text4 = "OK";
						num19 = GridCols.RowCount - 1;
						for (num4 = 0; num4 <= num19; num4++)
						{
							object[] array5;
							DataGridViewCell dataGridViewCell;
							bool[] array6;
							object obj8 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[0]).Value }, null, null, array6 = new bool[1] { true });
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							text2 = Strings.Trim(Conversions.ToString(obj8));
							if (Operators.CompareString(Strings.Mid(Strings.LCase(text2) + "               ", 1, Strings.Len("1:spf$comment$-")), "1:spf$comment$-", TextCompare: false) == 0)
							{
								text4 = "NOTOK";
								break;
							}
						}
						if (Operators.CompareString(text4, "NOTOK", TextCompare: false) != 0)
						{
							goto IL_0a6a;
						}
						Interaction.MsgBox("You have added a \"1:spf$comment$-\" edit column in row " + Conversions.ToString(num4 + 1) + " but have not enabled a Comments Capture Report. Please correct", MsgBoxStyle.Exclamation, "Invalid Option");
						goto end_IL_0001_2;
						IL_0a6a:
						text4 = "";
						text5 = "";
						text6 = "";
						text7 = "";
						text8 = "";
						text6 = "";
						num20 = GridCols.RowCount - 1;
						for (num4 = 0; num4 <= num20; num4++)
						{
							object[] array5;
							DataGridViewCell dataGridViewCell;
							bool[] array6;
							object obj9 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[11]).Value }, null, null, array6 = new bool[1] { true });
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							string text3 = Strings.UCase(Strings.Trim(Conversions.ToString(obj9)));
							if (Operators.CompareString(text3, "", TextCompare: false) == 0 || Operators.CompareString(text3, "\0", TextCompare: false) == 0)
							{
								continue;
							}
							Type typeFromHandle2 = typeof(Strings);
							object[] obj10 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[0]).Value };
							array5 = obj10;
							bool[] obj11 = new bool[1] { true };
							array6 = obj11;
							object obj12 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj10, null, null, obj11);
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							text2 = Strings.Trim(Conversions.ToString(obj12));
							object obj13 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[12]).Value }, null, null, array6 = new bool[1] { true });
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							text4 = Strings.Trim(Conversions.ToString(obj13));
							object obj14 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[4]).Value }, null, null, array6 = new bool[1] { true });
							if (array6[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
							}
							text5 = Strings.Trim(Conversions.ToString(obj14));
							if (Operators.CompareString(text12, "", TextCompare: false) != 0)
							{
								text6 = "Sorry, but at this time, you cannot apply a WHERE clause to a report using a Drilldown column.";
								break;
							}
							switch (text2)
							{
							default:
								if (Operators.CompareString(text2, "STARTS/ENDS WITH (%):", TextCompare: false) == 0)
								{
									goto case "STARTS WITH:";
								}
								if (Operators.CompareString(text5, "STRING", TextCompare: false) == 0)
								{
									switch (text3)
									{
									case ">":
									case ">=":
									case "<":
									case "<=":
									case "BETWEEN":
										goto IL_0e08;
									}
								}
								if ((Operators.CompareString(text5, "NUMBER", TextCompare: false) != 0 && Operators.CompareString(text5, "FLOAT", TextCompare: false) != 0) || (Operators.CompareString(text3, "CONTAINS", TextCompare: false) != 0 && Operators.CompareString(text3, "NOT CONTAINS", TextCompare: false) != 0 && Operators.CompareString(text3, "STARTS WITH", TextCompare: false) != 0))
								{
									if (Operators.CompareString(text5, "DATE", TextCompare: false) == 0)
									{
										text6 = "Sorry, but at this time, you cannot apply a WHERE operator to a DATE datatype.";
										break;
									}
									if (Operators.CompareString(Strings.Mid(text2, 1, 2), "1:", TextCompare: false) == 0)
									{
										continue;
									}
									text6 = "Sorry, but at this time, you cannot apply a WHERE clause to a nested report.";
									break;
								}
								goto IL_0e08;
							case "STARTS WITH:":
							case "ENDS WITH:":
							case "CONTAINS:":
								{
									text6 = "Sorry, but at this time, you cannot apply a WHERE clause to a Column Pattern.";
									break;
								}
								IL_0e08:
								text6 = "You cannot use WHERE operator: " + text3 + " with a column of datatype: " + text5 + ".";
								break;
							}
							break;
						}
						if (Operators.CompareString(text6, "", TextCompare: false) != 0)
						{
							Interaction.MsgBox(text6 + " (Row " + Conversions.ToString(num4 + 1) + ")", MsgBoxStyle.Exclamation, "Invalid Option");
							goto end_IL_0001;
						}
						if (NoCols < f_MinCols)
						{
							NoCols = f_MinCols;
						}
						NoCols += 2;
						MyRptSpec = "Type<\\\\>Key";
						num21 = NoCols - 2;
						for (int num3 = 1; num3 <= num21; num3++)
						{
							MyRptSpec = MyRptSpec + "<\\\\>COL" + Strings.Trim(Conversions.ToString(num3));
						}
						MyRptSpec = MyRptSpec + "\r\nTYPE<\\\\>HTMLI5" + BuildReport.Add_Delimiter(NoCols, 2);
						num22 = GridCols.RowCount - 1;
						num4 = 0;
						while (true)
						{
							int num3;
							if (num4 <= num22)
							{
								DataGridViewCell dataGridViewCell;
								object[] array5;
								bool[] array6;
								object obj15 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[0]).Value }, null, null, array6 = new bool[1] { true });
								if (array6[0])
								{
									dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
								}
								text2 = Strings.Trim(Conversions.ToString(obj15));
								switch (text2)
								{
								default:
									if (Operators.CompareString(text2, "STARTS/ENDS WITH (%):", TextCompare: false) != 0)
									{
										goto IL_12b5;
									}
									break;
								case "STARTS WITH:":
								case "ENDS WITH:":
								case "CONTAINS:":
									break;
								}
								object obj16 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[1]).Value }, null, null, array6 = new bool[1] { true });
								if (array6[0])
								{
									dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
								}
								string text3 = Strings.Trim(Conversions.ToString(obj16));
								int num11 = Strings.InStr(text3, ":");
								if (num11 > 1)
								{
									text4 = Strings.Mid(text3, 1, num11 - 1);
								}
								if (num11 <= 1 || (Operators.CompareString(text4, "1", TextCompare: false) != 0 && Operators.CompareString(text4, "2", TextCompare: false) != 0 && Operators.CompareString(text4, "3", TextCompare: false) != 0))
								{
									Interaction.MsgBox("Issue with row " + Conversions.ToString(num4 + 1) + " of the Columns Grid. When using Column Patterns, you must prefix the actual column pattern with a valid file number. E.g., 1:Lot.", MsgBoxStyle.Exclamation, "Missing File ID Prefix");
									break;
								}
								int num23 = GridCSV.RowCount - 1;
								for (num3 = 0; num3 <= num23; num3++)
								{
									if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[0].Value)), "", TextCompare: false) != 0 && Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[1].Value)), text4, TextCompare: false) == 0)
									{
										Type typeFromHandle3 = typeof(Strings);
										object[] obj17 = new object[1] { (dataGridViewCell = GridCSV.Rows[num3].Cells[4]).Value };
										array5 = obj17;
										bool[] obj18 = new bool[1] { true };
										array6 = obj18;
										object obj19 = NewLateBinding.LateGet(null, typeFromHandle3, "UCase", obj17, null, null, obj18);
										if (array6[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
										}
										if (Operators.CompareString(Strings.Trim(Conversions.ToString(obj19)), "AUTO", TextCompare: false) == 0 || Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[4].Value)), "", TextCompare: false) == 0)
										{
											Interaction.MsgBox("When using Column Patterns in the Columns Grid, you must specify exact Report Width on the Main tab and not use \"Auto\" nor leave Blank.", MsgBoxStyle.Exclamation, "Cannot Use Auto for Grid Width");
											goto end_IL_12bc;
										}
									}
								}
								goto IL_12b5;
							}
							int num8 = -1;
							int num24 = GridCSV.RowCount - 1;
							num3 = 0;
							while (true)
							{
								string text3;
								int num7;
								if (num3 <= num24)
								{
									text2 = Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[0].Value));
									if (Operators.CompareString(text2, "", TextCompare: false) != 0)
									{
										if ((Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[2].Value)), "", TextCompare: false) != 0 || Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[3].Value)), "", TextCompare: false) != 0) && Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3 + 1].Cells[0].Value)), "", TextCompare: false) == 0)
										{
											Interaction.MsgBox("You have specified a Join Column in row " + Conversions.ToString(num3 + 1) + " of the Input CSV Grid  but not specified a Nested report file in row " + Conversions.ToString(num3 + 2) + " of the grid. Either remove the join column or add a nested report file.", MsgBoxStyle.Exclamation, "Extra Join Column(s)");
											break;
										}
										if (num3 > 0 && Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3 - 1].Cells[2].Value)), "", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3 - 1].Cells[3].Value)), "", TextCompare: false) == 0)
										{
											Interaction.MsgBox("You have specified a nested report in row " + Conversions.ToString(num3 + 1) + " of the Input CSV Grid but not specified a Join Column in row " + Conversions.ToString(num3) + ". Please correct.", MsgBoxStyle.Exclamation, "Missing Join Column(s)");
											break;
										}
										if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[2].Value)), "", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[3].Value)), "", TextCompare: false) != 0)
										{
											Interaction.MsgBox("In row " + Conversions.ToString(num3 + 1) + " of the Input CSV Grid, you have specified Join Col2 but not Join Col1. If there is only one Join Column, add it to Join Col1.", MsgBoxStyle.Exclamation, "Missing Join Col1");
											break;
										}
										num8++;
										MyRptSpec = MyRptSpec + "\r\nINPUT-FILE<\\\\>" + text2;
										num7 = 1;
										int num25 = GridCSV.ColumnCount - 1;
										for (num4 = 1; num4 <= num25; num4++)
										{
											num7++;
											text2 = Strings.Trim(Conversions.ToString(GridCSV.Rows[num3].Cells[num4].Value));
											if (num4 == 1)
											{
												string text = text2;
												int num12 = 0;
												int num26 = GridCols.ColumnCount - 1;
												for (int num5 = 15; num5 <= num26; num5++)
												{
													int num27 = GridCols.RowCount - 1;
													for (int num6 = 0; num6 <= num27; num6++)
													{
														if (Operators.CompareString(Strings.Mid(Conversions.ToString(GridCols.Rows[num6].Cells[0].Value), 1, Strings.Len(text + ":")), text + ":", TextCompare: false) == 0 && (Operators.ConditionalCompareObjectEqual(GridCols.Rows[num6].Cells[9].Value, "0", TextCompare: false) || GridCols.Rows[num6].Cells[9].Value == null) && Operators.ConditionalCompareObjectNotEqual(GridCols.Rows[num6].Cells[num5].Value, "", TextCompare: false) && GridCols.Rows[num6].Cells[num5].Value != null)
														{
															num12++;
															break;
														}
													}
												}
												array2[num8] = Conversions.ToString(num12 * 30);
												num12 = 0;
												int num28 = GridCols.RowCount - 1;
												for (int num5 = 0; num5 <= num28; num5++)
												{
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(GridCols.Rows[num5].Cells[0].Value), 1, Strings.Len(text + ":")), text + ":", TextCompare: false) == 0 && (Operators.ConditionalCompareObjectEqual(GridCols.Rows[num5].Cells[9].Value, "0", TextCompare: false) || GridCols.Rows[num5].Cells[9].Value == null))
													{
														text3 = Strings.Trim(Conversions.ToString(GridCols.Rows[num5].Cells[5].Value));
														if (Operators.CompareString(text3, "", TextCompare: false) != 0 && text3 != null && Versioned.IsNumeric(text3))
														{
															num12 += Conversions.ToInteger(text3);
														}
													}
												}
												num12 = ((num12 <= 1900) ? (num12 + 20) : 1900);
												array[num8] = Conversions.ToString(num12);
											}
											else if (unchecked(num4 >= 4 && num4 <= 8) && Operators.CompareString(Strings.UCase(text2), "AUTO", TextCompare: false) == 0)
											{
												text2 = "Auto:";
												switch (num4)
												{
												case 4:
													text2 += array[num8];
													break;
												case 5:
													text2 = ((num3 < 1) ? (text2 + array3[num8]) : "Auto");
													break;
												case 6:
													text2 += array2[num8];
													break;
												case 7:
													text2 += array4[num8];
													break;
												case 8:
													text2 += Conversions.ToString(num15);
													break;
												}
											}
											MyRptSpec = MyRptSpec + "<\\\\>" + text2;
										}
										MyRptSpec += BuildReport.Add_Delimiter(NoCols, num7);
									}
									num3++;
									continue;
								}
								num7 = 0;
								text2 = "";
								text3 = "";
								if (CC_Enabled)
								{
									if (CC_RepGUID == Guid.Empty)
									{
										CC_RepGUID = Guid.NewGuid();
									}
									string text16 = "";
									int num29 = GridCols.RowCount - 1;
									for (num3 = 0; num3 <= num29; num3++)
									{
										text16 = Conversions.ToString(GridCols.Rows[num3].Cells[0].Value);
										if (Operators.CompareString(text16, null, TextCompare: false) != 0 && text16.StartsWith("1:"))
										{
											text16 = text16.Remove(0, 2);
											list.Add(text16);
										}
									}
									IEnumerator enumerator = CC_KeysColsListView.Items.GetEnumerator();
									while (enumerator.MoveNext())
									{
										ListViewItem listViewItem = (ListViewItem)enumerator.Current;
										arrayList.Add("'" + listViewItem.Text + "'");
									}
									if (enumerator is IDisposable)
									{
										(enumerator as IDisposable).Dispose();
									}
									text3 = string.Join(",", (string[])arrayList.ToArray(typeof(string)));
									text4 = Strings.Trim(CmbCCURL.Text);
									int num11 = Strings.InStr(text4, "->");
									if (num11 > 0)
									{
										text4 = Strings.Trim(Strings.Mid(text4 + "   ", num11 + 2));
									}
									text2 = ((!LikeOperator.LikeString(CC_GuidTxtBx.Text, "*<<<*>>>*", CompareMethod.Binary)) ? (CC_RepGUID.ToString("B") + "<\\\\>" + text4 + "<\\\\>" + cmbCCData.Text + "<\\\\>" + text3) : (CC_GuidTxtBx.Text + "<\\\\>" + text4 + "<\\\\>" + cmbCCData.Text + "<\\\\>" + text3));
									if (Operators.CompareString(cc_enabled_ver, "1", TextCompare: false) == 0)
									{
										text2 = text2 + "<\\\\>" + CC_HeaderTxtBx.Text + "<\\\\>" + CC_WidthMTxtBx.Text;
										MyRptSpec = MyRptSpec + "\r\nCC-ENABLED<\\\\>" + text2;
									}
									else
									{
										text2 = (text2 + "<\\\\>" + text15 + "<\\\\>") ?? "";
										MyRptSpec = MyRptSpec + "\r\nCC-ENABLED2<\\\\>" + text2;
									}
								}
								MyRptSpec = MyRptSpec + "\r\nOUTPUT-FILE<\\\\>" + text13 + BuildReport.Add_Delimiter(NoCols, 2);
								MyRptSpec = MyRptSpec + "\r\nPREPROCESS<\\\\>" + text17 + BuildReport.Add_Delimiter(NoCols, 2);
								MyRptSpec = MyRptSpec + "\r\nTOOLBAROPTS<\\\\>" + MyOpts + BuildReport.Add_Delimiter(NoCols, 2);
								MyRptSpec = MyRptSpec + "\r\nTHEME<\\\\>" + Strings.Trim(mnutheme.Text) + BuildReport.Add_Delimiter(NoCols, 2);
								MyRptSpec = MyRptSpec + "\r\nCOLSPAN<\\\\>" + text14 + BuildReport.Add_Delimiter(NoCols, 2);
								text2 = "N";
								if (mnuPage.Checked)
								{
									text2 = "Y";
								}
								MyRptSpec = MyRptSpec + "\r\nPAGEABLE1<\\\\>" + text2 + BuildReport.Add_Delimiter(NoCols, 2);
								int num30 = GridSort.RowCount - 1;
								for (num3 = 0; num3 <= num30; num3++)
								{
									if (Operators.ConditionalCompareObjectNotEqual(GridSort.Rows[num3].Cells[0].Value, "", TextCompare: false) && Operators.CompareString(Strings.Mid(Conversions.ToString(GridSort.Rows[num3].Cells[0].Value), 1, 1), "=", TextCompare: false) != 0)
									{
										text2 = Conversions.ToString(GridSort.Rows[num3].Cells[0].Value);
										text3 = Conversions.ToString(GridSort.Rows[num3].Cells[1].Value);
										if (Operators.CompareString(text3, "", TextCompare: false) == 0)
										{
											text3 = "Asc";
										}
										MyRptSpec = MyRptSpec + "\r\nSORT<\\\\>" + text2 + "<\\\\>" + text3 + BuildReport.Add_Delimiter(NoCols, 3);
									}
								}
								int num31 = GridRptHdr.RowCount - 1;
								for (num3 = 0; num3 <= num31; num3++)
								{
									text2 = Conversions.ToString(GridRptHdr.Rows[num3].Cells[0].Value);
									if (Operators.CompareString(text2, "", TextCompare: false) != 0)
									{
										text2 = Make_Tokens_UC(text2);
										MyRptSpec = MyRptSpec + "\r\nAT-TOP-OF-REPORTI<\\\\><\\\\>" + text2 + BuildReport.Add_Delimiter(NoCols, 3);
									}
								}
								if (Operators.CompareString(cmbTopOf.Text, "", TextCompare: false) != 0)
								{
									MyRptSpec = MyRptSpec + "\r\nAT-TOP-OF-COLI<\\\\>" + text12 + "<\\\\>" + TxtTopof.Text + BuildReport.Add_Delimiter(NoCols, 2);
								}
								text2 = "";
								text3 = "";
								text4 = "N";
								int num32 = GridCols.RowCount - 1;
								for (num4 = 0; num4 <= num32; num4++)
								{
									DataGridViewCell dataGridViewCell;
									object[] array5;
									bool[] array6;
									object obj20 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array5 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[6]).Value }, null, null, array6 = new bool[1] { true });
									if (array6[0])
									{
										dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
									}
									text2 = Strings.Trim(Conversions.ToString(obj20));
									if (LikeOperator.LikeString(text2, "F*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "C*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P*", CompareMethod.Binary))
									{
										Type typeFromHandle4 = typeof(Strings);
										object[] obj21 = new object[1] { (dataGridViewCell = GridCols.Rows[num4].Cells[4]).Value };
										array5 = obj21;
										bool[] obj22 = new bool[1] { true };
										array6 = obj22;
										object obj23 = NewLateBinding.LateGet(null, typeFromHandle4, "UCase", obj21, null, null, obj22);
										if (array6[0])
										{
											dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array5[0]));
										}
										text3 = Strings.Trim(Conversions.ToString(obj23));
										if (Operators.CompareString(text3, "STRING", TextCompare: false) == 0 || Operators.CompareString(text3, "DATE", TextCompare: false) == 0)
										{
											Interaction.MsgBox("You have configured a numeric format but defined a string datatype for row " + Conversions.ToString(num4 + 1) + " in the Columns grid", MsgBoxStyle.Exclamation, "Incompatible Datatype");
											text4 = "Y";
											break;
										}
									}
								}
								if (Operators.CompareString(text4, "Y", TextCompare: false) == 0)
								{
									break;
								}
								text2 = "";
								text3 = "";
								text4 = "";
								int num9 = 0;
								int num33 = GridCols.ColumnCount - 1;
								for (num4 = 0; num4 <= num33; num4++)
								{
									text5 = "";
									int num34 = num4;
									if (num34 == 0)
									{
										text2 = "COLUMN-DATA";
									}
									else if (num34 == 1)
									{
										text2 = "COLUMN-HEADERS";
									}
									else if (num34 == 2)
									{
										text2 = "COLUMN-ALIGNMENT";
									}
									else if (num34 == 3)
									{
										text2 = "COLHDR-ALIGNMENT";
									}
									else if (num34 == 4)
									{
										text2 = "COLUMN-DATATYPE";
									}
									else if (num34 == 5)
									{
										text2 = "COLUMN-WIDTH";
									}
									else if (num34 == 6)
									{
										text2 = "COLUMN-FORMAT2";
									}
									else if (num34 == 7)
									{
										text2 = "COLUMN-SORT";
									}
									else if (num34 == 8)
									{
										text2 = "COLUMN-FILTER";
									}
									else if (num34 == 9)
									{
										text2 = "COLUMN-HIDE";
									}
									else if (num34 == 10)
									{
										text2 = "COLUMN-FORMAT";
									}
									else if (num34 == 11)
									{
										text2 = "COLUMN-WHERE-OPR";
									}
									else if (num34 == 12)
									{
										text2 = "COLUMN-WHERE-VAL";
									}
									else if (num34 == 13)
									{
										text2 = "COLUMN-EDIT-TYPE";
									}
									else if (num34 == 14)
									{
										text2 = "COLUMN-EDIT-VAL";
									}
									else if (num34 == 15 || num34 == GridCols.ColumnCount - 1)
									{
										text2 = "COLUMN-SUMMARY";
									}
									text5 = "\r\n" + text2 + "<\\\\>";
									int num35 = GridCols.RowCount - 1;
									for (num3 = 0; num3 <= num35; num3++)
									{
										text4 = Conversions.ToString(GridCols.Rows[num3].Cells[0].Value);
										if (Operators.CompareString(text4, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text4, 1, 1), "=", TextCompare: false) != 0)
										{
											text3 = Conversions.ToString(GridCols.Rows[num3].Cells[num4].Value);
											if (Operators.CompareString(text2, "COLUMN-SUMMARY", TextCompare: false) == 0 && (Operators.CompareString(Strings.UCase(text3), "EXPR:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text3), "TEXT:", TextCompare: false) == 0))
											{
												text3 = Conversions.ToString(Operators.ConcatenateObject(text3, GridCols.Rows[num3].Cells[num4].Tag));
											}
											text5 = text5 + "<\\\\>" + text3;
											num9++;
										}
									}
									num9 += 2;
									MyRptSpec = MyRptSpec + text5 + BuildReport.Add_Delimiter(NoCols, num9);
								}
								num9 = 0;
								int num36 = GridColRule.RowCount - 1;
								num3 = 0;
								while (true)
								{
									if (num3 <= num36)
									{
										text2 = Conversions.ToString(GridColRule.Rows[num3].Cells[0].Value);
										text3 = Conversions.ToString(GridColRule.Rows[num3].Cells[1].Value);
										text4 = Conversions.ToString(GridColRule.Rows[num3].Cells[2].Value);
										text5 = Conversions.ToString(GridColRule.Rows[num3].Cells[3].Value);
										if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(text4, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text2, 1, 1), "=", TextCompare: false) != 0)
										{
											if (!Chk_Col_Rule(text2, text3))
											{
												Interaction.MsgBox("Column patterns for a column rule must be identical if used in both the display and the test column. (Display Pattern=" + text2 + ", and Test Pattern=" + text3 + ")", MsgBoxStyle.Exclamation, "Invalid Column Patterns Used in a Column Rule");
												break;
											}
											num9++;
											MyRptSpec = MyRptSpec + "\r\nCOLUMN-RULE" + Conversions.ToString(num9) + "<\\\\>" + text2 + "<\\\\>" + text3 + "<\\\\>" + text4 + "<\\\\>" + text5;
											text5 = "";
											text6 = "";
											num7 = 0;
											int num37 = GridColRule.ColumnCount - 1;
											for (num4 = 4; num4 <= num37; num4++)
											{
												text5 = Conversions.ToString(GridColRule.Rows[num3].Cells[num4].Value);
												if (Operators.CompareString(text5, "", TextCompare: false) != 0)
												{
													text6 = text6 + "<\\\\>" + BuildReport.Get_CSS_Attr(GridColRule.Columns[num4].HeaderText, f_MaxObj, ref CSSObj) + ":" + text5;
													num7++;
												}
											}
											MyRptSpec = MyRptSpec + text6 + BuildReport.Add_Delimiter(NoCols, 5 + num7);
										}
										num3++;
										continue;
									}
									result = 0;
									break;
								}
								break;
							}
							break;
							IL_12b5:
							num4++;
							continue;
							end_IL_12bc:
							break;
						}
						goto end_IL_0001_2;
					}
					goto IL_281b;
				}
				end_IL_0001_2:;
			}
			catch (object obj24) when (obj24 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj24);
				try0001_dispatch = 10213;
				continue;
			}
			break;
			IL_281b:
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

	public void Load_Grid_Format_Values(string MyKey, ref DataGridViewComboBoxColumn Cmb1)
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
							goto IL_000f;
						case 4:
							goto IL_001e;
						case 5:
							goto IL_002d;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_004f;
						case 8:
							goto IL_0092;
						case 9:
						case 10:
							goto IL_00b4;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_004f:
					num2 = 7;
					if ((Operators.CompareString(CSSObj[num5].ObjType, "values", TextCompare: false) == 0) & (Operators.CompareString(CSSObj[num5].Name, MyKey, TextCompare: false) == 0))
					{
						goto IL_0092;
					}
					goto IL_00b4;
					IL_0092:
					num2 = 8;
					Cmb1.Items.Add(CSSObj[num5].Value);
					goto IL_00b4;
					IL_00bb:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_004f;
					IL_00b4:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_00bb;
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
					Cmb1.Items.Add("");
					goto IL_0041;
					IL_0041:
					num2 = 6;
					num6 = f_MaxObj;
					num5 = 1;
					goto IL_00bb;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 257;
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
								goto IL_007a;
							case 10:
								goto IL_0080;
							case 11:
								goto IL_0092;
							case 12:
								goto IL_00a0;
							case 13:
								goto IL_00c7;
							default:
								goto end_IL_0001;
							case 14:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_00d0:
						if (num5 > num6)
						{
							goto end_IL_0001_2;
						}
						goto IL_00a0;
						IL_00a0:
						num2 = 12;
						GridColRule.Columns[num5].Width = (int)Math.Round((double)num7 / (double)num8);
						goto IL_00c7;
						IL_0092:
						num2 = 11;
						num6 = num8 - 1;
						num5 = 0;
						goto IL_00d0;
						IL_00c7:
						num2 = 13;
						num5++;
						goto IL_00d0;
						IL_000b:
						num2 = 2;
						Refresh_Combo();
						goto IL_0014;
						IL_0014:
						num2 = 3;
						num7 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num5 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						num8 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						num9 = 70;
						goto IL_0028;
						IL_0028:
						num2 = 7;
						GridSort.Columns[0].Width = GridSort.Width - 180;
						goto IL_0052;
						IL_0052:
						num2 = 8;
						GridRptHdr.Columns[num5].Width = GridRptHdr.Width - 75;
						goto IL_007a;
						IL_007a:
						num2 = 9;
						num8 = 6;
						goto IL_0080;
						IL_0080:
						num2 = 10;
						num7 = GridColRule.Width - num9;
						goto IL_0092;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 291;
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
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				DataGridView MyGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 476:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0045;
						case 6:
							goto IL_0060;
						case 7:
							goto IL_007b;
						case 8:
							goto IL_0096;
						case 9:
							goto IL_00a9;
						case 10:
							goto IL_00c2;
						case 11:
							goto IL_00d6;
						case 12:
							goto IL_00ef;
						case 13:
							goto IL_0108;
						case 14:
							goto IL_0121;
						case 15:
							goto IL_013a;
						case 16:
							goto IL_0153;
						case 17:
							goto IL_015d;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_013a:
					num2 = 15;
					ColCol.Items.Add("Starts/Ends With (%):");
					goto IL_0153;
					IL_0153:
					num2 = 16;
					Initialize_Formats();
					goto IL_015d;
					IL_0121:
					num2 = 14;
					ColCol.Items.Add("Contains:");
					goto IL_013a;
					IL_015d:
					num2 = 17;
					GridCols.RowCount = 0;
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyGrid = GridRptHdr;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridRptHdr = MyGrid;
					goto IL_002a;
					IL_002a:
					num2 = 4;
					MyGrid = GridSort;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridSort = MyGrid;
					goto IL_0045;
					IL_0045:
					num2 = 5;
					MyGrid = GridCSV;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridCSV = MyGrid;
					goto IL_0060;
					IL_0060:
					num2 = 6;
					MyGrid = GridCols;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridCols = MyGrid;
					goto IL_007b;
					IL_007b:
					num2 = 7;
					MyGrid = GridColRule;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridColRule = MyGrid;
					goto IL_0096;
					IL_0096:
					num2 = 8;
					ColName.Items.Clear();
					goto IL_00a9;
					IL_00a9:
					num2 = 9;
					ColName.Items.Add("");
					goto IL_00c2;
					IL_00c2:
					num2 = 10;
					ColCol.Items.Clear();
					goto IL_00d6;
					IL_00d6:
					num2 = 11;
					ColCol.Items.Add("");
					goto IL_00ef;
					IL_00ef:
					num2 = 12;
					ColCol.Items.Add("Starts With:");
					goto IL_0108;
					IL_0108:
					num2 = 13;
					ColCol.Items.Add("Ends With:");
					goto IL_0121;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				fTitle = "untitled.vgs";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 476;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Clear_Grids(string MyRowS)
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
					DataGridView MyGrid;
					int num6;
					int num7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 515:
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
								goto IL_002f;
							case 5:
								goto IL_004e;
							case 6:
								goto IL_006e;
							case 7:
								goto IL_008e;
							case 8:
								goto IL_00a9;
							case 9:
								goto IL_00f9;
							case 10:
							case 11:
								goto IL_0110;
							case 12:
								goto IL_011b;
							case 13:
								goto IL_0137;
							case 14:
								goto IL_0188;
							case 15:
							case 16:
								goto IL_019f;
							default:
								goto end_IL_0001;
							case 17:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_019f:
						num2 = 16;
						num5 += -1;
						goto IL_01a6;
						IL_00a9:
						num2 = 8;
						if (Strings.Len(RuntimeHelpers.GetObjectValue(ColName.Items[num5])) >= 1 && Operators.CompareString(Strings.Mid(Conversions.ToString(ColName.Items[num5]), 1, 1), MyRowS, TextCompare: false) == 0)
						{
							goto IL_00f9;
						}
						goto IL_0110;
						IL_0188:
						num2 = 14;
						ColCol.Items.RemoveAt(num5);
						goto IL_019f;
						IL_0110:
						num2 = 11;
						num5 += -1;
						goto IL_0117;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						MyGrid = GridSort;
						Clear_A_GridR(ref MyGrid, MyRowS, 0, 15);
						GridSort = MyGrid;
						goto IL_002f;
						IL_002f:
						num2 = 4;
						MyGrid = GridCols;
						Clear_A_GridR(ref MyGrid, MyRowS, 0, -1);
						GridCols = MyGrid;
						goto IL_004e;
						IL_004e:
						num2 = 5;
						MyGrid = GridColRule;
						Clear_A_GridR(ref MyGrid, MyRowS, 0, 100);
						GridColRule = MyGrid;
						goto IL_006e;
						IL_006e:
						num2 = 6;
						MyGrid = GridColRule;
						Clear_A_GridR(ref MyGrid, MyRowS, 1, 100);
						GridColRule = MyGrid;
						goto IL_008e;
						IL_008e:
						num2 = 7;
						num6 = ColName.Items.Count - 1;
						num5 = num6;
						goto IL_0117;
						IL_0117:
						if (num5 >= 0)
						{
							goto IL_00a9;
						}
						goto IL_011b;
						IL_011b:
						num2 = 12;
						num7 = ColCol.Items.Count - 1;
						num5 = num7;
						goto IL_01a6;
						IL_01a6:
						if (num5 < 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_0137;
						IL_0137:
						num2 = 13;
						if (Strings.Len(RuntimeHelpers.GetObjectValue(ColCol.Items[num5])) >= 1 && Operators.CompareString(Strings.Mid(Conversions.ToString(ColCol.Items[num5]), 1, 1), MyRowS, TextCompare: false) == 0)
						{
							goto IL_0188;
						}
						goto IL_019f;
						IL_00f9:
						num2 = 9;
						ColName.Items.RemoveAt(num5);
						goto IL_0110;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 515;
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

	public void Clear_A_GridR(ref DataGridView MyGrid, string MyRowS, int MyCol, int RowsInit)
	{
		int num = 0;
		int num2 = 0;
		string text = "";
		bool flag = false;
		checked
		{
			int num3 = MyGrid.RowCount - 1;
			for (num = num3; num >= 0; num += -1)
			{
				text = Conversions.ToString(MyGrid.Rows[num].Cells[MyCol].Value);
				if (Strings.Len(text) >= 1 && Operators.CompareString(Strings.Mid(text, 1, 1), MyRowS, TextCompare: false) == 0)
				{
					MyGrid.Rows.Remove(MyGrid.Rows[num]);
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			if (RowsInit != -1)
			{
				MyGrid.RowCount = RowsInit;
			}
			else
			{
				int num4 = MyGrid.RowCount - 1;
				for (num = num4; num >= 0; num += -1)
				{
					if (Operators.ConditionalCompareObjectEqual(MyGrid.Rows[num].Cells[MyCol].Value, "", TextCompare: false))
					{
						MyGrid.Rows.Remove(MyGrid.Rows[num]);
					}
				}
			}
			GridModule.Number_Grid(ref MyGrid);
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 619:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0180:
					num2 = 11;
					GridColRule.Rows[2].Cells[5].Value = "white";
					goto IL_01aa;
					IL_01aa:
					num2 = 12;
					GridColRule.Rows[2].Cells[8].Value = "normal";
					goto IL_01d4;
					IL_0156:
					num2 = 10;
					GridColRule.Rows[2].Cells[4].Value = "lightgreen";
					goto IL_0180;
					IL_01d4:
					num2 = 13;
					GridColRule.Rows[2].Cells[9].Value = "bold";
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
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				DataGridView MyGrid = GridColRule;
				BuildChart.Format_Grid(ref MyGrid, 5);
				GridColRule = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 619;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmReport - Form_Load";
						int num3 = 0;
						string text = "";
						bool flag = false;
						string text2 = "";
						string text3 = "";
						Set_CC_Enabled_ver("2");
						Button MyButton = CmdBrowseIn;
						BuildForm.Set_Btn_Img(ref MyButton, "browse");
						CmdBrowseIn = MyButton;
						MyButton = cmdEditVar;
						BuildForm.Set_Btn_Img(ref MyButton, "edit");
						cmdEditVar = MyButton;
						MyButton = cmdUp0;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						cmdUp0 = MyButton;
						MyButton = cmdDown0;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						cmdDown0 = MyButton;
						MyButton = cmdDel0;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						cmdDel0 = MyButton;
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
						MyButton = CmdUp4;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						CmdUp4 = MyButton;
						MyButton = CmdDown4;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						CmdDown4 = MyButton;
						MyButton = CmdDel4;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						CmdDel4 = MyButton;
						MyButton = CmdUp5;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						CmdUp5 = MyButton;
						MyButton = CmdDown5;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						CmdDown5 = MyButton;
						MyButton = CmdDel5;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						CmdDel5 = MyButton;
						MyButton = CC_KeysColsOrderUpBtn;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						CC_KeysColsOrderUpBtn = MyButton;
						MyButton = CC_KeysColsOrderDownBtn;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						CC_KeysColsOrderDownBtn = MyButton;
						MyButton = cmdMacroGuid;
						BuildForm.Set_Btn_Img(ref MyButton, "edit");
						cmdMacroGuid = MyButton;
						f_MinCols = GridCSV.ColumnCount - 1;
						BuildReport.Load_CSSObj(ref CSSObj, ref f_MaxObj);
						GridSort.RowCount = 15;
						GridRptHdr.RowCount = 10;
						GridColRule.RowCount = 100;
						GridCSV.RowCount = 3;
						DataGridView MyGrid = GridSort;
						GridModule.Number_Grid(ref MyGrid);
						GridSort = MyGrid;
						MyGrid = GridRptHdr;
						GridModule.Number_Grid(ref MyGrid);
						GridRptHdr = MyGrid;
						MyGrid = GridColRule;
						GridModule.Number_Grid(ref MyGrid);
						GridColRule = MyGrid;
						MyGrid = GridCSV;
						GridModule.Number_Grid(ref MyGrid);
						GridCSV = MyGrid;
						DataGridViewComboBoxColumn Cmb = ColRuleCTA;
						Load_Grid_Format_Values("text-align", ref Cmb);
						ColRuleCTA = Cmb;
						Cmb = ColRuleCVA;
						Load_Grid_Format_Values("valign", ref Cmb);
						ColRuleCVA = Cmb;
						Clear_All_Grids();
						Add_Query_To_Title();
						if (Operators.CompareString(f_ReportSpec, "", TextCompare: false) != 0)
						{
							Open_Report(f_ReportSpec);
						}
						MyGrid = GridCols;
						GridModule.Number_Grid(ref MyGrid);
						GridCols = MyGrid;
						if (Operators.CompareString(fDisplay, "", TextCompare: false) != 0 && Operators.CompareString(fActual, "", TextCompare: false) != 0)
						{
							text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(",", GridCSV.Rows[0].Cells[9].Value), ","));
							string[] array = Strings.Split(fDisplay, ",");
							string[] array2 = Strings.Split(fActual, ",");
							int num4 = Information.UBound(array);
							for (num3 = 0; num3 <= num4; num3++)
							{
								if (Operators.CompareString(Strings.Mid(array[num3] + "   ", 1, 3), "CI:", TextCompare: false) == 0)
								{
									fMenuItems++;
									text2 = Strings.Mid(array[num3], 4);
									text3 = Strings.Mid(array2[num3], 4);
									text3 = Strings.LCase(Strings.Replace(text3, ".GIF", "", 1, -1, CompareMethod.Text));
									flag = false;
									if (Strings.InStr(text, "," + text3 + ",") != 0)
									{
										flag = true;
									}
									ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
									toolStripMenuItem.Text = text2;
									toolStripMenuItem.Name = "mnu" + Conversions.ToString(fMenuItems);
									toolStripMenuItem.Tag = text3;
									toolStripMenuItem.CheckOnClick = true;
									toolStripMenuItem.Checked = flag;
									ToolStripMenuItem value = toolStripMenuItem;
									mnuFilterCharts.DropDownItems.Add(value);
								}
							}
							array = null;
							array2 = null;
						}
						IsStartUp = false;
						string myFile = Globals_Renamed.MySchemaDir + "\\cc_url.dat";
						ComboBox cmbQueue = CmbCCURL;
						General_Procedures.Load_Combo_File(myFile, ref cmbQueue);
						CmbCCURL = cmbQueue;
						string myFile2 = Globals_Renamed.MySchemaDir + "\\cc_data.dat";
						cmbQueue = cmbCCData;
						General_Procedures.Load_Combo_File(myFile2, ref cmbQueue);
						cmbCCData = cmbQueue;
						goto end_IL_0001;
					}
					case 1572:
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
					goto IL_065a;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1572;
				continue;
			}
			break;
			IL_065a:
			throw ProjectData.CreateProjectError(-2146828237);
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

	public int Get_File_No()
	{
		int num = 0;
		int num2 = 0;
		bool flag = false;
		num2 = 1;
		checked
		{
			do
			{
				flag = false;
				int num3 = GridCSV.RowCount - 1;
				for (num = 0; num <= num3; num++)
				{
					if (Operators.ConditionalCompareObjectEqual(GridCSV.Rows[num].Cells[1].Value, Conversions.ToString(num2), TextCompare: false))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
				num2++;
			}
			while (num2 <= 3);
			if (!flag)
			{
				return num2;
			}
			return -1;
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
					int rowIndex = GridCSV.CurrentCell.RowIndex;
					if (rowIndex == -1)
					{
						goto end_IL_0001;
					}
					int num3 = -1;
					string text = "";
					string text2 = Strings.Trim(Conversions.ToString(GridCSV.Rows[rowIndex].Cells[0].Value));
					text = Conversions.ToString(GridCSV.Rows[rowIndex].Cells[1].Value);
					num3 = ((Operators.CompareString(text, "", TextCompare: false) != 0) ? Conversions.ToInteger(text) : Get_File_No());
					string[] lCSVCols = new string[0];
					short num4 = 0;
					string text3 = "";
					bool flag = false;
					string MyMissData = "";
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					text3 = text2;
					BuildForm.FileOpenSave("O", text2, "csv+", "Find CSV/TAB File", "");
					text2 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					if (Operators.CompareString(text2, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						num7 = (int)Interaction.MsgBox("Loading a new file at a position normally clears data for the corresponding existing file in your report design. Do you wish to proceed and clear data (Yes), to cancel the action (Cancel) or to proceed but preserve the current report definition even if it may be incompatible with the new file (No)?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Load File and clear (Yes), Load File and Preserve Design (No) or Cancel Action (Cancel)?");
						if (num7 == 2)
						{
							goto end_IL_0001;
						}
					}
					else
					{
						num7 = 6;
					}
					if (General_Procedures.GetCSVHeaders(text2, ref lCSVCols) != 0)
					{
						goto end_IL_0001;
					}
					GridCSV.Rows[rowIndex].Cells[0].Value = BuildForm.Strip_Add_MyPCDir("S", text2);
					GridCSV.Rows[rowIndex].Cells[1].Value = Conversions.ToString(num3);
					GridCSV.Rows[rowIndex].Cells[2].Value = "";
					GridCSV.Rows[rowIndex].Cells[3].Value = "";
					GridCSV.Rows[rowIndex].Cells[4].Value = "Auto";
					GridCSV.Rows[rowIndex].Cells[5].Value = "Auto";
					GridCSV.Rows[rowIndex].Cells[6].Value = "Auto";
					GridCSV.Rows[rowIndex].Cells[7].Value = "Auto";
					GridCSV.Rows[rowIndex].Cells[8].Value = "Auto";
					GridCSV.Rows[rowIndex].Cells[12].Value = "N";
					GridCSV.Rows[rowIndex].Cells[14].Value = "N";
					GridCSV.Rows[rowIndex].Cells[15].Value = "N";
					int num10 = Information.UBound(lCSVCols);
					checked
					{
						for (num5 = 0; num5 <= num10; num5++)
						{
							lCSVCols[num5] = Strings.LCase(BuildForm.Test_Valid_jqw_Col(lCSVCols[num5]));
							lCSVCols[num5] = Conversions.ToString(num3) + ":" + lCSVCols[num5];
						}
						DataGridView MyGrid;
						switch (num7)
						{
						case 6:
						{
							Clear_Grids(Conversions.ToString(num3));
							num6 = GridCols.RowCount - 1;
							GridCols.RowCount = GridCols.RowCount + Information.UBound(lCSVCols) + 1;
							int num14 = Information.UBound(lCSVCols);
							for (num5 = 0; num5 <= num14; num5++)
							{
								ColName.Items.Add(lCSVCols[num5]);
								ColCol.Items.Add(lCSVCols[num5]);
								num6++;
								GridCols.Rows[num6].Cells[0].Value = lCSVCols[num5];
								GridCols.Rows[num6].Cells[1].Value = Strings.Replace(Strings.StrConv(Strings.Trim(Strings.Mid(lCSVCols[num5] + "   ", 3)), VbStrConv.ProperCase), "_", " ", 1, -1, CompareMethod.Text);
								GridCols.Rows[num6].Cells[2].Value = "left";
								GridCols.Rows[num6].Cells[3].Value = "left";
								GridCols.Rows[num6].Cells[4].Value = "string";
								GridCols.Rows[num6].Cells[5].Value = "100";
								GridCols.Rows[num6].Cells[7].Value = 1;
								GridCols.Rows[num6].Cells[8].Value = "chklist";
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
							MyGrid = GridCSV;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 2, ref MyMissData, 0);
							GridCSV = MyGrid;
							MyGrid = GridCSV;
							BuildReport.Report_Verify_Columns(ref MyGrid, ref lCSVCols, 3, ref MyMissData, 0);
							GridCSV = MyGrid;
							num8 = ColName.Items.Count;
							num9 = ColCol.Items.Count;
							int num11 = num8 - 1;
							for (num5 = num11; num5 >= 1; num5 += -1)
							{
								ColName.Items.RemoveAt(num5);
							}
							int num12 = num9 - 1;
							for (num5 = num12; num5 >= 5; num5 += -1)
							{
								ColCol.Items.RemoveAt(num5);
							}
							int num13 = Information.UBound(lCSVCols);
							for (num5 = 0; num5 <= num13; num5++)
							{
								ColName.Items.Add(lCSVCols[num5]);
								ColCol.Items.Add(lCSVCols[num5]);
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
				case 1985:
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
				try0001_dispatch = 1985;
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

	private void GridSort_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		int columnIndex = default(int);
		string left2 = default(string);
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
				case 361:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_008b;
						case 15:
							goto IL_0095;
						case 16:
							goto IL_00c1;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 9:
						case 12:
						case 14:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c1:
					num2 = 16;
					if (Operators.CompareString(left, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_008b:
					num2 = 13;
					if (columnIndex != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0095;
					IL_0069:
					num2 = 11;
					if (columnIndex != 0 || Operators.CompareString(left2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_008b;
					IL_0095:
					num2 = 15;
					left = Conversions.ToString(GridSort.Rows[rowIndex].Cells[1].Value);
					goto IL_00c1;
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
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				GridSort.Rows[rowIndex].Cells[1].Value = "Asc";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 361;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
				GridCSV.Focus();
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
				case 165:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0046;
						case 8:
							goto IL_004f;
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
					Set_CC_Enabled_ver("2");
					goto IL_0046;
					IL_0046:
					num2 = 7;
					Clear_All_Grids();
					goto IL_004f;
					IL_002c:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0038;
					IL_004f:
					num2 = 8;
					Initialize_Formats();
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
				try0001_dispatch = 165;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
				case 292:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0067;
						case 9:
							goto IL_007d;
						case 10:
							goto IL_0097;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 8:
						case 12:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0067:
					num2 = 7;
					if (rowIndex == -1 || columnIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_007d;
					IL_0097:
					num2 = 10;
					GridCols.CurrentCell = GridCols[columnIndex, checked(rowIndex + 1)];
					break;
					IL_004d:
					num2 = 6;
					if (columnIndex != 0 && columnIndex != 2 && columnIndex != 3 && columnIndex != 4)
					{
						goto end_IL_0001_3;
					}
					goto IL_0067;
					IL_007d:
					num2 = 9;
					if (rowIndex >= checked(GridCols.RowCount - 1))
					{
						goto end_IL_0001_3;
					}
					goto IL_0097;
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
					rowIndex = GridCols.CurrentCell.RowIndex;
					goto IL_004d;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				GridCols.CurrentCell = GridCols[columnIndex, rowIndex];
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 292;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
				GridColRule.Focus();
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
						GridColRule.Rows[f_Row].Cells[f_Col].Value = CmbCol.Text;
						GridColRule.Refresh();
						CmbCol.Visible = false;
						f_Row = -1;
						f_Col = -1;
					}
					goto end_IL_0001;
				case 181:
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
				try0001_dispatch = 181;
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

	private void cmdeditvar_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowIndex = default(int);
		frmfilename frmfilename2 = default(frmfilename);
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
							goto IL_001e;
						case 5:
							goto IL_002f;
						case 6:
							goto IL_005e;
						case 7:
							goto IL_0079;
						case 8:
							goto IL_0082;
						case 9:
							goto IL_008e;
						case 10:
							goto IL_0098;
						case 11:
							goto IL_00a3;
						case 12:
							goto IL_00d3;
						case 13:
							goto IL_00fd;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 14:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a3:
					num2 = 11;
					if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0))
					{
						goto IL_00d3;
					}
					goto IL_00fd;
					IL_00d3:
					num2 = 12;
					GridCSV.Rows[rowIndex].Cells[0].Value = Globals_Renamed.currvaluetmp;
					goto IL_00fd;
					IL_0098:
					num2 = 10;
					frmfilename2.ShowDialog();
					goto IL_00a3;
					IL_00fd:
					num2 = 13;
					Globals_Renamed.currvaluetmp = "";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					rowIndex = GridCSV.CurrentCell.RowIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (rowIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_002f;
					IL_002f:
					num2 = 5;
					text = Strings.Trim(Conversions.ToString(GridCSV.Rows[rowIndex].Cells[0].Value));
					goto IL_005e;
					IL_005e:
					num2 = 6;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0079;
					IL_0079:
					num2 = 7;
					Globals_Renamed.currvaluetmp = text;
					goto IL_0082;
					IL_0082:
					num2 = 8;
					Globals_Renamed.currinputstrtmp = "G";
					goto IL_008e;
					IL_008e:
					num2 = 9;
					frmfilename2 = new frmfilename();
					goto IL_0098;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				Interaction.MsgBox("You must first design a report before you can Assign a Global replacement variable.", MsgBoxStyle.Information, "Cannot Yet Assign a Global Variable");
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

	private void cmdUp0_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridCSV;
				GridModule.Grid_Up(ref MyGrid);
				GridCSV = MyGrid;
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

	private void cmdDown0_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridCSV;
				GridModule.Grid_Down(ref MyGrid);
				GridCSV = MyGrid;
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

	private void cmdDel0_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowIndex = default(int);
		int num5 = default(int);
		string myRowS = default(string);
		string text = default(string);
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
				case 411:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_002f;
						case 6:
							goto IL_0059;
						case 7:
							goto IL_0088;
						case 8:
							goto IL_008d;
						case 9:
							goto IL_00a5;
						case 11:
							goto IL_00c6;
						case 10:
						case 12:
						case 13:
							goto IL_00cd;
						case 14:
							goto IL_00db;
						case 15:
							goto IL_0100;
						case 16:
							goto IL_0110;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00db:
					num2 = 14;
					GridCSV.Rows.Remove(GridCSV.Rows[rowIndex]);
					goto IL_0100;
					IL_0100:
					num2 = 15;
					GridCSV.RowCount = 3;
					goto IL_0110;
					IL_00cd:
					num2 = 13;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_00db;
					IL_0110:
					num2 = 16;
					MyGrid = GridCSV;
					GridModule.Number_Grid(ref MyGrid);
					GridCSV = MyGrid;
					break;
					IL_000b:
					num2 = 2;
					rowIndex = GridCSV.CurrentCell.RowIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (rowIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_002f;
					IL_002f:
					num2 = 5;
					myRowS = Conversions.ToString(GridCSV.Rows[rowIndex].Cells[1].Value);
					goto IL_0059;
					IL_0059:
					num2 = 6;
					text = Strings.Trim(Conversions.ToString(GridCSV.Rows[rowIndex].Cells[0].Value));
					goto IL_0088;
					IL_0088:
					num2 = 7;
					num5 = 0;
					goto IL_008d;
					IL_008d:
					num2 = 8;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_00a5;
					}
					goto IL_00c6;
					IL_00a5:
					num2 = 9;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to remove Data for File: " + text, MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Delete File Data?");
					goto IL_00cd;
					IL_00c6:
					num2 = 11;
					num5 = 6;
					goto IL_00cd;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				Clear_Grids(myRowS);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 411;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridCols_MouseDoubleClick(object sender, MouseEventArgs e)
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
					string text2 = "";
					string text3 = "";
					int num3 = 0;
					string text4 = "";
					if (rowIndex == -1 || columnIndex < 15)
					{
						goto end_IL_0001;
					}
					checked
					{
						if (Operators.ConditionalCompareObjectEqual(GridCols.CurrentCell.Value, "Expr:", TextCompare: false))
						{
							text = Conversions.ToString(GridCols.CurrentCell.Tag);
							text2 = Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(GridCols.Rows[rowIndex].Cells[0].Value, "  ")), 1, 2);
							if (Operators.CompareString(Strings.Mid(text2, 2, 1), ":", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text2, 1, 1)))
							{
								goto end_IL_0001;
							}
							text4 = "";
							int num4 = GridCSV.RowCount - 1;
							for (num3 = 0; num3 <= num4; num3++)
							{
								text3 = Conversions.ToString(GridCSV.Rows[num3].Cells[1].Value);
								if (Operators.CompareString(text3, Strings.Mid(text2, 1, 1), TextCompare: false) == 0)
								{
									text4 = Conversions.ToString(GridCSV.Rows[num3].Cells[0].Value);
									break;
								}
							}
							MyProject.Forms.FrmMain.LoadComputedColumnForm(10, "MODIFY", text, "C", BuildForm.Replace_Globals(text4), -1);
							GridCols.CurrentCell.Tag = Globals_Renamed.currDataAny;
							Refresh();
							Globals_Renamed.currtxttmp = "";
							Globals_Renamed.currDataAny = "";
							goto end_IL_0001;
						}
						if (Operators.ConditionalCompareObjectEqual(GridCols.CurrentCell.Value, "Text:", TextCompare: false))
						{
							text = Conversions.ToString(GridCols.CurrentCell.Tag);
							text = Interaction.InputBox("Enter Text to display at the bottom of the report. Enter DEL to remove the text expression.", "Enter Text", text);
							if (Operators.CompareString(Strings.UCase(text), "DEL", TextCompare: false) == 0)
							{
								GridCols.CurrentCell.Tag = "";
							}
							else if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								GridCols.CurrentCell.Tag = text;
							}
						}
						goto end_IL_0001_2;
					}
				}
				case 732:
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
				goto IL_0312;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 732;
				continue;
			}
			break;
			IL_0312:
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

	private void TabCols_Enter(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string candidate_Drilldown_column = default(string);
		while (true)
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
				case 152:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0027;
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
					IL_0014:
					num2 = 3;
					cmbTopOf.Items.Clear();
					goto IL_0027;
					IL_0027:
					num2 = 4;
					cmbTopOf.Items.Add("");
					goto IL_003f;
					IL_000b:
					num2 = 2;
					candidate_Drilldown_column = Get_Candidate_Drilldown_column();
					goto IL_0014;
					IL_003f:
					num2 = 5;
					if (Operators.CompareString(candidate_Drilldown_column, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				cmbTopOf.Items.Add(candidate_Drilldown_column);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 152;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridCSV_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		int num7 = default(int);
		string text3 = default(string);
		int num8 = default(int);
		int num9 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				object instance;
				object[] obj;
				object[] array;
				bool[] obj2;
				bool[] array2;
				object instance2;
				DataGridView MyGrid;
				ComboBox MyCombo;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1249:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_0085;
						case 7:
							goto IL_011d;
						case 8:
							goto IL_0122;
						case 9:
							goto IL_012b;
						case 10:
							goto IL_0136;
						case 12:
							goto IL_0168;
						case 15:
							goto IL_0182;
						case 16:
							goto IL_0196;
						case 17:
							goto IL_01af;
						case 18:
							goto IL_01ce;
						case 19:
							goto IL_01ea;
						case 20:
							goto IL_0207;
						case 21:
							goto IL_020e;
						case 22:
							goto IL_0220;
						case 23:
							goto IL_022e;
						case 24:
							goto IL_023e;
						case 25:
						case 26:
							goto IL_0256;
						case 27:
							goto IL_0268;
						case 30:
							goto IL_02ab;
						case 31:
							goto IL_02bf;
						case 32:
							goto IL_02d8;
						case 33:
							goto IL_02f1;
						case 34:
							goto IL_030a;
						case 35:
							goto IL_0323;
						case 36:
							goto IL_033c;
						case 38:
							goto IL_037e;
						case 39:
							goto IL_0392;
						case 40:
							goto IL_03ab;
						case 41:
							goto IL_03c4;
						case 42:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 11:
						case 13:
						case 14:
						case 28:
						case 29:
						case 37:
						case 43:
						case 44:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_020e:
					num2 = 21;
					num5 = Strings.InStr(text, ":");
					goto IL_0220;
					IL_0220:
					num2 = 22;
					if (num5 != 0)
					{
						goto IL_022e;
					}
					goto IL_023e;
					IL_0207:
					num2 = 20;
					text = text2;
					goto IL_020e;
					IL_022e:
					num2 = 23;
					text = Strings.Mid(text2, checked(num5 + 1));
					goto IL_023e;
					IL_000b:
					num2 = 2;
					num6 = Conversions.ToInteger(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "ColumnIndex", new object[0], null, null, null));
					goto IL_003c;
					IL_003c:
					num2 = 3;
					num7 = Conversions.ToInteger(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "CurrentCell", new object[0], null, null, null), null, "RowIndex", new object[0], null, null, null));
					goto IL_006e;
					IL_006e:
					num2 = 4;
					if (num7 == -1 || num6 == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0085;
					IL_0085:
					num2 = 6;
					instance = sender;
					obj = new object[1] { num7 };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					instance2 = NewLateBinding.LateGet(instance, null, "rows", obj, null, null, obj2);
					if (array2[0])
					{
						num7 = (int)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(int));
					}
					text3 = Strings.Trim(Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(instance2, null, "cells", new object[1] { 1 }, null, null, null), null, "value", new object[0], null, null, null)));
					goto IL_011d;
					IL_0256:
					num2 = 26;
					num8 = checked(num8 + 1);
					goto IL_025f;
					IL_023e:
					num2 = 24;
					cmbCSV.Items.Add(text);
					goto IL_0256;
					IL_011d:
					num2 = 7;
					num8 = 0;
					goto IL_0122;
					IL_0122:
					num2 = 8;
					text2 = "";
					goto IL_012b;
					IL_012b:
					num2 = 9;
					text = "";
					goto IL_0136;
					IL_0136:
					num2 = 10;
					switch (num6)
					{
					case 2:
					case 3:
						break;
					case 10:
						goto IL_02ab;
					case 12:
					case 14:
					case 15:
						goto IL_037e;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0168;
					IL_037e:
					num2 = 38;
					cmbCSV.Items.Clear();
					goto IL_0392;
					IL_0392:
					num2 = 39;
					cmbCSV.Items.Add("");
					goto IL_03ab;
					IL_03ab:
					num2 = 40;
					cmbCSV.Items.Add("N");
					goto IL_03c4;
					IL_03c4:
					num2 = 41;
					cmbCSV.Items.Add("Y");
					break;
					IL_02ab:
					num2 = 30;
					cmbCSV.Items.Clear();
					goto IL_02bf;
					IL_02bf:
					num2 = 31;
					cmbCSV.Items.Add("");
					goto IL_02d8;
					IL_02d8:
					num2 = 32;
					cmbCSV.Items.Add("['1', '5', '10']");
					goto IL_02f1;
					IL_02f1:
					num2 = 33;
					cmbCSV.Items.Add("['1', '5', '10', '20']");
					goto IL_030a;
					IL_030a:
					num2 = 34;
					cmbCSV.Items.Add("['1', '10', '20', '30']");
					goto IL_0323;
					IL_0323:
					num2 = 35;
					cmbCSV.Items.Add("['1', '20', '40', '60']");
					goto IL_033c;
					IL_033c:
					num2 = 36;
					MyGrid = (DataGridView)sender;
					MyCombo = cmbCSV;
					GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 3);
					cmbCSV = MyCombo;
					sender = MyGrid;
					goto end_IL_0001_3;
					IL_0168:
					num2 = 12;
					if (num7 == 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_0182;
					IL_0182:
					num2 = 15;
					cmbCSV.Items.Clear();
					goto IL_0196;
					IL_0196:
					num2 = 16;
					cmbCSV.Items.Add("");
					goto IL_01af;
					IL_01af:
					num2 = 17;
					num9 = checked(ColName.Items.Count - 1);
					num8 = 0;
					goto IL_025f;
					IL_025f:
					if (num8 <= num9)
					{
						goto IL_01ce;
					}
					goto IL_0268;
					IL_0268:
					num2 = 27;
					MyGrid = (DataGridView)sender;
					MyCombo = cmbCSV;
					GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 3);
					cmbCSV = MyCombo;
					sender = MyGrid;
					goto end_IL_0001_3;
					IL_01ce:
					num2 = 18;
					text2 = Conversions.ToString(ColName.Items[num8]);
					goto IL_01ea;
					IL_01ea:
					num2 = 19;
					if (LikeOperator.LikeString(text2, text3 + ":*", CompareMethod.Binary))
					{
						goto IL_0207;
					}
					goto IL_0256;
					end_IL_0001_2:
					break;
				}
				num2 = 42;
				MyGrid = (DataGridView)sender;
				MyCombo = cmbCSV;
				GridModule.Combo_Click(ref MyGrid, ref MyCombo, ref f_Row, ref f_Col, 3);
				cmbCSV = MyCombo;
				sender = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 1249;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmbCSV_KeyPress(object sender, KeyPressEventArgs e)
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
				GridCSV.Focus();
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

	private void cmbCSV_Leave(object sender, EventArgs e)
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
					errsource = "FrmReport - CmbCSV_Leave";
					ProjectData.ClearProjectError();
					num2 = 2;
					if (!((f_Row == -1) | (f_Col == -1)))
					{
						GridCSV.Rows[f_Row].Cells[f_Col].Value = cmbCSV.Text;
						GridCSV.Refresh();
						cmbCSV.Visible = false;
						f_Row = -1;
						f_Col = -1;
					}
					goto end_IL_0001;
				case 181:
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
				try0001_dispatch = 181;
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

	private void GridCSV_Scroll(object sender, ScrollEventArgs e)
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

	private void TabRpt_Selected(object sender, TabControlEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		ListViewItem listViewItem = default(ListViewItem);
		List<string> list = default(List<string>);
		string text = default(string);
		int num5 = default(int);
		string value = default(string);
		int num7 = default(int);
		ArrayList arrayList = default(ArrayList);
		IEnumerator enumerator = default(IEnumerator);
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
				case 734:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0033;
						case 6:
							goto IL_00d2;
						case 7:
							goto IL_00d7;
						case 8:
							goto IL_00dc;
						case 9:
							goto IL_00e5;
						case 10:
							goto IL_00ef;
						case 11:
							goto IL_00f9;
						case 12:
							goto IL_0110;
						case 13:
							goto IL_013c;
						case 14:
							goto IL_014e;
						case 15:
							goto IL_015c;
						case 16:
						case 17:
							goto IL_016b;
						case 18:
							goto IL_017a;
						case 19:
							goto IL_0184;
						case 20:
							goto IL_0199;
						case 21:
							goto IL_01b5;
						case 22:
							goto IL_01cf;
						case 23:
						case 24:
							goto IL_01e7;
						case 25:
							goto IL_01f7;
						case 26:
							goto IL_0210;
						case 27:
							goto IL_0220;
						case 28:
							goto IL_022b;
						case 29:
							goto IL_0231;
						case 30:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 5:
						case 31:
						case 32:
						case 33:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01cf:
					num2 = 22;
					CC_KeysColsListView.Items.Remove(listViewItem);
					goto IL_01e7;
					IL_01e7:
					num2 = 24;
					goto IL_01ea;
					IL_01b5:
					num2 = 21;
					if (!list.Contains(listViewItem.Text))
					{
						goto IL_01cf;
					}
					goto IL_01e7;
					IL_0110:
					num2 = 12;
					text = Conversions.ToString(GridCols.Rows[num5].Cells[0].Value);
					goto IL_013c;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(e.TabPage.Name, Tab_CC.Name, TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0033;
					IL_0033:
					num2 = 3;
					if (Operators.ConditionalCompareObjectEqual(GridCSV.Rows[0].Cells[0].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridCSV.Rows[1].Cells[0].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridCSV.Rows[2].Cells[0].Value, "", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					goto IL_00d2;
					IL_013c:
					num2 = 13;
					if (text.StartsWith(value))
					{
						goto IL_014e;
					}
					goto IL_016b;
					IL_014e:
					num2 = 14;
					text = text.Remove(0, 2);
					goto IL_015c;
					IL_00d2:
					num2 = 6;
					num6 = 0;
					goto IL_00d7;
					IL_00d7:
					num2 = 7;
					num5 = 0;
					goto IL_00dc;
					IL_00dc:
					num2 = 8;
					value = "1:";
					goto IL_00e5;
					IL_00e5:
					num2 = 9;
					text = "";
					goto IL_00ef;
					IL_00ef:
					num2 = 10;
					list = new List<string>();
					goto IL_00f9;
					IL_00f9:
					num2 = 11;
					num7 = checked(GridCols.RowCount - 1);
					num5 = 0;
					goto IL_0174;
					IL_0174:
					if (num5 <= num7)
					{
						goto IL_0110;
					}
					goto IL_017a;
					IL_017a:
					num2 = 18;
					FillCC_SrcColsListView();
					goto IL_0184;
					IL_0184:
					num2 = 19;
					arrayList = new ArrayList(CC_KeysColsListView.Items);
					goto IL_0199;
					IL_0199:
					num2 = 20;
					enumerator = arrayList.GetEnumerator();
					goto IL_01ea;
					IL_01ea:
					if (enumerator.MoveNext())
					{
						listViewItem = (ListViewItem)enumerator.Current;
						goto IL_01b5;
					}
					goto IL_01f7;
					IL_01f7:
					num2 = 25;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_0210;
					IL_016b:
					num2 = 17;
					num5 = checked(num5 + 1);
					goto IL_0174;
					IL_0210:
					num2 = 26;
					CC_KeysColsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					goto IL_0220;
					IL_0220:
					num2 = 27;
					arrayList.Clear();
					goto IL_022b;
					IL_022b:
					num2 = 28;
					arrayList = null;
					goto IL_0231;
					IL_0231:
					num2 = 29;
					list.Clear();
					break;
					IL_015c:
					num2 = 15;
					list.Add(text);
					goto IL_016b;
					end_IL_0001_2:
					break;
				}
				num2 = 30;
				list = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 734;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_KeyAddBtn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		ListViewItem listViewItem = default(ListViewItem);
		ArrayList arrayList = default(ArrayList);
		IEnumerator enumerator = default(IEnumerator);
		while (true)
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
				case 313:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003b;
						case 5:
							goto IL_0056;
						case 6:
							goto IL_0073;
						case 7:
							goto IL_0099;
						case 8:
						case 9:
							goto IL_00b0;
						case 10:
							goto IL_00c0;
						case 11:
							goto IL_00d9;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0099:
					num2 = 7;
					CC_SrcColsListView.Items.Remove(listViewItem);
					goto IL_00b0;
					IL_0056:
					num2 = 5;
					if (CC_KeysColsListView.FindItemWithText(listViewItem.Text) == null)
					{
						goto IL_0073;
					}
					goto IL_00b0;
					IL_00d9:
					num2 = 11;
					arrayList.Clear();
					break;
					IL_0073:
					num2 = 6;
					CC_KeysColsListView.Items.Add(listViewItem.Text, listViewItem.Text, "");
					goto IL_0099;
					IL_000b:
					num2 = 2;
					if (CC_SrcColsListView.SelectedItems.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					arrayList = new ArrayList(CC_SrcColsListView.SelectedItems);
					goto IL_003b;
					IL_003b:
					num2 = 4;
					enumerator = arrayList.GetEnumerator();
					goto IL_00b3;
					IL_00b3:
					if (enumerator.MoveNext())
					{
						listViewItem = (ListViewItem)enumerator.Current;
						goto IL_0056;
					}
					goto IL_00c0;
					IL_00c0:
					num2 = 10;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_00d9;
					IL_00b0:
					num2 = 9;
					goto IL_00b3;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				arrayList = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 313;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_KeyRemoveBtn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		ArrayList arrayList = default(ArrayList);
		ListViewItem item = default(ListViewItem);
		IEnumerator enumerator = default(IEnumerator);
		while (true)
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
				case 243:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003b;
						case 5:
							goto IL_0056;
						case 6:
							goto IL_006b;
						case 7:
							goto IL_007a;
						case 8:
							goto IL_0092;
						case 9:
							goto IL_009b;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0092:
					num2 = 8;
					FillCC_SrcColsListView();
					goto IL_009b;
					IL_009b:
					num2 = 9;
					arrayList.Clear();
					break;
					IL_006b:
					num2 = 6;
					goto IL_006d;
					IL_0056:
					num2 = 5;
					CC_KeysColsListView.Items.Remove(item);
					goto IL_006b;
					IL_000b:
					num2 = 2;
					if (CC_KeysColsListView.SelectedItems.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					arrayList = new ArrayList(CC_KeysColsListView.SelectedItems);
					goto IL_003b;
					IL_003b:
					num2 = 4;
					enumerator = arrayList.GetEnumerator();
					goto IL_006d;
					IL_006d:
					if (enumerator.MoveNext())
					{
						item = (ListViewItem)enumerator.Current;
						goto IL_0056;
					}
					goto IL_007a;
					IL_007a:
					num2 = 7;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_0092;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				arrayList = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 243;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_KeysColsOrderUpBtn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int index = default(int);
		ListViewItem listViewItem = default(ListViewItem);
		ArrayList arrayList = default(ArrayList);
		IEnumerator enumerator = default(IEnumerator);
		while (true)
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
				case 304:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003b;
						case 5:
							goto IL_0056;
						case 6:
							goto IL_0061;
						case 7:
							goto IL_006e;
						case 8:
							goto IL_0083;
						case 9:
						case 10:
							goto IL_00a3;
						case 11:
							goto IL_00b3;
						case 12:
							goto IL_00cc;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0056:
					num2 = 5;
					index = listViewItem.Index;
					goto IL_0061;
					IL_0061:
					num2 = 6;
					if (index > 0)
					{
						goto IL_006e;
					}
					goto IL_00a3;
					IL_0083:
					num2 = 8;
					CC_KeysColsListView.Items.Insert(checked(index - 1), listViewItem.Text);
					goto IL_00a3;
					IL_006e:
					num2 = 7;
					CC_KeysColsListView.Items.Remove(listViewItem);
					goto IL_0083;
					IL_000b:
					num2 = 2;
					if (CC_KeysColsListView.SelectedItems.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					arrayList = new ArrayList(CC_KeysColsListView.SelectedItems);
					goto IL_003b;
					IL_003b:
					num2 = 4;
					enumerator = arrayList.GetEnumerator();
					goto IL_00a6;
					IL_00a6:
					if (enumerator.MoveNext())
					{
						listViewItem = (ListViewItem)enumerator.Current;
						goto IL_0056;
					}
					goto IL_00b3;
					IL_00b3:
					num2 = 11;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_00cc;
					IL_00a3:
					num2 = 10;
					goto IL_00a6;
					IL_00cc:
					num2 = 12;
					arrayList.Clear();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				arrayList = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 304;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_KeysColsOrderDownBtn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int index = default(int);
		ListViewItem listViewItem = default(ListViewItem);
		ArrayList arrayList = default(ArrayList);
		IEnumerator enumerator = default(IEnumerator);
		while (true)
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
				case 319:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003b;
						case 5:
							goto IL_0056;
						case 6:
							goto IL_0061;
						case 7:
							goto IL_007d;
						case 8:
							goto IL_0092;
						case 9:
						case 10:
							goto IL_00b2;
						case 11:
							goto IL_00c2;
						case 12:
							goto IL_00db;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0056:
					num2 = 5;
					index = listViewItem.Index;
					goto IL_0061;
					IL_0061:
					num2 = 6;
					if (index < CC_KeysColsListView.Items.Count)
					{
						goto IL_007d;
					}
					goto IL_00b2;
					IL_0092:
					num2 = 8;
					CC_KeysColsListView.Items.Insert(checked(index + 1), listViewItem.Text);
					goto IL_00b2;
					IL_007d:
					num2 = 7;
					CC_KeysColsListView.Items.Remove(listViewItem);
					goto IL_0092;
					IL_000b:
					num2 = 2;
					if (CC_KeysColsListView.SelectedItems.Count <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 3;
					arrayList = new ArrayList(CC_KeysColsListView.SelectedItems);
					goto IL_003b;
					IL_003b:
					num2 = 4;
					enumerator = arrayList.GetEnumerator();
					goto IL_00b5;
					IL_00b5:
					if (enumerator.MoveNext())
					{
						listViewItem = (ListViewItem)enumerator.Current;
						goto IL_0056;
					}
					goto IL_00c2;
					IL_00c2:
					num2 = 11;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_00db;
					IL_00b2:
					num2 = 10;
					goto IL_00b5;
					IL_00db:
					num2 = 12;
					arrayList.Clear();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				arrayList = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 319;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_NewGuidBtn_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
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
							goto IL_0014;
						case 5:
							goto IL_003a;
						case 6:
							goto IL_004e;
						case 7:
							goto IL_005a;
						case 8:
						case 9:
							goto IL_0061;
						case 10:
							goto IL_006c;
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
					flag = false;
					goto IL_0061;
					IL_0061:
					num2 = 9;
					if (!flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_006c;
					IL_004e:
					num2 = 6;
					if (num5 == 7)
					{
						goto IL_005a;
					}
					goto IL_0061;
					IL_006c:
					num2 = 10;
					CC_RepGUID = Guid.NewGuid();
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					flag = true;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					if (Operators.CompareString(Strings.Trim(CC_GuidTxtBx.Text), "", TextCompare: false) != 0)
					{
						goto IL_003a;
					}
					goto IL_0061;
					IL_003a:
					num2 = 5;
					num5 = (int)Interaction.MsgBox("Are you sure you wish to change the GUID?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Update GUID?");
					goto IL_004e;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				CC_GuidTxtBx.Text = CC_RepGUID.ToString("B");
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

	private void mnuCC_Click(object sender, EventArgs e)
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
				case 142:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					CC_Enabled = Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "Checked", new object[0], null, null, null));
					goto IL_002d;
					IL_002d:
					num2 = 3;
					GrpCC.Enabled = CC_Enabled;
					goto IL_0041;
					IL_0041:
					num2 = 4;
					if (CC_Enabled)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Set_CC_Enabled_ver("2");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 142;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FillCC_SrcColsListView()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string value = default(string);
		int num5 = default(int);
		List<string> list = default(List<string>);
		int num6 = default(int);
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		while (true)
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
				case 586:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_0025;
						case 6:
							goto IL_0059;
						case 7:
							goto IL_008d;
						case 8:
							goto IL_00a0;
						case 9:
							goto IL_00b6;
						case 10:
							goto IL_00e1;
						case 11:
							goto IL_0106;
						case 12:
							goto IL_0112;
						case 13:
						case 14:
							goto IL_0120;
						case 15:
							goto IL_012f;
						case 16:
							goto IL_0145;
						case 17:
							goto IL_016d;
						case 18:
						case 19:
							goto IL_018a;
						case 20:
							goto IL_019a;
						case 21:
							goto IL_01ab;
						case 22:
							goto IL_01bb;
						case 23:
							goto IL_01c6;
						case 24:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00e1:
					num2 = 10;
					if (text.StartsWith(value) && Strings.InStr(text, "spf$comment$") == 0)
					{
						goto IL_0106;
					}
					goto IL_0120;
					IL_0120:
					num2 = 14;
					num5 = checked(num5 + 1);
					goto IL_0129;
					IL_00b6:
					num2 = 9;
					text = Conversions.ToString(GridCols.Rows[num5].Cells[0].Value);
					goto IL_00e1;
					IL_0106:
					num2 = 11;
					text = text.Remove(0, 2);
					goto IL_0112;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					list = new List<string>();
					goto IL_001c;
					IL_001c:
					num2 = 4;
					value = "1:";
					goto IL_0025;
					IL_0025:
					num2 = 5;
					if (Operators.ConditionalCompareObjectNotEqual(GridCSV.Rows[0].Cells[1].Value, "", TextCompare: false))
					{
						goto IL_0059;
					}
					goto IL_008d;
					IL_0059:
					num2 = 6;
					value = Conversions.ToString(Operators.ConcatenateObject(GridCSV.Rows[0].Cells[1].Value, ":"));
					goto IL_008d;
					IL_008d:
					num2 = 7;
					CC_SrcColsListView.Items.Clear();
					goto IL_00a0;
					IL_00a0:
					num2 = 8;
					num6 = checked(GridCols.RowCount - 1);
					num5 = 0;
					goto IL_0129;
					IL_0129:
					if (num5 <= num6)
					{
						goto IL_00b6;
					}
					goto IL_012f;
					IL_012f:
					num2 = 15;
					enumerator = list.GetEnumerator();
					goto IL_018d;
					IL_018d:
					if (enumerator.MoveNext())
					{
						text = enumerator.Current;
						goto IL_0145;
					}
					goto IL_019a;
					IL_019a:
					num2 = 20;
					((IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
					goto IL_01ab;
					IL_01ab:
					num2 = 21;
					CC_SrcColsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					goto IL_01bb;
					IL_01bb:
					num2 = 22;
					list.Clear();
					goto IL_01c6;
					IL_01c6:
					num2 = 23;
					list = null;
					break;
					IL_0112:
					num2 = 12;
					list.Add(text);
					goto IL_0120;
					IL_0145:
					num2 = 16;
					if ((SPFExtensions.FindItemWithTextCS(CC_KeysColsListView, text) == null) & (SPFExtensions.FindItemWithTextCS(CC_SrcColsListView, text) == null))
					{
						goto IL_016d;
					}
					goto IL_018a;
					IL_016d:
					num2 = 17;
					CC_SrcColsListView.Items.Add(text, text, "");
					goto IL_018a;
					IL_018a:
					num2 = 19;
					goto IL_018d;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				text = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 586;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CC_HeaderTxtBx_Validating(object sender, CancelEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
		int num5 = default(int);
		int index = default(int);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
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
				case 351:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0032;
						case 7:
							goto IL_00c3;
						case 9:
						case 10:
							goto IL_00cc;
						case 8:
						case 11:
							goto IL_00de;
						case 12:
							goto IL_00ec;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ec:
					num2 = 12;
					Interaction.MsgBox("Comment header name conflicts with a Column Header specified in the Columns tab", MsgBoxStyle.Exclamation, "Invalid Comment Header");
					break;
					IL_00c3:
					num2 = 7;
					flag = false;
					goto IL_00de;
					IL_00cc:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_00d5;
					IL_00de:
					num2 = 11;
					if (flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_00ec;
					IL_000b:
					num2 = 2;
					index = 1;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					flag = true;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					num6 = checked(GridCols.RowCount - 1);
					num5 = 0;
					goto IL_00d5;
					IL_00d5:
					if (num5 <= num6)
					{
						goto IL_0032;
					}
					goto IL_00de;
					IL_0032:
					num2 = 6;
					typeFromHandle = typeof(Strings);
					obj = new object[1] { (dataGridViewCell = GridCols.Rows[num5].Cells[index]).Value };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					left = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
					if (array2[0])
					{
						dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (Operators.ConditionalCompareObjectEqual(left, Strings.UCase(CC_HeaderTxtBx.Text), TextCompare: false))
					{
						goto IL_00c3;
					}
					goto IL_00cc;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				e.Cancel = true;
				break;
				end_IL_0001:;
			}
			catch (object obj3) when (obj3 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 351;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string ValidateKeyCols()
	{
		string result = "";
		string text = "1";
		string text2 = "";
		if (Operators.ConditionalCompareObjectNotEqual(GridCSV.Rows[0].Cells[1].Value, "", TextCompare: false))
		{
			text = Conversions.ToString(GridCSV.Rows[0].Cells[1].Value);
			text2 = Conversions.ToString(GridCSV.Rows[0].Cells[0].Value);
		}
		ListViewItem listViewItem = null;
		int num = 0;
		string text3 = "";
		string text4 = "";
		bool flag = true;
		checked
		{
			foreach (ListViewItem item in CC_KeysColsListView.Items)
			{
				text3 = text + ":" + Strings.LCase(item.Text);
				flag = false;
				int num2 = GridCols.RowCount - 1;
				for (num = 0; num <= num2; num++)
				{
					object[] array;
					DataGridViewCell dataGridViewCell;
					bool[] array2;
					object obj = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array = new object[1] { (dataGridViewCell = GridCols.Rows[num].Cells[0]).Value }, null, null, array2 = new bool[1] { true });
					if (array2[0])
					{
						dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					text4 = Conversions.ToString(obj);
					if (Operators.CompareString(text4, "", TextCompare: false) != 0 && Operators.CompareString(text4, text3, TextCompare: false) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					result = "You have requested comments capture, but key column \"" + item.Text + "\" selected to uniquely identify a row on the Comments Capture Tab is not a selected column in the Top Level Report Table (ID=" + text + ", file= " + text2 + ")";
					break;
				}
			}
			return result;
		}
	}

	private void cmdMacroGuid_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		frmfilename frmfilename2 = default(frmfilename);
		while (true)
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
							goto IL_001d;
						case 4:
							goto IL_0029;
						case 5:
							goto IL_0031;
						case 6:
							goto IL_003a;
						case 7:
							goto IL_006a;
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
					num2 = 5;
					frmfilename2.ShowDialog();
					goto IL_003a;
					IL_003a:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_006a;
					IL_0029:
					num2 = 4;
					frmfilename2 = new frmfilename();
					goto IL_0031;
					IL_006a:
					num2 = 7;
					CC_GuidTxtBx.Text = Globals_Renamed.currvaluetmp;
					break;
					IL_000b:
					num2 = 2;
					Globals_Renamed.currvaluetmp = CC_GuidTxtBx.Text;
					goto IL_001d;
					IL_001d:
					num2 = 3;
					Globals_Renamed.currinputstrtmp = "GG";
					goto IL_0029;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Globals_Renamed.currvaluetmp = "";
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
	}

	private void GridCSV_KeyDown(object sender, KeyEventArgs e)
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
				int num5;
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
							goto IL_000f;
						case 4:
							goto IL_0023;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_0049;
						case 8:
							goto IL_0056;
						case 9:
							goto IL_009a;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0049:
					num2 = 6;
					e.Handled = true;
					goto end_IL_0001_3;
					IL_0056:
					num2 = 8;
					if ((columnIndex != 2 && columnIndex != 3 && columnIndex != 10 && columnIndex != 12 && columnIndex != 14 && columnIndex != 15) || (e.KeyValue != 13 && e.KeyValue != 115))
					{
						goto end_IL_0001_3;
					}
					goto IL_009a;
					IL_0035:
					num2 = 5;
					cmdDel0_Click(cmdDel0, new EventArgs());
					goto IL_0049;
					IL_009a:
					num2 = 9;
					GridCSV_Click(RuntimeHelpers.GetObjectValue(sender), new EventArgs());
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					columnIndex = GridCSV.CurrentCell.ColumnIndex;
					goto IL_0023;
					IL_0023:
					num2 = 4;
					if (e.KeyValue == 46)
					{
						goto IL_0035;
					}
					goto IL_0056;
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

	private void cmdAddCom_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		int num6 = default(int);
		int index = default(int);
		string text2 = default(string);
		bool flag = default(bool);
		string text3 = default(string);
		int num7 = default(int);
		int num8 = default(int);
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
					case 1209:
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
								goto IL_0022;
							case 7:
								goto IL_002b;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_0039;
							case 10:
								goto IL_0052;
							case 11:
								goto IL_007d;
							case 12:
								goto IL_00eb;
							case 13:
								goto IL_0108;
							case 14:
								goto IL_0116;
							case 16:
								goto IL_0130;
							case 17:
								goto IL_013f;
							case 19:
								goto IL_015c;
							case 15:
							case 18:
							case 20:
							case 21:
							case 22:
								goto IL_0185;
							case 23:
								goto IL_0194;
							case 24:
								goto IL_01a6;
							case 25:
								goto IL_01c2;
							case 26:
								goto IL_01de;
							case 27:
								goto IL_01f0;
							case 28:
								goto IL_0203;
							case 29:
								goto IL_0209;
							case 30:
								goto IL_0224;
							case 31:
								goto IL_024e;
							case 33:
							case 34:
								goto IL_0258;
							case 32:
							case 35:
								goto IL_0264;
							case 36:
								goto IL_0272;
							case 37:
							case 38:
								goto IL_028a;
							case 39:
								goto IL_02b2;
							case 40:
								goto IL_02e9;
							case 41:
								goto IL_0314;
							case 42:
								goto IL_033f;
							case 43:
								goto IL_036a;
							case 44:
								goto IL_0395;
							case 45:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 46:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0116:
						num2 = 14;
						text = "Y" + Strings.Mid(text, 2);
						goto IL_0185;
						IL_0130:
						num2 = 16;
						if (num5 == 10)
						{
							goto IL_013f;
						}
						goto IL_015c;
						IL_0108:
						num2 = 13;
						if (num5 == 1)
						{
							goto IL_0116;
						}
						goto IL_0130;
						IL_013f:
						num2 = 17;
						text = Strings.Mid(text, 1, 9) + "Y";
						goto IL_0185;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						index = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						text2 = "1:spf$comment$-";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						flag = false;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						text = "NNNNNNNNNN";
						goto IL_002b;
						IL_002b:
						num2 = 7;
						text3 = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						num5 = 0;
						goto IL_0039;
						IL_0039:
						num2 = 9;
						num7 = GridCols.RowCount - 1;
						num6 = 0;
						goto IL_018c;
						IL_018c:
						if (num6 <= num7)
						{
							goto IL_0052;
						}
						goto IL_0194;
						IL_0194:
						num2 = 23;
						num5 = Strings.InStr(text, "N");
						goto IL_01a6;
						IL_01a6:
						num2 = 24;
						GridCols.RowCount += 1;
						goto IL_01c2;
						IL_01c2:
						num2 = 25;
						MyGrid = GridCols;
						GridModule.Number_Grid(ref MyGrid);
						GridCols = MyGrid;
						goto IL_01de;
						IL_01de:
						num2 = 26;
						index = GridCols.RowCount - 1;
						goto IL_01f0;
						IL_01f0:
						num2 = 27;
						text2 += Conversions.ToString(num5);
						goto IL_0203;
						IL_0203:
						num2 = 28;
						flag = false;
						goto IL_0209;
						IL_0209:
						num2 = 29;
						num8 = ColCol.Items.Count - 1;
						num6 = 0;
						goto IL_025f;
						IL_025f:
						if (num6 <= num8)
						{
							goto IL_0224;
						}
						goto IL_0264;
						IL_0224:
						num2 = 30;
						if (Operators.CompareString(ColCol.Items[num6].ToString(), text2, TextCompare: false) == 0)
						{
							goto IL_024e;
						}
						goto IL_0258;
						IL_024e:
						num2 = 31;
						flag = true;
						goto IL_0264;
						IL_0264:
						num2 = 35;
						if (!flag)
						{
							goto IL_0272;
						}
						goto IL_028a;
						IL_0272:
						num2 = 36;
						ColCol.Items.Add(text2);
						goto IL_028a;
						IL_028a:
						num2 = 38;
						GridCols.Rows[index].Cells[0].Value = text2;
						goto IL_02b2;
						IL_02b2:
						num2 = 39;
						GridCols.Rows[index].Cells[1].Value = "Comment" + Conversions.ToString(num5);
						goto IL_02e9;
						IL_02e9:
						num2 = 40;
						GridCols.Rows[index].Cells[2].Value = "left";
						goto IL_0314;
						IL_0314:
						num2 = 41;
						GridCols.Rows[index].Cells[3].Value = "center";
						goto IL_033f;
						IL_033f:
						num2 = 42;
						GridCols.Rows[index].Cells[4].Value = "string";
						goto IL_036a;
						IL_036a:
						num2 = 43;
						GridCols.Rows[index].Cells[5].Value = "150";
						goto IL_0395;
						IL_0395:
						num2 = 44;
						GridCols.Rows[index].Cells[8].Value = "chklist";
						break;
						IL_0258:
						num2 = 34;
						num6++;
						goto IL_025f;
						IL_0052:
						num2 = 10;
						text3 = Conversions.ToString(GridCols.Rows[num6].Cells[0].Value);
						goto IL_007d;
						IL_007d:
						num2 = 11;
						if (Operators.CompareString(Strings.Mid(text3 + "                ", 1, Strings.Len("1:spf$comment$-")), text2, TextCompare: false) == 0 && Versioned.IsNumeric(Strings.Mid(text3, Strings.Len("1:spf$comment$-") + 1)) && Conversions.ToInteger(Strings.Mid(text3, Strings.Len("1:spf$comment$-") + 1)) <= 10)
						{
							goto IL_00eb;
						}
						goto IL_0185;
						IL_015c:
						num2 = 19;
						text = Strings.Mid(text, 1, num5 - 1) + "Y" + Strings.Mid(text, num5 + 1);
						goto IL_0185;
						IL_0185:
						num2 = 22;
						num6++;
						goto IL_018c;
						IL_00eb:
						num2 = 12;
						num5 = Conversions.ToInteger(Strings.Mid(text3, Strings.Len("1:spf$comment$-") + 1));
						goto IL_0108;
						end_IL_0001_2:
						break;
					}
					num2 = 45;
					GridCols.Rows[index].Cells[13].Value = "textbox";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1209;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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

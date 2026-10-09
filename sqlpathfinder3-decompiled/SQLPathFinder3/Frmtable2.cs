using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using SQLPathFinder3.My;
using TenTec.Windows.iGridLib;
using TenTec.Windows.iGridLib.Filtering;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class Frmtable2 : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType2")]
	private RadioButton _OptDBType2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType3")]
	private RadioButton _OptDBType3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType4")]
	private RadioButton _OptDBType4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType1")]
	private RadioButton _OptDBType1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType0")]
	private RadioButton _OptDBType0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdClose")]
	private Button _CmdClose;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdaddtable")]
	private Button _cmdaddtable;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridTbl")]
	private iGrid _GridTbl;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbFav")]
	private ComboBox _cmbFav;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp2")]
	private ToolStripMenuItem _mnuHelp2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSec")]
	private ToolStripMenuItem _mnuSec;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp")]
	private ToolStripMenuItem _mnuHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuJET")]
	private ToolStripMenuItem _mnuJET;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveFilters")]
	private ToolStripMenuItem _mnuSaveFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuLoadFilters")]
	private ToolStripMenuItem _mnuLoadFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRenaFilters")]
	private ToolStripMenuItem _mnuRenaFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuDelFilters")]
	private ToolStripMenuItem _mnuDelFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdSaveFilter")]
	private Button _cmdSaveFilter;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClearFilters")]
	private Button _cmdClearFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearFilters")]
	private ToolStripMenuItem _mnuClearFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnushowMetaData")]
	private ToolStripMenuItem _mnushowMetaData;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdLoad")]
	private Button _cmdLoad;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuExit")]
	private ToolStripMenuItem _mnuExit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptDBType6")]
	private RadioButton _OptDBType6;

	private string ll_DBType;

	private short ll_ObjectType;

	private int fLastOpt;

	private bool filterdefault;

	private const string lpAppName = "Query Defaults";

	private readonly string f_DefFFile;

	private bool f_IsColHdr;

	private readonly string ExceptionTableList;

	public virtual RadioButton OptDBType2
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType2;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType2 = value;
			radioButton = _OptDBType2;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	public virtual RadioButton OptDBType3
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType3;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType3 = value;
			radioButton = _OptDBType3;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	public virtual RadioButton OptDBType4
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType4;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType4 = value;
			radioButton = _OptDBType4;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	public virtual RadioButton OptDBType1
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType1;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType1 = value;
			radioButton = _OptDBType1;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	public virtual RadioButton OptDBType0
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType0;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType0 = value;
			radioButton = _OptDBType0;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("FrmOptDBType")]
	public virtual GroupBox FrmOptDBType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button CmdClose
	{
		[CompilerGenerated]
		get
		{
			return _CmdClose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
			Button button = _CmdClose;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdClose = value;
			button = _CmdClose;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdaddtable
	{
		[CompilerGenerated]
		get
		{
			return _cmdaddtable;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdaddtable_Click;
			Button button = _cmdaddtable;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdaddtable = value;
			button = _cmdaddtable;
			if (button != null)
			{
				button.Click += value2;
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

	[field: AccessedThroughProperty("mnuHelp1")]
	internal virtual ToolStripMenuItem mnuHelp1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual iGrid GridTbl
	{
		[CompilerGenerated]
		get
		{
			return _GridTbl;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			MouseEventHandler value2 = GridTbl_MouseDoubleClick;
			KeyPressEventHandler value3 = GridTbl_KeyPress;
			KeyEventHandler value4 = GridTbl_KeyDown;
			iGColDividerDoubleClickEventHandler val = new iGColDividerDoubleClickEventHandler(GridTbl_ColDividerDoubleClick);
			iGColWidthEventHandler val2 = new iGColWidthEventHandler(GridTbl_ColWidthEndChange);
			iGrid val3 = _GridTbl;
			if (val3 != null)
			{
				((Control)(object)val3).MouseDoubleClick -= value2;
				((Control)(object)val3).KeyPress -= value3;
				((Control)(object)val3).KeyDown -= value4;
				val3.ColDividerDoubleClick -= val;
				val3.ColWidthEndChange -= val2;
			}
			_GridTbl = value;
			val3 = _GridTbl;
			if (val3 != null)
			{
				((Control)(object)val3).MouseDoubleClick += value2;
				((Control)(object)val3).KeyPress += value3;
				((Control)(object)val3).KeyDown += value4;
				val3.ColDividerDoubleClick += val;
				val3.ColWidthEndChange += val2;
			}
		}
	}

	[field: AccessedThroughProperty("TblGridCol0CellStyle")]
	internal virtual iGCellStyle TblGridCol0CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol0ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol0ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol9CellStyle")]
	internal virtual iGCellStyle TblGridCol9CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol9ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol9ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol1CellStyle")]
	internal virtual iGCellStyle TblGridCol1CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol1ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol1ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol2CellStyle")]
	internal virtual iGCellStyle TblGridCol2CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol2ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol2ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol3CellStyle")]
	internal virtual iGCellStyle TblGridCol3CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol3ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol3ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol4CellStyle")]
	internal virtual iGCellStyle TblGridCol4CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol4ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol4ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol5CellStyle")]
	internal virtual iGCellStyle TblGridCol5CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol5ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol5ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol7CellStyle")]
	internal virtual iGCellStyle TblGridCol7CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol7ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol7ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol6CellStyle")]
	internal virtual iGCellStyle TblGridCol6CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol6ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol6ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol8CellStyle")]
	internal virtual iGCellStyle TblGridCol8CellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TblGridCol8ColHdrStyle")]
	internal virtual iGColHdrStyle TblGridCol8ColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GridF")]
	internal virtual iGAutoFilterManager GridF
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuHeaders")]
	internal virtual ToolStripMenuItem mnuHeaders
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ComboBox cmbFav
	{
		[CompilerGenerated]
		get
		{
			return _cmbFav;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbFav_SelectedIndexChanged;
			ComboBox comboBox = _cmbFav;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			_cmbFav = value;
			comboBox = _cmbFav;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuHelp2
	{
		[CompilerGenerated]
		get
		{
			return _mnuHelp2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelp2_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHelp2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHelp2 = value;
			toolStripMenuItem = _mnuHelp2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSec
	{
		[CompilerGenerated]
		get
		{
			return _mnuSec;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSec_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSec;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSec = value;
			toolStripMenuItem = _mnuSec;
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
			EventHandler value2 = mnuhelp_Click;
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

	internal virtual ToolStripMenuItem mnuJET
	{
		[CompilerGenerated]
		get
		{
			return _mnuJET;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuJET_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuJET;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuJET = value;
			toolStripMenuItem = _mnuJET;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuManageFilters")]
	internal virtual ToolStripMenuItem mnuManageFilters
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSaveFilters
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveFilter_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveFilters = value;
			toolStripMenuItem = _mnuSaveFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuLoadFilters
	{
		[CompilerGenerated]
		get
		{
			return _mnuLoadFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLoadFilter_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuLoadFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuLoadFilters = value;
			toolStripMenuItem = _mnuLoadFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator4")]
	internal virtual ToolStripSeparator ToolStripSeparator4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuRenaFilters
	{
		[CompilerGenerated]
		get
		{
			return _mnuRenaFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRenaFilters_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRenaFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRenaFilters = value;
			toolStripMenuItem = _mnuRenaFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator5")]
	internal virtual ToolStripSeparator ToolStripSeparator5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuDelFilters
	{
		[CompilerGenerated]
		get
		{
			return _mnuDelFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRenaFilters_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuDelFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuDelFilters = value;
			toolStripMenuItem = _mnuDelFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual Button cmdSaveFilter
	{
		[CompilerGenerated]
		get
		{
			return _cmdSaveFilter;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveFilter_Click;
			Button button = _cmdSaveFilter;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdSaveFilter = value;
			button = _cmdSaveFilter;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdClearFilters
	{
		[CompilerGenerated]
		get
		{
			return _cmdClearFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClearFilters_Click;
			Button button = _cmdClearFilters;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdClearFilters = value;
			button = _cmdClearFilters;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuClearFilters
	{
		[CompilerGenerated]
		get
		{
			return _mnuClearFilters;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClearFilters_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClearFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClearFilters = value;
			toolStripMenuItem = _mnuClearFilters;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnushowMetaData
	{
		[CompilerGenerated]
		get
		{
			return _mnushowMetaData;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnushowMetaData_Click;
			ToolStripMenuItem toolStripMenuItem = _mnushowMetaData;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnushowMetaData = value;
			toolStripMenuItem = _mnushowMetaData;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual Button cmdLoad
	{
		[CompilerGenerated]
		get
		{
			return _cmdLoad;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLoadFilter_Click;
			Button button = _cmdLoad;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdLoad = value;
			button = _cmdLoad;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("optDBType5")]
	internal virtual RadioButton optDBType5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuFile")]
	internal virtual ToolStripMenuItem mnuFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuExit
	{
		[CompilerGenerated]
		get
		{
			return _mnuExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuExit = value;
			toolStripMenuItem = _mnuExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual RadioButton OptDBType6
	{
		[CompilerGenerated]
		get
		{
			return _OptDBType6;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptObjectTypeNormal_CheckedChanged;
			RadioButton radioButton = _OptDBType6;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptDBType6 = value;
			radioButton = _OptDBType6;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[DebuggerNonUserCode]
	public Frmtable2()
	{
		base.FormClosed += frmtable_FormClosed;
		base.Load += frmtable_Load;
		base.Resize += frmtable_Resize;
		base.Closing += Frmtable2_Closing;
		ll_DBType = "0";
		ll_ObjectType = 2;
		fLastOpt = -1;
		filterdefault = false;
		f_DefFFile = Globals_Renamed.gQueryDir + "SPF_Default.tablefilter";
		f_IsColHdr = false;
		ExceptionTableList = "Test_or_Sort_Unit_or_Die_Test_Results_HBASE_MIDASQ";
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		this.TblGridCol0CellStyle = new iGCellStyle(true);
		this.TblGridCol0ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol9CellStyle = new iGCellStyle(true);
		this.TblGridCol9ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol1CellStyle = new iGCellStyle(true);
		this.TblGridCol1ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol2CellStyle = new iGCellStyle(true);
		this.TblGridCol2ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol3CellStyle = new iGCellStyle(true);
		this.TblGridCol3ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol4CellStyle = new iGCellStyle(true);
		this.TblGridCol4ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol5CellStyle = new iGCellStyle(true);
		this.TblGridCol5ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol7CellStyle = new iGCellStyle(true);
		this.TblGridCol7ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol6CellStyle = new iGCellStyle(true);
		this.TblGridCol6ColHdrStyle = new iGColHdrStyle(true);
		this.TblGridCol8CellStyle = new iGCellStyle(true);
		this.TblGridCol8ColHdrStyle = new iGColHdrStyle(true);
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.CmdClose = new System.Windows.Forms.Button();
		this.cmdaddtable = new System.Windows.Forms.Button();
		this.cmdClearFilters = new System.Windows.Forms.Button();
		this.cmdSaveFilter = new System.Windows.Forms.Button();
		this.cmbFav = new System.Windows.Forms.ComboBox();
		this.cmdLoad = new System.Windows.Forms.Button();
		this.FrmOptDBType = new System.Windows.Forms.GroupBox();
		this.OptDBType6 = new System.Windows.Forms.RadioButton();
		this.optDBType5 = new System.Windows.Forms.RadioButton();
		this.OptDBType2 = new System.Windows.Forms.RadioButton();
		this.OptDBType3 = new System.Windows.Forms.RadioButton();
		this.OptDBType4 = new System.Windows.Forms.RadioButton();
		this.OptDBType1 = new System.Windows.Forms.RadioButton();
		this.OptDBType0 = new System.Windows.Forms.RadioButton();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHeaders = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuJET = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuManageFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClearFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuSaveFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuLoadFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRenaFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuDelFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.mnushowMetaData = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSec = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.GridTbl = new iGrid();
		this.GridF = new iGAutoFilterManager();
		this.FrmOptDBType.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridTbl).BeginInit();
		base.SuspendLayout();
		((iGStyleBase)this.TblGridCol0CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		((iGStyleBase)this.TblGridCol9CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		((iGStyleBase)this.TblGridCol1CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		((iGStyleBase)this.TblGridCol2CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		((iGStyleBase)this.TblGridCol3CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		this.CmdClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdClose.BackColor = System.Drawing.SystemColors.Control;
		this.CmdClose.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdClose.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdClose.Location = new System.Drawing.Point(803, 100);
		this.CmdClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdClose.Name = "CmdClose";
		this.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdClose.Size = new System.Drawing.Size(67, 40);
		this.CmdClose.TabIndex = 8;
		this.CmdClose.Text = "Close";
		this.ToolTip1.SetToolTip(this.CmdClose, "Close Form");
		this.CmdClose.UseVisualStyleBackColor = false;
		this.cmdaddtable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdaddtable.BackColor = System.Drawing.SystemColors.Control;
		this.cmdaddtable.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdaddtable.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdaddtable.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdaddtable.Location = new System.Drawing.Point(803, 48);
		this.cmdaddtable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdaddtable.Name = "cmdaddtable";
		this.cmdaddtable.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdaddtable.Size = new System.Drawing.Size(67, 40);
		this.cmdaddtable.TabIndex = 7;
		this.cmdaddtable.Text = "Add";
		this.ToolTip1.SetToolTip(this.cmdaddtable, "Add Selected View or Utility");
		this.cmdaddtable.UseVisualStyleBackColor = false;
		this.cmdClearFilters.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdClearFilters.Location = new System.Drawing.Point(6, 67);
		this.cmdClearFilters.Name = "cmdClearFilters";
		this.cmdClearFilters.Size = new System.Drawing.Size(77, 24);
		this.cmdClearFilters.TabIndex = 9;
		this.cmdClearFilters.Text = "Clear";
		this.ToolTip1.SetToolTip(this.cmdClearFilters, "Clear Filters");
		this.cmdClearFilters.UseVisualStyleBackColor = true;
		this.cmdSaveFilter.Location = new System.Drawing.Point(96, 67);
		this.cmdSaveFilter.Name = "cmdSaveFilter";
		this.cmdSaveFilter.Size = new System.Drawing.Size(77, 24);
		this.cmdSaveFilter.TabIndex = 10;
		this.cmdSaveFilter.Text = "Save";
		this.ToolTip1.SetToolTip(this.cmdSaveFilter, "Save favorite Filter Conditions to disk");
		this.cmdSaveFilter.UseVisualStyleBackColor = true;
		this.cmbFav.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFav.FormattingEnabled = true;
		this.cmbFav.Items.AddRange(new object[1] { "Last" });
		this.cmbFav.Location = new System.Drawing.Point(276, 68);
		this.cmbFav.Name = "cmbFav";
		this.cmbFav.Size = new System.Drawing.Size(372, 24);
		this.cmbFav.TabIndex = 12;
		this.ToolTip1.SetToolTip(this.cmbFav, "Choose a Favorite Filter");
		this.cmdLoad.Location = new System.Drawing.Point(186, 67);
		this.cmdLoad.Name = "cmdLoad";
		this.cmdLoad.Size = new System.Drawing.Size(77, 24);
		this.cmdLoad.TabIndex = 11;
		this.cmdLoad.Text = "Load";
		this.ToolTip1.SetToolTip(this.cmdLoad, "Load a Filter or choose a favorite at right");
		this.cmdLoad.UseVisualStyleBackColor = true;
		this.FrmOptDBType.BackColor = System.Drawing.SystemColors.Control;
		this.FrmOptDBType.Controls.Add(this.OptDBType6);
		this.FrmOptDBType.Controls.Add(this.optDBType5);
		this.FrmOptDBType.Controls.Add(this.cmdLoad);
		this.FrmOptDBType.Controls.Add(this.cmdSaveFilter);
		this.FrmOptDBType.Controls.Add(this.cmbFav);
		this.FrmOptDBType.Controls.Add(this.cmdClearFilters);
		this.FrmOptDBType.Controls.Add(this.OptDBType2);
		this.FrmOptDBType.Controls.Add(this.OptDBType3);
		this.FrmOptDBType.Controls.Add(this.OptDBType4);
		this.FrmOptDBType.Controls.Add(this.OptDBType1);
		this.FrmOptDBType.Controls.Add(this.OptDBType0);
		this.FrmOptDBType.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FrmOptDBType.ForeColor = System.Drawing.SystemColors.ControlText;
		this.FrmOptDBType.Location = new System.Drawing.Point(5, 48);
		this.FrmOptDBType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.FrmOptDBType.Name = "FrmOptDBType";
		this.FrmOptDBType.Padding = new System.Windows.Forms.Padding(0);
		this.FrmOptDBType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.FrmOptDBType.Size = new System.Drawing.Size(781, 111);
		this.FrmOptDBType.TabIndex = 1;
		this.FrmOptDBType.TabStop = false;
		this.OptDBType6.AutoSize = true;
		this.OptDBType6.Location = new System.Drawing.Point(396, 28);
		this.OptDBType6.Name = "OptDBType6";
		this.OptDBType6.Size = new System.Drawing.Size(176, 20);
		this.OptDBType6.TabIndex = 13;
		this.OptDBType6.TabStop = true;
		this.OptDBType6.Text = "Parquet/CSV (DuckDB)";
		this.OptDBType6.UseVisualStyleBackColor = true;
		this.optDBType5.AutoSize = true;
		this.optDBType5.Location = new System.Drawing.Point(96, 28);
		this.optDBType5.Name = "optDBType5";
		this.optDBType5.Size = new System.Drawing.Size(79, 20);
		this.optDBType5.TabIndex = 2;
		this.optDBType5.TabStop = true;
		this.optDBType5.Text = "Queries";
		this.optDBType5.UseVisualStyleBackColor = true;
		this.OptDBType2.AutoSize = true;
		this.OptDBType2.BackColor = System.Drawing.SystemColors.Control;
		this.OptDBType2.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptDBType2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptDBType2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptDBType2.Location = new System.Drawing.Point(286, 28);
		this.OptDBType2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.OptDBType2.Name = "OptDBType2";
		this.OptDBType2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptDBType2.Size = new System.Drawing.Size(114, 20);
		this.OptDBType2.TabIndex = 4;
		this.OptDBType2.TabStop = true;
		this.OptDBType2.Text = "CSV (SQLite)";
		this.OptDBType2.UseVisualStyleBackColor = false;
		this.OptDBType3.AutoSize = true;
		this.OptDBType3.BackColor = System.Drawing.SystemColors.Control;
		this.OptDBType3.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptDBType3.Enabled = false;
		this.OptDBType3.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptDBType3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptDBType3.Location = new System.Drawing.Point(550, 28);
		this.OptDBType3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.OptDBType3.Name = "OptDBType3";
		this.OptDBType3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptDBType3.Size = new System.Drawing.Size(95, 20);
		this.OptDBType3.TabIndex = 5;
		this.OptDBType3.TabStop = true;
		this.OptDBType3.Tag = "";
		this.OptDBType3.Text = "CSV (JET)";
		this.OptDBType3.UseVisualStyleBackColor = false;
		this.OptDBType4.AutoSize = true;
		this.OptDBType4.BackColor = System.Drawing.SystemColors.Control;
		this.OptDBType4.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptDBType4.Enabled = false;
		this.OptDBType4.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptDBType4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptDBType4.Location = new System.Drawing.Point(650, 28);
		this.OptDBType4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.OptDBType4.Name = "OptDBType4";
		this.OptDBType4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptDBType4.Size = new System.Drawing.Size(102, 20);
		this.OptDBType4.TabIndex = 6;
		this.OptDBType4.TabStop = true;
		this.OptDBType4.Tag = "";
		this.OptDBType4.Text = "Inline Views";
		this.OptDBType4.UseVisualStyleBackColor = false;
		this.OptDBType1.AutoSize = true;
		this.OptDBType1.BackColor = System.Drawing.SystemColors.Control;
		this.OptDBType1.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptDBType1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptDBType1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptDBType1.Location = new System.Drawing.Point(196, 28);
		this.OptDBType1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.OptDBType1.Name = "OptDBType1";
		this.OptDBType1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptDBType1.Size = new System.Drawing.Size(73, 20);
		this.OptDBType1.TabIndex = 3;
		this.OptDBType1.TabStop = true;
		this.OptDBType1.Text = "Utilities";
		this.OptDBType1.UseVisualStyleBackColor = false;
		this.OptDBType0.AutoSize = true;
		this.OptDBType0.BackColor = System.Drawing.SystemColors.Control;
		this.OptDBType0.Checked = true;
		this.OptDBType0.Cursor = System.Windows.Forms.Cursors.Default;
		this.OptDBType0.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OptDBType0.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptDBType0.Location = new System.Drawing.Point(6, 28);
		this.OptDBType0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.OptDBType0.Name = "OptDBType0";
		this.OptDBType0.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.OptDBType0.Size = new System.Drawing.Size(65, 20);
		this.OptDBType0.TabIndex = 1;
		this.OptDBType0.TabStop = true;
		this.OptDBType0.Text = "Views";
		this.OptDBType0.UseVisualStyleBackColor = false;
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuFile, this.OptionsToolStripMenuItem, this.mnuManageFilters, this.mnushowMetaData, this.mnuHelp1 });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Size = new System.Drawing.Size(882, 28);
		this.MenuStrip1.TabIndex = 39;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuExit });
		this.mnuFile.Name = "mnuFile";
		this.mnuFile.Size = new System.Drawing.Size(46, 24);
		this.mnuFile.Text = "&File";
		this.mnuExit.Name = "mnuExit";
		this.mnuExit.Size = new System.Drawing.Size(128, 26);
		this.mnuExit.Text = "Close";
		this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuHeaders, this.ToolStripSeparator2, this.mnuJET });
		this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
		this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
		this.OptionsToolStripMenuItem.Text = "&Options";
		this.mnuHeaders.Checked = true;
		this.mnuHeaders.CheckOnClick = true;
		this.mnuHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuHeaders.Name = "mnuHeaders";
		this.mnuHeaders.Size = new System.Drawing.Size(448, 26);
		this.mnuHeaders.Text = "Make Column Headers Unique when Inserting a Query";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(445, 6);
		this.mnuJET.CheckOnClick = true;
		this.mnuJET.Name = "mnuJET";
		this.mnuJET.Size = new System.Drawing.Size(448, 26);
		this.mnuJET.Text = "Turn on CSV (JET) and Inline Views";
		this.mnuManageFilters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.mnuClearFilters, this.ToolStripSeparator3, this.mnuSaveFilters, this.mnuLoadFilters, this.ToolStripSeparator4, this.mnuRenaFilters, this.ToolStripSeparator5, this.mnuDelFilters });
		this.mnuManageFilters.Name = "mnuManageFilters";
		this.mnuManageFilters.Size = new System.Drawing.Size(120, 24);
		this.mnuManageFilters.Text = "&Manage Filters";
		this.mnuClearFilters.Name = "mnuClearFilters";
		this.mnuClearFilters.Size = new System.Drawing.Size(307, 26);
		this.mnuClearFilters.Text = "Clear Filters";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(304, 6);
		this.mnuSaveFilters.Name = "mnuSaveFilters";
		this.mnuSaveFilters.Size = new System.Drawing.Size(307, 26);
		this.mnuSaveFilters.Text = "Save Filter Conditions to Disk";
		this.mnuLoadFilters.Name = "mnuLoadFilters";
		this.mnuLoadFilters.Size = new System.Drawing.Size(307, 26);
		this.mnuLoadFilters.Text = "Load Filter Conditions From Disk";
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(304, 6);
		this.mnuRenaFilters.Name = "mnuRenaFilters";
		this.mnuRenaFilters.Size = new System.Drawing.Size(307, 26);
		this.mnuRenaFilters.Tag = "RENAME";
		this.mnuRenaFilters.Text = "Rename Current Filter";
		this.ToolStripSeparator5.Name = "ToolStripSeparator5";
		this.ToolStripSeparator5.Size = new System.Drawing.Size(304, 6);
		this.mnuDelFilters.Name = "mnuDelFilters";
		this.mnuDelFilters.Size = new System.Drawing.Size(307, 26);
		this.mnuDelFilters.Tag = "DELETE";
		this.mnuDelFilters.Text = "Delete Current Filter";
		this.mnushowMetaData.Name = "mnushowMetaData";
		this.mnushowMetaData.Size = new System.Drawing.Size(142, 24);
		this.mnushowMetaData.Text = "&Column Metadata";
		this.mnuHelp1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuHelp2, this.mnuSec, this.mnuHelp });
		this.mnuHelp1.Name = "mnuHelp1";
		this.mnuHelp1.Size = new System.Drawing.Size(55, 24);
		this.mnuHelp1.Text = "&Help";
		this.mnuHelp2.Name = "mnuHelp2";
		this.mnuHelp2.Size = new System.Drawing.Size(283, 26);
		this.mnuHelp2.Text = "Help on Form";
		this.mnuSec.Name = "mnuSec";
		this.mnuSec.Size = new System.Drawing.Size(283, 26);
		this.mnuSec.Text = "Help on Security/Permissions";
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.Size = new System.Drawing.Size(283, 26);
		this.mnuHelp.Text = "Help on Selected Table";
		((System.Windows.Forms.Control)(object)this.GridTbl).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridTbl.CurCellBackColor = System.Drawing.SystemColors.Highlight;
		this.GridTbl.CurCellBackColorNoFocus = System.Drawing.SystemColors.Highlight;
		this.GridTbl.CurCellForeColor = System.Drawing.Color.White;
		this.GridTbl.CurCellForeColorNoFocus = System.Drawing.Color.White;
		this.GridTbl.DefaultAutoGroupRow.Height = 21;
		this.GridTbl.DefaultRow.Height = 21;
		this.GridTbl.DefaultRow.NormalCellHeight = 21;
		((System.Windows.Forms.Control)(object)this.GridTbl).Font = new System.Drawing.Font("Arial", 8.25f);
		this.GridTbl.Header.Height = 22;
		((System.Windows.Forms.Control)(object)this.GridTbl).Location = new System.Drawing.Point(5, 165);
		((System.Windows.Forms.Control)(object)this.GridTbl).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.GridTbl).Name = "GridTbl";
		this.GridTbl.ReadOnly = true;
		this.GridTbl.RowResizeMode = (iGRowResizeMode)0;
		((System.Windows.Forms.Control)(object)this.GridTbl).Size = new System.Drawing.Size(871, 428);
		((System.Windows.Forms.Control)(object)this.GridTbl).TabIndex = 0;
		this.GridF.Grid = this.GridTbl;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 19f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(882, 598);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.GridTbl);
		base.Controls.Add(this.CmdClose);
		base.Controls.Add(this.cmdaddtable);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.FrmOptDBType);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.FromArgb(0, 0, 128);
		base.Location = new System.Drawing.Point(443, 117);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "Frmtable2";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Add a View or Utility";
		this.FrmOptDBType.ResumeLayout(false);
		this.FrmOptDBType.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridTbl).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private string Get_Filt_Name(string CurrentFile)
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
						case 3:
							goto IL_0013;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_0029;
						case 6:
							goto IL_0036;
						case 7:
							goto IL_0044;
						case 8:
							goto IL_0055;
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
					IL_0036:
					num2 = 6;
					CurrentFile = Strings.Mid(CurrentFile, checked(num5 + 1));
					goto IL_0044;
					IL_0044:
					num2 = 7;
					num5 = Strings.InStrRev(CurrentFile, ".tablefilter");
					goto IL_0055;
					IL_0029:
					num2 = 5;
					if (num5 != 0)
					{
						goto IL_0036;
					}
					goto IL_0044;
					IL_0055:
					num2 = 8;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = Strings.InStrRev(CurrentFile, "\\");
					goto IL_0029;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				result = Strings.Mid(CurrentFile, 1, checked(num5 - 1));
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
		return result;
	}

	private void Load_CmbFav(string MyDef, string inMyPrefix)
	{
		string text = "";
		int num = 0;
		int num2 = 0;
		string text2 = "";
		string text3 = "";
		checked
		{
			try
			{
				cmbFav.Items.Clear();
				cmbFav.Items.Add("All");
				cmbFav.Items.Add("Last");
				num2 = 1;
				do
				{
					ReadOnlyCollection<string> files;
					if (num2 == 1)
					{
						files = MyProject.Computer.FileSystem.GetFiles(Globals_Renamed.MySchemaDir, SearchOption.SearchTopLevelOnly, "*.tablefilter");
						text3 = "S:";
					}
					else
					{
						files = MyProject.Computer.FileSystem.GetFiles(Globals_Renamed.gQueryDir, SearchOption.SearchTopLevelOnly, "*.tablefilter");
						text3 = "P:";
					}
					foreach (string item in files)
					{
						text2 = Get_Filt_Name(item);
						if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text2), "SPF_DEFAULT", TextCompare: false) != 0)
						{
							cmbFav.Items.Add(text3 + Strings.Trim(text2));
						}
					}
					num2++;
				}
				while (num2 <= 2);
				cmbFav.SelectedIndex = 1;
				int num3 = cmbFav.Items.Count - 1;
				for (num2 = 0; num2 <= num3; num2++)
				{
					string left = inMyPrefix + Strings.UCase(MyDef);
					object[] array;
					ComboBox.ObjectCollection items;
					int index;
					bool[] array2;
					object right = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (items = cmbFav.Items)[index = num2] }, null, null, array2 = new bool[1] { true });
					if (array2[0])
					{
						items[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (Operators.ConditionalCompareObjectEqual(left, right, TextCompare: false))
					{
						cmbFav.SelectedIndex = num2;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error loading Filter Favorites." + ex2.Message, MsgBoxStyle.Exclamation, "Error");
				ProjectData.ClearProjectError();
			}
			finally
			{
				ReadOnlyCollection<string> files = null;
				Cursor.Current = Cursors.Default;
			}
		}
	}

	private void GetCSVs(string MyExt)
	{
		string text = "";
		string text2 = "";
		short num = 0;
		short num2 = 0;
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		checked
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				MyExt = Strings.UCase(MyExt);
				switch (MyExt)
				{
				case "CSV1":
					text3 = "TAB";
					text4 = "PLUS";
					text5 = "CSV";
					text6 = "ASC";
					text7 = "CSV";
					break;
				case "CSV2":
					text3 = "TAB";
					text4 = "PLUS";
					text5 = "PLUS";
					text6 = "ASC";
					text7 = "SDB";
					break;
				case "DUCKDB":
					text3 = "PARQUET";
					text4 = "CSV";
					text5 = "TAB";
					text6 = "TAB";
					text7 = "TAB";
					break;
				default:
					text3 = MyExt;
					text4 = MyExt;
					text5 = MyExt;
					text6 = MyExt;
					text7 = MyExt;
					break;
				}
				if (LikeOperator.LikeString(MyExt, "CSV*", CompareMethod.Binary))
				{
					MyExt = "CSV";
				}
				ReadOnlyCollection<string> files = MyProject.Computer.FileSystem.GetFiles(Globals_Renamed.MyPCDir, SearchOption.SearchTopLevelOnly, "*.*");
				foreach (string item in files)
				{
					text = item;
					num = (short)Strings.InStrRev(text, ".");
					if (num == 0)
					{
						continue;
					}
					text2 = Strings.Mid(text, num + 1);
					if (Operators.CompareString(Strings.UCase(text2), MyExt, TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), text3, TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), text4, TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), text5, TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), text6, TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), text7, TextCompare: false) == 0)
					{
						num2 = (short)Strings.InStrRev(text, "\\");
						if (num2 != 0)
						{
							text = Strings.Mid(text, num2 + 1);
						}
						iGRowCollection rows;
						(rows = GridTbl.Rows).Count = rows.Count + 1;
						GridTbl.Cells[GridTbl.Rows.Count - 1, 0].Value = text;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error loading " + MyExt + " Data: " + text + ". Contact Support" + ex2.Message, MsgBoxStyle.Exclamation, "Load Error");
				ProjectData.ClearProjectError();
			}
			finally
			{
				ReadOnlyCollection<string> files = null;
				Cursor.Current = Cursors.Default;
			}
		}
	}

	public void Set_Controls(bool MyTF, int MyOpt = 0)
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
				case 113:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					mnuManageFilters.Visible = MyTF;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					cmdLoad.Visible = MyTF;
					goto IL_0029;
					IL_0029:
					num2 = 4;
					cmbFav.Visible = MyTF;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				cmdSaveFilter.Visible = MyTF;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 113;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Load_Table_List()
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
						errsource = "frmtable - Load_Table_List";
						string text = "";
						int num3 = 0;
						int num4 = -1;
						string text2 = "VIEW";
						GridTbl.BeginUpdate();
						if (fLastOpt == 0)
						{
							GridF.SaveFilterToMemory("filterdefault");
							filterdefault = true;
						}
						GridTbl.Rows.Clear();
						if (OptDBType2.Checked || OptDBType3.Checked || OptDBType4.Checked || OptDBType6.Checked)
						{
							Set_Controls(MyTF: false);
							if (fLastOpt != 2)
							{
								GridTbl.Cols.Clear();
								GridTbl.Cols.Add("File Name");
								GridTbl.Cols[0].Width = ((Control)(object)GridTbl).Width - 20;
								((iGStyleBase)GridTbl.Cols[0].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
							}
							fLastOpt = 2;
							if (OptDBType2.Checked)
							{
								text = "CSV1";
								iGRowCollection rows;
								(rows = GridTbl.Rows).Count = rows.Count + 1;
								GridTbl.Cells[0, 0].Value = "[Browse]";
							}
							else if (OptDBType6.Checked)
							{
								text = "DUCKDB";
							}
							else if (OptDBType3.Checked)
							{
								text = "CSV2";
							}
							else
							{
								text = "ILV";
								iGRowCollection rows;
								(rows = GridTbl.Rows).Count = rows.Count + 1;
								GridTbl.Cells[0, 0].Value = " [Shared Inline Views]";
							}
							GetCSVs(text);
						}
						else if (OptDBType1.Checked)
						{
							Set_Controls(MyTF: false);
							if (fLastOpt != 1)
							{
								GridTbl.Cols.Clear();
								GridTbl.Cols.Add("Utility");
								((iGStyleBase)GridTbl.Cols[0].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols.Add("Description");
								((iGStyleBase)GridTbl.Cols[1].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols.Add("Type");
								((iGStyleBase)GridTbl.Cols[2].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
							}
							fLastOpt = 1;
							int maxTables = Globals_Renamed.MaxTables;
							for (num3 = 0; num3 <= maxTables; num3++)
							{
								if (Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num3].ObjectType), "UTILITIES", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num3].Show), "Y", TextCompare: false) == 0)
								{
									num4++;
									iGRowCollection rows;
									(rows = GridTbl.Rows).Count = rows.Count + 1;
									GridTbl.Cells[num4, 0].Value = Globals_Renamed.Tables[num3].Display;
									GridTbl.Cells[num4, 1].Value = Globals_Renamed.Tables[num3].Description;
									GridTbl.Cells[num4, 2].Value = Globals_Renamed.Tables[num3].lbl1;
								}
							}
						}
						else if (OptDBType0.Checked || optDBType5.Checked)
						{
							Set_Controls(MyTF: true);
							if (fLastOpt != 0)
							{
								GridTbl.Cols.Clear();
								GridTbl.Cols.Add("View");
								((iGStyleBase)GridTbl.Cols[0].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols[0].Width = 210;
								GridTbl.Cols.Add("Description");
								((iGStyleBase)GridTbl.Cols[1].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols[1].Width = 220;
								GridTbl.Cols.Add("Domain");
								((iGStyleBase)GridTbl.Cols[2].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols[2].Width = 88;
								GridTbl.Cols.Add("DB");
								((iGStyleBase)GridTbl.Cols[3].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
								GridTbl.Cols[3].Width = 88;
								GridTbl.Cols.Add("AT");
								GridTbl.Cols[4].Width = 47;
								GridTbl.Cols.Add("Board");
								GridTbl.Cols[5].Width = 63;
								GridTbl.Cols.Add("CS");
								GridTbl.Cols[6].Width = 47;
								GridTbl.Cols.Add("Fab");
								GridTbl.Cols[7].Width = 51;
								GridTbl.Cols.Add("IMO");
								GridTbl.Cols[8].Width = 51;
								GridTbl.Cols.Add("SPTD");
								GridTbl.Cols[9].Width = 61;
								GridTbl.Cols.Add("Wla");
								GridTbl.Cols[10].Width = 53;
							}
							if (OptDBType0.Checked)
							{
								Load_Views2(num4, "VIEW");
							}
							else
							{
								Load_Views2(num4, "QUERY");
							}
							if (filterdefault)
							{
								GridF.RestoreFilterFromMemory("filterdefault");
							}
							fLastOpt = 0;
						}
						if (GridTbl.Rows.Count > 0)
						{
							GridTbl.SortObject.Clear();
							GridTbl.SortObject.Add(0, (iGSortOrder)1);
							GridTbl.Sort();
							GridF.Grid = GridTbl;
							GridTbl.CurCell = GridTbl.Cells[0, 0];
						}
						Form_Resize();
						GridTbl.Rows.AutoHeight();
						GridTbl.EndUpdate();
						goto end_IL_0001;
					}
					case 2081:
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
					goto IL_0857;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2081;
				continue;
			}
			break;
			IL_0857:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdaddtable_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		bool toBeUnique = default(bool);
		bool flag2 = default(bool);
		string myOptDB = default(string);
		string MyNewAlias = default(string);
		bool flag3 = default(bool);
		string text3 = default(string);
		string left = default(string);
		int num5 = default(int);
		bool flag4 = default(bool);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				bool flag;
				bool flag5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1849:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002a;
						case 7:
							goto IL_002f;
						case 8:
							goto IL_0038;
						case 9:
							goto IL_0041;
						case 10:
							goto IL_0047;
						case 11:
							goto IL_004d;
						case 12:
							goto IL_0084;
						case 13:
							goto IL_009d;
						case 14:
							goto IL_00c3;
						case 16:
						case 17:
							goto IL_00df;
						case 18:
							goto IL_00f8;
						case 19:
							goto IL_00fe;
						case 20:
							goto IL_011c;
						case 21:
							goto IL_012f;
						case 22:
							goto IL_0140;
						case 23:
							goto IL_014b;
						case 25:
							goto IL_0157;
						case 26:
							goto IL_0175;
						case 27:
							goto IL_017b;
						case 28:
							goto IL_0185;
						case 29:
							goto IL_018b;
						case 30:
							goto IL_019b;
						case 31:
							goto IL_01c4;
						case 32:
							goto IL_01dd;
						case 33:
							goto IL_0200;
						case 34:
							goto IL_021d;
						case 36:
							goto IL_0247;
						case 37:
							goto IL_0258;
						case 35:
						case 38:
						case 39:
							goto IL_027c;
						case 40:
							goto IL_0295;
						case 43:
							goto IL_02c0;
						case 44:
							goto IL_02d4;
						case 45:
							goto IL_02e0;
						case 46:
							goto IL_02f3;
						case 47:
							goto IL_0301;
						case 49:
							goto IL_0311;
						case 50:
							goto IL_0328;
						case 51:
							goto IL_033b;
						case 52:
							goto IL_0349;
						case 53:
							goto IL_0353;
						case 54:
							goto IL_0373;
						case 55:
							goto IL_0395;
						case 56:
							goto IL_03ae;
						case 59:
							goto IL_03e4;
						case 60:
							goto IL_03ef;
						case 61:
							goto IL_0403;
						case 62:
							goto IL_0416;
						case 63:
							goto IL_0424;
						case 64:
							goto IL_0439;
						case 65:
							goto IL_0463;
						case 71:
							goto IL_0488;
						case 72:
							goto IL_049c;
						case 73:
							goto IL_04af;
						case 74:
							goto IL_04b9;
						case 76:
							goto IL_04cd;
						case 77:
							goto IL_04e1;
						case 78:
							goto IL_04f4;
						case 79:
							goto IL_0502;
						case 81:
							goto IL_050f;
						case 82:
							goto IL_0523;
						case 83:
							goto IL_0536;
						case 84:
							goto IL_0544;
						case 24:
						case 42:
						case 48:
						case 58:
						case 67:
						case 68:
						case 69:
						case 70:
						case 75:
						case 80:
						case 85:
						case 86:
							goto IL_054f;
						case 87:
							goto IL_0574;
						case 88:
							goto IL_057f;
						case 89:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
						case 41:
						case 57:
						case 66:
						case 90:
						case 91:
						case 92:
						case 93:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01dd:
					num2 = 32;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ShowRows("show all rows");
					goto IL_0200;
					IL_0200:
					num2 = 33;
					if (Operators.CompareString(text.ToUpper(), " [INSERT QUERY-VIEW]", TextCompare: false) == 0)
					{
						goto IL_021d;
					}
					goto IL_0247;
					IL_01c4:
					num2 = 31;
					if (Operators.CompareString(text2, "show all rows", TextCompare: false) != 0)
					{
						goto IL_01dd;
					}
					goto IL_0200;
					IL_021d:
					num2 = 34;
					flag = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Import_SPF_Query(toBeUnique);
					goto IL_027c;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					flag2 = false;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					myOptDB = "0";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					MyNewAlias = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					flag3 = false;
					goto IL_002f;
					IL_002f:
					num2 = 7;
					text3 = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					left = "";
					goto IL_0041;
					IL_0041:
					num2 = 9;
					num5 = 0;
					goto IL_0047;
					IL_0047:
					num2 = 10;
					flag4 = false;
					goto IL_004d;
					IL_004d:
					num2 = 11;
					if (GridTbl.CurCell.ColIndex != 0 || GridTbl.CurCell.RowIndex < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0084;
					IL_0247:
					num2 = 36;
					text3 = Get_DBOPT_or_File("F", text);
					goto IL_0258;
					IL_0084:
					num2 = 12;
					text = Conversions.ToString(GridTbl.CurCell.Value);
					goto IL_009d;
					IL_009d:
					num2 = 13;
					if ((Operators.CompareString(Strings.UCase(text), "PRE/POST QUERY", TextCompare: false) == 0) & (Globals_Renamed.g_FrmIdx > 1))
					{
						goto IL_00c3;
					}
					goto IL_00df;
					IL_00c3:
					num2 = 14;
					Interaction.MsgBox("You cannot nest Pre/Post queries beyond 2 levels", MsgBoxStyle.Exclamation, "Pre/Post Query");
					goto end_IL_0001_3;
					IL_00df:
					num2 = 17;
					if (Operators.CompareString(text, ExceptionTableList, TextCompare: false) == 0)
					{
						goto IL_00f8;
					}
					goto IL_00fe;
					IL_00f8:
					num2 = 18;
					flag4 = true;
					goto IL_00fe;
					IL_00fe:
					num2 = 19;
					if (OptDBType0.Checked && !flag4)
					{
						goto IL_011c;
					}
					goto IL_0157;
					IL_0258:
					num2 = 37;
					flag = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Import_SPF_Query(toBeUnique, text3);
					goto IL_027c;
					IL_011c:
					num2 = 20;
					ll_ObjectType = Conversions.ToShort("2");
					goto IL_012f;
					IL_012f:
					num2 = 21;
					myOptDB = Get_DBOPT_or_File("D", text);
					goto IL_0140;
					IL_0140:
					num2 = 22;
					ll_DBType = myOptDB;
					goto IL_014b;
					IL_014b:
					num2 = 23;
					flag3 = true;
					goto IL_054f;
					IL_0157:
					num2 = 25;
					if (optDBType5.Checked || flag4)
					{
						goto IL_0175;
					}
					goto IL_02c0;
					IL_027c:
					num2 = 39;
					if (Operators.CompareString(text2, "show all rows", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0295;
					IL_02c0:
					num2 = 43;
					if (OptDBType1.Checked)
					{
						goto IL_02d4;
					}
					goto IL_0311;
					IL_02d4:
					num2 = 44;
					text = BuildForm.Locate_Util_Item(text, -4);
					goto IL_02e0;
					IL_02e0:
					num2 = 45;
					ll_ObjectType = Conversions.ToShort("5");
					goto IL_02f3;
					IL_02f3:
					num2 = 46;
					ll_DBType = "8";
					goto IL_0301;
					IL_0301:
					num2 = 47;
					myOptDB = "8";
					goto IL_054f;
					IL_0311:
					num2 = 49;
					if (OptDBType2.Checked)
					{
						goto IL_0328;
					}
					goto IL_0488;
					IL_0328:
					num2 = 50;
					ll_ObjectType = Conversions.ToShort("1");
					goto IL_033b;
					IL_033b:
					num2 = 51;
					ll_DBType = "7";
					goto IL_0349;
					IL_0349:
					num2 = 52;
					myOptDB = "7";
					goto IL_0353;
					IL_0353:
					num2 = 53;
					if (Operators.CompareString(text.ToUpper(), "[BROWSE]", TextCompare: false) == 0)
					{
						goto IL_0373;
					}
					goto IL_054f;
					IL_0373:
					num2 = 54;
					BuildForm.FileOpenSave("O", "", "csv++", "", Globals_Renamed.MyPCDir);
					goto IL_0395;
					IL_0395:
					num2 = 55;
					text3 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_03ae;
					IL_03ae:
					num2 = 56;
					if ((Operators.CompareString(text3, "", TextCompare: false) == 0) | (Operators.CompareString(text3, "CANCEL", TextCompare: false) == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_03e4;
					IL_03e4:
					num2 = 59;
					text = Strings.Trim(text3);
					goto IL_03ef;
					IL_03ef:
					num2 = 60;
					text = BuildForm.Strip_Add_MyPCDir("S", text);
					goto IL_0403;
					IL_0403:
					num2 = 61;
					num5 = Strings.InStrRev(text3, ".");
					goto IL_0416;
					IL_0416:
					num2 = 62;
					if (num5 != 0)
					{
						goto IL_0424;
					}
					goto IL_0439;
					IL_0424:
					num2 = 63;
					left = Strings.UCase(Strings.Mid(text3, checked(num5 + 1)));
					goto IL_0439;
					IL_0439:
					num2 = 64;
					if (Strings.InStr(text, "\\") != 0 && Operators.CompareString(left, "SDB", TextCompare: false) != 0)
					{
						goto IL_0463;
					}
					goto IL_054f;
					IL_0295:
					num2 = 40;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ShowRows(text2);
					goto end_IL_0001_3;
					IL_0463:
					num2 = 65;
					Interaction.MsgBox("At this time, you cannot access CSV/TAB/ASC/PLUS Delimited Files that are not in your workFolder", MsgBoxStyle.Exclamation, "Invalid Selection");
					goto end_IL_0001_3;
					IL_0488:
					num2 = 71;
					if (OptDBType6.Checked)
					{
						goto IL_049c;
					}
					goto IL_04cd;
					IL_049c:
					num2 = 72;
					ll_ObjectType = Conversions.ToShort("2");
					goto IL_04af;
					IL_04af:
					num2 = 73;
					myOptDB = "D";
					goto IL_04b9;
					IL_04b9:
					num2 = 74;
					ll_DBType = "9";
					goto IL_054f;
					IL_04cd:
					num2 = 76;
					if (OptDBType3.Checked)
					{
						goto IL_04e1;
					}
					goto IL_050f;
					IL_04e1:
					num2 = 77;
					ll_ObjectType = Conversions.ToShort("3");
					goto IL_04f4;
					IL_04f4:
					num2 = 78;
					ll_DBType = "6";
					goto IL_0502;
					IL_0502:
					num2 = 79;
					myOptDB = "6";
					goto IL_054f;
					IL_050f:
					num2 = 81;
					if (OptDBType4.Checked)
					{
						goto IL_0523;
					}
					goto IL_054f;
					IL_0523:
					num2 = 82;
					ll_ObjectType = Conversions.ToShort("4");
					goto IL_0536;
					IL_0536:
					num2 = 83;
					ll_DBType = "4";
					goto IL_0544;
					IL_0544:
					num2 = 84;
					myOptDB = "4";
					goto IL_054f;
					IL_054f:
					num2 = 86;
					flag2 = MyProject.Forms.FrmMain.Add_Utility_or_Query_To_Tree(text, myOptDB, ref ll_DBType, ref ll_ObjectType, ref MyNewAlias);
					goto IL_0574;
					IL_0574:
					num2 = 87;
					if (!flag2)
					{
						goto end_IL_0001_3;
					}
					goto IL_057f;
					IL_057f:
					num2 = 88;
					if (!flag3)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0175:
					num2 = 26;
					flag = false;
					goto IL_017b;
					IL_017b:
					num2 = 27;
					text2 = "";
					goto IL_0185;
					IL_0185:
					num2 = 28;
					flag5 = false;
					goto IL_018b;
					IL_018b:
					num2 = 29;
					toBeUnique = mnuHeaders.Checked;
					goto IL_019b;
					IL_019b:
					num2 = 30;
					text2 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].CmbShowRows.Text.ToLower();
					goto IL_01c4;
					end_IL_0001_2:
					break;
				}
				num2 = 89;
				MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Chk_View_Filter(MyNewAlias);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1849;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string Get_DBOPT_or_File(string MyMode, string MyIn)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		string result = default(string);
		int num5 = default(int);
		int maxTables = default(int);
		int maxTables2 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string left2;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1045:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0022;
						case 7:
							goto IL_0058;
						case 8:
							goto IL_0067;
						case 9:
							goto IL_0078;
						case 10:
							goto IL_00f4;
						case 12:
						case 13:
							goto IL_0118;
						case 11:
						case 14:
							goto IL_012a;
						case 15:
							goto IL_0143;
						case 17:
							goto IL_0152;
						case 18:
							goto IL_016b;
						case 20:
							goto IL_017a;
						case 21:
							goto IL_0193;
						case 23:
							goto IL_01a2;
						case 24:
							goto IL_01bb;
						case 26:
							goto IL_01ca;
						case 27:
							goto IL_01e3;
						case 29:
							goto IL_01f2;
						case 30:
							goto IL_020b;
						case 32:
							goto IL_0217;
						case 33:
							goto IL_0230;
						case 35:
							goto IL_023c;
						case 36:
							goto IL_0255;
						case 38:
							goto IL_0261;
						case 39:
							goto IL_027a;
						case 42:
							goto IL_028a;
						case 43:
							goto IL_029a;
						case 44:
							goto IL_02ac;
						case 45:
							goto end_IL_0001_2;
						case 47:
						case 48:
							goto IL_0323;
						default:
							goto end_IL_0001;
						case 6:
						case 16:
						case 19:
						case 22:
						case 25:
						case 28:
						case 31:
						case 34:
						case 37:
						case 40:
						case 41:
						case 46:
						case 49:
						case 50:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0217:
					num2 = 32;
					if (Operators.CompareString(left, "a/t other2", TextCompare: false) == 0)
					{
						goto IL_0230;
					}
					goto IL_023c;
					IL_0230:
					num2 = 33;
					result = "B";
					goto end_IL_0001_3;
					IL_020b:
					num2 = 30;
					result = "9";
					goto end_IL_0001_3;
					IL_023c:
					num2 = 35;
					if (Operators.CompareString(left, "a/t other3", TextCompare: false) == 0)
					{
						goto IL_0255;
					}
					goto IL_0261;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					left = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					result = "";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					left2 = Strings.Trim(Strings.UCase(MyMode));
					if (Operators.CompareString(left2, "D", TextCompare: false) == 0)
					{
						goto IL_0058;
					}
					if (Operators.CompareString(left2, "F", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_028a;
					IL_0255:
					num2 = 36;
					result = "C";
					goto end_IL_0001_3;
					IL_028a:
					num2 = 42;
					MyIn = Strings.LCase(Strings.Trim(MyIn));
					goto IL_029a;
					IL_029a:
					num2 = 43;
					maxTables = Globals_Renamed.MaxTables;
					num5 = 0;
					goto IL_032c;
					IL_032c:
					if (num5 > maxTables)
					{
						goto end_IL_0001_3;
					}
					goto IL_02ac;
					IL_02ac:
					num2 = 44;
					if (Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num5].ObjectType), "QUERY", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Strings.LCase(Globals_Renamed.Tables[num5].Display)), MyIn, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0323;
					IL_0261:
					num2 = 38;
					if (Operators.CompareString(left, "midas", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_027a;
					IL_0323:
					num2 = 48;
					num5 = checked(num5 + 1);
					goto IL_032c;
					IL_0058:
					num2 = 7;
					MyIn = Strings.LCase(Strings.Trim(MyIn));
					goto IL_0067;
					IL_0067:
					num2 = 8;
					maxTables2 = Globals_Renamed.MaxTables;
					num5 = 0;
					goto IL_0121;
					IL_0121:
					if (num5 <= maxTables2)
					{
						goto IL_0078;
					}
					goto IL_012a;
					IL_0078:
					num2 = 9;
					if (Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num5].ObjectType), "VIEW", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.Tables[num5].Show)), "Y", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Strings.LCase(Globals_Renamed.Tables[num5].Display)), MyIn, TextCompare: false) == 0)
					{
						goto IL_00f4;
					}
					goto IL_0118;
					IL_027a:
					num2 = 39;
					result = "A";
					goto end_IL_0001_3;
					IL_0118:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_0121;
					IL_00f4:
					num2 = 10;
					left = Strings.LCase(Strings.Trim(Globals_Renamed.Tables[num5].DatabaseType));
					goto IL_012a;
					IL_012a:
					num2 = 14;
					if (Operators.CompareString(left, "mars ii", TextCompare: false) == 0)
					{
						goto IL_0143;
					}
					goto IL_0152;
					IL_0143:
					num2 = 15;
					result = "0";
					goto end_IL_0001_3;
					IL_0152:
					num2 = 17;
					if (Operators.CompareString(left, "mars iii", TextCompare: false) == 0)
					{
						goto IL_016b;
					}
					goto IL_017a;
					IL_016b:
					num2 = 18;
					result = "1";
					goto end_IL_0001_3;
					IL_017a:
					num2 = 20;
					if (Operators.CompareString(left, "aries", TextCompare: false) == 0)
					{
						goto IL_0193;
					}
					goto IL_01a2;
					IL_0193:
					num2 = 21;
					result = "2";
					goto end_IL_0001_3;
					IL_01a2:
					num2 = 23;
					if (Operators.CompareString(left, "oasys", TextCompare: false) == 0)
					{
						goto IL_01bb;
					}
					goto IL_01ca;
					IL_01bb:
					num2 = 24;
					result = "3";
					goto end_IL_0001_3;
					IL_01ca:
					num2 = 26;
					if (Operators.CompareString(left, "fab other", TextCompare: false) == 0)
					{
						goto IL_01e3;
					}
					goto IL_01f2;
					IL_01e3:
					num2 = 27;
					result = "5";
					goto end_IL_0001_3;
					IL_01f2:
					num2 = 29;
					if (Operators.CompareString(left, "a/t other1", TextCompare: false) == 0)
					{
						goto IL_020b;
					}
					goto IL_0217;
					end_IL_0001_2:
					break;
				}
				num2 = 45;
				result = Strings.LCase(Strings.Trim(Globals_Renamed.Tables[num5].fileschema));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1045;
				continue;
			}
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

	private void cmdClose_Click(object eventSender, EventArgs eventArgs)
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

	private void mnuHelp2_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Add+a+View+or+Utility+Form");
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

	private void frmtable_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string MyNoStr = default(string);
		long num6 = default(long);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num7;
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
						case 3:
							goto IL_0013;
						case 4:
							goto IL_0019;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_0023;
						case 7:
							goto IL_002c;
						case 8:
							goto IL_0051;
						case 9:
							goto IL_005f;
						case 10:
							goto IL_0068;
						case 11:
							goto IL_0072;
						case 12:
							goto IL_0090;
						case 13:
							goto IL_00a1;
						case 14:
							goto IL_00bc;
						case 17:
							goto IL_00d8;
						case 15:
						case 16:
						case 18:
						case 19:
							goto IL_0103;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00bc:
					num2 = 14;
					MyProject.Forms.FrmMain.FrmSQLQuery[num5] = null;
					goto IL_0103;
					IL_00d8:
					num2 = 17;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_MainQ_Branch(ref MyNoStr, "L", "");
					goto IL_0103;
					IL_00a1:
					num2 = 13;
					MyProject.Forms.FrmMain.FrmSQLQuery[num5].Dispose();
					goto IL_00bc;
					IL_0103:
					num2 = 19;
					MyProject.Forms.FrmMain.Focus();
					break;
					IL_000b:
					num2 = 2;
					MyNoStr = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num6 = 0L;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					num5 = 0;
					goto IL_001e;
					IL_001e:
					num2 = 5;
					num7 = 0;
					goto IL_0023;
					IL_0023:
					num2 = 6;
					text = "";
					goto IL_002c;
					IL_002c:
					num2 = 7;
					num6 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.GetNodeCount(includeSubTrees: false);
					goto IL_0051;
					IL_0051:
					num2 = 8;
					if (num6 == 0)
					{
						goto IL_005f;
					}
					goto IL_00d8;
					IL_005f:
					num2 = 9;
					Globals_Renamed.QueryCancel = 0;
					goto IL_0068;
					IL_0068:
					num2 = 10;
					num5 = Globals_Renamed.g_FrmIdx;
					goto IL_0072;
					IL_0072:
					num2 = 11;
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_0090;
					IL_0090:
					num2 = 12;
					if (Globals_Renamed.QueryCancel == 0)
					{
						goto IL_00a1;
					}
					goto IL_0103;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				Dispose();
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

	private void List1_KeyPress(object sender, KeyPressEventArgs e)
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
				case 103:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				cmdaddtable_Click(cmdaddtable, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 103;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void OptObjectTypeNormal_CheckedChanged(object sender, EventArgs e)
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
				Load_Table_List();
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

	private void mnuhelp_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		while (true)
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
				case 303:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0024;
						case 5:
							goto IL_005a;
						case 6:
							goto IL_0073;
						case 7:
							goto IL_00a1;
						case 8:
							goto IL_00b9;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 9:
						case 11:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005a:
					num2 = 5;
					text = Conversions.ToString(GridTbl.CurCell.Value);
					goto IL_0073;
					IL_0073:
					num2 = 6;
					text2 = General_Procedures.Get_Ini_Data("view", Strings.LCase(text), "N/A", 1000, Globals_Renamed.MySchemaDir + "\\view_help.ini");
					goto IL_00a1;
					IL_00b9:
					num2 = 8;
					Interaction.MsgBox("No help available for: " + text, MsgBoxStyle.Information, "No Help");
					goto end_IL_0001_3;
					IL_00a1:
					num2 = 7;
					if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00b9;
					IL_000b:
					num2 = 2;
					if (!OptDBType0.Checked)
					{
						goto end_IL_0001_3;
					}
					goto IL_0024;
					IL_0024:
					num2 = 4;
					if (GridTbl.CurCell.ColIndex != 0 || GridTbl.CurCell.RowIndex < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				BuildForm.Invoke_IE(text2);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 303;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuSec_Click(object sender, EventArgs e)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Security+Access");
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

	private void frmtable_Load(object sender, EventArgs e)
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
						errsource = "frmtable - Form_Load";
						string text = "";
						Cursor.Current = Cursors.WaitCursor;
						OptDBType0.Checked = true;
						GridTbl.BackColorEvenRows = Color.WhiteSmoke;
						GridTbl.SearchAsType.Mode = (iGSearchAsTypeMode)1;
						GridTbl.SearchAsType.DisplaySearchText = true;
						GridTbl.SearchAsType.DisplayKeyboardHint = true;
						GridTbl.SearchAsType.StartFromCurRow = false;
						GridTbl.SearchAsType.MatchRule = (iGMatchRule)1;
						GridTbl.SearchAsType.AutoCancel = true;
						Load_Table_List();
						Load_CmbFav("Last", "");
						text = ((Operators.CompareString(cmbFav.Text, "Last", TextCompare: false) != 0) ? (Strings.Trim(cmbFav.Text) + ".tablefilter") : f_DefFFile);
						LoadFilterFromFile(text);
						Cursor.Current = Cursors.Default;
						base.Left += 40;
						goto end_IL_0001;
					}
					case 343:
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
				try0001_dispatch = 343;
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

	public void SaveFilterToDisk(string MyFile, int MyOpt = 0)
	{
		try
		{
			string text = "";
			if (Operators.CompareString(MyFile, "", TextCompare: false) == 0)
			{
				BuildForm.FileOpenSave("S", "", "tablefilter", "Save Filter Condition File", Globals_Renamed.gQueryDir);
				if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "", TextCompare: false) == 0))
				{
					return;
				}
				MyFile = Strings.Trim(MyProject.Forms.FrmMain.CMDialog1Save.FileName);
			}
			GridF.SaveFilterToFile(MyFile);
			if (MyOpt == 1)
			{
				text = Get_Filt_Name(MyFile);
				Load_CmbFav(text, "P:");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Could not Save Favorite Filter: " + MyFile + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Favorites");
			ProjectData.ClearProjectError();
		}
	}

	public void LoadFilterFromFile(string MyFile)
	{
		string text = "";
		try
		{
			if (Operators.CompareString(MyFile, "", TextCompare: false) == 0)
			{
				BuildForm.FileOpenSave("O", "", "tablefilter", "Locate View Filter Condition File", Globals_Renamed.gQueryDir);
				if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) == 0))
				{
					return;
				}
				MyFile = Strings.Trim(MyProject.Forms.FrmMain.CMDialog1Open.FileName);
			}
			else
			{
				text = Strings.Mid(MyFile + "  ", 1, 2);
				if (Operators.CompareString(text, "S:", TextCompare: false) == 0)
				{
					MyFile = Globals_Renamed.MySchemaDir + "\\" + Strings.Mid(MyFile, 3);
				}
				else if (Operators.CompareString(text, "P:", TextCompare: false) == 0)
				{
					MyFile = Globals_Renamed.gQueryDir + Strings.Mid(MyFile, 3);
				}
			}
			if (MyProject.Computer.FileSystem.FileExists(MyFile))
			{
				GridF.ClearFilter();
				GridF.RestoreFilterFromFile(MyFile);
				GridF.SaveFilterToMemory("filterdefault");
				filterdefault = true;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Could not load Filter Favorite: " + MyFile + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Favorites");
			ProjectData.ClearProjectError();
		}
	}

	private void Form_Resize()
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 844:
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
								goto IL_001c;
							case 4:
								goto IL_0025;
							case 5:
								goto IL_0062;
							case 7:
								goto IL_0082;
							case 8:
								goto IL_0098;
							case 9:
								goto IL_00c3;
							case 10:
								goto IL_00ef;
							case 12:
								goto IL_0121;
							case 13:
								goto IL_0138;
							case 14:
								goto IL_0167;
							case 15:
								goto IL_0196;
							case 16:
								goto IL_01c5;
							case 17:
								goto IL_01f4;
							case 18:
								goto IL_0210;
							case 19:
								goto IL_022c;
							case 20:
								goto IL_0248;
							case 21:
								goto IL_0264;
							case 22:
								goto IL_0280;
							case 23:
								goto IL_029d;
							case 6:
							case 11:
							case 24:
							case 25:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 26:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_022c:
						num2 = 19;
						GridTbl.Cols[6].Width = 47;
						goto IL_0248;
						IL_0248:
						num2 = 20;
						GridTbl.Cols[7].Width = 50;
						goto IL_0264;
						IL_0210:
						num2 = 18;
						GridTbl.Cols[5].Width = 62;
						goto IL_022c;
						IL_0264:
						num2 = 21;
						GridTbl.Cols[8].Width = 50;
						goto IL_0280;
						IL_000b:
						num2 = 2;
						num5 = ((Control)(object)GridTbl).Width - 20;
						goto IL_001c;
						IL_001c:
						num2 = 3;
						num6 = 364;
						goto IL_0025;
						IL_0025:
						num2 = 4;
						if (OptDBType2.Checked || OptDBType3.Checked || OptDBType4.Checked || OptDBType6.Checked)
						{
							goto IL_0062;
						}
						goto IL_0082;
						IL_0280:
						num2 = 22;
						GridTbl.Cols[9].Width = 60;
						goto IL_029d;
						IL_029d:
						num2 = 23;
						GridTbl.Cols[10].Width = 50;
						break;
						IL_0062:
						num2 = 5;
						GridTbl.Cols[0].Width = num5;
						break;
						IL_0082:
						num2 = 7;
						if (OptDBType1.Checked)
						{
							goto IL_0098;
						}
						goto IL_0121;
						IL_0098:
						num2 = 8;
						GridTbl.Cols[0].Width = (int)Math.Round(0.3 * (double)num5);
						goto IL_00c3;
						IL_00c3:
						num2 = 9;
						GridTbl.Cols[1].Width = (int)Math.Round(0.5 * (double)num5);
						goto IL_00ef;
						IL_00ef:
						num2 = 10;
						GridTbl.Cols[2].Width = (int)Math.Round(0.19 * (double)num5);
						break;
						IL_0121:
						num2 = 12;
						if (!OptDBType0.Checked)
						{
							break;
						}
						goto IL_0138;
						IL_0138:
						num2 = 13;
						GridTbl.Cols[0].Width = (int)Math.Round(0.28 * (double)(num5 - num6));
						goto IL_0167;
						IL_0167:
						num2 = 14;
						GridTbl.Cols[1].Width = (int)Math.Round(0.43 * (double)(num5 - num6));
						goto IL_0196;
						IL_0196:
						num2 = 15;
						GridTbl.Cols[2].Width = (int)Math.Round(0.14 * (double)(num5 - num6));
						goto IL_01c5;
						IL_01c5:
						num2 = 16;
						GridTbl.Cols[3].Width = (int)Math.Round(0.14 * (double)(num5 - num6));
						goto IL_01f4;
						IL_01f4:
						num2 = 17;
						GridTbl.Cols[4].Width = 47;
						goto IL_0210;
						end_IL_0001_2:
						break;
					}
					num2 = 25;
					GridTbl.Rows.AutoHeight();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 844;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmtable_Resize(object sender, EventArgs e)
	{
		Form_Resize();
	}

	private void GridTbl_MouseDoubleClick(object sender, MouseEventArgs e)
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
				case 102:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0017;
						case 5:
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!f_IsColHdr)
					{
						break;
					}
					goto IL_0017;
					IL_0017:
					num2 = 3;
					f_IsColHdr = false;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				cmdaddtable_Click(cmdaddtable, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 102;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridTbl_KeyPress(object sender, KeyPressEventArgs e)
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
				case 103:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				cmdaddtable_Click(cmdaddtable, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 103;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridTbl_KeyDown(object sender, KeyEventArgs e)
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
							goto IL_001b;
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
					if (e.KeyCode != Keys.Tab)
					{
						goto end_IL_0001_3;
					}
					goto IL_001b;
					IL_001b:
					num2 = 3;
					OptDBType0.Focus();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				e.Handled = true;
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

	public void Load_Views2(int RowNo, string VieworQuery)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		bool flag = default(bool);
		int maxTables = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					iGRowCollection rows;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 914:
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
								goto IL_0024;
							case 6:
								goto IL_0029;
							case 7:
								goto IL_0061;
							case 8:
								goto IL_0066;
							case 9:
								goto IL_00d6;
							case 10:
								goto IL_00de;
							case 11:
								goto IL_00fe;
							case 12:
								goto IL_0129;
							case 13:
								goto IL_0154;
							case 14:
								goto IL_017f;
							case 15:
								goto IL_01aa;
							case 16:
								goto IL_01d5;
							case 17:
								goto IL_0200;
							case 18:
								goto IL_022b;
							case 19:
								goto IL_0256;
							case 20:
								goto IL_0281;
							case 21:
								goto IL_02ad;
							case 22:
								goto IL_02d9;
							case 23:
								goto IL_02e4;
							case 24:
							case 25:
								goto IL_0306;
							default:
								goto end_IL_0001;
							case 26:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_022b:
						num2 = 18;
						GridTbl.Cells[RowNo, 7].Value = Globals_Renamed.Tables[num5].lbl10;
						goto IL_0256;
						IL_0256:
						num2 = 19;
						GridTbl.Cells[RowNo, 8].Value = Globals_Renamed.Tables[num5].lbl7;
						goto IL_0281;
						IL_0200:
						num2 = 17;
						GridTbl.Cells[RowNo, 6].Value = Globals_Renamed.Tables[num5].lbl4;
						goto IL_022b;
						IL_0281:
						num2 = 20;
						GridTbl.Cells[RowNo, 9].Value = Globals_Renamed.Tables[num5].lbl6;
						goto IL_02ad;
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
						maxTables = Globals_Renamed.MaxTables;
						num5 = 0;
						goto IL_030d;
						IL_030d:
						if (num5 > maxTables)
						{
							goto end_IL_0001_2;
						}
						goto IL_0024;
						IL_0024:
						num2 = 5;
						flag = false;
						goto IL_0029;
						IL_0029:
						num2 = 6;
						if (Operators.CompareString(VieworQuery, "VIEW", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.Tables[num5].Display, ExceptionTableList, TextCompare: false) == 0)
						{
							goto IL_0061;
						}
						goto IL_0066;
						IL_02ad:
						num2 = 21;
						GridTbl.Cells[RowNo, 10].Value = Globals_Renamed.Tables[num5].lbl3;
						goto IL_02d9;
						IL_0061:
						num2 = 7;
						flag = true;
						goto IL_0066;
						IL_0066:
						num2 = 8;
						if ((Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num5].ObjectType), VieworQuery, TextCompare: false) == 0 || flag) && Operators.CompareString(Strings.UCase(Globals_Renamed.Tables[num5].Show), "Y", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.Tables[num5].lbl2, "DUP", TextCompare: false) != 0)
						{
							goto IL_00d6;
						}
						goto IL_0306;
						IL_02e4:
						num2 = 23;
						GridTbl.Cells[RowNo, 0].BackColor = Color.PeachPuff;
						goto IL_0306;
						IL_02d9:
						num2 = 22;
						if (flag)
						{
							goto IL_02e4;
						}
						goto IL_0306;
						IL_0306:
						num2 = 25;
						num5++;
						goto IL_030d;
						IL_00d6:
						num2 = 9;
						RowNo++;
						goto IL_00de;
						IL_00de:
						num2 = 10;
						(rows = GridTbl.Rows).Count = rows.Count + 1;
						goto IL_00fe;
						IL_00fe:
						num2 = 11;
						GridTbl.Cells[RowNo, 0].Value = Globals_Renamed.Tables[num5].Display;
						goto IL_0129;
						IL_0129:
						num2 = 12;
						GridTbl.Cells[RowNo, 1].Value = Globals_Renamed.Tables[num5].Description;
						goto IL_0154;
						IL_0154:
						num2 = 13;
						GridTbl.Cells[RowNo, 2].Value = Globals_Renamed.Tables[num5].lbl1;
						goto IL_017f;
						IL_017f:
						num2 = 14;
						GridTbl.Cells[RowNo, 3].Value = Globals_Renamed.Tables[num5].lbl0;
						goto IL_01aa;
						IL_01aa:
						num2 = 15;
						GridTbl.Cells[RowNo, 4].Value = Globals_Renamed.Tables[num5].lbl5;
						goto IL_01d5;
						IL_01d5:
						num2 = 16;
						GridTbl.Cells[RowNo, 5].Value = Globals_Renamed.Tables[num5].lbl9;
						goto IL_0200;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 914;
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

	private void mnuSaveFilter_Click(object sender, EventArgs e)
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
				SaveFilterToDisk("", 1);
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

	private void mnuLoadFilter_Click(object sender, EventArgs e)
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
				LoadFilterFromFile("");
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

	private void mnuClearFilters_Click(object sender, EventArgs e)
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
						case 3:
							goto IL_0019;
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
					GridF.ClearFilter();
					goto IL_0019;
					IL_0019:
					num2 = 3;
					if (!cmbFav.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				cmbFav.SelectedIndex = 0;
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

	private void mnuJET_Click(object sender, EventArgs e)
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
				case 162:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002b;
						case 4:
							goto IL_003a;
						case 6:
							goto IL_004d;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002b:
					num2 = 3;
					OptDBType3.Enabled = true;
					goto IL_003a;
					IL_003a:
					num2 = 4;
					OptDBType4.Enabled = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					if (Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "Checked", new object[0], null, null, null)))
					{
						goto IL_002b;
					}
					goto IL_004d;
					IL_004d:
					num2 = 6;
					OptDBType3.Enabled = false;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				OptDBType4.Enabled = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 162;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmbFav_SelectedIndexChanged(object sender, EventArgs e)
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
							goto IL_002a;
						case 5:
							goto IL_0041;
						case 6:
							goto IL_0062;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002a:
					num2 = 3;
					mnuClearFilters_Click(mnuClearFilters, new EventArgs());
					goto end_IL_0001_3;
					IL_0041:
					num2 = 5;
					if (Operators.CompareString(cmbFav.Text, "Last", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0062;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(cmbFav.Text, "All", TextCompare: false) == 0)
					{
						goto IL_002a;
					}
					goto IL_0041;
					IL_0062:
					num2 = 6;
					LoadFilterFromFile(f_DefFFile);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				LoadFilterFromFile(cmbFav.Text + ".tablefilter");
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

	private void Frmtable2_Closing(object sender, CancelEventArgs e)
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
					if (!filterdefault)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				SaveFilterToDisk(f_DefFFile);
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

	private void mnuRenaFilters_Click(object sender, EventArgs e)
	{
		string text = Strings.Trim(cmbFav.Text);
		string text2 = "";
		string text3 = "";
		int num = 0;
		try
		{
			if (Operators.CompareString(text, "All", TextCompare: false) == 0 || Operators.CompareString(text, "Last", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text, 1, 2), "S:", TextCompare: false) == 0)
			{
				return;
			}
			text2 = Globals_Renamed.gQueryDir + Strings.Mid(text, 3) + ".tablefilter";
			if (MyProject.Computer.FileSystem.FileExists(text2))
			{
				object left = NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null);
				if (Operators.ConditionalCompareObjectEqual(left, "RENAME", TextCompare: false))
				{
					text3 = Strings.Trim(Interaction.InputBox("Enter New Filter Name", "Rename", Strings.Mid(text, 3)));
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						if (General_Procedures.Check_For_Char(text3, "?/|<>\"*{};\\."))
						{
							Interaction.MsgBox("Invalid Filter Name: " + text3, MsgBoxStyle.Exclamation, "Error");
							return;
						}
						MyProject.Computer.FileSystem.RenameFile(text2, text3 + ".tablefilter");
						Load_CmbFav("P:" + text3, "P:");
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(left, "DELETE", TextCompare: false))
				{
					num = (int)Interaction.MsgBox("Are you sure you wish to delete the Filter?", MsgBoxStyle.YesNoCancel, "Delete?");
					if (num == 6)
					{
						MyProject.Computer.FileSystem.DeleteFile(text2);
						Load_CmbFav("All", "");
					}
				}
			}
			else
			{
				Load_CmbFav("All", "");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error Processing Filter: " + text + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Error");
			ProjectData.ClearProjectError();
		}
	}

	private void mnushowMetaData_Click(object sender, EventArgs e)
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
				int CSV_Connected = Globals_Renamed.CSV_ConnectedM;
				BuildForm.Invoke_CSVViewer2(1, "META", ref Globals_Renamed.CSV_hwndProcessM, ref CSV_Connected);
				Globals_Renamed.CSV_ConnectedM = checked((short)CSV_Connected);
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

	private void GridTbl_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
	{
		f_IsColHdr = true;
	}

	private void GridTbl_ColWidthEndChange(object sender, iGColWidthEventArgs e)
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
				GridTbl.Rows.AutoHeight();
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
}

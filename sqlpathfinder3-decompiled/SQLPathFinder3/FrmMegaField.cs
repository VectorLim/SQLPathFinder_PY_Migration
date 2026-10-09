using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;
using TenTec.Windows.iGridLib;
using TenTec.Windows.iGridLib.Filtering;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmMegaField : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearFilters")]
	private ToolStripMenuItem _mnuClearFilters;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnudeselectall")]
	private ToolStripMenuItem _mnudeselectall;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCopy")]
	private ToolStripMenuItem _mnuCopy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("iGFilter")]
	private iGAutoFilterManager _iGFilter;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnucmbsearch")]
	private ToolStripComboBox _mnucmbsearch;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("FieldGrid")]
	private iGrid _FieldGrid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("SplitContainer1")]
	private SplitContainer _SplitContainer1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuaddselected")]
	private ToolStripMenuItem _mnuaddselected;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdRemove")]
	private Button _cmdRemove;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdAdd")]
	private Button _cmdAdd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuAutoRowHeight2")]
	private ToolStripMenuItem _mnuAutoRowHeight2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRefresh2")]
	private ToolStripMenuItem _mnuRefresh2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private Button _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel2")]
	private Button _CmdCancel2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK2")]
	private Button _cmdOK2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRemoveLstItem")]
	private ToolStripMenuItem _mnuRemoveLstItem;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnucmbDataType")]
	private ToolStripComboBox _mnucmbDataType;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutWF")]
	private ToolStripMenuItem _mnuOutWF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutPaste2")]
	private ToolStripMenuItem _mnuOutPaste2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHLPaste")]
	private ToolStripMenuItem _mnuHLPaste;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutSelectAll")]
	private ToolStripMenuItem _mnuOutSelectAll;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutCopy")]
	private ToolStripMenuItem _mnuOutCopy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdIncl")]
	private Button _cmdIncl;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdexcl")]
	private Button _cmdexcl;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown")]
	private Button _cmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp")]
	private Button _cmdUp;

	private const int gDefaultRowHeight = 23;

	private string gOldValue;

	private string gOldDataType;

	private bool gdoFieldSearch;

	public string f_InMode;

	public string f_InData;

	private string f_InType;

	public string f_InType2;

	public string f_out;

	private const int gMaxSQLCols = 1999;

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("StatusStrip1")]
	internal virtual StatusStrip StatusStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("StatusLabel")]
	internal virtual ToolStripStatusLabel StatusLabel
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

	[field: AccessedThroughProperty("mnuGridContext")]
	internal virtual ContextMenuStrip mnuGridContext
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnudeselectall
	{
		[CompilerGenerated]
		get
		{
			return _mnudeselectall;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnudeselectall_Click;
			ToolStripMenuItem toolStripMenuItem = _mnudeselectall;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnudeselectall = value;
			toolStripMenuItem = _mnudeselectall;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuCopy
	{
		[CompilerGenerated]
		get
		{
			return _mnuCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCopy_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCopy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCopy = value;
			toolStripMenuItem = _mnuCopy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual iGAutoFilterManager iGFilter
	{
		[CompilerGenerated]
		get
		{
			return _iGFilter;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			iGFilterAppliedEventHandler val = new iGFilterAppliedEventHandler(iGFilter_FilterApplied);
			iGAutoFilterManager val2 = _iGFilter;
			if (val2 != null)
			{
				val2.FilterApplied -= val;
			}
			_iGFilter = value;
			val2 = _iGFilter;
			if (val2 != null)
			{
				val2.FilterApplied += val;
			}
		}
	}

	[field: AccessedThroughProperty("mnuSelCol")]
	internal virtual ToolStripMenuItem mnuSelCol
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripTextBox1")]
	internal virtual ToolStripTextBox ToolStripTextBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox mnucmbsearch
	{
		[CompilerGenerated]
		get
		{
			return _mnucmbsearch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnucmbsearch_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = _mnucmbsearch;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			_mnucmbsearch = value;
			toolStripComboBox = _mnucmbsearch;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("IGrid1DefaultColHdrStyle")]
	internal virtual iGColHdrStyle IGrid1DefaultColHdrStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("IGrid1DefaultCellStyle")]
	internal virtual iGCellStyle IGrid1DefaultCellStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual iGrid FieldGrid
	{
		[CompilerGenerated]
		get
		{
			return _FieldGrid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = GridCSV_MouseDoubleClick;
			KeyPressEventHandler value3 = GridCSV_KeyPress;
			iGrid val = _FieldGrid;
			if (val != null)
			{
				((Control)(object)val).MouseDoubleClick -= value2;
				((Control)(object)val).KeyPress -= value3;
			}
			_FieldGrid = value;
			val = _FieldGrid;
			if (val != null)
			{
				((Control)(object)val).MouseDoubleClick += value2;
				((Control)(object)val).KeyPress += value3;
			}
		}
	}

	internal virtual SplitContainer SplitContainer1
	{
		[CompilerGenerated]
		get
		{
			return _SplitContainer1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SplitContainer1_Resize;
			SplitterEventHandler value3 = SplitContainer1_SplitterMoved;
			SplitContainer splitContainer = _SplitContainer1;
			if (splitContainer != null)
			{
				splitContainer.Resize -= value2;
				splitContainer.SplitterMoved -= value3;
			}
			_SplitContainer1 = value;
			splitContainer = _SplitContainer1;
			if (splitContainer != null)
			{
				splitContainer.Resize += value2;
				splitContainer.SplitterMoved += value3;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuaddselected
	{
		[CompilerGenerated]
		get
		{
			return _mnuaddselected;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAdd_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuaddselected;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuaddselected = value;
			toolStripMenuItem = _mnuaddselected;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdRemove
	{
		[CompilerGenerated]
		get
		{
			return _cmdRemove;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRemove_Click;
			Button button = _cmdRemove;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdRemove = value;
			button = _cmdRemove;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdAdd
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
			EventHandler value2 = cmdAdd_Click;
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

	[field: AccessedThroughProperty("lblfields")]
	internal virtual Label lblfields
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuAutoRowHeight2
	{
		[CompilerGenerated]
		get
		{
			return _mnuAutoRowHeight2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuAutoRowHeight_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuAutoRowHeight2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuAutoRowHeight2 = value;
			toolStripMenuItem = _mnuAutoRowHeight2;
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

	internal virtual ToolStripMenuItem mnuRefresh2
	{
		[CompilerGenerated]
		get
		{
			return _mnuRefresh2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRefresh_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRefresh2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRefresh2 = value;
			toolStripMenuItem = _mnuRefresh2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
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
			EventHandler value2 = mnuClearAll_Click;
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

	internal virtual Button CmdCancel2
	{
		[CompilerGenerated]
		get
		{
			return _CmdCancel2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCancel2_Click;
			Button button = _CmdCancel2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdCancel2 = value;
			button = _CmdCancel2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdOK2
	{
		[CompilerGenerated]
		get
		{
			return _cmdOK2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOK2_Click;
			Button button = _cmdOK2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdOK2 = value;
			button = _cmdOK2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuHelp")]
	internal virtual ToolStripMenuItem mnuHelp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuHelpRegex")]
	internal virtual ToolStripMenuItem mnuHelpRegex
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuOutGridContext")]
	internal virtual ContextMenuStrip mnuOutGridContext
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuRemoveLstItem
	{
		[CompilerGenerated]
		get
		{
			return _mnuRemoveLstItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRemove_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRemoveLstItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRemoveLstItem = value;
			toolStripMenuItem = _mnuRemoveLstItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("outGrid")]
	internal virtual DataGridView outGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblType")]
	internal virtual Label lblType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbType")]
	internal virtual ComboBox cmbType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colField")]
	internal virtual DataGridViewTextBoxColumn colField
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colType")]
	internal virtual DataGridViewComboBoxColumn colType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuDataType")]
	internal virtual ToolStripMenuItem mnuDataType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox mnucmbDataType
	{
		[CompilerGenerated]
		get
		{
			return _mnucmbDataType;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbDataType_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = _mnucmbDataType;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			_mnucmbDataType = value;
			toolStripComboBox = _mnucmbDataType;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuFile")]
	internal virtual ToolStripMenuItem mnuFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuOutWF
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutWF;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutWF_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutWF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutWF = value;
			toolStripMenuItem = _mnuOutWF;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuOptDT")]
	internal virtual ToolStripMenuItem mnuOptDT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator4")]
	internal virtual ToolStripSeparator ToolStripSeparator4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuOutPaste2
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutPaste2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPaste2_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutPaste2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutPaste2 = value;
			toolStripMenuItem = _mnuOutPaste2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuHLPaste
	{
		[CompilerGenerated]
		get
		{
			return _mnuHLPaste;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHLPaste_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHLPaste;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHLPaste = value;
			toolStripMenuItem = _mnuHLPaste;
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

	internal virtual ToolStripMenuItem mnuOutSelectAll
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutSelectAll;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutSelectAll_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutSelectAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutSelectAll = value;
			toolStripMenuItem = _mnuOutSelectAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuOutCopy
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutCopy_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutCopy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutCopy = value;
			toolStripMenuItem = _mnuOutCopy;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual Button cmdIncl
	{
		[CompilerGenerated]
		get
		{
			return _cmdIncl;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdexcl_Click;
			Button button = _cmdIncl;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdIncl = value;
			button = _cmdIncl;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button cmdexcl
	{
		[CompilerGenerated]
		get
		{
			return _cmdexcl;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdexcl_Click;
			Button button = _cmdexcl;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdexcl = value;
			button = _cmdexcl;
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

	internal virtual Button cmdUp
	{
		[CompilerGenerated]
		get
		{
			return _cmdUp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp_Click;
			Button button = _cmdUp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdUp = value;
			button = _cmdUp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public FrmMegaField()
	{
		base.Load += FrmCSVViewer_Load;
		base.Resize += FrmCSVViewer_Resize;
		base.FormClosing += FrmMegaField_FormClosing;
		gOldValue = "main";
		gOldDataType = "KS:Unit/Die/Patch";
		gdoFieldSearch = false;
		f_InMode = "UTIL";
		f_InData = "";
		f_InType = "KS:Unit";
		f_InType2 = "KU";
		f_out = "";
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
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmMegaField));
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutWF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOptDT = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuDataType = new System.Windows.Forms.ToolStripMenuItem();
		this.mnucmbDataType = new System.Windows.Forms.ToolStripComboBox();
		this.ToolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
		this.mnucmbsearch = new System.Windows.Forms.ToolStripComboBox();
		this.mnuClearFilters = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelpRegex = new System.Windows.Forms.ToolStripMenuItem();
		this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
		this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdRemove = new System.Windows.Forms.Button();
		this.cmdAdd = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdexcl = new System.Windows.Forms.Button();
		this.cmdIncl = new System.Windows.Forms.Button();
		this.mnuGridContext = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuaddselected = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
		this.mnudeselectall = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSelCol = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuAutoRowHeight2 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRefresh2 = new System.Windows.Forms.ToolStripMenuItem();
		this.iGFilter = new iGAutoFilterManager();
		this.mnuOutGridContext = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuRemoveLstItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuOutSelectAll = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutCopy = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutPaste2 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuHLPaste = new System.Windows.Forms.ToolStripMenuItem();
		this.FieldGrid = new iGrid();
		this.IGrid1DefaultCellStyle = new iGCellStyle(true);
		this.IGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.lblType = new System.Windows.Forms.Label();
		this.cmbType = new System.Windows.Forms.ComboBox();
		this.outGrid = new System.Windows.Forms.DataGridView();
		this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.CmdCancel2 = new System.Windows.Forms.Button();
		this.cmdOK2 = new System.Windows.Forms.Button();
		this.lblfields = new System.Windows.Forms.Label();
		this.MenuStrip1.SuspendLayout();
		this.StatusStrip1.SuspendLayout();
		this.mnuGridContext.SuspendLayout();
		this.mnuOutGridContext.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.FieldGrid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.outGrid).BeginInit();
		base.SuspendLayout();
		this.MenuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.mnuFile, this.mnuDataType, this.mnucmbDataType, this.ToolStripTextBox1, this.mnucmbsearch, this.mnuClearFilters, this.mnuHelp });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
		this.MenuStrip1.Size = new System.Drawing.Size(1331, 37);
		this.MenuStrip1.TabIndex = 0;
		this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuOutWF, this.mnuOptDT });
		this.mnuFile.Name = "mnuFile";
		this.mnuFile.Size = new System.Drawing.Size(92, 33);
		this.mnuFile.Text = "Options";
		this.mnuOutWF.Name = "mnuOutWF";
		this.mnuOutWF.Size = new System.Drawing.Size(534, 34);
		this.mnuOutWF.Text = "Output File {MyInput.txt}";
		this.mnuOptDT.CheckOnClick = true;
		this.mnuOptDT.Name = "mnuOptDT";
		this.mnuOptDT.Size = new System.Drawing.Size(534, 34);
		this.mnuOptDT.Text = "Use with imBigData-KitchenSink (Save with DataType)";
		this.mnuOptDT.ToolTipText = "Check to save with Datatype as required by imBigData. Uncheck to save without Datatype for MongoDB-KitchenSink";
		this.mnuDataType.Name = "mnuDataType";
		this.mnuDataType.Size = new System.Drawing.Size(111, 33);
		this.mnuDataType.Text = "Data Type:";
		this.mnucmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.mnucmbDataType.Name = "mnucmbDataType";
		this.mnucmbDataType.Size = new System.Drawing.Size(191, 33);
		this.ToolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9f);
		this.ToolStripTextBox1.Name = "ToolStripTextBox1";
		this.ToolStripTextBox1.ReadOnly = true;
		this.ToolStripTextBox1.Size = new System.Drawing.Size(112, 33);
		this.ToolStripTextBox1.Text = "Field Level:";
		this.mnucmbsearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.mnucmbsearch.DropDownWidth = 250;
		this.mnucmbsearch.Items.AddRange(new object[4] { "Main", "ll", "ul", "wl" });
		this.mnucmbsearch.Name = "mnucmbsearch";
		this.mnucmbsearch.Size = new System.Drawing.Size(168, 33);
		this.mnuClearFilters.Image = (System.Drawing.Image)resources.GetObject("mnuClearFilters.Image");
		this.mnuClearFilters.Name = "mnuClearFilters";
		this.mnuClearFilters.Size = new System.Drawing.Size(138, 33);
		this.mnuClearFilters.Text = "Clear &Filters";
		this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuHelpRegex });
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.Size = new System.Drawing.Size(65, 33);
		this.mnuHelp.Text = "&Help";
		this.mnuHelpRegex.Name = "mnuHelpRegex";
		this.mnuHelpRegex.Size = new System.Drawing.Size(338, 34);
		this.mnuHelpRegex.Text = "Help on Regular Expressions";
		this.StatusStrip1.AutoSize = false;
		this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.StatusLabel });
		this.StatusStrip1.Location = new System.Drawing.Point(0, 871);
		this.StatusStrip1.Name = "StatusStrip1";
		this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
		this.StatusStrip1.Size = new System.Drawing.Size(1331, 41);
		this.StatusStrip1.TabIndex = 2;
		this.StatusStrip1.Text = "StatusStrip1";
		this.StatusLabel.Name = "StatusLabel";
		this.StatusLabel.Size = new System.Drawing.Size(0, 34);
		this.cmdRemove.Location = new System.Drawing.Point(783, 401);
		this.cmdRemove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdRemove.Name = "cmdRemove";
		this.cmdRemove.Size = new System.Drawing.Size(87, 44);
		this.cmdRemove.TabIndex = 3;
		this.cmdRemove.Text = "<<";
		this.ToolTip1.SetToolTip(this.cmdRemove, "Remove highlighted fields from Grid");
		this.cmdRemove.UseVisualStyleBackColor = true;
		this.cmdAdd.Location = new System.Drawing.Point(783, 351);
		this.cmdAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdAdd.Name = "cmdAdd";
		this.cmdAdd.Size = new System.Drawing.Size(87, 44);
		this.cmdAdd.TabIndex = 2;
		this.cmdAdd.Text = ">>";
		this.ToolTip1.SetToolTip(this.cmdAdd, "Add Highlighted fields to Grid");
		this.cmdAdd.UseVisualStyleBackColor = true;
		this.cmdClear.Location = new System.Drawing.Point(783, 451);
		this.cmdClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(87, 44);
		this.cmdClear.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.cmdClear, "Clear All Selections");
		this.cmdClear.UseVisualStyleBackColor = true;
		this.cmdUp.Location = new System.Drawing.Point(783, 501);
		this.cmdUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(87, 44);
		this.cmdUp.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move pattern up");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.Location = new System.Drawing.Point(783, 551);
		this.cmdDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(87, 44);
		this.cmdDown.TabIndex = 8;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Pattern Down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdexcl.Location = new System.Drawing.Point(783, 601);
		this.cmdexcl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdexcl.Name = "cmdexcl";
		this.cmdexcl.Size = new System.Drawing.Size(87, 44);
		this.cmdexcl.TabIndex = 9;
		this.cmdexcl.Tag = "E";
		this.cmdexcl.Text = "Excl (!)";
		this.ToolTip1.SetToolTip(this.cmdexcl, "Change Selected Patterns to Exclude");
		this.cmdexcl.UseVisualStyleBackColor = true;
		this.cmdIncl.Location = new System.Drawing.Point(783, 652);
		this.cmdIncl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdIncl.Name = "cmdIncl";
		this.cmdIncl.Size = new System.Drawing.Size(87, 44);
		this.cmdIncl.TabIndex = 10;
		this.cmdIncl.Tag = "I";
		this.cmdIncl.Text = "Incl";
		this.ToolTip1.SetToolTip(this.cmdIncl, "Change selected Patterns to Include");
		this.cmdIncl.UseVisualStyleBackColor = true;
		this.mnuGridContext.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuGridContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.mnuaddselected, this.ToolStripSeparator1, this.mnuCopy, this.mnudeselectall, this.mnuSelCol, this.ToolStripSeparator2, this.mnuAutoRowHeight2, this.ToolStripSeparator3, this.mnuRefresh2 });
		this.mnuGridContext.Name = "ContextMenuStrip1";
		this.mnuGridContext.Size = new System.Drawing.Size(389, 214);
		this.mnuaddselected.Name = "mnuaddselected";
		this.mnuaddselected.Size = new System.Drawing.Size(388, 32);
		this.mnuaddselected.Text = "Add Highlighted Fields to Output Grid";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(385, 6);
		this.mnuCopy.Name = "mnuCopy";
		this.mnuCopy.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
		this.mnuCopy.Size = new System.Drawing.Size(388, 32);
		this.mnuCopy.Tag = "CELLS";
		this.mnuCopy.Text = "Copy Selected Cells";
		this.mnudeselectall.Name = "mnudeselectall";
		this.mnudeselectall.ShortcutKeys = System.Windows.Forms.Keys.B | System.Windows.Forms.Keys.Control;
		this.mnudeselectall.Size = new System.Drawing.Size(388, 32);
		this.mnudeselectall.Text = "Deselect All";
		this.mnuSelCol.Name = "mnuSelCol";
		this.mnuSelCol.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control;
		this.mnuSelCol.Size = new System.Drawing.Size(388, 32);
		this.mnuSelCol.Text = "Select Column";
		this.mnuSelCol.Visible = false;
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(385, 6);
		this.mnuAutoRowHeight2.Name = "mnuAutoRowHeight2";
		this.mnuAutoRowHeight2.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.mnuAutoRowHeight2.ShowShortcutKeys = false;
		this.mnuAutoRowHeight2.Size = new System.Drawing.Size(388, 32);
		this.mnuAutoRowHeight2.Text = "Auto Row Height";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(385, 6);
		this.mnuRefresh2.Name = "mnuRefresh2";
		this.mnuRefresh2.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.mnuRefresh2.Size = new System.Drawing.Size(388, 32);
		this.mnuRefresh2.Text = "Refresh";
		this.mnuOutGridContext.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuOutGridContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.mnuRemoveLstItem, this.ToolStripSeparator4, this.mnuOutSelectAll, this.mnuOutCopy, this.mnuOutPaste2, this.ToolStripSeparator5, this.mnuHLPaste });
		this.mnuOutGridContext.Name = "mnuListBContext";
		this.mnuOutGridContext.Size = new System.Drawing.Size(625, 176);
		this.mnuRemoveLstItem.Name = "mnuRemoveLstItem";
		this.mnuRemoveLstItem.Size = new System.Drawing.Size(624, 32);
		this.mnuRemoveLstItem.Text = "Remove Selected Items";
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(621, 6);
		this.mnuOutSelectAll.Name = "mnuOutSelectAll";
		this.mnuOutSelectAll.Size = new System.Drawing.Size(624, 32);
		this.mnuOutSelectAll.Text = "Select All";
		this.mnuOutCopy.Name = "mnuOutCopy";
		this.mnuOutCopy.Size = new System.Drawing.Size(624, 32);
		this.mnuOutCopy.Text = "Copy";
		this.mnuOutPaste2.Name = "mnuOutPaste2";
		this.mnuOutPaste2.Size = new System.Drawing.Size(624, 32);
		this.mnuOutPaste2.Text = "Paste";
		this.ToolStripSeparator5.Name = "ToolStripSeparator5";
		this.ToolStripSeparator5.Size = new System.Drawing.Size(621, 6);
		this.mnuHLPaste.Name = "mnuHLPaste";
		this.mnuHLPaste.Size = new System.Drawing.Size(624, 32);
		this.mnuHLPaste.Text = "Make highlighted rows in Col 1 the same as the first highlighted row";
		((System.Windows.Forms.Control)(object)this.FieldGrid).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		((System.Windows.Forms.Control)(object)this.FieldGrid).ContextMenuStrip = this.mnuGridContext;
		this.FieldGrid.DefaultAutoGroupRow.Height = 28;
		this.FieldGrid.DefaultCol.CellStyle = this.IGrid1DefaultCellStyle;
		this.FieldGrid.DefaultCol.ColHdrStyle = this.IGrid1DefaultColHdrStyle;
		this.FieldGrid.DefaultCol.Width = 120;
		this.FieldGrid.DefaultRow.Height = 23;
		this.FieldGrid.DefaultRow.NormalCellHeight = 23;
		this.FieldGrid.DefaultRowHeightAutoSet = false;
		((System.Windows.Forms.Control)(object)this.FieldGrid).Font = new System.Drawing.Font("Calibri", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FieldGrid.Header.Height = 33;
		((System.Windows.Forms.Control)(object)this.FieldGrid).Location = new System.Drawing.Point(0, 9);
		((System.Windows.Forms.Control)(object)this.FieldGrid).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.FieldGrid).Name = "FieldGrid";
		this.FieldGrid.ProcessTab = false;
		this.FieldGrid.ReadOnly = true;
		this.FieldGrid.RowResizeMode = (iGRowResizeMode)0;
		this.FieldGrid.SelectionMode = (iGSelectionMode)3;
		((System.Windows.Forms.Control)(object)this.FieldGrid).Size = new System.Drawing.Size(780, 809);
		((System.Windows.Forms.Control)(object)this.FieldGrid).TabIndex = 0;
		this.FieldGrid.UseXPStyles = false;
		((iGStyleBase)this.IGrid1DefaultCellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
		((iGStyleBase)this.IGrid1DefaultCellStyle).TextTrimming = (iGStringTrimming)0;
		this.SplitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SplitContainer1.Location = new System.Drawing.Point(9, 40);
		this.SplitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.cmdIncl);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdexcl);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdDown);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdUp);
		this.SplitContainer1.Panel1.Controls.Add(this.lblType);
		this.SplitContainer1.Panel1.Controls.Add(this.cmbType);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdClear);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdRemove);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAdd);
		this.SplitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control)(object)this.FieldGrid);
		this.SplitContainer1.Panel2.Controls.Add(this.outGrid);
		this.SplitContainer1.Panel2.Controls.Add(this.CmdCancel2);
		this.SplitContainer1.Panel2.Controls.Add(this.cmdOK2);
		this.SplitContainer1.Panel2.Controls.Add(this.lblfields);
		this.SplitContainer1.Size = new System.Drawing.Size(1322, 825);
		this.SplitContainer1.SplitterDistance = 918;
		this.SplitContainer1.SplitterWidth = 8;
		this.SplitContainer1.TabIndex = 7;
		this.lblType.AutoSize = true;
		this.lblType.Location = new System.Drawing.Point(783, 279);
		this.lblType.Name = "lblType";
		this.lblType.Size = new System.Drawing.Size(43, 20);
		this.lblType.TabIndex = 6;
		this.lblType.Text = "Type";
		this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbType.DropDownWidth = 140;
		this.cmbType.FormattingEnabled = true;
		this.cmbType.Location = new System.Drawing.Point(783, 304);
		this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmbType.Name = "cmbType";
		this.cmbType.Size = new System.Drawing.Size(106, 28);
		this.cmbType.TabIndex = 5;
		this.outGrid.AllowUserToResizeRows = false;
		this.outGrid.BackgroundColor = System.Drawing.Color.White;
		this.outGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.outGrid.Columns.AddRange(this.colField, this.colType);
		this.outGrid.ContextMenuStrip = this.mnuOutGridContext;
		this.outGrid.Location = new System.Drawing.Point(3, 104);
		this.outGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.outGrid.Name = "outGrid";
		this.outGrid.RowHeadersVisible = false;
		this.outGrid.RowHeadersWidth = 51;
		this.outGrid.RowTemplate.Height = 24;
		this.outGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.outGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.outGrid.Size = new System.Drawing.Size(350, 716);
		this.outGrid.TabIndex = 11;
		this.colField.HeaderText = "Field";
		this.colField.MinimumWidth = 6;
		this.colField.Name = "colField";
		this.colField.Width = 125;
		this.colType.DropDownWidth = 140;
		this.colType.HeaderText = "Type";
		this.colType.MinimumWidth = 6;
		this.colType.Name = "colType";
		this.colType.Width = 125;
		this.CmdCancel2.Location = new System.Drawing.Point(112, 9);
		this.CmdCancel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.CmdCancel2.Name = "CmdCancel2";
		this.CmdCancel2.Size = new System.Drawing.Size(101, 55);
		this.CmdCancel2.TabIndex = 1;
		this.CmdCancel2.Text = "Cancel";
		this.CmdCancel2.UseVisualStyleBackColor = true;
		this.cmdOK2.Location = new System.Drawing.Point(8, 9);
		this.cmdOK2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cmdOK2.Name = "cmdOK2";
		this.cmdOK2.Size = new System.Drawing.Size(101, 55);
		this.cmdOK2.TabIndex = 0;
		this.cmdOK2.Text = "OK";
		this.cmdOK2.UseVisualStyleBackColor = true;
		this.lblfields.AutoSize = true;
		this.lblfields.Location = new System.Drawing.Point(4, 79);
		this.lblfields.Name = "lblfields";
		this.lblfields.Size = new System.Drawing.Size(295, 20);
		this.lblfields.TabIndex = 0;
		this.lblfields.Text = "Selected Fields (as Regular Expression):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(1331, 912);
		base.Controls.Add(this.StatusStrip1);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.SplitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "FrmMegaField";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Select from Available Fields";
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		this.StatusStrip1.ResumeLayout(false);
		this.StatusStrip1.PerformLayout();
		this.mnuGridContext.ResumeLayout(false);
		this.mnuOutGridContext.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.FieldGrid).EndInit();
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel1.PerformLayout();
		this.SplitContainer1.Panel2.ResumeLayout(false);
		this.SplitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.outGrid).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Form_Resize()
	{
		checked
		{
			if (gdoFieldSearch)
			{
				int num = SplitContainer1.SplitterDistance - cmbType.Width - 5;
				cmdAdd.Left = num;
				cmdRemove.Left = num;
				cmdClear.Left = num;
				lblType.Left = num;
				cmbType.Left = num;
				cmdUp.Left = num;
				cmdDown.Left = num;
				cmdIncl.Left = num;
				cmdexcl.Left = num;
				((Control)(object)FieldGrid).Width = num - 5;
				((Control)(object)FieldGrid).Height = SplitContainer1.Panel1.Height - 5;
				int num2 = num - 25;
				object obj = mnucmbsearch.Text;
				switch (Strings.LCase(Strings.Trim(Conversions.ToString(obj))))
				{
				case "main":
				case null:
				case "":
					FieldGrid.Cols[0].Width = (int)Math.Round(0.33 * (double)num2);
					FieldGrid.Cols[1].Width = (int)Math.Round(0.66 * (double)num2);
					break;
				default:
					FieldGrid.Cols[0].Width = (int)Math.Round(0.15 * (double)num2);
					FieldGrid.Cols[1].Width = (int)Math.Round(0.15 * (double)num2);
					FieldGrid.Cols[2].Width = (int)Math.Round(0.2 * (double)num2);
					FieldGrid.Cols[3].Width = (int)Math.Round(0.2 * (double)num2);
					FieldGrid.Cols[4].Width = (int)Math.Round(0.3 * (double)num2);
					break;
				}
				int num3 = SplitContainer1.Panel2.Width - 5;
				outGrid.Width = num3;
				outGrid.Columns[0].Width = (int)Math.Round((double)num3 * 0.6);
				outGrid.Columns[1].Width = (int)Math.Round((double)num3 * 0.3);
				outGrid.Height = SplitContainer1.Panel2.Height - 50;
			}
		}
	}

	public void ReadSQLite(string MySearch)
	{
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		string text = "";
		string text2 = "";
		string text3 = "";
		if (Operators.CompareString(f_InType, "KS:Unit", TextCompare: false) == 0)
		{
			text = Globals_Renamed.MySchemaDir + "\\ks_unit_segments.sdb";
			text2 = "KS_UNIT_SEGMENTS";
			text3 = "operation";
		}
		else if (Operators.CompareString(f_InType, "KS:Lot", TextCompare: false) == 0)
		{
			text = Globals_Renamed.MySchemaDir + "\\ks_lot_segments.sdb";
			text2 = "KS_LOT_SEGMENTS";
			text3 = "operation";
			if (Operators.CompareString(Strings.Trim(MySearch), "main", TextCompare: false) == 0)
			{
				MySearch = "ll";
			}
		}
		else if (Operators.CompareString(f_InType, "KS:Wafer", TextCompare: false) == 0)
		{
			text = Globals_Renamed.MySchemaDir + "\\ks_wafer_segments.sdb";
			text2 = "KS_WAFER_SEGMENTS";
			text3 = "operation";
			if (Operators.CompareString(Strings.Trim(MySearch), "main", TextCompare: false) == 0)
			{
				MySearch = "ll";
			}
		}
		else if (Operators.CompareString(f_InType, "KS:MQCS", TextCompare: false) == 0)
		{
			text = Globals_Renamed.MySchemaDir + "\\ks_MQCS_segments.sdb";
			text2 = "KS_MQCS_SEGMENTS";
			text3 = "operation";
		}
		if (Operators.CompareString(Strings.Trim(MySearch), "main", TextCompare: false) == 0)
		{
			MySearch = " main";
		}
		checked
		{
			if (File.Exists(text))
			{
				StatusLabel.Text = "Loading ...";
				Refresh();
				try
				{
					FieldGrid.Rows.Count = 0;
					if (Operators.CompareString(MySearch, " main", TextCompare: false) == 0 || Operators.CompareString(gOldValue, " main", TextCompare: false) == 0)
					{
						FieldGrid.Cols.Count = 0;
						Refresh();
					}
					DoCursor("WAIT");
					SQLiteConnection val = new SQLiteConnection("Data Source=" + text + ";Version=3;");
					val.Open();
					SQLiteCommand val2 = val.CreateCommand();
					if (Operators.CompareString(MySearch, " main", TextCompare: false) == 0)
					{
						val2.CommandText = "Select type As Class, field_name As Field_Name FROM " + text2 + " WHERE level='" + MySearch + "'";
					}
					else
					{
						val2.CommandText = "SELECT type AS Class, " + text3 + " AS Oper, attr1 AS Attr1, attr2 AS Attr2, field_name AS Field_Name FROM " + text2 + " WHERE level='" + MySearch + "'";
					}
					FieldGrid.FillWithData((object)val2);
					val.Close();
					iGFilter.Grid = FieldGrid;
					Form_Resize();
					int num = FieldGrid.Cols.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						FieldGrid.Cols[i].AllowMoving = false;
						FieldGrid.Cols[i].AllowGrouping = false;
					}
					StatusLabel.Text = Conversions.ToString(FieldGrid.Rows.Count) + " rows";
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox("Error processing imBigData Metadata file: " + text + ": " + ex2.Message, MsgBoxStyle.Exclamation, "Error");
					StatusLabel.Text = "";
					ProjectData.ClearProjectError();
				}
				finally
				{
					DoCursor();
				}
			}
			else
			{
				Interaction.MsgBox("File: " + text + " not found to load fields. Contact SQLPathFinder support for assistance.", MsgBoxStyle.Exclamation, "Not Found");
			}
			Application.DoEvents();
			Refresh();
		}
	}

	public void Init_Grid(string MyLabel = "")
	{
		FieldGrid.Cols.Clear();
		FieldGrid.Rows.Clear();
		if (!Information.IsNothing(iGFilter.Grid))
		{
			iGFilter.ClearFilter();
		}
		DoCursor("WAIT");
		if (Operators.CompareString(MyLabel, "", TextCompare: false) != 0)
		{
			StatusLabel.Text = MyLabel;
		}
		else
		{
			StatusLabel.Text = "";
		}
		Refresh();
		FieldGrid.SearchAsType.Mode = (iGSearchAsTypeMode)1;
		FieldGrid.SearchAsType.DisplaySearchText = true;
		FieldGrid.SearchAsType.DisplayKeyboardHint = true;
		FieldGrid.SearchAsType.StartFromCurRow = false;
		FieldGrid.SearchAsType.MatchRule = (iGMatchRule)1;
		FieldGrid.SearchAsType.AutoCancel = true;
	}

	public void ErrService(int ErrNum, string errsource, string ErrDesc)
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
				case 197:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_004e;
						case 5:
							goto IL_005c;
						case 6:
							goto IL_006c;
						case 7:
							goto IL_0074;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005c:
					num2 = 5;
					Interaction.MsgBox(text, MsgBoxStyle.Critical, "SQLPathFinder Error");
					goto IL_006c;
					IL_006c:
					ProjectData.ClearProjectError();
					num3 = 0;
					goto IL_0074;
					IL_004e:
					num2 = 4;
					text += ". Contact SQLPathFinder support if you need assistance.";
					goto IL_005c;
					IL_0074:
					num2 = 7;
					Information.Err().Clear();
					break;
					IL_000b:
					num2 = 2;
					text = "The following Error occurred: \r\n\r\n";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = text + errsource + " : " + ErrDesc + " (Number: " + Conversions.ToString(ErrNum) + ")";
					goto IL_004e;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				DoCursor();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 197;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmCSVViewer_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string[] array = default(string[]);
		string text = default(string);
		string left = default(string);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Button MyButton;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 2912:
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
								goto IL_0042;
							case 7:
								goto IL_0062;
							case 8:
								goto IL_0082;
							case 9:
								goto IL_0095;
							case 10:
								goto IL_00ab;
							case 11:
								goto IL_00c1;
							case 12:
								goto IL_00e1;
							case 13:
								goto IL_00f1;
							case 14:
								goto IL_0101;
							case 15:
								goto IL_0111;
							case 16:
								goto IL_0125;
							case 17:
								goto IL_0160;
							case 18:
								goto IL_0176;
							case 19:
								goto IL_0184;
							case 20:
								goto IL_019b;
							case 21:
							case 22:
								goto IL_01b5;
							case 23:
								goto IL_01cc;
							case 24:
								goto IL_01da;
							case 25:
								goto IL_01f3;
							case 26:
								goto IL_020a;
							case 27:
								goto IL_021f;
							case 28:
								goto IL_022e;
							case 29:
								goto IL_024a;
							case 30:
								goto IL_025d;
							case 31:
								goto IL_026b;
							case 32:
								goto IL_0281;
							case 33:
								goto IL_0296;
							case 34:
							case 36:
							case 37:
								goto IL_02af;
							case 35:
							case 38:
							case 39:
								goto IL_02d5;
							case 40:
								goto IL_02e7;
							case 41:
							case 42:
								goto IL_02f8;
							case 44:
								goto IL_0369;
							case 45:
								goto IL_0377;
							case 46:
								goto IL_0390;
							case 48:
								goto IL_03ac;
							case 49:
								goto IL_03ba;
							case 51:
								goto IL_03d6;
							case 52:
								goto IL_03e4;
							case 54:
								goto IL_0400;
							case 55:
								goto IL_040e;
							case 43:
							case 47:
							case 50:
							case 53:
							case 56:
							case 57:
								goto IL_042a;
							case 59:
								goto IL_0441;
							case 60:
								goto IL_0451;
							case 61:
								goto IL_0471;
							case 62:
								goto IL_0487;
							case 63:
								goto IL_0498;
							case 64:
								goto IL_04aa;
							case 65:
								goto IL_04c0;
							case 66:
								goto IL_0506;
							case 68:
								goto IL_051a;
							case 69:
								goto IL_0537;
							case 71:
								goto IL_0548;
							case 72:
								goto IL_0565;
							case 74:
								goto IL_0576;
							case 75:
								goto IL_0593;
							case 67:
							case 70:
							case 73:
							case 76:
							case 77:
								goto IL_05a2;
							case 78:
								goto IL_05bc;
							case 79:
							case 80:
								goto IL_05d7;
							case 81:
								goto IL_05ee;
							case 82:
								goto IL_05fc;
							case 83:
							case 84:
								goto IL_0617;
							case 85:
								goto IL_062e;
							case 86:
								goto IL_0643;
							case 87:
								goto IL_0652;
							case 88:
								goto IL_066e;
							case 89:
								goto IL_0681;
							case 90:
								goto IL_068f;
							case 91:
								goto IL_06a5;
							case 93:
								goto IL_06be;
							case 94:
								goto IL_06c5;
							case 92:
							case 95:
							case 96:
								goto IL_06d1;
							case 97:
							case 98:
								goto IL_06f7;
							case 99:
								goto IL_0709;
							case 101:
								goto IL_071b;
							case 102:
								goto IL_0729;
							case 100:
							case 103:
							case 104:
								goto IL_0738;
							case 105:
								goto IL_0751;
							case 106:
							case 107:
								goto IL_0763;
							case 108:
								goto IL_077c;
							case 109:
							case 110:
								goto IL_0788;
							case 111:
								goto IL_07ad;
							case 112:
								goto IL_07c1;
							case 113:
								goto IL_07da;
							case 114:
								goto IL_07f3;
							case 115:
								goto IL_080c;
							case 116:
								goto IL_0825;
							case 117:
								goto IL_0842;
							case 119:
								goto IL_0858;
							case 120:
								goto IL_0875;
							case 122:
								goto IL_0888;
							case 123:
								goto IL_08a5;
							case 125:
								goto IL_08b8;
							case 126:
								goto IL_08d5;
							case 58:
							case 118:
							case 121:
							case 124:
							case 127:
							case 128:
							case 129:
								goto IL_08e7;
							case 130:
								goto IL_08f9;
							case 131:
								goto IL_0906;
							case 132:
								goto IL_0919;
							case 133:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 134:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_08b8:
						num2 = 125;
						if (Operators.CompareString(f_InType, "KS:MQCS", TextCompare: false) == 0)
						{
							goto IL_08d5;
						}
						goto IL_08e7;
						IL_08d5:
						num2 = 126;
						mnucmbDataType.SelectedIndex = 3;
						goto IL_08e7;
						IL_08a5:
						num2 = 123;
						mnucmbDataType.SelectedIndex = 2;
						goto IL_08e7;
						IL_08e7:
						num2 = 129;
						Init_Grid("Loading...");
						goto IL_08f9;
						IL_000b:
						num2 = 2;
						array = null;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						text = "";
						goto IL_0019;
						IL_0019:
						num2 = 4;
						left = "I";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						MyButton = cmdUp;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						cmdUp = MyButton;
						goto IL_0042;
						IL_0042:
						num2 = 6;
						MyButton = cmdDown;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						cmdDown = MyButton;
						goto IL_0062;
						IL_0062:
						num2 = 7;
						MyButton = cmdClear;
						BuildForm.Set_Btn_Img(ref MyButton, "clear");
						cmdClear = MyButton;
						goto IL_0082;
						IL_0082:
						num2 = 8;
						FieldGrid.BackColorEvenRows = Color.WhiteSmoke;
						goto IL_0095;
						IL_0095:
						num2 = 9;
						FieldGrid.DefaultRow.Height = 23;
						goto IL_00ab;
						IL_00ab:
						num2 = 10;
						FieldGrid.DefaultRow.NormalCellHeight = 23;
						goto IL_00c1;
						IL_00c1:
						num2 = 11;
						if (Operators.CompareString(f_InMode, "FORM", TextCompare: false) == 0)
						{
							goto IL_00e1;
						}
						goto IL_0441;
						IL_00e1:
						num2 = 12;
						mnuFile.Visible = false;
						goto IL_00f1;
						IL_00f1:
						num2 = 13;
						mnuOptDT.Checked = true;
						goto IL_0101;
						IL_0101:
						num2 = 14;
						mnuOptDT.Enabled = false;
						goto IL_0111;
						IL_0111:
						num2 = 15;
						mnucmbDataType.Items.Clear();
						goto IL_0125;
						IL_0125:
						num2 = 16;
						if (Operators.CompareString(Strings.Mid(f_InData, 1, 12), "Column-iBDP:", TextCompare: false) == 0 && LikeOperator.LikeString(f_InData, "*<SOF>*<EOF>", CompareMethod.Binary))
						{
							goto IL_0160;
						}
						goto IL_02f8;
						IL_08f9:
						num2 = 130;
						Init_Other_Controls();
						goto IL_0906;
						IL_0160:
						num2 = 17;
						num5 = Strings.InStr(f_InData, "<SOF>");
						goto IL_0176;
						IL_0176:
						num2 = 18;
						if (num5 != 0)
						{
							goto IL_0184;
						}
						goto IL_01b5;
						IL_0184:
						num2 = 19;
						f_InType2 = Strings.Mid(f_InData, 13, 2);
						goto IL_019b;
						IL_019b:
						num2 = 20;
						f_InData = Strings.Mid(f_InData, num5 + 5);
						goto IL_01b5;
						IL_01b5:
						num2 = 22;
						num5 = Strings.InStrRev(f_InData, "<EOF>");
						goto IL_01cc;
						IL_01cc:
						num2 = 23;
						if (num5 != 0)
						{
							goto IL_01da;
						}
						goto IL_01f3;
						IL_01da:
						num2 = 24;
						f_InData = Strings.Mid(f_InData, 1, num5 - 1);
						goto IL_01f3;
						IL_01f3:
						num2 = 25;
						array = Strings.Split(f_InData, "<;>");
						goto IL_020a;
						IL_020a:
						num2 = 26;
						num6 = Information.UBound(array);
						num7 = 0;
						goto IL_02de;
						IL_02de:
						if (num7 <= num6)
						{
							goto IL_021f;
						}
						goto IL_02e7;
						IL_02e7:
						num2 = 40;
						f_InData = "";
						goto IL_02f8;
						IL_021f:
						num2 = 27;
						text2 = Strings.Trim(array[num7]);
						goto IL_022e;
						IL_022e:
						num2 = 28;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_024a;
						}
						goto IL_02d5;
						IL_024a:
						num2 = 29;
						num5 = Strings.InStrRev(text2, ";");
						goto IL_025d;
						IL_025d:
						num2 = 30;
						if (num5 != 0)
						{
							goto IL_026b;
						}
						goto IL_02d5;
						IL_026b:
						num2 = 31;
						text3 = Strings.Trim(Strings.Mid(text2, 1, num5 - 1));
						goto IL_0281;
						IL_0281:
						num2 = 32;
						text4 = Strings.Trim(Strings.Mid(text2, num5 + 1));
						goto IL_0296;
						IL_0296:
						num2 = 33;
						text4 = Conversions.ToString(Return_ColType(text4));
						goto IL_02af;
						IL_02af:
						num2 = 37;
						outGrid.Rows.Add(text3, text4);
						goto IL_02d5;
						IL_02d5:
						num2 = 39;
						num7++;
						goto IL_02de;
						IL_02f8:
						num2 = 42;
						switch (f_InType2)
						{
						case "KU":
						case "KD":
						case "KP":
							break;
						case "KL":
							goto IL_03ac;
						case "KW":
							goto IL_03d6;
						case "KM":
							goto IL_0400;
						default:
							goto IL_042a;
						}
						goto IL_0369;
						IL_0400:
						num2 = 54;
						f_InType = "KS:MQCS";
						goto IL_040e;
						IL_040e:
						num2 = 55;
						mnucmbDataType.Items.Add("KS:MQCS");
						goto IL_042a;
						IL_03d6:
						num2 = 51;
						f_InType = "KS:Wafer";
						goto IL_03e4;
						IL_03e4:
						num2 = 52;
						mnucmbDataType.Items.Add("KS:Wafer");
						goto IL_042a;
						IL_03ac:
						num2 = 48;
						f_InType = "KS:Lot";
						goto IL_03ba;
						IL_03ba:
						num2 = 49;
						mnucmbDataType.Items.Add("KS:Lot");
						goto IL_042a;
						IL_0369:
						num2 = 44;
						f_InType = "KS:Unit";
						goto IL_0377;
						IL_0377:
						num2 = 45;
						mnucmbDataType.Items.Add("KS:Unit/Die/Patch");
						goto IL_0390;
						IL_0390:
						num2 = 46;
						mnucmbDataType.Items.Add("KS:MQCS");
						goto IL_042a;
						IL_042a:
						num2 = 57;
						mnucmbDataType.SelectedIndex = 0;
						goto IL_08e7;
						IL_0441:
						num2 = 59;
						mnuOptDT.Visible = true;
						goto IL_0451;
						IL_0451:
						num2 = 60;
						if (Operators.CompareString(f_InData, "", TextCompare: false) != 0)
						{
							goto IL_0471;
						}
						goto IL_071b;
						IL_0471:
						num2 = 61;
						num5 = Strings.InStr(f_InData, "<SOF>pattern,type");
						goto IL_0487;
						IL_0487:
						num2 = 62;
						if (num5 != 0)
						{
							goto IL_0498;
						}
						goto IL_05d7;
						IL_0498:
						num2 = 63;
						left = Strings.Mid(f_InData, 1, 1);
						goto IL_04aa;
						IL_04aa:
						num2 = 64;
						f_InType2 = Strings.Mid(f_InData, 2, 2);
						goto IL_04c0;
						IL_04c0:
						num2 = 65;
						if (Operators.CompareString(f_InType2, "KU", TextCompare: false) == 0 || Operators.CompareString(f_InType2, "KD", TextCompare: false) == 0 || Operators.CompareString(f_InType2, "KP", TextCompare: false) == 0)
						{
							goto IL_0506;
						}
						goto IL_051a;
						IL_0906:
						num2 = 131;
						ReadSQLite(gOldValue);
						goto IL_0919;
						IL_0919:
						num2 = 132;
						gdoFieldSearch = true;
						break;
						IL_051a:
						num2 = 68;
						if (Operators.CompareString(f_InType2, "KL", TextCompare: false) == 0)
						{
							goto IL_0537;
						}
						goto IL_0548;
						IL_0537:
						num2 = 69;
						f_InType = "KS:Lot";
						goto IL_05a2;
						IL_0548:
						num2 = 71;
						if (Operators.CompareString(f_InType2, "KW", TextCompare: false) == 0)
						{
							goto IL_0565;
						}
						goto IL_0576;
						IL_0565:
						num2 = 72;
						f_InType = "KS:Wafer";
						goto IL_05a2;
						IL_0576:
						num2 = 74;
						if (Operators.CompareString(f_InType2, "KM", TextCompare: false) == 0)
						{
							goto IL_0593;
						}
						goto IL_05a2;
						IL_0593:
						num2 = 75;
						f_InType = "KS:MQCS";
						goto IL_05a2;
						IL_0506:
						num2 = 66;
						f_InType = "KS:Unit";
						goto IL_05a2;
						IL_05a2:
						num2 = 77;
						text = Strings.Trim(Strings.Mid(f_InData, 4, num5 - 4));
						goto IL_05bc;
						IL_05bc:
						num2 = 78;
						f_InData = Strings.Mid(f_InData, num5 + 17);
						goto IL_05d7;
						IL_05d7:
						num2 = 80;
						num5 = Strings.InStrRev(f_InData, "<EOF>");
						goto IL_05ee;
						IL_05ee:
						num2 = 81;
						if (num5 != 0)
						{
							goto IL_05fc;
						}
						goto IL_0617;
						IL_05fc:
						num2 = 82;
						f_InData = Strings.Mid(f_InData, 1, num5 - 1);
						goto IL_0617;
						IL_0617:
						num2 = 84;
						array = Strings.Split(f_InData, "\r\n");
						goto IL_062e;
						IL_062e:
						num2 = 85;
						num8 = Information.UBound(array);
						num7 = 0;
						goto IL_0700;
						IL_0700:
						if (num7 <= num8)
						{
							goto IL_0643;
						}
						goto IL_0709;
						IL_0709:
						num2 = 99;
						f_InData = "";
						goto IL_0738;
						IL_0643:
						num2 = 86;
						text2 = Strings.Trim(array[num7]);
						goto IL_0652;
						IL_0652:
						num2 = 87;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_066e;
						}
						goto IL_06f7;
						IL_066e:
						num2 = 88;
						num5 = Strings.InStrRev(text2, ",");
						goto IL_0681;
						IL_0681:
						num2 = 89;
						if (num5 != 0)
						{
							goto IL_068f;
						}
						goto IL_06be;
						IL_068f:
						num2 = 90;
						text3 = Strings.Trim(Strings.Mid(text2, 1, num5 - 1));
						goto IL_06a5;
						IL_06a5:
						num2 = 91;
						text4 = Strings.Trim(Strings.Mid(text2, num5 + 1));
						goto IL_06d1;
						IL_06be:
						num2 = 93;
						text3 = text2;
						goto IL_06c5;
						IL_06c5:
						num2 = 94;
						text4 = f_InType;
						goto IL_06d1;
						IL_06d1:
						num2 = 96;
						outGrid.Rows.Add(text3, text4);
						goto IL_06f7;
						IL_06f7:
						num2 = 98;
						num7++;
						goto IL_0700;
						IL_071b:
						num2 = 101;
						f_InType2 = "KU";
						goto IL_0729;
						IL_0729:
						num2 = 102;
						f_InType = "KS:Unit";
						goto IL_0738;
						IL_0738:
						num2 = 104;
						if (Operators.CompareString(left, "I", TextCompare: false) == 0)
						{
							goto IL_0751;
						}
						goto IL_0763;
						IL_0751:
						num2 = 105;
						mnuOptDT.Checked = true;
						goto IL_0763;
						IL_0763:
						num2 = 107;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_077c;
						}
						goto IL_0788;
						IL_077c:
						num2 = 108;
						text = "MyInput.txt";
						goto IL_0788;
						IL_0788:
						num2 = 110;
						mnuOutWF.Text = "Output File {" + Strings.Trim(text) + "}";
						goto IL_07ad;
						IL_07ad:
						num2 = 111;
						mnucmbDataType.Items.Clear();
						goto IL_07c1;
						IL_07c1:
						num2 = 112;
						mnucmbDataType.Items.Add("KS:Unit/Die/Patch");
						goto IL_07da;
						IL_07da:
						num2 = 113;
						mnucmbDataType.Items.Add("KS:Lot");
						goto IL_07f3;
						IL_07f3:
						num2 = 114;
						mnucmbDataType.Items.Add("KS:Wafer");
						goto IL_080c;
						IL_080c:
						num2 = 115;
						mnucmbDataType.Items.Add("KS:MQCS");
						goto IL_0825;
						IL_0825:
						num2 = 116;
						if (Operators.CompareString(f_InType, "KS:Unit", TextCompare: false) == 0)
						{
							goto IL_0842;
						}
						goto IL_0858;
						IL_0842:
						num2 = 117;
						mnucmbDataType.SelectedIndex = 0;
						goto IL_08e7;
						IL_0858:
						num2 = 119;
						if (Operators.CompareString(f_InType, "KS:Lot", TextCompare: false) == 0)
						{
							goto IL_0875;
						}
						goto IL_0888;
						IL_0875:
						num2 = 120;
						mnucmbDataType.SelectedIndex = 1;
						goto IL_08e7;
						IL_0888:
						num2 = 122;
						if (Operators.CompareString(f_InType, "KS:Wafer", TextCompare: false) == 0)
						{
							goto IL_08a5;
						}
						goto IL_08b8;
						end_IL_0001_2:
						break;
					}
					num2 = 133;
					Form_Resize();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2912;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Init_Other_Controls()
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
				case 1601:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003a;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_006a;
						case 8:
							goto IL_0082;
						case 9:
							goto IL_009a;
						case 10:
							goto IL_00b3;
						case 11:
							goto IL_00cc;
						case 12:
							goto IL_00e5;
						case 13:
							goto IL_00fe;
						case 15:
							goto IL_011f;
						case 17:
							goto IL_014b;
						case 19:
							goto IL_016a;
						case 21:
							goto IL_0186;
						case 22:
							goto IL_019f;
						case 23:
							goto IL_01b8;
						case 24:
							goto IL_01d1;
						case 16:
						case 18:
						case 20:
						case 25:
						case 26:
							goto IL_01ed;
						case 14:
						case 27:
						case 28:
							goto IL_0207;
						case 29:
							goto IL_0227;
						case 30:
							goto IL_0235;
						case 31:
							goto IL_0243;
						case 32:
							goto IL_025c;
						case 33:
							goto IL_0275;
						case 34:
							goto IL_028e;
						case 35:
							goto IL_02a7;
						case 36:
							goto IL_02c0;
						case 37:
							goto IL_02d9;
						case 39:
							goto IL_02f8;
						case 40:
							goto IL_0315;
						case 41:
							goto IL_0323;
						case 42:
							goto IL_0331;
						case 43:
							goto IL_034a;
						case 45:
							goto IL_0369;
						case 46:
							goto IL_0386;
						case 47:
							goto IL_0394;
						case 48:
							goto IL_03a2;
						case 49:
							goto IL_03bb;
						case 50:
							goto IL_03d4;
						case 52:
							goto IL_03f0;
						case 53:
							goto IL_040d;
						case 54:
							goto IL_041b;
						case 55:
							goto IL_0429;
						case 56:
							goto IL_0442;
						case 38:
						case 44:
						case 51:
						case 57:
						case 58:
							goto IL_045c;
						case 59:
							goto IL_0479;
						case 60:
							goto IL_0496;
						case 62:
							goto IL_04a9;
						case 63:
							goto IL_04c6;
						case 65:
							goto IL_04da;
						case 68:
							goto IL_04ef;
						case 61:
						case 64:
						case 66:
						case 67:
						case 69:
						case 70:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 71:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_04a9:
					num2 = 62;
					if (Operators.CompareString(f_InType2, "KP", TextCompare: false) == 0)
					{
						goto IL_04c6;
					}
					goto IL_04da;
					IL_04c6:
					num2 = 63;
					cmbType.SelectedIndex = 2;
					break;
					IL_0496:
					num2 = 60;
					cmbType.SelectedIndex = 1;
					break;
					IL_04da:
					num2 = 65;
					cmbType.SelectedIndex = 0;
					break;
					IL_000b:
					num2 = 2;
					gdoFieldSearch = false;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					cmbType.Items.Clear();
					goto IL_0027;
					IL_0027:
					num2 = 4;
					mnucmbsearch.Items.Clear();
					goto IL_003a;
					IL_003a:
					num2 = 5;
					colType.Items.Clear();
					goto IL_004d;
					IL_004d:
					num2 = 6;
					if (Operators.CompareString(f_InMode, "UTIL", TextCompare: false) == 0)
					{
						goto IL_006a;
					}
					goto IL_011f;
					IL_006a:
					num2 = 7;
					colType.Items.Add("KS:Unit");
					goto IL_0082;
					IL_0082:
					num2 = 8;
					colType.Items.Add("KS:Die");
					goto IL_009a;
					IL_009a:
					num2 = 9;
					colType.Items.Add("KS:Patch");
					goto IL_00b3;
					IL_00b3:
					num2 = 10;
					colType.Items.Add("KS:Lot");
					goto IL_00cc;
					IL_00cc:
					num2 = 11;
					colType.Items.Add("KS:Wafer");
					goto IL_00e5;
					IL_00e5:
					num2 = 12;
					colType.Items.Add("KS:MQCS");
					goto IL_00fe;
					IL_00fe:
					num2 = 13;
					colType.Items.Add("");
					goto IL_0207;
					IL_011f:
					num2 = 15;
					left = f_InType2;
					if (Operators.CompareString(left, "KL", TextCompare: false) == 0)
					{
						goto IL_014b;
					}
					if (Operators.CompareString(left, "KW", TextCompare: false) == 0)
					{
						goto IL_016a;
					}
					goto IL_0186;
					IL_04ef:
					num2 = 68;
					cmbType.SelectedIndex = 0;
					break;
					IL_0186:
					num2 = 21;
					colType.Items.Add("KS:Unit");
					goto IL_019f;
					IL_019f:
					num2 = 22;
					colType.Items.Add("KS:Die");
					goto IL_01b8;
					IL_01b8:
					num2 = 23;
					colType.Items.Add("KS:Patch");
					goto IL_01d1;
					IL_01d1:
					num2 = 24;
					colType.Items.Add("KS:MQCS");
					goto IL_01ed;
					IL_016a:
					num2 = 19;
					colType.Items.Add("KS:Wafer");
					goto IL_01ed;
					IL_014b:
					num2 = 17;
					colType.Items.Add("KS:Lot");
					goto IL_01ed;
					IL_01ed:
					num2 = 26;
					colType.Items.Add("");
					goto IL_0207;
					IL_0207:
					num2 = 28;
					if (Operators.CompareString(f_InType, "KS:Unit", TextCompare: false) == 0)
					{
						goto IL_0227;
					}
					goto IL_02f8;
					IL_0227:
					num2 = 29;
					gOldDataType = "KS:Unit/Die/Patch";
					goto IL_0235;
					IL_0235:
					num2 = 30;
					gOldValue = "main";
					goto IL_0243;
					IL_0243:
					num2 = 31;
					cmbType.Items.Add("KS:Unit");
					goto IL_025c;
					IL_025c:
					num2 = 32;
					cmbType.Items.Add("KS:Die");
					goto IL_0275;
					IL_0275:
					num2 = 33;
					cmbType.Items.Add("KS:Patch");
					goto IL_028e;
					IL_028e:
					num2 = 34;
					mnucmbsearch.Items.Add("main");
					goto IL_02a7;
					IL_02a7:
					num2 = 35;
					mnucmbsearch.Items.Add("ll");
					goto IL_02c0;
					IL_02c0:
					num2 = 36;
					mnucmbsearch.Items.Add("ul");
					goto IL_02d9;
					IL_02d9:
					num2 = 37;
					mnucmbsearch.Items.Add("wl");
					goto IL_045c;
					IL_02f8:
					num2 = 39;
					if (Operators.CompareString(f_InType, "KS:Lot", TextCompare: false) == 0)
					{
						goto IL_0315;
					}
					goto IL_0369;
					IL_0315:
					num2 = 40;
					gOldDataType = "KS:Lot";
					goto IL_0323;
					IL_0323:
					num2 = 41;
					gOldValue = "ll";
					goto IL_0331;
					IL_0331:
					num2 = 42;
					cmbType.Items.Add("KS:Lot");
					goto IL_034a;
					IL_034a:
					num2 = 43;
					mnucmbsearch.Items.Add("ll");
					goto IL_045c;
					IL_0369:
					num2 = 45;
					if (Operators.CompareString(f_InType, "KS:Wafer", TextCompare: false) == 0)
					{
						goto IL_0386;
					}
					goto IL_03f0;
					IL_0386:
					num2 = 46;
					gOldDataType = "KS:Wafer";
					goto IL_0394;
					IL_0394:
					num2 = 47;
					gOldValue = "ll";
					goto IL_03a2;
					IL_03a2:
					num2 = 48;
					cmbType.Items.Add("KS:Wafer");
					goto IL_03bb;
					IL_03bb:
					num2 = 49;
					mnucmbsearch.Items.Add("ll");
					goto IL_03d4;
					IL_03d4:
					num2 = 50;
					mnucmbsearch.Items.Add("wl");
					goto IL_045c;
					IL_03f0:
					num2 = 52;
					if (Operators.CompareString(f_InType, "KS:MQCS", TextCompare: false) == 0)
					{
						goto IL_040d;
					}
					goto IL_045c;
					IL_040d:
					num2 = 53;
					gOldDataType = "KS:MQCS";
					goto IL_041b;
					IL_041b:
					num2 = 54;
					gOldValue = "main";
					goto IL_0429;
					IL_0429:
					num2 = 55;
					cmbType.Items.Add("KS:MQCS");
					goto IL_0442;
					IL_0442:
					num2 = 56;
					mnucmbsearch.Items.Add("main");
					goto IL_045c;
					IL_045c:
					num2 = 58;
					if (Operators.CompareString(f_InType, "KS:Unit", TextCompare: false) == 0)
					{
						goto IL_0479;
					}
					goto IL_04ef;
					IL_0479:
					num2 = 59;
					if (Operators.CompareString(f_InType2, "KD", TextCompare: false) == 0)
					{
						goto IL_0496;
					}
					goto IL_04a9;
					end_IL_0001_2:
					break;
				}
				num2 = 70;
				mnucmbsearch.SelectedIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1601;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuRefresh_Click(object sender, EventArgs e)
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
				Refresh();
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

	public void Add_Filter_Count_Label(int MyMode)
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 564:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0035;
						case 6:
							goto IL_0045;
						case 7:
							goto IL_004b;
						case 9:
							goto IL_006f;
						case 10:
							goto IL_007d;
						case 13:
							goto IL_00b6;
						case 14:
							goto IL_00cb;
						case 16:
							goto IL_00f2;
						case 17:
							goto IL_0102;
						case 18:
							goto IL_0110;
						case 19:
							goto IL_0125;
						case 20:
							goto IL_0133;
						case 22:
							goto IL_0158;
						case 25:
							goto IL_017f;
						case 26:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 8:
						case 11:
						case 12:
						case 15:
						case 21:
						case 23:
						case 24:
						case 27:
						case 28:
						case 29:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cb:
					num2 = 14;
					StatusLabel.Text = Conversions.ToString(num5) + " of " + text;
					goto end_IL_0001_3;
					IL_006f:
					num2 = 9;
					if (num6 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_007d;
					IL_00b6:
					num2 = 13;
					num5 = FieldGrid.Rows.Count;
					goto IL_00cb;
					IL_007d:
					num2 = 10;
					StatusLabel.Text = Conversions.ToString(FieldGrid.Rows.Count) + Strings.Mid(text, num6);
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					text = StatusLabel.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0035:
					num2 = 5;
					num6 = Strings.InStr(text, " of ");
					goto IL_0045;
					IL_0045:
					num2 = 6;
					num5 = 0;
					goto IL_004b;
					IL_004b:
					num2 = 7;
					switch (MyMode)
					{
					case 0:
						break;
					case 1:
						goto IL_00b6;
					case 2:
						goto IL_00f2;
					case -1:
						goto IL_017f;
					default:
						goto end_IL_0001_3;
					}
					goto IL_006f;
					IL_017f:
					num2 = 25;
					if (num6 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_00f2:
					num2 = 16;
					num5 = iGFilter.FilteredRowCount;
					goto IL_0102;
					IL_0102:
					num2 = 17;
					if (num5 == -1)
					{
						goto IL_0110;
					}
					goto IL_0125;
					IL_0110:
					num2 = 18;
					num5 = FieldGrid.Rows.Count;
					goto IL_0125;
					IL_0125:
					num2 = 19;
					if (num6 == 0)
					{
						goto IL_0133;
					}
					goto IL_0158;
					IL_0133:
					num2 = 20;
					StatusLabel.Text = Conversions.ToString(num5) + " of " + text;
					goto end_IL_0001_3;
					IL_0158:
					num2 = 22;
					StatusLabel.Text = Conversions.ToString(num5) + Strings.Mid(text, num6);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 26;
				StatusLabel.Text = Strings.Mid(text, checked(num6 + 4));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 564;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
					iGFilter.ClearFilter();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Add_Filter_Count_Label(0);
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

	public void DoCursor(string MyMode = "DEFAULT")
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
				case 135:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0026;
						case 6:
							goto IL_0037;
						case 3:
						case 5:
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
					IL_000c:
					num2 = 2;
					left = Strings.UCase(MyMode);
					if (Operators.CompareString(left, "WAIT", TextCompare: false) == 0)
					{
						goto IL_0026;
					}
					goto IL_0037;
					IL_0037:
					num2 = 6;
					Cursor = Cursors.Default;
					break;
					IL_0026:
					num2 = 4;
					Cursor = Cursors.WaitCursor;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Refresh();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 135;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GetFirstLastSelectedColOrder(ref iGrid MyGrid, ref int firstSelectedColOrder, ref int lastSelectedColOrder)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		iGCell val = default(iGCell);
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
							goto IL_0014;
						case 4:
							goto IL_001d;
						case 5:
							goto IL_003b;
						case 6:
							goto IL_0053;
						case 7:
						case 8:
							goto IL_0065;
						case 9:
							goto IL_007d;
						case 10:
						case 11:
							goto IL_0090;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0053:
					num2 = 6;
					lastSelectedColOrder = val.Col.Order;
					goto IL_0065;
					IL_0065:
					num2 = 8;
					if (val.Col.Order < firstSelectedColOrder)
					{
						goto IL_007d;
					}
					goto IL_0090;
					IL_003b:
					num2 = 5;
					if (val.Col.Order > lastSelectedColOrder)
					{
						goto IL_0053;
					}
					goto IL_0065;
					IL_007d:
					num2 = 9;
					firstSelectedColOrder = val.Col.Order;
					goto IL_0090;
					IL_000b:
					num2 = 2;
					firstSelectedColOrder = int.MaxValue;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					lastSelectedColOrder = int.MinValue;
					goto IL_001d;
					IL_001d:
					num2 = 4;
					enumerator = MyGrid.SelectedCells.GetEnumerator();
					goto IL_0093;
					IL_0093:
					if (!enumerator.MoveNext())
					{
						break;
					}
					val = (iGCell)enumerator.Current;
					goto IL_003b;
					IL_0090:
					num2 = 11;
					goto IL_0093;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
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

	private void CopyToClipboard(ref iGrid MyGrid, bool fCopyColumnHeaders)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		StringBuilder stringBuilder = default(StringBuilder);
		iGCell val = default(iGCell);
		int num5 = default(int);
		int lastSelectedColOrder = default(int);
		bool selected = default(bool);
		int firstSelectedColOrder = default(int);
		int num7 = default(int);
		int num8 = default(int);
		iGCol val2 = default(iGCol);
		int num9 = default(int);
		int num10 = default(int);
		int num12 = default(int);
		int num13 = default(int);
		int num15 = default(int);
		iGCol val3 = default(iGCol);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					int num11;
					int num14;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 860:
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
								goto IL_0025;
							case 5:
							case 6:
								goto IL_0040;
							case 7:
								goto IL_004e;
							case 8:
								goto IL_0052;
							case 9:
								goto IL_0057;
							case 10:
								goto IL_0066;
							case 11:
								goto IL_0070;
							case 12:
								goto IL_007d;
							case 13:
								goto IL_008d;
							case 14:
								goto IL_00a0;
							case 15:
								goto IL_00b0;
							case 16:
								goto IL_00c7;
							case 17:
								goto IL_00d9;
							case 18:
							case 19:
							case 20:
								goto IL_00ed;
							case 21:
								goto IL_00fc;
							case 22:
							case 23:
								goto IL_010e;
							case 24:
								goto IL_0114;
							case 25:
								goto IL_011a;
							case 26:
								goto IL_0131;
							case 27:
								goto IL_0155;
							case 28:
								goto IL_0169;
							case 29:
								goto IL_0194;
							case 30:
								goto IL_01a7;
							case 31:
								goto IL_01ba;
							case 32:
								goto IL_01ca;
							case 33:
								goto IL_01e4;
							case 34:
								goto IL_01f0;
							case 35:
								goto IL_01fb;
							case 36:
							case 37:
								goto IL_020f;
							case 38:
								goto IL_0221;
							case 39:
							case 40:
							case 41:
								goto IL_0235;
							case 42:
								goto IL_0247;
							case 43:
							case 44:
								goto IL_0259;
							case 45:
								goto IL_026b;
							case 46:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 4:
							case 47:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_01fb:
						num2 = 35;
						stringBuilder.Append(val.Text);
						goto IL_020f;
						IL_020f:
						num2 = 37;
						if (num5 != lastSelectedColOrder)
						{
							goto IL_0221;
						}
						goto IL_0235;
						IL_01f0:
						num2 = 34;
						if (selected)
						{
							goto IL_01fb;
						}
						goto IL_020f;
						IL_0221:
						num2 = 38;
						stringBuilder.Append("\t");
						goto IL_0235;
						IL_000b:
						num2 = 2;
						if (MyGrid.SelectedCells.Count <= 0)
						{
							goto IL_0025;
						}
						goto IL_0040;
						IL_0025:
						num2 = 3;
						Interaction.MsgBox("Nothing Selected", MsgBoxStyle.Information, "Copy");
						goto end_IL_0001_3;
						IL_0040:
						num2 = 6;
						DoCursor("WAIT");
						goto IL_004e;
						IL_004e:
						num2 = 7;
						firstSelectedColOrder = 0;
						goto IL_0052;
						IL_0052:
						num2 = 8;
						lastSelectedColOrder = 0;
						goto IL_0057;
						IL_0057:
						num2 = 9;
						GetFirstLastSelectedColOrder(ref MyGrid, ref firstSelectedColOrder, ref lastSelectedColOrder);
						goto IL_0066;
						IL_0066:
						num2 = 10;
						stringBuilder = new StringBuilder();
						goto IL_0070;
						IL_0070:
						num2 = 11;
						if (fCopyColumnHeaders)
						{
							goto IL_007d;
						}
						goto IL_010e;
						IL_007d:
						num2 = 12;
						num6 = firstSelectedColOrder;
						num7 = lastSelectedColOrder;
						num8 = num6;
						goto IL_00f6;
						IL_00f6:
						if (num8 <= num7)
						{
							goto IL_008d;
						}
						goto IL_00fc;
						IL_00fc:
						num2 = 21;
						stringBuilder.Append("\r\n");
						goto IL_010e;
						IL_008d:
						num2 = 13;
						val2 = MyGrid.Cols.FromOrder(num8);
						goto IL_00a0;
						IL_00a0:
						num2 = 14;
						if (val2.Visible)
						{
							goto IL_00b0;
						}
						goto IL_00ed;
						IL_00b0:
						num2 = 15;
						stringBuilder.Append(RuntimeHelpers.GetObjectValue(val2.Text));
						goto IL_00c7;
						IL_00c7:
						num2 = 16;
						if (num8 != lastSelectedColOrder)
						{
							goto IL_00d9;
						}
						goto IL_00ed;
						IL_00d9:
						num2 = 17;
						stringBuilder.Append("\t");
						goto IL_00ed;
						IL_00ed:
						num2 = 20;
						num8++;
						goto IL_00f6;
						IL_010e:
						num2 = 23;
						num9 = 0;
						goto IL_0114;
						IL_0114:
						num2 = 24;
						num10 = 0;
						goto IL_011a;
						IL_011a:
						num2 = 25;
						num9 = MyGrid.SelectedCells[0].RowIndex;
						goto IL_0131;
						IL_0131:
						num2 = 26;
						num10 = MyGrid.SelectedCells[MyGrid.SelectedCells.Count - 1].RowIndex;
						goto IL_0155;
						IL_0155:
						num2 = 27;
						num11 = num9;
						num12 = num10;
						num13 = num11;
						goto IL_0262;
						IL_0262:
						if (num13 <= num12)
						{
							goto IL_0169;
						}
						goto IL_026b;
						IL_026b:
						num2 = 45;
						Clipboard.SetDataObject(stringBuilder.ToString(), copy: true);
						break;
						IL_0169:
						num2 = 28;
						if (MyGrid.Rows[num13].Visible || MyGrid.SelectInvisibleCells)
						{
							goto IL_0194;
						}
						goto IL_0259;
						IL_0235:
						num2 = 41;
						num5++;
						goto IL_023e;
						IL_0194:
						num2 = 29;
						num14 = firstSelectedColOrder;
						num15 = lastSelectedColOrder;
						num5 = num14;
						goto IL_023e;
						IL_023e:
						if (num5 <= num15)
						{
							goto IL_01a7;
						}
						goto IL_0247;
						IL_0247:
						num2 = 42;
						stringBuilder.Append("\r\n");
						goto IL_0259;
						IL_0259:
						num2 = 44;
						num13++;
						goto IL_0262;
						IL_01a7:
						num2 = 30;
						val3 = MyGrid.Cols.FromOrder(num5);
						goto IL_01ba;
						IL_01ba:
						num2 = 31;
						if (val3.Visible)
						{
							goto IL_01ca;
						}
						goto IL_0235;
						IL_01ca:
						num2 = 32;
						val = MyGrid.Cells[num13, val3.Index];
						goto IL_01e4;
						IL_01e4:
						num2 = 33;
						selected = val.Selected;
						goto IL_01f0;
						end_IL_0001_2:
						break;
					}
					num2 = 46;
					DoCursor();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 860;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuCopy_Click(object sender, EventArgs e)
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
				iGrid MyGrid = FieldGrid;
				CopyToClipboard(ref MyGrid, fCopyColumnHeaders: false);
				FieldGrid = MyGrid;
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

	private void mnudeselectall_Click(object sender, EventArgs e)
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
				case 111:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					if (!FieldGrid.RowMode)
					{
						break;
					}
					goto IL_001c;
					IL_001c:
					num2 = 3;
					FieldGrid.PerformAction((iGActions)15);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				FieldGrid.PerformAction((iGActions)13);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 111;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void iGFilter_FilterApplied(object sender, iGFilterAppliedEventArgs e)
	{
		Add_Filter_Count_Label(2);
	}

	private void mnuAutoRowHeight_Click(object sender, EventArgs e)
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
				case 132:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0027;
						case 5:
							goto IL_003a;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0019:
					num2 = 3;
					FieldGrid.BeginUpdate();
					goto IL_0027;
					IL_0027:
					num2 = 4;
					FieldGrid.Rows.AutoHeight();
					goto IL_003a;
					IL_000b:
					num2 = 2;
					Cursor = Cursors.WaitCursor;
					goto IL_0019;
					IL_003a:
					num2 = 5;
					FieldGrid.EndUpdate();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				Cursor = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 132;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnucmbsearch_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (gdoFieldSearch)
		{
			string text = mnucmbsearch.Text;
			if (Operators.CompareString(text, gOldValue, TextCompare: false) != 0)
			{
				ReadSQLite(text);
				gOldValue = text;
			}
		}
	}

	private void SplitContainer1_Resize(object sender, EventArgs e)
	{
		Form_Resize();
	}

	private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
	{
		Form_Resize();
	}

	private void AddSelectedCol(ref iGrid MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		iGCell val = default(iGCell);
		int num6 = default(int);
		object objectValue = default(object);
		int firstSelectedColOrder = default(int);
		int lastSelectedColOrder = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int num11 = default(int);
		iGCol val2 = default(iGCol);
		bool selected = default(bool);
		int num12 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num9;
					int num10;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1835:
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
								goto IL_0039;
							case 6:
								goto IL_0053;
							case 8:
							case 9:
								goto IL_006e;
							case 10:
								goto IL_007d;
							case 11:
								goto IL_0083;
							case 12:
								goto IL_0089;
							case 13:
								goto IL_0098;
							case 14:
								goto IL_00c6;
							case 17:
								goto IL_00dd;
							case 18:
								goto IL_010a;
							case 19:
								goto IL_0133;
							case 16:
							case 21:
							case 22:
								goto IL_0148;
							case 23:
								goto IL_014e;
							case 24:
								goto IL_0154;
							case 25:
								goto IL_015a;
							case 26:
								goto IL_0171;
							case 27:
								goto IL_0195;
							case 28:
								goto IL_01a4;
							case 29:
								goto IL_01b8;
							case 30:
								goto IL_01e3;
							case 31:
								goto IL_01f6;
							case 32:
								goto IL_0209;
							case 33:
								goto IL_0223;
							case 34:
								goto IL_022f;
							case 35:
								goto IL_0258;
							case 37:
								goto IL_025f;
							case 38:
								goto IL_026d;
							case 39:
								goto IL_028b;
							case 41:
								goto IL_029b;
							case 44:
								goto IL_02bc;
							case 45:
								goto IL_02cc;
							case 47:
								goto IL_02f9;
							case 48:
								goto IL_030a;
							case 49:
								goto IL_0330;
							case 50:
								goto IL_037e;
							case 52:
								goto IL_0393;
							case 55:
								goto IL_03aa;
							case 56:
								goto IL_03bb;
							case 57:
								goto IL_0412;
							case 58:
								goto IL_043d;
							case 60:
								goto IL_0452;
							case 63:
								goto IL_0469;
							case 64:
								goto IL_047a;
							case 65:
								goto IL_04f0;
							case 66:
								goto IL_051b;
							case 68:
								goto IL_0530;
							case 36:
							case 40:
							case 42:
							case 43:
							case 46:
							case 51:
							case 53:
							case 54:
							case 59:
							case 61:
							case 62:
							case 67:
							case 69:
							case 70:
								goto IL_0543;
							case 71:
								goto IL_055e;
							case 72:
							case 73:
							case 74:
							case 75:
							case 76:
								goto IL_058a;
							case 77:
								goto IL_059c;
							case 78:
								goto IL_05b8;
							case 79:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 7:
							case 15:
							case 20:
							case 80:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_04f0:
						num2 = 65;
						if (Operators.CompareString(MyGrid.Cells[num5, 3].Text, "", TextCompare: false) != 0)
						{
							goto IL_051b;
						}
						goto IL_0530;
						IL_051b:
						num2 = 66;
						text += "#";
						goto IL_0543;
						IL_047a:
						num2 = 64;
						text = "^" + mnucmbsearch.Text + "#" + MyGrid.Cells[num5, 0].Text + "#" + MyGrid.Cells[num5, 1].Text + "#" + val.Text;
						goto IL_04f0;
						IL_0530:
						num2 = 68;
						text += "$";
						goto IL_0543;
						IL_000b:
						num2 = 2;
						num6 = MyGrid.Cols.Count - 1;
						goto IL_001c;
						IL_001c:
						num2 = 3;
						text = "";
						goto IL_0025;
						IL_0025:
						num2 = 4;
						objectValue = RuntimeHelpers.GetObjectValue(cmbType.SelectedItem);
						goto IL_0039;
						IL_0039:
						num2 = 5;
						if (MyGrid.SelectedCells.Count <= 0)
						{
							goto IL_0053;
						}
						goto IL_006e;
						IL_0053:
						num2 = 6;
						Interaction.MsgBox("Nothing Selected", MsgBoxStyle.Information, "No Selection");
						goto end_IL_0001_3;
						IL_006e:
						num2 = 9;
						DoCursor("WAIT");
						goto IL_007d;
						IL_007d:
						num2 = 10;
						firstSelectedColOrder = 0;
						goto IL_0083;
						IL_0083:
						num2 = 11;
						lastSelectedColOrder = 0;
						goto IL_0089;
						IL_0089:
						num2 = 12;
						GetFirstLastSelectedColOrder(ref MyGrid, ref firstSelectedColOrder, ref lastSelectedColOrder);
						goto IL_0098;
						IL_0098:
						num2 = 13;
						if (Operators.CompareString(Strings.Trim(mnucmbsearch.Text), "main", TextCompare: false) == 0 && lastSelectedColOrder == 0)
						{
							goto IL_00c6;
						}
						goto IL_00dd;
						IL_0543:
						num2 = 70;
						if (!DuplicateGridValue(text, Conversions.ToString(objectValue)))
						{
							goto IL_055e;
						}
						goto IL_058a;
						IL_00c6:
						num2 = 14;
						DoCursor();
						goto end_IL_0001_3;
						IL_00dd:
						num2 = 17;
						if (outGrid.Rows.Count + MyGrid.SelectedCells.Count > 1999)
						{
							goto IL_010a;
						}
						goto IL_0148;
						IL_010a:
						num2 = 18;
						Interaction.MsgBox("Maximum SQLPathFinder Selected fields cannot exceed " + Conversions.ToString(1999) + ". Try to select fewer fields using RegEx Field Expressions.", MsgBoxStyle.Information, "Exceeded");
						goto IL_0133;
						IL_0133:
						num2 = 19;
						DoCursor();
						goto end_IL_0001_3;
						IL_0148:
						num2 = 22;
						num7 = 0;
						goto IL_014e;
						IL_014e:
						num2 = 23;
						num8 = 0;
						goto IL_0154;
						IL_0154:
						num2 = 24;
						num9 = 0;
						goto IL_015a;
						IL_015a:
						num2 = 25;
						num7 = MyGrid.SelectedCells[0].RowIndex;
						goto IL_0171;
						IL_0171:
						num2 = 26;
						num8 = MyGrid.SelectedCells[MyGrid.SelectedCells.Count - 1].RowIndex;
						goto IL_0195;
						IL_0195:
						num2 = 27;
						outGrid.SuspendLayout();
						goto IL_01a4;
						IL_01a4:
						num2 = 28;
						num10 = num7;
						num11 = num8;
						num5 = num10;
						goto IL_0593;
						IL_0593:
						if (num5 <= num11)
						{
							goto IL_01b8;
						}
						goto IL_059c;
						IL_059c:
						num2 = 77;
						outGrid.FirstDisplayedScrollingRowIndex = outGrid.RowCount - 1;
						goto IL_05b8;
						IL_05b8:
						num2 = 78;
						outGrid.ResumeLayout();
						break;
						IL_01b8:
						num2 = 29;
						if (MyGrid.Rows[num5].Visible || MyGrid.SelectInvisibleCells)
						{
							goto IL_01e3;
						}
						goto IL_058a;
						IL_055e:
						num2 = 71;
						outGrid.Rows.Add(text, objectValue);
						goto IL_058a;
						IL_01e3:
						num2 = 30;
						val2 = MyGrid.Cols.FromOrder(lastSelectedColOrder);
						goto IL_01f6;
						IL_01f6:
						num2 = 31;
						if (val2.Visible)
						{
							goto IL_0209;
						}
						goto IL_058a;
						IL_0209:
						num2 = 32;
						val = MyGrid.Cells[num5, val2.Index];
						goto IL_0223;
						IL_0223:
						num2 = 33;
						selected = val.Selected;
						goto IL_022f;
						IL_022f:
						num2 = 34;
						if (selected && Operators.CompareString(val.Text, "", TextCompare: false) != 0)
						{
							goto IL_0258;
						}
						goto IL_058a;
						IL_058a:
						num2 = 76;
						num5++;
						goto IL_0593;
						IL_0258:
						num2 = 35;
						num12 = lastSelectedColOrder;
						goto IL_025f;
						IL_025f:
						num2 = 37;
						if (num12 == num6)
						{
							goto IL_026d;
						}
						goto IL_02bc;
						IL_026d:
						num2 = 38;
						if (Operators.CompareString(val.Text, ".*", TextCompare: false) == 0)
						{
							goto IL_028b;
						}
						goto IL_029b;
						IL_028b:
						num2 = 39;
						text = val.Text;
						goto IL_0543;
						IL_029b:
						num2 = 41;
						text = "^" + val.Text + "$";
						goto IL_0543;
						IL_02bc:
						num2 = 44;
						if (num12 == num6 - 1)
						{
							goto IL_02cc;
						}
						goto IL_02f9;
						IL_02cc:
						num2 = 45;
						text = "^" + MyGrid.Cells[num5, num6].Text + "$";
						goto IL_0543;
						IL_02f9:
						num2 = 47;
						if (num12 == 0)
						{
							goto IL_030a;
						}
						goto IL_03aa;
						IL_030a:
						num2 = 48;
						text = "^" + mnucmbsearch.Text + "#" + val.Text;
						goto IL_0330;
						IL_0330:
						num2 = 49;
						if ((Operators.CompareString(MyGrid.Cells[num5, 1].Text, "", TextCompare: false) != 0) | (Operators.CompareString(MyGrid.Cells[num5, 2].Text, "", TextCompare: false) != 0))
						{
							goto IL_037e;
						}
						goto IL_0393;
						IL_037e:
						num2 = 50;
						text += "#";
						goto IL_0543;
						IL_0393:
						num2 = 52;
						text += "$";
						goto IL_0543;
						IL_03aa:
						num2 = 55;
						if (num12 == 1)
						{
							goto IL_03bb;
						}
						goto IL_0469;
						IL_03bb:
						num2 = 56;
						text = "^" + mnucmbsearch.Text + "#" + MyGrid.Cells[num5, 0].Text + "#" + val.Text;
						goto IL_0412;
						IL_0412:
						num2 = 57;
						if (Operators.CompareString(MyGrid.Cells[num5, 2].Text, "", TextCompare: false) != 0)
						{
							goto IL_043d;
						}
						goto IL_0452;
						IL_043d:
						num2 = 58;
						text += "#";
						goto IL_0543;
						IL_0452:
						num2 = 60;
						text += "$";
						goto IL_0543;
						IL_0469:
						num2 = 63;
						if (num12 == 2)
						{
							goto IL_047a;
						}
						goto IL_0543;
						end_IL_0001_2:
						break;
					}
					num2 = 79;
					DoCursor();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1835;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private bool DuplicateGridValue(string MyData, string MyType)
	{
		bool result = false;
		int count = outGrid.Rows.Count;
		checked
		{
			int num = count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (Operators.ConditionalCompareObjectEqual(MyData, outGrid.Rows[i].Cells[0].Value, TextCompare: false) && Operators.ConditionalCompareObjectEqual(MyType, outGrid.Rows[i].Cells[1].Value, TextCompare: false))
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}

	private void cmdAdd_Click(object sender, EventArgs e)
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
				case 67:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				iGrid MyGrid = FieldGrid;
				AddSelectedCol(ref MyGrid);
				FieldGrid = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 67;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdRemove_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int count = default(int);
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
				case 341:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_0053;
						case 6:
							goto IL_0066;
						case 7:
							goto end_IL_0001_2;
						case 10:
							goto IL_008f;
						case 11:
							goto IL_009d;
						case 12:
							goto IL_00cf;
						case 13:
							goto IL_00f5;
						default:
							goto end_IL_0001;
						case 3:
						case 8:
						case 9:
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00fe:
					if (num5 < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_009d;
					IL_009d:
					num2 = 11;
					if (Operators.ConditionalCompareObjectNotEqual(outGrid.SelectedRows[num5].Cells[0].Value, null, TextCompare: false))
					{
						goto IL_00cf;
					}
					goto IL_00f5;
					IL_008f:
					num2 = 10;
					num6 = checked(count - 1);
					num5 = num6;
					goto IL_00fe;
					IL_00cf:
					num2 = 12;
					outGrid.Rows.Remove(outGrid.SelectedRows[num5]);
					goto IL_00f5;
					IL_000b:
					num2 = 2;
					if (outGrid.RowCount == 1 && Operators.ConditionalCompareObjectEqual(outGrid.Rows[0].Cells[0].Value, null, TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					goto IL_0053;
					IL_00f5:
					num2 = 13;
					num5 = checked(num5 + -1);
					goto IL_00fe;
					IL_0053:
					num2 = 5;
					count = outGrid.SelectedRows.Count;
					goto IL_0066;
					IL_0066:
					num2 = 6;
					if (count <= 0)
					{
						break;
					}
					goto IL_008f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				Interaction.MsgBox("To remove selections, select one or more rows and click the << button. Use the Ctrl or Shift keys to select multiple rows", MsgBoxStyle.Exclamation, "No Selection");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 341;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public object check_Form_ok()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		object result = default(object);
		int num = default(int);
		int num3 = default(int);
		bool flag = default(bool);
		bool flag2 = default(bool);
		bool flag3 = default(bool);
		bool flag4 = default(bool);
		bool flag5 = default(bool);
		int num5 = default(int);
		int num6 = default(int);
		string left = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					result = true;
					goto IL_000b;
				case 519:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0019;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_0023;
						case 7:
							goto IL_0028;
						case 8:
							goto IL_002d;
						case 9:
							goto IL_0040;
						case 10:
						case 11:
							goto IL_0048;
						case 12:
							goto IL_0067;
						case 13:
							goto IL_0093;
						case 14:
							goto IL_00ac;
						case 16:
							goto IL_00b5;
						case 17:
							goto IL_00ce;
						case 19:
							goto IL_00d7;
						case 20:
							goto IL_00f0;
						case 22:
							goto IL_00fa;
						case 15:
						case 18:
						case 21:
						case 23:
						case 24:
							goto IL_0101;
						case 25:
							goto IL_0113;
						case 26:
							goto IL_0148;
						case 28:
							goto IL_0155;
						case 29:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 27:
						case 30:
						case 31:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00f0:
					num2 = 20;
					flag = true;
					goto IL_0101;
					IL_0148:
					num2 = 26;
					result = false;
					goto end_IL_0001_3;
					IL_00ce:
					num2 = 17;
					flag2 = true;
					goto IL_0101;
					IL_0155:
					num2 = 28;
					if (flag3 && flag4 && flag)
					{
						break;
					}
					goto end_IL_0001_3;
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					flag4 = false;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					flag2 = false;
					goto IL_001e;
					IL_001e:
					num2 = 5;
					flag5 = false;
					goto IL_0023;
					IL_0023:
					num2 = 6;
					flag = false;
					goto IL_0028;
					IL_0028:
					num2 = 7;
					flag3 = true;
					goto IL_002d;
					IL_002d:
					num2 = 8;
					if (mnuOptDT.Checked)
					{
						goto IL_0040;
					}
					goto IL_0048;
					IL_0040:
					num2 = 9;
					flag3 = false;
					goto IL_0048;
					IL_0048:
					num2 = 11;
					num5 = checked(outGrid.Rows.Count - 2);
					num6 = 0;
					goto IL_010a;
					IL_010a:
					if (num6 <= num5)
					{
						goto IL_0067;
					}
					goto IL_0113;
					IL_0113:
					num2 = 25;
					if ((flag4 && flag5) || (flag4 && flag2) || (flag2 && flag5) || (flag2 && flag) || (flag5 && flag))
					{
						goto IL_0148;
					}
					goto IL_0155;
					IL_0101:
					num2 = 24;
					num6 = checked(num6 + 1);
					goto IL_010a;
					IL_00fa:
					num2 = 22;
					flag4 = true;
					goto IL_0101;
					IL_0093:
					num2 = 13;
					if (Operators.CompareString(left, "KS:Wafer", TextCompare: false) == 0)
					{
						goto IL_00ac;
					}
					goto IL_00b5;
					IL_0067:
					num2 = 12;
					left = Conversions.ToString(outGrid.Rows[num6].Cells[1].Value);
					goto IL_0093;
					IL_00b5:
					num2 = 16;
					if (Operators.CompareString(left, "KS:Lot", TextCompare: false) == 0)
					{
						goto IL_00ce;
					}
					goto IL_00d7;
					IL_00ac:
					num2 = 14;
					flag5 = true;
					goto IL_0101;
					IL_00d7:
					num2 = 19;
					if (Operators.CompareString(left, "KS:MQCS", TextCompare: false) == 0)
					{
						goto IL_00f0;
					}
					goto IL_00fa;
					end_IL_0001_2:
					break;
				}
				num2 = 29;
				result = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 519;
				continue;
			}
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

	public string Split_colon(string MyIn)
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 171:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001d;
						case 5:
							goto IL_002e;
						case 6:
							goto IL_004a;
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
					IL_001d:
					num2 = 4;
					num5 = Strings.InStrRev(MyIn, ":");
					goto IL_002e;
					IL_002e:
					num2 = 5;
					if (num5 <= 1 || Strings.Len(MyIn) < 3)
					{
						goto end_IL_0001_3;
					}
					goto IL_004a;
					IL_0013:
					num2 = 3;
					MyIn = Strings.Trim(MyIn);
					goto IL_001d;
					IL_004a:
					num2 = 6;
					text = Strings.UCase(Strings.Mid(MyIn, 1, 1));
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				text += Strings.UCase(Strings.Mid(MyIn, checked(num5 + 1), 1));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 171;
				continue;
			}
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

	private void cmdOK2_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int count = default(int);
		bool flag = default(bool);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
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
					case 1599:
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
								goto IL_001f;
							case 4:
								goto IL_0024;
							case 5:
								goto IL_002d;
							case 6:
								goto IL_0036;
							case 7:
								goto IL_003f;
							case 8:
								goto IL_0048;
							case 9:
								goto IL_0051;
							case 10:
								goto IL_0068;
							case 11:
								goto IL_0072;
							case 12:
								goto IL_0083;
							case 13:
								goto IL_009c;
							case 16:
								goto IL_00bd;
							case 17:
								goto IL_00ee;
							case 18:
								goto IL_0107;
							case 20:
							case 21:
								goto IL_0123;
							case 22:
								goto IL_01a8;
							case 15:
							case 23:
							case 24:
							case 25:
								goto IL_01c1;
							case 26:
								goto IL_01c7;
							case 27:
								goto IL_01e5;
							case 28:
								goto IL_0210;
							case 29:
								goto IL_023b;
							case 30:
								goto IL_0241;
							case 32:
							case 33:
								goto IL_028d;
							case 31:
							case 34:
								goto IL_029c;
							case 36:
								goto IL_02af;
							case 37:
								goto IL_02cf;
							case 38:
								goto IL_02e4;
							case 39:
								goto IL_0314;
							case 42:
								goto IL_0335;
							case 43:
								goto IL_0353;
							case 44:
								goto IL_0388;
							case 45:
								goto IL_03d0;
							case 46:
								goto IL_03da;
							case 47:
								goto IL_03e9;
							case 50:
								goto IL_042d;
							case 51:
								goto IL_044b;
							case 52:
								goto IL_04bc;
							case 53:
								goto IL_04c6;
							case 54:
								goto IL_04d5;
							case 57:
								goto IL_0514;
							case 41:
							case 48:
							case 49:
							case 55:
							case 56:
							case 59:
							case 60:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 14:
							case 19:
							case 35:
							case 40:
							case 58:
							case 61:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0314:
						num2 = 39;
						Interaction.MsgBox("You must assign a text file to hold the fields. Click menu option Options -> Output File to set a file name", MsgBoxStyle.Exclamation, "No Output File");
						goto end_IL_0001_3;
						IL_042d:
						num2 = 50;
						num5 = outGrid.Rows.Count - 2;
						num6 = 0;
						goto IL_04cd;
						IL_03da:
						num2 = 46;
						num6++;
						goto IL_03e1;
						IL_04cd:
						if (num6 <= num5)
						{
							goto IL_044b;
						}
						goto IL_04d5;
						IL_000b:
						num2 = 2;
						count = outGrid.Rows.Count;
						goto IL_001f;
						IL_001f:
						num2 = 3;
						flag = true;
						goto IL_0024;
						IL_0024:
						num2 = 4;
						text = "";
						goto IL_002d;
						IL_002d:
						num2 = 5;
						text2 = "";
						goto IL_0036;
						IL_0036:
						num2 = 6;
						text3 = "";
						goto IL_003f;
						IL_003f:
						num2 = 7;
						text4 = "I";
						goto IL_0048;
						IL_0048:
						num2 = 8;
						text5 = "KU";
						goto IL_0051;
						IL_0051:
						num2 = 9;
						if (!mnuOptDT.Checked)
						{
							goto IL_0068;
						}
						goto IL_0072;
						IL_0068:
						num2 = 10;
						text4 = "M";
						goto IL_0072;
						IL_0072:
						num2 = 11;
						if (count > 1)
						{
							goto IL_0083;
						}
						goto IL_0514;
						IL_0083:
						num2 = 12;
						if (Conversions.ToBoolean(Operators.NotObject(check_Form_ok())))
						{
							goto IL_009c;
						}
						goto IL_00bd;
						IL_009c:
						num2 = 13;
						Interaction.MsgBox("You are not allowed to mix KS:Unit/Die/Patch, KS:Lot, KS:Wafer nor KS:MQCS Type fields in the Output Grid.", MsgBoxStyle.Exclamation, "Field Mixing");
						goto end_IL_0001_3;
						IL_00bd:
						num2 = 16;
						text5 = Split_colon(Conversions.ToString(outGrid.Rows[0].Cells[1].Value));
						goto IL_00ee;
						IL_00ee:
						num2 = 17;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							goto IL_0107;
						}
						goto IL_0123;
						IL_0107:
						num2 = 18;
						Interaction.MsgBox("You should specify a datatype (e.g., KS:Unit) in every cell of the Output Grid.", MsgBoxStyle.Exclamation, "Missing Type");
						goto end_IL_0001_3;
						IL_0123:
						num2 = 21;
						switch (text5)
						{
						case "KU":
						case "KD":
						case "KP":
							break;
						default:
							goto IL_01c1;
						}
						if (Operators.CompareString(cmbType.Text, "KS:Wafer", TextCompare: false) != 0 && Operators.CompareString(cmbType.Text, "KS:Lot", TextCompare: false) != 0 && Operators.CompareString(cmbType.Text, "KS:MQCS", TextCompare: false) != 0)
						{
							goto IL_01a8;
						}
						goto IL_01c1;
						IL_04d5:
						num2 = 54;
						f_out = "Column-iBDP:" + text5 + "<SOF>" + f_out + "<EOF>";
						break;
						IL_044b:
						num2 = 51;
						f_out = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(f_out + text, outGrid.Rows[num6].Cells[0].Value), ";"), outGrid.Rows[num6].Cells[1].Value));
						goto IL_04bc;
						IL_04bc:
						num2 = 52;
						text = "<;>";
						goto IL_04c6;
						IL_01a8:
						num2 = 22;
						text5 = Split_colon(cmbType.Text);
						goto IL_01c1;
						IL_01c1:
						num2 = 25;
						flag = true;
						goto IL_01c7;
						IL_01c7:
						num2 = 26;
						num7 = outGrid.Rows.Count - 2;
						num6 = 0;
						goto IL_0294;
						IL_0294:
						if (num6 <= num7)
						{
							goto IL_01e5;
						}
						goto IL_029c;
						IL_01e5:
						num2 = 27;
						text3 = Conversions.ToString(outGrid.Rows[num6].Cells[0].Value);
						goto IL_0210;
						IL_0210:
						num2 = 28;
						if (Strings.InStr(text3, "'") != 0 || Strings.InStr(text3, "\"") != 0)
						{
							goto IL_023b;
						}
						goto IL_028d;
						IL_04c6:
						num2 = 53;
						num6++;
						goto IL_04cd;
						IL_028d:
						num2 = 33;
						num6++;
						goto IL_0294;
						IL_023b:
						num2 = 29;
						flag = false;
						goto IL_0241;
						IL_0241:
						num2 = 30;
						Interaction.MsgBox("Regex values cannot contain single or double quotes. See row:" + Conversion.Str(num6 + 1) + " (" + text3 + ")", MsgBoxStyle.Exclamation, "Invalid Regex");
						goto IL_029c;
						IL_029c:
						num2 = 34;
						if (!flag)
						{
							goto end_IL_0001_3;
						}
						goto IL_02af;
						IL_02af:
						num2 = 36;
						if (Operators.CompareString(f_InMode, "UTIL", TextCompare: false) == 0)
						{
							goto IL_02cf;
						}
						goto IL_042d;
						IL_02cf:
						num2 = 37;
						text2 = General_Procedures.Get_Node_Value(mnuOutWF.Text);
						goto IL_02e4;
						IL_02e4:
						num2 = 38;
						if (Operators.CompareString(text2, "", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text2), "NO", TextCompare: false) == 0)
						{
							goto IL_0314;
						}
						goto IL_0335;
						IL_0514:
						num2 = 57;
						Interaction.MsgBox("You must select a field or CANCEL out of the form", MsgBoxStyle.Exclamation, "Nothing selected");
						goto end_IL_0001_3;
						IL_0335:
						num2 = 42;
						num8 = outGrid.Rows.Count - 2;
						num6 = 0;
						goto IL_03e1;
						IL_03e1:
						if (num6 <= num8)
						{
							goto IL_0353;
						}
						goto IL_03e9;
						IL_03e9:
						num2 = 47;
						f_out = text4 + text5 + text2 + "<SOF>pattern,type\r\n" + f_out + "<EOF>";
						break;
						IL_0353:
						num2 = 43;
						text3 = Conversions.ToString(Operators.ConcatenateObject(",", outGrid.Rows[num6].Cells[1].Value));
						goto IL_0388;
						IL_0388:
						num2 = 44;
						f_out = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(f_out + text, outGrid.Rows[num6].Cells[0].Value), text3));
						goto IL_03d0;
						IL_03d0:
						num2 = 45;
						text = "\r\n";
						goto IL_03da;
						end_IL_0001_2:
						break;
					}
					num2 = 60;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1599;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdCancel2_Click(object sender, EventArgs e)
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

	private void mnuClearAll_Click(object sender, EventArgs e)
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
				case 158:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 7:
						case 8:
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					if (outGrid.Rows.Count <= 0)
					{
						break;
					}
					goto IL_002a;
					IL_002a:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear all Selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear?");
					goto IL_003e;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_003e:
					num2 = 5;
					if (num5 != 7)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				outGrid.Rows.Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 158;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmCSVViewer_Resize(object sender, EventArgs e)
	{
		Form_Resize();
	}

	private void GridCSV_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Expected O, but got Unknown
		iGrid MyGrid = (iGrid)sender;
		AddSelectedCol(ref MyGrid);
		sender = MyGrid;
	}

	private void GridCSV_KeyPress(object sender, KeyPressEventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		short num = checked((short)Strings.Asc(e.KeyChar));
		if (num == 13)
		{
			iGrid MyGrid = (iGrid)sender;
			AddSelectedCol(ref MyGrid);
			sender = MyGrid;
		}
	}

	private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!gdoFieldSearch)
		{
			return;
		}
		string left = mnucmbDataType.Text;
		if (Operators.CompareString(left, gOldDataType, TextCompare: false) != 0)
		{
			if (Operators.CompareString(left, "KS:Unit/Die/Patch", TextCompare: false) == 0)
			{
				f_InType = "KS:Unit";
			}
			else
			{
				f_InType = left;
			}
			gOldDataType = left;
			Init_Other_Controls();
			ReadSQLite(gOldValue);
			gdoFieldSearch = true;
			Form_Resize();
		}
	}

	private void mnuOutWF_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
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
				case 251:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0045;
						case 5:
							goto IL_004d;
						case 6:
							goto IL_005c;
						case 7:
							goto IL_0073;
						case 9:
							goto IL_0080;
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
					IL_005c:
					num2 = 6;
					if (Operators.CompareString(oVal, "", TextCompare: false) == 0)
					{
						goto IL_0073;
					}
					goto IL_0080;
					IL_0073:
					num2 = 7;
					text = " {MyInput.txt}";
					break;
					IL_004d:
					num2 = 5;
					BuildForm.SetOutputOpt("WF", ref oVal);
					goto IL_005c;
					IL_0080:
					num2 = 9;
					text = " {" + Strings.Trim(oVal) + "}";
					break;
					IL_000b:
					num2 = 2;
					oVal = General_Procedures.Get_Node_Value(mnuOutWF.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if ((Operators.CompareString(oVal, "No", TextCompare: false) == 0) | (Operators.CompareString(oVal, "", TextCompare: false) == 0))
					{
						goto IL_0045;
					}
					goto IL_004d;
					IL_0045:
					num2 = 4;
					oVal = "MyInput.txt";
					goto IL_004d;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				mnuOutWF.Text = "Output File" + text;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 251;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private object Return_ColType(string c2)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object result = default(object);
		while (true)
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
				case 467:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0041;
						case 6:
							goto IL_004f;
						case 7:
							goto IL_0081;
						case 9:
							goto IL_008f;
						case 10:
							goto IL_010b;
						case 12:
							goto IL_0117;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 11:
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_010b:
					num2 = 10;
					result = "";
					goto end_IL_0001_3;
					IL_0081:
					num2 = 7;
					result = "KS:Wafer";
					goto end_IL_0001_3;
					IL_004f:
					num2 = 6;
					if (Operators.CompareString(f_InMode, "FORM", TextCompare: false) == 0 && Operators.CompareString(f_InType2, "KW", TextCompare: false) == 0)
					{
						goto IL_0081;
					}
					goto IL_008f;
					IL_008f:
					num2 = 9;
					if (Operators.CompareString(f_InMode, "FORM", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(f_InType2 + " ", 1, 1), "K", TextCompare: false) == 0)
					{
						switch (c2)
						{
						case "KS:Unit":
						case "KS:Patch":
						case "KS:Die":
							goto IL_0117;
						}
						if (Operators.CompareString(c2, "KS:MQCS", TextCompare: false) != 0)
						{
							goto IL_010b;
						}
					}
					goto IL_0117;
					IL_000b:
					num2 = 2;
					result = c2;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(f_InMode, "FORM", TextCompare: false) == 0 && Operators.CompareString(f_InType2, "KL", TextCompare: false) == 0)
					{
						goto IL_0041;
					}
					goto IL_004f;
					IL_0117:
					num2 = 12;
					switch (c2)
					{
					default:
						if (Operators.CompareString(c2, "KS:MQCS", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "KS:Unit":
					case "KS:Patch":
					case "KS:Die":
					case "KS:Lot":
					case "KS:Wafer":
						goto end_IL_0001_3;
					}
					break;
					IL_0041:
					num2 = 4;
					result = "KS:Lot";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				result = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 467;
				continue;
			}
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

	private void mnuPaste2_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num5 = default(int);
		string expression = default(string);
		string[] array = default(string[]);
		string text3 = default(string);
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
					case 475:
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
								goto IL_0018;
							case 5:
								goto IL_001d;
							case 6:
								goto IL_0026;
							case 7:
								goto IL_0038;
							case 8:
								goto IL_0049;
							case 9:
								goto IL_005d;
							case 10:
								goto IL_006c;
							case 11:
								goto IL_0088;
							case 12:
								goto IL_009a;
							case 13:
								goto IL_00a8;
							case 14:
								goto IL_00be;
							case 15:
								goto IL_00dd;
							case 17:
								goto IL_00f3;
							case 18:
								goto IL_00fa;
							case 16:
							case 19:
							case 20:
								goto IL_0105;
							case 21:
								goto IL_011e;
							case 22:
							case 23:
							case 24:
								goto IL_0146;
							case 25:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 26:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0105:
						num2 = 20;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_011e;
						}
						goto IL_0146;
						IL_011e:
						num2 = 21;
						outGrid.Rows.Add(text, text2);
						goto IL_0146;
						IL_00fa:
						num2 = 18;
						text2 = "";
						goto IL_0105;
						IL_0146:
						num2 = 24;
						num5++;
						goto IL_014f;
						IL_000b:
						num2 = 2;
						expression = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num5 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						array = null;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						text3 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						expression = MyProject.Computer.Clipboard.GetText();
						goto IL_0038;
						IL_0038:
						num2 = 7;
						array = Strings.Split(expression, "\r\n");
						goto IL_0049;
						IL_0049:
						num2 = 8;
						num6 = Information.UBound(array);
						num5 = 0;
						goto IL_014f;
						IL_014f:
						if (num5 > num6)
						{
							break;
						}
						goto IL_005d;
						IL_005d:
						num2 = 9;
						text3 = Strings.Trim(array[num5]);
						goto IL_006c;
						IL_006c:
						num2 = 10;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_0088;
						}
						goto IL_0146;
						IL_0088:
						num2 = 11;
						num7 = Strings.InStr(text3, ",");
						goto IL_009a;
						IL_009a:
						num2 = 12;
						if (num7 != 0)
						{
							goto IL_00a8;
						}
						goto IL_00f3;
						IL_00a8:
						num2 = 13;
						text = Strings.Trim(Strings.Mid(text3, 1, num7 - 1));
						goto IL_00be;
						IL_00be:
						num2 = 14;
						text2 = Strings.Trim(Strings.Mid(text3 + " ", num7 + 1));
						goto IL_00dd;
						IL_00dd:
						num2 = 15;
						text2 = Conversions.ToString(Return_ColType(text2));
						goto IL_0105;
						IL_00f3:
						num2 = 17;
						text = text3;
						goto IL_00fa;
						end_IL_0001_2:
						break;
					}
					num2 = 25;
					array = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 475;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuHLPaste_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num7 = default(int);
		string text = default(string);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 379:
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
								goto IL_0029;
							case 7:
								goto IL_0056;
							case 8:
								goto IL_0077;
							case 9:
								goto IL_0098;
							case 10:
								goto IL_00a6;
							case 11:
								goto IL_00b4;
							case 12:
								goto IL_00d7;
							case 13:
								goto IL_0114;
							default:
								goto end_IL_0001;
							case 14:
							case 15:
							case 16:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_011d:
						if (num5 < 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_00b4;
						IL_00b4:
						num2 = 11;
						if (outGrid.SelectedCells[num5].ColumnIndex == 1)
						{
							goto IL_00d7;
						}
						goto IL_0114;
						IL_00a6:
						num2 = 10;
						num6 = num7 - 1;
						num5 = num6;
						goto IL_011d;
						IL_00d7:
						num2 = 12;
						outGrid.Rows[outGrid.SelectedCells[num5].RowIndex].Cells[1].Value = text;
						goto IL_0114;
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
						num8 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						num7 = outGrid.GetCellCount(DataGridViewElementStates.Selected);
						goto IL_0029;
						IL_0029:
						num2 = 6;
						if (num7 < 1 || outGrid.SelectedCells[num7 - 1].ColumnIndex != 1)
						{
							goto end_IL_0001_2;
						}
						goto IL_0056;
						IL_0114:
						num2 = 13;
						num5 += -1;
						goto IL_011d;
						IL_0056:
						num2 = 7;
						text = Conversions.ToString(outGrid.SelectedCells[num7 - 1].Value);
						goto IL_0077;
						IL_0077:
						num2 = 8;
						num8 = unchecked((int)Interaction.MsgBox("Change highlighted rows in column \"Type\" to: \"" + text + "\"?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Change?"));
						goto IL_0098;
						IL_0098:
						num2 = 9;
						if (num8 != 6)
						{
							goto end_IL_0001_2;
						}
						goto IL_00a6;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 379;
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

	private void mnuOutSelectAll_Click(object sender, EventArgs e)
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
				outGrid.SelectAll();
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

	private void mnuOutCopy_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		DataGridViewRow dataGridViewRow = default(DataGridViewRow);
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
				case 503:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003a;
						case 5:
							goto IL_005a;
						case 6:
							goto IL_0075;
						case 7:
							goto IL_008c;
						case 9:
							goto IL_00ba;
						case 10:
							goto IL_00d2;
						case 8:
						case 11:
						case 12:
						case 13:
							goto IL_011c;
						case 14:
							goto IL_012f;
						case 15:
							goto IL_0148;
						case 16:
							goto IL_0160;
						case 17:
							goto IL_0173;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0075:
					num2 = 6;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_008c;
					}
					goto IL_00ba;
					IL_008c:
					num2 = 7;
					text = Conversions.ToString(Operators.ConcatenateObject(text2 + ",", dataGridViewRow.Cells[1].Value));
					goto IL_011c;
					IL_005a:
					num2 = 5;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_0075;
					}
					goto IL_011c;
					IL_00ba:
					num2 = 9;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_00d2;
					}
					goto IL_011c;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					enumerator = outGrid.SelectedRows.GetEnumerator();
					goto IL_011f;
					IL_011f:
					if (enumerator.MoveNext())
					{
						dataGridViewRow = (DataGridViewRow)enumerator.Current;
						goto IL_003a;
					}
					goto IL_012f;
					IL_012f:
					num2 = 14;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_0148;
					IL_011c:
					num2 = 13;
					goto IL_011f;
					IL_0148:
					num2 = 15;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0160;
					IL_0160:
					num2 = 16;
					MyProject.Computer.Clipboard.Clear();
					goto IL_0173;
					IL_0173:
					num2 = 17;
					MyProject.Computer.Clipboard.SetText(text);
					break;
					IL_00d2:
					num2 = 10;
					text = text + "\r\n" + text2 + "," + Strings.Trim(Conversions.ToString(dataGridViewRow.Cells[1].Value));
					goto IL_011c;
					IL_003a:
					num2 = 4;
					text2 = Strings.Trim(Conversions.ToString(dataGridViewRow.Cells[0].Value));
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				text = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 503;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdexcl_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string left = default(string);
		string left2 = default(string);
		int count = default(int);
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
				case 710:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0035;
						case 5:
						case 6:
							goto IL_007d;
						case 7:
							goto IL_0090;
						case 8:
							goto end_IL_0001_2;
						case 11:
							goto IL_00bf;
						case 12:
							goto IL_00cf;
						case 13:
							goto IL_0104;
						case 14:
							goto IL_0141;
						case 15:
							goto IL_016c;
						case 17:
							goto IL_01c1;
						case 18:
							goto IL_01ec;
						case 16:
						case 19:
						case 20:
						case 21:
							goto IL_0242;
						default:
							goto end_IL_0001;
						case 4:
						case 9:
						case 10:
						case 22:
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ec:
					num2 = 18;
					outGrid.SelectedRows[num5].Cells[0].Value = Strings.Mid(Conversions.ToString(outGrid.SelectedRows[num5].Cells[0].Value), 2);
					goto IL_0242;
					IL_016c:
					num2 = 15;
					outGrid.SelectedRows[num5].Cells[0].Value = Operators.ConcatenateObject("!", outGrid.SelectedRows[num5].Cells[0].Value);
					goto IL_0242;
					IL_0141:
					num2 = 14;
					if (Operators.CompareString(left, "E", TextCompare: false) == 0 && Operators.CompareString(left2, "!", TextCompare: false) != 0)
					{
						goto IL_016c;
					}
					goto IL_01c1;
					IL_01c1:
					num2 = 17;
					if (Operators.CompareString(left, "I", TextCompare: false) == 0 && Operators.CompareString(left2, "!", TextCompare: false) == 0)
					{
						goto IL_01ec;
					}
					goto IL_0242;
					IL_000b:
					num2 = 2;
					left = Strings.UCase(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null)), 1, 1));
					goto IL_0035;
					IL_0035:
					num2 = 3;
					if (outGrid.RowCount == 1 && Operators.ConditionalCompareObjectEqual(outGrid.Rows[0].Cells[0].Value, null, TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					goto IL_007d;
					IL_0242:
					num2 = 21;
					num5 = checked(num5 + 1);
					goto IL_024b;
					IL_007d:
					num2 = 6;
					count = outGrid.SelectedRows.Count;
					goto IL_0090;
					IL_0090:
					num2 = 7;
					if (count <= 0)
					{
						break;
					}
					goto IL_00bf;
					IL_00bf:
					num2 = 11;
					num6 = checked(count - 1);
					num5 = 0;
					goto IL_024b;
					IL_024b:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_00cf;
					IL_00cf:
					num2 = 12;
					if (Operators.ConditionalCompareObjectNotEqual(outGrid.SelectedRows[num5].Cells[0].Value, null, TextCompare: false))
					{
						goto IL_0104;
					}
					goto IL_0242;
					IL_0104:
					num2 = 13;
					left2 = Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(outGrid.SelectedRows[num5].Cells[0].Value, " ")), 1, 1);
					goto IL_0141;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Interaction.MsgBox("To change selections to either Include or Exclude, first select one or more rows and then click the appropriate button. Use the Ctrl or Shift keys to select multiple rows", MsgBoxStyle.Exclamation, "No Selection");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 710;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdUp_Click(object sender, EventArgs e)
	{
		if (outGrid.RowCount > 1)
		{
			int rowIndex = outGrid.CurrentCell.RowIndex;
			int rowCount = outGrid.RowCount;
			if (rowIndex != checked(rowCount - 1) && rowIndex != 0)
			{
				DataGridView MyGrid = outGrid;
				GridModule.Grid_Up(ref MyGrid);
				outGrid = MyGrid;
			}
		}
	}

	private void cmdDown_Click(object sender, EventArgs e)
	{
		if (outGrid.RowCount > 1)
		{
			int rowIndex = outGrid.CurrentCell.RowIndex;
			int rowCount = outGrid.RowCount;
			if (rowIndex < checked(rowCount - 2))
			{
				DataGridView MyGrid = outGrid;
				GridModule.Grid_Down(ref MyGrid);
				outGrid = MyGrid;
			}
		}
	}

	private void FrmMegaField_FormClosing(object sender, FormClosingEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		bool cancel = default(bool);
		while (true)
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
				case 197:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002f;
						case 5:
							goto IL_0034;
						case 6:
							goto IL_0049;
						case 7:
							goto IL_0056;
						case 9:
							goto IL_005f;
						case 8:
						case 10:
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
					IL_0049:
					num2 = 6;
					if (num5 == 7)
					{
						goto IL_0056;
					}
					goto IL_005f;
					IL_0056:
					num2 = 7;
					cancel = true;
					break;
					IL_0034:
					num2 = 5;
					num5 = (int)Interaction.MsgBox("Are you sure you want to cancel selections?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Close?");
					goto IL_0049;
					IL_005f:
					num2 = 9;
					f_out = "CANCEL";
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(f_out, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0025;
					IL_0025:
					num2 = 3;
					cancel = e.Cancel;
					goto IL_002f;
					IL_002f:
					num2 = 4;
					num5 = 0;
					goto IL_0034;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				e.Cancel = cancel;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 197;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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

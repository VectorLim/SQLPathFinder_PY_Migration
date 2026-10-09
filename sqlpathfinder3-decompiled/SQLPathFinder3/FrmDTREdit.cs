using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmDTREdit : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuNew")]
	private ToolStripMenuItem _mnuNew;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOpen")]
	private ToolStripMenuItem _mnuOpen;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSave")]
	private ToolStripMenuItem _mnuSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveAs")]
	private ToolStripMenuItem _mnuSaveAs;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRunQueryNormally")]
	private ToolStripMenuItem _mnuRunQueryNormally;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRunExplainPlan")]
	private ToolStripMenuItem _mnuRunExplainPlan;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRunDescribe")]
	private ToolStripMenuItem _mnuRunDescribe;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRunGetError")]
	private ToolStripMenuItem _mnuRunGetError;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClose")]
	private ToolStripMenuItem _mnuClose;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCut")]
	private ToolStripMenuItem _mnuCut;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCopy")]
	private ToolStripMenuItem _mnuCopy;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPaste")]
	private ToolStripMenuItem _mnuPaste;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSelectAll")]
	private ToolStripMenuItem _mnuSelectAll;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFind")]
	private ToolStripMenuItem _mnuFind;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFindNext")]
	private ToolStripMenuItem _mnuFindNext;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEdit")]
	private ToolStripMenuItem _mnuEdit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutVAX")]
	private ToolStripMenuItem _mnuOutVAX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutExcel2")]
	private ToolStripMenuItem _mnuOutExcel2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRow2")]
	private ToolStripMenuItem _mnuRow2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMacroFile")]
	private ToolStripMenuItem _mnuMacroFile;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCopyOpt")]
	private ToolStripMenuItem _mnuCopyOpt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuExcel_I")]
	private ToolStripMenuItem _mnuExcel_I;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuExcel_C")]
	private ToolStripMenuItem _mnuExcel_C;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGRID_I")]
	private ToolStripMenuItem _mnuGRID_I;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelp")]
	private ToolStripMenuItem _mnuHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button1")]
	private ToolStripButton __Tbr1_Button1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button2")]
	private ToolStripButton __Tbr1_Button2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button3")]
	private ToolStripButton __Tbr1_Button3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button4")]
	private ToolStripButton __Tbr1_Button4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button10")]
	private ToolStripButton __Tbr1_Button10;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Text1")]
	private TextBox _Text1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbSQLVA")]
	private ToolStripComboBox _cmbSQLVA;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_Tbr1_Button9")]
	private ToolStripButton __Tbr1_Button9;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuUndoRedo")]
	private ToolStripMenuItem _mnuUndoRedo;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdNodeO")]
	private ToolStripButton _CmdNodeO;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOut")]
	private ToolStripMenuItem _mnuOut;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutWF")]
	private ToolStripMenuItem _mnuOutWF;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuJMP_I")]
	private ToolStripMenuItem _mnuJMP_I;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuJMP_C")]
	private ToolStripMenuItem _mnuJMP_C;

	public bool fSaveSQL;

	public bool KeepSQL;

	public string KeepSQLData;

	public string APreScript;

	public string out_odbc;

	private string fTitle;

	private bool fSaveAs;

	private int out_row;

	private string out_inline;

	private string MacroFile;

	private bool ll_First;

	private string ll_FindStr;

	private short ll_FindCase;

	private bool FirstRunQ;

	public virtual ToolStripMenuItem mnuNew
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
			ToolStripMenuItem toolStripMenuItem = _mnuNew;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuNew = value;
			toolStripMenuItem = _mnuNew;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuOpen
	{
		[CompilerGenerated]
		get
		{
			return _mnuOpen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOpen_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOpen;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOpen = value;
			toolStripMenuItem = _mnuOpen;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuSave
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
			ToolStripMenuItem toolStripMenuItem = _mnuSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSave = value;
			toolStripMenuItem = _mnuSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuSaveAs
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveAs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveAs_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveAs = value;
			toolStripMenuItem = _mnuSaveAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuEdtDash1")]
	public virtual ToolStripSeparator mnuEdtDash1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuRunQueryNormally
	{
		[CompilerGenerated]
		get
		{
			return _mnuRunQueryNormally;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRunQueryNormally_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRunQueryNormally;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRunQueryNormally = value;
			toolStripMenuItem = _mnuRunQueryNormally;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuRunExplainPlan
	{
		[CompilerGenerated]
		get
		{
			return _mnuRunExplainPlan;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRunExplainPlan_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRunExplainPlan;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRunExplainPlan = value;
			toolStripMenuItem = _mnuRunExplainPlan;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuRunDescribe
	{
		[CompilerGenerated]
		get
		{
			return _mnuRunDescribe;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRunDescribe_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRunDescribe;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRunDescribe = value;
			toolStripMenuItem = _mnuRunDescribe;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuRunGetError
	{
		[CompilerGenerated]
		get
		{
			return _mnuRunGetError;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRunGetError_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRunGetError;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRunGetError = value;
			toolStripMenuItem = _mnuRunGetError;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuEdtDash2")]
	public virtual ToolStripSeparator mnuEdtDash2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuClose
	{
		[CompilerGenerated]
		get
		{
			return _mnuClose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuClose_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClose;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClose = value;
			toolStripMenuItem = _mnuClose;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuFile")]
	public virtual ToolStripMenuItem mnuFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuCut
	{
		[CompilerGenerated]
		get
		{
			return _mnuCut;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnucut_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCut;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCut = value;
			toolStripMenuItem = _mnuCut;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuCopy
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
			EventHandler value2 = mnucopy_Click;
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

	public virtual ToolStripMenuItem mnuPaste
	{
		[CompilerGenerated]
		get
		{
			return _mnuPaste;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPaste_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPaste;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPaste = value;
			toolStripMenuItem = _mnuPaste;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuSelectAll
	{
		[CompilerGenerated]
		get
		{
			return _mnuSelectAll;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuselectall_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSelectAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSelectAll = value;
			toolStripMenuItem = _mnuSelectAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuEdtDash0")]
	public virtual ToolStripSeparator mnuEdtDash0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuFind
	{
		[CompilerGenerated]
		get
		{
			return _mnuFind;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuFind_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFind;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFind = value;
			toolStripMenuItem = _mnuFind;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuFindNext
	{
		[CompilerGenerated]
		get
		{
			return _mnuFindNext;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnufindnext_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFindNext;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFindNext = value;
			toolStripMenuItem = _mnuFindNext;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuEdit
	{
		[CompilerGenerated]
		get
		{
			return _mnuEdit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuedit_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuEdit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuEdit = value;
			toolStripMenuItem = _mnuEdit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuOutVAX
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutVAX;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutVax_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutVAX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutVAX = value;
			toolStripMenuItem = _mnuOutVAX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuOutExcel2
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutExcel2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutExcel2_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutExcel2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutExcel2 = value;
			toolStripMenuItem = _mnuOutExcel2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuRow2
	{
		[CompilerGenerated]
		get
		{
			return _mnuRow2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRow2_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRow2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRow2 = value;
			toolStripMenuItem = _mnuRow2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMacroFile
	{
		[CompilerGenerated]
		get
		{
			return _mnuMacroFile;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMacroFile_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMacroFile;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMacroFile = value;
			toolStripMenuItem = _mnuMacroFile;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuCopyOpt
	{
		[CompilerGenerated]
		get
		{
			return _mnuCopyOpt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCopyOpt_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCopyOpt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCopyOpt = value;
			toolStripMenuItem = _mnuCopyOpt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuQuery")]
	public virtual ToolStripMenuItem mnuQuery
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuExcel_I
	{
		[CompilerGenerated]
		get
		{
			return _mnuExcel_I;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_I_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuExcel_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuExcel_I = value;
			toolStripMenuItem = _mnuExcel_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuExcel_C
	{
		[CompilerGenerated]
		get
		{
			return _mnuExcel_C;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_C_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuExcel_C;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuExcel_C = value;
			toolStripMenuItem = _mnuExcel_C;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuD1")]
	public virtual ToolStripSeparator mnuD1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuGRID_I
	{
		[CompilerGenerated]
		get
		{
			return _mnuGRID_I;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_I_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuGRID_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuGRID_I = value;
			toolStripMenuItem = _mnuGRID_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuUtilities")]
	public virtual ToolStripMenuItem mnuUtilities
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuHelp
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

	[field: AccessedThroughProperty("MainMenu1")]
	public virtual MenuStrip MainMenu1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripButton _Tbr1_Button1
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button1 = value;
			toolStripButton = __Tbr1_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _Tbr1_Button2
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button2 = value;
			toolStripButton = __Tbr1_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _Tbr1_Button3
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button3;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button3 = value;
			toolStripButton = __Tbr1_Button3;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _Tbr1_Button4
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button4 = value;
			toolStripButton = __Tbr1_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _Tbr1_Button10
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button10;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button10;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button10 = value;
			toolStripButton = __Tbr1_Button10;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Tbr1")]
	public virtual ToolStrip Tbr1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual TextBox Text1
	{
		[CompilerGenerated]
		get
		{
			return _Text1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Text1_TextChanged;
			TextBox textBox = _Text1;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			_Text1 = value;
			textBox = _Text1;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	internal virtual ToolStripComboBox cmbSQLVA
	{
		[CompilerGenerated]
		get
		{
			return _cmbSQLVA;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbSQLVA_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = _cmbSQLVA;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			_cmbSQLVA = value;
			toolStripComboBox = _cmbSQLVA;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblNodeO")]
	internal virtual ToolStripLabel lblNodeO
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblUsr")]
	internal virtual ToolStripLabel lblUsr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtUsr")]
	internal virtual ToolStripComboBox TxtUsr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblPW")]
	internal virtual ToolStripLabel lblPW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripButton _Tbr1_Button9
	{
		[CompilerGenerated]
		get
		{
			return __Tbr1_Button9;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Tbr1_ButtonClick;
			ToolStripButton toolStripButton = __Tbr1_Button9;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__Tbr1_Button9 = value;
			toolStripButton = __Tbr1_Button9;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblspace1")]
	internal virtual ToolStripLabel lblspace1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblSpace0")]
	internal virtual ToolStripLabel lblSpace0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblspace2")]
	internal virtual ToolStripLabel lblspace2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuUndoRedo
	{
		[CompilerGenerated]
		get
		{
			return _mnuUndoRedo;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuUndoRedo_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuUndoRedo;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuUndoRedo = value;
			toolStripMenuItem = _mnuUndoRedo;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtPW")]
	internal virtual TextBox TxtPW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton CmdNodeO
	{
		[CompilerGenerated]
		get
		{
			return _CmdNodeO;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdNodeO_Click;
			ToolStripButton toolStripButton = _CmdNodeO;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_CmdNodeO = value;
			toolStripButton = _CmdNodeO;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CmbNodeO")]
	internal virtual ToolStripComboBox CmbNodeO
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuOut
	{
		[CompilerGenerated]
		get
		{
			return _mnuOut;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOut_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOut;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOut = value;
			toolStripMenuItem = _mnuOut;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
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

	[field: AccessedThroughProperty("ToolStripLabel1")]
	internal virtual ToolStripLabel ToolStripLabel1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuJMP_I
	{
		[CompilerGenerated]
		get
		{
			return _mnuJMP_I;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_I_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuJMP_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuJMP_I = value;
			toolStripMenuItem = _mnuJMP_I;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuJMP_C
	{
		[CompilerGenerated]
		get
		{
			return _mnuJMP_C;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_C_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuJMP_C;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuJMP_C = value;
			toolStripMenuItem = _mnuJMP_C;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[DebuggerNonUserCode]
	public FrmDTREdit()
	{
		base.Load += FrmDTREdit_Load;
		base.FormClosing += FrmDTREdit_FormClosing;
		fSaveSQL = false;
		KeepSQL = false;
		KeepSQLData = "";
		APreScript = "";
		out_odbc = "";
		fTitle = "";
		fSaveAs = true;
		out_row = -1;
		out_inline = "";
		MacroFile = "";
		ll_First = true;
		ll_FindStr = "";
		ll_FindCase = 0;
		FirstRunQ = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmDTREdit));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.Text1 = new System.Windows.Forms.TextBox();
		this.MainMenu1 = new System.Windows.Forms.MenuStrip();
		this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEdtDash1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRunQueryNormally = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRunExplainPlan = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRunDescribe = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRunGetError = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEdtDash2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUndoRedo = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEdtDash0 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuFindNext = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutVAX = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutExcel2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuRow2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMacroFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCopyOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUtilities = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuExcel_I = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuExcel_C = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuD1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuGRID_I = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuJMP_I = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuJMP_C = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOut = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOutWF = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.Tbr1 = new System.Windows.Forms.ToolStrip();
		this._Tbr1_Button1 = new System.Windows.Forms.ToolStripButton();
		this._Tbr1_Button2 = new System.Windows.Forms.ToolStripButton();
		this._Tbr1_Button3 = new System.Windows.Forms.ToolStripButton();
		this._Tbr1_Button4 = new System.Windows.Forms.ToolStripButton();
		this._Tbr1_Button9 = new System.Windows.Forms.ToolStripButton();
		this._Tbr1_Button10 = new System.Windows.Forms.ToolStripButton();
		this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.cmbSQLVA = new System.Windows.Forms.ToolStripComboBox();
		this.lblSpace0 = new System.Windows.Forms.ToolStripLabel();
		this.lblNodeO = new System.Windows.Forms.ToolStripLabel();
		this.CmbNodeO = new System.Windows.Forms.ToolStripComboBox();
		this.CmdNodeO = new System.Windows.Forms.ToolStripButton();
		this.lblspace1 = new System.Windows.Forms.ToolStripLabel();
		this.lblUsr = new System.Windows.Forms.ToolStripLabel();
		this.TxtUsr = new System.Windows.Forms.ToolStripComboBox();
		this.lblspace2 = new System.Windows.Forms.ToolStripLabel();
		this.lblPW = new System.Windows.Forms.ToolStripLabel();
		this.TxtPW = new System.Windows.Forms.TextBox();
		this.MainMenu1.SuspendLayout();
		this.Tbr1.SuspendLayout();
		base.SuspendLayout();
		this.Text1.AcceptsReturn = true;
		this.Text1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Text1.BackColor = System.Drawing.SystemColors.Window;
		this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.Text1.Font = new System.Drawing.Font("Courier New", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Text1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Text1.Location = new System.Drawing.Point(0, 71);
		this.Text1.MaxLength = 0;
		this.Text1.Multiline = true;
		this.Text1.Name = "Text1";
		this.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Text1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.Text1.Size = new System.Drawing.Size(827, 354);
		this.Text1.TabIndex = 0;
		this.ToolTip1.SetToolTip(this.Text1, "Get Help on Nodes");
		this.Text1.WordWrap = false;
		this.MainMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.mnuFile, this.mnuEdit, this.mnuQuery, this.mnuUtilities, this.mnuOut, this.mnuHelp });
		this.MainMenu1.Location = new System.Drawing.Point(0, 0);
		this.MainMenu1.Name = "MainMenu1";
		this.MainMenu1.Size = new System.Drawing.Size(827, 28);
		this.MainMenu1.TabIndex = 2;
		this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[11]
		{
			this.mnuNew, this.mnuOpen, this.mnuSave, this.mnuSaveAs, this.mnuEdtDash1, this.mnuRunQueryNormally, this.mnuRunExplainPlan, this.mnuRunDescribe, this.mnuRunGetError, this.mnuEdtDash2,
			this.mnuClose
		});
		this.mnuFile.Name = "mnuFile";
		this.mnuFile.Size = new System.Drawing.Size(46, 24);
		this.mnuFile.Text = "&File";
		this.mnuNew.Name = "mnuNew";
		this.mnuNew.ShortcutKeys = System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Control;
		this.mnuNew.Size = new System.Drawing.Size(260, 26);
		this.mnuNew.Text = "&New Query";
		this.mnuOpen.Name = "mnuOpen";
		this.mnuOpen.ShortcutKeys = System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control;
		this.mnuOpen.Size = new System.Drawing.Size(260, 26);
		this.mnuOpen.Text = "&Open Query...";
		this.mnuSave.Name = "mnuSave";
		this.mnuSave.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.mnuSave.Size = new System.Drawing.Size(260, 26);
		this.mnuSave.Text = "&Save Query";
		this.mnuSaveAs.Name = "mnuSaveAs";
		this.mnuSaveAs.Size = new System.Drawing.Size(260, 26);
		this.mnuSaveAs.Text = "Save Query &As ...";
		this.mnuEdtDash1.Name = "mnuEdtDash1";
		this.mnuEdtDash1.Size = new System.Drawing.Size(257, 6);
		this.mnuRunQueryNormally.Name = "mnuRunQueryNormally";
		this.mnuRunQueryNormally.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.mnuRunQueryNormally.Size = new System.Drawing.Size(260, 26);
		this.mnuRunQueryNormally.Text = "&Run Query";
		this.mnuRunExplainPlan.Name = "mnuRunExplainPlan";
		this.mnuRunExplainPlan.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.mnuRunExplainPlan.Size = new System.Drawing.Size(260, 26);
		this.mnuRunExplainPlan.Text = "Generate Explain &Plan";
		this.mnuRunDescribe.Name = "mnuRunDescribe";
		this.mnuRunDescribe.ShortcutKeys = System.Windows.Forms.Keys.F9;
		this.mnuRunDescribe.Size = new System.Drawing.Size(260, 26);
		this.mnuRunDescribe.Text = "&Describe Table(s)";
		this.mnuRunGetError.Name = "mnuRunGetError";
		this.mnuRunGetError.ShortcutKeys = System.Windows.Forms.Keys.F11;
		this.mnuRunGetError.Size = new System.Drawing.Size(260, 26);
		this.mnuRunGetError.Text = "&Get Error Line";
		this.mnuEdtDash2.Name = "mnuEdtDash2";
		this.mnuEdtDash2.Size = new System.Drawing.Size(257, 6);
		this.mnuClose.Name = "mnuClose";
		this.mnuClose.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.mnuClose.Size = new System.Drawing.Size(260, 26);
		this.mnuClose.Text = "E&xit";
		this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.mnuUndoRedo, this.mnuCut, this.mnuCopy, this.mnuPaste, this.mnuSelectAll, this.mnuEdtDash0, this.mnuFind, this.mnuFindNext });
		this.mnuEdit.Name = "mnuEdit";
		this.mnuEdit.Size = new System.Drawing.Size(49, 24);
		this.mnuEdit.Text = "&Edit";
		this.mnuUndoRedo.Name = "mnuUndoRedo";
		this.mnuUndoRedo.ShortcutKeys = System.Windows.Forms.Keys.Z | System.Windows.Forms.Keys.Control;
		this.mnuUndoRedo.Size = new System.Drawing.Size(224, 26);
		this.mnuUndoRedo.Text = "Undo";
		this.mnuCut.Name = "mnuCut";
		this.mnuCut.ShortcutKeys = System.Windows.Forms.Keys.X | System.Windows.Forms.Keys.Control;
		this.mnuCut.Size = new System.Drawing.Size(224, 26);
		this.mnuCut.Text = "Cu&t";
		this.mnuCopy.Name = "mnuCopy";
		this.mnuCopy.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
		this.mnuCopy.Size = new System.Drawing.Size(224, 26);
		this.mnuCopy.Text = "&Copy";
		this.mnuPaste.Name = "mnuPaste";
		this.mnuPaste.ShortcutKeys = System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control;
		this.mnuPaste.Size = new System.Drawing.Size(224, 26);
		this.mnuPaste.Text = "&Paste";
		this.mnuSelectAll.Name = "mnuSelectAll";
		this.mnuSelectAll.ShortcutKeys = System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control;
		this.mnuSelectAll.Size = new System.Drawing.Size(224, 26);
		this.mnuSelectAll.Text = "&Select All";
		this.mnuEdtDash0.Name = "mnuEdtDash0";
		this.mnuEdtDash0.Size = new System.Drawing.Size(221, 6);
		this.mnuFind.Name = "mnuFind";
		this.mnuFind.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
		this.mnuFind.Size = new System.Drawing.Size(224, 26);
		this.mnuFind.Text = "&Find...";
		this.mnuFindNext.Enabled = false;
		this.mnuFindNext.Name = "mnuFindNext";
		this.mnuFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
		this.mnuFindNext.Size = new System.Drawing.Size(224, 26);
		this.mnuFindNext.Text = "Find &Next";
		this.mnuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuOutVAX, this.mnuOutExcel2, this.mnuRow2, this.mnuMacroFile, this.mnuCopyOpt });
		this.mnuQuery.Name = "mnuQuery";
		this.mnuQuery.Size = new System.Drawing.Size(62, 24);
		this.mnuQuery.Text = "&Query";
		this.mnuOutVAX.Checked = true;
		this.mnuOutVAX.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuOutVAX.Name = "mnuOutVAX";
		this.mnuOutVAX.Size = new System.Drawing.Size(470, 26);
		this.mnuOutVAX.Text = "Output To &Terminal";
		this.mnuOutExcel2.Name = "mnuOutExcel2";
		this.mnuOutExcel2.Size = new System.Drawing.Size(470, 26);
		this.mnuOutExcel2.Text = "Output To E&xcel or JMP (No)";
		this.mnuRow2.Name = "mnuRow2";
		this.mnuRow2.Size = new System.Drawing.Size(470, 26);
		this.mnuRow2.Text = "Set Row &Limit (No)";
		this.mnuMacroFile.Name = "mnuMacroFile";
		this.mnuMacroFile.Size = new System.Drawing.Size(470, 26);
		this.mnuMacroFile.Text = "Temporary &Macro File";
		this.mnuCopyOpt.Name = "mnuCopyOpt";
		this.mnuCopyOpt.ShortcutKeys = System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control;
		this.mnuCopyOpt.Size = new System.Drawing.Size(470, 26);
		this.mnuCopyOpt.Text = "&Insert New Query Options Based on Menu Settings";
		this.mnuUtilities.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.mnuExcel_I, this.mnuExcel_C, this.mnuD1, this.mnuGRID_I, this.ToolStripSeparator1, this.mnuJMP_I, this.mnuJMP_C });
		this.mnuUtilities.Name = "mnuUtilities";
		this.mnuUtilities.Size = new System.Drawing.Size(69, 24);
		this.mnuUtilities.Text = "&Output";
		this.mnuExcel_I.Name = "mnuExcel_I";
		this.mnuExcel_I.Size = new System.Drawing.Size(224, 26);
		this.mnuExcel_I.Tag = "E";
		this.mnuExcel_I.Text = "Show Excel";
		this.mnuExcel_C.Name = "mnuExcel_C";
		this.mnuExcel_C.Size = new System.Drawing.Size(224, 26);
		this.mnuExcel_C.Tag = "E";
		this.mnuExcel_C.Text = "Close Excel";
		this.mnuD1.Name = "mnuD1";
		this.mnuD1.Size = new System.Drawing.Size(221, 6);
		this.mnuGRID_I.Name = "mnuGRID_I";
		this.mnuGRID_I.Size = new System.Drawing.Size(224, 26);
		this.mnuGRID_I.Tag = "G";
		this.mnuGRID_I.Text = "Show Grid";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(221, 6);
		this.mnuJMP_I.Name = "mnuJMP_I";
		this.mnuJMP_I.Size = new System.Drawing.Size(224, 26);
		this.mnuJMP_I.Tag = "J";
		this.mnuJMP_I.Text = "Show JMP";
		this.mnuJMP_C.Name = "mnuJMP_C";
		this.mnuJMP_C.Size = new System.Drawing.Size(224, 26);
		this.mnuJMP_C.Tag = "J";
		this.mnuJMP_C.Text = "Close JMP";
		this.mnuOut.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuOutWF });
		this.mnuOut.Name = "mnuOut";
		this.mnuOut.Size = new System.Drawing.Size(96, 24);
		this.mnuOut.Text = "Output File";
		this.mnuOut.Visible = false;
		this.mnuOutWF.Checked = true;
		this.mnuOutWF.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuOutWF.Name = "mnuOutWF";
		this.mnuOutWF.Size = new System.Drawing.Size(253, 26);
		this.mnuOutWF.Text = "Output File {MyInput.txt}";
		this.mnuHelp.Font = new System.Drawing.Font("Arial", 8.25f);
		this.mnuHelp.Name = "mnuHelp";
		this.mnuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.mnuHelp.Size = new System.Drawing.Size(81, 24);
		this.mnuHelp.Text = "&Help (F1)";
		this.Tbr1.AutoSize = false;
		this.Tbr1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.Tbr1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this._Tbr1_Button1, this._Tbr1_Button2, this._Tbr1_Button3, this._Tbr1_Button4, this._Tbr1_Button9, this._Tbr1_Button10, this.ToolStripLabel1, this.cmbSQLVA, this.lblSpace0, this.lblNodeO,
			this.CmbNodeO, this.CmdNodeO, this.lblspace1, this.lblUsr, this.TxtUsr, this.lblspace2, this.lblPW
		});
		this.Tbr1.Location = new System.Drawing.Point(0, 28);
		this.Tbr1.Name = "Tbr1";
		this.Tbr1.Size = new System.Drawing.Size(827, 40);
		this.Tbr1.TabIndex = 1;
		this._Tbr1_Button1.AutoSize = false;
		this._Tbr1_Button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button1.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button1.Image");
		this._Tbr1_Button1.ImageTransparentColor = System.Drawing.Color.Red;
		this._Tbr1_Button1.Name = "_Tbr1_Button1";
		this._Tbr1_Button1.Size = new System.Drawing.Size(24, 22);
		this._Tbr1_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button1.ToolTipText = "New Query";
		this._Tbr1_Button2.AutoSize = false;
		this._Tbr1_Button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button2.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button2.Image");
		this._Tbr1_Button2.ImageTransparentColor = System.Drawing.Color.White;
		this._Tbr1_Button2.Name = "_Tbr1_Button2";
		this._Tbr1_Button2.Size = new System.Drawing.Size(24, 22);
		this._Tbr1_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button2.ToolTipText = "Open Query";
		this._Tbr1_Button3.AutoSize = false;
		this._Tbr1_Button3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button3.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button3.Image");
		this._Tbr1_Button3.ImageTransparentColor = System.Drawing.Color.Red;
		this._Tbr1_Button3.Name = "_Tbr1_Button3";
		this._Tbr1_Button3.Size = new System.Drawing.Size(24, 22);
		this._Tbr1_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button3.ToolTipText = "Save Query";
		this._Tbr1_Button4.AutoSize = false;
		this._Tbr1_Button4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button4.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button4.Image");
		this._Tbr1_Button4.ImageTransparentColor = System.Drawing.Color.Red;
		this._Tbr1_Button4.Name = "_Tbr1_Button4";
		this._Tbr1_Button4.Size = new System.Drawing.Size(24, 22);
		this._Tbr1_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button4.ToolTipText = "Run Query";
		this._Tbr1_Button9.AutoSize = false;
		this._Tbr1_Button9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button9.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button9.Image");
		this._Tbr1_Button9.ImageTransparentColor = System.Drawing.Color.Magenta;
		this._Tbr1_Button9.Name = "_Tbr1_Button9";
		this._Tbr1_Button9.Size = new System.Drawing.Size(24, 24);
		this._Tbr1_Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button9.ToolTipText = "Show SQL Session";
		this._Tbr1_Button10.AutoSize = false;
		this._Tbr1_Button10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._Tbr1_Button10.Image = (System.Drawing.Image)resources.GetObject("_Tbr1_Button10.Image");
		this._Tbr1_Button10.ImageTransparentColor = System.Drawing.Color.Red;
		this._Tbr1_Button10.Name = "_Tbr1_Button10";
		this._Tbr1_Button10.Size = new System.Drawing.Size(24, 22);
		this._Tbr1_Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._Tbr1_Button10.ToolTipText = "Cancel Query";
		this.ToolStripLabel1.Name = "ToolStripLabel1";
		this.ToolStripLabel1.Size = new System.Drawing.Size(17, 37);
		this.ToolStripLabel1.Text = "  ";
		this.cmbSQLVA.DropDownWidth = 120;
		this.cmbSQLVA.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		this.cmbSQLVA.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbSQLVA.Items.AddRange(new object[8] { "SQL->VA", "VA", "JSL", "R-SCRIPT", "CB_ACS", "SQLite", "WRITE-FILE", "WRITE" });
		this.cmbSQLVA.Name = "cmbSQLVA";
		this.cmbSQLVA.Size = new System.Drawing.Size(110, 40);
		this.cmbSQLVA.Text = "SQL->VA";
		this.lblSpace0.AutoSize = false;
		this.lblSpace0.Name = "lblSpace0";
		this.lblSpace0.Size = new System.Drawing.Size(10, 13);
		this.lblNodeO.Font = new System.Drawing.Font("Arial", 8.25f);
		this.lblNodeO.Name = "lblNodeO";
		this.lblNodeO.Size = new System.Drawing.Size(31, 37);
		this.lblNodeO.Text = "DB:";
		this.CmbNodeO.DropDownWidth = 200;
		this.CmbNodeO.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		this.CmbNodeO.Font = new System.Drawing.Font("Arial", 8.25f);
		this.CmbNodeO.Name = "CmbNodeO";
		this.CmbNodeO.Size = new System.Drawing.Size(145, 40);
		this.CmdNodeO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.CmdNodeO.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdNodeO.ForeColor = System.Drawing.Color.Blue;
		this.CmdNodeO.Image = (System.Drawing.Image)resources.GetObject("CmdNodeO.Image");
		this.CmdNodeO.ImageTransparentColor = System.Drawing.Color.White;
		this.CmdNodeO.Name = "CmdNodeO";
		this.CmdNodeO.Size = new System.Drawing.Size(29, 37);
		this.CmdNodeO.ToolTipText = "Gethelp on Nodes";
		this.lblspace1.AutoSize = false;
		this.lblspace1.Enabled = false;
		this.lblspace1.Name = "lblspace1";
		this.lblspace1.Size = new System.Drawing.Size(10, 24);
		this.lblUsr.Font = new System.Drawing.Font("Arial", 8.25f);
		this.lblUsr.Name = "lblUsr";
		this.lblUsr.Size = new System.Drawing.Size(30, 37);
		this.lblUsr.Text = "UN:";
		this.TxtUsr.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		this.TxtUsr.Font = new System.Drawing.Font("Arial", 8.25f);
		this.TxtUsr.Items.AddRange(new object[1] { "IWA" });
		this.TxtUsr.Name = "TxtUsr";
		this.TxtUsr.Size = new System.Drawing.Size(85, 40);
		this.lblspace2.AutoSize = false;
		this.lblspace2.Enabled = false;
		this.lblspace2.Name = "lblspace2";
		this.lblspace2.Size = new System.Drawing.Size(10, 13);
		this.lblPW.Font = new System.Drawing.Font("Arial", 8.25f);
		this.lblPW.Name = "lblPW";
		this.lblPW.Size = new System.Drawing.Size(34, 37);
		this.lblPW.Text = "PW:";
		this.TxtPW.Location = new System.Drawing.Point(670, 36);
		this.TxtPW.Name = "TxtPW";
		this.TxtPW.PasswordChar = '*';
		this.TxtPW.Size = new System.Drawing.Size(85, 23);
		this.TxtPW.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(827, 425);
		base.Controls.Add(this.TxtPW);
		base.Controls.Add(this.Tbr1);
		base.Controls.Add(this.Text1);
		base.Controls.Add(this.MainMenu1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8.25f);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(11, 37);
		base.Name = "FrmDTREdit";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Tag = "DTR";
		this.Text = "Editor - [Untitled]";
		this.MainMenu1.ResumeLayout(false);
		this.MainMenu1.PerformLayout();
		this.Tbr1.ResumeLayout(false);
		this.Tbr1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void DoRunQuery(short Index)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string myOtherServer = default(string);
		while (true)
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
				case 1269:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0031;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_0042;
						case 7:
							goto IL_004b;
						case 8:
							goto IL_0092;
						case 11:
							goto IL_00b2;
						case 12:
							goto IL_00e6;
						case 13:
							goto IL_00ef;
						case 14:
							goto IL_0107;
						case 15:
							goto IL_0114;
						case 16:
							goto IL_0126;
						case 17:
							goto IL_014a;
						case 18:
							goto IL_0158;
						case 19:
						case 20:
							goto IL_0165;
						case 22:
							goto IL_01d7;
						case 23:
							goto IL_01e4;
						case 25:
							goto IL_0205;
						case 28:
							goto IL_0221;
						case 29:
							goto IL_023d;
						case 30:
							goto IL_0254;
						case 32:
							goto IL_026f;
						case 33:
							goto IL_028f;
						case 35:
							goto IL_02aa;
						case 36:
							goto IL_02c6;
						case 37:
							goto IL_02dd;
						case 39:
							goto IL_02f8;
						case 40:
							goto IL_0314;
						case 41:
							goto IL_0348;
						case 43:
							goto IL_0363;
						case 44:
							goto IL_037a;
						case 45:
							goto IL_0393;
						case 46:
							goto IL_03ac;
						case 47:
							goto IL_03c5;
						case 48:
							goto IL_03de;
						case 49:
							goto IL_03ea;
						case 21:
						case 24:
						case 26:
						case 27:
						case 31:
						case 34:
						case 38:
						case 42:
						case 50:
						case 51:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 9:
						case 10:
						case 52:
						case 53:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_028f:
					num2 = 33;
					BuildSQL.RunQuery(text, 6, 1, -99, MacroFile);
					break;
					IL_0221:
					num2 = 28;
					text2 = Strings.Replace(Globals_Renamed.gJSLOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text);
					goto IL_023d;
					IL_026f:
					num2 = 32;
					text = Globals_Renamed.gROptions + APreScript + Text1.Text;
					goto IL_028f;
					IL_023d:
					num2 = 29;
					text = text2 + Text1.Text;
					goto IL_0254;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0031;
					IL_0031:
					num2 = 4;
					myOtherServer = "";
					goto IL_0039;
					IL_0039:
					num2 = 5;
					text2 = "";
					goto IL_0042;
					IL_0042:
					num2 = 6;
					text = "";
					goto IL_004b;
					IL_004b:
					num2 = 7;
					if ((Index == 1 || Index == 2 || Index == 3) && (LikeOperator.LikeString(CmbNodeO.Text, "TEXT*", CompareMethod.Binary) | (Operators.CompareString(cmbSQLVA.Text, "SQLite", TextCompare: false) == 0)))
					{
						goto IL_0092;
					}
					goto IL_00b2;
					IL_01d7:
					num2 = 22;
					if (Index == 0)
					{
						goto IL_01e4;
					}
					goto IL_0205;
					IL_01e4:
					num2 = 23;
					BuildSQL.RunQuery(Text1.Text, 1, 1, -99, "");
					break;
					IL_0254:
					num2 = 30;
					BuildSQL.RunQuery(text, 6, 1, -99, MacroFile);
					break;
					IL_0092:
					num2 = 8;
					Interaction.MsgBox("Getting the error line, table list or explain plans are not yet suppoted for Text files", MsgBoxStyle.Information, "Not Supported");
					goto end_IL_0001_3;
					IL_00b2:
					num2 = 11;
					Globals_Renamed.g_TS = "_" + DateAndTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CreateSpecificCulture("en-US")).Trim();
					goto IL_00e6;
					IL_00e6:
					num2 = 12;
					myOtherServer = Globals_Renamed.MyOtherServer1;
					goto IL_00ef;
					IL_00ef:
					num2 = 13;
					Globals_Renamed.MyOtherServer1 = Strings.Trim(CmbNodeO.Text);
					goto IL_0107;
					IL_0107:
					num2 = 14;
					if (Index == 2)
					{
						goto IL_0114;
					}
					goto IL_0126;
					IL_0114:
					num2 = 15;
					Globals_Renamed.MyOtherServer1 = BuildForm.SubStitute_Nodes_SQL(Globals_Renamed.MyOtherServer1);
					goto IL_0126;
					IL_0126:
					num2 = 16;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0 && FirstRunQ)
					{
						goto IL_014a;
					}
					goto IL_0165;
					IL_0205:
					num2 = 25;
					Interaction.MsgBox("Generating Explain Plans, Describing Tables, or Debugging SQL error lines are only relevant for SQL Queries", MsgBoxStyle.Information, "Explain Plans, Describing Tables, Debugging SQL");
					break;
					IL_014a:
					num2 = 17;
					BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
					goto IL_0158;
					IL_0158:
					num2 = 18;
					FirstRunQ = false;
					goto IL_0165;
					IL_0165:
					num2 = 20;
					switch (Strings.UCase(cmbSQLVA.Text))
					{
					case "VA":
						break;
					case "JSL":
						goto IL_0221;
					case "R-SCRIPT":
						goto IL_026f;
					case "CB_ACS":
						goto IL_02aa;
					case "WRITE-FILE":
						goto IL_02f8;
					default:
						goto IL_0363;
					}
					goto IL_01d7;
					IL_0363:
					num2 = 43;
					text = Save_Load_Query_File("S", "", 1, Index);
					goto IL_037a;
					IL_037a:
					num2 = 44;
					text = Strings.Replace(text, "@node@default (mars)@", "@MARSNODE@", 1, -1, CompareMethod.Text);
					goto IL_0393;
					IL_0393:
					num2 = 45;
					text = Strings.Replace(text, "@node@default (aries)@", "@ARIESNODE@", 1, -1, CompareMethod.Text);
					goto IL_03ac;
					IL_03ac:
					num2 = 46;
					text = Strings.Replace(text, "@node@default (oasys)@", "@OASYSNODE@", 1, -1, CompareMethod.Text);
					goto IL_03c5;
					IL_03c5:
					num2 = 47;
					text = Strings.Replace(text, "@node@default (other)@", "@OTHERNODE@", 1, -1, CompareMethod.Text);
					goto IL_03de;
					IL_03de:
					num2 = 48;
					text = BuildForm.SubStitute_Nodes_SQL(text);
					goto IL_03ea;
					IL_03ea:
					num2 = 49;
					BuildSQL.RunQuery(text, 6, 0, -99, MacroFile);
					break;
					IL_02f8:
					num2 = 39;
					text2 = Strings.Replace(Globals_Renamed.gWFOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text);
					goto IL_0314;
					IL_0314:
					num2 = 40;
					text = Strings.Replace(text2, "@FILE@", General_Procedures.Get_Node_Value(mnuOutWF.Text), 1, -1, CompareMethod.Text) + Text1.Text;
					goto IL_0348;
					IL_0348:
					num2 = 41;
					BuildSQL.RunQuery(text, 6, 1, -99, MacroFile);
					break;
					IL_02aa:
					num2 = 35;
					text2 = Strings.Replace(Globals_Renamed.gCBOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text);
					goto IL_02c6;
					IL_02c6:
					num2 = 36;
					text = text2 + Text1.Text;
					goto IL_02dd;
					IL_02dd:
					num2 = 37;
					BuildSQL.RunQuery(text, 6, 1, -99, MacroFile);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 51;
				Globals_Renamed.MyOtherServer1 = myOtherServer;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1269;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string Save_Load_Query_File(string MyMode, string MyFile, short ToRun, short QOpt)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text3 = default(string);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string sOpt = default(string);
		StreamReader streamReader = default(StreamReader);
		string text2 = default(string);
		short num5 = default(short);
		string text4 = default(string);
		string text5 = default(string);
		short num6 = default(short);
		short num7 = default(short);
		short num8 = default(short);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text12 = default(string);
		string text13 = default(string);
		string text14 = default(string);
		string ll_InFile = default(string);
		short num9 = default(short);
		short num10 = default(short);
		int num11 = default(int);
		string d_T = default(string);
		string d_UN = default(string);
		string d_Engine = default(string);
		string d_node = default(string);
		string text15 = default(string);
		string d_WorkDir = default(string);
		string d_PW = default(string);
		string d_ROWS = default(string);
		string d_LastO = default(string);
		string d_CSV = default(string);
		string d_EXP = default(string);
		string sQuery = default(string);
		short num12 = default(short);
		short num13 = default(short);
		int num14 = default(int);
		string text16 = default(string);
		string text17 = default(string);
		string text18 = default(string);
		string text19 = default(string);
		string text20 = default(string);
		string result = default(string);
		string[] DynArray = default(string[]);
		short num15 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num4;
					string text9;
					switch (try0001_dispatch)
					{
					default:
						num2 = 1;
						text3 = "";
						goto IL_000b;
					case 8806:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
								break;
							case 1:
								goto IL_1bda;
							default:
								goto end_IL_0001;
							}
							goto IL_1ac2;
						}
						IL_0dc2:
						num2 = 246;
						text = text + Globals_Renamed.CRLF + "<---- New Query ---->" + Globals_Renamed.CRLF + sOpt;
						goto IL_0e13;
						IL_0dfb:
						num2 = 248;
						text = text + Globals_Renamed.CRLF + sOpt;
						goto IL_0e13;
						IL_0db2:
						num2 = 245;
						if (ToRun == 1)
						{
							goto IL_0dc2;
						}
						goto IL_0dfb;
						IL_1bda:
						num4 = unchecked(num + 1);
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
							goto IL_0022;
						case 6:
							goto IL_002b;
						case 7:
							goto IL_0034;
						case 8:
							goto IL_0039;
						case 9:
							goto IL_003e;
						case 10:
							goto IL_0044;
						case 11:
							goto IL_004e;
						case 12:
							goto IL_0058;
						case 13:
							goto IL_0062;
						case 14:
							goto IL_006c;
						case 15:
							goto IL_0076;
						case 16:
							goto IL_0080;
						case 17:
							goto IL_008a;
						case 18:
							goto IL_0094;
						case 19:
							goto IL_009e;
						case 20:
							goto IL_00a8;
						case 21:
							goto IL_00ae;
						case 22:
							goto IL_00b4;
						case 23:
							goto IL_00ba;
						case 24:
							goto IL_00c4;
						case 25:
							goto IL_00ce;
						case 26:
							goto IL_00d8;
						case 27:
							goto IL_00e2;
						case 28:
							goto IL_00ec;
						case 29:
							goto IL_00f6;
						case 30:
							goto IL_0100;
						case 31:
							goto IL_010a;
						case 32:
							goto IL_0114;
						case 33:
							goto IL_011e;
						case 34:
							goto IL_0128;
						case 35:
							goto IL_0132;
						case 36:
							goto IL_0138;
						case 37:
							goto IL_013e;
						case 38:
							goto IL_0144;
						case 39:
							goto IL_014e;
						case 40:
							goto IL_0158;
						case 41:
							goto IL_0162;
						case 42:
							goto IL_016c;
						case 43:
							goto IL_0176;
						case 44:
							goto IL_0180;
						case 45:
							goto IL_0189;
						case 46:
							goto IL_0194;
						case 47:
							goto IL_019f;
						case 48:
							goto IL_01a6;
						case 50:
							goto IL_01e2;
						case 51:
							goto IL_01eb;
						case 52:
							goto IL_0200;
						case 53:
							goto IL_0222;
						case 54:
							goto IL_022c;
						case 56:
							goto IL_023c;
						case 57:
							goto IL_0255;
						case 58:
							goto IL_025f;
						case 60:
							goto IL_0277;
						case 61:
							goto IL_0288;
						case 62:
							goto IL_02a1;
						case 64:
							goto IL_02ac;
						case 63:
						case 65:
						case 66:
							goto IL_02c4;
						case 67:
							goto IL_02d2;
						case 68:
							goto IL_02eb;
						case 69:
							goto IL_02f5;
						case 71:
							goto IL_030c;
						case 72:
							goto IL_0325;
						case 73:
							goto IL_032f;
						case 75:
							goto IL_0346;
						case 76:
							goto IL_035f;
						case 77:
							goto IL_0369;
						case 79:
							goto IL_0380;
						case 80:
							goto IL_03d8;
						case 81:
							goto IL_03e2;
						case 83:
							goto IL_03f9;
						case 84:
							goto IL_0412;
						case 85:
							goto IL_041c;
						case 87:
							goto IL_0433;
						case 88:
							goto IL_044c;
						case 89:
							goto IL_0456;
						case 91:
							goto IL_046d;
						case 92:
							goto IL_0486;
						case 93:
							goto IL_0490;
						case 94:
							goto IL_04ab;
						case 96:
							goto IL_04b7;
						case 97:
							goto IL_04d0;
						case 98:
							goto IL_04da;
						case 100:
							goto IL_04fb;
						case 101:
							goto IL_0514;
						case 102:
							goto IL_051e;
						case 104:
							goto IL_053f;
						case 105:
							goto IL_0558;
						case 106:
							goto IL_0562;
						case 108:
							goto IL_0583;
						case 109:
							goto IL_059c;
						case 110:
							goto IL_05a6;
						case 111:
							goto IL_05b7;
						case 113:
							goto IL_05c3;
						case 114:
							goto IL_05ee;
						case 115:
							goto IL_05f8;
						case 117:
							goto IL_060f;
						case 118:
							goto IL_062b;
						case 119:
							goto IL_0631;
						case 120:
							goto IL_063b;
						case 123:
							goto IL_0658;
						case 124:
							goto IL_0670;
						case 125:
							goto IL_067e;
						case 127:
							goto IL_0693;
						case 128:
							goto IL_06af;
						case 126:
						case 129:
						case 130:
							goto IL_06d7;
						case 121:
						case 122:
						case 131:
							goto IL_06f3;
						case 132:
							goto IL_0704;
						case 134:
							goto IL_071c;
						case 135:
							goto IL_0729;
						case 55:
						case 59:
						case 70:
						case 74:
						case 78:
						case 82:
						case 86:
						case 90:
						case 95:
						case 99:
						case 103:
						case 107:
						case 112:
						case 116:
						case 133:
						case 136:
						case 137:
						case 138:
							goto IL_073f;
						case 139:
							goto IL_078a;
						case 141:
							goto IL_079d;
						case 142:
							goto IL_07ce;
						case 144:
							goto IL_07de;
						case 145:
							goto IL_0800;
						case 147:
							goto IL_0816;
						case 148:
							goto IL_0838;
						case 140:
						case 143:
						case 146:
						case 149:
						case 150:
							goto IL_0852;
						case 151:
							goto IL_086b;
						case 152:
							goto IL_0887;
						case 154:
							goto IL_0897;
						case 155:
							goto IL_08b3;
						case 153:
						case 156:
						case 157:
							goto IL_08c1;
						case 158:
							goto IL_08d1;
						case 160:
							goto IL_08e2;
						case 159:
						case 161:
						case 162:
							goto IL_08f0;
						case 163:
							goto IL_0904;
						case 164:
							goto IL_0933;
						case 166:
							goto IL_0943;
						case 167:
							goto IL_096d;
						case 169:
							goto IL_0984;
						case 165:
						case 168:
						case 170:
						case 171:
							goto IL_0992;
						case 172:
							goto IL_09a6;
						case 173:
							goto IL_09c2;
						case 175:
							goto IL_09d2;
						case 176:
							goto IL_09ee;
						case 178:
							goto IL_09ff;
						case 174:
						case 177:
						case 179:
						case 180:
							goto IL_0a18;
						case 181:
							goto IL_0a2c;
						case 182:
							goto IL_0a43;
						case 184:
							goto IL_0a54;
						case 183:
						case 185:
						case 186:
							goto IL_0a62;
						case 187:
							goto IL_0a76;
						case 188:
							goto IL_0a94;
						case 189:
						case 190:
							goto IL_0aaf;
						case 191:
							goto IL_0aca;
						case 192:
							goto IL_0ae0;
						case 194:
							goto IL_0af0;
						case 195:
							goto IL_0b06;
						case 197:
							goto IL_0b16;
						case 198:
							goto IL_0b2c;
						case 193:
						case 196:
						case 199:
						case 200:
							goto IL_0b3a;
						case 201:
							goto IL_0b4d;
						case 202:
							goto IL_0b5e;
						case 203:
							goto IL_0b6f;
						case 205:
							goto IL_0b84;
						case 206:
							goto IL_0b95;
						case 208:
							goto IL_0ba5;
						case 209:
							goto IL_0bb6;
						case 211:
							goto IL_0bc7;
						case 214:
							goto IL_0bde;
						case 215:
							goto IL_0c03;
						case 217:
							goto IL_0c1a;
						case 221:
							goto IL_0c33;
						case 204:
						case 207:
						case 210:
						case 212:
						case 213:
						case 216:
						case 218:
						case 219:
						case 220:
						case 222:
						case 223:
							goto IL_0c47;
						case 224:
							goto IL_0c54;
						case 225:
							goto IL_0c5d;
						case 228:
							goto IL_0c71;
						case 229:
							goto IL_0c87;
						case 230:
							goto IL_0c98;
						case 231:
							goto IL_0cb1;
						case 233:
							goto IL_0cce;
						case 234:
							goto IL_0cdd;
						case 232:
						case 235:
						case 236:
							goto IL_0ceb;
						case 237:
							goto IL_0cfa;
						case 238:
							goto IL_0d2a;
						case 239:
							goto IL_0d37;
						case 240:
							goto IL_0d44;
						case 241:
							goto IL_0d6f;
						case 242:
							goto IL_0d83;
						case 243:
							goto IL_0da4;
						case 245:
							goto IL_0db2;
						case 246:
							goto IL_0dc2;
						case 248:
							goto IL_0dfb;
						case 244:
						case 247:
						case 249:
						case 250:
						case 251:
							goto IL_0e13;
						case 252:
							goto IL_0e2e;
						case 255:
							goto IL_0e4a;
						case 256:
							goto IL_0e57;
						case 253:
						case 254:
						case 257:
						case 258:
						case 259:
							goto IL_0e67;
						case 260:
							goto IL_0e88;
						case 226:
						case 227:
						case 261:
							goto IL_0e96;
						case 263:
							goto IL_0ebb;
						case 264:
							goto IL_0ec3;
						case 265:
							goto IL_0ed1;
						case 266:
							goto IL_0ede;
						case 267:
							goto IL_0eeb;
						case 268:
							goto IL_0f04;
						case 269:
						case 270:
							goto IL_0f15;
						case 271:
							goto IL_0f23;
						case 272:
							goto IL_0f31;
						case 273:
							goto IL_0f3a;
						case 274:
							goto IL_0f60;
						case 275:
							goto IL_0f6a;
						case 277:
							goto IL_0f7e;
						case 278:
							goto IL_0f98;
						case 279:
							goto IL_0fa6;
						case 280:
							goto IL_0fbc;
						case 281:
							goto IL_0fc5;
						case 282:
							goto IL_0fd8;
						case 283:
							goto IL_0fe5;
						case 284:
							goto IL_1011;
						case 286:
							goto IL_1020;
						case 287:
							goto IL_104c;
						case 289:
							goto IL_105b;
						case 290:
							goto IL_106f;
						case 291:
							goto IL_1085;
						case 292:
							goto IL_1099;
						case 293:
							goto IL_10b7;
						case 294:
							goto IL_10cb;
						case 296:
							goto IL_13cc;
						case 297:
							goto IL_1400;
						case 300:
							goto IL_141f;
						case 301:
							goto IL_143b;
						case 304:
							goto IL_1457;
						case 305:
							goto IL_1473;
						case 307:
							goto IL_148e;
						case 310:
							goto IL_14a9;
						case 312:
							goto IL_14c3;
						case 313:
							goto IL_14e9;
						case 315:
							goto IL_1500;
						case 318:
							goto IL_151a;
						case 319:
							goto IL_1538;
						case 321:
							goto IL_1548;
						case 322:
							goto IL_1566;
						case 324:
							goto IL_1576;
						case 325:
							goto IL_1597;
						case 327:
							goto IL_15a5;
						case 320:
						case 323:
						case 326:
						case 328:
						case 329:
							goto IL_15b3;
						case 332:
							goto IL_15cd;
						case 334:
							goto IL_15e2;
						case 336:
							goto IL_15f7;
						case 338:
							goto IL_160c;
						case 340:
							goto IL_1621;
						case 342:
							goto IL_1636;
						case 344:
							goto IL_164b;
						case 346:
							goto IL_1660;
						case 348:
							goto IL_1675;
						case 349:
							goto IL_1689;
						case 350:
							goto IL_16a2;
						case 352:
							goto IL_16bd;
						case 355:
							goto IL_16d8;
						case 356:
							goto IL_16ec;
						case 357:
							goto IL_1708;
						case 362:
							goto IL_172c;
						case 363:
							goto IL_173d;
						case 364:
							goto IL_1759;
						case 366:
							goto IL_176e;
						case 285:
						case 288:
						case 295:
						case 298:
						case 299:
						case 302:
						case 303:
						case 306:
						case 308:
						case 309:
						case 311:
						case 314:
						case 316:
						case 317:
						case 330:
						case 331:
						case 333:
						case 335:
						case 337:
						case 339:
						case 341:
						case 343:
						case 345:
						case 347:
						case 351:
						case 353:
						case 354:
						case 358:
						case 359:
						case 360:
						case 361:
						case 365:
						case 367:
						case 368:
						case 369:
							goto IL_1786;
						case 370:
							goto IL_179c;
						case 371:
							goto IL_17a5;
						case 372:
							goto IL_17b6;
						case 373:
							goto IL_17ce;
						case 374:
							goto IL_17db;
						case 376:
							goto IL_17eb;
						case 377:
							goto IL_17fc;
						case 378:
							goto IL_1814;
						case 379:
							goto IL_1821;
						case 375:
						case 380:
						case 381:
							goto IL_182f;
						case 382:
							goto IL_183c;
						case 383:
							goto IL_18d2;
						case 384:
							goto IL_18df;
						case 385:
							goto IL_18fb;
						case 386:
							goto IL_1916;
						case 387:
							goto IL_1932;
						case 388:
							goto IL_194d;
						case 389:
							goto IL_1969;
						case 390:
							goto IL_1984;
						case 391:
							goto IL_19c2;
						case 392:
						case 393:
							goto IL_1a1d;
						case 394:
							goto IL_1a39;
						case 395:
							goto IL_1a4f;
						case 396:
							goto IL_1a6b;
						case 397:
							goto IL_1a81;
						case 401:
							goto IL_1ac2;
						case 402:
							goto IL_1b30;
						case 403:
							goto IL_1b59;
						case 404:
							goto IL_1b6a;
						case 406:
							goto IL_1b77;
						case 407:
							goto IL_1b88;
						case 408:
							goto IL_1b96;
						case 405:
						case 409:
						case 410:
							goto IL_1ba5;
						case 411:
							goto IL_1bb2;
						case 49:
						case 262:
						case 276:
						case 398:
						case 399:
						case 400:
						case 412:
						case 413:
						case 414:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 415:
							goto end_IL_0001_3;
						}
						goto default;
						IL_1ac2:
						num2 = 401;
						Interaction.MsgBox("Error loading Query file: " + MyFile + ". (" + Conversions.ToString(Information.Err().Number) + ":" + Information.Err().Description + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Load Error");
						goto IL_1b30;
						IL_1b30:
						num2 = 402;
						if ((Information.Err().Number == 76) | (Information.Err().Number == 53))
						{
							goto IL_1b59;
						}
						goto IL_1b77;
						IL_1b59:
						num2 = 403;
						Information.Err().Clear();
						goto IL_1b6a;
						IL_1b6a:
						ProjectData.ClearProjectError();
						num3 = -3;
						goto IL_1ba5;
						IL_1b77:
						num2 = 406;
						Information.Err().Clear();
						goto IL_1b88;
						IL_1b88:
						num2 = 407;
						streamReader.Close();
						goto IL_1b96;
						IL_1b96:
						num2 = 408;
						streamReader.Dispose();
						goto IL_1ba5;
						IL_1ba5:
						num2 = 410;
						text2 = "";
						goto IL_1bb2;
						IL_1bb2:
						num2 = 411;
						text = "";
						break;
						IL_000b:
						num2 = 2;
						text = "";
						goto IL_0014;
						IL_0014:
						num2 = 3;
						sOpt = "";
						goto IL_001d;
						IL_001d:
						num2 = 4;
						num5 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 5;
						text4 = "";
						goto IL_002b;
						IL_002b:
						num2 = 6;
						text5 = "";
						goto IL_0034;
						IL_0034:
						num2 = 7;
						num6 = 0;
						goto IL_0039;
						IL_0039:
						num2 = 8;
						num7 = 0;
						goto IL_003e;
						IL_003e:
						num2 = 9;
						num8 = 0;
						goto IL_0044;
						IL_0044:
						num2 = 10;
						text6 = "";
						goto IL_004e;
						IL_004e:
						num2 = 11;
						text7 = "";
						goto IL_0058;
						IL_0058:
						num2 = 12;
						text8 = "";
						goto IL_0062;
						IL_0062:
						num2 = 13;
						text9 = "";
						goto IL_006c;
						IL_006c:
						num2 = 14;
						text10 = "";
						goto IL_0076;
						IL_0076:
						num2 = 15;
						text11 = "";
						goto IL_0080;
						IL_0080:
						num2 = 16;
						text12 = "";
						goto IL_008a;
						IL_008a:
						num2 = 17;
						text13 = "";
						goto IL_0094;
						IL_0094:
						num2 = 18;
						text14 = "";
						goto IL_009e;
						IL_009e:
						num2 = 19;
						ll_InFile = "";
						goto IL_00a8;
						IL_00a8:
						num2 = 20;
						num9 = 0;
						goto IL_00ae;
						IL_00ae:
						num2 = 21;
						num10 = 0;
						goto IL_00b4;
						IL_00b4:
						num2 = 22;
						num11 = 0;
						goto IL_00ba;
						IL_00ba:
						num2 = 23;
						d_T = "";
						goto IL_00c4;
						IL_00c4:
						num2 = 24;
						d_UN = "";
						goto IL_00ce;
						IL_00ce:
						num2 = 25;
						d_Engine = "";
						goto IL_00d8;
						IL_00d8:
						num2 = 26;
						d_node = "";
						goto IL_00e2;
						IL_00e2:
						num2 = 27;
						text15 = "";
						goto IL_00ec;
						IL_00ec:
						num2 = 28;
						d_WorkDir = ".\\";
						goto IL_00f6;
						IL_00f6:
						num2 = 29;
						d_PW = "";
						goto IL_0100;
						IL_0100:
						num2 = 30;
						d_ROWS = "";
						goto IL_010a;
						IL_010a:
						num2 = 31;
						d_LastO = "";
						goto IL_0114;
						IL_0114:
						num2 = 32;
						d_CSV = "";
						goto IL_011e;
						IL_011e:
						num2 = 33;
						d_EXP = "";
						goto IL_0128;
						IL_0128:
						num2 = 34;
						sQuery = "";
						goto IL_0132;
						IL_0132:
						num2 = 35;
						num12 = 0;
						goto IL_0138;
						IL_0138:
						num2 = 36;
						num13 = 0;
						goto IL_013e;
						IL_013e:
						num2 = 37;
						num14 = 0;
						goto IL_0144;
						IL_0144:
						num2 = 38;
						text16 = "";
						goto IL_014e;
						IL_014e:
						num2 = 39;
						text2 = "";
						goto IL_0158;
						IL_0158:
						num2 = 40;
						text17 = "";
						goto IL_0162;
						IL_0162:
						num2 = 41;
						text18 = "";
						goto IL_016c;
						IL_016c:
						num2 = 42;
						text19 = "";
						goto IL_0176;
						IL_0176:
						num2 = 43;
						text20 = "";
						goto IL_0180;
						IL_0180:
						num2 = 44;
						result = "";
						goto IL_0189;
						IL_0189:
						num2 = 45;
						ll_InFile = out_odbc;
						goto IL_0194;
						IL_0194:
						num2 = 46;
						num11 = out_row;
						goto IL_019f;
						IL_019f:
						num2 = 47;
						num9 = 0;
						goto IL_01a6;
						IL_01a6:
						num2 = 48;
						switch (MyMode)
						{
						case "S":
						case "O":
							break;
						case "L":
							goto IL_0ebb;
						default:
							goto end_IL_0001_2;
						}
						goto IL_01e2;
						IL_0ebb:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_0ec3;
						IL_0ec3:
						num2 = 264;
						streamReader = new StreamReader(MyFile);
						goto IL_0ed1;
						IL_0ed1:
						num2 = 265;
						out_row = -1;
						goto IL_0ede;
						IL_0ede:
						num2 = 266;
						text3 = "";
						goto IL_0eeb;
						IL_0eeb:
						num2 = 267;
						if (streamReader.Peek() != -1)
						{
							goto IL_0f04;
						}
						goto IL_0f15;
						IL_0f04:
						num2 = 268;
						text3 = streamReader.ReadToEnd();
						goto IL_0f15;
						IL_0f15:
						num2 = 270;
						streamReader.Close();
						goto IL_0f23;
						IL_0f23:
						num2 = 271;
						streamReader.Dispose();
						goto IL_0f31;
						IL_0f31:
						num2 = 272;
						streamReader = null;
						goto IL_0f3a;
						IL_0f3a:
						num2 = 273;
						if (Strings.InStr(Strings.UCase(text3), Strings.UCase("<---- New Query ---->")) != 0)
						{
							goto IL_0f60;
						}
						goto IL_0f7e;
						IL_0f60:
						num2 = 274;
						text = text3;
						goto IL_0f6a;
						IL_0f6a:
						num2 = 275;
						text2 = "";
						break;
						IL_0f7e:
						num2 = 277;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						goto IL_0f98;
						IL_0f98:
						num2 = 278;
						DynArray.Initialize();
						goto IL_0fa6;
						IL_0fa6:
						num2 = 279;
						num14 = General_Procedures.ParseAndFillArray(text3, Globals_Renamed.CRLF, ref DynArray);
						goto IL_0fbc;
						IL_0fbc:
						num2 = 280;
						num12 = 0;
						goto IL_0fc5;
						IL_0fc5:
						num2 = 281;
						num15 = (short)num14;
						num13 = 1;
						goto IL_1793;
						IL_1793:
						if (num13 <= num15)
						{
							goto IL_0fd8;
						}
						goto IL_179c;
						IL_179c:
						num2 = 370;
						DynArray = null;
						goto IL_17a5;
						IL_17a5:
						num2 = 371;
						if (num12 == 0)
						{
							goto IL_17b6;
						}
						goto IL_17eb;
						IL_17b6:
						num2 = 372;
						Interaction.MsgBox("Error in SQLPathFinder Query File. Missing the Options token <OPTIONS>", MsgBoxStyle.Information, "Badly Formed Query File");
						goto IL_17ce;
						IL_17ce:
						num2 = 373;
						text2 = "";
						goto IL_17db;
						IL_17db:
						num2 = 374;
						text = "";
						goto IL_182f;
						IL_17eb:
						num2 = 376;
						if (num12 == 1)
						{
							goto IL_17fc;
						}
						goto IL_182f;
						IL_17fc:
						num2 = 377;
						Interaction.MsgBox("Error in SQLPathFinder Query File. Missing the Options terminator token </OPTIONS>", MsgBoxStyle.Information, "Badly Formed Query File");
						goto IL_1814;
						IL_1814:
						num2 = 378;
						text2 = "";
						goto IL_1821;
						IL_1821:
						num2 = 379;
						text = "";
						goto IL_182f;
						IL_182f:
						num2 = 381;
						text2 = "";
						goto IL_183c;
						IL_183c:
						num2 = 382;
						if (!((Operators.CompareString(text11, "", TextCompare: false) != 0) | (Operators.CompareString(text10, "", TextCompare: false) != 0) | (Operators.CompareString(text12, "", TextCompare: false) != 0) | (Operators.CompareString(text16, "", TextCompare: false) != 0) | (Operators.CompareString(text13, "", TextCompare: false) != 0) | (Operators.CompareString(text14, "", TextCompare: false) != 0) | (Operators.CompareString(text17, "", TextCompare: false) != 0) | (Operators.CompareString(text18, "", TextCompare: false) != 0)))
						{
							break;
						}
						goto IL_18d2;
						IL_18d2:
						num2 = 383;
						text2 = "<OPTIONS>";
						goto IL_18df;
						IL_18df:
						num2 = 384;
						if (Operators.CompareString(text16, "", TextCompare: false) != 0)
						{
							goto IL_18fb;
						}
						goto IL_1916;
						IL_18fb:
						num2 = 385;
						text2 = text2 + Globals_Renamed.CRLF + "/TABLE=" + text16;
						goto IL_1916;
						IL_1916:
						num2 = 386;
						if (Operators.CompareString(text13, "", TextCompare: false) != 0)
						{
							goto IL_1932;
						}
						goto IL_194d;
						IL_1932:
						num2 = 387;
						text2 = text2 + Globals_Renamed.CRLF + "/STACK=" + text13;
						goto IL_194d;
						IL_194d:
						num2 = 388;
						if (Operators.CompareString(text14, "", TextCompare: false) != 0)
						{
							goto IL_1969;
						}
						goto IL_1984;
						IL_1969:
						num2 = 389;
						text2 = text2 + Globals_Renamed.CRLF + "/HEADERS=" + text14;
						goto IL_1984;
						IL_1984:
						num2 = 390;
						if ((Operators.CompareString(text11, "", TextCompare: false) != 0) | (Operators.CompareString(text10, "", TextCompare: false) != 0) | (Operators.CompareString(text12, "", TextCompare: false) != 0))
						{
							goto IL_19c2;
						}
						goto IL_1a1d;
						IL_19c2:
						num2 = 391;
						text2 = text2 + Globals_Renamed.CRLF + "/CTVALUE=" + text11 + Globals_Renamed.CRLF + "/CTHEADER=" + text10 + Globals_Renamed.CRLF + "/CTROW=" + text12;
						goto IL_1a1d;
						IL_1a1d:
						num2 = 393;
						if (Operators.CompareString(text17, "", TextCompare: false) != 0)
						{
							goto IL_1a39;
						}
						goto IL_1a4f;
						IL_1a39:
						num2 = 394;
						text2 = text2 + "\r\n/PREPROCESS_CSV=" + text17;
						goto IL_1a4f;
						IL_1a4f:
						num2 = 395;
						if (Operators.CompareString(text18, "", TextCompare: false) != 0)
						{
							goto IL_1a6b;
						}
						goto IL_1a81;
						IL_1a6b:
						num2 = 396;
						text2 = text2 + "\r\n/NOHEADERS=" + text18;
						goto IL_1a81;
						IL_1a81:
						num2 = 397;
						text2 = text2 + Globals_Renamed.CRLF + "</OPTIONS>" + Globals_Renamed.CRLF + Globals_Renamed.CRLF;
						break;
						IL_0fd8:
						num2 = 282;
						text7 = DynArray[num13];
						goto IL_0fe5;
						IL_0fe5:
						num2 = 283;
						if ((num12 == 0) & (Operators.CompareString(Strings.Trim(Strings.UCase(text7)), "<OPTIONS>", TextCompare: false) == 0))
						{
							goto IL_1011;
						}
						goto IL_1020;
						IL_1011:
						num2 = 284;
						num12 = 1;
						goto IL_1786;
						IL_1020:
						num2 = 286;
						if ((num12 == 1) & (Operators.CompareString(Strings.Trim(Strings.UCase(text7)), "</OPTIONS>", TextCompare: false) == 0))
						{
							goto IL_104c;
						}
						goto IL_105b;
						IL_104c:
						num2 = 287;
						num12 = 2;
						goto IL_1786;
						IL_105b:
						num2 = 289;
						if (num12 == 1)
						{
							goto IL_106f;
						}
						goto IL_172c;
						IL_106f:
						num2 = 290;
						num7 = (short)Strings.InStr(text7, "=");
						goto IL_1085;
						IL_1085:
						num2 = 291;
						if (num7 != 0)
						{
							goto IL_1099;
						}
						goto IL_1786;
						IL_1099:
						num2 = 292;
						text6 = Strings.UCase(Strings.Trim(Strings.Mid(text7, 1, num7 - 1)));
						goto IL_10b7;
						IL_10b7:
						num2 = 293;
						text8 = Strings.Mid(text7, num7 + 1);
						goto IL_10cb;
						IL_10cb:
						num2 = 294;
						switch (text6)
						{
						case "/OLEDB":
							break;
						case "/NODE":
							goto IL_141f;
						case "/UN":
							goto IL_1457;
						case "/PW":
							goto IL_14a9;
						case "/T":
							goto IL_14c3;
						case "/CSV":
							goto IL_151a;
						case "/STACK":
							goto IL_15cd;
						case "/PREPROCESS_CSV":
							goto IL_15e2;
						case "/NOHEADERS":
							goto IL_15f7;
						case "/HEADERS":
							goto IL_160c;
						case "/CTVALUE":
						case "/CTVAL":
							goto IL_1621;
						case "/CTHEADER":
							goto IL_1636;
						case "/CTROW":
							goto IL_164b;
						case "/TABLE":
							goto IL_1660;
						case "/ENGINE":
							goto IL_1675;
						case "/ROWS":
							goto IL_16d8;
						default:
							goto IL_1786;
						}
						goto IL_13cc;
						IL_16d8:
						num2 = 355;
						text5 = Strings.Trim(BuildForm.HG_Strip(text8));
						goto IL_16ec;
						IL_16ec:
						num2 = 356;
						if (Operators.CompareString(text5, "", TextCompare: false) != 0)
						{
							goto IL_1708;
						}
						goto IL_1786;
						IL_1708:
						num2 = 357;
						out_row = (int)Math.Round(Conversion.Val(text5));
						goto IL_1786;
						IL_1675:
						num2 = 348;
						text8 = Strings.Trim(Strings.UCase(text8));
						goto IL_1689;
						IL_1689:
						num2 = 349;
						if (LikeOperator.LikeString(text8, "SQLITE*", CompareMethod.Binary))
						{
							goto IL_16a2;
						}
						goto IL_16bd;
						IL_16a2:
						num2 = 350;
						cmbSQLVA.Text = "SQLite";
						goto IL_1786;
						IL_16bd:
						num2 = 352;
						cmbSQLVA.Text = "SQL->VA";
						goto IL_1786;
						IL_1660:
						num2 = 346;
						text16 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_164b:
						num2 = 344;
						text12 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_1636:
						num2 = 342;
						text10 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_1621:
						num2 = 340;
						text11 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_160c:
						num2 = 338;
						text14 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_15f7:
						num2 = 336;
						text18 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_15e2:
						num2 = 334;
						text17 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_15cd:
						num2 = 332;
						text13 = BuildForm.HG_Strip(text8);
						goto IL_1786;
						IL_151a:
						num2 = 318;
						if (LikeOperator.LikeString(Strings.UCase(text8), "PROMPT *", CompareMethod.Binary))
						{
							goto IL_1538;
						}
						goto IL_1548;
						IL_1538:
						num2 = 319;
						ll_InFile = "PROMPT";
						goto IL_15b3;
						IL_1548:
						num2 = 321;
						if (LikeOperator.LikeString(Strings.UCase(text8), "&PROMPT&(EXCE)&PROMPT&*", CompareMethod.Binary))
						{
							goto IL_1566;
						}
						goto IL_1576;
						IL_1566:
						num2 = 322;
						ll_InFile = "PROMPT";
						goto IL_15b3;
						IL_1576:
						num2 = 324;
						if (Operators.CompareString(Strings.Trim(text8), "", TextCompare: false) != 0)
						{
							goto IL_1597;
						}
						goto IL_15a5;
						IL_1597:
						num2 = 325;
						ll_InFile = text8;
						goto IL_15b3;
						IL_15a5:
						num2 = 327;
						ll_InFile = "";
						goto IL_15b3;
						IL_15b3:
						num2 = 329;
						out_odbc = ll_InFile;
						goto IL_1786;
						IL_14c3:
						num2 = 312;
						if (Operators.CompareString(General_Procedures.Transform_YN(Strings.UCase(text8)), "Yes", TextCompare: false) == 0)
						{
							goto IL_14e9;
						}
						goto IL_1500;
						IL_14e9:
						num2 = 313;
						mnuOutVAX.Checked = true;
						goto IL_1786;
						IL_1500:
						num2 = 315;
						mnuOutVAX.Checked = false;
						goto IL_1786;
						IL_14a9:
						num2 = 310;
						TxtPW.Text = text8;
						goto IL_1786;
						IL_1457:
						num2 = 304;
						if (Operators.CompareString(text8, "//", TextCompare: false) == 0)
						{
							goto IL_1473;
						}
						goto IL_148e;
						IL_1473:
						num2 = 305;
						TxtUsr.Text = "IWA";
						goto IL_1786;
						IL_148e:
						num2 = 307;
						TxtUsr.Text = text8;
						goto IL_1786;
						IL_141f:
						num2 = 300;
						if (Operators.CompareString(text8, ".\\", TextCompare: false) != 0)
						{
							goto IL_143b;
						}
						goto IL_1786;
						IL_143b:
						num2 = 301;
						CmbNodeO.Text = text8;
						goto IL_1786;
						IL_13cc:
						num2 = 296;
						if ((Operators.CompareString(Strings.UCase(text8), "TEXT", TextCompare: false) == 0) | LikeOperator.LikeString(Strings.UCase(text8), "SQLITE*", CompareMethod.Binary))
						{
							goto IL_1400;
						}
						goto IL_1786;
						IL_1400:
						num2 = 297;
						CmbNodeO.Text = "TEXT";
						goto IL_1786;
						IL_172c:
						num2 = 362;
						if (num12 == 2)
						{
							goto IL_173d;
						}
						goto IL_1786;
						IL_173d:
						num2 = 363;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_1759;
						}
						goto IL_176e;
						IL_1759:
						num2 = 364;
						text += text7;
						goto IL_1786;
						IL_176e:
						num2 = 366;
						text = text + Globals_Renamed.CRLF + text7;
						goto IL_1786;
						IL_1786:
						num2 = 369;
						num13 = (short)unchecked(num13 + 1);
						goto IL_1793;
						IL_01e2:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_01eb;
						IL_01eb:
						num2 = 51;
						text5 = Strings.Trim(CmbNodeO.Text);
						goto IL_0200;
						IL_0200:
						num2 = 52;
						if (Operators.CompareString(cmbSQLVA.Text, "SQLite", TextCompare: false) == 0)
						{
							goto IL_0222;
						}
						goto IL_023c;
						IL_0222:
						num2 = 53;
						d_node = "/NODE=.\\";
						goto IL_022c;
						IL_022c:
						num2 = 54;
						text15 = "/OLEDB=SQLite";
						goto IL_073f;
						IL_023c:
						num2 = 56;
						if (Operators.CompareString(text5, "TEXT", TextCompare: false) == 0)
						{
							goto IL_0255;
						}
						goto IL_0277;
						IL_0255:
						num2 = 57;
						d_node = "/NODE=.\\";
						goto IL_025f;
						IL_025f:
						num2 = 58;
						text15 = "/OLEDB=" + text5;
						goto IL_073f;
						IL_0277:
						num2 = 60;
						text19 = General_Procedures.GetNodeKind(text5);
						goto IL_0288;
						IL_0288:
						num2 = 61;
						if (Operators.CompareString(text19, "", TextCompare: false) != 0)
						{
							goto IL_02a1;
						}
						goto IL_02ac;
						IL_02a1:
						num2 = 62;
						text4 = text5;
						goto IL_02c4;
						IL_02ac:
						num2 = 64;
						text4 = General_Procedures.NodeCheck_s(text5, "S", 0);
						goto IL_02c4;
						IL_02c4:
						num2 = 66;
						text20 = General_Procedures.TestNodeKind(text4, text19);
						goto IL_02d2;
						IL_02d2:
						num2 = 67;
						if (Operators.CompareString(text20, "OLAP", TextCompare: false) == 0)
						{
							goto IL_02eb;
						}
						goto IL_030c;
						IL_02eb:
						num2 = 68;
						text15 = "/OLEDB=MSOLAP";
						goto IL_02f5;
						IL_02f5:
						num2 = 69;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_030c:
						num2 = 71;
						if (Operators.CompareString(text20, "IBI-DAAS", TextCompare: false) == 0)
						{
							goto IL_0325;
						}
						goto IL_0346;
						IL_0325:
						num2 = 72;
						text15 = "/OLEDB=IBI-DaaS";
						goto IL_032f;
						IL_032f:
						num2 = 73;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_0346:
						num2 = 75;
						if (Operators.CompareString(text20, "MONGO", TextCompare: false) == 0)
						{
							goto IL_035f;
						}
						goto IL_0380;
						IL_035f:
						num2 = 76;
						text15 = "/OLEDB=Mongo";
						goto IL_0369;
						IL_0369:
						num2 = 77;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_0380:
						num2 = 79;
						if (((Strings.InStr(Strings.UCase(text4), "TERADATA") != 0) | (Strings.InStr(Strings.UCase(text4), "MIDAS") != 0)) || Operators.CompareString(text19, "TERADATA", TextCompare: false) == 0 || Operators.CompareString(text19, "TERADATA-MIDAS", TextCompare: false) == 0)
						{
							goto IL_03d8;
						}
						goto IL_03f9;
						IL_0e13:
						num2 = 251;
						if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
						{
							goto IL_0e2e;
						}
						goto IL_0e67;
						IL_0e2e:
						num2 = 252;
						text = text + Globals_Renamed.CRLF + sQuery;
						goto IL_0e67;
						IL_03f9:
						num2 = 83;
						if (Operators.CompareString(text20, "MYSQL", TextCompare: false) == 0)
						{
							goto IL_0412;
						}
						goto IL_0433;
						IL_0412:
						num2 = 84;
						text15 = "/OLEDB=MYSQL";
						goto IL_041c;
						IL_041c:
						num2 = 85;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_0433:
						num2 = 87;
						if (Operators.CompareString(text20, "IMBIGDATA", TextCompare: false) == 0)
						{
							goto IL_044c;
						}
						goto IL_046d;
						IL_044c:
						num2 = 88;
						text15 = "/OLEDB=PYSCRIPTDRIVER";
						goto IL_0456;
						IL_0456:
						num2 = 89;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_046d:
						num2 = 91;
						if (Operators.CompareString(text20, "HADOOP-IMPALA", TextCompare: false) == 0)
						{
							goto IL_0486;
						}
						goto IL_04b7;
						IL_0486:
						num2 = 92;
						text15 = "/OLEDB=HADOOPIMPALAODBC";
						goto IL_0490;
						IL_0490:
						num2 = 93;
						d_node = "/NODE=" + BuildSQL.StripODBC(text4, "H");
						goto IL_04ab;
						IL_04ab:
						num2 = 94;
						num10 = 2;
						goto IL_073f;
						IL_04b7:
						num2 = 96;
						if (Operators.CompareString(text20, "SAPHANA", TextCompare: false) == 0)
						{
							goto IL_04d0;
						}
						goto IL_04fb;
						IL_04d0:
						num2 = 97;
						text15 = "/OLEDB=SAPHANAODBC";
						goto IL_04da;
						IL_04da:
						num2 = 98;
						d_node = "/NODE=" + BuildSQL.StripODBC(text4, "S");
						goto IL_073f;
						IL_04fb:
						num2 = 100;
						if (Operators.CompareString(text20, "DENODO", TextCompare: false) == 0)
						{
							goto IL_0514;
						}
						goto IL_053f;
						IL_0514:
						num2 = 101;
						text15 = "/OLEDB=DENODOODBC";
						goto IL_051e;
						IL_051e:
						num2 = 102;
						d_node = "/NODE=" + BuildSQL.StripODBC(text4, "D");
						goto IL_073f;
						IL_053f:
						num2 = 104;
						if (Operators.CompareString(text20, "SNOWFLAKE", TextCompare: false) == 0)
						{
							goto IL_0558;
						}
						goto IL_0583;
						IL_0558:
						num2 = 105;
						text15 = "/OLEDB=SNOWFLAKEODBC";
						goto IL_0562;
						IL_0562:
						num2 = 106;
						d_node = "/NODE=" + BuildSQL.StripODBC(text4, "N");
						goto IL_073f;
						IL_0583:
						num2 = 108;
						if (Operators.CompareString(text20, "HADOOP", TextCompare: false) == 0)
						{
							goto IL_059c;
						}
						goto IL_05c3;
						IL_059c:
						num2 = 109;
						text15 = "/OLEDB=Hadoop";
						goto IL_05a6;
						IL_05a6:
						num2 = 110;
						d_node = "/NODE=" + text4;
						goto IL_05b7;
						IL_05b7:
						num2 = 111;
						num10 = 1;
						goto IL_073f;
						IL_05c3:
						num2 = 113;
						if (Operators.CompareString(text20, "POSTGRES", TextCompare: false) == 0 || Operators.CompareString(text20, "POSTGRES-UBER", TextCompare: false) == 0)
						{
							goto IL_05ee;
						}
						goto IL_060f;
						IL_0e4a:
						num2 = 255;
						text3 = "";
						goto IL_0e57;
						IL_060f:
						num2 = 117;
						if (Operators.CompareString(text20, "SQLSERVER", TextCompare: false) == 0)
						{
							goto IL_062b;
						}
						goto IL_071c;
						IL_062b:
						num2 = 118;
						num9 = 1;
						goto IL_0631;
						IL_0631:
						num2 = 119;
						text15 = "/OLEDB=SQLSERVER";
						goto IL_063b;
						IL_063b:
						num2 = 120;
						num7 = (short)Strings.InStr(Strings.UCase(text4), "@SQL7@UN&");
						goto IL_06f3;
						IL_06f3:
						num2 = 122;
						if (num7 != 0)
						{
							goto IL_0658;
						}
						goto IL_0704;
						IL_0704:
						num2 = 132;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_0658:
						num2 = 123;
						num6 = (short)Strings.InStr(Strings.UCase(text4), "&@PW&");
						goto IL_0670;
						IL_0670:
						num2 = 124;
						if (num6 == 0)
						{
							goto IL_067e;
						}
						goto IL_0693;
						IL_067e:
						num2 = 125;
						text4 = Strings.Mid(text4, 1, num7 - 1);
						goto IL_06d7;
						IL_0693:
						num2 = 127;
						num8 = (short)Strings.InStr(Strings.Mid(text4, num6 + 5), "&");
						goto IL_06af;
						IL_06af:
						num2 = 128;
						text4 = Strings.Mid(text4, 1, num7 - 1) + Strings.Mid(text4, num6 + 5 + num8);
						goto IL_06d7;
						IL_06d7:
						num2 = 130;
						num7 = (short)Strings.InStr(Strings.UCase(text4), "@SQL7@UN&");
						goto IL_06f3;
						IL_071c:
						num2 = 134;
						text15 = "/OLEDB=SQLPlus";
						goto IL_0729;
						IL_0729:
						num2 = 135;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_05ee:
						num2 = 114;
						text15 = "/OLEDB=Postgres";
						goto IL_05f8;
						IL_05f8:
						num2 = 115;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_03d8:
						num2 = 80;
						text15 = "/OLEDB=TERADATA";
						goto IL_03e2;
						IL_03e2:
						num2 = 81;
						d_node = "/NODE=" + text4;
						goto IL_073f;
						IL_073f:
						num2 = 138;
						if ((LikeOperator.LikeString(CmbNodeO.Text, "TEXT*", CompareMethod.Binary) & (Operators.CompareString(cmbSQLVA.Text, "SQLite", TextCompare: false) != 0)) || num9 == 1 || num10 == 2)
						{
							goto IL_078a;
						}
						goto IL_079d;
						IL_0e57:
						num2 = 256;
						text = "";
						goto IL_0e67;
						IL_0e67:
						num2 = 259;
						if (unchecked(Operators.CompareString(MyMode, "O", TextCompare: false) == 0 && num12 == 1))
						{
							goto IL_0e88;
						}
						goto IL_0e96;
						IL_079d:
						num2 = 141;
						if (Operators.CompareString(text20, "UBER", TextCompare: false) == 0 || Strings.InStr(text20, "UBER") != -1)
						{
							goto IL_07ce;
						}
						goto IL_07de;
						IL_0e88:
						num2 = 260;
						text3 = "";
						goto IL_0e96;
						IL_07de:
						num2 = 144;
						if (LikeOperator.LikeString(cmbSQLVA.Text, "SQLite*", CompareMethod.Binary))
						{
							goto IL_0800;
						}
						goto IL_0816;
						IL_0800:
						num2 = 145;
						text5 = cmbSQLVA.Text;
						goto IL_0852;
						IL_0816:
						num2 = 147;
						if (LikeOperator.LikeString(cmbSQLVA.Text, "SQL*", CompareMethod.Binary))
						{
							goto IL_0838;
						}
						goto IL_0852;
						IL_0838:
						num2 = 148;
						text5 = Strings.Mid(cmbSQLVA.Text, 6);
						goto IL_0852;
						IL_07ce:
						num2 = 142;
						text5 = "UBER-NET";
						goto IL_0852;
						IL_078a:
						num2 = 139;
						text5 = "VA";
						goto IL_0852;
						IL_0852:
						num2 = 150;
						d_Engine = "/ENGINE=" + Strings.Trim(text5);
						goto IL_086b;
						IL_086b:
						num2 = 151;
						if (Operators.CompareString(text15, "/OLEDB=SQLSERVER", TextCompare: false) == 0)
						{
							goto IL_0887;
						}
						goto IL_0897;
						IL_0887:
						num2 = 152;
						d_Engine = "/ENGINE=VA (.NET)";
						goto IL_08c1;
						IL_0897:
						num2 = 154;
						if (Operators.CompareString(text15, "/OLEDB=TERADATA", TextCompare: false) == 0)
						{
							goto IL_08b3;
						}
						goto IL_08c1;
						IL_08b3:
						num2 = 155;
						d_Engine = BuildForm.GetMidasEngine();
						goto IL_08c1;
						IL_08c1:
						num2 = 157;
						if (ToRun == 1)
						{
							goto IL_08d1;
						}
						goto IL_08e2;
						IL_08d1:
						num2 = 158;
						text5 = Globals_Renamed.MyPCDir;
						goto IL_08f0;
						IL_08e2:
						num2 = 160;
						text5 = ".\\";
						goto IL_08f0;
						IL_08f0:
						num2 = 162;
						d_WorkDir = "/WORKDIR=" + text5;
						goto IL_0904;
						IL_0904:
						num2 = 163;
						if (Operators.CompareString(Strings.UCase(Strings.Trim(TxtUsr.Text)), "IWA", TextCompare: false) == 0)
						{
							goto IL_0933;
						}
						goto IL_0943;
						IL_0933:
						num2 = 164;
						text5 = "//";
						goto IL_0992;
						IL_0943:
						num2 = 166;
						if (Operators.CompareString(Strings.Trim(TxtUsr.Text), "", TextCompare: false) != 0)
						{
							goto IL_096d;
						}
						goto IL_0984;
						IL_096d:
						num2 = 167;
						text5 = TxtUsr.Text;
						goto IL_0992;
						IL_0984:
						num2 = 169;
						text5 = "";
						goto IL_0992;
						IL_0992:
						num2 = 171;
						d_UN = "/UN=" + text5;
						goto IL_09a6;
						IL_09a6:
						num2 = 172;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							goto IL_09c2;
						}
						goto IL_09d2;
						IL_09c2:
						num2 = 173;
						text4 = "";
						goto IL_0a18;
						IL_09d2:
						num2 = 175;
						if (Operators.CompareString(text5, "//", TextCompare: false) == 0)
						{
							goto IL_09ee;
						}
						goto IL_09ff;
						IL_09ee:
						num2 = 176;
						text4 = "";
						goto IL_0a18;
						IL_09ff:
						num2 = 178;
						text4 = Strings.Trim(TxtPW.Text);
						goto IL_0a18;
						IL_0a18:
						num2 = 180;
						d_PW = "/PW=" + text4;
						goto IL_0a2c;
						IL_0a2c:
						num2 = 181;
						if (mnuOutVAX.Checked)
						{
							goto IL_0a43;
						}
						goto IL_0a54;
						IL_0a43:
						num2 = 182;
						text5 = "Yes";
						goto IL_0a62;
						IL_0a54:
						num2 = 184;
						text5 = "";
						goto IL_0a62;
						IL_0a62:
						num2 = 186;
						d_T = "/T=" + text5;
						goto IL_0a76;
						IL_0a76:
						num2 = 187;
						if (unchecked(num11 != -1 && num11 != -99))
						{
							goto IL_0a94;
						}
						goto IL_0aaf;
						IL_0a94:
						num2 = 188;
						d_ROWS = "/ROWS=" + Conversions.ToString(num11);
						goto IL_0aaf;
						IL_0aaf:
						num2 = 190;
						d_CSV = "/CSV=" + BuildForm.Get_CSVTXT_Opts(ll_InFile, ToRun, MyMode);
						goto IL_0aca;
						IL_0aca:
						num2 = 191;
						if (unchecked(QOpt == 1 && ToRun == 1))
						{
							goto IL_0ae0;
						}
						goto IL_0af0;
						IL_0ae0:
						num2 = 192;
						d_EXP = "/EXPLAIN=Y";
						goto IL_0b3a;
						IL_0af0:
						num2 = 194;
						if (unchecked(QOpt == 2 && ToRun == 1))
						{
							goto IL_0b06;
						}
						goto IL_0b16;
						IL_0b06:
						num2 = 195;
						d_EXP = "/EXPLAIN=D";
						goto IL_0b3a;
						IL_0b16:
						num2 = 197;
						if (unchecked(QOpt == 3 && ToRun == 1))
						{
							goto IL_0b2c;
						}
						goto IL_0b3a;
						IL_0b2c:
						num2 = 198;
						d_EXP = "/EXPLAIN=E";
						goto IL_0b3a;
						IL_0b3a:
						num2 = 200;
						if (ToRun == 1)
						{
							goto IL_0b4d;
						}
						goto IL_0c33;
						IL_0b4d:
						num2 = 201;
						if (QOpt == 2)
						{
							goto IL_0b5e;
						}
						goto IL_0bde;
						IL_0b5e:
						num2 = 202;
						if (num9 == 1)
						{
							goto IL_0b6f;
						}
						goto IL_0b84;
						IL_0b6f:
						num2 = 203;
						text3 = BuildSQL.Write_VA_Lib("va_lib_17.lib");
						goto IL_0c47;
						IL_0b84:
						num2 = 205;
						if (num10 == 1)
						{
							goto IL_0b95;
						}
						goto IL_0ba5;
						IL_0b95:
						num2 = 206;
						text3 = "DESCRIBE &PROMPT&(DATA^Table_Name^=^N^Table Name^SHOW TABLES;@OTHERNODE@^)&PROMPT&";
						goto IL_0c47;
						IL_0ba5:
						num2 = 208;
						if (num10 == 2)
						{
							goto IL_0bb6;
						}
						goto IL_0bc7;
						IL_0bb6:
						num2 = 209;
						text3 = "Describe &PROMPT&(DATA^Table_Name^=^N^Table Name^^)&PROMPT&";
						goto IL_0c47;
						IL_0bc7:
						num2 = 211;
						text3 = BuildSQL.Write_VA_Lib("va_lib_16.lib");
						goto IL_0c47;
						IL_0bde:
						num2 = 214;
						if (Operators.CompareString(Text1.SelectedText, "", TextCompare: false) == 0)
						{
							goto IL_0c03;
						}
						goto IL_0c1a;
						IL_0c03:
						num2 = 215;
						text3 = Text1.Text;
						goto IL_0c47;
						IL_0c1a:
						num2 = 217;
						text3 = Text1.SelectedText;
						goto IL_0c47;
						IL_0c33:
						num2 = 221;
						text3 = Text1.Text;
						goto IL_0c47;
						IL_0c47:
						num2 = 223;
						text = "";
						goto IL_0c54;
						IL_0c54:
						num2 = 224;
						num12 = 0;
						goto IL_0c5d;
						IL_0c5d:
						num2 = 225;
						text3 = Strings.Trim(text3);
						goto IL_0e96;
						IL_0e96:
						num2 = 227;
						if (Operators.CompareString(text3, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0c71;
						IL_0c71:
						num2 = 228;
						num7 = (short)Strings.InStr(text3, "<---- New Query ---->");
						goto IL_0c87;
						IL_0c87:
						num2 = 229;
						if (num7 != 0)
						{
							goto IL_0c98;
						}
						goto IL_0cce;
						IL_0c98:
						num2 = 230;
						sQuery = Strings.Trim(Strings.Mid(text3, 1, num7 - 1));
						goto IL_0cb1;
						IL_0cb1:
						num2 = 231;
						text3 = Strings.Trim(Strings.Mid(text3, num7 + 21));
						goto IL_0ceb;
						IL_0cce:
						num2 = 233;
						sQuery = Strings.Trim(text3);
						goto IL_0cdd;
						IL_0cdd:
						num2 = 234;
						text3 = "";
						goto IL_0ceb;
						IL_0ceb:
						num2 = 236;
						text5 = BuildForm.HG_Strip(sQuery);
						goto IL_0cfa;
						IL_0cfa:
						num2 = 237;
						if ((Operators.CompareString(sQuery, "", TextCompare: false) != 0) & (Operators.CompareString(text5, "", TextCompare: false) != 0))
						{
							goto IL_0d2a;
						}
						goto IL_0e67;
						IL_0d2a:
						num2 = 238;
						num12++;
						goto IL_0d37;
						IL_0d37:
						num2 = 239;
						sOpt = "";
						goto IL_0d44;
						IL_0d44:
						num2 = 240;
						num5 = BuildForm.Get_CTab_Opts(ref sOpt, ref sQuery, num12, d_node, text15, d_Engine, d_WorkDir, d_UN, d_PW, d_T, d_ROWS, d_CSV, d_EXP, d_LastO, ToRun, MyMode);
						goto IL_0d6f;
						IL_0d6f:
						num2 = 241;
						if (num5 == 0)
						{
							goto IL_0d83;
						}
						goto IL_0e4a;
						IL_0d83:
						num2 = 242;
						if (Operators.CompareString(Strings.Trim(text), "", TextCompare: false) == 0)
						{
							goto IL_0da4;
						}
						goto IL_0db2;
						IL_0da4:
						num2 = 243;
						text = sOpt;
						goto IL_0e13;
						end_IL_0001_2:
						break;
					}
					num2 = 414;
					result = text2 + text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 8806;
				continue;
			}
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

	public void SetMenuOpt()
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
				case 603:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0024;
						case 5:
							goto IL_002c;
						case 7:
							goto IL_003e;
						case 8:
							goto IL_0050;
						case 9:
							goto IL_0058;
						case 11:
							goto IL_006c;
						case 12:
							goto IL_008a;
						case 6:
						case 10:
						case 13:
						case 14:
							goto IL_009b;
						case 15:
							goto IL_00ba;
						case 16:
							goto IL_00d7;
						case 17:
							goto IL_00e0;
						case 19:
							goto IL_00f4;
						case 20:
							goto IL_0112;
						case 18:
						case 21:
						case 22:
							goto IL_0123;
						case 23:
							goto IL_0142;
						case 24:
							goto IL_015f;
						case 25:
							goto IL_0168;
						case 27:
							goto IL_017c;
						case 28:
							goto IL_019a;
						case 26:
						case 29:
						case 30:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 31:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0168:
					num2 = 25;
					mnuMacroFile.Checked = false;
					break;
					IL_017c:
					num2 = 27;
					text = " (" + Strings.Trim(MacroFile) + ")";
					goto IL_019a;
					IL_015f:
					num2 = 24;
					text = " (No)";
					goto IL_0168;
					IL_019a:
					num2 = 28;
					mnuMacroFile.Checked = true;
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (out_row == -1)
					{
						goto IL_0024;
					}
					goto IL_003e;
					IL_0024:
					num2 = 4;
					text = " (No)";
					goto IL_002c;
					IL_002c:
					num2 = 5;
					mnuRow2.Checked = false;
					goto IL_009b;
					IL_003e:
					num2 = 7;
					if (out_row == -99)
					{
						goto IL_0050;
					}
					goto IL_006c;
					IL_0050:
					num2 = 8;
					text = " (Prompt)";
					goto IL_0058;
					IL_0058:
					num2 = 9;
					mnuRow2.Checked = true;
					goto IL_009b;
					IL_006c:
					num2 = 11;
					text = " (" + Conversions.ToString(out_row) + ")";
					goto IL_008a;
					IL_008a:
					num2 = 12;
					mnuRow2.Checked = true;
					goto IL_009b;
					IL_009b:
					num2 = 14;
					mnuRow2.Text = "Set Row &Limit" + text + "...";
					goto IL_00ba;
					IL_00ba:
					num2 = 15;
					if (Operators.CompareString(out_odbc, "", TextCompare: false) == 0)
					{
						goto IL_00d7;
					}
					goto IL_00f4;
					IL_00d7:
					num2 = 16;
					text = " (No)";
					goto IL_00e0;
					IL_00e0:
					num2 = 17;
					mnuOutExcel2.Checked = false;
					goto IL_0123;
					IL_00f4:
					num2 = 19;
					text = " (" + Strings.Trim(out_odbc) + ")";
					goto IL_0112;
					IL_0112:
					num2 = 20;
					mnuOutExcel2.Checked = true;
					goto IL_0123;
					IL_0123:
					num2 = 22;
					mnuOutExcel2.Text = "Output To E&XCEL or JMP" + text + "...";
					goto IL_0142;
					IL_0142:
					num2 = 23;
					if (Operators.CompareString(MacroFile, "", TextCompare: false) == 0)
					{
						goto IL_015f;
					}
					goto IL_017c;
					end_IL_0001_2:
					break;
				}
				num2 = 30;
				mnuMacroFile.Text = "Temporary Macro File" + text + "...";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 603;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public short OpenSQLFile(string QName)
	{
		int try0001_dispatch = -1;
		string text = default(string);
		int num2 = default(int);
		short result;
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string MyData;
				switch (try0001_dispatch)
				{
				default:
				{
					text = "";
					short num3 = 0;
					MyData = "";
					short num4 = 0;
					string text2 = "";
					string text3 = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					result = 0;
					if (Operators.CompareString(QName, "", TextCompare: false) != 0)
					{
						text = QName;
						goto IL_025f;
					}
					text2 = (LikeOperator.LikeString(cmbSQLVA.Text, "SQL*", CompareMethod.Binary) ? "spfsql" : ((Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) == 0) ? "jsl" : ((Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) == 0) ? "r" : ((Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) == 0) ? "acs" : ((((Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0) & (Operators.CompareString(mnuOutWF.Text, "TT", TextCompare: false) == 0)) || Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0) ? "txt" : ((Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) != 0) ? "va" : "csv+"))))));
					BuildForm.FileOpenSave("O", "", text2, "", Globals_Renamed.gQueryDir);
					if (!((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(MyProject.Forms.FrmMain.CMDialog1Open.FileName), "", TextCompare: false) == 0)))
					{
						text = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						if (Operators.CompareString(Strings.Trim(Text1.Text), "", TextCompare: false) == 0)
						{
							goto IL_025f;
						}
						num3 = checked((short)Interaction.MsgBox("You currently have an active query. Loading the new query will clear your current one. Do you wish to continue loading the new query?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Load a New Query"));
						if (num3 != 7)
						{
							goto IL_025f;
						}
					}
					goto end_IL_0001;
				}
				case 1009:
					{
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Error opening SQL file " + text + ". " + Conversion.ErrorToString() + ".", MsgBoxStyle.Exclamation, "Error Opening Query File");
							result = 1;
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					IL_025f:
					if (Operators.CompareString(cmbSQLVA.Text, "VA", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0)
					{
						string text3 = General_Procedures.GetFileContents(text, ref MyData);
						if (Operators.CompareString(text3, "", TextCompare: false) == 0)
						{
							Text1.Text = MyData;
							goto end_IL_0001_2;
						}
						Interaction.MsgBox("Error opening " + text + ". (" + text3 + ")", MsgBoxStyle.Exclamation, "Error Opening Query File");
						result = 1;
						Information.Err().Clear();
					}
					else
					{
						Text1.Text = Save_Load_Query_File("L", text, 0, 0);
						Set_Query_In_Title(text);
						SetMenuOpt();
					}
					goto end_IL_0001;
				}
				goto IL_0427;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1009;
				continue;
			}
			break;
			IL_0427:
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

	public void Set_Query_In_Title(string FileName)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 260:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0022;
						case 4:
							goto IL_002b;
						case 5:
							goto IL_0038;
						case 7:
							goto IL_0045;
						case 8:
							goto IL_0056;
						case 9:
							goto IL_0062;
						case 11:
							goto IL_006c;
						case 10:
						case 12:
						case 13:
							goto IL_007b;
						case 14:
							goto IL_0085;
						case 6:
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
					IL_006c:
					num2 = 11;
					text = Strings.Mid(FileName, checked(num5 + 1));
					goto IL_007b;
					IL_007b:
					num2 = 13;
					fTitle = FileName;
					goto IL_0085;
					IL_0062:
					num2 = 9;
					text = FileName;
					goto IL_007b;
					IL_0085:
					num2 = 14;
					fSaveAs = false;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(FileName, "", TextCompare: false) == 0)
					{
						goto IL_0022;
					}
					goto IL_0045;
					IL_0022:
					num2 = 3;
					text = "Untitled";
					goto IL_002b;
					IL_002b:
					num2 = 4;
					fTitle = "SQLPathFinder";
					goto IL_0038;
					IL_0038:
					num2 = 5;
					fSaveAs = true;
					break;
					IL_0045:
					num2 = 7;
					num5 = checked((short)Strings.InStrRev(FileName, "\\"));
					goto IL_0056;
					IL_0056:
					num2 = 8;
					if (num5 == 0)
					{
						goto IL_0062;
					}
					goto IL_006c;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				Text = "Editor - [" + text + "]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 260;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public bool Chk_Save_SQL()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
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
							goto IL_0010;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_0022;
						case 6:
							goto IL_003a;
						case 7:
							goto IL_0047;
						case 8:
							goto IL_0051;
						case 9:
							goto IL_005a;
						case 11:
							goto IL_0062;
						case 12:
							goto IL_0070;
						case 14:
							goto IL_0079;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 13:
						case 15:
						case 16:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0062:
					num2 = 11;
					if (num5 == 7)
					{
						goto IL_0070;
					}
					goto IL_0079;
					IL_0070:
					num2 = 12;
					result = true;
					goto end_IL_0001_3;
					IL_005a:
					num2 = 9;
					result = true;
					goto end_IL_0001_3;
					IL_0079:
					num2 = 14;
					result = false;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					result = false;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					if (!fSaveSQL)
					{
						break;
					}
					goto IL_0022;
					IL_0022:
					num2 = 5;
					num5 = (int)Interaction.MsgBox("Do you wish to save your script?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Save Script?");
					goto IL_003a;
					IL_003a:
					num2 = 6;
					if (num5 == 6)
					{
						goto IL_0047;
					}
					goto IL_0062;
					IL_0047:
					num2 = 7;
					DoSave(0);
					goto IL_0051;
					IL_0051:
					num2 = 8;
					fSaveSQL = false;
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 234;
				continue;
			}
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

	private void FindIt2()
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
					errsource = "frmDTREdit - FindIt";
					int num3 = 0;
					string text = "";
					num3 = (ll_First ? ((ll_FindCase == 0) ? Text1.Text.IndexOf(ll_FindStr, StringComparison.OrdinalIgnoreCase) : Text1.Text.IndexOf(ll_FindStr, StringComparison.Ordinal)) : checked((ll_FindCase == 0) ? Text1.Text.IndexOf(ll_FindStr, Text1.SelectionStart + 1, StringComparison.OrdinalIgnoreCase) : Text1.Text.IndexOf(ll_FindStr, Text1.SelectionStart + 1, StringComparison.Ordinal)));
					if (num3 == -1)
					{
						text = "Cannot find \"" + ll_FindStr + "\"";
						Interaction.MsgBox(text, MsgBoxStyle.OkOnly, MyProject.Application.Info.Title);
						goto end_IL_0001;
					}
					Text1.Select(num3, ll_FindStr.Length);
					ll_First = false;
					mnuFindNext.Enabled = true;
					Text1.ScrollToCaret();
					goto end_IL_0001;
				}
				case 365:
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
				try0001_dispatch = 365;
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

	private void cmbSQLVA_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
		bool visible = default(bool);
		while (true)
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
				case 858:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_00b0;
						case 7:
							goto IL_00b8;
						case 6:
						case 8:
						case 9:
							goto IL_00bd;
						case 10:
							goto IL_00cd;
						case 11:
							goto IL_00dd;
						case 12:
							goto IL_00ed;
						case 13:
							goto IL_00fd;
						case 14:
							goto IL_010d;
						case 15:
							goto IL_011d;
						case 16:
							goto IL_012d;
						case 17:
							goto IL_013d;
						case 18:
							goto IL_014d;
						case 19:
							goto IL_0189;
						case 21:
							goto IL_019d;
						case 20:
						case 22:
						case 23:
							goto IL_01ae;
						case 24:
							goto IL_01ca;
						case 25:
							goto IL_01da;
						case 26:
							goto IL_01ea;
						case 27:
							goto IL_01fa;
						case 28:
							goto IL_021c;
						case 29:
						case 30:
							goto IL_0232;
						case 31:
							goto IL_0254;
						case 33:
							goto IL_025e;
						case 32:
						case 34:
						case 35:
							goto IL_0265;
						case 36:
							goto IL_0276;
						case 37:
							goto IL_0287;
						case 38:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 39:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01fa:
					num2 = 27;
					if (Operators.CompareString(cmbSQLVA.Text, "SQLite", TextCompare: false) == 0)
					{
						goto IL_021c;
					}
					goto IL_0232;
					IL_021c:
					num2 = 28;
					CmbNodeO.Text = "TEXT";
					goto IL_0232;
					IL_01ea:
					num2 = 26;
					mnuRunGetError.Visible = flag;
					goto IL_01fa;
					IL_0232:
					num2 = 30;
					if (Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0)
					{
						goto IL_0254;
					}
					goto IL_025e;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					visible = false;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					if (Operators.CompareString(cmbSQLVA.Text, "VA", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0 || Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0)
					{
						goto IL_00b0;
					}
					goto IL_00b8;
					IL_0254:
					num2 = 31;
					visible = false;
					goto IL_0265;
					IL_025e:
					num2 = 33;
					visible = true;
					goto IL_0265;
					IL_0265:
					num2 = 35;
					mnuRunQueryNormally.Visible = visible;
					goto IL_0276;
					IL_0276:
					num2 = 36;
					_Tbr1_Button4.Visible = visible;
					goto IL_0287;
					IL_0287:
					num2 = 37;
					_Tbr1_Button9.Visible = visible;
					break;
					IL_00b8:
					num2 = 7;
					flag = true;
					goto IL_00bd;
					IL_00b0:
					num2 = 5;
					flag = false;
					goto IL_00bd;
					IL_00bd:
					num2 = 9;
					lblNodeO.Visible = flag;
					goto IL_00cd;
					IL_00cd:
					num2 = 10;
					CmbNodeO.Visible = flag;
					goto IL_00dd;
					IL_00dd:
					num2 = 11;
					CmdNodeO.Visible = flag;
					goto IL_00ed;
					IL_00ed:
					num2 = 12;
					mnuQuery.Visible = flag;
					goto IL_00fd;
					IL_00fd:
					num2 = 13;
					mnuUtilities.Visible = flag;
					goto IL_010d;
					IL_010d:
					num2 = 14;
					lblUsr.Visible = flag;
					goto IL_011d;
					IL_011d:
					num2 = 15;
					TxtUsr.Visible = flag;
					goto IL_012d;
					IL_012d:
					num2 = 16;
					lblPW.Visible = flag;
					goto IL_013d;
					IL_013d:
					num2 = 17;
					TxtPW.Visible = flag;
					goto IL_014d;
					IL_014d:
					num2 = 18;
					if ((Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0) & (Operators.CompareString(mnuOutWF.Text, "TT", TextCompare: false) != 0))
					{
						goto IL_0189;
					}
					goto IL_019d;
					IL_0189:
					num2 = 19;
					mnuOut.Visible = true;
					goto IL_01ae;
					IL_019d:
					num2 = 21;
					mnuOut.Visible = false;
					goto IL_01ae;
					IL_01ae:
					num2 = 23;
					Tbr1.Items[10].Enabled = flag;
					goto IL_01ca;
					IL_01ca:
					num2 = 24;
					mnuRunDescribe.Visible = flag;
					goto IL_01da;
					IL_01da:
					num2 = 25;
					mnuRunExplainPlan.Visible = flag;
					goto IL_01ea;
					end_IL_0001_2:
					break;
				}
				num2 = 38;
				_Tbr1_Button10.Visible = visible;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 858;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmDTREdit_Load(object eventSender, EventArgs eventArgs)
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
				string text;
				string text2;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_000a;
				case 627:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_01ef;
						default:
							goto end_IL_0001;
						}
						goto IL_01bd;
					}
					IL_018c:
					num2 = 24;
					Text1.ForeColor = ColorTranslator.FromOle(Information.RGB(0, 0, 255));
					goto IL_01ac;
					IL_01ac:
					num2 = 25;
					Cursor.Current = Cursors.Default;
					goto end_IL_0001_2;
					IL_0174:
					num2 = 23;
					FileSystem.ChDir(MyProject.Application.Info.DirectoryPath);
					goto IL_018c;
					IL_01ef:
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
					case 6:
						goto IL_002d;
					case 7:
						goto IL_0043;
					case 8:
						goto IL_005f;
					case 10:
						goto IL_0084;
					case 11:
						goto IL_00a1;
					case 9:
					case 12:
					case 13:
						goto IL_00b0;
					case 15:
						goto IL_00d7;
					case 14:
					case 16:
					case 17:
						goto IL_00e6;
					case 18:
						goto IL_00f0;
					case 19:
						goto IL_0112;
					case 20:
						goto IL_0126;
					case 21:
						goto IL_0148;
					case 22:
						goto IL_015c;
					case 23:
						goto IL_0174;
					case 24:
						goto IL_018c;
					case 25:
						goto IL_01ac;
					case 27:
						goto IL_01bd;
					case 28:
						goto end_IL_0001_3;
					default:
						goto end_IL_0001;
					case 26:
					case 29:
						goto end_IL_0001_2;
					}
					goto default;
					IL_01bd:
					num2 = 27;
					Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
					break;
					IL_000a:
					num2 = 2;
					text = "";
					goto IL_0012;
					IL_0012:
					num2 = 3;
					text2 = "";
					goto IL_001b;
					IL_001b:
					num2 = 4;
					errsource = "FrmDTREdit - Form_Load";
					goto IL_0024;
					IL_0024:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_002d;
					IL_002d:
					num2 = 6;
					if (BuildForm.FormIsLoaded("FrmSQLQuerya", 1))
					{
						goto IL_0043;
					}
					goto IL_00d7;
					IL_0043:
					num2 = 7;
					if (Operators.CompareString(out_odbc, "", TextCompare: false) == 0)
					{
						goto IL_005f;
					}
					goto IL_0084;
					IL_005f:
					num2 = 8;
					out_odbc = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_odbc;
					goto IL_00b0;
					IL_0084:
					num2 = 10;
					if (Operators.CompareString(out_odbc, "--", TextCompare: false) == 0)
					{
						goto IL_00a1;
					}
					goto IL_00b0;
					IL_00a1:
					num2 = 11;
					out_odbc = "";
					goto IL_00b0;
					IL_00b0:
					num2 = 13;
					MacroFile = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile;
					goto IL_00e6;
					IL_00d7:
					num2 = 15;
					MacroFile = "";
					goto IL_00e6;
					IL_00e6:
					num2 = 17;
					SetMenuOpt();
					goto IL_00f0;
					IL_00f0:
					num2 = 18;
					if (Operators.CompareString(TxtUsr.Text, "", TextCompare: false) == 0)
					{
						goto IL_0112;
					}
					goto IL_0126;
					IL_0112:
					num2 = 19;
					TxtUsr.Text = "IWA";
					goto IL_0126;
					IL_0126:
					num2 = 20;
					if (Operators.CompareString(CmbNodeO.Text, "", TextCompare: false) == 0)
					{
						goto IL_0148;
					}
					goto IL_015c;
					IL_0148:
					num2 = 21;
					CmbNodeO.Text = "DEFAULT (MARS)";
					goto IL_015c;
					IL_015c:
					num2 = 22;
					FileSystem.ChDrive(MyProject.Application.Info.DirectoryPath);
					goto IL_0174;
					end_IL_0001_3:
					break;
				}
				num2 = 28;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 627;
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

	private void FrmDTREdit_FormClosing(object eventSender, FormClosingEventArgs eventArgs)
	{
		bool cancel = eventArgs.Cancel;
		CloseReason closeReason = eventArgs.CloseReason;
		if (!Chk_Save_SQL())
		{
			cancel = true;
			Globals_Renamed.QueryCancel = -1;
		}
		eventArgs.Cancel = cancel;
	}

	private void mnuappend_Click()
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
				SaveFile(1, l_SaveAs: true);
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

	public void MnuClose_Click(object eventSender, EventArgs eventArgs)
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

	public void mnucopy_Click(object eventSender, EventArgs eventArgs)
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
							goto IL_001f;
						case 4:
							goto IL_0031;
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
					if (Text1.SelectionLength <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_001f;
					IL_001f:
					num2 = 3;
					MyProject.Computer.Clipboard.Clear();
					goto IL_0031;
					IL_0031:
					num2 = 4;
					MyProject.Computer.Clipboard.SetText(Text1.SelectedText);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Text1.Focus();
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

	public void mnuCopyOpt_Click(object eventSender, EventArgs eventArgs)
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
							goto IL_0020;
						case 4:
							goto IL_0037;
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
					text = Save_Load_Query_File("O", "", 0, 0);
					goto IL_0020;
					IL_0020:
					num2 = 3;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0037;
					IL_0037:
					num2 = 4;
					Text1.SelectedText = "<---- New Query ---->" + Globals_Renamed.CRLF + text + Globals_Renamed.CRLF;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Text1.Focus();
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

	public void mnucut_Click(object eventSender, EventArgs eventArgs)
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
				case 133:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003a;
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
					MyProject.Computer.Clipboard.Clear();
					goto IL_001d;
					IL_001d:
					num2 = 3;
					MyProject.Computer.Clipboard.SetText(Text1.SelectedText);
					goto IL_003a;
					IL_003a:
					num2 = 4;
					Text1.SelectedText = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Text1.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 133;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuedit_Click(object eventSender, EventArgs eventArgs)
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
					mnuCopy.Enabled = Text1.SelectionLength > 0;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				mnuCut.Enabled = Text1.SelectionLength > 0;
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

	public void MnuFind_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		frmFind frmFind2 = default(frmFind);
		while (true)
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
				case 426:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0032;
						case 4:
							goto IL_003b;
						case 5:
							goto IL_005c;
						case 7:
							goto IL_007a;
						case 6:
						case 8:
						case 9:
							goto IL_0090;
						case 10:
							goto IL_00a1;
						case 11:
							goto IL_00c2;
						case 12:
							goto IL_00cc;
						case 13:
							goto IL_00d7;
						case 14:
							goto IL_00e7;
						case 15:
							goto IL_00f7;
						case 16:
							goto IL_0102;
						case 17:
							goto IL_0124;
						case 18:
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
					IL_00f7:
					num2 = 15;
					frmFind2.Dispose();
					goto IL_0102;
					IL_0102:
					num2 = 16;
					if (Operators.CompareString(Strings.Trim(ll_FindStr), "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0124;
					IL_00e7:
					num2 = 14;
					ll_FindStr = frmFind2.ll_FindStr;
					goto IL_00f7;
					IL_0124:
					num2 = 17;
					FindIt2();
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(Text1.Text), "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0032;
					IL_0032:
					num2 = 3;
					frmFind2 = new frmFind();
					goto IL_003b;
					IL_003b:
					num2 = 4;
					if (Operators.CompareString(Text1.SelectedText, "", TextCompare: false) != 0)
					{
						goto IL_005c;
					}
					goto IL_007a;
					IL_005c:
					num2 = 5;
					frmFind2.Text1.Text = Text1.SelectedText;
					goto IL_0090;
					IL_007a:
					num2 = 7;
					frmFind2.Text1.Text = ll_FindStr;
					goto IL_0090;
					IL_0090:
					num2 = 9;
					frmFind2.Text1.SelectionStart = 0;
					goto IL_00a1;
					IL_00a1:
					num2 = 10;
					frmFind2.Text1.SelectionLength = Strings.Len(frmFind2.Text1.Text);
					goto IL_00c2;
					IL_00c2:
					num2 = 11;
					ll_First = true;
					goto IL_00cc;
					IL_00cc:
					num2 = 12;
					frmFind2.ShowDialog();
					goto IL_00d7;
					IL_00d7:
					num2 = 13;
					ll_FindCase = frmFind2.ll_FindCase;
					goto IL_00e7;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				Text1.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 426;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnufindnext_Click(object eventSender, EventArgs eventArgs)
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
					FindIt2();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Text1.Focus();
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

	public void mnuHelp_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Query+Editor");
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

	public void mnuMacroFile_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
		while (true)
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
							goto IL_0014;
						case 4:
							goto IL_0023;
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
					oVal = MacroFile;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					BuildForm.SetOutputOpt("MACRO", ref oVal);
					goto IL_0023;
					IL_0023:
					num2 = 4;
					MacroFile = oVal;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				SetMenuOpt();
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

	public void mnuNew_Click(object eventSender, EventArgs eventArgs)
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
				case 286:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0032;
						case 4:
							goto IL_0040;
						case 5:
							goto IL_0053;
						case 6:
							goto IL_0060;
						case 7:
							goto IL_006d;
						case 8:
							goto IL_0076;
						case 9:
							goto IL_0083;
						case 10:
							goto IL_0093;
						case 11:
							goto IL_00a2;
						case 12:
							goto IL_00ac;
						case 13:
						case 14:
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0093:
					num2 = 10;
					Set_Query_In_Title("");
					goto IL_00a2;
					IL_00a2:
					num2 = 11;
					SetMenuOpt();
					goto IL_00ac;
					IL_0083:
					num2 = 9;
					mnuOutVAX.Checked = true;
					goto IL_0093;
					IL_00ac:
					num2 = 12;
					fSaveSQL = false;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(Text1.Text), "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0032;
					IL_0032:
					num2 = 3;
					if (!Chk_Save_SQL())
					{
						break;
					}
					goto IL_0040;
					IL_0040:
					num2 = 4;
					Text1.Text = "";
					goto IL_0053;
					IL_0053:
					num2 = 5;
					out_odbc = "";
					goto IL_0060;
					IL_0060:
					num2 = 6;
					MacroFile = "";
					goto IL_006d;
					IL_006d:
					num2 = 7;
					out_row = -1;
					goto IL_0076;
					IL_0076:
					num2 = 8;
					out_inline = "";
					goto IL_0083;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				Text1.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 286;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuOpen_Click(object eventSender, EventArgs eventArgs)
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
				short num5;
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
						case 4:
							goto IL_001e;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!Chk_Save_SQL())
					{
						goto end_IL_0001_3;
					}
					goto IL_001e;
					IL_001e:
					num2 = 4;
					num5 = OpenSQLFile("");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				fSaveSQL = false;
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

	public void mnuOutExcel2_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
		while (true)
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
							goto IL_0014;
						case 4:
							goto IL_0023;
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
					oVal = out_odbc;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					BuildForm.SetOutputOpt("EXCEL", ref oVal);
					goto IL_0023;
					IL_0023:
					num2 = 4;
					out_odbc = oVal;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				SetMenuOpt();
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

	public void mnuOutVax_Click(object eventSender, EventArgs eventArgs)
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
					if (!mnuOutVAX.Checked)
					{
						break;
					}
					goto IL_001c;
					IL_001c:
					num2 = 3;
					mnuOutVAX.Checked = false;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				mnuOutVAX.Checked = true;
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

	public void mnuPaste_Click(object eventSender, EventArgs eventArgs)
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
				case 119:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0020;
						case 4:
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
					if (!MyProject.Computer.Clipboard.ContainsText())
					{
						break;
					}
					goto IL_0020;
					IL_0020:
					num2 = 3;
					Text1.SelectedText = MyProject.Computer.Clipboard.GetText();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Text1.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 119;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuRow2_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
		while (true)
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
							goto IL_0019;
						case 4:
							goto IL_0028;
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
					oVal = Conversions.ToString(out_row);
					goto IL_0019;
					IL_0019:
					num2 = 3;
					BuildForm.SetOutputOpt("LIMIT", ref oVal);
					goto IL_0028;
					IL_0028:
					num2 = 4;
					out_row = checked((int)Math.Round(Conversion.Val(oVal)));
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				SetMenuOpt();
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

	public void DoSave(short Index)
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
				case 657:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0032;
						case 6:
							goto IL_00bc;
						case 7:
							goto IL_00dd;
						case 9:
							goto IL_010c;
						case 10:
							goto IL_0139;
						case 11:
							goto IL_0159;
						case 13:
							goto IL_017a;
						case 14:
							goto IL_018f;
						case 5:
						case 8:
						case 12:
						case 15:
						case 16:
						case 17:
							goto IL_01ac;
						case 18:
							goto IL_01c9;
						case 20:
							goto IL_01db;
						case 21:
							goto IL_01e8;
						case 23:
							goto IL_01f8;
						case 19:
						case 22:
						case 24:
						case 25:
						case 26:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_017a:
					num2 = 13;
					Text1.Text = KeepSQLData;
					goto IL_018f;
					IL_018f:
					num2 = 14;
					KeepSQLData = Save_Load_Query_File("S", "", 0, 0);
					goto IL_01ac;
					IL_0159:
					num2 = 11;
					if (Strings.InStr(KeepSQLData, "<---- New Query ---->") == 0)
					{
						goto IL_017a;
					}
					goto IL_01ac;
					IL_01ac:
					num2 = 17;
					if (Operators.CompareString(KeepSQLData, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_01c9;
					IL_000b:
					num2 = 2;
					if (KeepSQL)
					{
						goto IL_001a;
					}
					goto IL_01db;
					IL_001a:
					num2 = 3;
					KeepSQLData = Strings.Trim(Text1.Text);
					goto IL_0032;
					IL_0032:
					num2 = 4;
					if (Operators.CompareString(cmbSQLVA.Text, "VA", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) != 0)
					{
						goto IL_00bc;
					}
					goto IL_01ac;
					IL_01c9:
					num2 = 18;
					KeepSQLData = "EMPTY";
					break;
					IL_01db:
					num2 = 20;
					if (Index == 0)
					{
						goto IL_01e8;
					}
					goto IL_01f8;
					IL_01e8:
					num2 = 21;
					SaveFile(0, l_SaveAs: false);
					break;
					IL_01f8:
					num2 = 23;
					SaveFile(0, l_SaveAs: true);
					break;
					IL_00bc:
					num2 = 6;
					if (Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0)
					{
						goto IL_00dd;
					}
					goto IL_010c;
					IL_00dd:
					num2 = 7;
					KeepSQLData = General_Procedures.Get_Node_Value(mnuOutWF.Text) + "<SOF>\r\n" + KeepSQLData;
					goto IL_01ac;
					IL_010c:
					num2 = 9;
					if (Operators.CompareString(Strings.Mid(KeepSQLData, 1, Strings.Len("<---- New Query ---->")), "<---- New Query ---->", TextCompare: false) == 0)
					{
						goto IL_0139;
					}
					goto IL_0159;
					IL_0139:
					num2 = 10;
					KeepSQLData = Strings.Mid(KeepSQLData, checked(Strings.Len("<---- New Query ---->") + 1));
					goto IL_0159;
					end_IL_0001_2:
					break;
				}
				num2 = 26;
				fSaveSQL = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 657;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuselectall_Click(object eventSender, EventArgs eventArgs)
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
							goto IL_0024;
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
					if (Text1.Text.Length <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0024;
					IL_0024:
					num2 = 3;
					Text1.SelectionStart = 0;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				Text1.SelectionLength = Text1.Text.Length;
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

	private void SaveFile(short FileMode, bool l_SaveAs)
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
				{
					string text = "";
					string text2 = "";
					string text3 = "";
					string text4 = "";
					short num3 = 0;
					if (Operators.CompareString(Strings.Trim(Text1.Text), "", TextCompare: false) != 0)
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						text3 = (LikeOperator.LikeString(cmbSQLVA.Text, "SQL*", CompareMethod.Binary) ? "spfsql" : ((Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) == 0) ? "jsl" : ((Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) == 0) ? "r" : ((Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) == 0) ? "acs" : (((Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) == 0 && Operators.CompareString(mnuOutWF.Text, "TT", TextCompare: false) == 0) || Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) == 0) ? "txt" : ((Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) != 0) ? "va" : "csv+"))))));
						text4 = ((FileMode != 1) ? "S" : "A");
						if (fSaveAs == Conversions.ToBoolean("1") || l_SaveAs)
						{
							BuildForm.FileOpenSave(text4, fTitle, text3, "", Globals_Renamed.gQueryDir);
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
						}
						else
						{
							text = fTitle;
						}
						ProjectData.ClearProjectError();
						num2 = 3;
						text2 = ((Operators.CompareString(cmbSQLVA.Text, "VA", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "JSL", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "CB_ACS", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "R-SCRIPT", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "WRITE-FILE", TextCompare: false) != 0 && Operators.CompareString(cmbSQLVA.Text, "WRITE", TextCompare: false) != 0 && Strings.InStr(Text1.Text, "<---- New Query ---->") == 0) ? Save_Load_Query_File("S", "", 0, 0) : Text1.Text);
						num3 = General_Procedures.Save_SQL_Query(text2, text, FileMode);
						if (num3 == 1)
						{
							Set_Query_In_Title(text);
						}
					}
					else
					{
						Interaction.MsgBox("There is nothing to save.", MsgBoxStyle.Exclamation, "No Query to Save!");
					}
					break;
				}
				case 892:
					num = -1;
					switch (num2)
					{
					case 3:
						Interaction.MsgBox("Error saving the file. " + Conversion.ErrorToString() + ".", MsgBoxStyle.Critical, "Error Saving File");
						ProjectData.ClearProjectError();
						if (num == 0)
						{
							throw ProjectData.CreateProjectError(-2146828268);
						}
						num = 0;
						break;
					case 2:
						break;
					default:
						goto IL_03b6;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 892;
				continue;
			}
			break;
			IL_03b6:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Tbr1_ButtonClick(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		ToolStripItem toolStripItem = default(ToolStripItem);
		int num = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				short num5;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					toolStripItem = (ToolStripItem)eventSender;
					goto IL_000b;
				case 376:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0023;
						case 6:
							goto IL_005d;
						case 8:
							goto IL_0077;
						case 10:
							goto IL_008e;
						case 12:
							goto IL_009c;
						case 14:
							goto IL_00b4;
						case 15:
							goto IL_00d0;
						case 17:
							goto IL_00e2;
						case 18:
							goto IL_00e8;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 9:
						case 11:
						case 13:
						case 16:
						case 19:
						case 20:
						case 22:
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008e:
					num2 = 10;
					DoSave(0);
					goto end_IL_0001_3;
					IL_0077:
					num2 = 8;
					mnuOpen_Click(mnuOpen, new EventArgs());
					goto end_IL_0001_3;
					IL_009c:
					num2 = 12;
					mnuRunQueryNormally_Click(mnuRunQueryNormally, new EventArgs());
					goto end_IL_0001_3;
					IL_005d:
					num2 = 6;
					mnuNew_Click(mnuNew, new EventArgs());
					goto end_IL_0001_3;
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					Tbr1.Refresh();
					goto IL_0023;
					IL_0023:
					num2 = 4;
					switch (toolStripItem.Owner.Items.IndexOf(toolStripItem))
					{
					case 0:
						break;
					case 1:
						goto IL_0077;
					case 2:
						goto IL_008e;
					case 3:
						goto IL_009c;
					case 4:
						goto IL_00b4;
					case 5:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001_3;
					}
					goto IL_005d;
					IL_00b4:
					num2 = 14;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						goto IL_00d0;
					}
					goto IL_00e2;
					IL_00d0:
					num2 = 15;
					BuildForm.Invoke_SQL_Emulator();
					goto end_IL_0001_3;
					IL_00e2:
					num2 = 17;
					num5 = 0;
					goto IL_00e8;
					IL_00e8:
					num2 = 18;
					num5 = General_Procedures.Activate_Emulator_2();
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				BuildForm.Process_Screen("CANCEL");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 376;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Text1_TextChanged(object eventSender, EventArgs eventArgs)
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
				case 85:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					fSaveSQL = true;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					mnuUndoRedo.Text = "Undo";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				Application.DoEvents();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 85;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuUndoRedo_Click(object sender, EventArgs e)
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
				case 230:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003d;
						case 5:
							goto IL_004b;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_0077;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 7:
						case 11:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004b:
					num2 = 5;
					mnuUndoRedo.Text = "Redo";
					goto end_IL_0001_3;
					IL_0064:
					num2 = 8;
					if (!Text1.CanUndo)
					{
						goto end_IL_0001_3;
					}
					goto IL_0077;
					IL_003d:
					num2 = 4;
					Text1.Undo();
					goto IL_004b;
					IL_0077:
					num2 = 9;
					Text1.Undo();
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(mnuUndoRedo.Text, "Undo", TextCompare: false) == 0)
					{
						goto IL_002a;
					}
					goto IL_0064;
					IL_002a:
					num2 = 3;
					if (!Text1.CanUndo)
					{
						goto end_IL_0001_3;
					}
					goto IL_003d;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				mnuUndoRedo.Text = "Undo";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 230;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
		while (true)
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
				DoSave(0);
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

	private void mnuSaveAs_Click(object sender, EventArgs e)
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
				DoSave(1);
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

	private void mnuExcel_I_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object left = default(object);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 219:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0024;
						case 5:
							goto IL_0038;
						case 7:
							goto IL_0048;
						case 8:
							goto IL_005c;
						case 10:
							goto IL_006c;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 9:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0048:
					num2 = 7;
					if (Operators.ConditionalCompareObjectEqual(left, "J", TextCompare: false))
					{
						goto IL_005c;
					}
					goto IL_006c;
					IL_005c:
					num2 = 8;
					BuildForm.Invoke_JMP(1, "");
					goto end_IL_0001_3;
					IL_0038:
					num2 = 5;
					Test.Invoke_Excel3(1, "");
					goto end_IL_0001_3;
					IL_006c:
					num2 = 10;
					if (!Operators.ConditionalCompareObjectEqual(left, "G", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000c:
					num2 = 2;
					left = NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
					goto IL_0024;
					IL_0024:
					num2 = 4;
					if (Operators.ConditionalCompareObjectEqual(left, "E", TextCompare: false))
					{
						goto IL_0038;
					}
					goto IL_0048;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				BuildForm.Invoke_Txt("spfgrid", 1, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 219;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuExcel_C_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object left = default(object);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 161:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0024;
						case 5:
							goto IL_0038;
						case 7:
							goto IL_0048;
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
					IL_0024:
					num2 = 4;
					if (Operators.ConditionalCompareObjectEqual(left, "E", TextCompare: false))
					{
						goto IL_0038;
					}
					goto IL_0048;
					IL_0038:
					num2 = 5;
					Test.Invoke_Excel3(0, "");
					goto end_IL_0001_3;
					IL_000c:
					num2 = 2;
					left = NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
					goto IL_0024;
					IL_0048:
					num2 = 7;
					if (!Operators.ConditionalCompareObjectEqual(left, "J", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				BuildForm.Invoke_JMP(0, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 161;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuRunQueryNormally_Click(object sender, EventArgs e)
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
				DoRunQuery(0);
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

	private void mnuRunExplainPlan_Click(object sender, EventArgs e)
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
				DoRunQuery(1);
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

	private void mnuRunDescribe_Click(object sender, EventArgs e)
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
				DoRunQuery(2);
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

	private void mnuRunGetError_Click(object sender, EventArgs e)
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
				DoRunQuery(3);
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

	private void CmdNodeO_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmNodeSel frmNodeSel = default(FrmNodeSel);
		string f_OutNode = default(string);
		string f_Pattern = default(string);
		while (true)
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
				case 212:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0022;
						case 5:
							goto IL_002b;
						case 6:
							goto IL_0036;
						case 7:
							goto IL_0044;
						case 8:
							goto IL_004e;
						case 9:
							goto IL_0058;
						case 10:
							goto IL_0063;
						case 11:
							goto IL_006f;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0058:
					num2 = 9;
					frmNodeSel.ShowDialog();
					goto IL_0063;
					IL_0063:
					num2 = 10;
					f_OutNode = frmNodeSel.f_OutNode;
					goto IL_006f;
					IL_004e:
					num2 = 8;
					frmNodeSel.f_Pattern = f_Pattern;
					goto IL_0058;
					IL_006f:
					num2 = 11;
					frmNodeSel.Dispose();
					break;
					IL_000b:
					num2 = 2;
					f_Pattern = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					f_OutNode = CmbNodeO.Text;
					goto IL_0022;
					IL_0022:
					num2 = 4;
					frmNodeSel = new FrmNodeSel();
					goto IL_002b;
					IL_002b:
					num2 = 5;
					frmNodeSel.f_OutNode = f_OutNode;
					goto IL_0036;
					IL_0036:
					num2 = 6;
					frmNodeSel.f_DBType = "5";
					goto IL_0044;
					IL_0044:
					num2 = 7;
					frmNodeSel.f_Mode = 2;
					goto IL_004e;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				CmbNodeO.Text = f_OutNode;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 212;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuOut_Click(object sender, EventArgs e)
	{
	}

	private void mnuOutWF_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string oVal = default(string);
		string right = default(string);
		while (true)
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
							goto IL_001e;
						case 4:
							goto IL_0027;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_0053;
						case 7:
							goto IL_005b;
						case 8:
							goto IL_006a;
						case 9:
							goto IL_0081;
						case 11:
							goto IL_008f;
						case 10:
						case 12:
						case 13:
							goto IL_00aa;
						case 14:
							goto IL_00c5;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008f:
					num2 = 11;
					text = " {" + Strings.Trim(oVal) + "}";
					goto IL_00aa;
					IL_00aa:
					num2 = 13;
					mnuOutWF.Text = "Output File" + text;
					goto IL_00c5;
					IL_0081:
					num2 = 9;
					text = " {MyInput.txt}";
					goto IL_00aa;
					IL_00c5:
					num2 = 14;
					if (Operators.CompareString(oVal, right, TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					oVal = General_Procedures.Get_Node_Value(mnuOutWF.Text);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					text = "";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					right = oVal;
					goto IL_002c;
					IL_002c:
					num2 = 5;
					if ((Operators.CompareString(oVal, "No", TextCompare: false) == 0) | (Operators.CompareString(oVal, "", TextCompare: false) == 0))
					{
						goto IL_0053;
					}
					goto IL_005b;
					IL_0053:
					num2 = 6;
					oVal = "MyInput.txt";
					goto IL_005b;
					IL_005b:
					num2 = 7;
					BuildForm.SetOutputOpt("WF", ref oVal);
					goto IL_006a;
					IL_006a:
					num2 = 8;
					if (Operators.CompareString(oVal, "", TextCompare: false) == 0)
					{
						goto IL_0081;
					}
					goto IL_008f;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				fSaveSQL = true;
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
}

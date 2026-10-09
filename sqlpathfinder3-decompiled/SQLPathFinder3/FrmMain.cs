using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using SQLPathFinder3.My;
using SQLPathFinder3.My.Resources;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmMain : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuNewQuery")]
	private ToolStripMenuItem _MnuNewQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuOpenQuery")]
	private ToolStripMenuItem _MnuOpenQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuSaveQuery")]
	private ToolStripMenuItem _MnuSaveQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveQAs")]
	private ToolStripMenuItem _mnuSaveQAs;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuShowQuery")]
	private ToolStripMenuItem _MnuShowQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuRunQuery")]
	private ToolStripMenuItem _MnuRunQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuexit")]
	private ToolStripMenuItem _mnuexit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuFile0")]
	private ToolStripMenuItem _MnuFile0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuFile1")]
	private ToolStripMenuItem _MnuFile1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuFile2")]
	private ToolStripMenuItem _MnuFile2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuFile3")]
	private ToolStripMenuItem _MnuFile3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuAddTables")]
	private ToolStripMenuItem _MnuAddTables;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuCompute0")]
	private ToolStripMenuItem _MnuCompute0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuOutInline")]
	private ToolStripMenuItem _mnuOutInline;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMacroFile")]
	private ToolStripMenuItem _mnuMacroFile;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuToggleGrid")]
	private ToolStripMenuItem _mnuToggleGrid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuSASOptions")]
	private ToolStripMenuItem _MnuSASOptions;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuConnect")]
	private ToolStripMenuItem _MnuConnect;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuDisConnect")]
	private ToolStripMenuItem _MnuDisConnect;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuSpawnVAX")]
	private ToolStripMenuItem _MnuSpawnVAX;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuConfigure")]
	private ToolStripMenuItem _MnuConfigure;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMultiQuery")]
	private ToolStripMenuItem _mnuMultiQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuExcel")]
	private ToolStripMenuItem _MnuExcel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuCloseExcel")]
	private ToolStripMenuItem _MnuCloseExcel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuJMP")]
	private ToolStripMenuItem _mnuJMP;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCloseJMP")]
	private ToolStripMenuItem _mnuCloseJMP;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuTxt")]
	private ToolStripMenuItem _mnuTxt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("MnuHelPAbout")]
	private ToolStripMenuItem _MnuHelPAbout;

	public OpenFileDialog CMDialog1Open;

	public SaveFileDialog CMDialog1Save;

	public FontDialog CMDialog1Font;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button1")]
	private ToolStripButton __cmdIcon_Button1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button2")]
	private ToolStripButton __cmdIcon_Button2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button4")]
	private ToolStripButton __cmdIcon_Button4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button5")]
	private ToolStripButton __cmdIcon_Button5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button6")]
	private ToolStripButton __cmdIcon_Button6;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button7")]
	private ToolStripButton __cmdIcon_Button7;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button8")]
	private ToolStripButton __cmdIcon_Button8;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button9")]
	private ToolStripButton __cmdIcon_Button9;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button11")]
	private ToolStripButton __cmdIcon_Button11;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button12")]
	private ToolStripButton __cmdIcon_Button12;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button13")]
	private ToolStripButton __cmdIcon_Button13;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button14")]
	private ToolStripButton __cmdIcon_Button14;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button16")]
	private ToolStripButton __cmdIcon_Button16;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button17")]
	private ToolStripButton __cmdIcon_Button17;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button18")]
	private ToolStripButton __cmdIcon_Button18;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button10")]
	private ToolStripButton __cmdIcon_Button10;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("ClearScreenToolStripMenuItem")]
	private ToolStripMenuItem _ClearScreenToolStripMenuItem;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuBatch")]
	private ToolStripMenuItem _mnuBatch;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button19")]
	private ToolStripButton __cmdIcon_Button19;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSPFSChema")]
	private ToolStripMenuItem _mnuSPFSChema;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSetCredentials")]
	private ToolStripMenuItem _mnuSetCredentials;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuNodes")]
	private ToolStripMenuItem _mnuNodes;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRefreshViews")]
	private ToolStripMenuItem _mnuRefreshViews;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEmailQuery")]
	private ToolStripMenuItem _mnuEmailQuery;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallOracle")]
	private ToolStripMenuItem _mnuInstallOracle;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuQGlobals")]
	private ToolStripMenuItem _mnuQGlobals;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuEditTime")]
	private ToolStripMenuItem _mnuEditTime;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFabSHShare")]
	private ToolStripMenuItem _mnuFabSHShare;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFindTreePtn")]
	private ToolStripMenuItem _mnuFindTreePtn;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallRDef")]
	private ToolStripMenuItem _mnuInstallRDef;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallRNxt")]
	private ToolStripMenuItem _mnuInstallRNxt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSHCleanup0")]
	private ToolStripMenuItem _mnuSHCleanup0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSQLiteSchema0")]
	private ToolStripMenuItem _mnuSQLiteSchema0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGenEmail0")]
	private ToolStripMenuItem _mnuGenEmail0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveIcon")]
	private ToolStripMenuItem _mnuSaveIcon;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPivotSQLite")]
	private ToolStripMenuItem _mnuPivotSQLite;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPivotPython")]
	private ToolStripMenuItem _mnuPivotPython;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnusaveqexeI")]
	private ToolStripMenuItem _mnusaveqexeI;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveQExeS")]
	private ToolStripMenuItem _mnuSaveQExeS;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSHGUIAll")]
	private ToolStripMenuItem _mnuSHGUIAll;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdPythonEE")]
	private ToolStripButton _cmdPythonEE;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_cmdIcon_Button3a")]
	private ToolStripButton __cmdIcon_Button3a;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFocusTree")]
	private ToolStripMenuItem _mnuFocusTree;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuFindCol")]
	private ToolStripMenuItem _mnuFindCol;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuusepyorva")]
	private ToolStripMenuItem _mnuusepyorva;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallPyV3")]
	private ToolStripMenuItem _mnuInstallPyV3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGetVerConfig")]
	private ToolStripMenuItem _mnuGetVerConfig;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveQSPFSQL")]
	private ToolStripMenuItem _mnuSaveQSPFSQL;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSPFWiki")]
	private ToolStripMenuItem _mnuSPFWiki;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallPyV3Nxt")]
	private ToolStripMenuItem _mnuInstallPyV3Nxt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuTxtView")]
	private ToolStripSplitButton _mnuTxtView;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSPFGrid")]
	private ToolStripMenuItem _mnuSPFGrid;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSQLPos")]
	private ToolStripMenuItem _mnuSQLPos;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuencrypt0")]
	private ToolStripMenuItem _mnuencrypt0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInstallPyV3Old")]
	private ToolStripMenuItem _mnuInstallPyV3Old;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuhelpRev")]
	private ToolStripMenuItem _mnuhelpRev;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelpTrain")]
	private ToolStripMenuItem _mnuHelpTrain;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuHelpChg")]
	private ToolStripMenuItem _mnuHelpChg;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuGeniBDFields0")]
	private ToolStripMenuItem _mnuGeniBDFields0;

	private FrmMultiQuery FrmMultiQuery;

	private FrmSampleQs FrmSampleQs;

	public FrmCmdSim FrmCmdSimf;

	public FrmSQLQuerya[] FrmSQLQuery;

	public virtual ToolStripMenuItem MnuNewQuery
	{
		[CompilerGenerated]
		get
		{
			return _MnuNewQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuNewQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuNewQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuNewQuery = value;
			toolStripMenuItem = _MnuNewQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuOpenQuery
	{
		[CompilerGenerated]
		get
		{
			return _MnuOpenQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuOpenQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuOpenQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuOpenQuery = value;
			toolStripMenuItem = _MnuOpenQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuSaveQuery
	{
		[CompilerGenerated]
		get
		{
			return _MnuSaveQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuSaveQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuSaveQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuSaveQuery = value;
			toolStripMenuItem = _MnuSaveQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuSaveQAs
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveQAs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveQAs_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveQAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveQAs = value;
			toolStripMenuItem = _mnuSaveQAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuShowQuery
	{
		[CompilerGenerated]
		get
		{
			return _MnuShowQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuShowQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuShowQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuShowQuery = value;
			toolStripMenuItem = _MnuShowQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuFileDash1")]
	public virtual ToolStripSeparator MnuFileDash1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuRunQuery
	{
		[CompilerGenerated]
		get
		{
			return _MnuRunQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnurunquery_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuRunQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuRunQuery = value;
			toolStripMenuItem = _MnuRunQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuFileDash2")]
	public virtual ToolStripSeparator MnuFileDash2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuexit
	{
		[CompilerGenerated]
		get
		{
			return _mnuexit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExit_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuexit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuexit = value;
			toolStripMenuItem = _mnuexit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuFileBar")]
	public virtual ToolStripSeparator MnuFileBar
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuFile0
	{
		[CompilerGenerated]
		get
		{
			return _MnuFile0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuFile0_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuFile0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuFile0 = value;
			toolStripMenuItem = _MnuFile0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuFile1
	{
		[CompilerGenerated]
		get
		{
			return _MnuFile1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuFile0_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuFile1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuFile1 = value;
			toolStripMenuItem = _MnuFile1;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuFile2
	{
		[CompilerGenerated]
		get
		{
			return _MnuFile2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuFile0_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuFile2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuFile2 = value;
			toolStripMenuItem = _MnuFile2;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuFile3
	{
		[CompilerGenerated]
		get
		{
			return _MnuFile3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuFile0_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuFile3;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuFile3 = value;
			toolStripMenuItem = _MnuFile3;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuFile")]
	public virtual ToolStripMenuItem MnuFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuAddTables
	{
		[CompilerGenerated]
		get
		{
			return _MnuAddTables;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuAddTables_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuAddTables;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuAddTables = value;
			toolStripMenuItem = _MnuAddTables;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuCompute0
	{
		[CompilerGenerated]
		get
		{
			return _MnuCompute0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuCompute0_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuCompute0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuCompute0 = value;
			toolStripMenuItem = _MnuCompute0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuOutInline
	{
		[CompilerGenerated]
		get
		{
			return _mnuOutInline;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuOutInline_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuOutInline;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuOutInline = value;
			toolStripMenuItem = _mnuOutInline;
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

	public virtual ToolStripMenuItem mnuToggleGrid
	{
		[CompilerGenerated]
		get
		{
			return _mnuToggleGrid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuToggleGrid_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuToggleGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuToggleGrid = value;
			toolStripMenuItem = _mnuToggleGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuSASOptions
	{
		[CompilerGenerated]
		get
		{
			return _MnuSASOptions;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuSASOptions_DropDownOpening;
			ToolStripMenuItem toolStripMenuItem = _MnuSASOptions;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.DropDownOpening -= value2;
			}
			_MnuSASOptions = value;
			toolStripMenuItem = _MnuSASOptions;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.DropDownOpening += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuConnect
	{
		[CompilerGenerated]
		get
		{
			return _MnuConnect;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConnect_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuConnect;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuConnect = value;
			toolStripMenuItem = _MnuConnect;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuDisConnect
	{
		[CompilerGenerated]
		get
		{
			return _MnuDisConnect;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuDisconnect_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuDisConnect;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuDisConnect = value;
			toolStripMenuItem = _MnuDisConnect;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuSpawnVAX
	{
		[CompilerGenerated]
		get
		{
			return _MnuSpawnVAX;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuSpawnVAX_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuSpawnVAX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuSpawnVAX = value;
			toolStripMenuItem = _MnuSpawnVAX;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuSession")]
	public virtual ToolStripMenuItem MnuSession
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuConfigure
	{
		[CompilerGenerated]
		get
		{
			return _MnuConfigure;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuConfigure_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuConfigure;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuConfigure = value;
			toolStripMenuItem = _MnuConfigure;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMultiQuery
	{
		[CompilerGenerated]
		get
		{
			return _mnuMultiQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMultiQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMultiQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMultiQuery = value;
			toolStripMenuItem = _mnuMultiQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MnuUtilitiesDash0")]
	public virtual ToolStripSeparator MnuUtilitiesDash0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuExcel
	{
		[CompilerGenerated]
		get
		{
			return _MnuExcel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuExcel_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuExcel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuExcel = value;
			toolStripMenuItem = _MnuExcel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem MnuCloseExcel
	{
		[CompilerGenerated]
		get
		{
			return _MnuCloseExcel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuCloseExcel_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuCloseExcel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuCloseExcel = value;
			toolStripMenuItem = _MnuCloseExcel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuUtilitiesDash3")]
	public virtual ToolStripSeparator mnuUtilitiesDash3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuJMP
	{
		[CompilerGenerated]
		get
		{
			return _mnuJMP;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuJMP_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuJMP;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuJMP = value;
			toolStripMenuItem = _mnuJMP;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuCloseJMP
	{
		[CompilerGenerated]
		get
		{
			return _mnuCloseJMP;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCloseJMP_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCloseJMP;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCloseJMP = value;
			toolStripMenuItem = _mnuCloseJMP;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuUtilitiesdash4")]
	public virtual ToolStripSeparator mnuUtilitiesdash4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuTxt
	{
		[CompilerGenerated]
		get
		{
			return _mnuTxt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuTxt_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuTxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuTxt = value;
			toolStripMenuItem = _mnuTxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuUtilitiesDash5")]
	public virtual ToolStripSeparator mnuUtilitiesDash5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnubatchutilities")]
	public virtual ToolStripMenuItem mnubatchutilities
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MnaHelpdash1")]
	public virtual ToolStripSeparator MnaHelpdash1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MnuHelpDash3")]
	public virtual ToolStripSeparator MnuHelpDash3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem MnuHelPAbout
	{
		[CompilerGenerated]
		get
		{
			return _MnuHelPAbout;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MnuHelPAbout_Click;
			ToolStripMenuItem toolStripMenuItem = _MnuHelPAbout;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_MnuHelPAbout = value;
			toolStripMenuItem = _MnuHelPAbout;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuhelp")]
	public virtual ToolStripMenuItem mnuhelp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MainMenu1")]
	public virtual MenuStrip MainMenu1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripButton _cmdIcon_Button1
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button1 = value;
			toolStripButton = __cmdIcon_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button2
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button2 = value;
			toolStripButton = __cmdIcon_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button4
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button4 = value;
			toolStripButton = __cmdIcon_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button5
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button5;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button5 = value;
			toolStripButton = __cmdIcon_Button5;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button6
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button6;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button6;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button6 = value;
			toolStripButton = __cmdIcon_Button6;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button7
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button7;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button7;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button7 = value;
			toolStripButton = __cmdIcon_Button7;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button8
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button8;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button8;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button8 = value;
			toolStripButton = __cmdIcon_Button8;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button9
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button9;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button9;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button9 = value;
			toolStripButton = __cmdIcon_Button9;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button11
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button11;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button11;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button11 = value;
			toolStripButton = __cmdIcon_Button11;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button12
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button12;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button12;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button12 = value;
			toolStripButton = __cmdIcon_Button12;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button13
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button13;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button13;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button13 = value;
			toolStripButton = __cmdIcon_Button13;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button14
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button14;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button14;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button14 = value;
			toolStripButton = __cmdIcon_Button14;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button16
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button16;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button16;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button16 = value;
			toolStripButton = __cmdIcon_Button16;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button17
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button17;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button17;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button17 = value;
			toolStripButton = __cmdIcon_Button17;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _cmdIcon_Button18
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button18;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button18;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button18 = value;
			toolStripButton = __cmdIcon_Button18;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdIcon")]
	public virtual ToolStrip cmdIcon
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripButton _cmdIcon_Button10
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button10;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button10;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button10 = value;
			toolStripButton = __cmdIcon_Button10;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ClearScreenToolStripMenuItem
	{
		[CompilerGenerated]
		get
		{
			return _ClearScreenToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ClearScreenToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = _ClearScreenToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_ClearScreenToolStripMenuItem = value;
			toolStripMenuItem = _ClearScreenToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Gauge1")]
	internal virtual ToolStripProgressBar Gauge1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FolderBrowserDialog1")]
	internal virtual FolderBrowserDialog FolderBrowserDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuEmbedPrePost")]
	internal virtual ToolStripMenuItem mnuEmbedPrePost
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuBatch
	{
		[CompilerGenerated]
		get
		{
			return _mnuBatch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuBatch_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuBatch;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuBatch = value;
			toolStripMenuItem = _mnuBatch;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton _cmdIcon_Button19
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button19;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button19;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button19 = value;
			toolStripButton = __cmdIcon_Button19;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSPFSChema
	{
		[CompilerGenerated]
		get
		{
			return _mnuSPFSChema;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSPFSChema_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSPFSChema;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSPFSChema = value;
			toolStripMenuItem = _mnuSPFSChema;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSetCredentials
	{
		[CompilerGenerated]
		get
		{
			return _mnuSetCredentials;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSetCredentials_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSetCredentials;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSetCredentials = value;
			toolStripMenuItem = _mnuSetCredentials;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuNodes
	{
		[CompilerGenerated]
		get
		{
			return _mnuNodes;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuNodes_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuNodes;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuNodes = value;
			toolStripMenuItem = _mnuNodes;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuRefreshViews
	{
		[CompilerGenerated]
		get
		{
			return _mnuRefreshViews;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRefreshViews_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRefreshViews;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRefreshViews = value;
			toolStripMenuItem = _mnuRefreshViews;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuSaveQExe")]
	internal virtual ToolStripMenuItem mnuSaveQExe
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuspfsite")]
	internal virtual ToolStripMenuItem mnuspfsite
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuspfsitevalue")]
	internal virtual ToolStripTextBox mnuspfsitevalue
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuEmailQuery
	{
		[CompilerGenerated]
		get
		{
			return _mnuEmailQuery;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuEmailQuery_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuEmailQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuEmailQuery = value;
			toolStripMenuItem = _mnuEmailQuery;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuInstallR")]
	internal virtual ToolStripMenuItem mnuInstallR
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuInstallOracle
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallOracle;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallOracle;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallOracle = value;
			toolStripMenuItem = _mnuInstallOracle;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuPivotMode")]
	internal virtual ToolStripMenuItem mnuPivotMode
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

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
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

	internal virtual ToolStripMenuItem mnuQGlobals
	{
		[CompilerGenerated]
		get
		{
			return _mnuQGlobals;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuQGlobals_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuQGlobals;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuQGlobals = value;
			toolStripMenuItem = _mnuQGlobals;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuEditTime
	{
		[CompilerGenerated]
		get
		{
			return _mnuEditTime;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuEditTime_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuEditTime;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuEditTime = value;
			toolStripMenuItem = _mnuEditTime;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuFabSHShare
	{
		[CompilerGenerated]
		get
		{
			return _mnuFabSHShare;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpHTML_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFabSHShare;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFabSHShare = value;
			toolStripMenuItem = _mnuFabSHShare;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuFindTreePtn
	{
		[CompilerGenerated]
		get
		{
			return _mnuFindTreePtn;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuFindTreePtn_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFindTreePtn;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFindTreePtn = value;
			toolStripMenuItem = _mnuFindTreePtn;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator7")]
	internal virtual ToolStripSeparator ToolStripSeparator7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuInstallRDef
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallRDef;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallRDef;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallRDef = value;
			toolStripMenuItem = _mnuInstallRDef;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuInstallRNxt
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallRNxt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallRNxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallRNxt = value;
			toolStripMenuItem = _mnuInstallRNxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuQueryUtil")]
	internal virtual ToolStripMenuItem mnuQueryUtil
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSHCleanup0
	{
		[CompilerGenerated]
		get
		{
			return _mnuSHCleanup0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHCleanup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSHCleanup0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSHCleanup0 = value;
			toolStripMenuItem = _mnuSHCleanup0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSQLiteSchema0
	{
		[CompilerGenerated]
		get
		{
			return _mnuSQLiteSchema0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHCleanup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSQLiteSchema0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSQLiteSchema0 = value;
			toolStripMenuItem = _mnuSQLiteSchema0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuGenEmail0
	{
		[CompilerGenerated]
		get
		{
			return _mnuGenEmail0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHCleanup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuGenEmail0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuGenEmail0 = value;
			toolStripMenuItem = _mnuGenEmail0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSaveIcon
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveIcon;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveIcon_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveIcon;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveIcon = value;
			toolStripMenuItem = _mnuSaveIcon;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator9")]
	internal virtual ToolStripSeparator ToolStripSeparator9
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuInstallPython")]
	internal virtual ToolStripMenuItem mnuInstallPython
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuPivotSQLite
	{
		[CompilerGenerated]
		get
		{
			return _mnuPivotSQLite;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPivotSQLite_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPivotSQLite;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPivotSQLite = value;
			toolStripMenuItem = _mnuPivotSQLite;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuPivotPython
	{
		[CompilerGenerated]
		get
		{
			return _mnuPivotPython;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPivotPython_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPivotPython;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPivotPython = value;
			toolStripMenuItem = _mnuPivotPython;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnusaveqexeI
	{
		[CompilerGenerated]
		get
		{
			return _mnusaveqexeI;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SaveQExe;
			ToolStripMenuItem toolStripMenuItem = _mnusaveqexeI;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnusaveqexeI = value;
			toolStripMenuItem = _mnusaveqexeI;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator10")]
	internal virtual ToolStripSeparator ToolStripSeparator10
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSaveQExeS
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveQExeS;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SaveQExe;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveQExeS;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveQExeS = value;
			toolStripMenuItem = _mnuSaveQExeS;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuSQLiteHdrs")]
	internal virtual ToolStripMenuItem mnuSQLiteHdrs
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator8")]
	internal virtual ToolStripSeparator ToolStripSeparator8
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSHGUIAll
	{
		[CompilerGenerated]
		get
		{
			return _mnuSHGUIAll;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHGUIAll_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSHGUIAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSHGUIAll = value;
			toolStripMenuItem = _mnuSHGUIAll;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripButton cmdPythonEE
	{
		[CompilerGenerated]
		get
		{
			return _cmdPythonEE;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = _cmdPythonEE;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdPythonEE = value;
			toolStripButton = _cmdPythonEE;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton _cmdIcon_Button3a
	{
		[CompilerGenerated]
		get
		{
			return __cmdIcon_Button3a;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripButton toolStripButton = __cmdIcon_Button3a;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__cmdIcon_Button3a = value;
			toolStripButton = __cmdIcon_Button3a;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuIncNestUtil")]
	internal virtual ToolStripMenuItem mnuIncNestUtil
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("gImageList")]
	internal virtual ImageList gImageList
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuLegacyPivotHdrs")]
	internal virtual ToolStripMenuItem mnuLegacyPivotHdrs
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuFocusTree
	{
		[CompilerGenerated]
		get
		{
			return _mnuFocusTree;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuFocusTree_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFocusTree;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFocusTree = value;
			toolStripMenuItem = _mnuFocusTree;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuFindCol
	{
		[CompilerGenerated]
		get
		{
			return _mnuFindCol;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuFindCol_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuFindCol;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuFindCol = value;
			toolStripMenuItem = _mnuFindCol;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuusepyorva
	{
		[CompilerGenerated]
		get
		{
			return _mnuusepyorva;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuusepyorva_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuusepyorva;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuusepyorva = value;
			toolStripMenuItem = _mnuusepyorva;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuInstallPyV3
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallPyV3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallPyV3;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallPyV3 = value;
			toolStripMenuItem = _mnuInstallPyV3;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuGetVerConfig
	{
		[CompilerGenerated]
		get
		{
			return _mnuGetVerConfig;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GetVersionConfigToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuGetVerConfig;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuGetVerConfig = value;
			toolStripMenuItem = _mnuGetVerConfig;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator6")]
	internal virtual ToolStripSeparator ToolStripSeparator6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSaveQSPFSQL
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveQSPFSQL;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveQSPFSQL_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveQSPFSQL;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveQSPFSQL = value;
			toolStripMenuItem = _mnuSaveQSPFSQL;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSPFWiki
	{
		[CompilerGenerated]
		get
		{
			return _mnuSPFWiki;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpHTML_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSPFWiki;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSPFWiki = value;
			toolStripMenuItem = _mnuSPFWiki;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuInstallPyV3Nxt
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallPyV3Nxt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallPyV3Nxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallPyV3Nxt = value;
			toolStripMenuItem = _mnuInstallPyV3Nxt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripSplitButton mnuTxtView
	{
		[CompilerGenerated]
		get
		{
			return _mnuTxtView;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_ButtonClick;
			ToolStripSplitButton toolStripSplitButton = _mnuTxtView;
			if (toolStripSplitButton != null)
			{
				toolStripSplitButton.ButtonClick -= value2;
			}
			_mnuTxtView = value;
			toolStripSplitButton = _mnuTxtView;
			if (toolStripSplitButton != null)
			{
				toolStripSplitButton.ButtonClick += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSPFGrid
	{
		[CompilerGenerated]
		get
		{
			return _mnuSPFGrid;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuNotePad_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSPFGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSPFGrid = value;
			toolStripMenuItem = _mnuSPFGrid;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuJoinDuckDB")]
	internal virtual ToolStripMenuItem mnuJoinDuckDB
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

	internal virtual ToolStripMenuItem mnuSQLPos
	{
		[CompilerGenerated]
		get
		{
			return _mnuSQLPos;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSQLPos_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSQLPos;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSQLPos = value;
			toolStripMenuItem = _mnuSQLPos;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuencrypt0
	{
		[CompilerGenerated]
		get
		{
			return _mnuencrypt0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHCleanup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuencrypt0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuencrypt0 = value;
			toolStripMenuItem = _mnuencrypt0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("OldVersionToolStripMenuItem")]
	internal virtual ToolStripMenuItem OldVersionToolStripMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuInstallPyV3Old
	{
		[CompilerGenerated]
		get
		{
			return _mnuInstallPyV3Old;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuInstallR_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInstallPyV3Old;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInstallPyV3Old = value;
			toolStripMenuItem = _mnuInstallPyV3Old;
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

	internal virtual ToolStripMenuItem mnuhelpRev
	{
		[CompilerGenerated]
		get
		{
			return _mnuhelpRev;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpHTML_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuhelpRev;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuhelpRev = value;
			toolStripMenuItem = _mnuhelpRev;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuHelpTrain
	{
		[CompilerGenerated]
		get
		{
			return _mnuHelpTrain;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpHTML_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHelpTrain;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHelpTrain = value;
			toolStripMenuItem = _mnuHelpTrain;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuHelpChg
	{
		[CompilerGenerated]
		get
		{
			return _mnuHelpChg;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuHelpHTML_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuHelpChg;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuHelpChg = value;
			toolStripMenuItem = _mnuHelpChg;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuGeniBDFields0
	{
		[CompilerGenerated]
		get
		{
			return _mnuGeniBDFields0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSHCleanup_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuGeniBDFields0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuGeniBDFields0 = value;
			toolStripMenuItem = _mnuGeniBDFields0;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuAutoGrid")]
	internal virtual ToolStripMenuItem mnuAutoGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public FrmMain()
	{
		base.HandleDestroyed += FrmMain_HandleDestroyed;
		base.Load += FrmMain_Load;
		base.FormClosing += FrmMain_FormClosing;
		base.FormClosed += FrmMain_FormClosed;
		base.Resize += FrmMain_Resize;
		FrmSQLQuery = new FrmSQLQuerya[3];
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmMain));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.MainMenu1 = new System.Windows.Forms.MenuStrip();
		this.MnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuNewQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuOpenQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuSaveQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveQAs = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuShowQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveQExe = new System.Windows.Forms.ToolStripMenuItem();
		this.mnusaveqexeI = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuSaveQExeS = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveIcon = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveQSPFSQL = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEmailQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFileDash1 = new System.Windows.Forms.ToolStripSeparator();
		this.MnuRunQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFileDash2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuexit = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFileBar = new System.Windows.Forms.ToolStripSeparator();
		this.MnuFile0 = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFile1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFile2 = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuFile3 = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuSASOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuAddTables = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuCompute0 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuJoinDuckDB = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuPivotMode = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPivotSQLite = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPivotPython = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuLegacyPivotHdrs = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEmbedPrePost = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSQLiteHdrs = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuIncNestUtil = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuOutInline = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMacroFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuspfsite = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuspfsitevalue = new System.Windows.Forms.ToolStripTextBox();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuToggleGrid = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuFocusTree = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuFindCol = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuFindTreePtn = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRefreshViews = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuQGlobals = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuSession = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuConnect = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuDisConnect = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuSpawnVAX = new System.Windows.Forms.ToolStripMenuItem();
		this.ClearScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuNodes = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSetCredentials = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSQLPos = new System.Windows.Forms.ToolStripMenuItem();
		this.mnubatchutilities = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuConfigure = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuBatch = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMultiQuery = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSHGUIAll = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuEditTime = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuusepyorva = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuQueryUtil = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSHCleanup0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSQLiteSchema0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGenEmail0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuencrypt0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGeniBDFields0 = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuUtilitiesDash0 = new System.Windows.Forms.ToolStripSeparator();
		this.MnuExcel = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuCloseExcel = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUtilitiesDash3 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuJMP = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCloseJMP = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUtilitiesdash4 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuTxt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUtilitiesDash5 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuInstallR = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallRDef = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallRNxt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallPython = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallPyV3 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallPyV3Nxt = new System.Windows.Forms.ToolStripMenuItem();
		this.OldVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallPyV3Old = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInstallOracle = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuhelp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSPFWiki = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuhelpRev = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelpTrain = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuHelpChg = new System.Windows.Forms.ToolStripMenuItem();
		this.MnaHelpdash1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuFabSHShare = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuSPFSChema = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuGetVerConfig = new System.Windows.Forms.ToolStripMenuItem();
		this.MnuHelpDash3 = new System.Windows.Forms.ToolStripSeparator();
		this.MnuHelPAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.CMDialog1Open = new System.Windows.Forms.OpenFileDialog();
		this.CMDialog1Save = new System.Windows.Forms.SaveFileDialog();
		this.CMDialog1Font = new System.Windows.Forms.FontDialog();
		this.cmdIcon = new System.Windows.Forms.ToolStrip();
		this._cmdIcon_Button1 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button2 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button3a = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button4 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button5 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button6 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button7 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button8 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button19 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button9 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button10 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button11 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button12 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button13 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button14 = new System.Windows.Forms.ToolStripButton();
		this.mnuTxtView = new System.Windows.Forms.ToolStripSplitButton();
		this.mnuSPFGrid = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAutoGrid = new System.Windows.Forms.ToolStripMenuItem();
		this._cmdIcon_Button16 = new System.Windows.Forms.ToolStripButton();
		this.cmdPythonEE = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button17 = new System.Windows.Forms.ToolStripButton();
		this._cmdIcon_Button18 = new System.Windows.Forms.ToolStripButton();
		this.Gauge1 = new System.Windows.Forms.ToolStripProgressBar();
		this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.gImageList = new System.Windows.Forms.ImageList(this.components);
		this.MainMenu1.SuspendLayout();
		this.cmdIcon.SuspendLayout();
		base.SuspendLayout();
		this.MainMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.MnuFile, this.MnuSASOptions, this.MnuSession, this.mnubatchutilities, this.mnuhelp });
		this.MainMenu1.Location = new System.Drawing.Point(0, 0);
		this.MainMenu1.Name = "MainMenu1";
		this.MainMenu1.Size = new System.Drawing.Size(993, 28);
		this.MainMenu1.TabIndex = 1;
		this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[18]
		{
			this.MnuNewQuery, this.MnuOpenQuery, this.MnuSaveQuery, this.mnuSaveQAs, this.MnuShowQuery, this.mnuSaveQExe, this.mnuSaveIcon, this.mnuSaveQSPFSQL, this.mnuEmailQuery, this.MnuFileDash1,
			this.MnuRunQuery, this.MnuFileDash2, this.mnuexit, this.MnuFileBar, this.MnuFile0, this.MnuFile1, this.MnuFile2, this.MnuFile3
		});
		this.MnuFile.MergeAction = System.Windows.Forms.MergeAction.Remove;
		this.MnuFile.Name = "MnuFile";
		this.MnuFile.Size = new System.Drawing.Size(46, 24);
		this.MnuFile.Text = "&File";
		this.MnuNewQuery.Name = "MnuNewQuery";
		this.MnuNewQuery.ShortcutKeys = System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Control;
		this.MnuNewQuery.Size = new System.Drawing.Size(316, 26);
		this.MnuNewQuery.Text = "&New Query...";
		this.MnuOpenQuery.Name = "MnuOpenQuery";
		this.MnuOpenQuery.ShortcutKeys = System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control;
		this.MnuOpenQuery.Size = new System.Drawing.Size(316, 26);
		this.MnuOpenQuery.Text = "&Open Query...";
		this.MnuSaveQuery.Name = "MnuSaveQuery";
		this.MnuSaveQuery.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.MnuSaveQuery.Size = new System.Drawing.Size(316, 26);
		this.MnuSaveQuery.Text = "&Save Query";
		this.mnuSaveQAs.Name = "mnuSaveQAs";
		this.mnuSaveQAs.Size = new System.Drawing.Size(316, 26);
		this.mnuSaveQAs.Text = "Save Query &As...";
		this.MnuShowQuery.Name = "MnuShowQuery";
		this.MnuShowQuery.ShortcutKeys = System.Windows.Forms.Keys.Q | System.Windows.Forms.Keys.Control;
		this.MnuShowQuery.Size = new System.Drawing.Size(316, 26);
		this.MnuShowQuery.Text = "Show &Query...";
		this.mnuSaveQExe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnusaveqexeI, this.ToolStripSeparator10, this.mnuSaveQExeS });
		this.mnuSaveQExe.Name = "mnuSaveQExe";
		this.mnuSaveQExe.Size = new System.Drawing.Size(316, 26);
		this.mnuSaveQExe.Text = "Save Current Query as Exe/ZIP";
		this.mnuSaveQExe.ToolTipText = "save query as a self-contained executable";
		this.mnusaveqexeI.Name = "mnusaveqexeI";
		this.mnusaveqexeI.Size = new System.Drawing.Size(212, 26);
		this.mnusaveqexeI.Tag = "I";
		this.mnusaveqexeI.Text = "for Interactive Use";
		this.mnusaveqexeI.ToolTipText = "Uses Interactive Globals Translation";
		this.ToolStripSeparator10.Name = "ToolStripSeparator10";
		this.ToolStripSeparator10.Size = new System.Drawing.Size(209, 6);
		this.mnuSaveQExeS.Name = "mnuSaveQExeS";
		this.mnuSaveQExeS.Size = new System.Drawing.Size(212, 26);
		this.mnuSaveQExeS.Tag = "S";
		this.mnuSaveQExeS.Text = "for ScriptHost Use";
		this.mnuSaveQExeS.ToolTipText = "Uses ScriptHost Globals Translation";
		this.mnuSaveIcon.Name = "mnuSaveIcon";
		this.mnuSaveIcon.Size = new System.Drawing.Size(316, 26);
		this.mnuSaveIcon.Text = "Create Icon to Run a Query";
		this.mnuSaveQSPFSQL.Name = "mnuSaveQSPFSQL";
		this.mnuSaveQSPFSQL.Size = new System.Drawing.Size(316, 26);
		this.mnuSaveQSPFSQL.Text = "Save Current Query as SPFSQL File";
		this.mnuEmailQuery.Name = "mnuEmailQuery";
		this.mnuEmailQuery.Size = new System.Drawing.Size(316, 26);
		this.mnuEmailQuery.Text = "Email Query";
		this.MnuFileDash1.Name = "MnuFileDash1";
		this.MnuFileDash1.Size = new System.Drawing.Size(313, 6);
		this.MnuRunQuery.Name = "MnuRunQuery";
		this.MnuRunQuery.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.MnuRunQuery.Size = new System.Drawing.Size(316, 26);
		this.MnuRunQuery.Text = "&Run Query";
		this.MnuFileDash2.Name = "MnuFileDash2";
		this.MnuFileDash2.Size = new System.Drawing.Size(313, 6);
		this.mnuexit.Name = "mnuexit";
		this.mnuexit.Size = new System.Drawing.Size(316, 26);
		this.mnuexit.Text = "E&xit";
		this.MnuFileBar.Name = "MnuFileBar";
		this.MnuFileBar.Size = new System.Drawing.Size(313, 6);
		this.MnuFile0.Name = "MnuFile0";
		this.MnuFile0.Size = new System.Drawing.Size(316, 26);
		this.MnuFile0.Visible = false;
		this.MnuFile1.Name = "MnuFile1";
		this.MnuFile1.Size = new System.Drawing.Size(316, 26);
		this.MnuFile1.Visible = false;
		this.MnuFile2.Name = "MnuFile2";
		this.MnuFile2.Size = new System.Drawing.Size(316, 26);
		this.MnuFile2.Visible = false;
		this.MnuFile3.Name = "MnuFile3";
		this.MnuFile3.Size = new System.Drawing.Size(316, 26);
		this.MnuFile3.Visible = false;
		this.MnuSASOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[22]
		{
			this.MnuAddTables, this.MnuCompute0, this.ToolStripSeparator9, this.mnuJoinDuckDB, this.ToolStripSeparator1, this.mnuPivotMode, this.mnuLegacyPivotHdrs, this.mnuEmbedPrePost, this.mnuSQLiteHdrs, this.mnuIncNestUtil,
			this.ToolStripSeparator2, this.mnuOutInline, this.mnuMacroFile, this.mnuspfsite, this.ToolStripSeparator3, this.mnuToggleGrid, this.mnuFocusTree, this.mnuFindCol, this.mnuFindTreePtn, this.ToolStripSeparator4,
			this.mnuRefreshViews, this.mnuQGlobals
		});
		this.MnuSASOptions.MergeAction = System.Windows.Forms.MergeAction.Remove;
		this.MnuSASOptions.Name = "MnuSASOptions";
		this.MnuSASOptions.Size = new System.Drawing.Size(62, 24);
		this.MnuSASOptions.Text = "&Query";
		this.MnuAddTables.Name = "MnuAddTables";
		this.MnuAddTables.ShortcutKeys = System.Windows.Forms.Keys.T | System.Windows.Forms.Keys.Control;
		this.MnuAddTables.Size = new System.Drawing.Size(365, 26);
		this.MnuAddTables.Text = "Add &Table or Utility";
		this.MnuCompute0.Name = "MnuCompute0";
		this.MnuCompute0.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.MnuCompute0.Size = new System.Drawing.Size(365, 26);
		this.MnuCompute0.Text = "Create a Computed &Expression...";
		this.ToolStripSeparator9.Name = "ToolStripSeparator9";
		this.ToolStripSeparator9.Size = new System.Drawing.Size(362, 6);
		this.mnuJoinDuckDB.CheckOnClick = true;
		this.mnuJoinDuckDB.Name = "mnuJoinDuckDB";
		this.mnuJoinDuckDB.Size = new System.Drawing.Size(365, 26);
		this.mnuJoinDuckDB.Text = "Join Using DuckDB";
		this.mnuJoinDuckDB.ToolTipText = "Join Views Using DuckDB Rather than SQLite";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(362, 6);
		this.mnuPivotMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuPivotSQLite, this.mnuPivotPython });
		this.mnuPivotMode.Name = "mnuPivotMode";
		this.mnuPivotMode.Size = new System.Drawing.Size(365, 26);
		this.mnuPivotMode.Text = "Pivot Mode";
		this.mnuPivotMode.ToolTipText = "Pivot using SQLite or Python";
		this.mnuPivotSQLite.Name = "mnuPivotSQLite";
		this.mnuPivotSQLite.Size = new System.Drawing.Size(137, 26);
		this.mnuPivotSQLite.Text = "SQLite";
		this.mnuPivotPython.Name = "mnuPivotPython";
		this.mnuPivotPython.Size = new System.Drawing.Size(137, 26);
		this.mnuPivotPython.Text = "Python";
		this.mnuLegacyPivotHdrs.CheckOnClick = true;
		this.mnuLegacyPivotHdrs.Name = "mnuLegacyPivotHdrs";
		this.mnuLegacyPivotHdrs.Size = new System.Drawing.Size(365, 26);
		this.mnuLegacyPivotHdrs.Text = "Use Legacy Pivot Headers";
		this.mnuEmbedPrePost.Enabled = false;
		this.mnuEmbedPrePost.Name = "mnuEmbedPrePost";
		this.mnuEmbedPrePost.Size = new System.Drawing.Size(365, 26);
		this.mnuEmbedPrePost.Text = "Embed Pre/Post Queries";
		this.mnuSQLiteHdrs.CheckOnClick = true;
		this.mnuSQLiteHdrs.Name = "mnuSQLiteHdrs";
		this.mnuSQLiteHdrs.Size = new System.Drawing.Size(365, 26);
		this.mnuSQLiteHdrs.Text = "Allow Special Chars in SQLite Headers";
		this.mnuIncNestUtil.CheckOnClick = true;
		this.mnuIncNestUtil.Name = "mnuIncNestUtil";
		this.mnuIncNestUtil.Size = new System.Drawing.Size(365, 26);
		this.mnuIncNestUtil.Text = "Run child utils even if parent util disabled";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(362, 6);
		this.mnuOutInline.Name = "mnuOutInline";
		this.mnuOutInline.Size = new System.Drawing.Size(365, 26);
		this.mnuOutInline.Text = "Output to an &Inline View (No)";
		this.mnuMacroFile.Name = "mnuMacroFile";
		this.mnuMacroFile.Size = new System.Drawing.Size(365, 26);
		this.mnuMacroFile.Text = "Temporary &Macro File (No)";
		this.mnuspfsite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuspfsitevalue });
		this.mnuspfsite.Name = "mnuspfsite";
		this.mnuspfsite.Size = new System.Drawing.Size(365, 26);
		this.mnuspfsite.Text = "Temporary <<<SPF-Site>>>";
		this.mnuspfsite.ToolTipText = "Set Temporary <<<SPF-Site>>> macro variable for when running a single utility";
		this.mnuspfsitevalue.Font = new System.Drawing.Font("Segoe UI", 9f);
		this.mnuspfsitevalue.Name = "mnuspfsitevalue";
		this.mnuspfsitevalue.Size = new System.Drawing.Size(100, 27);
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(362, 6);
		this.mnuToggleGrid.Name = "mnuToggleGrid";
		this.mnuToggleGrid.ShortcutKeys = System.Windows.Forms.Keys.G | System.Windows.Forms.Keys.Control;
		this.mnuToggleGrid.Size = new System.Drawing.Size(365, 26);
		this.mnuToggleGrid.Text = "Toggle Grid";
		this.mnuFocusTree.Name = "mnuFocusTree";
		this.mnuFocusTree.ShortcutKeys = System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Control;
		this.mnuFocusTree.Size = new System.Drawing.Size(365, 26);
		this.mnuFocusTree.Text = "PathFinder Tree";
		this.mnuFindCol.Name = "mnuFindCol";
		this.mnuFindCol.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
		this.mnuFindCol.Size = new System.Drawing.Size(365, 26);
		this.mnuFindCol.Text = "Find Column Pattern";
		this.mnuFindTreePtn.AutoToolTip = true;
		this.mnuFindTreePtn.Name = "mnuFindTreePtn";
		this.mnuFindTreePtn.ShortcutKeys = System.Windows.Forms.Keys.F3;
		this.mnuFindTreePtn.Size = new System.Drawing.Size(365, 26);
		this.mnuFindTreePtn.Text = "Find Next Column Pattern";
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(362, 6);
		this.mnuRefreshViews.Image = SQLPathFinder3.My.Resources.Resources.RefreshW;
		this.mnuRefreshViews.ImageTransparentColor = System.Drawing.Color.White;
		this.mnuRefreshViews.Name = "mnuRefreshViews";
		this.mnuRefreshViews.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.mnuRefreshViews.Size = new System.Drawing.Size(365, 26);
		this.mnuRefreshViews.Text = "Refresh Views";
		this.mnuQGlobals.Name = "mnuQGlobals";
		this.mnuQGlobals.Size = new System.Drawing.Size(365, 26);
		this.mnuQGlobals.Text = "Set Query Global Variables";
		this.MnuSession.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.MnuConnect, this.MnuDisConnect, this.MnuSpawnVAX, this.ClearScreenToolStripMenuItem, this.mnuNodes, this.mnuSetCredentials, this.mnuSQLPos });
		this.MnuSession.MergeAction = System.Windows.Forms.MergeAction.Remove;
		this.MnuSession.Name = "MnuSession";
		this.MnuSession.Size = new System.Drawing.Size(77, 24);
		this.MnuSession.Text = "&Connect";
		this.MnuConnect.Name = "MnuConnect";
		this.MnuConnect.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control;
		this.MnuConnect.Size = new System.Drawing.Size(337, 26);
		this.MnuConnect.Text = "&Connect SQL Session";
		this.MnuDisConnect.Name = "MnuDisConnect";
		this.MnuDisConnect.Size = new System.Drawing.Size(337, 26);
		this.MnuDisConnect.Text = "&DisConnect SQL Session";
		this.MnuSpawnVAX.Name = "MnuSpawnVAX";
		this.MnuSpawnVAX.ShortcutKeys = System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Control;
		this.MnuSpawnVAX.Size = new System.Drawing.Size(337, 26);
		this.MnuSpawnVAX.Text = "&Show SQL Session";
		this.ClearScreenToolStripMenuItem.Name = "ClearScreenToolStripMenuItem";
		this.ClearScreenToolStripMenuItem.Size = new System.Drawing.Size(337, 26);
		this.ClearScreenToolStripMenuItem.Text = "Clear Screen";
		this.mnuNodes.Name = "mnuNodes";
		this.mnuNodes.ShortcutKeys = System.Windows.Forms.Keys.D | System.Windows.Forms.Keys.Control;
		this.mnuNodes.Size = new System.Drawing.Size(337, 26);
		this.mnuNodes.Text = "Set Default &Nodes";
		this.mnuSetCredentials.Name = "mnuSetCredentials";
		this.mnuSetCredentials.Size = new System.Drawing.Size(337, 26);
		this.mnuSetCredentials.Text = "Set Credentials";
		this.mnuSQLPos.Name = "mnuSQLPos";
		this.mnuSQLPos.Size = new System.Drawing.Size(337, 26);
		this.mnuSQLPos.Text = "Position SQL Session on Same Screen";
		this.mnubatchutilities.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[21]
		{
			this.MnuConfigure, this.ToolStripSeparator8, this.mnuBatch, this.mnuMultiQuery, this.mnuSHGUIAll, this.mnuEditTime, this.mnuusepyorva, this.ToolStripSeparator7, this.mnuQueryUtil, this.MnuUtilitiesDash0,
			this.MnuExcel, this.MnuCloseExcel, this.mnuUtilitiesDash3, this.mnuJMP, this.mnuCloseJMP, this.mnuUtilitiesdash4, this.mnuTxt, this.mnuUtilitiesDash5, this.mnuInstallR, this.mnuInstallPython,
			this.mnuInstallOracle
		});
		this.mnubatchutilities.MergeAction = System.Windows.Forms.MergeAction.Remove;
		this.mnubatchutilities.Name = "mnubatchutilities";
		this.mnubatchutilities.Size = new System.Drawing.Size(58, 24);
		this.mnubatchutilities.Text = "&Tools";
		this.MnuConfigure.Name = "MnuConfigure";
		this.MnuConfigure.ShortcutKeys = System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control;
		this.MnuConfigure.Size = new System.Drawing.Size(403, 26);
		this.MnuConfigure.Text = "&Configure SQLPathFinder...";
		this.ToolStripSeparator8.Name = "ToolStripSeparator8";
		this.ToolStripSeparator8.Size = new System.Drawing.Size(400, 6);
		this.mnuBatch.Name = "mnuBatch";
		this.mnuBatch.ShortcutKeys = System.Windows.Forms.Keys.B | System.Windows.Forms.Keys.Control;
		this.mnuBatch.Size = new System.Drawing.Size(403, 26);
		this.mnuBatch.Text = "Run Job in Batch on ScriptHost";
		this.mnuMultiQuery.Name = "mnuMultiQuery";
		this.mnuMultiQuery.ShortcutKeys = System.Windows.Forms.Keys.M | System.Windows.Forms.Keys.Control;
		this.mnuMultiQuery.Size = new System.Drawing.Size(403, 26);
		this.mnuMultiQuery.Text = "&Process Multiple Queries";
		this.mnuSHGUIAll.Name = "mnuSHGUIAll";
		this.mnuSHGUIAll.ShortcutKeys = System.Windows.Forms.Keys.U | System.Windows.Forms.Keys.Control;
		this.mnuSHGUIAll.Size = new System.Drawing.Size(403, 26);
		this.mnuSHGUIAll.Text = "ScriptHost Job Manager";
		this.mnuEditTime.Name = "mnuEditTime";
		this.mnuEditTime.Size = new System.Drawing.Size(403, 26);
		this.mnuEditTime.Text = "Edit an Update-Site-Time File";
		this.mnuusepyorva.Name = "mnuusepyorva";
		this.mnuusepyorva.ShortcutKeys = System.Windows.Forms.Keys.Y | System.Windows.Forms.Keys.Control;
		this.mnuusepyorva.Size = new System.Drawing.Size(403, 26);
		this.mnuusepyorva.Text = "Toggle Python or Legacy Extract Engine";
		this.ToolStripSeparator7.Name = "ToolStripSeparator7";
		this.ToolStripSeparator7.Size = new System.Drawing.Size(400, 6);
		this.mnuQueryUtil.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuSHCleanup0, this.mnuSQLiteSchema0, this.mnuGenEmail0, this.mnuencrypt0, this.mnuGeniBDFields0 });
		this.mnuQueryUtil.Name = "mnuQueryUtil";
		this.mnuQueryUtil.Size = new System.Drawing.Size(403, 26);
		this.mnuQueryUtil.Text = "Query Utilities";
		this.mnuSHCleanup0.Name = "mnuSHCleanup0";
		this.mnuSHCleanup0.Size = new System.Drawing.Size(379, 26);
		this.mnuSHCleanup0.Tag = "SH_Cleanup.vg2";
		this.mnuSHCleanup0.Text = "Cleanup ScriptHost or Local Personal folder";
		this.mnuSQLiteSchema0.Name = "mnuSQLiteSchema0";
		this.mnuSQLiteSchema0.Size = new System.Drawing.Size(379, 26);
		this.mnuSQLiteSchema0.Tag = "Get_Database_Schema_py.vg2";
		this.mnuSQLiteSchema0.Text = "Generate SQLPathFinder Database Schema";
		this.mnuSQLiteSchema0.ToolTipText = "Generate a Database Table Schema (Oracle, SQLServer, Teradata, SQLite, Impala)";
		this.mnuGenEmail0.Name = "mnuGenEmail0";
		this.mnuGenEmail0.Size = new System.Drawing.Size(379, 26);
		this.mnuGenEmail0.Tag = "Get_CDIS.vg2";
		this.mnuGenEmail0.Text = "Generate Email Address List";
		this.mnuGenEmail0.ToolTipText = "Generate e-mail Addresses for Email Utility";
		this.mnuencrypt0.Name = "mnuencrypt0";
		this.mnuencrypt0.Size = new System.Drawing.Size(379, 26);
		this.mnuencrypt0.Tag = "Encrypt_Text.vg2";
		this.mnuencrypt0.Text = "Encrypt Text (e.g., a password)";
		this.mnuencrypt0.ToolTipText = "Encrypt Text (e.g., a password)";
		this.mnuGeniBDFields0.Name = "mnuGeniBDFields0";
		this.mnuGeniBDFields0.Size = new System.Drawing.Size(379, 26);
		this.mnuGeniBDFields0.Tag = "Generate_imBigData_Fields.VG2";
		this.mnuGeniBDFields0.Text = "Generate imBigData DB Fields";
		this.mnuGeniBDFields0.ToolTipText = "Generate Fields from an imBigData DB";
		this.MnuUtilitiesDash0.Name = "MnuUtilitiesDash0";
		this.MnuUtilitiesDash0.Size = new System.Drawing.Size(400, 6);
		this.MnuExcel.Name = "MnuExcel";
		this.MnuExcel.ShortcutKeys = System.Windows.Forms.Keys.F9;
		this.MnuExcel.Size = new System.Drawing.Size(403, 26);
		this.MnuExcel.Text = "Show &Excel";
		this.MnuCloseExcel.Name = "MnuCloseExcel";
		this.MnuCloseExcel.Size = new System.Drawing.Size(403, 26);
		this.MnuCloseExcel.Text = "Close E&xcel";
		this.mnuUtilitiesDash3.Name = "mnuUtilitiesDash3";
		this.mnuUtilitiesDash3.Size = new System.Drawing.Size(400, 6);
		this.mnuJMP.Name = "mnuJMP";
		this.mnuJMP.ShortcutKeys = System.Windows.Forms.Keys.F11;
		this.mnuJMP.Size = new System.Drawing.Size(403, 26);
		this.mnuJMP.Text = "Show &JMP";
		this.mnuCloseJMP.Name = "mnuCloseJMP";
		this.mnuCloseJMP.Size = new System.Drawing.Size(403, 26);
		this.mnuCloseJMP.Text = "Close J&MP";
		this.mnuUtilitiesdash4.Name = "mnuUtilitiesdash4";
		this.mnuUtilitiesdash4.Size = new System.Drawing.Size(400, 6);
		this.mnuTxt.Name = "mnuTxt";
		this.mnuTxt.ShortcutKeys = System.Windows.Forms.Keys.F12;
		this.mnuTxt.Size = new System.Drawing.Size(403, 26);
		this.mnuTxt.Text = "Show Grid";
		this.mnuUtilitiesDash5.Name = "mnuUtilitiesDash5";
		this.mnuUtilitiesDash5.Size = new System.Drawing.Size(400, 6);
		this.mnuInstallR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuInstallRDef, this.mnuInstallRNxt });
		this.mnuInstallR.Name = "mnuInstallR";
		this.mnuInstallR.Size = new System.Drawing.Size(403, 26);
		this.mnuInstallR.Tag = "";
		this.mnuInstallR.Text = "Update/Install R";
		this.mnuInstallRDef.Name = "mnuInstallRDef";
		this.mnuInstallRDef.Size = new System.Drawing.Size(193, 26);
		this.mnuInstallRDef.Tag = "R_DEF";
		this.mnuInstallRDef.Text = "Default Version";
		this.mnuInstallRNxt.Name = "mnuInstallRNxt";
		this.mnuInstallRNxt.Size = new System.Drawing.Size(193, 26);
		this.mnuInstallRNxt.Tag = "R_NXT";
		this.mnuInstallRNxt.Text = "Next Version";
		this.mnuInstallPython.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuInstallPyV3, this.mnuInstallPyV3Nxt, this.OldVersionToolStripMenuItem });
		this.mnuInstallPython.Name = "mnuInstallPython";
		this.mnuInstallPython.Size = new System.Drawing.Size(403, 26);
		this.mnuInstallPython.Tag = "";
		this.mnuInstallPython.Text = "Update/Install Python";
		this.mnuInstallPyV3.Name = "mnuInstallPyV3";
		this.mnuInstallPyV3.Size = new System.Drawing.Size(193, 26);
		this.mnuInstallPyV3.Tag = "PYTHON3";
		this.mnuInstallPyV3.Text = "Default Version";
		this.mnuInstallPyV3Nxt.Name = "mnuInstallPyV3Nxt";
		this.mnuInstallPyV3Nxt.Size = new System.Drawing.Size(193, 26);
		this.mnuInstallPyV3Nxt.Tag = "PYTHON3_NXT";
		this.mnuInstallPyV3Nxt.Text = "Next Version";
		this.OldVersionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuInstallPyV3Old });
		this.OldVersionToolStripMenuItem.Name = "OldVersionToolStripMenuItem";
		this.OldVersionToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
		this.OldVersionToolStripMenuItem.Text = "Old Version";
		this.mnuInstallPyV3Old.Name = "mnuInstallPyV3Old";
		this.mnuInstallPyV3Old.Size = new System.Drawing.Size(168, 26);
		this.mnuInstallPyV3Old.Tag = "PYTHON3_OLD";
		this.mnuInstallPyV3Old.Text = "Old Version";
		this.mnuInstallOracle.Name = "mnuInstallOracle";
		this.mnuInstallOracle.Size = new System.Drawing.Size(403, 26);
		this.mnuInstallOracle.Tag = "O";
		this.mnuInstallOracle.Text = "Update Oracle Drivers";
		this.mnuInstallOracle.ToolTipText = "Updates Oracle drivers used with SQLPathFinder";
		this.mnuhelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.mnuSPFWiki, this.ToolStripSeparator5, this.mnuhelpRev, this.mnuHelpTrain, this.mnuHelpChg, this.MnaHelpdash1, this.mnuFabSHShare, this.ToolStripSeparator6, this.mnuSPFSChema, this.mnuGetVerConfig,
			this.MnuHelpDash3, this.MnuHelPAbout
		});
		this.mnuhelp.MergeAction = System.Windows.Forms.MergeAction.Remove;
		this.mnuhelp.Name = "mnuhelp";
		this.mnuhelp.Size = new System.Drawing.Size(55, 24);
		this.mnuhelp.Text = "&Help";
		this.mnuSPFWiki.Name = "mnuSPFWiki";
		this.mnuSPFWiki.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.mnuSPFWiki.Size = new System.Drawing.Size(313, 26);
		this.mnuSPFWiki.Tag = "WIKI";
		this.mnuSPFWiki.Text = "Help on SQLPathFinder (WIKI)";
		this.ToolStripSeparator5.Name = "ToolStripSeparator5";
		this.ToolStripSeparator5.Size = new System.Drawing.Size(310, 6);
		this.mnuhelpRev.Name = "mnuhelpRev";
		this.mnuhelpRev.Size = new System.Drawing.Size(313, 26);
		this.mnuhelpRev.Tag = "REV";
		this.mnuhelpRev.Text = "Revision History";
		this.mnuHelpTrain.Name = "mnuHelpTrain";
		this.mnuHelpTrain.Size = new System.Drawing.Size(313, 26);
		this.mnuHelpTrain.Tag = "TRAIN";
		this.mnuHelpTrain.Text = "Product Training";
		this.mnuHelpChg.Name = "mnuHelpChg";
		this.mnuHelpChg.Size = new System.Drawing.Size(313, 26);
		this.mnuHelpChg.Tag = "CHG";
		this.mnuHelpChg.Text = "How to Request Changes";
		this.MnaHelpdash1.Name = "MnaHelpdash1";
		this.MnaHelpdash1.Size = new System.Drawing.Size(310, 6);
		this.mnuFabSHShare.Name = "mnuFabSHShare";
		this.mnuFabSHShare.Size = new System.Drawing.Size(313, 26);
		this.mnuFabSHShare.Tag = "SH";
		this.mnuFabSHShare.Text = "ScriptHost Web Servers";
		this.ToolStripSeparator6.Name = "ToolStripSeparator6";
		this.ToolStripSeparator6.Size = new System.Drawing.Size(310, 6);
		this.mnuSPFSChema.Name = "mnuSPFSChema";
		this.mnuSPFSChema.Size = new System.Drawing.Size(313, 26);
		this.mnuSPFSChema.Text = "SQLPathFinder Schema";
		this.mnuSPFSChema.ToolTipText = "Show SQLPathFinder Views and Column Descriptions";
		this.mnuGetVerConfig.Name = "mnuGetVerConfig";
		this.mnuGetVerConfig.Size = new System.Drawing.Size(313, 26);
		this.mnuGetVerConfig.Text = "SQLPathFinder Version/Config";
		this.MnuHelpDash3.Name = "MnuHelpDash3";
		this.MnuHelpDash3.Size = new System.Drawing.Size(310, 6);
		this.MnuHelPAbout.Name = "MnuHelPAbout";
		this.MnuHelPAbout.Size = new System.Drawing.Size(313, 26);
		this.MnuHelPAbout.Text = "&About SQLPathFinder...";
		this.cmdIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmdIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[21]
		{
			this._cmdIcon_Button1, this._cmdIcon_Button2, this._cmdIcon_Button3a, this._cmdIcon_Button4, this._cmdIcon_Button5, this._cmdIcon_Button6, this._cmdIcon_Button7, this._cmdIcon_Button8, this._cmdIcon_Button19, this._cmdIcon_Button9,
			this._cmdIcon_Button10, this._cmdIcon_Button11, this._cmdIcon_Button12, this._cmdIcon_Button13, this._cmdIcon_Button14, this.mnuTxtView, this._cmdIcon_Button16, this.cmdPythonEE, this._cmdIcon_Button17, this._cmdIcon_Button18,
			this.Gauge1
		});
		this.cmdIcon.Location = new System.Drawing.Point(0, 28);
		this.cmdIcon.Name = "cmdIcon";
		this.cmdIcon.Size = new System.Drawing.Size(993, 42);
		this.cmdIcon.TabIndex = 0;
		this._cmdIcon_Button1.AutoSize = false;
		this._cmdIcon_Button1.Image = SQLPathFinder3.My.Resources.Resources.DOC2;
		this._cmdIcon_Button1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button1.ImageTransparentColor = System.Drawing.Color.Fuchsia;
		this._cmdIcon_Button1.Name = "_cmdIcon_Button1";
		this._cmdIcon_Button1.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button1.ToolTipText = "New Query";
		this._cmdIcon_Button2.AutoSize = false;
		this._cmdIcon_Button2.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button2.Image");
		this._cmdIcon_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button2.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button2.Name = "_cmdIcon_Button2";
		this._cmdIcon_Button2.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button2.ToolTipText = "Open Query";
		this._cmdIcon_Button3a.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._cmdIcon_Button3a.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button3a.Image");
		this._cmdIcon_Button3a.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button3a.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button3a.Name = "_cmdIcon_Button3a";
		this._cmdIcon_Button3a.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button3a.Text = "Open Sample Queries";
		this._cmdIcon_Button4.AutoSize = false;
		this._cmdIcon_Button4.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button4.Image");
		this._cmdIcon_Button4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button4.Name = "_cmdIcon_Button4";
		this._cmdIcon_Button4.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button4.ToolTipText = "Save Query";
		this._cmdIcon_Button5.AutoSize = false;
		this._cmdIcon_Button5.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button5.Image");
		this._cmdIcon_Button5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button5.Name = "_cmdIcon_Button5";
		this._cmdIcon_Button5.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button5.ToolTipText = "Run Query";
		this._cmdIcon_Button6.AutoSize = false;
		this._cmdIcon_Button6.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button6.Image");
		this._cmdIcon_Button6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button6.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button6.Name = "_cmdIcon_Button6";
		this._cmdIcon_Button6.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button6.ToolTipText = "Add View or Utility";
		this._cmdIcon_Button7.AutoSize = false;
		this._cmdIcon_Button7.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button7.Image");
		this._cmdIcon_Button7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button7.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button7.Name = "_cmdIcon_Button7";
		this._cmdIcon_Button7.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button7.ToolTipText = "Create Computed Expression";
		this._cmdIcon_Button8.AutoSize = false;
		this._cmdIcon_Button8.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button8.Image");
		this._cmdIcon_Button8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button8.Name = "_cmdIcon_Button8";
		this._cmdIcon_Button8.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button8.ToolTipText = "Show Query";
		this._cmdIcon_Button19.AutoSize = false;
		this._cmdIcon_Button19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._cmdIcon_Button19.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button19.Image");
		this._cmdIcon_Button19.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button19.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button19.Name = "_cmdIcon_Button19";
		this._cmdIcon_Button19.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button19.ToolTipText = "Run Job in Batch on ScriptHost";
		this._cmdIcon_Button9.AutoSize = false;
		this._cmdIcon_Button9.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button9.Image");
		this._cmdIcon_Button9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button9.Name = "_cmdIcon_Button9";
		this._cmdIcon_Button9.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button9.ToolTipText = "Process Multiple Queries";
		this._cmdIcon_Button10.AutoSize = false;
		this._cmdIcon_Button10.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button10.Image");
		this._cmdIcon_Button10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button10.ImageTransparentColor = System.Drawing.Color.Magenta;
		this._cmdIcon_Button10.Name = "_cmdIcon_Button10";
		this._cmdIcon_Button10.Size = new System.Drawing.Size(53, 39);
		this._cmdIcon_Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button10.ToolTipText = "Show SQL Session";
		this._cmdIcon_Button11.AutoSize = false;
		this._cmdIcon_Button11.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button11.Image");
		this._cmdIcon_Button11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button11.ImageTransparentColor = System.Drawing.Color.Fuchsia;
		this._cmdIcon_Button11.Name = "_cmdIcon_Button11";
		this._cmdIcon_Button11.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button11.ToolTipText = "Configure SQLPathFinder";
		this._cmdIcon_Button12.AutoSize = false;
		this._cmdIcon_Button12.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button12.Image");
		this._cmdIcon_Button12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button12.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button12.Name = "_cmdIcon_Button12";
		this._cmdIcon_Button12.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button12.ToolTipText = "Cancel Query";
		this._cmdIcon_Button13.AutoSize = false;
		this._cmdIcon_Button13.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button13.Image");
		this._cmdIcon_Button13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button13.Name = "_cmdIcon_Button13";
		this._cmdIcon_Button13.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button13.ToolTipText = "Show Excel";
		this._cmdIcon_Button14.AutoSize = false;
		this._cmdIcon_Button14.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button14.Image");
		this._cmdIcon_Button14.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button14.Name = "_cmdIcon_Button14";
		this._cmdIcon_Button14.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button14.ToolTipText = "Show JMP";
		this.mnuTxtView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mnuTxtView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuSPFGrid, this.mnuAutoGrid });
		this.mnuTxtView.Image = SQLPathFinder3.My.Resources.Resources.spfgrid2;
		this.mnuTxtView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.mnuTxtView.ImageTransparentColor = System.Drawing.Color.White;
		this.mnuTxtView.Name = "mnuTxtView";
		this.mnuTxtView.Size = new System.Drawing.Size(52, 39);
		this.mnuTxtView.Tag = "spfgrid";
		this.mnuTxtView.Text = "NotePadorSPFGrid";
		this.mnuTxtView.ToolTipText = "Show Grid";
		this.mnuSPFGrid.ImageTransparentColor = System.Drawing.Color.White;
		this.mnuSPFGrid.Name = "mnuSPFGrid";
		this.mnuSPFGrid.Size = new System.Drawing.Size(209, 26);
		this.mnuSPFGrid.Tag = "spfgrid";
		this.mnuSPFGrid.Text = "Grid";
		this.mnuSPFGrid.ToolTipText = "Manually load last Output to a Grid";
		this.mnuAutoGrid.CheckOnClick = true;
		this.mnuAutoGrid.Name = "mnuAutoGrid";
		this.mnuAutoGrid.Size = new System.Drawing.Size(209, 26);
		this.mnuAutoGrid.Text = "Auto Grid Display";
		this.mnuAutoGrid.ToolTipText = "Check to load output to Grid if a Query with Output is the last Step";
		this._cmdIcon_Button16.AutoSize = false;
		this._cmdIcon_Button16.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button16.Image");
		this._cmdIcon_Button16.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button16.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button16.Name = "_cmdIcon_Button16";
		this._cmdIcon_Button16.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button16.ToolTipText = "Set Nodes";
		this.cmdPythonEE.AutoSize = false;
		this.cmdPythonEE.BackColor = System.Drawing.Color.Orange;
		this.cmdPythonEE.Image = (System.Drawing.Image)resources.GetObject("cmdPythonEE.Image");
		this.cmdPythonEE.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.cmdPythonEE.ImageTransparentColor = System.Drawing.Color.White;
		this.cmdPythonEE.Name = "cmdPythonEE";
		this.cmdPythonEE.Size = new System.Drawing.Size(39, 39);
		this.cmdPythonEE.Tag = "0";
		this.cmdPythonEE.ToolTipText = "Using the Legacy Extract Engine";
		this._cmdIcon_Button17.AutoSize = false;
		this._cmdIcon_Button17.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button17.Image");
		this._cmdIcon_Button17.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button17.ImageTransparentColor = System.Drawing.Color.White;
		this._cmdIcon_Button17.Name = "_cmdIcon_Button17";
		this._cmdIcon_Button17.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button17.ToolTipText = "Help";
		this._cmdIcon_Button18.AutoSize = false;
		this._cmdIcon_Button18.Image = (System.Drawing.Image)resources.GetObject("_cmdIcon_Button18.Image");
		this._cmdIcon_Button18.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._cmdIcon_Button18.Name = "_cmdIcon_Button18";
		this._cmdIcon_Button18.Size = new System.Drawing.Size(40, 39);
		this._cmdIcon_Button18.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._cmdIcon_Button18.ToolTipText = "Exit";
		this.Gauge1.AutoSize = false;
		this.Gauge1.Name = "Gauge1";
		this.Gauge1.Size = new System.Drawing.Size(100, 20);
		this.Gauge1.Visible = false;
		this.gImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("gImageList.ImageStream");
		this.gImageList.TransparentColor = System.Drawing.Color.White;
		this.gImageList.Images.SetKeyName(0, "up");
		this.gImageList.Images.SetKeyName(1, "down");
		this.gImageList.Images.SetKeyName(2, "delete");
		this.gImageList.Images.SetKeyName(3, "browse");
		this.gImageList.Images.SetKeyName(4, "ok");
		this.gImageList.Images.SetKeyName(5, "cancel");
		this.gImageList.Images.SetKeyName(6, "left");
		this.gImageList.Images.SetKeyName(7, "right");
		this.gImageList.Images.SetKeyName(8, "edit");
		this.gImageList.Images.SetKeyName(9, "clear");
		this.gImageList.Images.SetKeyName(10, "searchfolder");
		this.gImageList.Images.SetKeyName(11, "refresh");
		this.gImageList.Images.SetKeyName(12, "save");
		this.gImageList.Images.SetKeyName(13, "run");
		this.gImageList.Images.SetKeyName(14, "clock");
		this.gImageList.Images.SetKeyName(15, "search");
		this.gImageList.Images.SetKeyName(16, "sort");
		this.gImageList.Images.SetKeyName(17, "insert");
		this.gImageList.Images.SetKeyName(18, "copy");
		this.gImageList.Images.SetKeyName(19, "editinfo");
		this.gImageList.Images.SetKeyName(20, "paste_color");
		this.gImageList.Images.SetKeyName(21, "helpb");
		this.gImageList.Images.SetKeyName(22, "closefldr");
		this.gImageList.Images.SetKeyName(23, "openfldr");
		this.gImageList.Images.SetKeyName(24, "helpy2");
		this.gImageList.Images.SetKeyName(25, "db");
		this.gImageList.Images.SetKeyName(26, "node");
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		base.ClientSize = new System.Drawing.Size(993, 598);
		base.Controls.Add(this.cmdIcon);
		base.Controls.Add(this.MainMenu1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.IsMdiContainer = true;
		base.Location = new System.Drawing.Point(140, -175);
		base.Name = "FrmMain";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SQLPathFinder (Main)";
		base.TransparencyKey = System.Drawing.Color.Transparent;
		this.MainMenu1.ResumeLayout(false);
		this.MainMenu1.PerformLayout();
		this.cmdIcon.ResumeLayout(false);
		this.cmdIcon.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public short ProcessCmdLineArgs(string MyArgTest)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num4 = default(int);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string MyErr = default(string);
		string MyQueryToProcess = default(string);
		int num6 = default(int);
		string text4 = default(string);
		int num7 = default(int);
		short num9 = default(short);
		short num10 = default(short);
		short num11 = default(short);
		string text5 = default(string);
		string f_Job = default(string);
		string text6 = default(string);
		short l_wMin = default(short);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text12 = default(string);
		string text13 = default(string);
		int num12 = default(int);
		string text14 = default(string);
		short num13 = default(short);
		bool ContainsMacro = default(bool);
		string text15 = default(string);
		string left = default(string);
		string text16 = default(string);
		string MyData = default(string);
		int num15 = default(int);
		string text17 = default(string);
		short num16 = default(short);
		int num17 = default(int);
		string text18 = default(string);
		string PackedExe = default(string);
		bool flag = default(bool);
		string left2 = default(string);
		string left3 = default(string);
		string text19 = default(string);
		string text20 = default(string);
		string text21 = default(string);
		string text22 = default(string);
		string MyInLineValue = default(string);
		short result = default(short);
		string[] commandLineArgs = default(string[]);
		int num18 = default(int);
		string[] array = default(string[]);
		int num19 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					int num8;
					short num14;
					string lpFileName;
					string lpString;
					string lpKeyName;
					string MyData2;
					switch (try0001_dispatch)
					{
					default:
						num2 = 1;
						num4 = 0;
						goto IL_0007;
					case 11941:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
								break;
							case 1:
								goto IL_26b9;
							default:
								goto end_IL_0001;
							}
							goto IL_2421;
						}
						IL_03cb:
						num2 = 81;
						Globals_Renamed.g_VarNo++;
						goto IL_03da;
						IL_03da:
						num2 = 82;
						Globals_Renamed.g_GVars[0, Globals_Renamed.g_VarNo] = text;
						goto IL_03ef;
						IL_03b6:
						num2 = 80;
						text2 = Strings.Trim(Strings.Mid(text3, num4 + 1));
						goto IL_03cb;
						IL_26b9:
						num5 = unchecked(num + 1);
						num = 0;
						switch (num5)
						{
						case 1:
							break;
						case 2:
							goto IL_0007;
						case 3:
							goto IL_000c;
						case 4:
							goto IL_0011;
						case 5:
							goto IL_0016;
						case 6:
							goto IL_001b;
						case 7:
							goto IL_0020;
						case 8:
							goto IL_0029;
						case 9:
							goto IL_0032;
						case 10:
							goto IL_003c;
						case 11:
							goto IL_0042;
						case 12:
							goto IL_004c;
						case 13:
							goto IL_0056;
						case 14:
							goto IL_0060;
						case 15:
							goto IL_006a;
						case 16:
							goto IL_0074;
						case 17:
							goto IL_007e;
						case 18:
							goto IL_0088;
						case 19:
							goto IL_0092;
						case 20:
							goto IL_0098;
						case 21:
							goto IL_00a2;
						case 22:
							goto IL_00a8;
						case 23:
							goto IL_00b2;
						case 24:
							goto IL_00b8;
						case 25:
							goto IL_00be;
						case 26:
							goto IL_00c4;
						case 27:
							goto IL_00ce;
						case 28:
							goto IL_00d8;
						case 29:
							goto IL_00e2;
						case 30:
							goto IL_00ec;
						case 31:
							goto IL_00f2;
						case 32:
							goto IL_00fc;
						case 33:
							goto IL_0102;
						case 34:
							goto IL_0108;
						case 35:
							goto IL_0112;
						case 36:
							goto IL_011c;
						case 37:
							goto IL_0126;
						case 38:
							goto IL_0130;
						case 39:
							goto IL_013a;
						case 40:
							goto IL_0140;
						case 41:
							goto IL_014a;
						case 42:
							goto IL_0154;
						case 43:
							goto IL_015e;
						case 44:
							goto IL_0168;
						case 45:
							goto IL_0172;
						case 46:
							goto IL_017c;
						case 47:
							goto IL_0186;
						case 48:
							goto IL_01a2;
						case 49:
						case 50:
							goto IL_01ae;
						case 51:
							goto IL_01ca;
						case 52:
						case 53:
							goto IL_01d6;
						case 54:
							goto IL_01f2;
						case 55:
						case 56:
							goto IL_0205;
						case 57:
							goto IL_0221;
						case 58:
						case 59:
							goto IL_022d;
						case 60:
							goto IL_0232;
						case 61:
							goto IL_024b;
						case 62:
							goto IL_025c;
						case 64:
							goto IL_027a;
						case 65:
							goto IL_0286;
						case 67:
							goto IL_02c6;
						case 68:
							goto IL_02f8;
						case 69:
							goto IL_030a;
						case 70:
							goto IL_031c;
						case 71:
							goto IL_032a;
						case 73:
							goto IL_0341;
						case 74:
							goto IL_0355;
						case 76:
							goto IL_036d;
						case 77:
							goto IL_0388;
						case 78:
							goto IL_03a1;
						case 80:
							goto IL_03b6;
						case 81:
							goto IL_03cb;
						case 82:
							goto IL_03da;
						case 83:
							goto IL_03ef;
						case 87:
							goto IL_040c;
						case 88:
							goto IL_042a;
						case 90:
							goto IL_0436;
						case 91:
							goto IL_0454;
						case 93:
							goto IL_0460;
						case 94:
							goto IL_047e;
						case 96:
							goto IL_048a;
						case 97:
							goto IL_04a8;
						case 99:
							goto IL_04b4;
						case 100:
							goto IL_04d2;
						case 102:
							goto IL_04de;
						case 103:
							goto IL_0501;
						case 104:
							goto IL_0513;
						case 105:
							goto IL_051d;
						case 106:
							goto IL_0527;
						case 107:
							goto IL_0538;
						case 108:
							goto IL_0553;
						case 109:
							goto IL_0569;
						case 111:
							goto IL_06e1;
						case 113:
							goto IL_06ee;
						case 115:
							goto IL_06fb;
						case 117:
							goto IL_0708;
						case 119:
							goto IL_0715;
						case 121:
							goto IL_0727;
						case 123:
							goto IL_0739;
						case 124:
							goto IL_075b;
						case 126:
							goto IL_076f;
						case 127:
							goto IL_0791;
						case 129:
							goto IL_07a5;
						case 130:
							goto IL_07ca;
						case 134:
							goto IL_07e7;
						case 137:
							goto IL_07f6;
						case 66:
						case 72:
						case 75:
						case 79:
						case 84:
						case 85:
						case 86:
						case 89:
						case 92:
						case 95:
						case 98:
						case 101:
						case 110:
						case 112:
						case 114:
						case 116:
						case 118:
						case 120:
						case 122:
						case 125:
						case 128:
						case 131:
						case 132:
						case 133:
						case 135:
						case 136:
						case 138:
						case 139:
							goto IL_0800;
						case 63:
						case 140:
							goto IL_081d;
						case 141:
							goto IL_0839;
						case 143:
						case 144:
							goto IL_085e;
						case 145:
							goto IL_086c;
						case 146:
							goto IL_0899;
						case 149:
							goto IL_08d2;
						case 150:
							goto IL_08ff;
						case 153:
							goto IL_0938;
						case 154:
							goto IL_0965;
						case 157:
							goto IL_099e;
						case 158:
							goto IL_09cb;
						case 161:
							goto IL_0a04;
						case 162:
							goto IL_0a31;
						case 165:
							goto IL_0a6a;
						case 166:
							goto IL_0a97;
						case 169:
							goto IL_0ad0;
						case 170:
							goto IL_0af2;
						case 173:
							goto IL_0b2b;
						case 174:
							goto IL_0b4d;
						case 177:
							goto IL_0b86;
						case 178:
							goto IL_0ba8;
						case 181:
							goto IL_0be1;
						case 182:
							goto IL_0c03;
						case 185:
							goto IL_0c3c;
						case 186:
							goto IL_0c69;
						case 189:
							goto IL_0ca2;
						case 190:
							goto IL_0ccf;
						case 193:
							goto IL_0d05;
						case 194:
							goto IL_0d32;
						case 148:
						case 152:
						case 156:
						case 160:
						case 164:
						case 168:
						case 172:
						case 176:
						case 180:
						case 184:
						case 188:
						case 192:
						case 196:
						case 197:
							goto IL_0d66;
						case 198:
							goto IL_0d85;
						case 199:
							goto IL_0d94;
						case 200:
							goto IL_0da3;
						case 201:
							goto IL_0dc5;
						case 202:
							goto IL_0dd9;
						case 203:
							goto IL_0e10;
						case 204:
							goto IL_0e22;
						case 205:
							goto IL_0e33;
						case 206:
							goto IL_0e40;
						case 207:
							goto IL_0e4c;
						case 212:
							goto IL_0e67;
						case 216:
							goto IL_0e93;
						case 217:
							goto IL_0eaf;
						case 208:
						case 210:
						case 211:
						case 214:
						case 215:
						case 218:
						case 219:
							goto IL_0ec5;
						case 220:
							goto IL_0f05;
						case 221:
						case 222:
							goto IL_0f14;
						case 223:
							goto IL_0f23;
						case 224:
							goto IL_0f30;
						case 227:
							goto IL_0f55;
						case 228:
							goto IL_0f71;
						case 226:
						case 229:
						case 230:
							goto IL_0f8e;
						case 231:
							goto IL_0f9d;
						case 232:
							goto IL_0fb3;
						case 233:
							goto IL_0fc4;
						case 235:
							goto IL_0fe0;
						case 234:
						case 236:
						case 237:
							goto IL_0ff0;
						case 238:
							goto IL_1006;
						case 239:
							goto IL_101a;
						case 240:
							goto IL_102e;
						case 241:
							goto IL_105a;
						case 242:
							goto IL_106f;
						case 243:
							goto IL_1080;
						case 244:
						case 245:
							goto IL_1096;
						case 246:
							goto IL_10ab;
						case 247:
							goto IL_10bc;
						case 250:
							goto IL_10d6;
						case 251:
							goto IL_10e0;
						case 248:
						case 249:
						case 252:
						case 253:
							goto IL_10ee;
						case 254:
							goto IL_1114;
						case 255:
							goto IL_1125;
						case 256:
							goto IL_1142;
						case 257:
						case 258:
							goto IL_115c;
						case 259:
							goto IL_118f;
						case 260:
							goto IL_11a3;
						case 262:
						case 263:
						case 264:
							goto IL_11b4;
						case 265:
							goto IL_11e3;
						case 266:
							goto IL_11f7;
						case 267:
							goto IL_1225;
						case 268:
							goto IL_122e;
						case 269:
						case 270:
							goto IL_1239;
						case 271:
							goto IL_1258;
						case 272:
							goto IL_126f;
						case 273:
							goto IL_127e;
						case 274:
							goto IL_129c;
						case 275:
							goto IL_12ac;
						case 276:
							goto IL_12bb;
						case 277:
							goto IL_12dd;
						case 278:
						case 279:
							goto IL_12f8;
						case 280:
							goto IL_1305;
						case 281:
							goto IL_1344;
						case 282:
							goto IL_1352;
						case 283:
							goto IL_1368;
						case 284:
							goto IL_1380;
						case 285:
							goto IL_1392;
						case 286:
							goto IL_13a7;
						case 287:
							goto IL_13f5;
						case 288:
							goto IL_140e;
						case 289:
							goto IL_141b;
						case 290:
							goto IL_1432;
						case 291:
							goto IL_1445;
						case 292:
							goto IL_1461;
						case 293:
							goto IL_146e;
						case 294:
							goto IL_1480;
						case 295:
							goto IL_1498;
						case 296:
						case 297:
						case 298:
							goto IL_14b4;
						case 299:
							goto IL_14c9;
						case 300:
							goto IL_14d2;
						case 301:
							goto IL_14db;
						case 302:
							goto IL_14f7;
						case 303:
						case 304:
						case 305:
							goto IL_1511;
						case 306:
							goto IL_1530;
						case 307:
							goto IL_157c;
						case 308:
							goto IL_1585;
						case 309:
							goto IL_1594;
						case 310:
							goto IL_15a3;
						case 311:
							goto IL_15c0;
						case 312:
						case 313:
						case 315:
						case 316:
						case 317:
							goto IL_15e7;
						case 318:
							goto IL_161b;
						case 319:
							goto IL_162c;
						case 322:
							goto IL_1643;
						case 323:
							goto IL_1665;
						case 326:
							goto IL_168c;
						case 327:
							goto IL_16b1;
						case 328:
							goto IL_16c6;
						case 329:
							goto IL_16d2;
						case 330:
							goto IL_16e3;
						case 331:
							goto IL_16f0;
						case 334:
							goto IL_1739;
						case 335:
							goto IL_1758;
						case 336:
							goto IL_1769;
						case 337:
							goto IL_177f;
						case 338:
							goto IL_1797;
						case 339:
							goto IL_17aa;
						case 340:
							goto IL_17c0;
						case 341:
							goto IL_17d2;
						case 342:
							goto IL_17e3;
						case 348:
							goto IL_17fe;
						case 349:
							goto IL_1823;
						case 350:
							goto IL_1830;
						case 352:
						case 353:
							goto IL_1861;
						case 354:
							goto IL_1876;
						case 355:
							goto IL_1882;
						case 356:
							goto IL_1893;
						case 357:
							goto IL_18a0;
						case 360:
							goto IL_18e9;
						case 361:
							goto IL_18ff;
						case 362:
							goto IL_1917;
						case 363:
							goto IL_1927;
						case 364:
							goto IL_1939;
						case 365:
							goto IL_1955;
						case 367:
							goto IL_1969;
						case 366:
						case 368:
						case 369:
							goto IL_197a;
						case 370:
							goto IL_19a0;
						case 371:
							goto IL_19b2;
						case 372:
							goto IL_19bf;
						case 373:
							goto IL_19e1;
						case 374:
							goto IL_1a2b;
						case 375:
							goto IL_1a3c;
						case 376:
							goto IL_1a52;
						case 377:
							goto IL_1a63;
						case 378:
							goto IL_1a77;
						case 379:
							goto IL_1a8b;
						case 380:
							goto IL_1a9f;
						case 382:
							goto IL_1ac0;
						case 386:
							goto IL_1ad4;
						case 387:
							goto IL_1b36;
						case 388:
							goto IL_1b52;
						case 390:
							goto IL_1b66;
						case 389:
						case 391:
						case 392:
							goto IL_1b77;
						case 393:
							goto IL_1b84;
						case 394:
							goto IL_1b8d;
						case 395:
							goto IL_1b97;
						case 396:
							goto IL_1be3;
						case 397:
							goto IL_1bff;
						case 398:
							goto IL_1c1b;
						case 400:
							goto IL_1c36;
						case 401:
							goto IL_1c52;
						case 402:
							goto IL_1c6e;
						case 399:
						case 403:
						case 404:
							goto IL_1c86;
						case 407:
							goto IL_1c9f;
						case 406:
						case 409:
						case 410:
							goto IL_1cd2;
						case 411:
							goto IL_1cf1;
						case 412:
							goto IL_1cf9;
						case 413:
							goto IL_1d06;
						case 414:
							goto IL_1d2e;
						case 415:
							goto IL_1d7d;
						case 416:
							goto IL_1d9c;
						case 418:
							goto IL_1df3;
						case 421:
							goto IL_1e3e;
						case 417:
						case 419:
						case 420:
						case 422:
						case 423:
							goto IL_1e92;
						case 424:
							goto IL_1ea9;
						case 425:
							goto IL_1eb6;
						case 426:
							goto IL_1ed8;
						case 427:
							goto IL_1f2c;
						case 429:
							goto IL_1f6d;
						case 430:
							goto IL_1fd7;
						case 428:
						case 431:
						case 432:
							goto IL_2039;
						case 433:
							goto IL_204a;
						case 434:
							goto IL_2060;
						case 435:
							goto IL_2071;
						case 436:
							goto IL_2085;
						case 437:
							goto IL_2099;
						case 438:
							goto IL_20aa;
						case 439:
							goto IL_20f6;
						case 440:
						case 441:
							goto IL_2104;
						case 442:
							goto IL_2115;
						case 443:
							goto IL_215f;
						case 444:
						case 445:
							goto IL_216d;
						case 446:
							goto IL_2181;
						case 447:
							goto IL_219d;
						case 448:
							goto IL_21ae;
						case 449:
							goto IL_21fa;
						case 453:
							goto IL_2211;
						case 454:
							goto IL_2222;
						case 455:
							goto IL_226c;
						case 460:
							goto IL_2283;
						case 461:
							goto IL_22a5;
						case 464:
							goto IL_22cc;
						case 465:
							goto IL_22f1;
						case 466:
							goto IL_2302;
						case 468:
							goto IL_2323;
						case 469:
							goto IL_233a;
						case 470:
							goto IL_2389;
						case 471:
							goto IL_239c;
						case 321:
						case 325:
						case 347:
						case 359:
						case 384:
						case 385:
						case 452:
						case 456:
						case 457:
						case 458:
						case 459:
						case 463:
						case 473:
						case 475:
						case 476:
							goto IL_23c7;
						case 477:
							goto IL_23d8;
						case 467:
						case 479:
							goto IL_23e6;
						case 481:
							goto IL_2421;
						case 482:
							goto IL_2438;
						case 483:
							goto IL_2445;
						case 314:
						case 485:
							goto IL_24a6;
						case 381:
						case 405:
						case 450:
						case 451:
						case 472:
						case 487:
							goto IL_24f6;
						case 351:
						case 408:
						case 489:
							goto IL_2546;
						case 491:
							num2 = 491;
							MyErr = Conversions.ToString(DateAndTime.Now) + ": The SQLPathFinder Query (" + MyQueryToProcess + ") could not be processed. SPF Jobs cannot be run in quiet mode." + Globals_Renamed.CRLF + Globals_Renamed.CRLF + MyErr;
							goto IL_262f;
						case 474:
						case 493:
							goto IL_25e2;
						case 142:
						case 147:
						case 151:
						case 155:
						case 159:
						case 163:
						case 167:
						case 171:
						case 175:
						case 179:
						case 183:
						case 187:
						case 191:
						case 195:
						case 209:
						case 213:
						case 332:
						case 358:
						case 480:
						case 484:
						case 486:
						case 488:
						case 490:
						case 492:
						case 494:
						case 495:
							goto IL_262f;
						case 496:
							goto IL_2638;
						case 497:
							goto IL_2645;
						case 498:
							goto IL_266d;
						case 499:
							goto IL_2686;
						case 500:
							goto IL_269d;
						case 343:
						case 383:
						case 478:
						case 501:
						case 502:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 225:
						case 261:
						case 320:
						case 324:
						case 333:
						case 344:
						case 345:
						case 346:
						case 462:
						case 503:
							goto end_IL_0001_3;
						}
						goto default;
						IL_2421:
						num2 = 481;
						FileSystem.FileClose(num6);
						goto IL_2438;
						IL_2438:
						num2 = 482;
						MyErr = "SQLPathFinder Error During Batch Execution...";
						goto IL_2445;
						IL_2445:
						num2 = 483;
						MyErr = MyErr + Globals_Renamed.CRLF + Globals_Renamed.CRLF + "Error Writing to SPF File:" + Strings.Trim(Globals_Renamed.MyPCDir) + text4 + ".spf. (" + Conversion.ErrorToString() + ")";
						goto IL_262f;
						IL_0007:
						num2 = 2;
						num7 = 0;
						goto IL_000c;
						IL_000c:
						num2 = 3;
						num8 = 0;
						goto IL_0011;
						IL_0011:
						num2 = 4;
						num9 = 0;
						goto IL_0016;
						IL_0016:
						num2 = 5;
						num10 = 0;
						goto IL_001b;
						IL_001b:
						num2 = 6;
						num11 = 0;
						goto IL_0020;
						IL_0020:
						num2 = 7;
						text5 = "";
						goto IL_0029;
						IL_0029:
						num2 = 8;
						f_Job = "";
						goto IL_0032;
						IL_0032:
						num2 = 9;
						text6 = "";
						goto IL_003c;
						IL_003c:
						num2 = 10;
						l_wMin = 0;
						goto IL_0042;
						IL_0042:
						num2 = 11;
						text7 = "";
						goto IL_004c;
						IL_004c:
						num2 = 12;
						text8 = "";
						goto IL_0056;
						IL_0056:
						num2 = 13;
						text9 = "";
						goto IL_0060;
						IL_0060:
						num2 = 14;
						text10 = "";
						goto IL_006a;
						IL_006a:
						num2 = 15;
						text4 = "";
						goto IL_0074;
						IL_0074:
						num2 = 16;
						text11 = "sqlpathfinder_input_arguments_error";
						goto IL_007e;
						IL_007e:
						num2 = 17;
						text12 = "";
						goto IL_0088;
						IL_0088:
						num2 = 18;
						text13 = "";
						goto IL_0092;
						IL_0092:
						num2 = 19;
						num12 = 0;
						goto IL_0098;
						IL_0098:
						num2 = 20;
						text14 = "";
						goto IL_00a2;
						IL_00a2:
						num2 = 21;
						num13 = 0;
						goto IL_00a8;
						IL_00a8:
						num2 = 22;
						MyErr = "";
						goto IL_00b2;
						IL_00b2:
						num2 = 23;
						num6 = 0;
						goto IL_00b8;
						IL_00b8:
						num2 = 24;
						ContainsMacro = false;
						goto IL_00be;
						IL_00be:
						num2 = 25;
						num14 = 0;
						goto IL_00c4;
						IL_00c4:
						num2 = 26;
						text15 = "";
						goto IL_00ce;
						IL_00ce:
						num2 = 27;
						left = "";
						goto IL_00d8;
						IL_00d8:
						num2 = 28;
						text16 = "";
						goto IL_00e2;
						IL_00e2:
						num2 = 29;
						MyData = "";
						goto IL_00ec;
						IL_00ec:
						num2 = 30;
						num15 = 0;
						goto IL_00f2;
						IL_00f2:
						num2 = 31;
						text17 = "";
						goto IL_00fc;
						IL_00fc:
						num2 = 32;
						num16 = 0;
						goto IL_0102;
						IL_0102:
						num2 = 33;
						num17 = 0;
						goto IL_0108;
						IL_0108:
						num2 = 34;
						text18 = "";
						goto IL_0112;
						IL_0112:
						num2 = 35;
						MyQueryToProcess = "";
						goto IL_011c;
						IL_011c:
						num2 = 36;
						text = "";
						goto IL_0126;
						IL_0126:
						num2 = 37;
						text2 = "";
						goto IL_0130;
						IL_0130:
						num2 = 38;
						PackedExe = "";
						goto IL_013a;
						IL_013a:
						num2 = 39;
						flag = true;
						goto IL_0140;
						IL_0140:
						num2 = 40;
						left2 = "PY3";
						goto IL_014a;
						IL_014a:
						num2 = 41;
						left3 = "";
						goto IL_0154;
						IL_0154:
						num2 = 42;
						text19 = "";
						goto IL_015e;
						IL_015e:
						num2 = 43;
						text20 = "";
						goto IL_0168;
						IL_0168:
						num2 = 44;
						text21 = "";
						goto IL_0172;
						IL_0172:
						num2 = 45;
						text22 = "";
						goto IL_017c;
						IL_017c:
						num2 = 46;
						MyInLineValue = "";
						goto IL_0186;
						IL_0186:
						num2 = 47;
						if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
						{
							goto IL_01a2;
						}
						goto IL_01ae;
						IL_01a2:
						num2 = 48;
						text19 = " /iReports_Version=NEXT";
						goto IL_01ae;
						IL_01ae:
						num2 = 50;
						if (Operators.CompareString(Globals_Renamed.gEncodeFFS, "Y", TextCompare: false) == 0)
						{
							goto IL_01ca;
						}
						goto IL_01d6;
						IL_01ca:
						num2 = 51;
						text21 = " /ENCODING_FULLFILE_SCAN=Y";
						goto IL_01d6;
						IL_01d6:
						num2 = 53;
						if (Operators.CompareString(Globals_Renamed.gEncodeUTFBOM, "Y", TextCompare: false) == 0)
						{
							goto IL_01f2;
						}
						goto IL_0205;
						IL_01f2:
						num2 = 54;
						text21 += " /ENCODING_UTFBOM=Y";
						goto IL_0205;
						IL_0205:
						num2 = 56;
						if (Operators.CompareString(Globals_Renamed.gConvertMAOUber, "Y", TextCompare: false) == 0)
						{
							goto IL_0221;
						}
						goto IL_022d;
						IL_0221:
						num2 = 57;
						text22 = " /USE_UBER_MAO=Y";
						goto IL_022d;
						IL_022d:
						num2 = 59;
						result = 1;
						goto IL_0232;
						IL_0232:
						num2 = 60;
						commandLineArgs = Environment.GetCommandLineArgs();
						num18 = 0;
						goto IL_080c;
						IL_080c:
						if (num18 < commandLineArgs.Length)
						{
							text3 = commandLineArgs[num18];
							goto IL_024b;
						}
						goto IL_081d;
						IL_03ef:
						num2 = 83;
						Globals_Renamed.g_GVars[1, Globals_Renamed.g_VarNo] = text2;
						goto IL_0800;
						IL_024b:
						num2 = 61;
						if (!flag)
						{
							goto IL_025c;
						}
						goto IL_07f6;
						IL_025c:
						num2 = 62;
						if (Operators.CompareString(MyErr, "", TextCompare: false) == 0)
						{
							goto IL_027a;
						}
						goto IL_081d;
						IL_081d:
						num2 = 140;
						if (Operators.CompareString(MyErr, "", TextCompare: false) != 0)
						{
							goto IL_0839;
						}
						goto IL_085e;
						IL_0839:
						num2 = 141;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": " + MyErr;
						goto IL_262f;
						IL_085e:
						num2 = 144;
						MyArgTest = Strings.UCase(MyArgTest);
						goto IL_086c;
						IL_086c:
						num2 = 145;
						if (Operators.CompareString(text10, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/STARTDIR") != 0)
						{
							goto IL_0899;
						}
						goto IL_08d2;
						IL_040c:
						num2 = 87;
						if (Operators.CompareString(Strings.UCase(text3), "/RUN", TextCompare: false) == 0)
						{
							goto IL_042a;
						}
						goto IL_0436;
						IL_0899:
						num2 = 146;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/STARTDIR", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_08d2:
						num2 = 149;
						if (Operators.CompareString(left2, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/EXTENGINE") != 0)
						{
							goto IL_08ff;
						}
						goto IL_0938;
						IL_042a:
						num2 = 88;
						num9 = 1;
						goto IL_0800;
						IL_08ff:
						num2 = 150;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/EXTENGINE", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0938:
						num2 = 153;
						if (Operators.CompareString(left3, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/SPFLOGLEVEL") != 0)
						{
							goto IL_0965;
						}
						goto IL_099e;
						IL_0436:
						num2 = 90;
						if (Operators.CompareString(Strings.UCase(text3), "/PAUSE", TextCompare: false) == 0)
						{
							goto IL_0454;
						}
						goto IL_0460;
						IL_0965:
						num2 = 154;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/SPFLOGLEVEL", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_099e:
						num2 = 157;
						if (Operators.CompareString(text6, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/SPFSQL") != 0)
						{
							goto IL_09cb;
						}
						goto IL_0a04;
						IL_0454:
						num2 = 91;
						num11 = 1;
						goto IL_0800;
						IL_09cb:
						num2 = 158;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/SPFSQL", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0a04:
						num2 = 161;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/PACK") != 0)
						{
							goto IL_0a31;
						}
						goto IL_0a6a;
						IL_0460:
						num2 = 93;
						if (Operators.CompareString(Strings.UCase(text3), "/WAIT", TextCompare: false) == 0)
						{
							goto IL_047e;
						}
						goto IL_048a;
						IL_0a31:
						num2 = 162;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/PACK", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0a6a:
						num2 = 165;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/JOB") != 0)
						{
							goto IL_0a97;
						}
						goto IL_0ad0;
						IL_047e:
						num2 = 94;
						num10 = 1;
						goto IL_0800;
						IL_0a97:
						num2 = 166;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty. Make sure you do not use spaces in assignment and bound value with double quotes if there are spaces in the path. E.g., @a@=\"c:\\my folder\\...\"", "@a@", "/JOB", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0ad0:
						num2 = 169;
						if (num9 == 0 && Strings.InStr(MyArgTest, "/RUN") != 0)
						{
							goto IL_0af2;
						}
						goto IL_0b2b;
						IL_048a:
						num2 = 96;
						if (Operators.CompareString(Strings.UCase(text3), "/MINIMIZE", TextCompare: false) == 0)
						{
							goto IL_04a8;
						}
						goto IL_04b4;
						IL_0af2:
						num2 = 170;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line should be separated from all other arguments with a space. E.g., @a@ /PAUSE", "@a@", "/RUN", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0b2b:
						num2 = 173;
						if (num16 == 0 && Strings.InStr(MyArgTest, "/LOG") != 0)
						{
							goto IL_0b4d;
						}
						goto IL_0b86;
						IL_04a8:
						num2 = 97;
						l_wMin = 1;
						goto IL_0800;
						IL_0b4d:
						num2 = 174;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line should be separated from all other arguments with a space. E.g., @a@ /PAUSE", "@a@", "/LOG", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0b86:
						num2 = 177;
						if (num10 == 0 && Strings.InStr(MyArgTest, "/WAIT") != 0)
						{
							goto IL_0ba8;
						}
						goto IL_0be1;
						IL_04b4:
						num2 = 99;
						if (Operators.CompareString(Strings.UCase(text3), "/LOG", TextCompare: false) == 0)
						{
							goto IL_04d2;
						}
						goto IL_04de;
						IL_0ba8:
						num2 = 178;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line should be separated from all other arguments with a space. E.g., @a@ /PAUSE", "@a@", "/WAIT", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0be1:
						num2 = 181;
						if (num11 == 0 && Strings.InStr(MyArgTest, "/PAUSE") != 0)
						{
							goto IL_0c03;
						}
						goto IL_0c3c;
						IL_04d2:
						num2 = 100;
						num16 = 1;
						goto IL_0800;
						IL_0c03:
						num2 = 182;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line should be separated from all other arguments with a space. E.g., @a@ /PAUSE", "@a@", "/PAUSE", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0c3c:
						num2 = 185;
						if (Operators.CompareString(text8, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/CONNECTRETRY") != 0)
						{
							goto IL_0c69;
						}
						goto IL_0ca2;
						IL_04de:
						num2 = 102;
						if (Operators.CompareString(Strings.Mid(text3, 1, 1), "/", TextCompare: false) == 0)
						{
							goto IL_0501;
						}
						goto IL_07e7;
						IL_0c69:
						num2 = 186;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty and must be numeric. Make sure you do not use spaces in assignment", "@a@", "/ConnectRetry", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0ca2:
						num2 = 189;
						if (Operators.CompareString(text9, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/RETRYTIME") != 0)
						{
							goto IL_0ccf;
						}
						goto IL_0d05;
						IL_0501:
						num2 = 103;
						num4 = Strings.InStr(text3, "=");
						goto IL_0513;
						IL_0ccf:
						num2 = 190;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty and must be numeric. Make sure you do not use spaces in assignment", "@a@", "/RetryTime", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0d05:
						num2 = 193;
						if (Operators.CompareString(text7, "", TextCompare: false) == 0 && Strings.InStr(MyArgTest, "/ORACLEARRAYSIZE") != 0)
						{
							goto IL_0d32;
						}
						goto IL_0d66;
						IL_0513:
						num2 = 104;
						text = "";
						goto IL_051d;
						IL_0d32:
						num2 = 194;
						MyErr = Conversions.ToString(DateAndTime.Now) + Strings.Replace(": The @a@ argument passed at the command line cannot be empty and must be numeric. Make sure you do not use spaces in assignment", "@a@", "/OrclArraySize", 1, -1, CompareMethod.Text);
						goto IL_262f;
						IL_0d66:
						num2 = 197;
						if (Operators.CompareString(text10, "", TextCompare: false) != 0)
						{
							goto IL_0d85;
						}
						goto IL_0e93;
						IL_0d85:
						num2 = 198;
						text10 = Environment.ExpandEnvironmentVariables(text10);
						goto IL_0d94;
						IL_0d94:
						num2 = 199;
						text10 = Strings.Trim(text10);
						goto IL_0da3;
						IL_0da3:
						num2 = 200;
						if (Operators.CompareString(Strings.Right(text10, 1), "\\", TextCompare: false) != 0)
						{
							goto IL_0dc5;
						}
						goto IL_0dd9;
						IL_0dc5:
						num2 = 201;
						text10 += "\\";
						goto IL_0dd9;
						IL_0dd9:
						num2 = 202;
						if (Strings.Len(text10) >= 2 && Operators.CompareString(Strings.Mid(text10, 2, 1), ":", TextCompare: false) == 0 && General_Procedures.MakeDirectory(text10, DoQuiet: true))
						{
							goto IL_0e10;
						}
						goto IL_0e67;
						IL_051d:
						num2 = 105;
						text2 = "";
						goto IL_0527;
						IL_0527:
						num2 = 106;
						if (num4 != 0)
						{
							goto IL_0538;
						}
						goto IL_0800;
						IL_0e10:
						num2 = 203;
						num15 = BuildForm.Copy_TNSNames(text10, IsQuiet: true, ref MyErr);
						goto IL_0e22;
						IL_0e22:
						num2 = 204;
						if (num15 == 0)
						{
							goto IL_0e33;
						}
						goto IL_262f;
						IL_0e33:
						num2 = 205;
						Globals_Renamed.MyPCDir = text10;
						goto IL_0e40;
						IL_0e40:
						num2 = 206;
						Globals_Renamed.gSavePCDir = false;
						goto IL_0e4c;
						IL_0e4c:
						num2 = 207;
						text18 = "/D ";
						goto IL_0ec5;
						IL_0e67:
						num2 = 212;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": Work Folder (" + text10 + ") is invalid for SQLPathFinder.";
						goto IL_262f;
						IL_0e93:
						num2 = 216;
						if (Operators.CompareString(text10, "", TextCompare: false) == 0)
						{
							goto IL_0eaf;
						}
						goto IL_0ec5;
						IL_0eaf:
						num2 = 217;
						num15 = BuildForm.Copy_TNSNames(Globals_Renamed.MyPCDir, IsQuiet: true, ref MyErr);
						goto IL_0ec5;
						IL_0ec5:
						num2 = 219;
						if ((Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0 && Operators.CompareString(left3, "", TextCompare: false) == 0) || Operators.CompareString(left3, "DEBUG", TextCompare: false) == 0)
						{
							goto IL_0f05;
						}
						goto IL_0f14;
						IL_0553:
						num2 = 108;
						text2 = Strings.Trim(Strings.Mid(text3, num4 + 1));
						goto IL_0569;
						IL_0538:
						num2 = 107;
						text = Strings.Trim(Strings.Mid(text3.ToUpper(), 1, num4 - 1));
						goto IL_0553;
						IL_0f05:
						num2 = 220;
						text20 = " /SPFLOGLEVEL=DEBUG";
						goto IL_0f14;
						IL_0f14:
						num2 = 222;
						MyQueryToProcess = Strings.Trim(MyQueryToProcess);
						goto IL_0f23;
						IL_0f23:
						num2 = 223;
						text11 = "";
						goto IL_0f30;
						IL_0f30:
						num2 = 224;
						if (Operators.CompareString(MyQueryToProcess, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0f55;
						IL_0f55:
						num2 = 227;
						MyQueryToProcess = Strings.Replace(MyQueryToProcess, "\"", "", 1, -1, CompareMethod.Text);
						goto IL_0f71;
						IL_0f71:
						num2 = 228;
						MyQueryToProcess = Strings.Replace(MyQueryToProcess, "\r\n", "", 1, -1, CompareMethod.Text);
						goto IL_0f8e;
						IL_0f8e:
						num2 = 230;
						MyQueryToProcess = Environment.ExpandEnvironmentVariables(MyQueryToProcess);
						goto IL_0f9d;
						IL_0f9d:
						num2 = 231;
						num4 = Strings.InStrRev(MyQueryToProcess, "\\", -1, CompareMethod.Text);
						goto IL_0fb3;
						IL_0fb3:
						num2 = 232;
						if (num4 != 0)
						{
							goto IL_0fc4;
						}
						goto IL_0fe0;
						IL_0fc4:
						num2 = 233;
						text4 = Strings.Trim(Strings.Mid(MyQueryToProcess, num4 + 1));
						goto IL_0ff0;
						IL_0fe0:
						num2 = 235;
						text4 = Strings.Trim(MyQueryToProcess);
						goto IL_0ff0;
						IL_0ff0:
						num2 = 237;
						num4 = Strings.InStrRev(text4, ".");
						goto IL_1006;
						IL_1006:
						num2 = 238;
						if (num4 != 0)
						{
							goto IL_101a;
						}
						goto IL_10d6;
						IL_101a:
						num2 = 239;
						text11 = Strings.Mid(text4, 1, num4 - 1);
						goto IL_102e;
						IL_102e:
						num2 = 240;
						text12 = Strings.Trim(Strings.UCase(Strings.Mid(text4, (int)Math.Round((double)num4 + 1.0))));
						goto IL_105a;
						IL_105a:
						num2 = 241;
						num7 = Strings.InStr(text12, "/");
						goto IL_106f;
						IL_106f:
						num2 = 242;
						if (num7 != 0)
						{
							goto IL_1080;
						}
						goto IL_1096;
						IL_1080:
						num2 = 243;
						text12 = Strings.Mid(text12, 1, num7 - 1);
						goto IL_1096;
						IL_1096:
						num2 = 245;
						num7 = Strings.InStr(text12, " ");
						goto IL_10ab;
						IL_10ab:
						num2 = 246;
						if (num7 != 0)
						{
							goto IL_10bc;
						}
						goto IL_10ee;
						IL_10bc:
						num2 = 247;
						text12 = Strings.Mid(text12, 1, num7 - 1);
						goto IL_10ee;
						IL_10d6:
						num2 = 250;
						text11 = text4;
						goto IL_10e0;
						IL_10e0:
						num2 = 251;
						text12 = "";
						goto IL_10ee;
						IL_10ee:
						num2 = 253;
						text17 = Strings.Replace(text11 + ".log2", " ", "", 1, -1, CompareMethod.Text);
						goto IL_1114;
						IL_1114:
						num2 = 254;
						if (num16 == 1)
						{
							goto IL_1125;
						}
						goto IL_115c;
						IL_1125:
						num2 = 255;
						if (File.Exists(Globals_Renamed.MyPCDir + text17))
						{
							goto IL_1142;
						}
						goto IL_115c;
						IL_1142:
						num2 = 256;
						File.Delete(Globals_Renamed.MyPCDir + text17);
						goto IL_115c;
						IL_115c:
						num2 = 258;
						if ((num9 == 0) & (Operators.CompareString(text5, "", TextCompare: false) == 0) & (Operators.CompareString(text6, "", TextCompare: false) == 0))
						{
							goto IL_118f;
						}
						goto IL_11b4;
						IL_118f:
						num2 = 259;
						if (!General_Procedures.SPF_Date_Chk_OK())
						{
							goto IL_11a3;
						}
						goto IL_11b4;
						IL_11a3:
						num2 = 260;
						result = 0;
						goto end_IL_0001_3;
						IL_11b4:
						num2 = 264;
						if (Operators.CompareString(MyQueryToProcess, "", TextCompare: false) != 0 && Strings.InStrRev(MyQueryToProcess, "\\") == 0)
						{
							goto IL_11e3;
						}
						goto IL_11f7;
						IL_0569:
						num2 = 109;
						switch (text)
						{
						case "/PACK":
							break;
						case "/JOB":
							goto IL_06ee;
						case "/SPFSQL":
							goto IL_06fb;
						case "/STARTDIR":
							goto IL_0708;
						case "/EXTENGINE":
							goto IL_0715;
						case "/SPFLOGLEVEL":
							goto IL_0727;
						case "/CONNECTRETRY":
							goto IL_0739;
						case "/RETRYTIME":
							goto IL_076f;
						case "/ORACLEARRAYSIZE":
							goto IL_07a5;
						default:
							goto IL_0800;
						}
						goto IL_06e1;
						IL_11e3:
						num2 = 265;
						MyQueryToProcess = Globals_Renamed.MyPCDir + MyQueryToProcess;
						goto IL_11f7;
						IL_11f7:
						num2 = 266;
						if (Operators.CompareString(text12, "VGE", TextCompare: false) == 0 || Operators.CompareString(text12, "VGEC", TextCompare: false) == 0)
						{
							goto IL_1225;
						}
						goto IL_1239;
						IL_07a5:
						num2 = 129;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Versioned.IsNumeric(text2))
						{
							goto IL_07ca;
						}
						goto IL_0800;
						IL_1225:
						num2 = 267;
						num9 = 1;
						goto IL_122e;
						IL_122e:
						num2 = 268;
						l_wMin = 1;
						goto IL_1239;
						IL_1239:
						num2 = 270;
						if (Operators.CompareString(text12, "VGEC", TextCompare: false) == 0)
						{
							goto IL_1258;
						}
						goto IL_1511;
						IL_1258:
						num2 = 271;
						text13 = MyProject.Computer.Clipboard.GetText();
						goto IL_126f;
						IL_126f:
						num2 = 272;
						text13 = Strings.LTrim(text13);
						goto IL_127e;
						IL_127e:
						num2 = 273;
						if (LikeOperator.LikeString(Strings.UCase(text13), "\r\n/CL_*", CompareMethod.Binary))
						{
							goto IL_129c;
						}
						goto IL_12f8;
						IL_129c:
						num2 = 274;
						text13 = Strings.Mid(text13, 3);
						goto IL_12ac;
						IL_12ac:
						num2 = 275;
						text13 = Strings.LTrim(text13);
						goto IL_12bb;
						IL_12bb:
						num2 = 276;
						if (Operators.CompareString(Strings.Right(text13, 2), "\r\n", TextCompare: false) == 0)
						{
							goto IL_12dd;
						}
						goto IL_12f8;
						IL_12dd:
						num2 = 277;
						text13 = Strings.Mid(text13, 1, Strings.Len(text13) - 2);
						goto IL_12f8;
						IL_12f8:
						num2 = 279;
						left = "N";
						goto IL_1305;
						IL_1305:
						num2 = 280;
						if (Operators.CompareString(text13, "", TextCompare: false) != 0 && LikeOperator.LikeString(Strings.UCase(text13), "/CL_*", CompareMethod.Binary) && Strings.Len(text13) > 4)
						{
							goto IL_1344;
						}
						goto IL_1511;
						IL_07e7:
						num2 = 134;
						MyQueryToProcess = text3;
						goto IL_0800;
						IL_07ca:
						num2 = 130;
						text7 = " /OracleArraySize=" + text2;
						goto IL_0800;
						IL_1344:
						num2 = 281;
						array = new string[0];
						goto IL_1352;
						IL_1352:
						num2 = 282;
						array = Strings.Split(text13, "&&");
						goto IL_1368;
						IL_1368:
						num2 = 283;
						num19 = Information.UBound(array);
						num12 = 0;
						goto IL_14c0;
						IL_14c0:
						if (num12 <= num19)
						{
							goto IL_1380;
						}
						goto IL_14c9;
						IL_14c9:
						num2 = 299;
						array = null;
						goto IL_14d2;
						IL_14d2:
						num2 = 300;
						array = null;
						goto IL_14db;
						IL_14db:
						num2 = 301;
						if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
						{
							goto IL_14f7;
						}
						goto IL_1511;
						IL_14f7:
						num2 = 302;
						MyProject.Computer.Clipboard.Clear();
						goto IL_1511;
						IL_1380:
						num2 = 284;
						text15 = Strings.LTrim(array[num12]);
						goto IL_1392;
						IL_1392:
						num2 = 285;
						num4 = Strings.InStr(text15, "=");
						goto IL_13a7;
						IL_13a7:
						num2 = 286;
						if (Operators.CompareString(text15, "", TextCompare: false) != 0 && Conversions.ToBoolean(Strings.UCase(Conversions.ToString(LikeOperator.LikeString(text15 + "     ", "/CL_*", CompareMethod.Binary)))) && num4 != 0)
						{
							goto IL_13f5;
						}
						goto IL_14b4;
						IL_076f:
						num2 = 126;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Versioned.IsNumeric(text2))
						{
							goto IL_0791;
						}
						goto IL_0800;
						IL_07f6:
						num2 = 137;
						flag = false;
						goto IL_0800;
						IL_13f5:
						num2 = 287;
						text = Strings.UCase(Strings.Mid(text15, 2, num4 - 2));
						goto IL_140e;
						IL_140e:
						num2 = 288;
						text2 = "";
						goto IL_141b;
						IL_141b:
						num2 = 289;
						if (Strings.Len(text15) > num4)
						{
							goto IL_1432;
						}
						goto IL_1445;
						IL_1432:
						num2 = 290;
						text2 = Strings.Mid(text15, num4 + 1);
						goto IL_1445;
						IL_1445:
						num2 = 291;
						if (Operators.CompareString(text, "/CL_", TextCompare: false) != 0)
						{
							goto IL_1461;
						}
						goto IL_14b4;
						IL_1461:
						num2 = 292;
						left = "Y";
						goto IL_146e;
						IL_146e:
						num2 = 293;
						Globals_Renamed.g_VarNo++;
						goto IL_1480;
						IL_1480:
						num2 = 294;
						Globals_Renamed.g_GVars[0, Globals_Renamed.g_VarNo] = text;
						goto IL_1498;
						IL_1498:
						num2 = 295;
						Globals_Renamed.g_GVars[1, Globals_Renamed.g_VarNo] = text2;
						goto IL_14b4;
						IL_14b4:
						num2 = 298;
						num12++;
						goto IL_14c0;
						IL_1511:
						num2 = 305;
						if (Operators.CompareString(text6, "", TextCompare: false) != 0)
						{
							goto IL_1530;
						}
						goto IL_15e7;
						IL_1530:
						num2 = 306;
						switch (text12)
						{
						case "VGQ":
						case "VG2":
						case "VGE":
							goto IL_157c;
						}
						if (Operators.CompareString(text12, "VGEC", TextCompare: false) == 0)
						{
							goto IL_157c;
						}
						goto IL_24a6;
						IL_0791:
						num2 = 127;
						text9 = " /RetryTime=" + text2;
						goto IL_0800;
						IL_157c:
						num2 = 307;
						num9 = 1;
						goto IL_1585;
						IL_1585:
						num2 = 308;
						text6 = Environment.ExpandEnvironmentVariables(text6);
						goto IL_1594;
						IL_1594:
						num2 = 309;
						text6 = Strings.Trim(text6);
						goto IL_15a3;
						IL_15a3:
						num2 = 310;
						if (Strings.InStrRev(text6, "\\") == 0)
						{
							goto IL_15c0;
						}
						goto IL_15e7;
						IL_15c0:
						num2 = 311;
						text6 = Globals_Renamed.MyPCDir + Strings.Trim(text6);
						goto IL_15e7;
						IL_24a6:
						num2 = 485;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": The SQLPathFinder Query (" + MyQueryToProcess + ") could not be processed. You can only SAVE SPFSQL files for queries with file extensions: vg2, vgq, vge, or vgec." + Globals_Renamed.CRLF + Globals_Renamed.CRLF + MyErr;
						goto IL_262f;
						IL_15e7:
						num2 = 317;
						if (unchecked((Operators.CompareString(text12, "VGQ", TextCompare: false) == 0 || Operators.CompareString(text12, "VG2", TextCompare: false) == 0) && num9 == 0))
						{
							goto IL_161b;
						}
						goto IL_1643;
						IL_0800:
						num2 = 139;
						num18++;
						goto IL_080c;
						IL_075b:
						num2 = 124;
						text8 = " /ConnectRetry=" + text2;
						goto IL_0800;
						IL_0739:
						num2 = 123;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Versioned.IsNumeric(text2))
						{
							goto IL_075b;
						}
						goto IL_0800;
						IL_161b:
						num2 = 318;
						num13 = OpenQuery(MyQueryToProcess, 1);
						goto IL_162c;
						IL_162c:
						num2 = 319;
						Application.DoEvents();
						goto end_IL_0001_3;
						IL_1643:
						num2 = 322;
						if (unchecked(Operators.CompareString(text12, "SPFSQL", TextCompare: false) == 0 && num9 == 0))
						{
							goto IL_1665;
						}
						goto IL_168c;
						IL_1665:
						num2 = 323;
						MyData2 = "";
						BuildForm.HG_Editor(ref MyData2, "SQL->VA", MyQueryToProcess);
						goto end_IL_0001_3;
						IL_168c:
						num2 = 326;
						if (unchecked(Operators.CompareString(text12, "SPF", TextCompare: false) == 0 && num9 == 0))
						{
							goto IL_16b1;
						}
						goto IL_17fe;
						IL_16b1:
						num2 = 327;
						num13 = Invoke_MQ_External(MyQueryToProcess, MyQueryToProcess, ref PackedExe);
						goto IL_16c6;
						IL_16c6:
						num2 = 328;
						Application.DoEvents();
						goto IL_16d2;
						IL_16d2:
						num2 = 329;
						if (num13 == 0)
						{
							goto IL_16e3;
						}
						goto IL_1739;
						IL_16e3:
						num2 = 330;
						MyErr = "SQLPathFinder Error During Batch Execution...";
						goto IL_16f0;
						IL_16f0:
						num2 = 331;
						MyErr = MyErr + Globals_Renamed.CRLF + Globals_Renamed.CRLF + "Error opening file " + MyQueryToProcess + " which was passed as an input argument to SQLPathFinder.";
						goto IL_262f;
						IL_1739:
						num2 = 334;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_1758;
						IL_1758:
						num2 = 335;
						Cursor.Current = Cursors.WaitCursor;
						goto IL_1769;
						IL_1769:
						num2 = 336;
						FrmMultiQuery.F_FormMode = "QUIET";
						goto IL_177f;
						IL_177f:
						num2 = 337;
						FrmMultiQuery.OptRun1.Checked = true;
						goto IL_1797;
						IL_1797:
						num2 = 338;
						FrmMultiQuery.f_Job = f_Job;
						goto IL_17aa;
						IL_17aa:
						num2 = 339;
						FrmMultiQuery.PrepSaveSHGUI(0, text5);
						goto IL_17c0;
						IL_17c0:
						num2 = 340;
						FrmMultiQuery.Close();
						goto IL_17d2;
						IL_17d2:
						num2 = 341;
						Cursor.Current = Cursors.Default;
						goto IL_17e3;
						IL_17e3:
						num2 = 342;
						result = 2;
						break;
						IL_17fe:
						num2 = 348;
						if (unchecked(Operators.CompareString(text12, "SPF", TextCompare: false) == 0 && num9 == 1))
						{
							goto IL_1823;
						}
						goto IL_1ad4;
						IL_1823:
						num2 = 349;
						Hide();
						goto IL_1830;
						IL_1830:
						num2 = 350;
						if (Operators.CompareString(left2, "VA", TextCompare: false) == 0 || BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: true, 1))
						{
							goto IL_1861;
						}
						goto IL_2546;
						IL_0727:
						num2 = 121;
						left3 = Strings.UCase(text2);
						goto IL_0800;
						IL_1861:
						num2 = 353;
						num13 = Invoke_MQ_External(MyQueryToProcess, MyQueryToProcess, ref PackedExe, QuietMode: true);
						goto IL_1876;
						IL_1876:
						num2 = 354;
						Application.DoEvents();
						goto IL_1882;
						IL_1882:
						num2 = 355;
						if (num13 == 0)
						{
							goto IL_1893;
						}
						goto IL_18e9;
						IL_1893:
						num2 = 356;
						MyErr = "SQLPathFinder Error During Batch Execution...";
						goto IL_18a0;
						IL_18a0:
						num2 = 357;
						MyErr = MyErr + Globals_Renamed.CRLF + Globals_Renamed.CRLF + "Error opening file " + MyQueryToProcess + " which was passed as an input argument to SQLPathFinder.";
						goto IL_262f;
						IL_18e9:
						num2 = 360;
						FrmMultiQuery.F_FormMode = "QUIET";
						goto IL_18ff;
						IL_18ff:
						num2 = 361;
						FrmMultiQuery.OptRun0.Checked = true;
						goto IL_1917;
						IL_1917:
						num2 = 362;
						Globals_Renamed.gWorkQuery = "";
						goto IL_1927;
						IL_1927:
						num2 = 363;
						FrmMultiQuery.Hide();
						goto IL_1939;
						IL_1939:
						num2 = 364;
						if (Operators.CompareString(left2, "VA", TextCompare: false) == 0)
						{
							goto IL_1955;
						}
						goto IL_1969;
						IL_1955:
						num2 = 365;
						Globals_Renamed.gUsePyEngineOVR = "N";
						goto IL_197a;
						IL_1969:
						num2 = 367;
						Globals_Renamed.gUsePyEngineOVR = "Y";
						goto IL_197a;
						IL_197a:
						num2 = 369;
						num13 = FrmMultiQuery.SaveScript(0, "", 0, IsPacked: true, QuietMode: true);
						goto IL_19a0;
						IL_19a0:
						num2 = 370;
						FrmMultiQuery.Close();
						goto IL_19b2;
						IL_19b2:
						num2 = 371;
						text15 = General_Procedures.Set_ORCL_Path();
						goto IL_19bf;
						IL_19bf:
						num2 = 372;
						Globals_Renamed.gWorkQuery = Strings.Replace(Globals_Renamed.gWorkQuery, "\r\n", "&&", 1, -1, CompareMethod.Text);
						goto IL_19e1;
						IL_19e1:
						num2 = 373;
						text16 = "cmd /c \"cd " + text18 + Strings.Trim(Globals_Renamed.MyPCDir) + text15 + "&&" + Globals_Renamed.gWorkQuery + "&&.\\runspf.exe";
						goto IL_1a2b;
						IL_1a2b:
						num2 = 374;
						if (num16 == 1)
						{
							goto IL_1a3c;
						}
						goto IL_1a52;
						IL_1a3c:
						num2 = 375;
						text16 = text16 + " >>" + text17;
						goto IL_1a52;
						IL_1a52:
						num2 = 376;
						if (num11 == 1)
						{
							goto IL_1a63;
						}
						goto IL_1a77;
						IL_1a63:
						num2 = 377;
						text16 += "&&Pause&&Pause";
						goto IL_1a77;
						IL_1a77:
						num2 = 378;
						text16 += "\"";
						goto IL_1a8b;
						IL_1a8b:
						num2 = 379;
						MyErr = General_Procedures.Run_Batch_Shell(text16, num10, l_wMin, 0);
						goto IL_1a9f;
						IL_1a9f:
						num2 = 380;
						if (Operators.CompareString(MyErr, "", TextCompare: false) == 0)
						{
							goto IL_1ac0;
						}
						goto IL_24f6;
						IL_1ac0:
						num2 = 382;
						result = 2;
						break;
						IL_1ad4:
						num2 = 386;
						switch (text12)
						{
						case "VGQ":
						case "VG2":
						case "VGE":
						case "VGEC":
						case "SPFSQL":
							break;
						default:
							goto IL_2283;
						}
						if (num9 == 1)
						{
							goto IL_1b36;
						}
						goto IL_2283;
						IL_0715:
						num2 = 119;
						left2 = Strings.UCase(text2);
						goto IL_0800;
						IL_1b36:
						num2 = 387;
						if (Operators.CompareString(left2, "VA", TextCompare: false) == 0)
						{
							goto IL_1b52;
						}
						goto IL_1b66;
						IL_1b52:
						num2 = 388;
						Globals_Renamed.gUsePyEngineOVR = "N";
						goto IL_1b77;
						IL_1b66:
						num2 = 390;
						Globals_Renamed.gUsePyEngineOVR = "Y";
						goto IL_1b77;
						IL_1b77:
						num2 = 392;
						Hide();
						goto IL_1b84;
						IL_1b84:
						num2 = 393;
						ContainsMacro = false;
						goto IL_1b8d;
						IL_1b8d:
						num2 = 394;
						text14 = text6;
						goto IL_1b97;
						IL_1b97:
						num2 = 395;
						switch (text12)
						{
						case "VG2":
						case "VGQ":
						case "VGE":
							goto IL_1be3;
						}
						if (Operators.CompareString(text12, "VGEC", TextCompare: false) == 0)
						{
							goto IL_1be3;
						}
						goto IL_1c36;
						IL_0708:
						num2 = 117;
						text10 = text2;
						goto IL_0800;
						IL_1be3:
						num2 = 396;
						if (Operators.CompareString(text6, "", TextCompare: false) == 0)
						{
							goto IL_1bff;
						}
						goto IL_1c1b;
						IL_1bff:
						num2 = 397;
						text14 = Strings.Trim(Globals_Renamed.MyPCDir) + "SQLPathFinder.va";
						goto IL_1c1b;
						IL_1c1b:
						num2 = 398;
						num13 = Save_To_VA(ref MyQueryToProcess, text14, 1, ref ContainsMacro, ref MyInLineValue);
						goto IL_1c86;
						IL_1c36:
						num2 = 400;
						if (Operators.CompareString(text6, "", TextCompare: false) == 0)
						{
							goto IL_1c52;
						}
						goto IL_1c6e;
						IL_1c52:
						num2 = 401;
						text14 = Strings.Trim(Globals_Renamed.MyPCDir) + "_SQLPathFinder_.spfsql";
						goto IL_1c6e;
						IL_1c6e:
						num2 = 402;
						num13 = Save_To_VA(ref MyQueryToProcess, text14, 2, ref ContainsMacro, ref MyInLineValue);
						goto IL_1c86;
						IL_1c86:
						num2 = 404;
						if (num13 != 0)
						{
							goto IL_1c9f;
						}
						goto IL_24f6;
						IL_1c9f:
						num2 = 407;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) != 0 || BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: true, 1))
						{
							goto IL_1cd2;
						}
						goto IL_2546;
						IL_06fb:
						num2 = 115;
						text6 = text2;
						goto IL_0800;
						IL_2546:
						num2 = 489;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": The SQLPathFinder Query (" + MyQueryToProcess + ") could not be processed." + Globals_Renamed.CRLF + Globals_Renamed.CRLF + MyErr;
						goto IL_262f;
						IL_1cd2:
						num2 = 410;
						if (Operators.CompareString(text6, "", TextCompare: false) == 0)
						{
							goto IL_1cf1;
						}
						goto IL_23c7;
						IL_1cf1:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_1cf9;
						IL_1cf9:
						num2 = 412;
						num6 = FileSystem.FreeFile();
						goto IL_1d06;
						IL_1d06:
						num2 = 413;
						FileSystem.FileOpen(num6, Strings.Trim(Globals_Renamed.MyPCDir) + text11 + ".spf", OpenMode.Output);
						goto IL_1d2e;
						IL_1d2e:
						num2 = 414;
						switch (text12)
						{
						case "VGQ":
						case "VG2":
						case "VGE":
							goto IL_1d7d;
						}
						if (Operators.CompareString(text12, "VGEC", TextCompare: false) == 0)
						{
							goto IL_1d7d;
						}
						goto IL_1e3e;
						IL_06ee:
						num2 = 113;
						f_Job = text2;
						goto IL_0800;
						IL_1d7d:
						num2 = 415;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
						{
							goto IL_1d9c;
						}
						goto IL_1df3;
						IL_1d9c:
						num2 = 416;
						FileSystem.PrintLine(num6, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.va /SPFSQL=\"" + text14 + "\"" + text19);
						goto IL_1e92;
						IL_1df3:
						num2 = 418;
						FileSystem.PrintLine(num6, "1," + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.va " + General_Procedures.CompressString(File.ReadAllText(text14)));
						goto IL_1e92;
						IL_1e3e:
						num2 = 421;
						FileSystem.PrintLine(num6, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.va \"" + text14 + "\"" + text19);
						goto IL_1e92;
						IL_1e92:
						num2 = 423;
						FileSystem.FileClose(num6);
						goto IL_1ea9;
						IL_1ea9:
						num2 = 424;
						text15 = General_Procedures.Set_ORCL_Path();
						goto IL_1eb6;
						IL_1eb6:
						num2 = 425;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
						{
							goto IL_1ed8;
						}
						goto IL_1f6d;
						IL_1ed8:
						num2 = 426;
						text16 = "cmd /c \"cd " + text18 + Strings.Trim(Globals_Renamed.MyPCDir) + text15 + "&&va \"" + MyProject.Application.Info.DirectoryPath + "\\Run_Multiple_SQLPathFinder.va\" ";
						goto IL_1f2c;
						IL_1f2c:
						num2 = 427;
						text16 = text16 + "\"" + Strings.Trim(Globals_Renamed.MyPCDir) + text11 + ".spf\"";
						goto IL_2039;
						IL_1f6d:
						num2 = 429;
						text16 = "cmd /c \"cd " + text18 + Strings.Trim(Globals_Renamed.MyPCDir) + text15 + "&&\"" + Globals_Renamed.gMyPyPath + "\" -s -u \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.py\" ";
						goto IL_1fd7;
						IL_1fd7:
						num2 = 430;
						text16 = text16 + "\"" + Strings.Trim(Globals_Renamed.MyPCDir) + text11 + ".spf\"" + text19 + text20 + text8 + text9 + text7 + text21 + text22;
						goto IL_2039;
						IL_2039:
						num2 = 432;
						if (num16 == 1)
						{
							goto IL_204a;
						}
						goto IL_2060;
						IL_204a:
						num2 = 433;
						text16 = text16 + " >>" + text17;
						goto IL_2060;
						IL_2060:
						num2 = 434;
						if (num11 == 1)
						{
							goto IL_2071;
						}
						goto IL_2085;
						IL_2071:
						num2 = 435;
						text16 += "&&Pause&&Pause";
						goto IL_2085;
						IL_2085:
						num2 = 436;
						text16 += "\"";
						goto IL_2099;
						IL_2099:
						num2 = 437;
						if (num16 == 1)
						{
							goto IL_20aa;
						}
						goto IL_2104;
						IL_20aa:
						num2 = 438;
						MyData2 = "BATCH-LOG";
						lpKeyName = Conversions.ToString(num17);
						lpString = Conversions.ToString(DateAndTime.Now) + " : Command to run: " + text16;
						lpFileName = Globals_Renamed.MyPCDir + text17;
						num13 = (short)Globals_Renamed.WritePrivateProfileString(ref MyData2, ref lpKeyName, ref lpString, ref lpFileName);
						goto IL_20f6;
						IL_20f6:
						num2 = 439;
						num17++;
						goto IL_2104;
						IL_2104:
						num2 = 441;
						if (num16 == 1)
						{
							goto IL_2115;
						}
						goto IL_216d;
						IL_2115:
						num2 = 442;
						lpFileName = "BATCH-LOG";
						lpString = Conversions.ToString(num17);
						lpKeyName = Conversions.ToString(DateAndTime.Now) + " : Job Log follows ...";
						MyData2 = Globals_Renamed.MyPCDir + text17;
						num13 = (short)Globals_Renamed.WritePrivateProfileString(ref lpFileName, ref lpString, ref lpKeyName, ref MyData2);
						goto IL_215f;
						IL_215f:
						num2 = 443;
						num17++;
						goto IL_216d;
						IL_216d:
						num2 = 445;
						MyErr = General_Procedures.Run_Batch_Shell(text16, num10, l_wMin, 0);
						goto IL_2181;
						IL_2181:
						num2 = 446;
						if (Operators.CompareString(MyErr, "", TextCompare: false) != 0)
						{
							goto IL_219d;
						}
						goto IL_2211;
						IL_219d:
						num2 = 447;
						if (num16 == 1)
						{
							goto IL_21ae;
						}
						goto IL_24f6;
						IL_21ae:
						num2 = 448;
						MyData2 = "BATCH-LOG";
						lpKeyName = Conversions.ToString(num17);
						lpString = Conversions.ToString(DateAndTime.Now) + " : Command Return message: " + MyErr;
						lpFileName = Globals_Renamed.MyPCDir + text17;
						num13 = (short)Globals_Renamed.WritePrivateProfileString(ref MyData2, ref lpKeyName, ref lpString, ref lpFileName);
						goto IL_21fa;
						IL_21fa:
						num2 = 449;
						num17++;
						goto IL_24f6;
						IL_2211:
						num2 = 453;
						if (num16 == 1)
						{
							goto IL_2222;
						}
						goto IL_23c7;
						IL_2222:
						num2 = 454;
						lpFileName = "BATCH-LOG";
						lpString = Conversions.ToString(num17);
						lpKeyName = Conversions.ToString(DateAndTime.Now) + " : Command Return message: No Message Returned";
						MyData2 = Globals_Renamed.MyPCDir + text17;
						num13 = (short)Globals_Renamed.WritePrivateProfileString(ref lpFileName, ref lpString, ref lpKeyName, ref MyData2);
						goto IL_226c;
						IL_226c:
						num2 = 455;
						num17++;
						goto IL_23c7;
						IL_2283:
						num2 = 460;
						if (unchecked(Operators.CompareString(text12, "VA", TextCompare: false) == 0 && num9 == 0))
						{
							goto IL_22a5;
						}
						goto IL_22cc;
						IL_22a5:
						num2 = 461;
						MyData2 = "";
						BuildForm.HG_Editor(ref MyData2, "VA", MyQueryToProcess);
						goto end_IL_0001_3;
						IL_22cc:
						num2 = 464;
						if (unchecked(Operators.CompareString(text12, "VA", TextCompare: false) == 0 && num9 == 1))
						{
							goto IL_22f1;
						}
						goto IL_25e2;
						IL_22f1:
						num2 = 465;
						MyErr = General_Procedures.GetFileContents(MyQueryToProcess, ref MyData);
						goto IL_2302;
						IL_2302:
						num2 = 466;
						if (Operators.CompareString(MyErr, "", TextCompare: false) == 0)
						{
							goto IL_2323;
						}
						goto IL_23e6;
						IL_23e6:
						num2 = 479;
						MyErr = "SQLPathFinder Error During Batch Execution...\r\n\r\nError opening file " + MyQueryToProcess + " which was passed as an input argument to SQLPathFinder. (" + MyErr + ")";
						goto IL_262f;
						IL_2323:
						num2 = 468;
						BuildSQL.RunQuery(MyData, 3, 1, -99, "");
						goto IL_233a;
						IL_233a:
						num2 = 469;
						text16 = "cmd /c \"cd " + text18 + Strings.Trim(Globals_Renamed.MyPCDir) + "&&Echo.&&Echo Output will be written to " + text11 + ".log&&Echo.&&Echo Query Running...&&Echo.&&va SQLPathFinder.va >" + text11 + ".log \"";
						goto IL_2389;
						IL_2389:
						num2 = 470;
						MyErr = General_Procedures.Run_Batch_Shell(text16, 0, l_wMin, 0);
						goto IL_239c;
						IL_239c:
						num2 = 471;
						if (Operators.CompareString(MyErr, "", TextCompare: false) != 0)
						{
							goto IL_24f6;
						}
						goto IL_23c7;
						IL_24f6:
						num2 = 487;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": The SQLPathFinder Query (" + MyQueryToProcess + ") could not be processed. Do check the file and the file path before resubmitting." + Globals_Renamed.CRLF + Globals_Renamed.CRLF + MyErr;
						goto IL_262f;
						IL_23c7:
						num2 = 476;
						if (num9 != 1)
						{
							break;
						}
						goto IL_23d8;
						IL_23d8:
						num2 = 477;
						result = 2;
						break;
						IL_25e2:
						num2 = 493;
						MyErr = Conversions.ToString(DateAndTime.Now) + ": Query (" + MyQueryToProcess + ") could not be processed. The file extension was not recognized. SQLPathFinder supports .vgq, .vg2, .vge, .vgec, .spf, .spfsql, or .VA exensions" + Globals_Renamed.CRLF + Globals_Renamed.CRLF + MyErr;
						goto IL_262f;
						IL_262f:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_2638;
						IL_2638:
						num2 = 496;
						num6 = FileSystem.FreeFile();
						goto IL_2645;
						IL_2645:
						num2 = 497;
						FileSystem.FileOpen(num6, Strings.Trim(Globals_Renamed.MyPCDir) + text11 + ".log", OpenMode.Output);
						goto IL_266d;
						IL_266d:
						num2 = 498;
						FileSystem.PrintLine(num6, MyErr);
						goto IL_2686;
						IL_2686:
						num2 = 499;
						FileSystem.FileClose(num6);
						goto IL_269d;
						IL_269d:
						num2 = 500;
						result = 0;
						break;
						IL_027a:
						num2 = 64;
						text3 = Strings.Trim(text3);
						goto IL_0286;
						IL_0286:
						num2 = 65;
						switch (text3)
						{
						case null:
						case "":
						case "\"\"":
							goto IL_0800;
						}
						if (Operators.CompareString(text3, "\"", TextCompare: false) != 0)
						{
							goto IL_02c6;
						}
						goto IL_0800;
						IL_06e1:
						num2 = 111;
						text5 = text2;
						goto IL_0800;
						IL_02c6:
						num2 = 67;
						if (Operators.CompareString(Strings.UCase(Strings.Mid(text3 + "     ", 1, 4)), "/CL_", TextCompare: false) == 0)
						{
							goto IL_02f8;
						}
						goto IL_040c;
						IL_02f8:
						num2 = 68;
						text3 = Strings.Trim(Strings.Mid(text3, 2));
						goto IL_030a;
						IL_030a:
						num2 = 69;
						num4 = Strings.InStr(text3, "=");
						goto IL_031c;
						IL_031c:
						num2 = 70;
						if (num4 == 0)
						{
							goto IL_032a;
						}
						goto IL_0341;
						IL_032a:
						num2 = 71;
						MyErr = "Invalid \"/CL_\" argument passed. Missing \"=\" assignment. Argument phrase being parsed: /" + text3;
						goto IL_0800;
						IL_0341:
						num2 = 73;
						if (num4 == Strings.Len(text3))
						{
							goto IL_0355;
						}
						goto IL_036d;
						IL_0355:
						num2 = 74;
						MyErr = "\"/CL_\" argument found without an argument value. Do make sure there are no spaces following the = assignment, and that the argument value is not empty. Argument phrase being parsed: /" + text3;
						goto IL_0800;
						IL_036d:
						num2 = 76;
						text = Strings.LCase(Strings.Trim(Strings.Mid(text3, 1, num4 - 1)));
						goto IL_0388;
						IL_0388:
						num2 = 77;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_03a1;
						}
						goto IL_03b6;
						IL_03a1:
						num2 = 78;
						MyErr = "\"/CL_\" by itself is not allowed as an argument. Argument phrase being parsed: /" + text3;
						goto IL_0800;
						end_IL_0001_2:
						break;
					}
					num2 = 502;
					Application.DoEvents();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 11941;
				continue;
			}
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

	public void LoadComputedColumnForm(short MyTab, string MyType, string MyAlias, string MyDataType, string MyList, short MyRow)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmComputedSQL frmComputedSQL = default(FrmComputedSQL);
		while (true)
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
				case 359:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0018;
						case 4:
							goto IL_0020;
						case 5:
							goto IL_0028;
						case 6:
							goto IL_0031;
						case 7:
							goto IL_0039;
						case 8:
							goto IL_0050;
						case 9:
							goto IL_0059;
						case 10:
							goto IL_0063;
						case 11:
							goto IL_006d;
						case 12:
							goto IL_0076;
						case 13:
							goto IL_0080;
						case 14:
							goto IL_008b;
						case 15:
							goto IL_0096;
						case 16:
							goto IL_009f;
						case 17:
							goto IL_00bc;
						case 18:
							goto IL_00c9;
						case 19:
							goto IL_00d6;
						case 20:
							goto IL_00df;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00df:
					num2 = 20;
					Globals_Renamed.currlisttmp = "";
					break;
					IL_00bc:
					num2 = 17;
					Globals_Renamed.currDataAny = "";
					goto IL_00c9;
					IL_00d6:
					num2 = 19;
					Globals_Renamed.currinttmp = 0;
					goto IL_00df;
					IL_00c9:
					num2 = 18;
					Globals_Renamed.currdatatypetmp = "";
					goto IL_00d6;
					IL_000b:
					num2 = 2;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0018;
					IL_0018:
					num2 = 3;
					Globals_Renamed.currtxttmp = MyType;
					goto IL_0020;
					IL_0020:
					num2 = 4;
					Globals_Renamed.currDataAny = MyAlias;
					goto IL_0028;
					IL_0028:
					num2 = 5;
					Globals_Renamed.currdatatypetmp = MyDataType;
					goto IL_0031;
					IL_0031:
					num2 = 6;
					Globals_Renamed.currinttmp = MyTab;
					goto IL_0039;
					IL_0039:
					num2 = 7;
					if (BuildForm.IsColPattern(Globals_Renamed.currdatatypetmp))
					{
						goto IL_0050;
					}
					goto IL_0059;
					IL_0050:
					num2 = 8;
					Globals_Renamed.currinttmp = 9;
					goto IL_0059;
					IL_0059:
					num2 = 9;
					Globals_Renamed.currlisttmp = MyList;
					goto IL_0063;
					IL_0063:
					num2 = 10;
					Globals_Renamed.currrowtmp = MyRow;
					goto IL_006d;
					IL_006d:
					num2 = 11;
					frmComputedSQL = new FrmComputedSQL();
					goto IL_0076;
					IL_0076:
					num2 = 12;
					frmComputedSQL.ShowDialog();
					goto IL_0080;
					IL_0080:
					num2 = 13;
					base.TopMost = true;
					goto IL_008b;
					IL_008b:
					num2 = 14;
					base.TopMost = false;
					goto IL_0096;
					IL_0096:
					num2 = 15;
					Application.DoEvents();
					goto IL_009f;
					IL_009f:
					num2 = 16;
					if (MyTab != 6 && MyTab != 10 && MyTab != 11)
					{
						goto IL_00bc;
					}
					goto IL_00c9;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 359;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void OpenMenuFile(string Index)
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
				short design_Mode;
				short num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 550:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 7:
							goto IL_0062;
						case 9:
							goto IL_0074;
						case 11:
							goto IL_0087;
						case 13:
							goto IL_009a;
						case 6:
						case 8:
						case 10:
						case 12:
						case 14:
						case 15:
							goto IL_00ad;
						case 17:
							goto IL_00cc;
						case 19:
							goto IL_00e7;
						case 21:
							goto IL_00fa;
						case 22:
							goto IL_010b;
						case 23:
						case 24:
							goto IL_0116;
						case 25:
							goto IL_0120;
						case 26:
							goto IL_0129;
						case 27:
							goto IL_013e;
						case 28:
							goto IL_014f;
						case 29:
							goto IL_0161;
						case 30:
							goto IL_016e;
						case 31:
						case 32:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
						case 18:
						case 20:
						case 33:
						case 34:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_014f:
					num2 = 28;
					FrmSQLQuery[num5].Dispose();
					goto IL_0161;
					IL_0161:
					num2 = 29;
					FrmSQLQuery[num5] = null;
					goto IL_016e;
					IL_013e:
					num2 = 27;
					if (Globals_Renamed.QueryCancel != 0)
					{
						break;
					}
					goto IL_014f;
					IL_016e:
					num2 = 30;
					num6 = OpenQuery(text, 1);
					break;
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
					goto IL_001e;
					IL_001e:
					num2 = 5;
					switch (Index)
					{
					case "0":
						break;
					case "1":
						goto IL_0074;
					case "2":
						goto IL_0087;
					case "3":
						goto IL_009a;
					default:
						goto IL_00ad;
					}
					goto IL_0062;
					IL_009a:
					num2 = 13;
					text = MnuFile3.Text;
					goto IL_00ad;
					IL_0087:
					num2 = 11;
					text = MnuFile2.Text;
					goto IL_00ad;
					IL_0074:
					num2 = 9;
					text = MnuFile1.Text;
					goto IL_00ad;
					IL_0062:
					num2 = 7;
					text = MnuFile0.Text;
					goto IL_00ad;
					IL_00ad:
					num2 = 15;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00cc;
					IL_00cc:
					num2 = 17;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_00e7;
					}
					if (design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_00fa;
					IL_00e7:
					num2 = 19;
					num6 = OpenQuery(text, 1);
					goto end_IL_0001_3;
					IL_00fa:
					num2 = 21;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_010b;
					}
					goto IL_0116;
					IL_010b:
					num2 = 22;
					Globals_Renamed.g_PrePostQueryOpen = true;
					goto IL_0116;
					IL_0116:
					num2 = 24;
					num5 = Globals_Renamed.g_FrmIdx;
					goto IL_0120;
					IL_0120:
					num2 = 25;
					Globals_Renamed.QueryCancel = 0;
					goto IL_0129;
					IL_0129:
					num2 = 26;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_013e;
					end_IL_0001_2:
					break;
				}
				num2 = 32;
				Globals_Renamed.g_PrePostQueryOpen = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 550;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Confirm_DBType_Setting(string MyOptDB, ref string ll_DBType, ref short ll_ObjectType)
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
					goto IL_000c;
				case 735:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_01d2;
						case 5:
							goto IL_01d7;
						case 6:
							goto IL_01dc;
						case 7:
							goto IL_0204;
						case 10:
							goto IL_0212;
						case 11:
							goto IL_0218;
						case 13:
							goto IL_0225;
						case 14:
							goto IL_022b;
						case 16:
							goto IL_0238;
						case 17:
							goto IL_023e;
						case 3:
						case 8:
						case 9:
						case 12:
						case 15:
						case 18:
						case 19:
							goto IL_024b;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01dc:
					num2 = 6;
					if (Operators.CompareString(MyOptDB, "B", TextCompare: false) == 0 || Operators.CompareString(MyOptDB, "C", TextCompare: false) == 0)
					{
						goto IL_0204;
					}
					goto IL_024b;
					IL_024b:
					num2 = 19;
					MnuAddTables.Enabled = true;
					break;
					IL_01d7:
					num2 = 5;
					ll_DBType = MyOptDB;
					goto IL_01dc;
					IL_0204:
					num2 = 7;
					ll_DBType = "9";
					goto IL_024b;
					IL_000c:
					num2 = 2;
					switch (MyOptDB)
					{
					case "0":
					case "1":
					case "2":
					case "3":
					case "5":
					case "9":
					case "A":
					case "B":
					case "C":
						break;
					case "4":
						goto IL_0212;
					case "6":
						goto IL_0225;
					case "7":
						goto IL_0238;
					default:
						goto IL_024b;
					}
					goto IL_01d2;
					IL_0238:
					num2 = 16;
					ll_ObjectType = 1;
					goto IL_023e;
					IL_023e:
					num2 = 17;
					ll_DBType = "0";
					goto IL_024b;
					IL_0225:
					num2 = 13;
					ll_ObjectType = 3;
					goto IL_022b;
					IL_022b:
					num2 = 14;
					ll_DBType = "0";
					goto IL_024b;
					IL_0212:
					num2 = 10;
					ll_ObjectType = 4;
					goto IL_0218;
					IL_0218:
					num2 = 11;
					ll_DBType = "0";
					goto IL_024b;
					IL_01d2:
					num2 = 4;
					ll_ObjectType = 2;
					goto IL_01d7;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				cmdIcon.Items[5].Enabled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 735;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public bool Add_Utility_or_Query_To_Tree(string MyTable, string MyOptDB, ref string ll_DBType, ref short ll_ObjectType, ref string MyNewAlias)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		bool result = default(bool);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string lSQLiteTable;
				string text5;
				string text8;
				string MyDataTag;
				short num6;
				string MyFile;
				string text7;
				string l_TableOpts;
				string text9;
				string text2;
				short num7;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "frmMain - Add_Utility_or_Query_To_Tree";
					MyFile = "";
					string text = "";
					text2 = "";
					string text3 = "";
					int num3 = 0;
					lSQLiteTable = "";
					short num4 = 0;
					int num5 = 0;
					num6 = 0;
					num7 = 0;
					string text4 = "";
					text5 = "";
					short num8 = 0;
					string text6 = "";
					text7 = "";
					text8 = "";
					text9 = "";
					short num9 = 0;
					l_TableOpts = "Y";
					TreeNode treeNode = null;
					TreeNode treeNode2 = null;
					MyDataTag = "";
					string text10 = "N";
					int num10 = 0;
					string text11 = "";
					string text12 = "";
					result = false;
					MyNewAlias = "";
					Cursor.Current = Cursors.WaitCursor;
					if (Operators.CompareString(MyOptDB, "8", TextCompare: false) == 0)
					{
						if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes.Count < 1)
						{
							goto IL_0162;
						}
						if (!FrmSQLQuery[Globals_Renamed.g_FrmIdx].MainQInTree)
						{
							num3 = 1;
						}
						if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].f_NoUtilities <= checked(100 - num3))
						{
							goto IL_0162;
						}
						Interaction.MsgBox("You can include up to " + Conversions.ToString(100) + " Utilities in a Query.", MsgBoxStyle.Information, "Exceeded Number of Utilities");
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					text7 = MyTable;
					text6 = "";
					switch (MyOptDB)
					{
					case "0":
						text6 = "mars ii";
						break;
					case "1":
						text6 = "mars iii";
						break;
					case "2":
						text6 = "aries";
						break;
					case "3":
						text6 = "oasys";
						break;
					case "5":
						text6 = "fab other";
						break;
					case "9":
						text6 = "a/t other1";
						break;
					case "B":
						text6 = "a/t other2";
						break;
					case "C":
						text6 = "a/t other3";
						break;
					case "A":
						text6 = "midas";
						break;
					}
					Globals_Renamed.QueryCancel = 0;
					num6 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].FindFreeTableIndex();
					if (num6 == -1)
					{
						Interaction.MsgBox("You can include up to " + Conversions.ToString(32) + " Tables or Views in a SQL Query.", MsgBoxStyle.Information, "Exceeded Number of Tables or Views");
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					MyNewAlias = "a" + Conversions.ToString((int)num6);
					checked
					{
						if (Operators.CompareString(MyOptDB, "4", TextCompare: false) == 0 || Operators.CompareString(MyOptDB, "6", TextCompare: false) == 0 || Operators.CompareString(MyOptDB, "7", TextCompare: false) == 0)
						{
							Confirm_DBType_Setting(MyOptDB, ref ll_DBType, ref ll_ObjectType);
							short num11 = (short)Globals_Renamed.MaxTables;
							for (num4 = 0; num4 <= num11; num4 = (short)unchecked(num4 + 1))
							{
								if (Operators.CompareString(Globals_Renamed.Tables[num4].Name, MyTable, TextCompare: false) == 0)
								{
									MyFile = Globals_Renamed.Tables[num4].fileschema;
									break;
								}
							}
							if (Operators.CompareString(MyOptDB, "6", TextCompare: false) == 0 || Operators.CompareString(MyOptDB, "7", TextCompare: false) == 0)
							{
								num9 = unchecked((short)((Operators.CompareString(MyOptDB, "6", TextCompare: false) != 0) ? 1 : 3));
								MyFile = ((Strings.InStr(MyTable, "\\") == 0) ? BuildForm.ProcessCSVHeaders(Strings.Trim(Globals_Renamed.MyPCDir) + Strings.Trim(MyTable), num9, ref lSQLiteTable) : BuildForm.ProcessCSVHeaders(Strings.Trim(MyTable), num9, ref lSQLiteTable));
								if (Operators.CompareString(MyFile, "", TextCompare: false) == 0)
								{
									goto IL_14e0;
								}
								goto IL_057e;
							}
							if (Operators.CompareString(MyOptDB, "4", TextCompare: false) != 0)
							{
								goto IL_057e;
							}
							if (Operators.CompareString(Strings.Trim(text7), "[Shared Inline Views]", TextCompare: false) == 0)
							{
								MyFile = "";
								BuildForm.FileOpenSave("O", "", "ilv", "", Globals_Renamed.MyPCDir);
								MyFile = CMDialog1Open.FileName;
								if ((Operators.CompareString(MyFile, "", TextCompare: false) == 0) | (Operators.CompareString(MyFile, "CANCEL", TextCompare: false) == 0))
								{
									Cursor.Current = Cursors.Default;
									goto end_IL_0001;
								}
								MyTable = Strings.Trim(MyFile);
								text7 = MyTable;
								goto IL_057e;
							}
							MyFile = Strings.Trim(Globals_Renamed.MyPCDir) + Strings.Trim(MyTable);
							goto IL_057e;
						}
						if (Operators.CompareString(MyOptDB, "D", TextCompare: false) == 0)
						{
							if (Operators.CompareString(Strings.Mid(Strings.UCase(MyTable), 1, 7), "DUCKDB:", TextCompare: false) != 0)
							{
								MyTable = "DUCKDB:" + MyTable;
							}
							MyFile = BuildForm.ProcessDuckDB(Strings.Trim(MyTable));
							num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, MyTable, MyFile, num6, "9", Conversions.ToShort("2"), l_TableOpts, ref MyDataTag);
							goto IL_0b0c;
						}
						ll_ObjectType = 2;
						short num12 = (short)Globals_Renamed.MaxTables;
						for (num4 = 0; num4 <= num12; num4 = (short)unchecked(num4 + 1))
						{
							if (Operators.CompareString(Globals_Renamed.Tables[num4].Display, text7, TextCompare: false) == 0 && (Operators.CompareString(Strings.LCase(Globals_Renamed.Tables[num4].DatabaseType), text6, TextCompare: false) == 0 || (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.Tables[num4].DatabaseType), "a/t other*", CompareMethod.Binary) && LikeOperator.LikeString(text6, "a/t other*", CompareMethod.Binary)) || (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.Tables[num4].DatabaseType), "fab other", CompareMethod.Binary) && LikeOperator.LikeString(text6, "a/t other*", CompareMethod.Binary))))
							{
								MyFile = Globals_Renamed.Tables[num4].fileschema;
								text8 = Strings.UCase(Globals_Renamed.Tables[num4].lbl8);
								MyTable = Globals_Renamed.Tables[num4].Name;
								break;
							}
						}
						if (LikeOperator.LikeString(Strings.UCase(text7), "*SITE SPECIFIC]**" + Strings.UCase("\\\\ATDFILE3.CH.INTEL.COM\\ATD-WEB\\PATHFINDING\\SQLPATHFINDER") + "*", CompareMethod.Binary))
						{
							MyTable = text7;
							MyFile = Strings.Trim(Strings.Mid(text7, Strings.Len("[Site Specific] ")));
							goto IL_0aba;
						}
						if (LikeOperator.LikeString(Strings.UCase(text7), "*SITE SPECIFIC]**" + Strings.UCase("\\\\AZSACTAPP22.INTEL.COM\\") + "*", CompareMethod.Binary))
						{
							MyTable = text7;
							MyFile = Strings.Trim(Strings.Mid(text7, Strings.Len("[Site Specific] ")));
							goto IL_0aba;
						}
						if (LikeOperator.LikeString(Strings.UCase(text7), "*SITE SPECIFIC]*.DAT*", CompareMethod.Binary))
						{
							MyTable = text7;
							MyFile = Strings.Trim(Strings.Mid(text7, Strings.Len("[Site Specific] ")));
							goto IL_0aba;
						}
						if (!LikeOperator.LikeString(Strings.UCase(text7), "*SITE SPECIFIC]*", CompareMethod.Binary) && Operators.CompareString(Strings.UCase(text7), "[WORK FOLDER]", TextCompare: false) != 0)
						{
							goto IL_0aba;
						}
						if (Operators.CompareString(Strings.UCase(text7), "[WORK FOLDER]", TextCompare: false) == 0)
						{
							MyFile = Globals_Renamed.MyPCDir;
						}
						BuildForm.GetSiteSpecific(MyFile, ref MyFile);
						if (Operators.CompareString(MyFile, "", TextCompare: false) == 0)
						{
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						}
						MyTable = "[Site Specific] " + MyFile;
						if (Operators.CompareString(Strings.UCase(text7), "[WORK FOLDER]", TextCompare: false) == 0)
						{
							num5 = Strings.InStrRev(MyFile, "\\");
							if (num5 != 0)
							{
								text7 = Strings.LCase(Strings.Trim(Strings.Mid(MyFile, num5 + 1)));
								text7 = Strings.Replace(text7, ".dat", "", 1, -1, CompareMethod.Text);
							}
						}
						else
						{
							text7 = MyTable;
						}
						goto IL_0aba;
					}
				}
				case 5423:
					{
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Cursor.Current = Cursors.Default;
							goto end_IL_0001_2;
						}
						break;
					}
					IL_0aba:
					Confirm_DBType_Setting(MyOptDB, ref ll_DBType, ref ll_ObjectType);
					l_TableOpts = Strings.Mid(text8 + "   ", 2);
					text9 = BuildForm.Get_Custom_Schema_Path(MyTable, MyFile);
					num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, text7, text9, num6, ll_DBType, ll_ObjectType, l_TableOpts, ref MyDataTag);
					goto IL_0b0c;
					IL_14e0:
					Confirm_DBType_Setting(MyOptDB, ref ll_DBType, ref ll_ObjectType);
					Cursor.Current = Cursors.Default;
					result = true;
					goto end_IL_0001_2;
					IL_0b0c:
					if (num7 == 0)
					{
						string text = "a" + Conversions.ToString((int)num6);
						if ((ll_ObjectType == 2) & ((Operators.CompareString(ll_DBType, "5", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0)))
						{
							TreeNode treeNode = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:a" + Conversions.ToString((int)num6));
							if (Operators.CompareString(Strings.UCase(treeNode.Text), "DATABASE {DEFAULT}", TextCompare: false) != 0)
							{
								Globals_Renamed.MyOtherServer1 = General_Procedures.Get_Node_Value(treeNode.Text);
							}
							else
							{
								Globals_Renamed.MyOtherServer1 = "Default";
								treeNode.Text = "Database {" + Globals_Renamed.MyOtherServer1 + "}";
							}
							text2 = Strings.UCase(General_Procedures.NodeCheck_s(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1), "S", 0));
							string text11 = General_Procedures.GetNodeKind(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1), "P");
							treeNode = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:a" + Conversions.ToString((int)num6));
							if (Operators.CompareString(treeNode.Text, "DB Type {Oracle}", TextCompare: false) == 0)
							{
								string text12 = General_Procedures.TestNodeKind(text2, text11);
								switch (text12)
								{
								case "SQLSERVER":
									treeNode.Text = "DB Type {SQLServer}";
									break;
								case "SQLITE":
									treeNode.Text = "DB Type {SQLite}";
									break;
								case "IMBIGDATA":
									treeNode.Text = "DB Type {iMBigData}";
									break;
								default:
									if (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0 || Operators.CompareString(text12, "TERADATA", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {Teradata}";
										break;
									}
									if (Operators.CompareString(text12, "MYSQL", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {MySQL}";
										break;
									}
									if (Operators.CompareString(text12, "SAPHANA", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {SAPHana}";
										break;
									}
									if (Operators.CompareString(text12, "DENODO", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {Denodo}";
										break;
									}
									if (Operators.CompareString(text12, "SNOWFLAKE", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {Snowflake}";
										break;
									}
									if (Operators.CompareString(text12, "POSTGRES", TextCompare: false) == 0)
									{
										treeNode.Text = "DB Type {Postgres}";
										break;
									}
									switch (text12)
									{
									case "POSTGRES":
										treeNode.Text = "DB Type {Postgres}";
										break;
									case "IBI-DAAS":
										treeNode.Text = "DB Type {IBI-DaaS}";
										break;
									case "MONGO":
										treeNode.Text = "DB Type {Mongo}";
										break;
									case "HADOOP":
										treeNode.Text = "DB Type {Hadoop}";
										break;
									}
									break;
								}
							}
						}
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].Set_UnSet_Join();
						if ((Operators.CompareString(MyOptDB, "4", TextCompare: false) == 0) & FrmSQLQuery[Globals_Renamed.g_FrmIdx].InlineInTree)
						{
							TreeNode treeNode = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "TABLES:ILV");
							if (treeNode.GetNodeCount(includeSubTrees: false) == 1)
							{
								text5 = (Globals_Renamed.MyOtherServer1 = BuildForm.GetDBTypeIL(1, MyTable, ll_DBType, ll_ObjectType));
								text2 = Strings.UCase(General_Procedures.NodeCheck_s(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, text5), "S", 0));
								string text11 = General_Procedures.GetNodeKind(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, text5), "P");
								string text12 = General_Procedures.TestNodeKind(text2, text11);
								if (Operators.CompareString(text5, "TEXT", TextCompare: false) == 0)
								{
									TreeNode treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:ILV");
									treeNode2.Text = "Database {TEXT (JET)}";
									treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:ILV");
									treeNode2.Text = "DB Type {TEXT}";
								}
								else if (Operators.CompareString(text5, "SQLite", TextCompare: false) == 0)
								{
									TreeNode treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:ILV");
									treeNode2.Text = "Database {TEXT (SQlite)}";
									treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:ILV");
									treeNode2.Text = "DB Type {SQLite}";
								}
								else
								{
									switch (text12)
									{
									case "SQLSERVER":
									{
										TreeNode treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:ILV");
										treeNode2.Text = "Database {" + text5 + "}";
										treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:ILV");
										treeNode2.Text = "DB Type {SQLServer}";
										break;
									}
									default:
										if (Operators.CompareString(text12, "Postgres", TextCompare: false) != 0)
										{
											TreeNode treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:ILV");
											treeNode2.Text = "Database {" + text5 + "}";
											treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:ILV");
											treeNode2.Text = "DB Type {Oracle}";
											break;
										}
										goto case "TERADATA";
									case "TERADATA":
									case "TERADATA-MIDAS":
									case "MYSQL":
									case "SAPHANA":
									case "DENODO":
									case "SNOWFLAKE":
									case "HADOOP":
									case "HADOOP-IMPALA":
									case "SQLITE":
									case "IBI-DAAS":
									case "MONGO":
									{
										TreeNode treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DATABASE:ILV");
										treeNode2.Text = "Database {" + text5 + "}";
										treeNode2 = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "DBTYPE:ILV");
										if (Operators.CompareString(text12, "TERADATA", TextCompare: false) == 0 || Operators.CompareString(text12, "TERADATA-MIDAS", TextCompare: false) == 0)
										{
											treeNode2.Text = "DB Type {Teradata}";
											break;
										}
										switch (text12)
										{
										case "IMBIGDATA":
											treeNode2.Text = "DB Type {iMBigData}";
											break;
										case "SQLITE":
											treeNode2.Text = "DB Type {SQLite}";
											break;
										case "IBI-DAAS":
											treeNode2.Text = "DB Type {IBI-DaaS}";
											break;
										case "MONGO":
											treeNode2.Text = "DB Type {Mongo}";
											break;
										default:
											if (Operators.CompareString(text12, "HADOOP-IMPALA", TextCompare: false) != 0)
											{
												switch (text12)
												{
												case "SAPHANA":
													treeNode2.Text = "DB Type {SAPHana}";
													break;
												case "DENODO":
													treeNode2.Text = "DB Type {Denodo}";
													break;
												case "SNOWFLAKE":
													treeNode2.Text = "DB Type {Snowflake}";
													break;
												case "POSTGRES":
													treeNode2.Text = "DB Type {Postgres}";
													break;
												default:
													treeNode2.Text = "DB Type {MySQL}";
													break;
												}
												break;
											}
											goto case "HADOOP";
										case "HADOOP":
											treeNode2.Text = "DB Type {Hadoop}";
											break;
										}
										break;
									}
									}
								}
							}
						}
						Confirm_DBType_Setting(MyOptDB, ref ll_DBType, ref ll_ObjectType);
						if ((ll_ObjectType == 4) & (Operators.CompareString(text5, "", TextCompare: false) != 0))
						{
							Globals_Renamed.MyOtherServer1 = text5;
						}
					}
					goto IL_14d5;
					IL_14d5:
					Cursor.Current = Cursors.Default;
					goto IL_14e0;
					IL_057e:
					if ((Operators.CompareString(Strings.Mid(MyTable, 1, 1), "&", TextCompare: false) == 0) | ((Operators.CompareString(MyOptDB, "4", TextCompare: false) == 0) | (Operators.CompareString(MyOptDB, "6", TextCompare: false) == 0) | (Operators.CompareString(MyOptDB, "7", TextCompare: false) == 0)))
					{
						if (Operators.CompareString(MyOptDB, "7", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(MyTable), "*.SDB", CompareMethod.Binary))
						{
							MyTable = BuildForm.Strip_Add_MyPCDir("S", MyTable);
							string text3 = BuildForm.Get_SQLite_TableName(MyTable);
							num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, text3, MyFile, num6, "9", Conversions.ToShort("2"), l_TableOpts, ref MyDataTag);
							if (Operators.CompareString(Strings.UCase(lSQLiteTable), "N/A", TextCompare: false) != 0 && Operators.CompareString(lSQLiteTable, "", TextCompare: false) != 0)
							{
								TreeNode treeNode = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "SCHEMA:a" + Conversions.ToString((int)num6));
								treeNode.Text = "Schema {" + lSQLiteTable + "}";
							}
						}
						else
						{
							num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, "", MyFile, num6, ll_DBType, ll_ObjectType, l_TableOpts, ref MyDataTag);
						}
					}
					else
					{
						num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, "", MyProject.Application.Info.DirectoryPath + "\\" + MyFile, num6, ll_DBType, ll_ObjectType, l_TableOpts, ref MyDataTag);
					}
					goto IL_0b0c;
					IL_0162:
					text2 = "Y";
					if (Operators.CompareString(Strings.UCase(MyTable), "HTML-REPORT", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(MyTable), "CHART-IN-JMP", TextCompare: false) == 0)
					{
						text2 = "YY";
					}
					num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_GridCol_View("A", MyTable, text2, "", 0, Conversions.ToString(1), 5, "", ref MyDataTag);
					goto IL_14d5;
				}
				goto IL_1565;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5423;
				continue;
			}
			break;
			IL_1565:
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

	public short Invoke_MQ_External(string MyFile, string MyTitleFile, ref string PackedExe, bool QuietMode = false)
	{
		string text = "";
		PackedExe = "";
		short result = 0;
		if (FrmMultiQuery != null && !FrmMultiQuery.IsDisposed)
		{
			FrmMultiQuery.Close();
			Application.DoEvents();
		}
		if (FrmMultiQuery == null || FrmMultiQuery.IsDisposed)
		{
			FrmMultiQuery = new FrmMultiQuery();
			if (QuietMode)
			{
				FrmMultiQuery.WindowState = FormWindowState.Minimized;
			}
			FrmMultiQuery.Show(this);
			FrmMultiQuery frmMultiQuery;
			DataGridView GridMQ = (frmMultiQuery = FrmMultiQuery).GridMQ2;
			string obj = BuildForm.gLoad_MQ(ref GridMQ, MyFile, ref PackedExe);
			frmMultiQuery.GridMQ2 = GridMQ;
			text = obj;
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				FrmMultiQuery.Set_Query_In_Title(MyTitleFile);
				FrmMultiQuery.fSaveAs = true;
				if (Operators.CompareString(PackedExe, "", TextCompare: false) != 0)
				{
					FrmMultiQuery.TxtDir.Text = PackedExe;
				}
				result = 1;
			}
			else
			{
				Interaction.MsgBox("Error Loading Multi-Query File " + MyFile + "." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Loading Query");
			}
		}
		return result;
	}

	public void SetMenuOpt(string i_Out_Inline, string i_Macrofile)
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
				case 363:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002a;
						case 5:
							goto IL_0032;
						case 7:
							goto IL_0045;
						case 8:
							goto IL_005d;
						case 6:
						case 9:
						case 10:
							goto IL_006d;
						case 11:
							goto IL_008c;
						case 12:
							goto IL_00a4;
						case 13:
							goto IL_00ad;
						case 15:
							goto IL_00c1;
						case 16:
							goto IL_00da;
						case 14:
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ad:
					num2 = 13;
					mnuMacroFile.Checked = false;
					break;
					IL_00c1:
					num2 = 15;
					text = " (" + Strings.Trim(i_Macrofile) + ")";
					goto IL_00da;
					IL_00a4:
					num2 = 12;
					text = " (No)";
					goto IL_00ad;
					IL_00da:
					num2 = 16;
					mnuMacroFile.Checked = true;
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.CompareString(i_Out_Inline, "", TextCompare: false) == 0)
					{
						goto IL_002a;
					}
					goto IL_0045;
					IL_002a:
					num2 = 4;
					text = " (No)";
					goto IL_0032;
					IL_0032:
					num2 = 5;
					mnuOutInline.Checked = false;
					goto IL_006d;
					IL_0045:
					num2 = 7;
					text = " (" + Strings.Trim(i_Out_Inline) + ")";
					goto IL_005d;
					IL_005d:
					num2 = 8;
					mnuOutInline.Checked = true;
					goto IL_006d;
					IL_006d:
					num2 = 10;
					mnuOutInline.Text = "Output To an &Inline View" + text + "...";
					goto IL_008c;
					IL_008c:
					num2 = 11;
					if (Operators.CompareString(i_Macrofile, "", TextCompare: false) == 0)
					{
						goto IL_00a4;
					}
					goto IL_00c1;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				mnuMacroFile.Text = "Temporary Macro File" + text + "...";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 363;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Get_DTRBuild_Ini2()
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
					errsource = "FrmMain - Get_DTRBuild_Ini2";
					string text = "Query Defaults";
					text = "MRU List";
					MnuFile0.Text = General_Procedures.Get_Ini_Data(text, "MRUII_1", "", 256, "");
					MnuFile1.Text = General_Procedures.Get_Ini_Data(text, "MRUII_2", "", 256, "");
					MnuFile2.Text = General_Procedures.Get_Ini_Data(text, "MRUII_3", "", 256, "");
					MnuFile3.Text = General_Procedures.Get_Ini_Data(text, "MRUII_4", "", 256, "");
					if (Operators.CompareString(MnuFile0.Text, "", TextCompare: false) != 0)
					{
						MnuFile0.Visible = true;
					}
					if (Operators.CompareString(MnuFile1.Text, "", TextCompare: false) != 0)
					{
						MnuFile1.Visible = true;
					}
					if (Operators.CompareString(MnuFile2.Text, "", TextCompare: false) != 0)
					{
						MnuFile2.Visible = true;
					}
					if (Operators.CompareString(MnuFile3.Text, "", TextCompare: false) != 0)
					{
						MnuFile3.Visible = true;
					}
					goto end_IL_0001;
				}
				case 399:
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
				try0001_dispatch = 399;
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

	public void Python_Legacy_Engine(string MyMode)
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
							goto IL_002a;
						case 4:
							goto IL_0049;
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
					cmdIcon.Items[17].Tag = "1";
					goto IL_002a;
					IL_002a:
					num2 = 3;
					cmdIcon.Items[17].BackColor = Color.Orange;
					goto IL_0049;
					IL_0049:
					num2 = 4;
					cmdIcon.Items[17].ToolTipText = "Using the Python Extract Engine";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Globals_Renamed.gUsePyEngine = "Y";
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

	private void cmdIcon_ButtonClick(object eventSender, EventArgs eventArgs)
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
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					toolStripItem = (ToolStripItem)eventSender;
					goto IL_000b;
				case 1113:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0039;
						case 7:
							goto IL_00ab;
						case 9:
							goto IL_00c5;
						case 11:
							goto IL_00e0;
						case 12:
							goto IL_00fa;
						case 13:
							goto IL_0103;
						case 16:
							goto IL_0122;
						case 17:
							goto IL_0141;
						case 18:
							goto IL_014f;
						case 19:
							goto IL_015d;
						case 21:
							goto IL_0171;
						case 22:
							goto IL_0181;
						case 23:
							goto IL_0191;
						case 27:
							goto IL_01a9;
						case 29:
							goto IL_01c4;
						case 31:
							goto IL_01df;
						case 33:
							goto IL_01fa;
						case 35:
							goto IL_0215;
						case 37:
							goto IL_0230;
						case 39:
							goto IL_024b;
						case 41:
							goto IL_0266;
						case 43:
							goto IL_0281;
						case 45:
							goto IL_029c;
						case 47:
							goto IL_02b0;
						case 49:
							goto IL_02cb;
						case 51:
							goto IL_02e3;
						case 53:
							goto IL_02fb;
						case 55:
							goto IL_0313;
						case 57:
							goto IL_0320;
						case 59:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 8:
						case 10:
						case 14:
						case 15:
						case 20:
						case 24:
						case 25:
						case 26:
						case 28:
						case 30:
						case 32:
						case 34:
						case 36:
						case 38:
						case 40:
						case 42:
						case 44:
						case 46:
						case 48:
						case 50:
						case 52:
						case 54:
						case 56:
						case 58:
						case 60:
						case 61:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_014f:
					num2 = 18;
					FrmSampleQs = new FrmSampleQs();
					goto IL_015d;
					IL_015d:
					num2 = 19;
					FrmSampleQs.Show(this);
					goto end_IL_0001_3;
					IL_0141:
					num2 = 17;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_014f;
					IL_00c5:
					num2 = 9;
					MnuOpenQuery_Click(MnuOpenQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Globals_Renamed.Design_Mode == 2)
					{
						goto IL_0024;
					}
					goto IL_0039;
					IL_0024:
					num2 = 4;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Refresh_Combo();
					goto IL_0039;
					IL_0039:
					num2 = 5;
					switch (toolStripItem.Owner.Items.IndexOf(toolStripItem))
					{
					case 0:
						break;
					case 1:
						goto IL_00c5;
					case 2:
						goto IL_00e0;
					case 3:
						goto IL_01a9;
					case 4:
						goto IL_01c4;
					case 5:
						goto IL_01df;
					case 6:
						goto IL_01fa;
					case 7:
						goto IL_0215;
					case 8:
						goto IL_0230;
					case 9:
						goto IL_024b;
					case 10:
						goto IL_0266;
					case 11:
						goto IL_0281;
					case 12:
						goto IL_029c;
					case 13:
						goto IL_02b0;
					case 14:
						goto IL_02cb;
					case 15:
						goto IL_02e3;
					case 16:
						goto IL_02fb;
					case 17:
						goto IL_0313;
					case 18:
						goto IL_0320;
					case 19:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001_3;
					}
					goto IL_00ab;
					IL_0320:
					num2 = 57;
					mnuHelpHTML_Click(mnuSPFWiki, new EventArgs());
					goto end_IL_0001_3;
					IL_0313:
					num2 = 55;
					Click_Py_Button();
					goto end_IL_0001_3;
					IL_02fb:
					num2 = 53;
					mnuNodes_Click(mnuNodes, new EventArgs());
					goto end_IL_0001_3;
					IL_02e3:
					num2 = 51;
					mnuTxt_Click(mnuTxt, new EventArgs());
					goto end_IL_0001_3;
					IL_02cb:
					num2 = 49;
					mnuJMP_Click(mnuJMP, new EventArgs());
					goto end_IL_0001_3;
					IL_02b0:
					num2 = 47;
					mnuExcel_Click(MnuExcel, new EventArgs());
					goto end_IL_0001_3;
					IL_029c:
					num2 = 45;
					BuildForm.Process_Screen("CANCEL");
					goto end_IL_0001_3;
					IL_0281:
					num2 = 43;
					MnuConfigure_Click(MnuConfigure, new EventArgs());
					goto end_IL_0001_3;
					IL_0266:
					num2 = 41;
					MnuSpawnVAX_Click(MnuSpawnVAX, new EventArgs());
					goto end_IL_0001_3;
					IL_024b:
					num2 = 39;
					mnuMultiQuery_Click(mnuMultiQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_0230:
					num2 = 37;
					mnuBatch_Click(mnuexit, new EventArgs());
					goto end_IL_0001_3;
					IL_0215:
					num2 = 35;
					MnuShowQuery_Click(MnuShowQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_01fa:
					num2 = 33;
					MnuCompute0_Click(MnuCompute0, new EventArgs());
					goto end_IL_0001_3;
					IL_01df:
					num2 = 31;
					MnuAddTables_Click(MnuAddTables, new EventArgs());
					goto end_IL_0001_3;
					IL_01c4:
					num2 = 29;
					mnurunquery_Click(MnuRunQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_01a9:
					num2 = 27;
					MnuSaveQuery_Click(MnuSaveQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_00e0:
					num2 = 11;
					if ((Globals_Renamed.Design_Mode == 2) & (Globals_Renamed.g_FrmIdx > 0))
					{
						goto IL_00fa;
					}
					goto IL_0103;
					IL_00fa:
					num2 = 12;
					Globals_Renamed.g_PrePostQueryOpen = true;
					goto IL_0103;
					IL_0103:
					num2 = 13;
					if (BuildForm.Test_for_PPQ("load"))
					{
						goto end_IL_0001_3;
					}
					goto IL_0122;
					IL_0122:
					num2 = 16;
					if (FrmSampleQs == null || FrmSampleQs.IsDisposed)
					{
						goto IL_0141;
					}
					goto IL_0171;
					IL_00ab:
					num2 = 7;
					MnuNewQuery_Click(MnuNewQuery, new EventArgs());
					goto end_IL_0001_3;
					IL_0171:
					num2 = 21;
					FrmSampleQs.WindowState = FormWindowState.Normal;
					goto IL_0181;
					IL_0181:
					num2 = 22;
					FrmSampleQs.TopMost = true;
					goto IL_0191;
					IL_0191:
					num2 = 23;
					FrmSampleQs.TopMost = false;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 59;
				mnuExit_Click(mnuexit, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1113;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Click_Py_Button()
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
				Python_Legacy_Engine("0");
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

	private void Set_BackColor2()
	{
		foreach (Control control in base.Controls)
		{
			try
			{
				MdiClient mdiClient = (MdiClient)control;
				mdiClient.BackColor = Color.White;
			}
			catch (InvalidCastException ex)
			{
				ProjectData.SetProjectError(ex);
				InvalidCastException ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	private void FrmMain_HandleDestroyed(object sender, EventArgs e)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void FrmMain_Load(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num4 = default(short);
		string[] currentRow = default(string[]);
		string text = default(string);
		string text2 = default(string);
		short num7 = default(short);
		string text3 = default(string);
		string text4 = default(string);
		short num8 = default(short);
		int num9 = default(int);
		string left = default(string);
		string MyErr = default(string);
		object MyReader = default(object);
		bool flag = default(bool);
		string MyMsg = default(string);
		string left2 = default(string);
		string text10 = default(string);
		int num11 = default(int);
		string text11 = default(string);
		short num12 = default(short);
		RegistryKey registryKey = default(RegistryKey);
		short num13 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					short num6;
					string text5;
					string text6;
					string text7;
					string text8;
					string text9;
					int num10;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 6305:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
							case 3:
								break;
							case 1:
								goto IL_13dd;
							default:
								goto end_IL_0001;
							}
							goto IL_139f;
						}
						IL_0b0f:
						num2 = 169;
						Globals_Renamed.Tables[num4].Description = currentRow[16];
						goto IL_0b2b;
						IL_0b2b:
						num2 = 170;
						Globals_Renamed.Tables[num4].Tag = "";
						goto IL_0b47;
						IL_0af3:
						num2 = 168;
						Globals_Renamed.Tables[num4].fileschema = currentRow[15];
						goto IL_0b0f;
						IL_13dd:
						num5 = unchecked(num + 1);
						num = 0;
						switch (num5)
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
							goto IL_0020;
						case 6:
							goto IL_0025;
						case 7:
							goto IL_002a;
						case 8:
							goto IL_0033;
						case 9:
							goto IL_003c;
						case 10:
							goto IL_0042;
						case 11:
							goto IL_004c;
						case 12:
							goto IL_0052;
						case 13:
							goto IL_005c;
						case 14:
							goto IL_0066;
						case 15:
							goto IL_0070;
						case 16:
							goto IL_007a;
						case 17:
							goto IL_0084;
						case 18:
							goto IL_008a;
						case 19:
							goto IL_0090;
						case 20:
							goto IL_009a;
						case 21:
							goto IL_00a0;
						case 22:
							goto IL_00aa;
						case 23:
							goto IL_00b0;
						case 24:
							goto IL_00ba;
						case 25:
							goto IL_00c4;
						case 26:
							goto IL_00ca;
						case 27:
							goto IL_00d4;
						case 28:
							goto IL_00da;
						case 29:
							goto IL_00e3;
						case 30:
							goto IL_010a;
						case 31:
						case 32:
							goto IL_0124;
						case 33:
							goto IL_012e;
						case 34:
							goto IL_014a;
						case 35:
							goto IL_0170;
						case 36:
							goto IL_0193;
						case 37:
							goto IL_01b9;
						case 38:
							goto IL_01c8;
						case 39:
							goto IL_01e7;
						case 40:
							goto IL_01f1;
						case 41:
							goto IL_0200;
						case 42:
							goto IL_020d;
						case 43:
							goto IL_0213;
						case 44:
							goto IL_0247;
						case 45:
							goto IL_0263;
						case 46:
							goto IL_0270;
						case 47:
							goto IL_0276;
						case 48:
							goto IL_0295;
						case 49:
							goto IL_02a3;
						case 50:
							goto IL_02d0;
						case 52:
							goto IL_02dd;
						case 53:
							goto IL_02f6;
						case 54:
							goto IL_0315;
						case 55:
							goto IL_0326;
						case 56:
							goto IL_0337;
						case 57:
							goto IL_0351;
						case 58:
							goto IL_035b;
						case 51:
						case 59:
						case 60:
						case 61:
						case 62:
							goto IL_0366;
						case 63:
							goto IL_0374;
						case 64:
							goto IL_038d;
						case 65:
							goto IL_03ac;
						case 66:
							goto IL_03bd;
						case 67:
							goto IL_03db;
						case 68:
						case 69:
							goto IL_03e7;
						case 70:
							goto IL_0408;
						case 71:
							goto IL_0411;
						case 72:
							goto IL_042d;
						case 73:
							goto IL_0441;
						case 74:
							goto IL_045d;
						case 75:
							goto IL_0479;
						case 76:
							goto IL_0495;
						case 77:
							goto IL_04b1;
						case 78:
							goto IL_04cd;
						case 79:
							goto IL_04d6;
						case 80:
							goto IL_04df;
						case 81:
							goto IL_04f6;
						case 82:
							goto IL_050a;
						case 83:
							goto IL_0526;
						case 85:
							goto IL_0539;
						case 84:
						case 86:
						case 87:
							goto IL_0549;
						case 88:
							goto IL_0553;
						case 89:
							goto IL_056a;
						case 90:
							goto IL_0578;
						case 91:
							goto IL_058d;
						case 92:
							goto IL_05a6;
						case 95:
							goto IL_05b6;
						case 93:
						case 94:
						case 96:
						case 97:
							goto IL_05c1;
						case 98:
							goto IL_05da;
						case 99:
							goto IL_05eb;
						case 100:
						case 101:
							goto IL_05f6;
						case 102:
							goto IL_05ff;
						case 103:
							goto IL_0617;
						case 104:
							goto IL_062f;
						case 105:
							goto IL_063d;
						case 106:
							goto IL_0645;
						case 107:
							goto IL_0663;
						case 108:
							goto IL_066d;
						case 109:
							goto IL_0676;
						case 110:
							goto IL_0684;
						case 111:
							goto IL_068a;
						case 112:
							goto IL_0690;
						case 113:
							goto IL_069e;
						case 114:
							goto IL_06b2;
						case 116:
							goto IL_06c0;
						case 117:
							goto IL_06d1;
						case 119:
							goto IL_06f3;
						case 115:
						case 120:
						case 121:
							goto IL_06fe;
						case 122:
							goto IL_070e;
						case 123:
							goto IL_071c;
						case 128:
							goto IL_073e;
						case 129:
							goto IL_0761;
						case 130:
							goto IL_0780;
						case 132:
						case 133:
							goto IL_07a0;
						case 134:
							goto IL_07bb;
						case 135:
							goto IL_07d6;
						case 136:
							goto IL_07f1;
						case 137:
							goto IL_080c;
						case 138:
							goto IL_0827;
						case 139:
							goto IL_0842;
						case 140:
							goto IL_085d;
						case 141:
							goto IL_0878;
						case 142:
							goto IL_0893;
						case 143:
							goto IL_08a0;
						case 144:
							goto IL_08b6;
						case 145:
							goto IL_08cf;
						case 146:
							goto IL_08eb;
						case 148:
							goto IL_090d;
						case 149:
							goto IL_0926;
						case 150:
							goto IL_0946;
						case 151:
							goto IL_0966;
						case 153:
							goto IL_098c;
						case 154:
							goto IL_09a5;
						case 155:
							goto IL_09c1;
						case 156:
							goto IL_09e1;
						case 158:
							goto IL_0a05;
						case 159:
							goto IL_0a21;
						case 160:
							goto IL_0a3d;
						case 147:
						case 152:
						case 157:
						case 161:
						case 162:
							goto IL_0a5a;
						case 163:
							goto IL_0a67;
						case 164:
							goto IL_0a83;
						case 165:
							goto IL_0a9f;
						case 166:
							goto IL_0abb;
						case 167:
							goto IL_0ad7;
						case 168:
							goto IL_0af3;
						case 169:
							goto IL_0b0f;
						case 170:
							goto IL_0b2b;
						case 171:
							goto IL_0b47;
						case 172:
							goto IL_0b67;
						case 173:
							goto IL_0b83;
						case 174:
							goto IL_0b90;
						case 125:
						case 126:
						case 127:
						case 175:
							goto IL_0b9a;
						case 176:
							goto IL_0bc6;
						case 177:
							goto IL_0bd4;
						case 178:
							goto IL_0bf2;
						case 179:
							goto IL_0c10;
						case 180:
						case 181:
							goto IL_0c1b;
						case 182:
							goto IL_0c24;
						case 183:
							goto IL_0c35;
						case 185:
							goto IL_0c48;
						case 184:
						case 186:
						case 187:
							goto IL_0c5e;
						case 118:
						case 188:
							goto IL_0c73;
						case 189:
							goto IL_0c86;
						case 190:
							goto IL_0cb1;
						case 191:
							goto IL_0cd9;
						case 192:
						case 193:
							goto IL_0cf7;
						case 194:
							goto IL_0d0d;
						case 195:
							goto IL_0d16;
						case 196:
							goto IL_0d27;
						case 197:
							goto IL_0d34;
						case 198:
							goto IL_0d4b;
						case 200:
							goto IL_0d58;
						case 201:
							goto IL_0d6c;
						case 203:
							goto IL_0d91;
						case 204:
							goto IL_0d9e;
						case 199:
						case 205:
						case 206:
							goto IL_0daf;
						case 207:
							goto IL_0dc2;
						case 208:
							goto IL_0dd3;
						case 210:
						case 211:
							goto IL_0df3;
						case 214:
							goto IL_0e01;
						case 215:
							goto IL_0e24;
						case 216:
							goto IL_0e42;
						case 218:
						case 219:
							goto IL_0e62;
						case 220:
							goto IL_0e7d;
						case 221:
							goto IL_0e98;
						case 222:
							goto IL_0eb3;
						case 223:
							goto IL_0ece;
						case 224:
							goto IL_0ee9;
						case 225:
							goto IL_0f02;
						case 226:
							goto IL_0f13;
						case 227:
							goto IL_0f2f;
						case 228:
						case 229:
							goto IL_0f3e;
						case 212:
						case 213:
						case 230:
							goto IL_0f48;
						case 231:
							goto IL_0f8b;
						case 232:
							goto IL_0f99;
						case 233:
							goto IL_0fb7;
						case 234:
							goto IL_0fd5;
						case 235:
						case 236:
							goto IL_0fe0;
						case 237:
							goto IL_0ff1;
						case 238:
							goto IL_1001;
						case 240:
							goto IL_1011;
						case 241:
							goto IL_1028;
						case 239:
						case 242:
						case 243:
							goto IL_1039;
						case 202:
						case 244:
							goto IL_104e;
						case 245:
							goto IL_105b;
						case 246:
							goto IL_107b;
						case 247:
							goto IL_108c;
						case 249:
						case 250:
							goto IL_10ac;
						case 251:
							goto IL_10b5;
						case 254:
							goto IL_10c3;
						case 255:
							goto IL_10e6;
						case 256:
							goto IL_1104;
						case 258:
						case 259:
							goto IL_1124;
						case 260:
							goto IL_113f;
						case 261:
							goto IL_1158;
						case 262:
							goto IL_1169;
						case 263:
							goto IL_1185;
						case 264:
						case 265:
							goto IL_1194;
						case 252:
						case 253:
						case 266:
							goto IL_119e;
						case 267:
							goto IL_11cd;
						case 268:
							goto IL_11dd;
						case 269:
							goto IL_11eb;
						case 270:
							goto IL_1209;
						case 271:
							goto IL_1227;
						case 272:
						case 273:
							goto IL_1232;
						case 274:
							goto IL_123f;
						case 275:
							goto IL_1250;
						case 276:
							goto IL_126c;
						case 277:
							goto IL_1275;
						case 278:
							goto IL_1283;
						case 279:
							goto IL_1293;
						case 280:
							goto IL_12aa;
						case 283:
							goto IL_12bd;
						case 284:
							goto IL_12ca;
						case 285:
							goto IL_12df;
						case 286:
							goto IL_12eb;
						case 287:
							goto IL_12f9;
						case 281:
						case 282:
						case 288:
						case 289:
							goto IL_1308;
						case 124:
						case 131:
						case 209:
						case 217:
						case 248:
						case 257:
						case 291:
							goto IL_131c;
						case 292:
							goto IL_1339;
						case 293:
							goto IL_1347;
						case 294:
							goto IL_1365;
						case 295:
							goto IL_1383;
						case 296:
						case 297:
							goto IL_138e;
						case 299:
							goto IL_139f;
						case 300:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 290:
						case 298:
						case 301:
							goto end_IL_0001_3;
						}
						goto default;
						IL_139f:
						num2 = 299;
						Support.ErrService(Information.Err().Number, text + text2, Information.Err().Description);
						break;
						IL_000a:
						num2 = 2;
						text = "FrmMain - Load";
						goto IL_0012;
						IL_0012:
						num2 = 3;
						text2 = "";
						goto IL_001b;
						IL_001b:
						num2 = 4;
						num4 = 0;
						goto IL_0020;
						IL_0020:
						num2 = 5;
						num6 = 0;
						goto IL_0025;
						IL_0025:
						num2 = 6;
						num7 = 0;
						goto IL_002a;
						IL_002a:
						num2 = 7;
						text3 = "";
						goto IL_0033;
						IL_0033:
						num2 = 8;
						text4 = "";
						goto IL_003c;
						IL_003c:
						num2 = 9;
						num8 = 0;
						goto IL_0042;
						IL_0042:
						num2 = 10;
						text5 = "";
						goto IL_004c;
						IL_004c:
						num2 = 11;
						num9 = 0;
						goto IL_0052;
						IL_0052:
						num2 = 12;
						text6 = "";
						goto IL_005c;
						IL_005c:
						num2 = 13;
						text7 = "";
						goto IL_0066;
						IL_0066:
						num2 = 14;
						text8 = "";
						goto IL_0070;
						IL_0070:
						num2 = 15;
						left = "";
						goto IL_007a;
						IL_007a:
						num2 = 16;
						MyErr = "";
						goto IL_0084;
						IL_0084:
						num2 = 17;
						MyReader = null;
						goto IL_008a;
						IL_008a:
						num2 = 18;
						flag = false;
						goto IL_0090;
						IL_0090:
						num2 = 19;
						MyMsg = "";
						goto IL_009a;
						IL_009a:
						num2 = 20;
						currentRow = null;
						goto IL_00a0;
						IL_00a0:
						num2 = 21;
						left2 = "";
						goto IL_00aa;
						IL_00aa:
						num2 = 22;
						num10 = 0;
						goto IL_00b0;
						IL_00b0:
						num2 = 23;
						text9 = "";
						goto IL_00ba;
						IL_00ba:
						num2 = 24;
						text10 = "";
						goto IL_00c4;
						IL_00c4:
						num2 = 25;
						num11 = 0;
						goto IL_00ca;
						IL_00ca:
						num2 = 26;
						text11 = "";
						goto IL_00d4;
						IL_00d4:
						num2 = 27;
						num12 = 0;
						goto IL_00da;
						IL_00da:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_00e3;
						IL_00e3:
						num2 = 29;
						if (MyProject.Computer.FileSystem.DirectoryExists(MyProject.Application.Info.DirectoryPath))
						{
							goto IL_010a;
						}
						goto IL_0124;
						IL_010a:
						num2 = 30;
						Directory.SetCurrentDirectory(MyProject.Application.Info.DirectoryPath);
						goto IL_0124;
						IL_0124:
						num2 = 32;
						Set_BackColor2();
						goto IL_012e;
						IL_012e:
						num2 = 33;
						Globals_Renamed.guDomain = Strings.UCase(Strings.Trim(Interaction.Environ("USERDOMAIN")));
						goto IL_014a;
						IL_014a:
						num2 = 34;
						Globals_Renamed.MySchemaDir = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Schema";
						goto IL_0170;
						IL_0170:
						num2 = 35;
						text4 = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Local";
						goto IL_0193;
						IL_0193:
						num2 = 36;
						Globals_Renamed.gChartDir = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Charts";
						goto IL_01b9;
						IL_01b9:
						num2 = 37;
						left2 = General_Procedures.Get_UI("LOAD");
						goto IL_01c8;
						IL_01c8:
						num2 = 38;
						if (Operators.CompareString(left2, "@@ERROR@@", TextCompare: false) == 0)
						{
							ProjectData.EndApp();
						}
						goto IL_01e7;
						IL_0b47:
						num2 = 171;
						left = Strings.Mid(Globals_Renamed.Tables[num4].ObjectType, 1, 1);
						goto IL_0b67;
						IL_01e7:
						num2 = 39;
						left2 = "";
						goto IL_01f1;
						IL_01f1:
						num2 = 40;
						text9 = Strings.LCase(General_Procedures.GetWindowsDir());
						goto IL_0200;
						IL_0200:
						num2 = 41;
						Globals_Renamed.gWinuser = SystemInformation.UserName;
						goto IL_020d;
						IL_020d:
						num2 = 42;
						num8 = 0;
						goto IL_0213;
						IL_0213:
						num2 = 43;
						MyErr = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "USE-REGISTRY", "N", 50, MyProject.Application.Info.DirectoryPath + "\\sqlpathfinder.ini");
						goto IL_0247;
						IL_0247:
						num2 = 44;
						if (Operators.CompareString(MyErr, "Y", TextCompare: false) == 0)
						{
							goto IL_0263;
						}
						goto IL_0366;
						IL_0263:
						num2 = 45;
						Globals_Renamed.gUseRegistry = "Y";
						goto IL_0270;
						IL_0270:
						num2 = 46;
						registryKey = null;
						goto IL_0276;
						IL_0276:
						num2 = 47;
						registryKey = MyProject.Computer.Registry.CurrentUser.OpenSubKey("software\\SQLPathFinder\\WorkFolder\\", writable: false);
						goto IL_0295;
						IL_0295:
						num2 = 48;
						if (registryKey != null)
						{
							goto IL_02a3;
						}
						goto IL_02dd;
						IL_02a3:
						num2 = 49;
						Globals_Renamed.IniFileName = registryKey.GetValue("Path").ToString() + "SQLPathFinder_" + Globals_Renamed.gWinuser + ".ini";
						goto IL_02d0;
						IL_02d0:
						num2 = 50;
						num8 = 1;
						goto IL_0366;
						IL_02dd:
						num2 = 52;
						text10 = Strings.Trim(MyProject.Application.Info.DirectoryPath);
						goto IL_02f6;
						IL_02f6:
						num2 = 53;
						if (Operators.CompareString(Strings.Right(text10, 1), "\\", TextCompare: false) != 0)
						{
							goto IL_0315;
						}
						goto IL_0326;
						IL_0315:
						num2 = 54;
						text10 += "\\";
						goto IL_0326;
						IL_0326:
						num2 = 55;
						text10 += "SQLPathFinder_All.ini";
						goto IL_0337;
						IL_0337:
						num2 = 56;
						if (MyProject.Computer.FileSystem.FileExists(text10))
						{
							goto IL_0351;
						}
						goto IL_0366;
						IL_0351:
						num2 = 57;
						Globals_Renamed.IniFileName = text10;
						goto IL_035b;
						IL_035b:
						num2 = 58;
						num8 = 1;
						goto IL_0366;
						IL_0366:
						num2 = 62;
						if (num8 == 0)
						{
							goto IL_0374;
						}
						goto IL_03e7;
						IL_0374:
						num2 = 63;
						text10 = Strings.Trim(MyProject.Application.Info.DirectoryPath);
						goto IL_038d;
						IL_038d:
						num2 = 64;
						if (Operators.CompareString(Strings.Right(text10, 1), "\\", TextCompare: false) != 0)
						{
							goto IL_03ac;
						}
						goto IL_03bd;
						IL_03ac:
						num2 = 65;
						text10 += "\\";
						goto IL_03bd;
						IL_03bd:
						num2 = 66;
						Globals_Renamed.IniFileName = text10 + "SQLPathFinder_" + Globals_Renamed.gWinuser + ".ini";
						goto IL_03db;
						IL_03db:
						num2 = 67;
						text10 = "";
						goto IL_03e7;
						IL_03e7:
						num2 = 69;
						Globals_Renamed.gBaseSHGUIDir = "\\\\SHUser-Prod.intel.com\\SHProdUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\";
						goto IL_0408;
						IL_0408:
						num2 = 70;
						VBMath.Randomize();
						goto IL_0411;
						IL_0411:
						num2 = 71;
						num11 = (int)Conversion.Int(32767f * VBMath.Rnd() + 1f);
						goto IL_042d;
						IL_042d:
						num2 = 72;
						Globals_Renamed.gSPFCache = Strings.Trim(Conversions.ToString(num11));
						goto IL_0441;
						IL_0441:
						num2 = 73;
						Globals_Renamed.MYMSAccessTitle = "SQLPathfinder - SQL Engine (" + Globals_Renamed.gSPFCache + ")";
						goto IL_045d;
						IL_045d:
						num2 = 74;
						Globals_Renamed.gJSLOptions = "<OPTIONS>\r\n/JSL=Y\r\n/OUTLOOK=<<<spf-email-type>>>\r\n@PROMPT@/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\n";
						goto IL_0479;
						IL_0479:
						num2 = 75;
						Globals_Renamed.gROptions = "<OPTIONS>\r\n/RSCRIPT=Y\r\n/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\n";
						goto IL_0495;
						IL_0495:
						num2 = 76;
						Globals_Renamed.gCBOptions = "<OPTIONS>\r\n/CB_ACS=Y\r\n@PROMPT@/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\n";
						goto IL_04b1;
						IL_04b1:
						num2 = 77;
						Globals_Renamed.gWFOptions = "<OPTIONS>\r\n/WRITE-FILE=Y\r\n/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n@PROMPT@/CSV=@FILE@\r\n</OPTIONS>\r\n";
						goto IL_04cd;
						IL_04cd:
						num2 = 78;
						GridModule.Init_Colors();
						goto IL_04d6;
						IL_04d6:
						num2 = 79;
						BuildForm.Get_DTRBuild_Ini();
						goto IL_04df;
						IL_04df:
						num2 = 80;
						Globals_Renamed.g_TmpSPFVG2 = "tmp_" + Globals_Renamed.gSPFCache;
						goto IL_04f6;
						IL_04f6:
						num2 = 81;
						mnuSPFGrid.Image = Resources.spfgrid2;
						goto IL_050a;
						IL_050a:
						num2 = 82;
						if (Operators.CompareString(Globals_Renamed.gUsePyEngine, "N", TextCompare: false) == 0)
						{
							goto IL_0526;
						}
						goto IL_0539;
						IL_0526:
						num2 = 83;
						Python_Legacy_Engine("1");
						goto IL_0549;
						IL_0539:
						num2 = 85;
						Python_Legacy_Engine("0");
						goto IL_0549;
						IL_0549:
						num2 = 87;
						text11 = Environment.CommandLine;
						goto IL_0553;
						IL_0553:
						num2 = 88;
						num9 = Strings.InStr(Strings.UCase(text11), ".EXE");
						goto IL_056a;
						IL_056a:
						num2 = 89;
						if (num9 != 0)
						{
							goto IL_0578;
						}
						goto IL_05b6;
						IL_0578:
						num2 = 90;
						text11 = Strings.Trim(Strings.Mid(text11, num9 + 4));
						goto IL_058d;
						IL_058d:
						num2 = 91;
						if (Operators.CompareString(text11, "\"", TextCompare: false) == 0)
						{
							goto IL_05a6;
						}
						goto IL_05c1;
						IL_05a6:
						num2 = 92;
						text11 = "";
						goto IL_05c1;
						IL_05b6:
						num2 = 95;
						text11 = "";
						goto IL_05c1;
						IL_05c1:
						num2 = 97;
						if (Operators.CompareString(text11, "", TextCompare: false) == 0)
						{
							goto IL_05da;
						}
						goto IL_05f6;
						IL_05da:
						num2 = 98;
						if (!General_Procedures.SPF_Date_Chk_OK())
						{
							goto IL_05eb;
						}
						goto IL_05f6;
						IL_05eb:
						num2 = 99;
						Application.Exit();
						goto IL_05f6;
						IL_05f6:
						ProjectData.ClearProjectError();
						num3 = -3;
						goto IL_05ff;
						IL_05ff:
						num2 = 102;
						FileSystem.ChDrive(MyProject.Application.Info.DirectoryPath);
						goto IL_0617;
						IL_0617:
						num2 = 103;
						FileSystem.ChDir(MyProject.Application.Info.DirectoryPath);
						goto IL_062f;
						IL_062f:
						num2 = 104;
						Information.Err().Clear();
						goto IL_063d;
						IL_063d:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_0645;
						IL_0645:
						num2 = 106;
						Text = "SQLPathFinder 3 (Main - " + Globals_Renamed.gSPFCache + ")";
						goto IL_0663;
						IL_0663:
						num2 = 107;
						Get_DTRBuild_Ini2();
						goto IL_066d;
						IL_066d:
						num2 = 108;
						Globals_Renamed.Design_Mode = 0;
						goto IL_0676;
						IL_0676:
						num2 = 109;
						BuildForm.Set_Controls(Globals_Renamed.Design_Mode);
						goto IL_0684;
						IL_0684:
						num2 = 110;
						num7 = 0;
						goto IL_068a;
						IL_068a:
						num2 = 111;
						num4 = 1;
						goto IL_0690;
						IL_0690:
						num2 = 112;
						if (num7 == 0)
						{
							goto IL_069e;
						}
						goto IL_06c0;
						IL_069e:
						num2 = 113;
						text3 = Globals_Renamed.MySchemaDir + "\\views.dat";
						goto IL_06b2;
						IL_06b2:
						num2 = 114;
						text2 = " - Load Global Views";
						goto IL_06fe;
						IL_06c0:
						num2 = 116;
						text3 = text4 + "\\views_local.dat";
						goto IL_06d1;
						IL_06d1:
						num2 = 117;
						if (MyProject.Computer.FileSystem.FileExists(text3))
						{
							goto IL_06f3;
						}
						goto IL_0c73;
						IL_06f3:
						num2 = 119;
						text2 = " - Load Local Views";
						goto IL_06fe;
						IL_06fe:
						num2 = 121;
						flag = BuildForm.OpenDelimitedFile(text3, ref MyReader, ref MyMsg);
						goto IL_070e;
						IL_070e:
						num2 = 122;
						if (!flag)
						{
							goto IL_071c;
						}
						goto IL_0b9a;
						IL_071c:
						num2 = 123;
						MyMsg = " - Unable to Load Views (" + MyMsg + ")";
						goto IL_131c;
						IL_0b9a:
						num2 = 127;
						if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
						{
							goto IL_073e;
						}
						goto IL_0bc6;
						IL_0bc6:
						num2 = 176;
						if (flag)
						{
							goto IL_0bd4;
						}
						goto IL_0c1b;
						IL_0bd4:
						num2 = 177;
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_0bf2;
						IL_0bf2:
						num2 = 178;
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_0c10;
						IL_0c10:
						num2 = 179;
						flag = false;
						goto IL_0c1b;
						IL_0c1b:
						num2 = 181;
						currentRow = null;
						goto IL_0c24;
						IL_0c24:
						num2 = 182;
						if (num7 == 0)
						{
							goto IL_0c35;
						}
						goto IL_0c48;
						IL_0c35:
						num2 = 183;
						Globals_Renamed.MaxTables = num4 - 1;
						goto IL_0c5e;
						IL_0c48:
						num2 = 185;
						Globals_Renamed.MaxTables = Globals_Renamed.MaxTables + num4 - 1;
						goto IL_0c5e;
						IL_0c5e:
						num2 = 187;
						num7 = (short)unchecked(num7 + 1);
						if (num7 <= 1)
						{
							goto IL_068a;
						}
						goto IL_0c73;
						IL_0c73:
						num2 = 188;
						num13 = (short)Globals_Renamed.MaxTables;
						num4 = 0;
						goto IL_0d04;
						IL_0d04:
						if (num4 <= num13)
						{
							goto IL_0c86;
						}
						goto IL_0d0d;
						IL_0d0d:
						num2 = 194;
						num7 = 0;
						goto IL_0d16;
						IL_0d16:
						num2 = 195;
						if (num7 == 0)
						{
							goto IL_0d27;
						}
						goto IL_0d58;
						IL_0d27:
						num2 = 196;
						text2 = " - Load Global Nodes";
						goto IL_0d34;
						IL_0d34:
						num2 = 197;
						text3 = Globals_Renamed.MySchemaDir + "\\node.dat";
						goto IL_0d4b;
						IL_0d4b:
						num2 = 198;
						num4 = 0;
						goto IL_0daf;
						IL_0d58:
						num2 = 200;
						text3 = text4 + "\\node_local.dat";
						goto IL_0d6c;
						IL_0d6c:
						num2 = 201;
						if (MyProject.Computer.FileSystem.FileExists(text3))
						{
							goto IL_0d91;
						}
						goto IL_104e;
						IL_0d91:
						num2 = 203;
						text2 = " - Load Local Nodes";
						goto IL_0d9e;
						IL_0d9e:
						num2 = 204;
						num4 = (short)(Globals_Renamed.nonodes + 1);
						goto IL_0daf;
						IL_0daf:
						num2 = 206;
						flag = BuildForm.OpenDelimitedFile(text3, ref MyReader, ref MyMsg);
						goto IL_0dc2;
						IL_0dc2:
						num2 = 207;
						if (!flag)
						{
							goto IL_0dd3;
						}
						goto IL_0df3;
						IL_0dd3:
						num2 = 208;
						MyMsg = " - Unable to Load Nodes (" + MyMsg + ")";
						goto IL_131c;
						IL_0df3:
						num2 = 211;
						num8 = 1;
						goto IL_0f48;
						IL_0f48:
						num2 = 213;
						if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))) && num8 == 1))
						{
							goto IL_0e01;
						}
						goto IL_0f8b;
						IL_0b83:
						num2 = 173;
						num4++;
						goto IL_0b90;
						IL_0b90:
						num2 = 174;
						currentRow = null;
						goto IL_0b9a;
						IL_0b67:
						num2 = 172;
						if (Operators.CompareString(left, "!", TextCompare: false) != 0)
						{
							goto IL_0b83;
						}
						goto IL_0b90;
						IL_0f8b:
						num2 = 231;
						if (flag)
						{
							goto IL_0f99;
						}
						goto IL_0fe0;
						IL_0f99:
						num2 = 232;
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_0fb7;
						IL_0fb7:
						num2 = 233;
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_0fd5;
						IL_0fd5:
						num2 = 234;
						flag = false;
						goto IL_0fe0;
						IL_0fe0:
						num2 = 236;
						if (num7 == 0)
						{
							goto IL_0ff1;
						}
						goto IL_1011;
						IL_0ff1:
						num2 = 237;
						Globals_Renamed.nonodes = (short)(num4 - 1);
						goto IL_1001;
						IL_1001:
						num2 = 238;
						BuildForm.Extra_Nodes();
						goto IL_1039;
						IL_1011:
						num2 = 240;
						if (num4 > Globals_Renamed.nonodes + 1)
						{
							goto IL_1028;
						}
						goto IL_1039;
						IL_1028:
						num2 = 241;
						Globals_Renamed.nonodes = (short)(num4 - 1);
						goto IL_1039;
						IL_1039:
						num2 = 243;
						num7 = (short)unchecked(num7 + 1);
						if (num7 <= 1)
						{
							goto IL_0d16;
						}
						goto IL_104e;
						IL_104e:
						num2 = 244;
						text2 = " - Load Schemas";
						goto IL_105b;
						IL_105b:
						num2 = 245;
						flag = BuildForm.OpenDelimitedFile(Globals_Renamed.MySchemaDir + "\\schema.dat", ref MyReader, ref MyMsg);
						goto IL_107b;
						IL_107b:
						num2 = 246;
						if (!flag)
						{
							goto IL_108c;
						}
						goto IL_10ac;
						IL_108c:
						num2 = 247;
						MyMsg = " - Unable to Load Schemas (" + MyMsg + ")";
						goto IL_131c;
						IL_10ac:
						num2 = 250;
						num4 = 0;
						goto IL_10b5;
						IL_10b5:
						num2 = 251;
						num8 = 1;
						goto IL_119e;
						IL_119e:
						num2 = 253;
						if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
						{
							goto IL_10c3;
						}
						goto IL_11cd;
						IL_11cd:
						num2 = 267;
						Globals_Renamed.noschemas = (short)(num4 - 1);
						goto IL_11dd;
						IL_11dd:
						num2 = 268;
						if (flag)
						{
							goto IL_11eb;
						}
						goto IL_1232;
						IL_11eb:
						num2 = 269;
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_1209;
						IL_1209:
						num2 = 270;
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_1227;
						IL_1227:
						num2 = 271;
						flag = false;
						goto IL_1232;
						IL_1232:
						num2 = 273;
						text2 = " - Load Operators and UI Data";
						goto IL_123f;
						IL_123f:
						num2 = 274;
						BuildForm.Load_Operators("ORCL_Operators.dat");
						goto IL_1250;
						IL_1250:
						num2 = 275;
						if (Operators.CompareString(text11, "", TextCompare: false) != 0)
						{
							goto IL_126c;
						}
						goto IL_12bd;
						IL_126c:
						num2 = 276;
						num12 = 1;
						goto IL_1275;
						IL_1275:
						num2 = 277;
						base.WindowState = FormWindowState.Minimized;
						goto IL_1283;
						IL_1283:
						num2 = 278;
						num12 = ProcessCmdLineArgs(text11);
						goto IL_1293;
						IL_1293:
						num2 = 279;
						if (unchecked(num12 == 0 || num12 == 2))
						{
							goto IL_12aa;
						}
						goto IL_1308;
						IL_12aa:
						num2 = 280;
						Close();
						goto IL_1308;
						IL_12bd:
						num2 = 283;
						MyErr = "";
						goto IL_12ca;
						IL_12ca:
						num2 = 284;
						num10 = BuildForm.Copy_TNSNames(Globals_Renamed.MyPCDir, IsQuiet: false, ref MyErr);
						goto IL_12df;
						IL_12df:
						num2 = 285;
						General_Procedures.Chk_Registry_cmd();
						goto IL_12eb;
						IL_12eb:
						num2 = 286;
						base.TopMost = true;
						goto IL_12f9;
						IL_12f9:
						num2 = 287;
						base.TopMost = false;
						goto IL_1308;
						IL_1308:
						num2 = 289;
						base.WindowState = FormWindowState.Normal;
						goto end_IL_0001_3;
						IL_10c3:
						num2 = 254;
						currentRow = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
						goto IL_10e6;
						IL_10e6:
						num2 = 255;
						if (!BuildForm.VerifySchemaRow(ref currentRow, 1, ref MyMsg))
						{
							goto IL_1104;
						}
						goto IL_1124;
						IL_1104:
						num2 = 256;
						MyMsg = " - Unable to Parse Nodes (" + MyMsg + ")";
						goto IL_131c;
						IL_1124:
						num2 = 259;
						Globals_Renamed.AllSchema[num4].Name = currentRow[0];
						goto IL_113f;
						IL_113f:
						num2 = 260;
						MyErr = Globals_Renamed.AllSchema[num4].Name;
						goto IL_1158;
						IL_1158:
						num2 = 261;
						left = Strings.Mid(MyErr, 1, 1);
						goto IL_1169;
						IL_1169:
						num2 = 262;
						if (Operators.CompareString(left, "!", TextCompare: false) != 0)
						{
							goto IL_1185;
						}
						goto IL_1194;
						IL_1185:
						num2 = 263;
						num4++;
						goto IL_1194;
						IL_1194:
						num2 = 265;
						currentRow = null;
						goto IL_119e;
						IL_0e01:
						num2 = 214;
						currentRow = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
						goto IL_0e24;
						IL_0e24:
						num2 = 215;
						if (!BuildForm.VerifySchemaRow(ref currentRow, 5, ref MyMsg))
						{
							goto IL_0e42;
						}
						goto IL_0e62;
						IL_0e42:
						num2 = 216;
						MyMsg = " - Unable to Parse Nodes (" + MyMsg + ")";
						goto IL_131c;
						IL_0e62:
						num2 = 219;
						Globals_Renamed.AllNodes[num4].Name = currentRow[0];
						goto IL_0e7d;
						IL_0e7d:
						num2 = 220;
						Globals_Renamed.AllNodes[num4].ORCL = currentRow[1];
						goto IL_0e98;
						IL_0e98:
						num2 = 221;
						Globals_Renamed.AllNodes[num4].DBDATA = currentRow[2];
						goto IL_0eb3;
						IL_0eb3:
						num2 = 222;
						Globals_Renamed.AllNodes[num4].Desc = currentRow[3];
						goto IL_0ece;
						IL_0ece:
						num2 = 223;
						Globals_Renamed.AllNodes[num4].Display = currentRow[4];
						goto IL_0ee9;
						IL_0ee9:
						num2 = 224;
						MyErr = Globals_Renamed.AllNodes[num4].Name;
						goto IL_0f02;
						IL_0f02:
						num2 = 225;
						left = Strings.Mid(MyErr, 1, 1);
						goto IL_0f13;
						IL_0f13:
						num2 = 226;
						if (Operators.CompareString(left, "!", TextCompare: false) != 0)
						{
							goto IL_0f2f;
						}
						goto IL_0f3e;
						IL_0f2f:
						num2 = 227;
						num4++;
						goto IL_0f3e;
						IL_0f3e:
						num2 = 229;
						currentRow = null;
						goto IL_0f48;
						IL_0c86:
						num2 = 189;
						if (Operators.CompareString(Globals_Renamed.Tables[num4].ObjectType, "Utilities", TextCompare: false) == 0)
						{
							goto IL_0cb1;
						}
						goto IL_0cf7;
						IL_0cb1:
						num2 = 190;
						Globals_Renamed.Tables[num4].Tag = Globals_Renamed.Tables[num4].DatabaseType;
						goto IL_0cd9;
						IL_0cd9:
						num2 = 191;
						Globals_Renamed.Tables[num4].DatabaseType = "Utilities";
						goto IL_0cf7;
						IL_0cf7:
						num2 = 193;
						num4 = (short)unchecked(num4 + 1);
						goto IL_0d04;
						IL_073e:
						num2 = 128;
						currentRow = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
						goto IL_0761;
						IL_0761:
						num2 = 129;
						if (!BuildForm.VerifySchemaRow(ref currentRow, 17, ref MyMsg))
						{
							goto IL_0780;
						}
						goto IL_07a0;
						IL_0780:
						num2 = 130;
						MyMsg = " - Unable to Parse Views (" + MyMsg + ")";
						goto IL_131c;
						IL_131c:
						num2 = 291;
						Interaction.MsgBox(text + text2 + MyMsg, MsgBoxStyle.Critical, "Node Error");
						goto IL_1339;
						IL_1339:
						num2 = 292;
						if (flag)
						{
							goto IL_1347;
						}
						goto IL_138e;
						IL_1347:
						num2 = 293;
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_1365;
						IL_1365:
						num2 = 294;
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_1383;
						IL_1383:
						num2 = 295;
						flag = false;
						goto IL_138e;
						IL_138e:
						num2 = 297;
						base.WindowState = FormWindowState.Normal;
						goto end_IL_0001_3;
						IL_07a0:
						num2 = 133;
						Globals_Renamed.Tables[num4].ObjectType = currentRow[0];
						goto IL_07bb;
						IL_07bb:
						num2 = 134;
						Globals_Renamed.Tables[num4].DatabaseType = currentRow[1];
						goto IL_07d6;
						IL_07d6:
						num2 = 135;
						Globals_Renamed.Tables[num4].lbl0 = currentRow[2];
						goto IL_07f1;
						IL_07f1:
						num2 = 136;
						Globals_Renamed.Tables[num4].lbl1 = currentRow[3];
						goto IL_080c;
						IL_080c:
						num2 = 137;
						Globals_Renamed.Tables[num4].lbl2 = currentRow[4];
						goto IL_0827;
						IL_0827:
						num2 = 138;
						Globals_Renamed.Tables[num4].lbl3 = currentRow[5];
						goto IL_0842;
						IL_0842:
						num2 = 139;
						Globals_Renamed.Tables[num4].lbl4 = currentRow[6];
						goto IL_085d;
						IL_085d:
						num2 = 140;
						Globals_Renamed.Tables[num4].lbl5 = currentRow[7];
						goto IL_0878;
						IL_0878:
						num2 = 141;
						Globals_Renamed.Tables[num4].lbl6 = currentRow[8];
						goto IL_0893;
						IL_0893:
						num2 = 142;
						MyErr = currentRow[9];
						goto IL_08a0;
						IL_08a0:
						num2 = 143;
						if (Strings.Len(MyErr) == 1)
						{
							goto IL_08b6;
						}
						goto IL_090d;
						IL_08b6:
						num2 = 144;
						Globals_Renamed.Tables[num4].lbl7 = MyErr;
						goto IL_08cf;
						IL_08cf:
						num2 = 145;
						Globals_Renamed.Tables[num4].lbl9 = "N";
						goto IL_08eb;
						IL_08eb:
						num2 = 146;
						Globals_Renamed.Tables[num4].lbl10 = "N";
						goto IL_0a5a;
						IL_090d:
						num2 = 148;
						if (Strings.Len(MyErr) >= 3)
						{
							goto IL_0926;
						}
						goto IL_098c;
						IL_0926:
						num2 = 149;
						Globals_Renamed.Tables[num4].lbl10 = Strings.Mid(MyErr, 3, 1);
						goto IL_0946;
						IL_0946:
						num2 = 150;
						Globals_Renamed.Tables[num4].lbl9 = Strings.Mid(MyErr, 2, 1);
						goto IL_0966;
						IL_0966:
						num2 = 151;
						Globals_Renamed.Tables[num4].lbl7 = Strings.Mid(MyErr, 1, 1);
						goto IL_0a5a;
						IL_098c:
						num2 = 153;
						if (Strings.Len(MyErr) >= 2)
						{
							goto IL_09a5;
						}
						goto IL_0a05;
						IL_09a5:
						num2 = 154;
						Globals_Renamed.Tables[num4].lbl10 = "N";
						goto IL_09c1;
						IL_09c1:
						num2 = 155;
						Globals_Renamed.Tables[num4].lbl9 = Strings.Mid(MyErr, 2, 1);
						goto IL_09e1;
						IL_09e1:
						num2 = 156;
						Globals_Renamed.Tables[num4].lbl7 = Strings.Mid(MyErr, 1, 1);
						goto IL_0a5a;
						IL_0a05:
						num2 = 158;
						Globals_Renamed.Tables[num4].lbl7 = "N";
						goto IL_0a21;
						IL_0a21:
						num2 = 159;
						Globals_Renamed.Tables[num4].lbl9 = "N";
						goto IL_0a3d;
						IL_0a3d:
						num2 = 160;
						Globals_Renamed.Tables[num4].lbl10 = "N";
						goto IL_0a5a;
						IL_0a5a:
						num2 = 162;
						MyErr = "";
						goto IL_0a67;
						IL_0a67:
						num2 = 163;
						Globals_Renamed.Tables[num4].lbl8 = currentRow[10];
						goto IL_0a83;
						IL_0a83:
						num2 = 164;
						Globals_Renamed.Tables[num4].Show = currentRow[11];
						goto IL_0a9f;
						IL_0a9f:
						num2 = 165;
						Globals_Renamed.Tables[num4].Name = currentRow[12];
						goto IL_0abb;
						IL_0abb:
						num2 = 166;
						Globals_Renamed.Tables[num4].Display = currentRow[13];
						goto IL_0ad7;
						IL_0ad7:
						num2 = 167;
						Globals_Renamed.Tables[num4].TableType = currentRow[14];
						goto IL_0af3;
						end_IL_0001_2:
						break;
					}
					num2 = 300;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6305;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmMain_FormClosing(object eventSender, FormClosingEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		int num3 = default(int);
		short num5 = default(short);
		bool cancel = default(bool);
		short num6 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					CloseReason closeReason;
					short num7;
					switch (try0001_dispatch)
					{
					default:
						num2 = 1;
						if (eventArgs.Cancel)
						{
							goto end_IL_0001;
						}
						goto IL_0015;
					case 456:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0001_2;
							}
							int num4 = unchecked(num + 1);
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 3:
								goto IL_0015;
							case 4:
								goto IL_0019;
							case 5:
								goto IL_0023;
							case 6:
								goto IL_002d;
							case 7:
								goto IL_0036;
							case 8:
								goto IL_003b;
							case 9:
								goto IL_004e;
							case 10:
								goto IL_0057;
							case 11:
								goto IL_0066;
							case 12:
								goto IL_006f;
							case 13:
								goto IL_0080;
							case 15:
							case 16:
							case 17:
								goto IL_008f;
							case 18:
								goto IL_009e;
							case 19:
								goto IL_00af;
							case 20:
								goto IL_00b8;
							case 21:
								goto IL_00c7;
							case 22:
								goto IL_00d8;
							case 23:
								goto IL_00e9;
							case 25:
							case 26:
								goto IL_00f3;
							case 27:
								goto IL_0104;
							case 28:
								goto IL_0110;
							case 29:
								goto IL_011c;
							case 30:
							case 31:
								goto end_IL_0001_3;
							default:
								goto end_IL_0001_2;
							case 2:
							case 14:
							case 24:
							case 32:
								goto end_IL_0001;
							}
							goto default;
						}
						IL_00f3:
						num2 = 26;
						FrmSQLQuery[num5].Dispose();
						goto IL_0104;
						IL_0104:
						num2 = 27;
						FrmSQLQuery[num5] = null;
						goto IL_0110;
						IL_00e9:
						num2 = 23;
						cancel = true;
						goto end_IL_0001;
						IL_0110:
						num2 = 28;
						num5 = (short)unchecked(num5 + -1);
						goto IL_0118;
						IL_0015:
						num2 = 3;
						num5 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 4;
						cancel = eventArgs.Cancel;
						goto IL_0023;
						IL_0023:
						num2 = 5;
						closeReason = eventArgs.CloseReason;
						goto IL_002d;
						IL_002d:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0036;
						IL_0036:
						num2 = 7;
						num6 = 0;
						goto IL_003b;
						IL_003b:
						num2 = 8;
						if (BuildForm.FormIsLoaded("FrmMultiQuery", 1))
						{
							goto IL_004e;
						}
						goto IL_008f;
						IL_004e:
						num2 = 9;
						Globals_Renamed.QueryCancel = 0;
						goto IL_0057;
						IL_0057:
						num2 = 10;
						FrmMultiQuery.Close();
						goto IL_0066;
						IL_0066:
						num2 = 11;
						Application.DoEvents();
						goto IL_006f;
						IL_006f:
						num2 = 12;
						if (Globals_Renamed.QueryCancel == -1)
						{
							goto IL_0080;
						}
						goto IL_008f;
						IL_0080:
						num2 = 13;
						cancel = true;
						goto end_IL_0001;
						IL_008f:
						num2 = 17;
						num6 = BuildForm.CountFormIsLoaded("FrmSQLQuerya");
						goto IL_009e;
						IL_009e:
						num2 = 18;
						if (num6 < 1)
						{
							break;
						}
						goto IL_00af;
						IL_00af:
						num2 = 19;
						Globals_Renamed.QueryCancel = 0;
						goto IL_00b8;
						IL_00b8:
						num2 = 20;
						num7 = (short)(num6 - 1);
						num5 = num7;
						goto IL_0118;
						IL_0118:
						if (num5 >= 0)
						{
							goto IL_00c7;
						}
						goto IL_011c;
						IL_011c:
						num2 = 29;
						Application.DoEvents();
						break;
						IL_00c7:
						num2 = 21;
						FrmSQLQuery[num5].Close();
						goto IL_00d8;
						IL_00d8:
						num2 = 22;
						if (Globals_Renamed.QueryCancel == -1)
						{
							goto IL_00e9;
						}
						goto IL_00f3;
						end_IL_0001_3:
						break;
					}
					num2 = 31;
					eventArgs.Cancel = cancel;
					break;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 456;
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

	private void FrmMain_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string lpApplicationName = default(string);
		short num6 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text;
				int num7;
				int num8;
				int num9;
				string lpString;
				string lpKeyName;
				ToolStripMenuItem mnuFile;
				int num10;
				int num5;
				int CSV_Connected;
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
							goto IL_0026;
						case 7:
							goto IL_0033;
						case 8:
							goto IL_003b;
						case 9:
							goto IL_0070;
						case 10:
							goto IL_00a6;
						case 11:
							goto IL_00dc;
						case 12:
							goto IL_0112;
						case 13:
							goto IL_0120;
						case 14:
							goto IL_0131;
						case 15:
							goto IL_0140;
						case 16:
							goto IL_0151;
						case 17:
							goto IL_0160;
						case 18:
							goto IL_0171;
						case 19:
							goto IL_0196;
						case 20:
							goto IL_01a7;
						case 21:
							goto IL_01c1;
						case 22:
							goto IL_01cf;
						case 23:
						case 24:
						case 25:
							goto IL_01e2;
						case 26:
							goto IL_01f3;
						case 27:
							goto IL_01fc;
						case 28:
							goto IL_0205;
						case 29:
							goto IL_021b;
						case 30:
							goto IL_0231;
						case 31:
							goto IL_0247;
						case 32:
							goto IL_025d;
						case 33:
							goto IL_0273;
						case 34:
							goto IL_027d;
						case 35:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 36:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_025d:
					num2 = 32;
					Array.Clear(Globals_Renamed.SubQueries, 0, Globals_Renamed.SubQueries.Length);
					goto IL_0273;
					IL_0273:
					num2 = 33;
					BuildForm.Clear_Global_Vars(1);
					goto IL_027d;
					IL_0247:
					num2 = 31;
					Array.Clear(Globals_Renamed.AllSchema, 0, Globals_Renamed.AllSchema.Length);
					goto IL_025d;
					IL_027d:
					num2 = 34;
					Cursor.Current = Cursors.Default;
					break;
					IL_000b:
					num2 = 2;
					lpApplicationName = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					num6 = 0;
					goto IL_0026;
					IL_0026:
					num2 = 6;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0033;
					IL_0033:
					num2 = 7;
					lpApplicationName = "MRU List";
					goto IL_003b;
					IL_003b:
					num2 = 8;
					lpKeyName = "MRUII_1";
					lpString = (mnuFile = MnuFile0).Text;
					num7 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpString, ref Globals_Renamed.IniFileName);
					mnuFile.Text = lpString;
					num5 = num7;
					goto IL_0070;
					IL_0070:
					num2 = 9;
					lpString = "MRUII_2";
					lpKeyName = (mnuFile = MnuFile1).Text;
					num8 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpString, ref lpKeyName, ref Globals_Renamed.IniFileName);
					mnuFile.Text = lpKeyName;
					num5 = num8;
					goto IL_00a6;
					IL_00a6:
					num2 = 10;
					lpKeyName = "MRUII_3";
					lpString = (mnuFile = MnuFile2).Text;
					num9 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpString, ref Globals_Renamed.IniFileName);
					mnuFile.Text = lpString;
					num5 = num9;
					goto IL_00dc;
					IL_00dc:
					num2 = 11;
					lpString = "MRUII_4";
					lpKeyName = (mnuFile = MnuFile3).Text;
					num10 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpString, ref lpKeyName, ref Globals_Renamed.IniFileName);
					mnuFile.Text = lpKeyName;
					num5 = num10;
					goto IL_0112;
					IL_0112:
					num2 = 12;
					BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
					goto IL_0120;
					IL_0120:
					num2 = 13;
					if (Globals_Renamed.Excel_Connected == 1)
					{
						goto IL_0131;
					}
					goto IL_0140;
					IL_0131:
					num2 = 14;
					BuildForm.Invoke_Excel(0, "");
					goto IL_0140;
					IL_0140:
					num2 = 15;
					if (Globals_Renamed.Excel_Connected2 == 1)
					{
						goto IL_0151;
					}
					goto IL_0160;
					IL_0151:
					num2 = 16;
					Test.Invoke_Excel2(0, "");
					goto IL_0160;
					IL_0160:
					num2 = 17;
					if (Globals_Renamed.CSV_ConnectedM == 1)
					{
						goto IL_0171;
					}
					goto IL_0196;
					IL_0171:
					num2 = 18;
					CSV_Connected = Globals_Renamed.CSV_ConnectedM;
					BuildForm.Invoke_CSVViewer2(0, "META", ref Globals_Renamed.CSV_hwndProcessM, ref CSV_Connected);
					Globals_Renamed.CSV_ConnectedM = checked((short)CSV_Connected);
					goto IL_0196;
					IL_0196:
					num2 = 19;
					if (Globals_Renamed.JMP_Connected == 1)
					{
						goto IL_01a7;
					}
					goto IL_01e2;
					IL_01a7:
					num2 = 20;
					num6 = checked((short)Interaction.MsgBox("Do you want to close the JMP session opened by SQLPathFinder?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Close JMP?"));
					goto IL_01c1;
					IL_01c1:
					num2 = 21;
					if (num6 == 6)
					{
						goto IL_01cf;
					}
					goto IL_01e2;
					IL_01cf:
					num2 = 22;
					BuildForm.Invoke_JMP(0, "");
					goto IL_01e2;
					IL_01e2:
					num2 = 25;
					if (Globals_Renamed.gDelBatFile > 0)
					{
						goto IL_01f3;
					}
					goto IL_01fc;
					IL_01f3:
					num2 = 26;
					General_Procedures.DelBatFile();
					goto IL_01fc;
					IL_01fc:
					num2 = 27;
					Globals_Renamed.g_UIDesc = null;
					goto IL_0205;
					IL_0205:
					num2 = 28;
					Array.Clear(Globals_Renamed.Tables, 0, Globals_Renamed.Tables.Length);
					goto IL_021b;
					IL_021b:
					num2 = 29;
					Array.Clear(Globals_Renamed.GridQuery_Curr, 0, Globals_Renamed.GridQuery_Curr.Length);
					goto IL_0231;
					IL_0231:
					num2 = 30;
					Array.Clear(Globals_Renamed.AllNodes, 0, Globals_Renamed.AllNodes.Length);
					goto IL_0247;
					end_IL_0001_2:
					break;
				}
				num2 = 35;
				Application.DoEvents();
				break;
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

	public void mnuCloseJMP_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_JMP(0, "");
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

	public void mnuHelpHTML_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myHTMLFile = default(string);
		while (true)
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
							goto IL_003b;
						case 6:
							goto IL_0049;
						case 7:
							goto IL_0071;
						case 9:
							goto IL_007f;
						case 10:
							goto IL_00a8;
						case 12:
							goto IL_00b4;
						case 13:
							goto IL_00dd;
						case 15:
							goto IL_00e9;
						case 16:
							goto IL_0112;
						case 5:
						case 8:
						case 11:
						case 14:
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00dd:
					num2 = 13;
					myHTMLFile = "https://wiki.ith.intel.com/display/SQLPathFinder/Training";
					break;
					IL_00e9:
					num2 = 15;
					if (!Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(eventSender, null, "tag", new object[0], null, null, null), "CHG", TextCompare: false))
					{
						break;
					}
					goto IL_0112;
					IL_00b4:
					num2 = 12;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(eventSender, null, "tag", new object[0], null, null, null), "TRAIN", TextCompare: false))
					{
						goto IL_00dd;
					}
					goto IL_00e9;
					IL_0112:
					num2 = 16;
					myHTMLFile = "https://wiki.ith.intel.com/display/SQLPathFinder/Change+Requests";
					break;
					IL_000b:
					num2 = 2;
					myHTMLFile = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(eventSender, null, "tag", new object[0], null, null, null), "WIKI", TextCompare: false))
					{
						goto IL_003b;
					}
					goto IL_0049;
					IL_003b:
					num2 = 4;
					myHTMLFile = "https://wiki.ith.intel.com/display/SQLPathFinder/SQLPathFinder+Home";
					break;
					IL_0049:
					num2 = 6;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(eventSender, null, "tag", new object[0], null, null, null), "SH", TextCompare: false))
					{
						goto IL_0071;
					}
					goto IL_007f;
					IL_0071:
					num2 = 7;
					myHTMLFile = "https://wiki.ith.intel.com/display/SQLPathFinder/ScriptHost+Web+Servers";
					break;
					IL_007f:
					num2 = 9;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(eventSender, null, "tag", new object[0], null, null, null), "REV", TextCompare: false))
					{
						goto IL_00a8;
					}
					goto IL_00b4;
					IL_00a8:
					num2 = 10;
					myHTMLFile = "https://wiki.ith.intel.com/display/SQLPathFinder/SQLPathFinder+Revision+History";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				BuildForm.Invoke_IE(myHTMLFile);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
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
	}

	public void mnuJMP_Click(object eventSender, EventArgs eventArgs)
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
				case 89:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0018;
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
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0018;
					IL_0018:
					num2 = 3;
					BuildForm.Invoke_JMP(1, "");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 89;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuTxt_Click(object eventSender, EventArgs eventArgs)
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
				case 71:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				BuildForm.Invoke_Txt(Conversions.ToString(mnuTxtView.Tag), 1, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 71;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void MnuCloseExcel_Click(object eventSender, EventArgs eventArgs)
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
				Test.Invoke_Excel3(0, "");
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

	public void MnuConfigure_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		frmconfigure frmconfigure2 = default(frmconfigure);
		while (true)
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
					frmconfigure2 = new frmconfigure();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				frmconfigure2.ShowDialog();
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

	public void mnuConnect_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_SQL_Emulator();
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

	public void mnuDisconnect_Click(object eventSender, EventArgs eventArgs)
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
				case 71:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 71;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuExcel_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myStr = default(string);
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
				case 182:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0041;
						case 6:
							goto IL_0049;
						case 7:
							goto IL_0053;
						case 8:
							goto IL_0060;
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
					myStr = Globals_Renamed.ExcelPending;
					goto IL_0049;
					IL_0049:
					num2 = 6;
					myStr = BuildForm.Replace_Globals(myStr, 1);
					goto IL_0053;
					IL_0060:
					num2 = 8;
					Test.Invoke_Excel3(1, "");
					break;
					IL_0053:
					num2 = 7;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0060;
					IL_000b:
					num2 = 2;
					myStr = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					if (Globals_Renamed.ExcelPending != null && Operators.CompareString(Globals_Renamed.ExcelPending, "", TextCompare: false) != 0)
					{
						goto IL_0041;
					}
					goto IL_0049;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 182;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void MnuHelPAbout_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmAbout frmAbout = default(FrmAbout);
		while (true)
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
					frmAbout = new FrmAbout();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				frmAbout.ShowDialog();
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

	public void mnuNodes_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmNode frmNode = default(FrmNode);
		while (true)
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
					frmNode = new FrmNode();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				frmNode.ShowDialog();
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

	public void MnuSpawnVAX_Click(object eventSender, EventArgs eventArgs)
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
				case 118:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_0035;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						goto IL_0024;
					}
					goto IL_0035;
					IL_0024:
					num2 = 3;
					BuildForm.Invoke_SQL_Emulator();
					goto end_IL_0001_3;
					IL_0035:
					num2 = 5;
					num5 = 0;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				num5 = General_Procedures.Activate_Emulator_2();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 118;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public void mnuExit_Click(object eventSender, EventArgs eventArgs)
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 324:
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
								goto IL_0025;
							case 5:
								goto IL_0032;
							case 6:
								goto IL_0041;
							case 7:
								goto IL_0049;
							case 8:
								goto IL_005b;
							case 9:
								goto IL_0063;
							case 12:
								goto IL_007a;
							case 13:
								goto IL_008d;
							case 14:
								goto IL_009b;
							case 18:
								goto IL_00ac;
							case 19:
								goto IL_00b5;
							case 20:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 10:
							case 11:
							case 15:
							case 16:
							case 17:
							case 21:
							case 22:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_009b:
						num2 = 14;
						Set_g_FrmIdx_2();
						goto end_IL_0001_3;
						IL_00ac:
						num2 = 18;
						Globals_Renamed.QueryCancel = 0;
						goto IL_00b5;
						IL_008d:
						num2 = 13;
						FrmSQLQuery[num5 - 1] = null;
						goto IL_009b;
						IL_00b5:
						num2 = 19;
						Close();
						break;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						if (Globals_Renamed.g_FrmIdx >= 1)
						{
							goto IL_0025;
						}
						goto IL_00ac;
						IL_0025:
						num2 = 4;
						num5 = BuildForm.CountFormIsLoaded("FrmSQLQuerya");
						goto IL_0032;
						IL_0032:
						num2 = 5;
						if (num5 < 2)
						{
							goto end_IL_0001_3;
						}
						goto IL_0041;
						IL_0041:
						num2 = 6;
						Globals_Renamed.QueryCancel = 0;
						goto IL_0049;
						IL_0049:
						num2 = 7;
						FrmSQLQuery[num5 - 1].Close();
						goto IL_005b;
						IL_005b:
						num2 = 8;
						Application.DoEvents();
						goto IL_0063;
						IL_0063:
						num2 = 9;
						if (Globals_Renamed.QueryCancel == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_007a;
						IL_007a:
						num2 = 12;
						FrmSQLQuery[num5 - 1].Dispose();
						goto IL_008d;
						end_IL_0001_2:
						break;
					}
					num2 = 20;
					if (Globals_Renamed.QueryCancel == 0)
					{
						ProjectData.EndApp();
					}
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 324;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void ClearScreenToolStripMenuItem_Click(object sender, EventArgs e)
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
				BuildForm.Process_Screen("CLS");
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

	private void MnuShowQuery_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string mySVA = default(string);
		string MyData = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string MyData2;
				int num5;
				long num6;
				string text;
				short design_Mode;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 383:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001e;
						case 6:
							goto IL_0027;
						case 7:
							goto IL_0030;
						case 8:
							goto IL_003e;
						case 10:
							goto IL_0058;
						case 12:
							goto IL_007a;
						case 13:
							goto IL_00ab;
						case 14:
							goto IL_00c0;
						case 15:
							goto IL_00d8;
						case 16:
							goto IL_00e2;
						case 9:
						case 11:
						case 17:
						case 18:
						case 19:
						case 20:
							goto IL_00fb;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d8:
					num2 = 15;
					mySVA = "SQL->VA";
					goto IL_00e2;
					IL_00e2:
					num2 = 16;
					BuildForm.HG_Editor(ref MyData, mySVA, "");
					goto IL_00fb;
					IL_00c0:
					num2 = 14;
					if (Operators.CompareString(MyData, "", TextCompare: false) != 0)
					{
						goto IL_00d8;
					}
					goto IL_00fb;
					IL_0058:
					num2 = 10;
					MyData2 = "";
					BuildForm.HG_Editor(ref MyData2, "SQL->VA", "");
					goto IL_00fb;
					IL_000b:
					num2 = 2;
					MyData = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num6 = 0L;
					goto IL_001e;
					IL_001e:
					num2 = 5;
					text = "";
					goto IL_0027;
					IL_0027:
					num2 = 6;
					mySVA = "";
					goto IL_0030;
					IL_0030:
					num2 = 7;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_003e;
					IL_003e:
					num2 = 8;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_0058;
					}
					if (design_Mode == 2)
					{
						goto IL_007a;
					}
					goto IL_00fb;
					IL_00fb:
					num2 = 20;
					MyData = "";
					break;
					IL_007a:
					num2 = 12;
					if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0].GetNodeCount(includeSubTrees: false) >= 1)
					{
						goto IL_00ab;
					}
					goto IL_00fb;
					IL_00ab:
					num2 = 13;
					MyData = BuildSQL.Generate_SQL(0, "");
					goto IL_00c0;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 383;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuMultiQuery_Click(object sender, EventArgs e)
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
				case 283:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 5:
							goto IL_0027;
						case 6:
							goto IL_0045;
						case 7:
							goto IL_0052;
						case 9:
							goto IL_0065;
						case 10:
							goto IL_0087;
						case 12:
							goto IL_009b;
						case 11:
						case 13:
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 4:
						case 8:
						case 15:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0087:
					num2 = 10;
					FrmMultiQuery.WindowState = FormWindowState.Maximized;
					break;
					IL_009b:
					num2 = 12;
					FrmMultiQuery.WindowState = FormWindowState.Normal;
					break;
					IL_0065:
					num2 = 9;
					if (Operators.CompareString(FrmMultiQuery.SaveWindowState, "M", TextCompare: false) == 0)
					{
						goto IL_0087;
					}
					goto IL_009b;
					IL_0045:
					num2 = 6;
					FrmMultiQuery = new FrmMultiQuery();
					goto IL_0052;
					IL_000b:
					num2 = 2;
					if (BuildForm.Test_for_PPQ("load"))
					{
						goto end_IL_0001_3;
					}
					goto IL_0027;
					IL_0027:
					num2 = 5;
					if (FrmMultiQuery == null || FrmMultiQuery.IsDisposed)
					{
						goto IL_0045;
					}
					goto IL_0065;
					IL_0052:
					num2 = 7;
					FrmMultiQuery.Show(this);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				FrmMultiQuery.GridMQ2.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 283;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void MnuAddTables_Click(object sender, EventArgs e)
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
				case 137:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0021;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_003f;
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
					IL_0021:
					num2 = 4;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveQuery = 1;
					goto IL_0035;
					IL_0035:
					num2 = 5;
					base.TopMost = true;
					goto IL_003f;
					IL_0019:
					num2 = 3;
					BuildForm.Add_FrmView();
					goto IL_0021;
					IL_003f:
					num2 = 6;
					base.TopMost = false;
					break;
					IL_000b:
					num2 = 2;
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_0019;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				Application.DoEvents();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 137;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Set_g_FrmIdx(short MyIdx)
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
						errsource = "";
						short num3 = 0;
						short num4 = 0;
						errsource = "FrmMain - Set_g_FrmIdx";
						if (MyIdx == 0)
						{
							Text = "SQLPathFinder 3 (Main - " + Globals_Renamed.gSPFCache + ")";
							if (BuildForm.FormIsLoaded("FrmSQLQuerya", 1))
							{
								FrmSQLQuery[0].WindowState = FormWindowState.Maximized;
								FrmSQLQuery[0].Enabled = true;
							}
							cmdIcon.Items[19].ToolTipText = "Exit";
							mnuSaveQAs.Enabled = true;
							mnuIncNestUtil.Enabled = true;
							FrmSQLQuery[0].f_PrePostName = "";
							FrmSQLQuery[0].f_SPFCacheRet = "";
						}
						else if (MyIdx > 0)
						{
							Text = "SQLPathFinder 3 (Pre/Post Query - " + FrmSQLQuery[MyIdx - 1].f_PrePostName + " - " + Globals_Renamed.gSPFCache + ")";
							short num5 = (short)(MyIdx - 1);
							for (num4 = 0; num4 <= num5; num4 = (short)unchecked(num4 + 1))
							{
								FrmSQLQuery[num4].Enabled = false;
							}
							cmdIcon.Items[19].ToolTipText = "Exit Pre/Post Query";
							if (mnuEmbedPrePost.Checked)
							{
								mnuSaveQAs.Enabled = false;
								FrmSQLQuery[MyIdx].fTitle = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".vg2";
								FrmSQLQuery[MyIdx].Set_QueryTitle();
							}
							mnuIncNestUtil.Enabled = false;
						}
						Globals_Renamed.g_FrmIdx = MyIdx;
						if (Globals_Renamed.g_FrmIdx > 0)
						{
							FrmSQLQuery[Globals_Renamed.g_FrmIdx].BackColor = Color.Beige;
						}
						goto end_IL_0001;
					}
					case 542:
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
				try0001_dispatch = 542;
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

	public void Force_g_FrmIdx(short SetIdx)
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
					ProjectData.ClearProjectError();
					num2 = 2;
					if (SetIdx < 0)
					{
						goto end_IL_0001;
					}
					errsource = "FrmMain - Force_g_FrmIdx";
					if (SetIdx == 0)
					{
						Text = "SQLPathFinder 3 (Main - " + Globals_Renamed.gSPFCache + ")";
						cmdIcon.Items[19].ToolTipText = "Exit";
						mnuSaveQAs.Enabled = true;
						mnuIncNestUtil.Enabled = true;
						if (Globals_Renamed.Design_Mode != 0)
						{
							SetMenuOpt(FrmSQLQuery[SetIdx].out_inline, FrmSQLQuery[SetIdx].MacroFile);
						}
					}
					else if (SetIdx >= 1)
					{
						Text = "SQLPathFinder 3 (Pre/Post Query - " + FrmSQLQuery[checked(SetIdx - 1)].f_PrePostName + " - " + Globals_Renamed.gSPFCache + ")";
						cmdIcon.Items[19].ToolTipText = "Exit Pre/Post Query";
						if (mnuEmbedPrePost.Checked)
						{
							mnuSaveQAs.Enabled = false;
							FrmSQLQuery[SetIdx].fTitle = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".vg2";
							FrmSQLQuery[SetIdx].Set_QueryTitle();
						}
						mnuIncNestUtil.Enabled = false;
						FrmSQLQuery[SetIdx].BackColor = Color.Beige;
					}
					if (!Information.IsNothing(FrmSQLQuery[SetIdx]) && !FrmSQLQuery[SetIdx].Enabled)
					{
						FrmSQLQuery[SetIdx].Enabled = true;
						FrmSQLQuery[SetIdx].WindowState = FormWindowState.Maximized;
						FrmSQLQuery[SetIdx].Show();
					}
					Globals_Renamed.g_FrmIdx = SetIdx;
					goto end_IL_0001;
				case 543:
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
				try0001_dispatch = 543;
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

	public void Set_g_FrmIdx_2()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num4 = default(short);
		string errsource = default(string);
		short num6 = default(short);
		short num7 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					short num8;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 1082:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
							case 3:
								break;
							case 1:
								goto IL_035e;
							default:
								goto end_IL_0001;
							}
							goto IL_032d;
						}
						IL_0206:
						num2 = 29;
						FrmSQLQuery[num4] = null;
						goto IL_0215;
						IL_0215:
						num2 = 31;
						num4 = (short)unchecked(num4 + 1);
						goto IL_021f;
						IL_01f4:
						num2 = 28;
						FrmSQLQuery[num4].Dispose();
						goto IL_0206;
						IL_035e:
						num5 = unchecked(num + 1);
						num = 0;
						switch (num5)
						{
						case 1:
							break;
						case 2:
							goto IL_000a;
						case 3:
							goto IL_0012;
						case 4:
							goto IL_0017;
						case 5:
							goto IL_001c;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_003d;
						case 8:
							goto IL_0084;
						case 9:
							goto IL_0092;
						case 10:
							goto IL_00a5;
						case 11:
							goto IL_00b5;
						case 12:
							goto IL_00d5;
						case 13:
							goto IL_00e9;
						case 14:
							goto IL_00f9;
						case 15:
							goto IL_0115;
						case 16:
							goto IL_013c;
						case 17:
						case 18:
						case 19:
							goto IL_0154;
						case 20:
							goto IL_0164;
						case 21:
							goto IL_0171;
						case 22:
							goto IL_018e;
						case 24:
							goto IL_01af;
						case 25:
							goto IL_01b8;
						case 26:
							goto IL_01c8;
						case 27:
							goto IL_01e2;
						case 28:
							goto IL_01f4;
						case 29:
							goto IL_0206;
						case 30:
						case 31:
							goto IL_0215;
						case 32:
							goto IL_0224;
						case 33:
							goto IL_022d;
						case 34:
							goto IL_023b;
						case 35:
							goto IL_0243;
						case 36:
							goto IL_0261;
						case 37:
							goto IL_0281;
						case 38:
							goto IL_0291;
						case 39:
							goto IL_02a1;
						case 23:
						case 40:
						case 41:
							goto IL_02ab;
						case 42:
							goto IL_02c8;
						case 43:
							goto IL_02e5;
						case 44:
							goto IL_02fb;
						case 45:
							goto IL_0311;
						case 49:
							goto IL_032d;
						case 50:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 46:
						case 47:
						case 48:
						case 51:
							goto end_IL_0001_3;
						}
						goto default;
						IL_032d:
						num2 = 49;
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						break;
						IL_000a:
						num2 = 2;
						errsource = "FrmMain - Set_g_FrmIdx_2";
						goto IL_0012;
						IL_0012:
						num2 = 3;
						num6 = 0;
						goto IL_0017;
						IL_0017:
						num2 = 4;
						num4 = 0;
						goto IL_001c;
						IL_001c:
						num2 = 5;
						num6 = BuildForm.CountFormIsLoaded("FrmSQLQuerya");
						goto IL_002a;
						IL_002a:
						num2 = 6;
						if (num6 >= 2)
						{
							goto IL_003d;
						}
						goto IL_01af;
						IL_003d:
						num2 = 7;
						Text = "SQLPathFinder 3 (Pre/Post Query - " + FrmSQLQuery[num6 - 2].f_PrePostName + " - " + Globals_Renamed.gSPFCache + ")";
						goto IL_0084;
						IL_0084:
						num2 = 8;
						num7 = (short)(num6 - 2);
						num4 = 0;
						goto IL_00af;
						IL_00af:
						if (num4 <= num7)
						{
							goto IL_0092;
						}
						goto IL_00b5;
						IL_00b5:
						num2 = 11;
						cmdIcon.Items[19].ToolTipText = "Exit Pre/Post Query";
						goto IL_00d5;
						IL_00d5:
						num2 = 12;
						if (mnuEmbedPrePost.Checked)
						{
							goto IL_00e9;
						}
						goto IL_0154;
						IL_00e9:
						num2 = 13;
						mnuSaveQAs.Enabled = false;
						goto IL_00f9;
						IL_00f9:
						num2 = 14;
						if (!Information.IsNothing(FrmSQLQuery[num6 - 1]))
						{
							goto IL_0115;
						}
						goto IL_0154;
						IL_0115:
						num2 = 15;
						FrmSQLQuery[num6 - 1].fTitle = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".vg2";
						goto IL_013c;
						IL_013c:
						num2 = 16;
						FrmSQLQuery[num6 - 1].Set_QueryTitle();
						goto IL_0154;
						IL_0154:
						num2 = 19;
						mnuIncNestUtil.Enabled = false;
						goto IL_0164;
						IL_0164:
						num2 = 20;
						Globals_Renamed.g_FrmIdx = (short)(num6 - 1);
						goto IL_0171;
						IL_0171:
						num2 = 21;
						if (!Information.IsNothing(FrmSQLQuery[Globals_Renamed.g_FrmIdx]))
						{
							goto IL_018e;
						}
						goto IL_02ab;
						IL_018e:
						num2 = 22;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].BackColor = Color.Beige;
						goto IL_02ab;
						IL_0092:
						num2 = 9;
						FrmSQLQuery[num4].Enabled = false;
						goto IL_00a5;
						IL_00a5:
						num2 = 10;
						num4 = (short)unchecked(num4 + 1);
						goto IL_00af;
						IL_01af:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_01b8;
						IL_01b8:
						num2 = 25;
						num8 = (short)(num6 + 1);
						num4 = num8;
						goto IL_021f;
						IL_021f:
						if (num4 <= 2)
						{
							goto IL_01c8;
						}
						goto IL_0224;
						IL_0224:
						num2 = 32;
						BuildForm.Garbage_Collect();
						goto IL_022d;
						IL_022d:
						num2 = 33;
						Information.Err().Clear();
						goto IL_023b;
						IL_023b:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_0243;
						IL_0243:
						num2 = 35;
						Text = "SQLPathFinder 3 (Main - " + Globals_Renamed.gSPFCache + ")";
						goto IL_0261;
						IL_0261:
						num2 = 36;
						cmdIcon.Items[19].ToolTipText = "Exit";
						goto IL_0281;
						IL_0281:
						num2 = 37;
						mnuSaveQAs.Enabled = true;
						goto IL_0291;
						IL_0291:
						num2 = 38;
						mnuIncNestUtil.Enabled = true;
						goto IL_02a1;
						IL_02a1:
						num2 = 39;
						Globals_Renamed.g_FrmIdx = 0;
						goto IL_02ab;
						IL_02ab:
						num2 = 41;
						if (Information.IsNothing(FrmSQLQuery[Globals_Renamed.g_FrmIdx]))
						{
							goto end_IL_0001_3;
						}
						goto IL_02c8;
						IL_02c8:
						num2 = 42;
						if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].Enabled)
						{
							goto end_IL_0001_3;
						}
						goto IL_02e5;
						IL_02e5:
						num2 = 43;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].Enabled = true;
						goto IL_02fb;
						IL_02fb:
						num2 = 44;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].WindowState = FormWindowState.Maximized;
						goto IL_0311;
						IL_0311:
						num2 = 45;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].Show();
						goto end_IL_0001_3;
						IL_01c8:
						num2 = 26;
						if (!Information.IsNothing(FrmSQLQuery[num4]))
						{
							goto IL_01e2;
						}
						goto IL_0215;
						IL_01e2:
						num2 = 27;
						FrmSQLQuery[num4].Close();
						goto IL_01f4;
						end_IL_0001_2:
						break;
					}
					num2 = 50;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1082;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public short Save_To_VA(ref string MyQueryToProcess, string MyVAFile, short MyMode, ref bool ContainsMacro, ref string MyInLineValue)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		int num = default(int);
		int num3 = default(int);
		short num4 = default(short);
		short num5 = default(short);
		int num7 = default(int);
		short num8 = default(short);
		short num9 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					string text2;
					short design_Mode;
					switch (try0001_dispatch)
					{
					default:
						num2 = 1;
						text = "";
						goto IL_000b;
					case 2063:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 5:
								break;
							case 3:
								goto IL_0512;
							case 2:
							case 4:
								goto IL_0593;
							case 1:
								goto IL_0613;
							default:
								goto end_IL_0001;
							}
							goto IL_0480;
						}
						IL_0446:
						num2 = 92;
						if (num4 != 1)
						{
							break;
						}
						goto IL_0453;
						IL_0453:
						num2 = 93;
						if (Strings.InStr(Strings.UCase(text), "{START-MACRO}") == 0)
						{
							break;
						}
						goto IL_0471;
						IL_0432:
						num2 = 91;
						FileSystem.FileClose(num5);
						goto IL_0446;
						IL_0613:
						num6 = unchecked(num + 1);
						num = 0;
						switch (num6)
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
							goto IL_0022;
						case 7:
							goto IL_002c;
						case 8:
							goto IL_0032;
						case 9:
							goto IL_003a;
						case 10:
							goto IL_0047;
						case 11:
							goto IL_004f;
						case 12:
							goto IL_005a;
						case 13:
							goto IL_006b;
						case 14:
							goto IL_007f;
						case 15:
							goto IL_0093;
						case 16:
							goto IL_009f;
						case 18:
							goto IL_00ac;
						case 19:
							goto IL_00b3;
						case 21:
							goto IL_00ce;
						case 22:
							goto IL_00db;
						case 24:
							goto IL_00ec;
						case 25:
							goto IL_00f9;
						case 28:
							goto IL_010e;
						case 29:
							goto IL_0117;
						case 30:
							goto IL_0121;
						case 31:
							goto IL_0136;
						case 32:
							goto IL_0147;
						case 33:
							goto IL_0150;
						case 34:
							goto IL_0162;
						case 35:
							goto IL_016f;
						case 36:
							goto IL_017d;
						case 37:
							goto IL_0185;
						case 39:
							goto IL_0197;
						case 17:
						case 20:
						case 23:
						case 26:
						case 27:
						case 38:
						case 41:
						case 42:
						case 43:
						case 44:
							goto IL_01a6;
						case 45:
							goto IL_01af;
						case 46:
							goto IL_01bd;
						case 48:
						case 49:
							goto IL_01c9;
						case 50:
							goto IL_01de;
						case 51:
							goto IL_01f5;
						case 52:
							goto IL_0214;
						case 53:
							goto IL_022a;
						case 54:
							goto IL_026c;
						case 56:
							goto IL_0275;
						case 59:
							goto IL_027f;
						case 60:
							goto IL_0298;
						case 55:
						case 57:
						case 58:
						case 61:
						case 62:
						case 63:
							goto IL_02a0;
						case 64:
							goto IL_02ad;
						case 65:
							goto IL_02be;
						case 66:
							goto IL_02dd;
						case 67:
							goto IL_02f1;
						case 68:
							goto IL_02fb;
						case 69:
						case 70:
						case 71:
							goto IL_030c;
						case 72:
							goto IL_0312;
						case 73:
							goto IL_032c;
						case 74:
							goto IL_033e;
						case 75:
							goto IL_0358;
						case 76:
							goto IL_036a;
						case 77:
							goto IL_0384;
						case 78:
						case 79:
							goto IL_0393;
						case 80:
							goto IL_03a5;
						case 81:
							goto IL_03b6;
						case 83:
							goto IL_03c3;
						case 82:
						case 84:
						case 85:
							goto IL_03d4;
						case 86:
							goto IL_03dd;
						case 87:
							goto IL_03f9;
						case 88:
							goto IL_0401;
						case 89:
							goto IL_040c;
						case 90:
							goto IL_041c;
						case 91:
							goto IL_0432;
						case 92:
							goto IL_0446;
						case 93:
							goto IL_0453;
						case 94:
							goto IL_0471;
						case 97:
							goto IL_0480;
						case 98:
							goto IL_04bf;
						case 99:
							goto IL_04cd;
						case 100:
							goto IL_04d6;
						case 101:
							goto IL_04ea;
						case 102:
							goto IL_04f8;
						case 105:
							goto IL_0506;
						case 104:
						case 107:
						case 108:
							goto IL_0512;
						case 109:
							goto IL_0552;
						case 110:
							goto IL_0560;
						case 111:
							goto IL_0569;
						case 112:
							goto IL_057d;
						case 113:
							goto IL_058b;
						case 115:
							goto IL_0593;
						case 116:
							goto IL_05a0;
						case 117:
						case 118:
							goto IL_05e2;
						case 119:
							goto IL_05f0;
						case 120:
							goto IL_05f9;
						case 40:
						case 47:
						case 95:
						case 96:
						case 103:
						case 106:
						case 114:
						case 121:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 122:
						case 123:
							goto end_IL_0001_3;
						}
						goto default;
						IL_0593:
						num2 = 115;
						if (MyMode == 0)
						{
							goto IL_05a0;
						}
						goto IL_05e2;
						IL_05a0:
						num2 = 116;
						Interaction.MsgBox("Error processing query script: " + MyQueryToProcess + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Processing Query");
						goto IL_05e2;
						IL_05e2:
						num2 = 118;
						Information.Err().Clear();
						goto IL_05f0;
						IL_05f0:
						ProjectData.ClearProjectError();
						num3 = -5;
						goto IL_05f9;
						IL_05f9:
						num2 = 120;
						num4 = 0;
						break;
						IL_0512:
						num2 = 108;
						Interaction.MsgBox("Error opening SPFSQL query file: " + MyQueryToProcess + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Opening SPFSQL Query File");
						goto IL_0552;
						IL_0552:
						num2 = 109;
						Information.Err().Clear();
						goto IL_0560;
						IL_0560:
						ProjectData.ClearProjectError();
						num3 = -4;
						goto IL_0569;
						IL_0569:
						num2 = 111;
						FileSystem.FileClose(num5);
						goto IL_057d;
						IL_057d:
						num2 = 112;
						Information.Err().Clear();
						goto IL_058b;
						IL_058b:
						num2 = 113;
						num4 = 0;
						break;
						IL_0480:
						num2 = 97;
						Interaction.MsgBox("Error saving query script: " + MyVAFile + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Saving File");
						goto IL_04bf;
						IL_04bf:
						num2 = 98;
						Information.Err().Clear();
						goto IL_04cd;
						IL_04cd:
						ProjectData.ClearProjectError();
						num3 = -3;
						goto IL_04d6;
						IL_04d6:
						num2 = 100;
						FileSystem.FileClose(num5);
						goto IL_04ea;
						IL_04ea:
						num2 = 101;
						Information.Err().Clear();
						goto IL_04f8;
						IL_04f8:
						num2 = 102;
						num4 = 0;
						break;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0014;
						IL_0014:
						num2 = 3;
						num7 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 4;
						num8 = 0;
						goto IL_001e;
						IL_001e:
						num2 = 5;
						num4 = 1;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						MyInLineValue = "N";
						goto IL_002c;
						IL_002c:
						num2 = 7;
						ContainsMacro = false;
						goto IL_0032;
						IL_0032:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_003a;
						IL_003a:
						num2 = 9;
						if (MyMode == 2)
						{
							goto IL_0047;
						}
						goto IL_00ac;
						IL_0047:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_004f;
						IL_004f:
						num2 = 11;
						num5 = (short)FileSystem.FreeFile();
						goto IL_005a;
						IL_005a:
						num2 = 12;
						FileSystem.FileOpen(num5, MyQueryToProcess, OpenMode.Input, OpenAccess.Read, OpenShare.Shared);
						goto IL_006b;
						IL_006b:
						num2 = 13;
						text = FileSystem.InputString(num5, (int)FileSystem.LOF(num5));
						goto IL_007f;
						IL_007f:
						num2 = 14;
						FileSystem.FileClose(num5);
						goto IL_0093;
						IL_0093:
						num2 = 15;
						text = BuildForm.SubStitute_Nodes_SQL(text);
						goto IL_009f;
						IL_009f:
						num2 = 16;
						num9 = 1;
						goto IL_01a6;
						IL_00ac:
						num2 = 18;
						num9 = 1;
						goto IL_00b3;
						IL_00b3:
						num2 = 19;
						design_Mode = Globals_Renamed.Design_Mode;
						if (design_Mode == 0)
						{
							goto IL_00ce;
						}
						if (design_Mode == 2)
						{
							goto IL_010e;
						}
						goto IL_01a6;
						IL_0471:
						num2 = 94;
						ContainsMacro = true;
						break;
						IL_010e:
						num2 = 28;
						Globals_Renamed.QueryCancel = 0;
						goto IL_0117;
						IL_0117:
						num2 = 29;
						num7 = Globals_Renamed.g_FrmIdx;
						goto IL_0121;
						IL_0121:
						num2 = 30;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
						goto IL_0136;
						IL_0136:
						num2 = 31;
						if (Globals_Renamed.QueryCancel == 0)
						{
							goto IL_0147;
						}
						goto IL_0197;
						IL_0147:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0150;
						IL_0150:
						num2 = 33;
						FrmSQLQuery[num7].Dispose();
						goto IL_0162;
						IL_0162:
						num2 = 34;
						FrmSQLQuery[num7] = null;
						goto IL_016f;
						IL_016f:
						num2 = 35;
						Information.Err().Clear();
						goto IL_017d;
						IL_017d:
						ProjectData.ClearProjectError();
						num3 = 4;
						goto IL_0185;
						IL_0185:
						num2 = 37;
						num9 = OpenQuery(MyQueryToProcess, 1);
						goto IL_01a6;
						IL_0197:
						num2 = 39;
						num4 = 0;
						break;
						IL_00ce:
						num2 = 21;
						if (MyMode == 0)
						{
							goto IL_00db;
						}
						goto IL_00ec;
						IL_00db:
						num2 = 22;
						num9 = OpenQuery(MyQueryToProcess, 1);
						goto IL_01a6;
						IL_00ec:
						num2 = 24;
						if (MyMode == 1)
						{
							goto IL_00f9;
						}
						goto IL_01a6;
						IL_00f9:
						num2 = 25;
						num9 = OpenQuery(MyQueryToProcess, 2);
						goto IL_01a6;
						IL_01a6:
						num2 = 44;
						Application.DoEvents();
						goto IL_01af;
						IL_01af:
						num2 = 45;
						if (num9 == 0)
						{
							goto IL_01bd;
						}
						goto IL_01c9;
						IL_01bd:
						num2 = 46;
						num4 = 0;
						break;
						IL_01c9:
						num2 = 49;
						if (unchecked(MyMode == 0 || MyMode == 1))
						{
							goto IL_01de;
						}
						goto IL_02a0;
						IL_01de:
						num2 = 50;
						MyInLineValue = FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline;
						goto IL_01f5;
						IL_01f5:
						num2 = 51;
						if (Operators.CompareString(Strings.Trim(MyInLineValue), "", TextCompare: false) == 0)
						{
							goto IL_0214;
						}
						goto IL_027f;
						IL_0214:
						num2 = 52;
						text = BuildSQL.Generate_SQL(1, "");
						goto IL_022a;
						IL_022a:
						num2 = 53;
						if (LikeOperator.LikeString(Strings.Mid(text + "         ", 1, 9), "<OPTIONS>*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.Mid(text, 1, 25), "*<---- New Query ---->*", CompareMethod.Binary))
						{
							goto IL_026c;
						}
						goto IL_0275;
						IL_0506:
						num2 = 105;
						num4 = 0;
						break;
						IL_0275:
						num2 = 56;
						num4 = 2;
						goto IL_02a0;
						IL_026c:
						num2 = 54;
						num4 = 1;
						goto IL_02a0;
						IL_027f:
						num2 = 59;
						text = FrmSQLQuery[Globals_Renamed.g_FrmIdx].SaveInlineView(MyInLineValue);
						goto IL_0298;
						IL_0298:
						num2 = 60;
						num4 = 3;
						goto IL_02a0;
						IL_02a0:
						num2 = 63;
						Globals_Renamed.gWorkQuery = "";
						goto IL_02ad;
						IL_02ad:
						num2 = 64;
						if (Globals_Renamed.Design_Mode == 2)
						{
							goto IL_02be;
						}
						goto IL_030c;
						IL_02be:
						num2 = 65;
						if (Operators.CompareString(Strings.Trim(MyInLineValue), "", TextCompare: false) == 0)
						{
							goto IL_02dd;
						}
						goto IL_030c;
						IL_02dd:
						num2 = 66;
						BuildSQL.RunQuery(text, 5, 1, -99, "");
						goto IL_02f1;
						IL_02f1:
						num2 = 67;
						text = Globals_Renamed.gWorkQuery;
						goto IL_02fb;
						IL_02fb:
						num2 = 68;
						Globals_Renamed.gWorkQuery = "";
						goto IL_030c;
						IL_030c:
						num2 = 71;
						num8 = 2;
						goto IL_0312;
						IL_0312:
						num2 = 72;
						if (!Information.IsNothing(FrmSQLQuery[num8]))
						{
							goto IL_032c;
						}
						goto IL_0393;
						IL_032c:
						num2 = 73;
						FrmSQLQuery[num8].Close();
						goto IL_033e;
						IL_033e:
						num2 = 74;
						if (!Information.IsNothing(FrmSQLQuery[num8]))
						{
							goto IL_0358;
						}
						goto IL_036a;
						IL_0358:
						num2 = 75;
						FrmSQLQuery[num8].Dispose();
						goto IL_036a;
						IL_036a:
						num2 = 76;
						if (!Information.IsNothing(FrmSQLQuery[num8]))
						{
							goto IL_0384;
						}
						goto IL_0393;
						IL_0384:
						num2 = 77;
						FrmSQLQuery[num8] = null;
						goto IL_0393;
						IL_0393:
						num2 = 79;
						num8 = (short)unchecked(num8 + -1);
						if (num8 >= 0)
						{
							goto IL_0312;
						}
						goto IL_03a5;
						IL_03a5:
						num2 = 80;
						if (Globals_Renamed.g_FrmIdx == 0)
						{
							goto IL_03b6;
						}
						goto IL_03c3;
						IL_03b6:
						num2 = 81;
						Globals_Renamed.Design_Mode = 0;
						goto IL_03d4;
						IL_03c3:
						num2 = 83;
						Globals_Renamed.g_FrmIdx--;
						goto IL_03d4;
						IL_03d4:
						num2 = 85;
						BuildForm.Garbage_Collect();
						goto IL_03dd;
						IL_03dd:
						num2 = 86;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_03f9;
						}
						goto IL_0506;
						IL_03f9:
						ProjectData.ClearProjectError();
						num3 = 5;
						goto IL_0401;
						IL_0401:
						num2 = 88;
						num5 = (short)FileSystem.FreeFile();
						goto IL_040c;
						IL_040c:
						num2 = 89;
						FileSystem.FileOpen(num5, MyVAFile, OpenMode.Output);
						goto IL_041c;
						IL_041c:
						num2 = 90;
						FileSystem.PrintLine(num5, text);
						goto IL_0432;
						end_IL_0001_2:
						break;
					}
					num2 = 121;
					if (num4 != 0)
					{
					}
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2063;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return num4;
	}

	public void Load_QueryForm(short MyIdx, short MyMode)
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
					string text = "";
					string text2 = "";
					errsource = "FrmMain - Load_QueryForm - ";
					text2 = Conversions.ToString((int)MyIdx) + ".VG2";
					if (MyIdx == 0)
					{
						text = "Main";
						BuildEmbedded.ClearAllQueries();
					}
					else
					{
						text = "Pre/Post Query - " + FrmSQLQuery[checked(MyIdx - 1)].f_PrePostName + " - ";
					}
					FrmSQLQuery[MyIdx] = new FrmSQLQuerya();
					FrmSQLQuery[MyIdx].Tag = Conversions.ToString((int)MyIdx);
					FrmSQLQuery[MyIdx].fTitle = "SQLBUILD" + text2;
					FrmSQLQuery[MyIdx].fSaveAs = true;
					FrmSQLQuery[MyIdx].Set_QueryTitle();
					Text = "SQLPathFinder 3 (" + text + " - " + Globals_Renamed.gSPFCache + ")";
					if (MyMode == 1)
					{
						FrmSQLQuery[MyIdx].Show();
						FrmSQLQuery[MyIdx].Refresh();
					}
					goto end_IL_0001;
				}
				case 327:
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
				try0001_dispatch = 327;
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

	public string Invoke_Utilities(string MyValue)
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
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildForm - Invoke_Utilities";
					string text = "";
					result = "";
					Globals_Renamed.currvaluetmp = "";
					if (Operators.CompareString(Strings.Mid(MyValue + "   ", 1, 3), "U->", TextCompare: false) == 0)
					{
						MyValue = Strings.UCase(Strings.Mid(MyValue, 4));
					}
					if ((Operators.CompareString(MyValue, "SQL", TextCompare: false) == 0) | (Operators.CompareString(MyValue, "Run-CB-ACS", TextCompare: false) == 0) | LikeOperator.LikeString(MyValue, "Write-File*", CompareMethod.Binary))
					{
						FrmDTREdit frmDTREdit = new FrmDTREdit();
						frmDTREdit.Text1.Text = "";
						if (Operators.CompareString(MyValue, "SQL", TextCompare: false) == 0)
						{
							frmDTREdit.cmbSQLVA.Text = "SQL->VA";
						}
						else if (LikeOperator.LikeString(MyValue, "Write-File*", CompareMethod.Binary))
						{
							if (Operators.CompareString(MyValue, "Write-File2", TextCompare: false) == 0)
							{
								frmDTREdit.mnuOutWF.Text = "TT";
							}
							frmDTREdit.cmbSQLVA.Text = "WRITE-FILE";
							frmDTREdit.out_odbc = "--";
						}
						else
						{
							frmDTREdit.cmbSQLVA.Text = "CB_ACS";
							frmDTREdit.out_odbc = "--";
						}
						frmDTREdit.KeepSQL = true;
						frmDTREdit.fSaveSQL = false;
						frmDTREdit.ShowDialog();
						result = Strings.Trim(frmDTREdit.KeepSQLData);
						frmDTREdit = null;
					}
					else if (Operators.CompareString(MyValue, "DOS", TextCompare: false) == 0)
					{
						result = Interaction.InputBox(General_Procedures.Get_UI("dosprompt"), "DOS Command");
					}
					else if ((Operators.CompareString(MyValue, "Write", TextCompare: false) == 0) | (Operators.CompareString(MyValue, "Wait", TextCompare: false) == 0))
					{
						result = BuildForm.Get_Special_Util(MyValue, "I", "");
					}
					else if (Operators.CompareString(Strings.UCase(MyValue), "CSR-INPUT", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(MyValue), "SDC-INPUT", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(MyValue), "MMS-INPUT", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(MyValue), "MMSWF-INPUT", TextCompare: false) == 0)
					{
						FrmInput frmInput = new FrmInput
						{
							F_OutputFile = "",
							F_Mode = "1"
						};
						if (Operators.CompareString(Strings.UCase(MyValue), "CSR-INPUT", TextCompare: false) == 0)
						{
							frmInput.f_InputFile = "@schemadir@\\utilities_csr.ini";
						}
						else if (Operators.CompareString(Strings.UCase(MyValue), "SDC-INPUT", TextCompare: false) == 0)
						{
							frmInput.f_InputFile = "@schemadir@\\utilities_sdc.ini";
						}
						else if (Operators.CompareString(Strings.UCase(MyValue), "MMSWF-INPUT", TextCompare: false) == 0)
						{
							frmInput.f_InputFile = "@schemadir@\\utilities_mmswf.ini";
						}
						else
						{
							frmInput.f_InputFile = "@schemadir@\\utilities_mms.ini";
						}
						frmInput.ShowDialog();
						result = frmInput.F_Output;
						frmInput.Dispose();
						frmInput = null;
					}
					else
					{
						string left = MyValue;
						if (Operators.CompareString(left, "Excel", TextCompare: false) == 0)
						{
							Globals_Renamed.currhelptmp = "Excel";
						}
						else
						{
							Globals_Renamed.currhelptmp = Strings.UCase(MyValue);
						}
						FrmUtil frmUtil = new FrmUtil();
						if (BuildForm.IsUtil("SQLITE-LOAD") || BuildForm.IsUtil("EMAIL") || BuildForm.IsUtil("ROWS-IN-FILE") || BuildForm.IsUtil("GET-TCA") || BuildForm.IsUtil("R") || BuildForm.IsUtil("TDX-TO-CSV") || BuildForm.IsUtil("PYTHON") || BuildForm.IsUtil("COPY") || BuildForm.IsUtil("MONGODB-IMPORT") || BuildForm.IsUtil("BEGIN-HPC"))
						{
							frmUtil.chkOption.Checked = false;
						}
						if (BuildForm.IsUtil("SMART-APPEND"))
						{
							frmUtil.CmbOpt.Text = "Version 3";
							frmUtil.chkOption.Checked = false;
						}
						else if (BuildForm.IsUtil("Excel"))
						{
							frmUtil.CmbOpt.Text = "Version 2";
							frmUtil.chkOption.Checked = true;
						}
						frmUtil.fUtilPath = "@EXEDIR@\\";
						frmUtil.fHelpType = "M";
						frmUtil.fVaFileName = BuildForm.Locate_Util_Item(MyValue, -1);
						frmUtil.ShowDialog();
						result = Globals_Renamed.currvaluetmp;
					}
					Globals_Renamed.currvaluetmp = "";
					goto end_IL_0001;
				}
				case 1202:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						Globals_Renamed.currvaluetmp = "";
						result = "";
						goto end_IL_0001;
					}
					break;
				}
				goto IL_04e8;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1202;
				continue;
			}
			break;
			IL_04e8:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public void NewQuery()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int g_FrmIdx = default(int);
		while (true)
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
							goto IL_0013;
						case 4:
							goto IL_0020;
						case 5:
							goto IL_0028;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0044;
						case 8:
							goto IL_004c;
						case 9:
							goto IL_0056;
						case 10:
							goto IL_0061;
						case 11:
							goto IL_006a;
						case 12:
							goto IL_0074;
						case 13:
							goto IL_008b;
						case 14:
							goto IL_0094;
						case 15:
							goto IL_009d;
						case 16:
							goto IL_00a7;
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008b:
					num2 = 13;
					BuildEmbedded.ClearAllQueries();
					goto IL_0094;
					IL_0094:
					num2 = 14;
					BuildSQL.Delete_TempTables();
					goto IL_009d;
					IL_00a7:
					num2 = 16;
					Python_Legacy_Engine("0");
					break;
					IL_009d:
					num2 = 15;
					BuildForm.Clear_Global_Vars(1);
					goto IL_00a7;
					IL_000b:
					num2 = 2;
					g_FrmIdx = Globals_Renamed.g_FrmIdx;
					goto IL_0013;
					IL_0013:
					num2 = 3;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0020;
					IL_0020:
					num2 = 4;
					Globals_Renamed.Design_Mode = 2;
					goto IL_0028;
					IL_0028:
					num2 = 5;
					BuildForm.Set_Controls(Globals_Renamed.Design_Mode);
					goto IL_0035;
					IL_0035:
					num2 = 6;
					Load_QueryForm(Globals_Renamed.g_FrmIdx, 1);
					goto IL_0044;
					IL_0044:
					num2 = 7;
					BuildForm.Add_FrmView();
					goto IL_004c;
					IL_004c:
					num2 = 8;
					base.TopMost = true;
					goto IL_0056;
					IL_0056:
					num2 = 9;
					base.TopMost = false;
					goto IL_0061;
					IL_0061:
					num2 = 10;
					Application.DoEvents();
					goto IL_006a;
					IL_006a:
					num2 = 11;
					Set_g_FrmIdx_2();
					goto IL_0074;
					IL_0074:
					num2 = 12;
					if (Globals_Renamed.g_FrmIdx != 0 || g_FrmIdx != 0)
					{
						break;
					}
					goto IL_008b;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				Show();
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

	public void UpdateMenu()
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
				case 610:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_00d7;
						case 6:
							goto IL_00f0;
						case 7:
							goto IL_0109;
						case 8:
							goto IL_0122;
						case 9:
							goto IL_0141;
						case 10:
							goto IL_0163;
						case 11:
							goto IL_0173;
						case 12:
							goto IL_0195;
						case 13:
							goto IL_01a5;
						case 14:
							goto IL_01c7;
						case 15:
							goto IL_01d7;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01a5:
					num2 = 13;
					if (Operators.CompareString(MnuFile1.Text, "", TextCompare: false) != 0)
					{
						goto IL_01c7;
					}
					goto IL_01d7;
					IL_01c7:
					num2 = 14;
					MnuFile1.Visible = true;
					goto IL_01d7;
					IL_0195:
					num2 = 12;
					MnuFile2.Visible = true;
					goto IL_01a5;
					IL_01d7:
					num2 = 15;
					if (Operators.CompareString(MnuFile0.Text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if ((Operators.CompareString(Strings.UCase(MnuFile0.Text), Strings.UCase(FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle), TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(MnuFile1.Text), Strings.UCase(FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle), TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(MnuFile2.Text), Strings.UCase(FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle), TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(MnuFile3.Text), Strings.UCase(FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle), TextCompare: false) == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_00d7;
					IL_00d7:
					num2 = 5;
					MnuFile3.Text = MnuFile2.Text;
					goto IL_00f0;
					IL_00f0:
					num2 = 6;
					MnuFile2.Text = MnuFile1.Text;
					goto IL_0109;
					IL_0109:
					num2 = 7;
					MnuFile1.Text = MnuFile0.Text;
					goto IL_0122;
					IL_0122:
					num2 = 8;
					MnuFile0.Text = FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle;
					goto IL_0141;
					IL_0141:
					num2 = 9;
					if (Operators.CompareString(MnuFile3.Text, "", TextCompare: false) != 0)
					{
						goto IL_0163;
					}
					goto IL_0173;
					IL_0163:
					num2 = 10;
					MnuFile3.Visible = true;
					goto IL_0173;
					IL_0173:
					num2 = 11;
					if (Operators.CompareString(MnuFile2.Text, "", TextCompare: false) != 0)
					{
						goto IL_0195;
					}
					goto IL_01a5;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				MnuFile0.Visible = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 610;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public short OpenQuery(string QueryName, short OpenQueryType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string PackedExe = default(string);
		short num5 = default(short);
		int num6 = default(int);
		short num7 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num4;
					string MyData;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 1279:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
							case 3:
								break;
							case 1:
								goto IL_03c7;
							default:
								goto end_IL_0001;
							}
							break;
						}
						IL_0390:
						num2 = 67;
						if (Globals_Renamed.g_FrmIdx <= 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_03a1;
						IL_03a1:
						num2 = 68;
						FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveQuery = 1;
						goto end_IL_0001_2;
						IL_0378:
						num2 = 64;
						Force_g_FrmIdx((short)(Globals_Renamed.g_FrmIdx - 1));
						goto end_IL_0001_2;
						IL_03c7:
						num4 = unchecked(num + 1);
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000a;
						case 3:
							goto IL_0013;
						case 4:
							goto IL_001c;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_0026;
						case 7:
							goto IL_002a;
						case 8:
							goto IL_0033;
						case 9:
							goto IL_004d;
						case 10:
							goto IL_006f;
						case 11:
							goto IL_00ab;
						case 12:
							goto IL_00bc;
						case 14:
							goto IL_00c6;
						case 13:
						case 15:
						case 16:
							goto IL_00d0;
						case 17:
							goto IL_00dd;
						case 19:
						case 20:
							goto IL_00e9;
						case 22:
							goto IL_00fd;
						case 23:
							goto IL_0103;
						case 24:
							goto IL_011d;
						case 21:
						case 25:
						case 26:
						case 27:
							goto IL_0136;
						case 28:
							goto IL_015a;
						case 30:
							goto IL_0179;
						case 31:
							goto IL_019d;
						case 33:
							goto IL_01bc;
						case 34:
							goto IL_01e3;
						case 35:
							goto IL_01f7;
						case 36:
							goto IL_020c;
						case 37:
							goto IL_0216;
						case 38:
							goto IL_0227;
						case 39:
							goto IL_0230;
						case 40:
							goto IL_0239;
						case 41:
							goto IL_0253;
						case 42:
							goto IL_0265;
						case 43:
							goto IL_0277;
						case 44:
						case 45:
							goto IL_0286;
						case 46:
							goto IL_0294;
						case 47:
							goto IL_029c;
						case 48:
							goto IL_02ad;
						case 51:
						case 52:
							goto IL_02c5;
						case 54:
							goto IL_02dd;
						case 55:
							goto IL_02ed;
						case 56:
							goto IL_02fd;
						case 57:
							goto IL_030c;
						case 58:
							goto IL_0315;
						case 59:
							goto IL_0323;
						case 60:
							goto IL_0333;
						case 61:
							goto IL_034b;
						case 62:
							goto IL_035a;
						case 63:
							goto IL_0367;
						case 64:
							goto IL_0378;
						case 67:
							goto IL_0390;
						case 68:
							goto IL_03a1;
						case 49:
						case 50:
						case 73:
							goto end_IL_0001_3;
						default:
							goto end_IL_0001;
						case 18:
						case 29:
						case 32:
						case 53:
						case 65:
						case 66:
						case 69:
						case 70:
						case 71:
						case 72:
						case 74:
							goto end_IL_0001_2;
						}
						goto default;
						IL_000a:
						num2 = 2;
						text = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						PackedExe = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						num5 = 0;
						goto IL_0021;
						IL_0021:
						num2 = 5;
						num6 = 0;
						goto IL_0026;
						IL_0026:
						num2 = 6;
						num7 = 1;
						goto IL_002a;
						IL_002a:
						num2 = 7;
						num6 = Globals_Renamed.g_FrmIdx;
						goto IL_0033;
						IL_0033:
						num2 = 8;
						if (Operators.CompareString(QueryName, "", TextCompare: false) == 0)
						{
							goto IL_004d;
						}
						goto IL_00fd;
						IL_004d:
						num2 = 9;
						BuildForm.FileOpenSave("O", "", "vgq", "", Globals_Renamed.gQueryDir);
						goto IL_006f;
						IL_006f:
						num2 = 10;
						if ((Operators.CompareString(CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(CMDialog1Open.FileName, "", TextCompare: false) == 0))
						{
							goto IL_00ab;
						}
						goto IL_00e9;
						IL_00ab:
						num2 = 11;
						if (num6 <= 0)
						{
							goto IL_00bc;
						}
						goto IL_00c6;
						IL_00bc:
						num2 = 12;
						num6 = 0;
						goto IL_00d0;
						IL_00c6:
						num2 = 14;
						num6--;
						goto IL_00d0;
						IL_00d0:
						num2 = 16;
						Force_g_FrmIdx((short)num6);
						goto IL_00dd;
						IL_00dd:
						num2 = 17;
						num7 = 0;
						goto end_IL_0001_2;
						IL_00e9:
						num2 = 20;
						text = CMDialog1Open.FileName;
						goto IL_0136;
						IL_00fd:
						num2 = 22;
						text = QueryName;
						goto IL_0103;
						IL_0103:
						num2 = 23;
						if (Strings.InStrRev(text, "\\") == 0)
						{
							goto IL_011d;
						}
						goto IL_0136;
						IL_011d:
						num2 = 24;
						text = Globals_Renamed.gQueryDir + Strings.Trim(text);
						goto IL_0136;
						IL_0136:
						num2 = 27;
						if (Operators.CompareString(Strings.LCase(Strings.Right(text, 7)), ".spfsql", TextCompare: false) == 0)
						{
							goto IL_015a;
						}
						goto IL_0179;
						IL_015a:
						num2 = 28;
						MyData = "";
						BuildForm.HG_Editor(ref MyData, "SQL->VA", text);
						goto end_IL_0001_2;
						IL_0179:
						num2 = 30;
						if (Operators.CompareString(Strings.LCase(Strings.Right(text, 3)), ".va", TextCompare: false) == 0)
						{
							goto IL_019d;
						}
						goto IL_01bc;
						IL_019d:
						num2 = 31;
						MyData = "";
						BuildForm.HG_Editor(ref MyData, "VA", text);
						goto end_IL_0001_2;
						IL_01bc:
						num2 = 33;
						if (Operators.CompareString(Strings.LCase(Strings.Right(text, 4)), ".spf", TextCompare: false) == 0)
						{
							goto IL_01e3;
						}
						goto IL_02dd;
						IL_01e3:
						num2 = 34;
						if (Globals_Renamed.g_FrmIdx > 0)
						{
							goto IL_01f7;
						}
						goto IL_02c5;
						IL_01f7:
						num2 = 35;
						Interaction.MsgBox("You cannot open Multi Query Files (.spf) from a Pre/Post Query", MsgBoxStyle.Information, "Invalid Operation");
						goto IL_020c;
						IL_020c:
						num2 = 36;
						num5 = Globals_Renamed.g_FrmIdx;
						goto IL_0216;
						IL_0216:
						num2 = 37;
						if (num5 >= 1)
						{
							goto IL_0227;
						}
						goto IL_0230;
						IL_0227:
						num2 = 38;
						Globals_Renamed.g_PrePostQueryOpen = false;
						goto IL_0230;
						IL_0230:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0239;
						IL_0239:
						num2 = 40;
						if (!Information.IsNothing(FrmSQLQuery[num5]))
						{
							goto IL_0253;
						}
						goto IL_0286;
						IL_0253:
						num2 = 41;
						FrmSQLQuery[num5].Close();
						goto IL_0265;
						IL_0265:
						num2 = 42;
						FrmSQLQuery[num5].Dispose();
						goto IL_0277;
						IL_0277:
						num2 = 43;
						FrmSQLQuery[num5] = null;
						goto IL_0286;
						IL_0286:
						num2 = 45;
						Information.Err().Clear();
						goto IL_0294;
						IL_0294:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_029c;
						IL_029c:
						num2 = 47;
						if (num5 < 1)
						{
							break;
						}
						goto IL_02ad;
						IL_02ad:
						num2 = 48;
						Force_g_FrmIdx((short)(num5 - 1));
						break;
						IL_02c5:
						num2 = 52;
						num7 = Invoke_MQ_External(text, text, ref PackedExe);
						goto end_IL_0001_2;
						IL_02dd:
						num2 = 54;
						Gauge1.Value = 0;
						goto IL_02ed;
						IL_02ed:
						num2 = 55;
						Gauge1.Visible = true;
						goto IL_02fd;
						IL_02fd:
						num2 = 56;
						cmdIcon.Refresh();
						goto IL_030c;
						IL_030c:
						num2 = 57;
						Globals_Renamed.Design_Mode = 2;
						goto IL_0315;
						IL_0315:
						num2 = 58;
						BuildForm.Set_Controls(Globals_Renamed.Design_Mode);
						goto IL_0323;
						IL_0323:
						num2 = 59;
						Load_QueryForm(Globals_Renamed.g_FrmIdx, 0);
						goto IL_0333;
						IL_0333:
						num2 = 60;
						num7 = FrmSQLQuery[Globals_Renamed.g_FrmIdx].OpenQuery_VG2(text, OpenQueryType);
						goto IL_034b;
						IL_034b:
						num2 = 61;
						cmdIcon.Refresh();
						goto IL_035a;
						IL_035a:
						num2 = 62;
						if (num7 == 0)
						{
							goto IL_0367;
						}
						goto IL_0390;
						IL_0367:
						num2 = 63;
						if (Globals_Renamed.g_FrmIdx <= 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_0378;
						end_IL_0001_3:
						break;
					}
					num2 = 73;
					num7 = 0;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1279;
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
		return num7;
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
				case 167:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002e;
						case 5:
							goto IL_0042;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_001f:
					num2 = 3;
					BuildForm.SetOutputOpt("MACRO", ref oVal);
					goto IL_002e;
					IL_002e:
					num2 = 4;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile = oVal;
					goto IL_0042;
					IL_000b:
					num2 = 2;
					oVal = FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile;
					goto IL_001f;
					IL_0042:
					num2 = 5;
					Globals_Renamed.currDataAny = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				SetMenuOpt(FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline, FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 167;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuOutInline_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string oVal = default(string);
		Globals_Renamed.AliasArray_Type[] g_AliasArray = default(Globals_Renamed.AliasArray_Type[]);
		short g_NoAlias = default(short);
		while (true)
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
				case 425:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_001e;
						case 5:
							goto IL_0037;
						case 6:
							goto IL_005b;
						case 7:
							goto IL_00b4;
						case 8:
							goto IL_00c8;
						case 9:
							goto IL_00dc;
						case 10:
							goto IL_00ec;
						case 11:
							goto IL_0101;
						case 12:
							goto IL_010e;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c8:
					num2 = 8;
					oVal = FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline;
					goto IL_00dc;
					IL_00dc:
					num2 = 9;
					BuildForm.SetOutputOpt("VIEW", ref oVal);
					goto IL_00ec;
					IL_00b4:
					num2 = 7;
					Globals_Renamed.currDataAny = g_AliasArray[0].Alias_Renamed;
					goto IL_00c8;
					IL_00ec:
					num2 = 10;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline = oVal;
					goto IL_0101;
					IL_000b:
					num2 = 2;
					g_AliasArray = new Globals_Renamed.AliasArray_Type[134];
					goto IL_0019;
					IL_0019:
					num2 = 3;
					g_NoAlias = -1;
					goto IL_001e;
					IL_001e:
					num2 = 4;
					Interaction.MsgBox(General_Procedures.Get_UI("INLINE"), MsgBoxStyle.Information, "Note on Inline Views");
					goto IL_0037;
					IL_0037:
					num2 = 5;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Load_Tree_Alias_To_Array("E", ref g_NoAlias, ref g_AliasArray, Do_Valid_Inc: false);
					goto IL_005b;
					IL_005b:
					num2 = 6;
					if (g_NoAlias != 1 || (!LikeOperator.LikeString(Strings.UCase(g_AliasArray[0].Alias_Renamed), "A*", CompareMethod.Binary) && Operators.CompareString(Strings.Trim(FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline), "", TextCompare: false) == 0))
					{
						break;
					}
					goto IL_00b4;
					IL_0101:
					num2 = 11;
					Globals_Renamed.currDataAny = "";
					goto IL_010e;
					IL_010e:
					num2 = 12;
					SetMenuOpt(FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline, FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				Interaction.MsgBox("Inline Views cannot be created for Cross-Database Views, SQLite Queries, TEXT {JET} Queries, other Inline Views,  nor for queries involving multiple views.", MsgBoxStyle.Information, "Cannot Create Inline View!");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 425;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void MnuNewQuery_Click(object eventSender, EventArgs eventArgs)
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
				short design_Mode;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 297:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0032;
						case 8:
							goto IL_003e;
						case 9:
							goto IL_004e;
						case 10:
							goto IL_0057;
						case 11:
							goto IL_0060;
						case 12:
							goto IL_0069;
						case 13:
							goto IL_007e;
						case 14:
							goto IL_008f;
						case 15:
							goto IL_00a0;
						case 16:
							goto IL_00ac;
						case 5:
						case 7:
						case 17:
						case 18:
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008f:
					num2 = 14;
					FrmSQLQuery[num5].Dispose();
					goto IL_00a0;
					IL_00a0:
					num2 = 15;
					FrmSQLQuery[num5] = null;
					goto IL_00ac;
					IL_007e:
					num2 = 13;
					if (Globals_Renamed.QueryCancel != 0)
					{
						break;
					}
					goto IL_008f;
					IL_00ac:
					num2 = 16;
					NewQuery();
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					Globals_Renamed.g_PrePostQueryOpen = false;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_0032;
					}
					if (design_Mode != 2)
					{
						break;
					}
					goto IL_003e;
					IL_0032:
					num2 = 6;
					NewQuery();
					break;
					IL_003e:
					num2 = 8;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_004e;
					}
					goto IL_0057;
					IL_004e:
					num2 = 9;
					Globals_Renamed.g_PrePostQueryOpen = true;
					goto IL_0057;
					IL_0057:
					num2 = 10;
					Globals_Renamed.QueryCancel = 0;
					goto IL_0060;
					IL_0060:
					num2 = 11;
					num5 = Globals_Renamed.g_FrmIdx;
					goto IL_0069;
					IL_0069:
					num2 = 12;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_007e;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				Globals_Renamed.g_PrePostQueryOpen = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 297;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void MnuOpenQuery_Click(object eventSender, EventArgs eventArgs)
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
				short design_Mode;
				short num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 528:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_0033;
						case 8:
							goto IL_0049;
						case 9:
							goto IL_0059;
						case 10:
						case 11:
							goto IL_0064;
						case 12:
							goto IL_006d;
						case 13:
							goto IL_0077;
						case 14:
							goto IL_008c;
						case 15:
							goto IL_009d;
						case 16:
							goto IL_00af;
						case 17:
							goto IL_00bc;
						case 19:
							goto IL_00d1;
						case 5:
						case 7:
						case 18:
						case 20:
						case 21:
						case 22:
							goto IL_00df;
						case 23:
							goto IL_00e4;
						case 24:
							goto IL_00fb;
						case 25:
						case 26:
							goto IL_0102;
						case 27:
							goto IL_010f;
						case 28:
							goto IL_011f;
						case 30:
							goto IL_013e;
						case 31:
							goto IL_014e;
						case 29:
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
					IL_010f:
					num2 = 27;
					MnuAddTables.Enabled = false;
					goto IL_011f;
					IL_011f:
					num2 = 28;
					cmdIcon.Items[6].Enabled = false;
					break;
					IL_0102:
					num2 = 26;
					if (!flag)
					{
						goto IL_010f;
					}
					goto IL_013e;
					IL_013e:
					num2 = 30;
					MnuAddTables.Enabled = true;
					goto IL_014e;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					Globals_Renamed.g_PrePostQueryOpen = false;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_0033;
					}
					if (design_Mode == 2)
					{
						goto IL_0049;
					}
					goto IL_00df;
					IL_014e:
					num2 = 31;
					cmdIcon.Items[6].Enabled = true;
					break;
					IL_0049:
					num2 = 8;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_0059;
					}
					goto IL_0064;
					IL_0059:
					num2 = 9;
					Globals_Renamed.g_PrePostQueryOpen = true;
					goto IL_0064;
					IL_0064:
					num2 = 11;
					Globals_Renamed.QueryCancel = 0;
					goto IL_006d;
					IL_006d:
					num2 = 12;
					num5 = Globals_Renamed.g_FrmIdx;
					goto IL_0077;
					IL_0077:
					num2 = 13;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_008c;
					IL_008c:
					num2 = 14;
					if (Globals_Renamed.QueryCancel == 0)
					{
						goto IL_009d;
					}
					goto IL_00d1;
					IL_009d:
					num2 = 15;
					FrmSQLQuery[num5].Dispose();
					goto IL_00af;
					IL_00af:
					num2 = 16;
					FrmSQLQuery[num5] = null;
					goto IL_00bc;
					IL_00bc:
					num2 = 17;
					num6 = OpenQuery("", 1);
					goto IL_00df;
					IL_00d1:
					num2 = 19;
					Set_g_FrmIdx_2();
					goto IL_00df;
					IL_0033:
					num2 = 6;
					num6 = OpenQuery("", 1);
					goto IL_00df;
					IL_00df:
					num2 = 22;
					flag = true;
					goto IL_00e4;
					IL_00e4:
					num2 = 23;
					if (!BuildForm.FormIsLoaded("FrmSQLQuerya", 1))
					{
						goto IL_00fb;
					}
					goto IL_0102;
					IL_00fb:
					num2 = 24;
					flag = false;
					goto IL_0102;
					end_IL_0001_2:
					break;
				}
				num2 = 33;
				Globals_Renamed.g_PrePostQueryOpen = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 528;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void MnuSaveQ(short Index)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text;
				short design_Mode;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 238:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0040;
						case 8:
							goto IL_004a;
						case 9:
							goto IL_005e;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 11:
						case 12:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0040:
					num2 = 7;
					if (!flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_004a;
					IL_004a:
					num2 = 8;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Set_QueryTitle();
					goto IL_005e;
					IL_002a:
					num2 = 6;
					flag = FrmSQLQuery[Globals_Renamed.g_FrmIdx].SaveQuery(Index);
					goto IL_0040;
					IL_005e:
					num2 = 9;
					if (Globals_Renamed.g_FrmIdx < 1)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					flag = false;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_002a;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				FrmSQLQuery[checked(Globals_Renamed.g_FrmIdx - 1)].SetQueryNode(FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 238;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void DoRunQuery_M()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
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
				case 448:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0038;
						case 7:
							goto IL_003d;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_006c;
						case 10:
							goto IL_008e;
						case 11:
							goto IL_0098;
						case 12:
							goto IL_00c2;
						case 13:
							goto IL_00cc;
						case 14:
							goto IL_00da;
						case 15:
							goto IL_00f2;
						case 16:
							goto IL_012e;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 18:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cc:
					num2 = 13;
					text = text + text2 + text3;
					goto IL_00da;
					IL_00da:
					num2 = 14;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_00f2;
					}
					goto IL_012e;
					IL_00c2:
					num2 = 12;
					text2 = " and ";
					goto IL_00cc;
					IL_00f2:
					num2 = 15;
					num5 = (int)Interaction.MsgBox("Note that you have assigned a temporary" + text + "on the Query menu which will override any" + text + "embedded in your query. Do you wish to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Do you wish to continue?");
					goto IL_012e;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text3 = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					text2 = "";
					goto IL_0025;
					IL_0025:
					num2 = 5;
					if (Globals_Renamed.Design_Mode == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0038;
					IL_0038:
					num2 = 6;
					num5 = 6;
					goto IL_003d;
					IL_003d:
					num2 = 7;
					if (Operators.CompareString(FrmSQLQuery[Globals_Renamed.g_FrmIdx].MacroFile, "", TextCompare: false) != 0)
					{
						goto IL_0064;
					}
					goto IL_006c;
					IL_0064:
					num2 = 8;
					text = " macro file ";
					goto IL_006c;
					IL_006c:
					num2 = 9;
					if (Operators.CompareString(mnuspfsitevalue.Text, "", TextCompare: false) != 0)
					{
						goto IL_008e;
					}
					goto IL_0098;
					IL_008e:
					num2 = 10;
					text3 = " SPF-Site/Map_Node ";
					goto IL_0098;
					IL_0098:
					num2 = 11;
					if (Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_00c2;
					}
					goto IL_00cc;
					IL_012e:
					num2 = 16;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].RunQueryMenu(0, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 448;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnurunquery_Click(object eventSender, EventArgs eventArgs)
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
				DoRunQuery_M();
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

	public void mnuToggleGrid_Click(object eventSender, EventArgs eventArgs)
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
				case 79:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].ToggleQGrid();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 79;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public bool Do_Node_Init_Chk()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		bool result = default(bool);
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
						errsource = "FrmMain - Do_Node_Init_Chk";
						string text = "";
						string text2 = "";
						string text3 = "";
						short num3 = 0;
						short num4 = 0;
						result = false;
						num3 = 1;
						do
						{
							switch (num3)
							{
							case 1:
								text3 = Globals_Renamed.MyMARSServer;
								text = "MARS";
								break;
							case 2:
								text3 = Globals_Renamed.MyOASysServer;
								text = "OASys";
								break;
							case 3:
								text3 = Globals_Renamed.MyARIESServer;
								text = "ARIES";
								break;
							case 4:
								text3 = Globals_Renamed.MyOtherServer;
								text = "Other";
								break;
							}
							text3 = Strings.Trim(Strings.UCase(text3));
							if ((Operators.CompareString(text3, "", TextCompare: false) == 0) | (Operators.CompareString(text3, "NONE", TextCompare: false) == 0))
							{
								text2 = text2 + "," + text;
							}
							num3 = (short)unchecked(num3 + 1);
						}
						while (num3 <= 4);
						if (Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							result = true;
							goto end_IL_0001;
						}
						Cursor.Current = Cursors.Default;
						text2 = Strings.Trim(Strings.Mid(text2 + " ", 2));
						num4 = (short)Interaction.MsgBox("The following nodes (" + text2 + ") have not been initialized on the Set Default Nodes Screen. Click Yes to open that window and set your nodes or No to exit query execution. Once nodes are set you can re-run the query", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Nodes Need to be Initialized");
						if (num4 == 6)
						{
							FrmNode frmNode = new FrmNode();
							frmNode.ShowDialog();
						}
						goto end_IL_0001;
					}
					case 399:
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
				try0001_dispatch = 399;
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

	public void MnuCompute0_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		TreeNode treeNode = default(TreeNode);
		while (true)
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
				case 302:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0046;
						case 6:
							goto IL_0058;
						case 7:
							goto IL_006c;
						case 8:
							goto IL_0087;
						case 9:
							goto IL_00a6;
						case 10:
							goto IL_00c7;
						case 11:
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0087:
					num2 = 8;
					if (!((num5 != 2) & (treeNode.GetNodeCount(includeSubTrees: false) >= 2)))
					{
						break;
					}
					goto IL_00a6;
					IL_00a6:
					num2 = 9;
					LoadComputedColumnForm(num5, "NEW", "", "C", "", -1);
					goto IL_00c7;
					IL_006c:
					num2 = 7;
					num5 = checked((short)FrmSQLQuery[Globals_Renamed.g_FrmIdx].SSTab1.SelectedIndex);
					goto IL_0087;
					IL_00c7:
					num2 = 10;
					Globals_Renamed.currtxttmp = "";
					break;
					IL_000b:
					num2 = 2;
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_001c;
					IL_001c:
					num2 = 3;
					treeNode = BuildForm.FindNodeByName(FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "ROOTQ");
					goto IL_0046;
					IL_0046:
					num2 = 4;
					if (treeNode == null)
					{
						goto end_IL_0001_3;
					}
					goto IL_0058;
					IL_0058:
					num2 = 6;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Refresh_Combo();
					goto IL_006c;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				Globals_Renamed.currinttmp = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 302;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void MnuSaveQuery_Click(object sender, EventArgs e)
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
				MnuSaveQ(0);
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

	private void mnuSaveQAs_Click(object sender, EventArgs e)
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
				MnuSaveQ(1);
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

	private void FrmMain_Resize(object sender, EventArgs e)
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
				case 365:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0054;
						case 6:
							goto IL_0072;
						case 8:
							goto IL_009f;
						case 9:
							goto IL_00bd;
						case 11:
							goto IL_00da;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 10:
						case 13:
						case 14:
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0072:
					num2 = 6;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin_Resize(FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin, new EventArgs());
					goto end_IL_0001_3;
					IL_009f:
					num2 = 8;
					if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Visible)
					{
						goto IL_00bd;
					}
					goto IL_00da;
					IL_0054:
					num2 = 5;
					if (FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Visible)
					{
						goto IL_0072;
					}
					goto IL_009f;
					IL_00bd:
					num2 = 9;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Focus();
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_0022;
					IL_0022:
					num2 = 4;
					if (Information.IsNothing(FrmSQLQuery[Globals_Renamed.g_FrmIdx]) || !FrmSQLQuery[Globals_Renamed.g_FrmIdx].Enabled)
					{
						goto end_IL_0001_3;
					}
					goto IL_0054;
					IL_00da:
					num2 = 11;
					if (!FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Visible)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 365;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void MnuFile0_Click(object sender, EventArgs e)
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
							goto IL_0028;
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
					text = Conversions.ToString(NewLateBinding.LateGet(sender, null, "Name", new object[0], null, null, null));
					goto IL_0028;
					IL_0028:
					num2 = 3;
					text = Strings.Trim(Strings.Mid(text, 8));
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				OpenMenuFile(text);
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

	private void mnuBatch_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmBatch4 frmBatch = default(FrmBatch4);
		while (true)
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
					frmBatch = new FrmBatch4();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				frmBatch.ShowDialog();
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

	private void mnuSetCredentials_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmNodeSel frmNodeSel = default(FrmNodeSel);
		while (true)
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
					frmNodeSel = new FrmNodeSel
					{
						f_Mode = -1
					};
					goto IL_001a;
					IL_001a:
					num2 = 3;
					frmNodeSel.ShowDialog();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				frmNodeSel.Dispose();
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

	private void mnuRefreshViews_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		int num3 = default(int);
		bool flag = default(bool);
		string errsource = default(string);
		bool fSaveAs = default(bool);
		string fTitle = default(string);
		string text = default(string);
		bool flag2 = default(bool);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				short num5;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					if (Globals_Renamed.Design_Mode != 2 || Globals_Renamed.g_RefreshViews)
					{
						break;
					}
					goto IL_001e;
				case 827:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
						case 3:
							break;
						case 1:
							goto IL_026b;
						default:
							goto end_IL_0001;
						}
						goto IL_01da;
					}
					IL_01b5:
					num2 = 32;
					Interaction.MsgBox("SQLPathFinder was unable to refresh the Views. If the problem is not obvious, contact support for assistance.", MsgBoxStyle.Exclamation, "Could not Refresh Views");
					goto IL_01cb;
					IL_01cb:
					num2 = 34;
					Globals_Renamed.g_RefreshViews = false;
					goto end_IL_0001_2;
					IL_01ab:
					num2 = 30;
					flag = false;
					goto IL_01cb;
					IL_026b:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_001e;
					case 3:
						goto IL_0031;
					case 4:
						goto IL_0039;
					case 5:
						goto IL_0042;
					case 6:
						goto IL_004a;
					case 7:
						goto IL_005b;
					case 8:
						goto IL_006c;
					case 9:
						goto IL_007d;
					case 10:
						goto IL_0083;
					case 11:
						goto IL_0089;
					case 12:
						goto IL_008f;
					case 13:
						goto IL_00a0;
					case 14:
						goto IL_00c4;
					case 15:
						goto IL_00ca;
					case 16:
						goto IL_00dd;
					case 17:
						goto IL_00eb;
					case 18:
						goto IL_00f4;
					case 19:
						goto IL_0105;
					case 20:
						goto IL_0116;
					case 21:
						goto IL_011f;
					case 22:
						goto IL_0130;
					case 23:
						goto IL_013c;
					case 24:
						goto IL_014a;
					case 25:
						goto IL_0152;
					case 26:
					case 27:
						goto IL_0174;
					case 28:
						goto IL_0186;
					case 29:
						goto IL_0198;
					case 30:
						goto IL_01ab;
					case 32:
						goto IL_01b5;
					case 31:
					case 33:
					case 34:
						goto IL_01cb;
					case 36:
						goto IL_01da;
					case 37:
						goto IL_01f9;
					case 38:
						goto IL_0207;
					case 39:
						goto IL_0210;
					case 40:
						goto IL_021b;
					case 41:
						goto IL_022d;
					case 44:
						goto IL_0245;
					case 42:
					case 43:
					case 45:
					case 46:
					case 47:
						goto end_IL_0001_3;
					default:
						goto end_IL_0001;
					case 35:
					case 48:
						goto end_IL_0001_2;
					}
					goto default;
					IL_01da:
					num2 = 36;
					Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
					goto IL_01f9;
					IL_01f9:
					num2 = 37;
					Information.Err().Clear();
					goto IL_0207;
					IL_0207:
					ProjectData.ClearProjectError();
					num3 = -3;
					goto IL_0210;
					IL_0210:
					num2 = 39;
					if (!flag)
					{
						break;
					}
					goto IL_021b;
					IL_021b:
					num2 = 40;
					FrmSQLQuery[0].fSaveAs = fSaveAs;
					goto IL_022d;
					IL_022d:
					num2 = 41;
					FrmSQLQuery[0].fTitle = fTitle;
					break;
					IL_0245:
					num2 = 44;
					Interaction.MsgBox("You cannot refresh Views used in a Pre/Post Query. To refresh, exit the Pre/Post query and re-enter.", MsgBoxStyle.Exclamation, "Cannot Refresh views Used in a Pre/Post Query");
					break;
					IL_001e:
					num2 = 2;
					if (Globals_Renamed.g_FrmIdx == 0)
					{
						goto IL_0031;
					}
					goto IL_0245;
					IL_0031:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_0039;
					IL_0039:
					num2 = 4;
					errsource = "FrmMain - RefreshViews";
					goto IL_0042;
					IL_0042:
					num2 = 5;
					Globals_Renamed.g_RefreshViews = true;
					goto IL_004a;
					IL_004a:
					num2 = 6;
					fSaveAs = FrmSQLQuery[0].fSaveAs;
					goto IL_005b;
					IL_005b:
					num2 = 7;
					fTitle = FrmSQLQuery[0].fTitle;
					goto IL_006c;
					IL_006c:
					num2 = 8;
					text = FrmSQLQuery[0].Text;
					goto IL_007d;
					IL_007d:
					num2 = 9;
					num5 = 0;
					goto IL_0083;
					IL_0083:
					num2 = 10;
					flag2 = false;
					goto IL_0089;
					IL_0089:
					num2 = 11;
					flag = false;
					goto IL_008f;
					IL_008f:
					num2 = 12;
					FrmSQLQuery[0].fSaveAs = false;
					goto IL_00a0;
					IL_00a0:
					num2 = 13;
					FrmSQLQuery[0].fTitle = Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".vg2";
					goto IL_00c4;
					IL_00c4:
					num2 = 14;
					flag = true;
					goto IL_00ca;
					IL_00ca:
					num2 = 15;
					flag2 = FrmSQLQuery[0].SaveQuery(0);
					goto IL_00dd;
					IL_00dd:
					num2 = 16;
					if (flag2)
					{
						goto IL_00eb;
					}
					goto IL_01b5;
					IL_00eb:
					num2 = 17;
					Globals_Renamed.QueryCancel = 0;
					goto IL_00f4;
					IL_00f4:
					num2 = 18;
					FrmSQLQuery[0].Close();
					goto IL_0105;
					IL_0105:
					num2 = 19;
					if (Globals_Renamed.QueryCancel == 0)
					{
						goto IL_0116;
					}
					goto IL_0174;
					IL_0116:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_011f;
					IL_011f:
					num2 = 21;
					FrmSQLQuery[0].Dispose();
					goto IL_0130;
					IL_0130:
					num2 = 22;
					FrmSQLQuery[0] = null;
					goto IL_013c;
					IL_013c:
					num2 = 23;
					Information.Err().Clear();
					goto IL_014a;
					IL_014a:
					ProjectData.ClearProjectError();
					num3 = 3;
					goto IL_0152;
					IL_0152:
					num2 = 25;
					num5 = OpenQuery(Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".vg2", 1);
					goto IL_0174;
					IL_0174:
					num2 = 27;
					FrmSQLQuery[0].fSaveAs = fSaveAs;
					goto IL_0186;
					IL_0186:
					num2 = 28;
					FrmSQLQuery[0].fTitle = fTitle;
					goto IL_0198;
					IL_0198:
					num2 = 29;
					FrmSQLQuery[0].Text = text;
					goto IL_01ab;
					end_IL_0001_3:
					break;
				}
				num2 = 47;
				Globals_Renamed.g_RefreshViews = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 827;
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

	private void SaveQExe(object sender, EventArgs e)
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
				BuildForm.Pack_SH(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
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

	private void mnuEmailQuery_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		int num3 = default(int);
		bool flag = default(bool);
		string text = default(string);
		string errsource = default(string);
		bool fSaveAs = default(bool);
		string fTitle = default(string);
		bool flag2 = default(bool);
		int num5 = default(int);
		bool flag3 = default(bool);
		string myJob = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		FrmEmail frmEmail = default(FrmEmail);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001;
					}
					goto IL_0013;
				case 1402:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_0462;
						default:
							goto end_IL_0001_2;
						}
						goto IL_03bc;
					}
					IL_0396:
					num2 = 48;
					flag = false;
					goto end_IL_0001;
					IL_03a0:
					num2 = 50;
					Interaction.MsgBox("SQLPathFinder was unable to Save the Query in order to email it. Contact support if you need assistance.", MsgBoxStyle.Exclamation, "Could not Email Query");
					goto end_IL_0001;
					IL_0383:
					num2 = 47;
					FrmSQLQuery[0].Text = text;
					goto IL_0396;
					IL_0462:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_0013;
					case 3:
						goto IL_0026;
					case 4:
						goto IL_002e;
					case 5:
						goto IL_0037;
					case 6:
						goto IL_0048;
					case 7:
						goto IL_0059;
					case 8:
						goto IL_006a;
					case 9:
						goto IL_006f;
					case 10:
						goto IL_0075;
					case 11:
						goto IL_007b;
					case 12:
						goto IL_0085;
					case 13:
						goto IL_008f;
					case 14:
						goto IL_0099;
					case 15:
						goto IL_00a3;
					case 16:
						goto IL_00bc;
					case 17:
						goto IL_00d5;
					case 18:
						goto IL_00db;
					case 19:
						goto IL_00ec;
					case 20:
						goto IL_0110;
					case 21:
						goto IL_0116;
					case 22:
						goto IL_0129;
					case 23:
						goto IL_0137;
					case 24:
						goto IL_0141;
					case 25:
						goto IL_0147;
					case 26:
						goto IL_0152;
					case 27:
						goto IL_0165;
					case 28:
						goto IL_0176;
					case 29:
						goto IL_0187;
					case 30:
						goto IL_0198;
					case 31:
						goto IL_01b1;
					case 32:
						goto IL_01c0;
					case 33:
						goto IL_01ce;
					case 34:
					case 35:
						goto IL_01da;
					case 36:
						goto IL_01f9;
					case 38:
						goto IL_0285;
					case 37:
					case 39:
					case 40:
						goto IL_032c;
					case 41:
						goto IL_033a;
					case 42:
					case 43:
						goto IL_034e;
					case 44:
						goto IL_0359;
					case 45:
						goto IL_035f;
					case 46:
						goto IL_0371;
					case 47:
						goto IL_0383;
					case 48:
						goto IL_0396;
					case 50:
						goto IL_03a0;
					case 53:
						goto IL_03bc;
					case 54:
						goto IL_03db;
					case 55:
						goto IL_03e9;
					case 56:
						goto IL_03f2;
					case 57:
						goto IL_03fd;
					case 58:
						goto IL_040f;
					case 59:
					case 60:
						goto IL_0423;
					case 61:
						goto IL_042e;
					case 63:
						goto end_IL_0001_3;
					default:
						goto end_IL_0001_2;
					case 49:
					case 51:
					case 52:
					case 62:
					case 64:
					case 65:
					case 66:
						goto end_IL_0001;
					}
					goto default;
					IL_03bc:
					num2 = 53;
					Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
					goto IL_03db;
					IL_03db:
					num2 = 54;
					Information.Err().Clear();
					goto IL_03e9;
					IL_03e9:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_03f2;
					IL_03f2:
					num2 = 56;
					if (flag)
					{
						goto IL_03fd;
					}
					goto IL_0423;
					IL_03fd:
					num2 = 57;
					FrmSQLQuery[0].fSaveAs = fSaveAs;
					goto IL_040f;
					IL_040f:
					num2 = 58;
					FrmSQLQuery[0].fTitle = fTitle;
					goto IL_0423;
					IL_0423:
					num2 = 60;
					if (!flag2)
					{
						goto end_IL_0001;
					}
					goto IL_042e;
					IL_042e:
					num2 = 61;
					MyProject.Forms.FrmEmail.Dispose();
					goto end_IL_0001;
					IL_0013:
					num2 = 2;
					if (Globals_Renamed.g_FrmIdx != 0)
					{
						break;
					}
					goto IL_0026;
					IL_0026:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_002e;
					IL_002e:
					num2 = 4;
					errsource = "FrmMain - mnuEmailQuery";
					goto IL_0037;
					IL_0037:
					num2 = 5;
					fSaveAs = FrmSQLQuery[0].fSaveAs;
					goto IL_0048;
					IL_0048:
					num2 = 6;
					fTitle = FrmSQLQuery[0].fTitle;
					goto IL_0059;
					IL_0059:
					num2 = 7;
					text = FrmSQLQuery[0].Text;
					goto IL_006a;
					IL_006a:
					num2 = 8;
					num5 = 0;
					goto IL_006f;
					IL_006f:
					num2 = 9;
					flag3 = false;
					goto IL_0075;
					IL_0075:
					num2 = 10;
					flag = false;
					goto IL_007b;
					IL_007b:
					num2 = 11;
					myJob = "";
					goto IL_0085;
					IL_0085:
					num2 = 12;
					text2 = "";
					goto IL_008f;
					IL_008f:
					num2 = 13;
					text3 = "";
					goto IL_0099;
					IL_0099:
					num2 = 14;
					text4 = "";
					goto IL_00a3;
					IL_00a3:
					num2 = 15;
					text5 = Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".vg2";
					goto IL_00bc;
					IL_00bc:
					num2 = 16;
					text6 = Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".txt";
					goto IL_00d5;
					IL_00d5:
					num2 = 17;
					flag2 = false;
					goto IL_00db;
					IL_00db:
					num2 = 18;
					FrmSQLQuery[0].fSaveAs = false;
					goto IL_00ec;
					IL_00ec:
					num2 = 19;
					FrmSQLQuery[0].fTitle = Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".vg2";
					goto IL_0110;
					IL_0110:
					num2 = 20;
					flag = true;
					goto IL_0116;
					IL_0116:
					num2 = 21;
					flag3 = FrmSQLQuery[0].SaveQuery(0);
					goto IL_0129;
					IL_0129:
					num2 = 22;
					if (flag3)
					{
						goto IL_0137;
					}
					goto IL_03a0;
					IL_0137:
					num2 = 23;
					frmEmail = new FrmEmail();
					goto IL_0141;
					IL_0141:
					num2 = 24;
					flag2 = true;
					goto IL_0147;
					IL_0147:
					num2 = 25;
					frmEmail.ShowDialog();
					goto IL_0152;
					IL_0152:
					num2 = 26;
					if (frmEmail.f_OK)
					{
						goto IL_0165;
					}
					goto IL_034e;
					IL_0165:
					num2 = 27;
					text2 = frmEmail.CmbTo.Text;
					goto IL_0176;
					IL_0176:
					num2 = 28;
					text3 = frmEmail.TxtSubj.Text;
					goto IL_0187;
					IL_0187:
					num2 = 29;
					text4 = frmEmail.TxtBody.Text;
					goto IL_0198;
					IL_0198:
					num2 = 30;
					if (Operators.CompareString(text4, "", TextCompare: false) != 0)
					{
						goto IL_01b1;
					}
					goto IL_01da;
					IL_01b1:
					num2 = 31;
					num5 = General_Procedures.Save_SQL_Query(text4, text6);
					goto IL_01c0;
					IL_01c0:
					num2 = 32;
					if (num5 == 0)
					{
						goto IL_01ce;
					}
					goto IL_01da;
					IL_01ce:
					num2 = 33;
					text6 = "";
					goto IL_01da;
					IL_01da:
					num2 = 35;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_01f9;
					}
					goto IL_0285;
					IL_01f9:
					num2 = 36;
					myJob = "\"" + Globals_Renamed.gMyPyPath + "\" \"" + Globals_Renamed.MySchemaDir + "\\DoJMP.py\" -m EMAILQ -s \"" + text6 + "\" -o \"" + text2 + "\" -i \"" + text5 + "\" -e \"" + text3 + "\" -t \"" + frmEmail.f_EmailMode + "\"";
					goto IL_032c;
					IL_0285:
					num2 = 38;
					myJob = "va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Send_Email.va\" \"" + Strings.Replace(text5, "\\", "/", 1, 1) + "\" \"" + text2 + "\" \"" + text3 + "\" \"" + Strings.Replace(text6, "\\", "/", 1, 1) + "\" \"" + frmEmail.f_EmailMode + "\"";
					goto IL_032c;
					IL_032c:
					num2 = 40;
					Support.pauseMS(1000);
					goto IL_033a;
					IL_033a:
					num2 = 41;
					num5 = BuildSQL.RunQ(myJob, 0);
					goto IL_034e;
					IL_034e:
					num2 = 43;
					frmEmail.Dispose();
					goto IL_0359;
					IL_0359:
					num2 = 44;
					flag2 = false;
					goto IL_035f;
					IL_035f:
					num2 = 45;
					FrmSQLQuery[0].fSaveAs = fSaveAs;
					goto IL_0371;
					IL_0371:
					num2 = 46;
					FrmSQLQuery[0].fTitle = fTitle;
					goto IL_0383;
					end_IL_0001_3:
					break;
				}
				num2 = 63;
				Interaction.MsgBox("You cannot Email Queries used in a Pre/Post Query.", MsgBoxStyle.Exclamation, "Cannot Email Queries Used in a Pre/Post Query");
				break;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1402;
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

	private void mnuInstallR_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string lpString = default(string);
		string text2 = default(string);
		string text3 = default(string);
		int num6 = default(int);
		string text4 = default(string);
		int num7 = default(int);
		string text5 = default(string);
		long mySpace = default(long);
		string text6 = default(string);
		string text7 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string lpApplicationName;
				string lpKeyName;
				string lpFileName;
				int num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1692:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0026;
						case 7:
							goto IL_002f;
						case 8:
							goto IL_0035;
						case 9:
							goto IL_003e;
						case 10:
							goto IL_0048;
						case 11:
							goto IL_0052;
						case 12:
							goto IL_005c;
						case 13:
							goto IL_0066;
						case 14:
							goto IL_006c;
						case 15:
							goto IL_008c;
						case 17:
							goto IL_00f5;
						case 18:
							goto IL_00ff;
						case 19:
							goto IL_0109;
						case 21:
							goto IL_011a;
						case 22:
							goto IL_0124;
						case 23:
							goto IL_013d;
						case 24:
							goto IL_0147;
						case 25:
							goto IL_0151;
						case 26:
							goto IL_015b;
						case 28:
							goto IL_016c;
						case 29:
							goto IL_0185;
						case 30:
							goto IL_018f;
						case 31:
							goto IL_0199;
						case 32:
							goto IL_01a3;
						case 34:
							goto IL_01b1;
						case 35:
							goto IL_01ca;
						case 36:
							goto IL_01d4;
						case 37:
							goto IL_01de;
						case 38:
							goto IL_01e8;
						case 41:
							goto IL_01f7;
						case 42:
							goto IL_0201;
						case 43:
							goto IL_020b;
						case 44:
							goto IL_0224;
						case 46:
							goto IL_0232;
						case 45:
						case 47:
						case 48:
							goto IL_023d;
						case 49:
							goto IL_0247;
						case 16:
						case 20:
						case 27:
						case 33:
						case 39:
						case 40:
						case 50:
						case 52:
						case 53:
							goto IL_025d;
						case 54:
							goto IL_02a7;
						case 55:
							goto IL_02c0;
						case 57:
						case 58:
						case 59:
							goto IL_02da;
						case 60:
							goto IL_02f5;
						case 61:
							goto IL_030e;
						case 63:
							goto IL_0327;
						case 64:
							goto IL_036f;
						case 65:
							goto IL_0380;
						case 66:
							goto IL_03db;
						case 67:
							goto IL_03f3;
						case 69:
							goto IL_0411;
						case 70:
							goto IL_042d;
						case 71:
							goto IL_045e;
						case 72:
							goto IL_046c;
						case 73:
							goto IL_048d;
						case 74:
							goto IL_0497;
						case 75:
							goto IL_04a5;
						case 78:
							goto IL_04ec;
						case 79:
							goto IL_0505;
						case 62:
						case 68:
						case 76:
						case 77:
						case 80:
						case 81:
						case 82:
						case 83:
							goto IL_0517;
						case 84:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 51:
						case 56:
						case 85:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_04a5:
					num2 = 75;
					lpApplicationName = "SQLPATHFINDER";
					lpKeyName = Strings.UCase(text);
					lpFileName = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini";
					num5 = Globals_Renamed.WritePrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpString, ref lpFileName);
					goto IL_0517;
					IL_04ec:
					num2 = 78;
					if (Operators.CompareString(text2, "Oracle", TextCompare: false) == 0)
					{
						goto IL_0505;
					}
					goto IL_0517;
					IL_0497:
					num2 = 74;
					BuildForm.Save_DTRBuild_Ini("C");
					goto IL_04a5;
					IL_0505:
					num2 = 79;
					BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
					goto IL_0517;
					IL_000b:
					num2 = 2;
					text3 = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num6 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text4 = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					num7 = 0;
					goto IL_0026;
					IL_0026:
					num2 = 6;
					text5 = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					mySpace = 0L;
					goto IL_0035;
					IL_0035:
					num2 = 8;
					text2 = "";
					goto IL_003e;
					IL_003e:
					num2 = 9;
					text6 = "";
					goto IL_0048;
					IL_0048:
					num2 = 10;
					text7 = "";
					goto IL_0052;
					IL_0052:
					num2 = 11;
					text = "";
					goto IL_005c;
					IL_005c:
					num2 = 12;
					lpString = "";
					goto IL_0066;
					IL_0066:
					num2 = 13;
					num5 = 0;
					goto IL_006c;
					IL_006c:
					num2 = 14;
					text4 = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
					goto IL_008c;
					IL_008c:
					num2 = 15;
					switch (text4)
					{
					case "O":
						break;
					case "PYTHON3":
					case "PYTHON3_NXT":
					case "PYTHON3_OLD":
						goto IL_011a;
					case "R_DEF":
					case "R_NXT":
						goto IL_01f7;
					default:
						goto end_IL_0001_3;
					}
					goto IL_00f5;
					IL_01f7:
					num2 = 41;
					text2 = "R";
					goto IL_0201;
					IL_0201:
					num2 = 42;
					text6 = "UpdateR.bat";
					goto IL_020b;
					IL_020b:
					num2 = 43;
					if (Operators.CompareString(text4, "R_NXT", TextCompare: false) == 0)
					{
						goto IL_0224;
					}
					goto IL_0232;
					IL_0224:
					num2 = 44;
					text7 = "NEXT";
					goto IL_023d;
					IL_0232:
					num2 = 46;
					text7 = "DEFAULT";
					goto IL_023d;
					IL_023d:
					num2 = 48;
					text = "RTERM";
					goto IL_0247;
					IL_0247:
					num2 = 49;
					mySpace = 800L;
					goto IL_025d;
					IL_011a:
					num2 = 21;
					text6 = "UpdatePython.bat";
					goto IL_0124;
					IL_0124:
					num2 = 22;
					if (Operators.CompareString(text4, "PYTHON3", TextCompare: false) == 0)
					{
						goto IL_013d;
					}
					goto IL_016c;
					IL_013d:
					num2 = 23;
					text2 = "Python-V3";
					goto IL_0147;
					IL_0147:
					num2 = 24;
					text7 = "V3";
					goto IL_0151;
					IL_0151:
					num2 = 25;
					text = "Python3";
					goto IL_015b;
					IL_015b:
					num2 = 26;
					mySpace = 7080L;
					goto IL_025d;
					IL_016c:
					num2 = 28;
					if (Operators.CompareString(text4, "PYTHON3_NXT", TextCompare: false) == 0)
					{
						goto IL_0185;
					}
					goto IL_01b1;
					IL_0185:
					num2 = 29;
					text2 = "Python-V3";
					goto IL_018f;
					IL_018f:
					num2 = 30;
					text7 = "V3N";
					goto IL_0199;
					IL_0199:
					num2 = 31;
					text = "Python3";
					goto IL_01a3;
					IL_01a3:
					num2 = 32;
					mySpace = 7440L;
					goto IL_025d;
					IL_01b1:
					num2 = 34;
					if (Operators.CompareString(text4, "PYTHON3_OLD", TextCompare: false) == 0)
					{
						goto IL_01ca;
					}
					goto IL_025d;
					IL_01ca:
					num2 = 35;
					text2 = "Python-V3";
					goto IL_01d4;
					IL_01d4:
					num2 = 36;
					text7 = "V3O";
					goto IL_01de;
					IL_01de:
					num2 = 37;
					text = "Python3";
					goto IL_01e8;
					IL_01e8:
					num2 = 38;
					mySpace = 7080L;
					goto IL_025d;
					IL_00f5:
					num2 = 17;
					text2 = "Oracle";
					goto IL_00ff;
					IL_00ff:
					num2 = 18;
					text6 = "UpdateOracle.bat";
					goto IL_0109;
					IL_0109:
					num2 = 19;
					mySpace = 250L;
					goto IL_025d;
					IL_025d:
					num2 = 53;
					if (Operators.CompareString(text2, "Python-V3", TextCompare: false) == 0 && MyProject.Computer.FileSystem.FileExists(MyProject.Application.Info.DirectoryPath + "\\" + text + "\\python.exe"))
					{
						goto IL_02a7;
					}
					goto IL_02da;
					IL_0517:
					num2 = 83;
					FileSystem.ChDrive(Globals_Renamed.MyPCDir);
					break;
					IL_02a7:
					num2 = 54;
					num6 = (int)Interaction.MsgBox("Python 3 is already installed. Do you wish to update it?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Python 3");
					goto IL_02c0;
					IL_02c0:
					num2 = 55;
					if (num6 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_02da;
					IL_02da:
					num2 = 59;
					text5 = BuildForm.check_disk_space(MyProject.Application.Info.DirectoryPath, mySpace);
					goto IL_02f5;
					IL_02f5:
					num2 = 60;
					if (Operators.CompareString(text5, "", TextCompare: false) != 0)
					{
						goto IL_030e;
					}
					goto IL_0327;
					IL_030e:
					num2 = 61;
					Interaction.MsgBox(text5, MsgBoxStyle.Exclamation, "Space Check");
					goto IL_0517;
					IL_0327:
					num2 = 63;
					num6 = (int)Interaction.MsgBox("This procedure updates your SQLPathFinder " + text2 + " install. Do make sure you are not running any queries or you may get permission denied errors. Continue with the " + text2 + " update?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Update " + text2);
					goto IL_036f;
					IL_036f:
					num2 = 64;
					if (num6 == 6)
					{
						goto IL_0380;
					}
					goto IL_0517;
					IL_0380:
					num2 = 65;
					text3 = General_Procedures.Run_Batch_Shell("cmd /c \"" + MyProject.Application.Info.DirectoryPath + "\\" + text6 + "\" " + Globals_Renamed.gInstallShare + " " + text7, 1, 0, 0);
					goto IL_03db;
					IL_03db:
					num2 = 66;
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						goto IL_03f3;
					}
					goto IL_0411;
					IL_03f3:
					num2 = 67;
					Interaction.MsgBox(text3, MsgBoxStyle.Exclamation, "Error Updating " + text2);
					goto IL_0517;
					IL_0411:
					num2 = 69;
					if (Operators.CompareString(text2, "R", TextCompare: false) == 0)
					{
						goto IL_042d;
					}
					goto IL_04ec;
					IL_042d:
					num2 = 70;
					num7 = (int)Interaction.MsgBox("Do you wish your SQLPathFinder " + text2 + " path to point to the new install?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Update " + text2 + "?");
					goto IL_045e;
					IL_045e:
					num2 = 71;
					if (num7 == 6)
					{
						goto IL_046c;
					}
					goto IL_0517;
					IL_046c:
					num2 = 72;
					Globals_Renamed.gMyRPath = MyProject.Application.Info.DirectoryPath + "\\R\\R-Latest\\bin\\x64\\RTerm.exe";
					goto IL_048d;
					IL_048d:
					num2 = 73;
					lpString = Globals_Renamed.gMyRPath;
					goto IL_0497;
					end_IL_0001_2:
					break;
				}
				num2 = 84;
				FileSystem.ChDir(Globals_Renamed.MyPCDir);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1692;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuPivotSQLite_Click(object sender, EventArgs e)
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
							goto IL_001c;
						case 5:
							goto IL_002f;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (mnuPivotSQLite.Checked)
					{
						goto IL_001c;
					}
					goto IL_002f;
					IL_001c:
					num2 = 3;
					mnuPivotSQLite.Checked = false;
					goto end_IL_0001_3;
					IL_002f:
					num2 = 5;
					mnuPivotSQLite.Checked = true;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				mnuPivotPython.Checked = false;
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

	private void mnuSPFSChema_Click(object sender, EventArgs e)
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

	private void mnuQGlobals_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmSetGlobals frmSetGlobals = default(FrmSetGlobals);
		while (true)
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
					frmSetGlobals = new FrmSetGlobals();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				frmSetGlobals.ShowDialog();
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

	private void mnuEditTime_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmSetGlobals frmSetGlobals = default(FrmSetGlobals);
		while (true)
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
				case 79:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					frmSetGlobals = new FrmSetGlobals();
					goto IL_0013;
					IL_0013:
					num2 = 3;
					frmSetGlobals.f_Mode = "E";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				frmSetGlobals.ShowDialog();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 79;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuFindTreePtn_Click(object sender, EventArgs e)
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
				case 61:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].SearchColinTree();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 61;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuSHCleanup_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		int num3 = default(int);
		string fTitle = default(string);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				short design_Mode;
				short num4;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_0010;
					}
					goto IL_002b;
				case 670:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
						case 3:
							break;
						case 1:
							goto IL_01fe;
						default:
							goto end_IL_0001;
						}
						goto IL_01aa;
					}
					IL_00aa:
					num2 = 13;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle = fTitle;
					goto IL_00c0;
					IL_00c0:
					num2 = 14;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveAs = true;
					goto IL_00d5;
					IL_009c:
					num2 = 12;
					num4 = OpenQuery(text, 1);
					goto IL_00aa;
					IL_01fe:
					num5 = num + 1;
					num = 0;
					switch (num5)
					{
					case 1:
						break;
					case 2:
						goto IL_0010;
					case 4:
						goto IL_002b;
					case 5:
						goto IL_0049;
					case 6:
						goto IL_0059;
					case 7:
						goto IL_006e;
					case 8:
						goto IL_0073;
					case 9:
						goto IL_0078;
					case 10:
						goto IL_0081;
					case 12:
						goto IL_009c;
					case 13:
						goto IL_00aa;
					case 14:
						goto IL_00c0;
					case 15:
						goto IL_00d5;
					case 17:
						goto IL_00e5;
					case 18:
						goto IL_00ee;
					case 19:
						goto IL_00f8;
					case 20:
						goto IL_010d;
					case 21:
						goto IL_0121;
					case 22:
						goto IL_012a;
					case 23:
						goto IL_013c;
					case 24:
						goto IL_0149;
					case 25:
						goto IL_0157;
					case 26:
						goto IL_015f;
					case 27:
						goto IL_016d;
					case 28:
						goto IL_0183;
					case 29:
						goto IL_0198;
					case 33:
						goto IL_01aa;
					case 34:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 3:
					case 11:
					case 16:
					case 30:
					case 31:
					case 32:
					case 35:
					case 36:
						goto end_IL_0001_3;
					}
					goto default;
					IL_01aa:
					num2 = 33;
					Interaction.MsgBox("Error loading query : " + text + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Load Error");
					break;
					IL_0010:
					num2 = 2;
					Interaction.MsgBox("Best not to run these Query utilities from within a Pre/Post query", MsgBoxStyle.Exclamation, "Pre/Post Query");
					goto end_IL_0001_3;
					IL_002b:
					num2 = 4;
					text2 = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
					goto IL_0049;
					IL_0049:
					num2 = 5;
					fTitle = Globals_Renamed.MyPCDir + text2;
					goto IL_0059;
					IL_0059:
					num2 = 6;
					text = Globals_Renamed.MySchemaDir + "\\" + text2;
					goto IL_006e;
					IL_006e:
					num2 = 7;
					num4 = 0;
					goto IL_0073;
					IL_0073:
					num2 = 8;
					num6 = 0;
					goto IL_0078;
					IL_0078:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_0081;
					IL_0081:
					num2 = 10;
					design_Mode = Globals_Renamed.Design_Mode;
					if (design_Mode == 0)
					{
						goto IL_009c;
					}
					if (design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_00e5;
					IL_00d5:
					num2 = 15;
					DoRunQuery_M();
					goto end_IL_0001_3;
					IL_00e5:
					num2 = 17;
					Globals_Renamed.QueryCancel = 0;
					goto IL_00ee;
					IL_00ee:
					num2 = 18;
					num6 = Globals_Renamed.g_FrmIdx;
					goto IL_00f8;
					IL_00f8:
					num2 = 19;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
					goto IL_010d;
					IL_010d:
					num2 = 20;
					if (Globals_Renamed.QueryCancel != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0121;
					IL_0121:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_012a;
					IL_012a:
					num2 = 22;
					FrmSQLQuery[num6].Dispose();
					goto IL_013c;
					IL_013c:
					num2 = 23;
					FrmSQLQuery[num6] = null;
					goto IL_0149;
					IL_0149:
					num2 = 24;
					Information.Err().Clear();
					goto IL_0157;
					IL_0157:
					ProjectData.ClearProjectError();
					num3 = 3;
					goto IL_015f;
					IL_015f:
					num2 = 26;
					num4 = OpenQuery(text, 1);
					goto IL_016d;
					IL_016d:
					num2 = 27;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].fTitle = fTitle;
					goto IL_0183;
					IL_0183:
					num2 = 28;
					FrmSQLQuery[Globals_Renamed.g_FrmIdx].fSaveAs = true;
					goto IL_0198;
					IL_0198:
					num2 = 29;
					DoRunQuery_M();
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 34;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 670;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuSaveIcon_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmUtil frmUtil = default(FrmUtil);
		while (true)
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
							goto IL_0013;
						case 4:
							goto IL_001f;
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
					frmUtil = new FrmUtil();
					goto IL_0013;
					IL_0013:
					num2 = 3;
					Globals_Renamed.currhelptmp = "CREATE-QUERY-SHORTCUT";
					goto IL_001f;
					IL_001f:
					num2 = 4;
					frmUtil.fVaFileName = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				frmUtil.ShowDialog();
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

	private void mnuPivotPython_Click(object sender, EventArgs e)
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
							goto IL_001c;
						case 5:
							goto IL_002f;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (mnuPivotPython.Checked)
					{
						goto IL_001c;
					}
					goto IL_002f;
					IL_001c:
					num2 = 3;
					mnuPivotPython.Checked = false;
					goto end_IL_0001_3;
					IL_002f:
					num2 = 5;
					mnuPivotPython.Checked = true;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				mnuPivotSQLite.Checked = false;
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

	private void mnuSHGUIAll_Click(object sender, EventArgs e)
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
				case 49:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				BuildForm.Start_SHGUI();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 49;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void MnuSASOptions_DropDownOpening(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
							goto IL_000f;
						case 4:
							goto IL_0038;
						case 5:
						case 6:
							goto IL_003e;
						case 7:
							goto IL_0047;
						case 8:
							goto IL_0056;
						case 10:
							goto IL_0069;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0047:
					num2 = 7;
					mnuPivotMode.Visible = false;
					goto IL_0056;
					IL_0056:
					num2 = 8;
					mnuLegacyPivotHdrs.Visible = true;
					goto end_IL_0001_3;
					IL_003e:
					num2 = 6;
					if (flag)
					{
						goto IL_0047;
					}
					goto IL_0069;
					IL_0069:
					num2 = 10;
					mnuPivotMode.Visible = true;
					break;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (cmdIcon.Items[17].BackColor == Color.Orange)
					{
						goto IL_0038;
					}
					goto IL_003e;
					IL_0038:
					num2 = 4;
					flag = true;
					goto IL_003e;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				mnuLegacyPivotHdrs.Visible = false;
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

	private void mnuFocusTree_Click(object sender, EventArgs e)
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
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Focus();
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

	private void mnuFindCol_Click(object sender, EventArgs e)
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
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				FrmSQLQuery[Globals_Renamed.g_FrmIdx].TxtTreeSearch.Focus();
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

	private void mnuusepyorva_Click(object sender, EventArgs e)
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
				Python_Legacy_Engine("0");
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

	private void GetVersionConfigToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmDTREdit frmDTREdit = default(FrmDTREdit);
		while (true)
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
							goto IL_0013;
						case 4:
							goto IL_0026;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_0048;
						case 7:
							goto IL_0055;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_0073;
						case 10:
							goto IL_007d;
						case 11:
							goto IL_0087;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0073:
					num2 = 9;
					frmDTREdit.KeepSQL = false;
					goto IL_007d;
					IL_007d:
					num2 = 10;
					frmDTREdit.fSaveSQL = false;
					goto IL_0087;
					IL_0064:
					num2 = 8;
					frmDTREdit.Text1.SelectionLength = 0;
					goto IL_0073;
					IL_0087:
					num2 = 11;
					frmDTREdit.ShowDialog();
					break;
					IL_000b:
					num2 = 2;
					frmDTREdit = new FrmDTREdit();
					goto IL_0013;
					IL_0013:
					num2 = 3;
					frmDTREdit.Text1.Text = BuildForm.Get_Version_Config();
					goto IL_0026;
					IL_0026:
					num2 = 4;
					frmDTREdit.cmbSQLVA.Text = "WRITE";
					goto IL_0039;
					IL_0039:
					num2 = 5;
					frmDTREdit.cmbSQLVA.Enabled = false;
					goto IL_0048;
					IL_0048:
					num2 = 6;
					frmDTREdit.out_odbc = "--";
					goto IL_0055;
					IL_0055:
					num2 = 7;
					frmDTREdit.Text1.SelectionStart = 0;
					goto IL_0064;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				frmDTREdit.Dispose();
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

	private void mnuSaveQSPFSQL_Click(object sender, EventArgs e)
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
				BuildForm.Save_SPFSQL("S");
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

	private void mnuNotePad_Click(object sender, EventArgs e)
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
							goto IL_0033;
						case 4:
							goto IL_0059;
						case 5:
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					mnuTxtView.Tag = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
					goto IL_0033;
					IL_0033:
					num2 = 3;
					if (!Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null), "spfgrid", TextCompare: false))
					{
						break;
					}
					goto IL_0059;
					IL_0059:
					num2 = 4;
					mnuTxtView.Image = Resources.spfgrid2;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				Refresh();
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

	private void mnuSQLPos_Click(object sender, EventArgs e)
	{
		if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0 && FrmCmdSimf != null && !FrmCmdSimf.IsDisposed)
		{
			FrmCmdSimf.StartPosition = FormStartPosition.Manual;
			FrmCmdSimf.Location = base.Location;
			FrmCmdSimf.Size = new Size(700, 500);
			FrmCmdSimf.StartPosition = FormStartPosition.CenterScreen;
			FrmCmdSimf.Show();
			FrmCmdSimf.WindowState = FormWindowState.Normal;
			FrmCmdSimf.TopMost = true;
			FrmCmdSimf.TopMost = false;
		}
	}
}

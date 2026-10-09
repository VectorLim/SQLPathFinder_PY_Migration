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
internal class FrmMultiQuery : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQNew")]
	private ToolStripMenuItem _mnuMQNew;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQOpen")]
	private ToolStripMenuItem _mnuMQOpen;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQSave")]
	private ToolStripMenuItem _mnuMQSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQSaveAS")]
	private ToolStripMenuItem _mnuMQSaveAS;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQRun")]
	private ToolStripMenuItem _mnuMQRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQExit")]
	private ToolStripMenuItem _mnuMQExit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQSHGUI")]
	private ToolStripMenuItem _mnuMQSHGUI;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuMQHelp")]
	private ToolStripMenuItem _mnuMQHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_TbMQ_Button1")]
	private ToolStripButton __TbMQ_Button1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_TbMQ_Button2")]
	private ToolStripButton __TbMQ_Button2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_TbMQ_Button3")]
	private ToolStripButton __TbMQ_Button3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_TbMQ_Button4")]
	private ToolStripButton __TbMQ_Button4;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCopy")]
	private Button _cmdCopy;

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
	[AccessedThroughProperty("cmdUp")]
	private Button _cmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdSaveScripts")]
	private Button _cmdSaveScripts;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtSHGUIDir")]
	private ComboBox _TxtSHGUIDir;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdSaveAddScripts")]
	private Button _cmdSaveAddScripts;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdEdit")]
	private Button _cmdEdit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBrowse")]
	private Button _cmdBrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridMQ2")]
	private DataGridView _GridMQ2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("EditQueryScriptToolStripMenuItem")]
	private ToolStripMenuItem _EditQueryScriptToolStripMenuItem;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("LoadQuryScriptToolStripMenuItem")]
	private ToolStripMenuItem _LoadQuryScriptToolStripMenuItem;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptRun0")]
	private RadioButton _OptRun0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OptRun1")]
	private RadioButton _OptRun1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdInsert")]
	private Button _cmdInsert;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuInsert")]
	private ToolStripMenuItem _mnuInsert;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSaveExeAs")]
	private ToolStripMenuItem _mnuSaveExeAs;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuSetQDir")]
	private ToolStripMenuItem _mnuSetQDir;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("_TbMQ_Button5")]
	private ToolStripButton __TbMQ_Button5;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuPySH")]
	private ToolStripMenuItem _mnuPySH;

	public bool fSaveAs;

	private string fTitle;

	private string f_ShguiDir;

	public string F_FormMode;

	public string f_Job;

	public string SaveWindowState;

	private string MyDefaultDir;

	private bool l_IsInit;

	private string Batch_ScriptName;

	public virtual ToolStripMenuItem mnuMQNew
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQNew;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQNew_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQNew;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQNew = value;
			toolStripMenuItem = _mnuMQNew;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMQOpen
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQOpen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQOpen_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQOpen;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQOpen = value;
			toolStripMenuItem = _mnuMQOpen;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMQSave
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQSave;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQSave_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQSave = value;
			toolStripMenuItem = _mnuMQSave;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMQSaveAS
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQSaveAS;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQSaveAS_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQSaveAS;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQSaveAS = value;
			toolStripMenuItem = _mnuMQSaveAS;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMQRun
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQRun_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQRun = value;
			toolStripMenuItem = _mnuMQRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnumqdash0")]
	public virtual ToolStripSeparator mnumqdash0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuMQExit
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQExit_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQExit = value;
			toolStripMenuItem = _mnuMQExit;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuMQFile")]
	public virtual ToolStripMenuItem mnuMQFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ToolStripMenuItem mnuMQSHGUI
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQSHGUI;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQSHGUI_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQSHGUI;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQSHGUI = value;
			toolStripMenuItem = _mnuMQSHGUI;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public virtual ToolStripMenuItem mnuMQHelp
	{
		[CompilerGenerated]
		get
		{
			return _mnuMQHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuMQHelp_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuMQHelp;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuMQHelp = value;
			toolStripMenuItem = _mnuMQHelp;
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

	public virtual ToolStripButton _TbMQ_Button1
	{
		[CompilerGenerated]
		get
		{
			return __TbMQ_Button1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TbMQ_ButtonClick;
			ToolStripButton toolStripButton = __TbMQ_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__TbMQ_Button1 = value;
			toolStripButton = __TbMQ_Button1;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _TbMQ_Button2
	{
		[CompilerGenerated]
		get
		{
			return __TbMQ_Button2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TbMQ_ButtonClick;
			ToolStripButton toolStripButton = __TbMQ_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__TbMQ_Button2 = value;
			toolStripButton = __TbMQ_Button2;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _TbMQ_Button3
	{
		[CompilerGenerated]
		get
		{
			return __TbMQ_Button3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TbMQ_ButtonClick;
			ToolStripButton toolStripButton = __TbMQ_Button3;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__TbMQ_Button3 = value;
			toolStripButton = __TbMQ_Button3;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public virtual ToolStripButton _TbMQ_Button4
	{
		[CompilerGenerated]
		get
		{
			return __TbMQ_Button4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TbMQ_ButtonClick;
			ToolStripButton toolStripButton = __TbMQ_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__TbMQ_Button4 = value;
			toolStripButton = __TbMQ_Button4;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TbMQ")]
	public virtual ToolStrip TbMQ
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	public virtual Button cmdDelete
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

	public virtual Button cmdDown
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

	public virtual Button cmdUp
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

	[field: AccessedThroughProperty("cmbQueue")]
	public virtual ComboBox cmbQueue
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdSaveScripts
	{
		[CompilerGenerated]
		get
		{
			return _cmdSaveScripts;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSaveScripts_Click;
			Button button = _cmdSaveScripts;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdSaveScripts = value;
			button = _cmdSaveScripts;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtDir")]
	public virtual ComboBox TxtDir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual ComboBox TxtSHGUIDir
	{
		[CompilerGenerated]
		get
		{
			return _TxtSHGUIDir;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TxtSHGUIDir_SelectedIndexChanged;
			ComboBox comboBox = _TxtSHGUIDir;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			_TxtSHGUIDir = value;
			comboBox = _TxtSHGUIDir;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

	public virtual Button cmdSaveAddScripts
	{
		[CompilerGenerated]
		get
		{
			return _cmdSaveAddScripts;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSaveAddScripts_Click;
			Button button = _cmdSaveAddScripts;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdSaveAddScripts = value;
			button = _cmdSaveAddScripts;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtName")]
	public virtual TextBox TxtName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblBatchTitle")]
	public virtual Label lblBatchTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblLog")]
	public virtual Label lblLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblSHGUI")]
	public virtual Label lblSHGUI
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblJob")]
	public virtual Label lblJob
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblVADir")]
	public virtual Label lblVADir
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FrmBatch")]
	public virtual GroupBox FrmBatch
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdEdit
	{
		[CompilerGenerated]
		get
		{
			return _cmdEdit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEdit_Click;
			Button button = _cmdEdit;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdEdit = value;
			button = _cmdEdit;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdBrowse
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

	[field: AccessedThroughProperty("lblq")]
	public virtual Label lblq
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual DataGridView GridMQ2
	{
		[CompilerGenerated]
		get
		{
			return _GridMQ2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridMQ2_CellEndEdit;
			DataGridViewColumnEventHandler value3 = GridMQ2_ColumnWidthChanged;
			DragEventHandler value4 = GridMQ2_DragDrop;
			DragEventHandler value5 = GridMQ2_DragOver;
			MouseEventHandler value6 = GridMQ2_MouseDown;
			KeyEventHandler value7 = GridMQ2_KeyDown;
			DataGridView dataGridView = _GridMQ2;
			if (dataGridView != null)
			{
				dataGridView.CellEndEdit -= value2;
				dataGridView.ColumnWidthChanged -= value3;
				dataGridView.DragDrop -= value4;
				dataGridView.DragOver -= value5;
				dataGridView.MouseDown -= value6;
				dataGridView.KeyDown -= value7;
			}
			_GridMQ2 = value;
			dataGridView = _GridMQ2;
			if (dataGridView != null)
			{
				dataGridView.CellEndEdit += value2;
				dataGridView.ColumnWidthChanged += value3;
				dataGridView.DragDrop += value4;
				dataGridView.DragOver += value5;
				dataGridView.MouseDown += value6;
				dataGridView.KeyDown += value7;
			}
		}
	}

	[field: AccessedThroughProperty("column1")]
	internal virtual DataGridViewTextBoxColumn column1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column2")]
	internal virtual DataGridViewTextBoxColumn Column2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ContextMenuGridMQ2")]
	internal virtual ContextMenuStrip ContextMenuGridMQ2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem EditQueryScriptToolStripMenuItem
	{
		[CompilerGenerated]
		get
		{
			return _EditQueryScriptToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = EditQueryScriptToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = _EditQueryScriptToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_EditQueryScriptToolStripMenuItem = value;
			toolStripMenuItem = _EditQueryScriptToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem LoadQuryScriptToolStripMenuItem
	{
		[CompilerGenerated]
		get
		{
			return _LoadQuryScriptToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = LoadQuryScriptToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = _LoadQuryScriptToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_LoadQuryScriptToolStripMenuItem = value;
			toolStripMenuItem = _LoadQuryScriptToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual RadioButton OptRun0
	{
		[CompilerGenerated]
		get
		{
			return _OptRun0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptRun0_CheckedChanged;
			RadioButton radioButton = _OptRun0;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptRun0 = value;
			radioButton = _OptRun0;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	internal virtual RadioButton OptRun1
	{
		[CompilerGenerated]
		get
		{
			return _OptRun1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OptRun0_CheckedChanged;
			RadioButton radioButton = _OptRun1;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			_OptRun1 = value;
			radioButton = _OptRun1;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	internal virtual Button cmdInsert
	{
		[CompilerGenerated]
		get
		{
			return _cmdInsert;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdInsert_Click;
			Button button = _cmdInsert;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdInsert = value;
			button = _cmdInsert;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuInsert
	{
		[CompilerGenerated]
		get
		{
			return _mnuInsert;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdInsert_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuInsert;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuInsert = value;
			toolStripMenuItem = _mnuInsert;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TBTxtLog")]
	internal virtual ToolStripTextBox TBTxtLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuOptions")]
	internal virtual ToolStripMenuItem mnuOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuUseTmp")]
	internal virtual ToolStripMenuItem mnuUseTmp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSaveExeAs
	{
		[CompilerGenerated]
		get
		{
			return _mnuSaveExeAs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSaveExeAs_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSaveExeAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSaveExeAs = value;
			toolStripMenuItem = _mnuSaveExeAs;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuSetQDir
	{
		[CompilerGenerated]
		get
		{
			return _mnuSetQDir;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSetQDir_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSetQDir;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSetQDir = value;
			toolStripMenuItem = _mnuSetQDir;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbSite")]
	internal virtual ComboBox cmbSite
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblSite")]
	internal virtual Label LblSite
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblMem")]
	internal virtual Label LblMem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbMem")]
	internal virtual ComboBox cmbMem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbjmp")]
	internal virtual ComboBox cmbjmp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbljmp")]
	internal virtual Label lbljmp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton _TbMQ_Button5
	{
		[CompilerGenerated]
		get
		{
			return __TbMQ_Button5;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TbMQ_ButtonClick;
			ToolStripButton toolStripButton = __TbMQ_Button5;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			__TbMQ_Button5 = value;
			toolStripButton = __TbMQ_Button5;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbType")]
	internal virtual ComboBox cmbType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtType")]
	internal virtual Label TxtType
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuPySH
	{
		[CompilerGenerated]
		get
		{
			return _mnuPySH;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuPySH_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuPySH;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuPySH = value;
			toolStripMenuItem = _mnuPySH;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[DebuggerNonUserCode]
	public FrmMultiQuery()
	{
		base.Load += FrmMultiQuery_Load;
		base.FormClosing += FrmMultiQuery_FormClosing;
		base.Resize += FrmMultiQuery_Resize;
		base.FormClosed += FrmMultiQuery_FormClosed;
		fSaveAs = true;
		fTitle = "";
		f_ShguiDir = "";
		F_FormMode = "NORMAL";
		f_Job = "";
		SaveWindowState = "N";
		MyDefaultDir = Globals_Renamed.gQueryDir;
		l_IsInit = false;
		Batch_ScriptName = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmMultiQuery));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.TxtName = new System.Windows.Forms.TextBox();
		this.OptRun0 = new System.Windows.Forms.RadioButton();
		this.OptRun1 = new System.Windows.Forms.RadioButton();
		this.cmdInsert = new System.Windows.Forms.Button();
		this.cmdCopy = new System.Windows.Forms.Button();
		this.cmdDelete = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdSaveScripts = new System.Windows.Forms.Button();
		this.cmdSaveAddScripts = new System.Windows.Forms.Button();
		this.cmdEdit = new System.Windows.Forms.Button();
		this.cmdBrowse = new System.Windows.Forms.Button();
		this.LblMem = new System.Windows.Forms.Label();
		this.MainMenu1 = new System.Windows.Forms.MenuStrip();
		this.mnuMQFile = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQNew = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQSave = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQSaveAS = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQRun = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSaveExeAs = new System.Windows.Forms.ToolStripMenuItem();
		this.mnumqdash0 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuMQExit = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQSHGUI = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuUseTmp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSetQDir = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuPySH = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuMQHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.TbMQ = new System.Windows.Forms.ToolStrip();
		this._TbMQ_Button1 = new System.Windows.Forms.ToolStripButton();
		this._TbMQ_Button2 = new System.Windows.Forms.ToolStripButton();
		this._TbMQ_Button3 = new System.Windows.Forms.ToolStripButton();
		this._TbMQ_Button4 = new System.Windows.Forms.ToolStripButton();
		this.TBTxtLog = new System.Windows.Forms.ToolStripTextBox();
		this._TbMQ_Button5 = new System.Windows.Forms.ToolStripButton();
		this.FrmBatch = new System.Windows.Forms.GroupBox();
		this.cmbType = new System.Windows.Forms.ComboBox();
		this.TxtType = new System.Windows.Forms.Label();
		this.cmbjmp = new System.Windows.Forms.ComboBox();
		this.lbljmp = new System.Windows.Forms.Label();
		this.cmbMem = new System.Windows.Forms.ComboBox();
		this.cmbSite = new System.Windows.Forms.ComboBox();
		this.LblSite = new System.Windows.Forms.Label();
		this.cmbQueue = new System.Windows.Forms.ComboBox();
		this.TxtDir = new System.Windows.Forms.ComboBox();
		this.TxtSHGUIDir = new System.Windows.Forms.ComboBox();
		this.lblBatchTitle = new System.Windows.Forms.Label();
		this.lblLog = new System.Windows.Forms.Label();
		this.lblSHGUI = new System.Windows.Forms.Label();
		this.lblJob = new System.Windows.Forms.Label();
		this.lblVADir = new System.Windows.Forms.Label();
		this.lblq = new System.Windows.Forms.Label();
		this.GridMQ2 = new System.Windows.Forms.DataGridView();
		this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ContextMenuGridMQ2 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.EditQueryScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadQuryScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
		this.MainMenu1.SuspendLayout();
		this.TbMQ.SuspendLayout();
		this.FrmBatch.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridMQ2).BeginInit();
		this.ContextMenuGridMQ2.SuspendLayout();
		base.SuspendLayout();
		this.TxtName.AcceptsReturn = true;
		this.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtName.BackColor = System.Drawing.SystemColors.Window;
		this.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtName.Enabled = false;
		this.TxtName.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtName.Location = new System.Drawing.Point(130, 66);
		this.TxtName.MaxLength = 0;
		this.TxtName.Name = "TxtName";
		this.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtName.Size = new System.Drawing.Size(468, 29);
		this.TxtName.TabIndex = 7;
		this.ToolTip1.SetToolTip(this.TxtName, "Specify a Job Name");
		this.OptRun0.BackColor = System.Drawing.SystemColors.Control;
		this.OptRun0.Checked = true;
		this.OptRun0.Location = new System.Drawing.Point(296, 27);
		this.OptRun0.Name = "OptRun0";
		this.OptRun0.Size = new System.Drawing.Size(100, 18);
		this.OptRun0.TabIndex = 25;
		this.OptRun0.TabStop = true;
		this.OptRun0.Text = "Interactive";
		this.ToolTip1.SetToolTip(this.OptRun0, "Run Scripts Interactively");
		this.OptRun0.UseVisualStyleBackColor = false;
		this.OptRun1.AutoSize = true;
		this.OptRun1.BackColor = System.Drawing.SystemColors.Control;
		this.OptRun1.Location = new System.Drawing.Point(402, 27);
		this.OptRun1.Name = "OptRun1";
		this.OptRun1.Size = new System.Drawing.Size(83, 26);
		this.OptRun1.TabIndex = 26;
		this.OptRun1.Text = "Batch";
		this.ToolTip1.SetToolTip(this.OptRun1, "Run scripts in Batch on ScriptHost");
		this.OptRun1.UseVisualStyleBackColor = false;
		this.cmdInsert.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdInsert.BackColor = System.Drawing.SystemColors.Control;
		this.cmdInsert.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdInsert.ImageIndex = 8;
		this.cmdInsert.Location = new System.Drawing.Point(707, 274);
		this.cmdInsert.Name = "cmdInsert";
		this.cmdInsert.Size = new System.Drawing.Size(60, 40);
		this.cmdInsert.TabIndex = 5;
		this.cmdInsert.Text = "Ins";
		this.cmdInsert.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdInsert, "Insert an empty row");
		this.cmdInsert.UseVisualStyleBackColor = false;
		this.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCopy.BackColor = System.Drawing.SystemColors.Control;
		this.cmdCopy.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdCopy.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdCopy.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCopy.ImageIndex = 6;
		this.cmdCopy.Location = new System.Drawing.Point(707, 364);
		this.cmdCopy.Name = "cmdCopy";
		this.cmdCopy.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdCopy.Size = new System.Drawing.Size(60, 40);
		this.cmdCopy.TabIndex = 7;
		this.cmdCopy.Text = "Copy";
		this.cmdCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdCopy, "Copy Query to another location and Update Grid");
		this.cmdCopy.UseVisualStyleBackColor = false;
		this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDelete.BackColor = System.Drawing.SystemColors.Control;
		this.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdDelete.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdDelete.ImageIndex = 5;
		this.cmdDelete.Location = new System.Drawing.Point(707, 324);
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdDelete.Size = new System.Drawing.Size(60, 40);
		this.cmdDelete.TabIndex = 6;
		this.cmdDelete.Text = "Del";
		this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdDelete, "Delete Row");
		this.cmdDelete.UseVisualStyleBackColor = false;
		this.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown.BackColor = System.Drawing.SystemColors.Control;
		this.cmdDown.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdDown.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdDown.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdDown.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdDown.ImageIndex = 4;
		this.cmdDown.Location = new System.Drawing.Point(707, 234);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdDown.Size = new System.Drawing.Size(60, 40);
		this.cmdDown.TabIndex = 4;
		this.cmdDown.Text = "Down";
		this.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Row down");
		this.cmdDown.UseVisualStyleBackColor = false;
		this.cmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp.BackColor = System.Drawing.SystemColors.Control;
		this.cmdUp.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdUp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdUp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdUp.ImageIndex = 3;
		this.cmdUp.Location = new System.Drawing.Point(707, 194);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdUp.Size = new System.Drawing.Size(60, 40);
		this.cmdUp.TabIndex = 3;
		this.cmdUp.Text = "Up";
		this.cmdUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move Row up");
		this.cmdUp.UseVisualStyleBackColor = false;
		this.cmdSaveScripts.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdSaveScripts.BackColor = System.Drawing.SystemColors.Control;
		this.cmdSaveScripts.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdSaveScripts.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdSaveScripts.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdSaveScripts.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdSaveScripts.ImageIndex = 0;
		this.cmdSaveScripts.Location = new System.Drawing.Point(636, 15);
		this.cmdSaveScripts.Name = "cmdSaveScripts";
		this.cmdSaveScripts.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdSaveScripts.Size = new System.Drawing.Size(52, 59);
		this.cmdSaveScripts.TabIndex = 13;
		this.cmdSaveScripts.Text = "Save Only";
		this.cmdSaveScripts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdSaveScripts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdSaveScripts, "Save/Update Scripts for batch execution");
		this.cmdSaveScripts.UseVisualStyleBackColor = false;
		this.cmdSaveAddScripts.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdSaveAddScripts.BackColor = System.Drawing.SystemColors.Control;
		this.cmdSaveAddScripts.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdSaveAddScripts.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdSaveAddScripts.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdSaveAddScripts.ImageIndex = 7;
		this.cmdSaveAddScripts.Location = new System.Drawing.Point(636, 110);
		this.cmdSaveAddScripts.Name = "cmdSaveAddScripts";
		this.cmdSaveAddScripts.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdSaveAddScripts.Size = new System.Drawing.Size(52, 59);
		this.cmdSaveAddScripts.TabIndex = 15;
		this.cmdSaveAddScripts.Text = "Save / Add";
		this.cmdSaveAddScripts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdSaveAddScripts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdSaveAddScripts, "Save Scripts for batch execution and add job to ScriptHost");
		this.cmdSaveAddScripts.UseVisualStyleBackColor = false;
		this.cmdEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdEdit.BackColor = System.Drawing.SystemColors.Control;
		this.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdEdit.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdEdit.ImageIndex = 2;
		this.cmdEdit.Location = new System.Drawing.Point(707, 154);
		this.cmdEdit.Name = "cmdEdit";
		this.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdEdit.Size = new System.Drawing.Size(60, 40);
		this.cmdEdit.TabIndex = 2;
		this.cmdEdit.Text = "Edit";
		this.cmdEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdEdit, "Edit Selected Query/Script");
		this.cmdEdit.UseVisualStyleBackColor = false;
		this.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
		this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdBrowse.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdBrowse.ImageIndex = 1;
		this.cmdBrowse.Location = new System.Drawing.Point(707, 114);
		this.cmdBrowse.Name = "cmdBrowse";
		this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdBrowse.Size = new System.Drawing.Size(60, 40);
		this.cmdBrowse.TabIndex = 1;
		this.cmdBrowse.Text = "Load";
		this.cmdBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ToolTip1.SetToolTip(this.cmdBrowse, "Locate a Query to load");
		this.cmdBrowse.UseVisualStyleBackColor = false;
		this.LblMem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LblMem.AutoSize = true;
		this.LblMem.Location = new System.Drawing.Point(438, 118);
		this.LblMem.Name = "LblMem";
		this.LblMem.Size = new System.Drawing.Size(52, 22);
		this.LblMem.TabIndex = 23;
		this.LblMem.Text = "Mem";
		this.ToolTip1.SetToolTip(this.LblMem, "Memory");
		this.MainMenu1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
		this.MainMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mnuMQFile, this.mnuMQSHGUI, this.mnuOptions, this.mnuMQHelp });
		this.MainMenu1.Location = new System.Drawing.Point(0, 0);
		this.MainMenu1.Name = "MainMenu1";
		this.MainMenu1.Size = new System.Drawing.Size(770, 38);
		this.MainMenu1.TabIndex = 24;
		this.mnuMQFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.mnuMQNew, this.mnuMQOpen, this.mnuMQSave, this.mnuMQSaveAS, this.mnuMQRun, this.mnuSaveExeAs, this.mnumqdash0, this.mnuMQExit });
		this.mnuMQFile.Name = "mnuMQFile";
		this.mnuMQFile.Size = new System.Drawing.Size(62, 34);
		this.mnuMQFile.Text = "&File";
		this.mnuMQNew.Name = "mnuMQNew";
		this.mnuMQNew.Size = new System.Drawing.Size(475, 40);
		this.mnuMQNew.Text = "New Multi-Query File";
		this.mnuMQOpen.Name = "mnuMQOpen";
		this.mnuMQOpen.Size = new System.Drawing.Size(475, 40);
		this.mnuMQOpen.Text = "Open Multi-Query File (.spf)";
		this.mnuMQSave.Name = "mnuMQSave";
		this.mnuMQSave.Size = new System.Drawing.Size(475, 40);
		this.mnuMQSave.Text = "Save Multi-Query File";
		this.mnuMQSaveAS.Name = "mnuMQSaveAS";
		this.mnuMQSaveAS.Size = new System.Drawing.Size(475, 40);
		this.mnuMQSaveAS.Text = "Save Multi-Query File As ...";
		this.mnuMQRun.Name = "mnuMQRun";
		this.mnuMQRun.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.mnuMQRun.Size = new System.Drawing.Size(475, 40);
		this.mnuMQRun.Text = "Run Multi-Query File Interactively";
		this.mnuSaveExeAs.Name = "mnuSaveExeAs";
		this.mnuSaveExeAs.Size = new System.Drawing.Size(475, 40);
		this.mnuSaveExeAs.Text = "Save Exe/ZIP As ...";
		this.mnumqdash0.Name = "mnumqdash0";
		this.mnumqdash0.Size = new System.Drawing.Size(472, 6);
		this.mnuMQExit.Name = "mnuMQExit";
		this.mnuMQExit.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.mnuMQExit.Size = new System.Drawing.Size(475, 40);
		this.mnuMQExit.Text = "Exit";
		this.mnuMQSHGUI.Name = "mnuMQSHGUI";
		this.mnuMQSHGUI.Size = new System.Drawing.Size(259, 34);
		this.mnuMQSHGUI.Text = "ScriptHost Job Manager!";
		this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mnuUseTmp, this.mnuSetQDir, this.mnuPySH });
		this.mnuOptions.Name = "mnuOptions";
		this.mnuOptions.Size = new System.Drawing.Size(104, 34);
		this.mnuOptions.Text = "Options";
		this.mnuUseTmp.Checked = true;
		this.mnuUseTmp.CheckOnClick = true;
		this.mnuUseTmp.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuUseTmp.Name = "mnuUseTmp";
		this.mnuUseTmp.Size = new System.Drawing.Size(913, 40);
		this.mnuUseTmp.Text = "Do not auto-save output files on ScriptHost (must explicitly save) - Uses Temp Folder";
		this.mnuSetQDir.Checked = true;
		this.mnuSetQDir.CheckOnClick = true;
		this.mnuSetQDir.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuSetQDir.Name = "mnuSetQDir";
		this.mnuSetQDir.Size = new System.Drawing.Size(913, 40);
		this.mnuSetQDir.Text = "Queries with no path exist in my Query folder (unchecked = Work folder)";
		this.mnuSetQDir.ToolTipText = "Check to set default folder as Query Folder as opposed to the Work Folder";
		this.mnuPySH.CheckOnClick = true;
		this.mnuPySH.Name = "mnuPySH";
		this.mnuPySH.Size = new System.Drawing.Size(913, 40);
		this.mnuPySH.Text = "Use Python on ScriptHost";
		this.mnuMQHelp.Name = "mnuMQHelp";
		this.mnuMQHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.mnuMQHelp.Size = new System.Drawing.Size(113, 34);
		this.mnuMQHelp.Text = "&Help (F1)";
		this.TbMQ.BackColor = System.Drawing.SystemColors.Control;
		this.TbMQ.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.TbMQ.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this._TbMQ_Button1, this._TbMQ_Button2, this._TbMQ_Button3, this._TbMQ_Button4, this.TBTxtLog, this._TbMQ_Button5 });
		this.TbMQ.Location = new System.Drawing.Point(0, 38);
		this.TbMQ.Name = "TbMQ";
		this.TbMQ.Size = new System.Drawing.Size(770, 35);
		this.TbMQ.TabIndex = 23;
		this._TbMQ_Button1.AutoSize = false;
		this._TbMQ_Button1.Image = (System.Drawing.Image)resources.GetObject("_TbMQ_Button1.Image");
		this._TbMQ_Button1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._TbMQ_Button1.ImageTransparentColor = System.Drawing.Color.Red;
		this._TbMQ_Button1.Name = "_TbMQ_Button1";
		this._TbMQ_Button1.Size = new System.Drawing.Size(24, 22);
		this._TbMQ_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._TbMQ_Button1.ToolTipText = "Clear all Entries";
		this._TbMQ_Button2.AutoSize = false;
		this._TbMQ_Button2.Image = (System.Drawing.Image)resources.GetObject("_TbMQ_Button2.Image");
		this._TbMQ_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._TbMQ_Button2.ImageTransparentColor = System.Drawing.Color.White;
		this._TbMQ_Button2.Name = "_TbMQ_Button2";
		this._TbMQ_Button2.Size = new System.Drawing.Size(24, 22);
		this._TbMQ_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._TbMQ_Button2.ToolTipText = "Load a Multi-Query Definition";
		this._TbMQ_Button3.AutoSize = false;
		this._TbMQ_Button3.Image = (System.Drawing.Image)resources.GetObject("_TbMQ_Button3.Image");
		this._TbMQ_Button3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._TbMQ_Button3.ImageTransparentColor = System.Drawing.Color.Red;
		this._TbMQ_Button3.Name = "_TbMQ_Button3";
		this._TbMQ_Button3.Size = new System.Drawing.Size(24, 22);
		this._TbMQ_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._TbMQ_Button3.ToolTipText = "Save the Multi-Query Definition";
		this._TbMQ_Button4.AutoSize = false;
		this._TbMQ_Button4.Image = (System.Drawing.Image)resources.GetObject("_TbMQ_Button4.Image");
		this._TbMQ_Button4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this._TbMQ_Button4.ImageTransparentColor = System.Drawing.Color.Red;
		this._TbMQ_Button4.Name = "_TbMQ_Button4";
		this._TbMQ_Button4.Size = new System.Drawing.Size(24, 22);
		this._TbMQ_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this._TbMQ_Button4.ToolTipText = "Run Scripts interactively";
		this.TBTxtLog.Font = new System.Drawing.Font("Segoe UI", 9f);
		this.TBTxtLog.Name = "TBTxtLog";
		this.TBTxtLog.Size = new System.Drawing.Size(130, 35);
		this.TBTxtLog.ToolTipText = "Specify a log file when running the query interactively. Ignored for a Batch run";
		this._TbMQ_Button5.BackColor = System.Drawing.Color.Orange;
		this._TbMQ_Button5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._TbMQ_Button5.Image = (System.Drawing.Image)resources.GetObject("_TbMQ_Button5.Image");
		this._TbMQ_Button5.ImageTransparentColor = System.Drawing.Color.White;
		this._TbMQ_Button5.Name = "_TbMQ_Button5";
		this._TbMQ_Button5.Size = new System.Drawing.Size(40, 29);
		this._TbMQ_Button5.Tag = "1";
		this._TbMQ_Button5.Text = "Using the Python Extract Engine for all Scripts";
		this.FrmBatch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.FrmBatch.BackColor = System.Drawing.SystemColors.Control;
		this.FrmBatch.Controls.Add(this.cmbType);
		this.FrmBatch.Controls.Add(this.TxtType);
		this.FrmBatch.Controls.Add(this.cmbjmp);
		this.FrmBatch.Controls.Add(this.lbljmp);
		this.FrmBatch.Controls.Add(this.cmbMem);
		this.FrmBatch.Controls.Add(this.LblMem);
		this.FrmBatch.Controls.Add(this.cmbSite);
		this.FrmBatch.Controls.Add(this.LblSite);
		this.FrmBatch.Controls.Add(this.cmbQueue);
		this.FrmBatch.Controls.Add(this.cmdSaveScripts);
		this.FrmBatch.Controls.Add(this.TxtDir);
		this.FrmBatch.Controls.Add(this.TxtSHGUIDir);
		this.FrmBatch.Controls.Add(this.cmdSaveAddScripts);
		this.FrmBatch.Controls.Add(this.TxtName);
		this.FrmBatch.Controls.Add(this.lblBatchTitle);
		this.FrmBatch.Controls.Add(this.lblLog);
		this.FrmBatch.Controls.Add(this.lblSHGUI);
		this.FrmBatch.Controls.Add(this.lblJob);
		this.FrmBatch.Controls.Add(this.lblVADir);
		this.FrmBatch.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FrmBatch.ForeColor = System.Drawing.SystemColors.ControlText;
		this.FrmBatch.Location = new System.Drawing.Point(6, 363);
		this.FrmBatch.Name = "FrmBatch";
		this.FrmBatch.Padding = new System.Windows.Forms.Padding(0);
		this.FrmBatch.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.FrmBatch.Size = new System.Drawing.Size(695, 169);
		this.FrmBatch.TabIndex = 15;
		this.FrmBatch.TabStop = false;
		this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbType.FormattingEnabled = true;
		this.cmbType.Location = new System.Drawing.Point(305, 91);
		this.cmbType.Name = "cmbType";
		this.cmbType.Size = new System.Drawing.Size(120, 30);
		this.cmbType.TabIndex = 27;
		this.TxtType.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtType.AutoSize = true;
		this.TxtType.Location = new System.Drawing.Point(265, 91);
		this.TxtType.Name = "TxtType";
		this.TxtType.Size = new System.Drawing.Size(52, 22);
		this.TxtType.TabIndex = 26;
		this.TxtType.Text = "Type";
		this.cmbjmp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbjmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbjmp.FormattingEnabled = true;
		this.cmbjmp.Items.AddRange(new object[3] { "", "v14", "v17" });
		this.cmbjmp.Location = new System.Drawing.Point(478, 91);
		this.cmbjmp.Name = "cmbjmp";
		this.cmbjmp.Size = new System.Drawing.Size(120, 30);
		this.cmbjmp.TabIndex = 9;
		this.lbljmp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbljmp.AutoSize = true;
		this.lbljmp.Location = new System.Drawing.Point(438, 91);
		this.lbljmp.Name = "lbljmp";
		this.lbljmp.Size = new System.Drawing.Size(48, 22);
		this.lbljmp.TabIndex = 25;
		this.lbljmp.Text = "JMP";
		this.cmbMem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMem.FormattingEnabled = true;
		this.cmbMem.Items.AddRange(new object[4] { "1GB", "2GB", "4GB", "8GB" });
		this.cmbMem.Location = new System.Drawing.Point(478, 117);
		this.cmbMem.Name = "cmbMem";
		this.cmbMem.Size = new System.Drawing.Size(120, 30);
		this.cmbMem.TabIndex = 11;
		this.cmbSite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSite.FormattingEnabled = true;
		this.cmbSite.Location = new System.Drawing.Point(130, 117);
		this.cmbSite.Name = "cmbSite";
		this.cmbSite.Size = new System.Drawing.Size(295, 30);
		this.cmbSite.TabIndex = 10;
		this.LblSite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LblSite.AutoSize = true;
		this.LblSite.Location = new System.Drawing.Point(8, 118);
		this.LblSite.Name = "LblSite";
		this.LblSite.Size = new System.Drawing.Size(136, 22);
		this.LblSite.TabIndex = 21;
		this.LblSite.Text = "ScriptHost Site";
		this.cmbQueue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmbQueue.BackColor = System.Drawing.SystemColors.Window;
		this.cmbQueue.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbQueue.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbQueue.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbQueue.Location = new System.Drawing.Point(130, 91);
		this.cmbQueue.Name = "cmbQueue";
		this.cmbQueue.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbQueue.Size = new System.Drawing.Size(120, 30);
		this.cmbQueue.TabIndex = 8;
		this.TxtDir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDir.BackColor = System.Drawing.SystemColors.Window;
		this.TxtDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtDir.Enabled = false;
		this.TxtDir.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDir.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtDir.Location = new System.Drawing.Point(130, 42);
		this.TxtDir.Name = "TxtDir";
		this.TxtDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDir.Size = new System.Drawing.Size(468, 30);
		this.TxtDir.TabIndex = 6;
		this.TxtSHGUIDir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSHGUIDir.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSHGUIDir.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtSHGUIDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TxtSHGUIDir.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtSHGUIDir.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtSHGUIDir.Location = new System.Drawing.Point(130, 144);
		this.TxtSHGUIDir.Name = "TxtSHGUIDir";
		this.TxtSHGUIDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSHGUIDir.Size = new System.Drawing.Size(468, 30);
		this.TxtSHGUIDir.TabIndex = 12;
		this.lblBatchTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblBatchTitle.AutoSize = true;
		this.lblBatchTitle.BackColor = System.Drawing.SystemColors.Control;
		this.lblBatchTitle.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblBatchTitle.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblBatchTitle.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblBatchTitle.Location = new System.Drawing.Point(8, 18);
		this.lblBatchTitle.Name = "lblBatchTitle";
		this.lblBatchTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblBatchTitle.Size = new System.Drawing.Size(784, 22);
		this.lblBatchTitle.TabIndex = 20;
		this.lblBatchTitle.Text = "To schedule scripts, Pack and SAVE them to a ScriptHost folder, and ADD Job to ScriptHost";
		this.lblLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblLog.AutoSize = true;
		this.lblLog.BackColor = System.Drawing.SystemColors.Control;
		this.lblLog.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblLog.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblLog.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblLog.Location = new System.Drawing.Point(8, 91);
		this.lblLog.Name = "lblLog";
		this.lblLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblLog.Size = new System.Drawing.Size(104, 22);
		this.lblLog.TabIndex = 19;
		this.lblLog.Text = "Job Queue";
		this.lblSHGUI.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblSHGUI.AutoSize = true;
		this.lblSHGUI.BackColor = System.Drawing.SystemColors.Control;
		this.lblSHGUI.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblSHGUI.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSHGUI.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblSHGUI.Location = new System.Drawing.Point(8, 144);
		this.lblSHGUI.Name = "lblSHGUI";
		this.lblSHGUI.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblSHGUI.Size = new System.Drawing.Size(158, 22);
		this.lblSHGUI.TabIndex = 18;
		this.lblSHGUI.Text = "ScriptHost Folder";
		this.lblJob.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblJob.BackColor = System.Drawing.SystemColors.Control;
		this.lblJob.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblJob.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblJob.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblJob.Location = new System.Drawing.Point(8, 66);
		this.lblJob.Name = "lblJob";
		this.lblJob.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblJob.Size = new System.Drawing.Size(65, 17);
		this.lblJob.TabIndex = 17;
		this.lblJob.Text = "Job Name";
		this.lblVADir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblVADir.AutoSize = true;
		this.lblVADir.BackColor = System.Drawing.SystemColors.Control;
		this.lblVADir.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblVADir.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVADir.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblVADir.Location = new System.Drawing.Point(8, 42);
		this.lblVADir.Name = "lblVADir";
		this.lblVADir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblVADir.Size = new System.Drawing.Size(78, 22);
		this.lblVADir.TabIndex = 16;
		this.lblVADir.Text = "Job File";
		this.lblq.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblq.BackColor = System.Drawing.SystemColors.Control;
		this.lblq.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lblq.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblq.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblq.Location = new System.Drawing.Point(3, 59);
		this.lblq.Name = "lblq";
		this.lblq.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblq.Size = new System.Drawing.Size(767, 44);
		this.lblq.TabIndex = 13;
		this.lblq.Text = resources.GetString("lblq.Text");
		this.GridMQ2.AllowDrop = true;
		this.GridMQ2.AllowUserToAddRows = false;
		this.GridMQ2.AllowUserToDeleteRows = false;
		this.GridMQ2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridMQ2.BackgroundColor = System.Drawing.Color.White;
		this.GridMQ2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.GridMQ2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		this.GridMQ2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridMQ2.Columns.AddRange(this.column1, this.Column2);
		this.GridMQ2.ContextMenuStrip = this.ContextMenuGridMQ2;
		this.GridMQ2.Location = new System.Drawing.Point(7, 114);
		this.GridMQ2.Name = "GridMQ2";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridMQ2.RowHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridMQ2.RowHeadersWidth = 62;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.GridMQ2.RowsDefaultCellStyle = dataGridViewCellStyle2;
		this.GridMQ2.RowTemplate.Height = 24;
		this.GridMQ2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.GridMQ2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GridMQ2.ShowCellErrors = false;
		this.GridMQ2.ShowRowErrors = false;
		this.GridMQ2.Size = new System.Drawing.Size(694, 270);
		this.GridMQ2.StandardTab = true;
		this.GridMQ2.TabIndex = 0;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.column1.DefaultCellStyle = dataGridViewCellStyle3;
		this.column1.HeaderText = "Query";
		this.column1.MinimumWidth = 6;
		this.column1.Name = "column1";
		this.column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.column1.Width = 125;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column2.HeaderText = "Description";
		this.Column2.MinimumWidth = 6;
		this.Column2.Name = "Column2";
		this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.Width = 125;
		this.ContextMenuGridMQ2.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuGridMQ2.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.EditQueryScriptToolStripMenuItem, this.LoadQuryScriptToolStripMenuItem, this.mnuInsert });
		this.ContextMenuGridMQ2.Name = "ContextMenuGridMQ2";
		this.ContextMenuGridMQ2.Size = new System.Drawing.Size(340, 112);
		this.EditQueryScriptToolStripMenuItem.Name = "EditQueryScriptToolStripMenuItem";
		this.EditQueryScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.T | System.Windows.Forms.Keys.Control;
		this.EditQueryScriptToolStripMenuItem.Size = new System.Drawing.Size(339, 36);
		this.EditQueryScriptToolStripMenuItem.Text = "Edit Query/Script";
		this.LoadQuryScriptToolStripMenuItem.Name = "LoadQuryScriptToolStripMenuItem";
		this.LoadQuryScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control;
		this.LoadQuryScriptToolStripMenuItem.Size = new System.Drawing.Size(339, 36);
		this.LoadQuryScriptToolStripMenuItem.Text = "Load a Query/Script";
		this.mnuInsert.Name = "mnuInsert";
		this.mnuInsert.ShortcutKeys = System.Windows.Forms.Keys.Insert;
		this.mnuInsert.Size = new System.Drawing.Size(339, 36);
		this.mnuInsert.Text = "Insert a Row";
		base.AutoScaleDimensions = new System.Drawing.SizeF(11f, 22f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(770, 536);
		base.Controls.Add(this.cmdInsert);
		base.Controls.Add(this.OptRun1);
		base.Controls.Add(this.OptRun0);
		base.Controls.Add(this.TbMQ);
		base.Controls.Add(this.cmdCopy);
		base.Controls.Add(this.cmdDelete);
		base.Controls.Add(this.cmdDown);
		base.Controls.Add(this.cmdUp);
		base.Controls.Add(this.FrmBatch);
		base.Controls.Add(this.cmdEdit);
		base.Controls.Add(this.cmdBrowse);
		base.Controls.Add(this.lblq);
		base.Controls.Add(this.MainMenu1);
		base.Controls.Add(this.GridMQ2);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(4, 50);
		base.Name = "FrmMultiQuery";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Process Multiple Queries [Untitled]";
		this.MainMenu1.ResumeLayout(false);
		this.MainMenu1.PerformLayout();
		this.TbMQ.ResumeLayout(false);
		this.TbMQ.PerformLayout();
		this.FrmBatch.ResumeLayout(false);
		this.FrmBatch.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.GridMQ2).EndInit();
		this.ContextMenuGridMQ2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void SetUsePyEnginePMQ(string MyMode)
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
				case 53:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				Globals_Renamed.gUsePyEngineOVR = "Y";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 53;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void PMQ_Use_Python_Engine(string MyMode)
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
				case 173:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
					TbMQ.Items[5].Tag = "1";
					goto IL_0029;
					IL_0029:
					num2 = 3;
					TbMQ.Items[5].BackColor = Color.Orange;
					goto IL_0047;
					IL_0047:
					num2 = 4;
					TbMQ.Items[5].ToolTipText = "Using the Python Extract Engine for all Scripts";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				ComboBox cmbtype = cmbType;
				BuildForm.Set_SH_Type(ref cmbtype, "P");
				cmbType = cmbtype;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 173;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Run_Interactive()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		int num = default(int);
		int num3 = default(int);
		string text2 = default(string);
		short num6 = default(short);
		string @string = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
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
					text = "";
					goto IL_000a;
				case 1007:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_000a;
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
							goto IL_004b;
						case 11:
							goto IL_0054;
						case 12:
							goto IL_0066;
						case 13:
							goto IL_0082;
						case 14:
						case 15:
							goto IL_008e;
						case 16:
							goto IL_00aa;
						case 17:
						case 18:
							goto IL_00b6;
						case 19:
							goto IL_00d2;
						case 20:
						case 21:
							goto IL_00de;
						case 22:
							goto IL_00fa;
						case 23:
						case 24:
							goto IL_010d;
						case 25:
							goto IL_0129;
						case 26:
						case 27:
							goto IL_0135;
						case 28:
							goto IL_0154;
						case 29:
							goto IL_0170;
						case 31:
							goto IL_019f;
						case 34:
							goto IL_01e7;
						case 30:
						case 32:
						case 33:
						case 35:
						case 36:
							goto IL_01f8;
						case 37:
							goto IL_020d;
						case 38:
							goto IL_0229;
						case 39:
							goto IL_023d;
						case 40:
							goto IL_024b;
						case 42:
							goto IL_026e;
						case 41:
						case 43:
						case 44:
							goto IL_0276;
						case 45:
							goto IL_028f;
						case 46:
							goto IL_02a0;
						case 47:
							goto IL_02b6;
						case 48:
						case 49:
							goto IL_02c4;
						case 51:
							goto IL_02e2;
						case 50:
						case 52:
						case 53:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 54:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_02b6:
					num2 = 47;
					text += text2;
					goto IL_02c4;
					IL_02c4:
					num2 = 49;
					num5 = SaveScript(0, "", 0, IsPacked: true, QuietMode: false, "", text);
					break;
					IL_02a0:
					num2 = 46;
					text2 = " > \"" + text2 + "\"";
					goto IL_02b6;
					IL_02e2:
					num2 = 51;
					Interaction.MsgBox("For Batch execution, use the Save/Add Buttons to Update Batch Jobs or to Schedule them on SHGUI", MsgBoxStyle.Information, "Use Save/Add Buttons For Batch Mode");
					break;
					IL_000a:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					@string = "";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					text2 = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					text3 = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					text4 = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					text5 = "";
					goto IL_0041;
					IL_0041:
					num2 = 9;
					text6 = "";
					goto IL_004b;
					IL_004b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0054;
					IL_0054:
					num2 = 11;
					if (OptRun())
					{
						goto IL_0066;
					}
					goto IL_02e2;
					IL_0066:
					num2 = 12;
					if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0)
					{
						goto IL_0082;
					}
					goto IL_008e;
					IL_0082:
					num2 = 13;
					text3 = " /SPFLOGLEVEL=DEBUG";
					goto IL_008e;
					IL_008e:
					num2 = 15;
					if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
					{
						goto IL_00aa;
					}
					goto IL_00b6;
					IL_00aa:
					num2 = 16;
					text4 = " /iREPORTS_VERSION=NEXT";
					goto IL_00b6;
					IL_00b6:
					num2 = 18;
					if (Operators.CompareString(Globals_Renamed.gEncodeFFS, "Y", TextCompare: false) == 0)
					{
						goto IL_00d2;
					}
					goto IL_00de;
					IL_00d2:
					num2 = 19;
					text5 = " /ENCODING_FULLFILE_SCAN=Y";
					goto IL_00de;
					IL_00de:
					num2 = 21;
					if (Operators.CompareString(Globals_Renamed.gEncodeUTFBOM, "Y", TextCompare: false) == 0)
					{
						goto IL_00fa;
					}
					goto IL_010d;
					IL_00fa:
					num2 = 22;
					text5 += " /ENCODING_UTFBOM=Y";
					goto IL_010d;
					IL_010d:
					num2 = 24;
					if (Operators.CompareString(Globals_Renamed.gConvertMAOUber, "Y", TextCompare: false) == 0)
					{
						goto IL_0129;
					}
					goto IL_0135;
					IL_0129:
					num2 = 25;
					text6 = " /USE_UBER_MAO=Y";
					goto IL_0135;
					IL_0135:
					num2 = 27;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_0154;
					}
					goto IL_01e7;
					IL_0154:
					num2 = 28;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0)
					{
						goto IL_0170;
					}
					goto IL_019f;
					IL_0170:
					num2 = 29;
					text = "va .\\RunSPF.exe" + text4 + text3 + text5 + text6;
					goto IL_01f8;
					IL_019f:
					num2 = 31;
					text = "\"" + Globals_Renamed.gMyPyPath + "\" -s -u RunSPF.zip /SPFINSTANCE=" + Globals_Renamed.gSPFCache + text4 + text3 + text5 + text6;
					goto IL_01f8;
					IL_01e7:
					num2 = 34;
					text = "va .\\RunSPF.exe" + text4;
					goto IL_01f8;
					IL_01f8:
					num2 = 36;
					text2 = Strings.Trim(TBTxtLog.Text);
					goto IL_020d;
					IL_020d:
					num2 = 37;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_0229;
					}
					goto IL_02c4;
					IL_0229:
					num2 = 38;
					num6 = checked((short)Strings.InStrRev(text2, "\\"));
					goto IL_023d;
					IL_023d:
					num2 = 39;
					if (num6 != 0)
					{
						goto IL_024b;
					}
					goto IL_026e;
					IL_024b:
					num2 = 40;
					@string = Strings.Trim(Strings.Mid(text2 + " ", checked(num6 + 1)));
					goto IL_0276;
					IL_026e:
					num2 = 42;
					@string = text2;
					goto IL_0276;
					IL_0276:
					num2 = 44;
					if (Strings.InStr(@string, ".") == 0)
					{
						goto IL_028f;
					}
					goto IL_02a0;
					IL_028f:
					num2 = 45;
					text2 += ".log";
					goto IL_02a0;
					end_IL_0001_2:
					break;
				}
				num2 = 53;
				Init_Idx();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1007;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private bool Is_A_Standard_Utility(string MyFile)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		bool flag = default(bool);
		int maxTables = default(int);
		while (true)
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
				case 419:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0023;
						case 6:
							goto IL_0027;
						case 7:
							goto IL_0088;
						case 8:
						case 9:
							goto IL_008e;
						case 10:
							goto IL_009e;
						case 11:
							goto IL_00b0;
						case 12:
							goto IL_00d8;
						case 13:
							goto IL_00f8;
						case 14:
							goto end_IL_0001_2;
						case 16:
						case 17:
						case 18:
							goto IL_012a;
						default:
							goto end_IL_0001;
						case 15:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d8:
					num2 = 12;
					text = Strings.UCase(Strings.Trim(Globals_Renamed.Tables[num5].Tag));
					goto IL_00f8;
					IL_00f8:
					num2 = 13;
					if (Operators.CompareString(text, "", TextCompare: false) != 0 && Strings.InStr(MyFile, text) != 0)
					{
						break;
					}
					goto IL_012a;
					IL_00b0:
					num2 = 11;
					if (Operators.CompareString(Globals_Renamed.Tables[num5].ObjectType, "Utilities", TextCompare: false) == 0)
					{
						goto IL_00d8;
					}
					goto IL_012a;
					IL_012a:
					num2 = 18;
					num5 = checked(num5 + 1);
					goto IL_0133;
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
					MyFile = Strings.UCase(MyFile);
					goto IL_0023;
					IL_0023:
					num2 = 5;
					flag = false;
					goto IL_0027;
					IL_0027:
					num2 = 6;
					if ((Strings.InStr(MyFile, Strings.UCase("ImportExcel.va")) != 0) | (Strings.InStr(MyFile, Strings.UCase("StackResults.va")) != 0) | (Strings.InStr(MyFile, "VA_JOIN.VA") != 0) | (Strings.InStr(MyFile, "WRITE_HEADERS.VA") != 0) | (Strings.InStr(MyFile, "ORDER_COLUMNS_APPEND.VA") != 0))
					{
						goto IL_0088;
					}
					goto IL_008e;
					IL_0088:
					num2 = 7;
					flag = true;
					goto IL_008e;
					IL_008e:
					num2 = 9;
					if (flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_009e;
					IL_009e:
					num2 = 10;
					maxTables = Globals_Renamed.MaxTables;
					num5 = 0;
					goto IL_0133;
					IL_0133:
					if (num5 > maxTables)
					{
						goto end_IL_0001_3;
					}
					goto IL_00b0;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				flag = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 419;
				continue;
			}
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

	private bool FileExists2(string MyFileName)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool result = default(bool);
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
				case 114:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0015;
						case 4:
							goto IL_0024;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					text = Strings.UCase(MyFileName);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					if (!Is_A_Standard_Utility(MyFileName))
					{
						break;
					}
					goto IL_0024;
					IL_0024:
					num2 = 4;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = MyProject.Computer.FileSystem.FileExists(MyFileName);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 114;
				continue;
			}
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

	private void InsertARow()
	{
		int num = 0;
		if (GridMQ2.SelectedRows.Count <= 0)
		{
			return;
		}
		if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(GridMQ2.Rows[599].Cells[0].Value, "", TextCompare: false), Operators.CompareObjectEqual(GridMQ2.Rows[599].Cells[1].Value, "", TextCompare: false))))
		{
			num = GridMQ2.SelectedRows[0].Index;
			GridMQ2.Rows.Insert(num, "", "");
			if (GridMQ2.RowCount > 600)
			{
				GridMQ2.Rows.RemoveAt(600);
			}
			Number_Grid2(checked((short)num));
			GridMQ2.Refresh();
		}
		else
		{
			Interaction.MsgBox("The last row of the grid has data. This prevents an insert", MsgBoxStyle.Exclamation, "Cannot Insert a row");
		}
	}

	public void PrepSaveSHGUI(int Index, string TmpFile, bool IsCustom = false)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string TmpMidas = default(string);
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
						errsource = "FrmMultiQuery - PrepSaveSHGUI";
						TmpMidas = "";
						string text = "";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						if (IsCustom)
						{
							goto IL_0055;
						}
						ComboBox TxtSHGuiDir = TxtSHGUIDir;
						bool num7 = BuildForm.ChkShguiBase(ref TxtSHGuiDir);
						TxtSHGUIDir = TxtSHGuiDir;
						if (num7)
						{
							goto IL_0055;
						}
						goto end_IL_0001;
					}
					case 673:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								break;
							default:
								goto IL_02d7;
							}
							break;
						}
						IL_0055:
						BuildForm.TempSetSHSave("S", ref TmpMidas);
						if (!IsCustom)
						{
							TmpFile = Strings.Trim(TmpFile);
							string text;
							if (Operators.CompareString(TmpFile, "", TextCompare: false) != 0)
							{
								short num5 = (short)Strings.InStrRev(TmpFile, "\\");
								text = ((num5 == 0) ? TxtSHGUIDir.Text : (TxtSHGUIDir.Text + Strings.Mid(TmpFile, 1, num5)));
								TmpFile = TxtSHGUIDir.Text + TmpFile;
							}
							else
							{
								text = TxtSHGUIDir.Text;
								TmpFile = "";
							}
							BuildForm.GetSHGUIFile(TmpFile, text);
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							TmpFile = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							if (Operators.CompareString(Strings.UCase(Strings.Mid(TmpFile, 1, Strings.Len(TxtSHGUIDir.Text))), Strings.UCase(TxtSHGUIDir.Text), TextCompare: false) == 0)
							{
								TxtDir.Text = Strings.Mid(TmpFile, Strings.Len(TxtSHGUIDir.Text) + 1);
								TmpFile = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
								short num5 = (short)Strings.InStrRev(TmpFile, ".");
								if (num5 != 0)
								{
									TmpFile = Strings.Mid(TmpFile, 1, num5 - 1);
								}
								num5 = (short)Strings.InStrRev(TmpFile, "\\");
								if (num5 != 0)
								{
									TmpFile = Strings.Mid(TmpFile, num5 + 1);
								}
								TxtName.Text = Strings.Trim(TmpFile);
								SaveSHGUI(Index);
							}
							else
							{
								Interaction.MsgBox("The Job must be placed in the ScriptHost folder or one of its sub-folders", MsgBoxStyle.Exclamation, "Invalid Path for Batch Job");
							}
						}
						else if (IsCustom)
						{
							short num6 = SaveScript(0, "", 0, IsPacked: true, QuietMode: false, TmpFile);
						}
						break;
					}
					BuildForm.TempSetSHSave("R", ref TmpMidas);
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 673;
				continue;
			}
			break;
			IL_02d7:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void SaveSHGUI(int Index)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myVAName = default(string);
		short num5 = default(short);
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
					num3 = -2;
					goto IL_000b;
				case 472:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_0168;
						default:
							goto end_IL_0001;
						}
						goto IL_00bb;
					}
					IL_0134:
					num2 = 20;
					Interaction.MsgBox("You must specify a Job name for Batch processing.", MsgBoxStyle.Exclamation, "Missing a Batch Job Name");
					goto IL_0149;
					IL_0149:
					num2 = 21;
					TxtName.Focus();
					break;
					IL_0126:
					num2 = 18;
					Init_Idx();
					break;
					IL_0168:
					num4 = num + 1;
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
						goto IL_0043;
					case 5:
						goto IL_004b;
					case 6:
						goto IL_0053;
					case 7:
						goto IL_006e;
					case 8:
						goto IL_0076;
					case 9:
						goto IL_0083;
					case 10:
						goto IL_008c;
					case 11:
						goto IL_009a;
					case 12:
						goto IL_00a8;
					case 15:
						goto IL_00bb;
					case 16:
						goto IL_0115;
					case 13:
					case 14:
					case 18:
						goto IL_0126;
					case 20:
						goto IL_0134;
					case 21:
						goto IL_0149;
					case 17:
					case 19:
					case 22:
					case 23:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 24:
						goto end_IL_0001_3;
					}
					goto default;
					IL_00bb:
					num2 = 15;
					Interaction.MsgBox("Error processing Multi-Query Definition File: (" + Conversions.ToString(Information.Err().Number) + "-" + Conversion.ErrorToString() + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Multi-Query Execution Error");
					goto IL_0115;
					IL_0115:
					num2 = 16;
					Information.Err().Clear();
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(Strings.Trim(TxtName.Text), "", TextCompare: false) != 0)
					{
						goto IL_0034;
					}
					goto IL_0134;
					IL_0034:
					num2 = 3;
					myVAName = TxtName.Text;
					goto IL_0043;
					IL_0043:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_004b;
					IL_004b:
					num2 = 5;
					Globals_Renamed.g_IsSHGUI = true;
					goto IL_0053;
					IL_0053:
					num2 = 6;
					num5 = SaveScript(0, myVAName, checked((short)Index), IsPacked: true);
					goto IL_006e;
					IL_006e:
					num2 = 7;
					Globals_Renamed.g_IsSHGUI = false;
					goto IL_0076;
					IL_0076:
					num2 = 8;
					if (num5 == 1)
					{
						goto IL_0083;
					}
					goto IL_0126;
					IL_0083:
					ProjectData.ClearProjectError();
					num3 = -3;
					goto IL_008c;
					IL_008c:
					num2 = 10;
					FileSystem.ChDrive(Globals_Renamed.MyPCDir);
					goto IL_009a;
					IL_009a:
					num2 = 11;
					FileSystem.ChDir(Globals_Renamed.MyPCDir);
					goto IL_00a8;
					IL_00a8:
					num2 = 12;
					Information.Err().Clear();
					goto IL_0126;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				Globals_Renamed.g_IsSHGUI = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 472;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Set_Col_Width()
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
							goto IL_002c;
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
					if (GridMQ2.Width == FrmBatch.Width)
					{
						break;
					}
					goto IL_002c;
					IL_002c:
					num2 = 3;
					GridMQ2.Width = FrmBatch.Width;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				GridMQ2.Columns[1].Width = checked(GridMQ2.Width - GridMQ2.Columns[0].Width - 80);
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

	public bool OptRun()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
							goto IL_001e;
						case 5:
							goto IL_0025;
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
					if (OptRun0.Checked)
					{
						goto IL_001e;
					}
					goto IL_0025;
					IL_001e:
					num2 = 3;
					result = true;
					goto end_IL_0001_3;
					IL_0025:
					num2 = 5;
					if (!OptRun1.Checked)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = false;
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
		return result;
	}

	public string Chk_PMQ_Util()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string[] array = default(string[]);
		string errsource = default(string);
		string result = default(string);
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
						array = new string[601];
						errsource = "Process_Multiple_Queries - Chk_PMQ_Util";
						int num3 = 0;
						short num4 = 0;
						string text = "";
						result = "";
						num4 = -1;
						num3 = 0;
						do
						{
							text = Strings.UCase(Strings.Trim(Conversions.ToString(GridMQ2.Rows[num3].Cells[0].Value)));
							if (LikeOperator.LikeString(text, "{RUN*LOOP}*", CompareMethod.Binary))
							{
								num4++;
								array[num4] = "{END-LOOP}";
							}
							else if (LikeOperator.LikeString(text, "{START*MACRO}*", CompareMethod.Binary))
							{
								num4++;
								array[num4] = "{END-MACRO}";
							}
							else if (LikeOperator.LikeString(text, "{IF*THEN}*", CompareMethod.Binary))
							{
								num4++;
								array[num4] = "{END-IF}";
							}
							else if (LikeOperator.LikeString(text, "{END*}", CompareMethod.Binary))
							{
								if (num4 < 0)
								{
									result = "An " + text + " utility was unexpectedly found in row. " + Conversions.ToString(num3 + 1) + ". Either there is no associated utility or the associated Utility is not enabled or empty";
								}
								else
								{
									if (Operators.CompareString(array[num4], text, TextCompare: false) != 0)
									{
										result = "SQLPathFinder does not support mixing of utilities. An " + text + " token was found prior to an expected " + array[num4] + " token in line " + Conversions.ToString(num3 + 1);
										break;
									}
									array[num4] = "";
									num4--;
								}
							}
							num3++;
						}
						while (num3 <= 599);
						Array.Clear(array, 0, array.Length);
						goto end_IL_0001;
					}
					case 513:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Array.Clear(array, 0, array.Length);
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 513;
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
					fTitle = "SQLPathFinder.spf";
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
				Text = "Process Multiple Queries - [" + text + "]";
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

	public string Set_Batch_Attr(string MyMode, string MyBatchStr, ref string PackedExe)
	{
		int num = 0;
		string text = "";
		string text2 = "";
		string text3 = Strings.UCase("\\\\SHUser-Intg.intel.com\\SHIntgUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
		string text4 = Strings.UCase("\\\\azsactapp22.intel.com\\scripting\\library\\" + Strings.Trim(Strings.LCase(Globals_Renamed.gWinuser)) + "\\");
		MyBatchStr = BuildForm.SHGUI_Path_Chg(MyBatchStr);
		PackedExe = "";
		string result = "";
		checked
		{
			if (Operators.CompareString(MyMode, "G", TextCompare: false) != 0)
			{
				if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(Strings.UCase(MyBatchStr), 1, Strings.Len("!SPF-BATCH-ATTRIBUTES->")), Strings.UCase("!SPF-BATCH-ATTRIBUTES->"), TextCompare: false) == 0)
				{
					MyBatchStr = Strings.Mid(MyBatchStr, Strings.Len("!SPF-BATCH-ATTRIBUTES->") + 1);
					string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
					num = General_Procedures.ParseAndFillArray(MyBatchStr, "|||", ref DynArray);
					if (num >= 4)
					{
						text = Strings.UCase(Strings.Trim(DynArray[4]));
						text2 = Strings.UCase(Strings.Trim(DynArray[1]));
						if (Operators.CompareString(Strings.Right(Strings.UCase("    " + text2), 4), ".EXE", TextCompare: false) == 0)
						{
							PackedExe = text2;
						}
						if ((Operators.CompareString(Strings.Mid(text, 1, Strings.Len(Globals_Renamed.gBaseSHGUIDir)), Strings.UCase(Globals_Renamed.gBaseSHGUIDir), TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text, 1, Strings.Len(text3)), text3, TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(text2, 1, Strings.Len(text4)), text4, TextCompare: false) == 0))
						{
							f_ShguiDir = Strings.Trim(DynArray[4]);
							string text5 = Strings.Trim(DynArray[3]);
							int num2 = Strings.InStr(text5, ";");
							ComboBox comboBox;
							if (num2 > 0)
							{
								text5 = Strings.Trim(Strings.Mid(text5, 1, num2 - 1));
								int num3 = Strings.InStr(text5, "=");
								if (num3 > 0)
								{
									text5 = Strings.UCase(Strings.Trim(Strings.Mid(text5, num3 + 1)));
									comboBox = cmbQueue;
									int num4 = BuildForm.Find_Combo_Item(ref comboBox, text5, 0);
									cmbQueue = comboBox;
									int selectedIndex = num4;
									cmbQueue.SelectedIndex = selectedIndex;
								}
							}
							TxtDir.Text = "";
							TxtName.Text = "";
							comboBox = TxtSHGUIDir;
							BuildForm.Init_SH_Controls2(ref comboBox, Globals_Renamed.gBaseSHGUIDir);
							TxtSHGUIDir = comboBox;
						}
					}
					DynArray = null;
				}
			}
			else
			{
				string text5 = "JobSize=" + cmbQueue.Text + ";ScriptType=" + cmbType.Text;
				result = "!SPF-BATCH-ATTRIBUTES->" + Strings.Trim(TxtDir.Text) + "|||" + Strings.Trim(TxtName.Text) + "|||" + text5 + "|||" + Strings.Trim(TxtSHGUIDir.Text) + "\r\n";
			}
			return result;
		}
	}

	public void OpenMQ(string FileName)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short num5 = default(short);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string PackedExe = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				string text2;
				short num6;
				short num7;
				string text3;
				DataGridView GridMQ;
				string obj;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					num5 = 0;
					goto IL_0006;
				case 867:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_02a3;
						default:
							goto end_IL_0001;
						}
						goto IL_0221;
					}
					IL_0261:
					num2 = 40;
					Information.Err().Clear();
					goto IL_0272;
					IL_0272:
					num2 = 42;
					GridMQ2.CurrentCell = GridMQ2[0, 0];
					break;
					IL_0221:
					num2 = 39;
					Interaction.MsgBox("Error accessing Multi-Query Definition File. (" + Conversion.ErrorToString() + " : " + text + ").", MsgBoxStyle.Critical, "Error Accessing Multi-Query Definition");
					goto IL_0261;
					IL_02a3:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_0006;
					case 3:
						goto IL_000f;
					case 4:
						goto IL_0014;
					case 5:
						goto IL_0019;
					case 6:
						goto IL_0022;
					case 7:
						goto IL_002b;
					case 8:
						goto IL_0034;
					case 9:
						goto IL_003d;
					case 11:
					case 12:
						goto IL_0056;
					case 13:
						goto IL_0061;
					case 14:
						goto IL_007c;
					case 15:
						goto IL_0084;
					case 16:
						goto IL_00a6;
					case 19:
						goto IL_00da;
					case 20:
						goto IL_00f3;
					case 21:
						goto IL_0110;
					case 18:
					case 23:
					case 24:
					case 25:
					case 26:
						goto IL_012f;
					case 27:
						goto IL_0134;
					case 28:
						goto IL_015e;
					case 29:
						goto IL_0188;
					case 30:
						goto IL_0198;
					case 31:
						goto IL_01ac;
					case 32:
						goto IL_01cc;
					case 33:
						goto IL_01e5;
					case 34:
						goto IL_01f0;
					case 35:
						goto IL_0209;
					case 38:
					case 39:
						goto IL_0221;
					case 40:
						goto IL_0261;
					case 10:
					case 17:
					case 22:
					case 36:
					case 37:
					case 41:
					case 42:
						goto IL_0272;
					case 43:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 44:
						goto end_IL_0001_3;
					}
					goto default;
					IL_0006:
					num2 = 2;
					text2 = "";
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num7 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					text3 = "";
					goto IL_0022;
					IL_0022:
					num2 = 6;
					text = "";
					goto IL_002b;
					IL_002b:
					num2 = 7;
					PackedExe = "";
					goto IL_0034;
					IL_0034:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_003d;
					IL_003d:
					num2 = 9;
					if (Script_Found() != 1)
					{
						goto IL_0056;
					}
					goto IL_0272;
					IL_0056:
					num2 = 12;
					FileName = Strings.Trim(FileName);
					goto IL_0061;
					IL_0061:
					num2 = 13;
					if (Operators.CompareString(FileName, "", TextCompare: false) == 0)
					{
						goto IL_007c;
					}
					goto IL_012f;
					IL_007c:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_0084;
					IL_0084:
					num2 = 15;
					BuildForm.FileOpenSave("O", "", "spf", "Load a SQLPathFinder Multi-Query Definition", Globals_Renamed.gQueryDir);
					goto IL_00a6;
					IL_00a6:
					num2 = 16;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0)
					{
						goto IL_00da;
					}
					goto IL_0272;
					IL_00da:
					num2 = 19;
					FileName = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_00f3;
					IL_00f3:
					num2 = 20;
					if (!LikeOperator.LikeString(Strings.UCase(FileName), "*.SPF", CompareMethod.Binary))
					{
						goto IL_0110;
					}
					goto IL_012f;
					IL_0110:
					num2 = 21;
					Interaction.MsgBox("Only files with extension \".spf\" can be loaded to this form", MsgBoxStyle.Exclamation, "Invalid File Extension");
					goto IL_0272;
					IL_012f:
					num2 = 26;
					num5 = 0;
					goto IL_0134;
					IL_0134:
					num2 = 27;
					GridMQ2.Rows[num5].Cells[0].Value = "";
					goto IL_015e;
					IL_015e:
					num2 = 28;
					GridMQ2.Rows[num5].Cells[1].Value = "";
					goto IL_0188;
					IL_0188:
					num2 = 29;
					num5 = checked((short)unchecked(num5 + 1));
					if (num5 <= 599)
					{
						goto IL_0134;
					}
					goto IL_0198;
					IL_0198:
					num2 = 30;
					TxtName.Text = "";
					goto IL_01ac;
					IL_01ac:
					num2 = 31;
					GridMQ = GridMQ2;
					obj = BuildForm.gLoad_MQ(ref GridMQ, FileName, ref PackedExe);
					GridMQ2 = GridMQ;
					text = obj;
					goto IL_01cc;
					IL_01cc:
					num2 = 32;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_01e5;
					}
					goto IL_0221;
					IL_01e5:
					num2 = 33;
					Set_Query_In_Title(FileName);
					goto IL_01f0;
					IL_01f0:
					num2 = 34;
					if (Operators.CompareString(PackedExe, "", TextCompare: false) != 0)
					{
						goto IL_0209;
					}
					goto IL_0272;
					IL_0209:
					num2 = 35;
					TxtDir.Text = PackedExe;
					goto IL_0272;
					end_IL_0001_2:
					break;
				}
				num2 = 43;
				GridMQ2.FirstDisplayedScrollingRowIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 867;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public string GetFullQName(string MyQ)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 263:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
						case 5:
							goto IL_0025;
						case 6:
							goto IL_002e;
						case 7:
							goto IL_0038;
						case 8:
							goto IL_0056;
						case 9:
							goto IL_005f;
						case 10:
						case 11:
							goto IL_006d;
						case 12:
							goto IL_0085;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 13:
						case 15:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005f:
					num2 = 9;
					MyQ = Strings.Mid(MyQ, 2);
					goto IL_006d;
					IL_006d:
					num2 = 11;
					if (Strings.InStr(MyQ, "\\") != 0)
					{
						break;
					}
					goto IL_0085;
					IL_0056:
					num2 = 8;
					text = "!";
					goto IL_005f;
					IL_0085:
					num2 = 12;
					result = text + MyDefaultDir + Strings.Trim(MyQ);
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					if (Is_A_Standard_Utility(MyQ))
					{
						goto IL_001a;
					}
					goto IL_0025;
					IL_001a:
					num2 = 3;
					result = MyQ;
					goto end_IL_0001_3;
					IL_0025:
					num2 = 5;
					text = "";
					goto IL_002e;
					IL_002e:
					num2 = 6;
					MyQ = Strings.Trim(MyQ);
					goto IL_0038;
					IL_0038:
					num2 = 7;
					if (Operators.CompareString(Strings.Mid(MyQ, 1, 1), "!", TextCompare: false) == 0)
					{
						goto IL_0056;
					}
					goto IL_006d;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				result = text + MyQ;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 263;
				continue;
			}
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

	public void Number_Grid2(short currRow)
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
					DataGridView MyGrid;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 200:
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
								goto IL_0039;
							case 5:
								goto IL_0064;
							case 6:
								goto IL_0073;
							case 7:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 8:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0039:
						num2 = 4;
						MyGrid = GridMQ2;
						BuildForm.SetMQColor(ref MyGrid, GridMQ2.CurrentRow.Index);
						GridMQ2 = MyGrid;
						goto IL_0064;
						IL_0064:
						num2 = 5;
						num5 = (short)unchecked(num5 + 1);
						if (num5 <= 599)
						{
							goto IL_000f;
						}
						goto IL_0073;
						IL_000f:
						num2 = 3;
						GridMQ2.Rows[num5].HeaderCell.Value = (num5 + 1).ToString();
						goto IL_0039;
						IL_0073:
						num2 = 6;
						Application.DoEvents();
						break;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						end_IL_0001_2:
						break;
					}
					num2 = 7;
					GridMQ2.CurrentCell = GridMQ2[0, currRow];
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 200;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void Init_Idx()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short num6 = default(short);
		while (true)
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
				case 349:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_007f;
						case 6:
							goto IL_009a;
						case 7:
							goto IL_00a8;
						case 9:
						case 10:
							goto IL_00b1;
						case 8:
						case 11:
							goto IL_00c4;
						case 12:
							goto IL_00d2;
						case 13:
							goto IL_00ee;
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
					IL_00a8:
					num2 = 7;
					num5 = 1;
					goto IL_00c4;
					IL_00c4:
					num2 = 11;
					if (num5 != 0)
					{
						break;
					}
					goto IL_00d2;
					IL_009a:
					num2 = 6;
					GridMQ2.Focus();
					goto IL_00a8;
					IL_00d2:
					num2 = 12;
					GridMQ2.CurrentCell = GridMQ2[0, 0];
					goto IL_00ee;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num6 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridMQ2.Rows[num6].Cells[0].Value)), "", TextCompare: false) == 0 || Operators.ConditionalCompareObjectEqual(GridMQ2.Rows[num6].Cells[0].Value, null, TextCompare: false))
					{
						goto IL_007f;
					}
					goto IL_00b1;
					IL_00ee:
					num2 = 13;
					GridMQ2.Focus();
					break;
					IL_00b1:
					num2 = 10;
					num6 = checked((short)unchecked(num6 + 1));
					if (num6 <= 599)
					{
						goto IL_0014;
					}
					goto IL_00c4;
					IL_007f:
					num2 = 5;
					GridMQ2.CurrentCell = GridMQ2[0, num6];
					goto IL_009a;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				Application.DoEvents();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 349;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public short Script_Found()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short result = default(short);
		short num6 = default(short);
		string prompt = default(string);
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
					case 338:
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
								goto IL_004c;
							case 6:
								goto IL_0055;
							case 7:
								goto IL_006b;
							case 8:
								goto IL_0078;
							case 9:
								goto IL_009c;
							case 11:
								goto IL_00a5;
							case 14:
								goto IL_00ae;
							case 15:
								goto IL_00bc;
							case 17:
								goto end_IL_0001_2;
							case 20:
							case 21:
								goto IL_00cf;
							default:
								goto end_IL_0001;
							case 10:
							case 12:
							case 13:
							case 16:
							case 18:
							case 19:
							case 22:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00ae:
						num2 = 14;
						if (num5 != 7)
						{
							break;
						}
						goto IL_00bc;
						IL_00bc:
						num2 = 15;
						result = 0;
						goto end_IL_0001_3;
						IL_00a5:
						num2 = 11;
						result = 1;
						goto end_IL_0001_3;
						IL_00cf:
						num2 = 21;
						num6 = (short)unchecked(num6 + 1);
						if (num6 > 599)
						{
							goto end_IL_0001_3;
						}
						goto IL_0014;
						IL_000b:
						num2 = 2;
						result = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num6 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						if (Operators.ConditionalCompareObjectNotEqual(GridMQ2.Rows[num6].Cells[0].Value, "", TextCompare: false))
						{
							goto IL_004c;
						}
						goto IL_00cf;
						IL_004c:
						num2 = 5;
						prompt = "Do you wish to save this Multi-Query File?";
						goto IL_0055;
						IL_0055:
						num2 = 6;
						num5 = (short)Interaction.MsgBox(prompt, MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Save Multi-Query Definition?");
						goto IL_006b;
						IL_006b:
						num2 = 7;
						if (num5 == 6)
						{
							goto IL_0078;
						}
						goto IL_00ae;
						IL_0078:
						num2 = 8;
						if (SaveScript(1, "", 0, IsPacked: true) == 1)
						{
							goto IL_009c;
						}
						goto IL_00a5;
						IL_009c:
						num2 = 9;
						result = 0;
						goto end_IL_0001_3;
						end_IL_0001_2:
						break;
					}
					num2 = 17;
					result = 1;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 338;
				continue;
			}
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public short SaveScript(short MyMode, string MyVAName, short SHGUIMode, bool IsPacked, bool QuietMode = false, string IsCustomF = "", string RunCodeStr = "")
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text2 = default(string);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string text3 = default(string);
		string text4 = default(string);
		short num5 = default(short);
		short num6 = default(short);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		bool flag = default(bool);
		short num7 = default(short);
		short num8 = default(short);
		string MyQueryToProcess = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text11 = default(string);
		string text13 = default(string);
		short num10 = default(short);
		bool ContainsMacro = default(bool);
		string file = default(string);
		short num11 = default(short);
		string text14 = default(string);
		bool flag2 = default(bool);
		int num12 = default(int);
		string text16 = default(string);
		string text17 = default(string);
		short num14 = default(short);
		Globals_Renamed.Exe_Type[] array = default(Globals_Renamed.Exe_Type[]);
		short num15 = default(short);
		string text19 = default(string);
		string text20 = default(string);
		bool flag3 = default(bool);
		string PackedExe = default(string);
		string text21 = default(string);
		string text22 = default(string);
		string MyInLineValue = default(string);
		string text23 = default(string);
		string text24 = default(string);
		short result = default(short);
		short num17 = default(short);
		short num18 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				short num9;
				string text15;
				string text18;
				short num16;
				short num13;
				string text10;
				string text12;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					text2 = "";
					goto IL_000b;
				case 12668:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 4:
						case 5:
						case 6:
							break;
						case 2:
						case 7:
							goto IL_27d7;
						case 3:
							goto IL_2816;
						case 1:
							goto IL_28b4;
						default:
							goto end_IL_0001;
						}
						goto IL_27a6;
					}
					IL_2586:
					num2 = 519;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_25a2;
					}
					goto IL_25b8;
					IL_25a2:
					num2 = 520;
					text += "\\";
					goto IL_25b8;
					IL_278f:
					num2 = 542;
					Set_Query_In_Title(text2);
					break;
					IL_28b4:
					num4 = num + 1;
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
						goto IL_002b;
					case 7:
						goto IL_0030;
					case 8:
						goto IL_0035;
					case 9:
						goto IL_003a;
					case 10:
						goto IL_0040;
					case 11:
						goto IL_0046;
					case 12:
						goto IL_0050;
					case 13:
						goto IL_005a;
					case 14:
						goto IL_0064;
					case 15:
						goto IL_006e;
					case 16:
						goto IL_0078;
					case 17:
						goto IL_0082;
					case 18:
						goto IL_008c;
					case 19:
						goto IL_0096;
					case 20:
						goto IL_00a0;
					case 21:
						goto IL_00aa;
					case 22:
						goto IL_00b0;
					case 23:
						goto IL_00b6;
					case 24:
						goto IL_00c0;
					case 25:
						goto IL_00c6;
					case 26:
						goto IL_00d0;
					case 27:
						goto IL_00d6;
					case 28:
						goto IL_00e0;
					case 29:
						goto IL_00e6;
					case 30:
						goto IL_00ec;
					case 31:
						goto IL_00f6;
					case 32:
						goto IL_0100;
					case 33:
						goto IL_0106;
					case 34:
						goto IL_0115;
					case 35:
						goto IL_011b;
					case 36:
						goto IL_0125;
					case 37:
						goto IL_012f;
					case 38:
						goto IL_0139;
					case 39:
						goto IL_013f;
					case 40:
						goto IL_0149;
					case 41:
						goto IL_0153;
					case 42:
						goto IL_015d;
					case 43:
						goto IL_0163;
					case 44:
						goto IL_016d;
					case 45:
						goto IL_0177;
					case 46:
						goto IL_0186;
					case 47:
						goto IL_01a2;
					case 48:
					case 49:
						goto IL_01ae;
					case 50:
						goto IL_01df;
					case 51:
					case 52:
						goto IL_01eb;
					case 53:
						goto IL_01f3;
					case 54:
						goto IL_01f8;
					case 55:
						goto IL_01fe;
					case 56:
						goto IL_0218;
					case 57:
						goto IL_0230;
					case 59:
						goto IL_023e;
					case 58:
					case 60:
					case 61:
						goto IL_024a;
					case 62:
						goto IL_0269;
					case 63:
						goto IL_027f;
					case 64:
						goto IL_028f;
					case 65:
						goto IL_029a;
					case 66:
						goto IL_02b3;
					case 68:
					case 69:
					case 70:
						goto IL_02c1;
					case 71:
						goto IL_02cb;
					case 72:
						goto IL_02d1;
					case 73:
						goto IL_0306;
					case 74:
						goto IL_0337;
					case 75:
						goto IL_0344;
					case 76:
					case 77:
						goto IL_0353;
					case 78:
						goto IL_038e;
					case 79:
						goto IL_03d1;
					case 80:
						goto IL_03e4;
					case 81:
						goto IL_03f2;
					case 83:
						goto IL_03fd;
					case 82:
					case 84:
					case 85:
						goto IL_040f;
					case 86:
						goto IL_0428;
					case 87:
						goto IL_0436;
					case 88:
						goto IL_0451;
					case 90:
					case 91:
						goto IL_045d;
					case 92:
						goto IL_046d;
					case 93:
						goto IL_0490;
					case 94:
						goto IL_049a;
					case 95:
						goto IL_04b2;
					case 96:
						goto IL_04c0;
					case 97:
						goto IL_04dc;
					case 99:
						goto IL_04f6;
					case 100:
						goto IL_050c;
					case 98:
					case 101:
					case 102:
						goto IL_0523;
					case 103:
						goto IL_0547;
					case 104:
						goto IL_0578;
					case 105:
						goto IL_05a9;
					case 106:
						goto IL_05c8;
					case 107:
						goto IL_05ed;
					case 108:
						goto IL_05f7;
					case 110:
						goto IL_060d;
					case 111:
						goto IL_0638;
					case 112:
						goto IL_0642;
					case 109:
					case 113:
					case 114:
						goto IL_064d;
					case 115:
						goto IL_065b;
					case 116:
						goto IL_0676;
					case 118:
					case 119:
						goto IL_0682;
					case 120:
						goto IL_069e;
					case 121:
						goto IL_06b7;
					case 123:
						goto IL_06da;
					case 128:
						goto IL_071e;
					case 129:
						goto IL_073a;
					case 131:
						goto IL_074f;
					case 135:
						goto IL_0770;
					case 136:
						goto IL_0780;
					case 137:
						goto IL_07a3;
					case 139:
						goto IL_07bd;
					case 138:
					case 140:
					case 141:
						goto IL_07ce;
					case 142:
						goto IL_07f2;
					case 143:
						goto IL_07fb;
					case 144:
						goto IL_081c;
					case 145:
						goto IL_0825;
					case 147:
						goto IL_0838;
					case 148:
						goto IL_0898;
					case 149:
						goto IL_08a1;
					case 151:
						goto IL_08b2;
					case 152:
						goto IL_08bb;
					case 146:
					case 150:
					case 153:
					case 154:
						goto IL_08c9;
					case 155:
						goto IL_08d6;
					case 156:
						goto IL_08df;
					case 157:
						goto IL_08fb;
					case 158:
						goto IL_0912;
					case 159:
						goto IL_0926;
					case 160:
						goto IL_093b;
					case 162:
						goto IL_0980;
					case 165:
						goto IL_09b2;
					case 166:
						goto IL_09c7;
					case 168:
						goto IL_09ed;
					case 161:
					case 163:
					case 164:
					case 167:
					case 169:
					case 170:
					case 171:
						goto IL_0a1c;
					case 172:
						goto IL_0a25;
					case 173:
						goto IL_0a2e;
					case 174:
						goto IL_0a3f;
					case 176:
						goto IL_0a63;
					case 175:
					case 177:
					case 178:
						goto IL_0a84;
					case 179:
						goto IL_0a95;
					case 180:
						goto IL_0adc;
					case 183:
						goto IL_0aef;
					case 184:
						goto IL_0b09;
					case 185:
						goto IL_0b27;
					case 188:
						goto IL_0b3a;
					case 189:
						goto IL_0b4e;
					case 190:
						goto IL_0b57;
					case 191:
						goto IL_0b64;
					case 192:
						goto IL_0b7d;
					case 193:
						goto IL_0b93;
					case 194:
						goto IL_0ba9;
					case 196:
						goto IL_0bd1;
					case 197:
						goto IL_0be5;
					case 198:
						goto IL_0bfa;
					case 199:
					case 200:
						goto IL_0c09;
					case 201:
						goto IL_0c2a;
					case 202:
						goto IL_0c37;
					case 203:
						goto IL_0c50;
					case 204:
						goto IL_0c66;
					case 205:
						goto IL_0c7f;
					case 209:
						goto IL_0caa;
					case 210:
						goto IL_0cbf;
					case 212:
						goto IL_0cd7;
					case 213:
						goto IL_0ce1;
					case 214:
						goto IL_0cef;
					case 215:
					case 216:
						goto IL_0d0d;
					case 217:
						goto IL_0d1a;
					case 218:
						goto IL_0d33;
					case 219:
						goto IL_0d49;
					case 220:
						goto IL_0d62;
					case 182:
					case 187:
					case 195:
					case 206:
					case 207:
					case 208:
					case 211:
					case 221:
					case 222:
						goto IL_0d85;
					case 223:
						goto IL_0d92;
					case 224:
						goto IL_0db6;
					case 225:
						goto IL_0dc3;
					case 226:
						goto IL_0e08;
					case 227:
						goto IL_0e24;
					case 228:
						goto IL_0e2c;
					case 232:
						goto IL_0e5f;
					case 233:
						goto IL_0e81;
					case 234:
						goto IL_0e91;
					case 235:
						goto IL_0e9e;
					case 237:
						goto IL_0eaf;
					case 238:
						goto IL_0eb9;
					case 241:
						goto IL_0ece;
					case 242:
						goto IL_0ef0;
					case 243:
						goto IL_0f0d;
					case 245:
						goto IL_0f3f;
					case 246:
						goto IL_0f59;
					case 248:
						goto IL_0fba;
					case 249:
						goto IL_0fce;
					case 250:
						goto IL_0ff6;
					case 252:
						goto IL_1026;
					case 255:
						goto IL_104d;
					case 259:
						goto IL_1060;
					case 260:
						goto IL_107d;
					case 262:
						goto IL_109b;
					case 263:
						goto IL_10b5;
					case 265:
						goto IL_10fd;
					case 236:
					case 239:
					case 240:
					case 244:
					case 247:
					case 251:
					case 253:
					case 254:
					case 256:
					case 257:
					case 258:
					case 261:
					case 264:
					case 266:
					case 267:
					case 268:
					case 269:
						goto IL_110a;
					case 270:
						goto IL_1129;
					case 271:
						goto IL_1142;
					case 272:
					case 273:
						goto IL_115d;
					case 274:
						goto IL_1179;
					case 276:
						goto IL_118e;
					case 280:
						goto IL_11ab;
					case 281:
						goto IL_11bc;
					case 282:
						goto IL_11c9;
					case 283:
						goto IL_1210;
					case 122:
					case 124:
					case 125:
					case 126:
					case 127:
					case 130:
					case 132:
					case 133:
					case 134:
					case 230:
					case 231:
					case 275:
					case 277:
					case 278:
					case 279:
					case 285:
					case 287:
					case 288:
					case 289:
						goto IL_1228;
					case 290:
						goto IL_1238;
					case 291:
						goto IL_1258;
					case 292:
						goto IL_1269;
					case 293:
						goto IL_127b;
					case 294:
					case 295:
					case 296:
					case 297:
					case 298:
						goto IL_128f;
					case 299:
						goto IL_12a8;
					case 300:
						goto IL_12d0;
					case 301:
						goto IL_12e8;
					case 302:
						goto IL_12f0;
					case 303:
						goto IL_1307;
					case 304:
						goto IL_132d;
					case 306:
						goto IL_1360;
					case 308:
						goto IL_1380;
					case 307:
					case 309:
					case 310:
						goto IL_138f;
					case 312:
						goto IL_13b7;
					case 313:
						goto IL_13f5;
					case 314:
						goto IL_1417;
					case 315:
						goto IL_1429;
					case 316:
						goto IL_1437;
					case 318:
						goto IL_1458;
					case 319:
						goto IL_1481;
					case 317:
					case 320:
					case 321:
						goto IL_149c;
					case 322:
						goto IL_14b5;
					case 323:
						goto IL_14c6;
					case 324:
						goto IL_14ce;
					case 325:
						goto IL_14dc;
					case 326:
						goto IL_14f0;
					case 327:
						goto IL_1509;
					case 328:
					case 329:
						goto IL_1522;
					case 330:
						goto IL_1534;
					case 332:
						goto IL_1549;
					case 331:
					case 333:
					case 334:
						goto IL_155c;
					case 335:
						goto IL_1578;
					case 336:
						goto IL_1591;
					case 337:
						goto IL_15a0;
					case 338:
						goto IL_15b9;
					case 339:
						goto IL_15c6;
					case 340:
						goto IL_15df;
					case 341:
						goto IL_15ec;
					case 342:
						goto IL_1605;
					case 343:
						goto IL_1612;
					case 344:
						goto IL_1634;
					case 345:
						goto IL_1646;
					case 346:
						goto IL_1654;
					case 348:
						goto IL_1675;
					case 349:
						goto IL_169e;
					case 347:
					case 350:
					case 351:
						goto IL_16b9;
					case 352:
					case 353:
						goto IL_16f0;
					case 354:
						goto IL_1712;
					case 355:
						goto IL_1724;
					case 356:
						goto IL_1738;
					case 357:
						goto IL_1760;
					case 359:
						goto IL_17b6;
					case 360:
						goto IL_17de;
					case 358:
					case 361:
					case 362:
						goto IL_1832;
					case 363:
					case 364:
						goto IL_184a;
					case 365:
						goto IL_1863;
					case 366:
						goto IL_1882;
					case 367:
						goto IL_189b;
					case 368:
					case 369:
						goto IL_18b6;
					case 370:
						goto IL_18c3;
					case 371:
						goto IL_18e5;
					case 372:
						goto IL_18f7;
					case 373:
						goto IL_190b;
					case 374:
						goto IL_1973;
					case 375:
						goto IL_19b5;
					case 377:
						goto IL_19db;
					case 376:
					case 378:
					case 379:
						goto IL_1a22;
					case 380:
					case 381:
						goto IL_1a3a;
					case 382:
						goto IL_1a53;
					case 383:
						goto IL_1a60;
					case 384:
						goto IL_1a7f;
					case 385:
					case 386:
						goto IL_1ad0;
					case 387:
						goto IL_1ae4;
					case 388:
						goto IL_1aff;
					case 389:
						goto IL_1b13;
					case 390:
						goto IL_1b3a;
					case 391:
						goto IL_1b87;
					case 393:
						goto IL_1b9e;
					case 394:
						goto IL_1bba;
					case 395:
						goto IL_1bee;
					case 392:
					case 396:
					case 397:
						goto IL_1c03;
					case 398:
						goto IL_1c1a;
					case 399:
						goto IL_1c28;
					case 400:
						goto IL_1c35;
					case 402:
					case 403:
						goto IL_1c4e;
					case 404:
						goto IL_1c61;
					case 405:
						goto IL_1c73;
					case 406:
						goto IL_1c8f;
					case 407:
						goto IL_1c98;
					case 408:
						goto IL_1cb4;
					case 409:
					case 410:
						goto IL_1ccc;
					case 411:
						goto IL_1ce1;
					case 413:
						goto IL_1cf7;
					case 415:
						goto IL_1d08;
					case 416:
						goto IL_1d1c;
					case 419:
						goto IL_1d37;
					case 420:
						goto IL_1d75;
					case 421:
						goto IL_1d87;
					case 422:
						goto IL_1d95;
					case 424:
						goto IL_1db6;
					case 425:
						goto IL_1ddf;
					case 423:
					case 426:
					case 427:
						goto IL_1dfa;
					case 428:
						goto IL_1e13;
					case 429:
						goto IL_1e24;
					case 430:
						goto IL_1e2c;
					case 431:
						goto IL_1e3a;
					case 432:
						goto IL_1e4e;
					case 433:
						goto IL_1e67;
					case 434:
						goto IL_1e7e;
					case 435:
					case 436:
						goto IL_1e89;
					case 437:
						goto IL_1ea2;
					case 438:
						goto IL_1eaf;
					case 439:
						goto IL_1efe;
					case 440:
						goto IL_1f5a;
					case 441:
						goto IL_1f75;
					case 442:
						goto IL_1f90;
					case 443:
						goto IL_1fb7;
					case 444:
						goto IL_2004;
					case 446:
						goto IL_201b;
					case 447:
						goto IL_2037;
					case 448:
						goto IL_206b;
					case 445:
					case 449:
					case 450:
						goto IL_2080;
					case 451:
						goto IL_208e;
					case 453:
					case 454:
						goto IL_20a2;
					case 455:
						goto IL_20b2;
					case 456:
						goto IL_20ce;
					case 457:
					case 458:
						goto IL_20e6;
					case 459:
						goto IL_20fb;
					case 461:
						goto IL_2111;
					case 463:
						goto IL_2122;
					case 464:
						goto IL_2136;
					case 467:
						goto IL_214f;
					case 468:
						goto IL_215c;
					case 469:
						goto IL_216e;
					case 471:
						goto IL_2180;
					case 311:
					case 414:
					case 417:
					case 418:
					case 462:
					case 465:
					case 466:
					case 470:
					case 472:
					case 473:
					case 474:
					case 475:
						goto IL_21a7;
					case 476:
						goto IL_21c0;
					case 477:
						goto IL_21d1;
					case 478:
						goto IL_21d9;
					case 479:
						goto IL_21e7;
					case 480:
						goto IL_21fb;
					case 481:
						goto IL_2214;
					case 482:
					case 483:
						goto IL_222d;
					case 484:
						goto IL_224a;
					case 485:
						goto IL_2253;
					case 486:
						goto IL_226b;
					case 487:
						goto IL_2298;
					case 488:
						goto IL_22dc;
					case 489:
						goto IL_22f6;
					case 490:
					case 491:
						goto IL_2317;
					case 492:
						goto IL_232f;
					case 493:
						goto IL_2348;
					case 495:
						goto IL_2358;
					case 496:
						goto IL_2371;
					case 498:
						goto IL_2381;
					case 499:
						goto IL_239a;
					case 501:
						goto IL_23aa;
					case 502:
						goto IL_23c3;
					case 494:
					case 497:
					case 500:
					case 503:
					case 504:
						goto IL_23d1;
					case 505:
						goto IL_23e9;
					case 506:
						goto IL_23f6;
					case 507:
						goto IL_240d;
					case 508:
						goto IL_241e;
					case 509:
						goto IL_2430;
					case 510:
					case 511:
						goto IL_2445;
					case 512:
						goto IL_245c;
					case 514:
						goto IL_246d;
					case 513:
					case 515:
					case 516:
						goto IL_247b;
					case 517:
						goto IL_24b2;
					case 519:
						goto IL_2586;
					case 520:
						goto IL_25a2;
					case 521:
					case 522:
						goto IL_25b8;
					case 518:
					case 523:
					case 524:
						goto IL_2698;
					case 525:
						goto IL_26a4;
					case 528:
						goto IL_26c2;
					case 529:
						goto IL_26cf;
					case 530:
						goto IL_26d7;
					case 531:
						goto IL_26ec;
					case 533:
						goto IL_26fd;
					case 532:
					case 534:
					case 535:
						goto IL_270b;
					case 536:
						goto IL_272a;
					case 537:
						goto IL_2738;
					case 538:
						goto IL_274f;
					case 526:
					case 527:
					case 539:
					case 540:
					case 541:
						goto IL_2769;
					case 542:
						goto IL_278f;
					case 545:
						goto IL_27a6;
					case 546:
						goto IL_27b7;
					case 547:
						goto IL_27bf;
					case 67:
					case 89:
					case 117:
					case 186:
					case 229:
					case 286:
					case 548:
						goto IL_27d7;
					case 549:
						goto IL_27ff;
					case 305:
					case 551:
						goto IL_2816;
					case 553:
						num2 = 553;
						Interaction.MsgBox("Error copying file (" + text3 + ") to ScriptHost location (" + text4 + "), due to: (" + Conversion.ErrorToString() + "). Check that a valid ScriptHost Directory is set." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Error Coping to SH Folder");
						goto case 554;
					case 554:
						num2 = 554;
						Information.Err().Clear();
						goto end_IL_0001_2;
					case 181:
					case 284:
					case 401:
					case 412:
					case 452:
					case 460:
					case 543:
					case 544:
					case 550:
					case 552:
					case 555:
					case 556:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 557:
					case 558:
						goto end_IL_0001_3;
					}
					goto default;
					IL_27a6:
					num2 = 545;
					Information.Err().Clear();
					goto IL_27b7;
					IL_27b7:
					ProjectData.ClearProjectError();
					num3 = 7;
					goto IL_27bf;
					IL_27bf:
					num2 = 547;
					FileSystem.FileClose(num5);
					goto IL_27d7;
					IL_000b:
					num2 = 2;
					num6 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text5 = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					text6 = "";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					text7 = "";
					goto IL_002b;
					IL_002b:
					num2 = 6;
					num5 = 0;
					goto IL_0030;
					IL_0030:
					num2 = 7;
					flag = false;
					goto IL_0035;
					IL_0035:
					num2 = 8;
					num7 = 0;
					goto IL_003a;
					IL_003a:
					num2 = 9;
					num8 = 0;
					goto IL_0040;
					IL_0040:
					num2 = 10;
					num9 = 0;
					goto IL_0046;
					IL_0046:
					num2 = 11;
					MyQueryToProcess = "";
					goto IL_0050;
					IL_0050:
					num2 = 12;
					text8 = "";
					goto IL_005a;
					IL_005a:
					num2 = 13;
					text = "";
					goto IL_0064;
					IL_0064:
					num2 = 14;
					text9 = "";
					goto IL_006e;
					IL_006e:
					num2 = 15;
					text10 = "";
					goto IL_0078;
					IL_0078:
					num2 = 16;
					text11 = "";
					goto IL_0082;
					IL_0082:
					num2 = 17;
					text3 = "";
					goto IL_008c;
					IL_008c:
					num2 = 18;
					text4 = "";
					goto IL_0096;
					IL_0096:
					num2 = 19;
					text12 = "";
					goto IL_00a0;
					IL_00a0:
					num2 = 20;
					text13 = "";
					goto IL_00aa;
					IL_00aa:
					num2 = 21;
					num10 = 0;
					goto IL_00b0;
					IL_00b0:
					num2 = 22;
					ContainsMacro = false;
					goto IL_00b6;
					IL_00b6:
					num2 = 23;
					file = "";
					goto IL_00c0;
					IL_00c0:
					num2 = 24;
					num11 = 0;
					goto IL_00c6;
					IL_00c6:
					num2 = 25;
					text14 = "";
					goto IL_00d0;
					IL_00d0:
					num2 = 26;
					flag2 = false;
					goto IL_00d6;
					IL_00d6:
					num2 = 27;
					text15 = "";
					goto IL_00e0;
					IL_00e0:
					num2 = 28;
					num12 = 0;
					goto IL_00e6;
					IL_00e6:
					num2 = 29;
					num13 = 0;
					goto IL_00ec;
					IL_00ec:
					num2 = 30;
					text16 = "";
					goto IL_00f6;
					IL_00f6:
					num2 = 31;
					text17 = "";
					goto IL_0100;
					IL_0100:
					num2 = 32;
					num14 = 0;
					goto IL_0106;
					IL_0106:
					num2 = 33;
					array = new Globals_Renamed.Exe_Type[601];
					goto IL_0115;
					IL_0115:
					num2 = 34;
					num15 = -1;
					goto IL_011b;
					IL_011b:
					num2 = 35;
					text18 = "";
					goto IL_0125;
					IL_0125:
					num2 = 36;
					text19 = "";
					goto IL_012f;
					IL_012f:
					num2 = 37;
					text20 = "N";
					goto IL_0139;
					IL_0139:
					num2 = 38;
					flag3 = false;
					goto IL_013f;
					IL_013f:
					num2 = 39;
					PackedExe = "";
					goto IL_0149;
					IL_0149:
					num2 = 40;
					text21 = "";
					goto IL_0153;
					IL_0153:
					num2 = 41;
					text22 = "N";
					goto IL_015d;
					IL_015d:
					num2 = 42;
					num16 = -1;
					goto IL_0163;
					IL_0163:
					num2 = 43;
					MyInLineValue = "";
					goto IL_016d;
					IL_016d:
					num2 = 44;
					text23 = "";
					goto IL_0177;
					IL_0177:
					num2 = 45;
					text24 = General_Procedures.Get_UI("errhelp0");
					goto IL_0186;
					IL_0186:
					num2 = 46;
					if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
					{
						goto IL_01a2;
					}
					goto IL_01ae;
					IL_01a2:
					num2 = 47;
					text21 = " /iReports_Version=NEXT";
					goto IL_01ae;
					IL_01ae:
					num2 = 49;
					if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0 && Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_01df;
					}
					goto IL_01eb;
					IL_25b8:
					num2 = 522;
					text5 = text16 + "\"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder_batch.py\" \"" + text8 + "\" \"N" + text20 + Globals_Renamed.gWebNext + text22 + Globals_Renamed.gEncodeFFS + Globals_Renamed.gEncodeUTFBOM + Globals_Renamed.gConvertMAOUber + "\" \"" + Strings.Trim(TxtName.Text) + "\" \"" + text + "\" \"" + text13 + "\" \"\" \"" + text19 + "\"";
					goto IL_2698;
					IL_01df:
					num2 = 50;
					text22 = Globals_Renamed.gPyDebug;
					goto IL_01eb;
					IL_01eb:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_01f3;
					IL_01f3:
					num2 = 53;
					result = 1;
					goto IL_01f8;
					IL_01f8:
					num2 = 54;
					num10 = 0;
					goto IL_01fe;
					IL_01fe:
					num2 = 55;
					text8 = Strings.Trim(Strings.UCase(cmbType.Text));
					goto IL_0218;
					IL_0218:
					num2 = 56;
					if (Operators.CompareString(MyVAName, "", TextCompare: false) == 0)
					{
						goto IL_0230;
					}
					goto IL_023e;
					IL_0230:
					num2 = 57;
					text11 = "__SQLPathFinder_";
					goto IL_024a;
					IL_023e:
					num2 = 59;
					text11 = Strings.Trim(MyVAName);
					goto IL_024a;
					IL_024a:
					num2 = 61;
					if (IsPacked && Operators.CompareString(MyVAName, "", TextCompare: false) != 0)
					{
						goto IL_0269;
					}
					goto IL_027f;
					IL_24b2:
					num2 = 517;
					text5 = text16 + "va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder_batch.va\" \"" + text8 + "\" \"N" + text20 + Globals_Renamed.gWebNext + text22 + Globals_Renamed.gEncodeFFS + Globals_Renamed.gEncodeUTFBOM + Globals_Renamed.gConvertMAOUber + "\" \"" + Strings.Trim(TxtName.Text) + "\" \"" + text + "\" \"" + text13 + "\" \"\" \"" + text19 + "\"";
					goto IL_2698;
					IL_0269:
					num2 = 62;
					text11 = Globals_Renamed.gSPFCache + "_" + text11;
					goto IL_027f;
					IL_027f:
					num2 = 63;
					if (IsPacked && MyMode == 0)
					{
						goto IL_028f;
					}
					goto IL_02c1;
					IL_028f:
					num2 = 64;
					text24 = Chk_PMQ_Util();
					goto IL_029a;
					IL_029a:
					num2 = 65;
					if (Operators.CompareString(text24, "", TextCompare: false) != 0)
					{
						goto IL_02b3;
					}
					goto IL_02c1;
					IL_02b3:
					num2 = 66;
					result = 0;
					goto IL_27d7;
					IL_02c1:
					num2 = 70;
					text5 = "";
					goto IL_02cb;
					IL_02cb:
					num2 = 71;
					num6 = 0;
					goto IL_02d1;
					IL_02d1:
					num2 = 72;
					if (Operators.ConditionalCompareObjectNotEqual(GridMQ2.Rows[num6].Cells[0].Value, null, TextCompare: false))
					{
						goto IL_0306;
					}
					goto IL_128f;
					IL_0306:
					num2 = 73;
					text6 = Strings.Trim(Conversions.ToString(GridMQ2.Rows[num6].Cells[0].Value));
					goto IL_0337;
					IL_0337:
					num2 = 74;
					if (MyMode == 0)
					{
						goto IL_0344;
					}
					goto IL_0353;
					IL_0344:
					num2 = 75;
					text6 = BuildForm.Replace_Globals(text6, 1);
					goto IL_0353;
					IL_0353:
					num2 = 77;
					text7 = "<<" + Strings.Trim(Conversions.ToString(GridMQ2.Rows[num6].Cells[1].Value));
					goto IL_038e;
					IL_038e:
					num2 = 78;
					if ((Operators.CompareString(text6, "", TextCompare: false) != 0) & ((MyMode == 1 || MyMode == 2) | ((MyMode == 0) & (Operators.CompareString(Strings.Mid(text6, 1, 1), "!", TextCompare: false) != 0))))
					{
						goto IL_03d1;
					}
					goto IL_128f;
					IL_03d1:
					num2 = 79;
					num8 = checked((short)Strings.InStr(text6, " \""));
					goto IL_03e4;
					IL_03e4:
					num2 = 80;
					if (num8 == 0)
					{
						goto IL_03f2;
					}
					goto IL_03fd;
					IL_03f2:
					num2 = 81;
					MyQueryToProcess = text6;
					goto IL_040f;
					IL_03fd:
					num2 = 83;
					MyQueryToProcess = Strings.Mid(text6, 1, checked(num8 - 1));
					goto IL_040f;
					IL_040f:
					num2 = 85;
					if (LikeOperator.LikeString(MyQueryToProcess, "*{*}", CompareMethod.Binary))
					{
						goto IL_0428;
					}
					goto IL_0770;
					IL_0428:
					num2 = 86;
					if (!IsPacked)
					{
						goto IL_0436;
					}
					goto IL_045d;
					IL_0436:
					num2 = 87;
					text24 = "Logic utilities can only be used when you Pack Scripts. Error in Line #" + Strings.Trim(Conversions.ToString((int)num6));
					goto IL_0451;
					IL_0451:
					num2 = 88;
					result = 0;
					goto IL_27d7;
					IL_045d:
					num2 = 91;
					if (MyMode == 0)
					{
						goto IL_046d;
					}
					goto IL_071e;
					IL_046d:
					num2 = 92;
					if (Operators.CompareString(Strings.Mid(MyQueryToProcess, 1, 1), "!", TextCompare: false) != 0)
					{
						goto IL_0490;
					}
					goto IL_1228;
					IL_0490:
					num2 = 93;
					num15 = checked((short)(num15 + 1));
					goto IL_049a;
					IL_049a:
					num2 = 94;
					array[num15].Type = Strings.UCase(MyQueryToProcess);
					goto IL_04b2;
					IL_04b2:
					num2 = 95;
					if (num8 != 0)
					{
						goto IL_04c0;
					}
					goto IL_04f6;
					IL_04c0:
					num2 = 96;
					array[num15].File = Strings.Mid(text6, checked(num8 + 1));
					goto IL_04dc;
					IL_04dc:
					num2 = 97;
					array[num15].File2 = "";
					goto IL_0523;
					IL_04f6:
					num2 = 99;
					array[num15].File = "";
					goto IL_050c;
					IL_050c:
					num2 = 100;
					array[num15].File2 = "";
					goto IL_0523;
					IL_0523:
					num2 = 102;
					array[num15].File = General_Procedures.Remove_Spaces_Bt_Quotes(array[num15].File);
					goto IL_0547;
					IL_0547:
					num2 = 103;
					array[num15].File = Strings.Replace(array[num15].File, "\" \"", ";", 1, -1, CompareMethod.Text);
					goto IL_0578;
					IL_0578:
					num2 = 104;
					array[num15].File = Strings.Replace(array[num15].File, "\"", "", 1, -1, CompareMethod.Text);
					goto IL_05a9;
					IL_05a9:
					num2 = 105;
					array[num15].ID = Strings.Trim(Conversions.ToString(checked(num6 + 1)));
					goto IL_05c8;
					IL_05c8:
					num2 = 106;
					if (Operators.CompareString(array[num15].Type, "{START-MACRO}", TextCompare: false) == 0)
					{
						goto IL_05ed;
					}
					goto IL_060d;
					IL_05ed:
					num2 = 107;
					num10 = checked((short)(num10 + 1));
					goto IL_05f7;
					IL_05f7:
					num2 = 108;
					file = array[num15].File;
					goto IL_064d;
					IL_060d:
					num2 = 110;
					if (Operators.CompareString(array[num15].Type, "{END-MACRO}", TextCompare: false) == 0 && num10 > 0)
					{
						goto IL_0638;
					}
					goto IL_064d;
					IL_0638:
					num2 = 111;
					num10 = checked((short)(num10 - 1));
					goto IL_0642;
					IL_0642:
					num2 = 112;
					file = "";
					goto IL_064d;
					IL_064d:
					num2 = 114;
					if (num10 > 1)
					{
						goto IL_065b;
					}
					goto IL_0682;
					IL_065b:
					num2 = 115;
					text24 = "SQLPathFinder does not support nested macros in the Process Multiple Queries form. Error in Line #" + Strings.Trim(Conversions.ToString((int)num6));
					goto IL_0676;
					IL_0676:
					num2 = 116;
					result = 0;
					goto IL_27d7;
					IL_0682:
					num2 = 119;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_069e;
					}
					goto IL_1228;
					IL_069e:
					num2 = 120;
					if (Operators.CompareString(text5, "", TextCompare: false) == 0)
					{
						goto IL_06b7;
					}
					goto IL_06da;
					IL_06b7:
					num2 = 121;
					text5 = array[num15].ID + "," + text6;
					goto IL_1228;
					IL_06da:
					num2 = 123;
					text5 = text5 + Globals_Renamed.CRLF + array[num15].ID + "," + text6;
					goto IL_1228;
					IL_071e:
					num2 = 128;
					if (Operators.CompareString(text5, "", TextCompare: false) == 0)
					{
						goto IL_073a;
					}
					goto IL_074f;
					IL_073a:
					num2 = 129;
					text5 = text6 + text7;
					goto IL_1228;
					IL_074f:
					num2 = 131;
					text5 = text5 + Globals_Renamed.CRLF + text6 + text7;
					goto IL_1228;
					IL_0770:
					num2 = 135;
					MyQueryToProcess = GetFullQName(MyQueryToProcess);
					goto IL_0780;
					IL_0780:
					num2 = 136;
					if (Operators.CompareString(Strings.Mid(text6, 1, 1), "!", TextCompare: false) == 0)
					{
						goto IL_07a3;
					}
					goto IL_07bd;
					IL_07a3:
					num2 = 137;
					flag = FileExists2(Strings.Mid(MyQueryToProcess, 2));
					goto IL_07ce;
					IL_07bd:
					num2 = 139;
					flag = FileExists2(MyQueryToProcess);
					goto IL_07ce;
					IL_07ce:
					num2 = 141;
					if ((MyMode == 0 && flag) || MyMode == 1 || MyMode == 2)
					{
						goto IL_07f2;
					}
					goto IL_11ab;
					IL_26a4:
					num2 = 525;
					num13 = BuildSQL.RunQ(text5, 1);
					goto IL_2769;
					IL_2698:
					num2 = 524;
					Application.DoEvents();
					goto IL_26a4;
					IL_26c2:
					num2 = 528;
					text24 = "";
					goto IL_26cf;
					IL_11ab:
					num2 = 280;
					if (!flag)
					{
						goto IL_11bc;
					}
					goto IL_27d7;
					IL_11bc:
					num2 = 281;
					text5 = "";
					goto IL_11c9;
					IL_11c9:
					num2 = 282;
					Interaction.MsgBox("Query/Script file number " + Conversions.ToString(checked(num6 + 1)) + " does not exist. (I.e., " + text6 + "). Specify a valid Query. If you are specifying input arguments, enclose them in double quotes", MsgBoxStyle.Exclamation, "Invalid Query File");
					goto IL_1210;
					IL_1210:
					num2 = 283;
					result = 0;
					break;
					IL_07f2:
					num2 = 142;
					num11 = 0;
					goto IL_07fb;
					IL_07fb:
					num2 = 143;
					if (Strings.InStr(Strings.UCase(MyQueryToProcess), ".SPFSQL") != 0)
					{
						goto IL_081c;
					}
					goto IL_0838;
					IL_081c:
					num2 = 144;
					num11 = 1;
					goto IL_0825;
					IL_0825:
					num2 = 145;
					text14 = ".spfsql";
					goto IL_08c9;
					IL_0838:
					num2 = 147;
					if (Strings.InStr(Strings.UCase(MyQueryToProcess), ".VGQ") != 0 || Strings.InStr(Strings.UCase(MyQueryToProcess), ".VG2") != 0 || Strings.InStr(Strings.UCase(MyQueryToProcess), ".VGEC") != 0 || Strings.InStr(Strings.UCase(MyQueryToProcess), ".VGE") != 0)
					{
						goto IL_0898;
					}
					goto IL_08b2;
					IL_26cf:
					num2 = 529;
					result = 0;
					goto IL_26d7;
					IL_26d7:
					num2 = 530;
					if (MyMode == 1 || MyMode == 2)
					{
						goto IL_26ec;
					}
					goto IL_26fd;
					IL_26ec:
					num2 = 531;
					text8 = "No Definition file was saved.";
					goto IL_270b;
					IL_08b2:
					num2 = 151;
					num11 = 2;
					goto IL_08bb;
					IL_08bb:
					num2 = 152;
					text14 = "";
					goto IL_08c9;
					IL_0898:
					num2 = 148;
					num11 = 0;
					goto IL_08a1;
					IL_08a1:
					num2 = 149;
					text14 = ".va";
					goto IL_08c9;
					IL_08c9:
					num2 = 154;
					text = "";
					goto IL_08d6;
					IL_08d6:
					num2 = 155;
					flag2 = false;
					goto IL_08df;
					IL_08df:
					num2 = 156;
					if (MyMode == 0 && num11 != 2)
					{
						goto IL_08fb;
					}
					goto IL_0caa;
					IL_08fb:
					num2 = 157;
					num8 = checked((short)Strings.InStrRev(MyQueryToProcess, "\\"));
					goto IL_0912;
					IL_0912:
					num2 = 158;
					if (num8 != 0)
					{
						goto IL_0926;
					}
					goto IL_09b2;
					IL_0926:
					num2 = 159;
					if (OptRun() || IsPacked)
					{
						goto IL_093b;
					}
					goto IL_0980;
					IL_093b:
					num2 = 160;
					text8 = Strings.Trim(Globals_Renamed.MyPCDir) + text11 + Strings.Trim(Conversions.ToString((int)num6)) + "__" + text14;
					goto IL_0a1c;
					IL_0980:
					num2 = 162;
					text8 = Strings.Trim(TxtDir.Text) + text11 + Strings.Trim(Conversions.ToString((int)num6)) + text14;
					goto IL_0a1c;
					IL_09b2:
					num2 = 165;
					if (OptRun() || IsPacked)
					{
						goto IL_09c7;
					}
					goto IL_09ed;
					IL_09c7:
					num2 = 166;
					text8 = text11 + Strings.Trim(Conversions.ToString((int)num6)) + "__" + text14;
					goto IL_0a1c;
					IL_09ed:
					num2 = 168;
					text8 = Strings.Trim(TxtDir.Text) + text11 + Strings.Trim(Conversions.ToString((int)num6)) + text14;
					goto IL_0a1c;
					IL_0a1c:
					num2 = 171;
					num7 = 0;
					goto IL_0a25;
					IL_0a25:
					num2 = 172;
					ContainsMacro = false;
					goto IL_0a2e;
					IL_0a2e:
					num2 = 173;
					if (num11 == 1)
					{
						goto IL_0a3f;
					}
					goto IL_0a63;
					IL_0a3f:
					num2 = 174;
					num7 = MyProject.Forms.FrmMain.Save_To_VA(ref MyQueryToProcess, text8, 2, ref ContainsMacro, ref MyInLineValue);
					goto IL_0a84;
					IL_0a63:
					num2 = 176;
					num7 = MyProject.Forms.FrmMain.Save_To_VA(ref MyQueryToProcess, text8, 0, ref ContainsMacro, ref MyInLineValue);
					goto IL_0a84;
					IL_0a84:
					num2 = 178;
					if (num7 == 0)
					{
						goto IL_0a95;
					}
					goto IL_0aef;
					IL_0a95:
					num2 = 179;
					Interaction.MsgBox("Script file number " + Conversions.ToString(checked(num6 + 1)) + " was not saved. (I.e., " + text6 + "). ", MsgBoxStyle.Exclamation, "SQLPathFinder Query Not Saved to VA");
					goto IL_0adc;
					IL_0adc:
					num2 = 180;
					result = 0;
					break;
					IL_0aef:
					num2 = 183;
					if (num7 == 1 && ContainsMacro && num10 > 0)
					{
						goto IL_0b09;
					}
					goto IL_0b3a;
					IL_0b09:
					num2 = 184;
					text24 = "SQLPathFinder does not support macros nested in queries which are themselves contained within a {Start-Macro} scope on the Process Multiple Queries form. Error in Line #" + Strings.Trim(Conversions.ToString((int)num6));
					goto IL_0b27;
					IL_0b27:
					num2 = 185;
					result = 0;
					goto IL_27d7;
					IL_0b3a:
					num2 = 188;
					if (num7 == 1)
					{
						goto IL_0b4e;
					}
					goto IL_0bd1;
					IL_0b4e:
					num2 = 189;
					flag2 = true;
					goto IL_0b57;
					IL_0b57:
					num2 = 190;
					num15 = checked((short)(num15 + 1));
					goto IL_0b64;
					IL_0b64:
					num2 = 191;
					array[num15].Type = "SPFSQL";
					goto IL_0b7d;
					IL_0b7d:
					num2 = 192;
					array[num15].File = text8;
					goto IL_0b93;
					IL_0b93:
					num2 = 193;
					array[num15].File2 = file;
					goto IL_0ba9;
					IL_0ba9:
					num2 = 194;
					array[num15].ID = Strings.Trim(Conversions.ToString(checked(num6 + 1)));
					goto IL_0d85;
					IL_0bd1:
					num2 = 196;
					if (num7 == 2)
					{
						goto IL_0be5;
					}
					goto IL_0d85;
					IL_0be5:
					num2 = 197;
					if (!OptRun())
					{
						goto IL_0bfa;
					}
					goto IL_0c09;
					IL_0bfa:
					num2 = 198;
					text = " \"Y\"";
					goto IL_0c09;
					IL_0c09:
					num2 = 200;
					if (Operators.CompareString(Strings.Trim(MyInLineValue), "", TextCompare: false) == 0)
					{
						goto IL_0c2a;
					}
					goto IL_0d85;
					IL_0c2a:
					num2 = 201;
					num15 = checked((short)(num15 + 1));
					goto IL_0c37;
					IL_0c37:
					num2 = 202;
					array[num15].Type = "SPFVA";
					goto IL_0c50;
					IL_0c50:
					num2 = 203;
					array[num15].File = text8;
					goto IL_0c66;
					IL_0c66:
					num2 = 204;
					array[num15].File2 = "";
					goto IL_0c7f;
					IL_0c7f:
					num2 = 205;
					array[num15].ID = Strings.Trim(Conversions.ToString(checked(num6 + 1)));
					goto IL_0d85;
					IL_0caa:
					num2 = 209;
					if (MyMode == 1 || MyMode == 2)
					{
						goto IL_0cbf;
					}
					goto IL_0cd7;
					IL_0cbf:
					num2 = 210;
					text8 = text6 + text7;
					goto IL_0d85;
					IL_0cd7:
					num2 = 212;
					text8 = text6;
					goto IL_0ce1;
					IL_0ce1:
					num2 = 213;
					if (IsPacked)
					{
						goto IL_0cef;
					}
					goto IL_0d0d;
					IL_0cef:
					num2 = 214;
					text8 = Strings.Replace(text8, "\"", "\"\"", 1, -1, CompareMethod.Text);
					goto IL_0d0d;
					IL_0d0d:
					num2 = 216;
					num15 = checked((short)(num15 + 1));
					goto IL_0d1a;
					IL_0d1a:
					num2 = 217;
					array[num15].Type = "SPFEXE";
					goto IL_0d33;
					IL_0d33:
					num2 = 218;
					array[num15].File = text8;
					goto IL_0d49;
					IL_0d49:
					num2 = 219;
					array[num15].File2 = "";
					goto IL_0d62;
					IL_0d62:
					num2 = 220;
					array[num15].ID = Strings.Trim(Conversions.ToString(checked(num6 + 1)));
					goto IL_0d85;
					IL_0d85:
					num2 = 222;
					text10 = "";
					goto IL_0d92;
					IL_0d92:
					num2 = 223;
					if (Operators.CompareString(MyInLineValue, "", TextCompare: false) != 0 && MyMode == 0)
					{
						goto IL_0db6;
					}
					goto IL_0e5f;
					IL_0db6:
					num2 = 224;
					text24 = "";
					goto IL_0dc3;
					IL_0dc3:
					num2 = 225;
					text24 = General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\"&&va \"" + Strings.Trim(text8) + "\"", 1, 0, 0);
					goto IL_0e08;
					IL_0e08:
					num2 = 226;
					if (Operators.CompareString(text24, "", TextCompare: false) != 0)
					{
						goto IL_0e24;
					}
					goto IL_1228;
					IL_0e24:
					num2 = 227;
					result = 0;
					goto IL_0e2c;
					IL_0e2c:
					num2 = 228;
					text24 = "Failed while saving Inline View Query Script # " + Strings.Trim(Conversions.ToString((int)num6)) + " included in the query list. " + text24;
					goto IL_27d7;
					IL_27d7:
					num2 = 548;
					Interaction.MsgBox("Error saving the Multi-Query Definition file. (" + Conversion.ErrorToString() + ")." + text24, MsgBoxStyle.OkOnly, "Error Saving Multi-Query Definition");
					goto IL_27ff;
					IL_27ff:
					num2 = 549;
					Information.Err().Clear();
					break;
					IL_0e5f:
					num2 = 232;
					if (IsPacked & (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0))
					{
						goto IL_0e81;
					}
					goto IL_0ece;
					IL_0e81:
					num2 = 233;
					if (MyMode == 0)
					{
						goto IL_0e91;
					}
					goto IL_0eaf;
					IL_0e91:
					num2 = 234;
					text9 = "";
					goto IL_0e9e;
					IL_0e9e:
					num2 = 235;
					text = "";
					goto IL_110a;
					IL_0eaf:
					num2 = 237;
					text9 = text8;
					goto IL_0eb9;
					IL_0eb9:
					num2 = 238;
					text = "";
					goto IL_110a;
					IL_0ece:
					num2 = 241;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_0ef0;
					}
					goto IL_1060;
					IL_0ef0:
					num2 = 242;
					if (!OptRun() && MyMode == 0 && flag2)
					{
						goto IL_0f0d;
					}
					goto IL_0f3f;
					IL_0f0d:
					num2 = 243;
					text9 = array[num15].ID + ",\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\SPFSQL3.VA " + General_Procedures.CompressString(File.ReadAllText(text8));
					goto IL_110a;
					IL_0f3f:
					num2 = 245;
					if (OptRun() && MyMode == 0 && flag2)
					{
						goto IL_0f59;
					}
					goto IL_0fba;
					IL_0f59:
					num2 = 246;
					text9 = array[num15].ID + "," + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\spfsql3.va " + General_Procedures.CompressString(File.ReadAllText(text8));
					goto IL_110a;
					IL_0fba:
					num2 = 248;
					if (num15 != -1)
					{
						goto IL_0fce;
					}
					goto IL_104d;
					IL_0fce:
					num2 = 249;
					if (Operators.CompareString(array[num15].Type, "SPFVA", TextCompare: false) == 0)
					{
						goto IL_0ff6;
					}
					goto IL_1026;
					IL_0ff6:
					num2 = 250;
					text9 = array[num15].ID + ",va.exe " + General_Procedures.CompressString(File.ReadAllText(text8));
					goto IL_110a;
					IL_1026:
					num2 = 252;
					text9 = array[num15].ID + "," + text8;
					goto IL_110a;
					IL_104d:
					num2 = 255;
					text9 = text8;
					goto IL_110a;
					IL_1060:
					num2 = 259;
					if (!OptRun() && MyMode == 0 && flag2)
					{
						goto IL_107d;
					}
					goto IL_109b;
					IL_107d:
					num2 = 260;
					text9 = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\SPFSQL3.VA \"" + text8 + "\"" + text21;
					goto IL_110a;
					IL_109b:
					num2 = 262;
					if (OptRun() && MyMode == 0 && flag2)
					{
						goto IL_10b5;
					}
					goto IL_10fd;
					IL_10b5:
					num2 = 263;
					text9 = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\spfsql3.va \"" + text8 + "\"" + text21;
					goto IL_110a;
					IL_10fd:
					num2 = 265;
					text9 = text8;
					goto IL_110a;
					IL_110a:
					num2 = 269;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1129;
					}
					goto IL_115d;
					IL_1129:
					num2 = 270;
					text9 = text9.Replace("\"\"", "\"");
					goto IL_1142;
					IL_1142:
					num2 = 271;
					text = text.Replace("\"\"", "\"");
					goto IL_115d;
					IL_115d:
					num2 = 273;
					if (Operators.CompareString(text5, "", TextCompare: false) == 0)
					{
						goto IL_1179;
					}
					goto IL_118e;
					IL_1179:
					num2 = 274;
					text5 = text9 + text;
					goto IL_1228;
					IL_118e:
					num2 = 276;
					text5 = text5 + Globals_Renamed.CRLF + text9 + text;
					goto IL_1228;
					IL_1228:
					num2 = 289;
					if (MyMode == 0)
					{
						goto IL_1238;
					}
					goto IL_128f;
					IL_1238:
					num2 = 290;
					GridMQ2.CurrentCell = GridMQ2[0, num6];
					goto IL_1258;
					IL_1258:
					num2 = 291;
					if (!QuietMode)
					{
						goto IL_1269;
					}
					goto IL_128f;
					IL_1269:
					num2 = 292;
					GridMQ2.Refresh();
					goto IL_127b;
					IL_127b:
					num2 = 293;
					Application.DoEvents();
					goto IL_128f;
					IL_128f:
					num2 = 298;
					num6 = checked((short)unchecked(num6 + 1));
					if (num6 <= 599)
					{
						goto IL_02d1;
					}
					goto IL_12a8;
					IL_12a8:
					num2 = 299;
					if (Operators.CompareString(text5, "", TextCompare: false) != 0 || (IsPacked && num15 > -1))
					{
						goto IL_12d0;
					}
					goto IL_26c2;
					IL_12d0:
					num2 = 300;
					if (MyMode == 1 || MyMode == 2)
					{
						goto IL_12e8;
					}
					goto IL_13b7;
					IL_12e8:
					ProjectData.ClearProjectError();
					num3 = 3;
					goto IL_12f0;
					IL_12f0:
					num2 = 302;
					if (fSaveAs || MyMode == 2)
					{
						goto IL_1307;
					}
					goto IL_1380;
					IL_1307:
					num2 = 303;
					BuildForm.FileOpenSave("S", fTitle, "spf", "Save a SQLPathFinder Multi-Query Definition", Globals_Renamed.gQueryDir);
					goto IL_132d;
					IL_132d:
					num2 = 304;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) != 0)
					{
						goto IL_1360;
					}
					goto IL_2816;
					IL_2816:
					num2 = 551;
					Information.Err().Clear();
					break;
					IL_1360:
					num2 = 306;
					text2 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_138f;
					IL_1380:
					num2 = 308;
					text2 = fTitle;
					goto IL_138f;
					IL_138f:
					num2 = 310;
					text5 = Set_Batch_Attr("G", "", ref PackedExe) + text5;
					goto IL_21a7;
					IL_13b7:
					num2 = 312;
					if (IsPacked && (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0))
					{
						goto IL_13f5;
					}
					goto IL_1d37;
					IL_26fd:
					num2 = 533;
					text8 = "";
					goto IL_270b;
					IL_270b:
					num2 = 535;
					Interaction.MsgBox("No Query Files to process." + text8, MsgBoxStyle.Information, "No Query Files");
					goto IL_272a;
					IL_13f5:
					num2 = 313;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1417;
					}
					goto IL_1549;
					IL_1417:
					num2 = 314;
					if (OptRun())
					{
						goto IL_1429;
					}
					goto IL_1458;
					IL_1429:
					num2 = 315;
					text2 = Batch_ScriptName;
					goto IL_1437;
					IL_1437:
					num2 = 316;
					text23 = Path.Combine(".\\", Path.GetFileName(Batch_ScriptName));
					goto IL_149c;
					IL_1458:
					num2 = 318;
					text23 = text11 + Path.GetFileName(Strings.Trim(TxtDir.Text)) + ".spf";
					goto IL_1481;
					IL_1481:
					num2 = 319;
					text2 = Path.Combine(Path.GetDirectoryName(Batch_ScriptName), text23);
					goto IL_149c;
					IL_149c:
					num2 = 321;
					text24 = " (" + text2 + ")";
					goto IL_14b5;
					IL_14b5:
					num2 = 322;
					if (!flag3)
					{
						goto IL_14c6;
					}
					goto IL_1522;
					IL_14c6:
					ProjectData.ClearProjectError();
					num3 = 4;
					goto IL_14ce;
					IL_14ce:
					num2 = 324;
					num5 = checked((short)FileSystem.FreeFile());
					goto IL_14dc;
					IL_14dc:
					num2 = 325;
					FileSystem.FileOpen(num5, text2, OpenMode.Output);
					goto IL_14f0;
					IL_14f0:
					num2 = 326;
					FileSystem.PrintLine(num5, text5);
					goto IL_1509;
					IL_1509:
					num2 = 327;
					FileSystem.FileClose(num5);
					goto IL_1522;
					IL_1522:
					num2 = 329;
					text5 = BuildSQL.Write_VA_Lib("va_lib_25py.lib");
					goto IL_1534;
					IL_1534:
					num2 = 330;
					Console.WriteLine("run multiple using py");
					goto IL_155c;
					IL_1549:
					num2 = 332;
					text5 = BuildSQL.Write_VA_Lib("va_lib_25.lib");
					goto IL_155c;
					IL_155c:
					num2 = 334;
					text17 = Strings.Trim(MyProject.Application.Info.DirectoryPath);
					goto IL_1578;
					IL_1578:
					num2 = 335;
					text5 = Strings.Replace(text5, "@EXEDIR@", text17, 1, -1, CompareMethod.Text);
					goto IL_1591;
					IL_1591:
					num2 = 336;
					text17 = Conversions.ToString((int)num15);
					goto IL_15a0;
					IL_15a0:
					num2 = 337;
					text5 = Strings.Replace(text5, "@NOELEMENTS@", text17, 1, -1, CompareMethod.Text);
					goto IL_15b9;
					IL_15b9:
					num2 = 338;
					text17 = Globals_Renamed.MyPCDir;
					goto IL_15c6;
					IL_15c6:
					num2 = 339;
					text5 = Strings.Replace(text5, "@PCWORKDIR@", text17, 1, -1, CompareMethod.Text);
					goto IL_15df;
					IL_15df:
					num2 = 340;
					text17 = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\";
					goto IL_15ec;
					IL_15ec:
					num2 = 341;
					text5 = Strings.Replace(text5, "@LIBDIR@", text17, 1, -1, CompareMethod.Text);
					goto IL_1605;
					IL_1605:
					num2 = 342;
					text17 = "";
					goto IL_1612;
					IL_1612:
					num2 = 343;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1634;
					}
					goto IL_16f0;
					IL_1634:
					num2 = 344;
					if (OptRun())
					{
						goto IL_1646;
					}
					goto IL_1675;
					IL_1646:
					num2 = 345;
					text2 = Batch_ScriptName;
					goto IL_1654;
					IL_1654:
					num2 = 346;
					text23 = Path.Combine(".\\", Path.GetFileName(Batch_ScriptName));
					goto IL_16b9;
					IL_1675:
					num2 = 348;
					text23 = text11 + Path.GetFileName(Strings.Trim(TxtDir.Text)) + ".spf";
					goto IL_169e;
					IL_169e:
					num2 = 349;
					text2 = Path.Combine(Path.GetDirectoryName(Batch_ScriptName), text23);
					goto IL_16b9;
					IL_16b9:
					num2 = 351;
					text17 = text17 + Globals_Renamed.CRLF + "#DATA spfFile IN \"" + text2 + "\"";
					goto IL_16f0;
					IL_16f0:
					num2 = 353;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_1712;
					}
					goto IL_184a;
					IL_1712:
					num2 = 354;
					num17 = num15;
					num14 = 0;
					goto IL_183f;
					IL_183f:
					if (num14 <= num17)
					{
						goto IL_1724;
					}
					goto IL_184a;
					IL_1724:
					num2 = 355;
					text13 = Strings.Trim(Conversions.ToString((int)num14));
					goto IL_1738;
					IL_1738:
					num2 = 356;
					if (Operators.CompareString(array[num14].Type, "SPFSQL", TextCompare: false) == 0)
					{
						goto IL_1760;
					}
					goto IL_17b6;
					IL_1760:
					num2 = 357;
					text17 = text17 + Globals_Renamed.CRLF + "#DATA my" + text13 + " IN \"" + Strings.Trim(array[num14].File) + "\"";
					goto IL_1832;
					IL_17b6:
					num2 = 359;
					if (Operators.CompareString(array[num14].Type, "SPFVA", TextCompare: false) == 0)
					{
						goto IL_17de;
					}
					goto IL_1832;
					IL_17de:
					num2 = 360;
					text17 = text17 + Globals_Renamed.CRLF + "#LOAD my" + text13 + " IN \"" + Strings.Trim(array[num14].File) + "\"";
					goto IL_1832;
					IL_1832:
					num2 = 362;
					num14 = checked((short)unchecked(num14 + 1));
					goto IL_183f;
					IL_184a:
					num2 = 364;
					text5 = Strings.Replace(text5, "@LOADMODULEDATA@", text17, 1, -1, CompareMethod.Text);
					goto IL_1863;
					IL_1863:
					num2 = 365;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1882;
					}
					goto IL_18b6;
					IL_1882:
					num2 = 366;
					text17 = "SPFFileToLoad = \"" + text23 + "\"";
					goto IL_189b;
					IL_189b:
					num2 = 367;
					text5 = Strings.Replace(text5, "@SPFFileToLoadDATA@", text17, 1, -1, CompareMethod.Text);
					goto IL_18b6;
					IL_18b6:
					num2 = 369;
					text17 = "";
					goto IL_18c3;
					IL_18c3:
					num2 = 370;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_18e5;
					}
					goto IL_1a3a;
					IL_18e5:
					num2 = 371;
					num18 = num15;
					num14 = 0;
					goto IL_1a2f;
					IL_1a2f:
					if (num14 <= num18)
					{
						goto IL_18f7;
					}
					goto IL_1a3a;
					IL_18f7:
					num2 = 372;
					text13 = Strings.Trim(Conversions.ToString((int)num14));
					goto IL_190b;
					IL_190b:
					num2 = 373;
					text17 = text17 + Globals_Renamed.CRLF + "SPFArr(" + text13 + ") = \"" + array[num14].Type + ":" + array[num14].ID + ":";
					goto IL_1973;
					IL_1973:
					num2 = 374;
					if ((Operators.CompareString(array[num14].Type, "SPFEXE", TextCompare: false) == 0) | LikeOperator.LikeString(array[num14].Type, "{*}*", CompareMethod.Binary))
					{
						goto IL_19b5;
					}
					goto IL_19db;
					IL_19b5:
					num2 = 375;
					text17 = text17 + array[num14].File + "\"";
					goto IL_1a22;
					IL_19db:
					num2 = 377;
					text17 = text17 + "my" + text13 + ";" + array[num14].File2 + "\"";
					goto IL_1a22;
					IL_1a22:
					num2 = 379;
					num14 = checked((short)unchecked(num14 + 1));
					goto IL_1a2f;
					IL_1a3a:
					num2 = 381;
					text5 = Strings.Replace(text5, "@ARRAYDATA@", text17, 1, -1, CompareMethod.Text);
					goto IL_1a53;
					IL_1a53:
					num2 = 382;
					text16 = "@ECHO OFF";
					goto IL_1a60;
					IL_1a60:
					num2 = 383;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1a7f;
					}
					goto IL_1ad0;
					IL_1a7f:
					num2 = 384;
					text16 = text16 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFPyEE_zipBuilder.py";
					goto IL_1ad0;
					IL_1ad0:
					num2 = 386;
					text16 += "\r\nVA /PACK=.\\RunSPF.exe .\\RunSPF.va";
					goto IL_1ae4;
					IL_1ae4:
					num2 = 387;
					text16 = text16 + "\r\nDEL/F/Q \".\\" + text11 + "*.*\" >NUL";
					goto IL_1aff;
					IL_1aff:
					num2 = 388;
					text16 += "\r\nDEL/F/Q .\\RunSPF.va >NUL";
					goto IL_1b13;
					IL_1b13:
					num2 = 389;
					if (Operators.CompareString(IsCustomF, "", TextCompare: false) == 0 && !OptRun())
					{
						goto IL_1b3a;
					}
					goto IL_1b9e;
					IL_272a:
					num2 = 536;
					if (IsPacked)
					{
						goto IL_2738;
					}
					goto IL_2769;
					IL_1b3a:
					num2 = 390;
					text16 = text16 + "\r\n" + BuildForm.AddDOSRetry("COPY .\\RunSPF.exe \"" + Strings.Trim(TxtSHGUIDir.Text) + Strings.Trim(TxtDir.Text) + "\"", "Copying");
					goto IL_1b87;
					IL_1b87:
					num2 = 391;
					text16 += "\r\nDEL /F/Q .\\RunSPF.exe";
					goto IL_1c03;
					IL_1b9e:
					num2 = 393;
					if (Operators.CompareString(IsCustomF, "", TextCompare: false) != 0)
					{
						goto IL_1bba;
					}
					goto IL_1c03;
					IL_1bba:
					num2 = 394;
					text16 = text16 + "\r\n" + BuildForm.AddDOSRetry("COPY /Y .\\RunSPF.exe \"" + Strings.Trim(IsCustomF) + "\"", "Copying");
					goto IL_1bee;
					IL_1bee:
					num2 = 395;
					text16 += "\r\nDEL /F/Q .\\RunSPF.exe";
					goto IL_1c03;
					IL_1c03:
					num2 = 397;
					text2 = Globals_Renamed.MyPCDir + "RunSPF.va";
					goto IL_1c1a;
					IL_1c1a:
					num2 = 398;
					if (QuietMode)
					{
						goto IL_1c28;
					}
					goto IL_1c4e;
					IL_1c28:
					num2 = 399;
					Globals_Renamed.gWorkQuery = text16;
					goto IL_1c35;
					IL_1c35:
					num2 = 400;
					text8 = General_Procedures.Save_File_General(text5, text2);
					break;
					IL_1c4e:
					num2 = 403;
					if (SHGUIMode == 0)
					{
						goto IL_1c61;
					}
					goto IL_1d08;
					IL_1c61:
					num2 = 404;
					text8 = General_Procedures.Save_File_General(text5, text2);
					goto IL_1c73;
					IL_1c73:
					num2 = 405;
					if (Operators.CompareString(text8, "", TextCompare: false) == 0)
					{
						goto IL_1c8f;
					}
					goto IL_1c98;
					IL_1c8f:
					num2 = 406;
					flag3 = true;
					goto IL_1c98;
					IL_1c98:
					num2 = 407;
					if (Operators.CompareString(RunCodeStr, "", TextCompare: false) != 0)
					{
						goto IL_1cb4;
					}
					goto IL_1ccc;
					IL_1cb4:
					num2 = 408;
					text16 = text16 + "\r\n@Echo.\r\n@Echo =================================================\r\n@Echo Run Query \r\n@Echo =================================================\r\n@Echo.\r\n" + RunCodeStr;
					goto IL_1ccc;
					IL_1ccc:
					num2 = 410;
					num12 = BuildSQL.RunQ(text16, 0);
					goto IL_1ce1;
					IL_1ce1:
					num2 = 411;
					if (num12 == 0)
					{
						break;
					}
					goto IL_1cf7;
					IL_1cf7:
					num2 = 413;
					text16 = "";
					goto IL_21a7;
					IL_1d08:
					num2 = 415;
					text16 += "\r\n@Echo Adding entry to ScriptHost. Stand by ...";
					goto IL_1d1c;
					IL_1d1c:
					num2 = 416;
					text16 += "\r\n@Echo.\r\n";
					goto IL_21a7;
					IL_1d37:
					num2 = 419;
					if (IsPacked && Operators.CompareString(Globals_Renamed.gSHType, "PY", TextCompare: false) == 0 && Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_1d75;
					}
					goto IL_214f;
					IL_2738:
					num2 = 537;
					TxtName.Text = "";
					goto IL_274f;
					IL_274f:
					num2 = 538;
					TxtDir.Text = "";
					goto IL_2769;
					IL_1d75:
					num2 = 420;
					if (OptRun())
					{
						goto IL_1d87;
					}
					goto IL_1db6;
					IL_1d87:
					num2 = 421;
					text2 = Batch_ScriptName;
					goto IL_1d95;
					IL_1d95:
					num2 = 422;
					text23 = Path.Combine(".\\", Path.GetFileName(Batch_ScriptName));
					goto IL_1dfa;
					IL_1db6:
					num2 = 424;
					text23 = text11 + Path.GetFileName(Strings.Trim(TxtDir.Text)) + ".spf";
					goto IL_1ddf;
					IL_1ddf:
					num2 = 425;
					text2 = Path.Combine(Path.GetDirectoryName(Batch_ScriptName), text23);
					goto IL_1dfa;
					IL_1dfa:
					num2 = 427;
					text24 = " (" + text2 + ")";
					goto IL_1e13;
					IL_1e13:
					num2 = 428;
					if (!flag3)
					{
						goto IL_1e24;
					}
					goto IL_1e89;
					IL_1e24:
					ProjectData.ClearProjectError();
					num3 = 5;
					goto IL_1e2c;
					IL_1e2c:
					num2 = 430;
					num5 = checked((short)FileSystem.FreeFile());
					goto IL_1e3a;
					IL_1e3a:
					num2 = 431;
					FileSystem.FileOpen(num5, text2, OpenMode.Output);
					goto IL_1e4e;
					IL_1e4e:
					num2 = 432;
					FileSystem.PrintLine(num5, text5);
					goto IL_1e67;
					IL_1e67:
					num2 = 433;
					FileSystem.FileClose(num5);
					goto IL_1e7e;
					IL_1e7e:
					num2 = 434;
					flag3 = true;
					goto IL_1e89;
					IL_1e89:
					num2 = 436;
					text2 = BuildForm.Strip_Add_MyPCDir("S", text2);
					goto IL_1ea2;
					IL_1ea2:
					num2 = 437;
					text16 = "@ECHO OFF";
					goto IL_1eaf;
					IL_1eaf:
					num2 = 438;
					text16 = text16 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFPyEE_zipBuilder.py\"";
					goto IL_1efe;
					IL_1efe:
					num2 = 439;
					text16 = text16 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Create_SPF_ZIP_EXE.Py\" /ZIP=\"RunSPF.zip\" /SPF=\"" + text2 + "\"";
					goto IL_1f5a;
					IL_1f5a:
					num2 = 440;
					text16 = text16 + "\r\nDEL/F/Q .\\" + text2 + " >NUL";
					goto IL_1f75;
					IL_1f75:
					num2 = 441;
					text16 = text16 + "\r\nDEL/F/Q \".\\" + text11 + "*.*\" >NUL";
					goto IL_1f90;
					IL_1f90:
					num2 = 442;
					if (Operators.CompareString(IsCustomF, "", TextCompare: false) == 0 && !OptRun())
					{
						goto IL_1fb7;
					}
					goto IL_201b;
					IL_2769:
					num2 = 541;
					if (!((MyMode == 1 || MyMode == 2) & (Operators.CompareString(text5, "", TextCompare: false) != 0)))
					{
						break;
					}
					goto IL_278f;
					IL_1fb7:
					num2 = 443;
					text16 = text16 + "\r\n" + BuildForm.AddDOSRetry("COPY .\\RunSPF.zip \"" + Strings.Trim(TxtSHGUIDir.Text) + Strings.Trim(TxtDir.Text) + "\"", "Copying");
					goto IL_2004;
					IL_2004:
					num2 = 444;
					text16 += "\r\nDEL /F/Q .\\RunSPF.zip";
					goto IL_2080;
					IL_201b:
					num2 = 446;
					if (Operators.CompareString(IsCustomF, "", TextCompare: false) != 0)
					{
						goto IL_2037;
					}
					goto IL_2080;
					IL_2037:
					num2 = 447;
					text16 = text16 + "\r\n" + BuildForm.AddDOSRetry("COPY /Y .\\RunSPF.zip \"" + Strings.Trim(IsCustomF) + "\"", "Copying");
					goto IL_206b;
					IL_206b:
					num2 = 448;
					text16 += "\r\nDEL /F/Q .\\RunSPF.zip";
					goto IL_2080;
					IL_2080:
					num2 = 450;
					if (QuietMode)
					{
						goto IL_208e;
					}
					goto IL_20a2;
					IL_208e:
					num2 = 451;
					Globals_Renamed.gWorkQuery = text16;
					break;
					IL_20a2:
					num2 = 454;
					if (SHGUIMode == 0)
					{
						goto IL_20b2;
					}
					goto IL_2122;
					IL_20b2:
					num2 = 455;
					if (Operators.CompareString(RunCodeStr, "", TextCompare: false) != 0)
					{
						goto IL_20ce;
					}
					goto IL_20e6;
					IL_20ce:
					num2 = 456;
					text16 = text16 + "\r\n@Echo.\r\n@Echo =================================================\r\n@Echo Run Query \r\n@Echo =================================================\r\n@Echo.\r\n" + RunCodeStr;
					goto IL_20e6;
					IL_20e6:
					num2 = 458;
					num12 = BuildSQL.RunQ(text16, 0);
					goto IL_20fb;
					IL_20fb:
					num2 = 459;
					if (num12 == 0)
					{
						break;
					}
					goto IL_2111;
					IL_2111:
					num2 = 461;
					text16 = "";
					goto IL_21a7;
					IL_2122:
					num2 = 463;
					text16 += "\r\n@Echo Adding entry to ScriptHost. Stand by ...";
					goto IL_2136;
					IL_2136:
					num2 = 464;
					text16 += "\r\n@Echo.\r\n";
					goto IL_21a7;
					IL_214f:
					num2 = 467;
					text24 = "";
					goto IL_215c;
					IL_215c:
					num2 = 468;
					if (OptRun())
					{
						goto IL_216e;
					}
					goto IL_2180;
					IL_216e:
					num2 = 469;
					text2 = Batch_ScriptName;
					goto IL_21a7;
					IL_2180:
					num2 = 471;
					text2 = Strings.Trim(TxtDir.Text) + text11 + ".spf";
					goto IL_21a7;
					IL_21a7:
					num2 = 475;
					text24 = " (" + text2 + ")";
					goto IL_21c0;
					IL_21c0:
					num2 = 476;
					if (!flag3)
					{
						goto IL_21d1;
					}
					goto IL_222d;
					IL_21d1:
					ProjectData.ClearProjectError();
					num3 = 6;
					goto IL_21d9;
					IL_21d9:
					num2 = 478;
					num5 = checked((short)FileSystem.FreeFile());
					goto IL_21e7;
					IL_21e7:
					num2 = 479;
					FileSystem.FileOpen(num5, text2, OpenMode.Output);
					goto IL_21fb;
					IL_21fb:
					num2 = 480;
					FileSystem.PrintLine(num5, text5);
					goto IL_2214;
					IL_2214:
					num2 = 481;
					FileSystem.FileClose(num5);
					goto IL_222d;
					IL_222d:
					num2 = 483;
					if (!OptRun() && SHGUIMode == 1)
					{
						goto IL_224a;
					}
					goto IL_2769;
					IL_224a:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_2253;
					IL_2253:
					num2 = 485;
					text12 = BuildForm.GetSHGUISubDir(TxtSHGUIDir.Text);
					goto IL_226b;
					IL_226b:
					num2 = 486;
					text13 = "JobSize=" + cmbQueue.Text + ";ScriptType=" + cmbType.Text;
					goto IL_2298;
					IL_2298:
					num2 = 487;
					text13 = text13 + ";MemSize=" + cmbMem.Text + ";Site=" + cmbSite.Text;
					goto IL_22dc;
					IL_22dc:
					num2 = 488;
					if (cmbjmp.SelectedIndex > 0)
					{
						goto IL_22f6;
					}
					goto IL_2317;
					IL_22f6:
					num2 = 489;
					text13 = text13 + ";JMP=" + cmbjmp.Text;
					goto IL_2317;
					IL_2317:
					num2 = 491;
					text = Strings.UCase(TxtSHGUIDir.Text);
					goto IL_232f;
					IL_232f:
					num2 = 492;
					if (LikeOperator.LikeString(text, "*PRODAT.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_2348;
					}
					goto IL_2358;
					IL_2348:
					num2 = 493;
					text19 = "AT";
					goto IL_23d1;
					IL_2358:
					num2 = 495;
					if (LikeOperator.LikeString(text, "*INTG.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_2371;
					}
					goto IL_2381;
					IL_2371:
					num2 = 496;
					text19 = "INTG";
					goto IL_23d1;
					IL_2381:
					num2 = 498;
					if (LikeOperator.LikeString(text, "*NSG.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_239a;
					}
					goto IL_23aa;
					IL_239a:
					num2 = 499;
					text19 = "NSG";
					goto IL_23d1;
					IL_23aa:
					num2 = 501;
					if (LikeOperator.LikeString(text, "*DEV.INTEL.COM*", CompareMethod.Binary))
					{
						goto IL_23c3;
					}
					goto IL_23d1;
					IL_23c3:
					num2 = 502;
					text19 = "DEV";
					goto IL_23d1;
					IL_23d1:
					num2 = 504;
					text8 = Strings.Trim(TxtDir.Text);
					goto IL_23e9;
					IL_23e9:
					num2 = 505;
					text = "";
					goto IL_23f6;
					IL_23f6:
					num2 = 506;
					num8 = checked((short)Strings.InStrRev(text8, "\\"));
					goto IL_240d;
					IL_240d:
					num2 = 507;
					if (num8 != 0)
					{
						goto IL_241e;
					}
					goto IL_2445;
					IL_241e:
					num2 = 508;
					text = Strings.Mid(text8, 1, num8);
					goto IL_2430;
					IL_2430:
					num2 = 509;
					text8 = Strings.Mid(text8, checked(num8 + 1));
					goto IL_2445;
					IL_2445:
					num2 = 511;
					if (mnuUseTmp.Checked)
					{
						goto IL_245c;
					}
					goto IL_246d;
					IL_245c:
					num2 = 512;
					text20 = "Y";
					goto IL_247b;
					IL_246d:
					num2 = 514;
					text20 = "N";
					goto IL_247b;
					IL_247b:
					num2 = 516;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_24b2;
					}
					goto IL_2586;
					end_IL_0001_2:
					break;
				}
				num2 = 556;
				Array.Clear(array, 0, array.Length);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 12668;
				continue;
			}
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void cmdCopy_Click(object eventSender, EventArgs eventArgs)
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
					switch (try0001_dispatch)
					{
					default:
					{
						short num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						if (!Operators.ConditionalCompareObjectNotEqual(GridMQ2.CurrentRow.Cells[0].Value, null, TextCompare: false))
						{
							break;
						}
						text3 = Strings.Trim(Conversions.ToString(GridMQ2.CurrentRow.Cells[0].Value));
						text7 = "";
						text3 = BuildForm.Replace_Globals(text3);
						if (Operators.CompareString(text3, "", TextCompare: false) == 0)
						{
							break;
						}
						if (LikeOperator.LikeString(Strings.UCase(text3), "*{END-*}", CompareMethod.Binary))
						{
							goto end_IL_0001;
						}
						num3 = (short)Strings.InStr(text3, " \"");
						if (num3 == 0)
						{
							text4 = text3;
							text2 = "";
						}
						else
						{
							text4 = Strings.Mid(text3, 1, num3 - 1);
							text2 = Strings.Mid(text3, num3);
						}
						if (LikeOperator.LikeString(text4, "!*", CompareMethod.Binary))
						{
							text4 = Strings.Mid(text4, 2);
							text7 = "!";
						}
						text4 = GetFullQName(text4);
						num3 = (short)Strings.InStrRev(text4, "\\");
						if (num3 != 0)
						{
							text = Strings.Mid(text4, num3 + 1);
						}
						else
						{
							text = text4;
							text4 = MyDefaultDir + text4;
						}
						text6 = ((text.Length < 3) ? "" : text.ToUpper().Substring(text.Length - 3));
						if (MyProject.Computer.FileSystem.FileExists(text4))
						{
							ProjectData.ClearProjectError();
							num2 = 3;
							MyProject.Forms.FrmMain.CMDialog1Save.FileName = text;
							MyProject.Forms.FrmMain.CMDialog1Save.Filter = "SQLPathFinder Query (*.vgq, *.vg2, *.vge)|*.vgq;*.VGQ;*.vg2;*.VG2;*.vge;*.VGE;*.vgec;*.VGEC|VA Scripts (*.va)|*.va;*.VA|Batch Scripts (*.bat)|*.bat;*.BAT|AllFiles (*.*)|*.*";
							if ((Operators.CompareString(text6, "VGQ", TextCompare: false) == 0) | (Operators.CompareString(text6, "VG2", TextCompare: false) == 0))
							{
								MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 1;
								MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "VG2";
							}
							else if (Operators.CompareString(text6, ".VA", TextCompare: false) == 0)
							{
								MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 3;
								MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "VA";
							}
							else
							{
								MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 5;
								MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "";
							}
							MyProject.Forms.FrmMain.CMDialog1Save.InitialDirectory = Globals_Renamed.gQueryDir;
							MyProject.Forms.FrmMain.CMDialog1Save.OverwritePrompt = true;
							MyProject.Forms.FrmMain.CMDialog1Save.CheckPathExists = true;
							MyProject.Forms.FrmMain.CMDialog1Save.Title = "Copy Query/Script to...";
							if (MyProject.Forms.FrmMain.CMDialog1Save.ShowDialog() == DialogResult.OK)
							{
								ProjectData.ClearProjectError();
								num2 = 4;
								text5 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
								text3 = text5 + text2;
								FileSystem.FileCopy(text4, text5);
								text3 = BuildForm.Strip_Add_MyPCDir("S2", text3, MyDefaultDir);
								GridMQ2.Rows[GridMQ2.CurrentCell.RowIndex].Cells[0].Value = text7 + text3;
								DataGridView MyGrid = GridMQ2;
								GridModule.Set_Grid_TopRow(ref MyGrid);
								GridMQ2 = MyGrid;
							}
						}
						else
						{
							Interaction.MsgBox("Could not copy as " + text4 + " was not found..", MsgBoxStyle.Information, "File Not Found");
						}
						break;
					}
					case 1113:
						num = -1;
						switch (num2)
						{
						case 2:
						case 4:
							Interaction.MsgBox("Error copying Query/Script. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Copying Query/Script");
							Information.Err().Clear();
							break;
						case 3:
							break;
						default:
							goto end_IL_0001_2;
						}
						break;
					}
					Init_Idx();
					break;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1113;
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

	private void cmdDelete_Click(object eventSender, EventArgs eventArgs)
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
					MyGrid = GridMQ2;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					GridMQ2 = MyGrid;
					goto IL_0024;
					IL_0024:
					num2 = 3;
					GridMQ2.RowCount = 600;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				MyGrid = GridMQ2;
				GridModule.Number_Grid(ref MyGrid);
				GridMQ2 = MyGrid;
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

	private void cmdDown_Click(object eventSender, EventArgs eventArgs)
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
				DataGridView MyGrid = GridMQ2;
				GridModule.Grid_Down(ref MyGrid);
				GridMQ2 = MyGrid;
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

	private void cmdEdit_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		int num3 = default(int);
		short num4 = default(short);
		string text = default(string);
		string text2 = default(string);
		short num7 = default(short);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					string MyData;
					short design_Mode;
					switch (try0001_dispatch)
					{
					default:
					{
						num2 = 1;
						short num6 = 0;
						goto IL_0006;
					}
					case 1745:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
							case 3:
								break;
							case 1:
								goto IL_0565;
							default:
								goto end_IL_0001;
							}
							goto IL_04dd;
						}
						IL_0324:
						num2 = 48;
						if (num4 == 1)
						{
							goto IL_0332;
						}
						goto IL_045c;
						IL_0332:
						num2 = 49;
						base.WindowState = FormWindowState.Minimized;
						goto IL_045c;
						IL_030d:
						num2 = 47;
						num4 = MyProject.Forms.FrmMain.OpenQuery(text, 1);
						goto IL_0324;
						IL_0565:
						num5 = unchecked(num + 1);
						num = 0;
						switch (num5)
						{
						case 1:
							break;
						case 2:
							goto IL_0006;
						case 3:
							goto IL_000f;
						case 4:
							goto IL_0018;
						case 5:
							goto IL_001d;
						case 6:
							goto IL_0022;
						case 7:
							goto IL_002b;
						case 8:
							goto IL_0034;
						case 9:
							goto IL_003d;
						case 10:
							goto IL_0047;
						case 11:
							goto IL_004d;
						case 12:
							goto IL_0055;
						case 13:
							goto IL_00bb;
						case 14:
							goto IL_00cc;
						case 16:
						case 17:
							goto IL_00e8;
						case 18:
							goto IL_0112;
						case 19:
							goto IL_011f;
						case 20:
							goto IL_0135;
						case 21:
							goto IL_0142;
						case 22:
						case 23:
							goto IL_014e;
						case 26:
							goto IL_0179;
						case 27:
							goto IL_0199;
						case 28:
							goto IL_01b4;
						case 29:
							goto IL_01cd;
						case 31:
							goto IL_01fb;
						case 34:
							goto IL_0227;
						case 35:
							goto IL_023a;
						case 36:
							goto IL_0248;
						case 38:
							goto IL_0253;
						case 37:
						case 39:
						case 40:
							goto IL_0265;
						case 41:
							goto IL_0272;
						case 42:
							goto IL_0286;
						case 43:
							goto IL_029a;
						case 44:
							goto IL_02b4;
						case 45:
							goto IL_02f2;
						case 47:
							goto IL_030d;
						case 48:
							goto IL_0324;
						case 49:
							goto IL_0332;
						case 51:
							goto IL_0343;
						case 52:
							goto IL_034c;
						case 53:
							goto IL_0356;
						case 54:
							goto IL_0374;
						case 55:
							goto IL_0388;
						case 56:
							goto IL_0391;
						case 57:
							goto IL_03ac;
						case 58:
							goto IL_03c2;
						case 59:
							goto IL_03d0;
						case 60:
							goto IL_03d8;
						case 61:
							goto IL_03ef;
						case 62:
							goto IL_03fd;
						case 66:
							goto IL_0410;
						case 67:
							goto IL_0429;
						case 69:
							goto IL_0446;
						case 46:
						case 50:
						case 63:
						case 64:
						case 65:
						case 68:
						case 70:
						case 71:
							goto IL_045c;
						case 72:
							goto IL_0475;
						case 73:
							goto IL_049d;
						case 74:
							goto IL_04c3;
						case 78:
							goto IL_04dd;
						case 79:
							goto IL_051d;
						case 80:
							goto IL_052b;
						case 82:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
						case 24:
						case 25:
						case 30:
						case 32:
						case 33:
						case 75:
						case 76:
						case 77:
						case 81:
						case 83:
						case 84:
						case 85:
						case 86:
						case 87:
							goto end_IL_0001_3;
						}
						goto default;
						IL_04dd:
						num2 = 78;
						Interaction.MsgBox("Error processing query: " + text + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Processing Query");
						goto IL_051d;
						IL_051d:
						num2 = 79;
						Information.Err().Clear();
						goto IL_052b;
						IL_052b:
						ProjectData.ClearProjectError();
						num3 = -3;
						goto end_IL_0001_3;
						IL_0006:
						num2 = 2;
						text = "";
						goto IL_000f;
						IL_000f:
						num2 = 3;
						text2 = "";
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num7 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						num4 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						text3 = "";
						goto IL_002b;
						IL_002b:
						num2 = 7;
						text4 = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						text4 = "";
						goto IL_003d;
						IL_003d:
						num2 = 9;
						text5 = "";
						goto IL_0047;
						IL_0047:
						num2 = 10;
						num8 = 0;
						goto IL_004d;
						IL_004d:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_0055;
						IL_0055:
						num2 = 12;
						if (!Operators.ConditionalCompareObjectNotEqual(GridMQ2.CurrentRow.Cells[0].Value, null, TextCompare: false) && Operators.CompareString(Strings.Trim(Conversions.ToString(GridMQ2.CurrentRow.Cells[0].Value)), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_00bb;
						IL_045c:
						num2 = 71;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0475;
						IL_00bb:
						num2 = 13;
						if (Globals_Renamed.g_FrmIdx > 0)
						{
							goto IL_00cc;
						}
						goto IL_00e8;
						IL_00cc:
						num2 = 14;
						Interaction.MsgBox("You cannot edit Multi-Query Scripts While Building a Pre/Post Query", MsgBoxStyle.Information, "Invalid Operation");
						goto end_IL_0001_3;
						IL_00e8:
						num2 = 17;
						text2 = Strings.Trim(Conversions.ToString(GridMQ2.CurrentRow.Cells[0].Value));
						goto IL_0112;
						IL_0112:
						num2 = 18;
						text2 = BuildForm.Replace_Globals(text2, 1);
						goto IL_011f;
						IL_011f:
						num2 = 19;
						if (LikeOperator.LikeString(text2, "!*", CompareMethod.Binary))
						{
							goto IL_0135;
						}
						goto IL_014e;
						IL_0135:
						num2 = 20;
						text2 = Strings.Mid(text2, 2);
						goto IL_0142;
						IL_0142:
						num2 = 21;
						text5 = "!";
						goto IL_014e;
						IL_014e:
						num2 = 23;
						if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(text2)), "{END*}", CompareMethod.Binary))
						{
							goto end_IL_0001_3;
						}
						goto IL_0179;
						IL_0179:
						num2 = 26;
						if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(text2)), "{*}*", CompareMethod.Binary))
						{
							goto IL_0199;
						}
						goto IL_01fb;
						IL_0199:
						num2 = 27;
						text4 = BuildForm.ParseUtilArgs(text2, "", "");
						goto IL_01b4;
						IL_01b4:
						num2 = 28;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_01cd;
						IL_01cd:
						num2 = 29;
						GridMQ2.CurrentRow.Cells[0].Value = text5 + text4;
						goto end_IL_0001_3;
						IL_01fb:
						num2 = 31;
						if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(text2)), "{END*}", CompareMethod.Binary))
						{
							goto end_IL_0001_3;
						}
						goto IL_0227;
						IL_0227:
						num2 = 34;
						num7 = (short)Strings.InStr(text2, " \"");
						goto IL_023a;
						IL_023a:
						num2 = 35;
						if (num7 == 0)
						{
							goto IL_0248;
						}
						goto IL_0253;
						IL_0248:
						num2 = 36;
						text = text2;
						goto IL_0265;
						IL_0253:
						num2 = 38;
						text = Strings.Mid(text2, 1, num7 - 1);
						goto IL_0265;
						IL_0265:
						num2 = 40;
						text = GetFullQName(text);
						goto IL_0272;
						IL_0272:
						num2 = 41;
						if (!FileExists2(text))
						{
							break;
						}
						goto IL_0286;
						IL_0286:
						num2 = 42;
						num7 = (short)Strings.InStrRev(text, ".");
						goto IL_029a;
						IL_029a:
						num2 = 43;
						text3 = Strings.Trim(Strings.UCase(Strings.Mid(text, num7 + 1)));
						goto IL_02b4;
						IL_02b4:
						num2 = 44;
						if (Operators.CompareString(text3, "VG2", TextCompare: false) == 0 || Operators.CompareString(text3, "VGQ", TextCompare: false) == 0 || Operators.CompareString(text3, "VGE", TextCompare: false) == 0)
						{
							goto IL_02f2;
						}
						goto IL_0410;
						IL_0475:
						num2 = 72;
						GridMQ2.CurrentRow.Cells[0].Value = text5 + text4;
						goto IL_049d;
						IL_049d:
						num2 = 73;
						if (!BuildForm.IsUtil("JMP") || cmbjmp.SelectedIndex != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_04c3;
						IL_0410:
						num2 = 66;
						if (Operators.CompareString(text3, "SPFSQL", TextCompare: false) == 0)
						{
							goto IL_0429;
						}
						goto IL_0446;
						IL_0429:
						num2 = 67;
						MyData = "";
						BuildForm.HG_Editor(ref MyData, "SQL", text);
						goto IL_045c;
						IL_0446:
						num2 = 69;
						text4 = BuildForm.ParseUtilArgs(text2, text3, text);
						goto IL_045c;
						IL_02f2:
						num2 = 45;
						design_Mode = Globals_Renamed.Design_Mode;
						if (design_Mode == 0)
						{
							goto IL_030d;
						}
						if (design_Mode == 2)
						{
							goto IL_0343;
						}
						goto IL_045c;
						IL_04c3:
						num2 = 74;
						cmbjmp.SelectedIndex = 1;
						goto end_IL_0001_3;
						IL_0343:
						num2 = 51;
						Globals_Renamed.QueryCancel = 0;
						goto IL_034c;
						IL_034c:
						num2 = 52;
						num8 = Globals_Renamed.g_FrmIdx;
						goto IL_0356;
						IL_0356:
						num2 = 53;
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Close();
						goto IL_0374;
						IL_0374:
						num2 = 54;
						if (Globals_Renamed.QueryCancel == 0)
						{
							goto IL_0388;
						}
						goto IL_045c;
						IL_0388:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0391;
						IL_0391:
						num2 = 56;
						MyProject.Forms.FrmMain.FrmSQLQuery[num8].Dispose();
						goto IL_03ac;
						IL_03ac:
						num2 = 57;
						MyProject.Forms.FrmMain.FrmSQLQuery[num8] = null;
						goto IL_03c2;
						IL_03c2:
						num2 = 58;
						Information.Err().Clear();
						goto IL_03d0;
						IL_03d0:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_03d8;
						IL_03d8:
						num2 = 60;
						num4 = MyProject.Forms.FrmMain.OpenQuery(text, 1);
						goto IL_03ef;
						IL_03ef:
						num2 = 61;
						if (num4 == 1)
						{
							goto IL_03fd;
						}
						goto IL_045c;
						IL_03fd:
						num2 = 62;
						base.WindowState = FormWindowState.Minimized;
						goto IL_045c;
						end_IL_0001_2:
						break;
					}
					num2 = 82;
					Interaction.MsgBox("Query " + text2 + " does not exist. Specify a valid Query File. Also, enclose input arguments in double quotes", MsgBoxStyle.Exclamation, "Invalid Query File");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1745;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdUp_Click(object eventSender, EventArgs eventArgs)
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
				DataGridView MyGrid = GridMQ2;
				GridModule.Grid_Up(ref MyGrid);
				GridMQ2 = MyGrid;
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

	private void FrmMultiQuery_Load(object eventSender, EventArgs eventArgs)
	{
		int num = 0;
		Button MyButton = cmdBrowse;
		BuildForm.Set_Btn_Img(ref MyButton, "browse");
		cmdBrowse = MyButton;
		MyButton = cmdEdit;
		BuildForm.Set_Btn_Img(ref MyButton, "editinfo");
		cmdEdit = MyButton;
		MyButton = cmdUp;
		BuildForm.Set_Btn_Img(ref MyButton, "up");
		cmdUp = MyButton;
		MyButton = cmdDown;
		BuildForm.Set_Btn_Img(ref MyButton, "down");
		cmdDown = MyButton;
		MyButton = cmdInsert;
		BuildForm.Set_Btn_Img(ref MyButton, "insert");
		cmdInsert = MyButton;
		MyButton = cmdDelete;
		BuildForm.Set_Btn_Img(ref MyButton, "delete");
		cmdDelete = MyButton;
		MyButton = cmdCopy;
		BuildForm.Set_Btn_Img(ref MyButton, "copy");
		cmdCopy = MyButton;
		MyButton = cmdSaveScripts;
		BuildForm.Set_Btn_Img(ref MyButton, "save");
		cmdSaveScripts = MyButton;
		MyButton = cmdSaveAddScripts;
		BuildForm.Set_Btn_Img(ref MyButton, "clock");
		cmdSaveAddScripts = MyButton;
		Globals_Renamed.gSHType = "PY";
		ComboBox cmbtype;
		if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
		{
			cmbtype = cmbType;
			BuildForm.Set_SH_Type(ref cmbtype, "V");
			cmbType = cmbtype;
		}
		else
		{
			cmbtype = cmbType;
			BuildForm.Set_SH_Type(ref cmbtype, "P");
			cmbType = cmbtype;
		}
		if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0)
		{
			mnuPySH.Checked = false;
		}
		else
		{
			mnuPySH.Checked = true;
		}
		l_IsInit = true;
		f_ShguiDir = Globals_Renamed.gSHGUIDir;
		fTitle = "SQLPathFinder.spf";
		Batch_ScriptName = Globals_Renamed.MyPCDir + "__sqlpathfinder__.spf";
		GridMQ2.RowCount = 600;
		GridMQ2.Columns[0].Width = 300;
		Set_Col_Width();
		Resize_OptRun();
		cmbtype = cmbQueue;
		ComboBox TxtSHGUIDir = this.TxtSHGUIDir;
		BuildForm.Init_SH_Controls(ref cmbtype, ref TxtSHGUIDir);
		this.TxtSHGUIDir = TxtSHGUIDir;
		cmbQueue = cmbtype;
		cmbQueue.SelectedIndex = 0;
		if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
		{
			cmbMem.SelectedIndex = 1;
		}
		else
		{
			cmbMem.SelectedIndex = 0;
		}
		cmbSite.SelectedIndex = 0;
		cmbjmp.SelectedIndex = 0;
		checked
		{
			int num2 = cmbSite.Items.Count - 1;
			for (num = 0; num <= num2; num++)
			{
				if (Operators.ConditionalCompareObjectEqual(cmbSite.Items[num], Globals_Renamed.gLastSHSite, TextCompare: false))
				{
					cmbSite.SelectedIndex = num;
					break;
				}
			}
			Number_Grid2(0);
		}
	}

	private void FrmMultiQuery_FormClosing(object eventSender, FormClosingEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		bool cancel = default(bool);
		int num = default(int);
		int num3 = default(int);
		int num5 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				CloseReason closeReason;
				bool flag;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					cancel = eventArgs.Cancel;
					goto IL_000b;
				case 526:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0015;
						case 4:
							goto IL_001e;
						case 5:
							goto IL_0023;
						case 6:
							goto IL_0028;
						case 8:
							goto IL_0049;
						case 9:
							goto IL_005d;
						case 10:
							goto IL_0095;
						case 11:
							goto IL_00b5;
						case 12:
							goto IL_00c3;
						case 14:
							goto IL_00d5;
						case 17:
							goto IL_010b;
						case 13:
						case 15:
						case 16:
						case 18:
						case 19:
							goto IL_0119;
						case 20:
							goto IL_0140;
						case 21:
							goto IL_0153;
						case 23:
							goto IL_0165;
						case 24:
							goto IL_016a;
						case 25:
							goto IL_0174;
						case 22:
						case 26:
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 28:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0165:
					num2 = 23;
					cancel = true;
					goto IL_016a;
					IL_016a:
					num2 = 24;
					Init_Idx();
					goto IL_0174;
					IL_0153:
					num2 = 21;
					BuildForm.Save_DTRBuild_Ini("B");
					break;
					IL_0174:
					num2 = 25;
					Globals_Renamed.QueryCancel = -1;
					break;
					IL_000b:
					num2 = 2;
					closeReason = eventArgs.CloseReason;
					goto IL_0015;
					IL_0015:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001e;
					IL_001e:
					num2 = 4;
					num5 = 0;
					goto IL_0023;
					IL_0023:
					num2 = 5;
					flag = false;
					goto IL_0028;
					IL_0028:
					num2 = 6;
					if (Operators.CompareString(F_FormMode, "NORMAL", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0049;
					IL_0049:
					num2 = 8;
					if (Script_Found() == 0)
					{
						goto IL_005d;
					}
					goto IL_0165;
					IL_005d:
					num2 = 9;
					if (LikeOperator.LikeString(Strings.UCase(TxtSHGUIDir.Text), "\\\\SHUSER*.INTEL.COM\\SH*USER$\\" + Strings.UCase(Globals_Renamed.gWinuser) + "*", CompareMethod.Binary))
					{
						goto IL_0095;
					}
					goto IL_010b;
					IL_0095:
					num2 = 10;
					num5 = Strings.InStr(TxtSHGUIDir.Text, Strings.LCase(Globals_Renamed.gWinuser));
					goto IL_00b5;
					IL_00b5:
					num2 = 11;
					if (num5 == 0)
					{
						goto IL_00c3;
					}
					goto IL_00d5;
					IL_00c3:
					num2 = 12;
					Globals_Renamed.gSHGUIDir = f_ShguiDir;
					goto IL_0119;
					IL_00d5:
					num2 = 14;
					Globals_Renamed.gSHGUIDir = Strings.Mid(TxtSHGUIDir.Text, 1, checked(num5 - 1)) + Strings.LCase(Globals_Renamed.gWinuser) + "\\";
					goto IL_0119;
					IL_010b:
					num2 = 17;
					Globals_Renamed.gSHGUIDir = Globals_Renamed.gBaseSHGUIDir;
					goto IL_0119;
					IL_0119:
					num2 = 19;
					if (Operators.CompareString(Strings.Trim(cmbSite.Text), "", TextCompare: false) != 0)
					{
						goto IL_0140;
					}
					goto IL_0153;
					IL_0140:
					num2 = 20;
					Globals_Renamed.gLastSHSite = cmbSite.Text;
					goto IL_0153;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				eventArgs.Cancel = cancel;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 526;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmMultiQuery_Resize(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		long num5 = default(long);
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
					num5 = 0L;
					goto IL_0007;
				case 308:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
						}
						int num4 = num + 1;
						num = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0007;
						case 3:
							goto IL_0010;
						case 4:
							goto IL_0021;
						case 6:
							goto IL_0031;
						case 8:
							goto IL_0046;
						case 5:
						case 7:
						case 9:
						case 10:
							goto IL_0054;
						case 11:
							goto IL_005f;
						case 12:
							goto IL_0071;
						case 14:
							goto IL_00a2;
						case 13:
						case 15:
						case 16:
							goto IL_00c4;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0071:
					num2 = 12;
					GridMQ2.Height = checked((int)(num5 - (FrmBatch.Height + cmdBrowse.Top + 45)));
					goto IL_00c4;
					IL_00a2:
					num2 = 14;
					GridMQ2.Height = checked((int)(num5 - (cmdBrowse.Top + 45)));
					goto IL_00c4;
					IL_005f:
					num2 = 11;
					if (!OptRun())
					{
						goto IL_0071;
					}
					goto IL_00a2;
					IL_00c4:
					num2 = 16;
					Set_Col_Width();
					break;
					IL_0007:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					if (base.WindowState == FormWindowState.Maximized)
					{
						goto IL_0021;
					}
					goto IL_0031;
					IL_0021:
					num2 = 4;
					SaveWindowState = "M";
					goto IL_0054;
					IL_0031:
					num2 = 6;
					if (base.WindowState != FormWindowState.Minimized)
					{
						goto IL_0046;
					}
					goto IL_0054;
					IL_0046:
					num2 = 8;
					SaveWindowState = "N";
					goto IL_0054;
					IL_0054:
					num2 = 10;
					num5 = base.Height;
					goto IL_005f;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				Application.DoEvents();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 308;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmMultiQuery_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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
				Dispose();
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

	public void mnuMQExit_Click(object eventSender, EventArgs eventArgs)
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

	public void mnuMQHelp_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Process+Multiple+Queries+Form");
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

	public void mnuMQNew_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short num6 = default(short);
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
					case 320:
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
								goto IL_0024;
							case 4:
								goto IL_0034;
							case 5:
								goto IL_0038;
							case 6:
								goto IL_0061;
							case 7:
								goto IL_008a;
							case 8:
								goto IL_0099;
							case 9:
								goto IL_00ac;
							case 10:
								goto IL_00bc;
							case 11:
								goto IL_00d8;
							case 12:
							case 13:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 14:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00ac:
						num2 = 9;
						GridMQ2.FirstDisplayedScrollingRowIndex = 0;
						goto IL_00bc;
						IL_00bc:
						num2 = 10;
						GridMQ2.CurrentCell = GridMQ2[1, 1];
						goto IL_00d8;
						IL_0099:
						num2 = 8;
						TxtName.Text = "";
						goto IL_00ac;
						IL_00d8:
						num2 = 11;
						Set_Query_In_Title("");
						break;
						IL_000b:
						num2 = 2;
						num5 = (short)Interaction.MsgBox("Are you sure you want to clear the Queries/Scripts?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear the Queries/Scripts?");
						goto IL_0024;
						IL_0024:
						num2 = 3;
						if (num5 != 6)
						{
							break;
						}
						goto IL_0034;
						IL_0034:
						num2 = 4;
						num6 = 0;
						goto IL_0038;
						IL_0038:
						num2 = 5;
						GridMQ2.Rows[num6].Cells[0].Value = "";
						goto IL_0061;
						IL_0061:
						num2 = 6;
						GridMQ2.Rows[num6].Cells[1].Value = "";
						goto IL_008a;
						IL_008a:
						num2 = 7;
						num6 = (short)unchecked(num6 + 1);
						if (num6 <= 599)
						{
							goto IL_0038;
						}
						goto IL_0099;
						end_IL_0001_2:
						break;
					}
					num2 = 13;
					Init_Idx();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 320;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuMQOpen_Click(object eventSender, EventArgs eventArgs)
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
				case 72:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
					OpenMQ("");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 72;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void mnuMQRun_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyErr = default(string);
		while (true)
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
							goto IL_0023;
						case 6:
							goto IL_003b;
						case 7:
							goto IL_0049;
						case 8:
							goto IL_0084;
						case 9:
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
					IL_003b:
					num2 = 6;
					SetUsePyEnginePMQ("G");
					goto IL_0049;
					IL_0049:
					num2 = 7;
					if (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) != 0 && (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "Y", TextCompare: false) != 0 || !BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1)))
					{
						break;
					}
					goto IL_0084;
					IL_0023:
					num2 = 4;
					Interaction.MsgBox("You cannot run a Multi-Query form While a Pre/Post Query is loaded", MsgBoxStyle.Exclamation, "Cannot Run");
					goto end_IL_0001_3;
					IL_0084:
					num2 = 8;
					Run_Interactive();
					break;
					IL_000b:
					num2 = 2;
					MyErr = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Globals_Renamed.g_FrmIdx > 0)
					{
						goto IL_0023;
					}
					goto IL_003b;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				SetUsePyEnginePMQ("U");
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

	private void TbMQ_ButtonClick(object eventSender, EventArgs eventArgs)
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
				case 287:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_005a;
						case 8:
							goto IL_0071;
						case 10:
							goto IL_0088;
						case 12:
							goto IL_00a0;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 9:
						case 11:
						case 13:
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0088:
					num2 = 10;
					mnuMQSave_Click(mnuMQSave, new EventArgs());
					goto end_IL_0001_3;
					IL_0071:
					num2 = 8;
					mnuMQOpen_Click(mnuMQOpen, new EventArgs());
					goto end_IL_0001_3;
					IL_00a0:
					num2 = 12;
					mnuMQRun_Click(mnuMQRun, new EventArgs());
					goto end_IL_0001_3;
					IL_005a:
					num2 = 6;
					mnuMQNew_Click(mnuMQNew, new EventArgs());
					goto end_IL_0001_3;
					IL_000b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0014;
					IL_0014:
					num2 = 3;
					TbMQ.Refresh();
					goto IL_0023;
					IL_0023:
					num2 = 4;
					switch (toolStripItem.Owner.Items.IndexOf(toolStripItem))
					{
					case 0:
						break;
					case 1:
						goto IL_0071;
					case 2:
						goto IL_0088;
					case 3:
						goto IL_00a0;
					case 5:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001_3;
					}
					goto IL_005a;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				PMQ_Use_Python_Engine("0");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 287;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void TxtSHGUIDir_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
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
				string myDir = Strings.UCase(TxtSHGUIDir.Text);
				ComboBox cmbsite = cmbSite;
				BuildForm.Init_SH_Sites(myDir, ref cmbsite);
				cmbSite = cmbsite;
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

	private void Resize_OptRun()
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
						case 4:
							goto IL_002c;
						case 5:
							goto IL_003a;
						case 6:
							goto IL_0049;
						case 8:
							goto IL_005c;
						case 9:
							goto IL_0071;
						case 10:
							goto IL_0080;
						case 11:
							goto IL_0090;
						case 7:
						case 12:
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0071:
					num2 = 9;
					base.Height = 538;
					goto IL_0080;
					IL_0080:
					num2 = 10;
					FrmBatch.Visible = true;
					goto IL_0090;
					IL_005c:
					num2 = 8;
					if (base.Height < 538)
					{
						goto IL_0071;
					}
					goto IL_0080;
					IL_0090:
					num2 = 11;
					TBTxtLog.Enabled = false;
					break;
					IL_000b:
					num2 = 2;
					if (OptRun())
					{
						goto IL_0017;
					}
					goto IL_005c;
					IL_0017:
					num2 = 3;
					if (base.Height < 436)
					{
						goto IL_002c;
					}
					goto IL_003a;
					IL_002c:
					num2 = 4;
					base.Height = 436;
					goto IL_003a;
					IL_003a:
					num2 = 5;
					FrmBatch.Visible = false;
					goto IL_0049;
					IL_0049:
					num2 = 6;
					TBTxtLog.Enabled = true;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				FrmMultiQuery_Resize(this, new EventArgs());
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 254;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridMQ2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		DataGridView MyGrid = GridMQ2;
		BuildForm.SetMQColor(ref MyGrid, GridMQ2.CurrentRow.Index);
		GridMQ2 = MyGrid;
	}

	private void GridMQ2_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
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
				Set_Col_Width();
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

	private void EditQueryScriptToolStripMenuItem_Click(object sender, EventArgs e)
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
				cmdEdit_Click(cmdEdit, new EventArgs());
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

	private void LoadQuryScriptToolStripMenuItem_Click(object sender, EventArgs e)
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
				cmdBrowse_Click(cmdBrowse, new EventArgs());
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

	private void cmdBrowse_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num4 = default(int);
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
						int num3 = 0;
						num4 = 0;
						IEnumerator FilesEnum = null;
						string text = "";
						int num5 = 0;
						num4 = GridMQ2.CurrentCell.RowIndex;
						num3 = BuildForm.File_Multi2(ref FilesEnum);
						if (num3 == 0)
						{
							goto end_IL_0001;
						}
						if (num4 + num3 > 600)
						{
							Interaction.MsgBox("Too many files were selected for the Process Multiple Queries form based on your starting row. The limit is " + Conversions.ToString(600) + ". Select fewer files or change the Grid starting position", MsgBoxStyle.Exclamation, "Too many Files");
							break;
						}
						FilesEnum.Reset();
						num5 = num4;
						while (FilesEnum.MoveNext())
						{
							text = Conversions.ToString(FilesEnum.Current);
							text = BuildForm.Strip_Add_MyPCDir("S2", text, MyDefaultDir);
							GridMQ2.Rows[num5].Cells[0].Value = text;
							num5++;
						}
						DataGridView MyGrid = GridMQ2;
						GridModule.Set_Grid_TopRow(ref MyGrid);
						GridMQ2 = MyGrid;
						break;
					}
					case 383:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Error accessing a Query. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Accessing Query");
							Information.Err().Clear();
							break;
						default:
							goto end_IL_0001_2;
						}
						break;
					}
					num4 = ((num4 >= GridMQ2.RowCount - 1) ? (GridMQ2.RowCount - 1) : (GridMQ2.CurrentCell.RowIndex + 1));
					Number_Grid2((short)num4);
					Init_Idx();
					break;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 383;
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

	private void OptRun0_CheckedChanged(object sender, EventArgs e)
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
				Resize_OptRun();
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

	private void mnuMQSave_Click(object sender, EventArgs e)
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
					SaveScript(1, "", 0, IsPacked: true);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Init_Idx();
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

	private void mnuMQSaveAS_Click(object sender, EventArgs e)
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
					SaveScript(2, "", 0, IsPacked: true);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Init_Idx();
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

	private void cmdSaveScripts_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyErr = default(string);
		while (true)
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
				case 209:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_001f;
						case 5:
							goto IL_0027;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0070;
						case 8:
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0027:
					num2 = 5;
					SetUsePyEnginePMQ("G");
					goto IL_0035;
					IL_0035:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) != 0 && (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "Y", TextCompare: false) != 0 || !BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1)))
					{
						break;
					}
					goto IL_0070;
					IL_001f:
					num2 = 4;
					MyErr = "";
					goto IL_0027;
					IL_0070:
					num2 = 7;
					PrepSaveSHGUI(0, TxtDir.Text);
					break;
					IL_000b:
					num2 = 2;
					if (BuildForm.Test_for_PPQ("save"))
					{
						goto end_IL_0001_3;
					}
					goto IL_001f;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				SetUsePyEnginePMQ("U");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 209;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdSaveAddScripts_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string MyErr = default(string);
		while (true)
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
				case 209:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_001f;
						case 5:
							goto IL_0027;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0070;
						case 8:
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0027:
					num2 = 5;
					SetUsePyEnginePMQ("G");
					goto IL_0035;
					IL_0035:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) != 0 && (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "Y", TextCompare: false) != 0 || !BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1)))
					{
						break;
					}
					goto IL_0070;
					IL_001f:
					num2 = 4;
					MyErr = "";
					goto IL_0027;
					IL_0070:
					num2 = 7;
					PrepSaveSHGUI(1, TxtDir.Text);
					break;
					IL_000b:
					num2 = 2;
					if (BuildForm.Test_for_PPQ("save"))
					{
						goto end_IL_0001_3;
					}
					goto IL_001f;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				SetUsePyEnginePMQ("U");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 209;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridMQ2_DragDrop(object sender, DragEventArgs e)
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
				case 70:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
				DataGridView MyGrid = GridMQ2;
				GridModule.GridDragDrop(ref MyGrid, ref e, DoColor: true);
				GridMQ2 = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 70;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridMQ2_DragOver(object sender, DragEventArgs e)
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
					DataGridView MyGrid;
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
							int num4 = unchecked(num + 1);
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
								goto IL_0078;
							case 6:
								goto IL_0099;
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
						IL_0026:
						num2 = 3;
						if (e.Y <= PointToScreen(new Point(GridMQ2.Location.X, GridMQ2.Location.Y)).Y + 30)
						{
							goto IL_0078;
						}
						goto IL_0099;
						IL_0078:
						num2 = 4;
						GridMQ2.FirstDisplayedScrollingRowIndex -= 1;
						goto end_IL_0001_3;
						IL_000b:
						num2 = 2;
						MyGrid = GridMQ2;
						GridModule.GridDragOver(ref MyGrid, ref e);
						GridMQ2 = MyGrid;
						goto IL_0026;
						IL_0099:
						num2 = 6;
						if (e.Y < PointToScreen(new Point(GridMQ2.Location.X + GridMQ2.Width, GridMQ2.Location.Y + GridMQ2.Height)).Y - 55)
						{
							goto end_IL_0001_3;
						}
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 7;
					GridMQ2.FirstDisplayedScrollingRowIndex += 1;
					break;
				}
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

	private void GridMQ2_MouseDown(object sender, MouseEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		Point position = default(Point);
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
							goto IL_001e;
						case 4:
							goto IL_0033;
						case 6:
							goto IL_004d;
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
					IL_001e:
					num2 = 3;
					position = new Point(e.X, e.Y);
					goto IL_0033;
					IL_0033:
					num2 = 4;
					ContextMenuGridMQ2.Show(GridMQ2, position);
					break;
					IL_000b:
					num2 = 2;
					if (e.Button == MouseButtons.Right)
					{
						goto IL_001e;
					}
					goto IL_004d;
					IL_004d:
					num2 = 6;
					MyGrid = GridMQ2;
					GridModule.InitiateDragDrop(ref MyGrid, ref e);
					GridMQ2 = MyGrid;
					break;
					end_IL_0001_2:
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

	private void cmdInsert_Click(object sender, EventArgs e)
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
				InsertARow();
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

	private void mnuSaveExeAs_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string tmpFile = default(string);
		string MyErr = default(string);
		while (true)
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
				case 446:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0022;
						case 5:
							goto IL_002a;
						case 6:
							goto IL_0038;
						case 7:
							goto IL_0076;
						case 8:
							goto IL_007f;
						case 9:
							goto IL_00af;
						case 11:
							goto IL_00d5;
						case 10:
						case 12:
						case 13:
							goto IL_00f8;
						case 14:
							goto IL_0123;
						case 15:
							goto IL_013c;
						case 16:
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_013c:
					num2 = 15;
					PrepSaveSHGUI(0, tmpFile, IsCustom: true);
					break;
					IL_00d5:
					num2 = 11;
					BuildForm.FileOpenSave("S", "", "zip", "Create Exe Job", Globals_Renamed.MyPCDir);
					goto IL_00f8;
					IL_007f:
					num2 = 8;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) == 0)
					{
						goto IL_00af;
					}
					goto IL_00d5;
					IL_00af:
					num2 = 9;
					BuildForm.FileOpenSave("S", "", "exe", "Create Exe Job", Globals_Renamed.MyPCDir);
					goto IL_00f8;
					IL_000b:
					num2 = 2;
					if (BuildForm.Test_for_PPQ("save"))
					{
						goto end_IL_0001_3;
					}
					goto IL_0022;
					IL_0022:
					num2 = 4;
					MyErr = "";
					goto IL_002a;
					IL_002a:
					num2 = 5;
					SetUsePyEnginePMQ("G");
					goto IL_0038;
					IL_0038:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) != 0 && (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "Y", TextCompare: false) != 0 || !BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1)))
					{
						break;
					}
					goto IL_0076;
					IL_00f8:
					num2 = 13;
					if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0123;
					IL_0123:
					num2 = 14;
					tmpFile = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_013c;
					IL_0076:
					num2 = 7;
					tmpFile = "";
					goto IL_007f;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				SetUsePyEnginePMQ("U");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 446;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void mnuSetQDir_Click(object sender, EventArgs e)
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
					if (!mnuSetQDir.Checked)
					{
						break;
					}
					goto IL_001c;
					IL_001c:
					num2 = 3;
					MyDefaultDir = Globals_Renamed.gQueryDir;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				MyDefaultDir = Globals_Renamed.MyPCDir;
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

	private void mnuMQSHGUI_Click(object sender, EventArgs e)
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

	private void GridMQ2_KeyDown(object sender, KeyEventArgs e)
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
					cmdDelete_Click(cmdDelete, new EventArgs());
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

	private void mnuPySH_Click(object sender, EventArgs e)
	{
		PMQ_Use_Python_Engine("0");
	}
}

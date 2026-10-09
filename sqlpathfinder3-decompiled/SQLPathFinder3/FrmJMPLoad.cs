using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmJMPLoad : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridDataType")]
	private DataGridView _GridDataType;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdBrowse")]
	private Button _CmdBrowse;

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
	[AccessedThroughProperty("CmdRefresh")]
	private Button _CmdRefresh;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuConvNum")]
	private ToolStripMenuItem _mnuConvNum;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuConvChar")]
	private ToolStripMenuItem _mnuConvChar;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuConvInt")]
	private ToolStripMenuItem _mnuConvInt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuConvDT")]
	private ToolStripMenuItem _mnuConvDT;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuConvdef")]
	private ToolStripMenuItem _mnuConvdef;

	public string f_CSVColumns;

	public string f_InFile;

	public string f_DelTable;

	public string f_TblName;

	public string f_ScriptType;

	public string f_DataType;

	public bool f_ColCaseInsensitive;

	private bool f_Ignore;

	internal virtual DataGridView GridDataType
	{
		[CompilerGenerated]
		get
		{
			return _GridDataType;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridDataType_CellValueChanged;
			DataGridView dataGridView = _GridDataType;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged -= value2;
			}
			_GridDataType = value;
			dataGridView = _GridDataType;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtInFile")]
	internal virtual TextBox TxtInFile
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

	internal virtual Button CmdBrowse
	{
		[CompilerGenerated]
		get
		{
			return _CmdBrowse;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdBrowse_Click;
			Button button = _CmdBrowse;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdBrowse = value;
			button = _CmdBrowse;
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

	[field: AccessedThroughProperty("OpenFileDialog1")]
	internal virtual OpenFileDialog OpenFileDialog1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdRefresh
	{
		[CompilerGenerated]
		get
		{
			return _CmdRefresh;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdRefresh_Click;
			Button button = _CmdRefresh;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdRefresh = value;
			button = _CmdRefresh;
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

	[field: AccessedThroughProperty("ChkDelTbl")]
	internal virtual CheckBox ChkDelTbl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtTblName")]
	internal virtual TextBox TxtTblName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblTblName")]
	internal virtual Label LblTblName
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

	[field: AccessedThroughProperty("mnuOptions")]
	internal virtual ToolStripMenuItem mnuOptions
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuDataTypes")]
	internal virtual ToolStripMenuItem mnuDataTypes
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("contextconvert")]
	internal virtual ContextMenuStrip contextconvert
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuConvNum
	{
		[CompilerGenerated]
		get
		{
			return _mnuConvNum;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConvNum_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuConvNum;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuConvNum = value;
			toolStripMenuItem = _mnuConvNum;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuConvChar
	{
		[CompilerGenerated]
		get
		{
			return _mnuConvChar;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConvNum_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuConvChar;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuConvChar = value;
			toolStripMenuItem = _mnuConvChar;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuConvInt
	{
		[CompilerGenerated]
		get
		{
			return _mnuConvInt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConvNum_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuConvInt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuConvInt = value;
			toolStripMenuItem = _mnuConvInt;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuConvDT
	{
		[CompilerGenerated]
		get
		{
			return _mnuConvDT;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConvNum_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuConvDT;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuConvDT = value;
			toolStripMenuItem = _mnuConvDT;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuConvdef
	{
		[CompilerGenerated]
		get
		{
			return _mnuConvdef;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuConvNum_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuConvdef;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuConvdef = value;
			toolStripMenuItem = _mnuConvdef;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("optweb")]
	internal virtual RadioButton optweb
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("optcsv")]
	internal virtual RadioButton optcsv
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

	[field: AccessedThroughProperty("ColDT")]
	internal virtual DataGridViewComboBoxColumn ColDT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColModel")]
	internal virtual DataGridViewComboBoxColumn ColModel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmJMPLoad()
	{
		base.Load += FrmJMPLoad_Load;
		base.Resize += FrmJMPLoad_Resize;
		f_CSVColumns = "";
		f_InFile = "";
		f_DelTable = "";
		f_TblName = "";
		f_ScriptType = "";
		f_DataType = "N";
		f_ColCaseInsensitive = false;
		f_Ignore = true;
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
		this.GridDataType = new System.Windows.Forms.DataGridView();
		this.Columns = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColDT = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColModel = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.contextconvert = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuConvChar = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuConvDT = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuConvdef = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuConvInt = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuConvNum = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtInFile = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.CmdBrowse = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.CmdRefresh = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ChkDelTbl = new System.Windows.Forms.CheckBox();
		this.TxtTblName = new System.Windows.Forms.TextBox();
		this.LblTblName = new System.Windows.Forms.Label();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuDataTypes = new System.Windows.Forms.ToolStripMenuItem();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.optweb = new System.Windows.Forms.RadioButton();
		this.optcsv = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)this.GridDataType).BeginInit();
		this.contextconvert.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.GridDataType.AllowUserToAddRows = false;
		this.GridDataType.AllowUserToDeleteRows = false;
		this.GridDataType.AllowUserToResizeColumns = false;
		this.GridDataType.AllowUserToResizeRows = false;
		this.GridDataType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridDataType.BackgroundColor = System.Drawing.Color.White;
		this.GridDataType.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
		this.GridDataType.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		this.GridDataType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridDataType.Columns.AddRange(this.Columns, this.ColDT, this.ColModel);
		this.GridDataType.ContextMenuStrip = this.contextconvert;
		this.GridDataType.Location = new System.Drawing.Point(3, 277);
		this.GridDataType.Margin = new System.Windows.Forms.Padding(4);
		this.GridDataType.Name = "GridDataType";
		this.GridDataType.RowHeadersWidth = 70;
		this.GridDataType.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.GridDataType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.GridDataType.Size = new System.Drawing.Size(700, 298);
		this.GridDataType.StandardTab = true;
		this.GridDataType.TabIndex = 6;
		this.Columns.HeaderText = "Columns";
		this.Columns.MinimumWidth = 6;
		this.Columns.Name = "Columns";
		this.Columns.ReadOnly = true;
		this.Columns.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Columns.Width = 150;
		this.ColDT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.ColDT.HeaderText = "Data Type";
		this.ColDT.Items.AddRange("Default", "Character", "Numeric");
		this.ColDT.MinimumWidth = 6;
		this.ColDT.Name = "ColDT";
		this.ColDT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ColDT.Width = 150;
		this.ColModel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.ColModel.HeaderText = "Modeling Type";
		this.ColModel.Items.AddRange("Default", "Continuous", "Nominal", "Ordinal");
		this.ColModel.MinimumWidth = 6;
		this.ColModel.Name = "ColModel";
		this.ColModel.Width = 150;
		this.contextconvert.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextconvert.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mnuConvChar, this.mnuConvDT, this.mnuConvdef, this.mnuConvInt, this.mnuConvNum });
		this.contextconvert.Name = "ContextMenuStrip1";
		this.contextconvert.Size = new System.Drawing.Size(276, 124);
		this.mnuConvChar.Name = "mnuConvChar";
		this.mnuConvChar.Size = new System.Drawing.Size(275, 24);
		this.mnuConvChar.Tag = "character";
		this.mnuConvChar.Text = "Convert Columns to Character";
		this.mnuConvDT.Name = "mnuConvDT";
		this.mnuConvDT.Size = new System.Drawing.Size(275, 24);
		this.mnuConvDT.Tag = "datetime";
		this.mnuConvDT.Text = "Convert Columns to Datetime";
		this.mnuConvdef.Name = "mnuConvdef";
		this.mnuConvdef.Size = new System.Drawing.Size(275, 24);
		this.mnuConvdef.Tag = "default";
		this.mnuConvdef.Text = "Convert Columns to Default";
		this.mnuConvInt.Name = "mnuConvInt";
		this.mnuConvInt.Size = new System.Drawing.Size(275, 24);
		this.mnuConvInt.Tag = "integer";
		this.mnuConvInt.Text = "Convert Columns to Integer";
		this.mnuConvNum.Name = "mnuConvNum";
		this.mnuConvNum.Size = new System.Drawing.Size(275, 24);
		this.mnuConvNum.Tag = "numeric";
		this.mnuConvNum.Text = "Convert Columns to Numeric";
		this.TxtInFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtInFile.Location = new System.Drawing.Point(8, 86);
		this.TxtInFile.Margin = new System.Windows.Forms.Padding(4);
		this.TxtInFile.Name = "TxtInFile";
		this.TxtInFile.Size = new System.Drawing.Size(529, 22);
		this.TxtInFile.TabIndex = 0;
		this.TxtInFile.TabStop = false;
		this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Label1.Location = new System.Drawing.Point(8, 36);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(695, 39);
		this.Label1.TabIndex = 2;
		this.Label1.Text = "Click Browse to select file to load:";
		this.CmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdBrowse.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdBrowse.ImageIndex = 2;
		this.CmdBrowse.Location = new System.Drawing.Point(551, 86);
		this.CmdBrowse.Margin = new System.Windows.Forms.Padding(4);
		this.CmdBrowse.Name = "CmdBrowse";
		this.CmdBrowse.Size = new System.Drawing.Size(73, 46);
		this.CmdBrowse.TabIndex = 2;
		this.CmdBrowse.Text = "Browse";
		this.CmdBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdBrowse, "Locate a File");
		this.CmdBrowse.UseVisualStyleBackColor = true;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdOK.ImageIndex = 1;
		this.CmdOK.Location = new System.Drawing.Point(627, 86);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(73, 46);
		this.CmdOK.TabIndex = 7;
		this.CmdOK.Text = "OK";
		this.CmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdOK, "Commit Changes");
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 0;
		this.CmdCancel.Location = new System.Drawing.Point(627, 134);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(73, 46);
		this.CmdCancel.TabIndex = 8;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdCancel, "Cancel Changes");
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.OpenFileDialog1.FileName = "OpenFileDialog1";
		this.CmdRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdRefresh.ImageIndex = 3;
		this.CmdRefresh.Location = new System.Drawing.Point(551, 134);
		this.CmdRefresh.Margin = new System.Windows.Forms.Padding(4);
		this.CmdRefresh.Name = "CmdRefresh";
		this.CmdRefresh.Size = new System.Drawing.Size(73, 46);
		this.CmdRefresh.TabIndex = 3;
		this.CmdRefresh.Text = "Columns";
		this.CmdRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.ToolTip1.SetToolTip(this.CmdRefresh, "Refresh Column Headers");
		this.CmdRefresh.UseVisualStyleBackColor = true;
		this.ChkDelTbl.AutoSize = true;
		this.ChkDelTbl.Checked = true;
		this.ChkDelTbl.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ChkDelTbl.Location = new System.Drawing.Point(8, 233);
		this.ChkDelTbl.Margin = new System.Windows.Forms.Padding(4);
		this.ChkDelTbl.Name = "ChkDelTbl";
		this.ChkDelTbl.Size = new System.Drawing.Size(251, 21);
		this.ChkDelTbl.TabIndex = 5;
		this.ChkDelTbl.Text = "Check to first delete table if it exists";
		this.ToolTip1.SetToolTip(this.ChkDelTbl, "Before loading, optionally remove table with the same name if it exists");
		this.ChkDelTbl.UseVisualStyleBackColor = true;
		this.TxtTblName.Location = new System.Drawing.Point(8, 201);
		this.TxtTblName.Margin = new System.Windows.Forms.Padding(4);
		this.TxtTblName.Name = "TxtTblName";
		this.TxtTblName.Size = new System.Drawing.Size(529, 22);
		this.TxtTblName.TabIndex = 4;
		this.LblTblName.AutoSize = true;
		this.LblTblName.Location = new System.Drawing.Point(8, 181);
		this.LblTblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTblName.Name = "LblTblName";
		this.LblTblName.Size = new System.Drawing.Size(151, 17);
		this.LblTblName.TabIndex = 9;
		this.LblTblName.Text = "Table Name to Create:";
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuOptions });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Size = new System.Drawing.Size(707, 28);
		this.MenuStrip1.TabIndex = 10;
		this.MenuStrip1.Text = "MenuStrip1";
		this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuDataTypes });
		this.mnuOptions.Name = "mnuOptions";
		this.mnuOptions.Size = new System.Drawing.Size(116, 24);
		this.mnuOptions.Text = "Other Options";
		this.mnuDataTypes.CheckOnClick = true;
		this.mnuDataTypes.Name = "mnuDataTypes";
		this.mnuDataTypes.Size = new System.Drawing.Size(251, 26);
		this.mnuDataTypes.Text = "Add DataTypes on Load";
		this.GroupBox1.Controls.Add(this.optweb);
		this.GroupBox1.Controls.Add(this.optcsv);
		this.GroupBox1.Location = new System.Drawing.Point(8, 119);
		this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.GroupBox1.Size = new System.Drawing.Size(276, 44);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.optweb.AutoSize = true;
		this.optweb.Location = new System.Drawing.Point(115, 17);
		this.optweb.Margin = new System.Windows.Forms.Padding(4);
		this.optweb.Name = "optweb";
		this.optweb.Size = new System.Drawing.Size(58, 21);
		this.optweb.TabIndex = 3;
		this.optweb.Text = "Web";
		this.optweb.UseVisualStyleBackColor = true;
		this.optcsv.AutoSize = true;
		this.optcsv.Checked = true;
		this.optcsv.Location = new System.Drawing.Point(8, 17);
		this.optcsv.Margin = new System.Windows.Forms.Padding(4);
		this.optcsv.Name = "optcsv";
		this.optcsv.Size = new System.Drawing.Size(51, 21);
		this.optcsv.TabIndex = 2;
		this.optcsv.TabStop = true;
		this.optcsv.Text = "File";
		this.optcsv.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(707, 577);
		base.Controls.Add(this.GroupBox1);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.LblTblName);
		base.Controls.Add(this.TxtTblName);
		base.Controls.Add(this.ChkDelTbl);
		base.Controls.Add(this.CmdRefresh);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.CmdBrowse);
		base.Controls.Add(this.TxtInFile);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.GridDataType);
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmJMPLoad";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Load Data and Optionally set Data Types";
		((System.ComponentModel.ISupportInitialize)this.GridDataType).EndInit();
		this.contextconvert.ResumeLayout(false);
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
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
							goto IL_000f;
						case 4:
							goto IL_0031;
						case 5:
							goto IL_004b;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					num5 = checked((int)Math.Round((double)(GridDataType.Width - 95) / 3.0));
					goto IL_0031;
					IL_0031:
					num2 = 4;
					GridDataType.Columns[0].Width = num5;
					goto IL_004b;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_004b:
					num2 = 5;
					GridDataType.Columns[1].Width = num5;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				GridDataType.Columns[2].Width = num5;
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

	public void GetHeaders(string Line1, string MyFile)
	{
		int num = 0;
		string text = "";
		string text2 = "";
		int num2 = 0;
		int num3 = 0;
		string text3 = "";
		string text4 = "";
		string text5 = "";
		num3 = Strings.InStrRev(MyFile, ".");
		checked
		{
			text = ((num3 == 0) ? "" : Strings.UCase(Strings.Mid(MyFile, num3 + 1)));
			if (Operators.CompareString(Strings.UCase(text), "JMP", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text), "RDATA", TextCompare: false) == 0)
			{
				return;
			}
			string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
			DynArray.Initialize();
			text2 = BuildForm.GetFileDLM("A." + text);
			num = General_Procedures.ParseAndFillArray(Line1, text2, ref DynArray);
			int num4 = num;
			for (num2 = 1; num2 <= num4; num2++)
			{
				text4 = DynArray[num2];
				text3 = "";
				int num5 = Strings.Len(text4);
				for (int i = 1; i <= num5; i++)
				{
					text5 = Strings.Mid(text4, i, 1);
					if (Operators.CompareString(text5, "\"", TextCompare: false) != 0)
					{
						text3 += text5;
					}
				}
				if (f_ColCaseInsensitive)
				{
					text3 = Strings.LCase(text3);
				}
				GridDataType.Rows.Add(Strings.Trim(text3), "Default", "Default");
				GridDataType.Rows[num2 - 1].HeaderCell.Value = num2.ToString();
			}
			DynArray = null;
		}
	}

	public void Integrate_Columns()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num7 = default(int);
		string text3 = default(string);
		int num8 = default(int);
		string right = default(string);
		bool flag = default(bool);
		string[] array = default(string[]);
		int num9 = default(int);
		int num10 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Type typeFromHandle;
					object[] obj;
					bool[] obj2;
					object left;
					Type typeFromHandle2;
					object[] obj3;
					DataGridViewCell dataGridViewCell;
					object[] array2;
					bool[] obj4;
					bool[] array3;
					object left2;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1584:
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
								goto IL_0031;
							case 5:
								goto IL_0035;
							case 6:
								goto IL_003a;
							case 7:
								goto IL_0043;
							case 8:
								goto IL_004c;
							case 9:
								goto IL_0051;
							case 10:
								goto IL_005b;
							case 11:
								goto IL_0065;
							case 12:
								goto IL_006b;
							case 13:
								goto IL_0071;
							case 14:
								goto IL_0088;
							case 15:
								goto IL_009f;
							case 16:
								goto IL_00a5;
							case 17:
								goto IL_00b9;
							case 18:
								goto IL_00c7;
							case 19:
								goto IL_00d9;
							case 20:
								goto IL_00ea;
							case 21:
								goto IL_00fb;
							case 22:
								goto IL_010c;
							case 23:
								goto IL_0125;
							case 25:
								goto IL_0132;
							case 26:
								goto IL_014b;
							case 28:
								goto IL_0158;
							case 29:
								goto IL_0171;
							case 31:
								goto IL_017f;
							case 24:
							case 27:
							case 30:
							case 32:
							case 33:
								goto IL_018a;
							case 34:
								goto IL_01aa;
							case 35:
								goto IL_01c3;
							case 37:
								goto IL_01d0;
							case 38:
								goto IL_01e9;
							case 40:
								goto IL_01f6;
							case 41:
								goto IL_020f;
							case 43:
								goto IL_021d;
							case 46:
								goto IL_022c;
							case 36:
							case 39:
							case 42:
							case 44:
							case 45:
							case 47:
							case 48:
								goto IL_0237;
							case 49:
								goto IL_024d;
							case 50:
								goto IL_0259;
							case 51:
								goto IL_0267;
							case 52:
								goto IL_02ea;
							case 53:
								goto IL_0311;
							case 55:
								goto IL_033f;
							case 56:
								goto IL_0359;
							case 57:
								goto IL_03dd;
							case 58:
								goto IL_0405;
							case 60:
							case 61:
								goto IL_0431;
							case 64:
								goto IL_044b;
							case 65:
								goto IL_045e;
							case 66:
								goto IL_0486;
							case 67:
								goto IL_04ae;
							case 68:
								goto IL_04d6;
							case 54:
							case 59:
							case 62:
							case 63:
							case 69:
							case 70:
							case 71:
								goto IL_04e2;
							case 72:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 3:
							case 73:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0431:
						num2 = 61;
						num5++;
						goto IL_043a;
						IL_044b:
						num2 = 64;
						GridDataType.RowCount = num6 + 1;
						goto IL_045e;
						IL_0405:
						num2 = 58;
						GridDataType.Rows[num5].Cells[2].Value = text;
						goto IL_04e2;
						IL_045e:
						num2 = 65;
						GridDataType.Rows[num6].Cells[0].Value = text2;
						goto IL_0486;
						IL_000b:
						num2 = 2;
						if (Operators.CompareString(Strings.Trim(f_CSVColumns), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0031;
						IL_0031:
						num2 = 4;
						num7 = 0;
						goto IL_0035;
						IL_0035:
						num2 = 5;
						num5 = 0;
						goto IL_003a;
						IL_003a:
						num2 = 6;
						text2 = "";
						goto IL_0043;
						IL_0043:
						num2 = 7;
						text3 = "";
						goto IL_004c;
						IL_004c:
						num2 = 8;
						num8 = 0;
						goto IL_0051;
						IL_0051:
						num2 = 9;
						text = "";
						goto IL_005b;
						IL_005b:
						num2 = 10;
						right = "";
						goto IL_0065;
						IL_0065:
						num2 = 11;
						num6 = 0;
						goto IL_006b;
						IL_006b:
						num2 = 12;
						flag = false;
						goto IL_0071;
						IL_0071:
						num2 = 13;
						array = Strings.Split(f_CSVColumns, ",");
						goto IL_0088;
						IL_0088:
						num2 = 14;
						if (GridDataType.RowCount > 0)
						{
							goto IL_009f;
						}
						goto IL_00a5;
						IL_009f:
						num2 = 15;
						flag = true;
						goto IL_00a5;
						IL_00a5:
						num2 = 16;
						num9 = Information.UBound(array);
						num7 = 0;
						goto IL_04e9;
						IL_04e9:
						if (num7 > num9)
						{
							break;
						}
						goto IL_00b9;
						IL_00b9:
						num2 = 17;
						text2 = Strings.Trim(array[num7]);
						goto IL_00c7;
						IL_00c7:
						num2 = 18;
						num8 = Strings.InStr(text2, " (");
						goto IL_00d9;
						IL_00d9:
						num2 = 19;
						if (num8 != 0)
						{
							goto IL_00ea;
						}
						goto IL_04e2;
						IL_00ea:
						num2 = 20;
						text3 = Strings.Mid(text2, num8 + 2, 1);
						goto IL_00fb;
						IL_00fb:
						num2 = 21;
						text = Strings.Mid(text2, num8 + 3, 1);
						goto IL_010c;
						IL_010c:
						num2 = 22;
						if (Operators.CompareString(text3, "C", TextCompare: false) == 0)
						{
							goto IL_0125;
						}
						goto IL_0132;
						IL_0125:
						num2 = 23;
						text3 = "Character";
						goto IL_018a;
						IL_0132:
						num2 = 25;
						if (Operators.CompareString(text3, "S", TextCompare: false) == 0)
						{
							goto IL_014b;
						}
						goto IL_0158;
						IL_014b:
						num2 = 26;
						text3 = "SPF-Date";
						goto IL_018a;
						IL_0158:
						num2 = 28;
						if (Operators.CompareString(text3, "D", TextCompare: false) == 0)
						{
							goto IL_0171;
						}
						goto IL_017f;
						IL_0171:
						num2 = 29;
						text3 = "Datetime";
						goto IL_018a;
						IL_017f:
						num2 = 31;
						text3 = "Numeric";
						goto IL_018a;
						IL_018a:
						num2 = 33;
						if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0)
						{
							goto IL_01aa;
						}
						goto IL_022c;
						IL_01aa:
						num2 = 34;
						if (Operators.CompareString(text, "C", TextCompare: false) == 0)
						{
							goto IL_01c3;
						}
						goto IL_01d0;
						IL_01c3:
						num2 = 35;
						text = "Continuous";
						goto IL_0237;
						IL_01d0:
						num2 = 37;
						if (Operators.CompareString(text, "O", TextCompare: false) == 0)
						{
							goto IL_01e9;
						}
						goto IL_01f6;
						IL_01e9:
						num2 = 38;
						text = "Ordinal";
						goto IL_0237;
						IL_01f6:
						num2 = 40;
						if (Operators.CompareString(text, "N", TextCompare: false) == 0)
						{
							goto IL_020f;
						}
						goto IL_021d;
						IL_020f:
						num2 = 41;
						text = "Nominal";
						goto IL_0237;
						IL_021d:
						num2 = 43;
						text = "Default";
						goto IL_0237;
						IL_022c:
						num2 = 46;
						text = "Default";
						goto IL_0237;
						IL_0237:
						num2 = 48;
						text2 = Strings.Trim(Strings.Mid(text2, 1, num8 - 1));
						goto IL_024d;
						IL_024d:
						num2 = 49;
						right = Strings.UCase(text2);
						goto IL_0259;
						IL_0259:
						num2 = 50;
						if (flag)
						{
							goto IL_0267;
						}
						goto IL_044b;
						IL_0267:
						num2 = 51;
						typeFromHandle = typeof(Strings);
						obj = new object[1] { (dataGridViewCell = GridDataType.Rows[num7].Cells[0]).Value };
						array2 = obj;
						obj2 = new bool[1] { true };
						array3 = obj2;
						left = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
						if (array3[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
						}
						if (Operators.ConditionalCompareObjectEqual(left, right, TextCompare: false))
						{
							goto IL_02ea;
						}
						goto IL_033f;
						IL_04ae:
						num2 = 67;
						GridDataType.Rows[num6].Cells[2].Value = text;
						goto IL_04d6;
						IL_0486:
						num2 = 66;
						GridDataType.Rows[num6].Cells[1].Value = text3;
						goto IL_04ae;
						IL_02ea:
						num2 = 52;
						GridDataType.Rows[num7].Cells[1].Value = text3;
						goto IL_0311;
						IL_0311:
						num2 = 53;
						GridDataType.Rows[num7].Cells[2].Value = text;
						goto IL_04e2;
						IL_033f:
						num2 = 55;
						num10 = GridDataType.RowCount - 1;
						num5 = 0;
						goto IL_043a;
						IL_043a:
						if (num5 <= num10)
						{
							goto IL_0359;
						}
						goto IL_04e2;
						IL_0359:
						num2 = 56;
						typeFromHandle2 = typeof(Strings);
						obj3 = new object[1] { (dataGridViewCell = GridDataType.Rows[num5].Cells[0]).Value };
						array2 = obj3;
						obj4 = new bool[1] { true };
						array3 = obj4;
						left2 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj3, null, null, obj4);
						if (array3[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
						}
						if (Operators.ConditionalCompareObjectEqual(left2, right, TextCompare: false))
						{
							goto IL_03dd;
						}
						goto IL_0431;
						IL_04e2:
						num2 = 71;
						num7++;
						goto IL_04e9;
						IL_04d6:
						num2 = 68;
						num6++;
						goto IL_04e2;
						IL_03dd:
						num2 = 57;
						GridDataType.Rows[num5].Cells[1].Value = text3;
						goto IL_0405;
						end_IL_0001_2:
						break;
					}
					num2 = 72;
					array = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj5) when (obj5 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj5);
				try0001_dispatch = 1584;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private string Prep_Columns()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string right = default(string);
		int num5 = default(int);
		string text = default(string);
		string result = default(string);
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
				case 493:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0043;
						case 8:
							goto IL_005c;
						case 9:
							goto IL_0094;
						case 10:
							goto IL_00c7;
						case 11:
						case 12:
							goto IL_0154;
						case 13:
							goto IL_0166;
						case 14:
							goto IL_017f;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0094:
					num2 = 9;
					right = Strings.Mid(Conversions.ToString(GridDataType.Rows[num5].Cells[2].Value), 1, 1);
					goto IL_00c7;
					IL_00c7:
					num2 = 10;
					text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text + ",", GridDataType.Rows[num5].Cells[0].Value), " ("), Strings.Mid(Conversions.ToString(GridDataType.Rows[num5].Cells[1].Value), 1, 1)), right), ")"));
					goto IL_0154;
					IL_005c:
					num2 = 8;
					if (Operators.ConditionalCompareObjectNotEqual(GridDataType.Rows[num5].Cells[1].Value, "Default", TextCompare: false))
					{
						goto IL_0094;
					}
					goto IL_0154;
					IL_0154:
					num2 = 12;
					num5 = checked(num5 + 1);
					goto IL_015d;
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
					text = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					right = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					if (GridDataType.RowCount <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0043;
					IL_0043:
					num2 = 7;
					num6 = checked(GridDataType.RowCount - 1);
					num5 = 0;
					goto IL_015d;
					IL_015d:
					if (num5 <= num6)
					{
						goto IL_005c;
					}
					goto IL_0166;
					IL_0166:
					num2 = 13;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_017f;
					IL_017f:
					num2 = 14;
					text = Strings.Mid(text, 2);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				result = text;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 493;
				continue;
			}
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

	public void Init_Headers(string MyFile, string MyTName)
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
							goto IL_001f;
						case 4:
							goto IL_002e;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_004d;
						case 8:
							goto IL_0065;
						case 9:
							goto IL_007c;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 10:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004d:
					num2 = 6;
					TxtTblName.Text = BuildChart.Derive_Chart_Table(ref MyFile);
					goto end_IL_0001_3;
					IL_0065:
					num2 = 8;
					if (Operators.CompareString(MyTName, "N/A", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_007c;
					IL_0038:
					num2 = 5;
					if (Operators.CompareString(MyTName, "", TextCompare: false) == 0)
					{
						goto IL_004d;
					}
					goto IL_0065;
					IL_007c:
					num2 = 9;
					TxtTblName.Text = "";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					MyFile = BuildForm.Strip_Add_MyPCDir("S", MyFile);
					goto IL_001f;
					IL_001f:
					num2 = 3;
					TxtInFile.Text = MyFile;
					goto IL_002e;
					IL_002e:
					num2 = 4;
					GetCSVHeadersList(MyFile);
					goto IL_0038;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				TxtTblName.Text = MyTName;
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

	private void GetCSVHeadersList(string CurrentFile)
	{
		string line = "";
		int num = 0;
		CurrentFile = Strings.Trim(CurrentFile);
		if (Operators.CompareString(CurrentFile, "", TextCompare: false) == 0)
		{
			return;
		}
		CurrentFile = BuildForm.Replace_Globals(CurrentFile, 1);
		CurrentFile = BuildForm.Strip_Add_MyPCDir("A", CurrentFile);
		GridDataType.Rows.Clear();
		if (LikeOperator.LikeString(Strings.UCase(CurrentFile), "*.JMP", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(CurrentFile), "*.RDATA", CompareMethod.Binary))
		{
			return;
		}
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			Refresh();
			StreamReader streamReader = new StreamReader(CurrentFile);
			try
			{
				if (streamReader.Peek() != -1)
				{
					line = streamReader.ReadLine();
				}
				GetHeaders(line, CurrentFile);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error loading CSV file: " + CurrentFile + ". (" + ex2.Message + "). Please try another file.", MsgBoxStyle.Exclamation, "Error Loading CSV File");
				ProjectData.ClearProjectError();
			}
			finally
			{
				streamReader.Close();
				streamReader.Dispose();
				streamReader = null;
				Cursor.Current = Cursors.Default;
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex4.Message + "). Please try another file.", MsgBoxStyle.Exclamation, "Error Accessing CSV File");
			ProjectData.ClearProjectError();
		}
	}

	private void CmdBrowse_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myFile = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text;
				int num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 524:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0021;
						case 6:
							goto IL_0037;
						case 7:
							goto IL_004a;
						case 8:
							goto IL_005d;
						case 9:
							goto IL_0079;
						case 10:
							goto IL_008d;
						case 12:
							goto IL_00a5;
						case 13:
							goto IL_00b9;
						case 11:
						case 14:
						case 15:
							goto IL_00ce;
						case 16:
							goto IL_00de;
						case 17:
							goto IL_00ee;
						case 18:
							goto IL_00fe;
						case 19:
							goto IL_010e;
						case 20:
							goto IL_0128;
						case 21:
							goto IL_0137;
						case 22:
							goto IL_0141;
						case 23:
							goto IL_0151;
						case 26:
							goto IL_0160;
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 24:
						case 25:
						case 28:
						case 29:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0141:
					num2 = 22;
					Init_Headers(myFile, "");
					goto IL_0151;
					IL_0151:
					num2 = 23;
					f_Ignore = false;
					goto end_IL_0001_3;
					IL_0137:
					num2 = 21;
					f_Ignore = true;
					goto IL_0141;
					IL_0160:
					num2 = 26;
					if (!optweb.Checked)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					myFile = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					num5 = 0;
					goto IL_0021;
					IL_0021:
					num2 = 5;
					if (optcsv.Checked)
					{
						goto IL_0037;
					}
					goto IL_0160;
					IL_0037:
					num2 = 6;
					OpenFileDialog1.FileName = "";
					goto IL_004a;
					IL_004a:
					num2 = 7;
					OpenFileDialog1.InitialDirectory = Globals_Renamed.MyPCDir;
					goto IL_005d;
					IL_005d:
					num2 = 8;
					if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_0079;
					}
					goto IL_00a5;
					IL_0079:
					num2 = 9;
					OpenFileDialog1.Title = "Select a CSV/TAB or JMP File";
					goto IL_008d;
					IL_008d:
					num2 = 10;
					OpenFileDialog1.Filter = "CSV/TAB/JMP Files (*.csv;*.tab;*.jmp)|*.csv;*.CSV;*.tab;*.TAB;*.jmp;*.JMP|ASC Files (*.asc)|*.asc;*.ASC|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_00ce;
					IL_00a5:
					num2 = 12;
					OpenFileDialog1.Title = "Select a CSV/TAB or RDATA File";
					goto IL_00b9;
					IL_00b9:
					num2 = 13;
					OpenFileDialog1.Filter = "CSV/TAB/RDATA Files (*.csv;*.tab;*.RDATA)|*.csv;*.CSV;*.tab;*.TAB;*.rdata;*.RDATA|ASC Files (*.asc)|*.asc;*.ASC|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_00ce;
					IL_00ce:
					num2 = 15;
					OpenFileDialog1.FilterIndex = 1;
					goto IL_00de;
					IL_00de:
					num2 = 16;
					OpenFileDialog1.CheckPathExists = true;
					goto IL_00ee;
					IL_00ee:
					num2 = 17;
					OpenFileDialog1.CheckFileExists = true;
					goto IL_00fe;
					IL_00fe:
					num2 = 18;
					OpenFileDialog1.ShowReadOnly = false;
					goto IL_010e;
					IL_010e:
					num2 = 19;
					if (OpenFileDialog1.ShowDialog() == DialogResult.Cancel)
					{
						goto end_IL_0001_3;
					}
					goto IL_0128;
					IL_0128:
					num2 = 20;
					myFile = OpenFileDialog1.FileName;
					goto IL_0137;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				BuildForm.Invoke_IE("Index.htm");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 524;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
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

	private void CmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		string text = default(string);
		int num5 = default(int);
		bool flag = default(bool);
		bool flag2 = default(bool);
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
					case 1995:
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
								goto IL_0018;
							case 4:
								goto IL_0020;
							case 5:
								goto IL_0029;
							case 6:
								goto IL_002e;
							case 7:
								goto IL_0033;
							case 8:
								goto IL_0038;
							case 9:
								goto IL_004c;
							case 10:
								goto IL_0065;
							case 11:
								goto IL_007a;
							case 14:
								goto IL_0094;
							case 15:
								goto IL_00bf;
							case 16:
								goto IL_00d4;
							case 19:
								goto IL_00eb;
							case 20:
								goto IL_012b;
							case 21:
								goto IL_0140;
							case 13:
							case 18:
							case 23:
							case 24:
								goto IL_0155;
							case 25:
								goto IL_0169;
							case 27:
								goto IL_0176;
							case 26:
							case 28:
							case 29:
								goto IL_0180;
							case 30:
								goto IL_01a0;
							case 31:
								goto IL_01a6;
							case 32:
								goto IL_01c1;
							case 33:
								goto IL_01d8;
							case 34:
								goto IL_020e;
							case 35:
								goto IL_023a;
							case 37:
							case 38:
								goto IL_0244;
							case 36:
							case 39:
							case 40:
								goto IL_0255;
							case 41:
								goto IL_0263;
							case 45:
								goto IL_02b5;
							case 46:
								goto IL_02bb;
							case 47:
								goto IL_02c1;
							case 48:
								goto IL_02dc;
							case 49:
								goto IL_0311;
							case 50:
								goto IL_0317;
							case 52:
								goto IL_0349;
							case 53:
								goto IL_0363;
							case 54:
								goto IL_03a3;
							case 56:
								goto IL_03ac;
							case 57:
								goto IL_03e9;
							case 58:
								goto IL_0415;
							case 55:
							case 60:
							case 61:
								goto IL_041e;
							case 51:
							case 59:
							case 62:
							case 63:
							case 64:
								goto IL_0433;
							case 65:
								goto IL_0441;
							case 43:
							case 44:
							case 67:
							case 68:
							case 69:
								goto IL_048d;
							case 70:
								goto IL_0497;
							case 71:
								goto IL_04a2;
							case 72:
								goto IL_04bb;
							case 73:
								goto IL_04d8;
							case 74:
							case 75:
								goto IL_04ee;
							case 76:
								goto IL_050e;
							case 77:
								goto IL_0528;
							case 78:
								goto IL_0549;
							case 79:
								goto IL_0592;
							case 80:
							case 81:
								goto IL_05ad;
							case 82:
								goto IL_05bc;
							case 83:
								goto IL_05d0;
							case 85:
								goto IL_05e2;
							case 84:
							case 86:
							case 87:
								goto IL_05f1;
							case 88:
								goto IL_0605;
							case 90:
								goto IL_0617;
							case 89:
							case 91:
							case 92:
								goto IL_0626;
							case 93:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 12:
							case 17:
							case 22:
							case 42:
							case 66:
							case 94:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0592:
						num2 = 79;
						f_TblName = "T_" + f_TblName;
						goto IL_05ad;
						IL_05ad:
						num2 = 81;
						f_CSVColumns = Prep_Columns();
						goto IL_05bc;
						IL_0626:
						num2 = 92;
						Cursor.Current = Cursors.Default;
						break;
						IL_05bc:
						num2 = 82;
						if (ChkDelTbl.Checked)
						{
							goto IL_05d0;
						}
						goto IL_05e2;
						IL_000b:
						num2 = 2;
						Cursor.Current = Cursors.WaitCursor;
						goto IL_0018;
						IL_0018:
						num2 = 3;
						left = "N";
						goto IL_0020;
						IL_0020:
						num2 = 4;
						text = "";
						goto IL_0029;
						IL_0029:
						num2 = 5;
						num5 = 0;
						goto IL_002e;
						IL_002e:
						num2 = 6;
						flag = true;
						goto IL_0033;
						IL_0033:
						num2 = 7;
						flag2 = false;
						goto IL_0038;
						IL_0038:
						num2 = 8;
						text2 = Strings.Trim(TxtInFile.Text);
						goto IL_004c;
						IL_004c:
						num2 = 9;
						if (Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							goto IL_0065;
						}
						goto IL_0094;
						IL_0065:
						num2 = 10;
						Interaction.MsgBox("No File or Table was specified for loading", MsgBoxStyle.Exclamation, "Missing File");
						goto IL_007a;
						IL_007a:
						num2 = 11;
						TxtInFile.Focus();
						goto end_IL_0001_3;
						IL_0094:
						num2 = 14;
						if (optcsv.Checked && LikeOperator.LikeString(Strings.UCase(text2), "*HTTP*://*", CompareMethod.Binary))
						{
							goto IL_00bf;
						}
						goto IL_00eb;
						IL_05d0:
						num2 = 83;
						f_DelTable = "Y";
						goto IL_05f1;
						IL_00bf:
						num2 = 15;
						Interaction.MsgBox("A Web Address should not be specified for CSV/TAB file access.Please correct.", MsgBoxStyle.Exclamation, "Web Address Not Allowed");
						goto IL_00d4;
						IL_00d4:
						num2 = 16;
						TxtInFile.Focus();
						goto end_IL_0001_3;
						IL_00eb:
						num2 = 19;
						if (optweb.Checked && !LikeOperator.LikeString(text2, "*<<<*>>>", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(text2), "*HTTP*://*", CompareMethod.Binary))
						{
							goto IL_012b;
						}
						goto IL_0155;
						IL_05e2:
						num2 = 85;
						f_DelTable = "N";
						goto IL_05f1;
						IL_05f1:
						num2 = 87;
						if (mnuDataTypes.Checked)
						{
							goto IL_0605;
						}
						goto IL_0617;
						IL_012b:
						num2 = 20;
						Interaction.MsgBox("Web Access was specified but a web address was not specified. Please correct.", MsgBoxStyle.Exclamation, "Invalid Web Address");
						goto IL_0140;
						IL_0140:
						num2 = 21;
						TxtInFile.Focus();
						goto end_IL_0001_3;
						IL_0155:
						num2 = 24;
						if (mnuDataTypes.Checked)
						{
							goto IL_0169;
						}
						goto IL_0176;
						IL_0169:
						num2 = 25;
						left = "Y";
						goto IL_0180;
						IL_0176:
						num2 = 27;
						left = "N";
						goto IL_0180;
						IL_0180:
						num2 = 29;
						if (Operators.CompareString(f_ScriptType, "R", TextCompare: false) == 0)
						{
							goto IL_01a0;
						}
						goto IL_02b5;
						IL_01a0:
						num2 = 30;
						flag = true;
						goto IL_01a6;
						IL_01a6:
						num2 = 31;
						if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
						{
							goto IL_01c1;
						}
						goto IL_0255;
						IL_01c1:
						num2 = 32;
						num6 = GridDataType.RowCount - 1;
						num5 = 0;
						goto IL_024d;
						IL_024d:
						if (num5 <= num6)
						{
							goto IL_01d8;
						}
						goto IL_0255;
						IL_01d8:
						num2 = 33;
						if (Operators.ConditionalCompareObjectEqual(GridDataType.Rows[num5].Cells[1].Value, "Default", TextCompare: false))
						{
							goto IL_020e;
						}
						goto IL_0244;
						IL_020e:
						num2 = 34;
						text = Conversions.ToString(GridDataType.Rows[num5].Cells[0].Value);
						goto IL_023a;
						IL_023a:
						num2 = 35;
						flag = false;
						goto IL_0255;
						IL_0244:
						num2 = 38;
						num5++;
						goto IL_024d;
						IL_0255:
						num2 = 40;
						if (!flag)
						{
							goto IL_0263;
						}
						goto IL_048d;
						IL_0263:
						num2 = 41;
						Interaction.MsgBox("In order to assign Column DataTypes on load, every column must have a data type assigned. Column " + text + " in row " + Conversions.ToString(num5 + 1) + " does not have an assigned DataType", MsgBoxStyle.Exclamation, "Missing DataType");
						goto end_IL_0001_3;
						IL_02b5:
						num2 = 45;
						flag = true;
						goto IL_02bb;
						IL_02bb:
						num2 = 46;
						flag2 = false;
						goto IL_02c1;
						IL_02c1:
						num2 = 47;
						if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
						{
							goto IL_02dc;
						}
						goto IL_0433;
						IL_02dc:
						num2 = 48;
						if (Operators.ConditionalCompareObjectEqual(GridDataType.Rows[0].Cells[1].Value, "Default", TextCompare: false))
						{
							goto IL_0311;
						}
						goto IL_0349;
						IL_0311:
						num2 = 49;
						flag = false;
						goto IL_0317;
						IL_0317:
						num2 = 50;
						text = Conversions.ToString(GridDataType.Rows[0].Cells[0].Value);
						goto IL_0433;
						IL_0349:
						num2 = 52;
						num7 = GridDataType.RowCount - 1;
						num5 = 1;
						goto IL_0427;
						IL_0427:
						if (num5 <= num7)
						{
							goto IL_0363;
						}
						goto IL_0433;
						IL_0363:
						num2 = 53;
						if (Operators.ConditionalCompareObjectEqual(GridDataType.Rows[num5].Cells[1].Value, "Default", TextCompare: false) && !flag2)
						{
							goto IL_03a3;
						}
						goto IL_03ac;
						IL_0605:
						num2 = 88;
						f_DataType = "Y";
						goto IL_0626;
						IL_03a3:
						num2 = 54;
						flag2 = true;
						goto IL_041e;
						IL_03ac:
						num2 = 56;
						if (Operators.ConditionalCompareObjectNotEqual(GridDataType.Rows[num5].Cells[1].Value, "Default", TextCompare: false) && flag2)
						{
							goto IL_03e9;
						}
						goto IL_041e;
						IL_0617:
						num2 = 90;
						f_DataType = "N";
						goto IL_0626;
						IL_03e9:
						num2 = 57;
						text = Conversions.ToString(GridDataType.Rows[num5].Cells[0].Value);
						goto IL_0415;
						IL_0415:
						num2 = 58;
						flag = false;
						goto IL_0433;
						IL_041e:
						num2 = 61;
						num5++;
						goto IL_0427;
						IL_0433:
						num2 = 64;
						if (!flag)
						{
							goto IL_0441;
						}
						goto IL_048d;
						IL_0441:
						num2 = 65;
						Interaction.MsgBox("In order to assign Column DataTypes on load, a contiguous set of columns starting from the first column must have data types assigned. Column " + text + " in row " + Conversions.ToString(num5 + 1) + " is an exception.", MsgBoxStyle.Exclamation, "Missing DataType");
						goto end_IL_0001_3;
						IL_048d:
						num2 = 69;
						f_DataType = left;
						goto IL_0497;
						IL_0497:
						num2 = 70;
						f_InFile = text2;
						goto IL_04a2;
						IL_04a2:
						num2 = 71;
						f_TblName = Strings.Trim(TxtTblName.Text);
						goto IL_04bb;
						IL_04bb:
						num2 = 72;
						if (Operators.CompareString(f_TblName, "", TextCompare: false) == 0)
						{
							goto IL_04d8;
						}
						goto IL_04ee;
						IL_04d8:
						num2 = 73;
						f_TblName = BuildChart.Derive_Chart_Table(ref f_InFile);
						goto IL_04ee;
						IL_04ee:
						num2 = 75;
						if (Operators.CompareString(f_ScriptType, "R", TextCompare: false) == 0)
						{
							goto IL_050e;
						}
						goto IL_05ad;
						IL_050e:
						num2 = 76;
						f_TblName = General_Procedures.Replace_Special_Chars(f_TblName);
						goto IL_0528;
						IL_0528:
						num2 = 77;
						f_TblName = Strings.Replace(f_TblName, " ", "", 1, -1, CompareMethod.Text);
						goto IL_0549;
						IL_0549:
						num2 = 78;
						if (Operators.CompareString(Strings.Trim(f_TblName), "", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(f_TblName + " ", 1, 1), "_", TextCompare: false) == 0)
						{
							goto IL_0592;
						}
						goto IL_05ad;
						end_IL_0001_2:
						break;
					}
					num2 = 93;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1995;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmJMPLoad_Load(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text2 = default(string);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text;
				Button MyButton;
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
							goto IL_0021;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_0061;
						case 8:
							goto IL_0081;
						case 9:
							goto IL_00a1;
						case 10:
							goto IL_00be;
						case 11:
							goto IL_00d7;
						case 12:
							goto IL_00f0;
						case 13:
							goto IL_0104;
						case 14:
							goto IL_011d;
						case 16:
							goto IL_0134;
						case 17:
							goto IL_0151;
						case 18:
							goto IL_016a;
						case 19:
							goto IL_017a;
						case 20:
							goto IL_018a;
						case 21:
							goto IL_019a;
						case 15:
						case 22:
						case 23:
							goto IL_01ab;
						case 24:
							goto IL_01c2;
						case 25:
							goto IL_01de;
						case 26:
							goto IL_01ed;
						case 27:
							goto IL_020d;
						case 28:
							goto IL_0221;
						case 29:
							goto IL_0236;
						case 30:
							goto IL_0255;
						case 31:
							goto IL_0263;
						case 32:
							goto IL_0273;
						case 33:
							goto IL_028c;
						case 34:
						case 35:
							goto IL_02a0;
						case 36:
							goto IL_02b0;
						case 38:
							goto IL_02c2;
						case 37:
						case 39:
						case 40:
							goto IL_02d9;
						case 41:
						case 42:
							goto IL_02e5;
						case 43:
							goto IL_0302;
						case 44:
							goto IL_0312;
						case 45:
							goto IL_032f;
						case 46:
							goto IL_033f;
						case 47:
							goto IL_0355;
						case 48:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 49:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0355:
					num2 = 47;
					f_Ignore = false;
					break;
					IL_01c2:
					num2 = 24;
					GridDataType.Columns[num5].SortMode = DataGridViewColumnSortMode.NotSortable;
					goto IL_01de;
					IL_033f:
					num2 = 46;
					GridDataType.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
					goto IL_0355;
					IL_01de:
					num2 = 25;
					num5 = checked(num5 + 1);
					goto IL_01e7;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text2 = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					num5 = 0;
					goto IL_0021;
					IL_0021:
					num2 = 5;
					MyButton = CmdCancel;
					BuildForm.Set_Btn_Img(ref MyButton, "cancel");
					CmdCancel = MyButton;
					goto IL_0041;
					IL_0041:
					num2 = 6;
					MyButton = CmdOK;
					BuildForm.Set_Btn_Img(ref MyButton, "ok");
					CmdOK = MyButton;
					goto IL_0061;
					IL_0061:
					num2 = 7;
					MyButton = CmdBrowse;
					BuildForm.Set_Btn_Img(ref MyButton, "searchfolder");
					CmdBrowse = MyButton;
					goto IL_0081;
					IL_0081:
					num2 = 8;
					MyButton = CmdRefresh;
					BuildForm.Set_Btn_Img(ref MyButton, "refresh");
					CmdRefresh = MyButton;
					goto IL_00a1;
					IL_00a1:
					num2 = 9;
					if (Operators.CompareString(f_ScriptType, "R", TextCompare: false) == 0)
					{
						goto IL_00be;
					}
					goto IL_0134;
					IL_00be:
					num2 = 10;
					ColDT.Items.Add("Datetime");
					goto IL_00d7;
					IL_00d7:
					num2 = 11;
					ColDT.Items.Add("Integer");
					goto IL_00f0;
					IL_00f0:
					num2 = 12;
					ColModel.Items.Clear();
					goto IL_0104;
					IL_0104:
					num2 = 13;
					ColModel.Items.Add("Default");
					goto IL_011d;
					IL_011d:
					num2 = 14;
					Label1.Text = "Select csv, tab, RDATA (native R) file to load to an R Dataframe";
					goto IL_01ab;
					IL_0134:
					num2 = 16;
					if (Operators.CompareString(f_ScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_0151;
					}
					goto IL_01ab;
					IL_0151:
					num2 = 17;
					ColDT.Items.Add("SPF-Date");
					goto IL_016a;
					IL_016a:
					num2 = 18;
					mnuConvDT.Visible = false;
					goto IL_017a;
					IL_017a:
					num2 = 19;
					mnuConvInt.Visible = false;
					goto IL_018a;
					IL_018a:
					num2 = 20;
					mnuDataTypes.Visible = true;
					goto IL_019a;
					IL_019a:
					num2 = 21;
					mnuOptions.Visible = true;
					goto IL_01ab;
					IL_01ab:
					num2 = 23;
					num6 = checked(GridDataType.ColumnCount - 1);
					num5 = 0;
					goto IL_01e7;
					IL_01e7:
					if (num5 <= num6)
					{
						goto IL_01c2;
					}
					goto IL_01ed;
					IL_01ed:
					num2 = 26;
					if (Operators.CompareString(f_InFile, "", TextCompare: false) != 0)
					{
						goto IL_020d;
					}
					goto IL_02e5;
					IL_020d:
					num2 = 27;
					f_InFile = Strings.Trim(f_InFile);
					goto IL_0221;
					IL_0221:
					num2 = 28;
					TxtInFile.Text = f_InFile;
					goto IL_0236;
					IL_0236:
					num2 = 29;
					if (LikeOperator.LikeString(Strings.UCase(f_InFile), "HTTP*://*", CompareMethod.Binary))
					{
						goto IL_0255;
					}
					goto IL_02c2;
					IL_0255:
					num2 = 30;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0263;
					IL_0263:
					num2 = 31;
					text2 = BuildForm.Get_First_Line_Web(f_InFile);
					goto IL_0273;
					IL_0273:
					num2 = 32;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_028c;
					}
					goto IL_02a0;
					IL_028c:
					num2 = 33;
					GetHeaders(text2, f_InFile);
					goto IL_02a0;
					IL_02a0:
					num2 = 35;
					optweb.Checked = true;
					goto IL_02b0;
					IL_02b0:
					num2 = 36;
					Cursor.Current = Cursors.Default;
					goto IL_02d9;
					IL_02c2:
					num2 = 38;
					Init_Headers(f_InFile, f_TblName);
					goto IL_02d9;
					IL_02d9:
					num2 = 40;
					Integrate_Columns();
					goto IL_02e5;
					IL_02e5:
					num2 = 42;
					if (Operators.CompareString(f_DelTable, "N", TextCompare: false) == 0)
					{
						goto IL_0302;
					}
					goto IL_0312;
					IL_0302:
					num2 = 43;
					ChkDelTbl.Checked = false;
					goto IL_0312;
					IL_0312:
					num2 = 44;
					if (Operators.CompareString(f_DataType, "Y", TextCompare: false) == 0)
					{
						goto IL_032f;
					}
					goto IL_033f;
					IL_032f:
					num2 = 45;
					mnuDataTypes.Checked = true;
					goto IL_033f;
					end_IL_0001_2:
					break;
				}
				num2 = 48;
				Resize_Form();
				break;
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

	private void FrmJMPLoad_Resize(object sender, EventArgs e)
	{
		Resize_Form();
	}

	private void CmdRefresh_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num5 = default(int);
		string myTName = default(string);
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
				case 487:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_0037;
						case 7:
							goto IL_007a;
						case 8:
							goto IL_0083;
						case 9:
							goto IL_0088;
						case 10:
							goto IL_0092;
						case 11:
							goto IL_00a9;
						case 12:
							goto IL_00be;
						case 13:
							goto IL_00cb;
						case 14:
						case 15:
						case 16:
							goto IL_00d5;
						case 17:
							goto IL_00e0;
						case 18:
							goto IL_00f4;
						case 20:
							goto IL_0106;
						case 21:
							goto IL_0114;
						case 22:
							goto IL_0120;
						case 23:
							goto IL_0139;
						case 24:
							goto IL_0147;
						case 19:
						case 25:
						case 26:
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 28:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0120:
					num2 = 22;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_0139;
					}
					goto IL_0147;
					IL_0139:
					num2 = 23;
					GetHeaders(text, text2);
					goto IL_0147;
					IL_0114:
					num2 = 21;
					text = BuildForm.Get_First_Line_Web(text2);
					goto IL_0120;
					IL_0147:
					num2 = 24;
					Cursor.Current = Cursors.Default;
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text2 = Strings.Trim(TxtInFile.Text);
					goto IL_0023;
					IL_0023:
					num2 = 4;
					myTName = Strings.Trim(TxtTblName.Text);
					goto IL_0037;
					IL_0037:
					num2 = 5;
					if ((Operators.CompareString(text2, "", TextCompare: false) == 0) | LikeOperator.LikeString(Strings.UCase(text2), "*.JMP", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(text2), "*.RDATA", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_007a;
					IL_007a:
					num2 = 7;
					text = "";
					goto IL_0083;
					IL_0083:
					num2 = 8;
					flag = true;
					goto IL_0088;
					IL_0088:
					num2 = 9;
					f_Ignore = true;
					goto IL_0092;
					IL_0092:
					num2 = 10;
					if (GridDataType.RowCount > 0)
					{
						goto IL_00a9;
					}
					goto IL_00d5;
					IL_00a9:
					num2 = 11;
					num5 = (int)Interaction.MsgBox("Are you sure you want to refresh Column Headers for this File?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Reload Columns?");
					goto IL_00be;
					IL_00be:
					num2 = 12;
					if (num5 == 7)
					{
						goto IL_00cb;
					}
					goto IL_00d5;
					IL_00cb:
					num2 = 13;
					flag = false;
					goto IL_00d5;
					IL_00d5:
					num2 = 16;
					if (!flag)
					{
						break;
					}
					goto IL_00e0;
					IL_00e0:
					num2 = 17;
					if (optcsv.Checked)
					{
						goto IL_00f4;
					}
					goto IL_0106;
					IL_00f4:
					num2 = 18;
					Init_Headers(text2, myTName);
					break;
					IL_0106:
					num2 = 20;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0114;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				f_Ignore = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 487;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridDataType_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (f_Ignore)
		{
			return;
		}
		int columnIndex = GridDataType.CurrentCell.ColumnIndex;
		int rowIndex = GridDataType.CurrentCell.RowIndex;
		if (columnIndex < 0 || rowIndex == -1)
		{
			return;
		}
		switch (columnIndex)
		{
		case 2:
			if (Operators.ConditionalCompareObjectEqual(GridDataType.Rows[rowIndex].Cells[columnIndex].Value, "Continuous", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridDataType.Rows[rowIndex].Cells[1].Value, "Character", TextCompare: false))
			{
				GridDataType.Rows[rowIndex].Cells[1].Value = "Numeric";
			}
			break;
		case 1:
			if (Operators.ConditionalCompareObjectEqual(GridDataType.Rows[rowIndex].Cells[columnIndex].Value, "Character", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridDataType.Rows[rowIndex].Cells[2].Value, "Continuous", TextCompare: false))
			{
				GridDataType.Rows[rowIndex].Cells[2].Value = "Default";
			}
			break;
		}
	}

	private void mnuConvNum_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string value = default(string);
		string text = default(string);
		int count = default(int);
		while (true)
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
				case 562:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0001;
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
							goto IL_003a;
						case 5:
							goto IL_0043;
						case 6:
							goto IL_005a;
						case 7:
							goto IL_0063;
						case 9:
							goto IL_00bb;
						case 11:
							goto IL_00c8;
						case 13:
							goto IL_00d5;
						case 15:
							goto IL_00e2;
						case 17:
							goto IL_00ef;
						case 8:
						case 10:
						case 12:
						case 14:
						case 16:
						case 18:
						case 19:
							goto IL_00fc;
						case 20:
							goto IL_0111;
						case 21:
							goto IL_0122;
						case 24:
							goto IL_013d;
						case 25:
							goto IL_0143;
						case 26:
							goto IL_015f;
						case 27:
							goto IL_0187;
						case 23:
						case 28:
						case 29:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 22:
						case 30:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0190:
					if (num5 > num6)
					{
						break;
					}
					goto IL_015f;
					IL_015f:
					num2 = 26;
					GridDataType.SelectedRows[num5].Cells[1].Value = value;
					goto IL_0187;
					IL_0143:
					num2 = 25;
					num6 = checked(GridDataType.SelectedRows.Count - 1);
					num5 = 0;
					goto IL_0190;
					IL_0187:
					num2 = 27;
					num5 = checked(num5 + 1);
					goto IL_0190;
					IL_000b:
					num2 = 2;
					Cursor.Current = Cursors.WaitCursor;
					goto IL_0018;
					IL_0018:
					num2 = 3;
					text = Strings.Trim(Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null)));
					goto IL_003a;
					IL_003a:
					num2 = 4;
					value = "";
					goto IL_0043;
					IL_0043:
					num2 = 5;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_005a;
					}
					goto IL_0063;
					IL_005a:
					num2 = 6;
					text = "numeric";
					goto IL_0063;
					IL_0063:
					num2 = 7;
					switch (text.ToLower())
					{
					case "numeric":
						break;
					case "integer":
						goto IL_00c8;
					case "character":
						goto IL_00d5;
					case "datetime":
						goto IL_00e2;
					case "default":
						goto IL_00ef;
					default:
						goto IL_00fc;
					}
					goto IL_00bb;
					IL_00ef:
					num2 = 17;
					value = "Default";
					goto IL_00fc;
					IL_00e2:
					num2 = 15;
					value = "Datetime";
					goto IL_00fc;
					IL_00d5:
					num2 = 13;
					value = "Character";
					goto IL_00fc;
					IL_00c8:
					num2 = 11;
					value = "Integer";
					goto IL_00fc;
					IL_00bb:
					num2 = 9;
					value = "Numeric";
					goto IL_00fc;
					IL_00fc:
					num2 = 19;
					count = GridDataType.SelectedRows.Count;
					goto IL_0111;
					IL_0111:
					num2 = 20;
					if (count <= 0)
					{
						goto IL_0122;
					}
					goto IL_013d;
					IL_0122:
					num2 = 21;
					Interaction.MsgBox("To convert rows, first highlight the rows to  convert. Use the Ctrl or Shift keys to select multiple rows", MsgBoxStyle.Exclamation, "No Rows selected");
					goto end_IL_0001_3;
					IL_013d:
					num2 = 24;
					num5 = 0;
					goto IL_0143;
					end_IL_0001_2:
					break;
				}
				num2 = 29;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 562;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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

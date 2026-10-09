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
public class FrmTabMenu : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBrowse")]
	private Button _cmdBrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridData")]
	private DataGridView _GridData;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdUp")]
	private Button _cmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDown")]
	private Button _cmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdDel")]
	private Button _cmdDel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK")]
	private Button _cmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCancel")]
	private Button _cmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBrowse2")]
	private Button _cmdBrowse2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdHelpFile")]
	private Button _cmdHelpFile;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdTitle")]
	private Button _cmdTitle;

	public string f_Mode;

	public string f_Data;

	private int f_Rows;

	private int col_label_no;

	private int col_folder_no;

	private int col_icon_no;

	private int col_URL_no;

	private int col_browse_no;

	private const string DLMObj = "<!@@@!>";

	private const string DLMCols = "<!;!>";

	private const string DLMRows = "<!|!>";

	private const string spf_web = "";

	[field: AccessedThroughProperty("lblTitle")]
	internal virtual Label lblTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHTMout")]
	internal virtual TextBox txtHTMout
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblOut")]
	internal virtual Label lblOut
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

	internal virtual DataGridView GridData
	{
		[CompilerGenerated]
		get
		{
			return _GridData;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = GridData_Click;
			DataGridView dataGridView = _GridData;
			if (dataGridView != null)
			{
				dataGridView.Click -= value2;
			}
			_GridData = value;
			dataGridView = _GridData;
			if (dataGridView != null)
			{
				dataGridView.Click += value2;
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

	internal virtual Button cmdDel
	{
		[CompilerGenerated]
		get
		{
			return _cmdDel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDel_Click;
			Button button = _cmdDel;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdDel = value;
			button = _cmdDel;
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

	[field: AccessedThroughProperty("TxtIn")]
	internal virtual TextBox TxtIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdBrowse2
	{
		[CompilerGenerated]
		get
		{
			return _cmdBrowse2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBrowse2_Click;
			Button button = _cmdBrowse2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdBrowse2 = value;
			button = _cmdBrowse2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblIn")]
	internal virtual Label lblIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdHelpFile
	{
		[CompilerGenerated]
		get
		{
			return _cmdHelpFile;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHelpFile_Click;
			Button button = _cmdHelpFile;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdHelpFile = value;
			button = _cmdHelpFile;
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

	internal virtual Button cmdTitle
	{
		[CompilerGenerated]
		get
		{
			return _cmdTitle;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdTitle_Click;
			Button button = _cmdTitle;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdTitle = value;
			button = _cmdTitle;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbWidth")]
	internal virtual ComboBox cmbWidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblwidth")]
	internal virtual Label lblwidth
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbTheme")]
	internal virtual ComboBox cmbTheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lbltheme")]
	internal virtual Label lbltheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbHeight")]
	internal virtual ComboBox cmbHeight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblHeight")]
	internal virtual Label lblHeight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem1")]
	internal virtual ToolStripMenuItem ToolStripMenuItem1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuDisp")]
	internal virtual ToolStripMenuItem mnuDisp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuopt")]
	internal virtual MenuStrip mnuopt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colLabel")]
	internal virtual DataGridViewTextBoxColumn colLabel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colFolder")]
	internal virtual DataGridViewComboBoxColumn colFolder
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colIcon")]
	internal virtual DataGridViewComboBoxColumn colIcon
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colURL")]
	internal virtual DataGridViewTextBoxColumn colURL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("colBrowse")]
	internal virtual DataGridViewButtonColumn colBrowse
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmTabMenu()
	{
		base.Load += FrmTabMenu_Load;
		base.Resize += FrmTabMenu_Resize;
		f_Mode = "TAB";
		f_Data = "";
		f_Rows = 50;
		col_label_no = 0;
		col_folder_no = 1;
		col_icon_no = 2;
		col_URL_no = 3;
		col_browse_no = 4;
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
		this.lblTitle = new System.Windows.Forms.Label();
		this.txtHTMout = new System.Windows.Forms.TextBox();
		this.lblOut = new System.Windows.Forms.Label();
		this.cmdBrowse = new System.Windows.Forms.Button();
		this.GridData = new System.Windows.Forms.DataGridView();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdDel = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.TxtIn = new System.Windows.Forms.TextBox();
		this.cmdBrowse2 = new System.Windows.Forms.Button();
		this.lblIn = new System.Windows.Forms.Label();
		this.cmdHelpFile = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdTitle = new System.Windows.Forms.Button();
		this.cmbWidth = new System.Windows.Forms.ComboBox();
		this.lblwidth = new System.Windows.Forms.Label();
		this.cmbTheme = new System.Windows.Forms.ComboBox();
		this.lbltheme = new System.Windows.Forms.Label();
		this.cmbHeight = new System.Windows.Forms.ComboBox();
		this.lblHeight = new System.Windows.Forms.Label();
		this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuDisp = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuopt = new System.Windows.Forms.MenuStrip();
		this.colLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFolder = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colIcon = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.colURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colBrowse = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.GridData).BeginInit();
		this.mnuopt.SuspendLayout();
		base.SuspendLayout();
		this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblTitle.Location = new System.Drawing.Point(9, 43);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(774, 54);
		this.lblTitle.TabIndex = 0;
		this.lblTitle.Text = "lblTitle";
		this.txtHTMout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtHTMout.Location = new System.Drawing.Point(9, 129);
		this.txtHTMout.Name = "txtHTMout";
		this.txtHTMout.Size = new System.Drawing.Size(697, 22);
		this.txtHTMout.TabIndex = 1;
		this.lblOut.AutoSize = true;
		this.lblOut.Location = new System.Drawing.Point(9, 110);
		this.lblOut.Name = "lblOut";
		this.lblOut.Size = new System.Drawing.Size(313, 17);
		this.lblOut.TabIndex = 2;
		this.lblOut.Text = "Enter HTML Output File Path or SharePoint URL";
		this.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdBrowse.Location = new System.Drawing.Point(800, 129);
		this.cmdBrowse.Name = "cmdBrowse";
		this.cmdBrowse.Size = new System.Drawing.Size(69, 34);
		this.cmdBrowse.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmdBrowse, "Get Output File");
		this.cmdBrowse.UseVisualStyleBackColor = true;
		this.GridData.AllowUserToAddRows = false;
		this.GridData.AllowUserToDeleteRows = false;
		this.GridData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridData.BackgroundColor = System.Drawing.Color.White;
		this.GridData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.GridData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.GridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridData.Columns.AddRange(this.colLabel, this.colFolder, this.colIcon, this.colURL, this.colBrowse);
		this.GridData.GridColor = System.Drawing.Color.White;
		this.GridData.Location = new System.Drawing.Point(9, 291);
		this.GridData.Name = "GridData";
		this.GridData.RowHeadersWidth = 50;
		this.GridData.RowTemplate.Height = 24;
		this.GridData.Size = new System.Drawing.Size(774, 320);
		this.GridData.StandardTab = true;
		this.GridData.TabIndex = 10;
		this.cmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdUp.Location = new System.Drawing.Point(800, 291);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(69, 34);
		this.cmdUp.TabIndex = 11;
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDown.Location = new System.Drawing.Point(800, 337);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(69, 34);
		this.cmdDown.TabIndex = 12;
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdDel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDel.Location = new System.Drawing.Point(800, 383);
		this.cmdDel.Name = "cmdDel";
		this.cmdDel.Size = new System.Drawing.Size(69, 34);
		this.cmdDel.TabIndex = 13;
		this.cmdDel.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(800, 43);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(69, 34);
		this.cmdOK.TabIndex = 14;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(800, 82);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(69, 34);
		this.cmdCancel.TabIndex = 15;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.TxtIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtIn.Location = new System.Drawing.Point(9, 185);
		this.TxtIn.Name = "TxtIn";
		this.TxtIn.Size = new System.Drawing.Size(697, 22);
		this.TxtIn.TabIndex = 4;
		this.cmdBrowse2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdBrowse2.Location = new System.Drawing.Point(800, 185);
		this.cmdBrowse2.Name = "cmdBrowse2";
		this.cmdBrowse2.Size = new System.Drawing.Size(69, 34);
		this.cmdBrowse2.TabIndex = 6;
		this.cmdBrowse2.UseVisualStyleBackColor = true;
		this.lblIn.AutoSize = true;
		this.lblIn.Location = new System.Drawing.Point(9, 166);
		this.lblIn.Name = "lblIn";
		this.lblIn.Size = new System.Drawing.Size(33, 17);
		this.lblIn.TabIndex = 11;
		this.lblIn.Text = "lblin";
		this.cmdHelpFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdHelpFile.Location = new System.Drawing.Point(719, 185);
		this.cmdHelpFile.Name = "cmdHelpFile";
		this.cmdHelpFile.Size = new System.Drawing.Size(70, 34);
		this.cmdHelpFile.TabIndex = 5;
		this.ToolTip1.SetToolTip(this.cmdHelpFile, "Help on File Format");
		this.cmdHelpFile.UseVisualStyleBackColor = true;
		this.cmdTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTitle.Location = new System.Drawing.Point(719, 129);
		this.cmdTitle.Name = "cmdTitle";
		this.cmdTitle.Size = new System.Drawing.Size(70, 36);
		this.cmdTitle.TabIndex = 2;
		this.ToolTip1.SetToolTip(this.cmdTitle, "Edit Menu Title");
		this.cmdTitle.UseVisualStyleBackColor = true;
		this.cmbWidth.FormattingEnabled = true;
		this.cmbWidth.Items.AddRange(new object[2] { "", "30" });
		this.cmbWidth.Location = new System.Drawing.Point(210, 244);
		this.cmbWidth.Name = "cmbWidth";
		this.cmbWidth.Size = new System.Drawing.Size(121, 24);
		this.cmbWidth.TabIndex = 8;
		this.cmbWidth.Text = "15";
		this.lblwidth.AutoSize = true;
		this.lblwidth.Location = new System.Drawing.Point(210, 224);
		this.lblwidth.Name = "lblwidth";
		this.lblwidth.Size = new System.Drawing.Size(83, 17);
		this.lblwidth.TabIndex = 13;
		this.lblwidth.Text = "Menu Width";
		this.cmbTheme.FormattingEnabled = true;
		this.cmbTheme.Items.AddRange(new object[9] { "", "Arctic", "Classic", "Dark", "Dark Blue", "Energy Blue", "Light", "ui-Redmond", "ui-Sunny" });
		this.cmbTheme.Location = new System.Drawing.Point(9, 244);
		this.cmbTheme.Name = "cmbTheme";
		this.cmbTheme.Size = new System.Drawing.Size(170, 24);
		this.cmbTheme.TabIndex = 7;
		this.cmbTheme.Text = "Classic";
		this.lbltheme.AutoSize = true;
		this.lbltheme.Location = new System.Drawing.Point(9, 224);
		this.lbltheme.Name = "lbltheme";
		this.lbltheme.Size = new System.Drawing.Size(52, 17);
		this.lbltheme.TabIndex = 15;
		this.lbltheme.Text = "Theme";
		this.cmbHeight.FormattingEnabled = true;
		this.cmbHeight.Items.AddRange(new object[2] { "", "5" });
		this.cmbHeight.Location = new System.Drawing.Point(359, 244);
		this.cmbHeight.Name = "cmbHeight";
		this.cmbHeight.Size = new System.Drawing.Size(121, 24);
		this.cmbHeight.TabIndex = 9;
		this.cmbHeight.Text = "5";
		this.lblHeight.AutoSize = true;
		this.lblHeight.Location = new System.Drawing.Point(359, 224);
		this.lblHeight.Name = "lblHeight";
		this.lblHeight.Size = new System.Drawing.Size(80, 17);
		this.lblHeight.TabIndex = 17;
		this.lblHeight.Text = "Title Height";
		this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuDisp });
		this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
		this.ToolStripMenuItem1.Size = new System.Drawing.Size(73, 24);
		this.ToolStripMenuItem1.Text = "Options";
		this.mnuDisp.CheckOnClick = true;
		this.mnuDisp.Name = "mnuDisp";
		this.mnuDisp.Size = new System.Drawing.Size(311, 26);
		this.mnuDisp.Text = "Do not Display Report to Terminal";
		this.mnuopt.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuopt.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.ToolStripMenuItem1 });
		this.mnuopt.Location = new System.Drawing.Point(0, 0);
		this.mnuopt.Name = "mnuopt";
		this.mnuopt.Size = new System.Drawing.Size(879, 28);
		this.mnuopt.TabIndex = 18;
		this.mnuopt.Text = "Options";
		this.colLabel.HeaderText = "Label";
		this.colLabel.Name = "colLabel";
		this.colLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colFolder.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colFolder.DisplayStyleForCurrentCellOnly = true;
		this.colFolder.DropDownWidth = 150;
		this.colFolder.FillWeight = 50f;
		this.colFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.colFolder.HeaderText = "Menu Type";
		this.colFolder.Items.AddRange("Folder-Open", "Folder-Close", "Menu-Item");
		this.colFolder.Name = "colFolder";
		this.colFolder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colIcon.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.colIcon.DisplayStyleForCurrentCellOnly = true;
		this.colIcon.DropDownWidth = 150;
		this.colIcon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.colIcon.HeaderText = "Icon";
		this.colIcon.Items.AddRange("", "Folder", "Book", "Favorite", "Notepad");
		this.colIcon.Name = "colIcon";
		this.colURL.HeaderText = "URL";
		this.colURL.Name = "colURL";
		this.colBrowse.HeaderText = "Browse";
		this.colBrowse.Name = "colBrowse";
		this.colBrowse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colBrowse.Text = "...";
		this.colBrowse.UseColumnTextForButtonValue = true;
		this.colBrowse.Width = 55;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(879, 613);
		base.Controls.Add(this.lblHeight);
		base.Controls.Add(this.cmbHeight);
		base.Controls.Add(this.lbltheme);
		base.Controls.Add(this.cmbTheme);
		base.Controls.Add(this.lblwidth);
		base.Controls.Add(this.cmbWidth);
		base.Controls.Add(this.cmdTitle);
		base.Controls.Add(this.cmdHelpFile);
		base.Controls.Add(this.lblIn);
		base.Controls.Add(this.cmdBrowse2);
		base.Controls.Add(this.TxtIn);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.cmdDel);
		base.Controls.Add(this.cmdDown);
		base.Controls.Add(this.cmdUp);
		base.Controls.Add(this.GridData);
		base.Controls.Add(this.cmdBrowse);
		base.Controls.Add(this.lblOut);
		base.Controls.Add(this.txtHTMout);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add(this.mnuopt);
		base.MainMenuStrip = this.mnuopt;
		base.Name = "FrmTabMenu";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Create TAB Output";
		((System.ComponentModel.ISupportInitialize)this.GridData).EndInit();
		this.mnuopt.ResumeLayout(false);
		this.mnuopt.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void FrmTabMenu_Load(object sender, EventArgs e)
	{
		int num = 0;
		int num2 = 0;
		string left = "";
		string[] array = null;
		string[] array2 = null;
		string[] array3 = null;
		string text = "";
		string text2 = "";
		int num3 = 7;
		GridData.RowCount = f_Rows;
		checked
		{
			int num4 = GridData.RowCount - 1;
			for (num = 0; num <= num4; num++)
			{
				int num5 = GridData.ColumnCount - 1;
				for (num2 = 0; num2 <= num5; num2++)
				{
					GridData.Rows[num].Cells[num2].Value = "";
				}
			}
			cmdTitle.Tag = "";
			txtHTMout.Text = "SQLPathFinder_" + Strings.LCase(f_Mode) + ".htm";
			Button MyButton = cmdUp;
			BuildForm.Set_Btn_Img(ref MyButton, "up");
			cmdUp = MyButton;
			MyButton = cmdDown;
			BuildForm.Set_Btn_Img(ref MyButton, "down");
			cmdDown = MyButton;
			MyButton = cmdDel;
			BuildForm.Set_Btn_Img(ref MyButton, "delete");
			cmdDel = MyButton;
			MyButton = cmdHelpFile;
			BuildForm.Set_Btn_Img(ref MyButton, "helpb");
			cmdHelpFile = MyButton;
			MyButton = cmdTitle;
			BuildForm.Set_Btn_Img(ref MyButton, "edit");
			cmdTitle = MyButton;
			MyButton = cmdBrowse;
			BuildForm.Set_Btn_Img(ref MyButton, "browse");
			cmdBrowse = MyButton;
			MyButton = cmdBrowse2;
			BuildForm.Set_Btn_Img(ref MyButton, "browse");
			cmdBrowse2 = MyButton;
			string left2 = f_Mode;
			if (Operators.CompareString(left2, "TAB", TextCompare: false) != 0)
			{
				if (Operators.CompareString(left2, "MENU", TextCompare: false) == 0)
				{
					Text = "Create Menu Output";
					lblTitle.Text = "Create a Menu Web Page. Specify folders and menu items as well as their attributes. This includes the web page to invoke when a menu item is clicked";
					lblIn.Text = "Specify a file with Menu options, or define Menu items in the table below. Click Help for file format";
					ToolTip1.SetToolTip(cmdBrowse2, "Get optional File with Menu Items");
					ToolTip1.SetToolTip(cmdHelpFile, "Show format of Menu File");
					colLabel.HeaderText = "Menu Label";
					colURL.HeaderText = "Menu URL";
					cmbWidth.Items.Clear();
					cmbWidth.Items.Add("");
					cmbWidth.Items.Add("15");
					lblwidth.Text = "Menu Width";
					cmbWidth.Text = "15";
					cmbHeight.Items.Clear();
					cmbHeight.Items.Add("");
					cmbHeight.Items.Add("0");
					cmbHeight.Items.Add("5");
					lblHeight.Text = "Title Height";
					cmbHeight.Text = "0";
				}
			}
			else
			{
				Text = "Create TAB Output";
				lblTitle.Text = "Create a Tabbed Web Page. Specify Tab labels and the relative WEB URL to load when Tab is Selected";
				lblIn.Text = "Specify a file with TAB options or specify TAB options in the table below. Click Help for file format";
				colLabel.HeaderText = "Tab Label";
				colURL.HeaderText = "Tab URL";
				ToolTip1.SetToolTip(cmdBrowse2, "Get optional File with Tab Definitions");
				ToolTip1.SetToolTip(cmdHelpFile, "Show format of Tab File");
				GridData.Columns[col_folder_no].Visible = false;
				GridData.Columns[col_icon_no].Visible = false;
				cmdTitle.Visible = false;
				TxtIn.Width = txtHTMout.Width;
				cmbWidth.Items.Clear();
				cmbWidth.Items.Add("");
				cmbWidth.Items.Add("5000px");
				cmbWidth.Items.Add("100%");
				lblwidth.Text = "TAB Width";
				cmbWidth.Text = "100%";
				cmbHeight.Items.Clear();
				cmbHeight.Items.Add("");
				cmbHeight.Items.Add("10000px");
				lblHeight.Text = "TAB Height";
				cmbHeight.Text = "100%";
				cmbWidth.Enabled = false;
				cmbHeight.Enabled = false;
			}
			DataGridView MyGrid = GridData;
			GridModule.Number_Grid(ref MyGrid);
			GridData = MyGrid;
			if (Operators.CompareString(Strings.Trim(f_Data), "", TextCompare: false) != 0)
			{
				array = Strings.Split(f_Data, "<!@@@!>");
				if (Information.UBound(array) >= 0)
				{
					txtHTMout.Text = array[0];
				}
				if (Information.UBound(array) >= 1)
				{
					TxtIn.Text = array[1];
				}
				if (Information.UBound(array) >= 2)
				{
					cmdTitle.Tag = array[2];
				}
				if (Information.UBound(array) >= 3)
				{
					cmbTheme.Text = array[3];
				}
				if (Operators.CompareString(f_Mode, "MENU", TextCompare: false) == 0)
				{
					if (Information.UBound(array) >= 4)
					{
						cmbWidth.Text = array[4];
					}
					if (Information.UBound(array) >= 5)
					{
						cmbHeight.Text = array[5];
					}
				}
				if (Information.UBound(array) >= 6)
				{
					left = Strings.Trim(Strings.UCase(array[6]));
				}
				if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
				{
					mnuDisp.Checked = true;
				}
				if (Information.UBound(array) >= num3)
				{
					array2 = Strings.Split(array[num3], "<!|!>");
					int num6 = Information.UBound(array2);
					for (num = 0; num <= num6; num++)
					{
						array3 = Strings.Split(array2[num], "<!;!>");
						int num7 = Information.UBound(array3);
						for (num2 = 0; num2 <= num7; num2++)
						{
							switch (Strings.UCase(Strings.Mid(array3[num2] + "    ", 1, 4)))
							{
							case "LBL:":
								GridData.Rows[num].Cells[col_label_no].Value = Strings.Trim(Strings.Mid(array3[num2], 5));
								break;
							case "URL:":
								GridData.Rows[num].Cells[col_URL_no].Value = Strings.Trim(Strings.Mid(array3[num2], 5));
								break;
							case "FLD:":
								GridData.Rows[num].Cells[col_folder_no].Value = Strings.Trim(Strings.Mid(array3[num2], 5));
								break;
							case "ICO:":
								GridData.Rows[num].Cells[col_icon_no].Value = Strings.Trim(Strings.Mid(array3[num2], 5));
								break;
							}
						}
						array3 = null;
					}
					array2 = null;
				}
				array = null;
			}
			Set_Col_Width();
		}
	}

	private void cmdBrowse_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string fileName = default(string);
		while (true)
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
				case 176:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0036;
						case 5:
							goto IL_004d;
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
					BuildForm.FileOpenSave("S", fileName, "htm", "HTML Report File", "");
					goto IL_0036;
					IL_0036:
					num2 = 4;
					fileName = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					goto IL_004d;
					IL_000b:
					num2 = 2;
					fileName = txtHTMout.Text;
					goto IL_0019;
					IL_004d:
					num2 = 5;
					if (Operators.CompareString(fileName, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				txtHTMout.Text = BuildForm.Strip_Add_MyPCDir("S", fileName);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 176;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
					f_Data = "CANCEL";
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

	public void Set_Col_Width()
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
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 395:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0016;
						case 6:
							goto IL_0044;
						case 8:
							goto IL_006c;
						case 9:
							goto IL_008c;
						case 10:
							goto IL_00ad;
						case 5:
						case 7:
						case 11:
						case 12:
							goto IL_00d9;
						case 13:
							goto IL_00f9;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ad:
					num2 = 10;
					num5 = checked((int)Math.Round((double)(GridData.Width - num6 - 275) / 2.0));
					goto IL_00d9;
					IL_0044:
					num2 = 6;
					num5 = checked((int)Math.Round((double)(GridData.Width - num6 - 75) / 2.0));
					goto IL_00d9;
					IL_008c:
					num2 = 9;
					GridData.Columns[col_icon_no].Width = 80;
					goto IL_00ad;
					IL_00d9:
					num2 = 12;
					GridData.Columns[col_label_no].Width = num5;
					goto IL_00f9;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = 55;
					goto IL_0016;
					IL_0016:
					num2 = 4;
					left = f_Mode;
					if (Operators.CompareString(left, "TAB", TextCompare: false) == 0)
					{
						goto IL_0044;
					}
					if (Operators.CompareString(left, "MENU", TextCompare: false) == 0)
					{
						goto IL_006c;
					}
					goto IL_00d9;
					IL_00f9:
					num2 = 13;
					GridData.Columns[col_URL_no].Width = num5;
					break;
					IL_006c:
					num2 = 8;
					GridData.Columns[col_folder_no].Width = 120;
					goto IL_008c;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				GridData.Columns[col_browse_no].Width = num6;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 395;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmTabMenu_Resize(object sender, EventArgs e)
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

	private void GridData_Click(object sender, EventArgs e)
	{
		int columnIndex = GridData.CurrentCell.ColumnIndex;
		int rowIndex = GridData.CurrentCell.RowIndex;
		if (columnIndex == -1 || rowIndex == -1)
		{
			return;
		}
		int num = columnIndex;
		if (num == col_browse_no)
		{
			string iniFile = Conversions.ToString(GridData.Rows[rowIndex].Cells[col_URL_no].Value);
			BuildForm.FileOpenSave("S", iniFile, "htm", "HTML File", "");
			iniFile = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
			if (Operators.CompareString(iniFile, "CANCEL", TextCompare: false) != 0)
			{
				GridData.Rows[rowIndex].Cells[col_URL_no].Value = BuildForm.Strip_Add_MyPCDir("S", iniFile);
			}
		}
	}

	private void cmdUp_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridData;
				GridModule.Grid_Up(ref MyGrid);
				GridData = MyGrid;
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
				DataGridView MyGrid = GridData;
				GridModule.Grid_Down(ref MyGrid);
				GridData = MyGrid;
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

	private void cmdDel_Click(object sender, EventArgs e)
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
				case 156:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_003f;
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
					MyGrid = GridData;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					GridData = MyGrid;
					goto IL_0024;
					IL_0024:
					num2 = 3;
					if (GridData.RowCount >= f_Rows)
					{
						break;
					}
					goto IL_003f;
					IL_003f:
					num2 = 4;
					GridData.RowCount = f_Rows;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				MyGrid = GridData;
				GridModule.Number_Grid(ref MyGrid);
				GridData = MyGrid;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 156;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		bool flag = default(bool);
		string text2 = default(string);
		bool flag2 = default(bool);
		bool flag3 = default(bool);
		string text3 = default(string);
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
					int num6;
					string left;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 4742:
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
								goto IL_0022;
							case 7:
								goto IL_0027;
							case 8:
								goto IL_0030;
							case 9:
								goto IL_0035;
							case 10:
								goto IL_003f;
							case 11:
								goto IL_004d;
							case 12:
								goto IL_006c;
							case 13:
								goto IL_008b;
							case 14:
								goto IL_00ad;
							case 16:
							case 17:
								goto IL_00d4;
							case 18:
								goto IL_00f8;
							case 19:
								goto IL_011c;
							case 20:
								goto IL_014a;
							case 21:
								goto IL_016e;
							case 22:
								goto IL_0192;
							case 23:
								goto IL_01b6;
							case 24:
								goto IL_01ca;
							case 26:
								goto IL_01d8;
							case 25:
							case 27:
							case 28:
								goto IL_01e3;
							case 29:
								goto IL_01fe;
							case 30:
								goto IL_0228;
							case 31:
								goto IL_022e;
							case 32:
								goto IL_0247;
							case 33:
								goto IL_028e;
							case 35:
							case 36:
								goto IL_02cc;
							case 34:
							case 37:
								goto IL_02db;
							case 38:
								goto IL_02f4;
							case 42:
								goto IL_031e;
							case 43:
								goto IL_0343;
							case 44:
								goto IL_034d;
							case 45:
								goto IL_0367;
							case 47:
								goto IL_0399;
							case 48:
								goto IL_03f3;
							case 49:
								goto IL_044d;
							case 50:
								goto IL_0535;
							case 53:
								goto IL_0579;
							case 54:
								goto IL_05d3;
							case 55:
								goto IL_062d;
							case 56:
								goto IL_0687;
							case 57:
								goto IL_06e1;
							case 58:
								goto IL_08e3;
							case 59:
							case 60:
								goto IL_0921;
							case 61:
								goto IL_0b23;
							case 46:
							case 51:
							case 52:
							case 62:
							case 63:
							case 64:
								goto IL_0b64;
							case 65:
								goto IL_0b73;
							case 66:
								goto IL_0b8c;
							case 68:
							case 69:
								goto IL_0bb0;
							case 70:
								goto IL_0bb6;
							case 71:
								goto IL_0bbc;
							case 72:
								goto IL_0bd5;
							case 73:
								goto IL_0bdf;
							case 74:
								goto IL_0c65;
							case 76:
								goto IL_0ceb;
							case 77:
								goto IL_0d71;
							case 78:
								goto IL_0dc1;
							case 79:
								goto IL_0dc7;
							case 80:
								goto IL_0e47;
							case 81:
								goto IL_0e81;
							case 82:
							case 83:
								goto IL_0ec4;
							case 84:
								goto IL_0efe;
							case 75:
							case 85:
							case 86:
							case 87:
								goto IL_0f42;
							case 88:
								goto IL_0f5b;
							case 89:
								goto IL_0f61;
							case 90:
								goto IL_0f6c;
							case 93:
								goto IL_0f96;
							case 94:
								goto IL_0fac;
							case 97:
								goto IL_0fb7;
							case 92:
							case 95:
							case 96:
							case 98:
							case 99:
							case 100:
								goto IL_0fd5;
							case 40:
							case 41:
							case 91:
							case 101:
							case 102:
								goto IL_0fe5;
							case 103:
								goto IL_0ff3;
							case 104:
								goto IL_1001;
							case 107:
								goto IL_1036;
							case 108:
								goto IL_1072;
							case 109:
								goto IL_1080;
							case 106:
							case 111:
							case 112:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 15:
							case 39:
							case 67:
							case 105:
							case 110:
							case 113:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_1001:
						num2 = 104;
						Interaction.MsgBox("No " + f_Mode + " data defined.", MsgBoxStyle.Exclamation, f_Mode + " Error");
						goto end_IL_0001_3;
						IL_044d:
						num2 = 49;
						if ((Operators.CompareString(text, "", TextCompare: false) == 0 && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)) || (Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)))
						{
							goto IL_0535;
						}
						goto IL_0b64;
						IL_0399:
						num2 = 47;
						GridData.Rows[num5].Cells[col_label_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_label_no].Value));
						goto IL_03f3;
						IL_0b23:
						num2 = 61;
						text = "Issue With Row " + Conversions.ToString(num5 + 1) + " Of the " + f_Mode + " grid. The Menu label And Menu Type must have values. The URL must (And must only) have a value For Type Menu-Item.";
						goto IL_0b64;
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
						flag = false;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						text2 = "";
						goto IL_0022;
						IL_0022:
						num2 = 6;
						flag2 = true;
						goto IL_0027;
						IL_0027:
						num2 = 7;
						text = "";
						goto IL_0030;
						IL_0030:
						num2 = 8;
						flag3 = false;
						goto IL_0035;
						IL_0035:
						num2 = 9;
						text3 = "";
						goto IL_003f;
						IL_003f:
						num2 = 10;
						f_Data = "";
						goto IL_004d;
						IL_004d:
						num2 = 11;
						txtHTMout.Text = Strings.Trim(txtHTMout.Text);
						goto IL_006c;
						IL_006c:
						num2 = 12;
						TxtIn.Text = Strings.Trim(TxtIn.Text);
						goto IL_008b;
						IL_008b:
						num2 = 13;
						if (Operators.CompareString(txtHTMout.Text, "", TextCompare: false) == 0)
						{
							goto IL_00ad;
						}
						goto IL_00d4;
						IL_00ad:
						num2 = 14;
						Interaction.MsgBox("You must specify an output file or SharePoint path.", MsgBoxStyle.Exclamation, f_Mode + " Error");
						goto end_IL_0001_3;
						IL_00d4:
						num2 = 17;
						f_Data = f_Data + txtHTMout.Text + "<!@@@!>";
						goto IL_00f8;
						IL_00f8:
						num2 = 18;
						f_Data = f_Data + TxtIn.Text + "<!@@@!>";
						goto IL_011c;
						IL_011c:
						num2 = 19;
						f_Data = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(f_Data, cmdTitle.Tag), "<!@@@!>"));
						goto IL_014a;
						IL_014a:
						num2 = 20;
						f_Data = f_Data + cmbTheme.Text + "<!@@@!>";
						goto IL_016e;
						IL_016e:
						num2 = 21;
						f_Data = f_Data + cmbWidth.Text + "<!@@@!>";
						goto IL_0192;
						IL_0192:
						num2 = 22;
						f_Data = f_Data + cmbHeight.Text + "<!@@@!>";
						goto IL_01b6;
						IL_01b6:
						num2 = 23;
						if (mnuDisp.Checked)
						{
							goto IL_01ca;
						}
						goto IL_01d8;
						IL_01ca:
						num2 = 24;
						text3 = "Y";
						goto IL_01e3;
						IL_01d8:
						num2 = 26;
						text3 = "N";
						goto IL_01e3;
						IL_01e3:
						num2 = 28;
						f_Data = f_Data + text3 + "<!@@@!>";
						goto IL_01fe;
						IL_01fe:
						num2 = 29;
						if (Operators.CompareString(Strings.Trim(TxtIn.Text), "", TextCompare: false) != 0)
						{
							goto IL_0228;
						}
						goto IL_031e;
						IL_0228:
						num2 = 30;
						flag3 = true;
						goto IL_022e;
						IL_022e:
						num2 = 31;
						num7 = GridData.RowCount - 1;
						num5 = 0;
						goto IL_02d3;
						IL_02d3:
						if (num5 <= num7)
						{
							goto IL_0247;
						}
						goto IL_02db;
						IL_0247:
						num2 = 32;
						if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_label_no].Value)), "", TextCompare: false) != 0)
						{
							goto IL_028e;
						}
						goto IL_02cc;
						IL_028e:
						num2 = 33;
						text = "Note that you cannot specify a file with " + f_Mode + " definitions as well as configure the " + f_Mode + " Grid. Please specify one or the other";
						goto IL_02db;
						IL_02db:
						num2 = 37;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_02f4;
						}
						goto IL_0fe5;
						IL_02f4:
						num2 = 38;
						Interaction.MsgBox(text, MsgBoxStyle.Exclamation, f_Mode + " Error");
						goto end_IL_0001_3;
						IL_02cc:
						num2 = 36;
						num5++;
						goto IL_02d3;
						IL_031e:
						num2 = 42;
						if (Operators.CompareString(TxtIn.Text, "", TextCompare: false) == 0)
						{
							goto IL_0343;
						}
						goto IL_0fe5;
						IL_0343:
						num2 = 43;
						text = "";
						goto IL_034d;
						IL_034d:
						num2 = 44;
						num8 = GridData.RowCount - 1;
						num5 = 0;
						goto IL_0b6b;
						IL_0b6b:
						if (num5 <= num8)
						{
							goto IL_0367;
						}
						goto IL_0b73;
						IL_0b73:
						num2 = 65;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_0b8c;
						}
						goto IL_0bb0;
						IL_0b8c:
						num2 = 66;
						Interaction.MsgBox(text, MsgBoxStyle.Exclamation, f_Mode + " Error");
						goto end_IL_0001_3;
						IL_0bb0:
						num2 = 69;
						flag = false;
						goto IL_0bb6;
						IL_0bb6:
						num2 = 70;
						flag2 = true;
						goto IL_0bbc;
						IL_0bbc:
						num2 = 71;
						num9 = GridData.RowCount - 1;
						num5 = 0;
						goto IL_0fdc;
						IL_0fdc:
						if (num5 <= num9)
						{
							goto IL_0bd5;
						}
						goto IL_0fe5;
						IL_0bd5:
						num2 = 72;
						text2 = "";
						goto IL_0bdf;
						IL_0bdf:
						num2 = 73;
						if (Operators.CompareString(f_Mode, "TAB", TextCompare: false) == 0 && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false))
						{
							goto IL_0c65;
						}
						goto IL_0ceb;
						IL_1072:
						num2 = 108;
						f_Data = "";
						goto IL_1080;
						IL_03f3:
						num2 = 48;
						GridData.Rows[num5].Cells[col_URL_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_URL_no].Value));
						goto IL_044d;
						IL_0c65:
						num2 = 74;
						text2 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text2 + "LBL:", GridData.Rows[num5].Cells[col_label_no].Value), "<!;!>"), "URL:"), GridData.Rows[num5].Cells[col_URL_no].Value));
						goto IL_0f42;
						IL_0ceb:
						num2 = 76;
						if (Operators.CompareString(f_Mode, "MENU", TextCompare: false) == 0 && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_folder_no].Value, "", TextCompare: false))
						{
							goto IL_0d71;
						}
						goto IL_0f42;
						IL_1080:
						num2 = 109;
						Interaction.MsgBox("For Menus, a Folder must be the first Menu-Type in the Grid", MsgBoxStyle.Exclamation, f_Mode + " Error");
						goto end_IL_0001_3;
						IL_0535:
						num2 = 50;
						text = "Issue With Row " + Conversions.ToString(num5 + 1) + " Of the " + f_Mode + " grid. The Tab URL And the Tab label must have values";
						goto IL_0b64;
						IL_0d71:
						num2 = 77;
						if (Conversions.ToBoolean(!flag && Conversions.ToBoolean(LikeOperator.LikeObject(GridData.Rows[num5].Cells[col_folder_no].Value, "Folder*", CompareMethod.Binary))))
						{
							goto IL_0dc1;
						}
						goto IL_0dc7;
						IL_0b64:
						num2 = 64;
						num5++;
						goto IL_0b6b;
						IL_0fe5:
						num2 = 102;
						if (!flag3)
						{
							goto IL_0ff3;
						}
						goto IL_1036;
						IL_0ff3:
						num2 = 103;
						f_Data = "";
						goto IL_1001;
						IL_0dc1:
						num2 = 78;
						flag = true;
						goto IL_0dc7;
						IL_0dc7:
						num2 = 79;
						text2 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text2 + "LBL:", GridData.Rows[num5].Cells[col_label_no].Value), "<!;!>"), "FLD:"), GridData.Rows[num5].Cells[col_folder_no].Value));
						goto IL_0e47;
						IL_0e47:
						num2 = 80;
						if (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_icon_no].Value, "", TextCompare: false))
						{
							goto IL_0e81;
						}
						goto IL_0ec4;
						IL_0e81:
						num2 = 81;
						text2 = Conversions.ToString(Operators.ConcatenateObject(text2 + "<!;!>ICO:", GridData.Rows[num5].Cells[col_icon_no].Value));
						goto IL_0ec4;
						IL_0ec4:
						num2 = 83;
						if (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false))
						{
							goto IL_0efe;
						}
						goto IL_0f42;
						IL_0efe:
						num2 = 84;
						text2 = Conversions.ToString(Operators.ConcatenateObject(text2 + "<!;!>URL:", GridData.Rows[num5].Cells[col_URL_no].Value));
						goto IL_0f42;
						IL_0f42:
						num2 = 87;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_0f5b;
						}
						goto IL_0fd5;
						IL_0f5b:
						num2 = 88;
						flag3 = true;
						goto IL_0f61;
						IL_0f61:
						num2 = 89;
						if (flag2)
						{
							goto IL_0f6c;
						}
						goto IL_0fb7;
						IL_0f6c:
						num2 = 90;
						if (Operators.CompareString(f_Mode, "MENU", TextCompare: false) != 0 || flag)
						{
							goto IL_0f96;
						}
						goto IL_0fe5;
						IL_0921:
						num2 = 60;
						if (Conversions.ToBoolean((Operators.CompareString(text, "", TextCompare: false) == 0 && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Conversions.ToBoolean(LikeOperator.LikeObject(GridData.Rows[num5].Cells[col_folder_no].Value, "Folder*", CompareMethod.Binary)) && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)) || (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_folder_no].Value, "Menu-Item", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)) || (Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_folder_no].Value, "", TextCompare: false) || Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)))))
						{
							goto IL_0b23;
						}
						goto IL_0b64;
						IL_0f96:
						num2 = 93;
						f_Data += text2;
						goto IL_0fac;
						IL_0fac:
						num2 = 94;
						flag2 = false;
						goto IL_0fd5;
						IL_0fb7:
						num2 = 97;
						f_Data = f_Data + "<!|!>" + text2;
						goto IL_0fd5;
						IL_0fd5:
						num2 = 100;
						num5++;
						goto IL_0fdc;
						IL_0367:
						num2 = 45;
						left = f_Mode;
						if (Operators.CompareString(left, "TAB", TextCompare: false) == 0)
						{
							goto IL_0399;
						}
						if (Operators.CompareString(left, "MENU", TextCompare: false) == 0)
						{
							goto IL_0579;
						}
						goto IL_0b64;
						IL_08e3:
						num2 = 58;
						text = "Issue With Row " + Conversions.ToString(num5 + 1) + " Of the " + f_Mode + " grid. The Menu label And Menu Type must have values. The URL must (And must only) have a value For Type Menu-Item.";
						goto IL_0921;
						IL_0579:
						num2 = 53;
						GridData.Rows[num5].Cells[col_label_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_label_no].Value));
						goto IL_05d3;
						IL_05d3:
						num2 = 54;
						GridData.Rows[num5].Cells[col_folder_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_folder_no].Value));
						goto IL_062d;
						IL_062d:
						num2 = 55;
						GridData.Rows[num5].Cells[col_icon_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_icon_no].Value));
						goto IL_0687;
						IL_0687:
						num2 = 56;
						GridData.Rows[num5].Cells[col_URL_no].Value = Strings.Trim(Conversions.ToString(GridData.Rows[num5].Cells[col_URL_no].Value));
						goto IL_06e1;
						IL_06e1:
						num2 = 57;
						if (Conversions.ToBoolean((Operators.CompareString(text, "", TextCompare: false) == 0 && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Conversions.ToBoolean(LikeOperator.LikeObject(GridData.Rows[num5].Cells[col_folder_no].Value, "Folder*", CompareMethod.Binary)) && Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)) || (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_folder_no].Value, "Menu-Item", TextCompare: false) && Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)) || (Operators.ConditionalCompareObjectEqual(GridData.Rows[num5].Cells[col_label_no].Value, "", TextCompare: false) && (Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_folder_no].Value, "", TextCompare: false) || Operators.ConditionalCompareObjectNotEqual(GridData.Rows[num5].Cells[col_URL_no].Value, "", TextCompare: false)))))
						{
							goto IL_08e3;
						}
						goto IL_0921;
						IL_1036:
						num2 = 107;
						if (Operators.CompareString(TxtIn.Text, "", TextCompare: false) != 0 || Operators.CompareString(f_Mode, "MENU", TextCompare: false) != 0 || flag)
						{
							break;
						}
						goto IL_1072;
						end_IL_0001_2:
						break;
					}
					num2 = 112;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4742;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdHelpFile_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string title = default(string);
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
				case 457:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0054;
						case 9:
							goto IL_0062;
						case 10:
							goto IL_006c;
						case 11:
							goto IL_0075;
						case 12:
							goto IL_0084;
						case 13:
							goto IL_0093;
						case 14:
							goto IL_00ac;
						case 15:
							goto IL_00c5;
						case 16:
							goto IL_00de;
						case 17:
							goto IL_00ed;
						case 18:
							goto IL_0106;
						case 19:
							goto IL_011f;
						case 20:
							goto IL_0138;
						case 5:
						case 8:
						case 21:
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_011f:
					num2 = 19;
					text = string.Concat(text + "\r\n  ,{ label: 'Menu 4', id:'spfmenu3', icon: '", "notepad.png'}");
					goto IL_0138;
					IL_0138:
					num2 = 20;
					text += "\r\n ]}";
					break;
					IL_0106:
					num2 = 18;
					text = string.Concat(text + "\r\n   { label: 'Menu 3', id:'spfmenu2', icon: '", "notepad.png' }");
					goto IL_011f;
					IL_004b:
					num2 = 6;
					title = "Sample TAB File";
					goto IL_0054;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					title = "";
					goto IL_001d;
					IL_001d:
					num2 = 4;
					left = f_Mode;
					if (Operators.CompareString(left, "TAB", TextCompare: false) == 0)
					{
						goto IL_004b;
					}
					if (Operators.CompareString(left, "MENU", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0062;
					IL_0054:
					num2 = 7;
					text = "label,url\r\n\"Tab 1\",\"http://www.intel.com\"\r\n\"Tab 2\",\"http://www.oracle.com\"";
					break;
					IL_0062:
					num2 = 9;
					title = "Sample Menu File";
					goto IL_006c;
					IL_006c:
					num2 = 10;
					text = "htm=";
					goto IL_0075;
					IL_0075:
					num2 = 11;
					text += "\r\n'SQLPathFinder_Test.htm', 'htm1.htm', 'tab.htm'\r\n";
					goto IL_0084;
					IL_0084:
					num2 = 12;
					text += "\r\n\r\nmenu=";
					goto IL_0093;
					IL_0093:
					num2 = 13;
					text = string.Concat(text + "\r\n { label: 'Folder 1', expanded: true, icon: '", "folder.png', items: [");
					goto IL_00ac;
					IL_00ac:
					num2 = 14;
					text = string.Concat(text + "\r\n   { label:'Menu 1', id:'spfmenu0', icon: '", "notepad.png' }");
					goto IL_00c5;
					IL_00c5:
					num2 = 15;
					text = string.Concat(text + "\r\n  ,{ label:'Menu 2', id:'spfmenu1', icon: '", "notepad.png'}");
					goto IL_00de;
					IL_00de:
					num2 = 16;
					text += "\r\n ]}";
					goto IL_00ed;
					IL_00ed:
					num2 = 17;
					text = string.Concat(text + "\r\n,{ label: 'Folder 2', expanded: false, icon: '", "folder.png', items: [");
					goto IL_0106;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				Interaction.MsgBox(text, MsgBoxStyle.Information, title);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 457;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdBrowse2_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string fileName = default(string);
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
				case 244:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002f;
						case 5:
							goto IL_0037;
						case 6:
							goto IL_0046;
						case 7:
							goto IL_006a;
						case 8:
							goto IL_0082;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0046:
					num2 = 6;
					BuildForm.FileOpenSave("O", fileName, "csv+", text + " File", "");
					goto IL_006a;
					IL_006a:
					num2 = 7;
					fileName = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0082;
					IL_0037:
					num2 = 5;
					fileName = TxtIn.Text;
					goto IL_0046;
					IL_0082:
					num2 = 8;
					if (Operators.CompareString(fileName, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text = "TAB";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.CompareString(f_Mode, "MENU", TextCompare: false) == 0)
					{
						goto IL_002f;
					}
					goto IL_0037;
					IL_002f:
					num2 = 4;
					text = "MENU";
					goto IL_0037;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				TxtIn.Text = BuildForm.Strip_Add_MyPCDir("S", fileName);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 244;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdTitle_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmText frmText = default(FrmText);
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
							goto IL_001e;
						case 4:
							goto IL_0027;
						case 5:
							goto IL_0031;
						case 6:
							goto IL_0040;
						case 7:
							goto IL_0050;
						case 8:
							goto IL_0060;
						case 9:
							goto IL_006a;
						case 10:
							goto IL_007a;
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
					num2 = 8;
					frmText.ShowDialog();
					goto IL_006a;
					IL_006a:
					num2 = 9;
					if (!frmText.f_OK)
					{
						break;
					}
					goto IL_007a;
					IL_0050:
					num2 = 7;
					frmText.LblColSpan.Visible = false;
					goto IL_0060;
					IL_007a:
					num2 = 10;
					cmdTitle.Tag = frmText.f_Data;
					break;
					IL_000b:
					num2 = 2;
					text = Conversions.ToString(cmdTitle.Tag);
					goto IL_001e;
					IL_001e:
					num2 = 3;
					frmText = new FrmText();
					goto IL_0027;
					IL_0027:
					num2 = 4;
					frmText.f_Data = text;
					goto IL_0031;
					IL_0031:
					num2 = 5;
					frmText.f_cs = Conversions.ToString(1);
					goto IL_0040;
					IL_0040:
					num2 = 6;
					frmText.cmbColSpan.Visible = false;
					goto IL_0050;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				frmText.Dispose();
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
}

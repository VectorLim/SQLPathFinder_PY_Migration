using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmRWMap : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridCol")]
	private DataGridView _GridCol;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdUp")]
	private Button _CmdUp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDown")]
	private Button _CmdDown;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdDelete")]
	private Button _CmdDelete;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdClear")]
	private Button _CmdClear;

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
	[AccessedThroughProperty("mnuSetColor")]
	private ToolStripMenuItem _mnuSetColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClearColor")]
	private ToolStripMenuItem _mnuClearColor;

	private int lNoRows;

	private const string ColDlm = ";;;";

	private const string RowDLM = "~~~";

	public string f_Value;

	public string f_Column;

	public bool f_ok;

	private const int colg_col = 3;

	internal virtual DataGridView GridCol
	{
		[CompilerGenerated]
		get
		{
			return _GridCol;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = GridCol_CellDoubleClick;
			MouseEventHandler value3 = GridCol_MouseUp;
			KeyEventHandler value4 = GridCol_KeyDown;
			DataGridView dataGridView = _GridCol;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick -= value2;
				dataGridView.MouseUp -= value3;
				dataGridView.KeyDown -= value4;
			}
			_GridCol = value;
			dataGridView = _GridCol;
			if (dataGridView != null)
			{
				dataGridView.CellDoubleClick += value2;
				dataGridView.MouseUp += value3;
				dataGridView.KeyDown += value4;
			}
		}
	}

	[field: AccessedThroughProperty("LblTitle")]
	internal virtual Label LblTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	internal virtual Button CmdDown
	{
		[CompilerGenerated]
		get
		{
			return _CmdDown;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown_Click;
			Button button = _CmdDown;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDown = value;
			button = _CmdDown;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdDelete
	{
		[CompilerGenerated]
		get
		{
			return _CmdDelete;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDelete_Click;
			Button button = _CmdDelete;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdDelete = value;
			button = _CmdDelete;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdClear
	{
		[CompilerGenerated]
		get
		{
			return _CmdClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdClear_Click;
			Button button = _CmdClear;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdClear = value;
			button = _CmdClear;
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
			EventHandler value2 = CMdOK_Click;
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

	[field: AccessedThroughProperty("Cmbcol")]
	internal virtual ComboBox Cmbcol
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColOpr")]
	internal virtual DataGridViewComboBoxColumn ColOpr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColOld")]
	internal virtual DataGridViewTextBoxColumn ColOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColNew")]
	internal virtual DataGridViewTextBoxColumn ColNew
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColColor")]
	internal virtual DataGridViewTextBoxColumn ColColor
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

	[field: AccessedThroughProperty("ContextMenuColEdit")]
	internal virtual ContextMenuStrip ContextMenuColEdit
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuSetColor
	{
		[CompilerGenerated]
		get
		{
			return _mnuSetColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuSetColor_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuSetColor;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuSetColor = value;
			toolStripMenuItem = _mnuSetColor;
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

	public FrmRWMap()
	{
		base.Load += Form1_Load;
		base.Resize += Form1_Resize;
		lNoRows = 50;
		f_Value = "";
		f_Column = "";
		f_ok = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmRWMap));
		this.GridCol = new System.Windows.Forms.DataGridView();
		this.ColOpr = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.ColOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.LblTitle = new System.Windows.Forms.Label();
		this.CmdUp = new System.Windows.Forms.Button();
		this.CmdDown = new System.Windows.Forms.Button();
		this.CmdDelete = new System.Windows.Forms.Button();
		this.CmdClear = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.Cmbcol = new System.Windows.Forms.ComboBox();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.ContextMenuColEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuSetColor = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClearColor = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)this.GridCol).BeginInit();
		this.ContextMenuColEdit.SuspendLayout();
		base.SuspendLayout();
		this.GridCol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridCol.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.GridCol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.GridCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.GridCol.Columns.AddRange(this.ColOpr, this.ColOld, this.ColNew, this.ColColor);
		this.GridCol.Location = new System.Drawing.Point(1, 124);
		this.GridCol.Margin = new System.Windows.Forms.Padding(4);
		this.GridCol.Name = "GridCol";
		this.GridCol.RowHeadersWidth = 50;
		this.GridCol.Size = new System.Drawing.Size(715, 276);
		this.GridCol.StandardTab = true;
		this.GridCol.TabIndex = 1;
		this.ColOpr.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.ColOpr.HeaderText = "Operator";
		this.ColOpr.Items.AddRange("", "==", "%in%", "!=", ">", ">=", "<", "<=", "Between");
		this.ColOpr.Name = "ColOpr";
		this.ColOld.HeaderText = "Value";
		this.ColOld.Name = "ColOld";
		this.ColNew.HeaderText = "Label";
		this.ColNew.Name = "ColNew";
		this.ColColor.HeaderText = "Color";
		this.ColColor.Name = "ColColor";
		this.ColColor.ReadOnly = true;
		this.ColColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.LblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblTitle.Location = new System.Drawing.Point(7, 5);
		this.LblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTitle.Name = "LblTitle";
		this.LblTitle.Size = new System.Drawing.Size(691, 76);
		this.LblTitle.TabIndex = 8;
		this.LblTitle.Text = resources.GetString("LblTitle.Text");
		this.CmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdUp.ImageKey = "Up2.bmp";
		this.CmdUp.Location = new System.Drawing.Point(724, 124);
		this.CmdUp.Margin = new System.Windows.Forms.Padding(4);
		this.CmdUp.Name = "CmdUp";
		this.CmdUp.Size = new System.Drawing.Size(72, 39);
		this.CmdUp.TabIndex = 2;
		this.CmdUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdUp.UseVisualStyleBackColor = true;
		this.CmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDown.ImageKey = "Down.bmp";
		this.CmdDown.Location = new System.Drawing.Point(724, 165);
		this.CmdDown.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDown.Name = "CmdDown";
		this.CmdDown.Size = new System.Drawing.Size(72, 39);
		this.CmdDown.TabIndex = 3;
		this.CmdDown.UseVisualStyleBackColor = true;
		this.CmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdDelete.ImageKey = "Delete.bmp";
		this.CmdDelete.Location = new System.Drawing.Point(724, 223);
		this.CmdDelete.Margin = new System.Windows.Forms.Padding(4);
		this.CmdDelete.Name = "CmdDelete";
		this.CmdDelete.Size = new System.Drawing.Size(72, 39);
		this.CmdDelete.TabIndex = 4;
		this.CmdDelete.UseVisualStyleBackColor = true;
		this.CmdClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdClear.ImageIndex = 3;
		this.CmdClear.Location = new System.Drawing.Point(724, 265);
		this.CmdClear.Margin = new System.Windows.Forms.Padding(4);
		this.CmdClear.Name = "CmdClear";
		this.CmdClear.Size = new System.Drawing.Size(72, 39);
		this.CmdClear.TabIndex = 5;
		this.CmdClear.UseVisualStyleBackColor = true;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.Location = new System.Drawing.Point(724, 1);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(72, 39);
		this.CmdOK.TabIndex = 6;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.Location = new System.Drawing.Point(724, 42);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(72, 39);
		this.CmdCancel.TabIndex = 7;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.Cmbcol.FormattingEnabled = true;
		this.Cmbcol.Location = new System.Drawing.Point(7, 87);
		this.Cmbcol.Margin = new System.Windows.Forms.Padding(4);
		this.Cmbcol.Name = "Cmbcol";
		this.Cmbcol.Size = new System.Drawing.Size(289, 24);
		this.Cmbcol.TabIndex = 0;
		this.ContextMenuColEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ContextMenuColEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mnuSetColor, this.mnuClearColor });
		this.ContextMenuColEdit.Name = "ContextMenuStrip1";
		this.ContextMenuColEdit.Size = new System.Drawing.Size(153, 52);
		this.mnuSetColor.Name = "mnuSetColor";
		this.mnuSetColor.Size = new System.Drawing.Size(152, 24);
		this.mnuSetColor.Text = "Set Color";
		this.mnuClearColor.Name = "mnuClearColor";
		this.mnuClearColor.Size = new System.Drawing.Size(152, 24);
		this.mnuClearColor.Text = "Clear Color";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 404);
		base.Controls.Add(this.Cmbcol);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.CmdClear);
		base.Controls.Add(this.CmdDelete);
		base.Controls.Add(this.CmdDown);
		base.Controls.Add(this.CmdUp);
		base.Controls.Add(this.LblTitle);
		base.Controls.Add(this.GridCol);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmRWMap";
		this.Text = "Create a Computed Column";
		((System.ComponentModel.ISupportInitialize)this.GridCol).EndInit();
		this.ContextMenuColEdit.ResumeLayout(false);
		base.ResumeLayout(false);
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
					case 195:
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
								goto IL_001f;
							case 7:
								goto IL_002e;
							case 8:
								goto IL_003f;
							case 9:
								goto IL_004c;
							case 10:
								goto IL_0073;
							default:
								goto end_IL_0001;
							case 11:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_007c:
						if (num5 > num6)
						{
							goto end_IL_0001_2;
						}
						goto IL_004c;
						IL_004c:
						num2 = 9;
						GridCol.Columns[num5].Width = (int)Math.Round((double)num7 / (double)num8);
						goto IL_0073;
						IL_003f:
						num2 = 8;
						num6 = num8 - 1;
						num5 = 0;
						goto IL_007c;
						IL_0073:
						num2 = 10;
						num5++;
						goto IL_007c;
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
						num9 = 70;
						goto IL_001f;
						IL_001f:
						num2 = 6;
						num8 = GridCol.ColumnCount;
						goto IL_002e;
						IL_002e:
						num2 = 7;
						num7 = GridCol.Width - num9;
						goto IL_003f;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 195;
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

	private void CmdDown_Click(object sender, EventArgs e)
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
				DataGridView MyGrid = GridCol;
				GridModule.Grid_Down(ref MyGrid);
				GridCol = MyGrid;
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
				DataGridView MyGrid = GridCol;
				GridModule.Grid_Up(ref MyGrid);
				GridCol = MyGrid;
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

	private void CmdDelete_Click(object sender, EventArgs e)
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
					MyGrid = GridCol;
					GridModule.Grid_Delete(ref MyGrid);
					GridCol = MyGrid;
					goto IL_0024;
					IL_0024:
					num2 = 3;
					GridCol.RowCount = lNoRows;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				MyGrid = GridCol;
				GridModule.Number_Grid(ref MyGrid);
				GridCol = MyGrid;
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

	private void Form1_Load(object sender, EventArgs e)
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
						errsource = "FrmRWMap - Load";
						Button MyButton = CmdUp;
						BuildForm.Set_Btn_Img(ref MyButton, "up");
						CmdUp = MyButton;
						MyButton = CmdDown;
						BuildForm.Set_Btn_Img(ref MyButton, "down");
						CmdDown = MyButton;
						MyButton = CmdDelete;
						BuildForm.Set_Btn_Img(ref MyButton, "delete");
						CmdDelete = MyButton;
						MyButton = CmdClear;
						BuildForm.Set_Btn_Img(ref MyButton, "clear");
						CmdClear = MyButton;
						int num3 = 0;
						int num4 = 0;
						int num5 = -1;
						string text = "";
						string text2 = "";
						GridCol.RowCount = lNoRows;
						DataGridView MyGrid = GridCol;
						GridModule.Number_Grid(ref MyGrid);
						GridCol = MyGrid;
						Cmbcol.Text = f_Column;
						if (Operators.CompareString(Strings.Trim(f_Value), "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(f_Value, "~~~");
							int num6 = Information.UBound(array);
							for (num3 = 0; num3 <= num6; num3++)
							{
								string[] array2 = Strings.Split(array[num3], ";;;");
								num5++;
								int num7 = GridCol.ColumnCount - 1;
								for (num4 = 0; num4 <= num7; num4++)
								{
									GridCol.Rows[num5].Cells[num4].Value = array2[num4];
								}
								text = Strings.Trim(Conversions.ToString(GridCol.Rows[num5].Cells[3].Value));
								if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text, 1, 1), "#", TextCompare: false) == 0)
								{
									GridCol[3, num5].Style.BackColor = ColorTranslator.FromHtml(text);
								}
								else
								{
									text2 = Conversions.ToString(Operators.ConcatenateObject(text2 + ",", GridCol.Rows[num5].Cells[3].Value));
									GridCol.Rows[num5].Cells[3].Value = "";
								}
								array2 = null;
							}
							array = null;
						}
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							text2 = Strings.Mid(text2, 2);
							Interaction.MsgBox("The wafermap color assignment method has changed. The following colors could present issues and were removed: " + text2 + ". Sorry for any inconvenience. Please reassign by double clicking the color cell or by right-clicking.", MsgBoxStyle.Exclamation, "Potential issue with colors");
						}
						goto end_IL_0001;
					}
					case 746:
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
					goto IL_0320;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 746;
				continue;
			}
			break;
			IL_0320:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void CmdClear_Click(object sender, EventArgs e)
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
				DataGridView MyGrid;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 218:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_002b;
						case 6:
							goto IL_0037;
						case 7:
							goto IL_0052;
						case 8:
							goto IL_0068;
						case 9:
							goto IL_0088;
						default:
							goto end_IL_0001;
						case 10:
						case 11:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0091:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0068;
					IL_0068:
					num2 = 8;
					GridCol[3, num5].Style.BackColor = Color.White;
					goto IL_0088;
					IL_0052:
					num2 = 7;
					num6 = checked(GridCol.RowCount - 1);
					num5 = 0;
					goto IL_0091;
					IL_0088:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_0091;
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
					num7 = (int)Interaction.MsgBox("Are you sure you wish to clear the Grid?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear the Grid?");
					goto IL_002b;
					IL_002b:
					num2 = 5;
					if (num7 != 6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0037;
					IL_0037:
					num2 = 6;
					MyGrid = GridCol;
					GridModule.Clear_A_Grid(ref MyGrid);
					GridCol = MyGrid;
					goto IL_0052;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 218;
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

	private void Form1_Resize(object sender, EventArgs e)
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

	public int Chk_Dup_Color(string MyColor, int MyRow)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		int num5 = default(int);
		int num6 = default(int);
		int result = default(int);
		while (true)
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
				case 273:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
						case 7:
							goto IL_0039;
						case 8:
							goto IL_004f;
						case 9:
							goto IL_007f;
						case 10:
							goto end_IL_0001_2;
						case 12:
						case 13:
							goto IL_00b5;
						default:
							goto end_IL_0001;
						case 6:
						case 11:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004f:
					num2 = 8;
					left = Strings.Trim(Conversions.ToString(GridCol.Rows[num5].Cells[3].Value));
					goto IL_007f;
					IL_007f:
					num2 = 9;
					if (Operators.CompareString(left, "", TextCompare: false) != 0 && num5 != MyRow && Operators.CompareString(left, MyColor, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_00b5;
					IL_00be:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_004f;
					IL_00b5:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_00be;
					IL_000b:
					num2 = 2;
					result = -1;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					left = "";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					if (Operators.CompareString(MyColor, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0039;
					IL_0039:
					num2 = 7;
					num6 = checked(GridCol.RowCount - 1);
					num5 = 0;
					goto IL_00be;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				result = num5;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 273;
				continue;
			}
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

	private void CMdOK_Click(object sender, EventArgs e)
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
						errsource = "FrmRWMap - CmdOK_Click";
						int num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						int num4 = 0;
						if (Operators.CompareString(Strings.Trim(Cmbcol.Text), "", TextCompare: false) == 0)
						{
							Interaction.MsgBox("You must assign a column for the Wafe Map label computation. Do select a column from the drop-down list", MsgBoxStyle.Exclamation, "Missing Column");
							goto end_IL_0001;
						}
						int num5 = GridCol.RowCount - 1;
						num3 = 0;
						while (true)
						{
							if (num3 <= num5)
							{
								text = Strings.Trim(Conversions.ToString(GridCol.Rows[num3].Cells[0].Value));
								text2 = Strings.Trim(Conversions.ToString(GridCol.Rows[num3].Cells[1].Value));
								text3 = Strings.Trim(Conversions.ToString(GridCol.Rows[num3].Cells[2].Value));
								text4 = Strings.Trim(Conversions.ToString(GridCol.Rows[num3].Cells[3].Value));
								if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "", TextCompare: false) != 0))
								{
									if ((Operators.CompareString(text3, "", TextCompare: false) == 0) | (Operators.CompareString(text4, "", TextCompare: false) == 0))
									{
										Interaction.MsgBox("You must assign a label and a color for each condition. Data missing from row " + Conversions.ToString(num3 + 1), MsgBoxStyle.Exclamation, "Invalid Definition");
										break;
									}
									if ((Operators.CompareString(text, "%in%", TextCompare: false) == 0) & !LikeOperator.LikeString(Strings.LCase(text2), "c(*)", CompareMethod.Binary))
									{
										Interaction.MsgBox("For the %in% operator in row " + Conversions.ToString(num3 + 1) + ", you must specify a value using format c(1,2,3) for numbers or c('A','B','C') for text columns.", MsgBoxStyle.Exclamation, "Invalid %in% Condition");
										break;
									}
									if (Operators.CompareString(text.ToLower(), "between", TextCompare: false) == 0 && (!LikeOperator.LikeString(text2, "*,*", CompareMethod.Binary) | LikeOperator.LikeString(text2, "*,*,*", CompareMethod.Binary)))
									{
										Interaction.MsgBox("For the Between operator in row " + Conversions.ToString(num3 + 1) + ", you must separate lower and upper ranges with a single comma. E.g., 5,60 for numbers or 'ABC','DEF' for strings.", MsgBoxStyle.Exclamation, "Invalid Between Condition");
										break;
									}
									num4 = Chk_Dup_Color(text4, num3);
									if (num4 != -1)
									{
										Interaction.MsgBox("You are now allowed to assign duplicate colors. Please adjust rows  " + Conversions.ToString(num3 + 1) + " and " + Conversions.ToString(num4 + 1), MsgBoxStyle.Exclamation, "Duplicate Colors");
										break;
									}
									text5 = text5 + "~~~" + text + ";;;" + text2 + ";;;" + text3 + ";;;" + text4;
								}
								num3++;
								continue;
							}
							text5 = Strings.Trim(text5);
							if (Operators.CompareString(text5, "", TextCompare: false) == 0)
							{
								Interaction.MsgBox("No Label computation was assigned for the WaferMap", MsgBoxStyle.Exclamation, "Missing Label Definition");
								break;
							}
							f_Value = text5;
							f_Value = Strings.Mid(f_Value, Strings.Len("~~~") + 1);
							f_Column = Strings.Trim(Cmbcol.Text);
							f_ok = true;
							Close();
							break;
						}
						goto end_IL_0001_2;
					}
					case 955:
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
					goto IL_03f1;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 955;
				continue;
			}
			break;
			IL_03f1:
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
				DataGridView GridY = GridCol;
				BuildChart.Chart_Clear_Color(ref GridY, 3, 3);
				GridCol = GridY;
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
				DataGridView GridY = GridCol;
				ColorDialog ColorDialog = ColorDialog1;
				BuildChart.Chart_Set_Color(ref GridY, ref ColorDialog, 3, 3);
				ColorDialog1 = ColorDialog;
				GridCol = GridY;
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

	private void GridCol_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
				mnuSetColor_Click(RuntimeHelpers.GetObjectValue(sender), new EventArgs());
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

	private void GridCol_MouseUp(object sender, MouseEventArgs e)
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
				case 130:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					if (e.Button != MouseButtons.Right)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				DataGridView GridY = GridCol;
				ContextMenuStrip ContextMenuColEdit = this.ContextMenuColEdit;
				BuildChart.Chart_Color_MouseUp(ref GridY, ref ContextMenuColEdit, 3, 3, e.X, e.Y);
				this.ContextMenuColEdit = ContextMenuColEdit;
				GridCol = GridY;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 130;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void GridCol_KeyDown(object sender, KeyEventArgs e)
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
					CmdDelete_Click(CmdDelete, new EventArgs());
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
}

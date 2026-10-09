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
internal class frmbinscounters : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TxtHeader")]
	private TextBox _TxtHeader;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdValues")]
	private Button _CmdValues;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdclose")]
	private Button _cmdclose;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdadd")]
	private Button _cmdadd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdPaste")]
	private Button _cmdPaste;

	private short ll_ObjectType;

	private string ll_ini_File;

	public bool ll_Special_Update;

	[field: AccessedThroughProperty("TxtName")]
	public virtual ComboBox TxtName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdLoadGrid")]
	public virtual Button cmdLoadGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtName1")]
	public virtual TextBox TxtName1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkpercent")]
	public virtual CheckBox chkpercent
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual TextBox TxtHeader
	{
		[CompilerGenerated]
		get
		{
			return _TxtHeader;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = TxtHeader_KeyPress;
			TextBox textBox = _TxtHeader;
			if (textBox != null)
			{
				textBox.KeyPress -= value2;
			}
			_TxtHeader = value;
			textBox = _TxtHeader;
			if (textBox != null)
			{
				textBox.KeyPress += value2;
			}
		}
	}

	public virtual Button CmdValues
	{
		[CompilerGenerated]
		get
		{
			return _CmdValues;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdvalues_Click;
			Button button = _CmdValues;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdValues = value;
			button = _CmdValues;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdclose
	{
		[CompilerGenerated]
		get
		{
			return _cmdclose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
			Button button = _cmdclose;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdclose = value;
			button = _cmdclose;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdadd
	{
		[CompilerGenerated]
		get
		{
			return _cmdadd;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdadd_Click;
			Button button = _cmdadd;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdadd = value;
			button = _cmdadd;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TxtHeaderLabel")]
	public virtual Label TxtHeaderLabel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblName")]
	public virtual Label LblName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtLabel")]
	internal virtual TextBox TxtLabel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtOther")]
	internal virtual ComboBox TxtOther
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCloseGrid")]
	internal virtual Button cmdCloseGrid
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdPaste
	{
		[CompilerGenerated]
		get
		{
			return _cmdPaste;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPaste_Click;
			Button button = _cmdPaste;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdPaste = value;
			button = _cmdPaste;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Button1")]
	internal virtual Button Button1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmbinscounters()
	{
		base.Load += frmbinscounters_Load;
		base.Paint += frmbinscounters_Paint;
		base.FormClosed += frmbinscounters_FormClosed;
		ll_ObjectType = 0;
		ll_ini_File = "\\starcolumns.ini";
		ll_Special_Update = false;
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
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdLoadGrid = new System.Windows.Forms.Button();
		this.Button1 = new System.Windows.Forms.Button();
		this.TxtName = new System.Windows.Forms.ComboBox();
		this.TxtName1 = new System.Windows.Forms.TextBox();
		this.chkpercent = new System.Windows.Forms.CheckBox();
		this.TxtHeader = new System.Windows.Forms.TextBox();
		this.CmdValues = new System.Windows.Forms.Button();
		this.cmdclose = new System.Windows.Forms.Button();
		this.cmdadd = new System.Windows.Forms.Button();
		this.TxtHeaderLabel = new System.Windows.Forms.Label();
		this.LblName = new System.Windows.Forms.Label();
		this.TxtLabel = new System.Windows.Forms.TextBox();
		this.TxtOther = new System.Windows.Forms.ComboBox();
		this.cmdCloseGrid = new System.Windows.Forms.Button();
		this.cmdPaste = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.cmdLoadGrid.BackColor = System.Drawing.SystemColors.Control;
		this.cmdLoadGrid.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdLoadGrid.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmdLoadGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdLoadGrid.Location = new System.Drawing.Point(245, 0);
		this.cmdLoadGrid.Name = "cmdLoadGrid";
		this.cmdLoadGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdLoadGrid.Size = new System.Drawing.Size(65, 40);
		this.cmdLoadGrid.TabIndex = 9;
		this.cmdLoadGrid.Text = "Show Grid";
		this.ToolTip1.SetToolTip(this.cmdLoadGrid, "Load a Grid With Help Values After Running Help Query (i.e., clicking Values)");
		this.cmdLoadGrid.UseVisualStyleBackColor = false;
		this.cmdLoadGrid.Visible = false;
		this.Button1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold);
		this.Button1.ForeColor = System.Drawing.Color.Black;
		this.Button1.Location = new System.Drawing.Point(327, 295);
		this.Button1.Name = "Button1";
		this.Button1.Size = new System.Drawing.Size(50, 24);
		this.Button1.TabIndex = 14;
		this.Button1.Text = "?";
		this.ToolTip1.SetToolTip(this.Button1, "Get Operations");
		this.Button1.UseVisualStyleBackColor = true;
		this.Button1.Visible = false;
		this.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtName.BackColor = System.Drawing.SystemColors.Window;
		this.TxtName.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtName.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtName.Location = new System.Drawing.Point(1, 221);
		this.TxtName.Name = "TxtName";
		this.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtName.Size = new System.Drawing.Size(377, 24);
		this.TxtName.TabIndex = 1;
		this.TxtName1.AcceptsReturn = true;
		this.TxtName1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtName1.BackColor = System.Drawing.SystemColors.Window;
		this.TxtName1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtName1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtName1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtName1.Location = new System.Drawing.Point(0, 73);
		this.TxtName1.MaxLength = 0;
		this.TxtName1.Multiline = true;
		this.TxtName1.Name = "TxtName1";
		this.TxtName1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtName1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.TxtName1.Size = new System.Drawing.Size(381, 121);
		this.TxtName1.TabIndex = 0;
		this.TxtName1.Tag = "M";
		this.TxtName1.Visible = false;
		this.chkpercent.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.chkpercent.BackColor = System.Drawing.Color.White;
		this.chkpercent.Cursor = System.Windows.Forms.Cursors.Default;
		this.chkpercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.chkpercent.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkpercent.ForeColor = System.Drawing.SystemColors.WindowText;
		this.chkpercent.Location = new System.Drawing.Point(1, 295);
		this.chkpercent.Name = "chkpercent";
		this.chkpercent.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.chkpercent.Size = new System.Drawing.Size(289, 34);
		this.chkpercent.TabIndex = 4;
		this.chkpercent.Text = "Check to compute percentage rollup";
		this.chkpercent.UseVisualStyleBackColor = false;
		this.chkpercent.Visible = false;
		this.TxtHeader.AcceptsReturn = true;
		this.TxtHeader.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHeader.BackColor = System.Drawing.Color.White;
		this.TxtHeader.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.TxtHeader.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtHeader.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtHeader.Location = new System.Drawing.Point(1, 268);
		this.TxtHeader.MaxLength = 0;
		this.TxtHeader.Name = "TxtHeader";
		this.TxtHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHeader.Size = new System.Drawing.Size(377, 23);
		this.TxtHeader.TabIndex = 2;
		this.CmdValues.BackColor = System.Drawing.SystemColors.Control;
		this.CmdValues.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdValues.Enabled = false;
		this.CmdValues.Font = new System.Drawing.Font("Arial", 8.25f);
		this.CmdValues.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdValues.Location = new System.Drawing.Point(120, 0);
		this.CmdValues.Name = "CmdValues";
		this.CmdValues.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdValues.Size = new System.Drawing.Size(60, 40);
		this.CmdValues.TabIndex = 8;
		this.CmdValues.Text = "Values";
		this.CmdValues.UseVisualStyleBackColor = false;
		this.cmdclose.BackColor = System.Drawing.SystemColors.Control;
		this.cmdclose.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdclose.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmdclose.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdclose.Location = new System.Drawing.Point(60, 0);
		this.cmdclose.Name = "cmdclose";
		this.cmdclose.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdclose.Size = new System.Drawing.Size(60, 40);
		this.cmdclose.TabIndex = 7;
		this.cmdclose.Text = "Close";
		this.cmdclose.UseVisualStyleBackColor = false;
		this.cmdadd.BackColor = System.Drawing.SystemColors.Control;
		this.cmdadd.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdadd.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmdadd.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdadd.Location = new System.Drawing.Point(0, 0);
		this.cmdadd.Name = "cmdadd";
		this.cmdadd.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdadd.Size = new System.Drawing.Size(60, 40);
		this.cmdadd.TabIndex = 6;
		this.cmdadd.Text = "Add";
		this.cmdadd.UseVisualStyleBackColor = false;
		this.TxtHeaderLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.TxtHeaderLabel.AutoSize = true;
		this.TxtHeaderLabel.BackColor = System.Drawing.Color.Transparent;
		this.TxtHeaderLabel.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtHeaderLabel.Font = new System.Drawing.Font("Arial", 8.25f);
		this.TxtHeaderLabel.ForeColor = System.Drawing.SystemColors.WindowText;
		this.TxtHeaderLabel.Location = new System.Drawing.Point(1, 252);
		this.TxtHeaderLabel.Name = "TxtHeaderLabel";
		this.TxtHeaderLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHeaderLabel.Size = new System.Drawing.Size(115, 16);
		this.TxtHeaderLabel.TabIndex = 8;
		this.TxtHeaderLabel.Text = "Optional Header:";
		this.LblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.LblName.AutoSize = true;
		this.LblName.BackColor = System.Drawing.Color.Transparent;
		this.LblName.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblName.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LblName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.LblName.Location = new System.Drawing.Point(1, 204);
		this.LblName.Name = "LblName";
		this.LblName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblName.Size = new System.Drawing.Size(187, 16);
		this.LblName.TabIndex = 7;
		this.LblName.Text = "Bin, Counter,or Rollup Name";
		this.TxtLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLabel.BackColor = System.Drawing.SystemColors.Window;
		this.TxtLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.TxtLabel.Location = new System.Drawing.Point(0, 40);
		this.TxtLabel.Multiline = true;
		this.TxtLabel.Name = "TxtLabel";
		this.TxtLabel.ReadOnly = true;
		this.TxtLabel.Size = new System.Drawing.Size(378, 216);
		this.TxtLabel.TabIndex = 11;
		this.TxtLabel.TabStop = false;
		this.TxtOther.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.TxtOther.FormattingEnabled = true;
		this.TxtOther.Items.AddRange(new object[6] { "", "1", "2", "3", "4", "5" });
		this.TxtOther.Location = new System.Drawing.Point(327, 268);
		this.TxtOther.Name = "TxtOther";
		this.TxtOther.Size = new System.Drawing.Size(50, 24);
		this.TxtOther.TabIndex = 3;
		this.TxtOther.Visible = false;
		this.cmdCloseGrid.BackColor = System.Drawing.SystemColors.Control;
		this.cmdCloseGrid.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdCloseGrid.Location = new System.Drawing.Point(310, 0);
		this.cmdCloseGrid.Name = "cmdCloseGrid";
		this.cmdCloseGrid.Size = new System.Drawing.Size(65, 40);
		this.cmdCloseGrid.TabIndex = 12;
		this.cmdCloseGrid.Text = "Close Grid";
		this.cmdCloseGrid.UseVisualStyleBackColor = false;
		this.cmdCloseGrid.Visible = false;
		this.cmdPaste.BackColor = System.Drawing.SystemColors.Control;
		this.cmdPaste.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdPaste.Location = new System.Drawing.Point(180, 0);
		this.cmdPaste.Name = "cmdPaste";
		this.cmdPaste.Size = new System.Drawing.Size(65, 40);
		this.cmdPaste.TabIndex = 13;
		this.cmdPaste.Text = "Paste";
		this.cmdPaste.UseVisualStyleBackColor = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.cmdclose;
		base.ClientSize = new System.Drawing.Size(380, 397);
		base.Controls.Add(this.Button1);
		base.Controls.Add(this.cmdPaste);
		base.Controls.Add(this.cmdCloseGrid);
		base.Controls.Add(this.TxtOther);
		base.Controls.Add(this.TxtName);
		base.Controls.Add(this.TxtName1);
		base.Controls.Add(this.chkpercent);
		base.Controls.Add(this.cmdLoadGrid);
		base.Controls.Add(this.TxtHeader);
		base.Controls.Add(this.cmdadd);
		base.Controls.Add(this.TxtHeaderLabel);
		base.Controls.Add(this.CmdValues);
		base.Controls.Add(this.LblName);
		base.Controls.Add(this.cmdclose);
		base.Controls.Add(this.TxtLabel);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8.25f);
		this.ForeColor = System.Drawing.Color.Black;
		base.Location = new System.Drawing.Point(427, 142);
		base.Name = "frmbinscounters";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Sort/Class Bin and Counter Rollups";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Get_SQL(short MyMode, ref string MySQL, ref string MyComm, string MyCol, string MyData)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myKey = default(string);
		while (true)
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
				case 385:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0027;
						case 6:
							goto IL_0055;
						case 7:
							goto IL_0087;
						case 8:
							goto IL_009f;
						case 9:
							goto IL_00b4;
						case 10:
							goto IL_00cd;
						case 11:
						case 12:
							goto IL_00e5;
						case 13:
							goto IL_00fe;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cd:
					num2 = 10;
					MySQL = Strings.Replace(MySQL, "<d>", MyData, 1, -1, CompareMethod.Text);
					goto IL_00e5;
					IL_00e5:
					num2 = 12;
					if (Operators.CompareString(MyComm, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00fe;
					IL_00b4:
					num2 = 9;
					MySQL = Strings.Replace(MySQL, "<ai>", Globals_Renamed.currprompttmp, 1, -1, CompareMethod.Text);
					goto IL_00cd;
					IL_00fe:
					num2 = 13;
					MyComm = Strings.Replace(MyComm, "<c>", MyCol, 1, -1, CompareMethod.Text);
					break;
					IL_000b:
					num2 = 2;
					myKey = "sql";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (MyMode == 2)
					{
						goto IL_001f;
					}
					goto IL_0027;
					IL_001f:
					num2 = 4;
					myKey = "sql2";
					goto IL_0027;
					IL_0027:
					num2 = 5;
					MySQL = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, myKey, "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
					goto IL_0055;
					IL_0055:
					num2 = 6;
					MyComm = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "comment", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
					goto IL_0087;
					IL_0087:
					num2 = 7;
					if (Operators.CompareString(MySQL, "", TextCompare: false) != 0)
					{
						goto IL_009f;
					}
					goto IL_00e5;
					IL_009f:
					num2 = 8;
					MySQL = Strings.Replace(MySQL, "<c>", MyCol, 1, -1, CompareMethod.Text);
					goto IL_00b4;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				MyComm = Strings.Replace(MyComm, "<d>", MyData, 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 385;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public void SetFrmCtrls2(short D0, short D3, short MyMode)
	{
		checked
		{
			if (D0 > 40)
			{
				D0 -= 40;
			}
			if (MyMode != 4)
			{
				base.Height = base.Height - D0 + 0;
			}
			ForeColor = ColorTranslator.FromOle(-2147483640);
			if (MyMode != 4)
			{
				TxtName.Top -= D3;
				LblName.Top -= D3;
			}
			CmdValues.Enabled = true;
			switch (MyMode)
			{
			case 0:
				TxtHeader.Top -= D3;
				TxtHeader.Width = TxtName.Width;
				TxtHeaderLabel.Top -= D3;
				chkpercent.Top -= D3;
				chkpercent.Visible = true;
				break;
			case 1:
				chkpercent.Top = TxtHeader.Top - D3;
				TxtHeader.Visible = false;
				TxtHeaderLabel.Visible = false;
				chkpercent.Visible = true;
				break;
			case 2:
				TxtHeader.Visible = false;
				TxtHeaderLabel.Visible = false;
				break;
			case 3:
			case 5:
				TxtHeader.Top -= D3;
				TxtHeader.Width = TxtName.Width;
				TxtHeaderLabel.Top -= D3;
				if (MyMode == 5)
				{
					TxtHeader.Enabled = false;
					TxtHeaderLabel.Enabled = false;
				}
				break;
			case 4:
				TxtName1.Top = TxtName.Top - D3;
				TxtName.Visible = false;
				TxtHeader.Visible = false;
				TxtHeaderLabel.Visible = false;
				TxtName1.Visible = true;
				break;
			}
		}
	}

	public void Add_To_SQL_Grid(short MyIndex, string declarevalue, string MyHeader, string MyDataType, string MyComment, string MyHeaders2)
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
				string text3;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "frmBinsCounter - Add_To_SQL_Grid";
					string text = "";
					string strHdr = "";
					short num3 = 0;
					string text2 = "None";
					text3 = "";
					string myHdrAction = "Y";
					text3 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "mode", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
					if (MyIndex == 0)
					{
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 15), "*select-a-field", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-class-oper*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-sort-oper*", TextCompare: false) == 0)
						{
							myHdrAction = "N";
						}
						if (ll_Special_Update)
						{
							Globals_Renamed.currvaluetmp = declarevalue;
							goto end_IL_0001;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*wip-spc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) == 0)
						{
							strHdr = Strings.Replace(Globals_Renamed.currvaluetmp, "*", "+", 1, -1, CompareMethod.Text);
						}
						else if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 27), "*start-imbigdata-operation-", TextCompare: false) == 0)
						{
							myHdrAction = "I";
							MyHeader = "Start->[" + MyHeader + "]";
						}
						else
						{
							strHdr = "";
						}
						text2 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "summary", "None", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
						MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Columns_Grid(MyHeader, declarevalue, Globals_Renamed.currDataAny, "", text2, "None", MyDataType, MyComment, Globals_Renamed.currtxttmp, strHdr, MyHeaders2, myHdrAction);
						goto IL_0264;
					}
					MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Add_Row_To_Filters_Grid("f(x)->" + Strings.LCase(MyHeader), declarevalue, Globals_Renamed.currDataAny, "", MyDataType, "X", Globals_Renamed.currtxttmp, "");
					goto IL_0264;
				}
				case 921:
					{
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
					IL_0264:
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-primary-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*ep-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*wip-spc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) == 0 || Operators.CompareString(text3, "col-header", TextCompare: false) == 0 || Operators.CompareString(text3, "col-col-header", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0)
					{
						TxtName1.Text = "";
						TxtHeader.Text = "";
						TxtName1.Focus();
					}
					else
					{
						TxtName.Text = "";
						TxtHeader.Text = "";
						TxtName.Focus();
					}
					goto end_IL_0001_2;
				}
				goto IL_03cf;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 921;
				continue;
			}
			break;
			IL_03cf:
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

	private void cmdadd_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text = default(string);
		string[] DynArray = default(string[]);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					short num5;
					string myDataType;
					string text9;
					string MyComm;
					string MySQL;
					string text3;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "frmBinsCounter - cmdadd_Click";
						MySQL = "";
						myDataType = "c";
						text = "";
						string text2 = "";
						text3 = "";
						short num3 = 0;
						short num4 = 0;
						string text4 = "";
						string text5 = "";
						string text6 = "";
						num5 = 0;
						MyComm = "";
						string text7 = "";
						short num6 = 0;
						int num7 = 0;
						short num8 = 0;
						short num9 = 0;
						string text8 = "";
						text9 = "";
						string text10 = "";
						string text11 = "";
						string text12 = "";
						string text13 = "0";
						string text14 = "";
						bool flag = false;
						text11 = Strings.LCase(Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "dt", "", 5, Globals_Renamed.MySchemaDir + ll_ini_File)));
						text10 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "prefix", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text11, "", TextCompare: false) != 0)
						{
							myDataType = text11;
						}
						text12 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "mode", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
						text13 = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "special", "0", 10, Globals_Renamed.MySchemaDir + ll_ini_File));
						text = ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-primary-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*ep-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*wip-spc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) == 0 || Operators.CompareString(text12, "col-header", TextCompare: false) == 0 || Operators.CompareString(text12, "col-col-header", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 15), "*select-a-field", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-class-oper*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-sort-oper*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0) ? Strings.Trim(TxtName1.Text) : ((Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 5), "*yas-", TextCompare: false) != 0) ? Strings.Trim(Strings.LCase(TxtName.Text)) : Strings.Trim(TxtName.Text)));
						short num10 = 0;
						short num11 = 0;
						string text15 = "";
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							break;
						}
						TxtHeader.Text = Strings.Trim(TxtHeader.Text);
						num5 = (short)MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].SSTab1.SelectedIndex;
						if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*bins-ctrs-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*bins-ctrs-wafers-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*btr-bins-ctrs-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*rollup-name*", TextCompare: false) == 0))
						{
							myDataType = "n";
							if (Strings.InStr(text, "@") != 0)
							{
								num3 = (short)Strings.InStr(text, "@");
								text15 = Strings.UCase(Strings.Mid(text, 1, 2));
								if ((Operators.CompareString(text15, "IB", TextCompare: false) == 0) | (Operators.CompareString(text15, "DB", TextCompare: false) == 0) | (Operators.CompareString(text15, "FB", TextCompare: false) == 0) | (Operators.CompareString(text15, "OI", TextCompare: false) == 0) | (Operators.CompareString(text15, "OF", TextCompare: false) == 0) | (Operators.CompareString(text15, "OP", TextCompare: false) == 0) | (Operators.CompareString(text15, "PC", TextCompare: false) == 0) | (Operators.CompareString(text15, "FC", TextCompare: false) == 0))
								{
									ProjectData.ClearProjectError();
									num2 = 3;
									num4 = unchecked((short)((Operators.CompareString(Strings.Mid(text15, 1, 1), "O", TextCompare: false) != 0) ? 3 : 4));
									num10 = (short)Math.Round(Conversion.Val(Strings.Mid(text, num4, num3 - 1)));
									num11 = (short)Math.Round(Conversion.Val(Strings.Mid(text, (short)unchecked(num3 + num4))));
									if (!unchecked(num10 < 0 || num11 <= 0))
									{
										ProjectData.ClearProjectError();
										num2 = 4;
										short num12 = num10;
										short num13 = num11;
										for (num6 = num12; num6 <= num13; num6 = (short)unchecked(num6 + 1))
										{
											text2 = text15 + Conversions.ToString(unchecked((int)num6));
											text5 = Strings.UCase(Strings.Mid(text2, 1, num4 - 1));
											text4 = Strings.Mid(text2, num4);
											if (chkpercent.CheckState == CheckState.Unchecked)
											{
												text3 = text2;
												Get_SQL(1, ref MySQL, ref MyComm, text5, text4);
											}
											else
											{
												myDataType = "f";
												text3 = text2 + "p";
												Get_SQL(2, ref MySQL, ref MyComm, text5, text4);
											}
											Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
										}
										ProjectData.ClearProjectError();
										num2 = 5;
										break;
									}
								}
								goto IL_0597;
							}
							text2 = Strings.UCase(text);
							text15 = Strings.Mid(text2, 1, 2);
							if ((Operators.CompareString(text15, "IB", TextCompare: false) == 0) | (Operators.CompareString(text15, "DB", TextCompare: false) == 0) | (Operators.CompareString(text15, "FB", TextCompare: false) == 0) | (Operators.CompareString(text15, "OI", TextCompare: false) == 0) | (Operators.CompareString(text15, "OF", TextCompare: false) == 0) | (Operators.CompareString(text15, "OP", TextCompare: false) == 0) | (Operators.CompareString(text15, "PC", TextCompare: false) == 0) | (Operators.CompareString(text15, "FC", TextCompare: false) == 0))
							{
								num4 = unchecked((short)((Operators.CompareString(Strings.Mid(text2, 1, 1), "O", TextCompare: false) != 0) ? 3 : 4));
								text5 = Strings.Mid(text2, 1, num4 - 1);
								text4 = Strings.Mid(text2, num4);
							}
							else
							{
								text5 = text2;
								text4 = "-1";
							}
							if (chkpercent.CheckState == CheckState.Unchecked)
							{
								Get_SQL(1, ref MySQL, ref MyComm, text5, text4);
							}
							else
							{
								myDataType = "f";
								text2 = text + "p";
								Get_SQL(2, ref MySQL, ref MyComm, text5, text4);
							}
							Add_To_SQL_Grid(num5, MySQL, text2, myDataType, MyComm, "");
							break;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*other-entity*", TextCompare: false) == 0)
						{
							text = Strings.Trim(Strings.UCase(text));
							text2 = ((Operators.CompareString(Strings.Trim(TxtHeader.Text), "", TextCompare: false) == 0) ? "" : ("AND leh.entity LIKE '" + Strings.Trim(Strings.UCase(TxtHeader.Text)) + "%' ESCAPE '/'"));
							text3 = "entity_" + text;
							Get_SQL(1, ref MySQL, ref MyComm, text, text2);
							Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							if (chkpercent.CheckState == CheckState.Checked)
							{
								text3 = "txn_date_" + text;
								myDataType = "d";
								Get_SQL(2, ref MySQL, ref MyComm, text, text2);
								Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							}
							break;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*other-entity-wafer*", TextCompare: false) == 0)
						{
							text = Strings.Trim(Strings.UCase(text));
							MyComm = "***Stores data for the last entity collected for the wafer scribe at operation <c>";
							text2 = ((Operators.CompareString(Strings.Trim(TxtHeader.Text), "", TextCompare: false) == 0) ? "" : (" AND n99.entity LIKE '" + Strings.Trim(Strings.UCase(TxtHeader.Text)) + "%' ESCAPE '/'"));
							if (LikeOperator.LikeString(Strings.UCase(text), "@SPC_PROC_OPER_MAP@", CompareMethod.Binary))
							{
								text6 = text;
								text3 = "entity_map";
							}
							else
							{
								text6 = "'" + text + "'";
								text3 = "entity_" + text;
							}
							Get_SQL(1, ref MySQL, ref MyComm, text, text6 + text2);
							Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							if (chkpercent.CheckState == CheckState.Checked)
							{
								text3 = ((!LikeOperator.LikeString(Strings.UCase(text), "@SPC_PROC_OPER_MAP@", CompareMethod.Binary)) ? ("txn_date_" + text) : "entity_txn_date_map");
								myDataType = "d";
								Get_SQL(2, ref MySQL, ref MyComm, text, text6 + text2);
								Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							}
							break;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*other-in-out-date*", TextCompare: false) == 0)
						{
							text = Strings.Trim(Strings.UCase(text));
							text3 = text10 + text;
							Get_SQL(1, ref MySQL, ref MyComm, text, "");
							Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							if (chkpercent.CheckState == CheckState.Checked)
							{
								text3 = "in_date_" + text;
								Get_SQL(2, ref MySQL, ref MyComm, text, "");
								Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							}
							break;
						}
						if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-attributes*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-primary-attributes*", TextCompare: false) == 0))
						{
							switch (Globals_Renamed.currprompttmp.ToLower())
							{
							case "apcl":
								text7 = "P_APC_LOT_VALUES";
								break;
							case "apcm":
								text7 = "P_APC_MODEL_VALUES";
								break;
							case "apclk":
								text7 = "P_APC_LOOKUP_VALUES";
								break;
							case "apcg":
								text7 = "P_APC_GENEALOGY_VALUES";
								break;
							}
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num14 = (short)num7;
							for (num6 = 1; num6 <= num14; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
								{
									text = "";
									text3 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text3 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
									}
									else
									{
										text = Strings.Trim(DynArray[num6]);
									}
									text = General_Procedures.StripQuotesAndDates(text, "C", 0);
									text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
									if (Operators.CompareString(text3, "", TextCompare: false) == 0)
									{
										text3 = "la_" + text;
									}
									else if (Operators.CompareString(Strings.UCase(text3), "MODE", TextCompare: false) == 0)
									{
										text3 = Strings.Trim(text3) + "1";
									}
									text3 = General_Procedures.Replace_Special_Chars(text3);
									if (LikeOperator.LikeString(Strings.UCase(text), "GEN_ATTR*", CompareMethod.Binary))
									{
										MySQL = Strings.UCase(Globals_Renamed.currprompttmp) + "." + Strings.LCase(text);
										MyComm = "***Stores the value of a primary APC attribute - " + text3;
									}
									else
									{
										MySQL = "(SELECT /*+ ORDERED */ v1.value FROM " + text7 + "@OASYSNODE@ v1 WHERE v1.attributeid = " + text + " AND v1.offline_row_id = " + Globals_Renamed.currprompttmp + ".offline_row_id AND rownum <= 1)";
										MyComm = "***Stores the value of a Secondary APC attribute - " + text3;
									}
									Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
								}
							}
							DynArray = null;
							break;
						}
						if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 22), "*entity-ctr-attributes", TextCompare: false) == 0)
						{
							text = Strings.Trim(TxtName.Text);
							text3 = ((Operators.CompareString(Strings.Trim(TxtHeader.Text), "", TextCompare: false) != 0) ? Strings.Trim(TxtHeader.Text) : (text10 + General_Procedures.Replace_Special_Chars(text)));
							text9 = Strings.Trim(TxtOther.Text);
							if (Operators.CompareString(text9, "", TextCompare: false) == 0)
							{
								text9 = Conversions.ToString(1);
								goto IL_0de2;
							}
							if (Versioned.IsNumeric(text9))
							{
								goto IL_0de2;
							}
							Interaction.MsgBox("You must enter a numeric number of days to go back to retrieve the latest counter/attribute value", MsgBoxStyle.Exclamation, "Invalid Entry");
							goto end_IL_0001;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field*", TextCompare: false) == 0)
						{
							text = Strings.Trim(TxtName.Text);
							if (Operators.CompareString(Strings.Trim(TxtHeader.Text), "", TextCompare: false) == 0)
							{
								Interaction.MsgBox("You must enter one or more values that result in the signal field being assigned a 1", MsgBoxStyle.Exclamation, "Invalid Entry");
								goto end_IL_0001;
							}
							string[] array = null;
							text7 = Strings.Trim(TxtHeader.Text);
							array = Strings.Split(text7, ",");
							text7 = "";
							short num15 = (short)Information.UBound(array);
							for (num6 = 0; num6 <= num15; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(array[num6]), "", TextCompare: false) != 0)
								{
									text7 = text7 + "," + Strings.Trim(array[num6]);
								}
							}
							text7 = Strings.Mid(text7, 2);
							array = null;
							text9 = Strings.Trim(TxtOther.Text);
							switch (text9)
							{
							default:
								if (Operators.CompareString(text9, "0", TextCompare: false) == 0)
								{
									break;
								}
								goto case null;
							case null:
							case "":
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field*", TextCompare: false) == 0)
								{
									text9 = "'$null'";
								}
								break;
							case "1":
								break;
							}
							Get_SQL(1, ref MySQL, ref MyComm, text, text9);
							MySQL = Strings.Replace(MySQL, "<hdr>", text7, 1, -1, CompareMethod.Text);
							Add_To_SQL_Grid(num5, MySQL, "s#" + text, myDataType, MyComm, "");
							break;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0)
						{
							text7 = text;
							text = TxtHeader.Text;
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								Interaction.MsgBox("Missing Signal Field Name", MsgBoxStyle.Exclamation, "Invalid");
								goto end_IL_0001;
							}
							if (Operators.CompareString(Strings.LCase(Strings.Mid(text, 1, 2)), "s#", TextCompare: false) != 0)
							{
								text = "s#" + text;
							}
							string[] array2 = null;
							array2 = Strings.Split(text7, "\r\n");
							text7 = "";
							string text16 = "if";
							MySQL = "";
							short num16 = (short)Information.UBound(array2);
							num6 = 0;
							while (true)
							{
								if (num6 <= num16)
								{
									text7 = Strings.Trim(array2[num6]);
									if (Operators.CompareString(text7, "", TextCompare: false) == 0)
									{
										goto IL_1256;
									}
									num3 = (short)Strings.InStrRev(text7, ">>");
									if (num3 != 0)
									{
										string text17 = Strings.Trim(Strings.Mid(text7, 1, num3 - 1));
										string text18 = Strings.Trim(Strings.Mid(text7, num3 + 2));
										if (Operators.CompareString(Strings.LCase(text18), "null", TextCompare: false) == 0)
										{
											text18 = "null";
										}
										if (Operators.CompareString(Strings.LCase(text17), "null", TextCompare: false) == 0)
										{
											text17 = "null";
										}
										if (Strings.Len(text17) > 4 && Operators.CompareString(Strings.UCase(Strings.Mid(text17, Strings.Len(text17) - 3, 4)), "NULL", TextCompare: false) == 0)
										{
											text17 = Strings.Mid(text17, 1, Strings.Len(text17) - 4) + "null";
										}
										if (Operators.CompareString(text17, "", TextCompare: false) != 0 && Operators.CompareString(text18, "", TextCompare: false) != 0)
										{
											MySQL = MySQL + text16 + " (" + text17 + ") { " + text18 + " }";
											text16 = " else if";
											goto IL_1256;
										}
										Interaction.MsgBox("Missing either the condition statement or the value in the following line of the signal field:\r\n\r\n" + text7 + "\r\n\r\nExpecting \"<condition> >> <value>\"", MsgBoxStyle.Exclamation, "Invalid");
										break;
									}
									if (Operators.CompareString(MySQL, "", TextCompare: false) == 0)
									{
										Interaction.MsgBox("An ELSE condition cannot be the first condition of a signal field. Expecting \"<condition> >> <value>\"", MsgBoxStyle.Exclamation, "Invalid");
										break;
									}
									MySQL = MySQL + " else { " + text7 + " }";
								}
								array2 = null;
								MySQL = Strings.Replace(MySQL, "{", "<~sqos~>", 1, -1, CompareMethod.Text);
								MySQL = Strings.Replace(MySQL, "}", "<~sqcs~>", 1, -1, CompareMethod.Text);
								MyComm = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "comment", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
								Add_To_SQL_Grid(num5, MySQL, text, myDataType, MyComm, "");
								goto end_IL_0001_2;
								IL_1256:
								num6 = (short)unchecked(num6 + 1);
							}
							goto end_IL_0001;
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*entity-attributes2*", TextCompare: false) == 0)
						{
							text = Strings.Trim(Strings.UCase(text));
							text3 = text10 + text;
							text3 = General_Procedures.Replace_Special_Chars(text3);
							Get_SQL(1, ref MySQL, ref MyComm, text, "");
							Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							if (chkpercent.CheckState == CheckState.Checked)
							{
								text3 = "old_" + text;
								text3 = General_Procedures.Replace_Special_Chars(text3);
								MySQL = "";
								Get_SQL(2, ref MySQL, ref MyComm, text, "");
								Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							}
						}
						else if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*ep-attributes*", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 15), "*select-a-field", TextCompare: false) == 0)
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num17 = (short)num7;
							for (num6 = 1; num6 <= num17; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
								{
									text = "";
									text3 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text3 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
									}
									else
									{
										text = Strings.Trim(DynArray[num6]);
									}
									if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*ep-attributes*", TextCompare: false) == 0)
									{
										text = General_Procedures.StripQuotesAndDates(text, "C", 0);
										text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
										if (Operators.CompareString(text3, "", TextCompare: false) == 0)
										{
											text3 = "ep_" + text;
										}
										text3 = General_Procedures.Replace_Special_Chars(text3);
									}
									else if (Operators.CompareString(text3, "", TextCompare: false) == 0)
									{
										text3 = text;
									}
									Get_SQL(1, ref MySQL, ref MyComm, text, "");
									Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
								}
							}
							DynArray = null;
						}
						else if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-class-oper*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-sort-oper*", TextCompare: false) == 0)
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num18 = (short)num7;
							for (num6 = 1; num6 <= num18; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
								{
									text = "";
									text2 = "";
									text3 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text2 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
										text3 = Strings.LCase(text2 + "@" + text);
										text = General_Procedures.StripQuotesAndDates(text, "C", 0);
										text2 = General_Procedures.StripQuotesAndDates(text2, "C", 0);
										text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
										MySQL = Globals_Renamed.currprompttmp;
										MySQL = Strings.Replace(MySQL, "<1>", text, 1, 1);
										MySQL = Strings.Replace(MySQL, "<2>", text2, 1, 1);
										MyComm = Globals_Renamed.currhelptmp;
										num3 = (short)Strings.InStr(MyComm, "@");
										if (num3 != 0)
										{
											MyComm = "***" + Strings.Mid(MyComm, 1, num3 - 1) + ": " + text2 + " at operation: " + text;
										}
										Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
									}
								}
							}
							DynArray = null;
						}
						else if (Operators.CompareString(text12, "col-col-header", TextCompare: false) == 0)
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num19 = (short)num7;
							for (num6 = 1; num6 <= num19; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
								{
									text = "";
									text3 = "";
									text2 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text2 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
										num9 = (short)Strings.InStr(text2, ",");
										if (num9 != 0)
										{
											text3 = Strings.Trim(Strings.Mid(text2, num9 + 1));
											text2 = Strings.Trim(Strings.Mid(text2, 1, num9 - 1));
										}
									}
									else
									{
										text = Strings.Trim(DynArray[num6]);
									}
									text = General_Procedures.StripQuotesAndDates(text, "C", 0);
									text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
									text2 = General_Procedures.StripQuotesAndDates(text2, "C", 0);
									text7 = text3;
									if (Operators.CompareString(text3, "", TextCompare: false) == 0)
									{
										text3 = text10 + text + "_" + text2;
									}
									text3 = Strings.LCase(General_Procedures.Replace_Special_Chars(text3));
									if (Strings.Len(text3) > 30)
									{
										Interaction.MsgBox("Column headers must be less than 30 characters. Please assign a header for columns" + text + " and " + text2 + ". Header=" + text3, MsgBoxStyle.Exclamation, "Column Header too long");
										break;
									}
									Get_SQL(1, ref MySQL, ref MyComm, text, text2);
									Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
								}
							}
							DynArray = null;
						}
						else if (Operators.CompareString(text12, "col-header", TextCompare: false) == 0)
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num20 = (short)num7;
							for (num6 = 1; num6 <= num20; num6 = (short)unchecked(num6 + 1))
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
								{
									text14 = ((Operators.CompareString(text13, "1", TextCompare: false) != 0) ? "" : ".");
									text = "";
									text3 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text3 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
									}
									else
									{
										text = Strings.Trim(DynArray[num6]);
									}
									text = General_Procedures.StripQuotesAndDates(text, "C", 0);
									text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
									if (Operators.CompareString(text3, "", TextCompare: false) == 0)
									{
										text3 = Strings.LCase(text);
										text3 = General_Procedures.Replace_Special_Chars(text3);
										if ((Operators.CompareString(Strings.Mid(text3 + " ", 1, 1), "0", TextCompare: false) >= 0) & (Operators.CompareString(Strings.Mid(text3 + " ", 1, 1), "9", TextCompare: false) <= 0))
										{
											text3 = text10 + text;
										}
										if (Operators.CompareString(text13, "0", TextCompare: false) == 0)
										{
											if (Strings.Len(text3) > 30)
											{
												Interaction.MsgBox("Column headers must be less than 30 characters. Please change the header for column " + text + ", header: " + text3, MsgBoxStyle.Exclamation, "Column Header too long");
												break;
											}
										}
										else if (Strings.Len(text3) > 30)
										{
											text3 = Strings.Mid(text3, 1, 27).Trim();
											text3 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Valid_Hdr(text3, "a0");
											text14 = General_Procedures.Replace_vbcrlf(text);
										}
									}
									else
									{
										text3 = Strings.LCase(General_Procedures.Replace_Special_Chars(text3));
										if ((Operators.CompareString(Strings.Mid(text3 + " ", 1, 1), "0", TextCompare: false) >= 0) & (Operators.CompareString(Strings.Mid(text3 + " ", 1, 1), "9", TextCompare: false) <= 0))
										{
											text3 = text10 + text;
										}
										if (Strings.Len(text3) > 30)
										{
											Interaction.MsgBox("Column headers must be less than 30 characters. Please change the header for column " + text + ", header: " + text3, MsgBoxStyle.Exclamation, "Column Header too long");
											break;
										}
									}
									Get_SQL(1, ref MySQL, ref MyComm, text, "");
									if (Operators.CompareString(text13, "1", TextCompare: false) == 0)
									{
										Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, text14);
									}
									else
									{
										Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
									}
								}
							}
							DynArray = null;
						}
						else if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-defect-ncdd*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-defect-ncdd-1266*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-ncdd-class*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-ncdd-finebin*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-class-names*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-class-names-1266*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-finebin-names*", TextCompare: false) == 0))
						{
							string text16 = "";
							text = Strings.Trim(text);
							if (chkpercent.CheckState == CheckState.Checked)
							{
								text16 = " AND y901.Adder = 1 ";
							}
							Get_SQL(1, ref MySQL, ref MyComm, text, text16);
							Add_To_SQL_Grid(num5, MySQL, text10 + text, myDataType, MyComm, "");
						}
						else if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*wip-spc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) == 0)
						{
							Globals_Renamed.currprompttmp = Strings.LCase(Globals_Renamed.currprompttmp);
							Get_SQL(1, ref MySQL, ref MyComm, text, "");
							MySQL = "";
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							num7 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
							short num21 = (short)num7;
							for (num6 = 1; num6 <= num21; num6 = (short)unchecked(num6 + 1))
							{
								DynArray[num6] = Strings.Trim(DynArray[num6]);
								if ((Operators.CompareString(DynArray[num6], "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Mid(DynArray[num6], 1, 1), "!", TextCompare: false) != 0))
								{
									text = "";
									text3 = "";
									num8 = (short)Strings.InStr(DynArray[num6], ",");
									if (num8 != 0)
									{
										text = Strings.Trim(Strings.Mid(DynArray[num6], 1, num8 - 1));
										text3 = Strings.Trim(Strings.Mid(DynArray[num6], num8 + 1));
										text = General_Procedures.StripQuotesAndDates(text, "C", 0);
										text3 = General_Procedures.StripQuotesAndDates(text3, "C", 0);
										if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text3, "", TextCompare: false) != 0))
										{
											if (Operators.CompareString(MySQL, "", TextCompare: false) == 0)
											{
												MySQL = "DECODE(" + Globals_Renamed.currprompttmp;
											}
											MySQL = MySQL + ",'" + text + "','" + text3 + "'";
										}
									}
								}
							}
							DynArray = null;
							if (Operators.CompareString(MySQL, "", TextCompare: false) != 0)
							{
								MySQL += ")";
								text3 = ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0) ? "spc_proc_oper_map" : ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) != 0) ? "wip_spc_oper_map" : "operation_map"));
								Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
							}
						}
						else
						{
							text = Strings.Trim(TxtName.Text);
							text3 = ((Operators.CompareString(Strings.Trim(TxtHeader.Text), "", TextCompare: false) != 0) ? Strings.Trim(TxtHeader.Text) : (text10 + General_Procedures.Replace_Special_Chars(text)));
							Get_SQL(1, ref MySQL, ref MyComm, text, "");
							if (Strings.InStr(Strings.LCase(MySQL), ":<ai-alias><;>") != 0)
							{
								MySQL = Strings.Replace(MySQL, ":<ai-alias><;>", ":" + Strings.Replace(Globals_Renamed.currDataAny, "->", "", 1, -1, CompareMethod.Text) + "<;>", 1, -1, CompareMethod.Text);
							}
							Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
						}
						break;
					}
					case 8615:
						{
							num = -1;
							switch (num2)
							{
							case 3:
								break;
							case 2:
							case 4:
							case 5:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								goto end_IL_0001_3;
							default:
								goto IL_21e9;
							}
							goto IL_0597;
						}
						IL_0de2:
						Get_SQL(1, ref MySQL, ref MyComm, text, text9);
						Add_To_SQL_Grid(num5, MySQL, text3, myDataType, MyComm, "");
						break;
						IL_0597:
						Interaction.MsgBox("Invalid bin entry: " + text + ". Entry should be of format IBnn@IBnn or DBnn@DBnn or FBnn@FBnn; e.g., IB1@IB32", MsgBoxStyle.Exclamation, "Invalid Bin Entry!");
						TxtName.Focus();
						goto end_IL_0001_3;
						end_IL_0001_2:
						break;
					}
					if (ll_Special_Update || Operators.CompareString(Globals_Renamed.currvaluetmp, "*spc-proc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*wip-spc-oper-map*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-map*", TextCompare: false) == 0)
					{
						Close();
					}
				}
				end_IL_0001_3:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 8615;
				continue;
			}
			break;
			IL_21e9:
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
					goto IL_000b;
				case 88:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
					if (!ll_Special_Update)
					{
						break;
					}
					goto IL_0017;
					IL_0017:
					num2 = 3;
					Globals_Renamed.currvaluetmp = "CANCEL";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 88;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void cmdvalues_Click(object eventSender, EventArgs eventArgs)
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
					errsource = "FrmBinsCounters - CmdValues_Click";
					if (Operators.CompareString(Globals_Renamed.gUsePyEngine, "N", TextCompare: false) == 0)
					{
						Interaction.MsgBox("Help Queries are now only supported for the Python Engine", MsgBoxStyle.Information, "Not Supported");
						goto end_IL_0001;
					}
					string text = "";
					string text2 = "";
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					string currvaluetmp = Globals_Renamed.currvaluetmp;
					if (Operators.CompareString(currvaluetmp, "*apc-attributes*", TextCompare: false) == 0 || Operators.CompareString(currvaluetmp, "*apc-primary-attributes*", TextCompare: false) == 0)
					{
						Globals_Renamed.currprompttmp = Strings.LCase(Globals_Renamed.currprompttmp);
						switch (Globals_Renamed.currprompttmp)
						{
						case "apcl":
							text2 = "LOT";
							break;
						case "apcm":
							text2 = "MODEL";
							break;
						case "apclk":
							text2 = "LOOKUP";
							break;
						case "apcg":
							text2 = "GENEALOGY";
							break;
						}
						text = ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*apc-primary-attributes*", TextCompare: false) != 0) ? ("SELECT /*+ ORDERED */ CASE WHEN UPPER(A.attr_chdb_mapped_col) LIKE 'GEN_ATTR%' THEN UPPER(A.attr_chdb_mapped_col) ELSE TO_CHAR(a.ATTR_ID) END || ',' || a.ATTR_Name AS Attribute_ID_Name_Pair, apco.Object_Name, a.ATTR_Description, a.ATTR_Type, apco.offline_status FROM P_APC_Object@OASYSNODE@ APCO, P_APC_Object_Attr@OASYSNODE@ A WHERE APCO.object_id = A.Object_ID AND APCO.Obj_Version = A.Obj_Version AND APCO.offline_status = 'C' AND APCO.object_name = &PROMPT&(DATA^apco.object_name^=^C^an APC Object^SELECT o.Object_name, o.obj_version, o.description FROM P_APC_Object@OASYSNODE@ O WHERE o.offline_status = 'C' AND o.object_type = '" + text2 + "' ORDER BY o.object_name^OARS_REG)&PROMPT& AND APCO.object_type = '" + text2 + "'") : ("SELECT /*+ ORDERED */ UPPER(A.attr_chdb_mapped_col) || ',' || a.ATTR_Name AS Attribute_ID_Name_Pair, apco.Object_Name, a.ATTR_Description, a.ATTR_Type, apco.offline_status FROM P_APC_Object@OASYSNODE@ APCO, P_APC_Object_Attr@OASYSNODE@ A WHERE APCO.object_id = A.Object_ID AND APCO.Obj_Version = A.Obj_Version AND APCO.offline_status = 'C' AND Upper(A.attr_chdb_mapped_col) LIKE 'GEN_ATTR%' AND APCO.object_name = &PROMPT&(DATA^apco.object_name^=^C^an APC Object^SELECT o.Object_name, o.obj_version, o.description FROM P_APC_Object@OASYSNODE@ O WHERE o.offline_status = 'C' AND o.object_type = '" + text2 + "' ORDER BY o.object_name^OARS_REG)&PROMPT& AND APCO.object_type = '" + text2 + "'"));
					}
					else
					{
						text = General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "help", "CANCEL", 2000, Globals_Renamed.MySchemaDir + ll_ini_File);
						if (Strings.Len(text) >= 2 && Operators.CompareString(Strings.Mid(text, 1, 1), "!", TextCompare: false) == 0)
						{
							text = Strings.Trim(General_Procedures.Get_Ini_Data("Column Help", Strings.Mid(text, 2).Trim(), "", 5000, Globals_Renamed.MySchemaDir + "\\columns.ini"));
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 15), "*select-a-field", TextCompare: false) == 0)
						{
							text = Strings.Replace(text, "@[]@", Globals_Renamed.currprompttmp, 1, -1, CompareMethod.Text);
						}
					}
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						text = "CANCEL";
					}
					BuildSQL.RunQuery(text, 2, 1, ll_ObjectType, "");
					goto end_IL_0001_2;
				}
				case 709:
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
				goto IL_02fb;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 709;
				continue;
			}
			break;
			IL_02fb:
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

	private void frmbinscounters_Load(object eventSender, EventArgs eventArgs)
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
						errsource = "frmBinsCounter - Form_Load";
						if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 3), "*x-", TextCompare: false) == 0)
						{
							ll_ini_File = "\\starcolumns2.ini";
						}
						string ll_DBType = "0";
						string text = "";
						short num3 = 0;
						short num4 = 0;
						short myMode = 0;
						FrmSQLQuerya obj = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx];
						string MyAlias = General_Procedures.Strip_Alias(1, Globals_Renamed.currDataAny);
						obj.Get_Database_Node_Plus(ref MyAlias, ref ll_DBType, ref ll_ObjectType);
						TxtHeader.Text = "";
						Globals_Renamed.currprompttmp = Strings.Trim(Globals_Renamed.currprompttmp);
						if (Operators.CompareString(Globals_Renamed.currprompttmp, "", TextCompare: false) == 0)
						{
							Globals_Renamed.currprompttmp = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "alias", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
						}
						text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "title", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							Text = text;
						}
						text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "label1", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							LblName.Text = text;
						}
						text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "labelh", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							TxtHeaderLabel.Text = text;
						}
						text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "labelchk", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							chkpercent.Text = text;
						}
						text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "d1", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
						if (Operators.CompareString(text, "", TextCompare: false) != 0 && Versioned.IsNumeric(text))
						{
							num3 = Conversions.ToShort(text);
							text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "d4", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Versioned.IsNumeric(text))
							{
								num4 = Conversions.ToShort(text);
							}
							text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "d6", "", 50, Globals_Renamed.MySchemaDir + ll_ini_File));
							if (Operators.CompareString(text, "", TextCompare: false) != 0 && Versioned.IsNumeric(text))
							{
								myMode = Conversions.ToShort(text);
							}
							if (num3 == 0 && (double)num4 == Conversions.ToDouble("800"))
							{
								num4 = Conversions.ToShort("53");
								SetFrmCtrls2(num3, num4, myMode);
							}
							else if (num3 == 0 && (double)num4 == Conversions.ToDouble("500"))
							{
								num4 = Conversions.ToShort("33");
								SetFrmCtrls2(num3, num4, myMode);
							}
							else if (num3 == 1400 && (double)num4 == Conversions.ToDouble("1400"))
							{
								num3 = 93;
								num4 = Conversions.ToShort("93");
								SetFrmCtrls2(num3, num4, myMode);
							}
							else if (num3 == 1600 && (double)num4 == Conversions.ToDouble("1800"))
							{
								num3 = 98;
								num4 = Conversions.ToShort("120");
								SetFrmCtrls2(num3, num4, myMode);
							}
							else
							{
								SetFrmCtrls2(num3, num4, myMode);
							}
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*other-entity-wafer*", TextCompare: false) == 0)
						{
							TxtName.Items.Add("@SPC_PROC_OPER_MAP@");
						}
						else if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 27), "*start-imbigdata-operation-", TextCompare: false) == 0)
						{
							TxtHeader.Text = "";
							TxtHeader.Enabled = false;
						}
						else if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-class-oper*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*x-test-sort-oper*", TextCompare: false) == 0)
						{
							Text = "Create " + Strings.Mid(Globals_Renamed.currhelptmp, 2);
						}
						else if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 22), "*entity-ctr-attributes", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field*", TextCompare: false) == 0)
						{
							TxtOther.Visible = true;
							TxtOther.Text = Conversions.ToString(1);
							TxtOther.Top = TxtHeader.Top;
							TxtHeader.Width = TxtHeader.Width - TxtOther.Width - 15;
							if (Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 22), "*entity-ctr-attributes", TextCompare: false) == 0)
							{
								ToolTip1.SetToolTip(TxtOther, "Days to Go Back. Default is 1");
							}
							else
							{
								TxtOther.Items.Clear();
								TxtOther.Items.Add("0");
								TxtOther.Items.Add("1");
								TxtOther.Items.Add("null");
								TxtOther.Text = "null";
								ToolTip1.SetToolTip(TxtOther, "Value to assign if field is null");
							}
						}
						else if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*signal-field-ibd*", TextCompare: false) == 0)
						{
							LblName.Top -= 30;
							TxtName1.Top = TxtName.Top - 30;
							TxtHeaderLabel.Top = TxtName1.Top + TxtName1.Height + 10;
							TxtHeader.Top = TxtHeaderLabel.Top + TxtHeaderLabel.Height + 5;
							TxtHeader.Visible = true;
							TxtHeaderLabel.Visible = true;
						}
						Form Form_Name = this;
						BuildForm.Set_Form_Position(ref Form_Name);
						chkpercent.CheckState = CheckState.Unchecked;
						goto end_IL_0001;
					}
					case 1884:
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
					goto IL_0792;
				}
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 1884;
				continue;
			}
			break;
			IL_0792:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmbinscounters_Paint(object eventSender, PaintEventArgs eventArgs)
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
					errsource = "frmBinsCounter - Form_Paint";
					string text = "";
					if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*bins-ctrs-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*bins-ctrs-wafers-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*btr-bins-ctrs-rollups*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*rollup-name*", TextCompare: false) == 0))
					{
						Text = "Sort/Class Bin and Counter Rollups";
						TxtHeader.Visible = false;
						TxtHeaderLabel.Visible = false;
						ForeColor = ColorTranslator.FromOle(-2147483640);
						cmdLoadGrid.Enabled = false;
					}
					else if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-class-names*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-finebin-names*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-class-names-1266*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-defect-ncdd*", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "*yas-defect-ncdd-1266*", TextCompare: false) == 0))
					{
						chkpercent.Text = "Adder Defects Only";
						chkpercent.Visible = true;
					}
					text = Strings.Trim(General_Procedures.Get_Ini_Data(Globals_Renamed.currvaluetmp, "paint", "", 2000, Globals_Renamed.MySchemaDir + ll_ini_File));
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						text = Strings.Replace(text, "<cr>", "\r\n", 1, -1, CompareMethod.Text);
						text = Strings.Replace(text, "<t>", "\t", 1, -1, CompareMethod.Text);
					}
					TxtLabel.Text = text;
					goto end_IL_0001;
				}
				case 483:
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
				try0001_dispatch = 483;
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

	private void frmbinscounters_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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
					BuildForm.Invoke_CSVViewer3(0, "HELP2");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Dispose();
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

	private void TxtHeader_KeyPress(object eventSender, KeyPressEventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short num5 = default(short);
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
					num5 = checked((short)Strings.Asc(eventArgs.KeyChar));
					goto IL_0011;
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
							goto IL_0011;
						case 3:
							goto IL_001a;
						case 4:
							goto IL_00ac;
						case 6:
							goto IL_00fe;
						case 5:
						case 7:
						case 8:
						case 9:
							goto IL_0105;
						case 10:
							goto IL_0115;
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
					IL_0115:
					num2 = 10;
					if (num5 != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_00ac:
					num2 = 4;
					if (!(num5 == 8 || num5 == 127 || num5 == 95 || (num5 >= 48 && num5 <= 57) || (num5 >= 97 && num5 <= 122) || (num5 >= 65 && num5 <= 90)))
					{
						goto IL_00fe;
					}
					goto IL_0105;
					IL_0105:
					num2 = 9;
					eventArgs.KeyChar = Strings.Chr(num5);
					goto IL_0115;
					IL_00fe:
					num2 = 6;
					num5 = 0;
					goto IL_0105;
					IL_0011:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "*entity-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*entity-current-attributes2*", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 22), "*entity-ctr-attributes", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*lot-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*operation-attributes*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*lot-attribute2*", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.currvaluetmp, "*ep-value*", TextCompare: false) == 0)
					{
						goto IL_00ac;
					}
					goto IL_0105;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				eventArgs.Handled = true;
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

	private void cmdPaste_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string[] array = default(string[]);
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
				case 343:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_0038;
						case 6:
							goto IL_003d;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_0056;
						case 9:
							goto IL_0067;
						case 10:
							goto IL_0076;
						case 11:
							goto IL_008f;
						case 12:
						case 13:
							goto IL_00a2;
						case 14:
							goto IL_00b1;
						case 15:
							goto IL_00b7;
						case 16:
							goto IL_00cf;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0076:
					num2 = 10;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_008f;
					}
					goto IL_00a2;
					IL_008f:
					num2 = 11;
					text2 = text2 + "\r\n" + text;
					goto IL_00a2;
					IL_0067:
					num2 = 9;
					text = Strings.Trim(array[num5]);
					goto IL_0076;
					IL_00a2:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_00ab;
					IL_000b:
					num2 = 2;
					if (!TxtName1.Visible)
					{
						goto end_IL_0001_3;
					}
					goto IL_0026;
					IL_0026:
					num2 = 4;
					text2 = MyProject.Computer.Clipboard.GetText();
					goto IL_0038;
					IL_0038:
					num2 = 5;
					num5 = 0;
					goto IL_003d;
					IL_003d:
					num2 = 6;
					array = Strings.Split(text2, "\r\n");
					goto IL_004e;
					IL_004e:
					num2 = 7;
					text2 = "";
					goto IL_0056;
					IL_0056:
					num2 = 8;
					num6 = Information.UBound(array);
					num5 = 0;
					goto IL_00ab;
					IL_00ab:
					if (num5 <= num6)
					{
						goto IL_0067;
					}
					goto IL_00b1;
					IL_00b1:
					num2 = 14;
					array = null;
					goto IL_00b7;
					IL_00b7:
					num2 = 15;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_00cf;
					IL_00cf:
					num2 = 16;
					text2 = Strings.Mid(text2, 3);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				TxtName1.Text += text2;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 343;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class FrmCalendar : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK")]
	private Button _cmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCancel")]
	private Button _cmdCancel;

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

	[field: AccessedThroughProperty("cmbHr")]
	internal virtual ComboBox cmbHr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbMin")]
	internal virtual ComboBox CmbMin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbSec")]
	internal virtual ComboBox cmbSec
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHr")]
	internal virtual Label LblHr
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblMin")]
	internal virtual Label LblMin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblSec")]
	internal virtual Label lblSec
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MonthCal")]
	internal virtual MonthCalendar MonthCal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CmbOffset")]
	internal virtual ComboBox CmbOffset
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblOffset")]
	internal virtual Label LblOffset
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

	public FrmCalendar()
	{
		base.FormClosed += FrmCalendar2_FormClosed;
		base.Load += FrmCalendar_Load;
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
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.cmbHr = new System.Windows.Forms.ComboBox();
		this.CmbMin = new System.Windows.Forms.ComboBox();
		this.cmbSec = new System.Windows.Forms.ComboBox();
		this.LblHr = new System.Windows.Forms.Label();
		this.LblMin = new System.Windows.Forms.Label();
		this.lblSec = new System.Windows.Forms.Label();
		this.MonthCal = new System.Windows.Forms.MonthCalendar();
		this.CmbOffset = new System.Windows.Forms.ComboBox();
		this.LblOffset = new System.Windows.Forms.Label();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.cmdOK.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmdOK.Location = new System.Drawing.Point(36, 354);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(79, 49);
		this.cmdOK.TabIndex = 5;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Font = new System.Drawing.Font("Arial", 8.25f);
		this.cmdCancel.Location = new System.Drawing.Point(228, 354);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(79, 49);
		this.cmdCancel.TabIndex = 6;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.cmbHr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbHr.FormattingEnabled = true;
		this.cmbHr.Items.AddRange(new object[24]
		{
			"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23"
		});
		this.cmbHr.Location = new System.Drawing.Point(36, 257);
		this.cmbHr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmbHr.Name = "cmbHr";
		this.cmbHr.Size = new System.Drawing.Size(68, 24);
		this.cmbHr.TabIndex = 1;
		this.CmbMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbMin.FormattingEnabled = true;
		this.CmbMin.Items.AddRange(new object[60]
		{
			"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
			"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
			"40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
			"50", "51", "52", "53", "54", "55", "56", "57", "58", "59"
		});
		this.CmbMin.Location = new System.Drawing.Point(140, 257);
		this.CmbMin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmbMin.Name = "CmbMin";
		this.CmbMin.Size = new System.Drawing.Size(68, 24);
		this.CmbMin.TabIndex = 2;
		this.cmbSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSec.FormattingEnabled = true;
		this.cmbSec.Items.AddRange(new object[60]
		{
			"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
			"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
			"40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
			"50", "51", "52", "53", "54", "55", "56", "57", "58", "59"
		});
		this.cmbSec.Location = new System.Drawing.Point(237, 257);
		this.cmbSec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmbSec.Name = "cmbSec";
		this.cmbSec.Size = new System.Drawing.Size(68, 24);
		this.cmbSec.TabIndex = 3;
		this.LblHr.AutoSize = true;
		this.LblHr.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LblHr.Location = new System.Drawing.Point(44, 235);
		this.LblHr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHr.Name = "LblHr";
		this.LblHr.Size = new System.Drawing.Size(38, 16);
		this.LblHr.TabIndex = 6;
		this.LblHr.Text = "Hour";
		this.LblMin.AutoSize = true;
		this.LblMin.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LblMin.Location = new System.Drawing.Point(140, 235);
		this.LblMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblMin.Name = "LblMin";
		this.LblMin.Size = new System.Drawing.Size(57, 16);
		this.LblMin.TabIndex = 7;
		this.LblMin.Text = "Minutes";
		this.lblSec.AutoSize = true;
		this.lblSec.Font = new System.Drawing.Font("Arial", 8.25f);
		this.lblSec.Location = new System.Drawing.Point(235, 235);
		this.lblSec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblSec.Name = "lblSec";
		this.lblSec.Size = new System.Drawing.Size(63, 16);
		this.lblSec.TabIndex = 8;
		this.lblSec.Text = "Seconds";
		this.MonthCal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.MonthCal.Location = new System.Drawing.Point(51, 22);
		this.MonthCal.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
		this.MonthCal.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.MonthCal.Name = "MonthCal";
		this.MonthCal.TabIndex = 0;
		this.CmbOffset.FormattingEnabled = true;
		this.CmbOffset.Items.AddRange(new object[7] { "", "CR==-06:00", "AZ==-07:00", "GMT==-00:00", "VN==+07:00", "CD==+08:00", "KM==+08:00" });
		this.CmbOffset.Location = new System.Drawing.Point(140, 302);
		this.CmbOffset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmbOffset.Name = "CmbOffset";
		this.CmbOffset.Size = new System.Drawing.Size(165, 24);
		this.CmbOffset.TabIndex = 4;
		this.ToolTip1.SetToolTip(this.CmbOffset, "Enter GMT Offset for Teradata Databases");
		this.CmbOffset.Visible = false;
		this.LblOffset.AutoSize = true;
		this.LblOffset.Location = new System.Drawing.Point(36, 305);
		this.LblOffset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOffset.Name = "LblOffset";
		this.LblOffset.Size = new System.Drawing.Size(50, 17);
		this.LblOffset.TabIndex = 11;
		this.LblOffset.Text = "Offset:";
		this.LblOffset.Visible = false;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(341, 434);
		base.Controls.Add(this.LblOffset);
		base.Controls.Add(this.CmbOffset);
		base.Controls.Add(this.MonthCal);
		base.Controls.Add(this.lblSec);
		base.Controls.Add(this.LblMin);
		base.Controls.Add(this.LblHr);
		base.Controls.Add(this.cmbSec);
		base.Controls.Add(this.CmbMin);
		base.Controls.Add(this.cmbHr);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmCalendar";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Calendar";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void FrmCalendar2_FormClosed(object sender, FormClosedEventArgs e)
	{
		Dispose();
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

	private void cmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 937:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 1:
							break;
						default:
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
							goto IL_004b;
						case 6:
							goto IL_00c5;
						case 8:
							goto IL_00d7;
						case 9:
							goto IL_00f5;
						case 10:
							goto IL_0170;
						case 11:
							goto IL_0184;
						case 12:
							goto IL_0196;
						case 13:
							goto IL_01b5;
						case 14:
						case 15:
							goto IL_01ca;
						case 16:
							goto IL_01f2;
						case 18:
						case 19:
							goto IL_020e;
						case 21:
							goto IL_021d;
						case 22:
							goto IL_0239;
						case 24:
							goto IL_0291;
						case 25:
							goto IL_030c;
						case 7:
						case 20:
						case 23:
						case 26:
						case 27:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 17:
						case 28:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0239:
					num2 = 22;
					Globals_Renamed.MyCalValue = MonthCal.SelectionEnd.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) + cmbHr.Text + CmbMin.Text + cmbSec.Text;
					break;
					IL_0291:
					num2 = 24;
					Globals_Renamed.MyCalValue = MonthCal.SelectionEnd.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + " " + cmbHr.Text + ":" + CmbMin.Text + ":" + cmbSec.Text;
					goto IL_030c;
					IL_021d:
					num2 = 21;
					if (Operators.CompareString(Globals_Renamed.MyCalDateType, "S", TextCompare: false) == 0)
					{
						goto IL_0239;
					}
					goto IL_0291;
					IL_030c:
					num2 = 25;
					Globals_Renamed.gCalOffset = "";
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (Operators.CompareString(Globals_Renamed.MyCalDateType, "D", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.MyCalDateType, "V", TextCompare: false) == 0)
					{
						goto IL_004b;
					}
					goto IL_00d7;
					IL_004b:
					num2 = 5;
					Globals_Renamed.MyCalValue = MonthCal.SelectionEnd.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")) + " " + cmbHr.Text + ":" + CmbMin.Text + ":" + cmbSec.Text;
					goto IL_00c5;
					IL_00d7:
					num2 = 8;
					if (Operators.CompareString(Globals_Renamed.MyCalDateType, "G", TextCompare: false) == 0)
					{
						goto IL_00f5;
					}
					goto IL_021d;
					IL_00f5:
					num2 = 9;
					Globals_Renamed.MyCalValue = MonthCal.SelectionEnd.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + " " + cmbHr.Text + ":" + CmbMin.Text + ":" + cmbSec.Text;
					goto IL_0170;
					IL_0170:
					num2 = 10;
					text = Strings.Trim(CmbOffset.Text);
					goto IL_0184;
					IL_0184:
					num2 = 11;
					num5 = Strings.InStrRev(text, "==");
					goto IL_0196;
					IL_0196:
					num2 = 12;
					if (num5 != 0 && Strings.Len(text) >= checked(num5 + 2))
					{
						goto IL_01b5;
					}
					goto IL_01ca;
					IL_00c5:
					num2 = 6;
					Globals_Renamed.gCalOffset = "";
					break;
					IL_01b5:
					num2 = 13;
					text = Strings.Trim(Strings.Mid(text, checked(num5 + 2)));
					goto IL_01ca;
					IL_01ca:
					num2 = 15;
					if (!((Operators.CompareString(text, "", TextCompare: false) == 0) | LikeOperator.LikeString(text, "[+-]##:##", CompareMethod.Binary)))
					{
						goto IL_01f2;
					}
					goto IL_020e;
					IL_01f2:
					num2 = 16;
					Interaction.MsgBox("An Invalid GMT Offset was specified. Offset should be of format +/- hh:mm. E.g., -06:00 or +01:00", MsgBoxStyle.Exclamation, "Invalid GMT Offset");
					goto end_IL_0001_3;
					IL_020e:
					num2 = 19;
					Globals_Renamed.gCalOffset = text;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 27;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 937;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001_3:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmCalendar_Load(object sender, EventArgs e)
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
					ProjectData.ClearProjectError();
					num2 = 2;
					string text = "FrmCalendar - Form_Load";
					DateTime now = DateAndTime.Now;
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					string text2 = "";
					text2 = Globals_Renamed.MyCalValue;
					if (Operators.CompareString(Globals_Renamed.MyCalDateType, "G", TextCompare: false) == 0)
					{
						CmbOffset.Visible = true;
						LblOffset.Visible = true;
						if (Operators.CompareString(Globals_Renamed.gCalOffset, "", TextCompare: false) != 0)
						{
							CmbOffset.Text = Globals_Renamed.gCalOffset;
						}
						else
						{
							CmbOffset.Text = "";
						}
					}
					else
					{
						Globals_Renamed.gCalOffset = "";
					}
					MonthCal.Left = checked((int)Math.Round((double)(base.Width - MonthCal.Width) / 2.0 - 2.0));
					if (Operators.CompareString(Globals_Renamed.MyCalDateType, "S", TextCompare: false) == 0 && Operators.CompareString(text2, "", TextCompare: false) != 0 && Strings.Len(text2) == 14)
					{
						text2 = Strings.Mid(text2, 1, 4) + "-" + Strings.Mid(text2, 5, 2) + "-" + Strings.Mid(text2, 7, 2) + " " + Strings.Mid(text2, 9, 2) + ":" + Strings.Mid(text2, 11, 2) + ":" + Strings.Mid(text2, 13, 2);
					}
					if (Information.IsDate(text2))
					{
						now = Conversions.ToDate(text2);
						num3 = Conversions.ToInteger(now.ToString("HH", CultureInfo.CreateSpecificCulture("en-US")));
						num4 = Conversions.ToInteger(now.ToString("mm", CultureInfo.CreateSpecificCulture("en-US")));
						num5 = Conversions.ToInteger(now.ToString("ss", CultureInfo.CreateSpecificCulture("en-US")));
					}
					else
					{
						now = DateAndTime.Now;
						num3 = 0;
						num4 = 0;
						num5 = 0;
					}
					MonthCal.SetDate(now);
					cmbHr.SelectedIndex = num3;
					CmbMin.SelectedIndex = num4;
					cmbSec.SelectedIndex = num5;
					goto end_IL_0001;
				}
				case 664:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox(Information.Err().Description);
						Information.Err().Clear();
						CmbMin.SelectedIndex = 0;
						cmbSec.SelectedIndex = 0;
						goto end_IL_0001;
					}
					break;
				}
				goto IL_02ce;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 664;
				continue;
			}
			break;
			IL_02ce:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}
}

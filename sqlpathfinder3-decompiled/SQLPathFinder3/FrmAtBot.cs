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
public class FrmAtBot : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdClose")]
	private Button _CmdClose;

	public string f_SortCol;

	[field: AccessedThroughProperty("CmbAt")]
	internal virtual ComboBox CmbAt
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblHeader")]
	internal virtual Label LblHeader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	internal virtual Button CmdClose
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
			EventHandler value2 = CmdClose_Click;
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

	public FrmAtBot()
	{
		f_SortCol = "";
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
		this.CmbAt = new System.Windows.Forms.ComboBox();
		this.LblHeader = new System.Windows.Forms.Label();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdClose = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.CmbAt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmbAt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CmbAt.FormattingEnabled = true;
		this.CmbAt.Location = new System.Drawing.Point(16, 116);
		this.CmbAt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmbAt.Name = "CmbAt";
		this.CmbAt.Size = new System.Drawing.Size(320, 24);
		this.CmbAt.TabIndex = 0;
		this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblHeader.Location = new System.Drawing.Point(23, 16);
		this.LblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblHeader.Name = "LblHeader";
		this.LblHeader.Size = new System.Drawing.Size(284, 75);
		this.LblHeader.TabIndex = 3;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.Location = new System.Drawing.Point(333, 4);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(59, 39);
		this.CmdOK.TabIndex = 1;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdClose.Location = new System.Drawing.Point(333, 46);
		this.CmdClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.CmdClose.Name = "CmdClose";
		this.CmdClose.Size = new System.Drawing.Size(59, 39);
		this.CmdClose.TabIndex = 2;
		this.CmdClose.Text = "Close";
		this.CmdClose.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(408, 171);
		base.Controls.Add(this.CmdClose);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.LblHeader);
		base.Controls.Add(this.CmbAt);
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmAtBot";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "At Bottom Of";
		base.ResumeLayout(false);
	}

	private void CmdClose_Click(object sender, EventArgs e)
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
					f_SortCol = "CANCEL";
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

	private void CmdOK_Click(object sender, EventArgs e)
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
				case 224:
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
							goto IL_0030;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_004a;
						case 8:
							goto IL_0061;
						case 9:
							goto IL_0069;
						case 5:
						case 10:
						case 11:
							goto IL_0083;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0061:
					num2 = 8;
					text = "Report";
					goto IL_0069;
					IL_0069:
					num2 = 9;
					f_SortCol = " {" + text + "}";
					goto IL_0083;
					IL_004a:
					num2 = 7;
					if (Operators.CompareString(text, "report", TextCompare: false) == 0)
					{
						goto IL_0061;
					}
					goto IL_0069;
					IL_0083:
					num2 = 11;
					text = Strings.LCase(text);
					break;
					IL_000b:
					num2 = 2;
					text = CmbAt.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0030;
					}
					goto IL_0041;
					IL_0030:
					num2 = 4;
					f_SortCol = "";
					goto IL_0083;
					IL_0041:
					num2 = 6;
					text = Strings.LCase(text);
					goto IL_004a;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 224;
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

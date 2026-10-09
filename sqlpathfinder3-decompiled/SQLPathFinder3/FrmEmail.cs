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
public class FrmEmail : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	public bool f_OK;

	public string f_EmailMode;

	[field: AccessedThroughProperty("CmbTo")]
	internal virtual ComboBox CmbTo
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtSubj")]
	internal virtual TextBox TxtSubj
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblSubj")]
	internal virtual Label LblSubj
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtBody")]
	internal virtual TextBox TxtBody
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmEmail()
	{
		base.Load += FrmEmail_Load;
		f_OK = true;
		f_EmailMode = "";
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
		this.CmbTo = new System.Windows.Forms.ComboBox();
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.TxtSubj = new System.Windows.Forms.TextBox();
		this.LblSubj = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.TxtBody = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.CmbTo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmbTo.FormattingEnabled = true;
		this.CmbTo.Items.AddRange(new object[6] { "sqlpathfinder_support@intel.com", "giovanni.carmiol@intel.com", "vishwas.nataraj@intel.com", "yaniv.naim@intel.com", "runner.yang@intel.com", "jolyon.m.clarke@intel.com" });
		this.CmbTo.Location = new System.Drawing.Point(12, 84);
		this.CmbTo.Margin = new System.Windows.Forms.Padding(4);
		this.CmbTo.Name = "CmbTo";
		this.CmbTo.Size = new System.Drawing.Size(651, 24);
		this.CmbTo.TabIndex = 0;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.Location = new System.Drawing.Point(592, 10);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(72, 44);
		this.CmdOK.TabIndex = 3;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Location = new System.Drawing.Point(512, 10);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(72, 44);
		this.CmdCancel.TabIndex = 4;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(12, 64);
		this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(259, 17);
		this.Label1.TabIndex = 3;
		this.Label1.Text = "Email addresses separated by commas:";
		this.TxtSubj.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSubj.Location = new System.Drawing.Point(12, 155);
		this.TxtSubj.Margin = new System.Windows.Forms.Padding(4);
		this.TxtSubj.Name = "TxtSubj";
		this.TxtSubj.Size = new System.Drawing.Size(651, 22);
		this.TxtSubj.TabIndex = 1;
		this.TxtSubj.Text = "SQLPathFinder Query";
		this.LblSubj.AutoSize = true;
		this.LblSubj.Location = new System.Drawing.Point(12, 135);
		this.LblSubj.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblSubj.Name = "LblSubj";
		this.LblSubj.Size = new System.Drawing.Size(59, 17);
		this.LblSubj.TabIndex = 5;
		this.LblSubj.Text = "Subject:";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(12, 207);
		this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(69, 17);
		this.Label2.TabIndex = 6;
		this.Label2.Text = "Message:";
		this.TxtBody.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBody.Location = new System.Drawing.Point(12, 226);
		this.TxtBody.Margin = new System.Windows.Forms.Padding(4);
		this.TxtBody.Multiline = true;
		this.TxtBody.Name = "TxtBody";
		this.TxtBody.Size = new System.Drawing.Size(651, 250);
		this.TxtBody.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(668, 480);
		base.Controls.Add(this.TxtBody);
		base.Controls.Add(this.Label2);
		base.Controls.Add(this.LblSubj);
		base.Controls.Add(this.TxtSubj);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.CmbTo);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmEmail";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Email Active Query";
		base.ResumeLayout(false);
		base.PerformLayout();
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
					goto IL_000b;
				case 63:
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
					f_OK = false;
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
				try0001_dispatch = 63;
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
							goto IL_000b;
						case 3:
							goto IL_0029;
						case 4:
							goto IL_0068;
						case 5:
							goto IL_0086;
						case 6:
							goto IL_00a4;
						case 7:
							goto IL_00bf;
						case 8:
							goto IL_00cc;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0086:
					num2 = 5;
					TxtSubj.Text = Strings.Trim(TxtSubj.Text);
					goto IL_00a4;
					IL_00a4:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.gEmailOutlook, "Y", TextCompare: false) == 0)
					{
						goto IL_00bf;
					}
					goto IL_00cc;
					IL_0068:
					num2 = 4;
					TxtBody.Text = Strings.Trim(TxtBody.Text);
					goto IL_0086;
					IL_00bf:
					num2 = 7;
					f_EmailMode = "O";
					goto IL_00cc;
					IL_000b:
					num2 = 2;
					CmbTo.Text = Strings.Trim(CmbTo.Text);
					goto IL_0029;
					IL_0029:
					num2 = 3;
					if (Operators.CompareString(CmbTo.Text, "", TextCompare: false) == 0 || Strings.InStr(CmbTo.Text.ToLower(), "@intel.com") == 0)
					{
						break;
					}
					goto IL_0068;
					IL_00cc:
					num2 = 8;
					Close();
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				Interaction.MsgBox("Specify a valid distribution list", MsgBoxStyle.Exclamation, "Invalid Data");
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

	private void FrmEmail_Load(object sender, EventArgs e)
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
				TxtBody.Text = "Please see attached query ...\r\n\r\n" + BuildForm.Get_Version_Config();
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
}

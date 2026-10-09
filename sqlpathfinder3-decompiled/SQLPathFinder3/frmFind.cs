using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[DesignerGenerated]
internal class frmFind : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("chkCase")]
	private CheckBox _chkCase;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("Text1")]
	private TextBox _Text1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdcancel")]
	private Button _cmdcancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdFind")]
	private Button _cmdFind;

	public string ll_FindStr;

	public short ll_FindCase;

	public virtual CheckBox chkCase
	{
		[CompilerGenerated]
		get
		{
			return _chkCase;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkCase_CheckStateChanged;
			CheckBox checkBox = _chkCase;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged -= value2;
			}
			_chkCase = value;
			checkBox = _chkCase;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged += value2;
			}
		}
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

	public virtual Button cmdcancel
	{
		[CompilerGenerated]
		get
		{
			return _cmdcancel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdCancel_Click;
			Button button = _cmdcancel;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdcancel = value;
			button = _cmdcancel;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public virtual Button cmdFind
	{
		[CompilerGenerated]
		get
		{
			return _cmdFind;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdFind_Click;
			Button button = _cmdFind;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdFind = value;
			button = _cmdFind;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label1")]
	public virtual Label Label1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmFind()
	{
		base.Activated += frmFind_Activated;
		base.Load += frmFind_Load;
		ll_FindStr = "";
		ll_FindCase = 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.frmFind));
		this.chkCase = new System.Windows.Forms.CheckBox();
		this.Text1 = new System.Windows.Forms.TextBox();
		this.cmdcancel = new System.Windows.Forms.Button();
		this.cmdFind = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.chkCase.AutoSize = true;
		this.chkCase.BackColor = System.Drawing.SystemColors.Control;
		this.chkCase.Cursor = System.Windows.Forms.Cursors.Default;
		this.chkCase.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkCase.ForeColor = System.Drawing.SystemColors.ControlText;
		this.chkCase.Location = new System.Drawing.Point(8, 55);
		this.chkCase.Name = "chkCase";
		this.chkCase.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.chkCase.Size = new System.Drawing.Size(173, 20);
		this.chkCase.TabIndex = 1;
		this.chkCase.Text = "&Case Sensitive Search";
		this.chkCase.UseVisualStyleBackColor = false;
		this.Text1.AcceptsReturn = true;
		this.Text1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Text1.BackColor = System.Drawing.SystemColors.Window;
		this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.Text1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Text1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Text1.Location = new System.Drawing.Point(87, 16);
		this.Text1.MaxLength = 0;
		this.Text1.Name = "Text1";
		this.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Text1.Size = new System.Drawing.Size(154, 23);
		this.Text1.TabIndex = 0;
		this.cmdcancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdcancel.BackColor = System.Drawing.SystemColors.Control;
		this.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdcancel.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdcancel.Location = new System.Drawing.Point(248, 40);
		this.cmdcancel.Name = "cmdcancel";
		this.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdcancel.Size = new System.Drawing.Size(73, 25);
		this.cmdcancel.TabIndex = 3;
		this.cmdcancel.Text = "Cancel";
		this.cmdcancel.UseVisualStyleBackColor = false;
		this.cmdFind.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdFind.BackColor = System.Drawing.SystemColors.Control;
		this.cmdFind.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdFind.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdFind.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdFind.Location = new System.Drawing.Point(248, 8);
		this.cmdFind.Name = "cmdFind";
		this.cmdFind.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdFind.Size = new System.Drawing.Size(73, 25);
		this.cmdFind.TabIndex = 2;
		this.cmdFind.Text = "&Find";
		this.cmdFind.UseVisualStyleBackColor = false;
		this.Label1.AutoSize = true;
		this.Label1.BackColor = System.Drawing.Color.Transparent;
		this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label1.Location = new System.Drawing.Point(4, 18);
		this.Label1.Name = "Label1";
		this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label1.Size = new System.Drawing.Size(77, 16);
		this.Label1.TabIndex = 6;
		this.Label1.Text = "Fi&nd What:";
		base.AcceptButton = this.cmdFind;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.cmdcancel;
		base.ClientSize = new System.Drawing.Size(330, 93);
		base.Controls.Add(this.chkCase);
		base.Controls.Add(this.Text1);
		base.Controls.Add(this.cmdcancel);
		base.Controls.Add(this.cmdFind);
		base.Controls.Add(this.Label1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.WindowText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(177, 239);
		base.Name = "frmFind";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Find";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void chkCase_CheckStateChanged(object eventSender, EventArgs eventArgs)
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
				ll_FindCase = checked((short)chkCase.CheckState);
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

	private void CmdCancel_Click(object eventSender, EventArgs eventArgs)
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
					ll_FindStr = "";
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

	private void cmdFind_Click(object eventSender, EventArgs eventArgs)
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
				case 73:
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
					ll_FindStr = Text1.Text;
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
				try0001_dispatch = 73;
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

	private void frmFind_Activated(object sender, EventArgs e)
	{
		if (Operators.CompareString(Text1.Text, "", TextCompare: false) != 0)
		{
			cmdFind.Enabled = true;
		}
	}

	private void frmFind_Load(object eventSender, EventArgs eventArgs)
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
				case 188:
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
					cmdFind.Enabled = false;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					base.Top = checked((int)Math.Round((double)Screen.PrimaryScreen.Bounds.Height / 2.0 - (double)base.Height / 2.0));
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				base.Left = checked((int)Math.Round((double)Screen.PrimaryScreen.Bounds.Width / 2.0 - (double)base.Width / 2.0));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 188;
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
				bool flag;
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
							goto IL_000f;
						case 4:
							goto IL_0030;
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
					flag = true;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(Text1.Text, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0030;
					IL_0030:
					num2 = 4;
					cmdFind.Enabled = false;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				cmdFind.Enabled = true;
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
}

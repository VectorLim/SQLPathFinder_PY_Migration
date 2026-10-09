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
public class FrmJS : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("TextBox1")]
	private TextBox _TextBox1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdNew")]
	private ToolStripButton _cmdNew;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClose")]
	private ToolStripButton _cmdClose;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private ToolStripButton _cmdClear;

	private bool fSaveExpr;

	private bool ListenExpr;

	public string fData;

	public string fType;

	internal virtual TextBox TextBox1
	{
		[CompilerGenerated]
		get
		{
			return _TextBox1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TextBox1_TextChanged;
			TextBox textBox = _TextBox1;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			_TextBox1 = value;
			textBox = _TextBox1;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStrip1")]
	internal virtual ToolStrip ToolStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdNew
	{
		[CompilerGenerated]
		get
		{
			return _cmdNew;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdNew_Click;
			ToolStripButton toolStripButton = _cmdNew;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdNew = value;
			toolStripButton = _cmdNew;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
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

	internal virtual ToolStripButton cmdClose
	{
		[CompilerGenerated]
		get
		{
			return _cmdClose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
			ToolStripButton toolStripButton = _cmdClose;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdClose = value;
			toolStripButton = _cmdClose;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton cmdClear
	{
		[CompilerGenerated]
		get
		{
			return _cmdClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClear_Click;
			ToolStripButton toolStripButton = _cmdClear;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdClear = value;
			toolStripButton = _cmdClear;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmJS()
	{
		base.FormClosing += Form3_FormClosing;
		base.Load += Form3_Load;
		fSaveExpr = false;
		ListenExpr = false;
		fData = "";
		fType = "TOOLTIP";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmJS));
		this.TextBox1 = new System.Windows.Forms.TextBox();
		this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
		this.cmdNew = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdClear = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdClose = new System.Windows.Forms.ToolStripButton();
		this.ToolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TextBox1.Location = new System.Drawing.Point(5, 30);
		this.TextBox1.Multiline = true;
		this.TextBox1.Name = "TextBox1";
		this.TextBox1.Size = new System.Drawing.Size(775, 351);
		this.TextBox1.TabIndex = 0;
		this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.cmdNew, this.ToolStripSeparator1, this.ToolStripSeparator2, this.cmdClear, this.ToolStripSeparator3, this.cmdClose });
		this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
		this.ToolStrip1.Name = "ToolStrip1";
		this.ToolStrip1.Size = new System.Drawing.Size(782, 27);
		this.ToolStrip1.TabIndex = 1;
		this.ToolStrip1.Text = "ToolStrip1";
		this.cmdNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdNew.Name = "cmdNew";
		this.cmdNew.Size = new System.Drawing.Size(148, 24);
		this.cmdNew.Text = "New Custom Tooltip";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 27);
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 27);
		this.cmdClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdClear.Image = (System.Drawing.Image)resources.GetObject("cmdClear.Image");
		this.cmdClear.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(47, 24);
		this.cmdClear.Text = "Clear";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 27);
		this.cmdClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdClose.Image = (System.Drawing.Image)resources.GetObject("cmdClose.Image");
		this.cmdClose.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new System.Drawing.Size(94, 24);
		this.cmdClose.Text = "Close / Save";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(782, 383);
		base.Controls.Add(this.ToolStrip1);
		base.Controls.Add(this.TextBox1);
		base.Name = "FrmJS";
		this.Text = "Custom ToolTip";
		this.ToolStrip1.ResumeLayout(false);
		this.ToolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void cmdClose_Click(object sender, EventArgs e)
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

	private void TextBox1_TextChanged(object sender, EventArgs e)
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
					if (!ListenExpr)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				fSaveExpr = true;
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

	private void Form3_FormClosing(object sender, FormClosingEventArgs e)
	{
		int num = 0;
		if (fSaveExpr)
		{
			switch ((int)Interaction.MsgBox("Do you wish to save your changes?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Save Changes?"))
			{
			case 2:
				e.Cancel = true;
				break;
			case 6:
				fData = Strings.Trim(TextBox1.Text);
				break;
			}
		}
		fData = General_Procedures.Quote_CRLF_Replace("E2", fData);
	}

	private void Form3_Load(object sender, EventArgs e)
	{
		fData = General_Procedures.Quote_CRLF_Replace("D2", fData);
		TextBox1.Text = fData;
		if (Operators.CompareString(fType, "RANGE", TextCompare: false) == 0)
		{
			Text = "Range Selector";
			cmdNew.Text = "New Range Selector";
		}
		else if (Operators.CompareString(fType, "LEGEND", TextCompare: false) == 0)
		{
			Text = "Legend Format";
			cmdNew.Text = "New Legend Formatter";
		}
		ListenExpr = true;
	}

	private void cmdClear_Click(object sender, EventArgs e)
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
							goto IL_0030;
						case 5:
							goto IL_0047;
						case 6:
							goto IL_0053;
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
					IL_0030:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Clear the Editor?", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "Clear Editor?");
					goto IL_0047;
					IL_0047:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0053;
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(TextBox1.Text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0030;
					IL_0053:
					num2 = 6;
					TextBox1.Text = "";
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				fSaveExpr = true;
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

	private void cmdNew_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string prompt = default(string);
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
				case 1277:
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
							goto IL_0019;
						case 6:
							goto IL_0053;
						case 8:
							goto IL_005f;
						case 10:
							goto IL_006b;
						case 5:
						case 7:
						case 9:
						case 11:
						case 12:
							goto IL_0078;
						case 13:
							goto IL_009a;
						case 14:
							goto IL_00af;
						case 16:
						case 17:
						case 18:
							goto IL_00c9;
						case 20:
							goto IL_0107;
						case 22:
							goto IL_0121;
						case 24:
							goto IL_013b;
						case 25:
							goto IL_014f;
						case 26:
							goto IL_0173;
						case 27:
							goto IL_0197;
						case 28:
							goto IL_01bb;
						case 29:
							goto IL_01df;
						case 30:
							goto IL_0203;
						case 31:
							goto IL_0227;
						case 32:
							goto IL_024b;
						case 33:
							goto IL_026f;
						case 34:
							goto IL_0293;
						case 35:
							goto IL_02b7;
						case 36:
							goto IL_02db;
						case 37:
							goto IL_02ff;
						case 38:
							goto IL_0323;
						case 39:
							goto IL_0347;
						case 40:
							goto IL_036b;
						case 41:
							goto IL_038f;
						case 42:
							goto IL_03b3;
						case 43:
							goto IL_03d7;
						case 44:
							goto IL_03fb;
						case 19:
						case 21:
						case 23:
						case 45:
						case 46:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
						case 47:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_03fb:
					num2 = 44;
					TextBox1.Text += "\r\n//   ,renderTo: $('#selectorContainer')  //render in separate container ";
					break;
					IL_0121:
					num2 = 22;
					TextBox1.Text = "return '<DIV style=\"background:white; color:black; text-align:center\">' + 'Custom tool tip <br>X Column= ' + categoryAxis.dataField + ': X Title= ' + categoryAxis.title.text + ': X Value = '+ categoryValue + '<br>Y Column= ' + serie.dataField + ': Y Title= ' + serie.displayText + ': Y Value= ' + value + '<br>Data Source Index= ' + itemIndex + '</DIV>';";
					break;
					IL_03d7:
					num2 = 43;
					TextBox1.Text += "\r\n//   ,titlePadding: { left: 0, right: 0, top: 20, bottom: 0 }";
					goto IL_03fb;
					IL_0107:
					num2 = 20;
					TextBox1.Text = "return value.replace(/_/g, ' ').toUpperCase();";
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					prompt = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					switch (fType)
					{
					case "TOOLTIP":
						break;
					case "LEGEND":
						goto IL_005f;
					case "RANGE":
						goto IL_006b;
					default:
						goto IL_0078;
					}
					goto IL_0053;
					IL_006b:
					num2 = 10;
					prompt = "Create New Range Selector Code";
					goto IL_0078;
					IL_005f:
					num2 = 8;
					prompt = "Create a New Legend Format Function?";
					goto IL_0078;
					IL_0053:
					num2 = 6;
					prompt = "Create a New Custom ToolTip Template";
					goto IL_0078;
					IL_0078:
					num2 = 12;
					if (Operators.CompareString(TextBox1.Text, "", TextCompare: false) != 0)
					{
						goto IL_009a;
					}
					goto IL_00c9;
					IL_009a:
					num2 = 13;
					num5 = (int)Interaction.MsgBox(prompt, MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3, "New Item?");
					goto IL_00af;
					IL_00af:
					num2 = 14;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_00c9;
					IL_00c9:
					num2 = 18;
					switch (fType)
					{
					case "LEGEND":
						break;
					case "TOOLTIP":
						goto IL_0121;
					case "RANGE":
						goto IL_013b;
					default:
						goto end_IL_0001_2;
					}
					goto IL_0107;
					IL_013b:
					num2 = 24;
					TextBox1.Text = " backgroundColor:  'white'";
					goto IL_014f;
					IL_014f:
					num2 = 25;
					TextBox1.Text += "\r\n,gridLines: { visible: false }";
					goto IL_0173;
					IL_0173:
					num2 = 26;
					TextBox1.Text += "\r\n//  Below are sample commented options";
					goto IL_0197;
					IL_0197:
					num2 = 27;
					TextBox1.Text += "\r\n//   ,serieType: 'area'";
					goto IL_01bb;
					IL_01bb:
					num2 = 28;
					TextBox1.Text += "\r\n//   ,padding: {/* left: 0, right: 0,*/ top: 20, bottom: 0 }";
					goto IL_01df;
					IL_01df:
					num2 = 29;
					TextBox1.Text += "\r\n//   ,size: 110";
					goto IL_0203;
					IL_0203:
					num2 = 30;
					TextBox1.Text += "\r\n//   ,showBorderLine: false";
					goto IL_0227;
					IL_0227:
					num2 = 31;
					TextBox1.Text += "\r\n//   ,borderLineWidth : 1";
					goto IL_024b;
					IL_024b:
					num2 = 32;
					TextBox1.Text += "\r\n//   ,borderLineColor: 'Blue'";
					goto IL_026f;
					IL_026f:
					num2 = 33;
					TextBox1.Text += "\r\n//   ,dataField: 'Close'";
					goto IL_0293;
					IL_0293:
					num2 = 34;
					TextBox1.Text += "\r\n//   ,minValue: new Date(2010, 5, 1)";
					goto IL_02b7;
					IL_02b7:
					num2 = 35;
					TextBox1.Text += "\r\n//   ,maxValue: new Date(2010, 5, 1)";
					goto IL_02db;
					IL_02db:
					num2 = 36;
					TextBox1.Text += "\r\n//   ,baseUnit: 'month' //'year' 'month' 'day' 'hour' 'minute' 'second' 'millisecond'";
					goto IL_02ff;
					IL_02ff:
					num2 = 37;
					TextBox1.Text += "\r\n//   ,dateFormat: 'yyyy-MM-dd HH' //'yyyy-MM-dd HH:MM:SS' 'MMMM yyyy'";
					goto IL_0323;
					IL_0323:
					num2 = 38;
					TextBox1.Text += "\r\n//   ,labels: {angle: -30, rotationPoint: 'right', verticalAlignment: 'top', offset: { x: 0, y: 0 }}";
					goto IL_0347;
					IL_0347:
					num2 = 39;
					TextBox1.Text += "\r\n//   ,valuesOnTicks: true";
					goto IL_036b;
					IL_036b:
					num2 = 40;
					TextBox1.Text += "\r\n//   ,unitInterval: 12";
					goto IL_038f;
					IL_038f:
					num2 = 41;
					TextBox1.Text += "\r\n//   ,labels: {formatFunction: function (value) {return months[value.getMonth()] + '\\'' + value.getFullYear().toString().substring(2);}}";
					goto IL_03b3;
					IL_03b3:
					num2 = 42;
					TextBox1.Text += "\r\n//   ,title: 'Sample Title'";
					goto IL_03d7;
					end_IL_0001_2:
					break;
				}
				num2 = 46;
				fSaveExpr = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1277;
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

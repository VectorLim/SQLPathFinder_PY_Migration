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
public class FrmText : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOK")]
	private Button _cmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdfont")]
	private ToolStripButton _cmdfont;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdColor")]
	private ToolStripButton _cmdColor;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmbAlign")]
	private ToolStripComboBox _cmbAlign;

	private string f_FontColor;

	private string f_Bold;

	private string f_Italic;

	private string f_Underline;

	private string f_Font;

	private int f_Fontsz;

	private string f_Backcolor;

	private FontStyle MyBoldf;

	private FontStyle MyItalicf;

	private FontStyle MyUnderlf;

	private Color ColorFF;

	private Color ColorBF;

	public string f_Data;

	public bool f_OK;

	public string f_cs;

	private const string f_DLM = "<:>";

	[field: AccessedThroughProperty("TxtData")]
	internal virtual TextBox TxtData
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("FontDialog1")]
	internal virtual FontDialog FontDialog1
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

	[field: AccessedThroughProperty("mnuToolStrip")]
	internal virtual ToolStrip mnuToolStrip
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdfont
	{
		[CompilerGenerated]
		get
		{
			return _cmdfont;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdfont_Click;
			ToolStripButton toolStripButton = _cmdfont;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdfont = value;
			toolStripButton = _cmdfont;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton cmdColor
	{
		[CompilerGenerated]
		get
		{
			return _cmdColor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColor_Click;
			ToolStripButton toolStripButton = _cmdColor;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdColor = value;
			toolStripButton = _cmdColor;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblAlign")]
	internal virtual ToolStripLabel LblAlign
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripComboBox cmbAlign
	{
		[CompilerGenerated]
		get
		{
			return _cmbAlign;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbAlign_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = _cmbAlign;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			_cmbAlign = value;
			toolStripComboBox = _cmbAlign;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblColSpan")]
	internal virtual ToolStripLabel LblColSpan
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbColSpan")]
	internal virtual ToolStripComboBox cmbColSpan
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmText()
	{
		base.Load += FrmText_Load;
		f_FontColor = "black";
		f_Bold = "N";
		f_Italic = "N";
		f_Underline = "N";
		f_Font = "Verdana";
		f_Fontsz = 12;
		f_Backcolor = "white";
		MyBoldf = FontStyle.Regular;
		MyItalicf = FontStyle.Regular;
		MyUnderlf = FontStyle.Regular;
		ColorFF = Color.Black;
		ColorBF = Color.White;
		f_Data = "";
		f_OK = false;
		f_cs = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmText));
		this.TxtData = new System.Windows.Forms.TextBox();
		this.cmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.FontDialog1 = new System.Windows.Forms.FontDialog();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.mnuToolStrip = new System.Windows.Forms.ToolStrip();
		this.cmdfont = new System.Windows.Forms.ToolStripButton();
		this.cmdColor = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.LblAlign = new System.Windows.Forms.ToolStripLabel();
		this.cmbAlign = new System.Windows.Forms.ToolStripComboBox();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.LblColSpan = new System.Windows.Forms.ToolStripLabel();
		this.cmbColSpan = new System.Windows.Forms.ToolStripComboBox();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.mnuToolStrip.SuspendLayout();
		base.SuspendLayout();
		this.TxtData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtData.Location = new System.Drawing.Point(4, 47);
		this.TxtData.Margin = new System.Windows.Forms.Padding(4);
		this.TxtData.Multiline = true;
		this.TxtData.Name = "TxtData";
		this.TxtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtData.Size = new System.Drawing.Size(573, 307);
		this.TxtData.TabIndex = 0;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdOK.ImageIndex = 0;
		this.cmdOK.Location = new System.Drawing.Point(585, 47);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(87, 49);
		this.cmdOK.TabIndex = 1;
		this.cmdOK.Text = "OK";
		this.cmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.CmdCancel.ImageIndex = 1;
		this.CmdCancel.Location = new System.Drawing.Point(585, 98);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(87, 49);
		this.CmdCancel.TabIndex = 2;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.mnuToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.mnuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.cmdfont, this.cmdColor, this.ToolStripSeparator1, this.LblAlign, this.cmbAlign, this.ToolStripSeparator2, this.LblColSpan, this.cmbColSpan });
		this.mnuToolStrip.Location = new System.Drawing.Point(0, 0);
		this.mnuToolStrip.Name = "mnuToolStrip";
		this.mnuToolStrip.Size = new System.Drawing.Size(673, 28);
		this.mnuToolStrip.TabIndex = 10;
		this.mnuToolStrip.Text = "ToolStrip1";
		this.cmdfont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdfont.Image = (System.Drawing.Image)resources.GetObject("cmdfont.Image");
		this.cmdfont.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdfont.Name = "cmdfont";
		this.cmdfont.Size = new System.Drawing.Size(24, 25);
		this.cmdfont.Text = "Font";
		this.cmdfont.ToolTipText = "Font";
		this.cmdColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdColor.Image = (System.Drawing.Image)resources.GetObject("cmdColor.Image");
		this.cmdColor.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdColor.Name = "cmdColor";
		this.cmdColor.Size = new System.Drawing.Size(24, 25);
		this.cmdColor.Text = "Back Color";
		this.cmdColor.ToolTipText = "Back Color";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 28);
		this.LblAlign.Name = "LblAlign";
		this.LblAlign.Size = new System.Drawing.Size(98, 25);
		this.LblAlign.Text = "   Alignment  ";
		this.cmbAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlign.Items.AddRange(new object[3] { "Center", "Left", "Right" });
		this.cmbAlign.Name = "cmbAlign";
		this.cmbAlign.Size = new System.Drawing.Size(160, 28);
		this.cmbAlign.ToolTipText = "Text Alignment";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 28);
		this.LblColSpan.Name = "LblColSpan";
		this.LblColSpan.Size = new System.Drawing.Size(117, 25);
		this.LblColSpan.Text = "   Column Span  ";
		this.cmbColSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbColSpan.Items.AddRange(new object[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
		this.cmbColSpan.Name = "cmbColSpan";
		this.cmbColSpan.Size = new System.Drawing.Size(121, 28);
		this.cmbColSpan.ToolTipText = "Text Column Span";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(673, 354);
		base.Controls.Add(this.mnuToolStrip);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.TxtData);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmText";
		this.Text = "Text";
		this.mnuToolStrip.ResumeLayout(false);
		this.mnuToolStrip.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public void Set_Text_Font()
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
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "FrmText - Set_Text_Font";
					if (Operators.CompareString(f_Bold, "Y", TextCompare: false) == 0)
					{
						MyBoldf = FontStyle.Bold;
					}
					else
					{
						MyBoldf = FontStyle.Regular;
					}
					if (Operators.CompareString(f_Italic, "Y", TextCompare: false) == 0)
					{
						MyItalicf = FontStyle.Italic;
					}
					else
					{
						MyItalicf = FontStyle.Regular;
					}
					if (Operators.CompareString(f_Underline, "Y", TextCompare: false) == 0)
					{
						MyUnderlf = FontStyle.Underline;
					}
					else
					{
						MyUnderlf = FontStyle.Regular;
					}
					ColorFF = ColorTranslator.FromHtml(f_FontColor);
					ColorBF = ColorTranslator.FromHtml(f_Backcolor);
					goto end_IL_0001;
				case 226:
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
				try0001_dispatch = 226;
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

	private void FrmText_Load(object sender, EventArgs e)
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
						errsource = "FrmText - Form_Load";
						string text = "Center";
						string text2 = "1";
						int num3 = 0;
						string text3 = "";
						Button MyButton = CmdCancel;
						BuildForm.Set_Btn_Img(ref MyButton, "cancel");
						CmdCancel = MyButton;
						MyButton = cmdOK;
						BuildForm.Set_Btn_Img(ref MyButton, "ok");
						cmdOK = MyButton;
						string[] array;
						if (Operators.CompareString(f_Data, "", TextCompare: false) != 0)
						{
							array = Strings.Split(f_Data, "<:>");
							f_Backcolor = array[0];
							f_FontColor = array[1];
							f_Bold = array[2];
							f_Italic = array[3];
							f_Underline = array[4];
							f_Font = array[5];
							f_Fontsz = Conversions.ToInteger(array[6]);
							text = array[7];
							text3 = array[8];
							text3 = Strings.Replace(text3, "<br>", "\r\n", 1, -1, CompareMethod.Text);
							text3 = General_Procedures.Quote_CRLF_Replace("SD", text3);
							TxtData.Text = General_Procedures.Quote_CRLF_Replace("DQ", text3);
							text2 = f_cs;
						}
						array = null;
						if ((Operators.CompareString(text, "Center", TextCompare: false) == 0) | (Operators.CompareString(text, "Left", TextCompare: false) == 0) | (Operators.CompareString(text, "Right", TextCompare: false) == 0))
						{
							int num4 = cmbAlign.Items.Count - 1;
							for (num3 = 0; num3 <= num4; num3++)
							{
								if (Operators.ConditionalCompareObjectEqual(cmbAlign.Items[num3], text, TextCompare: false))
								{
									cmbAlign.SelectedIndex = num3;
									break;
								}
							}
						}
						else
						{
							cmbAlign.SelectedIndex = 0;
						}
						if (Versioned.IsNumeric(text2) && Conversions.ToInteger(text2) >= 1 && Conversions.ToInteger(text2) <= 9)
						{
							cmbColSpan.SelectedIndex = Conversions.ToInteger(text2) - 1;
						}
						else
						{
							cmbColSpan.SelectedIndex = 0;
						}
						Set_Text_Font();
						Set_Align();
						TxtData.Font = new Font(f_Font, f_Fontsz, MyBoldf | MyItalicf | MyUnderlf);
						TxtData.ForeColor = ColorFF;
						TxtData.BackColor = ColorBF;
						goto end_IL_0001;
					}
					case 694:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Close();
							goto end_IL_0001;
						}
						break;
					}
					goto IL_02ec;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 694;
				continue;
			}
			break;
			IL_02ec:
			throw ProjectData.CreateProjectError(-2146828237);
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
				case 491:
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
							goto IL_0021;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_005d;
						case 7:
							goto IL_007b;
						case 8:
							goto IL_0099;
						case 9:
							goto IL_00b7;
						case 10:
							goto IL_00db;
						case 11:
							goto IL_00ff;
						case 12:
							goto IL_0120;
						case 13:
							goto IL_012f;
						case 14:
							goto IL_013e;
						case 15:
							goto IL_0148;
						case 16:
							goto IL_0162;
						case 17:
							goto IL_0176;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0148:
					num2 = 15;
					f_Data = f_Data + "<:>" + text;
					goto IL_0162;
					IL_0162:
					num2 = 16;
					f_cs = cmbColSpan.Text;
					goto IL_0176;
					IL_013e:
					num2 = 14;
					text = General_Procedures.Rep_Prob_Quote(text);
					goto IL_0148;
					IL_0176:
					num2 = 17;
					f_OK = true;
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					f_Data = f_Backcolor;
					goto IL_0021;
					IL_0021:
					num2 = 4;
					f_Data = f_Data + "<:>" + f_FontColor;
					goto IL_003f;
					IL_003f:
					num2 = 5;
					f_Data = f_Data + "<:>" + f_Bold;
					goto IL_005d;
					IL_005d:
					num2 = 6;
					f_Data = f_Data + "<:>" + f_Italic;
					goto IL_007b;
					IL_007b:
					num2 = 7;
					f_Data = f_Data + "<:>" + f_Underline;
					goto IL_0099;
					IL_0099:
					num2 = 8;
					f_Data = f_Data + "<:>" + f_Font;
					goto IL_00b7;
					IL_00b7:
					num2 = 9;
					f_Data = f_Data + "<:>" + Conversions.ToString(f_Fontsz);
					goto IL_00db;
					IL_00db:
					num2 = 10;
					f_Data = f_Data + "<:>" + cmbAlign.Text;
					goto IL_00ff;
					IL_00ff:
					num2 = 11;
					text = Strings.Replace(TxtData.Text, "\r\n", "<br>", 1, -1, CompareMethod.Text);
					goto IL_0120;
					IL_0120:
					num2 = 12;
					text = General_Procedures.Quote_CRLF_Replace("SE", text);
					goto IL_012f;
					IL_012f:
					num2 = 13;
					text = General_Procedures.Quote_CRLF_Replace("EQ", text);
					goto IL_013e;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				Close();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 491;
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

	public void Set_Align()
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
					goto IL_000c;
				case 183:
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
							goto IL_000c;
						case 4:
							goto IL_0047;
						case 6:
							goto IL_0059;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000c:
					num2 = 2;
					switch (cmbAlign.Text)
					{
					case "Center":
						break;
					case "Left":
						goto IL_0059;
					case "Right":
						goto end_IL_0001_2;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0047;
					IL_0059:
					num2 = 6;
					TxtData.TextAlign = HorizontalAlignment.Left;
					goto end_IL_0001_3;
					IL_0047:
					num2 = 4;
					TxtData.TextAlign = HorizontalAlignment.Center;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				TxtData.TextAlign = HorizontalAlignment.Right;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 183;
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

	private void cmdfont_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string errsource = default(string);
		int num5 = default(int);
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
					num3 = 2;
					goto IL_000a;
				case 990:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
						case 3:
							break;
						case 1:
							goto IL_0302;
						default:
							goto end_IL_0001;
						}
						goto IL_02d1;
					}
					IL_0282:
					num2 = 45;
					TxtData.Font = new Font(f_Font, f_Fontsz, MyBoldf | MyItalicf | MyUnderlf);
					goto IL_02b7;
					IL_02b7:
					num2 = 46;
					TxtData.ForeColor = ColorFF;
					goto end_IL_0001_2;
					IL_0278:
					num2 = 44;
					Set_Text_Font();
					goto IL_0282;
					IL_0302:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_000a;
					case 3:
						goto IL_0012;
					case 4:
						goto IL_0017;
					case 5:
						goto IL_0020;
					case 6:
						goto IL_003f;
					case 7:
						goto IL_004c;
					case 8:
						goto IL_0055;
					case 9:
						goto IL_0069;
					case 10:
						goto IL_007f;
					case 11:
						goto IL_0095;
					case 12:
						goto IL_00a3;
					case 13:
					case 15:
					case 16:
					case 17:
						goto IL_00c3;
					case 18:
						goto IL_00d1;
					case 19:
					case 20:
						goto IL_00db;
					case 21:
						goto IL_0110;
					case 22:
						goto IL_0120;
					case 23:
						goto IL_0130;
					case 24:
						goto IL_0140;
					case 25:
						goto IL_0150;
					case 26:
						goto IL_016a;
					case 27:
						goto IL_0183;
					case 29:
						goto IL_0195;
					case 28:
					case 30:
					case 31:
						goto IL_01a4;
					case 32:
						goto IL_01bd;
					case 34:
						goto IL_01cf;
					case 33:
					case 35:
					case 36:
						goto IL_01de;
					case 37:
						goto IL_01f7;
					case 39:
						goto IL_0209;
					case 38:
					case 40:
					case 41:
						goto IL_0218;
					case 42:
						goto IL_0236;
					case 43:
						goto IL_0256;
					case 44:
						goto IL_0278;
					case 45:
						goto IL_0282;
					case 46:
						goto IL_02b7;
					case 49:
						goto IL_02d1;
					case 50:
						goto end_IL_0001_3;
					default:
						goto end_IL_0001;
					case 14:
					case 47:
					case 48:
					case 51:
						goto end_IL_0001_2;
					}
					goto default;
					IL_02d1:
					num2 = 49;
					Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
					break;
					IL_000a:
					num2 = 2;
					errsource = "FrmText - CmdFont_Click";
					goto IL_0012;
					IL_0012:
					num2 = 3;
					num5 = 0;
					goto IL_0017;
					IL_0017:
					num2 = 4;
					Set_Text_Font();
					goto IL_0020;
					IL_0020:
					num2 = 5;
					if (Operators.CompareString(f_FontColor, "", TextCompare: false) != 0)
					{
						goto IL_003f;
					}
					goto IL_00db;
					IL_003f:
					num2 = 6;
					Information.Err().Clear();
					goto IL_004c;
					IL_004c:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0055;
					IL_0055:
					num2 = 8;
					FontDialog1.Color = ColorFF;
					goto IL_0069;
					IL_0069:
					num2 = 9;
					if (Information.Err().Number == 5)
					{
						goto IL_007f;
					}
					goto IL_00c3;
					IL_007f:
					num2 = 10;
					num5 = (int)Interaction.MsgBox("The color you have entered is not supported in the Font Dialog Box (:(). Do you wish to reset it to Black or keep the assigned value?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Unsupported Color in Dialog");
					goto IL_0095;
					IL_0095:
					num2 = 11;
					if (num5 != 6)
					{
						goto end_IL_0001_2;
					}
					goto IL_00a3;
					IL_00a3:
					num2 = 12;
					FontDialog1.Color = Color.Black;
					goto IL_00c3;
					IL_00c3:
					num2 = 17;
					Information.Err().Clear();
					goto IL_00d1;
					IL_00d1:
					ProjectData.ClearProjectError();
					num3 = 3;
					goto IL_00db;
					IL_00db:
					num2 = 20;
					FontDialog1.Font = new Font(f_Font, f_Fontsz, MyBoldf | MyItalicf | MyUnderlf);
					goto IL_0110;
					IL_0110:
					num2 = 21;
					FontDialog1.AllowScriptChange = false;
					goto IL_0120;
					IL_0120:
					num2 = 22;
					FontDialog1.ShowEffects = true;
					goto IL_0130;
					IL_0130:
					num2 = 23;
					FontDialog1.ShowColor = true;
					goto IL_0140;
					IL_0140:
					num2 = 24;
					FontDialog1.FontMustExist = true;
					goto IL_0150;
					IL_0150:
					num2 = 25;
					if (FontDialog1.ShowDialog() != DialogResult.OK)
					{
						goto end_IL_0001_2;
					}
					goto IL_016a;
					IL_016a:
					num2 = 26;
					if (FontDialog1.Font.Bold)
					{
						goto IL_0183;
					}
					goto IL_0195;
					IL_0183:
					num2 = 27;
					f_Bold = "Y";
					goto IL_01a4;
					IL_0195:
					num2 = 29;
					f_Bold = "N";
					goto IL_01a4;
					IL_01a4:
					num2 = 31;
					if (FontDialog1.Font.Italic)
					{
						goto IL_01bd;
					}
					goto IL_01cf;
					IL_01bd:
					num2 = 32;
					f_Italic = "Y";
					goto IL_01de;
					IL_01cf:
					num2 = 34;
					f_Italic = "N";
					goto IL_01de;
					IL_01de:
					num2 = 36;
					if (FontDialog1.Font.Underline)
					{
						goto IL_01f7;
					}
					goto IL_0209;
					IL_01f7:
					num2 = 37;
					f_Underline = "Y";
					goto IL_0218;
					IL_0209:
					num2 = 39;
					f_Underline = "N";
					goto IL_0218;
					IL_0218:
					num2 = 41;
					f_Font = Strings.LCase(FontDialog1.Font.Name);
					goto IL_0236;
					IL_0236:
					num2 = 42;
					f_Fontsz = checked((int)Math.Round(FontDialog1.Font.Size));
					goto IL_0256;
					IL_0256:
					num2 = 43;
					f_FontColor = Strings.LCase(FontDialog1.Color.Name);
					goto IL_0278;
					end_IL_0001_3:
					break;
				}
				num2 = 50;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 990;
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

	private void cmdColor_Click(object sender, EventArgs e)
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
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "FrmText - CmdColor_Click";
					if (Operators.CompareString(f_Backcolor, "", TextCompare: false) != 0)
					{
						ColorDialog1.Color = ColorBF;
					}
					ColorDialog1.FullOpen = true;
					if (ColorDialog1.ShowDialog() == DialogResult.OK)
					{
						f_Backcolor = ColorTranslator.ToHtml(ColorDialog1.Color);
						Set_Text_Font();
						TxtData.BackColor = ColorBF;
					}
					goto end_IL_0001;
				case 183:
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
				try0001_dispatch = 183;
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

	private void cmbAlign_SelectedIndexChanged(object sender, EventArgs e)
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
				Set_Align();
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
}

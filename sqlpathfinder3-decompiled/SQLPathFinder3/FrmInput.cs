using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;
using SQLPathFinder3.My.Resources;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmInput : Form
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

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdLoad")]
	private ToolStripButton _cmdLoad;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdSave")]
	private ToolStripButton _cmdSave;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdReload")]
	private ToolStripButton _cmdReload;

	public string f_InputFile;

	public string F_OutputFile;

	public string F_Output;

	public string F_Mode;

	private string f_DLM;

	private int f_NoTabs;

	private int f_NoCtlPerTab;

	private ComboBox f_NodeStored1;

	private ComboBox f_NodeStored2;

	private ComboBox f_NodeStored3;

	private ComboBox f_NodeStored4;

	private ComboBox f_NodeStored5;

	private ComboBox f_NodeStored6;

	private ComboBox f_NodeStored7;

	private ComboBox f_NodeStored8;

	private ComboBox f_NodeStored9;

	private string f_NoDisplayHdrs;

	private string f_NoDisplayData;

	private string f_MidasDriver;

	private string f_IgnoreCond;

	private const int lRequired = 1;

	private const int lwritefile = 2;

	private const int lStripQuotes = 3;

	private const int lReverseSlash = 4;

	private const int lSubStituteMissing = 5;

	private const int lSQLHelp = 6;

	private const int lTransform = 7;

	private const int lStripExt = 8;

	private const int lXlateNode = 9;

	private const int lFileOpen = 10;

	private const int lXlateNodeAppend = 11;

	private const int lMask = 12;

	private const int lpreservedoubleq = 13;

	private const int ldisplaymember = 14;

	private const int lStartVarName = 15;

	[field: AccessedThroughProperty("TabControl1")]
	internal virtual TabControl TabControl1
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

	[field: AccessedThroughProperty("LblTitle")]
	internal virtual Label LblTitle
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

	[field: AccessedThroughProperty("ToolStrip1")]
	internal virtual ToolStrip ToolStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdLoad
	{
		[CompilerGenerated]
		get
		{
			return _cmdLoad;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLoad_Click;
			ToolStripButton toolStripButton = _cmdLoad;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdLoad = value;
			toolStripButton = _cmdLoad;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton cmdSave
	{
		[CompilerGenerated]
		get
		{
			return _cmdSave;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSave_Click;
			ToolStripButton toolStripButton = _cmdSave;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdSave = value;
			toolStripButton = _cmdSave;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton cmdReload
	{
		[CompilerGenerated]
		get
		{
			return _cmdReload;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdReload_Click;
			ToolStripButton toolStripButton = _cmdReload;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdReload = value;
			toolStripButton = _cmdReload;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	public FrmInput()
	{
		base.FormClosing += FrmInput_FormClosing;
		base.Load += FrmInput_Load;
		f_InputFile = "";
		F_OutputFile = "";
		F_Output = "";
		F_Mode = "";
		f_DLM = "";
		f_NoTabs = 1;
		f_NoCtlPerTab = 20;
		f_NodeStored1 = null;
		f_NodeStored2 = null;
		f_NodeStored3 = null;
		f_NodeStored4 = null;
		f_NodeStored5 = null;
		f_NodeStored6 = null;
		f_NodeStored7 = null;
		f_NodeStored8 = null;
		f_NodeStored9 = null;
		f_NoDisplayHdrs = "";
		f_NoDisplayData = "";
		f_MidasDriver = "";
		f_IgnoreCond = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmInput));
		this.TabControl1 = new System.Windows.Forms.TabControl();
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.LblTitle = new System.Windows.Forms.Label();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
		this.cmdLoad = new System.Windows.Forms.ToolStripButton();
		this.cmdSave = new System.Windows.Forms.ToolStripButton();
		this.cmdReload = new System.Windows.Forms.ToolStripButton();
		this.ToolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabControl1.Location = new System.Drawing.Point(0, 91);
		this.TabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.TabControl1.Name = "TabControl1";
		this.TabControl1.SelectedIndex = 0;
		this.TabControl1.Size = new System.Drawing.Size(860, 720);
		this.TabControl1.TabIndex = 0;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdOK.ImageKey = "OK.bmp";
		this.cmdOK.Location = new System.Drawing.Point(695, 39);
		this.cmdOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(77, 50);
		this.cmdOK.TabIndex = 1;
		this.cmdOK.Text = "OK";
		this.cmdOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.cmdCancel.ImageKey = "none.bmp";
		this.cmdCancel.Location = new System.Drawing.Point(778, 39);
		this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(77, 50);
		this.cmdCancel.TabIndex = 2;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.LblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblTitle.Location = new System.Drawing.Point(3, 39);
		this.LblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTitle.Name = "LblTitle";
		this.LblTitle.Size = new System.Drawing.Size(692, 41);
		this.LblTitle.TabIndex = 3;
		this.LblTitle.Text = "Gather Input";
		this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.cmdLoad, this.cmdSave, this.cmdReload });
		this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
		this.ToolStrip1.Name = "ToolStrip1";
		this.ToolStrip1.Size = new System.Drawing.Size(861, 27);
		this.ToolStrip1.TabIndex = 4;
		this.ToolStrip1.Text = "ToolStrip1";
		this.cmdLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdLoad.Image = (System.Drawing.Image)resources.GetObject("cmdLoad.Image");
		this.cmdLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdLoad.Name = "cmdLoad";
		this.cmdLoad.Size = new System.Drawing.Size(29, 24);
		this.cmdLoad.Text = "&Open";
		this.cmdLoad.ToolTipText = "Load Saved Input selections";
		this.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdSave.Image = (System.Drawing.Image)resources.GetObject("cmdSave.Image");
		this.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdSave.Name = "cmdSave";
		this.cmdSave.Size = new System.Drawing.Size(29, 28);
		this.cmdSave.Text = "&Save";
		this.cmdSave.ToolTipText = "Save Current Input Selections";
		this.cmdReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdReload.Image = SQLPathFinder3.My.Resources.Resources.RefreshW;
		this.cmdReload.ImageTransparentColor = System.Drawing.Color.White;
		this.cmdReload.Name = "cmdReload";
		this.cmdReload.Size = new System.Drawing.Size(29, 28);
		this.cmdReload.Text = "Load Last Selections";
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(861, 814);
		base.Controls.Add(this.ToolStrip1);
		base.Controls.Add(this.LblTitle);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.TabControl1);
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmInput";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gather Input";
		this.ToolStrip1.ResumeLayout(false);
		this.ToolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public string Test_Ignore_Cond(string MyData)
	{
		string result = MyData;
		MyData = Strings.UCase(Strings.Trim(MyData));
		if (Operators.CompareString(MyData, "", TextCompare: false) != 0 && Operators.CompareString(f_IgnoreCond, "", TextCompare: false) != 0 && Operators.CompareString(MyData, Strings.UCase(f_IgnoreCond), TextCompare: false) == 0)
		{
			result = "";
		}
		return result;
	}

	public bool ValidDate(string MyDT, string MyStr, string Myopr, ref string MyErr)
	{
		bool flag = true;
		MyStr = Strings.UCase(Strings.Trim(MyStr));
		MyDT = Strings.Trim(Strings.UCase(MyDT));
		Myopr = Strings.Trim(Strings.UCase(Myopr));
		MyErr = "";
		checked
		{
			if (Operators.CompareString(MyStr, "", TextCompare: false) != 0)
			{
				bool flag2 = true;
				int num = 0;
				int num2 = 0;
				string text = "";
				string text2 = "";
				string text3 = "";
				num = Strings.InStr(MyStr, ";");
				if (Operators.CompareString(Myopr, "BETWEEN", TextCompare: false) == 0)
				{
					if (num <= 1)
					{
						MyErr = MyStr;
						flag = false;
					}
					else if (num != 0 && Strings.Len(MyStr) > num)
					{
						text = Strings.UCase(Strings.Trim(Strings.Mid(MyStr, 1, num - 1)));
						text2 = Strings.UCase(Strings.Trim(Strings.Mid(MyStr, num + 1)));
					}
					else
					{
						MyErr = MyStr;
						flag = false;
					}
				}
				else if (num != 0)
				{
					MyErr = MyStr;
					flag = false;
				}
				else
				{
					text = Strings.UCase(Strings.Trim(MyStr));
					text2 = "";
				}
				if (flag)
				{
					num2 = 1;
					do
					{
						text3 = ((num2 != 1) ? text2 : text);
						if (flag && Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							string left = MyDT;
							if (Operators.CompareString(left, "G", TextCompare: false) == 0 && !LikeOperator.LikeString(text3, "*DATE*", CompareMethod.Binary) && !LikeOperator.LikeString(MyStr, "*CURRENT_TIMESTAMP(*", CompareMethod.Binary) && (Strings.Len(text3) != 19 || !Versioned.IsNumeric(Strings.Mid(text3, 1, 4)) || Operators.CompareString(Strings.Mid(text3, 1, 4), "2000", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 1, 4), "3000", TextCompare: false) > 0 || Operators.CompareString(Strings.Mid(text3, 5, 1), "-", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text3, 6, 2)) || Operators.CompareString(Strings.Mid(text3, 6, 2), "01", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 6, 2), "12", TextCompare: false) > 0 || Operators.CompareString(Strings.Mid(text3, 8, 1), "-", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text3, 9, 2)) || Operators.CompareString(Strings.Mid(text3, 9, 2), "01", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 9, 2), "31", TextCompare: false) > 0 || Operators.CompareString(Strings.Mid(text3, 11, 1), " ", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text3, 12, 2)) || Operators.CompareString(Strings.Mid(text3, 12, 2), "00", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 12, 2), "23", TextCompare: false) > 0 || Operators.CompareString(Strings.Mid(text3, 14, 1), ":", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text3, 15, 2)) || Operators.CompareString(Strings.Mid(text3, 15, 2), "00", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 15, 2), "59", TextCompare: false) > 0 || Operators.CompareString(Strings.Mid(text3, 17, 1), ":", TextCompare: false) != 0 || !Versioned.IsNumeric(Strings.Mid(text3, 18, 2)) || Operators.CompareString(Strings.Mid(text3, 18, 2), "00", TextCompare: false) < 0 || Operators.CompareString(Strings.Mid(text3, 18, 2), "59", TextCompare: false) > 0))
							{
								MyErr = text3;
								flag = false;
							}
						}
						num2++;
					}
					while (num2 <= 2);
				}
			}
			return flag;
		}
	}

	public void Load_Save_Inputs(string MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		int num7 = default(int);
		string text8 = default(string);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		int num11 = default(int);
		int num12 = default(int);
		int num13 = default(int);
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
					case 2276:
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
								goto IL_001d;
							case 6:
								goto IL_0026;
							case 7:
								goto IL_002f;
							case 8:
								goto IL_0038;
							case 9:
								goto IL_0041;
							case 10:
								goto IL_004c;
							case 11:
								goto IL_0056;
							case 12:
								goto IL_005c;
							case 13:
								goto IL_0066;
							case 14:
								goto IL_006c;
							case 15:
								goto IL_0072;
							case 16:
								goto IL_0085;
							case 17:
								goto IL_0093;
							case 18:
								goto IL_00a8;
							case 19:
								goto IL_00bb;
							case 20:
								goto IL_00c9;
							case 21:
								goto IL_00df;
							case 22:
								goto IL_00ff;
							case 23:
								goto IL_0110;
							case 24:
								goto IL_0121;
							case 26:
								goto IL_016f;
							case 27:
								goto IL_0185;
							case 28:
								goto IL_01a6;
							case 29:
								goto IL_01c1;
							case 30:
								goto IL_01e0;
							case 31:
								goto IL_022e;
							case 33:
								goto IL_024b;
							case 32:
							case 34:
							case 35:
								goto IL_0253;
							case 36:
								goto IL_026f;
							case 37:
								goto IL_0281;
							case 38:
								goto IL_0296;
							case 39:
								goto IL_02c2;
							case 40:
								goto IL_0302;
							case 41:
								goto IL_032b;
							case 42:
								goto IL_036c;
							case 43:
							case 44:
								goto IL_039d;
							case 45:
								goto IL_03af;
							case 46:
								goto IL_03be;
							case 47:
								goto IL_03cd;
							case 48:
								goto IL_03e6;
							case 51:
								goto IL_0400;
							case 52:
								goto IL_041b;
							case 53:
								goto IL_043a;
							case 54:
								goto IL_0488;
							case 56:
								goto IL_04a5;
							case 57:
								goto IL_04ac;
							case 58:
								goto IL_04c9;
							case 59:
								goto IL_04de;
							case 61:
							case 62:
								goto IL_04ef;
							case 63:
								goto IL_0505;
							case 55:
							case 65:
							case 66:
								goto IL_0519;
							case 67:
								goto IL_0535;
							case 68:
								goto IL_0554;
							case 69:
								goto IL_0584;
							case 71:
							case 72:
								goto IL_05a0;
							case 73:
								goto IL_05c4;
							case 74:
								goto IL_05ca;
							case 75:
								goto IL_05dc;
							case 76:
								goto IL_05f1;
							case 77:
								goto IL_0634;
							case 78:
								goto IL_063a;
							case 79:
								goto IL_0666;
							case 80:
								goto IL_06a6;
							case 81:
								goto IL_06e7;
							case 82:
								goto IL_0706;
							case 83:
							case 84:
								goto IL_0732;
							case 85:
								goto IL_0744;
							case 86:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 25:
							case 49:
							case 50:
							case 60:
							case 64:
							case 70:
							case 87:
							case 88:
							case 89:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_02c2:
						num2 = 39;
						if (LikeOperator.LikeString(Strings.LCase(TabControl1.TabPages[num5 - 1].Controls[num6].Name), "combobox*", CompareMethod.Binary))
						{
							goto IL_0302;
						}
						goto IL_039d;
						IL_0302:
						num2 = 40;
						text = TabControl1.TabPages[num5 - 1].Controls[num6].Text;
						goto IL_032b;
						IL_03af:
						num2 = 45;
						num5++;
						goto IL_03b6;
						IL_032b:
						num2 = 41;
						text2 = Strings.LCase(Strings.Mid(Conversions.ToString(TabControl1.TabPages[num5 - 1].Controls[num6].Tag), 15) + text3);
						goto IL_036c;
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
						text3 = "";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						text = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text2 = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						text4 = "INPUT-PROMPTS";
						goto IL_0038;
						IL_0038:
						num2 = 8;
						text5 = "";
						goto IL_0041;
						IL_0041:
						num2 = 9;
						text6 = f_InputFile;
						goto IL_004c;
						IL_004c:
						num2 = 10;
						text7 = "";
						goto IL_0056;
						IL_0056:
						num2 = 11;
						num7 = 0;
						goto IL_005c;
						IL_005c:
						num2 = 12;
						text8 = "";
						goto IL_0066;
						IL_0066:
						num2 = 13;
						num8 = 1;
						goto IL_006c;
						IL_006c:
						num2 = 14;
						num9 = 0;
						goto IL_0072;
						IL_0072:
						num2 = 15;
						num7 = Strings.InStrRev(text6, "\\");
						goto IL_0085;
						IL_0085:
						num2 = 16;
						if (num7 != 0)
						{
							goto IL_0093;
						}
						goto IL_00a8;
						IL_0093:
						num2 = 17;
						text6 = Strings.Trim(Strings.Mid(text6, num7 + 1));
						goto IL_00a8;
						IL_00a8:
						num2 = 18;
						num7 = Strings.InStrRev(text6, ".");
						goto IL_00bb;
						IL_00bb:
						num2 = 19;
						if (num7 != 0)
						{
							goto IL_00c9;
						}
						goto IL_00df;
						IL_00c9:
						num2 = 20;
						text6 = Strings.Trim(Strings.Mid(text6, 1, num7 - 1));
						goto IL_00df;
						IL_00df:
						num2 = 21;
						text7 = BuildForm.Strip_Add_MyPCDir("A", text6 + ".save_ini_1");
						goto IL_00ff;
						IL_00ff:
						num2 = 22;
						text6 += ".save_ini";
						goto IL_0110;
						IL_0110:
						num2 = 23;
						MyMode = Strings.Trim(Strings.UCase(MyMode));
						goto IL_0121;
						IL_0121:
						num2 = 24;
						switch (MyMode)
						{
						case "S":
						case "S1":
							break;
						case "L":
						case "L1":
							goto IL_0400;
						default:
							goto end_IL_0001_3;
						}
						goto IL_016f;
						IL_0400:
						num2 = 51;
						if (Operators.CompareString(MyMode, "L", TextCompare: false) == 0)
						{
							goto IL_041b;
						}
						goto IL_04a5;
						IL_041b:
						num2 = 52;
						BuildForm.FileOpenSave("O", text6, "save_ini", "Load Input Selections from a File", Globals_Renamed.gQueryDir);
						goto IL_043a;
						IL_043a:
						num2 = 53;
						if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) != 0))
						{
							goto IL_0488;
						}
						goto IL_0519;
						IL_0488:
						num2 = 54;
						text5 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						goto IL_0519;
						IL_04a5:
						num2 = 56;
						text5 = text7;
						goto IL_04ac;
						IL_04ac:
						num2 = 57;
						if (!MyProject.Computer.FileSystem.FileExists(text5))
						{
							goto IL_04c9;
						}
						goto IL_04ef;
						IL_04c9:
						num2 = 58;
						Interaction.MsgBox("No Input settings have been previously saved.", MsgBoxStyle.Exclamation, "No Input Specification File");
						goto IL_04de;
						IL_04de:
						num2 = 59;
						text5 = "";
						goto end_IL_0001_3;
						IL_04ef:
						num2 = 62;
						num9 = unchecked((int)Interaction.MsgBox("This action will clear current selections. Do you wish to continue?", MsgBoxStyle.OkCancel | MsgBoxStyle.Question, "Continue?"));
						goto IL_0505;
						IL_0505:
						num2 = 63;
						if (num9 == 2)
						{
							goto end_IL_0001_3;
						}
						goto IL_0519;
						IL_0519:
						num2 = 66;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0535;
						IL_0535:
						num2 = 67;
						text = Strings.Trim(General_Procedures.Get_Ini_Data(text4, "SPF$-Input$-Version$", "", 50, text5));
						goto IL_0554;
						IL_0554:
						num2 = 68;
						if (Operators.CompareString(text, "1.0", TextCompare: false) != 0 && !LikeOperator.LikeString(text5.ToLower(), "*.save_ini", CompareMethod.Binary))
						{
							goto IL_0584;
						}
						goto IL_05a0;
						IL_036c:
						num2 = 42;
						text4 = text4 + "\r\n" + text2 + "=" + text;
						goto IL_039d;
						IL_0584:
						num2 = 69;
						Interaction.MsgBox("The Input Data file is not a valid SQLPathFinder input file format", MsgBoxStyle.Exclamation, "Unrecognised");
						goto end_IL_0001_3;
						IL_05a0:
						num2 = 72;
						text8 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(text4, "SPF$-ACTIVE-TAB$", "", 100, text5)));
						goto IL_05c4;
						IL_05c4:
						num2 = 73;
						num8 = 1;
						goto IL_05ca;
						IL_05ca:
						num2 = 74;
						num10 = f_NoTabs;
						num5 = 1;
						goto IL_074b;
						IL_074b:
						if (num5 > num10)
						{
							break;
						}
						goto IL_05dc;
						IL_05dc:
						num2 = 75;
						text3 = "-" + Conversions.ToString(num5);
						goto IL_05f1;
						IL_05f1:
						num2 = 76;
						if (Operators.CompareString(text8, "", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(TabControl1.TabPages[num5 - 1].Text), text8, TextCompare: false) == 0)
						{
							goto IL_0634;
						}
						goto IL_063a;
						IL_039d:
						num2 = 44;
						num6++;
						goto IL_03a6;
						IL_0634:
						num2 = 77;
						num8 = num5;
						goto IL_063a;
						IL_063a:
						num2 = 78;
						num11 = TabControl1.TabPages[num5 - 1].Controls.Count - 1;
						num6 = 0;
						goto IL_073b;
						IL_073b:
						if (num6 <= num11)
						{
							goto IL_0666;
						}
						goto IL_0744;
						IL_0744:
						num2 = 85;
						num5++;
						goto IL_074b;
						IL_0666:
						num2 = 79;
						if (LikeOperator.LikeString(Strings.LCase(TabControl1.TabPages[num5 - 1].Controls[num6].Name), "combobox*", CompareMethod.Binary))
						{
							goto IL_06a6;
						}
						goto IL_0732;
						IL_06a6:
						num2 = 80;
						text2 = Strings.LCase(Strings.Mid(Conversions.ToString(TabControl1.TabPages[num5 - 1].Controls[num6].Tag), 15) + text3);
						goto IL_06e7;
						IL_06e7:
						num2 = 81;
						text = Strings.Trim(General_Procedures.Get_Ini_Data(text4, text2, "", 5000, text5));
						goto IL_0706;
						IL_0706:
						num2 = 82;
						TabControl1.TabPages[num5 - 1].Controls[num6].Text = text;
						goto IL_0732;
						IL_0732:
						num2 = 84;
						num6++;
						goto IL_073b;
						IL_016f:
						num2 = 26;
						text4 = "[" + text4 + "]\r\nSPF$-Input$-Version$=1.0";
						goto IL_0185;
						IL_0185:
						num2 = 27;
						text4 = text4 + "\r\nSPF$-ACTIVE-TAB$=" + TabControl1.SelectedTab.Text;
						goto IL_01a6;
						IL_01a6:
						num2 = 28;
						if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
						{
							goto IL_01c1;
						}
						goto IL_024b;
						IL_01c1:
						num2 = 29;
						BuildForm.FileOpenSave("S", text6, "save_ini", "Specify File to Store Input selections", Globals_Renamed.gQueryDir);
						goto IL_01e0;
						IL_01e0:
						num2 = 30;
						if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "", TextCompare: false) != 0))
						{
							goto IL_022e;
						}
						goto IL_0253;
						IL_022e:
						num2 = 31;
						text5 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
						goto IL_0253;
						IL_024b:
						num2 = 33;
						text5 = text7;
						goto IL_0253;
						IL_0253:
						num2 = 35;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_026f;
						IL_026f:
						num2 = 36;
						num12 = f_NoTabs;
						num5 = 1;
						goto IL_03b6;
						IL_03b6:
						if (num5 <= num12)
						{
							goto IL_0281;
						}
						goto IL_03be;
						IL_03be:
						num2 = 46;
						text = General_Procedures.Save_File_General(text4, text5);
						goto IL_03cd;
						IL_03cd:
						num2 = 47;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_03e6;
						IL_03e6:
						num2 = 48;
						Interaction.MsgBox(text4, MsgBoxStyle.Exclamation, "Could not Save Input selections");
						goto end_IL_0001_3;
						IL_0281:
						num2 = 37;
						text3 = "-" + Conversions.ToString(num5);
						goto IL_0296;
						IL_0296:
						num2 = 38;
						num13 = TabControl1.TabPages[num5 - 1].Controls.Count - 1;
						num6 = 0;
						goto IL_03a6;
						IL_03a6:
						if (num6 <= num13)
						{
							goto IL_02c2;
						}
						goto IL_03af;
						end_IL_0001_2:
						break;
					}
					num2 = 86;
					TabControl1.SelectedIndex = num8 - 1;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2276;
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

	public string StripExt(string MyFile)
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
				case 115:
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
							goto IL_0010;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_0025;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0010:
					num2 = 3;
					text = MyFile;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num5 = Strings.InStrRev(text, ".");
					goto IL_0025;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0025:
					num2 = 5;
					if (num5 <= 1)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				text = Strings.Trim(Strings.Mid(text, 1, checked(num5 - 1)));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 115;
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
		return text;
	}

	public void Assign_Node(string MyMode, int MyTab, ref ComboBox MyCmb)
	{
		string left = MyMode.ToUpper();
		if (Operators.CompareString(left, "I", TextCompare: false) != 0)
		{
			if (Operators.CompareString(left, "G", TextCompare: false) == 0)
			{
				switch (MyTab)
				{
				case 1:
					MyCmb = f_NodeStored1;
					break;
				case 2:
					MyCmb = f_NodeStored2;
					break;
				case 3:
					MyCmb = f_NodeStored3;
					break;
				case 4:
					MyCmb = f_NodeStored4;
					break;
				case 5:
					MyCmb = f_NodeStored5;
					break;
				case 6:
					MyCmb = f_NodeStored6;
					break;
				case 7:
					MyCmb = f_NodeStored7;
					break;
				case 8:
					MyCmb = f_NodeStored8;
					break;
				case 9:
					MyCmb = f_NodeStored9;
					break;
				}
			}
		}
		else
		{
			switch (MyTab)
			{
			case 1:
				f_NodeStored1 = MyCmb;
				break;
			case 2:
				f_NodeStored2 = MyCmb;
				break;
			case 3:
				f_NodeStored3 = MyCmb;
				break;
			case 4:
				f_NodeStored4 = MyCmb;
				break;
			case 5:
				f_NodeStored5 = MyCmb;
				break;
			case 6:
				f_NodeStored6 = MyCmb;
				break;
			case 7:
				f_NodeStored7 = MyCmb;
				break;
			case 8:
				f_NodeStored8 = MyCmb;
				break;
			case 9:
				f_NodeStored9 = MyCmb;
				break;
			}
		}
	}

	public void Process_Transform(string MyVal, string CmbName)
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
						errsource = "frminput - Process_Transform";
						int num3 = 0;
						string text = "";
						string text2 = "";
						int num4 = 0;
						int num5 = 0;
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						text = CmbName;
						MyVal = Strings.Trim(Strings.LCase(MyVal));
						if (!LikeOperator.LikeString(text, "_*_*", CompareMethod.Binary))
						{
							goto end_IL_0001;
						}
						num5 = Strings.InStrRev(text, "_");
						text5 = Strings.Trim(Strings.Mid(text, num5 + 1));
						text = Strings.Mid(text, 1, num5 - 1);
						text4 = Strings.Trim(Strings.Mid(text, 2));
						text4 = ((Operators.CompareString(text4, "1", TextCompare: false) != 0) ? ("-" + text4) : "");
						text3 = "option" + text5 + text4;
						text = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data(text3, "transformcount", "0", 10, f_InputFile)));
						if (!Versioned.IsNumeric(text))
						{
							goto end_IL_0001;
						}
						num3 = Convert.ToInt32(Math.Floor(new decimal(Conversions.ToInteger(text))));
						int num6 = num3;
						for (num4 = 1; num4 <= num6; num4++)
						{
							text2 = Strings.Trim(General_Procedures.Get_Ini_Data(text3, "transform" + Conversions.ToString(num4), "", 5000, f_InputFile));
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								continue;
							}
							string[] array = Strings.Split(text2, ":");
							if (Information.UBound(array) == 2)
							{
								text6 = Strings.Trim(Strings.LCase(array[0]));
								text7 = Strings.Trim(array[1]);
								text8 = Strings.Trim(array[2]);
								if (Operators.CompareString(MyVal, text6, TextCompare: false) == 0)
								{
									f_NoDisplayData = Strings.Replace(f_NoDisplayData, text7, text8, 1, -1, CompareMethod.Text);
								}
							}
							array = null;
						}
						goto end_IL_0001;
					}
					case 588:
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
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 588;
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

	public string Generate_Data(string MyValue, string MyHelp)
	{
		int try0001_dispatch = -1;
		string text = default(string);
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
					string MyCol;
					string myCol;
					string text6;
					string text7;
					string text4;
					string PostCol;
					string[] array2;
					switch (try0001_dispatch)
					{
					default:
					{
						text = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "frminput - Generate_Data";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text2 = "";
						string text3 = "";
						text4 = "";
						string text5 = "";
						MyCol = "";
						myCol = "";
						text6 = "=";
						text7 = "c";
						string[] array = Strings.Split(MyHelp, "^");
						if (Information.UBound(array) >= 3)
						{
							myCol = array[1];
							text6 = Strings.LCase(array[2]);
							text7 = Strings.LCase(array[3]);
						}
						array = null;
						array = null;
						if (LikeOperator.LikeString(Strings.LCase(text6), "in group(*", CompareMethod.Binary))
						{
							text6 = "in group(";
						}
						if (Operators.CompareString(text7, "g", TextCompare: false) != 0)
						{
							goto IL_0107;
						}
						text3 = "";
						if (ValidDate(text7, MyValue, text6, ref text3))
						{
							goto IL_0107;
						}
						text = "$ERROR$:An invalid date or date range was specified in one of the text entries: " + text3;
						goto end_IL_0001;
					}
					case 1870:
						{
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
						IL_0107:
						switch (text7)
						{
						case "c":
						case "q":
						case "x":
							text4 = "'";
							PostCol = "'";
							break;
						default:
							text4 = "";
							PostCol = "";
							break;
						}
						array2 = Strings.Split(MyValue, ";");
						array2[0] = Strings.Trim(General_Procedures.StripQuotesAndDates(array2[0], text7, 0));
						switch (text6)
						{
						case "between":
							if (Information.UBound(array2) >= 1)
							{
								array2[1] = Strings.Trim(General_Procedures.StripQuotesAndDates(array2[1], text7, 0));
								BuildForm.Assign_Pre_Post_Col(2, text7, array2[0], ref text4, ref PostCol);
								text = text + text4 + array2[0] + PostCol + " AND ";
								BuildForm.Assign_Pre_Post_Col(2, text7, array2[1], ref text4, ref PostCol);
								text = text + text4 + array2[1] + PostCol;
							}
							else
							{
								text = "";
							}
							break;
						case "like":
							if (Strings.InStr(array2[0], "%") == 0 && Strings.InStr(array2[0], "_") == 0)
							{
								array2[0] += "%";
							}
							text = text4 + array2[0] + PostCol + " ESCAPE '/'";
							break;
						case "in":
						case "like list":
						{
							string text5;
							if (Operators.CompareString(text6, "like list", TextCompare: false) == 0)
							{
								text5 = "";
							}
							else
							{
								text5 = "(";
							}
							text5 = "";
							short num7 = (short)Information.UBound(array2);
							for (short num5 = 0; num5 <= num7; num5 = (short)unchecked(num5 + 1))
							{
								if (Operators.CompareString(array2[num5], "", TextCompare: false) != 0)
								{
									BuildForm.Assign_Pre_Post_Col(1, text7, array2[num5], ref text4, ref PostCol);
									text5 = ((Operators.CompareString(text5, "(", TextCompare: false) != 0) ? (text5 + "," + text4 + General_Procedures.StripQuotesAndDates(array2[num5], text7, 0) + PostCol) : (text5 + text4 + General_Procedures.StripQuotesAndDates(array2[0], text7, 0) + PostCol));
								}
							}
							if (Operators.CompareString(text6, "like list", TextCompare: false) == 0)
							{
								text5 = Strings.Replace(text5, ";", "</comma\\>", 1, -1, CompareMethod.Text);
								text5 = BuildForm.Process_Like_List(myCol, text5, "/");
								text5 = Strings.Replace(text5, "\r\n", " ", 1, -1, CompareMethod.Text);
							}
							else
							{
								text5 += ")";
							}
							text5 = Strings.Trim(text5);
							if (Operators.CompareString(text6, "like list", TextCompare: false) == 0)
							{
								if (Operators.CompareString(Strings.Mid(text5, Strings.Len(text5), 1), ")", TextCompare: false) == 0)
								{
									text5 = Strings.Mid(text5, 1, Strings.Len(text5) - 1);
								}
							}
							else
							{
								if (Operators.CompareString(Strings.Mid(text5, 1, 1), "(", TextCompare: false) == 0)
								{
									text5 = Strings.Trim(Strings.Mid(text5, 2));
								}
								if (Operators.CompareString(Strings.Mid(text5, 1, 1), ",", TextCompare: false) == 0)
								{
									text5 = Strings.Mid(text5, 2);
								}
								if (Operators.CompareString(Strings.Mid(text5, Strings.Len(text5), 1), ")", TextCompare: false) == 0)
								{
									text5 = Strings.Mid(text5, 1, Strings.Len(text5) - 1);
								}
							}
							text = Strings.Trim(text5);
							break;
						}
						case "in group":
						case "like group":
						case "in group (":
						{
							string text5 = Strings.Trim(array2[0]);
							if (Operators.CompareString(text5, "", TextCompare: false) != 0)
							{
								text5 = ((Information.UBound(array2) < 1) ? (text5 + ":1") : (text5 + ":" + array2[1]));
								BuildForm.Extract_IG_ColNo(ref text5, ref MyCol);
							}
							if (Operators.CompareString(text6, "in group(", TextCompare: false) == 0)
							{
								text = "\"\"" + text5 + "\"\",\"\"" + MyCol + "\"\"";
							}
							else if (Operators.CompareString(text6, "like group", TextCompare: false) == 0)
							{
								string text2 = "@esc@=/";
								text = "\"\"" + text5 + "\"\",\"\"" + MyCol + text2 + "\"\"";
							}
							else
							{
								text = "\"\"" + text5 + "\"\",\"\"" + MyCol + "\"\"";
							}
							break;
						}
						default:
							BuildForm.Assign_Pre_Post_Col(2, text7, array2[0], ref text4, ref PostCol);
							text = text4 + array2[0] + PostCol;
							break;
						}
						array2 = null;
						array2 = null;
						goto end_IL_0001;
					}
					goto IL_0784;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1870;
				continue;
			}
			break;
			IL_0784:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public string StripOuterQuotes(string MyData)
	{
		MyData = Strings.Trim(MyData);
		if (Operators.CompareString(MyData, "'", TextCompare: false) == 0 || Operators.CompareString(MyData, "''", TextCompare: false) == 0 || Operators.CompareString(MyData, "", TextCompare: false) == 0)
		{
			MyData = "";
		}
		else
		{
			if (Operators.CompareString(Strings.Mid(MyData, Strings.Len(MyData), 1), "'", TextCompare: false) == 0)
			{
				MyData = Strings.Mid(MyData, 1, checked(Strings.Len(MyData) - 1));
			}
			if (Operators.CompareString(Strings.Mid(MyData, 1, 1), "'", TextCompare: false) == 0)
			{
				MyData = Strings.Mid(MyData, 2);
			}
		}
		return MyData;
	}

	public string Init_Options()
	{
		string text = Conversions.ToString(1);
		int i = 0;
		int num = 0;
		int num2 = 0;
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "N";
		string text8 = "Y";
		string text9 = "N";
		string text10 = "N";
		string text11 = "N";
		string text12 = "N";
		string text13 = "N";
		string text14 = "N";
		string text15 = "N";
		string text16 = "N";
		int num3 = 0;
		int num4 = 0;
		string text17 = "N";
		string text18 = "N";
		string text19 = "N";
		string text20 = "Y";
		string text21 = "";
		string text22 = "combobox";
		string text23 = "";
		object obj = null;
		checked
		{
			int num5 = (int)Math.Floor((double)TabControl1.Height / (double)f_NoCtlPerTab);
			int num6 = (int)Math.Round(Math.Floor((double)num5 / 2.0) + 10.0);
			int num7 = 1;
			string[] Hdr = new string[0];
			string[] Val = new string[0];
			int num8 = 1;
			text = Conversions.ToString(num8);
			if (Operators.CompareString(F_Mode, "1", TextCompare: false) == 0 && Operators.CompareString(F_OutputFile, "", TextCompare: false) != 0)
			{
				text3 = BuildSQL.Load_Input_Arrays(F_OutputFile, ref Hdr, ref Val);
				if (Versioned.IsNumeric(text3))
				{
					num8 = Conversions.ToInteger(text3);
					text = Conversions.ToString(num8);
				}
			}
			text3 = General_Procedures.Get_Ini_Data("general", "title", "N/A", 5000, f_InputFile);
			if (Operators.CompareString(Strings.LCase(text3), "n/a", TextCompare: false) != 0)
			{
				LblTitle.Text = text3;
			}
			text3 = General_Procedures.Get_Ini_Data("general", "form-title", "N/A", 5000, f_InputFile);
			if (Operators.CompareString(Strings.LCase(text3), "n/a", TextCompare: false) != 0)
			{
				Text = text3;
			}
			text3 = General_Procedures.Get_Ini_Data("general", "no-tabs", "1", 10, f_InputFile);
			if (Versioned.IsNumeric(text3))
			{
				f_NoTabs = Convert.ToInt32(Math.Floor(new decimal(Conversions.ToInteger(text3))));
			}
			text3 = General_Procedures.Get_Ini_Data("general", "midas-driver", Globals_Renamed.gMidasDriver, 100, f_InputFile);
			f_MidasDriver = Globals_Renamed.gMidasDriver;
			Globals_Renamed.gMidasDriver = text3;
			text3 = General_Procedures.Get_Ini_Data("general", "ignore-condition", Globals_Renamed.gMidasDriver, 500, f_InputFile);
			f_IgnoreCond = text3;
			f_NoDisplayHdrs = "";
			f_NoDisplayData = "";
			int num9 = f_NoTabs;
			for (num = 1; num <= num9; num++)
			{
				text21 = ((num != 1) ? ("-" + Conversions.ToString(num)) : "");
				TabPage tabPage = new TabPage
				{
					Name = "tabpage" + Conversions.ToString(i),
					AutoScroll = true
				};
				text3 = General_Procedures.Get_Ini_Data("general", "tab-title-" + Conversions.ToString(num), "N/A", 5000, f_InputFile);
				if (Operators.CompareString(text3, "N/A", TextCompare: false) == 0)
				{
					tabPage.Text = "Inputs (" + Conversions.ToString(num) + ")";
				}
				else
				{
					tabPage.Text = text3;
				}
				tabPage.BackColor = Color.White;
				TabControl1.Controls.Add(tabPage);
				i = 0;
				do
				{
					text2 = General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "variable", "N/A", 5000, f_InputFile);
					if (Operators.CompareString(text2, "N/A", TextCompare: false) != 0)
					{
						text4 = Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "default", "N/A", 5000, f_InputFile));
						text5 = Strings.LCase(text4);
						text7 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "required", "N", 10, f_InputFile)));
						text8 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "writetofile", "Y", 10, f_InputFile)));
						text9 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "stripouterquotes", "N", 10, f_InputFile)));
						text10 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "preservedoublequotes", "N", 10, f_InputFile)));
						text11 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "stripextension", "N", 10, f_InputFile)));
						text13 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "reverseslash", "N", 10, f_InputFile)));
						text12 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "masked", "N", 10, f_InputFile)));
						text14 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "substitutemissing", "N", 10, f_InputFile)));
						text18 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "translatenode", "N", 10, f_InputFile)));
						text19 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "translatenodeappend", "N", 10, f_InputFile)));
						text20 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "fileopen", "Y", 10, f_InputFile)));
						text17 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "transformcount", "N", 10, f_InputFile)));
						text16 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "adddisplaymember", "N", 10, f_InputFile)));
						switch (text17)
						{
						default:
							if (Versioned.IsNumeric(text17))
							{
								text17 = "Y";
								break;
							}
							goto case "N";
						case "N":
						case "0":
						case null:
						case "":
							text17 = "N";
							break;
						}
						text6 = Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "values", "N/A", 5000, f_InputFile));
						if (LikeOperator.LikeString(text6.ToUpper(), "DATA^*^*^*^*", CompareMethod.Binary))
						{
							text15 = "Y";
						}
						if (LikeOperator.LikeString(text6.ToLower(), "@*file@*", CompareMethod.Binary))
						{
							text3 = General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "initvalues", "", 5000, f_InputFile);
							if (Operators.CompareString(text3, "", TextCompare: false) != 0)
							{
								text6 += text3;
							}
						}
						text22 = Strings.UCase(Strings.Trim(General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "controltype", "COMBOBOX", 100, f_InputFile)));
						text3 = General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "label", "N/A", 5000, f_InputFile);
						Label label = new Label
						{
							Name = "lbloption_" + Conversions.ToString(num) + "_" + Conversions.ToString(i),
							Visible = true,
							Left = 6,
							Anchor = (AnchorStyles.Top | AnchorStyles.Left),
							Top = tabPage.Top + (i + 1) * num5,
							AutoSize = true
						};
						if (Operators.CompareString(Strings.UCase(text3), "N/A", TextCompare: false) != 0)
						{
							label.Text = text3;
						}
						else
						{
							label.Text = text2;
						}
						if (i == 1)
						{
							label.Top = tabPage.Top + num6;
						}
						else
						{
							label.Top = tabPage.Top + num6 + (i - 1) * num5;
						}
						if (Operators.CompareString(text7, "Y", TextCompare: false) == 0)
						{
							label.ForeColor = Color.DarkRed;
						}
						else if (Operators.CompareString(text7, "C", TextCompare: false) == 0)
						{
							label.Font = new Font(label.Font, FontStyle.Underline);
						}
						tabPage.Controls.Add(label);
						if (Operators.CompareString(text12, "N", TextCompare: false) == 0)
						{
							obj = new ComboBox();
							if (Operators.CompareString(text5, "n/a", TextCompare: false) != 0)
							{
								if (LikeOperator.LikeString(Strings.LCase(text5), "*@username@*", CompareMethod.Binary))
								{
									text4 = Strings.Replace(text5, "@username@", Globals_Renamed.gWinuser, 1, -1, CompareMethod.Text);
									object instance = NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null);
									object[] obj2 = new object[1] { text4 };
									object[] array = obj2;
									bool[] obj3 = new bool[1] { true };
									bool[] array2 = obj3;
									NewLateBinding.LateCall(instance, null, "Add", obj2, null, null, obj3, IgnoreReturn: true);
									if (array2[0])
									{
										text4 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
									}
								}
								else if (LikeOperator.LikeString(Strings.LCase(text5), "*@dom\\username@*", CompareMethod.Binary))
								{
									text4 = Strings.Replace(text5, "@dom\\username@", Globals_Renamed.guDomain + "\\" + Globals_Renamed.gWinuser, 1, -1, CompareMethod.Text);
									object instance2 = NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null);
									object[] obj4 = new object[1] { text4 };
									object[] array = obj4;
									bool[] obj5 = new bool[1] { true };
									bool[] array2 = obj5;
									NewLateBinding.LateCall(instance2, null, "Add", obj4, null, null, obj5, IgnoreReturn: true);
									if (array2[0])
									{
										text4 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
									}
								}
								else
								{
									switch (text5)
									{
									case "@defaultdir@":
									{
										text4 = Globals_Renamed.MyPCDir;
										if (Operators.CompareString(Strings.Right(text4, 1), "\\", TextCompare: false) == 0)
										{
											text4 = Strings.Mid(text4, 1, Strings.Len(text4) - 1);
										}
										object[] array;
										bool[] array2;
										NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Add", array = new object[1] { text4 }, null, null, array2 = new bool[1] { true }, IgnoreReturn: true);
										if (array2[0])
										{
											text4 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
										}
										break;
									}
									case "@marsnode@":
									{
										text4 = Globals_Renamed.MyMARSServer;
										int myTab6 = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab6, ref MyCmb);
										obj = MyCmb;
										break;
									}
									case "@ariesnode@":
									{
										text4 = Globals_Renamed.MyARIESServer;
										int myTab5 = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab5, ref MyCmb);
										obj = MyCmb;
										break;
									}
									case "@oasysnode@":
									{
										text4 = Globals_Renamed.MyOASysServer;
										int myTab4 = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab4, ref MyCmb);
										obj = MyCmb;
										break;
									}
									case "@othernode@":
									{
										text4 = Globals_Renamed.MyOtherServer;
										int myTab3 = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab3, ref MyCmb);
										obj = MyCmb;
										break;
									}
									case "@midasnode@":
									{
										text4 = "MIDAS";
										int myTab2 = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab2, ref MyCmb);
										obj = MyCmb;
										break;
									}
									case "@csvinitheaders@":
									{
										int myTab = num;
										ComboBox MyCmb = (ComboBox)obj;
										Assign_Node("I", myTab, ref MyCmb);
										obj = MyCmb;
										text4 = "";
										break;
									}
									}
								}
								NewLateBinding.LateSet(obj, null, "Text", new object[1] { text4 }, null, null);
							}
							if (Operators.CompareString(Strings.Mid(text6 + " ", 1, 1), "~", TextCompare: false) == 0)
							{
								if (LikeOperator.LikeString(text6.ToLower(), "*@username@*", CompareMethod.Binary))
								{
									text6 = Strings.Replace(text6, "@username@", Globals_Renamed.gWinuser, 1, -1, CompareMethod.Text);
								}
								if (text6.Length > 1)
								{
									string[] array3 = Strings.Split(Strings.Mid(text6, 2), "/");
									int num10 = Information.UBound(array3);
									for (num2 = 0; num2 <= num10; num2++)
									{
										NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Add", new object[1] { Strings.Trim(array3[num2]) }, null, null, null, IgnoreReturn: true);
									}
									array3 = null;
								}
							}
							else if (Operators.CompareString(Strings.LCase(text6), "@othernode@", TextCompare: false) == 0)
							{
								int myTab7 = num;
								ComboBox MyCmb = (ComboBox)obj;
								Assign_Node("I", myTab7, ref MyCmb);
								obj = MyCmb;
							}
							else if (Operators.CompareString(Strings.LCase(text6), "@folder@", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(text5), "@defaultdir@", TextCompare: false) != 0 && Operators.CompareString(Strings.Trim(text5), "", TextCompare: false) != 0)
							{
								NewLateBinding.LateCall(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Add", new object[1] { "" }, null, null, null, IgnoreReturn: true);
								object instance3 = NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null);
								object[] obj6 = new object[1] { text5 };
								object[] array = obj6;
								bool[] obj7 = new bool[1] { true };
								bool[] array2 = obj7;
								NewLateBinding.LateCall(instance3, null, "Add", obj6, null, null, obj7, IgnoreReturn: true);
								if (array2[0])
								{
									text5 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
								}
							}
							if (Operators.CompareString(text22, "DROPDOWNLIST", TextCompare: false) == 0)
							{
								NewLateBinding.LateSet(obj, null, "DropDownStyle", new object[1] { ComboBoxStyle.DropDownList }, null, null);
								if (Operators.ConditionalCompareObjectGreaterEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Items", new object[0], null, null, null), null, "Count", new object[0], null, null, null), 1, TextCompare: false))
								{
									NewLateBinding.LateSet(obj, null, "SelectedIndex", new object[1] { 0 }, null, null);
								}
							}
						}
						else
						{
							obj = new TextBox();
							NewLateBinding.LateSet(obj, null, "PasswordChar", new object[1] { "*" }, null, null);
						}
						NewLateBinding.LateSet(obj, null, "Name", new object[1] { "ComboBox_" + Conversions.ToString(num) + "_" + Conversions.ToString(i) }, null, null);
						NewLateBinding.LateSet(obj, null, "Left", new object[1] { 143 }, null, null);
						if (i < f_NoCtlPerTab - 1)
						{
							NewLateBinding.LateSet(obj, null, "Width", new object[1] { Operators.SubtractObject(Operators.SubtractObject(cmdOK.Left, NewLateBinding.LateGet(obj, null, "left", new object[0], null, null, null)), 5) }, null, null);
						}
						else
						{
							NewLateBinding.LateSet(obj, null, "Width", new object[1] { Operators.SubtractObject(Operators.SubtractObject(cmdOK.Left, NewLateBinding.LateGet(obj, null, "left", new object[0], null, null, null)), 20) }, null, null);
						}
						NewLateBinding.LateSet(obj, null, "Anchor", new object[1] { 13 }, null, null);
						NewLateBinding.LateSet(obj, null, "Top", new object[1] { label.Top }, null, null);
						NewLateBinding.LateSet(obj, null, "Tag", new object[1] { text7 + text8 + text9 + text13 + text14 + text15 + text17 + text11 + text18 + text20 + text19 + text12 + text10 + text16 + text2 }, null, null);
						tabPage.Controls.Add((Control)obj);
						obj = null;
						text3 = General_Procedures.Get_Ini_Data("option" + Conversions.ToString(i) + text21, "help", "N/A", 5000, f_InputFile);
						if (Operators.CompareString(text3, "N/A", TextCompare: false) != 0)
						{
							Button MyButton = new Button();
							MyButton.Tag = text3;
							MyButton.Text = "";
							if (i < f_NoCtlPerTab - 1)
							{
								MyButton.Left = (int)Math.Round((double)cmdOK.Left + (double)cmdOK.Width / 2.0);
							}
							else
							{
								MyButton.Left = (int)Math.Round((double)cmdOK.Left + (double)cmdOK.Width / 2.0 - 15.0);
							}
							MyButton.Width = 34;
							MyButton.Height = 21;
							MyButton.Top = label.Top;
							MyButton.Name = "cmdhelp_" + Conversions.ToString(num) + "_" + Conversions.ToString(i);
							ToolTip1.SetToolTip(MyButton, "Help");
							BuildForm.Set_Btn_Img(ref MyButton, "helpb");
							MyButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
							MyButton.Click += cmdHelp0_Click;
							tabPage.Controls.Add(MyButton);
							MyButton = null;
						}
						if (Operators.CompareString(Strings.LCase(text6), "n/a", TextCompare: false) != 0 && Operators.CompareString(text6, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text6 + " ", 1, 1), "~", TextCompare: false) != 0)
						{
							Button MyButton2 = new Button();
							MyButton2.Tag = text6;
							MyButton2.Text = "";
							if (i < f_NoCtlPerTab - 1)
							{
								MyButton2.Left = (int)Math.Round((double)cmdOK.Left + (double)cmdOK.Width / 2.0 + 42.0);
							}
							else
							{
								MyButton2.Left = (int)Math.Round((double)cmdOK.Left + (double)cmdOK.Width / 2.0 + 27.0);
							}
							MyButton2.Width = 34;
							MyButton2.Height = 21;
							MyButton2.Top = label.Top;
							MyButton2.Name = "cmdval_" + Conversions.ToString(num) + "_" + Conversions.ToString(i);
							if (Operators.CompareString(Strings.LCase(text6), "@editor@", TextCompare: false) == 0)
							{
								ToolTip1.SetToolTip(MyButton2, "Editor");
								BuildForm.Set_Btn_Img(ref MyButton2, "edit");
							}
							else
							{
								ToolTip1.SetToolTip(MyButton2, "Valid Values");
								BuildForm.Set_Btn_Img(ref MyButton2, "search");
							}
							MyButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
							MyButton2.Click += cmdval0_Click;
							tabPage.Controls.Add(MyButton2);
							MyButton2 = null;
						}
						label = null;
					}
					i++;
				}
				while (!((Operators.CompareString(text2, "N/A", TextCompare: false) == 0) | (i == f_NoCtlPerTab)));
				num4 = 0;
				int num11 = tabPage.Controls.Count - 1;
				for (i = 0; i <= num11; i++)
				{
					text3 = tabPage.Controls[i].Name.ToLower();
					if (LikeOperator.LikeString(text3, "combobox*", CompareMethod.Binary))
					{
						num3 = Strings.InStrRev(text3, "_");
						text3 = Strings.Mid(text3, num3 + 1);
						tabPage.Controls[i].TabStop = true;
						tabPage.Controls[i].TabIndex = Conversions.ToInteger(text3) + 1;
						num4++;
					}
				}
				num4++;
				int num12 = tabPage.Controls.Count - 1;
				for (i = 0; i <= num12; i++)
				{
					text3 = tabPage.Controls[i].Name.ToLower();
					if (LikeOperator.LikeString(text3, "lbl*", CompareMethod.Binary))
					{
						tabPage.Controls[i].TabStop = false;
					}
					else if (LikeOperator.LikeString(text3, "cmd*", CompareMethod.Binary))
					{
						num3 = Strings.InStrRev(text3, "_");
						text3 = Strings.Mid(text3, num3 + 1);
						if (LikeOperator.LikeString(tabPage.Controls[i].Name.ToLower(), "cmdhelp*", CompareMethod.Binary))
						{
							tabPage.Controls[i].TabIndex = num4 + Conversions.ToInteger(text3);
						}
						else
						{
							tabPage.Controls[i].TabIndex = num4 + 1 + Conversions.ToInteger(text3);
						}
						tabPage.Controls[i].TabStop = true;
					}
				}
				num2 = 0;
				if (Operators.CompareString(F_Mode, "1", TextCompare: false) == 0 && Operators.CompareString(F_OutputFile, "", TextCompare: false) != 0 && num == num8)
				{
					int num13 = 0;
					int num14 = tabPage.Controls.Count - 1;
					for (i = 0; i <= num14; i++)
					{
						text3 = tabPage.Controls[i].Name.ToLower();
						if (Operators.CompareString(text3, "combobox_" + Conversions.ToString(num) + "_" + Conversions.ToString(num2), TextCompare: false) != 0)
						{
							continue;
						}
						text2 = Strings.Mid(Conversions.ToString(tabPage.Controls[i].Tag), 15);
						int num15 = Information.UBound(Hdr);
						for (num13 = 0; num13 <= num15; num13++)
						{
							if (Operators.CompareString(Strings.Trim(Hdr[num13]), text2, TextCompare: false) == 0)
							{
								tabPage.Controls[i].Text = Strings.Trim(Val[num13]);
								break;
							}
						}
						num2++;
					}
					Hdr = null;
					Val = null;
				}
				tabPage = null;
			}
			return text;
		}
	}

	private void cmdHelp0_Click(object sender, EventArgs e)
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
				case 129:
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
					if (!Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), "", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Interaction.MsgBox(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null)), MsgBoxStyle.OkOnly, "Column Help");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 129;
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

	private void cmdval0_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object instance = default(object);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		TabPage tabPage = default(TabPage);
		int num6 = default(int);
		ComboBox MyCmb = default(ComboBox);
		string left = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		int num7 = default(int);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string myOtherServer = default(string);
		int num8 = default(int);
		int num9 = default(int);
		FrmNodeSel frmNodeSel = default(FrmNodeSel);
		frmgetdata frmgetdata3 = default(frmgetdata);
		FrmDTREdit frmDTREdit = default(FrmDTREdit);
		string[] lCSVCols = default(string[]);
		short num10 = default(short);
		int num11 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text3;
					frmgetdata frmgetdata2;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 4122:
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
								goto IL_0028;
							case 4:
								goto IL_0046;
							case 5:
								goto IL_0065;
							case 6:
								goto IL_0076;
							case 7:
								goto IL_007b;
							case 8:
								goto IL_0084;
							case 9:
								goto IL_008d;
							case 10:
								goto IL_0097;
							case 11:
								goto IL_00aa;
							case 12:
								goto IL_00c5;
							case 13:
								goto IL_00cb;
							case 14:
								goto IL_00d5;
							case 15:
								goto IL_00df;
							case 16:
								goto IL_00e5;
							case 17:
								goto IL_00eb;
							case 18:
								goto IL_00f5;
							case 19:
								goto IL_00ff;
							case 20:
								goto IL_0109;
							case 21:
								goto IL_0113;
							case 22:
								goto IL_0120;
							case 23:
								goto IL_0138;
							case 24:
								goto IL_016e;
							case 26:
							case 27:
								goto IL_0185;
							case 25:
							case 28:
								goto IL_0194;
							case 29:
								goto IL_01b4;
							case 30:
								goto IL_01be;
							case 31:
								goto IL_01d6;
							case 32:
								goto IL_0216;
							case 34:
							case 35:
								goto IL_0232;
							case 33:
							case 36:
								goto IL_0241;
							case 37:
								goto IL_025a;
							case 40:
								goto IL_0278;
							case 41:
								goto IL_028d;
							case 44:
								goto IL_029a;
							case 45:
								goto IL_02b4;
							case 46:
								goto IL_02ba;
							case 48:
								goto IL_02c7;
							case 39:
							case 42:
							case 43:
							case 47:
							case 49:
							case 50:
								goto IL_02d2;
							case 51:
								goto IL_02ec;
							case 52:
								goto IL_02f6;
							case 53:
								goto IL_0308;
							case 54:
								goto IL_0319;
							case 56:
								goto IL_0325;
							case 57:
								goto IL_033f;
							case 58:
								goto IL_0355;
							case 59:
								goto IL_036a;
							case 60:
								goto IL_0388;
							case 55:
							case 61:
							case 62:
								goto IL_0392;
							case 63:
								goto IL_03ab;
							case 64:
								goto IL_03b6;
							case 66:
								goto IL_0539;
							case 67:
								goto IL_0556;
							case 68:
								goto IL_0580;
							case 70:
								goto IL_05b7;
							case 69:
							case 71:
							case 72:
								goto IL_05d5;
							case 73:
								goto IL_05f5;
							case 78:
								goto IL_0635;
							case 79:
								goto IL_063c;
							case 80:
								goto IL_0658;
							case 81:
								goto IL_067d;
							case 82:
								goto IL_06a5;
							case 83:
								goto IL_06be;
							case 84:
								goto IL_06d7;
							case 86:
								goto IL_06f4;
							case 87:
								goto IL_070d;
							case 85:
							case 88:
							case 89:
								goto IL_0727;
							case 90:
								goto IL_0754;
							case 91:
								goto IL_075b;
							case 92:
								goto IL_076e;
							case 93:
								goto IL_077c;
							case 94:
								goto IL_07a7;
							case 95:
							case 96:
							case 97:
								goto IL_07bb;
							case 98:
								goto IL_07f4;
							case 99:
								goto IL_0801;
							case 103:
								goto IL_0829;
							case 104:
								goto IL_0834;
							case 105:
								goto IL_083a;
							case 106:
								goto IL_0850;
							case 107:
								goto IL_087f;
							case 108:
								goto IL_088d;
							case 109:
								goto IL_089f;
							case 110:
								goto IL_08ea;
							case 113:
								goto IL_0901;
							case 114:
								goto IL_0921;
							case 115:
								goto IL_092b;
							case 116:
								goto IL_093d;
							case 117:
								goto IL_0952;
							case 118:
								goto IL_0963;
							case 119:
								goto IL_0972;
							case 120:
								goto IL_0983;
							case 121:
								goto IL_0994;
							case 122:
								goto IL_099f;
							case 123:
								goto IL_09aa;
							case 124:
								goto IL_09b5;
							case 125:
								goto IL_09c6;
							case 126:
								goto IL_09d1;
							case 127:
								goto IL_09ea;
							case 130:
								goto IL_0a22;
							case 131:
								goto IL_0a33;
							case 133:
								goto IL_0a44;
							case 134:
								goto IL_0a51;
							case 135:
								goto IL_0a77;
							case 137:
							case 138:
								goto IL_0a96;
							case 139:
								goto IL_0aa3;
							case 132:
							case 140:
							case 141:
								goto IL_0ab6;
							case 142:
								goto IL_0aff;
							case 143:
								goto IL_0b0c;
							case 144:
								goto IL_0b1a;
							case 145:
								goto IL_0b28;
							case 146:
								goto IL_0b44;
							case 147:
								goto IL_0b51;
							case 149:
								goto IL_0b75;
							case 151:
								goto IL_0b9f;
							case 152:
								goto IL_0bbb;
							case 153:
								goto IL_0bdb;
							case 155:
								goto IL_0bf3;
							case 156:
								goto IL_0c13;
							case 158:
								goto IL_0c28;
							case 159:
								goto IL_0c48;
							case 161:
								goto IL_0c5d;
							case 162:
								goto IL_0c7d;
							case 163:
								goto IL_0c8f;
							case 154:
							case 157:
							case 160:
							case 164:
							case 165:
								goto IL_0c9e;
							case 166:
								goto IL_0ccb;
							case 167:
								goto IL_0cf3;
							case 168:
								goto IL_0d01;
							case 169:
								goto IL_0d10;
							case 170:
								goto IL_0d1e;
							case 65:
							case 74:
							case 76:
							case 77:
							case 100:
							case 101:
							case 102:
							case 111:
							case 112:
							case 128:
							case 129:
							case 150:
							case 171:
							case 172:
							case 173:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 38:
							case 75:
							case 136:
							case 148:
							case 174:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_05d5:
						num2 = 72;
						if (MyProject.Forms.FrmMain.FolderBrowserDialog1.ShowDialog() != DialogResult.OK)
						{
							goto end_IL_0001_3;
						}
						goto IL_05f5;
						IL_05f5:
						num2 = 73;
						NewLateBinding.LateSet(instance, null, "Text", new object[1] { MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath }, null, null);
						break;
						IL_05b7:
						num2 = 70;
						MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = Globals_Renamed.MyPCDir;
						goto IL_05d5;
						IL_0185:
						num2 = 27;
						num5++;
						goto IL_018e;
						IL_000b:
						num2 = 2;
						text = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
						goto IL_0028;
						IL_0028:
						num2 = 3;
						text2 = Conversions.ToString(NewLateBinding.LateGet(sender, null, "Name", new object[0], null, null, null));
						goto IL_0046;
						IL_0046:
						num2 = 4;
						tabPage = TabControl1.TabPages[TabControl1.SelectedIndex];
						goto IL_0065;
						IL_0065:
						num2 = 5;
						num6 = TabControl1.SelectedIndex + 1;
						goto IL_0076;
						IL_0076:
						num2 = 6;
						MyCmb = null;
						goto IL_007b;
						IL_007b:
						num2 = 7;
						left = "Y";
						goto IL_0084;
						IL_0084:
						num2 = 8;
						text3 = "O";
						goto IL_008d;
						IL_008d:
						num2 = 9;
						text4 = "";
						goto IL_0097;
						IL_0097:
						num2 = 10;
						Assign_Node("G", num6, ref MyCmb);
						goto IL_00aa;
						IL_00aa:
						num2 = 11;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_00c5;
						IL_00c5:
						num2 = 12;
						instance = null;
						goto IL_00cb;
						IL_00cb:
						num2 = 13;
						text5 = "";
						goto IL_00d5;
						IL_00d5:
						num2 = 14;
						text6 = "";
						goto IL_00df;
						IL_00df:
						num2 = 15;
						num7 = 0;
						goto IL_00e5;
						IL_00e5:
						num2 = 16;
						num5 = 0;
						goto IL_00eb;
						IL_00eb:
						num2 = 17;
						text7 = "";
						goto IL_00f5;
						IL_00f5:
						num2 = 18;
						text8 = "";
						goto IL_00ff;
						IL_00ff:
						num2 = 19;
						text9 = "";
						goto IL_0109;
						IL_0109:
						num2 = 20;
						myOtherServer = "";
						goto IL_0113;
						IL_0113:
						num2 = 21;
						text7 = Strings.Mid(text2, 7);
						goto IL_0120;
						IL_0120:
						num2 = 22;
						num8 = tabPage.Controls.Count - 1;
						num5 = 0;
						goto IL_018e;
						IL_018e:
						if (num5 <= num8)
						{
							goto IL_0138;
						}
						goto IL_0194;
						IL_0138:
						num2 = 23;
						if (Operators.CompareString(Strings.LCase(tabPage.Controls[num5].Name), "combobox" + text7, TextCompare: false) == 0)
						{
							goto IL_016e;
						}
						goto IL_0185;
						IL_016e:
						num2 = 24;
						instance = tabPage.Controls[num5];
						goto IL_0194;
						IL_0194:
						num2 = 28;
						if (Strings.InStr(Strings.LCase(text), "<<<spf-sqlite-table>>>") != 0)
						{
							goto IL_01b4;
						}
						goto IL_029a;
						IL_01b4:
						num2 = 29;
						text4 = "";
						goto IL_01be;
						IL_01be:
						num2 = 30;
						num9 = tabPage.Controls.Count - 1;
						num5 = 0;
						goto IL_023b;
						IL_023b:
						if (num5 <= num9)
						{
							goto IL_01d6;
						}
						goto IL_0241;
						IL_01d6:
						num2 = 31;
						if (Operators.CompareString(Strings.LCase(tabPage.Controls[num5].Name), "combobox_" + Conversions.ToString(num6) + "_0", TextCompare: false) == 0)
						{
							goto IL_0216;
						}
						goto IL_0232;
						IL_0216:
						num2 = 32;
						text4 = tabPage.Controls[num5].Text;
						goto IL_0241;
						IL_0241:
						num2 = 36;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto IL_025a;
						}
						goto IL_0278;
						IL_025a:
						num2 = 37;
						Interaction.MsgBox("You have not yet assigned a SQLite database", MsgBoxStyle.Exclamation, "Missing SQLite Database");
						goto end_IL_0001_3;
						IL_0278:
						num2 = 40;
						text2 = Strings.Replace(text, "<<<spf-sqlite-table>>>", text4, 1, -1, CompareMethod.Text);
						goto IL_028d;
						IL_028d:
						num2 = 41;
						text = "@help@";
						goto IL_02d2;
						IL_0232:
						num2 = 35;
						num5++;
						goto IL_023b;
						IL_029a:
						num2 = 44;
						if (LikeOperator.LikeString(Strings.UCase(text), "DATA^*", CompareMethod.Binary))
						{
							goto IL_02b4;
						}
						goto IL_02c7;
						IL_02b4:
						num2 = 45;
						text2 = text;
						goto IL_02ba;
						IL_02ba:
						num2 = 46;
						text = "@help@";
						goto IL_02d2;
						IL_02c7:
						num2 = 48;
						text2 = "";
						goto IL_02d2;
						IL_02d2:
						num2 = 50;
						if (LikeOperator.LikeString(Strings.LCase(text), "@file@*", CompareMethod.Binary))
						{
							goto IL_02ec;
						}
						goto IL_0325;
						IL_02ec:
						num2 = 51;
						text8 = "";
						goto IL_02f6;
						IL_02f6:
						num2 = 52;
						if (Strings.Len(text) > 6)
						{
							goto IL_0308;
						}
						goto IL_0319;
						IL_0308:
						num2 = 53;
						text9 = Strings.Trim(Strings.Mid(text, 7));
						goto IL_0319;
						IL_0319:
						num2 = 54;
						text = "@file@";
						goto IL_0392;
						IL_0325:
						num2 = 56;
						if (LikeOperator.LikeString(Strings.LCase(text), "@*file@*", CompareMethod.Binary))
						{
							goto IL_033f;
						}
						goto IL_0392;
						IL_033f:
						num2 = 57;
						num7 = Strings.InStr(Strings.LCase(text), "file@");
						goto IL_0355;
						IL_0355:
						num2 = 58;
						text8 = Strings.Trim(Strings.Mid(text, 2, num7 - 2));
						goto IL_036a;
						IL_036a:
						num2 = 59;
						text9 = Strings.Trim(Strings.Mid(text + " ", num7 + 5));
						goto IL_0388;
						IL_0388:
						num2 = 60;
						text = "@file@";
						goto IL_0392;
						IL_0392:
						num2 = 62;
						if (Operators.CompareString(text9, "", TextCompare: false) == 0)
						{
							goto IL_03ab;
						}
						goto IL_03b6;
						IL_03ab:
						num2 = 63;
						text9 = Globals_Renamed.MyPCDir;
						goto IL_03b6;
						IL_03b6:
						num2 = 64;
						switch (Strings.LCase(text))
						{
						case "@folder@":
							break;
						case "@file@":
							goto IL_0635;
						case "@csvinitheaders@":
							goto IL_0829;
						case "@editor@":
							goto IL_0901;
						case "@help@":
							goto IL_0a22;
						case "@ariesnode@":
						case "@oasysnode@":
						case "@marsnode@":
						case "@othernode@":
							goto IL_0b9f;
						default:
							goto end_IL_0001_2;
						}
						goto IL_0539;
						IL_0b9f:
						num2 = 151;
						frmNodeSel = new FrmNodeSel
						{
							f_OutNode = text5,
							f_Mode = 0
						};
						goto IL_0bbb;
						IL_0bbb:
						num2 = 152;
						if (Operators.CompareString(Strings.LCase(text), "@ariesnode@", TextCompare: false) == 0)
						{
							goto IL_0bdb;
						}
						goto IL_0bf3;
						IL_0bdb:
						num2 = 153;
						frmNodeSel.f_DBType = "3";
						goto IL_0c9e;
						IL_0bf3:
						num2 = 155;
						if (Operators.CompareString(Strings.LCase(text), "@marsnode@", TextCompare: false) == 0)
						{
							goto IL_0c13;
						}
						goto IL_0c28;
						IL_0c13:
						num2 = 156;
						frmNodeSel.f_DBType = "0";
						goto IL_0c9e;
						IL_0c28:
						num2 = 158;
						if (Operators.CompareString(Strings.LCase(text), "@oasysnode@", TextCompare: false) == 0)
						{
							goto IL_0c48;
						}
						goto IL_0c5d;
						IL_0c48:
						num2 = 159;
						frmNodeSel.f_DBType = "2";
						goto IL_0c9e;
						IL_0c5d:
						num2 = 161;
						if (Operators.CompareString(Strings.LCase(text), "@othernode@", TextCompare: false) == 0)
						{
							goto IL_0c7d;
						}
						goto IL_0c9e;
						IL_0c7d:
						num2 = 162;
						frmNodeSel.f_DBType = "5";
						goto IL_0c8f;
						IL_0c8f:
						num2 = 163;
						frmNodeSel.f_Mode = 2;
						goto IL_0c9e;
						IL_0c9e:
						num2 = 165;
						if (Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null), "", TextCompare: false))
						{
							goto IL_0ccb;
						}
						goto IL_0cf3;
						IL_0ccb:
						num2 = 166;
						frmNodeSel.f_OutNode = Conversions.ToString(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null));
						goto IL_0cf3;
						IL_0cf3:
						num2 = 167;
						frmNodeSel.ShowDialog();
						goto IL_0d01;
						IL_0d01:
						num2 = 168;
						text5 = frmNodeSel.f_OutNode;
						goto IL_0d10;
						IL_0d10:
						num2 = 169;
						frmNodeSel.Dispose();
						goto IL_0d1e;
						IL_0d1e:
						num2 = 170;
						NewLateBinding.LateSet(instance, null, "Text", new object[1] { text5 }, null, null);
						break;
						IL_0a22:
						num2 = 130;
						if (MyCmb == null)
						{
							goto IL_0a33;
						}
						goto IL_0a44;
						IL_0a33:
						num2 = 131;
						text6 = "1";
						goto IL_0ab6;
						IL_0a44:
						num2 = 133;
						text6 = "0";
						goto IL_0a51;
						IL_0a51:
						num2 = 134;
						if (Operators.CompareString(Strings.Trim(MyCmb.Text), "", TextCompare: false) == 0)
						{
							goto IL_0a77;
						}
						goto IL_0a96;
						IL_0a77:
						num2 = 135;
						Interaction.MsgBox("You have not as yet set the value for your Database Node. Please do so before running this help query!", MsgBoxStyle.Exclamation, "Missing Database Node");
						goto end_IL_0001_3;
						IL_0a96:
						num2 = 138;
						myOtherServer = Globals_Renamed.MyOtherServer1;
						goto IL_0aa3;
						IL_0aa3:
						num2 = 139;
						Globals_Renamed.MyOtherServer1 = MyCmb.Text;
						goto IL_0ab6;
						IL_0ab6:
						num2 = 141;
						frmgetdata2 = new frmgetdata();
						frmgetdata2.Tag = text6;
						frmgetdata2.ll_Mode = "I";
						frmgetdata2.ll_Tag = Conversions.ToString(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null));
						frmgetdata3 = frmgetdata2;
						goto IL_0aff;
						IL_0aff:
						num2 = 142;
						Globals_Renamed.currinputstrtmp = text2;
						goto IL_0b0c;
						IL_0b0c:
						num2 = 143;
						frmgetdata3.ShowDialog();
						goto IL_0b1a;
						IL_0b1a:
						num2 = 144;
						frmgetdata3.Dispose();
						goto IL_0b28;
						IL_0b28:
						num2 = 145;
						if (Operators.CompareString(text6, "0", TextCompare: false) == 0)
						{
							goto IL_0b44;
						}
						goto IL_0b51;
						IL_0b44:
						num2 = 146;
						Globals_Renamed.MyOtherServer1 = myOtherServer;
						goto IL_0b51;
						IL_0b51:
						num2 = 147;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "CANCEL", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0b75;
						IL_0b75:
						num2 = 149;
						NewLateBinding.LateSet(instance, null, "Text", new object[1] { Globals_Renamed.currinputstrtmp }, null, null);
						break;
						IL_0901:
						num2 = 113;
						text5 = Conversions.ToString(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null));
						goto IL_0921;
						IL_0921:
						num2 = 114;
						frmDTREdit = new FrmDTREdit();
						goto IL_092b;
						IL_092b:
						num2 = 115;
						frmDTREdit.Text1.Text = text5;
						goto IL_093d;
						IL_093d:
						num2 = 116;
						frmDTREdit.cmbSQLVA.Text = "WRITE";
						goto IL_0952;
						IL_0952:
						num2 = 117;
						frmDTREdit.cmbSQLVA.Enabled = false;
						goto IL_0963;
						IL_0963:
						num2 = 118;
						frmDTREdit.out_odbc = "--";
						goto IL_0972;
						IL_0972:
						num2 = 119;
						frmDTREdit.Text1.SelectionStart = 0;
						goto IL_0983;
						IL_0983:
						num2 = 120;
						frmDTREdit.Text1.SelectionLength = 0;
						goto IL_0994;
						IL_0994:
						num2 = 121;
						frmDTREdit.KeepSQL = true;
						goto IL_099f;
						IL_099f:
						num2 = 122;
						frmDTREdit.fSaveSQL = false;
						goto IL_09aa;
						IL_09aa:
						num2 = 123;
						frmDTREdit.ShowDialog();
						goto IL_09b5;
						IL_09b5:
						num2 = 124;
						text5 = Strings.Trim(frmDTREdit.KeepSQLData);
						goto IL_09c6;
						IL_09c6:
						num2 = 125;
						frmDTREdit.Dispose();
						goto IL_09d1;
						IL_09d1:
						num2 = 126;
						if (Operators.CompareString(text5, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_09ea;
						IL_09ea:
						num2 = 127;
						NewLateBinding.LateSet(instance, null, "text", new object[1] { Strings.Replace(text5, "\r\n", " ", 1, -1, CompareMethod.Text) }, null, null);
						break;
						IL_0829:
						num2 = 103;
						lCSVCols = new string[0];
						goto IL_0834;
						IL_0834:
						num2 = 104;
						num10 = 0;
						goto IL_083a;
						IL_083a:
						num2 = 105;
						num10 = General_Procedures.GetCSVHeaders(MyCmb.Text, ref lCSVCols, MakeHdrsLowerCase: false, R_Replace: false, AllowAllCols: true);
						goto IL_0850;
						IL_0850:
						num2 = 106;
						NewLateBinding.LateCall(NewLateBinding.LateGet(instance, null, "items", new object[0], null, null, null), null, "clear", new object[0], null, null, null, IgnoreReturn: true);
						goto IL_087f;
						IL_087f:
						num2 = 107;
						if (num10 != 0)
						{
							break;
						}
						goto IL_088d;
						IL_088d:
						num2 = 108;
						num11 = Information.UBound(lCSVCols);
						num5 = 0;
						goto IL_08f3;
						IL_08f3:
						if (num5 > num11)
						{
							break;
						}
						goto IL_089f;
						IL_089f:
						num2 = 109;
						NewLateBinding.LateCall(NewLateBinding.LateGet(instance, null, "items", new object[0], null, null, null), null, "add", new object[1] { "[" + Strings.Trim(lCSVCols[num5]) + "]" }, null, null, null, IgnoreReturn: true);
						goto IL_08ea;
						IL_08ea:
						num2 = 110;
						num5++;
						goto IL_08f3;
						IL_0635:
						num2 = 78;
						text6 = text8;
						goto IL_063c;
						IL_063c:
						num2 = 79;
						if (Operators.CompareString(text6, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0658;
						IL_0658:
						num2 = 80;
						text5 = Strings.Trim(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null)));
						goto IL_067d;
						IL_067d:
						num2 = 81;
						left = Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Tag", new object[0], null, null, null)), 10, 1);
						goto IL_06a5;
						IL_06a5:
						num2 = 82;
						if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
						{
							goto IL_06be;
						}
						goto IL_06f4;
						IL_06be:
						num2 = 83;
						BuildForm.FileOpenSave("O", text5, text6, "Choose a File", text9);
						goto IL_06d7;
						IL_06d7:
						num2 = 84;
						text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						goto IL_0727;
						IL_06f4:
						num2 = 86;
						BuildForm.FileOpenSave("S", text5, text6, "Choose a File", text9);
						goto IL_070d;
						IL_070d:
						num2 = 87;
						text4 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
						goto IL_0727;
						IL_0727:
						num2 = 89;
						if (!((Operators.CompareString(text4, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0)))
						{
							break;
						}
						goto IL_0754;
						IL_0754:
						num2 = 90;
						text5 = text4;
						goto IL_075b;
						IL_075b:
						num2 = 91;
						num7 = Strings.InStrRev(text5, "\\");
						goto IL_076e;
						IL_076e:
						num2 = 92;
						if (num7 != 0)
						{
							goto IL_077c;
						}
						goto IL_07bb;
						IL_077c:
						num2 = 93;
						if (Operators.CompareString(Strings.UCase(Strings.Mid(text5, 1, num7)), Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) == 0)
						{
							goto IL_07a7;
						}
						goto IL_07bb;
						IL_07a7:
						num2 = 94;
						text5 = Strings.Mid(text5, num7 + 1);
						goto IL_07bb;
						IL_07bb:
						num2 = 97;
						if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Tag", new object[0], null, null, null)), 8, 1), "Y", TextCompare: false) == 0)
						{
							goto IL_07f4;
						}
						goto IL_0801;
						IL_07f4:
						num2 = 98;
						text5 = StripExt(text5);
						goto IL_0801;
						IL_0801:
						num2 = 99;
						NewLateBinding.LateSet(instance, null, "Text", new object[1] { text5 }, null, null);
						break;
						IL_0539:
						num2 = 66;
						MyProject.Forms.FrmMain.FolderBrowserDialog1.Description = "Locate a Directory";
						goto IL_0556;
						IL_0556:
						num2 = 67;
						if (Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null), "", TextCompare: false))
						{
							goto IL_0580;
						}
						goto IL_05b7;
						IL_0580:
						num2 = 68;
						MyProject.Forms.FrmMain.FolderBrowserDialog1.SelectedPath = Conversions.ToString(NewLateBinding.LateGet(instance, null, "Text", new object[0], null, null, null));
						goto IL_05d5;
						end_IL_0001_2:
						break;
					}
					num2 = 173;
					tabPage = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4122;
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
					F_Output = "CANCEL";
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

	private void cmdOK_Click(object sender, EventArgs e)
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
						errsource = "frmInput - CmdOK";
						string text = "";
						string text2 = "";
						string text3 = ",";
						string text4 = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						Button button = null;
						string text5 = "";
						string text6 = "";
						bool flag = false;
						TabPage tabPage = TabControl1.TabPages[TabControl1.SelectedIndex];
						string text7 = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						text7 = ((TabControl1.SelectedIndex != 0) ? ("-" + Conversions.ToString(TabControl1.SelectedIndex + 1)) : "");
						f_NoDisplayHdrs = Strings.Trim(General_Procedures.Get_Ini_Data("donotdisplay" + text7, "headers", "", 5000, f_InputFile));
						f_NoDisplayData = "\"" + Strings.Trim(General_Procedures.Get_Ini_Data("donotdisplay" + text7, "data", "", 5000, f_InputFile)) + "\"";
						if (Operators.CompareString(f_NoDisplayData, "\"\"", TextCompare: false) == 0)
						{
							f_NoDisplayData = "";
						}
						text3 = f_DLM;
						if (Operators.CompareString(text3, ",", TextCompare: false) != 0)
						{
							f_NoDisplayHdrs = Strings.Replace(f_NoDisplayHdrs, ",", text3, 1, -1, CompareMethod.Text);
							f_NoDisplayData = Strings.Replace(f_NoDisplayData, ",", text3, 1, -1, CompareMethod.Text);
						}
						object obj = null;
						flag = false;
						text6 = "";
						int num6 = f_NoCtlPerTab - 1;
						num3 = 0;
						while (true)
						{
							if (num3 <= num6)
							{
								int num7 = tabPage.Controls.Count - 1;
								num4 = 0;
								while (true)
								{
									if (num4 <= num7)
									{
										if (Operators.CompareString(Strings.LCase(tabPage.Controls[num4].Name), "combobox_" + Conversions.ToString(TabControl1.SelectedIndex + 1) + "_" + Conversions.ToString(num3), TextCompare: false) == 0)
										{
											obj = tabPage.Controls[num4];
											if ((Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 1, 1), "Y", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Text", new object[0], null, null, null))), "", TextCompare: false) == 0))
											{
												Interaction.MsgBox("Some required fields (shown in dark red) are missing values. Please correct before submitting the form", MsgBoxStyle.Exclamation, "Missing Required Values");
												goto end_IL_0414;
											}
											if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 1, 1), "C", TextCompare: false) == 0)
											{
												text6 += Test_Ignore_Cond(Strings.Trim(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Text", new object[0], null, null, null))));
												flag = true;
											}
											if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 2, 1), "Y", TextCompare: false) == 0)
											{
												text = text + text3 + Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 15);
												if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 14, 1), "Y", TextCompare: false) == 0)
												{
													text = text + text3 + Strings.Trim(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 15)) + "_display";
												}
											}
										}
										num4++;
										continue;
									}
									num3++;
									break;
								}
								continue;
							}
							if (flag & (Operators.CompareString(Strings.Trim(text6), "", TextCompare: false) == 0))
							{
								Interaction.MsgBox("You must enter data for one of the underlined variables. Please correct before submitting the form", MsgBoxStyle.Exclamation, "Missing Conditional Values");
								break;
							}
							text = text + text3 + f_NoDisplayHdrs;
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								text = Strings.Mid(text, 2);
							}
							int num8 = f_NoCtlPerTab - 1;
							num3 = 0;
							while (true)
							{
								if (num3 <= num8)
								{
									int num9 = tabPage.Controls.Count - 1;
									num4 = 0;
									while (true)
									{
										if (num4 <= num9)
										{
											if (Operators.CompareString(Strings.LCase(tabPage.Controls[num4].Name), "combobox_" + Conversions.ToString(TabControl1.SelectedIndex + 1) + "_" + Conversions.ToString(num3), TextCompare: false) == 0)
											{
												obj = tabPage.Controls[num4];
												if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 2, 1), "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 7, 1), "Y", TextCompare: false) == 0)
												{
													text5 = Conversions.ToString(NewLateBinding.LateGet(obj, null, "Text", new object[0], null, null, null));
													text10 = text5;
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 13, 1), "Y", TextCompare: false) == 0)
													{
														text5 = General_Procedures.Quote_CRLF_Replace("EQ", text5);
													}
													text4 = Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Name", new object[0], null, null, null)), 9);
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 6, 1), "Y", TextCompare: false) == 0)
													{
														int num10 = tabPage.Controls.Count - 1;
														for (num5 = 0; num5 <= num10; num5++)
														{
															if (Operators.CompareString(Strings.LCase(tabPage.Controls[num5].Name), "cmdval" + text4, TextCompare: false) == 0)
															{
																button = (Button)tabPage.Controls[num5];
																text5 = Generate_Data(text5, Conversions.ToString(button.Tag));
																if (LikeOperator.LikeString(text5, "$ERROR$:*", CompareMethod.Binary))
																{
																	Interaction.MsgBox(Strings.Mid(text5, 9) + ". Please correct before submitting the form", MsgBoxStyle.Exclamation, "Data Entry Error");
																	goto end_IL_0a6b;
																}
																break;
															}
														}
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 3, 1), "Y", TextCompare: false) == 0)
													{
														text5 = StripOuterQuotes(text5);
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 8, 1), "Y", TextCompare: false) == 0)
													{
														text5 = StripExt(text5);
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 4, 1), "Y", TextCompare: false) == 0)
													{
														text5 = Strings.Replace(text5, "\\", "/", 1, -1, CompareMethod.Text);
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 5, 1), "Y", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(text5), "", TextCompare: false) == 0)
													{
														text5 = "*N/A*";
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 7, 1), "Y", TextCompare: false) == 0)
													{
														Process_Transform(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Text", new object[0], null, null, null)), text4);
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 9, 1), "Y", TextCompare: false) == 0)
													{
														text5 = ((Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 11, 1), "Y", TextCompare: false) != 0) ? General_Procedures.NodeCheck_s(text5, "S", 1) : (text5 + "^^^^^" + General_Procedures.NodeCheck_s(text5, "S2", 1)));
													}
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 13, 1), "Y", TextCompare: false) == 0)
													{
														text5 = General_Procedures.Quote_CRLF_Replace("DQ", text5);
													}
													text5 = Strings.Replace(text5, "\"", "\"\"");
													if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 2, 1), "Y", TextCompare: false) == 0)
													{
														text2 = text2 + text3 + "\"" + text5 + "\"";
														if (Operators.CompareString(Strings.Mid(Conversions.ToString(NewLateBinding.LateGet(obj, null, "Tag", new object[0], null, null, null)), 14, 1), "Y", TextCompare: false) == 0)
														{
															text2 = text2 + text3 + "\"" + text10 + "\"";
														}
													}
												}
											}
											num4++;
											continue;
										}
										num3++;
										break;
									}
									continue;
								}
								f_NoDisplayData = Strings.Trim(f_NoDisplayData);
								if (Operators.CompareString(f_NoDisplayData, "", TextCompare: false) != 0)
								{
									text2 = text2 + text3 + f_NoDisplayData;
								}
								if (Operators.CompareString(text2, "", TextCompare: false) != 0)
								{
									text2 = Strings.Mid(text2, 2) + "\r\n";
								}
								text = Strings.Trim(text);
								if (Operators.CompareString(Strings.Right(text, 1), ",", TextCompare: false) == 0)
								{
									text = Strings.Mid(text, 1, Strings.Len(text) - 1);
								}
								F_Output = text + "\r\n" + text2;
								Load_Save_Inputs("S1");
								tabPage = null;
								Close();
								break;
								continue;
								end_IL_0a6b:
								break;
							}
							break;
							continue;
							end_IL_0414:
							break;
						}
						goto end_IL_0001;
					}
					case 2917:
						num = -1;
						switch (num2)
						{
						case 2:
						{
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							TabPage tabPage = null;
							goto end_IL_0001;
						}
						}
						break;
					}
					goto IL_0b9b;
				}
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 2917;
				continue;
			}
			break;
			IL_0b9b:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FrmInput_FormClosing(object sender, FormClosingEventArgs e)
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
				case 54:
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
				Globals_Renamed.gMidasDriver = f_MidasDriver;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 54;
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void FrmInput_Load(object sender, EventArgs e)
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
						errsource = "frmInput - Load";
						int num3 = 1;
						Button MyButton = cmdCancel;
						BuildForm.Set_Btn_Img(ref MyButton, "cancel");
						cmdCancel = MyButton;
						MyButton = cmdOK;
						BuildForm.Set_Btn_Img(ref MyButton, "ok");
						cmdOK = MyButton;
						int num4 = 0;
						string text = "";
						F_Output = "CANCEL";
						f_InputFile = Strings.Trim(f_InputFile);
						f_InputFile = Strings.Replace(f_InputFile, "@schemadir@", Globals_Renamed.MySchemaDir, 1, -1, CompareMethod.Text);
						F_OutputFile = Strings.Trim(F_OutputFile);
						if (Operators.CompareString(F_Mode, "", TextCompare: false) == 0)
						{
							f_DLM = BuildForm.GetFileDLM(F_OutputFile);
						}
						else if (Operators.CompareString(F_Mode, "1", TextCompare: false) == 0)
						{
							f_DLM = ",";
						}
						if (Operators.CompareString(f_InputFile, "", TextCompare: false) != 0 && Strings.InStrRev(f_InputFile, "\\") == 0)
						{
							f_InputFile = Globals_Renamed.MyPCDir + f_InputFile;
						}
						if ((Operators.CompareString(f_InputFile, "", TextCompare: false) == 0) | !File.Exists(f_InputFile))
						{
							Interaction.MsgBox("The input specification file cannot be found (" + f_InputFile + ")", MsgBoxStyle.Exclamation, "Input Specification File Not Found");
							break;
						}
						if (LikeOperator.LikeString(f_InputFile, "\\\\*", CompareMethod.Binary))
						{
							num4 = Strings.InStrRev(f_InputFile, "\\");
							text = Globals_Renamed.MyPCDir + Strings.Trim(Strings.Mid(f_InputFile, num4 + 1));
							FileSystem.FileCopy(f_InputFile, text);
							f_InputFile = text;
						}
						num3 = Conversions.ToInteger(Init_Options());
						if (Operators.CompareString(F_Mode, "1", TextCompare: false) == 0)
						{
							TabControl1.SelectedIndex = num3 - 1;
						}
						goto end_IL_0001;
					}
					case 612:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							break;
						default:
							goto end_IL_0001_2;
						}
						break;
					}
					Close();
					break;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 612;
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

	private void cmdLoad_Click(object sender, EventArgs e)
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
				case 55:
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
				Load_Save_Inputs("L");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void cmdSave_Click(object sender, EventArgs e)
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
				case 55:
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
				Load_Save_Inputs("S");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

	private void cmdReload_Click(object sender, EventArgs e)
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
				case 55:
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
				Load_Save_Inputs("L1");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 55;
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

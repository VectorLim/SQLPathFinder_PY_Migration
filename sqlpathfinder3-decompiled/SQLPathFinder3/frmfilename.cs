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
internal class frmfilename : Form
{
	private IContainer components;

	public ToolTip ToolTip1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdBrowse")]
	private Button _cmdBrowse;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("chkprompt")]
	private CheckBox _chkprompt;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	private string MyType;

	private short gRunTime;

	private short ll_ObjectType;

	private string ll_DBType;

	private Control cmbgtype;

	public string f_Pattern;

	private string L_Mode;

	[field: AccessedThroughProperty("cmbftype")]
	public virtual ComboBox cmbftype
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mskFileName")]
	public virtual ComboBox mskFileName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkUnique")]
	public virtual CheckBox chkUnique
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdBrowse
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
			EventHandler value2 = CmdBrowse_Click;
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

	public virtual CheckBox chkprompt
	{
		[CompilerGenerated]
		get
		{
			return _chkprompt;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ChkPrompt_CheckStateChanged;
			CheckBox checkBox = _chkprompt;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged -= value2;
			}
			_chkprompt = value;
			checkBox = _chkprompt;
			if (checkBox != null)
			{
				checkBox.CheckStateChanged += value2;
			}
		}
	}

	public virtual Button CmdCancel
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

	public virtual Button CmdOK
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
			EventHandler value2 = cmdOK_Click;
			MouseEventHandler value3 = CmdOK_MouseDown;
			Button button = _CmdOK;
			if (button != null)
			{
				button.Click -= value2;
				button.MouseDown -= value3;
			}
			_CmdOK = value;
			button = _CmdOK;
			if (button != null)
			{
				button.Click += value2;
				button.MouseDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("lblftype")]
	public virtual Label lblftype
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label1")]
	public virtual Label Label1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[DebuggerNonUserCode]
	public frmfilename()
	{
		base.Load += frmfilename_Load;
		base.FormClosed += frmfilename_FormClosed;
		base.MouseHover += frmfilename_MouseHover;
		MyType = "";
		gRunTime = 0;
		ll_ObjectType = 0;
		ll_DBType = "0";
		cmbgtype = null;
		f_Pattern = "";
		L_Mode = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.frmfilename));
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmbftype = new System.Windows.Forms.ComboBox();
		this.chkUnique = new System.Windows.Forms.CheckBox();
		this.chkprompt = new System.Windows.Forms.CheckBox();
		this.mskFileName = new System.Windows.Forms.ComboBox();
		this.cmdBrowse = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.lblftype = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.cmbftype.BackColor = System.Drawing.SystemColors.Window;
		this.cmbftype.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbftype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbftype.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbftype.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbftype.Location = new System.Drawing.Point(78, 248);
		this.cmbftype.Name = "cmbftype";
		this.cmbftype.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbftype.Size = new System.Drawing.Size(144, 24);
		this.cmbftype.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmbftype, "Select file type");
		this.cmbftype.Visible = false;
		this.chkUnique.BackColor = System.Drawing.SystemColors.Control;
		this.chkUnique.Cursor = System.Windows.Forms.Cursors.Default;
		this.chkUnique.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkUnique.ForeColor = System.Drawing.SystemColors.ControlText;
		this.chkUnique.Location = new System.Drawing.Point(2, 217);
		this.chkUnique.Name = "chkUnique";
		this.chkUnique.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.chkUnique.Size = new System.Drawing.Size(220, 20);
		this.chkUnique.TabIndex = 2;
		this.chkUnique.Text = "Add timestamp to name";
		this.ToolTip1.SetToolTip(this.chkUnique, "Check to have a unique timestamp added to the file name");
		this.chkUnique.UseVisualStyleBackColor = false;
		this.chkUnique.Visible = false;
		this.chkprompt.AutoSize = true;
		this.chkprompt.BackColor = System.Drawing.SystemColors.Control;
		this.chkprompt.Cursor = System.Windows.Forms.Cursors.Default;
		this.chkprompt.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkprompt.ForeColor = System.Drawing.SystemColors.ControlText;
		this.chkprompt.Location = new System.Drawing.Point(2, 194);
		this.chkprompt.Name = "chkprompt";
		this.chkprompt.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.chkprompt.Size = new System.Drawing.Size(216, 20);
		this.chkprompt.TabIndex = 1;
		this.chkprompt.Text = "Prompt for Value at Run-Time";
		this.ToolTip1.SetToolTip(this.chkprompt, "Check to be prompted for a value at Run-Time. ");
		this.chkprompt.UseVisualStyleBackColor = false;
		this.mskFileName.BackColor = System.Drawing.SystemColors.Window;
		this.mskFileName.Cursor = System.Windows.Forms.Cursors.Default;
		this.mskFileName.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.mskFileName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.mskFileName.Location = new System.Drawing.Point(2, 164);
		this.mskFileName.Name = "mskFileName";
		this.mskFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.mskFileName.Size = new System.Drawing.Size(392, 24);
		this.mskFileName.Sorted = true;
		this.mskFileName.TabIndex = 0;
		this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
		this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdBrowse.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdBrowse.Location = new System.Drawing.Point(126, 0);
		this.cmdBrowse.Name = "cmdBrowse";
		this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdBrowse.Size = new System.Drawing.Size(62, 34);
		this.cmdBrowse.TabIndex = 4;
		this.cmdBrowse.Text = "Browse";
		this.cmdBrowse.UseVisualStyleBackColor = false;
		this.cmdBrowse.Visible = false;
		this.CmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCancel.Location = new System.Drawing.Point(63, 0);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCancel.Size = new System.Drawing.Size(62, 34);
		this.CmdCancel.TabIndex = 5;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = false;
		this.CmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdOK.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdOK.Location = new System.Drawing.Point(0, 0);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdOK.Size = new System.Drawing.Size(62, 34);
		this.CmdOK.TabIndex = 6;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = false;
		this.lblftype.AutoSize = true;
		this.lblftype.BackColor = System.Drawing.SystemColors.Control;
		this.lblftype.Cursor = System.Windows.Forms.Cursors.Default;
		this.lblftype.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblftype.ForeColor = System.Drawing.SystemColors.ControlText;
		this.lblftype.Location = new System.Drawing.Point(2, 248);
		this.lblftype.Name = "lblftype";
		this.lblftype.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblftype.Size = new System.Drawing.Size(74, 16);
		this.lblftype.TabIndex = 3;
		this.lblftype.Text = " File Type:";
		this.lblftype.Visible = false;
		this.Label1.BackColor = System.Drawing.Color.Transparent;
		this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label1.Location = new System.Drawing.Point(2, 48);
		this.Label1.Name = "Label1";
		this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label1.Size = new System.Drawing.Size(392, 101);
		this.Label1.TabIndex = 5;
		this.Label1.Text = "Enter the name of the file you wish your results written to.";
		base.AcceptButton = this.CmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(397, 288);
		base.Controls.Add(this.cmbftype);
		base.Controls.Add(this.mskFileName);
		base.Controls.Add(this.chkUnique);
		base.Controls.Add(this.cmdBrowse);
		base.Controls.Add(this.chkprompt);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.lblftype);
		base.Controls.Add(this.Label1);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.SystemColors.WindowText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(425, 150);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmfilename";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Tag = "DTR";
		this.Text = "Enter a File Name";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void ChkPrompt_CheckStateChanged(object eventSender, EventArgs eventArgs)
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
				case 371:
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
							goto IL_002a;
						case 5:
							goto IL_0040;
						case 6:
							goto IL_004f;
						case 7:
							goto IL_006b;
						case 8:
							goto IL_007a;
						case 9:
							goto IL_0089;
						case 12:
							goto IL_009f;
						case 13:
							goto IL_00af;
						case 14:
							goto IL_00cc;
						case 15:
							goto IL_00dc;
						case 16:
							goto IL_00ec;
						case 17:
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 10:
						case 11:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cc:
					num2 = 14;
					chkUnique.Visible = true;
					goto IL_00dc;
					IL_00dc:
					num2 = 15;
					cmbftype.Visible = true;
					goto IL_00ec;
					IL_00af:
					num2 = 13;
					if (Operators.CompareString(MyType, "E", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00cc;
					IL_00ec:
					num2 = 16;
					lblftype.Visible = true;
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(MyType, "X", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_002a;
					IL_002a:
					num2 = 4;
					if (chkprompt.CheckState == CheckState.Checked)
					{
						goto IL_0040;
					}
					goto IL_009f;
					IL_0040:
					num2 = 5;
					mskFileName.Enabled = false;
					goto IL_004f;
					IL_004f:
					num2 = 6;
					if (Operators.CompareString(MyType, "E", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_006b;
					IL_006b:
					num2 = 7;
					chkUnique.Visible = false;
					goto IL_007a;
					IL_007a:
					num2 = 8;
					cmbftype.Visible = false;
					goto IL_0089;
					IL_0089:
					num2 = 9;
					lblftype.Visible = false;
					goto end_IL_0001_3;
					IL_009f:
					num2 = 12;
					mskFileName.Enabled = true;
					goto IL_00af;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				mskFileName.Focus();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 371;
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

	private void CmdBrowse_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						short num3 = 0;
						string text6 = "0";
						if (Operators.CompareString(MyType, "I", TextCompare: false) == 0)
						{
							BuildForm.FileOpenSave("O", "", "txt", "Find File With Items For Row Filter", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "X", TextCompare: false) == 0)
						{
							BuildForm.FileOpenSave("O", "", "txt", "Find File With Field Patterns", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "F", TextCompare: false) == 0)
						{
							if (LikeOperator.LikeString(cmbftype.Text, "*File", CompareMethod.Binary))
							{
								BuildForm.FileOpenSave("O", "", "txt", "Find File", "");
								if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
								{
									break;
								}
								text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
						}
						else if (Operators.CompareString(MyType, "P", TextCompare: false) == 0)
						{
							text4 = mskFileName.Text;
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "PPT2", TextCompare: false) == 0)
							{
								BuildForm.FileOpenSave("O", text4, "ppt", "Choose a PowerPoint Template File", "");
							}
							else
							{
								BuildForm.FileOpenSave("O", text4, "ppt", "Select a PowerPoint file", "");
							}
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "U", TextCompare: false) == 0)
						{
							text4 = mskFileName.Text;
							BuildForm.FileOpenSave("O", text4, "jsl", "Choose a JSL File", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "C", TextCompare: false) == 0)
						{
							text4 = mskFileName.Text;
							BuildForm.FileOpenSave("O", text4, "images", "Choose an Image", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "D", TextCompare: false) == 0)
						{
							text4 = mskFileName.Text;
							BuildForm.FileOpenSave("O", text4, "htm", "Choose an HTML File", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "N", TextCompare: false) == 0 && Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(mskFileName.Text), "*.SDB", CompareMethod.Binary))
						{
							text4 = mskFileName.Text;
							BuildForm.FileOpenSave("O", text4, "sdb", "Choose a SQLite Database", "");
							if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
							{
								break;
							}
							text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
						}
						else if (Operators.CompareString(MyType, "N", TextCompare: false) == 0)
						{
							text4 = mskFileName.Text;
							FrmNodeSel frmNodeSel = new FrmNodeSel
							{
								f_OutNode = text4
							};
							text6 = ((Operators.CompareString(ll_DBType, "3", TextCompare: false) == 0) ? "2" : ((Operators.CompareString(ll_DBType, "2", TextCompare: false) != 0) ? ll_DBType : "3"));
							frmNodeSel.f_DBType = text6;
							frmNodeSel.f_Mode = 1;
							frmNodeSel.f_Pattern = f_Pattern;
							frmNodeSel.ShowDialog();
							text4 = frmNodeSel.f_OutNode;
							frmNodeSel.Dispose();
						}
						else if (Operators.CompareString(MyType, "A", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.currinputstrtmp, "ASDB", TextCompare: false) == 0)
						{
							if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
							{
								BuildSQL.RunQ(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Generate_Any_Schema.py\" /DBTYPE=SQLite /Alias=DEFAULT /SDB=\"", chkUnique.Tag), "\" /TABLE=\"LIST ALL TABLES\" /SPFINSTANCE=\""), Globals_Renamed.gSPFCache), "\"")), 0, "M");
							}
							else
							{
								BuildSQL.RunQ(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Generate_SQLite_Schema.va\" /sdb=\"", chkUnique.Tag), "\" /TABLE=\"LIST ALL TABLES\"")), 0, "M");
							}
						}
						else
						{
							text = ((Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) != 0) ? Strings.Trim(mskFileName.Text) : ((Operators.CompareString(MyType, "V", TextCompare: false) == 0) ? "MyInlineView" : ((Operators.CompareString(MyType, "Z", TextCompare: false) != 0) ? "OutFile" : "")));
							text2 = ((Operators.CompareString(MyType, "I", TextCompare: false) == 0) ? "txt" : ((Operators.CompareString(MyType, "V", TextCompare: false) == 0) ? "ilv" : ((Operators.CompareString(MyType, "J", TextCompare: false) == 0) ? ((Operators.CompareString(Globals_Renamed.currinputstrtmp, "JMP2", TextCompare: false) == 0) ? "csv" : ((Operators.CompareString(Globals_Renamed.currinputstrtmp, "JR", TextCompare: false) != 0) ? "jrn" : "csv+r")) : ((Operators.CompareString(MyType, "B", TextCompare: false) != 0) ? "csv+" : "txt"))));
							if (Operators.CompareString(MyType, "Z", TextCompare: false) == 0)
							{
								text5 = "Select Macro File";
								BuildForm.FileOpenSave("O", text, text2, text5, "");
								if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) == 0)
								{
									break;
								}
								text4 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							}
							else
							{
								BuildForm.FileOpenSave("S", text, text2, "Specify Name and Path of File to create", "");
								if (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0)
								{
									break;
								}
								text4 = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
							}
						}
						if (Operators.CompareString(MyType, "E", TextCompare: false) == 0 || Operators.CompareString(MyType, "V", TextCompare: false) == 0 || Operators.CompareString(MyType, "Z", TextCompare: false) == 0 || Operators.CompareString(MyType, "B", TextCompare: false) == 0 || Operators.CompareString(MyType, "J", TextCompare: false) == 0 || Operators.CompareString(MyType, "P", TextCompare: false) == 0 || Operators.CompareString(MyType, "U", TextCompare: false) == 0 || Operators.CompareString(MyType, "C", TextCompare: false) == 0 || Operators.CompareString(MyType, "D", TextCompare: false) == 0 || Operators.CompareString(MyType, "X", TextCompare: false) == 0 || (Operators.CompareString(MyType, "F", TextCompare: false) == 0 && LikeOperator.LikeString(cmbftype.Text, "*File", CompareMethod.Binary)) || (Operators.CompareString(MyType, "N", TextCompare: false) == 0 && Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(text4), "*.SDB", CompareMethod.Binary)))
						{
							num3 = (short)Strings.InStrRev(text4, "\\");
							if (num3 != 0)
							{
								text3 = Strings.UCase(Strings.Mid(text4, 1, num3));
								if (Operators.CompareString(text3, Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) == 0)
								{
									text4 = Strings.Mid(text4, num3 + 1);
								}
								else if ((Operators.CompareString(MyType, "C", TextCompare: false) == 0 || Operators.CompareString(MyType, "D", TextCompare: false) == 0) && LikeOperator.LikeString(text3, Strings.UCase(Globals_Renamed.MyPCDir) + "*", CompareMethod.Binary))
								{
									text4 = ".\\" + Strings.Mid(text4, Strings.Len(Globals_Renamed.MyPCDir) + 1);
								}
							}
						}
						if (Operators.CompareString(MyType, "A", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.currinputstrtmp, "ASDB", TextCompare: false) != 0)
						{
							mskFileName.Text = text4;
						}
						break;
					}
					case 2954:
						num = -1;
						switch (num2)
						{
						case 2:
							break;
						default:
							goto IL_0bc0;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2954;
				continue;
			}
			break;
			IL_0bc0:
			throw ProjectData.CreateProjectError(-2146828237);
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
					Globals_Renamed.currvaluetmp = "CANCEL";
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

	private void cmdOK_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		short num5 = default(short);
		string text4 = default(string);
		string str = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					string NodeList;
					ComboBox comboBox;
					bool num7;
					string currvaluetmp;
					int result;
					bool num8;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 6480:
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
								goto IL_0013;
							case 4:
								goto IL_001c;
							case 5:
								goto IL_0025;
							case 6:
								goto IL_002a;
							case 7:
								goto IL_002f;
							case 8:
								goto IL_0038;
							case 9:
								goto IL_0041;
							case 10:
								goto IL_0056;
							case 11:
								goto IL_0076;
							case 12:
								goto IL_00e7;
							case 13:
								goto IL_00fc;
							case 14:
								goto IL_010b;
							case 15:
								goto IL_011b;
							case 19:
								goto IL_0138;
							case 20:
								goto IL_0158;
							case 21:
								goto IL_0176;
							case 23:
								goto IL_0189;
							case 24:
								goto IL_01a7;
							case 26:
								goto IL_01b7;
							case 27:
								goto IL_01d5;
							case 29:
								goto IL_01e6;
							case 30:
								goto IL_01fb;
							case 31:
								goto IL_020a;
							case 32:
								goto IL_021a;
							case 22:
							case 25:
							case 28:
							case 34:
							case 35:
								goto IL_0230;
							case 17:
							case 18:
							case 36:
							case 37:
								goto IL_023b;
							case 38:
								goto IL_0258;
							case 40:
								goto IL_0267;
							case 41:
								goto IL_0284;
							case 43:
								goto IL_0293;
							case 44:
								goto IL_02b0;
							case 46:
								goto IL_02bf;
							case 47:
								goto IL_031b;
							case 49:
								goto IL_033c;
							case 50:
								goto IL_0384;
							case 52:
								goto IL_0391;
							case 39:
							case 42:
							case 45:
							case 48:
							case 51:
							case 53:
							case 54:
								goto IL_039b;
							case 55:
								goto IL_03a5;
							case 56:
								goto IL_03b2;
							case 57:
								goto IL_0426;
							case 58:
								goto IL_043d;
							case 60:
								goto IL_0450;
							case 61:
								goto IL_0477;
							case 63:
								goto IL_048b;
							case 64:
								goto IL_04bd;
							case 65:
								goto IL_04d5;
							case 66:
								goto IL_0507;
							case 68:
								goto IL_051b;
							case 69:
								goto IL_0530;
							case 70:
								goto IL_0544;
							case 71:
								goto IL_0552;
							case 72:
								goto IL_0561;
							case 74:
								goto IL_0575;
							case 73:
							case 75:
							case 76:
								goto IL_0580;
							case 77:
								goto IL_0593;
							case 78:
								goto IL_05a1;
							case 79:
							case 80:
								goto IL_05b4;
							case 81:
								goto IL_05cd;
							case 83:
								goto IL_05e1;
							case 84:
								goto IL_05fc;
							case 85:
								goto IL_0613;
							case 86:
								goto IL_062a;
							case 87:
								goto IL_063d;
							case 88:
								goto IL_0659;
							case 89:
								goto IL_0679;
							case 90:
								goto IL_0698;
							case 91:
								goto IL_06aa;
							case 92:
								goto IL_06f1;
							case 67:
							case 82:
							case 93:
							case 94:
							case 95:
							case 96:
								goto IL_0711;
							case 97:
								goto IL_072d;
							case 98:
								goto IL_0737;
							case 99:
								goto IL_0750;
							case 100:
								goto IL_0765;
							case 106:
								goto IL_078a;
							case 107:
								goto IL_07aa;
							case 108:
								goto IL_07c9;
							case 109:
								goto IL_07e0;
							case 111:
								goto IL_07f3;
							case 112:
								goto IL_0815;
							case 114:
								goto IL_0824;
							case 115:
								goto IL_086e;
							case 117:
								goto IL_0885;
							case 118:
								goto IL_089a;
							case 122:
								goto IL_08b5;
							case 123:
								goto IL_08d5;
							case 124:
								goto IL_08fc;
							case 126:
								goto IL_090c;
							case 127:
								goto IL_091f;
							case 128:
								goto IL_0931;
							case 129:
								goto IL_094b;
							case 130:
							case 131:
								goto IL_096d;
							case 132:
								goto IL_0986;
							case 133:
								goto IL_0997;
							case 136:
								goto IL_09bd;
							case 137:
								goto IL_09e0;
							case 138:
								goto IL_0a0a;
							case 140:
								goto IL_0a1e;
							case 141:
								goto IL_0a39;
							case 142:
								goto IL_0a58;
							case 146:
								goto IL_0a7b;
							case 147:
								goto IL_0a9e;
							case 148:
								goto IL_0ac8;
							case 150:
								goto IL_0adc;
							case 151:
								goto IL_0af7;
							case 152:
								goto IL_0b16;
							case 156:
								goto IL_0b39;
							case 157:
								goto IL_0b5c;
							case 158:
								goto IL_0b86;
							case 161:
								goto IL_0ba7;
							case 162:
								goto IL_0bc2;
							case 163:
								goto IL_0be1;
							case 167:
								goto IL_0c04;
							case 168:
								goto IL_0c24;
							case 169:
								goto IL_0c3f;
							case 170:
								goto IL_0c58;
							case 173:
								goto IL_0c6c;
							case 174:
								goto IL_0c8f;
							case 175:
								goto IL_0caa;
							case 176:
								goto IL_0ccc;
							case 177:
								goto IL_0cff;
							case 178:
								goto IL_0d17;
							case 181:
								goto IL_0d34;
							case 182:
								goto IL_0d56;
							case 183:
								goto IL_0d6e;
							case 186:
								goto IL_0d8b;
							case 187:
								goto IL_0dc3;
							case 188:
								goto IL_0ddb;
							case 191:
								goto IL_0df6;
							case 195:
								goto IL_0e1e;
							case 196:
								goto IL_0e41;
							case 197:
								goto IL_0e5c;
							case 198:
								goto IL_0e7b;
							case 200:
								goto IL_0ecb;
							case 201:
								goto IL_0eeb;
							case 202:
								goto IL_0f06;
							case 203:
								goto IL_0f25;
							case 205:
								goto IL_0f50;
							case 206:
								goto IL_0f73;
							case 207:
								goto IL_0f8b;
							case 208:
								goto IL_0fa3;
							case 209:
								goto IL_0fb9;
							case 210:
								goto IL_0fca;
							case 212:
								goto IL_1001;
							case 211:
							case 213:
							case 214:
								goto IL_100f;
							case 215:
								goto IL_1043;
							case 217:
							case 218:
								goto IL_1062;
							case 219:
								goto IL_108f;
							case 221:
								goto IL_10a3;
							case 224:
								goto IL_10c3;
							case 225:
								goto IL_10e6;
							case 228:
								goto IL_111c;
							case 229:
								goto IL_113c;
							case 230:
								goto IL_1163;
							case 235:
								goto IL_119b;
							case 236:
								goto IL_11bb;
							case 237:
								goto IL_11da;
							case 241:
								goto IL_11ff;
							case 242:
								goto IL_1222;
							case 243:
								goto IL_123c;
							case 245:
								goto IL_124f;
							case 246:
								goto IL_1279;
							case 248:
								goto IL_1298;
							case 251:
								goto IL_12af;
							case 252:
								goto IL_12d2;
							case 253:
								goto IL_1320;
							case 255:
								goto IL_133f;
							case 256:
								goto IL_1357;
							case 257:
								goto IL_1369;
							case 258:
								goto IL_137c;
							case 262:
								goto IL_139c;
							case 263:
								goto IL_13c6;
							case 265:
								goto IL_13d9;
							case 266:
								goto IL_13fc;
							case 267:
								goto IL_147b;
							case 269:
							case 270:
							case 271:
								goto IL_14b1;
							case 272:
								goto IL_14c7;
							case 59:
							case 62:
							case 102:
							case 103:
							case 104:
							case 105:
							case 110:
							case 113:
							case 116:
							case 120:
							case 121:
							case 125:
							case 134:
							case 135:
							case 139:
							case 143:
							case 144:
							case 145:
							case 149:
							case 153:
							case 154:
							case 155:
							case 160:
							case 164:
							case 165:
							case 166:
							case 171:
							case 172:
							case 180:
							case 185:
							case 190:
							case 192:
							case 193:
							case 194:
							case 199:
							case 204:
							case 220:
							case 222:
							case 223:
							case 227:
							case 231:
							case 233:
							case 234:
							case 238:
							case 239:
							case 240:
							case 244:
							case 247:
							case 249:
							case 250:
							case 254:
							case 260:
							case 261:
							case 264:
							case 273:
							case 274:
							case 275:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 16:
							case 33:
							case 101:
							case 119:
							case 159:
							case 179:
							case 184:
							case 189:
							case 216:
							case 226:
							case 232:
							case 259:
							case 268:
							case 276:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_1369:
						num2 = 257;
						mskFileName.SelectionStart = 0;
						goto IL_137c;
						IL_137c:
						num2 = 258;
						mskFileName.SelectionLength = 0;
						goto end_IL_0001_3;
						IL_1357:
						num2 = 256;
						mskFileName.Focus();
						goto IL_1369;
						IL_139c:
						num2 = 262;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_13c6;
						}
						goto IL_13d9;
						IL_000b:
						num2 = 2;
						text = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						text2 = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						text3 = "";
						goto IL_0025;
						IL_0025:
						num2 = 5;
						num5 = 0;
						goto IL_002a;
						IL_002a:
						num2 = 6;
						num6 = 0;
						goto IL_002f;
						IL_002f:
						num2 = 7;
						text4 = "";
						goto IL_0038;
						IL_0038:
						num2 = 8;
						str = "";
						goto IL_0041;
						IL_0041:
						num2 = 9;
						text2 = Strings.Trim(mskFileName.Text);
						goto IL_0056;
						IL_0056:
						num2 = 10;
						if (Operators.CompareString(MyType, "T", TextCompare: false) == 0)
						{
							goto IL_0076;
						}
						goto IL_0138;
						IL_0076:
						num2 = 11;
						if (((Operators.CompareString(Strings.Mid(text2, 1, 1), "0", TextCompare: false) >= 0) & (Operators.CompareString(Strings.Mid(text2, 1, 1), "9", TextCompare: false) <= 0)) | ((Strings.InStr(text2, ":") != 0) | (Strings.InStr(text2, "{") != 0) | (Strings.InStr(text2, "}") != 0)))
						{
							goto IL_00e7;
						}
						goto IL_023b;
						IL_00e7:
						num2 = 12;
						Interaction.MsgBox("Temporary Table labels cannot start with a number nor contain characters :, {, or }.", MsgBoxStyle.Exclamation, "Invalid Temp Table Name");
						goto IL_00fc;
						IL_00fc:
						num2 = 13;
						mskFileName.Focus();
						goto IL_010b;
						IL_010b:
						num2 = 14;
						mskFileName.SelectionStart = 0;
						goto IL_011b;
						IL_011b:
						num2 = 15;
						mskFileName.SelectionLength = 0;
						goto end_IL_0001_3;
						IL_0138:
						num2 = 19;
						if (Operators.CompareString(MyType, "A-1", TextCompare: false) == 0)
						{
							goto IL_0158;
						}
						goto IL_023b;
						IL_0158:
						num2 = 20;
						if (Operators.CompareString(Strings.UCase(text2), "YES", TextCompare: false) == 0)
						{
							goto IL_0176;
						}
						goto IL_0189;
						IL_0176:
						num2 = 21;
						Globals_Renamed.currvaluetmp = "Yes";
						goto IL_0230;
						IL_0189:
						num2 = 23;
						if (Operators.CompareString(Strings.UCase(text2), "NO", TextCompare: false) == 0)
						{
							goto IL_01a7;
						}
						goto IL_01b7;
						IL_01a7:
						num2 = 24;
						Globals_Renamed.currvaluetmp = "No";
						goto IL_0230;
						IL_01b7:
						num2 = 26;
						if (Operators.CompareString(Strings.UCase(text2), "DEFAULT", TextCompare: false) == 0)
						{
							goto IL_01d5;
						}
						goto IL_01e6;
						IL_01d5:
						num2 = 27;
						Globals_Renamed.currvaluetmp = "Default";
						goto IL_0230;
						IL_0230:
						num2 = 35;
						Close();
						goto IL_023b;
						IL_01e6:
						num2 = 29;
						Interaction.MsgBox("Invalid Post Extract Selection Selection}.", MsgBoxStyle.Exclamation, "Invalid Entry");
						goto IL_01fb;
						IL_01fb:
						num2 = 30;
						mskFileName.Focus();
						goto IL_020a;
						IL_020a:
						num2 = 31;
						mskFileName.SelectionStart = 0;
						goto IL_021a;
						IL_021a:
						num2 = 32;
						mskFileName.SelectionLength = 0;
						goto end_IL_0001_3;
						IL_023b:
						num2 = 37;
						if (Operators.CompareString(MyType, "L", TextCompare: false) == 0)
						{
							goto IL_0258;
						}
						goto IL_0267;
						IL_0258:
						num2 = 38;
						text = "-1";
						goto IL_039b;
						IL_0267:
						num2 = 40;
						if (Operators.CompareString(MyType, "V", TextCompare: false) == 0)
						{
							goto IL_0284;
						}
						goto IL_0293;
						IL_0284:
						num2 = 41;
						text = "";
						goto IL_039b;
						IL_0293:
						num2 = 43;
						if (Operators.CompareString(MyType, "R", TextCompare: false) == 0)
						{
							goto IL_02b0;
						}
						goto IL_02bf;
						IL_02b0:
						num2 = 44;
						text = "No";
						goto IL_039b;
						IL_02bf:
						num2 = 46;
						if ((Operators.CompareString(MyType, "E", TextCompare: false) == 0) | (Operators.CompareString(MyType, "J", TextCompare: false) == 0) | (Operators.CompareString(MyType, "P", TextCompare: false) == 0) | (Operators.CompareString(MyType, "U", TextCompare: false) == 0))
						{
							goto IL_031b;
						}
						goto IL_033c;
						IL_031b:
						num2 = 47;
						text = "." + cmbftype.Text.ToLower();
						goto IL_039b;
						IL_033c:
						num2 = 49;
						if (Operators.CompareString(MyType, "N", TextCompare: false) == 0 || (Operators.CompareString(MyType, "A", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.currinputstrtmp, "ASDB", TextCompare: false) != 0))
						{
							goto IL_0384;
						}
						goto IL_0391;
						IL_13c6:
						num2 = 263;
						Globals_Renamed.currvaluetmp = text;
						break;
						IL_13d9:
						num2 = 265;
						if (Operators.CompareString(MyType, "N", TextCompare: false) == 0)
						{
							goto IL_13fc;
						}
						goto IL_14b1;
						IL_0391:
						num2 = 52;
						text = "";
						goto IL_039b;
						IL_0384:
						num2 = 50;
						text = "Default";
						goto IL_039b;
						IL_039b:
						num2 = 54;
						text2 = "";
						goto IL_03a5;
						IL_03a5:
						num2 = 55;
						Globals_Renamed.currvaluetmp = "";
						goto IL_03b2;
						IL_03b2:
						num2 = 56;
						if ((Operators.CompareString(MyType, "E", TextCompare: false) == 0) | (Operators.CompareString(MyType, "V", TextCompare: false) == 0) | (Operators.CompareString(MyType, "J", TextCompare: false) == 0) | (Operators.CompareString(MyType, "P", TextCompare: false) == 0) | (Operators.CompareString(MyType, "U", TextCompare: false) == 0))
						{
							goto IL_0426;
						}
						goto IL_078a;
						IL_0426:
						num2 = 57;
						if (chkprompt.CheckState == CheckState.Checked)
						{
							goto IL_043d;
						}
						goto IL_0450;
						IL_043d:
						num2 = 58;
						Globals_Renamed.currvaluetmp = "PROMPT";
						break;
						IL_0450:
						num2 = 60;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_0477;
						}
						goto IL_048b;
						IL_0477:
						num2 = 61;
						Globals_Renamed.currvaluetmp = "";
						break;
						IL_048b:
						num2 = 63;
						if ((Operators.CompareString(MyType, "P", TextCompare: false) == 0) | (Operators.CompareString(MyType, "U", TextCompare: false) == 0))
						{
							goto IL_04bd;
						}
						goto IL_051b;
						IL_04bd:
						num2 = 64;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_04d5;
						IL_04d5:
						num2 = 65;
						if (!LikeOperator.LikeString(Globals_Renamed.currvaluetmp, "*<<<*>>>*", CompareMethod.Binary) && Strings.InStrRev(Globals_Renamed.currvaluetmp, ".") == 0)
						{
							goto IL_0507;
						}
						goto IL_0711;
						IL_13fc:
						num2 = 266;
						if (Operators.CompareString(Strings.Trim(Strings.UCase(mskFileName.Text)), "DEFAULT", TextCompare: false) != 0 && (Operators.CompareString(ll_DBType, "9", TextCompare: false) != 0 || !LikeOperator.LikeString(Strings.UCase(mskFileName.Text), "*.SDB", CompareMethod.Binary)) && !LikeOperator.LikeString(Strings.UCase(mskFileName.Text), "DUCKDB:*", CompareMethod.Binary))
						{
							goto IL_147b;
						}
						goto IL_14b1;
						IL_0507:
						num2 = 66;
						text4 += text;
						goto IL_0711;
						IL_051b:
						num2 = 68;
						text4 = Strings.Trim(mskFileName.Text);
						goto IL_0530;
						IL_0530:
						num2 = 69;
						num5 = (short)Strings.InStrRev(text4, "\\", -1, CompareMethod.Text);
						goto IL_0544;
						IL_0544:
						num2 = 70;
						if (num5 != 0)
						{
							goto IL_0552;
						}
						goto IL_0575;
						IL_0552:
						num2 = 71;
						str = Strings.Mid(text4, 1, num5);
						goto IL_0561;
						IL_0561:
						num2 = 72;
						text4 = Strings.Mid(text4, num5 + 1);
						goto IL_0580;
						IL_0575:
						num2 = 74;
						str = "";
						goto IL_0580;
						IL_0580:
						num2 = 76;
						num5 = (short)Strings.InStr(text4, ".");
						goto IL_0593;
						IL_0593:
						num2 = 77;
						if (num5 != 0)
						{
							goto IL_05a1;
						}
						goto IL_05b4;
						IL_05a1:
						num2 = 78;
						text4 = Strings.Mid(text4, 1, num5 - 1);
						goto IL_05b4;
						IL_05b4:
						num2 = 80;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto IL_05cd;
						}
						goto IL_05e1;
						IL_05cd:
						num2 = 81;
						Globals_Renamed.currvaluetmp = "";
						goto IL_0711;
						IL_05e1:
						num2 = 83;
						Globals_Renamed.currvaluetmp = Strings.Trim(str) + Strings.Trim(text4);
						goto IL_05fc;
						IL_05fc:
						num2 = 84;
						if (chkUnique.CheckState == CheckState.Checked)
						{
							goto IL_0613;
						}
						goto IL_062a;
						IL_0613:
						num2 = 85;
						Globals_Renamed.currvaluetmp += "{TS}";
						goto IL_062a;
						IL_062a:
						num2 = 86;
						Globals_Renamed.currvaluetmp += text;
						goto IL_063d;
						IL_063d:
						num2 = 87;
						Globals_Renamed.currvaluetmp = General_Procedures.Substitute_Chars(Globals_Renamed.currvaluetmp, "\"", "'");
						goto IL_0659;
						IL_0659:
						num2 = 88;
						if (Operators.CompareString(MyType, "V", TextCompare: false) == 0)
						{
							goto IL_0679;
						}
						goto IL_0711;
						IL_0679:
						num2 = 89;
						Globals_Renamed.currvaluetmp = BuildForm.GetDBTypeIL(0, Globals_Renamed.currvaluetmp, ll_DBType, ll_ObjectType);
						goto IL_0698;
						IL_0698:
						num2 = 90;
						Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp.Trim();
						goto IL_06aa;
						IL_06aa:
						num2 = 91;
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.currvaluetmp.ToUpper().Substring(Globals_Renamed.currvaluetmp.Length - 4), ".ILV", TextCompare: false) != 0)
						{
							goto IL_06f1;
						}
						goto IL_0711;
						IL_147b:
						num2 = 267;
						NodeList = (comboBox = mskFileName).Text;
						num7 = BuildForm.VerifyNodes(ref NodeList);
						comboBox.Text = NodeList;
						if (!num7)
						{
							goto end_IL_0001_3;
						}
						goto IL_14b1;
						IL_06f1:
						num2 = 92;
						Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp.Trim() + ".ilv";
						goto IL_0711;
						IL_0711:
						num2 = 96;
						if (LikeOperator.LikeString(Globals_Renamed.currvaluetmp, "*<<<*>>>*", CompareMethod.Binary))
						{
							break;
						}
						goto IL_072d;
						IL_072d:
						num2 = 97;
						text2 = Globals_Renamed.currvaluetmp;
						goto IL_0737;
						IL_0737:
						num2 = 98;
						text2 = Strings.Replace(text2, "{TS}", "", 1, -1, CompareMethod.Text);
						goto IL_0750;
						IL_0750:
						num2 = 99;
						if (!General_Procedures.Check_For_Char(text2, "?/|<>\"*{};"))
						{
							break;
						}
						goto IL_0765;
						IL_0765:
						num2 = 100;
						Interaction.MsgBox("File names cannot contain the following characters: ?/|<>\"*{};", MsgBoxStyle.Exclamation, "Invalid Character");
						goto end_IL_0001_3;
						IL_078a:
						num2 = 106;
						if (Operators.CompareString(MyType, "L", TextCompare: false) == 0)
						{
							goto IL_07aa;
						}
						goto IL_08b5;
						IL_07aa:
						num2 = 107;
						mskFileName.Text = Strings.Trim(mskFileName.Text);
						goto IL_07c9;
						IL_07c9:
						num2 = 108;
						if (chkprompt.CheckState == CheckState.Checked)
						{
							goto IL_07e0;
						}
						goto IL_07f3;
						IL_07e0:
						num2 = 109;
						Globals_Renamed.currvaluetmp = "-99";
						break;
						IL_07f3:
						num2 = 111;
						if (Operators.CompareString(mskFileName.Text, "", TextCompare: false) == 0)
						{
							goto IL_0815;
						}
						goto IL_0824;
						IL_0815:
						num2 = 112;
						Globals_Renamed.currvaluetmp = text;
						break;
						IL_0824:
						num2 = 114;
						if (LikeOperator.LikeString(mskFileName.Text, "<<<*>>>", CompareMethod.Binary) | (Versioned.IsNumeric(mskFileName.Text) & (Strings.InStr(mskFileName.Text, ".") == 0)))
						{
							goto IL_086e;
						}
						goto IL_0885;
						IL_086e:
						num2 = 115;
						Globals_Renamed.currvaluetmp = mskFileName.Text;
						break;
						IL_0885:
						num2 = 117;
						Interaction.MsgBox("The row limit must either be an integer or a macro variable bounded by <<< and >>>.", MsgBoxStyle.Exclamation, "Invalid Row Limit");
						goto IL_089a;
						IL_089a:
						num2 = 118;
						mskFileName.Focus();
						goto end_IL_0001_3;
						IL_08b5:
						num2 = 122;
						if (Operators.CompareString(MyType, "A", TextCompare: false) == 0)
						{
							goto IL_08d5;
						}
						goto IL_09bd;
						IL_08d5:
						num2 = 123;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_08fc;
						}
						goto IL_090c;
						IL_08fc:
						num2 = 124;
						Globals_Renamed.currvaluetmp = text;
						break;
						IL_090c:
						num2 = 126;
						Globals_Renamed.currvaluetmp = mskFileName.Text;
						goto IL_091f;
						IL_091f:
						num2 = 127;
						Globals_Renamed.currvaluetmp = Strings.Trim(Globals_Renamed.currvaluetmp);
						goto IL_0931;
						IL_0931:
						num2 = 128;
						if (chkUnique.CheckState == CheckState.Checked)
						{
							goto IL_094b;
						}
						goto IL_096d;
						IL_094b:
						num2 = 129;
						Globals_Renamed.currvaluetmp = BuildForm.Get_SchemaName_Ini(Conversions.ToString(chkUnique.Tag));
						goto IL_096d;
						IL_096d:
						num2 = 131;
						num5 = (short)Strings.InStr(Globals_Renamed.currvaluetmp, "{");
						goto IL_0986;
						IL_0986:
						num2 = 132;
						if (num5 == 0)
						{
							break;
						}
						goto IL_0997;
						IL_0997:
						num2 = 133;
						Globals_Renamed.currvaluetmp = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 1, num5 - 1));
						break;
						IL_09bd:
						num2 = 136;
						if (Operators.CompareString(MyType, "Z", TextCompare: false) == 0)
						{
							goto IL_09e0;
						}
						goto IL_0a7b;
						IL_09e0:
						num2 = 137;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_0a0a;
						}
						goto IL_0a1e;
						IL_0a0a:
						num2 = 138;
						Globals_Renamed.currvaluetmp = "";
						break;
						IL_0a1e:
						num2 = 140;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0a39;
						IL_0a39:
						num2 = 141;
						if (Strings.InStr(Globals_Renamed.currvaluetmp, ".") != 0)
						{
							break;
						}
						goto IL_0a58;
						IL_0a58:
						num2 = 142;
						Globals_Renamed.currvaluetmp += ".csv";
						break;
						IL_0a7b:
						num2 = 146;
						if (Operators.CompareString(MyType, "B", TextCompare: false) == 0)
						{
							goto IL_0a9e;
						}
						goto IL_0b39;
						IL_0a9e:
						num2 = 147;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_0ac8;
						}
						goto IL_0adc;
						IL_0ac8:
						num2 = 148;
						Globals_Renamed.currvaluetmp = "";
						break;
						IL_0adc:
						num2 = 150;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0af7;
						IL_0af7:
						num2 = 151;
						if (Strings.InStr(Globals_Renamed.currvaluetmp, ".") != 0)
						{
							break;
						}
						goto IL_0b16;
						IL_0b16:
						num2 = 152;
						Globals_Renamed.currvaluetmp += ".txt";
						break;
						IL_0b39:
						num2 = 156;
						if (Operators.CompareString(MyType, "Q", TextCompare: false) == 0)
						{
							goto IL_0b5c;
						}
						goto IL_0c04;
						IL_0b5c:
						num2 = 157;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto IL_0b86;
						}
						goto IL_0ba7;
						IL_0b86:
						num2 = 158;
						Interaction.MsgBox("You must specify a valid file namee. To remove the File from your query, delete it from the Query Columns Grid.", MsgBoxStyle.Exclamation, "Missing");
						goto end_IL_0001_3;
						IL_0ba7:
						num2 = 161;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0bc2;
						IL_0bc2:
						num2 = 162;
						if (Strings.InStr(Globals_Renamed.currvaluetmp, ".") != 0)
						{
							break;
						}
						goto IL_0be1;
						IL_0be1:
						num2 = 163;
						Globals_Renamed.currvaluetmp += ".txt";
						break;
						IL_0c04:
						num2 = 167;
						if (Operators.CompareString(MyType, "R", TextCompare: false) == 0)
						{
							goto IL_0c24;
						}
						goto IL_0c6c;
						IL_0c24:
						num2 = 168;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0c3f;
						IL_0c3f:
						num2 = 169;
						if (Versioned.IsNumeric(Globals_Renamed.currvaluetmp))
						{
							break;
						}
						goto IL_0c58;
						IL_0c58:
						num2 = 170;
						Globals_Renamed.currvaluetmp = text;
						break;
						IL_0c6c:
						num2 = 173;
						if (Operators.CompareString(MyType, "K", TextCompare: false) == 0)
						{
							goto IL_0c8f;
						}
						goto IL_0e1e;
						IL_0c8f:
						num2 = 174;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0caa;
						IL_0caa:
						num2 = 175;
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0ccc;
						IL_0ccc:
						num2 = 176;
						currvaluetmp = Globals_Renamed.currvaluetmp;
						result = Conversions.ToInteger(Globals_Renamed.currvaluetmp);
						num8 = int.TryParse(currvaluetmp, out result);
						Globals_Renamed.currvaluetmp = Conversions.ToString(result);
						if (!num8)
						{
							goto IL_0cff;
						}
						goto IL_0d34;
						IL_0cff:
						num2 = 177;
						Interaction.MsgBox("The Mongo option must be an Integer", MsgBoxStyle.Exclamation, "Invalid Option");
						goto IL_0d17;
						IL_0d17:
						num2 = 178;
						mskFileName.Focus();
						goto end_IL_0001_3;
						IL_0d34:
						num2 = 181;
						if (Operators.ConditionalCompareObjectLess(Conversion.Int(Globals_Renamed.currvaluetmp), 0, TextCompare: false))
						{
							goto IL_0d56;
						}
						goto IL_0d8b;
						IL_0d56:
						num2 = 182;
						Interaction.MsgBox("The Option value must be greater than 0", MsgBoxStyle.Exclamation, "Invalid Option");
						goto IL_0d6e;
						IL_0d6e:
						num2 = 183;
						mskFileName.Focus();
						goto end_IL_0001_3;
						IL_0d8b:
						num2 = 186;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "K3", TextCompare: false) == 0 && Operators.ConditionalCompareObjectGreater(Conversion.Int(Globals_Renamed.currvaluetmp), 100, TextCompare: false))
						{
							goto IL_0dc3;
						}
						goto IL_0df6;
						IL_14c7:
						num2 = 272;
						Globals_Renamed.currvaluetmp = Strings.Trim(Globals_Renamed.currvaluetmp);
						break;
						IL_0dc3:
						num2 = 187;
						Interaction.MsgBox("The Fill Percent must be between 0 and 100", MsgBoxStyle.Exclamation, "Invalid Option");
						goto IL_0ddb;
						IL_0ddb:
						num2 = 188;
						mskFileName.Focus();
						goto end_IL_0001_3;
						IL_0df6:
						num2 = 191;
						Globals_Renamed.currvaluetmp = Conversion.Str(RuntimeHelpers.GetObjectValue(Conversion.Int(Globals_Renamed.currvaluetmp)));
						break;
						IL_0e1e:
						num2 = 195;
						if (Operators.CompareString(MyType, "C", TextCompare: false) == 0)
						{
							goto IL_0e41;
						}
						goto IL_0ecb;
						IL_0e41:
						num2 = 196;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0e5c;
						IL_0e5c:
						num2 = 197;
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0e7b;
						IL_0e7b:
						num2 = 198;
						Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + "+" + cmbftype.Text + ":" + cmbgtype.Text;
						break;
						IL_0ecb:
						num2 = 200;
						if (Operators.CompareString(MyType, "D", TextCompare: false) == 0)
						{
							goto IL_0eeb;
						}
						goto IL_0f50;
						IL_0eeb:
						num2 = 201;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						goto IL_0f06;
						IL_0f06:
						num2 = 202;
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0f25;
						IL_0f25:
						num2 = 203;
						Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + ":" + cmbftype.Text;
						break;
						IL_0f50:
						num2 = 205;
						if (Operators.CompareString(MyType, "O", TextCompare: false) == 0)
						{
							goto IL_0f73;
						}
						goto IL_10c3;
						IL_0f73:
						num2 = 206;
						text2 = Strings.Trim(cmbftype.Text);
						goto IL_0f8b;
						IL_0f8b:
						num2 = 207;
						text3 = Strings.Trim(mskFileName.Text);
						goto IL_0fa3;
						IL_0fa3:
						num2 = 208;
						num5 = (short)Strings.InStr(text2, ":");
						goto IL_0fb9;
						IL_0fb9:
						num2 = 209;
						if (num5 != 0)
						{
							goto IL_0fca;
						}
						goto IL_1001;
						IL_0fca:
						num2 = 210;
						text2 = Strings.Trim(Strings.Mid(text2, num5 + 1)) + ":" + Strings.Trim(Strings.Mid(text2, 1, num5 - 1));
						goto IL_100f;
						IL_1001:
						num2 = 212;
						text2 = "";
						goto IL_100f;
						IL_100f:
						num2 = 214;
						if (Operators.CompareString(Strings.LCase(text3) + ":" + Strings.LCase(text3), Strings.LCase(text2), TextCompare: false) == 0)
						{
							goto IL_1043;
						}
						goto IL_1062;
						IL_1043:
						num2 = 215;
						Interaction.MsgBox("You cannot append a Layout object to itself.", MsgBoxStyle.Exclamation, "Invalid Append");
						goto end_IL_0001_3;
						IL_1062:
						num2 = 218;
						if ((Operators.CompareString(text3, "", TextCompare: false) == 0) | (Operators.CompareString(text2, "", TextCompare: false) == 0))
						{
							goto IL_108f;
						}
						goto IL_10a3;
						IL_108f:
						num2 = 219;
						Globals_Renamed.currvaluetmp = "";
						break;
						IL_10a3:
						num2 = 221;
						Globals_Renamed.currvaluetmp = text3 + "</@#;>" + text2;
						break;
						IL_10c3:
						num2 = 224;
						if (Operators.CompareString(MyType, "F", TextCompare: false) == 0)
						{
							goto IL_10e6;
						}
						goto IL_11ff;
						IL_10e6:
						num2 = 225;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_111c;
						IL_111c:
						num2 = 228;
						if (Operators.CompareString(L_Mode, "", TextCompare: false) == 0)
						{
							goto IL_113c;
						}
						goto IL_119b;
						IL_113c:
						num2 = 229;
						if (!BuildForm.Test_Search_Pat(cmbftype.Text, mskFileName.Text))
						{
							goto end_IL_0001_3;
						}
						goto IL_1163;
						IL_1163:
						num2 = 230;
						Globals_Renamed.currvaluetmp = cmbftype.Text + "<;>" + mskFileName.Text;
						break;
						IL_119b:
						num2 = 235;
						if (Operators.CompareString(L_Mode, "Q", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_11bb;
						IL_11bb:
						num2 = 236;
						text2 = Strings.UCase(Strings.Mid(cmbftype.Text, 1, 1));
						goto IL_11da;
						IL_11da:
						num2 = 237;
						Globals_Renamed.currvaluetmp = text2 + mskFileName.Text;
						break;
						IL_11ff:
						num2 = 241;
						if (Operators.CompareString(MyType, "X", TextCompare: false) == 0)
						{
							goto IL_1222;
						}
						goto IL_12af;
						IL_1222:
						num2 = 242;
						if (!chkprompt.Checked)
						{
							goto IL_123c;
						}
						goto IL_124f;
						IL_123c:
						num2 = 243;
						Globals_Renamed.currvaluetmp = "No";
						break;
						IL_124f:
						num2 = 245;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "", TextCompare: false) != 0)
						{
							goto IL_1279;
						}
						goto IL_1298;
						IL_1279:
						num2 = 246;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						break;
						IL_1298:
						num2 = 248;
						Globals_Renamed.currvaluetmp = "Yes";
						break;
						IL_12af:
						num2 = 251;
						if (Operators.CompareString(MyType, "PARALLEL", TextCompare: false) == 0)
						{
							goto IL_12d2;
						}
						goto IL_139c;
						IL_12d2:
						num2 = 252;
						if (Operators.CompareString(Strings.Trim(mskFileName.Text), "0", TextCompare: false) >= 0 && Operators.CompareString(Strings.Trim(mskFileName.Text), "9", TextCompare: false) <= 0)
						{
							goto IL_1320;
						}
						goto IL_133f;
						IL_14b1:
						num2 = 271;
						Globals_Renamed.currvaluetmp = mskFileName.Text;
						goto IL_14c7;
						IL_1320:
						num2 = 253;
						Globals_Renamed.currvaluetmp = Strings.Trim(mskFileName.Text);
						break;
						IL_133f:
						num2 = 255;
						Interaction.MsgBox("Invalid number of parallel threads set. Value should be between 0 and 9.", MsgBoxStyle.Exclamation, "Invalid Thread Count");
						goto IL_1357;
						end_IL_0001_2:
						break;
					}
					num2 = 275;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6480;
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

	private void frmfilename_Load(object eventSender, EventArgs eventArgs)
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
						errsource = "frmFileName - Form_Load";
						short num3 = 0;
						string text = "";
						short num4 = 0;
						bool flag = false;
						string text2 = "";
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "", TextCompare: false) == 0)
						{
							MyType = "";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "MACRO", TextCompare: false) == 0)
						{
							MyType = "Z";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "PARALLEL", TextCompare: false) == 0)
						{
							MyType = "PARALLEL";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "WF", TextCompare: false) == 0)
						{
							MyType = "B";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "FC", TextCompare: false) == 0)
						{
							MyType = "F";
							text2 = "C";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "FQ", TextCompare: false) == 0)
						{
							MyType = "F";
							text2 = "Q";
							L_Mode = "Q";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "GG", TextCompare: false) == 0)
						{
							MyType = "G";
							text2 = "G";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "X1", TextCompare: false) == 0)
						{
							MyType = "X";
							text2 = "Sub-Component";
						}
						else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "X2", TextCompare: false) == 0)
						{
							MyType = "X";
							text2 = "MQCS";
						}
						else if (Operators.CompareString(Strings.Mid(Globals_Renamed.currinputstrtmp + "  ", 1, 2), "A-", TextCompare: false) == 0)
						{
							MyType = Globals_Renamed.currinputstrtmp;
						}
						else
						{
							MyType = Strings.Mid(Strings.UCase(Strings.Trim(Globals_Renamed.currinputstrtmp)), 1, 1);
						}
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "&RUN-TIME&", TextCompare: false) == 0)
						{
							gRunTime = 1;
						}
						else
						{
							gRunTime = 0;
						}
						switch (MyType)
						{
						case "L":
							if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "-1", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.currvaluetmp, "-99", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.currvaluetmp, "&RUN-TIME&", TextCompare: false) != 0))
							{
								mskFileName.Text = Globals_Renamed.currvaluetmp;
							}
							else if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "-99", TextCompare: false) == 0)
							{
								chkprompt.CheckState = CheckState.Checked;
								mskFileName.Text = "";
							}
							else
							{
								mskFileName.Text = "";
							}
							Text = "Enter an Output Row Limit";
							mskFileName.Items.Add("10");
							mskFileName.Items.Add("20");
							mskFileName.Items.Add("50");
							mskFileName.Items.Add("99");
							if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "&RUN-TIME&", TextCompare: false) == 0)
							{
								Label1.Text = General_Procedures.Get_UI("filerow1");
								chkprompt.Visible = false;
								ToolTip1.SetToolTip(CmdCancel, "Cancel Query");
							}
							else
							{
								Label1.Text = General_Procedures.Get_UI("filerow2");
								chkprompt.Visible = false;
							}
							break;
						case "A-1":
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							mskFileName.Items.Add("Yes");
							mskFileName.Items.Add("No");
							mskFileName.Items.Add("Default");
							chkprompt.Visible = false;
							cmdBrowse.Visible = false;
							Label1.Text = "Enter Yes to perform Kitchen Sink field pattern selection and exclusion in Python following the MongoDB extract. This becomes necessary as the number of patterns increase. Choose No to do Pattern selection and Exclusion in MongoDB. Choose Default to use the general setting defined in the SPF Configuration Form.";
							Text = "Pattern Selection";
							break;
						case "X":
							Text = text2 + " Data";
							Label1.Text = "Check Option \"Include " + text2 + " Data\" to include fields belonging to a " + text2 + " item in your Kitchen Sink query. Optionally specify a file name containing a list of regular expression patterns the " + text2 + " field names must match. The file should contain a column header and store one row per regular expression pattern. Uncheck option \"Include " + text2 + " Data\" to not include " + text2 + " fields in your query";
							ToolTip1.SetToolTip(chkprompt, "Include " + text2 + " Data");
							ToolTip1.SetToolTip(cmdBrowse, "Locate file with field patterns");
							chkprompt.Text = "Include " + text2 + " Data";
							chkprompt.Visible = true;
							cmdBrowse.Visible = true;
							if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "N", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0)
							{
								chkprompt.Checked = true;
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "Y", TextCompare: false) != 0)
								{
									mskFileName.Text = Globals_Renamed.currvaluetmp;
								}
							}
							else
							{
								mskFileName.Text = "";
								chkprompt.Checked = false;
							}
							break;
						case "R":
							Text = "Refresh Rate";
							Label1.Text = "Enter the HTML Report Refresh Rate in seconds. Valid if Output is published to a SharePoint or Web Site";
							mskFileName.Items.Add("120");
							mskFileName.Items.Add("300");
							mskFileName.Items.Add("600");
							if (Versioned.IsNumeric(Globals_Renamed.currvaluetmp))
							{
								mskFileName.Text = Globals_Renamed.currvaluetmp;
							}
							else
							{
								mskFileName.Text = "";
							}
							chkprompt.Visible = false;
							break;
						case "G":
						{
							Text = "Globals";
							Label1.Text = "Choose or Enter a Global Variable bounded by <<< >>>";
							if (Operators.CompareString(text2, "G", TextCompare: false) == 0)
							{
								Label1.Text += ". The Globals must translate to a GUID (a Global Unique Identifier) which has a specific format. E.g., 2ed6657d-e927-568b-95e1-2665a8aea6a2";
							}
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							short num5 = (short)Globals_Renamed.gNoGlobals;
							for (num4 = 0; num4 <= num5; num4 = (short)unchecked(num4 + 1))
							{
								mskFileName.Items.Add("<<<" + Globals_Renamed.gGlobals[num4].Variable + ">>>");
							}
							chkprompt.Visible = false;
							break;
						}
						case "E":
						{
							chkUnique.Visible = true;
							cmdBrowse.Visible = true;
							cmbftype.Items.Clear();
							cmbftype.Items.Add("tab");
							cmbftype.Items.Add("csv");
							cmbftype.Items.Add("parquet");
							cmbftype.Items.Add("txt");
							cmbftype.Visible = true;
							lblftype.Visible = true;
							base.Height = 330;
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								mskFileName.Text = "";
								cmbftype.Text = "tab";
							}
							else if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "PROMPT", TextCompare: false) == 0)
							{
								chkprompt.CheckState = CheckState.Checked;
								mskFileName.Text = "";
								cmbftype.Text = "tab";
							}
							else if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "&RUN-TIME&", TextCompare: false) != 0)
							{
								if (Strings.InStr(Strings.UCase(Globals_Renamed.currvaluetmp), "{TS}") != 0)
								{
									chkUnique.CheckState = CheckState.Checked;
									Globals_Renamed.currvaluetmp = Strings.Replace(Globals_Renamed.currvaluetmp, "{TS}", "", 1, -1, CompareMethod.Text);
								}
								else
								{
									chkUnique.CheckState = CheckState.Unchecked;
								}
								num3 = (short)Strings.InStrRev(Strings.UCase(Globals_Renamed.currvaluetmp), ".");
								if (num3 != 0)
								{
									mskFileName.Text = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
									cmbftype.Text = Strings.LCase(Strings.Mid(Globals_Renamed.currvaluetmp, num3 + 1));
								}
								else
								{
									mskFileName.Text = Globals_Renamed.currvaluetmp;
									cmbftype.Text = "tab";
								}
							}
							else
							{
								mskFileName.Text = "";
							}
							if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "&RUN-TIME&", TextCompare: false) != 0)
							{
								Label1.Text = General_Procedures.Get_UI("filecsv1");
								chkprompt.Visible = true;
							}
							else
							{
								Label1.Text = General_Procedures.Get_UI("filecsv2");
								chkprompt.Visible = false;
								ToolTip1.SetToolTip(CmdCancel, "Cancel Query");
								cmbftype.Text = "tab";
							}
							Text = "Enter Output File/Table";
							ComboBox MyCombo = mskFileName;
							BuildForm.Add_Shares(ref MyCombo);
							mskFileName = MyCombo;
							mskFileName.Items.Add("\\\\rf3p-nas2-discovery.rf3prod.mfg.intel.com\\PCSA\\");
							mskFileName.Items.Add("<<<%username%>>>_t1");
							break;
						}
						case "I":
							if (Operators.CompareString(Strings.UCase(Globals_Renamed.currvaluetmp), "&RUN-TIME&", TextCompare: false) == 0)
							{
								mskFileName.Text = "";
								Label1.Text = General_Procedures.Get_UI("filein");
								chkprompt.Visible = false;
								ToolTip1.SetToolTip(CmdCancel, "Cancel the Query being run");
							}
							Text = "Enter a Text File Name Containing Item List";
							cmdBrowse.Visible = true;
							break;
						case "V":
							cmdBrowse.Visible = true;
							chkprompt.Visible = false;
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Database_Node_Plus(ref Globals_Renamed.currDataAny, ref ll_DBType, ref ll_ObjectType);
								mskFileName.Text = "";
							}
							else
							{
								num3 = (short)Strings.InStr(Strings.UCase(Globals_Renamed.currvaluetmp), ".ILV");
								if (num3 != 0)
								{
									mskFileName.Text = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
								}
								else
								{
									mskFileName.Text = Globals_Renamed.currvaluetmp;
								}
							}
							Label1.Text = General_Procedures.Get_UI("fileilv");
							Text = "Enter Inline View";
							break;
						case "N":
							ToolTip1.SetToolTip(cmdBrowse, "Get Help on Nodes");
							cmdBrowse.Visible = true;
							chkprompt.Visible = false;
							text = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 1));
							if (Operators.CompareString(text, "", TextCompare: false) == 0)
							{
								text = "5";
							}
							ll_DBType = text;
							if ((Operators.CompareString(ll_DBType, "9", TextCompare: false) != 0 || !LikeOperator.LikeString(Strings.UCase(Globals_Renamed.currvaluetmp), "*.SDB", CompareMethod.Binary)) && !LikeOperator.LikeString(Strings.UCase(Globals_Renamed.currvaluetmp), "DUCKDB:*", CompareMethod.Binary))
							{
								cmdBrowse.Text = "Nodes";
							}
							mskFileName.Text = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 3));
							Label1.Text = General_Procedures.Get_UI("filedb");
							Text = "Specify Database";
							break;
						case "M":
							mskFileName.Items.Add("0");
							mskFileName.Items.Add("0.0");
							Label1.Text = General_Procedures.Get_UI("filect");
							Text = "Pivot Missing Value";
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							mskFileName.Text = Strings.Trim(Globals_Renamed.currvaluetmp);
							break;
						case "K":
							Text = "Mongo Query Option";
							switch (Globals_Renamed.currinputstrtmp)
							{
							case "K1":
								mskFileName.Items.Add("10000");
								Label1.Text = General_Procedures.Get_UI("filem1");
								break;
							case "K2":
								mskFileName.Items.Add("10000");
								Label1.Text = General_Procedures.Get_UI("filem2");
								break;
							case "K3":
								Label1.Text = General_Procedures.Get_UI("filem3");
								mskFileName.Items.Add("10");
								mskFileName.Items.Add("25");
								mskFileName.Items.Add("50");
								break;
							}
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							mskFileName.Text = Strings.Trim(Globals_Renamed.currvaluetmp);
							break;
						case "H":
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							mskFileName.Items.Add("MERGE (a0)");
							mskFileName.Items.Add("MERGE (a1)");
							mskFileName.Items.Add("MERGE (a0) MERGE(a1)");
							mskFileName.Items.Add("mapjoin (a1)");
							mskFileName.Text = Strings.Trim(Globals_Renamed.currvaluetmp);
							Label1.Text = General_Procedures.Get_UI("filehint");
							Text = "Type in a Database Hint";
							break;
						case "Y":
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							num4 = 1;
							do
							{
								mskFileName.Items.Add(Conversions.ToString(unchecked((int)num4)));
								num4 = (short)unchecked(num4 + 1);
							}
							while (num4 <= 9);
							mskFileName.DropDownStyle = ComboBoxStyle.DropDownList;
							text = Strings.Trim(Globals_Renamed.currvaluetmp);
							unchecked
							{
								num4 = (short)((Operators.CompareString(text, "", TextCompare: false) == 0 || Strings.Len(text) > 1 || ((Operators.CompareString(text, "1", TextCompare: false) < 0) & (Operators.CompareString(text, "9", TextCompare: false) > 0))) ? 1 : checked((short)Conversions.ToInteger(text)));
							}
							mskFileName.SelectedIndex = num4 - 1;
							Label1.Text = "Enter the number of display objects you wish shown per row on this layout. The value should be between 1 and 9";
							Text = "Layout Items Per Row";
							break;
						case "O":
						{
							string[] array = Strings.Split(Globals_Renamed.currvaluetmp, "</@#;>");
							mskFileName.DropDownStyle = ComboBoxStyle.DropDownList;
							ComboBox MyCombo = mskFileName;
							BuildChart.Load_Items_To_Combo(ref MyCombo, array[2], array[0]);
							mskFileName = MyCombo;
							chkprompt.Visible = false;
							cmbftype.Left = mskFileName.Left;
							cmbftype.Width = mskFileName.Width;
							cmbftype.Items.Clear();
							MyCombo = cmbftype;
							BuildChart.Load_Items_To_Combo(ref MyCombo, array[3], array[1]);
							cmbftype = MyCombo;
							array = null;
							cmbftype.Visible = true;
							Label1.Text = "Enter the Layout Object to process in the first drop down box and the display object (e.g., Chart) to append to the layout in the second drop down box";
							Text = "Append To Layout";
							base.Height = 330;
							break;
						}
						case "A":
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "ASDB", TextCompare: false) == 0)
							{
								chkUnique.CheckState = CheckState.Unchecked;
								chkUnique.Visible = false;
								cmdBrowse.Visible = true;
								num3 = (short)Strings.InStr(Globals_Renamed.currvaluetmp, ":");
							}
							else
							{
								chkUnique.Text = "Auto Recommend a MARS Schema";
								chkUnique.Visible = true;
								mskFileName.Items.Clear();
								mskFileName.Items.Add("Default");
								short noschemas = Globals_Renamed.noschemas;
								for (num4 = 0; num4 <= noschemas; num4 = (short)unchecked(num4 + 1))
								{
									mskFileName.Items.Add(Globals_Renamed.AllSchema[num4].Name);
								}
								chkUnique.CheckState = CheckState.Checked;
								cmdBrowse.Visible = false;
								num3 = (short)Strings.InStrRev(Globals_Renamed.currvaluetmp, ":");
							}
							chkprompt.Visible = false;
							if (num3 != 0)
							{
								text = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, num3 + 1));
								mskFileName.Text = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1));
							}
							else
							{
								text = "";
								mskFileName.Text = Strings.Trim(Globals_Renamed.currvaluetmp);
							}
							chkUnique.Tag = text;
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "ASDB", TextCompare: false) == 0)
							{
								Label1.Text = "Enter the SQLite Table to Use in the SQLite Database.";
								Text = "SQLite Table";
							}
							else
							{
								text = ((Operators.CompareString(text, "", TextCompare: false) != 0) ? ("node " + text) : "the query");
								Label1.Text = "Enter MARS Schema for " + text + ". This is relevant for A/T where data is stored in different schemas. E.g., in ATD, CPU data is stored in A43_PROD_0. Check Auto Recommend MARS Schema to have SQLPathFinder determine the most likely schema.";
								Text = "Specify a MARS Database Schema Name";
							}
							break;
						case "T":
							cmdBrowse.Visible = false;
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								mskFileName.Text = "";
							}
							else
							{
								num3 = (short)Strings.InStr(Strings.UCase(Globals_Renamed.currvaluetmp), ".TAB");
								if (num3 != 0)
								{
									mskFileName.Text = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
								}
								else
								{
									mskFileName.Text = Globals_Renamed.currvaluetmp;
								}
							}
							Label1.Text = General_Procedures.Get_UI("filetemp");
							chkprompt.Visible = false;
							Text = "Enter Temporary Table Label";
							break;
						case "S":
						{
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							string[] MyTableArr = null;
							mskFileName.DropDownStyle = ComboBoxStyle.DropDownList;
							flag = true;
							ComboBox MyCombo;
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "SR", TextCompare: false) == 0)
							{
								MyCombo = mskFileName;
								BuildChart.Load_JMP_Tables_To_Combo(ref MyCombo, ref MyTableArr, "R");
								mskFileName = MyCombo;
							}
							else
							{
								MyCombo = mskFileName;
								BuildChart.Load_JMP_Tables_To_Combo(ref MyCombo, ref MyTableArr, "JMP");
								mskFileName = MyCombo;
							}
							MyCombo = mskFileName;
							BuildChart.Update_Combo_Chart_Table(ref MyCombo, Globals_Renamed.currvaluetmp);
							mskFileName = MyCombo;
							Label1.Text = "Choose the Chart table to act upon";
							Text = "Choose a Chart Table";
							MyTableArr = null;
							break;
						}
						case "Z":
							cmdBrowse.Visible = true;
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							Label1.Text = "Enter a Temporary Macro File to translate Macros Variables of format <<<macroname>>>. The file must be a CSV file with column headers equal to the MacroName and column values equal to the translation. Can be used to debug Query Segments which need a Macro File";
							chkprompt.Visible = false;
							Text = "Enter Macro File";
							break;
						case "B":
							cmdBrowse.Visible = true;
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							Label1.Text = "Enter File to write output to.";
							chkprompt.Visible = false;
							Text = "Enter File Name";
							break;
						case "W":
						{
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							mskFileName.DropDownStyle = ComboBoxStyle.DropDownList;
							flag = true;
							ComboBox MyCombo = mskFileName;
							BuildChart.Load_Items_To_Combo(ref MyCombo, Strings.Trim(Strings.Mid(Globals_Renamed.currinputstrtmp + " ", 2)), Globals_Renamed.currvaluetmp);
							mskFileName = MyCombo;
							Label1.Text = "Choose a Results Window to act upon";
							Text = "Choose a Results Window";
							break;
						}
						case "PARALLEL":
							mskFileName.Items.Clear();
							num4 = 0;
							do
							{
								mskFileName.Items.Add(Conversion.Str(num4));
								num4 = (short)unchecked(num4 + 1);
							}
							while (num4 <= 9);
							Label1.Text = General_Procedures.Get_UI("parallellabel");
							Text = "Parallel Threads";
							cmdBrowse.Visible = false;
							chkprompt.Visible = false;
							mskFileName.Text = Strings.Trim(Globals_Renamed.currvaluetmp);
							break;
						case "P":
						case "U":
							chkUnique.Visible = false;
							chkprompt.Visible = false;
							cmdBrowse.Visible = true;
							cmbftype.Items.Clear();
							if (Operators.CompareString(MyType, "P", TextCompare: false) == 0)
							{
								cmbftype.Text = "PPT";
								cmbftype.Items.Add("PPT");
							}
							else
							{
								cmbftype.Text = "JSL";
								cmbftype.Items.Add("JSL");
							}
							cmbftype.Text = Conversions.ToString(cmbftype.Items[0]);
							lblftype.Visible = true;
							cmbftype.Visible = true;
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "U", TextCompare: false) == 0)
							{
								Text = "Enter JSL Path";
								Label1.Text = "Enter Path to a JMP Scripting Language file (.JSL) you wish to run. Enter nothing and click OK to clear your setting";
							}
							else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "PPT", TextCompare: false) == 0)
							{
								Text = "Enter PowerPoint File";
								Label1.Text = "Enter a PowerPoint Output File to store elements from a journal. Enter nothing and click OK to clear your setting";
							}
							else
							{
								Text = "Enter PowerPoint Template";
								Label1.Text = "Enter an optional PowerPoint Template File containing textless, rectangular shapes to hold Journal elements. Enter nothing and click OK to clear your setting";
							}
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							break;
						case "C":
						{
							chkUnique.Visible = false;
							chkprompt.Visible = false;
							cmdBrowse.Visible = true;
							cmbftype.Items.Clear();
							cmbftype.Text = "1";
							mskFileName.Text = "";
							cmbftype.Left = lblftype.Left;
							lblftype.Top = chkUnique.Top;
							ComboBox comboBox = new ComboBox();
							Label label = new Label();
							comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
							comboBox.Top = cmbftype.Top;
							comboBox.Left = cmbftype.Left + cmbftype.Width + 20;
							label.Top = lblftype.Top;
							label.Left = comboBox.Left;
							label.Font = new Font(label.Font, FontStyle.Regular);
							comboBox.Font = new Font(comboBox.Font, FontStyle.Regular);
							label.Text = "Col Span";
							lblftype.Text = "Images Per Row";
							label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
							comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
							ToolTip1.SetToolTip(comboBox, "Set the Column Span for a Single Image");
							ToolTip1.SetToolTip(cmbftype, "Set the Number of Images to display per row if an image pattern is specified");
							cmbgtype = comboBox;
							label.AutoSize = true;
							num4 = 1;
							do
							{
								unchecked
								{
									cmbftype.Items.Add(Conversions.ToString((int)num4));
									comboBox.Items.Add(Conversions.ToString((int)num4));
								}
								num4 = (short)unchecked(num4 + 1);
							}
							while (num4 <= 9);
							cmbftype.SelectedIndex = 0;
							comboBox.SelectedIndex = 0;
							lblftype.Visible = true;
							cmbftype.Visible = true;
							base.Controls.Add(comboBox);
							base.Controls.Add(label);
							Text = "Enter Image Path";
							Label1.Text = General_Procedures.Get_UI("imgpath");
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								break;
							}
							num4 = 1;
							do
							{
								text = ":";
								if (num4 == 2)
								{
									text = "+";
								}
								num3 = (short)Strings.InStrRev(Globals_Renamed.currvaluetmp, text);
								if (num3 != 0)
								{
									string text3 = Strings.Trim(Strings.LCase(Strings.Mid(Globals_Renamed.currvaluetmp + " ", num3 + 1)));
									Globals_Renamed.currvaluetmp = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
									if (Versioned.IsNumeric(text3) && ((Conversions.ToInteger(text3) >= 1) & (Conversions.ToInteger(text3) <= 9)))
									{
										if (num4 == 1)
										{
											comboBox.SelectedIndex = Conversions.ToInteger(text3) - 1;
										}
										else
										{
											cmbftype.SelectedIndex = Conversions.ToInteger(text3) - 1;
										}
									}
								}
								num4 = (short)unchecked(num4 + 1);
							}
							while (num4 <= 2);
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							break;
						}
						case "D":
							chkUnique.Visible = false;
							chkprompt.Visible = false;
							cmdBrowse.Visible = true;
							cmbftype.Items.Clear();
							cmbftype.Text = "1";
							mskFileName.Text = "";
							cmbftype.Left = lblftype.Left;
							lblftype.Top = chkUnique.Top;
							lblftype.Text = "Col Span";
							ToolTip1.SetToolTip(cmbftype, "Set the Column Span for the HTML Report");
							num4 = 1;
							do
							{
								cmbftype.Items.Add(Conversions.ToString(unchecked((int)num4)));
								num4 = (short)unchecked(num4 + 1);
							}
							while (num4 <= 9);
							cmbftype.SelectedIndex = 0;
							lblftype.Visible = true;
							cmbftype.Visible = true;
							Text = "Enter JMP HTML Path";
							Label1.Text = General_Procedures.Get_UI("jmp-htmlpath");
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								break;
							}
							num3 = (short)Strings.InStrRev(Globals_Renamed.currvaluetmp, ":");
							if (num3 != 0)
							{
								string text3 = Strings.Trim(Strings.LCase(Strings.Mid(Globals_Renamed.currvaluetmp + " ", num3 + 1)));
								Globals_Renamed.currvaluetmp = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
								if (Versioned.IsNumeric(text3) && ((Conversions.ToInteger(text3) >= 1) & (Conversions.ToInteger(text3) <= 9)))
								{
									cmbftype.SelectedIndex = Conversions.ToInteger(text3) - 1;
								}
							}
							mskFileName.Text = Globals_Renamed.currvaluetmp;
							break;
						case "F":
							chkUnique.Visible = false;
							chkprompt.Visible = false;
							cmbftype.Items.Clear();
							if (Operators.CompareString(L_Mode, "Q", TextCompare: false) == 0)
							{
								Label1.Text = "Either specify a Function file (option Function File) or a File containing Field Patterns and Types (option Regex File). For the latter the file should contain column headers. E.g.,\r\npattern,type\r\n^ll#opinfo,KS:Unit";
								cmbftype.Items.Add("Regex File");
								cmbftype.Items.Add("Function File");
								ToolTip1.SetToolTip(cmbftype, "Choose File Type");
								lblftype.Text = "File Type";
								Text = "Select File";
							}
							else
							{
								cmbftype.Items.Add("Starts With");
								cmbftype.Items.Add("Ends With");
								cmbftype.Items.Add("Contains");
								cmbftype.Items.Add("Not Starts With");
								cmbftype.Items.Add("In File");
								cmbftype.Items.Add("Starts/Ends With (%)");
								cmbftype.Items.Add("Regex");
								if (Operators.CompareString(text2, "C", TextCompare: false) == 0)
								{
									cmbftype.Items.Add("Regex File");
									cmbftype.Items.Add("Function File");
									cmbftype.Items.Add("Not Regex");
									cmbftype.Items.Add("Not Regex File");
								}
								Label1.Text = "Enter a column pattern for your query as well as the search operator. For search operator \"Starts/Ends With\" , separate the starting and ending patterns with a % wildcard";
								ToolTip1.SetToolTip(cmbftype, "Choose a Column Name Search Operator");
								lblftype.Text = "Pattern Operator";
								Text = "Select Column Pattern";
							}
							cmdBrowse.Visible = true;
							mskFileName.Text = "";
							cmbftype.Left = lblftype.Left;
							lblftype.Top = chkUnique.Top;
							cmbftype.Width = mskFileName.Width;
							cmbftype.SelectedIndex = 0;
							lblftype.Visible = true;
							cmbftype.Visible = true;
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0)
							{
								string text3 = "";
								if (Operators.CompareString(L_Mode, "Q", TextCompare: false) == 0 && Strings.Len(Globals_Renamed.currvaluetmp) >= 2)
								{
									text3 = ((Operators.CompareString(Strings.UCase(Strings.Mid(Globals_Renamed.currvaluetmp, 1, 1)), "R", TextCompare: false) != 0) ? "Function File" : "Regex File");
									Globals_Renamed.currvaluetmp = Strings.Mid(Globals_Renamed.currvaluetmp, 2);
									cmbftype.Enabled = false;
								}
								else
								{
									num3 = (short)Strings.InStr(Globals_Renamed.currvaluetmp, ";");
									if (num3 != 0)
									{
										text3 = Strings.Trim(Strings.LCase(Strings.Mid(Globals_Renamed.currvaluetmp + " ", num3 + 1)));
										Globals_Renamed.currvaluetmp = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
									}
								}
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									short num6 = (short)(cmbftype.Items.Count - 1);
									for (num4 = 0; num4 <= num6; num4 = (short)unchecked(num4 + 1))
									{
										if (Operators.CompareString(Strings.LCase(text3), Strings.LCase(cmbftype.Items[num4].ToString()), TextCompare: false) == 0)
										{
											cmbftype.SelectedIndex = num4;
											break;
										}
									}
								}
								mskFileName.Text = Globals_Renamed.currvaluetmp;
							}
							else if (Operators.CompareString(text2, "C", TextCompare: false) == 0)
							{
								cmbftype.SelectedIndex = 2;
							}
							if (Operators.CompareString(L_Mode, "Q", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.currDataAny, "", TextCompare: false) != 0)
							{
								ComboBox MyCombo = mskFileName;
								BuildChart.Load_Items_To_Combo(ref MyCombo, Globals_Renamed.currDataAny, "");
								mskFileName = MyCombo;
							}
							break;
						case "J":
							chkUnique.Visible = false;
							chkprompt.Visible = false;
							cmdBrowse.Visible = true;
							cmbftype.Items.Clear();
							if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "JMP2", TextCompare: false) == 0)
							{
								cmbftype.Items.Add("CSV");
								cmbftype.Items.Add("JMP");
								Label1.Text = General_Procedures.Get_UI("filejrn2");
							}
							else if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "JR", TextCompare: false) == 0)
							{
								cmbftype.Items.Add("TAB");
								cmbftype.Items.Add("CSV");
								cmbftype.Items.Add("RDATA");
								Label1.Text = General_Procedures.Get_UI("filejrnr");
							}
							else
							{
								cmbftype.Items.Add("JRN");
								cmbftype.Items.Add("HTM");
								cmbftype.Items.Add("DOC");
								cmbftype.Items.Add("RTF");
								cmbftype.Items.Add("PNG");
								cmbftype.Items.Add("TXT");
								Label1.Text = General_Procedures.Get_UI("filejrn");
							}
							Text = "Enter Output File Name";
							cmbftype.Visible = true;
							lblftype.Visible = true;
							lblftype.Top = chkUnique.Top;
							cmbftype.Top = chkUnique.Top;
							base.Height = 330;
							if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
							{
								mskFileName.Text = "";
								cmbftype.Text = Conversions.ToString(cmbftype.Items[0]);
							}
							else
							{
								num3 = (short)Strings.InStrRev(Strings.UCase(Globals_Renamed.currvaluetmp), ".");
								if (num3 != 0)
								{
									mskFileName.Text = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
									cmbftype.Text = Strings.Trim(Strings.LCase(Strings.Mid(Globals_Renamed.currvaluetmp + " ", num3 + 1)));
								}
								else
								{
									mskFileName.Text = Globals_Renamed.currvaluetmp;
									cmbftype.Text = Conversions.ToString(cmbftype.Items[0]);
								}
							}
							mskFileName.Items.Add("\\\\rf3p-nas2-discovery.rf3prod.mfg.intel.com\\PCSA\\");
							break;
						default:
							MyType = "";
							break;
						}
						if (!flag & (Operators.CompareString(mskFileName.Text, "", TextCompare: false) != 0))
						{
							mskFileName.SelectionStart = 0;
							mskFileName.SelectionLength = Strings.Len(mskFileName.Text);
						}
						Globals_Renamed.currvaluetmp = "CANCEL";
						if (Operators.CompareString(MyType, "", TextCompare: false) == 0)
						{
							Close();
						}
						goto end_IL_0001;
					}
					case 10854:
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
					goto IL_2a9c;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 10854;
				continue;
			}
			break;
			IL_2a9c:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void frmfilename_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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
				Dispose();
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

	private void CmdOK_MouseDown(object sender, MouseEventArgs e)
	{
	}

	private void frmfilename_MouseHover(object sender, EventArgs e)
	{
	}
}

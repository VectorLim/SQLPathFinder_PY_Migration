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
internal class FrmNode : Form
{
	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdHelp")]
	private Button _cmdHelp;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdNode0")]
	private Button _CmdNode0;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdNode1")]
	private Button _CmdNode1;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdNode2")]
	private Button _CmdNode2;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdNode3")]
	private Button _CmdNode3;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdGo")]
	private Button _cmdGo;

	[field: AccessedThroughProperty("cmbNode3")]
	public virtual ComboBox cmbNode3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual Button cmdHelp
	{
		[CompilerGenerated]
		get
		{
			return _cmdHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHelp_Click;
			Button button = _cmdHelp;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdHelp = value;
			button = _cmdHelp;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmbNode2")]
	public virtual ComboBox cmbNode2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbNode1")]
	public virtual ComboBox cmbNode1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmbNode0")]
	public virtual ComboBox cmbNode0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("Label6")]
	public virtual Label Label6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label3")]
	public virtual Label Label3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblOASys")]
	public virtual Label LblOASys
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LblARIES")]
	public virtual Label LblARIES
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button CmdNode0
	{
		[CompilerGenerated]
		get
		{
			return _CmdNode0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdNode0_Click;
			Button button = _CmdNode0;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdNode0 = value;
			button = _CmdNode0;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdNode1
	{
		[CompilerGenerated]
		get
		{
			return _CmdNode1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdNode0_Click;
			Button button = _CmdNode1;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdNode1 = value;
			button = _CmdNode1;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdNode2
	{
		[CompilerGenerated]
		get
		{
			return _CmdNode2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdNode0_Click;
			Button button = _CmdNode2;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdNode2 = value;
			button = _CmdNode2;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual Button CmdNode3
	{
		[CompilerGenerated]
		get
		{
			return _CmdNode3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdNode0_Click;
			Button button = _CmdNode3;
			if (button != null)
			{
				button.Click -= value2;
			}
			_CmdNode3 = value;
			button = _CmdNode3;
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

	[field: AccessedThroughProperty("cmbFacility")]
	internal virtual ComboBox cmbFacility
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button cmdGo
	{
		[CompilerGenerated]
		get
		{
			return _cmdGo;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGo_Click;
			Button button = _cmdGo;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdGo = value;
			button = _cmdGo;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[DebuggerNonUserCode]
	public FrmNode()
	{
		base.Load += FrmNode_Load;
		base.FormClosed += FrmNode_FormClosed;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmNode));
		this.CmdNode0 = new System.Windows.Forms.Button();
		this.CmdNode1 = new System.Windows.Forms.Button();
		this.CmdNode2 = new System.Windows.Forms.Button();
		this.CmdNode3 = new System.Windows.Forms.Button();
		this.cmdGo = new System.Windows.Forms.Button();
		this.cmbNode3 = new System.Windows.Forms.ComboBox();
		this.cmdHelp = new System.Windows.Forms.Button();
		this.cmbNode2 = new System.Windows.Forms.ComboBox();
		this.cmbNode1 = new System.Windows.Forms.ComboBox();
		this.cmbNode0 = new System.Windows.Forms.ComboBox();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.CmdOK = new System.Windows.Forms.Button();
		this.Label6 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.LblOASys = new System.Windows.Forms.Label();
		this.LblARIES = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.cmbFacility = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.CmdNode0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdNode0.ImageIndex = 0;
		this.CmdNode0.Location = new System.Drawing.Point(409, 56);
		this.CmdNode0.Name = "CmdNode0";
		this.CmdNode0.Size = new System.Drawing.Size(25, 23);
		this.CmdNode0.TabIndex = 1;
		this.CmdNode0.Tag = "0";
		this.CmdNode0.UseVisualStyleBackColor = true;
		this.CmdNode1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdNode1.ImageIndex = 0;
		this.CmdNode1.Location = new System.Drawing.Point(409, 116);
		this.CmdNode1.Name = "CmdNode1";
		this.CmdNode1.Size = new System.Drawing.Size(25, 23);
		this.CmdNode1.TabIndex = 3;
		this.CmdNode1.Tag = "3";
		this.CmdNode1.UseVisualStyleBackColor = true;
		this.CmdNode2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdNode2.ImageIndex = 0;
		this.CmdNode2.Location = new System.Drawing.Point(409, 176);
		this.CmdNode2.Name = "CmdNode2";
		this.CmdNode2.Size = new System.Drawing.Size(25, 23);
		this.CmdNode2.TabIndex = 5;
		this.CmdNode2.Tag = "2";
		this.CmdNode2.UseVisualStyleBackColor = true;
		this.CmdNode3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdNode3.ImageIndex = 0;
		this.CmdNode3.Location = new System.Drawing.Point(409, 236);
		this.CmdNode3.Name = "CmdNode3";
		this.CmdNode3.Size = new System.Drawing.Size(25, 23);
		this.CmdNode3.TabIndex = 7;
		this.CmdNode3.Tag = "5";
		this.CmdNode3.UseVisualStyleBackColor = true;
		this.cmdGo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdGo.ImageIndex = 0;
		this.cmdGo.Location = new System.Drawing.Point(409, 306);
		this.cmdGo.Name = "cmdGo";
		this.cmdGo.Size = new System.Drawing.Size(49, 23);
		this.cmdGo.TabIndex = 9;
		this.cmdGo.Tag = "5";
		this.cmdGo.Text = "Go";
		this.cmdGo.UseVisualStyleBackColor = true;
		this.cmbNode3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbNode3.BackColor = System.Drawing.SystemColors.Window;
		this.cmbNode3.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbNode3.DropDownWidth = 200;
		this.cmbNode3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNode3.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbNode3.Location = new System.Drawing.Point(136, 236);
		this.cmbNode3.Name = "cmbNode3";
		this.cmbNode3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbNode3.Size = new System.Drawing.Size(256, 24);
		this.cmbNode3.Sorted = true;
		this.cmbNode3.TabIndex = 6;
		this.cmbNode3.Tag = "";
		this.cmbNode3.Text = "None";
		this.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdHelp.BackColor = System.Drawing.SystemColors.Control;
		this.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmdHelp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmdHelp.Location = new System.Drawing.Point(177, 378);
		this.cmdHelp.Name = "cmdHelp";
		this.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmdHelp.Size = new System.Drawing.Size(65, 34);
		this.cmdHelp.TabIndex = 12;
		this.cmdHelp.Text = "Help";
		this.cmdHelp.UseVisualStyleBackColor = false;
		this.cmbNode2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbNode2.BackColor = System.Drawing.SystemColors.Window;
		this.cmbNode2.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbNode2.DropDownWidth = 200;
		this.cmbNode2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNode2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbNode2.Location = new System.Drawing.Point(136, 176);
		this.cmbNode2.Name = "cmbNode2";
		this.cmbNode2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbNode2.Size = new System.Drawing.Size(256, 24);
		this.cmbNode2.Sorted = true;
		this.cmbNode2.TabIndex = 4;
		this.cmbNode2.Tag = "";
		this.cmbNode2.Text = "None";
		this.cmbNode1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbNode1.BackColor = System.Drawing.SystemColors.Window;
		this.cmbNode1.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbNode1.DropDownWidth = 200;
		this.cmbNode1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNode1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbNode1.Location = new System.Drawing.Point(136, 116);
		this.cmbNode1.Name = "cmbNode1";
		this.cmbNode1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbNode1.Size = new System.Drawing.Size(256, 24);
		this.cmbNode1.Sorted = true;
		this.cmbNode1.TabIndex = 2;
		this.cmbNode1.Tag = "";
		this.cmbNode1.Text = "None";
		this.cmbNode0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbNode0.BackColor = System.Drawing.SystemColors.Window;
		this.cmbNode0.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbNode0.DropDownWidth = 200;
		this.cmbNode0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNode0.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cmbNode0.Location = new System.Drawing.Point(136, 56);
		this.cmbNode0.Name = "cmbNode0";
		this.cmbNode0.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.cmbNode0.Size = new System.Drawing.Size(256, 24);
		this.cmbNode0.Sorted = true;
		this.cmbNode0.TabIndex = 0;
		this.cmbNode0.Tag = "";
		this.cmbNode0.Text = "None";
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmdCancel.BackColor = System.Drawing.SystemColors.Control;
		this.CmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdCancel.Location = new System.Drawing.Point(95, 378);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdCancel.Size = new System.Drawing.Size(65, 34);
		this.CmdCancel.TabIndex = 11;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = false;
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CmdOK.BackColor = System.Drawing.SystemColors.Control;
		this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.CmdOK.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.CmdOK.Location = new System.Drawing.Point(13, 378);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CmdOK.Size = new System.Drawing.Size(65, 34);
		this.CmdOK.TabIndex = 10;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = false;
		this.Label6.AutoSize = true;
		this.Label6.BackColor = System.Drawing.SystemColors.Control;
		this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label6.Location = new System.Drawing.Point(13, 236);
		this.Label6.Name = "Label6";
		this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label6.Size = new System.Drawing.Size(44, 16);
		this.Label6.TabIndex = 24;
		this.Label6.Text = "Other";
		this.Label3.AutoSize = true;
		this.Label3.BackColor = System.Drawing.SystemColors.Control;
		this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
		this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label3.Location = new System.Drawing.Point(13, 56);
		this.Label3.Name = "Label3";
		this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Label3.Size = new System.Drawing.Size(94, 16);
		this.Label3.TabIndex = 19;
		this.Label3.Text = "MARS / XEUS";
		this.LblOASys.AutoSize = true;
		this.LblOASys.BackColor = System.Drawing.SystemColors.Control;
		this.LblOASys.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblOASys.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblOASys.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblOASys.Location = new System.Drawing.Point(13, 176);
		this.LblOASys.Name = "LblOASys";
		this.LblOASys.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblOASys.Size = new System.Drawing.Size(98, 16);
		this.LblOASys.TabIndex = 21;
		this.LblOASys.Text = "OASys / XEUS";
		this.LblARIES.AutoSize = true;
		this.LblARIES.BackColor = System.Drawing.SystemColors.Control;
		this.LblARIES.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblARIES.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblARIES.ForeColor = System.Drawing.SystemColors.ControlText;
		this.LblARIES.Location = new System.Drawing.Point(13, 116);
		this.LblARIES.Name = "LblARIES";
		this.LblARIES.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.LblARIES.Size = new System.Drawing.Size(95, 16);
		this.LblARIES.TabIndex = 20;
		this.LblARIES.Text = "ARIES / XEUS";
		this.Label1.Location = new System.Drawing.Point(13, 306);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(117, 52);
		this.Label1.TabIndex = 25;
		this.Label1.Text = "Set Nodes for";
		this.cmbFacility.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFacility.FormattingEnabled = true;
		this.cmbFacility.Items.AddRange(new object[21]
		{
			"ATD", "CD", "CRTO", "D1C", "D1D", "SPTD", "F21-NMDM FAB", "F21 (F9)-NMDM DP/WLA", "F24", "F26-PGDM FAB",
			"F26-PGDM DP/WLA", "F28", "F32", "F68", "IMO", "KM", "KM8", "OWLA", "PG", "T72",
			"VN"
		});
		this.cmbFacility.Location = new System.Drawing.Point(136, 306);
		this.cmbFacility.Name = "cmbFacility";
		this.cmbFacility.Size = new System.Drawing.Size(256, 24);
		this.cmbFacility.TabIndex = 8;
		base.AcceptButton = this.CmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(470, 424);
		base.Controls.Add(this.cmdGo);
		base.Controls.Add(this.cmbFacility);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.CmdNode3);
		base.Controls.Add(this.CmdNode2);
		base.Controls.Add(this.CmdNode1);
		base.Controls.Add(this.CmdNode0);
		base.Controls.Add(this.cmbNode3);
		base.Controls.Add(this.cmdHelp);
		base.Controls.Add(this.cmbNode2);
		base.Controls.Add(this.cmbNode1);
		base.Controls.Add(this.cmbNode0);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Controls.Add(this.Label6);
		base.Controls.Add(this.Label3);
		base.Controls.Add(this.LblOASys);
		base.Controls.Add(this.LblARIES);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Location = new System.Drawing.Point(3, 29);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmNode";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Set Default Nodes";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	~FrmNode()
	{
		base.Finalize();
	}

	private void CmdCancel_Click(object eventSender, EventArgs eventArgs)
	{
		Close();
	}

	private void cmdHelp_Click(object eventSender, EventArgs eventArgs)
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
				BuildForm.Invoke_IE("https://wiki.ith.intel.com/display/SQLPathFinder/Setting+Database+Nodes");
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

	private void cmdOK_Click(object eventSender, EventArgs eventArgs)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string lpKeyName = default(string);
		string text = default(string);
		short num6 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string NodeList;
				ComboBox comboBox;
				bool num5;
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
						case 3:
							goto IL_0019;
						case 4:
							goto IL_0022;
						case 6:
							goto IL_0053;
						case 7:
							goto IL_005d;
						case 8:
							goto IL_0062;
						case 9:
							goto IL_0074;
						case 10:
							goto IL_0082;
						case 12:
							goto IL_0090;
						case 11:
						case 13:
						case 14:
							goto IL_00aa;
						case 15:
							goto IL_00bd;
						case 16:
							goto IL_00d0;
						case 17:
							goto IL_00e3;
						case 18:
							goto IL_00f6;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d0:
					num2 = 16;
					Globals_Renamed.MyOASysServer = cmbNode2.Text;
					goto IL_00e3;
					IL_00e3:
					num2 = 17;
					Globals_Renamed.MyOtherServer = cmbNode3.Text;
					goto IL_00f6;
					IL_00bd:
					num2 = 15;
					Globals_Renamed.MyARIESServer = cmbNode1.Text;
					goto IL_00d0;
					IL_00f6:
					num2 = 18;
					BuildForm.Save_DTRBuild_Ini("C");
					break;
					IL_000b:
					num2 = 2;
					lpKeyName = cmbNode0.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					text = "";
					goto IL_0022;
					IL_0022:
					num2 = 4;
					NodeList = (comboBox = cmbNode3).Text;
					num5 = BuildForm.VerifyNodes(ref NodeList);
					comboBox.Text = NodeList;
					if (!num5)
					{
						goto end_IL_0001_3;
					}
					goto IL_0053;
					IL_0053:
					num2 = 6;
					text = BuildForm.Get_SchemaName_Ini(lpKeyName);
					goto IL_005d;
					IL_005d:
					num2 = 7;
					num6 = 0;
					goto IL_0062;
					IL_0062:
					num2 = 8;
					num6 = checked((short)Strings.InStr(text, "{"));
					goto IL_0074;
					IL_0074:
					num2 = 9;
					if (num6 == 0)
					{
						goto IL_0082;
					}
					goto IL_0090;
					IL_0082:
					num2 = 10;
					Globals_Renamed.MyDBSchema = text;
					goto IL_00aa;
					IL_0090:
					num2 = 12;
					Globals_Renamed.MyDBSchema = Strings.Trim(Strings.Mid(text, 1, checked(num6 - 1)));
					goto IL_00aa;
					IL_00aa:
					num2 = 14;
					Globals_Renamed.MyMARSServer = cmbNode0.Text;
					goto IL_00bd;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				Close();
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

	private void FrmNode_Load(object eventSender, EventArgs eventArgs)
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
					errsource = "frmNode - Form_Load";
					Button MyButton = CmdNode0;
					BuildForm.Set_Btn_Img(ref MyButton, "helpb");
					CmdNode0 = MyButton;
					MyButton = CmdNode1;
					BuildForm.Set_Btn_Img(ref MyButton, "helpb");
					CmdNode1 = MyButton;
					MyButton = CmdNode2;
					BuildForm.Set_Btn_Img(ref MyButton, "helpb");
					CmdNode2 = MyButton;
					MyButton = CmdNode3;
					BuildForm.Set_Btn_Img(ref MyButton, "helpb");
					CmdNode3 = MyButton;
					cmbNode0.Text = Globals_Renamed.MyMARSServer;
					cmbNode1.Text = Globals_Renamed.MyARIESServer;
					cmbNode2.Text = Globals_Renamed.MyOASysServer;
					cmbNode3.Text = Globals_Renamed.MyOtherServer;
					cmbFacility.SelectedIndex = 0;
					goto end_IL_0001;
				}
				case 252:
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
				try0001_dispatch = 252;
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

	private void FrmNode_FormClosed(object eventSender, FormClosedEventArgs eventArgs)
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

	private void CmdNode0_Click(object sender, EventArgs e)
	{
		string f_OutNode = "";
		object left = NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null);
		if (Operators.ConditionalCompareObjectEqual(left, 0, TextCompare: false))
		{
			f_OutNode = cmbNode0.Text;
		}
		else if (Operators.ConditionalCompareObjectEqual(left, 2, TextCompare: false))
		{
			f_OutNode = cmbNode2.Text;
		}
		else if (Operators.ConditionalCompareObjectEqual(left, 3, TextCompare: false))
		{
			f_OutNode = cmbNode1.Text;
		}
		else if (Operators.ConditionalCompareObjectEqual(left, 5, TextCompare: false))
		{
			f_OutNode = cmbNode3.Text;
		}
		FrmNodeSel frmNodeSel = new FrmNodeSel();
		frmNodeSel.f_OutNode = f_OutNode;
		frmNodeSel.f_DBType = Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null));
		frmNodeSel.f_Mode = 0;
		frmNodeSel.f_Pattern = "";
		frmNodeSel.ShowDialog();
		f_OutNode = frmNodeSel.f_OutNode;
		frmNodeSel.Dispose();
		switch (Conversions.ToInteger(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null)))
		{
		case 0:
			cmbNode0.Text = f_OutNode;
			break;
		case 2:
			cmbNode2.Text = f_OutNode;
			break;
		case 3:
			cmbNode1.Text = f_OutNode;
			break;
		case 5:
			cmbNode3.Text = f_OutNode;
			break;
		case 1:
		case 4:
			break;
		}
	}

	private void cmdGo_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string ONode = default(string);
		string ANode = default(string);
		string text = default(string);
		string MNode = default(string);
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
				case 382:
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
							goto IL_0022;
						case 5:
							goto IL_002b;
						case 6:
							goto IL_0034;
						case 7:
							goto IL_0039;
						case 8:
							goto IL_0058;
						case 10:
							goto IL_006d;
						case 11:
							goto IL_007d;
						case 12:
							goto IL_008e;
						case 13:
							goto IL_009f;
						case 14:
							goto IL_00b0;
						case 15:
							goto IL_00cf;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_009f:
					num2 = 13;
					cmbNode2.Text = ONode;
					goto IL_00b0;
					IL_00b0:
					num2 = 14;
					cmbNode3.Text = Strings.Trim(cmbNode3.Text);
					goto IL_00cf;
					IL_008e:
					num2 = 12;
					cmbNode1.Text = ANode;
					goto IL_009f;
					IL_00cf:
					num2 = 15;
					if (Operators.CompareString(cmbNode3.Text, "", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(cmbNode3.Text), "NONE", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text = cmbFacility.Text;
					goto IL_0019;
					IL_0019:
					num2 = 3;
					MNode = "";
					goto IL_0022;
					IL_0022:
					num2 = 4;
					ANode = "";
					goto IL_002b;
					IL_002b:
					num2 = 5;
					ONode = "";
					goto IL_0034;
					IL_0034:
					num2 = 6;
					num5 = 7;
					goto IL_0039;
					IL_0039:
					num2 = 7;
					num5 = (int)Interaction.MsgBox("Do you wish to initialize Nodes for facility " + text + "?", MsgBoxStyle.YesNoCancel, "Initialize Nodes?");
					goto IL_0058;
					IL_0058:
					num2 = 8;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_006d;
					IL_006d:
					num2 = 10;
					BuildForm.Assign_Facility_Node(text, ref MNode, ref ANode, ref ONode);
					goto IL_007d;
					IL_007d:
					num2 = 11;
					cmbNode0.Text = MNode;
					goto IL_008e;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				cmbNode3.Text = "All.SPEED";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 382;
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

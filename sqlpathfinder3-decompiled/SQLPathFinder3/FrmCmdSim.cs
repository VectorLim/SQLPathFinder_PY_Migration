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
public class FrmCmdSim : Form
{
	private delegate void InvokewithString(string text);

	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private ToolStripButton _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdRun")]
	private ToolStripButton _cmdRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdCancel")]
	private ToolStripButton _cmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdOut")]
	private ToolStripButton _cmdOut;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdExit")]
	private ToolStripButton _cmdExit;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuwhiteblue")]
	private ToolStripMenuItem _mnuwhiteblue;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnublackwhite")]
	private ToolStripMenuItem _mnublackwhite;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuwhiteblack")]
	private ToolStripMenuItem _mnuwhiteblack;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuRun")]
	private ToolStripMenuItem _mnuRun;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuCancel")]
	private ToolStripMenuItem _mnuCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuShow")]
	private ToolStripMenuItem _mnuShow;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuClear")]
	private ToolStripMenuItem _mnuClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnuwhitemedblue")]
	private ToolStripMenuItem _mnuwhitemedblue;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnublackteal")]
	private ToolStripMenuItem _mnublackteal;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("mnupurplegrey")]
	private ToolStripMenuItem _mnupurplegrey;

	private ProcessStartInfo psi;

	private Process cmd;

	[field: AccessedThroughProperty("TxtIn")]
	internal virtual ComboBox TxtIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblIn")]
	internal virtual Label lblIn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TxtResults")]
	internal virtual TextBox TxtResults
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblResults")]
	internal virtual Label lblResults
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
			EventHandler value2 = mnuClear_Click;
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

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdRun
	{
		[CompilerGenerated]
		get
		{
			return _cmdRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRun_Click;
			ToolStripButton toolStripButton = _cmdRun;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdRun = value;
			toolStripButton = _cmdRun;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	internal virtual ToolStripButton cmdCancel
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
			EventHandler value2 = mnuCancel_Click;
			ToolStripButton toolStripButton = _cmdCancel;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdCancel = value;
			toolStripButton = _cmdCancel;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdOut
	{
		[CompilerGenerated]
		get
		{
			return _cmdOut;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuShow_Click;
			ToolStripButton toolStripButton = _cmdOut;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdOut = value;
			toolStripButton = _cmdOut;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuSep3")]
	internal virtual ToolStripSeparator mnuSep3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripButton cmdExit
	{
		[CompilerGenerated]
		get
		{
			return _cmdExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExit_Click;
			ToolStripButton toolStripButton = _cmdExit;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			_cmdExit = value;
			toolStripButton = _cmdExit;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("OptionsToolStripMenuItem")]
	internal virtual ToolStripMenuItem OptionsToolStripMenuItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuKeepLog")]
	internal virtual ToolStripMenuItem mnuKeepLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnucolors")]
	internal virtual ToolStripMenuItem mnucolors
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuwhiteblue
	{
		[CompilerGenerated]
		get
		{
			return _mnuwhiteblue;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuwhiteblue;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuwhiteblue = value;
			toolStripMenuItem = _mnuwhiteblue;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnublackwhite
	{
		[CompilerGenerated]
		get
		{
			return _mnublackwhite;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnublackwhite;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnublackwhite = value;
			toolStripMenuItem = _mnublackwhite;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuwhiteblack
	{
		[CompilerGenerated]
		get
		{
			return _mnuwhiteblack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuwhiteblack;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuwhiteblack = value;
			toolStripMenuItem = _mnuwhiteblack;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuRun
	{
		[CompilerGenerated]
		get
		{
			return _mnuRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuRun_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuRun = value;
			toolStripMenuItem = _mnuRun;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuCancel
	{
		[CompilerGenerated]
		get
		{
			return _mnuCancel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuCancel_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuCancel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuCancel = value;
			toolStripMenuItem = _mnuCancel;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuShow
	{
		[CompilerGenerated]
		get
		{
			return _mnuShow;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuShow_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuShow;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuShow = value;
			toolStripMenuItem = _mnuShow;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual ToolStripMenuItem mnuClear
	{
		[CompilerGenerated]
		get
		{
			return _mnuClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuClear_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuClear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuClear = value;
			toolStripMenuItem = _mnuClear;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnuwhitemedblue
	{
		[CompilerGenerated]
		get
		{
			return _mnuwhitemedblue;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnuwhitemedblue;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnuwhitemedblue = value;
			toolStripMenuItem = _mnuwhitemedblue;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnublackteal
	{
		[CompilerGenerated]
		get
		{
			return _mnublackteal;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnublackteal;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnublackteal = value;
			toolStripMenuItem = _mnublackteal;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem mnupurplegrey
	{
		[CompilerGenerated]
		get
		{
			return _mnupurplegrey;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuwhiteblue_Click;
			ToolStripMenuItem toolStripMenuItem = _mnupurplegrey;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			_mnupurplegrey = value;
			toolStripMenuItem = _mnupurplegrey;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public FrmCmdSim()
	{
		base.FormClosing += FrmCmdSim_FormClosing;
		base.FormClosed += FrmCmdSim_FormClosed;
		base.Load += FrmCmdSim_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmCmdSim));
		this.TxtIn = new System.Windows.Forms.ComboBox();
		this.lblIn = new System.Windows.Forms.Label();
		this.TxtResults = new System.Windows.Forms.TextBox();
		this.lblResults = new System.Windows.Forms.Label();
		this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
		this.cmdClear = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdRun = new System.Windows.Forms.ToolStripButton();
		this.cmdCancel = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdOut = new System.Windows.Forms.ToolStripButton();
		this.mnuSep3 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdExit = new System.Windows.Forms.ToolStripButton();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuKeepLog = new System.Windows.Forms.ToolStripMenuItem();
		this.mnucolors = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuwhiteblue = new System.Windows.Forms.ToolStripMenuItem();
		this.mnublackwhite = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuwhiteblack = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuwhitemedblue = new System.Windows.Forms.ToolStripMenuItem();
		this.mnublackteal = new System.Windows.Forms.ToolStripMenuItem();
		this.mnupurplegrey = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCancel = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuShow = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStrip1.SuspendLayout();
		this.MenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TxtIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtIn.FormattingEnabled = true;
		this.TxtIn.Items.AddRange(new object[5] { "ipconfig", "dir", "set", "dir c:\\ /s", "notepad " });
		this.TxtIn.Location = new System.Drawing.Point(10, 80);
		this.TxtIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.TxtIn.Name = "TxtIn";
		this.TxtIn.Size = new System.Drawing.Size(1049, 28);
		this.TxtIn.TabIndex = 0;
		this.lblIn.AutoSize = true;
		this.lblIn.Location = new System.Drawing.Point(10, 48);
		this.lblIn.Name = "lblIn";
		this.lblIn.Size = new System.Drawing.Size(86, 20);
		this.lblIn.TabIndex = 1;
		this.lblIn.Text = "Command:";
		this.TxtResults.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtResults.BackColor = System.Drawing.Color.DarkBlue;
		this.TxtResults.Font = new System.Drawing.Font("Courier New", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtResults.ForeColor = System.Drawing.Color.White;
		this.TxtResults.Location = new System.Drawing.Point(10, 152);
		this.TxtResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.TxtResults.Multiline = true;
		this.TxtResults.Name = "TxtResults";
		this.TxtResults.ReadOnly = true;
		this.TxtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtResults.Size = new System.Drawing.Size(1050, 558);
		this.TxtResults.TabIndex = 2;
		this.TxtResults.TabStop = false;
		this.TxtResults.WordWrap = false;
		this.lblResults.AutoSize = true;
		this.lblResults.Location = new System.Drawing.Point(10, 124);
		this.lblResults.Name = "lblResults";
		this.lblResults.Size = new System.Drawing.Size(67, 20);
		this.lblResults.TabIndex = 3;
		this.lblResults.Text = "Results:";
		this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.cmdClear, this.ToolStripSeparator1, this.cmdRun, this.cmdCancel, this.ToolStripSeparator2, this.cmdOut, this.mnuSep3, this.cmdExit });
		this.ToolStrip1.Location = new System.Drawing.Point(0, 33);
		this.ToolStrip1.Name = "ToolStrip1";
		this.ToolStrip1.Size = new System.Drawing.Size(1073, 34);
		this.ToolStrip1.TabIndex = 5;
		this.ToolStrip1.Text = "ToolStrip1";
		this.cmdClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdClear.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(153, 29);
		this.cmdClear.Text = "Clear Screen (Del)";
		this.cmdClear.ToolTipText = "Clear Screen";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 34);
		this.cmdRun.Image = (System.Drawing.Image)resources.GetObject("cmdRun.Image");
		this.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdRun.Name = "cmdRun";
		this.cmdRun.Size = new System.Drawing.Size(190, 29);
		this.cmdRun.Text = "Run Command (F8)";
		this.cmdCancel.Image = (System.Drawing.Image)resources.GetObject("cmdCancel.Image");
		this.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(131, 29);
		this.cmdCancel.Text = "Cancel (F10)";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 34);
		this.cmdOut.Image = (System.Drawing.Image)resources.GetObject("cmdOut.Image");
		this.cmdOut.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdOut.Name = "cmdOut";
		this.cmdOut.Size = new System.Drawing.Size(239, 29);
		this.cmdOut.Text = "Show Query Output (F12)";
		this.mnuSep3.Name = "mnuSep3";
		this.mnuSep3.Size = new System.Drawing.Size(6, 34);
		this.cmdExit.Image = (System.Drawing.Image)resources.GetObject("cmdExit.Image");
		this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdExit.Name = "cmdExit";
		this.cmdExit.Size = new System.Drawing.Size(79, 29);
		this.cmdExit.Text = "Close";
		this.MenuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.OptionsToolStripMenuItem });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
		this.MenuStrip1.Size = new System.Drawing.Size(1073, 33);
		this.MenuStrip1.TabIndex = 6;
		this.MenuStrip1.Text = "MenuStrip1";
		this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.mnuKeepLog, this.mnucolors, this.ToolStripSeparator3, this.mnuRun, this.mnuCancel, this.mnuShow, this.mnuClear });
		this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
		this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
		this.OptionsToolStripMenuItem.Text = "Options";
		this.mnuKeepLog.CheckOnClick = true;
		this.mnuKeepLog.Name = "mnuKeepLog";
		this.mnuKeepLog.Size = new System.Drawing.Size(265, 34);
		this.mnuKeepLog.Text = "Keep Log History";
		this.mnucolors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.mnuwhiteblue, this.mnublackwhite, this.mnuwhiteblack, this.mnuwhitemedblue, this.mnublackteal, this.mnupurplegrey });
		this.mnucolors.Name = "mnucolors";
		this.mnucolors.Size = new System.Drawing.Size(265, 34);
		this.mnucolors.Text = "Colors";
		this.mnuwhiteblue.Checked = true;
		this.mnuwhiteblue.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mnuwhiteblue.Name = "mnuwhiteblue";
		this.mnuwhiteblue.Size = new System.Drawing.Size(295, 34);
		this.mnuwhiteblue.Tag = "WHITEDARKBLUE";
		this.mnuwhiteblue.Text = "White on Dark Blue";
		this.mnublackwhite.Name = "mnublackwhite";
		this.mnublackwhite.Size = new System.Drawing.Size(295, 34);
		this.mnublackwhite.Tag = "BLACKWHITE";
		this.mnublackwhite.Text = "Black on White";
		this.mnuwhiteblack.Name = "mnuwhiteblack";
		this.mnuwhiteblack.Size = new System.Drawing.Size(295, 34);
		this.mnuwhiteblack.Tag = "WHITEBLACK";
		this.mnuwhiteblack.Text = "White on Black";
		this.mnuwhitemedblue.Name = "mnuwhitemedblue";
		this.mnuwhitemedblue.Size = new System.Drawing.Size(295, 34);
		this.mnuwhitemedblue.Tag = "WHITEMEDBLUE";
		this.mnuwhitemedblue.Text = "White on Medium Blue";
		this.mnublackteal.Name = "mnublackteal";
		this.mnublackteal.Size = new System.Drawing.Size(295, 34);
		this.mnublackteal.Tag = "BLACKTEAL";
		this.mnublackteal.Text = "Black on Teal";
		this.mnupurplegrey.Name = "mnupurplegrey";
		this.mnupurplegrey.Size = new System.Drawing.Size(295, 34);
		this.mnupurplegrey.Tag = "PURPLEGREY";
		this.mnupurplegrey.Text = "Purple on Grey";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(262, 6);
		this.mnuRun.Name = "mnuRun";
		this.mnuRun.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.mnuRun.Size = new System.Drawing.Size(265, 34);
		this.mnuRun.Text = "Run Command";
		this.mnuCancel.Name = "mnuCancel";
		this.mnuCancel.ShortcutKeys = System.Windows.Forms.Keys.F10;
		this.mnuCancel.Size = new System.Drawing.Size(265, 34);
		this.mnuCancel.Text = "Cancel Query";
		this.mnuShow.Name = "mnuShow";
		this.mnuShow.ShortcutKeys = System.Windows.Forms.Keys.F12;
		this.mnuShow.Size = new System.Drawing.Size(265, 34);
		this.mnuShow.Text = "Show Output";
		this.mnuClear.Name = "mnuClear";
		this.mnuClear.ShortcutKeys = System.Windows.Forms.Keys.Delete;
		this.mnuClear.Size = new System.Drawing.Size(265, 34);
		this.mnuClear.Text = "Clear Screen";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1073, 728);
		base.Controls.Add(this.ToolStrip1);
		base.Controls.Add(this.MenuStrip1);
		base.Controls.Add(this.lblResults);
		base.Controls.Add(this.TxtResults);
		base.Controls.Add(this.lblIn);
		base.Controls.Add(this.TxtIn);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.MenuStrip1;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "FrmCmdSim";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SQLPathFinder Query Log";
		this.ToolStrip1.ResumeLayout(false);
		this.ToolStrip1.PerformLayout();
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void cmdExit_Click(object sender, EventArgs e)
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

	public void RunCmdWindow(string MyCMD, int MyMode = 0)
	{
		int num = 0;
		int num2 = 0;
		try
		{
			if (!Information.IsNothing(cmd) && !cmd.HasExited)
			{
				Interaction.MsgBox("A Query is running. First cancel the running query or wait for it to finish before running another query?", MsgBoxStyle.Information, "Query Running");
				return;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.ClearProjectError();
		}
		try
		{
			if (!Information.IsNothing(cmd))
			{
				cmd.Kill();
				cmd.Close();
				cmd.Dispose();
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			ProjectData.ClearProjectError();
		}
		if (Operators.CompareString(MyCMD, "", TextCompare: false) == 0)
		{
			MyCMD = Strings.Trim(TxtIn.Text);
		}
		if (Operators.CompareString(MyCMD, "", TextCompare: false) == 0)
		{
			return;
		}
		if (MyMode == 0)
		{
			MyCMD = Globals_Renamed.gSPFCmdWinInit + "&&@Echo.&&@Echo Stand by ...&&" + MyCMD + "&&@Echo SQLP^> ";
		}
		checked
		{
			if (!mnuKeepLog.Checked)
			{
				TxtResults.Clear();
			}
			else
			{
				num = TxtResults.Lines.Length;
				if (num >= 5000)
				{
					num2 = (int)Conversion.Int((double)TxtResults.Text.Length / 2.0);
					TxtResults.Text = TxtResults.Text.Substring(num2);
					TxtResults.Text = TxtResults.Text.Substring(TxtResults.Text.IndexOf("\r\n") + 2);
				}
			}
			psi = new ProcessStartInfo();
			ProcessStartInfo processStartInfo = psi;
			processStartInfo.FileName = "cmd";
			processStartInfo.Arguments = " /Q /c " + MyCMD;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardInput = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo = null;
			cmd = new Process();
			Process process = cmd;
			process.StartInfo = psi;
			process.EnableRaisingEvents = true;
			process = null;
			cmd.ErrorDataReceived += Async_Data_Received;
			cmd.OutputDataReceived += Async_Data_Received;
			cmd.Start();
			cmd.BeginOutputReadLine();
			cmd.BeginErrorReadLine();
		}
	}

	public void CancelCmdWindow()
	{
		if (!Information.IsNothing(cmd))
		{
			try
			{
				if (!cmd.HasExited)
				{
					KillProcess();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
		if (!Information.IsNothing(cmd))
		{
			cmd.Close();
			cmd.Dispose();
		}
	}

	public void KillProcess()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		Process process = default(Process);
		ProcessStartInfo processStartInfo = default(ProcessStartInfo);
		ProcessStartInfo processStartInfo2 = default(ProcessStartInfo);
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
				case 364:
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
							goto IL_0014;
						case 4:
							goto IL_0019;
						case 5:
							goto IL_0042;
						case 6:
							goto IL_0051;
						case 7:
							goto IL_005c;
						case 8:
							goto IL_0067;
						case 9:
							goto IL_0072;
						case 10:
							goto IL_007e;
						case 11:
							goto IL_008b;
						case 12:
							goto IL_008e;
						case 13:
							goto IL_00a0;
						case 14:
							goto IL_00ab;
						case 15:
							goto IL_00b6;
						case 16:
							goto IL_00c1;
						case 17:
							goto IL_00cc;
						case 18:
							goto IL_00e3;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c1:
					num2 = 16;
					process.Dispose();
					goto IL_00cc;
					IL_00cc:
					num2 = 17;
					if (Information.IsNothing(cmd))
					{
						goto end_IL_0001_3;
					}
					goto IL_00e3;
					IL_00b6:
					num2 = 15;
					process.Close();
					goto IL_00c1;
					IL_00e3:
					num2 = 18;
					cmd.Close();
					break;
					IL_000b:
					num2 = 2;
					processStartInfo = new ProcessStartInfo();
					goto IL_0014;
					IL_0014:
					num2 = 3;
					processStartInfo2 = processStartInfo;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					processStartInfo2.Arguments = "/PID " + Conversions.ToString(cmd.Id) + " /T /f";
					goto IL_0042;
					IL_0042:
					num2 = 5;
					processStartInfo2.FileName = "taskkill";
					goto IL_0051;
					IL_0051:
					num2 = 6;
					processStartInfo2.UseShellExecute = false;
					goto IL_005c;
					IL_005c:
					num2 = 7;
					processStartInfo2.RedirectStandardError = true;
					goto IL_0067;
					IL_0067:
					num2 = 8;
					processStartInfo2.RedirectStandardOutput = true;
					goto IL_0072;
					IL_0072:
					num2 = 9;
					processStartInfo2.RedirectStandardInput = true;
					goto IL_007e;
					IL_007e:
					num2 = 10;
					processStartInfo2.CreateNoWindow = true;
					goto IL_008b;
					IL_008b:
					processStartInfo2 = null;
					goto IL_008e;
					IL_008e:
					num2 = 12;
					process = new Process
					{
						StartInfo = processStartInfo
					};
					goto IL_00a0;
					IL_00a0:
					num2 = 13;
					process.Start();
					goto IL_00ab;
					IL_00ab:
					num2 = 14;
					process.WaitForExit();
					goto IL_00b6;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				cmd.Dispose();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 364;
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

	public void CloseCmdWindow()
	{
		Close();
	}

	private void Async_Data_Received(object sender, DataReceivedEventArgs e)
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
				case 77:
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
				Invoke(new InvokewithString(Sync_Output), e.Data);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 77;
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

	private void Sync_Output(string Text)
	{
		TxtResults.AppendText(Text + Environment.NewLine);
		TxtResults.ScrollToCaret();
	}

	public void ClearCmdWindow()
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
				TxtResults.Clear();
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

	private void FrmCmdSim_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!Information.IsNothing(cmd))
		{
			try
			{
				if (!cmd.HasExited)
				{
					int num = (int)Interaction.MsgBox("A Query process is running. Are you sure you wish to exit?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Exit");
					if (num != 6)
					{
						e.Cancel = true;
						return;
					}
					cmd.Kill();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
		try
		{
			if (!Information.IsNothing(cmd))
			{
				cmd.Close();
				cmd.Dispose();
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			ProjectData.ClearProjectError();
		}
		if (mnuKeepLog.Checked)
		{
			Globals_Renamed.gCmdWinHist = "Y";
		}
		else
		{
			Globals_Renamed.gCmdWinHist = "N";
		}
		BuildForm.Save_DTRBuild_Ini("W");
		if (base.WindowState == FormWindowState.Normal)
		{
			Globals_Renamed.gfrmlog_x = base.Left;
			Globals_Renamed.gfrmlog_y = base.Top;
			Globals_Renamed.gfrmlog_w = base.Width;
			Globals_Renamed.gfrmlog_h = base.Height;
			BuildForm.Save_DTRBuild_Ini("W2");
		}
	}

	private void FrmCmdSim_FormClosed(object sender, FormClosedEventArgs e)
	{
		Dispose();
	}

	private void FrmCmdSim_Load(object sender, EventArgs e)
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
				case 294:
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
							goto IL_0014;
						case 4:
							goto IL_002d;
						case 5:
						case 6:
							goto IL_003e;
						case 7:
							goto IL_0058;
						case 8:
							goto IL_0066;
						case 9:
						case 10:
							goto IL_0076;
						case 11:
							goto IL_0091;
						case 12:
							goto IL_00a0;
						case 13:
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0066:
					num2 = 8;
					base.Width = Globals_Renamed.gfrmlog_w;
					goto IL_0076;
					IL_0076:
					num2 = 10;
					if (Globals_Renamed.gfrmlog_x == 0 || Globals_Renamed.gfrmlog_y == 0)
					{
						break;
					}
					goto IL_0091;
					IL_0058:
					num2 = 7;
					base.Height = Globals_Renamed.gfrmlog_h;
					goto IL_0066;
					IL_00a0:
					num2 = 12;
					base.Left = Globals_Renamed.gfrmlog_x;
					break;
					IL_000b:
					num2 = 2;
					Set_Term_Color();
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(Globals_Renamed.gCmdWinHist, "Y", TextCompare: false) == 0)
					{
						goto IL_002d;
					}
					goto IL_003e;
					IL_002d:
					num2 = 4;
					mnuKeepLog.Checked = true;
					goto IL_003e;
					IL_003e:
					num2 = 6;
					if (Globals_Renamed.gfrmlog_h != 0 && Globals_Renamed.gfrmlog_w != 0)
					{
						goto IL_0058;
					}
					goto IL_0076;
					IL_0091:
					num2 = 11;
					base.Top = Globals_Renamed.gfrmlog_y;
					goto IL_00a0;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				Text = Text + " (" + Globals_Renamed.gSPFCache + ")";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 294;
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

	private void Set_Term_Color()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj5 = default(object);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				TextBox txtResults;
				object obj;
				TextBox txtResults2;
				object obj6;
				TextBox txtResults3;
				object obj7;
				TextBox txtResults4;
				object obj8;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 935:
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
							goto IL_0029;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0047;
						case 7:
							goto IL_0056;
						case 8:
							goto IL_0065;
						case 9:
							goto IL_007a;
						case 10:
							goto IL_0095;
						case 11:
							goto IL_00ad;
						case 12:
							goto IL_00cc;
						case 14:
							goto IL_0145;
						case 15:
							goto IL_0159;
						case 16:
							goto IL_016d;
						case 18:
							goto IL_0183;
						case 19:
							goto IL_0197;
						case 20:
							goto IL_01ab;
						case 22:
							goto IL_01c1;
						case 23:
							goto IL_01d5;
						case 24:
							goto IL_01e9;
						case 26:
							goto IL_01ff;
						case 27:
							goto IL_0224;
						case 28:
							goto IL_0238;
						case 30:
							goto IL_024e;
						case 31:
							goto IL_0274;
						case 32:
							goto IL_0288;
						case 34:
							goto IL_029b;
						case 35:
							goto IL_02c1;
						case 36:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 17:
						case 21:
						case 25:
						case 29:
						case 33:
						case 37:
						case 38:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ff:
					num2 = 26;
					txtResults = TxtResults;
					obj = obj2;
					txtResults.BackColor = ((obj != null) ? ((Color)obj) : default(Color));
					goto IL_0224;
					IL_0159:
					num2 = 15;
					TxtResults.ForeColor = Color.White;
					goto IL_016d;
					IL_0288:
					num2 = 32;
					mnublackteal.Checked = true;
					goto end_IL_0001_3;
					IL_016d:
					num2 = 16;
					mnuwhiteblue.Checked = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					mnuwhiteblue.Checked = false;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					mnublackwhite.Checked = false;
					goto IL_0029;
					IL_0029:
					num2 = 4;
					mnuwhiteblack.Checked = false;
					goto IL_0038;
					IL_0038:
					num2 = 5;
					mnuwhitemedblue.Checked = false;
					goto IL_0047;
					IL_0047:
					num2 = 6;
					mnublackteal.Checked = false;
					goto IL_0056;
					IL_0056:
					num2 = 7;
					mnupurplegrey.Checked = false;
					goto IL_0065;
					IL_0065:
					num2 = 8;
					obj2 = Color.FromArgb(0, 55, 218);
					goto IL_007a;
					IL_007a:
					num2 = 9;
					obj3 = Color.FromArgb(97, 214, 214);
					goto IL_0095;
					IL_0095:
					num2 = 10;
					obj4 = Color.FromArgb(136, 23, 52);
					goto IL_00ad;
					IL_00ad:
					num2 = 11;
					obj5 = Color.FromArgb(252, 252, 252);
					goto IL_00cc;
					IL_00cc:
					num2 = 12;
					switch (Globals_Renamed.gCmdWinColor)
					{
					case "WHITEDARKBLUE":
						break;
					case "BLACKWHITE":
						goto IL_0183;
					case "WHITEBLACK":
						goto IL_01c1;
					case "WHITEMEDBLUE":
						goto IL_01ff;
					case "BLACKTEAL":
						goto IL_024e;
					case "PURPLEGREY":
						goto IL_029b;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0145;
					IL_029b:
					num2 = 34;
					txtResults2 = TxtResults;
					obj6 = obj5;
					txtResults2.BackColor = ((obj6 != null) ? ((Color)obj6) : default(Color));
					goto IL_02c1;
					IL_0224:
					num2 = 27;
					TxtResults.ForeColor = Color.White;
					goto IL_0238;
					IL_0238:
					num2 = 28;
					mnuwhitemedblue.Checked = true;
					goto end_IL_0001_3;
					IL_0145:
					num2 = 14;
					TxtResults.BackColor = Color.DarkBlue;
					goto IL_0159;
					IL_02c1:
					num2 = 35;
					txtResults3 = TxtResults;
					obj7 = obj4;
					txtResults3.ForeColor = ((obj7 != null) ? ((Color)obj7) : default(Color));
					break;
					IL_01d5:
					num2 = 23;
					TxtResults.ForeColor = Color.White;
					goto IL_01e9;
					IL_01e9:
					num2 = 24;
					mnuwhiteblack.Checked = true;
					goto end_IL_0001_3;
					IL_01c1:
					num2 = 22;
					TxtResults.BackColor = Color.Black;
					goto IL_01d5;
					IL_024e:
					num2 = 30;
					txtResults4 = TxtResults;
					obj8 = obj3;
					txtResults4.BackColor = ((obj8 != null) ? ((Color)obj8) : default(Color));
					goto IL_0274;
					IL_0197:
					num2 = 19;
					TxtResults.ForeColor = Color.Black;
					goto IL_01ab;
					IL_01ab:
					num2 = 20;
					mnublackwhite.Checked = true;
					goto end_IL_0001_3;
					IL_0183:
					num2 = 18;
					TxtResults.BackColor = Color.White;
					goto IL_0197;
					IL_0274:
					num2 = 31;
					TxtResults.ForeColor = Color.Black;
					goto IL_0288;
					end_IL_0001_2:
					break;
				}
				num2 = 36;
				mnupurplegrey.Checked = true;
				break;
				end_IL_0001:;
			}
			catch (object obj9) when (obj9 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj9);
				try0001_dispatch = 935;
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

	private void mnuwhiteblue_Click(object sender, EventArgs e)
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
				case 87:
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
					Globals_Renamed.gCmdWinColor = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				Set_Term_Color();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 87;
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

	private void mnuRun_Click(object sender, EventArgs e)
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
				case 131:
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
							goto IL_002d;
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
					RunCmdWindow("");
					goto IL_001a;
					IL_001a:
					num2 = 3;
					TxtResults.AppendText("\r\nSQLP>");
					goto IL_002d;
					IL_002d:
					num2 = 4;
					TxtResults.SelectionStart = TxtResults.Text.Length;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				TxtResults.ScrollToCaret();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 131;
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

	private void mnuCancel_Click(object sender, EventArgs e)
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
				case 125:
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
							goto IL_0014;
						case 4:
							goto IL_0027;
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
					CancelCmdWindow();
					goto IL_0014;
					IL_0014:
					num2 = 3;
					TxtResults.AppendText("\r\nSQLP>");
					goto IL_0027;
					IL_0027:
					num2 = 4;
					TxtResults.SelectionStart = TxtResults.Text.Length;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				TxtResults.ScrollToCaret();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 125;
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

	private void mnuShow_Click(object sender, EventArgs e)
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
				case 60:
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
				BuildForm.Invoke_Txt("spfgrid", 1, "");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 60;
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

	private void mnuClear_Click(object sender, EventArgs e)
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
					ClearCmdWindow();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				TxtResults.AppendText("SQLP>");
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
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[DesignerGenerated]
public sealed class FrmAbout : Form
{
	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("OKButton")]
	private Button _OKButton;

	private IContainer components;

	[field: AccessedThroughProperty("TableLayoutPanel")]
	internal virtual TableLayoutPanel TableLayoutPanel
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LogoPictureBox")]
	internal virtual PictureBox LogoPictureBox
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabelProductName")]
	internal virtual Label LabelProductName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabelVersion")]
	internal virtual Label LabelVersion
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabelCompanyName")]
	internal virtual Label LabelCompanyName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextBoxDescription")]
	internal virtual TextBox TextBoxDescription
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Button OKButton
	{
		[CompilerGenerated]
		get
		{
			return _OKButton;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OKButton_Click;
			Button button = _OKButton;
			if (button != null)
			{
				button.Click -= value2;
			}
			_OKButton = value;
			button = _OKButton;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabelCopyright")]
	internal virtual Label LabelCopyright
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmAbout()
	{
		base.Load += AboutBox1_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLPathFinder3.FrmAbout));
		this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
		this.LabelProductName = new System.Windows.Forms.Label();
		this.LabelVersion = new System.Windows.Forms.Label();
		this.LabelCopyright = new System.Windows.Forms.Label();
		this.LabelCompanyName = new System.Windows.Forms.Label();
		this.TextBoxDescription = new System.Windows.Forms.TextBox();
		this.OKButton = new System.Windows.Forms.Button();
		this.LogoPictureBox = new System.Windows.Forms.PictureBox();
		this.TableLayoutPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.LogoPictureBox).BeginInit();
		base.SuspendLayout();
		this.TableLayoutPanel.ColumnCount = 2;
		this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.55076f));
		this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.44924f));
		this.TableLayoutPanel.Controls.Add(this.LabelProductName, 1, 0);
		this.TableLayoutPanel.Controls.Add(this.LabelVersion, 1, 1);
		this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 1, 2);
		this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 1, 3);
		this.TableLayoutPanel.Controls.Add(this.TextBoxDescription, 1, 4);
		this.TableLayoutPanel.Controls.Add(this.OKButton, 1, 5);
		this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
		this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel.Location = new System.Drawing.Point(10, 10);
		this.TableLayoutPanel.Name = "TableLayoutPanel";
		this.TableLayoutPanel.RowCount = 6;
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.6f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.4f));
		this.TableLayoutPanel.Size = new System.Drawing.Size(411, 250);
		this.TableLayoutPanel.TabIndex = 0;
		this.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LabelProductName.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LabelProductName.Location = new System.Drawing.Point(70, 0);
		this.LabelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.LabelProductName.MaximumSize = new System.Drawing.Size(0, 18);
		this.LabelProductName.Name = "LabelProductName";
		this.LabelProductName.Size = new System.Drawing.Size(338, 18);
		this.LabelProductName.TabIndex = 0;
		this.LabelProductName.Text = "Product Name";
		this.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LabelVersion.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LabelVersion.Location = new System.Drawing.Point(70, 25);
		this.LabelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.LabelVersion.MaximumSize = new System.Drawing.Size(0, 18);
		this.LabelVersion.Name = "LabelVersion";
		this.LabelVersion.Size = new System.Drawing.Size(338, 18);
		this.LabelVersion.TabIndex = 0;
		this.LabelVersion.Text = "Version";
		this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LabelCopyright.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LabelCopyright.Location = new System.Drawing.Point(70, 50);
		this.LabelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.LabelCopyright.MaximumSize = new System.Drawing.Size(0, 18);
		this.LabelCopyright.Name = "LabelCopyright";
		this.LabelCopyright.Size = new System.Drawing.Size(338, 18);
		this.LabelCopyright.TabIndex = 0;
		this.LabelCopyright.Text = "Copyright";
		this.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LabelCompanyName.Font = new System.Drawing.Font("Arial", 8.25f);
		this.LabelCompanyName.Location = new System.Drawing.Point(70, 75);
		this.LabelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.LabelCompanyName.MaximumSize = new System.Drawing.Size(0, 18);
		this.LabelCompanyName.Name = "LabelCompanyName";
		this.LabelCompanyName.Size = new System.Drawing.Size(338, 18);
		this.LabelCompanyName.TabIndex = 0;
		this.LabelCompanyName.Text = "Company Name";
		this.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TextBoxDescription.Font = new System.Drawing.Font("Arial", 8.25f);
		this.TextBoxDescription.Location = new System.Drawing.Point(70, 103);
		this.TextBoxDescription.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
		this.TextBoxDescription.Multiline = true;
		this.TextBoxDescription.Name = "TextBoxDescription";
		this.TextBoxDescription.ReadOnly = true;
		this.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TextBoxDescription.Size = new System.Drawing.Size(338, 107);
		this.TextBoxDescription.TabIndex = 0;
		this.TextBoxDescription.TabStop = false;
		this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.OKButton.Location = new System.Drawing.Point(321, 222);
		this.OKButton.Name = "OKButton";
		this.OKButton.Size = new System.Drawing.Size(87, 25);
		this.OKButton.TabIndex = 0;
		this.OKButton.Text = "&OK";
		this.LogoPictureBox.Image = (System.Drawing.Image)resources.GetObject("LogoPictureBox.Image");
		this.LogoPictureBox.Location = new System.Drawing.Point(3, 3);
		this.LogoPictureBox.Name = "LogoPictureBox";
		this.TableLayoutPanel.SetRowSpan(this.LogoPictureBox, 6);
		this.LogoPictureBox.Size = new System.Drawing.Size(48, 48);
		this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.LogoPictureBox.TabIndex = 0;
		this.LogoPictureBox.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.OKButton;
		base.ClientSize = new System.Drawing.Size(431, 270);
		base.Controls.Add(this.TableLayoutPanel);
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmAbout";
		base.Padding = new System.Windows.Forms.Padding(10);
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About SQLPathFinder";
		this.TableLayoutPanel.ResumeLayout(false);
		this.TableLayoutPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.LogoPictureBox).EndInit();
		base.ResumeLayout(false);
	}

	private void AboutBox1_Load(object sender, EventArgs e)
	{
		LabelProductName.Text = "SQLPathFinder";
		LabelVersion.Text = MyProject.Application.Info.Version.ToString();
		LabelCopyright.Text = "Copyright © 2024";
		LabelCompanyName.Text = "Jolyon Clarke";
		TextBoxDescription.Text = "SQLPathFinder helps you query, join and analyze data from databases. It also includes utilities (such as If-Then) to help you create end-to-end applications.\r\n\r\n\"The earth is but one country, and mankind its citizens.\" Baha'u'llah";
	}

	private void OKButton_Click(object sender, EventArgs e)
	{
		Close();
	}
}

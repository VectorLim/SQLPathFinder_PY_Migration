using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;
using TenTec.Windows.iGridLib;
using TenTec.Windows.iGridLib.Filtering;

namespace SQLPathFinder3;

[DesignerGenerated]
public class FrmNodeSel : Form
{
	public struct Chart_Type
	{
		public string ScriptType;

		public string Name;

		public string Desc;

		public string Code;

		public string Ini;

		public string Type;
	}

	private IContainer components;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdOK")]
	private Button _CmdOK;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("CmdCancel")]
	private Button _CmdCancel;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("cmdClear")]
	private Button _cmdClear;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[AccessedThroughProperty("GridNode")]
	private iGrid _GridNode;

	public string f_OutNode;

	public string f_DBType;

	public int f_Mode;

	public string f_Pattern;

	private bool f_IsColHdr;

	private int f_MaxCharts;

	private Chart_Type[] Charts;

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

	internal virtual Button cmdClear
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
			Button button = _cmdClear;
			if (button != null)
			{
				button.Click -= value2;
			}
			_cmdClear = value;
			button = _cmdClear;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	internal virtual iGrid GridNode
	{
		[CompilerGenerated]
		get
		{
			return _GridNode;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			iGColWidthEventHandler val = new iGColWidthEventHandler(GridNode_ColWidthEndChange);
			KeyEventHandler value2 = GridNode_KeyDown;
			KeyPressEventHandler value3 = GridNode_KeyPress;
			MouseEventHandler value4 = GridNode_MouseDoubleClick;
			iGColDividerDoubleClickEventHandler val2 = new iGColDividerDoubleClickEventHandler(GridNode_ColDividerDoubleClick);
			iGrid val3 = _GridNode;
			if (val3 != null)
			{
				val3.ColWidthEndChange -= val;
				((Control)(object)val3).KeyDown -= value2;
				((Control)(object)val3).KeyPress -= value3;
				((Control)(object)val3).MouseDoubleClick -= value4;
				val3.ColDividerDoubleClick -= val2;
			}
			_GridNode = value;
			val3 = _GridNode;
			if (val3 != null)
			{
				val3.ColWidthEndChange += val;
				((Control)(object)val3).KeyDown += value2;
				((Control)(object)val3).KeyPress += value3;
				((Control)(object)val3).MouseDoubleClick += value4;
				val3.ColDividerDoubleClick += val2;
			}
		}
	}

	[field: AccessedThroughProperty("igFilter")]
	internal virtual iGAutoFilterManager igFilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FrmNodeSel()
	{
		base.Load += FrmNodeSel_Load;
		base.Resize += FrmNodeSel_Resize;
		f_OutNode = "";
		f_DBType = "0";
		f_Mode = 0;
		f_Pattern = "";
		f_IsColHdr = false;
		f_MaxCharts = 0;
		Charts = new Chart_Type[101];
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		this.CmdOK = new System.Windows.Forms.Button();
		this.CmdCancel = new System.Windows.Forms.Button();
		this.cmdClear = new System.Windows.Forms.Button();
		this.GridNode = new iGrid();
		this.igFilter = new iGAutoFilterManager();
		((System.ComponentModel.ISupportInitialize)this.GridNode).BeginInit();
		base.SuspendLayout();
		this.CmdOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdOK.Location = new System.Drawing.Point(836, 6);
		this.CmdOK.Margin = new System.Windows.Forms.Padding(4);
		this.CmdOK.Name = "CmdOK";
		this.CmdOK.Size = new System.Drawing.Size(80, 37);
		this.CmdOK.TabIndex = 1;
		this.CmdOK.Text = "OK";
		this.CmdOK.UseVisualStyleBackColor = true;
		this.CmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CmdCancel.Location = new System.Drawing.Point(836, 44);
		this.CmdCancel.Margin = new System.Windows.Forms.Padding(4);
		this.CmdCancel.Name = "CmdCancel";
		this.CmdCancel.Size = new System.Drawing.Size(80, 37);
		this.CmdCancel.TabIndex = 2;
		this.CmdCancel.Text = "Cancel";
		this.CmdCancel.UseVisualStyleBackColor = true;
		this.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClear.Location = new System.Drawing.Point(836, 95);
		this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
		this.cmdClear.Name = "cmdClear";
		this.cmdClear.Size = new System.Drawing.Size(80, 37);
		this.cmdClear.TabIndex = 3;
		this.cmdClear.Text = "Clear";
		this.cmdClear.UseVisualStyleBackColor = true;
		((System.Windows.Forms.Control)(object)this.GridNode).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridNode.CurCellBackColor = System.Drawing.SystemColors.Highlight;
		this.GridNode.CurCellForeColor = System.Drawing.SystemColors.HighlightText;
		this.GridNode.DefaultAutoGroupRow.Height = 20;
		this.GridNode.DefaultRow.Height = 20;
		this.GridNode.DefaultRow.NormalCellHeight = 20;
		this.GridNode.Header.Height = 21;
		((System.Windows.Forms.Control)(object)this.GridNode).Location = new System.Drawing.Point(4, 3);
		((System.Windows.Forms.Control)(object)this.GridNode).Name = "GridNode";
		this.GridNode.ReadOnly = true;
		this.GridNode.RowResizeMode = (iGRowResizeMode)0;
		((System.Windows.Forms.Control)(object)this.GridNode).Size = new System.Drawing.Size(825, 447);
		((System.Windows.Forms.Control)(object)this.GridNode).TabIndex = 0;
		base.AcceptButton = this.CmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.CancelButton = this.CmdCancel;
		base.ClientSize = new System.Drawing.Size(922, 455);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.GridNode);
		base.Controls.Add(this.cmdClear);
		base.Controls.Add(this.CmdCancel);
		base.Controls.Add(this.CmdOK);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmNodeSel";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Select one or More Nodes ";
		((System.ComponentModel.ISupportInitialize)this.GridNode).EndInit();
		base.ResumeLayout(false);
	}

	public void Resize_Node_Grid()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		double num6 = default(double);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num8;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 447:
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
								goto IL_001d;
							case 4:
								goto IL_002c;
							case 6:
								goto IL_0049;
							case 7:
								goto IL_0081;
							case 9:
								goto IL_009a;
							case 10:
								goto IL_00a0;
							case 11:
								goto IL_00af;
							case 12:
								goto IL_00cb;
							case 13:
								goto IL_00ed;
							case 15:
								goto IL_00ff;
							case 5:
							case 8:
							case 14:
							case 16:
							case 17:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 18:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00cb:
						num2 = 12;
						GridNode.Cols[num5].Width = (int)Math.Round(num6);
						goto IL_00ed;
						IL_00ed:
						num2 = 13;
						num5++;
						goto IL_00f6;
						IL_00f6:
						if (num5 > num7)
						{
							break;
						}
						goto IL_00cb;
						IL_0049:
						num2 = 6;
						GridNode.Cols[1].Width = (int)Math.Round(num6 - (double)GridNode.Cols[0].Width);
						goto IL_0081;
						IL_000b:
						num2 = 2;
						num6 = ((Control)(object)GridNode).Width - 23;
						goto IL_001d;
						IL_001d:
						num2 = 3;
						GridNode.BeginUpdate();
						goto IL_002c;
						IL_002c:
						num2 = 4;
						num8 = f_Mode;
						if (num8 == -2)
						{
							goto IL_0049;
						}
						if (num8 == -1)
						{
							goto IL_009a;
						}
						goto IL_00ff;
						IL_0081:
						num2 = 7;
						GridNode.Rows.AutoHeight();
						break;
						IL_00ff:
						num2 = 15;
						GridNode.Cols[2].Width = (int)Math.Round(num6 - (double)GridNode.Cols[1].Width - (double)GridNode.Cols[0].Width);
						break;
						IL_009a:
						num2 = 9;
						num5 = 0;
						goto IL_00a0;
						IL_00a0:
						num2 = 10;
						num6 /= 3.0;
						goto IL_00af;
						IL_00af:
						num2 = 11;
						num7 = GridNode.Cols.Count - 1;
						num5 = 0;
						goto IL_00f6;
						end_IL_0001_2:
						break;
					}
					num2 = 17;
					GridNode.EndUpdate();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 447;
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

	public bool NodeMatch(ref string[] MyPatArr, string MyNode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		bool result = default(bool);
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
				case 155:
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
							goto IL_0014;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_002f;
						case 7:
							goto end_IL_0001_2;
						case 9:
						case 10:
							goto IL_004b;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0054:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_002f;
					IL_002f:
					num2 = 6;
					if (LikeOperator.LikeString(MyNode, MyPatArr[num5], CompareMethod.Binary))
					{
						break;
					}
					goto IL_004b;
					IL_001e:
					num2 = 5;
					num6 = Information.UBound(MyPatArr);
					num5 = 0;
					goto IL_0054;
					IL_004b:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_0054;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					MyNode = Strings.LCase(MyNode);
					goto IL_001e;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 155;
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
		return result;
	}

	private string ERB(string Str)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		int num5 = default(int);
		string result = default(string);
		int num6 = default(int);
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
					case 313:
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
							case 5:
								goto IL_002f;
							case 6:
								goto IL_0038;
							case 7:
								goto IL_003d;
							case 8:
								goto IL_0046;
							case 9:
								goto IL_004f;
							case 10:
								goto IL_005a;
							case 11:
								goto IL_0064;
							case 12:
								goto IL_0074;
							case 13:
								goto IL_0082;
							case 14:
								goto IL_00a0;
							case 15:
								goto IL_00bc;
							case 16:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 4:
							case 17:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0082:
						num2 = 13;
						text = "000" + Conversions.ToString(Strings.Asc(text2) + 72);
						goto IL_00a0;
						IL_00a0:
						num2 = 14;
						text3 += Strings.Mid(text, Strings.Len(text) - 2);
						goto IL_00bc;
						IL_0074:
						num2 = 12;
						text2 = Strings.Mid(Str, num5, 1);
						goto IL_0082;
						IL_00bc:
						num2 = 15;
						num5++;
						goto IL_00c5;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						if (Operators.CompareString(Str, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_002f;
						IL_002f:
						num2 = 5;
						text2 = "";
						goto IL_0038;
						IL_0038:
						num2 = 6;
						num5 = 0;
						goto IL_003d;
						IL_003d:
						num2 = 7;
						text3 = "";
						goto IL_0046;
						IL_0046:
						num2 = 8;
						text = "";
						goto IL_004f;
						IL_004f:
						num2 = 9;
						Str = Strings.StrReverse(Str);
						goto IL_005a;
						IL_005a:
						num2 = 10;
						text3 = "";
						goto IL_0064;
						IL_0064:
						num2 = 11;
						num6 = Strings.Len(Str);
						num5 = 1;
						goto IL_00c5;
						IL_00c5:
						if (num5 > num6)
						{
							break;
						}
						goto IL_0074;
						end_IL_0001_2:
						break;
					}
					num2 = 16;
					result = "[" + text3 + "]";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 313;
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
		return result;
	}

	private void FrmNodeSel_Load(object sender, EventArgs e)
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
						errsource = "FrmNodeSel - Load";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						string text = "";
						string text2 = "";
						bool flag = false;
						string pattern = "";
						int num6 = 0;
						bool flag2 = false;
						int num7 = 0;
						string[] MyPatArr = new string[0];
						GridNode.BeginUpdate();
						if (f_Mode != -2)
						{
							GridNode.ReadOnly = false;
							GridNode.SingleClickEdit = true;
							GridNode.BackColorEvenRows = Color.WhiteSmoke;
							GridNode.CurCellBackColor = SystemColors.Highlight;
							GridNode.CurCellForeColor = SystemColors.HighlightText;
						}
						else
						{
							GridNode.ReadOnly = true;
						}
						GridNode.SearchAsType.Mode = (iGSearchAsTypeMode)1;
						GridNode.SearchAsType.DisplaySearchText = true;
						GridNode.SearchAsType.DisplayKeyboardHint = true;
						GridNode.SearchAsType.StartFromCurRow = false;
						GridNode.SearchAsType.MatchRule = (iGMatchRule)1;
						GridNode.SearchAsType.AutoCancel = true;
						switch (f_Mode)
						{
						case -1:
						{
							Text = "Set Credentials for Nodes Needed";
							base.Height = (int)Math.Round((double)base.Height / 2.0);
							GridNode.Cols.Add("Database");
							GridNode.Cols[0].Width = 125;
							GridNode.Cols[0].AllowMoving = false;
							GridNode.Cols.Add("User Name");
							GridNode.Cols[1].Width = 125;
							GridNode.Cols[1].AllowMoving = false;
							GridNode.Cols.Add("Password");
							GridNode.Cols[2].Width = 125;
							GridNode.Cols[2].AllowMoving = false;
							GridNode.Cols.Add("IDX");
							GridNode.Cols[3].Visible = false;
							GridNode.Cols[3].AllowMoving = false;
							GridNode.Cols[0].CellStyle.ReadOnly = (iGBool)0;
							GridNode.Cols[2].CellStyle.TypeFlags = (iGCellTypeFlags)128;
							GridNode.Cols[3].CellStyle.ReadOnly = (iGBool)0;
							num3 = -1;
							int nonodes3 = Globals_Renamed.nonodes;
							for (num5 = 0; num5 <= nonodes3; num5++)
							{
								if (Operators.CompareString(Globals_Renamed.AllNodes[num5].DBDATA2, "", TextCompare: false) != 0)
								{
									num3++;
									GridNode.Rows.Count = num3 + 1;
									GridNode.Cells[num3, 0].Value = Globals_Renamed.AllNodes[num5].Name;
									GridNode.Cells[num3, 3].Value = Conversions.ToString(num5);
									GridNode.Cells[num3, 1].Value = Globals_Renamed.AllNodes[num5].UN;
									GridNode.Cells[num3, 2].Value = Globals_Renamed.AllNodes[num5].PW;
								}
							}
							GridNode.SortObject.Clear();
							GridNode.SortObject.Add(0);
							GridNode.Sort();
							if (GridNode.Rows.Count > 0)
							{
								GridNode.CurCell = GridNode.Cells[0, 1];
							}
							break;
						}
						case -2:
							Text = "Choose a Script Object (Double-click to select)";
							CmdOK.Visible = false;
							cmdClear.Visible = false;
							CmdCancel.Top = CmdOK.Top;
							CmdCancel.Text = "Close";
							base.Height += 100;
							GridNode.Cols.Add("Object");
							GridNode.Cols[0].Width = 150;
							GridNode.Cols[0].AllowMoving = false;
							GridNode.Cols[0].SortType = (iGSortType)0;
							((iGStyleBase)GridNode.Cols[0].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
							GridNode.Cols.Add("Description");
							GridNode.Cols[1].Width = 125;
							GridNode.Cols[1].AllowMoving = false;
							GridNode.Cols[1].SortType = (iGSortType)0;
							GridNode.Cols[1].AllowSizing = false;
							((iGStyleBase)GridNode.Cols[1].CellStyle).TextFormatFlags = (iGStringFormatFlags)4096;
							GridNode.Cols.Add("Code");
							GridNode.Cols[2].Visible = false;
							GridNode.Cols[2].AllowMoving = false;
							GridNode.Cols.Add("Config");
							GridNode.Cols[3].Visible = false;
							GridNode.Cols[3].AllowMoving = false;
							igFilter.Grid = GridNode;
							Load_Chart_Objects();
							if (GridNode.Rows.Count > 0)
							{
								GridNode.CurCell = GridNode.Cells[0, 0];
							}
							break;
						case 0:
						case 1:
						case 2:
						{
							GridNode.Cols.Add("Select");
							GridNode.Cols[0].Width = 75;
							GridNode.Cols.Add("Node");
							GridNode.Cols[1].Width = 200;
							GridNode.Cols.Add("Description");
							GridNode.Cols[0].CellStyle.Type = (iGCellType)2;
							((iGStyleBase)GridNode.Cols[0].CellStyle).ImageAlign = (iGContentAlignment)2;
							GridNode.Cols[2].AllowSizing = false;
							GridNode.Cols[1].CellStyle.ReadOnly = (iGBool)0;
							GridNode.Cols[2].CellStyle.ReadOnly = (iGBool)0;
							igFilter.Grid = GridNode;
							f_Pattern = Strings.Trim(f_Pattern);
							if (Operators.CompareString(f_Pattern, "", TextCompare: false) != 0)
							{
								MyPatArr = Strings.Split(f_Pattern, ",");
							}
							switch (f_DBType)
							{
							case "0":
							case "1":
								pattern = "*mars*";
								break;
							case "3":
								pattern = "*aries*";
								break;
							case "2":
								pattern = "*oasys*";
								break;
							}
							switch (f_Mode)
							{
							case 1:
								GridNode.Rows.Count = Globals_Renamed.nonodes + 3;
								if ((Operators.CompareString(f_DBType, "5", TextCompare: false) == 0) | (Operators.CompareString(f_DBType, "9", TextCompare: false) == 0))
								{
									if (Operators.CompareString(f_Pattern, "", TextCompare: false) == 0)
									{
										GridNode.Cells[num6, 1].Value = " TEXT";
										GridNode.Cells[num6, 2].Value = "Query Text files using SQLite";
										GridNode.Cells[num6, 0].Value = false;
									}
									else
									{
										num6 = -1;
									}
								}
								else
								{
									GridNode.Cells[num6, 1].Value = " Default";
									GridNode.Cells[num6, 2].Value = "Use the Global (Default) node";
									GridNode.Cells[num6, 0].Value = false;
								}
								num6++;
								GridNode.Cells[num6, 1].Value = " <<<spf-site>>>";
								GridNode.Cells[num6, 2].Value = "References node specified in Site-Loop utility";
								GridNode.Cells[num6, 0].Value = false;
								break;
							case 2:
								GridNode.Rows.Count = Globals_Renamed.nonodes + 6;
								GridNode.Cells[0, 1].Value = " DEFAULT (MARS)";
								GridNode.Cells[0, 2].Value = "Use the Global (Default) MARS node";
								GridNode.Cells[0, 0].Value = false;
								GridNode.Cells[1, 1].Value = " DEFAULT (ARIES)";
								GridNode.Cells[1, 2].Value = "Use the Global (Default) ARIES node";
								GridNode.Cells[1, 0].Value = false;
								GridNode.Cells[2, 1].Value = " DEFAULT (OASYS)";
								GridNode.Cells[2, 2].Value = "Use the Global (Default) OASys node";
								GridNode.Cells[2, 0].Value = false;
								GridNode.Cells[3, 1].Value = " TEXT";
								GridNode.Cells[3, 2].Value = "Query Text files using SQLite";
								GridNode.Cells[3, 0].Value = false;
								num6 = 3;
								break;
							default:
								GridNode.Rows.Count = Globals_Renamed.nonodes + 1;
								num6 = -1;
								break;
							}
							int nonodes = Globals_Renamed.nonodes;
							for (num3 = 0; num3 <= nonodes; num3++)
							{
								if (Operators.CompareString(Globals_Renamed.AllNodes[num3].Display, "Y", TextCompare: false) != 0)
								{
									continue;
								}
								if (f_Mode == 2)
								{
									flag2 = true;
								}
								else
								{
									flag2 = false;
									switch (f_DBType)
									{
									case "0":
									case "1":
									case "2":
									case "3":
										if (Operators.CompareString(f_Pattern, "", TextCompare: false) != 0)
										{
											flag2 = NodeMatch(ref MyPatArr, Globals_Renamed.AllNodes[num3].Name);
										}
										else if (LikeOperator.LikeString(Strings.LCase(Globals_Renamed.AllNodes[num3].Name), pattern, CompareMethod.Binary) || LikeOperator.LikeString(Strings.LCase(Globals_Renamed.AllNodes[num3].Name), "*mao*", CompareMethod.Binary) || BuildSQL.IsXEUS_DIS(Globals_Renamed.AllNodes[num3].Name))
										{
											flag2 = true;
										}
										break;
									case "A":
										if (Operators.CompareString(f_Pattern, "", TextCompare: false) != 0 && NodeMatch(ref MyPatArr, Globals_Renamed.AllNodes[num3].Name))
										{
											flag2 = true;
										}
										else if (Operators.CompareString(f_Pattern, "", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.LCase(Globals_Renamed.AllNodes[num3].Name), "*midas*", CompareMethod.Binary))
										{
											flag2 = true;
										}
										break;
									case "5":
									case "9":
										if (Operators.CompareString(f_Pattern, "", TextCompare: false) == 0 && Operators.CompareString(Strings.Right(Strings.Trim(Strings.LCase(Globals_Renamed.AllNodes[num3].Name)), 5), ".mars", TextCompare: false) != 0 && Operators.CompareString(Strings.Right(Strings.Trim(Strings.LCase(Globals_Renamed.AllNodes[num3].Name)), 6), ".aries", TextCompare: false) != 0 && Operators.CompareString(Strings.Right(Strings.Trim(Strings.LCase(Globals_Renamed.AllNodes[num3].Name)), 6), ".oasys", TextCompare: false) != 0)
										{
											flag2 = true;
										}
										else if (Operators.CompareString(f_Pattern, "", TextCompare: false) != 0 && NodeMatch(ref MyPatArr, Globals_Renamed.AllNodes[num3].Name))
										{
											flag2 = true;
										}
										break;
									}
								}
								if (flag2)
								{
									num6++;
									GridNode.Cells[num6, 0].Value = false;
									GridNode.Cells[num6, 1].Value = Globals_Renamed.AllNodes[num3].Name;
									GridNode.Cells[num6, 2].Value = Globals_Renamed.AllNodes[num3].Desc;
								}
							}
							GridNode.Rows.Count = num6 + 1;
							if (Operators.CompareString(Strings.Trim(f_OutNode), "", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(f_OutNode, ",");
								int num8 = Information.UBound(array);
								for (num3 = 0; num3 <= num8; num3++)
								{
									text = Strings.Trim(array[num3]);
									text2 = Strings.LCase(text);
									if (Operators.CompareString(text2, "", TextCompare: false) == 0)
									{
										continue;
									}
									flag = false;
									int num9 = GridNode.Rows.Count - 1;
									for (num4 = 0; num4 <= num9; num4++)
									{
										if (Operators.CompareString(GridNode.Cells[num4, 1].Value.ToString().ToLower().Trim(), text2, TextCompare: false) == 0)
										{
											GridNode.Cells[num4, 0].Value = true;
											GridNode.Cells[num4, 1].Value = Operators.ConcatenateObject(Strings.Right("000000" + Strings.Trim(Conversions.ToString(num7)), 6), GridNode.Cells[num4, 1].Value);
											num7++;
											flag = true;
											break;
										}
									}
									if (flag)
									{
										continue;
									}
									GridNode.Rows.Count = GridNode.Rows.Count + 1;
									GridNode.Cells[GridNode.Rows.Count - 1, 0].Value = true;
									GridNode.Cells[GridNode.Rows.Count - 1, 1].Value = Strings.Right("000000" + Strings.Trim(Conversions.ToString(num7)), 6) + text;
									num7++;
									GridNode.Cells[GridNode.Rows.Count - 1, 2].Value = "";
									int nonodes2 = Globals_Renamed.nonodes;
									for (num5 = 0; num5 <= nonodes2; num5++)
									{
										if (Operators.CompareString(Globals_Renamed.AllNodes[num5].Display, "N", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(Globals_Renamed.AllNodes[num5].Name), text2, TextCompare: false) == 0)
										{
											GridNode.Cells[GridNode.Rows.Count - 1, 2].Value = Globals_Renamed.AllNodes[num5].Desc;
											break;
										}
									}
								}
								array = null;
							}
							int num10 = GridNode.Rows.Count - 1;
							for (num3 = 0; num3 <= num10; num3++)
							{
								if (Operators.ConditionalCompareObjectEqual(GridNode.Cells[num3, 0].Value, false, TextCompare: false))
								{
									GridNode.Cells[num3, 1].Value = Operators.ConcatenateObject(Strings.Right("000000" + Strings.Trim(Conversions.ToString(num7)), 6), GridNode.Cells[num3, 1].Value);
								}
							}
							if (Operators.CompareString(f_Pattern, "", TextCompare: false) != 0)
							{
								MyPatArr = null;
							}
							GridNode.SortObject.Clear();
							GridNode.SortObject.Add(1);
							GridNode.Sort();
							int num11 = GridNode.Rows.Count - 1;
							for (num3 = 0; num3 <= num11; num3++)
							{
								GridNode.Cells[num3, 1].Value = Strings.Trim(Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(GridNode.Cells[num3, 1].Value, " ")), 7));
							}
							if (GridNode.Rows.Count > 0)
							{
								GridNode.CurCell = GridNode.Cells[0, 1];
							}
							break;
						}
						}
						Resize_Node_Grid();
						if (f_Mode == -2)
						{
							GridNode.Rows.AutoHeight();
						}
						GridNode.EndUpdate();
						goto end_IL_0001;
					}
					case 5302:
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
					goto IL_14ec;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5302;
				continue;
			}
			break;
			IL_14ec:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Load_Chart_Objects()
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
					object MyReader;
					string MyMsg;
					bool flag;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "FrmChartChoose - Form_Load";
						int num3 = 0;
						int num4 = 0;
						MyReader = null;
						flag = false;
						MyMsg = "";
						string[] array = null;
						f_MaxCharts = 0;
						num3 = 0;
						flag = BuildForm.OpenDelimitedFile(Globals_Renamed.gChartDir + "\\ChartObjects.dat", ref MyReader, ref MyMsg);
						if (!flag)
						{
							goto IL_0470;
						}
						while (true)
						{
							if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
							{
								array = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
								if (!BuildForm.VerifySchemaRow(ref array, 6, ref MyMsg))
								{
									goto IL_0470;
								}
								Charts[num3].ScriptType = array[0];
								Charts[num3].Name = array[1];
								Charts[num3].Desc = array[2];
								Charts[num3].Code = array[3];
								Charts[num3].Ini = array[4];
								Charts[num3].Type = array[5];
								if (Operators.CompareString(Strings.Mid(Charts[num3].ScriptType, 1, 1), "!", TextCompare: false) != 0)
								{
									Charts[num3].ScriptType = Strings.Trim(Strings.UCase(Charts[num3].ScriptType));
									num3++;
								}
								array = null;
								continue;
							}
							if (flag)
							{
								NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
								NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
								flag = false;
							}
							array = null;
							f_MaxCharts = num3;
							Charts[num3].ScriptType = f_Pattern;
							Charts[num3].Name = "[Browse]";
							Charts[num3].Desc = "Browse to locate a Chart Configuration (INI) file";
							Charts[num3].Code = "[Browse]";
							Charts[num3].Ini = "";
							Charts[num3].Type = "3";
							GridNode.Rows.Count = f_MaxCharts + 1;
							num4 = -1;
							int num5 = f_MaxCharts;
							for (num3 = 0; num3 <= num5; num3++)
							{
								if (Operators.CompareString(Charts[num3].ScriptType, f_Pattern, TextCompare: false) == 0)
								{
									num4++;
									GridNode.Cells[num4, 0].Value = Charts[num3].Name;
									GridNode.Cells[num4, 1].Value = Charts[num3].Desc;
									GridNode.Cells[num4, 2].Value = Charts[num3].Code;
									GridNode.Cells[num4, 3].Value = Charts[num3].Ini;
									switch (Charts[num3].Type)
									{
									case "1":
										((iGStyleBase)GridNode.Rows[num4].CellStyle).BackColor = Color.BlanchedAlmond;
										break;
									case "2":
										((iGStyleBase)GridNode.Rows[num4].CellStyle).BackColor = Color.Honeydew;
										break;
									case "3":
										((iGStyleBase)GridNode.Rows[num4].CellStyle).BackColor = Color.MistyRose;
										break;
									}
								}
							}
							if (num4 != -1)
							{
								GridNode.Rows.Count = num4 + 1;
							}
							else
							{
								GridNode.Rows.Count = 0;
							}
							break;
						}
						goto end_IL_0001;
					}
					case 1301:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								Cursor.Current = Cursors.Default;
								break;
							default:
								goto end_IL_0001_2;
							}
							break;
						}
						IL_0470:
						Interaction.MsgBox("Error loading Chart Objects (" + MyMsg + ")", MsgBoxStyle.Critical, "Load Error");
						if (flag)
						{
							NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
							NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
							flag = false;
						}
						Cursor.Current = Cursors.Default;
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
				try0001_dispatch = 1301;
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

	private void Process_Chart_Object()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		int rowIndex = default(int);
		string text4 = default(string);
		string left = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string myText;
				string myText2;
				string myText3;
				TreeNode MyNode;
				TreeView treeCol;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1317:
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
							goto IL_001e;
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0078;
						case 8:
							goto IL_0098;
						case 9:
							goto IL_00b8;
						case 10:
							goto IL_00c2;
						case 11:
							goto IL_00cc;
						case 12:
							goto IL_00d6;
						case 13:
							goto IL_00f7;
						case 14:
							goto IL_0119;
						case 15:
							goto IL_016a;
						case 16:
							goto IL_0183;
						case 17:
							goto IL_01a3;
						case 18:
							goto IL_01bc;
						case 20:
							goto IL_01d8;
						case 21:
							goto IL_01fd;
						case 22:
							goto IL_0249;
						case 23:
							goto IL_0262;
						case 24:
							goto IL_026c;
						case 26:
							goto IL_02c3;
						case 27:
							goto IL_02f7;
						case 28:
							goto IL_0301;
						case 30:
							goto IL_0356;
						case 36:
							goto IL_03a4;
						case 37:
							goto IL_03bd;
						case 39:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 5:
						case 19:
						case 25:
						case 29:
						case 31:
						case 32:
						case 33:
						case 34:
						case 35:
						case 38:
						case 40:
						case 41:
						case 42:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0301:
					num2 = 28;
					myText = text;
					MyNode = (treeCol = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol).SelectedNode;
					BuildChart.Process_Chart(myText, ref MyNode, "", "", text2);
					treeCol.SelectedNode = MyNode;
					goto end_IL_0001_3;
					IL_0356:
					num2 = 30;
					Interaction.MsgBox("This appears to be an incompatible chart configuration file. You are creating an \"" + f_Pattern + "\" object using a \"" + text3 + "\" INI file. Please choose another file.", MsgBoxStyle.Exclamation, "Incompatible Chart Configuration File");
					goto end_IL_0001_3;
					IL_02f7:
					num2 = 27;
					text = "CHARTP";
					goto IL_0301;
					IL_0249:
					num2 = 22;
					if (Operators.CompareString(text3, "HTML", TextCompare: false) == 0)
					{
						goto IL_0262;
					}
					goto IL_026c;
					IL_000b:
					num2 = 2;
					rowIndex = GridNode.CurCell.RowIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					if (GridNode.Rows.Count < 1 || rowIndex == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_004b;
					IL_0262:
					num2 = 23;
					text = "CHARTR";
					goto IL_026c;
					IL_004b:
					num2 = 6;
					if (!Operators.ConditionalCompareObjectNotEqual(GridNode.Cells[rowIndex, 0].Value, "", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					goto IL_0078;
					IL_0078:
					num2 = 7;
					text4 = Conversions.ToString(GridNode.Cells[rowIndex, 2].Value);
					goto IL_0098;
					IL_0098:
					num2 = 8;
					text2 = Conversions.ToString(GridNode.Cells[rowIndex, 3].Value);
					goto IL_00b8;
					IL_00b8:
					num2 = 9;
					left = "";
					goto IL_00c2;
					IL_00c2:
					num2 = 10;
					text3 = "";
					goto IL_00cc;
					IL_00cc:
					num2 = 11;
					text = "Chart";
					goto IL_00d6;
					IL_00d6:
					num2 = 12;
					if (Operators.CompareString(Strings.UCase(text4), "[BROWSE]", TextCompare: false) == 0)
					{
						goto IL_00f7;
					}
					goto IL_03a4;
					IL_00f7:
					num2 = 13;
					BuildForm.FileOpenSave("O", "", "ini", "Select a Chart Configuration File", "");
					goto IL_0119;
					IL_0119:
					num2 = 14;
					if (!((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					goto IL_016a;
					IL_016a:
					num2 = 15;
					text2 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
					goto IL_0183;
					IL_0183:
					num2 = 16;
					left = General_Procedures.Get_Ini_Data("TEMPLATE", "FILE", "N/A", 255, text2);
					goto IL_01a3;
					IL_01a3:
					num2 = 17;
					if (Operators.CompareString(left, "N/A", TextCompare: false) == 0)
					{
						goto IL_01bc;
					}
					goto IL_01d8;
					IL_01bc:
					num2 = 18;
					Interaction.MsgBox("This appears to be an invalid chart configuration file. Please choose another.", MsgBoxStyle.Exclamation, "Invalid Config");
					goto end_IL_0001_3;
					IL_01d8:
					num2 = 20;
					text3 = Strings.UCase(General_Procedures.Get_Ini_Data("TEMPLATE", "TYPE", "N/A", 255, text2));
					goto IL_01fd;
					IL_01fd:
					num2 = 21;
					if (Operators.CompareString(text3, Strings.UCase(f_Pattern), TextCompare: false) == 0 || (Operators.CompareString(text3, "JSL", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(f_Pattern), "JMP", TextCompare: false) == 0))
					{
						goto IL_0249;
					}
					goto IL_02c3;
					IL_026c:
					num2 = 24;
					myText2 = text;
					MyNode = (treeCol = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol).SelectedNode;
					BuildChart.Process_Chart(myText2, ref MyNode, "", "", text2);
					treeCol.SelectedNode = MyNode;
					goto end_IL_0001_3;
					IL_03a4:
					num2 = 36;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_03bd;
					IL_02c3:
					num2 = 26;
					if (Operators.CompareString(text3, "HTMLPY", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(f_Pattern), "HTML", TextCompare: false) == 0)
					{
						goto IL_02f7;
					}
					goto IL_0356;
					IL_03bd:
					num2 = 37;
					myText3 = text4;
					MyNode = (treeCol = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol).SelectedNode;
					BuildChart.Process_Chart(myText3, ref MyNode, "", "", text2);
					treeCol.SelectedNode = MyNode;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 39;
				string myText4 = text4;
				MyNode = (treeCol = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol).SelectedNode;
				BuildChart.Process_Chart(myText4, ref MyNode);
				treeCol.SelectedNode = MyNode;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1317;
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

	private void FrmNodeSel_Resize(object sender, EventArgs e)
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
				Resize_Node_Grid();
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

	private void CmdOK_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num7 = default(int);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1329:
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
								goto IL_0010;
							case 5:
								goto IL_0025;
							case 6:
								goto IL_002a;
							case 7:
								goto IL_0047;
							case 8:
								goto IL_007e;
							case 9:
								goto IL_00a8;
							case 11:
							case 12:
							case 13:
								goto IL_00d3;
							case 14:
								goto IL_00e2;
							case 15:
								goto IL_0100;
							case 16:
								goto IL_012c;
							case 17:
								goto IL_0167;
							case 18:
								goto IL_0197;
							case 19:
								goto IL_01ce;
							case 20:
								goto IL_01fe;
							case 22:
								goto IL_0285;
							case 23:
								goto IL_029e;
							case 24:
								goto IL_02b7;
							case 21:
							case 25:
							case 26:
								goto IL_02dd;
							case 28:
								goto IL_02f2;
							case 29:
								goto IL_02fc;
							case 30:
								goto IL_0317;
							case 31:
								goto IL_0343;
							case 32:
							case 33:
								goto IL_0377;
							case 34:
								goto IL_0383;
							case 35:
								goto IL_03ae;
							case 38:
								goto IL_03cb;
							case 39:
								goto IL_03e9;
							case 41:
								goto IL_0405;
							case 42:
								goto IL_0423;
							case 37:
							case 40:
							case 43:
							case 44:
								goto IL_043d;
							case 4:
							case 27:
							case 45:
							case 46:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 10:
							case 36:
							case 47:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0047:
						num2 = 7;
						if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridNode.Cells[num5, 1].Value)), "", TextCompare: false) != 0)
						{
							goto IL_007e;
						}
						goto IL_00d3;
						IL_007e:
						num2 = 8;
						if (Operators.ConditionalCompareObjectEqual(GridNode.Cells[num5, 2].Value, "", TextCompare: false))
						{
							goto IL_00a8;
						}
						goto IL_00d3;
						IL_02dd:
						num2 = 26;
						num5++;
						goto IL_02e4;
						IL_00a8:
						num2 = 9;
						Interaction.MsgBox("No Password specified for Row " + Conversions.ToString(num5 + 1), MsgBoxStyle.Exclamation, "Missing Password");
						goto end_IL_0001_3;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num6 = f_Mode;
						if (num6 == -1)
						{
							goto IL_0025;
						}
						goto IL_02f2;
						IL_02f2:
						num2 = 28;
						text = "";
						goto IL_02fc;
						IL_02fc:
						num2 = 29;
						num7 = GridNode.Rows.Count - 1;
						num5 = 0;
						goto IL_037e;
						IL_037e:
						if (num5 <= num7)
						{
							goto IL_0317;
						}
						goto IL_0383;
						IL_0383:
						num2 = 34;
						if (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text, ",", TextCompare: false) == 0)
						{
							goto IL_03ae;
						}
						goto IL_03cb;
						IL_00d3:
						num2 = 13;
						num5++;
						goto IL_00da;
						IL_03cb:
						num2 = 38;
						if (Strings.InStr(text.ToLower(), ",default,") != 0)
						{
							goto IL_03e9;
						}
						goto IL_0405;
						IL_03e9:
						num2 = 39;
						text = Strings.Replace(text, ",default,", ",", 1, -1, CompareMethod.Text);
						goto IL_043d;
						IL_0405:
						num2 = 41;
						if (Strings.InStr(text.ToLower(), ",none,") != 0)
						{
							goto IL_0423;
						}
						goto IL_043d;
						IL_0423:
						num2 = 42;
						text = Strings.Replace(text, ",none,", ",", 1, -1, CompareMethod.Text);
						goto IL_043d;
						IL_043d:
						num2 = 44;
						f_OutNode = Strings.Mid(text, 2).Trim();
						break;
						IL_03ae:
						num2 = 35;
						Interaction.MsgBox("You must select at least one node.", MsgBoxStyle.Exclamation, "No Nodes Selected");
						goto end_IL_0001_3;
						IL_0317:
						num2 = 30;
						if (Operators.ConditionalCompareObjectEqual(GridNode.Cells[num5, 0].Value, true, TextCompare: false))
						{
							goto IL_0343;
						}
						goto IL_0377;
						IL_0343:
						num2 = 31;
						text = text + "," + GridNode.Cells[num5, 1].Value.ToString().Trim();
						goto IL_0377;
						IL_0377:
						num2 = 33;
						num5++;
						goto IL_037e;
						IL_0025:
						num2 = 5;
						num8 = 0;
						goto IL_002a;
						IL_002a:
						num2 = 6;
						num9 = GridNode.Rows.Count - 1;
						num5 = 0;
						goto IL_00da;
						IL_00da:
						if (num5 <= num9)
						{
							goto IL_0047;
						}
						goto IL_00e2;
						IL_00e2:
						num2 = 14;
						num10 = GridNode.Rows.Count - 1;
						num5 = 0;
						goto IL_02e4;
						IL_02e4:
						if (num5 > num10)
						{
							break;
						}
						goto IL_0100;
						IL_0100:
						num2 = 15;
						num8 = (int)Math.Round(Conversion.Val(RuntimeHelpers.GetObjectValue(GridNode.Cells[num5, 3].Value)));
						goto IL_012c;
						IL_012c:
						num2 = 16;
						if (Operators.CompareString(Strings.Trim(Conversions.ToString(GridNode.Cells[num5, 1].Value)), "", TextCompare: false) != 0)
						{
							goto IL_0167;
						}
						goto IL_0285;
						IL_0167:
						num2 = 17;
						Globals_Renamed.AllNodes[num8].UN = Conversions.ToString(GridNode.Cells[num5, 1].Value);
						goto IL_0197;
						IL_0197:
						num2 = 18;
						Globals_Renamed.AllNodes[num8].UN = Strings.Replace(Globals_Renamed.AllNodes[num8].UN, "@", "<at-sign>", 1, -1, CompareMethod.Text);
						goto IL_01ce;
						IL_01ce:
						num2 = 19;
						Globals_Renamed.AllNodes[num8].PW = Conversions.ToString(GridNode.Cells[num5, 2].Value);
						goto IL_01fe;
						IL_01fe:
						num2 = 20;
						Globals_Renamed.AllNodes[num8].DBDATA = Strings.Replace(Globals_Renamed.AllNodes[num8].DBDATA2, "@USER-PROMPT@", "UN&" + Globals_Renamed.AllNodes[num8].UN + "&@PW&" + ERB(Globals_Renamed.AllNodes[num8].PW) + "&", 1, -1, CompareMethod.Text);
						goto IL_02dd;
						IL_0285:
						num2 = 22;
						Globals_Renamed.AllNodes[num8].UN = "";
						goto IL_029e;
						IL_029e:
						num2 = 23;
						Globals_Renamed.AllNodes[num8].PW = "";
						goto IL_02b7;
						IL_02b7:
						num2 = 24;
						Globals_Renamed.AllNodes[num8].DBDATA = Globals_Renamed.AllNodes[num8].DBDATA2;
						goto IL_02dd;
						end_IL_0001_2:
						break;
					}
					num2 = 46;
					Close();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1329;
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

	private void cmdClear_Click(object sender, EventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string prompt = default(string);
		int num7 = default(int);
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
							goto IL_000f;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_001d;
						case 6:
							goto IL_002e;
						case 7:
						case 8:
							goto IL_0039;
						case 9:
							goto IL_004d;
						case 10:
							goto IL_005d;
						case 11:
							goto IL_007c;
						case 12:
							goto IL_008e;
						case 13:
							goto IL_00af;
						case 15:
							goto IL_00d4;
						case 14:
						case 16:
						case 17:
							goto IL_00f7;
						default:
							goto end_IL_0001;
						case 18:
						case 19:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_00af:
					num2 = 13;
					GridNode.Cells[num5, 2].Value = "";
					goto IL_00f7;
					IL_00d4:
					num2 = 15;
					GridNode.Cells[num5, 0].Value = false;
					goto IL_00f7;
					IL_008e:
					num2 = 12;
					GridNode.Cells[num5, 1].Value = "";
					goto IL_00af;
					IL_00f7:
					num2 = 17;
					num5 = checked(num5 + 1);
					goto IL_0100;
					IL_000b:
					num2 = 2;
					num6 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					prompt = "Are you sure you wish to clear your node selections?";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					if (f_Mode == -1)
					{
						goto IL_002e;
					}
					goto IL_0039;
					IL_002e:
					num2 = 6;
					prompt = "Are you sure you wish to clear Credentials you entered?";
					goto IL_0039;
					IL_0039:
					num2 = 8;
					num6 = (int)Interaction.MsgBox(prompt, MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Clear?");
					goto IL_004d;
					IL_004d:
					num2 = 9;
					if (num6 != 6)
					{
						goto end_IL_0001_2;
					}
					goto IL_005d;
					IL_005d:
					num2 = 10;
					num7 = checked(GridNode.Rows.Count - 1);
					num5 = 0;
					goto IL_0100;
					IL_0100:
					if (num5 > num7)
					{
						goto end_IL_0001_2;
					}
					goto IL_007c;
					IL_007c:
					num2 = 11;
					if (f_Mode == -1)
					{
						goto IL_008e;
					}
					goto IL_00d4;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 364;
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

	private void GridNode_ColWidthEndChange(object sender, iGColWidthEventArgs e)
	{
		if (f_Mode != -1)
		{
			Resize_Node_Grid();
		}
	}

	private void GridNode_KeyDown(object sender, KeyEventArgs e)
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
				case 95:
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
							goto IL_001b;
						case 4:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 6:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (e.KeyCode != Keys.Tab)
					{
						goto end_IL_0001_3;
					}
					goto IL_001b;
					IL_001b:
					num2 = 3;
					CmdOK.Focus();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				e.Handled = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 95;
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

	private void GridNode_KeyPress(object sender, KeyPressEventArgs e)
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
				case 152:
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
							goto IL_0020;
						case 4:
							goto IL_0032;
						case 6:
							goto IL_003e;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0020:
					num2 = 3;
					if (f_Mode == -2)
					{
						goto IL_0032;
					}
					goto IL_003e;
					IL_0032:
					num2 = 4;
					Process_Chart_Object();
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					if (Strings.Asc(e.KeyChar) != 13)
					{
						goto end_IL_0001_3;
					}
					goto IL_0020;
					IL_003e:
					num2 = 6;
					if (f_Mode == -1)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				SelectNode();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 152;
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

	private void SelectNode()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int colIndex = default(int);
		int rowIndex = default(int);
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
				case 178:
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
							goto IL_001e;
						case 4:
							goto IL_0032;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					colIndex = GridNode.CurCell.ColIndex;
					goto IL_001e;
					IL_001e:
					num2 = 3;
					rowIndex = GridNode.CurCell.RowIndex;
					goto IL_0032;
					IL_0032:
					num2 = 4;
					if ((colIndex == 1 || colIndex == 2) && rowIndex > -1)
					{
						break;
					}
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				GridNode.Cells[rowIndex, 0].Value = Operators.NotObject(GridNode.Cells[rowIndex, 0].Value);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 178;
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

	private void GridNode_MouseDoubleClick(object sender, MouseEventArgs e)
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
				case 167:
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
							goto IL_0017;
						case 5:
						case 6:
							goto IL_0024;
						case 7:
							goto IL_0036;
						case 9:
							goto IL_0042;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 8:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0024:
					num2 = 6;
					if (f_Mode == -2)
					{
						goto IL_0036;
					}
					goto IL_0042;
					IL_0036:
					num2 = 7;
					Process_Chart_Object();
					goto end_IL_0001_3;
					IL_0017:
					num2 = 3;
					f_IsColHdr = false;
					goto end_IL_0001_3;
					IL_0042:
					num2 = 9;
					if (f_Mode == -1)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if (f_IsColHdr)
					{
						goto IL_0017;
					}
					goto IL_0024;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				SelectNode();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 167;
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

	private void GridNode_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
	{
		f_IsColHdr = true;
	}
}

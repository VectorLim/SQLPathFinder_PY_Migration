using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildChart
{
	private const string MyPre = "<<<";

	private const string MyPost = ">>>";

	public static void Save_CSS_Grid(ref DataGridView GridFormat, ref Globals_Renamed.Report_Format_Type[] CSSObj, int f_MaxObj, int NoCols, ref string MyRptSpec)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num7 = default(int);
		string text3 = default(string);
		int num8 = default(int);
		int num9 = default(int);
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
					case 540:
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
								goto IL_0034;
							case 9:
								goto IL_0048;
							case 10:
								goto IL_006f;
							case 11:
								goto IL_008b;
							case 12:
								goto IL_0095;
							case 13:
								goto IL_009f;
							case 14:
								goto IL_00a5;
							case 15:
								goto IL_00bb;
							case 16:
								goto IL_00e3;
							case 17:
								goto IL_00fc;
							case 18:
								goto IL_0143;
							case 19:
							case 20:
								goto IL_014e;
							case 21:
								goto IL_0160;
							case 22:
							case 23:
								goto IL_0198;
							default:
								goto end_IL_0001;
							case 24:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0143:
						num2 = 18;
						num5++;
						goto IL_014e;
						IL_014e:
						num2 = 20;
						num6++;
						goto IL_0157;
						IL_00fc:
						num2 = 17;
						text = text + "<\\\\>" + BuildReport.Get_CSS_Attr(GridFormat.Columns[num6].HeaderText, f_MaxObj, ref CSSObj) + ":" + text2;
						goto IL_0143;
						IL_0198:
						num2 = 23;
						num7++;
						goto IL_019f;
						IL_000b:
						num2 = 2;
						num7 = 0;
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
						text2 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						num5 = 0;
						goto IL_0034;
						IL_0034:
						num2 = 8;
						num8 = GridFormat.RowCount - 1;
						num7 = 0;
						goto IL_019f;
						IL_019f:
						if (num7 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_0048;
						IL_0048:
						num2 = 9;
						text3 = Conversions.ToString(GridFormat.Rows[num7].Cells[0].Value);
						goto IL_006f;
						IL_006f:
						num2 = 10;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_008b;
						}
						goto IL_0198;
						IL_008b:
						num2 = 11;
						text2 = "";
						goto IL_0095;
						IL_0095:
						num2 = 12;
						text = "";
						goto IL_009f;
						IL_009f:
						num2 = 13;
						num5 = 0;
						goto IL_00a5;
						IL_00a5:
						num2 = 14;
						num9 = GridFormat.ColumnCount - 1;
						num6 = 1;
						goto IL_0157;
						IL_0157:
						if (num6 <= num9)
						{
							goto IL_00bb;
						}
						goto IL_0160;
						IL_0160:
						num2 = 21;
						MyRptSpec = MyRptSpec + "\r\nFORMAT<\\\\>" + text3 + text + BuildReport.Add_Delimiter(NoCols, 2 + num5);
						goto IL_0198;
						IL_00bb:
						num2 = 15;
						text2 = Conversions.ToString(GridFormat.Rows[num7].Cells[num6].Value);
						goto IL_00e3;
						IL_00e3:
						num2 = 16;
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_00fc;
						}
						goto IL_014e;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 540;
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

	public static void Load_CSS_Grid(ref string[] MyRowFile, ref Globals_Renamed.Report_Format_Type[] CSSObj, int f_MaxObj, ref DataGridView GridFormat, int FormatCnt, int MyStart)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string value = default(string);
		int num6 = default(int);
		string left = default(string);
		string text = default(string);
		int num7 = default(int);
		bool flag = default(bool);
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
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 630:
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
								goto IL_0034;
							case 9:
								goto IL_0039;
							case 10:
								goto IL_0052;
							case 11:
								goto IL_005b;
							case 12:
								goto IL_006d;
							case 13:
								goto IL_007e;
							case 14:
								goto IL_0099;
							case 15:
								goto IL_00b8;
							case 16:
								goto IL_00be;
							case 17:
								goto IL_00c9;
							case 18:
								goto IL_0112;
							case 19:
								goto IL_012a;
							case 21:
							case 22:
								goto IL_0134;
							case 20:
							case 23:
								goto IL_0143;
							case 24:
								goto IL_014e;
							case 25:
								goto IL_0161;
							case 26:
								goto IL_018d;
							case 28:
							case 29:
								goto IL_01bb;
							case 27:
							case 30:
							case 31:
							case 32:
								goto IL_01ce;
							default:
								goto end_IL_0001;
							case 33:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_01bb:
						num2 = 29;
						num5++;
						goto IL_01c4;
						IL_0134:
						num2 = 22;
						num5++;
						goto IL_013d;
						IL_018d:
						num2 = 26;
						GridFormat.Rows[FormatCnt].Cells[num5].Value = Strings.LCase(value);
						goto IL_01ce;
						IL_01ce:
						num2 = 32;
						num6++;
						goto IL_01d5;
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
						left = "";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						value = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						num7 = 0;
						goto IL_0034;
						IL_0034:
						num2 = 8;
						flag = false;
						goto IL_0039;
						IL_0039:
						num2 = 9;
						num8 = Information.UBound(MyRowFile);
						num6 = MyStart;
						goto IL_01d5;
						IL_01d5:
						if (num6 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_0052;
						IL_0052:
						num2 = 10;
						text = MyRowFile[num6];
						goto IL_005b;
						IL_005b:
						num2 = 11;
						num7 = Strings.InStr(text, ":");
						goto IL_006d;
						IL_006d:
						num2 = 12;
						if (num7 != 0)
						{
							goto IL_007e;
						}
						goto IL_01ce;
						IL_007e:
						num2 = 13;
						left = Strings.LCase(Strings.Trim(Strings.Mid(text, 1, num7 - 1)));
						goto IL_0099;
						IL_0099:
						num2 = 14;
						value = Strings.Trim(Strings.Mid(text + " ", num7 + 1));
						goto IL_00b8;
						IL_00b8:
						num2 = 15;
						flag = false;
						goto IL_00be;
						IL_00be:
						num2 = 16;
						num9 = f_MaxObj;
						num5 = 0;
						goto IL_013d;
						IL_013d:
						if (num5 <= num9)
						{
							goto IL_00c9;
						}
						goto IL_0143;
						IL_00c9:
						num2 = 17;
						if ((Operators.CompareString(CSSObj[num5].ObjType, "headers", TextCompare: false) == 0) & (Operators.CompareString(left, Strings.Trim(Strings.LCase(CSSObj[num5].Value)), TextCompare: false) == 0))
						{
							goto IL_0112;
						}
						goto IL_0134;
						IL_0112:
						num2 = 18;
						left = Strings.LCase(CSSObj[num5].Name);
						goto IL_012a;
						IL_012a:
						num2 = 19;
						flag = true;
						goto IL_0143;
						IL_0143:
						num2 = 23;
						if (flag)
						{
							goto IL_014e;
						}
						goto IL_01ce;
						IL_014e:
						num2 = 24;
						num10 = GridFormat.ColumnCount - 1;
						num5 = 0;
						goto IL_01c4;
						IL_01c4:
						if (num5 <= num10)
						{
							goto IL_0161;
						}
						goto IL_01ce;
						IL_0161:
						num2 = 25;
						if (Operators.CompareString(left, Strings.LCase(GridFormat.Columns[num5].HeaderText), TextCompare: false) == 0)
						{
							goto IL_018d;
						}
						goto IL_01bb;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 630;
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

	public static bool GetColCase(TreeNode MyCurrNode, int MyMode)
	{
		bool result = false;
		switch (MyMode)
		{
		case 1:
			if (Conversions.ToBoolean(Conversions.ToBoolean(LikeOperator.LikeObject(MyCurrNode.Parent.Parent.Tag, "HTML-BUTTON*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(MyCurrNode.Parent.Parent.Tag, "JMP-BUTTON*", CompareMethod.Binary))))
			{
				if (LikeOperator.LikeString(MyCurrNode.Parent.Parent.Parent.Parent.Nodes[2].Text, "*{Yes}", CompareMethod.Binary))
				{
					result = true;
				}
			}
			else if (LikeOperator.LikeString(MyCurrNode.Parent.Parent.Nodes[2].Text, "*{Yes}", CompareMethod.Binary))
			{
				result = true;
			}
			break;
		case 2:
			if (LikeOperator.LikeString(MyCurrNode.Parent.Parent.Parent.Parent.Nodes[2].Text, "*{Yes}", CompareMethod.Binary))
			{
				result = true;
			}
			break;
		}
		return result;
	}

	public static bool Chk_OK_Case_Nodes(ref TreeNode MyNode)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		bool result = default(bool);
		int num = default(int);
		int num3 = default(int);
		TreeNode treeNode = default(TreeNode);
		IEnumerator enumerator = default(IEnumerator);
		IEnumerator enumerator2 = default(IEnumerator);
		TreeNode treeNode2 = default(TreeNode);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					result = true;
					goto IL_0006;
				case 428:
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
							goto IL_0006;
						case 3:
							goto IL_000f;
						case 4:
							goto IL_0037;
						case 5:
							goto IL_0059;
						case 6:
							goto IL_007c;
						case 7:
							goto IL_00f3;
						case 9:
						case 10:
							goto IL_00fb;
						case 8:
						case 11:
							goto IL_010e;
						case 13:
						case 14:
							goto IL_012b;
						case 12:
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_007c:
					num2 = 6;
					if (Conversions.ToBoolean(Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "HTML-CHARTR*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "HTML-CHARTP*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "HTML-R-PROGRAM*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "JMP-LOAD DATA*", CompareMethod.Binary))))
					{
						goto IL_00f3;
					}
					goto IL_00fb;
					IL_010e:
					num2 = 11;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					break;
					IL_00f3:
					num2 = 7;
					result = false;
					goto IL_010e;
					IL_012b:
					num2 = 14;
					goto IL_012e;
					IL_0006:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					enumerator2 = MyNode.Parent.Nodes.GetEnumerator();
					goto IL_012e;
					IL_012e:
					if (!enumerator2.MoveNext())
					{
						break;
					}
					treeNode2 = (TreeNode)enumerator2.Current;
					goto IL_0037;
					IL_00fb:
					num2 = 10;
					goto IL_00fe;
					IL_0037:
					num2 = 4;
					if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, "SCRIPT:*", CompareMethod.Binary)))
					{
						goto IL_0059;
					}
					goto IL_012b;
					IL_0059:
					num2 = 5;
					enumerator = treeNode2.Nodes.GetEnumerator();
					goto IL_00fe;
					IL_00fe:
					if (enumerator.MoveNext())
					{
						treeNode = (TreeNode)enumerator.Current;
						goto IL_007c;
					}
					goto IL_010e;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				if (enumerator2 is IDisposable)
				{
					(enumerator2 as IDisposable).Dispose();
				}
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 428;
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

	public static void Add_Labels_To_Options(ref TabPage TabOptions, int Starti, int Endi, int l_TopMax)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num5 = default(int);
		int num = default(int);
		int num3 = default(int);
		Label label = default(Label);
		int num6 = default(int);
		int num7 = default(int);
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
						num2 = 1;
						num5 = 0;
						goto IL_0006;
					case 433:
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
								goto IL_0006;
							case 3:
								goto IL_000f;
							case 4:
								goto IL_0015;
							case 5:
								goto IL_0022;
							case 6:
								goto IL_0028;
							case 7:
								goto IL_0038;
							case 8:
								goto IL_0041;
							case 9:
								goto IL_005d;
							case 10:
								goto IL_007d;
							case 11:
								goto IL_0089;
							case 12:
								goto IL_0095;
							case 13:
								goto IL_00a3;
							case 14:
								goto IL_00b2;
							case 15:
								goto IL_00be;
							case 17:
								goto IL_00ce;
							case 18:
								goto IL_00de;
							case 16:
							case 19:
							case 20:
								goto IL_00ec;
							case 21:
								goto IL_011f;
							case 22:
								goto IL_0131;
							default:
								goto end_IL_0001;
							case 23:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_00ec:
						num2 = 20;
						label.Top = (int)Math.Round((Math.Floor((double)num5 / 2.0) - 1.0) * (double)num6 + (double)l_TopMax);
						goto IL_011f;
						IL_011f:
						num2 = 21;
						TabOptions.Controls.Add(label);
						goto IL_0131;
						IL_00de:
						num2 = 18;
						label.Anchor = AnchorStyles.Top | AnchorStyles.Right;
						goto IL_00ec;
						IL_0131:
						num2 = 22;
						num5++;
						goto IL_0138;
						IL_0006:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num6 = 48;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						if (Endi < 20)
						{
							goto IL_0022;
						}
						goto IL_0028;
						IL_0022:
						num2 = 5;
						num6 = 55;
						goto IL_0028;
						IL_0028:
						num2 = 6;
						num7 = Endi;
						num5 = Starti;
						goto IL_0138;
						IL_0138:
						if (num5 > num7)
						{
							goto end_IL_0001_2;
						}
						goto IL_0038;
						IL_0038:
						num2 = 7;
						label = new Label();
						goto IL_0041;
						IL_0041:
						num2 = 8;
						label.Name = "LblOption" + Conversions.ToString(num5 + 1);
						goto IL_005d;
						IL_005d:
						num2 = 9;
						label.Text = "Option " + Conversions.ToString(num5) + " Label";
						goto IL_007d;
						IL_007d:
						num2 = 10;
						label.Visible = false;
						goto IL_0089;
						IL_0089:
						num2 = 11;
						label.AutoSize = true;
						goto IL_0095;
						IL_0095:
						num2 = 12;
						label.TabIndex = num5 - 2;
						goto IL_00a3;
						IL_00a3:
						num2 = 13;
						if (unchecked(num5 % 2) == 0)
						{
							goto IL_00b2;
						}
						goto IL_00ce;
						IL_00b2:
						num2 = 14;
						label.Left = 3;
						goto IL_00be;
						IL_00be:
						num2 = 15;
						label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
						goto IL_00ec;
						IL_00ce:
						num2 = 17;
						label.Left = 326;
						goto IL_00de;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 433;
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

	public static void Chart_Clear_Color(ref DataGridView GridY, int MyCol1, int MyCol2)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short num6 = default(short);
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
				case 283:
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
							goto IL_002c;
						case 5:
							goto IL_0031;
						case 6:
							goto IL_006d;
						case 7:
							goto IL_0082;
						case 8:
							goto IL_008f;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008f:
					num2 = 8;
					GridY.Rows[num5].Cells[num6].Value = "";
					break;
					IL_0082:
					num2 = 7;
					if (num7 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_008f;
					IL_0031:
					num2 = 5;
					if ((num6 != MyCol1 && num6 != MyCol2) || !Operators.ConditionalCompareObjectNotEqual(GridY.Rows[num5].Cells[num6].Value, "", TextCompare: false))
					{
						goto end_IL_0001_3;
					}
					goto IL_006d;
					IL_006d:
					num2 = 6;
					num7 = (int)Interaction.MsgBox("Clear color selection?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Clear");
					goto IL_0082;
					IL_000b:
					num2 = 2;
					num6 = checked((short)GridY.CurrentCell.ColumnIndex);
					goto IL_001b;
					IL_001b:
					num2 = 3;
					num5 = checked((short)GridY.CurrentCell.RowIndex);
					goto IL_002c;
					IL_002c:
					num2 = 4;
					num7 = 0;
					goto IL_0031;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				GridY[num6, num5].Style.BackColor = Color.White;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 283;
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

	public static void Chart_Set_Color(ref DataGridView GridY, ref ColorDialog ColorDialog1, int MyCol1, int MyCol2)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowIndex = default(int);
		int columnIndex = default(int);
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
				case 400:
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
							goto IL_002a;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_007e;
						case 8:
						case 9:
							goto IL_00b2;
						case 10:
							goto IL_00be;
						case 11:
							goto IL_00d1;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004d:
					num2 = 6;
					if (Operators.ConditionalCompareObjectNotEqual(GridY.Rows[rowIndex].Cells[columnIndex].Value, "", TextCompare: false))
					{
						goto IL_007e;
					}
					goto IL_00b2;
					IL_007e:
					num2 = 7;
					ColorDialog1.Color = ColorTranslator.FromHtml(Conversions.ToString(GridY.Rows[rowIndex].Cells[columnIndex].Value));
					goto IL_00b2;
					IL_00d1:
					num2 = 11;
					GridY.Rows[rowIndex].Cells[columnIndex].Value = "#" + ColorDialog1.Color.ToArgb().ToString("X8").Substring(2, 6);
					break;
					IL_00b2:
					num2 = 9;
					ColorDialog1.FullOpen = true;
					goto IL_00be;
					IL_000b:
					num2 = 2;
					columnIndex = GridY.CurrentCell.ColumnIndex;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					rowIndex = GridY.CurrentCell.RowIndex;
					goto IL_002a;
					IL_002a:
					num2 = 4;
					if (rowIndex == -1 || (columnIndex != MyCol1 && columnIndex != MyCol2))
					{
						goto end_IL_0001_3;
					}
					goto IL_004d;
					IL_00be:
					num2 = 10;
					if (ColorDialog1.ShowDialog() != DialogResult.OK)
					{
						goto end_IL_0001_3;
					}
					goto IL_00d1;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				GridY[columnIndex, rowIndex].Style.BackColor = ColorDialog1.Color;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 400;
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

	public static void Chart_Color_MouseUp(ref DataGridView GridY, ref ContextMenuStrip ContextMenuColEdit, int MyCol1, int MyCol2, int x, int y)
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
						ProjectData.ClearProjectError();
						num2 = 2;
						if (GridY.RowCount != 0)
						{
							short num3 = (short)GridY.CurrentCell.ColumnIndex;
							short num4 = (short)GridY.CurrentCell.RowIndex;
							if (num3 == MyCol1 || num3 == MyCol2)
							{
								Point position = new Point(x, y);
								ContextMenuColEdit.Show(GridY, position);
							}
						}
						goto end_IL_0001;
					case 145:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, "BuildChart - Chart_Color_MouseUp", Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					goto IL_00c7;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 145;
				continue;
			}
			break;
			IL_00c7:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static void Click_Y_For_Grid(ref DataGridView GridY, ref ListBox lstcolumns, bool DoNumber)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int index = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		string text = default(string);
		int num9 = default(int);
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
					case 474:
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
								goto IL_0019;
							case 6:
								goto IL_001e;
							case 7:
								goto IL_0027;
							case 8:
								goto IL_0032;
							case 10:
								goto IL_0044;
							case 11:
								goto IL_005f;
							case 12:
								goto IL_006a;
							case 13:
								goto IL_007e;
							case 14:
								goto IL_00a2;
							case 15:
								goto IL_00c5;
							case 16:
								goto IL_00ef;
							case 17:
								goto IL_0102;
							case 18:
								goto IL_0129;
							case 19:
								goto IL_0138;
							case 20:
								goto IL_014a;
							case 21:
								goto IL_0155;
							case 22:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 9:
							case 23:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0138:
						num2 = 19;
						num5++;
						goto IL_0141;
						IL_0102:
						num2 = 17;
						GridY.Rows[index].Cells[num6].Value = "";
						goto IL_0129;
						IL_0132:
						if (num6 <= num7)
						{
							goto IL_0102;
						}
						goto IL_0138;
						IL_0129:
						num2 = 18;
						num6++;
						goto IL_0132;
						IL_000b:
						num2 = 2;
						index = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num5 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						num8 = -1;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						num6 = 0;
						goto IL_001e;
						IL_001e:
						num2 = 6;
						text = "";
						goto IL_0027;
						IL_0027:
						num2 = 7;
						num8 = lstcolumns.SelectedIndex;
						goto IL_0032;
						IL_0032:
						num2 = 8;
						if (num8 == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_0044;
						IL_0044:
						num2 = 10;
						num9 = lstcolumns.SelectedIndices.Count - 1;
						num5 = 0;
						goto IL_0141;
						IL_0141:
						if (num5 <= num9)
						{
							goto IL_005f;
						}
						goto IL_014a;
						IL_014a:
						num2 = 20;
						lstcolumns.Focus();
						goto IL_0155;
						IL_0155:
						num2 = 21;
						if (!DoNumber)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_005f:
						num2 = 11;
						index = GridY.RowCount;
						goto IL_006a;
						IL_006a:
						num2 = 12;
						GridY.RowCount += 1;
						goto IL_007e;
						IL_007e:
						num2 = 13;
						text = Conversions.ToString(lstcolumns.Items[lstcolumns.SelectedIndices[num5]]);
						goto IL_00a2;
						IL_00a2:
						num2 = 14;
						GridY.Rows[index].Cells[0].Value = text;
						goto IL_00c5;
						IL_00c5:
						num2 = 15;
						GridY.Rows[index].Cells[1].Value = Strings.StrConv(text, VbStrConv.ProperCase);
						goto IL_00ef;
						IL_00ef:
						num2 = 16;
						num7 = GridY.ColumnCount - 1;
						num6 = 2;
						goto IL_0132;
						end_IL_0001_2:
						break;
					}
					num2 = 22;
					GridModule.Number_Grid(ref GridY);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 474;
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

	public static string Substitute_Title(string MyTitle, string MyTemplateType)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string result = default(string);
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
						errsource = "BuildChart - Substitute_Title";
						string text = "";
						int num3 = 0;
						int num4 = 0;
						string text2 = "";
						string text3 = "";
						result = MyTitle;
						if (Strings.InStr(MyTitle, "{") == 0)
						{
							goto end_IL_0001;
						}
						MyTitle = Strings.Replace(MyTitle, "<<<", "{", 1, -1, CompareMethod.Text);
						MyTitle = Strings.Replace(MyTitle, ">>>", "}", 1, -1, CompareMethod.Text);
						string[] array = Strings.Split(MyTitle, "{");
						if (Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0)
						{
							text = array[0];
						}
						else if (Operators.CompareString(array[0], "", TextCompare: false) != 0)
						{
							text = array[0] + "\"";
						}
						int num5 = Information.UBound(array);
						for (num3 = 1; num3 <= num5; num3++)
						{
							num4 = Strings.InStr(array[num3], "}");
							text2 = ((num4 >= Strings.Len(array[num3])) ? "" : Strings.Mid(array[num3], num4 + 1));
							if (num4 > 1)
							{
								text3 = Strings.Mid(array[num3], 1, num4 - 1);
								if (LikeOperator.LikeString(text3.ToLower(), "spf_by_col*", CompareMethod.Binary))
								{
									text = text + "," + Strings.Trim(text3);
								}
								else if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) != 0) ? (text + ",<<<chart-table>>>$" + Strings.Trim(text3) + "<<<chart-where0>>>[1]") : (text + "{df[\"" + Strings.Trim(text3) + "\"].iloc[0]}"));
								}
							}
							if (Operators.CompareString(text2, "", TextCompare: false) != 0)
							{
								text = ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) != 0) ? (text + ",\"" + text2 + "\"") : (text + text2));
							}
						}
						array = null;
						if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0)
						{
							text += ",\"";
						}
						result = text;
						goto end_IL_0001;
					}
					case 649:
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
				try0001_dispatch = 649;
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
		return result;
	}

	public static string Strip_Custom_Col(string MyValue, ref string MyColumn)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string result = default(string);
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
				case 159:
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
							goto IL_0019;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_0031;
						case 7:
							goto IL_003e;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0021:
					num2 = 5;
					num5 = Strings.InStr(MyValue, "~~~");
					goto IL_0031;
					IL_0031:
					num2 = 6;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_003e;
					IL_0019:
					num2 = 4;
					result = "";
					goto IL_0021;
					IL_003e:
					num2 = 7;
					MyColumn = Strings.Mid(MyValue, 1, checked(num5 - 1));
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					MyColumn = "";
					goto IL_0019;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				result = Strings.Mid(MyValue, checked(num5 + Strings.Len("~~~")));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 159;
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

	public static void Generate_WMap_Expr(string MyValue, string MyTable, ref string MyScript, string MyGroup)
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
						errsource = "BuildChart - Generate_WMap_Expr";
						string text = "";
						int num3 = 0;
						int num4 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string replacement = "";
						string replacement2 = "";
						string text6 = "";
						string text7 = "";
						string MyColumn = "";
						int num5 = 0;
						MyValue = Strip_Custom_Col(MyValue, ref MyColumn);
						if (Operators.CompareString(Strings.Trim(MyValue), "", TextCompare: false) != 0 && Operators.CompareString(MyTable, "", TextCompare: false) != 0 && Operators.CompareString(MyColumn, "", TextCompare: false) != 0)
						{
							text2 = MyTable + " <- transform(" + MyTable + ",my.group.col.wm=";
							text5 = MyTable + " <- transform(" + MyTable + ",mygroupcolcolorwm=";
							text4 = MyTable + " <- transform(" + MyTable + ",mygroupcolsortwm=";
							text3 = "my.color.wm<-levels(reorder(" + MyTable + "$mygroupcolcolorwm," + MyTable + "$mygroupcolsortwm,fun=NA));";
							replacement = ",par.settings = simpleTheme(c(col=my.color.wm,fill=my.color.wm))";
							replacement2 = ",fill=my.color.wm";
							text6 = "my.color.wm.grid=c(";
							if (Operators.CompareString(Strings.Trim(MyValue), "", TextCompare: false) != 0)
							{
								string[] array = Strings.Split(MyValue, "~~~");
								int num6 = Information.UBound(array);
								for (num3 = 0; num3 <= num6; num3++)
								{
									string[] array2 = Strings.Split(array[num3], ";;;");
									num4++;
									text7 = ((num4 > 9) ? Conversions.ToString(num4) : (" " + Conversions.ToString(num4)));
									string text8 = array2[0];
									string text9 = array2[1];
									if (Operators.CompareString(Strings.LCase(text8), "between", TextCompare: false) == 0)
									{
										num5 = Strings.InStr(text9, ",");
										if (num5 != 0)
										{
											text8 = Strings.Trim(Strings.Mid(text9, 1, num5 - 1));
											if (Operators.CompareString(text8, "", TextCompare: false) == 0)
											{
												text8 = "''";
											}
											text9 = Strings.Trim(Strings.Mid(text9, num5 + 1));
											if (Operators.CompareString(text9, "", TextCompare: false) == 0)
											{
												text9 = "''";
											}
											text8 = ">= " + text8 + " & " + MyColumn + " <= " + text9;
											text9 = "";
										}
									}
									text2 = text2 + "\r\n" + text + "ifelse(" + MyColumn + " " + text8 + " " + text9 + ",'" + text7 + ". " + Strings.Replace(array2[2], "'", "", 1, -1, CompareMethod.Text) + "'";
									if (Operators.CompareString(array2[3], "", TextCompare: false) == 0)
									{
										array2[3] = "white";
									}
									text5 = text5 + "\r\n" + text + "ifelse(" + MyColumn + " " + text8 + " " + text9 + ",'" + array2[3] + "'";
									text4 = text4 + "\r\n" + text + "ifelse(" + MyColumn + " " + text8 + " " + text9 + "," + Conversions.ToString(num4);
									text6 = text6 + text + "'" + array2[3] + "'\r\n";
									text = ",";
									array2 = null;
								}
								array = null;
							}
							text7 = ((num4 + 1 >= 9) ? Conversions.ToString(num4 + 1) : (" " + Conversions.ToString(num4 + 1)));
							text2 = text2 + ",'" + text7 + ". Other '";
							text5 += ",'whitesmoke'";
							text4 = text4 + "," + text7;
							text6 += ",'whitesmoke');";
							int num7 = num4;
							for (num3 = 1; num3 <= num7; num3++)
							{
								text2 += ")";
								text5 += ")";
								text4 += ")";
							}
							text2 += ");";
							text5 += ");";
							text4 += ");";
							text2 = text2 + "\r\n" + text5 + "\r\n" + text4 + "\r\n" + text3 + "\r\n" + text6;
						}
						else
						{
							text2 = "<<<chart-table>>> <- transform(<<<chart-table>>>,mygroupcolsortwm=as.numeric(factor(<<<chart-table>>>$" + MyGroup + ")));";
							text2 = text2 + "\r\nn2<-length(levels(factor(<<<chart-table>>>$" + MyGroup + ")));";
							text2 += "\r\nmy.color.wm<-<<<init$heat.colors(n2)$color-palette>>>;my.color.wm.grid<-<<<init$heat.colors(n2)$color-palette>>>;";
						}
						MyScript = Strings.Replace(MyScript, "<<<wafer-map-expr>>>", text2, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<wafer-map-color-1>>>", replacement, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<wafer-map-color-2>>>", replacement2, 1, -1, CompareMethod.Text);
						goto end_IL_0001;
					}
					case 1523:
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
					goto IL_0629;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1523;
				continue;
			}
			break;
			IL_0629:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static void Get_CS_and_Label(string MyMode, string MyColumn, ref string MyCS, ref string MyLabel)
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
				case 255:
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
							goto IL_0019;
						case 5:
							goto IL_002e;
						case 6:
							goto IL_0037;
						case 7:
							goto IL_003c;
						case 8:
							goto IL_004d;
						case 9:
							goto IL_005a;
						case 10:
							goto IL_0078;
						case 11:
							goto IL_0088;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 14:
						case 15:
						case 16:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005a:
					num2 = 9;
					text = Strings.Trim(Strings.Mid(MyColumn + " ", checked(num5 + 1)));
					goto IL_0078;
					IL_0078:
					num2 = 10;
					if (!Versioned.IsNumeric(text))
					{
						goto end_IL_0001_3;
					}
					goto IL_0088;
					IL_004d:
					num2 = 8;
					if (num5 <= 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_005a;
					IL_0088:
					num2 = 11;
					MyCS = text;
					break;
					IL_000b:
					num2 = 2;
					MyLabel = MyColumn;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					MyCS = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (Operators.CompareString(MyMode, "JSL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_002e;
					IL_002e:
					num2 = 5;
					text = "";
					goto IL_0037;
					IL_0037:
					num2 = 6;
					num5 = 0;
					goto IL_003c;
					IL_003c:
					num2 = 7;
					num5 = Strings.InStrRev(MyColumn, ":");
					goto IL_004d;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				MyLabel = Strings.Trim(Strings.Mid(MyColumn, 1, checked(num5 - 1)));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 255;
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

	public static void Init_Options(int l_NoOptsTabCols, ref TabPage TabCols, ref TabPage TabOptions, ref ToolTip ToolTip1)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myType = default(string);
		int num5 = default(int);
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
				case 344:
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
							goto IL_001c;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_0026;
						case 7:
							goto IL_0050;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_0080;
						case 11:
							goto IL_008e;
						case 10:
						case 12:
						case 13:
							goto IL_0099;
						case 14:
						case 15:
							goto IL_00f1;
						default:
							goto end_IL_0001;
						case 16:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_008e:
					num2 = 11;
					myType = "COMBOBOX";
					goto IL_0099;
					IL_0099:
					num2 = 13;
					Create_Option_Control(num5, myType, l_NoOptsTabCols, ref TabCols, ref TabOptions, ref ToolTip1, Globals_Renamed.gOptOptions[num5].Label, Globals_Renamed.gOptOptions[num5].Default_value, text, Globals_Renamed.gOptOptions[num5].Selected, Globals_Renamed.gOptOptions[num5].Help);
					goto IL_00f1;
					IL_0080:
					num2 = 9;
					myType = "CHECKBOX";
					goto IL_0099;
					IL_00f1:
					num2 = 15;
					num5 = checked(num5 + 1);
					if (num5 > 25)
					{
						goto end_IL_0001_2;
					}
					goto IL_0026;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					myType = "COMBOBOX";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					num5 = 0;
					goto IL_0021;
					IL_0021:
					num2 = 5;
					num5 = 0;
					goto IL_0026;
					IL_0026:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.gOptOptions[num5].Variable, "N/A", TextCompare: false) != 0)
					{
						goto IL_0050;
					}
					goto IL_00f1;
					IL_0050:
					num2 = 7;
					text = Globals_Renamed.gOptOptions[num5].Show;
					goto IL_0064;
					IL_0064:
					num2 = 8;
					if (Operators.CompareString(Strings.UCase(text), "ON|OFF", TextCompare: false) == 0)
					{
						goto IL_0080;
					}
					goto IL_008e;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 344;
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

	public static void Create_Option_Control(int MyNo, string MyType, int l_NoOptsTabCols, ref TabPage TabCols, ref TabPage TabOptions, ref ToolTip ToolTip1, string MyLabel, string MyDefault, string MyData, string MySelected, string MyHelp)
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
						errsource = "BuildChart - Create_Option_Control";
						int MyLeft = 0;
						int MyTop = 0;
						int num3 = 0;
						string left = Strings.UCase(MyType);
						if (Operators.CompareString(left, "CHECKBOX", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "COMBOBOX", TextCompare: false) != 0)
							{
								goto end_IL_0001;
							}
							ComboBox comboBox = new ComboBox();
							Get_Control_Position(MyNo, ref MyLeft, ref MyTop, l_NoOptsTabCols, ref TabCols, ref TabOptions);
							comboBox.Name = "CONTROL" + Conversions.ToString(MyNo);
							if (Operators.CompareString(Strings.UCase(MyData), "@COLUMNS@", TextCompare: false) == 0)
							{
								comboBox.Tag = "COMBOBOXC";
							}
							else
							{
								comboBox.Tag = "COMBOBOX";
							}
							comboBox.Width = 164;
							comboBox.DropDownWidth = 200;
							Set_Option_Label(MyNo, MyLabel, l_NoOptsTabCols, ref TabCols, ref TabOptions);
							comboBox.Items.Clear();
							if (Operators.CompareString(Strings.UCase(MyData), "@COLUMNS@", TextCompare: false) != 0)
							{
								int num4 = 0;
								string[] array = Strings.Split(MyData, "|");
								num4 = Information.UBound(array);
								int num5 = num4;
								for (num3 = 0; num3 <= num5; num3++)
								{
									comboBox.Items.Add(array[num3]);
								}
								array = null;
							}
							if (Operators.CompareString(MySelected, "N/A", TextCompare: false) == 0)
							{
								comboBox.Text = MyDefault;
							}
							else
							{
								comboBox.Text = MySelected;
							}
							if (comboBox.Left != 3)
							{
								comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
							}
							comboBox.Visible = true;
							if (MyNo <= l_NoOptsTabCols)
							{
								TabCols.Controls.Add(comboBox);
							}
							else
							{
								TabOptions.Controls.Add(comboBox);
							}
							comboBox.Left = MyLeft;
							comboBox.Top = MyTop + 20;
							comboBox.SelectionStart = 0;
							comboBox.SelectionLength = 0;
							if ((Operators.CompareString(MyHelp, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(MyHelp), "N/A", TextCompare: false) != 0))
							{
								ToolTip1.SetToolTip(comboBox, MyHelp);
							}
							comboBox.Visible = true;
							goto end_IL_0001;
						}
						CheckBox checkBox = new CheckBox();
						checkBox.Appearance = Appearance.Normal;
						Get_Control_Position(MyNo, ref MyLeft, ref MyTop, l_NoOptsTabCols, ref TabCols, ref TabOptions);
						checkBox.AutoSize = true;
						checkBox.Name = "CONTROL" + Conversions.ToString(MyNo);
						checkBox.Tag = "CHECKBOX";
						checkBox.Text = MyLabel;
						if (Operators.CompareString(Strings.UCase(MyDefault), "ON", TextCompare: false) == 0)
						{
							checkBox.Checked = true;
						}
						if (Operators.CompareString(MySelected, "N/A", TextCompare: false) != 0)
						{
							if (Operators.CompareString(MySelected, "1", TextCompare: false) == 0)
							{
								checkBox.Checked = true;
							}
							else
							{
								checkBox.Checked = false;
							}
						}
						checkBox.Visible = true;
						if (MyNo <= l_NoOptsTabCols)
						{
							TabCols.Controls.Add(checkBox);
						}
						else
						{
							TabOptions.Controls.Add(checkBox);
						}
						checkBox.Left = MyLeft;
						checkBox.Top = MyTop + 10;
						if ((Operators.CompareString(MyHelp, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(MyHelp), "N/A", TextCompare: false) != 0))
						{
							ToolTip1.SetToolTip(checkBox, MyHelp);
						}
						if (checkBox.Left != 3)
						{
							checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
						}
						checkBox.Visible = true;
						goto end_IL_0001;
					}
					case 989:
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
				try0001_dispatch = 989;
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

	public static void Set_Option_Label(int MyNo, string MyLabel, int l_NoOptsTabCols, ref TabPage TabCols, ref TabPage TabOptions)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		Control control = default(Control);
		TabPage tabPage = default(TabPage);
		IEnumerator enumerator = default(IEnumerator);
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
				case 297:
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
						case 5:
							goto IL_0023;
						case 4:
						case 6:
						case 7:
							goto IL_002a;
						case 8:
							goto IL_0049;
						case 9:
							goto IL_0090;
						case 10:
							goto IL_009c;
						case 12:
						case 13:
							goto IL_00ac;
						case 11:
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0049:
					num2 = 8;
					if ((control is Label) & (Operators.CompareString(Strings.Trim(Strings.UCase(control.Text)), "OPTION " + Strings.Trim(Conversions.ToString(MyNo)) + " LABEL", TextCompare: false) == 0))
					{
						goto IL_0090;
					}
					goto IL_00ac;
					IL_0090:
					num2 = 9;
					control.Text = MyLabel;
					goto IL_009c;
					IL_00ac:
					num2 = 13;
					goto IL_00af;
					IL_009c:
					num2 = 10;
					control.Visible = true;
					break;
					IL_000b:
					num2 = 2;
					if (MyNo <= l_NoOptsTabCols)
					{
						goto IL_001a;
					}
					goto IL_0023;
					IL_001a:
					num2 = 3;
					tabPage = TabCols;
					goto IL_002a;
					IL_0023:
					num2 = 5;
					tabPage = TabOptions;
					goto IL_002a;
					IL_002a:
					num2 = 7;
					enumerator = tabPage.Controls.GetEnumerator();
					goto IL_00af;
					IL_00af:
					if (!enumerator.MoveNext())
					{
						break;
					}
					control = (Control)enumerator.Current;
					goto IL_0049;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 297;
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

	public static void Get_Control_Position(int MyNo, ref int MyLeft, ref int MyTop, int l_NoOptsTabCols, ref TabPage TabCols, ref TabPage TabOptions)
	{
		TabPage tabPage = ((MyNo > l_NoOptsTabCols) ? TabOptions : TabCols);
		foreach (Control control in tabPage.Controls)
		{
			if ((control is Label) & (Operators.CompareString(Strings.Trim(Strings.UCase(control.Text)), "OPTION " + Strings.Trim(Conversions.ToString(MyNo)) + " LABEL", TextCompare: false) == 0))
			{
				MyLeft = control.Left;
				MyTop = control.Top;
				break;
			}
		}
	}

	public static void Format_Grid(ref DataGridView MyGrid, int Colf_FC)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int colf_ff = default(int);
		int colf_fsz = default(int);
		int colf_fw = default(int);
		int colf_fst = default(int);
		int colf_td = default(int);
		int num7 = default(int);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num6;
					string text;
					string text2;
					string text3;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 605:
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
								goto IL_0036;
							case 9:
								goto IL_003d;
							case 10:
								goto IL_0045;
							case 11:
								goto IL_004d;
							case 12:
								goto IL_0055;
							case 13:
								goto IL_005d;
							case 14:
								goto IL_0072;
							case 15:
								goto IL_00a7;
							case 16:
								goto IL_00e7;
							case 17:
							case 18:
								goto IL_0128;
							case 19:
								goto IL_0159;
							case 20:
								goto IL_0197;
							case 21:
							case 22:
								goto IL_01c1;
							case 23:
								goto IL_01d9;
							default:
								goto end_IL_0001;
							case 24:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0197:
						num2 = 20;
						MyGrid[Colf_FC, num5].Style.Font = Set_Font_Format(num5, ref MyGrid, colf_ff, colf_fsz, colf_fw, colf_fst, colf_td);
						goto IL_01c1;
						IL_01c1:
						num2 = 22;
						MyGrid.Rows[num5].Height = 25;
						goto IL_01d9;
						IL_0159:
						num2 = 19;
						MyGrid[Colf_FC, num5].Style.ForeColor = ColorTranslator.FromHtml(Conversions.ToString(MyGrid.Rows[num5].Cells[Colf_FC].Value));
						goto IL_0197;
						IL_01d9:
						num2 = 23;
						num5++;
						goto IL_01e0;
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
						text = "";
						goto IL_001d;
						IL_001d:
						num2 = 5;
						text2 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text3 = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						num7 = Colf_FC - 1;
						goto IL_0036;
						IL_0036:
						num2 = 8;
						colf_ff = Colf_FC + 1;
						goto IL_003d;
						IL_003d:
						num2 = 9;
						colf_fsz = Colf_FC + 2;
						goto IL_0045;
						IL_0045:
						num2 = 10;
						colf_fst = Colf_FC + 3;
						goto IL_004d;
						IL_004d:
						num2 = 11;
						colf_fw = Colf_FC + 4;
						goto IL_0055;
						IL_0055:
						num2 = 12;
						colf_td = Colf_FC + 6;
						goto IL_005d;
						IL_005d:
						num2 = 13;
						num8 = MyGrid.RowCount - 1;
						num5 = 0;
						goto IL_01e0;
						IL_01e0:
						if (num5 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_0072;
						IL_0072:
						num2 = 14;
						if (Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[num5].Cells[num7].Value, "", TextCompare: false))
						{
							goto IL_00a7;
						}
						goto IL_0128;
						IL_00a7:
						num2 = 15;
						MyGrid[num7, num5].Style.BackColor = ColorTranslator.FromHtml(Conversions.ToString(MyGrid.Rows[num5].Cells[num7].Value));
						goto IL_00e7;
						IL_00e7:
						num2 = 16;
						MyGrid[Colf_FC, num5].Style.BackColor = ColorTranslator.FromHtml(Conversions.ToString(MyGrid.Rows[num5].Cells[num7].Value));
						goto IL_0128;
						IL_0128:
						num2 = 18;
						if (Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[num5].Cells[Colf_FC].Value, "", TextCompare: false))
						{
							goto IL_0159;
						}
						goto IL_01c1;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 605;
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

	public static void Grid_Format_DC(ref DataGridView MyGrid, ref FontDialog FontDialog1, ref ColorDialog ColorDialog1, int ColF_FC)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int rowIndex = default(int);
		int num4 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		string errsource = default(string);
		int columnIndex = default(int);
		int[] customColors = default(int[]);
		int num10 = default(int);
		int num11 = default(int);
		int num12 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num9;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 1754:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
							case 3:
								break;
							case 1:
								goto IL_059e;
							default:
								goto end_IL_0001;
							}
							goto IL_056d;
						}
						IL_04b3:
						num2 = 67;
						MyGrid.Rows[rowIndex].Cells[num4].Value = Conversions.ToString(FontDialog1.Font.Size);
						goto IL_04e7;
						IL_04e7:
						num2 = 68;
						MyGrid[ColF_FC, rowIndex].Style.Font = Set_Font_Format(rowIndex, ref MyGrid, num5, num4, num6, num7, num8);
						goto IL_0511;
						IL_047f:
						num2 = 66;
						MyGrid.Rows[rowIndex].Cells[num5].Value = Strings.LCase(FontDialog1.Font.Name);
						goto IL_04b3;
						IL_059e:
						num9 = unchecked(num + 1);
						num = 0;
						switch (num9)
						{
						case 1:
							break;
						case 2:
							goto IL_000a;
						case 3:
							goto IL_0012;
						case 4:
							goto IL_0022;
						case 5:
							goto IL_0032;
						case 6:
							goto IL_0067;
						case 7:
							goto IL_006e;
						case 8:
							goto IL_0075;
						case 9:
							goto IL_007c;
						case 10:
							goto IL_0084;
						case 11:
							goto IL_008c;
						case 12:
							goto IL_0094;
						case 13:
							goto IL_009a;
						case 15:
							goto IL_00ae;
						case 17:
							goto IL_00b5;
						case 18:
							goto IL_00c7;
						case 19:
							goto IL_00fa;
						case 20:
						case 21:
							goto IL_0130;
						case 22:
							goto IL_013c;
						case 23:
							goto IL_0149;
						case 24:
							goto IL_015c;
						case 25:
							goto IL_018b;
						case 26:
							goto IL_01ab;
						case 29:
							goto IL_01d1;
						case 30:
							goto IL_01f1;
						case 31:
							goto IL_0226;
						case 32:
							goto IL_0234;
						case 33:
							goto IL_023d;
						case 34:
							goto IL_0270;
						case 35:
							goto IL_0286;
						case 36:
							goto IL_029c;
						case 37:
							goto IL_02aa;
						case 38:
						case 40:
						case 41:
						case 42:
							goto IL_02c6;
						case 43:
							goto IL_02d4;
						case 44:
						case 45:
							goto IL_02de;
						case 46:
							goto IL_02fb;
						case 47:
							goto IL_0307;
						case 48:
							goto IL_0313;
						case 49:
							goto IL_031f;
						case 50:
							goto IL_032b;
						case 51:
							goto IL_0341;
						case 52:
							goto IL_0356;
						case 54:
							goto IL_0382;
						case 53:
						case 55:
						case 56:
							goto IL_03ab;
						case 57:
							goto IL_03c0;
						case 59:
							goto IL_03ec;
						case 58:
						case 60:
						case 61:
							goto IL_0415;
						case 62:
							goto IL_042a;
						case 64:
							goto IL_0456;
						case 63:
						case 65:
						case 66:
							goto IL_047f;
						case 67:
							goto IL_04b3;
						case 68:
							goto IL_04e7;
						case 69:
							goto IL_0511;
						case 70:
							goto IL_0548;
						case 73:
							goto IL_056d;
						case 74:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 16:
						case 27:
						case 28:
						case 39:
						case 71:
						case 72:
						case 75:
							goto end_IL_0001_3;
						}
						goto default;
						IL_056d:
						num2 = 73;
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						break;
						IL_000a:
						num2 = 2;
						errsource = "BuildChart - Grid_Format_DC";
						goto IL_0012;
						IL_0012:
						num2 = 3;
						columnIndex = MyGrid.CurrentCell.ColumnIndex;
						goto IL_0022;
						IL_0022:
						num2 = 4;
						rowIndex = MyGrid.CurrentCell.RowIndex;
						goto IL_0032;
						IL_0032:
						num2 = 5;
						customColors = new int[2]
						{
							Information.RGB(167, 201, 66),
							Information.RGB(234, 242, 211)
						};
						goto IL_0067;
						IL_0067:
						num2 = 6;
						num10 = ColF_FC - 1;
						goto IL_006e;
						IL_006e:
						num2 = 7;
						num5 = ColF_FC + 1;
						goto IL_0075;
						IL_0075:
						num2 = 8;
						num4 = ColF_FC + 2;
						goto IL_007c;
						IL_007c:
						num2 = 9;
						num7 = ColF_FC + 3;
						goto IL_0084;
						IL_0084:
						num2 = 10;
						num6 = ColF_FC + 4;
						goto IL_008c;
						IL_008c:
						num2 = 11;
						num8 = ColF_FC + 6;
						goto IL_0094;
						IL_0094:
						num2 = 12;
						num11 = 0;
						goto IL_009a;
						IL_009a:
						num2 = 13;
						if (rowIndex == -1)
						{
							goto end_IL_0001_3;
						}
						goto IL_00ae;
						IL_00ae:
						num2 = 15;
						num12 = columnIndex;
						goto IL_00b5;
						IL_00b5:
						num2 = 17;
						if (num12 == num10)
						{
							goto IL_00c7;
						}
						goto IL_01d1;
						IL_00c7:
						num2 = 18;
						if (Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[rowIndex].Cells[columnIndex].Value, "", TextCompare: false))
						{
							goto IL_00fa;
						}
						goto IL_0130;
						IL_00fa:
						num2 = 19;
						ColorDialog1.Color = ColorTranslator.FromHtml(Conversions.ToString(MyGrid.Rows[rowIndex].Cells[columnIndex].Value));
						goto IL_0130;
						IL_0130:
						num2 = 21;
						ColorDialog1.FullOpen = true;
						goto IL_013c;
						IL_013c:
						num2 = 22;
						ColorDialog1.CustomColors = customColors;
						goto IL_0149;
						IL_0149:
						num2 = 23;
						if (ColorDialog1.ShowDialog() != DialogResult.OK)
						{
							goto end_IL_0001_3;
						}
						goto IL_015c;
						IL_015c:
						num2 = 24;
						MyGrid.Rows[rowIndex].Cells[columnIndex].Value = ColorTranslator.ToHtml(ColorDialog1.Color);
						goto IL_018b;
						IL_018b:
						num2 = 25;
						MyGrid[num10, rowIndex].Style.BackColor = ColorDialog1.Color;
						goto IL_01ab;
						IL_01ab:
						num2 = 26;
						MyGrid[ColF_FC, rowIndex].Style.BackColor = ColorDialog1.Color;
						goto end_IL_0001_3;
						IL_01d1:
						num2 = 29;
						if (num12 != ColF_FC && num12 != num5 && num12 != num4)
						{
							goto end_IL_0001_3;
						}
						goto IL_01f1;
						IL_0511:
						num2 = 69;
						MyGrid.Rows[rowIndex].Cells[ColF_FC].Value = Strings.LCase(FontDialog1.Color.Name);
						goto IL_0548;
						IL_0548:
						num2 = 70;
						MyGrid[ColF_FC, rowIndex].Style.ForeColor = FontDialog1.Color;
						goto end_IL_0001_3;
						IL_01f1:
						num2 = 30;
						if (Operators.ConditionalCompareObjectNotEqual(MyGrid.Rows[rowIndex].Cells[ColF_FC].Value, "", TextCompare: false))
						{
							goto IL_0226;
						}
						goto IL_02de;
						IL_0226:
						num2 = 31;
						Information.Err().Clear();
						goto IL_0234;
						IL_0234:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_023d;
						IL_023d:
						num2 = 33;
						FontDialog1.Color = ColorTranslator.FromHtml(Conversions.ToString(MyGrid.Rows[rowIndex].Cells[ColF_FC].Value));
						goto IL_0270;
						IL_0270:
						num2 = 34;
						if (Information.Err().Number == 5)
						{
							goto IL_0286;
						}
						goto IL_02c6;
						IL_0286:
						num2 = 35;
						num11 = unchecked((int)Interaction.MsgBox("Color not supported. Do you wish to reset it to Black or keep the assigned value?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Color"));
						goto IL_029c;
						IL_029c:
						num2 = 36;
						if (num11 != 6)
						{
							goto end_IL_0001_3;
						}
						goto IL_02aa;
						IL_02aa:
						num2 = 37;
						FontDialog1.Color = Color.Black;
						goto IL_02c6;
						IL_02c6:
						num2 = 42;
						Information.Err().Clear();
						goto IL_02d4;
						IL_02d4:
						ProjectData.ClearProjectError();
						num3 = 3;
						goto IL_02de;
						IL_02de:
						num2 = 45;
						FontDialog1.Font = Set_Font_Format(rowIndex, ref MyGrid, num5, num4, num6, num7, num8);
						goto IL_02fb;
						IL_02fb:
						num2 = 46;
						FontDialog1.AllowScriptChange = false;
						goto IL_0307;
						IL_0307:
						num2 = 47;
						FontDialog1.ShowEffects = true;
						goto IL_0313;
						IL_0313:
						num2 = 48;
						FontDialog1.ShowColor = true;
						goto IL_031f;
						IL_031f:
						num2 = 49;
						FontDialog1.FontMustExist = true;
						goto IL_032b;
						IL_032b:
						num2 = 50;
						if (FontDialog1.ShowDialog() != DialogResult.OK)
						{
							goto end_IL_0001_3;
						}
						goto IL_0341;
						IL_0341:
						num2 = 51;
						if (FontDialog1.Font.Bold)
						{
							goto IL_0356;
						}
						goto IL_0382;
						IL_0356:
						num2 = 52;
						MyGrid.Rows[rowIndex].Cells[num6].Value = "bold";
						goto IL_03ab;
						IL_0382:
						num2 = 54;
						MyGrid.Rows[rowIndex].Cells[num6].Value = "normal";
						goto IL_03ab;
						IL_03ab:
						num2 = 56;
						if (FontDialog1.Font.Italic)
						{
							goto IL_03c0;
						}
						goto IL_03ec;
						IL_03c0:
						num2 = 57;
						MyGrid.Rows[rowIndex].Cells[num7].Value = "italic";
						goto IL_0415;
						IL_03ec:
						num2 = 59;
						MyGrid.Rows[rowIndex].Cells[num7].Value = "normal";
						goto IL_0415;
						IL_0415:
						num2 = 61;
						if (FontDialog1.Font.Underline)
						{
							goto IL_042a;
						}
						goto IL_0456;
						IL_042a:
						num2 = 62;
						MyGrid.Rows[rowIndex].Cells[num8].Value = "underline";
						goto IL_047f;
						IL_0456:
						num2 = 64;
						MyGrid.Rows[rowIndex].Cells[num8].Value = "normal";
						goto IL_047f;
						end_IL_0001_2:
						break;
					}
					num2 = 74;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1754;
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

	public static Font Set_Font_Format(int MyRow, ref DataGridView MyGrid, int colf_ff, int colf_fsz, int colf_fw, int colf_fst, int colf_td)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		FontStyle fontStyle = default(FontStyle);
		FontStyle fontStyle2 = default(FontStyle);
		string text = default(string);
		int num5 = default(int);
		string left2 = default(string);
		string left3 = default(string);
		FontStyle fontStyle3 = default(FontStyle);
		Font result = default(Font);
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
				case 634:
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
							goto IL_0022;
						case 6:
							goto IL_002b;
						case 7:
							goto IL_0034;
						case 8:
							goto IL_0039;
						case 9:
							goto IL_003e;
						case 10:
							goto IL_0044;
						case 11:
							goto IL_006b;
						case 12:
							goto IL_0084;
						case 13:
							goto IL_008e;
						case 14:
							goto IL_00b5;
						case 15:
							goto IL_00c6;
						case 16:
							goto IL_00cd;
						case 17:
							goto IL_00f5;
						case 18:
							goto IL_011d;
						case 19:
							goto IL_0145;
						case 20:
							goto IL_015e;
						case 22:
							goto IL_0168;
						case 21:
						case 23:
						case 24:
							goto IL_016f;
						case 25:
							goto IL_0188;
						case 27:
							goto IL_0192;
						case 26:
						case 28:
						case 29:
							goto IL_0199;
						case 30:
							goto IL_01b2;
						case 32:
							goto IL_01bc;
						case 31:
						case 33:
						case 34:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 35:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0199:
					num2 = 29;
					if (Operators.CompareString(left, "underline", TextCompare: false) == 0)
					{
						goto IL_01b2;
					}
					goto IL_01bc;
					IL_01b2:
					num2 = 30;
					fontStyle = FontStyle.Underline;
					break;
					IL_0192:
					num2 = 27;
					fontStyle2 = FontStyle.Regular;
					goto IL_0199;
					IL_01bc:
					num2 = 32;
					fontStyle = FontStyle.Regular;
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					num5 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					left2 = "";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					left3 = "";
					goto IL_002b;
					IL_002b:
					num2 = 6;
					left = "";
					goto IL_0034;
					IL_0034:
					num2 = 7;
					fontStyle3 = FontStyle.Regular;
					goto IL_0039;
					IL_0039:
					num2 = 8;
					fontStyle2 = FontStyle.Regular;
					goto IL_003e;
					IL_003e:
					num2 = 9;
					fontStyle = FontStyle.Regular;
					goto IL_0044;
					IL_0044:
					num2 = 10;
					text = Conversions.ToString(MyGrid.Rows[MyRow].Cells[colf_ff].Value);
					goto IL_006b;
					IL_006b:
					num2 = 11;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0084;
					}
					goto IL_008e;
					IL_0084:
					num2 = 12;
					text = "Arial";
					goto IL_008e;
					IL_008e:
					num2 = 13;
					num5 = Conversions.ToInteger(MyGrid.Rows[MyRow].Cells[colf_fsz].Value);
					goto IL_00b5;
					IL_00b5:
					num2 = 14;
					if (num5 <= 0)
					{
						goto IL_00c6;
					}
					goto IL_00cd;
					IL_00c6:
					num2 = 15;
					num5 = 10;
					goto IL_00cd;
					IL_00cd:
					num2 = 16;
					left2 = Conversions.ToString(MyGrid.Rows[MyRow].Cells[colf_fw].Value);
					goto IL_00f5;
					IL_00f5:
					num2 = 17;
					left3 = Conversions.ToString(MyGrid.Rows[MyRow].Cells[colf_fst].Value);
					goto IL_011d;
					IL_011d:
					num2 = 18;
					left = Conversions.ToString(MyGrid.Rows[MyRow].Cells[colf_td].Value);
					goto IL_0145;
					IL_0145:
					num2 = 19;
					if (Operators.CompareString(left2, "bold", TextCompare: false) == 0)
					{
						goto IL_015e;
					}
					goto IL_0168;
					IL_015e:
					num2 = 20;
					fontStyle3 = FontStyle.Bold;
					goto IL_016f;
					IL_0168:
					num2 = 22;
					fontStyle3 = FontStyle.Regular;
					goto IL_016f;
					IL_016f:
					num2 = 24;
					if (Operators.CompareString(left3, "italic", TextCompare: false) == 0)
					{
						goto IL_0188;
					}
					goto IL_0192;
					IL_0188:
					num2 = 25;
					fontStyle2 = FontStyle.Italic;
					goto IL_0199;
					end_IL_0001_2:
					break;
				}
				num2 = 34;
				result = new Font(text, num5, fontStyle3 | fontStyle2 | fontStyle);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 634;
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

	public static void UpdateTblNodes(ref TreeNode MyNode, string OldName, string NewName)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		TreeNode MyNode2 = default(TreeNode);
		string text = default(string);
		IEnumerator enumerator = default(IEnumerator);
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
				case 351:
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
							goto IL_001d;
						case 5:
							goto IL_003f;
						case 6:
							goto IL_004e;
						case 7:
							goto IL_0093;
						case 8:
							goto IL_00a7;
						case 9:
							goto IL_00bb;
						case 10:
						case 11:
						case 12:
							goto IL_00d5;
						case 13:
							goto IL_00e2;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a7:
					num2 = 8;
					if (Operators.CompareString(left, OldName, TextCompare: false) == 0)
					{
						goto IL_00bb;
					}
					goto IL_00d5;
					IL_00bb:
					num2 = 9;
					MyNode2.Text = General_Procedures.Set_Node_Value(MyNode2.Text, NewName);
					goto IL_00d5;
					IL_0093:
					num2 = 7;
					left = Strings.UCase(General_Procedures.Get_Node_Value(MyNode2.Text));
					goto IL_00a7;
					IL_00d5:
					num2 = 12;
					UpdateTblNodes(ref MyNode2, OldName, NewName);
					goto IL_00e2;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					left = "";
					goto IL_001d;
					IL_001d:
					num2 = 4;
					enumerator = MyNode.Nodes.GetEnumerator();
					goto IL_00e5;
					IL_00e5:
					if (!enumerator.MoveNext())
					{
						break;
					}
					MyNode2 = (TreeNode)enumerator.Current;
					goto IL_003f;
					IL_00e2:
					num2 = 13;
					goto IL_00e5;
					IL_003f:
					num2 = 5;
					text = MyNode2.Tag.ToString();
					goto IL_004e;
					IL_004e:
					num2 = 6;
					if (LikeOperator.LikeString(text, "JMP-SAVE TABLE*", CompareMethod.Binary) | LikeOperator.LikeString(text, "JMP-CLOSE TABLE*", CompareMethod.Binary) | (Operators.CompareString(text, "CHART_TABLE_READ", TextCompare: false) == 0) | (Operators.CompareString(text, "CHART_TABLE_WRITE", TextCompare: false) == 0))
					{
						goto IL_0093;
					}
					goto IL_00d5;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 351;
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

	public static void Update_Chart_Table_Name(string OldName, string NewName, ref TreeNode CurrentNode)
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
					errsource = "BuildChart - Update_Chart_Table_Name";
					TreeNode parent = CurrentNode.Parent;
					TreeNode MyNode = null;
					OldName = Strings.UCase(OldName);
					if (Conversions.ToBoolean(LikeOperator.LikeObject(parent.Tag, "SCRIPT:*:BUTTON", CompareMethod.Binary)))
					{
						MyNode = parent.Parent.Parent;
					}
					else if (Conversions.ToBoolean(LikeOperator.LikeObject(parent.Tag, "SCRIPT:*", CompareMethod.Binary)))
					{
						MyNode = parent;
					}
					UpdateTblNodes(ref MyNode, OldName, NewName);
					goto end_IL_0001;
				}
				case 164:
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
				try0001_dispatch = 164;
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

	public static void Set_StdCtrl(string MyKey, string MyData, bool DoLower = false)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int g_NoStdCtrls = default(int);
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
				case 228:
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
							goto IL_001e;
						case 5:
							goto IL_002b;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0056;
						case 9:
							goto end_IL_0001_2;
						case 12:
						case 13:
							goto IL_008b;
						default:
							goto end_IL_0001;
						case 8:
						case 10:
						case 11:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004d:
					num2 = 6;
					if (!DoLower)
					{
						break;
					}
					goto IL_0056;
					IL_0056:
					num2 = 7;
					Globals_Renamed.g_StdCtrls[num5].Value = Strings.LCase(MyData);
					goto end_IL_0001_3;
					IL_002b:
					num2 = 5;
					if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Token, MyKey, TextCompare: false) == 0)
					{
						goto IL_004d;
					}
					goto IL_008b;
					IL_008b:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_0092;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyKey = Strings.UCase(Strings.Trim(MyKey));
					goto IL_001e;
					IL_001e:
					num2 = 4;
					g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
					num5 = 0;
					goto IL_0092;
					IL_0092:
					if (num5 > g_NoStdCtrls)
					{
						goto end_IL_0001_3;
					}
					goto IL_002b;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Globals_Renamed.g_StdCtrls[num5].Value = MyData;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 228;
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

	public static string Get_StdCtrl(string MyKey)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int g_NoStdCtrls = default(int);
		string result = default(string);
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
				case 192:
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
							goto IL_0018;
						case 5:
							goto IL_0027;
						case 6:
							goto IL_0035;
						case 7:
							goto end_IL_0001_2;
						case 9:
						case 10:
							goto IL_0070;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0079:
					if (num5 > g_NoStdCtrls)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0035:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Token, MyKey, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0070;
					IL_0027:
					num2 = 5;
					g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
					num5 = 0;
					goto IL_0079;
					IL_0070:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_0079;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					result = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					MyKey = Strings.UCase(Strings.Trim(MyKey));
					goto IL_0027;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = Globals_Renamed.g_StdCtrls[num5].Value;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 192;
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

	public static void Set_CK_Box(ref CheckBox MyCK, ref string MyVal)
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
				case 93:
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
							goto IL_0018;
						case 5:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 6:
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (!MyCK.Checked)
					{
						break;
					}
					goto IL_0018;
					IL_0018:
					num2 = 3;
					MyVal = "1";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				MyVal = "0";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 93;
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

	public static void Get_CK_Box(ref CheckBox MyCK, ref string MyVal)
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
							goto IL_0021;
						case 5:
							goto IL_002f;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(MyVal, "N/A", TextCompare: false) == 0)
					{
						goto IL_0021;
					}
					goto IL_002f;
					IL_0021:
					num2 = 3;
					MyCK.Visible = false;
					goto end_IL_0001_3;
					IL_002f:
					num2 = 5;
					if (Operators.CompareString(MyVal, "1", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				MyCK.Checked = true;
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

	public static string Get_Chart_Out_File(ref TreeNode MyChartNode, string TemplateType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string result = default(string);
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
				case 414:
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
							goto IL_001d;
						case 5:
							goto IL_0022;
						case 6:
							goto IL_002b;
						case 7:
							goto IL_0053;
						case 9:
							goto IL_0060;
						case 8:
						case 10:
						case 11:
							goto IL_006b;
						case 12:
							goto IL_0087;
						case 13:
							goto IL_00a0;
						case 14:
							goto IL_00b1;
						case 15:
							goto IL_00c3;
						case 16:
							goto IL_00d1;
						case 18:
							goto IL_00f9;
						case 21:
							goto IL_010f;
						case 17:
						case 19:
						case 20:
						case 22:
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c3:
					num2 = 15;
					if (num5 != 0)
					{
						goto IL_00d1;
					}
					goto IL_00f9;
					IL_00d1:
					num2 = 16;
					text = Strings.Trim(Strings.LCase(Strings.Mid(text + " ", checked(num5 + 1))));
					break;
					IL_00b1:
					num2 = 14;
					num5 = Strings.InStr(text, ":");
					goto IL_00c3;
					IL_00f9:
					num2 = 18;
					text = "unknown" + text2;
					break;
					IL_000b:
					num2 = 2;
					text3 = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					text = "";
					goto IL_001d;
					IL_001d:
					num2 = 4;
					num5 = 0;
					goto IL_0022;
					IL_0022:
					num2 = 5;
					text2 = "";
					goto IL_002b;
					IL_002b:
					num2 = 6;
					if (Operators.CompareString(TemplateType, "R", TextCompare: false) == 0 || Operators.CompareString(TemplateType, "PY", TextCompare: false) == 0)
					{
						goto IL_0053;
					}
					goto IL_0060;
					IL_010f:
					num2 = 21;
					text3 = Strings.LCase(text3) + text2;
					break;
					IL_0060:
					num2 = 9;
					text2 = ".gif";
					goto IL_006b;
					IL_0053:
					num2 = 7;
					text2 = "";
					goto IL_006b;
					IL_006b:
					num2 = 11;
					text3 = Strings.Trim(MyChartNode.Nodes[0].Text);
					goto IL_0087;
					IL_0087:
					num2 = 12;
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto IL_00a0;
					}
					goto IL_010f;
					IL_00a0:
					num2 = 13;
					text = Conversions.ToString(MyChartNode.Tag);
					goto IL_00b1;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				result = text3;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 414;
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

	public static string Get_Chart_Obj_Name(string MyIni)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
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
				case 400:
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
							goto IL_001a;
						case 5:
							goto IL_0031;
						case 7:
							goto IL_0040;
						case 8:
							goto IL_0058;
						case 9:
							goto IL_006c;
						case 10:
							goto IL_0087;
						case 11:
							goto IL_009f;
						case 12:
							goto IL_00b1;
						case 13:
							goto IL_00bf;
						case 15:
							goto IL_00d6;
						case 14:
						case 16:
						case 17:
							goto IL_00dc;
						case 18:
							goto IL_00ee;
						case 19:
							goto IL_00fc;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 21:
						case 22:
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00dc:
					num2 = 17;
					num5 = Strings.InStrRev(text, ".");
					goto IL_00ee;
					IL_00ee:
					num2 = 18;
					if (num5 == 0)
					{
						break;
					}
					goto IL_00fc;
					IL_00d6:
					num2 = 15;
					text = MyIni;
					goto IL_00dc;
					IL_00fc:
					num2 = 19;
					text = Strings.Trim(Strings.Mid(text, 1, checked(num5 - 1)));
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					MyIni = Strings.Trim(MyIni);
					goto IL_001a;
					IL_001a:
					num2 = 4;
					if (Operators.CompareString(MyIni, "", TextCompare: false) == 0)
					{
						goto IL_0031;
					}
					goto IL_0040;
					IL_0031:
					num2 = 5;
					text = "Unknown";
					goto end_IL_0001_3;
					IL_0040:
					num2 = 7;
					if (Strings.InStrRev(MyIni, "\\") == 0)
					{
						goto IL_0058;
					}
					goto IL_006c;
					IL_0058:
					num2 = 8;
					MyIni = Globals_Renamed.gChartDir + "\\" + MyIni;
					goto IL_006c;
					IL_006c:
					num2 = 9;
					text = General_Procedures.Get_Ini_Data("TEMPLATE", "NAME", "N/A", 50, MyIni);
					goto IL_0087;
					IL_0087:
					num2 = 10;
					if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_009f;
					IL_009f:
					num2 = 11;
					num5 = Strings.InStrRev(MyIni, "\\");
					goto IL_00b1;
					IL_00b1:
					num2 = 12;
					if (num5 != 0)
					{
						goto IL_00bf;
					}
					goto IL_00d6;
					IL_00bf:
					num2 = 13;
					text = Strings.Trim(Strings.Mid(MyIni, checked(num5 + 1)));
					goto IL_00dc;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				text = Strings.StrConv(text, VbStrConv.ProperCase);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 400;
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

	public static void Load_Items_To_Combo(ref ComboBox MyCombo, string MyList, string MyDefault)
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
						errsource = "BuildChart - Load_Items_To_Combo";
						int num3 = 0;
						MyCombo.Items.Clear();
						if (Operators.CompareString(MyList, "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(MyList, ";");
							int num4 = Information.UBound(array);
							for (num3 = 0; num3 <= num4; num3++)
							{
								MyCombo.Items.Add(array[num3]);
							}
							array = null;
						}
						MyCombo.Items.Add("");
						MyCombo.Sorted = true;
						if (Operators.CompareString(MyDefault, "", TextCompare: false) == 0)
						{
							goto end_IL_0001;
						}
						int num5 = MyCombo.Items.Count - 1;
						for (num3 = 0; num3 <= num5; num3++)
						{
							if (Operators.ConditionalCompareObjectEqual(MyDefault, MyCombo.Items[num3], TextCompare: false))
							{
								MyCombo.SelectedIndex = num3;
								break;
							}
						}
						goto end_IL_0001;
					}
					case 272:
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
				try0001_dispatch = 272;
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

	public static void Get_Prior_Window_Objects(string MyMode, ref TreeNode CurrentNode, ref string MyWindow)
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
						errsource = "BuildChart - Get_Prior_Window_Objects";
						if (CurrentNode == null)
						{
							goto end_IL_0001;
						}
						TreeNode parent = CurrentNode.Parent;
						TreeNode treeNode = null;
						TreeNode treeNode2 = null;
						TreeNode treeNode3 = null;
						string text = "";
						string text2 = "";
						string text3 = "";
						int num3 = 0;
						MyWindow = "";
						string left = Strings.UCase(MyMode);
						text2 = ((Operators.CompareString(left, "L", TextCompare: false) != 0) ? "-CREATE WINDOW*" : "-ADD LAYOUT*");
						if (Conversions.ToBoolean(LikeOperator.LikeObject(parent.Tag, "SCRIPT:*:BUTTON", CompareMethod.Binary)))
						{
							treeNode = parent.Parent.Parent;
						}
						else if (Conversions.ToBoolean(LikeOperator.LikeObject(parent.Tag, "SCRIPT:*", CompareMethod.Binary)))
						{
							treeNode = parent;
						}
						text = General_Procedures.Get_Chart_Script_Type(Conversions.ToString(treeNode.Tag), 1);
						IEnumerator enumerator = treeNode.Nodes.GetEnumerator();
						while (enumerator.MoveNext())
						{
							treeNode2 = (TreeNode)enumerator.Current;
							if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, text + text2, CompareMethod.Binary)))
							{
								text3 = General_Procedures.Get_Node_Value(treeNode2.Text);
								if (Operators.CompareString(MyMode, "L", TextCompare: false) == 0)
								{
									num3 = Strings.InStr(text3, ":");
									if (num3 != 0)
									{
										text3 = Strings.Trim(Strings.Mid(text3, 1, num3 - 1));
									}
								}
								MyWindow = MyWindow + ";" + text3;
							}
							else
							{
								if (!Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, text + "-BUTTON:*", CompareMethod.Binary)))
								{
									continue;
								}
								IEnumerator enumerator2 = treeNode2.Nodes[1].Nodes.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									treeNode3 = (TreeNode)enumerator2.Current;
									if (!Conversions.ToBoolean(LikeOperator.LikeObject(treeNode3.Tag, text + text2, CompareMethod.Binary)))
									{
										continue;
									}
									text3 = General_Procedures.Get_Node_Value(treeNode3.Text);
									if (Operators.CompareString(MyMode, "L", TextCompare: false) == 0)
									{
										num3 = Strings.InStr(text3, ":");
										if (num3 != 0)
										{
											text3 = Strings.Trim(Strings.Mid(text3, 1, num3 - 1));
										}
									}
									MyWindow = MyWindow + ";" + text3;
								}
								if (enumerator2 is IDisposable)
								{
									(enumerator2 as IDisposable).Dispose();
								}
							}
						}
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
						if (Operators.CompareString(MyWindow, "", TextCompare: false) != 0)
						{
							MyWindow = Strings.Mid(MyWindow, 2);
						}
						goto end_IL_0001;
					}
					case 770:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							MyWindow = "";
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 770;
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

	public static string StripQ2(string MyVal)
	{
		return Strings.Replace(Strings.Replace(MyVal, "'", "", 1, -1, CompareMethod.Text), "\"", "", 1, -1, CompareMethod.Text);
	}

	public static string Get_Chart_Where(string MyTemplateType, string Where_Mode, string MyWValue, string LDlm1, string lDlm2, string Where_Prefix)
	{
		int try0001_dispatch = -1;
		string text;
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
						text = "";
						if (Operators.CompareString(Where_Mode, "N/A", TextCompare: false) == 0)
						{
							goto end_IL_0001;
						}
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "BuildChart - Get_Where_Chart";
						int num3 = 0;
						int num4 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string MyDT = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						int num5 = -1;
						bool flag = false;
						string text10 = "";
						string text11 = "";
						string text12 = "";
						switch (MyTemplateType)
						{
						case "JSL":
						case "JMP":
							switch (Where_Mode)
							{
							case "0":
							case "4":
								text10 = ":Name(\"";
								text11 = "\")";
								break;
							case "1":
								text10 = "Column(::ForTable,\"";
								text11 = "\")[::Foridx];";
								break;
							case "2":
								text10 = "Column(::RenaTable,\"";
								text11 = "\")";
								break;
							}
							break;
						case "R":
							text10 = Where_Prefix;
							text11 = "";
							break;
						case "PY":
							Where_Prefix = Strings.Trim(Where_Prefix);
							num4 = Strings.InStr(Where_Prefix, "<>");
							if (num4 > 1 && Operators.CompareString(Where_Prefix, "<>", TextCompare: false) != 0)
							{
								text10 = Strings.Trim(Strings.Mid(Where_Prefix, 1, num4 - 1));
								text11 = Strings.Trim(Strings.Mid(Where_Prefix + " ", num4 + 2));
							}
							break;
						default:
							text10 = "";
							text11 = "";
							break;
						}
						if (Operators.CompareString(Strings.Trim(MyWValue), "", TextCompare: false) != 0)
						{
							string[] array = Strings.Split(MyWValue, "~~~");
							int num6 = Information.UBound(array);
							for (num3 = 0; num3 <= num6; num3++)
							{
								string[] array2 = Strings.Split(array[num3], ";");
								text7 = array2[0];
								text5 = array2[1];
								text2 = array2[2];
								text3 = array2[3];
								text4 = array2[4];
								text6 = array2[5];
								if (Operators.CompareString(MyTemplateType, "JS", TextCompare: false) == 0)
								{
									if (Operators.CompareString(text3, "", TextCompare: false) == 0)
									{
										text3 = "=";
									}
									if (num3 == 0)
									{
										text7 = "";
									}
									else if (Operators.CompareString(text7, "", TextCompare: false) == 0)
									{
										text7 = "And";
									}
								}
								else
								{
									if (Operators.CompareString(text3, "", TextCompare: false) == 0 && (Operators.CompareString(Where_Mode, "0", TextCompare: false) == 0 || Operators.CompareString(Where_Mode, "3", TextCompare: false) == 0))
									{
										text3 = "==";
									}
									if (num3 == 0)
									{
										text7 = "";
									}
									else if ((Operators.CompareString(text7, "", TextCompare: false) == 0) & ((Operators.CompareString(Where_Mode, "0", TextCompare: false) == 0) | (Operators.CompareString(Where_Mode, "3", TextCompare: false) == 0)))
									{
										text7 = "&";
									}
									else if (Operators.CompareString(Where_Mode, "4", TextCompare: false) == 0)
									{
										text7 = ",";
									}
									if (LikeOperator.LikeString(Strings.UCase(text3), "*MISSING", CompareMethod.Binary))
									{
										text4 = "";
									}
								}
								if (Operators.CompareString(Where_Mode, "3", TextCompare: false) == 0)
								{
									switch (Strings.UCase(text2))
									{
									case "MODELING-TYPE":
										text2 = "UpperCase(::_Allmt<<<pseudo-col>>>_)";
										if (Operators.CompareString(text4, "", TextCompare: false) != 0)
										{
											text4 = "UpperCase(\"" + text4 + "\")";
										}
										break;
									case "COLUMN-HEADER":
										text2 = "UpperCase(::_AllCol<<<pseudo-col>>>_)";
										if (Operators.CompareString(text4, "", TextCompare: false) != 0)
										{
											text4 = "UpperCase(\"" + text4 + "\")";
										}
										break;
									case "COLUMN-INDEX":
										text2 = "::_AllIdx<<<pseudo-col>>>_";
										break;
									default:
										text2 = "";
										text4 = "";
										text3 = "";
										break;
									}
									if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0))
									{
										if (Operators.CompareString(Strings.UCase(Strings.Mid(text3, 1, 4)), "NOT ", TextCompare: false) == 0)
										{
											text3 = Strings.Mid(text3, 5);
											text12 = " == 0";
										}
										else
										{
											text12 = " != 0";
										}
										switch (Strings.UCase(text3))
										{
										case "STARTSWITH":
										case "ENDSWITH":
											text = text + "\r\n       " + text7 + " " + text5 + " " + text3 + "(" + text2 + "," + text4 + ")" + text12 + text6;
											break;
										case "CONTAINS":
											text = text + "\r\n       " + text7 + " " + text5 + " " + text3 + "(" + text2 + "," + text4 + ")" + text12 + text6;
											break;
										default:
											text = text + "\r\n       " + text7 + " " + text5 + " " + text2 + " " + text3 + " " + text4 + " " + text6;
											break;
										}
									}
								}
								else
								{
									BuildForm.Strip_Col_DT(ref text2, ref MyDT);
									if ((Operators.CompareString(Strings.UCase(MyDT), "N", TextCompare: false) == 0) | LikeOperator.LikeString(Strings.UCase(text3), "*MISSING", CompareMethod.Binary) | (Strings.InStr(text4, ":") != 0))
									{
										text8 = "";
										text9 = "";
									}
									else if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 || Operators.CompareString(MyTemplateType, "JS", TextCompare: false) == 0 || Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0)
									{
										text8 = "";
										text9 = "";
									}
									else
									{
										text8 = "\"";
										text9 = "\"";
									}
									if ((Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(text4, "", TextCompare: false) != 0) || ((Operators.CompareString(text2, "", TextCompare: false) != 0) & LikeOperator.LikeString(Strings.UCase(text3), "*MISSING", CompareMethod.Binary)))
									{
										switch (Where_Mode)
										{
										case "0":
											text2 = text10 + text2 + text11;
											if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(text3), "*MISSING", CompareMethod.Binary))
											{
												text2 = ((Operators.CompareString(Strings.UCase(text3), "IS NOT MISSING", TextCompare: false) != 0) ? ("Is Missing(" + text2 + ")") : ("!Is Missing(" + text2 + ")"));
												text3 = "";
											}
											text = ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "STARTSWITH", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " grepl(pattern='^" + StripQ2(text4) + "',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "CONTAINS", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " grepl(pattern='" + StripQ2(text4) + "',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "ENDSWITH", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " grepl(pattern='" + StripQ2(text4) + "$',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "NOT STARTSWITH", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " !grepl(pattern='^" + StripQ2(text4) + "',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "NOT CONTAINS", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " !grepl(pattern='" + StripQ2(text4) + "',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "NOT ENDSWITH", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " !grepl(pattern='" + StripQ2(text4) + "$',x=" + text2 + ",fixed=FALSE,ignore.case=TRUE) " + text6) : ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "IS NOT MISSING", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " (" + text2 + ".notnull()) " + text6) : ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "CONTAINS", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " (" + text2 + ".str.contains(" + text8 + text4 + text9 + ")) " + text6) : ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "IN", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " (" + text2 + ".isin([" + text8 + text4 + text9 + "])) " + text6) : ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text3), "NOT IN", TextCompare: false) == 0) ? (text + LDlm1 + text7 + " " + text5 + " (~" + text2 + ".isin([" + text8 + text4 + text9 + "])) " + text6) : ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) != 0) ? (text + LDlm1 + text7 + " " + text5 + " " + text2 + " " + text3 + " " + text8 + text4 + text9 + " " + text6) : (text + LDlm1 + text7 + " " + text5 + " (" + text2 + " " + text3 + " " + text8 + text4 + text9 + ") " + text6))))))))))));
											break;
										case "1":
											if ((Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0) | ((Operators.CompareString(MyTemplateType, "JMP", TextCompare: false) == 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0)))
											{
												text4 = "::" + text4;
											}
											text = text + LDlm1 + text4 + " = " + text10 + text2 + text11;
											break;
										case "2":
											text = text + LDlm1 + text10 + text2 + text11 + "<<Set Name(\"" + text4 + "\");";
											break;
										case "3":
											text = text + "       " + text7 + " " + text5 + " " + text2 + " " + text3 + " " + text8 + text4 + text9 + " " + text6;
											break;
										case "4":
											BuildForm.Strip_Col_DT(ref text4, ref MyDT);
											text = text + "\r\n                " + text7 + text10 + text2 + text11 + " = " + text10 + text4 + text11;
											break;
										}
										LDlm1 = lDlm2;
									}
								}
								array2 = null;
							}
							array = null;
						}
						if (Operators.CompareString(Where_Mode, "4", TextCompare: false) == 0)
						{
							text = ((Operators.CompareString(Strings.Trim(text), "", TextCompare: false) != 0) ? ("By Matching Columns(" + text + "),") : "Cartesian Join,");
						}
						goto end_IL_0001_2;
					}
					case 4244:
						num = -1;
						switch (num2)
						{
						case 2:
							text = "";
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_10ca;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4244;
				continue;
			}
			break;
			IL_10ca:
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public static void Invoke_Chart(ref TreeView TreeCol, ref short fSaveQuery, string MyTag)
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
						errsource = "BuildChart - Invoke_Chart";
						TreeNode treeNode = null;
						string MyChart = "";
						string MyChartActual = "";
						string text = "";
						string text2 = "";
						string MyWindow = "";
						string text3 = "";
						string text4 = "";
						int num3 = 0;
						int num4 = 0;
						if ((Operators.CompareString(MyTag, "CHART_VARIABLE", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "CHART_TITLE", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "CHART_JMP_FONT", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "CHART_JOURNAL", TextCompare: false) == 0))
						{
							TreeCol.LabelEdit = true;
							TreeCol.SelectedNode.BeginEdit();
							fSaveQuery = 1;
						}
						else if (LikeOperator.LikeString(MyTag, "JMP-BUTTON*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "R-BUTTON*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "HTML-BUTTON*", CompareMethod.Binary))
						{
							MyChart = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
							MyChart = Interaction.InputBox("Specify the Button Caption", "Enter Button Caption", MyChart);
							if (Operators.CompareString(MyChart, "", TextCompare: false) != 0)
							{
								TreeCol.SelectedNode.Text = "Button {" + MyChart + "}";
								fSaveQuery = 1;
							}
						}
						else
						{
							if ((Operators.CompareString(Strings.UCase(Strings.Mid(MyTag + "            ", 1, 12)), "U->CHART-IN-", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Mid(MyTag + "           ", 1, 11)), "U->HTML-REP", TextCompare: false) == 0))
							{
								goto end_IL_0001;
							}
							if (LikeOperator.LikeString(MyTag, "*-SAVE TABLE*", CompareMethod.Binary) | (Operators.CompareString(MyTag, "JMP-CLOSE TABLE", TextCompare: false) == 0) | LikeOperator.LikeString(MyTag, "CHART_TABLE_WRITE", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "CHART_FILE*", CompareMethod.Binary) | (Operators.CompareString(MyTag, "CHART_PPT2", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "CHART_PPT", TextCompare: false) == 0))
							{
								switch (MyTag)
								{
								case "CHART_FILE2":
									Globals_Renamed.currinputstrtmp = "JMP";
									break;
								case "CHART_FILE":
									Globals_Renamed.currinputstrtmp = "JMP2";
									break;
								case "CHART_FILER":
									Globals_Renamed.currinputstrtmp = "JR";
									break;
								case "CHART_PPT2":
									Globals_Renamed.currinputstrtmp = "PPT2";
									break;
								case "CHART_PPT":
									Globals_Renamed.currinputstrtmp = "PPT";
									break;
								default:
									if (Conversions.ToBoolean(LikeOperator.LikeString(MyTag, "R-*", CompareMethod.Binary) || (Operators.CompareString(MyTag, "CHART_TABLE_WRITE", TextCompare: false) == 0 && Conversions.ToBoolean(LikeOperator.LikeObject(TreeCol.SelectedNode.Parent.Tag, "R-*", CompareMethod.Binary)))))
									{
										Globals_Renamed.currinputstrtmp = "SR";
									}
									else
									{
										Globals_Renamed.currinputstrtmp = "S";
									}
									break;
								}
								Globals_Renamed.currvaluetmp = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "No", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "";
								}
								frmfilename frmfilename2 = new frmfilename();
								frmfilename2.ShowDialog();
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "No";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
								{
									TreeCol.SelectedNode.Text = General_Procedures.Set_Node_Value(TreeCol.SelectedNode.Text, Globals_Renamed.currvaluetmp);
									fSaveQuery = 1;
								}
							}
							else if ((Operators.CompareString(MyTag, "JMP-SAVE WINDOW", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "JMP-CLOSE WINDOW", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "JMP-JOURNAL WINDOW", TextCompare: false) == 0))
							{
								TreeView treeView;
								TreeNode CurrentNode = (treeView = TreeCol).SelectedNode;
								Get_Prior_Window_Objects("W", ref CurrentNode, ref MyChart);
								treeView.SelectedNode = CurrentNode;
								Globals_Renamed.currinputstrtmp = "W" + MyChart;
								Globals_Renamed.currvaluetmp = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "No", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "";
								}
								frmfilename frmfilename3 = new frmfilename();
								frmfilename3.ShowDialog();
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "No";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
								{
									TreeCol.SelectedNode.Text = General_Procedures.Set_Node_Value(TreeCol.SelectedNode.Text, Globals_Renamed.currvaluetmp);
									fSaveQuery = 1;
								}
							}
							else if (Operators.CompareString(MyTag, "JMP-ADD SCRIPT", TextCompare: false) == 0)
							{
								Globals_Renamed.currvaluetmp = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "No", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "";
								}
								Globals_Renamed.currinputstrtmp = "U";
								frmfilename frmfilename4 = new frmfilename();
								frmfilename4.ShowDialog();
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "No";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
								{
									TreeCol.SelectedNode.Text = General_Procedures.Set_Node_Value(TreeCol.SelectedNode.Text, Globals_Renamed.currvaluetmp);
									fSaveQuery = 1;
								}
							}
							else if (LikeOperator.LikeString(MyTag, "JMP-ADD LAYOUT*", CompareMethod.Binary))
							{
								Globals_Renamed.currinputstrtmp = "Y";
								MyChart = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								num3 = Strings.InStr(MyChart, ":");
								if (num3 != 0)
								{
									Globals_Renamed.currvaluetmp = Strings.Trim(Strings.Mid(MyChart, num3 + 1));
									MyChart = Strings.Mid(MyChart, 1, num3);
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0 || Strings.Len(Globals_Renamed.currvaluetmp) > 1 || ((Operators.CompareString(Globals_Renamed.currvaluetmp, "1", TextCompare: false) < 0) & (Operators.CompareString(Globals_Renamed.currvaluetmp, "9", TextCompare: false) > 0)))
								{
									Globals_Renamed.currvaluetmp = "1";
								}
								frmfilename frmfilename5 = new frmfilename();
								frmfilename5.ShowDialog();
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "1";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
								{
									TreeCol.SelectedNode.Text = General_Procedures.Set_Node_Value(TreeCol.SelectedNode.Text, MyChart + Globals_Renamed.currvaluetmp);
									fSaveQuery = 1;
								}
							}
							else if (LikeOperator.LikeString(MyTag, "JMP-APPEND LAYOUT*", CompareMethod.Binary))
							{
								Globals_Renamed.currinputstrtmp = "O";
								Globals_Renamed.currvaluetmp = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "No", TextCompare: false) == 0)
								{
									Globals_Renamed.currvaluetmp = "";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0)
								{
									text3 = Strings.Trim(TreeCol.SelectedNode.Name);
									num3 = Strings.InStr(text3, ":");
									text3 = ((num3 == 0) ? Strings.UCase(text3) : Strings.UCase(Strings.Trim(Strings.Mid(text3, 1, num3 - 1))));
								}
								else
								{
									text3 = "";
								}
								TreeView treeView;
								TreeNode CurrentNode = (treeView = TreeCol).SelectedNode;
								Get_Prior_Window_Objects("L", ref CurrentNode, ref MyWindow);
								treeView.SelectedNode = CurrentNode;
								TreeView obj = TreeCol;
								CurrentNode = obj.SelectedNode;
								Get_Prior_Chart_Table_Filter_Objects(ref CurrentNode, ref MyChart, ref MyChartActual);
								obj.SelectedNode = CurrentNode;
								string[] array = Strings.Split(MyChart, ",");
								string[] array2 = Strings.Split(MyChartActual, ",");
								MyChart = "";
								MyChartActual = "";
								text = "";
								text2 = "";
								int num5 = Information.UBound(array);
								for (num4 = 0; num4 <= num5; num4++)
								{
									MyChart = MyChart + ";" + array[num4] + "                                                                                                    : " + array2[num4];
									if (Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(text3, Strings.UCase(array2[num4]), TextCompare: false) == 0)
									{
										text3 = array[num4] + "                                                                                                    : " + array2[num4];
										MyChartActual = "OK";
									}
								}
								array = null;
								array2 = null;
								if (Operators.CompareString(MyChart, "", TextCompare: false) != 0)
								{
									MyChart = Strings.Mid(MyChart, 2);
								}
								if (Operators.CompareString(MyChartActual, "OK", TextCompare: false) != 0)
								{
									text3 = "";
								}
								Globals_Renamed.currvaluetmp = Globals_Renamed.currvaluetmp + "</@#;>" + text3 + "</@#;>" + MyWindow + "</@#;>" + MyChart;
								frmfilename frmfilename6 = new frmfilename();
								frmfilename6.ShowDialog();
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
								{
									MyChart = "No";
									MyChartActual = "";
								}
								if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
								{
									num3 = Strings.InStr(Globals_Renamed.currvaluetmp, "</@#;>");
									if (num3 != 0)
									{
										MyChart = Strings.Mid(Globals_Renamed.currvaluetmp, 1, num3 - 1);
										MyChartActual = Strings.Trim(Strings.Mid(Globals_Renamed.currvaluetmp + " ", num3 + Strings.Len("</@#;>")));
									}
									else
									{
										MyChart = "No";
										MyChartActual = "";
									}
									TreeCol.SelectedNode.Text = General_Procedures.Set_Node_Value(TreeCol.SelectedNode.Text, MyChart);
									TreeCol.SelectedNode.Name = MyChartActual;
									fSaveQuery = 1;
								}
							}
							else if (LikeOperator.LikeString(MyTag, "JMP-ADD JSL*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "R-ADD R-SCRIPT*", CompareMethod.Binary))
							{
								FrmDTREdit frmDTREdit = new FrmDTREdit();
								MyChart = Strings.Trim(TreeCol.SelectedNode.Name);
								if (Operators.CompareString(MyChart, "", TextCompare: false) == 0)
								{
									if (LikeOperator.LikeString(MyTag, "JMP*", CompareMethod.Binary))
									{
										MyChart = "/*--------------------------------------------------------------------------------------------------------------------------------------------------";
										MyChart += "\r\n| Paste your JSL code below and title the window if you wish. Chart-Object will be replaced with the";
										MyChart += "\r\n| JSL Object Name. Remove the SQLPathFinder generated script below and do not name this JSL object ";
										MyChart += "\r\n| if you do not need to add your JSL script to a SQLPathFinder Create Window object.";
										MyChart += "\r\n --------------------------------------------------------------------------------------------------------------------------------------------------*/";
										MyChart += "\r\n<<<chart-object>>> = Outline Box (\"Window Title\",";
										MyChart += "\r\n/*Paste Code Below*/\r\n\r\n\r\n";
										MyChart += "\r\n/*Paste Code Above*/";
										MyChart += "\r\n);";
									}
									else
									{
										MyChart = BuildSQL.Write_VA_Lib("R_lib_4.lib");
									}
								}
								frmDTREdit.Text1.Text = MyChart;
								frmDTREdit.Text1.SelectionStart = 0;
								frmDTREdit.Text1.SelectionLength = 0;
								if (LikeOperator.LikeString(MyTag, "JMP*", CompareMethod.Binary))
								{
									frmDTREdit.cmbSQLVA.Text = "JSL";
									MyChartActual = "MyUnknownJSL";
								}
								else
								{
									frmDTREdit.cmbSQLVA.Text = "R-SCRIPT";
									MyChartActual = "MyUnknownR";
								}
								frmDTREdit.KeepSQL = true;
								frmDTREdit.fSaveSQL = false;
								frmDTREdit.out_odbc = "--";
								frmDTREdit.ShowDialog();
								text = Strings.Trim(frmDTREdit.KeepSQLData);
								if ((Operators.CompareString(text, "EMPTY", TextCompare: false) != 0) & (Operators.CompareString(text, "", TextCompare: false) != 0))
								{
									TreeCol.SelectedNode.Name = text;
									if (Operators.CompareString(Strings.Trim(TreeCol.SelectedNode.Nodes[0].Text), "", TextCompare: false) == 0)
									{
										MyChart = Conversions.ToString(TreeCol.SelectedNode.Tag);
										num3 = Strings.InStr(MyChart, ":");
										if (num3 != 0)
										{
											MyChart = Strings.Trim(Strings.Mid(MyChart + " ", num3 + 1));
											MyChart = ((Operators.CompareString(MyChart, "", TextCompare: false) != 0) ? Strings.StrConv(MyChart, VbStrConv.ProperCase) : MyChartActual);
										}
										else
										{
											MyChart = MyChartActual;
										}
										TreeCol.SelectedNode.Nodes[0].Text = MyChart;
									}
								}
								frmDTREdit.Dispose();
								fSaveQuery = 1;
							}
							else if (LikeOperator.LikeString(MyTag, "*-ADD COLUMN*", CompareMethod.Binary))
							{
								treeNode = TreeCol.SelectedNode;
								MyChart = Strings.Trim(General_Procedures.Get_Node_Value(treeNode.Nodes[0].Text));
								if (Operators.CompareString(Strings.UCase(MyChart), "NO", TextCompare: false) == 0)
								{
									Interaction.MsgBox("Do assign a table before creating computed column", MsgBoxStyle.Exclamation, "Missing Table");
									goto end_IL_0001;
								}
								if (LikeOperator.LikeString(MyTag, "R-ADD COLUMN*", CompareMethod.Binary))
								{
									Globals_Renamed.currinttmp = 7;
								}
								else
								{
									Globals_Renamed.currinttmp = 4;
								}
								Globals_Renamed.currtxttmp = "";
								Globals_Renamed.currdatatypetmp = "";
								Globals_Renamed.currDataAny = "";
								Globals_Renamed.currlisttmp = MyChart;
								Globals_Renamed.currtxttmp = Strings.Trim(General_Procedures.Get_Node_Value(treeNode.Text));
								if (Operators.CompareString(Strings.UCase(Globals_Renamed.currtxttmp), "NO", TextCompare: false) == 0)
								{
									Globals_Renamed.currtxttmp = "";
								}
								Globals_Renamed.currdatatypetmp = treeNode.Name;
								if (Operators.CompareString(Strings.UCase(Globals_Renamed.currdatatypetmp), "", TextCompare: false) != 0)
								{
									num3 = Strings.InStr(Globals_Renamed.currdatatypetmp, ":");
									if (num3 != 0)
									{
										Globals_Renamed.currDataAny = Strings.Mid(Globals_Renamed.currdatatypetmp, num3 + 1);
										Globals_Renamed.currdatatypetmp = Strings.Mid(Globals_Renamed.currdatatypetmp, 1, num3 - 1);
									}
								}
								FrmComputedSQL frmComputedSQL = new FrmComputedSQL();
								frmComputedSQL.ShowDialog();
								frmComputedSQL.Dispose();
								if (Operators.CompareString(Strings.Trim(Globals_Renamed.currtxttmp), "", TextCompare: false) != 0)
								{
									treeNode.Text = "Add Column {" + Globals_Renamed.currtxttmp + "}";
									treeNode.Name = Globals_Renamed.currdatatypetmp + ":" + Globals_Renamed.currDataAny;
									fSaveQuery = 1;
								}
								Globals_Renamed.currDataAny = "";
								Globals_Renamed.currdatatypetmp = "";
								Globals_Renamed.currinttmp = 0;
								Globals_Renamed.currtxttmp = "";
							}
							else if (LikeOperator.LikeString(MyTag, "JMP-CREATE WINDOW*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "HTML-CREATE WINDOW*", CompareMethod.Binary))
							{
								treeNode = TreeCol.SelectedNode;
								MyChart = "";
								MyChartActual = "";
								text = "";
								text2 = "";
								MyWindow = "";
								Get_Prior_Chart_Table_Filter_Objects(ref treeNode, ref MyChart, ref text2);
								text = TreeCol.SelectedNode.Name;
								Cursor.Current = Cursors.WaitCursor;
								FrmJMPWindow frmJMPWindow = new FrmJMPWindow();
								if (LikeOperator.LikeString(MyTag, "JMP-*", CompareMethod.Binary))
								{
									frmJMPWindow.f_ScriptType = "JMP";
								}
								else
								{
									frmJMPWindow.f_ScriptType = "HTML";
								}
								frmJMPWindow.f_Charts = MyChart;
								frmJMPWindow.f_ChartsActual = text2;
								frmJMPWindow.f_JSLScript = text;
								frmJMPWindow.ShowDialog();
								text = frmJMPWindow.f_JSLScript;
								frmJMPWindow.Dispose();
								treeNode.Name = text;
								fSaveQuery = 1;
							}
							else if (LikeOperator.LikeString(MyTag, "HTML-CREATE MENU WINDOW*", CompareMethod.Binary) || LikeOperator.LikeString(MyTag, "HTML-CREATE TAB WINDOW*", CompareMethod.Binary))
							{
								treeNode = TreeCol.SelectedNode;
								text = TreeCol.SelectedNode.Name;
								Cursor.Current = Cursors.WaitCursor;
								FrmTabMenu frmTabMenu = new FrmTabMenu();
								if (LikeOperator.LikeString(MyTag, "HTML-CREATE MENU WINDOW*", CompareMethod.Binary))
								{
									frmTabMenu.f_Mode = "MENU";
								}
								else
								{
									frmTabMenu.f_Mode = "TAB";
								}
								frmTabMenu.f_Data = text;
								frmTabMenu.ShowDialog();
								text = frmTabMenu.f_Data;
								if (Operators.CompareString(text, "CANCEL", TextCompare: false) != 0)
								{
									treeNode.Name = text;
									fSaveQuery = 1;
								}
								MyProject.Forms.FrmJMPWindow.Dispose();
							}
							else if (LikeOperator.LikeString(MyTag, "HTML-REPORT*", CompareMethod.Binary))
							{
								treeNode = TreeCol.SelectedNode;
								text = TreeCol.SelectedNode.Name;
								Cursor.Current = Cursors.WaitCursor;
								if (LikeOperator.LikeString(MyTag, "HTML-REPORTI*", CompareMethod.Binary))
								{
									TreeView obj2 = TreeCol;
									TreeNode CurrentNode = obj2.SelectedNode;
									Get_Prior_Chart_Table_Filter_Objects(ref CurrentNode, ref MyChart, ref MyChartActual);
									obj2.SelectedNode = CurrentNode;
									FrmReporti frmReporti = new FrmReporti();
									frmReporti.f_ReportSpec = text;
									text2 = treeNode.Nodes[0].Text;
									if (Operators.CompareString(text2, "", TextCompare: false) == 0)
									{
										text2 = "untitled";
									}
									frmReporti.f_ReportSpecName = text2;
									frmReporti.fDisplay = MyChart;
									frmReporti.fActual = MyChartActual;
									frmReporti.ShowDialog();
									text = frmReporti.f_ReportSpec;
									frmReporti.Dispose();
								}
								else
								{
									FrmReport frmReport = new FrmReport();
									frmReport.f_ReportSpec = text;
									text2 = treeNode.Nodes[0].Text;
									if (Operators.CompareString(text2, "", TextCompare: false) == 0)
									{
										text2 = "untitled";
									}
									frmReport.f_ReportSpecName = text2;
									frmReport.ShowDialog();
									text = frmReport.f_ReportSpec;
									frmReport.Dispose();
								}
								treeNode.Name = text;
								fSaveQuery = 1;
							}
							else if (LikeOperator.LikeString(MyTag, "HTML-REPCSS*", CompareMethod.Binary))
							{
								treeNode = TreeCol.SelectedNode;
								text = TreeCol.SelectedNode.Name;
								Cursor.Current = Cursors.WaitCursor;
								FrmCSS frmCSS = new FrmCSS();
								frmCSS.f_ReportSpec = text;
								text2 = treeNode.Nodes[0].Text;
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = "untitled";
								}
								frmCSS.f_ReportSpecName = text2;
								frmCSS.ShowDialog();
								text = frmCSS.f_ReportSpec;
								frmCSS.Dispose();
								treeNode.Name = text;
								fSaveQuery = 1;
							}
							else if (LikeOperator.LikeString(MyTag, "JMP-CHART*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "R-CHART*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "JMP-NEWTABLE*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "*-NOTABLE*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "HTML-CHART*", CompareMethod.Binary))
							{
								TreeCol.SelectedNode.Collapse();
								TreeView obj3 = TreeCol;
								TreeNode CurrentNode = obj3.SelectedNode;
								Load_Chart(ref CurrentNode);
								obj3.SelectedNode = CurrentNode;
								fSaveQuery = 1;
							}
							else if ((Operators.CompareString(MyTag, "JMP-LOAD DATA", TextCompare: false) == 0) | (Operators.CompareString(MyTag, "R-LOAD DATA", TextCompare: false) == 0))
							{
								text = General_Procedures.Get_Node_Value(TreeCol.SelectedNode.Text);
								if (Operators.CompareString(text, "No", TextCompare: false) == 0)
								{
									text = "";
									MyChartActual = "";
								}
								else
								{
									MyChartActual = TreeCol.SelectedNode.Nodes[0].Name;
								}
								MyWindow = TreeCol.SelectedNode.Name;
								text3 = TreeCol.SelectedNode.Nodes[0].Text;
								FrmJMPLoad frmJMPLoad = new FrmJMPLoad();
								frmJMPLoad.f_InFile = text;
								frmJMPLoad.f_CSVColumns = MyChartActual;
								if (Operators.CompareString(MyTag, "R-LOAD DATA", TextCompare: false) == 0 && GetColCase(TreeCol.SelectedNode, 2))
								{
									frmJMPLoad.f_ColCaseInsensitive = true;
								}
								else if (Operators.CompareString(MyTag, "JMP-LOAD DATA", TextCompare: false) == 0 && GetColCase(TreeCol.SelectedNode, 1))
								{
									frmJMPLoad.f_ColCaseInsensitive = true;
								}
								text4 = "N";
								if (Strings.Len(MyWindow) >= 2)
								{
									text4 = Strings.Mid(MyWindow, 2, 1);
									MyWindow = Strings.Mid(MyWindow, 1, 1);
								}
								frmJMPLoad.f_DelTable = MyWindow;
								frmJMPLoad.f_DataType = text4;
								frmJMPLoad.f_TblName = text3;
								if (LikeOperator.LikeString(MyTag, "JMP-*", CompareMethod.Binary))
								{
									frmJMPLoad.f_ScriptType = "JMP";
								}
								else
								{
									frmJMPLoad.f_ScriptType = "R";
								}
								frmJMPLoad.ShowDialog();
								text = frmJMPLoad.f_InFile;
								MyChartActual = frmJMPLoad.f_CSVColumns;
								MyWindow = frmJMPLoad.f_DelTable;
								text2 = frmJMPLoad.f_TblName;
								text4 = frmJMPLoad.f_DataType;
								frmJMPLoad.Dispose();
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = Derive_Chart_Table(ref text);
								}
								if (Operators.CompareString(text2, "N/A", TextCompare: false) == 0)
								{
									MyChartActual = "";
								}
								if (Operators.CompareString(text2, text3, TextCompare: false) != 0)
								{
									string oldName = text3;
									string newName = text2;
									TreeView treeView;
									TreeNode CurrentNode = (treeView = TreeCol).SelectedNode;
									Update_Chart_Table_Name(oldName, newName, ref CurrentNode);
									treeView.SelectedNode = CurrentNode;
								}
								TreeCol.SelectedNode.Text = "Load Data {" + text + "}";
								TreeCol.SelectedNode.Nodes[0].Text = text2;
								TreeCol.SelectedNode.Nodes[0].Name = MyChartActual;
								TreeCol.SelectedNode.Name = MyWindow + text4;
								fSaveQuery = 1;
							}
							else if (!(LikeOperator.LikeString(MyTag, "JMP-*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "CHART_*", CompareMethod.Binary) | LikeOperator.LikeString(MyTag, "SCRIPT:*", CompareMethod.Binary)))
							{
							}
							goto end_IL_0001;
						}
						goto end_IL_0001_2;
					}
					case 5728:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_1696;
				}
				end_IL_0001_2:;
			}
			catch (object obj4) when (obj4 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj4);
				try0001_dispatch = 5728;
				continue;
			}
			break;
			IL_1696:
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

	public static bool Get_JMP_or_R_Columns(string MyScriptType, string MyTable, ref string[] ColArr)
	{
		bool result = false;
		bool flag = false;
		string MyTables = "";
		int num = 0;
		int num2 = 0;
		string MyDT = "";
		string text = "";
		checked
		{
			try
			{
				if (Get_JMP_R_Tables(ref MyTables, 1, MyScriptType))
				{
					string[] array = Strings.Split(MyTables, "~");
					if (Information.UBound(array) != -1)
					{
						int num3 = Information.UBound(array);
						for (num = 0; num <= num3; num++)
						{
							if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(array[num])), Strings.Trim(Strings.UCase(MyTable)) + ";*", CompareMethod.Binary))
							{
								text = array[num];
								ColArr = Strings.Split(text, ";");
								int num4 = Information.UBound(ColArr);
								for (num2 = 1; num2 <= num4; num2++)
								{
									text = Strings.Trim(ColArr[num2]);
									BuildForm.Strip_Col_DT(ref text, ref MyDT);
									ColArr[num2] = text;
								}
								result = true;
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error retrieving " + MyScriptType + " columns: " + ex2.Message + ")", MsgBoxStyle.Exclamation, "Column Error");
				ProjectData.ClearProjectError();
			}
			finally
			{
				string[] array = null;
			}
			return result;
		}
	}

	public static void Load_JMP_Tables_To_Combo(ref ComboBox MyCombo, ref string[] MyTableArr, string MyTemplateType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		int num6 = default(int);
		bool flag = default(bool);
		string MyTables = default(string);
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
				case 396:
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
							goto IL_0018;
						case 5:
							goto IL_001d;
						case 6:
							goto IL_0026;
						case 7:
							goto IL_002b;
						case 8:
							goto IL_003a;
						case 9:
							goto IL_0046;
						case 10:
							goto IL_0053;
						case 11:
							goto IL_0066;
						case 12:
							goto IL_007d;
						case 13:
							goto IL_008f;
						case 14:
							goto IL_0099;
						case 15:
							goto IL_00ab;
						case 16:
							goto IL_00b9;
						case 17:
						case 18:
							goto IL_00d1;
						case 19:
							goto IL_00e3;
						case 20:
						case 21:
						case 22:
							goto IL_00f6;
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d1:
					num2 = 18;
					MyCombo.Items.Add(text);
					goto IL_00e3;
					IL_00e3:
					num2 = 19;
					num5 = checked(num5 + 1);
					goto IL_00ec;
					IL_00b9:
					num2 = 16;
					text = Strings.Trim(Strings.Mid(text, 1, checked(num6 - 1)));
					goto IL_00d1;
					IL_00f6:
					num2 = 22;
					MyCombo.Items.Add("");
					break;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyTables = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = 0;
					goto IL_001d;
					IL_001d:
					num2 = 5;
					text = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					num6 = 0;
					goto IL_002b;
					IL_002b:
					num2 = 7;
					MyCombo.Items.Clear();
					goto IL_003a;
					IL_003a:
					num2 = 8;
					flag = Get_JMP_R_Tables(ref MyTables, 1, MyTemplateType);
					goto IL_0046;
					IL_0046:
					num2 = 9;
					if (flag)
					{
						goto IL_0053;
					}
					goto IL_00f6;
					IL_0053:
					num2 = 10;
					MyTableArr = Strings.Split(MyTables, "~");
					goto IL_0066;
					IL_0066:
					num2 = 11;
					if (Information.UBound(MyTableArr) != -1)
					{
						goto IL_007d;
					}
					goto IL_00f6;
					IL_007d:
					num2 = 12;
					num7 = Information.UBound(MyTableArr);
					num5 = 0;
					goto IL_00ec;
					IL_00ec:
					if (num5 <= num7)
					{
						goto IL_008f;
					}
					goto IL_00f6;
					IL_008f:
					num2 = 13;
					text = MyTableArr[num5];
					goto IL_0099;
					IL_0099:
					num2 = 14;
					num6 = Strings.InStr(text, ";");
					goto IL_00ab;
					IL_00ab:
					num2 = 15;
					if (num6 != 0)
					{
						goto IL_00b9;
					}
					goto IL_00d1;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				MyCombo.Sorted = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 396;
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

	public static string Get_R_Env_L()
	{
		return "spf_f999=\"" + Strings.Replace(Globals_Renamed.MyPCDir, "\\", "/", 1, -1, CompareMethod.Text) + Globals_Renamed.gSPFCache + ".RData\";if (file.exists(spf_f999)) {load(file = spf_f999)};";
	}

	public static bool Get_JMP_R_Tables(ref string MyTables, int MyMode, string MyScriptType)
	{
		int try0001_dispatch = -1;
		bool result = default(bool);
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
					string text = "";
					string text2 = "";
					string expression = "";
					string text3 = "";
					short num3 = 0;
					result = false;
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "General_Procedures - Get_JMP_R_Tables";
					if (Operators.CompareString(MyScriptType, "JSL", TextCompare: false) == 0)
					{
						MyScriptType = "JMP";
					}
					MyTables = "";
					string left = MyScriptType;
					if (Operators.CompareString(left, "JMP", TextCompare: false) != 0)
					{
						if (Operators.CompareString(left, "R", TextCompare: false) == 0)
						{
							text2 = "r.r$schema";
						}
					}
					else
					{
						text2 = Globals_Renamed.MyPCDir + "jsl.jsl$schema";
					}
					if (MyMode == 0 || MyMode == 3 || MyMode == 4)
					{
						string left2 = MyScriptType;
						if (Operators.CompareString(left2, "JMP", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left2, "R", TextCompare: false) == 0)
							{
								text3 = "";
								expression = BuildSQL.Write_VA_Lib("R_lib_3.lib");
							}
						}
						else
						{
							text3 = Globals_Renamed.gJSLOptions;
							text3 = Strings.Replace(text3, "@PROMPT@", "", 1, -1, CompareMethod.Text);
							expression = BuildSQL.Write_VA_Lib("JSL_lib_3.lib");
						}
						expression = Strings.Replace(expression, "@MYFILE@", text2, 1, -1, CompareMethod.Text);
						if (MyMode == 4)
						{
							MyTables = expression;
						}
						else if (Operators.CompareString(MyScriptType, "JMP", TextCompare: false) == 0)
						{
							expression = text3 + expression;
							BuildSQL.RunQuery(expression, 6, 1, -99, "");
						}
						else
						{
							expression = Globals_Renamed.gROptions + Get_R_Env_L() + "\r\n" + expression;
							BuildSQL.RunQuery(expression, 6, 1, -99, "");
						}
					}
					if (MyMode == 4)
					{
						goto end_IL_0001;
					}
					if (Operators.CompareString(MyScriptType, "R", TextCompare: false) == 0)
					{
						text2 = Globals_Renamed.MyPCDir + text2;
					}
					if (File.Exists(text2) && (MyMode == 1 || MyMode == 3))
					{
						text = General_Procedures.OpenReadFileContents(text2);
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							MyTables = text;
							result = true;
						}
					}
					goto end_IL_0001;
				}
				case 576:
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
				try0001_dispatch = 576;
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
		return result;
	}

	public static string Map_In_Out(string MyMap, string MyIn)
	{
		string result = MyIn;
		checked
		{
			try
			{
				string text = "";
				int num = 0;
				int num2 = 0;
				MyMap = Strings.Trim(MyMap);
				if ((Operators.CompareString(MyIn, "", TextCompare: false) != 0) & (Operators.CompareString(MyMap, "", TextCompare: false) != 0) & (Operators.CompareString(MyMap, "N/A", TextCompare: false) != 0))
				{
					text = "|" + Strings.UCase(MyIn) + "=";
					num = Strings.InStr(Strings.UCase(MyMap), text);
					if (num != 0)
					{
						int num3 = Strings.Len(text);
						text = Strings.Mid(MyMap, num + num3);
						num2 = Strings.InStr(text, "|");
						text = ((num2 == 0) ? "" : Strings.Mid(text, 1, num2 - 1));
					}
					else
					{
						text = "";
					}
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						result = text;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error mapping data in BuildChart - Map_In_Out: " + ex2.ToString(), MsgBoxStyle.Exclamation, "Map Error");
				result = MyIn;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	public static void Process_Chart(string MyText, ref TreeNode MyNode, string MyArg1 = "", string MyArg2 = "", string MyArg3 = "", string MyArg4 = "", string MyArg5 = "")
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
						errsource = "BuildChart - Process_Chart";
						if (MyNode == null)
						{
							goto end_IL_0001;
						}
						string text = "";
						int num3 = 18;
						int num4 = 0;
						int num5 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						text = Strings.UCase(MyText);
						num4 = Strings.InStr(text, ":");
						if (num4 != 0)
						{
							text3 = Strings.Trim(Strings.Mid(text + " ", num4));
							text = Strings.Mid(text, 1, num4 - 1);
						}
						else
						{
							text3 = "";
						}
						if ((Operators.CompareString(text, "SCRIPT", TextCompare: false) == 0) | (Operators.CompareString(text, "END BUTTON", TextCompare: false) == 0) | (Operators.CompareString(text, "END PROGRAM", TextCompare: false) == 0))
						{
							goto end_IL_0001;
						}
						if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareString(text, "BUTTON", TextCompare: false) == 0, LikeOperator.LikeObject(MyNode.Tag, "SCRIPT:*:BUTTON", CompareMethod.Binary))))
						{
							Interaction.MsgBox("You cannot nest buttons within buttons", MsgBoxStyle.Exclamation, "Buttons");
							goto end_IL_0001;
						}
						text4 = General_Procedures.Get_Chart_Script_Type(Conversions.ToString(MyNode.Tag), 1);
						TreeNode treeNode = new TreeNode();
						treeNode.Text = Strings.StrConv(MyText, VbStrConv.ProperCase);
						treeNode.Tag = text4 + "-" + text + text3;
						treeNode.Name = "";
						TreeNode treeNode2 = treeNode;
						switch (text)
						{
						case "SAVE WINDOW":
							num3 = 30;
							break;
						case "SAVE TABLE":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = "Save Table {" + MyArg1 + "}";
							num3 = 30;
							break;
						case "ADD JSL":
							treeNode2.Name = MyArg1;
							treeNode2.Text = "JSL";
							num3 = 31;
							break;
						case "ADD SCRIPT":
							treeNode2.Name = "";
							num3 = 31;
							break;
						case "ADD R-SCRIPT":
							treeNode2.Name = MyArg1;
							treeNode2.Text = "R-Script";
							num3 = 36;
							break;
						case "CLOSE ALL":
							treeNode2.Text = "Close Journals/Reports";
							num3 = 1;
							break;
						case "EXIT JMP":
							treeNode2.Text = "Exit JMP";
							num3 = 1;
							break;
						default:
							if ((Operators.CompareString(text, "CLOSE WINDOW", TextCompare: false) == 0) | (Operators.CompareString(text, "CLOSE TABLE", TextCompare: false) == 0) | (Operators.CompareString(text, "CLOSE ME", TextCompare: false) == 0))
							{
								num3 = 1;
								break;
							}
							switch (text)
							{
							case "JOURNAL WINDOW":
								num3 = 34;
								break;
							case "JOURNAL-TO-POWERPOINT":
								num3 = 34;
								break;
							case "END_FOR":
								num3 = 26;
								break;
							default:
								if (Operators.CompareString(text, "CREATE MENU WINDOW", TextCompare: false) != 0)
								{
									switch (text)
									{
									case "ADD LAYOUT":
										num3 = 34;
										break;
									case "APPEND LAYOUT":
										num3 = 13;
										break;
									case "ADD COLUMN":
										num3 = 27;
										break;
									case "LOAD DATA":
										num3 = 3;
										break;
									case "BUTTON":
										treeNode2.Text = "Button";
										num3 = 36;
										break;
									case "R-PROGRAM":
										treeNode2.Text = "R-Program";
										num3 = 36;
										break;
									case "REPORT":
										treeNode2.Text = "Report";
										num3 = 43;
										break;
									case "REPORTI":
										treeNode2.Text = "Interactive Report";
										num3 = 43;
										break;
									case "REPCSS":
										treeNode2.Text = "Report Style Sheet";
										num3 = 42;
										break;
									default:
										if (!(LikeOperator.LikeString(text, "CHART*", CompareMethod.Binary) | (Operators.CompareString(text, "NEWTABLE", TextCompare: false) == 0) | (Operators.CompareString(text, "NOTABLE", TextCompare: false) == 0)))
										{
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*SORT.INI", CompareMethod.Binary))
										{
											num3 = 37;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*SUMMARY.INI", CompareMethod.Binary))
										{
											num3 = 38;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*FOR_ALL_*.INI", CompareMethod.Binary))
										{
											num3 = 24;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*FILTER.INI", CompareMethod.Binary))
										{
											num3 = 35;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*PSEUDO_SIGMA_COLUMN.INI", CompareMethod.Binary))
										{
											num3 = 27;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*TEXT.INI", CompareMethod.Binary))
										{
											num3 = 34;
											break;
										}
										if (LikeOperator.LikeString(Strings.UCase(MyArg3), "*SHOWTABLE.INI", CompareMethod.Binary))
										{
											num3 = 33;
											break;
										}
										switch (text)
										{
										default:
											if (Operators.CompareString(text, "CHARTI", TextCompare: false) != 0)
											{
												num3 = 36;
												break;
											}
											goto case "CHART";
										case "CHART":
										case "CHARTR":
										case "CHARTP":
											num3 = 32;
											break;
										}
										break;
									}
									break;
								}
								goto case "CREATE WINDOW";
							case "CREATE WINDOW":
							case "CREATE TAB WINDOW":
								treeNode2.Name = MyArg1;
								num3 = 34;
								break;
							}
							break;
						}
						treeNode2.ImageIndex = num3;
						treeNode2.SelectedImageIndex = num3;
						switch (text)
						{
						case "LOAD DATA":
						{
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = "Load Data {" + MyArg1 + "}";
							if (Operators.CompareString(MyArg1, "No", TextCompare: false) == 0)
							{
								text2 = "N/A";
							}
							else
							{
								text2 = MyArg4;
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = Derive_Chart_Table(ref MyArg1);
								}
							}
							MyArg3 = Strings.Trim(Strings.UCase(MyArg3));
							if (Strings.Len(MyArg3) < 2 && Operators.CompareString(MyArg3, "N", TextCompare: false) != 0)
							{
								MyArg3 = "Y";
							}
							treeNode2.Name = MyArg3;
							treeNode = new TreeNode();
							treeNode.Text = text2;
							treeNode.Tag = "CHART_TABLE";
							treeNode.Name = MyArg2;
							treeNode.ImageIndex = 33;
							treeNode.SelectedImageIndex = 33;
							TreeNode node2 = treeNode;
							treeNode2.Nodes.Add(node2);
							break;
						}
						case "CLOSE WINDOW":
						case "JOURNAL WINDOW":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = treeNode2.Text + " {" + MyArg1 + "}";
							break;
						case "ADD SCRIPT":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = "Script {" + MyArg1 + "}";
							break;
						case "SAVE WINDOW":
						{
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode2.Text = treeNode2.Text + " {" + MyArg2 + "}";
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode = new TreeNode();
							treeNode.Text = "File {" + MyArg1 + "}";
							treeNode.Tag = "CHART_FILE2";
							treeNode.ImageIndex = 33;
							treeNode.SelectedImageIndex = 33;
							TreeNode node5 = treeNode;
							treeNode2.Nodes.Add(node5);
							break;
						}
						case "CREATE WINDOW":
						{
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "Win" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg2));
								}
							}
							treeNode2.Text = "Create Window {" + MyArg2 + "}";
							if (Operators.CompareString(MyArg3, "", TextCompare: false) == 0)
							{
								MyArg3 = ((Operators.CompareString(text4, "JMP", TextCompare: false) != 0) ? "Window Title or Email Subject" : "Window Title");
							}
							treeNode = new TreeNode();
							treeNode.Text = MyArg3;
							treeNode.Tag = "CHART_TITLE";
							treeNode.ImageIndex = 18;
							treeNode.SelectedImageIndex = 18;
							TreeNode node4 = treeNode;
							treeNode2.Nodes.Add(node4);
							break;
						}
						case "CREATE TAB WINDOW":
						case "CREATE MENU WINDOW":
						{
							if (Operators.CompareString(text, "CREATE MENU WINDOW", TextCompare: false) == 0)
							{
								treeNode2.Text = "Create Menu Window";
							}
							else
							{
								treeNode2.Text = "Create Tab Window";
							}
							if (Operators.CompareString(MyArg3, "", TextCompare: false) == 0)
							{
								MyArg3 = "Window Title";
							}
							treeNode = new TreeNode();
							treeNode.Text = MyArg3;
							treeNode.Tag = "CHART_TITLE";
							treeNode.ImageIndex = 18;
							treeNode.SelectedImageIndex = 18;
							TreeNode node3 = treeNode;
							treeNode2.Nodes.Add(node3);
							break;
						}
						case "CLOSE TABLE":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = "Close Table {" + MyArg1 + "}";
							break;
						case "BUTTON":
						{
							TreeNode treeNode5 = new TreeNode();
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "MyButton" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
								if ((Operators.CompareString(text4, "HTML", TextCompare: false) == 0) | (Operators.CompareString(text4, "R", TextCompare: false) == 0))
								{
									(treeNode = treeNode2).Tag = Operators.ConcatenateObject(treeNode.Tag, ".BTN");
								}
							}
							text5 = Strings.Trim(Strings.Mid(MyArg1, 9));
							treeNode5.Text = MyArg1;
							treeNode5.Tag = "CHART_VARIABLE";
							treeNode5.ImageIndex = 18;
							treeNode5.SelectedImageIndex = 18;
							treeNode2.Nodes.Add(treeNode5);
							if (Operators.CompareString(MyArg2, "", TextCompare: false) != 0)
							{
								treeNode2.Text = "Button {" + MyArg2 + "}";
							}
							treeNode = new TreeNode();
							treeNode.Text = "Script";
							treeNode.Name = MyNode.Parent.Parent.Name + ":" + text5;
							treeNode.Tag = "SCRIPT:" + text4 + ":BUTTON";
							TreeNode treeNode6 = treeNode;
							if ((Operators.CompareString(text4, "HTML", TextCompare: false) == 0) | (Operators.CompareString(text4, "R", TextCompare: false) == 0))
							{
								treeNode6.ImageIndex = 41;
								treeNode6.SelectedImageIndex = 41;
							}
							else
							{
								treeNode6.ImageIndex = 31;
								treeNode6.SelectedImageIndex = 31;
							}
							treeNode2.Nodes.Add(treeNode6);
							break;
						}
						case "R-PROGRAM":
						{
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "MyRProgram" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
							}
							text5 = Strings.Trim(Strings.Mid(MyArg1, 11));
							treeNode = new TreeNode();
							treeNode.Text = "Script";
							treeNode.Name = MyNode.Parent.Parent.Name + ":" + text5;
							treeNode.Tag = "SCRIPT:R:RPROGRAM";
							treeNode.ImageIndex = 44;
							treeNode.SelectedImageIndex = 44;
							TreeNode node6 = treeNode;
							treeNode2.Nodes.Add(node6);
							break;
						}
						case "JOURNAL-TO-POWERPOINT":
						{
							TreeNode treeNode12 = new TreeNode();
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "Current Journal()";
							}
							treeNode12.Text = MyArg1;
							treeNode12.Tag = "CHART_JOURNAL";
							treeNode12.ImageIndex = 18;
							treeNode12.SelectedImageIndex = 18;
							treeNode2.Nodes.Add(treeNode12);
							TreeNode treeNode13 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode13.Text = "PowerPoint File {" + MyArg2 + "}";
							treeNode13.Tag = "CHART_PPT";
							treeNode13.ImageIndex = 33;
							treeNode13.SelectedImageIndex = 33;
							treeNode2.Nodes.Add(treeNode13);
							TreeNode treeNode14 = new TreeNode();
							if (Operators.CompareString(MyArg3, "", TextCompare: false) == 0)
							{
								MyArg3 = "No";
							}
							treeNode14.Text = "Opt PowerPoint Template {" + MyArg3 + "}";
							treeNode14.Tag = "CHART_PPT2";
							treeNode14.ImageIndex = 33;
							treeNode14.SelectedImageIndex = 33;
							treeNode2.Nodes.Add(treeNode14);
							break;
						}
						case "SAVE TABLE":
						{
							TreeNode treeNode7 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode7.Text = "File {" + MyArg2 + "}";
							if (Operators.CompareString(text4, "R", TextCompare: false) == 0)
							{
								treeNode7.Tag = "CHART_FILER";
							}
							else
							{
								treeNode7.Tag = "CHART_FILE";
							}
							treeNode7.ImageIndex = 33;
							treeNode7.SelectedImageIndex = 33;
							treeNode2.Nodes.Add(treeNode7);
							break;
						}
						case "NOTABLE":
						{
							TreeNode treeNode15 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode15.Text = "Source Table {" + MyArg2 + "}";
							treeNode15.Tag = "CHART_TABLE_READ";
							treeNode15.ImageIndex = 33;
							treeNode15.SelectedImageIndex = 33;
							treeNode2.Text = Get_Chart_Obj_Name(MyArg3);
							treeNode15.Name = MyArg3;
							treeNode2.Name = MyArg4;
							treeNode2.Nodes.Add(treeNode15);
							break;
						}
						case "CHART2":
						{
							text2 = (treeNode2.Text = Get_Chart_Obj_Name(MyArg3));
							TreeNode treeNode8 = new TreeNode();
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "My" + text2 + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
							}
							treeNode8.Text = MyArg1;
							treeNode8.Tag = "CHART_VARIABLE";
							treeNode8.ImageIndex = 18;
							treeNode8.SelectedImageIndex = 18;
							treeNode8.Name = MyArg3;
							treeNode2.Name = MyArg4;
							treeNode2.Nodes.Add(treeNode8);
							break;
						}
						case "CHART":
						case "NEWTABLE":
						case "CHARTR":
						case "CHARTI":
						case "CHARTP":
						{
							text2 = (treeNode2.Text = Get_Chart_Obj_Name(MyArg3));
							TreeNode treeNode9 = new TreeNode();
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "My" + text2 + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
								if ((Operators.CompareString(text4, "HTML", TextCompare: false) == 0) | (Operators.CompareString(text4, "R", TextCompare: false) == 0))
								{
									if (Operators.CompareString(text, "CHARTR", TextCompare: false) == 0 || Operators.CompareString(text4, "R", TextCompare: false) == 0)
									{
										(treeNode = treeNode2).Tag = Operators.ConcatenateObject(treeNode.Tag, ".RRR");
									}
									else if (Operators.CompareString(text, "CHARTP", TextCompare: false) == 0)
									{
										(treeNode = treeNode2).Tag = Operators.ConcatenateObject(treeNode.Tag, ".PPP");
									}
									else
									{
										(treeNode = treeNode2).Tag = Operators.ConcatenateObject(treeNode.Tag, ".GIF");
									}
								}
								if (Operators.CompareString(text, "NEWTABLE", TextCompare: false) == 0)
								{
									MyArg1 = "N/A";
								}
							}
							treeNode9.Name = "";
							treeNode9.Text = MyArg1;
							if (LikeOperator.LikeString(text, "CHART*", CompareMethod.Binary))
							{
								treeNode9.Tag = "CHART_VARIABLE";
								treeNode9.ImageIndex = 18;
								treeNode9.SelectedImageIndex = 18;
							}
							else
							{
								treeNode9.Tag = "CHART_TABLE";
								treeNode9.ImageIndex = 33;
								treeNode9.SelectedImageIndex = 33;
							}
							TreeNode treeNode10 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode10.Text = "Source Table {" + MyArg2 + "}";
							treeNode10.Tag = "CHART_TABLE_READ";
							treeNode10.ImageIndex = 33;
							treeNode10.SelectedImageIndex = 33;
							treeNode9.Name = MyArg3;
							if ((Operators.CompareString(Strings.UCase(treeNode2.Text), "GNUPLOTCHART", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(treeNode9.Name), "", TextCompare: false) == 0))
							{
								treeNode9.Name = "gnuplotchart.ini";
							}
							treeNode2.Name = MyArg4;
							treeNode2.Nodes.Add(treeNode9);
							treeNode2.Nodes.Add(treeNode10);
							if (Operators.CompareString(Strings.UCase(MyArg3), "JOIN.INI", TextCompare: false) == 0)
							{
								TreeNode treeNode11 = new TreeNode();
								text2 = Get_StdCtrl("PSEUDO-COL");
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = "No";
								}
								treeNode11.Text = "Source Table {" + text2 + "}";
								treeNode11.Tag = "CHART_TABLE_READ";
								treeNode11.ImageIndex = 33;
								treeNode11.SelectedImageIndex = 33;
								treeNode2.Nodes.Add(treeNode11);
							}
							if (LikeOperator.LikeString(text, "CHART*", CompareMethod.Binary))
							{
								num5 = 21;
								if (Operators.CompareString(MyArg5, "No", TextCompare: false) != 0)
								{
									MyArg5 = "Yes";
									num5 = 20;
								}
								BuildForm.Add_Tree_Node(treeNode2, "Enabled {" + MyArg5 + "}", Conversions.ToString(Operators.ConcatenateObject("ENABLED:C", treeNode2.Tag)), "ENABLEDC", (short)num5);
							}
							break;
						}
						case "ADD JSL":
						case "ADD R-SCRIPT":
						{
							TreeNode treeNode3 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								text2 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), "MYOTHERRPT"), text2);
								}
								if (Operators.CompareString(text, "ADD R-SCRIPT", TextCompare: false) == 0)
								{
									(treeNode = treeNode2).Tag = Operators.ConcatenateObject(treeNode.Tag, ".RRR");
									MyArg2 = "MyOtherRpt" + text2;
								}
							}
							treeNode3.Text = MyArg2;
							treeNode3.Tag = "CHART_VARIABLE";
							treeNode3.ImageIndex = 18;
							treeNode3.SelectedImageIndex = 18;
							treeNode2.Nodes.Add(treeNode3);
							break;
						}
						case "ADD LAYOUT":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "Layout" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter();
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
							}
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0 || Strings.Len(MyArg2) > 1 || ((Operators.CompareString(MyArg2, "1", TextCompare: false) < 0) & (Operators.CompareString(MyArg2, "9", TextCompare: false) > 0)))
							{
								MyArg2 = "1";
							}
							treeNode2.Text = "Add Layout {" + MyArg1 + ":" + MyArg2 + "}";
							break;
						case "APPEND LAYOUT":
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = treeNode2.Text + " {" + MyArg1 + "}";
							treeNode2.Name = MyArg2;
							break;
						case "ADD COLUMN":
						{
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = "No";
							}
							treeNode2.Text = "Add Column {" + MyArg1 + "}";
							TreeNode treeNode4 = new TreeNode();
							if (Operators.CompareString(MyArg2, "", TextCompare: false) == 0)
							{
								MyArg2 = "No";
							}
							treeNode4.Text = "Source Table {" + MyArg2 + "}";
							treeNode4.Tag = "CHART_TABLE_WRITE";
							treeNode4.Name = "";
							treeNode4.ImageIndex = 33;
							treeNode4.SelectedImageIndex = 33;
							treeNode2.Name = MyArg3;
							treeNode2.Nodes.Add(treeNode4);
							break;
						}
						case "REPORT":
						case "REPORTI":
						case "REPCSS":
						{
							if (Operators.CompareString(MyArg1, "", TextCompare: false) == 0)
							{
								MyArg1 = ((Operators.CompareString(text, "REPORT", TextCompare: false) != 0 && Operators.CompareString(text, "REPORTI", TextCompare: false) != 0) ? ("MyCSS" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter()) : ("MyReport" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Next_Chart_Counter()));
								if (Strings.InStr(Conversions.ToString(treeNode2.Tag), ":") == 0)
								{
									treeNode2.Tag = Operators.ConcatenateObject(Operators.ConcatenateObject(treeNode2.Tag, ":"), Strings.UCase(MyArg1));
								}
							}
							treeNode = new TreeNode();
							treeNode.Name = "";
							treeNode.Text = MyArg1;
							treeNode.Tag = "CHART_VARIABLE";
							treeNode.ImageIndex = 18;
							treeNode.SelectedImageIndex = 18;
							TreeNode node = treeNode;
							treeNode2.Name = MyArg2;
							treeNode2.Nodes.Add(node);
							break;
						}
						}
						MyNode.Nodes.Add(treeNode2);
						MyNode.Expand();
						goto end_IL_0001_2;
					}
					case 7684:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_1e3a;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 7684;
				continue;
			}
			break;
			IL_1e3a:
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

	public static bool Check_For_Create_Window(string LayOutMode, string MyTrueCol, ref TreeNode MyParentNode, ref string MyConflictNode)
	{
		bool result = true;
		MyConflictNode = "";
		try
		{
			string text = "";
			string text2 = "";
			TreeNode treeNode = null;
			int num = 0;
			text = Strings.UCase(MyTrueCol);
			checked
			{
				foreach (TreeNode node in MyParentNode.Nodes)
				{
					if (Conversions.ToBoolean(Operators.OrObject(LikeOperator.LikeObject(node.Tag, "JMP-CREATE WINDOW*", CompareMethod.Binary), LikeOperator.LikeObject(node.Tag, "HTML-CREATE WINDOW*", CompareMethod.Binary))))
					{
						text2 = Strings.UCase(node.Name);
						num = Strings.InStr(text2, "@@@");
						if (num == 0)
						{
							continue;
						}
						text2 = Strings.Mid(text2, num + Strings.Len("@@@"));
						text2 = Strings.Replace(text2, "|", ";", 1, -1, CompareMethod.Text);
						text2 = Strings.Trim(Strings.Replace(text2, "@@@", ";", 1, -1, CompareMethod.Text));
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							text2 = ";" + Strings.UCase(text2) + ";";
						}
						if (Strings.InStr(text2, ";" + text + ";") != 0)
						{
							result = false;
							MyConflictNode = "\"" + node.Text + "\"";
							if (node.Parent.Parent != null)
							{
								MyConflictNode = MyConflictNode + " under step \"" + node.Parent.Parent.Text + "\"";
							}
							break;
						}
					}
					else
					{
						if (!Conversions.ToBoolean(LikeOperator.LikeObject(node.Tag, "JMP-APPEND LAYOUT*", CompareMethod.Binary)))
						{
							continue;
						}
						text2 = ((Operators.CompareString(LayOutMode, "L", TextCompare: false) != 0) ? Strings.UCase(node.Name) : Strings.UCase(General_Procedures.Get_Node_Value(node.Text)));
						num = Strings.InStr(text2, ":");
						if (num != 0)
						{
							text2 = Strings.Mid(text2, 1, num - 1);
						}
						if (Operators.CompareString(text, text2, TextCompare: false) == 0)
						{
							result = false;
							MyConflictNode = "\"" + node.Text + "\"";
							if (node.Parent.Parent != null)
							{
								MyConflictNode = MyConflictNode + " under step \"" + node.Parent.Parent.Text + "\"";
							}
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error verifying object for deletion in BuildChart - Check_For_Create_Window: " + ex2.ToString(), (MsgBoxStyle)Conversions.ToInteger(Conversions.ToString(48) + Conversions.ToString(0)), "Error");
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Check_Chart_Obj_Name(string MyMode, string Chart_Type, ref TreeNode ANode, string SrcTag, string SrcObj, ref string MyConflictNode)
	{
		string result = Conversions.ToString(Value: true);
		MyConflictNode = "";
		try
		{
			string right = "";
			if (Operators.CompareString(MyMode, "T", TextCompare: false) == 0)
			{
				right = (Conversions.ToBoolean(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-CHART:*", CompareMethod.Binary)) ? Strings.UCase(General_Procedures.Get_Node_Value(ANode.Nodes[1].Text)) : (Conversions.ToBoolean(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-ADD COLUMN*", CompareMethod.Binary)) ? Strings.UCase(General_Procedures.Get_Node_Value(ANode.Nodes[0].Text)) : (Conversions.ToBoolean(Operators.OrObject(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-CLOSE TABLE*", CompareMethod.Binary), LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-SAVE TABLE*", CompareMethod.Binary))) ? Strings.UCase(General_Procedures.Get_Node_Value(ANode.Text)) : (Conversions.ToBoolean(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-NEWTABLE*", CompareMethod.Binary)) ? Strings.UCase(General_Procedures.Get_Node_Value(ANode.Nodes[1].Text)) : ((!Conversions.ToBoolean(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-NOTABLE*", CompareMethod.Binary))) ? "" : Strings.UCase(General_Procedures.Get_Node_Value(ANode.Nodes[0].Text)))))));
			}
			else if (Operators.CompareString(MyMode, "W", TextCompare: false) == 0 && Conversions.ToBoolean(Operators.OrObject(Operators.OrObject(LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-SAVE WINDOW*", CompareMethod.Binary), LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-JOURNAL WINDOW*", CompareMethod.Binary)), LikeOperator.LikeObject(ANode.Tag, Chart_Type + "-CLOSE WINDOW*", CompareMethod.Binary))))
			{
				right = Strings.UCase(General_Procedures.Get_Node_Value(ANode.Text));
			}
			if (Operators.CompareString(SrcObj, right, TextCompare: false) == 0)
			{
				result = Conversions.ToString(Value: false);
				MyConflictNode = "\"" + ANode.Text + "\"";
				if (ANode.Parent.Parent != null)
				{
					MyConflictNode = MyConflictNode + " under step \"" + ANode.Parent.Parent.Text + "\"";
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error verifying object for deletion in BuildChart - Check_Chart_Obj_Name: " + ex2.ToString(), (MsgBoxStyle)Conversions.ToInteger(Conversions.ToString(48) + Conversions.ToString(0)), "Error");
			result = Conversions.ToString(Value: false);
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static bool Check_Chart_Delete2(int MyMode, ref TreeNode MyNode)
	{
		bool flag = true;
		try
		{
			string text = "";
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			string text2 = "";
			TreeNode treeNode3 = null;
			TreeNode treeNode4 = null;
			string text3 = "";
			string text4 = "";
			int num = 0;
			string text5 = "";
			string text6 = "";
			string MyConflictNode = "";
			string text7 = "delete";
			switch (MyMode)
			{
			case 1:
				text7 = "move";
				break;
			case 2:
				text7 = "disable";
				break;
			}
			checked
			{
				if (MyNode != null)
				{
					if (Conversions.ToBoolean(Operators.AndObject(MyMode == 2, Operators.CompareObjectEqual(MyNode.Tag, "ENABLEDC", TextCompare: false))))
					{
						MyNode = MyNode.Parent;
					}
					TreeNode treeNode5;
					object[] array;
					bool[] array2;
					object obj = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (treeNode5 = MyNode).Tag }, null, null, array2 = new bool[1] { true });
					if (array2[0])
					{
						treeNode5.Tag = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					bool num2 = Operators.CompareString(Strings.Mid(Conversions.ToString(obj), 1, 4), "JMP-", TextCompare: false) != 0;
					object obj2 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (treeNode5 = MyNode).Tag }, null, null, array2 = new bool[1] { true });
					if (array2[0])
					{
						treeNode5.Tag = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					bool num3 = num2 & (Operators.CompareString(Strings.Mid(Conversions.ToString(obj2), 1, 5), "HTML-", TextCompare: false) != 0);
					object obj3 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array = new object[1] { (treeNode5 = MyNode).Tag }, null, null, array2 = new bool[1] { true });
					if (array2[0])
					{
						treeNode5.Tag = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (!(num3 & (Operators.CompareString(Strings.Mid(Conversions.ToString(obj3), 1, 2), "R-", TextCompare: false) != 0)))
					{
						Type typeFromHandle = typeof(Strings);
						object[] obj4 = new object[1] { (treeNode5 = MyNode).Tag };
						array = obj4;
						bool[] obj5 = new bool[1] { true };
						array2 = obj5;
						object obj6 = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj4, null, null, obj5);
						if (array2[0])
						{
							treeNode5.Tag = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (Operators.CompareString(Strings.Mid(Conversions.ToString(obj6), 1, 4), "JMP-", TextCompare: false) == 0)
						{
							text = Strings.Trim(Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(MyNode.Tag, " ")), 5));
						}
						else
						{
							Type typeFromHandle2 = typeof(Strings);
							object[] obj7 = new object[1] { (treeNode5 = MyNode).Tag };
							array = obj7;
							bool[] obj8 = new bool[1] { true };
							array2 = obj8;
							object obj9 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj7, null, null, obj8);
							if (array2[0])
							{
								treeNode5.Tag = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							text = ((Operators.CompareString(Strings.Mid(Conversions.ToString(obj9), 1, 2), "R-", TextCompare: false) != 0) ? Strings.Trim(Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(MyNode.Tag, " ")), 6)) : ((!Conversions.ToBoolean(LikeOperator.LikeObject(MyNode.Tag, "R-CHART*", CompareMethod.Binary))) ? Strings.Trim(Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(MyNode.Tag, " ")), 3)) : ("R" + Strings.Trim(Strings.Mid(Conversions.ToString(Operators.ConcatenateObject(MyNode.Tag, " ")), 3)))));
						}
						num = Strings.InStr(text, ":");
						if (num != 0)
						{
							text5 = Strings.Mid(text, num + 1);
							text = Strings.Mid(text, 1, num - 1);
							if (Operators.CompareString(Strings.UCase(text), "REPORTI", TextCompare: false) == 0)
							{
								text5 = "I:" + text5;
							}
							else if (Operators.CompareString(Strings.UCase(text), "CHARTI", TextCompare: false) == 0)
							{
								text5 = "CI:" + text5;
							}
						}
						else
						{
							text5 = "";
						}
						treeNode = MyNode.Parent;
						switch (MyMode)
						{
						case 1:
							flag = true;
							break;
						case 0:
						case 2:
							if (Operators.CompareString(text, "RCHART", TextCompare: false) == 0)
							{
								treeNode = treeNode.Parent.Parent;
							}
							if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "SCRIPT:*:BUTTON", CompareMethod.Binary)))
							{
								treeNode2 = treeNode.Parent.Parent;
							}
							else if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "SCRIPT:*", CompareMethod.Binary)))
							{
								treeNode2 = treeNode;
							}
							text2 = General_Procedures.Get_Chart_Script_Type(Conversions.ToString(treeNode2.Tag), 1);
							switch (text)
							{
							case "LOAD DATA":
							case "NEWTABLE":
							case "CREATE WINDOW":
								if (Operators.CompareString(text, "LOAD DATA", TextCompare: false) == 0)
								{
									text3 = Strings.UCase(MyNode.Nodes[0].Text);
									text6 = "T";
								}
								else if (Operators.CompareString(text, "NEWTABLE", TextCompare: false) == 0)
								{
									text3 = Strings.UCase(General_Procedures.Get_Node_Value(MyNode.Nodes[0].Text));
									text6 = "T";
								}
								else
								{
									text3 = Strings.UCase(General_Procedures.Get_Node_Value(MyNode.Text));
									text6 = "W";
								}
								if (!((Operators.CompareString(text3, "N/A", TextCompare: false) == 0) | (Operators.CompareString(text3, "NO", TextCompare: false) == 0)))
								{
									foreach (TreeNode node in treeNode2.Nodes)
									{
										treeNode3 = node;
										if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode3.Tag, text2 + "-BUTTON:*", CompareMethod.Binary)))
										{
											foreach (TreeNode node2 in treeNode3.Nodes[1].Nodes)
											{
												treeNode4 = node2;
												flag = Conversions.ToBoolean(Check_Chart_Obj_Name(text6, text2, ref treeNode4, text, text3, ref MyConflictNode));
												if (!flag)
												{
													break;
												}
											}
										}
										else
										{
											flag = Conversions.ToBoolean(Check_Chart_Obj_Name(text6, text2, ref treeNode3, text, text3, ref MyConflictNode));
										}
										if (!flag)
										{
											break;
										}
									}
									break;
								}
								goto end_IL_0003;
							case "CHART":
							case "CHARTI":
							case "ADD JSL":
							case "CHART2":
							case "CHARTR":
							case "CHARTP":
							case "REPORT":
							case "REPORTI":
							case "ADD R-SCRIPT":
							case "RCHART":
								flag = Check_For_Create_Window("D", text5, ref treeNode, ref MyConflictNode);
								break;
							case "ADD LAYOUT":
								flag = Check_For_Create_Window("L", text5, ref treeNode, ref MyConflictNode);
								break;
							case "BUTTON":
								if (MyNode.Nodes[1].GetNodeCount(includeSubTrees: false) == 0)
								{
									flag = Check_For_Create_Window("D", text5, ref treeNode, ref MyConflictNode);
									break;
								}
								Interaction.MsgBox("Buttons with Script Objects cannot be deleted. First remove script objects", MsgBoxStyle.Exclamation, "Delete");
								flag = false;
								goto end_IL_0003;
							}
							break;
						}
						if (!flag & (Operators.CompareString(MyConflictNode, "", TextCompare: false) != 0))
						{
							Interaction.MsgBox("To " + text7 + " this node, you must first remove dependency on it at node " + MyConflictNode, MsgBoxStyle.Exclamation, "Delete");
						}
					}
				}
			}
			end_IL_0003:;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error verifying object for deletion in BuildChart - Check_Chart_Delete: " + ex2.ToString(), (MsgBoxStyle)Conversions.ToInteger(Conversions.ToString(48) + Conversions.ToString(0)), "Delete");
			flag = false;
			ProjectData.ClearProjectError();
		}
		return flag;
	}

	public static string Get_JSL_Display_Label(string MyTrue, ref string[] MyTrueArr, ref string[] MyDisplayArr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string result = default(string);
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
							goto IL_0010;
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
							goto IL_0057;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0060:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_002f;
					IL_002f:
					num2 = 6;
					if (Operators.CompareString(Strings.UCase(MyTrueArr[num5]), MyTrue, TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0057;
					IL_001e:
					num2 = 5;
					num6 = Information.UBound(MyTrueArr);
					num5 = 0;
					goto IL_0060;
					IL_0057:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_0060;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					result = MyTrue;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					MyTrue = Strings.UCase(MyTrue);
					goto IL_001e;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = MyDisplayArr[num5];
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
		return result;
	}

	public static void Update_Combo_Chart_Table(ref ComboBox MyCombo, string MyTable)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		bool flag = default(bool);
		string prompt = default(string);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Type typeFromHandle;
					object[] obj;
					bool[] obj2;
					object left;
					Type typeFromHandle2;
					object[] obj3;
					ComboBox.ObjectCollection items;
					int index;
					object[] array;
					bool[] obj4;
					bool[] array2;
					object left2;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 717:
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
								goto IL_0022;
							case 6:
								goto IL_0041;
							case 7:
								goto IL_0058;
							case 8:
								goto IL_0069;
							case 9:
								goto IL_0079;
							case 11:
								goto IL_008c;
							case 12:
								goto IL_0091;
							case 13:
								goto IL_00ac;
							case 14:
								goto IL_0125;
							case 15:
								goto IL_0132;
							case 17:
							case 18:
								goto IL_013b;
							case 16:
							case 19:
								goto IL_014d;
							case 20:
								goto IL_015d;
							case 21:
								goto IL_016f;
							case 22:
								goto IL_0180;
							case 23:
								goto IL_019b;
							case 24:
								goto end_IL_0001_2;
							case 26:
							case 27:
								goto IL_0225;
							default:
								goto end_IL_0001;
							case 10:
							case 25:
							case 28:
							case 29:
							case 30:
							case 31:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0180:
						num2 = 22;
						num5 = MyCombo.Items.Count - 1;
						num6 = 0;
						goto IL_022e;
						IL_022e:
						if (num6 > num5)
						{
							goto end_IL_0001_3;
						}
						goto IL_019b;
						IL_016f:
						num2 = 21;
						MyCombo.Items.Add(MyTable);
						goto IL_0180;
						IL_019b:
						num2 = 23;
						typeFromHandle = typeof(Strings);
						obj = new object[1] { (items = MyCombo.Items)[index = num6] };
						array = obj;
						obj2 = new bool[1] { true };
						array2 = obj2;
						left = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
						if (array2[0])
						{
							items[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (Operators.ConditionalCompareObjectEqual(left, Strings.UCase(MyTable), TextCompare: false))
						{
							break;
						}
						goto IL_0225;
						IL_000b:
						num2 = 2;
						flag = false;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num6 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						prompt = General_Procedures.Get_UI("errupdttbl");
						goto IL_0022;
						IL_0022:
						num2 = 5;
						if (Operators.CompareString(Strings.Trim(MyTable), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0041;
						IL_0041:
						num2 = 6;
						if (MyCombo.Items.Count == 1)
						{
							goto IL_0058;
						}
						goto IL_008c;
						IL_0058:
						num2 = 7;
						Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, "Not Found");
						goto IL_0069;
						IL_0069:
						num2 = 8;
						MyCombo.Items.Add(MyTable);
						goto IL_0079;
						IL_0079:
						num2 = 9;
						MyCombo.SelectedIndex = 1;
						goto end_IL_0001_3;
						IL_008c:
						num2 = 11;
						flag = false;
						goto IL_0091;
						IL_0091:
						num2 = 12;
						num7 = MyCombo.Items.Count - 1;
						num6 = 0;
						goto IL_0144;
						IL_0144:
						if (num6 <= num7)
						{
							goto IL_00ac;
						}
						goto IL_014d;
						IL_00ac:
						num2 = 13;
						typeFromHandle2 = typeof(Strings);
						obj3 = new object[1] { (items = MyCombo.Items)[index = num6] };
						array = obj3;
						obj4 = new bool[1] { true };
						array2 = obj4;
						left2 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj3, null, null, obj4);
						if (array2[0])
						{
							items[index] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
						}
						if (Operators.ConditionalCompareObjectEqual(left2, Strings.UCase(MyTable), TextCompare: false))
						{
							goto IL_0125;
						}
						goto IL_013b;
						IL_0225:
						num2 = 27;
						num6++;
						goto IL_022e;
						IL_013b:
						num2 = 18;
						num6++;
						goto IL_0144;
						IL_0125:
						num2 = 14;
						MyCombo.SelectedIndex = num6;
						goto IL_0132;
						IL_0132:
						num2 = 15;
						flag = true;
						goto IL_014d;
						IL_014d:
						num2 = 19;
						if (flag)
						{
							goto end_IL_0001_3;
						}
						goto IL_015d;
						IL_015d:
						num2 = 20;
						Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, "Not Found");
						goto IL_016f;
						end_IL_0001_2:
						break;
					}
					num2 = 24;
					MyCombo.SelectedIndex = num6;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj5) when (obj5 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj5);
				try0001_dispatch = 717;
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

	public static string Add_JSL_Title(string MyTitle, string MyScriptType, string MyMode = "C", string MyData = "")
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
				case 468:
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
						case 5:
							goto IL_003d;
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0059;
						case 8:
							goto IL_0068;
						case 9:
							goto IL_0076;
						case 11:
							goto IL_008c;
						case 13:
							goto IL_00b6;
						case 14:
							goto IL_00c5;
						case 15:
							goto IL_00d4;
						case 16:
							goto IL_00e4;
						case 17:
							goto IL_00f3;
						case 19:
							goto IL_0105;
						case 20:
							goto IL_0114;
						case 21:
							goto IL_0137;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 10:
						case 12:
						case 18:
						case 23:
						case 24:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003d:
					num2 = 5;
					text += "/*------------------------------------------------------------------------------------";
					goto IL_004b;
					IL_004b:
					num2 = 6;
					text += "\r\n|";
					goto IL_0059;
					IL_00f3:
					num2 = 17;
					text += "\r\n#--------------------------------------------------------------------------------------";
					goto end_IL_0001_3;
					IL_0059:
					num2 = 7;
					text = text + "\r\n| " + MyTitle;
					goto IL_0068;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(MyScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_003d;
					}
					if (Operators.CompareString(MyScriptType, "R", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_008c;
					IL_0068:
					num2 = 8;
					text += "\r\n|";
					goto IL_0076;
					IL_008c:
					num2 = 11;
					if (Operators.CompareString(MyMode, "C", TextCompare: false) == 0)
					{
						goto IL_00b6;
					}
					if (Operators.CompareString(MyMode, "P", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0105;
					IL_0076:
					num2 = 9;
					text += "\r\n --------------------------------------------------------------------------------------*/";
					goto end_IL_0001_3;
					IL_0105:
					num2 = 19;
					text += "cat(\"------------------------------------------------------------------------------------\\n\\n\");";
					goto IL_0114;
					IL_0114:
					num2 = 20;
					text = text + "\r\ncat(\"" + Strings.Replace(MyTitle, "<>", MyData, 1, -1, CompareMethod.Text) + " ... \");";
					goto IL_0137;
					IL_0137:
					num2 = 21;
					text += "\r\ncat(paste(format(Sys.time(), \"%Y-%m-%d %H:%M:%S\"),\"\\n\\n\",sep=\"\"));";
					break;
					IL_00b6:
					num2 = 13;
					text += "#------------------------------------------------------------------------------------";
					goto IL_00c5;
					IL_00c5:
					num2 = 14;
					text += "\r\n#";
					goto IL_00d4;
					IL_00d4:
					num2 = 15;
					text = text + "\r\n# " + MyTitle;
					goto IL_00e4;
					IL_00e4:
					num2 = 16;
					text += "\r\n#";
					goto IL_00f3;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				text += "\r\ncat(\"------------------------------------------------------------------------------------\\n\");";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 468;
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

	public static string Save_JSL_Output(string MyWinObj, string MyExt, string MyFile)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string result = default(string);
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
				case 542:
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
							goto IL_001d;
						case 5:
							goto IL_0026;
						case 6:
							goto IL_003e;
						case 8:
							goto IL_005b;
						case 7:
						case 9:
						case 10:
							goto IL_0075;
						case 11:
							goto IL_008b;
						case 12:
							goto IL_0096;
						case 14:
							goto IL_00f9;
						case 16:
							goto IL_0106;
						case 18:
							goto IL_0113;
						case 20:
							goto IL_0120;
						case 22:
							goto IL_012d;
						case 24:
							goto IL_013a;
						case 13:
						case 15:
						case 17:
						case 19:
						case 21:
						case 23:
						case 25:
						case 26:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0113:
					num2 = 18;
					text = "<< save RTF(::MyOutFile1)";
					break;
					IL_0106:
					num2 = 16;
					text = "<< save MSWord(::MyOutFile1,Native)";
					break;
					IL_0120:
					num2 = 20;
					text = "<< save TEXT(::MyOutFile1)";
					break;
					IL_00f9:
					num2 = 14;
					text = "<< save HTML(::MyOutFile1)";
					break;
					IL_000b:
					num2 = 2;
					text2 = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					text3 = "";
					goto IL_001d;
					IL_001d:
					num2 = 4;
					text = "";
					goto IL_0026;
					IL_0026:
					num2 = 5;
					if (Strings.InStrRev(MyFile, "\\") == 0)
					{
						goto IL_003e;
					}
					goto IL_005b;
					IL_003e:
					num2 = 6;
					MyFile = "::MyOutFile1= ::Default_Dir || \"" + Strings.Trim(MyFile) + "\";";
					goto IL_0075;
					IL_005b:
					num2 = 8;
					MyFile = "::MyOutFile1= \"" + Strings.Trim(MyFile) + "\";";
					goto IL_0075;
					IL_0075:
					num2 = 10;
					text2 = "If (Is Empty(" + MyWinObj + ") == 0, Try( " + MyWinObj;
					goto IL_008b;
					IL_008b:
					num2 = 11;
					text3 = " ;,Print(\"File could not be saved\") ),Print(\"The Window does not exist\" ));";
					goto IL_0096;
					IL_0096:
					num2 = 12;
					switch (MyExt)
					{
					case "HTM":
					case "HTML":
						break;
					case "DOC":
						goto IL_0106;
					case "RTF":
						goto IL_0113;
					case "TXT":
						goto IL_0120;
					case "PNG":
						goto IL_012d;
					default:
						goto IL_013a;
					}
					goto IL_00f9;
					IL_013a:
					num2 = 24;
					text = "<< save Journal(::MyOutFile1)";
					break;
					IL_012d:
					num2 = 22;
					text = "<< save Picture(::MyOutFile1,PNG)";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 26;
				result = Add_JSL_Title("Save the Display Window", "JMP") + "\r\n" + MyFile + "\r\n" + text2 + " " + text + text3;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 542;
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

	public static string Generate_Create_Window_JSL(ref TreeNode MyNode, string MyInData, string MyTitle, string MyWinObj)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string left = default(string);
		string text2 = default(string);
		string result = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string MyChart = default(string);
		string MyChartActual = default(string);
		int num7 = default(int);
		int num8 = default(int);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		int num9 = default(int);
		string text12 = default(string);
		string left2 = default(string);
		string[] MyDisplayArr = default(string[]);
		string[] MyTrueArr = default(string[]);
		string[] array = default(string[]);
		string[] array2 = default(string[]);
		int num10 = default(int);
		string[] array3 = default(string[]);
		int num11 = default(int);
		int num12 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text13;
					string text14;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 3644:
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
								goto IL_0018;
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
								goto IL_004b;
							case 11:
								goto IL_0055;
							case 12:
								goto IL_005f;
							case 13:
								goto IL_0069;
							case 14:
								goto IL_0073;
							case 15:
								goto IL_0079;
							case 16:
								goto IL_007f;
							case 17:
								goto IL_0089;
							case 18:
								goto IL_0093;
							case 19:
								goto IL_009d;
							case 20:
								goto IL_00a7;
							case 21:
								goto IL_00b1;
							case 22:
								goto IL_00b7;
							case 23:
								goto IL_00c1;
							case 24:
								goto IL_00cb;
							case 25:
								goto IL_00d5;
							case 26:
								goto IL_00df;
							case 27:
								goto IL_00ff;
							case 28:
								goto IL_010d;
							case 29:
								goto IL_0120;
							case 30:
								goto IL_0133;
							case 31:
								goto IL_0145;
							case 32:
								goto IL_014e;
							case 33:
								goto IL_0165;
							case 34:
								goto IL_016b;
							case 35:
								goto IL_0174;
							case 36:
								goto IL_0188;
							case 37:
								goto IL_019c;
							case 38:
								goto IL_01ce;
							case 40:
								goto IL_01de;
							case 41:
								goto IL_01fa;
							case 42:
								goto IL_020d;
							case 43:
								goto IL_021b;
							case 44:
								goto IL_0230;
							case 45:
								goto IL_029e;
							case 47:
								goto IL_02ac;
							case 39:
							case 46:
							case 48:
							case 49:
							case 50:
							case 51:
								goto IL_02ba;
							case 52:
								goto IL_02d4;
							case 53:
								goto IL_02ea;
							case 54:
								goto IL_02ff;
							case 55:
								goto IL_0309;
							case 56:
								goto IL_0313;
							case 57:
								goto IL_0329;
							case 58:
								goto IL_033e;
							case 59:
								goto IL_035d;
							case 60:
								goto IL_037a;
							case 61:
								goto IL_038f;
							case 62:
								goto IL_0399;
							case 63:
								goto IL_03c4;
							case 64:
							case 65:
								goto IL_03eb;
							case 66:
								goto IL_03fd;
							case 67:
								goto IL_0403;
							case 68:
								goto IL_041c;
							case 69:
								goto IL_0462;
							case 70:
								goto IL_0481;
							case 71:
								goto IL_048b;
							case 72:
							case 73:
								goto IL_0497;
							case 74:
								goto IL_04a9;
							case 75:
							case 76:
								goto IL_04b1;
							case 77:
								goto IL_04ca;
							case 79:
								goto IL_04e4;
							case 78:
							case 80:
							case 81:
								goto IL_04ef;
							case 82:
								goto IL_0509;
							case 83:
								goto IL_051f;
							case 84:
								goto IL_0529;
							case 85:
								goto IL_0533;
							case 86:
								goto IL_0548;
							case 87:
								goto IL_0567;
							case 88:
								goto IL_0584;
							case 89:
								goto IL_0599;
							case 90:
								goto IL_05a3;
							case 91:
								goto IL_05cd;
							case 92:
							case 93:
								goto IL_05f4;
							case 94:
								goto IL_0606;
							case 95:
							case 96:
								goto IL_060e;
							case 97:
								goto IL_0627;
							case 98:
								goto IL_0656;
							case 99:
								goto IL_0660;
							case 100:
							case 101:
								goto IL_066c;
							case 102:
								goto IL_0672;
							case 103:
								goto IL_068b;
							case 105:
								goto IL_06e0;
							case 104:
							case 106:
							case 107:
								goto IL_0732;
							case 108:
								goto IL_0743;
							case 109:
								goto IL_0749;
							case 110:
								goto IL_074f;
							case 111:
								goto IL_0791;
							case 112:
								goto IL_079e;
							case 113:
								goto IL_07af;
							case 114:
								goto IL_07cb;
							case 115:
								goto IL_07f5;
							case 116:
								goto IL_080c;
							case 117:
								goto IL_0835;
							case 118:
								goto IL_0850;
							case 119:
								goto IL_0861;
							case 120:
								goto IL_087d;
							case 122:
								goto IL_08dc;
							case 121:
							case 123:
							case 124:
								goto IL_0914;
							case 125:
								goto IL_0925;
							case 126:
								goto IL_0936;
							case 128:
								goto IL_094d;
							case 129:
								goto IL_096c;
							case 130:
								goto IL_0999;
							case 131:
								goto IL_09ad;
							case 132:
								goto IL_09c1;
							case 133:
								goto IL_09d5;
							case 134:
								goto IL_0a0d;
							case 135:
								goto IL_0a2c;
							case 137:
								goto IL_0a7c;
							case 136:
							case 138:
							case 139:
								goto IL_0ab7;
							case 140:
								goto IL_0acb;
							case 141:
								goto IL_0adf;
							case 127:
							case 142:
							case 143:
							case 144:
								goto IL_0af6;
							case 145:
								goto IL_0b0a;
							case 146:
								goto IL_0b24;
							case 147:
								goto IL_0b62;
							case 149:
								goto IL_0b79;
							case 150:
								goto IL_0b95;
							case 148:
							case 151:
							case 152:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 153:
							case 154:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0af6:
						num2 = 144;
						text += "\r\nWait( 0.1 );";
						goto IL_0b0a;
						IL_0b0a:
						num2 = 145;
						text = text + "\r\n" + MyWinObj + " << Bring Window To Front;";
						goto IL_0b24;
						IL_0adf:
						num2 = 141;
						text += "\r\ntry(Save Text File (::MyBat,::MyFile99 ),print(\"Error Saving File Command for Email/JMP Integration...\"));";
						goto IL_0af6;
						IL_0b24:
						num2 = 146;
						if (((Operators.CompareString(left, "PPT", TextCompare: false) == 0) | (Operators.CompareString(left, "EMAIL", TextCompare: false) == 0)) & (Operators.CompareString(text2, "", TextCompare: false) != 0))
						{
							goto IL_0b62;
						}
						goto IL_0b79;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num5 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num6 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						text3 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						text4 = "";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						text5 = "";
						goto IL_0038;
						IL_0038:
						num2 = 8;
						text6 = "";
						goto IL_0041;
						IL_0041:
						num2 = 9;
						text7 = "";
						goto IL_004b;
						IL_004b:
						num2 = 10;
						text8 = "";
						goto IL_0055;
						IL_0055:
						num2 = 11;
						text = "";
						goto IL_005f;
						IL_005f:
						num2 = 12;
						MyChart = "";
						goto IL_0069;
						IL_0069:
						num2 = 13;
						MyChartActual = "";
						goto IL_0073;
						IL_0073:
						num2 = 14;
						num7 = 1;
						goto IL_0079;
						IL_0079:
						num2 = 15;
						num8 = 2;
						goto IL_007f;
						IL_007f:
						num2 = 16;
						text9 = "";
						goto IL_0089;
						IL_0089:
						num2 = 17;
						text10 = "";
						goto IL_0093;
						IL_0093:
						num2 = 18;
						left = "";
						goto IL_009d;
						IL_009d:
						num2 = 19;
						text2 = "";
						goto IL_00a7;
						IL_00a7:
						num2 = 20;
						text11 = "";
						goto IL_00b1;
						IL_00b1:
						num2 = 21;
						num9 = 0;
						goto IL_00b7;
						IL_00b7:
						num2 = 22;
						text12 = "";
						goto IL_00c1;
						IL_00c1:
						num2 = 23;
						text13 = "";
						goto IL_00cb;
						IL_00cb:
						num2 = 24;
						text14 = "";
						goto IL_00d5;
						IL_00d5:
						num2 = 25;
						left2 = "0";
						goto IL_00df;
						IL_00df:
						num2 = 26;
						if (Operators.CompareString(Strings.Trim(MyInData), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_00ff;
						IL_00ff:
						num2 = 27;
						Get_Prior_Chart_Table_Filter_Objects(ref MyNode, ref MyChart, ref MyChartActual);
						goto IL_010d;
						IL_010d:
						num2 = 28;
						MyDisplayArr = Strings.Split(MyChart, ",");
						goto IL_0120;
						IL_0120:
						num2 = 29;
						MyTrueArr = Strings.Split(MyChartActual, ",");
						goto IL_0133;
						IL_0133:
						num2 = 30;
						array = Strings.Split(MyInData, "@@@");
						goto IL_0145;
						IL_0145:
						num2 = 31;
						left2 = array[0];
						goto IL_014e;
						IL_014e:
						num2 = 32;
						if (Information.UBound(array) == 4)
						{
							goto IL_0165;
						}
						goto IL_02ba;
						IL_0165:
						num2 = 33;
						num7 = 3;
						goto IL_016b;
						IL_016b:
						num2 = 34;
						num8 = num7 + 1;
						goto IL_0174;
						IL_0174:
						num2 = 35;
						text10 = BuildForm.Replace_Globals(Strings.Trim(array[1]));
						goto IL_0188;
						IL_0188:
						num2 = 36;
						text9 = BuildForm.Replace_Globals(Strings.Trim(array[2]));
						goto IL_019c;
						IL_019c:
						num2 = 37;
						if (LikeOperator.LikeString(Strings.UCase(text10), "EMAIL:*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(text10), "EMAIL-A:*", CompareMethod.Binary))
						{
							goto IL_01ce;
						}
						goto IL_01de;
						IL_0b62:
						num2 = 147;
						text += "\r\nWait (0.1);";
						break;
						IL_01de:
						num2 = 40;
						if (Operators.CompareString(text10, "", TextCompare: false) != 0)
						{
							goto IL_01fa;
						}
						goto IL_02ba;
						IL_01fa:
						num2 = 41;
						num9 = Strings.InStrRev(text10, ".");
						goto IL_020d;
						IL_020d:
						num2 = 42;
						if (num9 != 0)
						{
							goto IL_021b;
						}
						goto IL_0230;
						IL_021b:
						num2 = 43;
						text12 = Strings.UCase(Strings.Mid(text10, num9 + 1));
						goto IL_0230;
						IL_0230:
						num2 = 44;
						if ((Operators.CompareString(text12, "RTF", TextCompare: false) == 0) | (Operators.CompareString(text12, "HTM", TextCompare: false) == 0) | (Operators.CompareString(text12, "HTML", TextCompare: false) == 0) | (Operators.CompareString(text12, "PNG", TextCompare: false) == 0) | (Operators.CompareString(text12, "TXT", TextCompare: false) == 0) | (Operators.CompareString(text12, "JRN", TextCompare: false) == 0))
						{
							goto IL_029e;
						}
						goto IL_02ac;
						IL_029e:
						num2 = 45;
						left = "EXPORT";
						goto IL_02ba;
						IL_02ac:
						num2 = 47;
						left = "PPT";
						goto IL_02ba;
						IL_01ce:
						num2 = 38;
						left = "EMAIL";
						goto IL_02ba;
						IL_02ba:
						num2 = 51;
						if (Information.UBound(array) >= 1)
						{
							goto IL_02d4;
						}
						goto IL_04b1;
						IL_02d4:
						num2 = 52;
						array2 = Strings.Split(array[num7], "|");
						goto IL_02ea;
						IL_02ea:
						num2 = 53;
						num10 = Information.UBound(array2);
						num5 = 0;
						goto IL_04a0;
						IL_04a0:
						if (num5 <= num10)
						{
							goto IL_02ff;
						}
						goto IL_04a9;
						IL_04a9:
						num2 = 74;
						array2 = null;
						goto IL_04b1;
						IL_02ff:
						num2 = 54;
						text3 = "";
						goto IL_0309;
						IL_0309:
						num2 = 55;
						text4 = "";
						goto IL_0313;
						IL_0313:
						num2 = 56;
						array3 = Strings.Split(array2[num5], ";");
						goto IL_0329;
						IL_0329:
						num2 = 57;
						num11 = Information.UBound(array3);
						num6 = 0;
						goto IL_03f4;
						IL_03f4:
						if (num6 <= num11)
						{
							goto IL_033e;
						}
						goto IL_03fd;
						IL_03fd:
						num2 = 66;
						array3 = null;
						goto IL_0403;
						IL_0403:
						num2 = 67;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_041c;
						}
						goto IL_0497;
						IL_041c:
						num2 = 68;
						text = text + text5 + "::HL" + Strings.Trim(Conversions.ToString(num5)) + "= HLISTBOX(" + text3 + ");";
						goto IL_0462;
						IL_0462:
						num2 = 69;
						text7 = text7 + text6 + "::HL" + Strings.Trim(Conversions.ToString(num5));
						goto IL_0481;
						IL_0481:
						num2 = 70;
						text5 = "\r\n";
						goto IL_048b;
						IL_048b:
						num2 = 71;
						text6 = ",";
						goto IL_0497;
						IL_0497:
						num2 = 73;
						num5++;
						goto IL_04a0;
						IL_033e:
						num2 = 58;
						if (Operators.CompareString(array3[num6], "", TextCompare: false) != 0)
						{
							goto IL_035d;
						}
						goto IL_03eb;
						IL_035d:
						num2 = 59;
						text11 = Strings.Trim(Get_JSL_Display_Label(Strings.Trim(array3[num6]), ref MyTrueArr, ref MyDisplayArr));
						goto IL_037a;
						IL_037a:
						num2 = 60;
						text3 = text3 + text4 + "::" + text11;
						goto IL_038f;
						IL_038f:
						num2 = 61;
						text4 = ",";
						goto IL_0399;
						IL_0399:
						num2 = 62;
						if (Operators.CompareString(left, "PPT", TextCompare: false) == 0 || Operators.CompareString(left, "EMAIL", TextCompare: false) == 0)
						{
							goto IL_03c4;
						}
						goto IL_03eb;
						IL_0b79:
						num2 = 149;
						if (Operators.CompareString(left, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_0b95;
						IL_03c4:
						num2 = 63;
						text2 = text2 + ";;;;;" + Strings.Replace(text11, " ", "", 1, -1, CompareMethod.Text);
						goto IL_03eb;
						IL_03eb:
						num2 = 65;
						num6++;
						goto IL_03f4;
						IL_04b1:
						num2 = 76;
						if (Operators.CompareString(text7, "", TextCompare: false) != 0)
						{
							goto IL_04ca;
						}
						goto IL_04e4;
						IL_04ca:
						num2 = 77;
						text7 = "VListBox(" + text7 + ")";
						goto IL_04ef;
						IL_04e4:
						num2 = 79;
						text7 = "";
						goto IL_04ef;
						IL_04ef:
						num2 = 81;
						if (Information.UBound(array) >= 2)
						{
							goto IL_0509;
						}
						goto IL_060e;
						IL_0509:
						num2 = 82;
						array2 = Strings.Split(array[num8], "|");
						goto IL_051f;
						IL_051f:
						num2 = 83;
						text3 = "";
						goto IL_0529;
						IL_0529:
						num2 = 84;
						text4 = "";
						goto IL_0533;
						IL_0533:
						num2 = 85;
						num12 = Information.UBound(array2);
						num5 = 0;
						goto IL_05fd;
						IL_05fd:
						if (num5 <= num12)
						{
							goto IL_0548;
						}
						goto IL_0606;
						IL_0606:
						num2 = 94;
						array2 = null;
						goto IL_060e;
						IL_0548:
						num2 = 86;
						if (Operators.CompareString(array2[num5], "", TextCompare: false) != 0)
						{
							goto IL_0567;
						}
						goto IL_05f4;
						IL_0567:
						num2 = 87;
						text11 = Strings.Trim(Get_JSL_Display_Label(Strings.Trim(array2[num5]), ref MyTrueArr, ref MyDisplayArr));
						goto IL_0584;
						IL_0584:
						num2 = 88;
						text3 = text3 + text4 + "::" + text11;
						goto IL_0599;
						IL_0599:
						num2 = 89;
						text4 = ",";
						goto IL_05a3;
						IL_05a3:
						num2 = 90;
						if ((Operators.CompareString(left, "PPT", TextCompare: false) == 0) | (Operators.CompareString(left, "EMAIL", TextCompare: false) == 0))
						{
							goto IL_05cd;
						}
						goto IL_05f4;
						IL_05cd:
						num2 = 91;
						text2 = text2 + ";;;;;" + Strings.Replace(text11, " ", "", 1, -1, CompareMethod.Text);
						goto IL_05f4;
						IL_05f4:
						num2 = 93;
						num5++;
						goto IL_05fd;
						IL_060e:
						num2 = 96;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_0627;
						}
						goto IL_066c;
						IL_0627:
						num2 = 97;
						text = text + text5 + "::VLFilter= VLISTBOX(" + text3 + ");";
						goto IL_0656;
						IL_0656:
						num2 = 98;
						text5 = "\r\n";
						goto IL_0660;
						IL_0660:
						num2 = 99;
						text8 = "::VLFilter";
						goto IL_066c;
						IL_066c:
						num2 = 101;
						array = null;
						goto IL_0672;
						IL_0672:
						num2 = 102;
						if (Operators.CompareString(left2, "0", TextCompare: false) == 0)
						{
							goto IL_068b;
						}
						goto IL_06e0;
						IL_068b:
						num2 = 103;
						text = text + "\r\n" + MyWinObj + " = New Window( \"" + MyTitle + "\", HListBox(" + text7 + "," + text8 + "));";
						goto IL_0732;
						IL_06e0:
						num2 = 105;
						text = text + "\r\n" + MyWinObj + " = New Window( \"" + MyTitle + "\", HListBox(" + text8 + "," + text7 + "));";
						goto IL_0732;
						IL_0732:
						num2 = 107;
						text += "\r\nWait( 0.1 );";
						goto IL_0743;
						IL_0743:
						num2 = 108;
						MyDisplayArr = null;
						goto IL_0749;
						IL_0749:
						num2 = 109;
						MyTrueArr = null;
						goto IL_074f;
						IL_074f:
						num2 = 110;
						if (((Operators.CompareString(left, "PPT", TextCompare: false) == 0) | (Operators.CompareString(left, "EMAIL", TextCompare: false) == 0)) && Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto IL_0791;
						}
						goto IL_0af6;
						IL_0b95:
						num2 = 150;
						text = text + "\r\n" + Save_JSL_Output(MyWinObj, text12, text10);
						break;
						IL_0791:
						num2 = 111;
						text2 = Strings.Mid(text2, 6);
						goto IL_079e;
						IL_079e:
						num2 = 112;
						text += "\r\n::MyBat= ::Default_Dir || \"spf_signal_test_$instance$.bat\";";
						goto IL_07af;
						IL_07af:
						num2 = 113;
						if (Operators.CompareString(left, "PPT", TextCompare: false) == 0)
						{
							goto IL_07cb;
						}
						goto IL_094d;
						IL_07cb:
						num2 = 114;
						text = text + "\r\n\r\n" + Add_JSL_Title("Load Images to PowerPoint", "JMP");
						goto IL_07f5;
						IL_07f5:
						num2 = 115;
						text = text + "\r\n::MyJnl99 = " + MyWinObj + ";";
						goto IL_080c;
						IL_080c:
						num2 = 116;
						text = text + "\r\n::MyJnl99str = \"_" + Strings.Replace(MyWinObj, "::", "", 1, -1, CompareMethod.Text) + "\";";
						goto IL_0835;
						IL_0835:
						num2 = 117;
						text = text + "\r\n" + BuildSQL.Write_VA_Lib("JSL_lib_4.lib");
						goto IL_0850;
						IL_0850:
						num2 = 118;
						text += "\r\n\r\n//Create and Run BAT File to add images";
						goto IL_0861;
						IL_0861:
						num2 = 119;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
						{
							goto IL_087d;
						}
						goto IL_08dc;
						IL_087d:
						num2 = 120;
						text = text + "\r\n::MyFile = \"\\!\"$py$exe$dir$\\!\" \\!\"\" || ::Default_Dir || \"DOJMP.py\\!\" -m \\!\"PPT\\!\"  -s \\!\"" + Strings.Replace(text9, "\\", "/", 1, 1) + "\\!\" -o \\!\"" + Strings.Replace(text10, "\\", "/", 1, 1) + "\\!\"  -i \\!\"\" || substitute(::MyFolder,\"\\!\\\",\"/\") || \"\\!\"\";";
						goto IL_0914;
						IL_08dc:
						num2 = 122;
						text = text + "\r\n::MyFile = \"va \\!\"\" || ::Default_Dir || \"DOJMP.va\\!\" \\!\"PPT\\!\"  \\!\"" + text9 + "\\!\" \\!\"" + text10 + "\\!\"  \" || \"\\!\"\" || ::MyFolder || \"\\!\"\";";
						goto IL_0914;
						IL_0914:
						num2 = 124;
						text += "\r\ntry(::MyFile99=load text file (::MyBat),::MyFile99=\"\");";
						goto IL_0925;
						IL_0925:
						num2 = 125;
						text += "\r\nif (::MyFile99 == \"\",::MyFile99=MyFile,::MyFile99 = concat(::MyFile99,\"\\!N\",MyFile));";
						goto IL_0936;
						IL_0936:
						num2 = 126;
						text += "\r\ntry(Save Text File (::MyBat,::MyFile99 ),print(\"Error Saving File Command for Email/JMP Integration...\"));";
						goto IL_0af6;
						IL_094d:
						num2 = 128;
						if (Operators.CompareString(left, "EMAIL", TextCompare: false) == 0)
						{
							goto IL_096c;
						}
						goto IL_0af6;
						IL_096c:
						num2 = 129;
						text = text + "\r\n\r\n" + Add_JSL_Title("Email Window", "JMP");
						goto IL_0999;
						IL_0999:
						num2 = 130;
						text += "\r\n::MyFolder= ::Default_Dir || \"gfx\\\";";
						goto IL_09ad;
						IL_09ad:
						num2 = 131;
						text += "\r\n\r\n//Save Window and Run Email job";
						goto IL_09c1;
						IL_09c1:
						num2 = 132;
						text += "\r\n::MyOutFile1= ::Default_Dir || \"Test_$instance$.htm\";";
						goto IL_09d5;
						IL_09d5:
						num2 = 133;
						text = text + "\r\nIf (Is Empty(" + MyWinObj + ") == 0, Try( " + MyWinObj + " << save HTML(::MyOutFile1) ;,Print(\"File could not be saved\") ),Print(\"The Window does not exist\" ));";
						goto IL_0a0d;
						IL_0a0d:
						num2 = 134;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
						{
							goto IL_0a2c;
						}
						goto IL_0a7c;
						IL_0a2c:
						num2 = 135;
						text = text + "\r\n::MyFile = \"\\!\"$py$exe$dir$\\!\" \\!\"\" || ::Default_Dir || \"DOJMP.py\\!\" -m EMAIL  -s Test_$instance$.htm -o \\!\"" + Strings.Replace(text10, "\\", "/", 1, 1) + "\\!\"  -i  \\!\"\" || substitute(::MyFolder,\"\\!\\\",\"/\") || \"\\!\"  -e  \\!\"" + text9 + "\\!\" -t <<<spf-email-type>>>\";";
						goto IL_0ab7;
						IL_0a7c:
						num2 = 137;
						text = text + "\r\n::MyFile = \"va \\!\"\" || ::Default_Dir || \"DOJMP.va\\!\" \\!\"EMAIL\\!\"  \\!\"Test_$instance$.htm\\!\" \\!\"" + text10 + "\\!\"  \\!\"\" || ::MyFolder || \"\\!\"  \\!\"" + text9 + "\\!\" \\!\"<<<spf-email-type>>>\\!\"\";";
						goto IL_0ab7;
						IL_0ab7:
						num2 = 139;
						text += "\r\ntry(::MyFile99=load text file (::MyBat),::MyFile99=\"\");";
						goto IL_0acb;
						IL_0acb:
						num2 = 140;
						text += "\r\nif (::MyFile99 == \"\",::MyFile99=MyFile,::MyFile99 = concat(::MyFile99,\"\\!N\",MyFile));";
						goto IL_0adf;
						end_IL_0001_2:
						break;
					}
					num2 = 152;
					result = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3644;
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

	public static string Get_HTML_Label(string MyData)
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
				switch (try0001_dispatch)
				{
				default:
				{
					text = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "FrmText - Form_Load";
					string text2 = "Center";
					string text3 = "white";
					string text4 = "black";
					string text5 = "N";
					string text6 = "N";
					string text7 = "N";
					string text8 = "verdana";
					string text9 = "10";
					string text10 = "";
					double num3 = 0.0;
					MyData = Strings.Trim(MyData);
					if (Operators.CompareString(MyData, "", TextCompare: false) != 0)
					{
						string[] array = Strings.Split(MyData, "<:>");
						text3 = array[0];
						text4 = array[1];
						text5 = Strings.UCase(array[2]);
						text6 = Strings.UCase(array[3]);
						text7 = Strings.UCase(array[4]);
						text8 = array[5];
						text9 = array[6];
						text2 = Strings.LCase(array[7]);
						text10 = array[8];
						text10 = General_Procedures.Quote_CRLF_Replace("DQ", text10);
						array = null;
						num3 = Conversions.ToDouble(text9) / 3.0;
						text = "<p style=\"text-align: " + text2 + "\"";
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							text = text + " style=\"background-color: " + text3 + "\"";
						}
						text += ">";
						if (Operators.CompareString(text5, "Y", TextCompare: false) == 0)
						{
							text += "<b>";
						}
						if (Operators.CompareString(text7, "Y", TextCompare: false) == 0)
						{
							text += "<u>";
						}
						if (Operators.CompareString(text6, "Y", TextCompare: false) == 0)
						{
							text += "<i>";
						}
						text = text + "<font face=\"" + text8 + "\" size=\"" + Conversions.ToString(num3) + "\" color=\"" + text4 + "\">";
						text = text + text10 + "</font>";
						if (Operators.CompareString(text6, "Y", TextCompare: false) == 0)
						{
							text += "</i>";
						}
						if (Operators.CompareString(text7, "Y", TextCompare: false) == 0)
						{
							text += "</u>";
						}
						if (Operators.CompareString(text5, "Y", TextCompare: false) == 0)
						{
							text += "</b>";
						}
					}
					goto end_IL_0001;
				}
				case 655:
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
				try0001_dispatch = 655;
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
		return text;
	}

	public static string Generate_Create_Window_Script(ref TreeNode MyNode, string MyInData, string MyTitle, string MyWinObj)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		string text = default(string);
		string[] MyDisplayArr = default(string[]);
		string[] MyTrueArr = default(string[]);
		int num5 = default(int);
		int num6 = default(int);
		string text2 = default(string);
		string left = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string str = default(string);
		string text6 = default(string);
		int num7 = default(int);
		string MyChart = default(string);
		string MyChartActual = default(string);
		int num8 = default(int);
		string MyCS = default(string);
		string MyLabel = default(string);
		int num9 = default(int);
		int num10 = default(int);
		string text8 = default(string);
		string[] array = default(string[]);
		string[] array2 = default(string[]);
		int num11 = default(int);
		string[] array3 = default(string[]);
		int num12 = default(int);
		int num13 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 5681:
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
								goto IL_0018;
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
								goto IL_004b;
							case 11:
								goto IL_0055;
							case 12:
								goto IL_005f;
							case 13:
								goto IL_0069;
							case 14:
								goto IL_006f;
							case 15:
								goto IL_0079;
							case 16:
								goto IL_0083;
							case 17:
								goto IL_0089;
							case 18:
								goto IL_0093;
							case 19:
								goto IL_009d;
							case 20:
								goto IL_00a7;
							case 21:
								goto IL_00ad;
							case 22:
								goto IL_00b3;
							case 23:
								goto IL_00bd;
							case 24:
								goto IL_00dd;
							case 25:
								goto IL_00eb;
							case 26:
								goto IL_00fe;
							case 27:
								goto IL_0111;
							case 28:
								goto IL_0123;
							case 29:
								goto IL_012c;
							case 30:
								goto IL_0136;
							case 31:
								goto IL_0140;
							case 32:
								goto IL_014a;
							case 33:
								goto IL_0172;
							case 34:
								goto IL_018d;
							case 35:
								goto IL_01a8;
							case 36:
								goto IL_01be;
							case 37:
								goto IL_01d0;
							case 38:
								goto IL_01e0;
							case 39:
								goto IL_01f3;
							case 40:
							case 41:
								goto IL_0206;
							case 42:
								goto IL_023f;
							case 44:
								goto IL_0260;
							case 45:
								goto IL_0277;
							case 43:
							case 46:
							case 47:
								goto IL_0298;
							case 48:
								goto IL_02af;
							case 49:
								goto IL_02cf;
							case 50:
								goto IL_02e6;
							case 51:
								goto IL_02f9;
							case 52:
								goto IL_0323;
							case 53:
								goto IL_0331;
							case 54:
								goto IL_0344;
							case 55:
								goto IL_034a;
							case 56:
								goto IL_0350;
							case 57:
							case 58:
							case 59:
								goto IL_035a;
							case 60:
								goto IL_0371;
							case 61:
								goto IL_0384;
							case 62:
								goto IL_03ae;
							case 63:
								goto IL_03bb;
							case 64:
								goto IL_03ce;
							case 65:
								goto IL_03d4;
							case 66:
								goto IL_03da;
							case 67:
							case 68:
							case 69:
								goto IL_03e4;
							case 70:
								goto IL_03fe;
							case 71:
								goto IL_0411;
							case 72:
								goto IL_043b;
							case 73:
								goto IL_0457;
							case 74:
								goto IL_046a;
							case 75:
								goto IL_0470;
							case 76:
								goto IL_0476;
							case 77:
							case 78:
							case 79:
								goto IL_0480;
							case 80:
								goto IL_0497;
							case 81:
								goto IL_04a5;
							case 82:
								goto IL_04cf;
							case 83:
								goto IL_04eb;
							case 84:
								goto IL_04fe;
							case 85:
								goto IL_0504;
							case 86:
								goto IL_050a;
							case 87:
							case 88:
							case 89:
								goto IL_0514;
							case 90:
								goto IL_052b;
							case 91:
								goto IL_0539;
							case 92:
								goto IL_0563;
							case 93:
								goto IL_057f;
							case 94:
								goto IL_0592;
							case 95:
								goto IL_0598;
							case 96:
								goto IL_059e;
							case 97:
							case 98:
							case 99:
								goto IL_05a8;
							case 100:
								goto IL_05bf;
							case 101:
								goto IL_05cd;
							case 102:
								goto IL_05f7;
							case 103:
								goto IL_0613;
							case 104:
								goto IL_0626;
							case 105:
								goto IL_062d;
							case 106:
								goto IL_0633;
							case 107:
							case 108:
							case 109:
								goto IL_063e;
							case 110:
								goto IL_0656;
							case 111:
							case 112:
								goto IL_066a;
							case 113:
								goto IL_0685;
							case 114:
								goto IL_069b;
							case 115:
								goto IL_06b0;
							case 116:
								goto IL_06c1;
							case 117:
								goto IL_06cb;
							case 118:
								goto IL_06e1;
							case 119:
								goto IL_06f6;
							case 120:
								goto IL_0715;
							case 121:
								goto IL_0724;
							case 122:
								goto IL_072e;
							case 123:
								goto IL_0742;
							case 124:
								goto IL_075d;
							case 125:
								goto IL_0770;
							case 126:
								goto IL_0786;
							case 128:
								goto IL_07ba;
							case 129:
								goto IL_07d8;
							case 130:
								goto IL_07ee;
							case 131:
								goto IL_0807;
							case 133:
								goto IL_082f;
							case 134:
								goto IL_084d;
							case 135:
								goto IL_0864;
							case 136:
								goto IL_087f;
							case 138:
								goto IL_08a7;
							case 139:
								goto IL_08d9;
							case 140:
								goto IL_08e9;
							case 141:
								goto IL_0904;
							case 142:
								goto IL_0915;
							case 143:
								goto IL_092e;
							case 145:
								goto IL_094d;
							case 146:
								goto IL_09a4;
							case 147:
								goto IL_09c1;
							case 149:
								goto IL_09e0;
							case 150:
								goto IL_0a11;
							case 151:
								goto IL_0a2e;
							case 153:
								goto IL_0a4d;
							case 154:
								goto IL_0a7e;
							case 155:
								goto IL_0a9b;
							case 157:
								goto IL_0aba;
							case 158:
								goto IL_0aeb;
							case 160:
								goto IL_0b23;
							case 161:
								goto IL_0b55;
							case 163:
								goto IL_0b78;
							case 127:
							case 132:
							case 137:
							case 144:
							case 148:
							case 152:
							case 156:
							case 159:
							case 162:
							case 164:
							case 165:
								goto IL_0b92;
							case 166:
								goto IL_0bae;
							case 168:
								goto IL_0bcb;
							case 167:
							case 169:
							case 170:
								goto IL_0bd9;
							case 171:
							case 172:
								goto IL_0c15;
							case 173:
								goto IL_0c2a;
							case 174:
								goto IL_0c33;
							case 175:
								goto IL_0c47;
							case 176:
								goto IL_0c5c;
							case 177:
							case 178:
								goto IL_0c67;
							case 179:
								goto IL_0c7b;
							case 180:
								goto IL_0c99;
							case 181:
								goto IL_0cb4;
							case 182:
								goto IL_0cc1;
							case 183:
								goto IL_0cd9;
							case 184:
								goto IL_0ce6;
							case 185:
								goto IL_0d08;
							case 186:
								goto IL_0d1a;
							case 187:
								goto IL_0d27;
							case 188:
								goto IL_0d3e;
							case 189:
								goto IL_0d5f;
							case 190:
								goto IL_0d75;
							case 191:
								goto IL_0d8e;
							case 192:
								goto IL_0daa;
							case 193:
								goto IL_0dc3;
							case 195:
								goto IL_0df0;
							case 196:
								goto IL_0e11;
							case 197:
								goto IL_0e27;
							case 198:
								goto IL_0e40;
							case 199:
								goto IL_0e5c;
							case 200:
								goto IL_0e75;
							case 202:
								goto IL_0e9d;
							case 203:
								goto IL_0ebe;
							case 204:
								goto IL_0ed5;
							case 205:
								goto IL_0ef0;
							case 206:
								goto IL_0f0c;
							case 207:
								goto IL_0f25;
							case 209:
								goto IL_0f4d;
							case 210:
								goto IL_0fa4;
							case 212:
								goto IL_0fd6;
							case 213:
								goto IL_1007;
							case 215:
								goto IL_1039;
							case 216:
								goto IL_106a;
							case 218:
								goto IL_109c;
							case 219:
								goto IL_10cd;
							case 221:
								goto IL_1106;
							case 194:
							case 201:
							case 208:
							case 211:
							case 214:
							case 217:
							case 220:
							case 222:
							case 223:
							case 224:
								goto IL_1122;
							case 225:
								goto IL_115c;
							case 226:
								goto IL_1171;
							case 227:
							case 228:
								goto IL_117c;
							case 229:
								goto IL_1190;
							case 230:
								goto IL_1199;
							case 231:
								goto IL_11b5;
							case 233:
								goto IL_11eb;
							case 232:
							case 234:
							case 235:
								goto IL_121e;
							case 236:
								goto IL_1232;
							case 237:
								goto IL_123b;
							case 238:
								goto IL_1244;
							case 239:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 240:
							case 241:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_1232:
						num2 = 236;
						result = text;
						goto IL_123b;
						IL_123b:
						num2 = 237;
						MyDisplayArr = null;
						goto IL_1244;
						IL_121e:
						num2 = 235;
						text += "\r\n</td></tr></table>";
						goto IL_1232;
						IL_1244:
						num2 = 238;
						MyTrueArr = null;
						break;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num5 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num6 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						text2 = "";
						goto IL_0026;
						IL_0026:
						num2 = 6;
						left = "0";
						goto IL_002f;
						IL_002f:
						num2 = 7;
						text = "";
						goto IL_0038;
						IL_0038:
						num2 = 8;
						text3 = "";
						goto IL_0041;
						IL_0041:
						num2 = 9;
						text4 = "";
						goto IL_004b;
						IL_004b:
						num2 = 10;
						text5 = "";
						goto IL_0055;
						IL_0055:
						num2 = 11;
						str = "";
						goto IL_005f;
						IL_005f:
						num2 = 12;
						text6 = "";
						goto IL_0069;
						IL_0069:
						num2 = 13;
						num7 = 3;
						goto IL_006f;
						IL_006f:
						num2 = 14;
						MyChart = "";
						goto IL_0079;
						IL_0079:
						num2 = 15;
						MyChartActual = "";
						goto IL_0083;
						IL_0083:
						num2 = 16;
						num8 = 0;
						goto IL_0089;
						IL_0089:
						num2 = 17;
						MyCS = "";
						goto IL_0093;
						IL_0093:
						num2 = 18;
						MyLabel = "";
						goto IL_009d;
						IL_009d:
						num2 = 19;
						text7 = "";
						goto IL_00a7;
						IL_00a7:
						num2 = 20;
						num9 = 3;
						goto IL_00ad;
						IL_00ad:
						num2 = 21;
						num10 = 2;
						goto IL_00b3;
						IL_00b3:
						num2 = 22;
						text8 = "";
						goto IL_00bd;
						IL_00bd:
						num2 = 23;
						if (Operators.CompareString(Strings.Trim(MyInData), "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_00dd;
						IL_00dd:
						num2 = 24;
						Get_Prior_Chart_Table_Filter_Objects(ref MyNode, ref MyChart, ref MyChartActual);
						goto IL_00eb;
						IL_00eb:
						num2 = 25;
						MyDisplayArr = Strings.Split(MyChart, ",");
						goto IL_00fe;
						IL_00fe:
						num2 = 26;
						MyTrueArr = Strings.Split(MyChartActual, ",");
						goto IL_0111;
						IL_0111:
						num2 = 27;
						array = Strings.Split(MyInData, "@@@");
						goto IL_0123;
						IL_0123:
						num2 = 28;
						left = array[0];
						goto IL_012c;
						IL_012c:
						num2 = 29;
						text = "<table class=\"tblout\"><tr class=\"tblout\"><td class=\"tblout\" valign=\"top\">";
						goto IL_0136;
						IL_0136:
						num2 = 30;
						text3 = "<table class=\"tblout\">";
						goto IL_0140;
						IL_0140:
						num2 = 31;
						text4 = "<table class=\"tblout\">";
						goto IL_014a;
						IL_014a:
						num2 = 32;
						if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode.Parent.Tag, "SCRIPT:*:BUTTON", CompareMethod.Binary)))
						{
							goto IL_0172;
						}
						goto IL_0260;
						IL_0172:
						num2 = 33;
						text5 = Conversions.ToString(MyNode.Parent.Parent.Tag);
						goto IL_018d;
						IL_018d:
						num2 = 34;
						str = General_Procedures.Get_Node_Value(MyNode.Parent.Parent.Text);
						goto IL_01a8;
						IL_01a8:
						num2 = 35;
						if (LikeOperator.LikeString(text5, "*:*.BTN", CompareMethod.Binary))
						{
							goto IL_01be;
						}
						goto IL_0206;
						IL_01be:
						num2 = 36;
						num8 = Strings.InStr(text5, ":");
						goto IL_01d0;
						IL_01d0:
						num2 = 37;
						text5 = Strings.Mid(text5, num8 + 1);
						goto IL_01e0;
						IL_01e0:
						num2 = 38;
						num8 = Strings.InStrRev(text5, ".BTN");
						goto IL_01f3;
						IL_01f3:
						num2 = 39;
						text5 = Strings.Mid(text5, 1, num8 - 1);
						goto IL_0206;
						IL_0206:
						num2 = 41;
						text = text + "\r\n:MODE:" + Strings.Trim(text5) + ":->" + Strings.Trim(str);
						goto IL_023f;
						IL_023f:
						num2 = 42;
						text = text + "\r\n:FILE:" + Strings.Trim(text5) + ".htm";
						goto IL_0298;
						IL_0260:
						num2 = 44;
						if (Information.UBound(array) >= 1)
						{
							goto IL_0277;
						}
						goto IL_0298;
						IL_0277:
						num2 = 45;
						text = text + "\r\n:FILE:" + BuildForm.Replace_Globals(Strings.Trim(array[1]));
						goto IL_0298;
						IL_0298:
						num2 = 47;
						if (Information.UBound(array) >= 2)
						{
							goto IL_02af;
						}
						goto IL_02cf;
						IL_02af:
						num2 = 48;
						text = text + "\r\n:CSS:" + BuildForm.Replace_Globals(Strings.Trim(array[2]));
						goto IL_02cf;
						IL_02cf:
						num2 = 49;
						if (Information.UBound(array) >= 3)
						{
							goto IL_02e6;
						}
						goto IL_035a;
						IL_02e6:
						num2 = 50;
						text8 = Strings.UCase(Strings.Trim(array[3]));
						goto IL_02f9;
						IL_02f9:
						num2 = 51;
						if ((Operators.CompareString(text8, "E->Y", TextCompare: false) == 0) | (Operators.CompareString(text8, "E->N", TextCompare: false) == 0))
						{
							goto IL_0323;
						}
						goto IL_035a;
						IL_0323:
						num2 = 52;
						text8 = Strings.Mid(text8, 4, 1);
						goto IL_0331;
						IL_0331:
						num2 = 53;
						text = text + "\r\n:CSSEMBED:" + text8;
						goto IL_0344;
						IL_0344:
						num2 = 54;
						num9 = 4;
						goto IL_034a;
						IL_034a:
						num2 = 55;
						num10 = 3;
						goto IL_0350;
						IL_0350:
						num2 = 56;
						num7 = 4;
						goto IL_035a;
						IL_035a:
						num2 = 59;
						if (Information.UBound(array) >= 4)
						{
							goto IL_0371;
						}
						goto IL_03e4;
						IL_0371:
						num2 = 60;
						text8 = Strings.UCase(Strings.Trim(array[4]));
						goto IL_0384;
						IL_0384:
						num2 = 61;
						if (Operators.CompareString(Strings.Mid(text8 + "    ", 1, 4), "RR->", TextCompare: false) == 0)
						{
							goto IL_03ae;
						}
						goto IL_03e4;
						IL_03ae:
						num2 = 62;
						text8 = Strings.Mid(text8, 5);
						goto IL_03bb;
						IL_03bb:
						num2 = 63;
						text = text + "\r\n:RR:" + text8;
						goto IL_03ce;
						IL_03ce:
						num2 = 64;
						num9 = 5;
						goto IL_03d4;
						IL_03d4:
						num2 = 65;
						num10 = 4;
						goto IL_03da;
						IL_03da:
						num2 = 66;
						num7 = 5;
						goto IL_03e4;
						IL_03e4:
						num2 = 69;
						if (Information.UBound(array) >= 5)
						{
							goto IL_03fe;
						}
						goto IL_0480;
						IL_03fe:
						num2 = 70;
						text8 = Strings.UCase(Strings.Trim(array[5]));
						goto IL_0411;
						IL_0411:
						num2 = 71;
						if (Operators.CompareString(Strings.Mid(text8 + "   ", 1, 3), "B->", TextCompare: false) == 0)
						{
							goto IL_043b;
						}
						goto IL_0480;
						IL_043b:
						num2 = 72;
						text8 = Strings.Trim(Strings.Mid(text8 + " ", 4));
						goto IL_0457;
						IL_0457:
						num2 = 73;
						text = text + "\r\n:B:" + text8;
						goto IL_046a;
						IL_046a:
						num2 = 74;
						num9 = 6;
						goto IL_0470;
						IL_0470:
						num2 = 75;
						num10 = 5;
						goto IL_0476;
						IL_0476:
						num2 = 76;
						num7 = 6;
						goto IL_0480;
						IL_0480:
						num2 = 79;
						if (Information.UBound(array) >= 6)
						{
							goto IL_0497;
						}
						goto IL_0514;
						IL_0497:
						num2 = 80;
						text8 = Strings.Trim(array[6]);
						goto IL_04a5;
						IL_04a5:
						num2 = 81;
						if (Operators.CompareString(Strings.Mid(text8 + "      ", 1, 6), "EM-A->", TextCompare: false) == 0)
						{
							goto IL_04cf;
						}
						goto IL_0514;
						IL_04cf:
						num2 = 82;
						text8 = Strings.Trim(Strings.Mid(text8 + " ", 7));
						goto IL_04eb;
						IL_04eb:
						num2 = 83;
						text = text + "\r\n:EM-A:" + text8;
						goto IL_04fe;
						IL_04fe:
						num2 = 84;
						num9 = 7;
						goto IL_0504;
						IL_0504:
						num2 = 85;
						num10 = 6;
						goto IL_050a;
						IL_050a:
						num2 = 86;
						num7 = 7;
						goto IL_0514;
						IL_0514:
						num2 = 89;
						if (Information.UBound(array) >= 7)
						{
							goto IL_052b;
						}
						goto IL_05a8;
						IL_052b:
						num2 = 90;
						text8 = Strings.Trim(array[7]);
						goto IL_0539;
						IL_0539:
						num2 = 91;
						if (Operators.CompareString(Strings.Mid(text8 + "      ", 1, 6), "EM-S->", TextCompare: false) == 0)
						{
							goto IL_0563;
						}
						goto IL_05a8;
						IL_0563:
						num2 = 92;
						text8 = Strings.Trim(Strings.Mid(text8 + " ", 7));
						goto IL_057f;
						IL_057f:
						num2 = 93;
						text = text + "\r\n:EM-S:" + text8;
						goto IL_0592;
						IL_0592:
						num2 = 94;
						num9 = 8;
						goto IL_0598;
						IL_0598:
						num2 = 95;
						num10 = 7;
						goto IL_059e;
						IL_059e:
						num2 = 96;
						num7 = 8;
						goto IL_05a8;
						IL_05a8:
						num2 = 99;
						if (Information.UBound(array) >= 8)
						{
							goto IL_05bf;
						}
						goto IL_063e;
						IL_05bf:
						num2 = 100;
						text8 = Strings.Trim(array[8]);
						goto IL_05cd;
						IL_05cd:
						num2 = 101;
						if (Operators.CompareString(Strings.Mid(text8 + "     ", 1, 5), "SEC->", TextCompare: false) == 0)
						{
							goto IL_05f7;
						}
						goto IL_063e;
						IL_05f7:
						num2 = 102;
						text8 = Strings.Trim(Strings.Mid(text8 + " ", 6));
						goto IL_0613;
						IL_0613:
						num2 = 103;
						text = text + "\r\n:SEC:" + text8;
						goto IL_0626;
						IL_0626:
						num2 = 104;
						num9 = 9;
						goto IL_062d;
						IL_062d:
						num2 = 105;
						num10 = 8;
						goto IL_0633;
						IL_0633:
						num2 = 106;
						num7 = 9;
						goto IL_063e;
						IL_063e:
						num2 = 109;
						if (Operators.CompareString(MyTitle, "Window Title", TextCompare: false) != 0)
						{
							goto IL_0656;
						}
						goto IL_066a;
						IL_0656:
						num2 = 110;
						text = text + "\r\n:TITLE:" + MyTitle;
						goto IL_066a;
						IL_066a:
						num2 = 112;
						if (Information.UBound(array) >= num9)
						{
							goto IL_0685;
						}
						goto IL_0c67;
						IL_0685:
						num2 = 113;
						array2 = Strings.Split(array[num7], "|");
						goto IL_069b;
						IL_069b:
						num2 = 114;
						num11 = Information.UBound(array2);
						num5 = 0;
						goto IL_0c53;
						IL_0c53:
						if (num5 <= num11)
						{
							goto IL_06b0;
						}
						goto IL_0c5c;
						IL_0c5c:
						num2 = 176;
						array2 = null;
						goto IL_0c67;
						IL_06b0:
						num2 = 115;
						text3 += "\r\n<tr class=\"tblout\">";
						goto IL_06c1;
						IL_06c1:
						num2 = 116;
						text2 = "";
						goto IL_06cb;
						IL_06cb:
						num2 = 117;
						array3 = Strings.Split(array2[num5], ";");
						goto IL_06e1;
						IL_06e1:
						num2 = 118;
						num12 = Information.UBound(array3);
						num6 = 0;
						goto IL_0c21;
						IL_0c21:
						if (num6 <= num12)
						{
							goto IL_06f6;
						}
						goto IL_0c2a;
						IL_0c2a:
						num2 = 173;
						array3 = null;
						goto IL_0c33;
						IL_0c33:
						num2 = 174;
						text3 += "\r\n</tr>";
						goto IL_0c47;
						IL_0c47:
						num2 = 175;
						num5++;
						goto IL_0c53;
						IL_06f6:
						num2 = 119;
						if (Operators.CompareString(array3[num6], "", TextCompare: false) != 0)
						{
							goto IL_0715;
						}
						goto IL_0c15;
						IL_0715:
						num2 = 120;
						text2 = Strings.Trim(array3[num6]);
						goto IL_0724;
						IL_0724:
						num2 = 121;
						MyCS = "";
						goto IL_072e;
						IL_072e:
						num2 = 122;
						Get_CS_and_Label("", text2, ref MyCS, ref MyLabel);
						goto IL_0742;
						IL_0742:
						num2 = 123;
						if (LikeOperator.LikeString(Strings.UCase(text2), "TEXT:*:*", CompareMethod.Binary))
						{
							goto IL_075d;
						}
						goto IL_07ba;
						IL_075d:
						num2 = 124;
						num8 = Strings.InStr(6, text2, ":");
						goto IL_0770;
						IL_0770:
						num2 = 125;
						MyCS = Strings.Trim(Strings.Mid(text2, 6, num8 - 6));
						goto IL_0786;
						IL_0786:
						num2 = 126;
						text2 = "\r\n" + Get_HTML_Label(Strings.Mid(General_Procedures.Quote_CRLF_Replace("SD", text2), num8 + 1)) + "\r\n";
						goto IL_0b92;
						IL_07ba:
						num2 = 128;
						if (LikeOperator.LikeString(Strings.UCase(text2), "IMAGES:*:*", CompareMethod.Binary))
						{
							goto IL_07d8;
						}
						goto IL_082f;
						IL_07d8:
						num2 = 129;
						num8 = Strings.InStr(8, text2, ":");
						goto IL_07ee;
						IL_07ee:
						num2 = 130;
						MyCS = Strings.Trim(Strings.Mid(text2, 8, num8 - 8));
						goto IL_0807;
						IL_0807:
						num2 = 131;
						text2 = "\r\nIDR:" + Strings.Mid(text2, num8 + 1) + "\r\n";
						goto IL_0b92;
						IL_082f:
						num2 = 133;
						if (LikeOperator.LikeString(Strings.UCase(text2), "JMP-HTML:*:*", CompareMethod.Binary))
						{
							goto IL_084d;
						}
						goto IL_08a7;
						IL_084d:
						num2 = 134;
						num8 = Strings.InStr(10, text2, ":");
						goto IL_0864;
						IL_0864:
						num2 = 135;
						MyCS = Strings.Trim(Strings.Mid(text2, 10, num8 - 10));
						goto IL_087f;
						IL_087f:
						num2 = 136;
						text2 = "\r\nIHJ:" + Strings.Mid(text2, num8 + 1) + "\r\n";
						goto IL_0b92;
						IL_08a7:
						num2 = 138;
						if (Operators.CompareString(Strings.Mid(Strings.UCase(text2) + "   ", 1, 3), "CI:", TextCompare: false) == 0)
						{
							goto IL_08d9;
						}
						goto IL_094d;
						IL_08d9:
						num2 = 139;
						text5 = Strings.Mid(MyLabel, 4);
						goto IL_08e9;
						IL_08e9:
						num2 = 140;
						num8 = Strings.InStrRev(Strings.UCase(text5), ".GIF");
						goto IL_0904;
						IL_0904:
						num2 = 141;
						if (num8 != 0)
						{
							goto IL_0915;
						}
						goto IL_092e;
						IL_0915:
						num2 = 142;
						text5 = Strings.Trim(Strings.Mid(text5, 1, num8 - 1));
						goto IL_092e;
						IL_092e:
						num2 = 143;
						text2 = "\r\nHTMIC:" + text5 + "\r\n";
						goto IL_0b92;
						IL_094d:
						num2 = 145;
						if ((Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".PNG", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".GIF", TextCompare: false) == 0))
						{
							goto IL_09a4;
						}
						goto IL_09e0;
						IL_09a4:
						num2 = 146;
						text6 = Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr)));
						goto IL_09c1;
						IL_09c1:
						num2 = 147;
						text2 = "\r\nIMG:" + text6 + ".gif\r\n";
						goto IL_0b92;
						IL_09e0:
						num2 = 149;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".RRR", TextCompare: false) == 0)
						{
							goto IL_0a11;
						}
						goto IL_0a4d;
						IL_0a11:
						num2 = 150;
						text6 = Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr)));
						goto IL_0a2e;
						IL_0a2e:
						num2 = 151;
						text2 = "\r\nIHT:" + text6 + "___.htm\r\n";
						goto IL_0b92;
						IL_0a4d:
						num2 = 153;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".PPP", TextCompare: false) == 0)
						{
							goto IL_0a7e;
						}
						goto IL_0aba;
						IL_0a7e:
						num2 = 154;
						text6 = Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr)));
						goto IL_0a9b;
						IL_0a9b:
						num2 = 155;
						text2 = "\r\nIHP:" + text6 + "___.htm\r\n";
						goto IL_0b92;
						IL_0aba:
						num2 = 157;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".BTN", TextCompare: false) == 0)
						{
							goto IL_0aeb;
						}
						goto IL_0b23;
						IL_0aeb:
						num2 = 158;
						text2 = "\r\nBTN:" + Strings.LCase(Strings.Trim(Strings.Replace(MyLabel, ".btn", ".htm", 1, -1, CompareMethod.Text))) + "\r\n";
						goto IL_0b92;
						IL_0b23:
						num2 = 160;
						if (Operators.CompareString(Strings.Mid(Strings.UCase(text2) + "  ", 1, 2), "I:", TextCompare: false) == 0)
						{
							goto IL_0b55;
						}
						goto IL_0b78;
						IL_0b55:
						num2 = 161;
						text2 = "\r\nHTMI:" + Strings.Mid(MyLabel, 3) + "\r\n";
						goto IL_0b92;
						IL_0b78:
						num2 = 163;
						text2 = "\r\nHTM:" + MyLabel + "\r\n";
						goto IL_0b92;
						IL_0b92:
						num2 = 165;
						if (Operators.CompareString(MyCS, "1", TextCompare: false) > 0)
						{
							goto IL_0bae;
						}
						goto IL_0bcb;
						IL_0bae:
						num2 = 166;
						MyCS = " colspan=\"" + MyCS + "\"";
						goto IL_0bd9;
						IL_0bcb:
						num2 = 168;
						MyCS = "";
						goto IL_0bd9;
						IL_0bd9:
						num2 = 170;
						text3 = text3 + "\r\n<td class=\"tblout\"" + MyCS + ">" + text2 + "</td>";
						goto IL_0c15;
						IL_0c15:
						num2 = 172;
						num6++;
						goto IL_0c21;
						IL_0c67:
						num2 = 178;
						text3 += "\r\n</table>";
						goto IL_0c7b;
						IL_0c7b:
						num2 = 179;
						if (Information.UBound(array) >= num10)
						{
							goto IL_0c99;
						}
						goto IL_117c;
						IL_0c99:
						num2 = 180;
						array2 = Strings.Split(array[num7 + 1], "|");
						goto IL_0cb4;
						IL_0cb4:
						num2 = 181;
						text2 = "";
						goto IL_0cc1;
						IL_0cc1:
						num2 = 182;
						num13 = Information.UBound(array2);
						num5 = 0;
						goto IL_1168;
						IL_1168:
						if (num5 <= num13)
						{
							goto IL_0cd9;
						}
						goto IL_1171;
						IL_1171:
						num2 = 226;
						array2 = null;
						goto IL_117c;
						IL_0cd9:
						num2 = 183;
						text2 = "";
						goto IL_0ce6;
						IL_0ce6:
						num2 = 184;
						if (Operators.CompareString(array2[num5], "", TextCompare: false) != 0)
						{
							goto IL_0d08;
						}
						goto IL_1122;
						IL_0d08:
						num2 = 185;
						text2 = Strings.Trim(array2[num5]);
						goto IL_0d1a;
						IL_0d1a:
						num2 = 186;
						MyCS = "";
						goto IL_0d27;
						IL_0d27:
						num2 = 187;
						Get_CS_and_Label("", text2, ref MyCS, ref MyLabel);
						goto IL_0d3e;
						IL_0d3e:
						num2 = 188;
						if (LikeOperator.LikeString(Strings.UCase(text2), "TEXT:*:*", CompareMethod.Binary))
						{
							goto IL_0d5f;
						}
						goto IL_0df0;
						IL_0d5f:
						num2 = 189;
						num8 = Strings.InStr(6, text2, ":");
						goto IL_0d75;
						IL_0d75:
						num2 = 190;
						MyCS = Strings.Trim(Strings.Mid(text2, 6, num8 - 6));
						goto IL_0d8e;
						IL_0d8e:
						num2 = 191;
						if (Operators.CompareString(MyCS, "1", TextCompare: false) > 0)
						{
							goto IL_0daa;
						}
						goto IL_0dc3;
						IL_0daa:
						num2 = 192;
						MyCS = " colspan=\"" + MyCS + "\"";
						goto IL_0dc3;
						IL_0dc3:
						num2 = 193;
						text2 = "\r\n" + Get_HTML_Label(Strings.Mid(text2, num8 + 1)) + "\r\n";
						goto IL_1122;
						IL_0df0:
						num2 = 195;
						if (LikeOperator.LikeString(Strings.UCase(text2), "IMAGES:*:*", CompareMethod.Binary))
						{
							goto IL_0e11;
						}
						goto IL_0e9d;
						IL_0e11:
						num2 = 196;
						num8 = Strings.InStr(8, text2, ":");
						goto IL_0e27;
						IL_0e27:
						num2 = 197;
						MyCS = Strings.Trim(Strings.Mid(text2, 8, num8 - 8));
						goto IL_0e40;
						IL_0e40:
						num2 = 198;
						if (Operators.CompareString(MyCS, "1", TextCompare: false) > 0)
						{
							goto IL_0e5c;
						}
						goto IL_0e75;
						IL_0e5c:
						num2 = 199;
						MyCS = " colspan=\"" + MyCS + "\"";
						goto IL_0e75;
						IL_0e75:
						num2 = 200;
						text2 = "\r\nIDR:" + Strings.Mid(text2, num8 + 1) + "\r\n";
						goto IL_1122;
						IL_0e9d:
						num2 = 202;
						if (LikeOperator.LikeString(Strings.UCase(text2), "JMP-HTML:*:*", CompareMethod.Binary))
						{
							goto IL_0ebe;
						}
						goto IL_0f4d;
						IL_0ebe:
						num2 = 203;
						num8 = Strings.InStr(10, text2, ":");
						goto IL_0ed5;
						IL_0ed5:
						num2 = 204;
						MyCS = Strings.Trim(Strings.Mid(text2, 10, num8 - 10));
						goto IL_0ef0;
						IL_0ef0:
						num2 = 205;
						if (Operators.CompareString(MyCS, "1", TextCompare: false) > 0)
						{
							goto IL_0f0c;
						}
						goto IL_0f25;
						IL_0f0c:
						num2 = 206;
						MyCS = " colspan=\"" + MyCS + "\"";
						goto IL_0f25;
						IL_0f25:
						num2 = 207;
						text2 = "\r\nIHJ:" + Strings.Mid(text2, num8 + 1) + "\r\n";
						goto IL_1122;
						IL_0f4d:
						num2 = 209;
						if ((Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".PNG", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".GIF", TextCompare: false) == 0))
						{
							goto IL_0fa4;
						}
						goto IL_0fd6;
						IL_0fa4:
						num2 = 210;
						text2 = "\r\nIMG:" + Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr))) + ".gif\r\n";
						goto IL_1122;
						IL_0fd6:
						num2 = 212;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".RRR", TextCompare: false) == 0)
						{
							goto IL_1007;
						}
						goto IL_1039;
						IL_1007:
						num2 = 213;
						text2 = "\r\nIHT:" + Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr))) + ".htm\r\n";
						goto IL_1122;
						IL_1039:
						num2 = 215;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".PPP", TextCompare: false) == 0)
						{
							goto IL_106a;
						}
						goto IL_109c;
						IL_106a:
						num2 = 216;
						text2 = "\r\nIHP:" + Strings.LCase(Strings.Trim(Get_JSL_Display_Label(MyLabel, ref MyTrueArr, ref MyDisplayArr))) + ".htm\r\n";
						goto IL_1122;
						IL_109c:
						num2 = 218;
						if (Operators.CompareString(Strings.UCase(Strings.Right("    " + MyLabel, 4)), ".BTN", TextCompare: false) == 0)
						{
							goto IL_10cd;
						}
						goto IL_1106;
						IL_10cd:
						num2 = 219;
						text2 = "\r\nBTN:" + Strings.LCase(Strings.Trim(Strings.Replace(MyLabel, ".btn", ".htm", 1, -1, CompareMethod.Text))) + "\r\n";
						goto IL_1122;
						IL_1106:
						num2 = 221;
						text2 = "\r\nHTM:" + MyLabel + "\r\n";
						goto IL_1122;
						IL_1122:
						num2 = 224;
						text4 = text4 + "\r\n<tr class=\"tblout\"><td class=\"tblout\"" + MyCS + ">" + text2 + "</td></tr>";
						goto IL_115c;
						IL_115c:
						num2 = 225;
						num5++;
						goto IL_1168;
						IL_117c:
						num2 = 228;
						text4 += "\r\n</table>";
						goto IL_1190;
						IL_1190:
						num2 = 229;
						array = null;
						goto IL_1199;
						IL_1199:
						num2 = 230;
						if (Operators.CompareString(left, "0", TextCompare: false) == 0)
						{
							goto IL_11b5;
						}
						goto IL_11eb;
						IL_11b5:
						num2 = 231;
						text = text + "\r\n" + text3 + "\r\n</td><td class=\"tblout\" valign=\"top\">\r\n" + text4;
						goto IL_121e;
						IL_11eb:
						num2 = 233;
						text = text + "\r\n" + text4 + "\r\n</td><td class=\"tblout\" valign=\"top\">\r\n" + text3;
						goto IL_121e;
						end_IL_0001_2:
						break;
					}
					num2 = 239;
					array = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5681;
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

	public static string Generate_Create_Tab_Menu_Script(string MyMode, string MyInData, string MyWinTitle)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string result = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text12 = default(string);
		string text13 = default(string);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		string text14 = default(string);
		string[] array = default(string[]);
		string[] array2 = default(string[]);
		string[] array3 = default(string[]);
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
					case 2881:
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
								goto IL_002e;
							case 7:
								goto IL_0037;
							case 8:
								goto IL_0040;
							case 9:
								goto IL_0049;
							case 10:
								goto IL_0053;
							case 11:
								goto IL_005d;
							case 12:
								goto IL_0067;
							case 13:
								goto IL_0071;
							case 14:
								goto IL_007b;
							case 15:
								goto IL_0085;
							case 16:
								goto IL_008f;
							case 17:
								goto IL_0095;
							case 18:
								goto IL_009b;
							case 19:
								goto IL_00a1;
							case 20:
								goto IL_00a7;
							case 21:
								goto IL_00ad;
							case 22:
								goto IL_00b3;
							case 23:
								goto IL_00bd;
							case 24:
								goto IL_00c3;
							case 25:
								goto IL_00c9;
							case 26:
								goto IL_00cf;
							case 27:
								goto IL_00d5;
							case 28:
								goto IL_00f5;
							case 29:
								goto IL_0107;
							case 30:
								goto IL_0125;
							case 31:
								goto IL_013d;
							case 32:
							case 33:
								goto IL_0151;
							case 34:
								goto IL_0165;
							case 35:
								goto IL_017e;
							case 36:
							case 37:
								goto IL_0193;
							case 38:
								goto IL_01a7;
							case 39:
								goto IL_01b5;
							case 40:
								goto IL_01ce;
							case 41:
								goto IL_01f0;
							case 42:
								goto IL_020a;
							case 43:
								goto IL_0224;
							case 44:
								goto IL_024e;
							case 46:
								goto IL_0263;
							case 45:
							case 47:
							case 48:
								goto IL_0277;
							case 49:
								goto IL_028a;
							case 50:
								goto IL_029d;
							case 51:
								goto IL_02b9;
							case 52:
								goto IL_02d4;
							case 53:
								goto IL_02ea;
							case 54:
								goto IL_02f0;
							case 55:
								goto IL_02f6;
							case 56:
								goto IL_02fc;
							case 57:
								goto IL_0302;
							case 58:
								goto IL_0317;
							case 59:
								goto IL_0321;
							case 60:
								goto IL_0337;
							case 61:
								goto IL_0341;
							case 62:
								goto IL_034b;
							case 63:
								goto IL_0355;
							case 64:
								goto IL_035f;
							case 65:
								goto IL_0369;
							case 66:
								goto IL_037f;
							case 68:
								goto IL_03e2;
							case 70:
								goto IL_0407;
							case 71:
								goto IL_0426;
							case 72:
								goto IL_043f;
							case 73:
								goto IL_0449;
							case 75:
								goto IL_0456;
							case 76:
								goto IL_046f;
							case 77:
								goto IL_0479;
							case 79:
								goto IL_0486;
							case 80:
								goto IL_049f;
							case 81:
								goto IL_04a9;
							case 84:
								goto IL_04b7;
							case 85:
								goto IL_04d6;
							case 86:
								goto IL_04ef;
							case 89:
								goto IL_050f;
							case 67:
							case 69:
							case 74:
							case 78:
							case 82:
							case 83:
							case 87:
							case 88:
							case 90:
							case 91:
								goto IL_0531;
							case 92:
								goto IL_0544;
							case 94:
								goto IL_056e;
							case 95:
								goto IL_0586;
							case 96:
								goto IL_05a3;
							case 97:
								goto IL_05bb;
							case 99:
								goto IL_05ca;
							case 100:
								goto IL_05d4;
							case 101:
								goto IL_05f0;
							case 102:
								goto IL_05fe;
							case 103:
							case 104:
								goto IL_0611;
							case 105:
								goto IL_065a;
							case 106:
								goto IL_0663;
							case 107:
								goto IL_066c;
							case 109:
								goto IL_068a;
							case 110:
								goto IL_06a0;
							case 111:
								goto IL_06ae;
							case 112:
							case 113:
								goto IL_06c1;
							case 114:
								goto IL_0705;
							case 115:
								goto IL_070b;
							case 108:
							case 116:
							case 117:
								goto IL_0715;
							case 93:
							case 98:
							case 118:
							case 119:
								goto IL_0726;
							case 120:
								goto IL_072c;
							case 121:
								goto IL_073e;
							case 122:
								goto IL_0745;
							case 124:
								goto IL_0772;
							case 125:
								goto IL_078b;
							case 126:
								goto IL_0798;
							case 127:
								goto IL_07b1;
							case 128:
								goto IL_07be;
							case 129:
								goto IL_07da;
							case 130:
								goto IL_07ea;
							case 132:
								goto IL_082f;
							case 133:
								goto IL_084b;
							case 134:
								goto IL_085b;
							case 135:
							case 136:
								goto IL_0871;
							case 137:
								goto IL_088d;
							case 138:
								goto IL_089d;
							case 139:
								goto IL_08b3;
							case 123:
							case 131:
							case 140:
							case 141:
							case 142:
								goto IL_08ce;
							case 143:
							case 144:
							case 145:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 146:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0486:
						num2 = 79;
						if (Operators.CompareString(text, "Menu-Item", TextCompare: false) == 0)
						{
							goto IL_049f;
						}
						goto IL_0531;
						IL_049f:
						num2 = 80;
						text2 = "";
						goto IL_04a9;
						IL_0479:
						num2 = 77;
						text3 = "https://dtdpathwebnlb.ch.intel.com/sqlpathfinder_reports/jqwidgets/Production_spf/folder.png";
						goto IL_0531;
						IL_04a9:
						num2 = 81;
						text3 = "https://dtdpathwebnlb.ch.intel.com/sqlpathfinder_reports/jqwidgets/Production_spf/book.png";
						goto IL_0531;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						text4 = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						text5 = "";
						goto IL_0025;
						IL_0025:
						num2 = 5;
						text6 = "";
						goto IL_002e;
						IL_002e:
						num2 = 6;
						text7 = "";
						goto IL_0037;
						IL_0037:
						num2 = 7;
						text8 = "";
						goto IL_0040;
						IL_0040:
						num2 = 8;
						text9 = "";
						goto IL_0049;
						IL_0049:
						num2 = 9;
						text10 = "";
						goto IL_0053;
						IL_0053:
						num2 = 10;
						text11 = "";
						goto IL_005d;
						IL_005d:
						num2 = 11;
						text12 = "";
						goto IL_0067;
						IL_0067:
						num2 = 12;
						text = "";
						goto IL_0071;
						IL_0071:
						num2 = 13;
						text13 = "";
						goto IL_007b;
						IL_007b:
						num2 = 14;
						text3 = "";
						goto IL_0085;
						IL_0085:
						num2 = 15;
						text2 = "";
						goto IL_008f;
						IL_008f:
						num2 = 16;
						num5 = 0;
						goto IL_0095;
						IL_0095:
						num2 = 17;
						num6 = 0;
						goto IL_009b;
						IL_009b:
						num2 = 18;
						num7 = 0;
						goto IL_00a1;
						IL_00a1:
						num2 = 19;
						num8 = 0;
						goto IL_00a7;
						IL_00a7:
						num2 = 20;
						num9 = 0;
						goto IL_00ad;
						IL_00ad:
						num2 = 21;
						num10 = 0;
						goto IL_00b3;
						IL_00b3:
						num2 = 22;
						text14 = "";
						goto IL_00bd;
						IL_00bd:
						num2 = 23;
						array = null;
						goto IL_00c3;
						IL_00c3:
						num2 = 24;
						array2 = null;
						goto IL_00c9;
						IL_00c9:
						num2 = 25;
						array3 = null;
						goto IL_00cf;
						IL_00cf:
						num2 = 26;
						num11 = 7;
						goto IL_00d5;
						IL_00d5:
						num2 = 27;
						if (Operators.CompareString(Strings.Trim(MyInData), "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_00f5;
						IL_00f5:
						num2 = 28;
						array = Strings.Split(MyInData, "<!@@@!>");
						goto IL_0107;
						IL_0107:
						num2 = 29;
						text4 = ":FILE:" + BuildForm.Replace_Globals(Strings.Trim(array[0]));
						goto IL_0125;
						IL_0125:
						num2 = 30;
						if (Operators.CompareString(MyWinTitle, "Window Title", TextCompare: false) != 0)
						{
							goto IL_013d;
						}
						goto IL_0151;
						IL_013d:
						num2 = 31;
						text4 = text4 + "\r\n:WINTITLE:" + MyWinTitle;
						goto IL_0151;
						IL_0151:
						num2 = 33;
						text9 = BuildForm.Replace_Globals(Strings.Trim(array[1]));
						goto IL_0165;
						IL_0165:
						num2 = 34;
						if (Operators.CompareString(text9, "", TextCompare: false) != 0)
						{
							goto IL_017e;
						}
						goto IL_0193;
						IL_017e:
						num2 = 35;
						text4 = text4 + "\r\n:IN:" + text9;
						goto IL_0193;
						IL_0193:
						num2 = 37;
						text10 = BuildForm.Replace_Globals(Strings.Trim(array[2]));
						goto IL_01a7;
						IL_01a7:
						num2 = 38;
						text11 = Strings.Trim(array[5]);
						goto IL_01b5;
						IL_01b5:
						num2 = 39;
						if (Operators.CompareString(text10, "", TextCompare: false) != 0)
						{
							goto IL_01ce;
						}
						goto IL_01f0;
						IL_01ce:
						num2 = 40;
						text4 = text4 + "\r\n:TITLE:" + Get_HTML_Label(General_Procedures.Quote_CRLF_Replace("SD", text10));
						goto IL_01f0;
						IL_01f0:
						num2 = 41;
						text4 = text4 + "\r\n:THEME:" + Strings.Trim(array[3]);
						goto IL_020a;
						IL_020a:
						num2 = 42;
						text4 = text4 + "\r\n:WIDTH:" + Strings.Trim(array[4]);
						goto IL_0224;
						IL_0224:
						num2 = 43;
						if (Operators.CompareString(text10, "", TextCompare: false) == 0 && Operators.CompareString(MyMode, "MENU", TextCompare: false) == 0)
						{
							goto IL_024e;
						}
						goto IL_0263;
						IL_03e2:
						num2 = 68;
						text12 = Strings.RTrim(Strings.Mid(array3[num6] + "    ", 5));
						goto IL_0531;
						IL_024e:
						num2 = 44;
						text4 += "\r\n:HEIGHT:0";
						goto IL_0277;
						IL_0263:
						num2 = 46;
						text4 = text4 + "\r\n:HEIGHT:" + text11;
						goto IL_0277;
						IL_0277:
						num2 = 48;
						text14 = Strings.UCase(Strings.Trim(array[6]));
						goto IL_028a;
						IL_028a:
						num2 = 49;
						text4 = text4 + "\r\n:NODISP:" + text14;
						goto IL_029d;
						IL_029d:
						num2 = 50;
						if (Operators.CompareString(text9, "", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_02b9;
						IL_02b9:
						num2 = 51;
						if (Information.UBound(array) >= num11)
						{
							goto IL_02d4;
						}
						goto IL_08ce;
						IL_02d4:
						num2 = 52;
						array2 = Strings.Split(array[num11], "<!|!>");
						goto IL_02ea;
						IL_02ea:
						num2 = 53;
						num8 = 0;
						goto IL_02f0;
						IL_02f0:
						num2 = 54;
						num9 = 0;
						goto IL_02f6;
						IL_02f6:
						num2 = 55;
						num7 = 0;
						goto IL_02fc;
						IL_02fc:
						num2 = 56;
						num10 = 0;
						goto IL_0302;
						IL_0302:
						num2 = 57;
						num12 = Information.UBound(array2);
						num5 = 0;
						goto IL_0735;
						IL_0735:
						if (num5 <= num12)
						{
							goto IL_0317;
						}
						goto IL_073e;
						IL_073e:
						num2 = 121;
						array2 = null;
						goto IL_0745;
						IL_0745:
						num2 = 122;
						if (Operators.CompareString(MyMode, "TAB", TextCompare: false) == 0)
						{
							goto IL_0772;
						}
						if (Operators.CompareString(MyMode, "MENU", TextCompare: false) == 0)
						{
							goto IL_082f;
						}
						goto IL_08ce;
						IL_0531:
						num2 = 91;
						num6++;
						goto IL_053a;
						IL_082f:
						num2 = 132;
						if (Operators.CompareString(text5, "", TextCompare: false) != 0)
						{
							goto IL_084b;
						}
						goto IL_0871;
						IL_084b:
						num2 = 133;
						text5 = Strings.Mid(text5, 5);
						goto IL_085b;
						IL_085b:
						num2 = 134;
						text5 += "<cr>]}";
						goto IL_0871;
						IL_0871:
						num2 = 136;
						if (Operators.CompareString(text6, "", TextCompare: false) != 0)
						{
							goto IL_088d;
						}
						goto IL_089d;
						IL_088d:
						num2 = 137;
						text6 = Strings.Mid(text6, 2);
						goto IL_089d;
						IL_089d:
						num2 = 138;
						text4 = text4 + "\r\n:TREE:" + text5;
						goto IL_08b3;
						IL_08b3:
						num2 = 139;
						text4 = text4 + "\r\n:URL:" + text6;
						goto IL_08ce;
						IL_0772:
						num2 = 124;
						if (Operators.CompareString(text5, "", TextCompare: false) != 0)
						{
							goto IL_078b;
						}
						goto IL_0798;
						IL_078b:
						num2 = 125;
						text5 = Strings.Mid(text5, 5);
						goto IL_0798;
						IL_0798:
						num2 = 126;
						if (Operators.CompareString(text6, "", TextCompare: false) != 0)
						{
							goto IL_07b1;
						}
						goto IL_07be;
						IL_07b1:
						num2 = 127;
						text6 = Strings.Mid(text6, 5);
						goto IL_07be;
						IL_07be:
						num2 = 128;
						if (Operators.CompareString(text7, "", TextCompare: false) != 0)
						{
							goto IL_07da;
						}
						goto IL_07ea;
						IL_07da:
						num2 = 129;
						text7 = Strings.Mid(text7, 6);
						goto IL_07ea;
						IL_07ea:
						num2 = 130;
						text4 = text4 + "\r\n:LI:" + text5 + "\r\n:DIV:" + text6 + "\r\n:URL:" + text7;
						goto IL_08ce;
						IL_0317:
						num2 = 58;
						text8 = "";
						goto IL_0321;
						IL_0321:
						num2 = 59;
						array3 = Strings.Split(array2[num5], "<!;!>");
						goto IL_0337;
						IL_0337:
						num2 = 60;
						text12 = "";
						goto IL_0341;
						IL_0341:
						num2 = 61;
						text = "";
						goto IL_034b;
						IL_034b:
						num2 = 62;
						text3 = "";
						goto IL_0355;
						IL_0355:
						num2 = 63;
						text13 = "";
						goto IL_035f;
						IL_035f:
						num2 = 64;
						text2 = "";
						goto IL_0369;
						IL_0369:
						num2 = 65;
						num13 = Information.UBound(array3);
						num6 = 0;
						goto IL_053a;
						IL_053a:
						if (num6 <= num13)
						{
							goto IL_037f;
						}
						goto IL_0544;
						IL_0544:
						num2 = 92;
						if (Operators.CompareString(MyMode, "TAB", TextCompare: false) == 0)
						{
							goto IL_056e;
						}
						if (Operators.CompareString(MyMode, "MENU", TextCompare: false) == 0)
						{
							goto IL_05ca;
						}
						goto IL_0726;
						IL_08ce:
						num2 = 142;
						array = null;
						break;
						IL_05ca:
						num2 = 99;
						text8 = "<cr>";
						goto IL_05d4;
						IL_05d4:
						num2 = 100;
						if (Operators.CompareString(text, "Menu-Item", TextCompare: false) == 0)
						{
							goto IL_05f0;
						}
						goto IL_068a;
						IL_05f0:
						num2 = 101;
						if (num7 != 0)
						{
							goto IL_05fe;
						}
						goto IL_0611;
						IL_05fe:
						num2 = 102;
						text8 += ",";
						goto IL_0611;
						IL_0611:
						num2 = 104;
						text8 = text8 + "{ label: \"" + text12 + "\", id:\"spfmenu" + Conversions.ToString(num10) + "\", icon: \"" + text3 + "\"}";
						goto IL_065a;
						IL_065a:
						num2 = 105;
						num7++;
						goto IL_0663;
						IL_0663:
						num2 = 106;
						num10++;
						goto IL_066c;
						IL_066c:
						num2 = 107;
						text6 = text6 + ",\"" + text13 + "\"";
						goto IL_0715;
						IL_068a:
						num2 = 109;
						if (LikeOperator.LikeString(text, "Folder*", CompareMethod.Binary))
						{
							goto IL_06a0;
						}
						goto IL_0715;
						IL_06a0:
						num2 = 110;
						if (num8 != 0)
						{
							goto IL_06ae;
						}
						goto IL_06c1;
						IL_06ae:
						num2 = 111;
						text8 += "]}<cr>,";
						goto IL_06c1;
						IL_06c1:
						num2 = 113;
						text8 = text8 + "{ label: \"" + text12 + "\", expanded: " + text2 + ", icon: \"" + text3 + "\", items: [";
						goto IL_0705;
						IL_0705:
						num2 = 114;
						num7 = 0;
						goto IL_070b;
						IL_070b:
						num2 = 115;
						num8++;
						goto IL_0715;
						IL_0715:
						num2 = 117;
						text5 += text8;
						goto IL_0726;
						IL_056e:
						num2 = 94;
						text5 = text5 + "<cr><li>" + text12 + "</li>";
						goto IL_0586;
						IL_0586:
						num2 = 95;
						text6 = text6 + "<cr><div id='spftabs1_" + Conversions.ToString(num9) + "'></div>";
						goto IL_05a3;
						IL_05a3:
						num2 = 96;
						text7 = text7 + ",<cr>'" + text13 + "'";
						goto IL_05bb;
						IL_05bb:
						num2 = 97;
						num9++;
						goto IL_0726;
						IL_0726:
						num2 = 119;
						array3 = null;
						goto IL_072c;
						IL_072c:
						num2 = 120;
						num5++;
						goto IL_0735;
						IL_037f:
						num2 = 66;
						switch (Strings.Mid(array3[num6] + "    ", 1, 4))
						{
						case "LBL:":
							break;
						case "FLD:":
							goto IL_0407;
						case "ICO:":
							goto IL_04b7;
						case "URL:":
							goto IL_050f;
						default:
							goto IL_0531;
						}
						goto IL_03e2;
						IL_050f:
						num2 = 89;
						text13 = Strings.RTrim(Strings.Mid(array3[num6] + "    ", 5));
						goto IL_0531;
						IL_04b7:
						num2 = 84;
						text3 = Strings.RTrim(Strings.Mid(array3[num6] + "    ", 5));
						goto IL_04d6;
						IL_04d6:
						num2 = 85;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_04ef;
						}
						goto IL_0531;
						IL_04ef:
						num2 = 86;
						text3 = "https://dtdpathwebnlb.ch.intel.com/sqlpathfinder_reports/jqwidgets/Production_spf/" + Strings.LCase(text3) + ".png";
						goto IL_0531;
						IL_0407:
						num2 = 70;
						text = Strings.RTrim(Strings.Mid(array3[num6] + "    ", 5));
						goto IL_0426;
						IL_0426:
						num2 = 71;
						if (Operators.CompareString(text, "Folder-Open", TextCompare: false) == 0)
						{
							goto IL_043f;
						}
						goto IL_0456;
						IL_043f:
						num2 = 72;
						text2 = "true";
						goto IL_0449;
						IL_0449:
						num2 = 73;
						text3 = "https://dtdpathwebnlb.ch.intel.com/sqlpathfinder_reports/jqwidgets/Production_spf/folder.png";
						goto IL_0531;
						IL_0456:
						num2 = 75;
						if (Operators.CompareString(text, "Folder-Close", TextCompare: false) == 0)
						{
							goto IL_046f;
						}
						goto IL_0486;
						IL_046f:
						num2 = 76;
						text2 = "false";
						goto IL_0479;
						end_IL_0001_2:
						break;
					}
					num2 = 145;
					result = text4;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2881;
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

	public static void Get_Prior_Chart_Table_Filter_Objects(ref TreeNode CurrentNode, ref string MyChart, ref string MyChartActual)
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
						errsource = "BuildChart - Get_Prior_Chart_Table_Filter_Objects";
						if (CurrentNode == null)
						{
							goto end_IL_0001;
						}
						TreeNode treeNode = null;
						TreeNode treeNode2 = null;
						TreeNode treeNode3 = null;
						int num3 = 0;
						treeNode = CurrentNode.Parent;
						int num4 = 0;
						string text = "";
						string text2 = "";
						bool flag = true;
						IEnumerator enumerator = treeNode.Nodes.GetEnumerator();
						while (enumerator.MoveNext())
						{
							treeNode2 = (TreeNode)enumerator.Current;
							num4 = Strings.InStr(Conversions.ToString(treeNode2.Tag), ":");
							text = ((num4 == 0) ? "" : Strings.Trim(Strings.Mid(Conversions.ToString(treeNode2.Tag), num4 + 1)));
							if (Conversions.ToBoolean(Operators.OrObject(Operators.OrObject(Operators.OrObject(Operators.OrObject(Operators.OrObject(Operators.OrObject(LikeOperator.LikeObject(treeNode2.Tag, "JMP-CHART*", CompareMethod.Binary), LikeOperator.LikeObject(treeNode2.Tag, "R-CHART*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "JMP-BUTTON*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "R-BUTTON*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "HTML-BUTTON*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "HTML-REPORT*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "HTML-CHART*", CompareMethod.Binary))))
							{
								text2 = treeNode2.Nodes[0].Text;
								if (Operators.CompareString(text2, "", TextCompare: false) == 0)
								{
									text2 = Strings.StrConv(text, VbStrConv.ProperCase);
								}
								flag = true;
								if (Conversions.ToBoolean(Operators.OrObject(Operators.OrObject(LikeOperator.LikeObject(treeNode2.Tag, "JMP-CHART*", CompareMethod.Binary), LikeOperator.LikeObject(treeNode2.Tag, "R-CHART*", CompareMethod.Binary)), LikeOperator.LikeObject(treeNode2.Tag, "HTML-CHART*", CompareMethod.Binary))) && treeNode2.Nodes.Count >= 3 && Operators.CompareString(General_Procedures.Get_Node_Value(treeNode2.Nodes[2].Text), "No", TextCompare: false) == 0)
								{
									flag = false;
								}
								if (flag)
								{
									if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, "HTML-REPORTI*", CompareMethod.Binary)))
									{
										text2 = "I:" + text2;
										text = "I:" + text;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, "HTML-CHARTI*", CompareMethod.Binary)))
									{
										text2 = "CI:" + text2;
										text = "CI:" + text;
									}
									MyChart = MyChart + "," + text2;
									MyChartActual = MyChartActual + "," + text;
								}
							}
							else if (Conversions.ToBoolean(Operators.OrObject(LikeOperator.LikeObject(treeNode2.Tag, "JMP-ADD JSL*", CompareMethod.Binary), LikeOperator.LikeObject(treeNode2.Tag, "R-ADD R-SCRIPT*", CompareMethod.Binary))))
							{
								text2 = treeNode2.Nodes[0].Text;
								if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) != 0)
								{
									MyChart = MyChart + "," + text2;
									MyChartActual = MyChartActual + "," + text;
								}
							}
							else if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, "JMP-ADD LAYOUT*", CompareMethod.Binary)))
							{
								text2 = Strings.Trim(General_Procedures.Get_Node_Value(treeNode2.Text));
								num4 = Strings.InStr(text2, ":");
								if (num4 != 0)
								{
									text2 = Strings.Trim(Strings.Mid(text2, 1, num4 - 1));
								}
								MyChart = MyChart + "," + text2;
								MyChartActual = MyChartActual + "," + text;
							}
							else
							{
								if (!Conversions.ToBoolean(LikeOperator.LikeObject(treeNode2.Tag, "HTML-R-PROGRAM*", CompareMethod.Binary)))
								{
									continue;
								}
								IEnumerator enumerator2 = treeNode2.Nodes[0].Nodes.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									treeNode3 = (TreeNode)enumerator2.Current;
									num3 = Strings.InStr(Conversions.ToString(treeNode3.Tag), ":");
									text = ((num3 == 0) ? "" : Strings.Trim(Strings.Mid(Conversions.ToString(treeNode3.Tag), num3 + 1)));
									if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode3.Tag, "R-CHART*", CompareMethod.Binary)))
									{
										text2 = treeNode3.Nodes[0].Text;
										if (Operators.CompareString(text2, "", TextCompare: false) == 0)
										{
											text2 = Strings.StrConv(text, VbStrConv.ProperCase);
										}
										if (treeNode3.Nodes.Count >= 3 && Operators.CompareString(General_Procedures.Get_Node_Value(treeNode3.Nodes[2].Text), "No", TextCompare: false) != 0)
										{
											MyChart = MyChart + "," + text2;
											MyChartActual = MyChartActual + "," + text;
										}
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(treeNode3.Tag, "R-ADD R-SCRIPT*", CompareMethod.Binary)))
									{
										text2 = treeNode3.Nodes[0].Text;
										if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) != 0)
										{
											MyChart = MyChart + "," + text2;
											MyChartActual = MyChartActual + "," + text;
										}
									}
								}
								if (enumerator2 is IDisposable)
								{
									(enumerator2 as IDisposable).Dispose();
								}
							}
						}
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
						if (Operators.CompareString(MyChart, "", TextCompare: false) != 0)
						{
							MyChart = Strings.Mid(MyChart, 2);
						}
						if (Operators.CompareString(MyChartActual, "", TextCompare: false) != 0)
						{
							MyChartActual = Strings.Mid(MyChartActual, 2);
						}
						goto end_IL_0001_2;
					}
					case 1633:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							MyChart = "";
							MyChartActual = "";
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_0697;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1633;
				continue;
			}
			break;
			IL_0697:
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

	public static string Get_JSL_Script(ref TreeNode MyNode, int MyMode, bool IsSingle = false)
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
					switch (try0001_dispatch)
					{
					default:
					{
						text = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "BuildChart - Get_JSL_Script";
						if (MyNode == null)
						{
							goto end_IL_0001;
						}
						string text2 = Conversions.ToString(MyNode.Tag);
						int num3 = 0;
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string left = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						string text11 = "";
						string text12 = "";
						string text13 = "";
						int num4 = 0;
						string text14 = "";
						string text15 = "";
						string text16 = "";
						string text17 = "";
						string text18 = "";
						string text19 = "If (IsEmpty(::Default_Dir),::Default_Dir =\"$default$dir$\");\r\n";
						string text20 = "";
						string left2 = "";
						bool flag = false;
						bool flag2 = false;
						num3 = Strings.InStr(text2, ":");
						if (num3 != 0)
						{
							text15 = Strings.Mid(text2, num3);
							text16 = Strings.Mid(text2, num3 + 1);
							text16 = Strings.StrConv(text16, VbStrConv.ProperCase);
							text2 = Strings.Mid(text2, 1, num3 - 1);
						}
						else
						{
							text15 = "";
							text16 = "";
						}
						switch (text2)
						{
						case "JMP-SAVE WINDOW":
							text5 = General_Procedures.Get_Node_Value(MyNode.Text);
							text3 = General_Procedures.Get_Node_Value(MyNode.Nodes[0].Text);
							if (MyMode == 0)
							{
								if ((Operators.CompareString(text3, "No", TextCompare: false) != 0) & (Operators.CompareString(text5, "No", TextCompare: false) != 0))
								{
									num3 = Strings.InStrRev(text3, ".");
									text4 = ((num3 != 0) ? Strings.Trim(Strings.UCase(Strings.Mid(text3, num3 + 1))) : "JRN");
									text5 = "::" + text5 + "_" + Globals_Renamed.gSPFCache;
									text = text19 + "\r\n" + Save_JSL_Output(text5, text4, text3);
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text5;
							}
							break;
						case "JMP-SAVE TABLE":
						case "R-SAVE TABLE":
							text4 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
							text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Nodes[0].Text));
							if (MyMode == 0)
							{
								if (!((Operators.CompareString(text3, "No", TextCompare: false) != 0) & (Operators.CompareString(text4, "No", TextCompare: false) != 0)))
								{
									break;
								}
								num3 = Strings.InStrRev(text3, ".");
								text5 = ((num3 != 0) ? Strings.Trim(Strings.UCase(Strings.Mid(text3, num3 + 1))) : "CSV");
								if (Operators.CompareString(text2, "JMP-SAVE TABLE", TextCompare: false) == 0)
								{
									text = string.Concat(str2: (Strings.InStrRev(text3, "\\") != 0) ? ("::MyOutFile1= \"" + Strings.Trim(text3) + "\";") : (text19 + "::MyOutFile1= ::Default_Dir || \"" + Strings.Trim(text3) + "\";"), str0: Add_JSL_Title("Save a Table", "JMP"), str1: "\r\n");
									if (Operators.CompareString(text5, "CSV", TextCompare: false) != 0)
									{
										if (Operators.CompareString(text5, "JMP", TextCompare: false) == 0)
										{
											text = text + "\r\nTry(Data table(\"" + text4 + "\") << save (::MyOutFile1),Print(\"Could not save Data Table ...\"));";
										}
									}
									else
									{
										text += "\r\ncurrent_pref = Char( Arg( Parse( (Char( Get Preferences( Export settings ) )) ), 1 ) ); // Get current prefs\r\nPref(Export Settings(End Of Field( Comma ), Export Table Headers( 1 )));  // Set prefs (comma delimited, no headers)";
										text = text + "\r\nTry(Data table(\"" + text4 + "\") << save (::MyOutFile1,Text),Print(\"Could not save Data Table ...\"));";
										text += "\r\nEval( Parse( \"pref(\" || current_pref || \")\" ) );  //Reset prefs";
									}
									break;
								}
								text6 = ((Strings.InStrRev(text3, "\\") != 0) ? ("::myoutfile1 <- \"" + Strings.Trim(text3) + "\";") : ("myoutfile1 <- paste(default_dir,\"" + Strings.Trim(text3) + "\",sep=\"\");"));
								if (Operators.CompareString(text5, "CSV", TextCompare: false) == 0)
								{
									text9 = ",";
								}
								else if (Operators.CompareString(text5, "TAB", TextCompare: false) == 0)
								{
									text9 = "\\t";
								}
								text = Add_JSL_Title("Save a Dataframe", "R", "P") + "\r\n" + text6;
								text = ((Operators.CompareString(text5, "RDATA", TextCompare: false) != 0) ? (text + "\r\nwrite.table(" + text4 + ",file=myoutfile1,append=FALSE,quote=FALSE,na=\"\",sep=\"" + text9 + "\",dec=\".\",eol=\"\\n\",col.names=TRUE,row.names=FALSE);") : (text + "\r\nsaveRDS(" + text4 + ",file=myoutfile1);"));
								text = text + "\r\ncat(paste(\"  Data Saved to " + text5 + " file: " + Strings.Trim(text3) + " ... \",format(Sys.time(), \"%Y-%m-%d %H:%M:%S\"),\"\\n\\n\\n\",sep=\"\"));";
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text4 + "</@#;>" + text3;
							}
							break;
						case "JMP-CLOSE WINDOW":
							text3 = General_Procedures.Get_Node_Value(MyNode.Text);
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "No", TextCompare: false) != 0)
								{
									text3 = "::" + text3 + "_" + Globals_Renamed.gSPFCache;
									text = Add_JSL_Title("Close Window", "JMP");
									text = text + "\r\nIF(Is Empty(" + text3 + ") == 0, " + text3 + "<< Close Window();,CAPTION({250,250},\"The window could not be closed as it does not exist. Right-click and choose Close to remove this message\"));";
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3;
							}
							break;
						case "JMP-JOURNAL WINDOW":
							text3 = General_Procedures.Get_Node_Value(MyNode.Text);
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "No", TextCompare: false) != 0)
								{
									text3 = "::" + text3 + "_" + Globals_Renamed.gSPFCache;
									text = Add_JSL_Title("Journal Window", "JMP");
									text += "\r\nClear Globals(::SPFJRN1);";
									text = text + "\r\nIF(Is Empty(" + text3 + ") == 0, ::SPFJRN1 = New Window( \"Report Journal\", <<Journal() ) ; " + text3 + " << Journal() ;,CAPTION({250,250},\"The window does not exist and so could not be written to a journal. Right-click and choose Close to remove this message\"));";
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3;
							}
							break;
						case "JMP-ADD SCRIPT":
							text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "No", TextCompare: false) != 0)
								{
									text = Add_JSL_Title("Run Script", "JMP");
									text = text + "\r\nInclude(\"" + text3 + "\");";
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3;
							}
							break;
						case "JMP-JOURNAL-TO-POWERPOINT":
							text3 = Strings.Trim(MyNode.Nodes[0].Text);
							text4 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Nodes[1].Text));
							text5 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Nodes[2].Text));
							if (MyMode == 0)
							{
								if ((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(text3, "No", TextCompare: false) != 0) & (Operators.CompareString(text4, "No", TextCompare: false) != 0))
								{
									if (Operators.CompareString(text5, "No", TextCompare: false) == 0)
									{
										text5 = "";
									}
									text = Add_JSL_Title("Send Journal to PowerPoint", "JMP");
									text += "\r\nClear Globals(::MyJnl99);";
									text += "\r\n::MyJnl99str = \"_CJ\";";
									text = text + "\r\n::MyJnl99 = " + text3 + ";";
									text += "\r\n::MyBat= ::Default_Dir || \"spf_signal_test_$instance$.bat\";";
									text = text + "\r\n" + BuildSQL.Write_VA_Lib("JSL_lib_4.lib");
									text += "\r\n\r\n//Create and Run BAT File to add images";
									text = ((Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) != 0) ? (text + "\r\n::MyFile = \"va \\!\"\" || ::Default_Dir || \"DOJMP.va\\!\" \\!\"PPT\\!\"  \\!\"" + text5 + "\\!\" \\!\"" + text4 + "\\!\"  \" || \"\\!\"\" || ::MyFolder || \"\\!\"\";") : (text + "\r\n::MyFile = \"\\!\"$py$exe$dir$\\!\" \\!\"\" || ::Default_Dir || \"DOJMP.py\\!\" -m \\!\"PPT\\!\"  -s \\!\"" + Strings.Replace(text5, "\\", "/", 1, 1) + "\\!\" -o \\!\"" + Strings.Replace(text4, "\\", "/", 1, 1) + "\\!\"  -i \\!\"\" || substitute(::MyFolder,\"\\!\\\",\"/\") || \"\\!\"\";"));
									text += "\r\ntry(::MyFile99=load text file (::MyBat),::MyFile99=\"\");";
									text += "\r\nif (::MyFile99 == \"\",::MyFile99=MyFile,::MyFile99 = concat(::MyFile99,\"\\!N\",MyFile));";
									text += "\r\ntry(Save Text file (::MyBat,::MyFile99 ),print(\"Error Saving File Command for Email/JMP Integration...\"));";
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5;
							}
							break;
						case "JMP-END_FOR":
						case "R-END_FOR":
							text = ((MyMode != 0) ? (text2 + text15) : ((Operators.CompareString(left2, "R", TextCompare: false) != 0) ? "));" : "}};"));
							break;
						case "JMP-CLOSE TABLE":
							text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
							if (MyMode == 0)
							{
								if (Operators.CompareString(Strings.UCase(text3), "NO", TextCompare: false) != 0)
								{
									text = Add_JSL_Title("Close a Data Table", "JMP") + "\r\nTry(Close( Data Table (\"" + text3 + "\") , NoSave),Print(\"Warning Only: Table could not be found!\"));";
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3;
							}
							break;
						case "JMP-ADD LAYOUT":
							text3 = General_Procedures.Get_Node_Value(MyNode.Text);
							num3 = Strings.InStr(text3, ":");
							if (num3 != 0)
							{
								text4 = Strings.Trim(Strings.Mid(text3, num3 + 1));
								text3 = Strings.Trim(Strings.Mid(text3, 1, num3 - 1));
							}
							else
							{
								text4 = "1";
							}
							if (Operators.CompareString(text4, "", TextCompare: false) == 0 || Strings.Len(text4) > 1 || ((Operators.CompareString(text4, "1", TextCompare: false) < 0) & (Operators.CompareString(text4, "9", TextCompare: false) > 0)))
							{
								text4 = "1";
							}
							text = ((MyMode != 0) ? (text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4) : (Add_JSL_Title("Create a Layout Object", "JMP") + "\r\n" + text3 + "=LineUpBox(NCol(" + text4 + ") , Spacing (3) );"));
							break;
						case "JMP-APPEND LAYOUT":
							text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
							text4 = Strings.Trim(MyNode.Name);
							if (MyMode == 0)
							{
								if ((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0))
								{
									num3 = Strings.InStr(text4, ":");
									if (num3 != 0)
									{
										text4 = Strings.Trim(Strings.Mid(text4, num3 + 1));
									}
									if (Operators.CompareString(text4, "", TextCompare: false) != 0)
									{
										text = Add_JSL_Title("Append Layout Object", "JMP") + "\r\n" + text3 + "<<Append(" + text4 + ");";
									}
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4;
							}
							break;
						case "JMP-ADD COLUMN":
						case "R-ADD COLUMN":
							text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
							text4 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Nodes[0].Text));
							text5 = MyNode.Name;
							if (MyMode == 0)
							{
								if (Operators.CompareString(Strings.UCase(text3), "NO", TextCompare: false) == 0)
								{
									break;
								}
								num3 = Strings.InStr(text5, ":");
								if (num3 != 0)
								{
									text6 = Strings.Mid(text5, num3 + 1);
									text5 = Strings.Mid(text5, 1, num3 - 1);
								}
								else
								{
									text6 = "";
								}
								if (Operators.CompareString(text6, "", TextCompare: false) != 0)
								{
									text6 = Strings.Replace(text6, "'", "\"", 1, -1, CompareMethod.Text);
								}
								if (Operators.CompareString(text2, "JMP-ADD COLUMN", TextCompare: false) == 0)
								{
									switch (Strings.Trim(Strings.UCase(text5)))
									{
									case "CN":
										text7 = ",Character, Nominal,";
										break;
									case "CO":
										text7 = ",Character, Ordinal,";
										break;
									case "NC":
										text7 = ",Numeric, Continuous,";
										break;
									}
									text = Add_JSL_Title("Create a Computed Column", "JMP");
									text = text + "\r\nTry(::SPFDT=Data Table(\"" + text4 + "\");";
									text = text + "\r\n::SPFdt<<new column(\"" + text3 + "\"" + text7 + " Formula(" + text6 + "),EvalFormula),Print(\"Could not Create Computed Column\"));";
								}
								else
								{
									text = Add_JSL_Title("Create a Computed Column", "R");
									text = text + "\r\n" + text4 + "<- transform(" + text4 + "," + text3 + "=" + text6 + ");";
								}
							}
							else
							{
								text5 = General_Procedures.Quote_CRLF_Replace("E", text5);
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5;
							}
							break;
						case "JMP-CLOSE ALL":
						case "JMP-EXIT JMP":
							if (MyMode == 0)
							{
								if (Operators.CompareString(text2, "JMP-CLOSE ALL", TextCompare: false) != 0)
								{
									if (Operators.CompareString(text2, "JMP-EXIT JMP", TextCompare: false) == 0)
									{
										text = Add_JSL_Title("Exit JMP", "JMP");
										text += "\r\nExit(No Save);";
									}
								}
								else
								{
									text = Add_JSL_Title("Close All Journals and Reports", "JMP");
									text += "\r\nClose All( Journals, No Save);";
									text += "\r\nClose All( Reports, No Save);";
								}
							}
							else
							{
								text = text2 + text15;
							}
							break;
						case "JMP-CLOSE ME":
							if (MyMode == 0)
							{
								text = Add_JSL_Title("Close the Active Window", "JMP");
								text += "\r\nTry(Current Window() << Close Window(NoSave),";
								text += "\r\n    Caption({250, 250},\"Could not close the current window. Right-click and choose Close to remove this message\"));";
							}
							else
							{
								text = text2 + text15;
							}
							break;
						case "JMP-ADD JSL":
						case "R-ADD R-SCRIPT":
							text3 = Strings.Trim(MyNode.Name);
							text4 = MyNode.Nodes[0].Text;
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = text3;
									if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
									{
										text4 = "MyUnknownJSL";
									}
									text = Strings.Replace(text, "<<<chart-object>>>", text4, 1, -1, CompareMethod.Text);
								}
							}
							else
							{
								text3 = General_Procedures.Quote_CRLF_Replace("E", text3);
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4;
							}
							break;
						case "JMP-CREATE WINDOW":
							text3 = Strings.Trim(MyNode.Name);
							text4 = General_Procedures.Get_Node_Value(MyNode.Text);
							text5 = MyNode.Nodes[0].Text;
							if (MyMode == 0)
							{
								text4 = "::" + text4 + "_" + Globals_Renamed.gSPFCache;
								text3 = Generate_Create_Window_JSL(ref MyNode, text3, text5, text4);
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = Add_JSL_Title("Create a Results Window", "JMP") + "\r\n" + text3;
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5;
							}
							break;
						case "HTML-CREATE WINDOW":
							text3 = Strings.Trim(MyNode.Name);
							text4 = General_Procedures.Get_Node_Value(MyNode.Text);
							text5 = MyNode.Nodes[0].Text;
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = Generate_Create_Window_Script(ref MyNode, text3, text5, text4);
								}
								break;
							}
							text5 = General_Procedures.Quote_CRLF_Replace("EQ", text5);
							text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5;
							break;
						case "HTML-CREATE TAB WINDOW":
						case "HTML-CREATE MENU WINDOW":
							text3 = Strings.Trim(MyNode.Name);
							text5 = MyNode.Nodes[0].Text;
							text4 = ((!LikeOperator.LikeString(text2, "*MENU*", CompareMethod.Binary)) ? "TAB" : "MENU");
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = Generate_Create_Tab_Menu_Script(text4, text3, text5);
								}
								break;
							}
							text5 = General_Procedures.Quote_CRLF_Replace("EQ", text5);
							text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5;
							break;
						case "HTML-REPORT":
						case "HTML-REPORTI":
						case "HTML-REPCSS":
							text3 = MyNode.Nodes[0].Text;
							text4 = Strings.Trim(MyNode.Name);
							if (Operators.CompareString(text4, "", TextCompare: false) == 0)
							{
								text4 = BuildSQL.Write_VA_Lib("goldtheme.rss");
							}
							if (MyMode == 0)
							{
								text = text4;
								break;
							}
							text4 = General_Procedures.Quote_CRLF_Replace("EQ", text4);
							text = text2 + text15 + "</@#;>" + text3 + "</@#;>\r\n" + text4 + "\r\nHTML-REPORT-SPEC-END";
							break;
						case "JMP-BUTTON":
						case "R-BUTTON":
						case "HTML-BUTTON":
							if (LikeOperator.LikeString(text2, "JMP*", CompareMethod.Binary))
							{
								left2 = "JMP";
							}
							else
							{
								left2 = "R";
								text19 = "if (!exists(\"default_dir\")) {default_dir <- \"$default$dir$\"};\r\n";
							}
							text3 = Strings.Trim(MyNode.Nodes[0].Text);
							text4 = General_Procedures.Get_Node_Value(MyNode.Text);
							if (Operators.CompareString(Strings.UCase(text4), "BUTTON", TextCompare: false) == 0)
							{
								text4 = "";
							}
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = text16;
								}
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = Get_All_JSL(ref MyNode, MyMode);
									if (Operators.CompareString(Strings.Trim(Strings.Replace(text, "\r\n", "", 1, -1, CompareMethod.Text)), "", TextCompare: false) == 0)
									{
										text = "";
									}
									else if (Operators.CompareString(left2, "JMP", TextCompare: false) == 0)
									{
										text = "::" + Strings.Trim(text3) + "= ButtonBox( \"" + text4 + "\",\r\n" + text;
										text += "\r\n);";
									}
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4;
								text = text + "\r\n" + Get_All_JSL(ref MyNode, MyMode);
								text = text + "\r\n" + left2 + "-END BUTTON";
							}
							break;
						case "HTML-R-PROGRAM":
							text4 = "No";
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = text16;
								}
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									text = Get_All_JSL(ref MyNode, MyMode);
									if (Operators.CompareString(Strings.Trim(Strings.Replace(text, "\r\n", "", 1, -1, CompareMethod.Text)), "", TextCompare: false) == 0)
									{
										text = "";
									}
								}
							}
							else
							{
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4;
								text = text + "\r\n" + Get_All_JSL(ref MyNode, MyMode);
								text += "\r\nR-END PROGRAM";
							}
							break;
						case "JMP-LOAD DATA":
						case "R-LOAD DATA":
							if (LikeOperator.LikeString(text2, "JMP*", CompareMethod.Binary))
							{
								left2 = "JMP";
							}
							else
							{
								left2 = "R";
								text19 = "if (!exists(\"default_dir\")) {default_dir <- \"$default$dir$\"};\r\n";
								text19 += "source(\"$spf$dir$spf_r_lib.r\");\r\n";
							}
							text6 = Strings.Trim(MyNode.Nodes[0].Name);
							text7 = Strings.Trim(Strings.UCase(MyNode.Name));
							if (Operators.CompareString(text2, "JMP-LOAD DATA", TextCompare: false) == 0)
							{
								flag = GetColCase(MyNode, 1);
							}
							if (MyMode == 0)
							{
								text3 = BuildForm.Replace_Globals(General_Procedures.Get_Node_Value(MyNode.Text));
								text9 = text3;
								text10 = General_Procedures.Get_JMP_R_DLM(text3, left2);
								text4 = Strings.Trim(BuildForm.Replace_Globals(MyNode.Nodes[0].Text));
								if (Operators.CompareString(text4, "N/A", TextCompare: false) == 0)
								{
									text4 = "";
								}
								if (Strings.Len(text7) >= 2)
								{
									left = Strings.Mid(text7, 2, 1);
									text7 = Strings.Mid(text7, 1, 1);
								}
								if (Operators.CompareString(text3, "No", TextCompare: false) == 0)
								{
									break;
								}
								num3 = Strings.InStrRev(text3, ".");
								text5 = ((num3 != 0) ? Strings.UCase(Strings.Trim(Strings.Mid(text3 + " ", num3 + 1))) : ((Operators.CompareString(left2, "JMP", TextCompare: false) != 0) ? "RDATA" : "JMP"));
								if ((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0))
								{
									if (Operators.CompareString(left2, "JMP", TextCompare: false) != 0)
									{
										if (Operators.CompareString(left2, "R", TextCompare: false) == 0)
										{
											flag2 = false;
											if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(text3)), "HTTP*://*", CompareMethod.Binary))
											{
												flag2 = true;
											}
											if (Strings.InStrRev(text3, "\\") != 0 || flag2)
											{
												text3 = Strings.Replace(text3, "\\", "/", 1, -1, CompareMethod.Text);
												text3 = text19 + "\r\nmyinfile1<- \"" + Strings.Trim(text3) + "\";";
											}
											else
											{
												text3 = text19 + "\r\nmyinfile1<- paste(default_dir,\"" + Strings.Trim(text3) + "\",sep=\"\");";
											}
											text3 = Add_JSL_Title("Load file to an R Dataframe", left2, "P") + "\r\n" + text3;
											text7 = ((Operators.CompareString(text7, "Y", TextCompare: false) != 0) ? "" : ("suppressWarnings(try(remove(" + text4 + "),silent=TRUE));\r\n"));
											string left3 = Strings.UCase(text5);
											if (Operators.CompareString(left3, "RDATA", TextCompare: false) == 0)
											{
												text = text3 + "\r\n" + text7 + "\r\n" + text4 + " <- readRDS(myinfile1);";
											}
											else
											{
												text = text3 + "\r\n" + text7 + "\r\n";
												if (flag2)
												{
													text += "myexe<-paste(\"$default$exe$\",\"get_web_text.exe\",sep=\"\");";
													text += "\r\nl_myinfile_t <- paste(default_dir,\"r_<<<spf-instance>>>.data\",sep=\"\");";
													text += "\r\nunlink(l_myinfile_t, recursive = FALSE, force = TRUE);";
													text += "\r\nsystem(paste('\"',myexe,'\" /from=',myinfile1, \" /to=\", l_myinfile_t,\" /binary=Y /quiet=Y\",sep=\"\"));";
													text = text + "\r\n" + text4 + " <- read.csv(l_myinfile_t, fileEncoding = \"UTF-8-BOM\", header = TRUE, sep = \"" + text10 + "\" , quote=\"\\\"\", comment.char=\"\",dec=\".\",stringsAsFactor=FALSE,na.strings=c(\".\",\"\",\"\\\\N\")";
												}
												else
												{
													text = text + text4 + " <- read.csv(myinfile1, fileEncoding = \"UTF-8-BOM\", header = TRUE, sep = \"" + text10 + "\" , quote=\"\\\"\", comment.char=\"\",dec=\".\",stringsAsFactor=FALSE,na.strings=c(\".\",\"\",\"\\\\N\")";
												}
												if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
												{
													text += "\r\n<!!INSERTDT!!>";
												}
												text += ");";
												if (flag2)
												{
													text += "\r\nunlink(l_myinfile_t, recursive = FALSE, force = TRUE);";
												}
												text8 = "";
												text11 = "";
												if (Operators.CompareString(text6, "", TextCompare: false) != 0)
												{
													string[] array = Strings.Split(text6, ",");
													int num5 = Information.UBound(array);
													for (num4 = 0; num4 <= num5; num4++)
													{
														if (Operators.CompareString(text8, "", TextCompare: false) != 0)
														{
															text11 = "\r\n,";
														}
														num3 = Strings.InStr(array[num4], " (");
														if (num3 == 0)
														{
															continue;
														}
														text14 = Strings.Trim(Strings.Mid(array[num4], 1, num3 - 1));
														text12 = Strings.Mid(array[num4], num3 + 2, 1);
														text13 = "";
														switch (text12)
														{
														case "C":
															if (Operators.CompareString(left, "N", TextCompare: false) == 0)
															{
																text12 = text4 + "$" + text14 + "<-factor(" + text4 + "$" + text14 + ");";
																text = text + "\r\n" + text12;
															}
															else
															{
																text8 = text8 + text11 + "'character'";
															}
															break;
														case "N":
															if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
															{
																text8 = text8 + text11 + "'numeric'";
															}
															break;
														case "I":
															if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
															{
																text8 = text8 + text11 + "'integer'";
															}
															break;
														case "D":
															text12 = text4 + "$" + text14 + " <- as.POSIXct(" + text4 + "$" + text14 + ",format=\"%Y-%m-%d %H:%M:%S\");";
															text = text + "\r\n" + text12;
															if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
															{
																text8 = text8 + text11 + "'character'";
															}
															break;
														}
													}
													array = null;
												}
												if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
												{
													if (Operators.CompareString(text8, "", TextCompare: false) != 0)
													{
														text8 = ",colClasses=c(" + text8 + "\r\n)";
													}
													text = Strings.Replace(text, "<!!INSERTDT!!>", text8, 1, -1, CompareMethod.Text);
												}
												if (GetColCase(MyNode, 2))
												{
													text = text + "\r\ncolnames(" + text4 + ")<-tolower(colnames(" + text4 + "));";
												}
											}
											text = text + "\r\ncat(paste(\"  Data loaded to DataFrame: " + text4 + " ... \",format(Sys.time(), \"%Y-%m-%d %H:%M:%S\"),\"\\n\\n\\n\",sep=\"\"));";
										}
									}
									else
									{
										text3 = ((!((Strings.InStrRev(text3, "\\") != 0) | LikeOperator.LikeString(Strings.UCase(Strings.Trim(text3)), "HTTP*://*", CompareMethod.Binary))) ? (text19 + "::MyInFile1= ::Default_Dir || \"" + Strings.Trim(text3) + "\";") : ("::MyInFile1= \"" + Strings.Trim(text3) + "\";"));
										text7 = ((Operators.CompareString(text7, "Y", TextCompare: false) != 0) ? "" : ("Try(Close( Data Table (\"" + text4 + "\") , NoSave);,Print(\"Warning Only: Table could not be found!\"));\r\n"));
										text3 = Add_JSL_Title("Load file to a JMP Table table", left2) + "\r\n" + text3;
										if (Operators.CompareString(text5, "JMP", TextCompare: false) == 0)
										{
											text = text3 + "\r\n" + text7 + "\r\n::myspfcsv=Open(::MyInFile1);";
										}
										else
										{
											text = text3 + "\r\n" + text7 + "\r\n::myspfcsv= Open(::MyInFile1,";
											if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
											{
												text += "\r\ncolumns(<!!INSERTDT!!>),";
											}
											text += "\r\nImport Settings(";
											text += "\r\n   End Of Line( CRLF, CR, LF ),";
											text = text + "\r\n   End Of Field( " + text10 + " ),";
											text += "\r\n   Strip Quotes( 1 ),";
											text += "\r\n   Use Apostrophe as Quotation Mark( 0 ),";
											text += "\r\n   Labels( 1 ),";
											text += "\r\n   Column Names Start( 1 ),";
											text += "\r\n   Data Starts( 2 ),";
											text += "\r\n   Lines To Read( All ),";
											text += "\r\n   Year Rule( \"10-90\" )";
											text += "\r\n   ));";
											text += "\r\nWait(0.1);";
											if (flag)
											{
												text += "\r\n/*Set Columns Lowercase*/\r\nFor(j = 1, j <= NCol(myspfcsv), j++,";
												text += "\r\n  col = Column(j); nm = lowercase(char(column name(j)));";
												text += "\r\n  col << Set Name(nm);\r\n);";
											}
											if (Operators.CompareString(text6, "", TextCompare: false) != 0)
											{
												text += "\r\n::myspfcsv<< Current Data Table();";
												string[] array = Strings.Split(text6, ",");
												text8 = "";
												int num6 = Information.UBound(array);
												for (num4 = 0; num4 <= num6; num4++)
												{
													num3 = Strings.InStr(array[num4], " (");
													if (num3 == 0)
													{
														continue;
													}
													text14 = Strings.Trim(Strings.Mid(array[num4], 1, num3 - 1));
													text12 = Strings.Mid(array[num4], num3 + 2, 1);
													text13 = Strings.Mid(array[num4], num3 + 3, 1);
													if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
													{
														text8 = text8 + "\r\nColumn(\"" + text14 + "\"";
														switch (text12)
														{
														case "C":
															text12 = ",Character";
															break;
														case "N":
															text12 = ",Numeric";
															break;
														case "S":
															text12 = ",Numeric";
															text13 = "S";
															break;
														}
														text8 = text8 + text12 + text13 switch
														{
															"O" => ",Ordinal", 
															"N" => ",Nominal", 
															"C" => ",Continuous", 
															"S" => ",Continuous, Format(\"y/m/d h:m:s\", 22, 0 ),Input Format( \"y/m/d h:m:s\", 0 )", 
															_ => "", 
														} + "),";
														continue;
													}
													text14 = "column (\"" + text14 + "\")";
													switch (text12)
													{
													case "C":
														text12 = " << Data Type(Character)";
														break;
													case "N":
														text12 = " << Data Type (Numeric)";
														break;
													case "S":
														text12 = "";
														text13 = "S";
														break;
													}
													text13 = text13 switch
													{
														"O" => " << Set Modeling Type(\"Ordinal\");", 
														"N" => " << Set Modeling Type(\"Nominal\");", 
														"C" => " << Set Modeling Type(\"Continuous\");", 
														"S" => ";", 
														_ => ";", 
													};
													text = text + "\r\n" + text14 + text12 + text13;
												}
												array = null;
											}
											if (Operators.CompareString(left, "Y", TextCompare: false) == 0)
											{
												text = Strings.Replace(text, "<!!INSERTDT!!>", text8, 1, -1, CompareMethod.Text);
											}
										}
									}
								}
								if (Operators.CompareString(left2, "JMP", TextCompare: false) == 0)
								{
									text = text + "\r\n::myspfcsv << Set Name(\"" + text4 + "\");";
								}
							}
							else
							{
								text3 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Text));
								text4 = Strings.Trim(MyNode.Nodes[0].Text);
								if (Operators.CompareString(text4, "N/A", TextCompare: false) == 0)
								{
									text4 = "";
								}
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text6 + "</@#;>" + text7 + "</@#;>" + text4;
							}
							break;
						case "JMP-CHART":
						case "JMP-NEWTABLE":
							text3 = MyNode.Nodes[0].Text;
							text4 = Strings.Trim(MyNode.Nodes[1].Text);
							text4 = General_Procedures.Get_Node_Value(text4);
							text5 = Strings.Trim(MyNode.Nodes[0].Name);
							text6 = Strings.Trim(MyNode.Name);
							text7 = ((Operators.CompareString(text2, "JMP-CHART", TextCompare: false) != 0) ? "" : General_Procedures.Get_Node_Value(MyNode.Nodes[2].Text));
							if (MyMode == 0)
							{
								if (!unchecked(Operators.CompareString(text7, "No", TextCompare: false) != 0 || IsSingle))
								{
									break;
								}
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = text16;
								}
								if ((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0))
								{
									text = Add_JSL_Title("Generate a Chart or Perform a Table Manipulation", "JMP") + "\r\n" + Generate_Chart(text3, text5, text4, text6, "", flag);
									text9 = "Current Data Table (Data Table(\"" + text4 + "\"));\r\n";
									if (IsSingle)
									{
										text9 = text9 + "Data Table(\"" + text4 + "\") << Clear Row States;\r\n";
									}
									text = text9 + text;
								}
							}
							else
							{
								text6 = General_Procedures.Quote_CRLF_Replace("E", text6);
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5 + "</@#;>" + text6 + "</@#;>" + text7;
							}
							break;
						case "HTML-CHART":
						case "HTML-CHARTR":
						case "HTML-CHARTP":
						case "R-CHART":
						case "HTML-CHARTI":
							text3 = MyNode.Nodes[0].Text;
							text4 = Strings.Trim(General_Procedures.Get_Node_Value(MyNode.Nodes[1].Text));
							text5 = Strings.Trim(MyNode.Nodes[0].Name);
							text6 = Strings.Trim(MyNode.Name);
							text7 = General_Procedures.Get_Node_Value(MyNode.Nodes[2].Text);
							if (MyMode == 0)
							{
								text4 = Strings.Trim(BuildForm.Replace_Globals(text4));
								text6 = Strings.Trim(BuildForm.Replace_Globals(text6));
								if (!unchecked(Operators.CompareString(text7, "No", TextCompare: false) != 0 || IsSingle))
								{
									break;
								}
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = text16;
								}
								if (!((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0)))
								{
									break;
								}
								if ((Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) == 0) | (Operators.CompareString(text2, "R-CHART", TextCompare: false) == 0))
								{
									text20 = "R";
									if (Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) == 0)
									{
										text4 = "csv1";
									}
									if (Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) == 0)
									{
										flag = GetColCase(MyNode, 1);
									}
									else if (Operators.CompareString(text2, "R-CHART", TextCompare: false) == 0)
									{
										flag = GetColCase(MyNode, 2);
									}
								}
								else if (Operators.CompareString(text2, "HTML-CHARTP", TextCompare: false) != 0)
								{
									text20 = ((Operators.CompareString(text2, "HTML-CHARTI", TextCompare: false) != 0) ? "" : "JS");
								}
								else
								{
									text20 = "PY";
									text4 = "csv1";
									flag = GetColCase(MyNode, 1);
								}
								text = Generate_Chart(text3, text5, text4, text6, text20, flag, IsSingle);
								if (IsSingle && (Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) == 0 || Operators.CompareString(text2, "R-CHART", TextCompare: false) == 0))
								{
									text += "\r\ntry(shell.exec(\"<<<chart-out-file>>>.htm\"),silent=TRUE);";
								}
								text9 = Get_Chart_Out_File(ref MyNode, text20);
								text = ((Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) != 0 && Operators.CompareString(text2, "R-CHART", TextCompare: false) != 0 && Operators.CompareString(text2, "HTML-CHARTP", TextCompare: false) != 0) ? Strings.Replace(text, "<<<chart-out-file>>>", text9, 1, -1, CompareMethod.Text) : Strings.Replace(text, "<<<chart-out-file>>>", text9 + "___", 1, -1, CompareMethod.Text));
								if (Operators.CompareString(text2, "HTML-CHARTR", TextCompare: false) == 0)
								{
									text = Strings.Replace(text, "@SPF-R-SOURCE@", "csv", 1, -1, CompareMethod.Text);
								}
								else if (Operators.CompareString(text2, "R-CHART", TextCompare: false) == 0)
								{
									text = Strings.Replace(text, "@SPF-R-SOURCE@", "r-dataframe", 1, -1, CompareMethod.Text);
								}
							}
							else
							{
								text6 = General_Procedures.Quote_CRLF_Replace("E", text6);
								text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5 + "</@#;>" + text6 + "</@#;>" + text7;
							}
							break;
						case "JMP-NOTABLE":
						case "R-NOTABLE":
							text3 = "";
							text4 = Strings.Trim(MyNode.Nodes[0].Text);
							text4 = General_Procedures.Get_Node_Value(text4);
							text5 = Strings.Trim(MyNode.Nodes[0].Name);
							text6 = Strings.Trim(MyNode.Name);
							if (MyMode == 0)
							{
								if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0)
								{
									text = Generate_Chart("", text5, text4, text6, "", flag);
								}
								break;
							}
							text6 = General_Procedures.Quote_CRLF_Replace("E", text6);
							text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5 + "</@#;>" + text6;
							break;
						case "JMP-CHART2":
							text3 = MyNode.Nodes[0].Text;
							text4 = "";
							text5 = Strings.Trim(MyNode.Nodes[0].Name);
							text6 = Strings.Trim(MyNode.Name);
							if (MyMode == 0)
							{
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									text3 = text16;
								}
								if ((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0))
								{
									text = Generate_Chart(text3, text5, text4, text6, "", flag);
								}
								break;
							}
							text6 = General_Procedures.Quote_CRLF_Replace("E", text6);
							text = text2 + text15 + "</@#;>" + text3 + "</@#;>" + text4 + "</@#;>" + text5 + "</@#;>" + text6;
							break;
						}
						goto end_IL_0001_2;
					}
					case 12894:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_3294;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 12894;
				continue;
			}
			break;
			IL_3294:
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0001:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public static string Get_All_JSL(ref TreeNode MyNode, int MyMode, bool RunImmediate = false, string MyPromptTxt = "", int AddDelete = 0)
	{
		int try0001_dispatch = -1;
		string result = default(string);
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
					result = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildChart - GET_All_JSL";
					string text = "";
					bool flag = true;
					if (MyNode == null)
					{
						goto end_IL_0001;
					}
					string text2 = "";
					string text3 = "";
					string text4 = "";
					int num3 = 0;
					int num4 = 0;
					string text5 = "";
					bool flag2 = false;
					string text6 = "N";
					string text7 = Globals_Renamed.gSPFCache;
					IEnumerator enumerator = MyNode.Nodes.GetEnumerator();
					while (enumerator.MoveNext())
					{
						TreeNode treeNode = (TreeNode)enumerator.Current;
						if (!Conversions.ToBoolean(LikeOperator.LikeObject(treeNode.Tag, "SCRIPT:*", CompareMethod.Binary)))
						{
							continue;
						}
						text = General_Procedures.Get_Chart_Script_Type(Conversions.ToString(treeNode.Tag), 1);
						if (AddDelete == 1 && !flag2 && Operators.CompareString(text, "HTML", TextCompare: false) == 0)
						{
							flag2 = true;
						}
						if (Conversions.ToBoolean(Operators.CompareString(text, "HTML", TextCompare: false) == 0 && Conversions.ToBoolean(Operators.NotObject(LikeOperator.LikeObject(treeNode.Tag, "SCRIPT:HTML:BUTTON*", CompareMethod.Binary)))))
						{
							text6 = Strings.Trim(General_Procedures.Get_Node_Value(treeNode.Parent.Nodes[3].Text));
							text6 = ((Operators.CompareString(Strings.UCase(text6), "YES", TextCompare: false) != 0) ? "N" : "Y");
							text7 = Strings.Trim(General_Procedures.Get_Node_Value(treeNode.Parent.Nodes[4].Text));
							if (Operators.CompareString(text7, "", TextCompare: false) == 0)
							{
								text7 = Globals_Renamed.gSPFCache;
							}
						}
						IEnumerator enumerator2 = treeNode.Nodes.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							TreeNode MyNode2 = (TreeNode)enumerator2.Current;
							if (Operators.CompareString(text, "HTML", TextCompare: false) == 0 && MyMode == 0)
							{
								checked
								{
									if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-REPORT*", CompareMethod.Binary)))
									{
										text4 = "";
										text4 = Conversions.ToString(MyNode2.Tag);
										num3 = Strings.InStr(text4, ":");
										if (num3 != 0)
										{
											text4 = Strings.Trim(Strings.Mid(text4 + " ", num3 + 1));
										}
										text5 = "/INSTANCE=" + Globals_Renamed.gSPFCache;
										if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-REPORTI*", CompareMethod.Binary)))
										{
											text5 = text5 + "\r\n/JSON-ONLY=" + text6;
											text5 = text5 + "\r\n/CHART-INSTANCE=" + text7;
										}
										text5 = text5 + "\r\n/ID=" + text4 + "\r\n/REPORT=HTML-";
										text5 = ((!RunImmediate) ? (text5 + "DEFER") : (text5 + "RUN"));
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-REPCSS*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-RUN\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CREATE WINDOW*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-LAYOUT\r\n/OUTLOOK=<<<spf-email-type>>>\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
										text5 = text5 + "\r\n/JSON-ONLY=" + text6;
										text5 = text5 + "\r\n/CHART-INSTANCE=" + text7;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CREATE TAB WINDOW*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-TAB-LAYOUT\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
										text5 = text5 + "\r\n/JSON-ONLY=" + text6;
										text5 = text5 + "\r\n/CHART-INSTANCE=" + text7;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CREATE MENU WINDOW*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-MENU-LAYOUT\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
										text5 = text5 + "\r\n/JSON-ONLY=" + text6;
										text5 = text5 + "\r\n/CHART-INSTANCE=" + text7;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CHARTR*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-RPLOT\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CHARTP*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-PYPLOT\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CHARTI*", CompareMethod.Binary)))
									{
										text4 = "";
										text4 = Conversions.ToString(MyNode2.Tag);
										num3 = Strings.InStr(text4, ":");
										if (num3 != 0)
										{
											text4 = Strings.Trim(Strings.Mid(text4 + " ", num3 + 1));
											num4 = Strings.InStrRev(Strings.UCase(text4), ".GIF");
											if (num4 != 0)
											{
												text4 = Strings.Trim(Strings.Mid(text4, 1, num4 - 1));
											}
										}
										text5 = "/INSTANCE=" + Globals_Renamed.gSPFCache;
										text5 = text5 + "\r\n/JSON-ONLY=" + text6;
										text5 = text5 + "\r\n/CHART-INSTANCE=" + text7;
										text5 = text5 + "\r\n/ID=" + text4 + "\r\n/REPORT=HTML-JS-";
										text5 = ((!RunImmediate) ? (text5 + "DEFER") : (text5 + "RUN"));
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-CHART*", CompareMethod.Binary)))
									{
										text5 = "/REPORT=HTML-GNUPLOT\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
									}
									else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyNode2.Tag, "HTML-R-PROGRAM*", CompareMethod.Binary)))
									{
										text5 = "/RSCRIPT=Y\r\n/INSTANCE=" + Globals_Renamed.gSPFCache;
									}
									if (flag)
									{
										if (Operators.CompareString(MyPromptTxt, "", TextCompare: false) != 0)
										{
											text5 = text5 + "\r\n/PROMPT-TEXT=" + MyPromptTxt;
										}
										flag = false;
									}
									text2 = "\r\n<---- New Query ---->\r\n<OPTIONS>\r\n" + text5 + "\r\n/APP_SERVER_DEFAULT=<<<SPF-APP-SERVER>>>\r\n</OPTIONS>\r\n";
									text3 = text3 + text2 + Get_JSL_Script(ref MyNode2, MyMode);
								}
							}
							else
							{
								text3 = text3 + text2 + Get_JSL_Script(ref MyNode2, MyMode);
								text2 = "\r\n\r\n";
							}
						}
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
						break;
					}
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					switch (MyMode)
					{
					case 0:
						result = text3;
						if (flag2)
						{
							result = text3 + "\r\n<---- New Query ---->\r\n<OPTIONS>\r\n/REPORT=HTML-DELETE\r\n/INSTANCE=" + Globals_Renamed.gSPFCache + "\r\n</OPTIONS>\r\nN/A\r\n";
						}
						break;
					case 1:
						result = text + "-SCRIPT\r\n" + text3;
						break;
					}
					goto end_IL_0001_2;
				}
				case 1870:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_0784;
				end_IL_0001_2:;
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
			continue;
			end_IL_0001:
			break;
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Derive_Chart_Table(ref string InFile)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
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
				case 471:
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
							goto IL_0019;
						case 5:
							goto IL_0025;
						case 6:
							goto IL_005d;
						case 7:
							goto IL_0065;
						case 9:
							goto IL_0075;
						case 10:
							goto IL_0090;
						case 12:
							goto IL_00a7;
						case 11:
						case 13:
						case 14:
							goto IL_00bb;
						case 15:
							goto IL_00c9;
						case 17:
							goto IL_00d3;
						case 16:
						case 18:
						case 19:
							goto IL_00e3;
						case 20:
							goto IL_00fa;
						case 21:
							goto IL_013b;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 23:
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00e3:
					num2 = 19;
					left = Strings.Mid(text + " ", 1, 1);
					goto IL_00fa;
					IL_00fa:
					num2 = 20;
					if (!(((Operators.CompareString(left, "0", TextCompare: false) >= 0) & (Operators.CompareString(left, "9", TextCompare: false) <= 0)) | (Operators.CompareString(left, "_", TextCompare: false) == 0)))
					{
						break;
					}
					goto IL_013b;
					IL_00d3:
					num2 = 17;
					text = Strings.Mid(InFile, checked(num5 + 1));
					goto IL_00e3;
					IL_013b:
					num2 = 21;
					text = "T_" + text;
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					left = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					InFile = BuildForm.Replace_Globals(InFile);
					goto IL_0025;
					IL_0025:
					num2 = 5;
					if ((Operators.CompareString(Strings.Trim(InFile), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(InFile)), "NO", TextCompare: false) == 0))
					{
						goto IL_005d;
					}
					goto IL_0075;
					IL_005d:
					num2 = 6;
					text = "N/A";
					goto IL_0065;
					IL_0065:
					num2 = 7;
					InFile = "No";
					goto end_IL_0001_3;
					IL_0075:
					num2 = 9;
					if (LikeOperator.LikeString(Strings.UCase(InFile), "HTTP*//*", CompareMethod.Binary))
					{
						goto IL_0090;
					}
					goto IL_00a7;
					IL_0090:
					num2 = 10;
					num5 = Strings.InStrRev(InFile, "/");
					goto IL_00bb;
					IL_00a7:
					num2 = 12;
					num5 = Strings.InStrRev(InFile, "\\");
					goto IL_00bb;
					IL_00bb:
					num2 = 14;
					if (num5 == 0)
					{
						goto IL_00c9;
					}
					goto IL_00d3;
					IL_00c9:
					num2 = 15;
					text = InFile;
					goto IL_00e3;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				text = Strings.Replace(text, ".", "_", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 471;
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

	public static string Save_Chart_To_Node()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string result = default(string);
		int g_NoStdCtrls = default(int);
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
					case 572:
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
								goto IL_0021;
							case 6:
								goto IL_0032;
							case 7:
								goto IL_0099;
							case 8:
							case 9:
								goto IL_00ec;
							case 10:
								goto IL_00fe;
							case 11:
								goto IL_0104;
							case 12:
								goto IL_014c;
							case 13:
							case 14:
								goto IL_0196;
							case 15:
								goto IL_01a8;
							case 16:
								goto IL_01c1;
							case 17:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 18:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0032:
						num2 = 6;
						if ((Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Type, "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Value, "", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Value, "N/A", TextCompare: false) != 0))
						{
							goto IL_0099;
						}
						goto IL_00ec;
						IL_0099:
						num2 = 7;
						text = text + "<\\\\>" + Strings.Trim(Globals_Renamed.g_StdCtrls[num5].ID) + "=" + Globals_Renamed.g_StdCtrls[num5].Value;
						goto IL_00ec;
						IL_01c1:
						num2 = 16;
						text = Strings.Mid(text, Strings.Len("<\\\\>") + 1);
						break;
						IL_00ec:
						num2 = 9;
						num5++;
						goto IL_00f5;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						text = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						num5 = 0;
						goto IL_0021;
						IL_0021:
						num2 = 5;
						g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
						num5 = 0;
						goto IL_00f5;
						IL_00f5:
						if (num5 <= g_NoStdCtrls)
						{
							goto IL_0032;
						}
						goto IL_00fe;
						IL_00fe:
						num2 = 10;
						num5 = 0;
						goto IL_0104;
						IL_0104:
						num2 = 11;
						if ((Operators.CompareString(Globals_Renamed.gOptOptions[num5].Selected, "N/A", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.gOptOptions[num5].Selected, "", TextCompare: false) != 0))
						{
							goto IL_014c;
						}
						goto IL_0196;
						IL_014c:
						num2 = 12;
						text = text + "<\\\\>OPT" + Conversions.ToString(num5) + "=" + Strings.Trim(Globals_Renamed.gOptOptions[num5].Selected);
						goto IL_0196;
						IL_0196:
						num2 = 14;
						num5++;
						if (num5 <= 25)
						{
							goto IL_0104;
						}
						goto IL_01a8;
						IL_01a8:
						num2 = 15;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_01c1;
						end_IL_0001_2:
						break;
					}
					num2 = 17;
					result = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 572;
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

	public static string Generate_Chart(string ChartVar, string IniFile, string InTable, string ChartDef, string OverideTemplateType, bool l_ColCaseInsensitive, bool IsSIngle = false)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
		string MyScript = default(string);
		string MyTemplateType = default(string);
		string result = default(string);
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
				case 200:
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
							goto IL_0018;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0039;
						case 9:
							goto IL_0048;
						case 10:
							goto IL_0061;
						case 11:
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0039:
					num2 = 7;
					if (!flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_0048;
					IL_0048:
					num2 = 9;
					if (Operators.CompareString(OverideTemplateType, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0061;
					IL_002a:
					num2 = 6;
					flag = Update_Chart_Variables(IniFile, ChartDef, ref MyScript, ref MyTemplateType);
					goto IL_0039;
					IL_0061:
					num2 = 10;
					OverideTemplateType = MyTemplateType;
					break;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					flag = false;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					MyScript = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					MyTemplateType = "";
					goto IL_002a;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = Substitute_Script(MyScript, InTable, ChartVar, OverideTemplateType, IniFile, l_ColCaseInsensitive, IsSIngle);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 200;
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

	public static bool Update_Chart_Variables(string IniFile, string ChartDef, ref string MyScript, ref string MyTemplateType)
	{
		bool result = false;
		int num = 0;
		int num2 = 0;
		bool flag = false;
		string text = "";
		string text2 = "";
		int num3 = 0;
		int num4 = 0;
		string text3 = "";
		MyScript = "";
		string[] array;
		checked
		{
			if (Operators.CompareString(ChartDef, "", TextCompare: false) != 0)
			{
				array = Strings.Split(ChartDef, "<\\\\>");
				IniFile = Load_Chart_Ini_File(IniFile, ref MyTemplateType, ref MyScript);
				if (Operators.CompareString(IniFile, "", TextCompare: false) == 0 || !Init_Chart_Variables() || !Load_Chart_Ini(IniFile))
				{
					goto IL_01fb;
				}
				int num5 = Information.UBound(array);
				num = 0;
				while (num <= num5)
				{
					text = array[num];
					num4 = Strings.InStr(text, "=");
					if (num4 != 0)
					{
						text2 = Strings.Mid(text, num4);
						text2 = ((Operators.CompareString(text2, "=", TextCompare: false) != 0) ? Strings.Mid(text2, 2) : "");
						text = Strings.UCase(Strings.Trim(Strings.Mid(text, 1, num4 - 1)));
						if (Operators.CompareString(Strings.Trim(Strings.Mid(text + "   ", 1, 3)), "OPT", TextCompare: false) == 0)
						{
							num3 = Conversions.ToInteger(Strings.Mid(text, 4));
							Globals_Renamed.gOptOptions[num3].Selected = text2;
						}
						else
						{
							int g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
							for (num2 = 0; num2 <= g_NoStdCtrls; num2++)
							{
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].ID, text, TextCompare: false) == 0)
								{
									Globals_Renamed.g_StdCtrls[num2].Value = text2;
									break;
								}
							}
						}
						num++;
						continue;
					}
					goto IL_01c6;
				}
				result = true;
			}
			goto IL_01fe;
		}
		IL_01c6:
		text3 = "Chart Definition may be corrupted. Cannot Continue";
		Interaction.MsgBox(text3, MsgBoxStyle.Exclamation, "Chart Error");
		MyScript = "";
		result = false;
		goto IL_01fb;
		IL_01fe:
		return result;
		IL_01fb:
		array = null;
		goto IL_01fe;
	}

	public static bool Init_Chart_Variables()
	{
		int try0001_dispatch = -1;
		bool result = default(bool);
		string MyMsg = default(string);
		string text = default(string);
		int num2 = default(int);
		object MyReader = default(object);
		bool flag = default(bool);
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
						result = false;
						MyMsg = "";
						text = "BuildChart - Init_Chart_Variables";
						ProjectData.ClearProjectError();
						num2 = 2;
						int num3 = 0;
						MyReader = null;
						flag = false;
						string[] array = null;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						num3 = 0;
						do
						{
							Globals_Renamed.g_StdCtrls[num3].ID = "";
							Globals_Renamed.g_StdCtrls[num3].Type = "";
							Globals_Renamed.g_StdCtrls[num3].Token = "";
							Globals_Renamed.g_StdCtrls[num3].Value = "";
							Globals_Renamed.g_StdCtrls[num3].Default_val = "";
							Globals_Renamed.g_StdCtrls[num3].JSL = "";
							Globals_Renamed.g_StdCtrls[num3].Arg1 = "";
							Globals_Renamed.g_StdCtrls[num3].Map = "";
							num3++;
						}
						while (num3 <= 124);
						Globals_Renamed.g_NoStdCtrls = -1;
						flag = BuildForm.OpenDelimitedFile(Globals_Renamed.MySchemaDir + "\\Chart_Controls.dat", ref MyReader, ref MyMsg);
						if (!flag)
						{
							break;
						}
						num3 = 0;
						while (true)
						{
							if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
							{
								array = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
								if (!BuildForm.VerifySchemaRow(ref array, 9, ref MyMsg))
								{
									goto end_IL_0001;
								}
								Globals_Renamed.g_StdCtrls[num3].ID = array[0];
								Globals_Renamed.g_StdCtrls[num3].Type = array[1];
								Globals_Renamed.g_StdCtrls[num3].Token = array[2];
								Globals_Renamed.g_StdCtrls[num3].Value = array[3];
								Globals_Renamed.g_StdCtrls[num3].Default_val = array[4];
								Globals_Renamed.g_StdCtrls[num3].JSL = array[5];
								Globals_Renamed.g_StdCtrls[num3].Arg1 = array[6];
								Globals_Renamed.g_StdCtrls[num3].Map = array[7];
								text4 = array[8];
								text2 = Globals_Renamed.g_StdCtrls[num3].ID;
								text3 = Strings.Mid(text2, 1, 1);
								if (Operators.CompareString(text3, "!", TextCompare: false) != 0)
								{
									num3++;
								}
								else
								{
									Globals_Renamed.g_StdCtrls[num3].Token = Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Token));
								}
								array = null;
								continue;
							}
							Globals_Renamed.g_NoStdCtrls = (short)(num3 - 1);
							if (flag)
							{
								NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
								NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
								flag = false;
							}
							array = null;
							int g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
							for (num3 = 0; num3 <= g_NoStdCtrls; num3++)
							{
								Globals_Renamed.g_StdCtrls[num3].Value = Globals_Renamed.g_StdCtrls[num3].Default_val;
							}
							num3 = 1;
							do
							{
								Globals_Renamed.gOptOptions[num3 - 1].Variable = "N/A";
								Globals_Renamed.gOptOptions[num3 - 1].JSL = "";
								Globals_Renamed.gOptOptions[num3 - 1].Label = "";
								Globals_Renamed.gOptOptions[num3 - 1].Show = "";
								Globals_Renamed.gOptOptions[num3 - 1].Default_value = "";
								Globals_Renamed.gOptOptions[num3 - 1].Return_Value = "";
								Globals_Renamed.gOptOptions[num3 - 1].Help = "";
								Globals_Renamed.gOptOptions[num3 - 1].Selected = "N/A";
								num3++;
							}
							while (num3 <= 25);
							result = true;
							break;
						}
						goto end_IL_0001_2;
					}
					case 1184:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								break;
							default:
								goto end_IL_0001_3;
							}
							break;
						}
						end_IL_0001:
						break;
					}
					if (Operators.CompareString(MyMsg, "", TextCompare: false) == 0)
					{
						MyMsg = Information.Err().Description;
					}
					Interaction.MsgBox(text + "- Error loading Chart Objects (" + MyMsg + ")", MsgBoxStyle.Critical, "Load Error");
					if (flag)
					{
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						flag = false;
					}
					Information.Err().Clear();
					break;
				}
				end_IL_0001_3:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1184;
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
		return result;
	}

	private static string Prep_Special_Tokens(string MyTemplateType, int NoReplaces, string MyToken, string MyOutput, string MyJSL, string IsChartType)
	{
		string result = "";
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		string text8 = "";
		int num = 0;
		int num2 = 0;
		bool flag = false;
		text = Strings.Trim(MyOutput);
		if (Operators.CompareString(Strings.Replace(text, ";", "", 1, -1, CompareMethod.Text), "", TextCompare: false) == 0)
		{
			flag = true;
		}
		string[] array;
		if ((Operators.CompareString(MyToken, "N/A", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(MyToken), "", TextCompare: false) != 0))
		{
			text = MyOutput;
			if (Operators.CompareString(text, "", TextCompare: false) != 0 && !flag)
			{
				array = Strings.Split(MyOutput, ";");
				text8 = "";
				int num3 = Information.UBound(array);
				checked
				{
					for (num = 0; ((NoReplaces >> 31) ^ num) <= ((NoReplaces >> 31) ^ num3); num += NoReplaces)
					{
						if (Operators.CompareString(MyToken, "ROW-LEGEND", TextCompare: false) == 0)
						{
							text2 = array[num];
							text3 = Strings.Trim(array[num + 1]);
							if (Operators.CompareString(text3, "1", TextCompare: false) == 0)
							{
								text4 = "JMP Default";
							}
							else
							{
								text3 = "0";
								text4 = "";
							}
							text5 = Strings.Trim(array[num + 2]);
							if (Operators.CompareString(text5, "1", TextCompare: false) == 0)
							{
								text6 = "Standard";
							}
							else
							{
								text5 = "0";
								text6 = "";
							}
						}
						else if (LikeOperator.LikeString(MyToken, "*-AXIS-REF-LINE*", CompareMethod.Binary))
						{
							text2 = array[num];
							text3 = array[num + 1];
							text4 = array[num + 2];
							text5 = array[num + 3];
						}
						if (Operators.CompareString(MyToken, "ROW-LEGEND", TextCompare: false) == 0)
						{
							text7 = ((Operators.CompareString(text2, "", TextCompare: false) == 0) ? "" : ("Row Legend(:Name(\"" + text2 + "\"),Color( " + text3 + " ),Color Theme( \"" + text4 + "\" ),Marker( " + text5 + " ),Marker Theme( \"" + text6 + "\" ),)"));
						}
						else if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) != 0)
							{
								text7 = ((Operators.CompareString(MyTemplateType, "PY", TextCompare: false) != 0) ? ("Add Ref Line( " + text2 + ", \"" + text3 + "\", \"" + text4 + "\", \"" + text5 + "\" )") : (text2 + ";" + text3 + ";" + text4 + ";" + text5));
							}
							else if (LikeOperator.LikeString(MyToken, "Y*", CompareMethod.Binary))
							{
								text7 = " panel.abline(h=" + text2 + ", lty=\"" + Strings.LCase(text3) + "\", col=\"" + Strings.LCase(text4) + "\", lwd=1)";
								text7 = ((Operators.CompareString(IsChartType, "RXY", TextCompare: false) != 0 && Operators.CompareString(IsChartType, "RQQ", TextCompare: false) != 0 && Operators.CompareString(IsChartType, "RDENSITY", TextCompare: false) != 0) ? (text7 + "\r\n panel.text(.5," + text2 + ",\"" + text5 + "\",adj=c(0,<<<Y-TXT-ADJ-1>>>),cex=.8)") : (text7 + "\r\n grid.text(y=unit(" + text2 + ",\"native\"),x=unit(0,\"npc\"), just=c('left','bottom'), label=\"" + text5 + "\",gp=gpar(col='black',cex=.8))"));
							}
							else
							{
								text7 = " panel.abline(v=" + text2 + ", lty=\"" + Strings.LCase(text3) + "\", col=\"" + Strings.LCase(text4) + "\", lwd=1)";
								text7 = ((Operators.CompareString(IsChartType, "RXY", TextCompare: false) != 0 && Operators.CompareString(IsChartType, "RQQ", TextCompare: false) != 0 && Operators.CompareString(IsChartType, "RDENSITY", TextCompare: false) != 0) ? (text7 + "\r\n panel.text(" + text2 + ",.5,\"" + text5 + "\",adj=c(0,<<<Y-TXT-ADJ-1>>>),cex=.8)") : (text7 + "\r\n grid.text(x=unit(" + text2 + ",\"native\"),y=unit(1,\"npc\"), just=c('left','top'), label=\"" + text5 + "\",gp=gpar(col='black',cex=.8))"));
							}
						}
						else
						{
							text7 = "";
						}
						if (Operators.CompareString(text7, "", TextCompare: false) != 0)
						{
							text7 = Strings.Replace(MyJSL, "<>", text7, 1, -1, CompareMethod.Text);
							if (Operators.CompareString(text8, "", TextCompare: false) != 0)
							{
								text8 += "\r\n";
							}
							text8 += text7;
						}
					}
					result = text8;
				}
			}
		}
		array = null;
		return result;
	}

	private static void Substitute_Var_Prefix(string l_MyChartType, string MyToken, string MyOutput, string MyPrefix, string MyReturn_Value, ref string MyScript)
	{
		string text = "";
		string text2 = "";
		int num = 0;
		if (!((Operators.CompareString(MyToken, "N/A", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(MyToken), "", TextCompare: false) != 0)))
		{
			return;
		}
		text = MyOutput;
		if (((Operators.CompareString(MyToken, "Y-MIN", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "Y-MAX", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "X-MIN", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "X-MAX", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "Y-2-MAX", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "Y-2-MIN", TextCompare: false) != 0) & (Operators.CompareString(MyToken, "CHART-WHERE", TextCompare: false) != 0)) && ((Operators.CompareString(text, "0", TextCompare: false) == 0) & (Operators.CompareString(MyPrefix, "", TextCompare: false) != 0)))
		{
			text = "";
		}
		if ((Operators.CompareString(Strings.UCase(text), "N/A", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(MyToken + "  ", 1, 2), "0-", TextCompare: false) == 0))
		{
			text = "0";
		}
		else if ((Operators.CompareString(Strings.UCase(text), "N/A", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Strings.Mid(MyToken + "     ", 1, 5)), "init$", TextCompare: false) == 0))
		{
			text2 = Strings.Mid(MyToken, 6);
			num = Strings.InStr(text2, "$");
			if (num != 0)
			{
				text2 = Strings.Mid(text2, 1, checked(num - 1));
				text = text2;
			}
		}
		else if ((Operators.CompareString(Strings.UCase(text), "NONE", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(text), "N/A", TextCompare: false) == 0))
		{
			text = "";
		}
		text = Map_In_Out(MyReturn_Value, text);
		if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(MyPrefix, "", TextCompare: false) != 0))
		{
			text = Strings.Replace(MyPrefix, "<>", text, 1, -1, CompareMethod.Text);
		}
		MyScript = Strings.Replace(MyScript, "<<<" + MyToken + ">>>", text, 1, -1, CompareMethod.Text);
		if (Operators.CompareString(MyToken, "Y-MAX", TextCompare: false) == 0)
		{
			MyScript = Strings.Replace(MyScript, "<<<" + MyToken + "H>>>", Strings.Replace(text, "ylim", "xlim", 1, -1, CompareMethod.Text), 1, -1, CompareMethod.Text);
		}
	}

	public static string Get_Gnu_Plot_Y(string MyYVars, string MyX)
	{
		string text = "";
		int num = 0;
		string text2 = "";
		string text3 = "";
		int num2 = 0;
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		string text8 = "";
		text2 = MyYVars;
		num2 = Strings.InStr(MyX, ";");
		checked
		{
			if (num2 != 0)
			{
				MyX = Strings.Trim(Strings.Mid(MyX, 1, num2 - 1));
			}
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				string[] array = Strings.Split(text2, "</tr>");
				if (Information.UBound(array) >= 0)
				{
					int num3 = Information.UBound(array);
					for (num = 0; num <= num3; num++)
					{
						string[] array2 = Strings.Split(array[num], ";");
						if (Information.UBound(array2) >= 0)
						{
							text = ((num == 0) ? (text + "              using {" + array2[0] + "}:xtic({" + MyX + "})") : ((Operators.CompareString(Strings.Mid(Strings.Trim(array2[0]) + " ", 1, 1), "=", TextCompare: false) != 0) ? (text + "\r\n          ,'' using {" + array2[0] + "}") : (text + "\r\n          ," + Strings.Mid(Strings.Trim(array2[0]) + " ", 2))));
							text4 = Strings.Trim(array2[1]);
							text5 = Strings.Trim(array2[3]);
							text6 = Strings.Trim(array2[4]);
							text7 = "";
							if (Information.UBound(array2) >= 5)
							{
								text7 = Strings.Trim(array2[5]);
							}
							text8 = "";
							if (Information.UBound(array2) >= 6)
							{
								text8 = Strings.Trim(array2[6]);
							}
							if (Operators.CompareString(text4, "", TextCompare: false) != 0)
							{
								text4 = " title \"" + text4 + "\"";
							}
							if (Operators.CompareString(text5, "", TextCompare: false) != 0)
							{
								text5 = " axes " + text5;
							}
							if (Operators.CompareString(text6, "", TextCompare: false) != 0)
							{
								text6 = " with " + text6;
							}
							if (Operators.CompareString(text7, "", TextCompare: false) != 0)
							{
								text7 = " linewidth " + text7;
							}
							if (Operators.CompareString(text8, "", TextCompare: false) != 0)
							{
								text8 = " lc rgbcolor '" + text8 + "'";
							}
							text = text + text4 + text5 + text6 + text7 + text8 + "\\";
						}
						array2 = null;
						text3 = "\r\n          ,'' ";
					}
				}
				array = null;
			}
			return text;
		}
	}

	public static string Check_Chart_Vars(ref string MyScript)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string[] array = default(string[]);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		string myFile = default(string);
		string text2 = default(string);
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
				case 300:
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
							goto IL_0023;
						case 5:
							goto IL_0028;
						case 6:
							goto IL_0033;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_0060;
						case 9:
							goto IL_0071;
						case 10:
							goto IL_0092;
						case 11:
						case 12:
							goto IL_00c4;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0071:
					num2 = 9;
					if (Operators.CompareString(Strings.Trim(array[num5]), "", TextCompare: false) != 0)
					{
						goto IL_0092;
					}
					goto IL_00c4;
					IL_0092:
					num2 = 10;
					text = Strings.Replace(text, "<<<" + Strings.Trim(Strings.UCase(array[num5])) + ">>>", "", 1, -1, CompareMethod.Text);
					goto IL_00c4;
					IL_00cd:
					if (num5 > num6)
					{
						break;
					}
					goto IL_0071;
					IL_00c4:
					num2 = 12;
					num5 = checked(num5 + 1);
					goto IL_00cd;
					IL_000b:
					num2 = 2;
					myFile = Globals_Renamed.MySchemaDir + "\\chart_variables.txt";
					goto IL_001e;
					IL_001e:
					num2 = 3;
					num5 = 0;
					goto IL_0023;
					IL_0023:
					num2 = 4;
					text = MyScript;
					goto IL_0028;
					IL_0028:
					num2 = 5;
					text2 = General_Procedures.OpenReadFileContents(myFile);
					goto IL_0033;
					IL_0033:
					num2 = 6;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_004e;
					IL_004e:
					num2 = 7;
					array = Strings.Split(text2, "\r\n");
					goto IL_0060;
					IL_0060:
					num2 = 8;
					num6 = Information.UBound(array);
					num5 = 0;
					goto IL_00cd;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				array = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 300;
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

	private static string Substitute_Script(string MyScript, string MyInFile, string ChartVar, string MyTemplateType, string inifile, bool l_ColCaseInsensitive, bool IsSIngle)
	{
		string result = "";
		checked
		{
			try
			{
				string text = "";
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				string text2 = "";
				int num5 = 0;
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				string text7 = "";
				string text8 = "";
				string text9 = "N/A";
				bool flag = false;
				string text10 = "";
				string text11 = "";
				bool flag2 = false;
				bool flag3 = false;
				string text12 = "N/A";
				string left = "A";
				string text13 = "";
				string text14 = "";
				string myGroup = "";
				string myVaLib = "R_lib_0.lib";
				string myVaLib2 = "R_lib_1.lib";
				string myVaLib3 = "R_lib_2.lib";
				string remember_X = "";
				string remember_X2 = "";
				string remember_X3 = "";
				string l_Last = "";
				string l_Minus = "";
				string left2 = "";
				string text15 = "";
				switch (Strings.UCase(inifile))
				{
				case "RPARETOPLOT.INI":
					text15 = "RPARETO";
					break;
				case "ROVERLAYBARXY.INI":
					text15 = "ROVERLAY";
					break;
				case "RXYPLOT.INI":
					text15 = "RXY";
					break;
				case "RQQMATHPLOT.INI":
					text15 = "RQQ";
					break;
				case "RDENSITY.INI":
					text15 = "RDENSITY";
					break;
				}
				string text16 = "";
				string text17 = "";
				string text18 = "";
				string text19 = "";
				string text20 = "";
				string text21 = "N/A";
				string text22 = "N/A";
				int num6 = 0;
				string text23 = "";
				string text24 = "";
				int num7 = -1;
				string text25 = "\"\"";
				string text26 = "";
				string text27 = "";
				string text28 = "";
				string replacement = "";
				string replacement2 = "";
				string replacement3 = "";
				string replacement4 = "";
				bool AddLegendText = false;
				string expression = "";
				string text29 = "";
				string text30 = "";
				bool flag4 = false;
				bool flag5 = false;
				string text31 = "";
				string text32 = "";
				string text33 = "";
				switch (MyTemplateType)
				{
				case "JSL":
				{
					text20 = "                   Where( ";
					int g_NoStdCtrls4 = Globals_Renamed.g_NoStdCtrls;
					for (num3 = 0; num3 <= g_NoStdCtrls4; num3++)
					{
						if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "CHART-ITEMS-PER-ROW", TextCompare: false) == 0))
						{
							if (!((Operators.CompareString(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value), "N/A", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value), "", TextCompare: false) != 0)))
							{
								continue;
							}
							int g_NoStdCtrls5 = Globals_Renamed.g_NoStdCtrls;
							for (num2 = 0; num2 <= g_NoStdCtrls5; num2++)
							{
								if (!((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num2].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "BY", TextCompare: false) == 0)))
								{
									continue;
								}
								text = Strings.Trim(Globals_Renamed.g_StdCtrls[num2].Value);
								if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text, "N/A", TextCompare: false) != 0))
								{
									MyScript = BuildSQL.Write_VA_Lib("JSL_lib_1.lib") + "\r\n" + MyScript + "\r\n" + BuildSQL.Write_VA_Lib("JSL_lib_2.lib");
									string[] array = Strings.Split(text, ";");
									num7 = Information.UBound(array);
									int num9 = num7;
									for (num5 = 0; num5 <= num9; num5++)
									{
										text16 = text16 + " ," + array[num5];
										text17 = text17 + "\r\n       ::SPF_By_Col" + Conversions.ToString(num5 + 1) + " = column(::SPFSumm," + Conversions.ToString(num5 + 1) + ")[::MyCounter + ::Idx2];";
										text18 = text18 + text20 + array[num5] + " == ::SPF_By_Col" + Conversions.ToString(num5 + 1);
										text20 = "\r\n                            & ";
									}
									if (Operators.CompareString(text16, "", TextCompare: false) != 0)
									{
										text16 = Strings.Mid(text16, 3);
									}
								}
								break;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "WHERE-MODE", TextCompare: false) == 0))
						{
							text9 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "Y-AXIS", TextCompare: false) == 0))
						{
							text24 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if ((Operators.CompareString(Strings.UCase(text24), "N/A", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(text24), "", TextCompare: false) == 0))
							{
								text24 = "";
							}
						}
						else if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "BY", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value), "N/A", TextCompare: false) != 0)
						{
							text31 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
						}
					}
					break;
				}
				case "PY":
				{
					int g_NoStdCtrls3 = Globals_Renamed.g_NoStdCtrls;
					for (num3 = 0; num3 <= g_NoStdCtrls3; num3++)
					{
						if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "WHERE-PREFIX", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text, "N/A", TextCompare: false) != 0))
							{
								text26 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "TITLE", TextCompare: false) == 0))
						{
							expression = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							expression = Substitute_Title(expression, MyTemplateType);
						}
					}
					break;
				}
				case "R":
				{
					int g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
					for (num3 = 0; num3 <= g_NoStdCtrls; num3++)
					{
						if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "USE-R-CONDITION", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if (Operators.CompareString(text, "1", TextCompare: false) == 0 || Operators.CompareString(text, "2", TextCompare: false) == 0)
							{
								flag2 = true;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "CHART-ITEMS-PER-ROW", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "-1", TextCompare: false) == 0)
							{
								flag4 = true;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-LABELS", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if (Operators.CompareString(text, "Y", TextCompare: false) == 0)
							{
								flag3 = true;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-SUBTITLE", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								text25 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "WHERE-PREFIX", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text, "N/A", TextCompare: false) != 0))
							{
								text26 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "R-CONCAT-X", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							text12 = ((!((Operators.CompareString(text, "N/A", TextCompare: false) == 0) | (Operators.CompareString(text, "", TextCompare: false) == 0))) ? Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value) : "");
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "X-DATA-TYPE", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							left = ((!((Operators.CompareString(text, "N/A", TextCompare: false) == 0) | (Operators.CompareString(text, "", TextCompare: false) == 0))) ? Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value) : "");
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "BYFILE0", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "", TextCompare: false) != 0))
							{
								myVaLib = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "BYFILE1", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "", TextCompare: false) != 0))
							{
								myVaLib2 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "BYFILE2", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) & (Operators.CompareString(text, "", TextCompare: false) != 0))
							{
								myVaLib3 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "Y", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (LikeOperator.LikeString(Strings.UCase(text), "STARTINGWITH:*", CompareMethod.Binary))
							{
								text28 = "^" + Strings.Trim(Strings.Mid(text, 14));
							}
							else if (LikeOperator.LikeString(Strings.UCase(text), "ENDINGWITH:*", CompareMethod.Binary))
							{
								text28 = Strings.Trim(Strings.Mid(text, 12)) + "$";
							}
							else if (LikeOperator.LikeString(Strings.UCase(text), "CONTAINING:*", CompareMethod.Binary))
							{
								text28 = Strings.Trim(Strings.Mid(text, 12));
							}
							if (Operators.CompareString(text28, "", TextCompare: false) != 0)
							{
								text27 = "for (colidx in 1:ncol(<<<chart-table>>>)) {";
								text27 = text27 + "\r\nif (grepl(pattern='" + text28 + "',x=names(<<<chart-table>>>)[colidx],fixed=FALSE,ignore.case=TRUE)==TRUE) {";
								text27 += "\r\nAColi <- <<<chart-table>>>[,colidx];";
								text27 += "\r\nAColHdri <- names(<<<chart-table>>>)[colidx];";
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-TITLES-1", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								replacement = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-TITLES-2", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								replacement3 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-AUTOMAX-1", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								replacement2 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "DEFAULT-AUTOMAX-2", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
							{
								replacement4 = text;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "TITLE", TextCompare: false) == 0))
						{
							expression = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							expression = Substitute_Title(expression, MyTemplateType);
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "MODE", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								text = "";
							}
							MyScript = Strings.Replace(MyScript, "<<<simpletheme>>>", text, 1, -1, CompareMethod.Text);
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "R-GROUP-BY", TextCompare: false) == 0))
						{
							myGroup = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "X-COMPUTED", TextCompare: false) == 0))
						{
							text = Strings.UCase(Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value));
							if (Operators.CompareString(text, "N/A", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0)
							{
								flag5 = true;
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "KEEP-UNUSED-CHARTS", TextCompare: false) == 0))
						{
							left2 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
						}
					}
					int g_NoStdCtrls2 = Globals_Renamed.g_NoStdCtrls;
					for (num3 = 0; num3 <= g_NoStdCtrls2; num3++)
					{
						if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "BY", TextCompare: false) == 0))
						{
							text = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
							if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text, "N/A", TextCompare: false) != 0))
							{
								if (Operators.CompareString(MyTemplateType, "JS", TextCompare: false) == 0)
								{
									continue;
								}
								text32 = text;
								string[] array = Strings.Split(text, ";");
								num7 = Information.UBound(array);
								int num8 = num7;
								for (num5 = 0; num5 <= num8; num5++)
								{
									if (num5 == 0)
									{
										text16 = "mylvl" + Conversions.ToString(num5 + 1) + "<-factor(<<<chart-table>>>$" + array[num5] + ");";
										if (!flag2)
										{
											text18 = "[" + text26 + array[num5] + "==spf_by_col" + Conversions.ToString(num5 + 1);
											text19 = text26 + array[num5] + "==spf_by_col" + Conversions.ToString(num5 + 1);
										}
										continue;
									}
									if (!flag2)
									{
										text18 = text18 + " & " + text26 + array[num5] + "==spf_by_col" + Conversions.ToString(num5 + 1);
										text19 = text18 + " & " + text26 + array[num5] + "==spf_by_col" + Conversions.ToString(num5 + 1);
									}
									text16 = text16 + "\r\nmylvl" + Conversions.ToString(num5 + 1) + "<-factor(<<<chart-table>>>$" + array[num5] + ");";
								}
								if (!flag2)
								{
									if (num7 == 0)
									{
										text16 = text16 + "\r\n" + BuildSQL.Write_VA_Lib(myVaLib2);
										text17 = "};";
										if (Operators.CompareString(text25, "\"\"", TextCompare: false) == 0)
										{
											text25 = "paste(\"Where " + Strings.StrConv(array[0], VbStrConv.ProperCase) + " = \",spf_by_col1,sep=\"\")";
										}
									}
									else
									{
										text16 = text16 + "\r\n" + BuildSQL.Write_VA_Lib(myVaLib3);
										text17 = "}};";
										if (Operators.CompareString(text25, "\"\"", TextCompare: false) == 0)
										{
											text25 = "paste(\"Where " + Strings.StrConv(array[0], VbStrConv.ProperCase) + " = \",spf_by_col1,\" and " + Strings.StrConv(array[1], VbStrConv.ProperCase) + " = \",spf_by_col2,sep=\"\")";
										}
									}
									MyScript = Strings.Replace(MyScript, "<<<R-CONDITIONING-LAYOUT>>>", "", 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<R-BY-2>>>", "", 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<R-BY-20>>>", "", 1, -1, CompareMethod.Text);
									text18 += "<<<CHART-WHERE>>>]";
								}
								else
								{
									text16 = text16 + "\r\n" + BuildSQL.Write_VA_Lib(myVaLib);
									text17 = "";
									text18 = "[<<<CHART-WHERE>>>]";
									text16 = ((num7 != 0) ? (text16 + "\r\nnorows<-ceiling((nlevels(mylvl1) * nlevels(mylvl2))/NoPerRow);") : (text16 + "\r\nnorows<-ceiling(nlevels(mylvl1)/NoPerRow);"));
									MyScript = ((!flag4) ? Strings.Replace(MyScript, "<<<R-CONDITIONING-LAYOUT>>>", ",layout=c(NoPerRow,norows)  #layout=c((cols,rows,pages)", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<R-CONDITIONING-LAYOUT>>>", "", 1, -1, CompareMethod.Text));
								}
							}
							else
							{
								text16 = BuildSQL.Write_VA_Lib(myVaLib);
								text17 = "";
								text18 = "[<<<CHART-WHERE>>>]";
								MyScript = Strings.Replace(MyScript, "<<<R-CONDITIONING-LAYOUT>>>", "", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<R-BY-2>>>", "", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<R-BY-20>>>", "", 1, -1, CompareMethod.Text);
							}
						}
						else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num3].Type)), "SETUP", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num3].Token, "WHERE-MODE", TextCompare: false) == 0))
						{
							text9 = Strings.Trim(Globals_Renamed.g_StdCtrls[num3].Value);
						}
					}
					MyScript = Strings.Replace(MyScript, "<<<CHART-BY-1>>>", text16, 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<CHART-BY-2>>>", text17, 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<DEFAULT-SUBTITLE>>>", text25, 1, -1, CompareMethod.Text);
					if (Operators.CompareString(text27, "", TextCompare: false) != 0)
					{
						MyScript = Strings.Replace(MyScript, "<<<FOR-ALL-COLS>>>", text27, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<END-FOR-ALL-COLS>>>", "}};", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-TITLES-2>>>", replacement3, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-AUTOMAX-2>>>", replacement4, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-TITLES-1>>>", "", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-AUTOMAX-1>>>", "", 1, -1, CompareMethod.Text);
					}
					else
					{
						MyScript = Strings.Replace(MyScript, "<<<FOR-ALL-COLS>>>", "", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<END-FOR-ALL-COLS>>>", "", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-TITLES-2>>>", "", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-AUTOMAX-2>>>", "", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-TITLES-1>>>", replacement, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<DEFAULT-AUTOMAX-1>>>", replacement2, 1, -1, CompareMethod.Text);
					}
					break;
				}
				}
				if (unchecked(num7 >= 0 && !flag2))
				{
					MyScript = Strings.Replace(Replacement: (Operators.CompareString(left2, "1", TextCompare: false) != 0) ? ("spf.myno<-length(mylvl1" + text18 + ") - sum(is.na(mylvl1)); if (spf.myno >=1 ) {") : "if (1==1 ) {", Expression: MyScript, Find: "<<<drop-unused-chart>>>", Start: 1, Count: -1, Compare: CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<close-drop-unused-chart>>>", "};", 1, -1, CompareMethod.Text);
				}
				else
				{
					MyScript = Strings.Replace(MyScript, "<<<drop-unused-chart>>>", "", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<close-drop-unused-chart>>>", "", 1, -1, CompareMethod.Text);
				}
				expression = Strings.Replace(expression, "<<<chart-where0>>>", text18, 1, -1, CompareMethod.Text);
				if ((Operators.CompareString(text15, "RPARETO", TextCompare: false) == 0) & (Operators.CompareString(text32, "", TextCompare: false) != 0))
				{
					string[] array2 = Strings.Split(text32, ";");
					text6 = "";
					int num10 = Information.UBound(array2);
					for (num5 = 0; num5 <= num10; num5++)
					{
						text6 = text6 + "+factor(<<<chart-table>>>$" + Strings.Trim(array2[num5]) + ")<<<CHART-WHERE-BASE>>>";
					}
					array2 = null;
					text32 = text6;
				}
				int g_NoStdCtrls6 = Globals_Renamed.g_NoStdCtrls;
				for (num2 = 0; num2 <= g_NoStdCtrls6; num2++)
				{
					if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num2].Type)), "STANDARD", TextCompare: false) != 0)
					{
						continue;
					}
					switch (Globals_Renamed.g_StdCtrls[num2].Token)
					{
					case "X":
					case "Y":
					case "R-BY-2":
					case "R-GROUP-BY":
					case "GENERAL_VALUE":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						text3 = "";
						text4 = "";
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							switch (MyTemplateType)
							{
							case "HTML":
							{
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) != 0)
								{
									break;
								}
								int g_NoStdCtrls7 = Globals_Renamed.g_NoStdCtrls;
								for (num5 = 0; num5 <= g_NoStdCtrls7; num5++)
								{
									if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num5].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Token, "X", TextCompare: false) == 0))
									{
										text11 = Globals_Renamed.g_StdCtrls[num5].Value;
									}
									else if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num5].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Token, "MODE", TextCompare: false) == 0))
									{
										text10 = Globals_Renamed.g_StdCtrls[num5].Value;
									}
								}
								if (Operators.CompareString(text10, "", TextCompare: false) != 0)
								{
									MyScript = text10 + "\r\n" + MyScript;
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", "", 1, -1, CompareMethod.Text);
									break;
								}
								if (Operators.CompareString(text11, "", TextCompare: false) != 0)
								{
									text = Get_Gnu_Plot_Y(text, text11);
								}
								MyScript = Strings.Replace(MyScript, "$plot$", "plot '$spf-in-file$'\\", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text, 1, -1, CompareMethod.Text);
								break;
							}
							case "JS":
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) == 0)
								{
									int g_NoStdCtrls8 = Globals_Renamed.g_NoStdCtrls;
									for (num5 = 0; num5 <= g_NoStdCtrls8; num5++)
									{
										if ((Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num5].Type)), "STANDARD", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_StdCtrls[num5].Token, "MODE", TextCompare: false) == 0))
										{
											text10 = Globals_Renamed.g_StdCtrls[num5].Value;
											break;
										}
									}
									MyScript = text10 + "\r\n" + MyScript;
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "X", TextCompare: false) == 0)
								{
									text11 = Globals_Renamed.g_StdCtrls[num2].Value;
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text11, 1, -1, CompareMethod.Text);
								}
								break;
							case "R":
							{
								text2 = "";
								text6 = "";
								text7 = "";
								text8 = "";
								text30 = "";
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) == 0 && Operators.CompareString(text27, "", TextCompare: false) != 0)
								{
									text = "AColi";
								}
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) == 0)
								{
									text = Pre_Process_R_BarChart(ref MyScript, text, remember_X, remember_X2, remember_X3, text18, ref AddLegendText, flag5, myGroup, text15);
								}
								if (Operators.CompareString(left, "I", TextCompare: false) == 0 || Operators.CompareString(left, "N", TextCompare: false) == 0 || flag5)
								{
									text13 = "";
									text14 = "";
								}
								else
								{
									text13 = "factor(";
									text14 = ")";
								}
								string[] array2 = Strings.Split(text, ";");
								int num12 = Information.UBound(array2);
								for (num5 = 0; num5 <= num12; num5++)
								{
									text4 = Strings.Trim(array2[num5]);
									if ((num5 == 0) & (Operators.CompareString(text4, "", TextCompare: false) == 0))
									{
										text2 = "";
										text30 = "";
										break;
									}
									if (Operators.CompareString(text2, "", TextCompare: false) == 0)
									{
										if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "X", TextCompare: false) == 0)
										{
											if ((Operators.CompareString(text12, "", TextCompare: false) == 0) | (Information.UBound(array2) < 1))
											{
												text2 = "~" + text13 + Strings.Trim(array2[num5]) + text14 + text18;
												text30 = "~" + text13 + Strings.Trim(array2[num5]) + text14;
												text12 = "";
												text7 = "~" + text13 + "<<<chart-table>>>$" + array2[num5] + text14 + text18;
											}
											else
											{
												text2 = "~" + text13 + "myconcatx" + text14 + text18;
												text30 = "~" + text13 + "myconcatx" + text14;
												text7 = "~" + text13 + "<<<chart-table>>>$myconcatx" + text14 + text18;
											}
											text6 = Strings.Trim(array2[num5]);
										}
										else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-BY-2", TextCompare: false) == 0)
										{
											text2 = "|factor(" + Strings.Trim(array2[num5]) + ")" + text18;
											text30 = "|factor(" + Strings.Trim(array2[num5]) + ")";
											text6 = "+factor(<<<chart-table>>>$" + Strings.Trim(array2[num5]) + ")" + text18;
										}
										else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-GROUP-BY", TextCompare: false) == 0)
										{
											text2 = ",group=factor(" + Strings.Trim(array2[num5]) + ")" + text18;
											text30 = ",group=factor(" + Strings.Trim(array2[num5]) + ")";
											text6 = "+factor(<<<chart-table>>>$" + Strings.Trim(array2[num5]) + ")" + text18;
										}
										else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "GENERAL_VALUE", TextCompare: false) == 0)
										{
											text2 = Strings.Trim(array2[num5]) + text18;
											text6 = Strings.Trim(array2[num5]);
											text7 = "<<<chart-table>>>$" + Strings.Trim(array2[num5]);
										}
										else
										{
											text2 = Strings.Trim(array2[num5]) + text18;
											text30 = Strings.Trim(array2[num5]);
											text6 = Strings.Trim(array2[num5]);
											text7 = "<<<chart-table>>>$" + Strings.Trim(array2[num5]);
											text8 = "\"" + Strings.StrConv(Strings.Trim(array2[num5]), VbStrConv.ProperCase) + "\"";
										}
										continue;
									}
									text4 = text4 + "," + Strings.Trim(array2[num5]);
									if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "X", TextCompare: false) == 0)
									{
										if ((Operators.CompareString(text12, "", TextCompare: false) == 0) | (Information.UBound(array2) < 1))
										{
											text2 = text2 + " * " + text13 + Strings.Trim(array2[num5]) + text14 + text18;
											text30 = text30 + " * " + text13 + Strings.Trim(array2[num5]) + text14;
											text7 = text7 + " * " + text13 + "<<<chart-table>>>$" + Strings.Trim(array2[num5]) + text14 + text18;
										}
										text6 = text6 + "," + Strings.Trim(array2[num5]);
									}
									else if ((Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-BY-2", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-GROUP-BY", TextCompare: false) == 0))
									{
										text2 = text2 + " * factor(" + Strings.Trim(array2[num5]) + ")" + text18;
										text30 = text30 + " * factor(" + Strings.Trim(array2[num5]) + ")";
										text6 = ((Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-GROUP-BY", TextCompare: false) != 0) ? (text6 + "+factor(<<<chart-table>>>$" + Strings.Trim(array2[num5]) + ")" + text18) : (text6 + ",factor(<<<chart-table>>>$" + Strings.Trim(array2[num5]) + ")" + text18));
									}
									else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "GENERAL_VALUE", TextCompare: false) == 0)
									{
										text2 = text2 + "," + array2[num5] + text18;
										text6 = text6 + "," + Strings.Trim(array2[num5]);
										text7 = text7 + ",<<<chart-table>>>$" + Strings.Trim(array2[num5]);
									}
									else
									{
										text2 = text2 + " + " + Strings.Trim(array2[num5]) + text18;
										text30 = text30 + " + " + Strings.Trim(array2[num5]);
										text6 = text6 + "," + Strings.Trim(array2[num5]);
										text7 = text7 + ",<<<chart-table>>>$" + Strings.Trim(array2[num5]);
										text8 = text8 + ",\"" + Strings.StrConv(Strings.Trim(array2[num5]), VbStrConv.ProperCase) + "\"";
									}
								}
								MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text2, 1, -1, CompareMethod.Text);
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "X", TextCompare: false) == 0)
								{
									MyScript = Strings.Replace(MyScript, "<<<~X>>>", Strings.Mid(text2, 2), 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<X0>>>", text30, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", text6, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":::>>>", Strings.Replace(text6, ",", "+", 1, -1, CompareMethod.Text), 1, -1, CompareMethod.Text);
									text12 = Strings.Replace(text12, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", text6, 1, -1, CompareMethod.Text);
									remember_X = text2;
									remember_X2 = text7;
									remember_X3 = text30;
									MyScript = Strings.Replace(MyScript, "<<<R-CONCAT-X>>>", text12, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + "::>>>", text7, 1, -1, CompareMethod.Text);
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) == 0)
								{
									MyScript = Strings.Replace(MyScript, "<<<Y0>>>", text30, 1, -1, CompareMethod.Text);
									if (Operators.CompareString(text15, "ROVERLAY", TextCompare: false) == 0)
									{
										Get_Pre_Last(text30, ref l_Minus, ref l_Last, "+");
										MyScript = Strings.Replace(MyScript, "<<<Y0-LAST>>>", l_Last, 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<Y0-MINUS>>>", l_Minus, 1, -1, CompareMethod.Text);
									}
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", text6, 1, -1, CompareMethod.Text);
									if (Operators.CompareString(text15, "ROVERLAY", TextCompare: false) == 0)
									{
										Get_Pre_Last(text6, ref l_Minus, ref l_Last, ",");
										MyScript = Strings.Replace(MyScript, "<<<Y:-LAST>>>", l_Last, 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<Y:-MINUS>>>", l_Minus, 1, -1, CompareMethod.Text);
									}
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + "::>>>", text7, 1, -1, CompareMethod.Text);
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-GROUP-BY", TextCompare: false) == 0)
								{
									MyScript = Strings.Replace(MyScript, "<<<R-GROUP-BY0>>>", text30, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", text6, 1, -1, CompareMethod.Text);
									if (Operators.CompareString(text6, "", TextCompare: false) == 0)
									{
										MyScript = Strings.Replace(MyScript, "<<<~groupcomment>>>", "#", 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<groupcomment>>>", "", 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<r$chart$group>>>", "FALSE", 1, -1, CompareMethod.Text);
									}
									else
									{
										MyScript = Strings.Replace(MyScript, "<<<~groupcomment>>>", "", 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<groupcomment>>>", "#", 1, -1, CompareMethod.Text);
										MyScript = Strings.Replace(MyScript, "<<<r$chart$group>>>", "TRUE", 1, -1, CompareMethod.Text);
									}
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-BY-2", TextCompare: false) == 0)
								{
									MyScript = Strings.Replace(MyScript, "<<<R-BY-20>>>", text30, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<R-BY:>>>", text6, 1, -1, CompareMethod.Text);
									text31 = text6;
									MyScript = ((Operators.CompareString(text6, "", TextCompare: false) != 0) ? Strings.Replace(MyScript, "<<<~bycomment>>>", "", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<~bycomment>>>", "#", 1, -1, CompareMethod.Text));
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "GENERAL_VALUE", TextCompare: false) == 0)
								{
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", text6, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + "::>>>", text7, 1, -1, CompareMethod.Text);
								}
								array2 = null;
								break;
							}
							default:
								if (Strings.InStr(text, " ** ") == 0)
								{
									text = Strings.Replace(text, ";", ",", 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text, 1, -1, CompareMethod.Text);
									text4 = text;
								}
								else
								{
									string[] array2 = Strings.Split(text, ";");
									int num11 = Information.UBound(array2);
									for (num5 = 0; num5 <= num11; num5++)
									{
										text2 = Strings.Trim(array2[num5]);
										num = Strings.InStrRev(text2, " ** ");
										if (num != 0)
										{
											text3 = text3 + "," + Strings.Mid(text2, num + Strings.Len(" ** "));
											text4 = text4 + "," + Strings.Mid(text2, 1, num - 1);
										}
									}
									array2 = null;
									if (Operators.CompareString(text3, "", TextCompare: false) != 0)
									{
										text3 = Strings.Mid(text3, 2);
									}
									if (Operators.CompareString(text4, "", TextCompare: false) != 0)
									{
										text4 = Strings.Mid(text4, 2);
									}
									MyScript = Strings.Replace(MyScript, "<<<XY-EXTRA>>>", text3, 1, -1, CompareMethod.Text);
									MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text4, 1, -1, CompareMethod.Text);
								}
								text4 = Strings.Replace(text4, ":name(\"", "", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "\")", "", 1, -1, CompareMethod.Text);
								if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "X", TextCompare: false) == 0)
								{
									text21 = text4;
								}
								else if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y", TextCompare: false) == 0)
								{
									text22 = text4;
								}
								break;
							}
						}
						else
						{
							MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", "", 1, -1, CompareMethod.Text);
							if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "R-GROUP-BY", TextCompare: false) == 0)
							{
								MyScript = Strings.Replace(MyScript, "<<<R-GROUP-BY0>>>", "", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ":>>>", "", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<~groupcomment>>>", "#", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<groupcomment>>>", "", 1, -1, CompareMethod.Text);
							}
						}
						break;
					case "PSEUDO-X":
					{
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						text3 = "";
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							break;
						}
						if (Operators.CompareString(MyTemplateType, "JS", TextCompare: false) == 0)
						{
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, Globals_Renamed.g_StdCtrls[num2].Value, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
							break;
						}
						string[] array2 = Strings.Split(text, ";");
						int num13 = Information.UBound(array2);
						for (num5 = 0; num5 <= num13; num5++)
						{
							text2 = Strings.Trim(array2[num5]);
							text4 = "";
							text5 = "";
							if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) != 0)
							{
								continue;
							}
							num4 = Strings.InStrRev(text2, " (");
							if (num4 != 0)
							{
								text4 = Strings.Trim(Strings.UCase(Strings.Mid(text2, num4)));
								text2 = Strings.Mid(text2, 1, num4 - 1);
								if (Operators.CompareString(text4, "(N)", TextCompare: false) == 0)
								{
									text4 = "Num(";
									text5 = ")";
								}
								else
								{
									text4 = "";
									text5 = "";
								}
							}
							text3 = ((num5 != 0) ? (text3 + "\r\n                            & " + text2 + " == " + text4 + " ::t[" + Conversions.ToString(num5 + 1) + "][::i] " + text5) : (text3 + text2 + " == " + text4 + " ::t[" + Conversions.ToString(num5 + 1) + "][::i] " + text5));
						}
						if (Information.UBound(array2) == 0)
						{
							flag = true;
						}
						array2 = null;
						MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text3, 1, -1, CompareMethod.Text);
						if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0 && flag)
						{
							MyScript = Strings.Replace(MyScript, "::t[1]", "::t", 1, -1, CompareMethod.Text);
						}
						break;
					}
					case "TITLE":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (unchecked(Operators.CompareString(text, "", TextCompare: false) == 0 && flag3))
						{
							MyScript = Strings.Replace(MyScript, "\"<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>\"", "default_title", 1, -1, CompareMethod.Text);
						}
						else if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							text2 = text;
							if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0 || Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0)
							{
								text2 = expression;
							}
							MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text2, 1, -1, CompareMethod.Text);
						}
						break;
					case "X-AXIS":
					case "Y-AXIS":
					case "Y-AXIS-2":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (unchecked(Operators.CompareString(text, "", TextCompare: false) == 0 && flag3))
						{
							MyScript = ((Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y-AXIS", TextCompare: false) != 0) ? ((Operators.CompareString(Globals_Renamed.g_StdCtrls[num2].Token, "Y-AXIS-2", TextCompare: false) != 0) ? Strings.Replace(MyScript, "\"<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>\"", "default_x_axis", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "\"<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>\"", "default_y_2_axis", 1, -1, CompareMethod.Text)) : Strings.Replace(MyScript, "\"<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>\"", "default_y_axis", 1, -1, CompareMethod.Text));
						}
						else
						{
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, Globals_Renamed.g_StdCtrls[num2].Value, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						}
						break;
					case "BY":
					case "GROUP-BY":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (Operators.CompareString(MyTemplateType, "JS", TextCompare: false) == 0)
						{
							MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", text, 1, -1, CompareMethod.Text);
							break;
						}
						text = Strings.Replace(text, ";", ",", 1, -1, CompareMethod.Text);
						Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						if (Operators.CompareString(Strings.UCase(Globals_Renamed.g_StdCtrls[num2].Token), "BY", TextCompare: false) == 0)
						{
							MyScript = ((Operators.CompareString(text, "", TextCompare: false) == 0) ? Strings.Replace(MyScript, "<<<BYJS>>>", "myby<-\"\"" + text + ";", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<BYJS>>>", "myby<-<<<chart-table-subset>>>$" + text + ";", 1, -1, CompareMethod.Text));
						}
						break;
					case "CHART-WHERE":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						switch (MyTemplateType)
						{
						case "JSL":
							if (Operators.CompareString(text9, "0", TextCompare: false) != 0)
							{
								text18 = ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) ? Get_Chart_Where(MyTemplateType, text9, text, "   ", "\r\n   ", text26) : "");
							}
							else
							{
								text = ((Operators.CompareString(text, "N/A", TextCompare: false) != 0) ? Get_Chart_Where(MyTemplateType, text9, text, text20, "\r\n                            ", text26) : "");
								text18 += text;
								if (Operators.CompareString(Strings.Trim(text18), "", TextCompare: false) != 0)
								{
									text18 += "),";
								}
							}
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text18, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
							break;
						case "R":
							if (Operators.CompareString(text9, "0", TextCompare: false) == 0)
							{
								if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
								{
									text = "";
								}
								else
								{
									text = Get_Chart_Where(MyTemplateType, text9, text, text20, "\r\n                            ", text26);
									text33 = text;
									if (Operators.CompareString(text33, "", TextCompare: false) != 0)
									{
										text33 = "[" + text33 + "]";
									}
									if ((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text18, "[<<<CHART-WHERE>>>]", TextCompare: false) != 0))
									{
										text = " & " + text;
									}
									else if ((Operators.CompareString(text, "", TextCompare: false) == 0) & (Operators.CompareString(text18, "[<<<CHART-WHERE>>>]", TextCompare: false) == 0))
									{
										MyScript = Strings.Replace(MyScript, "[<<<CHART-WHERE>>>]", "<<<CHART-WHERE>>>", 1, -1, CompareMethod.Text);
									}
									MyScript = Strings.Replace(MyScript, "[<<<CHART-WHERE-OTHER>>>]", text18, 1, -1, CompareMethod.Text);
								}
							}
							else if (((Operators.CompareString(text, "", TextCompare: false) == 0) | (Operators.CompareString(text, "N/A", TextCompare: false) == 0)) & (Operators.CompareString(text18, "[<<<CHART-WHERE>>>]", TextCompare: false) == 0))
							{
								MyScript = Strings.Replace(MyScript, "[<<<CHART-WHERE>>>]", "<<<CHART-WHERE>>>", 1, -1, CompareMethod.Text);
							}
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
							if (((Operators.CompareString(text, "", TextCompare: false) == 0) | (Operators.CompareString(text, "N/A", TextCompare: false) == 0)) & (Operators.CompareString(text18, "[<<<CHART-WHERE>>>]", TextCompare: false) == 0))
							{
								MyScript = Strings.Replace(MyScript, "<<<R-SUBSET>>>", "", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<R-SUBSET2>>>", "<<<chart-table>>>", 1, -1, CompareMethod.Text);
							}
							else
							{
								MyScript = Strings.Replace(MyScript, "<<<R-SUBSET>>>", ",subset=(" + Strings.Replace(Strings.Replace(text18, "[", "", 1, -1, CompareMethod.Text), "]", "", 1, -1, CompareMethod.Text) + ")", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<R-SUBSET2>>>", "subset(<<<chart-table>>>," + Strings.Replace(Strings.Replace(text18, "[", "", 1, -1, CompareMethod.Text), "]", "", 1, -1, CompareMethod.Text) + ")", 1, -1, CompareMethod.Text);
								MyScript = Strings.Replace(MyScript, "<<<CHART-WHERE>>>", text, 1, -1, CompareMethod.Text);
							}
							break;
						case "JS":
							Substitute_Var_Prefix(MyOutput: (Operators.CompareString(text, "N/A", TextCompare: false) != 0) ? Get_Chart_Where(MyTemplateType, "0", text, "", "\r\n", text26) : "", l_MyChartType: "S", MyToken: Globals_Renamed.g_StdCtrls[num2].Token, MyPrefix: Globals_Renamed.g_StdCtrls[num2].JSL, MyReturn_Value: Globals_Renamed.g_StdCtrls[num2].Map, MyScript: ref MyScript);
							break;
						case "PY":
							if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
							{
								text = "";
							}
							else
							{
								text = Get_Chart_Where(MyTemplateType, "0", text, "", " ", text26);
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									text = "df=df[" + text + "]";
								}
							}
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
							break;
						}
						break;
					case "X-COMPUTED":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (Operators.CompareString(text, "N/A", TextCompare: false) == 0)
						{
							break;
						}
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							text = "myxcomputed<-0;";
						}
						else
						{
							num4 = Strings.InStr(text, ":");
							if (num4 != 0)
							{
								text2 = Strings.Mid(text, 1, num4 - 1);
								text = Strings.Mid(text, num4 + 1);
								text = "<<<chart-table>>> = transform(<<<chart-table>>>, " + text2 + "=" + text + ");\r\nmyxcomputed<-1;";
							}
						}
						MyScript = Strings.Replace(MyScript, "<<<X-COMPUTED>>>", text, 1, -1, CompareMethod.Text);
						break;
					case "ROW-LEGEND":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							text = Prep_Special_Tokens(MyTemplateType, 3, Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, text15);
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, "", "", ref MyScript);
						}
						break;
					case "X-AXIS-REF-LINE":
					case "Y-AXIS-REF-LINE":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (!LikeOperator.LikeString(Strings.UCase(text), "N/A*", CompareMethod.Binary))
						{
							text = Prep_Special_Tokens(MyTemplateType, 4, Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, text15);
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, "", "", ref MyScript);
						}
						else
						{
							MyScript = Strings.Replace(MyScript, "<<<" + Globals_Renamed.g_StdCtrls[num2].Token + ">>>", "", 1, -1, CompareMethod.Text);
						}
						break;
					case "Y-TXT-ADJ-1":
						if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) != 0)
						{
							break;
						}
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (Operators.CompareString(text, "N/A", TextCompare: false) != 0)
						{
							if (!Versioned.IsNumeric(text))
							{
								text = "1";
							}
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, "", "", ref MyScript);
						}
						break;
					case "IN-FILE":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0)
						{
							text = Strings.Replace(text, "\\", "/", 1, -1, CompareMethod.Text);
						}
						Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						break;
					case "CUSTOM-GROUP":
						text = Globals_Renamed.g_StdCtrls[num2].Value;
						Generate_WMap_Expr(text, MyInFile, ref MyScript, myGroup);
						break;
					case "Y-MAX":
					case "Y-MIN":
					case "Y-2-MAX":
					case "Y-2-MIN":
					case "X-MAX":
					case "X-MIN":
						if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0)
						{
							text = Globals_Renamed.g_StdCtrls[num2].Value;
							if (Operators.CompareString(text, "*", TextCompare: false) == 0)
							{
								text = "";
							}
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, text, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						}
						else
						{
							Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, Globals_Renamed.g_StdCtrls[num2].Value, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						}
						break;
					default:
						Substitute_Var_Prefix("S", Globals_Renamed.g_StdCtrls[num2].Token, Globals_Renamed.g_StdCtrls[num2].Value, Globals_Renamed.g_StdCtrls[num2].JSL, Globals_Renamed.g_StdCtrls[num2].Map, ref MyScript);
						break;
					case "CHART-THEME":
						break;
					}
				}
				num2 = 0;
				do
				{
					Substitute_Var_Prefix("O", Globals_Renamed.gOptOptions[num2].Variable, Globals_Renamed.gOptOptions[num2].Selected, Globals_Renamed.gOptOptions[num2].JSL, Globals_Renamed.gOptOptions[num2].Return_Value, ref MyScript);
					num2++;
				}
				while (num2 <= 25);
				if (LikeOperator.LikeString(ChartVar, "*{*}*", CompareMethod.Binary))
				{
					ChartVar = General_Procedures.Get_Node_Value(ChartVar);
				}
				MyScript = ((!IsSIngle) ? Strings.Replace(MyScript, "<<<SPF-RIGHT-CLICK-RUN>>>", "N", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<SPF-RIGHT-CLICK-RUN>>>", "Y", 1, -1, CompareMethod.Text));
				if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0)
				{
					if (Operators.CompareString(text21, "N/A", TextCompare: false) != 0)
					{
						string[] array3 = Strings.Split(text21, ",");
						MyScript = Strings.Replace(MyScript, "<<<X:>>>", array3[0], 1, -1, CompareMethod.Text);
						array3 = null;
					}
					if (Operators.CompareString(text22, "N/A", TextCompare: false) != 0)
					{
						string[] array3 = Strings.Split(text22, ",");
						MyScript = Strings.Replace(MyScript, "<<<Y:>>>", array3[0], 1, -1, CompareMethod.Text);
						text23 = "";
						int num14 = Information.UBound(array3);
						for (num6 = 1; num6 <= num14; num6++)
						{
							text23 = text23 + "\r\n         Dispatch({},\"" + array3[num6] + "\",TextEditBox,Set Text( \"" + text24 + "\" )),";
						}
						array3 = null;
						MyScript = Strings.Replace(MyScript, "<<<Y1:>>>", text23, 1, -1, CompareMethod.Text);
					}
					MyScript = Strings.Replace(MyScript, "<<<R-BY:>>>", text31, 1, -1, CompareMethod.Text);
					text31 = Strings.Replace(text31, ":name(\"", "", 1, -1, CompareMethod.Text);
					text31 = Strings.Replace(text31, "\")", "", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<R-BY::>>>", text31, 1, -1, CompareMethod.Text);
				}
				if (num7 == -1)
				{
					MyScript = Strings.Replace(MyScript, "<<<CHART-OBJECT>>>", ChartVar, 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<CHART-OBJECT-BY>>>", ChartVar, 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<SPF_BY_COL1>>>", "", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<SPF_BY_COL2>>>", "", 1, -1, CompareMethod.Text);
				}
				else
				{
					MyScript = Strings.Replace(MyScript, "<<<CHART-OBJECT-BY>>>", ChartVar, 1, -1, CompareMethod.Text);
					if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0)
					{
						MyScript = Strings.Replace(MyScript, "<<<CHART-OBJECT>>>", ChartVar + "$OB", 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<CHART-BY-1>>>", text16, 1, -1, CompareMethod.Text);
						MyScript = Strings.Replace(MyScript, "<<<CHART-BY-2>>>", text17, 1, -1, CompareMethod.Text);
					}
					int num15 = num7;
					for (num5 = 0; num5 <= num15; num5++)
					{
						text2 = "SPF_BY_COL" + Conversions.ToString(num5 + 1);
						if (Operators.CompareString(MyTemplateType, "JSL", TextCompare: false) == 0)
						{
							MyScript = Strings.Replace(MyScript, "<<<" + text2 + ">>>", "\" || Char(::" + text2 + ") || \"", 1, -1, CompareMethod.Text);
						}
						else if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0)
						{
							MyScript = Strings.Replace(MyScript, "<<<" + text2 + ">>>", "\"," + text2.ToLower() + ",\"", 1, -1, CompareMethod.Text);
						}
					}
				}
				MyScript = Strings.Replace(MyScript, "<<<R-BY:>>>", text31, 1, -1, CompareMethod.Text);
				MyScript = Strings.Replace(MyScript, "<<<R-BY::>>>", text32, 1, -1, CompareMethod.Text);
				MyScript = Strings.Replace(MyScript, "<<<CHART-WHERE-BASE>>>", text33, 1, -1, CompareMethod.Text);
				MyScript = ((Operators.CompareString(text19, "", TextCompare: false) == 0) ? Strings.Replace(MyScript, "<<<R-SUBSET-BY>>>", "<<<CHART-TABLE-SUBSET>>>", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<R-SUBSET-BY>>>", "subset(<<<chart-table-subset>>>," + text19 + ")", 1, -1, CompareMethod.Text));
				if (Operators.CompareString(text33, "", TextCompare: false) != 0)
				{
					MyScript = Strings.Replace(MyScript, "<<<R-SUBSET-ACTION>>>", "<<<chart-table-subset>>> <- subset(<<<chart-table>>>," + Strings.Replace(Strings.Replace(text33, "[", "", 1, -1, CompareMethod.Text), "]", "", 1, -1, CompareMethod.Text) + ");", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<CHART-TABLE-SUBSET>>>", "<<<CHART-TABLE>>>_subset99", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<REMOVE-CHART-TABLE-SUBSET>>>", "remove(<<<CHART-TABLE>>>_subset99);", 1, -1, CompareMethod.Text);
				}
				else
				{
					MyScript = Strings.Replace(MyScript, "<<<R-SUBSET-ACTION>>>", "", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<CHART-TABLE-SUBSET>>>", "<<<CHART-TABLE>>>", 1, -1, CompareMethod.Text);
					MyScript = Strings.Replace(MyScript, "<<<REMOVE-CHART-TABLE-SUBSET>>>", "", 1, -1, CompareMethod.Text);
				}
				MyScript = Strings.Replace(MyScript, "<<<CHART-TABLE>>>", MyInFile, 1, -1, CompareMethod.Text);
				MyScript = ((!l_ColCaseInsensitive) ? Strings.Replace(MyScript, "<<<CHART-COLUMN-CASE>>>", "0", 1, -1, CompareMethod.Text) : Strings.Replace(MyScript, "<<<CHART-COLUMN-CASE>>>", "1", 1, -1, CompareMethod.Text));
				MyScript = Strings.Replace(MyScript, "<<<r$chart$group>>>", "FALSE", 1, -1, CompareMethod.Text);
				if (Operators.CompareString(MyTemplateType, "R", TextCompare: false) == 0)
				{
					MyScript = Strings.Replace(MyScript, "list(paste(\",", "list(paste(\"\",", 1, -1, CompareMethod.Text);
				}
				if (Operators.CompareString(MyTemplateType, "PY", TextCompare: false) == 0)
				{
					MyScript = Check_Chart_Vars(ref MyScript);
				}
				result = MyScript;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error processing Object Script in BuildChart - Substitute_Script : " + ex2.ToString(), MsgBoxStyle.Critical, "Error");
				ProjectData.ClearProjectError();
			}
			finally
			{
				string[] array = null;
				string[] array2 = null;
			}
			return result;
		}
	}

	public static void Get_Pre_Last(string l_Data, ref string l_Minus, ref string l_Last, string l_DLM)
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
				case 174:
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
							goto IL_0018;
						case 5:
							goto IL_0021;
						case 6:
							goto IL_002d;
						case 7:
							goto IL_0039;
						case 8:
							goto IL_004b;
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
					IL_002d:
					num2 = 6;
					if (num5 == 0)
					{
						break;
					}
					goto IL_0039;
					IL_0039:
					num2 = 7;
					l_Last = Strings.Trim(Strings.Mid(l_Data, checked(num5 + 1)));
					goto IL_004b;
					IL_0021:
					num2 = 5;
					num5 = Strings.InStrRev(l_Data, l_DLM);
					goto IL_002d;
					IL_004b:
					num2 = 8;
					l_Minus = Strings.Trim(Strings.Mid(l_Data, 1, checked(num5 - 1)));
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					l_Last = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					l_Minus = "";
					goto IL_0021;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				l_Minus = l_Data;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 174;
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

	public static string Pre_Process_R_BarChart(ref string MyScript, string MyY, string Remember_X, string Remember_X2, string Remember_X0, string Chart_Where_By, ref bool AddLegendText, bool IsXComputed, string MyGroup, string IsChartType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		int num5 = default(int);
		int num6 = default(int);
		bool flag = default(bool);
		string l_Last = default(string);
		string l_Minus = default(string);
		string result = default(string);
		string[] array = default(string[]);
		int num7 = default(int);
		string[] array2 = default(string[]);
		string[] array3 = default(string[]);
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
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 3091:
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
								goto IL_0014;
							case 4:
								goto IL_001d;
							case 5:
								goto IL_0026;
							case 6:
								goto IL_002f;
							case 7:
								goto IL_0038;
							case 8:
								goto IL_0041;
							case 9:
								goto IL_004a;
							case 10:
								goto IL_0054;
							case 11:
								goto IL_005e;
							case 12:
								goto IL_0068;
							case 13:
								goto IL_006e;
							case 14:
								goto IL_0074;
							case 15:
								goto IL_007a;
							case 16:
								goto IL_0084;
							case 17:
								goto IL_008e;
							case 18:
								goto IL_00e9;
							case 19:
								goto IL_00ef;
							case 20:
								goto IL_00f4;
							case 21:
								goto IL_0100;
							case 22:
								goto IL_011e;
							case 23:
								goto IL_0128;
							case 24:
								goto IL_0143;
							case 25:
								goto IL_0155;
							case 26:
								goto IL_016a;
							case 27:
								goto IL_0179;
							case 28:
								goto IL_018c;
							case 29:
								goto IL_019d;
							case 30:
								goto IL_01b3;
							case 31:
								goto IL_01d1;
							case 32:
								goto IL_01dd;
							case 33:
								goto IL_01f3;
							case 35:
								goto IL_020d;
							case 36:
								goto IL_0226;
							case 34:
							case 37:
							case 38:
								goto IL_0231;
							case 39:
								goto IL_0264;
							case 40:
								goto IL_0277;
							case 41:
								goto IL_028a;
							case 42:
							case 43:
								goto IL_029f;
							case 44:
								goto IL_02b1;
							case 45:
								goto IL_02b7;
							case 46:
								goto IL_02d0;
							case 47:
								goto IL_02dd;
							case 48:
								goto IL_02f6;
							case 49:
								goto IL_0303;
							case 50:
								goto IL_031c;
							case 51:
								goto IL_0329;
							case 52:
								goto IL_033c;
							case 53:
								goto IL_034f;
							case 54:
								goto IL_0362;
							case 55:
								goto IL_036c;
							case 56:
								goto IL_0376;
							case 57:
								goto IL_0396;
							case 58:
								goto IL_03a0;
							case 59:
								goto IL_03aa;
							case 60:
								goto IL_03bb;
							case 61:
								goto IL_0405;
							case 62:
								goto IL_0416;
							case 63:
								goto IL_042f;
							case 65:
								goto IL_044d;
							case 64:
							case 66:
							case 67:
								goto IL_0487;
							case 68:
								goto IL_0491;
							case 69:
								goto IL_04a6;
							case 70:
								goto IL_04b0;
							case 71:
								goto IL_04d1;
							case 72:
								goto IL_04db;
							case 73:
								goto IL_053b;
							case 74:
								goto IL_0554;
							case 76:
								goto IL_05a9;
							case 75:
							case 77:
							case 78:
								goto IL_060a;
							case 79:
								goto IL_0622;
							case 80:
								goto IL_0634;
							case 81:
								goto IL_063e;
							case 82:
								goto IL_0657;
							case 84:
								goto IL_066c;
							case 85:
								goto IL_0676;
							case 86:
								goto IL_0688;
							case 87:
								goto IL_06bf;
							case 88:
								goto IL_06ce;
							case 83:
							case 89:
							case 90:
								goto IL_06e7;
							case 91:
								goto IL_06f8;
							case 92:
								goto IL_0703;
							case 93:
							case 94:
								goto IL_0716;
							case 95:
								goto IL_0727;
							case 96:
								goto IL_072d;
							case 97:
								goto IL_0733;
							case 98:
								goto IL_075b;
							case 99:
								goto IL_076f;
							case 100:
								goto IL_0785;
							case 101:
								goto IL_07ad;
							case 102:
								goto IL_07c3;
							case 103:
								goto IL_07dc;
							case 104:
								goto IL_07f5;
							case 105:
								goto IL_080e;
							case 106:
								goto IL_0826;
							case 107:
								goto IL_083b;
							case 108:
								goto IL_0853;
							case 109:
								goto IL_0868;
							case 111:
								goto IL_0875;
							case 112:
								goto IL_088e;
							case 113:
								goto IL_08a7;
							case 114:
								goto IL_08c0;
							case 115:
								goto IL_08ca;
							case 116:
								goto IL_08d8;
							case 117:
								goto IL_08e2;
							case 118:
								goto IL_08f4;
							case 119:
								goto IL_0906;
							case 120:
								goto IL_094d;
							case 121:
								goto IL_095c;
							case 122:
							case 123:
								goto IL_0964;
							case 124:
								goto IL_097a;
							case 125:
								goto IL_0984;
							case 126:
								goto IL_099d;
							case 127:
								goto IL_09b6;
							case 129:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 110:
							case 128:
							case 130:
							case 131:
							case 132:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_097a:
						num2 = 124;
						text = "";
						goto IL_0984;
						IL_0984:
						num2 = 125;
						MyScript = Strings.Replace(MyScript, "<<<CHART-TABLE2>>>", "<<<CHART-TABLE>>>", 1, -1, CompareMethod.Text);
						goto IL_099d;
						IL_0964:
						num2 = 123;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHARTPRE>>>", text, 1, -1, CompareMethod.Text);
						goto IL_097a;
						IL_099d:
						num2 = 126;
						if (Operators.CompareString(Remember_X0, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_09b6;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0014;
						IL_0014:
						num2 = 3;
						text3 = "";
						goto IL_001d;
						IL_001d:
						num2 = 4;
						text4 = "";
						goto IL_0026;
						IL_0026:
						num2 = 5;
						text = "";
						goto IL_002f;
						IL_002f:
						num2 = 6;
						text5 = "";
						goto IL_0038;
						IL_0038:
						num2 = 7;
						text6 = "";
						goto IL_0041;
						IL_0041:
						num2 = 8;
						text7 = "";
						goto IL_004a;
						IL_004a:
						num2 = 9;
						text8 = "";
						goto IL_0054;
						IL_0054:
						num2 = 10;
						text9 = "";
						goto IL_005e;
						IL_005e:
						num2 = 11;
						text10 = "";
						goto IL_0068;
						IL_0068:
						num2 = 12;
						num5 = 0;
						goto IL_006e;
						IL_006e:
						num2 = 13;
						num6 = 0;
						goto IL_0074;
						IL_0074:
						num2 = 14;
						flag = false;
						goto IL_007a;
						IL_007a:
						num2 = 15;
						l_Last = "";
						goto IL_0084;
						IL_0084:
						num2 = 16;
						l_Minus = "";
						goto IL_008e;
						IL_008e:
						num2 = 17;
						if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.gOptOptions[0].Variable)), "BAR-STACK", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.gOptOptions[0].Selected)), "TRUE", TextCompare: false) == 0)
						{
							goto IL_00e9;
						}
						goto IL_00ef;
						IL_09b6:
						num2 = 127;
						MyScript = Strings.Replace(MyScript, "<<<XBARCHART>>>", Remember_X0, 1, -1, CompareMethod.Text);
						goto end_IL_0001_3;
						IL_00e9:
						num2 = 18;
						flag = true;
						goto IL_00ef;
						IL_00ef:
						num2 = 19;
						result = MyY;
						goto IL_00f4;
						IL_00f4:
						num2 = 20;
						MyGroup = Strings.Trim(MyGroup);
						goto IL_0100;
						IL_0100:
						num2 = 21;
						if (Operators.CompareString(Strings.UCase(MyGroup), "N/A", TextCompare: false) == 0)
						{
							goto IL_011e;
						}
						goto IL_0128;
						IL_011e:
						num2 = 22;
						MyGroup = "";
						goto IL_0128;
						IL_0128:
						num2 = 23;
						if (Strings.InStr(MyY, "->") != 0)
						{
							goto IL_0143;
						}
						goto IL_0875;
						IL_0143:
						num2 = 24;
						array = Strings.Split(MyY, ";");
						goto IL_0155;
						IL_0155:
						num2 = 25;
						num7 = Information.UBound(array);
						num5 = 0;
						goto IL_02a8;
						IL_02a8:
						if (num5 <= num7)
						{
							goto IL_016a;
						}
						goto IL_02b1;
						IL_02b1:
						num2 = 44;
						array = null;
						goto IL_02b7;
						IL_02b7:
						num2 = 45;
						if (Operators.CompareString(text8, "", TextCompare: false) != 0)
						{
							goto IL_02d0;
						}
						goto IL_02dd;
						IL_02d0:
						num2 = 46;
						text8 = Strings.Mid(text8, 2);
						goto IL_02dd;
						IL_02dd:
						num2 = 47;
						if (Operators.CompareString(text9, "", TextCompare: false) != 0)
						{
							goto IL_02f6;
						}
						goto IL_0303;
						IL_02f6:
						num2 = 48;
						text9 = Strings.Mid(text9, 2);
						goto IL_0303;
						IL_0303:
						num2 = 49;
						if (Operators.CompareString(text10, "", TextCompare: false) != 0)
						{
							goto IL_031c;
						}
						goto IL_0329;
						IL_031c:
						num2 = 50;
						text10 = Strings.Mid(text10, 2);
						goto IL_0329;
						IL_0329:
						num2 = 51;
						array2 = Strings.Split(text10, ";");
						goto IL_033c;
						IL_033c:
						num2 = 52;
						array = Strings.Split(text8, ";");
						goto IL_034f;
						IL_034f:
						num2 = 53;
						array3 = Strings.Split(text9, ";");
						goto IL_0362;
						IL_0362:
						num2 = 54;
						text6 = "s0";
						goto IL_036c;
						IL_036c:
						num2 = 55;
						text5 = ",na.rm=TRUE";
						goto IL_0376;
						IL_0376:
						num2 = 56;
						if (Operators.CompareString(Strings.LCase(array[0]), "length", TextCompare: false) == 0)
						{
							goto IL_0396;
						}
						goto IL_03a0;
						IL_0396:
						num2 = 57;
						text5 = "";
						goto IL_03a0;
						IL_03a0:
						num2 = 58;
						text = "if (exists(\"s0\")) {remove(s0)};";
						goto IL_03aa;
						IL_03aa:
						num2 = 59;
						text += "\r\nif (exists(\"newbc99\")) {remove(newbc99)};";
						goto IL_03bb;
						IL_03bb:
						num2 = 60;
						text = text + "\r\ntry(s0<-aggregate(<<<chart-table>>>$" + Strings.Trim(array3[0]) + Chart_Where_By + "<<<x::>>><<<r-group-by:>>>,FUN=" + array[0] + text5 + "),silent=TRUE);";
						goto IL_0405;
						IL_0405:
						num2 = 61;
						text += "\r\nif(exists(\"s0\")) {";
						goto IL_0416;
						IL_0416:
						num2 = 62;
						if (Operators.CompareString(MyGroup, "", TextCompare: false) == 0)
						{
							goto IL_042f;
						}
						goto IL_044d;
						IL_042f:
						num2 = 63;
						text = text + "\r\ncolnames(s0)<-c(\"x1\",\"" + array2[0] + "\"); #rename s0 cols";
						goto IL_0487;
						IL_044d:
						num2 = 65;
						text = text + "\r\ncolnames(s0)<-c(\"x1\",\"" + MyGroup + "\", \"" + array2[0] + "\"); #rename s0 cols";
						goto IL_0487;
						IL_0487:
						num2 = 67;
						text6 = "s0";
						goto IL_0491;
						IL_0491:
						num2 = 68;
						num8 = Information.UBound(array);
						num5 = 1;
						goto IL_062b;
						IL_062b:
						if (num5 <= num8)
						{
							goto IL_04a6;
						}
						goto IL_0634;
						IL_0634:
						num2 = 80;
						text5 = "";
						goto IL_063e;
						IL_063e:
						num2 = 81;
						if (Operators.CompareString(text6, "s0", TextCompare: false) == 0)
						{
							goto IL_0657;
						}
						goto IL_066c;
						IL_0657:
						num2 = 82;
						text += "\r\nnewbc99<-s0;";
						goto IL_06e7;
						IL_066c:
						num2 = 84;
						text5 = "merge(s0, s1, by=\"x1\", all=TRUE)";
						goto IL_0676;
						IL_0676:
						num2 = 85;
						num9 = Information.UBound(array);
						num5 = 2;
						goto IL_06c8;
						IL_06c8:
						if (num5 <= num9)
						{
							goto IL_0688;
						}
						goto IL_06ce;
						IL_06ce:
						num2 = 88;
						text = text + "\r\nnewbc99<-" + text5 + "; #Create Dataframe using Full Outer Joins";
						goto IL_06e7;
						IL_06e7:
						num2 = 90;
						text += "\r\nif (myxcomputed==0) {newbc99$x1<-as.character(newbc99$x1)}; #Set datatype for x1 to character";
						goto IL_06f8;
						IL_06f8:
						num2 = 91;
						if (flag)
						{
							goto IL_0703;
						}
						goto IL_0716;
						IL_0703:
						num2 = 92;
						text += "\r\nnewbc99[is.na(newbc99)] <- 0; #Prevent Errors with Missing Values and Stack";
						goto IL_0716;
						IL_0716:
						num2 = 94;
						text += "\r\n};";
						goto IL_0727;
						IL_0727:
						num2 = 95;
						array = null;
						goto IL_072d;
						IL_072d:
						num2 = 96;
						array2 = null;
						goto IL_0733;
						IL_0733:
						num2 = 97;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART>>>", Strings.Replace(text10, ";", " + ", 1, -1, CompareMethod.Text), 1, -1, CompareMethod.Text);
						goto IL_075b;
						IL_075b:
						num2 = 98;
						Get_Pre_Last(text10, ref l_Minus, ref l_Last, ";");
						goto IL_076f;
						IL_076f:
						num2 = 99;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART-LAST>>>", l_Last, 1, -1, CompareMethod.Text);
						goto IL_0785;
						IL_0785:
						num2 = 100;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART-MINUS>>>", Strings.Replace(l_Minus, ";", " + ", 1, -1, CompareMethod.Text), 1, -1, CompareMethod.Text);
						goto IL_07ad;
						IL_07ad:
						num2 = 101;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHARTPRE>>>", text, 1, -1, CompareMethod.Text);
						goto IL_07c3;
						IL_07c3:
						num2 = 102;
						MyScript = Strings.Replace(MyScript, "<<<XBARCHART>>>", "~factor(x1)", 1, -1, CompareMethod.Text);
						goto IL_07dc;
						IL_07dc:
						num2 = 103;
						MyScript = Strings.Replace(MyScript, "<<<CHART-TABLE2>>>", "newbc99", 1, -1, CompareMethod.Text);
						goto IL_07f5;
						IL_07f5:
						num2 = 104;
						MyScript = Strings.Replace(MyScript, "<<<R-SUBSET>>>", "", 1, -1, CompareMethod.Text);
						goto IL_080e;
						IL_080e:
						num2 = 105;
						if (Operators.CompareString(Remember_X, "", TextCompare: false) != 0)
						{
							goto IL_0826;
						}
						goto IL_083b;
						IL_0826:
						num2 = 106;
						MyScript = Strings.Replace(MyScript, "<<<X>>>", Remember_X, 1, -1, CompareMethod.Text);
						goto IL_083b;
						IL_083b:
						num2 = 107;
						if (Operators.CompareString(Remember_X2, "", TextCompare: false) != 0)
						{
							goto IL_0853;
						}
						goto IL_0868;
						IL_0853:
						num2 = 108;
						MyScript = Strings.Replace(MyScript, "<<<X::>>>", Remember_X2, 1, -1, CompareMethod.Text);
						goto IL_0868;
						IL_0868:
						num2 = 109;
						result = text9;
						goto end_IL_0001_3;
						IL_0688:
						num2 = 86;
						text5 = "merge(" + text5 + ", s" + Conversions.ToString(num5) + ", by=\"x1\", all=TRUE)";
						goto IL_06bf;
						IL_06bf:
						num2 = 87;
						num5++;
						goto IL_06c8;
						IL_04a6:
						num2 = 69;
						text5 = ",na.rm=TRUE";
						goto IL_04b0;
						IL_04b0:
						num2 = 70;
						if (Operators.CompareString(Strings.LCase(array[num5]), "length", TextCompare: false) == 0)
						{
							goto IL_04d1;
						}
						goto IL_04db;
						IL_04d1:
						num2 = 71;
						text5 = "";
						goto IL_04db;
						IL_04db:
						num2 = 72;
						text = text + "\r\ns" + Conversions.ToString(num5) + "<-aggregate(<<<chart-table>>>$" + Strings.Trim(array3[num5]) + Chart_Where_By + "<<<x::>>><<<r-group-by:>>>,FUN=" + array[num5] + text5 + "); #Compute Stats";
						goto IL_053b;
						IL_053b:
						num2 = 73;
						if (Operators.CompareString(MyGroup, "", TextCompare: false) == 0)
						{
							goto IL_0554;
						}
						goto IL_05a9;
						IL_0554:
						num2 = 74;
						text = text + "\r\ncolnames(s" + Conversions.ToString(num5) + ")<-c(\"x1\",\"" + array2[num5] + "\"); #rename s" + Conversions.ToString(num5) + " cols";
						goto IL_060a;
						IL_05a9:
						num2 = 76;
						text = text + "\r\ncolnames(s" + Conversions.ToString(num5) + ")<-c(\"x1\",\"" + MyGroup + "\", \"" + array2[num5] + "\"); #rename s" + Conversions.ToString(num5) + " cols";
						goto IL_060a;
						IL_060a:
						num2 = 78;
						text6 = text6 + ",s" + Conversions.ToString(num5);
						goto IL_0622;
						IL_0622:
						num2 = 79;
						num5++;
						goto IL_062b;
						IL_016a:
						num2 = 26;
						text7 = Strings.Trim(array[num5]);
						goto IL_0179;
						IL_0179:
						num2 = 27;
						num6 = Strings.InStrRev(text7, "->");
						goto IL_018c;
						IL_018c:
						num2 = 28;
						if (num6 != 0)
						{
							goto IL_019d;
						}
						goto IL_029f;
						IL_019d:
						num2 = 29;
						text2 = Strings.Trim(Strings.Mid(text7, 1, num6 - 1));
						goto IL_01b3;
						IL_01b3:
						num2 = 30;
						text3 = Strings.Trim(Strings.Mid(text7, num6 + Strings.Len("->")));
						goto IL_01d1;
						IL_01d1:
						num2 = 31;
						text4 = Strings.LCase(text3);
						goto IL_01dd;
						IL_01dd:
						num2 = 32;
						if (LikeOperator.LikeString(text3, "quantile,probs=.*", CompareMethod.Binary))
						{
							goto IL_01f3;
						}
						goto IL_020d;
						IL_01f3:
						num2 = 33;
						text4 = "P" + Strings.Right(text3, 2);
						goto IL_0231;
						IL_020d:
						num2 = 35;
						if (Operators.CompareString(text3, "length", TextCompare: false) == 0)
						{
							goto IL_0226;
						}
						goto IL_0231;
						IL_0226:
						num2 = 36;
						text4 = "count";
						goto IL_0231;
						IL_0231:
						num2 = 38;
						text4 = Strings.Replace(Strings.StrConv(text4, VbStrConv.ProperCase) + "_" + Strings.StrConv(text2, VbStrConv.ProperCase), ",", "", 1, -1, CompareMethod.Text);
						goto IL_0264;
						IL_0264:
						num2 = 39;
						text8 = text8 + ";" + text3;
						goto IL_0277;
						IL_0277:
						num2 = 40;
						text9 = text9 + ";" + text2;
						goto IL_028a;
						IL_028a:
						num2 = 41;
						text10 = text10 + ";" + text4;
						goto IL_029f;
						IL_029f:
						num2 = 43;
						num5++;
						goto IL_02a8;
						IL_0875:
						num2 = 111;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART-MINUS>>>", "<<<Y0-MINUS>>>", 1, -1, CompareMethod.Text);
						goto IL_088e;
						IL_088e:
						num2 = 112;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART-LAST>>>", "<<<Y0-LAST>>>", 1, -1, CompareMethod.Text);
						goto IL_08a7;
						IL_08a7:
						num2 = 113;
						MyScript = Strings.Replace(MyScript, "<<<YBARCHART>>>", "<<<Y0>>>", 1, -1, CompareMethod.Text);
						goto IL_08c0;
						IL_08c0:
						num2 = 114;
						text = "";
						goto IL_08ca;
						IL_08ca:
						num2 = 115;
						if (flag)
						{
							goto IL_08d8;
						}
						goto IL_0964;
						IL_08d8:
						num2 = 116;
						text = "#Prevent Errors with Missing Values and Stack";
						goto IL_08e2;
						IL_08e2:
						num2 = 117;
						array = Strings.Split(MyY, ";");
						goto IL_08f4;
						IL_08f4:
						num2 = 118;
						num10 = Information.UBound(array);
						num5 = 0;
						goto IL_0956;
						IL_0956:
						if (num5 <= num10)
						{
							goto IL_0906;
						}
						goto IL_095c;
						IL_095c:
						num2 = 121;
						array = null;
						goto IL_0964;
						IL_0906:
						num2 = 119;
						text = text + "\r\n<<<chart-table>>>$" + Strings.Trim(array[num5]) + "[is.na(<<<chart-table>>>$" + Strings.Trim(array[num5]) + ")]<-0;";
						goto IL_094d;
						IL_094d:
						num2 = 120;
						num5++;
						goto IL_0956;
						end_IL_0001_2:
						break;
					}
					num2 = 129;
					MyScript = Strings.Replace(MyScript, "<<<XBARCHART>>>", "<<<X0>>>", 1, -1, CompareMethod.Text);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3091;
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

	public static bool Load_Chart_Ini(string MyIni)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		int num = 0;
		string text4 = "";
		int num2 = 0;
		bool result = true;
		text = "SETUP";
		int g_NoStdCtrls = Globals_Renamed.g_NoStdCtrls;
		checked
		{
			for (num = 0; num <= g_NoStdCtrls; num++)
			{
				if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num].Type)), text, TextCompare: false) == 0)
				{
					Globals_Renamed.g_StdCtrls[num].Value = General_Procedures.Get_Ini_Data(text, Globals_Renamed.g_StdCtrls[num].Token, Globals_Renamed.g_StdCtrls[num].Default_val, 5000, MyIni);
				}
			}
			text = "STANDARD";
			int g_NoStdCtrls2 = Globals_Renamed.g_NoStdCtrls;
			for (num = 0; num <= g_NoStdCtrls2; num++)
			{
				if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_StdCtrls[num].Type)), text, TextCompare: false) != 0)
				{
					continue;
				}
				text4 = General_Procedures.Get_Ini_Data(text, Globals_Renamed.g_StdCtrls[num].Token, "N/A", 5000, MyIni);
				if (Operators.CompareString(text4, "N/A", TextCompare: false) == 0)
				{
					Globals_Renamed.g_StdCtrls[num].Value = "N/A";
					continue;
				}
				string[] array = Strings.Split(text4, "|");
				int num3 = Information.UBound(array);
				for (num2 = 0; num2 <= num3; num2++)
				{
					if (Information.UBound(array) >= 0)
					{
						Globals_Renamed.g_StdCtrls[num].Default_val = array[0];
					}
					if (Information.UBound(array) >= 1)
					{
						Globals_Renamed.g_StdCtrls[num].JSL = array[1];
					}
					if (Information.UBound(array) >= 2)
					{
						Globals_Renamed.g_StdCtrls[num].Arg1 = array[2];
					}
				}
				if (Operators.CompareString(Globals_Renamed.g_StdCtrls[num].Default_val, "", TextCompare: false) != 0)
				{
					Globals_Renamed.g_StdCtrls[num].Value = Globals_Renamed.g_StdCtrls[num].Default_val;
				}
			}
			num = 0;
			do
			{
				Globals_Renamed.gOptOptions[num].Variable = "";
				Globals_Renamed.gOptOptions[num].JSL = "";
				Globals_Renamed.gOptOptions[num].Label = "";
				Globals_Renamed.gOptOptions[num].Show = "";
				Globals_Renamed.gOptOptions[num].Return_Value = "";
				Globals_Renamed.gOptOptions[num].Default_value = "";
				Globals_Renamed.gOptOptions[num].Help = "";
				text = "OPTION" + Conversions.ToString(num);
				Globals_Renamed.gOptOptions[num].Variable = General_Procedures.Get_Ini_Data(text, "VARIABLE", "N/A", 255, MyIni);
				if (Operators.CompareString(Globals_Renamed.gOptOptions[num].Variable, "N/A", TextCompare: false) != 0)
				{
					Globals_Renamed.gOptOptions[num].JSL = General_Procedures.Get_Ini_Data(text, "JSL", "N/A", 2000, MyIni);
					Globals_Renamed.gOptOptions[num].Label = General_Procedures.Get_Ini_Data(text, "LABEL", "N/A", 255, MyIni);
					Globals_Renamed.gOptOptions[num].Show = General_Procedures.Get_Ini_Data(text, "SHOW", "N/A", 2000, MyIni);
					Globals_Renamed.gOptOptions[num].Default_value = General_Procedures.Get_Ini_Data(text, "DEFAULT", "N/A", 255, MyIni);
					Globals_Renamed.gOptOptions[num].Return_Value = General_Procedures.Get_Ini_Data(text, "RETURN", "", 2000, MyIni);
					Globals_Renamed.gOptOptions[num].Help = General_Procedures.Get_Ini_Data(text, "HELP", "N/A", 2000, MyIni);
				}
				num++;
			}
			while (num <= 25);
			return result;
		}
	}

	public static string Load_Chart_Ini_File(string MyIniFile, ref string MyTemplateType, ref string MyScript)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		if (Operators.CompareString(Strings.Trim(MyIniFile), "", TextCompare: false) == 0)
		{
			text4 = "No Chart Configuration file is assigned";
		}
		else
		{
			if (Strings.InStrRev(MyIniFile, "\\") == 0)
			{
				MyIniFile = Globals_Renamed.gChartDir + "\\" + Strings.Trim(MyIniFile);
			}
			if (!File.Exists(MyIniFile))
			{
				text4 = "Chart Configuration file does not exist (" + MyIniFile + ")";
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				text2 = "TEMPLATE";
				text3 = General_Procedures.Get_Ini_Data(text2, "FILE", "N/A", 255, MyIniFile);
				if (Operators.CompareString(text3, "N/A", TextCompare: false) == 0)
				{
					text4 = "This appears to be an invalid chart configuration file. (" + text3 + "). Contact support for assistance.";
				}
				else
				{
					if (Strings.InStrRev(text3, "\\") == 0)
					{
						text3 = Globals_Renamed.gChartDir + "\\" + text3;
					}
					MyTemplateType = Strings.UCase(General_Procedures.Get_Ini_Data(text2, "TYPE", "JSL", 255, MyIniFile));
					MyScript = General_Procedures.OpenReadFileContents(text3);
					if (Operators.CompareString(MyScript, "", TextCompare: false) != 0)
					{
						text = MyIniFile;
						goto IL_0196;
					}
					text4 = "No Chart Template Script was provided for this Chart Configuration file. (" + text3 + ").";
				}
			}
		}
		Cursor.Current = Cursors.Default;
		Interaction.MsgBox(text4 + " Choose another chart config. file.", MsgBoxStyle.Exclamation, "Error");
		MyTemplateType = "";
		text = "";
		MyScript = "";
		goto IL_0196;
		IL_0196:
		Cursor.Current = Cursors.Default;
		return text;
	}

	public static void Load_Chart(ref TreeNode MyChartNode)
	{
		string text = "";
		string text2 = "";
		string MyTemplateType = "JSL";
		string text3 = "";
		string text4 = "";
		bool flag = false;
		string MyScript = "";
		string text5 = "";
		int num = 0;
		string MyTemplateType2 = "";
		string MyChart = "";
		string MyChartActual = "";
		text5 = MyChartNode.Name;
		text = (Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "*-NOTABLE*", CompareMethod.Binary)) ? General_Procedures.Get_Node_Value(MyChartNode.Nodes[0].Text) : ((!Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "JMP-CHART2*", CompareMethod.Binary))) ? General_Procedures.Get_Node_Value(MyChartNode.Nodes[1].Text) : ""));
		if (Operators.CompareString(text, "No", TextCompare: false) == 0)
		{
			text = "";
		}
		text2 = MyChartNode.Nodes[0].Name;
		if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Strings.InStrRev(text2, "\\") == 0)
		{
			text2 = Globals_Renamed.gChartDir + "\\" + text2;
		}
		if (Operators.CompareString(text5, "", TextCompare: false) == 0)
		{
			text2 = Load_Chart_Ini_File(text2, ref MyTemplateType, ref MyScript);
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				if (Init_Chart_Variables())
				{
					flag = Load_Chart_Ini(text2);
					if (flag)
					{
						goto IL_01c9;
					}
				}
			}
			goto IL_0754;
		}
		Cursor.Current = Cursors.WaitCursor;
		flag = Update_Chart_Variables(text2, text5, ref MyScript, ref MyTemplateType2);
		goto IL_01c9;
		IL_01c9:
		if (flag & (Operators.CompareString(text2, "", TextCompare: false) != 0))
		{
			MyTemplateType = General_Procedures.Get_Chart_Script_Type(Conversions.ToString(MyChartNode.Tag), 2);
			if (Operators.CompareString(MyTemplateType, "HTML", TextCompare: false) == 0)
			{
				if (Conversions.ToBoolean(Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "HTML-CHARTR*", CompareMethod.Binary)) || Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "HTML-CHARTP*", CompareMethod.Binary))))
				{
					if (Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "HTML-CHARTP*", CompareMethod.Binary)))
					{
						MyProject.Forms.FrmChart.f_TemplateType2 = MyTemplateType + "PY";
					}
					else
					{
						MyProject.Forms.FrmChart.f_TemplateType2 = MyTemplateType;
					}
					MyProject.Forms.FrmChart.Text = MyChartNode.Text + " [" + Get_Chart_Out_File(ref MyChartNode, "R") + "]";
					if (GetColCase(MyChartNode, 1))
					{
						MyProject.Forms.FrmChart.f_ColCaseInsensitive = "Y";
					}
					MyProject.Forms.FrmChart.ShowDialog();
					if (MyProject.Forms.FrmChart.f_OK && Operators.CompareString(MyProject.Forms.FrmChart.LastChartTable, "", TextCompare: false) != 0)
					{
						text = MyProject.Forms.FrmChart.LastChartTable;
					}
					MyProject.Forms.FrmChart.Dispose();
				}
				else if (Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "HTML-CHARTI*", CompareMethod.Binary)))
				{
					Get_Prior_Chart_Table_Filter_Objects(ref MyChartNode, ref MyChart, ref MyChartActual);
					FrmJSChart frmJSChart = new FrmJSChart();
					frmJSChart.f_TemplateType2 = MyTemplateType;
					frmJSChart.Text = "Chart [" + Get_Chart_Out_File(ref MyChartNode, "") + "]";
					frmJSChart.fDisplay = MyChart;
					frmJSChart.fActual = MyChartActual;
					frmJSChart.fName = MyChartNode.FirstNode.Text;
					frmJSChart.ShowDialog();
					if (frmJSChart.f_OK && Operators.CompareString(frmJSChart.LastChartTable, "", TextCompare: false) != 0)
					{
						text = frmJSChart.LastChartTable;
					}
					frmJSChart.Dispose();
				}
				else
				{
					FrmGnuChart frmGnuChart = new FrmGnuChart();
					frmGnuChart.f_TemplateType2 = MyTemplateType;
					frmGnuChart.Text = "Chart [" + Get_Chart_Out_File(ref MyChartNode, "") + "]";
					frmGnuChart.ShowDialog();
					if (frmGnuChart.f_OK && Operators.CompareString(frmGnuChart.LastChartTable, "", TextCompare: false) != 0)
					{
						text = frmGnuChart.LastChartTable;
					}
					frmGnuChart.Dispose();
				}
			}
			else
			{
				FrmChart frmChart = new FrmChart();
				if (LikeOperator.LikeString(Strings.UCase(MyChartNode.Text), "*PSEUDO*SIGMA*", CompareMethod.Binary))
				{
					frmChart.f_Pseudo = "Y";
				}
				frmChart.f_TemplateType2 = MyTemplateType;
				frmChart.Text = MyChartNode.Text;
				if (Conversions.ToBoolean(Operators.NotObject(LikeOperator.LikeObject(MyChartNode.Tag, "JMP-CHART2*", CompareMethod.Binary))))
				{
					frmChart.Load_Tables(text);
				}
				frmChart.ShowDialog();
				if (frmChart.f_OK && Operators.CompareString(frmChart.LastChartTable, "", TextCompare: false) != 0)
				{
					text = frmChart.LastChartTable;
				}
				frmChart.Dispose();
			}
			MyChartNode.Name = Save_Chart_To_Node();
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = "No";
			}
			if (Conversions.ToBoolean(LikeOperator.LikeObject(MyChartNode.Tag, "*-NOTABLE*", CompareMethod.Binary)))
			{
				MyChartNode.Nodes[0].Text = "Source Table {" + text + "}";
			}
			else if (Conversions.ToBoolean(Operators.NotObject(LikeOperator.LikeObject(MyChartNode.Tag, "*-CHART2:*", CompareMethod.Binary))))
			{
				MyChartNode.Nodes[1].Text = "Source Table {" + text + "}";
			}
			if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareString(text, "No", TextCompare: false) != 0, LikeOperator.LikeObject(MyChartNode.Tag, "*NEWTABLE*", CompareMethod.Binary))))
			{
				MyTemplateType2 = Conversions.ToString(MyChartNode.Tag);
				num = Strings.InStr(Strings.UCase(MyTemplateType2), ":MY");
				if (num != 0)
				{
					MyTemplateType2 = Strings.Mid(MyTemplateType2, checked(num + 3));
					MyTemplateType2 = Strings.StrConv(MyTemplateType2, VbStrConv.ProperCase);
				}
				else
				{
					MyTemplateType2 = "";
				}
				MyChartNode.Nodes[0].Text = Strings.Trim(MyTemplateType2) + "_" + text;
				if (Operators.CompareString(MyChartNode.Text, "Join", TextCompare: false) == 0)
				{
					MyChartNode.Nodes[2].Text = "Source Table {" + Get_StdCtrl("PSEUDO-COL") + "}";
				}
			}
		}
		else if (Operators.CompareString(text2, "", TextCompare: false) == 0)
		{
			text3 = "You must first load a Chart Template File before Starting the Chart Input Form";
			text4 = "Missing Chart Template";
			Cursor.Current = Cursors.Default;
			Interaction.MsgBox(text3, MsgBoxStyle.Exclamation, text4);
			goto IL_0754;
		}
		Cursor.Current = Cursors.Default;
		return;
		IL_0754:
		Cursor.Current = Cursors.Default;
	}
}

using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildAF
{
	public static void Init_SQLite_AF_Variables(ref Globals_Renamed.SQLiteAF_Type[] gSQliteAF, ref Globals_Renamed.SQLiteAF_Level_Type[] gSQLiteAF_Level)
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
				case 346:
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
							goto IL_0013;
						case 5:
							goto IL_0023;
						case 6:
							goto IL_0037;
						case 7:
							goto IL_004b;
						case 8:
							goto IL_005f;
						case 9:
							goto IL_0073;
						case 10:
							goto IL_0088;
						case 11:
							goto IL_009d;
						case 12:
							goto IL_00b2;
						case 13:
							goto IL_00c1;
						case 14:
							goto IL_00ca;
						case 15:
							goto IL_00cf;
						case 16:
							goto IL_00e0;
						case 17:
							goto IL_00f1;
						default:
							goto end_IL_0001;
						case 18:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_00cf:
					num2 = 15;
					gSQLiteAF_Level[num5].Level = num5;
					goto IL_00e0;
					IL_00e0:
					num2 = 16;
					gSQLiteAF_Level[num5].AF_Exists = false;
					goto IL_00f1;
					IL_00ca:
					num2 = 14;
					num5 = 0;
					goto IL_00cf;
					IL_00f1:
					num2 = 17;
					num5 = checked(num5 + 1);
					if (num5 > 20)
					{
						goto end_IL_0001_2;
					}
					goto IL_00cf;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0013;
					IL_0013:
					num2 = 4;
					gSQliteAF[num5].Level = 0;
					goto IL_0023;
					IL_0023:
					num2 = 5;
					gSQliteAF[num5].AF_Type = "";
					goto IL_0037;
					IL_0037:
					num2 = 6;
					gSQliteAF[num5].AF = "";
					goto IL_004b;
					IL_004b:
					num2 = 7;
					gSQliteAF[num5].ColHdr = "";
					goto IL_005f;
					IL_005f:
					num2 = 8;
					gSQliteAF[num5].fn = "";
					goto IL_0073;
					IL_0073:
					num2 = 9;
					gSQliteAF[num5].expr = "";
					goto IL_0088;
					IL_0088:
					num2 = 10;
					gSQliteAF[num5].PB = "";
					goto IL_009d;
					IL_009d:
					num2 = 11;
					gSQliteAF[num5].OB = "";
					goto IL_00b2;
					IL_00b2:
					num2 = 12;
					num5 = checked(num5 + 1);
					if (num5 <= 99)
					{
						goto IL_0013;
					}
					goto IL_00c1;
					IL_00c1:
					num2 = 13;
					Globals_Renamed.g_NoSQLiteAF = 0;
					goto IL_00ca;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 346;
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

	public static void Translate_Col(ref string MyCol, ref string MyError)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string MyNew = default(string);
		int num5 = default(int);
		int num6 = default(int);
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
				case 415:
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
							goto IL_0015;
						case 5:
							goto IL_001e;
						case 8:
							goto IL_002c;
						case 9:
							goto IL_003c;
						case 10:
							goto IL_0042;
						case 11:
							goto IL_0052;
						case 12:
							goto IL_0064;
						case 13:
							goto IL_0072;
						case 14:
							goto IL_008a;
						case 15:
							goto IL_009d;
						case 17:
							goto IL_00b8;
						case 18:
							goto IL_00d1;
						case 21:
							goto IL_00ec;
						case 6:
						case 7:
						case 16:
						case 19:
						case 20:
						case 22:
						case 23:
						case 24:
							goto IL_0106;
						default:
							goto end_IL_0001;
						case 25:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_00b8:
					num2 = 17;
					if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
					{
						goto IL_00d1;
					}
					goto IL_0106;
					IL_00d1:
					num2 = 18;
					MyError = "No matching Column found for SQLite Analytical function " + text + ". Please correct.";
					goto IL_0106;
					IL_009d:
					num2 = 15;
					MyCol = Strings.Replace(MyCol, text, Strings.Trim(MyNew), 1, -1, CompareMethod.Text);
					goto IL_0106;
					IL_00ec:
					num2 = 21;
					MyError = "Missing end delimiter'}' in SQLite Analytical Function. (" + MyCol + "). Please correct.";
					goto IL_0106;
					IL_000b:
					num2 = 2;
					num5 = -99;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num6 = 0;
					goto IL_0015;
					IL_0015:
					num2 = 4;
					text = "";
					goto IL_001e;
					IL_001e:
					num2 = 5;
					MyNew = "";
					goto IL_0106;
					IL_0106:
					num2 = 7;
					if (!((num5 != 0) & (Operators.CompareString(MyError, "", TextCompare: false) == 0)))
					{
						goto end_IL_0001_2;
					}
					goto IL_002c;
					IL_002c:
					num2 = 8;
					num5 = Strings.InStr(MyCol, "{");
					goto IL_003c;
					IL_003c:
					num2 = 9;
					num6 = 0;
					goto IL_0042;
					IL_0042:
					num2 = 10;
					if (num5 != 0)
					{
						goto IL_0052;
					}
					goto IL_0106;
					IL_0052:
					num2 = 11;
					num6 = Strings.InStr(MyCol, "}");
					goto IL_0064;
					IL_0064:
					num2 = 12;
					if (num6 > num5)
					{
						goto IL_0072;
					}
					goto IL_00ec;
					IL_0072:
					num2 = 13;
					text = Strings.Trim(Strings.Mid(MyCol, num5, checked(num6 - num5 + 1)));
					goto IL_008a;
					IL_008a:
					num2 = 14;
					if (BuildSQL.Get_Col_Header_From_ID(text, ref MyNew, ref MyError))
					{
						goto IL_009d;
					}
					goto IL_00b8;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 415;
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

	public static void Parse_AF_Expression(string InFn, ref string OutFn, ref string OutExpr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
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
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 335:
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
								goto IL_0022;
							case 7:
								goto IL_002b;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_0048;
							case 10:
								goto IL_0058;
							case 11:
								goto IL_006c;
							case 12:
								goto IL_007e;
							case 13:
								goto IL_008c;
							case 14:
								goto IL_009f;
							case 15:
								goto IL_00ad;
							case 16:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 17:
							case 18:
							case 19:
							case 20:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_008c:
						num2 = 13;
						num5 = Strings.InStrRev(text, ")");
						goto IL_009f;
						IL_009f:
						num2 = 14;
						if (num5 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_00ad;
						IL_007e:
						num2 = 12;
						if (num6 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_008c;
						IL_00ad:
						num2 = 15;
						OutFn = Strings.UCase(Strings.Trim(Strings.Mid(text, 1, num6 - 1)));
						break;
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
						num5 = 0;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						text = "";
						goto IL_0022;
						IL_0022:
						num2 = 6;
						OutFn = "";
						goto IL_002b;
						IL_002b:
						num2 = 7;
						OutExpr = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						num7 = Strings.InStr(Strings.UCase(InFn), "OVER");
						goto IL_0048;
						IL_0048:
						num2 = 9;
						if (num7 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0058;
						IL_0058:
						num2 = 10;
						text = Strings.Trim(Strings.Mid(InFn, 1, num7 - 1));
						goto IL_006c;
						IL_006c:
						num2 = 11;
						num6 = Strings.InStr(text, "(");
						goto IL_007e;
						end_IL_0001_2:
						break;
					}
					num2 = 16;
					OutExpr = Strings.Trim(Strings.Mid(text, num6 + 1, num5 - num6 - 1));
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 335;
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

	public static void Get_AF_Partition_Order_2(string InFn, ref string MyPB, string MyColHdr, ref string MyError)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
		int num7 = default(int);
		int num8 = default(int);
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
				case 411:
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
							goto IL_0019;
						case 6:
							goto IL_001e;
						case 7:
							goto IL_0027;
						case 8:
							goto IL_003b;
						case 9:
							goto IL_0045;
						case 10:
							goto IL_0050;
						case 11:
							goto IL_005a;
						case 12:
							goto IL_0073;
						case 13:
							goto IL_0084;
						case 14:
							goto IL_0097;
						case 15:
							goto IL_00aa;
						case 16:
							goto IL_00c3;
						case 17:
							goto IL_00d6;
						case 18:
							goto IL_00e9;
						case 19:
							goto IL_00f8;
						case 22:
							goto IL_0105;
						case 20:
						case 21:
						case 23:
						case 24:
						case 25:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 26:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00e9:
					num2 = 18;
					if (num5 <= num6)
					{
						break;
					}
					goto IL_00f8;
					IL_00f8:
					num2 = 19;
					MyError = text;
					break;
					IL_00d6:
					num2 = 17;
					num6 = Strings.InStrRev(MyPB, ")");
					goto IL_00e9;
					IL_0105:
					num2 = 22;
					MyPB = "";
					break;
					IL_000b:
					num2 = 2;
					num7 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num8 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num5 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					num6 = 0;
					goto IL_001e;
					IL_001e:
					num2 = 6;
					text2 = "";
					goto IL_0027;
					IL_0027:
					num2 = 7;
					text = "Please check analytical function expression for column: " + MyColHdr + ". It is suspected that your Analytical Function is part of another expression. This is currently not supported";
					goto IL_003b;
					IL_003b:
					num2 = 8;
					InFn = Strings.Trim(InFn);
					goto IL_0045;
					IL_0045:
					num2 = 9;
					text2 = Strings.UCase(InFn);
					goto IL_0050;
					IL_0050:
					num2 = 10;
					MyPB = "";
					goto IL_005a;
					IL_005a:
					num2 = 11;
					if (!LikeOperator.LikeString(text2, "*(*)*PARTITION* BY *)", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0073;
					IL_0073:
					num2 = 12;
					num7 = Strings.InStr(text2, " BY ");
					goto IL_0084;
					IL_0084:
					num2 = 13;
					num8 = Strings.InStrRev(text2, ")");
					goto IL_0097;
					IL_0097:
					num2 = 14;
					if (num7 != 0 && num8 != 0)
					{
						goto IL_00aa;
					}
					goto IL_0105;
					IL_00aa:
					num2 = 15;
					MyPB = Strings.Trim(checked(Strings.Mid(InFn, num7 + 4, num8 - (num7 + 4))));
					goto IL_00c3;
					IL_00c3:
					num2 = 16;
					num5 = Strings.InStrRev(MyPB, "(");
					goto IL_00d6;
					end_IL_0001_2:
					break;
				}
				num2 = 25;
				MyPB = BuildSQL.Replace_Comma_semicolon(MyPB);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 411;
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

	public static void Prepare_SQLite_SQL_for_AF_2(ref Globals_Renamed.SQLiteAF_Type[] gSQliteAF, ref Globals_Renamed.SQLiteAF_Level_Type[] gSQLiteAF_Level, ref string MyError)
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
						errsource = "BuildAF - Prepare_SQLite_SQL_for_AF_2";
						int num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						int num4 = 0;
						int num5 = -1;
						int g_NoQColumns = Globals_Renamed.g_NoQColumns;
						for (num3 = 0; num3 <= g_NoQColumns; num3++)
						{
							text3 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Column);
							text2 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column));
							text4 = "";
							text7 = "";
							num4 = -1;
							text8 = "";
							text9 = "N";
							text10 = "";
							if (LikeOperator.LikeString(text2, "RANK_SEMICOLON*(*)* OVER *(*PARTITION*BY*ORDER*BY*)", CompareMethod.Binary))
							{
								Globals_Renamed.g_QColumns[num3].Column = Strings.Replace(Globals_Renamed.g_QColumns[num3].Column, "rank_semicolon", "rank", 1, -1, CompareMethod.Text);
								Globals_Renamed.g_QColumns[num3].Column = Strings.Replace(Globals_Renamed.g_QColumns[num3].Column, ";", ",", 1, -1, CompareMethod.Text);
							}
							else if (LikeOperator.LikeString(text2, "LEAD-SPF*(*)* OVER *(*PARTITION*BY*ORDER*BY*)", CompareMethod.Binary))
							{
								Globals_Renamed.g_QColumns[num3].Column = Strings.Replace(Globals_Renamed.g_QColumns[num3].Column, "LEAD-SPF", "LEAD", 1, -1, CompareMethod.Text);
							}
							else if (LikeOperator.LikeString(text2, "LAG-SPF*(*)* OVER *(*PARTITION*BY*ORDER*BY*)", CompareMethod.Binary))
							{
								Globals_Renamed.g_QColumns[num3].Column = Strings.Replace(Globals_Renamed.g_QColumns[num3].Column, "LAG-SPF", "LAG", 1, -1, CompareMethod.Text);
							}
							else if ((LikeOperator.LikeString(text2, "COUNT*(*DISTINCT*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "STDEV*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P01*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P10*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P25*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P50*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P75*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P90*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P95*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P99*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P65*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "P80*", CompareMethod.Binary)) && LikeOperator.LikeString(text2, "*(*)* OVER *(*PARTITION*BY*)*", CompareMethod.Binary) && !LikeOperator.LikeString(text2, "* ORDER *BY *)", CompareMethod.Binary))
							{
								text4 = "SUMMARY";
							}
							if (Operators.CompareString(text4, "", TextCompare: false) == 0)
							{
								continue;
							}
							num5++;
							if (num5 == 100)
							{
								MyError = "You can only create a maximum of " + Conversions.ToString(100) + "Custom Analytical functions";
								break;
							}
							text5 = Globals_Renamed.g_QColumns[num3].List;
							if (LikeOperator.LikeString(Strings.UCase(text5), "@IF@*", CompareMethod.Binary))
							{
								text = Strings.Trim(Strings.Mid(text5, Strings.Len("@IF@") + 1));
								num4 = ((!Versioned.IsNumeric(text)) ? 1 : Conversions.ToShort(text));
							}
							else
							{
								if (Operators.CompareString(Strings.UCase(text5), "S", TextCompare: false) == 0)
								{
									MyError = "You cannot create SQLite analytical functions at the summary level";
									break;
								}
								if (LikeOperator.LikeString(Strings.UCase(text5), "S+*", CompareMethod.Binary))
								{
									text = Strings.Trim(Strings.Mid(text5, 3));
									if (Versioned.IsNumeric(text))
									{
										num4 = Conversions.ToShort(text) + 10;
									}
								}
								else
								{
									num4 = 0;
								}
							}
							gSQLiteAF_Level[num4].AF_Exists = true;
							gSQliteAF[num5].Level = num4;
							gSQliteAF[num5].ColHdr = Globals_Renamed.g_QColumns[num3].Header;
							gSQliteAF[num5].AF_Type = text4;
							gSQliteAF[num5].AF = text3;
							if (Operators.CompareString(text4, "SUMMARY", TextCompare: false) == 0)
							{
								if (Strings.InStr(text2, " ORDER BY ") != 0)
								{
									MyError = "Please check aggregate analytical Function expression for column: " + Globals_Renamed.g_QColumns[num3].Header + ". Either this is an Aggregate Analytical function that does not support the ORDER BY clause or this Analytical function is part of another expression, which is not allowed.";
									break;
								}
								Parse_AF_Expression(text3, ref gSQliteAF[num5].fn, ref gSQliteAF[num5].expr);
								text8 = gSQliteAF[num5].fn;
								if (!LikeOperator.LikeString(text2, "*PARTITION*BY*)", CompareMethod.Binary) || LikeOperator.LikeString(text2, "*PARTITION*BY*)*)*", CompareMethod.Binary))
								{
									MyError = "Please check analytical function expression for column: " + Globals_Renamed.g_QColumns[num3].Header + ". Custom Analytical functions cannot be nested within another expression.";
									break;
								}
							}
							Globals_Renamed.g_QColumns[num3].Column = "/*AF$*/";
							Get_AF_Partition_Order_2(text3, ref gSQliteAF[num5].PB, Globals_Renamed.g_QColumns[num3].Header, ref MyError);
							if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
							{
								Translate_Col(ref gSQliteAF[num5].AF, ref MyError);
							}
							if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
							{
								break;
							}
						}
						if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
						{
							Globals_Renamed.g_NoSQLiteAF = num5 + 1;
							if ((Globals_Renamed.g_NoSQLiteAF >= 1) & (Globals_Renamed.g_NoQColumns < 1))
							{
								MyError = "You must have more than one column in your SQLite output in order to use a custom SQLite Aggregate Analytical function";
							}
						}
						goto end_IL_0001;
					}
					case 1564:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							MyError = "Error Preparing SQL for Custom SQLite Analytical Function Logic";
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					goto IL_0652;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1564;
				continue;
			}
			break;
			IL_0652:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static string Get_AF_Pre_Select_Level(ref string InSQL, string FinalTable, int End_Level, ref Globals_Renamed.SQLiteAF_Type[] gSQLiteAF, int idx, int AFNo, string MyError)
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
						errsource = "BuildAf - Get_AF_Pre_Select_Level";
						result = "";
						string text = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string @string = "SELECT /*L" + Conversions.ToString(End_Level) + "*/";
						string text7 = ") t /*L" + Conversions.ToString(End_Level) + "*/";
						num3 = Strings.InStr(InSQL, @string);
						if (num3 != 0)
						{
							num4 = Strings.InStr(InSQL, text7);
							if (num4 > 0)
							{
								text = Strings.Mid(InSQL, num3, num4 - num3);
								text5 = Strings.Trim(Strings.Mid(InSQL, 1, num3 - 1));
								text6 = "";
								if (Operators.CompareString(Strings.Right(text5, 3), "(\r\n", TextCompare: false) == 0)
								{
									text6 = "\r\n)";
								}
								InSQL = text5 + FinalTable + text6 + Strings.Mid(InSQL, num4 + Strings.Len(text7));
							}
							else
							{
								text = Strings.Mid(InSQL, num3);
								InSQL = "";
							}
							num4 = -1;
							while (num4 != 0)
							{
								num4 = Strings.InStr(Strings.UCase(text), "/*AF$*/");
								if (num4 != 0)
								{
									text2 = Strings.Mid(text, 1, num4);
									text4 = Strings.Mid(text, num4 - 1);
									num5 = Strings.InStrRev(text2, "\r\n");
									if (num5 != 0)
									{
										text2 = Strings.Mid(text2, 1, num5 - 1);
									}
									num6 = Strings.InStr(text4, "\r\n");
									if (num6 != 0)
									{
										text4 = Strings.Mid(text4, num6);
									}
									text = text2 + text4;
								}
							}
							result = text + ";\r\n";
						}
						else
						{
							MyError = "Unexpected SQL Query Generated. Missing Query level " + Conversions.ToString(End_Level);
						}
						goto end_IL_0001;
					}
					case 565:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							MyError = Information.Err().Description;
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
				try0001_dispatch = 565;
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

	public static int Get_Highest_Q_Level(int QueryType, ref string MyError)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int num5 = default(int);
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
					errsource = "BuildAF - Get_Highest_Q_Level";
					int num3 = 0;
					string text = "";
					int num4 = 0;
					string text2 = "";
					num5 = ((QueryType >= 2) ? 10 : 0);
					text2 = BuildSQL.GetMaxLvl();
					if (LikeOperator.LikeString(Strings.UCase(text2), "S+*", CompareMethod.Binary))
					{
						text = Strings.Trim(Strings.Mid(text2, 3));
						num4 = ((!Versioned.IsNumeric(text)) ? 10 : Conversions.ToInteger(text));
					}
					else if (Versioned.IsNumeric(text2))
					{
						num4 = Conversions.ToInteger(text2);
					}
					if (num4 > num5)
					{
						num5 = num4;
					}
					goto end_IL_0001;
				}
				case 231:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						MyError = Information.Err().Description;
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 231;
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
		return num5;
	}

	public static string Get_SQLite_Col_Headers(int AtLevel, int HighestQLevel, ref string MyError)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text = default(string);
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
						errsource = "BuildAF - Get_SQLite_Col_Headers";
						text = "";
						int num3 = 0;
						string text2 = "";
						string text3 = "";
						bool flag = true;
						string text4 = "";
						string text5 = "";
						string text6 = "";
						int num4 = 0;
						string text7 = "";
						int g_NoQColumns = Globals_Renamed.g_NoQColumns;
						for (num3 = 0; num3 <= g_NoQColumns; num3++)
						{
							text2 = Strings.UCase(Globals_Renamed.g_QColumns[num3].List);
							text7 = Strings.UCase(Globals_Renamed.g_QColumns[num3].Statistics);
							text3 = Globals_Renamed.g_QColumns[num3].Show;
							flag = true;
							if (AtLevel == 0)
							{
								if (LikeOperator.LikeString(text2, "@IF@*", CompareMethod.Binary) || LikeOperator.LikeString(text2, "S+*", CompareMethod.Binary) || Operators.CompareString(text7, "EXPR", TextCompare: false) == 0)
								{
									flag = false;
								}
							}
							else if (AtLevel <= 9)
							{
								if (LikeOperator.LikeString(text2, "S+*", CompareMethod.Binary) || Operators.CompareString(text7, "EXPR", TextCompare: false) == 0)
								{
									flag = false;
								}
								else if (LikeOperator.LikeString(text2, "@IF@*", CompareMethod.Binary))
								{
									text4 = Strings.Trim(Strings.Mid(text2, Strings.Len("@IF@") + 1));
									if (Versioned.IsNumeric(text4) && Conversions.ToInteger(text4) > AtLevel)
									{
										flag = false;
									}
								}
							}
							else if (AtLevel == 10)
							{
								if (LikeOperator.LikeString(text2, "S+*", CompareMethod.Binary) || Operators.CompareString(text3, "N", TextCompare: false) == 0)
								{
									flag = false;
								}
							}
							else if (AtLevel > 10)
							{
								if (Operators.CompareString(text3, "N", TextCompare: false) == 0)
								{
									flag = false;
								}
								else if (LikeOperator.LikeString(text2, "S+*", CompareMethod.Binary))
								{
									text4 = Strings.Trim(Strings.Mid(text2, 3));
									if (Versioned.IsNumeric(text4))
									{
										num4 = Conversions.ToInteger(text4) + 10;
										if (num4 > AtLevel)
										{
											flag = false;
										}
										else if (num4 == HighestQLevel && Operators.CompareString(text3, "N:S", TextCompare: false) == 0)
										{
											flag = false;
										}
									}
								}
							}
							if (!flag)
							{
								continue;
							}
							text5 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Column);
							text6 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Header);
							if (Operators.CompareString(text5, "", TextCompare: false) != 0)
							{
								if (Strings.InStr(text5, "CrossTab->[") != 0)
								{
									text5 = Strings.Replace(text5, ":Y]", ":N]", 1, -1, CompareMethod.Text);
									text6 = text5;
								}
								else if (BuildForm.IsColPattern(text5))
								{
									text5 = Strings.Replace(text5, "[[Y<;>", "[[N<;>", 1, -1, CompareMethod.Text);
									text5 = Strings.Replace(text5, "[[M<;>", "[[N<;>", 1, -1, CompareMethod.Text);
									text6 = text5;
								}
								else if (Operators.CompareString(Strings.Trim(text6), "", TextCompare: false) == 0)
								{
									text6 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column), 1);
								}
								text = ((Operators.CompareString(text, "", TextCompare: false) != 0) ? (text + "\r\n," + text6) : (text + "\r\n" + text6));
							}
						}
						goto end_IL_0001;
					}
					case 992:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							MyError = Information.Err().Description;
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					goto IL_0416;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 992;
				continue;
			}
			break;
			IL_0416:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public static string Generate_AF_SQLite(ref Globals_Renamed.SQLiteAF_Type[] gSQLiteAF, ref Globals_Renamed.SQLiteAF_Level_Type[] gSQLiteAF_Level, string InSQL, int QueryType, ref string MyError)
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
					{
						result = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "BuildAF - Generate_AF_SQLite";
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						string text5 = "";
						int num7 = 0;
						string text6 = "";
						string text7 = "";
						int num8 = 0;
						string text8 = "";
						num8 = Get_Highest_Q_Level(QueryType, ref MyError);
						if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
						{
							goto end_IL_0001;
						}
						num3 = Strings.InStr(InSQL, "SELECT /*L");
						if (num3 != 0)
						{
							text4 = Strings.Mid(InSQL, 1, num3 - 1);
							InSQL = Strings.Mid(InSQL, num3);
						}
						num5 = 0;
						do
						{
							num7 = 0;
							if (gSQLiteAF_Level[num5].AF_Exists)
							{
								text3 = Get_SQLite_Col_Headers(num5, num8, ref MyError);
								if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
								{
									break;
								}
								int num9 = Globals_Renamed.g_NoSQLiteAF - 1;
								for (num6 = 0; num6 <= num9; num6++)
								{
									if (gSQLiteAF[num6].Level == num5)
									{
										num7++;
										if (num7 == 1)
										{
											text6 = "T_L" + Conversions.ToString(num5) + "_Init";
											text7 = "T_L" + Conversions.ToString(num5) + "_Result";
											if (Operators.CompareString(text, "", TextCompare: false) != 0)
											{
												text += "\r\n";
											}
											text = text + "\r\nDROP TABLE IF EXISTS " + text6 + ";\r\nCREATE TABLE " + text6 + " AS";
											text2 = Get_AF_Pre_Select_Level(ref InSQL, text7, num5, ref gSQLiteAF, num6, num7, MyError);
											if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
											{
												if (chk_select_comma_1(text2))
												{
													num3 = Strings.InStr(text2, ",");
													text5 = Strings.Mid(text2, 1, num3 - 1);
													text2 = Strings.Mid(text2, 1, num3 - 1) + " " + Strings.Mid(text2, num3 + 1);
												}
												if (Operators.CompareString(InSQL, "", TextCompare: false) == 0)
												{
													num3 = Strings.InStrRev(text2, "ORDER BY\r\n");
													if (num3 != 0)
													{
														text8 = Strings.Mid(text2, num3);
														text2 = Strings.Mid(text2, 1, num3 - 1) + ";";
													}
												}
												text = text + "\r\n" + text2;
												num4 = num5 + 1;
											}
										}
										if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
										{
											text = text + "\r\n" + Create_AF_SQL_2(text6, ref gSQLiteAF, num6, num7, ref MyError);
										}
									}
									if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
									{
										MyError = MyError + " (" + gSQLiteAF[num6].AF + ")";
										break;
									}
								}
								if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
								{
									text = text + "\r\n" + Create_Final_Table_at_Level_for_AF(text7, num5, text6, text3, num7);
								}
							}
							if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
							{
								break;
							}
							num5++;
						}
						while (num5 <= 20);
						if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
						{
							if (Operators.CompareString(text8, "", TextCompare: false) == 0)
							{
								text8 = "\r\n;";
							}
							if (Operators.CompareString(InSQL, "", TextCompare: false) != 0)
							{
								text = text + "\r\n\r\n" + InSQL + text8;
							}
							result = text4 + text;
						}
						goto end_IL_0001;
					}
					case 1071:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							MyError = "Error Generating SQL for SQLite Analytical Function Logic";
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
				try0001_dispatch = 1071;
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

	public static string Create_AF_SQL_2(string InitTable, ref Globals_Renamed.SQLiteAF_Type[] gSQLiteAF, int Idx, int AFNo, ref string MyError)
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
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildAF - Create_AF_SQL_2";
					result = "";
					int level = gSQLiteAF[Idx].Level;
					string tStem = "T_L" + Conversions.ToString(level) + "_" + Conversions.ToString(AFNo) + "_";
					string text = "";
					int num3 = 0;
					string text2 = "";
					string MyCol = gSQLiteAF[Idx].PB;
					Translate_Col(ref MyCol, ref MyError);
					if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
					{
						goto end_IL_0001;
					}
					Translate_Col(ref gSQLiteAF[Idx].expr, ref MyError);
					if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
					{
						string[] PBList = Strings.Split(MyCol, ";");
						int num4 = Information.UBound(PBList);
						for (num3 = 0; num3 <= num4; num3 = checked(num3 + 1))
						{
							PBList[num3] = Strings.Trim(PBList[num3]);
						}
						MyCol = Strings.Trim(Strings.Replace(MyCol, ";", ",", 1, -1, CompareMethod.Text));
						string aF_Type = gSQLiteAF[Idx].AF_Type;
						if (Operators.CompareString(aF_Type, "SUMMARY", TextCompare: false) == 0)
						{
							text = Create_AF_Summary(gSQLiteAF[Idx].AF_Type, gSQLiteAF[Idx].fn, gSQLiteAF[Idx].expr, gSQLiteAF[Idx].ColHdr, InitTable, tStem, MyCol, ref PBList, ref MyError);
						}
						PBList = null;
						if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
						{
							result = text;
						}
					}
					goto end_IL_0001;
				}
				case 505:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						MyError = "Error generating Custom SQLite Analytical Function Logic for '" + gSQLiteAF[Idx].AF_Type + "'";
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 505;
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

	public static string Create_AF_Summary(string Fn_Type, string Fn, string Expr, string ColHdr, string InitTable, string TStem, string PB, ref string[] PBList, ref string MyError)
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
						errsource = "BuildAF - Create_AF_Summary";
						result = "";
						string text = "";
						int num3 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						text = "DROP TABLE IF EXISTS " + TStem + "1;\r\nCREATE TABLE " + TStem + "1 AS\r\nSELECT " + Fn + "(" + Expr + ") AS AF$S1";
						int num4 = Information.UBound(PBList);
						for (num3 = 0; num3 <= num4; num3++)
						{
							text2 = "AF$PB" + Conversions.ToString(num3 + 1);
							text4 = text4 + text3 + text2;
							text = text + "\r\n," + PBList[num3] + " AS " + text2;
							text3 = ",";
						}
						text = text + "\r\nFROM " + InitTable + " GROUP BY ";
						text3 = "";
						int num5 = Information.UBound(PBList);
						for (num3 = 0; num3 <= num5; num3++)
						{
							text = text + "\r\n" + text3 + "AF$PB" + Conversions.ToString(num3 + 1);
							text3 = ",";
						}
						text += "\r\n;";
						text = text + "\r\nCREATE INDEX " + TStem + "1_Idx ON " + TStem + "1 (" + text4 + ");";
						text = text + "\r\nDROP TABLE IF EXISTS " + TStem + "Result;\r\nCREATE TABLE " + TStem + "Result AS\r\nSELECT a0.rowid AS orig_rowid, a1.AF$S1 AS " + ColHdr;
						text = text + "\r\nFROM " + InitTable + " a0 LEFT JOIN " + TStem + "1 a1 ON ";
						text3 = "";
						int num6 = Information.UBound(PBList);
						for (num3 = 0; num3 <= num6; num3++)
						{
							text = text + "\r\n" + text3 + PBList[num3] + " = a1.AF$PB" + Conversions.ToString(num3 + 1);
							text3 = "AND ";
						}
						text += "\r\n;";
						text = text + "\r\nDROP TABLE IF EXISTS " + TStem + "1;\r\n";
						text = text + "\r\nCREATE INDEX " + TStem + "Result_Idx ON " + TStem + "Result (orig_rowid);";
						result = text;
						goto end_IL_0001;
					}
					case 792:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							MyError = "Error generating SQL for Analytical Function " + Fn_Type;
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
				try0001_dispatch = 792;
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

	public static string Create_Final_Table_at_Level_for_AF(string Finaltable, int Level, string InitTable, string SelectCols, int AFCtr)
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
						errsource = "BuildAF - Create_Final_Table_at_level_for_AF";
						result = "";
						int num3 = 0;
						string text = "";
						text = "DROP TABLE IF EXISTS " + Finaltable + ";";
						text = text + "\r\nCREATE TABLE " + Finaltable + " AS";
						text += "\r\nSELECT";
						text = text + "\r\n" + SelectCols;
						text = text + "\r\nFROM " + InitTable + " a0";
						for (num3 = 1; num3 <= AFCtr; num3++)
						{
							text = text + "\r\nLEFT JOIN T_L" + Conversions.ToString(Level) + "_" + Conversions.ToString(num3) + "_Result a" + Conversions.ToString(num3) + " ON a0.rowid = a" + Conversions.ToString(num3) + ".orig_rowid";
						}
						text += "\r\n;";
						for (num3 = 1; num3 <= AFCtr; num3++)
						{
							text = text + "\r\nDROP TABLE IF EXISTS T_L" + Conversions.ToString(Level) + "_" + Conversions.ToString(num3) + "_Result;";
						}
						text = text + "\r\nDROP TABLE IF EXISTS " + InitTable + ";";
						result = text;
						goto end_IL_0001;
					}
					case 401:
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
				try0001_dispatch = 401;
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

	public static bool chk_select_comma_1(string MySQL)
	{
		bool result = false;
		int num = Strings.InStr(MySQL, "*/");
		checked
		{
			MySQL = Strings.Mid(MySQL, num + 2);
			int num2 = Strings.Len(MySQL);
			for (int i = 1; i <= num2; i++)
			{
				string left = Strings.Mid(MySQL, i, 1);
				if (Operators.CompareString(left, "\n", TextCompare: false) != 0 && Operators.CompareString(left, "\r", TextCompare: false) != 0 && Operators.CompareString(left, " ", TextCompare: false) != 0)
				{
					if (Operators.CompareString(left, ",", TextCompare: false) == 0)
					{
						result = true;
					}
					break;
				}
			}
			return result;
		}
	}
}

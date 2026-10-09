using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildSQL
{
	private static string gHasMacro = "N";

	public static string process_imbigdata_ks(string MyIn, ref string MyiBDFunc, string CBBegin, string CBEnd, ref string Error_Msg)
	{
		string result = "";
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string delimiter = "<;>";
		string stringMatch = ";";
		string text5 = "";
		checked
		{
			try
			{
				string text6 = Strings.Trim(Strings.UCase(Strings.Mid(MyIn + "   ", 13, 1)));
				if (Operators.CompareString(text6, "K", TextCompare: false) == 0)
				{
					text6 = "KS";
				}
				text2 = Strings.Trim(Strings.UCase(Strings.Mid(MyIn + "   ", 14, 1)));
				if (Operators.CompareString(Strings.UCase(Strings.Mid(MyIn + "            ", 1, 12)), "COLUMN-IBDF:", TextCompare: false) != 0)
				{
					goto IL_0182;
				}
				string text7 = Strings.Trim(Strings.Mid(MyIn + "               ", 16));
				if (Operators.CompareString(text7, "", TextCompare: false) == 0)
				{
					MyIn = "";
					goto IL_0182;
				}
				text5 = Strings.UCase(Strings.Mid(MyIn, 15, 1));
				if (Operators.CompareString(text5, "F", TextCompare: false) == 0)
				{
					MyiBDFunc = MyiBDFunc + "\r\nSQL_Get_File_Contents_v1(\"" + text7 + "\")";
				}
				else
				{
					result = "\r\nGet_iBD_Patterns_SPF(\"" + Strings.UCase(Strings.Mid(MyIn, 13, 2)) + "->" + text7 + "\")";
				}
				goto end_IL_0037;
				IL_0182:
				if (Operators.CompareString(MyIn, "", TextCompare: false) != 0)
				{
					if (Operators.CompareString(text6, "KS", TextCompare: false) == 0)
					{
						switch (text2)
						{
						default:
							if (Operators.CompareString(text2, "M", TextCompare: false) != 0)
							{
								text2 = "U";
								text3 = "UNIT";
								text4 = "";
								break;
							}
							goto case "L";
						case "L":
						case "W":
						case "U":
						case "D":
						case "P":
							switch (text2)
							{
							case "D":
								text3 = "UNIT";
								text4 = "DIE_";
								break;
							case "P":
								text3 = "UNIT";
								text4 = "PATCH_";
								break;
							case "M":
								text3 = "UNIT";
								text4 = "MQCS_";
								break;
							case "L":
								text3 = "LOT";
								text4 = "";
								break;
							case "W":
								text3 = "WAFER";
								text4 = "";
								break;
							case "U":
								text3 = "UNIT";
								text4 = "";
								break;
							}
							break;
						}
						int num = Strings.InStr(Strings.UCase(MyIn), "<SOF>");
						if (num != 0)
						{
							MyIn = Strings.Mid(MyIn, num + 5);
						}
						num = Strings.InStrRev(Strings.UCase(MyIn), "<EOF>");
						if (num != 0)
						{
							MyIn = Strings.Trim(Strings.Mid(MyIn, 1, num - 1));
						}
						string[] array = Strings.Split(MyIn, delimiter);
						int num2 = Information.UBound(array);
						for (int i = 0; i <= num2; i++)
						{
							text7 = Strings.Trim(array[i]);
							if (Operators.CompareString(text7, "", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(text7), "pattern,type", TextCompare: false) == 0)
							{
								continue;
							}
							int num3 = Strings.InStrRev(text7, stringMatch);
							string text8;
							string text9;
							if (num3 > 0)
							{
								text8 = Strings.Mid(text7, 1, num3 - 1);
								text9 = Strings.Trim(Strings.UCase(Strings.Mid(text7 + " ", num3 + 1)));
							}
							else
							{
								text8 = Strings.Trim(text7);
								text9 = "";
							}
							if (Operators.CompareString(text8, "", TextCompare: false) == 0)
							{
								continue;
							}
							string text10;
							string text11;
							if (Operators.CompareString(text9, "", TextCompare: false) != 0)
							{
								switch (text9)
								{
								default:
									if (Operators.CompareString(text9, "KS:MQCS", TextCompare: false) != 0)
									{
										text9 = text2;
										text10 = text3;
										text11 = text4;
										break;
									}
									goto case "KS:LOT";
								case "KS:LOT":
								case "KS:WAFER":
								case "KS:UNIT":
								case "KS:DIE":
								case "KS:PATCH":
									if (Operators.CompareString(text9, "KS:DIE", TextCompare: false) == 0 || Operators.CompareString(text9, "KS:PATCH", TextCompare: false) == 0 || Operators.CompareString(text9, "KS:MQCS", TextCompare: false) == 0)
									{
										text10 = "UNIT";
										text11 = Strings.UCase(Strings.Trim(Strings.Mid(text9, 4))) + "_";
									}
									else
									{
										text10 = Strings.UCase(Strings.Trim(Strings.Mid(text9, 4)));
										text11 = "";
									}
									break;
								}
							}
							else
							{
								text9 = text2;
								text10 = text3;
								text11 = text4;
							}
							text = text + "\r\n" + CBBegin + "         <Param DOMAIN='" + text6 + "' DATATYPE='" + text10 + "' " + text11 + "COLUMN_PATTERN='" + text8 + "'/>" + CBEnd;
						}
						array = null;
					}
					result = text;
				}
				end_IL_0037:;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Error_Msg = "Error generating imBigData Query: " + ex2.Message + ")";
				result = "";
				ProjectData.ClearProjectError();
			}
			finally
			{
				string[] array = null;
			}
			return result;
		}
	}

	public static string Get_Header_or_Field(string MyMode, string MyListF = "*")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		object obj = default(object);
		string text2 = default(string);
		int g_NoQColumns = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 943:
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
							goto IL_0022;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0039;
						case 8:
							goto IL_0050;
						case 9:
							goto IL_0059;
						case 10:
							goto IL_006b;
						case 11:
							goto IL_00a2;
						case 12:
							goto IL_00d1;
						case 14:
							goto IL_0108;
						case 15:
							goto IL_0130;
						case 17:
							goto IL_014a;
						case 18:
							goto IL_0178;
						case 22:
							goto IL_01a2;
						case 23:
							goto IL_01d3;
						case 24:
							goto IL_01e9;
						case 25:
							goto IL_0216;
						case 27:
							goto IL_022f;
						case 28:
							goto IL_0282;
						case 13:
						case 16:
						case 19:
						case 20:
						case 21:
						case 26:
						case 29:
						case 30:
						case 31:
						case 32:
							goto IL_029e;
						case 33:
						case 34:
						case 35:
							goto IL_02ba;
						case 36:
							goto IL_02cc;
						case 37:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 38:
						case 39:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0130:
					num2 = 15;
					text = Globals_Renamed.g_QColumns[num5].Header;
					goto IL_029e;
					IL_014a:
					num2 = 17;
					if (Operators.CompareString(Strings.Right(Globals_Renamed.g_QColumns[num5].Column, 2), ".*", TextCompare: false) != 0)
					{
						goto IL_0178;
					}
					goto IL_029e;
					IL_0108:
					num2 = 14;
					if (Operators.CompareString(Globals_Renamed.g_QColumns[num5].Header, "", TextCompare: false) != 0)
					{
						goto IL_0130;
					}
					goto IL_014a;
					IL_0178:
					num2 = 18;
					text = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num5].Column), 1);
					goto IL_029e;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					obj = ",";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					text2 = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					MyMode = Strings.UCase(Strings.Trim(MyMode));
					goto IL_0039;
					IL_0039:
					num2 = 7;
					if (Operators.CompareString(MyMode, "F", TextCompare: false) == 0)
					{
						goto IL_0050;
					}
					goto IL_0059;
					IL_0050:
					num2 = 8;
					obj = "||','||";
					goto IL_0059;
					IL_0059:
					num2 = 9;
					g_NoQColumns = Globals_Renamed.g_NoQColumns;
					num5 = 0;
					goto IL_02c3;
					IL_02c3:
					if (num5 <= g_NoQColumns)
					{
						goto IL_006b;
					}
					goto IL_02cc;
					IL_02cc:
					num2 = 36;
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_006b:
					num2 = 10;
					if (Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num5].Show), 1, 1), "Y", TextCompare: false) == 0)
					{
						goto IL_00a2;
					}
					goto IL_02ba;
					IL_00a2:
					num2 = 11;
					if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num5].List)), MyListF, CompareMethod.Binary))
					{
						goto IL_00d1;
					}
					goto IL_02ba;
					IL_00d1:
					num2 = 12;
					left = Strings.UCase(Strings.Trim(MyMode));
					if (Operators.CompareString(left, "H", TextCompare: false) == 0)
					{
						goto IL_0108;
					}
					if (Operators.CompareString(left, "F", TextCompare: false) == 0)
					{
						goto IL_01a2;
					}
					goto IL_029e;
					IL_029e:
					num2 = 32;
					text2 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text2, obj), text));
					goto IL_02ba;
					IL_01a2:
					num2 = 22;
					if (Operators.CompareString(Strings.Right(Globals_Renamed.g_QColumns[num5].Column, 2), ".*", TextCompare: false) != 0)
					{
						goto IL_01d3;
					}
					goto IL_029e;
					IL_01d3:
					num2 = 23;
					text = Globals_Renamed.g_QColumns[num5].Column;
					goto IL_01e9;
					IL_01e9:
					num2 = 24;
					if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType), "g", TextCompare: false) == 0)
					{
						goto IL_0216;
					}
					goto IL_022f;
					IL_0216:
					num2 = 25;
					text = "COALESCE(CAST(" + text + " AS VARCHAR(19)),'')";
					goto IL_029e;
					IL_022f:
					num2 = 27;
					if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType), "n", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType), "f", TextCompare: false) == 0)
					{
						goto IL_0282;
					}
					goto IL_029e;
					IL_02ba:
					num2 = 35;
					num5 = checked(num5 + 1);
					goto IL_02c3;
					IL_0282:
					num2 = 28;
					text = "COALESCE(CAST(" + text + " AS VARCHAR(50)),'')";
					goto IL_029e;
					end_IL_0001_2:
					break;
				}
				num2 = 37;
				text2 = Strings.Mid(text2, checked(Strings.Len(RuntimeHelpers.GetObjectValue(obj)) + 1));
				break;
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 943;
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
		return text2;
	}

	public static string Get_Uber_Type()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 105:
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
							goto IL_0026;
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
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0026;
					IL_0026:
					num2 = 3;
					result = "UBER-NET";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = "UBER";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 105;
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

	public static string Mongo_Round(string MyColumn, string HighestLevel, string LevelType, int CurrentLevel, string MyDT, string MyShow)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string right = default(string);
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
				case 545:
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
							goto IL_0025;
						case 4:
							goto IL_006c;
						case 5:
							goto IL_0112;
						case 7:
							goto IL_0177;
						case 6:
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_006c:
					num2 = 4;
					if (Operators.CompareString(Strings.Mid(Strings.Trim(MyColumn) + " ", 1, 1), "{", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(MyColumn) + "  ", 1, 2), "'$", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(Strings.Trim(MyColumn)) + "       ", 1, 7), "spf_fn$", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(Strings.Trim(MyColumn)) + "        ", 1, 8), "<~sqos~>", TextCompare: false) == 0)
					{
						goto IL_0112;
					}
					goto IL_0177;
					IL_0177:
					num2 = 7;
					MyColumn = "{'$round': ['$" + MyColumn + "'," + Strings.Mid(MyShow, checked((int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(MyShow, ":")))) + 1.0))) + "]}";
					break;
					IL_0025:
					num2 = 3;
					if (Operators.CompareString(HighestLevel, right, TextCompare: false) != 0 || (Operators.CompareString(MyDT, "N", TextCompare: false) != 0 && Operators.CompareString(MyDT, "F", TextCompare: false) != 0) || Strings.InStr(MyShow, "Y:") == 0)
					{
						break;
					}
					goto IL_006c;
					IL_0112:
					num2 = 5;
					MyColumn = "{'$round': [" + MyColumn + "," + Strings.Mid(MyShow, checked((int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(MyShow, ":")))) + 1.0))) + "]}";
					break;
					IL_000b:
					num2 = 2;
					right = Strings.Trim(LevelType) + Strings.Trim(Conversions.ToString(CurrentLevel));
					goto IL_0025;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				result = MyColumn;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 545;
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

	public static string Mongo_Project(string MyColumn, string HighestLevel, string LevelType, int CurrentLevel, string ll_M5, string MyDT, string ExtraFn, string MyShow)
	{
		string text = "";
		string text2 = "";
		text2 = Strings.Trim(LevelType) + Strings.Trim(Conversions.ToString(CurrentLevel));
		MyColumn = Mongo_Round(MyColumn, HighestLevel, LevelType, CurrentLevel, MyDT, MyShow);
		if (Operators.CompareString(HighestLevel, text2, TextCompare: false) == 0 && Operators.CompareString(ll_M5, "NO", TextCompare: false) == 0)
		{
			if (Operators.CompareString(ExtraFn, "", TextCompare: false) != 0)
			{
				return "{ '$ifNull': [ {'$size' : '$" + MyColumn + "'}, '' ] }";
			}
			if (Operators.CompareString(Strings.UCase(MyDT), "O", TextCompare: false) == 0)
			{
				return "{ '$dateToString': { 'format': '%Y-%m-%d %H:%M:%S', 'date': '$" + MyColumn + "'} }";
			}
			if (Operators.CompareString(Strings.Mid(Strings.Trim(MyColumn) + " ", 1, 1), "{", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(Strings.Trim(MyColumn)) + "        ", 1, 8), "<~sqos~>", TextCompare: false) == 0)
			{
				return "{ '$ifNull': [ " + MyColumn + ", '' ] }";
			}
			return "{ '$ifNull': [ '$" + MyColumn + "', '' ] }";
		}
		if (Operators.CompareString(ExtraFn, "", TextCompare: false) != 0)
		{
			return "{ '$size' : '$" + MyColumn + "'}";
		}
		if (Operators.CompareString(HighestLevel, text2, TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(MyDT), "O", TextCompare: false) == 0)
		{
			return "{ '$dateToString': { 'format': '%Y-%m-%d %H:%M:%S', 'date': '$" + MyColumn + "'} }";
		}
		if (Operators.CompareString(Strings.Mid(Strings.Trim(MyColumn) + " ", 1, 1), "{", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(Strings.Trim(MyColumn)) + "        ", 1, 8), "<~sqos~>", TextCompare: false) == 0)
		{
			return "{ '$ifNull': [ " + MyColumn + ", '' ] }";
		}
		return "'$" + MyColumn + "'";
	}

	public static string Generate_Mongo_Query(short SpecialTable, string ll_Row_limit, int QueryType, short l_SummPlusOne, short gILTop, short ll_TableIndex, string ll_MajorAlias, ref string ErrorMSG, string ll_M5, string ll_DisplayName, int fNoPostExtArr, ref Globals_Renamed.fPostExtArr_Type[] fPostExtArr)
	{
		string text = "";
		ErrorMSG = "";
		string text2 = "";
		int num = 0;
		int num2 = 0;
		bool flag = true;
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		string text8 = "";
		string text9 = "";
		string text10 = "";
		string text11 = "";
		string text12 = "";
		string[] array = new string[10];
		string[] array2 = new string[10];
		string[] array3 = new string[10];
		string text13 = "";
		string text14 = "";
		string text15 = "";
		string text16 = "";
		string text17 = "";
		string text18 = "";
		string[] array4 = new string[10];
		string text19 = "";
		bool flag2 = false;
		string text20 = "";
		bool flag3 = true;
		string highestLevel = "";
		bool flag4 = false;
		string text21 = "";
		string text22 = "";
		string text23 = "";
		string text24 = "";
		bool flag5 = false;
		int num3 = 0;
		string text25 = "";
		string text26 = "";
		string text27 = "";
		int num4 = 0;
		int num5 = 0;
		string text28 = "";
		int num6 = 0;
		string text29 = Globals_Renamed.gWinuser + "_" + Strings.LCase(ll_MajorAlias) + "_" + Globals_Renamed.gSPFCache;
		string text30 = "";
		bool flag6 = false;
		checked
		{
			try
			{
				num = 0;
				do
				{
					array4[num] = "";
					num++;
				}
				while (num <= 9);
				num = 0;
				do
				{
					array[num] = "";
					num++;
				}
				while (num <= 9);
				num = 0;
				do
				{
					array2[num] = "";
					num++;
				}
				while (num <= 9);
				num = 0;
				do
				{
					array3[num] = "";
					num++;
				}
				while (num <= 9);
				text27 = "<!>,{ '$unset':[";
				text2 = "";
				int g_NoQColumns = Globals_Renamed.g_NoQColumns;
				for (num = 0; num <= g_NoQColumns; num++)
				{
					if (Operators.CompareString(Strings.Right(Strings.Trim(Globals_Renamed.g_QColumns[num].Column), 2), ".*", TextCompare: false) == 0)
					{
						flag4 = true;
					}
					else if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column, "MDBNOT"))
					{
						flag5 = true;
						num4++;
						text21 = Globals_Renamed.g_QColumns[num].Column;
						text21 = Strings.Replace(text21, "<;>not ", "<;>", 1, -1, CompareMethod.Text);
						text21 = ChkHdrCol_Pattern(text21, 6);
						text27 = text27 + "\r\n" + text2 + text21;
						if (Strings.InStr(text21, "<;>Regex File<;>") != 0)
						{
							num6 = Strings.InStr(text21, "<;>Regex File<;>");
							text28 = Strings.Mid(text21, num6 + 16);
							num6 = Strings.InStr(text28, "<;>");
							if (num6 != 0)
							{
								text28 = Strings.Mid(text28, 1, num6 - 1);
							}
							text28 = Strings.Replace(text28, "<dot>", ".", 1, -1, CompareMethod.Text);
						}
						text2 = ",";
					}
					else if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column, "FUNCFILE"))
					{
						flag5 = true;
						num5++;
					}
					else if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column))
					{
						flag5 = true;
					}
				}
				text27 = ((Operators.CompareString(text27, "<!>,{ '$unset':[", TextCompare: false) == 0) ? "" : (text27 + "\r\n]}"));
				if (num4 > 1)
				{
					ErrorMSG = "At this time, you can only specify a single NOT Column Pattern search operator per query. To specify multiple patterns, try \"Not Regex File\"";
				}
				else if (fNoPostExtArr == -1 && (num4 > 1 || (num4 == 1 && Operators.CompareString(text28, "", TextCompare: false) == 0)))
				{
					ErrorMSG = "At this time, you can only specify a single \"NOT Regex File\" search to exclude fields when Post Extract Pattern Selection is set to No";
				}
				else if (num5 > 1)
				{
					ErrorMSG = "At this time, you can only specify a single Function File Column Pattern search operator per query.";
				}
				if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
				{
					if (fNoPostExtArr >= 0 && !flag4)
					{
						flag4 = true;
					}
					text24 = "<Collection=" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Collection(ll_TableIndex, 1, 1) + "/>";
					text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOECHUNK:" + ll_MajorAlias));
					if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) == 0)
					{
						text4 = "-1";
					}
					text24 = text24 + "\r\n<MongoEChunk=" + text4 + "/>";
					text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOWCHUNK:" + ll_MajorAlias));
					if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) == 0)
					{
						text4 = "-1";
					}
					text24 = text24 + "\r\n<MongoWChunk=" + text4 + "/>";
					text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOSUBCOMP:" + ll_MajorAlias));
					text24 = text24 + "\r\n<MongoSubComp=" + text4 + "/>";
					text5 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOMQCSDATA:" + ll_MajorAlias));
					text24 = text24 + "\r\n<MongoMQCS=" + text5 + "/>";
					if (((Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text4), "YES", TextCompare: false) != 0) || (Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text5), "YES", TextCompare: false) != 0)) && num5 >= 1)
					{
						text24 += "\r\n<MongoSubComp_FunctionFileName=Y/>";
						if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text4), "YES", TextCompare: false) != 0)
						{
							text24 = text24 + "\r\n<MongoPost_Col_SUBRegexFileName=" + text29 + ".sub_tab_include/>";
						}
						if (Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(text5), "YES", TextCompare: false) != 0)
						{
							text24 = text24 + "\r\n<MongoPost_Col_MQCSRegexFileName=" + text29 + ".mqcs_tab_include/>";
						}
					}
					if ((Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) != 0 || Operators.CompareString(Strings.UCase(text5), "NO", TextCompare: false) != 0) && Operators.CompareString(text28, "", TextCompare: false) != 0)
					{
						text24 = text24 + "\r\n<MongoSubComp_NotRegexFile=" + text28 + "/>";
					}
					text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOFILL:" + ll_MajorAlias));
					if (Operators.CompareString(Strings.UCase(text4), "NO", TextCompare: false) == 0)
					{
						text4 = "-1";
					}
					text24 = text24 + "\r\n<MongoFill=" + text4 + "/>";
					text21 = text4;
					text4 = (((Strings.InStr(Strings.UCase(ll_DisplayName), "KITCHENSINK") != 0 && Strings.InStr(Strings.UCase(ll_DisplayName), "CONTEXT") == 0 && Strings.InStr(Strings.UCase(ll_DisplayName), "COLUMNS") == 0) || flag4 || flag5) ? "SA" : ((Operators.CompareString(Strings.UCase(ll_M5), "NO", TextCompare: false) != 0) ? "SA" : "A"));
					text24 = text24 + "\r\n<MongoWFreq=" + text4 + "/>";
					if (LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "KITCHENSINK_UNIT*", CompareMethod.Binary) && !Globals_Renamed.gCheckedMongoKSDie)
					{
						flag6 = Validate_UN_Node("CLIKE", "ATD.I.MONGO2", "TMGUSER_DIE300*", ref Globals_Renamed.gMongoKSDie, "");
						Globals_Renamed.gCheckedMongoKSDie = true;
					}
					for (num = 0; num <= fNoPostExtArr; num++)
					{
						if (LikeOperator.LikeString(fPostExtArr[num].mode, "MongoPost*", CompareMethod.Binary))
						{
							text24 = text24 + "\r\n<" + fPostExtArr[num].mode + "=" + fPostExtArr[num].show + "/>";
						}
					}
				}
				text21 = "";
				text4 = "";
				if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
				{
					if (QueryType == 2 || QueryType == 3)
					{
						gILTop--;
						highestLevel = "S" + Strings.Trim(Conversions.ToString(unchecked((int)l_SummPlusOne)));
					}
					else
					{
						highestLevel = "L" + Strings.Trim(Conversions.ToString(unchecked((int)gILTop)));
					}
					if (QueryType == 3 && flag5)
					{
						ErrorMSG = "At this time you cannot build Cross Tab Queries that involve Column Patterns when querying Mongo Databases";
					}
				}
				if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
				{
					flag3 = true;
					int num7 = gILTop;
					for (num2 = 0; num2 <= num7; num2++)
					{
						text2 = ((num2 != 0) ? " " : ",");
						int g_NoQColumns2 = Globals_Renamed.g_NoQColumns;
						for (num = 0; num <= g_NoQColumns2; num++)
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
							if (BuildForm.IsColPattern(text4, "MDBNOT"))
							{
								continue;
							}
							text6 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
							text7 = Globals_Renamed.g_QColumns[num].Show;
							text8 = Globals_Renamed.g_QColumns[num].Comment;
							text9 = "N";
							if (Operators.CompareString(Strings.Mid(text8 + "   ", 1, 3), "***", TextCompare: false) == 0)
							{
								text9 = "Y";
							}
							text19 = Globals_Renamed.g_QColumns[num].List;
							text3 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num].DataType));
							flag2 = false;
							if (Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) == 0)
							{
								text6 = "";
							}
							else if (Operators.CompareString(text6, "", TextCompare: false) == 0 && !BuildForm.IsColPattern(text4))
							{
								text6 = General_Procedures.Strip_Column(Strings.Trim(text4), 3);
							}
							if (!LikeOperator.LikeString(text19, "s+*", CompareMethod.Binary) && (!LikeOperator.LikeString(Strings.UCase(text19), "@IF@*", CompareMethod.Binary) || (LikeOperator.LikeString(Strings.UCase(text19), "@IF@*", CompareMethod.Binary) && Operators.CompareString(Strings.UCase(text19), "@IF@" + Strings.Trim(Conversions.ToString(num2)), TextCompare: false) <= 0)))
							{
								flag2 = true;
							}
							if (Operators.CompareString(text9, "Y", TextCompare: false) == 0)
							{
								if (num2 == 0 && LikeOperator.LikeString(text19, "s+*", CompareMethod.Binary))
								{
									text9 = "N";
								}
								else if (num2 >= 1 && Operators.CompareString(Strings.UCase(text19), Strings.UCase("@IF@") + Strings.Trim(Conversions.ToString(num2)), TextCompare: false) != 0)
								{
									text9 = "N";
								}
							}
							if (!flag2 || Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) == 0 || (Operators.CompareString(Strings.Mid(text7, 1, 1), "Y", TextCompare: false) != 0 && Operators.CompareString(text7, "N:S", TextCompare: false) != 0 && (gILTop <= num2 || Operators.CompareString(text7, "N", TextCompare: false) != 0)))
							{
								continue;
							}
							if (num2 == 0 && flag3 && !flag4)
							{
								array[num2] = "\r\n    '_id' : 0";
							}
							else if (num2 == 0 && flag3 && flag4)
							{
								array[num2] = "\r\n    '_id' : '$_id'";
							}
							flag3 = false;
							if (BuildForm.IsColPattern(text4))
							{
								text6 = ChkHdrCol_Pattern(text4, 3);
								text26 = "";
								if (Operators.CompareString(text6, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text6, 1, 1), "_", TextCompare: false) == 0)
								{
									text26 = text6;
									text6 = "";
								}
								else if (Operators.CompareString(text6, "", TextCompare: false) != 0 && Operators.CompareString(text6, "\\#", TextCompare: false) == 0)
								{
									text26 = "#";
									text6 = "";
								}
								if (num2 == 0)
								{
									text22 = ChkHdrCol_Pattern(text4, 2);
									text25 = ChkHdrCol_Pattern(text4, 4);
									text21 = ((Operators.CompareString(text22, "|<>|", TextCompare: false) != 0) ? Strings.Replace(text22, "|<>|", "'$" + text25 + "|<>|'", 1, -1, CompareMethod.Text) : ("'$" + text25 + "|<>|'"));
									text21 = "'" + text6 + "|<>|" + text26 + "' : " + text21;
									array[num2] = array[num2] + "\r\n   " + text2 + Strings.Replace(text4, text22, text21, 1, -1, CompareMethod.Text);
								}
								else
								{
									text21 = "'" + text6 + "|<>|" + text26 + "' : '$" + text6 + "|<>|" + text26 + "'";
									text23 = Strings.Replace(ChkHdrCol_Pattern(text4, 0), "|<>|", text21, 1, -1, CompareMethod.Text);
									if (Strings.InStr(text23, "<;>Function File<;>") != 0)
									{
										text23 = Strings.Replace(text23, "Column-Pattern->[[M<;>", "Column-Pattern->[[O<;>", 1, -1, CompareMethod.Text);
									}
									array[num2] = array[num2] + "\r\n   " + text2 + text23;
								}
							}
							else
							{
								array[num2] = array[num2] + "\r\n   " + text2 + "'" + text6 + "' : ";
								if (Operators.CompareString(text9, "Y", TextCompare: false) == 0)
								{
									text4 = Mongo_Round(text4, highestLevel, "L", num2, text3, text7);
									array[num2] += text4;
								}
								else if (num2 == 0)
								{
									array[num2] += Mongo_Project(text4, highestLevel, "L", num2, ll_M5, text3, "", text7);
								}
								else
								{
									array[num2] += Mongo_Project(text6, highestLevel, "L", num2, ll_M5, text3, "", text7);
								}
							}
							text2 = ",";
						}
						if (Operators.CompareString(array[num2], "", TextCompare: false) == 0 && (Operators.CompareString(array[num2], "", TextCompare: false) != 0 || !flag4))
						{
							continue;
						}
						text21 = "'$project'";
						text20 = ((num2 < 1) ? "" : "\r\n<!>,");
						if (flag4)
						{
							text21 = "'$addFields'";
							if (Operators.CompareString(array[num2], "", TextCompare: false) == 0)
							{
								array[num2] = "\r\n    '_id':'$_id'";
							}
						}
						array[num2] = text20 + "{ " + text21 + ":{" + array[num2] + "\r\n  }}";
					}
					num3 = 0;
					if (SpecialTable == 1)
					{
						array2[0] = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Mongo_Clause(ll_TableIndex, "<!>,", QueryType);
						if (LikeOperator.LikeString(array2[0], "Error:*", CompareMethod.Binary))
						{
							text = "";
							ErrorMSG = array2[0];
						}
						num3 = 1;
					}
					if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
					{
						int num8 = num3;
						int num9 = gILTop;
						for (num2 = num8; num2 <= num9; num2++)
						{
							array2[num2] = Generate_Row_Filter_Mongo("S", "L" + Conversions.ToString(num2), QueryType, ll_TableIndex);
							if (LikeOperator.LikeString(array2[num2], "Error:*", CompareMethod.Binary))
							{
								text = "";
								ErrorMSG = array2[num2];
								break;
							}
							if (Operators.CompareString(array2[num2], "", TextCompare: false) != 0)
							{
								if (num2 >= 1)
								{
									text20 = "\r\n<!>,";
									text30 = "";
								}
								else
								{
									text20 = "";
									text30 = ((!(LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "KITCHENSINK_UNIT*", CompareMethod.Binary) & !Globals_Renamed.gMongoKSDie)) ? "" : "\r\n<!>,{ '$match':{ '$and': [{ '$nor': [{'_id' : {'$regex' : '_', '$options': 'i'}} ]} ]}}");
								}
								array2[num2] = text20 + "{ '$match':{ '$and': [" + array2[num2] + "\r\n ] }}" + text30;
							}
						}
					}
					if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
					{
						for (num2 = 1; num2 <= l_SummPlusOne; num2++)
						{
							array3[num2] = Generate_Row_Filter_Mongo("S", "S" + Conversions.ToString(num2), QueryType, ll_TableIndex);
							if (LikeOperator.LikeString(array3[num2], "Error:*", CompareMethod.Binary))
							{
								text = "";
								ErrorMSG = array2[num2];
								break;
							}
							if (Operators.CompareString(array3[num2], "", TextCompare: false) != 0)
							{
								array3[num2] = "\r\n<!>,{ '$match':{ '$and':[" + array3[num2] + "\r\n ] }}";
							}
						}
					}
				}
				if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0 && (QueryType == 2 || QueryType == 3))
				{
					text15 = "\r\n    '_id' : 0";
					int g_NoQColumns3 = Globals_Renamed.g_NoQColumns;
					for (num = 0; num <= g_NoQColumns3; num++)
					{
						if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column, "MDBNOT"))
						{
							continue;
						}
						text7 = Globals_Renamed.g_QColumns[num].Show;
						text8 = Globals_Renamed.g_QColumns[num].List;
						text12 = "";
						if ((Operators.CompareString(Strings.Mid(text7, 1, 1), "Y", TextCompare: false) != 0 && Operators.CompareString(text7, "N:S", TextCompare: false) != 0) || General_Procedures.SummPlusLevel(text8) != 0)
						{
							continue;
						}
						text3 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num].DataType));
						text10 = Strings.LCase(Strings.Trim(Globals_Renamed.g_QColumns[num].Statistics));
						text9 = text10 switch
						{
							"stddev" => "stdDevSamp", 
							"stddevp" => "stdDevPop", 
							"addtoset" => "addToSet", 
							_ => text10, 
						};
						text6 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
						text4 = Strings.Trim(Strings.Trim(Globals_Renamed.g_QColumns[num].Header));
						text12 = "";
						if (BuildForm.IsColPattern(text6))
						{
							text6 = ChkHdrCol_Pattern(text6, 0);
							text4 = ChkHdrCol_Pattern(text6, 3);
							text26 = "";
							if (Operators.CompareString(text4, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text4, 1, 1), "_", TextCompare: false) == 0)
							{
								text26 = text4;
								text4 = "";
							}
							if (Operators.CompareString(text9, "none", TextCompare: false) == 0)
							{
								text21 = "'" + text4 + "|<>|" + text26 + "' : '$" + text4 + "|<>|" + text26 + "'";
								text13 = text13 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
								text21 = "'" + text4 + "|<>|" + text26 + "' : '$_id." + text4 + "|<>|" + text26 + "'";
								text15 = text15 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
								continue;
							}
							if (LikeOperator.LikeString(text9, "count distinct*", CompareMethod.Binary))
							{
								text21 = "'" + text4 + "|<>|" + text26 + "' : { '$addToSet' : '$" + text4 + "|<>|" + text26 + "' }";
								text14 = text14 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
								text12 = "$size";
							}
							else if (LikeOperator.LikeString(text9, "count*", CompareMethod.Binary))
							{
								text21 = "'" + text4 + "|<>|" + text26 + "' : { '$sum' : 1 }";
								text14 = text14 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
							}
							else
							{
								text21 = "'" + text4 + "|<>|" + text26 + "' : { '$" + text9 + "' : '$" + text4 + "|<>|" + text26 + "' }";
								text14 = text14 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
							}
							text21 = "'" + text4 + "|<>|" + text26 + "' : '$" + text4 + "|<>|" + text26 + "'";
							text15 = text15 + "\r\n   ," + Strings.Replace(text6, "|<>|", text21, 1, -1, CompareMethod.Text);
							continue;
						}
						text4 = Strings.Trim(Strings.Trim(Globals_Renamed.g_QColumns[num].Header));
						if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
						{
							text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num].Column), 3);
						}
						if (Operators.CompareString(text9, "none", TextCompare: false) == 0)
						{
							text13 = text13 + "\r\n   ,'" + text4 + "' : '$" + text4 + "'";
							text15 = text15 + "\r\n   ,'" + text4 + "' : " + Mongo_Project("_id." + text4, highestLevel, "S", 0, ll_M5, text3, "", text7);
							continue;
						}
						if (!LikeOperator.LikeString(text9, "count distinct*", CompareMethod.Binary))
						{
							text14 = ((!LikeOperator.LikeString(text9, "count*", CompareMethod.Binary)) ? (text14 + "\r\n   ,'" + text4 + "' : { '$" + text9 + "' : '$" + text4 + "' }") : (text14 + "\r\n   ,'" + text4 + "' : { '$sum' : 1 }"));
						}
						else
						{
							text14 = text14 + "\r\n   ,'" + text4 + "' : { '$addToSet' : '$" + text4 + "' }";
							text12 = "$size";
						}
						text15 = text15 + "\r\n   ,'" + text4 + "' : " + Mongo_Project(text4, highestLevel, "S", 0, ll_M5, text3, text12, text7);
					}
					if (Operators.CompareString(text13, "", TextCompare: false) == 0)
					{
						text13 = "\r\n   '_id' : None";
					}
					else
					{
						text13 = Strings.Mid(text13, 7);
						text13 = "\r\n   '_id':{\r\n    " + text13 + "\r\n   }";
					}
					array4[0] = "\r\n<!>,{ '$group':{" + text13 + text14 + "\r\n  }}";
					array4[0] = array4[0] + "\r\n<!>,{ '$project':{" + text15 + "\r\n  }}";
				}
				if (Operators.CompareString(ErrorMSG, "", TextCompare: false) == 0)
				{
					for (num2 = 1; num2 <= l_SummPlusOne; num2++)
					{
						flag = true;
						int g_NoQColumns4 = Globals_Renamed.g_NoQColumns;
						for (num = 0; num <= g_NoQColumns4; num++)
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
							if (BuildForm.IsColPattern(text4, "MDBNOT"))
							{
								continue;
							}
							text6 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
							text7 = Globals_Renamed.g_QColumns[num].Show;
							text8 = Globals_Renamed.g_QColumns[num].Comment;
							text9 = "N";
							if (Operators.CompareString(Strings.Mid(text8 + "   ", 1, 3), "***", TextCompare: false) == 0)
							{
								text9 = "Y";
							}
							text19 = Globals_Renamed.g_QColumns[num].List;
							if (Operators.CompareString(text9, "Y", TextCompare: false) == 0 && Operators.CompareString(text19, "s+" + Conversions.ToString(num2), TextCompare: false) != 0)
							{
								text9 = "N";
							}
							text3 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num].DataType));
							flag2 = false;
							if (Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) == 0)
							{
								text6 = "";
							}
							else if (Operators.CompareString(text6, "", TextCompare: false) == 0 && !BuildForm.IsColPattern(text4))
							{
								text6 = General_Procedures.Strip_Column(Strings.Trim(text4), 3);
							}
							if (!LikeOperator.LikeString(text19, "s+*", CompareMethod.Binary) || (LikeOperator.LikeString(text19, "s+*", CompareMethod.Binary) && Operators.CompareString(text19, "s+" + Strings.Trim(Conversions.ToString(num2)), TextCompare: false) <= 0))
							{
								flag2 = true;
							}
							if (!flag2 || Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) == 0 || (Operators.CompareString(Strings.Mid(text7, 1, 1), "Y", TextCompare: false) != 0 && (l_SummPlusOne <= num2 || Operators.CompareString(text7, "N:S", TextCompare: false) != 0)))
							{
								continue;
							}
							if (flag)
							{
								array4[num2] = "\r\n    '_id' : 0";
								flag = false;
							}
							if (BuildForm.IsColPattern(text4))
							{
								text4 = ChkHdrCol_Pattern(text4, 0);
								text6 = ChkHdrCol_Pattern(text4, 3);
								text26 = "";
								if (Operators.CompareString(text6, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(text6, 1, 1), "#", TextCompare: false) == 0)
								{
									text26 = text6;
									text6 = "";
								}
								text21 = "'" + text6 + "|<>|" + text26 + "' : 1";
								array4[num2] = array4[num2] + "\r\n   ," + Strings.Replace(text4, "|<>|", text21, 1, -1, CompareMethod.Text);
							}
							else
							{
								array4[num2] = array4[num2] + "\r\n   ,'" + text6 + "' : ";
								if (Operators.CompareString(text9, "Y", TextCompare: false) == 0)
								{
									text4 = Mongo_Round(text4, highestLevel, "S", num2, text3, text7);
									array4[num2] += text4;
								}
								else
								{
									array4[num2] += Mongo_Project(text6, highestLevel, "S", num2, ll_M5, text3, "", text7);
								}
							}
						}
						if (Operators.CompareString(array4[num2], "", TextCompare: false) != 0)
						{
							array4[num2] = "\r\n<!>,{ '$project':{" + array4[num2] + "\r\n  }}";
						}
					}
					flag = true;
					if (unchecked(QueryType == 1 || QueryType == 2))
					{
						int g_NoQColumns5 = Globals_Renamed.g_NoQColumns;
						for (num = 0; num <= g_NoQColumns5; num++)
						{
							if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column, "MDBNOT") || Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num].Show, 1, 1), "Y", TextCompare: false) != 0)
							{
								continue;
							}
							text8 = Strings.LCase(Strings.Trim(Globals_Renamed.g_QColumns[num].Sort));
							if (Operators.CompareString(text8, "none", TextCompare: false) != 0)
							{
								text9 = ((Operators.CompareString(text8, "asc", TextCompare: false) != 0) ? "-1" : "1");
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
								if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
								{
									text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num].Column), 3);
								}
								if (flag)
								{
									text16 = "   '" + text4 + "' : " + text9;
									flag = false;
									continue;
								}
								text16 = text16 + "\r\n  ,'" + text4 + "' : " + text9;
							}
						}
					}
					if (Operators.CompareString(text16, "", TextCompare: false) != 0)
					{
						text16 = "\r\n<!>,{ '$sort' : {\r\n" + text16 + "\r\n }}";
					}
					if ((Operators.CompareString(ll_Row_limit, "-1", TextCompare: false) != 0) & (Operators.CompareString(ll_Row_limit, "-99", TextCompare: false) != 0))
					{
						text17 = text17 + "\r\n<!>,{'$limit': " + ll_Row_limit + " }";
					}
					text2 = " ";
					text = text24;
					if (Operators.CompareString(array2[0], "", TextCompare: false) != 0)
					{
						text = text + "\r\n" + text2 + array2[0];
						text2 = "<!>,";
					}
					if (Operators.CompareString(array[0], "", TextCompare: false) != 0)
					{
						text = text + "\r\n" + text2 + array[0];
						text2 = "<!>,";
					}
					num = 1;
					do
					{
						text = text + array2[num] + array[num];
						num++;
					}
					while (num <= 9);
					text += array4[0];
					num = 1;
					do
					{
						text = text + array3[num] + array4[num];
						num++;
					}
					while (num <= 9);
					text = text + text27 + text16 + text17;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ErrorMSG = "Error generating Mongo DB Query: (" + ex2.Message + ")";
				text = "";
				ProjectData.ClearProjectError();
			}
			finally
			{
				array = null;
				array4 = null;
				array2 = null;
				array3 = null;
			}
			return text;
		}
	}

	public static string Generate_Row_Filter_Mongo(string MyMode, string List, int QueryType, int ll_TableIndex)
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
						errsource = "BuildSQL - Generate_Row_Filter_Mongo.";
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
						string text11 = "";
						string text12 = "";
						string text13 = "";
						string text14 = "";
						string text15 = "";
						string text16 = "";
						string text17 = "";
						string text18 = "";
						string text19 = "";
						string text20 = "";
						string text21 = "";
						string text22 = "";
						string MyCol = "";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						bool flag = true;
						string text23 = "";
						bool flag2 = false;
						string text24 = "";
						bool flag3 = false;
						string text25 = "";
						string text26 = "";
						string text27 = "";
						bool flag4 = false;
						int num6 = 0;
						string text28 = "";
						string text29 = "";
						string text30 = "";
						string text31 = "";
						string text32 = "";
						bool flag5 = false;
						string replacement = "";
						string text33 = "";
						string text34 = "";
						string text35 = "SQL_";
						text17 = "DATA^";
						List = Strings.UCase(List);
						text24 = Strings.Mid(List, 2, 1);
						short g_NoQFilters = Globals_Renamed.g_NoQFilters;
						for (num4 = 0; num4 <= g_NoQFilters; num4 = (short)unchecked(num4 + 1))
						{
							text13 = "";
							text15 = Globals_Renamed.g_QFilters[num4].List;
							text6 = Strings.Trim(Globals_Renamed.g_QFilters[num4].And_Renamed);
							text26 = "";
							text27 = "";
							text25 = "";
							int num7 = num4 + 1;
							int g_NoQFilters2 = Globals_Renamed.g_NoQFilters;
							for (num6 = num7; num6 <= g_NoQFilters2; num6++)
							{
								text25 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QFilters[num6].And_Renamed));
								if (Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QFilters[num6].And_Renamed) + "  ", 2), "--", TextCompare: false) != 0)
								{
									text25 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QFilters[num6].And_Renamed));
									break;
								}
							}
							if (Operators.CompareString(text25, "", TextCompare: false) == 0)
							{
								text25 = "AND";
							}
							flag2 = false;
							if (Operators.CompareString(Strings.Mid(text6 + "  ", 1, 2), "--", TextCompare: false) != 0)
							{
								if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
								{
									if (Operators.CompareString(List, "L0", TextCompare: false) == 0)
									{
										if (!LikeOperator.LikeString(Strings.UCase(text15), "@IF@*", CompareMethod.Binary) && !LikeOperator.LikeString(text15, "s+*", CompareMethod.Binary))
										{
											flag2 = true;
										}
									}
									else if (LikeOperator.LikeString(List, "L*", CompareMethod.Binary) && Operators.CompareString(Strings.UCase(text15), "@IF@" + text24, TextCompare: false) == 0)
									{
										flag2 = true;
									}
									else if (LikeOperator.LikeString(List, "S*", CompareMethod.Binary) && Operators.CompareString(Strings.UCase(text15), Strings.UCase("s+") + text24, TextCompare: false) == 0)
									{
										flag2 = true;
									}
								}
								else if (LikeOperator.LikeString(Strings.UCase(text15), Strings.UCase(List), CompareMethod.Binary))
								{
									flag2 = true;
								}
							}
							if (flag2)
							{
								text28 = " : ";
								text29 = " : ";
								text30 = "'";
								text31 = "{";
								text32 = "}}";
								text33 = "";
								text = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num4].DataType, 1, 1)));
								text7 = Strings.Trim(Globals_Renamed.g_QFilters[num4].ParentO);
								text4 = Strings.Trim(Globals_Renamed.g_QFilters[num4].Column);
								text5 = text4;
								text13 = Globals_Renamed.g_QFilters[num4].ColHead;
								text8 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QFilters[num4].Operator_Renamed));
								text3 = Strings.Trim(Globals_Renamed.g_QFilters[num4].Value1);
								text2 = Strings.Trim(Globals_Renamed.g_QFilters[num4].Value2);
								if (Operators.CompareString(Strings.UCase(text3), "&PROMPT&", TextCompare: false) == 0)
								{
									text2 = "";
									flag5 = true;
								}
								else
								{
									flag5 = false;
								}
								text12 = "";
								text11 = Strings.Trim(Globals_Renamed.g_QFilters[num4].ParenC);
								if (Operators.CompareString(text7, "((", TextCompare: false) == 0 || Operators.CompareString(text7, "(((", TextCompare: false) == 0 || Operators.CompareString(text11, "))", TextCompare: false) == 0 || Operators.CompareString(text11, ")))", TextCompare: false) == 0 || LikeOperator.LikeString(text6, "*NOT*", CompareMethod.Binary))
								{
									flag4 = true;
									text22 = "Error: Neither double nor triple open/close parentheses nor NOT clauses are supported for Mongo DB at this time";
								}
								else if ((Operators.CompareString(Strings.UCase(text6), "AND", TextCompare: false) == 0 || Operators.CompareString(text6, "", TextCompare: false) == 0) && Operators.CompareString(text7, "(", TextCompare: false) == 0 && !flag3 && Operators.CompareString(text25, "OR", TextCompare: false) == 0)
								{
									if (Operators.CompareString(text11, ")", TextCompare: false) != 0)
									{
										flag3 = true;
										text26 = (flag ? "  " : " ,");
										text26 += "{'$or':[";
										flag = true;
									}
									else
									{
										flag4 = true;
										text22 = "Error: You have an Open and Close Parenthesis on the Filter Grid on the same line which makes no sense";
									}
								}
								else if (Operators.CompareString(text6, "OR", TextCompare: false) == 0 && Operators.CompareString(text7, "", TextCompare: false) == 0 && flag3 && Operators.CompareString(text25, "AND", TextCompare: false) == 0 && Operators.CompareString(text11, ")", TextCompare: false) == 0)
								{
									flag3 = false;
									text27 = "\r\n  ]}";
								}
								else if (flag3 && (Operators.CompareString(text6, "OR", TextCompare: false) != 0 || Operators.CompareString(text7, "", TextCompare: false) != 0 || Operators.CompareString(text11, ")", TextCompare: false) != 0))
								{
									flag4 = true;
									text22 = "Error: An invalid OR clause was generated for MongoDB";
								}
								else if (Operators.CompareString(text6, "OR", TextCompare: false) == 0 && !flag3)
								{
									flag4 = true;
									text22 = "Error: An OR clause unbounded by parentheses was found in your MongoDB Filter";
								}
								else if (Operators.CompareString(text6, "AND", TextCompare: false) == 0 && Operators.CompareString(text11, ")", TextCompare: false) == 0)
								{
									flag4 = true;
									text22 = "Error: An unexpected AND clause and closing parenthesis was found in your MongoDB Filter";
								}
								if (!flag4 && BuildForm.IsValidFilter("2", text4, text8, text3, text2))
								{
									switch (text8)
									{
									case "=":
										text9 = "'$eq'";
										text33 = "=";
										break;
									case ">":
										text9 = "'$gt'";
										text33 = ">";
										break;
									case ">=":
										text9 = "'$gte'";
										text33 = ">=";
										break;
									case "<":
										text9 = "'$lt'";
										text33 = "<";
										break;
									case "<=":
										text9 = "'$lte'";
										text33 = "<=";
										break;
									case "!=":
									case "<>":
										text9 = "'$ne'";
										text33 = "!=";
										break;
									case "BETWEEN":
										if (!flag5)
										{
											text9 = "'$gte'";
											text12 = ", '$lte' : ";
										}
										else
										{
											text9 = "'$gte'";
											text12 = "";
										}
										text33 = "$between";
										break;
									case "NOT BETWEEN":
										if (!flag5)
										{
											text9 = "'$lt'";
											text12 = ", '$gt' : ";
										}
										else
										{
											text9 = "'$lt'";
											text12 = "";
										}
										text33 = "$not $between";
										break;
									case "IN":
										text9 = "'$in'";
										text33 = "$in";
										break;
									case "NOT IN":
										text9 = "'$nin'";
										text33 = "$not $in";
										break;
									case "REGEX":
									case "NOT REGEX":
										if (!flag5)
										{
											text3 = Strings.Replace(text3, "''", "&quot;", 1, -1, CompareMethod.Text);
											text3 = Strings.Replace(text3, "'", "", 1, -1, CompareMethod.Text);
											text3 = Strings.Replace(text3, "&quot;", "''", 1, -1, CompareMethod.Text);
											text3 = "'" + text3 + "', '$options': 'i'";
										}
										if (Operators.CompareString(text8, "REGEX", TextCompare: false) == 0)
										{
											text9 = "'$regex'";
											text33 = "regex";
										}
										else
										{
											text9 = "'$not' : { '$regex'";
											text32 = "}}}";
											text33 = "not regex";
										}
										break;
									case "IS NULL":
										text3 = "'" + text4 + "' : {'$eq' : None}}";
										text9 = "";
										text12 = "";
										text4 = "";
										text28 = "";
										text29 = "";
										text30 = "";
										text31 = "";
										text32 = "";
										break;
									case "IS NOT NULL":
										text3 = "'" + text4 + "' : {'$ne' : None}}";
										text9 = "";
										text12 = "";
										text4 = "";
										text28 = "";
										text29 = "";
										text30 = "";
										text31 = "";
										text32 = "";
										break;
									default:
										text9 = text8;
										break;
									}
									Add_Mongo_Date_Syntax(ref text3, ref text2, text, text8);
									if (LikeOperator.LikeString(text8, "* GROUP", CompareMethod.Binary))
									{
										num3 = (short)Strings.InStr(text8, " ");
										text8 = Strings.Mid(text8, 1, num3 - 1);
										if (Operators.CompareString(text8, "IN", TextCompare: false) == 0 || Operators.CompareString(text8, "NOT-IN", TextCompare: false) == 0)
										{
											text9 = "$in";
											text29 = "";
										}
										else if (Operators.CompareString(text8, "REGEX", TextCompare: false) == 0 || Operators.CompareString(text8, "NOT-REGEX", TextCompare: false) == 0)
										{
											text9 = "$regex";
											text29 = "";
										}
										text20 = "";
										Chk_Temp_Inc("G", ref text3, ref text20);
										if (Operators.CompareString(text20, "", TextCompare: false) != 0)
										{
											if (QueryType >= 2)
											{
												text22 = "Error: You cannot at this time create a query involving summaries and use of Incremental In Group due to the possibility of incorrect results.";
												break;
											}
											text20 = "->" + text20;
										}
										BuildForm.Extract_IG_ColNo(ref text3, ref MyCol);
										text3 = "\r\n" + text35 + "Get_CSV_List(\"" + text3 + text20 + "\", \"" + MyCol + "\", \"" + text4 + " " + text9 + "\")";
										switch (text8)
										{
										case "IN":
											text12 = "\r\n   ]";
											text9 = "'$in' : [";
											break;
										case "NOT-IN":
											text12 = "\r\n   ]";
											text9 = "'$nin' : [";
											break;
										case "NOT-REGEX":
											text9 = "'$nor': [";
											text12 = "";
											text4 = "";
											text28 = "";
											text30 = "";
											text31 = "";
											text32 = "\r\n   ]}";
											break;
										default:
											text9 = "'$or': [";
											text12 = "";
											text4 = "";
											text28 = "";
											text30 = "";
											text31 = "";
											text32 = "\r\n   ]}";
											break;
										}
									}
									else if (LikeOperator.LikeString(text8, "*TEMP*", CompareMethod.Binary))
									{
										text29 = "";
										num3 = (short)Strings.InStr(text8, " ");
										if (num3 != 0)
										{
											text8 = Strings.Trim(Strings.Mid(text8, 1, num3 - 1));
										}
										text9 = ((Operators.CompareString(text8, "IN", TextCompare: false) != 0) ? "$regex" : "$in");
										text20 = "";
										Chk_Temp_Inc("T", ref text3, ref text20);
										if (Operators.CompareString(text20, "", TextCompare: false) != 0)
										{
											text20 = "->" + text20;
										}
										if ((short)Strings.InStr(text3, "{") != 0)
										{
											num5 = (short)Strings.InStr(text3, ":");
											text14 = ((num5 == 0) ? text3 : Strings.Mid(text3, 1, num5 - 1));
											text14 = General_Procedures.Get_Obj_Alias("F->T", General_Procedures.Get_Node_Value(text14));
											text3 = Set_TT_Name(text14, "Mongo");
											if (Strings.InStr(text3, "\\") == 0)
											{
												text3 = ".\\" + text3;
											}
											text3 += text20;
										}
										text14 = "";
										if ((Operators.CompareString(text8, "IN", TextCompare: false) == 0) & ((Operators.CompareString(Strings.UCase(text), "N", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(text), "F", TextCompare: false) == 0)))
										{
											text14 = "N";
										}
										text3 = "\r\n" + text35 + "Get_CSV_List(\"" + text3 + "\", " + text2 + ", \"" + text4 + " " + text9 + text14 + "\")";
										text2 = "";
										if (Operators.CompareString(text8, "IN", TextCompare: false) == 0)
										{
											text12 = "\r\n   ]";
											text9 = "'$in' : [";
										}
										else
										{
											text12 = "";
											text4 = "";
											text28 = "";
											text30 = "";
											text31 = "";
											text9 = "'$or': [";
											text32 = "\r\n   ]}";
										}
									}
									else if (LikeOperator.LikeString(text8, "*IN*", CompareMethod.Binary))
									{
										text3 = Process_In_List(text3, "", "", "M");
									}
									else if (Operators.CompareString(text8, "REGEX LIST", TextCompare: false) == 0 || Operators.CompareString(text8, "NOT REGEX LIST", TextCompare: false) == 0)
									{
										text34 = ((Operators.CompareString(text8, "REGEX LIST", TextCompare: false) != 0) ? "'$nor'" : "'$or'");
										text3 = (flag5 ? (text34 + ": [\r\n" + text3) : (text34 + ": [\r\n" + BuildForm.Process_Regex_List(text4, text3)));
										text33 = Strings.LCase(text8);
										text8 = "";
										text28 = "";
										text29 = "";
										text30 = "";
										text31 = "";
										text4 = "";
										text9 = "";
										text32 = "\r\n  ]}";
									}
									if (flag5)
									{
										text16 = "";
										text18 = "";
										text19 = "";
										text18 = Globals_Renamed.g_QFilters[num4].Value2;
										if (Strings.InStr(text5, "^") != 0)
										{
											text5 = "MyData";
										}
										num3 = (short)Strings.InStr(text18, "^");
										if (num3 != 0)
										{
											text19 = Strings.Trim(Strings.Mid(text18, num3 + 1));
											num5 = (short)Strings.InStr(text19, "/");
											if ((Operators.CompareString(Strings.Trim(text19), "/", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(text19), "", TextCompare: false) == 0))
											{
												text16 = "";
												text21 = "?:{" + Conversions.ToString(ll_TableIndex) + "}: ";
											}
											else if (num5 != 0)
											{
												text16 = Strings.Trim(Strings.Mid(text19, 1, num5 - 1));
												text21 = "~" + text19;
											}
											else if (Operators.CompareString(text19, "", TextCompare: false) != 0)
											{
												text16 = Strings.Trim(text19);
												text21 = "?:{" + Conversions.ToString(ll_TableIndex) + "}: ";
											}
											if ((Operators.CompareString(Strings.Mid(text16, 1, 1), "~", TextCompare: false) == 0) & (Strings.Len(text16) >= 2))
											{
												text16 = Strings.Mid(text16, 2);
											}
											text18 = Strings.Trim(Strings.Mid(text18, 1, num3 - 1));
										}
										if (!LikeOperator.LikeString(text33, "*Regex List*", CompareMethod.Binary) && Operators.CompareString(text18, "", TextCompare: false) == 0)
										{
											text18 = text13;
										}
										replacement = "&PROMPT&(" + text17 + text5 + "^" + text33 + "^" + text + "^" + text18 + "^" + text21 + "^" + text16 + ")&PROMPT&";
									}
									if (flag)
									{
										text23 = " ";
										flag = false;
									}
									else
									{
										text23 = ",";
									}
									if (flag5)
									{
										text3 = Strings.Replace(text3, "&PROMPT&", replacement, 1, -1, CompareMethod.Text);
									}
									text22 = text22 + "\r\n" + text26 + "  " + text23 + "{ " + text30 + text4 + text30 + text28 + text31 + text9 + text29 + text3 + text12 + text2 + " " + text32 + text27;
								}
							}
							if (LikeOperator.LikeString(text22, "Error:*", CompareMethod.Binary))
							{
								break;
							}
						}
						if (flag3 && !LikeOperator.LikeString(text22, "Error:*", CompareMethod.Binary))
						{
							text22 = "Error: There is a missing close parenthesis in your MongoFB Filter";
						}
						result = text22;
						goto end_IL_0001;
					}
					case 5352:
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
					goto IL_151e;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5352;
				continue;
			}
			break;
			IL_151e:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void Add_Mongo_Date_Syntax(ref string MyVal, ref string MyVal2, string MyDT, string MyOpr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 263:
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
							goto IL_0016;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_0040;
						case 7:
							goto IL_004e;
						case 8:
						case 9:
							goto IL_0064;
						case 10:
							goto IL_007e;
						case 11:
							goto IL_008c;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 14:
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0064:
					num2 = 9;
					if (!LikeOperator.LikeString(Strings.UCase(MyOpr), "*BETWEEN*", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_007e;
					IL_007e:
					num2 = 10;
					text = General_Procedures.StripQuotesAndDates(text, MyDT, 0);
					goto IL_008c;
					IL_004e:
					num2 = 7;
					MyVal = "dt.datetime.strptime('" + text2 + "','%Y-%m-%d %H:%M:%S')";
					goto IL_0064;
					IL_008c:
					num2 = 11;
					if (!Information.IsDate(text))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text2 = MyVal;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text = MyVal2;
					goto IL_0016;
					IL_0016:
					num2 = 4;
					if (Operators.CompareString(Strings.UCase(MyDT), "O", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0035:
					num2 = 5;
					text2 = General_Procedures.StripQuotesAndDates(text2, MyDT, 0);
					goto IL_0040;
					IL_0040:
					num2 = 6;
					if (Information.IsDate(text2))
					{
						goto IL_004e;
					}
					goto IL_0064;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				MyVal2 = "dt.datetime.strptime('" + text + "','%Y-%m-%d %H:%M:%S')";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 263;
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

	public static bool IsXEUS_DIS(string MyNode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 136:
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
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyNode = Strings.UCase(MyNode);
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (!LikeOperator.LikeString(MyNode, "*XEUS*", CompareMethod.Binary) && !LikeOperator.LikeString(MyNode, "*_DIS", CompareMethod.Binary) && !LikeOperator.LikeString(MyNode, "*_CAFE", CompareMethod.Binary) && !LikeOperator.LikeString(MyNode, "*_DIS_*", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 136;
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

	public static bool IsUBER_MAO(string MyNode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 108:
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
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyNode = Strings.UCase(MyNode);
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (!LikeOperator.LikeString(MyNode, "*_PROD_ARIES", CompareMethod.Binary) && !LikeOperator.LikeString(MyNode, "*_PROD_MARS", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 108;
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

	public static string ChkHdrCol_Pattern(string Myhdr, int MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		string text = default(string);
		string result = default(string);
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
					case 1023:
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
								goto IL_0046;
							case 4:
								goto IL_004b;
							case 5:
								goto IL_0050;
							case 6:
								goto IL_0055;
							case 7:
								goto IL_005a;
							case 8:
								goto IL_0063;
							case 9:
								goto IL_0079;
							case 10:
								goto IL_008a;
							case 11:
								goto IL_009b;
							case 12:
								goto IL_00af;
							case 13:
								goto IL_00c0;
							case 14:
								goto IL_00ce;
							case 15:
								goto IL_00e3;
							case 16:
								goto IL_00f0;
							case 17:
								goto IL_0101;
							case 18:
								goto IL_010f;
							case 20:
								goto IL_0132;
							case 24:
								goto IL_0143;
							case 28:
								goto IL_0157;
							case 29:
								goto IL_0169;
							case 30:
								goto IL_017a;
							case 31:
								goto IL_0187;
							case 32:
								goto IL_0194;
							case 33:
								goto IL_01a6;
							case 34:
								goto IL_01c9;
							case 36:
								goto IL_01e1;
							case 39:
								goto IL_01f3;
							case 40:
								goto IL_0203;
							case 41:
								goto IL_0215;
							case 42:
								goto IL_0226;
							case 43:
								goto IL_0233;
							case 45:
								goto IL_0255;
							case 46:
								goto IL_0262;
							case 48:
								goto IL_027f;
							case 49:
								goto IL_028c;
							case 51:
								goto IL_029f;
							case 52:
								goto IL_02af;
							case 53:
								goto IL_02c1;
							case 54:
								goto IL_02cf;
							case 19:
							case 21:
							case 22:
							case 23:
							case 25:
							case 26:
							case 27:
							case 35:
							case 37:
							case 38:
							case 44:
							case 47:
							case 50:
							case 55:
							case 56:
							case 57:
							case 58:
							case 59:
							case 60:
							case 61:
							case 62:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 63:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00af:
						num2 = 12;
						num5 = Strings.InStr(Myhdr, "<;>");
						goto IL_00c0;
						IL_00c0:
						num2 = 13;
						if (num5 > 1)
						{
							goto IL_00ce;
						}
						goto IL_0143;
						IL_009b:
						num2 = 11;
						Myhdr = Strings.Trim(Strings.Mid(Myhdr, num6 + 3));
						goto IL_00af;
						IL_00ce:
						num2 = 14;
						Myhdr = Strings.Trim(Strings.Mid(Myhdr, 1, num5 - 1));
						goto IL_00e3;
						IL_000b:
						num2 = 2;
						if (!BuildForm.IsColPattern(Myhdr) || ((MyMode < 2 || MyMode > 7) && (MyMode != 0 || Strings.InStr(Myhdr, "<;>|<>|<;>") != 0)))
						{
							break;
						}
						goto IL_0046;
						IL_00e3:
						num2 = 15;
						if (MyMode != 4)
						{
							break;
						}
						goto IL_00f0;
						IL_0101:
						num2 = 17;
						if (num7 > 1)
						{
							goto IL_010f;
						}
						goto IL_0132;
						IL_00f0:
						num2 = 16;
						num7 = Strings.InStr(Myhdr, ":");
						goto IL_0101;
						IL_010f:
						num2 = 18;
						Myhdr = Strings.Trim(Strings.Mid(Myhdr, 1, num7 - 1)) + ".";
						break;
						IL_0046:
						num2 = 3;
						num6 = 0;
						goto IL_004b;
						IL_004b:
						num2 = 4;
						num5 = 0;
						goto IL_0050;
						IL_0050:
						num2 = 5;
						num7 = 0;
						goto IL_0055;
						IL_0055:
						num2 = 6;
						num8 = 0;
						goto IL_005a;
						IL_005a:
						num2 = 7;
						text = "";
						goto IL_0063;
						IL_0063:
						num2 = 8;
						if (MyMode == 4 || MyMode == 5)
						{
							goto IL_0079;
						}
						goto IL_0157;
						IL_0132:
						num2 = 20;
						Myhdr = "";
						break;
						IL_0157:
						num2 = 28;
						num6 = Strings.InStrRev(Myhdr, "<;>");
						goto IL_0169;
						IL_0169:
						num2 = 29;
						if (num6 == 0)
						{
							break;
						}
						goto IL_017a;
						IL_017a:
						num2 = 30;
						text = Strings.Mid(Myhdr, num6);
						goto IL_0187;
						IL_0187:
						num2 = 31;
						if (MyMode == 3)
						{
							goto IL_0194;
						}
						goto IL_01f3;
						IL_0194:
						num2 = 32;
						Myhdr = Strings.Trim(Strings.Mid(text, 4));
						goto IL_01a6;
						IL_01a6:
						num2 = 33;
						if (Operators.CompareString(Myhdr, "]]", TextCompare: false) != 0 && Strings.Len(Myhdr) > 2)
						{
							goto IL_01c9;
						}
						goto IL_01e1;
						IL_0143:
						num2 = 24;
						Myhdr = "";
						break;
						IL_01c9:
						num2 = 34;
						Myhdr = Strings.Mid(Myhdr, 1, Strings.Len(Myhdr) - 2);
						break;
						IL_01e1:
						num2 = 36;
						Myhdr = "";
						break;
						IL_01f3:
						num2 = 39;
						Myhdr = Strings.Mid(Myhdr, 1, num6 - 1);
						goto IL_0203;
						IL_0203:
						num2 = 40;
						num5 = Strings.InStrRev(Myhdr, "<;>");
						goto IL_0215;
						IL_0215:
						num2 = 41;
						if (num5 == 0)
						{
							break;
						}
						goto IL_0226;
						IL_0226:
						num2 = 42;
						if (MyMode == 0)
						{
							goto IL_0233;
						}
						goto IL_0255;
						IL_0233:
						num2 = 43;
						Myhdr = Strings.Mid(Myhdr, 1, num5 - 1) + "<;>|<>|" + text;
						break;
						IL_0255:
						num2 = 45;
						if (MyMode == 6)
						{
							goto IL_0262;
						}
						goto IL_027f;
						IL_0262:
						num2 = 46;
						Myhdr = Strings.Mid(Myhdr, 1, num5 - 1) + "<;>'|<>|'<;>]]";
						break;
						IL_027f:
						num2 = 48;
						if (MyMode == 2)
						{
							goto IL_028c;
						}
						goto IL_029f;
						IL_028c:
						num2 = 49;
						Myhdr = Strings.Mid(Myhdr, num5 + 3);
						break;
						IL_029f:
						num2 = 51;
						Myhdr = Strings.Mid(Myhdr, 1, num5 - 1);
						goto IL_02af;
						IL_02af:
						num2 = 52;
						num7 = Strings.InStrRev(Myhdr, "<;>");
						goto IL_02c1;
						IL_02c1:
						num2 = 53;
						if (num7 == 0)
						{
							break;
						}
						goto IL_02cf;
						IL_02cf:
						num2 = 54;
						Myhdr = Strings.Mid(Myhdr, num7 + 3);
						break;
						IL_0079:
						num2 = 9;
						num6 = Strings.InStr(Myhdr, "<;>");
						goto IL_008a;
						IL_008a:
						num2 = 10;
						if (num6 == 0)
						{
							break;
						}
						goto IL_009b;
						end_IL_0001_2:
						break;
					}
					num2 = 62;
					result = Myhdr;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1023;
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

	public static void Special_Process_List(string MyView)
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
						errsource = "FrmSQLQuery - Special_Process_Listt";
						int num3 = 0;
						MyView = Strings.LCase(MyView);
						if (LikeOperator.LikeString(MyView, "yas a*", CompareMethod.Binary))
						{
							int g_NoQColumns = Globals_Renamed.g_NoQColumns;
							for (num3 = 0; num3 <= g_NoQColumns; num3++)
							{
								Globals_Renamed.g_QColumns[num3].Column = Strings.Replace(Globals_Renamed.g_QColumns[num3].Column, "y900.", "y0.", 1, -1, CompareMethod.Text);
							}
							int g_NoQFilters = Globals_Renamed.g_NoQFilters;
							for (num3 = 0; num3 <= g_NoQFilters; num3++)
							{
								Globals_Renamed.g_QFilters[num3].Column = Strings.Replace(Globals_Renamed.g_QFilters[num3].Column, "y900.", "y0.", 1, -1, CompareMethod.Text);
							}
						}
						goto end_IL_0001;
					}
					case 232:
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
				try0001_dispatch = 232;
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

	public static string Create_Replace_Headers()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text4 = default(string);
		int g_NoQColumns = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text3;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 612:
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
							goto IL_0022;
						case 6:
							goto IL_002b;
						case 7:
							goto IL_0033;
						case 8:
							goto IL_0044;
						case 9:
							goto IL_0059;
						case 10:
							goto IL_00fb;
						case 11:
							goto IL_0105;
						case 12:
							goto IL_012d;
						case 14:
							goto IL_0147;
						case 13:
						case 15:
						case 16:
							goto IL_0169;
						case 17:
							goto IL_0182;
						case 18:
						case 19:
						case 20:
							goto IL_01be;
						case 21:
							goto IL_01d0;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0105:
					num2 = 11;
					if (Operators.CompareString(Globals_Renamed.g_QColumns[num5].Header, "", TextCompare: false) != 0)
					{
						goto IL_012d;
					}
					goto IL_0147;
					IL_012d:
					num2 = 12;
					text = Globals_Renamed.g_QColumns[num5].Header;
					goto IL_0169;
					IL_00fb:
					num2 = 10;
					text = "";
					goto IL_0105;
					IL_0147:
					num2 = 14;
					text = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num5].Column), 1);
					goto IL_0169;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text2 = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					text = "";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					text3 = "";
					goto IL_002b;
					IL_002b:
					num2 = 6;
					text4 = "";
					goto IL_0033;
					IL_0033:
					num2 = 7;
					g_NoQColumns = Globals_Renamed.g_NoQColumns;
					num5 = 0;
					goto IL_01c7;
					IL_01c7:
					if (num5 <= g_NoQColumns)
					{
						goto IL_0044;
					}
					goto IL_01d0;
					IL_01d0:
					num2 = 21;
					if (Operators.CompareString(text4, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0044:
					num2 = 8;
					text2 = Globals_Renamed.g_QColumns[num5].LHeader;
					goto IL_0059;
					IL_0059:
					num2 = 9;
					switch (text2)
					{
					case null:
					case "":
					case ".":
						goto IL_01be;
					}
					if (Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num5].Show), 1, 1), "Y", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num5].Pivot), "ROW", TextCompare: false) == 0 && Operators.CompareString(Strings.Right(Globals_Renamed.g_QColumns[num5].Column, 2), ".*", TextCompare: false) != 0)
					{
						goto IL_00fb;
					}
					goto IL_01be;
					IL_0169:
					num2 = 16;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_0182;
					}
					goto IL_01be;
					IL_0182:
					num2 = 17;
					text4 = text4 + "," + General_Procedures.Strip_CSV_Square(text, 1) + "[=]" + text2;
					goto IL_01be;
					IL_01be:
					num2 = 20;
					num5 = checked(num5 + 1);
					goto IL_01c7;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				text4 = Strings.Mid(text4, 2);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 612;
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
		return text4;
	}

	public static string Create_Header_Opt(string ll_DatabaseType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		string ll_JoinDuckDB = default(string);
		int g_NoQColumns = default(int);
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
				case 1017:
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
							goto IL_0022;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0041;
						case 9:
							goto IL_004e;
						case 8:
						case 10:
						case 11:
							goto IL_0059;
						case 12:
							goto IL_006b;
						case 13:
							goto IL_0105;
						case 14:
							goto IL_011b;
						case 15:
							goto IL_0143;
						case 17:
							goto IL_015d;
						case 18:
							goto IL_018b;
						case 16:
						case 19:
						case 20:
						case 21:
							goto IL_01af;
						case 22:
							goto IL_01ed;
						case 24:
							goto IL_021b;
						case 23:
						case 25:
						case 26:
							goto IL_0226;
						case 27:
							goto IL_024f;
						case 28:
							goto IL_0268;
						case 29:
							goto IL_0281;
						case 30:
							goto IL_029f;
						case 31:
						case 32:
							goto IL_02ae;
						case 33:
						case 34:
							goto IL_02bd;
						case 35:
							goto IL_02e6;
						case 36:
						case 37:
						case 38:
							goto IL_0305;
						case 39:
							goto IL_0317;
						case 40:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 41:
						case 42:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ed:
					num2 = 22;
					text = " (" + Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType) + ")";
					goto IL_0226;
					IL_021b:
					num2 = 24;
					text = "";
					goto IL_0226;
					IL_0305:
					num2 = 38;
					num5 = checked(num5 + 1);
					goto IL_030e;
					IL_0226:
					num2 = 26;
					if (BuildForm.IsColPattern(text2) && Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) != 0)
					{
						goto IL_024f;
					}
					goto IL_02bd;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text2 = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					text = "";
					goto IL_0022;
					IL_0022:
					num2 = 5;
					text3 = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
					{
						goto IL_0041;
					}
					goto IL_004e;
					IL_0041:
					num2 = 7;
					ll_JoinDuckDB = "Y";
					goto IL_0059;
					IL_004e:
					num2 = 9;
					ll_JoinDuckDB = "N";
					goto IL_0059;
					IL_0059:
					num2 = 11;
					g_NoQColumns = Globals_Renamed.g_NoQColumns;
					num5 = 0;
					goto IL_030e;
					IL_030e:
					if (num5 <= g_NoQColumns)
					{
						goto IL_006b;
					}
					goto IL_0317;
					IL_0317:
					num2 = 39;
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_006b:
					num2 = 12;
					if ((Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num5].Show), 1, 1), "Y", TextCompare: false) == 0) & (((Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0) && Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num5].Pivot), "ROW", TextCompare: false) == 0) || (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) != 0 && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) != 0)))
					{
						goto IL_0105;
					}
					goto IL_0305;
					IL_0281:
					num2 = 29;
					if (Strings.InStr(Strings.LCase(text2), "<~sqo~>") != 0)
					{
						goto IL_029f;
					}
					goto IL_02ae;
					IL_0268:
					num2 = 28;
					text2 = Strings.Replace(text2, "Column-Pattern->[[M<;>", "Column-Pattern->[[B<;>", 1, -1, CompareMethod.Text);
					goto IL_0281;
					IL_024f:
					num2 = 27;
					text2 = Strings.Replace(text2, "Column-Pattern->[[N<;>", "Column-Pattern->[[B<;>", 1, -1, CompareMethod.Text);
					goto IL_0268;
					IL_02ae:
					num2 = 32;
					text2 = ChkHdrCol_Pattern(text2, 0);
					goto IL_02bd;
					IL_02bd:
					num2 = 34;
					if (!BuildForm.IsColPattern(text2) || Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) != 0)
					{
						goto IL_02e6;
					}
					goto IL_0305;
					IL_029f:
					num2 = 30;
					text2 = BuildForm.Repl_Squiggly(text2, 1);
					goto IL_02ae;
					IL_02e6:
					num2 = 35;
					text3 = text3 + "," + General_Procedures.Strip_CSV_Square(text2, 1, ll_JoinDuckDB) + text;
					goto IL_0305;
					IL_0105:
					num2 = 13;
					text2 = "Unknown_" + Conversions.ToString(num5);
					goto IL_011b;
					IL_011b:
					num2 = 14;
					if (Operators.CompareString(Globals_Renamed.g_QColumns[num5].Header, "", TextCompare: false) != 0)
					{
						goto IL_0143;
					}
					goto IL_015d;
					IL_0143:
					num2 = 15;
					text2 = Globals_Renamed.g_QColumns[num5].Header;
					goto IL_01af;
					IL_015d:
					num2 = 17;
					if (Operators.CompareString(Strings.Right(Globals_Renamed.g_QColumns[num5].Column, 2), ".*", TextCompare: false) != 0)
					{
						goto IL_018b;
					}
					goto IL_01af;
					IL_018b:
					num2 = 18;
					text2 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num5].Column), 1);
					goto IL_01af;
					IL_01af:
					num2 = 21;
					if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType), "q", TextCompare: false) == 0)
					{
						goto IL_01ed;
					}
					goto IL_021b;
					end_IL_0001_2:
					break;
				}
				num2 = 40;
				text3 = Strings.Mid(text3, 2);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1017;
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
		return text3;
	}

	public static string Replace_ge_CRLF(string MyExpr, string ll_DatabaseType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string replacement = default(string);
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
				case 119:
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
							goto IL_002b;
						case 5:
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					replacement = " ";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_002b;
					IL_002b:
					num2 = 4;
					replacement = "\r\n";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = Strings.Replace(MyExpr, "!!!!!", replacement, 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 119;
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

	public static bool Check_UN_Missing(string MyInNode, string MyUsr, int ShowUN)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool result = default(bool);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				bool flag;
				bool flag2;
				bool flag3;
				int num5;
				string text;
				string[] array;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 190:
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
							goto IL_001a;
						case 6:
							goto IL_001f;
						case 7:
							goto IL_0028;
						case 8:
							goto IL_002d;
						case 9:
							goto IL_0041;
						case 10:
							goto IL_0061;
						case 11:
						case 12:
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002d:
					num2 = 8;
					if (!LikeOperator.LikeString(MyUsr, "*$ERROR$*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0041;
					IL_0041:
					num2 = 9;
					Interaction.MsgBox("You have not yet set a user name and password for node: " + MyInNode + ". Choose menu option Connect -> Set Credentials to do so.", MsgBoxStyle.Exclamation, "Missing User Name and Password");
					goto IL_0061;
					IL_0028:
					num2 = 7;
					flag = false;
					goto IL_002d;
					IL_0061:
					num2 = 10;
					result = true;
					break;
					IL_000b:
					num2 = 2;
					flag2 = false;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					flag3 = false;
					goto IL_0015;
					IL_0015:
					num2 = 4;
					array = null;
					goto IL_001a;
					IL_001a:
					num2 = 5;
					num5 = 0;
					goto IL_001f;
					IL_001f:
					num2 = 6;
					text = "";
					goto IL_0028;
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
				try0001_dispatch = 190;
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

	public static bool Validate_UN_Node(string MyMode, string MyNode, string IniSearch, ref bool MyBool, string MyMsg, string fDom = "")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool result = default(bool);
		string MyErr = default(string);
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
				case 895:
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
							goto IL_0030;
						case 5:
							goto IL_003a;
						case 7:
							goto IL_00b3;
						case 8:
							goto IL_00ea;
						case 9:
							goto IL_00ee;
						case 11:
							goto IL_00f8;
						case 12:
							goto IL_0118;
						case 15:
							goto IL_0124;
						case 16:
							goto IL_013c;
						case 17:
							goto IL_0141;
						case 19:
							goto IL_014b;
						case 20:
							goto IL_0164;
						case 21:
							goto IL_017c;
						case 22:
							goto IL_01aa;
						case 25:
							goto IL_01b6;
						case 26:
							goto IL_01cf;
						case 27:
							goto IL_01d4;
						case 29:
							goto IL_01de;
						case 30:
							goto IL_01f7;
						case 31:
							goto IL_020f;
						case 32:
							goto IL_023d;
						case 35:
							goto IL_0246;
						case 36:
							goto IL_025e;
						case 37:
							goto IL_0263;
						case 39:
							goto IL_026d;
						case 42:
							goto IL_0276;
						case 43:
							goto IL_028e;
						case 44:
							goto IL_0293;
						case 46:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 10:
						case 13:
						case 14:
						case 18:
						case 23:
						case 24:
						case 28:
						case 33:
						case 34:
						case 38:
						case 40:
						case 41:
						case 45:
						case 47:
						case 48:
						case 49:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ee:
					num2 = 9;
					MyBool = true;
					goto end_IL_0001_3;
					IL_00f8:
					num2 = 11;
					Interaction.MsgBox(Strings.Replace(MyMsg, "@NODE@", MyNode, 1, -1, CompareMethod.Text), MsgBoxStyle.Exclamation, "Access Denied");
					goto IL_0118;
					IL_00ea:
					num2 = 8;
					result = false;
					goto IL_00ee;
					IL_0118:
					num2 = 12;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					MyErr = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(Strings.UCase(MyMode), "ROR", TextCompare: false) == 0)
					{
						goto IL_0030;
					}
					goto IL_003a;
					IL_0030:
					num2 = 4;
					MyMode = "OR";
					goto IL_003a;
					IL_003a:
					num2 = 5;
					switch (Strings.UCase(MyMode))
					{
					case "I":
						break;
					case "R":
						goto IL_0124;
					case "OR":
					case "LIKE-OR":
						goto IL_01b6;
					case "C":
						goto IL_0246;
					case "CLIKE":
						goto IL_0276;
					default:
						goto end_IL_0001_3;
					}
					goto IL_00b3;
					IL_0276:
					num2 = 42;
					if (!General_Procedures.Verify_Roles(IniSearch, ref MyErr, "LIKE-OR", fDom))
					{
						break;
					}
					goto IL_028e;
					IL_028e:
					num2 = 43;
					result = false;
					goto IL_0293;
					IL_0293:
					num2 = 44;
					MyBool = true;
					goto end_IL_0001_3;
					IL_0246:
					num2 = 35;
					if (General_Procedures.Verify_Roles(IniSearch, ref MyErr, "AND", fDom))
					{
						goto IL_025e;
					}
					goto IL_026d;
					IL_025e:
					num2 = 36;
					result = false;
					goto IL_0263;
					IL_0263:
					num2 = 37;
					MyBool = true;
					goto end_IL_0001_3;
					IL_026d:
					num2 = 39;
					result = true;
					goto end_IL_0001_3;
					IL_01b6:
					num2 = 25;
					if (General_Procedures.Verify_Roles(IniSearch, ref MyErr, Strings.UCase(MyMode), fDom))
					{
						goto IL_01cf;
					}
					goto IL_01de;
					IL_01cf:
					num2 = 26;
					result = false;
					goto IL_01d4;
					IL_01d4:
					num2 = 27;
					MyBool = true;
					goto end_IL_0001_3;
					IL_01de:
					num2 = 29;
					if (Operators.CompareString(MyErr, "", TextCompare: false) != 0)
					{
						goto IL_01f7;
					}
					goto IL_020f;
					IL_01f7:
					num2 = 30;
					MyMsg = MyMsg + " - (" + MyErr + ")";
					goto IL_020f;
					IL_020f:
					num2 = 31;
					Interaction.MsgBox(Strings.Replace(Strings.Replace(MyMsg, "@NODE@", MyNode, 1, -1, CompareMethod.Text), "@ROLE@", IniSearch, 1, -1, CompareMethod.Text), MsgBoxStyle.Exclamation, "Access Denied");
					goto IL_023d;
					IL_023d:
					num2 = 32;
					result = true;
					goto end_IL_0001_3;
					IL_0124:
					num2 = 15;
					if (General_Procedures.Verify_Roles(IniSearch, ref MyErr, "AND", fDom))
					{
						goto IL_013c;
					}
					goto IL_014b;
					IL_013c:
					num2 = 16;
					result = false;
					goto IL_0141;
					IL_0141:
					num2 = 17;
					MyBool = true;
					goto end_IL_0001_3;
					IL_014b:
					num2 = 19;
					if (Operators.CompareString(MyErr, "", TextCompare: false) != 0)
					{
						goto IL_0164;
					}
					goto IL_017c;
					IL_0164:
					num2 = 20;
					MyMsg = MyMsg + " - (" + MyErr + ")";
					goto IL_017c;
					IL_017c:
					num2 = 21;
					Interaction.MsgBox(Strings.Replace(Strings.Replace(MyMsg, "@NODE@", MyNode, 1, -1, CompareMethod.Text), "@ROLE@", IniSearch, 1, -1, CompareMethod.Text), MsgBoxStyle.Exclamation, "Access Denied");
					goto IL_01aa;
					IL_01aa:
					num2 = 22;
					result = true;
					goto end_IL_0001_3;
					IL_00b3:
					num2 = 7;
					if (Operators.CompareString(Strings.UCase(General_Procedures.Get_Ini_Data(IniSearch, Globals_Renamed.gWinuser.ToLower(), "N", 10, "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Config\\sec\\sec.ini")), "Y", TextCompare: false) == 0)
					{
						goto IL_00ea;
					}
					goto IL_00f8;
					end_IL_0001_2:
					break;
				}
				num2 = 46;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 895;
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

	public static string Process_Hdr(string ListToProcess, string CBBegin, string CBEnd, ref string gProcessHeader)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text7 = default(string);
		string[] DynArray = default(string[]);
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
						errsource = "BuildSQL - Process_Hdr";
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						string text6 = "";
						text7 = "";
						num3 = Strings.InStr(Strings.Mid(ListToProcess, 2), "&");
						if (num3 == 0)
						{
							goto end_IL_0001;
						}
						text6 = "','";
						text = Strings.Trim(Strings.UCase(Strings.Mid(ListToProcess, 1, num3 + 1)));
						text2 = Strings.Trim(Strings.UCase(Strings.Mid(ListToProcess, num3 + 2)));
						if (Operators.CompareString(text, "&SELECTHDRS&", TextCompare: false) == 0)
						{
							gProcessHeader = "'*****************************************************************************";
							gProcessHeader += "\r\n'Create SQL*Plus Query Header";
							gProcessHeader += "\r\n'*****************************************************************************";
							gProcessHeader += "\r\ncommandHDR = join(array(_";
							gProcessHeader = gProcessHeader + "\r\n" + CBBegin + "SELECT " + CBEnd;
							text7 = CBBegin + "SELECT" + CBEnd;
							num5 = 1;
							text = "&SELECTHDRL&";
						}
						switch (text)
						{
						case "&SELECTHDRH&":
							gProcessHeader = gProcessHeader + "\r\n" + CBBegin + "FROM dual;" + CBEnd;
							gProcessHeader += "\r\n\"\"), vbNewLine)\r\n";
							text7 = gProcessHeader;
							gProcessHeader = "";
							break;
						case "&SELECTHDRE1&":
							text7 = text7 + "\r\n" + CBBegin + "         ||''" + CBEnd + "\r\n" + CBBegin + "FROM (" + CBEnd + "\r\n";
							break;
						case "&SELECTHDRE2&":
							text7 = text7 + CBBegin + ")" + CBEnd + "\r\n";
							break;
						case "&SELECTHDRL&":
						{
							DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
							long num6 = 0L;
							num6 = General_Procedures.ParseAndFillArray(text2, ",", ref DynArray);
							int num7 = (int)num6;
							for (num4 = 1; num4 <= num7; num4++)
							{
								if (Operators.CompareString(Strings.Trim(DynArray[num4]), "", TextCompare: false) != 0)
								{
									if (num5 == 1)
									{
										text7 = text7 + "\r\n" + CBBegin + "                 '\"\"' || " + Strings.LCase(DynArray[num4]) + " ||'\"\"' " + CBEnd;
										gProcessHeader = gProcessHeader + "\r\n" + CBBegin + "               '\"\"' || '" + Strings.UCase(DynArray[num4]) + "' || '\"\"'" + CBEnd;
										num5 = 0;
									}
									else
									{
										text7 = text7 + "\r\n" + CBBegin + "         || " + text6 + " || '\"\"' || " + Strings.LCase(DynArray[num4]) + " ||'\"\"' " + CBEnd;
										gProcessHeader = gProcessHeader + "\r\n" + CBBegin + "       || " + text6 + " || '\"\"' || '" + Strings.UCase(DynArray[num4]) + "' || '\"\"'" + CBEnd;
									}
								}
							}
							DynArray = null;
							break;
						}
						default:
						{
							if (Operators.CompareString(Strings.Mid(text2, 1, 1), "&", TextCompare: false) == 0)
							{
								text7 = text7 + CBBegin + Strings.Mid(text2, 2) + CBEnd + "\r\n";
								break;
							}
							int g_NoQColumns = Globals_Renamed.g_NoQColumns;
							for (num4 = 0; num4 <= g_NoQColumns; num4++)
							{
								if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.g_QColumns[num4].List)), text2, TextCompare: false) == 0)
								{
									text3 = Strings.UCase(Globals_Renamed.g_QColumns[num4].DataType);
									text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Column);
									text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
									if (Operators.CompareString(text5, "", TextCompare: false) == 0)
									{
										text5 = General_Procedures.Strip_Column(text4, 1);
									}
									text7 = ((!((Operators.CompareString(text3, "C", TextCompare: false) == 0) | (Operators.CompareString(text3, "Q", TextCompare: false) == 0) | (Operators.CompareString(text3, "X", TextCompare: false) == 0))) ? (text7 + "\r\n" + CBBegin + "         || " + text6 + " || " + Strings.LCase(text5) + CBEnd) : (text7 + "\r\n" + CBBegin + "         || " + text6 + " || '\"\"' || " + Strings.LCase(text5) + " ||'\"\"' " + CBEnd));
									gProcessHeader = gProcessHeader + "\r\n" + CBBegin + "       || " + text6 + " || '\"\"' || '" + Strings.UCase(text5) + "' || '\"\"'" + CBEnd;
								}
							}
							break;
						}
						}
						goto end_IL_0001;
					}
					case 1565:
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
				try0001_dispatch = 1565;
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
		return text7;
	}

	public static void Delete_TempTables()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string path = default(string);
		int num = default(int);
		int num3 = default(int);
		int num5 = default(int);
		string[] files = default(string[]);
		int num6 = default(int);
		string[] files2 = default(string[]);
		int num7 = default(int);
		string[] files3 = default(string[]);
		int num8 = default(int);
		string[] files4 = default(string[]);
		string[] files5 = default(string[]);
		int num9 = default(int);
		string[] files6 = default(string[]);
		int num10 = default(int);
		string[] files7 = default(string[]);
		int num11 = default(int);
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
						path = "";
						goto IL_000a;
					case 761:
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
								goto IL_000a;
							case 3:
								goto IL_0013;
							case 4:
								goto IL_0045;
							case 5:
								goto IL_004e;
							case 6:
								goto IL_0064;
							case 7:
								goto IL_0096;
							case 8:
								goto IL_009f;
							case 9:
								goto IL_00b5;
							case 10:
								goto IL_00be;
							case 11:
								goto IL_00e7;
							case 12:
								goto IL_00f1;
							case 13:
								goto IL_0108;
							case 14:
								goto IL_0111;
							case 15:
								goto IL_013a;
							case 16:
								goto IL_0144;
							case 17:
								goto IL_015b;
							case 18:
								goto IL_0164;
							case 19:
								goto IL_0192;
							case 20:
								goto IL_019c;
							case 21:
								goto IL_01b3;
							case 22:
								goto IL_01bc;
							case 23:
								goto IL_01ea;
							case 24:
								goto IL_01f4;
							case 25:
								goto IL_020b;
							case 26:
								goto IL_0214;
							case 27:
								goto IL_023d;
							case 28:
								goto IL_0247;
							case 29:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 30:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0144:
						num2 = 16;
						num5++;
						goto IL_014d;
						IL_0096:
						num2 = 7;
						File.Delete(path);
						goto IL_009f;
						IL_013a:
						num2 = 15;
						File.Delete(path);
						goto IL_0144;
						IL_00e7:
						num2 = 11;
						File.Delete(path);
						goto IL_00f1;
						IL_000a:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0013;
						IL_0013:
						num2 = 3;
						files = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Strings.Trim(Globals_Renamed.gSPFCache) + ".tab");
						num6 = 0;
						goto IL_0056;
						IL_0056:
						if (num6 < files.Length)
						{
							path = files[num6];
							goto IL_0045;
						}
						goto IL_0064;
						IL_0064:
						num2 = 6;
						files2 = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Strings.Trim(Globals_Renamed.gSPFCache) + ".csv");
						num7 = 0;
						goto IL_00a7;
						IL_00a7:
						if (num7 < files2.Length)
						{
							path = files2[num7];
							goto IL_0096;
						}
						goto IL_00b5;
						IL_00b5:
						num2 = 9;
						path = "";
						goto IL_00be;
						IL_00be:
						num2 = 10;
						files3 = Directory.GetFiles(Globals_Renamed.MyPCDir, Globals_Renamed.gSPFCache + "_*.ini");
						num8 = 0;
						goto IL_00fa;
						IL_00fa:
						if (num8 < files3.Length)
						{
							path = files3[num8];
							goto IL_00e7;
						}
						goto IL_0108;
						IL_0108:
						num2 = 13;
						path = "";
						goto IL_0111;
						IL_0111:
						num2 = 14;
						files4 = Directory.GetFiles(Globals_Renamed.MyPCDir, Globals_Renamed.gSPFCache + "_*.htm*");
						num5 = 0;
						goto IL_014d;
						IL_014d:
						if (num5 < files4.Length)
						{
							path = files4[num5];
							goto IL_013a;
						}
						goto IL_015b;
						IL_015b:
						num2 = 17;
						path = "";
						goto IL_0164;
						IL_0164:
						num2 = 18;
						files5 = Directory.GetFiles(Globals_Renamed.MyPCDir, "*" + Globals_Renamed.gSPFCache + "*.vg2");
						num9 = 0;
						goto IL_01a5;
						IL_01a5:
						if (num9 < files5.Length)
						{
							path = files5[num9];
							goto IL_0192;
						}
						goto IL_01b3;
						IL_01b3:
						num2 = 21;
						path = "";
						goto IL_01bc;
						IL_01bc:
						num2 = 22;
						files6 = Directory.GetFiles(Globals_Renamed.MyPCDir, "*" + Globals_Renamed.gSPFCache + "*.spf$data");
						num10 = 0;
						goto IL_01fd;
						IL_01fd:
						if (num10 < files6.Length)
						{
							path = files6[num10];
							goto IL_01ea;
						}
						goto IL_020b;
						IL_020b:
						num2 = 25;
						path = "";
						goto IL_0214;
						IL_0214:
						num2 = 26;
						files7 = Directory.GetFiles(Globals_Renamed.MyPCDir, Globals_Renamed.gSPFCache + ".RData");
						num11 = 0;
						goto IL_0250;
						IL_0250:
						if (num11 >= files7.Length)
						{
							break;
						}
						path = files7[num11];
						goto IL_023d;
						IL_004e:
						num2 = 5;
						num6++;
						goto IL_0056;
						IL_023d:
						num2 = 27;
						File.Delete(path);
						goto IL_0247;
						IL_0247:
						num2 = 28;
						num11++;
						goto IL_0250;
						IL_0045:
						num2 = 4;
						File.Delete(path);
						goto IL_004e;
						IL_01ea:
						num2 = 23;
						File.Delete(path);
						goto IL_01f4;
						IL_01f4:
						num2 = 24;
						num10++;
						goto IL_01fd;
						IL_00f1:
						num2 = 12;
						num8++;
						goto IL_00fa;
						IL_0192:
						num2 = 19;
						File.Delete(path);
						goto IL_019c;
						IL_019c:
						num2 = 20;
						num9++;
						goto IL_01a5;
						IL_009f:
						num2 = 8;
						num7++;
						goto IL_00a7;
						end_IL_0001_2:
						break;
					}
					num2 = 29;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 761;
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

	public static string Set_TT_Name(string MyAlias, string MyDBName)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
					break;
				case 109:
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
				result = Globals_Renamed.gWinuser + "_" + Strings.Trim(MyAlias) + "_" + Strings.Trim(Globals_Renamed.gSPFCache) + ".tab";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 109;
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

	public static string Get_SQLite_or_DuckDB_DT(object ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string right = default(string);
		string right2 = default(string);
		short num5 = default(short);
		short g_NoQColumns = default(short);
		string result = default(string);
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
					case 510:
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
							case 4:
								goto IL_0019;
							case 5:
								goto IL_0022;
							case 6:
								goto IL_002b;
							case 7:
								goto IL_0034;
							case 8:
								goto IL_0048;
							case 9:
								goto IL_0051;
							case 11:
								goto IL_005f;
							case 12:
								goto IL_0069;
							case 10:
							case 13:
							case 14:
								goto IL_0074;
							case 15:
								goto IL_0086;
							case 16:
								goto IL_00b2;
							case 17:
								goto IL_00cd;
							case 18:
								goto IL_00e9;
							case 19:
								goto IL_0121;
							case 20:
								goto IL_0137;
							case 21:
							case 22:
								goto IL_0170;
							case 23:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 24:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0121:
						num2 = 19;
						text = Strings.Mid(text, 2, Strings.Len(text) - 2);
						goto IL_0137;
						IL_0137:
						num2 = 20;
						text2 = text2 + "," + text + "(" + text3 + ")";
						goto IL_0170;
						IL_00e9:
						num2 = 18;
						if ((Operators.CompareString(Strings.Mid(text, 1, 1), right, TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(text, Strings.Len(text), 1), right2, TextCompare: false) == 0))
						{
							goto IL_0121;
						}
						goto IL_0137;
						IL_0170:
						num2 = 22;
						num5 = (short)unchecked(num5 + 1);
						goto IL_017a;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						text3 = "";
						goto IL_0019;
						IL_0019:
						num2 = 4;
						text2 = "";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						text = "";
						goto IL_002b;
						IL_002b:
						num2 = 6;
						text2 = "";
						goto IL_0034;
						IL_0034:
						num2 = 7;
						if (Operators.ConditionalCompareObjectEqual(ll_JoinDuckDB, "Y", TextCompare: false))
						{
							goto IL_0048;
						}
						goto IL_005f;
						IL_0048:
						num2 = 8;
						right = "\"";
						goto IL_0051;
						IL_0051:
						num2 = 9;
						right2 = "\"";
						goto IL_0074;
						IL_005f:
						num2 = 11;
						right = "[";
						goto IL_0069;
						IL_0069:
						num2 = 12;
						right2 = "]";
						goto IL_0074;
						IL_0074:
						num2 = 14;
						g_NoQColumns = Globals_Renamed.g_NoQColumns;
						num5 = 0;
						goto IL_017a;
						IL_017a:
						if (num5 > g_NoQColumns)
						{
							break;
						}
						goto IL_0086;
						IL_0086:
						num2 = 15;
						text3 = Strings.Mid(Strings.LCase(Globals_Renamed.g_QColumns[num5].DataType) + " ", 1, 1);
						goto IL_00b2;
						IL_00b2:
						num2 = 16;
						text = Strings.Trim(Globals_Renamed.g_QColumns[num5].Header);
						goto IL_00cd;
						IL_00cd:
						num2 = 17;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_00e9;
						}
						goto IL_0170;
						end_IL_0001_2:
						break;
					}
					num2 = 23;
					result = text2;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 510;
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

	public static string Substitute_Fn2(string Query_To_Run, string ll_DatabaseType, string ll_DisplayName)
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
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int num9 = default(int);
		int num10 = default(int);
		int num11 = default(int);
		int num12 = default(int);
		int num13 = default(int);
		int num14 = default(int);
		string text6 = default(string);
		string left = default(string);
		string replacement = default(string);
		string left2 = default(string);
		int num16 = default(int);
		string[] Args = default(string[]);
		int NArgs = default(int);
		int num17 = default(int);
		int num18 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text7;
					int num15;
					string left3;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 4172:
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
							case 4:
								goto IL_0015;
							case 5:
								goto IL_001e;
							case 6:
								goto IL_0023;
							case 7:
								goto IL_0028;
							case 8:
								goto IL_002d;
							case 9:
								goto IL_0032;
							case 10:
								goto IL_0038;
							case 11:
								goto IL_003e;
							case 12:
								goto IL_0044;
							case 13:
								goto IL_004a;
							case 14:
								goto IL_0054;
							case 15:
								goto IL_005e;
							case 16:
								goto IL_0068;
							case 17:
								goto IL_0072;
							case 18:
								goto IL_007c;
							case 19:
								goto IL_0086;
							case 20:
								goto IL_0090;
							case 21:
								goto IL_009a;
							case 22:
								goto IL_00a5;
							case 23:
							case 24:
								goto IL_00ab;
							case 25:
								goto IL_00b6;
							case 26:
								goto IL_00cc;
							case 27:
								goto IL_00dd;
							case 28:
								goto IL_00f5;
							case 29:
								goto IL_010d;
							case 31:
								goto IL_0142;
							case 32:
								goto IL_014b;
							case 30:
							case 33:
							case 34:
								goto IL_0156;
							case 35:
								goto IL_0160;
							case 36:
								goto IL_016a;
							case 37:
								goto IL_0174;
							case 38:
								goto IL_017e;
							case 39:
								goto IL_0188;
							case 40:
								goto IL_018e;
							case 41:
								goto IL_0194;
							case 42:
								goto IL_01ab;
							case 43:
								goto IL_01b9;
							case 44:
								goto IL_01d2;
							case 46:
								goto IL_01de;
							case 47:
								goto IL_01fd;
							case 50:
								goto IL_0209;
							case 51:
								goto IL_0222;
							case 45:
							case 49:
							case 52:
							case 53:
								goto IL_022c;
							case 48:
							case 54:
								goto IL_023e;
							case 55:
								goto IL_024c;
							case 57:
								goto IL_0259;
							case 56:
							case 58:
							case 59:
								goto IL_0277;
							case 60:
								goto IL_02a4;
							case 62:
								goto IL_02cb;
							case 63:
								goto IL_02d1;
							case 64:
								goto IL_02e5;
							case 65:
								goto IL_02f4;
							case 66:
								goto IL_030d;
							case 68:
								goto IL_0319;
							case 69:
								goto IL_0332;
							case 71:
								goto IL_033e;
							case 72:
								goto IL_035d;
							case 67:
							case 70:
							case 74:
							case 75:
								goto IL_0367;
							case 73:
							case 76:
								goto IL_0379;
							case 77:
								goto IL_0387;
							case 79:
								goto IL_039e;
							case 80:
								goto IL_03b4;
							case 81:
								goto IL_03d3;
							case 82:
								goto IL_03fd;
							case 84:
								goto IL_0415;
							case 86:
								goto IL_0440;
							case 88:
								goto IL_0460;
							case 89:
								goto IL_046c;
							case 90:
								goto IL_0485;
							case 91:
								goto IL_049e;
							case 92:
								goto IL_04d4;
							case 93:
								goto IL_04ed;
							case 94:
								goto IL_0504;
							case 95:
								goto IL_052d;
							case 96:
								goto IL_0543;
							case 99:
								goto IL_0561;
							case 102:
								goto IL_057f;
							case 103:
								goto IL_058b;
							case 104:
								goto IL_059d;
							case 105:
								goto IL_05b8;
							case 107:
								goto IL_05d4;
							case 108:
								goto IL_05e9;
							case 109:
								goto IL_05ff;
							case 110:
								goto IL_062a;
							case 111:
								goto IL_0643;
							case 112:
								goto IL_0659;
							case 113:
								goto IL_066f;
							case 115:
								goto IL_0689;
							case 122:
								goto IL_06c6;
							case 123:
								goto IL_06d2;
							case 124:
								goto IL_06d8;
							case 125:
								goto IL_06e9;
							case 126:
								goto IL_0706;
							case 128:
								goto IL_0a73;
							case 129:
								goto IL_0ab5;
							case 131:
								goto IL_0ace;
							case 132:
								goto IL_0aee;
							case 134:
								goto IL_0b14;
							case 135:
								goto IL_0b28;
							case 137:
								goto IL_0b3e;
							case 138:
								goto IL_0b78;
							case 141:
								goto IL_0b8f;
							case 142:
								goto IL_0ba3;
							case 127:
							case 130:
							case 133:
							case 136:
							case 139:
							case 140:
							case 143:
							case 144:
								goto IL_0bb9;
							case 145:
								goto IL_0be3;
							case 146:
								goto IL_0bf0;
							case 147:
								goto IL_0c1f;
							case 148:
								goto IL_0c4d;
							case 149:
								goto IL_0c69;
							case 150:
								goto IL_0c73;
							case 151:
								goto IL_0c84;
							case 152:
								goto IL_0cb3;
							case 154:
								goto IL_0cc9;
							case 153:
							case 155:
							case 156:
							case 157:
							case 158:
								goto IL_0d01;
							case 159:
								goto IL_0d0a;
							case 61:
							case 78:
							case 83:
							case 85:
							case 87:
							case 97:
							case 98:
							case 100:
							case 101:
							case 106:
							case 114:
							case 116:
							case 117:
							case 118:
							case 119:
							case 120:
							case 121:
							case 160:
							case 161:
							case 162:
								goto IL_0d18;
							case 163:
								goto IL_0d44;
							case 164:
							case 165:
								goto IL_0d6a;
							default:
								goto end_IL_0001;
							case 166:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_04d4:
						num2 = 92;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_04ed;
						}
						goto IL_0504;
						IL_04ed:
						num2 = 93;
						text2 = "ERROR - SPF_FN$ function error. SPF_FN$" + text3 + " does not support date format string: " + text4;
						goto IL_0504;
						IL_049e:
						num2 = 91;
						text = Strings.Trim(General_Procedures.Get_Ini_Data(text3, "sqlite_" + text5, "", 3000, Globals_Renamed.MySchemaDir + "\\spf_functions.ini"));
						goto IL_04d4;
						IL_0504:
						num2 = 94;
						if (Operators.CompareString(Strings.Mid(text2 + "     ", 1, 5), "ERROR", TextCompare: false) != 0)
						{
							goto IL_052d;
						}
						goto IL_0d18;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num6 = 0;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						text3 = "";
						goto IL_001e;
						IL_001e:
						num2 = 5;
						num7 = 0;
						goto IL_0023;
						IL_0023:
						num2 = 6;
						num8 = 0;
						goto IL_0028;
						IL_0028:
						num2 = 7;
						num9 = 0;
						goto IL_002d;
						IL_002d:
						num2 = 8;
						num10 = 0;
						goto IL_0032;
						IL_0032:
						num2 = 9;
						num11 = 0;
						goto IL_0038;
						IL_0038:
						num2 = 10;
						num12 = 0;
						goto IL_003e;
						IL_003e:
						num2 = 11;
						num13 = 0;
						goto IL_0044;
						IL_0044:
						num2 = 12;
						num14 = 0;
						goto IL_004a;
						IL_004a:
						num2 = 13;
						text6 = "";
						goto IL_0054;
						IL_0054:
						num2 = 14;
						left = "";
						goto IL_005e;
						IL_005e:
						num2 = 15;
						text = "";
						goto IL_0068;
						IL_0068:
						num2 = 16;
						text4 = "";
						goto IL_0072;
						IL_0072:
						num2 = 17;
						replacement = "";
						goto IL_007c;
						IL_007c:
						num2 = 18;
						text7 = "";
						goto IL_0086;
						IL_0086:
						num2 = 19;
						text5 = "";
						goto IL_0090;
						IL_0090:
						num2 = 20;
						left2 = "";
						goto IL_009a;
						IL_009a:
						num2 = 21;
						ll_DisplayName = Strings.UCase(ll_DisplayName);
						goto IL_00a5;
						IL_00a5:
						num2 = 22;
						text2 = Query_To_Run;
						goto IL_00ab;
						IL_00ab:
						num2 = 24;
						num13 = Strings.Len(text2);
						goto IL_00b6;
						IL_00b6:
						num2 = 25;
						num8 = Strings.InStr(Strings.UCase(text2), "SPF_FN$");
						goto IL_00cc;
						IL_00cc:
						num2 = 26;
						if (num8 != 0)
						{
							goto IL_00dd;
						}
						goto IL_0d6a;
						IL_00dd:
						num2 = 27;
						num9 = Strings.InStr(Strings.Mid(text2, num8), "(");
						goto IL_00f5;
						IL_00f5:
						num2 = 28;
						if (unchecked(num9 != 0 && num9 <= 55))
						{
							goto IL_010d;
						}
						goto IL_0142;
						IL_010d:
						num2 = 29;
						text3 = Strings.Trim(Strings.LCase(Strings.Mid(text2, num8 + Strings.Len("SPF_FN$"), num9 - (Strings.Len("SPF_FN$") + 1))));
						goto IL_0156;
						IL_0142:
						num2 = 31;
						text2 = "ERROR - SPF_FN$ function error. Could not find opening (";
						goto IL_014b;
						IL_014b:
						num2 = 32;
						text3 = "";
						goto IL_0156;
						IL_0156:
						num2 = 34;
						text = "";
						goto IL_0160;
						IL_0160:
						num2 = 35;
						text6 = "'";
						goto IL_016a;
						IL_016a:
						num2 = 36;
						text4 = "";
						goto IL_0174;
						IL_0174:
						num2 = 37;
						replacement = "";
						goto IL_017e;
						IL_017e:
						num2 = 38;
						text7 = "";
						goto IL_0188;
						IL_0188:
						num2 = 39;
						num12 = 0;
						goto IL_018e;
						IL_018e:
						num2 = 40;
						num7 = 0;
						goto IL_0194;
						IL_0194:
						num2 = 41;
						num15 = num8 + num9;
						num16 = num13;
						num5 = num15;
						goto IL_0235;
						IL_0235:
						if (num5 <= num16)
						{
							goto IL_01ab;
						}
						goto IL_023e;
						IL_01ab:
						num2 = 42;
						left = Strings.Mid(text2, num5, 1);
						goto IL_01b9;
						IL_01b9:
						num2 = 43;
						if (Operators.CompareString(left, "(", TextCompare: false) == 0)
						{
							goto IL_01d2;
						}
						goto IL_01de;
						IL_01d2:
						num2 = 44;
						num12++;
						goto IL_022c;
						IL_01de:
						num2 = 46;
						if (unchecked(Operators.CompareString(left, ")", TextCompare: false) == 0 && num12 == 0))
						{
							goto IL_01fd;
						}
						goto IL_0209;
						IL_01fd:
						num2 = 47;
						num7 = num5;
						goto IL_023e;
						IL_023e:
						num2 = 54;
						if (num7 == 0)
						{
							goto IL_024c;
						}
						goto IL_0259;
						IL_024c:
						num2 = 55;
						text2 = "ERROR - SPF_FN$ function error. Could not find closing )";
						goto IL_0277;
						IL_0259:
						num2 = 57;
						text = Strings.Trim(Strings.Mid(text2, num8 + num9, num7 - (num8 + num9)));
						goto IL_0277;
						IL_0277:
						num2 = 59;
						if (Operators.CompareString(Strings.Mid(text2 + "     ", 1, 5), "ERROR", TextCompare: false) != 0)
						{
							goto IL_02a4;
						}
						goto IL_0d18;
						IL_02a4:
						num2 = 60;
						if (Operators.CompareString(text3, "to_date", TextCompare: false) == 0 || Operators.CompareString(text3, "lww", TextCompare: false) == 0)
						{
							goto IL_02cb;
						}
						goto IL_06c6;
						IL_052d:
						num2 = 95;
						text = Strings.Replace(text, "<>", text6, 1, -1, CompareMethod.Text);
						goto IL_0543;
						IL_06c6:
						num2 = 122;
						Args = new string[21];
						goto IL_06d2;
						IL_06d2:
						num2 = 123;
						NArgs = 0;
						goto IL_06d8;
						IL_06d8:
						num2 = 124;
						left2 = Return_Args(text, ref Args, ref NArgs, ll_DatabaseType);
						goto IL_06e9;
						IL_06e9:
						num2 = 125;
						if (Operators.CompareString(left2, "", TextCompare: false) == 0)
						{
							goto IL_0706;
						}
						goto IL_0d01;
						IL_0706:
						num2 = 126;
						switch (text3)
						{
						case "instr":
							break;
						case "htmlq1":
							goto IL_0ace;
						case "date_diff":
						case "ifnull":
						case "date_diff_xeus":
						case "html":
						case "tooltip":
						case "html_image":
						case "at_met_to_date":
						case "mdo_lab_shift":
							goto IL_0b14;
						case "mdo_execution_shift":
						case "gmt_pt":
						case "gmt_pt_str":
						case "pt_gmt":
						case "pt_gmt_str":
						case "gmt_ma":
						case "gmt_ma_str":
						case "ma_gmt":
						case "ma_gmt_str":
						case "intelww":
						case "current_shift":
							goto IL_0b8f;
						default:
							goto IL_0bb9;
						}
						goto IL_0a73;
						IL_0b8f:
						num2 = 141;
						if (NArgs != 1)
						{
							goto IL_0ba3;
						}
						goto IL_0bb9;
						IL_0ba3:
						num2 = 142;
						text2 = "ERROR - SPF_FN$ function error. Inconsistent number of arguments for SPF_FN$" + text3;
						goto IL_0bb9;
						IL_0b14:
						num2 = 134;
						if (NArgs != 2)
						{
							goto IL_0b28;
						}
						goto IL_0b3e;
						IL_0b28:
						num2 = 135;
						text2 = "ERROR - SPF_FN$ function error. Inconsistent number of arguments for SPF_FN$" + text3;
						goto IL_0bb9;
						IL_0b3e:
						num2 = 137;
						if (NArgs == 2 && (Operators.CompareString(Args[0], "", TextCompare: false) == 0 || Operators.CompareString(Args[1], "", TextCompare: false) == 0))
						{
							goto IL_0b78;
						}
						goto IL_0bb9;
						IL_0d44:
						num2 = 163;
						text2 = Strings.Mid(text2, 1, num8 - 1) + text + Strings.Mid(text2, num7 + 1);
						goto IL_0d6a;
						IL_0209:
						num2 = 50;
						if (Operators.CompareString(left, ")", TextCompare: false) == 0)
						{
							goto IL_0222;
						}
						goto IL_022c;
						IL_0b78:
						num2 = 138;
						text2 = "ERROR - SPF_FN$ function error. Missing arguments for SPF_FN$" + text3;
						goto IL_0bb9;
						IL_0ace:
						num2 = 131;
						Args[0] = Strings.Replace(Args[0], "'", "", 1, -1, CompareMethod.Text);
						goto IL_0aee;
						IL_0aee:
						num2 = 132;
						Args[2] = Strings.Replace(Args[2], "'", "", 1, -1, CompareMethod.Text);
						goto IL_0bb9;
						IL_0a73:
						num2 = 128;
						if (((Operators.CompareString(Strings.UCase(ll_DatabaseType), "SQLSERVER", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(ll_DatabaseType), "MONGO", TextCompare: false) != 0) || NArgs != 3) && NArgs != 4)
						{
							goto IL_0ab5;
						}
						goto IL_0bb9;
						IL_0d18:
						num2 = 162;
						if (Operators.CompareString(Strings.Mid(text2 + "     ", 1, 5), "ERROR", TextCompare: false) != 0)
						{
							goto IL_0d44;
						}
						goto IL_0d6a;
						IL_0367:
						num2 = 75;
						num6++;
						goto IL_0370;
						IL_0543:
						num2 = 96;
						text = Strings.Replace(text, "<cr>", "\r\n          ", 1, -1, CompareMethod.Text);
						goto IL_0d18;
						IL_0ab5:
						num2 = 129;
						text2 = "ERROR - SPF_FN$ function error. Inconsistent number of arguments for SPF_FN$" + text3;
						goto IL_0bb9;
						IL_0bb9:
						num2 = 144;
						if (Operators.CompareString(text3, "date_diff", TextCompare: false) == 0 && LikeOperator.LikeString(ll_DisplayName, "*XEUS*", CompareMethod.Binary))
						{
							goto IL_0be3;
						}
						goto IL_0bf0;
						IL_0222:
						num2 = 51;
						num12--;
						goto IL_022c;
						IL_0be3:
						num2 = 145;
						ll_DatabaseType = "XEUS";
						goto IL_0bf0;
						IL_0bf0:
						num2 = 146;
						if (Operators.CompareString(Strings.Mid(text2 + "     ", 1, 5), "ERROR", TextCompare: false) != 0)
						{
							goto IL_0c1f;
						}
						goto IL_0d01;
						IL_0c1f:
						num2 = 147;
						text5 = General_Procedures.Get_Ini_Data(text3, ll_DatabaseType.ToLower(), "", 2000, Globals_Renamed.MySchemaDir + "\\spf_functions.ini");
						goto IL_0c4d;
						IL_0c4d:
						num2 = 148;
						if (Operators.CompareString(text5, "", TextCompare: false) != 0)
						{
							goto IL_0c69;
						}
						goto IL_0cc9;
						IL_0c69:
						num2 = 149;
						text = text5;
						goto IL_0c73;
						IL_0c73:
						num2 = 150;
						num17 = NArgs - 1;
						num5 = 0;
						goto IL_0cbf;
						IL_0cbf:
						if (num5 <= num17)
						{
							goto IL_0c84;
						}
						goto IL_0d01;
						IL_0c84:
						num2 = 151;
						text = Strings.Replace(text, "<" + Conversions.ToString(num5 + 1) + ">", Args[num5], 1, -1, CompareMethod.Text);
						goto IL_0cb3;
						IL_0cb3:
						num2 = 152;
						num5++;
						goto IL_0cbf;
						IL_0cc9:
						num2 = 154;
						text2 = "ERROR - SPF_FN$ function error. SPF_FN$" + text3 + " is not supported with " + ll_DatabaseType + " databases";
						goto IL_0d01;
						IL_0d01:
						num2 = 158;
						Args = null;
						goto IL_0d0a;
						IL_0d0a:
						num2 = 159;
						Args = null;
						goto IL_0d18;
						IL_02cb:
						num2 = 62;
						num14 = 0;
						goto IL_02d1;
						IL_02d1:
						num2 = 63;
						num18 = Strings.Len(text);
						num6 = 1;
						goto IL_0370;
						IL_0370:
						if (num6 <= num18)
						{
							goto IL_02e5;
						}
						goto IL_0379;
						IL_02e5:
						num2 = 64;
						left = Strings.Mid(text, num6, 1);
						goto IL_02f4;
						IL_02f4:
						num2 = 65;
						if (Operators.CompareString(left, "(", TextCompare: false) == 0)
						{
							goto IL_030d;
						}
						goto IL_0319;
						IL_030d:
						num2 = 66;
						num14++;
						goto IL_0367;
						IL_0319:
						num2 = 68;
						if (Operators.CompareString(left, ")", TextCompare: false) == 0)
						{
							goto IL_0332;
						}
						goto IL_033e;
						IL_0332:
						num2 = 69;
						num14--;
						goto IL_0367;
						IL_033e:
						num2 = 71;
						if (unchecked(Operators.CompareString(left, ",", TextCompare: false) == 0 && num14 == 0))
						{
							goto IL_035d;
						}
						goto IL_0367;
						IL_035d:
						num2 = 72;
						num10 = num6;
						goto IL_0379;
						IL_0379:
						num2 = 76;
						if (num10 == 0)
						{
							goto IL_0387;
						}
						goto IL_039e;
						IL_0387:
						num2 = 77;
						text2 = "ERROR - SPF_FN$ function error. Inconsistent number of arguments for SPF_FN$" + text3;
						goto IL_0d18;
						IL_039e:
						num2 = 79;
						text6 = Strings.Trim(Strings.Mid(text, 1, num10 - 1));
						goto IL_03b4;
						IL_03b4:
						num2 = 80;
						text4 = Strings.Trim(Strings.Mid(text + " ", num10 + 1));
						goto IL_03d3;
						IL_03d3:
						num2 = 81;
						if ((Operators.CompareString(text6, "", TextCompare: false) == 0) | (Operators.CompareString(text4, "", TextCompare: false) == 0))
						{
							goto IL_03fd;
						}
						goto IL_0415;
						IL_03fd:
						num2 = 82;
						text2 = "ERROR - SPF_FN$ function error. Missing arguments for SPF_FN$" + text3;
						goto IL_0d18;
						IL_0415:
						num2 = 84;
						if (Operators.CompareString(text3, "to_date", TextCompare: false) == 0)
						{
							goto IL_0440;
						}
						if (Operators.CompareString(text3, "lww", TextCompare: false) == 0)
						{
							goto IL_057f;
						}
						goto IL_0d18;
						IL_022c:
						num2 = 53;
						num5++;
						goto IL_0235;
						IL_057f:
						num2 = 102;
						text4 = Strings.Trim(text4);
						goto IL_058b;
						IL_058b:
						num2 = 103;
						num11 = Strings.InStr(text4, ":");
						goto IL_059d;
						IL_059d:
						num2 = 104;
						if (num11 == 0 || num11 == Strings.Len(text4))
						{
							goto IL_05b8;
						}
						goto IL_05d4;
						IL_0d6a:
						num2 = 165;
						if (!((num8 != 0) & (Operators.CompareString(Strings.Mid(text2 + "     ", 1, 5), "ERROR", TextCompare: false) != 0)))
						{
							goto end_IL_0001_2;
						}
						goto IL_00ab;
						IL_05d4:
						num2 = 107;
						replacement = Strings.Trim(Strings.Mid(text4, num11 + 1));
						goto IL_05e9;
						IL_05e9:
						num2 = 108;
						text4 = Strings.Trim(Strings.Mid(text4, 1, num11 - 1));
						goto IL_05ff;
						IL_05ff:
						num2 = 109;
						text5 = General_Procedures.Get_Ini_Data(text3, ll_DatabaseType.ToLower(), "", 2000, Globals_Renamed.MySchemaDir + "\\spf_functions.ini");
						goto IL_062a;
						IL_062a:
						num2 = 110;
						if (Operators.CompareString(text5, "", TextCompare: false) != 0)
						{
							goto IL_0643;
						}
						goto IL_0689;
						IL_0643:
						num2 = 111;
						text = Strings.Replace(text5, "<1>", text6, 1, -1, CompareMethod.Text);
						goto IL_0659;
						IL_0659:
						num2 = 112;
						text = Strings.Replace(text, "<2>", text4, 1, -1, CompareMethod.Text);
						goto IL_066f;
						IL_066f:
						num2 = 113;
						text = Strings.Replace(text, "<3>", replacement, 1, -1, CompareMethod.Text);
						goto IL_0d18;
						IL_0689:
						num2 = 115;
						text2 = "ERROR - SPF_FN$ function error. SPF_FN$" + text3 + " is not supported with " + ll_DatabaseType + " databases";
						goto IL_0d18;
						IL_05b8:
						num2 = 105;
						text2 = "ERROR - SPF_FN$ function error. SPF_FN$" + text3 + " expects a second argument of format hh:mm in 24 hour format. E.g., 18:30 or 6:30";
						goto IL_0d18;
						IL_0440:
						num2 = 86;
						left3 = Strings.UCase(ll_DatabaseType);
						if (Operators.CompareString(left3, "SQLITE", TextCompare: false) == 0)
						{
							goto IL_0460;
						}
						goto IL_0561;
						IL_0561:
						num2 = 99;
						text2 = "ERROR - SPF_FN$ function error. SPF_FN$" + text3 + " is currently only supported with SQLite databases";
						goto IL_0d18;
						IL_0460:
						num2 = 88;
						text5 = Strings.LCase(text4);
						goto IL_046c;
						IL_046c:
						num2 = 89;
						text5 = Strings.Replace(text5, " ", "", 1, -1, CompareMethod.Text);
						goto IL_0485;
						IL_0485:
						num2 = 90;
						text5 = Strings.Replace(text5, "'", "", 1, -1, CompareMethod.Text);
						goto IL_049e;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4172;
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
		return text2;
	}

	public static string Return_Args(string ArgAll, ref string[] Args, ref int NArgs, string ll_DatabaseType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		string text2 = default(string);
		int num7 = default(int);
		string left = default(string);
		int num8 = default(int);
		string right = default(string);
		int num9 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text3;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 808:
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
								goto IL_002f;
							case 5:
							case 6:
								goto IL_003c;
							case 7:
								goto IL_0041;
							case 8:
								goto IL_004a;
							case 9:
								goto IL_004f;
							case 10:
								goto IL_0055;
							case 11:
								goto IL_0064;
							case 12:
								goto IL_006e;
							case 13:
								goto IL_0078;
							case 16:
								goto IL_0088;
							case 17:
								goto IL_008f;
							case 18:
								goto IL_00a3;
							case 19:
								goto IL_00b2;
							case 20:
								goto IL_00f3;
							case 22:
								goto IL_0102;
							case 23:
								goto IL_0143;
							case 25:
								goto IL_0152;
							case 26:
								goto IL_0171;
							case 27:
								goto IL_0178;
							case 28:
								goto IL_0181;
							case 29:
								goto IL_0193;
							case 30:
								goto IL_01ae;
							case 32:
								goto IL_01d1;
							case 21:
							case 24:
							case 35:
							case 36:
								goto IL_01de;
							case 14:
							case 15:
							case 31:
							case 33:
							case 34:
							case 37:
								goto IL_01f1;
							case 38:
								goto IL_022a;
							case 39:
								goto IL_0254;
							case 40:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 41:
							case 42:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0181:
						num2 = 28;
						if (NArgs <= num5)
						{
							goto IL_0193;
						}
						goto IL_01d1;
						IL_0193:
						num2 = 29;
						Args[NArgs - 1] = Strings.Trim(Strings.Mid(text, 1, num6 - 1));
						goto IL_01ae;
						IL_0178:
						num2 = 27;
						NArgs++;
						goto IL_0181;
						IL_01ae:
						num2 = 30;
						text = Strings.Trim(Strings.Mid(text + " ", num6 + 1));
						goto IL_01f1;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						if (Operators.CompareString(Strings.UCase(ll_DatabaseType), "MONGO", TextCompare: false) == 0)
						{
							goto IL_002f;
						}
						goto IL_003c;
						IL_002f:
						num2 = 4;
						ArgAll = BuildForm.Repl_Squiggly(ArgAll, 1);
						goto IL_003c;
						IL_003c:
						num2 = 6;
						num7 = 0;
						goto IL_0041;
						IL_0041:
						num2 = 7;
						left = "";
						goto IL_004a;
						IL_004a:
						num2 = 8;
						num8 = 0;
						goto IL_004f;
						IL_004f:
						num2 = 9;
						num6 = 0;
						goto IL_0055;
						IL_0055:
						num2 = 10;
						num5 = Information.UBound(Args) + 1;
						goto IL_0064;
						IL_0064:
						num2 = 11;
						right = "";
						goto IL_006e;
						IL_006e:
						num2 = 12;
						text3 = "";
						goto IL_0078;
						IL_0078:
						num2 = 13;
						text = Strings.Trim(ArgAll);
						goto IL_01f1;
						IL_01f1:
						num2 = 15;
						if (Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text, right, TextCompare: false) != 0 && Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							goto IL_0088;
						}
						goto IL_022a;
						IL_01d1:
						num2 = 32;
						text2 = "Invalid number of arguments.";
						goto IL_01f1;
						IL_0143:
						num2 = 23;
						num8--;
						goto IL_01de;
						IL_0088:
						num2 = 16;
						right = text;
						goto IL_008f;
						IL_008f:
						num2 = 17;
						num9 = Strings.Len(text);
						num7 = 1;
						goto IL_01e7;
						IL_01e7:
						if (num7 <= num9)
						{
							goto IL_00a3;
						}
						goto IL_01f1;
						IL_00a3:
						num2 = 18;
						left = Strings.Mid(text, num7, 1);
						goto IL_00b2;
						IL_00b2:
						num2 = 19;
						if (Operators.CompareString(left, "(", TextCompare: false) == 0 || (Operators.CompareString(Strings.UCase(ll_DatabaseType), "MONGO", TextCompare: false) == 0 && Operators.CompareString(left, "{", TextCompare: false) == 0))
						{
							goto IL_00f3;
						}
						goto IL_0102;
						IL_00f3:
						num2 = 20;
						num8++;
						goto IL_01de;
						IL_01de:
						num2 = 36;
						num7++;
						goto IL_01e7;
						IL_0102:
						num2 = 22;
						if (Operators.CompareString(left, ")", TextCompare: false) == 0 || (Operators.CompareString(Strings.UCase(ll_DatabaseType), "MONGO", TextCompare: false) == 0 && Operators.CompareString(left, "}", TextCompare: false) == 0))
						{
							goto IL_0143;
						}
						goto IL_0152;
						IL_022a:
						num2 = 38;
						if (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0254;
						IL_0254:
						num2 = 39;
						NArgs++;
						break;
						IL_0152:
						num2 = 25;
						if (unchecked(Operators.CompareString(left, ",", TextCompare: false) == 0 && num8 == 0))
						{
							goto IL_0171;
						}
						goto IL_01de;
						IL_0171:
						num2 = 26;
						num6 = num7;
						goto IL_0178;
						end_IL_0001_2:
						break;
					}
					num2 = 40;
					Args[NArgs - 1] = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 808;
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
		return text2;
	}

	public static string Test_Dup_hdrs(ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string text = default(string);
		string text2 = default(string);
		short num6 = default(short);
		short num7 = default(short);
		short num8 = default(short);
		string text3 = default(string);
		short num10 = default(short);
		string text4 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					short num9;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 752:
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
								goto IL_0021;
							case 5:
								goto IL_002f;
							case 6:
								goto IL_005b;
							case 7:
								goto IL_0088;
							case 9:
								goto IL_00ab;
							case 10:
								goto IL_00c4;
							case 12:
								goto IL_00de;
							case 13:
							case 14:
								goto IL_00ed;
							case 15:
								goto IL_011a;
							case 16:
								goto IL_0131;
							case 17:
								goto IL_015e;
							case 18:
								goto IL_0177;
							case 19:
							case 20:
								goto IL_01ad;
							case 21:
								goto IL_01cd;
							case 23:
							case 24:
								goto IL_0225;
							case 22:
							case 25:
							case 26:
								goto IL_023a;
							case 8:
							case 11:
							case 28:
								goto IL_0254;
							default:
								goto end_IL_0001;
							case 27:
							case 29:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0225:
						num2 = 24;
						num5 = (short)unchecked(num5 + 1);
						goto IL_022f;
						IL_023a:
						num2 = 26;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_0254;
						IL_01cd:
						num2 = 21;
						text = "You have two columns in the Columns Grid that have the same header. I.e., Column Header \"" + text2 + "\" in rows " + (num6 + 1) + " and " + (num5 + 1) + ". This can cause issues when running the SQL. Do change one of the headers.";
						goto IL_023a;
						IL_0254:
						num2 = 28;
						num6 = (short)unchecked(num6 + 1);
						goto IL_025e;
						IL_000b:
						num2 = 2;
						text = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num7 = (short)(MyGrid.RowCount - 1);
						goto IL_0021;
						IL_0021:
						num2 = 4;
						num8 = num7;
						num6 = 0;
						goto IL_025e;
						IL_025e:
						if (num6 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_002f;
						IL_002f:
						num2 = 5;
						text2 = Strings.Trim(Conversions.ToString(MyGrid.Rows[num6].Cells[1].Value));
						goto IL_005b;
						IL_005b:
						num2 = 6;
						text3 = Strings.Trim(Conversions.ToString(MyGrid.Rows[num6].Cells[11].Value));
						goto IL_0088;
						IL_0088:
						num2 = 7;
						if (Operators.CompareString(Check_ibd_field(text3), "", TextCompare: false) == 0)
						{
							goto IL_00ab;
						}
						goto IL_0254;
						IL_00ab:
						num2 = 9;
						if (Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							goto IL_00c4;
						}
						goto IL_00ed;
						IL_00c4:
						num2 = 10;
						if (!BuildForm.IsColPattern(text3))
						{
							goto IL_00de;
						}
						goto IL_0254;
						IL_00de:
						num2 = 12;
						text2 = General_Procedures.Strip_Column(text3, 1);
						goto IL_00ed;
						IL_00ed:
						num2 = 14;
						if ((Operators.CompareString(text2, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "*", TextCompare: false) != 0))
						{
							goto IL_011a;
						}
						goto IL_023a;
						IL_011a:
						num2 = 15;
						num9 = (short)(num6 + 1);
						num10 = num7;
						num5 = num9;
						goto IL_022f;
						IL_022f:
						if (num5 <= num10)
						{
							goto IL_0131;
						}
						goto IL_023a;
						IL_0131:
						num2 = 16;
						text4 = Strings.Trim(Conversions.ToString(MyGrid.Rows[num5].Cells[1].Value));
						goto IL_015e;
						IL_015e:
						num2 = 17;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto IL_0177;
						}
						goto IL_01ad;
						IL_0177:
						num2 = 18;
						text4 = General_Procedures.Strip_Column(Strings.Trim(Conversions.ToString(MyGrid.Rows[num5].Cells[11].Value)), 1);
						goto IL_01ad;
						IL_01ad:
						num2 = 20;
						if (Operators.CompareString(Strings.LCase(text2), Strings.LCase(text4), TextCompare: false) == 0)
						{
							goto IL_01cd;
						}
						goto IL_0225;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 752;
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
		return text;
	}

	public static string Get_Sort(short MyMode, int l_AddDT = 0)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		short num5 = default(short);
		string text2 = default(string);
		string text3 = default(string);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		string text4 = default(string);
		short num9 = default(short);
		short num11 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					short num10;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1342:
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
								goto IL_0047;
							case 11:
								goto IL_0063;
							case 12:
								goto IL_0070;
							case 14:
								goto IL_0079;
							case 15:
								goto IL_0086;
							case 13:
							case 16:
							case 17:
							case 18:
								goto IL_008f;
							case 19:
								goto IL_00a3;
							case 20:
								goto IL_00a9;
							case 21:
								goto IL_00b3;
							case 22:
							case 23:
								goto IL_00bb;
							case 24:
								goto IL_00d6;
							case 25:
								goto IL_0152;
							case 26:
								goto IL_016d;
							case 27:
								goto IL_0183;
							case 28:
								goto IL_018d;
							case 29:
								goto IL_019a;
							case 30:
								goto IL_01b5;
							case 31:
								goto IL_01e0;
							case 33:
								goto IL_01ee;
							case 32:
							case 34:
							case 35:
							case 36:
								goto IL_01fb;
							case 37:
								goto IL_021c;
							case 38:
								goto IL_023d;
							case 39:
								goto IL_0254;
							case 40:
								goto IL_0293;
							case 42:
								goto IL_02af;
							case 43:
								goto IL_02ee;
							case 45:
								goto IL_0307;
							case 46:
								goto IL_0331;
							case 50:
								goto IL_0351;
							case 51:
								goto IL_0390;
							case 41:
							case 44:
							case 47:
							case 48:
							case 49:
							case 52:
							case 53:
							case 54:
								goto IL_03a9;
							case 55:
								goto IL_03c6;
							case 57:
								goto IL_03de;
							case 56:
							case 58:
							case 59:
							case 60:
								goto IL_0413;
							default:
								goto end_IL_0001;
							case 61:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0307:
						num2 = 45;
						if (Operators.CompareString(Strings.Mid(text + "   ", 1, 3), "<!>", TextCompare: false) == 0)
						{
							goto IL_0331;
						}
						goto IL_03a9;
						IL_0331:
						num2 = 46;
						text = Strings.Replace(text, "<!>", "", 1, -1, CompareMethod.Text);
						goto IL_03a9;
						IL_02ee:
						num2 = 43;
						text = Strings.Mid(text, 2, Strings.Len(text) - 2);
						goto IL_03a9;
						IL_0351:
						num2 = 50;
						if (Operators.CompareString(Strings.Mid(text, 1, 1), "\"", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(text, Strings.Len(text), 1), "\"", TextCompare: false) == 0)
						{
							goto IL_0390;
						}
						goto IL_03a9;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						text2 = "";
						goto IL_0019;
						IL_0019:
						num2 = 4;
						text = "";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						text3 = "";
						goto IL_002b;
						IL_002b:
						num2 = 6;
						num6 = Globals_Renamed.g_NoQColumns;
						goto IL_0034;
						IL_0034:
						num2 = 7;
						num7 = 0;
						goto IL_0039;
						IL_0039:
						num2 = 8;
						num8 = -1;
						goto IL_003e;
						IL_003e:
						num2 = 9;
						text4 = "";
						goto IL_0047;
						IL_0047:
						num2 = 10;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
						{
							goto IL_0063;
						}
						goto IL_008f;
						IL_0063:
						num2 = 11;
						if (MyMode == 0)
						{
							goto IL_0070;
						}
						goto IL_0079;
						IL_0070:
						num2 = 12;
						MyMode = 3;
						goto IL_008f;
						IL_0079:
						num2 = 14;
						if (MyMode == 1)
						{
							goto IL_0086;
						}
						goto IL_008f;
						IL_0086:
						num2 = 15;
						MyMode = 2;
						goto IL_008f;
						IL_008f:
						num2 = 18;
						if (MyMode == 2 || MyMode == 3)
						{
							goto IL_00a3;
						}
						goto IL_00bb;
						IL_0413:
						num2 = 60;
						num5 = (short)unchecked(num5 + num9);
						goto IL_041e;
						IL_00a3:
						num2 = 19;
						num6 = 0;
						goto IL_00a9;
						IL_00a9:
						num2 = 20;
						num7 = Globals_Renamed.g_NoQColumns;
						goto IL_00b3;
						IL_00b3:
						num2 = 21;
						num8 = 1;
						goto IL_00bb;
						IL_00bb:
						num2 = 23;
						num10 = (short)num6;
						num11 = (short)num7;
						num9 = (short)num8;
						num5 = num10;
						goto IL_041e;
						IL_041e:
						if (unchecked(((short)(num9 >> 15) ^ num5) > ((short)(num9 >> 15) ^ num11)))
						{
							goto end_IL_0001_2;
						}
						goto IL_00d6;
						IL_00d6:
						num2 = 24;
						if ((Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num5].Show), 1, 1), "Y", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_QColumns[num5].Sort, "None", TextCompare: false) != 0) & (Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num5].Pivot), "ROW", TextCompare: false) == 0))
						{
							goto IL_0152;
						}
						goto IL_0413;
						IL_0152:
						num2 = 25;
						text2 = Strings.UCase(Globals_Renamed.g_QColumns[num5].Sort);
						goto IL_016d;
						IL_016d:
						num2 = 26;
						text = Globals_Renamed.g_QColumns[num5].Header;
						goto IL_0183;
						IL_0183:
						num2 = 27;
						text3 = "";
						goto IL_018d;
						IL_018d:
						num2 = 28;
						if (l_AddDT == 1)
						{
							goto IL_019a;
						}
						goto IL_01fb;
						IL_019a:
						num2 = 29;
						text3 = Strings.UCase(Globals_Renamed.g_QColumns[num5].DataType);
						goto IL_01b5;
						IL_01b5:
						num2 = 30;
						if (Operators.CompareString(text3, "N", TextCompare: false) == 0 || Operators.CompareString(text3, "F", TextCompare: false) == 0)
						{
							goto IL_01e0;
						}
						goto IL_01ee;
						IL_0390:
						num2 = 51;
						text = Strings.Mid(text, 2, Strings.Len(text) - 2);
						goto IL_03a9;
						IL_01ee:
						num2 = 33;
						text3 = "";
						goto IL_01fb;
						IL_01e0:
						num2 = 31;
						text3 = "-1";
						goto IL_01fb;
						IL_01fb:
						num2 = 36;
						if (Operators.CompareString(Strings.Trim(text), "", TextCompare: false) == 0)
						{
							goto IL_021c;
						}
						goto IL_0351;
						IL_021c:
						num2 = 37;
						text = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num5].Column), 1);
						goto IL_023d;
						IL_023d:
						num2 = 38;
						if (MyMode == 1 || MyMode == 2)
						{
							goto IL_0254;
						}
						goto IL_03a9;
						IL_03a9:
						num2 = 54;
						if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
						{
							goto IL_03c6;
						}
						goto IL_03de;
						IL_0254:
						num2 = 39;
						if (Operators.CompareString(Strings.Mid(text, 1, 1), "[", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(text, Strings.Len(text), 1), "]", TextCompare: false) == 0)
						{
							goto IL_0293;
						}
						goto IL_02af;
						IL_03c6:
						num2 = 55;
						text4 = text + " " + text2 + text3;
						goto IL_0413;
						IL_0293:
						num2 = 40;
						text = Strings.Mid(text, 2, Strings.Len(text) - 2);
						goto IL_03a9;
						IL_02af:
						num2 = 42;
						if (Operators.CompareString(Strings.Mid(text, 1, 1), "\"", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(text, Strings.Len(text), 1), "\"", TextCompare: false) == 0)
						{
							goto IL_02ee;
						}
						goto IL_0307;
						IL_03de:
						num2 = 57;
						text4 = text4 + "," + text + " " + text2 + text3;
						goto IL_0413;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1342;
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
		return text4;
	}

	public static string Get_QShow_Final(string MyShow)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 157:
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
							goto IL_003e;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000c:
					num2 = 2;
					left = Strings.Trim(Strings.UCase(Strings.Mid(MyShow + " ", 1, 1)));
					if (Operators.CompareString(left, "P", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_003e;
					IL_003e:
					num2 = 4;
					result = "N" + Strings.Trim(Strings.Mid(MyShow + "  ", 2));
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = MyShow;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 157;
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

	public static string Verify_Temp_Table(string ll_MajorAlias, string ll_AliasStr)
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
						errsource = "BuildSQL - Verify_Temp_Table";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						string text = "";
						bool flag = false;
						result = "";
						short g_NoQFilters = Globals_Renamed.g_NoQFilters;
						for (num4 = 0; num4 <= g_NoQFilters; num4 = (short)unchecked(num4 + 1))
						{
							if ((LikeOperator.LikeString(Strings.UCase(Globals_Renamed.g_QFilters[num4].Operator_Renamed), "*TEMP", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(Globals_Renamed.g_QFilters[num4].Operator_Renamed), "*TEMP-R", CompareMethod.Binary)) && Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QFilters[num4].And_Renamed), 1, 2), "--", TextCompare: false) != 0)
							{
								text = General_Procedures.Strip_Alias(1, Globals_Renamed.g_QFilters[num4].Value2);
								flag = false;
								num3 = (short)Strings.InStr(ll_AliasStr, "," + Strings.Trim(text) + ",");
								num5 = (short)Strings.InStr(ll_AliasStr, "," + Strings.Trim(ll_MajorAlias) + ",");
								if (num5 < num3)
								{
									flag = true;
								}
								else
								{
									Globals_Renamed.g_QFilters[num4].Value2 = General_Procedures.Strip_Alias(0, Globals_Renamed.g_QFilters[num4].Value2);
								}
								if (flag)
								{
									result = "This sub-query references a Temp Table belonging to query " + text + " which is to run after it. Temp Table references must be for earlier sub-queries";
									break;
								}
							}
						}
						goto end_IL_0001;
					}
					case 422:
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
					goto IL_01dc;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 422;
				continue;
			}
			break;
			IL_01dc:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Chk_Query_Util(ref short g_NoAlias, ref Globals_Renamed.AliasArray_Type[] g_AliasArray)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string[] array = default(string[]);
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
						errsource = "BuildSQL - Chk_Query_Util";
						array = new string[601];
						short num3 = 0;
						short num4 = 0;
						string text = "";
						result = "";
						num4 = -1;
						short num5 = (short)(g_NoAlias - 1);
						for (num3 = 0; num3 <= num5; num3 = (short)unchecked(num3 + 1))
						{
							if (Strings.InStr(Strings.UCase(g_AliasArray[num3].DBType), "{RUN-LOOP}") != 0)
							{
								num4++;
								array[num4] = "END-LOOP";
							}
							else if (Strings.InStr(Strings.UCase(g_AliasArray[num3].DBType), "{FOR-LOOP}") != 0)
							{
								num4++;
								array[num4] = "END-LOOP";
							}
							else if (Strings.InStr(Strings.UCase(g_AliasArray[num3].DBType), "{SITE-LOOP}") != 0)
							{
								num4++;
								array[num4] = "END-LOOP";
							}
							else if (Strings.InStr(Strings.UCase(g_AliasArray[num3].DBType), "{START-MACRO}") != 0)
							{
								num4++;
								array[num4] = "END-MACRO";
							}
							else if (Strings.InStr(Strings.UCase(g_AliasArray[num3].DBType), "{IF-THEN}") != 0)
							{
								num4++;
								array[num4] = "END-IF";
							}
							else if (LikeOperator.LikeString(Strings.UCase(g_AliasArray[num3].DBType), "*{END-IF}", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(g_AliasArray[num3].DBType), "*{END-LOOP}", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(g_AliasArray[num3].DBType), "*{END-MACRO}", CompareMethod.Binary))
							{
								text = General_Procedures.Get_Node_Value(Strings.UCase(g_AliasArray[num3].DBType));
								if (num4 < 0)
								{
									result = "An " + text + " utility was unexpectedly found. Either there is no associated Utility or the associated Utility may not be enabled or may be unassigned.";
								}
								else
								{
									if (Operators.CompareString(array[num4], text, TextCompare: false) != 0)
									{
										result = "SQLPathFinder does not support mixing of utilities. An " + text + " token was found prior to an expected " + array[num4] + " token. This could happen if you have added the correct Loop and End_Loop utilities but have not yet configured a Loop utility so it is ignored. If so, simply disable that loop and the associated End Loop utilities if you want to configure them later.";
										break;
									}
									array[num4] = "";
									num4--;
								}
							}
						}
						Array.Clear(array, 0, array.Length);
						goto end_IL_0001;
					}
					case 714:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Array.Clear(array, 0, array.Length);
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 714;
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

	public static string Get_Num_Suffix(int MyNo)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
					goto IL_000c;
				case 166:
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
							goto IL_0029;
						case 6:
							goto IL_0034;
						case 8:
							goto IL_003f;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
						case 7:
						case 9:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003f:
					num2 = 8;
					result = "3rd";
					goto end_IL_0001_3;
					IL_0034:
					num2 = 6;
					result = "2nd";
					goto end_IL_0001_3;
					IL_000c:
					num2 = 2;
					switch (MyNo)
					{
					case 1:
						break;
					case 2:
						goto IL_0034;
					case 3:
						goto IL_003f;
					default:
						goto end_IL_0001_2;
					}
					goto IL_0029;
					IL_0029:
					num2 = 4;
					result = "1st";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				result = Conversions.ToString(MyNo) + "th";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 166;
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

	public static string Translate_CompExpr(string ll_MajorAlias, string ll_DatabaseType)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		int num2 = default(int);
		string text;
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
						errsource = "BuildSQL - Translate_CompExpr";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						short num7 = 0;
						short num8 = 0;
						bool flag = false;
						bool flag2 = false;
						string text9 = "";
						short num9 = 0;
						string text10 = "";
						string text11 = "";
						short num10 = 0;
						text = "";
						ll_MajorAlias = Strings.LCase(ll_MajorAlias);
						num9 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
						num4 = 1;
						while (true)
						{
							IL_00a9:
							num6 = ((num4 == 1) ? Globals_Renamed.g_NoQColumns : ((!unchecked(num4 == 2 || num4 == 3)) ? Globals_Renamed.g_NoQJoins : Globals_Renamed.g_NoQFilters));
							short num11 = num6;
							num5 = 0;
							while (true)
							{
								if (num5 <= num11)
								{
									text9 = Conversions.ToString(unchecked((int)num5));
									text11 = "";
									switch (num4)
									{
									case 1:
										text6 = Globals_Renamed.g_QColumns[num5].Column;
										text2 = Globals_Renamed.g_QColumns[num5].level;
										num7 = (short)Strings.InStrRev(text2, ",");
										if (num7 != 0)
										{
											text8 = Strings.LCase(Strings.Trim(Strings.Mid(text2, 1, num7 - 1)));
											text2 = Strings.Trim(Strings.Mid(text2, num7 + 1));
										}
										else
										{
											text8 = "";
										}
										num10 = General_Procedures.SummPlusLevel(text2);
										if (num10 > 0)
										{
											text2 = Conversions.ToString(num10 + 100);
										}
										if (Operators.CompareString(ll_MajorAlias, "all", TextCompare: false) == 0)
										{
											text2 = "200";
											if ((Operators.CompareString(Strings.Mid(text6, 1, 12), "CrossTab->[[", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_QColumns[num5].AllStat, "", TextCompare: false) != 0))
											{
												text6 = "CrossTab->[[" + Globals_Renamed.g_QColumns[num5].AllStat;
											}
										}
										text9 = ((Operators.CompareString(Globals_Renamed.g_QColumns[num5].No, "", TextCompare: false) != 0) ? Globals_Renamed.g_QColumns[num5].No : "?");
										text3 = " in row " + text9 + " of the Columns Grid. ";
										break;
									case 2:
										text6 = Globals_Renamed.g_QFilters[num5].Column;
										text2 = Globals_Renamed.g_QFilters[num5].level;
										num7 = (short)Strings.InStrRev(text2, ",");
										if (num7 != 0)
										{
											text8 = Strings.LCase(Strings.Trim(Strings.Mid(text2, 1, num7 - 1)));
											text2 = Strings.Trim(Strings.Mid(text2, num7 + 1));
										}
										else
										{
											text8 = "";
										}
										num10 = General_Procedures.SummPlusLevel(text2);
										if (num10 > 0)
										{
											text2 = Conversions.ToString(num10 + 100);
										}
										if (Operators.CompareString(ll_MajorAlias, "all", TextCompare: false) == 0)
										{
											text2 = "200";
										}
										text3 = " in the " + Get_Num_Suffix(num5 + 1) + " relative row of the Filters Grid for this alias. ";
										break;
									case 3:
										text6 = Globals_Renamed.g_QFilters[num5].Value2;
										if (Strings.InStr(Strings.LCase(text6), "{col") == 0)
										{
											goto IL_0c2d;
										}
										text2 = Conversions.ToString(0);
										text8 = "";
										text3 = " in the " + Get_Num_Suffix(num5 + 1) + " relative row of the Filters Grid for this alias. ";
										break;
									default:
										text3 = " in row " + Conversions.ToString(num5 + 1) + " of the Joins Grid for this alias. ";
										text6 = ((num4 != 4) ? Globals_Renamed.g_QJoins[num5].Col2 : Globals_Renamed.g_QJoins[num5].Col1);
										text2 = "0";
										text11 = Strings.LCase(General_Procedures.Strip_Alias(1, text6)) + ".";
										if (Operators.CompareString(text11, "all.", TextCompare: false) == 0)
										{
											text11 = "";
										}
										text6 = General_Procedures.Strip_Alias(0, text6);
										if (Operators.CompareString(ll_MajorAlias, "all", TextCompare: false) == 0)
										{
											if (Operators.CompareString(text11, "", TextCompare: false) != 0)
											{
												text2 = "1";
											}
										}
										else
										{
											text11 = "";
										}
										break;
									}
									num7 = -99;
									flag2 = false;
									while (true)
									{
										IL_0adc:
										if ((num7 != 0) & (Operators.CompareString(text, "", TextCompare: false) == 0))
										{
											num7 = (short)Strings.InStr(text6, "{");
											num8 = 0;
											if (num7 == 0)
											{
												continue;
											}
											num8 = (short)Strings.InStr(text6, "}");
											if (num8 > num7)
											{
												text7 = Strings.Trim(Strings.Mid(text6, num7, (short)unchecked(num8 - num7) + 1));
												flag = false;
												short num12 = num9;
												num3 = 0;
												while (true)
												{
													if (num3 <= num12)
													{
														Type typeFromHandle = typeof(Strings);
														DataGridViewCell dataGridViewCell;
														object[] obj = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[12]).Value };
														object[] array = obj;
														bool[] obj2 = new bool[1] { true };
														bool[] array2 = obj2;
														object obj3 = NewLateBinding.LateGet(null, typeFromHandle, "LCase", obj, null, null, obj2);
														if (array2[0])
														{
															dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
														}
														text10 = Conversions.ToString(obj3);
														if (Operators.CompareString(Strings.LCase(text7), "{" + text10 + "}", TextCompare: false) != 0)
														{
															num3 = (short)unchecked(num3 + 1);
															continue;
														}
														unchecked
														{
															if ((Operators.CompareString(text2, "0", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) == 0))
															{
																if (num4 == 3)
																{
																	text5 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value);
																}
																else if (num4 == 4 || num4 == 5)
																{
																	text4 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value);
																	text4 = General_Procedures.Strip_Alias(1, text4);
																	if ((Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(text4), "ALL", TextCompare: false) != 0))
																	{
																		text5 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value);
																		if (Strings.InStr(text5, ".") == 0)
																		{
																			text5 = Strings.Trim(text4) + "." + Strings.Trim(text5);
																		}
																	}
																	else
																	{
																		text5 = General_Procedures.Strip_Alias(0, Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value));
																	}
																}
																else
																{
																	text5 = General_Procedures.Strip_Alias(0, Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[11].Value));
																}
																if ((Operators.CompareString(ll_MajorAlias, "all", TextCompare: false) == 0 && num4 == 2) & (Operators.CompareString(text2, "0", TextCompare: false) == 0))
																{
																	text2 = Conversions.ToString(1);
																}
															}
															else
															{
																text5 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].Cells[1].Value));
																if ((((Operators.CompareString(ll_MajorAlias, "all", TextCompare: false) == 0) & (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)) | (Operators.CompareString(ll_MajorAlias, "sql", TextCompare: false) == 0)) & (Operators.CompareString(text5, "", TextCompare: false) != 0))
																{
																	if (Operators.CompareString(Strings.Mid(text5, 1, 1), "[", TextCompare: false) != 0)
																	{
																		text5 = "[" + text5 + "]";
																	}
																	if (Operators.CompareString(Strings.Mid(text5, 1, 1), "[", TextCompare: false) != 0)
																	{
																		text5 = "[" + text5 + "]";
																	}
																}
																else if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 && Operators.CompareString(text5, "", TextCompare: false) != 0)
																{
																	if (Operators.CompareString(Strings.Mid(text5, 1, 1), "\"", TextCompare: false) != 0)
																	{
																		text5 = "\"" + text5 + "\"";
																	}
																}
																else if (((Operators.CompareString(ll_DatabaseType, "Hadoop", TextCompare: false) == 0) & (Operators.CompareString(text5, "", TextCompare: false) != 0)) && Operators.CompareString(Strings.Mid(text5, 1, 1), "`", TextCompare: false) != 0)
																{
																	text5 = "`" + text5 + "`";
																}
																if (Operators.CompareString(text5, "", TextCompare: false) == 0)
																{
																	text5 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num3].HeaderCell.Value);
																	text = "Column Number " + text5 + " used in the query must have a column header. Please add one.";
																	break;
																}
															}
															flag = true;
															flag2 = true;
															if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0 && num4 == 1)
															{
																text5 = "'$" + text5 + "'";
															}
														}
													}
													if (flag)
													{
														text6 = Strings.Replace(text6, text7, text5, 1, -1, CompareMethod.Text);
													}
													else
													{
														text = "No matching Column found for Token " + text7 + text3 + "Please add the base column to the Columns Grid or delete this expression." + General_Procedures.Get_UI("errhelp0");
													}
													goto IL_0adc;
												}
												goto end_IL_0c35;
											}
											text = "Missing end delimiter ('}')" + text3 + "Please correct the expression or delete it." + General_Procedures.Get_UI("errhelp0");
											continue;
										}
										text6 = Replace_ge_CRLF(text6, ll_DatabaseType);
										switch (num4)
										{
										case 1:
											if ((Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(text6, 1, 12), "CrossTab->[[", TextCompare: false) == 0) & (Operators.CompareString(Globals_Renamed.g_QColumns[num5].AllStat, "", TextCompare: false) != 0))
											{
												Globals_Renamed.g_QColumns[num5].AllStat = Strings.Mid(text6, 13);
											}
											else
											{
												Globals_Renamed.g_QColumns[num5].Column = text6;
											}
											break;
										case 2:
											Globals_Renamed.g_QFilters[num5].Column = text6;
											break;
										case 3:
											Globals_Renamed.g_QFilters[num5].Value2 = text6;
											break;
										case 4:
											Globals_Renamed.g_QJoins[num5].Col1 = text11 + text6;
											break;
										case 5:
											Globals_Renamed.g_QJoins[num5].Col2 = text11 + text6;
											break;
										}
										break;
									}
									goto IL_0c2d;
								}
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									break;
								}
								num4 = (short)unchecked(num4 + 1);
								if (num4 > 5)
								{
									break;
								}
								goto IL_00a9;
								IL_0c2d:
								num5 = (short)unchecked(num5 + 1);
								continue;
								end_IL_0c35:
								break;
							}
							break;
						}
						goto end_IL_0001;
					}
					case 3227:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							text = Information.Err().Description;
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					goto IL_0cd1;
				}
				end_IL_0001:;
			}
			catch (object obj4) when (obj4 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj4);
				try0001_dispatch = 3227;
				continue;
			}
			break;
			IL_0cd1:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public static string Generate_SQL(short ShowUN, string MyAlias, bool IsPacked = false, string MyPromptPrefix = "")
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		Globals_Renamed.fPostExtArr_Type[] fPostExtArr = default(Globals_Renamed.fPostExtArr_Type[]);
		int fNoPostExtArr = default(int);
		string left = default(string);
		int num = default(int);
		string text50 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					Globals_Renamed.AliasArray_Type[] g_AliasArray;
					string text;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "BuildSQL - Generate_SQL";
						short num3 = 0;
						TreeNode treeNode = null;
						TreeNode treeNode2 = null;
						text = "";
						string text2 = "";
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						short num7 = 0;
						int num8 = 0;
						int num9 = 0;
						int num10 = 0;
						string ll_DBType = "0";
						short ll_ObjectType = 0;
						string text3 = "";
						short num11 = 0;
						string text4 = "";
						string text5 = "";
						string text6 = "0";
						string text7 = "";
						string text8 = "";
						short num12 = 0;
						string text9 = "";
						string text10 = "";
						string text11 = "";
						string text12 = "";
						string text13 = "";
						string text14 = "";
						string text15 = "";
						string text16 = "";
						string text17 = "";
						string text18 = "";
						string text19 = "";
						string text20 = "";
						string text21 = "";
						bool flag = false;
						bool ll_OutInline = false;
						string text22 = "";
						bool flag2 = false;
						string text23 = "";
						string text24 = "";
						bool flag3 = false;
						string text25 = "";
						string text26 = "";
						string text27 = "";
						int num13 = 0;
						string text28 = "";
						string text29 = "";
						g_AliasArray = new Globals_Renamed.AliasArray_Type[134];
						short g_NoAlias = -1;
						string text30 = "";
						string text31 = "N";
						string text32 = "N";
						string text33 = "";
						short num14 = 0;
						string text34 = "";
						string text35 = "";
						short num15 = 0;
						short num16 = 0;
						string text36 = "";
						int num17 = -1;
						string text37 = "";
						short num18 = 0;
						int num19 = 0;
						string text38 = "";
						string text39 = "-1";
						string text40 = "";
						bool flag4 = false;
						bool flag5 = false;
						string text41 = "";
						string text42 = "N";
						string ll_M = "NO";
						short num20 = -1;
						fPostExtArr = new Globals_Renamed.fPostExtArr_Type[201];
						fNoPostExtArr = -1;
						int num21 = 0;
						int num22 = 0;
						string text43 = "NO";
						left = "NO";
						string text44 = "N";
						string text45 = "";
						string text46 = "";
						string text47 = "";
						bool flag6 = true;
						string ll_JoinDuckDB = "N";
						Array.Clear(fPostExtArr, 0, fPostExtArr.Length);
						fNoPostExtArr = -1;
						if (MyProject.Forms.FrmMain.mnuJoinDuckDB.Checked)
						{
							text42 = "Y";
						}
						Globals_Renamed.g_LastTblIdx = "";
						Globals_Renamed.ODBC_Prompt = "";
						Globals_Renamed.g_TS = "_" + DateAndTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CreateSpecificCulture("en-US")).Trim();
						if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) == 0)
						{
							gHasMacro = "N";
						}
						treeNode = BuildForm.FindNodeByName(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], "ROOTQ");
						if ((treeNode.GetNodeCount(includeSubTrees: false) > 2) & (Operators.CompareString(Strings.Trim(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline), "", TextCompare: false) != 0))
						{
							Interaction.MsgBox("You cannot create inline views that involve utilities or that contain more than one query", MsgBoxStyle.Critical, "Error Creating Inline View");
						}
						else
						{
							text27 = Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "SHOWSQL:All")));
							string text48 = text27;
							text28 = Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "QUOTECSV:All")));
							text29 = Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "EMPTYNULL:All")));
							text16 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "ROW:All"));
							text39 = ((Operators.CompareString(text16, "No", TextCompare: false) != 0) ? text16 : "-1");
							text16 = "";
							text40 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DISTINCT:All"));
							string myMode = "E";
							if (Operators.CompareString(MyAlias, "MAINQ", TextCompare: false) == 0)
							{
								myMode = "Q";
								MyAlias = "";
							}
							short num23 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
							for (num4 = 0; num4 <= num23; num4 = (short)unchecked(num4 + 1))
							{
								if (Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[7].Value, "", TextCompare: false))
								{
									MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[7].Value = "None";
								}
								string left2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[2].Value);
								string myText = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[11].Value);
								myText = General_Procedures.Strip_Alias(1, myText);
								if (Operators.ConditionalCompareObjectNotEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[7].Value, "None", TextCompare: false) || Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num4].Cells[2].Value, "Y:Strip", TextCompare: false) || (Operators.CompareString(left2, "--", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(myText), "all", TextCompare: false) == 0))
								{
									flag4 = true;
								}
							}
							if (Operators.CompareString(MyAlias, "", TextCompare: false) == 0)
							{
								flag2 = false;
								MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Load_Tree_Alias_To_Array(myMode, ref g_NoAlias, ref g_AliasArray, Do_Valid_Inc: true, flag4, MyPromptPrefix);
								if ((Operators.CompareString(g_AliasArray[0].Alias_Renamed, "TXT", TextCompare: false) == 0) | (Operators.CompareString(g_AliasArray[0].Alias_Renamed, "SQL", TextCompare: false) == 0))
								{
									text16 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "STACK:All"));
									if (Operators.CompareString(text16, "Yes", TextCompare: false) == 0)
									{
										flag = true;
										num19 = treeNode.GetNodeCount(includeSubTrees: false);
										if (unchecked(num19 > 3 || flag4))
										{
											Interaction.MsgBox("Interleave queries are only supported for Single SQLite or MS Jet Queries.", MsgBoxStyle.Critical, "Interleave Conditions are Invalid");
											text = "";
											goto IL_4074;
										}
									}
									else
									{
										flag = false;
									}
								}
								if (((g_NoAlias == 1) & LikeOperator.LikeString(Strings.LCase(g_AliasArray[0].Alias_Renamed), "a*", CompareMethod.Binary)) && Operators.CompareString(Strings.Trim(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline), "", TextCompare: false) != 0)
								{
									ll_OutInline = true;
								}
								if (Operators.CompareString(Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "STACK:All"))), "NO", TextCompare: false) == 0)
								{
									DataGridView MyGrid = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery;
									text16 = Test_Dup_hdrs(ref MyGrid);
									if (Operators.CompareString(text16, "", TextCompare: false) != 0)
									{
										Interaction.MsgBox(text16, MsgBoxStyle.Exclamation, "Duplicate Column Headers Found");
										text = "";
										goto IL_4074;
									}
								}
							}
							else
							{
								flag2 = true;
								num4 = 0;
								do
								{
									g_AliasArray[num4].Alias_Renamed = "";
									g_AliasArray[num4].DBType = "";
									g_AliasArray[num4].Prompt = "";
									g_AliasArray[num4].Tag = "";
									num4 = (short)unchecked(num4 + 1);
								}
								while (num4 <= 132);
								g_NoAlias = 1;
								g_AliasArray[0].Alias_Renamed = MyAlias;
								if (LikeOperator.LikeString(MyAlias, "UTILITIES:*", CompareMethod.Binary))
								{
									string text49 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("G", MyAlias);
									num8 = Strings.InStr(text49, ":");
									if (num8 != 0)
									{
										text49 = Strings.Mid(text49, 1, num8) + "Y" + Strings.Mid(text49, num8 + 1);
									}
									g_AliasArray[0].DBType = text49;
								}
								else
								{
									string text49 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + MyAlias));
									g_AliasArray[0].DBType = text49;
									string MyError = "";
									flag6 = true;
									VerifyValidIncQuery_v2(ref MyError, ref flag6, MyAlias);
									if (!flag6)
									{
										g_AliasArray[0].Tag = "RAW";
										g_NoAlias++;
										g_AliasArray[1].Alias_Renamed = MyAlias;
										g_AliasArray[1].DBType = "DUCKDB";
										g_AliasArray[1].Prompt = "";
										g_AliasArray[1].Tag = "AGG";
									}
								}
							}
							flag3 = false;
							short num24 = (short)(g_NoAlias - 1);
							for (num4 = 0; num4 <= num24; num4 = (short)unchecked(num4 + 1))
							{
								if (Operators.CompareString(Strings.UCase(g_AliasArray[num4].Alias_Renamed), "ALL", TextCompare: false) == 0)
								{
									flag3 = true;
									break;
								}
							}
							if (unchecked(!flag3 && !flag2))
							{
								text33 = "SN";
							}
							else if (flag3)
							{
								text33 = "J";
							}
							num5 = 0;
							do
							{
								switch (num5)
								{
								case 0:
									text16 = "*{START-MACRO}*";
									text18 = "{END-MACRO}";
									text17 = "macros using the Legacy extract engine";
									break;
								case 1:
									text16 = "*{SITE-LOOP}*";
									text18 = "{END-LOOP}";
									text17 = "Site-Loops";
									break;
								case 2:
									text16 = "*{BEGIN-HPC}*";
									text18 = "{END-HPC}";
									text17 = "HPC-Blocks";
									break;
								default:
									text16 = "*{RUN-LOOP}*\"Y\"";
									text18 = "{END-LOOP}";
									text17 = "Run-Loops where you can continue on despite Query Errors";
									break;
								}
								num15 = 0;
								num16 = 0;
								if (num5 != 0 || Operators.CompareString(Globals_Renamed.gUsePyEngine, "Y", TextCompare: false) != 0)
								{
									short num25 = (short)(g_NoAlias - 1);
									for (num4 = 0; num4 <= num25; num4 = (short)unchecked(num4 + 1))
									{
										if (LikeOperator.LikeString(Strings.UCase(g_AliasArray[num4].DBType), text16, CompareMethod.Binary))
										{
											num15++;
										}
										else if (num5 >= 1 && (Strings.InStr(Strings.UCase(g_AliasArray[num4].DBType), "{FOR-LOOP}") != 0 || Strings.InStr(Strings.UCase(g_AliasArray[num4].DBType), "{RUN-LOOP}") != 0) && num15 > 0)
										{
											num16++;
										}
										else if (unchecked(Strings.InStr(Strings.UCase(g_AliasArray[num4].DBType), text18) != 0 && num15 > 0))
										{
											if (num16 == 0)
											{
												num15--;
											}
											else
											{
												num16--;
											}
										}
										if (num15 > 1)
										{
											break;
										}
									}
									if (num15 > 1)
									{
										break;
									}
								}
								num5 = (short)unchecked(num5 + 1);
							}
							while (num5 <= 3);
							if (num15 > 1)
							{
								text19 = ". Problem occurs in ";
								if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) != 0)
								{
									text19 = text19 + " Pre/ Post Query at " + MyPromptPrefix + " -> ";
								}
								text19 = text19 + "Step " + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", g_AliasArray[num4].Alias_Renamed);
								Interaction.MsgBox("SQLPathFinder does not support nested " + text17 + text19, MsgBoxStyle.Critical, "Nested Utility Error");
								text = "";
							}
							else
							{
								text16 = Chk_Query_Util(ref g_NoAlias, ref g_AliasArray);
								if (Operators.CompareString(text16, "", TextCompare: false) != 0)
								{
									Interaction.MsgBox(text16, MsgBoxStyle.Critical, "Incorrectly Structured Utilities");
									text = "";
								}
								else
								{
									text16 = "";
									num15 = 0;
									num16 = 0;
									short num26 = (short)(g_NoAlias - 1);
									for (num4 = 0; num4 <= num26; num4 = (short)unchecked(num4 + 1))
									{
										if (LikeOperator.LikeString(Strings.UCase(g_AliasArray[num4].DBType), "*{BEGIN-HPC}*", CompareMethod.Binary))
										{
											num15++;
										}
										else if (LikeOperator.LikeString(Strings.UCase(g_AliasArray[num4].DBType), "*{END-HPC}*", CompareMethod.Binary))
										{
											num16++;
											if (num16 > num15)
											{
												text16 = "Begin-HPC utilities must occur before End-HPC utilities.";
												break;
											}
										}
									}
									if (Operators.CompareString(text16, "", TextCompare: false) == 0 && num15 != num16)
									{
										text16 = "There are an inconsistent number of enabled Begin-HPC and End-HPC utilities.";
									}
									if (Operators.CompareString(text16, "", TextCompare: false) != 0)
									{
										Interaction.MsgBox(text16, MsgBoxStyle.Critical, "Query Structure Error");
										text = "";
									}
									else
									{
										short num27 = (short)(g_NoAlias - 1);
										num4 = 0;
										while (true)
										{
											if (num4 <= num27)
											{
												text26 = "";
												text30 = "N";
												text31 = "N";
												text32 = "N";
												text35 = "";
												text44 = "N";
												string MyAlias2 = g_AliasArray[num4].Alias_Renamed;
												string text49 = g_AliasArray[num4].DBType;
												text24 = "";
												if (LikeOperator.LikeString(MyAlias2, "UTILITIES:*", CompareMethod.Binary))
												{
													num13 = 0;
													text24 = text49;
													text26 = g_AliasArray[num4].Prompt;
													num8 = Strings.InStr(text24, ":");
													if (num8 != 0)
													{
														if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "       ", 1, 7)), "U->SQL:", TextCompare: false) == 0)
														{
															text14 = "";
															text2 = Strings.Trim(Strings.Mid(text24, num8 + 2));
															text2 = BuildForm.SubStitute_Nodes_SQL(text2);
														}
														else
														{
															if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "                  ", 1, 18)), "U->PRE/POST QUERY:", TextCompare: false) == 0)
															{
																text37 = General_Procedures.Get_Node_Value(text24);
																num17 = 0;
																if (MyProject.Forms.FrmMain.mnuEmbedPrePost.Checked)
																{
																	text36 = Strings.Trim(Globals_Renamed.gSPFCache) + ".vg2";
																	num17 = BuildEmbedded.GetQuerySave(text37, text36);
																	text37 = Globals_Renamed.MyPCDir + text36;
																}
																if (Strings.InStrRev(text37, "\\") == 0)
																{
																	text37 = Globals_Renamed.gQueryDir + text37;
																}
																if (MyProject.Computer.FileSystem.FileExists(text37) && num17 != -1)
																{
																	if (Globals_Renamed.g_FrmIdx + 1 > 2)
																	{
																		text = "";
																		text2 = "$PPQERROR$";
																		Interaction.MsgBox("Error at step: " + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", g_AliasArray[num4].Alias_Renamed) + ". Pre/Post queries cannot nest beyond 2 levels", MsgBoxStyle.Exclamation, "Query Levels");
																	}
																	else
																	{
																		Globals_Renamed.g_FrmIdx++;
																		num20 = Globals_Renamed.g_FrmIdx;
																		if (MyProject.Forms.FrmMain.OpenQuery(text37, 0) != 0)
																		{
																			if (Operators.CompareString(Strings.Trim(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline), "", TextCompare: false) == 0)
																			{
																				num9 = Strings.InStr(text26, ". ");
																				if (num9 != 0)
																				{
																					text26 = Strings.Trim(Strings.Mid(text26, 1, num9 - 1));
																				}
																				num20 = Globals_Renamed.g_FrmIdx;
																				text2 = Generate_SQL(1, "", IsPacked, text26);
																				if (Operators.CompareString(text2, "", TextCompare: false) == 0)
																				{
																					text2 = "$PPQERROR$";
																				}
																			}
																			else
																			{
																				text2 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].SaveInlineView(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline);
																				if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) == 0)
																				{
																					text2 = "$ILVERROR$";
																				}
																				else if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0)
																				{
																					if (General_Procedures.Save_SQL_Query(text2, Globals_Renamed.MyPCDir + Globals_Renamed.g_TmpSPFVG2 + ".va") == 0)
																					{
																						text2 = "$ILVERROR$";
																					}
																					else
																					{
																						text34 = General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\"&&va \"" + Globals_Renamed.g_TmpSPFVG2 + ".va\"", 1, 0, 0);
																						if (Operators.CompareString(text34, "", TextCompare: false) != 0)
																						{
																							text2 = "$ILVERROR$";
																							Interaction.MsgBox("Error generating Inline View: " + text34, MsgBoxStyle.Exclamation, "Error");
																						}
																						else
																						{
																							text2 = "";
																						}
																					}
																				}
																				else if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
																				{
																					text2 = "";
																				}
																			}
																		}
																		else
																		{
																			text2 = "$PPQERROR$";
																		}
																		if (!Information.IsNothing(MyProject.Forms.FrmMain.FrmSQLQuery[num20]))
																		{
																			MyProject.Forms.FrmMain.FrmSQLQuery[num20].Close();
																			MyProject.Forms.FrmMain.FrmSQLQuery[num20].Dispose();
																			MyProject.Forms.FrmMain.FrmSQLQuery[num20] = null;
																			Globals_Renamed.g_FrmIdx = (short)(num20 - 1);
																			num20 = -1;
																		}
																	}
																	text14 = "";
																	switch (text2)
																	{
																	case "$ILVERROR$":
																		break;
																	case "$PPQERROR$":
																		goto IL_1396;
																	case "$PPQERROR$OK":
																		goto IL_13b8;
																	default:
																		goto IL_3f13;
																	}
																	Interaction.MsgBox("Could not generate Inline View in pre/post query step " + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", g_AliasArray[num4].Alias_Renamed) + ". Please correct the sub-query or remove it ", MsgBoxStyle.Exclamation, "Inline View Not Generated");
																	text = "";
																	text2 = "";
																}
																else
																{
																	Interaction.MsgBox("Could not locate pre/post query in step " + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", g_AliasArray[num4].Alias_Renamed) + ". Please correct the query path or remove the sub-query ", MsgBoxStyle.Exclamation, "Pre/Post Query Not Found");
																	text = "";
																}
																break;
															}
															if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "            ", 1, 12)), "U->CHART-IN-", TextCompare: false) == 0)
															{
																treeNode2 = BuildForm.FindNodeByName(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], g_AliasArray[num4].Tag);
																text2 = BuildChart.Get_All_JSL(ref treeNode2, 0);
																if (Operators.CompareString(text2, "", TextCompare: false) != 0)
																{
																	text18 = ((Operators.CompareString(text26, "", TextCompare: false) == 0) ? Strings.Replace(Globals_Renamed.gJSLOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text) : Strings.Replace(Globals_Renamed.gJSLOptions, "@PROMPT@", "/PROMPT-TEXT=" + text26 + "\r\n", 1, -1, CompareMethod.Text));
																	text2 = text18 + text2;
																}
																text14 = "";
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "              ", 1, 14)), "U->HTML-REPORT", TextCompare: false) == 0)
															{
																treeNode2 = BuildForm.FindNodeByName(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].TreeCol.Nodes[0], g_AliasArray[num4].Tag);
																text2 = BuildChart.Get_All_JSL(ref treeNode2, 0, RunImmediate: false, text26, 1);
																text14 = "";
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "              ", 1, 14)), "U->RUN-CB-ACS:", TextCompare: false) == 0)
															{
																text14 = "";
																text2 = Strings.Trim(Strings.Mid(text24, num8 + 2));
																if (Operators.CompareString(text2, "", TextCompare: false) != 0)
																{
																	text18 = ((Operators.CompareString(text26, "", TextCompare: false) == 0) ? Strings.Replace(Globals_Renamed.gCBOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text) : Strings.Replace(Globals_Renamed.gCBOptions, "@PROMPT@", "/PROMPT-TEXT=" + text26 + "\r\n", 1, -1, CompareMethod.Text));
																	text2 = text18 + text2;
																}
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "              ", 1, 14)), "U->WRITE-FILE:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "               ", 1, 15)), "U->WRITE-FILE2:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "               ", 1, 15)), "U->WRITE-FILE3:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->KS-FIELDS:", TextCompare: false) == 0)
															{
																text14 = "";
																text2 = Strings.Trim(Strings.Mid(text24, num8 + 2));
																text16 = "";
																num9 = Strings.InStr(text2, "<SOF>");
																if (num9 != 0)
																{
																	text16 = Strings.Trim(Strings.Mid(text2, 1, num9 - 1));
																	text2 = Strings.Trim(Strings.Mid(text2 + "        ", num9 + 5));
																}
																if (Operators.CompareString(text2, "", TextCompare: false) != 0)
																{
																	if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "              ", 1, 15)), "U->WRITE-FILE3:", TextCompare: false) == 0)
																	{
																		num9 = Strings.InStr(text2, "<EOF>");
																		if (num9 > 1)
																		{
																			text2 = Strings.Trim(Strings.Mid(text2, 1, num9 - 1));
																		}
																		if (Operators.CompareString(text2, "", TextCompare: false) != 0)
																		{
																			text17 = "";
																			if (ShowUN == 1)
																			{
																				if (Strings.InStr(text16, "\\") == 0)
																				{
																					text16 = Globals_Renamed.MyPCDir + text16;
																				}
																				text16 = BuildForm.Replace_Globals(text16);
																				text2 = BuildForm.Replace_Globals(text2);
																				text17 = General_Procedures.Save_File_General(text2, text16);
																			}
																			text2 = "";
																			text14 = "";
																			if (Operators.CompareString(text17, "", TextCompare: false) != 0)
																			{
																				Interaction.MsgBox("Error with Utility Write-File-Immediate. " + text17, MsgBoxStyle.Exclamation, "Could not Save");
																				text = "";
																				break;
																			}
																			goto IL_401f;
																		}
																	}
																	else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "            ", 1, 13)), "U->KS-FIELDS:", TextCompare: false) == 0)
																	{
																		text18 = Strings.UCase(Strings.Mid(text16, 1, 1));
																		text17 = Strings.UCase(Strings.Mid(text16, 2, 2));
																		text16 = Strings.Mid(text16, 4);
																		text16 = BuildForm.Replace_Globals(text16);
																		text2 = BuildForm.Replace_Globals(text2);
																		if (Operators.CompareString(text18, "M", TextCompare: false) == 0)
																		{
																			num9 = Strings.InStr(text2, "<EOF>");
																			if (num9 > 1)
																			{
																				text2 = Strings.Trim(Strings.Mid(text2, 1, num9 - 1));
																			}
																			string[] array = Strings.Split(text2, "\r\n");
																			text2 = "";
																			int num28 = Information.UBound(array);
																			for (int i = 0; i <= num28; i++)
																			{
																				if (Operators.CompareString(Strings.Trim(array[i]), "", TextCompare: false) != 0)
																				{
																					string[] array2 = Strings.Split(array[i], ",");
																					if (Operators.CompareString(Strings.Trim(array2[0]), "", TextCompare: false) != 0)
																					{
																						text2 = ((Operators.CompareString(text2, "", TextCompare: false) != 0) ? (text2 + "\r\n" + Strings.Trim(array2[0])) : Strings.Trim(array2[0]));
																					}
																					array2 = null;
																				}
																			}
																			array = null;
																			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
																			{
																				text2 += "\r\n<EOF>";
																			}
																		}
																		text18 = ((Operators.CompareString(text26, "", TextCompare: false) == 0) ? Strings.Replace(Globals_Renamed.gWFOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text) : Strings.Replace(Globals_Renamed.gWFOptions, "@PROMPT@", "/PROMPT-TEXT=" + text26 + "\r\n", 1, -1, CompareMethod.Text));
																		text2 = Strings.Replace(text18, "@FILE@", text16, 1, -1, CompareMethod.Text) + text2;
																	}
																	else
																	{
																		text18 = ((Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "              ", 1, 14)), "U->WRITE-FILE:", TextCompare: false) != 0 || Operators.CompareString(text26, "", TextCompare: false) == 0) ? Strings.Replace(Globals_Renamed.gWFOptions, "@PROMPT@", "", 1, -1, CompareMethod.Text) : Strings.Replace(Globals_Renamed.gWFOptions, "@PROMPT@", "/PROMPT-TEXT=" + text26 + "\r\n", 1, -1, CompareMethod.Text));
																		text2 = Strings.Replace(text18, "@FILE@", text16, 1, -1, CompareMethod.Text) + text2;
																	}
																}
																BuildForm.Garbage_Collect();
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->CSR-INPUT:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->MMS-INPUT:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "               ", 1, 15)), "U->MMSWF-INPUT:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->SDC-INPUT:", TextCompare: false) == 0)
															{
																text14 = "";
																if (Strings.Len(text24) >= 15)
																{
																	text16 = Strings.Trim(Strings.Mid(text24, num8 + 2));
																	text18 = ((Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->CSR-INPUT:", TextCompare: false) == 0) ? "CSR" : ((Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "             ", 1, 13)), "U->SDC-INPUT:", TextCompare: false) == 0) ? "SDC" : ((Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "               ", 1, 15)), "U->MMSWF-INPUT:", TextCompare: false) != 0) ? "MMS" : "MMSWF")));
																	text2 = Process_CSR_MMS(text16, text18);
																	if (Operators.CompareString(text2, "", TextCompare: false) == 0)
																	{
																		text = "";
																		break;
																	}
																	text2 = ((Operators.CompareString(text26, "", TextCompare: false) == 0) ? Strings.Replace(text2, "@PROMPT@", "Create " + text18 + " Signal", 1, -1, CompareMethod.Text) : Strings.Replace(text2, "@PROMPT@", text26, 1, -1, CompareMethod.Text));
																}
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "                ", 1, 16)), "U->PROMPT-INPUT:", TextCompare: false) == 0)
															{
																if (Strings.Len(text24) >= 18)
																{
																	text16 = Strings.Trim(Strings.Mid(text24, 42));
																	text2 = "&PROMPT&(INPU^" + text16 + ")&PROMPT&";
																	text14 = Set_SQL_Query_Options(ShowUN, 5, Conversions.ToString(3), MyAlias2, "", "", "", -1, "", "", "", "", ll_UnionSort: false, "", "", "", text24, "", text26, text3, text30, text27, text35, text28, text29, "", text31, text44, "N", num13);
																}
															}
															else if (Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "                  ", 1, 18)), "U->MONGODB-IMPORT:", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Strings.Mid(text24 + "         ", 1, 19)), "U->MONGODB-EXTRACT:", TextCompare: false) == 0)
															{
																num9 = Strings.InStr(Strings.LCase(text24), ".va \"");
																if (num9 != 0)
																{
																	text16 = Strings.Mid(text24, num9 + 5);
																	num10 = Strings.InStr(text16, "\" ");
																	if (num10 > 0)
																	{
																		text16 = Strings.Trim(Strings.UCase(Strings.Mid(text16, 1, num10 - 1)));
																	}
																}
																text18 = General_Procedures.NodeCheck_s(BuildForm.Replace_Globals(text16), "S", 1);
																if (Operators.CompareString(text16, "", TextCompare: false) != 0 && Operators.CompareString(text16, text18, TextCompare: false) != 0)
																{
																	text24 = Strings.Replace(text24, text16, text18, 1, -1, CompareMethod.Text);
																}
																text14 = Set_SQL_Query_Options(ShowUN, 5, Conversions.ToString(3), MyAlias2, "", "", "", -1, "", "", "", "", ll_UnionSort: false, "", "", "", text24, "", text26, text3, text30, text27, text35, text28, text29, "", text31, text44, "N", num13);
																text2 = "";
															}
															else
															{
																text16 = Strings.Mid(text24, 4, num8 - 4);
																if ((Operators.CompareString(text16, "Write", TextCompare: false) == 0) | (Operators.CompareString(text16, "Wait", TextCompare: false) == 0))
																{
																	text24 = BuildForm.Get_Special_Util(text16, "S", Strings.Mid(text24, num8 + 2));
																}
																else if ((Operators.CompareString(text16, "Get-Site-Time", TextCompare: false) == 0) | (Operators.CompareString(text16, "Site-Loop", TextCompare: false) == 0))
																{
																	if (Operators.CompareString(text16, "Site-Loop", TextCompare: false) == 0)
																	{
																		string[] array3 = Regex.Split(text24, "\" \"");
																		num13 = ((array3.Length >= 2) ? Conversions.ToInteger(array3[1].Replace("\"", "")) : 0);
																	}
																	text24 = Strings.Mid(text24, num8 + 2);
																	text24 = BuildForm.SubStitute_Nodes_SQL(text24);
																}
																else if (Operators.CompareString(Strings.Mid(Strings.UCase(text24), 1, 14), "U->START-MACRO", TextCompare: false) == 0)
																{
																	text24 = Strings.Mid(text24, num8 + 2);
																	if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) == 0)
																	{
																		gHasMacro = "Y";
																	}
																	else if (Operators.CompareString(gHasMacro, "Y", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.gUsePyEngine, "N", TextCompare: false) == 0)
																	{
																		Interaction.MsgBox("SQLPathFinder does not support nested Macros using the Legacy Engine. Problem occurs in Pre/Post Query at " + MyPromptPrefix + " -> " + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", g_AliasArray[num4].Alias_Renamed), MsgBoxStyle.Exclamation, "Nested Macro");
																		text = "";
																		text2 = "";
																		break;
																	}
																}
																else if (Operators.CompareString(Strings.Mid(Strings.UCase(text24), 1, 12), "U->END-MACRO", TextCompare: false) == 0)
																{
																	text24 = Strings.Mid(text24, num8 + 2);
																	if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) == 0)
																	{
																		gHasMacro = "N";
																	}
																}
																else if (LikeOperator.LikeString(Strings.UCase(text24), "U->FOR-LOOP*", CompareMethod.Binary))
																{
																	string[] array4 = Regex.Split(text24, "\" \"");
																	num13 = ((array4.Length >= 5) ? Conversions.ToInteger(array4[4].Replace("\"", "")) : 0);
																	text24 = Strings.Mid(text24, num8 + 2);
																}
																else if (LikeOperator.LikeString(Strings.UCase(text24), "U->RUN-LOOP*", CompareMethod.Binary))
																{
																	string[] array5 = Regex.Split(text24, "\" \"");
																	num13 = ((array5.Length >= 5) ? Conversions.ToInteger(array5[4].Replace("\"", "")) : 0);
																	text24 = Strings.Mid(text24, num8 + 2);
																}
																else
																{
																	text24 = Strings.Mid(text24, num8 + 2);
																}
																text14 = Set_SQL_Query_Options(ShowUN, 5, Conversions.ToString(3), MyAlias2, "", "", "", -1, "", "", "", "", ll_UnionSort: false, "", "", "", text24, "", text26, text3, text30, text27, text35, text28, text29, "", text31, text44, "N", num13);
																text2 = "";
															}
														}
													}
												}
												else if (Operators.CompareString(Strings.UCase(MyAlias2), "ALL", TextCompare: false) == 0)
												{
													text6 = text39;
													text5 = text40;
													num13 = 0;
													text47 = "";
													text16 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "STACK:All"));
													flag = Operators.CompareString(text16, "Yes", TextCompare: false) == 0;
													text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "TERMINAL:All"));
													Globals_Renamed.ODBC_Prompt = "";
													text7 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_odbc;
													Globals_Renamed.ODBC_Prompt = BuildForm.Replace_Globals(text7);
													if (LikeOperator.LikeString(Strings.UCase(Globals_Renamed.ODBC_Prompt), "*.PARQUET*", CompareMethod.Binary))
													{
														Interaction.MsgBox("Parquet output is currently only supported for imBigData, MongoDB and MIDAS-HBASE View Output. It is not yet supported following a Join. Please change your Output File.", MsgBoxStyle.Exclamation, "Invalid Output File");
														text = "";
														break;
													}
													ll_DBType = "5";
													ll_ObjectType = 1;
													MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ClearFromTables(MyAlias2, "I");
													text16 = text20;
													num7 = -1;
													text18 = ((Operators.CompareString(text42, "Y", TextCompare: false) != 0) ? "," : "@@@@@");
													short num29 = (short)(g_NoAlias - 1);
													for (num6 = 0; num6 <= num29; num6 = (short)unchecked(num6 + 1))
													{
														if (Operators.CompareString(g_AliasArray[num6].Alias_Renamed, "All", TextCompare: false) != 0 && Operators.CompareString(Strings.Mid(g_AliasArray[num6].Alias_Renamed, 1, 1), "U", TextCompare: false) != 0 && Operators.CompareString(g_AliasArray[num6].Tag, "RAW", TextCompare: false) != 0)
														{
															num7++;
															num8 = Strings.InStr(text16, text18);
															if (num8 != 0)
															{
																if (Operators.CompareString(text42, "Y", TextCompare: false) == 0)
																{
																	text17 = Strings.Mid(text16, 1, num8 - 1);
																	text17 = Strings.Mid(text17, 8);
																	num9 = Strings.InStrRev(text17, ":");
																	if (num9 != 0)
																	{
																		text17 = Strings.Mid(text17, 1, num9 - 1);
																	}
																	Globals_Renamed.FromTables[num7].Table = text17 + " " + Strings.LCase(g_AliasArray[num6].Alias_Renamed);
																	text16 = Strings.Mid(text16, num8 + 5);
																}
																else
																{
																	Globals_Renamed.FromTables[num7].Table = Strings.Trim(Strings.Mid(text16, 1, num8 - 1)) + " " + Strings.LCase(g_AliasArray[num6].Alias_Renamed);
																	text16 = Strings.Mid(text16, num8 + 1);
																}
															}
															else
															{
																if (Operators.CompareString(text16, "", TextCompare: false) == 0)
																{
																	break;
																}
																if (Operators.CompareString(text42, "Y", TextCompare: false) == 0)
																{
																	text16 = Strings.Mid(text16, 8);
																	num9 = Strings.InStrRev(text16, ":");
																	if (num9 != 0)
																	{
																		text16 = Strings.Mid(text16, 1, num9 - 1);
																	}
																}
																Globals_Renamed.FromTables[num7].Table = Strings.Trim(text16) + " " + Strings.LCase(g_AliasArray[num6].Alias_Renamed);
																text16 = "";
															}
															Globals_Renamed.FromTables[num7].Schema = "";
															Globals_Renamed.FromTables[num7].Node = "";
														}
													}
													Globals_Renamed.MaxFromTables = (short)(num7 + 1);
													text3 = "";
													num11 = -1;
													if (Operators.CompareString(left, "YES", TextCompare: false) == 0)
													{
														int num30 = fNoPostExtArr;
														for (num21 = 0; num21 <= num30; num21++)
														{
															if (Operators.CompareString(fPostExtArr[num21].mode, "L", TextCompare: false) == 0)
															{
																MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[fPostExtArr[num21].row].Cells[2].Value = "--";
															}
														}
													}
													Populate_Query_Arrays_v2(MyAlias2, flag, ll_ObjectType, "", text49, text47, text45);
													if (Operators.CompareString(left, "YES", TextCompare: false) == 0)
													{
														int num31 = fNoPostExtArr;
														for (num21 = 0; num21 <= num31; num21++)
														{
															if (Operators.CompareString(fPostExtArr[num21].mode, "J", TextCompare: false) == 0)
															{
																num22 = Globals_Renamed.g_NoQColumns + 1;
																Globals_Renamed.g_QColumns[num22].Column = fPostExtArr[num21].show;
																Globals_Renamed.g_QColumns[num22].Header = "";
																Globals_Renamed.g_QColumns[num22].Show = "Y";
																Globals_Renamed.g_QColumns[num22].Sort = "None";
																Globals_Renamed.g_QColumns[num22].Statistics = "None";
																Globals_Renamed.g_QColumns[num22].Pivot = "Row";
																Globals_Renamed.g_QColumns[num22].AllStat = "None";
																Globals_Renamed.g_QColumns[num22].DataType = "c";
																Globals_Renamed.g_QColumns[num22].level = "s,0";
																Globals_Renamed.g_QColumns[num22].List = "CSV";
																Globals_Renamed.g_NoQColumns++;
															}
														}
													}
													text35 = Get_SQLite_or_DuckDB_DT(text42);
													text12 = "";
													text10 = "";
													text11 = "";
													text13 = "";
													text21 = "";
													text2 = "";
													text23 = "";
													text23 = Create_Header_Opt(text49);
													text9 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "CTMISS:All"));
													if (Operators.CompareString(text9, "No", TextCompare: false) == 0)
													{
														text9 = "";
													}
													if (!flag)
													{
														text2 = Generate_SQL_2(ShowUN, ll_ObjectType, ll_DBType, MyAlias2, text49, text3, num11, text9, ref text12, ref text10, ref text11, text6, text5, ll_OutInline, ref text13, "", IsPacked, text32, "", MyPromptPrefix, text28, fNoPostExtArr, ref fPostExtArr, text42);
														text21 = ((Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0) ? Get_Sort(2, 1) : (MyProject.Forms.FrmMain.mnuPivotSQLite.Checked ? Get_Sort(2, 1) : ((!MyProject.Forms.FrmMain.mnuPivotSQLite.Checked) ? Get_Sort(1, 1) : Get_Sort(2))));
													}
													else
													{
														text21 = Get_Sort(0, 1);
													}
													if (unchecked(Operators.CompareString(text2, "", TextCompare: false) == 0 && !flag))
													{
														text = "";
														break;
													}
													text22 = "*Instance*";
													text41 = Create_Replace_Headers();
													if (MyProject.Forms.FrmMain.mnuAutoGrid.Checked && Operators.CompareString(text33, "J", TextCompare: false) == 0 && flag3 && num4 == g_NoAlias - 1 && (Operators.CompareString(text4, "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text4), "YES", TextCompare: false) == 0))
													{
														text4 = "G";
													}
													text14 = Set_SQL_Query_Options(ShowUN, ll_ObjectType, ll_DBType, MyAlias2, text49, text4, text7, -1, text12, text11, text10, text20, flag, text21, text22, text23, text24, text13, text26, text3, text30, text27, text35, text28, text29, text41, text31, text44, text42, num13);
													text2 = Strings.Replace(text2, "<<<SPF-OUTPUTFILE>>>", text7, 1, -1, CompareMethod.Text);
													text22 = "";
													if (Operators.CompareString(text14, "$ERROR$", TextCompare: false) == 0)
													{
														text = "";
														break;
													}
												}
												else
												{
													text47 = g_AliasArray[num4].Tag;
													if (Operators.CompareString(text47, "AGG", TextCompare: false) == 0)
													{
														ll_ObjectType = 1;
														ll_DBType = Conversions.ToString(5);
														text46 = ".\\";
														MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ClearFromTables(MyAlias2, "AGG");
													}
													else
													{
														MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Database_Node_Plus(ref MyAlias2, ref ll_DBType, ref ll_ObjectType);
														text46 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DATABASE:" + MyAlias2);
														text46 = General_Procedures.Get_Node_Value(text46);
														MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].ClearFromTables(MyAlias2, "A2");
													}
													text48 = text27;
													num13 = 0;
													if (ll_ObjectType == 2)
													{
														text3 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "VIEW:" + MyAlias2);
														if (LikeOperator.LikeString(Strings.UCase(text3), "TEST_OR_SORT_UNIT_OR_DIE_TEST_RESULTS_HBASE_MIDAS*", CompareMethod.Binary) || Operators.CompareString(Strings.UCase(text49), "IMBIGDATA", TextCompare: false) == 0)
														{
															text45 = text45 + "," + Strings.LCase(MyAlias2);
														}
														text16 = Strings.Trim(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "PARALLEL:" + MyAlias2)));
														if (Operators.CompareString(text16, "0", TextCompare: false) >= 0 && Operators.CompareString(text16, "9", TextCompare: false) <= 0)
														{
															num13 = Conversions.ToInteger(text16);
														}
													}
													else
													{
														text3 = "";
													}
													if ((Operators.CompareString(Strings.UCase(MyAlias2), "TXT", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(MyAlias2), "SQL", TextCompare: false) == 0))
													{
														text16 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "STACK:All"));
														if (Operators.CompareString(text16, "Yes", TextCompare: false) == 0)
														{
															flag = true;
															num19 = treeNode.GetNodeCount(includeSubTrees: false);
															if (num19 > 2)
															{
																Interaction.MsgBox("Interleave queries are only supported for Single SQLite or MS Jet Queries.", MsgBoxStyle.Critical, "Interleave Conditions are Invalid");
																text = "";
																break;
															}
														}
														else
														{
															flag = false;
														}
													}
													text43 = "NO";
													if (Operators.CompareString(text49, "Mongo", TextCompare: false) == 0)
													{
														text43 = Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOPOSTEXTPATTERN:" + MyAlias2)));
														if (Operators.CompareString(text43, "YES", TextCompare: false) == 0 || Operators.CompareString(text43, "DEFAULT", TextCompare: false) == 0)
														{
															left = "YES";
															MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Preprocess_QueryForm_for_MongoDB_Post_Extract(MyAlias2, ref fNoPostExtArr, ref fPostExtArr);
														}
													}
													Populate_Query_Arrays_v2(MyAlias2, flag, ll_ObjectType, text33, text49, text47);
													if (Operators.CompareString(text49, "Oracle", TextCompare: false) == 0)
													{
														int g_NoQColumns = Globals_Renamed.g_NoQColumns;
														for (num21 = 0; num21 <= g_NoQColumns; num21++)
														{
															if ((Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num21].DataType), "b", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num21].DataType), "r", TextCompare: false) == 0) && Operators.CompareString(Strings.Mid(Strings.LCase(Globals_Renamed.g_QColumns[num21].Show), 1, 1), "y", TextCompare: false) == 0)
															{
																text44 = "Y";
																break;
															}
														}
													}
													text23 = "";
													if (Operators.CompareString(text49, "SQLite", TextCompare: false) == 0 || Operators.CompareString(text49, "Mongo", TextCompare: false) == 0 || Operators.CompareString(text49, "Oracle", TextCompare: false) == 0)
													{
														if (Operators.CompareString(text49, "SQLite", TextCompare: false) == 0 && Operators.CompareString(text47, "AGG", TextCompare: false) != 0)
														{
															text30 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "PREPROCCSV:" + MyAlias2));
															text31 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "NOHDRS:" + MyAlias2));
															text32 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "CARTESIAN:" + MyAlias2));
														}
														text23 = Create_Header_Opt(text49);
													}
													text26 = "";
													text26 = g_AliasArray[num4].Prompt + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "PROMPT-TEXT:" + MyAlias2);
													text9 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "CTMISS:" + MyAlias2));
													if (Operators.CompareString(text9, "No", TextCompare: false) == 0)
													{
														text9 = "";
													}
													text5 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DISTINCT:" + MyAlias2));
													unchecked
													{
														num11 = (short)((ll_ObjectType != 2) ? (-1) : checked((short)Math.Round(Conversion.Val(Strings.Mid(MyAlias2, 2)))));
														text12 = "";
														text10 = "";
														text11 = "";
														text13 = "";
														text4 = "";
														Globals_Renamed.ODBC_Prompt = "";
														text7 = Set_TT_Name(MyAlias2, text49);
														if (Operators.CompareString(text47, "RAW", TextCompare: false) == 0)
														{
															text7 = Strings.Replace(text7, Globals_Renamed.gSPFCache + ".", "agg_" + Globals_Renamed.gSPFCache + ".", 1, -1, CompareMethod.Text);
														}
														text16 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "ROW:" + MyAlias2));
														text6 = ((Operators.CompareString(Strings.UCase(text16), "NO", TextCompare: false) != 0) ? Strings.Trim(text16) : "-1");
														if (Operators.CompareString(MyAlias, "", TextCompare: false) == 0 && !flag3 && (Operators.CompareString(text47, "", TextCompare: false) == 0 || Operators.CompareString(text47, "AGG", TextCompare: false) == 0))
														{
															if ((Operators.CompareString(text39, "-1", TextCompare: false) != 0) & (Operators.CompareString(text6, "-1", TextCompare: false) == 0))
															{
																text6 = text39;
															}
															else if (((Operators.CompareString(text39, "-1", TextCompare: false) != 0) & (Operators.CompareString(text6, "-1", TextCompare: false) != 0)) && (Versioned.IsNumeric(text39) & Versioned.IsNumeric(text6)) && Conversions.ToInteger(text39) > Conversions.ToInteger(text6))
															{
																text6 = text39;
															}
															if ((Operators.CompareString(text5, "No", TextCompare: false) == 0) & (Operators.CompareString(text40, "Yes", TextCompare: false) == 0))
															{
																text5 = "Yes";
															}
															text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "TERMINAL:All"));
															text7 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_odbc;
															Globals_Renamed.ODBC_Prompt = BuildForm.Replace_Globals(text7);
															if (Operators.CompareString(text49, "iMBigData", TextCompare: false) != 0 && Operators.CompareString(text49, "Mongo", TextCompare: false) != 0 && !LikeOperator.LikeString(Strings.UCase(text3), "*HBASE_MIDAS*", CompareMethod.Binary) && LikeOperator.LikeString(Strings.UCase(Globals_Renamed.ODBC_Prompt), "*.PARQUET*", CompareMethod.Binary))
															{
																Interaction.MsgBox("Parquet output is currently only supported for imBigData, MongoDB and MIDAS-HBASE View Output. Please change your Output File.", MsgBoxStyle.Exclamation, "Invalid Output File");
																text = "";
																break;
															}
														}
														else if (Operators.CompareString(MyAlias, "", TextCompare: false) != 0)
														{
															text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "TERMINAL:All"));
														}
														if (Operators.CompareString(text49, "Mongo", TextCompare: false) == 0 && Operators.CompareString(text5, "Yes", TextCompare: false) == 0)
														{
															Interaction.MsgBox("Mongo DB Queries do not yet support DISTINCT Queries. Please remove the Distinct clause", MsgBoxStyle.Exclamation, "Invalid Mongo Option");
															text = "";
															break;
														}
														if (Operators.CompareString(text49, "Mongo", TextCompare: false) == 0)
														{
															ll_M = Strings.UCase(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "MONGOWCOLS:" + MyAlias2)));
														}
														text21 = "";
														text15 = "";
														text2 = "";
													}
													if (!flag)
													{
														short num32 = (short)(g_NoAlias - 1);
														for (num5 = 0; num5 <= num32; num5 = (short)unchecked(num5 + 1))
														{
															text16 = text16 + "," + Strings.Trim(g_AliasArray[num5].Alias_Renamed);
														}
														text16 += ",";
														if (ShowUN == 0 && LikeOperator.LikeString(Strings.UCase(text3), "IMO_VF_DIVAS_*INSPECTIONS_AND_DEFECTS*", CompareMethod.Binary))
														{
															text2 = "/* SQL not Shown */";
														}
														else
														{
															if (LikeOperator.LikeString(Strings.UCase(text3), "IMO_VF_DIVAS_*INSPECTIONS_AND_DEFECTS*", CompareMethod.Binary))
															{
																text48 = "No";
															}
															if (Operators.CompareString(text47, "RAW", TextCompare: false) == 0)
															{
																text6 = Conversions.ToString(-1);
																text4 = "No";
																text5 = "No";
															}
															if (Operators.CompareString(text47, "AGG", TextCompare: false) == 0)
															{
																ll_JoinDuckDB = "Y";
															}
															text2 = Generate_SQL_2(ShowUN, ll_ObjectType, ll_DBType, MyAlias2, text49, text3, num11, text9, ref text12, ref text10, ref text11, text6, text5, ll_OutInline, ref text13, text16, IsPacked, text32, ll_M, MyPromptPrefix, text28, fNoPostExtArr, ref fPostExtArr, ll_JoinDuckDB);
															if (Operators.CompareString(text47, "AGG", TextCompare: false) == 0)
															{
																ll_JoinDuckDB = "N";
															}
														}
														if (unchecked(!flag2 && !flag3) & (Operators.CompareString(Strings.Trim(text11), "", TextCompare: false) != 0))
														{
															text21 = ((Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0) ? Get_Sort(2, 1) : ((!MyProject.Forms.FrmMain.mnuPivotSQLite.Checked && !MyProject.Forms.FrmMain.mnuPivotSQLite.Checked) ? ((!MyProject.Forms.FrmMain.mnuPivotSQLite.Checked) ? Get_Sort(1, 1) : Get_Sort(2)) : Get_Sort(2, 1)));
														}
													}
													else
													{
														text21 = Get_Sort(1, 1);
													}
													if (!flag & (Operators.CompareString(text2, "", TextCompare: false) == 0))
													{
														text = "";
														goto IL_402f;
													}
													if (unchecked(ll_ObjectType == 1 || ll_ObjectType == 3))
													{
														if (Operators.CompareString(text47, "AGG", TextCompare: false) == 0)
														{
															text15 = Set_TT_Name(MyAlias2, text49);
															text15 = Strings.Replace(text15, Globals_Renamed.gSPFCache + ".", "agg_" + Globals_Renamed.gSPFCache + ".", 1, -1, CompareMethod.Text);
															text15 = "DUCKDB:" + text15 + ":t0";
														}
														else
														{
															short num33 = (short)(Globals_Renamed.MaxFromTables - 1);
															for (num5 = 0; num5 <= num33; num5 = (short)unchecked(num5 + 1))
															{
																text16 = Strings.Trim(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GetCurrTableName("", "", num5));
																num8 = Strings.InStrRev(text16, " ");
																if (num8 != 0)
																{
																	text18 = Strings.Mid(text16, num8 + 1);
																	text16 = Strings.Mid(text16, 1, num8 - 1);
																}
																else
																{
																	text18 = "a100";
																}
																if (Strings.InStrRev(text16, "\\") != 0 || LikeOperator.LikeString(text16, "*<<<*>>>*", CompareMethod.Binary))
																{
																	text16 = text16 + ":" + text18;
																}
																text15 = ((Operators.CompareString(text15, "", TextCompare: false) != 0) ? (text15 + "," + text16) : text16);
															}
														}
													}
													else
													{
														text15 = "";
													}
													if (Operators.CompareString(text7, "", TextCompare: false) != 0 && Operators.CompareString(text47, "RAW", TextCompare: false) != 0)
													{
														if (Operators.CompareString(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "STACK:All")), "Yes", TextCompare: false) == 0)
														{
															text16 = Strings.Trim(text7);
															text18 = ",";
														}
														else if (Operators.CompareString(text42, "Y", TextCompare: false) == 0)
														{
															num8 = Strings.InStrRev(text7, ".");
															text16 = Strings.Trim(Strings.Mid(text7, 1, num8 - 1));
															text16 = "DUCKDB:" + Strings.Trim(text7) + ":" + text16;
															text18 = "@@@@@";
														}
														else
														{
															text16 = Strings.Trim(text7);
															text18 = ",";
														}
														text20 = ((Operators.CompareString(text20, "", TextCompare: false) != 0) ? (text20 + text18 + text16) : text16);
													}
													text41 = "";
													if (Operators.CompareString(text33, "SN", TextCompare: false) == 0)
													{
														text41 = Create_Replace_Headers();
													}
													if (MyProject.Forms.FrmMain.mnuAutoGrid.Checked && (Operators.CompareString(text4, "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text4), "YES", TextCompare: false) == 0))
													{
														if (Operators.CompareString(text33, "SN", TextCompare: false) == 0 && num4 == g_NoAlias - 1)
														{
															text4 = "G";
														}
														else if (!flag3 && flag2 && num4 == g_NoAlias - 1)
														{
															text4 = "G";
														}
													}
													text14 = Set_SQL_Query_Options(ShowUN, ll_ObjectType, ll_DBType, MyAlias2, text49, text4, text7, -1, text12, text11, text10, text15, flag, text21, "", text23, text24, text13, text26, text3, text30, text48, text35, text28, text29, text41, text31, text44, "N", num13);
													text2 = Strings.Replace(text2, "<<<SPF-OUTPUTFILE>>>", text7, 1, -1, CompareMethod.Text);
													text2 = Strings.Replace(text2, "<<<SPF-OUTPUTNODE>>>", text46, 1, -1, CompareMethod.Text);
													if (LikeOperator.LikeString(Strings.UCase(text3), "MCS_PARAMETER_*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(text3), "CAPACITY_*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(text3), "CAPACITY2_*", CompareMethod.Binary))
													{
														if (Globals_Renamed.gATDMCS2 == -1)
														{
															if (Operators.CompareString(Globals_Renamed.gWinuser.ToLower(), "jmclarke", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.gWinuser.ToLower(), "sys_smatrpt", TextCompare: false) == 0)
															{
																Globals_Renamed.gATDMCS2 = 1;
															}
															else
															{
																bool MyBool = false;
																bool flag7 = false;
																flag7 = Validate_UN_Node("C", "Current ATCDS Node", "ATCAP_COMMON_MORGAMING_USER", ref MyBool, "");
																if (MyBool)
																{
																	Globals_Renamed.gATDMCS2 = 1;
																}
																else
																{
																	Globals_Renamed.gATDMCS2 = 0;
																}
															}
														}
														if ((LikeOperator.LikeString(Strings.UCase(text3), "MCS_PARAMETER_*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(text3), "CAPACITY_*", CompareMethod.Binary)) && Globals_Renamed.gATDMCS2 == 1)
														{
															text2 = Strings.Replace(text2, "a0.[Hide]=0", "1=1", 1, -1, CompareMethod.Text);
														}
														else if (LikeOperator.LikeString(Strings.UCase(text3), "CAPACITY2_*", CompareMethod.Binary) && Globals_Renamed.gATDMCS2 == 1)
														{
															text2 = Strings.Replace(text2, "z2.hide=0", "1=1", 1, -1, CompareMethod.Text);
															text2 = Strings.Replace(text2, "z3.hide=0", "1=1", 1, -1, CompareMethod.Text);
															text2 = Strings.Replace(text2, "Reporting.vwPhoenixScenarioinputDataNonMorGaming", "Reporting.vwPhoenixScenarioinputData", 1, -1, CompareMethod.Text);
														}
													}
													if (Operators.CompareString(text14, "$ERROR$", TextCompare: false) == 0)
													{
														text = "";
														break;
													}
												}
												goto IL_3f13;
											}
											goto IL_402f;
											IL_1396:
											text = "";
											break;
											IL_13b8:
											text2 = "";
											goto IL_401f;
											IL_401f:
											num4 = (short)unchecked(num4 + 1);
											continue;
											IL_3f13:
											if (Operators.CompareString(text14, "", TextCompare: false) != 0 || Operators.CompareString(text2, "", TextCompare: false) != 0)
											{
												if (Operators.CompareString(text, "", TextCompare: false) == 0)
												{
													text = Strings.Trim(text14) + Strings.Trim(text2);
													flag5 = false;
													if (Strings.InStr(Strings.UCase(text2), "/WRITE-FILE=Y") != 0)
													{
														flag5 = true;
													}
												}
												else
												{
													text = ((!flag5) ? (text + "\r\n\r\n<---- New Query ---->\r\n\r\n" + Strings.Trim(text14) + Strings.Trim(text2)) : (text + "\r\n<---- New Query ---->\r\n" + Strings.Trim(text14) + Strings.Trim(text2)));
													flag5 = false;
													if (Strings.InStr(Strings.UCase(text2), "/WRITE-FILE=Y") != 0)
													{
														flag5 = true;
													}
												}
											}
											goto IL_401f;
											IL_402f:
											if (Operators.CompareString(text2, "", TextCompare: false) == 0 && Operators.CompareString(MyPromptPrefix, "", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) == 0)
											{
												text = "$PPQERROR$OK";
											}
											break;
										}
									}
								}
							}
						}
						goto IL_4074;
					}
					case 17244:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
								Information.Err().Clear();
								MyProject.Forms.FrmMain.Gauge1.Value = 0;
								MyProject.Forms.FrmMain.Gauge1.Visible = false;
								Cursor.Current = Cursors.Default;
								MyProject.Forms.FrmMain.cmdIcon.Refresh();
								break;
							default:
								goto end_IL_0001;
							}
							break;
						}
						IL_4074:
						Array.Clear(Globals_Renamed.FromTables, 0, Globals_Renamed.FromTables.Length);
						Array.Clear(g_AliasArray, 0, g_AliasArray.Length);
						if (!Information.IsNothing(text) && Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(147)), "\"", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(148)), "\"", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(149)), ".", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(145)), "'", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(146)), "'", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, Conversions.ToString(Strings.Chr(133)), "...", 1, -1, CompareMethod.Text);
							text = Strings.Replace(text, "</Q/>", "'", 1, -1, CompareMethod.Text);
						}
						if (Information.IsNothing(text) || Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							text50 = "";
							break;
						}
						text50 = BuildForm.Replace_Globals(text.ToString());
						text50 = Strings.Replace(text50, "<;>DUCKDB:", "<;>", 1, -1, CompareMethod.Text);
						break;
					}
					if (Operators.CompareString(left, "YES", TextCompare: false) != 0)
					{
						break;
					}
					int num34 = fNoPostExtArr;
					for (int num21 = 0; num21 <= num34; num21++)
					{
						if (Operators.CompareString(fPostExtArr[num21].mode, "C", TextCompare: false) == 0 || Operators.CompareString(fPostExtArr[num21].mode, "L", TextCompare: false) == 0)
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[fPostExtArr[num21].row].Cells[2].Value = fPostExtArr[num21].show;
						}
					}
					Array.Clear(fPostExtArr, 0, fPostExtArr.Length);
					fNoPostExtArr = -1;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 17244;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text50;
	}

	public static string Tab_and_Data(ref string MyInput)
	{
		string text = "1";
		checked
		{
			try
			{
				int num = Strings.InStr(Strings.UCase(MyInput), ",SPF$TAB\r\n");
				if (num != 0)
				{
					MyInput = Strings.Mid(MyInput, 1, num - 1) + Strings.Mid(MyInput, num + 8);
					int num2 = Strings.InStrRev(MyInput, ",");
					if (num2 != 0)
					{
						text = Strings.Mid(MyInput, num2 + 1);
						text = Strings.Replace(text, "\"", "", 1, -1, CompareMethod.Text);
						text = Strings.Trim(Strings.Replace(text, "\r\n", "", 1, -1, CompareMethod.Text));
						MyInput = Strings.Mid(MyInput, 1, num2 - 1);
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error parsing utility. (" + ex2.Message + ":)." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Utility Error");
				ProjectData.ClearProjectError();
			}
			return text;
		}
	}

	public static string Load_Input_Arrays(string MyInput, ref string[] Hdr, ref string[] Val)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string expression = default(string);
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
				case 290:
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
							goto IL_0015;
						case 4:
							goto IL_0025;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_0044;
						case 7:
							goto IL_0056;
						case 8:
							goto IL_0064;
						case 9:
							goto IL_007c;
						case 10:
							goto IL_008f;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0064:
					num2 = 8;
					expression = Strings.Replace(expression, "\r\n", "", 1, -1, CompareMethod.Text);
					goto IL_007c;
					IL_007c:
					num2 = 9;
					Val = Strings.Split(expression, "\",\"");
					goto IL_008f;
					IL_0056:
					num2 = 7;
					expression = Strings.Mid(MyInput, checked(num5 + 2));
					goto IL_0064;
					IL_008f:
					num2 = 10;
					Val[0] = Strings.Replace(Val[0], "\"", "", 1, -1, CompareMethod.Text);
					break;
					IL_000b:
					num2 = 2;
					result = Tab_and_Data(ref MyInput);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					num5 = Strings.InStr(MyInput, "\r\n");
					goto IL_0025;
					IL_0025:
					num2 = 4;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_0035:
					num2 = 5;
					expression = Strings.Mid(MyInput, 1, checked(num5 - 1));
					goto IL_0044;
					IL_0044:
					num2 = 6;
					Hdr = Strings.Split(expression, ",");
					goto IL_0056;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Val[Information.UBound(Val)] = Strings.Replace(Val[Information.UBound(Val)], "\"", "", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 290;
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

	public static string Process_CSR_MMS(string MyInput, string MyMode)
	{
		string result = "";
		checked
		{
			if (Operators.CompareString(MyInput, "", TextCompare: false) != 0)
			{
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				int num = 0;
				string[] Val = null;
				string[] Hdr = null;
				int num2 = 0;
				string text5 = "";
				string text6 = "";
				string text7 = "";
				try
				{
					text2 = Globals_Renamed.MySchemaDir + "\\utilities_" + Strings.LCase(Strings.Trim(MyMode)) + ".txt";
					if (MyProject.Computer.FileSystem.FileExists(text2))
					{
						text = General_Procedures.OpenReadFileContents(text2);
						string text8 = Load_Input_Arrays(MyInput, ref Hdr, ref Val);
						int num3 = Information.UBound(Hdr);
						for (num = 0; num <= num3; num++)
						{
							num2 = Strings.InStr(Hdr[num], "-");
							if (num2 == 0)
							{
								continue;
							}
							switch (Strings.Trim(Strings.Mid(Strings.UCase(Hdr[num]), 1, num2 - 1)))
							{
							case "SPF$":
								text = Strings.Replace(text, "<<<" + Strings.Trim(Hdr[num]) + ">>>", Strings.Trim(Val[num]), 1, -1, CompareMethod.Text);
								break;
							case "SPF$COLS":
							{
								string text9 = Strings.Trim(Strings.Mid(Hdr[num], num2 + 1));
								if (Operators.CompareString(text9, "", TextCompare: false) != 0 && Operators.CompareString(Strings.Trim(Val[num]), "", TextCompare: false) != 0)
								{
									text3 = ((Operators.CompareString(Strings.UCase(Strings.Trim(Val[num])), "GETDATETIME()", TextCompare: false) == 0) ? (text3 + "\r\n,Datetime('now','localtime') AS [" + text9 + "]") : ((Operators.CompareString(Strings.Mid(Strings.Trim(Val[num]), 1, 1), "[", TextCompare: false) != 0) ? (text3 + "\r\n,'" + Strings.Replace(Strings.Trim(Val[num]), "'", "", 1, -1, CompareMethod.Text) + "' AS [" + text9 + "]") : (text3 + "\r\n," + Strings.Trim(Val[num]) + " AS [" + text9 + "]")));
									text4 = text4 + "," + text9;
								}
								break;
							}
							case "SPF$FILTCOL":
								text5 = Strings.Trim(Val[num]);
								break;
							case "SPF$FILTOPR":
								text6 = Strings.Trim(Val[num]);
								break;
							case "SPF$FILTVAL":
								text7 = Strings.Trim(Val[num]);
								break;
							}
						}
						if (Operators.CompareString(Strings.Mid(text3 + "   ", 1, 3), "\r\n,", TextCompare: false) == 0)
						{
							text3 = "\r\n " + Strings.Mid(text3, 4);
						}
						if (Operators.CompareString(Strings.Mid(text4 + " ", 1, 1), ",", TextCompare: false) == 0)
						{
							text4 = Strings.Mid(text4, 2);
						}
						text5 = ((Operators.CompareString(text5, "", TextCompare: false) == 0 || Operators.CompareString(text6, "", TextCompare: false) == 0 || Operators.CompareString(text7, "", TextCompare: false) == 0) ? "" : ("AND " + text5 + " " + text6 + " '" + text7 + "'"));
						text = Strings.Replace(text, "<<<SPF$FILT>>>", text5, 1, -1, CompareMethod.Text);
						text = Strings.Replace(text, "<<<SPF$COLS>>>", text3, 1, -1, CompareMethod.Text);
						text = Strings.Replace(text, "<<<SPF$HDRS>>>", text4, 1, -1, CompareMethod.Text);
						text = Strings.Replace(text, "<<<SPF-INSTANCE>>>", Globals_Renamed.gSPFCache, 1, -1, CompareMethod.Text);
						result = text;
					}
					else
					{
						Interaction.MsgBox(MyMode + " utility error. " + text2 + " not Found). Contact support.", MsgBoxStyle.Exclamation, "Utility Error");
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox(MyMode + " utility error. (" + ex2.Message + ":)." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Utility Error");
					ProjectData.ClearProjectError();
				}
				finally
				{
					Hdr = null;
					Val = null;
				}
			}
			return result;
		}
	}

	public static string StripODBC(string MyNode, string MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		string text2 = default(string);
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
				case 402:
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
							goto IL_0022;
						case 7:
							goto IL_0043;
						case 9:
							goto IL_004f;
						case 10:
							goto IL_0071;
						case 12:
							goto IL_007e;
						case 13:
							goto IL_00a0;
						case 8:
						case 11:
						case 14:
						case 15:
							goto IL_00ab;
						case 16:
							goto IL_00c8;
						case 17:
							goto IL_00d6;
						case 18:
							goto IL_00ef;
						case 19:
							goto IL_0100;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 21:
						case 22:
						case 23:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d6:
					num2 = 17;
					text = Strings.Trim(Strings.Mid(text, checked(num5 + Strings.Len(text2))));
					goto IL_00ef;
					IL_00ef:
					num2 = 18;
					num6 = Strings.InStr(text, "@");
					goto IL_0100;
					IL_00c8:
					num2 = 16;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00d6;
					IL_0100:
					num2 = 19;
					if (num6 <= 1)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					text = MyNode;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num6 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 5;
					text2 = "@HADOOPIMPALAODBC@";
					goto IL_0022;
					IL_0022:
					num2 = 6;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyMode)), "S", TextCompare: false) == 0)
					{
						goto IL_0043;
					}
					goto IL_004f;
					IL_0043:
					num2 = 7;
					text2 = "@SAPHANAODBC@";
					goto IL_00ab;
					IL_004f:
					num2 = 9;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyMode)), "D", TextCompare: false) == 0)
					{
						goto IL_0071;
					}
					goto IL_007e;
					IL_0071:
					num2 = 10;
					text2 = "@DENODOODBC@";
					goto IL_00ab;
					IL_007e:
					num2 = 12;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyMode)), "N", TextCompare: false) == 0)
					{
						goto IL_00a0;
					}
					goto IL_00ab;
					IL_00a0:
					num2 = 13;
					text2 = "@SNOWFLAKEODBC@";
					goto IL_00ab;
					IL_00ab:
					num2 = 15;
					num5 = Strings.InStr(Strings.UCase(text + " "), text2);
					goto IL_00c8;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				text = Strings.Mid(text, 1, checked(num6 - 1));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 402;
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

	public static string Set_SQL_Query_Options(short ShowUN, short ll_ObjectType, string ll_DBType, string ll_MajorAlias, string ll_DatabaseType, string ll_Out_TT, string ll_out_CSV, int ll_Out_Rows, string ll_CTROW_s, string ll_CTVAL_S, string ll_CTHdr_S, string ll_Tables, bool ll_UnionSort, string ll_SortCols, string ll_DeleteTables, string ll_Headers, string ll_Utilities, string ll_Stack, string myPromptTxt, string ll_DisplayName, string PreProcCSV, string ll_ShowSQL, string ll_SQLiteDT, string ll_QUOTECSV, string ll_EmptyNull, string ll_ReplaceHeaders, string ll_NoHdrs, string L_LobData, string ll_JoinDuckDB, int ll_Parallel)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		int num2 = default(int);
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
						errsource = "BuildSQL - Set_SQL_Query_Options";
						ProjectData.ClearProjectError();
						num2 = 2;
						string text = "";
						string OUser = "";
						string OPwd = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						result = "";
						if (!(ll_UnionSort | (Operators.CompareString(ll_Utilities, "", TextCompare: false) != 0)))
						{
							if (ll_ObjectType == 1 && (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
							{
								text2 = text2 + "/NODE=" + ll_Tables;
								ll_Tables = "";
								text2 += "\r\n/OLEDB=DUCKDB";
								text2 += "\r\n/ENGINE=DUCKDB";
								text2 += "\r\n/UN=";
								text2 += "\r\n/PW=";
							}
							else
							{
								switch (ll_ObjectType)
								{
								case 1:
									text2 += "/NODE=.\\";
									text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SQLite";
									text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=SQLite";
									text2 = text2 + Globals_Renamed.CRLF + "/UN=";
									text2 = text2 + Globals_Renamed.CRLF + "/PW=";
									break;
								case 3:
									text2 += "/NODE=.\\";
									text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=TEXT";
									text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
									text2 = text2 + Globals_Renamed.CRLF + "/UN=";
									text2 = text2 + Globals_Renamed.CRLF + "/PW=";
									break;
								default:
									{
										if ((Operators.CompareString(ll_DBType, "0", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "1", TextCompare: false) == 0))
										{
											text = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
											General_Procedures.GetUNPWList(ShowUN, text, ref OUser, ref OPwd);
										}
										else if (Operators.CompareString(ll_DBType, "2", TextCompare: false) == 0)
										{
											text = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
											General_Procedures.GetUNPWList(ShowUN, text, ref OUser, ref OPwd);
										}
										else if (Operators.CompareString(ll_DBType, "3", TextCompare: false) == 0)
										{
											text = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
											General_Procedures.GetUNPWList(ShowUN, text, ref OUser, ref OPwd);
										}
										else if ((Operators.CompareString(ll_DBType, "5", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0))
										{
											text = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
											General_Procedures.GetUNPWList(ShowUN, text, ref OUser, ref OPwd);
										}
										else if (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0)
										{
											text = General_Procedures.GetNodeToUse("MIDAS", Globals_Renamed.MyOtherServer1);
										}
										else
										{
											text = "";
											OUser = "";
											OPwd = "";
										}
										text6 = General_Procedures.GetNodeKind(text);
										if (Operators.CompareString(text6, "", TextCompare: false) == 0)
										{
											if (!Check_UN_Missing(text, OUser, ShowUN))
											{
												switch (ll_DatabaseType)
												{
												default:
													if (Operators.CompareString(ll_DatabaseType, "Snowflake", TextCompare: false) != 0)
													{
														break;
													}
													goto case "SQLServer";
												case "SQLServer":
												case "Teradata":
												case "IBI-DaaS":
												case "MySQL":
												case "Hadoop":
												case "SQLite":
												case "Mongo":
												case "SAPHana":
												case "Postgres":
												case "Denodo":
												{
													text = General_Procedures.NodeCheck_s(text, "S", ShowUN);
													if (ShowUN != 0)
													{
														break;
													}
													for (short num3 = (short)Strings.InStr(Strings.UCase(text), "@UN&"); num3 != 0; num3 = (short)Strings.InStr(Strings.UCase(text), "@UN&"))
													{
														short num4 = (short)Strings.InStr(Strings.UCase(text), "&@PW&");
														if (num4 == 0)
														{
															text = Strings.Mid(text, 1, num3 - 1);
														}
														else
														{
															short num5 = (short)Strings.InStr(Strings.Mid(text, num4 + 5), "&");
															text = Strings.Mid(text, 1, num3 - 1) + Strings.Mid(text, num4 + 5 + num5);
														}
													}
													break;
												}
												}
												goto IL_0521;
											}
											result = "$ERROR$";
										}
										else
										{
											if (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) != 0)
											{
												goto IL_0521;
											}
											Interaction.MsgBox("Database Node: " + text + " can only be accessed using the Python Extract Engine. Switch to the Python Extract Engine to continue.", MsgBoxStyle.Exclamation, "Query Error");
											result = "$ERROR$";
										}
										goto end_IL_0001;
									}
									IL_0521:
									text2 = ((Operators.CompareString(ll_DatabaseType, "Hadoop", TextCompare: false) == 0 && (Operators.CompareString(text6, "HADOOP-IMPALA", TextCompare: false) == 0 || LikeOperator.LikeString(Strings.UCase(text), "*@HADOOPIMPALAODBC@*", CompareMethod.Binary))) ? (text2 + "/NODE=" + StripODBC(text, "H")) : ((Operators.CompareString(ll_DatabaseType, "SAPHana", TextCompare: false) == 0 && (Operators.CompareString(text6, "SAPHANA", TextCompare: false) == 0 || LikeOperator.LikeString(Strings.UCase(text), "*@SAPHANAODBC@*", CompareMethod.Binary))) ? (text2 + "/NODE=" + StripODBC(text, "S")) : ((Operators.CompareString(ll_DatabaseType, "Denodo", TextCompare: false) == 0 && (Operators.CompareString(text6, "DENODO", TextCompare: false) == 0 || LikeOperator.LikeString(Strings.UCase(text), "*@DENODOODBC@*", CompareMethod.Binary))) ? (text2 + "/NODE=" + StripODBC(text, "D")) : ((Operators.CompareString(ll_DatabaseType, "Snowflake", TextCompare: false) == 0 && (Operators.CompareString(text6, "SNOWFLAKE", TextCompare: false) == 0 || LikeOperator.LikeString(Strings.UCase(text), "*@SNOWFLAKEODBC@*", CompareMethod.Binary))) ? (text2 + "/NODE=" + StripODBC(text, "N")) : ((Operators.CompareString(text6, "ORACLE-UBER", TextCompare: false) != 0 && Strings.InStr(Strings.UCase(text), "(UBER)") == 0) ? (text2 + "/NODE=" + text) : (text2 + "/NODE= " + Strings.Replace(text, "(UBER)", "", 1, -1, CompareMethod.Text)))))));
									text2 = text2 + Globals_Renamed.CRLF + "/UN=" + OUser;
									text2 = text2 + Globals_Renamed.CRLF + "/PW=" + OPwd;
									if (LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "TEST_OR_SORT_UNIT_OR_DIE_TEST_RESULTS_HBASE_MIDAS*", CompareMethod.Binary) || Operators.CompareString(Strings.UCase(ll_DatabaseType), "IMBIGDATA", TextCompare: false) == 0)
									{
										text3 = "PYSCRIPTDRIVER";
										text5 = Strings.Trim(Strings.Mid(ll_MajorAlias, 2));
										text4 = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "ASYNCAPI:a" + text5));
										if ((Operators.CompareString(Strings.UCase(text4), "YES", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(text4), "Y", TextCompare: false) == 0) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "IMBIGDATA_SORT_ANCILLARY*", CompareMethod.Binary))
										{
											text3 = "PYSCRIPTDRIVERIMPORT";
										}
										text2 = text2 + "\r\n/OLEDB=" + text3 + "\r\n/ENGINE=VA";
										break;
									}
									switch (ll_DatabaseType)
									{
									case "DUCKDB":
										text2 += "\r\n/OLEDB=DUCKDB\r\n/ENGINE=DUCKDB";
										break;
									case "SQLServer":
										text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SQLSERVER";
										text2 = ((Strings.InStr(Strings.UCase(text), "@UBER@") == 0 && Operators.CompareString(text6, "SQLSERVER-UBER", TextCompare: false) != 0) ? (text2 + Globals_Renamed.CRLF + "/ENGINE=VA (.NET)") : (text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type()));
										break;
									case "Teradata":
										if (LikeOperator.LikeString(Strings.UCase(text), "*MIDAS*", CompareMethod.Binary) || Operators.CompareString(text6, "TERADATA-MIDAS", TextCompare: false) == 0)
										{
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=TERADATA";
											text2 = text2 + Globals_Renamed.CRLF + BuildForm.GetMidasEngine();
											break;
										}
										goto default;
									default:
										if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && (IsXEUS_DIS(text) || IsUBER_MAO(text) || LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*XEUS*", CompareMethod.Binary)))
										{
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Oracle";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type();
											if (Operators.CompareString(Globals_Renamed.gOrclRetry, "Y", TextCompare: false) == 0)
											{
												text2 = text2 + Globals_Renamed.CRLF + "/CONNECTRETRY=Y";
											}
											break;
										}
										if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && (LikeOperator.LikeString(text, "*(UBER)*", CompareMethod.Binary) || Operators.CompareString(text6, "ORACLE-UBER", TextCompare: false) == 0))
										{
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Oracle";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type();
											if (Operators.CompareString(Globals_Renamed.gOrclRetry, "Y", TextCompare: false) == 0)
											{
												text2 = text2 + Globals_Renamed.CRLF + "/CONNECTRETRY=Y";
											}
											break;
										}
										if (Operators.CompareString(ll_DatabaseType, "Teradata", TextCompare: false) == 0 && (LikeOperator.LikeString(text, "*(.NET)*", CompareMethod.Binary) || Operators.CompareString(text6, "TERADATA-.NET", TextCompare: false) == 0))
										{
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=TERADATA";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA (.NET)";
											break;
										}
										switch (ll_DatabaseType)
										{
										case "Teradata":
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=TERADATA";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type();
											break;
										case "Mongo":
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Mongo";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
											break;
										case "IBI-DaaS":
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=IBI-DaaS";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type();
											break;
										case "MySQL":
											text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=MYSQL";
											text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
											break;
										case "SAPHana":
											if (LikeOperator.LikeString(Strings.UCase(text), "*@SAPHANAODBC@*", CompareMethod.Binary) || Operators.CompareString(text6, "SAPHANA", TextCompare: false) == 0)
											{
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SAPHANAODBC";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
												break;
											}
											goto default;
										default:
											if (Operators.CompareString(ll_DatabaseType, "Denodo", TextCompare: false) == 0 && (LikeOperator.LikeString(Strings.UCase(text), "*@DENODOODBC@*", CompareMethod.Binary) || Operators.CompareString(text6, "DENODO", TextCompare: false) == 0))
											{
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=DENODOODBC";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
												break;
											}
											if (Operators.CompareString(ll_DatabaseType, "Snowflake", TextCompare: false) == 0 && (LikeOperator.LikeString(Strings.UCase(text), "*@SNOWFLAKEODBC@*", CompareMethod.Binary) || Operators.CompareString(text6, "SNOWFLAKE", TextCompare: false) == 0))
											{
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SNOWFLAKEODBC";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
												break;
											}
											if (Operators.CompareString(ll_DatabaseType, "Hadoop", TextCompare: false) == 0 && (LikeOperator.LikeString(Strings.UCase(text), "*@HADOOPIMPALAODBC@*", CompareMethod.Binary) || Operators.CompareString(text6, "HADOOP-IMPALA", TextCompare: false) == 0))
											{
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=HADOOPIMPALAODBC";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
												break;
											}
											switch (ll_DatabaseType)
											{
											case "Hadoop":
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Hadoop";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=Hadoop";
												break;
											case "SQLite":
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SQLite";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=SQLite";
												break;
											case "Postgres":
												if (Operators.CompareString(text6, "POSTGRES-UBER", TextCompare: false) == 0)
												{
													text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Postgres";
													text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=" + Get_Uber_Type();
													break;
												}
												goto default;
											default:
												if (Operators.CompareString(ll_DatabaseType, "Postgres", TextCompare: false) == 0)
												{
													text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=Postgres";
													text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
													break;
												}
												text2 = text2 + Globals_Renamed.CRLF + "/OLEDB=SQLPlus";
												text2 = text2 + Globals_Renamed.CRLF + "/ENGINE=VA";
												if (Operators.CompareString(Globals_Renamed.gOrclRetry, "Y", TextCompare: false) == 0)
												{
													text2 = text2 + Globals_Renamed.CRLF + "/CONNECTRETRY=Y";
												}
												break;
											}
											break;
										}
										break;
									}
									break;
								}
							}
						}
						text2 = ((!(ll_UnionSort | (Operators.CompareString(ll_Utilities, "", TextCompare: false) != 0))) ? (text2 + Globals_Renamed.CRLF + "/WORKDIR=.\\") : (text2 + "/WORKDIR=.\\"));
						if (Operators.CompareString(ll_Utilities, "", TextCompare: false) != 0)
						{
							if (LikeOperator.LikeString(ll_Utilities, "/*", CompareMethod.Binary))
							{
								text2 = text2 + Globals_Renamed.CRLF + ll_Utilities;
							}
							else
							{
								text2 = text2 + Globals_Renamed.CRLF + "/INSTANCE=" + Globals_Renamed.gSPFCache;
								text2 = text2 + Globals_Renamed.CRLF + "/OUTLOOK=<<<spf-email-type>>>";
								text2 = text2 + Globals_Renamed.CRLF + "/UTILITIES=" + ll_Utilities;
								if (Operators.CompareString(Strings.Trim(myPromptTxt), "", TextCompare: false) != 0)
								{
									text2 = text2 + Globals_Renamed.CRLF + "/PROMPT-TEXT=" + myPromptTxt;
								}
								if (ll_Parallel > 1)
								{
									text2 = text2 + "\r\n/PARALLEL=" + Strings.Trim(Conversion.Str(ll_Parallel));
								}
							}
						}
						else
						{
							text2 = text2 + Globals_Renamed.CRLF + "/T=" + ll_Out_TT;
							if (unchecked(ll_Out_Rows != -1 && ll_Out_Rows != -99))
							{
								text2 = text2 + Globals_Renamed.CRLF + "/ROWS=" + Conversions.ToString(ll_Out_Rows);
							}
							if ((ShowUN == 1) & !Globals_Renamed.g_IsSHGUI)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/TS=" + Globals_Renamed.g_TS;
							}
							switch (Strings.UCase(ll_out_CSV))
							{
							case "PROMPT":
								text3 = "&PROMPT&(EXCE)&PROMPT&";
								break;
							case null:
							case "":
								text3 = "";
								break;
							default:
								text3 = ll_out_CSV;
								if (((ShowUN == 1) & !Globals_Renamed.g_IsSHGUI) && Strings.InStr(text3, "{TS}") != 0)
								{
									text3 = Strings.Replace(text3, "{TS}", Globals_Renamed.g_TS, 1, -1, CompareMethod.Text);
								}
								break;
							}
							text2 = text2 + Globals_Renamed.CRLF + "/CSV=" + text3;
							if (Operators.CompareString(Strings.Trim(ll_CTVAL_S), "", TextCompare: false) != 0)
							{
								if (Strings.InStr(Strings.LCase(ll_CTROW_s), "<~sqo~>") != 0)
								{
									ll_CTROW_s = BuildForm.Repl_Squiggly(ll_CTROW_s, 1);
								}
								if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0)
								{
									text2 = text2 + Globals_Renamed.CRLF + "/CTROW=" + Strings.Replace(ll_CTROW_s, "Column-Pattern->[[M<;>", "Column-Pattern->[[B<;>", 1, -1, CompareMethod.Text);
								}
								else
								{
									if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										ll_CTROW_s = Strings.Replace(ll_CTROW_s, "Column-Pattern->[[M<;>", "Column-Pattern->[[B<;>", 1, -1, CompareMethod.Text);
									}
									text2 = text2 + Globals_Renamed.CRLF + "/CTROW=" + Strings.Replace(ll_CTROW_s, "Column-Pattern->[[N<;>", "Column-Pattern->[[B<;>", 1, -1, CompareMethod.Text);
								}
								text2 = text2 + Globals_Renamed.CRLF + "/CTVALUE=" + ll_CTVAL_S;
								text2 = text2 + Globals_Renamed.CRLF + "/CTHEADER=" + ll_CTHdr_S;
								text2 = text2 + Globals_Renamed.CRLF + "/CTARRAY=" + ll_MajorAlias + "," + Globals_Renamed.gSPFCache;
							}
							if (Operators.CompareString(Strings.Trim(ll_Stack), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/STACK=" + ll_Stack;
							}
							if (Operators.CompareString(Strings.Trim(ll_Tables), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/TABLE=" + ll_Tables;
							}
							if (Operators.CompareString(Strings.Trim(ll_SortCols), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/SORT=" + ll_SortCols;
							}
							if (Operators.CompareString(Strings.Trim(ll_Headers), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/HEADERS=" + ll_Headers;
							}
							if (Operators.CompareString(Strings.Trim(ll_ReplaceHeaders), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/REPLACEHDRS=" + ll_ReplaceHeaders;
							}
							if (Operators.CompareString(Strings.Trim(ll_DeleteTables), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/DELETE=" + ll_DeleteTables;
							}
							text2 = text2 + Globals_Renamed.CRLF + "/INSTANCE=" + Globals_Renamed.gSPFCache;
							if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0 && MyProject.Forms.FrmMain.mnuLegacyPivotHdrs.Checked)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/USE_LEGACY_PIVOT_HEADERS=Y";
							}
							if (Operators.CompareString(Strings.Trim(myPromptTxt), "", TextCompare: false) != 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/PROMPT-TEXT=" + myPromptTxt;
							}
							ll_DisplayName = Strings.Trim(ll_DisplayName);
							if (Operators.CompareString(ll_DisplayName, "", TextCompare: false) != 0)
							{
								short num3;
								if (LikeOperator.LikeString(ll_DisplayName.ToUpper(), "*SITE SPECIFIC]*", CompareMethod.Binary))
								{
									num3 = (short)Strings.InStrRev(ll_DisplayName, "\\");
									if (num3 != 0)
									{
										ll_DisplayName = Strings.Mid(ll_DisplayName, num3 + 1);
									}
								}
								num3 = (short)Strings.InStr(ll_DisplayName, " ");
								if (num3 != 0)
								{
									ll_DisplayName = Strings.Mid(ll_DisplayName, 1, num3 - 1);
								}
								text7 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_ViewDefVersion(ll_MajorAlias);
								text7 = Strings.Trim(text7);
								if (Operators.CompareString(text7, "", TextCompare: false) != 0)
								{
									ll_DisplayName = ll_DisplayName + "@" + text7;
								}
								text2 = text2 + Globals_Renamed.CRLF + "/RECORD=" + ll_DisplayName;
							}
							if (Operators.CompareString(ll_MajorAlias, "SQL", TextCompare: false) == 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/HEADERS_UNIQUE=Y";
							}
							if (Operators.CompareString(Strings.UCase(Strings.Trim(PreProcCSV)), "YES", TextCompare: false) == 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/PREPROCESS_CSV=Y";
							}
							if (Operators.CompareString(Strings.UCase(Strings.Trim(ll_NoHdrs)), "YES", TextCompare: false) == 0)
							{
								text2 = text2 + Globals_Renamed.CRLF + "/NOHEADERS=Y";
							}
							if (Operators.CompareString(Strings.UCase(Strings.Trim(ll_ShowSQL)), "YES", TextCompare: false) == 0)
							{
								text2 += "\r\n/SHOWSQL=Y";
							}
							if (ll_Parallel > 1)
							{
								text2 = text2 + "\r\n/PARALLEL=" + Strings.Trim(Conversion.Str(ll_Parallel));
							}
							if ((ll_ObjectType == 1) & (Operators.CompareString(Strings.Trim(ll_SQLiteDT), "", TextCompare: false) != 0))
							{
								text2 = text2 + "\r\n/SQLITE_DT=" + ll_SQLiteDT;
							}
							if ((Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0 && ll_ObjectType == 1) || (Operators.CompareString(BuildForm.FNUsePyEngine(), "N", TextCompare: false) == 0 && ll_ObjectType == 1 && Operators.CompareString(Strings.UCase(Strings.Trim(ll_QUOTECSV)), "YES", TextCompare: false) == 0))
							{
								text2 += "\r\n/QUOTECSV=Y";
							}
							if ((ll_ObjectType == 1) & (Operators.CompareString(Strings.UCase(Strings.Trim(ll_EmptyNull)), "YES", TextCompare: false) == 0))
							{
								text2 += "\r\n/EMPTY_TO_NULL=Y";
							}
							if (Operators.CompareString(L_LobData, "Y", TextCompare: false) == 0)
							{
								text2 += "\r\n/LOBDATA=Y";
							}
							if (MyProject.Forms.FrmMain.mnuPivotSQLite.Checked)
							{
								text2 += "\r\n/PIVOT-MODE=S";
							}
							else if (MyProject.Forms.FrmMain.mnuPivotPython.Checked)
							{
								text2 += "\r\n/PIVOT-MODE=P";
							}
							text2 = text2 + "\r\n/HADOOP_SERVER_DEFAULT=" + General_Procedures.NodeCheck_s(Globals_Renamed.gHadoopServer, "S", ShowUN);
						}
						if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) != 0)
						{
							text3 = ((Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) != 0) ? "" : (Globals_Renamed.CRLF + "/RESET=Y"));
							text2 = "<OPTIONS>" + text3 + Globals_Renamed.CRLF + text2 + Globals_Renamed.CRLF + "</OPTIONS>" + Globals_Renamed.CRLF + Globals_Renamed.CRLF;
						}
						result = Strings.Trim(text2);
						goto end_IL_0001;
					}
					case 6223:
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
					goto IL_1885;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 6223;
				continue;
			}
			break;
			IL_1885:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string GetLvlNo(string MyLvl)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		short num5 = default(short);
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
				case 110:
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
							goto IL_0025;
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
					result = "0";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = checked((short)Strings.InStrRev(MyLvl, ","));
					goto IL_0025;
					IL_0025:
					num2 = 4;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = Strings.Trim(Strings.Mid(MyLvl, checked(num5 + 1)));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 110;
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

	public static string GetMaxLvl()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		short num5 = default(short);
		short g_NoQColumns = default(short);
		short g_NoQFilters = default(short);
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
					case 397:
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
								goto IL_0021;
							case 5:
								goto IL_002a;
							case 6:
								goto IL_005d;
							case 7:
								goto IL_0077;
							case 8:
								goto IL_008b;
							case 9:
							case 10:
								goto IL_0092;
							case 11:
								goto IL_00a5;
							case 12:
								goto IL_00b4;
							case 13:
								goto IL_00e8;
							case 14:
								goto IL_0103;
							case 15:
								goto IL_0118;
							case 16:
							case 17:
								goto IL_0120;
							default:
								goto end_IL_0001;
							case 18:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0077:
						num2 = 7;
						if (Operators.CompareString(text, text2, TextCompare: false) > 0)
						{
							goto IL_008b;
						}
						goto IL_0092;
						IL_008b:
						num2 = 8;
						text2 = text;
						goto IL_0092;
						IL_005d:
						num2 = 6;
						text = GetLvlNo(Globals_Renamed.g_QColumns[num5].level);
						goto IL_0077;
						IL_0092:
						num2 = 10;
						num5 = (short)unchecked(num5 + 1);
						goto IL_009c;
						IL_000b:
						num2 = 2;
						text2 = "0";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						g_NoQColumns = Globals_Renamed.g_NoQColumns;
						num5 = 0;
						goto IL_009c;
						IL_009c:
						if (num5 <= g_NoQColumns)
						{
							goto IL_0021;
						}
						goto IL_00a5;
						IL_00a5:
						num2 = 11;
						g_NoQFilters = Globals_Renamed.g_NoQFilters;
						num5 = 0;
						goto IL_012a;
						IL_012a:
						if (num5 > g_NoQFilters)
						{
							goto end_IL_0001_2;
						}
						goto IL_00b4;
						IL_00b4:
						num2 = 12;
						if (Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QFilters[num5].And_Renamed), 1, 2), "--", TextCompare: false) != 0)
						{
							goto IL_00e8;
						}
						goto IL_0120;
						IL_00e8:
						num2 = 13;
						text = GetLvlNo(Globals_Renamed.g_QFilters[num5].level);
						goto IL_0103;
						IL_0103:
						num2 = 14;
						if (Operators.CompareString(text, text2, TextCompare: false) > 0)
						{
							goto IL_0118;
						}
						goto IL_0120;
						IL_0118:
						num2 = 15;
						text2 = text;
						goto IL_0120;
						IL_0120:
						num2 = 17;
						num5 = (short)unchecked(num5 + 1);
						goto IL_012a;
						IL_0021:
						num2 = 4;
						text = "0";
						goto IL_002a;
						IL_002a:
						num2 = 5;
						if (Operators.CompareString(Strings.UCase(Strings.Mid(Globals_Renamed.g_QColumns[num5].Show, 1, 1)), "Y", TextCompare: false) == 0)
						{
							goto IL_005d;
						}
						goto IL_0092;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 397;
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
		return text2;
	}

	public static string Round_SQLite(string MyShow, string MyCol, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 349:
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
							goto IL_0022;
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
					if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0022;
					IL_0022:
					num2 = 3;
					result = "Round(" + MyCol + "::DOUBLE," + Strings.Mid(MyShow, checked((int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(MyShow, ":")))) + 1.0))) + ")";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = checked("CASE WHEN " + Strings.Mid(MyShow, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(MyShow, ":")))) + 1.0)) + "=0 THEN CAST(ROUND(" + MyCol + ",0) AS Integer) ELSE Round(" + MyCol + "," + Strings.Mid(MyShow, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(MyShow, ":")))) + 1.0))) + ") END";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 349;
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

	public static string Generate_Select(string IsSummaryLevel, bool IsSQLOuter, bool IsSQLInner, ref string MyHints, string CBBegin, string CBEnd, short QueryType, short gILTop, short SpecialTable, short ll_ObjectType, string ll_DBType, string ll_MajorAlias, string ll_DatabaseType, short ll_TableIndex, string ll_Row_Limit, string ll_Distinct_Rows, bool ll_OutInline, string ll_QuoteCSV, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		string result;
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text7;
				bool flag;
				string text10;
				string text;
				short num3;
				short g_NoQColumns;
				switch (try0001_dispatch)
				{
				default:
				{
					result = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					text = "";
					num3 = 0;
					short num4 = 0;
					string text2 = "";
					string text3 = "";
					string text4 = "";
					string text5 = "";
					string text6 = "";
					text7 = "";
					string text8 = "";
					flag = false;
					int num5 = 0;
					bool flag2 = false;
					string text9 = "";
					text10 = "\"";
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						text10 = "";
					}
					text = text + CBBegin + "SELECT ";
					if (General_Procedures.SummPlusLevel(IsSummaryLevel) > 0)
					{
						flag = true;
					}
					if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
					{
						if (QueryType == 1)
						{
							text9 = Conversions.ToString((int)gILTop);
						}
						else
						{
							text4 = Strings.UCase(IsSummaryLevel);
							if (LikeOperator.LikeString(text4, "S+*", CompareMethod.Binary))
							{
								text5 = Strings.Mid(text4, 3);
								text9 = ((!Versioned.IsNumeric(text5)) ? "10" : Conversions.ToString(checked(10 + Conversions.ToInteger(text5))));
							}
							else
							{
								text9 = "10";
							}
						}
						text = text + "/*L" + text9 + "*/ ";
					}
					MyHints = "";
					if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "Hadoop", TextCompare: false) == 0)
					{
						MyHints = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Hints(ll_MajorAlias, ll_TableIndex, ll_ObjectType, ll_DatabaseType);
						if (QueryType != 2 && gILTop == 0 && IsSQLInner)
						{
							text += MyHints;
						}
					}
					if (Operators.CompareString(ll_Distinct_Rows, "Yes", TextCompare: false) == 0 && IsSQLOuter)
					{
						text += " DISTINCT ";
					}
					if (ll_ObjectType != 3)
					{
						switch (ll_DatabaseType)
						{
						case "SQLServer":
						case "IBI-DaaS":
						case "Teradata":
							break;
						default:
							goto IL_0241;
						}
					}
					if (Operators.CompareString(ll_Row_Limit, "-1", TextCompare: false) != 0 && IsSQLOuter)
					{
						text = text + " TOP " + ll_Row_Limit + " ";
					}
					goto IL_0241;
				}
				case 7974:
					{
						num = -1;
						switch (num2)
						{
						case 2:
							result = "ERROR:The following error occurred while generating the SQL Select Clause: " + Information.Err().Description;
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
					IL_0241:
					text += CBEnd;
					num3 = 1;
					g_NoQColumns = Globals_Renamed.g_NoQColumns;
					for (short num4 = 0; num4 <= g_NoQColumns; num4 = checked((short)unchecked(num4 + 1)))
					{
						string text3 = Globals_Renamed.g_QColumns[num4].Show;
						string text8 = Globals_Renamed.g_QColumns[num4].List;
						string text2 = Strings.UCase(Strings.Trim(Globals_Renamed.g_QColumns[num4].DataType));
						string text6 = ((!flag) ? Strings.Trim(Globals_Renamed.g_QColumns[num4].Statistics) : "None");
						int num5 = General_Procedures.SummPlusLevel(text8);
						bool flag2 = General_Procedures.ChkSummPlus(text8, "gt", IsSummaryLevel);
						if (!IsSQLOuter & (Operators.CompareString(text3, "N:S", TextCompare: false) == 0))
						{
							text3 = "Y";
						}
						if (!(((Operators.CompareString(Strings.Mid(text3, 1, 1), "Y", TextCompare: false) == 0 && num5 == 0) || (flag && num5 != 0 && !flag2 && !IsSQLOuter)) | ((flag && IsSQLOuter) & (Operators.CompareString(Strings.Mid(text3, 1, 1), "Y", TextCompare: false) == 0)) | (((Operators.CompareString(Strings.Mid(text3, 1, 1), "N", TextCompare: false) == 0 && num5 == 0) & (Operators.CompareString(Strings.Trim(Globals_Renamed.g_QColumns[num4].Statistics), "None", TextCompare: false) != 0)) && !IsSQLOuter)))
						{
							continue;
						}
						string text4 = Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num4].List), 1, 1);
						string text5;
						if (flag)
						{
							if (General_Procedures.ChkSummPlus(text8, "eq", IsSummaryLevel))
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Column);
								text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
							}
							else
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
								if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
								{
									text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num4].Column), 1);
								}
								text5 = text4;
							}
							if (BuildForm.IsColPattern(text4))
							{
								if (Operators.CompareString(Strings.Trim(Globals_Renamed.g_QColumns[num4].Statistics), "Expr", TextCompare: false) == 0 || !General_Procedures.ChkSummPlus(text8, "eq", IsSummaryLevel))
								{
									text4 = ChkHdrCol_Pattern(text4, 0);
								}
								text5 = "";
							}
						}
						else if ((QueryType == 2 || QueryType == 3 || gILTop > 0) && Operators.CompareString(text6, "Expr", TextCompare: false) != 0)
						{
							if ((gILTop > 0) & (Operators.CompareString(Strings.UCase(text8), "@IF@" + Strings.Trim(Conversions.ToString((int)gILTop)), TextCompare: false) == 0))
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Column);
								text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
							}
							else
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
								if (Operators.CompareString(text4, "", TextCompare: false) == 0)
								{
									text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Column);
									if (Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) != 0)
									{
										text4 = General_Procedures.Strip_Column(Strings.Trim(text4), 1);
									}
								}
								text5 = ((Operators.CompareString(Strings.Right(text4, 2), ".*", TextCompare: false) == 0) ? "" : text4);
							}
						}
						else if (SpecialTable == 1)
						{
							if (Operators.CompareString(text6, "Expr", TextCompare: false) == 0)
							{
								text4 = Globals_Renamed.g_QColumns[num4].Column;
								text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
							}
							else
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
								if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
								{
									text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num4].Column), 1);
								}
								text5 = text4;
							}
						}
						else
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Column);
							text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
						}
						if (Operators.CompareString(text6, "None", TextCompare: false) != 0)
						{
							if (Operators.CompareString(text5, "", TextCompare: false) == 0)
							{
								text5 = text6 + "Of" + General_Procedures.Strip_Column(text4, 0);
							}
							if (Operators.CompareString(text6, "Expr", TextCompare: false) != 0)
							{
								if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && Strings.InStr(text4, "CrossTab->[") != 0)
								{
									text4 = ((!LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary)) ? Strings.Replace(text4, ";:", ";" + text6 + "(|<>|):", 1, -1, CompareMethod.Text) : Strings.Replace(text4, ";:", ";" + Round_SQLite(text3, text6 + "(|<>|)", ll_JoinDuckDB) + ":", 1, -1, CompareMethod.Text));
									if (LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary))
									{
										text3 = "Y";
									}
								}
								else if (BuildForm.IsColPattern(text4))
								{
									if (!(LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary) && IsSQLOuter))
									{
										text4 = ((Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0 || (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) != 0 && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) != 0)) ? Strings.Replace(text4, "|<>|", text6 + "(|<>|)", 1, -1, CompareMethod.Text) : ((Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0) ? Strings.Replace(text4, "|<>|", text6 + "(NULLIF(|<>|,''))", 1, -1, CompareMethod.Text) : Strings.Replace(text4, "|<>|", text6 + "(|<>|)", 1, -1, CompareMethod.Text)));
									}
									else if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
									{
										text4 = Strings.Replace(text4, "|<>|", Round_SQLite(text3, text6 + "( NULLIF(|<>|,''))", ll_JoinDuckDB), 1, -1, CompareMethod.Text);
									}
									else if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										text4 = Strings.Replace(text4, "|<>|", Round_SQLite(text3, text6 + "( |<>| )", ll_JoinDuckDB), 1, -1, CompareMethod.Text);
									}
									if (LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary))
									{
										text3 = "Y";
									}
								}
								else
								{
									if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										switch (Strings.UCase(text6))
										{
										case "FIRST_VALUE":
											text6 = "First";
											break;
										case "LAST_VALUE":
											text6 = "Last";
											break;
										case "STDDEVP":
											text6 = "STDDev_Pop";
											break;
										case "STDEV":
											text6 = "STDDev";
											break;
										case "VAR":
										case "VARIANCE":
											text6 = "Var_Samp";
											break;
										case "SKEW":
											text6 = "Skewness";
											break;
										}
									}
									if (LikeOperator.LikeString(Strings.UCase(text6), "P*", CompareMethod.Binary) && ll_ObjectType != 1)
									{
										switch (ll_DatabaseType)
										{
										case "Oracle":
										case "SAPHana":
										case "Postgres":
											goto IL_0b7d;
										}
										if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
										{
											goto IL_0b7d;
										}
									}
									if (LikeOperator.LikeString(Strings.UCase(text6), "P*", CompareMethod.Binary) && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										text7 = ((Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0 || Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0) ? "" : "::DOUBLE");
										text4 = "Percentile_Cont(." + Strings.Mid(text6, 2, 2) + ")  Within Group (Order By " + text4 + text7 + ")";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "SUBSTR_MAX_15", TextCompare: false) == 0 && (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "Substr(MAX(" + text4 + "),15)";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCAT", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "'" + text10 + "' || Group_Concat(" + text4 + ") || '" + text10 + "'";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATC", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "'" + text10 + "' || Group_Concat(" + text4 + ") || '," + text10 + "'";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATSB", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "Group_Concat(" + text4 + ",\"; \")";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATSB DISTINCT", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "Replace(Group_Concat(Distinct " + text4 + "),',','; ')";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATCB", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "'" + text10 + "' || Group_Concat(" + text4 + ",\", \") || '" + text10 + "'";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCAT DISTINCT", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "Group_Concat(Distinct " + text4 + ")";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATQ DISTINCT", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "'" + text10 + "' ||Group_Concat(Distinct " + text4 + ")|| '" + text10 + "'";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "GROUP_CONCATCB DISTINCT", TextCompare: false) == 0 && (ll_ObjectType == 1 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0))
									{
										text4 = "'" + text10 + "' || LTrim(Group_Concat(Distinct \" \" || " + text4 + ")) || '" + text10 + "'";
									}
									else if (Operators.CompareString(Strings.UCase(text6), "COUNT DISTINCT", TextCompare: false) == 0)
									{
										text4 = ((Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0 || (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) != 0 && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) != 0 && ll_ObjectType != 1)) ? ("Count (Distinct " + text4 + ")") : ((Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0) ? ("Count (Distinct NULLIF(" + text4 + ",''))") : ("Count (Distinct " + text4 + ")")));
									}
									else if (Operators.CompareString(Strings.UCase(text6), "COUNT(*)", TextCompare: false) == 0)
									{
										text4 = "Count(*)";
									}
									else if ((Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 || ll_ObjectType == 1)) || (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 && ll_ObjectType == 1))
									{
										if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) == 0 || (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 && ll_ObjectType == 1))
										{
											if (Operators.CompareString(Strings.UCase(text2), "F", TextCompare: false) == 0)
											{
												text7 = "::DOUBLE";
											}
											else if (Operators.CompareString(Strings.UCase(text2), "N", TextCompare: false) == 0)
											{
												text7 = "::BIGINT";
											}
											text4 = Strings.Trim(text6) + " (" + text4 + text7 + ")";
										}
										else
										{
											text4 = Strings.Trim(text6) + " (NULLIF(" + text4 + ",''))";
										}
									}
									else
									{
										text4 = Strings.Trim(text6) + "(" + text4 + ")";
									}
								}
							}
							else if (Operators.CompareString(text6, "Expr", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && Strings.InStr(text4, "CrossTab->[") != 0)
							{
								text4 = ((!LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary)) ? Strings.Replace(text4, ";:", ";" + Strings.Trim(Globals_Renamed.g_QColumns[num4].AllStat) + ":", 1, -1, CompareMethod.Text) : Strings.Replace(text4, ";:", ";" + Round_SQLite(text3, Strings.Trim(Globals_Renamed.g_QColumns[num4].AllStat), ll_JoinDuckDB) + ":", 1, -1, CompareMethod.Text));
								if (LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary))
								{
									text3 = "Y";
								}
								text4 = Strings.Replace(text4, "[MyPivotedColumn(s)]", "|<>|", 1, -1, CompareMethod.Text);
							}
						}
						else if (BuildForm.IsColPattern(text4) && Operators.CompareString(text6, "None", TextCompare: false) == 0)
						{
							if (IsSQLInner && IsSQLOuter)
							{
								if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0) && Strings.InStr(text4, "@MONGO-KS1@:") != 0)
								{
									text4 = ChkHdrCol_Pattern(text4, 0);
								}
							}
							else
							{
								text4 = ChkHdrCol_Pattern(text4, 0);
							}
							text5 = "";
							if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary))
							{
								text3 = "Y";
							}
						}
						else if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && (Strings.InStr(text4, "CrossTab->[") != 0 || BuildForm.IsColPattern(text4)) && LikeOperator.LikeString(text3, "Y:*", CompareMethod.Binary))
						{
							text3 = "Y";
						}
						goto IL_14ca;
						IL_14ca:
						if (IsSQLOuter)
						{
							if (((Operators.CompareString(text2, "F", TextCompare: false) == 0) | (Operators.CompareString(text2, "N", TextCompare: false) == 0)) & (Strings.InStr(text3, "Y:") != 0))
							{
								text4 = ((Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0) ? ((!BuildForm.IsColPattern(text4) || Strings.InStr(text4, "<;>|<>|<;>") == 0) ? Round_SQLite(text3, text4, ll_JoinDuckDB) : Strings.Replace(text4, "|<>|", Round_SQLite(text3, "|<>|", ll_JoinDuckDB), 1, -1, CompareMethod.Text)) : checked((Operators.CompareString(ll_DatabaseType, "SQLServer", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "IBI-DaaS", TextCompare: false) == 0) ? ("CAST(Str(" + text4 + ",20," + Strings.Mid(text3, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(text3, ":")))) + 1.0)) + ") AS Float)") : (ll_DatabaseType switch
								{
									"Teradata" => "CAST(" + text4 + " AS decimal(25," + Strings.Mid(text3, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(text3, ":")))) + 1.0)) + "))", 
									"Oracle" => "Round(Cast(" + text4 + " as Decimal(30,14))," + Strings.Mid(text3, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(text3, ":")))) + 1.0)) + ")", 
									"DUCKDB" => Round_SQLite(text3, text4, ll_JoinDuckDB), 
									_ => "Round(" + text4 + "," + Strings.Mid(text3, (int)Math.Round(Conversions.ToDouble(Strings.Trim(Conversions.ToString(Strings.InStr(text3, ":")))) + 1.0)) + ")", 
								})));
							}
							if (!ll_OutInline)
							{
								if (Operators.CompareString(text2, "X", TextCompare: false) == 0)
								{
									if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "Postgres", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										text4 = "Replace(Replace(Replace(Replace(Replace(Replace(" + text4 + ",',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ')";
									}
									else
									{
										switch (ll_DatabaseType)
										{
										case "SQLite":
											text4 = "Replace(Replace(Replace(Replace(Replace(Replace(" + text4 + ",',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ')";
											break;
										default:
											if (Operators.CompareString(ll_DatabaseType, "Snowflake", TextCompare: false) != 0)
											{
												if (Operators.CompareString(ll_DatabaseType, "Teradata", TextCompare: false) == 0)
												{
													text4 = "OReplace(OReplace(OReplace(OReplace(OReplace(OReplace(" + text4 + "||'',',',';'),'09'XC,' '),'0A'XC,' '),'0D'XC,' '),'22'XC,''''),'07'XC,' ')";
												}
												break;
											}
											goto case "SQLServer";
										case "SQLServer":
										case "SAPHana":
										case "Denodo":
											text4 = "Replace(Replace(Replace(Replace(Replace(Replace(" + text4 + ",',',';'),char(9),' '),char(10),' '),char(13),' '),char(34),''''),char(7),' ')";
											break;
										}
									}
								}
								if (Operators.CompareString(Strings.UCase(text3), "Y:STRIP", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 && (Operators.CompareString(text2, "C", TextCompare: false) == 0 || Operators.CompareString(text2, "X", TextCompare: false) == 0))
								{
									text4 = "SPF_FN$StripTags(" + text4 + ")";
								}
								if (Operators.CompareString(text2, "D", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(text3), "Y:*", CompareMethod.Binary))
								{
									switch (Strings.UCase(text3))
									{
									case "Y:GMTPT":
										text4 = "CAST((FROM_TZ(CAST(" + text4 + " AS TIMESTAMP),'+00:00') AT TIME ZONE 'America/Los_Angeles') AS DATE)";
										break;
									case "Y:GMTMA":
										text4 = "CAST((FROM_TZ(CAST(" + text4 + " AS TIMESTAMP),'+00:00') AT TIME ZONE 'Asia/Hong_Kong') AS DATE)";
										break;
									case "Y:UTCPT":
										text4 = "TIMEZONE(" + text4 + ", 'UTC', 'America/Los_Angeles')";
										break;
									}
								}
								if (Operators.CompareString(text2, "D", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && !LikeOperator.LikeString(Strings.UCase(text6), "COUNT*", CompareMethod.Binary) && Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
								{
									text4 = ((Operators.CompareString(Strings.UCase(text3), "Y:MS", TextCompare: false) != 0) ? ("To_Char(" + text4 + ",'yyyy-mm-dd hh24:mi:ss')") : ("To_Char(" + text4 + ",'yyyy-mm-dd hh24:mi:ss.FF')"));
								}
								else if ((Operators.CompareString(text2, "T", TextCompare: false) == 0 || Operators.CompareString(text2, "G", TextCompare: false) == 0) && Operators.CompareString(ll_DatabaseType, "Teradata", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text6), "COUNT", TextCompare: false) != 0)
								{
									text4 = "CAST(" + text4 + " AS char(19))";
								}
								else if (Operators.CompareString(text2, "T", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "SQLServer", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text6), "COUNT", TextCompare: false) != 0)
								{
									text4 = "CONVERT(CHAR(10)," + text4 + ", 23)  + ' ' + CONVERT(CHAR(8)," + text4 + ", 8)";
								}
								else if (Operators.CompareString(text2, "D", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "TEXT", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(text6), "COUNT", TextCompare: false) != 0)
								{
									text4 = "Cstr(DatePart('YYYY'," + text4 + ")) + '-' + Right('0' + Cstr( DatePart('m'," + text4 + ")),2) + '-' + Right('0' + Cstr(DatePart('d'," + text4 + ")),2) + ' ' + Right('0' + Cstr(Hour(" + text4 + ")),2) + ':' + Right('0' + Cstr(DatePart('n'," + text4 + ")),2) + ':' + Right('0' + Cstr(DatePart('s'," + text4 + ")),2)";
								}
								else if (Operators.CompareString(text2, "V", TextCompare: false) == 0 && Operators.CompareString(ll_DatabaseType, "Postgres", TextCompare: false) == 0 && !LikeOperator.LikeString(Strings.UCase(text6), "COUNT*", CompareMethod.Binary))
								{
									text4 = "To_Char(" + text4 + ",'yyyy-mm-dd hh24:mi:ss')";
								}
							}
						}
						if (Operators.CompareString(text5, "", TextCompare: false) != 0 && Strings.InStr(text4, "CrossTab->[") == 0 && !BuildForm.IsColPattern(text4))
						{
							text4 = ((Strings.InStr(Strings.Trim(text5), " ") == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text5), 1, 1), "[", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text5), 1, 1), "`", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text5), 1, 1), "\"", TextCompare: false) == 0) ? (text4 + " AS " + text5) : (text4 + " AS \"" + text5 + "\""));
						}
						if ((!flag & (Strings.InStr(text4, "CrossTab->[") != 0)) && QueryType == 1 && gILTop == 0)
						{
							text4 = Strings.Replace(text4, ":N]", ":Y]", 1, -1, CompareMethod.Text);
						}
						if (!flag && BuildForm.IsColPattern(text4) && QueryType == 1 && gILTop == 0)
						{
							if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0)
							{
								text4 = Strings.Replace(text4, "[[N<;>", "[[J<;>", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "[[M<;>", "[[J<;>", 1, -1, CompareMethod.Text);
							}
							else
							{
								text4 = Strings.Replace(text4, "[[N<;>", "[[Y<;>", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "[[M<;>", "[[Y<;>", 1, -1, CompareMethod.Text);
							}
						}
						if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && BuildForm.IsColPattern(text4) && gILTop > 0)
						{
							text4 = Strings.Replace(text4, "[[Y<;>", "[[N<;>", 1, -1, CompareMethod.Text);
							text4 = Strings.Replace(text4, "[[J<;>", "[[N<;>", 1, -1, CompareMethod.Text);
						}
						if (num3 == 1)
						{
							text = text + Globals_Renamed.CRLF + CBBegin + "          " + text4 + CBEnd;
							num3 = 0;
						}
						else
						{
							text = text + Globals_Renamed.CRLF + CBBegin + "         ," + text4 + CBEnd;
						}
						continue;
						IL_0b7d:
						text4 = "Percentile_Cont(." + Strings.Mid(text6, 2, 2) + ")  Within Group (Order By " + text4 + ")";
						goto IL_14ca;
					}
					result = text;
					goto end_IL_0001;
				}
				goto IL_1f5c;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 7974;
				continue;
			}
			break;
			IL_1f5c:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Check_Default_Node(string ll_DBType, ref string MyNode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		string text = default(string);
		string left = default(string);
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
				case 946:
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
							goto IL_0025;
						case 6:
							goto IL_002e;
						case 7:
							goto IL_0056;
						case 8:
							goto IL_0069;
						case 10:
							goto IL_0078;
						case 11:
							goto IL_0090;
						case 12:
							goto IL_00a4;
						case 14:
							goto IL_00b1;
						case 15:
							goto IL_00c9;
						case 16:
							goto IL_00dd;
						case 18:
							goto IL_00eb;
						case 19:
							goto IL_00ff;
						case 9:
						case 13:
						case 17:
						case 20:
						case 21:
							goto IL_010a;
						case 22:
							goto IL_0122;
						case 23:
							goto IL_012c;
						case 24:
							goto IL_0139;
						case 25:
							goto IL_0182;
						case 27:
							goto IL_019e;
						case 28:
							goto IL_01bf;
						case 29:
							goto IL_01f4;
						case 31:
							goto IL_020f;
						case 32:
							goto IL_023a;
						case 34:
							goto IL_0249;
						case 35:
							goto IL_0274;
						case 37:
							goto IL_0280;
						case 38:
							goto IL_02ab;
						case 40:
							goto IL_02b7;
						case 41:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 26:
						case 30:
						case 33:
						case 36:
						case 39:
						case 42:
						case 43:
						case 44:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_02ab:
					num2 = 38;
					result = "You cannot query a Denodo Data Source using the Legacy Extract Engine. Please set your Extract Engine to Python";
					goto end_IL_0001_3;
					IL_023a:
					num2 = 32;
					result = "You cannot query a Hadoop-Impala-ODBC Data Source using the Legacy Extract Engine. Please set your Extract Engine to Python";
					goto end_IL_0001_3;
					IL_020f:
					num2 = 31;
					if (Strings.InStr(text, "@HADOOPIMPALAODBC@") != 0 && Operators.CompareString(left, "N", TextCompare: false) == 0)
					{
						goto IL_023a;
					}
					goto IL_0249;
					IL_0249:
					num2 = 34;
					if (Strings.InStr(text, "@SAPHANAODBC@") != 0 && Operators.CompareString(left, "N", TextCompare: false) == 0)
					{
						goto IL_0274;
					}
					goto IL_0280;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text2 = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					text = "";
					goto IL_0025;
					IL_0025:
					num2 = 5;
					left = BuildForm.FNUsePyEngine();
					goto IL_002e;
					IL_002e:
					num2 = 6;
					if (Operators.CompareString(ll_DBType, "0", TextCompare: false) == 0 || Operators.CompareString(ll_DBType, "1", TextCompare: false) == 0)
					{
						goto IL_0056;
					}
					goto IL_0078;
					IL_02b7:
					num2 = 40;
					if (Strings.InStr(text, "@SNOWFLAKEODBC@") != 0 && Operators.CompareString(left, "N", TextCompare: false) == 0)
					{
						break;
					}
					goto end_IL_0001_3;
					IL_0078:
					num2 = 10;
					if (Operators.CompareString(ll_DBType, "2", TextCompare: false) == 0)
					{
						goto IL_0090;
					}
					goto IL_00b1;
					IL_0090:
					num2 = 11;
					MyNode = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
					goto IL_00a4;
					IL_00a4:
					num2 = 12;
					text2 = " ARIES";
					goto IL_010a;
					IL_00b1:
					num2 = 14;
					if (Operators.CompareString(ll_DBType, "3", TextCompare: false) == 0)
					{
						goto IL_00c9;
					}
					goto IL_00eb;
					IL_00c9:
					num2 = 15;
					MyNode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
					goto IL_00dd;
					IL_00dd:
					num2 = 16;
					text2 = " OASYS";
					goto IL_010a;
					IL_00eb:
					num2 = 18;
					MyNode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
					goto IL_00ff;
					IL_00ff:
					num2 = 19;
					text2 = "";
					goto IL_010a;
					IL_0056:
					num2 = 7;
					MyNode = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
					goto IL_0069;
					IL_0069:
					num2 = 8;
					text2 = " MARS";
					goto IL_010a;
					IL_010a:
					num2 = 21;
					if (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0)
					{
						goto IL_0122;
					}
					goto IL_012c;
					IL_0122:
					num2 = 22;
					text2 = " MIDAS";
					goto IL_012c;
					IL_012c:
					num2 = 23;
					MyNode = BuildForm.Replace_Globals(MyNode);
					goto IL_0139;
					IL_0139:
					num2 = 24;
					if (Operators.CompareString(Strings.UCase(MyNode), "DEFAULT", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(MyNode), "NONE", TextCompare: false) == 0 || Operators.CompareString(Strings.Trim(MyNode), "", TextCompare: false) == 0)
					{
						goto IL_0182;
					}
					goto IL_019e;
					IL_0274:
					num2 = 35;
					result = "You cannot query a SAP Hana Data Source using the Legacy Extract Engine. Please set your Extract Engine to Python";
					goto end_IL_0001_3;
					IL_0280:
					num2 = 37;
					if (Strings.InStr(text, "@DENODOODBC@") != 0 && Operators.CompareString(left, "N", TextCompare: false) == 0)
					{
						goto IL_02ab;
					}
					goto IL_02b7;
					IL_019e:
					num2 = 27;
					text = Strings.Trim(Strings.UCase(General_Procedures.NodeCheck_s(MyNode, "S2", 0, "Y")));
					goto IL_01bf;
					IL_01bf:
					num2 = 28;
					if (Strings.InStr("," + text, ",DB:") != 0 && Operators.CompareString(left, "N", TextCompare: false) == 0)
					{
						goto IL_01f4;
					}
					goto IL_020f;
					IL_0182:
					num2 = 25;
					result = "You cannot run a" + text2 + " query without assigning a Default or explicit node. Please set a database node";
					goto end_IL_0001_3;
					IL_01f4:
					num2 = 29;
					result = "You cannot query Data Source: \"" + MyNode + "\" using the Legacy Extract Engine. Please set your Extract Engine to Python";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 41;
				result = "You cannot query a Snowflake Data Source using the Legacy Extract Engine. Please set your Extract Engine to Python";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 946;
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

	public static string Check_XEUS_View(string ll_DisplayName, string ll_DBType, string MyPromptPrefix)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		int num = default(int);
		int num3 = default(int);
		string MyNode = default(string);
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
					num2 = 1;
					text = "";
					goto IL_000a;
				case 769:
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
							goto IL_000a;
						case 3:
							goto IL_0021;
						case 4:
						case 5:
							goto IL_0032;
						case 6:
							goto IL_003b;
						case 7:
							goto IL_0044;
						case 8:
							goto IL_004f;
						case 9:
							goto IL_00a7;
						case 10:
							goto IL_00ad;
						case 11:
							goto IL_00b3;
						case 12:
							goto IL_00bf;
						case 14:
							goto IL_00f4;
						case 15:
							goto IL_0111;
						case 17:
							goto IL_011a;
						case 18:
							goto IL_0134;
						case 16:
						case 19:
						case 20:
							goto IL_013b;
						case 21:
							goto IL_0181;
						case 23:
							goto IL_018d;
						case 24:
							goto IL_01b5;
						case 26:
							goto IL_01be;
						case 27:
							goto IL_0206;
						case 22:
						case 25:
						case 28:
						case 29:
							goto IL_020d;
						case 30:
							goto IL_0223;
						case 32:
							goto IL_0235;
						case 33:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 31:
						case 34:
						case 35:
						case 36:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_018d:
					num2 = 23;
					if (LikeOperator.LikeString(MyNode, "*<<<*>>>*", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*MAP_NODES*", CompareMethod.Binary))
					{
						goto IL_01b5;
					}
					goto IL_01be;
					IL_0181:
					num2 = 21;
					num5 = 1;
					goto IL_020d;
					IL_01b5:
					num2 = 24;
					num5 = 2;
					goto IL_020d;
					IL_01be:
					num2 = 26;
					if (Operators.CompareString(ll_DBType, "2", TextCompare: false) == 0 && (LikeOperator.LikeString(MyNode, "*ARIES*", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*MAO*", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*MAP_NODES*", CompareMethod.Binary)))
					{
						goto IL_0206;
					}
					goto IL_020d;
					IL_000a:
					num2 = 2;
					if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) != 0)
					{
						goto IL_0021;
					}
					goto IL_0032;
					IL_0021:
					num2 = 3;
					MyPromptPrefix = ". Problem occurs in Pre/Post Query at " + MyPromptPrefix;
					goto IL_0032;
					IL_0032:
					num2 = 5;
					MyNode = "";
					goto IL_003b;
					IL_003b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0044;
					IL_0044:
					num2 = 7;
					text = Check_Default_Node(ll_DBType, ref MyNode);
					goto IL_004f;
					IL_004f:
					num2 = 8;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					switch (ll_DBType)
					{
					default:
						if (Operators.CompareString(ll_DBType, "3", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "0":
					case "1":
					case "2":
						break;
					}
					goto IL_00a7;
					IL_020d:
					num2 = 29;
					if (num6 >= 1 && num5 == 0)
					{
						goto IL_0223;
					}
					goto IL_0235;
					IL_00a7:
					num2 = 9;
					num6 = 0;
					goto IL_00ad;
					IL_00ad:
					num2 = 10;
					num5 = 0;
					goto IL_00b3;
					IL_00b3:
					num2 = 11;
					MyNode = Strings.UCase(MyNode);
					goto IL_00bf;
					IL_00bf:
					num2 = 12;
					if (LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*GENERALDATA_MARS_XEUS*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "DUAL *", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_00f4;
					IL_0235:
					num2 = 32;
					if (num6 == 0 && num5 == 1)
					{
						break;
					}
					goto end_IL_0001_3;
					IL_00f4:
					num2 = 14;
					if (Strings.InStr(Strings.UCase(ll_DisplayName), "XEUS") != 0)
					{
						goto IL_0111;
					}
					goto IL_011a;
					IL_0111:
					num2 = 15;
					num6 = 1;
					goto IL_013b;
					IL_011a:
					num2 = 17;
					if (Conversions.ToDouble(ll_DBType) == 2.0)
					{
						goto IL_0134;
					}
					goto IL_013b;
					IL_0134:
					num2 = 18;
					num6 = 2;
					goto IL_013b;
					IL_013b:
					num2 = 20;
					if (LikeOperator.LikeString(MyNode, "*_XEUS*", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*_DIS", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*_DIS_*", CompareMethod.Binary) || LikeOperator.LikeString(MyNode, "*_CAFE", CompareMethod.Binary))
					{
						goto IL_0181;
					}
					goto IL_018d;
					IL_0223:
					num2 = 30;
					text = "You are trying to use a non-XEUS database node with a XEUS view.  This Is Not allowed. Please change your database node" + MyPromptPrefix;
					goto end_IL_0001_3;
					IL_0206:
					num2 = 27;
					num5 = 2;
					goto IL_020d;
					end_IL_0001_2:
					break;
				}
				num2 = 33;
				text = "You are trying to use a XEUS database node with a non-XEUS view.  This Is Not allowed. Please change your database node" + MyPromptPrefix;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 769;
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

	public static string Check_Open_Close_Paren()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
		string text2 = default(string);
		string result = default(string);
		int num7 = default(int);
		int num8 = default(int);
		string[] array = default(string[]);
		int g_NoQFilters = default(int);
		int num9 = default(int);
		int g_NoQFilters2 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text3;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 981:
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
								goto IL_0022;
							case 7:
								goto IL_0027;
							case 8:
								goto IL_0030;
							case 9:
								goto IL_0035;
							case 10:
								goto IL_003f;
							case 11:
								goto IL_0049;
							case 12:
								goto IL_005b;
							case 13:
								goto IL_00d3;
							case 14:
								goto IL_00f3;
							case 15:
								goto IL_0118;
							case 16:
							case 17:
							case 18:
								goto IL_012a;
							case 19:
								goto IL_013c;
							case 20:
								goto IL_0158;
							case 21:
								goto IL_0165;
							case 22:
								goto IL_0178;
							case 23:
								goto IL_018d;
							case 24:
								goto IL_0193;
							case 25:
								goto IL_0199;
							case 26:
								goto IL_01a3;
							case 27:
								goto IL_01b5;
							case 28:
								goto IL_01f1;
							case 29:
								goto IL_0237;
							case 30:
								goto IL_025a;
							case 31:
								goto IL_02a0;
							case 32:
							case 33:
								goto IL_02c5;
							case 34:
								goto IL_02d7;
							case 35:
								goto IL_02e9;
							case 37:
							case 38:
								goto IL_0302;
							case 36:
							case 39:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 40:
							case 41:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_005b:
						num2 = 12;
						if ((LikeOperator.LikeString(Globals_Renamed.g_QFilters[num5].ParentO, "*(*", CompareMethod.Binary) || LikeOperator.LikeString(Globals_Renamed.g_QFilters[num5].ParenC, "*)*", CompareMethod.Binary)) && Operators.CompareString(Strings.Mid(Globals_Renamed.g_QFilters[num5].And_Renamed + "  ", 1, 2), "--", TextCompare: false) != 0)
						{
							goto IL_00d3;
						}
						goto IL_012a;
						IL_012a:
						num2 = 18;
						num5++;
						goto IL_0133;
						IL_02c5:
						num2 = 33;
						num6++;
						goto IL_02ce;
						IL_0118:
						num2 = 15;
						text += text2;
						goto IL_012a;
						IL_000b:
						num2 = 2;
						result = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num7 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						num8 = 0;
						goto IL_001d;
						IL_001d:
						num2 = 5;
						num5 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						num6 = 0;
						goto IL_0027;
						IL_0027:
						num2 = 7;
						text = "";
						goto IL_0030;
						IL_0030:
						num2 = 8;
						array = null;
						goto IL_0035;
						IL_0035:
						num2 = 9;
						text2 = "";
						goto IL_003f;
						IL_003f:
						num2 = 10;
						text3 = "";
						goto IL_0049;
						IL_0049:
						num2 = 11;
						g_NoQFilters = Globals_Renamed.g_NoQFilters;
						num5 = 0;
						goto IL_0133;
						IL_0133:
						if (num5 <= g_NoQFilters)
						{
							goto IL_005b;
						}
						goto IL_013c;
						IL_013c:
						num2 = 19;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0158;
						IL_0158:
						num2 = 20;
						text = Strings.Mid(text, 2);
						goto IL_0165;
						IL_0165:
						num2 = 21;
						array = Strings.Split(text, ";");
						goto IL_0178;
						IL_0178:
						num2 = 22;
						num9 = Information.UBound(array);
						num5 = 0;
						goto IL_030b;
						IL_030b:
						if (num5 > num9)
						{
							break;
						}
						goto IL_018d;
						IL_018d:
						num2 = 23;
						num7 = 0;
						goto IL_0193;
						IL_0193:
						num2 = 24;
						num8 = 0;
						goto IL_0199;
						IL_0199:
						num2 = 25;
						text2 = array[num5];
						goto IL_01a3;
						IL_01a3:
						num2 = 26;
						g_NoQFilters2 = Globals_Renamed.g_NoQFilters;
						num6 = 0;
						goto IL_02ce;
						IL_02ce:
						if (num6 <= g_NoQFilters2)
						{
							goto IL_01b5;
						}
						goto IL_02d7;
						IL_02d7:
						num2 = 34;
						if (num7 != num8)
						{
							goto IL_02e9;
						}
						goto IL_0302;
						IL_02e9:
						num2 = 35;
						result = "On the Filter tab, the count of open parentheses for uncommented filters at level " + text2 + " does not match the count of closed parentheses ";
						break;
						IL_0302:
						num2 = 38;
						num5++;
						goto IL_030b;
						IL_01b5:
						num2 = 27;
						if (Operators.CompareString(Strings.Mid(Globals_Renamed.g_QFilters[num6].And_Renamed + "  ", 1, 2), "--", TextCompare: false) != 0)
						{
							goto IL_01f1;
						}
						goto IL_02c5;
						IL_01f1:
						num2 = 28;
						if (LikeOperator.LikeString(Globals_Renamed.g_QFilters[num6].ParentO, "*(*", CompareMethod.Binary) && Operators.CompareString(text2, Globals_Renamed.g_QFilters[num6].level, TextCompare: false) == 0)
						{
							goto IL_0237;
						}
						goto IL_025a;
						IL_00d3:
						num2 = 13;
						text2 = ";" + Globals_Renamed.g_QFilters[num5].level;
						goto IL_00f3;
						IL_0237:
						num2 = 29;
						num7 += General_Procedures.Count_Character(Globals_Renamed.g_QFilters[num6].ParentO, "(");
						goto IL_025a;
						IL_025a:
						num2 = 30;
						if (LikeOperator.LikeString(Globals_Renamed.g_QFilters[num6].ParenC, "*)*", CompareMethod.Binary) && Operators.CompareString(text2, Globals_Renamed.g_QFilters[num6].level, TextCompare: false) == 0)
						{
							goto IL_02a0;
						}
						goto IL_02c5;
						IL_00f3:
						num2 = 14;
						if (!LikeOperator.LikeString(text, "*" + text2 + "*", CompareMethod.Binary))
						{
							goto IL_0118;
						}
						goto IL_012a;
						IL_02a0:
						num2 = 31;
						num8 += General_Procedures.Count_Character(Globals_Renamed.g_QFilters[num6].ParenC, ")");
						goto IL_02c5;
						end_IL_0001_2:
						break;
					}
					num2 = 39;
					array = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 981;
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

	public static string Check_ibd_field(string MyField, int MyMode = 0)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
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
				case 314:
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
							goto IL_0022;
						case 6:
							goto IL_002e;
						case 7:
							goto IL_003e;
						case 8:
							goto IL_004b;
						case 9:
						case 10:
						case 11:
							goto IL_005d;
						case 12:
							goto IL_0086;
						case 14:
							goto IL_0092;
						case 15:
							goto IL_00a7;
						case 17:
							goto IL_00b3;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
						case 16:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0086:
					num2 = 12;
					result = "IBD";
					goto end_IL_0001_3;
					IL_0092:
					num2 = 14;
					if (LikeOperator.LikeString(MyField, "<PARAMGROUP OPERATION=*", CompareMethod.Binary))
					{
						goto IL_00a7;
					}
					goto IL_00b3;
					IL_00b3:
					num2 = 17;
					if (Operators.CompareString(MyField, "</PARAMGROUP>", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_00a7:
					num2 = 15;
					result = "STARTIBD";
					goto end_IL_0001_3;
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
					MyField = Strings.UCase(MyField);
					goto IL_0022;
					IL_0022:
					num2 = 5;
					if (MyMode == 1)
					{
						goto IL_002e;
					}
					goto IL_005d;
					IL_002e:
					num2 = 6;
					num5 = Strings.InStr(MyField, ">");
					goto IL_003e;
					IL_003e:
					num2 = 7;
					if (num5 != 0)
					{
						goto IL_004b;
					}
					goto IL_005d;
					IL_004b:
					num2 = 8;
					MyField = Strings.Mid(MyField, checked(num5 + 1));
					goto IL_005d;
					IL_005d:
					num2 = 11;
					if (LikeOperator.LikeString(MyField, "<PARAM DOMAIN=*", CompareMethod.Binary) && Strings.InStr(MyField, "OPERATION=") == 0)
					{
						goto IL_0086;
					}
					goto IL_0092;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				result = "ENDIBD";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 314;
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

	public static string Generate_SQL_2(short ShowUN, short ll_ObjectType, string ll_DBType, string ll_MajorAlias, string ll_DatabaseType, string ll_DisplayName, short ll_TableIndex, string ll_CTMiss, ref string CTRow_s, ref string CTHeader_s, ref string CTValue_s, string ll_Row_Limit, string ll_Distinct_Rows, bool ll_OutInline, ref string MyStack, string ll_AliasStr, bool IsPacked, string ll_Cartesian, string ll_M5, string MyPromptPrefix, string ll_QuoteCSV, int fNoPostExtArr, ref Globals_Renamed.fPostExtArr_Type[] fPostExtArr, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string result = default(string);
		int num = default(int);
		string[] DynArray = default(string[]);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string MyiBDFunc;
					short num9;
					short myIdx;
					short num14;
					string ll_SPCMap;
					string ll_SPCMapHdr;
					string ll_SPCMap2;
					string ll_SPCMapHdr2;
					string[] gInLineFilter;
					string[] gInlineCol;
					short num17;
					short num16;
					string MyError;
					short NoWhere;
					string text24;
					string CBBegin;
					string CBEnd;
					string MyHints;
					Globals_Renamed.SQLiteAF_Type[] gSQliteAF;
					Globals_Renamed.SQLiteAF_Level_Type[] gSQLiteAF_Level;
					string text10;
					short num18;
					int num32;
					string text3;
					string left;
					string SQLStr;
					switch (try0001_dispatch)
					{
					default:
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						MyiBDFunc = "";
						text = "BuildSQL - Generate_SQL_2";
						text2 = ": dim variables";
						result = "";
						text3 = "";
						short num3 = 0;
						short num4 = 0;
						int num5 = 0;
						short num6 = 0;
						short num7 = 0;
						SQLStr = "";
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						text10 = "";
						string text11 = "";
						string text12 = "";
						short num8 = 0;
						num9 = 0;
						short num10 = 0;
						short num11 = 0;
						short num12 = 0;
						short num13 = 0;
						myIdx = 0;
						num14 = 0;
						ll_SPCMap = "''";
						ll_SPCMapHdr = "''";
						ll_SPCMap2 = "''";
						ll_SPCMapHdr2 = "''";
						string text13 = "";
						string text14 = "";
						string text15 = "";
						short num15 = 0;
						string text16 = "";
						num16 = 0;
						gInLineFilter = new string[11];
						gInlineCol = new string[11];
						string text17 = "";
						num17 = 0;
						bool flag = true;
						string text18 = "";
						string text19 = "";
						num16 = 0;
						num3 = 0;
						do
						{
							gInLineFilter[num3] = "";
							gInlineCol[num3] = "N";
							num3 = (short)unchecked(num3 + 1);
						}
						while (num3 <= 9);
						MyError = "";
						num18 = 0;
						int num19 = 0;
						int num20 = 0;
						int num21 = 0;
						int num22 = 0;
						int num23 = 0;
						int num24 = 0;
						int num25 = 0;
						string text20 = "";
						string text21 = "";
						string text22 = "";
						NoWhere = 0;
						string text23 = "";
						text24 = "";
						if (Operators.CompareString(MyPromptPrefix, "", TextCompare: false) != 0)
						{
							text24 = "There is a problem with the Pre/Post Query at " + MyPromptPrefix + ". ";
						}
						switch (ll_MajorAlias)
						{
						case "SQL":
							text5 = "for the SQLite Text File query. ";
							break;
						case "All->":
						case "All":
							text23 = ((Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0) ? "SQLite" : "DuckDB");
							text5 = "for the Final " + text23 + " query that joins all results. ";
							text23 = "";
							break;
						case "ILV":
							text5 = "for the Inline View query. ";
							break;
						case "TXT":
							text5 = "for the MS JET Text File query. ";
							break;
						default:
							text5 = "for View with alias " + ll_MajorAlias + ". ";
							break;
						}
						text24 = text24 + "Error occurred preparing SQL " + text5;
						string text25 = "";
						string text26 = "";
						string text27 = "";
						string text28 = "";
						string text29 = "";
						string text30 = "";
						string text31 = "";
						string text32 = "";
						left = "";
						string text33 = "";
						int num26 = -1;
						short num27 = 0;
						int num28 = 0;
						CBBegin = "";
						CBEnd = "";
						MyHints = "";
						string text34 = "";
						short num29 = 0;
						string text35 = "";
						string text36 = "";
						string text37 = "";
						string text38 = "";
						short num30 = 0;
						gSQliteAF = new Globals_Renamed.SQLiteAF_Type[101];
						gSQLiteAF_Level = new Globals_Renamed.SQLiteAF_Level_Type[22];
						int num31 = 0;
						BuildAF.Init_SQLite_AF_Variables(ref gSQliteAF, ref gSQLiteAF_Level);
						if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0)
						{
							text16 = "500";
						}
						else
						{
							text16 = "0";
						}
						text2 = ": Display the Gauge";
						MyProject.Forms.FrmMain.Gauge1.Value = 0;
						MyProject.Forms.FrmMain.Gauge1.Visible = true;
						MyProject.Forms.FrmMain.cmdIcon.Refresh();
						CTRow_s = "";
						CTValue_s = "";
						if (Globals_Renamed.MaxFromTables < 1)
						{
							MyError = "At least one table must be selected for access in order to generate a query.";
						}
						else if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0 && Operators.CompareString(Globals_Renamed.gUsePyEngine, "N", TextCompare: false) == 0)
						{
							MyError = "MongoDB queries are now only supported for the Python Extract Engine";
						}
						else
						{
							if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
							{
								BuildAF.Prepare_SQLite_SQL_for_AF_2(ref gSQliteAF, ref gSQLiteAF_Level, ref MyError);
							}
							if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
							{
								text23 = "";
								text25 = "";
								text26 = "";
								text27 = "";
								num28 = 0;
								text12 = "";
								MyError = Translate_CompExpr(ll_MajorAlias, ll_DatabaseType);
								if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
								{
									if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) != 0)
									{
										MyError = Verify_Temp_Table(ll_MajorAlias, ll_AliasStr);
									}
									if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
									{
										text2 = ": Set CBBegin and CBEnd Variables ";
										Set_Engine_Delimiters(ref CBBegin, ref CBEnd);
										if ((Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) != 0 || Operators.CompareString(ll_Cartesian, "Yes", TextCompare: false) != 0) && Globals_Renamed.MaxFromTables >= 2)
										{
											text23 = Check_Joins();
											if (Operators.CompareString(text23, "", TextCompare: false) != 0)
											{
												num7 = (short)Interaction.MsgBox(text24 + "You have designed a query involving multiple tables but specified no Join Condition for table with alias " + text23 + ". This means that a cartesian product will be used to combine all rows from table " + text23 + " with all rows from every other table. If this is what you intended, click YES to continue. Otherwise, Click NO and add a Join condition using the Joins Grid", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Query Involves a Cartesian Product. Do you want to continue?");
												if (num7 == 7)
												{
													MyError = "Exiting due to a missing Join condition";
													goto IL_35d5;
												}
											}
										}
										text2 = ": Check whether a Table Index/key has been selected";
										if (ll_ObjectType == 2 && Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0)
										{
											MyError = Check_XEUS_View(ll_DisplayName, ll_DBType, MyPromptPrefix);
											if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
											{
												goto IL_35d5;
											}
											if (!LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*_STATUS*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*_SCHEMA*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*_MASTER*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "DUAL*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*_NOKEY*", CompareMethod.Binary) && !LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "*_REFERENCE*", CompareMethod.Binary) && Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) != 0)
											{
												num6 = 0;
												short g_NoQFilters = Globals_Renamed.g_NoQFilters;
												for (num3 = 0; num3 <= g_NoQFilters; num3 = (short)unchecked(num3 + 1))
												{
													if ((Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QFilters[num3].And_Renamed), 1, 2), "--", TextCompare: false) != 0) & (Operators.CompareString(Globals_Renamed.g_QFilters[num3].Key, ".", TextCompare: false) == 0))
													{
														num6 = 1;
														break;
													}
												}
												if (num6 == 0)
												{
													num7 = (short)Interaction.MsgBox(text24 + General_Procedures.Get_UI("errkey"), MsgBoxStyle.YesNo | MsgBoxStyle.Information | MsgBoxStyle.DefaultButton2, "Keyed Column Not Yet Selected for Row Filter");
													if (num7 == 6)
													{
														Cursor.Current = Cursors.Default;
														BuildForm.Invoke_IE("IndexDB.htm");
													}
													goto IL_3d11;
												}
											}
										}
										text2 = ": Determine validity and characteristics of Query";
										text10 = "";
										short g_NoQColumns = Globals_Renamed.g_NoQColumns;
										for (num3 = 0; num3 <= g_NoQColumns; num3 = (short)unchecked(num3 + 1))
										{
											if (Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num3].List), "P_SPC$MAP$", TextCompare: false) == 0)
											{
												ll_SPCMap = Globals_Renamed.g_QColumns[num3].Column;
												ll_SPCMapHdr = Globals_Renamed.g_QColumns[num3].Header;
											}
											else if (Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num3].List), "F_WIP$MAP$", TextCompare: false) == 0)
											{
												ll_SPCMap2 = Globals_Renamed.g_QColumns[num3].Column;
												ll_SPCMapHdr2 = Globals_Renamed.g_QColumns[num3].Header;
											}
										}
										short g_NoQColumns2 = Globals_Renamed.g_NoQColumns;
										for (num3 = 0; num3 <= g_NoQColumns2; num3 = (short)unchecked(num3 + 1))
										{
											text4 = Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num3].Show), 1, 1);
											if (Operators.CompareString(text4, "Y", TextCompare: false) == 0)
											{
												num8 = 1;
											}
											if ((Operators.CompareString(Globals_Renamed.g_QColumns[num3].Sort, "None", TextCompare: false) != 0) & (Operators.CompareString(text4, "Y", TextCompare: false) == 0))
											{
												num10++;
											}
											text7 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Column);
											if ((Operators.CompareString(text4, "Y", TextCompare: false) == 0) & (Operators.CompareString(Strings.Right(Strings.Trim(text7), 2), ".*", TextCompare: false) == 0))
											{
												text10 = "*";
											}
											text5 = Globals_Renamed.g_QColumns[num3].Statistics;
											text11 = Globals_Renamed.g_QColumns[num3].List;
											if (LikeOperator.LikeString(Strings.UCase(text11), "@IF@*", CompareMethod.Binary))
											{
												if (Operators.CompareString(Strings.UCase(text11), "@IF@", TextCompare: false) == 0)
												{
													gInlineCol[1] = "Y";
													if (num16 == 0)
													{
														num16 = 1;
													}
													Globals_Renamed.g_QColumns[num3].List = Strings.LCase("@IF@") + "1";
												}
												else if (Operators.CompareString(Strings.UCase(text11), "@IF@", TextCompare: false) != 0)
												{
													text12 = Strings.Trim(Strings.Mid(text11, Strings.Len("@IF@") + 1));
													if (Versioned.IsNumeric(text12) && Conversions.ToShort(text12) <= 9)
													{
														gInlineCol[Conversions.ToShort(text12)] = "Y";
														if (Conversions.ToShort(text12) > num16)
														{
															num16 = Conversions.ToShort(text12);
														}
													}
												}
											}
											else if (General_Procedures.SummPlusLevel(text11) > 0)
											{
												num30 = General_Procedures.SummPlusLevel(text11);
												if (num30 > num17)
												{
													num17 = num30;
												}
											}
											text8 = Globals_Renamed.g_QColumns[num3].Header;
											if ((Operators.CompareString(text5, "None", TextCompare: false) != 0) & ((Operators.CompareString(text4, "Y", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num3].Show), "N:S", TextCompare: false) == 0)))
											{
												num9++;
											}
											text2 = ": Check For a Cross-Tab Query";
											text6 = Globals_Renamed.g_QColumns[num3].Pivot;
											if ((Operators.CompareString(text6, "Value", TextCompare: false) == 0) & (Operators.CompareString(text4, "Y", TextCompare: false) == 0))
											{
												num11++;
												text23 = ((num11 != 1) ? "," : "");
												CTValue_s = CTValue_s + text23 + BuildForm.Get_Col_Header(text7, text8, ll_ObjectType);
											}
											else if ((Operators.CompareString(text6, "Header", TextCompare: false) == 0) & (Operators.CompareString(text4, "Y", TextCompare: false) == 0))
											{
												num12++;
												num13 = num3;
												CTHeader_s = BuildForm.Get_Col_Header(text7, text8, ll_ObjectType);
											}
											else if ((Operators.CompareString(text6, "Row", TextCompare: false) == 0) & (Operators.CompareString(text4, "Y", TextCompare: false) == 0))
											{
												num15++;
												text23 = ((num15 != 1) ? "," : "");
												CTRow_s = CTRow_s + text23 + BuildForm.Get_Col_Header(text7, text8, ll_ObjectType);
											}
											else if ((Operators.CompareString(text6, "Stack", TextCompare: false) == 0) & (Operators.CompareString(text4, "Y", TextCompare: false) == 0))
											{
												num14++;
												text23 = ((num14 != 1) ? "," : "");
												MyStack = MyStack + text23 + BuildForm.Get_Col_Header(text7, text8, ll_ObjectType);
											}
											if ((Operators.CompareString(text6, "Value", TextCompare: false) == 0) & (Operators.CompareString(text5, "None", TextCompare: false) == 0))
											{
												MyError = "You have specified a Pivot query but the column to be transposed, (column " + text7 + " has a setting of 'Value' in the CrossTab column), must also be assigned a Summary Function such as SUM, COUNT, AVG, MAX, MIN, STDEV.";
												break;
											}
										}
										if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
										{
											bool flag2 = false;
											bool flag3 = false;
											if (Operators.CompareString(ll_DatabaseType, "iMBigData", TextCompare: false) == 0 && LikeOperator.LikeString(Strings.UCase(ll_DisplayName), "IMBIGDATA A*", CompareMethod.Binary))
											{
												short g_NoQColumns3 = Globals_Renamed.g_NoQColumns;
												for (num3 = 0; num3 <= g_NoQColumns3; num3 = (short)unchecked(num3 + 1))
												{
													text4 = Strings.Mid(Strings.Trim(Globals_Renamed.g_QColumns[num3].Show), 1, 1);
													if (Operators.CompareString(text4, "Y", TextCompare: false) == 0)
													{
														text6 = Globals_Renamed.g_QColumns[num3].Column;
														if (Operators.CompareString(Check_ibd_field(text6), "STARTIBD", TextCompare: false) == 0)
														{
															flag2 = true;
														}
														else if (Operators.CompareString(Check_ibd_field(text6), "ENDIBD", TextCompare: false) == 0)
														{
															if (!flag2)
															{
																MyError = "An \"end-imbigdata-operation\" tag was found in the Columns Grid without an associated \"*start-imbigdata-operation..*\" (i.e., \"START->[]\") tag";
																break;
															}
															flag2 = false;
														}
														else if (unchecked(Operators.CompareString(Check_ibd_field(text6), "IBD", TextCompare: false) == 0 && !flag2))
														{
															MyError = "You must bound imBigData fields with \"*start-imbigdata-operation..*\" (i.e., \"START->[]\") and *end-imbigdata-operation* tags";
															break;
														}
													}
												}
												if (unchecked(flag2 && !flag3))
												{
													MyError = "You must add an imBigData \"*end-imbigdata-operation*\" tag For every \"*start-imbigdata-operation..*\" (i.e., \"START->[]\") tag";
												}
											}
											if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
											{
												short g_NoQFilters2 = Globals_Renamed.g_NoQFilters;
												for (num3 = 0; num3 <= g_NoQFilters2; num3 = (short)unchecked(num3 + 1))
												{
													if (Operators.CompareString(Strings.Mid(Strings.Trim(Globals_Renamed.g_QFilters[num3].And_Renamed), 1, 2), "--", TextCompare: false) != 0)
													{
														num30 = General_Procedures.SummPlusLevel(Globals_Renamed.g_QFilters[num3].List);
														if (num30 > num17)
														{
															num17 = num30;
														}
													}
												}
												num18 = 0;
												if (num8 == 0)
												{
													MyError = "At least one column must be selected For display. Either you have Not selected any columns In the Query Grid Or all columns have a Display Setting Of 'N' or 'P'";
												}
												else if (unchecked(Operators.CompareString(text10, "*", TextCompare: false) == 0 && num9 > 0))
												{
													MyError = "Unfortunately, you cannot create a summary query (i.e., specify a summary function in one of the \"Statistics\" cells of the Query Grid), and specify the \"a<i>.*\" operator to select all columns from a table. You must either remove the summary functions or specify individual column names.";
												}
												else if ((Operators.CompareString(text10, "*", TextCompare: false) == 0) & (Operators.CompareString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].out_inline, "", TextCompare: false) != 0))
												{
													MyError = "Unfortunately, you cannot create an Inline View that includes columns with the \"a<i>.*\" operator. You must either remove the Inline View requirement or specify individual column names.";
												}
												else if ((Operators.CompareString(text10, "*", TextCompare: false) == 0) & (Operators.CompareString(ll_MajorAlias, "ILV", TextCompare: false) == 0) & (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0))
												{
													MyError = "Unfortunately, when using the SQLPlus Oracle driver, you cannot include columns with the \"a<i>.*\" operator. You must either use another Oracle driver or specify individual column names.";
												}
												else
												{
													text2 = ": Continue Validation of the CrossTab Query";
													if (num11 >= 1)
													{
														if (num14 >= 1)
														{
															MyError = General_Procedures.Get_UI("errsql6");
														}
														else if (ll_OutInline)
														{
															MyError = General_Procedures.Get_UI("errsql7");
														}
														else if (num12 == 0)
														{
															MyError = General_Procedures.Get_UI("errsql1");
														}
														else if (num12 > 1)
														{
															MyError = General_Procedures.Get_UI("errsql2");
														}
														else
														{
															if (num15 != 0)
															{
																num18 = 3;
																if (Operators.CompareString(ll_CTMiss, "", TextCompare: false) != 0)
																{
																	CTValue_s = CTValue_s + ":M=" + Strings.Trim(ll_CTMiss);
																}
																goto IL_1033;
															}
															MyError = General_Procedures.Get_UI("errsql3");
														}
													}
													else
													{
														if (num12 < 1)
														{
															goto IL_1033;
														}
														MyError = General_Procedures.Get_UI("errsql4");
													}
												}
											}
										}
									}
								}
							}
						}
						goto IL_35d5;
					}
					case 15831:
						{
							num = -1;
							switch (num2)
							{
							case 2:
								Support.ErrService(Information.Err().Number, text + text2, Information.Err().Description);
								Information.Err().Clear();
								MyProject.Forms.FrmMain.Gauge1.Value = 0;
								MyProject.Forms.FrmMain.Gauge1.Visible = false;
								goto end_IL_0001;
							}
							break;
						}
						IL_35d5:
						if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
						{
							Interaction.MsgBox(text24 + MyError, MsgBoxStyle.Critical, "Error Generating SQL Query");
							result = "";
						}
						else
						{
							if (Strings.InStr(Strings.LCase(SQLStr), "@spf-for-loop") != 0)
							{
								short num3 = 1;
								do
								{
									unchecked
									{
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-oracle-" + Conversions.ToString((int)num3) + "@", "CAST((FROM_TZ(CAST(TO_DATE('<<<spf-job-start-gmt-time>>>','YYYY-MM-DD HH24:MI:SS') AS TIMESTAMP), 'GMT') AT LOCAL) AS DATE) - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-oracle-" + Conversions.ToString((int)num3) + "@", "CAST((FROM_TZ(CAST(TO_DATE('<<<spf-job-start-gmt-time>>>','YYYY-MM-DD HH24:MI:SS') AS TIMESTAMP), 'GMT') AT LOCAL) AS DATE) + <<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-sqlserver-" + Conversions.ToString((int)num3) + "@", "DATEADD(minute, DATEDIFF(minute, GETUTCDATE(), GETDATE()), '<<<spf-job-start-gmt-time>>>') - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-sqlserver-" + Conversions.ToString((int)num3) + "@", "DATEADD(minute, DATEDIFF(minute, GETUTCDATE(), GETDATE()), '<<<spf-job-start-gmt-time>>>') + <<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-ibi-daas-" + Conversions.ToString((int)num3) + "@", "DATEADD(minute, DATEDIFF(minute, GETUTCDATE(), GETDATE()), '<<<spf-job-start-gmt-time>>>') - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-ibi-daas-" + Conversions.ToString((int)num3) + "@", "DATEADD(minute, DATEDIFF(minute, GETUTCDATE(), GETDATE()), '<<<spf-job-start-gmt-time>>>') + <<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
										{
											SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-teradata-" + Conversions.ToString((int)num3) + "@", "CAST('<<<spf-job-start-gmt-time>>>+00:00' AS timestamp(0)) - INTERVAL '<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + "-int>>>' Day ", 1, -1, CompareMethod.Text);
											SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-teradata-" + Conversions.ToString((int)num3) + "@", "CAST('<<<spf-job-start-gmt-time>>>+00:00' AS timestamp(0)) - INTERVAL '<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + "-int>>>' Day + INTERVAL '<<<spf-step-" + Conversions.ToString((int)num3) + "-int>>>' Day", 1, -1, CompareMethod.Text);
										}
										else
										{
											SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-teradata-" + Conversions.ToString((int)num3) + "@", "CAST('<<<spf-job-start-gmt-time>>>+00:00' AS timestamp(0)) - INTERVAL '<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>' Day ", 1, -1, CompareMethod.Text);
											SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-teradata-" + Conversions.ToString((int)num3) + "@", "CAST('<<<spf-job-start-gmt-time>>>+00:00' AS timestamp(0)) - INTERVAL '<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>' Day + INTERVAL '<<<spf-step-" + Conversions.ToString((int)num3) + ">>>' Day", 1, -1, CompareMethod.Text);
										}
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-sqlite-" + Conversions.ToString((int)num3) + "@", "DateTime('<<<spf-job-start-gmt-time>>>','Localtime','-<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>> Days')", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-sqlite-" + Conversions.ToString((int)num3) + "@", "DateTime('<<<spf-job-start-gmt-time>>>','Localtime','-<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>> Days','+<<<spf-step-" + Conversions.ToString((int)num3) + ">>> Days')", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-xeus-" + Conversions.ToString((int)num3) + "@", "TO_DATE('<<<spf-job-start-time>>>','YYYY-MM-DD HH24:MI:SS') - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-xeus-" + Conversions.ToString((int)num3) + "@", "TO_DATE('<<<spf-job-start-time>>>','YYYY-MM-DD HH24:MI:SS') + <<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-postgres-" + Conversions.ToString((int)num3) + "@", "TO_DATE('<<<spf-job-start-time>>>','YYYY-MM-DD HH24:MI:SS') - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-postgres-" + Conversions.ToString((int)num3) + "@", "TO_DATE('<<<spf-job-start-time>>>','YYYY-MM-DD HH24:MI:SS') + <<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-start-mongo-" + Conversions.ToString((int)num3) + "@", "(dt.datetime.strptime('<<<spf-job-start-time>>>','%Y-%m-%d %H:%M:%S') - dt.timedelta(<<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>))", 1, -1, CompareMethod.Text);
										SQLStr = Strings.Replace(SQLStr, "@spf-for-loop-end-mongo-" + Conversions.ToString((int)num3) + "@", "(dt.datetime.strptime('<<<spf-job-start-time>>>','%Y-%m-%d %H:%M:%S') + dt.timedelta(<<<spf-step-" + Conversions.ToString((int)num3) + ">>> - <<<spf-loop-ctr-" + Conversions.ToString((int)num3) + ">>>))", 1, -1, CompareMethod.Text);
									}
									num3 = (short)unchecked(num3 + 1);
								}
								while (num3 <= 3);
							}
							SQLStr = BuildForm.Repl_Squiggly(SQLStr, 1);
							if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 && Strings.InStr(SQLStr, "@@@DUCKDBTABLE@@@") != 0)
							{
								string text4 = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
								int num19 = Strings.InStrRev(text4, ":");
								if (num19 != -1)
								{
									text4 = Strings.Trim(Strings.Mid(text4, num19 + 1));
								}
								SQLStr = Strings.Replace(SQLStr, "@@@DUCKDBTABLE@@@", text4, 1, -1, CompareMethod.Text);
							}
							if (Operators.CompareString(ll_DatabaseType, "iMBigData", TextCompare: false) == 0 && Strings.InStr(SQLStr, "@@@@@IBD-FUNCTIONS@@@@@") != 0)
							{
								SQLStr = ((Operators.CompareString(MyiBDFunc, "", TextCompare: false) == 0) ? Strings.Replace(SQLStr, "@@@@@IBD-FUNCTIONS@@@@@", "", 1, -1, CompareMethod.Text) : Strings.Replace(SQLStr, "@@@@@IBD-FUNCTIONS@@@@@", MyiBDFunc, 1, -1, CompareMethod.Text));
							}
							if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0)
							{
								SQLStr = Strings.Replace(SQLStr, "Column-Pattern->[[M<;>", "Column-Pattern->[[N<;>", 1, -1, CompareMethod.Text);
							}
							result = ((!BuildForm.IsCSV(ll_ObjectType)) ? SubStitute_Node_Schema(ref SQLStr, ShowUN, ll_SPCMap, ll_SPCMapHdr, ll_SPCMap2, ll_SPCMapHdr2, ll_DBType) : SQLStr);
						}
						goto IL_3d11;
						IL_333d:
						if ((unchecked((uint)num32) & ((Operators.CompareString(ll_Row_Limit, "-1", TextCompare: false) != 0) ? 1u : 0u)) != 0)
						{
							SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + " LIMIT " + ll_Row_Limit + " " + CBEnd;
						}
						if (ll_ObjectType == 1)
						{
							if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
							{
								SQLStr = "SELECT * FROM (\r\n" + SQLStr + "\r\n)";
							}
							if (Operators.CompareString(ll_JoinDuckDB, "N", TextCompare: false) == 0)
							{
								string text36 = Create_SQLite_Idx(ll_JoinDuckDB);
								if (Operators.CompareString(text36, "", TextCompare: false) != 0)
								{
									SQLStr = text36 + "\r\n\r\n" + SQLStr;
								}
							}
						}
						if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && !ll_OutInline)
						{
							SQLStr = SQLStr + "\r\n" + CBBegin + "/*END SQL*/" + CBEnd + "\r\n";
						}
						if (ll_TableIndex != -1)
						{
							SQLStr += MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_PostSQL(ll_TableIndex, ref NoWhere, ll_ObjectType, ll_Row_Limit, text24, CBBegin, CBEnd, ll_OutInline, ll_DatabaseType);
						}
						if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
						{
							goto IL_35d5;
						}
						text2 = ": Finish building query";
						goto IL_350c;
						IL_19d3:
						MyError = Strings.Mid(text3, 7);
						goto IL_35d5;
						IL_1033:
						if (num14 == 1)
						{
							MyError = General_Procedures.Get_UI("errsql5");
						}
						else
						{
							text2 = ": Validate a Group By or a Select Query";
							if (num18 != 3)
							{
								if (num9 > 0)
								{
									num18 = 2;
								}
								else
								{
									num18 = 1;
									short g_NoQFilters3 = Globals_Renamed.g_NoQFilters;
									short num3;
									for (num3 = 0; num3 <= g_NoQFilters3; num3 = (short)unchecked(num3 + 1))
									{
										if (!LikeOperator.LikeString(Globals_Renamed.g_QFilters[num3].And_Renamed, "--*", CompareMethod.Binary))
										{
											left = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num3].DataType, 2, 1)));
											if (Operators.CompareString(left, "S", TextCompare: false) == 0)
											{
												break;
											}
										}
									}
									if (Operators.CompareString(left, "S", TextCompare: false) == 0)
									{
										MyError = General_Procedures.Get_UI("errsql8") + " " + Conversions.ToString(num3 + 1) + ") from your filter grid";
										goto IL_35d5;
									}
								}
							}
							if (unchecked(num18 == 1 && num17 > 0))
							{
								MyError = General_Procedures.Get_UI("errsql9");
							}
							else
							{
								MyError = Check_Open_Close_Paren();
								if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
								{
									if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
									{
										short g_NoQColumns4 = Globals_Renamed.g_NoQColumns;
										for (short num3 = 0; num3 <= g_NoQColumns4; num3 = (short)unchecked(num3 + 1))
										{
											string text4 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Header);
											if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
											{
												text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column), 1);
											}
											if ((Operators.CompareString(text4, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "*", TextCompare: false) != 0))
											{
												short num33 = (short)(num3 + 1);
												short g_NoQColumns5 = Globals_Renamed.g_NoQColumns;
												for (short num4 = num33; num4 <= g_NoQColumns5; num4 = (short)unchecked(num4 + 1))
												{
													string text5 = Strings.Trim(Globals_Renamed.g_QColumns[num4].Header);
													if (Operators.CompareString(Strings.Trim(text5), "", TextCompare: false) == 0)
													{
														text5 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num4].Column), 1);
													}
													if (Operators.CompareString(Strings.LCase(text4), Strings.LCase(text5), TextCompare: false) == 0)
													{
														MyError = "You have two columns in the Columns Grid that have the same header. I.e., Column Header \"" + text4 + "\" in rows " + Globals_Renamed.g_QColumns[num3].No + " and " + Globals_Renamed.g_QColumns[num4].No + ". This can cause issues when running the SQL. Do change one of the headers.";
														break;
													}
												}
											}
											if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
											{
												break;
											}
										}
									}
									if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
									{
										if (ll_ObjectType == 3)
										{
											string text6 = ".\\keywords_jet.ini";
										}
										else
										{
											string text6 = ".\\keywords.ini";
										}
										short g_NoQColumns6 = Globals_Renamed.g_NoQColumns;
										for (short num3 = 0; num3 <= g_NoQColumns6; num3 = (short)unchecked(num3 + 1))
										{
											string text4 = Strings.LCase(Strings.Trim(Globals_Renamed.g_QColumns[num3].Header));
											if (Operators.CompareString(text4, "", TextCompare: false) == 0)
											{
												text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column), 1);
											}
											if (((Operators.CompareString(text4, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "*", TextCompare: false) != 0)) && Operators.CompareString(General_Procedures.Get_Ini_Data("Invalid Keywords", Strings.LCase(text4), "yes", 10, Globals_Renamed.MySchemaDir + "\\keywords.ini"), "no", TextCompare: false) == 0)
											{
												MyError = "Column header \"" + Globals_Renamed.g_QColumns[num3].Header + "\" in row " + Globals_Renamed.g_QColumns[num3].No + " of the Columns Grid is a reserved keyword in some SQL engines and should not be used as a column header. Please change it.";
												break;
											}
										}
										if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
										{
											text2 = ": If No errors were encountered";
											if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
											{
												goto IL_350c;
											}
											if (ll_ObjectType == 2 && Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0)
											{
												Special_Process_List(ll_DisplayName);
												MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Determine_lists_to_set(ll_TableIndex);
												MyError = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Check_Required_Filter(ll_MajorAlias, ll_TableIndex);
												if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
												{
													goto IL_35d5;
												}
											}
											SQLStr = "";
											short num29 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GetSpecialTable(ll_TableIndex, ll_DBType);
											short g_NoQFilters4 = Globals_Renamed.g_NoQFilters;
											short num3;
											for (num3 = 0; num3 <= g_NoQFilters4; num3 = (short)unchecked(num3 + 1))
											{
												left = Globals_Renamed.g_QFilters[num3].List;
												if (Operators.CompareString(Strings.UCase(left), "@IF@", TextCompare: false) == 0)
												{
													Globals_Renamed.g_QFilters[num3].List = Strings.LCase("@IF@") + "1";
												}
											}
											num3 = 1;
											do
											{
												string text4 = "@IF@" + Strings.Trim(Conversions.ToString(unchecked((int)num3)));
												string[] array = gInLineFilter;
												short num34 = num3;
												short NoWhere2 = -1;
												array[num34] = Generate_Row_Filter(ref NoWhere2, text4, 1, CBBegin, CBEnd, ll_ObjectType, text24, ll_Row_Limit, ll_TableIndex, ll_OutInline, ll_DatabaseType);
												num3 = (short)unchecked(num3 + 1);
											}
											while (num3 <= 9);
											num3 = 9;
											do
											{
												if ((Operators.CompareString(gInlineCol[num3], "Y", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(gInLineFilter[num3]), "", TextCompare: false) != 0))
												{
													gInlineCol[num3 - 1] = "Y";
													if (num3 > num16)
													{
														num16 = num3;
													}
												}
												num3 = (short)unchecked(num3 + -1);
											}
											while (num3 >= 1);
											if (unchecked(num18 == 2 || num18 == 3))
											{
												num16++;
											}
											if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) != 0 && Operators.CompareString(text10, "*", TextCompare: false) == 0 && (num9 > 0 || num16 > 0))
											{
												MyError = General_Procedures.Get_UI("errsql10");
											}
											else if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0 && (fNoPostExtArr >= 0 || Operators.CompareString(text10, "*", TextCompare: false) == 0) && num9 > 0)
											{
												MyError = "Unfortunately, you cannot create a summary query and either perform a SELECT * query or select MongoDB option: \"Post Extract Pattern Selection\".";
											}
											else
											{
												if (Globals_Renamed.g_NoSQLiteAF >= 1)
												{
													int num31 = 0;
													num3 = 0;
													do
													{
														if (gSQLiteAF_Level[num3].AF_Exists && num3 > num31)
														{
															num31 = num3;
														}
														num3 = (short)unchecked(num3 + 1);
													}
													while (num3 <= 20);
													if (num18 == 1)
													{
														if (num16 <= num31)
														{
															num16 = (short)(num31 + 1);
														}
													}
													else if (num17 + 10 <= num31)
													{
														num17 = (short)(num31 - 10 + 1);
													}
												}
												text2 = ": Write SQLPathFinder Query Header Information";
												text2 = ": Write pre logic";
												MyProject.Forms.FrmMain.Gauge1.Value = 15;
												if (ll_TableIndex != -1)
												{
													SQLStr += MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_PreSQL(ll_TableIndex, ref NoWhere, ll_ObjectType, ll_Row_Limit, text24, CBBegin, CBEnd, ll_OutInline, ll_DatabaseType);
												}
												if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0)
												{
													SQLStr = Generate_Mongo_Query(num29, ll_Row_Limit, num18, num17, num16, ll_TableIndex, ll_MajorAlias, ref MyError, ll_M5, ll_DisplayName, fNoPostExtArr, ref fPostExtArr);
													if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
													{
														goto IL_350c;
													}
												}
												else
												{
													if (num29 != 3)
													{
														text2 = ": Build the Select clause";
														if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0 && !ll_OutInline)
														{
															SQLStr = SQLStr + CBBegin + "/*BEGIN SQL*/" + CBEnd + "\r\n";
														}
														bool flag = true;
														text3 = "";
														short num35 = num17;
														num3 = num35;
														while (num3 >= 0)
														{
															string text19;
															string text18;
															if (num3 == 0)
															{
																text19 = Conversions.ToString(Value: true);
																text18 = "";
															}
															else
															{
																text19 = Conversions.ToString(Value: false);
																text18 = "s+" + Conversions.ToString(unchecked((int)num3));
															}
															text3 = Generate_Select(text18, flag, Conversions.ToBoolean(text19), ref MyHints, CBBegin, CBEnd, num18, num16, num29, ll_ObjectType, ll_DBType, ll_MajorAlias, ll_DatabaseType, ll_TableIndex, ll_Row_Limit, ll_Distinct_Rows, ll_OutInline, ll_QuoteCSV, ll_JoinDuckDB);
															if (!LikeOperator.LikeString(text3, "ERROR:*", CompareMethod.Binary))
															{
																SQLStr = ((num3 != 0) ? (SQLStr + text3 + "\r\n" + CBBegin + "FROM" + CBEnd + "\r\n" + CBBegin + "(" + CBEnd + "\r\n") : (SQLStr + text3));
																flag = false;
																text3 = "";
																num3 = (short)unchecked(num3 + -1);
																continue;
															}
															goto IL_19d3;
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 30;
														Process_Inline("S", num18, CBBegin, CBEnd, num29, MyHints, ll_DatabaseType, ref SQLStr, ref gInLineFilter, ref gInlineCol, num16, 9, ref ll_ObjectType, ll_MajorAlias);
														MyProject.Forms.FrmMain.Gauge1.Value = 35;
														text2 = ": Build the From clause";
														SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "FROM " + CBEnd + Globals_Renamed.CRLF;
														string text6;
														if (unchecked(ll_ObjectType == 0 || ll_ObjectType == 4))
														{
															short num36 = (short)(Globals_Renamed.MaxFromTables - 1);
															for (num3 = 0; num3 <= num36; num3 = (short)unchecked(num3 + 1))
															{
																string text5 = Strings.Trim(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GetCurrTableName("", "", num3));
																int num19 = Strings.InStrRev(text5, " a");
																if (num19 != 0)
																{
																	text6 = Strings.Mid(text5, 1, num19 - 1);
																	myIdx = ((ll_TableIndex != -1) ? ll_TableIndex : ((short)Math.Round(Conversion.Val(Strings.Trim(Strings.Mid(text5, num19 + 2))))));
																}
																left = ((num3 != 1) ? "," : "");
																if (unchecked(Operators.CompareString(Strings.Mid(Strings.Trim(text5), 1, 1), "&", TextCompare: false) == 0 || ll_ObjectType == 4))
																{
																	DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
																	int num28 = General_Procedures.ParseAndFillArray(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GetTableListDtls0(myIdx), Globals_Renamed.CRLF, ref DynArray);
																	SQLStr = ((num3 != 0) ? (SQLStr + Globals_Renamed.CRLF + CBBegin + "," + DynArray[1] + CBEnd) : (SQLStr + CBBegin + DynArray[1] + CBEnd));
																	int num37 = num28 - 2;
																	for (int num5 = 2; num5 <= num37; num5++)
																	{
																		SQLStr = SQLStr + Globals_Renamed.CRLF + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].DoInline(DynArray[num5], CBBegin, CBEnd);
																	}
																	SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + DynArray[num28 - 1];
																	DynArray = null;
																	if (num19 != 0)
																	{
																		SQLStr = SQLStr + " " + Strings.Mid(text5, num19 + 1);
																	}
																	SQLStr += CBEnd;
																}
																else
																{
																	SQLStr = ((num3 != 1) ? (SQLStr + Globals_Renamed.CRLF + CBBegin + left + text5 + CBEnd) : (SQLStr + CBBegin + left + text5 + CBEnd));
																}
															}
														}
														else if (ll_ObjectType == 2 && Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0)
														{
															SQLStr += MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_View_From_Clause(ll_TableIndex, ref NoWhere, num29, ll_ObjectType, ll_Row_Limit, text24, CBBegin, CBEnd, ll_OutInline, ll_DatabaseType, ref MyError, ref MyiBDFunc);
															if (Operators.CompareString(MyError, "", TextCompare: false) != 0)
															{
																goto IL_35d5;
															}
														}
														else if (ll_ObjectType == 3 || ll_ObjectType == 1 || Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0)
														{
															SQLStr += Write_Join(CBBegin, CBEnd, ll_ObjectType, ll_JoinDuckDB);
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 40;
														text2 = ": Build the Where clause";
														if (num29 == 0)
														{
															NoWhere = -1;
															if (unchecked(ll_ObjectType == 0 || ll_ObjectType == 4))
															{
																short g_NoQJoins = Globals_Renamed.g_NoQJoins;
																for (num3 = 0; num3 <= g_NoQJoins; num3 = (short)unchecked(num3 + 1))
																{
																	string text21 = Globals_Renamed.g_QJoins[num3].Col1;
																	if (Operators.CompareString(text21, "", TextCompare: false) != 0)
																	{
																		string text20 = Globals_Renamed.g_QJoins[num3].Col2;
																		if (Operators.CompareString(text20, "", TextCompare: false) != 0)
																		{
																			string text22 = Strings.Trim(Globals_Renamed.g_QJoins[num3].Join_Renamed);
																			string text14 = Strings.Trim(Globals_Renamed.g_QJoins[num3].And_Renamed);
																			string text13 = Strings.Trim(Globals_Renamed.g_QJoins[num3].ParenO);
																			string text15 = Strings.Trim(Globals_Renamed.g_QJoins[num3].ParenC);
																			if (Operators.CompareString(Strings.Mid(text22, 1, 1), "*", TextCompare: false) == 0)
																			{
																				text21 += " (+)";
																				text22 = Strings.Mid(text22, 2);
																			}
																			else if (Operators.CompareString(Strings.Mid(text22, Strings.Len(text22), 1), "*", TextCompare: false) == 0)
																			{
																				text20 += " (+)";
																				text22 = Strings.Mid(text22, 1, Strings.Len(text22) - 1);
																			}
																			if (NoWhere == -1)
																			{
																				SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "WHERE " + CBEnd;
																				text14 = ((!LikeOperator.LikeString(text14, "*NOT*", CompareMethod.Binary)) ? "         " : " NOT     ");
																				SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + text14 + "     " + text13 + text21 + " " + text22 + " " + text20 + text15 + CBEnd;
																				NoWhere = 0;
																			}
																			else
																			{
																				if (Operators.CompareString(text14, "", TextCompare: false) == 0)
																				{
																					text14 = "AND";
																				}
																				SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + " " + text14 + "     " + text13 + text21 + " " + text22 + " " + text20 + text15 + CBEnd;
																			}
																		}
																	}
																}
															}
															else if (ll_ObjectType == 2 && Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0)
															{
																SQLStr += MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_Joins(ll_TableIndex, ref NoWhere, CBBegin, CBEnd);
															}
															MyProject.Forms.FrmMain.Gauge1.Value = 50;
															if (Globals_Renamed.g_NoQFilters > -1)
															{
																SQLStr += Globals_Renamed.CRLF;
															}
															SQLStr = ((NoWhere != -1 || Operators.CompareString(Strings.Right(SQLStr, 2), Globals_Renamed.CRLF, TextCompare: false) != 0) ? (SQLStr + Generate_Row_Filter(ref NoWhere, "==ALL==", 1, CBBegin, CBEnd, ll_ObjectType, text24, ll_Row_Limit, ll_TableIndex, ll_OutInline, ll_DatabaseType)) : (SQLStr + Generate_Row_Filter(ref NoWhere, "==ALL==", 0, CBBegin, CBEnd, ll_ObjectType, text24, ll_Row_Limit, ll_TableIndex, ll_OutInline, ll_DatabaseType)));
														}
														if (ll_ObjectType != 3)
														{
															switch (ll_DatabaseType)
															{
															default:
																if (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0 || Operators.CompareString(ll_DBType, "Mongo", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 || ll_ObjectType == 1)
																{
																	break;
																}
																switch (ll_DatabaseType)
																{
																default:
																	if (Operators.CompareString(ll_DatabaseType, "Postgres", TextCompare: false) != 0 && Operators.CompareString(ll_Row_Limit, "-1", TextCompare: false) != 0)
																	{
																		if (NoWhere == -1)
																		{
																			NoWhere = 0;
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "WHERE" + CBEnd;
																			SQLStr = ((Operators.CompareString(ll_Row_Limit, "-99", TextCompare: false) != 0) ? (SQLStr + Globals_Renamed.CRLF + CBBegin + "              RowNum <= " + ll_Row_Limit + CBEnd) : (SQLStr + Globals_Renamed.CRLF + CBBegin + "              RowNum <= &PROMPT&(LIMI)&PROMPT&" + CBEnd));
																		}
																		else
																		{
																			left = ((Operators.CompareString(Strings.Right(SQLStr, 2), "\r\n", TextCompare: false) != 0) ? "\r\n" : "");
																			SQLStr = ((Operators.CompareString(ll_Row_Limit, "-99", TextCompare: false) != 0) ? (SQLStr + left + CBBegin + " AND      RowNum <= " + ll_Row_Limit + CBEnd) : (SQLStr + left + CBBegin + " AND      RowNum <= " + Conversions.ToString(Conversions.ToDouble("&PROMPT&(LIMI)&PROMPT&") * Conversions.ToDouble(CBEnd))));
																		}
																	}
																	break;
																case "MySQL":
																case "Hadoop":
																case "SAPHana":
																case "Denodo":
																case "Snowflake":
																	break;
																}
																break;
															case "SQLServer":
															case "IBI-DaaS":
															case "Teradata":
															case "SQLite":
																break;
															}
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 60;
														Process_Inline("W", num18, CBBegin, CBEnd, num29, "", ll_DatabaseType, ref SQLStr, ref gInLineFilter, ref gInlineCol, num16, 9, ref ll_ObjectType, ll_MajorAlias);
														text2 = ": Build the Group By clause";
														if (unchecked(num18 == 2 || num18 == 3))
														{
															short num8 = 1;
															short g_NoQColumns7 = Globals_Renamed.g_NoQColumns;
															for (num3 = 0; num3 <= g_NoQColumns7; num3 = (short)unchecked(num3 + 1))
															{
																string text11 = Globals_Renamed.g_QColumns[num3].List;
																if (((Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num3].Show, 1, 1), "Y", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.g_QColumns[num3].Show, "N:S", TextCompare: false) == 0)) & (General_Procedures.SummPlusLevel(text11) == 0))
																{
																	text6 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Statistics);
																	string text5;
																	if ((num29 == 1 && Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num3].Comment, 1, 3), "***", TextCompare: false) != 0) || ((num18 == 2 || num18 == 3) && Operators.CompareString(Strings.UCase(text11), "@IF@" + Strings.Trim(Conversions.ToString(unchecked((int)num16))), TextCompare: false) != 0))
																	{
																		text5 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Header);
																		if (Operators.CompareString(Strings.Trim(text5), "", TextCompare: false) == 0)
																		{
																			text5 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column), 1);
																		}
																	}
																	else
																	{
																		text5 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Column);
																	}
																	if (Operators.CompareString(text6, "None", TextCompare: false) == 0)
																	{
																		if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && Strings.InStr(text5, "CrossTab->[") != 0)
																		{
																			text5 = Strings.Replace(text5, ":N]", ":A]", 1, -1, CompareMethod.Text);
																			text5 = Strings.Replace(text5, ":M]", ":A]", 1, -1, CompareMethod.Text);
																		}
																		if (BuildForm.IsColPattern(text5))
																		{
																			text5 = Strings.Replace(text5, "[[N<;>", "[[A<;>", 1, -1, CompareMethod.Text);
																			text5 = Strings.Replace(text5, "[[M<;>", "[[A<;>", 1, -1, CompareMethod.Text);
																			text5 = Strings.Replace(text5, "[[J<;>", "[[A<;>", 1, -1, CompareMethod.Text);
																			text5 = Strings.Replace(text5, "[[Y<;>", "[[A<;>", 1, -1, CompareMethod.Text);
																			text5 = ChkHdrCol_Pattern(text5, 0);
																		}
																		if (num8 == 1)
																		{
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "GROUP BY " + CBEnd;
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "          " + text5 + CBEnd;
																			num8 = 0;
																		}
																		else
																		{
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "         ," + text5 + CBEnd;
																		}
																	}
																}
															}
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 70;
														text2 = ": Build the Having clause";
														if (unchecked(num18 == 2 || num18 == 3))
														{
															short num8 = 1;
															short g_NoQFilters5 = Globals_Renamed.g_NoQFilters;
															for (num3 = 0; num3 <= g_NoQFilters5; num3 = (short)unchecked(num3 + 1))
															{
																string text23 = Strings.Trim(Globals_Renamed.g_QFilters[num3].And_Renamed);
																left = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num3].DataType, 2, 1)));
																if ((Operators.CompareString(left, "S", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(text23, 1, 2), "--", TextCompare: false) != 0))
																{
																	left = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num3].DataType, 1, 1)));
																	string text25 = Strings.Trim(Globals_Renamed.g_QFilters[num3].ParentO);
																	string text26 = Strings.Trim(Globals_Renamed.g_QFilters[num3].Column);
																	string text27 = Strings.Trim(Globals_Renamed.g_QFilters[num3].Operator_Renamed);
																	string text29 = Strings.Trim(Globals_Renamed.g_QFilters[num3].Value1);
																	string text30 = Strings.Trim(Globals_Renamed.g_QFilters[num3].Value2);
																	string text32 = Strings.Trim(Globals_Renamed.g_QFilters[num3].ParenC);
																	string text4 = ((!LikeOperator.LikeString(Strings.UCase(text27), "*BETWEEN*", CompareMethod.Binary) || Strings.InStr(text29, "&PROMPT&") != 0 || (LikeOperator.LikeString(Strings.Replace(text29, "'", "", 1, -1, CompareMethod.Text), "<<<*>>>", CompareMethod.Binary) && (!LikeOperator.LikeString(Strings.Replace(text29, "'", "", 1, -1, CompareMethod.Text), "<<<*>>>", CompareMethod.Binary) || Operators.CompareString(text30, "", TextCompare: false) == 0))) ? "" : "And");
																	if (BuildForm.IsValidFilter("2", text26, text27, text29, text30))
																	{
																		if (LikeOperator.LikeString(Strings.UCase(text27), "*IN*", CompareMethod.Binary))
																		{
																			text29 = "(" + text29 + ")";
																		}
																		else if ((Operators.CompareString(left, "C", TextCompare: false) == 0) | (Operators.CompareString(left, "Q", TextCompare: false) == 0) | (Operators.CompareString(left, "X", TextCompare: false) == 0))
																		{
																			text29 = General_Procedures.StripQuotesAndDates(text29, "", 0);
																			text29 = "'" + text29 + "'";
																			if (LikeOperator.LikeString(Strings.UCase(text27), "*BETWEEN*", CompareMethod.Binary))
																			{
																				text30 = General_Procedures.StripQuotesAndDates(text30, "", 0);
																				text30 = "'" + text30 + "'";
																			}
																		}
																		else if (Operators.CompareString(left, "D", TextCompare: false) == 0 || Operators.CompareString(left, "V", TextCompare: false) == 0)
																		{
																			if (Information.IsDate(text29))
																			{
																				text29 = "To_Date('" + text29 + "','dd-Mon-yyyy hh24:mi:ss')";
																			}
																			if (LikeOperator.LikeString(Strings.UCase(text27), "*BETWEEN*", CompareMethod.Binary) && Information.IsDate(text30))
																			{
																				text30 = "To_Date('" + text30 + "','dd-Mon-yyyy hh24:mi:ss')";
																			}
																		}
																		if (num8 == 1)
																		{
																			num8 = 0;
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "HAVING" + CBEnd;
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "              " + text25 + text26 + " " + text27 + " " + text29 + " " + text4 + " " + text30 + " " + text32 + CBEnd;
																		}
																		else
																		{
																			switch (text23)
																			{
																			case null:
																			case "":
																				text23 = "AND";
																				break;
																			case "OR":
																				text23 = "OR ";
																				break;
																			}
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + " " + text23 + "      " + text25 + text26 + " " + text27 + " " + text29 + " " + text4 + " " + text30 + " " + text32 + CBEnd;
																		}
																	}
																}
															}
														}
														text6 = "";
														if (num17 > 0)
														{
															string text4 = "t";
															short num38 = num17;
															for (num3 = 1; num3 <= num38; num3 = (short)unchecked(num3 + 1))
															{
																if (Operators.CompareString(ll_DatabaseType, "Oracle", TextCompare: false) == 0)
																{
																	text4 = "t" + Conversions.ToString(unchecked((int)num3));
																}
																else if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
																{
																	text4 = "t /*L" + Conversions.ToString(9 + num3) + "*/";
																}
																SQLStr = SQLStr + "\r\n" + CBBegin + " ) " + text4 + CBEnd;
																string text39 = SQLStr;
																short NoWhere2 = -1;
																SQLStr = text39 + Generate_Row_Filter(ref NoWhere2, "s+" + Conversions.ToString(unchecked((int)num3)), 1, CBBegin, CBEnd, ll_ObjectType, text24, ll_Row_Limit, ll_TableIndex, ll_OutInline, ll_DatabaseType);
															}
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 75;
														text2 = ": Build the Order By clause";
														if (unchecked(num18 == 1 || num18 == 2))
														{
															short num8 = 1;
															short num27 = 0;
															string text4 = "  ";
															string text17 = "0";
															short g_NoQColumns8;
															unchecked
															{
																if (Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0)
																{
																	if (num17 > 0)
																	{
																		text17 = "s+" + Conversions.ToString((int)num17);
																	}
																	else
																	{
																		text17 = GetMaxLvl();
																		if (Operators.CompareString(Strings.LCase(text17), "s", TextCompare: false) != 0 && (num18 == 2 || num18 == 3))
																		{
																			text17 = "s";
																		}
																	}
																}
																g_NoQColumns8 = Globals_Renamed.g_NoQColumns;
															}
															for (num3 = 0; num3 <= g_NoQColumns8; num3 = (short)unchecked(num3 + 1))
															{
																if (Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num3].Show, 1, 1), "Y", TextCompare: false) == 0)
																{
																	num27++;
																	text6 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Sort);
																	string text9 = Strings.UCase(Globals_Renamed.g_QColumns[num3].Statistics);
																	string text8 = "";
																	if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) == 0 && Operators.CompareString(Strings.UCase(ll_MajorAlias), "ALL", TextCompare: false) == 0 && Operators.CompareString(text9, "NONE", TextCompare: false) == 0)
																	{
																		string text7 = Strings.UCase(Globals_Renamed.g_QColumns[num3].DataType);
																		if (Operators.CompareString(text7, "F", TextCompare: false) == 0)
																		{
																			text8 = "::DOUBLE";
																		}
																		else if (Operators.CompareString(text7, "N", TextCompare: false) == 0)
																		{
																			text8 = "::BIGINT";
																		}
																	}
																	if (Operators.CompareString(text6, "None", TextCompare: false) != 0)
																	{
																		if (num8 == 1)
																		{
																			num8 = 0;
																			SQLStr = ((Operators.CompareString(Strings.Right(SQLStr, 2), "\r\n", TextCompare: false) != 0) ? (SQLStr + "\r\n" + CBBegin + "ORDER BY" + CBEnd) : (SQLStr + CBBegin + "ORDER BY" + CBEnd));
																		}
																		if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
																		{
																			string text5 = Strings.Trim(Globals_Renamed.g_QColumns[num3].Header);
																			if (Operators.CompareString(Strings.Trim(text5), "", TextCompare: false) == 0)
																			{
																				text5 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num3].Column), 1);
																			}
																			text5 += text8;
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "         " + text4 + text5 + " " + text6 + CBEnd;
																		}
																		else
																		{
																			SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "         " + text4 + Conversions.ToString(unchecked((int)num27)) + " " + text6 + CBEnd;
																		}
																		text4 = " ,";
																	}
																}
															}
														}
														MyProject.Forms.FrmMain.Gauge1.Value = 90;
														if (ll_ObjectType == 1)
														{
															goto IL_333c;
														}
														switch (ll_DatabaseType)
														{
														case "MySQL":
														case "SQLite":
														case "Hadoop":
														case "SAPHana":
														case "Denodo":
														case "Snowflake":
														case "Postgres":
															goto IL_333c;
														}
														num32 = ((Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0) ? 1 : 0);
														goto IL_333d;
													}
													SQLStr += MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Get_View_From_Clause(ll_TableIndex, ref NoWhere, num29, ll_ObjectType, ll_Row_Limit, text24, CBBegin, CBEnd, ll_OutInline, ll_DatabaseType, ref MyError, ref MyiBDFunc);
													if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
													{
														goto IL_350c;
													}
												}
											}
										}
									}
								}
							}
						}
						goto IL_35d5;
						IL_3d11:
						MyProject.Forms.FrmMain.Gauge1.Value = 100;
						MyProject.Forms.FrmMain.Gauge1.Visible = false;
						MyProject.Forms.FrmMain.cmdIcon.Refresh();
						Globals_Renamed.CRLF = "\r\n";
						Array.Clear(gInLineFilter, 0, gInLineFilter.Length);
						Array.Clear(gInlineCol, 0, gInlineCol.Length);
						goto end_IL_0001;
						IL_333c:
						num32 = 1;
						goto IL_333d;
						IL_350c:
						if (ll_ObjectType == 2 && Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) != 0)
						{
							SQLStr = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Substitute_View(ll_TableIndex, SQLStr, ll_DBType);
						}
						if (Globals_Renamed.g_NoSQLiteAF >= 1)
						{
							SQLStr = BuildAF.Generate_AF_SQLite(ref gSQliteAF, ref gSQLiteAF_Level, SQLStr, num18, ref MyError);
						}
						if (Operators.CompareString(MyError, "", TextCompare: false) == 0)
						{
							SQLStr = Substitute_Fn2(SQLStr, ll_DatabaseType, ll_DisplayName);
							if (LikeOperator.LikeString(SQLStr, "ERROR*", CompareMethod.Binary))
							{
								MyError = SQLStr;
								SQLStr = "";
							}
						}
						goto IL_35d5;
					}
					goto IL_3e0d;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 15831;
				continue;
			}
			break;
			IL_3e0d:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Replace_Comma_semicolon(string ArgAll)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string result = default(string);
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
				case 453:
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
							goto IL_0015;
						case 4:
							goto IL_0019;
						case 6:
							goto IL_0045;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_0053;
						case 9:
							goto IL_005c;
						case 10:
							goto IL_0062;
						case 11:
							goto IL_0075;
						case 12:
							goto IL_0083;
						case 13:
							goto IL_009c;
						case 14:
							goto IL_00a5;
						case 16:
							goto IL_00b6;
						case 17:
							goto IL_00cf;
						case 18:
							goto IL_00d8;
						case 20:
							goto IL_00e9;
						case 21:
							goto IL_0108;
						case 23:
							goto IL_011d;
						case 15:
						case 19:
						case 22:
						case 24:
						case 25:
							goto IL_012c;
						case 26:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0108:
					num2 = 21;
					text += ";";
					goto IL_012c;
					IL_011d:
					num2 = 23;
					text += text2;
					goto IL_012c;
					IL_00e9:
					num2 = 20;
					if (Operators.CompareString(text2, ",", TextCompare: false) == 0 && num5 == 0)
					{
						goto IL_0108;
					}
					goto IL_011d;
					IL_012c:
					num2 = 25;
					num6 = checked(num6 + 1);
					goto IL_0135;
					IL_000b:
					num2 = 2;
					ArgAll = Strings.Trim(ArgAll);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					result = ArgAll;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if ((Operators.CompareString(ArgAll, "", TextCompare: false) == 0) | (Strings.InStr(ArgAll, ",") == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_0045;
					IL_0045:
					num2 = 6;
					text = "";
					goto IL_004e;
					IL_004e:
					num2 = 7;
					num6 = 0;
					goto IL_0053;
					IL_0053:
					num2 = 8;
					text2 = "";
					goto IL_005c;
					IL_005c:
					num2 = 9;
					num5 = 0;
					goto IL_0062;
					IL_0062:
					num2 = 10;
					num7 = Strings.Len(ArgAll);
					num6 = 1;
					goto IL_0135;
					IL_0135:
					if (num6 > num7)
					{
						break;
					}
					goto IL_0075;
					IL_0075:
					num2 = 11;
					text2 = Strings.Mid(ArgAll, num6, 1);
					goto IL_0083;
					IL_0083:
					num2 = 12;
					if (Operators.CompareString(text2, "(", TextCompare: false) == 0)
					{
						goto IL_009c;
					}
					goto IL_00b6;
					IL_009c:
					num2 = 13;
					num5 = checked(num5 + 1);
					goto IL_00a5;
					IL_00a5:
					num2 = 14;
					text += text2;
					goto IL_012c;
					IL_00b6:
					num2 = 16;
					if (Operators.CompareString(text2, ")", TextCompare: false) == 0)
					{
						goto IL_00cf;
					}
					goto IL_00e9;
					IL_00cf:
					num2 = 17;
					num5 = checked(num5 - 1);
					goto IL_00d8;
					IL_00d8:
					num2 = 18;
					text += text2;
					goto IL_012c;
					end_IL_0001_2:
					break;
				}
				num2 = 26;
				result = text;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 453;
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

	public static bool Get_Col_Header_From_ID(string MyOld, ref string MyNew, ref string MyError)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		bool result = default(bool);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				Type typeFromHandle;
				object[] obj;
				DataGridViewCell dataGridViewCell;
				object[] array;
				bool[] obj2;
				bool[] array2;
				object obj3;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 566:
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
							goto IL_003e;
						case 6:
							goto IL_0042;
						case 7:
							goto IL_0050;
						case 8:
							goto IL_00e1;
						case 9:
							goto IL_010c;
						case 10:
							goto IL_014c;
						case 11:
							goto IL_0165;
						case 12:
							goto IL_019f;
						case 14:
							goto end_IL_0001_2;
						case 17:
						case 18:
							goto IL_01c3;
						default:
							goto end_IL_0001;
						case 13:
						case 15:
						case 16:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_010c:
					num2 = 9;
					MyNew = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[1].Value);
					goto IL_014c;
					IL_014c:
					num2 = 10;
					if (Operators.CompareString(MyNew, "", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0165;
					IL_00e1:
					num2 = 8;
					if (Operators.CompareString(Strings.LCase(MyOld), "{" + text + "}", TextCompare: false) == 0)
					{
						goto IL_010c;
					}
					goto IL_01c3;
					IL_0165:
					num2 = 11;
					MyNew = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].HeaderCell.Value);
					goto IL_019f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					num6 = checked(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
					goto IL_003e;
					IL_003e:
					num2 = 5;
					result = false;
					goto IL_0042;
					IL_0042:
					num2 = 6;
					num7 = num6;
					num5 = 0;
					goto IL_01cc;
					IL_01cc:
					if (num5 > num7)
					{
						goto end_IL_0001_3;
					}
					goto IL_0050;
					IL_0050:
					num2 = 7;
					typeFromHandle = typeof(Strings);
					obj = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[12]).Value };
					array = obj;
					obj2 = new bool[1] { true };
					array2 = obj2;
					obj3 = NewLateBinding.LateGet(null, typeFromHandle, "LCase", obj, null, null, obj2);
					if (array2[0])
					{
						dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					text = Conversions.ToString(obj3);
					goto IL_00e1;
					IL_01c3:
					num2 = 18;
					num5 = checked(num5 + 1);
					goto IL_01cc;
					IL_019f:
					num2 = 12;
					MyError = "Column Number " + MyNew + " used in the query must have a column header. Please add one.";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj4) when (obj4 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj4);
				try0001_dispatch = 566;
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

	public static void Process_Inline(string MyMode, short QueryType, string CBBegin, string CBEnd, short SpecialTable, string MyHints, string l_DBType, ref string SQLStr, ref string[] gInLineFilter, ref string[] gInlineCol, short gILTop, short gILMax, ref short ll_ObjectType, string ll_MajorAlias)
	{
		short num = 0;
		short num2 = 0;
		short num3 = 0;
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		int num4 = 0;
		string text7 = "";
		string text8 = "";
		int num5 = 0;
		string text9 = "";
		string text10 = "";
		if (gILTop == 0 && (QueryType == 2 || QueryType == 3))
		{
			gILTop = 1;
		}
		string left = Strings.UCase(MyMode);
		checked
		{
			if (Operators.CompareString(left, "S", TextCompare: false) != 0)
			{
				if (Operators.CompareString(left, "W", TextCompare: false) != 0)
				{
					return;
				}
				switch (l_DBType)
				{
				default:
					if (Operators.CompareString(l_DBType, "Hadoop", TextCompare: false) != 0)
					{
						text3 = "";
						break;
					}
					goto case "SQLServer";
				case "SQLServer":
				case "SQLite":
				case "IBI-DaaS":
				case "MySQL":
				case "SAPHana":
				case "Teradata":
					text3 = " t";
					break;
				}
				short num6 = gILTop;
				for (num3 = 1; num3 <= num6; num3 = (short)unchecked(num3 + 1))
				{
					text = Strings.Trim(gInLineFilter[num3]);
					text6 = "";
					if (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0)
					{
						text6 = " /*L" + Conversions.ToString(num3 - 1) + "*/";
					}
					if (Operators.CompareString(Strings.Right(SQLStr, 2), "\r\n", TextCompare: false) == 0)
					{
						SQLStr = SQLStr + CBBegin + ")" + text3 + text6 + CBEnd;
					}
					else
					{
						SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + ")" + text3 + text6 + CBEnd;
					}
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						SQLStr += gInLineFilter[num3];
					}
				}
				return;
			}
			short num7 = (short)(gILTop - 1);
			for (num3 = num7; num3 >= 0; num3 = (short)unchecked(num3 + -1))
			{
				num2 = 1;
				if (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0)
				{
					text6 = "/*L" + Conversions.ToString(unchecked((int)num3)) + "*/ ";
				}
				if (num3 == 0)
				{
					SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "FROM" + CBEnd + Globals_Renamed.CRLF + CBBegin + "(" + CBEnd + Globals_Renamed.CRLF + CBBegin + Strings.UCase("SELECT ") + text6 + MyHints + " " + CBEnd;
				}
				else
				{
					SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "FROM" + CBEnd + Globals_Renamed.CRLF + CBBegin + "(" + CBEnd + Globals_Renamed.CRLF + CBBegin + Strings.UCase("SELECT ") + text6 + CBEnd;
				}
				short g_NoQColumns = Globals_Renamed.g_NoQColumns;
				for (num = 0; num <= g_NoQColumns; num = (short)unchecked(num + 1))
				{
					text5 = Globals_Renamed.g_QColumns[num].List;
					text = "Y";
					if (LikeOperator.LikeString(Strings.UCase(text5), "@IF@*", CompareMethod.Binary))
					{
						text3 = Strings.Trim(Strings.Mid(text5, Strings.Len("@IF@") + 1));
						if (Versioned.IsNumeric(text3) && Conversions.ToShort(text3) > num3)
						{
							text = "N";
						}
					}
					if (Globals_Renamed.g_QColumns[num].Column != null && (Operators.CompareString(Globals_Renamed.g_QColumns[num].Statistics, "Expr", TextCompare: false) != 0 || Strings.InStr(Globals_Renamed.g_QColumns[num].Column, "CrossTab->") != 0 || BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column)) && General_Procedures.SummPlusLevel(text5) == 0 && (Operators.CompareString(text, "Y", TextCompare: false) == 0 || BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num].Column)))
					{
						text3 = Strings.Trim(Conversions.ToString(unchecked((int)num3)));
						if ((Operators.CompareString(gInlineCol[num3], "Y", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(text5), "@IF@" + text3, TextCompare: false) == 0))
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
							text2 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
							if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) != 0)
							{
								text4 = ((Strings.InStr(Strings.Trim(text2), " ") == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text2), 1, 1), "[", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text2, 1, 1), "`", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text2, 1, 1), "\"", TextCompare: false) == 0) ? (text4 + " AS " + text2) : (text4 + " AS \"" + text2 + "\""));
							}
						}
						else if (SpecialTable == 1 || num3 != 0)
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
							if (BuildForm.IsColPattern(text4))
							{
								text4 = ChkHdrCol_Pattern(text4, 0);
								text2 = "";
							}
							else if (Strings.InStr(text4, "CrossTab->[") != 0)
							{
								text2 = "";
							}
							else
							{
								text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
								if (Operators.CompareString(Strings.Trim(text4), "", TextCompare: false) == 0)
								{
									text4 = General_Procedures.Strip_Column(Strings.Trim(Globals_Renamed.g_QColumns[num].Column), 1);
								}
							}
							if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && BuildForm.IsColPattern(text4))
							{
								text4 = Strings.Replace(text4, "[[Y<;>", "[[N<;>", 1, -1, CompareMethod.Text);
								text4 = Strings.Replace(text4, "[[J<;>", "[[N<;>", 1, -1, CompareMethod.Text);
							}
							if (Strings.InStr(text4, "CrossTab->[") == 0 && !BuildForm.IsColPattern(text4))
							{
								text4 = text4 + " AS " + text4;
							}
						}
						else if (num3 == 0)
						{
							text4 = Strings.Trim(Globals_Renamed.g_QColumns[num].Column);
							text2 = Strings.Trim(Globals_Renamed.g_QColumns[num].Header);
							if (Strings.InStr(text4, "CrossTab->[") != 0)
							{
								text4 = Strings.Replace(text4, ":N]", ":Y]", 1, -1, CompareMethod.Text);
							}
							if (BuildForm.IsColPattern(text4))
							{
								if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "DUCKDB", TextCompare: false) == 0) && Operators.CompareString(Strings.Mid(Strings.LCase(text4), 1, 23), "column-pattern->[[n<;>a", TextCompare: false) == 0 && Strings.InStr(Strings.LCase(text4), "<dot>") != 0)
								{
									text4 = Strings.Replace(text4, "[[N<;>", "[[X<;>", 1, -1, CompareMethod.Text);
									num4 = Strings.InStr(text4, "[[X<;>");
									text7 = Strings.Mid(text4, num4 + 6);
									num4 = Strings.InStr(text7, "<;>");
									text7 = Strings.Mid(text7, 1, num4 - 1);
									if (!LikeOperator.LikeString(text4, "*<;>" + Globals_Renamed.gWinuser + "_a*_" + Globals_Renamed.gSPFCache + "<dot>tab<;>*", CompareMethod.Binary) && Strings.InStr(Strings.LCase(text4), "<;>duckdb:") == 0)
									{
										text4 = Strings.Replace(text4, "[[X<;>" + text7 + "<;>", "[[X<;>sql<;>", 1, -1, CompareMethod.Text);
									}
								}
								else if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "DUCKDB", TextCompare: false) == 0) && Operators.CompareString(Strings.Mid(Strings.LCase(text4), 1, 23), "column-pattern->[[n<;>a", TextCompare: false) == 0 && Strings.InStr(Strings.LCase(text4), "__sdb__") != 0)
								{
									text4 = Strings.Replace(text4, "[[N<;>", "[[X<;>", 1, -1, CompareMethod.Text);
									num4 = Strings.InStr(text4, "[[X<;>");
									text7 = Strings.Mid(text4, num4 + 6);
									num4 = Strings.InStr(text7, "<;>");
									text7 = Strings.Mid(text7, 1, num4 - 1);
									num4 = Strings.InStr(Strings.LCase(text4), "__sdb__");
									text8 = Strings.Mid(text4, num4 + 7);
									num4 = Strings.InStr(text8, "<;>");
									text8 = Strings.Mid(text8, 1, num4 - 1);
									text4 = Strings.Replace(text4, "[[X<;>" + text7 + "<;>", "[[X<;>" + text8 + "<;>", 1, -1, CompareMethod.Text);
								}
								else if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && unchecked(0u - ((Operators.CompareString(Strings.Mid(Strings.LCase(text4), 1, 23), "column-pattern->[[y<;>a", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.LCase(text4), 1, 23), "column-pattern->[[j<;>a", TextCompare: false) == 0) ? 1u : 0u)) != 0)
								{
									text4 = Strings.Replace(text4, "[[Y<;>", "[[X<;>", 1, -1, CompareMethod.Text);
									text4 = Strings.Replace(text4, "[[J<;>", "[[X<;>", 1, -1, CompareMethod.Text);
								}
								else
								{
									text4 = Strings.Replace(text4, "[[N<;>", "[[Y<;>", 1, -1, CompareMethod.Text);
									if (Operators.CompareString(ll_MajorAlias, "All", TextCompare: false) == 0 && (Operators.CompareString(l_DBType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(l_DBType, "DUCKDB", TextCompare: false) == 0) && Strings.InStr(text4, "@MONGO-KS1@:") != 0)
									{
										text4 = ChkHdrCol_Pattern(text4, 0);
									}
								}
								if (Strings.InStr(text4, "[[M<;>") != 0)
								{
									text10 = "";
									text9 = ChkHdrCol_Pattern(text4, 5);
									num4 = Strings.InStr(text9, ":");
									if (num4 > 1)
									{
										text10 = Strings.Trim(Strings.Mid(text9, num4 + 1));
										text4 = Strings.Replace(text4, "->[[M<;>" + text9 + "<;>", "->[[M<;>" + text10 + "<;>", 1, -1, CompareMethod.Text);
									}
									text4 = Strings.Replace(text4, "[[M<;>", "[[X<;>", 1, -1, CompareMethod.Text);
								}
								if (Operators.CompareString(Globals_Renamed.g_QColumns[num].Statistics, "Expr", TextCompare: false) == 0 || Operators.CompareString(text, "N", TextCompare: false) == 0)
								{
									text4 = ChkHdrCol_Pattern(text4, 0);
								}
							}
							if (Operators.CompareString(Strings.Trim(text2), "", TextCompare: false) != 0)
							{
								text4 = ((Strings.InStr(Strings.Trim(text2), " ") == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text2), 1, 1), "[", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text2), 1, 1), "`", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Strings.Trim(text2), 1, 1), "\"", TextCompare: false) == 0) ? (text4 + " AS " + text2) : (text4 + " AS \"" + text2 + "\""));
							}
						}
						if (num2 == 1)
						{
							SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "          " + text4 + CBEnd;
							num2 = 0;
						}
						else
						{
							SQLStr = SQLStr + Globals_Renamed.CRLF + CBBegin + "         ," + text4 + CBEnd;
						}
					}
				}
			}
		}
	}

	public static void Chk_Temp_Inc(string MyMode, ref string MyValue, ref string MyIncrement)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 372:
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
						case 7:
							goto IL_003f;
						case 8:
							goto IL_004f;
						case 9:
							goto IL_005f;
						case 11:
							goto IL_008b;
						case 13:
							goto IL_00a0;
						case 10:
						case 12:
						case 14:
						case 15:
							goto IL_00b6;
						case 16:
							goto IL_00c4;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 18:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a0:
					num2 = 13;
					num5 = Strings.InStrRev(MyValue, ":");
					goto IL_00b6;
					IL_008b:
					num2 = 11;
					num5 = Strings.InStr(MyValue, ":");
					goto IL_00b6;
					IL_00c4:
					num2 = 16;
					MyIncrement = Strings.Trim(checked(Strings.Mid(MyValue + " ", num6 + 5, num5 - (num6 + 5))));
					break;
					IL_00b6:
					num2 = 15;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00c4;
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
					MyIncrement = "";
					goto IL_001d;
					IL_001d:
					num2 = 5;
					if (Operators.CompareString(Strings.Trim(MyValue), "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_003f;
					IL_003f:
					num2 = 7;
					num6 = Strings.InStr(MyValue, "^^^^^");
					goto IL_004f;
					IL_004f:
					num2 = 8;
					if (num6 <= 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_005f;
					IL_005f:
					num2 = 9;
					left = Strings.UCase(MyMode);
					if (Operators.CompareString(left, "T", TextCompare: false) == 0)
					{
						goto IL_008b;
					}
					if (Operators.CompareString(left, "G", TextCompare: false) == 0)
					{
						goto IL_00a0;
					}
					goto IL_00b6;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				MyValue = Strings.Mid(MyValue, 1, checked(num6 - 1)) + " " + Strings.Mid(MyValue, num5);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 372;
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

	public static string Process_Quotes_Filter(string MyFilter)
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
				case 104:
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
					text = MyFilter;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Strings.InStr(text, "<!>") == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				text = Strings.Replace(text, "<!>", "<$!>", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 104;
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

	public static string Generate_Row_Filter(ref short NoWhere, string List, short CR, string CBBegin, string CBEnd, short ll_ObjectType, string MsgPrefix, string ll_Row_Limit, short ll_TableIndex, bool ll_OutInline, string ll_DatabaseType)
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
					errsource = "BuildSQL - Generate_Row_Filter. " + MsgPrefix + ".";
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
					string text11 = "";
					string text12 = "";
					string text13 = "";
					string text14 = "";
					int num3 = 0;
					string text15 = "";
					string text16 = "";
					string text17 = "";
					string text18 = "";
					string text19 = "";
					string text20 = "SQL_";
					text16 = "DATA" + Conversions.ToString((int)ll_ObjectType) + "^";
					short num4 = 0;
					string text21 = "";
					string text22 = "";
					short num5 = 0;
					short num6 = 0;
					short num7 = 0;
					short num8 = 0;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					int num12 = 0;
					string text23 = "";
					string text24 = "";
					string MyCol = "";
					string source = ")(&%$#@!";
					checked
					{
						if ((Operators.CompareString(Strings.LCase(List), "rowlim", TextCompare: false) == 0) & (Operators.CompareString(ll_Row_Limit, "-1", TextCompare: false) != 0) & (Operators.CompareString(ll_Row_Limit, "-99", TextCompare: false) != 0))
						{
							if (NoWhere == -1)
							{
								text23 = ((CR != 1) ? (text23 + CBBegin + "WHERE" + CBEnd) : (text23 + Globals_Renamed.CRLF + CBBegin + "WHERE" + CBEnd));
								text23 = text23 + Globals_Renamed.CRLF + CBBegin + "      RowNum <= " + ll_Row_Limit + CBEnd;
							}
							else
							{
								text23 = text23 + CBBegin + " AND       RowNum <= " + ll_Row_Limit + CBEnd;
							}
						}
						else
						{
							if (Strings.Len(List) >= 3 && Strings.InStr(List, "*-[") != 0 && Operators.CompareString(Strings.Mid(List, Strings.Len(List), 1), "]", TextCompare: false) == 0)
							{
								num5 = (short)Strings.InStr(List, "*-[");
								source = "," + Strings.Replace(Strings.LCase(Strings.Mid(List, num5 + 3, Strings.Len(List) - (num5 + 3))), " ", "", 1, -1, CompareMethod.Text) + ",";
								List = Strings.Mid(List, 1, num5);
							}
							short g_NoQFilters = Globals_Renamed.g_NoQFilters;
							for (num6 = 0; num6 <= g_NoQFilters; num6 = (short)unchecked(num6 + 1))
							{
								text20 = "SQL_";
								text12 = "";
								text = Globals_Renamed.g_QFilters[num6].List;
								string text25 = text;
								text6 = Strings.Trim(Globals_Renamed.g_QFilters[num6].And_Renamed);
								if ((((Operators.CompareString(List, "==ALL==", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(Strings.UCase(text), 1, Strings.Len("@IF@")), "@IF@", TextCompare: false) != 0) & (General_Procedures.SummPlusLevel(text) == 0)) | ((Operators.CompareString(List, "*", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(Strings.UCase(text), 1, Strings.Len("@IF@")), "@IF@", TextCompare: false) != 0) & (General_Procedures.SummPlusLevel(text) == 0) & !LikeOperator.LikeString(source, "*," + Strings.LCase(text) + ",*", CompareMethod.Binary)) | ((Operators.CompareString(List, "*", TextCompare: false) != 0) & LikeOperator.LikeString(Strings.LCase(text), Strings.LCase(List), CompareMethod.Binary) & !LikeOperator.LikeString(source, "*," + Strings.LCase(text) + ",*", CompareMethod.Binary))) & (Operators.CompareString(Strings.Mid(text6, 1, 2), "--", TextCompare: false) != 0))
								{
									text = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num6].DataType, 2, 1)));
									if ((Operators.CompareString(text, "X", TextCompare: false) == 0) | (Operators.CompareString(text, "", TextCompare: false) == 0))
									{
										text = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num6].DataType, 1, 1)));
										text7 = Strings.Trim(Globals_Renamed.g_QFilters[num6].ParentO);
										text4 = Strings.Trim(Globals_Renamed.g_QFilters[num6].Column);
										if (Strings.InStr(text4, "         : ") != 0)
										{
											text4 = General_Procedures.Strip_Hdr_Colon_Col(text4);
										}
										text12 = Globals_Renamed.g_QFilters[num6].ColHead;
										text8 = Strings.Trim(Globals_Renamed.g_QFilters[num6].Operator_Renamed);
										text3 = Strings.Trim(Globals_Renamed.g_QFilters[num6].Value1);
										text2 = Strings.Trim(Globals_Renamed.g_QFilters[num6].Value2);
										text11 = ((!LikeOperator.LikeString(Strings.UCase(text8), "*BETWEEN*", CompareMethod.Binary) || Strings.InStr(text3, "&PROMPT&") != 0 || (LikeOperator.LikeString(Strings.Replace(text3, "'", "", 1, -1, CompareMethod.Text), "<<<*>>>", CompareMethod.Binary) && (!LikeOperator.LikeString(Strings.Replace(text3, "'", "", 1, -1, CompareMethod.Text), "<<<*>>>", CompareMethod.Binary) || Operators.CompareString(text2, "", TextCompare: false) == 0))) ? "" : " AND ");
										if (Operators.CompareString(Strings.UCase(text3), "&PROMPT&", TextCompare: false) == 0)
										{
											text2 = "";
										}
										text10 = Strings.Trim(Globals_Renamed.g_QFilters[num6].ParenC);
										if (BuildForm.IsValidFilter("2", text4, text8, text3, text2))
										{
											if (Operators.CompareString(Strings.UCase(text8), "IN FILE", TextCompare: false) == 0)
											{
												if ((Operators.CompareString(Strings.Mid(text4, 1, 1), "(", TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(text4, Strings.Len(text4), 1), ")", TextCompare: false) == 0) & (Strings.InStr(text4, ",") != 0) & (((Strings.InStr(Strings.LCase(text4), "test_name,") != 0) & (Strings.InStr(Strings.LCase(text4), "structure_name)") != 0)) | ((Strings.InStr(Strings.LCase(text4), "lot") != 0) & (Strings.InStr(Strings.LCase(text4), "wafer") != 0))))
												{
													text4 = Strings.Mid(text4, 2, Strings.Len(text4) - 2);
													text8 = "IN GROUP(Hdr=N:=;=)";
													text24 = ":1;2";
													text20 = "";
													num5 = (short)Strings.InStr(text4, ",");
													if (num5 != 0)
													{
														text4 = Strings.Mid(text4, 1, num5 - 1) + ";" + Strings.Mid(text4, num5 + 1);
													}
												}
												else
												{
													text8 = "IN GROUP(Hdr=N:In)";
													text24 = ":1";
													text20 = "";
												}
												if (Strings.InStr(text3, "&PROMPT&") == 0)
												{
													text3 += text24;
												}
											}
											else if (Operators.CompareString(Strings.UCase(text8), "LIKE FILE", TextCompare: false) == 0)
											{
												text8 = "IN GROUP(Hdr=N:Like)";
												if (Strings.InStr(text3, "&PROMPT&") == 0)
												{
													text3 += ":1";
												}
												text20 = "";
											}
											if (Strings.InStr(text3, "&PROMPT&") == 0)
											{
												if (Operators.CompareString(Strings.UCase(text8), "IN FILE", TextCompare: false) == 0)
												{
													text8 = "IN";
													text3 = "&PROMPT&(INFILE^[" + text + "]" + text3 + ")&PROMPT&";
												}
												else if (Operators.CompareString(Strings.UCase(text8), "LIKE FILE", TextCompare: false) == 0)
												{
													text8 = "LIKE";
													text3 = "&PROMPT&(ILFILE-" + text4 + "^[" + text + "]" + text3 + ")&PROMPT&";
													text4 = "(" + text4;
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "* GROUP", CompareMethod.Binary) || (Operators.CompareString(Strings.Mid(Strings.UCase(text8), 1, 12), "LIKE GROUP [", TextCompare: false) == 0 && Operators.CompareString(Strings.Mid(text8, 14, 1), "]", TextCompare: false) == 0))
												{
													text14 = ((Operators.CompareString(Strings.Mid(Strings.UCase(text8), 1, 12), "LIKE GROUP [", TextCompare: false) != 0 || Operators.CompareString(Strings.Mid(text8, 13, 1), " ", TextCompare: false) == 0) ? "" : ("@esc@=" + Strings.Mid(text8, 13, 1)));
													num5 = (short)Strings.InStr(text8, " ");
													text8 = Strings.Mid(text8, 1, num5 - 1);
													text19 = "";
													Chk_Temp_Inc("G", ref text3, ref text19);
													if (Operators.CompareString(text19, "", TextCompare: false) != 0)
													{
														text19 = "->" + text19;
													}
													BuildForm.Extract_IG_ColNo(ref text3, ref MyCol);
													text3 = "\r\n" + text20 + "Get_CSV_List(\"" + text3 + text19 + "\", \"" + MyCol + text14 + "\", \"" + Process_Quotes_Filter(text4) + " " + text8 + "\")";
													if (ll_OutInline)
													{
														text3 = Strings.Replace(text3, "\"", "\"\"", 1, -1, CompareMethod.Text);
													}
													text4 = "(" + text4;
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "IN GROUP(*", CompareMethod.Binary))
												{
													num5 = (short)Strings.InStr(text8, ")");
													text8 = Strings.UCase(Strings.Mid(text8, 10, num5 - 10));
													text19 = "";
													Chk_Temp_Inc("G", ref text3, ref text19);
													if (Operators.CompareString(text19, "", TextCompare: false) != 0)
													{
														text19 = "->" + text19;
													}
													BuildForm.Extract_IG_ColNo(ref text3, ref MyCol);
													text3 = "\r\n" + text20 + Strings.Replace("Get_CSV_List_2(\"" + text3 + text19 + "\", \"" + MyCol + "\", \"" + Process_Quotes_Filter(text4) + "\", \"" + text8 + "\")", "\r\n", "", 1, -1, CompareMethod.Text);
													if (ll_OutInline)
													{
														text3 = Strings.Replace(text3, "\"", "\"\"", 1, -1, CompareMethod.Text);
													}
													text4 = "";
													text8 = "";
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "IN TEMP(BT*)", CompareMethod.Binary))
												{
													num5 = (short)Strings.InStr(text8, ")");
													text8 = Strings.UCase(Strings.Mid(text8, 9, num5 - 9));
													if ((short)Strings.InStr(text3, "{") != 0)
													{
														num8 = (short)Strings.InStr(text3, ":");
														text13 = ((num8 == 0) ? text3 : Strings.Mid(text3, 1, num8 - 1));
														text13 = General_Procedures.Get_Obj_Alias("F->T", General_Procedures.Get_Node_Value(text13));
														text3 = Set_TT_Name(text13, ll_DatabaseType);
														if (Strings.InStr(text3, "\\") == 0)
														{
															text3 = ".\\" + text3;
														}
														text13 = Strings.Trim(text13) + "->";
														text2 = Strings.Replace(text2, text13, "", 1, -1, CompareMethod.Text);
													}
													text3 = "\r\n" + text20 + Strings.Replace("Get_CSV_List_2(\"" + text3 + "\", \"" + text2 + "\", \"" + text4 + "\", \"" + text8 + "\")", "\r\n", "", 1, -1, CompareMethod.Text);
													text2 = "";
													text4 = "";
													text8 = "";
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "IN TEMP(*", CompareMethod.Binary))
												{
													num5 = (short)Strings.InStr(text8, ")");
													text8 = Strings.UCase(Strings.Mid(text8, 9, num5 - 9));
													if (Strings.InStr(text8, "=") == 0)
													{
														text8 = "+IN" + text8;
														text8 = Strings.Replace(text8, ";", ";IN", 1, -1, CompareMethod.Text);
													}
													text19 = "";
													Chk_Temp_Inc("T", ref text3, ref text19);
													if (Operators.CompareString(text19, "", TextCompare: false) != 0)
													{
														text19 = "->" + text19;
													}
													if ((short)Strings.InStr(text3, "{") != 0)
													{
														num8 = (short)Strings.InStr(text3, ":");
														text13 = ((num8 == 0) ? text3 : Strings.Mid(text3, 1, num8 - 1));
														text13 = General_Procedures.Get_Obj_Alias("F->T", General_Procedures.Get_Node_Value(text13));
														text3 = Set_TT_Name(text13, ll_DatabaseType);
														if (Strings.InStr(text3, "\\") == 0)
														{
															text3 = ".\\" + text3;
														}
														text3 += text19;
														text13 = Strings.Trim(text13) + "->";
														text2 = Strings.Replace(text2, text13, "", 1, -1, CompareMethod.Text);
													}
													text3 = "\r\n" + text20 + Strings.Replace("Get_CSV_List_2(\"" + text3 + "\", \"" + text2 + "\", \"" + Process_Quotes_Filter(text4) + "\", \"" + text8 + "\")", "\r\n", "", 1, -1, CompareMethod.Text);
													text2 = "";
													text4 = "";
													text8 = "";
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "*TEMP*", CompareMethod.Binary))
												{
													num5 = (short)Strings.InStr(text8, " ");
													if (num5 != 0)
													{
														text8 = Strings.Trim(Strings.Mid(text8, 1, num5 - 1));
													}
													text19 = "";
													Chk_Temp_Inc("T", ref text3, ref text19);
													if (Operators.CompareString(text19, "", TextCompare: false) != 0)
													{
														text19 = "->" + text19;
													}
													if ((short)Strings.InStr(text3, "{") != 0)
													{
														num8 = (short)Strings.InStr(text3, ":");
														text13 = ((num8 == 0) ? text3 : Strings.Mid(text3, 1, num8 - 1));
														text13 = General_Procedures.Get_Obj_Alias("F->T", General_Procedures.Get_Node_Value(text13));
														text3 = Set_TT_Name(text13, ll_DatabaseType);
														if (Strings.InStr(text3, "\\") == 0)
														{
															text3 = ".\\" + text3;
														}
														text3 += text19;
													}
													text13 = "";
													if ((Operators.CompareString(Strings.UCase(Strings.Trim(text8)), "IN", TextCompare: false) == 0) & ((Operators.CompareString(Strings.UCase(text), "N", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(text), "F", TextCompare: false) == 0)))
													{
														text13 = "N";
													}
													else if ((Operators.CompareString(Strings.UCase(Strings.Trim(text8)), "IN", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(text), "D", TextCompare: false) == 0))
													{
														text13 = "D";
													}
													text3 = "\r\n" + text20 + "Get_CSV_List(\"" + text3 + "\", " + text2 + ", \"" + Process_Quotes_Filter(text4) + " " + text8 + text13 + "\")";
													text4 = "(" + text4;
													if (Operators.CompareString(Strings.UCase(text8), "LIKE-R", TextCompare: false) == 0)
													{
														text4 = "(";
														text8 = "";
													}
													text2 = "";
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "*IN*", CompareMethod.Binary))
												{
													text3 = Process_In_List(text3, CBBegin, CBEnd, "S");
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "LIKE LIST*", CompareMethod.Binary))
												{
													text3 = ((Strings.Len(text8) < 13) ? BuildForm.Process_Like_List(text4, text3, "") : BuildForm.Process_Like_List(text4, text3, Strings.Mid(text8, 12, 1)));
													if (Strings.InStr(text3, ">>>%") != 0)
													{
														text3 = Strings.Replace(text3, ">>>%", ">>>", 1, -1, CompareMethod.Text);
													}
													text8 = "LIKE";
													text4 = "(" + text4;
												}
												else if (Operators.CompareString(text, "D", TextCompare: false) == 0 || Operators.CompareString(text, "V", TextCompare: false) == 0)
												{
													if (Information.IsDate(text3))
													{
														text3 = "To_Date('" + text3 + "','dd-Mon-yyyy hh24:mi:ss')";
													}
													if (LikeOperator.LikeString(Strings.UCase(text8), "*BETWEEN*", CompareMethod.Binary) && Information.IsDate(text2))
													{
														text2 = "To_Date('" + text2 + "','dd-Mon-yyyy hh24:mi:ss')";
													}
												}
											}
											else
											{
												text15 = "";
												text17 = "";
												text18 = "";
												text17 = Globals_Renamed.g_QFilters[num6].Value2;
												text5 = text4;
												if (Strings.InStr(text5, "^") != 0)
												{
													text5 = "MyData";
												}
												num5 = (short)Strings.InStr(text17, "^");
												if (num5 != 0)
												{
													text18 = Strings.Trim(Strings.Mid(text17, num5 + 1));
													num8 = ((!General_Procedures.IsDateDT(text)) ? ((short)Strings.InStr(text18, "/")) : ((short)Strings.InStr(text18, "//")));
													if (num8 != 0)
													{
														text15 = Strings.Mid(text18, 1, num8 - 1);
														if (LikeOperator.LikeString(Strings.UCase(Strings.Mid(text18 + " ", num8 + 1)), "*SELECT*FROM*", CompareMethod.Binary))
														{
															text18 = Strings.Trim(Strings.Mid(text18, num8 + 1));
														}
													}
													else if (LikeOperator.LikeString(Strings.Trim(Strings.UCase(text18)), "SELECT*FROM*", CompareMethod.Binary))
													{
														text15 = "";
													}
													else
													{
														text15 = text18;
														text18 = "";
													}
													text17 = Strings.Trim(Strings.Mid(text17, 1, num5 - 1));
												}
												if (Operators.CompareString(Strings.UCase(text8), "IN FILE", TextCompare: false) == 0)
												{
													text8 = "IN";
													text3 = "&PROMPT&(INFILE^[" + text + "]" + text3 + ")&PROMPT&";
												}
												else if (Operators.CompareString(Strings.UCase(text8), "LIKE FILE", TextCompare: false) == 0)
												{
													text8 = "LIKE";
													text3 = "&PROMPT&(ILFILE-" + text4 + "^[" + text + "]" + text3 + ")&PROMPT&";
													text4 = "(" + text4;
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "* GROUP", CompareMethod.Binary))
												{
													text9 = text8;
													num5 = (short)Strings.InStr(text8, " ");
													text8 = Strings.Mid(text8, 1, num5 - 1);
													text3 = string.Concat("\r\n" + text20 + "Get_CSV_List(\"&PROMPT&(" + text16 + text5 + "^" + Strings.UCase(text9) + "^N^" + text17 + "^", "^", text15, ")&PROMPT&\", \"", text4, " ", text8, "\")");
													if (ll_OutInline)
													{
														text3 = Strings.Replace(text3, "\"", "\"\"", 1, -1, CompareMethod.Text);
													}
													text4 = "(" + text4;
												}
												else if (LikeOperator.LikeString(Strings.UCase(text8), "IN GROUP(*", CompareMethod.Binary))
												{
													num5 = (short)Strings.InStr(text8, ")");
													text8 = Strings.UCase(Strings.Mid(text8, 10, num5 - 10));
													text9 = "IN GROUP";
													text3 = string.Concat("\r\n" + text20 + "Get_CSV_List_2(\"&PROMPT&(" + text16 + text5 + "^" + Strings.UCase(text9) + "^N^" + text17 + "^", "^", text15, ")&PROMPT&\", \"", text4, "\", \"", text8, "\")");
													if (ll_OutInline)
													{
														text3 = Strings.Replace(text3, "\"", "\"\"", 1, -1, CompareMethod.Text);
													}
													text4 = "";
													text8 = "";
												}
												else
												{
													if (Operators.CompareString(text18, "", TextCompare: false) != 0)
													{
														text21 = ((!LikeOperator.LikeString(Strings.Trim(Strings.UCase(text18)), "SELECT*FROM*", CompareMethod.Binary)) ? ("~" + text18) : text18);
													}
													else if (ll_ObjectType == 2)
													{
														text21 = ((!ll_OutInline) ? ("?:{" + Conversions.ToString(unchecked((int)ll_TableIndex)) + "}: ") : "");
													}
													else
													{
														num5 = (short)Strings.InStr(text4, ".");
														if (num5 != 0)
														{
															text22 = Strings.Mid(text4, 1, num5 - 1);
															num4 = (short)Math.Round(Conversion.Val(Strings.Trim(Strings.Mid(text22, 2))));
															short num13 = (short)(Globals_Renamed.MaxFromTables - 1);
															for (num7 = 0; num7 <= num13; num7 = (short)unchecked(num7 + 1))
															{
																if ((short)Strings.InStr(Globals_Renamed.FromTables[num7].Table, text22) != 0)
																{
																	text21 = "?:{" + Conversions.ToString(unchecked((int)num4)) + "}:" + MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GetCurrTableName("", "", num7);
																	break;
																}
															}
														}
													}
													if (Operators.CompareString(Strings.UCase(text8), "LIKE LIST", TextCompare: false) == 0)
													{
														text8 = "LIKE";
														text3 = "&PROMPT&(" + text16 + text5 + "^LIKE LIST^" + text + "^" + text17 + "^" + text21 + "^" + text15 + ")&PROMPT&";
														text4 = "(" + text4;
													}
													else
													{
														if (Operators.CompareString(text17, "", TextCompare: false) == 0)
														{
															text17 = text12;
														}
														text3 = "&PROMPT&(" + text16 + text5 + "^" + text8 + "^" + text + "^" + text17 + "^" + text21 + "^" + text15 + ")&PROMPT&";
													}
												}
											}
											if (Operators.CompareString(ll_DatabaseType, "iMBigData", TextCompare: false) == 0)
											{
												num5 = (short)Strings.InStrRev(text25, "___");
												string text26;
												if (num5 != 0)
												{
													text26 = Strings.UCase(Strings.Mid(text25, num5 + 3));
													text26 = " DOMAIN='" + text26 + "' ";
												}
												else
												{
													text26 = " DOMAIN='SORT' ";
												}
												if (Operators.CompareString(text23, "", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(text26), "DOMAIN='KS'", TextCompare: false) == 0)
												{
													text23 = Globals_Renamed.CRLF + "          <KSMaterialSelection";
												}
												else if (Operators.CompareString(text23, "", TextCompare: false) == 0)
												{
													text23 = Globals_Renamed.CRLF + "          <MaterialSelection" + text26;
												}
												if (LikeOperator.LikeString(Strings.UCase(text3), "*GET_CSV_LIST*", CompareMethod.Binary))
												{
													text4 = Strings.Replace(text4, "(", "", 1, -1, CompareMethod.Text);
													text23 = Conversions.ToString(Add_MaterialSelection_Tags(NoWhere != 0, text6, text23));
													num5 = (short)Strings.InStrRev(text3, " In\")");
													if (num5 != 0)
													{
														text3 = Strings.Mid(text3, 1, num5) + "imbigdata-in\")";
													}
													text23 = text23 + " " + text4 + "=" + text3 + "\r\n";
													NoWhere = 0;
												}
												else
												{
													if (Operators.CompareString(Strings.UCase(text8), "IN", TextCompare: false) == 0)
													{
														text3 = Strings.Replace(text3, "(", "", 1, -1, CompareMethod.Text);
														text3 = Strings.Replace(text3, ")", "", 1, -1, CompareMethod.Text);
														text3 = Strings.Replace(text3, "'", "", 1, -1, CompareMethod.Text);
													}
													else if (Operators.CompareString(text8, "=", TextCompare: false) == 0)
													{
														text3 = Strings.Replace(text3, "'", "", 1, -1, CompareMethod.Text);
													}
													switch (General_Procedures.Count_Character(text4, ";"))
													{
													case 0:
														text3 = Strings.Replace(text3, "\r\n", "", 1, -1, CompareMethod.Text);
														text3 = "'" + text3 + "'";
														text23 = Conversions.ToString(Add_MaterialSelection_Tags(NoWhere != 0, text6, text23));
														text23 = text23 + " " + text4 + "=" + text3;
														NoWhere = 0;
														break;
													case 1:
													{
														num5 = (short)Strings.InStr(text4, ";");
														text2 = Strings.Trim(Strings.Mid(text4, 1, num5 - 1));
														text10 = Strings.Trim(Strings.Mid(text4, num5 + 1));
														string[] array = Strings.Split(text3, "\r\n");
														num10 = -1;
														short num17 = (short)Information.UBound(array);
														for (num7 = 0; num7 <= num17; num7 = (short)unchecked(num7 + 1))
														{
															array[num7] = Strings.Trim(array[num7]);
															if (Operators.CompareString(Strings.Mid(array[num7], 1, 1), ",", TextCompare: false) == 0)
															{
																array[num7] = Strings.Trim(Strings.Mid(array[num7] + " ", 2));
															}
															if (Operators.CompareString(array[num7], "", TextCompare: false) != 0 && Operators.CompareString(array[num7], ",", TextCompare: false) != 0)
															{
																num11 = General_Procedures.Count_Character(array[num7], ",");
																text9 = "";
																text = "";
																num10++;
																if (num10 != 0 || (num10 == 0 && Strings.InStr(Strings.Mid(text23, Strings.Len(text23) - 2), "/>") != 0) || (num10 == 0 && NoWhere == 0 && Operators.CompareString(text6, "OR", TextCompare: false) == 0))
																{
																	text23 += "\r\n         <MaterialSelection";
																}
																num5 = (short)Strings.InStr(array[num7], ",");
																if (num11 == 0)
																{
																	text9 = " " + text2 + "='" + array[num7] + "'/>";
																	text = "";
																}
																else
																{
																	text9 = " " + text2 + "= '" + Strings.Trim(Strings.Mid(array[num7], 1, num5 - 1)) + "'";
																	if (num5 == Strings.Len(array[num7]))
																	{
																		text9 += "/>";
																		text = "";
																	}
																	else
																	{
																		text = " " + text10 + "='" + Strings.Trim(Strings.Mid(array[num7], num5 + 1)) + "'/>";
																	}
																}
																text23 = text23 + text9 + text;
																NoWhere = 0;
															}
														}
														array = null;
														break;
													}
													case 3:
													{
														if (!LikeOperator.LikeString(text4, "*LOT;WAFERS;DIE;DIE*", CompareMethod.Binary))
														{
															break;
														}
														string[] array = Strings.Split(text3, "\r\n");
														num10 = -1;
														short num14 = (short)Information.UBound(array);
														string[] array2;
														for (num7 = 0; num7 <= num14; num7 = (short)unchecked(num7 + 1))
														{
															array[num7] = Strings.Trim(array[num7]);
															if (Operators.CompareString(Strings.Mid(array[num7], 1, 1), ",", TextCompare: false) == 0)
															{
																array[num7] = Strings.Mid(array[num7], 2);
															}
															if (General_Procedures.Count_Character(array[num7], ",") == 3)
															{
																array2 = Strings.Split(array[num7], ",");
																int num15 = Information.UBound(array2);
																for (num3 = 0; num3 <= num15; num3++)
																{
																	array2[num3] = Strings.Trim(array2[num3]);
																}
																if (Operators.CompareString(array2[0], "", TextCompare: false) != 0 && ((Operators.CompareString(array2[2], "", TextCompare: false) == 0 && Operators.CompareString(array2[3], "", TextCompare: false) == 0) || (Operators.CompareString(array2[3], "", TextCompare: false) != 0 && Operators.CompareString(array2[2], "", TextCompare: false) != 0)))
																{
																	int num16 = Information.UBound(array2) - 1;
																	for (num3 = 0; num3 <= num16; num3++)
																	{
																		if (Operators.CompareString(array2[num3], "", TextCompare: false) == 0)
																		{
																			continue;
																		}
																		switch (num3)
																		{
																		case 0:
																			num10++;
																			if (num10 != 0 || (num10 == 0 && Strings.InStr(Strings.Mid(text23, Strings.Len(text23) - 2), "/>") != 0) || (num10 == 0 && NoWhere == 0 && Operators.CompareString(text6, "OR", TextCompare: false) == 0))
																			{
																				text23 += "\r\n         <MaterialSelection";
																			}
																			text23 = text23 + " LOT='" + array2[num3] + "'";
																			NoWhere = 0;
																			break;
																		case 1:
																			text23 = text23 + " WAFERS='" + array2[num3] + "'";
																			break;
																		case 2:
																			text23 = text23 + " DIE='" + array2[num3] + "," + array2[num3 + 1] + "'";
																			break;
																		}
																	}
																	text23 += "/>";
																}
															}
														}
														array = null;
														array2 = null;
														break;
													}
													}
												}
											}
											else if (NoWhere == -1)
											{
												NoWhere = 0;
												text23 = ((CR != 1) ? (text23 + CBBegin + "WHERE" + CBEnd) : (text23 + Globals_Renamed.CRLF + CBBegin + "WHERE" + CBEnd));
												text6 = ((!LikeOperator.LikeString(text6, "*NOT*", CompareMethod.Binary)) ? "         " : " NOT     ");
												text23 = text23 + Globals_Renamed.CRLF + CBBegin + text6 + "     " + text7 + text4 + " " + text8 + " " + text3 + text11 + text2 + " " + text10 + CBEnd;
											}
											else
											{
												switch (text6)
												{
												case null:
												case "":
													text6 = "AND";
													break;
												case "OR":
													text6 = "OR ";
													break;
												}
												if (Operators.CompareString(text23, "", TextCompare: false) != 0)
												{
													text23 += Globals_Renamed.CRLF;
												}
												text23 = text23 + CBBegin + " " + text6 + "      " + text7 + text4 + " " + text8 + " " + text3 + text11 + text2 + " " + text10 + CBEnd;
											}
										}
									}
								}
							}
						}
						if (Operators.CompareString(ll_DatabaseType, "iMBigData", TextCompare: false) == 0 && Strings.Len(text23) >= 2 && Strings.InStr(Strings.Mid(text23, Strings.Len(text23) - 2), "/>") == 0)
						{
							text23 += "/>";
						}
						result = text23;
						goto end_IL_0001;
					}
				}
				case 9132:
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
				goto IL_23e2;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 9132;
				continue;
			}
			break;
			IL_23e2:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static object Add_MaterialSelection_Tags(bool NoWhere, string AND_OR, string SQLStr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		object result = default(object);
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
				case 260:
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
							goto IL_0028;
						case 4:
							goto IL_0060;
						case 5:
						case 6:
						case 7:
							goto IL_0073;
						case 8:
							goto IL_00ab;
						case 9:
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0028:
					num2 = 3;
					if (Strings.InStr(Strings.Mid("  " + SQLStr, checked(Strings.Len("  " + SQLStr) - 2)), "/>") == 0)
					{
						goto IL_0060;
					}
					goto IL_0073;
					IL_0060:
					num2 = 4;
					SQLStr += "/>";
					goto IL_0073;
					IL_00ab:
					num2 = 8;
					SQLStr += "\r\n         <MaterialSelection";
					break;
					IL_0073:
					num2 = 7;
					if (Strings.InStr(Strings.Mid("  " + SQLStr, checked(Strings.Len("  " + SQLStr) - 2)), "/>") == 0)
					{
						break;
					}
					goto IL_00ab;
					IL_000b:
					num2 = 2;
					if (!NoWhere && Operators.CompareString(AND_OR, "OR", TextCompare: false) == 0)
					{
						goto IL_0028;
					}
					goto IL_0073;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				result = SQLStr;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 260;
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

	public static string Set_SQLite_Hdr(string MyInHdr)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 148:
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
							goto IL_0015;
						case 4:
							goto IL_0019;
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
					MyInHdr = Strings.Trim(MyInHdr);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					result = MyInHdr;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (!((Operators.CompareString(MyInHdr, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Mid(MyInHdr + " ", 1, 1), "[", TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = "[" + MyInHdr + "]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 148;
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

	public static string Set_Hadoop_Hdr(string MyInHdr, string MyBound)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
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
				case 136:
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
							goto IL_0015;
						case 4:
							goto IL_0019;
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
					MyInHdr = Strings.Trim(MyInHdr);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					result = MyInHdr;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (!((Operators.CompareString(MyInHdr, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Mid(MyInHdr + " ", 1, 1), MyBound, TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = MyBound + MyInHdr + MyBound;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 136;
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

	public static bool Check_Hbase_Special(ref string[,] l_Hbase, string in_alias)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		bool result = default(bool);
		int num = default(int);
		int num3 = default(int);
		int num5 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					result = false;
					goto IL_0006;
				case 199:
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
							goto IL_0014;
						case 5:
							goto IL_0019;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0055;
						case 8:
							goto end_IL_0001_2;
						case 11:
						case 12:
							goto IL_0070;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0035:
					num2 = 6;
					if (Operators.CompareString(l_Hbase[num5, 1], "N", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0055;
					IL_0055:
					num2 = 7;
					l_Hbase[num5, 1] = "Y";
					break;
					IL_0019:
					num2 = 5;
					if (Operators.CompareString(in_alias, l_Hbase[num5, 0], TextCompare: false) == 0)
					{
						goto IL_0035;
					}
					goto IL_0070;
					IL_0070:
					num2 = 12;
					num5 = checked(num5 + 1);
					if (num5 > 4)
					{
						goto end_IL_0001_3;
					}
					goto IL_0019;
					IL_0006:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num5 = 0;
					goto IL_0019;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 199;
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

	public static string TestSummaryorPivotQ_Grid(string ll_MajorAlias)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string result = default(string);
		int num = default(int);
		int num3 = default(int);
		string left = default(string);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		int num7 = default(int);
		string text2 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					result = "";
					goto IL_000a;
				case 523:
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
							goto IL_000a;
						case 3:
							goto IL_0013;
						case 4:
							goto IL_0038;
						case 5:
							goto IL_0046;
						case 6:
							goto IL_0085;
						case 7:
							goto IL_00c5;
						case 8:
							goto IL_00d1;
						case 9:
							goto IL_0104;
						case 10:
							goto IL_0144;
						case 11:
							goto IL_019b;
						case 12:
						case 13:
						case 14:
							goto IL_01a8;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0104:
					num2 = 9;
					left = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[4].Value);
					goto IL_0144;
					IL_0144:
					num2 = 10;
					if ((Operators.CompareString(Strings.Mid(text, 1, 1), "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text, 1, 1), "P", TextCompare: false) == 0 || Operators.CompareString(text, "N:S", TextCompare: false) == 0) && Operators.CompareString(left, "None", TextCompare: false) != 0)
					{
						goto IL_019b;
					}
					goto IL_01a8;
					IL_01a8:
					num2 = 14;
					num5 = checked(num5 + 1);
					goto IL_01b1;
					IL_019b:
					num2 = 11;
					result = "S";
					goto IL_01a8;
					IL_000a:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num6 = checked(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
					goto IL_0038;
					IL_0038:
					num2 = 4;
					num7 = num6;
					num5 = 0;
					goto IL_01b1;
					IL_01b1:
					if (num5 > num7)
					{
						goto end_IL_0001_2;
					}
					goto IL_0046;
					IL_0046:
					num2 = 5;
					text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[2].Value);
					goto IL_0085;
					IL_0085:
					num2 = 6;
					text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[11].Value);
					goto IL_00c5;
					IL_00c5:
					num2 = 7;
					text2 = General_Procedures.Strip_Alias(1, text2);
					goto IL_00d1;
					IL_00d1:
					num2 = 8;
					if (Operators.CompareString(text, "--", TextCompare: false) != 0 && Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2), TextCompare: false) == 0)
					{
						goto IL_0104;
					}
					goto IL_01a8;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 523;
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

	public static void VerifyValidIncQuery_v2(ref string MyError, ref bool isValid, string ll_MajorAlias)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string expression = default(string);
		string text = default(string);
		string MyValue = default(string);
		int num5 = default(int);
		string MyIncrement = default(string);
		string text2 = default(string);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		string text3 = default(string);
		string text4 = default(string);
		string value = default(string);
		string myMode = default(string);
		string left = default(string);
		int num9 = default(int);
		string[] array = default(string[]);
		int num10 = default(int);
		int num11 = default(int);
		string left2 = default(string);
		int num12 = default(int);
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
					object right;
					Type typeFromHandle2;
					object[] obj3;
					DataGridViewCell dataGridViewCell;
					object[] array2;
					bool[] obj4;
					bool[] array3;
					object obj5;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 2559:
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
								goto IL_002b;
							case 7:
								goto IL_0034;
							case 8:
								goto IL_0039;
							case 9:
								goto IL_005d;
							case 10:
								goto IL_006b;
							case 11:
								goto IL_00ab;
							case 12:
								goto IL_00ec;
							case 13:
								goto IL_00f9;
							case 14:
								goto IL_0160;
							case 15:
								goto IL_01a0;
							case 16:
								goto IL_01e0;
							case 17:
								goto IL_01ed;
							case 18:
								goto IL_0206;
							case 19:
								goto IL_0212;
							case 20:
								goto IL_0247;
							case 21:
								goto IL_0262;
							case 23:
								goto IL_0270;
							case 22:
							case 24:
							case 25:
								goto IL_027b;
							case 26:
								goto IL_0285;
							case 27:
								goto IL_0294;
							case 28:
								goto IL_02ad;
							case 29:
								goto IL_02b6;
							case 31:
								goto IL_02c6;
							case 32:
							case 33:
							case 34:
							case 35:
								goto IL_02dd;
							case 30:
							case 36:
								goto IL_02ef;
							case 37:
								goto IL_02fd;
							case 39:
								goto IL_0309;
							case 40:
								goto IL_031a;
							case 41:
								goto IL_0325;
							case 42:
								goto IL_033e;
							case 44:
								goto IL_034a;
							case 45:
								goto IL_0363;
							case 47:
								goto IL_0370;
							case 48:
								goto IL_03a4;
							case 49:
								goto IL_03bd;
							case 51:
							case 52:
								goto IL_03ca;
							case 53:
								goto IL_03f0;
							case 54:
								goto IL_0403;
							case 55:
								goto IL_0418;
							case 56:
								goto IL_0427;
							case 57:
								goto IL_0440;
							case 59:
								goto IL_044e;
							case 58:
							case 60:
							case 61:
								goto IL_0459;
							case 62:
								goto IL_045f;
							case 63:
								goto IL_046e;
							case 64:
								goto IL_04ae;
							case 65:
								goto IL_04ef;
							case 66:
								goto IL_0530;
							case 67:
								goto IL_053d;
							case 68:
								goto IL_05a6;
							case 69:
								goto IL_05e6;
							case 70:
								goto IL_0602;
							case 72:
								goto IL_06af;
							case 73:
								goto IL_06cd;
							case 75:
								goto IL_0762;
							case 76:
								goto IL_07a3;
							case 71:
							case 74:
							case 77:
							case 78:
							case 79:
								goto IL_07b7;
							case 80:
								goto IL_07cd;
							case 81:
								goto IL_07eb;
							case 84:
								goto IL_07f7;
							case 83:
							case 86:
							case 87:
							case 88:
							case 89:
								goto IL_0804;
							case 82:
							case 85:
							case 90:
								goto IL_0816;
							case 91:
								goto IL_0824;
							case 93:
							case 94:
								goto IL_0832;
							case 97:
								goto IL_0849;
							case 98:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 38:
							case 43:
							case 46:
							case 50:
							case 92:
							case 95:
							case 96:
							case 99:
							case 100:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_02c6:
						num2 = 31;
						expression = Strings.Trim(Strings.UCase(text));
						goto IL_02dd;
						IL_0762:
						num2 = 75;
						MyValue = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[11].Value);
						goto IL_07a3;
						IL_02dd:
						num2 = 35;
						num5++;
						goto IL_02e6;
						IL_07a3:
						num2 = 76;
						MyValue = Strings.UCase(General_Procedures.Strip_Alias(0, MyValue));
						goto IL_07b7;
						IL_000b:
						num2 = 2;
						MyIncrement = "";
						goto IL_0014;
						IL_0014:
						num2 = 3;
						text = "";
						goto IL_001d;
						IL_001d:
						num2 = 4;
						text2 = "";
						goto IL_0026;
						IL_0026:
						num2 = 5;
						num6 = 0;
						goto IL_002b;
						IL_002b:
						num2 = 6;
						expression = "";
						goto IL_0034;
						IL_0034:
						num2 = 7;
						isValid = true;
						goto IL_0039;
						IL_0039:
						num2 = 8;
						num7 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.RowCount - 1;
						goto IL_005d;
						IL_005d:
						num2 = 9;
						num8 = num7;
						num5 = 0;
						goto IL_02e6;
						IL_02e6:
						if (num5 <= num8)
						{
							goto IL_006b;
						}
						goto IL_02ef;
						IL_006b:
						num2 = 10;
						text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num5].Cells[0].Value);
						goto IL_00ab;
						IL_00ab:
						num2 = 11;
						text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num5].Cells[10].Value);
						goto IL_00ec;
						IL_00ec:
						num2 = 12;
						text4 = General_Procedures.Strip_Alias(1, text);
						goto IL_00f9;
						IL_00f9:
						num2 = 13;
						if (Operators.CompareString(Strings.Mid(text3 + "  ", 1, 2), "--", TextCompare: false) != 0 && (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text4), TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text4) + "->", TextCompare: false) == 0))
						{
							goto IL_0160;
						}
						goto IL_02dd;
						IL_07b7:
						num2 = 79;
						if (Operators.CompareString(MyValue, text2, TextCompare: false) == 0)
						{
							goto IL_07cd;
						}
						goto IL_0804;
						IL_07cd:
						num2 = 80;
						if (Operators.CompareString(Strings.UCase(value), "NONE", TextCompare: false) == 0)
						{
							goto IL_07eb;
						}
						goto IL_07f7;
						IL_0160:
						num2 = 14;
						value = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num5].Cells[3].Value);
						goto IL_01a0;
						IL_01a0:
						num2 = 15;
						MyValue = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num5].Cells[4].Value);
						goto IL_01e0;
						IL_01e0:
						num2 = 16;
						text = General_Procedures.Strip_Alias(0, text);
						goto IL_01ed;
						IL_01ed:
						num2 = 17;
						if (Strings.InStr(text, "         : ") != 0)
						{
							goto IL_0206;
						}
						goto IL_0212;
						IL_0206:
						num2 = 18;
						text = General_Procedures.Strip_Hdr_Colon_Col(text);
						goto IL_0212;
						IL_0212:
						num2 = 19;
						if (LikeOperator.LikeString(Strings.UCase(value), "*GROUP*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(value), "*TEMP*", CompareMethod.Binary))
						{
							goto IL_0247;
						}
						goto IL_02dd;
						IL_07eb:
						num2 = 81;
						isValid = true;
						goto IL_0816;
						IL_0247:
						num2 = 20;
						if (LikeOperator.LikeString(Strings.UCase(value), "*GROUP*", CompareMethod.Binary))
						{
							goto IL_0262;
						}
						goto IL_0270;
						IL_0262:
						num2 = 21;
						myMode = "G";
						goto IL_027b;
						IL_0270:
						num2 = 23;
						myMode = "T";
						goto IL_027b;
						IL_027b:
						num2 = 25;
						MyIncrement = "";
						goto IL_0285;
						IL_0285:
						num2 = 26;
						Chk_Temp_Inc(myMode, ref MyValue, ref MyIncrement);
						goto IL_0294;
						IL_0294:
						num2 = 27;
						if (Operators.CompareString(MyIncrement, "", TextCompare: false) != 0)
						{
							goto IL_02ad;
						}
						goto IL_02dd;
						IL_02ad:
						num2 = 28;
						num6++;
						goto IL_02b6;
						IL_02b6:
						num2 = 29;
						if (num6 <= 1)
						{
							goto IL_02c6;
						}
						goto IL_02ef;
						IL_02ef:
						num2 = 36;
						if (num6 == 0)
						{
							goto IL_02fd;
						}
						goto IL_0309;
						IL_02fd:
						num2 = 37;
						isValid = true;
						goto end_IL_0001_3;
						IL_0309:
						num2 = 39;
						if (num6 == 1)
						{
							goto IL_031a;
						}
						goto IL_0849;
						IL_031a:
						num2 = 40;
						left = TestSummaryorPivotQ_Grid(ll_MajorAlias);
						goto IL_0325;
						IL_0325:
						num2 = 41;
						if (Operators.CompareString(left, "", TextCompare: false) == 0)
						{
							goto IL_033e;
						}
						goto IL_034a;
						IL_033e:
						num2 = 42;
						isValid = true;
						goto end_IL_0001_3;
						IL_034a:
						num2 = 44;
						if (Operators.CompareString(left, "P", TextCompare: false) == 0)
						{
							goto IL_0363;
						}
						goto IL_0370;
						IL_0363:
						num2 = 45;
						isValid = false;
						goto end_IL_0001_3;
						IL_0370:
						num2 = 47;
						text = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "IGNOREINCSUM:" + ll_MajorAlias));
						goto IL_03a4;
						IL_03a4:
						num2 = 48;
						if (Operators.CompareString(text, "Yes", TextCompare: false) == 0)
						{
							goto IL_03bd;
						}
						goto IL_03ca;
						IL_03bd:
						num2 = 49;
						isValid = true;
						goto end_IL_0001_3;
						IL_03ca:
						num2 = 52;
						num9 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1;
						goto IL_03f0;
						IL_03f0:
						num2 = 53;
						array = Strings.Split(expression, ";");
						goto IL_0403;
						IL_0403:
						num2 = 54;
						num10 = Information.UBound(array);
						num11 = 0;
						goto IL_083b;
						IL_083b:
						if (num11 > num10)
						{
							goto end_IL_0001_3;
						}
						goto IL_0418;
						IL_0418:
						num2 = 55;
						text2 = Strings.Trim(array[num11]);
						goto IL_0427;
						IL_0427:
						num2 = 56;
						if (Strings.InStr(text2, "{") != 0)
						{
							goto IL_0440;
						}
						goto IL_044e;
						IL_0440:
						num2 = 57;
						left2 = "I";
						goto IL_0459;
						IL_044e:
						num2 = 59;
						left2 = "C";
						goto IL_0459;
						IL_0459:
						num2 = 61;
						isValid = false;
						goto IL_045f;
						IL_045f:
						num2 = 62;
						num12 = num9;
						num5 = 0;
						goto IL_080d;
						IL_080d:
						if (num5 <= num12)
						{
							goto IL_046e;
						}
						goto IL_0816;
						IL_046e:
						num2 = 63;
						text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[2].Value);
						goto IL_04ae;
						IL_04ae:
						num2 = 64;
						text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[10].Value);
						goto IL_04ef;
						IL_04ef:
						num2 = 65;
						text4 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[11].Value);
						goto IL_0530;
						IL_0530:
						num2 = 66;
						text4 = General_Procedures.Strip_Alias(1, text4);
						goto IL_053d;
						IL_053d:
						num2 = 67;
						if (Operators.CompareString(Strings.UCase(ll_MajorAlias), Strings.UCase(text4), TextCompare: false) == 0 && (Operators.CompareString(Strings.Mid(text3, 1, 1), "Y", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(text3, 1, 1), "P", TextCompare: false) == 0 || Operators.CompareString(text3, "N:S", TextCompare: false) == 0) && General_Procedures.SummPlusLevel(text) == 0)
						{
							goto IL_05a6;
						}
						goto IL_0804;
						IL_07f7:
						num2 = 84;
						isValid = false;
						goto IL_0816;
						IL_0824:
						num2 = 91;
						MyError = "For Incremental queries, each incremental filter field must be part of the GROUP BY portion of a summary query (i.e., each field should have a \"Show\" value of Y or N:S) and should **NOT** have a summary function assigned in the \"Statistic\" cell of the COLUMNS TAB";
						goto end_IL_0001_3;
						IL_0832:
						num2 = 94;
						num11++;
						goto IL_083b;
						IL_0816:
						num2 = 90;
						if (!isValid)
						{
							goto IL_0824;
						}
						goto IL_0832;
						IL_05a6:
						num2 = 68;
						value = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[4].Value);
						goto IL_05e6;
						IL_05e6:
						num2 = 69;
						if (Operators.CompareString(left2, "I", TextCompare: false) == 0)
						{
							goto IL_0602;
						}
						goto IL_06af;
						IL_0602:
						num2 = 70;
						typeFromHandle = typeof(Strings);
						obj = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[12]).Value };
						array2 = obj;
						obj2 = new bool[1] { true };
						array3 = obj2;
						right = NewLateBinding.LateGet(null, typeFromHandle, "UCase", obj, null, null, obj2);
						if (array3[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
						}
						MyValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("{", right), "}"));
						goto IL_07b7;
						IL_0849:
						num2 = 97;
						isValid = false;
						break;
						IL_0804:
						num2 = 89;
						num5++;
						goto IL_080d;
						IL_06af:
						num2 = 72;
						if (LikeOperator.LikeString(Strings.UCase(text), "@IF@*", CompareMethod.Binary))
						{
							goto IL_06cd;
						}
						goto IL_0762;
						IL_06cd:
						num2 = 73;
						typeFromHandle2 = typeof(Strings);
						obj3 = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num5].Cells[1]).Value };
						array2 = obj3;
						obj4 = new bool[1] { true };
						array3 = obj4;
						obj5 = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", obj3, null, null, obj4);
						if (array3[0])
						{
							dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
						}
						MyValue = Conversions.ToString(obj5);
						goto IL_07b7;
						end_IL_0001_2:
						break;
					}
					num2 = 98;
					MyError = "You are only allowed one incremental filter per query. This query has at least 2.";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj6) when (obj6 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj6);
				try0001_dispatch = 2559;
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

	public static void Populate_Query_Array_Check_Joins(string ll_MajorAlias, int LastGridRow, ref string MyMsg)
	{
		int num = 0;
		checked
		{
			int num2 = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.RowCount - 1;
			for (int i = 0; i <= num2; i++)
			{
				string text = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[i].Cells[2].Value));
				string text2 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[i].Cells[4].Value));
				int num3 = Strings.InStr(text, ":");
				if (num3 != 0)
				{
					text = Strings.Trim(Strings.Mid(text, num3 + 1));
				}
				num3 = Strings.InStr(text2, ":");
				if (num3 != 0)
				{
					text2 = Strings.Trim(Strings.Mid(text2, num3 + 1));
				}
				if (!((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(text2, "", TextCompare: false) != 0)))
				{
					continue;
				}
				string text3 = General_Procedures.Strip_Alias(1, text);
				string text4 = General_Procedures.Strip_Alias(1, text2);
				if (!((Operators.CompareString(text3, "", TextCompare: false) != 0) & (Operators.CompareString(text4, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.LCase(text3), Strings.LCase(text4), TextCompare: false) != 0)))
				{
					continue;
				}
				string text5 = ((Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text3), TextCompare: false) == 0) ? Strings.LCase(General_Procedures.Strip_Alias(0, text)) : ((Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text4), TextCompare: false) != 0) ? "" : Strings.LCase(General_Procedures.Strip_Alias(0, text2))));
				if (!((Operators.CompareString(text5, "", TextCompare: false) != 0) & LikeOperator.LikeString(text5, "*{*}*", CompareMethod.Binary)))
				{
					continue;
				}
				text5 = General_Procedures.Get_Node_Value(text5);
				int g_NoQColumns = Globals_Renamed.g_NoQColumns;
				for (int j = 0; j <= g_NoQColumns; j++)
				{
					if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[j].ColID), text5, TextCompare: false) != 0)
					{
						continue;
					}
					if (Operators.CompareString(Globals_Renamed.g_QColumns[j].Show, "N", TextCompare: false) != 0)
					{
						break;
					}
					Globals_Renamed.g_QColumns[j].Show = "Y";
					for (int k = 0; k <= LastGridRow; k++)
					{
						if (Operators.ConditionalCompareObjectEqual(Globals_Renamed.g_QColumns[j].ColID, MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[k].Cells[12].Value, TextCompare: false) && Operators.ConditionalCompareObjectEqual(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[k].Cells[2].Value, "N", TextCompare: false))
						{
							MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[k].Cells[2].Value = "P";
							if (num == 0)
							{
								MyMsg = MyMsg + "\r\nFor Alias: " + ll_MajorAlias + ", changed the Show Field in the following rows of the Columns Grid from N to P since they are used in the Join:";
							}
							MyMsg = Conversions.ToString(Operators.ConcatenateObject(MyMsg, Operators.ConcatenateObject("\r\n  Row: " + Conversion.Str(RuntimeHelpers.GetObjectValue(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[k].HeaderCell.Value)) + ", Column: ", MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[k].Cells[0].Value)));
							num++;
							break;
						}
					}
					break;
				}
			}
		}
	}

	public static void Populate_Query_Arrays_v2(string ll_MajorAlias, bool ll_UnionSort, short ll_ObjectType, string MyMode, string ll_DatabaseType, string ValidIncQueryAgg, string l_Special_HBASE = "")
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
						errsource = "BuildSQL - Populate_Query_Arrays_v2";
						string[,] l_Hbase = new string[5, 2];
						string[] array = null;
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						short num6 = 0;
						short num7 = 0;
						short num8 = 0;
						short num9 = 0;
						short num10 = 0;
						short num11 = 0;
						short num12 = 0;
						bool flag = false;
						bool flag2 = false;
						int num13 = 0;
						string text4 = "";
						string text5 = "";
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						string text10 = "";
						string text11 = "";
						string text12 = "";
						string text13 = "";
						string text14 = "";
						string text15 = "";
						string text16 = "";
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						string text17 = "";
						string text18 = "";
						string text19 = "";
						string text20 = "";
						string text21 = "";
						bool flag6 = false;
						string MyMsg = "";
						num10 = 0;
						do
						{
							Globals_Renamed.g_QColumns[num10].No = "";
							Globals_Renamed.g_QColumns[num10].ColAlias = "";
							Globals_Renamed.g_QColumns[num10].Header = "";
							Globals_Renamed.g_QColumns[num10].Show = "";
							Globals_Renamed.g_QColumns[num10].Sort = "";
							Globals_Renamed.g_QColumns[num10].Statistics = "";
							Globals_Renamed.g_QColumns[num10].Pivot = "";
							Globals_Renamed.g_QColumns[num10].level = "";
							Globals_Renamed.g_QColumns[num10].DataType = "";
							Globals_Renamed.g_QColumns[num10].Comment = "";
							Globals_Renamed.g_QColumns[num10].List = "";
							Globals_Renamed.g_QColumns[num10].Column = "";
							Globals_Renamed.g_QColumns[num10].ColID = "";
							Globals_Renamed.g_QColumns[num10].AllStat = "";
							Globals_Renamed.g_QColumns[num10].LHeader = "";
							num10 = (short)unchecked(num10 + 1);
						}
						while (num10 <= 1999);
						Globals_Renamed.g_NoQColumns = -1;
						num10 = 0;
						do
						{
							Globals_Renamed.g_QFilters[num10].And_Renamed = "";
							Globals_Renamed.g_QFilters[num10].ParentO = "";
							Globals_Renamed.g_QFilters[num10].Column = "";
							Globals_Renamed.g_QFilters[num10].Operator_Renamed = "";
							Globals_Renamed.g_QFilters[num10].Value1 = "";
							Globals_Renamed.g_QFilters[num10].Value2 = "";
							Globals_Renamed.g_QFilters[num10].ParenC = "";
							Globals_Renamed.g_QFilters[num10].level = "";
							Globals_Renamed.g_QFilters[num10].Key = "";
							Globals_Renamed.g_QFilters[num10].DataType = "";
							Globals_Renamed.g_QFilters[num10].List = "";
							Globals_Renamed.g_QFilters[num10].ColHead = "";
							num10 = (short)unchecked(num10 + 1);
						}
						while (num10 <= 999);
						Globals_Renamed.g_NoQFilters = -1;
						num10 = 0;
						do
						{
							Globals_Renamed.g_QJoins[num10].And_Renamed = "";
							Globals_Renamed.g_QJoins[num10].ParenO = "";
							Globals_Renamed.g_QJoins[num10].Col1 = "";
							Globals_Renamed.g_QJoins[num10].Join_Renamed = "";
							Globals_Renamed.g_QJoins[num10].Col2 = "";
							Globals_Renamed.g_QJoins[num10].ParenC = "";
							num10 = (short)unchecked(num10 + 1);
						}
						while (num10 <= 999);
						Globals_Renamed.g_NoQJoins = -1;
						num10 = 0;
						do
						{
							num9 = 0;
							do
							{
								l_Hbase[num10, num9] = "";
								num9 = (short)unchecked(num9 + 1);
							}
							while (num9 <= 1);
							num10 = (short)unchecked(num10 + 1);
						}
						while (num10 <= 4);
						if (Operators.CompareString(l_Special_HBASE, "", TextCompare: false) != 0)
						{
							l_Special_HBASE += ",";
							array = Strings.Split(l_Special_HBASE, ",");
							num9 = -1;
							short num14 = (short)Information.UBound(array);
							for (num10 = 0; num10 <= num14; num10 = (short)unchecked(num10 + 1))
							{
								if (Operators.CompareString(Strings.Trim(array[num10]), "", TextCompare: false) != 0)
								{
									num9++;
									l_Hbase[num9, 0] = Strings.Trim(array[num10]);
									l_Hbase[num9, 1] = "N";
								}
							}
						}
						array = null;
						num7 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.RowCount - 1);
						if (Operators.CompareString(Strings.LCase(ll_MajorAlias), "all", TextCompare: false) == 0)
						{
							ll_MajorAlias = "All->";
						}
						if (Operators.CompareString(ll_MajorAlias, "All->", TextCompare: false) != 0 && (Operators.CompareString(ValidIncQueryAgg, "", TextCompare: false) == 0 || Operators.CompareString(ValidIncQueryAgg, "RAW", TextCompare: false) == 0))
						{
							num9 = -1;
							short num15 = num7;
							for (num10 = 0; num10 <= num15; num10 = (short)unchecked(num10 + 1))
							{
								text11 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value);
								text21 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[10].Value);
								object[] array2;
								DataGridViewCell dataGridViewCell;
								bool[] array3;
								object obj = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array2 = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[4]).Value }, null, null, array3 = new bool[1] { true });
								if (array3[0])
								{
									dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
								}
								text19 = Conversions.ToString(obj);
								flag6 = false;
								if (Operators.CompareString(text19, "expr", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(Strings.Mid(text21 + "  ", 1, 2)), "s+", TextCompare: false) == 0)
								{
									flag6 = true;
								}
								if (Operators.CompareString(text11, "--", TextCompare: false) == 0)
								{
									continue;
								}
								switch (ValidIncQueryAgg)
								{
								case "RAW":
									if (flag6)
									{
										continue;
									}
									break;
								case null:
								case "":
									break;
								default:
									continue;
								}
								text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value);
								text2 = General_Procedures.Strip_Alias(1, text2);
								if (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2), TextCompare: false) != 0)
								{
									continue;
								}
								num9++;
								Globals_Renamed.g_QColumns[num9].Column = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value);
								Globals_Renamed.g_QColumns[num9].Column = General_Procedures.Strip_Alias(0, Globals_Renamed.g_QColumns[num9].Column);
								Globals_Renamed.g_QColumns[num9].No = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].HeaderCell.Value);
								Globals_Renamed.g_QColumns[num9].Header = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Value);
								if (BuildForm.IsColPattern(Globals_Renamed.g_QColumns[num9].Column))
								{
									if (Strings.InStr(Strings.LCase(Globals_Renamed.g_QColumns[num9].Column), ".column-pattern") != 0)
									{
										num4 = Strings.InStr(Globals_Renamed.g_QColumns[num9].Column, ".");
										if (num4 != 0)
										{
											Globals_Renamed.g_QColumns[num9].Column = Strings.Mid(Globals_Renamed.g_QColumns[num9].Column, num4 + 1);
										}
									}
									if (Strings.InStr(Strings.LCase(Globals_Renamed.g_QColumns[num9].Column), "<;>duckdb:") != 0)
									{
										Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, "<;>DUCKDB:", "<;>", 1, -1, CompareMethod.Text);
									}
									Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, "]]", "<;>" + Globals_Renamed.g_QColumns[num9].Header + "]]");
									Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, ".", "<dot>", 1, -1, CompareMethod.Text);
									Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, ",", "<comma>", 1, -1, CompareMethod.Text);
									Globals_Renamed.g_QColumns[num9].Header = "";
								}
								else if (Operators.CompareString(ll_DatabaseType, "iMBigData", TextCompare: false) == 0 && Strings.InStr(Globals_Renamed.g_QColumns[num9].Column, "AKA='@HDR@'") != 0)
								{
									if (Operators.CompareString(Globals_Renamed.g_QColumns[num9].Header, "", TextCompare: false) != 0)
									{
										Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, "AKA='@HDR@'", "AKA='" + Globals_Renamed.g_QColumns[num9].Header + "'", 1, -1, CompareMethod.Text);
									}
									else
									{
										Globals_Renamed.g_QColumns[num9].Column = Strings.Replace(Globals_Renamed.g_QColumns[num9].Column, "AKA='@HDR@'", "", 1, -1, CompareMethod.Text);
									}
								}
								if (BuildForm.IsColXPattern(Globals_Renamed.g_QColumns[num9].Column))
								{
									BuildForm.DoColXPattern(ref Globals_Renamed.g_QColumns[num9].Column, ref Globals_Renamed.g_QColumns[num9].Header, ll_DatabaseType, text2);
								}
								text17 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Tag);
								if (Information.IsNothing(text17))
								{
									text17 = "";
								}
								Globals_Renamed.g_QColumns[num9].LHeader = text17;
								if (LikeOperator.LikeString(Strings.LCase(ll_MajorAlias), "sql*", CompareMethod.Binary))
								{
									Globals_Renamed.g_QColumns[num9].Header = Set_SQLite_Hdr(Globals_Renamed.g_QColumns[num9].Header);
								}
								else if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
								{
									Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(Globals_Renamed.g_QColumns[num9].Header, "\"");
								}
								else if (Operators.CompareString(ll_DatabaseType, "Hadoop", TextCompare: false) == 0)
								{
									Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(Globals_Renamed.g_QColumns[num9].Header, "`");
								}
								Globals_Renamed.g_QColumns[num9].Show = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value);
								if (Operators.CompareString(Globals_Renamed.g_QColumns[num9].Show, "P", TextCompare: false) == 0)
								{
									if (Operators.CompareString(MyMode, "SN", TextCompare: false) == 0 && Operators.CompareString(ValidIncQueryAgg, "", TextCompare: false) == 0)
									{
										Globals_Renamed.g_QColumns[num9].Show = "N";
									}
									else
									{
										Globals_Renamed.g_QColumns[num9].Show = "Y";
									}
								}
								if (Operators.CompareString(MyMode, "J", TextCompare: false) == 0 || Operators.CompareString(ValidIncQueryAgg, "RAW", TextCompare: false) == 0)
								{
									Globals_Renamed.g_QColumns[num9].Sort = "None";
								}
								else
								{
									Globals_Renamed.g_QColumns[num9].Sort = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[3].Value);
								}
								if (Operators.CompareString(ValidIncQueryAgg, "", TextCompare: false) == 0)
								{
									Globals_Renamed.g_QColumns[num9].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[4].Value);
									Globals_Renamed.g_QColumns[num9].Pivot = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[5].Value);
								}
								else
								{
									Globals_Renamed.g_QColumns[num9].Statistics = "None";
									Globals_Renamed.g_QColumns[num9].Pivot = "Row";
								}
								Globals_Renamed.g_QColumns[num9].DataType = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[8].Value);
								Globals_Renamed.g_QColumns[num9].Comment = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[9].Value);
								Globals_Renamed.g_QColumns[num9].List = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[10].Value);
								Globals_Renamed.g_QColumns[num9].ColID = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[12].Value);
								Globals_Renamed.g_QColumns[num9].level = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[6].Value));
							}
							Globals_Renamed.g_NoQColumns = num9;
							Populate_Query_Array_Check_Joins(ll_MajorAlias, num7, ref MyMsg);
							num9 = -1;
							short num16 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.RowCount - 1);
							for (num10 = 0; num10 <= num16; num10 = (short)unchecked(num10 + 1))
							{
								text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[2].Value);
								num4 = Strings.InStrRev(text2, ":");
								if (num4 != 0)
								{
									text2 = Strings.Trim(Strings.Mid(text2, num4 + 1));
								}
								text8 = General_Procedures.Strip_Alias(0, text2);
								text2 = General_Procedures.Strip_Alias(1, text2);
								text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[4].Value);
								num4 = Strings.InStrRev(text3, ":");
								if (num4 != 0)
								{
									text3 = Strings.Trim(Strings.Mid(text3, num4 + 1));
								}
								text6 = General_Procedures.Strip_Alias(0, text3);
								text3 = General_Procedures.Strip_Alias(1, text3);
								text16 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[3].Value);
								if (((Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2), TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text3), TextCompare: false) == 0)) | (LikeOperator.LikeString(text16, "IS*NULL", CompareMethod.Binary) & (Operators.CompareString(text3, "", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(text2), Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)))
								{
									num9++;
									Globals_Renamed.g_QJoins[num9].And_Renamed = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[0].Value));
									Globals_Renamed.g_QJoins[num9].ParenO = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[1].Value));
									Globals_Renamed.g_QJoins[num9].ParenC = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[5].Value));
									Globals_Renamed.g_QJoins[num9].Col1 = text2 + "->" + text8;
									Globals_Renamed.g_QJoins[num9].Join_Renamed = text16;
									if (Operators.CompareString(text6, "", TextCompare: false) != 0)
									{
										Globals_Renamed.g_QJoins[num9].Col2 = text3 + "->" + text6;
									}
									else
									{
										Globals_Renamed.g_QJoins[num9].Col2 = "";
									}
								}
							}
							Globals_Renamed.g_NoQJoins = num9;
						}
						else if (Operators.CompareString(ll_MajorAlias, "All->", TextCompare: false) != 0 && Operators.CompareString(ValidIncQueryAgg, "AGG", TextCompare: false) == 0)
						{
							num9 = -1;
							short num17 = num7;
							for (num10 = 0; num10 <= num17; num10 = (short)unchecked(num10 + 1))
							{
								text11 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value);
								text21 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[10].Value);
								flag6 = false;
								if ((Operators.CompareString(text21, "", TextCompare: false) == 0) | (Operators.CompareString(Strings.LCase(Strings.Mid(text21 + "  ", 1, 2)), "s+", TextCompare: false) == 0))
								{
									flag6 = true;
								}
								if (Operators.CompareString(text11, "--", TextCompare: false) != 0 && Operators.CompareString(text11, "N", TextCompare: false) != 0)
								{
									text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value);
									text2 = General_Procedures.Strip_Alias(1, text2);
									text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Value);
									if (Operators.CompareString(Strings.Trim(text3), "", TextCompare: false) == 0)
									{
										text3 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value));
										num4 = Strings.InStr(text3, "->");
										if (num4 != 0)
										{
											text3 = Strings.Mid(text3, num4 + 2);
										}
										num4 = Strings.InStr(text3, ".");
										if (num4 != 0)
										{
											text3 = Strings.Mid(text3, num4 + 1);
										}
										text3 = General_Procedures.Strip_CSV_Square(text3, ll_ObjectType);
									}
									if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
									{
										text3 = Set_SQLite_Hdr(text3);
									}
									else if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
									{
										text3 = Set_Hadoop_Hdr(text3, "\"");
									}
									if (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2), TextCompare: false) == 0)
									{
										num9++;
										if (!flag6)
										{
											Globals_Renamed.g_QColumns[num9].Column = text3;
											Globals_Renamed.g_QColumns[num9].List = "CSV";
										}
										else
										{
											Globals_Renamed.g_QColumns[num9].Column = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value);
											Globals_Renamed.g_QColumns[num9].Column = General_Procedures.Strip_Alias(0, Globals_Renamed.g_QColumns[num9].Column);
											Globals_Renamed.g_QColumns[num9].List = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[10].Value);
										}
										Globals_Renamed.g_QColumns[num9].No = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].HeaderCell.Value);
										Globals_Renamed.g_QColumns[num9].Header = text3;
										text17 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Tag);
										if (Information.IsNothing(text17))
										{
											text17 = "";
										}
										Globals_Renamed.g_QColumns[num9].LHeader = text17;
										Globals_Renamed.g_QColumns[num9].Show = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value);
										if (Operators.CompareString(Globals_Renamed.g_QColumns[num9].Show, "P", TextCompare: false) == 0)
										{
											if (Operators.CompareString(MyMode, "SN", TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].Show = "N";
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].Show = "Y";
											}
										}
										if (Operators.CompareString(MyMode, "J", TextCompare: false) == 0)
										{
											Globals_Renamed.g_QColumns[num9].Sort = "None";
										}
										else
										{
											Globals_Renamed.g_QColumns[num9].Sort = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[3].Value);
										}
										Globals_Renamed.g_QColumns[num9].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[4].Value);
										Globals_Renamed.g_QColumns[num9].Pivot = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[5].Value);
										Globals_Renamed.g_QColumns[num9].DataType = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[8].Value);
										Globals_Renamed.g_QColumns[num9].Comment = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[9].Value);
										Globals_Renamed.g_QColumns[num9].ColID = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[12].Value);
										Globals_Renamed.g_QColumns[num9].level = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[6].Value));
									}
								}
							}
							Globals_Renamed.g_NoQColumns = num9;
							Populate_Query_Array_Check_Joins(ll_MajorAlias, num7, ref MyMsg);
						}
						else
						{
							num9 = -1;
							flag4 = false;
							flag3 = false;
							flag5 = false;
							short num18 = num7;
							for (num10 = 0; num10 <= num18; num10 = (short)unchecked(num10 + 1))
							{
								text11 = Strings.UCase(Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value), 1, 1));
								object[] array2;
								DataGridViewCell dataGridViewCell;
								bool[] array3;
								object obj2 = NewLateBinding.LateGet(null, typeof(Strings), "UCase", array2 = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[5]).Value }, null, null, array3 = new bool[1] { true });
								if (array3[0])
								{
									dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
								}
								text4 = Conversions.ToString(obj2);
								text = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value);
								text2 = General_Procedures.Strip_Alias(1, text);
								text3 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Value);
								if ((Operators.CompareString(text11, "Y", TextCompare: false) == 0 || Operators.CompareString(text11, "P", TextCompare: false) == 0) && Operators.CompareString(l_Special_HBASE, "", TextCompare: false) != 0 && LikeOperator.LikeString(l_Special_HBASE, "*," + text2 + ",*", CompareMethod.Binary))
								{
									if (Check_Hbase_Special(ref l_Hbase, text2))
									{
										num9++;
										Globals_Renamed.g_QColumns[num9].No = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].HeaderCell.Value);
										Globals_Renamed.g_QColumns[num9].Column = "Column-Pattern->[[Y<;>" + text2 + "<;>" + Globals_Renamed.gWinuser + "_" + text2 + "_" + Globals_Renamed.gSPFCache + "<dot>tab<;>Regex<;><dot>*<;>|<>|<;>]]";
										Globals_Renamed.g_QColumns[num9].Header = "";
										Globals_Renamed.g_QColumns[num9].Show = "Y";
										Globals_Renamed.g_QColumns[num9].Sort = "None";
										Globals_Renamed.g_QColumns[num9].Statistics = "None";
										Globals_Renamed.g_QColumns[num9].Pivot = "Row";
										Globals_Renamed.g_QColumns[num9].AllStat = "None";
										Globals_Renamed.g_QColumns[num9].DataType = "c";
										Globals_Renamed.g_QColumns[num9].level = "";
										Globals_Renamed.g_QColumns[num9].List = "CSV";
									}
								}
								else
								{
									if (BuildForm.IsColXPattern(text))
									{
										BuildForm.DoColXPattern(ref text, ref text3, ll_DatabaseType, text2);
									}
									if (((Operators.CompareString(text11, "Y", TextCompare: false) == 0 || Operators.CompareString(text11, "P", TextCompare: false) == 0) && !BuildForm.IsColPattern(text, "MDBNOT") && (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0 || Operators.CompareString(text4, "HEADER", TextCompare: false) != 0)) || Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
									{
										num9++;
										Globals_Renamed.g_QColumns[num9].No = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].HeaderCell.Value);
										if (BuildForm.IsColPattern(text))
										{
											num4 = Strings.InStr(text, ".Column-Pattern");
											if (num4 == 0)
											{
												num3 = Strings.InStr(text, "->Column-Pattern");
												if (num3 != 0)
												{
													text17 = Strings.Mid(text, 1, num3 - 1);
													text = Strings.Mid(text, num3 + 2);
												}
											}
											else
											{
												text17 = Strings.Mid(text, 1, num4 - 1);
												text = Strings.Mid(text, num4 + 1);
											}
											if (Strings.InStr(Strings.LCase(text), "<;>duckdb:") != 0)
											{
												num4 = Strings.InStr(text, "<;>");
												if (num4 != 0)
												{
													string text22 = Strings.Mid(text, num4 + 3);
													num3 = Strings.InStr(text22, "<;>");
													if (num3 != 0)
													{
														text22 = Strings.Mid(text22, num3);
													}
													text = Strings.Mid(text, 1, num4 - 1) + "<;>" + text17 + text22;
												}
											}
											text = Strings.Replace(text, "]]", "<;>" + text3 + "]]");
											text = Strings.Replace(text, ".", "<dot>", 1, -1, CompareMethod.Text);
											text = Strings.Replace(text, ",", "<comma>", 1, -1, CompareMethod.Text);
											text3 = "";
										}
										else if (Operators.CompareString(Strings.Trim(text3), "", TextCompare: false) == 0)
										{
											text3 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value));
											num4 = Strings.InStr(text3, "->");
											if (num4 != 0)
											{
												text3 = Strings.Mid(text3, num4 + 2);
											}
											num4 = Strings.InStr(text3, ".");
											if (num4 != 0)
											{
												text3 = Strings.Mid(text3, num4 + 1);
											}
											text3 = General_Procedures.Strip_CSV_Square(text3, ll_ObjectType);
										}
										flag = false;
										flag2 = false;
										if ((Operators.CompareString(text4, "VALUE", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) != 0))
										{
											short num19 = (short)(num9 - 1);
											for (num12 = 0; num12 <= num19; num12 = (short)unchecked(num12 + 1))
											{
												if (Operators.CompareString(Globals_Renamed.g_QColumns[num12].Column, "CrossTab->[[" + Strings.LCase(text2) + "," + Globals_Renamed.gSPFCache + ";:N]]", TextCompare: false) == 0)
												{
													flag = true;
													break;
												}
											}
											if (!flag)
											{
												Globals_Renamed.g_QColumns[num9].Column = "CrossTab->[[" + Strings.LCase(text2) + "," + Globals_Renamed.gSPFCache + ";:N]]";
												Globals_Renamed.g_QColumns[num9].Header = "";
												Globals_Renamed.g_QColumns[num9].LHeader = "";
												if (Conversions.ToBoolean(LikeOperator.LikeObject(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value, "Y*", CompareMethod.Binary)))
												{
													Globals_Renamed.g_QColumns[num9].Show = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value);
												}
												else
												{
													Globals_Renamed.g_QColumns[num9].Show = Get_QShow_Final(Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value), 1, 1));
												}
												Globals_Renamed.g_QColumns[num9].Sort = "None";
												Globals_Renamed.g_QColumns[num9].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[7].Value);
												Globals_Renamed.g_QColumns[num9].AllStat = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[7].Tag);
											}
										}
										else if ((Operators.CompareString(text4, "STACK", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) != 0))
										{
											short num20 = (short)(num9 - 1);
											for (num12 = 0; num12 <= num20; num12 = (short)unchecked(num12 + 1))
											{
												if (Operators.CompareString(Strings.LCase(Globals_Renamed.g_QColumns[num12].Header), "_data_" + Strings.LCase(text2), TextCompare: false) == 0)
												{
													flag2 = true;
													num13++;
													break;
												}
											}
											if (!flag2)
											{
												Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + "._data_";
												Globals_Renamed.g_QColumns[num9].Header = "_data_" + Strings.LCase(text2);
												Globals_Renamed.g_QColumns[num9].Show = Get_QShow_Final(Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value), 1, 1));
												Globals_Renamed.g_QColumns[num9].Sort = "None";
												Globals_Renamed.g_QColumns[num9].Statistics = "None";
												Globals_Renamed.g_QColumns[num9].Pivot = "Row";
												Globals_Renamed.g_QColumns[num9].Comment = "";
												Globals_Renamed.g_QColumns[num9].List = "CSV";
												Globals_Renamed.g_QColumns[num9].level = "";
												num9++;
												Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + "._label_";
												Globals_Renamed.g_QColumns[num9].Header = "_label_" + Strings.LCase(text2);
												Globals_Renamed.g_QColumns[num9].Show = Get_QShow_Final(Strings.Mid(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value), 1, 1));
												Globals_Renamed.g_QColumns[num9].Sort = "None";
												Globals_Renamed.g_QColumns[num9].Statistics = "None";
												Globals_Renamed.g_QColumns[num9].Pivot = "Row";
												Globals_Renamed.g_QColumns[num9].DataType = "c";
												Globals_Renamed.g_QColumns[num9].Comment = "";
												Globals_Renamed.g_QColumns[num9].List = "CSV";
												Globals_Renamed.g_QColumns[num9].level = "";
												flag2 = true;
											}
										}
										else
										{
											Globals_Renamed.g_QColumns[num9].Show = Get_QShow_Final(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[2].Value));
											if (Operators.CompareString(text3, "*", TextCompare: false) == 0)
											{
												switch (Strings.LCase(text2))
												{
												case "sql":
													if (flag3)
													{
														Globals_Renamed.g_QColumns[num9].Show = "N";
													}
													flag3 = true;
													break;
												case "ilv":
													if (flag5)
													{
														Globals_Renamed.g_QColumns[num9].Show = "N";
													}
													flag5 = true;
													break;
												case "txt":
													if (flag4)
													{
														Globals_Renamed.g_QColumns[num9].Show = "N";
													}
													flag4 = true;
													break;
												}
												Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + "." + text3;
												Globals_Renamed.g_QColumns[num9].Header = "";
												Globals_Renamed.g_QColumns[num9].LHeader = "";
											}
											else if (BuildForm.IsColPattern(text))
											{
												Globals_Renamed.g_QColumns[num9].Header = "";
												Globals_Renamed.g_QColumns[num9].Column = text;
											}
											else
											{
												if (Operators.CompareString(Strings.LCase(text2 + "->"), Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
												{
													Globals_Renamed.g_QColumns[num9].Column = General_Procedures.Strip_Alias(0, Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[11].Value));
													switch (ll_DatabaseType)
													{
													case "SQLite":
														Globals_Renamed.g_QColumns[num9].Header = Set_SQLite_Hdr(text3);
														break;
													case "Hadoop":
														Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(text3, "`");
														break;
													case "DUCKDB":
														Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(text3, "\"");
														break;
													default:
														Globals_Renamed.g_QColumns[num9].Header = text3;
														break;
													}
												}
												else if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0)
												{
													if (Operators.CompareString(Strings.Mid(text3, 1, 1), "[", TextCompare: false) != 0)
													{
														Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + ".[" + text3 + "]";
													}
													else
													{
														Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + "." + text3;
													}
													Globals_Renamed.g_QColumns[num9].Header = Set_SQLite_Hdr(text3);
												}
												else if (Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
												{
													Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + ".\"" + text3 + "\"";
													Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(text3, "\"");
												}
												else
												{
													Globals_Renamed.g_QColumns[num9].Column = Strings.LCase(text2) + ".`" + text3 + "`";
													Globals_Renamed.g_QColumns[num9].Header = Set_Hadoop_Hdr(text3, "`");
												}
												Globals_Renamed.g_QColumns[num9].LHeader = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[1].Tag);
												if (Information.IsNothing(Globals_Renamed.g_QColumns[num9].LHeader))
												{
													Globals_Renamed.g_QColumns[num9].LHeader = "";
												}
											}
											Globals_Renamed.g_QColumns[num9].Sort = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[3].Value);
										}
										if (unchecked(!flag && !flag2))
										{
											if (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[4].Value);
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].Statistics = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[7].Value);
											}
											if (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].Pivot = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[5].Value);
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].Pivot = "Row";
											}
											Globals_Renamed.g_QColumns[num9].DataType = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[8].Value);
											if (Operators.CompareString(text4, "VALUE", TextCompare: false) == 0 && Operators.CompareString(Strings.LCase(Strings.Mid(Globals_Renamed.g_QColumns[num9].DataType + " ", 1, 1)), "x", TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].DataType = Strings.Trim("c" + Strings.Mid(Globals_Renamed.g_QColumns[num9].DataType + " ", 2, 1));
											}
											text18 = Strings.LCase(Globals_Renamed.g_QColumns[num9].DataType);
											object obj3 = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array2 = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[4]).Value }, null, null, array3 = new bool[1] { true });
											if (array3[0])
											{
												dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
											}
											text19 = Conversions.ToString(obj3);
											object obj4 = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array2 = new object[1] { (dataGridViewCell = MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[7]).Value }, null, null, array3 = new bool[1] { true });
											if (array3[0])
											{
												dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array2[0]));
											}
											text20 = Conversions.ToString(obj4);
											if (Operators.CompareString(text18, "f", TextCompare: false) != 0 && (LikeOperator.LikeString(text19, "p*", CompareMethod.Binary) || Operators.CompareString(text19, "avg", TextCompare: false) == 0 || LikeOperator.LikeString(text19, "std*", CompareMethod.Binary) || LikeOperator.LikeString(text19, "var*", CompareMethod.Binary) || LikeOperator.LikeString(text20, "p*", CompareMethod.Binary) || Operators.CompareString(text20, "avg", TextCompare: false) == 0 || LikeOperator.LikeString(text20, "std*", CompareMethod.Binary) || LikeOperator.LikeString(text20, "var*", CompareMethod.Binary) || LikeOperator.LikeString(text20, "skew*", CompareMethod.Binary) || LikeOperator.LikeString(text20, "kurtosis*", CompareMethod.Binary)))
											{
												Globals_Renamed.g_QColumns[num9].DataType = "f";
											}
											else if (Operators.CompareString(text18, "f", TextCompare: false) != 0 && Operators.CompareString(text18, "n", TextCompare: false) != 0 && (LikeOperator.LikeString(text19, "count*", CompareMethod.Binary) || Operators.CompareString(text19, "sum", TextCompare: false) == 0 || Operators.CompareString(text19, "total", TextCompare: false) == 0 || LikeOperator.LikeString(text20, "count*", CompareMethod.Binary) || Operators.CompareString(text20, "sum", TextCompare: false) == 0 || Operators.CompareString(text20, "total", TextCompare: false) == 0))
											{
												Globals_Renamed.g_QColumns[num9].DataType = "n";
											}
											if (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].Comment = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[9].Value);
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].Comment = "";
											}
											if (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].List = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[10].Value);
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].List = "CSV";
											}
											if (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)
											{
												Globals_Renamed.g_QColumns[num9].level = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridQuery.Rows[num10].Cells[6].Value));
											}
											else if (Operators.CompareString(text20, "none", TextCompare: false) != 0)
											{
												Globals_Renamed.g_QColumns[num9].level = "s,s";
											}
											else
											{
												Globals_Renamed.g_QColumns[num9].level = "s,0";
											}
										}
										else if (flag)
										{
											num9--;
										}
									}
								}
							}
							Globals_Renamed.g_NoQColumns = num9;
							num9 = -1;
							short num21 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.RowCount - 1);
							for (num10 = 0; num10 <= num21; num10 = (short)unchecked(num10 + 1))
							{
								text8 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[2].Value));
								num4 = Strings.InStr(text8, ":");
								if (num4 != 0)
								{
									text8 = Strings.Trim(Strings.Mid(text8, num4 + 1));
								}
								text2 = General_Procedures.Strip_Alias(1, text8);
								text6 = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[4].Value));
								num4 = Strings.InStr(text6, ":");
								if (num4 != 0)
								{
									text6 = Strings.Trim(Strings.Mid(text6, num4 + 1));
								}
								text3 = General_Procedures.Strip_Alias(1, text6);
								text16 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[3].Value);
								if (((Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2) + "->", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text3) + "->", TextCompare: false) == 0)) | ((Operators.CompareString(Strings.LCase(text2), Strings.LCase(text3), TextCompare: false) != 0) & (Operators.CompareString(text3, "", TextCompare: false) != 0)) | (LikeOperator.LikeString(text16, "IS*NULL", CompareMethod.Binary) & (Operators.CompareString(text3, "", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(text2) + "->", Strings.LCase(ll_MajorAlias), TextCompare: false) == 0)))
								{
									num9++;
									Globals_Renamed.g_QJoins[num9].Col1 = text8;
									Globals_Renamed.g_QJoins[num9].Join_Renamed = text16;
									Globals_Renamed.g_QJoins[num9].And_Renamed = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[0].Value);
									Globals_Renamed.g_QJoins[num9].ParenO = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[1].Value);
									Globals_Renamed.g_QJoins[num9].ParenC = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridJoin.Rows[num10].Cells[5].Value);
									Globals_Renamed.g_QJoins[num9].Col2 = text6;
								}
							}
							Globals_Renamed.g_NoQJoins = num9;
						}
						num9 = -1;
						num6 = (short)(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.RowCount - 1);
						short num22 = num6;
						for (num10 = 0; num10 <= num22; num10 = (short)unchecked(num10 + 1))
						{
							text2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[10].Value);
							text2 = General_Procedures.Strip_Alias(1, text2);
							text21 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[9].Value);
							flag6 = false;
							if (Operators.CompareString(text21, "", TextCompare: false) == 0 || Operators.CompareString(Strings.LCase(Strings.Mid(text21 + "  ", 1, 2)), "s+", TextCompare: false) == 0)
							{
								flag6 = true;
							}
							if (!((Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2), TextCompare: false) == 0) | (Operators.CompareString(Strings.LCase(ll_MajorAlias), Strings.LCase(text2) + "->", TextCompare: false) == 0)))
							{
								continue;
							}
							num9++;
							switch (ValidIncQueryAgg)
							{
							case "RAW":
								if (!flag6)
								{
									break;
								}
								goto default;
							default:
								if (Operators.CompareString(ValidIncQueryAgg, "AGG", TextCompare: false) != 0 || !flag6)
								{
									continue;
								}
								break;
							case null:
							case "":
								break;
							}
							Globals_Renamed.g_QFilters[num9].Column = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[10].Value);
							Globals_Renamed.g_QFilters[num9].Column = General_Procedures.Strip_Alias(0, Globals_Renamed.g_QFilters[num9].Column);
							Globals_Renamed.g_QFilters[num9].And_Renamed = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[0].Value);
							Globals_Renamed.g_QFilters[num9].ParentO = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[1].Value);
							Globals_Renamed.g_QFilters[num9].Operator_Renamed = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[3].Value);
							Globals_Renamed.g_QFilters[num9].Value1 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[4].Value);
							Globals_Renamed.g_QFilters[num9].Value2 = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[11].Value);
							Globals_Renamed.g_QFilters[num9].ParenC = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[5].Value);
							Globals_Renamed.g_QFilters[num9].Key = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[7].Tag);
							Globals_Renamed.g_QFilters[num9].DataType = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[8].Value);
							Globals_Renamed.g_QFilters[num9].List = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[9].Value);
							Globals_Renamed.g_QFilters[num9].ColHead = Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[2].Value);
							if (Operators.CompareString(Strings.UCase(General_Procedures.Set_Get_Fn("G", "", Globals_Renamed.g_QFilters[num9].ColHead, "")), "Y", TextCompare: false) == 0)
							{
								Globals_Renamed.g_QFilters[num9].ColHead = General_Procedures.Strip_Alias(1, Globals_Renamed.g_QFilters[num9].ColHead);
							}
							num4 = Strings.InStr(Globals_Renamed.g_QFilters[num9].ColHead, ".");
							if (num4 != 0)
							{
								Globals_Renamed.g_QFilters[num9].ColHead = Strings.Mid(Globals_Renamed.g_QFilters[num9].ColHead, num4 + 1);
							}
							Globals_Renamed.g_QFilters[num9].level = Strings.Trim(Conversions.ToString(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].GridFilter.Rows[num10].Cells[6].Value));
						}
						Globals_Renamed.g_NoQFilters = num9;
						short g_NoQColumns = Globals_Renamed.g_NoQColumns;
						for (num10 = 0; num10 <= g_NoQColumns; num10 = (short)unchecked(num10 + 1))
						{
							if ((Operators.CompareString(Strings.Mid(Globals_Renamed.g_QColumns[num10].Comment, 1, 3), "***", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Globals_Renamed.g_QColumns[num10].Statistics), "EXPR", TextCompare: false) == 0))
							{
								text17 = Globals_Renamed.g_QColumns[num10].Column;
								Globals_Renamed.g_QColumns[num10].Column = Replace_ge_CRLF(text17, ll_DatabaseType);
							}
						}
						short g_NoQFilters = Globals_Renamed.g_NoQFilters;
						for (num10 = 0; num10 <= g_NoQFilters; num10 = (short)unchecked(num10 + 1))
						{
							text17 = Strings.Trim(Strings.UCase(Strings.Mid(Globals_Renamed.g_QFilters[num10].DataType + "  ", 2, 1)));
							if ((Operators.CompareString(text17, "X", TextCompare: false) == 0) | (Operators.CompareString(text17, "S", TextCompare: false) == 0))
							{
								text17 = Globals_Renamed.g_QFilters[num10].Column;
								Globals_Renamed.g_QFilters[num10].Column = Replace_ge_CRLF(text17, ll_DatabaseType);
							}
						}
						l_Hbase = null;
						goto end_IL_0001;
					}
					case 16099:
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
					goto IL_3f19;
				}
				end_IL_0001:;
			}
			catch (object obj5) when (obj5 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj5);
				try0001_dispatch = 16099;
				continue;
			}
			break;
			IL_3f19:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static string Write_Join(string CBBegin, string CBEnd, short ll_ObjectType, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string[,] array = default(string[,]);
		short num5 = default(short);
		string myExt = default(string);
		short num6 = default(short);
		string text2 = default(string);
		short num7 = default(short);
		string replacement = default(string);
		short num8 = default(short);
		short num9 = default(short);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		short num10 = default(short);
		short num11 = default(short);
		string text9 = default(string);
		short num12 = default(short);
		short num13 = default(short);
		short num14 = default(short);
		short num15 = default(short);
		string text11 = default(string);
		short g_NoQJoins = default(short);
		short num16 = default(short);
		string result = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text10;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 3497:
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
								goto IL_0020;
							case 5:
								goto IL_0029;
							case 6:
								goto IL_002e;
							case 7:
								goto IL_0033;
							case 8:
								goto IL_003c;
							case 9:
								goto IL_0041;
							case 10:
								goto IL_0047;
							case 11:
								goto IL_004d;
							case 12:
								goto IL_0057;
							case 13:
								goto IL_0061;
							case 14:
								goto IL_006b;
							case 15:
								goto IL_0075;
							case 16:
								goto IL_007f;
							case 17:
								goto IL_0089;
							case 18:
								goto IL_008f;
							case 19:
								goto IL_0095;
							case 20:
								goto IL_009f;
							case 21:
								goto IL_00a9;
							case 22:
								goto IL_00af;
							case 23:
								goto IL_00c1;
							case 24:
								goto IL_00d3;
							case 25:
								goto IL_00e3;
							case 26:
								goto IL_00e9;
							case 27:
								goto IL_00fe;
							case 28:
								goto IL_0121;
							case 29:
								goto IL_013d;
							case 30:
								goto IL_014e;
							case 31:
								goto IL_016e;
							case 32:
								goto IL_017b;
							case 34:
								goto IL_01bc;
							case 35:
								goto IL_01d5;
							case 36:
								goto IL_01e9;
							case 37:
								goto IL_01f3;
							case 38:
								goto IL_0201;
							case 39:
								goto IL_0225;
							case 40:
							case 41:
								goto IL_0238;
							case 42:
								goto IL_0248;
							case 43:
								goto IL_0271;
							case 45:
								goto IL_02a0;
							case 48:
								goto IL_02d0;
							case 49:
								goto IL_02e4;
							case 50:
								goto IL_02f2;
							case 51:
								goto IL_0307;
							case 55:
								goto IL_0330;
							case 56:
								goto IL_033f;
							case 33:
							case 44:
							case 46:
							case 47:
							case 52:
							case 53:
							case 54:
							case 57:
							case 58:
								goto IL_0357;
							case 59:
								goto IL_0361;
							case 60:
								goto IL_0374;
							case 61:
								goto IL_0385;
							case 62:
								goto IL_039c;
							case 63:
								goto IL_03ab;
							case 64:
								goto IL_03e5;
							case 66:
								goto IL_03fc;
							case 67:
								goto IL_043c;
							case 68:
								goto IL_0446;
							case 70:
								goto IL_0454;
							case 71:
								goto IL_045e;
							case 69:
							case 72:
							case 73:
								goto IL_0469;
							case 74:
								goto IL_046f;
							case 75:
								goto IL_047c;
							case 76:
								goto IL_0488;
							case 77:
								goto IL_0499;
							case 78:
							case 79:
								goto IL_04ab;
							case 80:
								goto IL_04c3;
							case 81:
								goto IL_04d5;
							case 82:
								goto IL_050a;
							case 83:
								goto IL_0510;
							case 84:
								goto IL_0522;
							case 85:
								goto IL_053d;
							case 86:
								goto IL_0558;
							case 87:
								goto IL_0571;
							case 88:
							case 89:
								goto IL_057d;
							case 90:
								goto IL_0598;
							case 91:
								goto IL_05b1;
							case 92:
							case 93:
								goto IL_05bd;
							case 94:
								goto IL_05d8;
							case 95:
								goto IL_05f3;
							case 96:
								goto IL_060e;
							case 97:
								goto IL_063a;
							case 98:
								goto IL_0644;
							case 99:
								goto IL_06a1;
							case 100:
								goto IL_0712;
							case 101:
								goto IL_0724;
							case 102:
								goto IL_0795;
							case 103:
								goto IL_07a6;
							case 104:
								goto IL_07c1;
							case 105:
								goto IL_07da;
							case 107:
								goto IL_07f9;
							case 108:
								goto IL_0814;
							case 109:
								goto IL_082d;
							case 111:
								goto IL_084c;
							case 112:
								goto IL_0867;
							case 113:
								goto IL_0880;
							case 115:
								goto IL_089c;
							case 116:
								goto IL_08b7;
							case 117:
								goto IL_08d0;
							case 119:
								goto IL_08ed;
							case 106:
							case 110:
							case 114:
							case 118:
							case 120:
							case 121:
								goto IL_0904;
							case 122:
								goto IL_096d;
							case 124:
								goto IL_097a;
							case 125:
								goto IL_0993;
							case 126:
								goto IL_09ac;
							case 127:
								goto IL_09c5;
							case 128:
								goto IL_09de;
							case 131:
							case 132:
								goto IL_0a58;
							case 123:
							case 129:
							case 130:
							case 133:
							case 134:
							case 135:
								goto IL_0a72;
							case 136:
								goto IL_0a88;
							case 137:
								goto IL_0a99;
							case 138:
								goto IL_0ab5;
							case 139:
								goto IL_0ac6;
							case 140:
							case 141:
								goto IL_0ad1;
							case 142:
								goto IL_0ae1;
							case 143:
								goto IL_0b11;
							case 65:
							case 144:
							case 145:
								goto IL_0b29;
							case 146:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 147:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0330:
						num2 = 55;
						text = array[num5, 1];
						goto IL_033f;
						IL_033f:
						num2 = 56;
						array[num5, 1] = Make_Valid_SQLite_Table(text, myExt, ll_JoinDuckDB);
						goto IL_0357;
						IL_0307:
						num2 = 51;
						array[num5, 1] = text + " " + array[num5, 2];
						goto IL_0357;
						IL_0357:
						num2 = 58;
						num6++;
						goto IL_0361;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0014;
						IL_0014:
						num2 = 3;
						array = new string[33, 3];
						goto IL_0020;
						IL_0020:
						num2 = 4;
						text = "";
						goto IL_0029;
						IL_0029:
						num2 = 5;
						num6 = 0;
						goto IL_002e;
						IL_002e:
						num2 = 6;
						num7 = 0;
						goto IL_0033;
						IL_0033:
						num2 = 7;
						replacement = "";
						goto IL_003c;
						IL_003c:
						num2 = 8;
						num8 = 0;
						goto IL_0041;
						IL_0041:
						num2 = 9;
						num5 = 0;
						goto IL_0047;
						IL_0047:
						num2 = 10;
						num9 = 0;
						goto IL_004d;
						IL_004d:
						num2 = 11;
						text3 = "";
						goto IL_0057;
						IL_0057:
						num2 = 12;
						text4 = "";
						goto IL_0061;
						IL_0061:
						num2 = 13;
						text5 = "";
						goto IL_006b;
						IL_006b:
						num2 = 14;
						text6 = "";
						goto IL_0075;
						IL_0075:
						num2 = 15;
						text7 = "";
						goto IL_007f;
						IL_007f:
						num2 = 16;
						text8 = "";
						goto IL_0089;
						IL_0089:
						num2 = 17;
						num10 = 0;
						goto IL_008f;
						IL_008f:
						num2 = 18;
						num11 = 0;
						goto IL_0095;
						IL_0095:
						num2 = 19;
						myExt = "";
						goto IL_009f;
						IL_009f:
						num2 = 20;
						text9 = "           ";
						goto IL_00a9;
						IL_00a9:
						num2 = 21;
						num5 = 0;
						goto IL_00af;
						IL_00af:
						num2 = 22;
						array[num5, 1] = "";
						goto IL_00c1;
						IL_00c1:
						num2 = 23;
						array[num5, 2] = "";
						goto IL_00d3;
						IL_00d3:
						num2 = 24;
						num5 = (short)unchecked(num5 + 1);
						if (num5 <= 31)
						{
							goto IL_00af;
						}
						goto IL_00e3;
						IL_00e3:
						num2 = 25;
						num6 = 0;
						goto IL_00e9;
						IL_00e9:
						num2 = 26;
						num12 = (short)(Globals_Renamed.MaxFromTables - 1);
						num5 = 0;
						goto IL_036b;
						IL_036b:
						if (num5 <= num12)
						{
							goto IL_00fe;
						}
						goto IL_0374;
						IL_0374:
						num2 = 60;
						if (Globals_Renamed.g_NoQJoins < 0)
						{
							goto IL_0385;
						}
						goto IL_03fc;
						IL_0385:
						num2 = 61;
						text2 = text2 + CBBegin + array[0, 1] + CBEnd;
						goto IL_039c;
						IL_039c:
						num2 = 62;
						num13 = (short)(num6 - 1);
						num5 = 1;
						goto IL_03ef;
						IL_03ef:
						if (num5 <= num13)
						{
							goto IL_03ab;
						}
						goto IL_0b29;
						IL_03ab:
						num2 = 63;
						text2 = text2 + Globals_Renamed.CRLF + CBBegin + "," + array[num5, 1] + CBEnd;
						goto IL_03e5;
						IL_03e5:
						num2 = 64;
						num5 = (short)unchecked(num5 + 1);
						goto IL_03ef;
						IL_03fc:
						num2 = 66;
						if (Operators.CompareString(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "JOIN:All")), "Yes", TextCompare: false) == 0)
						{
							goto IL_043c;
						}
						goto IL_0454;
						IL_043c:
						num2 = 67;
						replacement = " LEFT OUTER JOIN ";
						goto IL_0446;
						IL_0446:
						num2 = 68;
						text10 = "O";
						goto IL_0469;
						IL_0454:
						num2 = 70;
						replacement = " INNER JOIN ";
						goto IL_045e;
						IL_045e:
						num2 = 71;
						text10 = "I";
						goto IL_0469;
						IL_0469:
						num2 = 73;
						text2 = CBBegin;
						goto IL_046f;
						IL_046f:
						num2 = 74;
						if (ll_ObjectType == 3)
						{
							goto IL_047c;
						}
						goto IL_04ab;
						IL_047c:
						num2 = 75;
						num14 = num6;
						num5 = 2;
						goto IL_04a3;
						IL_04a3:
						if (num5 <= num14)
						{
							goto IL_0488;
						}
						goto IL_04ab;
						IL_0488:
						num2 = 76;
						text2 += "( ";
						goto IL_0499;
						IL_0499:
						num2 = 77;
						num5 = (short)unchecked(num5 + 1);
						goto IL_04a3;
						IL_04ab:
						num2 = 79;
						text2 = text2 + text9 + array[0, 1] + CBEnd;
						goto IL_04c3;
						IL_04c3:
						num2 = 80;
						num15 = (short)(num6 - 1);
						num5 = 1;
						goto IL_0b1e;
						IL_0b1e:
						if (num5 <= num15)
						{
							goto IL_04d5;
						}
						goto IL_0b29;
						IL_0b29:
						num2 = 145;
						Array.Clear(array, 0, array.Length);
						break;
						IL_04d5:
						num2 = 81;
						text11 = "\r\n" + CBBegin + " @JOIN-TYPE@ " + array[num5, 1] + CBEnd;
						goto IL_050a;
						IL_050a:
						num2 = 82;
						num10 = 1;
						goto IL_0510;
						IL_0510:
						num2 = 83;
						g_NoQJoins = Globals_Renamed.g_NoQJoins;
						num8 = 0;
						goto IL_0a7f;
						IL_0a7f:
						if (num8 <= g_NoQJoins)
						{
							goto IL_0522;
						}
						goto IL_0a88;
						IL_0a88:
						num2 = 136;
						if (num10 == 1)
						{
							goto IL_0a99;
						}
						goto IL_0ad1;
						IL_0a99:
						num2 = 137;
						text11 = Strings.Replace(text11, " @JOIN-TYPE@ ", " CROSS JOIN ", 1, -1, CompareMethod.Text);
						goto IL_0ab5;
						IL_0ab5:
						num2 = 138;
						text2 += text11;
						goto IL_0ac6;
						IL_0ac6:
						num2 = 139;
						num10 = 0;
						goto IL_0ad1;
						IL_0ad1:
						num2 = 141;
						if (ll_ObjectType == 3)
						{
							goto IL_0ae1;
						}
						goto IL_0b11;
						IL_0ae1:
						num2 = 142;
						text2 = text2 + Globals_Renamed.CRLF + CBBegin + ") " + CBEnd;
						goto IL_0b11;
						IL_0b11:
						num2 = 143;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0b1e;
						IL_0522:
						num2 = 84;
						text7 = Strings.Trim(Globals_Renamed.g_QJoins[num8].Col1);
						goto IL_053d;
						IL_053d:
						num2 = 85;
						text8 = Strings.Trim(Globals_Renamed.g_QJoins[num8].Join_Renamed);
						goto IL_0558;
						IL_0558:
						num2 = 86;
						if (Operators.CompareString(text8, "", TextCompare: false) == 0)
						{
							goto IL_0571;
						}
						goto IL_057d;
						IL_0571:
						num2 = 87;
						text8 = "=";
						goto IL_057d;
						IL_057d:
						num2 = 89;
						text4 = Strings.Trim(Globals_Renamed.g_QJoins[num8].And_Renamed);
						goto IL_0598;
						IL_0598:
						num2 = 90;
						if (Operators.CompareString(text4, "", TextCompare: false) == 0)
						{
							goto IL_05b1;
						}
						goto IL_05bd;
						IL_05b1:
						num2 = 91;
						text4 = "AND";
						goto IL_05bd;
						IL_05bd:
						num2 = 93;
						text3 = Strings.Trim(Globals_Renamed.g_QJoins[num8].ParenO);
						goto IL_05d8;
						IL_05d8:
						num2 = 94;
						text5 = Strings.Trim(Globals_Renamed.g_QJoins[num8].ParenC);
						goto IL_05f3;
						IL_05f3:
						num2 = 95;
						text6 = Strings.Trim(Globals_Renamed.g_QJoins[num8].Col2);
						goto IL_060e;
						IL_060e:
						num2 = 96;
						if (LikeOperator.LikeString(Strings.UCase(text8), "IS*NULL", CompareMethod.Binary) & (Operators.CompareString(text6, ".", TextCompare: false) == 0))
						{
							goto IL_063a;
						}
						goto IL_0644;
						IL_063a:
						num2 = 97;
						text6 = "";
						goto IL_0644;
						IL_0644:
						num2 = 98;
						if (((Operators.CompareString(text7, "", TextCompare: false) != 0) & (Operators.CompareString(text6, "", TextCompare: false) != 0)) | (LikeOperator.LikeString(text8, "IS*NULL", CompareMethod.Binary) & (Operators.CompareString(text7, "", TextCompare: false) != 0) & (Operators.CompareString(text6, "", TextCompare: false) == 0)))
						{
							goto IL_06a1;
						}
						goto IL_0a72;
						IL_06a1:
						num2 = 99;
						if (LikeOperator.LikeString(text8, "*IS*NULL", CompareMethod.Binary) | (LikeOperator.LikeString(Strings.UCase(text7), "*" + Strings.UCase(array[num5, 2]) + ".*", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(text6), "*" + Strings.UCase(array[num5, 2]) + ".*", CompareMethod.Binary)))
						{
							goto IL_0712;
						}
						goto IL_0a72;
						IL_0712:
						num2 = 100;
						num16 = (short)(num5 - 1);
						num9 = 0;
						goto IL_0a65;
						IL_0a65:
						if (num9 <= num16)
						{
							goto IL_0724;
						}
						goto IL_0a72;
						IL_0724:
						num2 = 101;
						if (LikeOperator.LikeString(text8, "*IS*NULL", CompareMethod.Binary) | (LikeOperator.LikeString(Strings.UCase(text7), "*" + Strings.UCase(array[num9, 2]) + ".*", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(text6), "*" + Strings.UCase(array[num9, 2]) + ".*", CompareMethod.Binary)))
						{
							goto IL_0795;
						}
						goto IL_0a58;
						IL_0795:
						num2 = 102;
						if (num10 == 1)
						{
							goto IL_07a6;
						}
						goto IL_097a;
						IL_07a6:
						num2 = 103;
						if (LikeOperator.LikeString(Strings.UCase(text8), "FULL*", CompareMethod.Binary))
						{
							goto IL_07c1;
						}
						goto IL_07f9;
						IL_07c1:
						num2 = 104;
						text8 = Strings.Replace(text8, "Full ", "", 1, -1, CompareMethod.Text);
						goto IL_07da;
						IL_07da:
						num2 = 105;
						text11 = Strings.Replace(text11, "@JOIN-TYPE@", "FULL OUTER JOIN", 1, -1, CompareMethod.Text);
						goto IL_0904;
						IL_07f9:
						num2 = 107;
						if (LikeOperator.LikeString(Strings.UCase(text8), "RIGHT*", CompareMethod.Binary))
						{
							goto IL_0814;
						}
						goto IL_084c;
						IL_0814:
						num2 = 108;
						text8 = Strings.Replace(text8, "Right ", "", 1, -1, CompareMethod.Text);
						goto IL_082d;
						IL_082d:
						num2 = 109;
						text11 = Strings.Replace(text11, "@JOIN-TYPE@", "RIGHT OUTER JOIN", 1, -1, CompareMethod.Text);
						goto IL_0904;
						IL_084c:
						num2 = 111;
						if (LikeOperator.LikeString(Strings.UCase(text8), "LEFT*", CompareMethod.Binary))
						{
							goto IL_0867;
						}
						goto IL_089c;
						IL_0867:
						num2 = 112;
						text8 = Strings.Replace(text8, "Left ", "", 1, -1, CompareMethod.Text);
						goto IL_0880;
						IL_0880:
						num2 = 113;
						text11 = Strings.Replace(text11, "@JOIN-TYPE@", "LEFT OUTER JOIN", 1, -1, CompareMethod.Text);
						goto IL_0904;
						IL_089c:
						num2 = 115;
						if (LikeOperator.LikeString(Strings.UCase(text8), "INNER*", CompareMethod.Binary))
						{
							goto IL_08b7;
						}
						goto IL_08ed;
						IL_08b7:
						num2 = 116;
						text8 = Strings.Replace(text8, "Inner ", "", 1, -1, CompareMethod.Text);
						goto IL_08d0;
						IL_08d0:
						num2 = 117;
						text11 = Strings.Replace(text11, "@JOIN-TYPE@", "INNER JOIN", 1, -1, CompareMethod.Text);
						goto IL_0904;
						IL_08ed:
						num2 = 119;
						text11 = Strings.Replace(text11, " @JOIN-TYPE@ ", replacement, 1, -1, CompareMethod.Text);
						goto IL_0904;
						IL_0904:
						num2 = 121;
						text2 = text2 + text11 + "\r\n" + CBBegin + "  ON " + text3 + text7 + " " + text8 + " " + text6 + text5 + " " + CBEnd;
						goto IL_096d;
						IL_096d:
						num2 = 122;
						num10 = 0;
						goto IL_0a72;
						IL_097a:
						num2 = 124;
						text8 = Strings.Replace(text8, "Full ", "", 1, -1, CompareMethod.Text);
						goto IL_0993;
						IL_0993:
						num2 = 125;
						text8 = Strings.Replace(text8, "Right ", "", 1, -1, CompareMethod.Text);
						goto IL_09ac;
						IL_09ac:
						num2 = 126;
						text8 = Strings.Replace(text8, "Left ", "", 1, -1, CompareMethod.Text);
						goto IL_09c5;
						IL_09c5:
						num2 = 127;
						text8 = Strings.Replace(text8, "Inner ", "", 1, -1, CompareMethod.Text);
						goto IL_09de;
						IL_09de:
						num2 = 128;
						text2 = text2 + Globals_Renamed.CRLF + CBBegin + " " + text4 + " " + text3 + text7 + " " + text8 + " " + text6 + text5 + " " + CBEnd;
						goto IL_0a72;
						IL_0a58:
						num2 = 132;
						num9 = (short)unchecked(num9 + 1);
						goto IL_0a65;
						IL_0a72:
						num2 = 135;
						num8 = (short)unchecked(num8 + 1);
						goto IL_0a7f;
						IL_00fe:
						num2 = 27;
						array[num5, 1] = Strings.Trim(Globals_Renamed.FromTables[num5].Table);
						goto IL_0121;
						IL_0121:
						num2 = 28;
						num7 = (short)Strings.InStrRev(array[num5, 1], " ");
						goto IL_013d;
						IL_013d:
						num2 = 29;
						if (num7 != 0)
						{
							goto IL_014e;
						}
						goto IL_0330;
						IL_014e:
						num2 = 30;
						array[num5, 2] = Strings.Mid(array[num5, 1], num7 + 1);
						goto IL_016e;
						IL_016e:
						num2 = 31;
						if (ll_ObjectType == 3)
						{
							goto IL_017b;
						}
						goto IL_01bc;
						IL_017b:
						num2 = 32;
						array[num5, 1] = "[" + Strings.Mid(array[num5, 1], 1, num7 - 1) + "] " + array[num5, 2];
						goto IL_0357;
						IL_01bc:
						num2 = 34;
						text = Strings.Mid(array[num5, 1], 1, num7 - 1);
						goto IL_01d5;
						IL_01d5:
						num2 = 35;
						num11 = (short)Strings.InStrRev(text, ".");
						goto IL_01e9;
						IL_01e9:
						num2 = 36;
						myExt = "";
						goto IL_01f3;
						IL_01f3:
						num2 = 37;
						if (num11 != 0)
						{
							goto IL_0201;
						}
						goto IL_0238;
						IL_0201:
						num2 = 38;
						myExt = Strings.UCase(Strings.Trim(Strings.Mid(text + " ", num11 + 1)));
						goto IL_0225;
						IL_0225:
						num2 = 39;
						text = Strings.Mid(text, 1, num11 - 1);
						goto IL_0238;
						IL_0238:
						num2 = 41;
						if (ll_ObjectType == 1)
						{
							goto IL_0248;
						}
						goto IL_02d0;
						IL_0248:
						num2 = 42;
						if (Strings.InStrRev(text, "\\") != 0 || LikeOperator.LikeString(text, "*<<<*>>>*", CompareMethod.Binary))
						{
							goto IL_0271;
						}
						goto IL_02a0;
						IL_0361:
						num2 = 59;
						num5 = (short)unchecked(num5 + 1);
						goto IL_036b;
						IL_02a0:
						num2 = 45;
						array[num5, 1] = Make_Valid_SQLite_Table(text, myExt, ll_JoinDuckDB) + " " + array[num5, 2];
						goto IL_0357;
						IL_0271:
						num2 = 43;
						array[num5, 1] = array[num5, 2] + " " + array[num5, 2];
						goto IL_0357;
						IL_02d0:
						num2 = 48;
						num11 = (short)Strings.InStrRev(text, "\\");
						goto IL_02e4;
						IL_02e4:
						num2 = 49;
						if (num11 != 0)
						{
							goto IL_02f2;
						}
						goto IL_0307;
						IL_02f2:
						num2 = 50;
						text = Strings.Trim(Strings.Mid(text, num11 + 1));
						goto IL_0307;
						end_IL_0001_2:
						break;
					}
					num2 = 146;
					result = text2;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3497;
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

	public static string Make_Valid_SQLite_Table(string MyTable, string MyExt, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 531:
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
							goto IL_001e;
						case 6:
							goto IL_0026;
						case 7:
							goto IL_0038;
						case 8:
							goto IL_0045;
						case 9:
							goto IL_0078;
						case 11:
							goto IL_008f;
						case 12:
							goto IL_00a8;
						case 14:
							goto IL_00bd;
						case 15:
							goto IL_0123;
						case 17:
							goto IL_0133;
						case 10:
						case 13:
						case 16:
						case 18:
						case 19:
							goto IL_0143;
						case 20:
							goto IL_0155;
						case 21:
							goto IL_016d;
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 22:
						case 24:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a8:
					num2 = 12;
					text += "_";
					goto IL_0143;
					IL_00bd:
					num2 = 14;
					if ((Operators.CompareString(text2, "0", TextCompare: false) >= 0 && Operators.CompareString(text2, "9", TextCompare: false) <= 0) || (Operators.CompareString(Strings.UCase(text2), "A", TextCompare: false) >= 0 && Operators.CompareString(Strings.UCase(text2), "Z", TextCompare: false) <= 0) || Operators.CompareString(text2, "_", TextCompare: false) == 0)
					{
						goto IL_0123;
					}
					goto IL_0133;
					IL_008f:
					num2 = 11;
					if (Operators.CompareString(text2, " ", TextCompare: false) == 0)
					{
						goto IL_00a8;
					}
					goto IL_00bd;
					IL_0123:
					num2 = 15;
					text += text2;
					goto IL_0143;
					IL_000b:
					num2 = 2;
					text2 = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					num5 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					num6 = 0;
					goto IL_001e;
					IL_001e:
					num2 = 5;
					text = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					num7 = Strings.Len(MyTable);
					num6 = 1;
					goto IL_014c;
					IL_014c:
					if (num6 <= num7)
					{
						goto IL_0038;
					}
					goto IL_0155;
					IL_0155:
					num2 = 20;
					if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_016d;
					IL_016d:
					num2 = 21;
					text = "<DDBTable>" + text + "</DDBTable>";
					goto end_IL_0001_3;
					IL_0038:
					num2 = 7;
					text2 = Strings.Mid(MyTable, num6, 1);
					goto IL_0045;
					IL_0045:
					num2 = 8;
					if (num6 == 1 && Operators.CompareString(text2, "0", TextCompare: false) >= 0 && Operators.CompareString(text2, "9", TextCompare: false) <= 0)
					{
						goto IL_0078;
					}
					goto IL_008f;
					IL_0133:
					num2 = 17;
					text += "_";
					goto IL_0143;
					IL_0143:
					num2 = 19;
					num6 = checked(num6 + 1);
					goto IL_014c;
					IL_0078:
					num2 = 9;
					text = text + "T" + text2;
					goto IL_0143;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				text = "[" + text + "]";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 531;
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

	public static string Check_Joins()
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
						errsource = "BuildSQL - Check_Joins";
						string text = "";
						string text2 = "";
						string text3 = "";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						result = "";
						short num7 = (short)(Globals_Renamed.MaxFromTables - 1);
						for (num4 = 0; num4 <= num7; num4 = (short)unchecked(num4 + 1))
						{
							text3 = Strings.Trim(Globals_Renamed.FromTables[num4].Table);
							num3 = (short)Strings.InStrRev(text3, " ");
							text3 = ((num3 == 0) ? "" : (Strings.UCase(Strings.Trim(Strings.Mid(text3, num3 + 1))) + "."));
							num6 = 0;
							short g_NoQJoins = Globals_Renamed.g_NoQJoins;
							for (num5 = 0; num5 <= g_NoQJoins; num5 = (short)unchecked(num5 + 1))
							{
								if (((Operators.CompareString(Strings.Trim(Globals_Renamed.g_QJoins[num5].Col1), "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(Globals_Renamed.g_QJoins[num5].Col2), "", TextCompare: false) != 0)) && (((Strings.InStr(Strings.UCase(Globals_Renamed.g_QJoins[num5].Col1), text3) != 0) & (Strings.InStr(Strings.UCase(Globals_Renamed.g_QJoins[num5].Col2), text3) == 0)) | ((Strings.InStr(Strings.UCase(Globals_Renamed.g_QJoins[num5].Col2), text3) != 0) & (Strings.InStr(Strings.UCase(Globals_Renamed.g_QJoins[num5].Col1), text3) == 0))))
								{
									num6++;
									break;
								}
							}
							if (num6 == 0)
							{
								if (Operators.CompareString(Strings.Right(text3, 1), ".", TextCompare: false) == 0)
								{
									text3 = Strings.Mid(text3, 1, Strings.Len(text3) - 1);
								}
								if (LikeOperator.LikeString(text3, "A*", CompareMethod.Binary))
								{
									text3 = Strings.LCase(text3);
								}
								result = text3;
								break;
							}
						}
						goto end_IL_0001;
					}
					case 590:
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
				try0001_dispatch = 590;
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

	public static string Create_SQLite_Idx(string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text10 = default(string);
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
						errsource = "BuildSQL - Creast_SQLite_Idx";
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						string text5 = "";
						short num3 = 0;
						short num4 = 0;
						short num5 = 0;
						short num6 = 0;
						string text6 = "";
						string text7 = "";
						string text8 = "";
						string text9 = "";
						text10 = "";
						string text11 = ((Operators.CompareString(ll_JoinDuckDB, "N", TextCompare: false) != 0) ? " " : " IF NOT EXISTS ");
						short num7 = (short)(Globals_Renamed.MaxFromTables - 1);
						for (num5 = 1; num5 <= num7; num5 = (short)unchecked(num5 + 1))
						{
							text6 = "";
							text7 = "";
							text3 = Strings.Trim(Globals_Renamed.FromTables[num5].Table);
							num3 = (short)Strings.InStrRev(text3, ".");
							if (num3 != 0)
							{
								text8 = Strings.UCase(Strings.Trim(Strings.Mid(text3 + "    ", num3 + 1, 3)));
								text9 = "";
								text5 = Strings.Trim(Strings.Mid(text3, 1, num3 - 1));
								text3 = Strings.Mid(text3, num3 + 1);
								num4 = (short)Strings.InStr(text3, " ");
								if (num4 != 0)
								{
									text3 = Strings.UCase(Strings.Trim(Strings.Mid(text3, num4 + 1)));
								}
								text4 = ((!LikeOperator.LikeString(text5, "*<<<*>>>*", CompareMethod.Binary)) ? Make_Valid_SQLite_Table(text5, "", ll_JoinDuckDB) : ((Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) != 0) ? ("[" + text3 + "]") : ("\"" + text3 + "\"")));
								text9 = "";
								short g_NoQJoins = Globals_Renamed.g_NoQJoins;
								for (num6 = 0; num6 <= g_NoQJoins; num6 = (short)unchecked(num6 + 1))
								{
									if ((Operators.CompareString(Strings.Trim(Globals_Renamed.g_QJoins[num6].Col1), "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Trim(Globals_Renamed.g_QJoins[num6].Col2), "", TextCompare: false) != 0))
									{
										Chk_SQLite_Idx_Col(Globals_Renamed.g_QJoins[num6].Col1, text3, ref text7, ll_JoinDuckDB);
										Chk_SQLite_Idx_Col(Globals_Renamed.g_QJoins[num6].Col2, text3, ref text7, ll_JoinDuckDB);
									}
								}
								if (Operators.CompareString(text7, "", TextCompare: false) != 0)
								{
									text7 = Strings.Mid(text7, 2);
									text10 = text10 + "\r\nDROP INDEX IF EXISTS " + text9 + "Idx" + text3 + ";\r\nCreate Index" + text11 + text9 + "Idx" + text3 + " ON " + text4 + " (" + text7 + ");";
								}
							}
						}
						goto end_IL_0001;
					}
					case 834:
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
				try0001_dispatch = 834;
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
		return text10;
	}

	public static void Chk_SQLite_Idx_Col(string MyColumn, string MyAlias, ref string Join_Cols, string ll_JoinDuckDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		string right = default(string);
		string stringMatch = default(string);
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
				case 404:
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
							goto IL_002f;
						case 6:
							goto IL_0038;
						case 8:
							goto IL_0045;
						case 9:
							goto IL_004e;
						case 7:
						case 10:
						case 11:
							goto IL_0059;
						case 12:
							goto IL_007c;
						case 13:
							goto IL_008c;
						case 14:
							goto IL_009c;
						case 15:
							goto IL_00af;
						case 16:
							goto IL_00ec;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 18:
						case 19:
						case 20:
						case 21:
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_009c:
					num2 = 14;
					text = Strings.Trim(Strings.Mid(MyColumn, checked(num5 + 1)));
					goto IL_00af;
					IL_00af:
					num2 = 15;
					if (Operators.CompareString(Strings.Mid(text + " ", 1, 1), right, TextCompare: false) != 0 || Strings.InStrRev(text, stringMatch) != Strings.Len(text))
					{
						goto end_IL_0001_3;
					}
					goto IL_00ec;
					IL_008c:
					num2 = 13;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_009c;
					IL_00ec:
					num2 = 16;
					if (Strings.InStr(Strings.UCase(Join_Cols), Strings.UCase(text)) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) == 0)
					{
						goto IL_002f;
					}
					goto IL_0045;
					IL_002f:
					num2 = 5;
					right = "\"";
					goto IL_0038;
					IL_0038:
					num2 = 6;
					stringMatch = "\"";
					goto IL_0059;
					IL_0045:
					num2 = 8;
					right = "[";
					goto IL_004e;
					IL_004e:
					num2 = 9;
					stringMatch = "]";
					goto IL_0059;
					IL_0059:
					num2 = 11;
					if (!LikeOperator.LikeString(Strings.UCase(MyColumn), MyAlias + ".*", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_007c;
					IL_007c:
					num2 = 12;
					num5 = Strings.InStr(MyColumn, ".");
					goto IL_008c;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				Join_Cols = Join_Cols + "," + text;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 404;
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

	public static string Process_In_List(string MyList, string CBBegin, string CBEnd, string MyMode)
	{
		string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
		int num = 0;
		int num2 = 0;
		string delim = ",";
		string text = "(";
		string text2 = ")";
		if (Operators.CompareString(Strings.Trim(Strings.UCase(MyMode)), "M", TextCompare: false) == 0)
		{
			text = "[";
			text2 = "]";
		}
		string text3 = "";
		if (Strings.InStr(MyList, "</comma\\>") != 0)
		{
			delim = "</comma\\>";
		}
		num = General_Procedures.ParseAndFillArray(MyList, delim, ref DynArray);
		int num3 = num;
		for (num2 = 1; num2 <= num3; num2 = checked(num2 + 1))
		{
			text3 = ((num2 != 1) ? (text3 + CBEnd + "\r\n" + CBBegin + "," + DynArray[num2]) : (text + DynArray[num2]));
		}
		text3 += text2;
		DynArray = null;
		return text3;
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static string Write_VA_Lib(string MyVaLib)
	{
		int try0001_dispatch = -1;
		short num3 = default(short);
		int num2 = default(int);
		string result;
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
						result = "";
						num3 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						num3 = (short)FileSystem.FreeFile();
						FileSystem.FileOpen(num3, Globals_Renamed.MySchemaDir + "\\" + Strings.Trim(MyVaLib), OpenMode.Input, OpenAccess.Read, OpenShare.Shared);
						result = FileSystem.InputString(num3, (int)FileSystem.LOF(num3));
						FileSystem.FileClose(num3);
						goto end_IL_0001;
					case 199:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Error reading file " + MyVaLib + ". (" + Conversion.ErrorToString() + ".)" + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Read Error");
							Information.Err().Clear();
							FileSystem.FileClose(num3);
							result = "";
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 199;
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

	public static void RunQuery(string Query_To_Run, short Query_Code, short MyOpt, short ll_ObjectType, string ll_MacroFile)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		long num6 = default(long);
		long num7 = default(long);
		long num8 = default(long);
		long num9 = default(long);
		long num10 = default(long);
		string text6 = default(string);
		int num15 = default(int);
		int num16 = default(int);
		string[] array = default(string[]);
		int num17 = default(int);
		string text7 = default(string);
		string text8 = default(string);
		string find = default(string);
		bool flag = default(bool);
		string text9 = default(string);
		FrmInput frmInput = default(FrmInput);
		frmfilename frmfilename2 = default(frmfilename);
		frmfilename frmfilename3 = default(frmfilename);
		int num18 = default(int);
		frmgetdata frmgetdata2 = default(frmgetdata);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					long num12;
					string text4;
					string text5;
					long num13;
					long num14;
					long num5;
					short num11;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 3550:
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
								goto IL_0022;
							case 6:
								goto IL_0028;
							case 7:
								goto IL_002e;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_003a;
							case 10:
								goto IL_0041;
							case 11:
								goto IL_0047;
							case 12:
								goto IL_004e;
							case 13:
								goto IL_0058;
							case 14:
								goto IL_0062;
							case 15:
								goto IL_006c;
							case 16:
								goto IL_0076;
							case 17:
								goto IL_007d;
							case 18:
								goto IL_0084;
							case 19:
								goto IL_008a;
							case 20:
								goto IL_0090;
							case 21:
								goto IL_009c;
							case 22:
								goto IL_00a2;
							case 23:
								goto IL_00ac;
							case 24:
								goto IL_00b6;
							case 25:
								goto IL_00c0;
							case 26:
								goto IL_00c6;
							case 27:
								goto IL_00d0;
							case 28:
								goto IL_00dc;
							case 29:
								goto IL_00e6;
							case 30:
								goto IL_00f3;
							case 31:
								goto IL_0154;
							case 32:
								goto IL_015a;
							case 33:
								goto IL_0185;
							case 34:
								goto IL_0193;
							case 35:
								goto IL_019c;
							case 36:
								goto IL_01a6;
							case 37:
								goto IL_01b2;
							case 38:
								goto IL_01bf;
							case 39:
								goto IL_01c6;
							case 40:
							case 41:
								goto IL_01d3;
							case 42:
								goto IL_01e4;
							case 43:
								goto IL_01f6;
							case 44:
								goto IL_0211;
							case 45:
								goto IL_0226;
							case 46:
								goto IL_0235;
							case 47:
								goto IL_0260;
							case 49:
							case 50:
								goto IL_0271;
							case 51:
								goto IL_028d;
							case 52:
								goto IL_02aa;
							case 53:
								goto IL_02b9;
							case 55:
								goto IL_041f;
							case 56:
								goto IL_043b;
							case 57:
								goto IL_0447;
							case 58:
								goto IL_047e;
							case 59:
								goto IL_048d;
							case 60:
								goto IL_0497;
							case 61:
								goto IL_04a8;
							case 62:
								goto IL_04c3;
							case 63:
								goto IL_050f;
							case 64:
							case 65:
								goto IL_0523;
							case 66:
								goto IL_052e;
							case 68:
								goto IL_054f;
							case 69:
								goto IL_0560;
							case 70:
								goto IL_056f;
							case 71:
								goto IL_0578;
							case 72:
							case 73:
								goto IL_0587;
							case 74:
								goto IL_0592;
							case 75:
								goto IL_05a3;
							case 76:
								goto IL_05b9;
							case 79:
								goto IL_05d8;
							case 80:
								goto IL_05f2;
							case 84:
								goto IL_060b;
							case 85:
								goto IL_0627;
							case 86:
								goto IL_0634;
							case 87:
								goto IL_0641;
							case 88:
								goto IL_064b;
							case 89:
								goto IL_0656;
							case 91:
								goto IL_069f;
							case 92:
								goto IL_06b2;
							case 94:
								goto IL_06c9;
							case 95:
								goto IL_06d6;
							case 96:
								goto IL_06e0;
							case 97:
								goto IL_06eb;
							case 99:
								goto IL_070c;
							case 101:
								goto IL_0723;
							case 102:
								goto IL_075d;
							case 103:
								goto IL_0766;
							case 105:
								goto IL_0787;
							case 107:
								goto IL_079b;
							case 108:
								goto IL_07bc;
							case 111:
								goto IL_07de;
							case 112:
								goto IL_07ee;
							case 113:
								goto IL_0800;
							case 114:
								goto IL_0811;
							case 115:
								goto IL_0822;
							case 116:
								goto IL_0832;
							case 117:
								goto IL_084b;
							case 118:
								goto IL_0864;
							case 119:
								goto IL_086e;
							case 120:
								goto IL_087a;
							case 121:
								goto IL_0886;
							case 122:
								goto IL_0891;
							case 123:
								goto IL_08a0;
							case 124:
								goto IL_08ab;
							case 126:
								goto IL_08cc;
							case 54:
							case 77:
							case 78:
							case 82:
							case 83:
							case 93:
							case 100:
							case 106:
							case 109:
							case 110:
							case 127:
							case 128:
							case 129:
								goto IL_08e8;
							case 130:
							case 131:
								goto IL_08f7;
							case 132:
								goto IL_0909;
							case 133:
								goto IL_091e;
							case 134:
								goto IL_0936;
							case 135:
								goto IL_0947;
							case 136:
								goto IL_0966;
							case 137:
								goto IL_0986;
							case 139:
								goto IL_099a;
							case 140:
								goto IL_09b4;
							case 142:
								goto IL_09c5;
							case 143:
								goto IL_09e4;
							case 145:
								goto IL_09f8;
							case 151:
								goto IL_0a1c;
							case 154:
								goto IL_0a40;
							case 155:
								goto IL_0a50;
							case 157:
								goto IL_0a6d;
							case 158:
								goto IL_0a7d;
							case 67:
							case 81:
							case 90:
							case 98:
							case 104:
							case 125:
							case 162:
								goto IL_0a9c;
							case 164:
								goto IL_0aaf;
							case 165:
								goto IL_0ac4;
							case 167:
								goto IL_0ad7;
							case 168:
								goto IL_0ae7;
							case 48:
							case 138:
							case 141:
							case 144:
							case 146:
							case 147:
							case 148:
							case 149:
							case 150:
							case 152:
							case 153:
							case 156:
							case 159:
							case 160:
							case 161:
							case 163:
							case 166:
							case 169:
							case 170:
							case 171:
								goto IL_0afb;
							case 172:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 173:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_09e4:
						num2 = 143;
						Run_Help_Query_py(text, MyOpt, ll_ObjectType);
						goto IL_0afb;
						IL_09f8:
						num2 = 145;
						Interaction.MsgBox("Help Queries are no longer supported for the Legacy Engine", MsgBoxStyle.Information, "Not Supported");
						goto IL_0afb;
						IL_09c5:
						num2 = 142;
						if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
						{
							goto IL_09e4;
						}
						goto IL_09f8;
						IL_0a1c:
						num2 = 151;
						if (Operators.CompareString(Query_To_Run, "CANCEL", TextCompare: false) != 0)
						{
							goto IL_0a40;
						}
						goto IL_0afb;
						IL_000b:
						num2 = 2;
						text2 = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						text3 = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						num5 = 0L;
						goto IL_0022;
						IL_0022:
						num2 = 5;
						num6 = 0L;
						goto IL_0028;
						IL_0028:
						num2 = 6;
						num7 = 0L;
						goto IL_002e;
						IL_002e:
						num2 = 7;
						num8 = 0L;
						goto IL_0034;
						IL_0034:
						num2 = 8;
						num9 = 0L;
						goto IL_003a;
						IL_003a:
						num2 = 9;
						num10 = 0L;
						goto IL_0041;
						IL_0041:
						num2 = 10;
						num11 = 0;
						goto IL_0047;
						IL_0047:
						num2 = 11;
						num12 = 0L;
						goto IL_004e;
						IL_004e:
						num2 = 12;
						text = "";
						goto IL_0058;
						IL_0058:
						num2 = 13;
						text4 = "";
						goto IL_0062;
						IL_0062:
						num2 = 14;
						text5 = "";
						goto IL_006c;
						IL_006c:
						num2 = 15;
						text6 = "";
						goto IL_0076;
						IL_0076:
						num2 = 16;
						num13 = 0L;
						goto IL_007d;
						IL_007d:
						num2 = 17;
						num14 = 0L;
						goto IL_0084;
						IL_0084:
						num2 = 18;
						num15 = -1;
						goto IL_008a;
						IL_008a:
						num2 = 19;
						num16 = 0;
						goto IL_0090;
						IL_0090:
						num2 = 20;
						array = new string[16];
						goto IL_009c;
						IL_009c:
						num2 = 21;
						num17 = 0;
						goto IL_00a2;
						IL_00a2:
						num2 = 22;
						text7 = "";
						goto IL_00ac;
						IL_00ac:
						num2 = 23;
						text8 = "";
						goto IL_00b6;
						IL_00b6:
						num2 = 24;
						find = "";
						goto IL_00c0;
						IL_00c0:
						num2 = 25;
						flag = false;
						goto IL_00c6;
						IL_00c6:
						num2 = 26;
						text9 = "";
						goto IL_00d0;
						IL_00d0:
						num2 = 27;
						Query_To_Run = BuildForm.Replace_Globals(Query_To_Run);
						goto IL_00dc;
						IL_00dc:
						num2 = 28;
						text = "";
						goto IL_00e6;
						IL_00e6:
						num2 = 29;
						Globals_Renamed.gWorkQuery = "";
						goto IL_00f3;
						IL_00f3:
						num2 = 30;
						if (Strings.InStr(Query_To_Run, "FROM Insp_Wafer_Summary y0 LEFT JOIN Insp_Defect y901") != 0 || Strings.InStr(Query_To_Run, "CROSS JOIN A_Seq_1_To_9") != 0 || Strings.InStr(Query_To_Run, "GROUP BY c99.[CollateralID]) c100 ON c100.[CollateralID] = c0.[CollateralID]") != 0 || Strings.InStr(Query_To_Run, "FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha") != 0 || Strings.InStr(Query_To_Run, "GROUP BY a1.spcs_id) v44 ON v44.spcs_id = a") != 0 || Strings.InStr(Query_To_Run, "GROUP BY a0.lao_start_ww, a0.obj_s_id, omtr.obj_mt_id") != 0)
						{
							goto IL_0154;
						}
						goto IL_015a;
						IL_0a40:
						num2 = 154;
						if (Query_Code == 1)
						{
							goto IL_0a50;
						}
						goto IL_0a6d;
						IL_0a50:
						num2 = 155;
						num11 = (short)Interaction.MsgBox("You have not yet generated a query.", MsgBoxStyle.Exclamation, "No Query Selected!");
						goto IL_0afb;
						IL_0a6d:
						num2 = 157;
						if (Query_Code == 2)
						{
							goto IL_0a7d;
						}
						goto IL_0afb;
						IL_0a7d:
						num2 = 158;
						num11 = (short)Interaction.MsgBox("No help available for this column.", MsgBoxStyle.Exclamation, "No Help Available!");
						goto IL_0afb;
						IL_0afb:
						num2 = 171;
						array = null;
						break;
						IL_0154:
						num2 = 31;
						flag = true;
						goto IL_015a;
						IL_015a:
						num2 = 32;
						if ((Operators.CompareString(Query_To_Run, "", TextCompare: false) != 0) & (Operators.CompareString(Query_To_Run, "CANCEL", TextCompare: false) != 0))
						{
							goto IL_0185;
						}
						goto IL_0a1c;
						IL_0185:
						num2 = 33;
						Cursor.Current = Cursors.WaitCursor;
						goto IL_0193;
						IL_0193:
						num2 = 34;
						text2 = "&PROMPT&(";
						goto IL_019c;
						IL_019c:
						num2 = 35;
						text3 = ")&PROMPT&";
						goto IL_01a6;
						IL_01a6:
						num2 = 36;
						num9 = Strings.Len(text2);
						goto IL_01b2;
						IL_01b2:
						num2 = 37;
						num10 = Strings.Len(text3);
						goto IL_01bf;
						IL_01bf:
						num2 = 38;
						num6 = 1L;
						goto IL_01c6;
						IL_01c6:
						num2 = 39;
						num5 = Strings.Len(Query_To_Run);
						goto IL_01d3;
						IL_01d3:
						num2 = 41;
						num7 = Strings.InStr((int)num6, Query_To_Run, text2, CompareMethod.Text);
						goto IL_01e4;
						IL_01e4:
						num2 = 42;
						if (num7 != 0)
						{
							goto IL_01f6;
						}
						goto IL_08f7;
						IL_01f6:
						num2 = 43;
						text += Strings.Mid(Query_To_Run, (int)num6, (int)(num7 - num6));
						goto IL_0211;
						IL_0211:
						num2 = 44;
						num8 = Strings.InStr((int)(num7 + 1), Query_To_Run, text3, CompareMethod.Text);
						goto IL_0226;
						IL_0226:
						num2 = 45;
						if (num8 == 0)
						{
							goto IL_0235;
						}
						goto IL_0271;
						IL_0235:
						num2 = 46;
						num11 = (short)Interaction.MsgBox(General_Procedures.Get_UI("errprompt") + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Parsing Query!");
						goto IL_0260;
						IL_0260:
						num2 = 47;
						text = "";
						goto IL_0afb;
						IL_0271:
						num2 = 50;
						find = Strings.Trim(Strings.Mid(Query_To_Run, (int)num7, (int)(num8 + num10 - num7)));
						goto IL_028d;
						IL_028d:
						num2 = 51;
						Globals_Renamed.currinputstrtmp = Strings.Mid(Query_To_Run, (int)(num7 + num9), (int)(num8 - (num7 + num9)));
						goto IL_02aa;
						IL_02aa:
						num2 = 52;
						Cursor.Current = Cursors.Default;
						goto IL_02b9;
						IL_02b9:
						num2 = 53;
						switch (Strings.UCase(Strings.Mid(Globals_Renamed.currinputstrtmp, 1, 4)))
						{
						case "DATA":
							break;
						case "EXCE":
						case "FILE":
							goto IL_060b;
						case "LIMI":
							goto IL_06c9;
						case "INFI":
						case "ILFI":
						case "IGFI":
							goto IL_0723;
						case "INPU":
							goto IL_07de;
						default:
							goto IL_08e8;
						}
						goto IL_041f;
						IL_07de:
						num2 = 111;
						text8 = Strings.Mid(Globals_Renamed.currinputstrtmp, 6);
						goto IL_07ee;
						IL_07ee:
						num2 = 112;
						num17 = Strings.InStr(text8, "\" \"");
						goto IL_0800;
						IL_0800:
						num2 = 113;
						if (num17 != 0)
						{
							goto IL_0811;
						}
						goto IL_08e8;
						IL_0811:
						num2 = 114;
						text7 = Strings.Mid(text8, 1, num17 - 1);
						goto IL_0822;
						IL_0822:
						num2 = 115;
						text8 = Strings.Mid(text8, num17 + 3);
						goto IL_0832;
						IL_0832:
						num2 = 116;
						text7 = Strings.Replace(text7, "\"", "", 1, -1, CompareMethod.Text);
						goto IL_084b;
						IL_084b:
						num2 = 117;
						text8 = Strings.Replace(text8, "\"", "", 1, -1, CompareMethod.Text);
						goto IL_0864;
						IL_0864:
						num2 = 118;
						frmInput = new FrmInput();
						goto IL_086e;
						IL_086e:
						num2 = 119;
						frmInput.f_InputFile = text7;
						goto IL_087a;
						IL_087a:
						num2 = 120;
						frmInput.F_OutputFile = text8;
						goto IL_0886;
						IL_0886:
						num2 = 121;
						frmInput.ShowDialog();
						goto IL_0891;
						IL_0891:
						num2 = 122;
						Globals_Renamed.currinputstrtmp = frmInput.F_Output;
						goto IL_08a0;
						IL_08a0:
						num2 = 123;
						frmInput.Dispose();
						goto IL_08ab;
						IL_08ab:
						num2 = 124;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "CANCEL", TextCompare: false) != 0)
						{
							goto IL_08cc;
						}
						goto IL_0a9c;
						IL_08cc:
						num2 = 126;
						text += BuildForm.Replace_Globals(Globals_Renamed.currinputstrtmp);
						goto IL_08e8;
						IL_0723:
						num2 = 101;
						if ((Query_Code != 4) | (((Query_Code == 4) & (Operators.CompareString(Globals_Renamed.currinputstrtmp, "INFILE^[C]&PROMPT&", TextCompare: false) == 0)) | LikeOperator.LikeString(Globals_Renamed.currinputstrtmp, "ILFILE*", CompareMethod.Binary)))
						{
							goto IL_075d;
						}
						goto IL_079b;
						IL_075d:
						num2 = 102;
						File_To_InList();
						goto IL_0766;
						IL_0766:
						num2 = 103;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "", TextCompare: false) != 0)
						{
							goto IL_0787;
						}
						goto IL_0a9c;
						IL_0787:
						num2 = 105;
						text += Globals_Renamed.currinputstrtmp;
						goto IL_08e8;
						IL_079b:
						num2 = 107;
						if ((Query_Code == 4) & (Operators.CompareString(Globals_Renamed.currinputstrtmp, "INFILE^[C]&PROMPT&", TextCompare: false) != 0))
						{
							goto IL_07bc;
						}
						goto IL_08e8;
						IL_07bc:
						num2 = 108;
						text = text + "&PROMPT&(" + Globals_Renamed.currinputstrtmp + ")&PROMPT&";
						goto IL_08e8;
						IL_06c9:
						num2 = 94;
						Globals_Renamed.currvaluetmp = "&RUN-TIME&";
						goto IL_06d6;
						IL_06d6:
						num2 = 95;
						frmfilename2 = new frmfilename();
						goto IL_06e0;
						IL_06e0:
						num2 = 96;
						frmfilename2.ShowDialog();
						goto IL_06eb;
						IL_06eb:
						num2 = 97;
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) != 0)
						{
							goto IL_070c;
						}
						goto IL_0a9c;
						IL_070c:
						num2 = 99;
						text += Globals_Renamed.currvaluetmp;
						goto IL_08e8;
						IL_060b:
						num2 = 84;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "FILE", TextCompare: false) == 0)
						{
							goto IL_0627;
						}
						goto IL_0634;
						IL_0627:
						num2 = 85;
						Globals_Renamed.currinputstrtmp = "EXCE";
						goto IL_0634;
						IL_0634:
						num2 = 86;
						Globals_Renamed.currvaluetmp = "&RUN-TIME&";
						goto IL_0641;
						IL_0641:
						num2 = 87;
						frmfilename3 = new frmfilename();
						goto IL_064b;
						IL_064b:
						num2 = 88;
						frmfilename3.ShowDialog();
						goto IL_0656;
						IL_0656:
						num2 = 89;
						if (!((Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "PROMPT", TextCompare: false) == 0) | (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)))
						{
							goto IL_069f;
						}
						goto IL_0a9c;
						IL_069f:
						num2 = 91;
						Globals_Renamed.ODBC_Prompt = BuildForm.Replace_Globals(Globals_Renamed.currvaluetmp);
						goto IL_06b2;
						IL_06b2:
						num2 = 92;
						text += Globals_Renamed.ODBC_Prompt;
						goto IL_08e8;
						IL_041f:
						num2 = 55;
						if (LikeOperator.LikeString(Globals_Renamed.currinputstrtmp, "DATA*^*^*^*^*^*^*", CompareMethod.Binary))
						{
							goto IL_043b;
						}
						goto IL_05d8;
						IL_043b:
						num2 = 56;
						num18 = num15;
						num16 = 0;
						goto IL_0487;
						IL_0487:
						if (num16 <= num18)
						{
							goto IL_0447;
						}
						goto IL_048d;
						IL_048d:
						num2 = 59;
						frmgetdata2 = new frmgetdata();
						goto IL_0497;
						IL_0497:
						num2 = 60;
						frmgetdata2.Tag = Conversions.ToString(unchecked((int)MyOpt));
						goto IL_04a8;
						IL_04a8:
						num2 = 61;
						text6 = Strings.Mid(Globals_Renamed.currinputstrtmp + " ", 5, 1);
						goto IL_04c3;
						IL_04c3:
						num2 = 62;
						if ((Operators.CompareString(text6, "1", TextCompare: false) == 0) | (Operators.CompareString(text6, "2", TextCompare: false) == 0) | (Operators.CompareString(text6, "3", TextCompare: false) == 0) | (Operators.CompareString(text6, "4", TextCompare: false) == 0))
						{
							goto IL_050f;
						}
						goto IL_0523;
						IL_050f:
						num2 = 63;
						frmgetdata2.ll_ObjectType = (short)Conversions.ToInteger(text6);
						goto IL_0523;
						IL_0523:
						num2 = 65;
						frmgetdata2.ShowDialog();
						goto IL_052e;
						IL_052e:
						num2 = 66;
						if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "CANCEL", TextCompare: false) != 0)
						{
							goto IL_054f;
						}
						goto IL_0a9c;
						IL_054f:
						num2 = 68;
						text += Globals_Renamed.currinputstrtmp;
						goto IL_0560;
						IL_0560:
						num2 = 69;
						if (num15 < 14)
						{
							goto IL_056f;
						}
						goto IL_0587;
						IL_056f:
						num2 = 70;
						num15++;
						goto IL_0578;
						IL_0578:
						num2 = 71;
						array[num15] = Globals_Renamed.currinputstrtmp;
						goto IL_0587;
						IL_0587:
						num2 = 73;
						if (flag)
						{
							goto IL_0592;
						}
						goto IL_08e8;
						IL_0592:
						num2 = 74;
						text9 = Strings.Mid(Query_To_Run, (int)(num8 + num10));
						goto IL_05a3;
						IL_05a3:
						num2 = 75;
						text9 = Strings.Replace(text9, find, Globals_Renamed.currinputstrtmp, 1, -1, CompareMethod.Text);
						goto IL_05b9;
						IL_05b9:
						num2 = 76;
						Query_To_Run = Strings.Mid(Query_To_Run, 1, (int)(num8 + num10)) + text9;
						goto IL_08e8;
						IL_08e8:
						num2 = 129;
						num6 = num8 + num10;
						goto IL_08f7;
						IL_0447:
						num2 = 57;
						Globals_Renamed.currinputstrtmp = Strings.Replace(Globals_Renamed.currinputstrtmp, "@spf-arg-" + Strings.Trim(Conversions.ToString(num16 + 1)) + "@", array[num16], 1, -1, CompareMethod.Text);
						goto IL_047e;
						IL_047e:
						num2 = 58;
						num16++;
						goto IL_0487;
						IL_05d8:
						num2 = 79;
						Interaction.MsgBox(General_Procedures.Get_UI("sqlprompthelp"), MsgBoxStyle.Exclamation, "Invalid Prompt String Specified");
						goto IL_05f2;
						IL_05f2:
						num2 = 80;
						Globals_Renamed.currinputstrtmp = "CANCEL";
						goto IL_0a9c;
						IL_0a9c:
						num2 = 162;
						if (Query_Code != 1)
						{
							goto IL_0aaf;
						}
						goto IL_0afb;
						IL_0aaf:
						num2 = 164;
						if (unchecked(Query_Code == 4 || Query_Code == 5))
						{
							goto IL_0ac4;
						}
						goto IL_0ad7;
						IL_0ac4:
						num2 = 165;
						Globals_Renamed.gWorkQuery = "";
						goto IL_0afb;
						IL_0ad7:
						num2 = 167;
						if (Query_Code == 8)
						{
							goto IL_0ae7;
						}
						goto IL_0afb;
						IL_0ae7:
						num2 = 168;
						Globals_Renamed.gWorkQuery = "CANCEL";
						goto IL_0afb;
						IL_08f7:
						num2 = 131;
						Cursor.Current = Cursors.WaitCursor;
						goto IL_0909;
						IL_0909:
						num2 = 132;
						if (num7 != 0)
						{
							goto IL_01d3;
						}
						goto IL_091e;
						IL_091e:
						num2 = 133;
						text += Strings.Mid(Query_To_Run, (int)num6);
						goto IL_0936;
						IL_0936:
						num2 = 134;
						Cursor.Current = Cursors.Default;
						goto IL_0947;
						IL_0947:
						num2 = 135;
						if (Operators.CompareString(text, "CANCEL", TextCompare: false) != 0)
						{
							goto IL_0966;
						}
						goto IL_0afb;
						IL_0966:
						num2 = 136;
						if (unchecked(Query_Code == 1 || Query_Code == 3 || Query_Code == 6 || Query_Code == 9))
						{
							goto IL_0986;
						}
						goto IL_099a;
						IL_0986:
						num2 = 137;
						Run_The_Query(text, Query_Code, ll_MacroFile);
						goto IL_0afb;
						IL_099a:
						num2 = 139;
						if (unchecked(Query_Code == 4 || Query_Code == 5 || Query_Code == 8))
						{
							goto IL_09b4;
						}
						goto IL_09c5;
						IL_09b4:
						num2 = 140;
						Globals_Renamed.gWorkQuery = text;
						goto IL_0afb;
						end_IL_0001_2:
						break;
					}
					num2 = 172;
					Cursor.Current = Cursors.Default;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3550;
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

	public static void Run_Help_Query_py(string Query_To_Run, short MyOpt, short ll_ObjectType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myFile = default(string);
		string text = default(string);
		string Query_To_Run2 = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string myJob = default(string);
		int num10 = default(int);
		int num11 = default(int);
		string SQLCode = default(string);
		string OUser = default(string);
		string OPwd = default(string);
		string text2 = default(string);
		bool flag2 = default(bool);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text13 = default(string);
		string text14 = default(string);
		string text17 = default(string);
		string text18 = default(string);
		string text19 = default(string);
		string text20 = default(string);
		string text21 = default(string);
		string MyErr = default(string);
		string text22 = default(string);
		string myNodeKind = default(string);
		string left = default(string);
		string left2 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num4;
					int num7;
					int num8;
					int num9;
					bool flag;
					int num12;
					int num13;
					string text12;
					string text15;
					string text16;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_000a;
					case 5441:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
								break;
							case 1:
								goto IL_10e9;
							default:
								goto end_IL_0001;
							}
							goto IL_1044;
						}
						IL_0fd7:
						num2 = 259;
						myFile = Strings.Trim(Globals_Renamed.MyPCDir + text);
						goto IL_0ff0;
						IL_0ff0:
						num2 = 260;
						Query_To_Run2 = BuildForm.Replace_Globals(Query_To_Run2, 1);
						goto IL_0ffe;
						IL_0fae:
						num2 = 258;
						Query_To_Run2 = SubStitute_Node_Schema(ref Query_To_Run2, 1, "", "", "", "", "0", IsHlpQ: true);
						goto IL_0fd7;
						IL_10e9:
						num4 = unchecked(num + 1);
						goto IL_10ec;
						IL_1044:
						num2 = 268;
						if (Information.Err().Number != 521)
						{
							break;
						}
						goto IL_1061;
						IL_1061:
						num2 = 269;
						ProjectData.ClearProjectError();
						if (num == 0)
						{
							throw ProjectData.CreateProjectError(-2146828268);
						}
						num4 = num;
						goto IL_10ec;
						IL_0ffe:
						num2 = 261;
						num5 = General_Procedures.Save_SQL_Query(Query_To_Run2, myFile);
						goto IL_100f;
						IL_100f:
						num2 = 262;
						if (num5 == 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_1023;
						IL_10ec:
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
							goto IL_001c;
						case 6:
							goto IL_0025;
						case 7:
							goto IL_002a;
						case 8:
							goto IL_002f;
						case 9:
							goto IL_0034;
						case 10:
							goto IL_003a;
						case 11:
							goto IL_0044;
						case 12:
							goto IL_004e;
						case 13:
							goto IL_0054;
						case 14:
							goto IL_005a;
						case 15:
							goto IL_0064;
						case 16:
							goto IL_006e;
						case 17:
							goto IL_0078;
						case 18:
							goto IL_007e;
						case 19:
							goto IL_0084;
						case 20:
							goto IL_008e;
						case 21:
							goto IL_0094;
						case 22:
							goto IL_009e;
						case 23:
							goto IL_00a8;
						case 24:
							goto IL_00b2;
						case 25:
							goto IL_00bc;
						case 26:
							goto IL_00c6;
						case 27:
							goto IL_00d0;
						case 28:
							goto IL_00da;
						case 29:
							goto IL_00e4;
						case 30:
							goto IL_00ee;
						case 31:
							goto IL_00f8;
						case 32:
							goto IL_010c;
						case 33:
							goto IL_0116;
						case 34:
							goto IL_0120;
						case 35:
							goto IL_012a;
						case 36:
							goto IL_0134;
						case 37:
							goto IL_013e;
						case 38:
							goto IL_0148;
						case 39:
							goto IL_0152;
						case 40:
							goto IL_015c;
						case 41:
							goto IL_0166;
						case 42:
							goto IL_0170;
						case 43:
							goto IL_017a;
						case 44:
							goto IL_0184;
						case 45:
							goto IL_018e;
						case 47:
							goto IL_01a8;
						case 48:
							goto IL_01b2;
						case 49:
							goto IL_01ce;
						case 50:
						case 51:
							goto IL_01da;
						case 52:
							goto IL_01f8;
						case 53:
							goto IL_01fe;
						case 54:
						case 55:
							goto IL_0218;
						case 56:
							goto IL_0228;
						case 57:
							goto IL_023f;
						case 58:
							goto IL_0249;
						case 59:
							goto IL_02ab;
						case 60:
							goto IL_02ea;
						case 61:
							goto IL_0300;
						case 62:
							goto IL_030b;
						case 63:
							goto IL_0319;
						case 66:
							goto IL_0333;
						case 67:
							goto IL_033d;
						case 68:
							goto IL_039f;
						case 69:
							goto IL_03a9;
						case 70:
							goto IL_03c6;
						case 71:
							goto IL_03dc;
						case 72:
							goto IL_03eb;
						case 73:
							goto IL_03fd;
						case 74:
							goto IL_0413;
						case 75:
							goto IL_0420;
						case 77:
							goto IL_0433;
						case 78:
							goto IL_044e;
						case 80:
							goto IL_0468;
						case 81:
							goto IL_0483;
						case 83:
							goto IL_049a;
						case 84:
							goto IL_04b5;
						case 86:
							goto IL_04cc;
						case 87:
							goto IL_04e7;
						case 76:
						case 79:
						case 82:
						case 85:
						case 88:
						case 89:
						case 90:
							goto IL_04fd;
						case 91:
							goto IL_050d;
						case 93:
							goto IL_0540;
						case 94:
							goto IL_0546;
						case 95:
							goto IL_0564;
						case 96:
							goto IL_0578;
						case 97:
							goto IL_0585;
						case 99:
							goto IL_0597;
						case 100:
							goto IL_05b0;
						case 102:
							goto IL_05ca;
						case 103:
							goto IL_05e3;
						case 105:
							goto IL_05fd;
						case 106:
							goto IL_0616;
						case 108:
							goto IL_0630;
						case 109:
							goto IL_0649;
						case 111:
							goto IL_0656;
						case 112:
							goto IL_066f;
						case 113:
							goto IL_0683;
						case 114:
							goto IL_069c;
						case 117:
							goto IL_06ac;
						case 118:
							goto IL_06b6;
						case 98:
						case 101:
						case 104:
						case 107:
						case 110:
						case 115:
						case 116:
						case 119:
						case 120:
							goto IL_06bd;
						case 121:
							goto IL_06c8;
						case 122:
							goto IL_06d5;
						case 123:
							goto IL_06e2;
						case 124:
							goto IL_06f0;
						case 125:
							goto IL_06fd;
						case 126:
							goto IL_070b;
						case 92:
						case 127:
						case 129:
						case 130:
						case 132:
						case 133:
						case 134:
						case 135:
							goto IL_0732;
						case 136:
							goto IL_074c;
						case 137:
							goto IL_076d;
						case 139:
							goto IL_078e;
						case 140:
							goto IL_07af;
						case 142:
							goto IL_07d0;
						case 143:
							goto IL_07f1;
						case 145:
							goto IL_0812;
						case 146:
							goto IL_0833;
						case 148:
							goto IL_0854;
						case 149:
							goto IL_0872;
						case 150:
							goto IL_0883;
						case 152:
							goto IL_0896;
						case 153:
							goto IL_08b4;
						case 154:
							goto IL_08c5;
						case 156:
							goto IL_08d8;
						case 157:
							goto IL_08f6;
						case 158:
							goto IL_0907;
						case 160:
							goto IL_0921;
						case 161:
							goto IL_0942;
						case 162:
							goto IL_0958;
						case 163:
							goto IL_0969;
						case 164:
							goto IL_0980;
						case 165:
							goto IL_09a1;
						case 166:
						case 167:
						case 168:
							goto IL_09b8;
						case 170:
							goto IL_09cd;
						case 138:
						case 141:
						case 144:
						case 147:
						case 151:
						case 155:
						case 159:
						case 169:
						case 171:
						case 172:
							goto IL_09df;
						case 174:
							goto IL_09fa;
						case 175:
							goto IL_0a0b;
						case 176:
							goto IL_0a2a;
						case 177:
							goto IL_0a46;
						case 178:
							goto IL_0a5d;
						case 180:
							goto IL_0a77;
						case 181:
							goto IL_0a98;
						case 183:
							goto IL_0aa9;
						case 184:
							goto IL_0ab6;
						case 187:
							goto IL_0aca;
						case 188:
							goto IL_0ae6;
						case 189:
							goto IL_0af3;
						case 191:
							goto IL_0b06;
						case 192:
							goto IL_0b22;
						case 193:
							goto IL_0b2f;
						case 195:
							goto IL_0b42;
						case 196:
							goto IL_0b5e;
						case 197:
							goto IL_0b6b;
						case 199:
							goto IL_0b7e;
						case 200:
							goto IL_0b9a;
						case 201:
							goto IL_0ba7;
						case 203:
							goto IL_0bba;
						case 204:
							goto IL_0bd6;
						case 205:
							goto IL_0be3;
						case 207:
							goto IL_0bf6;
						case 208:
							goto IL_0c12;
						case 209:
							goto IL_0c1f;
						case 211:
							goto IL_0c3c;
						case 212:
							goto IL_0c58;
						case 213:
							goto IL_0c65;
						case 215:
							goto IL_0c78;
						case 216:
							goto IL_0c94;
						case 217:
							goto IL_0ca1;
						case 219:
							goto IL_0cbe;
						case 220:
							goto IL_0cda;
						case 221:
							goto IL_0ce7;
						case 223:
							goto IL_0cfa;
						case 224:
							goto IL_0d16;
						case 225:
							goto IL_0d23;
						case 227:
							goto IL_0d40;
						case 228:
							goto IL_0d53;
						case 229:
							goto IL_0d6c;
						case 230:
							goto IL_0daa;
						case 231:
						case 232:
							goto IL_0db9;
						case 233:
							goto IL_0dc6;
						case 234:
							goto IL_0dd3;
						case 236:
							goto IL_0dea;
						case 237:
							goto IL_0e0d;
						case 238:
							goto IL_0e1a;
						case 239:
							goto IL_0e27;
						case 241:
							goto IL_0e3d;
						case 242:
							goto IL_0e59;
						case 244:
							goto IL_0e69;
						case 245:
							goto IL_0e85;
						case 246:
							goto IL_0e92;
						case 179:
						case 182:
						case 185:
						case 186:
						case 190:
						case 194:
						case 198:
						case 202:
						case 206:
						case 210:
						case 214:
						case 218:
						case 222:
						case 226:
						case 235:
						case 240:
						case 243:
						case 247:
						case 248:
							goto IL_0ea0;
						case 249:
							goto IL_0eb1;
						case 250:
							goto IL_0ec2;
						case 251:
							goto IL_0eee;
						case 252:
							goto IL_0efd;
						case 254:
							goto IL_0f15;
						case 253:
						case 255:
						case 256:
							goto IL_0f2a;
						case 257:
							goto IL_0f45;
						case 258:
							goto IL_0fae;
						case 259:
							goto IL_0fd7;
						case 260:
							goto IL_0ff0;
						case 261:
							goto IL_0ffe;
						case 262:
							goto IL_100f;
						case 65:
						case 264:
						case 265:
							goto IL_1023;
						case 268:
							goto IL_1044;
						case 269:
							goto IL_1061;
						case 271:
							goto end_IL_0001_3;
						default:
							goto end_IL_0001;
						case 46:
						case 64:
						case 128:
						case 131:
						case 173:
						case 263:
						case 266:
						case 267:
						case 270:
						case 272:
						case 273:
						case 274:
							goto end_IL_0001_2;
						}
						goto default;
						IL_000a:
						num2 = 2;
						Query_To_Run2 = "";
						goto IL_0012;
						IL_0012:
						num2 = 3;
						num5 = 0;
						goto IL_0017;
						IL_0017:
						num2 = 4;
						num6 = 0;
						goto IL_001c;
						IL_001c:
						num2 = 5;
						myFile = "";
						goto IL_0025;
						IL_0025:
						num2 = 6;
						num7 = 0;
						goto IL_002a;
						IL_002a:
						num2 = 7;
						num8 = 0;
						goto IL_002f;
						IL_002f:
						num2 = 8;
						num9 = 0;
						goto IL_0034;
						IL_0034:
						num2 = 9;
						flag = false;
						goto IL_003a;
						IL_003a:
						num2 = 10;
						myJob = "";
						goto IL_0044;
						IL_0044:
						num2 = 11;
						text = "";
						goto IL_004e;
						IL_004e:
						num2 = 12;
						num10 = 0;
						goto IL_0054;
						IL_0054:
						num2 = 13;
						num11 = 0;
						goto IL_005a;
						IL_005a:
						num2 = 14;
						SQLCode = "";
						goto IL_0064;
						IL_0064:
						num2 = 15;
						OUser = "";
						goto IL_006e;
						IL_006e:
						num2 = 16;
						OPwd = "";
						goto IL_0078;
						IL_0078:
						num2 = 17;
						num12 = 0;
						goto IL_007e;
						IL_007e:
						num2 = 18;
						num13 = 0;
						goto IL_0084;
						IL_0084:
						num2 = 19;
						text2 = "";
						goto IL_008e;
						IL_008e:
						num2 = 20;
						flag2 = false;
						goto IL_0094;
						IL_0094:
						num2 = 21;
						text3 = "<OPTIONS>";
						goto IL_009e;
						IL_009e:
						num2 = 22;
						text4 = "/NODE=";
						goto IL_00a8;
						IL_00a8:
						num2 = 23;
						text5 = "/UN=";
						goto IL_00b2;
						IL_00b2:
						num2 = 24;
						text6 = "/PW=";
						goto IL_00bc;
						IL_00bc:
						num2 = 25;
						text7 = "/OLEDB=";
						goto IL_00c6;
						IL_00c6:
						num2 = 26;
						text8 = "/ENGINE=VA";
						goto IL_00d0;
						IL_00d0:
						num2 = 27;
						text9 = "/WORKDIR=.\\";
						goto IL_00da;
						IL_00da:
						num2 = 28;
						text10 = "/T=";
						goto IL_00e4;
						IL_00e4:
						num2 = 29;
						text11 = "/CSV=.\\spf_help_data.tab";
						goto IL_00ee;
						IL_00ee:
						num2 = 30;
						text12 = "/HEADERS=";
						goto IL_00f8;
						IL_00f8:
						num2 = 31;
						text13 = "/INSTANCE=" + Globals_Renamed.gSPFCache;
						goto IL_010c;
						IL_010c:
						num2 = 32;
						text14 = "/PROMPT-TEXT=Run Help Query";
						goto IL_0116;
						IL_0116:
						num2 = 33;
						text15 = "/RECORD=";
						goto IL_0120;
						IL_0120:
						num2 = 34;
						text16 = "/HADOOP_SERVER_DEFAULT=";
						goto IL_012a;
						IL_012a:
						num2 = 35;
						text17 = "/GETHELPQUERY=Y";
						goto IL_0134;
						IL_0134:
						num2 = 36;
						text18 = "/PREPROCESS_CSV=";
						goto IL_013e;
						IL_013e:
						num2 = 37;
						text19 = "</OPTIONS>";
						goto IL_0148;
						IL_0148:
						num2 = 38;
						text20 = "";
						goto IL_0152;
						IL_0152:
						num2 = 39;
						text21 = "/UTILITIES=";
						goto IL_015c;
						IL_015c:
						num2 = 40;
						MyErr = "";
						goto IL_0166;
						IL_0166:
						num2 = 41;
						text22 = "N";
						goto IL_0170;
						IL_0170:
						num2 = 42;
						myNodeKind = "";
						goto IL_017a;
						IL_017a:
						num2 = 43;
						left = "";
						goto IL_0184;
						IL_0184:
						num2 = 44;
						left2 = "";
						goto IL_018e;
						IL_018e:
						num2 = 45;
						if (!BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1))
						{
							goto end_IL_0001_2;
						}
						goto IL_01a8;
						IL_01a8:
						num2 = 47;
						MyErr = "";
						goto IL_01b2;
						IL_01b2:
						num2 = 48;
						if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0)
						{
							goto IL_01ce;
						}
						goto IL_01da;
						IL_01ce:
						num2 = 49;
						MyErr = " /SPFLOGLEVEL=DEBUG";
						goto IL_01da;
						IL_01da:
						num2 = 51;
						if (Strings.InStr(1, Strings.UCase(Query_To_Run), "@TEXT@") != 0)
						{
							goto IL_01f8;
						}
						goto IL_0218;
						IL_01f8:
						num2 = 52;
						ll_ObjectType = 3;
						goto IL_01fe;
						IL_01fe:
						num2 = 53;
						Query_To_Run = Strings.Replace(Query_To_Run, "@TEXT@", "", 1, -1, CompareMethod.Text);
						goto IL_0218;
						IL_0218:
						num2 = 55;
						if (ll_ObjectType == 3)
						{
							goto IL_0228;
						}
						goto IL_0333;
						IL_0228:
						num2 = 56;
						text21 = text21 + "@EXEDIR@\\GetHelpCSV.va \"" + Query_To_Run + "\"";
						goto IL_023f;
						IL_023f:
						num2 = 57;
						text = "_GetHelpCSV_.spfsql";
						goto IL_0249;
						IL_0249:
						num2 = 58;
						myJob = "\"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.py\" /SPFSQL=\"" + text + "\" /SPFINSTANCE=" + Globals_Renamed.gSPFCache + MyErr;
						goto IL_02ab;
						IL_02ab:
						num2 = 59;
						Query_To_Run2 = string.Join("\r\n", text3, text9, text13, "/OUTLOOK=N", text21, text14, text17, text19);
						goto IL_02ea;
						IL_02ea:
						num2 = 60;
						myFile = Strings.Trim(Globals_Renamed.MyPCDir + text);
						goto IL_0300;
						IL_0300:
						num2 = 61;
						Query_To_Run2 = BuildForm.Replace_Globals(Query_To_Run2, 1);
						goto IL_030b;
						IL_030b:
						num2 = 62;
						num5 = General_Procedures.Save_SQL_Query(Query_To_Run2, myFile);
						goto IL_0319;
						IL_0319:
						num2 = 63;
						if (num5 == 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_1023;
						IL_0333:
						num2 = 66;
						text = "_GetHelpQuery_.spfsql";
						goto IL_033d;
						IL_033d:
						num2 = 67;
						myJob = "\"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.py\" /SPFSQL=\"" + text + "\" /SPFINSTANCE=" + Globals_Renamed.gSPFCache + MyErr;
						goto IL_039f;
						IL_039f:
						num2 = 68;
						text7 = "/OLEDB=SQLPLUS";
						goto IL_03a9;
						IL_03a9:
						num2 = 69;
						if (LikeOperator.LikeString(Strings.UCase(Query_To_Run), "*@NODE@*@*", CompareMethod.Binary))
						{
							goto IL_03c6;
						}
						goto IL_0540;
						IL_03c6:
						num2 = 70;
						num10 = Strings.InStr(Strings.UCase(Query_To_Run), "@NODE@");
						goto IL_03dc;
						IL_03dc:
						num2 = 71;
						SQLCode = Strings.Mid(Query_To_Run, num10 + 6);
						goto IL_03eb;
						IL_03eb:
						num2 = 72;
						num11 = Strings.InStr(SQLCode, "@");
						goto IL_03fd;
						IL_03fd:
						num2 = 73;
						SQLCode = Strings.Trim(Strings.Mid(SQLCode, 1, num11 - 1));
						goto IL_0413;
						IL_0413:
						num2 = 74;
						if (MyOpt == 0)
						{
							goto IL_0420;
						}
						goto IL_0433;
						IL_0420:
						num2 = 75;
						SQLCode = BuildForm.SubStitute_Nodes_SQL(SQLCode);
						goto IL_04fd;
						IL_0433:
						num2 = 77;
						if (LikeOperator.LikeString(Strings.UCase(SQLCode), "*DEFAULT (MARS)*", CompareMethod.Binary))
						{
							goto IL_044e;
						}
						goto IL_0468;
						IL_044e:
						num2 = 78;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
						goto IL_04fd;
						IL_0468:
						num2 = 80;
						if (LikeOperator.LikeString(Strings.UCase(SQLCode), "*DEFAULT (ARIES)*", CompareMethod.Binary))
						{
							goto IL_0483;
						}
						goto IL_049a;
						IL_0483:
						num2 = 81;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
						goto IL_04fd;
						IL_049a:
						num2 = 83;
						if (LikeOperator.LikeString(Strings.UCase(SQLCode), "*DEFAULT (OASYS)*", CompareMethod.Binary))
						{
							goto IL_04b5;
						}
						goto IL_04cc;
						IL_04b5:
						num2 = 84;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
						goto IL_04fd;
						IL_04cc:
						num2 = 86;
						if (LikeOperator.LikeString(Strings.UCase(SQLCode), "*DEFAULT (OTHER)*", CompareMethod.Binary))
						{
							goto IL_04e7;
						}
						goto IL_04fd;
						IL_04e7:
						num2 = 87;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
						goto IL_04fd;
						IL_04fd:
						num2 = 90;
						General_Procedures.GetUNPWList(1, SQLCode, ref OUser, ref OPwd);
						goto IL_050d;
						IL_050d:
						num2 = 91;
						Query_To_Run = Strings.Mid(Query_To_Run, 1, num10 - 1) + Strings.Mid(Query_To_Run + " ", num10 + 6 + num11);
						goto IL_0732;
						IL_0540:
						num2 = 93;
						flag2 = true;
						goto IL_0546;
						IL_0546:
						num2 = 94;
						if ((MyOpt == 0) | (Strings.InStr(1, Query_To_Run, "@OTHERNODE@") != 0))
						{
							goto IL_0564;
						}
						goto IL_0597;
						IL_0564:
						num2 = 95;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
						goto IL_0578;
						IL_0578:
						num2 = 96;
						if (MyOpt == 0)
						{
							goto IL_0585;
						}
						goto IL_06bd;
						IL_0585:
						num2 = 97;
						SQLCode = BuildForm.SubStitute_Nodes_SQL(SQLCode);
						goto IL_06bd;
						IL_0597:
						num2 = 99;
						if (Strings.InStr(1, Query_To_Run, "@MARSNODE@") != 0)
						{
							goto IL_05b0;
						}
						goto IL_05ca;
						IL_05b0:
						num2 = 100;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
						goto IL_06bd;
						IL_05ca:
						num2 = 102;
						if (Strings.InStr(1, Query_To_Run, "@OASYSNODE@") != 0)
						{
							goto IL_05e3;
						}
						goto IL_05fd;
						IL_05e3:
						num2 = 103;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
						goto IL_06bd;
						IL_05fd:
						num2 = 105;
						if (Strings.InStr(1, Query_To_Run, "@ARIESNODE@") != 0)
						{
							goto IL_0616;
						}
						goto IL_0630;
						IL_0616:
						num2 = 106;
						SQLCode = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
						goto IL_06bd;
						IL_0630:
						num2 = 108;
						if (Strings.InStr(1, Query_To_Run, "@ARIESCLASSNODE@") != 0)
						{
							goto IL_0649;
						}
						goto IL_0656;
						IL_0649:
						num2 = 109;
						SQLCode = Globals_Renamed.MyARIESClassServer1;
						goto IL_06bd;
						IL_0656:
						num2 = 111;
						if (Strings.InStr(1, Query_To_Run, "@MIDASNODE@") != 0)
						{
							goto IL_066f;
						}
						goto IL_06ac;
						IL_066f:
						num2 = 112;
						SQLCode = General_Procedures.GetNodeToUse("MIDAS", Globals_Renamed.MyOtherServer1);
						goto IL_0683;
						IL_0683:
						num2 = 113;
						if (!LikeOperator.LikeString(SQLCode, "*MIDAS*", CompareMethod.Binary))
						{
							goto IL_069c;
						}
						goto IL_06bd;
						IL_069c:
						num2 = 114;
						SQLCode = "All.MIDAS";
						goto IL_06bd;
						IL_06ac:
						num2 = 117;
						SQLCode = "";
						goto IL_06b6;
						IL_06b6:
						num2 = 118;
						flag2 = false;
						goto IL_06bd;
						IL_06bd:
						num2 = 120;
						if (flag2)
						{
							goto IL_06c8;
						}
						goto IL_0732;
						IL_06c8:
						num2 = 121;
						SQLCode = BuildForm.Replace_Globals(SQLCode);
						goto IL_06d5;
						IL_06d5:
						num2 = 122;
						num5 = BuildForm.Chk_SPF_Site(ref SQLCode, 0);
						goto IL_06e2;
						IL_06e2:
						num2 = 123;
						if (num5 != 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_06f0;
						IL_06f0:
						num2 = 124;
						num6 = BuildForm.Chk_SPF_Site(ref SQLCode, 1);
						goto IL_06fd;
						IL_06fd:
						num2 = 125;
						if (num6 != 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_070b;
						IL_070b:
						num2 = 126;
						General_Procedures.GetUNPWList(1, SQLCode, ref OUser, ref OPwd);
						goto IL_0732;
						IL_0732:
						num2 = 135;
						text2 = General_Procedures.NodeCheck_s(SQLCode, "S", 1, "Y");
						goto IL_074c;
						IL_074c:
						num2 = 136;
						if (Strings.InStr(Strings.UCase(text2), "@HADOOPIMPALAODBC@") != 0)
						{
							goto IL_076d;
						}
						goto IL_078e;
						IL_076d:
						num2 = 137;
						text4 += StripODBC(text2, "H");
						goto IL_09df;
						IL_078e:
						num2 = 139;
						if (Strings.InStr(Strings.UCase(text2), "@SAPHANAODBC@") != 0)
						{
							goto IL_07af;
						}
						goto IL_07d0;
						IL_07af:
						num2 = 140;
						text4 += StripODBC(text2, "S");
						goto IL_09df;
						IL_07d0:
						num2 = 142;
						if (Strings.InStr(Strings.UCase(text2), "@DENODOODBC@") != 0)
						{
							goto IL_07f1;
						}
						goto IL_0812;
						IL_07f1:
						num2 = 143;
						text4 += StripODBC(text2, "D");
						goto IL_09df;
						IL_0812:
						num2 = 145;
						if (Strings.InStr(Strings.UCase(text2), "@SNOWFLAKEODBC@") != 0)
						{
							goto IL_0833;
						}
						goto IL_0854;
						IL_0833:
						num2 = 146;
						text4 += StripODBC(text2, "N");
						goto IL_09df;
						IL_0854:
						num2 = 148;
						if (LikeOperator.LikeString(Strings.UCase(text2), "DUCKDB:*", CompareMethod.Binary))
						{
							goto IL_0872;
						}
						goto IL_0896;
						IL_0872:
						num2 = 149;
						text4 += SQLCode;
						goto IL_0883;
						IL_0883:
						num2 = 150;
						myNodeKind = "DUCKDB";
						goto IL_09df;
						IL_0896:
						num2 = 152;
						if (LikeOperator.LikeString(Strings.UCase(text2), "*@UBER@POSTGRES*", CompareMethod.Binary))
						{
							goto IL_08b4;
						}
						goto IL_08d8;
						IL_08b4:
						num2 = 153;
						text4 += SQLCode;
						goto IL_08c5;
						IL_08c5:
						num2 = 154;
						myNodeKind = "POSTGRES-UBER";
						goto IL_09df;
						IL_08d8:
						num2 = 156;
						if (LikeOperator.LikeString(Strings.UCase(text2), "DB:*", CompareMethod.Binary))
						{
							goto IL_08f6;
						}
						goto IL_0921;
						IL_08f6:
						num2 = 157;
						text4 += SQLCode;
						goto IL_0907;
						IL_0907:
						num2 = 158;
						myNodeKind = General_Procedures.GetNodeKind(SQLCode);
						goto IL_09df;
						IL_0921:
						num2 = 160;
						if (LikeOperator.LikeString(Strings.UCase(text2), "*.SDB", CompareMethod.Binary))
						{
							goto IL_0942;
						}
						goto IL_09cd;
						IL_0942:
						num2 = 161;
						num10 = Strings.InStrRev(text2, "\\");
						goto IL_0958;
						IL_0958:
						num2 = 162;
						if (num10 != 0)
						{
							goto IL_0969;
						}
						goto IL_09b8;
						IL_0969:
						num2 = 163;
						left2 = Strings.UCase(Strings.Mid(text2, 1, num10));
						goto IL_0980;
						IL_0980:
						num2 = 164;
						if (Operators.CompareString(left2, Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) == 0)
						{
							goto IL_09a1;
						}
						goto IL_09b8;
						IL_09a1:
						num2 = 165;
						text2 = Strings.Mid(text2, num10 + 1);
						goto IL_09b8;
						IL_09b8:
						num2 = 168;
						text4 += text2;
						goto IL_09df;
						IL_09cd:
						num2 = 170;
						text4 += text2;
						goto IL_09df;
						IL_09df:
						num2 = 172;
						if (Check_UN_Missing(SQLCode, OUser, 1))
						{
							goto end_IL_0001_2;
						}
						goto IL_09fa;
						IL_09fa:
						num2 = 174;
						left = General_Procedures.TestNodeKind(text2, myNodeKind);
						goto IL_0a0b;
						IL_0a0b:
						num2 = 175;
						if (Operators.CompareString(left, "TERADATA-MIDAS", TextCompare: false) == 0)
						{
							goto IL_0a2a;
						}
						goto IL_0aca;
						IL_0a2a:
						num2 = 176;
						if (LikeOperator.LikeString(Globals_Renamed.gMidasDriver, "UBER*", CompareMethod.Binary))
						{
							goto IL_0a46;
						}
						goto IL_0a77;
						IL_0a46:
						num2 = 177;
						text7 = "/OLEDB=" + Globals_Renamed.gMidasDriver;
						goto IL_0a5d;
						IL_0a5d:
						num2 = 178;
						text8 = "/ENGINE=" + Get_Uber_Type();
						goto IL_0ea0;
						IL_0a77:
						num2 = 180;
						if (LikeOperator.LikeString(Strings.UCase(Globals_Renamed.gMidasDriver), ".NET*", CompareMethod.Binary))
						{
							goto IL_0a98;
						}
						goto IL_0aa9;
						IL_0a98:
						num2 = 181;
						text7 = "/OLEDB=VA (.NET)";
						goto IL_0ea0;
						IL_0aa9:
						num2 = 183;
						text7 = "/OLEDB=CB";
						goto IL_0ab6;
						IL_0ab6:
						num2 = 184;
						text8 = "/ENGINE=CB";
						goto IL_0ea0;
						IL_0aca:
						num2 = 187;
						if (Operators.CompareString(left, "HADOOP-IMPALA", TextCompare: false) == 0)
						{
							goto IL_0ae6;
						}
						goto IL_0b06;
						IL_0ae6:
						num2 = 188;
						text7 = "/OLEDB=HADOOPIMPALAODBC";
						goto IL_0af3;
						IL_0af3:
						num2 = 189;
						text8 = "/ENGINE=VA";
						goto IL_0ea0;
						IL_0b06:
						num2 = 191;
						if (Operators.CompareString(left, "DUCKDB", TextCompare: false) == 0)
						{
							goto IL_0b22;
						}
						goto IL_0b42;
						IL_0b22:
						num2 = 192;
						text7 = "/OLEDB=DUCKDB";
						goto IL_0b2f;
						IL_0b2f:
						num2 = 193;
						text8 = "/ENGINE=DUCKDB";
						goto IL_0ea0;
						IL_0b42:
						num2 = 195;
						if (Operators.CompareString(left, "SAPHANA", TextCompare: false) == 0)
						{
							goto IL_0b5e;
						}
						goto IL_0b7e;
						IL_0b5e:
						num2 = 196;
						text7 = "/OLEDB=SAPHANAODBC";
						goto IL_0b6b;
						IL_0b6b:
						num2 = 197;
						text8 = "/ENGINE=VA";
						goto IL_0ea0;
						IL_0b7e:
						num2 = 199;
						if (Operators.CompareString(left, "DENODO", TextCompare: false) == 0)
						{
							goto IL_0b9a;
						}
						goto IL_0bba;
						IL_0b9a:
						num2 = 200;
						text7 = "/OLEDB=DENODOODBC";
						goto IL_0ba7;
						IL_0ba7:
						num2 = 201;
						text8 = "/ENGINE=VA";
						goto IL_0ea0;
						IL_0bba:
						num2 = 203;
						if (Operators.CompareString(left, "SNOWFLAKE", TextCompare: false) == 0)
						{
							goto IL_0bd6;
						}
						goto IL_0bf6;
						IL_0bd6:
						num2 = 204;
						text7 = "/OLEDB=SNOWFLAKEODBC";
						goto IL_0be3;
						IL_0be3:
						num2 = 205;
						text8 = "/ENGINE=VA";
						goto IL_0ea0;
						IL_0bf6:
						num2 = 207;
						if (Operators.CompareString(left, "POSTGRES-UBER", TextCompare: false) == 0)
						{
							goto IL_0c12;
						}
						goto IL_0c3c;
						IL_0c12:
						num2 = 208;
						text7 = "/OLEDB=Postgres";
						goto IL_0c1f;
						IL_0c1f:
						num2 = 209;
						text8 = "/ENGINE=" + Get_Uber_Type();
						goto IL_0ea0;
						IL_0c3c:
						num2 = 211;
						if (Operators.CompareString(left, "POSTGRES", TextCompare: false) == 0)
						{
							goto IL_0c58;
						}
						goto IL_0c78;
						IL_0c58:
						num2 = 212;
						text7 = "/OLEDB=Postgres";
						goto IL_0c65;
						IL_0c65:
						num2 = 213;
						text8 = "/ENGINE=VA";
						goto IL_0ea0;
						IL_0c78:
						num2 = 215;
						if (Operators.CompareString(left, "UBER", TextCompare: false) == 0)
						{
							goto IL_0c94;
						}
						goto IL_0cbe;
						IL_0c94:
						num2 = 216;
						text7 = "/OLEDB=UBER";
						goto IL_0ca1;
						IL_0ca1:
						num2 = 217;
						text8 = "/ENGINE=" + Get_Uber_Type();
						goto IL_0ea0;
						IL_0cbe:
						num2 = 219;
						if (Operators.CompareString(left, "TERADATA-.NET", TextCompare: false) == 0)
						{
							goto IL_0cda;
						}
						goto IL_0cfa;
						IL_0cda:
						num2 = 220;
						text7 = "/OLEDB=TERADATA";
						goto IL_0ce7;
						IL_0ce7:
						num2 = 221;
						text8 = "/ENGINE=VA (.NET)";
						goto IL_0ea0;
						IL_0cfa:
						num2 = 223;
						if (Operators.CompareString(left, "TERADATA-UBER", TextCompare: false) == 0)
						{
							goto IL_0d16;
						}
						goto IL_0d40;
						IL_0d16:
						num2 = 224;
						text7 = "/OLEDB=TERADATA";
						goto IL_0d23;
						IL_0d23:
						num2 = 225;
						text8 = "/ENGINE=" + Get_Uber_Type();
						goto IL_0ea0;
						IL_0d40:
						num2 = 227;
						if (ll_ObjectType == 1)
						{
							goto IL_0d53;
						}
						goto IL_0dea;
						IL_0d53:
						num2 = 228;
						if (unchecked(Globals_Renamed.Design_Mode != 0 && MyOpt != 0))
						{
							goto IL_0d6c;
						}
						goto IL_0db9;
						IL_0d6c:
						num2 = 229;
						if (Strings.InStr(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "PREPROCCSV:SQL"), "{Yes}") != 0)
						{
							goto IL_0daa;
						}
						goto IL_0db9;
						IL_0daa:
						num2 = 230;
						text22 = " \"Y\"";
						goto IL_0db9;
						IL_0db9:
						num2 = 232;
						text7 = "/OLEDB=SQLite";
						goto IL_0dc6;
						IL_0dc6:
						num2 = 233;
						text8 = "/ENGINE=SQLite";
						goto IL_0dd3;
						IL_0dd3:
						num2 = 234;
						text18 += text22;
						goto IL_0ea0;
						IL_0dea:
						num2 = 236;
						if (ll_ObjectType == 2 && Operators.CompareString(left, "SQLITE", TextCompare: false) == 0)
						{
							goto IL_0e0d;
						}
						goto IL_0e3d;
						IL_1023:
						num2 = 265;
						num5 = RunQ(myJob, 0, "M");
						goto end_IL_0001_2;
						IL_0e0d:
						num2 = 237;
						text7 = "/OLEDB=SQLite";
						goto IL_0e1a;
						IL_0e1a:
						num2 = 238;
						text8 = "/ENGINE=SQLite";
						goto IL_0e27;
						IL_0e27:
						num2 = 239;
						text18 = text18 + text18 + text22;
						goto IL_0ea0;
						IL_0e3d:
						num2 = 241;
						if (Operators.CompareString(left, "MONGO", TextCompare: false) == 0)
						{
							goto IL_0e59;
						}
						goto IL_0e69;
						IL_0e59:
						num2 = 242;
						text7 = "/OLEDB=MONGO";
						goto IL_0ea0;
						IL_0e69:
						num2 = 244;
						if (Operators.CompareString(left, "SQLSERVER", TextCompare: false) == 0)
						{
							goto IL_0e85;
						}
						goto IL_0ea0;
						IL_0e85:
						num2 = 245;
						text7 = "/OLEDB=SQLSERVER";
						goto IL_0e92;
						IL_0e92:
						num2 = 246;
						text8 = "/ENGINE=VA (.NET)";
						goto IL_0ea0;
						IL_0ea0:
						num2 = 248;
						text5 += OUser;
						goto IL_0eb1;
						IL_0eb1:
						num2 = 249;
						text6 += OPwd;
						goto IL_0ec2;
						IL_0ec2:
						num2 = 250;
						if (Operators.CompareString(Strings.Mid(Query_To_Run + "       ", 1, 7), "NOSHOW-", TextCompare: false) == 0)
						{
							goto IL_0eee;
						}
						goto IL_0f15;
						IL_0eee:
						num2 = 251;
						Query_To_Run = Strings.Mid(Query_To_Run, 8);
						goto IL_0efd;
						IL_0efd:
						num2 = 252;
						text10 += "N";
						goto IL_0f2a;
						IL_0f15:
						num2 = 254;
						text10 += "G";
						goto IL_0f2a;
						IL_0f2a:
						num2 = 256;
						text20 = Strings.Replace(Query_To_Run, "\r\n", " ", 1, -1, CompareMethod.Text);
						goto IL_0f45;
						IL_0f45:
						num2 = 257;
						Query_To_Run2 = string.Join("\r\n", text3, text4, text5, text6, text7, text8, text9, text10, text11, text13, text14, text17, text18, text19, text20);
						goto IL_0fae;
						end_IL_0001_3:
						break;
					}
					num2 = 271;
					Interaction.MsgBox("Error running Help Query: (" + Conversions.ToString(Information.Err().Number) + "-" + Conversion.ErrorToString() + "). Try again." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Help Query Error");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5441;
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

	public static void Run_The_Query(string Query_To_Run, short Query_Code, string ll_MacroFile)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num4 = default(short);
		string text = default(string);
		string myJob = default(string);
		string text2 = default(string);
		short num6 = default(short);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string MyErr = default(string);
		string path = default(string);
		string[] files = default(string[]);
		int num7 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text3;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1842:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_05ae;
						default:
							goto end_IL_0001;
						}
						break;
					}
					IL_04e1:
					num2 = 78;
					if (num4 != 1)
					{
						goto end_IL_0001_2;
					}
					goto IL_04ef;
					IL_04ef:
					num2 = 79;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_2;
					}
					goto IL_0507;
					IL_04cf:
					num2 = 77;
					num4 = RunQ(myJob, 0);
					goto IL_04e1;
					IL_05ae:
					while (true)
					{
						int num5 = num + 1;
						num = 0;
						switch (num5)
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
							goto IL_002a;
						case 7:
							goto IL_0033;
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
						case 16:
							goto IL_00a2;
						case 17:
							goto IL_00b0;
						case 18:
							goto IL_00df;
						case 19:
							goto IL_00ea;
						case 20:
							goto IL_0101;
						case 21:
						case 22:
							goto IL_010c;
						case 23:
							goto IL_0128;
						case 24:
						case 25:
							goto IL_0134;
						case 26:
							goto IL_0150;
						case 27:
						case 28:
							goto IL_015c;
						case 29:
							goto IL_0178;
						case 30:
						case 31:
							goto IL_0184;
						case 32:
							goto IL_01a0;
						case 33:
						case 34:
							goto IL_01b3;
						case 35:
							goto IL_01cf;
						case 36:
						case 37:
							goto IL_01db;
						case 38:
							goto IL_01f1;
						case 39:
							goto IL_020a;
						case 40:
							goto IL_0215;
						case 41:
							goto IL_022d;
						case 42:
						case 43:
							goto IL_0244;
						case 44:
							goto IL_0263;
						case 46:
							goto IL_02e9;
						case 49:
							goto IL_033d;
						case 50:
							goto IL_034a;
						case 51:
							goto IL_0363;
						case 53:
							goto IL_0370;
						case 54:
							goto IL_037e;
						case 55:
							goto IL_0397;
						case 57:
							goto IL_03a5;
						case 58:
							goto IL_03be;
						case 45:
						case 47:
						case 48:
						case 52:
						case 56:
						case 59:
						case 60:
							goto IL_03c9;
						case 61:
							goto IL_03dd;
						case 62:
							goto IL_0401;
						case 63:
							goto IL_0418;
						case 64:
							goto IL_0426;
						case 66:
							goto IL_0442;
						case 65:
						case 67:
						case 68:
							goto IL_0451;
						case 69:
							goto IL_0469;
						case 70:
						case 71:
						case 72:
							goto IL_0484;
						case 73:
							goto IL_0492;
						case 75:
							goto IL_04a5;
						case 76:
							goto IL_04c7;
						case 77:
							goto IL_04cf;
						case 78:
							goto IL_04e1;
						case 79:
							goto IL_04ef;
						case 80:
							goto IL_0507;
						case 84:
							num2 = 84;
							if (Information.Err().Number == 53)
							{
								goto case 85;
							}
							goto case 87;
						case 85:
							num2 = 85;
							ProjectData.ClearProjectError();
							if (num == 0)
							{
								throw ProjectData.CreateProjectError(-2146828268);
							}
							continue;
						case 87:
							num2 = 87;
							Interaction.MsgBox("Error accessing CSV (Excel/JMP) file name " + text + ". Either the File path is invalid or another process is locking the file. Do correct before re-running. " + Conversion.ErrorToString(), MsgBoxStyle.Critical, "Error Accessing CSV (Excel or JMP) File");
							goto end_IL_0001_2;
						case 91:
							goto end_IL_0001_3;
						default:
							goto end_IL_0001;
						case 15:
						case 74:
						case 81:
						case 82:
						case 83:
						case 86:
						case 88:
						case 89:
						case 90:
						case 92:
						case 93:
							goto end_IL_0001_2;
						}
						break;
					}
					goto default;
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
					num4 = 0;
					goto IL_0021;
					IL_0021:
					num2 = 5;
					text3 = "";
					goto IL_002a;
					IL_002a:
					num2 = 6;
					myJob = "";
					goto IL_0033;
					IL_0033:
					num2 = 7;
					num6 = 0;
					goto IL_0038;
					IL_0038:
					num2 = 8;
					text4 = "";
					goto IL_0041;
					IL_0041:
					num2 = 9;
					text5 = "";
					goto IL_004b;
					IL_004b:
					num2 = 10;
					text6 = "";
					goto IL_0055;
					IL_0055:
					num2 = 11;
					text7 = "";
					goto IL_005f;
					IL_005f:
					num2 = 12;
					MyErr = "";
					goto IL_0069;
					IL_0069:
					num2 = 13;
					path = "";
					goto IL_0073;
					IL_0073:
					num2 = 14;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0 && !BuildForm.Can_Use_Py_Engine(ref MyErr, QuietMode: false, 1))
					{
						goto end_IL_0001_2;
					}
					goto IL_00a2;
					IL_0507:
					num2 = 80;
					Globals_Renamed.ExcelPending = text;
					goto end_IL_0001_2;
					IL_00a2:
					num2 = 16;
					if (Globals_Renamed.gPurgeColPatternFiles)
					{
						goto IL_00b0;
					}
					goto IL_010c;
					IL_00b0:
					num2 = 17;
					files = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Globals_Renamed.gSPFCache + ".cols");
					num7 = 0;
					goto IL_00f3;
					IL_00f3:
					if (num7 < files.Length)
					{
						path = files[num7];
						goto IL_00df;
					}
					goto IL_0101;
					IL_0101:
					num2 = 20;
					Globals_Renamed.gPurgeColPatternFiles = false;
					goto IL_010c;
					IL_04c7:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_04cf;
					IL_00df:
					num2 = 18;
					File.Delete(path);
					goto IL_00ea;
					IL_00ea:
					num2 = 19;
					num7 = checked(num7 + 1);
					goto IL_00f3;
					IL_010c:
					num2 = 22;
					if (Operators.CompareString(Globals_Renamed.gPyDebug, "Y", TextCompare: false) == 0)
					{
						goto IL_0128;
					}
					goto IL_0134;
					IL_0128:
					num2 = 23;
					text4 = " /SPFLOGLEVEL=DEBUG";
					goto IL_0134;
					IL_0134:
					num2 = 25;
					if (Operators.CompareString(Globals_Renamed.gWebNext, "Y", TextCompare: false) == 0)
					{
						goto IL_0150;
					}
					goto IL_015c;
					IL_0150:
					num2 = 26;
					text5 = " /iREPORTS_VERSION=NEXT";
					goto IL_015c;
					IL_015c:
					num2 = 28;
					if (Operators.CompareString(Globals_Renamed.gEncodeFFS, "Y", TextCompare: false) == 0)
					{
						goto IL_0178;
					}
					goto IL_0184;
					IL_0178:
					num2 = 29;
					text6 = " /ENCODING_FULLFILE_SCAN=Y";
					goto IL_0184;
					IL_0184:
					num2 = 31;
					if (Operators.CompareString(Globals_Renamed.gEncodeUTFBOM, "Y", TextCompare: false) == 0)
					{
						goto IL_01a0;
					}
					goto IL_01b3;
					IL_01a0:
					num2 = 32;
					text6 += " /ENCODING_UTFBOM=Y";
					goto IL_01b3;
					IL_01b3:
					num2 = 34;
					if (Operators.CompareString(Globals_Renamed.gConvertMAOUber, "Y", TextCompare: false) == 0)
					{
						goto IL_01cf;
					}
					goto IL_01db;
					IL_01cf:
					num2 = 35;
					text7 = " /USE_UBER_MAO=Y";
					goto IL_01db;
					IL_01db:
					num2 = 37;
					if (Query_Code == 6 || Query_Code == 9)
					{
						goto IL_01f1;
					}
					goto IL_033d;
					IL_01f1:
					num2 = 38;
					text2 = Strings.Trim(Globals_Renamed.MyPCDir) + "_sqlpathfinder_.spfsql";
					goto IL_020a;
					IL_020a:
					num2 = 39;
					ll_MacroFile = Strings.Trim(ll_MacroFile);
					goto IL_0215;
					IL_0215:
					num2 = 40;
					if (Operators.CompareString(ll_MacroFile, "", TextCompare: false) != 0)
					{
						goto IL_022d;
					}
					goto IL_0244;
					IL_022d:
					num2 = 41;
					ll_MacroFile = " /IMMEDIATE_MACROFILE=\"" + ll_MacroFile + "\"";
					goto IL_0244;
					IL_0244:
					num2 = 43;
					if (Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_0263;
					}
					goto IL_02e9;
					IL_0263:
					num2 = 44;
					myJob = "\"" + Globals_Renamed.gMyPyPath + "\" -s -u \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.py\" /SPFSQL=\"" + text2 + "\" /SPFGUI=Y" + ll_MacroFile + " /SPFINSTANCE=" + Globals_Renamed.gSPFCache + text5 + text4 + text6 + text7;
					goto IL_03c9;
					IL_02e9:
					num2 = 46;
					myJob = "va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.va\" /SPFSQL=\"" + text2 + "\"" + ll_MacroFile + text5;
					goto IL_03c9;
					IL_033d:
					num2 = 49;
					if (Query_Code == 1)
					{
						goto IL_034a;
					}
					goto IL_0370;
					IL_034a:
					num2 = 50;
					text2 = Strings.Trim(Globals_Renamed.MyPCDir + "SQLPathfinder_xdb.va");
					goto IL_0363;
					IL_0363:
					num2 = 51;
					myJob = "va SQLPathfinder_xdb.va";
					goto IL_03c9;
					IL_0370:
					num2 = 53;
					if (Query_Code == 10)
					{
						goto IL_037e;
					}
					goto IL_03a5;
					IL_037e:
					num2 = 54;
					text2 = Strings.Trim(Globals_Renamed.MyPCDir + "SQLPathfinder_ilv.va");
					goto IL_0397;
					IL_0397:
					num2 = 55;
					myJob = "va SQLPathfinder_ilv.va";
					goto IL_03c9;
					IL_03a5:
					num2 = 57;
					text2 = Strings.Trim(Globals_Renamed.MyPCDir + "SQLPathfinder.va");
					goto IL_03be;
					IL_03be:
					num2 = 58;
					myJob = "va SQLPathfinder.va";
					goto IL_03c9;
					IL_03c9:
					num2 = 60;
					if (Query_Code != 9)
					{
						goto IL_03dd;
					}
					goto IL_0484;
					IL_03dd:
					num2 = 61;
					if (Operators.CompareString(Strings.Trim(Globals_Renamed.ODBC_Prompt), "", TextCompare: false) != 0)
					{
						goto IL_0401;
					}
					goto IL_0484;
					IL_0401:
					num2 = 62;
					num6 = checked((short)Strings.InStrRev(Globals_Renamed.ODBC_Prompt, "\\", -1, CompareMethod.Text));
					goto IL_0418;
					IL_0418:
					num2 = 63;
					if (num6 == 0)
					{
						goto IL_0426;
					}
					goto IL_0442;
					IL_0426:
					num2 = 64;
					text = Strings.Trim(Globals_Renamed.MyPCDir + Globals_Renamed.ODBC_Prompt);
					goto IL_0451;
					IL_0442:
					num2 = 66;
					text = Strings.Trim(Globals_Renamed.ODBC_Prompt);
					goto IL_0451;
					IL_0451:
					num2 = 68;
					if (Strings.InStr(text, "{TS}") != 0)
					{
						goto IL_0469;
					}
					goto IL_0484;
					IL_0469:
					num2 = 69;
					text = Strings.Replace(text, "{TS}", Globals_Renamed.g_TS, 1, -1, CompareMethod.Text);
					goto IL_0484;
					IL_0484:
					num2 = 72;
					num4 = General_Procedures.Save_SQL_Query(Query_To_Run, text2);
					goto IL_0492;
					IL_0492:
					num2 = 73;
					if (num4 == 0)
					{
						goto end_IL_0001_2;
					}
					goto IL_04a5;
					IL_04a5:
					num2 = 75;
					if (Query_Code != 1 && Query_Code != 6 && Query_Code != 7 && Query_Code != 9 && Query_Code != 10)
					{
						goto end_IL_0001_2;
					}
					goto IL_04c7;
					end_IL_0001_3:
					break;
				}
				num2 = 91;
				Interaction.MsgBox("Error running query: (" + Conversion.ErrorToString() + "). Try again." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Query Execution Error");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1842;
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void File_To_InList()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string[] DynArray = default(string[]);
		int num = default(int);
		int num3 = default(int);
		string CBBegin = default(string);
		string text = default(string);
		string text2 = default(string);
		int num4 = default(int);
		string text3 = default(string);
		string CBEnd = default(string);
		string text4 = default(string);
		short num6 = default(short);
		int num7 = default(int);
		string theString = default(string);
		string text5 = default(string);
		string left = default(string);
		short num8 = default(short);
		short num9 = default(short);
		string text10 = default(string);
		frmfilename frmfilename2 = default(frmfilename);
		int num12 = default(int);
		int num13 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					string text6;
					string text7;
					string text8;
					string text9;
					short num10;
					short num11;
					switch (try0001_dispatch)
					{
					default:
						num2 = 1;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						goto IL_0016;
					case 3076:
						{
							num = num2;
							switch ((num3 <= -2) ? 1 : num3)
							{
							case 2:
								break;
							case 1:
								goto IL_09e8;
							default:
								goto end_IL_0001;
							}
							goto IL_0973;
						}
						IL_087a:
						num2 = 117;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + "OR " + text + " LIKE " + text2 + Strings.RTrim(DynArray[num4]) + "%" + text3 + CBEnd;
						goto IL_093f;
						IL_08e1:
						num2 = 119;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + "OR " + text + " LIKE " + text2 + Strings.RTrim(DynArray[num4]) + text3 + CBEnd;
						goto IL_093f;
						IL_084c:
						num2 = 116;
						if ((Strings.InStr(DynArray[num4], "%") == 0) & (Strings.InStr(DynArray[num4], "_") == 0))
						{
							goto IL_087a;
						}
						goto IL_08e1;
						IL_09e8:
						num5 = unchecked(num + 1);
						num = 0;
						switch (num5)
						{
						case 1:
							break;
						case 2:
							goto IL_0016;
						case 3:
							goto IL_001f;
						case 4:
							goto IL_0024;
						case 5:
							goto IL_0029;
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
							goto IL_0081;
						case 16:
							goto IL_008b;
						case 17:
							goto IL_0091;
						case 18:
							goto IL_009b;
						case 19:
							goto IL_00a5;
						case 20:
							goto IL_00af;
						case 21:
							goto IL_00b9;
						case 22:
							goto IL_00bf;
						case 23:
							goto IL_00c5;
						case 24:
							goto IL_00cf;
						case 25:
							goto IL_00d8;
						case 26:
							goto IL_00f1;
						case 28:
							goto IL_00fe;
						case 29:
							goto IL_0117;
						case 27:
						case 30:
						case 31:
							goto IL_0122;
						case 32:
							goto IL_0141;
						case 33:
							goto IL_014e;
						case 34:
							goto IL_0158;
						case 35:
							goto IL_0163;
						case 36:
							goto IL_0198;
						case 39:
							goto IL_01ae;
						case 40:
							goto IL_01c7;
						case 42:
							goto IL_01ea;
						case 43:
							goto IL_0200;
						case 38:
						case 41:
						case 44:
						case 45:
						case 46:
						case 47:
							goto IL_0225;
						case 48:
							goto IL_023e;
						case 49:
							goto IL_0250;
						case 50:
							goto IL_0261;
						case 52:
							goto IL_026f;
						case 53:
							goto IL_0279;
						case 54:
							goto IL_028f;
						case 55:
							goto IL_02a2;
						case 51:
						case 56:
						case 57:
							goto IL_02b7;
						case 58:
							goto IL_02c4;
						case 59:
							goto IL_0310;
						case 60:
							goto IL_031a;
						case 62:
							goto IL_032a;
						case 63:
							goto IL_037d;
						case 64:
							goto IL_0387;
						case 66:
							goto IL_0395;
						case 67:
							goto IL_039f;
						case 61:
						case 65:
						case 68:
						case 69:
							goto IL_03aa;
						case 70:
							goto IL_03b2;
						case 71:
							goto IL_03bd;
						case 72:
							goto IL_03ce;
						case 73:
							goto IL_03e2;
						case 74:
							goto IL_03f6;
						case 75:
							goto IL_0403;
						case 76:
							goto IL_0416;
						case 77:
							goto IL_0432;
						case 78:
							goto IL_0441;
						case 79:
							goto IL_0452;
						case 80:
							goto IL_0475;
						case 81:
							goto IL_048f;
						case 82:
							goto IL_049d;
						case 84:
							goto IL_04ec;
						case 85:
							goto IL_0554;
						case 83:
						case 86:
						case 87:
							goto IL_058c;
						case 90:
							goto IL_059b;
						case 91:
							goto IL_05b9;
						case 92:
							goto IL_05d3;
						case 93:
							goto IL_05e1;
						case 95:
							goto IL_0633;
						case 96:
							goto IL_069b;
						case 88:
						case 89:
						case 94:
						case 97:
						case 98:
						case 99:
						case 100:
							goto IL_06d9;
						case 101:
							goto IL_06eb;
						case 103:
							goto IL_070f;
						case 104:
							goto IL_072b;
						case 105:
							goto IL_073a;
						case 106:
							goto IL_074b;
						case 107:
							goto IL_076e;
						case 108:
							goto IL_079c;
						case 110:
							goto IL_07de;
						case 109:
						case 111:
						case 112:
							goto IL_081f;
						case 115:
							goto IL_082e;
						case 116:
							goto IL_084c;
						case 117:
							goto IL_087a;
						case 119:
							goto IL_08e1;
						case 113:
						case 114:
						case 118:
						case 120:
						case 121:
						case 122:
						case 123:
							goto IL_093f;
						case 124:
							goto IL_0951;
						case 127:
							goto IL_0973;
						case 128:
							goto IL_09b3;
						case 129:
							goto IL_09ca;
						case 37:
						case 102:
						case 125:
						case 126:
						case 130:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 131:
							goto end_IL_0001_3;
						}
						goto default;
						IL_0973:
						num2 = 127;
						Interaction.MsgBox("Error accessing file " + text4 + ". " + Conversion.ErrorToString() + ".", MsgBoxStyle.Critical, "Error Accessing File");
						goto IL_09b3;
						IL_09b3:
						num2 = 128;
						FileSystem.FileClose(num6);
						goto IL_09ca;
						IL_09ca:
						num2 = 129;
						Globals_Renamed.currinputstrtmp = "";
						break;
						IL_0016:
						num2 = 2;
						DynArray.Initialize();
						goto IL_001f;
						IL_001f:
						num2 = 3;
						num7 = 0;
						goto IL_0024;
						IL_0024:
						num2 = 4;
						num4 = 0;
						goto IL_0029;
						IL_0029:
						num2 = 5;
						num6 = 0;
						goto IL_002e;
						IL_002e:
						num2 = 6;
						theString = "";
						goto IL_0037;
						IL_0037:
						num2 = 7;
						text2 = "";
						goto IL_0040;
						IL_0040:
						num2 = 8;
						text3 = "";
						goto IL_0049;
						IL_0049:
						num2 = 9;
						text5 = "";
						goto IL_0053;
						IL_0053:
						num2 = 10;
						text4 = "";
						goto IL_005d;
						IL_005d:
						num2 = 11;
						CBBegin = "";
						goto IL_0067;
						IL_0067:
						num2 = 12;
						CBEnd = "";
						goto IL_0071;
						IL_0071:
						num2 = 13;
						left = "";
						goto IL_007b;
						IL_007b:
						num2 = 14;
						num8 = 0;
						goto IL_0081;
						IL_0081:
						num2 = 15;
						text = "";
						goto IL_008b;
						IL_008b:
						num2 = 16;
						num9 = 1;
						goto IL_0091;
						IL_0091:
						num2 = 17;
						text6 = "";
						goto IL_009b;
						IL_009b:
						num2 = 18;
						text7 = "";
						goto IL_00a5;
						IL_00a5:
						num2 = 19;
						text8 = "";
						goto IL_00af;
						IL_00af:
						num2 = 20;
						text9 = "";
						goto IL_00b9;
						IL_00b9:
						num2 = 21;
						num10 = 0;
						goto IL_00bf;
						IL_00bf:
						num2 = 22;
						num11 = 0;
						goto IL_00c5;
						IL_00c5:
						num2 = 23;
						text10 = "";
						goto IL_00cf;
						IL_00cf:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_00d8;
						IL_00d8:
						num2 = 25;
						if (LikeOperator.LikeString(Globals_Renamed.currinputstrtmp, "INFILE*", CompareMethod.Binary))
						{
							goto IL_00f1;
						}
						goto IL_00fe;
						IL_00f1:
						num2 = 26;
						left = "I";
						goto IL_0122;
						IL_00fe:
						num2 = 28;
						if (LikeOperator.LikeString(Globals_Renamed.currinputstrtmp, "ILFILE*", CompareMethod.Binary))
						{
							goto IL_0117;
						}
						goto IL_0122;
						IL_0117:
						num2 = 29;
						left = "L";
						goto IL_0122;
						IL_0122:
						num2 = 31;
						if (Strings.InStr(Globals_Renamed.currinputstrtmp, "&PROMPT&") != 0)
						{
							goto IL_0141;
						}
						goto IL_0225;
						IL_0141:
						num2 = 32;
						Globals_Renamed.currvaluetmp = "&RUN-TIME&";
						goto IL_014e;
						IL_014e:
						num2 = 33;
						frmfilename2 = new frmfilename();
						goto IL_0158;
						IL_0158:
						num2 = 34;
						frmfilename2.ShowDialog();
						goto IL_0163;
						IL_0163:
						num2 = 35;
						if ((Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(Globals_Renamed.currvaluetmp), "", TextCompare: false) == 0))
						{
							goto IL_0198;
						}
						goto IL_01ae;
						IL_0198:
						num2 = 36;
						Globals_Renamed.currinputstrtmp = "";
						break;
						IL_01ae:
						num2 = 39;
						if (Operators.CompareString(left, "I", TextCompare: false) == 0)
						{
							goto IL_01c7;
						}
						goto IL_01ea;
						IL_01c7:
						num2 = 40;
						Globals_Renamed.currinputstrtmp = Strings.Mid(Globals_Renamed.currinputstrtmp, 1, 10) + Globals_Renamed.currvaluetmp;
						goto IL_0225;
						IL_01ea:
						num2 = 42;
						num8 = (short)Strings.InStr(Globals_Renamed.currinputstrtmp, "^[C]");
						goto IL_0200;
						IL_0200:
						num2 = 43;
						Globals_Renamed.currinputstrtmp = Strings.Mid(Globals_Renamed.currinputstrtmp, 1, num8 + 3) + Globals_Renamed.currvaluetmp;
						goto IL_0225;
						IL_0225:
						num2 = 47;
						if (Operators.CompareString(left, "I", TextCompare: false) == 0)
						{
							goto IL_023e;
						}
						goto IL_026f;
						IL_023e:
						num2 = 48;
						text5 = Strings.Mid(Globals_Renamed.currinputstrtmp, 9, 1);
						goto IL_0250;
						IL_0250:
						num2 = 49;
						text4 = Strings.Mid(Globals_Renamed.currinputstrtmp, 11);
						goto IL_0261;
						IL_0261:
						num2 = 50;
						text = "";
						goto IL_02b7;
						IL_026f:
						num2 = 52;
						text5 = "C";
						goto IL_0279;
						IL_0279:
						num2 = 53;
						num8 = (short)Strings.InStr(Globals_Renamed.currinputstrtmp, "^[C]");
						goto IL_028f;
						IL_028f:
						num2 = 54;
						text4 = Strings.Mid(Globals_Renamed.currinputstrtmp, num8 + 4);
						goto IL_02a2;
						IL_02a2:
						num2 = 55;
						text = Strings.Mid(Globals_Renamed.currinputstrtmp, 8, num8 - 8);
						goto IL_02b7;
						IL_02b7:
						num2 = 57;
						Globals_Renamed.currinputstrtmp = "";
						goto IL_02c4;
						IL_02c4:
						num2 = 58;
						if (Conversions.ToBoolean(Strings.UCase(Conversions.ToString(Operators.CompareString(text5, "D", TextCompare: false) == 0))) || Conversions.ToBoolean(Strings.UCase(Conversions.ToString(Operators.CompareString(text5, "V", TextCompare: false) == 0))))
						{
							goto IL_0310;
						}
						goto IL_032a;
						IL_093f:
						num2 = 123;
						num4++;
						goto IL_0948;
						IL_032a:
						num2 = 62;
						if ((Operators.CompareString(Strings.UCase(text5), "C", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(text5), "Q", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(text5), "X", TextCompare: false) == 0) | General_Procedures.IsDateDT(text5, 1))
						{
							goto IL_037d;
						}
						goto IL_0395;
						IL_037d:
						num2 = 63;
						text2 = "'";
						goto IL_0387;
						IL_0387:
						num2 = 64;
						text3 = "'";
						goto IL_03aa;
						IL_0395:
						num2 = 66;
						text2 = "";
						goto IL_039f;
						IL_039f:
						num2 = 67;
						text3 = "";
						goto IL_03aa;
						IL_0310:
						num2 = 59;
						text2 = "To_Date('";
						goto IL_031a;
						IL_031a:
						num2 = 60;
						text3 = "','dd-mon-yyyy hh24:mi:ss')";
						goto IL_03aa;
						IL_03aa:
						ProjectData.ClearProjectError();
						num3 = 2;
						goto IL_03b2;
						IL_03b2:
						num2 = 70;
						num6 = (short)FileSystem.FreeFile();
						goto IL_03bd;
						IL_03bd:
						num2 = 71;
						FileSystem.FileOpen(num6, text4, OpenMode.Input, OpenAccess.Read, OpenShare.Shared);
						goto IL_03ce;
						IL_03ce:
						num2 = 72;
						theString = FileSystem.InputString(num6, (int)FileSystem.LOF(num6));
						goto IL_03e2;
						IL_03e2:
						num2 = 73;
						FileSystem.FileClose(num6);
						goto IL_03f6;
						IL_03f6:
						num2 = 74;
						Set_Engine_Delimiters(ref CBBegin, ref CBEnd);
						goto IL_0403;
						IL_0403:
						num2 = 75;
						num7 = General_Procedures.ParseAndFillArray(theString, Globals_Renamed.CRLF, ref DynArray);
						goto IL_0416;
						IL_0416:
						num2 = 76;
						if (Operators.CompareString(left, "I", TextCompare: false) == 0)
						{
							goto IL_0432;
						}
						goto IL_070f;
						IL_0432:
						num2 = 77;
						num12 = num7;
						num4 = 1;
						goto IL_06e2;
						IL_06e2:
						if (num4 <= num12)
						{
							goto IL_0441;
						}
						goto IL_06eb;
						IL_06eb:
						num2 = 101;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + ")";
						break;
						IL_0441:
						num2 = 78;
						if (num9 == 1)
						{
							goto IL_0452;
						}
						goto IL_059b;
						IL_0452:
						num2 = 79;
						if (Operators.CompareString(Strings.Trim(DynArray[num4]), "", TextCompare: false) != 0)
						{
							goto IL_0475;
						}
						goto IL_06d9;
						IL_0475:
						num2 = 80;
						num8 = (short)Strings.InStr(Strings.RTrim(DynArray[num4]), ",");
						goto IL_048f;
						IL_048f:
						num2 = 81;
						if (num8 == 0)
						{
							goto IL_049d;
						}
						goto IL_04ec;
						IL_049d:
						num2 = 82;
						Globals_Renamed.currinputstrtmp = CBEnd + Globals_Renamed.CRLF + CBBegin + "(" + text2 + Strings.RTrim(DynArray[num4]) + text3 + CBEnd;
						goto IL_058c;
						IL_04ec:
						num2 = 84;
						text10 = "(" + text2 + Strings.RTrim(Strings.Mid(DynArray[num4], 1, num8 - 1)) + text3 + "," + text2 + Strings.Mid(Strings.RTrim(DynArray[num4]), num8 + 1) + text3 + ")";
						goto IL_0554;
						IL_0554:
						num2 = 85;
						Globals_Renamed.currinputstrtmp = CBEnd + Globals_Renamed.CRLF + CBBegin + "(" + text10 + CBEnd;
						goto IL_058c;
						IL_058c:
						num2 = 87;
						num9 = 0;
						goto IL_06d9;
						IL_059b:
						num2 = 90;
						if (Operators.CompareString(DynArray[num4], "", TextCompare: false) != 0)
						{
							goto IL_05b9;
						}
						goto IL_06d9;
						IL_05b9:
						num2 = 91;
						num8 = (short)Strings.InStr(Strings.RTrim(DynArray[num4]), ",");
						goto IL_05d3;
						IL_05d3:
						num2 = 92;
						if (num8 == 0)
						{
							goto IL_05e1;
						}
						goto IL_0633;
						IL_05e1:
						num2 = 93;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + "," + text2 + Strings.RTrim(DynArray[num4]) + text3 + CBEnd;
						goto IL_06d9;
						IL_0633:
						num2 = 95;
						text10 = "(" + text2 + Strings.RTrim(Strings.Mid(DynArray[num4], 1, num8 - 1)) + text3 + "," + text2 + Strings.Mid(Strings.RTrim(DynArray[num4]), num8 + 1) + text3 + ")";
						goto IL_069b;
						IL_069b:
						num2 = 96;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + "," + text10 + CBEnd;
						goto IL_06d9;
						IL_06d9:
						num2 = 100;
						num4++;
						goto IL_06e2;
						IL_070f:
						num2 = 103;
						if (Operators.CompareString(left, "L", TextCompare: false) != 0)
						{
							break;
						}
						goto IL_072b;
						IL_072b:
						num2 = 104;
						num13 = num7;
						num4 = 1;
						goto IL_0948;
						IL_0948:
						if (num4 <= num13)
						{
							goto IL_073a;
						}
						goto IL_0951;
						IL_0951:
						num2 = 124;
						Globals_Renamed.currinputstrtmp = Globals_Renamed.currinputstrtmp + Globals_Renamed.CRLF + CBBegin + ")";
						break;
						IL_073a:
						num2 = 105;
						if (num9 == 1)
						{
							goto IL_074b;
						}
						goto IL_082e;
						IL_074b:
						num2 = 106;
						if (Operators.CompareString(Strings.Trim(DynArray[num4]), "", TextCompare: false) != 0)
						{
							goto IL_076e;
						}
						goto IL_093f;
						IL_076e:
						num2 = 107;
						if ((Strings.InStr(DynArray[num4], "%") == 0) & (Strings.InStr(DynArray[num4], "_") == 0))
						{
							goto IL_079c;
						}
						goto IL_07de;
						IL_079c:
						num2 = 108;
						Globals_Renamed.currinputstrtmp = " " + text2 + Strings.RTrim(DynArray[num4]) + "%" + text3 + CBEnd;
						goto IL_081f;
						IL_07de:
						num2 = 110;
						Globals_Renamed.currinputstrtmp = CBEnd + Globals_Renamed.CRLF + CBBegin + text2 + Strings.RTrim(DynArray[num4]) + text3 + CBEnd;
						goto IL_081f;
						IL_081f:
						num2 = 112;
						num9 = 0;
						goto IL_093f;
						IL_082e:
						num2 = 115;
						if (Operators.CompareString(DynArray[num4], "", TextCompare: false) != 0)
						{
							goto IL_084c;
						}
						goto IL_093f;
						end_IL_0001_2:
						break;
					}
					num2 = 130;
					DynArray = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3076;
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

	public static void Set_Engine_Delimiters(ref string CBBegin, ref string CBEnd)
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
				case 63:
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
					CBBegin = "";
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				CBEnd = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 63;
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

	public static string SubStitute_Node_Schema(ref string Query_To_Run, short ShowUN, string ll_SPCMap, string ll_SPCMapHdr, string ll_SPCMap2, string ll_SPCMapHdr2, string ll_DBType, bool IsHlpQ = false)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		short num6 = default(short);
		short num7 = default(short);
		int num8 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		string text6 = default(string);
		string text7 = default(string);
		string text8 = default(string);
		string text9 = default(string);
		string text10 = default(string);
		string text11 = default(string);
		string text12 = default(string);
		string OUser = default(string);
		string OPwd = default(string);
		string text13 = default(string);
		string OUser2 = default(string);
		string OPwd2 = default(string);
		string OUser3 = default(string);
		string OUser4 = default(string);
		string OUser5 = default(string);
		string OPwd3 = default(string);
		string OPwd4 = default(string);
		string OPwd5 = default(string);
		string text14 = default(string);
		short num9 = default(short);
		string text15 = default(string);
		string text16 = default(string);
		string result = default(string);
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
					case 5243:
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
							case 4:
								goto IL_0015;
							case 5:
								goto IL_001a;
							case 6:
								goto IL_001f;
							case 7:
								goto IL_0028;
							case 8:
								goto IL_0031;
							case 9:
								goto IL_003a;
							case 10:
								goto IL_0044;
							case 11:
								goto IL_004e;
							case 12:
								goto IL_0058;
							case 13:
								goto IL_0062;
							case 14:
								goto IL_006c;
							case 15:
								goto IL_0076;
							case 16:
								goto IL_0080;
							case 17:
								goto IL_008a;
							case 18:
								goto IL_0094;
							case 19:
								goto IL_009e;
							case 20:
								goto IL_00a8;
							case 21:
								goto IL_00b2;
							case 22:
								goto IL_00bc;
							case 23:
								goto IL_00c6;
							case 24:
								goto IL_00d0;
							case 25:
								goto IL_00da;
							case 26:
								goto IL_00e4;
							case 27:
								goto IL_00ee;
							case 28:
								goto IL_00f8;
							case 29:
								goto IL_0102;
							case 30:
								goto IL_010c;
							case 31:
								goto IL_0112;
							case 32:
								goto IL_011c;
							case 33:
								goto IL_0126;
							case 34:
								goto IL_0179;
							case 36:
								goto IL_0187;
							case 35:
							case 37:
							case 38:
								goto IL_019c;
							case 39:
								goto IL_01ef;
							case 41:
								goto IL_01fd;
							case 40:
							case 42:
							case 43:
								goto IL_0212;
							case 44:
								goto IL_0265;
							case 46:
								goto IL_0273;
							case 45:
							case 47:
							case 48:
								goto IL_0288;
							case 49:
								goto IL_02db;
							case 51:
								goto IL_02e9;
							case 50:
							case 52:
							case 53:
								goto IL_02fe;
							case 54:
								goto IL_0310;
							case 55:
								goto IL_0321;
							case 56:
								goto IL_033a;
							case 58:
								goto IL_034b;
							case 59:
								goto IL_035e;
							case 60:
								goto IL_0375;
							case 61:
								goto IL_0396;
							case 62:
								goto IL_03cf;
							case 67:
								goto IL_03e8;
							case 68:
								goto IL_0406;
							case 57:
							case 63:
							case 64:
							case 65:
							case 66:
							case 69:
							case 70:
								goto IL_0411;
							case 71:
								goto IL_042a;
							case 72:
								goto IL_0474;
							case 73:
								goto IL_047e;
							case 74:
								goto IL_04bd;
							case 76:
								goto IL_04cb;
							case 75:
							case 77:
							case 78:
								goto IL_04d6;
							case 79:
								goto IL_04e6;
							case 80:
								goto IL_04f6;
							case 81:
								goto IL_0506;
							case 82:
								goto IL_0516;
							case 83:
								goto IL_0526;
							case 84:
								goto IL_0534;
							case 85:
								goto IL_0574;
							case 86:
								goto IL_057e;
							case 88:
								goto IL_058c;
							case 89:
								goto IL_0596;
							case 92:
								goto IL_05a5;
							case 93:
								goto IL_05af;
							case 87:
							case 90:
							case 91:
							case 94:
							case 95:
								goto IL_05ba;
							case 96:
								goto IL_05d3;
							case 97:
								goto IL_05d9;
							case 98:
								goto IL_05e7;
							case 100:
								goto IL_05f7;
							case 101:
								goto IL_0605;
							case 103:
								goto IL_0615;
							case 104:
								goto IL_0623;
							case 106:
								goto IL_0633;
							case 107:
								goto IL_0641;
							case 109:
								goto IL_0651;
							case 110:
								goto IL_065f;
							case 112:
								goto IL_066f;
							case 113:
								goto IL_067d;
							case 115:
								goto IL_068d;
							case 116:
								goto IL_069b;
							case 118:
								goto IL_06ab;
							case 119:
								goto IL_06b9;
							case 121:
								goto IL_06c9;
							case 122:
								goto IL_06d8;
							case 124:
								goto IL_06e8;
							case 125:
								goto IL_06f7;
							case 127:
								goto IL_0707;
							case 128:
								goto IL_0716;
							case 130:
								goto IL_0729;
							case 131:
								goto IL_073b;
							case 133:
								goto IL_074e;
							case 134:
								goto IL_0760;
							case 136:
								goto IL_0773;
							case 137:
								goto IL_0785;
							case 139:
								goto IL_0798;
							case 140:
								goto IL_07aa;
							case 142:
								goto IL_07bd;
							case 143:
								goto IL_07cf;
							case 145:
								goto IL_07e2;
							case 146:
								goto IL_07f4;
							case 148:
								goto IL_0807;
							case 149:
								goto IL_0819;
							case 151:
								goto IL_082c;
							case 152:
								goto IL_083e;
							case 154:
								goto IL_0851;
							case 155:
								goto IL_0863;
							case 157:
								goto IL_0876;
							case 158:
								goto IL_0888;
							case 160:
								goto IL_089b;
							case 161:
								goto IL_08ad;
							case 163:
								goto IL_08c0;
							case 164:
								goto IL_08d2;
							case 166:
								goto IL_08e5;
							case 167:
								goto IL_08f7;
							case 169:
								goto IL_090a;
							case 170:
								goto IL_091c;
							case 172:
								goto IL_092f;
							case 173:
								goto IL_0941;
							case 175:
								goto IL_0954;
							case 176:
								goto IL_0966;
							case 178:
								goto IL_0979;
							case 179:
								goto IL_098b;
							case 181:
								goto IL_099b;
							case 182:
								goto IL_09ad;
							case 184:
								goto IL_09bd;
							case 185:
								goto IL_09cf;
							case 187:
								goto IL_09df;
							case 188:
								goto IL_09f1;
							case 99:
							case 102:
							case 105:
							case 108:
							case 111:
							case 114:
							case 117:
							case 120:
							case 123:
							case 126:
							case 129:
							case 132:
							case 135:
							case 138:
							case 141:
							case 144:
							case 147:
							case 150:
							case 153:
							case 156:
							case 159:
							case 162:
							case 165:
							case 168:
							case 171:
							case 174:
							case 177:
							case 180:
							case 183:
							case 186:
							case 189:
							case 190:
								goto IL_09ff;
							case 191:
								goto IL_0a0f;
							case 192:
							case 193:
								goto IL_0a19;
							case 194:
								goto IL_0a32;
							case 195:
								goto IL_0a46;
							case 196:
								goto IL_0a63;
							case 197:
								goto IL_0a74;
							case 199:
								goto IL_0a8e;
							case 200:
								goto IL_0a9f;
							case 202:
								goto IL_0ab9;
							case 203:
								goto IL_0aca;
							case 205:
								goto IL_0ae1;
							case 206:
								goto IL_0af2;
							case 208:
								goto IL_0b09;
							case 209:
								goto IL_0b1a;
							case 211:
								goto IL_0b31;
							case 212:
								goto IL_0b42;
							case 214:
								goto IL_0b59;
							case 215:
								goto IL_0b6a;
							case 217:
								goto IL_0b81;
							case 218:
								goto IL_0b92;
							case 220:
								goto IL_0ba9;
							case 221:
								goto IL_0bbb;
							case 223:
								goto IL_0bd2;
							case 224:
								goto IL_0be4;
							case 226:
								goto IL_0bfb;
							case 227:
								goto IL_0c0d;
							case 229:
								goto IL_0c24;
							case 230:
								goto IL_0c36;
							case 232:
								goto IL_0c4d;
							case 233:
								goto IL_0c5f;
							case 235:
								goto IL_0c76;
							case 236:
								goto IL_0c88;
							case 238:
								goto IL_0c9f;
							case 239:
								goto IL_0cb1;
							case 241:
								goto IL_0cc8;
							case 242:
								goto IL_0cda;
							case 244:
								goto IL_0cf1;
							case 245:
								goto IL_0d03;
							case 247:
								goto IL_0d1a;
							case 248:
								goto IL_0d2c;
							case 250:
								goto IL_0d43;
							case 251:
								goto IL_0d55;
							case 253:
								goto IL_0d6c;
							case 254:
								goto IL_0d7e;
							case 256:
								goto IL_0d95;
							case 257:
								goto IL_0da7;
							case 259:
								goto IL_0dbe;
							case 260:
								goto IL_0dd0;
							case 262:
								goto IL_0de7;
							case 263:
								goto IL_0df9;
							case 265:
								goto IL_0e10;
							case 266:
								goto IL_0e22;
							case 268:
								goto IL_0e39;
							case 269:
								goto IL_0e4b;
							case 271:
								goto IL_0e62;
							case 272:
								goto IL_0e74;
							case 274:
								goto IL_0e8b;
							case 275:
								goto IL_0e9d;
							case 277:
								goto IL_0eb3;
							case 278:
								goto IL_0ec5;
							case 280:
								goto IL_0ed8;
							case 281:
								goto IL_0eea;
							case 283:
								goto IL_0f06;
							case 284:
								goto IL_0f18;
							case 286:
								goto IL_0f2c;
							case 287:
								goto IL_0f3e;
							case 198:
							case 201:
							case 204:
							case 207:
							case 210:
							case 213:
							case 216:
							case 219:
							case 222:
							case 225:
							case 228:
							case 231:
							case 234:
							case 237:
							case 240:
							case 243:
							case 246:
							case 249:
							case 252:
							case 255:
							case 258:
							case 261:
							case 264:
							case 267:
							case 270:
							case 273:
							case 276:
							case 279:
							case 282:
							case 285:
							case 288:
							case 289:
								goto IL_0f50;
							case 290:
							case 291:
								goto IL_0f60;
							case 292:
								goto IL_0f74;
							case 293:
								goto IL_0f8c;
							case 294:
								goto IL_0f96;
							case 295:
								goto IL_0fa3;
							case 296:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 297:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0f8c:
						num2 = 293;
						Query_To_Run = text;
						goto IL_0f96;
						IL_0f96:
						num2 = 294;
						text = "";
						goto IL_0fa3;
						IL_0f74:
						num2 = 292;
						text += Strings.Mid(Query_To_Run, num5);
						goto IL_0f8c;
						IL_0fa3:
						num2 = 295;
						num6 = (short)unchecked(num6 + 1);
						if (num6 > 31)
						{
							break;
						}
						goto IL_05d9;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num7 = 0;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						num5 = 0;
						goto IL_001a;
						IL_001a:
						num2 = 5;
						num8 = 0;
						goto IL_001f;
						IL_001f:
						num2 = 6;
						text2 = "";
						goto IL_0028;
						IL_0028:
						num2 = 7;
						text = "";
						goto IL_0031;
						IL_0031:
						num2 = 8;
						text3 = "";
						goto IL_003a;
						IL_003a:
						num2 = 9;
						text4 = "";
						goto IL_0044;
						IL_0044:
						num2 = 10;
						text5 = "";
						goto IL_004e;
						IL_004e:
						num2 = 11;
						text6 = "";
						goto IL_0058;
						IL_0058:
						num2 = 12;
						text7 = "";
						goto IL_0062;
						IL_0062:
						num2 = 13;
						text8 = "";
						goto IL_006c;
						IL_006c:
						num2 = 14;
						text9 = "";
						goto IL_0076;
						IL_0076:
						num2 = 15;
						text10 = "";
						goto IL_0080;
						IL_0080:
						num2 = 16;
						text11 = "";
						goto IL_008a;
						IL_008a:
						num2 = 17;
						text12 = "";
						goto IL_0094;
						IL_0094:
						num2 = 18;
						OUser = "";
						goto IL_009e;
						IL_009e:
						num2 = 19;
						OPwd = "";
						goto IL_00a8;
						IL_00a8:
						num2 = 20;
						text13 = "";
						goto IL_00b2;
						IL_00b2:
						num2 = 21;
						OUser2 = "";
						goto IL_00bc;
						IL_00bc:
						num2 = 22;
						OPwd2 = "";
						goto IL_00c6;
						IL_00c6:
						num2 = 23;
						OUser3 = "";
						goto IL_00d0;
						IL_00d0:
						num2 = 24;
						OUser4 = "";
						goto IL_00da;
						IL_00da:
						num2 = 25;
						OUser5 = "";
						goto IL_00e4;
						IL_00e4:
						num2 = 26;
						OPwd3 = "";
						goto IL_00ee;
						IL_00ee:
						num2 = 27;
						OPwd4 = "";
						goto IL_00f8;
						IL_00f8:
						num2 = 28;
						OPwd5 = "";
						goto IL_0102;
						IL_0102:
						num2 = 29;
						text14 = "";
						goto IL_010c;
						IL_010c:
						num2 = 30;
						num9 = 0;
						goto IL_0112;
						IL_0112:
						num2 = 31;
						text15 = "";
						goto IL_011c;
						IL_011c:
						num2 = 32;
						text16 = "";
						goto IL_0126;
						IL_0126:
						num2 = 33;
						if ((Operators.CompareString(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1)), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1))), "NONE", TextCompare: false) == 0))
						{
							goto IL_0179;
						}
						goto IL_0187;
						IL_0179:
						num2 = 34;
						text9 = "";
						goto IL_019c;
						IL_0187:
						num2 = 36;
						text9 = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
						goto IL_019c;
						IL_019c:
						num2 = 38;
						if ((Operators.CompareString(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1)), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1))), "NONE", TextCompare: false) == 0))
						{
							goto IL_01ef;
						}
						goto IL_01fd;
						IL_01ef:
						num2 = 39;
						text10 = "";
						goto IL_0212;
						IL_01fd:
						num2 = 41;
						text10 = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
						goto IL_0212;
						IL_0212:
						num2 = 43;
						if ((Operators.CompareString(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1)), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1))), "NONE", TextCompare: false) == 0))
						{
							goto IL_0265;
						}
						goto IL_0273;
						IL_0265:
						num2 = 44;
						text12 = "";
						goto IL_0288;
						IL_0273:
						num2 = 46;
						text12 = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
						goto IL_0288;
						IL_0288:
						num2 = 48;
						if ((Operators.CompareString(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1)), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1))), "NONE", TextCompare: false) == 0))
						{
							goto IL_02db;
						}
						goto IL_02e9;
						IL_02db:
						num2 = 49;
						text13 = "";
						goto IL_02fe;
						IL_02e9:
						num2 = 51;
						text13 = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
						goto IL_02fe;
						IL_02fe:
						num2 = 53;
						num8 = Strings.InStr(text13, ".(");
						goto IL_0310;
						IL_0310:
						num2 = 54;
						if (num8 != 0)
						{
							goto IL_0321;
						}
						goto IL_03e8;
						IL_0321:
						num2 = 55;
						if (Strings.InStr(text13, ",") != 0)
						{
							goto IL_033a;
						}
						goto IL_034b;
						IL_033a:
						num2 = 56;
						text14 = "@[]@";
						goto IL_0411;
						IL_034b:
						num2 = 58;
						num9 = (short)Strings.InStr(text13, ").");
						goto IL_035e;
						IL_035e:
						num2 = 59;
						if ((num9 != 0) & (num9 > num8 + 2))
						{
							goto IL_0375;
						}
						goto IL_0411;
						IL_0375:
						num2 = 60;
						text14 = Strings.Trim(Strings.UCase(Strings.Mid(text13, num8 + 2, num9 - (num8 + 2))));
						goto IL_0396;
						IL_0396:
						num2 = 61;
						if (LikeOperator.LikeString(Strings.UCase(text14), "SCANV_*", CompareMethod.Binary) & (Operators.CompareString(Strings.Mid(text14, Strings.Len(text14), 1), ".", TextCompare: false) != 0))
						{
							goto IL_03cf;
						}
						goto IL_0411;
						IL_03cf:
						num2 = 62;
						text14 += ".";
						goto IL_0411;
						IL_03e8:
						num2 = 67;
						if (Strings.InStr(Strings.LCase(text13), "<<<spf-site>>>") != 0)
						{
							goto IL_0406;
						}
						goto IL_0411;
						IL_0406:
						num2 = 68;
						text14 = "@[]@";
						goto IL_0411;
						IL_0411:
						num2 = 70;
						text3 = Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyDBSchema, Globals_Renamed.MyDBSchema1));
						goto IL_042a;
						IL_042a:
						num2 = 71;
						if ((Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0) & (Operators.CompareString(Strings.UCase(text3), "MC_1_", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(Strings.UCase(text13)), "ATD.ASPIRE", TextCompare: false) == 0))
						{
							goto IL_0474;
						}
						goto IL_047e;
						IL_0474:
						num2 = 72;
						text3 = "A43_PROD_0.";
						goto IL_047e;
						IL_047e:
						num2 = 73;
						if ((Operators.CompareString(Strings.Trim(Globals_Renamed.MyARIESClassServer1), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(Strings.Trim(Globals_Renamed.MyARIESClassServer1)), "NONE", TextCompare: false) == 0))
						{
							goto IL_04bd;
						}
						goto IL_04cb;
						IL_04bd:
						num2 = 74;
						text11 = "";
						goto IL_04d6;
						IL_04cb:
						num2 = 76;
						text11 = Globals_Renamed.MyARIESClassServer1;
						goto IL_04d6;
						IL_04d6:
						num2 = 78;
						General_Procedures.GetUNPWList(ShowUN, text9, ref OUser, ref OPwd);
						goto IL_04e6;
						IL_04e6:
						num2 = 79;
						General_Procedures.GetUNPWList(ShowUN, text10, ref OUser3, ref OPwd3);
						goto IL_04f6;
						IL_04f6:
						num2 = 80;
						General_Procedures.GetUNPWList(ShowUN, text12, ref OUser4, ref OPwd4);
						goto IL_0506;
						IL_0506:
						num2 = 81;
						General_Procedures.GetUNPWList(ShowUN, text11, ref OUser5, ref OPwd5);
						goto IL_0516;
						IL_0516:
						num2 = 82;
						General_Procedures.GetUNPWList(ShowUN, text13, ref OUser2, ref OPwd2);
						goto IL_0526;
						IL_0526:
						num2 = 83;
						if (!IsHlpQ)
						{
							goto IL_0534;
						}
						goto IL_05a5;
						IL_0534:
						num2 = 84;
						if (Operators.CompareString(General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "JOIN:All")), "Yes", TextCompare: false) == 0)
						{
							goto IL_0574;
						}
						goto IL_058c;
						IL_0574:
						num2 = 85;
						text16 = " (+)";
						goto IL_057e;
						IL_057e:
						num2 = 86;
						text15 = "1";
						goto IL_05ba;
						IL_058c:
						num2 = 88;
						text16 = "";
						goto IL_0596;
						IL_0596:
						num2 = 89;
						text15 = "0";
						goto IL_05ba;
						IL_05a5:
						num2 = 92;
						text16 = "";
						goto IL_05af;
						IL_05af:
						num2 = 93;
						text15 = "0";
						goto IL_05ba;
						IL_05ba:
						num2 = 95;
						Query_To_Run = Strings.Replace(Query_To_Run, "@MIDASNODE@", "", 1, -1, CompareMethod.Text);
						goto IL_05d3;
						IL_05d3:
						num2 = 96;
						num6 = 1;
						goto IL_05d9;
						IL_05d9:
						num2 = 97;
						if (num6 == 1)
						{
							goto IL_05e7;
						}
						goto IL_05f7;
						IL_05e7:
						num2 = 98;
						text2 = "@START_OF_WW@(";
						goto IL_09ff;
						IL_05f7:
						num2 = 100;
						if (num6 == 2)
						{
							goto IL_0605;
						}
						goto IL_0615;
						IL_0605:
						num2 = 101;
						text2 = "@END_OF_WW@(";
						goto IL_09ff;
						IL_0615:
						num2 = 103;
						if (num6 == 3)
						{
							goto IL_0623;
						}
						goto IL_0633;
						IL_0623:
						num2 = 104;
						text2 = "@MARSSCHEMA@";
						goto IL_09ff;
						IL_0633:
						num2 = 106;
						if (num6 == 4)
						{
							goto IL_0641;
						}
						goto IL_0651;
						IL_0641:
						num2 = 107;
						text2 = "@MARSNODE@";
						goto IL_09ff;
						IL_0651:
						num2 = 109;
						if (num6 == 5)
						{
							goto IL_065f;
						}
						goto IL_066f;
						IL_065f:
						num2 = 110;
						text2 = "@ARIESNODE@";
						goto IL_09ff;
						IL_066f:
						num2 = 112;
						if (num6 == 6)
						{
							goto IL_067d;
						}
						goto IL_068d;
						IL_067d:
						num2 = 113;
						text2 = "@OASYSNODE@";
						goto IL_09ff;
						IL_068d:
						num2 = 115;
						if (num6 == 7)
						{
							goto IL_069b;
						}
						goto IL_06ab;
						IL_069b:
						num2 = 116;
						text2 = "@ARIESCLASSNODE@";
						goto IL_09ff;
						IL_06ab:
						num2 = 118;
						if (num6 == 8)
						{
							goto IL_06b9;
						}
						goto IL_06c9;
						IL_06b9:
						num2 = 119;
						text2 = "@MARSSERVER@";
						goto IL_09ff;
						IL_06c9:
						num2 = 121;
						if (num6 == 9)
						{
							goto IL_06d8;
						}
						goto IL_06e8;
						IL_06d8:
						num2 = 122;
						text2 = "@ARIESSERVER@";
						goto IL_09ff;
						IL_06e8:
						num2 = 124;
						if (num6 == 10)
						{
							goto IL_06f7;
						}
						goto IL_0707;
						IL_06f7:
						num2 = 125;
						text2 = "@OASYSSERVER@";
						goto IL_09ff;
						IL_0707:
						num2 = 127;
						if (num6 == 11)
						{
							goto IL_0716;
						}
						goto IL_0729;
						IL_0716:
						num2 = 128;
						text2 = "@ARIESCLASSSERVER@";
						goto IL_09ff;
						IL_0729:
						num2 = 130;
						if (num6 == 12)
						{
							goto IL_073b;
						}
						goto IL_074e;
						IL_073b:
						num2 = 131;
						text2 = "@MARSUSERNAME@";
						goto IL_09ff;
						IL_074e:
						num2 = 133;
						if (num6 == 13)
						{
							goto IL_0760;
						}
						goto IL_0773;
						IL_0760:
						num2 = 134;
						text2 = "@MARSPASSWORD@";
						goto IL_09ff;
						IL_0773:
						num2 = 136;
						if (num6 == 14)
						{
							goto IL_0785;
						}
						goto IL_0798;
						IL_0785:
						num2 = 137;
						text2 = "@OUTERJOIN@";
						goto IL_09ff;
						IL_0798:
						num2 = 139;
						if (num6 == 15)
						{
							goto IL_07aa;
						}
						goto IL_07bd;
						IL_07aa:
						num2 = 140;
						text2 = "@OTHERNODE@";
						goto IL_09ff;
						IL_07bd:
						num2 = 142;
						if (num6 == 16)
						{
							goto IL_07cf;
						}
						goto IL_07e2;
						IL_07cf:
						num2 = 143;
						text2 = "@OTHERSERVER@";
						goto IL_09ff;
						IL_07e2:
						num2 = 145;
						if (num6 == 17)
						{
							goto IL_07f4;
						}
						goto IL_0807;
						IL_07f4:
						num2 = 146;
						text2 = "@OTHERUSERNAME@";
						goto IL_09ff;
						IL_0807:
						num2 = 148;
						if (num6 == 18)
						{
							goto IL_0819;
						}
						goto IL_082c;
						IL_0819:
						num2 = 149;
						text2 = "@OTHERPASSWORD@";
						goto IL_09ff;
						IL_082c:
						num2 = 151;
						if (num6 == 19)
						{
							goto IL_083e;
						}
						goto IL_0851;
						IL_083e:
						num2 = 152;
						text2 = "@OUN@";
						goto IL_09ff;
						IL_0851:
						num2 = 154;
						if (num6 == 20)
						{
							goto IL_0863;
						}
						goto IL_0876;
						IL_0863:
						num2 = 155;
						text2 = "@AUN@";
						goto IL_09ff;
						IL_0876:
						num2 = 157;
						if (num6 == 21)
						{
							goto IL_0888;
						}
						goto IL_089b;
						IL_0888:
						num2 = 158;
						text2 = "@ACUN@";
						goto IL_09ff;
						IL_089b:
						num2 = 160;
						if (num6 == 22)
						{
							goto IL_08ad;
						}
						goto IL_08c0;
						IL_08ad:
						num2 = 161;
						text2 = "@OPW@";
						goto IL_09ff;
						IL_08c0:
						num2 = 163;
						if (num6 == 23)
						{
							goto IL_08d2;
						}
						goto IL_08e5;
						IL_08d2:
						num2 = 164;
						text2 = "@APW@";
						goto IL_09ff;
						IL_08e5:
						num2 = 166;
						if (num6 == 24)
						{
							goto IL_08f7;
						}
						goto IL_090a;
						IL_08f7:
						num2 = 167;
						text2 = "@ACPW@";
						goto IL_09ff;
						IL_090a:
						num2 = 169;
						if (num6 == 25)
						{
							goto IL_091c;
						}
						goto IL_092f;
						IL_091c:
						num2 = 170;
						text2 = "@ORCLOJ@";
						goto IL_09ff;
						IL_092f:
						num2 = 172;
						if (num6 == 26)
						{
							goto IL_0941;
						}
						goto IL_0954;
						IL_0941:
						num2 = 173;
						text2 = "@OTHERSCHEMA@";
						goto IL_09ff;
						IL_0954:
						num2 = 175;
						if (num6 == 27)
						{
							goto IL_0966;
						}
						goto IL_0979;
						IL_0966:
						num2 = 176;
						text2 = "@SPC_PROC_OPER_HDR_MAP@";
						goto IL_09ff;
						IL_0979:
						num2 = 178;
						if (num6 == 28)
						{
							goto IL_098b;
						}
						goto IL_099b;
						IL_098b:
						num2 = 179;
						text2 = "@SPC_PROC_OPER_MAP@";
						goto IL_09ff;
						IL_099b:
						num2 = 181;
						if (num6 == 29)
						{
							goto IL_09ad;
						}
						goto IL_09bd;
						IL_09ad:
						num2 = 182;
						text2 = "@ID-SID@";
						goto IL_09ff;
						IL_09bd:
						num2 = 184;
						if (num6 == 30)
						{
							goto IL_09cf;
						}
						goto IL_09df;
						IL_09cf:
						num2 = 185;
						text2 = "@SPC_PROC_OPER_HDR_MAP2@";
						goto IL_09ff;
						IL_09df:
						num2 = 187;
						if (num6 == 31)
						{
							goto IL_09f1;
						}
						goto IL_09ff;
						IL_09f1:
						num2 = 188;
						text2 = "@SPC_PROC_OPER_MAP2@";
						goto IL_09ff;
						IL_09ff:
						num2 = 190;
						num7 = (short)Strings.Len(text2);
						goto IL_0a0f;
						IL_0a0f:
						num2 = 191;
						num5 = 1;
						goto IL_0a19;
						IL_0a19:
						num2 = 193;
						num8 = Strings.InStr(num5, Strings.UCase(Query_To_Run), text2);
						goto IL_0a32;
						IL_0a32:
						num2 = 194;
						if (num8 != 0)
						{
							goto IL_0a46;
						}
						goto IL_0f60;
						IL_0a46:
						num2 = 195;
						text += Strings.Mid(Query_To_Run, num5, num8 - num5);
						goto IL_0a63;
						IL_0a63:
						num2 = 196;
						if (num6 == 1)
						{
							goto IL_0a74;
						}
						goto IL_0a8e;
						IL_0a74:
						num2 = 197;
						text += "(SELECT MIN(c99.Start_Date) FROM @MARSSCHEMA@F_Calendar c99 WHERE c99.event_code = 'S' AND c99.ww = ";
						goto IL_0f50;
						IL_0a8e:
						num2 = 199;
						if (num6 == 2)
						{
							goto IL_0a9f;
						}
						goto IL_0ab9;
						IL_0a9f:
						num2 = 200;
						text += "(SELECT MAX(c99.End_Date) FROM @MARSSCHEMA@F_Calendar c99 WHERE c99.event_code = 'S' AND c99.ww = ";
						goto IL_0f50;
						IL_0ab9:
						num2 = 202;
						if (num6 == 3)
						{
							goto IL_0aca;
						}
						goto IL_0ae1;
						IL_0aca:
						num2 = 203;
						text += text3;
						goto IL_0f50;
						IL_0ae1:
						num2 = 205;
						if (num6 == 4)
						{
							goto IL_0af2;
						}
						goto IL_0b09;
						IL_0af2:
						num2 = 206;
						text += text4;
						goto IL_0f50;
						IL_0b09:
						num2 = 208;
						if (num6 == 5)
						{
							goto IL_0b1a;
						}
						goto IL_0b31;
						IL_0b1a:
						num2 = 209;
						text += text5;
						goto IL_0f50;
						IL_0b31:
						num2 = 211;
						if (num6 == 6)
						{
							goto IL_0b42;
						}
						goto IL_0b59;
						IL_0b42:
						num2 = 212;
						text += text7;
						goto IL_0f50;
						IL_0b59:
						num2 = 214;
						if (num6 == 7)
						{
							goto IL_0b6a;
						}
						goto IL_0b81;
						IL_0b6a:
						num2 = 215;
						text += text6;
						goto IL_0f50;
						IL_0b81:
						num2 = 217;
						if (num6 == 8)
						{
							goto IL_0b92;
						}
						goto IL_0ba9;
						IL_0b92:
						num2 = 218;
						text += text9;
						goto IL_0f50;
						IL_0ba9:
						num2 = 220;
						if (num6 == 9)
						{
							goto IL_0bbb;
						}
						goto IL_0bd2;
						IL_0bbb:
						num2 = 221;
						text += text10;
						goto IL_0f50;
						IL_0bd2:
						num2 = 223;
						if (num6 == 10)
						{
							goto IL_0be4;
						}
						goto IL_0bfb;
						IL_0be4:
						num2 = 224;
						text += text12;
						goto IL_0f50;
						IL_0bfb:
						num2 = 226;
						if (num6 == 11)
						{
							goto IL_0c0d;
						}
						goto IL_0c24;
						IL_0c0d:
						num2 = 227;
						text += text11;
						goto IL_0f50;
						IL_0c24:
						num2 = 229;
						if (num6 == 12)
						{
							goto IL_0c36;
						}
						goto IL_0c4d;
						IL_0c36:
						num2 = 230;
						text += OUser;
						goto IL_0f50;
						IL_0c4d:
						num2 = 232;
						if (num6 == 13)
						{
							goto IL_0c5f;
						}
						goto IL_0c76;
						IL_0c5f:
						num2 = 233;
						text += OPwd;
						goto IL_0f50;
						IL_0c76:
						num2 = 235;
						if (num6 == 14)
						{
							goto IL_0c88;
						}
						goto IL_0c9f;
						IL_0c88:
						num2 = 236;
						text += text15;
						goto IL_0f50;
						IL_0c9f:
						num2 = 238;
						if (num6 == 15)
						{
							goto IL_0cb1;
						}
						goto IL_0cc8;
						IL_0cb1:
						num2 = 239;
						text += text8;
						goto IL_0f50;
						IL_0cc8:
						num2 = 241;
						if (num6 == 16)
						{
							goto IL_0cda;
						}
						goto IL_0cf1;
						IL_0cda:
						num2 = 242;
						text += text13;
						goto IL_0f50;
						IL_0cf1:
						num2 = 244;
						if (num6 == 17)
						{
							goto IL_0d03;
						}
						goto IL_0d1a;
						IL_0d03:
						num2 = 245;
						text += OUser2;
						goto IL_0f50;
						IL_0d1a:
						num2 = 247;
						if (num6 == 18)
						{
							goto IL_0d2c;
						}
						goto IL_0d43;
						IL_0d2c:
						num2 = 248;
						text += OPwd2;
						goto IL_0f50;
						IL_0d43:
						num2 = 250;
						if (num6 == 19)
						{
							goto IL_0d55;
						}
						goto IL_0d6c;
						IL_0d55:
						num2 = 251;
						text += OUser4;
						goto IL_0f50;
						IL_0d6c:
						num2 = 253;
						if (num6 == 20)
						{
							goto IL_0d7e;
						}
						goto IL_0d95;
						IL_0d7e:
						num2 = 254;
						text += OUser3;
						goto IL_0f50;
						IL_0d95:
						num2 = 256;
						if (num6 == 21)
						{
							goto IL_0da7;
						}
						goto IL_0dbe;
						IL_0da7:
						num2 = 257;
						text += OUser5;
						goto IL_0f50;
						IL_0dbe:
						num2 = 259;
						if (num6 == 22)
						{
							goto IL_0dd0;
						}
						goto IL_0de7;
						IL_0dd0:
						num2 = 260;
						text += OPwd4;
						goto IL_0f50;
						IL_0de7:
						num2 = 262;
						if (num6 == 23)
						{
							goto IL_0df9;
						}
						goto IL_0e10;
						IL_0df9:
						num2 = 263;
						text += OPwd3;
						goto IL_0f50;
						IL_0e10:
						num2 = 265;
						if (num6 == 24)
						{
							goto IL_0e22;
						}
						goto IL_0e39;
						IL_0e22:
						num2 = 266;
						text += OPwd5;
						goto IL_0f50;
						IL_0e39:
						num2 = 268;
						if (num6 == 25)
						{
							goto IL_0e4b;
						}
						goto IL_0e62;
						IL_0e4b:
						num2 = 269;
						text += text16;
						goto IL_0f50;
						IL_0e62:
						num2 = 271;
						if (num6 == 26)
						{
							goto IL_0e74;
						}
						goto IL_0e8b;
						IL_0e74:
						num2 = 272;
						text += text14;
						goto IL_0f50;
						IL_0e8b:
						num2 = 274;
						if (num6 == 27)
						{
							goto IL_0e9d;
						}
						goto IL_0eb3;
						IL_0e9d:
						num2 = 275;
						text += ll_SPCMapHdr;
						goto IL_0f50;
						IL_0eb3:
						num2 = 277;
						if (num6 == 28)
						{
							goto IL_0ec5;
						}
						goto IL_0ed8;
						IL_0ec5:
						num2 = 278;
						text += ll_SPCMap;
						goto IL_0f50;
						IL_0ed8:
						num2 = 280;
						if (num6 == 29)
						{
							goto IL_0eea;
						}
						goto IL_0f06;
						IL_0eea:
						num2 = 281;
						text += Strings.LCase(Globals_Renamed.gWinuser);
						goto IL_0f50;
						IL_0f06:
						num2 = 283;
						if (num6 == 30)
						{
							goto IL_0f18;
						}
						goto IL_0f2c;
						IL_0f18:
						num2 = 284;
						text += ll_SPCMapHdr2;
						goto IL_0f50;
						IL_0f2c:
						num2 = 286;
						if (num6 == 31)
						{
							goto IL_0f3e;
						}
						goto IL_0f50;
						IL_0f3e:
						num2 = 287;
						text += ll_SPCMap2;
						goto IL_0f50;
						IL_0f50:
						num2 = 289;
						num5 = num8 + num7;
						goto IL_0f60;
						IL_0f60:
						num2 = 291;
						if (num8 != 0)
						{
							goto IL_0a19;
						}
						goto IL_0f74;
						end_IL_0001_2:
						break;
					}
					num2 = 296;
					result = Query_To_Run;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5243;
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

	public static short RunQ(string MyJob, short MyMode, string MyModal = "")
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num5 = default(int);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		short result = default(short);
		string myFile = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					num5 = 0;
					goto IL_0007;
				case 774:
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
						while (true)
						{
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_0007;
							case 3:
								goto IL_0010;
							case 4:
								goto IL_0019;
							case 5:
								goto IL_0029;
							case 6:
							case 7:
								goto IL_0033;
							case 8:
								goto IL_0063;
							case 9:
							case 10:
								goto IL_007e;
							case 11:
								goto IL_008b;
							case 12:
								goto IL_0095;
							case 14:
								goto IL_00ae;
							case 15:
								goto IL_00bb;
							case 16:
								goto IL_00cf;
							case 13:
							case 17:
							case 18:
								goto IL_00e6;
							case 19:
								goto IL_00eb;
							case 20:
								goto IL_00f9;
							case 22:
								goto IL_010c;
							case 23:
								goto IL_0128;
							case 24:
								goto IL_0140;
							case 25:
							case 26:
								goto IL_0150;
							case 27:
								goto IL_015e;
							case 28:
								goto IL_0179;
							case 30:
								goto IL_0182;
							case 31:
								goto IL_018c;
							case 32:
								goto IL_019a;
							case 33:
								goto end_IL_0001_2;
							case 37:
								num2 = 37;
								if (Information.Err().Number == 521)
								{
									goto IL_01ce;
								}
								goto case 40;
							case 38:
								goto IL_01ce;
							case 40:
								num2 = 40;
								Interaction.MsgBox("Error running Script: (" + Conversions.ToString(Information.Err().Number) + "-" + Conversion.ErrorToString() + "). Try again." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Script Error");
								goto end_IL_0001_3;
							default:
								goto end_IL_0001;
							case 21:
							case 29:
							case 34:
							case 35:
							case 36:
							case 39:
							case 41:
							case 42:
								goto end_IL_0001_3;
							}
							break;
							IL_01ce:
							num2 = 38;
							ProjectData.ClearProjectError();
							if (num == 0)
							{
								throw ProjectData.CreateProjectError(-2146828268);
							}
							num4 = num;
						}
						goto default;
					}
					IL_015e:
					num2 = 27;
					MyProject.Forms.FrmMain.FrmCmdSimf.RunCmdWindow(text);
					goto IL_0179;
					IL_0179:
					num2 = 28;
					result = 1;
					goto end_IL_0001_3;
					IL_0150:
					num2 = 26;
					BuildForm.Invoke_SQL_Emulator();
					goto IL_015e;
					IL_0182:
					num2 = 30;
					num5 = General_Procedures.Activate_Emulator_2();
					goto IL_018c;
					IL_018c:
					num2 = 31;
					if (num5 != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_019a;
					IL_0007:
					num2 = 2;
					myFile = "";
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text = "";
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (Globals_Renamed.gDelBatFile == 0)
					{
						goto IL_0029;
					}
					goto IL_0033;
					IL_0029:
					num2 = 5;
					Globals_Renamed.gDelBatFile = 1;
					goto IL_0033;
					IL_0033:
					num2 = 7;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "N", TextCompare: false) == 0 && Operators.CompareString(BuildForm.FNUsePyEngine(), "Y", TextCompare: false) == 0)
					{
						goto IL_0063;
					}
					goto IL_007e;
					IL_019a:
					num2 = 32;
					General_Procedures.Sendkeys2(text, wait: true);
					break;
					IL_0063:
					num2 = 8;
					MyJob = "TITLE " + Globals_Renamed.MYMSAccessTitle + "\r\n" + MyJob;
					goto IL_007e;
					IL_007e:
					num2 = 10;
					if (MyMode == 0)
					{
						goto IL_008b;
					}
					goto IL_00ae;
					IL_008b:
					num2 = 11;
					text = Globals_Renamed.gSPFCache;
					goto IL_0095;
					IL_0095:
					num2 = 12;
					myFile = Globals_Renamed.MyPCDir + text + ".bat";
					goto IL_00e6;
					IL_00ae:
					num2 = 14;
					if (MyMode == 1)
					{
						goto IL_00bb;
					}
					goto IL_00e6;
					IL_00bb:
					num2 = 15;
					text = Globals_Renamed.gSPFCache + "_";
					goto IL_00cf;
					IL_00cf:
					num2 = 16;
					myFile = Globals_Renamed.MyPCDir + text + ".bat";
					goto IL_00e6;
					IL_00e6:
					num2 = 18;
					result = 0;
					goto IL_00eb;
					IL_00eb:
					num2 = 19;
					num5 = General_Procedures.Save_SQL_Query(MyJob, myFile);
					goto IL_00f9;
					IL_00f9:
					num2 = 20;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_010c;
					IL_010c:
					num2 = 22;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						goto IL_0128;
					}
					goto IL_0182;
					IL_0128:
					num2 = 23;
					if (Operators.CompareString(MyModal, "M", TextCompare: false) == 0)
					{
						goto IL_0140;
					}
					goto IL_0150;
					IL_0140:
					num2 = 24;
					BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
					goto IL_0150;
					end_IL_0001_2:
					break;
				}
				num2 = 33;
				result = 1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 774;
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
}

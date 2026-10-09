using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildForm
{
	public static void Add_FrmView()
	{
		Frmtable2 frmtable = new Frmtable2();
		frmtable.ShowDialog();
		MyProject.Forms.FrmMain.Activate();
	}

	public static void Init_SH_Sites(string MyDir, ref ComboBox cmbsite)
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
				case 663:
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
							goto IL_002c;
						case 5:
							goto IL_0040;
						case 6:
							goto IL_0054;
						case 7:
							goto IL_0068;
						case 8:
							goto IL_007c;
						case 9:
							goto IL_0090;
						case 11:
							goto IL_00ab;
						case 12:
							goto IL_00c0;
						case 14:
							goto IL_00db;
						case 15:
							goto IL_00f0;
						case 17:
							goto IL_010b;
						case 18:
							goto IL_0120;
						case 19:
							goto IL_0135;
						case 21:
							goto IL_0151;
						case 22:
							goto IL_0166;
						case 23:
							goto IL_017b;
						case 24:
							goto IL_0190;
						case 25:
							goto IL_01a5;
						case 26:
							goto IL_01ba;
						case 27:
							goto IL_01cf;
						case 28:
							goto IL_01e4;
						case 10:
						case 13:
						case 16:
						case 20:
						case 29:
						case 30:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 31:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ba:
					num2 = 26;
					cmbsite.Items.Add("Kulim");
					goto IL_01cf;
					IL_01cf:
					num2 = 27;
					cmbsite.Items.Add("NewMexico");
					goto IL_01e4;
					IL_01a5:
					num2 = 25;
					cmbsite.Items.Add("Israel");
					goto IL_01ba;
					IL_01e4:
					num2 = 28;
					cmbsite.Items.Add("Oregon");
					break;
					IL_000b:
					num2 = 2;
					cmbsite.Items.Clear();
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (LikeOperator.LikeString(MyDir, "*\\\\SHUSER-PRODAT.*", CompareMethod.Binary))
					{
						goto IL_002c;
					}
					goto IL_00ab;
					IL_002c:
					num2 = 4;
					cmbsite.Items.Add("Arizona");
					goto IL_0040;
					IL_0040:
					num2 = 5;
					cmbsite.Items.Add("Chengdu");
					goto IL_0054;
					IL_0054:
					num2 = 6;
					cmbsite.Items.Add("CostaRica");
					goto IL_0068;
					IL_0068:
					num2 = 7;
					cmbsite.Items.Add("Kulim");
					goto IL_007c;
					IL_007c:
					num2 = 8;
					cmbsite.Items.Add("NewMexico");
					goto IL_0090;
					IL_0090:
					num2 = 9;
					cmbsite.Items.Add("Vietnam");
					break;
					IL_00ab:
					num2 = 11;
					if (LikeOperator.LikeString(MyDir, "*\\\\SHUSER-INTG.*", CompareMethod.Binary))
					{
						goto IL_00c0;
					}
					goto IL_00db;
					IL_00c0:
					num2 = 12;
					cmbsite.Items.Add("VF");
					break;
					IL_00db:
					num2 = 14;
					if (LikeOperator.LikeString(MyDir, "*\\\\SHUSER-DEV.*", CompareMethod.Binary))
					{
						goto IL_00f0;
					}
					goto IL_010b;
					IL_00f0:
					num2 = 15;
					cmbsite.Items.Add("Oregon");
					break;
					IL_010b:
					num2 = 17;
					if (LikeOperator.LikeString(MyDir, "*\\\\SHUSER-NSG.*", CompareMethod.Binary))
					{
						goto IL_0120;
					}
					goto IL_0151;
					IL_0120:
					num2 = 18;
					cmbsite.Items.Add("Dalian");
					goto IL_0135;
					IL_0135:
					num2 = 19;
					cmbsite.Items.Add("NewMexico");
					break;
					IL_0151:
					num2 = 21;
					cmbsite.Items.Add("VF");
					goto IL_0166;
					IL_0166:
					num2 = 22;
					cmbsite.Items.Add("Arizona");
					goto IL_017b;
					IL_017b:
					num2 = 23;
					cmbsite.Items.Add("Chengdu");
					goto IL_0190;
					IL_0190:
					num2 = 24;
					cmbsite.Items.Add("Ireland");
					goto IL_01a5;
					end_IL_0001_2:
					break;
				}
				num2 = 30;
				cmbsite.SelectedIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 663;
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

	public static bool IsUtil(string MyUtil)
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
				case 103:
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
							goto IL_002c;
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
					if (Operators.CompareString(Strings.UCase(Globals_Renamed.currhelptmp), Strings.UCase(MyUtil), TextCompare: false) != 0)
					{
						break;
					}
					goto IL_002c;
					IL_002c:
					num2 = 3;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 103;
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

	public static void Test_File_CRLF(string MyFile, ref string MyNewFile, ref string MyMsg, ref string MyEmbed, string MyEmbedPPQFlag)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string MyData = default(string);
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
				case 929:
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
							goto IL_001d;
						case 6:
							goto IL_0026;
						case 7:
							goto IL_002f;
						case 8:
							goto IL_0038;
						case 9:
							goto IL_0044;
						case 11:
							goto IL_0063;
						case 12:
							goto IL_007b;
						case 14:
							goto IL_008c;
						case 15:
							goto IL_009d;
						case 16:
							goto IL_00ae;
						case 17:
							goto IL_00bf;
						case 18:
							goto IL_00cd;
						case 20:
							goto IL_00de;
						case 21:
							goto IL_00f5;
						case 22:
							goto IL_011a;
						case 23:
							goto IL_0133;
						case 25:
							goto IL_0148;
						case 10:
						case 13:
						case 19:
						case 24:
						case 26:
						case 27:
						case 28:
						case 29:
						case 30:
							goto IL_016b;
						case 31:
							goto IL_0187;
						case 32:
							goto IL_0230;
						case 33:
						case 34:
						case 35:
							goto IL_023e;
						case 36:
							goto IL_0257;
						case 37:
							goto IL_0268;
						case 38:
							goto IL_0279;
						case 39:
							goto IL_0287;
						case 40:
							goto IL_02a4;
						case 42:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 41:
						case 43:
						case 44:
						case 45:
						case 46:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_023e:
					num2 = 35;
					if (Operators.CompareString(MyMsg, "", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0257;
					IL_0257:
					num2 = 36;
					num5 = Strings.InStr(MyData, "/START_EMBED_ALL");
					goto IL_0268;
					IL_0230:
					num2 = 32;
					MyMsg = "If you are creating a query, note that you cannot add a Pre/Post Query that itself contains Pre/Post Queres. If legitimate i.e., Pre/Post queries do not extend beyond 2 levels), please add them individually. If you are generating SQL (especially when using Pre/Post Queries that are stored externally), do ensure that none of your Pre/Post queries extend beyond 2 levels.";
					goto IL_023e;
					IL_0268:
					num2 = 37;
					num6 = Strings.InStr(MyData, "/END_EMBED_ALL");
					goto IL_0279;
					IL_000b:
					num2 = 2;
					MyData = "";
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
					MyEmbed = "";
					goto IL_0026;
					IL_0026:
					num2 = 6;
					MyNewFile = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					MyMsg = "";
					goto IL_0038;
					IL_0038:
					num2 = 8;
					MyMsg = General_Procedures.GetFileContents(MyFile, ref MyData);
					goto IL_0044;
					IL_0044:
					num2 = 9;
					if (Operators.CompareString(MyMsg, "", TextCompare: false) == 0)
					{
						goto IL_0063;
					}
					goto IL_016b;
					IL_0063:
					num2 = 11;
					if (Operators.CompareString(MyData, "", TextCompare: false) == 0)
					{
						goto IL_007b;
					}
					goto IL_008c;
					IL_007b:
					num2 = 12;
					MyMsg = "Query File is empty";
					goto IL_016b;
					IL_008c:
					num2 = 14;
					num5 = Strings.InStr(MyData, "\r\n\"OUTERJOIN:\",");
					goto IL_009d;
					IL_009d:
					num2 = 15;
					if (num5 == 0)
					{
						goto IL_00ae;
					}
					goto IL_016b;
					IL_00ae:
					num2 = 16;
					num6 = Strings.InStr(MyData, "\n\"OUTERJOIN:\",");
					goto IL_00bf;
					IL_00bf:
					num2 = 17;
					if (num6 == 0)
					{
						goto IL_00cd;
					}
					goto IL_00de;
					IL_00cd:
					num2 = 18;
					MyMsg = "Query file not in expected format. Line delimiter not recognized.";
					goto IL_016b;
					IL_00de:
					num2 = 20;
					MyData = Strings.Replace(MyData, "\n", "\r\n", 1, -1, CompareMethod.Text);
					goto IL_00f5;
					IL_00f5:
					num2 = 21;
					MyMsg = General_Procedures.Save_File_General(MyData, Globals_Renamed.MyPCDir + Strings.Trim(Globals_Renamed.gSPFCache) + ".vg2");
					goto IL_011a;
					IL_011a:
					num2 = 22;
					if (Operators.CompareString(MyMsg, "", TextCompare: false) != 0)
					{
						goto IL_0133;
					}
					goto IL_0148;
					IL_0133:
					num2 = 23;
					MyMsg = "Could not replace query file line delimiters - " + MyMsg;
					goto IL_016b;
					IL_0148:
					num2 = 25;
					MyNewFile = Globals_Renamed.MyPCDir + Strings.Trim(Globals_Renamed.gSPFCache) + ".vg2";
					goto IL_016b;
					IL_016b:
					num2 = 30;
					if (Operators.CompareString(MyMsg, "", TextCompare: false) == 0)
					{
						goto IL_0187;
					}
					goto IL_023e;
					IL_0187:
					num2 = 31;
					if ((Globals_Renamed.g_FrmIdx > 0 && Operators.CompareString(MyEmbedPPQFlag, "Y", TextCompare: false) == 0 && Strings.InStr(MyData, "\"EMBED:\",\"N\"") != 0 && Strings.InStr(MyData, "U->Pre/Post Query:") != 0) || (Globals_Renamed.g_FrmIdx > 0 && Operators.CompareString(MyEmbedPPQFlag, "N", TextCompare: false) == 0 && Strings.InStr(MyData, "\"EMBED:\",\"Y\"") != 0 && Strings.InStr(MyData, "U->Pre/Post Query:") != 0) || (Globals_Renamed.g_FrmIdx > 1 && Operators.CompareString(MyEmbedPPQFlag, "N", TextCompare: false) == 0 && Strings.InStr(MyData, "\"EMBED:\",\"N\"") != 0 && Strings.InStr(MyData, "U->Pre/Post Query:") != 0))
					{
						goto IL_0230;
					}
					goto IL_023e;
					IL_0279:
					num2 = 38;
					if (num6 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0287;
					IL_0287:
					num2 = 39;
					if (Globals_Renamed.g_FrmIdx <= 0 || checked(num6 - num5) <= 18)
					{
						break;
					}
					goto IL_02a4;
					IL_02a4:
					num2 = 40;
					MyMsg = "If you are creating a query, note that you cannot add a Pre/Post Query that itself contains Pre/Post Queres. If legitimate i.e., Pre/Post queries do not extend beyond 2 levels), please add them individually. If you are generating SQL (especially when using Pre/Post Queries that are stored externally), do ensure that none of your Pre/Post queries extend beyond 2 levels.";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 42;
				MyEmbed = Strings.Trim(checked(Strings.Mid(MyData, num5 + 16, num6 - (num5 + 16))));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 929;
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

	public static bool OpenDelimitedFile(string FileToOpen, ref object MyReader, ref string MyMsg)
	{
		bool result = true;
		MyMsg = "";
		if (!File.Exists(FileToOpen))
		{
			MyMsg = "File not found.";
			result = false;
		}
		else
		{
			try
			{
				MyReader = new TextFieldParser(FileToOpen);
				NewLateBinding.LateSet(MyReader, null, "TextFieldType", new object[1] { FieldType.Delimited }, null, null);
				NewLateBinding.LateCall(MyReader, null, "SetDelimiters", new object[1] { "," }, null, null, null, IgnoreReturn: true);
				NewLateBinding.LateSet(MyReader, null, "HasFieldsEnclosedInQuotes", new object[1] { true }, null, null);
				NewLateBinding.LateSet(MyReader, null, "TrimWhiteSpace", new object[1] { true }, null, null);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				MyMsg = ex2.Message;
				result = false;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	public static bool VerifySchemaRow(ref string[] currentRow, int MyCount, ref string MyMSG, string ObjectType = "-1")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 348:
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
							goto IL_0026;
						case 7:
							goto IL_0035;
						case 8:
							goto IL_003e;
						case 10:
							goto IL_0048;
						case 12:
							goto IL_0081;
						case 13:
							goto IL_0094;
						case 14:
							goto IL_009e;
						case 15:
							goto IL_00b0;
						case 16:
							goto IL_00c3;
						case 17:
							goto IL_00cd;
						case 18:
							goto IL_00dc;
						case 19:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 11:
						case 20:
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00dc:
					num2 = 18;
					MyMSG += ".";
					break;
					IL_00b0:
					num2 = 15;
					MyMSG = MyMSG + text + currentRow[num5];
					goto IL_00c3;
					IL_00d6:
					if (num5 <= num6)
					{
						goto IL_00b0;
					}
					goto IL_00dc;
					IL_00c3:
					num2 = 16;
					text = ",";
					goto IL_00cd;
					IL_000b:
					num2 = 2;
					result = true;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyMSG = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					num5 = 0;
					goto IL_0026;
					IL_0026:
					num2 = 6;
					if (Information.IsNothing(currentRow))
					{
						goto IL_0035;
					}
					goto IL_0048;
					IL_0035:
					num2 = 7;
					MyMSG = "Empty row.";
					goto IL_003e;
					IL_003e:
					num2 = 8;
					result = false;
					goto end_IL_0001_3;
					IL_0048:
					num2 = 10;
					if (Operators.CompareString(ObjectType, "4", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Strings.UCase(currentRow[0])), "!INLINE", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0081;
					IL_00cd:
					num2 = 17;
					num5 = checked(num5 + 1);
					goto IL_00d6;
					IL_0081:
					num2 = 12;
					if (currentRow.Length == MyCount)
					{
						goto end_IL_0001_3;
					}
					goto IL_0094;
					IL_0094:
					num2 = 13;
					MyMSG = "Invalid row: ";
					goto IL_009e;
					IL_009e:
					num2 = 14;
					num6 = Information.UBound(currentRow);
					num5 = 0;
					goto IL_00d6;
					end_IL_0001_2:
					break;
				}
				num2 = 19;
				result = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 348;
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

	public static bool Test_for_PPQ(string MyMode)
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
				case 141:
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
							goto IL_0029;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					MyMode = Strings.LCase(MyMode);
					goto IL_0019;
					IL_0019:
					num2 = 4;
					if (Globals_Renamed.g_FrmIdx <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0029;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_0029:
					num2 = 5;
					Interaction.MsgBox("Sorry, you cannot " + MyMode + " items while a Pre/Post Query is loaded", MsgBoxStyle.Exclamation, Strings.StrConv(MyMode, VbStrConv.ProperCase) + " Error");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 141;
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

	public static string check_disk_space(string MyPath, long MySpace)
	{
		string result = "";
		long num = 0L;
		long num2 = 0L;
		checked
		{
			try
			{
				string pathRoot = Path.GetPathRoot(MyPath);
				if (Strings.InStr(pathRoot, ":") != 0)
				{
					DriveInfo driveInfo = new DriveInfo(pathRoot);
					num = (long)Math.Round((double)driveInfo.AvailableFreeSpace / 1024.0 / 1024.0);
					num2 = (long)Math.Round((double)driveInfo.TotalSize / 1024.0 / 1024.0);
					if (num < MySpace)
					{
						result = "Cannot proceed as there is only " + Conversions.ToString(num) + " of " + Conversions.ToString(num2) + " MB free on drive " + driveInfo.Name + ". " + Conversions.ToString(MySpace) + " MB are needed. Do free up space or consider installing SQLPathFinder on another drive.";
					}
				}
				else
				{
					result = "Unable to determine free space for path: " + MyPath;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				result = "Unable to compute free space for path:   " + MyPath + ". " + ex2.Message;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	public static string Get_Version_Config()
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
				case 431:
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
							goto IL_0035;
						case 5:
							goto IL_0068;
						case 6:
							goto IL_009b;
						case 7:
							goto IL_00ae;
						case 8:
							goto IL_00c1;
						case 9:
							goto IL_0103;
						case 10:
							goto IL_0117;
						case 11:
							goto IL_012b;
						case 12:
							goto IL_013f;
						case 13:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0117:
					num2 = 10;
					text = text + "\r\n ARIES/XEUS Node= " + Globals_Renamed.MyARIESServer;
					goto IL_012b;
					IL_012b:
					num2 = 11;
					text = text + "\r\n OASYS/XEUS Node= " + Globals_Renamed.MyOASysServer;
					goto IL_013f;
					IL_0103:
					num2 = 9;
					text = text + "\r\n MARS/XEUS Node= " + Globals_Renamed.MyMARSServer;
					goto IL_0117;
					IL_013f:
					num2 = 12;
					text = text + "\r\n Other Node= " + Globals_Renamed.MyOtherServer;
					break;
					IL_000b:
					num2 = 2;
					text = "Your SQLPathFinder Version/Config: ";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = text + "\r\n GUI version= " + MyProject.Application.Info.Version.ToString();
					goto IL_0035;
					IL_0035:
					num2 = 4;
					text = text + "\r\n VA_EE version= " + General_Procedures.Get_Ini_Data("SQLPATHFINDER", "VA_EE", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
					goto IL_0068;
					IL_0068:
					num2 = 5;
					text = text + "\r\n PY_EE version= " + General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PY_EE", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
					goto IL_009b;
					IL_009b:
					num2 = 6;
					text = text + "\r\n Use Python Engine= " + Globals_Renamed.gUsePyEngine;
					goto IL_00ae;
					IL_00ae:
					num2 = 7;
					text = text + "\r\n Python 3 Build= " + Globals_Renamed.MyPy3Ver;
					goto IL_00c1;
					IL_00c1:
					num2 = 8;
					text = text + "\r\n R Build= " + General_Procedures.Get_Ini_Data("SQLPATHFINDER", "BUILD", "", 25, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\R\\R-Latest\\SQLPathFinder_R_Version.ini");
					goto IL_0103;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				text += "\r\n\r\n";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 431;
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

	public static bool Can_Use_Py_Engine(ref string MyErr, bool QuietMode, int MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		bool gCanUsePyEE = default(bool);
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
				case 292:
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
							goto IL_0027;
						case 5:
							goto IL_0033;
						case 6:
						case 7:
							goto IL_003e;
						case 8:
							goto IL_0077;
						case 10:
							goto IL_008b;
						case 9:
						case 11:
						case 12:
							goto IL_0095;
						case 13:
							goto IL_00ac;
						case 14:
						case 15:
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008b:
					num2 = 10;
					Globals_Renamed.gCanUsePyEE = true;
					goto IL_0095;
					IL_0077:
					num2 = 8;
					MyErr = text + "install SQLPathFinder's Python by choosing menu option Tools -> Update/Install Python -> Version 3.x";
					goto IL_0095;
					IL_00ac:
					num2 = 13;
					Interaction.MsgBox(MyErr, MsgBoxStyle.Exclamation, "Python EE");
					break;
					IL_0095:
					num2 = 12;
					if (Globals_Renamed.gCanUsePyEE || QuietMode)
					{
						break;
					}
					goto IL_00ac;
					IL_000b:
					num2 = 2;
					if (Globals_Renamed.gCanUsePyEE)
					{
						break;
					}
					goto IL_001e;
					IL_001e:
					num2 = 3;
					text = "In order to use the Python Extract Engine, please ";
					goto IL_0027;
					IL_0027:
					num2 = 4;
					if (MyMode == 1)
					{
						goto IL_0033;
					}
					goto IL_003e;
					IL_0033:
					num2 = 5;
					text = "You are configured to run using the Python Extract Engine. In order to use this Engine, please ";
					goto IL_003e;
					IL_003e:
					num2 = 7;
					if (Operators.CompareString(Strings.Trim(Globals_Renamed.gMyPyPath), "", TextCompare: false) == 0 || !MyProject.Computer.FileSystem.FileExists(Globals_Renamed.gMyPyPath))
					{
						goto IL_0077;
					}
					goto IL_008b;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				gCanUsePyEE = Globals_Renamed.gCanUsePyEE;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 292;
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
		return gCanUsePyEE;
	}

	public static string FNUsePyEngine()
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
				case 138:
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
							goto IL_0043;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					result = "Y";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "Y", TextCompare: false) != 0 && Operators.CompareString(Globals_Renamed.gUsePyEngineOVR, "N", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0043;
					IL_0043:
					num2 = 4;
					result = Globals_Renamed.gUsePyEngineOVR;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = Globals_Renamed.gUsePyEngine;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 138;
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

	public static bool Check_if_loop_macro(string MyData)
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
				case 284:
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
							goto IL_00dd;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					MyData = Strings.UCase(MyData);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					if (Operators.CompareString(Strings.UCase(Strings.Mid(MyData + "          ", 1, 10)), "U->IF-THEN", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(Strings.Mid(MyData + "           ", 1, 11)), "U->FOR-LOOP", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(Strings.Mid(MyData + "           ", 1, 11)), "U->RUN-LOOP", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(Strings.Mid(MyData + "              ", 1, 14)), "U->START-MACRO", TextCompare: false) != 0 && Operators.CompareString(Strings.UCase(Strings.Mid(MyData + "            ", 1, 12)), "U->SITE-LOOP", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00dd;
					IL_00dd:
					num2 = 4;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 284;
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

	public static void Set_Tree_Opt(string MyTag, ref string MyNode, ref short fSaveQuery)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		frmfilename frmfilename2 = default(frmfilename);
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
				case 315:
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
							goto IL_002d;
						case 6:
							goto IL_0048;
						case 7:
							goto IL_0063;
						case 8:
						case 9:
							goto IL_0071;
						case 10:
							goto IL_007a;
						case 11:
							goto IL_0084;
						case 12:
							goto IL_00a0;
						case 13:
							goto IL_00ad;
						case 14:
							goto IL_00c9;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a0:
					num2 = 12;
					Globals_Renamed.currvaluetmp = "No";
					goto IL_00ad;
					IL_00ad:
					num2 = 13;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00c9;
					IL_0084:
					num2 = 11;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) == 0)
					{
						goto IL_00a0;
					}
					goto IL_00ad;
					IL_00c9:
					num2 = 14;
					MyNode = General_Procedures.Set_Node_Value(MyNode, Globals_Renamed.currvaluetmp);
					break;
					IL_000b:
					num2 = 2;
					Globals_Renamed.currinputstrtmp = MyTag;
					goto IL_0013;
					IL_0013:
					num2 = 3;
					Globals_Renamed.currvaluetmp = MyNode;
					goto IL_001c;
					IL_001c:
					num2 = 4;
					Globals_Renamed.currvaluetmp = General_Procedures.Get_Node_Value(Globals_Renamed.currvaluetmp);
					goto IL_002d;
					IL_002d:
					num2 = 5;
					if (Operators.CompareString(Globals_Renamed.currinputstrtmp, "A-1", TextCompare: false) != 0)
					{
						goto IL_0048;
					}
					goto IL_0071;
					IL_0048:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "No", TextCompare: false) == 0)
					{
						goto IL_0063;
					}
					goto IL_0071;
					IL_0063:
					num2 = 7;
					Globals_Renamed.currvaluetmp = "";
					goto IL_0071;
					IL_0071:
					num2 = 9;
					frmfilename2 = new frmfilename();
					goto IL_007a;
					IL_007a:
					num2 = 10;
					frmfilename2.ShowDialog();
					goto IL_0084;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				fSaveQuery = 1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 315;
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

	public static string Test_Valid_jqw_Col(string MyCol)
	{
		string text = Strings.Trim(MyCol);
		if (Operators.CompareString(General_Procedures.Get_Ini_Data("Invalid Keywords", Strings.LCase(text), "yes", 10, Globals_Renamed.MySchemaDir + "\\keywords_jqw.ini"), "no", TextCompare: false) == 0)
		{
			text += "$";
		}
		return text;
	}

	public static string GenerateQAsExe(string MyMode, string MyFile)
	{
		int try0001_dispatch = -1;
		string result;
		string TmpMidas = default(string);
		int num2 = default(int);
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
					string text = "";
					int num3 = 0;
					TmpMidas = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					string text2 = "BuildForm - GenerateQAsExe";
					if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
					{
						TempSetSHSave("S", ref TmpMidas);
					}
					text = BuildSQL.Generate_SQL(1, "", IsPacked: true);
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						break;
					}
					Globals_Renamed.gWorkQuery = "";
					BuildSQL.RunQuery(text, 5, 1, -99, "");
					text = Globals_Renamed.gWorkQuery;
					Globals_Renamed.gWorkQuery = "";
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						if (SaveScriptExe(text, MyFile, 1) == 0)
						{
							result = "Failed to Pack/Save";
						}
					}
					else
					{
						result = "User Cancelled out of Query";
					}
					break;
				}
				case 292:
					num = -1;
					switch (num2)
					{
					case 2:
						result = "Failed to Save (" + Information.Err().Description + ")";
						Information.Err().Clear();
						break;
					default:
						goto end_IL_0001;
					}
					break;
				}
				if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
				{
					TempSetSHSave("R", ref TmpMidas);
				}
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 292;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void Pack_SH(string MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text4;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 583:
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
							goto IL_003d;
						case 8:
							goto IL_0054;
						case 9:
							goto IL_005d;
						case 10:
							goto IL_0071;
						case 11:
							goto IL_0082;
						case 13:
							goto IL_009e;
						case 14:
							goto IL_00b2;
						case 15:
							goto IL_00cb;
						case 16:
							goto IL_00d5;
						case 17:
							goto IL_0106;
						case 19:
							goto IL_012c;
						case 18:
						case 20:
						case 21:
							goto IL_014f;
						case 23:
							goto IL_019f;
						case 24:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 22:
						case 25:
						case 26:
						case 27:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_012c:
					num2 = 19;
					FileOpenSave("S", text, "zip", "Create Query as Python ZIP to run" + text2, text3);
					goto IL_014f;
					IL_0106:
					num2 = 17;
					FileOpenSave("S", text, "exe", "Create Query as Executable" + text2, text3);
					goto IL_014f;
					IL_019f:
					num2 = 23;
					text = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					break;
					IL_014f:
					num2 = 21;
					if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "", TextCompare: false) == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_019f;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text3 = "";
					goto IL_001c;
					IL_001c:
					num2 = 4;
					text2 = "";
					goto IL_0025;
					IL_0025:
					num2 = 5;
					text4 = "";
					goto IL_002e;
					IL_002e:
					num2 = 6;
					MyMode = Strings.Trim(Strings.UCase(MyMode));
					goto IL_003d;
					IL_003d:
					num2 = 7;
					if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
					{
						goto IL_0054;
					}
					goto IL_005d;
					IL_0054:
					num2 = 8;
					text2 = " for ScriptHost.";
					goto IL_005d;
					IL_005d:
					num2 = 9;
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_0071;
					IL_0071:
					num2 = 10;
					if (Globals_Renamed.g_FrmIdx == 1)
					{
						goto IL_0082;
					}
					goto IL_009e;
					IL_0082:
					num2 = 11;
					Interaction.MsgBox("You cannot save Pre/Post queries as executables", MsgBoxStyle.Critical, "Error");
					goto end_IL_0001_3;
					IL_009e:
					num2 = 13;
					text3 = Strings.Trim(Interaction.Environ("USERPROFILE"));
					goto IL_00b2;
					IL_00b2:
					num2 = 14;
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto IL_00cb;
					}
					goto IL_00d5;
					IL_00cb:
					num2 = 15;
					text3 = Globals_Renamed.MyPCDir;
					goto IL_00d5;
					IL_00d5:
					num2 = 16;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						goto IL_0106;
					}
					goto IL_012c;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				text4 = GenerateQAsExe(MyMode, text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 583;
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

	public static void Save_SPFSQL(string MyOpt)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
					string text = "BuildForm - Save_SPFSQL";
					string iniFile = "";
					string text2 = "";
					string text3 = "";
					int num3 = 0;
					string text4 = "";
					if (Globals_Renamed.Design_Mode != 2)
					{
						goto end_IL_0001;
					}
					if (Globals_Renamed.g_FrmIdx == 1)
					{
						Interaction.MsgBox("You cannot save Pre/Post queries as spfsql files", MsgBoxStyle.Critical, "Error");
						goto end_IL_0001;
					}
					text2 = Strings.Trim(Interaction.Environ("USERPROFILE"));
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						text2 = Globals_Renamed.MyPCDir;
					}
					FileOpenSave("S", iniFile, "spfsql", "Save Query as SPFSQL File", text2);
					if ((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "CANCEL", TextCompare: false) == 0) | (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "", TextCompare: false) == 0))
					{
						goto end_IL_0001;
					}
					iniFile = MyProject.Forms.FrmMain.CMDialog1Save.FileName;
					text3 = BuildSQL.Generate_SQL(1, "", IsPacked: true);
					if (Operators.CompareString(text3, "", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					Globals_Renamed.gWorkQuery = "";
					BuildSQL.RunQuery(text3, 5, 1, -99, "");
					text3 = Globals_Renamed.gWorkQuery;
					Globals_Renamed.gWorkQuery = "";
					if (Operators.CompareString(text3, "", TextCompare: false) != 0 && General_Procedures.Save_SQL_Query(text3, iniFile) != 0 && Operators.CompareString(Strings.UCase(Strings.Trim(MyOpt)), "S", TextCompare: false) == 0)
					{
						FrmDTREdit frmDTREdit = new FrmDTREdit();
						text4 = General_Procedures.Set_ORCL_Path();
						if (Operators.CompareString(Strings.Mid(text4 + "  ", 1, 2), "&&", TextCompare: false) == 0)
						{
							text4 = Strings.Mid(text4 + "  ", 3);
						}
						text4 = text4 + "\r\nSET tns_admin=" + Globals_Renamed.MySchemaDir;
						text3 = "REM ====================================\r\n";
						text3 += "REM Sample Query Execution Commands are:\r\n";
						text3 = text3 + "REM ====================================\r\n\r\n" + text4 + "\r\n";
						text3 = text3 + "\"" + Globals_Renamed.gMyPyPath + "\" -s -u \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFSQL3.py\" /SPFSQL=\"" + iniFile + "\" /SPFINSTANCE=" + Globals_Renamed.gSPFCache;
						frmDTREdit.Text1.Text = text3 + "\r\n";
						frmDTREdit.cmbSQLVA.Text = "WRITE";
						frmDTREdit.cmbSQLVA.Enabled = false;
						frmDTREdit.out_odbc = "--";
						frmDTREdit.Text1.SelectionStart = 0;
						frmDTREdit.Text1.SelectionLength = 0;
						frmDTREdit.KeepSQL = false;
						frmDTREdit.fSaveSQL = false;
						frmDTREdit.ShowDialog();
						frmDTREdit.Dispose();
					}
					goto end_IL_0001_2;
				}
				case 860:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error saving Query  (" + Information.Err().Description + ")", MsgBoxStyle.Critical, "Save Error");
						Information.Err().Clear();
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_0392;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 860;
				continue;
			}
			break;
			IL_0392:
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

	public static string Get_Globals_in_File(string MyFile, ref Globals_Renamed.gVar_Type[] MylArr, ref int lNoArr, string MyMode, int MaxColIdx = 2)
	{
		string text = "";
		string text2 = "";
		string MyData = "";
		int num = 0;
		string text3 = "";
		Array.Clear(MylArr, 0, MylArr.Length);
		lNoArr = -1;
		checked
		{
			if (LikeOperator.LikeString(Strings.LCase(MyFile), "*<<<cl_*", CompareMethod.Binary))
			{
				int g_VarNo = Globals_Renamed.g_VarNo;
				for (num = 0; num <= g_VarNo; num++)
				{
					MyFile = Strings.Replace(MyFile, "<<<" + Globals_Renamed.g_GVars[0, num] + ">>>", Globals_Renamed.g_GVars[1, num], 1, -1, CompareMethod.Text);
				}
			}
			text = GetFileDLM(MyFile);
			if (Operators.CompareString(MyFile, "", TextCompare: false) == 0)
			{
				text2 = "No file to return";
			}
			else if (LikeOperator.LikeString(MyFile.ToLower(), "http*", CompareMethod.Binary))
			{
				text2 = General_Procedures.Get_Web(MyFile, ref MyData);
				if (Operators.CompareString(text2, "", TextCompare: false) == 0 && Operators.CompareString(MyData, "", TextCompare: false) == 0)
				{
					text2 = "No File or data was found at web address";
				}
			}
			else
			{
				text2 = General_Procedures.GetFileContents(MyFile, ref MyData);
			}
			if (Operators.CompareString(text2, "", TextCompare: false) == 0)
			{
				string[] array = Strings.Split(MyData, "\r\n");
				int num2 = Information.UBound(array);
				string[] array2;
				for (num = 1; num <= num2; num++)
				{
					text3 = Strings.Trim(array[num]);
					array2 = General_Procedures.SplitQuoted(text3, Conversions.ToChar(text));
					if (Information.UBound(array2) == MaxColIdx && Operators.CompareString(Strings.Trim(array2[0]), "", TextCompare: false) != 0 && lNoArr < 100)
					{
						lNoArr++;
						MylArr[lNoArr].GMode = MyMode;
						MylArr[lNoArr].Variable = Strings.Trim(General_Procedures.TrimQuoted(array2[0]));
						MylArr[lNoArr].Value = General_Procedures.TrimQuoted(array2[1]);
						if (MaxColIdx == 1)
						{
							MylArr[lNoArr].SHValue = "";
						}
						else
						{
							MylArr[lNoArr].SHValue = General_Procedures.TrimQuoted(array2[2]);
						}
					}
				}
				array = null;
				array2 = null;
			}
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				text2 = "Error Retrieving Variables. " + text2;
			}
			return text2;
		}
	}

	public static string Do_View_Dict(string MyMode, string MyNode = "", string MyData = "")
	{
		int num = 0;
		bool flag = false;
		string result = "";
		MyMode = Strings.Trim(Strings.UCase(MyMode));
		MyNode = Strings.Trim(Strings.LCase(MyNode));
		MyData = Strings.Trim(Strings.LCase(MyData));
		checked
		{
			switch (MyMode)
			{
			case "A":
			{
				if (Strings.InStr(MyNode, "[site specific]") != 0)
				{
					num = Strings.InStrRev(MyNode, "\\");
					if (num != 0)
					{
						MyNode = Strings.Trim(Strings.Mid(MyNode, num + 1));
					}
				}
				if (Globals_Renamed.gViewCount >= 100)
				{
					break;
				}
				int num3 = Globals_Renamed.gViewCount - 1;
				for (int i = 0; i <= num3; i++)
				{
					if (LikeOperator.LikeString(Globals_Renamed.gView_Dict[i], MyNode + ":::*", CompareMethod.Binary))
					{
						flag = true;
						if (Strings.InStr(MyData, "*") == 0 && Operators.CompareString(MyData, "", TextCompare: false) != 0)
						{
							MyData = "*" + MyData + "*";
						}
						Globals_Renamed.gView_Dict[i] = MyNode + ":::" + MyData;
						break;
					}
				}
				if (!flag)
				{
					if (Strings.InStr(MyData, "*") == 0 && Operators.CompareString(MyData, "", TextCompare: false) != 0)
					{
						MyData = "*" + MyData + "*";
					}
					Globals_Renamed.gView_Dict[Globals_Renamed.gViewCount] = MyNode + ":::" + MyData;
					Globals_Renamed.gViewCount++;
				}
				break;
			}
			case "C":
			{
				int i = 0;
				do
				{
					Globals_Renamed.gView_Dict[i] = "";
					i++;
				}
				while (i <= 99);
				break;
			}
			case "G":
			{
				if (Operators.CompareString(MyNode, "", TextCompare: false) == 0)
				{
					break;
				}
				if (Strings.InStr(MyNode, ",") != 0)
				{
					num = Strings.InStr(MyNode, ",");
					if (num > 1)
					{
						MyNode = Strings.Mid(MyNode, 1, num - 1);
					}
				}
				if (Strings.InStr(MyNode, "[site specific]") != 0)
				{
					num = Strings.InStrRev(MyNode, "\\");
					if (num != 0)
					{
						MyNode = Strings.Trim(Strings.Mid(MyNode, num + 1));
					}
				}
				int num2 = Globals_Renamed.gViewCount - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (LikeOperator.LikeString(Globals_Renamed.gView_Dict[i], MyNode + ":::*", CompareMethod.Binary))
					{
						result = Globals_Renamed.gView_Dict[i];
						num = Strings.InStr(result, ":::");
						result = Strings.Trim(Strings.Mid(result + " ", num + 3));
						break;
					}
				}
				break;
			}
			}
			return result;
		}
	}

	public static void Show_Temp_Table_Results(string ll_MajorAlias)
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
					errsource = "BuildForm - Show_Temp_Table_Results";
					string text = "";
					string text2 = "";
					text = General_Procedures.Get_Node_Value(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx].Find_Node_Value("T", "DBTYPE:" + ll_MajorAlias));
					text2 = BuildSQL.Set_TT_Name(ll_MajorAlias, text);
					text2 = Replace_Globals(text2, 1);
					if (Strings.InStr(text2, "\\") == 0)
					{
						text2 = Strings.Trim(Globals_Renamed.MyPCDir + text2);
					}
					if (MyProject.Computer.FileSystem.FileExists(text2))
					{
						Invoke_Txt("spfgrid", 2, text2);
					}
					else
					{
						Interaction.MsgBox(General_Procedures.Get_UI("errtt"), MsgBoxStyle.Exclamation, "Not Found");
					}
					goto end_IL_0001;
				}
				case 245:
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
				try0001_dispatch = 245;
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

	public static string Handle_Squiggly(string MyInput)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		string result = default(string);
		string text3 = default(string);
		int num7 = default(int);
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
					case 843:
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
								goto IL_0022;
							case 7:
								goto IL_0027;
							case 8:
								goto IL_0030;
							case 9:
								goto IL_003a;
							case 10:
								goto IL_0044;
							case 11:
								goto IL_0053;
							case 12:
								goto IL_0061;
							case 13:
								goto IL_0069;
							case 15:
								goto IL_00c9;
							case 16:
								goto IL_00d7;
							case 18:
								goto IL_00e1;
							case 19:
								goto IL_010d;
							case 20:
								goto IL_012e;
							case 22:
								goto IL_013b;
							case 26:
								goto IL_0146;
							case 27:
								goto IL_0154;
							case 29:
								goto IL_0161;
							case 30:
								goto IL_016f;
							case 32:
								goto IL_017c;
							case 33:
								goto IL_018a;
							case 35:
								goto IL_0196;
							case 36:
								goto IL_01a4;
							case 14:
							case 17:
							case 21:
							case 23:
							case 24:
							case 25:
							case 28:
							case 31:
							case 34:
							case 37:
							case 38:
								goto IL_01b0;
							case 39:
								goto IL_01be;
							case 40:
								goto IL_01d0;
							case 41:
								goto IL_01de;
							case 42:
								goto IL_01e7;
							case 44:
								goto IL_01fe;
							case 45:
								goto IL_020c;
							case 46:
								goto IL_0215;
							case 48:
								goto IL_022d;
							case 49:
								goto IL_0246;
							case 50:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 43:
							case 47:
							case 51:
							case 52:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_013b:
						num2 = 22;
						num5 = 0;
						goto IL_01b0;
						IL_01b0:
						num2 = 38;
						text += text2;
						goto IL_01be;
						IL_012e:
						num2 = 20;
						num6++;
						goto IL_01b0;
						IL_01be:
						num2 = 39;
						num6++;
						goto IL_01c7;
						IL_000b:
						num2 = 2;
						result = MyInput;
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
						num5 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						num7 = 0;
						goto IL_0027;
						IL_0027:
						num2 = 7;
						text2 = "";
						goto IL_0030;
						IL_0030:
						num2 = 8;
						num8 = Strings.Len(MyInput);
						goto IL_003a;
						IL_003a:
						num2 = 9;
						text = "";
						goto IL_0044;
						IL_0044:
						num2 = 10;
						num9 = num8;
						num6 = 1;
						goto IL_01c7;
						IL_01c7:
						if (num6 <= num9)
						{
							goto IL_0053;
						}
						goto IL_01d0;
						IL_01d0:
						num2 = 40;
						if (num5 == 1)
						{
							goto IL_01de;
						}
						goto IL_01fe;
						IL_01de:
						num2 = 41;
						result = "ERROR";
						goto IL_01e7;
						IL_01e7:
						num2 = 42;
						Interaction.MsgBox("There is a mismatch in the number of single quotes in the computed expression. Please Correct.", MsgBoxStyle.OkOnly, "Mismatch");
						goto end_IL_0001_3;
						IL_01fe:
						num2 = 44;
						if (num7 != 0)
						{
							goto IL_020c;
						}
						goto IL_022d;
						IL_020c:
						num2 = 45;
						result = "ERROR";
						goto IL_0215;
						IL_0215:
						num2 = 46;
						Interaction.MsgBox("There is a mismatch in open and close parentheses in the computed expression. Please Correct.", MsgBoxStyle.OkOnly, "Mismatch");
						goto end_IL_0001_3;
						IL_022d:
						num2 = 48;
						text = Strings.Replace(text, "\\{", "<~sqos~>", 1, -1, CompareMethod.Text);
						goto IL_0246;
						IL_0246:
						num2 = 49;
						text = Strings.Replace(text, "\\}", "<~sqcs~>", 1, -1, CompareMethod.Text);
						break;
						IL_0053:
						num2 = 11;
						text3 = Strings.Mid(MyInput, num6, 1);
						goto IL_0061;
						IL_0061:
						num2 = 12;
						text2 = text3;
						goto IL_0069;
						IL_0069:
						num2 = 13;
						switch (text3)
						{
						case "'":
							break;
						case "{":
							goto IL_0146;
						case "}":
							goto IL_0161;
						case "(":
							goto IL_017c;
						case ")":
							goto IL_0196;
						default:
							goto IL_01b0;
						}
						goto IL_00c9;
						IL_0196:
						num2 = 35;
						if (num5 == 0)
						{
							goto IL_01a4;
						}
						goto IL_01b0;
						IL_01a4:
						num2 = 36;
						num7--;
						goto IL_01b0;
						IL_017c:
						num2 = 32;
						if (num5 == 0)
						{
							goto IL_018a;
						}
						goto IL_01b0;
						IL_018a:
						num2 = 33;
						num7++;
						goto IL_01b0;
						IL_0161:
						num2 = 29;
						if (num5 == 1)
						{
							goto IL_016f;
						}
						goto IL_01b0;
						IL_016f:
						num2 = 30;
						text2 = "<~sqc~>";
						goto IL_01b0;
						IL_0146:
						num2 = 26;
						if (num5 == 1)
						{
							goto IL_0154;
						}
						goto IL_01b0;
						IL_0154:
						num2 = 27;
						text2 = "<~sqo~>";
						goto IL_01b0;
						IL_00c9:
						num2 = 15;
						if (num5 == 0)
						{
							goto IL_00d7;
						}
						goto IL_00e1;
						IL_00d7:
						num2 = 16;
						num5 = 1;
						goto IL_01b0;
						IL_00e1:
						num2 = 18;
						if (Operators.CompareString(Strings.Mid(MyInput + " ", num6 + 1, 1), "'", TextCompare: false) == 0)
						{
							goto IL_010d;
						}
						goto IL_013b;
						IL_010d:
						num2 = 19;
						text2 += Strings.Mid(MyInput + " ", num6 + 1, 1);
						goto IL_012e;
						end_IL_0001_2:
						break;
					}
					num2 = 50;
					result = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 843;
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

	public static string Repl_Squiggly(string MyStr, int MyMode)
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
				case 235:
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
							goto IL_0025;
						case 5:
							goto IL_003b;
						case 6:
							goto IL_0047;
						case 7:
							goto IL_005d;
						case 9:
							goto IL_0077;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0047:
					num2 = 6;
					text = Strings.Replace(text, "<~sqos~>", "\\{", 1, -1, CompareMethod.Text);
					goto IL_005d;
					IL_005d:
					num2 = 7;
					text = Strings.Replace(text, "<~sqcs~>", "\\}", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_003b:
					num2 = 5;
					if (MyMode == 0)
					{
						goto IL_0047;
					}
					goto IL_0077;
					IL_0077:
					num2 = 9;
					text = Strings.Replace(text, "<~sqos~>", "{", 1, -1, CompareMethod.Text);
					break;
					IL_000b:
					num2 = 2;
					text = MyStr;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					text = Strings.Replace(text, "<~sqo~>", "{", 1, -1, CompareMethod.Text);
					goto IL_0025;
					IL_0025:
					num2 = 4;
					text = Strings.Replace(text, "<~sqc~>", "}", 1, -1, CompareMethod.Text);
					goto IL_003b;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				text = Strings.Replace(text, "<~sqcs~>", "}", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 235;
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

	public static bool IsColXPattern(string MyColumn)
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
				case 91:
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
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (Strings.InStr(Strings.LCase(MyColumn), "column-transform->[[") == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 91;
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

	public static void DoColXPattern(ref string MyColumn, ref string MyHeader, string ll_DatabaseType, string MyAlias)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		string text4 = default(string);
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
					case 1041:
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
								goto IL_002b;
							case 8:
								goto IL_0034;
							case 9:
								goto IL_003d;
							case 10:
								goto IL_0054;
							case 11:
								goto IL_0067;
							case 12:
								goto IL_0083;
							case 13:
								goto IL_0091;
							case 14:
							case 15:
								goto IL_00a3;
							case 16:
								goto IL_00b9;
							case 17:
								goto IL_00ca;
							case 18:
								goto IL_00dc;
							case 19:
								goto IL_00ed;
							case 20:
								goto IL_0103;
							case 21:
								goto IL_0122;
							case 22:
								goto IL_013b;
							case 23:
								goto IL_0141;
							case 24:
								goto IL_015c;
							case 25:
								goto IL_0175;
							case 26:
								goto IL_01aa;
							case 28:
								goto IL_01dd;
							case 31:
								goto IL_01f0;
							case 32:
								goto IL_021c;
							case 33:
								goto IL_0235;
							case 35:
								goto IL_029f;
							case 34:
							case 36:
							case 37:
								goto IL_0306;
							case 27:
							case 29:
							case 30:
							case 38:
							case 39:
								goto IL_0311;
							case 40:
								goto IL_0327;
							case 41:
							case 42:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 43:
							case 44:
							case 45:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0235:
						num2 = 33;
						text = text + "Column-Pattern->[[N<;>" + text2 + "<;>" + Globals_Renamed.gWinuser + "_" + text2 + "_" + Globals_Renamed.gSPFCache + "<dot>tab<;>Regex<;>" + text3 + "#<;>|<>|]]";
						goto IL_0306;
						IL_029f:
						num2 = 35;
						text = text + "Column-Pattern->[[N<;>" + text2 + "<;>" + Globals_Renamed.gWinuser + "_" + text2 + "_" + Globals_Renamed.gSPFCache + "<dot>tab<;>Starts With<;>" + text3 + "#<;>|<>|]]";
						goto IL_0306;
						IL_021c:
						num2 = 32;
						if (Operators.CompareString(text3, "^u\\d[0-9_u]*", TextCompare: false) == 0)
						{
							goto IL_0235;
						}
						goto IL_029f;
						IL_0306:
						num2 = 37;
						MyHeader = "";
						goto IL_0311;
						IL_000b:
						num2 = 2;
						text = "";
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
						num7 = 0;
						goto IL_0022;
						IL_0022:
						num2 = 6;
						text4 = "";
						goto IL_002b;
						IL_002b:
						num2 = 7;
						text3 = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						text2 = "";
						goto IL_003d;
						IL_003d:
						num2 = 9;
						num5 = Strings.InStr(Strings.LCase(MyColumn), "column-transform->[[");
						goto IL_0054;
						IL_0054:
						num2 = 10;
						num6 = Strings.InStrRev(MyColumn, "]]");
						goto IL_0067;
						IL_0067:
						num2 = 11;
						if (num5 == 0 || num6 <= num5 + 21)
						{
							goto end_IL_0001_3;
						}
						goto IL_0083;
						IL_0311:
						num2 = 39;
						if (Strings.Len(MyColumn) <= num6 + 1)
						{
							break;
						}
						goto IL_0327;
						IL_0083:
						num2 = 12;
						if (num5 > 1)
						{
							goto IL_0091;
						}
						goto IL_00a3;
						IL_0091:
						num2 = 13;
						text = Strings.Mid(MyColumn, 1, num5 - 1);
						goto IL_00a3;
						IL_00a3:
						num2 = 15;
						text4 = Strings.Trim(Strings.Mid(MyColumn, 1, num6 - 1));
						goto IL_00b9;
						IL_00b9:
						num2 = 16;
						text4 = Strings.Mid(text4, num5 + 20);
						goto IL_00ca;
						IL_00ca:
						num2 = 17;
						num7 = Strings.InStr(text4, "<;>");
						goto IL_00dc;
						IL_00dc:
						num2 = 18;
						if (num7 <= 1)
						{
							goto end_IL_0001_3;
						}
						goto IL_00ed;
						IL_00ed:
						num2 = 19;
						text3 = Strings.Trim(Strings.Mid(text4, 1, num7 - 1));
						goto IL_0103;
						IL_0103:
						num2 = 20;
						text2 = Strings.Trim(Strings.Mid(text4 + " ", num7 + 3));
						goto IL_0122;
						IL_0122:
						num2 = 21;
						if (Operators.CompareString(text2, "<ai>", TextCompare: false) == 0)
						{
							goto IL_013b;
						}
						goto IL_0141;
						IL_013b:
						num2 = 22;
						text2 = MyAlias;
						goto IL_0141;
						IL_0141:
						num2 = 23;
						if (Operators.CompareString(ll_DatabaseType, "Mongo", TextCompare: false) == 0)
						{
							goto IL_015c;
						}
						goto IL_01f0;
						IL_015c:
						num2 = 24;
						if (Operators.CompareString(text3, "^u\\d[0-9_u]*", TextCompare: false) == 0)
						{
							goto IL_0175;
						}
						goto IL_01dd;
						IL_0175:
						num2 = 25;
						text = text + "Column-Pattern->[[M<;>" + text2 + "<;><Collection=ATM_KS_UNIT_COLUMNS/>@MONGO-KS1@:<:><rni><;>Regex<;>" + text3 + "$<;>|<>|<;>\\#]]";
						goto IL_01aa;
						IL_01aa:
						num2 = 26;
						text = Strings.Replace(text, "<:><rni>", "<:>" + DateTime.Now.ToString("yyMMddHHmmssfffffff"), 1, -1, CompareMethod.Text);
						goto IL_0311;
						IL_01dd:
						num2 = 28;
						text += text3;
						goto IL_0311;
						IL_01f0:
						num2 = 31;
						if (Operators.CompareString(ll_DatabaseType, "SQLite", TextCompare: false) == 0 || Operators.CompareString(ll_DatabaseType, "DUCKDB", TextCompare: false) == 0)
						{
							goto IL_021c;
						}
						goto IL_0311;
						IL_0327:
						num2 = 40;
						text += Strings.Mid(MyColumn, num6 + 2);
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 42;
					MyColumn = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1041;
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

	public static bool IsColPattern(string MyStr, string MyMode = "ALL")
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
				case 347:
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
						case 5:
							goto IL_0064;
						case 6:
							goto IL_007d;
						case 8:
							goto IL_0084;
						case 9:
							goto IL_009d;
						case 11:
							goto IL_00a5;
						case 12:
							goto IL_00d8;
						case 14:
							goto IL_00e0;
						case 15:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 10:
						case 13:
						case 16:
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0084:
					num2 = 8;
					if (!LikeOperator.LikeString(Strings.LCase(MyStr), "*column-pattern->*@mongo-ks1@*<;>not *<;>*<;>*]]", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_009d;
					IL_009d:
					num2 = 9;
					result = true;
					goto end_IL_0001_3;
					IL_00d8:
					num2 = 12;
					result = true;
					goto end_IL_0001_3;
					IL_0064:
					num2 = 5;
					if (!LikeOperator.LikeString(Strings.LCase(MyStr), "*column-pattern->*", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_007d;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					switch (Strings.UCase(Strings.Trim(MyMode)))
					{
					case "ALL":
						break;
					case "MDBNOT":
						goto IL_0084;
					case "MDB":
						goto IL_00a5;
					case "FUNCFILE":
						goto IL_00e0;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0064;
					IL_00e0:
					num2 = 14;
					if (!LikeOperator.LikeString(Strings.LCase(MyStr), "*column-pattern->*<;>function file<;>*", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_00a5:
					num2 = 11;
					if (!LikeOperator.LikeString(Strings.LCase(MyStr), "*column-pattern->*@mongo-ks1@*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.LCase(MyStr), "*@mongo-ks1@*<;>not *<;>*<;>]]", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_00d8;
					IL_007d:
					num2 = 6;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 15;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 347;
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

	public static bool Test_Search_Pat(string MyOpr, string MyData)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
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
							goto IL_0024;
						case 6:
							goto IL_0045;
						case 7:
							goto IL_0052;
						case 8:
							goto IL_0056;
						case 10:
							goto IL_006d;
						case 11:
							goto IL_0088;
						case 12:
							goto IL_008d;
						case 15:
							goto IL_00a7;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 13:
						case 14:
						case 17:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a7:
					num2 = 15;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0088:
					num2 = 11;
					result = false;
					goto IL_008d;
					IL_006d:
					num2 = 10;
					if (num5 != 1 && num5 != Strings.Len(MyData))
					{
						goto end_IL_0001_3;
					}
					goto IL_0088;
					IL_008d:
					num2 = 12;
					Interaction.MsgBox("When using Pattern Operator \"Starts/Ends With (%)\", % should not occur at the start or end of the search string. Consider using operators \"Starts With\" or \"Ends With\" instead. Sample valid value is: lot%_time", MsgBoxStyle.Exclamation, "Invalid Pattern");
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					result = true;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num5 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num5 = Strings.InStr(MyData, "%");
					goto IL_0024;
					IL_0024:
					num2 = 5;
					if (Operators.CompareString(Strings.UCase(MyOpr), "Starts/Ends With (%)".ToUpper(), TextCompare: false) == 0)
					{
						goto IL_0045;
					}
					goto IL_00a7;
					IL_0045:
					num2 = 6;
					if (num5 == 0)
					{
						goto IL_0052;
					}
					goto IL_006d;
					IL_0052:
					num2 = 7;
					result = false;
					goto IL_0056;
					IL_0056:
					num2 = 8;
					Interaction.MsgBox("When using Pattern Operator \"Starts/Ends With (%)\", separate the start and end value with %. E.g., lot%_time", MsgBoxStyle.Exclamation, "Invalid Pattern");
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				Interaction.MsgBox("When using Pattern Operator \"" + MyOpr + "\", a % is interpreted literally.", MsgBoxStyle.Information, "% Found");
				break;
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

	public static void Set_Btn_Img(ref Button MyButton, string MyKey)
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
				case 81:
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
					MyButton.ImageList = MyProject.Forms.FrmMain.gImageList;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				MyButton.ImageKey = MyKey;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 81;
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

	public static string Strip_Add_MyPCDir(string MyMode, string MyData, string MyAnyDir = "")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string left = default(string);
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
					case 801:
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
								goto IL_0023;
							case 6:
								goto IL_002d;
							case 7:
								goto IL_003c;
							case 8:
								goto IL_0040;
							case 10:
								goto IL_006d;
							case 11:
								goto IL_0080;
							case 13:
								goto IL_00d1;
							case 14:
								goto IL_00e2;
							case 15:
								goto IL_00fa;
							case 16:
								goto IL_0118;
							case 18:
								goto IL_0129;
							case 19:
								goto IL_0147;
							case 23:
								goto IL_016d;
							case 24:
								goto IL_0197;
							case 27:
								goto IL_01ae;
							case 28:
								goto IL_01d8;
							case 30:
								goto IL_01f2;
							case 31:
								goto IL_0200;
							case 34:
								goto IL_0213;
							case 35:
								goto IL_0221;
							case 36:
								goto IL_0239;
							case 37:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 9:
							case 12:
							case 17:
							case 20:
							case 21:
							case 22:
							case 25:
							case 26:
							case 29:
							case 32:
							case 33:
							case 38:
							case 39:
							case 40:
							case 41:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00d1:
						num2 = 13;
						if (num5 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_00e2;
						IL_00e2:
						num2 = 14;
						left = Strings.Trim(Strings.UCase(Strings.Mid(MyData, 1, num5)));
						goto IL_00fa;
						IL_0197:
						num2 = 24;
						result = Globals_Renamed.MyPCDir + MyData;
						goto end_IL_0001_3;
						IL_00fa:
						num2 = 15;
						if (Operators.CompareString(left, Globals_Renamed.MyPCDir.ToUpper(), TextCompare: false) == 0)
						{
							goto IL_0118;
						}
						goto IL_0129;
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
						MyData = Strings.Trim(MyData);
						goto IL_0023;
						IL_0023:
						num2 = 5;
						MyAnyDir = Strings.Trim(MyAnyDir);
						goto IL_002d;
						IL_002d:
						num2 = 6;
						MyMode = Strings.Trim(Strings.UCase(MyMode));
						goto IL_003c;
						IL_003c:
						num2 = 7;
						result = MyData;
						goto IL_0040;
						IL_0040:
						num2 = 8;
						if (Operators.CompareString(MyMode, "S2", TextCompare: false) == 0 && Operators.CompareString(MyAnyDir, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_006d;
						IL_0118:
						num2 = 16;
						result = Strings.Mid(MyData, num5 + 1);
						goto end_IL_0001_3;
						IL_006d:
						num2 = 10;
						num5 = Strings.InStrRev(MyData, "\\", -1, CompareMethod.Text);
						goto IL_0080;
						IL_0080:
						num2 = 11;
						switch (MyMode)
						{
						case "S":
							break;
						case "A":
							goto IL_016d;
						case "A2":
							goto IL_01ae;
						case "S2":
							goto IL_0213;
						default:
							goto end_IL_0001_3;
						}
						goto IL_00d1;
						IL_0213:
						num2 = 34;
						if (num5 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0221;
						IL_0221:
						num2 = 35;
						left = Strings.Trim(Strings.UCase(Strings.Mid(MyData, 1, num5)));
						goto IL_0239;
						IL_0239:
						num2 = 36;
						if (Operators.CompareString(left, MyAnyDir.ToUpper(), TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_01ae:
						num2 = 27;
						if (num5 == 0 && Strings.InStr(Conversions.ToInteger(MyData), "<<<spf-query-dir>>>", Conversions.ToString(-1), CompareMethod.Text) != 0)
						{
							goto IL_01d8;
						}
						goto IL_01f2;
						IL_0129:
						num2 = 18;
						if (Operators.CompareString(left, Globals_Renamed.gQueryDir.ToUpper(), TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0147;
						IL_01d8:
						num2 = 28;
						result = Strings.Replace(MyData, "<<<spf-query-dir>>>", Globals_Renamed.gQueryDir, 1, -1, CompareMethod.Text);
						goto end_IL_0001_3;
						IL_01f2:
						num2 = 30;
						if (num5 != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0200;
						IL_0200:
						num2 = 31;
						result = Globals_Renamed.MyPCDir + MyData;
						goto end_IL_0001_3;
						IL_016d:
						num2 = 23;
						if (num5 != 0 || Strings.InStr(Conversions.ToInteger(MyData), "<<<spf-query-dir>>>", Conversions.ToString(-1), CompareMethod.Text) != 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0197;
						IL_0147:
						num2 = 19;
						result = "<<<spf-query-dir>>>" + Strings.Trim(Strings.Mid(MyData, num5 + 1));
						goto end_IL_0001_3;
						end_IL_0001_2:
						break;
					}
					num2 = 37;
					result = Strings.Mid(MyData, num5 + 1);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 801;
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

	public static string Replace_Amp_Carat(string MySQLHelp, string MyMode)
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
				case 287:
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
							goto IL_003f;
						case 5:
							goto IL_0056;
						case 6:
							goto IL_006d;
						case 8:
							goto IL_0087;
						case 9:
							goto IL_009e;
						case 10:
							goto IL_00b6;
						case 3:
						case 7:
						case 11:
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00b6:
					num2 = 10;
					MySQLHelp = Strings.Replace(MySQLHelp, "<spf-c>", "^", 1, -1, CompareMethod.Text);
					break;
					IL_003f:
					num2 = 4;
					MySQLHelp = Strings.Replace(MySQLHelp, "&", "<spf-a>", 1, -1, CompareMethod.Text);
					goto IL_0056;
					IL_009e:
					num2 = 9;
					MySQLHelp = Strings.Replace(MySQLHelp, "<spf-at>", "@", 1, -1, CompareMethod.Text);
					goto IL_00b6;
					IL_0056:
					num2 = 5;
					MySQLHelp = Strings.Replace(MySQLHelp, "@", "<spf-at>", 1, -1, CompareMethod.Text);
					goto IL_006d;
					IL_000c:
					num2 = 2;
					left = Strings.Trim(Strings.UCase(MyMode));
					if (Operators.CompareString(left, "E", TextCompare: false) == 0)
					{
						goto IL_003f;
					}
					if (Operators.CompareString(left, "D", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0087;
					IL_006d:
					num2 = 6;
					MySQLHelp = Strings.Replace(MySQLHelp, "^", "<spf-c>", 1, -1, CompareMethod.Text);
					break;
					IL_0087:
					num2 = 8;
					MySQLHelp = Strings.Replace(MySQLHelp, "<spf-a>", "&", 1, -1, CompareMethod.Text);
					goto IL_009e;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = MySQLHelp;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 287;
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

	public static int DoRegistry(string MyMode, string MyData)
	{
		int result = 0;
		try
		{
			string left = Strings.Trim(Strings.UCase(MyMode));
			if (Operators.CompareString(left, "W", TextCompare: false) == 0)
			{
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("software\\SQLPathFinder\\WorkFolder").SetValue("Path", MyData, RegistryValueKind.String);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Error accessing computer registry\r\n" + ex2.Message + ". Contact support if you need assistance.", MsgBoxStyle.Critical, "Access Issue");
			result = 1;
			ProjectData.ClearProjectError();
		}
		finally
		{
		}
		return result;
	}

	public static string CnvLocalDB(string lLocalDB)
	{
		string result = "";
		lLocalDB = Strings.UCase(Strings.Trim(lLocalDB));
		switch (lLocalDB)
		{
		case "SQLITE":
			result = "SQLite";
			break;
		case "SQLSERVER":
			result = "SQLServer";
			break;
		case "MYSQL":
			result = "MySQL";
			break;
		case "SAPHANA":
			result = "SAPHana";
			break;
		case "DENODO":
			result = "Denodo";
			break;
		case "SNOWFLAKE":
			result = "Snowflake";
			break;
		case "POSTGRES":
			result = "Postgres";
			break;
		case "IBI-DAAS":
			result = "IBI-DaaS";
			break;
		case "IMBIGDATA":
			result = "iMBigData";
			break;
		default:
			if (Operators.CompareString(lLocalDB, "TEXT", TextCompare: false) != 0)
			{
				result = Strings.StrConv(lLocalDB, VbStrConv.ProperCase);
				break;
			}
			goto case "DUCKDB";
		case "DUCKDB":
			result = lLocalDB;
			break;
		case null:
		case "":
			break;
		}
		return result;
	}

	public static TreeNode SearchTree(string TextTosearch, TreeNode MyRootQNode, ref int l_Idx1, ref int l_Idx2, ref int l_Idx3, ref int l_Idx4, ref int l_Idx5)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		TreeNode treeNode = default(TreeNode);
		int num7 = default(int);
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
								goto IL_0025;
							case 6:
								goto IL_006a;
							case 8:
								goto IL_0080;
							case 9:
								goto IL_0091;
							case 10:
								goto IL_00ca;
							case 12:
								goto IL_00e1;
							case 13:
								goto IL_00f3;
							case 14:
								goto IL_011f;
							case 16:
								goto IL_0136;
							case 17:
								goto IL_0147;
							case 18:
								goto IL_0167;
							case 20:
								goto IL_017e;
							case 21:
								goto IL_0192;
							case 22:
								goto IL_01b1;
							case 23:
								goto IL_01e2;
							case 24:
								goto IL_0205;
							case 26:
								goto IL_0228;
							case 27:
								goto IL_024f;
							case 28:
								goto IL_0260;
							case 25:
							case 30:
							case 31:
								goto IL_026c;
							case 32:
								goto IL_027e;
							case 34:
							case 35:
							case 36:
								goto IL_028b;
							case 29:
							case 33:
							case 37:
							case 38:
								goto IL_029f;
							case 41:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 7:
							case 11:
							case 15:
							case 19:
							case 39:
							case 40:
							case 42:
							case 43:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_027e:
						num2 = 32;
						l_Idx1 = num5;
						goto IL_029f;
						IL_028b:
						num2 = 36;
						num5++;
						goto IL_0294;
						IL_026c:
						num2 = 31;
						if (!Information.IsNothing(treeNode))
						{
							goto IL_027e;
						}
						goto IL_028b;
						IL_029f:
						num2 = 38;
						if (Information.IsNothing(treeNode))
						{
							break;
						}
						goto end_IL_0001_3;
						IL_000b:
						num2 = 2;
						treeNode = null;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num5 = 0;
						goto IL_0014;
						IL_0014:
						num2 = 4;
						if (l_Idx5 != -1)
						{
							goto IL_0025;
						}
						goto IL_006a;
						IL_0025:
						num2 = 5;
						treeNode = SearchLoop(MyRootQNode.Nodes[l_Idx1].Nodes[l_Idx2].Nodes[l_Idx3].Nodes[l_Idx4], TextTosearch, 5, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_006a;
						IL_006a:
						num2 = 6;
						if (!Information.IsNothing(treeNode))
						{
							goto end_IL_0001_3;
						}
						goto IL_0080;
						IL_0080:
						num2 = 8;
						if (l_Idx4 != -1)
						{
							goto IL_0091;
						}
						goto IL_00ca;
						IL_0091:
						num2 = 9;
						treeNode = SearchLoop(MyRootQNode.Nodes[l_Idx1].Nodes[l_Idx2].Nodes[l_Idx3], TextTosearch, 4, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_00ca;
						IL_00ca:
						num2 = 10;
						if (!Information.IsNothing(treeNode))
						{
							goto end_IL_0001_3;
						}
						goto IL_00e1;
						IL_00e1:
						num2 = 12;
						if (l_Idx3 != -1)
						{
							goto IL_00f3;
						}
						goto IL_011f;
						IL_00f3:
						num2 = 13;
						treeNode = SearchLoop(MyRootQNode.Nodes[l_Idx1].Nodes[l_Idx2], TextTosearch, 3, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_011f;
						IL_011f:
						num2 = 14;
						if (!Information.IsNothing(treeNode))
						{
							goto end_IL_0001_3;
						}
						goto IL_0136;
						IL_0136:
						num2 = 16;
						if (l_Idx2 != -1)
						{
							goto IL_0147;
						}
						goto IL_0167;
						IL_0147:
						num2 = 17;
						treeNode = SearchLoop(MyRootQNode.Nodes[l_Idx1], TextTosearch, 2, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_0167;
						IL_0167:
						num2 = 18;
						if (!Information.IsNothing(treeNode))
						{
							goto end_IL_0001_3;
						}
						goto IL_017e;
						IL_017e:
						num2 = 20;
						if (l_Idx1 != -1)
						{
							goto IL_0192;
						}
						goto IL_029f;
						IL_0192:
						num2 = 21;
						num6 = l_Idx1;
						num7 = MyRootQNode.Nodes.Count - 1;
						num5 = num6;
						goto IL_0294;
						IL_0294:
						if (num5 <= num7)
						{
							goto IL_01b1;
						}
						goto IL_029f;
						IL_01b1:
						num2 = 22;
						if (Operators.CompareString(Strings.UCase(MyRootQNode.Nodes[num5].Text), "OUTPUT OPTIONS", TextCompare: false) != 0)
						{
							goto IL_01e2;
						}
						goto IL_028b;
						IL_01e2:
						num2 = 23;
						if (MyRootQNode.Nodes[num5].Nodes.Count > 0)
						{
							goto IL_0205;
						}
						goto IL_0228;
						IL_0205:
						num2 = 24;
						treeNode = SearchLoop(MyRootQNode.Nodes[num5], TextTosearch, 2, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_026c;
						IL_0228:
						num2 = 26;
						if (MyRootQNode.Nodes[num5].Text.IndexOf(TextTosearch) >= 0)
						{
							goto IL_024f;
						}
						goto IL_026c;
						IL_024f:
						num2 = 27;
						treeNode = MyRootQNode.Nodes[num5];
						goto IL_0260;
						IL_0260:
						num2 = 28;
						l_Idx1 = num5 + 1;
						goto IL_029f;
						end_IL_0001_2:
						break;
					}
					num2 = 41;
					l_Idx1 = -1;
					break;
				}
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
		return treeNode;
	}

	public static TreeNode SearchLoop(TreeNode ANode, string TextTosearch, int MyLevel, ref int l_Idx1, ref int l_Idx2, ref int l_Idx3, ref int l_Idx4, ref int l_Idx5)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		TreeNode treeNode = default(TreeNode);
		int num6 = default(int);
		int num8 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 651:
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
								goto IL_0016;
							case 6:
								goto IL_003b;
							case 8:
								goto IL_0044;
							case 10:
								goto IL_004e;
							case 12:
								goto IL_0059;
							case 14:
								goto IL_0064;
							case 5:
							case 7:
							case 9:
							case 11:
							case 13:
							case 15:
							case 16:
								goto IL_006f;
							case 17:
								goto IL_0074;
							case 18:
								goto IL_0082;
							case 19:
								goto IL_0088;
							case 20:
								goto IL_00a7;
							case 21:
								goto IL_00d8;
							case 22:
								goto IL_00fb;
							case 23:
								goto IL_011e;
							case 24:
								goto IL_0130;
							case 28:
								goto IL_0151;
							case 29:
								goto IL_0178;
							case 30:
								goto IL_0189;
							case 26:
							case 27:
							case 32:
							case 33:
							case 34:
								goto IL_01a8;
							case 25:
							case 31:
							case 35:
								goto IL_01ba;
							case 36:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 37:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0189:
						num2 = 30;
						AssignLevel("I", MyLevel, num5, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_01ba;
						IL_01ba:
						num2 = 35;
						if (!Information.IsNothing(treeNode))
						{
							goto end_IL_0001_3;
						}
						break;
						IL_0178:
						num2 = 29;
						treeNode = ANode.Nodes[num5];
						goto IL_0189;
						IL_01a8:
						num2 = 34;
						num5++;
						goto IL_01b1;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num6 = 0;
						goto IL_0016;
						IL_0016:
						num2 = 4;
						switch (MyLevel)
						{
						case 1:
							break;
						case 2:
							goto IL_0044;
						case 3:
							goto IL_004e;
						case 4:
							goto IL_0059;
						case 5:
							goto IL_0064;
						default:
							goto IL_006f;
						}
						goto IL_003b;
						IL_0064:
						num2 = 14;
						num6 = l_Idx5;
						goto IL_006f;
						IL_0059:
						num2 = 12;
						num6 = l_Idx4;
						goto IL_006f;
						IL_004e:
						num2 = 10;
						num6 = l_Idx3;
						goto IL_006f;
						IL_0044:
						num2 = 8;
						num6 = l_Idx2;
						goto IL_006f;
						IL_003b:
						num2 = 6;
						num6 = l_Idx1;
						goto IL_006f;
						IL_006f:
						num2 = 16;
						treeNode = null;
						goto IL_0074;
						IL_0074:
						num2 = 17;
						if (num6 == -1)
						{
							goto IL_0082;
						}
						goto IL_0088;
						IL_0082:
						num2 = 18;
						num6 = 0;
						goto IL_0088;
						IL_0088:
						num2 = 19;
						num7 = num6;
						num8 = ANode.Nodes.Count - 1;
						num5 = num7;
						goto IL_01b1;
						IL_01b1:
						if (num5 <= num8)
						{
							goto IL_00a7;
						}
						goto IL_01ba;
						IL_00a7:
						num2 = 20;
						if (Operators.CompareString(Strings.UCase(ANode.Nodes[num5].Text), "QUERY OPTIONS", TextCompare: false) != 0)
						{
							goto IL_00d8;
						}
						goto IL_01a8;
						IL_00d8:
						num2 = 21;
						if (ANode.Nodes[num5].Nodes.Count > 0)
						{
							goto IL_00fb;
						}
						goto IL_0151;
						IL_00fb:
						num2 = 22;
						treeNode = SearchLoop(ANode.Nodes[num5], TextTosearch, MyLevel + 1, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_011e;
						IL_011e:
						num2 = 23;
						if (!Information.IsNothing(treeNode))
						{
							goto IL_0130;
						}
						goto IL_01a8;
						IL_0130:
						num2 = 24;
						AssignLevel("A", MyLevel, num5, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
						goto IL_01ba;
						IL_0151:
						num2 = 28;
						if (ANode.Nodes[num5].Text.IndexOf(TextTosearch) >= 0)
						{
							goto IL_0178;
						}
						goto IL_01a8;
						end_IL_0001_2:
						break;
					}
					num2 = 36;
					AssignLevel("R", MyLevel, -1, ref l_Idx1, ref l_Idx2, ref l_Idx3, ref l_Idx4, ref l_Idx5);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 651;
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
		return treeNode;
	}

	public static void AssignLevel(string MyMode, int MyLevel, int MyInc, ref int l_Idx1, ref int l_Idx2, ref int l_Idx3, ref int l_Idx4, ref int l_Idx5)
	{
		checked
		{
			switch (Strings.UCase(Strings.Trim(MyMode)))
			{
			case "I":
				switch (MyLevel)
				{
				case 1:
					l_Idx1 = MyInc + 1;
					break;
				case 2:
					l_Idx2 = MyInc + 1;
					break;
				case 3:
					l_Idx3 = MyInc + 1;
					break;
				case 4:
					l_Idx4 = MyInc + 1;
					break;
				case 5:
					l_Idx5 = MyInc + 1;
					break;
				}
				break;
			case "A":
				switch (MyLevel)
				{
				case 1:
					l_Idx1 = MyInc;
					break;
				case 2:
					l_Idx2 = MyInc;
					break;
				case 3:
					l_Idx3 = MyInc;
					break;
				case 4:
					l_Idx4 = MyInc;
					break;
				case 5:
					l_Idx5 = MyInc;
					break;
				}
				break;
			case "R":
				switch (MyLevel)
				{
				case 1:
					l_Idx1 = -1;
					break;
				case 2:
					if (l_Idx2 != -1 && l_Idx1 != -1)
					{
						l_Idx1++;
					}
					l_Idx2 = -1;
					break;
				case 3:
					if (l_Idx3 != -1 && l_Idx2 != -1)
					{
						l_Idx2++;
					}
					l_Idx3 = -1;
					break;
				case 4:
					if (l_Idx4 != -1 && l_Idx3 != -1)
					{
						l_Idx3++;
					}
					l_Idx4 = -1;
					break;
				case 5:
					if (l_Idx5 != -1 && l_Idx4 != -1)
					{
						l_Idx4++;
					}
					l_Idx5 = -1;
					break;
				}
				break;
			}
		}
	}

	public static bool IsValidFilter(string MyMode, string MyAndorCol, string MyOpr, string MyVal1, string MyVal2)
	{
		bool flag = true;
		string text = Strings.Trim(MyVal1);
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			text = Strings.Replace(text, "'", "", 1, -1, CompareMethod.Text);
			if ((Operators.CompareString(Strings.Mid(text + "   ", 1, 3), "<<<", TextCompare: false) == 0) & (Operators.CompareString(Strings.Right("   " + text, 3), ">>>", TextCompare: false) == 0))
			{
				flag = false;
			}
		}
		bool result = true;
		MyOpr = Strings.UCase(MyOpr);
		if (Operators.CompareString(MyMode, "1", TextCompare: false) != 0)
		{
			if (Operators.CompareString(MyMode, "2", TextCompare: false) == 0 && ((Operators.CompareString(MyAndorCol, "", TextCompare: false) == 0) | (Operators.CompareString(MyOpr, "", TextCompare: false) == 0) | ((Operators.CompareString(MyOpr, "IS NULL", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "IS NOT NULL", TextCompare: false) != 0) & (Operators.CompareString(MyVal1, "", TextCompare: false) == 0)) | ((((LikeOperator.LikeString(MyOpr, "*BETWEEN*", CompareMethod.Binary) | LikeOperator.LikeString(MyOpr, "*TEMP", CompareMethod.Binary)) & (Operators.CompareString(MyVal1, "&PROMPT&", TextCompare: false) != 0)) && flag) & (Operators.CompareString(MyVal2, "", TextCompare: false) == 0))))
			{
				result = false;
			}
		}
		else if ((Operators.CompareString(Strings.Mid(Strings.Trim(MyAndorCol) + "  ", 1, 2), "--", TextCompare: false) == 0) | (Operators.CompareString(MyOpr, "", TextCompare: false) == 0) | ((Operators.CompareString(MyOpr, "IS NULL", TextCompare: false) != 0) & (Operators.CompareString(MyOpr, "IS NOT NULL", TextCompare: false) != 0) & (Operators.CompareString(MyVal1, "", TextCompare: false) == 0)) | (((LikeOperator.LikeString(MyOpr, "*BETWEEN*", CompareMethod.Binary) & (Operators.CompareString(MyVal1, "&PROMPT&", TextCompare: false) != 0)) && flag) & (Operators.CompareString(MyVal2, "", TextCompare: false) == 0)))
		{
			result = false;
		}
		return result;
	}

	public static string Get_Col_Header(string MyCol, string MyHdr, short ll_ObjectType)
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
				case 431:
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
							goto IL_0043;
						case 5:
						case 6:
							goto IL_005c;
						case 7:
							goto IL_0084;
						case 8:
						case 9:
							goto IL_009d;
						case 11:
							goto IL_00b4;
						case 12:
							goto IL_00bf;
						case 13:
							goto IL_00cf;
						case 14:
							goto IL_00ee;
						case 15:
						case 16:
							goto IL_0107;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 18:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00cf:
					num2 = 13;
					if (Operators.CompareString(Strings.Mid(text, 1, 1), "\"", TextCompare: false) == 0)
					{
						goto IL_00ee;
					}
					goto IL_0107;
					IL_00ee:
					num2 = 14;
					text = Strings.Replace(text, "\"", "", 1, -1, CompareMethod.Text);
					goto IL_0107;
					IL_00bf:
					num2 = 12;
					text = General_Procedures.Strip_CSV_Square(text, ll_ObjectType);
					goto IL_00cf;
					IL_0107:
					num2 = 16;
					if (Operators.CompareString(Strings.Mid(text + "   ", 1, 3), "<!>", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(MyHdr, "", TextCompare: false) != 0)
					{
						goto IL_0025;
					}
					goto IL_00b4;
					IL_0025:
					num2 = 3;
					if (Operators.CompareString(Strings.Mid(MyHdr, 1, 1), "\"", TextCompare: false) == 0)
					{
						goto IL_0043;
					}
					goto IL_005c;
					IL_0043:
					num2 = 4;
					MyHdr = Strings.Replace(MyHdr, "\"", "", 1, -1, CompareMethod.Text);
					goto IL_005c;
					IL_005c:
					num2 = 6;
					if (Operators.CompareString(Strings.Mid(MyHdr + "   ", 1, 3), "<!>", TextCompare: false) == 0)
					{
						goto IL_0084;
					}
					goto IL_009d;
					IL_0084:
					num2 = 7;
					MyHdr = Strings.Replace(MyHdr, "<!>", "", 1, -1, CompareMethod.Text);
					goto IL_009d;
					IL_009d:
					num2 = 9;
					text = General_Procedures.Strip_CSV_Square(MyHdr, ll_ObjectType);
					goto end_IL_0001_3;
					IL_00b4:
					num2 = 11;
					text = General_Procedures.Strip_Column(MyCol, 1);
					goto IL_00bf;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				text = Strings.Replace(text, "<!>", "", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 431;
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

	public static void Clear_Global_Vars(int MyMode)
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
							goto IL_0020;
						case 4:
							goto IL_0028;
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
					Array.Clear(Globals_Renamed.gGlobals, 0, Globals_Renamed.gGlobals.Length);
					goto IL_0020;
					IL_0020:
					num2 = 3;
					Globals_Renamed.gNoGlobals = -1;
					goto IL_0028;
					IL_0028:
					num2 = 4;
					if (MyMode != 1)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				Globals_Renamed.gGlobalsFile = "";
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
	}

	public static string GetFileDLM(string CurrentFile)
	{
		string result = ",";
		int num = 0;
		string text = "";
		num = Strings.InStrRev(CurrentFile, ".");
		switch ((num == 0) ? "" : Strings.UCase(Strings.Mid(CurrentFile, checked(num + 1))))
		{
		case "TAB":
			result = "\t";
			break;
		case "ASC":
			result = "|";
			break;
		case "PLUS":
			result = "+";
			break;
		}
		return result;
	}

	public static string Replace_Globals(string MyStr, int MyMode = 0)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		string text2 = default(string);
		string text3 = default(string);
		int gNoGlobals = default(int);
		int gNoGlobals2 = default(int);
		int g_VarNo = default(int);
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
					case 1164:
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
								goto IL_0026;
							case 8:
								goto IL_0042;
							case 9:
								goto IL_005a;
							case 10:
								goto IL_0073;
							case 11:
								goto IL_0087;
							case 12:
							case 13:
								goto IL_00a8;
							case 14:
								goto IL_00d8;
							case 15:
								goto IL_00ef;
							case 16:
								goto IL_0106;
							case 17:
								goto IL_0131;
							case 18:
								goto IL_015c;
							case 19:
								goto IL_0187;
							case 20:
								goto IL_019e;
							case 21:
								goto IL_01b5;
							case 22:
								goto IL_01cc;
							case 23:
								goto IL_01e3;
							case 24:
								goto IL_01fa;
							case 25:
								goto IL_0211;
							case 26:
								goto IL_0228;
							case 27:
								goto IL_0235;
							case 28:
								goto IL_024c;
							case 29:
							case 30:
								goto IL_0265;
							case 31:
								goto IL_0276;
							case 32:
								goto IL_0285;
							case 33:
								goto IL_02c3;
							case 35:
								goto IL_02d9;
							case 36:
								goto IL_02e8;
							case 37:
								goto IL_02fe;
							case 38:
								goto IL_0317;
							case 39:
								goto IL_032d;
							case 40:
								goto IL_035c;
							case 34:
							case 41:
							case 42:
								goto IL_036f;
							case 43:
								goto IL_037e;
							case 44:
								goto IL_03b4;
							default:
								goto end_IL_0001;
							case 7:
							case 45:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0317:
						num2 = 38;
						text = Globals_Renamed.gGlobals[num5].Value;
						goto IL_032d;
						IL_032d:
						num2 = 39;
						text2 = Strings.Replace(text2, "<<<" + Globals_Renamed.gGlobals[num5].Variable + ">>>", text, 1, -1, CompareMethod.Text);
						goto IL_035c;
						IL_02fe:
						num2 = 37;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_0317;
						}
						goto IL_032d;
						IL_035c:
						num2 = 40;
						num5++;
						goto IL_0365;
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
						text = "";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						text2 = MyStr;
						goto IL_0026;
						IL_0026:
						num2 = 6;
						if (Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_0042;
						IL_0042:
						num2 = 8;
						text3 = MyProject.Forms.FrmMain.mnuspfsitevalue.Text;
						goto IL_005a;
						IL_005a:
						num2 = 9;
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							goto IL_0073;
						}
						goto IL_00a8;
						IL_0073:
						num2 = 10;
						text2 = Strings.Replace(text2, "<<<spf-site>>>", text3, 1, -1, CompareMethod.Text);
						goto IL_0087;
						IL_0087:
						num2 = 11;
						text2 = Strings.Replace(text2, "<<<spf-site-for-file-name>>>", General_Procedures.Replace_Special_Chars(text3, "_"), 1, -1, CompareMethod.Text);
						goto IL_00a8;
						IL_00a8:
						num2 = 13;
						text2 = Strings.Replace(text2, "<<<spf-hadoop-server>>>", General_Procedures.NodeCheck_s(Globals_Renamed.gHadoopServer, "S", Conversions.ToShort("1")), 1, -1, CompareMethod.Text);
						goto IL_00d8;
						IL_00d8:
						num2 = 14;
						text2 = Strings.Replace(text2, "<<<spf-app-server>>>", "atd_atm.hadoop", 1, -1, CompareMethod.Text);
						goto IL_00ef;
						IL_00ef:
						num2 = 15;
						text2 = Strings.Replace(text2, "<<<spf-query-dir>>>", Globals_Renamed.gQueryDir, 1, -1, CompareMethod.Text);
						goto IL_0106;
						IL_0106:
						num2 = 16;
						text2 = Strings.Replace(text2, "<<<spf-fab-sh-dir>>>", "\\\\SHUser-Prod.intel.com\\SHProdUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\", 1, -1, CompareMethod.Text);
						goto IL_0131;
						IL_0131:
						num2 = 17;
						text2 = Strings.Replace(text2, "<<<spf-at-sh-dir>>>", "\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\", 1, -1, CompareMethod.Text);
						goto IL_015c;
						IL_015c:
						num2 = 18;
						text2 = Strings.Replace(text2, "<<<spf-dl-sh-dir>>>", "\\\\SHUser-NSG.intel.com\\SHNSGUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\", 1, -1, CompareMethod.Text);
						goto IL_0187;
						IL_0187:
						num2 = 19;
						text2 = Strings.Replace(text2, "<<<spf-email-type>>>", Globals_Renamed.gEmailOutlook, 1, -1, CompareMethod.Text);
						goto IL_019e;
						IL_019e:
						num2 = 20;
						text2 = Strings.Replace(text2, "<<<spf-instance>>>", Globals_Renamed.gSPFCache, 1, -1, CompareMethod.Text);
						goto IL_01b5;
						IL_01b5:
						num2 = 21;
						text2 = Strings.Replace(text2, "<<<spf-tdx-config>>>", "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Config\\TDX\\", 1, -1, CompareMethod.Text);
						goto IL_01cc;
						IL_01cc:
						num2 = 22;
						text2 = Strings.Replace(text2, "<<<spf-tdx-data1>>>", "\\\\atdpfile1.ch.intel.com\\hadoop_staging\\tool_data_attd_atm\\datd\\", 1, -1, CompareMethod.Text);
						goto IL_01e3;
						IL_01e3:
						num2 = 23;
						text2 = Strings.Replace(text2, "<<<spf-schema-dir>>>", Globals_Renamed.MySchemaDir, 1, -1, CompareMethod.Text);
						goto IL_01fa;
						IL_01fa:
						num2 = 24;
						text2 = Strings.Replace(text2, "<<<spf-winuser>>>", Globals_Renamed.gWinuser, 1, -1, CompareMethod.Text);
						goto IL_0211;
						IL_0211:
						num2 = 25;
						text2 = Strings.Replace(text2, "</dq/>", "\"", 1, -1, CompareMethod.Text);
						goto IL_0228;
						IL_0228:
						num2 = 26;
						if (MyMode == 1)
						{
							goto IL_0235;
						}
						goto IL_0265;
						IL_0235:
						num2 = 27;
						text2 = Strings.Replace(text2, "<<<%username%>>>", Globals_Renamed.gWinuser, 1, -1, CompareMethod.Text);
						goto IL_024c;
						IL_024c:
						num2 = 28;
						text2 = Strings.Replace(text2, "<<<spf-default-dir>>>", Globals_Renamed.MyPCDir, 1, -1, CompareMethod.Text);
						goto IL_0265;
						IL_0265:
						num2 = 30;
						if (!Globals_Renamed.gISSHSave)
						{
							goto IL_0276;
						}
						goto IL_02d9;
						IL_0276:
						num2 = 31;
						gNoGlobals = Globals_Renamed.gNoGlobals;
						num5 = 0;
						goto IL_02cc;
						IL_02cc:
						if (num5 <= gNoGlobals)
						{
							goto IL_0285;
						}
						goto IL_036f;
						IL_0285:
						num2 = 32;
						text2 = Strings.Replace(text2, "<<<" + Globals_Renamed.gGlobals[num5].Variable + ">>>", Globals_Renamed.gGlobals[num5].Value, 1, -1, CompareMethod.Text);
						goto IL_02c3;
						IL_02c3:
						num2 = 33;
						num5++;
						goto IL_02cc;
						IL_02d9:
						num2 = 35;
						gNoGlobals2 = Globals_Renamed.gNoGlobals;
						num5 = 0;
						goto IL_0365;
						IL_0365:
						if (num5 <= gNoGlobals2)
						{
							goto IL_02e8;
						}
						goto IL_036f;
						IL_036f:
						num2 = 42;
						g_VarNo = Globals_Renamed.g_VarNo;
						num5 = 0;
						goto IL_03bd;
						IL_03bd:
						if (num5 > g_VarNo)
						{
							goto end_IL_0001_2;
						}
						goto IL_037e;
						IL_037e:
						num2 = 43;
						text2 = Strings.Replace(text2, "<<<" + Globals_Renamed.g_GVars[0, num5] + ">>>", Globals_Renamed.g_GVars[1, num5], 1, -1, CompareMethod.Text);
						goto IL_03b4;
						IL_03b4:
						num2 = 44;
						num5++;
						goto IL_03bd;
						IL_02e8:
						num2 = 36;
						text = Globals_Renamed.gGlobals[num5].SHValue;
						goto IL_02fe;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1164;
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

	public static void Assign_Pre_Post_Col(short MyMode, string MyDT, string MyStr, ref string PreCol, ref string PostCol)
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
				short num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 1005:
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
							goto IL_0020;
						case 6:
							goto IL_0037;
						case 7:
							goto IL_0084;
						case 8:
							goto IL_008d;
						case 10:
							goto IL_009a;
						case 11:
							goto IL_00c2;
						case 12:
							goto IL_00cc;
						case 14:
							goto IL_00db;
						case 15:
							goto IL_00e5;
						case 18:
							goto IL_00f7;
						case 19:
							goto IL_0123;
						case 20:
							goto IL_018f;
						case 21:
							goto IL_0199;
						case 23:
							goto IL_01a8;
						case 24:
							goto IL_01b2;
						case 27:
							goto IL_01c4;
						case 28:
							goto IL_01dc;
						case 29:
							goto IL_01f1;
						case 30:
							goto IL_01fb;
						case 32:
							goto IL_020a;
						case 33:
							goto IL_0214;
						case 36:
							goto IL_0226;
						case 37:
							goto IL_0236;
						case 38:
							goto IL_0278;
						case 39:
							goto IL_0282;
						case 41:
							goto IL_0291;
						case 42:
							goto IL_029b;
						case 45:
							goto IL_02aa;
						case 46:
							goto IL_02d2;
						case 47:
							goto IL_02dc;
						case 49:
							goto IL_02eb;
						case 50:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 9:
						case 13:
						case 16:
						case 17:
						case 22:
						case 25:
						case 26:
						case 31:
						case 34:
						case 35:
						case 40:
						case 43:
						case 44:
						case 48:
						case 51:
						case 52:
						case 53:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_008d:
					num2 = 8;
					PostCol = "";
					goto end_IL_0001_3;
					IL_009a:
					num2 = 10;
					if ((Operators.CompareString(MyDT, "n", TextCompare: false) == 0) | (Operators.CompareString(MyDT, "f", TextCompare: false) == 0))
					{
						goto IL_00c2;
					}
					goto IL_00db;
					IL_0084:
					num2 = 7;
					PreCol = "";
					goto IL_008d;
					IL_01a8:
					num2 = 23;
					PreCol = "To_Date('";
					goto IL_01b2;
					IL_000b:
					num2 = 2;
					MyStr = Strings.UCase(MyStr);
					goto IL_0015;
					IL_0015:
					num2 = 3;
					MyDT = Strings.LCase(MyDT);
					goto IL_0020;
					IL_0020:
					num2 = 4;
					num5 = MyMode;
					if (num5 == 1)
					{
						goto IL_0037;
					}
					if (num5 != 2)
					{
						goto end_IL_0001_3;
					}
					goto IL_00f7;
					IL_01b2:
					num2 = 24;
					PostCol = "','dd-mon-yyyy hh24:mi:ss')";
					goto end_IL_0001_3;
					IL_00f7:
					num2 = 18;
					if (Operators.CompareString(MyDT, "d", TextCompare: false) == 0 || Operators.CompareString(MyDT, "v", TextCompare: false) == 0)
					{
						goto IL_0123;
					}
					goto IL_01c4;
					IL_018f:
					num2 = 20;
					PreCol = "";
					goto IL_0199;
					IL_01c4:
					num2 = 27;
					if (Operators.CompareString(MyDT, "o", TextCompare: false) == 0)
					{
						goto IL_01dc;
					}
					goto IL_0226;
					IL_01dc:
					num2 = 28;
					if (LikeOperator.LikeString(MyStr, "*NOW()*", CompareMethod.Binary))
					{
						goto IL_01f1;
					}
					goto IL_020a;
					IL_01f1:
					num2 = 29;
					PreCol = "";
					goto IL_01fb;
					IL_01fb:
					num2 = 30;
					PostCol = "";
					goto end_IL_0001_3;
					IL_020a:
					num2 = 32;
					PreCol = "dt.datetime.strptime('";
					goto IL_0214;
					IL_0214:
					num2 = 33;
					PostCol = "','%Y-%m-%d %H:%M:%S')";
					goto end_IL_0001_3;
					IL_0226:
					num2 = 36;
					if (General_Procedures.IsDateDT(MyDT, 1))
					{
						goto IL_0236;
					}
					goto IL_02aa;
					IL_0236:
					num2 = 37;
					if (LikeOperator.LikeString(MyStr, "*DATE*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*TIMESTAMP*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*NOW()*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*SPF_FN$LWW*", CompareMethod.Binary))
					{
						goto IL_0278;
					}
					goto IL_0291;
					IL_0199:
					num2 = 21;
					PostCol = "";
					goto end_IL_0001_3;
					IL_0037:
					num2 = 6;
					if (General_Procedures.IsDateDT(MyDT, 1) && (LikeOperator.LikeString(MyStr, "*DATE*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*TIMESTAMP*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*NOW()*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*SPF_FN$LWW*", CompareMethod.Binary)))
					{
						goto IL_0084;
					}
					goto IL_009a;
					IL_00c2:
					num2 = 11;
					PreCol = "";
					goto IL_00cc;
					IL_0291:
					num2 = 41;
					PreCol = "'";
					goto IL_029b;
					IL_029b:
					num2 = 42;
					PostCol = "'";
					goto end_IL_0001_3;
					IL_0278:
					num2 = 38;
					PreCol = "";
					goto IL_0282;
					IL_0282:
					num2 = 39;
					PostCol = "";
					goto end_IL_0001_3;
					IL_02aa:
					num2 = 45;
					if ((Operators.CompareString(MyDT, "n", TextCompare: false) == 0) | (Operators.CompareString(MyDT, "f", TextCompare: false) == 0))
					{
						goto IL_02d2;
					}
					goto IL_02eb;
					IL_02d2:
					num2 = 46;
					PreCol = "";
					goto IL_02dc;
					IL_02dc:
					num2 = 47;
					PostCol = "";
					goto end_IL_0001_3;
					IL_02eb:
					num2 = 49;
					PreCol = "'";
					break;
					IL_0123:
					num2 = 19;
					if (LikeOperator.LikeString(MyStr, "*SYSDATE*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*CURRENT_*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*TRUNC(*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*SYSDATE*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*'D'*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*'DD'*", CompareMethod.Binary) || LikeOperator.LikeString(MyStr, "*@*_OF_WW@*", CompareMethod.Binary))
					{
						goto IL_018f;
					}
					goto IL_01a8;
					IL_00cc:
					num2 = 12;
					PostCol = "";
					goto end_IL_0001_3;
					IL_00db:
					num2 = 14;
					PreCol = "'";
					goto IL_00e5;
					IL_00e5:
					num2 = 15;
					PostCol = "'";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 50;
				PostCol = "'";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1005;
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

	public static string GetMidasEngine()
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
				case 164:
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
							goto IL_0023;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0055;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0023:
					num2 = 3;
					result = "/ENGINE=" + BuildSQL.Get_Uber_Type();
					goto end_IL_0001_3;
					IL_0038:
					num2 = 5;
					if (!LikeOperator.LikeString(Strings.UCase(Globals_Renamed.gMidasDriver), ".NET*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0055;
					IL_000b:
					num2 = 2;
					if (LikeOperator.LikeString(Globals_Renamed.gMidasDriver, "UBER*", CompareMethod.Binary))
					{
						goto IL_0023;
					}
					goto IL_0038;
					IL_0055:
					num2 = 6;
					result = "/ENGINE=VA (.NET)";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				result = "/ENGINE=CB";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 164;
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

	public static string AddDOSRetry(string DOSAction, string MyDesc)
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
				case 620:
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
							goto IL_002a;
						case 5:
						case 6:
							goto IL_0040;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_005c;
						case 9:
							goto IL_006a;
						case 10:
							goto IL_0079;
						case 11:
							goto IL_0088;
						case 12:
							goto IL_0097;
						case 13:
							goto IL_00a6;
						case 14:
							goto IL_00b6;
						case 15:
							goto IL_00c5;
						case 16:
							goto IL_00d4;
						case 17:
							goto IL_00e3;
						case 18:
							goto IL_00f2;
						case 19:
							goto IL_0101;
						case 20:
							goto IL_0110;
						case 21:
							goto IL_011f;
						case 22:
							goto IL_012e;
						case 23:
							goto IL_013d;
						case 24:
							goto IL_014c;
						case 25:
							goto IL_015b;
						case 26:
							goto IL_016a;
						case 27:
							goto IL_0179;
						case 28:
							goto IL_0188;
						case 29:
							goto IL_0197;
						case 30:
							goto IL_01a6;
						case 31:
							goto IL_01b5;
						case 32:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 33:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0197:
					num2 = 29;
					text += "\r\nRem";
					goto IL_01a6;
					IL_01a6:
					num2 = 30;
					text += "\r\n:RETRYEND";
					goto IL_01b5;
					IL_0188:
					num2 = 28;
					text += "\r\nGOTO DORETRY";
					goto IL_0197;
					IL_01b5:
					num2 = 31;
					text += "\r\nRem";
					break;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					if (Operators.CompareString(MyDesc, "", TextCompare: false) != 0)
					{
						goto IL_002a;
					}
					goto IL_0040;
					IL_002a:
					num2 = 4;
					text = text + "\r\n@Echo " + MyDesc + ". Stand By ...";
					goto IL_0040;
					IL_0040:
					num2 = 6;
					text += "\r\nSET /A RunCtr=1";
					goto IL_004e;
					IL_004e:
					num2 = 7;
					text += "\r\nSET /A RunMax=8";
					goto IL_005c;
					IL_005c:
					num2 = 8;
					text += "\r\nSet /A RunSecs=10";
					goto IL_006a;
					IL_006a:
					num2 = 9;
					text += "\r\nset /A RunWait=Runsecs*1000";
					goto IL_0079;
					IL_0079:
					num2 = 10;
					text += "\r\nRem";
					goto IL_0088;
					IL_0088:
					num2 = 11;
					text += "\r\n:DORETRY";
					goto IL_0097;
					IL_0097:
					num2 = 12;
					text += "\r\nRem";
					goto IL_00a6;
					IL_00a6:
					num2 = 13;
					text = text + "\r\n" + DOSAction;
					goto IL_00b6;
					IL_00b6:
					num2 = 14;
					text += "\r\nSET SOK=%errorlevel%";
					goto IL_00c5;
					IL_00c5:
					num2 = 15;
					text += "\r\nIF (%SOK%) EQU (0) GOTO RETRYEND";
					goto IL_00d4;
					IL_00d4:
					num2 = 16;
					text += "\r\nRem";
					goto IL_00e3;
					IL_00e3:
					num2 = 17;
					text += "\r\n:DORETRY";
					goto IL_00f2;
					IL_00f2:
					num2 = 18;
					text += "\r\nRem";
					goto IL_0101;
					IL_0101:
					num2 = 19;
					text += "\r\nSET /A RunCtr+=1";
					goto IL_0110;
					IL_0110:
					num2 = 20;
					text += "\r\nIf (%RunCtr%) GTR (%RunMax%) GOTO RETRYEND";
					goto IL_011f;
					IL_011f:
					num2 = 21;
					text += "\r\n@Echo.";
					goto IL_012e;
					IL_012e:
					num2 = 22;
					text += "\r\n@Echo *****************************************************************************";
					goto IL_013d;
					IL_013d:
					num2 = 23;
					text += "\r\n@Echo   Waiting %RunSecs% seconds before retrying  %RunCtr% of %RunMax% ... %date% %time%";
					goto IL_014c;
					IL_014c:
					num2 = 24;
					text += "\r\nPING 1.1.1.1 -n  1 -w %RunWait% > NUL";
					goto IL_015b;
					IL_015b:
					num2 = 25;
					text += "\r\n@Echo   Retrying ...";
					goto IL_016a;
					IL_016a:
					num2 = 26;
					text += "\r\n@Echo *****************************************************************************";
					goto IL_0179;
					IL_0179:
					num2 = 27;
					text += "\r\n@Echo.";
					goto IL_0188;
					end_IL_0001_2:
					break;
				}
				num2 = 32;
				text += "\r\n@Echo.";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 620;
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

	public static short SaveScriptExe(string DTRCode, string SQLPJob, int MyOpt = 0)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		int num2 = default(int);
		short result;
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string text2;
				string text7;
				string text5;
				switch (try0001_dispatch)
				{
				default:
				{
					errsource = "BuildForm - SaveScriptExe";
					ProjectData.ClearProjectError();
					num2 = 2;
					string text = "SPFSQL";
					text2 = Globals_Renamed.gSPFCache + "_RunSPF";
					string text3 = "";
					string text4 = "my0";
					result = 1;
					ProjectData.ClearProjectError();
					num2 = 3;
					if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(FNUsePyEngine(), "N", TextCompare: false) == 0)
					{
						text5 = Globals_Renamed.gSPFCache + ".va";
						if (General_Procedures.Save_SQL_Query(DTRCode, Globals_Renamed.MyPCDir + text5) != 0)
						{
							string text6;
							string expression;
							if (Operators.CompareString(FNUsePyEngine(), "Y", TextCompare: false) == 0)
							{
								text3 = Globals_Renamed.gSPFCache + ".spfsql";
								text4 = "spfFile";
								text6 = "SPFFileToLoad = \"" + text3 + "\"";
								expression = BuildSQL.Write_VA_Lib("va_lib_25py.lib");
							}
							else
							{
								expression = BuildSQL.Write_VA_Lib("va_lib_25.lib");
							}
							text6 = Strings.Trim(MyProject.Application.Info.DirectoryPath);
							expression = Strings.Replace(expression, "@EXEDIR@", text6, 1, -1, CompareMethod.Text);
							expression = Strings.Replace(expression, "@NOELEMENTS@", "0", 1, -1, CompareMethod.Text);
							text6 = Globals_Renamed.MyPCDir;
							expression = Strings.Replace(expression, "@PCWORKDIR@", text6, 1, -1, CompareMethod.Text);
							text6 = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\";
							expression = Strings.Replace(expression, "@LIBDIR@", text6, 1, -1, CompareMethod.Text);
							text6 = "";
							text6 = "\r\n#DATA " + text4 + " IN \"" + text5 + "\"";
							text = "SPFSQL";
							expression = Strings.Replace(expression, "@LOADMODULEDATA@", text6, 1, -1, CompareMethod.Text);
							if (Operators.CompareString(FNUsePyEngine(), "Y", TextCompare: false) == 0)
							{
								text6 = "SPFFileToLoad = \"" + text3 + "\"";
								expression = Strings.Replace(expression, "@SPFFileToLoadDATA@", text6, 1, -1, CompareMethod.Text);
							}
							else
							{
								text6 = "\r\nSPFArr(0) = \"" + text + ":1:my0;\"";
							}
							expression = Strings.Replace(expression, "@ARRAYDATA@", text6, 1, -1, CompareMethod.Text);
							text7 = "@ECHO OFF";
							if (Operators.CompareString(FNUsePyEngine(), "Y", TextCompare: false) == 0)
							{
								text7 = text7 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFPyEE_zipBuilder.py\"";
							}
							text7 = text7 + "\r\nVA /PACK=.\\" + text2 + ".exe .\\" + text2 + ".va";
							text7 = text7 + "\r\nDEL/F/Q .\\" + text5 + " >NUL";
							text7 = text7 + "\r\nDEL/F/Q .\\" + text2 + ".va >NUL";
							text7 = text7 + "\r\n" + AddDOSRetry("COPY /Y " + text2 + ".exe \"" + SQLPJob + "\"", "Copying");
							text7 = text7 + "\r\nDEL /F/Q .\\" + text2 + ".exe";
							text5 = Globals_Renamed.MyPCDir + text2 + ".va";
							if (General_Procedures.Save_SQL_Query(expression, text5) != 0)
							{
								goto IL_04db;
							}
						}
					}
					else
					{
						text5 = Globals_Renamed.gSPFCache + ".spfsql";
						if (General_Procedures.Save_SQL_Query(DTRCode, Globals_Renamed.MyPCDir + text5) != 0)
						{
							text7 = "@ECHO OFF";
							text7 = text7 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\SPFPyEE_zipBuilder.py\"";
							text7 = text7 + "\r\n\"" + Globals_Renamed.gMyPyPath + "\" \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Create_SPF_ZIP_EXE.Py\" /ZIP=\"" + text2 + ".zip\" /SPF=\"" + text5 + "\"";
							text7 = text7 + "\r\nDEL/F/Q .\\" + text5 + " >NUL";
							text7 = text7 + "\r\n" + AddDOSRetry("COPY /Y " + text2 + ".zip \"" + SQLPJob + "\"", "Copying");
							text7 = text7 + "\r\nDEL /F/Q .\\" + text2 + ".zip";
							goto IL_04db;
						}
					}
					goto IL_0640;
				}
				case 1661:
					{
						num = -1;
						switch (num2)
						{
						case 2:
						case 3:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = 0;
							goto end_IL_0001;
						}
						break;
					}
					IL_0640:
					result = 0;
					Information.Err().Clear();
					goto end_IL_0001;
					IL_04db:
					if (MyOpt == 1)
					{
						text7 += "\r\n@ECHO. *********************************************************************************";
						text7 += "\r\n@ECHO  Execute this query executable using command:";
						text7 = ((Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) != 0 && Operators.CompareString(FNUsePyEngine(), "N", TextCompare: false) != 0) ? (text7 + "\r\n@ECHO  \"" + Globals_Renamed.gMyPyPath + "\" -s -u \"" + SQLPJob + "\" /SPFDIR=\"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\"") : (text7 + "\r\n@ECHO  \"" + SQLPJob + "\" /SPFDIR=\"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\""));
						text7 += "\r\n@ECHO. *********************************************************************************";
					}
					text7 += "\r\n@ECHO ON";
					text5 = Globals_Renamed.MyPCDir + text2 + ".bat";
					if (General_Procedures.Save_SQL_Query(text7, text5) == 0)
					{
						goto IL_0640;
					}
					if (BuildSQL.RunQ(".\\" + text2 + ".bat", 0, "M") == 0)
					{
						result = 0;
					}
					goto end_IL_0001;
				}
				goto IL_06b7;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1661;
				continue;
			}
			break;
			IL_06b7:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static int Chk_SPF_Site(ref string SQLCode, int MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		bool flag = default(bool);
		string text2 = default(string);
		string f_DBType = default(string);
		string value = default(string);
		int result = default(int);
		FrmNodeSel frmNodeSel = default(FrmNodeSel);
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
				case 829:
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
						case 6:
							goto IL_0027;
						case 7:
							goto IL_0030;
						case 8:
							goto IL_0035;
						case 10:
							goto IL_004b;
						case 11:
							goto IL_007f;
						case 12:
							goto IL_0085;
						case 13:
							goto IL_008f;
						case 14:
							goto IL_0099;
						case 16:
							goto IL_00a6;
						case 17:
							goto IL_00c4;
						case 18:
							goto IL_00ca;
						case 9:
						case 15:
						case 19:
						case 20:
							goto IL_00d7;
						case 21:
							goto IL_00e5;
						case 22:
							goto IL_0103;
						case 23:
							goto IL_011c;
						case 24:
							goto IL_0126;
						case 25:
							goto IL_0132;
						case 26:
							goto IL_013e;
						case 27:
							goto IL_014f;
						case 28:
							goto IL_015e;
						case 29:
							goto IL_0169;
						case 30:
							goto IL_0175;
						case 31:
						case 32:
							goto IL_0182;
						case 33:
							goto IL_019f;
						case 35:
							goto IL_01b6;
						case 36:
							goto IL_01cc;
						case 38:
							goto IL_01f0;
						case 39:
							goto IL_0206;
						case 40:
							goto IL_020c;
						case 41:
							goto IL_022e;
						case 44:
							goto IL_0244;
						case 45:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 34:
						case 37:
						case 42:
						case 43:
						case 46:
						case 47:
						case 48:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_020c:
					num2 = 40;
					SQLCode = Strings.Replace(SQLCode, "map_nodes" + Conversions.ToString(num5), text, 1, -1, CompareMethod.Text);
					goto IL_022e;
					IL_022e:
					num2 = 41;
					num5 = checked(num5 + 1);
					if (num5 > 9)
					{
						goto end_IL_0001_3;
					}
					goto IL_020c;
					IL_0206:
					num2 = 39;
					num5 = 1;
					goto IL_020c;
					IL_01b6:
					num2 = 35;
					SQLCode = Strings.Replace(SQLCode, "<<<spf-site>>>", text, 1, -1, CompareMethod.Text);
					goto IL_01cc;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num5 = 0;
					goto IL_0015;
					IL_0015:
					num2 = 4;
					text2 = "";
					goto IL_001e;
					IL_001e:
					num2 = 5;
					f_DBType = "3";
					goto IL_0027;
					IL_0027:
					num2 = 6;
					value = "0";
					goto IL_0030;
					IL_0030:
					num2 = 7;
					result = 0;
					goto IL_0035;
					IL_0035:
					num2 = 8;
					if (MyMode == 0)
					{
						goto IL_004b;
					}
					if (MyMode == 1)
					{
						goto IL_00a6;
					}
					goto IL_00d7;
					IL_01cc:
					num2 = 36;
					SQLCode = Strings.Replace(SQLCode, "<<<spf-site-for-file-name>>>", General_Procedures.Replace_Special_Chars(text, "_"), 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_00a6:
					num2 = 16;
					if (Strings.InStr(SQLCode.ToLower(), "map_nodes") != 0)
					{
						goto IL_00c4;
					}
					goto IL_00ca;
					IL_00c4:
					num2 = 17;
					flag = true;
					goto IL_00ca;
					IL_00ca:
					num2 = 18;
					text2 = "\"Map_Nodes\"";
					goto IL_00d7;
					IL_004b:
					num2 = 10;
					if ((Strings.InStr(SQLCode.ToLower(), "<<<spf-site>>>") != 0) | (Strings.InStr(SQLCode.ToLower(), "<<<spf-site-for-file-name>>>") != 0))
					{
						goto IL_007f;
					}
					goto IL_0085;
					IL_007f:
					num2 = 11;
					flag = true;
					goto IL_0085;
					IL_0085:
					num2 = 12;
					text2 = "\"<<<spf-site>>>\"";
					goto IL_008f;
					IL_008f:
					num2 = 13;
					f_DBType = "5";
					goto IL_0099;
					IL_0099:
					num2 = 14;
					value = "2";
					goto IL_00d7;
					IL_00d7:
					num2 = 20;
					if (!flag)
					{
						goto end_IL_0001_3;
					}
					goto IL_00e5;
					IL_00e5:
					num2 = 21;
					text = Strings.Trim(MyProject.Forms.FrmMain.mnuspfsitevalue.Text);
					goto IL_0103;
					IL_0103:
					num2 = 22;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_011c;
					}
					goto IL_0182;
					IL_011c:
					num2 = 23;
					frmNodeSel = new FrmNodeSel();
					goto IL_0126;
					IL_0126:
					num2 = 24;
					frmNodeSel.f_OutNode = text;
					goto IL_0132;
					IL_0132:
					num2 = 25;
					frmNodeSel.f_DBType = f_DBType;
					goto IL_013e;
					IL_013e:
					num2 = 26;
					frmNodeSel.f_Mode = Conversions.ToInteger(value);
					goto IL_014f;
					IL_014f:
					num2 = 27;
					frmNodeSel.f_Pattern = "";
					goto IL_015e;
					IL_015e:
					num2 = 28;
					frmNodeSel.ShowDialog();
					goto IL_0169;
					IL_0169:
					num2 = 29;
					text = frmNodeSel.f_OutNode;
					goto IL_0175;
					IL_0175:
					num2 = 30;
					frmNodeSel.Dispose();
					goto IL_0182;
					IL_0182:
					num2 = 32;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						goto IL_019f;
					}
					goto IL_0244;
					IL_019f:
					num2 = 33;
					if (MyMode == 0)
					{
						goto IL_01b6;
					}
					if (MyMode != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_01f0;
					IL_0244:
					num2 = 44;
					Interaction.MsgBox("No Node selected for " + text2, MsgBoxStyle.Exclamation, "Missing Node");
					break;
					IL_01f0:
					num2 = 38;
					SQLCode = Strings.Replace(SQLCode, "map_nodes", text, 1, -1, CompareMethod.Text);
					goto IL_0206;
					end_IL_0001_2:
					break;
				}
				num2 = 45;
				result = 1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 829;
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

	public static void Add_Shares(ref ComboBox MyCmb)
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
				case 733:
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
							goto IL_0033;
						case 4:
							goto IL_005b;
						case 5:
							goto IL_0083;
						case 6:
							goto IL_00ab;
						case 7:
							goto IL_00d3;
						case 8:
							goto IL_00e7;
						case 9:
							goto IL_00fb;
						case 10:
							goto IL_0110;
						case 11:
							goto IL_0125;
						case 12:
							goto IL_013a;
						case 13:
							goto IL_014f;
						case 14:
							goto IL_0164;
						case 15:
							goto IL_0179;
						case 16:
							goto IL_018e;
						case 17:
							goto IL_01a3;
						case 18:
							goto IL_01b8;
						case 19:
							goto IL_01cd;
						case 20:
							goto IL_01e2;
						case 21:
							goto IL_01f7;
						case 22:
							goto IL_020c;
						case 23:
							goto IL_0221;
						case 24:
							goto IL_0236;
						case 25:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 26:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_020c:
					num2 = 22;
					MyCmb.Items.Add("\\\\isSHFS.intel.com\\isAnalysis$\\");
					goto IL_0221;
					IL_0221:
					num2 = 23;
					MyCmb.Items.Add("\\\\kmshfs.intel.com\\kmanalysis$\\");
					goto IL_0236;
					IL_01f7:
					num2 = 21;
					MyCmb.Items.Add("\\\\irSHFS.intel.com\\irAnalysis$\\");
					goto IL_020c;
					IL_0236:
					num2 = 24;
					MyCmb.Items.Add("\\\\nmshfs.intel.com\\nmanalysis$\\");
					break;
					IL_000b:
					num2 = 2;
					MyCmb.Items.Add("\\\\shuser-prod.intel.com\\SHProdUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_0033;
					IL_0033:
					num2 = 3;
					MyCmb.Items.Add("\\\\shuser-prodat.intel.com\\SHProdATUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_005b;
					IL_005b:
					num2 = 4;
					MyCmb.Items.Add("\\\\shuser-nsg.intel.com\\SHNSGUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_0083;
					IL_0083:
					num2 = 5;
					MyCmb.Items.Add("\\\\shuser-intg.intel.com\\SHIntgUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_00ab;
					IL_00ab:
					num2 = 6;
					MyCmb.Items.Add("\\\\shuser-dev.intel.com\\SHDevUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_00d3;
					IL_00d3:
					num2 = 7;
					MyCmb.Items.Add("<<<spf-query-dir>>>");
					goto IL_00e7;
					IL_00e7:
					num2 = 8;
					MyCmb.Items.Add("\\\\azATSHFS.intel.com\\azATAnalysis$\\");
					goto IL_00fb;
					IL_00fb:
					num2 = 9;
					MyCmb.Items.Add("\\\\azMASHFS.intel.com\\azMAAnalysis$\\");
					goto IL_0110;
					IL_0110:
					num2 = 10;
					MyCmb.Items.Add("\\\\cdATSHFS.intel.com\\cdATAnalysis$\\maoatm");
					goto IL_0125;
					IL_0125:
					num2 = 11;
					MyCmb.Items.Add("\\\\cdmashfs.intel.com\\cdmaanalysis$\\maoatm");
					goto IL_013a;
					IL_013a:
					num2 = 12;
					MyCmb.Items.Add("\\\\crATSHFS.intel.com\\crATAnalysis$\\maoatm");
					goto IL_014f;
					IL_014f:
					num2 = 13;
					MyCmb.Items.Add("\\\\kmatshfs.intel.com\\kmatanalysis$\\maoatm");
					goto IL_0164;
					IL_0164:
					num2 = 14;
					MyCmb.Items.Add("\\\\pgATSHFS.intel.com\\pgATAnalysis$\\maoatm");
					goto IL_0179;
					IL_0179:
					num2 = 15;
					MyCmb.Items.Add("\\\\pgmashfs.intel.com\\pgmaanalysis$\\maoatm");
					goto IL_018e;
					IL_018e:
					num2 = 16;
					MyCmb.Items.Add("\\\\vnmashfs.intel.com\\vnmaanalysis$\\maoatm");
					goto IL_01a3;
					IL_01a3:
					num2 = 17;
					MyCmb.Items.Add("\\\\vfSHFS.intel.com\\vfAnalysis$\\");
					goto IL_01b8;
					IL_01b8:
					num2 = 18;
					MyCmb.Items.Add("\\\\azSHFS.intel.com\\azAnalysis$\\");
					goto IL_01cd;
					IL_01cd:
					num2 = 19;
					MyCmb.Items.Add("\\\\cdSHFS.intel.com\\cdAnalysis$\\");
					goto IL_01e2;
					IL_01e2:
					num2 = 20;
					MyCmb.Items.Add("\\\\dlSHFS.intel.com\\dlAnalysis$\\");
					goto IL_01f7;
					end_IL_0001_2:
					break;
				}
				num2 = 25;
				MyCmb.Items.Add("\\\\orSHFS.intel.com\\orAnalysis$\\");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 733;
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

	public static string Get_Special_Util(string MyTag, string MyMode, string MyData)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				string left;
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
						case 4:
							goto IL_001a;
						case 6:
							goto IL_004d;
						case 7:
							goto IL_0064;
						case 8:
							goto IL_0078;
						case 9:
							goto IL_008b;
						case 12:
							goto IL_00aa;
						case 13:
							goto IL_00b4;
						case 14:
							goto IL_00c9;
						case 15:
							goto IL_00e7;
						case 16:
							goto IL_0100;
						case 18:
							goto IL_0108;
						case 19:
							goto IL_012b;
						case 21:
							goto IL_0138;
						case 22:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 10:
						case 11:
						case 17:
						case 20:
						case 23:
						case 24:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004d:
					num2 = 6;
					if (Operators.CompareString(MyData, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0064;
					IL_0064:
					num2 = 7;
					text = General_Procedures.Get_UI("s->" + MyTag);
					goto IL_0078;
					IL_0138:
					num2 = 21;
					text = Strings.Replace(text, "\"", "'", 1, -1, CompareMethod.Text);
					break;
					IL_0078:
					num2 = 8;
					text2 = Strings.Replace(text, "<>", MyData, 1, -1, CompareMethod.Text);
					goto IL_008b;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					num5 = 0;
					goto IL_001a;
					IL_001a:
					num2 = 4;
					left = Strings.UCase(Strings.Trim(MyMode));
					if (Operators.CompareString(left, "S", TextCompare: false) == 0)
					{
						goto IL_004d;
					}
					if (Operators.CompareString(left, "I", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00aa;
					IL_008b:
					num2 = 9;
					text2 = Strings.Replace(text2, "'", "\"", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_00aa:
					num2 = 12;
					text3 = "";
					goto IL_00b4;
					IL_00b4:
					num2 = 13;
					text3 = General_Procedures.Get_UI("u->" + MyTag);
					goto IL_00c9;
					IL_00c9:
					num2 = 14;
					text = Interaction.InputBox(text3 + ". Enter NONE to Initialize", "Utility Command", MyData);
					goto IL_00e7;
					IL_00e7:
					num2 = 15;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0100;
					}
					goto IL_0108;
					IL_0100:
					num2 = 16;
					text2 = MyData;
					goto end_IL_0001_3;
					IL_0108:
					num2 = 18;
					if (Operators.CompareString(Strings.Trim(Strings.UCase(text)), "NONE", TextCompare: false) == 0)
					{
						goto IL_012b;
					}
					goto IL_0138;
					IL_012b:
					num2 = 19;
					text2 = "NONE";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 22;
				text2 = text;
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
		return text2;
	}

	public static void Strip_Col_DT(ref string MyCol, ref string MyDT)
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
				case 164:
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
							goto IL_0020;
						case 5:
							goto IL_0029;
						case 6:
							goto IL_0035;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0020:
					num2 = 4;
					MyDT = "";
					goto IL_0029;
					IL_0029:
					num2 = 5;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0035;
					IL_000f:
					num2 = 3;
					num5 = Strings.InStrRev(MyCol, "(");
					goto IL_0020;
					IL_0035:
					num2 = 6;
					MyDT = Strings.UCase(Strings.Trim(Strings.Mid(MyCol + " ", checked(num5 + 1), 1)));
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				MyCol = Strings.Trim(Strings.Mid(MyCol, 1, checked(num5 - 1)));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 164;
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

	public static string Get_First_Line_Web(string MyURL)
	{
		string result = "";
		string text = "";
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(MyURL);
		try
		{
			httpWebRequest.MaximumAutomaticRedirections = 4;
			httpWebRequest.MaximumResponseHeadersLength = 4;
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			try
			{
				Stream responseStream = httpWebResponse.GetResponseStream();
				try
				{
					StreamReader streamReader = new StreamReader(responseStream);
					try
					{
						text = streamReader.ReadLine();
						if (text.Length > 0)
						{
							result = text;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Interaction.MsgBox("Error reading header line of: " + MyURL + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Read Error");
						ProjectData.ClearProjectError();
					}
					finally
					{
						streamReader.Close();
						streamReader.Dispose();
						streamReader = null;
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Interaction.MsgBox("Error reading: " + MyURL + " (" + ex4.Message + ")", MsgBoxStyle.Exclamation, "Read Error");
					ProjectData.ClearProjectError();
				}
				finally
				{
					responseStream.Close();
					responseStream.Dispose();
					responseStream = null;
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Interaction.MsgBox("Error accessing: " + MyURL + " (" + ex6.Message + ")", MsgBoxStyle.Exclamation, "Access Error");
				ProjectData.ClearProjectError();
			}
			finally
			{
				httpWebResponse.Close();
				httpWebResponse = null;
			}
		}
		catch (Exception ex7)
		{
			ProjectData.SetProjectError(ex7);
			Exception ex8 = ex7;
			Interaction.MsgBox("Error opening: " + MyURL + " (" + ex8.Message + ")", MsgBoxStyle.Exclamation, "Open Error");
			ProjectData.ClearProjectError();
		}
		finally
		{
		}
		httpWebRequest = null;
		return result;
	}

	public static void HG_GrdList_KP(ref DataGridView GrdList, ref ComboBox CmbTxt, ref KeyPressEventArgs e, bool chkUp, ref bool CmbBoxKey, ref short f_TrueRow, ref short f_ROW, ref short f_COL)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string myChar = default(string);
		short num5 = default(short);
		short num6 = default(short);
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
					case 459:
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
								goto IL_001b;
							case 4:
								goto IL_0024;
							case 7:
								goto IL_0047;
							case 8:
								goto IL_004d;
							case 9:
								goto IL_005d;
							case 10:
								goto IL_0067;
							case 11:
							case 12:
								goto IL_0073;
							case 14:
								goto IL_0089;
							case 15:
								goto IL_0097;
							case 16:
								goto IL_00a9;
							case 17:
								goto IL_00b8;
							case 18:
								goto IL_00c0;
							case 19:
								goto IL_00e5;
							case 13:
							case 20:
							case 21:
							case 22:
								goto IL_010d;
							case 23:
								goto IL_011f;
							case 24:
								goto IL_012b;
							case 25:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 5:
							case 6:
							case 26:
							case 27:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_010d:
						num2 = 22;
						GridModule.Combo_Click(ref GrdList, ref CmbTxt, ref f_ROW, ref f_COL, 1, myChar);
						goto IL_011f;
						IL_011f:
						num2 = 23;
						CmbTxt.SelectionLength = 0;
						goto IL_012b;
						IL_00e5:
						num2 = 19;
						GrdList.CurrentCell = GrdList.Rows[f_TrueRow].Cells[0];
						goto IL_010d;
						IL_012b:
						num2 = 24;
						CmbTxt.SelectionStart = Strings.Len(CmbTxt.Text);
						break;
						IL_000b:
						num2 = 2;
						num5 = (short)Strings.Asc(e.KeyChar);
						goto IL_001b;
						IL_001b:
						num2 = 3;
						myChar = "";
						goto IL_0024;
						IL_0024:
						num2 = 4;
						if (GrdList.CurrentRow.Index < 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0047;
						IL_0047:
						num2 = 7;
						CmbBoxKey = true;
						goto IL_004d;
						IL_004d:
						num2 = 8;
						if (num5 != 13)
						{
							goto IL_005d;
						}
						goto IL_0089;
						IL_005d:
						num2 = 9;
						if (chkUp)
						{
							goto IL_0067;
						}
						goto IL_0073;
						IL_0067:
						num2 = 10;
						num5 = General_Procedures.ToUpper(num5);
						goto IL_0073;
						IL_0073:
						num2 = 12;
						myChar = Conversions.ToString(Strings.Chr(num5));
						goto IL_010d;
						IL_0089:
						num2 = 14;
						if (num5 == 13)
						{
							goto IL_0097;
						}
						goto IL_010d;
						IL_0097:
						num2 = 15;
						num6 = (short)GrdList.CurrentCell.RowIndex;
						goto IL_00a9;
						IL_00a9:
						num2 = 16;
						if (f_TrueRow == -1)
						{
							goto IL_00b8;
						}
						goto IL_00c0;
						IL_00b8:
						num2 = 17;
						f_TrueRow = num6;
						goto IL_00c0;
						IL_00c0:
						num2 = 18;
						if ((f_TrueRow != GrdList.RowCount - 1) & (f_TrueRow != num6))
						{
							goto IL_00e5;
						}
						goto IL_010d;
						end_IL_0001_2:
						break;
					}
					num2 = 25;
					f_TrueRow = -1;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 459;
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

	public static void Set_Label_Ind(short MyMode, int MyPos, string MyLabel, ref Label lbli, string IsChecked)
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
				short num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 420:
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
							goto IL_001f;
						case 5:
							goto IL_002e;
						case 7:
							goto IL_004b;
						case 8:
							goto IL_008d;
						case 9:
							goto IL_009c;
						case 11:
							goto IL_00ba;
						case 12:
							goto IL_00eb;
						case 13:
							goto IL_00fb;
						case 15:
							goto IL_0117;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 6:
						case 10:
						case 14:
						case 17:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ba:
					num2 = 11;
					if (Operators.CompareString(Strings.Mid(MyLabel, MyPos, 1), "Y", TextCompare: false) == 0 && Operators.CompareString(IsChecked, "N", TextCompare: false) == 0)
					{
						goto IL_00eb;
					}
					goto IL_0117;
					IL_002e:
					num2 = 5;
					lbli.Font = new Font(lbli.Font, FontStyle.Regular);
					goto end_IL_0001_3;
					IL_009c:
					num2 = 9;
					lbli.Font = new Font(lbli.Font, FontStyle.Bold);
					goto end_IL_0001_3;
					IL_00eb:
					num2 = 12;
					lbli.ForeColor = Color.Black;
					goto IL_00fb;
					IL_000c:
					num2 = 2;
					num5 = MyMode;
					if (num5 == 0)
					{
						goto IL_001f;
					}
					if (num5 != 1)
					{
						goto end_IL_0001_3;
					}
					goto IL_004b;
					IL_00fb:
					num2 = 13;
					lbli.Font = new Font(lbli.Font, FontStyle.Bold);
					goto end_IL_0001_3;
					IL_004b:
					num2 = 7;
					if (Operators.CompareString(Strings.Mid(MyLabel, MyPos, 1), "Y", TextCompare: false) == 0 && (Operators.CompareString(IsChecked, "Y", TextCompare: false) == 0 || Operators.CompareString(IsChecked, " ", TextCompare: false) == 0))
					{
						goto IL_008d;
					}
					goto IL_00ba;
					IL_0117:
					num2 = 15;
					lbli.ForeColor = Color.Black;
					break;
					IL_001f:
					num2 = 4;
					lbli.ForeColor = Color.Black;
					goto IL_002e;
					IL_008d:
					num2 = 8;
					lbli.ForeColor = Color.DarkRed;
					goto IL_009c;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				lbli.Font = new Font(lbli.Font, FontStyle.Regular);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 420;
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

	public static void Set_Lbl_Color(short MyMode, string MyLabel, ref Label lbl4, ref Label lbl5, ref Label lbl6, ref Label lbl7, ref Label lbl8, ref Label lbl9, string IsChecked = "")
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
				case 239:
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
							goto IL_0032;
						case 5:
							goto IL_0047;
						case 6:
							goto IL_005c;
						case 7:
							goto IL_0072;
						case 8:
							goto IL_0088;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_005c:
					num2 = 6;
					Set_Label_Ind(MyMode, 3, MyLabel, ref lbl6, Strings.Mid(IsChecked, 3, 1));
					goto IL_0072;
					IL_0072:
					num2 = 7;
					Set_Label_Ind(MyMode, 4, MyLabel, ref lbl7, Strings.Mid(IsChecked, 4, 1));
					goto IL_0088;
					IL_0047:
					num2 = 5;
					Set_Label_Ind(MyMode, 2, MyLabel, ref lbl5, Strings.Mid(IsChecked, 2, 1));
					goto IL_005c;
					IL_0088:
					num2 = 8;
					Set_Label_Ind(MyMode, 5, MyLabel, ref lbl8, Strings.Mid(IsChecked, 5, 1));
					break;
					IL_000b:
					num2 = 2;
					if (Strings.Len(IsChecked) < 6)
					{
						goto IL_001b;
					}
					goto IL_0032;
					IL_001b:
					num2 = 3;
					IsChecked = Strings.Mid(IsChecked + "      ", 1, 6);
					goto IL_0032;
					IL_0032:
					num2 = 4;
					Set_Label_Ind(MyMode, 1, MyLabel, ref lbl4, Strings.Mid(IsChecked, 1, 1));
					goto IL_0047;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				Set_Label_Ind(MyMode, 6, MyLabel, ref lbl9, Strings.Mid(IsChecked, 6, 1));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 239;
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

	public static void TempSetSHSave(string MyMode, ref string TmpMidas)
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
				string left;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 247:
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
							goto IL_0034;
						case 5:
							goto IL_003d;
						case 6:
							goto IL_005d;
						case 7:
						case 8:
							goto IL_006b;
						case 10:
							goto IL_0076;
						case 11:
							goto IL_0094;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 9:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0034:
					num2 = 4;
					TmpMidas = Globals_Renamed.gMidasDriver;
					goto IL_003d;
					IL_003d:
					num2 = 5;
					if (!LikeOperator.LikeString(Strings.UCase(Globals_Renamed.gMidasDriver), "UBER*", CompareMethod.Binary))
					{
						goto IL_005d;
					}
					goto IL_006b;
					IL_0094:
					num2 = 11;
					Globals_Renamed.gMidasDriver = TmpMidas;
					break;
					IL_005d:
					num2 = 6;
					Globals_Renamed.gMidasDriver = "UBER";
					goto IL_006b;
					IL_000c:
					num2 = 2;
					left = Strings.UCase(MyMode);
					if (Operators.CompareString(left, "S", TextCompare: false) == 0)
					{
						goto IL_0034;
					}
					if (Operators.CompareString(left, "R", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0076;
					IL_006b:
					num2 = 8;
					Globals_Renamed.gISSHSave = true;
					goto end_IL_0001_3;
					IL_0076:
					num2 = 10;
					if (Operators.CompareString(Strings.Trim(TmpMidas), "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0094;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				Globals_Renamed.gISSHSave = false;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 247;
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

	public static bool ChkShguiBase(ref ComboBox TxtSHGuiDir)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool result = default(bool);
		string value = default(string);
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
				case 303:
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
							goto IL_002c;
						case 5:
							goto IL_0054;
						case 6:
							goto IL_0074;
						case 7:
							goto IL_009b;
						case 9:
							goto IL_00a2;
						case 10:
							goto IL_00c7;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_009b:
					num2 = 7;
					result = true;
					goto end_IL_0001_3;
					IL_00a2:
					num2 = 9;
					if (Operators.CompareString(Strings.UCase(TxtSHGuiDir.Text), Strings.UCase(value), TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00c7;
					IL_0074:
					num2 = 6;
					if (Operators.CompareString(Strings.UCase(TxtSHGuiDir.Text), Strings.UCase(Globals_Renamed.gBaseSHGUIDir), TextCompare: false) == 0)
					{
						goto IL_009b;
					}
					goto IL_00a2;
					IL_00c7:
					num2 = 10;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					value = "\\\\SHUser-Intg.intel.com\\SHIntgUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\";
					goto IL_0028;
					IL_0028:
					num2 = 3;
					result = false;
					goto IL_002c;
					IL_002c:
					num2 = 4;
					if (Operators.CompareString(Strings.Right(Strings.Trim(TxtSHGuiDir.Text), 1), "\\", TextCompare: false) != 0)
					{
						goto IL_0054;
					}
					goto IL_0074;
					IL_0054:
					num2 = 5;
					TxtSHGuiDir.Text = Strings.Trim(TxtSHGuiDir.Text) + "\\";
					goto IL_0074;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = General_Procedures.MakeDirectory(TxtSHGuiDir.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 303;
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

	public static void Init_SH_Controls(ref ComboBox cmbQueue, ref ComboBox TxtSHGUIDir)
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
				case 303:
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
							goto IL_0023;
						case 4:
							goto IL_004b;
						case 5:
							goto IL_0073;
						case 6:
							goto IL_009b;
						case 7:
							goto IL_00c3;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0073:
					num2 = 5;
					TxtSHGUIDir.Items.Add("\\\\SHUser-NSG.intel.com\\SHNSGUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_009b;
					IL_009b:
					num2 = 6;
					TxtSHGUIDir.Items.Add("\\\\SHUser-Intg.intel.com\\SHIntgUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_00c3;
					IL_004b:
					num2 = 4;
					TxtSHGUIDir.Items.Add("\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_0073;
					IL_00c3:
					num2 = 7;
					TxtSHGUIDir.Items.Add("\\\\SHUser-Dev.intel.com\\SHDevUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					break;
					IL_000b:
					num2 = 2;
					General_Procedures.Load_Combo_File(Globals_Renamed.MySchemaDir + "\\Job.Queues", ref cmbQueue);
					goto IL_0023;
					IL_0023:
					num2 = 3;
					TxtSHGUIDir.Items.Add("\\\\SHUser-Prod.intel.com\\SHProdUser$\\" + Strings.LCase(Globals_Renamed.gWinuser) + "\\");
					goto IL_004b;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Init_SH_Controls2(ref TxtSHGUIDir, Globals_Renamed.gSHGUIDir);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 303;
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

	public static void Init_SH_Controls2(ref ComboBox TxtSHGUIDir, string MyDir)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		bool flag = default(bool);
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
				case 193:
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
							goto IL_0026;
						case 5:
							goto IL_0042;
						case 6:
							goto IL_0047;
						case 8:
						case 9:
							goto IL_0056;
						case 7:
						case 10:
							goto IL_0062;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0047:
					num2 = 6;
					TxtSHGUIDir.SelectedIndex = num5;
					goto IL_0062;
					IL_0062:
					num2 = 10;
					if (flag)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0042:
					num2 = 5;
					flag = true;
					goto IL_0047;
					IL_0056:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_005d;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num6 = checked(TxtSHGUIDir.Items.Count - 1);
					num5 = 0;
					goto IL_005d;
					IL_005d:
					if (num5 <= num6)
					{
						goto IL_0026;
					}
					goto IL_0062;
					IL_0026:
					num2 = 4;
					if (Operators.ConditionalCompareObjectEqual(MyDir, TxtSHGUIDir.Items[num5], TextCompare: false))
					{
						goto IL_0042;
					}
					goto IL_0056;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				TxtSHGUIDir.SelectedIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 193;
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

	public static void GetSHGUIFile(string TmpFile, string TmpDir)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		string left = default(string);
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
					case 776:
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
								goto IL_002f;
							case 6:
								goto IL_0040;
							case 7:
								goto IL_004d;
							case 8:
							case 9:
							case 10:
								goto IL_0066;
							case 11:
								goto IL_0097;
							case 12:
								goto IL_00af;
							case 13:
							case 14:
								goto IL_00cb;
							case 16:
								goto IL_00ec;
							case 17:
								goto IL_0104;
							case 18:
							case 19:
								goto IL_0120;
							case 20:
								goto IL_013a;
							case 21:
								goto IL_0191;
							case 22:
								goto IL_01b6;
							case 23:
								goto IL_01c4;
							case 25:
								goto IL_01ef;
							case 24:
							case 26:
							case 27:
								goto IL_01f6;
							case 28:
								goto IL_0204;
							case 30:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 15:
							case 29:
							case 31:
							case 32:
							case 33:
							case 34:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_01f6:
						num2 = 27;
						if (num5 == 0)
						{
							break;
						}
						goto IL_0204;
						IL_0204:
						num2 = 28;
						MyProject.Forms.FrmMain.CMDialog1Save.FileName = Strings.Trim(Strings.Mid(MyProject.Forms.FrmMain.CMDialog1Save.FileName, 1, num5)) + Strings.Trim(text);
						goto end_IL_0001_3;
						IL_01ef:
						num2 = 25;
						text = TmpFile;
						goto IL_01f6;
						IL_0097:
						num2 = 11;
						if (Operators.CompareString(left, ".zip", TextCompare: false) == 0)
						{
							goto IL_00af;
						}
						goto IL_00cb;
						IL_000b:
						num2 = 2;
						left = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num5 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						if (Operators.CompareString(TmpFile, "", TextCompare: false) != 0)
						{
							goto IL_002f;
						}
						goto IL_0066;
						IL_002f:
						num2 = 5;
						num5 = Strings.InStrRev(TmpFile, ".");
						goto IL_0040;
						IL_0040:
						num2 = 6;
						if (num5 > 0)
						{
							goto IL_004d;
						}
						goto IL_0066;
						IL_004d:
						num2 = 7;
						left = Strings.LCase(Strings.Trim(Strings.Mid(TmpFile, num5)));
						goto IL_0066;
						IL_0066:
						num2 = 10;
						if (Operators.CompareString(Globals_Renamed.gSHType, "VA", TextCompare: false) == 0 || Operators.CompareString(FNUsePyEngine(), "N", TextCompare: false) == 0)
						{
							goto IL_0097;
						}
						goto IL_00ec;
						IL_00af:
						num2 = 12;
						TmpFile = Strings.Mid(TmpFile, 1, num5 - 1) + ".exe";
						goto IL_00cb;
						IL_00ec:
						num2 = 16;
						if (Operators.CompareString(left, ".exe", TextCompare: false) == 0)
						{
							goto IL_0104;
						}
						goto IL_0120;
						IL_0104:
						num2 = 17;
						TmpFile = Strings.Mid(TmpFile, 1, num5 - 1) + ".zip";
						goto IL_0120;
						IL_0120:
						num2 = 19;
						FileOpenSave("S", TmpFile, "zip", "Create Batch Job in this ScriptHost Folder. No spaces in file name", TmpDir);
						goto IL_013a;
						IL_013a:
						num2 = 20;
						if (Operators.CompareString(Strings.UCase(MyProject.Forms.FrmMain.CMDialog1Save.FileName), "CANCEL", TextCompare: false) == 0 || Strings.InStr(MyProject.Forms.FrmMain.CMDialog1Save.FileName, " ") == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0191;
						IL_00cb:
						num2 = 14;
						FileOpenSave("S", TmpFile, "exe", "Create Batch Job in this ScriptHost Folder", TmpDir);
						goto end_IL_0001_3;
						IL_0191:
						num2 = 21;
						num5 = Strings.InStrRev(MyProject.Forms.FrmMain.CMDialog1Save.FileName, "\\");
						goto IL_01b6;
						IL_01b6:
						num2 = 22;
						if (num5 != 0)
						{
							goto IL_01c4;
						}
						goto IL_01ef;
						IL_01c4:
						num2 = 23;
						text = Strings.Trim(Strings.Mid(MyProject.Forms.FrmMain.CMDialog1Save.FileName, num5 + 1));
						goto IL_01f6;
						end_IL_0001_2:
						break;
					}
					num2 = 30;
					MyProject.Forms.FrmMain.CMDialog1Save.FileName = text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 776;
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

	public static int Find_Combo_Item(ref ComboBox cmbType, string MyType, int NotFound)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int result = default(int);
		int num = default(int);
		int num3 = default(int);
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
					result = NotFound;
					goto IL_0006;
				case 164:
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
							goto IL_0026;
						case 5:
							goto end_IL_0001_2;
						case 7:
						case 8:
							goto IL_0060;
						default:
							goto end_IL_0001;
						case 6:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0068:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0026;
					IL_0026:
					num2 = 4;
					if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(cmbType.Items[num5], null, "Text", new object[0], null, null, null), MyType, TextCompare: false))
					{
						break;
					}
					goto IL_0060;
					IL_000f:
					num2 = 3;
					num6 = checked(cmbType.Items.Count - 1);
					num5 = 0;
					goto IL_0068;
					IL_0060:
					num2 = 8;
					num5 = checked(num5 + 1);
					goto IL_0068;
					IL_0006:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = num5;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 164;
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

	public static void Load_HTTP_Combo(ref ComboBox MyCmb)
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
				case 358:
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
							goto IL_001f;
						case 4:
							goto IL_0033;
						case 5:
							goto IL_0047;
						case 6:
							goto IL_005b;
						case 7:
							goto IL_006f;
						case 8:
							goto IL_0083;
						case 9:
							goto IL_0097;
						case 10:
							goto IL_00ac;
						case 11:
							goto IL_00c1;
						case 12:
							goto IL_00d6;
						case 13:
							goto IL_00eb;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00c1:
					num2 = 11;
					MyCmb.Items.Add("http://isshweb.intel.com/isAnalysis$/");
					goto IL_00d6;
					IL_00d6:
					num2 = 12;
					MyCmb.Items.Add("http://kmshweb.intel.com/kmanalysis$/");
					goto IL_00eb;
					IL_00ac:
					num2 = 10;
					MyCmb.Items.Add("http://irshweb.intel.com/irAnalysis$/");
					goto IL_00c1;
					IL_00eb:
					num2 = 13;
					MyCmb.Items.Add("http://nmshweb.intel.com/nmanalysis$/");
					break;
					IL_000b:
					num2 = 2;
					MyCmb.Items.Add("http://azatshweb.intel.com/AzatAnalysis$/MAOATM/");
					goto IL_001f;
					IL_001f:
					num2 = 3;
					MyCmb.Items.Add("http://azatshweb.intel.com/AzatAnalysis$/DETD_MAO/");
					goto IL_0033;
					IL_0033:
					num2 = 4;
					MyCmb.Items.Add("http://pgatshweb.intel.com/pgatAnalysis$/MAOATM/");
					goto IL_0047;
					IL_0047:
					num2 = 5;
					MyCmb.Items.Add("http://cdatshweb.intel.com/cdatAnalysis$/MAOATM/");
					goto IL_005b;
					IL_005b:
					num2 = 6;
					MyCmb.Items.Add("http://kmatshweb.intel.com/kmatanalysis$/MAOATM");
					goto IL_006f;
					IL_006f:
					num2 = 7;
					MyCmb.Items.Add("http://vnatshweb.intel.com/vnatAnalysis$/MAOATM/");
					goto IL_0083;
					IL_0083:
					num2 = 8;
					MyCmb.Items.Add("http://azshweb.intel.com/AzAnalysis$/");
					goto IL_0097;
					IL_0097:
					num2 = 9;
					MyCmb.Items.Add("http://dlshweb.intel.com/dlAnalysis$/");
					goto IL_00ac;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				MyCmb.Items.Add("http://orshweb.intel.com/orAnalysis$/");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 358;
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

	public static string IsMap_Nodes(string MyData)
	{
		string result = "0";
		MyData = Strings.Trim(Strings.LCase(MyData));
		if (Operators.CompareString(MyData, "map_nodes", TextCompare: false) == 0)
		{
			result = "";
		}
		else if ((Operators.CompareString(Strings.Mid(MyData + "         ", 1, 9), "map_nodes", TextCompare: false) == 0) & (Strings.Len(MyData) == 10))
		{
			string text = Strings.Mid(MyData, 10);
			if ((Operators.CompareString(text, "1", TextCompare: false) >= 0) & (Operators.CompareString(text, "9", TextCompare: false) <= 0))
			{
				result = text;
			}
		}
		return result;
	}

	public static bool VerifyNodes(ref string NodeList0)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		bool result;
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
					errsource = "BuildForm - VerifyNodes";
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					string text = "";
					string text2 = "";
					bool flag = false;
					int num6 = 0;
					string str = NodeList0;
					NodeList0 = Strings.Replace(NodeList0, "ALL.*MMS*", "PG.MMS,ATD.MMS,KM.MMS,CD.MMS,VN.MMS", 1, -1, CompareMethod.Text);
					result = true;
					str = Strings.UCase(Strings.Trim(str));
					if (LikeOperator.LikeString(str, "*<<<*>>>*", CompareMethod.Binary))
					{
						goto end_IL_0001;
					}
					if (Strings.InStr(str, "MIDAS") != 0 && Strings.InStr(str, ",") != 0)
					{
						Interaction.MsgBox("You must select one MIDAS Node!", MsgBoxStyle.Exclamation, "Node Error");
						result = false;
						goto end_IL_0001;
					}
					string[] array = Strings.Split(str, ",");
					num3 = Information.UBound(array);
					int num7 = num3;
					for (num4 = 0; num4 <= num7; num4 = checked(num4 + 1))
					{
						text = Strings.UCase(Strings.Trim(array[num4]));
						if (Operators.CompareString(IsMap_Nodes(text), "0", TextCompare: false) != 0)
						{
							continue;
						}
						flag = false;
						int nonodes = Globals_Renamed.nonodes;
						for (num5 = 0; num5 <= nonodes; num5 = checked(num5 + 1))
						{
							text2 = Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].Name));
							if (Operators.CompareString(text2, text, TextCompare: false) == 0)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							num6 = (int)Interaction.MsgBox("You selected an unknown node (" + text + ").  This is ok for privately defined nodes (e.g., Oracle nodes in a tnsnames.ora file). Click OK to continue or Cancel to correct the node.", MsgBoxStyle.OkCancel | MsgBoxStyle.Question, "Unknown Node");
							if (num6 == 2)
							{
								result = false;
								break;
							}
						}
					}
					array = null;
					goto end_IL_0001_2;
				}
				case 468:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
						Information.Err().Clear();
						result = false;
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_020a;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 468;
				continue;
			}
			break;
			IL_020a:
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static int Copy_TNSNames(string MyDir, bool IsQuiet, ref string MyErr)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int result;
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
					result = 0;
					MyErr = "";
					string text = "";
					string text2 = "";
					string text3 = "";
					int num3 = 0;
					text = Globals_Renamed.MySchemaDir + "\\TNSNAMES.ORA";
					text2 = Strings.Trim(MyDir) + "TNSNAMES.ORA";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text3 = "SQLNET.AUTHENTICATION_SERVICES= (NTS,KERBEROS5PRE,KERBEROS5,BEQ)";
					text3 = text3 + "\r\nSQLNET.KERBEROS5_CONF = " + Globals_Renamed.MySchemaDir + "\\krb5.conf";
					text3 += "\r\nSQLNET.KERBEROS5_CONF_MIT=TRUE";
					text3 += "\r\nSQLNET.KERBEROS5_CC_NAME = OSMSFT://";
					text3 += "\r\nSQLNET.AUTHENTICATION_KERBEROS5_SERVICE=oracle";
					text3 += "\r\nSQLNET.FALLBACK_AUTHENTICATION=TRUE";
					if (General_Procedures.Save_SQL_Query(text3, MyDir + "sqlnet.ora") == 0)
					{
						break;
					}
					text = Globals_Renamed.MySchemaDir + "\\drilldown.js";
					text2 = Strings.Trim(MyDir) + "drilldown.js";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\table.js";
					text2 = Strings.Trim(MyDir) + "table.js";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\plus.gif";
					text2 = Strings.Trim(MyDir) + "plus.gif";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\minus.gif";
					text2 = Strings.Trim(MyDir) + "minus.gif";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\DoJMP.va";
					text2 = Strings.Trim(MyDir) + "DoJMP.va";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\DoJMP.py";
					text2 = Strings.Trim(MyDir) + "DoJMP.py";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\intelcachain.pem";
					text2 = Strings.Trim(MyDir) + "intelcachain.pem";
					Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					text = Globals_Renamed.MySchemaDir + "\\sqlpathfinder_style_1.css";
					text2 = Strings.Trim(MyDir) + "sqlpathfinder_style_1.css";
					if (MyProject.Computer.FileSystem.FileExists(text))
					{
						Microsoft.VisualBasic.FileSystem.FileCopy(text, text2);
					}
					goto end_IL_0001;
				}
				case 711:
					num = -1;
					switch (num2)
					{
					case 2:
						break;
					default:
						goto end_IL_0001_2;
					}
					break;
				}
				if (IsQuiet)
				{
					MyErr = General_Procedures.Get_UI("errsql11");
				}
				else
				{
					Interaction.MsgBox("Error Copying files from " + Globals_Renamed.MySchemaDir + " to " + MyDir + ". (" + Conversion.ErrorToString() + "). " + General_Procedures.Get_UI("errsql11"), MsgBoxStyle.Critical, "Copy Error");
				}
				Information.Err().Clear();
				result = 1;
				break;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 711;
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

	public static void FileOpenSave(string MyMode, string iniFile, string fExt, string fTitle, string IniDir)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string filter = default(string);
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
				case 3146:
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
							goto IL_001d;
						case 5:
							goto IL_0034;
						case 6:
							goto IL_004c;
						case 7:
						case 8:
							goto IL_0066;
						case 9:
							goto IL_007e;
						case 10:
							goto IL_0097;
						case 11:
							goto IL_00af;
						case 12:
							goto IL_00c8;
						case 13:
						case 14:
							goto IL_00e3;
						case 15:
							goto IL_00fc;
						case 16:
							goto IL_0119;
						case 18:
							goto IL_013a;
						case 19:
							goto IL_0154;
						case 17:
						case 20:
						case 21:
							goto IL_0170;
						case 23:
							goto IL_0505;
						case 25:
							goto IL_0514;
						case 27:
							goto IL_0523;
						case 29:
							goto IL_0532;
						case 31:
							goto IL_0541;
						case 33:
							goto IL_0550;
						case 35:
							goto IL_055f;
						case 37:
							goto IL_056e;
						case 39:
							goto IL_057d;
						case 41:
							goto IL_058c;
						case 43:
							goto IL_059b;
						case 45:
							goto IL_05aa;
						case 47:
							goto IL_05b9;
						case 49:
							goto IL_05c8;
						case 51:
							goto IL_05d7;
						case 53:
							goto IL_05e6;
						case 55:
							goto IL_05f5;
						case 57:
							goto IL_0604;
						case 59:
							goto IL_0613;
						case 61:
							goto IL_0622;
						case 63:
							goto IL_0631;
						case 64:
							goto IL_063a;
						case 65:
							goto IL_0657;
						case 67:
							goto IL_0677;
						case 69:
							goto IL_0683;
						case 22:
						case 24:
						case 26:
						case 28:
						case 30:
						case 32:
						case 34:
						case 36:
						case 38:
						case 40:
						case 42:
						case 44:
						case 46:
						case 48:
						case 50:
						case 52:
						case 54:
						case 56:
						case 58:
						case 60:
						case 62:
						case 66:
						case 68:
						case 70:
						case 71:
							goto IL_06cf;
						case 72:
							goto IL_06e8;
						case 73:
							goto IL_0701;
						case 74:
							goto IL_0719;
						case 75:
							goto IL_0732;
						case 76:
						case 77:
							goto IL_074e;
						case 79:
							goto IL_078d;
						case 80:
							goto IL_07a5;
						case 81:
							goto IL_07be;
						case 82:
							goto IL_07d7;
						case 84:
							goto IL_07f4;
						case 85:
							goto IL_080d;
						case 86:
							goto IL_0826;
						case 83:
						case 87:
						case 88:
							goto IL_0840;
						case 89:
							goto IL_0858;
						case 90:
						case 91:
							goto IL_0877;
						case 92:
							goto IL_0897;
						case 95:
							goto IL_08bc;
						case 96:
							goto IL_08d7;
						case 97:
							goto IL_08f0;
						case 98:
							goto IL_0909;
						case 99:
							goto IL_0922;
						case 100:
							goto IL_093a;
						case 101:
							goto IL_0957;
						case 104:
							goto IL_097d;
						case 105:
							goto IL_0996;
						case 106:
							goto IL_09af;
						case 107:
							goto IL_09c8;
						case 108:
							goto IL_09e0;
						case 109:
							goto IL_09fd;
						case 102:
						case 103:
						case 110:
						case 111:
						case 112:
							goto IL_0a1d;
						case 113:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 78:
						case 93:
						case 94:
						case 114:
						case 115:
						case 116:
						case 117:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0858:
					num2 = 89;
					MyProject.Forms.FrmMain.CMDialog1Open.Title = "Open";
					goto IL_0877;
					IL_0877:
					num2 = 91;
					if (MyProject.Forms.FrmMain.CMDialog1Open.ShowDialog() != DialogResult.Cancel)
					{
						goto end_IL_0001_3;
					}
					goto IL_0897;
					IL_0840:
					num2 = 88;
					if (Operators.CompareString(fTitle, "", TextCompare: false) == 0)
					{
						goto IL_0858;
					}
					goto IL_0877;
					IL_0897:
					num2 = 92;
					MyProject.Forms.FrmMain.CMDialog1Open.FileName = "CANCEL";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					filter = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					fExt = Strings.LCase(fExt);
					goto IL_001d;
					IL_001d:
					num2 = 4;
					if (Operators.CompareString(fExt, "all", TextCompare: false) != 0)
					{
						goto IL_0034;
					}
					goto IL_0066;
					IL_0034:
					num2 = 5;
					MyProject.Forms.FrmMain.CMDialog1Open.DefaultExt = fExt;
					goto IL_004c;
					IL_004c:
					num2 = 6;
					MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = fExt;
					goto IL_0066;
					IL_0066:
					num2 = 8;
					MyProject.Forms.FrmMain.CMDialog1Open.FileName = iniFile;
					goto IL_007e;
					IL_007e:
					num2 = 9;
					MyProject.Forms.FrmMain.CMDialog1Save.FileName = iniFile;
					goto IL_0097;
					IL_0097:
					num2 = 10;
					if (Operators.CompareString(fTitle, "", TextCompare: false) != 0)
					{
						goto IL_00af;
					}
					goto IL_00e3;
					IL_00af:
					num2 = 11;
					MyProject.Forms.FrmMain.CMDialog1Open.Title = fTitle;
					goto IL_00c8;
					IL_00c8:
					num2 = 12;
					MyProject.Forms.FrmMain.CMDialog1Save.Title = fTitle;
					goto IL_00e3;
					IL_00e3:
					num2 = 14;
					if (Operators.CompareString(IniDir, "", TextCompare: false) == 0)
					{
						goto IL_00fc;
					}
					goto IL_013a;
					IL_00fc:
					num2 = 15;
					MyProject.Forms.FrmMain.CMDialog1Open.InitialDirectory = Globals_Renamed.MyPCDir;
					goto IL_0119;
					IL_0119:
					num2 = 16;
					MyProject.Forms.FrmMain.CMDialog1Save.InitialDirectory = Globals_Renamed.MyPCDir;
					goto IL_0170;
					IL_013a:
					num2 = 18;
					MyProject.Forms.FrmMain.CMDialog1Open.InitialDirectory = IniDir;
					goto IL_0154;
					IL_0154:
					num2 = 19;
					MyProject.Forms.FrmMain.CMDialog1Save.InitialDirectory = IniDir;
					goto IL_0170;
					IL_0170:
					num2 = 21;
					switch (fExt)
					{
					case "aqua":
						break;
					case "vgq":
						goto IL_0514;
					case "xml.gz":
						goto IL_0523;
					case "vg2":
						goto IL_0532;
					case "spfsql":
						goto IL_0541;
					case "xls":
						goto IL_0550;
					case "csv-jmp":
						goto IL_055f;
					case "csv+":
						goto IL_056e;
					case "csv+p":
						goto IL_057d;
					case "csvpz+":
						goto IL_058c;
					case "csv+r":
						goto IL_059b;
					case "csv++":
						goto IL_05aa;
					case "csv+++":
						goto IL_05b9;
					case "txt+":
						goto IL_05c8;
					case "ppt":
						goto IL_05d7;
					case "jrn":
						goto IL_05e6;
					case "spf":
						goto IL_05f5;
					case null:
					case "":
						goto IL_0604;
					case "dat":
						goto IL_0613;
					case "ini":
						goto IL_0622;
					case "all":
						goto IL_0631;
					case "images":
						goto IL_0677;
					default:
						goto IL_0683;
					}
					goto IL_0505;
					IL_0683:
					num2 = 69;
					filter = Strings.UCase(fExt) + " Files (*." + fExt + ")|*." + fExt + ";*." + Strings.UCase(fExt) + "|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0677:
					num2 = 67;
					filter = "Image Files (*.png;*.gif;*.jpg;*.bmp)|*.png;*.PNG;*.gif;*.GIF;*.jpg;*.JPG;*.bmp;*.BMP|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0631:
					num2 = 63;
					filter = "SQLPathFinder Query (*.vgq, *.vg2, *.vge, *.vgec)|*.vgq;*.VGQ;*.vg2;*.VG2;*.vge;*.VGE;*.vgec;*.VGEC|VA Scripts (*.va)|*.va;*.VA|SQLPathFinder SQL Scripts (*.spfsql)|*.spfsql;*.SPFSQL|Batch Scripts (*.bat)|*.bat;*.BAT|AllFiles (*.*)|*.*";
					goto IL_063a;
					IL_063a:
					num2 = 64;
					MyProject.Forms.FrmMain.CMDialog1Open.DefaultExt = "vg2";
					goto IL_0657;
					IL_0657:
					num2 = 65;
					MyProject.Forms.FrmMain.CMDialog1Save.DefaultExt = "vg2";
					goto IL_06cf;
					IL_0622:
					num2 = 61;
					filter = "INI File (*.ini)|*.ini;*.INI|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0613:
					num2 = 59;
					filter = "Schema Files (*.dat)|*.dat;*.DAT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0604:
					num2 = 57;
					filter = "";
					goto IL_06cf;
					IL_05f5:
					num2 = 55;
					filter = "Multi Script Files (*.spf)|*.spf;*.SPF";
					goto IL_06cf;
					IL_05e6:
					num2 = 53;
					filter = "JRN Files (*.jrn;)|*.jrn;*.JRN|HTML Files (*.htm)|*.htm;*.HTM|DOC Files (*.doc)|*.doc;*.DOC|RTF Files (*.rtf)|*.rtf;*.RTF|PNG Files (*.png)|*.png;*.PNG|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_05d7:
					num2 = 51;
					filter = "PowerPoint Files (*.ppt)|*.ppt;*.PPT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_05c8:
					num2 = 49;
					filter = "Txt/HTM Files (*.txt;*.htm;*.html)|*.txt;*.htm;*.html;*.TXT;*.HTM;*.HTML|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_05b9:
					num2 = 47;
					filter = "CSV/TAB/JSON Files (*.csv;*.tab;*.json)|*.csv;*.CSV;*.tab;*.TAB;*.json;*.JSON|JSON Files (*.json)|*.json;*.JSON|CSV Files (*.csv)|*.csv;*.CSV|TAB Files (*.tab)|*.tab;*.TAB|ASC Files (*.asc)|*.asc;*.ASC|PLUS Files (*.plus)|*.plus;*.PLUS|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_05aa:
					num2 = 45;
					filter = "CSV/TAB/SDB Files (*.csv;*.tab;*.sdb)|*.csv;*.CSV;*.tab;*.TAB;*.sdb;*.SDB|SDB Files (*.sdb)|*.sdb;*.SDB|CSV Files (*.csv)|*.csv;*.CSV|TAB Files (*.tab)|*.tab;*.TAB|ASC Files (*.asc)|*.asc;*.ASC|PLUS Files (*.plus)|*.plus;*.PLUS|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_059b:
					num2 = 43;
					filter = "CSV/TAB/RDATA Files (*.csv;*.tab;*.RDATA)|*.csv;*.CSV;*.tab;*.TAB;*.rdata;*.RDATA|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_058c:
					num2 = 41;
					filter = "CSV/TAB/Parquet/ZIP Files (*.csv;*.tab;*.parquet;*.zip)|*.csv;*.CSV;*.tab;*.TAB;*.parquet;*.PARQUET;*.zip;*.ZIP|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_057d:
					num2 = 39;
					filter = "CSV/TAB/Parquet Files (*.csv;*.tab;*.parquet)|*.csv;*.CSV;*.tab;*.TAB;*.parquet;*.PARQUET|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_056e:
					num2 = 37;
					filter = "CSV/TAB Files (*.csv;*.tab)|*.csv;*.CSV;*.tab;*.TAB|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_055f:
					num2 = 35;
					filter = "CSV/TAB/JMP Files (*.csv;*.tab;*.jmp)|*.csv;*.CSV;*.tab;*.TAB;*.jmp;*.JMP|ASC Files (*.asc)|*.asc;*.ASC|TXT Files (*.txt)|*.txt;*.TXT|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0550:
					num2 = 33;
					filter = "XLS Files (*.xls;*.xlsx; *.xlsm)|*.xls;*.XLS;*.xlsx;*.XLSX;*.xlsm;*.XLSM|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0541:
					num2 = 31;
					filter = "SQLPathFinder SQL Files (*.spfsql)|*.spfsql;*.SPFSQL|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0532:
					num2 = 29;
					filter = "SQLPathFinder Queries (*.vg2, *.vge, *,vgec)|*.vg2;*.VG2; *.vge; *.VGE; *.vgec; *.VGEC|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0523:
					num2 = 27;
					filter = "TDX Files (*.xml.gz, *.xml)|*.xml.gz;*.XML.GZ;*.xml;*.XML|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0514:
					num2 = 25;
					filter = "SQLPathFinder Queries (*.vgq, *.vg2, *.vge, *.vgec)|*.vgq;*.VGQ;*.vg2;*.VG2;*.vge;*.VGE;*.vgec;*.VGEC|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_0505:
					num2 = 23;
					filter = "CSV/TSV Files (*.csv;*.tsv)|*.csv;*.CSV;*.tsv;*.TSV|PARQUET Files (*.parquet)|*.parquet;*.PARQUET|All Files (*.*)|*.*";
					goto IL_06cf;
					IL_06cf:
					num2 = 71;
					MyProject.Forms.FrmMain.CMDialog1Open.Filter = filter;
					goto IL_06e8;
					IL_06e8:
					num2 = 72;
					MyProject.Forms.FrmMain.CMDialog1Save.Filter = filter;
					goto IL_0701;
					IL_0701:
					num2 = 73;
					if (Operators.CompareString(fExt, "", TextCompare: false) != 0)
					{
						goto IL_0719;
					}
					goto IL_074e;
					IL_0719:
					num2 = 74;
					MyProject.Forms.FrmMain.CMDialog1Open.FilterIndex = 1;
					goto IL_0732;
					IL_0732:
					num2 = 75;
					MyProject.Forms.FrmMain.CMDialog1Save.FilterIndex = 1;
					goto IL_074e;
					IL_074e:
					num2 = 77;
					switch (MyMode)
					{
					case "O":
						break;
					case "S":
					case "A":
						goto IL_08bc;
					default:
						goto end_IL_0001_3;
					}
					goto IL_078d;
					IL_08bc:
					num2 = 95;
					if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0)
					{
						goto IL_08d7;
					}
					goto IL_097d;
					IL_08d7:
					num2 = 96;
					MyProject.Forms.FrmMain.CMDialog1Save.OverwritePrompt = true;
					goto IL_08f0;
					IL_08f0:
					num2 = 97;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
					goto IL_0909;
					IL_0909:
					num2 = 98;
					MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
					goto IL_0922;
					IL_0922:
					num2 = 99;
					if (Operators.CompareString(fTitle, "", TextCompare: false) == 0)
					{
						goto IL_093a;
					}
					goto IL_0a1d;
					IL_093a:
					num2 = 100;
					MyProject.Forms.FrmMain.CMDialog1Open.Title = "Save As";
					goto IL_0957;
					IL_0957:
					num2 = 101;
					MyProject.Forms.FrmMain.CMDialog1Save.Title = "Save As";
					goto IL_0a1d;
					IL_097d:
					num2 = 104;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckFileExists = true;
					goto IL_0996;
					IL_0996:
					num2 = 105;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
					goto IL_09af;
					IL_09af:
					num2 = 106;
					MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
					goto IL_09c8;
					IL_09c8:
					num2 = 107;
					if (Operators.CompareString(fTitle, "", TextCompare: false) == 0)
					{
						goto IL_09e0;
					}
					goto IL_0a1d;
					IL_09e0:
					num2 = 108;
					MyProject.Forms.FrmMain.CMDialog1Open.Title = "Append To";
					goto IL_09fd;
					IL_09fd:
					num2 = 109;
					MyProject.Forms.FrmMain.CMDialog1Save.Title = "Append To";
					goto IL_0a1d;
					IL_0a1d:
					num2 = 112;
					if (MyProject.Forms.FrmMain.CMDialog1Save.ShowDialog() != DialogResult.Cancel)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_078d:
					num2 = 79;
					if (Operators.CompareString(fExt, "", TextCompare: false) == 0)
					{
						goto IL_07a5;
					}
					goto IL_07f4;
					IL_07a5:
					num2 = 80;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
					goto IL_07be;
					IL_07be:
					num2 = 81;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckFileExists = false;
					goto IL_07d7;
					IL_07d7:
					num2 = 82;
					MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
					goto IL_0840;
					IL_07f4:
					num2 = 84;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckFileExists = true;
					goto IL_080d;
					IL_080d:
					num2 = 85;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
					goto IL_0826;
					IL_0826:
					num2 = 86;
					MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
					goto IL_0840;
					end_IL_0001_2:
					break;
				}
				num2 = 113;
				MyProject.Forms.FrmMain.CMDialog1Save.FileName = "CANCEL";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 3146;
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

	public static void Get_HTTP_Help(string MyHTML, string MyAltHTML)
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
				int num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 418:
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
							goto IL_0027;
						case 7:
							goto IL_0059;
						case 8:
							goto IL_0073;
						case 9:
							goto IL_00e8;
						case 10:
							goto IL_00f9;
						case 11:
							goto IL_0106;
						case 13:
							goto IL_0117;
						case 12:
						case 14:
						case 15:
						case 16:
							goto IL_0124;
						case 18:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 17:
						case 19:
						case 20:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00e8:
					num2 = 9;
					num5 = Strings.InStrRev(MyHTML, "/");
					goto IL_00f9;
					IL_00f9:
					num2 = 10;
					if (num5 != 0)
					{
						goto IL_0106;
					}
					goto IL_0117;
					IL_0124:
					num2 = 16;
					Invoke_IE(MyHTML);
					goto end_IL_0001_3;
					IL_0106:
					num2 = 11;
					MyHTML = Strings.Mid(MyHTML, 1, num5);
					goto IL_0124;
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
					MyHTML = Strings.Trim(MyHTML);
					goto IL_0027;
					IL_0027:
					num2 = 6;
					if (Operators.CompareString(Strings.LCase(MyHTML), "http://", TextCompare: false) == 0 || !LikeOperator.LikeString(Strings.LCase(MyHTML), "http*://*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0059;
					IL_0117:
					num2 = 13;
					Invoke_IE(MyAltHTML);
					goto IL_0124;
					IL_0059:
					num2 = 7;
					text = Strings.Right("     " + Strings.LCase(MyHTML), 5);
					goto IL_0073;
					IL_0073:
					num2 = 8;
					if (LikeOperator.LikeString(text, "*.csv", CompareMethod.Binary) || LikeOperator.LikeString(text, "*.tab", CompareMethod.Binary) || LikeOperator.LikeString(text, "*.txt", CompareMethod.Binary) || LikeOperator.LikeString(text, "*.asc", CompareMethod.Binary) || LikeOperator.LikeString(text, "*.jmp", CompareMethod.Binary) || Operators.CompareString(text, ".rdata", TextCompare: false) == 0 || Operators.CompareString(text, ".plus", TextCompare: false) == 0)
					{
						goto IL_00e8;
					}
					goto IL_0124;
					end_IL_0001_2:
					break;
				}
				num2 = 18;
				Invoke_IE(MyAltHTML);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 418;
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

	public static void Invoke_IE(string MyHTMLFile)
	{
		int num = 0;
		string text = "http://sqlpathfinder.ch.intel.com/";
		MyHTMLFile = Strings.Trim(MyHTMLFile);
		if (Operators.CompareString(MyHTMLFile, "", TextCompare: false) != 0)
		{
			if (Operators.CompareString(Strings.UCase(Strings.Trim(Strings.Mid(MyHTMLFile + "    ", 1, 4))), "HTTP", TextCompare: false) != 0)
			{
				MyHTMLFile = text + MyHTMLFile;
			}
			int hwnd = MyProject.Forms.FrmMain.Handle.ToInt32();
			string lpOperation = "Open";
			string lpParameters = "";
			num = Globals_Renamed.ShellExecute(hwnd, ref lpOperation, ref MyHTMLFile, ref lpParameters, ref Globals_Renamed.MyPCDir, 1);
		}
	}

	public static int Invoke_CSVViewer2(short Action, string MyType, ref Process CSV_HwndProcess, ref int CSV_Connected)
	{
		int result = 0;
		string text = Globals_Renamed.MySchemaDir + "\\SPF_META_DATA_FIELDS.csv";
		string WindowText = "CSV Viewer*[SPF_META_DATA_FIELDS.csv]*";
		string text2 = "Metadata";
		int num = 0;
		int num2 = 0;
		try
		{
			Application.UseWaitCursor = true;
			Cursor.Current = Cursors.WaitCursor;
			string fileName = MyProject.Application.Info.DirectoryPath + "\\csvviewer.exe";
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				FileName = fileName,
				WindowStyle = ProcessWindowStyle.Normal
			};
			if (CSV_Connected == 1 && CSV_HwndProcess.HasExited)
			{
				CSV_HwndProcess.Close();
				CSV_HwndProcess.Dispose();
				CSV_Connected = 0;
			}
			switch (Action)
			{
			case 1:
				if (CSV_Connected == 0)
				{
					processStartInfo.Arguments = " /CSV=\"" + text + "\"";
					CSV_HwndProcess = Process.Start(processStartInfo);
					CSV_Connected = 1;
				}
				else
				{
					num2 = 0;
					num = General_Procedures.FindWindowLike(ref num2, 0, ref WindowText);
					num = Globals_Renamed.ShowWindow(num2, 1);
					num = Globals_Renamed.SetWindowPos(num2, 0, 0, 0, 0, 0, -1);
					Interaction.AppActivate(CSV_HwndProcess.Id);
				}
				break;
			case 0:
				if (CSV_Connected == 1)
				{
					if (!CSV_HwndProcess.HasExited)
					{
						CSV_HwndProcess.Kill();
					}
					CSV_HwndProcess.Close();
					CSV_HwndProcess.Dispose();
					CSV_Connected = 0;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox("Could not access " + text2 + ". (" + ex2.ToString() + ").", MsgBoxStyle.Exclamation, text2);
			CSV_Connected = 0;
			result = 1;
			ProjectData.ClearProjectError();
		}
		finally
		{
			Application.UseWaitCursor = false;
			Cursor.Current = Cursors.Default;
		}
		return result;
	}

	public static int Invoke_CSVViewer3(short Action, string MyType)
	{
		int try0001_dispatch = -1;
		int result;
		string text = default(string);
		int num2 = default(int);
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
					result = 0;
					text = "Finding";
					ProjectData.ClearProjectError();
					num2 = 2;
					string WindowText = "";
					int num3 = 0;
					int HwndReturn = 0;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyType)), "HELP2", TextCompare: false) == 0)
					{
						WindowText = "CSV Viewer*[SPF_Help_Data.tab]*";
					}
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyType)), "HELP2", TextCompare: false) != 0)
					{
						goto end_IL_0001;
					}
					Process[] processesByName = Process.GetProcessesByName("CSVViewer");
					if (processesByName.Length <= 0)
					{
						goto end_IL_0001;
					}
					switch (Action)
					{
					case 1:
					{
						text = "Showing";
						int num4 = 0;
						Process[] array2 = processesByName;
						foreach (Process process2 in array2)
						{
							if (process2 != null && LikeOperator.LikeString(process2.MainWindowTitle, WindowText, CompareMethod.Binary))
							{
								num4 = process2.Id;
								break;
							}
						}
						if (num4 != 0)
						{
							num3 = General_Procedures.FindWindowLike(ref HwndReturn, 0, ref WindowText);
							if (num3 == 1)
							{
								num3 = Globals_Renamed.ShowWindow(HwndReturn, 1);
								num3 = Globals_Renamed.SetWindowPos(HwndReturn, 0, 0, 0, 0, 0, -1);
							}
							Interaction.AppActivate(num4);
						}
						break;
					}
					case 0:
					{
						text = "Closing";
						Process[] array = processesByName;
						foreach (Process process in array)
						{
							if (process != null && LikeOperator.LikeString(process.MainWindowTitle, WindowText, CompareMethod.Binary))
							{
								process.Kill();
							}
						}
						break;
					}
					}
					goto end_IL_0001_2;
				}
				case 491:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error " + text + " CSV Viewer. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "CSV Viewer");
						Information.Err().Clear();
						result = 1;
						Cursor.Current = Cursors.Default;
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_0221;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 491;
				continue;
			}
			break;
			IL_0221:
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

	public static void Save_DTRBuild_Ini(string MyMode)
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
					errsource = "BuildForm - Save_DTRBuild_Ini";
					int num3 = 0;
					string text = "";
					int num4 = 0;
					MyMode = Strings.Trim(Strings.UCase(MyMode));
					text = "Query Defaults";
					switch (MyMode)
					{
					case "C":
					{
						if (Operators.CompareString(Globals_Renamed.gUseRegistry, "Y", TextCompare: false) == 0 && DoRegistry("W", Globals_Renamed.gQueryDir) == 0)
						{
							Globals_Renamed.IniFileName = Globals_Renamed.gQueryDir + "SQLPathFinder_" + Globals_Renamed.gWinuser + ".ini";
						}
						string lpString;
						if (Globals_Renamed.gSavePCDir)
						{
							lpString = "SPF Temp Directory";
							num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyPCDir, ref Globals_Renamed.IniFileName);
						}
						lpString = "SPF Working Directory II";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gQueryDir, ref Globals_Renamed.IniFileName);
						lpString = "Excel Executable2";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyExcelPath, ref Globals_Renamed.IniFileName);
						lpString = "JMP Executable";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyJMPPath, ref Globals_Renamed.IniFileName);
						lpString = "Use Outlook Email";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gEmailOutlook, ref Globals_Renamed.IniFileName);
						lpString = "Use SPF Command Window";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gSPFCmdWin, ref Globals_Renamed.IniFileName);
						lpString = "MIDAS Driver";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gMidasDriver, ref Globals_Renamed.IniFileName);
						lpString = "Database Schema";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyDBSchema, ref Globals_Renamed.IniFileName);
						lpString = "MARS Server";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyMARSServer, ref Globals_Renamed.IniFileName);
						lpString = "ARIES Server";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyARIESServer, ref Globals_Renamed.IniFileName);
						lpString = "OASYS Server";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyOASysServer, ref Globals_Renamed.IniFileName);
						lpString = "OTHER Server";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.MyOtherServer, ref Globals_Renamed.IniFileName);
						lpString = "OracleRetry";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gOrclRetry, ref Globals_Renamed.IniFileName);
						lpString = "Python EE Debug";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gPyDebug, ref Globals_Renamed.IniFileName);
						lpString = "Next Interactive Web";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gWebNext, ref Globals_Renamed.IniFileName);
						lpString = "Encode Full FileScan";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gEncodeFFS, ref Globals_Renamed.IniFileName);
						lpString = "Convert MAO Uber";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gConvertMAOUber, ref Globals_Renamed.IniFileName);
						lpString = "Set UTF-8-BOM Encoding";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gEncodeUTFBOM, ref Globals_Renamed.IniFileName);
						lpString = "Minimize PATH2";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref Globals_Renamed.gUseMinPath, ref Globals_Renamed.IniFileName);
						text = "Colors";
						int num5 = Information.UBound(Globals_Renamed.gGridColor);
						string lpString2;
						for (num3 = 0; num3 <= num5; num3 = checked(num3 + 1))
						{
							lpString = "Grid_a" + Strings.Trim(Conversions.ToString(num3));
							lpString2 = Globals_Renamed.gGridColor[num3].Color.Name;
							num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref lpString2, ref Globals_Renamed.IniFileName);
						}
						lpString2 = "SQLPATHFINDER";
						lpString = "RTERM";
						string lpKeyName = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini";
						num4 = Globals_Renamed.WritePrivateProfileString(ref lpString2, ref lpString, ref Globals_Renamed.gMyRPath, ref lpKeyName);
						break;
					}
					case "B":
					{
						string lpKeyName = "SHGUI Working Directory";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpKeyName, ref Globals_Renamed.gSHGUIDir, ref Globals_Renamed.IniFileName);
						lpKeyName = "SHGUI SITE";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpKeyName, ref Globals_Renamed.gLastSHSite, ref Globals_Renamed.IniFileName);
						break;
					}
					case "W":
					{
						string lpKeyName = "CmdWinColor";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpKeyName, ref Globals_Renamed.gCmdWinColor, ref Globals_Renamed.IniFileName);
						lpKeyName = "CmdWinHistory";
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpKeyName, ref Globals_Renamed.gCmdWinHist, ref Globals_Renamed.IniFileName);
						break;
					}
					case "W2":
					{
						string lpKeyName = "CmdWinH";
						string lpString = Conversions.ToString(Globals_Renamed.gfrmlog_h);
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpKeyName, ref lpString, ref Globals_Renamed.IniFileName);
						lpString = "CmdWinW";
						lpKeyName = Conversions.ToString(Globals_Renamed.gfrmlog_w);
						num4 = Globals_Renamed.WritePrivateProfileString(ref text, ref lpString, ref lpKeyName, ref Globals_Renamed.IniFileName);
						break;
					}
					}
					goto end_IL_0001;
				}
				case 1163:
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
				try0001_dispatch = 1163;
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

	public static string Get_SchemaName_Ini(string lpKeyName)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
		string text2 = default(string);
		int num2 = default(int);
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
					errsource = "BuildForm - Get_SchemaName_Ini";
					string myIniFile = Globals_Renamed.MySchemaDir + "\\MARSSchema.ini";
					string text = "";
					text2 = "mc_1_";
					text = text2;
					if (Operators.CompareString(Strings.Trim(lpKeyName), "", TextCompare: false) != 0)
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						text = (((Strings.InStr(lpKeyName, ".[") == 0 || Strings.InStr(Strings.UCase(lpKeyName), "].MARS") == 0) && Strings.InStr(Strings.LCase(lpKeyName), "<<<spf-site>>>") == 0 && Strings.InStr(Strings.UCase(lpKeyName), "ALL.MARS") == 0 && Strings.InStr(Strings.UCase(lpKeyName), "MAP_NODES") == 0 && !LikeOperator.LikeString(lpKeyName, "*<<<*>>>*", CompareMethod.Binary)) ? General_Procedures.Get_Ini_Data("Schema", Strings.UCase(lpKeyName), text, 128, myIniFile) : "@[]@");
						if (Operators.CompareString(Strings.UCase(Strings.Trim(text)), "NONE", TextCompare: false) == 0)
						{
							text = "";
						}
						else if (Operators.CompareString(Strings.Trim(text), "", TextCompare: false) == 0)
						{
							text = "mc_1_";
						}
						text2 = text;
					}
					goto end_IL_0001;
				}
				case 356:
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
				goto IL_019a;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 356;
				continue;
			}
			break;
			IL_019a:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return text2;
	}

	public static void Process_Screen(string Action)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						if (Operators.CompareString(Action, "CLS", TextCompare: false) != 0)
						{
							if (Operators.CompareString(Action, "CANCEL", TextCompare: false) == 0 && MyProject.Forms.FrmMain.FrmCmdSimf != null && !MyProject.Forms.FrmMain.FrmCmdSimf.IsDisposed)
							{
								MyProject.Forms.FrmMain.FrmCmdSimf.CancelCmdWindow();
							}
						}
						else if (MyProject.Forms.FrmMain.FrmCmdSimf != null && !MyProject.Forms.FrmMain.FrmCmdSimf.IsDisposed)
						{
							MyProject.Forms.FrmMain.FrmCmdSimf.ClearCmdWindow();
						}
						goto end_IL_0001;
					}
					short num3 = 0;
					string keys = "";
					if (Operators.CompareString(Action, "CLS", TextCompare: false) != 0)
					{
						if (Operators.CompareString(Action, "CANCEL", TextCompare: false) == 0)
						{
							keys = "^+{BREAK}";
						}
					}
					else
					{
						keys = "CLS{ENTER}";
					}
					num3 = General_Procedures.Activate_Emulator_2();
					if (num3 == 1)
					{
						Application.DoEvents();
						SendKeys.SendWait(keys);
					}
					goto end_IL_0001_2;
				}
				case 378:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error communicating with SQL Engine. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Session Error");
						Information.Err().Clear();
						goto end_IL_0001_2;
					}
					break;
				}
				goto IL_01b0;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 378;
				continue;
			}
			break;
			IL_01b0:
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

	public static void Invoke_SQL_Emulator(string MyModal = "")
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text2 = default(string);
		int num = default(int);
		int num3 = default(int);
		ProcessStartInfo processStartInfo = default(ProcessStartInfo);
		string text = default(string);
		string text3 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				string text4;
				int num5;
				short num6;
				int num7;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					text2 = General_Procedures.Set_ORCL_Path();
					goto IL_000a;
				case 1111:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_0377;
						default:
							goto end_IL_0001;
						}
						goto IL_0323;
					}
					IL_02fa:
					num2 = 41;
					Globals_Renamed.MS_hwndProcess = Process.Start(processStartInfo);
					goto IL_0309;
					IL_0309:
					num2 = 42;
					Globals_Renamed.MSAccess_Connected = 1;
					goto IL_0312;
					IL_02ed:
					num2 = 40;
					processStartInfo.Arguments = text;
					goto IL_02fa;
					IL_0377:
					num4 = num + 1;
					num = 0;
					switch (num4)
					{
					case 1:
						break;
					case 2:
						goto IL_000a;
					case 3:
						goto IL_0022;
					case 4:
						goto IL_002b;
					case 5:
						goto IL_0034;
					case 6:
						goto IL_0041;
					case 7:
						goto IL_004e;
					case 8:
						goto IL_006c;
					case 9:
						goto IL_0087;
					case 10:
						goto IL_00cb;
					case 11:
					case 12:
						goto IL_00e4;
					case 13:
						goto IL_0102;
					case 14:
						goto IL_0115;
					case 15:
						goto IL_0146;
					case 16:
						goto IL_015d;
					case 18:
						goto IL_0179;
					case 19:
						goto IL_0191;
					case 20:
						goto IL_01aa;
					case 21:
						goto IL_01c3;
					case 24:
						goto IL_01e4;
					case 25:
						goto IL_01ee;
					case 26:
						goto IL_01f4;
					case 27:
						goto IL_01fa;
					case 28:
						goto IL_0200;
					case 29:
						goto IL_020a;
					case 30:
						goto IL_0228;
					case 31:
						goto IL_0239;
					case 32:
						goto IL_027d;
					case 33:
					case 34:
						goto IL_0290;
					case 35:
						goto IL_02a8;
					case 36:
						goto IL_02b5;
					case 37:
						goto IL_02c9;
					case 38:
						goto IL_02d1;
					case 39:
						goto IL_02e1;
					case 40:
						goto IL_02ed;
					case 41:
						goto IL_02fa;
					case 42:
						goto IL_0309;
					case 43:
						goto IL_0312;
					case 45:
						goto IL_0323;
					case 46:
						goto IL_0342;
					case 49:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 17:
					case 22:
					case 23:
					case 44:
					case 47:
					case 48:
					case 50:
					case 51:
					case 52:
						goto end_IL_0001_3;
					}
					goto default;
					IL_0323:
					num2 = 45;
					Interaction.MsgBox("Error invoking Command Window: " + Conversion.ErrorToString(), MsgBoxStyle.Exclamation, "Invoke Error");
					goto IL_0342;
					IL_0342:
					num2 = 46;
					Cursor.Current = Cursors.Default;
					goto end_IL_0001_3;
					IL_000a:
					num2 = 2;
					text3 = MyProject.Application.Info.Version.ToString();
					goto IL_0022;
					IL_0022:
					num2 = 3;
					text = "";
					goto IL_002b;
					IL_002b:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_0034;
					IL_0034:
					num2 = 5;
					Microsoft.VisualBasic.FileSystem.ChDrive(Globals_Renamed.MyPCDir);
					goto IL_0041;
					IL_0041:
					num2 = 6;
					Microsoft.VisualBasic.FileSystem.ChDir(Globals_Renamed.MyPCDir);
					goto IL_004e;
					IL_004e:
					num2 = 7;
					if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						goto IL_006c;
					}
					goto IL_01e4;
					IL_006c:
					num2 = 8;
					Globals_Renamed.gSPFCmdWinInit = "@Echo Off&&prompt SQLP$G &&cd /d " + Globals_Renamed.MyPCDir + "&&SET PYTHONPATH=";
					goto IL_0087;
					IL_0087:
					num2 = 9;
					if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.gMyRPath)), Strings.Trim(Strings.UCase(MyProject.Application.Info.DirectoryPath + "\\R\\R-Latest\\bin\\x64\\RTerm.exe")), TextCompare: false) == 0)
					{
						goto IL_00cb;
					}
					goto IL_00e4;
					IL_00cb:
					num2 = 10;
					Globals_Renamed.gSPFCmdWinInit += "&&SET R_LIBS_USER=";
					goto IL_00e4;
					IL_00e4:
					num2 = 12;
					Globals_Renamed.gSPFCmdWinInit = Globals_Renamed.gSPFCmdWinInit + "&&Echo SQLPathFinder - " + text3 + " &&Echo.&&cd &&Echo.";
					goto IL_0102;
					IL_0102:
					num2 = 13;
					Globals_Renamed.gSPFCmdWinInit += text2;
					goto IL_0115;
					IL_0115:
					num2 = 14;
					if (MyProject.Forms.FrmMain.FrmCmdSimf == null || MyProject.Forms.FrmMain.FrmCmdSimf.IsDisposed)
					{
						goto IL_0146;
					}
					goto IL_0179;
					IL_0312:
					num2 = 43;
					Cursor.Current = Cursors.Default;
					goto end_IL_0001_3;
					IL_0179:
					num2 = 18;
					MyProject.Forms.FrmMain.FrmCmdSimf.Show();
					goto IL_0191;
					IL_0191:
					num2 = 19;
					MyProject.Forms.FrmMain.FrmCmdSimf.WindowState = FormWindowState.Normal;
					goto IL_01aa;
					IL_01aa:
					num2 = 20;
					MyProject.Forms.FrmMain.FrmCmdSimf.TopMost = true;
					goto IL_01c3;
					IL_01c3:
					num2 = 21;
					MyProject.Forms.FrmMain.FrmCmdSimf.TopMost = false;
					goto end_IL_0001_3;
					IL_0146:
					num2 = 15;
					MyProject.Forms.FrmMain.FrmCmdSimf = new FrmCmdSim();
					goto IL_015d;
					IL_015d:
					num2 = 16;
					MyProject.Forms.FrmMain.FrmCmdSimf.Show();
					goto end_IL_0001_3;
					IL_01e4:
					num2 = 24;
					text4 = "";
					goto IL_01ee;
					IL_01ee:
					num2 = 25;
					num5 = 0;
					goto IL_01f4;
					IL_01f4:
					num2 = 26;
					num6 = 0;
					goto IL_01fa;
					IL_01fa:
					num2 = 27;
					num7 = 0;
					goto IL_0200;
					IL_0200:
					num2 = 28;
					processStartInfo = new ProcessStartInfo();
					goto IL_020a;
					IL_020a:
					num2 = 29;
					text = "cmd /k \"title " + Globals_Renamed.MYMSAccessTitle + "&&prompt SQLP$G &&cd /d " + Globals_Renamed.MyPCDir;
					goto IL_0228;
					IL_0228:
					num2 = 30;
					text += "&&SET PYTHONPATH=";
					goto IL_0239;
					IL_0239:
					num2 = 31;
					if (Operators.CompareString(Strings.Trim(Strings.UCase(Globals_Renamed.gMyRPath)), Strings.Trim(Strings.UCase(MyProject.Application.Info.DirectoryPath + "\\R\\R-Latest\\bin\\x64\\RTerm.exe")), TextCompare: false) == 0)
					{
						goto IL_027d;
					}
					goto IL_0290;
					IL_027d:
					num2 = 32;
					text += "&&SET R_LIBS_USER=";
					goto IL_0290;
					IL_0290:
					num2 = 34;
					text = text + "&&Echo SQLPathFinder - " + text3 + " &&Echo.&&cd &&Echo.";
					goto IL_02a8;
					IL_02a8:
					num2 = 35;
					text += text2;
					goto IL_02b5;
					IL_02b5:
					num2 = 36;
					if (Globals_Renamed.MSAccess_Connected != 0)
					{
						break;
					}
					goto IL_02c9;
					IL_02c9:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_02d1;
					IL_02d1:
					num2 = 38;
					processStartInfo.FileName = "cmd.exe";
					goto IL_02e1;
					IL_02e1:
					num2 = 39;
					processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
					goto IL_02ed;
					end_IL_0001_2:
					break;
				}
				num2 = 49;
				Interaction.MsgBox(General_Procedures.Get_UI("errconn4"), MsgBoxStyle.Exclamation, "Already Connected");
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1111;
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

	public static void Close_SQL_Emulator(string lSPFCmdWin)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0001_dispatch)
				{
				default:
					if (Operators.CompareString(lSPFCmdWin, "Y", TextCompare: false) == 0)
					{
						if (MyProject.Forms.FrmMain.FrmCmdSimf != null && !MyProject.Forms.FrmMain.FrmCmdSimf.IsDisposed)
						{
							MyProject.Forms.FrmMain.FrmCmdSimf.CloseCmdWindow();
						}
					}
					else if (Globals_Renamed.MSAccess_Connected == 1)
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						if (!Globals_Renamed.MS_hwndProcess.HasExited)
						{
							Globals_Renamed.MS_hwndProcess.Kill();
						}
						Globals_Renamed.MS_hwndProcess.Close();
						Globals_Renamed.MS_hwndProcess.Dispose();
						Globals_Renamed.MSAccess_Connected = 0;
					}
					goto end_IL_0001;
				case 191:
					num = -1;
					switch (num2)
					{
					case 2:
						Globals_Renamed.MSAccess_Connected = 0;
						goto end_IL_0001;
					}
					break;
				}
				goto IL_00f5;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 191;
				continue;
			}
			break;
			IL_00f5:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static void Set_Controls(short MyMode)
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
				short num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000c;
				case 489:
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
							goto IL_0022;
						case 5:
							goto IL_003a;
						case 6:
							goto IL_005d;
						case 7:
							goto IL_0080;
						case 8:
							goto IL_00a3;
						case 10:
							goto IL_00cc;
						case 11:
							goto IL_00e5;
						case 12:
							goto IL_0109;
						case 13:
							goto IL_012d;
						case 14:
							goto IL_0151;
						case 3:
						case 9:
						case 15:
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 17:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_003a:
					num2 = 5;
					MyProject.Forms.FrmMain.cmdIcon.Items[3].Enabled = false;
					goto IL_005d;
					IL_005d:
					num2 = 6;
					MyProject.Forms.FrmMain.cmdIcon.Items[4].Enabled = false;
					goto IL_0080;
					IL_0022:
					num2 = 4;
					MyProject.Forms.FrmMain.MnuSASOptions.Visible = false;
					goto IL_003a;
					IL_0080:
					num2 = 7;
					MyProject.Forms.FrmMain.cmdIcon.Items[5].Enabled = false;
					goto IL_00a3;
					IL_000c:
					num2 = 2;
					num5 = MyMode;
					if (num5 == 0)
					{
						goto IL_0022;
					}
					if (num5 != 2)
					{
						break;
					}
					goto IL_00cc;
					IL_00a3:
					num2 = 8;
					MyProject.Forms.FrmMain.cmdIcon.Items[6].Enabled = false;
					break;
					IL_00cc:
					num2 = 10;
					MyProject.Forms.FrmMain.MnuSASOptions.Visible = true;
					goto IL_00e5;
					IL_00e5:
					num2 = 11;
					MyProject.Forms.FrmMain.cmdIcon.Items[3].Enabled = true;
					goto IL_0109;
					IL_0109:
					num2 = 12;
					MyProject.Forms.FrmMain.cmdIcon.Items[4].Enabled = true;
					goto IL_012d;
					IL_012d:
					num2 = 13;
					MyProject.Forms.FrmMain.cmdIcon.Items[5].Enabled = true;
					goto IL_0151;
					IL_0151:
					num2 = 14;
					MyProject.Forms.FrmMain.cmdIcon.Items[6].Enabled = true;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				MyProject.Forms.FrmMain.cmdIcon.Refresh();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 489;
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

	public static void Invoke_Excel(short Action, string MyXLFile)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		ProcessStartInfo processStartInfo = default(ProcessStartInfo);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num5;
				short num6;
				int num3;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildForm - Invoke_Excel";
					num3 = 0;
					string text = "";
					string text2 = "";
					int num4 = 0;
					string text3 = "";
					processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = Globals_Renamed.MyExcelPath;
					processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
					goto IL_0047;
				}
				case 929:
					{
						num = -1;
						switch (num2)
						{
						case 3:
							Interaction.MsgBox("Error starting EXCEL. (" + Conversion.ErrorToString() + "). Check Excel path in Configure Form.", MsgBoxStyle.Critical, "Excel Error");
							Globals_Renamed.Excel_Connected = 0;
							Cursor.Current = Cursors.Default;
							Information.Err().Clear();
							goto end_IL_0001;
						case 4:
							break;
						case 5:
							Interaction.MsgBox("Error closing EXCEL. (" + Conversion.ErrorToString() + "). Perhaps it is closed.", MsgBoxStyle.Critical, "Excel Error");
							Globals_Renamed.Excel_Connected = 0;
							Information.Err().Clear();
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						default:
							goto end_IL_0001_2;
						}
						goto IL_02cc;
					}
					IL_0047:
					num5 = 0;
					if (Globals_Renamed.Excel_Connected == 1)
					{
						num3 = 0;
						if (Globals_Renamed.Excel_hwndProcess.HasExited)
						{
							Globals_Renamed.Excel_hwndProcess.Close();
							Globals_Renamed.Excel_hwndProcess.Dispose();
							Globals_Renamed.Excel_Connected = 0;
						}
					}
					num3 = 0;
					num6 = Action;
					if (num6 != 0)
					{
						if ((uint)(num6 - 1) > 1u)
						{
							goto end_IL_0001;
						}
						if (Action == 1)
						{
							MyXLFile = Globals_Renamed.ExcelPending;
						}
						if (Operators.CompareString(MyXLFile, "", TextCompare: false) != 0)
						{
							MyXLFile = Replace_Globals(MyXLFile, 1);
							if (!MyProject.Computer.FileSystem.FileExists(MyXLFile))
							{
								MyXLFile = "";
							}
						}
						if (Globals_Renamed.Excel_Connected == 0)
						{
							ProjectData.ClearProjectError();
							num2 = 3;
							if (Operators.CompareString(MyXLFile, "", TextCompare: false) != 0)
							{
								processStartInfo.Arguments = "/r \"" + MyXLFile + "\"";
								Globals_Renamed.Excel_hwndProcess = Process.Start(processStartInfo);
							}
							else
							{
								processStartInfo.Arguments = "";
								Globals_Renamed.Excel_hwndProcess = Process.Start(processStartInfo);
							}
							Globals_Renamed.Excel_Connected = 1;
							goto end_IL_0001;
						}
						ProjectData.ClearProjectError();
						num2 = 4;
						string text2 = "Microsoft Excel*";
						int num4 = 0;
						if (General_Procedures.FindWindowLike(ref num4, 0, ref text2) == 0)
						{
							goto IL_02cc;
						}
						num3 = Globals_Renamed.ShowWindow(num4, 1);
						num3 = Globals_Renamed.SetWindowPos(num4, 0, 0, 0, 0, 0, -1);
						if (Operators.CompareString(MyXLFile, "", TextCompare: false) != 0)
						{
							General_Procedures.SetFileROAttr(MyXLFile, MyAction: true);
							int hwnd = MyProject.Forms.FrmMain.Handle.ToInt32();
							string lpOperation = "Open";
							string lpParameters = "";
							num3 = Globals_Renamed.ShellExecute(hwnd, ref lpOperation, ref MyXLFile, ref lpParameters, ref Globals_Renamed.MyPCDir, 1);
							General_Procedures.SetFileROAttr(MyXLFile, MyAction: false);
						}
						else
						{
							Interaction.AppActivate(Globals_Renamed.Excel_hwndProcess.Id);
						}
						goto end_IL_0001;
					}
					ProjectData.ClearProjectError();
					num2 = 5;
					if (Globals_Renamed.Excel_Connected == 1)
					{
						if (!Globals_Renamed.Excel_hwndProcess.HasExited)
						{
							Globals_Renamed.Excel_hwndProcess.Kill();
						}
						Globals_Renamed.Excel_hwndProcess.Close();
						Globals_Renamed.Excel_hwndProcess.Dispose();
						Globals_Renamed.Excel_Connected = 0;
					}
					goto end_IL_0001;
					IL_02cc:
					num3 = (int)Interaction.MsgBox("Error Reconnecting to Excel. (" + Conversion.ErrorToString() + ") Perhaps it is closed. Do you want to Re-Start EXCEL? ", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "Excel Error");
					Information.Err().Clear();
					if (num3 == 7)
					{
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					Cursor.Current = Cursors.Default;
					Globals_Renamed.Excel_Connected = 0;
					goto IL_0047;
					end_IL_0001_2:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 929;
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

	public static void Invoke_JMP(short Action, string MyJMPFile)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string lpFile = default(string);
		ProcessStartInfo processStartInfo = default(ProcessStartInfo);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				short num5;
				int num3;
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildForm - Invoke_JMP";
					num3 = 0;
					lpFile = Strings.Trim(Globals_Renamed.MyPCDir) + "SQLPathFinder.jsl";
					string text = "";
					int num4 = 0;
					string text2 = "";
					string text3 = "";
					processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = Globals_Renamed.MyJMPPath;
					processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
					goto IL_005d;
				}
				case 1254:
					{
						num = -1;
						switch (num2)
						{
						case 4:
							Interaction.MsgBox("Error starting JMP. (" + Conversion.ErrorToString() + "). Check JMP path in Configure Form.", MsgBoxStyle.Critical, "JMP Error");
							Information.Err().Clear();
							Globals_Renamed.JMP_Connected = 0;
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						case 3:
							break;
						case 5:
							goto IL_041f;
						case 6:
							Interaction.MsgBox("Error closing JMP. (" + Conversion.ErrorToString() + "). Perhaps it is already closed", MsgBoxStyle.Critical, "JMP Error");
							Information.Err().Clear();
							Globals_Renamed.JMP_Connected = 0;
							goto end_IL_0001;
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Cursor.Current = Cursors.Default;
							goto end_IL_0001;
						default:
							goto end_IL_0001_2;
						}
						goto IL_040e;
					}
					IL_005d:
					if (Globals_Renamed.JMP_Connected == 1 && Globals_Renamed.JMP_hwndProcess.HasExited)
					{
						Globals_Renamed.JMP_hwndProcess.Close();
						Globals_Renamed.JMP_hwndProcess.Dispose();
						Globals_Renamed.JMP_Connected = 0;
					}
					num3 = 0;
					num5 = Action;
					if (num5 != 0)
					{
						if ((uint)(num5 - 1) > 1u)
						{
							goto end_IL_0001;
						}
						if (Action == 1)
						{
							MyJMPFile = Globals_Renamed.ExcelPending;
						}
						if (Operators.CompareString(MyJMPFile, "", TextCompare: false) != 0)
						{
							MyJMPFile = Replace_Globals(MyJMPFile, 1);
							string text3 = General_Procedures.Get_JMP_R_DLM(MyJMPFile, "JMP");
							string text2 = "//!" + Globals_Renamed.CRLF + "csv=open(\"" + MyJMPFile + "\"";
							text2 = text2 + Globals_Renamed.CRLF + ",Import Settings(";
							text2 = text2 + Globals_Renamed.CRLF + "End Of Line( CRLF, CR, LF ),";
							text2 = text2 + Globals_Renamed.CRLF + "End Of Field( " + text3 + " ),";
							text2 = text2 + Globals_Renamed.CRLF + "Strip Quotes( 1 ),";
							text2 = text2 + Globals_Renamed.CRLF + "Use Apostrophe as Quotation Mark( 0 ),";
							text2 = text2 + Globals_Renamed.CRLF + "Labels( 1 ),";
							text2 = text2 + Globals_Renamed.CRLF + "Column Names Start( 1 ),";
							text2 = text2 + Globals_Renamed.CRLF + "Data Starts( 2 ),";
							text2 = text2 + Globals_Renamed.CRLF + "Lines To Read( All )));";
							ProjectData.ClearProjectError();
							num2 = 3;
							if (General_Procedures.Save_SQL_Query(text2, lpFile) == 0)
							{
								goto IL_040e;
							}
						}
						ProjectData.ClearProjectError();
						num2 = 4;
						if (Globals_Renamed.JMP_Connected == 0)
						{
							if (Operators.CompareString(MyJMPFile, "", TextCompare: false) != 0)
							{
								processStartInfo.Arguments = " \"" + lpFile + "\"";
								Globals_Renamed.JMP_hwndProcess = Process.Start(processStartInfo);
							}
							else
							{
								processStartInfo.Arguments = "";
								Globals_Renamed.JMP_hwndProcess = Process.Start(processStartInfo);
							}
							Globals_Renamed.JMP_Connected = 1;
							goto end_IL_0001;
						}
						string text = "JMP -*";
						int num4 = 0;
						num3 = General_Procedures.FindWindowLike(ref num4, 0, ref text);
						if (num3 == 0)
						{
							text = "JMP*";
							num4 = 0;
							num3 = General_Procedures.FindWindowLike(ref num4, 0, ref text);
						}
						if (num3 == 0)
						{
							goto IL_041f;
						}
						if (Operators.CompareString(MyJMPFile, "", TextCompare: false) != 0)
						{
							ProjectData.ClearProjectError();
							num2 = 5;
							num3 = Globals_Renamed.ShowWindow(num4, 1);
							num3 = Globals_Renamed.SetWindowPos(num4, 0, 0, 0, 0, 0, -1);
							int hwnd = MyProject.Forms.FrmMain.Handle.ToInt32();
							string lpOperation = "Open";
							string lpParameters = "";
							num3 = Globals_Renamed.ShellExecute(hwnd, ref lpOperation, ref lpFile, ref lpParameters, ref Globals_Renamed.MyPCDir, 1);
						}
						else
						{
							num3 = Globals_Renamed.ShowWindow(num4, 1);
							num3 = Globals_Renamed.SetWindowPos(num4, 0, 0, 0, 0, 0, -1);
							Interaction.AppActivate(Globals_Renamed.JMP_hwndProcess.Id);
						}
						goto end_IL_0001;
					}
					ProjectData.ClearProjectError();
					num2 = 6;
					if (Globals_Renamed.JMP_Connected == 1)
					{
						if (!Globals_Renamed.JMP_hwndProcess.HasExited)
						{
							Globals_Renamed.JMP_hwndProcess.Kill();
						}
						Globals_Renamed.JMP_hwndProcess.Close();
						Globals_Renamed.JMP_hwndProcess.Dispose();
						Globals_Renamed.JMP_Connected = 0;
					}
					goto end_IL_0001;
					IL_041f:
					num3 = (int)Interaction.MsgBox("Error connecting to JMP. (" + Conversion.ErrorToString() + ") Perhaps it is closed. Do you wish to retry? ", MsgBoxStyle.YesNo | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2, "JMP Error");
					Information.Err().Clear();
					if (num3 == 7)
					{
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					Cursor.Current = Cursors.Default;
					Globals_Renamed.JMP_Connected = 0;
					goto IL_005d;
					IL_040e:
					Information.Err().Clear();
					goto end_IL_0001;
					end_IL_0001_2:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1254;
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

	public static void Invoke_Txt(string MyApp, short MyMode, string MyTxtFile)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
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
					int num3 = 0;
					string text2 = "notepad";
					text = "NotePad";
					if (Operators.CompareString(MyApp, "spfgrid", TextCompare: false) == 0)
					{
						text2 = MyProject.Application.Info.DirectoryPath + "\\csvviewer.exe";
						text = "SPFGrid";
					}
					if (MyMode == 1)
					{
						MyTxtFile = Globals_Renamed.ExcelPending;
					}
					MyTxtFile = Strings.Trim(MyTxtFile);
					if (Operators.CompareString(MyTxtFile, "", TextCompare: false) != 0)
					{
						MyTxtFile = Replace_Globals(MyTxtFile, 1);
						MyTxtFile = ((Operators.CompareString(MyApp, "notepad", TextCompare: false) != 0) ? (" /CSV=\"" + MyTxtFile + "\" /AUTOFILTER=\"Y\" /SESSIONID=\"" + Globals_Renamed.gSPFCache + "\"") : (" " + MyTxtFile));
					}
					num3 = Interaction.Shell(text2 + MyTxtFile, AppWinStyle.NormalFocus);
					goto end_IL_0001;
				}
				case 328:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error starting " + text + ". (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, text + " Error");
						Cursor.Current = Cursors.Default;
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 328;
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

	public static void Assign_Facility_Node(string InFac, ref string MNode, ref string ANode, ref string ONode)
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
				case 2671:
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
							goto IL_01f5;
						case 8:
							goto IL_0201;
						case 10:
							goto IL_0213;
						case 12:
							goto IL_0220;
						case 14:
							goto IL_022d;
						case 16:
							goto IL_023a;
						case 18:
							goto IL_0247;
						case 20:
							goto IL_0254;
						case 5:
						case 7:
						case 9:
						case 11:
						case 13:
						case 15:
						case 17:
						case 19:
						case 21:
						case 22:
							goto IL_0262;
						case 24:
							goto IL_05fc;
						case 26:
							goto IL_060c;
						case 28:
							goto IL_061c;
						case 30:
							goto IL_062c;
						case 32:
							goto IL_063c;
						case 34:
							goto IL_064c;
						case 36:
							goto IL_065c;
						case 38:
							goto IL_066c;
						case 40:
							goto IL_067c;
						case 42:
							goto IL_0689;
						case 44:
							goto IL_0696;
						case 46:
							goto IL_06a9;
						case 48:
							goto IL_06b6;
						case 50:
							goto IL_06c3;
						case 52:
							goto IL_06d0;
						case 54:
							goto IL_06dd;
						case 56:
							goto IL_06ea;
						case 23:
						case 25:
						case 27:
						case 29:
						case 31:
						case 33:
						case 35:
						case 37:
						case 39:
						case 41:
						case 43:
						case 45:
						case 47:
						case 49:
						case 51:
						case 53:
						case 55:
						case 57:
						case 58:
							goto IL_06fe;
						case 60:
							goto IL_08b6;
						case 62:
							goto IL_08c3;
						case 64:
							goto IL_08d6;
						case 66:
							goto IL_08e3;
						case 68:
							goto IL_08f0;
						case 70:
							goto IL_08fd;
						case 72:
							goto IL_090a;
						case 74:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 59:
						case 61:
						case 63:
						case 65:
						case 67:
						case 69:
						case 71:
						case 73:
						case 75:
						case 76:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_08d6:
					num2 = 64;
					ONode = "AFO.OASYS";
					goto end_IL_0001_3;
					IL_08c3:
					num2 = 62;
					ONode = InFac + "_PROD_XEUS";
					goto end_IL_0001_3;
					IL_08e3:
					num2 = 66;
					ONode = "F21_PROD_XEUS";
					goto end_IL_0001_3;
					IL_08b6:
					num2 = 60;
					ONode = "D1D_PROD_XEUS";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					InFac = Strings.Trim(Strings.UCase(InFac));
					goto IL_001a;
					IL_001a:
					num2 = 3;
					ANode = InFac + ".ARIES";
					goto IL_002a;
					IL_002a:
					num2 = 4;
					switch (Strings.UCase(InFac))
					{
					case "D1D":
					case "D1C":
						break;
					case "F24":
					case "F28":
					case "F32":
					case "F68":
						goto IL_0201;
					case "T72":
						goto IL_0213;
					case "KM8":
						goto IL_0220;
					case "F21-NMDM FAB":
						goto IL_022d;
					case "F21 (F9)-NMDM DP/WLA":
						goto IL_023a;
					case "F26-PGDM DP/WLA":
						goto IL_0247;
					case "F26-PGDM FAB":
						goto IL_0254;
					default:
						goto IL_0262;
					}
					goto IL_01f5;
					IL_0254:
					num2 = 20;
					ANode = "F26_PROD_XEUS";
					goto IL_0262;
					IL_0247:
					num2 = 18;
					ANode = "PG.ARIES";
					goto IL_0262;
					IL_023a:
					num2 = 16;
					ANode = "F21.ARIES";
					goto IL_0262;
					IL_022d:
					num2 = 14;
					ANode = "F21_PROD_XEUS";
					goto IL_0262;
					IL_0220:
					num2 = 12;
					ANode = "KM.ARIES";
					goto IL_0262;
					IL_0213:
					num2 = 10;
					ANode = "ATD.ARIES";
					goto IL_0262;
					IL_0201:
					num2 = 8;
					ANode = InFac + "_PROD_XEUS";
					goto IL_0262;
					IL_01f5:
					num2 = 6;
					ANode = "D1D_PROD_XEUS";
					goto IL_0262;
					IL_0262:
					num2 = 22;
					switch (Strings.UCase(InFac))
					{
					case "ATD":
						break;
					case "CD":
						goto IL_060c;
					case "CRTO":
						goto IL_061c;
					case "SPTD":
						goto IL_062c;
					case "PG":
						goto IL_063c;
					case "KM":
						goto IL_064c;
					case "KM8":
						goto IL_065c;
					case "T72":
						goto IL_066c;
					case "VN":
						goto IL_067c;
					case "D1D":
					case "D1C":
						goto IL_0689;
					case "F24":
					case "F28":
					case "F32":
					case "F68":
					case "F26":
					case "S33":
						goto IL_0696;
					case "OWLA":
						goto IL_06a9;
					case "F21-NMDM FAB":
						goto IL_06b6;
					case "F21 (F9)-NMDM DP/WLA":
						goto IL_06c3;
					case "F26-PGDM DP/WLA":
						goto IL_06d0;
					case "F26-PGDM FAB":
						goto IL_06dd;
					default:
						goto IL_06ea;
					}
					goto IL_05fc;
					IL_06ea:
					num2 = 56;
					MNode = InFac + ".MARS";
					goto IL_06fe;
					IL_06dd:
					num2 = 54;
					MNode = "F26_PROD_XEUS";
					goto IL_06fe;
					IL_06d0:
					num2 = 52;
					MNode = "PG.[A12_PROD_0.].MARS";
					goto IL_06fe;
					IL_06c3:
					num2 = 50;
					MNode = "F21.[F21_PROD_21.].MARS";
					goto IL_06fe;
					IL_06b6:
					num2 = 48;
					MNode = "F21_PROD_XEUS";
					goto IL_06fe;
					IL_06a9:
					num2 = 46;
					MNode = "AFO.MARS";
					goto IL_06fe;
					IL_0696:
					num2 = 44;
					MNode = InFac + "_PROD_XEUS";
					goto IL_06fe;
					IL_0689:
					num2 = 42;
					MNode = "D1D_PROD_XEUS";
					goto IL_06fe;
					IL_067c:
					num2 = 40;
					MNode = "VN.[A90_PROD_21.].MARS";
					goto IL_06fe;
					IL_066c:
					num2 = 38;
					MNode = "T72.[T72_PROD_72.].MARS";
					goto IL_06fe;
					IL_065c:
					num2 = 36;
					MNode = "KM8.[A88_PROD_21.].MARS";
					goto IL_06fe;
					IL_064c:
					num2 = 34;
					MNode = "KM.[A15_PROD_21.].MARS";
					goto IL_06fe;
					IL_063c:
					num2 = 32;
					MNode = "PG.[A12_PROD_0.].MARS";
					goto IL_06fe;
					IL_062c:
					num2 = 30;
					MNode = "SPTD.[A52_PROD_52.].MARS";
					goto IL_06fe;
					IL_061c:
					num2 = 28;
					MNode = "CRTO.[A61_PROD_4.].MARS";
					goto IL_06fe;
					IL_060c:
					num2 = 26;
					MNode = "CD.[A48_PROD_21.].MARS";
					goto IL_06fe;
					IL_05fc:
					num2 = 24;
					MNode = "ATD.[A43_PROD_0.].MARS";
					goto IL_06fe;
					IL_06fe:
					num2 = 58;
					switch (Strings.UCase(InFac))
					{
					case "D1D":
					case "D1C":
						break;
					case "F24":
					case "F28":
					case "F32":
					case "F68":
						goto IL_08c3;
					case "OWLA":
						goto IL_08d6;
					case "F21-NMDM FAB":
						goto IL_08e3;
					case "F21 (F9)-NMDM DP/WLA":
						goto IL_08f0;
					case "F26-PGDM FAB":
						goto IL_08fd;
					case "F26-PGDM DP/WLA":
						goto IL_090a;
					default:
						goto end_IL_0001_2;
					}
					goto IL_08b6;
					IL_090a:
					num2 = 72;
					ONode = "PG.OASYS";
					goto end_IL_0001_3;
					IL_08fd:
					num2 = 70;
					ONode = "F26_PROD_XEUS";
					goto end_IL_0001_3;
					IL_08f0:
					num2 = 68;
					ONode = "F21.OASYS";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 74;
				ONode = InFac + ".OASYS";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2671;
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

	public static void Set_SH_Type(ref ComboBox cmbtype, string MyMode)
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
							goto IL_001a;
						case 4:
							goto IL_002e;
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
					cmbtype.Items.Clear();
					goto IL_001a;
					IL_001a:
					num2 = 3;
					cmbtype.Items.Add("PYTHON-I311");
					goto IL_002e;
					IL_002e:
					num2 = 4;
					cmbtype.Items.Add("PYTHON-I313");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				cmbtype.SelectedIndex = 0;
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
	}

	public static void Get_DTRBuild_Ini()
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
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
						string text = "";
						errsource = "BuildForm - Get_DTRBuild_Ini";
						bool flag = false;
						bool flag2 = false;
						string text2 = "";
						string text3 = "";
						int num3 = 0;
						int num4 = 0;
						string name = Strings.Trim(Interaction.Environ("TEMP"));
						string text4 = "";
						name = Environment.ExpandEnvironmentVariables(name);
						if (Operators.CompareString(Strings.Right(name, 1), "\\", TextCompare: false) != 0)
						{
							name += "\\";
						}
						ProjectData.ClearProjectError();
						num2 = 2;
						text = "Query Defaults";
						Globals_Renamed.gQueryDir = General_Procedures.Get_Ini_Data(text, "SPF Working Directory II", "NONE", 512, "");
						string text5 = "%userprofile%\\AppData\\Local\\Temp\\SQLPathFinder_Temp\\";
						string name2 = "%userprofile%\\SQLPathFinder_Data\\";
						if (Operators.CompareString(Strings.UCase(Globals_Renamed.gQueryDir), "NONE", TextCompare: false) == 0)
						{
							Globals_Renamed.MyPCDir = Environment.ExpandEnvironmentVariables(text5);
							Globals_Renamed.gQueryDir = Environment.ExpandEnvironmentVariables(name2);
						}
						Globals_Renamed.MyPCDir = General_Procedures.Get_Ini_Data(text, "SPF Temp Directory", Globals_Renamed.MyPCDir, 512, "");
						if (Operators.CompareString(Strings.UCase(Globals_Renamed.MyPCDir), "NONE", TextCompare: false) == 0 || Operators.CompareString(Globals_Renamed.MyPCDir, "", TextCompare: false) == 0)
						{
							Globals_Renamed.MyPCDir = text5;
						}
						Globals_Renamed.MyPCDir = Environment.ExpandEnvironmentVariables(Globals_Renamed.MyPCDir);
						if (Operators.CompareString(Strings.LCase(Globals_Renamed.MyPCDir), Strings.LCase(Environment.ExpandEnvironmentVariables("%userprofile%\\documents\\sqlpathfinder_data\\")), TextCompare: false) == 0)
						{
							Globals_Renamed.MyPCDir = Environment.ExpandEnvironmentVariables(text5);
						}
						if (!MyProject.Computer.FileSystem.DirectoryExists(Globals_Renamed.MyPCDir) && !General_Procedures.MakeDirectory(Globals_Renamed.MyPCDir, DoQuiet: true))
						{
							Globals_Renamed.MyPCDir = name;
						}
						if (!MyProject.Computer.FileSystem.DirectoryExists(Globals_Renamed.gQueryDir) && !General_Procedures.MakeDirectory(Globals_Renamed.gQueryDir, DoQuiet: true))
						{
							Globals_Renamed.gQueryDir = name;
						}
						Globals_Renamed.gSHGUIDir = General_Procedures.Get_Ini_Data(text, "SHGUI Working Directory", Globals_Renamed.gBaseSHGUIDir, 128, "");
						Globals_Renamed.gSHGUIDir = Environment.ExpandEnvironmentVariables(Globals_Renamed.gSHGUIDir);
						Globals_Renamed.gSHGUIDir = SHGUI_Path_Chg(Globals_Renamed.gSHGUIDir);
						Globals_Renamed.MyExcelPath = General_Procedures.Get_Ini_Data(text, "Excel Executable2", "Automatic", 128, "");
						Globals_Renamed.gMyRPath = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "RTERM", MyProject.Application.Info.DirectoryPath + "\\R\\R-Latest\\bin\\x64\\RTerm.exe", 512, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\sqlpathfinder.ini");
						Globals_Renamed.MyJMPPath = General_Procedures.Get_Ini_Data(text, "JMP Executable", "C:\\Program Files\\SAS\\JMPPRO\\14\\jmp.exe", 128, "");
						Globals_Renamed.gMidasDriver = General_Procedures.Get_Ini_Data(text, "MIDAS Driver", "UBER", 128, "");
						if (LikeOperator.LikeString(Globals_Renamed.gMidasDriver, "UBER*", CompareMethod.Binary))
						{
							Globals_Renamed.gMidasDriver = "UBER";
						}
						Globals_Renamed.MyDBSchema = General_Procedures.Get_Ini_Data(text, "Database Schema", "mc_1_", 128, "");
						Globals_Renamed.MyMARSServer = General_Procedures.Get_Ini_Data(text, "MARS Server", "None", 128, "");
						Globals_Renamed.MyARIESServer = General_Procedures.Get_Ini_Data(text, "ARIES Server", "None", 128, "");
						Globals_Renamed.MyOASysServer = General_Procedures.Get_Ini_Data(text, "OASYS Server", "None", 128, "");
						Globals_Renamed.MyOtherServer = General_Procedures.Get_Ini_Data(text, "OTHER Server", "ALL.SPEED", 128, "");
						if (Operators.CompareString(Strings.UCase(Globals_Renamed.MyMARSServer), "NONE", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Globals_Renamed.MyARIESServer), "NONE", TextCompare: false) == 0 || Operators.CompareString(Strings.UCase(Globals_Renamed.MyOASysServer), "NONE", TextCompare: false) == 0 || Operators.CompareString(Strings.Trim(Globals_Renamed.MyMARSServer), "", TextCompare: false) == 0 || Operators.CompareString(Strings.Trim(Globals_Renamed.MyARIESServer), "", TextCompare: false) == 0 || Operators.CompareString(Strings.Trim(Globals_Renamed.MyOASysServer), "", TextCompare: false) == 0)
						{
							text4 = General_Procedures.Get_Ini_Data("INSTALL", "FACILITY", "UNINITIALIZED", 128, Globals_Renamed.MySchemaDir + "\\updatesqlpathfinder.path");
							if (Operators.CompareString(Strings.UCase(text4), "UNINITIALIZED", TextCompare: false) != 0)
							{
								Assign_Facility_Node(text4, ref Globals_Renamed.MyMARSServer, ref Globals_Renamed.MyARIESServer, ref Globals_Renamed.MyOASysServer);
							}
						}
						Globals_Renamed.gLastSHSite = General_Procedures.Get_Ini_Data(text, "SHGUI SITE", "VF", 20, "");
						if (Operators.CompareString(Globals_Renamed.gLastSHSite, "Penang", TextCompare: false) == 0)
						{
							Globals_Renamed.gLastSHSite = "Kulim";
						}
						if (Operators.CompareString(Globals_Renamed.MyDBSchema, "@[]@", TextCompare: false) != 0)
						{
							Globals_Renamed.MyDBSchema = Get_SchemaName_Ini(Globals_Renamed.MyMARSServer);
						}
						Globals_Renamed.gEmailOutlook = General_Procedures.Get_Ini_Data(text, "Use Outlook Email", "N", 10, "");
						Globals_Renamed.gSPFCmdWin = General_Procedures.Get_Ini_Data(text, "Use SPF Command window", "Y", 10, "");
						if (Operators.CompareString(Globals_Renamed.gSPFCmdWin, "Y", TextCompare: false) == 0)
						{
							MyProject.Forms.FrmMain.mnuSQLPos.Visible = true;
						}
						else
						{
							MyProject.Forms.FrmMain.mnuSQLPos.Visible = false;
						}
						Globals_Renamed.gOrclRetry = Strings.UCase(General_Procedures.Get_Ini_Data(text, "OracleRetry", "N", 10, ""));
						Globals_Renamed.gPyDebug = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Python EE Debug", "N", 10, ""));
						Globals_Renamed.gWebNext = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Next Interactive Web", "N", 10, ""));
						Globals_Renamed.gEncodeFFS = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Encode Full FileScan", "N", 10, ""));
						Globals_Renamed.gEncodeUTFBOM = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Set UTF-8-BOM Encoding", "N", 10, ""));
						Globals_Renamed.gConvertMAOUber = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Convert MAO Uber", "N", 10, ""));
						text5 = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PY_DEF", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
						ToolStripMenuItem mnuInstallPyV;
						(mnuInstallPyV = MyProject.Forms.FrmMain.mnuInstallPyV3).Text = mnuInstallPyV.Text + " (" + text5 + ")";
						text5 = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PY_NEXT", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
						(mnuInstallPyV = MyProject.Forms.FrmMain.mnuInstallPyV3Nxt).Text = mnuInstallPyV.Text + " (" + text5 + ")";
						text5 = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "PY_OLD", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
						(mnuInstallPyV = MyProject.Forms.FrmMain.mnuInstallPyV3Old).Text = mnuInstallPyV.Text + " (" + text5 + ")";
						text5 = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "R_DEF", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
						(mnuInstallPyV = MyProject.Forms.FrmMain.mnuInstallRDef).Text = mnuInstallPyV.Text + " (" + text5 + ")";
						text5 = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "R_NEXT", "", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder_Version.ini");
						(mnuInstallPyV = MyProject.Forms.FrmMain.mnuInstallRNxt).Text = mnuInstallPyV.Text + " (" + text5 + ")";
						Globals_Renamed.MyPy3Ver = General_Procedures.Get_Ini_Data("SQLPATHFINDER", "BUILD", "", 25, Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Python3\\SQLPathFinder_Python_Version.ini");
						Globals_Renamed.gUseMinPath = Strings.UCase(General_Procedures.Get_Ini_Data(text, "Minimize PATH2", "Y", 10, ""));
						Globals_Renamed.gCmdWinColor = Strings.UCase(General_Procedures.Get_Ini_Data(text, "CmdWinColor", "BLACKWHITE", 50, ""));
						Globals_Renamed.gCmdWinHist = Strings.UCase(General_Procedures.Get_Ini_Data(text, "CmdWinHistory", "Y", 10, ""));
						text5 = Strings.UCase(General_Procedures.Get_Ini_Data(text, "CmdWinH", "0", 50, ""));
						if (Versioned.IsNumeric(text5))
						{
							Globals_Renamed.gfrmlog_h = Conversions.ToInteger(text5);
						}
						text5 = Strings.UCase(General_Procedures.Get_Ini_Data(text, "CmdWinW", "0", 50, ""));
						if (Versioned.IsNumeric(text5))
						{
							Globals_Renamed.gfrmlog_w = Conversions.ToInteger(text5);
						}
						text = "Colors";
						int num5 = Information.UBound(Globals_Renamed.gGridColor);
						for (num3 = 0; num3 <= num5; num3++)
						{
							text5 = General_Procedures.Get_Ini_Data(text, "Grid_a" + Strings.Trim(Conversions.ToString(num3)), "White", 128, "");
							int num6 = Information.UBound(Globals_Renamed.gStdColors);
							for (num4 = 0; num4 <= num6; num4++)
							{
								if (Operators.CompareString(text5, Globals_Renamed.gStdColors[num4].Name, TextCompare: false) == 0)
								{
									Globals_Renamed.gGridColor[num3].Color = Globals_Renamed.gStdColors[num4];
									break;
								}
							}
						}
						Globals_Renamed.gInstallShare = General_Procedures.Get_Ini_Data("INSTALL", "UPDATE-PATH", "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Updates\\UpdatesIII", 5000, Globals_Renamed.MySchemaDir + "\\updatesqlpathfinder.path");
						goto end_IL_0001;
					}
					case 2564:
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
					goto IL_0a3a;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 2564;
				continue;
			}
			break;
			IL_0a3a:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static void Extra_Nodes()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short nonodes = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					string text;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1977:
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
								goto IL_0018;
							case 5:
								goto IL_0020;
							case 6:
								goto IL_0027;
							case 7:
								goto IL_003e;
							case 8:
								goto IL_0055;
							case 9:
								goto IL_006c;
							case 10:
								goto IL_0084;
							case 11:
								goto IL_008c;
							case 12:
								goto IL_00a4;
							case 13:
								goto IL_00bc;
							case 14:
								goto IL_00d4;
							case 15:
								goto IL_00ec;
							case 16:
								goto IL_00f4;
							case 17:
								goto IL_010c;
							case 18:
								goto IL_0124;
							case 19:
								goto IL_013c;
							case 20:
								goto IL_0154;
							case 21:
								goto IL_015c;
							case 22:
								goto IL_0174;
							case 23:
								goto IL_018c;
							case 24:
								goto IL_01a4;
							case 25:
								goto IL_01bc;
							case 26:
								goto IL_01c4;
							case 27:
								goto IL_01dc;
							case 28:
								goto IL_01f4;
							case 29:
								goto IL_020c;
							case 30:
								goto IL_0224;
							case 31:
								goto IL_022c;
							case 32:
								goto IL_0244;
							case 33:
								goto IL_025c;
							case 34:
								goto IL_0274;
							case 35:
								goto IL_028c;
							case 36:
								goto IL_0294;
							case 37:
								goto IL_02ac;
							case 38:
								goto IL_02c4;
							case 39:
								goto IL_02dc;
							case 40:
								goto IL_02f4;
							case 41:
								goto IL_02fc;
							case 42:
								goto IL_0314;
							case 43:
								goto IL_032c;
							case 44:
								goto IL_0344;
							case 45:
								goto IL_035c;
							case 46:
								goto IL_0364;
							case 47:
								goto IL_037c;
							case 48:
								goto IL_0394;
							case 49:
								goto IL_03ac;
							case 50:
								goto IL_03c4;
							case 51:
								goto IL_03cc;
							case 52:
								goto IL_03e4;
							case 53:
								goto IL_03fc;
							case 54:
								goto IL_0414;
							case 55:
								goto IL_042c;
							case 56:
								goto IL_0444;
							case 57:
								goto IL_044c;
							case 58:
								goto IL_0464;
							case 59:
								goto IL_047c;
							case 60:
								goto IL_0494;
							case 61:
								goto IL_04ac;
							case 62:
								goto IL_04c4;
							case 63:
								goto IL_04cc;
							case 64:
								goto IL_04e4;
							case 65:
								goto IL_04fc;
							case 66:
								goto IL_0514;
							case 67:
								goto IL_052c;
							case 68:
								goto IL_0534;
							case 69:
								goto IL_054c;
							case 70:
								goto IL_0564;
							case 71:
								goto IL_057c;
							case 72:
								goto IL_0594;
							case 73:
								goto IL_059d;
							case 74:
								goto IL_05ae;
							case 75:
								goto IL_05dc;
							case 77:
								goto IL_0603;
							case 76:
							case 78:
							case 79:
								goto IL_061c;
							case 80:
								goto IL_0634;
							case 81:
								goto IL_064c;
							default:
								goto end_IL_0001;
							case 82:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_061c:
						num2 = 79;
						Globals_Renamed.AllNodes[num5].UN = "";
						goto IL_0634;
						IL_0634:
						num2 = 80;
						Globals_Renamed.AllNodes[num5].PW = "";
						goto IL_064c;
						IL_0603:
						num2 = 77;
						Globals_Renamed.AllNodes[num5].DBDATA2 = "";
						goto IL_061c;
						IL_064c:
						num2 = 81;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0654;
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
						num5 = Globals_Renamed.nonodes;
						goto IL_0020;
						IL_0020:
						num2 = 5;
						num5++;
						goto IL_0027;
						IL_0027:
						num2 = 6;
						Globals_Renamed.AllNodes[num5].Name = "ATD.TCA";
						goto IL_003e;
						IL_003e:
						num2 = 7;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_0055;
						IL_0055:
						num2 = 8;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_006c;
						IL_006c:
						num2 = 9;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_0084;
						IL_0084:
						num2 = 10;
						num5++;
						goto IL_008c;
						IL_008c:
						num2 = 11;
						Globals_Renamed.AllNodes[num5].Name = "ALL.TCAP";
						goto IL_00a4;
						IL_00a4:
						num2 = 12;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_00bc;
						IL_00bc:
						num2 = 13;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_00d4;
						IL_00d4:
						num2 = 14;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_00ec;
						IL_00ec:
						num2 = 15;
						num5++;
						goto IL_00f4;
						IL_00f4:
						num2 = 16;
						Globals_Renamed.AllNodes[num5].Name = "ALL.SAMS";
						goto IL_010c;
						IL_010c:
						num2 = 17;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_0124;
						IL_0124:
						num2 = 18;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_013c;
						IL_013c:
						num2 = 19;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_0154;
						IL_0154:
						num2 = 20;
						num5++;
						goto IL_015c;
						IL_015c:
						num2 = 21;
						Globals_Renamed.AllNodes[num5].Name = "ALL.D.SAMS";
						goto IL_0174;
						IL_0174:
						num2 = 22;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_018c;
						IL_018c:
						num2 = 23;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_01a4;
						IL_01a4:
						num2 = 24;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_01bc;
						IL_01bc:
						num2 = 25;
						num5++;
						goto IL_01c4;
						IL_01c4:
						num2 = 26;
						Globals_Renamed.AllNodes[num5].Name = "ATD.MKC";
						goto IL_01dc;
						IL_01dc:
						num2 = 27;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_01f4;
						IL_01f4:
						num2 = 28;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_020c;
						IL_020c:
						num2 = 29;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_0224;
						IL_0224:
						num2 = 30;
						num5++;
						goto IL_022c;
						IL_022c:
						num2 = 31;
						Globals_Renamed.AllNodes[num5].Name = "DETD-PCS.[A52_PROD_52.].MARS";
						goto IL_0244;
						IL_0244:
						num2 = 32;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_025c;
						IL_025c:
						num2 = 33;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_0274;
						IL_0274:
						num2 = 34;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_028c;
						IL_028c:
						num2 = 35;
						num5++;
						goto IL_0294;
						IL_0294:
						num2 = 36;
						Globals_Renamed.AllNodes[num5].Name = "DETD-PCS.OASYS";
						goto IL_02ac;
						IL_02ac:
						num2 = 37;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_02c4;
						IL_02c4:
						num2 = 38;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_02dc;
						IL_02dc:
						num2 = 39;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_02f4;
						IL_02f4:
						num2 = 40;
						num5++;
						goto IL_02fc;
						IL_02fc:
						num2 = 41;
						Globals_Renamed.AllNodes[num5].Name = "DETD-PCS.ARIES";
						goto IL_0314;
						IL_0314:
						num2 = 42;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_032c;
						IL_032c:
						num2 = 43;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_0344;
						IL_0344:
						num2 = 44;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_035c;
						IL_035c:
						num2 = 45;
						num5++;
						goto IL_0364;
						IL_0364:
						num2 = 46;
						Globals_Renamed.AllNodes[num5].Name = "PG-DISCOVERY.[A12_PROD_0.].MARS";
						goto IL_037c;
						IL_037c:
						num2 = 47;
						Globals_Renamed.AllNodes[num5].ORCL = "Y";
						goto IL_0394;
						IL_0394:
						num2 = 48;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_03ac;
						IL_03ac:
						num2 = 49;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:ORACLE";
						goto IL_03c4;
						IL_03c4:
						num2 = 50;
						num5++;
						goto IL_03cc;
						IL_03cc:
						num2 = 51;
						Globals_Renamed.AllNodes[num5].Name = "All.I.Montester";
						goto IL_03e4;
						IL_03e4:
						num2 = 52;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_03fc;
						IL_03fc:
						num2 = 53;
						Globals_Renamed.AllNodes[num5].ORCL = "N";
						goto IL_0414;
						IL_0414:
						num2 = 54;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:SQLSERVER";
						goto IL_042c;
						IL_042c:
						num2 = 55;
						Globals_Renamed.AllNodes[num5].Desc = "Montester DB";
						goto IL_0444;
						IL_0444:
						num2 = 56;
						num5++;
						goto IL_044c;
						IL_044c:
						num2 = 57;
						Globals_Renamed.AllNodes[num5].Name = "IMO.PMS";
						goto IL_0464;
						IL_0464:
						num2 = 58;
						Globals_Renamed.AllNodes[num5].ORCL = "N";
						goto IL_047c;
						IL_047c:
						num2 = 59;
						Globals_Renamed.AllNodes[num5].Display = "Y";
						goto IL_0494;
						IL_0494:
						num2 = 60;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:SQLSERVER";
						goto IL_04ac;
						IL_04ac:
						num2 = 61;
						Globals_Renamed.AllNodes[num5].Desc = "IMO PMS Data (Santa Clara, CA)";
						goto IL_04c4;
						IL_04c4:
						num2 = 62;
						num5++;
						goto IL_04cc;
						IL_04cc:
						num2 = 63;
						Globals_Renamed.AllNodes[num5].Name = "ALL.BLS";
						goto IL_04e4;
						IL_04e4:
						num2 = 64;
						Globals_Renamed.AllNodes[num5].ORCL = "N";
						goto IL_04fc;
						IL_04fc:
						num2 = 65;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_0514;
						IL_0514:
						num2 = 66;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:SQLSERVER";
						goto IL_052c;
						IL_052c:
						num2 = 67;
						num5++;
						goto IL_0534;
						IL_0534:
						num2 = 68;
						Globals_Renamed.AllNodes[num5].Name = "ALL.GEDI";
						goto IL_054c;
						IL_054c:
						num2 = 69;
						Globals_Renamed.AllNodes[num5].ORCL = "N";
						goto IL_0564;
						IL_0564:
						num2 = 70;
						Globals_Renamed.AllNodes[num5].Display = "N";
						goto IL_057c;
						IL_057c:
						num2 = 71;
						Globals_Renamed.AllNodes[num5].DBDATA = "DB:SQLSERVER";
						goto IL_0594;
						IL_0594:
						num2 = 72;
						Globals_Renamed.nonodes = num5;
						goto IL_059d;
						IL_059d:
						num2 = 73;
						nonodes = Globals_Renamed.nonodes;
						num5 = 0;
						goto IL_0654;
						IL_0654:
						if (num5 > nonodes)
						{
							goto end_IL_0001_2;
						}
						goto IL_05ae;
						IL_05ae:
						num2 = 74;
						if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "*@USER-PROMPT@", CompareMethod.Binary))
						{
							goto IL_05dc;
						}
						goto IL_0603;
						IL_05dc:
						num2 = 75;
						Globals_Renamed.AllNodes[num5].DBDATA2 = Globals_Renamed.AllNodes[num5].DBDATA;
						goto IL_061c;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1977;
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

	public static void Load_Operators(string MyOperatorFile)
	{
		int try0001_dispatch = -1;
		object MyReader = default(object);
		bool flag = default(bool);
		string MyMsg = default(string);
		string text3 = default(string);
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
						short num3 = 0;
						string text = "";
						string text2 = "";
						MyReader = null;
						flag = false;
						MyMsg = "";
						string[] array = null;
						text3 = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						num3 = 0;
						text3 = Globals_Renamed.MySchemaDir + "\\" + Strings.Trim(MyOperatorFile);
						flag = OpenDelimitedFile(text3, ref MyReader, ref MyMsg);
						if (!flag)
						{
							break;
						}
						while (true)
						{
							if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
							{
								array = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
								if (!VerifySchemaRow(ref array, 4, ref MyMsg))
								{
									goto end_IL_0001;
								}
								Globals_Renamed.gOperators[num3].DB_Type = array[0];
								Globals_Renamed.gOperators[num3].Name = array[1];
								Globals_Renamed.gOperators[num3].Type = array[2];
								Globals_Renamed.gOperators[num3].Description = array[3];
								text2 = Globals_Renamed.gOperators[num3].DB_Type;
								text = Strings.Mid(text2 + " ", 1, 1);
								if (Operators.CompareString(text, "!", TextCompare: false) != 0)
								{
									Globals_Renamed.gOperators[num3].DB_Type = Strings.UCase(Globals_Renamed.gOperators[num3].DB_Type);
									Globals_Renamed.gOperators[num3].Type = Strings.UCase(Globals_Renamed.gOperators[num3].Type);
									num3++;
								}
								array = null;
								continue;
							}
							array = null;
							if (flag)
							{
								NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
								NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
								flag = false;
							}
							Globals_Renamed.gNoOperators = (short)(num3 - 1);
							break;
						}
						goto end_IL_0001_2;
					}
					case 632:
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
						MyMsg = Conversion.ErrorToString();
					}
					Interaction.MsgBox("Error loading Operator File:  " + text3 + ". " + MyMsg, MsgBoxStyle.Exclamation, "Load Error");
					if (flag)
					{
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						flag = false;
					}
					break;
				}
				end_IL_0001_3:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 632;
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

	public static void Start_SHGUI()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
					int num3 = 0;
					ProjectData.ClearProjectError();
					num2 = 2;
					if (MyProject.Computer.FileSystem.FileExists("C:\\Program Files (x86)\\Intel\\ScriptHostJobManager\\ScriptHostJobManager.exe"))
					{
						num3 = Interaction.Shell("C:\\Program Files (x86)\\Intel\\ScriptHostJobManager\\ScriptHostJobManager.exe", AppWinStyle.NormalFocus);
					}
					else
					{
						Interaction.MsgBox("Unable to locate ScriptHost Job Manager. Make sure it is installed from Intel Software Market", MsgBoxStyle.Exclamation, "SH Error");
					}
					goto end_IL_0001;
				}
				case 147:
					num = -1;
					switch (num2)
					{
					case 2:
						Interaction.MsgBox("Error starting ScriptHost Job Manager. Try again." + General_Procedures.Get_UI("errhelp0") + " " + Conversion.ErrorToString() + ".", MsgBoxStyle.Critical, "SH Error");
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 147;
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

	public static void File_Multi(short MyMode, ref string FileNames, ref int MyCount)
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
						errsource = "BuildForm - File_Multi";
						string text = "";
						string text2 = "";
						string text3 = "";
						short num3 = 0;
						string text4 = "";
						FileNames = "";
						MyCount = 0;
						text2 = ",";
						MyProject.Forms.FrmMain.CMDialog1Open.Filter = "CSV/TAB Files (*.csv;*.tab)|*.csv;*.CSV;*.tab;*.TAB|All Files (*.*)|*.*";
						MyProject.Forms.FrmMain.CMDialog1Open.DefaultExt = "csv";
						MyProject.Forms.FrmMain.CMDialog1Open.Title = "Select One or More Files";
						MyProject.Forms.FrmMain.CMDialog1Open.InitialDirectory = Globals_Renamed.MyPCDir;
						MyProject.Forms.FrmMain.CMDialog1Open.FileName = "";
						MyProject.Forms.FrmMain.CMDialog1Open.Multiselect = true;
						MyProject.Forms.FrmMain.CMDialog1Open.DereferenceLinks = true;
						MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
						MyProject.Forms.FrmMain.CMDialog1Open.CheckFileExists = true;
						MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
						MyProject.Forms.FrmMain.CMDialog1Open.FilterIndex = 1;
						if (MyProject.Forms.FrmMain.CMDialog1Open.ShowDialog() != DialogResult.OK)
						{
							goto end_IL_0001;
						}
						IEnumerator enumerator = MyProject.Forms.FrmMain.CMDialog1Open.FileNames.GetEnumerator();
						while (enumerator.MoveNext())
						{
							MyCount++;
							text4 = Conversions.ToString(enumerator.Current);
							num3 = (short)Strings.InStrRev(text4, "\\");
							if (num3 != 0)
							{
								text = Strings.UCase(Strings.Mid(text4, 1, num3));
								if (Operators.CompareString(text, Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) == 0 || MyMode == 2)
								{
									FileNames = FileNames + text3 + Strings.Mid(text4, num3 + 1);
								}
								else if (Operators.CompareString(text, Strings.UCase(Globals_Renamed.gQueryDir), TextCompare: false) == 0)
								{
									FileNames = FileNames + text3 + "<<<spf-query-dir>>>" + Strings.Mid(text4, num3 + 1);
								}
								else
								{
									FileNames = FileNames + text3 + text4;
								}
							}
							text3 = text2;
						}
						goto end_IL_0001_2;
					}
					case 652:
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
					goto IL_02c2;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 652;
				continue;
			}
			break;
			IL_02c2:
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

	public static int File_Multi2(ref IEnumerator FilesEnum)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int num4 = default(int);
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
					errsource = "BuildForm - File_Multi2";
					string text = "";
					short num3 = 0;
					string text2 = "";
					num4 = 0;
					FilesEnum = null;
					MyProject.Forms.FrmMain.CMDialog1Open.Title = "Select One or More SQLPathFinder Query Files";
					MyProject.Forms.FrmMain.CMDialog1Open.Filter = "SQLPathFinder Query (*.vgq, *.vg2, *.vge, *.vgec)|*.vgq;*.VGQ;*.vg2;*.VG2;*.vge;*.VGE;*.vgec;*.VGEC|VA Scripts (*.va)|*.va;*.VA|SQLPathFinder SQL Scripts (*.spfsql)|*.spfsql;*.SPFSQL|Batch Scripts (*.bat)|*.bat;*.BAT|AllFiles (*.*)|*.*";
					MyProject.Forms.FrmMain.CMDialog1Open.DefaultExt = "vg2";
					MyProject.Forms.FrmMain.CMDialog1Open.FileName = "";
					MyProject.Forms.FrmMain.CMDialog1Open.Multiselect = true;
					MyProject.Forms.FrmMain.CMDialog1Open.DereferenceLinks = true;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckPathExists = true;
					MyProject.Forms.FrmMain.CMDialog1Open.CheckFileExists = true;
					MyProject.Forms.FrmMain.CMDialog1Open.ShowReadOnly = false;
					MyProject.Forms.FrmMain.CMDialog1Open.FilterIndex = 1;
					MyProject.Forms.FrmMain.CMDialog1Open.InitialDirectory = Globals_Renamed.gQueryDir;
					if (MyProject.Forms.FrmMain.CMDialog1Open.ShowDialog() == DialogResult.OK)
					{
						FilesEnum = MyProject.Forms.FrmMain.CMDialog1Open.FileNames.GetEnumerator();
						while (FilesEnum.MoveNext())
						{
							num4 = checked(num4 + 1);
						}
					}
					else
					{
						num4 = 0;
					}
					goto end_IL_0001;
				}
				case 426:
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
				try0001_dispatch = 426;
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
		return num4;
	}

	public static string GetDBTypeIL(short MyMode, string FileName, string ll_DBType, short ll_ObjectType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string text = default(string);
		string result = default(string);
		short num6 = default(short);
		string text2 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					short num7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1433:
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
								goto IL_0019;
							case 5:
								goto IL_001f;
							case 7:
								goto IL_0038;
							case 8:
								goto IL_003c;
							case 9:
								goto IL_0065;
							case 11:
								goto IL_0071;
							case 12:
								goto IL_007a;
							case 13:
								goto IL_0087;
							case 15:
								goto IL_0097;
							case 16:
								goto IL_00a4;
							case 18:
								goto IL_00b4;
							case 19:
								goto IL_00dc;
							case 21:
								goto IL_00f6;
							case 22:
								goto IL_010e;
							case 24:
								goto IL_0125;
							case 25:
								goto IL_013d;
							case 27:
								goto IL_0154;
							case 28:
								goto IL_018c;
							case 14:
							case 17:
							case 20:
							case 23:
							case 26:
							case 29:
							case 30:
								goto IL_01a1;
							case 32:
								goto IL_01c0;
							case 33:
								goto IL_01d4;
							case 34:
								goto IL_01e2;
							case 31:
							case 35:
							case 36:
								goto IL_01f3;
							case 37:
								goto IL_01ff;
							case 38:
								goto IL_023a;
							case 39:
							case 40:
								goto IL_0246;
							case 41:
								goto IL_0266;
							case 42:
								goto IL_0279;
							case 43:
								goto IL_0287;
							case 45:
								goto IL_02b0;
							case 49:
								goto IL_02d3;
							case 50:
								goto IL_02dd;
							case 51:
								goto IL_02f2;
							case 52:
								goto IL_0304;
							case 53:
								goto IL_0312;
							case 54:
								goto IL_0325;
							case 55:
								goto IL_033a;
							case 56:
							case 57:
							case 58:
							case 59:
								goto IL_0361;
							case 60:
								goto IL_037a;
							case 61:
								goto IL_0384;
							case 62:
								goto IL_03ab;
							case 64:
								goto IL_03b7;
							case 65:
								goto IL_03d5;
							case 67:
								goto IL_03e2;
							case 68:
								goto IL_03fb;
							case 69:
								goto IL_040e;
							case 70:
								goto IL_041c;
							case 72:
								goto IL_0434;
							case 71:
							case 73:
							case 74:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 6:
							case 10:
							case 44:
							case 46:
							case 47:
							case 48:
							case 63:
							case 66:
							case 75:
							case 76:
							case 77:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0266:
						num2 = 41;
						num5 = (short)Strings.InStrRev(FileName, ".");
						goto IL_0279;
						IL_0279:
						num2 = 42;
						if (num5 != 0)
						{
							goto IL_0287;
						}
						goto IL_02b0;
						IL_0246:
						num2 = 40;
						text = Strings.UCase("(" + Strings.Trim(text) + ")");
						goto IL_0266;
						IL_0287:
						num2 = 43;
						result = Strings.Trim(Strings.Mid(FileName, 1, num5 - 1)) + " " + text + ".ilv";
						goto end_IL_0001_3;
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
						num6 = 0;
						goto IL_001f;
						IL_001f:
						num2 = 5;
						num7 = MyMode;
						if (num7 == 0)
						{
							goto IL_0038;
						}
						if (num7 != 1)
						{
							goto end_IL_0001_3;
						}
						goto IL_02d3;
						IL_02b0:
						num2 = 45;
						result = Strings.Trim(FileName) + " " + text + ".ilv";
						goto end_IL_0001_3;
						IL_02d3:
						num2 = 49;
						text = "";
						goto IL_02dd;
						IL_02dd:
						num2 = 50;
						if (LikeOperator.LikeString(FileName, "*(*)*", CompareMethod.Binary))
						{
							goto IL_02f2;
						}
						goto IL_0361;
						IL_02f2:
						num2 = 51;
						num5 = (short)Strings.InStr(FileName, "(");
						goto IL_0304;
						IL_0304:
						num2 = 52;
						if (num5 != 0)
						{
							goto IL_0312;
						}
						goto IL_0361;
						IL_0312:
						num2 = 53;
						num6 = (short)Strings.InStrRev(FileName, ")");
						goto IL_0325;
						IL_0325:
						num2 = 54;
						if (unchecked(num6 != 0 && num6 > num5))
						{
							goto IL_033a;
						}
						goto IL_0361;
						IL_033a:
						num2 = 55;
						text = Strings.Trim(Strings.UCase(Strings.Mid(FileName, num5 + 1, (short)unchecked(num6 - num5) - 1)));
						goto IL_0361;
						IL_0361:
						num2 = 59;
						if (Operators.CompareString(text, "", TextCompare: false) == 0)
						{
							goto IL_037a;
						}
						goto IL_0384;
						IL_037a:
						num2 = 60;
						text = "MAO";
						goto IL_0384;
						IL_0384:
						num2 = 61;
						if ((Operators.CompareString(text, "TEXT", TextCompare: false) == 0) | LikeOperator.LikeString(text, "ALL.*", CompareMethod.Binary))
						{
							goto IL_03ab;
						}
						goto IL_03b7;
						IL_03ab:
						num2 = 62;
						result = text;
						goto end_IL_0001_3;
						IL_03b7:
						num2 = 64;
						if (Operators.CompareString(Strings.UCase(text), "SQLITE", TextCompare: false) == 0)
						{
							goto IL_03d5;
						}
						goto IL_03e2;
						IL_03d5:
						num2 = 65;
						result = "SQLite";
						goto end_IL_0001_3;
						IL_03e2:
						num2 = 67;
						text2 = Strings.Trim(General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1));
						goto IL_03fb;
						IL_03fb:
						num2 = 68;
						num5 = (short)Strings.InStr(text2, ".");
						goto IL_040e;
						IL_040e:
						num2 = 69;
						if (num5 != 0)
						{
							goto IL_041c;
						}
						goto IL_0434;
						IL_041c:
						num2 = 70;
						text2 = Strings.Trim(Strings.Mid(text2, 1, num5));
						break;
						IL_0434:
						num2 = 72;
						text2 = "F12.";
						break;
						IL_0038:
						num2 = 7;
						result = FileName;
						goto IL_003c;
						IL_003c:
						num2 = 8;
						if ((Operators.CompareString(Strings.Trim(FileName), "", TextCompare: false) == 0) | LikeOperator.LikeString(FileName, "*(*)*", CompareMethod.Binary))
						{
							goto IL_0065;
						}
						goto IL_0071;
						IL_0065:
						num2 = 9;
						result = FileName;
						goto end_IL_0001_3;
						IL_0071:
						num2 = 11;
						result = "";
						goto IL_007a;
						IL_007a:
						num2 = 12;
						if (ll_ObjectType == 1)
						{
							goto IL_0087;
						}
						goto IL_0097;
						IL_0087:
						num2 = 13;
						text = "SQLite";
						goto IL_01a1;
						IL_0097:
						num2 = 15;
						if (ll_ObjectType == 3)
						{
							goto IL_00a4;
						}
						goto IL_00b4;
						IL_00a4:
						num2 = 16;
						text = "TEXT";
						goto IL_01a1;
						IL_00b4:
						num2 = 18;
						if ((Operators.CompareString(ll_DBType, "0", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "1", TextCompare: false) == 0))
						{
							goto IL_00dc;
						}
						goto IL_00f6;
						IL_00dc:
						num2 = 19;
						text = General_Procedures.GetNodeToUse(Globals_Renamed.MyMARSServer, Globals_Renamed.MyMARSServer1);
						goto IL_01a1;
						IL_00f6:
						num2 = 21;
						if (Operators.CompareString(ll_DBType, "2", TextCompare: false) == 0)
						{
							goto IL_010e;
						}
						goto IL_0125;
						IL_010e:
						num2 = 22;
						text = General_Procedures.GetNodeToUse(Globals_Renamed.MyARIESServer, Globals_Renamed.MyARIESServer1);
						goto IL_01a1;
						IL_0125:
						num2 = 24;
						if (Operators.CompareString(ll_DBType, "3", TextCompare: false) == 0)
						{
							goto IL_013d;
						}
						goto IL_0154;
						IL_013d:
						num2 = 25;
						text = General_Procedures.GetNodeToUse(Globals_Renamed.MyOASysServer, Globals_Renamed.MyOASysServer1);
						goto IL_01a1;
						IL_0154:
						num2 = 27;
						if ((Operators.CompareString(ll_DBType, "5", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "9", TextCompare: false) == 0) | (Operators.CompareString(ll_DBType, "A", TextCompare: false) == 0))
						{
							goto IL_018c;
						}
						goto IL_01a1;
						IL_018c:
						num2 = 28;
						text = General_Procedures.GetNodeToUse(Globals_Renamed.MyOtherServer, Globals_Renamed.MyOtherServer1);
						goto IL_01a1;
						IL_01a1:
						num2 = 30;
						if (!LikeOperator.LikeString(Strings.UCase(text), "ALL.*", CompareMethod.Binary))
						{
							goto IL_01c0;
						}
						goto IL_01f3;
						IL_01c0:
						num2 = 32;
						num5 = (short)Strings.InStrRev(text, ".");
						goto IL_01d4;
						IL_01d4:
						num2 = 33;
						if (num5 != 0)
						{
							goto IL_01e2;
						}
						goto IL_01f3;
						IL_01e2:
						num2 = 34;
						text = Strings.Mid(text, num5 + 1);
						goto IL_01f3;
						IL_01f3:
						num2 = 36;
						text = Strings.UCase(text);
						goto IL_01ff;
						IL_01ff:
						num2 = 37;
						if ((Operators.CompareString(text, "MARS", TextCompare: false) == 0) | (Operators.CompareString(text, "ARIES", TextCompare: false) == 0) | (Operators.CompareString(text, "OASYS", TextCompare: false) == 0))
						{
							goto IL_023a;
						}
						goto IL_0246;
						IL_023a:
						num2 = 38;
						text = "MAO";
						goto IL_0246;
						end_IL_0001_2:
						break;
					}
					num2 = 74;
					result = text2 + text;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1433;
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

	public static void Extract_IG_ColNo(ref string MyFile, ref string MyCol)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short num6 = default(short);
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
					case 280:
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
								goto IL_001f;
							case 6:
								goto IL_0031;
							case 7:
								goto IL_003d;
							case 9:
								goto IL_004a;
							case 10:
								goto IL_005e;
							case 11:
								goto IL_0071;
							case 12:
								goto IL_007f;
							case 13:
							case 14:
								goto IL_0097;
							case 15:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 8:
							case 16:
							case 17:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0071:
						num2 = 11;
						if (num5 != 0)
						{
							goto IL_007f;
						}
						goto IL_0097;
						IL_007f:
						num2 = 12;
						MyCol = Strings.Trim(Strings.Mid(MyCol, 1, num5 - 1));
						goto IL_0097;
						IL_005e:
						num2 = 10;
						num5 = (short)Strings.InStr(MyCol, ".");
						goto IL_0071;
						IL_0097:
						num2 = 14;
						MyFile = Strings.Trim(Strings.Mid(MyFile, 1, num6 - 1));
						break;
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
						MyFile = Strings.Trim(MyFile);
						goto IL_001f;
						IL_001f:
						num2 = 5;
						num6 = (short)Strings.InStrRev(MyFile, ":");
						goto IL_0031;
						IL_0031:
						num2 = 6;
						if (num6 == 0)
						{
							goto IL_003d;
						}
						goto IL_004a;
						IL_003d:
						num2 = 7;
						MyCol = "1";
						goto end_IL_0001_3;
						IL_004a:
						num2 = 9;
						MyCol = Strings.Trim(Strings.Mid(MyFile, num6 + 1));
						goto IL_005e;
						end_IL_0001_2:
						break;
					}
					num2 = 15;
					MyCol = General_Procedures.StripQuotesAndDates(MyCol, "C", 0);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 280;
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

	public static string SHGUI_Path_Chg(string MyIn)
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
							goto IL_000f;
						case 4:
							goto IL_003c;
						case 5:
							goto IL_0052;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					if (Operators.CompareString(MyIn, "", TextCompare: false) == 0 || Strings.InStr(MyIn.ToUpper(), "SCRIPTHOST") == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_003c;
					IL_0052:
					num2 = 5;
					text = Strings.Replace(text, "\\\\ScriptHost\\SHUser$\\", "\\\\SHUser-Intg.intel.com\\SHIntgUser$\\", 1, -1, CompareMethod.Text);
					break;
					IL_000b:
					num2 = 2;
					text = MyIn;
					goto IL_000f;
					IL_003c:
					num2 = 4;
					text = Strings.Replace(text, "\\\\ScriptHost\\SHUser\\", "\\\\SHUser-Intg.intel.com\\SHIntgUser$\\", 1, -1, CompareMethod.Text);
					goto IL_0052;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				text = Strings.Replace(text, "\\\\ScriptHost-Prod\\SHUser$\\", "\\\\SHuser-Prod.Intel.com\\SHProdUser$\\", 1, -1, CompareMethod.Text);
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
		return text;
	}

	public static string GetSHGUISubDir(string MySHGUIDir)
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
				case 238:
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
							goto IL_0047;
						case 6:
							goto IL_005e;
						case 7:
							goto IL_008b;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0047:
					num2 = 5;
					text = Strings.Trim(Strings.Mid(text, checked(Strings.Len(text2) + 1)));
					goto IL_005e;
					IL_005e:
					num2 = 6;
					if (!((Operators.CompareString(text, "", TextCompare: false) != 0) & (Operators.CompareString(Strings.Right(text, 1), "\\", TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					goto IL_008b;
					IL_0022:
					num2 = 4;
					if (!LikeOperator.LikeString(Strings.UCase(text), Strings.UCase(text2) + "*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_0047;
					IL_008b:
					num2 = 7;
					text += "\\";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					text2 = Strings.UCase(Globals_Renamed.gBaseSHGUIDir);
					goto IL_0019;
					IL_0019:
					num2 = 3;
					text = Strings.Trim(MySHGUIDir);
					goto IL_0022;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				text = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 238;
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

	public static void Load_Functions(string MyFunctionFile)
	{
		int try0001_dispatch = -1;
		object MyReader = default(object);
		bool flag = default(bool);
		string MyMsg = default(string);
		string text3 = default(string);
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
						short num3 = 0;
						string text = "";
						string text2 = "";
						MyReader = null;
						flag = false;
						MyMsg = "";
						string[] array = null;
						text3 = "";
						ProjectData.ClearProjectError();
						num2 = 2;
						num3 = 0;
						text3 = Globals_Renamed.MySchemaDir + "\\" + MyFunctionFile;
						flag = OpenDelimitedFile(text3, ref MyReader, ref MyMsg);
						if (!flag)
						{
							break;
						}
						while (true)
						{
							if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
							{
								array = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
								if (!VerifySchemaRow(ref array, 3, ref MyMsg))
								{
									goto end_IL_0001;
								}
								Globals_Renamed.Functions[num3].Name = array[0];
								Globals_Renamed.Functions[num3].Type = array[1];
								Globals_Renamed.Functions[num3].Description = array[2];
								text2 = Globals_Renamed.Functions[num3].Name;
								text = Strings.Mid(text2, 1, 1);
								if (Operators.CompareString(text, "!", TextCompare: false) != 0)
								{
									Globals_Renamed.Functions[num3].Type = Strings.UCase(Globals_Renamed.Functions[num3].Type);
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
							Globals_Renamed.nofunctions = (short)(num3 - 1);
							break;
						}
						goto end_IL_0001_2;
					}
					case 560:
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
						MyMsg = Conversion.ErrorToString();
					}
					Interaction.MsgBox("Error loading Function File: " + text3 + ". " + MyMsg, MsgBoxStyle.Exclamation, "Load Error");
					if (flag)
					{
						NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						flag = false;
					}
					break;
				}
				end_IL_0001_3:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 560;
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

	public static string Get_QueryTitle(string MyFile)
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
							goto IL_0010;
						case 4:
							goto IL_0014;
						case 5:
							goto IL_0026;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0010:
					num2 = 3;
					result = MyFile;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num5 = checked((short)Strings.InStrRev(MyFile, "\\"));
					goto IL_0026;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0026:
					num2 = 5;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				result = Strings.Trim(Strings.Mid(MyFile + " ", checked(num5 + 1)));
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
		return result;
	}

	public static void Make_Headers_Unique2(ref string[] CurrentRow, bool IsR = false)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string[] array = default(string[]);
		int num5 = default(int);
		string[] array2 = default(string[]);
		string[] array3 = default(string[]);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		bool flag = default(bool);
		string text = default(string);
		string text2 = default(string);
		int num9 = default(int);
		int num10 = default(int);
		int num11 = default(int);
		bool flag2 = default(bool);
		bool flag3 = default(bool);
		string text3 = default(string);
		string text4 = default(string);
		int num12 = default(int);
		int num13 = default(int);
		int num14 = default(int);
		int num15 = default(int);
		int num16 = default(int);
		int num17 = default(int);
		int num18 = default(int);
		int num20 = default(int);
		int num22 = default(int);
		int num24 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num19;
					int num21;
					int num23;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 1793:
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
								goto IL_0023;
							case 8:
								goto IL_002c;
							case 9:
								goto IL_0035;
							case 10:
								goto IL_003b;
							case 11:
								goto IL_0048;
							case 12:
								goto IL_004e;
							case 13:
								goto IL_005c;
							case 14:
								goto IL_006a;
							case 15:
								goto IL_0078;
							case 16:
								goto IL_007e;
							case 17:
								goto IL_0084;
							case 18:
								goto IL_008e;
							case 19:
								goto IL_0098;
							case 20:
								goto IL_00a3;
							case 21:
								goto IL_00af;
							case 22:
								goto IL_00bb;
							case 23:
								goto IL_00c7;
							case 24:
								goto IL_00d3;
							case 25:
								goto IL_00de;
							case 26:
								goto IL_0132;
							case 27:
								goto IL_013e;
							case 28:
								goto IL_0149;
							case 29:
								goto IL_0157;
							case 30:
								goto IL_016c;
							case 31:
								goto IL_0180;
							case 32:
								goto IL_0195;
							case 33:
								goto IL_01a9;
							case 34:
								goto IL_01bd;
							case 35:
								goto IL_01d2;
							case 36:
								goto IL_01e1;
							case 37:
								goto IL_01ee;
							case 39:
								goto IL_01fc;
							case 38:
							case 40:
							case 41:
								goto IL_0207;
							case 42:
								goto IL_0217;
							case 43:
								goto IL_0236;
							case 44:
								goto IL_023c;
							case 45:
								goto IL_024c;
							case 46:
								goto IL_0262;
							case 48:
								goto IL_027f;
							case 49:
								goto IL_0288;
							case 50:
								goto IL_0291;
							case 51:
								goto IL_0297;
							case 52:
								goto IL_029d;
							case 53:
								goto IL_02af;
							case 54:
								goto IL_02c8;
							case 56:
							case 57:
								goto IL_02d2;
							case 55:
							case 58:
								goto IL_02e1;
							case 59:
								goto IL_02f2;
							case 60:
								goto IL_02fb;
							case 61:
								goto IL_0311;
							case 62:
								goto IL_0331;
							case 63:
								goto IL_0358;
							case 64:
								goto IL_037d;
							case 65:
								goto IL_0389;
							case 66:
								goto IL_039a;
							case 67:
							case 68:
							case 70:
							case 71:
								goto IL_03ac;
							case 73:
								goto IL_03c2;
							case 69:
							case 72:
							case 74:
							case 75:
								goto IL_03ca;
							case 76:
								goto IL_03dc;
							case 77:
								goto IL_03e9;
							case 79:
								goto IL_0403;
							case 78:
							case 80:
							case 81:
								goto IL_041c;
							case 82:
								goto IL_042d;
							case 83:
								goto IL_043e;
							case 84:
								goto IL_0447;
							case 47:
							case 85:
							case 86:
							case 87:
								goto IL_0459;
							case 88:
								goto IL_0468;
							case 89:
								goto IL_0473;
							case 91:
								goto IL_048e;
							case 92:
								goto IL_0499;
							case 93:
								goto IL_04a4;
							case 94:
								goto IL_04bd;
							case 95:
								goto IL_04c9;
							case 96:
								goto IL_04d4;
							case 97:
								goto IL_04df;
							case 98:
								goto IL_04f3;
							case 99:
								goto IL_0507;
							case 90:
							case 100:
							case 101:
							case 102:
								goto IL_0518;
							case 103:
								goto IL_051e;
							case 104:
								goto IL_0524;
							case 105:
								goto IL_052a;
							case 106:
								goto IL_0530;
							case 107:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 108:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00a3:
						num2 = 20;
						array[num5] = "";
						goto IL_00af;
						IL_00af:
						num2 = 21;
						array2[num5] = "";
						goto IL_00bb;
						IL_0132:
						num2 = 26;
						num5++;
						goto IL_0139;
						IL_00bb:
						num2 = 22;
						array3[num5] = "";
						goto IL_00c7;
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
						num7 = -1;
						goto IL_0019;
						IL_0019:
						num2 = 5;
						num8 = -1;
						goto IL_001e;
						IL_001e:
						num2 = 6;
						flag = false;
						goto IL_0023;
						IL_0023:
						num2 = 7;
						text = "";
						goto IL_002c;
						IL_002c:
						num2 = 8;
						text2 = "";
						goto IL_0035;
						IL_0035:
						num2 = 9;
						num9 = 0;
						goto IL_003b;
						IL_003b:
						num2 = 10;
						num10 = Information.UBound(CurrentRow);
						goto IL_0048;
						IL_0048:
						num2 = 11;
						num11 = 0;
						goto IL_004e;
						IL_004e:
						num2 = 12;
						array = new string[num10 + 1];
						goto IL_005c;
						IL_005c:
						num2 = 13;
						array2 = new string[num10 + 1];
						goto IL_006a;
						IL_006a:
						num2 = 14;
						array3 = new string[num10 + 1];
						goto IL_0078;
						IL_0078:
						num2 = 15;
						flag2 = false;
						goto IL_007e;
						IL_007e:
						num2 = 16;
						flag3 = false;
						goto IL_0084;
						IL_0084:
						num2 = 17;
						text3 = "";
						goto IL_008e;
						IL_008e:
						num2 = 18;
						text4 = "";
						goto IL_0098;
						IL_0098:
						num2 = 19;
						num12 = num10;
						num5 = 0;
						goto IL_00ce;
						IL_00ce:
						if (num5 <= num12)
						{
							goto IL_00a3;
						}
						goto IL_00d3;
						IL_00d3:
						num2 = 24;
						num13 = num10;
						num5 = 0;
						goto IL_0139;
						IL_0139:
						if (num5 <= num13)
						{
							goto IL_00de;
						}
						goto IL_013e;
						IL_013e:
						num2 = 27;
						Array.Sort(array);
						goto IL_0149;
						IL_0149:
						num2 = 28;
						num14 = num10;
						num5 = 0;
						goto IL_01d9;
						IL_01d9:
						if (num5 <= num14)
						{
							goto IL_0157;
						}
						goto IL_01e1;
						IL_01e1:
						num2 = 36;
						if (!IsR)
						{
							goto IL_01ee;
						}
						goto IL_01fc;
						IL_01ee:
						num2 = 37;
						text4 = "_0";
						goto IL_0207;
						IL_01fc:
						num2 = 39;
						text4 = ".1";
						goto IL_0207;
						IL_0207:
						num2 = 41;
						num15 = num10 - 1;
						num5 = 0;
						goto IL_0460;
						IL_0460:
						if (num5 <= num15)
						{
							goto IL_0217;
						}
						goto IL_0468;
						IL_0468:
						num2 = 88;
						if (flag2)
						{
							goto IL_0473;
						}
						goto IL_048e;
						IL_0473:
						num2 = 89;
						Interaction.MsgBox("Could not make headers unique. Too many duplicates.", MsgBoxStyle.Exclamation, "Header Issue");
						goto IL_0518;
						IL_048e:
						num2 = 91;
						if (flag3)
						{
							goto IL_0499;
						}
						goto IL_0518;
						IL_0499:
						num2 = 92;
						num16 = num10;
						num5 = 0;
						goto IL_04c4;
						IL_04c4:
						if (num5 <= num16)
						{
							goto IL_04a4;
						}
						goto IL_04c9;
						IL_04c9:
						num2 = 95;
						Array.Sort(array);
						goto IL_04d4;
						IL_04d4:
						num2 = 96;
						num17 = num10;
						num5 = 0;
						goto IL_050e;
						IL_050e:
						if (num5 <= num17)
						{
							goto IL_04df;
						}
						goto IL_0518;
						IL_04df:
						num2 = 97;
						num18 = Strings.InStr(array[num5], ":");
						goto IL_04f3;
						IL_04f3:
						num2 = 98;
						CurrentRow[num5] = Strings.Mid(array[num5], num18 + 1);
						goto IL_0507;
						IL_0507:
						num2 = 99;
						num5++;
						goto IL_050e;
						IL_04a4:
						num2 = 93;
						array[num5] = array2[num5] + ":" + array3[num5];
						goto IL_04bd;
						IL_04bd:
						num2 = 94;
						num5++;
						goto IL_04c4;
						IL_0518:
						num2 = 102;
						array = null;
						goto IL_051e;
						IL_051e:
						num2 = 103;
						array2 = null;
						goto IL_0524;
						IL_0524:
						num2 = 104;
						array3 = null;
						goto IL_052a;
						IL_052a:
						num2 = 105;
						array = null;
						goto IL_0530;
						IL_0530:
						num2 = 106;
						array2 = null;
						break;
						IL_0217:
						num2 = 42;
						if (Operators.CompareString(array[num5], array[num5 + 1], TextCompare: false) == 0)
						{
							goto IL_0236;
						}
						goto IL_0459;
						IL_0236:
						num2 = 43;
						flag3 = true;
						goto IL_023c;
						IL_023c:
						num2 = 44;
						if (num5 == num10 - 1)
						{
							goto IL_024c;
						}
						goto IL_027f;
						IL_024c:
						num2 = 45;
						array[num5 + 1] = array[num5 + 1] + text4;
						goto IL_0262;
						IL_0262:
						num2 = 46;
						array3[num5 + 1] = array3[num5 + 1] + text4;
						goto IL_0459;
						IL_027f:
						num2 = 48;
						text = array[num5];
						goto IL_0288;
						IL_0288:
						num2 = 49;
						text2 = array3[num5];
						goto IL_0291;
						IL_0291:
						num2 = 50;
						flag = true;
						goto IL_0297;
						IL_0297:
						num2 = 51;
						num7 = 0;
						goto IL_029d;
						IL_029d:
						num2 = 52;
						num19 = num5 + 2;
						num20 = num10;
						num6 = num19;
						goto IL_02db;
						IL_02db:
						if (num6 <= num20)
						{
							goto IL_02af;
						}
						goto IL_02e1;
						IL_02af:
						num2 = 53;
						if (Operators.CompareString(array[num6], text, TextCompare: false) != 0)
						{
							goto IL_02c8;
						}
						goto IL_02d2;
						IL_02c8:
						num2 = 54;
						flag = false;
						goto IL_02e1;
						IL_02e1:
						num2 = 58;
						if (!flag)
						{
							goto IL_02f2;
						}
						goto IL_03c2;
						IL_02f2:
						num2 = 59;
						num6--;
						goto IL_02fb;
						IL_02fb:
						num2 = 60;
						num21 = num6 + 1;
						num22 = num10;
						num8 = num21;
						goto IL_03b5;
						IL_03b5:
						if (num8 <= num22)
						{
							goto IL_0311;
						}
						goto IL_03ca;
						IL_0311:
						num2 = 61;
						if (LikeOperator.LikeString(array[num8], text + "_*", CompareMethod.Binary))
						{
							goto IL_0331;
						}
						goto IL_03ca;
						IL_0331:
						num2 = 62;
						text3 = Strings.Trim(Strings.Mid(array[num8] + "  ", Strings.Len(text) + 2));
						goto IL_0358;
						IL_0358:
						num2 = 63;
						if (Versioned.IsNumeric(text3) && Strings.InStr(text3, ".") == 0)
						{
							goto IL_037d;
						}
						goto IL_03ac;
						IL_00c7:
						num2 = 23;
						num5++;
						goto IL_00ce;
						IL_037d:
						num2 = 64;
						num9 = Conversions.ToInteger(text3);
						goto IL_0389;
						IL_0389:
						num2 = 65;
						if (num9 + 1 > num7)
						{
							goto IL_039a;
						}
						goto IL_03ac;
						IL_039a:
						num2 = 66;
						num7 = num9 + 1;
						goto IL_03ac;
						IL_03ac:
						num2 = 71;
						num8++;
						goto IL_03b5;
						IL_03c2:
						num2 = 73;
						num6 = num10;
						goto IL_03ca;
						IL_03ca:
						num2 = 75;
						num23 = num5 + 1;
						num24 = num6;
						num8 = num23;
						goto IL_0450;
						IL_0450:
						if (num8 <= num24)
						{
							goto IL_03dc;
						}
						goto IL_0459;
						IL_03dc:
						num2 = 76;
						if (!IsR)
						{
							goto IL_03e9;
						}
						goto IL_0403;
						IL_03e9:
						num2 = 77;
						text4 = "_" + Conversions.ToString(num7);
						goto IL_041c;
						IL_0403:
						num2 = 79;
						text4 = "." + Conversions.ToString(num7 + 1);
						goto IL_041c;
						IL_041c:
						num2 = 81;
						array[num8] = text + text4;
						goto IL_042d;
						IL_042d:
						num2 = 82;
						array3[num8] = text2 + text4;
						goto IL_043e;
						IL_043e:
						num2 = 83;
						num7++;
						goto IL_0447;
						IL_0447:
						num2 = 84;
						num8++;
						goto IL_0450;
						IL_02d2:
						num2 = 57;
						num6++;
						goto IL_02db;
						IL_0459:
						num2 = 87;
						num5++;
						goto IL_0460;
						IL_0157:
						num2 = 29;
						num18 = Strings.InStrRev(array[num5], ":");
						goto IL_016c;
						IL_016c:
						num2 = 30;
						array2[num5] = Strings.Mid(array[num5], num18 + 1);
						goto IL_0180;
						IL_0180:
						num2 = 31;
						array[num5] = Strings.Mid(array[num5], 1, num18 - 1);
						goto IL_0195;
						IL_0195:
						num2 = 32;
						num11 = Strings.InStr(array2[num5], "@@@@@");
						goto IL_01a9;
						IL_01a9:
						num2 = 33;
						array3[num5] = Strings.Mid(array2[num5], num11 + 5);
						goto IL_01bd;
						IL_01bd:
						num2 = 34;
						array2[num5] = Strings.Mid(array2[num5], 1, num11 - 1);
						goto IL_01d2;
						IL_01d2:
						num2 = 35;
						num5++;
						goto IL_01d9;
						IL_00de:
						num2 = 25;
						array[num5] = Strings.LCase(Strings.Trim(CurrentRow[num5])) + ":" + Strings.Mid(Conversions.ToString(1000000 + num5), 2) + "@@@@@" + Strings.Trim(CurrentRow[num5]);
						goto IL_0132;
						end_IL_0001_2:
						break;
					}
					num2 = 107;
					array3 = null;
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1793;
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

	public static int CSVHdrToArray(ref string[] CurrentRow, string CurrentFile, string MyMode)
	{
		int num = 0;
		MyMode = Strings.Trim(Strings.UCase(MyMode));
		string text = ",";
		int num2 = 0;
		int num3 = 0;
		text = GetFileDLM(CurrentFile);
		if (Strings.InStrRev(CurrentFile, "\\") == 0)
		{
			CurrentFile = Globals_Renamed.MyPCDir + CurrentFile;
		}
		checked
		{
			if (File.Exists(CurrentFile))
			{
				try
				{
					TextFieldParser textFieldParser = new TextFieldParser(CurrentFile);
					try
					{
						if (!textFieldParser.EndOfData)
						{
							textFieldParser.TextFieldType = FieldType.Delimited;
							textFieldParser.SetDelimiters(text);
							textFieldParser.HasFieldsEnclosedInQuotes = true;
							textFieldParser.TrimWhiteSpace = true;
							if (!textFieldParser.EndOfData)
							{
								CurrentRow = textFieldParser.ReadFields();
							}
						}
						else
						{
							num = 2;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Interaction.MsgBox("Error loading CSV file: " + CurrentFile + ". (" + ex2.Message + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Load Error");
						num = 1;
						ProjectData.ClearProjectError();
					}
					finally
					{
						textFieldParser.Close();
						textFieldParser.Dispose();
						textFieldParser = null;
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex4.Message + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Access Error");
					num = 1;
					ProjectData.ClearProjectError();
				}
				if (num == 0)
				{
					try
					{
						if (Operators.CompareString(Strings.Mid(MyMode, 1, 1), "Y", TextCompare: false) == 0)
						{
							int num4 = Information.UBound(CurrentRow);
							for (num3 = 0; num3 <= num4; num3++)
							{
								CurrentRow[num3] = Strings.LCase(CurrentRow[num3]);
							}
						}
						if (Operators.CompareString(Strings.Mid(MyMode, 2, 1), "Y", TextCompare: false) == 0)
						{
							int num5 = Information.UBound(CurrentRow);
							for (num3 = 0; num3 <= num5; num3++)
							{
								CurrentRow[num3] = Strings.Replace(CurrentRow[num3], "[", "(", 1, -1, CompareMethod.Text);
								CurrentRow[num3] = Strings.Replace(CurrentRow[num3], "]", ")", 1, -1, CompareMethod.Text);
								CurrentRow[num3] = Strings.Replace(CurrentRow[num3], ",", ";", 1, -1, CompareMethod.Text);
								CurrentRow[num3] = Strings.Replace(CurrentRow[num3], "\t", " ", 1, -1, CompareMethod.Text);
								CurrentRow[num3] = Strings.Replace(CurrentRow[num3], "\"", "", 1, -1, CompareMethod.Text);
							}
						}
						if (Operators.CompareString(Strings.Mid(MyMode, 3, 1), "Y", TextCompare: false) == 0)
						{
							Make_Headers_Unique2(ref CurrentRow);
						}
					}
					catch (Exception ex5)
					{
						ProjectData.SetProjectError(ex5);
						Exception ex6 = ex5;
						Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex6.Message + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Access Error");
						num = 1;
						ProjectData.ClearProjectError();
					}
				}
			}
			return num;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static string ProcessCSVHeaders(string CurrentFile, short l_Mode, ref string lSQLiteTable)
	{
		int num = 0;
		string text = "";
		string text2 = "";
		int num2 = 0;
		string text3 = "";
		string text4 = Globals_Renamed.MySchemaDir + "\\SQLPathFinder.csvSchema$";
		int num3 = 0;
		short num4 = 255;
		string text5 = "";
		string text6 = "";
		int num5 = 0;
		int num6 = 0;
		string[] CurrentRow = new string[0];
		int num7 = 0;
		string text7 = "N";
		string left = "";
		if (Operators.CompareString(lSQLiteTable, "", TextCompare: false) == 0)
		{
			lSQLiteTable = "N/A";
		}
		checked
		{
			if (Operators.CompareString(Strings.Right(Strings.Trim(Strings.UCase(CurrentFile)), 4), ".SDB", TextCompare: false) == 0)
			{
				text6 = Strings.Trim(CurrentFile) + "Schema$";
				num3 = Strings.InStrRev(text6, "\\");
				if (num3 != 0)
				{
					text6 = Strings.Trim(Strings.Mid(text6, num3 + 1));
				}
				text6 = Globals_Renamed.MyPCDir + text6;
				num7 = 2;
				text4 = Globals_Renamed.MySchemaDir + "\\SQLPathFinder.sdbSchema$";
				if (Operators.CompareString(lSQLiteTable, "", TextCompare: false) != 0 && Operators.CompareString(lSQLiteTable, "N/A", TextCompare: false) != 0)
				{
					text3 = General_Procedures.OpenReadFileContents(text4);
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						text3 = Strings.Replace(text3, "@SQLITE@", lSQLiteTable, 1, -1, CompareMethod.Text);
						left = General_Procedures.Save_File_General(text3, text6);
					}
					text3 = "";
				}
			}
			else
			{
				text6 = Strings.Trim(CurrentFile) + "Schema$";
				num3 = Strings.InStrRev(text6, "\\");
				if (num3 != 0)
				{
					text6 = Strings.Trim(Strings.Mid(text6, num3 + 1));
				}
				if (LikeOperator.LikeString(text6, "*<<<*>>>*", CompareMethod.Binary))
				{
					text6 = "spf.csvSchema$";
				}
				text6 = Globals_Renamed.MyPCDir + text6;
			}
			switch (l_Mode)
			{
			case 3:
				num4 = 255;
				break;
			case 1:
				num4 = 32675;
				text7 = "Y";
				break;
			}
			string text8 = text6;
			if (!File.Exists(CurrentFile))
			{
				if (Operators.CompareString(Strings.Right(Strings.Trim(Strings.UCase(CurrentFile)), 4), ".SDB", TextCompare: false) != 0 && Operators.CompareString(left, "", TextCompare: false) == 0)
				{
					try
					{
						Microsoft.VisualBasic.FileSystem.FileCopy(text4, text6);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Interaction.MsgBox("Error copying CSV Source Schema, (" + text4 + "), to work directory, (" + text6 + "). (" + Conversion.ErrorToString() + "). Check your work folder." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Copy Error");
						text8 = "";
						ProjectData.ClearProjectError();
					}
				}
			}
			else
			{
				switch (num7)
				{
				case 2:
					num5 = Get_SQLite_Schema(CurrentFile, text8, ref lSQLiteTable);
					break;
				default:
					switch ((l_Mode != 1) ? CSVHdrToArray(ref CurrentRow, CurrentFile, "NNN") : CSVHdrToArray(ref CurrentRow, CurrentFile, "YY" + text7))
					{
					case 0:
						try
						{
							num = Microsoft.VisualBasic.FileSystem.FreeFile();
							Microsoft.VisualBasic.FileSystem.FileOpen(num, text6, OpenMode.Output);
							try
							{
								text = ((l_Mode != 1) ? "" : ("!USES SQLITE FOR CSV PROCESSING" + Globals_Renamed.CRLF));
								text += "!======================================";
								text = text + Globals_Renamed.CRLF + "!Column Delimited file storing:";
								text = text + Globals_Renamed.CRLF + "! 1. Table/List Name";
								text = text + Globals_Renamed.CRLF + "! 2. Folder Name";
								text = text + Globals_Renamed.CRLF + "! 3. Column Name for display with datatype in parenthesis";
								text = text + Globals_Renamed.CRLF + "! 4. Display Option:";
								text = text + Globals_Renamed.CRLF + "!      K or KC = Keyed Column";
								text = text + Globals_Renamed.CRLF + "!      C or KC = Important column";
								text = text + Globals_Renamed.CRLF + "!      R       = Potential Row Filter";
								text = text + Globals_Renamed.CRLF + "! 5. True column name or expression with (3) referenced as column name alias (AS), or blank if you can use (3)";
								text = text + Globals_Renamed.CRLF + "! 6. Column Description";
								text = text + Globals_Renamed.CRLF + "! 7. Column Help. STD->TXT results in General Help for CSV Files";
								text = text + Globals_Renamed.CRLF + "!Table,Column,Display,Description";
								text = text + Globals_Renamed.CRLF + "!======================================";
								text = text + Globals_Renamed.CRLF + "!START ";
								Microsoft.VisualBasic.FileSystem.PrintLine(num, text);
								if (Information.UBound(CurrentRow) + 1 > num4)
								{
									Interaction.MsgBox("The CSV processor requires files with fewer than " + Conversions.ToString(unchecked((int)num4)) + " columns. (" + CurrentFile + ")", MsgBoxStyle.Exclamation, "Too many Columns");
									text8 = "";
									break;
								}
								num6 = Information.UBound(CurrentRow);
								if (l_Mode == 1 && Information.UBound(CurrentRow) + 1 > 1999)
								{
									Interaction.MsgBox("This view has many columns (" + Conversions.ToString(num6) + "). However, only the first " + Conversions.ToString(1999) + " will be shown. To output more columns, use column pattern matching", MsgBoxStyle.Information, "Only " + Conversions.ToString(1999) + " Columns shown");
									num6 = 1999;
								}
								text2 = " (c)";
								if (l_Mode == 1)
								{
									text = "CSV,Columns,\"row_id (n)\",,\"[rowid]\",\"Unique Row Identifier. When processing text files, increments from 2 to n+1 (where n is the number of rows of data). Otherwise goes from 1 to n.\",\"STD->TXT\"";
									text += "\r\nCSV,Columns,\"[select-column-pattern] (c)\",,\"<ai><;><ti>\",\"Select columns matching a pattern\",\"\"";
								}
								int num8 = num6;
								for (num2 = 0; num2 <= num8; num2++)
								{
									text3 = Strings.Trim(CurrentRow[num2]);
									text = ((!unchecked(num2 == 0 && l_Mode != 1)) ? (text + Globals_Renamed.CRLF + "CSV,Columns,\"" + text3 + text2 + "\",C,\"[" + text3 + "]\",\"\",\"STD->TXT\"") : ("CSV,Columns,\"" + text3 + text2 + "\",C,\"[" + text3 + "]\",\"\",\"STD->TXT\""));
								}
								Microsoft.VisualBasic.FileSystem.PrintLine(num, text);
							}
							catch (Exception ex5)
							{
								ProjectData.SetProjectError(ex5);
								Exception ex6 = ex5;
								Interaction.MsgBox("Error loading CSV file: " + CurrentFile + ". (" + ex6.Message + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Load Error");
								text8 = "";
								ProjectData.ClearProjectError();
							}
							finally
							{
								Microsoft.VisualBasic.FileSystem.FileClose(num);
							}
						}
						catch (Exception ex7)
						{
							ProjectData.SetProjectError(ex7);
							Exception ex8 = ex7;
							Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex8.Message + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Access Error");
							text8 = "";
							ProjectData.ClearProjectError();
						}
						break;
					case 2:
						try
						{
							text4 = Globals_Renamed.MySchemaDir + "\\SQLPathFinder.csvSchema$";
							Microsoft.VisualBasic.FileSystem.FileCopy(text4, text6);
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Interaction.MsgBox("Error copying SQLPathFinder CSV Source Schema, (" + text4 + "), to work directory, (" + text6 + "). (" + Conversion.ErrorToString() + "). Check your work folder." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Copy Error");
							text8 = "";
							ProjectData.ClearProjectError();
						}
						break;
					default:
						text8 = "";
						break;
					}
					break;
				case 1:
					break;
				}
			}
			CurrentRow = null;
			return text8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static string ProcessDuckDB(string CurrentFile)
	{
		string text = "";
		int num = 0;
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string str = "";
		string text7 = CurrentFile;
		if (Operators.CompareString(Strings.Mid(Strings.UCase(CurrentFile), 1, 7), "DUCKDB:", TextCompare: false) == 0)
		{
			CurrentFile = Strings.Mid(CurrentFile, 8);
		}
		if (Strings.InStrRev(text7, "\\") == 0)
		{
			text7 = Globals_Renamed.MyPCDir + Strings.Trim(CurrentFile);
		}
		num = Strings.InStrRev(CurrentFile, ".");
		checked
		{
			if (num != 0)
			{
				str = Strings.Mid(CurrentFile, 1, num - 1);
			}
			text = Strings.Trim(str) + ".DuckDBSchema$";
			num = Strings.InStrRev(text, "\\");
			if (num != 0)
			{
				text = Strings.Trim(Strings.Mid(text, num + 1));
			}
			text = Globals_Renamed.MyPCDir + text;
			if (!File.Exists(text7) || LikeOperator.LikeString(text, "*<<<*>>>*", CompareMethod.Binary))
			{
				text3 = Globals_Renamed.MySchemaDir + "\\SQLPathFinder.DuckDBSchema$";
				try
				{
					Microsoft.VisualBasic.FileSystem.FileCopy(text3, text);
					string text8 = text;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox("Error copying DuckDB Source Schema, (" + text3 + "), to your work directory. (" + Conversion.ErrorToString() + "). Check your work folder." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Copy Error");
					text = "";
					ProjectData.ClearProjectError();
				}
			}
			else
			{
				if (Strings.InStrRev(text7, "\\") != 0)
				{
					text7 = Strings.Replace(text7, "\\", "/", 1, -1, CompareMethod.Text);
				}
				string text9 = ((Strings.InStrRev(text, "\\") == 0) ? text : Strings.Replace(text, "\\", "/", 1, -1, CompareMethod.Text));
				text4 = General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\" && \"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Generate_Any_Schema.py\" /DBType=DuckDB /DB=\"" + text7 + "\" /Alias=DEFAULT  /Out=\"" + text9 + "\" /SPFINSTANCE=\"" + Globals_Renamed.gSPFCache + "\" ", 1, 1, 0);
				if (Operators.CompareString(text4, "", TextCompare: false) != 0)
				{
					Interaction.MsgBox("Error Generating DuckDB Schema. (" + text4 + ").", MsgBoxStyle.OkOnly, "Schema Error");
					text = "";
				}
			}
			return text;
		}
	}

	public static string Get_SQLite_TableName(string MyTable)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
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
							goto IL_0019;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_0030;
						case 7:
							goto IL_003d;
						case 8:
							goto IL_004c;
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0030:
					num2 = 6;
					if (num5 != 0)
					{
						goto IL_003d;
					}
					goto IL_004c;
					IL_003d:
					num2 = 7;
					text = Strings.Mid(text, checked(num5 + 1));
					goto IL_004c;
					IL_001e:
					num2 = 5;
					num5 = Strings.InStrRev(text, "\\");
					goto IL_0030;
					IL_004c:
					num2 = 8;
					text = General_Procedures.Replace_Special_Chars(Strings.Trim(Strings.Replace(text, ".sdb", "", 1, -1, CompareMethod.Text)));
					break;
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
					text = MyTable;
					goto IL_001e;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				result = text + "_Status";
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static short Get_SQLite_Schema(string MySDB, string MySchemaf, ref string lSQLiteTable)
	{
		short result = 1;
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		if (Operators.CompareString(lSQLiteTable, "", TextCompare: false) != 0 && Operators.CompareString(lSQLiteTable, "N/A", TextCompare: false) != 0)
		{
			text2 = "/TABLE=\"" + lSQLiteTable + "\"";
		}
		text = ((Operators.CompareString(FNUsePyEngine(), "Y", TextCompare: false) != 0) ? General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\" &&va \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Generate_SQLite_Schema.va\" /SDB=\"" + MySDB + "\"/Out=\"" + MySchemaf + "\"/LIMIT=\"Y\" " + text2, 1, 1, 0) : General_Procedures.Run_Batch_Shell("cmd /c cd \"" + Globals_Renamed.MyPCDir + "\" && \"" + Globals_Renamed.gMyPyPath + "\" -s \"" + Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Generate_Any_Schema.py\" /DBType=SQLite /SDB=\"" + MySDB + "\" /Alias=a0  /Out=\"" + MySchemaf + "\" /Limit=Y /SPFINSTANCE=\"" + Globals_Renamed.gSPFCache + "\" " + text2, 1, 1, 0));
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			Interaction.MsgBox("Error Generating SQLite Schema. (" + text + ").", MsgBoxStyle.OkOnly, "Schema Error");
		}
		else if (File.Exists(Strip_Add_MyPCDir("A", MySchemaf)))
		{
			StreamReader streamReader = new StreamReader(MySchemaf);
			try
			{
				text3 = streamReader.ReadLine();
				if (LikeOperator.LikeString(Strings.UCase(text3), "!TABLE=*", CompareMethod.Binary) && text3.Length > 7 && !LikeOperator.LikeString(Strings.UCase(text3), "!TABLE=N/A*", CompareMethod.Binary))
				{
					lSQLiteTable = Strings.Trim(Strings.Mid(text3, 8));
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error obtaining Table Name from SQLite Schema File: " + MySchemaf + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Missing Table");
				ProjectData.ClearProjectError();
			}
			finally
			{
				streamReader.Close();
				streamReader.Dispose();
				streamReader = null;
			}
		}
		else
		{
			text2 = Globals_Renamed.MySchemaDir + "\\SQLPathFinder.sdbSchema$";
			try
			{
				Microsoft.VisualBasic.FileSystem.FileCopy(text2, MySchemaf);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Interaction.MsgBox("Error copying SQLite Source Schema, (" + text2 + "), to work directory, (" + MySchemaf + "). (" + Conversion.ErrorToString() + "). Check your work folder." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Copy Error");
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	public static string Get_Operator_Comment_SQL(string Operator_Renamed)
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
						errsource = "BuildForm - Get_Operator_Comment_SQL";
						short num3 = 0;
						Operator_Renamed = Strings.LCase(Operator_Renamed);
						if (LikeOperator.LikeString(Operator_Renamed, "in group(*", CompareMethod.Binary))
						{
							Operator_Renamed = "in group(=;=)";
						}
						result = "Unknown Operator";
						short gNoOperators = Globals_Renamed.gNoOperators;
						for (num3 = 0; num3 <= gNoOperators; num3 = (short)unchecked(num3 + 1))
						{
							if ((Operators.CompareString(Strings.Mid(Globals_Renamed.gOperators[num3].DB_Type, 1, 1), "F", TextCompare: false) == 0 || Operators.CompareString(Strings.Mid(Globals_Renamed.gOperators[num3].DB_Type, 1, 1), "M", TextCompare: false) == 0) & (Operators.CompareString(Strings.LCase(Globals_Renamed.gOperators[num3].Name), Operator_Renamed, TextCompare: false) == 0))
							{
								result = Globals_Renamed.gOperators[num3].Description;
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
		return result;
	}

	public static string Get_CSVTXT_Opts(string ll_InFile, short ToRun, string MyMode)
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
				case 202:
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
							goto IL_0041;
						case 5:
							goto IL_004d;
						case 6:
							goto IL_006e;
						case 7:
							goto IL_0079;
						case 8:
						case 9:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 4:
						case 10:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004d:
					num2 = 5;
					if (!(Operators.CompareString(Strings.Trim(ll_InFile), "", TextCompare: false) != 0 && ToRun == 1))
					{
						break;
					}
					goto IL_006e;
					IL_006e:
					num2 = 6;
					ll_InFile = Replace_Globals(ll_InFile);
					goto IL_0079;
					IL_0041:
					num2 = 3;
					result = "&PROMPT&(EXCE)&PROMPT&";
					goto end_IL_0001_3;
					IL_0079:
					num2 = 7;
					Globals_Renamed.ODBC_Prompt = ll_InFile;
					break;
					IL_000b:
					num2 = 2;
					if ((Operators.CompareString(MyMode, "S", TextCompare: false) == 0 || ToRun == 1) & (Operators.CompareString(Strings.Trim(Strings.UCase(ll_InFile)), "PROMPT", TextCompare: false) == 0))
					{
						goto IL_0041;
					}
					goto IL_004d;
					end_IL_0001_2:
					break;
				}
				num2 = 9;
				result = ll_InFile;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 202;
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

	public static bool IsCSV(short ll_ObjectType)
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
				case 80:
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
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (!(ll_ObjectType == 3 || ll_ObjectType == 1))
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 80;
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

	public static string Assign_Col_Level(string MyMode, string MySumm, string MyList, string l_Alias)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		string text2 = default(string);
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
						errsource = "BuildForm - Assign_Col_Level";
						short num3 = 0;
						string text = "";
						num3 = (short)Strings.InStr(l_Alias, "->");
						l_Alias = ((num3 == 0) ? Strings.LCase(l_Alias) : Strings.LCase(Strings.Mid(l_Alias, 1, num3 - 1)));
						l_Alias = l_Alias switch
						{
							"txt" => "t", 
							"sql" => "s", 
							"ilv" => "i", 
							_ => (Operators.CompareString(l_Alias, "all", TextCompare: false) == 0) ? "a" : "v", 
						};
						if (!LikeOperator.LikeString(Strings.UCase(MyList), "@IF@*", CompareMethod.Binary))
						{
							text2 = ((General_Procedures.SummPlusLevel(MyList) <= 0) ? "0" : MyList);
						}
						else
						{
							text = Strings.Trim(Strings.Mid(MyList + " ", Strings.Len("@IF@") + 1));
							if ((Operators.CompareString(Strings.Mid(text + " ", 1, 1), "0", TextCompare: false) < 0) | (Operators.CompareString(Strings.Mid(text + " ", 1, 1), "9", TextCompare: false) > 0))
							{
								text = "1";
							}
							text2 = text;
							if (Operators.CompareString(text2, "", TextCompare: false) == 0)
							{
								text2 = "1";
							}
						}
						if (Operators.CompareString(MyMode, "C", TextCompare: false) != 0)
						{
							if (Operators.CompareString(MyMode, "F", TextCompare: false) == 0 && Strings.Len(MySumm) >= 2)
							{
								MySumm = Strings.LCase(Strings.Mid(MySumm, 2, 1));
								if (Operators.CompareString(MySumm, "s", TextCompare: false) == 0)
								{
									text2 = "S";
								}
							}
						}
						else if (Operators.CompareString(Strings.UCase(MySumm), "EXPR", TextCompare: false) == 0)
						{
							text2 = "S";
						}
						text2 = l_Alias + "," + text2;
						goto end_IL_0001;
					}
					case 617:
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
				try0001_dispatch = 617;
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
		return text2;
	}

	public static string SubStitute_Nodes_SQL(string MySQL)
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
						errsource = "Buildform - SubStitute_Nodes_SQL";
						text = MySQL;
						short num3 = 0;
						string text2 = "";
						string text3 = "";
						num3 = 1;
						do
						{
							switch (num3)
							{
							case 1:
								text2 = "DEFAULT (MARS)";
								text3 = Globals_Renamed.MyMARSServer;
								break;
							case 2:
								text2 = "DEFAULT (OASYS)";
								text3 = Globals_Renamed.MyOASysServer;
								break;
							case 3:
								text2 = "DEFAULT (ARIES)";
								text3 = Globals_Renamed.MyARIESServer;
								break;
							default:
								text2 = "DEFAULT (OTHER)";
								text3 = Globals_Renamed.MyOtherServer;
								break;
							}
							text = Strings.Replace(text, text2, text3, 1, -1, CompareMethod.Text);
							num3 = (short)unchecked(num3 + 1);
						}
						while (num3 <= 4);
						goto end_IL_0001;
					}
					case 207:
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
				try0001_dispatch = 207;
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

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void CheckILVSchema(string ILVFile)
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
				switch (try0001_dispatch)
				{
				default:
				{
					ProjectData.ClearProjectError();
					num2 = 2;
					errsource = "BuildForm - CheckILVSchema";
					text = "";
					string text2 = "";
					short num3 = 0;
					if (!MyProject.Computer.FileSystem.FileExists(ILVFile))
					{
						num3 = checked((short)Strings.InStrRev(ILVFile, "\\"));
						text2 = ((num3 == 0) ? "" : Strings.UCase(Strings.Mid(ILVFile, 1, num3)));
						if (Operators.CompareString(text2, Strings.UCase(Globals_Renamed.MyPCDir), TextCompare: false) == 0)
						{
							text = Globals_Renamed.MySchemaDir + "\\$SQLPathFinder$ (MAO).ilv";
							ProjectData.ClearProjectError();
							num2 = 3;
							Microsoft.VisualBasic.FileSystem.FileCopy(text, ILVFile);
							ProjectData.ClearProjectError();
							num2 = 4;
						}
					}
					goto end_IL_0001;
				}
				case 319:
					num = -1;
					switch (num2)
					{
					case 3:
						Interaction.MsgBox("Error copying Inline View Schema, (" + text + "), to work folder. (" + Conversion.ErrorToString() + "). Check your work folder path." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Critical, "Copy Error");
						Information.Err().Clear();
						goto end_IL_0001;
					case 2:
					case 4:
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
				try0001_dispatch = 319;
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

	public static void SetOutputOpt(string MyMode, ref string oVal)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		frmfilename frmfilename2 = default(frmfilename);
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
							goto IL_0013;
						case 4:
							goto IL_001c;
						case 5:
							goto IL_0024;
						case 6:
							goto IL_002d;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_001c:
					num2 = 4;
					frmfilename2 = new frmfilename();
					goto IL_0024;
					IL_0024:
					num2 = 5;
					frmfilename2.ShowDialog();
					goto IL_002d;
					IL_0013:
					num2 = 3;
					Globals_Renamed.currvaluetmp = oVal;
					goto IL_001c;
					IL_002d:
					num2 = 6;
					if (Operators.CompareString(Globals_Renamed.currvaluetmp, "CANCEL", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					Globals_Renamed.currinputstrtmp = MyMode;
					goto IL_0013;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				oVal = Globals_Renamed.currvaluetmp;
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

	public static short Get_CTab_Opts(ref string sOpt, ref string sQuery, short SegNo, string d_node, string d_oledb, string d_Engine, string d_WorkDir, string d_UN, string d_PW, string d_T, string d_ROWS, string d_CSV, string d_EXP, string d_LastO, short ToRun, string MyMode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		string @string = default(string);
		string text = default(string);
		string ll_InFile = default(string);
		string text2 = default(string);
		int num10 = default(int);
		int num11 = default(int);
		string source = default(string);
		string text3 = default(string);
		short result = default(short);
		string[] DynArray = default(string[]);
		int num12 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					int num5;
					int num9;
					bool flag;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 5695:
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
								goto IL_0024;
							case 8:
								goto IL_0029;
							case 9:
								goto IL_0032;
							case 10:
								goto IL_003c;
							case 11:
								goto IL_0046;
							case 12:
								goto IL_0050;
							case 13:
								goto IL_0056;
							case 14:
								goto IL_005c;
							case 15:
								goto IL_0066;
							case 16:
								goto IL_0070;
							case 17:
								goto IL_007a;
							case 18:
								goto IL_007f;
							case 19:
								goto IL_00a5;
							case 20:
								goto IL_00bc;
							case 21:
								goto IL_00d3;
							case 22:
								goto IL_00e4;
							case 23:
								goto IL_00fd;
							case 24:
								goto IL_0109;
							case 25:
								goto IL_0154;
							case 27:
								goto IL_0162;
							case 26:
							case 28:
							case 29:
								goto IL_0174;
							case 30:
								goto IL_019f;
							case 31:
							case 32:
								goto IL_01ba;
							case 33:
								goto IL_01c4;
							case 34:
								goto IL_01dd;
							case 35:
								goto IL_01ef;
							case 36:
								goto IL_0208;
							case 37:
								goto IL_021b;
							case 38:
								goto IL_0234;
							case 39:
								goto IL_0247;
							case 40:
								goto IL_0260;
							case 41:
								goto IL_0273;
							case 42:
								goto IL_028c;
							case 43:
								goto IL_029f;
							case 44:
								goto IL_02b8;
							case 45:
								goto IL_02cb;
							case 46:
								goto IL_02e4;
							case 47:
								goto IL_02f7;
							case 48:
								goto IL_0321;
							case 49:
								goto IL_0334;
							case 50:
								goto IL_034b;
							case 51:
							case 52:
								goto IL_0368;
							case 53:
								goto IL_0381;
							case 54:
								goto IL_0394;
							case 55:
								goto IL_03be;
							case 56:
								goto IL_03d1;
							case 57:
								goto IL_03db;
							case 58:
								goto IL_03f2;
							case 59:
								goto IL_03fd;
							case 60:
								goto IL_0410;
							case 61:
								goto IL_041f;
							case 62:
								goto IL_0434;
							case 63:
								goto IL_0445;
							case 64:
								goto IL_0463;
							case 65:
								goto IL_047b;
							case 66:
								goto IL_0491;
							case 68:
								goto IL_04b4;
							case 69:
								goto IL_04ca;
							case 71:
								goto IL_04ed;
							case 72:
								goto IL_0503;
							case 74:
								goto IL_0526;
							case 75:
								goto IL_053c;
							case 77:
								goto IL_055f;
							case 78:
								goto IL_0575;
							case 80:
								goto IL_0598;
							case 81:
								goto IL_05ae;
							case 83:
								goto IL_05d1;
							case 84:
								goto IL_05e7;
							case 86:
								goto IL_060a;
							case 87:
								goto IL_0620;
							case 88:
								goto IL_062c;
							case 90:
								goto IL_0653;
							case 91:
								goto IL_0669;
							case 93:
								goto IL_068c;
							case 94:
								goto IL_06b0;
							case 96:
								goto IL_06d3;
							case 97:
								goto IL_06e9;
							case 99:
								goto IL_070c;
							case 100:
								goto IL_0722;
							case 102:
								goto IL_0745;
							case 103:
								goto IL_075b;
							case 105:
								goto IL_077e;
							case 106:
								goto IL_0794;
							case 108:
								goto IL_07b7;
							case 109:
								goto IL_07cd;
							case 111:
								goto IL_07f0;
							case 112:
								goto IL_0806;
							case 114:
								goto IL_0829;
							case 115:
								goto IL_083f;
							case 117:
								goto IL_0862;
							case 118:
								goto IL_0878;
							case 120:
								goto IL_08a0;
							case 121:
								goto IL_08b6;
							case 123:
								goto IL_08d9;
							case 124:
								goto IL_08ef;
							case 126:
								goto IL_0912;
							case 127:
								goto IL_0928;
							case 129:
								goto IL_094b;
							case 130:
								goto IL_0964;
							case 132:
								goto IL_098a;
							case 133:
								goto IL_09a3;
							case 135:
								goto IL_09c9;
							case 136:
								goto IL_09e2;
							case 138:
								goto IL_0a08;
							case 139:
								goto IL_0a21;
							case 141:
								goto IL_0a47;
							case 142:
								goto IL_0a60;
							case 144:
								goto IL_0a86;
							case 145:
								goto IL_0a9f;
							case 147:
								goto IL_0ac5;
							case 148:
								goto IL_0ade;
							case 150:
								goto IL_0b04;
							case 151:
								goto IL_0b1d;
							case 153:
								goto IL_0b43;
							case 154:
								goto IL_0b5c;
							case 156:
								goto IL_0b82;
							case 157:
								goto IL_0b9b;
							case 159:
								goto IL_0bc1;
							case 160:
								goto IL_0bda;
							case 162:
								goto IL_0c00;
							case 163:
								goto IL_0c19;
							case 165:
								goto IL_0c3f;
							case 166:
								goto IL_0c58;
							case 168:
								goto IL_0c7e;
							case 169:
								goto IL_0c97;
							case 171:
								goto IL_0cbd;
							case 172:
								goto IL_0cd6;
							case 174:
								goto IL_0cfc;
							case 175:
								goto IL_0d15;
							case 177:
								goto IL_0d3b;
							case 178:
								goto IL_0d54;
							case 179:
								goto IL_0d68;
							case 180:
								goto IL_0d77;
							case 181:
								goto IL_0d8b;
							case 183:
								goto IL_0dac;
							case 184:
								goto IL_0dc5;
							case 186:
								goto IL_0deb;
							case 187:
								goto IL_0e04;
							case 189:
								goto IL_0e2a;
							case 190:
								goto IL_0e43;
							case 192:
								goto IL_0e69;
							case 193:
								goto IL_0e82;
							case 195:
								goto IL_0ea8;
							case 196:
								goto IL_0ec1;
							case 198:
								goto IL_0ee7;
							case 199:
								goto IL_0f00;
							case 201:
								goto IL_0f26;
							case 202:
								goto IL_0f3f;
							case 204:
								goto IL_0f65;
							case 206:
								goto IL_0f84;
							case 207:
								goto IL_0f9d;
							case 209:
								goto IL_0fc3;
							case 210:
								goto IL_0fdc;
							case 212:
								goto IL_1002;
							case 213:
								goto IL_101b;
							case 215:
								goto IL_103e;
							case 216:
								goto IL_1057;
							case 218:
								goto IL_107a;
							case 219:
								goto IL_1093;
							case 67:
							case 70:
							case 73:
							case 76:
							case 79:
							case 82:
							case 85:
							case 89:
							case 92:
							case 95:
							case 98:
							case 101:
							case 104:
							case 107:
							case 110:
							case 113:
							case 116:
							case 119:
							case 122:
							case 125:
							case 128:
							case 131:
							case 134:
							case 137:
							case 140:
							case 143:
							case 146:
							case 149:
							case 152:
							case 155:
							case 158:
							case 161:
							case 164:
							case 167:
							case 170:
							case 173:
							case 176:
							case 182:
							case 185:
							case 188:
							case 191:
							case 194:
							case 197:
							case 200:
							case 203:
							case 205:
							case 208:
							case 211:
							case 214:
							case 217:
							case 220:
							case 221:
							case 222:
								goto IL_10b6;
							case 223:
								goto IL_10cb;
							case 224:
								goto IL_10d4;
							case 226:
								goto IL_10f1;
							case 227:
								goto IL_1123;
							case 230:
								goto IL_1133;
							case 231:
								goto IL_11be;
							case 232:
								goto IL_11da;
							case 233:
								goto IL_11f0;
							case 234:
								goto IL_120c;
							case 235:
								goto IL_1222;
							case 236:
								goto IL_123e;
							case 237:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 225:
							case 228:
							case 229:
							case 238:
							case 239:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_11da:
						num2 = 232;
						sOpt = sOpt + Globals_Renamed.CRLF + d_ROWS;
						goto IL_11f0;
						IL_11f0:
						num2 = 233;
						if (Operators.CompareString(d_EXP, "", TextCompare: false) != 0)
						{
							goto IL_120c;
						}
						goto IL_1222;
						IL_11be:
						num2 = 231;
						if (Operators.CompareString(d_ROWS, "", TextCompare: false) != 0)
						{
							goto IL_11da;
						}
						goto IL_11f0;
						IL_120c:
						num2 = 234;
						sOpt = sOpt + Globals_Renamed.CRLF + d_EXP;
						goto IL_1222;
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
						num7 = 0;
						goto IL_001a;
						IL_001a:
						num2 = 5;
						num8 = 0;
						goto IL_001f;
						IL_001f:
						num2 = 6;
						num9 = 0;
						goto IL_0024;
						IL_0024:
						num2 = 7;
						flag = false;
						goto IL_0029;
						IL_0029:
						num2 = 8;
						@string = "";
						goto IL_0032;
						IL_0032:
						num2 = 9;
						text = "";
						goto IL_003c;
						IL_003c:
						num2 = 10;
						ll_InFile = "";
						goto IL_0046;
						IL_0046:
						num2 = 11;
						text2 = "";
						goto IL_0050;
						IL_0050:
						num2 = 12;
						num10 = 0;
						goto IL_0056;
						IL_0056:
						num2 = 13;
						num11 = 0;
						goto IL_005c;
						IL_005c:
						num2 = 14;
						source = "";
						goto IL_0066;
						IL_0066:
						num2 = 15;
						text3 = "";
						goto IL_0070;
						IL_0070:
						num2 = 16;
						sOpt = "";
						goto IL_007a;
						IL_007a:
						num2 = 17;
						result = 0;
						goto IL_007f;
						IL_007f:
						num2 = 18;
						if (Strings.InStr(Strings.UCase(Strings.Trim(sQuery)), "<OPTIONS>") != 0)
						{
							goto IL_00a5;
						}
						goto IL_1133;
						IL_00a5:
						num2 = 19;
						num7 = Strings.InStr(Strings.UCase(sQuery), "<OPTIONS>");
						goto IL_00bc;
						IL_00bc:
						num2 = 20;
						num6 = Strings.InStr(Strings.UCase(sQuery), "</OPTIONS>");
						goto IL_00d3;
						IL_00d3:
						num2 = 21;
						if (num6 != 0)
						{
							goto IL_00e4;
						}
						goto IL_10f1;
						IL_00e4:
						num2 = 22;
						text = Strings.Mid(sQuery, num7 + 9, num6 - (10 + num7));
						goto IL_00fd;
						IL_00fd:
						num2 = 23;
						@string = Strings.UCase(text);
						goto IL_0109;
						IL_0109:
						num2 = 24;
						if (Strings.Len(sQuery) <= num6 + 10 || Operators.CompareString(Strings.Trim(Strings.Replace(Strings.Mid(sQuery, num6 + 10), "\r\n", "")), "", TextCompare: false) == 0)
						{
							goto IL_0154;
						}
						goto IL_0162;
						IL_1222:
						num2 = 235;
						if (Operators.CompareString(d_LastO, "", TextCompare: false) == 0)
						{
							break;
						}
						goto IL_123e;
						IL_0162:
						num2 = 27;
						sQuery = Strings.Mid(sQuery, num6 + 10);
						goto IL_0174;
						IL_0154:
						num2 = 25;
						sQuery = "";
						goto IL_0174;
						IL_0174:
						num2 = 29;
						if (Strings.InStr(@string, "/OLEDB=MONGO") != 0 && Strings.InStr(@string, "/UN=//") != 0)
						{
							goto IL_019f;
						}
						goto IL_01ba;
						IL_123e:
						num2 = 236;
						sOpt = sOpt + Globals_Renamed.CRLF + d_LastO;
						break;
						IL_019f:
						num2 = 30;
						text = Strings.Replace(text, "/UN=//", "/UN=", 1, -1, CompareMethod.Text);
						goto IL_01ba;
						IL_01ba:
						num2 = 32;
						sOpt = "<OPTIONS>";
						goto IL_01c4;
						IL_01c4:
						num2 = 33;
						if (Strings.InStr(@string, "/NODE=") == 0)
						{
							goto IL_01dd;
						}
						goto IL_01ef;
						IL_01dd:
						num2 = 34;
						sOpt = sOpt + Globals_Renamed.CRLF + d_node;
						goto IL_01ef;
						IL_01ef:
						num2 = 35;
						if (Strings.InStr(@string, "/OLEDB=") == 0)
						{
							goto IL_0208;
						}
						goto IL_021b;
						IL_0208:
						num2 = 36;
						sOpt = sOpt + Globals_Renamed.CRLF + d_oledb;
						goto IL_021b;
						IL_021b:
						num2 = 37;
						if (Strings.InStr(@string, "/ENGINE=") == 0)
						{
							goto IL_0234;
						}
						goto IL_0247;
						IL_0234:
						num2 = 38;
						sOpt = sOpt + Globals_Renamed.CRLF + d_Engine;
						goto IL_0247;
						IL_0247:
						num2 = 39;
						if (Strings.InStr(@string, "/WORKDIR=") == 0)
						{
							goto IL_0260;
						}
						goto IL_0273;
						IL_0260:
						num2 = 40;
						sOpt = sOpt + Globals_Renamed.CRLF + d_WorkDir;
						goto IL_0273;
						IL_0273:
						num2 = 41;
						if (Strings.InStr(@string, "/UN=") == 0)
						{
							goto IL_028c;
						}
						goto IL_029f;
						IL_028c:
						num2 = 42;
						sOpt = sOpt + Globals_Renamed.CRLF + d_UN;
						goto IL_029f;
						IL_029f:
						num2 = 43;
						if (Strings.InStr(@string, "/PW=") == 0)
						{
							goto IL_02b8;
						}
						goto IL_02cb;
						IL_02b8:
						num2 = 44;
						sOpt = sOpt + Globals_Renamed.CRLF + d_PW;
						goto IL_02cb;
						IL_02cb:
						num2 = 45;
						if (Strings.InStr(@string, "/T=") == 0)
						{
							goto IL_02e4;
						}
						goto IL_02f7;
						IL_02e4:
						num2 = 46;
						sOpt = sOpt + Globals_Renamed.CRLF + d_T;
						goto IL_02f7;
						IL_02f7:
						num2 = 47;
						if ((Strings.InStr(@string, "/ROWS=") == 0) & (Operators.CompareString(d_ROWS, "", TextCompare: false) != 0))
						{
							goto IL_0321;
						}
						goto IL_0334;
						IL_0321:
						num2 = 48;
						sOpt = sOpt + Globals_Renamed.CRLF + d_ROWS;
						goto IL_0334;
						IL_0334:
						num2 = 49;
						if ((ToRun == 1) & !Globals_Renamed.g_IsSHGUI)
						{
							goto IL_034b;
						}
						goto IL_0368;
						IL_034b:
						num2 = 50;
						sOpt = sOpt + Globals_Renamed.CRLF + "/TS=" + Globals_Renamed.g_TS;
						goto IL_0368;
						IL_0368:
						num2 = 52;
						if (Strings.InStr(@string, "/CSV=") == 0)
						{
							goto IL_0381;
						}
						goto IL_0394;
						IL_0381:
						num2 = 53;
						sOpt = sOpt + Globals_Renamed.CRLF + d_CSV;
						goto IL_0394;
						IL_0394:
						num2 = 54;
						if ((Strings.InStr(@string, "/EXPLAIN=") == 0) & (Operators.CompareString(d_EXP, "", TextCompare: false) != 0))
						{
							goto IL_03be;
						}
						goto IL_03d1;
						IL_03be:
						num2 = 55;
						sOpt = sOpt + Globals_Renamed.CRLF + d_EXP;
						goto IL_03d1;
						IL_03d1:
						num2 = 56;
						@string = "";
						goto IL_03db;
						IL_03db:
						num2 = 57;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						goto IL_03f2;
						IL_03f2:
						num2 = 58;
						DynArray.Initialize();
						goto IL_03fd;
						IL_03fd:
						num2 = 59;
						num11 = General_Procedures.ParseAndFillArray(text, Globals_Renamed.CRLF, ref DynArray);
						goto IL_0410;
						IL_0410:
						num2 = 60;
						num12 = num11;
						num10 = 1;
						goto IL_10c2;
						IL_10c2:
						if (num10 <= num12)
						{
							goto IL_041f;
						}
						goto IL_10cb;
						IL_10cb:
						num2 = 223;
						DynArray = null;
						goto IL_10d4;
						IL_10d4:
						num2 = 224;
						sOpt = sOpt + Globals_Renamed.CRLF + "</OPTIONS>";
						goto end_IL_0001_3;
						IL_041f:
						num2 = 61;
						num8 = Strings.InStr(DynArray[num10], "=");
						goto IL_0434;
						IL_0434:
						num2 = 62;
						if (num8 != 0)
						{
							goto IL_0445;
						}
						goto IL_10b6;
						IL_0445:
						num2 = 63;
						source = Strings.Trim(Strings.Mid(Strings.UCase(DynArray[num10]), 1, num8 - 1));
						goto IL_0463;
						IL_0463:
						num2 = 64;
						text3 = Strings.Trim(Strings.Mid(DynArray[num10], num8 + 1));
						goto IL_047b;
						IL_047b:
						num2 = 65;
						if (LikeOperator.LikeString(source, "*/*NODE", CompareMethod.Binary))
						{
							goto IL_0491;
						}
						goto IL_04b4;
						IL_0491:
						num2 = 66;
						sOpt = sOpt + Globals_Renamed.CRLF + "/NODE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_04b4:
						num2 = 68;
						if (LikeOperator.LikeString(source, "*/*OLEDB", CompareMethod.Binary))
						{
							goto IL_04ca;
						}
						goto IL_04ed;
						IL_04ca:
						num2 = 69;
						sOpt = sOpt + Globals_Renamed.CRLF + "/OLEDB=" + HG_Strip(text3);
						goto IL_10b6;
						IL_04ed:
						num2 = 71;
						if (LikeOperator.LikeString(source, "*/*ENGINE", CompareMethod.Binary))
						{
							goto IL_0503;
						}
						goto IL_0526;
						IL_0503:
						num2 = 72;
						sOpt = sOpt + Globals_Renamed.CRLF + "/ENGINE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0526:
						num2 = 74;
						if (LikeOperator.LikeString(source, "*/*WORKDIR", CompareMethod.Binary))
						{
							goto IL_053c;
						}
						goto IL_055f;
						IL_053c:
						num2 = 75;
						sOpt = sOpt + Globals_Renamed.CRLF + "/WORKDIR=" + HG_Strip(text3);
						goto IL_10b6;
						IL_055f:
						num2 = 77;
						if (LikeOperator.LikeString(source, "*/*PREPROCESS_CSV", CompareMethod.Binary))
						{
							goto IL_0575;
						}
						goto IL_0598;
						IL_0575:
						num2 = 78;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PREPROCESS_CSV=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0598:
						num2 = 80;
						if (LikeOperator.LikeString(source, "*/*NOHEADERS", CompareMethod.Binary))
						{
							goto IL_05ae;
						}
						goto IL_05d1;
						IL_05ae:
						num2 = 81;
						sOpt = sOpt + Globals_Renamed.CRLF + "/NOHEADERS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_05d1:
						num2 = 83;
						if (LikeOperator.LikeString(source, "*/*QUOTECSV", CompareMethod.Binary))
						{
							goto IL_05e7;
						}
						goto IL_060a;
						IL_05e7:
						num2 = 84;
						sOpt = sOpt + Globals_Renamed.CRLF + "/QUOTECSV=" + HG_Strip(text3);
						goto IL_10b6;
						IL_060a:
						num2 = 86;
						if (LikeOperator.LikeString(source, "*/*CSV", CompareMethod.Binary))
						{
							goto IL_0620;
						}
						goto IL_0653;
						IL_0620:
						num2 = 87;
						ll_InFile = HG_Strip(text3);
						goto IL_062c;
						IL_062c:
						num2 = 88;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CSV=" + Get_CSVTXT_Opts(ll_InFile, ToRun, MyMode);
						goto IL_10b6;
						IL_0653:
						num2 = 90;
						if (LikeOperator.LikeString(source, "*/*STACK", CompareMethod.Binary))
						{
							goto IL_0669;
						}
						goto IL_068c;
						IL_0669:
						num2 = 91;
						sOpt = sOpt + Globals_Renamed.CRLF + "/STACK=" + HG_Strip(text3);
						goto IL_10b6;
						IL_068c:
						num2 = 93;
						if (LikeOperator.LikeString(source, "*/*CTVAL", CompareMethod.Binary) | LikeOperator.LikeString(source, "*/*CTVALUE", CompareMethod.Binary))
						{
							goto IL_06b0;
						}
						goto IL_06d3;
						IL_06b0:
						num2 = 94;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CTVALUE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_06d3:
						num2 = 96;
						if (LikeOperator.LikeString(source, "*/*CTHEADER", CompareMethod.Binary))
						{
							goto IL_06e9;
						}
						goto IL_070c;
						IL_06e9:
						num2 = 97;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CTHEADER=" + HG_Strip(text3);
						goto IL_10b6;
						IL_070c:
						num2 = 99;
						if (LikeOperator.LikeString(source, "*/*CTROW", CompareMethod.Binary))
						{
							goto IL_0722;
						}
						goto IL_0745;
						IL_0722:
						num2 = 100;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CTROW=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0745:
						num2 = 102;
						if (LikeOperator.LikeString(source, "*/*TABLE", CompareMethod.Binary))
						{
							goto IL_075b;
						}
						goto IL_077e;
						IL_075b:
						num2 = 103;
						sOpt = sOpt + Globals_Renamed.CRLF + "/TABLE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_077e:
						num2 = 105;
						if (LikeOperator.LikeString(source, "*/*DELETE", CompareMethod.Binary))
						{
							goto IL_0794;
						}
						goto IL_07b7;
						IL_0794:
						num2 = 106;
						sOpt = sOpt + Globals_Renamed.CRLF + "/DELETE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_07b7:
						num2 = 108;
						if (LikeOperator.LikeString(source, "*/*CTARRAY", CompareMethod.Binary))
						{
							goto IL_07cd;
						}
						goto IL_07f0;
						IL_07cd:
						num2 = 109;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CTARRAY=" + HG_Strip(text3);
						goto IL_10b6;
						IL_07f0:
						num2 = 111;
						if (LikeOperator.LikeString(source, "*/*SORT", CompareMethod.Binary))
						{
							goto IL_0806;
						}
						goto IL_0829;
						IL_0806:
						num2 = 112;
						sOpt = sOpt + Globals_Renamed.CRLF + "/SORT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0829:
						num2 = 114;
						if (LikeOperator.LikeString(source, "*/*SHOWSQL", CompareMethod.Binary))
						{
							goto IL_083f;
						}
						goto IL_0862;
						IL_083f:
						num2 = 115;
						sOpt = sOpt + Globals_Renamed.CRLF + "/SHOWSQL=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0862:
						num2 = 117;
						if (LikeOperator.LikeString(source, "*/*PARALLEL", CompareMethod.Binary))
						{
							goto IL_0878;
						}
						goto IL_08a0;
						IL_0878:
						num2 = 118;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PARALLEL=" + Strings.Trim(HG_Strip(text3));
						goto IL_10b6;
						IL_08a0:
						num2 = 120;
						if (LikeOperator.LikeString(source, "*/*SQLITE_DT", CompareMethod.Binary))
						{
							goto IL_08b6;
						}
						goto IL_08d9;
						IL_08b6:
						num2 = 121;
						sOpt = sOpt + Globals_Renamed.CRLF + "/SQLITE_DT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_08d9:
						num2 = 123;
						if (LikeOperator.LikeString(source, "*/*QUOTECSV", CompareMethod.Binary))
						{
							goto IL_08ef;
						}
						goto IL_0912;
						IL_08ef:
						num2 = 124;
						sOpt = sOpt + Globals_Renamed.CRLF + "/QUOTECSV=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0912:
						num2 = 126;
						if (LikeOperator.LikeString(source, "*/*EMPTY_TO_NULL", CompareMethod.Binary))
						{
							goto IL_0928;
						}
						goto IL_094b;
						IL_0928:
						num2 = 127;
						sOpt = sOpt + Globals_Renamed.CRLF + "/EMPTY_TO_NULL=" + HG_Strip(text3);
						goto IL_10b6;
						IL_094b:
						num2 = 129;
						if (LikeOperator.LikeString(source, "*/*REPLACEHDRS", CompareMethod.Binary))
						{
							goto IL_0964;
						}
						goto IL_098a;
						IL_0964:
						num2 = 130;
						sOpt = sOpt + Globals_Renamed.CRLF + "/REPLACEHDRS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_098a:
						num2 = 132;
						if (LikeOperator.LikeString(source, "*/*PROMPT-TEXT", CompareMethod.Binary))
						{
							goto IL_09a3;
						}
						goto IL_09c9;
						IL_09a3:
						num2 = 133;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PROMPT-TEXT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_09c9:
						num2 = 135;
						if (LikeOperator.LikeString(source, "*/*RECORD", CompareMethod.Binary))
						{
							goto IL_09e2;
						}
						goto IL_0a08;
						IL_09e2:
						num2 = 136;
						sOpt = sOpt + Globals_Renamed.CRLF + "/RECORD=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0a08:
						num2 = 138;
						if (LikeOperator.LikeString(source, "*/*PIVOT-MODE", CompareMethod.Binary))
						{
							goto IL_0a21;
						}
						goto IL_0a47;
						IL_0a21:
						num2 = 139;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PIVOT-MODE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0a47:
						num2 = 141;
						if (LikeOperator.LikeString(source, "*/*PIVOTDOT", CompareMethod.Binary))
						{
							goto IL_0a60;
						}
						goto IL_0a86;
						IL_0a60:
						num2 = 142;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PIVOTDOT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0a86:
						num2 = 144;
						if (LikeOperator.LikeString(source, "*/*SQLITE_EXE", CompareMethod.Binary))
						{
							goto IL_0a9f;
						}
						goto IL_0ac5;
						IL_0a9f:
						num2 = 145;
						sOpt = sOpt + Globals_Renamed.CRLF + "/SQLITE_EXE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0ac5:
						num2 = 147;
						if (LikeOperator.LikeString(source, "*/*USE_LEGACY_PIVOT_HEADERS", CompareMethod.Binary))
						{
							goto IL_0ade;
						}
						goto IL_0b04;
						IL_0ade:
						num2 = 148;
						sOpt = sOpt + Globals_Renamed.CRLF + "/USE_LEGACY_PIVOT_HEADERS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0b04:
						num2 = 150;
						if (LikeOperator.LikeString(source, "*/*HEADERS", CompareMethod.Binary))
						{
							goto IL_0b1d;
						}
						goto IL_0b43;
						IL_0b1d:
						num2 = 151;
						sOpt = sOpt + Globals_Renamed.CRLF + "/HEADERS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0b43:
						num2 = 153;
						if (LikeOperator.LikeString(source, "*/*HADOOP_SERVER_DEFAULT", CompareMethod.Binary))
						{
							goto IL_0b5c;
						}
						goto IL_0b82;
						IL_0b5c:
						num2 = 154;
						sOpt = sOpt + Globals_Renamed.CRLF + "/HADOOP_SERVER_DEFAULT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0b82:
						num2 = 156;
						if (LikeOperator.LikeString(source, "*/*APP_SERVER_DEFAULT", CompareMethod.Binary))
						{
							goto IL_0b9b;
						}
						goto IL_0bc1;
						IL_0b9b:
						num2 = 157;
						sOpt = sOpt + Globals_Renamed.CRLF + "/APP_SERVER_DEFAULT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0bc1:
						num2 = 159;
						if (LikeOperator.LikeString(source, "*/*LOBDATA", CompareMethod.Binary))
						{
							goto IL_0bda;
						}
						goto IL_0c00;
						IL_0bda:
						num2 = 160;
						sOpt = sOpt + Globals_Renamed.CRLF + "/LOBDATA=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0c00:
						num2 = 162;
						if (LikeOperator.LikeString(source, "*/*APPENDMODE", CompareMethod.Binary))
						{
							goto IL_0c19;
						}
						goto IL_0c3f;
						IL_0c19:
						num2 = 163;
						sOpt = sOpt + Globals_Renamed.CRLF + "/APPENDMODE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0c3f:
						num2 = 165;
						if (LikeOperator.LikeString(source, "*/*CONNECTRETRY", CompareMethod.Binary))
						{
							goto IL_0c58;
						}
						goto IL_0c7e;
						IL_0c58:
						num2 = 166;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CONNECTRETRY=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0c7e:
						num2 = 168;
						if (LikeOperator.LikeString(source, "*/*RESET", CompareMethod.Binary))
						{
							goto IL_0c97;
						}
						goto IL_0cbd;
						IL_0c97:
						num2 = 169;
						sOpt = sOpt + Globals_Renamed.CRLF + "/RESET=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0cbd:
						num2 = 171;
						if (LikeOperator.LikeString(source, "*/*HEADERS_UNIQUE", CompareMethod.Binary))
						{
							goto IL_0cd6;
						}
						goto IL_0cfc;
						IL_0cd6:
						num2 = 172;
						sOpt = sOpt + Globals_Renamed.CRLF + "/HEADERS_UNIQUE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0cfc:
						num2 = 174;
						if (LikeOperator.LikeString(source, "*/*INSTANCE", CompareMethod.Binary))
						{
							goto IL_0d15;
						}
						goto IL_0d3b;
						IL_0d15:
						num2 = 175;
						sOpt = sOpt + Globals_Renamed.CRLF + "/INSTANCE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0d3b:
						num2 = 177;
						if (LikeOperator.LikeString(source, "*/*UTILITIES", CompareMethod.Binary))
						{
							goto IL_0d54;
						}
						goto IL_0dac;
						IL_0d54:
						num2 = 178;
						text2 = General_Procedures.Quote_CRLF_Replace("EQ", text3);
						goto IL_0d68;
						IL_0d68:
						num2 = 179;
						text2 = HG_Strip(text2);
						goto IL_0d77;
						IL_0d77:
						num2 = 180;
						text2 = General_Procedures.Quote_CRLF_Replace("DQ", text2);
						goto IL_0d8b;
						IL_0d8b:
						num2 = 181;
						sOpt = sOpt + Globals_Renamed.CRLF + "/UTILITIES=" + text2;
						goto IL_10b6;
						IL_0dac:
						num2 = 183;
						if (LikeOperator.LikeString(source, "*/*EXPLAIN", CompareMethod.Binary))
						{
							goto IL_0dc5;
						}
						goto IL_0deb;
						IL_0dc5:
						num2 = 184;
						sOpt = sOpt + Globals_Renamed.CRLF + "/EXPLAIN=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0deb:
						num2 = 186;
						if (LikeOperator.LikeString(source, "*/*JSL", CompareMethod.Binary))
						{
							goto IL_0e04;
						}
						goto IL_0e2a;
						IL_0e04:
						num2 = 187;
						sOpt = sOpt + Globals_Renamed.CRLF + "/JSL=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0e2a:
						num2 = 189;
						if (LikeOperator.LikeString(source, "*/*RSCRIPT", CompareMethod.Binary))
						{
							goto IL_0e43;
						}
						goto IL_0e69;
						IL_0e43:
						num2 = 190;
						sOpt = sOpt + Globals_Renamed.CRLF + "/RSCRIPT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0e69:
						num2 = 192;
						if (LikeOperator.LikeString(source, "*/*CB_ACS", CompareMethod.Binary))
						{
							goto IL_0e82;
						}
						goto IL_0ea8;
						IL_0e82:
						num2 = 193;
						sOpt = sOpt + Globals_Renamed.CRLF + "/CB_ACS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0ea8:
						num2 = 195;
						if (LikeOperator.LikeString(source, "*/*WRITE-FILE", CompareMethod.Binary))
						{
							goto IL_0ec1;
						}
						goto IL_0ee7;
						IL_0ec1:
						num2 = 196;
						sOpt = sOpt + Globals_Renamed.CRLF + "/WRITE-FILE=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0ee7:
						num2 = 198;
						if (LikeOperator.LikeString(source, "*/*REPORT", CompareMethod.Binary))
						{
							goto IL_0f00;
						}
						goto IL_0f26;
						IL_0f00:
						num2 = 199;
						sOpt = sOpt + Globals_Renamed.CRLF + "/REPORT=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0f26:
						num2 = 201;
						if (LikeOperator.LikeString(source, "*/*OUTLOOK", CompareMethod.Binary))
						{
							goto IL_0f3f;
						}
						goto IL_0f65;
						IL_0f3f:
						num2 = 202;
						sOpt = sOpt + Globals_Renamed.CRLF + "/OUTLOOK=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0f65:
						num2 = 204;
						if (!LikeOperator.LikeString(source, "*/*TXT", CompareMethod.Binary))
						{
							goto IL_0f84;
						}
						goto IL_10b6;
						IL_0f84:
						num2 = 206;
						if (LikeOperator.LikeString(source, "*/*ID", CompareMethod.Binary))
						{
							goto IL_0f9d;
						}
						goto IL_0fc3;
						IL_0f9d:
						num2 = 207;
						sOpt = sOpt + Globals_Renamed.CRLF + "/ID=" + HG_Strip(text3);
						goto IL_10b6;
						IL_0fc3:
						num2 = 209;
						if (LikeOperator.LikeString(source, "*/*UN", CompareMethod.Binary))
						{
							goto IL_0fdc;
						}
						goto IL_1002;
						IL_0fdc:
						num2 = 210;
						sOpt = sOpt + Globals_Renamed.CRLF + "/UN=" + HG_Strip(text3);
						goto IL_10b6;
						IL_1002:
						num2 = 212;
						if (LikeOperator.LikeString(source, "*/*PW", CompareMethod.Binary))
						{
							goto IL_101b;
						}
						goto IL_103e;
						IL_101b:
						num2 = 213;
						sOpt = sOpt + Globals_Renamed.CRLF + "/PW=" + HG_Strip(text3);
						goto IL_10b6;
						IL_103e:
						num2 = 215;
						if (LikeOperator.LikeString(source, "*/T", CompareMethod.Binary))
						{
							goto IL_1057;
						}
						goto IL_107a;
						IL_1057:
						num2 = 216;
						sOpt = sOpt + Globals_Renamed.CRLF + "/T=" + HG_Strip(text3);
						goto IL_10b6;
						IL_107a:
						num2 = 218;
						if (LikeOperator.LikeString(source, "*/*ROWS", CompareMethod.Binary))
						{
							goto IL_1093;
						}
						goto IL_10b6;
						IL_1093:
						num2 = 219;
						sOpt = sOpt + Globals_Renamed.CRLF + "/ROWS=" + HG_Strip(text3);
						goto IL_10b6;
						IL_10b6:
						num2 = 222;
						num10++;
						goto IL_10c2;
						IL_10f1:
						num2 = 226;
						Interaction.MsgBox("Query error in segment " + Conversions.ToString(unchecked((int)SegNo)) + ". The <OPTIONS> token was present, but no </OPTIONS> end token was found. Correct before saving." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Invalid Query");
						goto IL_1123;
						IL_1123:
						num2 = 227;
						result = 1;
						goto end_IL_0001_3;
						IL_1133:
						num2 = 230;
						sOpt = "<OPTIONS>" + Globals_Renamed.CRLF + d_node + Globals_Renamed.CRLF + d_oledb + Globals_Renamed.CRLF + d_Engine + Globals_Renamed.CRLF + d_WorkDir + Globals_Renamed.CRLF + d_UN + Globals_Renamed.CRLF + d_PW + Globals_Renamed.CRLF + d_T + Globals_Renamed.CRLF + d_CSV;
						goto IL_11be;
						end_IL_0001_2:
						break;
					}
					num2 = 237;
					sOpt = sOpt + Globals_Renamed.CRLF + "</OPTIONS>";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 5695;
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

	public static string HG_Strip(string MyStr)
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
							goto IL_0021;
						case 4:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000b:
					num2 = 2;
					text = Strings.Replace(MyStr, "\n", "");
					goto IL_0021;
					IL_0021:
					num2 = 3;
					text = Strings.Replace(text, "\r", "");
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				text = Strings.Replace(text, "\"", "");
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

	public static void HG_Editor(ref string MyData, string MySVA, string MyQ)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		FrmDTREdit frmDTREdit = default(FrmDTREdit);
		string text = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				short num5;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 332:
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
							goto IL_0038;
						case 8:
							goto IL_0045;
						case 7:
						case 9:
						case 10:
							goto IL_004b;
						case 11:
							goto IL_005d;
						case 12:
							goto IL_006e;
						case 13:
							goto IL_007f;
						case 14:
							goto IL_0091;
						case 15:
							goto IL_00a1;
						case 16:
							goto IL_00b9;
						case 17:
						case 18:
							goto IL_00c7;
						case 19:
							goto IL_00d2;
						case 20:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 21:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00b9:
					num2 = 16;
					num5 = frmDTREdit.OpenSQLFile(MyQ);
					goto IL_00c7;
					IL_00c7:
					num2 = 18;
					frmDTREdit.fSaveSQL = false;
					goto IL_00d2;
					IL_00a1:
					num2 = 15;
					if (Operators.CompareString(MyQ, "", TextCompare: false) != 0)
					{
						goto IL_00b9;
					}
					goto IL_00c7;
					IL_00d2:
					num2 = 19;
					frmDTREdit.ShowDialog();
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					frmDTREdit = new FrmDTREdit();
					goto IL_0018;
					IL_0018:
					num2 = 4;
					text = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					if (Operators.CompareString(MySVA, "SQL", TextCompare: false) == 0)
					{
						goto IL_0038;
					}
					goto IL_0045;
					IL_0038:
					num2 = 6;
					text = "SQL->VA";
					goto IL_004b;
					IL_0045:
					num2 = 8;
					text = MySVA;
					goto IL_004b;
					IL_004b:
					num2 = 10;
					frmDTREdit.Text1.Text = MyData;
					goto IL_005d;
					IL_005d:
					num2 = 11;
					frmDTREdit.Text1.SelectionLength = 0;
					goto IL_006e;
					IL_006e:
					num2 = 12;
					frmDTREdit.Text1.SelectionStart = 0;
					goto IL_007f;
					IL_007f:
					num2 = 13;
					frmDTREdit.cmbSQLVA.Text = text;
					goto IL_0091;
					IL_0091:
					num2 = 14;
					frmDTREdit.Tag = "";
					goto IL_00a1;
					end_IL_0001_2:
					break;
				}
				num2 = 20;
				frmDTREdit = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 332;
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

	public static void HG_cmbcolIG_DD(ref ComboBox cmbColIG, ref ComboBox TxtBox)
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
				case 340:
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
						case 4:
							goto IL_0025;
						case 5:
							goto IL_0034;
						case 6:
							goto IL_003e;
						case 7:
							goto IL_0055;
						case 9:
							goto IL_006f;
						case 10:
							goto IL_008b;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0055:
					num2 = 7;
					Interaction.MsgBox("Cannot get column headers. No file specified.", MsgBoxStyle.Exclamation, "Missing CSV");
					goto end_IL_0001_3;
					IL_006f:
					num2 = 9;
					if (MyProject.Computer.FileSystem.FileExists(text))
					{
						break;
					}
					goto IL_008b;
					IL_003e:
					num2 = 6;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_0055;
					}
					goto IL_006f;
					IL_008b:
					num2 = 10;
					Interaction.MsgBox("Cannot get column headers for: " + text + ". (" + Conversions.ToString(Information.Err().Number) + ":" + Information.Err().Description + ")." + General_Procedures.Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Header Error");
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					Microsoft.VisualBasic.FileSystem.ChDrive(Globals_Renamed.MyPCDir);
					goto IL_0018;
					IL_0018:
					num2 = 3;
					Microsoft.VisualBasic.FileSystem.ChDir(Globals_Renamed.MyPCDir);
					goto IL_0025;
					IL_0025:
					num2 = 4;
					text = Strings.Trim(TxtBox.Text);
					goto IL_0034;
					IL_0034:
					num2 = 5;
					text = Replace_Globals(text, 1);
					goto IL_003e;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				General_Procedures.LoadCSVHeaders(text, ref cmbColIG, 0);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 340;
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

	public static string gLoad_MQ(ref DataGridView GridMQ, string FileName, ref string PackedExe)
	{
		int num = 0;
		short num2 = 0;
		string text = "";
		string text2 = "";
		string result = "";
		try
		{
			if (File.Exists(FileName))
			{
				StreamReader streamReader = new StreamReader(FileName);
				try
				{
					num = 0;
					while (streamReader.Peek() != -1 && num < 600)
					{
						text = streamReader.ReadLine();
						if (Operators.CompareString(Strings.Mid(Strings.UCase(text), 1, Strings.Len("!SPF-BATCH-ATTRIBUTES->")), Strings.UCase("!SPF-BATCH-ATTRIBUTES->"), TextCompare: false) == 0)
						{
							text2 = MyProject.Forms.FrmMultiQuery.Set_Batch_Attr("S", text, ref PackedExe);
							continue;
						}
						checked
						{
							num2 = (short)Strings.InStrRev(text, "<<");
							if (num2 != 0)
							{
								text = Strings.Trim(text);
								GridMQ.Rows[num].Cells[0].Value = Strings.Mid(text, 1, num2 - 1);
								GridMQ.Rows[num].Cells[1].Value = Strings.Mid(text, num2 + 2);
							}
							else
							{
								GridMQ.Rows[num].Cells[0].Value = Strings.Trim(text);
							}
							SetMQColor(ref GridMQ, num);
							num++;
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					result = ex2.Message;
					ProjectData.ClearProjectError();
				}
				finally
				{
					streamReader.Close();
					streamReader.Dispose();
					streamReader = null;
					GridMQ.FirstDisplayedScrollingRowIndex = 0;
				}
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			result = ex4.Message;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void SetMQColor(ref DataGridView MyGrid, int MyRow)
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
							goto IL_0057;
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
					if (Operators.CompareString(Strings.Mid(Strings.Trim(Conversions.ToString(MyGrid.Rows[MyRow].Cells[0].Value)) + " ", 1, 1), "!", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0057;
					IL_0057:
					num2 = 3;
					MyGrid.Rows[MyRow].DefaultCellStyle.ForeColor = Color.Maroon;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				MyGrid.Rows[MyRow].DefaultCellStyle.ForeColor = Color.Black;
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
	}

	public static string Get_Custom_Schema_Path(string MyTable, string FileToOpen2)
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
				case 347:
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
							goto IL_0024;
						case 4:
							goto IL_003d;
						case 5:
							goto IL_0054;
						case 7:
							goto IL_005e;
						case 8:
							goto IL_0077;
						case 10:
							goto IL_007e;
						case 11:
							goto IL_0098;
						case 12:
							goto IL_00b0;
						case 14:
							goto IL_00b8;
						case 15:
							goto IL_00d2;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 9:
						case 13:
						case 16:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00b0:
					num2 = 12;
					result = FileToOpen2;
					goto end_IL_0001_3;
					IL_00b8:
					num2 = 14;
					if (!LikeOperator.LikeString(Strings.UCase(FileToOpen2), "\\\\AZSACTAPP22.INTEL.COM\\*", CompareMethod.Binary))
					{
						break;
					}
					goto IL_00d2;
					IL_0098:
					num2 = 11;
					FileToOpen2 = Strings.Replace(FileToOpen2, "\\\\AZSTMGTSPATH01.ch.intel.com\\sqlpathfinder$", "\\\\ATDFILE3.CH.INTEL.COM\\ATD-WEB\\PATHFINDING\\SQLPATHFINDER", 1, -1, CompareMethod.Text);
					goto IL_00b0;
					IL_00d2:
					num2 = 15;
					result = FileToOpen2;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					if (LikeOperator.LikeString(Strings.UCase(MyTable), "*SITE SPECIFIC]*", CompareMethod.Binary))
					{
						goto IL_0024;
					}
					goto IL_005e;
					IL_0024:
					num2 = 3;
					if (LikeOperator.LikeString(Strings.UCase(FileToOpen2), "\\\\AZSTMGTSPATH01*", CompareMethod.Binary))
					{
						goto IL_003d;
					}
					goto IL_0054;
					IL_003d:
					num2 = 4;
					FileToOpen2 = Strings.Replace(FileToOpen2, "\\\\AZSTMGTSPATH01.ch.intel.com\\sqlpathfinder$", "\\\\ATDFILE3.CH.INTEL.COM\\ATD-WEB\\PATHFINDING\\SQLPATHFINDER", 1, -1, CompareMethod.Text);
					goto IL_0054;
					IL_0054:
					num2 = 5;
					result = FileToOpen2;
					goto end_IL_0001_3;
					IL_005e:
					num2 = 7;
					if (LikeOperator.LikeString(Strings.UCase(FileToOpen2), "\\\\ATDFILE3.CH.INTEL.COM\\ATD-WEB\\PATHFINDING\\SQLPATHFINDER*", CompareMethod.Binary))
					{
						goto IL_0077;
					}
					goto IL_007e;
					IL_0077:
					num2 = 8;
					result = FileToOpen2;
					goto end_IL_0001_3;
					IL_007e:
					num2 = 10;
					if (LikeOperator.LikeString(Strings.UCase(FileToOpen2), "\\\\AZSTMGTSPATH01*", CompareMethod.Binary))
					{
						goto IL_0098;
					}
					goto IL_00b8;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				result = MyProject.Application.Info.DirectoryPath + "\\" + FileToOpen2;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 347;
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

	public static void GetSiteSpecific(string MyDirectory, ref string MyFile)
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
				case 358:
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
							goto IL_002c;
						case 5:
							goto IL_0048;
						case 6:
							goto IL_006a;
						case 7:
							goto IL_0085;
						case 8:
						case 9:
							goto IL_0090;
						case 10:
							goto IL_00ae;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0085:
					num2 = 7;
					MyDirectory = "";
					goto IL_0090;
					IL_0090:
					num2 = 9;
					FileOpenSave("O", "", "dat", "", MyDirectory);
					goto IL_00ae;
					IL_006a:
					num2 = 6;
					if (!MyProject.Computer.FileSystem.DirectoryExists(MyDirectory))
					{
						goto IL_0085;
					}
					goto IL_0090;
					IL_00ae:
					num2 = 10;
					if (!((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) != 0)))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					MyFile = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					if (Operators.CompareString(MyDirectory, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_002c;
					IL_002c:
					num2 = 4;
					if (Operators.CompareString(Strings.UCase(MyDirectory), "LOCAL\\VIEWS\\", TextCompare: false) == 0)
					{
						goto IL_0048;
					}
					goto IL_0090;
					IL_0048:
					num2 = 5;
					MyDirectory = Strings.Trim(MyProject.Application.Info.DirectoryPath) + "\\Local\\Views";
					goto IL_006a;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				MyFile = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 358;
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

	public static string Locate_Util_Item(string MyUtility, int MyStart)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string[] array = default(string[]);
		string result = default(string);
		string text = default(string);
		int num5 = default(int);
		string left = default(string);
		int maxTables = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num6;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 729:
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
							goto IL_001d;
						case 5:
							goto IL_0022;
						case 6:
							goto IL_0029;
						case 7:
							goto IL_0032;
						case 8:
							goto IL_003b;
						case 9:
							goto IL_004c;
						case 10:
							goto IL_0077;
						case 11:
							goto IL_0085;
						case 13:
							goto IL_00a4;
						case 12:
						case 14:
						case 15:
							goto IL_00c0;
						case 16:
							goto IL_00d8;
						case 17:
							goto IL_00e5;
						case 19:
							goto IL_0101;
						case 20:
							goto IL_010f;
						case 22:
							goto IL_0128;
						case 23:
							goto IL_0136;
						case 25:
							goto IL_014f;
						case 26:
							goto IL_015d;
						case 28:
							goto IL_0177;
						case 31:
						case 32:
						case 33:
							goto IL_0194;
						case 18:
						case 21:
						case 24:
						case 27:
						case 29:
						case 30:
						case 34:
							goto IL_01a6;
						case 35:
							goto IL_01bf;
						case 36:
							goto IL_01cf;
						case 38:
							goto IL_01d9;
						case 39:
							goto IL_01ec;
						case 40:
							goto IL_0203;
						case 41:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 37:
						case 42:
						case 43:
						case 44:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ec:
					num2 = 39;
					if (Information.UBound(array) < MyStart)
					{
						break;
					}
					goto IL_0203;
					IL_0203:
					num2 = 40;
					result = array[MyStart];
					break;
					IL_01d9:
					num2 = 38;
					array = Strings.Split(text, "|");
					goto IL_01ec;
					IL_0194:
					num2 = 33;
					num5 = checked(num5 + 1);
					goto IL_019d;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					MyUtility = Strings.UCase(MyUtility);
					goto IL_001d;
					IL_001d:
					num2 = 4;
					num5 = 0;
					goto IL_0022;
					IL_0022:
					num2 = 5;
					num6 = checked(MyStart + 1);
					goto IL_0029;
					IL_0029:
					num2 = 6;
					text = "";
					goto IL_0032;
					IL_0032:
					num2 = 7;
					left = "";
					goto IL_003b;
					IL_003b:
					num2 = 8;
					maxTables = Globals_Renamed.MaxTables;
					num5 = 0;
					goto IL_019d;
					IL_019d:
					if (num5 <= maxTables)
					{
						goto IL_004c;
					}
					goto IL_01a6;
					IL_004c:
					num2 = 9;
					if (Operators.CompareString(Globals_Renamed.Tables[num5].ObjectType, "Utilities", TextCompare: false) == 0)
					{
						goto IL_0077;
					}
					goto IL_0194;
					IL_0077:
					num2 = 10;
					if (MyStart == -4)
					{
						goto IL_0085;
					}
					goto IL_00a4;
					IL_0085:
					num2 = 11;
					left = Strings.UCase(Globals_Renamed.Tables[num5].Display);
					goto IL_00c0;
					IL_00a4:
					num2 = 13;
					left = Strings.UCase(Globals_Renamed.Tables[num5].Name);
					goto IL_00c0;
					IL_00c0:
					num2 = 15;
					if (Operators.CompareString(left, MyUtility, TextCompare: false) == 0)
					{
						goto IL_00d8;
					}
					goto IL_0194;
					IL_00d8:
					num2 = 16;
					if (MyStart == -1)
					{
						goto IL_00e5;
					}
					goto IL_0101;
					IL_00e5:
					num2 = 17;
					text = Globals_Renamed.Tables[num5].Tag;
					goto IL_01a6;
					IL_0101:
					num2 = 19;
					if (MyStart == -2)
					{
						goto IL_010f;
					}
					goto IL_0128;
					IL_010f:
					num2 = 20;
					text = Globals_Renamed.Tables[num5].Description;
					goto IL_01a6;
					IL_0128:
					num2 = 22;
					if (MyStart == -3)
					{
						goto IL_0136;
					}
					goto IL_014f;
					IL_0136:
					num2 = 23;
					text = Globals_Renamed.Tables[num5].Display;
					goto IL_01a6;
					IL_014f:
					num2 = 25;
					if (MyStart == -4)
					{
						goto IL_015d;
					}
					goto IL_0177;
					IL_015d:
					num2 = 26;
					text = Globals_Renamed.Tables[num5].Name;
					goto IL_01a6;
					IL_0177:
					num2 = 28;
					text = Globals_Renamed.Tables[num5].fileschema;
					goto IL_01a6;
					IL_01a6:
					num2 = 34;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_01bf;
					IL_01bf:
					num2 = 35;
					if (MyStart <= 0)
					{
						goto IL_01cf;
					}
					goto IL_01d9;
					IL_01cf:
					num2 = 36;
					result = text;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 41;
				array = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 729;
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

	public static string ParseUtilArgs(string lMyRow, string MyExt, string MyQueryToProcess, string MyFType = "P")
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
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
					{
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "BuildForm - ParseUtilArgs";
						string text = "";
						string text2 = "";
						string text3 = "";
						short num3 = 0;
						string text4 = "";
						short num4 = 0;
						short num5 = 0;
						int num6 = 0;
						string left = "L";
						string fUtilPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\";
						int num7 = 0;
						int num8 = 0;
						string text5 = "";
						lMyRow = Strings.Trim(lMyRow);
						Globals_Renamed.currhelptmp = "";
						Globals_Renamed.currvaluetmp = "";
						result = "";
						if (Strings.InStr(Strings.UCase(lMyRow), "@EXEDIR@") != 0)
						{
							fUtilPath = "@EXEDIR@\\";
						}
						if (LikeOperator.LikeString(Strings.UCase(lMyRow), "{*}*", CompareMethod.Binary))
						{
							num7 = Strings.InStr(lMyRow, "}");
							if (num7 != 0)
							{
								text2 = Strings.Mid(lMyRow, 2, num7 - 2);
								if (LikeOperator.LikeString(Strings.UCase(text2), "PYSCRIPT:*", CompareMethod.Binary))
								{
									text2 = Strings.Trim(Strings.Mid(text2 + " ", 10));
									text = Strings.Trim(Strings.Mid(lMyRow, num7 + 2));
									num8 = Strings.InStr(text, "\"");
									text = Strings.Mid(text, num8 + 1);
									num8 = Strings.InStr(text, "\"");
									text = Strings.Trim(Strings.Mid(text + " ", num8 + 1));
									text5 = Locate_Util_Item(text2, -1);
								}
								else
								{
									text = Strings.Trim(Strings.Mid(lMyRow, num7 + 2));
								}
								Globals_Renamed.currhelptmp = Locate_Util_Item(text2, 5);
								fUtilPath = "";
							}
						}
						else if (Strings.InStr(Strings.UCase(lMyRow), Strings.UCase("ImportExcel.va")) != 0)
						{
							num7 = Strings.InStr(lMyRow, " \"");
							if (num7 != 0)
							{
								text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
							}
							Globals_Renamed.currhelptmp = "Excel";
							left = "I";
						}
						else if (Strings.InStr(Strings.UCase(lMyRow), Strings.UCase("StackResults.va")) != 0)
						{
							num7 = Strings.InStr(lMyRow, " \"");
							if (num7 != 0)
							{
								text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
							}
							Globals_Renamed.currhelptmp = "STACK";
						}
						else if (Strings.InStr(Strings.UCase(lMyRow), "SPFCOPY.VA") != 0)
						{
							num7 = Strings.InStr(lMyRow, " \"");
							if (num7 != 0)
							{
								text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
							}
							Globals_Renamed.currhelptmp = "COPY";
						}
						else if (Strings.InStr(Strings.UCase(lMyRow), Strings.UCase("STACKJMP.VA")) != 0)
						{
							num7 = Strings.InStr(lMyRow, " \"");
							if (num7 != 0)
							{
								text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
							}
							Globals_Renamed.currhelptmp = "STACK";
						}
						else if (Strings.InStr(Strings.UCase(lMyRow), "VA_JOIN.VA") != 0)
						{
							num7 = Strings.InStr(lMyRow, " \"");
							if (num7 != 0)
							{
								text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
							}
							Globals_Renamed.currhelptmp = "JOIN";
						}
						else
						{
							text = "";
							Globals_Renamed.currhelptmp = "";
							short num9 = (short)Globals_Renamed.MaxTables;
							for (num5 = 0; num5 <= num9; num5 = (short)unchecked(num5 + 1))
							{
								if (Operators.CompareString(Globals_Renamed.Tables[num5].ObjectType, "Utilities", TextCompare: false) == 0 && Operators.CompareString(Strings.Trim(Globals_Renamed.Tables[num5].Tag), "", TextCompare: false) != 0)
								{
									text5 = Locate_Util_Item(Globals_Renamed.Tables[num5].Name, -1);
									if (Strings.InStr(Strings.UCase(lMyRow), Strings.UCase(text5)) != 0)
									{
										num7 = Strings.InStr(lMyRow, " \"");
										if (num7 != 0)
										{
											text = Strings.Trim(Strings.Mid(lMyRow, num7 + 1));
										}
										Globals_Renamed.currhelptmp = Locate_Util_Item(Globals_Renamed.Tables[num5].Name, 5);
										break;
									}
								}
							}
						}
						if (Operators.CompareString(Globals_Renamed.currhelptmp, "", TextCompare: false) == 0)
						{
							if ((Operators.CompareString(MyExt, ".VA", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(MyExt), "VA", TextCompare: false) == 0) | (Operators.CompareString(MyExt, "JSL", TextCompare: false) == 0) | (Operators.CompareString(MyExt, "BAT", TextCompare: false) == 0) | (Operators.CompareString(MyExt, "CMD", TextCompare: false) == 0))
							{
								num6 = Interaction.Shell("Notepad.exe " + MyQueryToProcess, AppWinStyle.NormalFocus);
							}
							goto end_IL_0001;
						}
						if (IsUtil("SQLITE-DELETE"))
						{
							Globals_Renamed.currhelptmp = "DELETE";
							text5 = "SPFDelete.bat";
							MyFType = "M";
						}
						FrmUtil frmUtil = new FrmUtil();
						frmUtil.OptXL_Load.Checked = true;
						frmUtil.fVaFileName = text5;
						frmUtil.fHelpType = MyFType;
						if (IsUtil("Excel") & (Operators.CompareString(left, "I", TextCompare: false) == 0))
						{
							frmUtil.OptXL_Import.Checked = true;
						}
						frmUtil.fUtilPath = fUtilPath;
						if (IsUtil("SQLITE-LOAD") || IsUtil("ROWS-IN-FILE") || IsUtil("R") || IsUtil("TDX-TO-CSV") || IsUtil("GET-TCA") || IsUtil("PYTHON") || IsUtil("COPY") || IsUtil("MONGODB-IMPORT") || IsUtil("BEGIN-HPC"))
						{
							frmUtil.chkOption.Checked = false;
						}
						num4 = 0;
						num3 = 0;
						text3 = "";
						short num10 = (short)Strings.Len(text);
						for (num5 = 1; num5 <= num10; num5 = (short)unchecked(num5 + 1))
						{
							text4 = Strings.Mid(text, num5, 1);
							if (Operators.CompareString(text4, "\"", TextCompare: false) == 0)
							{
								if (num4 == 0)
								{
									num4 = 1;
								}
								else
								{
									num4 = 0;
									num3++;
									if ((Operators.CompareString(Globals_Renamed.currhelptmp, "Excel", TextCompare: false) == 0) & (Operators.CompareString(left, "I", TextCompare: false) == 0))
									{
										switch (num3)
										{
										case 1:
											frmUtil.TxtInXL.Text = Strings.Trim(text3);
											break;
										case 2:
											frmUtil.TxtOutXL.Text = Strings.Trim(text3);
											break;
										case 3:
											frmUtil.TxtCSV.Text = text3;
											break;
										case 4:
											frmUtil.TxtXLSheet.Text = text3;
											break;
										case 6:
											frmUtil.TxtXLMacro.Text = text3;
											break;
										case 7:
											frmUtil.CmbOpt.Text = text3;
											break;
										case 8:
											if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
											{
												frmUtil.chkOption.Checked = true;
											}
											else
											{
												frmUtil.chkOption.Checked = false;
											}
											break;
										}
									}
									else if ((Operators.CompareString(Globals_Renamed.currhelptmp, "Excel", TextCompare: false) == 0) & (Operators.CompareString(left, "L", TextCompare: false) == 0))
									{
										switch (num3)
										{
										case 1:
											frmUtil.TxtCSV.Text = Strings.Trim(text3);
											break;
										case 2:
											frmUtil.TxtOutXL.Text = Strings.Trim(text3);
											break;
										case 3:
											frmUtil.CmbOpt.Text = text3;
											break;
										case 4:
											if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
											{
												frmUtil.chkOption.Checked = true;
											}
											else
											{
												frmUtil.chkOption.Checked = false;
											}
											break;
										}
									}
									else if (IsUtil("EMAIL"))
									{
										switch (num3)
										{
										case 1:
											frmUtil.TxtCSV.Text = text3;
											frmUtil.chkOption.Checked = false;
											break;
										case 2:
											frmUtil.TxtOutXL.Text = Strings.Trim(text3);
											break;
										case 3:
											frmUtil.TxtXLSheet.Text = Strings.Trim(text3);
											break;
										case 4:
											frmUtil.TxtInXL.Text = Strings.Trim(text3);
											break;
										case 5:
											frmUtil.TxtXLMacro.Text = text3;
											break;
										case 6:
											frmUtil.TxtOther1.Text = text3;
											break;
										case 7:
											frmUtil.TxtOther2.Text = text3;
											break;
										case 8:
											if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
											{
												frmUtil.chkOption.Checked = true;
											}
											break;
										}
									}
									else
									{
										switch (num3)
										{
										case 1:
											frmUtil.TxtCSV.Text = Strings.Trim(text3);
											break;
										case 2:
											if (IsUtil("START-MACRO"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
											}
											else if (IsUtil("SITE-LOOP"))
											{
												frmUtil.ParallelThreadsComboBox1.Text = text3;
											}
											else
											{
												frmUtil.TxtOutXL.Text = Strings.Trim(text3);
											}
											break;
										case 3:
											if (IsUtil("JMP") || IsUtil("ROWS-IN-FILE") || IsUtil("PYTHON") || IsUtil("R") || IsUtil("COPY") || IsUtil("GET-TCA"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
											}
											else
											{
												frmUtil.TxtInXL.Text = Strings.Trim(text3);
											}
											break;
										case 4:
											if (IsUtil("SQLITE-LOAD") || IsUtil("WEB-COPY") || IsUtil("GET-FILES") || IsUtil("FILE-COMPARE"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
												if (IsUtil("WEB-COPY") && num5 == Strings.Len(text))
												{
													frmUtil.CmbOpt.Text = "Version 1";
												}
											}
											else if (IsUtil("ROWS-IN-FILE"))
											{
												frmUtil.TxtInXL.Text = Strings.Trim(text3);
											}
											else if (!IsUtil("PYTHON") && !IsUtil("R"))
											{
												frmUtil.TxtXLSheet.Text = text3;
											}
											break;
										case 5:
											if (IsUtil("TDX-TO-CSV"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
											}
											else if (IsUtil("FILE-COMPARE"))
											{
												frmUtil.TxtXLSheet.Text = text3;
											}
											else if (IsUtil("PYTHON") || IsUtil("WEB-COPY"))
											{
												frmUtil.CmbOpt.Text = text3;
											}
											else if (IsUtil("FOR-LOOP") | IsUtil("RUN-LOOP") | IsUtil("SITE-LOOP"))
											{
												frmUtil.ParallelThreadsComboBox1.Text = text3;
											}
											else
											{
												frmUtil.TxtXLMacro.Text = text3;
											}
											break;
										case 6:
											if (IsUtil("MONGODB-IMPORT") || IsUtil("MONGODB-EXTRACT") || IsUtil("IF-THEN") || IsUtil("BEGIN-HPC"))
											{
												frmUtil.TxtOther1.Text = text3;
											}
											else if (IsUtil("AUTO-COMMONALITY"))
											{
												frmUtil.mnuopt1.Text = text3;
											}
											else if (IsUtil("TDX-TO-CSV") || IsUtil("FILE-COMPARE"))
											{
												frmUtil.TxtXLMacro.Text = text3;
											}
											else if (IsUtil("WEB-COPY"))
											{
												frmUtil.TxtXLSheet.Text = text3;
											}
											else if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "N", TextCompare: false) == 0)
											{
												frmUtil.chkOption.Checked = false;
											}
											break;
										case 7:
											if (IsUtil("MONGODB-IMPORT") || IsUtil("MONGODB-EXTRACT") || IsUtil("IF-THEN"))
											{
												frmUtil.TxtOther2.Text = text3;
											}
											else if (IsUtil("AUTO-COMMONALITY"))
											{
												frmUtil.TxtOther1.Text = text3;
											}
											else if (IsUtil("BEGIN-HPC"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
											}
											else if (IsUtil("EXCEL-TO-CSV"))
											{
												frmUtil.TxtXLSheet.Text = text3;
											}
											else
											{
												frmUtil.TxtOther1.Text = text3;
											}
											break;
										case 8:
											if (IsUtil("MONGODB-EXTRACT"))
											{
												frmUtil.mnuopt1.Text = text3;
											}
											else if (IsUtil("MONGODB-IMPORT"))
											{
												if (Operators.CompareString(Strings.UCase(Strings.Trim(text3)), "Y", TextCompare: false) == 0)
												{
													frmUtil.chkOption.Checked = true;
												}
												else
												{
													frmUtil.chkOption.Checked = false;
												}
											}
											else if (IsUtil("BEGIN-HPC"))
											{
												frmUtil.CmbOpt.Text = "Version 2";
											}
											else
											{
												frmUtil.TxtOther2.Text = text3;
											}
											break;
										case 9:
											if (IsUtil("SMART-APPEND"))
											{
												if (Operators.CompareString(text3, "Version 2", TextCompare: false) == 0)
												{
													text3 = "Version 3";
													frmUtil.TxtOther1.Text = "N";
												}
												frmUtil.CmbOpt.Text = text3;
											}
											else if (IsUtil("AUTO-COMMONALITY"))
											{
												frmUtil.mnuOpt2.Text = text3;
											}
											else if (IsUtil("ROBOCOPY") || IsUtil("BEGIN-HPC"))
											{
												frmUtil.mnuopt1.Text = text3;
											}
											break;
										case 10:
											if (IsUtil("BEGIN-HPC"))
											{
												frmUtil.mnuOpt2.Text = text3;
											}
											else if (IsUtil("AUTO-COMMONALITY") && Operators.CompareString(Strings.LCase(text3), "<<<spf-instance>>>", TextCompare: false) != 0)
											{
												frmUtil.mnuopt3.Text = text3;
											}
											break;
										case 11:
											if (IsUtil("BEGIN-HPC"))
											{
												frmUtil.TxtOther2.Text = text3;
											}
											else if (IsUtil("AUTO-COMMONALITY"))
											{
												frmUtil.mnuopt4.Text = text3;
											}
											else
											{
												frmUtil.mnuopt1.Text = text3;
											}
											break;
										case 13:
											if (IsUtil("SMART-APPEND"))
											{
												frmUtil.mnuOpt2.Text = text3;
											}
											break;
										case 14:
											if (IsUtil("SMART-APPEND"))
											{
												if (Operators.CompareString(text3, "Y", TextCompare: false) == 0)
												{
													frmUtil.OrderedParallelMenuItem.Checked = true;
												}
												else
												{
													frmUtil.OrderedParallelMenuItem.Checked = false;
												}
											}
											break;
										}
									}
								}
								text3 = "";
							}
							else
							{
								text3 += text4;
							}
						}
						frmUtil.ShowDialog();
						if (Operators.CompareString(Globals_Renamed.currvaluetmp, "", TextCompare: false) != 0)
						{
							result = Globals_Renamed.currvaluetmp;
						}
						Globals_Renamed.currvaluetmp = "";
						goto end_IL_0001_2;
					}
					case 4658:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							Globals_Renamed.currhelptmp = "";
							Globals_Renamed.currvaluetmp = "";
							result = "";
							goto end_IL_0001_2;
						}
						break;
					}
					goto IL_1268;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 4658;
				continue;
			}
			break;
			IL_1268:
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

	public static TreeNode FindNodeByName(TreeNode MyNode, string MyName)
	{
		TreeNode treeNode = null;
		string text = "";
		TreeNode treeNode2 = null;
		foreach (TreeNode node in MyNode.Nodes)
		{
			text = node.Name.ToString();
			if (Operators.CompareString(text, MyName, TextCompare: false) == 0)
			{
				treeNode = node;
				break;
			}
			treeNode = FindNodeByName(node, MyName);
			if (treeNode != null)
			{
				break;
			}
		}
		return treeNode;
	}

	public static void Garbage_Collect()
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
				case 61:
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
					GC.Collect();
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 3;
				GC.WaitForPendingFinalizers();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 61;
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

	public static void UpdateNodeByName(string MyMode, ref TreeNode MyNode, string MyName, string MyValue)
	{
		TreeNode treeNode = FindNodeByName(MyNode, MyName);
		if (treeNode == null)
		{
			return;
		}
		string left = Strings.UCase(MyMode);
		if (Operators.CompareString(left, "T", TextCompare: false) != 0)
		{
			if (Operators.CompareString(left, "G", TextCompare: false) == 0)
			{
				treeNode.Tag = MyValue;
			}
		}
		else
		{
			treeNode.Text = MyValue;
		}
	}

	public static void Add_Tree_Node(TreeNode MyNode, string MyText, string MyName, string MyTag, short MyImgIdx)
	{
		if (MyNode != null)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Name = MyName;
			treeNode.Text = MyText;
			treeNode.Tag = MyTag;
			if (MyImgIdx != -1)
			{
				treeNode.ImageIndex = MyImgIdx;
				treeNode.SelectedImageIndex = MyImgIdx;
			}
			MyNode.Nodes.Add(treeNode);
		}
	}

	public static void Add_Tree_Node_by_Name(TreeNode MyTreeNode, string MyParentName, string MyText, string MyName, string MyTag, short MyImgIdx, bool DoExpand)
	{
		TreeNode treeNode = null;
		if (Operators.CompareString(MyParentName, "", TextCompare: false) == 0 || MyTreeNode == null)
		{
			return;
		}
		treeNode = FindNodeByName(MyTreeNode, MyParentName);
		if (treeNode != null)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Name = MyName;
			treeNode2.Text = MyText;
			treeNode2.Tag = MyTag;
			if (MyImgIdx != -1)
			{
				treeNode2.ImageIndex = MyImgIdx;
				treeNode2.SelectedImageIndex = MyImgIdx;
			}
			treeNode.Nodes.Add(treeNode2);
			if (DoExpand)
			{
				treeNode2.Expand();
			}
		}
	}

	public static bool FormIsLoaded(string MyFrm, short MyCount)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		bool result = default(bool);
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
					errsource = "General_Procedures - FormIsLoaded";
					string text = "";
					short num3 = 0;
					result = false;
					num3 = 0;
					MyFrm = Strings.UCase(MyFrm);
					IEnumerator enumerator = MyProject.Application.OpenForms.GetEnumerator();
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Form form = (Form)enumerator.Current;
							text = Strings.UCase(form.Name);
							if (Operators.CompareString(text, MyFrm, TextCompare: false) == 0)
							{
								num3 = checked((short)(num3 + 1));
								if (num3 >= MyCount)
								{
									result = true;
									break;
								}
							}
							continue;
						}
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
						break;
					}
					goto end_IL_0001;
				}
				case 211:
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
				try0001_dispatch = 211;
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

	public static short CountFormIsLoaded(string MyFrm)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		short num3 = default(short);
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
					errsource = "BuildForm - CountFormIsLoaded";
					string text = "";
					num3 = 0;
					MyFrm = Strings.UCase(MyFrm);
					IEnumerator enumerator = MyProject.Application.OpenForms.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Form form = (Form)enumerator.Current;
						text = Strings.UCase(form.Name);
						if (Operators.CompareString(text, MyFrm, TextCompare: false) == 0)
						{
							num3 = checked((short)(num3 + 1));
						}
					}
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto end_IL_0001;
				}
				case 184:
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
				try0001_dispatch = 184;
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
		return num3;
	}

	public static void Set_Form_Position(ref Form Form_Name)
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
				case 214:
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
							goto IL_0062;
						case 5:
							goto IL_008d;
						case 6:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000f:
					num2 = 3;
					Form_Name.Left = checked(Screen.FromControl(MyProject.Forms.FrmMain).Bounds.Left + Screen.FromControl(MyProject.Forms.FrmMain).Bounds.Width - (Form_Name.Width + 200));
					goto IL_0062;
					IL_0062:
					num2 = 4;
					num5 = Screen.FromControl(MyProject.Forms.FrmMain.FrmSQLQuery[Globals_Renamed.g_FrmIdx]).Bounds.Height;
					goto IL_008d;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_008d:
					num2 = 5;
					if (num5 != 480)
					{
						goto end_IL_0001_3;
					}
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 6;
				Form_Name.Top = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 214;
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

	public static string Process_Like_List(string MyCol, string MyList, string MyEsc)
	{
		string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
		long num = 0L;
		int num2 = 0;
		short num3 = 0;
		string CBBegin = "";
		string CBEnd = "";
		string text = "";
		string delim = ",";
		if (Strings.InStr(MyList, "</comma\\>") != 0)
		{
			delim = "</comma\\>";
		}
		num = General_Procedures.ParseAndFillArray(MyList, delim, ref DynArray);
		MyEsc = Strings.Trim(MyEsc);
		num3 = 1;
		BuildSQL.Set_Engine_Delimiters(ref CBBegin, ref CBEnd);
		string text2 = "";
		checked
		{
			int num4 = (int)num;
			for (num2 = 1; num2 <= num4; num2++)
			{
				if (Operators.CompareString(Strings.Trim(DynArray[num2]), "", TextCompare: false) != 0)
				{
					text = Strings.RTrim(General_Procedures.StripQuotesAndDates(DynArray[num2], "C", 0));
					if (num3 != 1)
					{
						text2 = ((!((Strings.InStr(text, "%") == 0) & (Strings.InStr(text, "_") == 0))) ? (text2 + Globals_Renamed.CRLF + CBBegin + "OR " + MyCol + " LIKE '" + text + "'") : (text2 + Globals_Renamed.CRLF + CBBegin + "OR " + MyCol + " LIKE '" + text + "%'"));
					}
					else
					{
						text2 = ((!((Strings.InStr(text, "%") == 0) & (Strings.InStr(text, "_") == 0))) ? (" '" + text + "'") : (" '" + text + "%'"));
						num3 = 0;
					}
					if (Operators.CompareString(MyEsc, "", TextCompare: false) != 0 && Strings.InStr(text, MyEsc) != 0)
					{
						text2 = text2 + " ESCAPE '" + MyEsc + "'";
					}
					text2 += CBEnd;
				}
			}
			text2 = text2 + Globals_Renamed.CRLF + CBBegin + ")";
			DynArray = null;
			return text2;
		}
	}

	public static string Process_Regex_List(string MyCol, string MyList)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string text = default(string);
		string text2 = default(string);
		int num6 = default(int);
		string[] DynArray = default(string[]);
		long num7 = default(long);
		string delim = default(string);
		int num8 = default(int);
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
				case 487:
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
						case 4:
							goto IL_0027;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_0031;
						case 7:
							goto IL_003a;
						case 8:
							goto IL_0043;
						case 9:
							goto IL_005a;
						case 10:
						case 11:
							goto IL_0066;
						case 12:
							goto IL_0076;
						case 13:
							goto IL_007c;
						case 14:
							goto IL_0085;
						case 15:
							goto IL_0095;
						case 16:
							goto IL_00b9;
						case 17:
							goto IL_00d3;
						case 18:
							goto IL_00e1;
						case 19:
							goto IL_0111;
						case 21:
							goto IL_011b;
						case 20:
						case 22:
						case 23:
						case 24:
							goto IL_0152;
						case 25:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 26:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0111:
					num2 = 19;
					num5 = 0;
					goto IL_0152;
					IL_011b:
					num2 = 21;
					text = text + "\r\n    ,{'" + MyCol + "' : {'$regex' : '" + text2 + "', '$options': 'i'}}";
					goto IL_0152;
					IL_00e1:
					num2 = 18;
					text = "     {'" + MyCol + "' : {'$regex' : '" + text2 + "', '$options': 'i'}}";
					goto IL_0111;
					IL_0152:
					num2 = 24;
					num6 = checked(num6 + 1);
					goto IL_015b;
					IL_000b:
					num2 = 2;
					DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
					goto IL_0021;
					IL_0021:
					num2 = 3;
					num7 = 0L;
					goto IL_0027;
					IL_0027:
					num2 = 4;
					num6 = 0;
					goto IL_002c;
					IL_002c:
					num2 = 5;
					num5 = 0;
					goto IL_0031;
					IL_0031:
					num2 = 6;
					text2 = "";
					goto IL_003a;
					IL_003a:
					num2 = 7;
					delim = ",";
					goto IL_0043;
					IL_0043:
					num2 = 8;
					if (Strings.InStr(MyList, "</comma\\>") != 0)
					{
						goto IL_005a;
					}
					goto IL_0066;
					IL_005a:
					num2 = 9;
					delim = "</comma\\>";
					goto IL_0066;
					IL_0066:
					num2 = 11;
					num7 = General_Procedures.ParseAndFillArray(MyList, delim, ref DynArray);
					goto IL_0076;
					IL_0076:
					num2 = 12;
					num5 = 1;
					goto IL_007c;
					IL_007c:
					num2 = 13;
					text = "";
					goto IL_0085;
					IL_0085:
					num2 = 14;
					num8 = checked((int)num7);
					num6 = 1;
					goto IL_015b;
					IL_015b:
					if (num6 > num8)
					{
						break;
					}
					goto IL_0095;
					IL_0095:
					num2 = 15;
					if (Operators.CompareString(Strings.Trim(DynArray[num6]), "", TextCompare: false) != 0)
					{
						goto IL_00b9;
					}
					goto IL_0152;
					IL_00b9:
					num2 = 16;
					text2 = Strings.RTrim(General_Procedures.StripQuotesAndDates(DynArray[num6], "C", 0));
					goto IL_00d3;
					IL_00d3:
					num2 = 17;
					if (num5 == 1)
					{
						goto IL_00e1;
					}
					goto IL_011b;
					end_IL_0001_2:
					break;
				}
				num2 = 25;
				DynArray = null;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 487;
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

	public static string HG_CmpxpCal(string MyOrigText, string MyDT, string MyAlias)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string currtxttmp = default(string);
		string currdatatypetmp = default(string);
		string result = default(string);
		short currrowtmp = default(short);
		FrmCalendar frmCalendar = default(FrmCalendar);
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
				case 1159:
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
							goto IL_0026;
						case 7:
							goto IL_0033;
						case 9:
							goto IL_02eb;
						case 10:
							goto IL_02f5;
						case 11:
							goto IL_02ff;
						case 12:
							goto IL_0309;
						case 13:
							goto IL_032a;
						case 14:
							goto IL_0333;
						case 15:
							goto IL_033d;
						case 16:
							goto IL_0347;
						case 18:
							goto IL_0357;
						case 19:
							goto IL_0360;
						case 20:
							goto IL_038e;
						case 21:
							goto IL_03a1;
						case 22:
						case 23:
							goto IL_03b8;
						case 24:
							goto IL_03c6;
						case 25:
							goto IL_03d0;
						case 26:
							goto IL_03db;
						case 8:
						case 17:
						case 27:
						case 28:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 29:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0333:
					num2 = 14;
					Globals_Renamed.currtxttmp = currtxttmp;
					goto IL_033d;
					IL_033d:
					num2 = 15;
					Globals_Renamed.currdatatypetmp = currdatatypetmp;
					goto IL_0347;
					IL_032a:
					num2 = 13;
					result = Globals_Renamed.currtxttmp;
					goto IL_0333;
					IL_0347:
					num2 = 16;
					Globals_Renamed.currrowtmp = currrowtmp;
					break;
					IL_000b:
					num2 = 2;
					result = MyOrigText;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					currtxttmp = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					currdatatypetmp = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					currrowtmp = 0;
					goto IL_0026;
					IL_0026:
					num2 = 6;
					Globals_Renamed.gCalOffset = "";
					goto IL_0033;
					IL_0033:
					num2 = 7;
					switch (Strings.UCase(MyDT))
					{
					case "E":
						break;
					case "D":
					case "T":
					case "H":
					case "G":
					case "U":
					case "M":
					case "O":
					case "L":
					case "P":
					case "V":
					case "S":
					case "S2":
					case "W":
					case "J":
					case "K":
						goto IL_0357;
					default:
						goto end_IL_0001_2;
					}
					goto IL_02eb;
					IL_0357:
					num2 = 18;
					Globals_Renamed.MyCalValue = MyOrigText;
					goto IL_0360;
					IL_0360:
					num2 = 19;
					if ((Operators.CompareString(Strings.UCase(MyDT), "G", TextCompare: false) == 0) & LikeOperator.LikeString(Globals_Renamed.MyCalValue, "####-##-## ##:##:##[-+]##:##", CompareMethod.Binary))
					{
						goto IL_038e;
					}
					goto IL_03b8;
					IL_038e:
					num2 = 20;
					Globals_Renamed.gCalOffset = Strings.Right(Globals_Renamed.MyCalValue, 6);
					goto IL_03a1;
					IL_03a1:
					num2 = 21;
					Globals_Renamed.MyCalValue = Strings.Mid(Globals_Renamed.MyCalValue, 1, 19);
					goto IL_03b8;
					IL_03b8:
					num2 = 23;
					Globals_Renamed.MyCalDateType = Strings.UCase(MyDT);
					goto IL_03c6;
					IL_03c6:
					num2 = 24;
					frmCalendar = new FrmCalendar();
					goto IL_03d0;
					IL_03d0:
					num2 = 25;
					frmCalendar.ShowDialog();
					goto IL_03db;
					IL_03db:
					num2 = 26;
					result = Globals_Renamed.MyCalValue + Globals_Renamed.gCalOffset;
					break;
					IL_02eb:
					num2 = 9;
					currtxttmp = Globals_Renamed.currtxttmp;
					goto IL_02f5;
					IL_02f5:
					num2 = 10;
					currdatatypetmp = Globals_Renamed.currdatatypetmp;
					goto IL_02ff;
					IL_02ff:
					num2 = 11;
					currrowtmp = Globals_Renamed.currrowtmp;
					goto IL_0309;
					IL_0309:
					num2 = 12;
					MyProject.Forms.FrmMain.LoadComputedColumnForm(3, MyOrigText, MyAlias, "", "", -1);
					goto IL_032a;
					end_IL_0001_2:
					break;
				}
				num2 = 28;
				Globals_Renamed.gCalOffset = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1159;
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

	public static void HG_Fltr_Hlp_2(string MyHlpQ, string MyDT, string MyOpr, ref ComboBox TxtBox1, ref ComboBox cmbColIG, ref Button CmdLoadGrid, short MyOpt, string MyAlias, short ll_ObjectType)
	{
		int try0001_dispatch = -1;
		string errsource = default(string);
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
						errsource = "BuildForm - HG_Fltr_Hlp_2";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						string text4 = "";
						if (LikeOperator.LikeString(Strings.UCase(MyOpr), "* FILE", CompareMethod.Binary) | LikeOperator.LikeString(Strings.UCase(MyOpr), "* GROUP*", CompareMethod.Binary))
						{
							text3 = ((!LikeOperator.LikeString(Strings.UCase(MyOpr), "* GROUP*", CompareMethod.Binary)) ? "txt" : "csv+");
							FileOpenSave("O", "", text3, "Find File With Items To Use In Row Filter", "");
							if (!((Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "CANCEL", TextCompare: false) != 0) & (Operators.CompareString(MyProject.Forms.FrmMain.CMDialog1Open.FileName, "", TextCompare: false) != 0)))
							{
								goto end_IL_0001;
							}
							text2 = MyProject.Forms.FrmMain.CMDialog1Open.FileName;
							if (LikeOperator.LikeString(Strings.UCase(MyOpr), "* GROUP*", CompareMethod.Binary))
							{
								num3 = (short)Strings.InStrRev(text2, "\\", -1, CompareMethod.Text);
								text = ((num3 == 0) ? "" : Strings.Mid(text2, 1, num3));
								if (Operators.CompareString(Strings.UCase(Globals_Renamed.MyPCDir), Strings.UCase(text), TextCompare: false) == 0)
								{
									text2 = ".\\" + Strings.Mid(text2, num3 + 1);
								}
								else if (Operators.CompareString(Strings.UCase(Globals_Renamed.gQueryDir), Strings.UCase(text), TextCompare: false) == 0)
								{
									text2 = "<<<spf-query-dir>>>" + Strings.Mid(text2, num3 + 1);
								}
							}
							TxtBox1.Text = text2;
							cmbColIG.Text = "1";
							goto end_IL_0001;
						}
						if (Operators.CompareString(Strings.UCase(MyDT), "E", TextCompare: false) == 0 || General_Procedures.IsDateDT(MyDT, 3))
						{
							TxtBox1.Text = HG_CmpxpCal(TxtBox1.Text, MyDT, MyAlias);
							goto end_IL_0001;
						}
						if (Operators.CompareString(Strings.Trim(MyHlpQ), "", TextCompare: false) == 0)
						{
							Interaction.MsgBox("No help is defined for this field. Let support know if this is important to you.", MsgBoxStyle.Information, "No Help Query");
							goto end_IL_0001;
						}
						if (Operators.CompareString(Globals_Renamed.gUsePyEngine, "N", TextCompare: false) == 0)
						{
							Interaction.MsgBox("Help Queries are now only supported for the Python Engine", MsgBoxStyle.Information, "Not Supported");
							goto end_IL_0001;
						}
						if (Operators.CompareString(Strings.Mid(MyHlpQ, 1, 1), "!", TextCompare: false) == 0)
						{
							text4 = Strings.Trim(Strings.Mid(MyHlpQ + " ", 2).ToLower());
							text4 = Strings.Trim(General_Procedures.Get_Ini_Data("Column Help", text4, "", 5000, Globals_Renamed.MySchemaDir + "\\Columns.ini"));
							MyHlpQ = text4;
						}
						if (Operators.CompareString(MyHlpQ, "", TextCompare: false) != 0)
						{
							if (Operators.CompareString(Strings.Mid(MyHlpQ, 1, 1), "~", TextCompare: false) == 0)
							{
								Interaction.MsgBox("Obtain Valid Values by clicking the drop down arrow of the Enter Value combo box", MsgBoxStyle.Information, "Valid Values");
							}
							else
							{
								BuildSQL.RunQuery(MyHlpQ, 2, MyOpt, ll_ObjectType, "");
							}
						}
						goto end_IL_0001_2;
					}
					case 889:
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
					goto IL_03af;
				}
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 889;
				continue;
			}
			break;
			IL_03af:
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

	public static void HG_Copy2(ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		DataGridViewRow dataGridViewRow = default(DataGridViewRow);
		int num5 = default(int);
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
							goto IL_0013;
						case 4:
							goto IL_0023;
						case 5:
							goto IL_0030;
						case 6:
							goto IL_0035;
						case 7:
							goto IL_0055;
						case 8:
							goto IL_006c;
						case 10:
							goto IL_0090;
						case 9:
						case 11:
						case 12:
							goto IL_00bd;
						case 13:
							goto IL_00d0;
						case 14:
							goto IL_00e9;
						case 15:
							goto IL_0101;
						case 16:
							goto IL_0114;
						case 17:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0090:
					num2 = 10;
					text = text + "\r\n" + HG_Strip(Conversions.ToString(dataGridViewRow.Cells[num5].Value));
					goto IL_00bd;
					IL_0055:
					num2 = 7;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto IL_006c;
					}
					goto IL_0090;
					IL_0114:
					num2 = 16;
					MyProject.Computer.Clipboard.SetText(text);
					break;
					IL_006c:
					num2 = 8;
					text = HG_Strip(Conversions.ToString(dataGridViewRow.Cells[num5].Value));
					goto IL_00bd;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					num5 = MyGrid.CurrentCell.ColumnIndex;
					goto IL_0023;
					IL_0023:
					num2 = 4;
					if (num5 == -1)
					{
						goto IL_0030;
					}
					goto IL_0035;
					IL_0030:
					num2 = 5;
					num5 = 0;
					goto IL_0035;
					IL_0035:
					num2 = 6;
					enumerator = MyGrid.SelectedRows.GetEnumerator();
					goto IL_00c0;
					IL_00c0:
					if (enumerator.MoveNext())
					{
						dataGridViewRow = (DataGridViewRow)enumerator.Current;
						goto IL_0055;
					}
					goto IL_00d0;
					IL_00d0:
					num2 = 13;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
					goto IL_00e9;
					IL_00bd:
					num2 = 12;
					goto IL_00c0;
					IL_00e9:
					num2 = 14;
					if (Operators.CompareString(text, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0101;
					IL_0101:
					num2 = 15;
					MyProject.Computer.Clipboard.Clear();
					goto IL_0114;
					end_IL_0001_2:
					break;
				}
				num2 = 17;
				text = "";
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

	public static void HG_mnuPaste2(ref DataGridView Grid, short l_TotalRows)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string[] array = default(string[]);
		short num6 = default(short);
		string expression = default(string);
		string text = default(string);
		int num7 = default(int);
		int num8 = default(int);
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
					case 534:
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
								goto IL_0021;
							case 6:
								goto IL_0026;
							case 7:
								goto IL_002b;
							case 8:
								goto IL_0030;
							case 9:
								goto IL_003c;
							case 10:
								goto IL_006e;
							case 12:
							case 13:
								goto IL_007a;
							case 11:
							case 14:
								goto IL_0089;
							case 15:
								goto IL_00a4;
							case 16:
								goto IL_00b7;
							case 17:
								goto IL_00c9;
							case 18:
								goto IL_00db;
							case 19:
								goto IL_00ea;
							case 20:
								goto IL_0103;
							case 21:
								goto IL_0127;
							case 22:
								goto IL_0131;
							case 24:
							case 25:
								goto IL_0146;
							case 23:
							case 26:
							case 27:
								goto IL_0157;
							case 28:
								goto IL_015d;
							case 29:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 30:
							case 31:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0146:
						num2 = 25;
						num5++;
						goto IL_014f;
						IL_0157:
						num2 = 27;
						array = null;
						goto IL_015d;
						IL_0131:
						num2 = 22;
						if (num6 < l_TotalRows)
						{
							goto IL_0146;
						}
						goto IL_0157;
						IL_015d:
						num2 = 28;
						if (num6 < l_TotalRows)
						{
							goto end_IL_0001_3;
						}
						break;
						IL_000b:
						num2 = 2;
						expression = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						num5 = 0;
						goto IL_0018;
						IL_0018:
						num2 = 4;
						text = "";
						goto IL_0021;
						IL_0021:
						num2 = 5;
						num6 = 0;
						goto IL_0026;
						IL_0026:
						num2 = 6;
						array = null;
						goto IL_002b;
						IL_002b:
						num2 = 7;
						num6 = l_TotalRows;
						goto IL_0030;
						IL_0030:
						num2 = 8;
						num7 = l_TotalRows - 1;
						num5 = 0;
						goto IL_0083;
						IL_0083:
						if (num5 <= num7)
						{
							goto IL_003c;
						}
						goto IL_0089;
						IL_003c:
						num2 = 9;
						if (Operators.ConditionalCompareObjectEqual(Grid.Rows[num5].Cells[0].Value, "", TextCompare: false))
						{
							goto IL_006e;
						}
						goto IL_007a;
						IL_006e:
						num2 = 10;
						num6 = (short)num5;
						goto IL_0089;
						IL_0089:
						num2 = 14;
						if (num6 < l_TotalRows && Grid.Visible)
						{
							goto IL_00a4;
						}
						goto IL_0157;
						IL_007a:
						num2 = 13;
						num5++;
						goto IL_0083;
						IL_00a4:
						num2 = 15;
						expression = MyProject.Computer.Clipboard.GetText();
						goto IL_00b7;
						IL_00b7:
						num2 = 16;
						array = Strings.Split(expression, "\r\n");
						goto IL_00c9;
						IL_00c9:
						num2 = 17;
						num8 = Information.UBound(array);
						num5 = 0;
						goto IL_014f;
						IL_014f:
						if (num5 <= num8)
						{
							goto IL_00db;
						}
						goto IL_0157;
						IL_00db:
						num2 = 18;
						text = Strings.Trim(array[num5]);
						goto IL_00ea;
						IL_00ea:
						num2 = 19;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_0103;
						}
						goto IL_0146;
						IL_0103:
						num2 = 20;
						Grid.Rows[num6].Cells[0].Value = text;
						goto IL_0127;
						IL_0127:
						num2 = 21;
						num6++;
						goto IL_0131;
						end_IL_0001_2:
						break;
					}
					num2 = 29;
					Interaction.MsgBox("The maximum number of rows was exceeded in the Filter Grid. No more rows can be pasted!", MsgBoxStyle.Exclamation, "Max Rows");
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 534;
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

	public static void HG_mnuClear2(ref DataGridView Grid)
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
				case 188:
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
							goto IL_0035;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_004a;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0035:
					num2 = 5;
					if (num5 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0041;
					IL_0041:
					num2 = 6;
					GridModule.Clear_A_Grid(ref Grid);
					goto IL_004a;
					IL_001e:
					num2 = 4;
					num5 = (int)Interaction.MsgBox("Are you sure you want to clear the grid?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question | MsgBoxStyle.DefaultButton3 | MsgBoxStyle.SystemModal, "Clear Grid?");
					goto IL_0035;
					IL_004a:
					num2 = 7;
					Grid.CurrentCell = Grid.Rows[0].Cells[0];
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (!Grid.Visible)
					{
						goto end_IL_0001_3;
					}
					goto IL_001e;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Grid.FirstDisplayedScrollingRowIndex = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 188;
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

	public static void HG_cmbcolIG_Clk(ref ComboBox cmbColIG)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
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
				case 141:
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
							goto IL_0023;
						case 6:
							goto IL_0034;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0018:
					num2 = 4;
					text = cmbColIG.Text;
					goto IL_0023;
					IL_0023:
					num2 = 5;
					num5 = checked((short)Strings.InStr(text, "."));
					goto IL_0034;
					IL_000f:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0034:
					num2 = 6;
					if (num5 == 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				cmbColIG.Text = Strings.Mid(text, 1, checked(num5 - 1));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 141;
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

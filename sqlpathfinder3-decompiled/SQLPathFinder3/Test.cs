using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class Test
{
	private static Microsoft.Office.Interop.Excel.Application gObjExcel = null;

	private static Workbook gObjXLWB = null;

	public static void ForceExcelToQuit(bool MyMode)
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
				case 419:
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
							goto IL_001c;
						case 4:
							goto IL_0024;
						case 5:
							goto IL_002c;
						case 6:
							goto IL_0034;
						case 7:
							goto IL_003c;
						case 8:
							goto IL_004c;
						case 9:
							goto IL_0055;
						case 11:
							goto IL_0085;
						case 10:
						case 12:
						case 13:
							goto IL_00ae;
						case 14:
						case 15:
							goto IL_00be;
						case 16:
							goto IL_00d1;
						case 17:
							goto IL_00df;
						case 18:
							goto IL_00e8;
						case 19:
							goto IL_00f1;
						case 20:
							goto IL_00fa;
						case 21:
							goto IL_0103;
						case 22:
						case 23:
							goto IL_0113;
						case 24:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00fa:
					num2 = 20;
					GC.WaitForPendingFinalizers();
					goto IL_0103;
					IL_0103:
					num2 = 21;
					Marshal.FinalReleaseComObject(gObjExcel);
					goto IL_0113;
					IL_00f1:
					num2 = 19;
					GC.Collect();
					goto IL_00fa;
					IL_0113:
					num2 = 23;
					Globals_Renamed.Excel_Connected2 = 0;
					break;
					IL_000b:
					num2 = 2;
					if (gObjExcel != null)
					{
						goto IL_001c;
					}
					goto IL_0113;
					IL_001c:
					num2 = 3;
					GC.Collect();
					goto IL_0024;
					IL_0024:
					num2 = 4;
					GC.WaitForPendingFinalizers();
					goto IL_002c;
					IL_002c:
					num2 = 5;
					GC.Collect();
					goto IL_0034;
					IL_0034:
					num2 = 6;
					GC.WaitForPendingFinalizers();
					goto IL_003c;
					IL_003c:
					num2 = 7;
					if (gObjXLWB != null)
					{
						goto IL_004c;
					}
					goto IL_00be;
					IL_004c:
					num2 = 8;
					if (MyMode)
					{
						goto IL_0055;
					}
					goto IL_0085;
					IL_0055:
					num2 = 9;
					gObjXLWB.Close(RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value));
					goto IL_00ae;
					IL_0085:
					num2 = 11;
					gObjXLWB.Close(false, RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value));
					goto IL_00ae;
					IL_00ae:
					num2 = 13;
					Marshal.FinalReleaseComObject(gObjXLWB);
					goto IL_00be;
					IL_00be:
					num2 = 15;
					gObjExcel.Workbooks.Close();
					goto IL_00d1;
					IL_00d1:
					num2 = 16;
					gObjExcel.Quit();
					goto IL_00df;
					IL_00df:
					num2 = 17;
					gObjXLWB = null;
					goto IL_00e8;
					IL_00e8:
					num2 = 18;
					gObjExcel = null;
					goto IL_00f1;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 419;
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

	public static void Invoke_Excel3(short Action, string MyXLFile)
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
				case 107:
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
					if (Operators.CompareString(Globals_Renamed.MyExcelPath, "Automatic", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_0024;
					IL_0024:
					num2 = 3;
					Invoke_Excel2(Action, MyXLFile);
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				BuildForm.Invoke_Excel(Action, MyXLFile);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 107;
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

	public static void Invoke_Excel2(short Action, string MyXLFile)
	{
		int num = 0;
		int num2 = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = true;
		switch (Action)
		{
		case 1:
		case 2:
			flag = true;
			if (Action == 1)
			{
				MyXLFile = Globals_Renamed.ExcelPending;
			}
			if (Operators.CompareString(MyXLFile, "", TextCompare: false) != 0)
			{
				MyXLFile = BuildForm.Replace_Globals(MyXLFile, 1);
				if (Strings.InStrRev(MyXLFile, "\\") == 0)
				{
					MyXLFile = Globals_Renamed.MyPCDir + Strings.Trim(MyXLFile);
				}
				if (!MyProject.Computer.FileSystem.FileExists(MyXLFile))
				{
					MyXLFile = "";
				}
			}
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (Globals_Renamed.Excel_Connected2 == 0)
				{
					gObjExcel = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
				}
				gObjXLWB = null;
				if (Operators.CompareString(MyXLFile, "", TextCompare: false) != 0)
				{
					num2 = Strings.InStrRev(MyXLFile, ".");
					if (num2 != 0 && Operators.CompareString(Strings.Trim(Strings.UCase(Strings.Mid(MyXLFile, num2))), ".TAB", TextCompare: false) == 0)
					{
						flag2 = true;
						flag3 = false;
					}
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
					File.SetAttributes(MyXLFile, FileAttributes.ReadOnly);
					gObjExcel.Workbooks.OpenText(MyXLFile, RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), 1, XlTextQualifier.xlTextQualifierDoubleQuote, RuntimeHelpers.GetObjectValue(Missing.Value), flag2, RuntimeHelpers.GetObjectValue(Missing.Value), flag3, RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value), RuntimeHelpers.GetObjectValue(Missing.Value));
					gObjXLWB = gObjExcel.ActiveWorkbook;
					File.SetAttributes(MyXLFile, FileAttributes.Normal);
				}
				gObjExcel.Visible = true;
				gObjExcel.UserControl = true;
				Globals_Renamed.Excel_Connected2 = 1;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error accessing Excel. Did you close Excel?  If problem persists, contact Support:, (" + ex2.ToString() + ").", MsgBoxStyle.Exclamation, "Excel Automation Error");
				ForceExcelToQuit(MyMode: false);
				flag = false;
				ProjectData.ClearProjectError();
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
			try
			{
				if (flag)
				{
					num = Globals_Renamed.ShowWindow(gObjExcel.Hwnd, 1);
					num = Globals_Renamed.SetWindowPos(gObjExcel.Hwnd, 0, 0, 0, 0, 0, -1);
				}
				break;
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Interaction.MsgBox("Error Showing Excel. (" + ex4.ToString() + "). Manually display Excel if you can.", MsgBoxStyle.Exclamation, "Excel Error");
				ProjectData.ClearProjectError();
				break;
			}
		case 0:
			if (Globals_Renamed.Excel_Connected2 == 1)
			{
				ForceExcelToQuit(MyMode: true);
			}
			break;
		}
	}
}

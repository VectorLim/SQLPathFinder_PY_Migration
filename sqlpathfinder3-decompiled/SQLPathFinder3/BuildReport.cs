using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildReport
{
	public static void Report_Verify_Columns(ref DataGridView MyGrid, ref string[] lCSVCols, int MyCol, ref string MyMissData, int Opts)
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
						errsource = "BuildReport - Report_Verify_Columns";
						int num3 = 0;
						bool flag = false;
						string text = "";
						int num4 = 0;
						int num5 = 0;
						bool flag2 = true;
						int num6 = MyGrid.RowCount - 1;
						for (num3 = 0; num3 <= num6; num3++)
						{
							object[] array;
							DataGridViewCell dataGridViewCell;
							bool[] array2;
							object obj = NewLateBinding.LateGet(null, typeof(Strings), "LCase", array = new object[1] { (dataGridViewCell = MyGrid.Rows[num3].Cells[MyCol]).Value }, null, null, array2 = new bool[1] { true });
							if (array2[0])
							{
								dataGridViewCell.Value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
							}
							text = Strings.Trim(Conversions.ToString(obj));
							if (text.Length == 1 && Strings.Asc(text) < 65)
							{
								text = "";
							}
							flag2 = true;
							if (Opts == 1)
							{
								switch (text)
								{
								default:
									if (Operators.CompareString(text, "starts/ends with (%):", TextCompare: false) != 0)
									{
										break;
									}
									goto case "starts with:";
								case "starts with:":
								case "ends with:":
								case "contains:":
									flag2 = false;
									break;
								}
							}
							if (Operators.CompareString(text, "", TextCompare: false) == 0 || !flag2)
							{
								continue;
							}
							flag = false;
							int num7 = Information.UBound(lCSVCols);
							for (num4 = 0; num4 <= num7; num4++)
							{
								if (Operators.CompareString(text, Strings.Trim(Strings.LCase(lCSVCols[num4])), TextCompare: false) == 0)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								if (Operators.CompareString(MyMissData, "", TextCompare: false) == 0)
								{
									MyMissData = text;
									num5 = Information.UBound(lCSVCols) + 1;
									lCSVCols = (string[])Utils.CopyArray(lCSVCols, new string[num5 + 1]);
									lCSVCols[num5] = "===================";
								}
								else if (text.Length != 1 || Strings.Asc(text) >= 65)
								{
									MyMissData = MyMissData + " ," + text;
								}
								num5 = Information.UBound(lCSVCols) + 1;
								lCSVCols = (string[])Utils.CopyArray(lCSVCols, new string[num5 + 1]);
								lCSVCols[num5] = text;
							}
						}
						goto end_IL_0001;
					}
					case 640:
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
					goto IL_02b6;
				}
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 640;
				continue;
			}
			break;
			IL_02b6:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public static void Initialize_Grid_Format_Values_1(string MyKey, ref DataGridViewComboBoxColumn Cmb1, int f_MaxObj, ref Globals_Renamed.Report_Format_Type[] CSSObj)
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
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 240:
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
							goto IL_002d;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_004a;
						case 8:
							goto IL_0085;
						case 9:
						case 10:
							goto IL_00a3;
						default:
							goto end_IL_0001;
						case 11:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_004a:
					num2 = 7;
					if ((Operators.CompareString(CSSObj[num5].ObjType, "values", TextCompare: false) == 0) & (Operators.CompareString(CSSObj[num5].Name, MyKey, TextCompare: false) == 0))
					{
						goto IL_0085;
					}
					goto IL_00a3;
					IL_0085:
					num2 = 8;
					Cmb1.Items.Add(CSSObj[num5].Value);
					goto IL_00a3;
					IL_00aa:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_004a;
					IL_00a3:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_00aa;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyKey = Strings.LCase(Strings.Trim(MyKey));
					goto IL_001e;
					IL_001e:
					num2 = 4;
					Cmb1.Items.Clear();
					goto IL_002d;
					IL_002d:
					num2 = 5;
					Cmb1.Items.Add("");
					goto IL_0041;
					IL_0041:
					num2 = 6;
					num6 = f_MaxObj;
					num5 = 1;
					goto IL_00aa;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 240;
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

	public static string Get_CSS_Attr(string MyHeader, int f_MaxObj, ref Globals_Renamed.Report_Format_Type[] CSSObj)
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
				case 222:
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
							goto IL_001f;
						case 5:
							goto IL_0023;
						case 6:
							goto IL_002d;
						case 7:
							goto end_IL_0001_2;
						case 9:
						case 10:
							goto IL_008e;
						default:
							goto end_IL_0001;
						case 8:
						case 11:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0097:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_002d;
					IL_002d:
					num2 = 6;
					if ((Operators.CompareString(CSSObj[num5].ObjType, "headers", TextCompare: false) == 0) & (Operators.CompareString(Strings.Trim(Strings.LCase(CSSObj[num5].Name)), MyHeader, TextCompare: false) == 0))
					{
						break;
					}
					goto IL_008e;
					IL_0023:
					num2 = 5;
					num6 = f_MaxObj;
					num5 = 0;
					goto IL_0097;
					IL_008e:
					num2 = 10;
					num5 = checked(num5 + 1);
					goto IL_0097;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					MyHeader = Strings.Trim(Strings.LCase(MyHeader));
					goto IL_001f;
					IL_001f:
					num2 = 4;
					result = MyHeader;
					goto IL_0023;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				result = Strings.LCase(CSSObj[num5].Value);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 222;
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

	public static void Load_CSSObj(ref Globals_Renamed.Report_Format_Type[] CSSObj, ref int f_MaxObj)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text = default(string);
		object MyReader = default(object);
		bool flag = default(bool);
		string MyMsg = default(string);
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
						text = "BuildReport - Load_CSSObj";
						MyReader = null;
						flag = false;
						MyMsg = "";
						string[] array = null;
						int num3 = 0;
						int num4 = Information.UBound(CSSObj);
						for (num3 = 0; num3 <= num4; num3++)
						{
							CSSObj[num3].ObjType = "";
							CSSObj[num3].Name = "";
							CSSObj[num3].Value = "";
						}
						f_MaxObj = 0;
						num3 = 0;
						flag = BuildForm.OpenDelimitedFile(Globals_Renamed.gChartDir + "\\Report_Values.dat", ref MyReader, ref MyMsg);
						if (!flag)
						{
							break;
						}
						while (true)
						{
							if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
							{
								array = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
								if (!BuildForm.VerifySchemaRow(ref array, 3, ref MyMsg))
								{
									goto end_IL_0001;
								}
								CSSObj[num3].ObjType = array[0];
								CSSObj[num3].Name = array[1];
								CSSObj[num3].Value = array[2];
								if (Operators.CompareString(Strings.Mid(CSSObj[num3].ObjType, 1, 1), "!", TextCompare: false) != 0)
								{
									CSSObj[num3].ObjType = Strings.LCase(Strings.Trim(Strings.LCase(CSSObj[num3].ObjType)));
									CSSObj[num3].Name = Strings.Trim(CSSObj[num3].Name);
									CSSObj[num3].Value = Strings.Trim(CSSObj[num3].Value);
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
							f_MaxObj = num3;
							break;
						}
						goto end_IL_0001_2;
					}
					case 676:
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
					Interaction.MsgBox(text + " - Error loading CSS Objects (" + MyMsg + ")", MsgBoxStyle.Critical, "Load Error");
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
				try0001_dispatch = 676;
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

	public static void Grid_Row_Del(ref DataGridView MyGrid)
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
				int rowCount;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
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
							goto IL_000b;
						case 3:
							goto IL_0015;
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
					rowCount = MyGrid.RowCount;
					goto IL_0015;
					IL_0015:
					num2 = 3;
					GridModule.Grid_Delete_Multi(ref MyGrid);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				GridModule.Number_Grid(ref MyGrid);
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

	public static string Add_Delimiter(int NoCols, int LastCol)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		string text = default(string);
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
				case 120:
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
							goto IL_0017;
						case 5:
							goto IL_001f;
						case 6:
							goto IL_002a;
						case 7:
							goto IL_0038;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0040:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_002a;
					IL_002a:
					num2 = 6;
					text += "<\\\\>";
					goto IL_0038;
					IL_001f:
					num2 = 5;
					num6 = num7;
					num5 = 1;
					goto IL_0040;
					IL_0038:
					num2 = 7;
					num5 = checked(num5 + 1);
					goto IL_0040;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num7 = checked(NoCols - LastCol);
					goto IL_0017;
					IL_0017:
					num2 = 4;
					text = "";
					goto IL_001f;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 120;
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

	public static void Verify_Column(ref DataGridViewComboBoxColumn MyCombo, string MyCol)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
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
				case 197:
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
							goto IL_002a;
						case 6:
							goto IL_0046;
						case 8:
						case 9:
							goto IL_004f;
						case 7:
						case 10:
							goto IL_005b;
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
					IL_0046:
					num2 = 6;
					flag = true;
					goto IL_005b;
					IL_005b:
					num2 = 10;
					if (flag)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_002a:
					num2 = 5;
					if (Operators.ConditionalCompareObjectEqual(MyCol, MyCombo.Items[num5], TextCompare: false))
					{
						goto IL_0046;
					}
					goto IL_004f;
					IL_004f:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_0056;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					flag = false;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					num6 = checked(MyCombo.Items.Count - 1);
					num5 = 0;
					goto IL_0056;
					IL_0056:
					if (num5 <= num6)
					{
						goto IL_002a;
					}
					goto IL_005b;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				MyCombo.Items.Add(MyCol);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 197;
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

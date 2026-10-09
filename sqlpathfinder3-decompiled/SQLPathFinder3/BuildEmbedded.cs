using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class BuildEmbedded
{
	public static bool IsDupQuery(string MyKey)
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
					errsource = "BuildEmbedded - IsDupQuery";
					int num3 = 0;
					result = false;
					if (Operators.CompareString(MyKey, "", TextCompare: false) == 0)
					{
						result = true;
						goto end_IL_0001;
					}
					MyKey = Strings.UCase(MyKey);
					num3 = 0;
					do
					{
						if (Operators.CompareString(MyKey, Strings.UCase(Globals_Renamed.SubQueries[num3].ID), TextCompare: false) == 0)
						{
							result = true;
							break;
						}
						num3 = checked(num3 + 1);
					}
					while (num3 <= 99);
					goto end_IL_0001;
				}
				case 158:
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
				try0001_dispatch = 158;
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

	public static void ClearAllQueries()
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
					errsource = "Build_Embedded - ClearAllQueries";
					int num3 = 0;
					num3 = 0;
					do
					{
						Globals_Renamed.SubQueries[num3].ID = "";
						Globals_Renamed.SubQueries[num3].Data = "";
						Globals_Renamed.SubQueries[num3].OldNode = "";
						Globals_Renamed.SubQueries[num3].OldTag = "";
						Globals_Renamed.SubQueries[num3].OldText = "";
						num3 = checked(num3 + 1);
					}
					while (num3 <= 99);
					goto end_IL_0001;
				}
				case 177:
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
				try0001_dispatch = 177;
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

	public static int GetFreeQuerySlot()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int result = default(int);
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
					errsource = "Build_Embedded - GetFreeQuerySlot";
					int num3 = 0;
					result = -1;
					num3 = 0;
					do
					{
						if (Operators.CompareString(Globals_Renamed.SubQueries[num3].ID, "", TextCompare: false) == 0)
						{
							result = num3;
							break;
						}
						num3 = checked(num3 + 1);
					}
					while (num3 <= 99);
					goto end_IL_0001;
				}
				case 122:
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
				try0001_dispatch = 122;
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

	public static string AddQueryToSlot(string MyMode, string MyFile, string MyIDName)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text3 = default(string);
		int num3 = default(int);
		string result;
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
					string text = "Build_Embedded - AddQueryToSlot";
					string text2 = "";
					text3 = "";
					string text4 = "";
					num3 = -1;
					int num4 = 0;
					Information.Err().Clear();
					result = "";
					num3 = GetFreeQuerySlot();
					if (num3 != -1)
					{
						Globals_Renamed.SubQueries[num3].OldNode = "";
						Globals_Renamed.SubQueries[num3].OldTag = "";
						Globals_Renamed.SubQueries[num3].OldText = "";
						if (Operators.CompareString(MyIDName, "", TextCompare: false) == 0)
						{
							text4 = "PrePostQuery_" + Conversions.ToString(num3);
							MyIDName = Interaction.InputBox("What would you like to call your Embedded Query?", "Embedded Query name", text4);
							MyIDName = Strings.Replace(MyIDName, "\"", "'", 1, -1, CompareMethod.Text);
							MyIDName = Strings.Trim(General_Procedures.Replace_Special_Chars(MyIDName, "", 1));
							if (Operators.CompareString(MyIDName, "", TextCompare: false) == 0)
							{
								goto end_IL_0001;
							}
							text4 = MyIDName;
							num4 = -1;
							while (IsDupQuery(MyIDName) && num4 < 100)
							{
								num4 = checked(num4 + 1);
								MyIDName = text4 + "_" + Conversions.ToString(num4);
							}
							if (num4 > 100)
							{
								text3 = "All available Query Slots are Named";
								break;
							}
						}
						text2 = ((Operators.CompareString(MyMode, "F", TextCompare: false) != 0) ? MyFile : General_Procedures.OpenReadFileContents(MyFile));
						if (Operators.CompareString(text2, "", TextCompare: false) != 0)
						{
							Globals_Renamed.SubQueries[num3].Data = text2;
							Globals_Renamed.SubQueries[num3].ID = MyIDName;
							result = MyIDName;
							goto end_IL_0001;
						}
						text3 = ((Operators.CompareString(MyMode, "F", TextCompare: false) != 0) ? "An empty embedded query was loaded" : ("Could not locate intermediate File holding Query. I.e.," + MyFile));
					}
					else
					{
						text3 = "Unable to get a free slot to save the embedded query";
					}
					break;
				}
				case 756:
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
				Interaction.MsgBox("Error Saving Embedded Query. " + text3 + " (" + Information.Err().Description + ") . Contact support for assistance.", MsgBoxStyle.Exclamation, "Save Error");
				Information.Err().Clear();
				if (num3 != -1 && num3 >= 0 && num3 < 100)
				{
					Globals_Renamed.SubQueries[num3].ID = "";
					Globals_Renamed.SubQueries[num3].Data = "";
					Globals_Renamed.SubQueries[num3].OldNode = "";
					Globals_Renamed.SubQueries[num3].OldTag = "";
					Globals_Renamed.SubQueries[num3].OldText = "";
				}
				result = "";
				break;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 756;
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

	public static int GetQuerySlot(string MyKey)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int result = default(int);
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
					errsource = "Build_Embedded - GetQuerySlot";
					int num3 = 0;
					result = -1;
					if (Operators.CompareString(MyKey, "", TextCompare: false) == 0)
					{
						goto end_IL_0001;
					}
					MyKey = Strings.UCase(MyKey);
					num3 = 0;
					do
					{
						if (Operators.CompareString(MyKey, Strings.UCase(Globals_Renamed.SubQueries[num3].ID), TextCompare: false) == 0)
						{
							result = num3;
							break;
						}
						num3 = checked(num3 + 1);
					}
					while (num3 <= 99);
					goto end_IL_0001;
				}
				case 154:
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
				try0001_dispatch = 154;
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

	public static void ClearQuerySlot(string MyKey)
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
					errsource = "Build_Embedded - ClearQuerySlot";
					int num3 = -1;
					num3 = GetQuerySlot(MyKey);
					if (num3 != -1)
					{
						Globals_Renamed.SubQueries[num3].ID = "";
						Globals_Renamed.SubQueries[num3].Data = "";
						Globals_Renamed.SubQueries[num3].OldNode = "";
						Globals_Renamed.SubQueries[num3].OldTag = "";
						Globals_Renamed.SubQueries[num3].OldText = "";
					}
					goto end_IL_0001;
				}
				case 188:
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
				try0001_dispatch = 188;
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

	public static string UpdateQuerySlot(ref TreeNode ANode, string MyFile)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text3 = default(string);
		string text5;
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
					string text = "Build_Embedded - UpdateQuerySlot";
					string text2 = "";
					int num3 = -1;
					text3 = "";
					string text4 = "";
					Information.Err().Clear();
					text5 = "";
					text4 = Conversions.ToString(ANode.Tag);
					text4 = ((!((Strings.InStr(text4, "{") == 0) | (Strings.InStr(text4, "}") == 0))) ? General_Procedures.Get_Node_Value(text4) : "");
					num3 = GetQuerySlot(text4);
					if (num3 != -1)
					{
						if (Strings.InStrRev(MyFile, "\\") == 0)
						{
							MyFile = Globals_Renamed.MyPCDir + MyFile;
						}
						if (MyProject.Computer.FileSystem.FileExists(MyFile))
						{
							StreamReader streamReader = new StreamReader(MyFile);
							if (streamReader.Peek() != -1)
							{
								text2 = streamReader.ReadToEnd();
								Globals_Renamed.SubQueries[num3].Data = text2;
							}
							streamReader.Close();
							streamReader.Dispose();
							streamReader = null;
							text5 = Globals_Renamed.SubQueries[num3].ID;
							goto end_IL_0001;
						}
						text3 = "Could not locate intermediate File housing Query (" + MyFile + ")";
					}
					else
					{
						text3 = "No embedded source found";
					}
					break;
				}
				case 452:
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
				Interaction.MsgBox("Error Saving Embedded Query:" + ANode.Name + ". " + text3 + " (" + Information.Err().Description + ") . Contact support for assistance.", MsgBoxStyle.Exclamation, "Save Error");
				Information.Err().Clear();
				text5 = Conversions.ToString(-1);
				break;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 452;
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
		return text5;
	}

	public static int GetQuerySave(string MyKey, string MyFile)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text3 = default(string);
		int num5;
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
					string text = "Build_Embedded - GetQuerySave";
					string text2 = "";
					int num3 = -1;
					text3 = "";
					short num4 = 0;
					Information.Err().Clear();
					num5 = -1;
					num3 = GetQuerySlot(MyKey);
					if (num3 != -1)
					{
						if (Strings.InStrRev(MyFile, "\\") == 0)
						{
							MyFile = Globals_Renamed.MyPCDir + MyFile;
						}
						text2 = Globals_Renamed.SubQueries[num3].Data;
						if (Operators.CompareString(text2, "", TextCompare: false) == 0)
						{
							text3 = "No Source Code found";
							break;
						}
						if (General_Procedures.Save_SQL_Query(text2, MyFile) != 0)
						{
							num5 = num3;
							goto end_IL_0001;
						}
						text3 = "Unable to process Query";
					}
					else
					{
						text3 = "Unable to locate embedded code";
					}
					break;
				}
				case 302:
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
				Interaction.MsgBox("Error retrieving Embedded Query: " + MyKey + ". " + text3 + ". (" + Information.Err().Description + ") . Contact support for assistance.", MsgBoxStyle.Exclamation, "Get Error");
				Information.Err().Clear();
				num5 = -1;
				break;
				end_IL_0001_2:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 302;
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
}

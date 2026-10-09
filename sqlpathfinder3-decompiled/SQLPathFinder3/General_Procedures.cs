using System;
using System.Collections;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class General_Procedures
{
	[SpecialName]
	private static short _0024STATIC_0024FindWindowLike_0024038108810E_0024level;

	[SpecialName]
	private static short _0024STATIC_0024FindWindowLike_0024038108810E_0024iFound;

	public static bool HasAccess(string ltFullPath)
	{
		bool result;
		try
		{
			using StreamWriter streamWriter = new StreamWriter(ltFullPath);
			streamWriter.Close();
			result = true;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			result = false;
			ProjectData.ClearProjectError();
		}
		finally
		{
		}
		return result;
	}

	public static void Chk_Registry_cmd()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		bool flag = default(bool);
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
				case 225:
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
							goto IL_0034;
						case 5:
							goto IL_0038;
						case 6:
							goto IL_0063;
						case 7:
							goto IL_0067;
						case 8:
							goto IL_0070;
						case 9:
						case 10:
							goto IL_0086;
						case 11:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0063:
					num2 = 6;
					flag = true;
					goto IL_0067;
					IL_0067:
					num2 = 7;
					if (flag)
					{
						goto IL_0070;
					}
					goto IL_0086;
					IL_0086:
					ProjectData.ClearProjectError();
					num3 = 0;
					break;
					IL_0070:
					num2 = 8;
					Interaction.MsgBox("Use of your Command Window may require Administrative privilege which can cause issues in SQLPathFinder. Please select SQLPathFinder menu option Tools -> Check if cmd.exe Requires Elevated Privileges to remove this restriction.", MsgBoxStyle.Information, "Registry Check Required");
					goto IL_0086;
					IL_000b:
					num2 = 2;
					flag = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					if (MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", "C:\\Windows\\System32\\cmd.exe", null) != null)
					{
						goto IL_0034;
					}
					goto IL_0038;
					IL_0034:
					num2 = 4;
					flag = true;
					goto IL_0038;
					IL_0038:
					num2 = 5;
					if (!flag && MyProject.Computer.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", "C:\\Windows\\System32\\cmd.exe", null) != null)
					{
						goto IL_0063;
					}
					goto IL_0067;
					end_IL_0001_2:
					break;
				}
				num2 = 11;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 225;
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

	public static string TrimQuoted(string MyData)
	{
		MyData = Strings.Replace(MyData, "\"\"", "\"", 1, -1, CompareMethod.Text);
		if (Strings.Len(MyData) > 0 && Operators.CompareString(Strings.Mid(MyData, 1, 1), "\"", TextCompare: false) == 0)
		{
			MyData = Strings.Mid(MyData, 2);
		}
		if (Strings.Len(MyData) > 0 && Operators.CompareString(Strings.Mid(MyData, Strings.Len(MyData), 1), "\"", TextCompare: false) == 0)
		{
			MyData = Strings.Mid(MyData, 1, checked(Strings.Len(MyData) - 1));
		}
		return MyData;
	}

	public static string[] SplitQuoted(string sStr, char cComma = ',', char cQuote = '"')
	{
		if (sStr.IndexOf(cQuote) < 0)
		{
			return sStr.Split(cComma);
		}
		ArrayList arrayList = new ArrayList();
		int startIndex = 0;
		checked
		{
			while (true)
			{
				int num = sStr.IndexOf(cComma, startIndex);
				if (num < 0)
				{
					break;
				}
				int num2 = sStr.IndexOf(cQuote, startIndex);
				if (num2 < 0)
				{
					num2 = sStr.Length;
				}
				if (num < num2)
				{
					arrayList.Add(sStr.Substring(0, num));
					sStr = sStr.Substring(num + 1);
					startIndex = 0;
					continue;
				}
				num2 = sStr.IndexOf(cQuote, num2 + 1);
				if (num2 < 0)
				{
					num2 = sStr.Length - 1;
				}
				startIndex = num2 + 1;
			}
			arrayList.Add(sStr);
			object obj = arrayList.ToArray(typeof(string));
			return (string[])obj;
		}
	}

	public static string Get_Web(string MyURL, ref string MyScript)
	{
		string result = "";
		try
		{
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
							MyScript = streamReader.ReadToEnd();
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							result = "Error reading file: " + MyURL + " (" + ex2.Message + ")";
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
						result = "Error reading file: " + MyURL + " (" + ex4.Message + ")";
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
					result = "Error accessing file: " + MyURL + " (" + ex6.Message + ")";
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
				result = "Error opening file: " + MyURL + " (" + ex8.Message + ")";
				ProjectData.ClearProjectError();
			}
			finally
			{
			}
			httpWebRequest = null;
		}
		catch (Exception ex9)
		{
			ProjectData.SetProjectError(ex9);
			Exception ex10 = ex9;
			result = "Error accessing file: " + MyURL + " (" + ex10.Message + ")";
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string GetFileContents(string FullPath, ref string MyData)
	{
		string result = "";
		MyData = "";
		if (Operators.CompareString(FullPath, "", TextCompare: false) == 0 || MyProject.Computer.FileSystem.FileExists(FullPath))
		{
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(FullPath);
				MyData = streamReader.ReadToEnd();
				streamReader.Close();
				streamReader.Dispose();
				streamReader = null;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				result = ex2.Message;
				ProjectData.ClearProjectError();
			}
		}
		else
		{
			result = "File Not Found!";
		}
		return result;
	}

	public static bool Verify_Roles(string MyRole, ref string MyErr, string And_Or = "AND", string fDom = "")
	{
		bool result = false;
		MyErr = "";
		And_Or = Strings.Trim(Strings.UCase(And_Or));
		if (Operators.CompareString(And_Or, "OR", TextCompare: false) != 0 && Operators.CompareString(And_Or, "AND", TextCompare: false) != 0 && Operators.CompareString(And_Or, "LIKE-OR", TextCompare: false) != 0)
		{
			And_Or = "AND";
		}
		MyRole = Strings.UCase(Strings.Trim(MyRole));
		bool flag = false;
		bool flag2 = false;
		int num = 0;
		bool flag3 = false;
		string text = Strings.LCase(Strings.Trim(Interaction.Environ("USERNAME")));
		string text2 = Strings.LCase(Strings.Trim(Interaction.Environ("USERDOMAIN")));
		string text3 = "";
		checked
		{
			if (Operators.CompareString(MyRole, "", TextCompare: false) != 0 && Operators.CompareString(text, "", TextCompare: false) != 0 && Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				text2 += ".corp.intel.com";
				string[] array = Strings.Split(MyRole, ",");
				PrincipalContext principalContext = null;
				Principal principal = null;
				try
				{
					principalContext = new PrincipalContext(ContextType.Domain, text2);
					flag = true;
					principal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, text);
					if (!Information.IsNothing(principal))
					{
						flag2 = true;
						if (Operators.CompareString(And_Or, "AND", TextCompare: false) == 0)
						{
							int num2 = Information.UBound(array);
							for (num = 0; num <= num2; num++)
							{
								text3 = Strings.UCase(Strings.Trim(array[num]));
								flag3 = false;
								if (Operators.CompareString(text3, "", TextCompare: false) != 0)
								{
									foreach (Principal group in principal.GetGroups())
									{
										Type typeFromHandle = typeof(Strings);
										object[] array2 = new object[1];
										object instance = group;
										array2[0] = NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null);
										object[] array3 = array2;
										bool[] obj = new bool[1] { true };
										bool[] array4 = obj;
										object left = NewLateBinding.LateGet(null, typeFromHandle, "UCase", array2, null, null, obj);
										if (array4[0])
										{
											NewLateBinding.LateSetComplex(instance, null, "Name", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
										}
										if (!Operators.ConditionalCompareObjectEqual(left, text3, TextCompare: false))
										{
											Type typeFromHandle2 = typeof(Strings);
											object[] array5 = new object[1];
											instance = group;
											array5[0] = NewLateBinding.LateGet(instance, null, "SamAccountName", new object[0], null, null, null);
											array3 = array5;
											bool[] obj2 = new bool[1] { true };
											array4 = obj2;
											left = NewLateBinding.LateGet(null, typeFromHandle2, "UCase", array5, null, null, obj2);
											if (array4[0])
											{
												NewLateBinding.LateSetComplex(instance, null, "SamAccountName", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
											}
											if (!Operators.ConditionalCompareObjectEqual(left, text3, TextCompare: false))
											{
												continue;
											}
										}
										flag3 = true;
										break;
									}
									if (!flag3)
									{
										break;
									}
								}
								else
								{
									flag3 = true;
								}
							}
						}
						else if (Operators.CompareString(And_Or, "LIKE-OR", TextCompare: false) == 0)
						{
							int num3 = Information.UBound(array);
							for (num = 0; num <= num3; num++)
							{
								text3 = Strings.UCase(Strings.Trim(array[num]));
								flag3 = false;
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									continue;
								}
								foreach (Principal group2 in principal.GetGroups())
								{
									Type typeFromHandle3 = typeof(Strings);
									object[] array6 = new object[1];
									object instance = group2;
									array6[0] = NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null);
									object[] array3 = array6;
									bool[] array4;
									object left = NewLateBinding.LateGet(null, typeFromHandle3, "UCase", array6, null, null, array4 = new bool[1] { true });
									if (array4[0])
									{
										NewLateBinding.LateSetComplex(instance, null, "Name", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
									}
									int num4;
									if (!Conversions.ToBoolean(LikeOperator.LikeObject(left, text3, CompareMethod.Binary)))
									{
										Type typeFromHandle4 = typeof(Strings);
										object[] array7 = new object[1];
										instance = group2;
										array7[0] = NewLateBinding.LateGet(instance, null, "SamAccountName", new object[0], null, null, null);
										array3 = array7;
										bool[] obj3 = new bool[1] { true };
										array4 = obj3;
										left = NewLateBinding.LateGet(null, typeFromHandle4, "UCase", array7, null, null, obj3);
										if (array4[0])
										{
											NewLateBinding.LateSetComplex(instance, null, "SamAccountName", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
										}
										num4 = (Conversions.ToBoolean(LikeOperator.LikeObject(left, text3, CompareMethod.Binary)) ? 1 : 0);
									}
									else
									{
										num4 = 1;
									}
									if (Conversions.ToBoolean(unchecked((byte)num4) != 0))
									{
										flag3 = true;
										break;
									}
								}
								if (flag3)
								{
									break;
								}
							}
						}
						else
						{
							int num5 = Information.UBound(array);
							for (num = 0; num <= num5; num++)
							{
								text3 = Strings.UCase(Strings.Trim(array[num]));
								flag3 = false;
								if (Operators.CompareString(text3, "", TextCompare: false) == 0)
								{
									continue;
								}
								foreach (Principal group3 in principal.GetGroups())
								{
									Type typeFromHandle5 = typeof(Strings);
									object[] array8 = new object[1];
									object instance = group3;
									array8[0] = NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null);
									object[] array3 = array8;
									bool[] obj4 = new bool[1] { true };
									bool[] array4 = obj4;
									object left = NewLateBinding.LateGet(null, typeFromHandle5, "UCase", array8, null, null, obj4);
									if (array4[0])
									{
										NewLateBinding.LateSetComplex(instance, null, "Name", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
									}
									if (!Operators.ConditionalCompareObjectEqual(left, text3, TextCompare: false))
									{
										Type typeFromHandle6 = typeof(Strings);
										object[] array9 = new object[1];
										instance = group3;
										array9[0] = NewLateBinding.LateGet(instance, null, "SamAccountName", new object[0], null, null, null);
										array3 = array9;
										bool[] obj5 = new bool[1] { true };
										array4 = obj5;
										left = NewLateBinding.LateGet(null, typeFromHandle6, "UCase", array9, null, null, obj5);
										if (array4[0])
										{
											NewLateBinding.LateSetComplex(instance, null, "SamAccountName", new object[1] { array3[0] }, null, null, OptimisticSet: true, RValueBase: false);
										}
										if (!Operators.ConditionalCompareObjectEqual(left, text3, TextCompare: false))
										{
											continue;
										}
									}
									flag3 = true;
									break;
								}
								if (flag3)
								{
									break;
								}
							}
						}
						if (flag3)
						{
							result = true;
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					MyErr = ex2.Message;
					ProjectData.ClearProjectError();
				}
				finally
				{
					if (flag2)
					{
						principal.Dispose();
						principal = null;
					}
					if (flag)
					{
						principalContext.Dispose();
						principalContext = null;
					}
					array = null;
				}
			}
			return result;
		}
	}

	public static string Strip_Extension(string MyFile)
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
				case 208:
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
							goto IL_0029;
						case 6:
							goto IL_0036;
						case 7:
						case 8:
							goto IL_004b;
						case 9:
							goto IL_005c;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
						case 12:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0036:
					num2 = 6;
					text = Strings.Trim(Strings.Mid(MyFile, 1, checked(num5 - 1)));
					goto IL_004b;
					IL_004b:
					num2 = 8;
					num5 = Strings.InStrRev(text, "\\");
					goto IL_005c;
					IL_0029:
					num2 = 5;
					if (num5 > 1)
					{
						goto IL_0036;
					}
					goto IL_004b;
					IL_005c:
					num2 = 9;
					if (num5 != 0 && num5 < Strings.Len(text))
					{
						break;
					}
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					text = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					num5 = Strings.InStrRev(MyFile, ".");
					goto IL_0029;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				text = Strings.Trim(Strings.Mid(text, checked(num5 + 1)));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 208;
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

	public static short Save_SQL_Query(string InStr, string MyFile, int MyOMode = 0)
	{
		short result = 1;
		FileMode mode = ((MyOMode != 1) ? FileMode.Create : FileMode.Append);
		try
		{
			StreamWriter streamWriter = new StreamWriter(new FileStream(MyFile, mode, FileAccess.ReadWrite, FileShare.ReadWrite));
			try
			{
				streamWriter.WriteLine(InStr);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox("Error: \"" + ex2.Message + "\"  occurred while writing to:" + MyFile + ".", MsgBoxStyle.Exclamation, "Error Writing to File");
				result = 0;
				ProjectData.ClearProjectError();
			}
			finally
			{
				streamWriter.Close();
				streamWriter.Dispose();
				streamWriter = null;
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Interaction.MsgBox("Error: \"" + ex4.Message + "\"  occurred while opening:" + MyFile + ".", MsgBoxStyle.Exclamation, "Error Opening File");
			result = 0;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Rep_Prob_Quote(string MyStr)
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
				case 225:
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
							goto IL_002f;
						case 5:
							goto IL_004f;
						case 6:
							goto IL_006f;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002f:
					num2 = 4;
					text = Strings.Replace(text, Conversions.ToString(Strings.Chr(146)), "'", 1, -1, CompareMethod.Text);
					goto IL_004f;
					IL_004f:
					num2 = 5;
					text = Strings.Replace(text, Conversions.ToString(Strings.Chr(147)), "\"", 1, -1, CompareMethod.Text);
					goto IL_006f;
					IL_000f:
					num2 = 3;
					text = Strings.Replace(text, Conversions.ToString(Strings.Chr(145)), "'", 1, -1, CompareMethod.Text);
					goto IL_002f;
					IL_006f:
					num2 = 6;
					text = Strings.Replace(text, Conversions.ToString(Strings.Chr(148)), "\"", 1, -1, CompareMethod.Text);
					break;
					IL_000b:
					num2 = 2;
					text = MyStr;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				text = Strings.Replace(text, Conversions.ToString(Strings.Chr(149)), "-", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 225;
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

	public static bool IsDateDT(string MyDataType, int MyMode = 0)
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
				case 1008:
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
							goto IL_001f;
						case 6:
							goto IL_0041;
						case 7:
							goto IL_0120;
						case 9:
							goto IL_012a;
						case 10:
							goto IL_01f9;
						case 12:
							goto IL_0204;
						case 13:
							goto IL_028f;
						case 15:
							goto IL_029a;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 11:
						case 14:
						case 17:
						case 18:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01f9:
					num2 = 10;
					result = true;
					goto end_IL_0001_3;
					IL_0041:
					num2 = 6;
					switch (MyDataType)
					{
					default:
						if (Operators.CompareString(MyDataType, "w", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "d":
					case "t":
					case "l":
					case "s":
					case "g":
					case "h":
					case "p":
					case "j":
					case "k":
					case "u":
					case "m":
					case "o":
					case "v":
						break;
					}
					goto IL_0120;
					IL_012a:
					num2 = 9;
					switch (MyDataType)
					{
					default:
						if (Operators.CompareString(MyDataType, "w", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "t":
					case "g":
					case "h":
					case "u":
					case "m":
					case "o":
					case "l":
					case "p":
					case "j":
					case "k":
					case "v":
					case "s":
						break;
					}
					goto IL_01f9;
					IL_0120:
					num2 = 7;
					result = true;
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					MyDataType = Strings.Trim(Strings.LCase(MyDataType));
					goto IL_001f;
					IL_001f:
					num2 = 4;
					switch (MyMode)
					{
					case 0:
						break;
					case 1:
						goto IL_012a;
					case 2:
						goto IL_0204;
					case 3:
						goto IL_029a;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0041;
					IL_029a:
					num2 = 15;
					switch (MyDataType)
					{
					default:
						if (Operators.CompareString(MyDataType, "s2", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "d":
					case "v":
					case "w":
					case "t":
					case "g":
					case "h":
					case "u":
					case "m":
					case "p":
					case "j":
					case "k":
					case "o":
					case "l":
					case "s":
						break;
					}
					break;
					IL_0204:
					num2 = 12;
					switch (MyDataType)
					{
					default:
						if (Operators.CompareString(MyDataType, "w", TextCompare: false) != 0)
						{
							goto end_IL_0001_3;
						}
						break;
					case "g":
					case "u":
					case "s":
					case "m":
					case "p":
					case "j":
					case "k":
					case "o":
						break;
					}
					goto IL_028f;
					IL_028f:
					num2 = 13;
					result = true;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				result = true;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1008;
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

	public static string Check_VA()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
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
				case 259:
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
							goto IL_002b;
						case 6:
							goto IL_006b;
						case 7:
							goto IL_0087;
						case 9:
							goto IL_0092;
						case 10:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 5:
						case 8:
						case 11:
						case 12:
						case 13:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0087:
					num2 = 7;
					result = "C:\\Program Files\\MBTools;C:\\Program Files\\MBTools\\sys;";
					goto end_IL_0001_3;
					IL_0092:
					num2 = 9;
					if (!MyProject.Computer.FileSystem.FileExists("C:\\MBTools\\va.exe"))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_002b:
					num2 = 4;
					if (Operators.CompareString(Globals_Renamed.gUseMinPath, "N", TextCompare: false) == 0 && Operators.CompareString(text, "", TextCompare: false) != 0 && Strings.InStr(text, "\\mbtools\\") != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_006b;
					IL_006b:
					num2 = 6;
					if (MyProject.Computer.FileSystem.FileExists("C:\\Program Files\\MBTools\\va.exe"))
					{
						goto IL_0087;
					}
					goto IL_0092;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					text = Strings.Trim(Strings.LCase(Environment.ExpandEnvironmentVariables("%path%")));
					goto IL_002b;
					end_IL_0001_2:
					break;
				}
				num2 = 10;
				result = "C:\\MBTools;C:\\MBTools\\sys;";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 259;
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

	public static string Set_ORCL_Path()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				string gUseMinPath;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 484:
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
							goto IL_002f;
						case 7:
							goto IL_0064;
						case 8:
						case 9:
							goto IL_0070;
						case 11:
							goto IL_00a1;
						case 12:
							goto IL_00ab;
						case 13:
							goto IL_00bc;
						case 14:
							goto IL_00d5;
						case 16:
							goto IL_00fe;
						case 17:
							goto IL_0117;
						case 19:
							goto IL_012b;
						case 22:
							goto IL_0138;
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 10:
						case 15:
						case 18:
						case 20:
						case 21:
						case 24:
						case 25:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00d5:
					num2 = 14;
					result = text + Strings.Trim(MyProject.Application.Info.DirectoryPath) + text2 + ";%PATH%";
					goto end_IL_0001_3;
					IL_00fe:
					num2 = 16;
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						goto IL_0117;
					}
					goto IL_012b;
					IL_00bc:
					num2 = 13;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_00d5;
					}
					goto IL_00fe;
					IL_0117:
					num2 = 17;
					result = text + "%PATH%";
					goto end_IL_0001_3;
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
					text2 = "";
					goto IL_0026;
					IL_0026:
					num2 = 5;
					text2 = "\\oracle\\instantclient_19_17";
					goto IL_002f;
					IL_002f:
					num2 = 6;
					if (!MyProject.Computer.FileSystem.FileExists(MyProject.Application.Info.DirectoryPath + text2 + "\\sqlplus.exe"))
					{
						goto IL_0064;
					}
					goto IL_0070;
					IL_0064:
					num2 = 7;
					text2 = "";
					goto IL_0070;
					IL_0070:
					num2 = 9;
					gUseMinPath = Globals_Renamed.gUseMinPath;
					if (Operators.CompareString(gUseMinPath, "N", TextCompare: false) == 0)
					{
						goto IL_00a1;
					}
					if (Operators.CompareString(gUseMinPath, "Y", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0138;
					IL_012b:
					num2 = 19;
					result = "";
					goto end_IL_0001_3;
					IL_0138:
					num2 = 22;
					text3 = Check_VA();
					break;
					IL_00a1:
					num2 = 11;
					text3 = Check_VA();
					goto IL_00ab;
					IL_00ab:
					num2 = 12;
					text = "&&SET PATH=" + text3;
					goto IL_00bc;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				result = "&&SET PATH=c:\\windows;c:\\windows\\system32;" + text3 + Strings.Trim(MyProject.Application.Info.DirectoryPath) + text2;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 484;
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

	public static bool IsPosInteger(string MyStr)
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
				case 111:
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
					if (Versioned.IsNumeric(MyStr) && Conversion.Val(MyStr) == (double)Conversions.ToLong(MyStr) && Conversion.Val(MyStr) >= 0.0)
					{
						break;
					}
					goto end_IL_0001_3;
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
				try0001_dispatch = 111;
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

	public static string GetFirstLine(string CurrentFile, bool MakeHdrsLowerCase)
	{
		string text = "";
		CurrentFile = BuildForm.Replace_Globals(CurrentFile, 1);
		if (Strings.InStrRev(CurrentFile, "\\") == 0)
		{
			CurrentFile = Globals_Renamed.MyPCDir + CurrentFile;
		}
		if (File.Exists(CurrentFile))
		{
			try
			{
				StreamReader streamReader = new StreamReader(CurrentFile);
				try
				{
					if (streamReader.Peek() != -1)
					{
						text = streamReader.ReadLine();
					}
					if (Operators.CompareString(text, "", TextCompare: false) != 0 && MakeHdrsLowerCase)
					{
						text = Strings.LCase(text);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox("Error loading file: " + CurrentFile + ". (" + ex2.Message + "). Please try another file.", MsgBoxStyle.Exclamation, "File Error");
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
				Interaction.MsgBox("Error accessing file: " + CurrentFile + ". (" + ex4.Message + ").", MsgBoxStyle.Exclamation, "File Error");
				ProjectData.ClearProjectError();
			}
		}
		return text;
	}

	public static short GetCSVHeaders(string CurrentFile, ref string[] lCSVCols, bool MakeHdrsLowerCase = true, bool R_Replace = false, bool AllowAllCols = false)
	{
		short result = 0;
		int num = 0;
		string text = "";
		object obj = null;
		CurrentFile = BuildForm.Replace_Globals(CurrentFile, 1);
		text = BuildForm.GetFileDLM(CurrentFile);
		string firstLine = GetFirstLine(CurrentFile, MakeHdrsLowerCase);
		checked
		{
			if (Operators.CompareString(firstLine, "", TextCompare: false) != 0)
			{
				StringReader stringReader = new StringReader(firstLine);
				obj = new TextFieldParser(stringReader)
				{
					TextFieldType = FieldType.Delimited,
					HasFieldsEnclosedInQuotes = true,
					TrimWhiteSpace = true
				};
				object instance = obj;
				object[] obj2 = new object[1] { text };
				object[] array = obj2;
				bool[] obj3 = new bool[1] { true };
				bool[] array2 = obj3;
				NewLateBinding.LateCall(instance, null, "SetDelimiters", obj2, null, null, obj3, IgnoreReturn: true);
				if (array2[0])
				{
					text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
				}
				if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(obj, null, "EndOfData", new object[0], null, null, null))))
				{
					try
					{
						lCSVCols = (string[])NewLateBinding.LateGet(obj, null, "ReadFields", new object[0], null, null, null);
						if (R_Replace)
						{
							int num2 = Information.UBound(lCSVCols);
							for (num = 0; num <= num2; num++)
							{
								if (Operators.CompareString(Strings.Mid(lCSVCols[num], 1, 1), "_", TextCompare: false) == 0)
								{
									lCSVCols[num] = "x" + lCSVCols[num];
								}
								else if (Versioned.IsNumeric(Strings.Mid(lCSVCols[num], 1, 1)))
								{
									lCSVCols[num] = "X" + lCSVCols[num];
								}
								lCSVCols[num] = Regex.Replace(lCSVCols[num], "[^\\w\\.]", ".");
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Interaction.MsgBox("Error Reading First Row of File: " + CurrentFile + ". (" + ex2.Message + ").", MsgBoxStyle.Exclamation, "File Error");
						result = 1;
						ProjectData.ClearProjectError();
					}
					finally
					{
						NewLateBinding.LateCall(obj, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
						NewLateBinding.LateCall(obj, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
						obj = null;
					}
				}
				else
				{
					result = 1;
				}
				stringReader.Close();
				if (!AllowAllCols)
				{
					if (Information.UBound(lCSVCols) + 1 > 1999)
					{
						Interaction.MsgBox("The CSV processor only supports files with fewer than " + Conversions.ToString(1999) + " columns. (" + CurrentFile + ")", MsgBoxStyle.Exclamation, "Too many Columns");
						result = 1;
						lCSVCols = null;
					}
					else
					{
						BuildForm.Make_Headers_Unique2(ref lCSVCols, R_Replace);
					}
				}
			}
			else
			{
				result = 1;
			}
			return result;
		}
	}

	public static string Get_JMP_R_DLM(string MyFile, string MyScriptType)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		string left = default(string);
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
				case 374:
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
							goto IL_002e;
						case 4:
						case 6:
						case 7:
							goto IL_0037;
						case 8:
							goto IL_0040;
						case 9:
							goto IL_004a;
						case 10:
							goto IL_006d;
						case 11:
							goto IL_0085;
						case 12:
							goto IL_009e;
						case 13:
							goto IL_00b6;
						case 15:
							goto IL_00c3;
						case 18:
							goto IL_00d1;
						case 19:
							goto IL_00e9;
						case 21:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 16:
						case 17:
						case 20:
						case 22:
						case 23:
						case 24:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00b6:
					num2 = 13;
					result = "Tab";
					goto end_IL_0001_3;
					IL_00c3:
					num2 = 15;
					result = "\\t";
					goto end_IL_0001_3;
					IL_009e:
					num2 = 12;
					if (Operators.CompareString(MyScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_00b6;
					}
					goto IL_00c3;
					IL_00d1:
					num2 = 18;
					if (Operators.CompareString(MyScriptType, "JMP", TextCompare: false) != 0)
					{
						break;
					}
					goto IL_00e9;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(MyScriptType, "JMP", TextCompare: false) == 0)
					{
						goto IL_0022;
					}
					goto IL_002e;
					IL_0022:
					num2 = 3;
					result = "Tab, Comma";
					goto IL_0037;
					IL_002e:
					num2 = 5;
					result = ",";
					goto IL_0037;
					IL_0037:
					num2 = 7;
					left = "";
					goto IL_0040;
					IL_0040:
					num2 = 8;
					MyFile = Strings.Trim(MyFile);
					goto IL_004a;
					IL_004a:
					num2 = 9;
					if (Operators.CompareString(MyFile, "", TextCompare: false) != 0 && MyFile.Length > 4)
					{
						goto IL_006d;
					}
					goto IL_0085;
					IL_00e9:
					num2 = 19;
					result = "Comma";
					goto end_IL_0001_3;
					IL_006d:
					num2 = 10;
					left = Strings.UCase(MyFile.Substring(checked(MyFile.Length - 4)));
					goto IL_0085;
					IL_0085:
					num2 = 11;
					if (Operators.CompareString(left, ".TAB", TextCompare: false) == 0)
					{
						goto IL_009e;
					}
					goto IL_00d1;
					end_IL_0001_2:
					break;
				}
				num2 = 21;
				result = ",";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 374;
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

	public static short SummPlusLevel(string InList)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		short result = default(short);
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
				case 258:
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
							goto IL_0018;
						case 5:
							goto IL_0022;
						case 6:
							goto IL_004b;
						case 7:
							goto IL_0057;
						case 8:
							goto IL_0086;
						case 11:
							goto IL_0096;
						case 12:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
						case 10:
						case 13:
						case 14:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004b:
					num2 = 6;
					text = Strings.Mid(InList, 3, 1);
					goto IL_0057;
					IL_0057:
					num2 = 7;
					if (!((Operators.CompareString(text, "1", TextCompare: false) >= 0) & (Operators.CompareString(text, "9", TextCompare: false) <= 0)))
					{
						goto end_IL_0001_3;
					}
					goto IL_0086;
					IL_0096:
					num2 = 11;
					if (Operators.CompareString(InList, "s+10", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0086:
					num2 = 8;
					result = checked((short)Conversions.ToInteger(text));
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					text = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					result = 0;
					goto IL_0018;
					IL_0018:
					num2 = 4;
					InList = Strings.LCase(InList);
					goto IL_0022;
					IL_0022:
					num2 = 5;
					if (Operators.CompareString(Strings.Mid(InList, 1, 2), "s+", TextCompare: false) == 0 && InList.Length == 3)
					{
						goto IL_004b;
					}
					goto IL_0096;
					end_IL_0001_2:
					break;
				}
				num2 = 12;
				result = 10;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 258;
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

	public static bool ChkSummPlus(string InList, string CompareOpr, string InListCompare)
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
							goto IL_0023;
						case 6:
							goto IL_003a;
						case 7:
							goto IL_0043;
						case 8:
							goto IL_005a;
						case 9:
							goto IL_0063;
						case 10:
							goto IL_0073;
						case 11:
							goto IL_0089;
						case 13:
							goto IL_00ce;
						case 14:
							goto IL_00e2;
						case 16:
							goto IL_00ea;
						case 17:
							goto IL_00fe;
						case 19:
							goto IL_0106;
						case 20:
							goto IL_011a;
						case 22:
							goto IL_0122;
						case 23:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 12:
						case 15:
						case 18:
						case 21:
						case 24:
						case 25:
						case 26:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00ea:
					num2 = 16;
					if (Operators.CompareString(InList, InListCompare, TextCompare: false) >= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00fe;
					IL_00fe:
					num2 = 17;
					result = true;
					goto end_IL_0001_3;
					IL_011a:
					num2 = 20;
					result = true;
					goto end_IL_0001_3;
					IL_00ce:
					num2 = 13;
					if (Operators.CompareString(InList, InListCompare, TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00e2;
					IL_000b:
					num2 = 2;
					result = false;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					InListCompare = Strings.LCase(InListCompare);
					goto IL_0019;
					IL_0019:
					num2 = 4;
					InList = Strings.LCase(InList);
					goto IL_0023;
					IL_0023:
					num2 = 5;
					if (Operators.CompareString(InList, "s+10", TextCompare: false) == 0)
					{
						goto IL_003a;
					}
					goto IL_0043;
					IL_003a:
					num2 = 6;
					InList = "s+a";
					goto IL_0043;
					IL_0043:
					num2 = 7;
					if (Operators.CompareString(InListCompare, "s+10", TextCompare: false) == 0)
					{
						goto IL_005a;
					}
					goto IL_0063;
					IL_005a:
					num2 = 8;
					InList = "s+a";
					goto IL_0063;
					IL_0063:
					num2 = 9;
					CompareOpr = Strings.Trim(Strings.LCase(CompareOpr));
					goto IL_0073;
					IL_0073:
					num2 = 10;
					if (SummPlusLevel(InList) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0089;
					IL_0089:
					num2 = 11;
					switch (CompareOpr)
					{
					case "eq":
						break;
					case "lt":
						goto IL_00ea;
					case "gt":
						goto IL_0106;
					case "le":
						goto IL_0122;
					default:
						goto end_IL_0001_3;
					}
					goto IL_00ce;
					IL_0122:
					num2 = 22;
					if (Operators.CompareString(InList, InListCompare, TextCompare: false) >= 0 && Operators.CompareString(InList, InListCompare, TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_00e2:
					num2 = 14;
					result = true;
					goto end_IL_0001_3;
					IL_0106:
					num2 = 19;
					if (Operators.CompareString(InList, InListCompare, TextCompare: false) <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_011a;
					end_IL_0001_2:
					break;
				}
				num2 = 23;
				result = true;
				break;
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
		return result;
	}

	public static void Load_Combo_File(string MyFile, ref ComboBox cmbQueue)
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
						errsource = "General_Procedures - Load_Combo_File";
						string text = "";
						short num3 = 0;
						string[] array;
						if (File.Exists(MyFile))
						{
							text = OpenReadFileContents(MyFile);
							array = Strings.Split(text, "\r\n");
							short num4 = (short)Information.UBound(array);
							for (num3 = 0; num3 <= num4; num3 = (short)unchecked(num3 + 1))
							{
								if (Operators.CompareString(Strings.Mid(array[num3], 1, 1), "!", TextCompare: false) != 0)
								{
									cmbQueue.Items.Add(array[num3]);
								}
							}
						}
						array = null;
						goto end_IL_0001;
					}
					case 186:
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
				try0001_dispatch = 186;
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

	public static string Get_Chart_Script_Type(string MyIn, int MyMode)
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
				checked
				{
					int num6;
					int num7;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 541:
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
								goto IL_0022;
							case 7:
								goto IL_0032;
							case 9:
								goto IL_004c;
							case 10:
								goto IL_0061;
							case 11:
								goto IL_006c;
							case 12:
								goto IL_007e;
							case 13:
								goto IL_008c;
							case 15:
								goto IL_00a3;
							case 16:
								goto IL_00b8;
							case 19:
								goto IL_00c8;
							case 20:
								goto IL_00d9;
							case 21:
								goto IL_00e7;
							case 22:
								goto IL_0100;
							case 23:
								goto IL_0112;
							case 24:
								goto IL_0120;
							case 26:
								goto IL_0137;
							case 27:
								goto IL_0151;
							case 29:
								goto IL_015d;
							case 30:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 8:
							case 14:
							case 17:
							case 18:
							case 25:
							case 28:
							case 31:
							case 32:
							case 33:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_007e:
						num2 = 12;
						if (num5 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_008c;
						IL_008c:
						num2 = 13;
						text = Strings.Trim(Strings.Mid(text, 1, num5 - 1));
						goto end_IL_0001_3;
						IL_006c:
						num2 = 11;
						num5 = Strings.InStrRev(text, ":");
						goto IL_007e;
						IL_00a3:
						num2 = 15;
						if (!LikeOperator.LikeString(MyIn, "HTML-R-*", CompareMethod.Binary))
						{
							goto end_IL_0001_3;
						}
						goto IL_00b8;
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
						text = "JMP";
						goto IL_0022;
						IL_0022:
						num2 = 6;
						MyIn = Strings.Trim(Strings.UCase(MyIn));
						goto IL_0032;
						IL_0032:
						num2 = 7;
						if (MyMode == 1)
						{
							goto IL_004c;
						}
						if (MyMode != 2)
						{
							goto end_IL_0001_3;
						}
						goto IL_00c8;
						IL_00b8:
						num2 = 16;
						text = "R";
						goto end_IL_0001_3;
						IL_00c8:
						num2 = 19;
						num5 = Strings.InStr(MyIn, "CHART-IN-");
						goto IL_00d9;
						IL_00d9:
						num2 = 20;
						if (num5 != 0)
						{
							goto IL_00e7;
						}
						goto IL_0137;
						IL_00e7:
						num2 = 21;
						text = Strings.UCase(Strings.Trim(Strings.Mid(MyIn, num5 + 9)));
						goto IL_0100;
						IL_0100:
						num2 = 22;
						num5 = Strings.InStrRev(text, ":");
						goto IL_0112;
						IL_0112:
						num2 = 23;
						if (num5 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0120;
						IL_0120:
						num2 = 24;
						text = Strings.Trim(Strings.Mid(text, 1, num5 - 1));
						goto end_IL_0001_3;
						IL_0137:
						num2 = 26;
						if (LikeOperator.LikeString(Strings.UCase(MyIn), "*HTML-*", CompareMethod.Binary))
						{
							goto IL_0151;
						}
						goto IL_015d;
						IL_0151:
						num2 = 27;
						text = "HTML";
						goto end_IL_0001_3;
						IL_015d:
						num2 = 29;
						if (!LikeOperator.LikeString(Strings.UCase(MyIn), "R-*", CompareMethod.Binary))
						{
							goto end_IL_0001_3;
						}
						break;
						IL_004c:
						num2 = 9;
						if (LikeOperator.LikeString(MyIn, "SCRIPT:*", CompareMethod.Binary))
						{
							goto IL_0061;
						}
						goto IL_00a3;
						IL_0061:
						num2 = 10;
						text = Strings.Mid(MyIn, 8);
						goto IL_006c;
						end_IL_0001_2:
						break;
					}
					num2 = 30;
					text = "R";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 541;
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

	public static bool Check_For_Char(string MyStr, string MyChars)
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
				case 143:
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
							goto IL_0023;
						case 6:
							goto end_IL_0001_2;
						case 8:
						case 9:
							goto IL_0046;
						default:
							goto end_IL_0001;
						case 7:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_004f:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0023;
					IL_0023:
					num2 = 5;
					if (Strings.InStr(MyStr, Strings.Mid(MyChars, num5, 1)) != 0)
					{
						break;
					}
					goto IL_0046;
					IL_0014:
					num2 = 4;
					num6 = Strings.Len(MyChars);
					num5 = 1;
					goto IL_004f;
					IL_0046:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_004f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					result = false;
					goto IL_0014;
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
				try0001_dispatch = 143;
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

	public static string Quote_CRLF_Replace(string MyMode, string MyInData)
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
					goto IL_000c;
				case 945:
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
							goto IL_0169;
						case 5:
							goto IL_017f;
						case 6:
							goto IL_0195;
						case 8:
							goto IL_01b1;
						case 9:
							goto IL_01c7;
						case 10:
							goto IL_01de;
						case 12:
							goto IL_01fb;
						case 13:
							goto IL_0212;
						case 14:
							goto IL_0229;
						case 15:
							goto IL_0240;
						case 17:
							goto IL_025d;
						case 18:
							goto IL_0274;
						case 19:
							goto IL_028b;
						case 20:
							goto IL_02a2;
						case 22:
							goto IL_02bc;
						case 24:
							goto IL_02d6;
						case 26:
							goto IL_02f0;
						case 28:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 7:
						case 11:
						case 16:
						case 21:
						case 23:
						case 25:
						case 27:
						case 29:
						case 30:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0169:
					num2 = 4;
					text = Strings.Replace(MyInData, "\"", "<~>", 1, -1, CompareMethod.Text);
					goto IL_017f;
					IL_017f:
					num2 = 5;
					text = Strings.Replace(text, "\r", "<{>", 1, -1, CompareMethod.Text);
					goto IL_0195;
					IL_01de:
					num2 = 10;
					text = Strings.Replace(text, "<}>", "\n", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_0195:
					num2 = 6;
					text = Strings.Replace(text, "\n", "<}>", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_000c:
					num2 = 2;
					switch (Strings.UCase(MyMode))
					{
					case "E":
						break;
					case "D":
						goto IL_01b1;
					case "E2":
						goto IL_01fb;
					case "D2":
						goto IL_025d;
					case "EQ":
						goto IL_02bc;
					case "DQ":
						goto IL_02d6;
					case "SD":
						goto IL_02f0;
					case "SE":
						goto end_IL_0001_2;
					default:
						goto end_IL_0001_3;
					}
					goto IL_0169;
					IL_02f0:
					num2 = 26;
					text = Strings.Replace(MyInData, "<-s-c->", ";", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_02d6:
					num2 = 24;
					text = Strings.Replace(MyInData, "<~>", "\"", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_02bc:
					num2 = 22;
					text = Strings.Replace(MyInData, "\"", "<~>", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_025d:
					num2 = 17;
					text = Strings.Replace(MyInData, "<~~~>", "\"", 1, -1, CompareMethod.Text);
					goto IL_0274;
					IL_0274:
					num2 = 18;
					text = Strings.Replace(text, "<{{{>", "\r", 1, -1, CompareMethod.Text);
					goto IL_028b;
					IL_028b:
					num2 = 19;
					text = Strings.Replace(text, "<}}}>", "\n", 1, -1, CompareMethod.Text);
					goto IL_02a2;
					IL_02a2:
					num2 = 20;
					text = Strings.Replace(text, "<-s---c->", ";", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_01fb:
					num2 = 12;
					text = Strings.Replace(MyInData, "\"", "<~~~>", 1, -1, CompareMethod.Text);
					goto IL_0212;
					IL_0212:
					num2 = 13;
					text = Strings.Replace(text, "\r", "<{{{>", 1, -1, CompareMethod.Text);
					goto IL_0229;
					IL_0229:
					num2 = 14;
					text = Strings.Replace(text, "\n", "<}}}>", 1, -1, CompareMethod.Text);
					goto IL_0240;
					IL_0240:
					num2 = 15;
					text = Strings.Replace(text, ";", "<-s---c->", 1, -1, CompareMethod.Text);
					goto end_IL_0001_3;
					IL_01b1:
					num2 = 8;
					text = Strings.Replace(MyInData, "<~>", "\"", 1, -1, CompareMethod.Text);
					goto IL_01c7;
					IL_01c7:
					num2 = 9;
					text = Strings.Replace(text, "<{>", "\r", 1, -1, CompareMethod.Text);
					goto IL_01de;
					end_IL_0001_2:
					break;
				}
				num2 = 28;
				text = Strings.Replace(MyInData, ";", "<-s-c->", 1, -1, CompareMethod.Text);
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 945;
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

	public static string Remove_Spaces_Bt_Quotes(string MyArg)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		int num5 = default(int);
		int num6 = default(int);
		string text2 = default(string);
		int num7 = default(int);
		int num8 = default(int);
		string left = default(string);
		string result = default(string);
		int num9 = default(int);
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
				case 714:
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
							goto IL_0031;
						case 9:
							goto IL_003a;
						case 10:
							goto IL_003f;
						case 12:
						case 13:
							goto IL_0073;
						case 14:
							goto IL_007e;
						case 15:
							goto IL_008d;
						case 16:
							goto IL_009b;
						case 17:
							goto IL_00b4;
						case 18:
							goto IL_00be;
						case 19:
							goto IL_00d2;
						case 21:
							goto IL_00e6;
						case 20:
						case 22:
						case 23:
							goto IL_00f1;
						case 24:
							goto IL_0121;
						case 25:
							goto IL_0127;
						case 26:
							goto IL_012d;
						case 28:
							goto IL_0141;
						case 29:
							goto IL_0160;
						case 30:
							goto IL_0166;
						case 31:
							goto IL_016c;
						case 33:
							goto IL_017d;
						case 34:
							goto IL_01a2;
						case 35:
							goto IL_01b0;
						case 37:
							goto IL_01b9;
						case 39:
							goto IL_01e2;
						case 27:
						case 32:
						case 36:
						case 38:
						case 40:
						case 41:
							goto IL_01f1;
						case 42:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 11:
						case 43:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01b9:
					num2 = 37;
					if (!(Operators.CompareString(text, " ", TextCompare: false) == 0 && num5 == 0 && num6 == 1))
					{
						goto IL_01e2;
					}
					goto IL_01f1;
					IL_01e2:
					num2 = 39;
					text2 += text;
					goto IL_01f1;
					IL_01b0:
					num2 = 35;
					num6 = 1;
					goto IL_01f1;
					IL_01f1:
					num2 = 41;
					num7 = checked(num7 + 1);
					goto IL_01fa;
					IL_000b:
					num2 = 2;
					num7 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num8 = 0;
					goto IL_0015;
					IL_0015:
					num2 = 4;
					num5 = 0;
					goto IL_001a;
					IL_001a:
					num2 = 5;
					num6 = 0;
					goto IL_001f;
					IL_001f:
					num2 = 6;
					text = "";
					goto IL_0028;
					IL_0028:
					num2 = 7;
					left = "";
					goto IL_0031;
					IL_0031:
					num2 = 8;
					text2 = "";
					goto IL_003a;
					IL_003a:
					num2 = 9;
					result = MyArg;
					goto IL_003f;
					IL_003f:
					num2 = 10;
					if ((Strings.InStr(MyArg, "\"") == 0) | (Operators.CompareString(Strings.Trim(MyArg), "", TextCompare: false) == 0))
					{
						goto end_IL_0001_3;
					}
					goto IL_0073;
					IL_0073:
					num2 = 13;
					num8 = Strings.Len(MyArg);
					goto IL_007e;
					IL_007e:
					num2 = 14;
					num9 = num8;
					num7 = 1;
					goto IL_01fa;
					IL_01fa:
					if (num7 > num9)
					{
						break;
					}
					goto IL_008d;
					IL_008d:
					num2 = 15;
					text = Strings.Mid(MyArg, num7, 1);
					goto IL_009b;
					IL_009b:
					num2 = 16;
					if (Operators.CompareString(text, "\t", TextCompare: false) == 0)
					{
						goto IL_00b4;
					}
					goto IL_00be;
					IL_00b4:
					num2 = 17;
					text = " ";
					goto IL_00be;
					IL_00be:
					num2 = 18;
					if (checked(num7 + 1) <= num8)
					{
						goto IL_00d2;
					}
					goto IL_00e6;
					IL_00d2:
					num2 = 19;
					left = Strings.Mid(MyArg, checked(num7 + 1), 1);
					goto IL_00f1;
					IL_00e6:
					num2 = 21;
					left = "";
					goto IL_00f1;
					IL_00f1:
					num2 = 23;
					if ((Operators.CompareString(text, "\"", TextCompare: false) == 0 && num5 == 1) & (Operators.CompareString(left, "\"", TextCompare: false) != 0))
					{
						goto IL_0121;
					}
					goto IL_0141;
					IL_0121:
					num2 = 24;
					num5 = 0;
					goto IL_0127;
					IL_0127:
					num2 = 25;
					num6 = 0;
					goto IL_012d;
					IL_012d:
					num2 = 26;
					text2 += text;
					goto IL_01f1;
					IL_0141:
					num2 = 28;
					if (Operators.CompareString(text, "\"", TextCompare: false) == 0 && num5 == 0)
					{
						goto IL_0160;
					}
					goto IL_017d;
					IL_0160:
					num2 = 29;
					num5 = 1;
					goto IL_0166;
					IL_0166:
					num2 = 30;
					num6 = 0;
					goto IL_016c;
					IL_016c:
					num2 = 31;
					text2 += text;
					goto IL_01f1;
					IL_017d:
					num2 = 33;
					if (Operators.CompareString(text, " ", TextCompare: false) == 0 && num5 == 0 && num6 == 0)
					{
						goto IL_01a2;
					}
					goto IL_01b9;
					IL_01a2:
					num2 = 34;
					text2 += text;
					goto IL_01b0;
					end_IL_0001_2:
					break;
				}
				num2 = 42;
				result = text2;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 714;
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
	public static void SetFileROAttr(string MyFile, bool MyAction)
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
					errsource = "General_Procedures - SetFileROAttr";
					bool flag = false;
					MyFile = Strings.Trim(MyFile);
					if (!File.Exists(MyFile))
					{
						goto end_IL_0001;
					}
					FileAttribute attr = Microsoft.VisualBasic.FileSystem.GetAttr(MyFile);
					if ((attr & FileAttribute.ReadOnly) == FileAttribute.ReadOnly)
					{
						flag = true;
					}
					switch (MyAction)
					{
					case true:
						if (!flag)
						{
							Microsoft.VisualBasic.FileSystem.SetAttr(MyFile, FileAttribute.ReadOnly);
						}
						break;
					case false:
						if (flag)
						{
							Microsoft.VisualBasic.FileSystem.SetAttr(MyFile, FileAttribute.Normal);
						}
						break;
					}
					goto end_IL_0001;
				}
				case 171:
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
				try0001_dispatch = 171;
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

	public static string OpenReadFileContents(string MyFile)
	{
		string result = "";
		MyFile = Strings.Trim(MyFile);
		if (Operators.CompareString(MyFile, "", TextCompare: false) != 0)
		{
			if (Strings.InStrRev(MyFile, "\\") == 0)
			{
				MyFile = Globals_Renamed.MyPCDir + MyFile;
			}
			if (MyProject.Computer.FileSystem.FileExists(MyFile))
			{
				try
				{
					StreamReader streamReader = new StreamReader(MyFile);
					try
					{
						if (streamReader.Peek() != -1)
						{
							result = streamReader.ReadToEnd();
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Interaction.MsgBox("Error reading file: " + MyFile + " (" + ex2.Message + ")", MsgBoxStyle.Exclamation, "Read Error");
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
					Interaction.MsgBox("Error opening file: " + MyFile + " (" + ex4.Message + ")", MsgBoxStyle.Exclamation, "Open Error");
					ProjectData.ClearProjectError();
				}
			}
			else
			{
				Interaction.MsgBox("File Not Found: " + MyFile, MsgBoxStyle.Exclamation, "Open Error");
			}
		}
		return result;
	}

	public static bool MakeDirectory(string sDirName, bool DoQuiet = false)
	{
		int try0001_dispatch = -1;
		bool result = default(bool);
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
					result = false;
					text = Strings.Trim(sDirName);
					if (Operators.CompareString(text, "", TextCompare: false) == 0 || Operators.CompareString(text, "\\", TextCompare: false) == 0)
					{
						break;
					}
					ProjectData.ClearProjectError();
					num2 = 2;
					if (Operators.CompareString(Strings.Right(text, 1), "\\", TextCompare: false) == 0)
					{
						text = Strings.Mid(text, 1, checked(Strings.Len(text) - 1));
					}
					if (!MyProject.Computer.FileSystem.DirectoryExists(text))
					{
						MyProject.Computer.FileSystem.CreateDirectory(text);
						if (MyProject.Computer.FileSystem.DirectoryExists(text))
						{
							result = true;
						}
					}
					else
					{
						result = true;
					}
					goto end_IL_0001;
				case 266:
					num = -1;
					switch (num2)
					{
					case 2:
						break;
					default:
						goto IL_0140;
					}
					break;
				}
				if (!DoQuiet)
				{
					Interaction.MsgBox("Error Accessing Folder " + text + "(" + Conversion.ErrorToString() + "). Specify a path you can access", MsgBoxStyle.Exclamation, "Folder Error");
				}
				Information.Err().Clear();
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 266;
				continue;
			}
			break;
			IL_0140:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Get_Ini_Data(string MyApp, string MyKey, string MyDef, short MyLen, string MyIniFile)
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
				switch (try0001_dispatch)
				{
				default:
				{
					errsource = "BuildForm - Get_Ini_data";
					ProjectData.ClearProjectError();
					num2 = 2;
					result = "";
					if (Operators.CompareString(Strings.Trim(MyIniFile), "", TextCompare: false) == 0)
					{
						MyIniFile = Globals_Renamed.IniFileName;
					}
					string lpApplicationName = MyApp;
					string lpKeyName = MyKey;
					string lpDefault = MyDef;
					string lpReturnedString = Strings.Space(MyLen);
					int nSize = Strings.Len(lpReturnedString);
					int privateProfileString = Globals_Renamed.GetPrivateProfileString(ref lpApplicationName, ref lpKeyName, ref lpDefault, ref lpReturnedString, nSize, ref MyIniFile);
					result = Strings.Left(lpReturnedString, privateProfileString);
					goto end_IL_0001;
				}
				case 157:
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
				try0001_dispatch = 157;
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

	public static string GetWindowsDir()
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string lpBuffer = default(string);
		int windowsDirectory = default(int);
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
				case 168:
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
						case 5:
							goto IL_0037;
						case 6:
							goto IL_0055;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_002a:
					num2 = 4;
					lpBuffer = Strings.Left(lpBuffer, windowsDirectory);
					goto IL_0037;
					IL_0037:
					num2 = 5;
					if (Operators.CompareString(Strings.Right(lpBuffer, 1), "\\", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0055;
					IL_001a:
					num2 = 3;
					windowsDirectory = Globals_Renamed.GetWindowsDirectory(ref lpBuffer, 145);
					goto IL_002a;
					IL_0055:
					num2 = 6;
					result = lpBuffer + "\\";
					goto end_IL_0001_3;
					IL_000b:
					num2 = 2;
					lpBuffer = new string('\0', 145);
					goto IL_001a;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				result = lpBuffer;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 168;
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

	public static string Run_Batch_Shell(string MyBatQuery, short l_WaitQ, short l_wMin = 0, short l_RunAs = 0)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
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
					result = "";
					Process process = new Process();
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					if (l_wMin == 1)
					{
						processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
					}
					else
					{
						processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
					}
					if (l_RunAs == 1)
					{
						processStartInfo.UseShellExecute = true;
						processStartInfo.Verb = "runas";
					}
					processStartInfo.FileName = "cmd.exe";
					processStartInfo.Arguments = MyBatQuery;
					process = Process.Start(processStartInfo);
					if (l_WaitQ == 1)
					{
						process.WaitForExit();
						if (!process.HasExited)
						{
							process.Kill();
						}
						process.Close();
						process.Dispose();
					}
					goto end_IL_0001;
				}
				case 249:
					num = -1;
					switch (num2)
					{
					case 2:
						result = "Error running command " + MyBatQuery + ". (" + Conversion.ErrorToString() + ")." + Get_UI("errhelp0");
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 249;
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

	public static bool SPF_Date_Chk_OK()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string text2 = default(string);
		string text = default(string);
		bool result = default(bool);
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
						string text3 = "General_Procedures - SPF_Date_Chk_OK";
						string text4 = "";
						int num3 = 0;
						string prompt = "";
						short num4 = 0;
						text2 = "";
						text = "There is probably an issue with your SQLPathFinder INI Date file. Do delete file " + Globals_Renamed.MySchemaDir + "\\SQLPathFinder.ini, perform an update and try restarting SQLPathFinder. If problem persists, contact support";
						result = false;
						text2 = "\r\n\r\nTo update:";
						text2 += "\r\n  1. Close SQLPathFinder";
						text2 += "\r\n  2. Start SQLPathFinder3";
						text2 += "\r\n  3. Enter Update when prompted";
						text4 = Get_Ini_Data("SQLPATHFINDER", "LAST_UPDATE_DATE", "NONE", 25, Globals_Renamed.MySchemaDir + "\\SQLPathFinder.ini");
						if (Operators.CompareString(Strings.UCase(text4), "NONE", TextCompare: false) == 0)
						{
							prompt = "You must update SQLPathFinder before its first use.";
							prompt += text2;
							Interaction.MsgBox(prompt, MsgBoxStyle.Information | MsgBoxStyle.MsgBoxSetForeground, "SQLPathFinder Update Required");
							goto end_IL_0001;
						}
						if (Versioned.IsNumeric(text4))
						{
							num3 = JulianDate() - Conversions.ToInteger(text4);
						}
						else
						{
							if (!Information.IsDate(text4))
							{
								prompt = text + text2;
								Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, "SQLPathFinder INI File Corruption");
								goto end_IL_0001;
							}
							num3 = (int)DateAndTime.DateDiff(DateInterval.Day, Conversions.ToDate(text4), DateAndTime.Now);
						}
						if (num3 > 7)
						{
							prompt = Get_UI("warnupdate");
							prompt += text2;
							num4 = (short)Interaction.MsgBox(prompt, MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Update Recommended");
							if (num4 == 7)
							{
								result = true;
							}
						}
						else
						{
							result = true;
						}
						goto end_IL_0001;
					}
					case 479:
						num = -1;
						switch (num2)
						{
						case 2:
						{
							string prompt = "The following error occurred " + Information.Err().Description + " ." + text + text2;
							Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, "SQLPathFinder INI File Corruption");
							Information.Err().Clear();
							goto end_IL_0001;
						}
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 479;
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

	public static void Sendkeys2(string MyText, bool wait = false)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		short num6 = default(short);
		short num7 = default(short);
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
					case 145:
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
								goto IL_001f;
							case 4:
								goto IL_0029;
							case 5:
								goto IL_0036;
							case 6:
								goto IL_0047;
							case 7:
								goto IL_0050;
							default:
								goto end_IL_0001;
							case 8:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0036:
						num2 = 5;
						SendKeys.SendWait(MyText.Substring(num5, 1));
						goto IL_0047;
						IL_0047:
						num2 = 6;
						Support.pauseMS(1);
						goto IL_0050;
						IL_0059:
						if (num5 > num6)
						{
							goto end_IL_0001_2;
						}
						goto IL_0036;
						IL_0050:
						num2 = 7;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0059;
						IL_000b:
						num2 = 2;
						MyText = " " + MyText + "~";
						goto IL_001f;
						IL_001f:
						num2 = 3;
						num7 = (short)MyText.Length;
						goto IL_0029;
						IL_0029:
						num2 = 4;
						num6 = (short)(num7 - 1);
						num5 = 0;
						goto IL_0059;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 145;
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

	public static string Get_UI(string MyKey)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string result = default(string);
		int num = default(int);
		int num3 = default(int);
		string text = default(string);
		string text2 = default(string);
		string[] currentRow = default(string[]);
		string text3 = default(string);
		object MyReader = default(object);
		bool flag = default(bool);
		string MyMsg = default(string);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num4;
				switch (try0001_dispatch)
				{
				default:
					num2 = 1;
					result = "";
					goto IL_000a;
				case 957:
					{
						num = num2;
						switch ((num3 <= -2) ? 1 : num3)
						{
						case 2:
							break;
						case 1:
							goto IL_02c9;
						default:
							goto end_IL_0001;
						}
						goto IL_0217;
					}
					IL_013e:
					num2 = 28;
					Globals_Renamed.g_UIDesc.Add(text, text2);
					goto IL_0156;
					IL_0156:
					num2 = 31;
					currentRow = null;
					goto IL_015d;
					IL_011e:
					num2 = 27;
					if (Operators.CompareString(Strings.Mid(text2, 1, 1), "!", TextCompare: false) != 0)
					{
						goto IL_013e;
					}
					goto IL_0156;
					IL_02c9:
					num4 = num + 1;
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
						goto IL_001b;
					case 5:
						goto IL_0020;
					case 6:
						goto IL_0025;
					case 7:
						goto IL_002e;
					case 8:
						goto IL_0033;
					case 9:
						goto IL_003c;
					case 10:
						goto IL_0047;
					case 12:
						goto IL_0062;
					case 13:
						goto IL_007f;
					case 18:
						goto IL_0099;
					case 19:
						goto IL_00b9;
					case 21:
					case 22:
						goto IL_00db;
					case 23:
						goto IL_00e4;
					case 24:
						goto IL_00ed;
					case 25:
						goto IL_00f9;
					case 26:
						goto IL_0105;
					case 27:
						goto IL_011e;
					case 28:
						goto IL_013e;
					case 29:
					case 30:
					case 31:
						goto IL_0156;
					case 15:
					case 16:
					case 17:
					case 32:
						goto IL_015d;
					case 33:
						goto IL_0189;
					case 34:
						goto IL_0194;
					case 35:
						goto IL_01af;
					case 36:
						goto IL_01ca;
					case 37:
					case 38:
						goto IL_01d2;
					case 40:
						goto IL_01db;
					case 41:
						goto IL_01e4;
					case 42:
						goto IL_01f8;
					case 43:
						goto IL_0200;
					case 14:
					case 20:
					case 46:
						goto IL_0217;
					case 47:
						goto IL_0230;
					case 48:
						goto IL_023f;
					case 49:
						goto IL_0262;
					case 50:
						goto IL_026d;
					case 51:
						goto IL_0288;
					case 52:
						goto IL_02a3;
					case 53:
					case 54:
						goto IL_02ab;
					case 55:
						goto end_IL_0001_2;
					default:
						goto end_IL_0001;
					case 11:
					case 39:
					case 44:
					case 45:
					case 56:
					case 57:
						goto end_IL_0001_3;
					}
					goto default;
					IL_000a:
					ProjectData.ClearProjectError();
					num3 = 2;
					goto IL_0012;
					IL_0012:
					num2 = 3;
					text3 = "General_Procedures - Load_UI_Collection";
					goto IL_001b;
					IL_001b:
					num2 = 4;
					MyReader = null;
					goto IL_0020;
					IL_0020:
					num2 = 5;
					flag = false;
					goto IL_0025;
					IL_0025:
					num2 = 6;
					MyMsg = "";
					goto IL_002e;
					IL_002e:
					num2 = 7;
					currentRow = null;
					goto IL_0033;
					IL_0033:
					num2 = 8;
					text2 = "";
					goto IL_003c;
					IL_003c:
					num2 = 9;
					text = "";
					goto IL_0047;
					IL_0047:
					num2 = 10;
					if (Operators.CompareString(MyKey, "LOAD", TextCompare: false) == 0)
					{
						goto IL_0062;
					}
					goto IL_01db;
					IL_01db:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_01e4;
					IL_01e4:
					num2 = 41;
					result = Conversions.ToString(Globals_Renamed.g_UIDesc[MyKey]);
					goto IL_01f8;
					IL_01f8:
					ProjectData.ClearProjectError();
					num3 = 0;
					goto IL_0200;
					IL_0200:
					num2 = 43;
					Information.Err().Clear();
					goto end_IL_0001_3;
					IL_0062:
					num2 = 12;
					flag = BuildForm.OpenDelimitedFile(Globals_Renamed.MySchemaDir + "\\SQLPathFinder_UI.Dat", ref MyReader, ref MyMsg);
					goto IL_007f;
					IL_007f:
					num2 = 13;
					if (!flag)
					{
						goto IL_0217;
					}
					goto IL_015d;
					IL_015d:
					num2 = 17;
					if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(MyReader, null, "EndOfData", new object[0], null, null, null))))
					{
						goto IL_0099;
					}
					goto IL_0189;
					IL_0189:
					num2 = 33;
					if (flag)
					{
						goto IL_0194;
					}
					goto IL_01d2;
					IL_0194:
					num2 = 34;
					NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
					goto IL_01af;
					IL_01af:
					num2 = 35;
					NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
					goto IL_01ca;
					IL_01ca:
					num2 = 36;
					flag = false;
					goto IL_01d2;
					IL_01d2:
					num2 = 38;
					currentRow = null;
					goto end_IL_0001_3;
					IL_0099:
					num2 = 18;
					currentRow = (string[])NewLateBinding.LateGet(MyReader, null, "ReadFields", new object[0], null, null, null);
					goto IL_00b9;
					IL_00b9:
					num2 = 19;
					if (BuildForm.VerifySchemaRow(ref currentRow, 2, ref MyMsg))
					{
						goto IL_00db;
					}
					goto IL_0217;
					IL_0217:
					num2 = 46;
					if (Operators.CompareString(MyMsg, "", TextCompare: false) == 0)
					{
						goto IL_0230;
					}
					goto IL_023f;
					IL_0230:
					num2 = 47;
					MyMsg = Information.Err().Description;
					goto IL_023f;
					IL_023f:
					num2 = 48;
					Interaction.MsgBox(text3 + " - Error loading UI Objects (" + MyMsg + ")", MsgBoxStyle.Critical, "Load Error");
					goto IL_0262;
					IL_0262:
					num2 = 49;
					if (flag)
					{
						goto IL_026d;
					}
					goto IL_02ab;
					IL_026d:
					num2 = 50;
					NewLateBinding.LateCall(MyReader, null, "Close", new object[0], null, null, null, IgnoreReturn: true);
					goto IL_0288;
					IL_0288:
					num2 = 51;
					NewLateBinding.LateCall(MyReader, null, "Dispose", new object[0], null, null, null, IgnoreReturn: true);
					goto IL_02a3;
					IL_02a3:
					num2 = 52;
					flag = false;
					goto IL_02ab;
					IL_02ab:
					num2 = 54;
					result = "@@ERROR@@";
					break;
					IL_00db:
					num2 = 22;
					text2 = currentRow[0];
					goto IL_00e4;
					IL_00e4:
					num2 = 23;
					text = currentRow[1];
					goto IL_00ed;
					IL_00ed:
					num2 = 24;
					text2 = Strings.Trim(text2);
					goto IL_00f9;
					IL_00f9:
					num2 = 25;
					text = Strings.Trim(text);
					goto IL_0105;
					IL_0105:
					num2 = 26;
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						goto IL_011e;
					}
					goto IL_0156;
					end_IL_0001_2:
					break;
				}
				num2 = 55;
				Information.Err().Clear();
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 957;
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

	public static short Activate_Emulator_2()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		short result = default(short);
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
					errsource = "General_Procedures - Activate_Emulator_2";
					int num3 = 0;
					int num4 = 0;
					string text = "";
					int HwndReturn = 0;
					short num5 = 0;
					string text2 = "errconn3";
					bool flag = false;
					while (true)
					{
						num5 = checked((short)(num5 + 1));
						if (Globals_Renamed.MSAccess_Connected == 1)
						{
							if (!Globals_Renamed.MS_hwndProcess.HasExited)
							{
								text = Globals_Renamed.MYMSAccessTitle;
								ProjectData.ClearProjectError();
								num2 = 3;
								if (Operators.CompareString(text, "", TextCompare: false) != 0)
								{
									string WindowText = "*" + text + "*";
									num3 = FindWindowLike(ref HwndReturn, 0, ref WindowText);
									if (num3 == 0)
									{
										text = "*SPFSQL3";
										HwndReturn = 0;
										num3 = FindWindowLike(ref HwndReturn, 0, ref text);
									}
									if (num3 == 0)
									{
										goto end_IL_0001;
									}
								}
								num3 = Globals_Renamed.ShowWindow(HwndReturn, 1);
								num3 = Globals_Renamed.SetWindowPos(HwndReturn, 0, 0, 0, 0, 0, -1);
								Interaction.AppActivate(Globals_Renamed.MS_hwndProcess.Id);
								result = 1;
								break;
							}
							BuildForm.Close_SQL_Emulator(Globals_Renamed.gSPFCmdWin);
							BuildForm.Invoke_SQL_Emulator();
							Support.pauseMS(4000);
							if (num5 > 1)
							{
								break;
							}
						}
						else
						{
							if (num5 > 1)
							{
								result = 0;
								break;
							}
							BuildForm.Invoke_SQL_Emulator();
							Support.pauseMS(4000);
						}
					}
					goto end_IL_0001_2;
				}
				case 418:
					{
						num = -1;
						switch (num2)
						{
						case 3:
							break;
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							goto end_IL_0001_2;
						default:
							goto end_IL_0001_3;
						}
						break;
					}
					end_IL_0001:
					break;
				}
				Interaction.MsgBox("Unable to Connect to Session.", MsgBoxStyle.Critical, "Connection Error");
				result = 0;
				break;
				end_IL_0001_3:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 418;
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

	public static void DelBatFile()
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
					case 1471:
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
								goto IL_0025;
							case 5:
								goto IL_0033;
							case 6:
								goto IL_003c;
							case 7:
								goto IL_004e;
							case 8:
								goto IL_005c;
							case 9:
								goto IL_0065;
							case 10:
								goto IL_007d;
							case 11:
								goto IL_008c;
							case 12:
								goto IL_0096;
							case 13:
								goto IL_00b3;
							case 14:
								goto IL_00c2;
							case 15:
								goto IL_00cc;
							case 16:
								goto IL_00e4;
							case 17:
								goto IL_00f3;
							case 18:
								goto IL_00fd;
							case 19:
								goto IL_0115;
							case 20:
								goto IL_0124;
							case 21:
								goto IL_012e;
							case 22:
								goto IL_0146;
							case 23:
								goto IL_0155;
							case 24:
								goto IL_015f;
							case 25:
								goto IL_0177;
							case 26:
								goto IL_0186;
							case 27:
								goto IL_0190;
							case 28:
								goto IL_01a8;
							case 29:
								goto IL_01b7;
							case 30:
								goto IL_01c1;
							case 31:
								goto IL_01d9;
							case 32:
								goto IL_01e8;
							case 33:
								goto IL_01f2;
							case 34:
								goto IL_020a;
							case 35:
								goto IL_0219;
							case 36:
								goto IL_0223;
							case 37:
								goto IL_022c;
							case 38:
								goto IL_025a;
							case 39:
								goto IL_0264;
							case 40:
								goto IL_027b;
							case 41:
								goto IL_0284;
							case 42:
								goto IL_02b2;
							case 43:
								goto IL_02bc;
							case 44:
								goto IL_02d3;
							case 45:
								goto IL_02dc;
							case 46:
								goto IL_030a;
							case 47:
								goto IL_0314;
							case 48:
								goto IL_032b;
							case 49:
								goto IL_0334;
							case 50:
								goto IL_0362;
							case 51:
								goto IL_036c;
							case 52:
								goto IL_0383;
							case 53:
								goto IL_038c;
							case 54:
								goto IL_03ba;
							case 55:
								goto IL_03c4;
							case 56:
								goto IL_03db;
							case 57:
								goto IL_03e4;
							case 58:
								goto IL_040d;
							case 59:
								goto IL_0417;
							case 60:
								goto IL_042e;
							case 61:
								goto IL_0437;
							case 62:
								goto IL_046a;
							case 63:
								goto IL_0474;
							case 64:
								goto IL_048b;
							case 65:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 66:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_036c:
						num2 = 51;
						num5++;
						goto IL_0375;
						IL_02b2:
						num2 = 42;
						File.Delete(path);
						goto IL_02bc;
						IL_0362:
						num2 = 50;
						File.Delete(path);
						goto IL_036c;
						IL_030a:
						num2 = 46;
						File.Delete(path);
						goto IL_0314;
						IL_000a:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_0013;
						IL_0013:
						num2 = 3;
						path = Globals_Renamed.MyPCDir + "jsl.jsl$schema";
						goto IL_0025;
						IL_0025:
						num2 = 4;
						if (File.Exists(path))
						{
							goto IL_0033;
						}
						goto IL_003c;
						IL_0033:
						num2 = 5;
						File.Delete(path);
						goto IL_003c;
						IL_003c:
						num2 = 6;
						path = Globals_Renamed.MyPCDir + "r.r$schema";
						goto IL_004e;
						IL_004e:
						num2 = 7;
						if (File.Exists(path))
						{
							goto IL_005c;
						}
						goto IL_0065;
						IL_005c:
						num2 = 8;
						File.Delete(path);
						goto IL_0065;
						IL_0065:
						num2 = 9;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".bat";
						goto IL_007d;
						IL_007d:
						num2 = 10;
						if (File.Exists(path))
						{
							goto IL_008c;
						}
						goto IL_0096;
						IL_008c:
						num2 = 11;
						File.Delete(path);
						goto IL_0096;
						IL_0096:
						num2 = 12;
						path = Globals_Renamed.MyPCDir + "spf_signal_test_" + Globals_Renamed.gSPFCache + ".bat";
						goto IL_00b3;
						IL_00b3:
						num2 = 13;
						if (File.Exists(path))
						{
							goto IL_00c2;
						}
						goto IL_00cc;
						IL_00c2:
						num2 = 14;
						File.Delete(path);
						goto IL_00cc;
						IL_00cc:
						num2 = 15;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".wait";
						goto IL_00e4;
						IL_00e4:
						num2 = 16;
						if (File.Exists(path))
						{
							goto IL_00f3;
						}
						goto IL_00fd;
						IL_00f3:
						num2 = 17;
						File.Delete(path);
						goto IL_00fd;
						IL_00fd:
						num2 = 18;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + "_.bat";
						goto IL_0115;
						IL_0115:
						num2 = 19;
						if (File.Exists(path))
						{
							goto IL_0124;
						}
						goto IL_012e;
						IL_0124:
						num2 = 20;
						File.Delete(path);
						goto IL_012e;
						IL_012e:
						num2 = 21;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".R";
						goto IL_0146;
						IL_0146:
						num2 = 22;
						if (File.Exists(path))
						{
							goto IL_0155;
						}
						goto IL_015f;
						IL_0155:
						num2 = 23;
						File.Delete(path);
						goto IL_015f;
						IL_015f:
						num2 = 24;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + "_RunSPF.va";
						goto IL_0177;
						IL_0177:
						num2 = 25;
						if (File.Exists(path))
						{
							goto IL_0186;
						}
						goto IL_0190;
						IL_0186:
						num2 = 26;
						File.Delete(path);
						goto IL_0190;
						IL_0190:
						num2 = 27;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + "_RunSPF.exe";
						goto IL_01a8;
						IL_01a8:
						num2 = 28;
						if (File.Exists(path))
						{
							goto IL_01b7;
						}
						goto IL_01c1;
						IL_01b7:
						num2 = 29;
						File.Delete(path);
						goto IL_01c1;
						IL_01c1:
						num2 = 30;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + "_RunSPF.bat";
						goto IL_01d9;
						IL_01d9:
						num2 = 31;
						if (File.Exists(path))
						{
							goto IL_01e8;
						}
						goto IL_01f2;
						IL_01e8:
						num2 = 32;
						File.Delete(path);
						goto IL_01f2;
						IL_01f2:
						num2 = 33;
						path = Globals_Renamed.MyPCDir + Globals_Renamed.gSPFCache + ".jsl";
						goto IL_020a;
						IL_020a:
						num2 = 34;
						if (File.Exists(path))
						{
							goto IL_0219;
						}
						goto IL_0223;
						IL_0219:
						num2 = 35;
						File.Delete(path);
						goto IL_0223;
						IL_0223:
						num2 = 36;
						path = "";
						goto IL_022c;
						IL_022c:
						num2 = 37;
						files = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Globals_Renamed.gSPFCache + ".cols");
						num6 = 0;
						goto IL_026d;
						IL_026d:
						if (num6 < files.Length)
						{
							path = files[num6];
							goto IL_025a;
						}
						goto IL_027b;
						IL_027b:
						num2 = 40;
						path = "";
						goto IL_0284;
						IL_0284:
						num2 = 41;
						files2 = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Globals_Renamed.gSPFCache + ".tab_include");
						num7 = 0;
						goto IL_02c5;
						IL_02c5:
						if (num7 < files2.Length)
						{
							path = files2[num7];
							goto IL_02b2;
						}
						goto IL_02d3;
						IL_02d3:
						num2 = 44;
						path = "";
						goto IL_02dc;
						IL_02dc:
						num2 = 45;
						files3 = Directory.GetFiles(Globals_Renamed.MyPCDir, "*_" + Globals_Renamed.gSPFCache + ".tab_exclude");
						num8 = 0;
						goto IL_031d;
						IL_031d:
						if (num8 < files3.Length)
						{
							path = files3[num8];
							goto IL_030a;
						}
						goto IL_032b;
						IL_032b:
						num2 = 48;
						path = "";
						goto IL_0334;
						IL_0334:
						num2 = 49;
						files4 = Directory.GetFiles(Globals_Renamed.MyPCDir, "r_" + Globals_Renamed.gSPFCache + ".*");
						num5 = 0;
						goto IL_0375;
						IL_0375:
						if (num5 < files4.Length)
						{
							path = files4[num5];
							goto IL_0362;
						}
						goto IL_0383;
						IL_0383:
						num2 = 52;
						path = "";
						goto IL_038c;
						IL_038c:
						num2 = 53;
						files5 = Directory.GetFiles(Globals_Renamed.MyPCDir, "spfpdr_*_" + Globals_Renamed.gSPFCache + "_scripts.spfps");
						num9 = 0;
						goto IL_03cd;
						IL_03cd:
						if (num9 < files5.Length)
						{
							path = files5[num9];
							goto IL_03ba;
						}
						goto IL_03db;
						IL_03db:
						num2 = 56;
						path = "";
						goto IL_03e4;
						IL_03e4:
						num2 = 57;
						files6 = Directory.GetFiles(Globals_Renamed.MyPCDir, Globals_Renamed.g_TmpSPFVG2 + ".*");
						num10 = 0;
						goto IL_0420;
						IL_0420:
						if (num10 < files6.Length)
						{
							path = files6[num10];
							goto IL_040d;
						}
						goto IL_042e;
						IL_042e:
						num2 = 60;
						path = "";
						goto IL_0437;
						IL_0437:
						num2 = 61;
						files7 = Directory.GetFiles(Globals_Renamed.MyPCDir, "spf_parquet_temp_" + Strings.Trim(Globals_Renamed.gSPFCache) + ".tab");
						num11 = 0;
						goto IL_047d;
						IL_047d:
						if (num11 < files7.Length)
						{
							path = files7[num11];
							goto IL_046a;
						}
						goto IL_048b;
						IL_048b:
						num2 = 64;
						BuildSQL.Delete_TempTables();
						break;
						IL_0264:
						num2 = 39;
						num6++;
						goto IL_026d;
						IL_046a:
						num2 = 62;
						File.Delete(path);
						goto IL_0474;
						IL_0474:
						num2 = 63;
						num11++;
						goto IL_047d;
						IL_025a:
						num2 = 38;
						File.Delete(path);
						goto IL_0264;
						IL_040d:
						num2 = 58;
						File.Delete(path);
						goto IL_0417;
						IL_0417:
						num2 = 59;
						num10++;
						goto IL_0420;
						IL_0314:
						num2 = 47;
						num8++;
						goto IL_031d;
						IL_03ba:
						num2 = 54;
						File.Delete(path);
						goto IL_03c4;
						IL_03c4:
						num2 = 55;
						num9++;
						goto IL_03cd;
						IL_02bc:
						num2 = 43;
						num7++;
						goto IL_02c5;
						end_IL_0001_2:
						break;
					}
					num2 = 65;
					Information.Err().Clear();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1471;
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

	public static string Transform_YN(string MyYN)
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
				case 187:
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
							goto IL_0064;
						case 6:
							goto IL_006f;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 5:
						case 7:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_000c:
					num2 = 2;
					switch (Strings.UCase(MyYN))
					{
					case "Y":
					case "-1":
					case "YES":
						break;
					case "N":
					case "0":
						goto IL_006f;
					default:
						goto end_IL_0001_2;
					}
					goto IL_0064;
					IL_006f:
					num2 = 6;
					result = "No";
					goto end_IL_0001_3;
					IL_0064:
					num2 = 4;
					result = "Yes";
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				result = MyYN;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 187;
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

	public static int FindWindowLike(ref int HwndReturn, int hWndStart, ref string WindowText)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int window = default(int);
		string lpString = default(string);
		int length = default(int);
		int result = default(int);
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
								goto IL_0023;
							case 5:
								goto IL_002f;
							case 6:
							case 7:
								goto IL_003a;
							case 8:
								goto IL_0049;
							case 11:
								goto IL_0059;
							case 12:
								goto IL_0067;
							case 13:
								goto IL_0078;
							case 14:
								goto IL_0087;
							case 15:
								goto IL_009a;
							case 16:
								goto IL_00a8;
							case 17:
								goto IL_00bb;
							case 18:
								goto IL_00c4;
							case 20:
								goto IL_00cf;
							case 9:
							case 10:
							case 19:
							case 21:
							case 22:
							case 23:
								goto IL_00e0;
							case 24:
								goto IL_00fa;
							case 25:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 26:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00bb:
						num2 = 17;
						_0024STATIC_0024FindWindowLike_0024038108810E_0024iFound = 1;
						goto IL_00c4;
						IL_00c4:
						num2 = 18;
						HwndReturn = window;
						goto IL_00e0;
						IL_00a8:
						num2 = 16;
						if (LikeOperator.LikeString(lpString, WindowText, CompareMethod.Binary))
						{
							goto IL_00bb;
						}
						goto IL_00cf;
						IL_00cf:
						num2 = 20;
						window = Globals_Renamed.GetWindow(window, 2);
						goto IL_00e0;
						IL_000b:
						num2 = 2;
						if (_0024STATIC_0024FindWindowLike_0024038108810E_0024level == 0)
						{
							goto IL_001b;
						}
						goto IL_003a;
						IL_001b:
						num2 = 3;
						_0024STATIC_0024FindWindowLike_0024038108810E_0024iFound = 0;
						goto IL_0023;
						IL_0023:
						num2 = 4;
						if (hWndStart == 0)
						{
							goto IL_002f;
						}
						goto IL_003a;
						IL_002f:
						num2 = 5;
						hWndStart = Globals_Renamed.GetDesktopWindow();
						goto IL_003a;
						IL_003a:
						num2 = 7;
						_0024STATIC_0024FindWindowLike_0024038108810E_0024level++;
						goto IL_0049;
						IL_0049:
						num2 = 8;
						window = Globals_Renamed.GetWindow(hWndStart, 5);
						goto IL_00e0;
						IL_00e0:
						num2 = 10;
						if (!((window == 0) | (_0024STATIC_0024FindWindowLike_0024038108810E_0024iFound == 1)))
						{
							goto IL_0059;
						}
						goto IL_00fa;
						IL_00fa:
						num2 = 24;
						_0024STATIC_0024FindWindowLike_0024038108810E_0024level--;
						break;
						IL_0059:
						num2 = 11;
						length = FindWindowLike(ref HwndReturn, window, ref WindowText);
						goto IL_0067;
						IL_0067:
						num2 = 12;
						if (_0024STATIC_0024FindWindowLike_0024038108810E_0024iFound == 0)
						{
							goto IL_0078;
						}
						goto IL_00e0;
						IL_0078:
						num2 = 13;
						lpString = Strings.Space(255);
						goto IL_0087;
						IL_0087:
						num2 = 14;
						length = Globals_Renamed.GetWindowText(window, ref lpString, 255);
						goto IL_009a;
						IL_009a:
						num2 = 15;
						lpString = Strings.Left(lpString, length);
						goto IL_00a8;
						end_IL_0001_2:
						break;
					}
					num2 = 25;
					result = _0024STATIC_0024FindWindowLike_0024038108810E_0024iFound;
					break;
				}
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
		return result;
	}

	public static string Save_File_General(string MyScript, string MyFile, bool MyAppend = false)
	{
		string result = "";
		try
		{
			MyProject.Computer.FileSystem.WriteAllText(MyFile, MyScript, MyAppend, Encoding.ASCII);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			result = "Could not write to file: " + MyFile + ". (" + ex2.Message + ").";
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Substitute_Chars(string InputStr, string FindChar, string NewChar)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string sDest = default(string);
		short maxInsertLength = default(short);
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
							goto IL_0014;
						case 4:
							goto IL_0019;
						case 5:
							goto IL_001e;
						case 6:
							goto IL_0023;
						case 7:
							goto IL_002e;
						case 10:
							goto IL_0035;
						case 11:
							goto IL_0044;
						case 12:
							goto IL_0052;
						case 8:
						case 9:
						case 13:
							goto IL_0063;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0035:
					num2 = 10;
					num5 = checked((short)Strings.InStr(sDest, FindChar));
					goto IL_0044;
					IL_0044:
					num2 = 11;
					if (num5 != 0)
					{
						goto IL_0052;
					}
					goto IL_0063;
					IL_0063:
					num2 = 9;
					if (num5 == 0)
					{
						break;
					}
					goto IL_0035;
					IL_0052:
					num2 = 12;
					StringType.MidStmtStr(ref sDest, num5, maxInsertLength, NewChar);
					goto IL_0063;
					IL_000b:
					num2 = 2;
					sDest = "";
					goto IL_0014;
					IL_0014:
					num2 = 3;
					num5 = 0;
					goto IL_0019;
					IL_0019:
					num2 = 4;
					maxInsertLength = 0;
					goto IL_001e;
					IL_001e:
					num2 = 5;
					sDest = InputStr;
					goto IL_0023;
					IL_0023:
					num2 = 6;
					maxInsertLength = checked((short)Strings.Len(FindChar));
					goto IL_002e;
					IL_002e:
					num2 = 7;
					num5 = -1;
					goto IL_0063;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				result = sDest;
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

	public static int JulianDate()
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		string errsource = default(string);
		int result;
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
						int num3 = 0;
						int num4 = 1;
						int num5 = 1;
						int num6 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						errsource = "General_Procedures - JulianDate";
						int num7 = DateAndTime.Month(DateAndTime.Now);
						int num8 = DateAndTime.Day(DateAndTime.Now);
						int num9 = DateAndTime.Year(DateAndTime.Now);
						result = -1;
						if (num9 <= 1585)
						{
							num4 = 0;
						}
						result = (int)Math.Round(-1.0 * Conversion.Int(7.0 * (Conversion.Int((double)(num7 + 9) / 12.0) + (double)num9) / 4.0));
						if (num7 - 9 < 0)
						{
							num5 = -1;
						}
						num3 = Math.Abs(num7 - 9);
						num6 = (int)Conversion.Int((double)num9 + (double)num5 * Conversion.Int((double)num3 / 7.0));
						num6 = (int)Math.Round(-1.0 * Conversion.Int((Conversion.Int((double)num6 / 100.0) + 1.0) * 3.0 / 4.0));
						result = (int)Math.Round((double)result + Conversion.Int((double)(275 * num7) / 9.0) + (double)num8 + (double)(num4 * num6));
						result = result + 1721027 + 2 * num4 + 367 * num9;
						goto end_IL_0001;
					}
					case 399:
						num = -1;
						switch (num2)
						{
						case 2:
							Support.ErrService(Information.Err().Number, errsource, Information.Err().Description);
							Information.Err().Clear();
							result = -1;
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 399;
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

	public static string Set_Get_Fn(string MyMode, string MyType, string MyData, string l_Alias)
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
						errsource = "General_Procedures - Set_Get_Fn";
						result = "";
						MyMode = Strings.Trim(Strings.UCase(MyMode));
						MyType = Strings.Trim(Strings.UCase(MyType));
						l_Alias = Strings.Trim(Strings.LCase(l_Alias));
						MyData = Strings.Trim(Strings.UCase(MyData));
						short num3 = (short)Strings.InStr(l_Alias, "->");
						if (num3 != 0)
						{
							l_Alias = Strings.Trim(Strings.Mid(l_Alias, 1, num3 - 1));
						}
						string left = MyMode;
						if (Operators.CompareString(left, "S", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "G", TextCompare: false) == 0)
							{
								result = ((!(LikeOperator.LikeString(MyData, "*F(*)->*", CompareMethod.Binary) | LikeOperator.LikeString(MyData, "*S(*)->*", CompareMethod.Binary))) ? "N" : "Y");
							}
							goto end_IL_0001;
						}
						string text = ((Operators.CompareString(MyType, "C", TextCompare: false) == 0) ? ((Operators.CompareString(MyData, "NONE", TextCompare: false) != 0) ? "s" : "f") : ((Operators.CompareString(MyType, "F", TextCompare: false) != 0) ? "f" : ((Operators.CompareString(MyData, "X", TextCompare: false) != 0) ? "s" : "f")));
						if ((Operators.CompareString(l_Alias, "all", TextCompare: false) == 0) | (Operators.CompareString(l_Alias, "sql", TextCompare: false) == 0) | (Operators.CompareString(l_Alias, "ilv", TextCompare: false) == 0) | (Operators.CompareString(l_Alias, "txt", TextCompare: false) == 0))
						{
							l_Alias = "x";
						}
						result = text + "(" + l_Alias + ")->";
						goto end_IL_0001;
					}
					case 494:
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
				try0001_dispatch = 494;
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

	public static string Get_Obj_Alias(string MyMode, string InAlias)
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
						errsource = "General_Procedures - Get_Obj_Alias";
						short num3 = 0;
						string text = "";
						text2 = "";
						num3 = (short)Strings.InStr(InAlias, "->");
						if (num3 != 0)
						{
							text = Strings.Mid(InAlias, num3);
							InAlias = Strings.Mid(InAlias, 1, num3 - 1);
						}
						string left = Strings.Trim(Strings.UCase(MyMode));
						if (Operators.CompareString(left, "T->F", TextCompare: false) == 0)
						{
							text2 = Strings.Trim(Strings.LCase(InAlias)) switch
							{
								"sql" => "Sqlite (all)", 
								"all" => "All Sources", 
								"txt" => "Text (all)", 
								"ilv" => "Inline (all)", 
								_ => "View (" + InAlias + ")", 
							};
						}
						else if (Operators.CompareString(left, "F->T", TextCompare: false) == 0)
						{
							switch (Strings.Trim(Strings.LCase(InAlias)))
							{
							case "sqlite (all)":
								text2 = "SQL";
								break;
							case "all sources":
								text2 = "All";
								break;
							case "text (all)":
								text2 = "TXT";
								break;
							case "inline (all)":
								text2 = "ILV";
								break;
							default:
								num3 = (short)Strings.InStrRev(InAlias, ")");
								text2 = ((num3 == 0) ? InAlias : Strings.Mid(InAlias, 7, num3 - 7));
								break;
							}
						}
						text2 += text;
						goto end_IL_0001;
					}
					case 492:
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
				try0001_dispatch = 492;
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

	public static int Count_Character(string MyStr, string MyChar)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
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
				case 187:
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
							goto end_IL_0001_2;
						case 6:
							goto IL_0042;
						case 7:
							goto IL_0051;
						case 8:
							goto IL_006c;
						case 9:
							goto IL_0072;
						default:
							goto end_IL_0001;
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_007b:
					if (num5 > num6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0051;
					IL_0051:
					num2 = 7;
					if (Operators.CompareString(Strings.Mid(MyStr, num5, 1), MyChar, TextCompare: false) == 0)
					{
						goto IL_006c;
					}
					goto IL_0072;
					IL_0042:
					num2 = 6;
					num6 = Strings.Len(MyStr);
					num5 = 1;
					goto IL_007b;
					IL_006c:
					num2 = 8;
					num7 = checked(num7 + 1);
					goto IL_0072;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num7 = 0;
					goto IL_0014;
					IL_0014:
					num2 = 4;
					if (Operators.CompareString(MyStr, "", TextCompare: false) == 0 || Operators.CompareString(MyChar, "", TextCompare: false) == 0)
					{
						break;
					}
					goto IL_0042;
					IL_0072:
					num2 = 9;
					num5 = checked(num5 + 1);
					goto IL_007b;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				num7 = 0;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 187;
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
		return num7;
	}

	public static string GetNodeKind(string MyNode, string MyMode = "F")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		string text = default(string);
		int num6 = default(int);
		string left = default(string);
		string text2 = default(string);
		int nonodes = default(int);
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
				case 1397:
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
							goto IL_002f;
						case 8:
							goto IL_003e;
						case 9:
							goto IL_004f;
						case 10:
							goto IL_006a;
						case 11:
							goto IL_0087;
						case 12:
							goto IL_00a2;
						case 13:
							goto IL_00bd;
						case 15:
							goto IL_00ca;
						case 16:
							goto IL_00e5;
						case 14:
						case 17:
						case 18:
							goto IL_00f1;
						case 20:
							goto IL_046b;
						case 21:
							goto IL_0477;
						case 22:
							goto IL_0499;
						case 23:
							goto IL_04aa;
						case 24:
							goto end_IL_0001_2;
						case 29:
						case 30:
							goto IL_04d2;
						default:
							goto end_IL_0001;
						case 19:
						case 25:
						case 26:
						case 27:
						case 28:
						case 31:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0499:
					num2 = 22;
					num5 = Strings.InStr(text, "-");
					goto IL_04aa;
					IL_04aa:
					num2 = 23;
					if (num5 <= 1)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0477:
					num2 = 21;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(MyMode)), "P", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0499;
					IL_04d2:
					num2 = 30;
					num6 = checked(num6 + 1);
					goto IL_04db;
					IL_000b:
					num2 = 2;
					num6 = 0;
					goto IL_0010;
					IL_0010:
					num2 = 3;
					num5 = 0;
					goto IL_0015;
					IL_0015:
					num2 = 4;
					left = "";
					goto IL_001e;
					IL_001e:
					num2 = 5;
					text2 = "";
					goto IL_0027;
					IL_0027:
					num2 = 6;
					text = "";
					goto IL_002f;
					IL_002f:
					num2 = 7;
					MyNode = Strings.Trim(Get_First_Node(MyNode));
					goto IL_003e;
					IL_003e:
					num2 = 8;
					nonodes = Globals_Renamed.nonodes;
					num6 = 0;
					goto IL_04db;
					IL_04db:
					if (num6 > nonodes)
					{
						goto end_IL_0001_3;
					}
					goto IL_004f;
					IL_004f:
					num2 = 9;
					left = Strings.UCase(Globals_Renamed.AllNodes[num6].Name);
					goto IL_006a;
					IL_006a:
					num2 = 10;
					if (Operators.CompareString(left, Strings.UCase(MyNode), TextCompare: false) == 0)
					{
						goto IL_0087;
					}
					goto IL_04d2;
					IL_0087:
					num2 = 11;
					text2 = Strings.UCase(Globals_Renamed.AllNodes[num6].DBDATA);
					goto IL_00a2;
					IL_00a2:
					num2 = 12;
					if (LikeOperator.LikeString(Strings.UCase(text2), "P:*@UBER@*", CompareMethod.Binary))
					{
						goto IL_00bd;
					}
					goto IL_00ca;
					IL_00bd:
					num2 = 13;
					text2 = "DB:POSTGRES-UBER";
					goto IL_00f1;
					IL_00ca:
					num2 = 15;
					if (LikeOperator.LikeString(Strings.UCase(text2), "O:*@UBER@*", CompareMethod.Binary))
					{
						goto IL_00e5;
					}
					goto IL_00f1;
					IL_00e5:
					num2 = 16;
					text2 = "DB:ORACLE-UBER:";
					goto IL_00f1;
					IL_00f1:
					num2 = 18;
					switch (text2)
					{
					case "DB:ORACLE":
					case "DB:ORACLE-UBER":
					case "DB:SQLSERVER":
					case "DB:SQLSERVER-UBER":
					case "DB:TERADATA":
					case "DB:TERADATA-.NET":
					case "DB:TERADATA-UBER":
					case "DB:MYSQL":
					case "DB:SQLITE":
					case "DB:HADOOP":
					case "DB:HADOOP-IMPALA":
					case "DB:SAPHANA":
					case "DB:DENODO":
					case "DB:SNOWFLAKE":
					case "DB:ODBC":
					case "DB:MONGO":
					case "DB:IBI-DAAS":
					case "DB:OLAP":
					case "DB:TERADATA-MIDAS":
					case "DB:POSTGRES":
					case "DB:POSTGRES-UBER":
					case "DB:IMBIGDATA":
						break;
					default:
						goto end_IL_0001_3;
					}
					goto IL_046b;
					IL_046b:
					num2 = 20;
					text = Strings.Mid(text2, 4);
					goto IL_0477;
					end_IL_0001_2:
					break;
				}
				num2 = 24;
				text = Strings.Mid(text, 1, checked(num5 - 1));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1397;
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

	public static string TestNodeKind(string MyConn, string MyNodeKind)
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
				case 1364:
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
							goto IL_0045;
						case 7:
							goto IL_0053;
						case 8:
							goto IL_007b;
						case 10:
							goto IL_0089;
						case 11:
							goto IL_00b2;
						case 13:
							goto IL_00c1;
						case 14:
							goto IL_00ea;
						case 16:
							goto IL_00f9;
						case 17:
							goto IL_0122;
						case 19:
							goto IL_0131;
						case 20:
							goto IL_015a;
						case 22:
							goto IL_0169;
						case 23:
							goto IL_01b6;
						case 25:
							goto IL_01c5;
						case 26:
							goto IL_020a;
						case 28:
							goto IL_0219;
						case 29:
							goto IL_0242;
						case 31:
							goto IL_0251;
						case 32:
							goto IL_027a;
						case 34:
							goto IL_0289;
						case 35:
							goto IL_02c0;
						case 37:
							goto IL_02cf;
						case 38:
							goto IL_02f8;
						case 40:
							goto IL_0307;
						case 41:
							goto IL_0330;
						case 43:
							goto IL_033f;
						case 44:
							goto IL_0357;
						case 46:
							goto IL_0366;
						case 47:
							goto IL_039d;
						case 49:
							goto IL_03ac;
						case 50:
							goto IL_03d5;
						case 52:
							goto IL_03e1;
						case 53:
							goto IL_0418;
						case 55:
							goto IL_0424;
						case 56:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 6:
						case 9:
						case 12:
						case 15:
						case 18:
						case 21:
						case 24:
						case 27:
						case 30:
						case 33:
						case 36:
						case 39:
						case 42:
						case 45:
						case 48:
						case 51:
						case 54:
						case 57:
						case 58:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_033f:
					num2 = 43;
					if (Operators.CompareString(MyNodeKind, "IMBIGDATA", TextCompare: false) == 0)
					{
						goto IL_0357;
					}
					goto IL_0366;
					IL_0357:
					num2 = 44;
					result = "IMBIGDATA";
					goto end_IL_0001_3;
					IL_01b6:
					num2 = 23;
					result = "UBER";
					goto end_IL_0001_3;
					IL_0366:
					num2 = 46;
					if (LikeOperator.LikeString(MyConn, "*@SQL7@*", CompareMethod.Binary) || LikeOperator.LikeString(MyConn, "*@SQL2000@*", CompareMethod.Binary) || Operators.CompareString(MyNodeKind, "SQLSERVER", TextCompare: false) == 0)
					{
						goto IL_039d;
					}
					goto IL_03ac;
					IL_000b:
					num2 = 2;
					result = "";
					goto IL_0013;
					IL_0013:
					num2 = 3;
					MyConn = Strings.UCase(MyConn);
					goto IL_001d;
					IL_001d:
					num2 = 4;
					if (Strings.InStr(MyConn, "MIDAS") != 0 || Operators.CompareString(MyNodeKind, "TERADATA-MIDAS", TextCompare: false) == 0)
					{
						goto IL_0045;
					}
					goto IL_0053;
					IL_015a:
					num2 = 20;
					result = "SNOWFLAKE";
					goto end_IL_0001_3;
					IL_0053:
					num2 = 7;
					if (Strings.InStr(MyConn, "@HADOOPIMPALAODBC@") != 0 || Operators.CompareString(MyNodeKind, "HADOOP-IMPALA", TextCompare: false) == 0)
					{
						goto IL_007b;
					}
					goto IL_0089;
					IL_0122:
					num2 = 17;
					result = "DENODO";
					goto end_IL_0001_3;
					IL_0089:
					num2 = 10;
					if (Strings.InStr(MyConn, "HADOOP") != 0 || Operators.CompareString(MyNodeKind, "HADOOP", TextCompare: false) == 0)
					{
						goto IL_00b2;
					}
					goto IL_00c1;
					IL_03ac:
					num2 = 49;
					if (Strings.InStr(MyConn, "MSOLAP") != 0 || Operators.CompareString(MyNodeKind, "OLAP", TextCompare: false) == 0)
					{
						goto IL_03d5;
					}
					goto IL_03e1;
					IL_00c1:
					num2 = 13;
					if (Strings.InStr(MyConn, "@SAPHANAODBC@") != 0 || Operators.CompareString(MyNodeKind, "SAPHANA", TextCompare: false) == 0)
					{
						goto IL_00ea;
					}
					goto IL_00f9;
					IL_00ea:
					num2 = 14;
					result = "SAPHANA";
					goto end_IL_0001_3;
					IL_00f9:
					num2 = 16;
					if (Strings.InStr(MyConn, "@DENODOODBC@") != 0 || Operators.CompareString(MyNodeKind, "DENODO", TextCompare: false) == 0)
					{
						goto IL_0122;
					}
					goto IL_0131;
					IL_03e1:
					num2 = 52;
					if (Strings.InStr(MyConn, "IBI-DAAS") != 0 || Operators.CompareString(MyNodeKind, "IBI-DAAS", TextCompare: false) == 0 || Operators.CompareString(MyNodeKind, "IBI", TextCompare: false) == 0)
					{
						goto IL_0418;
					}
					goto IL_0424;
					IL_0131:
					num2 = 19;
					if (Strings.InStr(MyConn, "@SNOWFLAKEODBC@") != 0 || Operators.CompareString(MyNodeKind, "SNOWFLAKE", TextCompare: false) == 0)
					{
						goto IL_015a;
					}
					goto IL_0169;
					IL_00b2:
					num2 = 11;
					result = "HADOOP";
					goto end_IL_0001_3;
					IL_0169:
					num2 = 22;
					if (BuildSQL.IsXEUS_DIS(MyConn) || Strings.InStr(MyConn, "@UBER@") != 0 || Operators.CompareString(MyNodeKind, "ORACLE-UBER", TextCompare: false) == 0 || Operators.CompareString(MyNodeKind, "SQLSERVER-UBER", TextCompare: false) == 0 || Operators.CompareString(MyNodeKind, "POSTGRES-UBER", TextCompare: false) == 0)
					{
						goto IL_01b6;
					}
					goto IL_01c5;
					IL_007b:
					num2 = 8;
					result = "HADOOP-IMPALA";
					goto end_IL_0001_3;
					IL_0424:
					num2 = 55;
					if (Strings.InStr(MyConn, "MYSQL") == 0 && Operators.CompareString(MyNodeKind, "MYSQL", TextCompare: false) != 0)
					{
						goto end_IL_0001_3;
					}
					break;
					IL_0045:
					num2 = 5;
					result = "TERADATA-MIDAS";
					goto end_IL_0001_3;
					IL_0418:
					num2 = 53;
					result = "IBI-DAAS";
					goto end_IL_0001_3;
					IL_01c5:
					num2 = 25;
					if (((LikeOperator.LikeString(MyConn, "*@TERADATA@*", CompareMethod.Binary) || LikeOperator.LikeString(MyConn, "*@TERADATAIWA@*", CompareMethod.Binary)) && LikeOperator.LikeString(MyConn, "*(.NET)*", CompareMethod.Binary)) || Operators.CompareString(MyNodeKind, "TERADATA-.NET", TextCompare: false) == 0)
					{
						goto IL_020a;
					}
					goto IL_0219;
					IL_0330:
					num2 = 41;
					result = "MONGO";
					goto end_IL_0001_3;
					IL_039d:
					num2 = 47;
					result = "SQLSERVER";
					goto end_IL_0001_3;
					IL_03d5:
					num2 = 50;
					result = "OLAP";
					goto end_IL_0001_3;
					IL_0219:
					num2 = 28;
					if (LikeOperator.LikeString(MyConn, "TERADATA@*", CompareMethod.Binary) || Operators.CompareString(MyNodeKind, "TERADATA-UBER", TextCompare: false) == 0)
					{
						goto IL_0242;
					}
					goto IL_0251;
					IL_02f8:
					num2 = 38;
					result = "DUCKDB";
					goto end_IL_0001_3;
					IL_0251:
					num2 = 31;
					if (Strings.InStr(MyConn, "TERADATA") != 0 || Operators.CompareString(MyNodeKind, "TERADATA", TextCompare: false) == 0)
					{
						goto IL_027a;
					}
					goto IL_0289;
					IL_02c0:
					num2 = 35;
					result = "SQLITE";
					goto end_IL_0001_3;
					IL_0289:
					num2 = 34;
					if (LikeOperator.LikeString(MyConn, "*.SDB", CompareMethod.Binary) || Strings.InStr(MyConn, "SQLITE") != 0 || Operators.CompareString(MyNodeKind, "SQLITE", TextCompare: false) == 0)
					{
						goto IL_02c0;
					}
					goto IL_02cf;
					IL_027a:
					num2 = 32;
					result = "TERADATA";
					goto end_IL_0001_3;
					IL_0242:
					num2 = 29;
					result = "TERADATA-UBER";
					goto end_IL_0001_3;
					IL_02cf:
					num2 = 37;
					if (LikeOperator.LikeString(MyConn, "DUCKDB:*", CompareMethod.Binary) || Operators.CompareString(MyNodeKind, "DUCKDB", TextCompare: false) == 0)
					{
						goto IL_02f8;
					}
					goto IL_0307;
					IL_020a:
					num2 = 26;
					result = "TERADATA-.NET";
					goto end_IL_0001_3;
					IL_0307:
					num2 = 40;
					if (Strings.InStr(MyConn, "@MONGO@") != 0 || Operators.CompareString(MyNodeKind, "MONGO", TextCompare: false) == 0)
					{
						goto IL_0330;
					}
					goto IL_033f;
					end_IL_0001_2:
					break;
				}
				num2 = 56;
				result = "MYSQL";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1364;
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

	public static string NodeCheck_s(string MyNode, string MyMode, short ShowUN, string MyAppendDB = "N")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string NodeCheck = default(string);
		short num6 = default(short);
		string left = default(string);
		string OUser = default(string);
		string OPwd = default(string);
		int num7 = default(int);
		string[] DynArray = default(string[]);
		string text = default(string);
		short num8 = default(short);
		short nonodes = default(short);
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
					case 1291:
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
								goto IL_0030;
							case 8:
								goto IL_0035;
							case 9:
								goto IL_003a;
							case 10:
								goto IL_0044;
							case 11:
								goto IL_004f;
							case 12:
								goto IL_0067;
							case 13:
								goto IL_0070;
							case 14:
								goto IL_007b;
							case 16:
								goto IL_0085;
							case 17:
								goto IL_008a;
							case 18:
								goto IL_00a1;
							case 19:
								goto IL_00ac;
							case 20:
								goto IL_00b8;
							case 21:
								goto IL_00c4;
							case 15:
							case 22:
							case 23:
								goto IL_00d7;
							case 24:
								goto IL_00e7;
							case 25:
								goto IL_0110;
							case 26:
							case 27:
								goto IL_011c;
							case 28:
								goto IL_012e;
							case 29:
								goto IL_0149;
							case 30:
								goto IL_0166;
							case 31:
								goto IL_017e;
							case 33:
								goto IL_019f;
							case 34:
								goto IL_01c6;
							case 35:
								goto IL_02a6;
							case 36:
								goto IL_02d0;
							case 37:
								goto IL_02de;
							case 39:
								goto IL_02fc;
							case 42:
								goto IL_0326;
							case 43:
								goto IL_0334;
							case 45:
								goto IL_0358;
							case 38:
							case 40:
							case 41:
							case 44:
							case 46:
							case 47:
							case 48:
								goto IL_0385;
							case 49:
								goto IL_038f;
							case 50:
								goto IL_0399;
							case 51:
								goto IL_03cb;
							case 56:
							case 57:
								goto IL_03e4;
							case 32:
							case 52:
							case 53:
							case 54:
							case 55:
							case 58:
								goto IL_03f7;
							default:
								goto end_IL_0001;
							case 59:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0326:
						num2 = 42;
						if (num5 == 1)
						{
							goto IL_0334;
						}
						goto IL_0358;
						IL_0334:
						num2 = 43;
						NodeCheck = Strings.Mid(Strings.Trim(Globals_Renamed.AllNodes[num6].DBDATA), 3);
						goto IL_0385;
						IL_02fc:
						num2 = 39;
						NodeCheck = NodeCheck + "," + Strings.Trim(Globals_Renamed.AllNodes[num6].DBDATA);
						goto IL_0385;
						IL_0358:
						num2 = 45;
						NodeCheck = NodeCheck + "," + Strings.Mid(Strings.Trim(Globals_Renamed.AllNodes[num6].DBDATA), 3);
						goto IL_0385;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						left = "";
						goto IL_0019;
						IL_0019:
						num2 = 4;
						OUser = "";
						goto IL_0022;
						IL_0022:
						num2 = 5;
						OPwd = "";
						goto IL_002b;
						IL_002b:
						num2 = 6;
						num7 = -1;
						goto IL_0030;
						IL_0030:
						num2 = 7;
						DynArray = null;
						goto IL_0035;
						IL_0035:
						num2 = 8;
						num5 = 0;
						goto IL_003a;
						IL_003a:
						num2 = 9;
						text = "";
						goto IL_0044;
						IL_0044:
						num2 = 10;
						MyMode = Strings.UCase(MyMode);
						goto IL_004f;
						IL_004f:
						num2 = 11;
						if (Operators.CompareString(MyMode, "O", TextCompare: false) == 0)
						{
							goto IL_0067;
						}
						goto IL_0085;
						IL_0067:
						num2 = 12;
						NodeCheck = "N";
						goto IL_0070;
						IL_0070:
						num2 = 13;
						MyNode = Get_First_Node(MyNode);
						goto IL_007b;
						IL_007b:
						num2 = 14;
						num7 = 1;
						goto IL_00d7;
						IL_0085:
						num2 = 16;
						NodeCheck = MyNode;
						goto IL_008a;
						IL_008a:
						num2 = 17;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
						goto IL_00a1;
						IL_00a1:
						num2 = 18;
						DynArray.Initialize();
						goto IL_00ac;
						IL_00ac:
						num2 = 19;
						DynArray[0] = "";
						goto IL_00b8;
						IL_00b8:
						num2 = 20;
						DynArray[1] = "";
						goto IL_00c4;
						IL_00c4:
						num2 = 21;
						num7 = ParseAndFillArray(MyNode, ",", ref DynArray);
						goto IL_00d7;
						IL_00d7:
						num2 = 23;
						num8 = (short)num7;
						num5 = 1;
						goto IL_0401;
						IL_0401:
						if (num5 > num8)
						{
							goto end_IL_0001_2;
						}
						goto IL_00e7;
						IL_00e7:
						num2 = 24;
						if (Operators.CompareString(MyMode, "S", TextCompare: false) == 0 || Operators.CompareString(MyMode, "S2", TextCompare: false) == 0)
						{
							goto IL_0110;
						}
						goto IL_011c;
						IL_0385:
						num2 = 48;
						OUser = "";
						goto IL_038f;
						IL_0110:
						num2 = 25;
						MyNode = DynArray[num5];
						goto IL_011c;
						IL_011c:
						num2 = 27;
						nonodes = Globals_Renamed.nonodes;
						num6 = 0;
						goto IL_03ee;
						IL_03ee:
						if (num6 <= nonodes)
						{
							goto IL_012e;
						}
						goto IL_03f7;
						IL_012e:
						num2 = 28;
						left = Strings.UCase(Globals_Renamed.AllNodes[num6].Name);
						goto IL_0149;
						IL_0149:
						num2 = 29;
						if (Operators.CompareString(left, Strings.UCase(MyNode), TextCompare: false) == 0)
						{
							goto IL_0166;
						}
						goto IL_03e4;
						IL_0166:
						num2 = 30;
						if (Operators.CompareString(MyMode, "O", TextCompare: false) == 0)
						{
							goto IL_017e;
						}
						goto IL_019f;
						IL_017e:
						num2 = 31;
						NodeCheck = Strings.UCase(Globals_Renamed.AllNodes[num6].ORCL);
						goto IL_03f7;
						IL_019f:
						num2 = 33;
						text = Strings.Mid(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num6].DBDATA)), 1, 2);
						goto IL_01c6;
						IL_01c6:
						num2 = 34;
						switch (text)
						{
						case "S:":
						case "G:":
						case "T:":
						case "Q:":
						case "L:":
						case "H:":
						case "I:":
						case "M:":
						case "P:":
							goto IL_02a6;
						}
						if ((Operators.CompareString(MyMode, "S2", TextCompare: false) == 0 && Operators.CompareString(text, "O:", TextCompare: false) == 0) || (Operators.CompareString(text, "DB", TextCompare: false) == 0 && Operators.CompareString(MyAppendDB, "Y", TextCompare: false) == 0))
						{
							goto IL_02a6;
						}
						goto IL_03f7;
						IL_038f:
						num2 = 49;
						OPwd = "";
						goto IL_0399;
						IL_03cb:
						num2 = 51;
						TranslateUNPW(ShowUN, ref NodeCheck, ref OUser, ref OPwd);
						goto IL_03f7;
						IL_0399:
						num2 = 50;
						if ((Strings.InStr(Strings.UCase(NodeCheck), "@SQL7@") != 0) | (Strings.InStr(Strings.UCase(NodeCheck), "@PW&") != 0))
						{
							goto IL_03cb;
						}
						goto IL_03f7;
						IL_03f7:
						num2 = 58;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0401;
						IL_02a6:
						num2 = 35;
						if (Operators.CompareString(text, "DB", TextCompare: false) == 0 && Operators.CompareString(MyAppendDB, "Y", TextCompare: false) == 0)
						{
							goto IL_02d0;
						}
						goto IL_0326;
						IL_03e4:
						num2 = 57;
						num6 = (short)unchecked(num6 + 1);
						goto IL_03ee;
						IL_02d0:
						num2 = 36;
						if (num5 == 1)
						{
							goto IL_02de;
						}
						goto IL_02fc;
						IL_02de:
						num2 = 37;
						NodeCheck = Strings.Trim(Globals_Renamed.AllNodes[num6].DBDATA);
						goto IL_0385;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1291;
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
		return NodeCheck;
	}

	public static string GetNodeToUse(string dNode, string cNode)
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
				case 140:
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
							goto IL_0051;
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
					if (!((Operators.CompareString(Strings.UCase(cNode), "DEFAULT", TextCompare: false) == 0) | (Operators.CompareString(Strings.Trim(cNode), "", TextCompare: false) == 0) | (Operators.CompareString(Strings.UCase(cNode), "NONE", TextCompare: false) == 0)))
					{
						break;
					}
					goto IL_0051;
					IL_0051:
					num2 = 3;
					result = dNode;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				result = cNode;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 140;
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

	public static void GetUserPwd2(short ShowUN, string MyNode, ref string OUser, ref string OPwd, ref string oDB)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string NodeCheck = default(string);
		string left = default(string);
		short nonodes = default(short);
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
				case 757:
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
							goto IL_002b;
						case 7:
							goto IL_0034;
						case 8:
							goto IL_003d;
						case 9:
							goto IL_004c;
						case 10:
							goto IL_0067;
						case 11:
							goto IL_0078;
						case 12:
							goto IL_0092;
						case 13:
							goto IL_00aa;
						case 14:
							goto IL_00db;
						case 15:
							goto IL_00e5;
						case 17:
							goto IL_00f5;
						case 18:
							goto IL_0123;
						case 19:
							goto IL_012d;
						case 21:
							goto IL_013d;
						case 22:
							goto IL_016b;
						case 23:
							goto IL_0175;
						case 24:
							goto IL_017f;
						case 26:
							goto IL_0190;
						case 27:
							goto IL_01be;
						case 28:
							goto IL_01de;
						case 30:
							goto IL_01ef;
						case 31:
							goto IL_021d;
						case 32:
							goto end_IL_0001_2;
						case 35:
						case 36:
							goto IL_0236;
						default:
							goto end_IL_0001;
						case 16:
						case 20:
						case 25:
						case 29:
						case 33:
						case 34:
						case 37:
						case 38:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_01ef:
					num2 = 30;
					if (!LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "*@USER-PROMPT@", CompareMethod.Binary))
					{
						goto end_IL_0001_3;
					}
					goto IL_021d;
					IL_021d:
					num2 = 31;
					OUser = "$ERROR$";
					break;
					IL_01de:
					num2 = 28;
					TranslateUNPW(ShowUN, ref NodeCheck, ref OUser, ref OPwd);
					goto end_IL_0001_3;
					IL_0236:
					num2 = 36;
					num5 = checked((short)unchecked(num5 + 1));
					goto IL_023e;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					left = "";
					goto IL_0018;
					IL_0018:
					num2 = 4;
					NodeCheck = "";
					goto IL_0021;
					IL_0021:
					num2 = 5;
					oDB = "N";
					goto IL_002b;
					IL_002b:
					num2 = 6;
					OUser = "";
					goto IL_0034;
					IL_0034:
					num2 = 7;
					OPwd = "";
					goto IL_003d;
					IL_003d:
					num2 = 8;
					MyNode = Strings.Trim(Strings.UCase(MyNode));
					goto IL_004c;
					IL_004c:
					num2 = 9;
					if (Operators.CompareString(MyNode, "", TextCompare: false) == 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0067;
					IL_0067:
					num2 = 10;
					nonodes = Globals_Renamed.nonodes;
					num5 = 0;
					goto IL_023e;
					IL_023e:
					if (num5 > nonodes)
					{
						goto end_IL_0001_3;
					}
					goto IL_0078;
					IL_0078:
					num2 = 11;
					left = Strings.UCase(Globals_Renamed.AllNodes[num5].Name);
					goto IL_0092;
					IL_0092:
					num2 = 12;
					if (Operators.CompareString(left, MyNode, TextCompare: false) == 0)
					{
						goto IL_00aa;
					}
					goto IL_0236;
					IL_00aa:
					num2 = 13;
					if (Operators.CompareString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "O:IWA", TextCompare: false) == 0)
					{
						goto IL_00db;
					}
					goto IL_00f5;
					IL_00db:
					num2 = 14;
					OUser = "//";
					goto IL_00e5;
					IL_00e5:
					num2 = 15;
					OPwd = "";
					goto end_IL_0001_3;
					IL_00f5:
					num2 = 17;
					if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "O:CBSQL", CompareMethod.Binary))
					{
						goto IL_0123;
					}
					goto IL_013d;
					IL_0123:
					num2 = 18;
					OUser = "$CB$";
					goto IL_012d;
					IL_012d:
					num2 = 19;
					OPwd = "";
					goto end_IL_0001_3;
					IL_013d:
					num2 = 21;
					if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "DB:*", CompareMethod.Binary))
					{
						goto IL_016b;
					}
					goto IL_0190;
					IL_016b:
					num2 = 22;
					OUser = "";
					goto IL_0175;
					IL_0175:
					num2 = 23;
					OPwd = "";
					goto IL_017f;
					IL_017f:
					num2 = 24;
					oDB = "Y";
					goto end_IL_0001_3;
					IL_0190:
					num2 = 26;
					if (LikeOperator.LikeString(Strings.UCase(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA)), "O:*UN&*", CompareMethod.Binary))
					{
						goto IL_01be;
					}
					goto IL_01ef;
					IL_01be:
					num2 = 27;
					NodeCheck = Strings.Mid(Strings.Trim(Globals_Renamed.AllNodes[num5].DBDATA), 3);
					goto IL_01de;
					end_IL_0001_2:
					break;
				}
				num2 = 32;
				OPwd = "";
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 757;
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

	public static void TranslateUNPW(short ShowUN, ref string NodeCheck, ref string OUser, ref string OPwd)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		short num5 = default(short);
		string text2 = default(string);
		short num6 = default(short);
		short num7 = default(short);
		string text3 = default(string);
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
					case 500:
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
								goto IL_0043;
							case 10:
								goto IL_004d;
							case 11:
								goto IL_0057;
							case 12:
								goto IL_006a;
							case 13:
								goto IL_007b;
							case 14:
								goto IL_0096;
							case 15:
								goto IL_00a9;
							case 16:
								goto IL_00bb;
							case 17:
								goto IL_00c2;
							case 18:
								goto IL_00dc;
							case 19:
								goto IL_00ef;
							case 20:
								goto IL_010a;
							case 21:
								goto IL_011d;
							case 22:
								goto IL_012f;
							case 23:
								goto IL_0136;
							case 26:
								goto IL_0156;
							case 27:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 24:
							case 25:
							case 28:
							case 29:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_012f:
						num2 = 22;
						OPwd = text;
						goto IL_0136;
						IL_0136:
						num2 = 23;
						NodeCheck = Strings.Mid(NodeCheck, 1, num5 - 1) + OPwd + text2;
						goto end_IL_0001_3;
						IL_011d:
						num2 = 21;
						text2 = Strings.Mid(NodeCheck, num5 + 3 + num6);
						goto IL_012f;
						IL_0156:
						num2 = 26;
						OUser = "*********";
						break;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						num7 = 0;
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
						text3 = "";
						goto IL_002b;
						IL_002b:
						num2 = 7;
						text2 = "";
						goto IL_0034;
						IL_0034:
						num2 = 8;
						if (ShowUN == 1)
						{
							goto IL_0043;
						}
						goto IL_0156;
						IL_0043:
						num2 = 9;
						OUser = "";
						goto IL_004d;
						IL_004d:
						num2 = 10;
						OPwd = "";
						goto IL_0057;
						IL_0057:
						num2 = 11;
						num7 = (short)Strings.InStr(NodeCheck, "UN&");
						goto IL_006a;
						IL_006a:
						num2 = 12;
						if (num7 == 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_007b;
						IL_007b:
						num2 = 13;
						num6 = (short)Strings.InStr(Strings.Mid(NodeCheck, num7 + 3), "&");
						goto IL_0096;
						IL_0096:
						num2 = 14;
						text3 = Strings.Mid(NodeCheck, num7 + 3, num6 - 1);
						goto IL_00a9;
						IL_00a9:
						num2 = 15;
						text2 = Strings.Mid(NodeCheck, num7 + 3 + num6);
						goto IL_00bb;
						IL_00bb:
						num2 = 16;
						OUser = text3;
						goto IL_00c2;
						IL_00c2:
						num2 = 17;
						NodeCheck = Strings.Mid(NodeCheck, 1, num7 - 1) + OUser + text2;
						goto IL_00dc;
						IL_00dc:
						num2 = 18;
						num5 = (short)Strings.InStr(NodeCheck, "PW&");
						goto IL_00ef;
						IL_00ef:
						num2 = 19;
						num6 = (short)Strings.InStr(Strings.Mid(NodeCheck, num5 + 3), "&");
						goto IL_010a;
						IL_010a:
						num2 = 20;
						text = Strings.Mid(NodeCheck, num5 + 3, num6 - 1);
						goto IL_011d;
						end_IL_0001_2:
						break;
					}
					num2 = 27;
					OPwd = "*********";
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 500;
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

	public static void GetUNPWList(short ShowUN, string MyNode, ref string OUser, ref string OPwd)
	{
		int num = 0;
		short num2 = 0;
		string text = "";
		string text2 = "";
		string text3 = "";
		string oDB = "";
		string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
		num = ParseAndFillArray(MyNode, ",", ref DynArray);
		OUser = "";
		OPwd = "";
		checked
		{
			short num3 = (short)num;
			for (num2 = 1; num2 <= num3; num2 = (short)unchecked(num2 + 1))
			{
				text = Strings.Trim(DynArray[num2]);
				text2 = "";
				text3 = "";
				GetUserPwd2(ShowUN, text, ref text2, ref text3, ref oDB);
				if (Operators.CompareString(oDB, "Y", TextCompare: false) == 0)
				{
					OUser = "";
					OPwd = "";
					break;
				}
				if (Operators.CompareString(text2, "$ERROR$", TextCompare: false) == 0)
				{
					OUser = text2;
					OPwd = "";
					break;
				}
				if (num2 == 1)
				{
					OUser = text2;
					OPwd = text3;
				}
				else
				{
					OUser = OUser + "@@@@@" + text2;
					OPwd = OPwd + "@@@@@" + text3;
				}
			}
			DynArray = null;
			num = 0;
		}
	}

	public static string LoadCSVHeaders1(string CurrentFile)
	{
		string text = "";
		try
		{
			string text2 = "";
			string text3 = ",";
			Microsoft.VisualBasic.FileSystem.ChDrive(Globals_Renamed.MyPCDir);
			Microsoft.VisualBasic.FileSystem.ChDir(Globals_Renamed.MyPCDir);
			if (File.Exists(CurrentFile))
			{
				StreamReader streamReader = new StreamReader(CurrentFile);
				try
				{
					if (streamReader.Peek() != -1)
					{
						text2 = streamReader.ReadLine();
					}
					text3 = BuildForm.GetFileDLM(CurrentFile);
					if (Operators.CompareString(text3, ",", TextCompare: false) != 0)
					{
						text2 = Strings.Replace(text2, text3, " (c),");
					}
					text = text2;
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						text += " (c)";
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox("Error loading CSV file: " + CurrentFile + ". (" + ex2.Message + ":" + Information.Err().Description + ")." + Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Loading CSV File");
					ProjectData.ClearProjectError();
				}
				finally
				{
					streamReader.Close();
					streamReader.Dispose();
					streamReader = null;
				}
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex4.Message + ":)." + Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Accessing CSV File");
			ProjectData.ClearProjectError();
		}
		return text;
	}

	public static void LoadCSVHeaders(string CurrentFile, ref ComboBox MyCombo, int MyMode)
	{
		checked
		{
			try
			{
				string theString = "";
				int num = 0;
				short num2 = 0;
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				short num3 = 0;
				string text5 = ",";
				Microsoft.VisualBasic.FileSystem.ChDrive(Globals_Renamed.MyPCDir);
				Microsoft.VisualBasic.FileSystem.ChDir(Globals_Renamed.MyPCDir);
				if (!File.Exists(CurrentFile))
				{
					return;
				}
				StreamReader streamReader = new StreamReader(CurrentFile);
				try
				{
					if (streamReader.Peek() != -1)
					{
						theString = streamReader.ReadLine();
					}
					text5 = BuildForm.GetFileDLM(CurrentFile);
					string[] DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
					num = ParseAndFillArray(theString, text5, ref DynArray);
					text = MyCombo.Text;
					MyCombo.Items.Clear();
					short num4 = (short)num;
					for (num2 = 1; num2 <= num4; num2 = (short)unchecked(num2 + 1))
					{
						text2 = DynArray[num2];
						text3 = "";
						short num5 = (short)Strings.Len(text2);
						for (num3 = 1; num3 <= num5; num3 = (short)unchecked(num3 + 1))
						{
							text4 = Strings.Mid(text2, num3, 1);
							if (Operators.CompareString(text4, "\"", TextCompare: false) != 0)
							{
								text3 += text4;
							}
						}
						if (MyMode == 0)
						{
							MyCombo.Items.Add(Conversions.ToString(unchecked((int)num2)) + ". " + text3);
						}
						else
						{
							MyCombo.Items.Add(Strings.Trim(text3));
						}
					}
					MyCombo.Text = text;
					DynArray = null;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Interaction.MsgBox("Error loading CSV file: " + CurrentFile + ". (" + ex2.Message + ":" + Information.Err().Description + ")." + Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Loading CSV File");
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
				Interaction.MsgBox("Error accessing CSV file: " + CurrentFile + ". (" + ex4.Message + ":)." + Get_UI("errhelp0"), MsgBoxStyle.Exclamation, "Error Accessing CSV File");
				ProjectData.ClearProjectError();
			}
		}
	}

	public static string Strip_Hdr_Colon_Col(string MyElement)
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
						errsource = "General_Procedures - Strip_Hdr_Colon_Col";
						short num3 = 0;
						num3 = (short)Strings.InStr(MyElement, ":");
						result = ((num3 != 0) ? Strings.Mid(MyElement, num3 + 2) : MyElement);
						goto end_IL_0001;
					}
					case 106:
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
				try0001_dispatch = 106;
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

	public static string Strip_CSV_Square(string MyColumn, short ll_ObjectType, string ll_JoinDuckDB = "N")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string text = default(string);
		string text2 = default(string);
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
				case 319:
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
						case 4:
							goto IL_002b;
						case 6:
							goto IL_0038;
						case 7:
							goto IL_0041;
						case 5:
						case 8:
						case 9:
							goto IL_004b;
						case 10:
							goto IL_0050;
						case 11:
							goto IL_005f;
						case 12:
							goto IL_006a;
						case 13:
							goto IL_0086;
						case 15:
							goto IL_0093;
						case 16:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 14:
						case 17:
						case 18:
						case 19:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_006a:
					num2 = 12;
					if (Operators.CompareString(MyColumn, text + text2, TextCompare: false) == 0)
					{
						goto IL_0086;
					}
					goto IL_0093;
					IL_0086:
					num2 = 13;
					MyColumn = "";
					goto end_IL_0001_3;
					IL_005f:
					num2 = 11;
					MyColumn = Strings.Trim(MyColumn);
					goto IL_006a;
					IL_0093:
					num2 = 15;
					if (!((Operators.CompareString(Strings.Mid(MyColumn, 1, 1), text, TextCompare: false) == 0) & (Operators.CompareString(Strings.Mid(MyColumn, Strings.Len(MyColumn), 1), text2, TextCompare: false) == 0)))
					{
						goto end_IL_0001_3;
					}
					break;
					IL_000b:
					num2 = 2;
					if (Operators.CompareString(ll_JoinDuckDB, "Y", TextCompare: false) == 0)
					{
						goto IL_0022;
					}
					goto IL_0038;
					IL_0022:
					num2 = 3;
					text = "\"";
					goto IL_002b;
					IL_002b:
					num2 = 4;
					text2 = "\"";
					goto IL_004b;
					IL_0038:
					num2 = 6;
					text = "[";
					goto IL_0041;
					IL_0041:
					num2 = 7;
					text2 = "]";
					goto IL_004b;
					IL_004b:
					num2 = 9;
					result = MyColumn;
					goto IL_0050;
					IL_0050:
					num2 = 10;
					if (!BuildForm.IsCSV(ll_ObjectType))
					{
						goto end_IL_0001_3;
					}
					goto IL_005f;
					end_IL_0001_2:
					break;
				}
				num2 = 16;
				result = Strings.Mid(MyColumn, 2, checked(Strings.Len(MyColumn) - 2));
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 319;
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

	public static string Strip_Column(string MyColumn, short MyMode)
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
						errsource = "General_Procedures - Strip_Column";
						string text = "";
						short num3 = 0;
						short num4 = 0;
						string text2 = "";
						switch (MyMode)
						{
						case 0:
							text = Strings.Mid(Strings.Trim(MyColumn), 2);
							num3 = (short)Strings.InStr(text, ".");
							if (num3 != 0)
							{
								text2 = Strings.Mid(text, 1, num3 - 1);
								text = Strings.Mid(text, num3 + 1);
								text = Strings.Trim(text) + Strings.Trim(text2);
							}
							else
							{
								text = Strings.Trim(MyColumn);
							}
							break;
						case 1:
							num3 = (short)Strings.InStr(MyColumn, ".");
							text = ((num3 == 0) ? MyColumn : Strings.Mid(MyColumn, num3 + 1));
							break;
						case 3:
							num3 = (short)Strings.InStr(MyColumn, "'$");
							if (num3 != 0)
							{
								text = Strings.Mid(MyColumn, num3 + 2);
								num4 = (short)Strings.InStrRev(text, "'");
								if (num4 != 0)
								{
									text = Strings.Mid(text, 1, num4 - 1);
								}
							}
							else
							{
								text = MyColumn;
							}
							break;
						}
						result = text;
						goto end_IL_0001;
					}
					case 363:
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
				try0001_dispatch = 363;
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

	public static string Strip_Alias(short MyMode, string MyText)
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
						errsource = "General_Procedures - Strip_Alias";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						result = "";
						num3 = (short)Strings.InStr(MyText, "->");
						if (num3 != 0)
						{
							result = ((MyMode != 0) ? Strings.Trim(Strings.Mid(MyText, 1, num3 - 1)) : Strings.Trim(Strings.Mid(MyText, num3 + 2)));
						}
						goto end_IL_0001;
					}
					case 144:
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
				try0001_dispatch = 144;
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

	public static string Get_Node_Value(string MyText)
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
						errsource = "General_Procedures - Get_Node_Value";
						ProjectData.ClearProjectError();
						num2 = 2;
						short num3 = 0;
						result = "";
						num3 = (short)Strings.InStr(MyText, "{");
						if (num3 != 0)
						{
							MyText = Strings.Mid(MyText, num3 + 1);
						}
						num3 = (short)Strings.InStrRev(MyText, "}");
						if (num3 != 0)
						{
							MyText = Strings.Mid(MyText, 1, num3 - 1);
						}
						result = Strings.Trim(MyText);
						goto end_IL_0001;
					}
					case 153:
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
				try0001_dispatch = 153;
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

	public static string Set_Node_Value(string MyNode, string MyValue)
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
				switch (try0001_dispatch)
				{
				default:
				{
					errsource = "General_Procedures - Set_Node_Value";
					string text = "";
					ProjectData.ClearProjectError();
					num2 = 2;
					short num3 = 0;
					result = MyNode;
					text = MyNode;
					if (LikeOperator.LikeString(Strings.Trim(text), "*{*}", CompareMethod.Binary))
					{
						num3 = checked((short)Strings.InStr(text, "{"));
						if (num3 != 0)
						{
							text = Strings.Mid(text, 1, num3);
						}
						result = text + Strings.Trim(MyValue) + "}";
					}
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

	public static int ParseAndFillArray(string TheString, string Delim, ref string[] DynArray)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int num7 = default(int);
		int num8 = default(int);
		int result = default(int);
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
							case 7:
							case 18:
								goto IL_0025;
							case 8:
								goto IL_003e;
							case 9:
								goto IL_004f;
							case 10:
								goto IL_005d;
							case 13:
								goto IL_007c;
							case 12:
							case 14:
							case 15:
								goto IL_008f;
							case 16:
								goto IL_00a8;
							case 17:
								goto IL_00af;
							case 11:
							case 19:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 20:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_008f:
						num2 = 15;
						num5 += Strings.Len(DynArray[num6]) + Strings.Len(Delim);
						goto IL_00a8;
						IL_00a8:
						num2 = 16;
						num7 = num5;
						goto IL_00af;
						IL_007c:
						num2 = 13;
						DynArray[num6] = Strings.Mid(TheString, num7, num8);
						goto IL_008f;
						IL_00af:
						num2 = 17;
						num6++;
						goto IL_0025;
						IL_000b:
						num2 = 2;
						num6 = 1;
						goto IL_0010;
						IL_0010:
						num2 = 3;
						num7 = 1;
						goto IL_0015;
						IL_0015:
						num2 = 4;
						num5 = 1;
						goto IL_001a;
						IL_001a:
						num2 = 5;
						num8 = Strings.Len(TheString);
						goto IL_0025;
						IL_0025:
						num2 = 7;
						DynArray = (string[])Utils.CopyArray(DynArray, new string[num6 + 1]);
						goto IL_003e;
						IL_003e:
						num2 = 8;
						num8 = Strings.InStr(num7, TheString, Delim) - num7;
						goto IL_004f;
						IL_004f:
						num2 = 9;
						if (num8 < 0)
						{
							goto IL_005d;
						}
						goto IL_007c;
						IL_005d:
						num2 = 10;
						DynArray[num6] = Strings.Right(TheString, Strings.Len(TheString) - (num5 - 1));
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 19;
					result = Information.UBound(DynArray);
					break;
				}
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

	public static short ToUpper(short KeyAscii)
	{
		int try0001_dispatch = -1;
		int num2 = default(int);
		short result;
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
					ProjectData.ClearProjectError();
					num2 = 2;
					text = Conversions.ToString(Strings.Chr(KeyAscii));
					KeyAscii = checked((short)Strings.Asc(Strings.UCase(text)));
					result = KeyAscii;
					goto end_IL_0001;
				}
				case 62:
					num = -1;
					switch (num2)
					{
					case 2:
						Information.Err().Clear();
						result = 0;
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 62;
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

	public static string StripQuotesAndDates(string TextStr, string MyDataType, short gSpecialTest)
	{
		int try0001_dispatch = -1;
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
						short num3 = 0;
						string text = "";
						string text2 = "";
						string text3 = "";
						bool flag = false;
						ProjectData.ClearProjectError();
						num2 = 2;
						text2 = "";
						MyDataType = Strings.UCase(MyDataType);
						short num4;
						switch (MyDataType)
						{
						default:
							if (Operators.CompareString(MyDataType, "V", TextCompare: false) == 0)
							{
								goto case "D";
							}
							if (IsDateDT(MyDataType, 1) && (LikeOperator.LikeString(Strings.UCase(TextStr), "*SPF-JOB-START-DAY*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*TIMESTAMP*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*DATE*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*@SPF-FOR-LOOP*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*SPF_FN$LWW", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*NOW()*", CompareMethod.Binary)))
							{
								text2 = ((!LikeOperator.LikeString(Strings.UCase(TextStr), "*@SPF-FOR-LOOP*", CompareMethod.Binary)) ? TextStr : Strings.Replace(TextStr, "'", "", 1, -1, CompareMethod.Text));
								break;
							}
							goto IL_023e;
						case "D":
						case "S":
						case "O":
							{
								if (((LikeOperator.LikeString(Strings.UCase(TextStr), "*SYSDATE*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*CURRENT_*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*SPF_FN$*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*TRUNC(*", CompareMethod.Binary)) | LikeOperator.LikeString(Strings.UCase(TextStr), "*SYSDATE*", CompareMethod.Binary)) || LikeOperator.LikeString(Strings.UCase(TextStr), "*'D'*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*'DD'*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*TO_NUMBER*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*@*_OF_WW@*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*SPF-JOB-START-DAY*", CompareMethod.Binary) || LikeOperator.LikeString(Strings.UCase(TextStr), "*@SPF-FOR-LOOP*", CompareMethod.Binary) || LikeOperator.LikeString(TextStr, "*<<<*>>>*", CompareMethod.Binary) || LikeOperator.LikeString(TextStr, "*NOW()*", CompareMethod.Binary))
								{
									text2 = TextStr;
									break;
								}
								goto IL_023e;
							}
							IL_023e:
							text2 = Strings.Replace(TextStr, "\"", "", 1, -1, CompareMethod.Text);
							if (Operators.CompareString(MyDataType, "E", TextCompare: false) == 0)
							{
								break;
							}
							if (Operators.CompareString(Strings.Trim(text2), "''", TextCompare: false) != 0 && Strings.InStr(text2, "''") != 0)
							{
								text2 = Strings.Replace(text2, "''", "<//q//>", 1, -1, CompareMethod.Text);
								flag = true;
							}
							text2 = Strings.Replace(text2, "'", "", 1, -1, CompareMethod.Text);
							if (flag)
							{
								text2 = Strings.Replace(text2, "<//q//>", "''", 1, -1, CompareMethod.Text);
							}
							if (Operators.CompareString(MyDataType, "D", TextCompare: false) == 0 || Operators.CompareString(MyDataType, "V", TextCompare: false) == 0)
							{
								num3 = (short)Strings.InStr(text2, "To_Date(");
								if (num3 > 0)
								{
									text2 = Strings.Mid(text2, num3 + 9);
								}
								num3 = (short)Strings.InStr(text2, ",dd-mon-yyyy hh24:mi:ss)");
								if (num3 > 0)
								{
									text2 = Strings.Mid(text2, 1, num3 - 1);
								}
							}
							else if (Operators.CompareString(MyDataType, "O", TextCompare: false) == 0)
							{
								num3 = (short)Strings.InStr(text2, "dt.datetime.strptime(");
								if (num3 > 0)
								{
									text2 = Strings.Mid(text2, num3 + 9);
								}
								num3 = (short)Strings.InStr(text2, ",%Y-%m-%d %H:%M:%S)");
								if (num3 > 0)
								{
									text2 = Strings.Mid(text2, 1, num3 - 1);
								}
							}
							if (gSpecialTest != 1 && gSpecialTest != 2)
							{
								break;
							}
							text3 = text2;
							text2 = "";
							num4 = (short)Strings.Len(text3);
							for (num3 = 1; num3 <= num4; num3 = (short)unchecked(num3 + 1))
							{
								text = Strings.Mid(text3, num3, 1);
								if ((Operators.CompareString(text, "(", TextCompare: false) != 0) & (Operators.CompareString(text, ")", TextCompare: false) != 0))
								{
									text2 += text;
								}
							}
							break;
						}
						result = text2;
						goto end_IL_0001;
					}
					case 1121:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Error parsing expression. (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Critical, "Error Parsing String");
							goto end_IL_0001;
						}
						break;
					}
					goto IL_0497;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 1121;
				continue;
			}
			break;
			IL_0497:
			throw ProjectData.CreateProjectError(-2146828237);
		}
		if (num != 0)
		{
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static string Replace_vbcrlf(string MyTxt)
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
							goto IL_0021;
						case 4:
							goto IL_0037;
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
					text = Strings.Replace(MyTxt, ",", "", 1, -1, CompareMethod.Text);
					goto IL_0021;
					IL_0021:
					num2 = 3;
					text = Strings.Replace(text, "\r\n", "", 1, -1, CompareMethod.Text);
					goto IL_0037;
					IL_0037:
					num2 = 4;
					text = Strings.Replace(text, "\t", "", 1, -1, CompareMethod.Text);
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 5;
				text = Strings.Replace(text, "\"", "", 1, -1, CompareMethod.Text);
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
		return text;
	}

	public static string Replace_Special_Chars(string TextStr, string ReplChar = "", int MyMode = 0)
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
						errsource = "General_Procedures - Replace_Special_Chars";
						string @string = "-'\" @./,+%*^$#!~&()=><?\\|}{[];:` ";
						switch (MyMode)
						{
						case 1:
							@string = "'\"@./,+%*^$#!~&()=><?\\|}{[];:`";
							break;
						case 2:
							@string = "\",[]{}|`'<>";
							break;
						}
						short num3 = 0;
						string text = "";
						string text2 = "";
						ReplChar = Strings.Trim(ReplChar);
						short num4 = (short)Strings.Len(TextStr);
						for (num3 = 1; num3 <= num4; num3 = (short)unchecked(num3 + 1))
						{
							text = Strings.Mid(TextStr, num3, 1);
							if (Strings.InStr(@string, text) == 0)
							{
								text2 += text;
							}
							else if (Operators.CompareString(ReplChar, "", TextCompare: false) != 0)
							{
								text2 += ReplChar;
							}
						}
						result = text2;
						goto end_IL_0001;
					}
					case 236:
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
				try0001_dispatch = 236;
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

	public static string Get_First_Node(string MyNode)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string result = default(string);
		string[] DynArray = default(string[]);
		int num5 = default(int);
		int num6 = default(int);
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
							goto IL_000b;
						case 3:
							goto IL_0021;
						case 4:
							goto IL_002b;
						case 5:
							goto IL_0035;
						case 6:
							goto IL_003d;
						case 7:
							goto IL_004e;
						case 8:
							goto IL_0059;
						case 9:
							goto IL_0079;
						case 11:
						case 12:
							goto IL_008b;
						case 10:
						case 13:
							goto IL_009a;
						case 14:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 15:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0079:
					num2 = 9;
					result = Strings.Trim(DynArray[num5]);
					goto IL_009a;
					IL_009a:
					num2 = 13;
					DynArray = null;
					break;
					IL_0059:
					num2 = 8;
					if (Operators.CompareString(Strings.Trim(DynArray[num5]), "", TextCompare: false) != 0)
					{
						goto IL_0079;
					}
					goto IL_008b;
					IL_008b:
					num2 = 12;
					num5 = checked(num5 + 1);
					goto IL_0094;
					IL_000b:
					num2 = 2;
					DynArray = (string[])Utils.CopyArray(DynArray, new string[2]);
					goto IL_0021;
					IL_0021:
					num2 = 3;
					DynArray.Initialize();
					goto IL_002b;
					IL_002b:
					num2 = 4;
					DynArray.Initialize();
					goto IL_0035;
					IL_0035:
					num2 = 5;
					result = "";
					goto IL_003d;
					IL_003d:
					num2 = 6;
					num6 = ParseAndFillArray(MyNode, ",", ref DynArray);
					goto IL_004e;
					IL_004e:
					num2 = 7;
					num7 = num6;
					num5 = 1;
					goto IL_0094;
					IL_0094:
					if (num5 <= num7)
					{
						goto IL_0059;
					}
					goto IL_009a;
					end_IL_0001_2:
					break;
				}
				num2 = 14;
				num6 = 0;
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
		return result;
	}

	public static string CompressString(string inputString)
	{
		string result = "";
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(inputString);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
				{
					gZipStream.Write(bytes, 0, bytes.Length);
				}
				memoryStream.Position = 0L;
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			throw;
		}
		finally
		{
			byte[] bytes = null;
			byte[] array = null;
			byte[] array2 = null;
		}
	}

	public static string DecompressZippedString(string inputZippedString)
	{
		string result = "";
		int num = 4096;
		try
		{
			byte[] buffer = Convert.FromBase64String(inputZippedString);
			byte[] array = new byte[checked(num + 1)];
			using (GZipStream gZipStream = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress))
			{
				using MemoryStream memoryStream = new MemoryStream();
				int num2 = 0;
				for (num2 = gZipStream.Read(array, 0, num); num2 > 0; num2 = gZipStream.Read(array, 0, num))
				{
					memoryStream.Write(array, 0, num2);
				}
				result = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return result;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			throw;
		}
		finally
		{
			byte[] buffer = null;
			byte[] array = null;
		}
	}
}

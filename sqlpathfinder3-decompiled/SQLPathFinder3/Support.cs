using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class Support
{
	public class NodeTextSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			TreeNode treeNode = (TreeNode)x;
			TreeNode treeNode2 = (TreeNode)y;
			if (Conversions.ToBoolean(Operators.AndObject(Operators.AndObject((treeNode.Nodes.Count == 0) & (treeNode2.Nodes.Count == 0), Operators.CompareObjectEqual(treeNode.Tag, "", TextCompare: false)), Operators.CompareObjectEqual(treeNode2.Tag, "", TextCompare: false))))
			{
				return string.Compare(treeNode.Text, treeNode2.Text);
			}
			return 0;
		}

		int IComparer.Compare(object x, object y)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Compare
			return this.Compare(x, y);
		}
	}

	public class NodeNameSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			TreeNode treeNode = (TreeNode)x;
			TreeNode treeNode2 = (TreeNode)y;
			if (Conversions.ToBoolean(Operators.AndObject(Operators.AndObject((treeNode.Nodes.Count == 0) & (treeNode2.Nodes.Count == 0), Operators.CompareObjectEqual(treeNode.Tag, "", TextCompare: false)), Operators.CompareObjectEqual(treeNode2.Tag, "", TextCompare: false))))
			{
				return string.Compare(treeNode.Name, treeNode2.Name);
			}
			return 0;
		}

		int IComparer.Compare(object x, object y)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Compare
			return this.Compare(x, y);
		}
	}

	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int GetTickCount();

	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern object Sleep(int dwMilliseconds);

	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int SleepEx(int dwMilliseconds, int bAlertable);

	public static void pauseMS(int pauseTime)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int tickCount = default(int);
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
							goto IL_000b;
						case 5:
							goto IL_0015;
						case 6:
							goto IL_0020;
						case 3:
						case 4:
						case 7:
							goto IL_0029;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0029:
					num2 = 4;
					if (checked(GetTickCount() - tickCount) >= pauseTime)
					{
						goto end_IL_0001_2;
					}
					goto IL_0015;
					IL_0015:
					num2 = 5;
					SleepEx(10, 1);
					goto IL_0020;
					IL_000b:
					num2 = 2;
					tickCount = GetTickCount();
					goto IL_0029;
					IL_0020:
					num2 = 6;
					Application.DoEvents();
					goto IL_0029;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 109;
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

	public static void ErrService(int ErrNum, string errsource, string ErrDesc)
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
				case 221:
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
							goto IL_005d;
						case 5:
							goto IL_0075;
						case 6:
							goto IL_0085;
						case 7:
							goto IL_008d;
						case 8:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 9:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0075:
					num2 = 5;
					Interaction.MsgBox(text, MsgBoxStyle.Critical, "SQLPathFinder Error");
					goto IL_0085;
					IL_0085:
					ProjectData.ClearProjectError();
					num3 = 0;
					goto IL_008d;
					IL_005d:
					num2 = 4;
					text = text + "." + General_Procedures.Get_UI("errhelp0");
					goto IL_0075;
					IL_008d:
					num2 = 7;
					Information.Err().Clear();
					break;
					IL_000b:
					num2 = 2;
					text = "The following Error occurred: " + Globals_Renamed.CRLF + Globals_Renamed.CRLF;
					goto IL_0022;
					IL_0022:
					num2 = 3;
					text = text + errsource + " : " + ErrDesc + " (Number: " + Conversions.ToString(ErrNum) + ")";
					goto IL_005d;
					end_IL_0001_2:
					break;
				}
				num2 = 8;
				Cursor.Current = Cursors.Default;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 221;
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

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class GridModule
{
	public static int CountInVisibleBT(ref DataGridView MyGrid, int StartIdx, int EndIdx)
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
				case 134:
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
							goto IL_0022;
						case 6:
							goto IL_0040;
						case 7:
							goto IL_0046;
						default:
							goto end_IL_0001;
						case 8:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_0022:
					num2 = 5;
					if (!MyGrid.Rows[num5].Visible)
					{
						goto IL_0040;
					}
					goto IL_0046;
					IL_0040:
					num2 = 6;
					num6 = checked(num6 + 1);
					goto IL_0046;
					IL_004e:
					if (num5 > num7)
					{
						goto end_IL_0001_2;
					}
					goto IL_0022;
					IL_0046:
					num2 = 7;
					num5 = checked(num5 + 1);
					goto IL_004e;
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
					num7 = EndIdx;
					num5 = StartIdx;
					goto IL_004e;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 134;
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
		return num6;
	}

	public static void Clear_A_Grid(ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
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
					case 215:
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
								goto IL_0037;
							case 7:
								goto IL_005d;
							case 8:
								goto IL_0083;
							case 9:
								goto IL_0091;
							default:
								goto end_IL_0001;
							case 10:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0037:
						num2 = 6;
						MyGrid.Rows[num5].Cells[num6].Value = "";
						goto IL_005d;
						IL_005d:
						num2 = 7;
						MyGrid.Rows[num5].Cells[num6].Tag = "";
						goto IL_0083;
						IL_0091:
						num2 = 9;
						num5++;
						goto IL_0098;
						IL_0083:
						num2 = 8;
						num6++;
						goto IL_008b;
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
						num7 = MyGrid.RowCount - 1;
						num5 = 0;
						goto IL_0098;
						IL_0098:
						if (num5 > num7)
						{
							goto end_IL_0001_2;
						}
						goto IL_0025;
						IL_0025:
						num2 = 5;
						num8 = MyGrid.ColumnCount - 1;
						num6 = 0;
						goto IL_008b;
						IL_008b:
						if (num6 <= num8)
						{
							goto IL_0037;
						}
						goto IL_0091;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 215;
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

	public static void List_Drag_Drop(string MyMode, ref ListBox MyListBox, ref string l_DragSource, ref int l_DragIdx, DragEventArgs e = null)
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
					goto IL_000c;
				case 581:
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
							goto IL_004d;
						case 5:
							goto IL_0058;
						case 6:
							goto IL_0063;
						case 7:
							goto IL_0073;
						case 9:
							goto IL_008f;
						case 10:
							goto IL_00aa;
						case 12:
							goto IL_00ba;
						case 15:
							goto IL_00cd;
						case 16:
							goto IL_00d3;
						case 17:
							goto IL_00f1;
						case 18:
							goto IL_0117;
						case 19:
							goto IL_0129;
						case 20:
							goto IL_0137;
						case 22:
							goto IL_0161;
						case 21:
						case 23:
						case 24:
							goto IL_018a;
						case 25:
						case 26:
						case 27:
							goto IL_01a5;
						case 28:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 3:
						case 8:
						case 11:
						case 13:
						case 14:
						case 29:
						case 30:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0058:
					num2 = 5;
					l_DragIdx = MyListBox.SelectedIndex;
					goto IL_0063;
					IL_0063:
					num2 = 6;
					if (l_DragIdx == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_0073;
					IL_004d:
					num2 = 4;
					l_DragSource = MyListBox.Name;
					goto IL_0058;
					IL_0073:
					num2 = 7;
					MyListBox.DoDragDrop(MyListBox.Text, DragDropEffects.All);
					goto end_IL_0001_3;
					IL_000c:
					num2 = 2;
					switch (Strings.Trim(Strings.UCase(MyMode)))
					{
					case "MD":
						break;
					case "DO":
						goto IL_008f;
					case "DD":
						goto IL_00cd;
					default:
						goto end_IL_0001_3;
					}
					goto IL_004d;
					IL_00cd:
					num2 = 15;
					num5 = -1;
					goto IL_00d3;
					IL_00d3:
					num2 = 16;
					if (Operators.CompareString(l_DragSource, MyListBox.Name, TextCompare: false) == 0)
					{
						goto IL_00f1;
					}
					goto IL_01a5;
					IL_00f1:
					num2 = 17;
					num5 = MyListBox.IndexFromPoint(MyListBox.PointToClient(new Point(e.X, e.Y)));
					goto IL_0117;
					IL_0117:
					num2 = 18;
					if (num5 != l_DragIdx)
					{
						goto IL_0129;
					}
					goto IL_01a5;
					IL_0129:
					num2 = 19;
					if (num5 == -1)
					{
						goto IL_0137;
					}
					goto IL_0161;
					IL_0137:
					num2 = 20;
					MyListBox.Items.Add(RuntimeHelpers.GetObjectValue(e.Data.GetData(DataFormats.Text)));
					goto IL_018a;
					IL_0161:
					num2 = 22;
					MyListBox.Items.Insert(num5, RuntimeHelpers.GetObjectValue(e.Data.GetData(DataFormats.Text)));
					goto IL_018a;
					IL_018a:
					num2 = 24;
					MyListBox.Items.RemoveAt(MyListBox.SelectedIndex);
					goto IL_01a5;
					IL_01a5:
					num2 = 27;
					l_DragSource = "";
					break;
					IL_008f:
					num2 = 9;
					if (Operators.CompareString(l_DragSource, MyListBox.Name, TextCompare: false) == 0)
					{
						goto IL_00aa;
					}
					goto IL_00ba;
					IL_00aa:
					num2 = 10;
					e.Effect = DragDropEffects.Move;
					goto end_IL_0001_3;
					IL_00ba:
					num2 = 12;
					e.Effect = DragDropEffects.None;
					goto end_IL_0001_3;
					end_IL_0001_2:
					break;
				}
				num2 = 28;
				l_DragIdx = -1;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 581;
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

	public static void Combo_Click(ref DataGridView MyGrid, ref ComboBox MyCombo, ref short f_Row, ref short f_col, short Special = 0, string MyChar = "")
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		short num5 = default(short);
		string text = default(string);
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
					case 443:
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
								goto IL_0029;
							case 6:
								goto IL_003a;
							case 7:
								goto IL_004a;
							case 8:
								goto IL_0057;
							case 10:
								goto IL_0093;
							case 11:
								goto IL_00bb;
							case 12:
								goto IL_00d3;
							case 13:
								goto IL_00ec;
							case 15:
								goto IL_010c;
							case 14:
							case 16:
								goto IL_011c;
							case 9:
							case 17:
							case 18:
							case 19:
								goto IL_012c;
							case 20:
								goto IL_0138;
							case 21:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 22:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_010c:
						num2 = 15;
						num5 = (short)unchecked(num5 + 1);
						goto IL_0116;
						IL_012c:
						num2 = 19;
						MyCombo.Visible = true;
						goto IL_0138;
						IL_011c:
						num2 = 16;
						MyCombo.SelectedIndex = num5;
						goto IL_012c;
						IL_0138:
						num2 = 20;
						MyCombo.BringToFront();
						break;
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
						f_Row = (short)MyGrid.CurrentCell.RowIndex;
						goto IL_0029;
						IL_0029:
						num2 = 5;
						f_col = (short)MyGrid.CurrentCell.ColumnIndex;
						goto IL_003a;
						IL_003a:
						num2 = 6;
						Grid_Combo_Move(ref MyGrid, ref MyCombo, f_Row, f_col, Special);
						goto IL_004a;
						IL_004a:
						num2 = 7;
						if (Special != 0)
						{
							goto IL_0057;
						}
						goto IL_0093;
						IL_0057:
						num2 = 8;
						MyCombo.Text = Conversions.ToString(Operators.ConcatenateObject(MyGrid.Rows[f_Row].Cells[f_col].Value, MyChar));
						goto IL_012c;
						IL_0093:
						num2 = 10;
						text = Conversions.ToString(MyGrid.Rows[f_Row].Cells[f_col].Value);
						goto IL_00bb;
						IL_00bb:
						num2 = 11;
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							goto IL_00d3;
						}
						goto IL_012c;
						IL_00d3:
						num2 = 12;
						num6 = (short)(MyCombo.Items.Count - 1);
						num5 = 0;
						goto IL_0116;
						IL_0116:
						if (num5 <= num6)
						{
							goto IL_00ec;
						}
						goto IL_011c;
						IL_00ec:
						num2 = 13;
						if (!Operators.ConditionalCompareObjectEqual(MyCombo.Items[num5], text, TextCompare: false))
						{
							goto IL_010c;
						}
						goto IL_011c;
						end_IL_0001_2:
						break;
					}
					num2 = 21;
					MyCombo.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 443;
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

	public static int CountRowsVisible(ref DataGridView MyGrid)
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
				checked
				{
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 181:
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
							case 6:
								goto IL_0027;
							case 7:
								goto IL_002c;
							case 8:
								goto IL_003e;
							case 9:
								goto IL_0059;
							case 10:
								goto IL_0060;
							default:
								goto end_IL_0001;
							case 4:
							case 5:
							case 11:
							case 12:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_003e:
						num2 = 8;
						if (MyGrid.Rows[num5].Visible)
						{
							goto IL_0059;
						}
						goto IL_0060;
						IL_0059:
						num2 = 9;
						num6++;
						goto IL_0060;
						IL_0069:
						if (num5 > num7)
						{
							goto end_IL_0001_2;
						}
						goto IL_003e;
						IL_0060:
						num2 = 10;
						num5++;
						goto IL_0069;
						IL_000b:
						num2 = 2;
						num6 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						if (MyGrid.RowCount == 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_0027;
						IL_0027:
						num2 = 6;
						num5 = 0;
						goto IL_002c;
						IL_002c:
						num2 = 7;
						num7 = MyGrid.RowCount - 1;
						num5 = 0;
						goto IL_0069;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 181;
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
		return num6;
	}

	private static void Grid_Combo_Move(ref DataGridView MyGrid, ref ComboBox MyCombo, short f_Row, short f_col, short Special = 0)
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
						errsource = "GridModule - Grid_Combo_Move";
						float num3 = 0f;
						float num4 = 0f;
						short num5 = 0;
						int horizontalScrollingOffset = MyGrid.HorizontalScrollingOffset;
						if (Special == 1)
						{
							MyCombo.Width = MyGrid.Columns[f_col].Width - 22;
						}
						else if (Operators.CompareString(MyGrid.Name, "GridColor", TextCompare: false) == 0)
						{
							MyCombo.Width = MyGrid.Columns[f_col].Width - 19;
						}
						else
						{
							MyCombo.Width = MyGrid.Columns[f_col].Width;
						}
						MyCombo.Height = MyGrid.Rows[f_Row].Height;
						if (Special == 1)
						{
							num3 = MyGrid.Left + 2;
							num4 = MyGrid.Top + 2;
						}
						else if (Operators.CompareString(MyGrid.Name, "GridColor", TextCompare: false) == 0)
						{
							num3 = MyGrid.Left + MyGrid.RowHeadersWidth + 2;
							num4 = MyGrid.Top + MyGrid.ColumnHeadersHeight + 2;
						}
						else if (Special == 3)
						{
							num3 = MyGrid.Left + MyGrid.RowHeadersWidth - horizontalScrollingOffset;
							num4 = MyGrid.Top + MyGrid.ColumnHeadersHeight;
						}
						else
						{
							num3 = MyGrid.Left + MyGrid.RowHeadersWidth;
							num4 = MyGrid.Top + MyGrid.ColumnHeadersHeight;
						}
						short num6 = (short)(MyGrid.CurrentCell.ColumnIndex - 1);
						for (num5 = 0; num5 <= num6; num5 = (short)unchecked(num5 + 1))
						{
							if (MyGrid.Columns[num5].Visible)
							{
								num3 += (float)MyGrid.Columns[num5].Width;
							}
						}
						short num7 = (short)MyGrid.FirstDisplayedScrollingRowIndex;
						short num8 = (short)(MyGrid.CurrentCell.RowIndex - 1);
						for (num5 = num7; num5 <= num8; num5 = (short)unchecked(num5 + 1))
						{
							if (MyGrid.Rows[num5].Visible)
							{
								num4 += (float)MyGrid.Rows[num5].Height;
							}
						}
						MyCombo.SetBounds((int)Math.Round(num3), (int)Math.Round(num4), 0, 0, BoundsSpecified.Location);
						goto end_IL_0001;
					}
					case 645:
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
				try0001_dispatch = 645;
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

	public static void Set_Grid_TopRow(ref DataGridView MyGrid)
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
							int num4 = unchecked(num + 1);
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_000b;
							case 4:
								goto IL_0022;
							case 5:
								goto IL_0033;
							case 6:
								goto IL_003f;
							case 8:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 3:
							case 7:
							case 9:
							case 10:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0022:
						num2 = 4;
						num5 = (short)MyGrid.CurrentCell.RowIndex;
						goto IL_0033;
						IL_0033:
						num2 = 5;
						num6 = (short)MyGrid.DisplayedRowCount(includePartialRow: false);
						goto IL_003f;
						IL_000b:
						num2 = 2;
						if (MyGrid.RowCount <= 0)
						{
							goto end_IL_0001_3;
						}
						goto IL_0022;
						IL_003f:
						num2 = 6;
						if (!((num5 >= MyGrid.FirstDisplayedScrollingRowIndex) & (num5 < MyGrid.FirstDisplayedScrollingRowIndex + num6)))
						{
							break;
						}
						goto end_IL_0001_3;
						end_IL_0001_2:
						break;
					}
					num2 = 8;
					MyGrid.FirstDisplayedScrollingRowIndex = num5;
					break;
				}
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

	public static void Grid_Up(ref DataGridView MyGrid)
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
					{
						int num3 = 0;
						object obj = null;
						object obj2 = null;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						Color white = Color.White;
						Color white2 = Color.White;
						Color white3 = Color.White;
						Color white4 = Color.White;
						bool flag = false;
						int num7 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						if (CountRowsVisible(ref MyGrid) <= 0 || Information.IsNothing(MyGrid.CurrentCell.RowIndex))
						{
							goto end_IL_0001;
						}
						num5 = MyGrid.CurrentCell.RowIndex;
						num6 = MyGrid.FirstDisplayedScrollingRowIndex;
						num4 = MyGrid.DisplayedRowCount(includePartialRow: false);
						num7 = CountInVisibleBT(ref MyGrid, num6, num5);
						if (((num5 >= num6) & (num5 <= num6 + num4 + num7)) && num5 > 0)
						{
							white = MyGrid.Rows[num5].DefaultCellStyle.BackColor;
							white2 = MyGrid.Rows[num5 - 1].DefaultCellStyle.BackColor;
							int num8 = MyGrid.ColumnCount - 1;
							for (num3 = 0; num3 <= num8; num3++)
							{
								obj = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num5].Cells[num3].Value);
								obj2 = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num5 - 1].Cells[num3].Value);
								MyGrid.Rows[num5].Cells[num3].Value = RuntimeHelpers.GetObjectValue(obj2);
								MyGrid.Rows[num5 - 1].Cells[num3].Value = RuntimeHelpers.GetObjectValue(obj);
								obj = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num5].Cells[num3].Tag);
								obj2 = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num5 - 1].Cells[num3].Tag);
								MyGrid.Rows[num5].Cells[num3].Tag = RuntimeHelpers.GetObjectValue(obj2);
								MyGrid.Rows[num5 - 1].Cells[num3].Tag = RuntimeHelpers.GetObjectValue(obj);
								white3 = MyGrid[num3, num5].Style.BackColor;
								white4 = MyGrid[num3, num5 - 1].Style.BackColor;
								MyGrid[num3, num5].Style.BackColor = white4;
								MyGrid[num3, num5 - 1].Style.BackColor = white3;
								white3 = MyGrid[num3, num5].Style.ForeColor;
								white4 = MyGrid[num3, num5 - 1].Style.ForeColor;
								MyGrid[num3, num5].Style.ForeColor = white4;
								MyGrid[num3, num5 - 1].Style.ForeColor = white3;
								Font font = MyGrid[num3, num5].Style.Font;
								Font font2 = MyGrid[num3, num5 - 1].Style.Font;
								MyGrid[num3, num5].Style.Font = font2;
								MyGrid[num3, num5 - 1].Style.Font = font;
							}
							MyGrid.Rows[num5].DefaultCellStyle.BackColor = white2;
							MyGrid.Rows[num5 - 1].DefaultCellStyle.BackColor = white;
							BuildForm.SetMQColor(ref MyGrid, num5);
							BuildForm.SetMQColor(ref MyGrid, num5 - 1);
							flag = false;
							if (!MyGrid.Rows[num5 - 1].Visible)
							{
								flag = true;
								MyGrid.Rows[num5 - 1].Visible = true;
							}
							MyGrid.CurrentCell = MyGrid[0, num5 - 1];
							if (flag)
							{
								MyGrid.Rows[num5].Visible = false;
							}
							if (num6 > MyGrid.CurrentCell.RowIndex)
							{
								MyGrid.FirstDisplayedScrollingRowIndex = MyGrid.CurrentCell.RowIndex;
							}
						}
						goto end_IL_0001;
					}
					case 1164:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Unexpected error while moving row up (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Exclamation, "Unexpected Error");
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj3) when (obj3 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 1164;
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

	public static void Grid_Down(ref DataGridView MyGrid)
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
					{
						int num3 = 0;
						int num4 = -1;
						int num5 = 0;
						int num6 = 0;
						object obj = null;
						object obj2 = null;
						Color white = Color.White;
						Color white2 = Color.White;
						Color white3 = Color.White;
						Color white4 = Color.White;
						bool flag = false;
						int num7 = 0;
						ProjectData.ClearProjectError();
						num2 = 2;
						if (CountRowsVisible(ref MyGrid) <= 0 || Information.IsNothing(MyGrid.CurrentCell.RowIndex))
						{
							goto end_IL_0001;
						}
						num4 = MyGrid.CurrentCell.RowIndex;
						num6 = MyGrid.FirstDisplayedScrollingRowIndex;
						num5 = MyGrid.DisplayedRowCount(includePartialRow: false);
						num7 = CountInVisibleBT(ref MyGrid, num6, num4);
						if (((num4 >= num6) & (num4 <= num6 + num5 + num7)) && MyGrid.CurrentCell.RowIndex < MyGrid.RowCount - 1)
						{
							white = MyGrid.Rows[num4].DefaultCellStyle.BackColor;
							white2 = MyGrid.Rows[num4 + 1].DefaultCellStyle.BackColor;
							int num8 = MyGrid.ColumnCount - 1;
							for (num3 = 0; num3 <= num8; num3++)
							{
								obj = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num3].Value);
								obj2 = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4 + 1].Cells[num3].Value);
								MyGrid.Rows[num4].Cells[num3].Value = RuntimeHelpers.GetObjectValue(obj2);
								MyGrid.Rows[num4 + 1].Cells[num3].Value = RuntimeHelpers.GetObjectValue(obj);
								obj = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num3].Tag);
								obj2 = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4 + 1].Cells[num3].Tag);
								MyGrid.Rows[num4].Cells[num3].Tag = RuntimeHelpers.GetObjectValue(obj2);
								MyGrid.Rows[num4 + 1].Cells[num3].Tag = RuntimeHelpers.GetObjectValue(obj);
								white3 = MyGrid[num3, num4].Style.BackColor;
								white4 = MyGrid[num3, num4 + 1].Style.BackColor;
								MyGrid[num3, num4].Style.BackColor = white4;
								MyGrid[num3, num4 + 1].Style.BackColor = white3;
								white3 = MyGrid[num3, num4].Style.ForeColor;
								white4 = MyGrid[num3, num4 + 1].Style.ForeColor;
								MyGrid[num3, num4].Style.ForeColor = white4;
								MyGrid[num3, num4 + 1].Style.ForeColor = white3;
								Font font = MyGrid[num3, num4].Style.Font;
								Font font2 = MyGrid[num3, num4 + 1].Style.Font;
								MyGrid[num3, num4].Style.Font = font2;
								MyGrid[num3, num4 + 1].Style.Font = font;
							}
							MyGrid.Rows[num4].DefaultCellStyle.BackColor = white2;
							MyGrid.Rows[num4 + 1].DefaultCellStyle.BackColor = white;
							BuildForm.SetMQColor(ref MyGrid, num4);
							BuildForm.SetMQColor(ref MyGrid, num4 + 1);
							flag = false;
							if (!MyGrid.Rows[num4 + 1].Visible)
							{
								flag = true;
								MyGrid.Rows[num4 + 1].Visible = true;
							}
							MyGrid.CurrentCell = MyGrid[0, num4 + 1];
							if (flag)
							{
								MyGrid.Rows[num4].Visible = false;
							}
							if (num6 > MyGrid.RowCount - 1)
							{
								MyGrid.FirstDisplayedScrollingRowIndex = MyGrid.CurrentCell.RowIndex;
							}
							else if (num6 + num5 + num7 < MyGrid.CurrentCell.RowIndex)
							{
								MyGrid.FirstDisplayedScrollingRowIndex = MyGrid.CurrentCell.RowIndex - num5 - num7;
							}
						}
						goto end_IL_0001;
					}
					case 1205:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Unexpected error while moving row down (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Exclamation, "Unexpected Error");
							Information.Err().Clear();
							goto end_IL_0001;
						}
						break;
					}
				}
			}
			catch (object obj3) when (obj3 is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj3);
				try0001_dispatch = 1205;
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

	public static void Grid_Delete(ref DataGridView MyGrid)
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
						if (CountRowsVisible(ref MyGrid) <= 0)
						{
							goto end_IL_0001;
						}
						ProjectData.ClearProjectError();
						num2 = 2;
						if (!Information.IsNothing(MyGrid.CurrentCell.RowIndex))
						{
							short num3 = (short)MyGrid.CurrentCell.RowIndex;
							short num4 = (short)MyGrid.FirstDisplayedScrollingRowIndex;
							short num5 = (short)MyGrid.DisplayedRowCount(includePartialRow: false);
							short num6 = (short)MyGrid.RowCount;
							if ((num3 >= num4) & (num3 <= (short)unchecked(num4 + num5)))
							{
								MyGrid.Rows.Remove(MyGrid.CurrentRow);
							}
						}
						goto end_IL_0001;
					case 206:
						num = -1;
						switch (num2)
						{
						case 2:
							Interaction.MsgBox("Unexpected error while deleting row (" + Conversion.ErrorToString() + ").", MsgBoxStyle.Exclamation, "Unexpected Error");
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
				try0001_dispatch = 206;
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

	public static void Grid_Delete_Multi(ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int count = default(int);
		int num6 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				int num7;
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
							goto IL_001a;
						case 4:
							goto end_IL_0001_2;
						case 7:
							goto IL_0049;
						case 8:
							goto IL_004e;
						case 9:
							goto IL_0053;
						case 10:
							goto IL_0066;
						case 12:
							goto IL_0080;
						case 11:
						case 13:
						case 14:
							goto IL_0087;
						case 15:
							goto IL_0095;
						case 16:
							goto IL_00a3;
						case 17:
							goto IL_00bf;
						case 18:
						case 19:
							goto IL_00df;
						default:
							goto end_IL_0001;
						case 5:
						case 6:
						case 20:
						case 21:
						case 22:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_00a3:
					num2 = 16;
					if (MyGrid.SelectedRows[num5].Visible)
					{
						goto IL_00bf;
					}
					goto IL_00df;
					IL_00bf:
					num2 = 17;
					MyGrid.Rows.Remove(MyGrid.SelectedRows[num5]);
					goto IL_00df;
					IL_00e8:
					if (num5 < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_00a3;
					IL_00df:
					num2 = 19;
					num5 = checked(num5 + -1);
					goto IL_00e8;
					IL_000b:
					num2 = 2;
					count = MyGrid.SelectedRows.Count;
					goto IL_001a;
					IL_001a:
					num2 = 3;
					if (count <= 0)
					{
						break;
					}
					goto IL_0049;
					IL_0049:
					num2 = 7;
					num5 = 0;
					goto IL_004e;
					IL_004e:
					num2 = 8;
					num6 = 0;
					goto IL_0053;
					IL_0053:
					num2 = 9;
					if (count == MyGrid.RowCount)
					{
						goto IL_0066;
					}
					goto IL_0080;
					IL_0066:
					num2 = 10;
					num6 = (int)Interaction.MsgBox("Are you sure you wish to remove all visible, selected rows?", MsgBoxStyle.YesNoCancel | MsgBoxStyle.Question, "Remove Selected Rows");
					goto IL_0087;
					IL_0080:
					num2 = 12;
					num6 = 6;
					goto IL_0087;
					IL_0087:
					num2 = 14;
					if (num6 != 6)
					{
						goto end_IL_0001_3;
					}
					goto IL_0095;
					IL_0095:
					num2 = 15;
					num7 = checked(count - 1);
					num5 = num7;
					goto IL_00e8;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				Interaction.MsgBox("To remove rows, select one or more rows (i.e., highlight the complete row) and click the Delete button. Use the Ctrl or Shift keys to select multiple rows", MsgBoxStyle.Exclamation, "Row Deletion");
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
	}

	public static Point Get_Cell_Point(ref DataGridView MyGrid, int CurrCol, int CurrRow, int MyX, int MyY)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		Point result = default(Point);
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
						case 3:
							goto IL_0028;
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
					result = MyGrid.PointToScreen(MyGrid.GetCellDisplayRectangle(CurrCol, CurrRow, cutOverflow: false).Location);
					goto IL_0028;
					IL_0028:
					num2 = 3;
					result.X = MyX;
					break;
					end_IL_0001_2:
					break;
				}
				num2 = 4;
				checked
				{
					result.Y += MyY;
					break;
				}
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

	public static void Grid_Row_Sel(ref DataGridView MyGrid)
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
							goto IL_000b;
						case 3:
							goto IL_000f;
						case 4:
							goto IL_0021;
						case 5:
							goto IL_0030;
						case 6:
							goto IL_003f;
						case 7:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 8:
						case 9:
						case 10:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_0021:
					num2 = 4;
					num5 = MyGrid.CurrentCell.RowIndex;
					goto IL_0030;
					IL_0030:
					num2 = 5;
					if (num5 == -1)
					{
						goto end_IL_0001_3;
					}
					goto IL_003f;
					IL_000f:
					num2 = 3;
					if (MyGrid.RowCount <= 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0021;
					IL_003f:
					num2 = 6;
					MyGrid.Rows[num5].Selected = true;
					break;
					IL_000b:
					num2 = 2;
					num5 = -1;
					goto IL_000f;
					end_IL_0001_2:
					break;
				}
				num2 = 7;
				MyGrid.CurrentCell = MyGrid[0, num5];
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
	}

	public static void Number_Grid(ref DataGridView MyGrid)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
		int rowCount = default(int);
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
								goto IL_001a;
							case 6:
								goto IL_002c;
							case 7:
								goto IL_0038;
							case 8:
								goto IL_005e;
							default:
								goto end_IL_0001;
							case 5:
							case 9:
								goto end_IL_0001_2;
							}
							goto default;
						}
						IL_0064:
						if (num5 > num6)
						{
							goto end_IL_0001_2;
						}
						goto IL_0038;
						IL_0038:
						num2 = 7;
						MyGrid.Rows[num5].HeaderCell.Value = (num5 + 1).ToString();
						goto IL_005e;
						IL_002c:
						num2 = 6;
						num6 = rowCount - 1;
						num5 = 0;
						goto IL_0064;
						IL_005e:
						num2 = 8;
						num5++;
						goto IL_0064;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						rowCount = MyGrid.RowCount;
						goto IL_001a;
						IL_001a:
						num2 = 4;
						if (rowCount <= 0)
						{
							goto end_IL_0001_2;
						}
						goto IL_002c;
						end_IL_0001:
						break;
					}
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 159;
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

	public static void InitiateDragDrop(ref DataGridView MyGrid, ref MouseEventArgs e)
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
					int num3 = -1;
					if (MyGrid.RowCount != 0)
					{
						num3 = MyGrid.HitTest(e.X, e.Y).RowIndex;
						if (num3 > -1)
						{
							MyGrid.DoDragDrop(num3, DragDropEffects.Move);
						}
					}
					goto end_IL_0001;
				}
				case 129:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "GridModule - InitiateDragDrop", Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 129;
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

	public static void GridDragDrop(ref DataGridView MyGrid, ref DragEventArgs e, bool DoColor, bool DoTag = false)
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
					int num3 = -1;
					int num4 = -1;
					Point p = new Point(e.X, e.Y);
					Type type = MyGrid.Rows.GetType();
					Point point = MyGrid.PointToClient(p);
					int num5 = 0;
					bool flag = false;
					num3 = MyGrid.HitTest(point.X, point.Y).RowIndex;
					num4 = Convert.ToInt32(RuntimeHelpers.GetObjectValue(e.Data.GetData(Type.GetType("System.Int32"))));
					if (num3 == -1 || num3 == num4)
					{
						goto end_IL_0001;
					}
					MyGrid.Rows.Insert(num3);
					checked
					{
						if (num4 > num3)
						{
							num4++;
							flag = true;
						}
						int num6 = MyGrid.ColumnCount - 1;
						for (num5 = 0; num5 <= num6; num5++)
						{
							MyGrid.Rows[num3].Cells[num5].Value = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num5].Value);
							if (DoTag)
							{
								MyGrid.Rows[num3].Cells[num5].Tag = RuntimeHelpers.GetObjectValue(MyGrid.Rows[num4].Cells[num5].Tag);
							}
						}
						MyGrid.Rows.RemoveAt(num4);
						Number_Grid(ref MyGrid);
						MyGrid.CurrentCell = MyGrid[MyGrid.CurrentCell.ColumnIndex, num3];
						if (DoColor)
						{
							if (!flag)
							{
								num3--;
							}
							BuildForm.SetMQColor(ref MyGrid, num3);
						}
						goto end_IL_0001;
					}
				}
				case 488:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "GridModule - GridDragDrop", Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 488;
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

	public static void GridDragOver(ref DataGridView MyGrid, ref DragEventArgs e)
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
					int num3 = -1;
					if (e.Data.GetDataPresent("System.Int32", autoConvert: true))
					{
						Point point = MyGrid.PointToClient(new Point(e.X, e.Y));
						num3 = MyGrid.HitTest(point.X, point.Y).RowIndex;
						if (num3 != -1)
						{
							MyGrid.CurrentCell = MyGrid[MyGrid.CurrentCell.ColumnIndex, num3];
							e.Effect = DragDropEffects.Move;
						}
						else
						{
							e.Effect = DragDropEffects.None;
						}
					}
					goto end_IL_0001;
				}
				case 209:
					num = -1;
					switch (num2)
					{
					case 2:
						Support.ErrService(Information.Err().Number, "GridModule - GridDragOver", Information.Err().Description);
						Information.Err().Clear();
						goto end_IL_0001;
					}
					break;
				}
			}
			catch (object obj) when (obj is Exception && num2 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 209;
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

	public static void Init_Colors()
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
				case 893:
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
							goto IL_0022;
						case 5:
							goto IL_0039;
						case 6:
							goto IL_0044;
						case 7:
							goto IL_0056;
						case 8:
							goto IL_0068;
						case 9:
							goto IL_007a;
						case 10:
							goto IL_008d;
						case 11:
							goto IL_00a0;
						case 12:
							goto IL_00b3;
						case 13:
							goto IL_00c6;
						case 14:
							goto IL_00d9;
						case 15:
							goto IL_00ec;
						case 16:
							goto IL_0100;
						case 17:
							goto IL_0114;
						case 18:
							goto IL_0128;
						case 19:
							goto IL_013c;
						case 20:
							goto IL_0150;
						case 21:
							goto IL_0164;
						case 22:
							goto IL_0178;
						case 23:
							goto IL_018c;
						case 24:
							goto IL_01a0;
						case 25:
							goto IL_01b4;
						case 26:
							goto IL_01c8;
						case 27:
							goto IL_01dc;
						case 28:
							goto IL_01f0;
						case 29:
							goto IL_0204;
						case 30:
							goto IL_0218;
						case 31:
							goto IL_022c;
						case 32:
							goto IL_0240;
						case 33:
							goto IL_0254;
						case 34:
							goto IL_0268;
						case 35:
							goto IL_027c;
						case 36:
							goto IL_0290;
						case 37:
							goto IL_02a4;
						case 38:
							goto end_IL_0001_2;
						default:
							goto end_IL_0001;
						case 39:
							goto end_IL_0001_3;
						}
						goto default;
					}
					IL_02a4:
					num2 = 37;
					Globals_Renamed.gStdColors[31] = Color.Peru;
					break;
					IL_0022:
					num2 = 4;
					Globals_Renamed.gGridColor[num5].Color = Color.White;
					goto IL_0039;
					IL_0290:
					num2 = 36;
					Globals_Renamed.gStdColors[30] = Color.BurlyWood;
					goto IL_02a4;
					IL_0039:
					num2 = 5;
					num5 = checked(num5 + 1);
					goto IL_003f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					IL_000f:
					num2 = 3;
					num6 = Information.UBound(Globals_Renamed.gGridColor);
					num5 = 0;
					goto IL_003f;
					IL_003f:
					if (num5 <= num6)
					{
						goto IL_0022;
					}
					goto IL_0044;
					IL_0044:
					num2 = 6;
					Globals_Renamed.gStdColors[0] = Color.White;
					goto IL_0056;
					IL_0056:
					num2 = 7;
					Globals_Renamed.gStdColors[1] = Color.Beige;
					goto IL_0068;
					IL_0068:
					num2 = 8;
					Globals_Renamed.gStdColors[2] = Color.LightGray;
					goto IL_007a;
					IL_007a:
					num2 = 9;
					Globals_Renamed.gStdColors[3] = Color.Gainsboro;
					goto IL_008d;
					IL_008d:
					num2 = 10;
					Globals_Renamed.gStdColors[4] = Color.Lavender;
					goto IL_00a0;
					IL_00a0:
					num2 = 11;
					Globals_Renamed.gStdColors[5] = Color.Thistle;
					goto IL_00b3;
					IL_00b3:
					num2 = 12;
					Globals_Renamed.gStdColors[6] = Color.LightSteelBlue;
					goto IL_00c6;
					IL_00c6:
					num2 = 13;
					Globals_Renamed.gStdColors[7] = Color.LightBlue;
					goto IL_00d9;
					IL_00d9:
					num2 = 14;
					Globals_Renamed.gStdColors[8] = Color.PaleTurquoise;
					goto IL_00ec;
					IL_00ec:
					num2 = 15;
					Globals_Renamed.gStdColors[9] = Color.LightCyan;
					goto IL_0100;
					IL_0100:
					num2 = 16;
					Globals_Renamed.gStdColors[10] = Color.Honeydew;
					goto IL_0114;
					IL_0114:
					num2 = 17;
					Globals_Renamed.gStdColors[11] = Color.Teal;
					goto IL_0128;
					IL_0128:
					num2 = 18;
					Globals_Renamed.gStdColors[12] = Color.Aqua;
					goto IL_013c;
					IL_013c:
					num2 = 19;
					Globals_Renamed.gStdColors[13] = Color.PaleGreen;
					goto IL_0150;
					IL_0150:
					num2 = 20;
					Globals_Renamed.gStdColors[14] = Color.LightGreen;
					goto IL_0164;
					IL_0164:
					num2 = 21;
					Globals_Renamed.gStdColors[15] = Color.Yellow;
					goto IL_0178;
					IL_0178:
					num2 = 22;
					Globals_Renamed.gStdColors[16] = Color.LightGoldenrodYellow;
					goto IL_018c;
					IL_018c:
					num2 = 23;
					Globals_Renamed.gStdColors[17] = Color.Cornsilk;
					goto IL_01a0;
					IL_01a0:
					num2 = 24;
					Globals_Renamed.gStdColors[18] = Color.PapayaWhip;
					goto IL_01b4;
					IL_01b4:
					num2 = 25;
					Globals_Renamed.gStdColors[19] = Color.BlanchedAlmond;
					goto IL_01c8;
					IL_01c8:
					num2 = 26;
					Globals_Renamed.gStdColors[20] = Color.Bisque;
					goto IL_01dc;
					IL_01dc:
					num2 = 27;
					Globals_Renamed.gStdColors[21] = Color.PeachPuff;
					goto IL_01f0;
					IL_01f0:
					num2 = 28;
					Globals_Renamed.gStdColors[22] = Color.MistyRose;
					goto IL_0204;
					IL_0204:
					num2 = 29;
					Globals_Renamed.gStdColors[23] = Color.LightPink;
					goto IL_0218;
					IL_0218:
					num2 = 30;
					Globals_Renamed.gStdColors[24] = Color.NavajoWhite;
					goto IL_022c;
					IL_022c:
					num2 = 31;
					Globals_Renamed.gStdColors[25] = Color.Moccasin;
					goto IL_0240;
					IL_0240:
					num2 = 32;
					Globals_Renamed.gStdColors[26] = Color.Wheat;
					goto IL_0254;
					IL_0254:
					num2 = 33;
					Globals_Renamed.gStdColors[27] = Color.PaleGoldenrod;
					goto IL_0268;
					IL_0268:
					num2 = 34;
					Globals_Renamed.gStdColors[28] = Color.Khaki;
					goto IL_027c;
					IL_027c:
					num2 = 35;
					Globals_Renamed.gStdColors[29] = Color.DarkSalmon;
					goto IL_0290;
					end_IL_0001_2:
					break;
				}
				num2 = 38;
				Globals_Renamed.gStdColors[32] = Color.Orange;
				break;
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 893;
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

	public static void Init_Grid_For_Colors(ref DataGridView MyGrid, ref ComboBox CmbColor)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		int num5 = default(int);
		int num6 = default(int);
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
					case 610:
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
								goto IL_001b;
							case 5:
								goto IL_002c;
							case 6:
								goto IL_0061;
							case 7:
								goto IL_006c;
							case 8:
								goto IL_0091;
							case 9:
								goto IL_00b8;
							case 10:
								goto IL_00e0;
							case 11:
								goto IL_0108;
							case 12:
								goto IL_0112;
							case 13:
								goto IL_0122;
							case 14:
								goto IL_0138;
							case 15:
								goto IL_0158;
							case 16:
								goto IL_0164;
							case 17:
								goto IL_0176;
							case 18:
								goto IL_01a1;
							case 19:
								goto IL_01e2;
							case 20:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 21:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_0158:
						num2 = 15;
						num5++;
						goto IL_015f;
						IL_002c:
						num2 = 5;
						MyGrid.Rows[num5].Cells[0].Value = "a" + Strings.Trim(Conversions.ToString(num5));
						goto IL_0061;
						IL_0138:
						num2 = 14;
						CmbColor.Items.Add(Globals_Renamed.gStdColors[num5]);
						goto IL_0158;
						IL_0061:
						num2 = 6;
						num5++;
						goto IL_0067;
						IL_000b:
						num2 = 2;
						num5 = 0;
						goto IL_000f;
						IL_000f:
						num2 = 3;
						MyGrid.RowCount = 36;
						goto IL_001b;
						IL_001b:
						num2 = 4;
						num6 = MyGrid.RowCount - 5;
						num5 = 0;
						goto IL_0067;
						IL_0067:
						if (num5 <= num6)
						{
							goto IL_002c;
						}
						goto IL_006c;
						IL_006c:
						num2 = 7;
						MyGrid.Rows[num5].Cells[0].Value = "SQLite";
						goto IL_0091;
						IL_0091:
						num2 = 8;
						MyGrid.Rows[num5 + 1].Cells[0].Value = "Text";
						goto IL_00b8;
						IL_00b8:
						num2 = 9;
						MyGrid.Rows[num5 + 2].Cells[0].Value = "Inline";
						goto IL_00e0;
						IL_00e0:
						num2 = 10;
						MyGrid.Rows[num5 + 3].Cells[0].Value = "All";
						goto IL_0108;
						IL_0108:
						num2 = 11;
						Number_Grid(ref MyGrid);
						goto IL_0112;
						IL_0112:
						num2 = 12;
						CmbColor.Items.Clear();
						goto IL_0122;
						IL_0122:
						num2 = 13;
						num7 = Information.UBound(Globals_Renamed.gStdColors) - 4;
						num5 = 0;
						goto IL_015f;
						IL_015f:
						if (num5 <= num7)
						{
							goto IL_0138;
						}
						goto IL_0164;
						IL_0164:
						num2 = 16;
						num8 = MyGrid.RowCount - 1;
						num5 = 0;
						goto IL_01e9;
						IL_01e9:
						if (num5 > num8)
						{
							break;
						}
						goto IL_0176;
						IL_0176:
						num2 = 17;
						MyGrid.Rows[num5].DefaultCellStyle.BackColor = Globals_Renamed.gGridColor[num5].Color;
						goto IL_01a1;
						IL_01a1:
						num2 = 18;
						MyGrid.Rows[num5].Cells[1].Value = MyGrid.Rows[num5].DefaultCellStyle.BackColor.Name;
						goto IL_01e2;
						IL_01e2:
						num2 = 19;
						num5++;
						goto IL_01e9;
						end_IL_0001_2:
						break;
					}
					num2 = 20;
					MyGrid.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 610;
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

	public static void Commit_Grid_Color(ref DataGridView MyGrid)
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
				case 126:
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
							goto IL_0049;
						default:
							goto end_IL_0001;
						case 6:
							goto end_IL_0001_2;
						}
						goto default;
					}
					IL_004f:
					if (num5 > num6)
					{
						goto end_IL_0001_2;
					}
					goto IL_0020;
					IL_0020:
					num2 = 4;
					Globals_Renamed.gGridColor[num5].Color = MyGrid.Rows[num5].DefaultCellStyle.BackColor;
					goto IL_0049;
					IL_000f:
					num2 = 3;
					num6 = checked(MyGrid.RowCount - 1);
					num5 = 0;
					goto IL_004f;
					IL_0049:
					num2 = 5;
					num5 = checked(num5 + 1);
					goto IL_004f;
					IL_000b:
					num2 = 2;
					num5 = 0;
					goto IL_000f;
					end_IL_0001:
					break;
				}
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0001_dispatch = 126;
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

	public static void CmbDrawItem(ref object Sender, DrawItemEventArgs e)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		Rectangle rect = default(Rectangle);
		Color color = default(Color);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				Brush white;
				object obj;
				switch (try0001_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = -2;
					goto IL_000b;
				case 438:
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
							goto IL_0032;
						case 6:
							goto IL_0062;
						case 7:
							goto IL_009d;
						case 8:
							goto IL_00a6;
						case 9:
							goto IL_00af;
						case 10:
							goto IL_00b9;
						case 11:
							goto IL_00d1;
						case 12:
							goto IL_00e9;
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
					IL_009d:
					num2 = 7;
					white = Brushes.White;
					goto IL_00a6;
					IL_00a6:
					num2 = 8;
					e.DrawBackground();
					goto IL_00af;
					IL_0062:
					num2 = 6;
					rect = checked(new Rectangle(2, e.Bounds.Top + 2, e.Bounds.Height, e.Bounds.Height - 5));
					goto IL_009d;
					IL_00af:
					num2 = 9;
					e.DrawFocusRectangle();
					goto IL_00b9;
					IL_000b:
					num2 = 2;
					e.DrawBackground();
					goto IL_0014;
					IL_0014:
					num2 = 3;
					e.DrawFocusRectangle();
					goto IL_001d;
					IL_001d:
					num2 = 4;
					if (e.Index < 0)
					{
						goto end_IL_0001_3;
					}
					goto IL_0032;
					IL_0032:
					num2 = 5;
					obj = ((ComboBox)Sender).Items[e.Index];
					color = ((obj != null) ? ((Color)obj) : default(Color));
					goto IL_0062;
					IL_00d1:
					num2 = 11;
					e.Graphics.FillRectangle(new SolidBrush(color), rect);
					goto IL_00e9;
					IL_00e9:
					num2 = 12;
					rect.Inflate(1, 1);
					break;
					IL_00b9:
					num2 = 10;
					e.Graphics.DrawRectangle(new Pen(color), rect);
					goto IL_00d1;
					end_IL_0001_2:
					break;
				}
				num2 = 13;
				checked
				{
					e.Graphics.DrawString(color.Name, ((ComboBox)Sender).Font, Brushes.Brown, e.Bounds.Height + 5, unchecked(checked(e.Bounds.Height - ((ComboBox)Sender).Font.Height) / 2) + e.Bounds.Top);
					break;
				}
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 438;
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

	public static void ComboColor_Click(ref DataGridView MyGrid, ref ComboBox MyCombo, ref short f_Row, ref short f_col)
	{
		int try0001_dispatch = -1;
		int num3 = default(int);
		int num = default(int);
		int num2 = default(int);
		string left = default(string);
		string right = default(string);
		short num5 = default(short);
		short num6 = default(short);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					object obj;
					switch (try0001_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = -2;
						goto IL_000b;
					case 371:
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
								goto IL_0043;
							case 8:
								goto IL_0052;
							case 9:
								goto IL_0076;
							case 10:
								goto IL_008f;
							case 11:
								goto IL_00c0;
							case 13:
								goto IL_00d7;
							case 12:
							case 14:
								goto IL_00e7;
							case 15:
								goto IL_00f4;
							case 16:
								goto IL_0100;
							case 17:
								goto end_IL_0001_2;
							default:
								goto end_IL_0001;
							case 18:
								goto end_IL_0001_3;
							}
							goto default;
						}
						IL_00f4:
						num2 = 15;
						MyCombo.Visible = true;
						goto IL_0100;
						IL_00c0:
						num2 = 11;
						if (Operators.CompareString(left, right, TextCompare: false) != 0)
						{
							goto IL_00d7;
						}
						goto IL_00e7;
						IL_00d7:
						num2 = 13;
						num5 = (short)unchecked(num5 + 1);
						goto IL_00e1;
						IL_00e7:
						num2 = 14;
						MyCombo.SelectedIndex = num5;
						goto IL_00f4;
						IL_000b:
						num2 = 2;
						right = "";
						goto IL_0013;
						IL_0013:
						num2 = 3;
						left = "";
						goto IL_001c;
						IL_001c:
						num2 = 4;
						num5 = 0;
						goto IL_0021;
						IL_0021:
						num2 = 5;
						f_Row = (short)MyGrid.CurrentCell.RowIndex;
						goto IL_0032;
						IL_0032:
						num2 = 6;
						f_col = (short)MyGrid.CurrentCell.ColumnIndex;
						goto IL_0043;
						IL_0043:
						num2 = 7;
						Grid_Combo_Move(ref MyGrid, ref MyCombo, f_Row, f_col, 0);
						goto IL_0052;
						IL_0052:
						num2 = 8;
						right = MyGrid.Rows[f_Row].DefaultCellStyle.BackColor.Name;
						goto IL_0076;
						IL_0076:
						num2 = 9;
						num6 = (short)(MyCombo.Items.Count - 1);
						num5 = 0;
						goto IL_00e1;
						IL_00e1:
						if (num5 <= num6)
						{
							goto IL_008f;
						}
						goto IL_00e7;
						IL_008f:
						num2 = 10;
						obj = MyCombo.Items[num5];
						left = ((obj != null) ? ((Color)obj) : default(Color)).Name;
						goto IL_00c0;
						IL_0100:
						num2 = 16;
						MyCombo.BringToFront();
						break;
						end_IL_0001_2:
						break;
					}
					num2 = 17;
					MyCombo.Focus();
					break;
				}
				end_IL_0001:;
			}
			catch (object obj2) when (obj2 is Exception && num3 != 0 && num == 0)
			{
				ProjectData.SetProjectError((Exception)obj2);
				try0001_dispatch = 371;
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

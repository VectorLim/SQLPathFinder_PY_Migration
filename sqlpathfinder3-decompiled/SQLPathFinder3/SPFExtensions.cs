using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class SPFExtensions
{
	public static ListViewItem FindItemWithTextCS(this ListView myList, string textToSearch)
	{
		ListViewItem result = null;
		foreach (ListViewItem item in myList.Items)
		{
			if (Operators.CompareString(item.Text, textToSearch, TextCompare: false) == 0)
			{
				result = item;
				break;
			}
		}
		return result;
	}
}

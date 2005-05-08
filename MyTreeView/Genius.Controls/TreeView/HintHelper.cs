using System;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls.TreeView
{
	/// <summary>
	/// Summary description for HintHelper.
	/// </summary>
	class HintHelper
	{
		public Node Node;
		public string Text;
		public int Column;

		public HintHelper(string aText, Node aNode, int aDisplayColumn)
		{
			Node = aNode;
			Text = aText;
			Column = aDisplayColumn;
		}
	}
}

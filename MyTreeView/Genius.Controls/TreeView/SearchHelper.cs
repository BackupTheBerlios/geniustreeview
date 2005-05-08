using System;
using System.Diagnostics;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	/// <summary>
	/// classe Helper dédiée à la recherche
	/// </summary>
	internal class SearchHelper
	{
		private GeniusTreeView	FOwner;
		private string			FBuffer;
		private Node			FLastSearchNode;
		private bool			FInit;
		private SearchDirectionEnum FCurrentDirection;

		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aOwner"></param>
		public SearchHelper(GeniusTreeView aOwner)
		{
			FOwner = aOwner;
			FBuffer = string.Empty;
			FCurrentDirection = FOwner.SearchDirection;
			FInit = false;
		}

		delegate Node GetNextNodeDelegate(Node aNode);

		private GetNextNodeDelegate CalculGetNextNode()
		{
			if (FOwner.IncrementalSearch == IncrementalSearchEnum.VisibleOnly)
			{
				if (FCurrentDirection == SearchDirectionEnum.Forward )
				{
					return new GetNextNodeDelegate(FOwner.NextVisibleNode);
				}
				else
				{
					return new GetNextNodeDelegate(FOwner.PrevVisibleNode);
				}
			}
			else
			{
				if (FCurrentDirection == SearchDirectionEnum.Forward )
				{
					return new GetNextNodeDelegate(FOwner.NextVisibleOrNotNode);
				}
				else
				{
					return new GetNextNodeDelegate(FOwner.PrevVisibleOrNotNode);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyChar"></param>
		public bool HandleIncrementalSearch(char keyChar)
		{
			Debug.WriteLine(String.Format("HandleIncrementalSearch({0}) -> {1}", keyChar, FBuffer));
			bool backSearch = false;
			bool foundMatch = false;
			Node Run;
			GetNextNodeDelegate GetNextNode;
			if (FOwner.IncrementalSearch != IncrementalSearchEnum.None)
			{
				if (keyChar != 0)
				{
					backSearch = keyChar == (char)Keys.Back;
					if (!backSearch || FBuffer.Length > 1)
					{
						switch(FOwner.SearchStart)
						{
							case SearchStartEnum.AlwaysStartOver :
								Run = null;
								break;
							case SearchStartEnum.FocusedNode :
								Run = (Node)FOwner.SelectedNode;
								break;
							default:
								Run = FLastSearchNode;
								break;
						}
						if (backSearch)
							ToggleDirection();
						Debug.WriteLine(String.Format("Direction : {0}", FCurrentDirection));
						GetNextNode = CalculGetNextNode();

						switch(FOwner.IncrementalSearch)
						{
							case IncrementalSearchEnum.All :
								if (Run == null)
									Run = (Node)FOwner.FirstVisible();
								else if (!FInit)
									Run = GetNextNode(Run);
								break;
							case IncrementalSearchEnum.VisibleOnly :
								if (Run == null)
									Run = (Node)FOwner.FirstVisible();
								else if (!Node.IsFullyVisible(Run))
									Run = Node.NextVisibleNode(Run);
								else if (!FInit)
									Run = GetNextNode(Run);
								break;
						}
						if (Run != null)
						{
							string newSearch;
							if (backSearch)	
								newSearch = FBuffer.Substring(0, FBuffer.Length-1);
							else
								newSearch = FBuffer + keyChar;
							Node Stop = Run;
							do
							{			
								if (FOwner.DoIncrementalSearch(Run, newSearch))
								{
									foundMatch = true;
									break;
								}
								Run = GetNextNode(Run);
							}
							while (Run != null && Run != Stop);
							if (foundMatch)
							{
								FBuffer = newSearch;
								FLastSearchNode = Run;
								FOwner.ShowNode(Run);
								FOwner.SelectNode(Run);
							}
						}
					}
					else if (backSearch)
					{
						ToggleDirection();
						Debug.WriteLine(String.Format("ToogleDirection : {0}", FCurrentDirection));
					}
				}
				if (!FInit && !backSearch)
					FInit = true;
				return true;
			}
			return false;
		}

		private void ToggleDirection()
		{
			if (FCurrentDirection == SearchDirectionEnum.Forward)
				FCurrentDirection = SearchDirectionEnum.Backward;
			else
				FCurrentDirection = SearchDirectionEnum.Forward;			
		}
	}
}

using System;
using System.Collections;
using System.Diagnostics;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	internal delegate void OnExpandInternalDelegate(Nodes Sender, ExpandEventArgs e);
	internal delegate void OnCollapseInternalDelegate(Nodes Sender, CollapseEventArgs e);
	internal delegate void OnNodeInternalDelegate(NodeEventArgs e);
	internal delegate void OnCheckInternalDelegate(Nodes Sender, CheckEventArgs e);
	internal delegate void OnUnCheckInternalDelegate(Nodes Sender, UnCheckEventArgs e);
	internal delegate void OnSelectInternalDelegate(Nodes Sender, CanSelectEventArgs e);
	internal delegate void OnUnSelectInternalDelegate(Nodes Sender, CanUnSelectEventArgs e);

	internal class Nodes  : INodeEnumerable
	{
		public event OnExpandInternalDelegate	OnBeginExpand;
		public event OnCollapseInternalDelegate	OnBeginCollapse;
		/// <summary>
		/// OnInitNode n'est pas un event car cela ralenti l'execution
		/// avec cette delegate l'ajout de 1 000 000  de noeud prend 2590 ms
		/// sans cette delegate l'ajout de 1 000 000  de noeud prend 1685 ms
		/// </summary>
		public OnNodeInternalDelegate			OnInitNode;
		public event OnCheckInternalDelegate	OnBeginCheck;
		public event OnUnCheckInternalDelegate	OnBeginUnCheck;
		public event OnSelectInternalDelegate	OnBeginSelect;
		public event OnUnSelectInternalDelegate OnBeginUnSelect;
		public event OnNodeInternalDelegate		OnInvalidateNodeNeeded;

		private int FDefaultNodeHeight;
		private int FNumPad;
		private Root FRoot;
		private IList FSelecteds;
		private int FColIndex;

		public Nodes()
		{
			FDefaultNodeHeight = 18;
			FRoot = new Root(this);
			FSelecteds = new ArrayList();
			FColIndex = Constantes.NoColumn;
		}
		#region propriétés
		public int DefaultNodeHeight
		{
			get
			{
				return FDefaultNodeHeight;
			}
			set
			{
				FDefaultNodeHeight = value;
			}
		}

		public Node Selected
		{
			get
			{
				if (FSelecteds.Count > 0)
					return (Node)FSelecteds[0];
				else
					return null;
			}
		}

		public Root RootNode
		{
			get
			{
				return FRoot;
			}
		}

		public int CurrentColIndex
		{
			get
			{
				return FColIndex;
			}
			set
			{
				FColIndex = value;
			}
		}

		/// <summary>
		///  renvoi le nombre de noeud à la racine
		/// </summary>
		public int Count
		{
			get
			{
				return FRoot.ChildCount;
			}
		}

		/// <summary>
		/// renvoi le nombre total de noeuds
		/// </summary>
		public int TotalCount
		{
			get
			{
				return FRoot.TotalCount;
			}
		}
		#endregion

		private Node NewNode()
		{
			Node Result = new Node();
			Result.Height = DefaultNodeHeight;
			Result.TotalHeight = DefaultNodeHeight;
			Result.ImageIndex = -1;
			Set.Include(ref Result.State, NodeState.Visible);
			return Result;
		}

		private void InitNode(Node n)
		{
			if (OnInitNode != null)
				OnInitNode(new NodeEventArgs(n));
		}

		private void UpdateTotalCount(Node aNode, int nb)
		{
			while (aNode != null)
			{
				aNode.TotalCount += nb;
				aNode = aNode.Parent;
			}
			int totalCount = FRoot.TotalCount;
			FNumPad = 1;
			while (totalCount >= 10)
			{
				totalCount /= 10;
				FNumPad++;
			}
		}

		#region méthodes internes
		
		internal string RankNode(Node aNode)
		{
			if (aNode.Parent != null)
				return  RankNode(aNode.Parent) + aNode.Index.ToString().PadLeft(FNumPad, '0');
			else
				return aNode.Index.ToString().PadLeft(FNumPad, '0');
		}

		internal void Clear()
		{
			FSelecteds.Clear();
			FRoot = new Root(this);
			FNumPad = 4;
		}

		internal Node InternalAdd(Node aParent, object aData)
		{
			Node child;
			int index;

			if (aParent == null)
				aParent = FRoot;

			if (aParent.LastChild != null)
				index = aParent.LastChild.Index + 1;
			else
			{
				index = 0;
				Set.Include(ref aParent.State, NodeState.HasChildren);
			}
			child = NewNode();
			child.InInitNode = true;
			try
			{
				child.Index = index;
				child.Data = aData;
				InitNode(child);
				Set.Exclude(ref child.State, NodeState.Selected);
				child.PrevSibling = aParent.LastChild;
				if (aParent.LastChild != null)
					aParent.LastChild.NextSibling =child;
				child.Parent = aParent;
				aParent.LastChild = child;
				if (aParent.FirstChild == null)
					aParent.FirstChild = child;
				UpdateParentFields(child, 1);
			}
			finally
			{
				child.InInitNode = false;
			}
			aParent.ChildCount++;
			UpdateTotalCount(aParent, 1);
			return child;
		}

		private void UpdateParentFields(Node child, int sens)
		{
			if (Set.Contains(child.State, NodeState.Visible))
			{
				Node.UpdateTotalTotalHeight(child.Parent, child.TotalHeight * sens);
				if (Set.Contains(child.State, NodeState.HasCheck))
					Node.UpdateTotalCheck(child.Parent, (1 + child.TotalCheck) * sens);
				else
					Node.UpdateTotalCheck(child.Parent, (child.TotalCheck) * sens);
				if (Set.Contains(child.State, NodeState.Checked))
					Node.UpdateTotalCheckedCount(child.Parent, (1 + child.TotalChecked) * sens);
				else
					Node.UpdateTotalCheckedCount(child.Parent, (child.TotalChecked) * sens);
				if (Set.Contains(child.State, NodeState.Signaled))
					Node.UpdateTotalSignaledCount(child.Parent, (1 + child.TotalSignaled) * sens);
				else
					Node.UpdateTotalSignaledCount(child.Parent, (child.TotalSignaled) * sens);
			}
		}
		private void IndexRenommer(Node aParent)
		{
			if (aParent == null)
				return;
			Node aNode = aParent.FirstChild;
			IndexRenommer(aNode, 0);
		}

		private void IndexRenommer(Node aNode, int index)
		{
			while (aNode != null)
			{
				aNode.Index = index++;
				aNode = aNode.NextSibling;
			}
		}

		internal bool InternalDelete(Node aNode)
		{
			if (aNode == null || aNode == FRoot)
				return false;
			InternalUnSelect(aNode);
			if (aNode.Parent != null)
			{
				if (aNode.Parent.FirstChild == aNode)
					aNode.Parent.FirstChild = aNode.NextSibling;
				if (aNode.Parent.LastChild == aNode)
					aNode.Parent.LastChild = aNode.PrevSibling;
				/*
				if (Set.Contains(aNode.Parent.State, NodeState.Expanded) && 
					Set.Contains(aNode.State, NodeState.Visible))
					Node.UpdateTotalTotalHeight(aNode.Parent, -aNode.TotalHeight);
				*/
				UpdateTotalCount(aNode.Parent, aNode.TotalCount - 1);
				aNode.Parent.ChildCount--;
				UpdateParentFields(aNode, -1);
			}
			if (aNode.PrevSibling != null)
				aNode.PrevSibling.NextSibling = aNode.NextSibling;
			if (aNode.NextSibling != null)
				aNode.NextSibling.PrevSibling = aNode.PrevSibling;
			IndexRenommer(aNode.Parent);
			return true;
		}

		internal void InternalInsert(Node n, Node where, DragPosition position)
		{
			if (position == DragPosition.Under)
			{	
				n.NextSibling = where.FirstChild;
				if (n.NextSibling != null)
					n.NextSibling.PrevSibling = n;
				n.PrevSibling = null;
				n.Parent = where;
				where.FirstChild = n;
				if (where.LastChild == null)
					where.LastChild = n;
				n.Index = 0;
				where.ChildCount++;
				Set.Include(ref where.State, NodeState.HasChildren);
				if (Set.Contains(where.State, NodeState.Expanded) && 
					Set.Contains(where.State, NodeState.Visible))
					Node.UpdateTotalTotalHeight(where, n.TotalHeight);
				UpdateTotalCount(where, n.TotalCount + 1);
				IndexRenommer(n.NextSibling, 1);
				UpdateParentFields(n, 1);
			}
			else 
			{
				if (position == DragPosition.After)
				{
					n.PrevSibling = where;
					n.NextSibling = where.NextSibling;
					if (where.Parent.LastChild == where)
						where.Parent.LastChild = n;
				}
				else
				{
					n.NextSibling = where;
					n.PrevSibling = where.PrevSibling;
					if (where.Parent.FirstChild == where)
						where.Parent.FirstChild = n;
				}
				if (n.PrevSibling != null)
					n.PrevSibling.NextSibling = n;
				if (n.NextSibling != null)
					n.NextSibling.PrevSibling = n;
				n.Parent = where.Parent;
				n.Parent.ChildCount++;
				UpdateTotalCount(n.Parent, n.TotalCount + 1);
				IndexRenommer(n.Parent);
				UpdateParentFields(n, 1);
			}
		}

		internal bool InternalExpand(Node n, out int aHeight)
		{
			ExpandEventArgs ev = new ExpandEventArgs(n);

			if (OnBeginExpand != null)
				OnBeginExpand(this, ev);
			aHeight = 0;
			if (ev.CanExpand)
			{
				int childHeight = 0;
				Set.Include(ref n.State, NodeState.Expanded);
				//ajuster la hauteur total
				Node child = n.FirstChild;
				while (child != null)
				{
					if (Set.Contains(child.State, NodeState.Visible))
						childHeight += child.TotalHeight;
					child = child.NextSibling;
				}
				aHeight = n.TotalHeight;
				Node.UpdateTotalTotalHeight(n, childHeight);
				aHeight = n.TotalHeight - aHeight;
			}
			return ev.CanExpand;
		}

		internal bool InternalCollapse(Node n, out int aHeight)
		{
			CollapseEventArgs ev = new CollapseEventArgs(n);
			if (OnBeginCollapse != null)
				OnBeginCollapse(this, ev);
			aHeight = 0;
			if (ev.CanCollapse)
			{
				Set.Exclude(ref n.State, NodeState.Expanded);
				aHeight = n.TotalHeight;
				Node.UpdateTotalTotalHeight(n.Parent, n.Height-aHeight);
				n.TotalHeight = n.Height;
				aHeight = n.TotalHeight - aHeight;
				Node selected = Selected;
				if (selected != null && !Node.IsFullyVisible(selected))
					InternalUnSelect(selected);
			}
			return ev.CanCollapse;
		}
		
		internal bool UnCheck(Node n)
		{
			UnCheckEventArgs ev = new UnCheckEventArgs(n);
			if (OnBeginUnCheck != null)
				OnBeginUnCheck(this, ev);
			if (ev.CanUnCheck)
			{
				Node child = n.FirstChild;
				bool isheck = Set.Contains(n.State, NodeState.Checked);
				Set.Exclude(ref n.State, NodeState.Checked);
				while (child != null)
				{
					if (Set.Contains(child.State, NodeState.HasCheck) && child.IsVisible)
						UnCheck(child);
					child = child.NextSibling;
				}
				if (isheck && n.IsVisible)
					Node.UpdateTotalCheckedCount(n.Parent, -1);
			}
			return ev.CanUnCheck;
		}

		internal bool Check(Node n)
		{
			CheckEventArgs ev = new CheckEventArgs(n);
			if (OnBeginCheck != null)
				OnBeginCheck(this, ev);
			if (ev.CanCheck)
			{
				Node child = n.FirstChild;
				while (child != null)
				{
					if (Set.Contains(child.State, NodeState.HasCheck) && child.IsVisible)
						Check(child);
					child = child.NextSibling;
				}
				if (!Set.Contains(n.State, NodeState.Checked))
				{
					Set.Include(ref n.State, NodeState.Checked);
					if (n.IsVisible)
						Node.UpdateTotalCheckedCount(n.Parent, 1);
				}
				Debug.WriteLine(String.Format("{2} (check :{0}, checked{1}",n.TotalCheck, n.TotalChecked, n.Text));
				Debug.WriteLine(String.Format("{2} (check :{0}, checked{1}",n.Parent.TotalCheck, n.Parent.TotalChecked, n.Parent.Text));
			}
			return ev.CanCheck;
		}

		internal void UnSelectAll()
		{
			foreach(Node n in FSelecteds)
				Set.Exclude(ref n.State, NodeState.Selected);
			FSelecteds.Clear();
		}

		internal bool InternalSelectNode(Node n, bool add, int aCol)
		{
			if (!FSelecteds.Contains(n) || FColIndex != aCol)
			{
				CanSelectEventArgs ev = new CanSelectEventArgs(n, aCol);
				if (OnBeginSelect != null)
					OnBeginSelect(this, ev);
				if (ev.CanSelect)
				{
					if (!FSelecteds.Contains(n))
					{
						if (!add)
							UnSelectAll();
						Set.Include(ref n.State, NodeState.Selected);
						FSelecteds.Add(n);
					}
					FColIndex = aCol;
				}
				return ev.CanSelect;
			}
			return false;
		}

		internal void InternalUnSelect(Node n)
		{
			Set.Exclude(ref n.State, NodeState.Selected);
			FSelecteds.Remove(n);
		}

		internal bool UnSelect(Node n, int aCol)
		{
			CanUnSelectEventArgs ev = new CanUnSelectEventArgs(n, aCol);
			if (OnBeginUnSelect != null)
				OnBeginUnSelect(this, ev);
			if (ev.CanUnSelect)
			{
				InternalUnSelect(n);
			}
			return ev.CanUnSelect;
		}
		
		internal bool InternalSetVisible(Node n, bool value, out int aHeight)
		{
			bool isvisible = Set.Contains(n.State, NodeState.Visible); 
			if (value != isvisible)
			{
				InternalUnSelect(n);
				aHeight = FRoot.TotalHeight;
				if (value)
				{
					Set.Include(ref n.State, NodeState.Visible);
					if (Set.Contains(n.Parent.State, NodeState.Expanded))
						Node.UpdateTotalTotalHeight(n.Parent, n.TotalHeight);
					Node.UpdateTotalCheck(n.Parent, n.TotalCheck);
					if (Set.Contains(n.State, NodeState.HasCheck))
						Node.UpdateTotalCheck(n.Parent, 1);
					Node.UpdateTotalCheckedCount(n.Parent, n.TotalChecked);
					if (Set.Contains(n.State, NodeState.Checked))
						Node.UpdateTotalCheckedCount(n.Parent, 1);
					Node.UpdateTotalSignaledCount(n.Parent, n.TotalSignaled);
					if (Set.Contains(n.State, NodeState.Signaled))
						Node.UpdateTotalSignaledCount(n.Parent, 1);
				}
				else
				{
					Set.Exclude(ref n.State, NodeState.Visible);
					if (Set.Contains(n.Parent.State, NodeState.Expanded))
						Node.UpdateTotalTotalHeight(n.Parent, -n.TotalHeight);
					Node.UpdateTotalCheck(n.Parent, -n.TotalCheck);
					if (Set.Contains(n.State, NodeState.HasCheck))
						Node.UpdateTotalCheck(n.Parent, -1);
					Node.UpdateTotalCheckedCount(n.Parent, -n.TotalChecked);
					if (Set.Contains(n.State, NodeState.Checked))
						Node.UpdateTotalCheckedCount(n.Parent, -1);
					Node.UpdateTotalSignaledCount(n.Parent, -n.TotalSignaled);
					if (Set.Contains(n.State, NodeState.Signaled))
						Node.UpdateTotalSignaledCount(n.Parent, -1);
				}
				aHeight = FRoot.TotalHeight - aHeight;
				return true;
			}
			aHeight = 0;
			return false;
		}

		internal void InvalidateNode(Node n)
		{
			if (OnInvalidateNodeNeeded != null)
				OnInvalidateNodeNeeded(new NodeEventArgs(n));
		}

		#region tri par quicksort
		#region Ancien quicksort
		/// <summary>
		/// récupération du index ième node
		/// </summary>
		/// <param name="aParent"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private Node NodeByIndex(Node aParent, int index)
		{
			Node Result = aParent.FirstChild;
			while (--index >= 0)
			{
				Result = Result.NextSibling;
			}
			return Result;
		}

		private void ExchangeRef(ref Node aref, Node a, Node b)
		{
			if (aref == a)
				aref = b;
			else if (aref == b)
				aref = a;
		}

		private void ExchangeItems(Node aParent, ref Node a, ref Node b)
		{
			Node anext, bnext, aprev, bprev;
			if (a == b)
				return;
			ExchangeRef(ref aParent.FirstChild, a, b);
			ExchangeRef(ref aParent.LastChild, a, b);
			if (a.NextSibling == b)
			{ 
				//b suit a
				b.PrevSibling = a.PrevSibling;
				a.NextSibling = b.NextSibling;
				a.PrevSibling = b;
				b.NextSibling = a;
			}
			else if (b.NextSibling == a)
			{
				//a suit b
				a.PrevSibling = b.PrevSibling;
				b.NextSibling = a.NextSibling;
				b.PrevSibling = a;
				a.NextSibling = b;
			}
			else
			{
				aprev = a.PrevSibling;
				anext = a.NextSibling;
				bprev = b.PrevSibling;
				bnext = b.NextSibling;
				b.PrevSibling = aprev;
				b.NextSibling = anext;
				a.PrevSibling = bprev;
				a.NextSibling = bnext;
			}
			if (b.PrevSibling != null)
				b.PrevSibling.NextSibling = b;
			if (a.NextSibling != null)
				a.NextSibling.PrevSibling = a;
			if (a.PrevSibling != null)
				a.PrevSibling.NextSibling = a;
			if (b.NextSibling != null)
				b.NextSibling.PrevSibling = b;
			int aindex = a.Index;
			a.Index = b.Index;
			b.Index = aindex;
			anext = a;
			a = b;
			b = anext;
		}

		private void QuickSort(Node aParent, int L, int R, int acol, SortDirection aDirection, OnCompareNodeDelegate aFct)
		{
			Node iNode, jNode, pNode;
			int multiplier = (aDirection == SortDirection.Ascending ? 1 : -1);
			do
			{
				iNode = NodeByIndex(aParent, L);
				jNode = NodeByIndex(aParent, R);
				pNode = NodeByIndex(aParent, (L + R) >> 1);
				do
				{
				while (aFct(iNode, pNode, acol) * multiplier < 0) 
					iNode = iNode.NextSibling;
				while (aFct(jNode, pNode, acol) * multiplier > 0) 
					jNode = jNode.PrevSibling;
					if (iNode.Index <= jNode.Index)
					{
						ExchangeItems(aParent, ref iNode, ref jNode);
						if (pNode.Index == iNode.Index)
							pNode = jNode;
						else if (pNode == jNode)
							pNode = iNode;
						iNode = iNode.NextSibling;
						jNode = jNode.PrevSibling;
					}
				}
				while(jNode != null && iNode != null && iNode.Index <= jNode.Index);
				if (jNode != null && L < jNode.Index) 
					QuickSort(aParent, L, jNode.Index, acol, aDirection, aFct);
				L = iNode.Index;
			}
			while(iNode.Index < R);
			#region quickSort Delphi
			/*
procedure TStringList.QuickSort(L, R: Integer; SCompare: TStringListSortCompare);
var
  I, J, P: Integer;
begin
  repeat
	I := L;
	J := R;
	P := (L + R) shr 1;
	repeat
	  while SCompare(Self, I, P) < 0 do Inc(I);
	  while SCompare(Self, J, P) > 0 do Dec(J);
	  if I <= J then
	  begin
		ExchangeItems(I, J);
		if P = I then
		  P := J
		else if P = J then
		  P := I;
		Inc(I);
		Dec(J);
	  end;
	until I > J;
	if L < J then QuickSort(L, J, SCompare);
	L := I;
  until I >= R;
end;
			 */
			#endregion
		}

		#endregion

		private void QuickSort(ref Node[] aTab, int L, int R, int acol, SortDirection aDirection, OnCompareNodeDelegate aFct)
		{
			int I, J, P;

			int multiplier = (aDirection == SortDirection.Ascending ? 1 : -1);
			do
			{
				I = L;
				J = R;
				P = (L + R) >> 1;
				do
				{
					while (aFct(aTab[I], aTab[P], acol) * multiplier < 0) 
						I++;
					while (aFct(aTab[J], aTab[P], acol) * multiplier > 0) 
						J--;
					if (I <= J)
					{
						Node tmp = aTab[I];
						aTab[I] = aTab[J];
						aTab[J] = tmp;
						if (P == I)
							P = J;
						else if (P == J)
							P = I;
						I++;
						J--;
					}
				}
				while (I <= J);
				if (L < J) 
					QuickSort(ref aTab,  L, J, acol, aDirection, aFct);
				L = I;
			}
			while (I < R);
			#region quickSort Delphi
			/*
procedure TStringList.QuickSort(L, R: Integer; SCompare: TStringListSortCompare);
var
  I, J, P: Integer;
begin
  repeat
	I := L;
	J := R;
	P := (L + R) shr 1;
	repeat
	  while SCompare(Self, I, P) < 0 do Inc(I);
	  while SCompare(Self, J, P) > 0 do Dec(J);
	  if I <= J then
	  begin
		ExchangeItems(I, J);
		if P = I then
		  P := J
		else if P = J then
		  P := I;
		Inc(I);
		Dec(J);
	  end;
	until I > J;
	if L < J then QuickSort(L, J, SCompare);
	L := I;
  until I >= R;
end;
			 */
			#endregion
		}

		#endregion

		internal void DoSort(Node aNode, int aCol, SortDirection aDirection, OnCompareNodeDelegate aCompareFct, bool recurse)
		{
			Node run;
			
			if (aNode.ChildCount > 1)
			{
				Node[] children = aNode.Children;
				QuickSort(ref children, 0, children.Length-1, aCol, aDirection, aCompareFct);
				//refaire les liaisons entre noeuds
				aNode.FirstChild = children[0];
				children[0].PrevSibling = null;
				aNode.FirstChild.Index = 0;
				for (int i = 1; i < children.Length; i++)
				{
					children[i-1].NextSibling = children[i];
					children[i].PrevSibling = children[i-1];	
					children[i].Index = i;
				}
				aNode.LastChild = children[children.Length-1];
				aNode.LastChild.NextSibling = null;

			}
			run = aNode.FirstChild;
			while (recurse && run != null)
			{
				DoSort(run, aCol, aDirection, aCompareFct, recurse);
				run = run.NextSibling;
			}
		}
		#endregion

		#region INodeEnumerable Members

		public IEnumerable GetNodes()
		{
			return new NodeEnumerator(FRoot, false);
		}

		IEnumerable Genius.Controls.TreeView.Core.INodeEnumerable.GetNodes(bool recurse)
		{
			return new NodeEnumerator(FRoot, recurse);
		}

		public IEnumerable GetVisibleNodes(bool recurse)
		{
			return new NodeEnumerator(FRoot, recurse, true);
		}

		#endregion
	}
}

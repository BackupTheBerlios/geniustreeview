using System;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	/// <summary>
	/// delegate de base
	/// </summary>
	public delegate void OnNodeDelegate(GeniusTreeView Sender, NodeEventArgs e);

	/// <summary>
	/// delegate utiliser avec <see cref="GeniusTreeView.OnGetHint"/>
	/// </summary>
	public delegate void OnGetHintDelegate(GeniusTreeView Sender, NodeGetHintEventArgs e);

	public delegate void OnExpandDelegate(GeniusTreeView Sender, ExpandEventArgs e);
	public delegate void OnCollapseDelegate(GeniusTreeView Sender, CollapseEventArgs e);
	public delegate void OnCheckDelegate(GeniusTreeView Sender, CheckEventArgs e);
	public delegate void OnUnCheckDelegate(GeniusTreeView Sender, UnCheckEventArgs e);

	public delegate void OnCanEditDelegate(GeniusTreeView Sender, CanEditEventArgs e);
	public delegate void OnInitEditDelegate(GeniusTreeView Sender, EditEventArgs e);
	public delegate void OnAfterEditDelegate(GeniusTreeView Sender, EditEventArgs e);
	public delegate void OnSelectDelegate(GeniusTreeView Sender, CanSelectEventArgs e);
	public delegate void OnUnSelectDelegate(GeniusTreeView Sender, CanUnSelectEventArgs e);
	public delegate void OnSelectedDelegate(GeniusTreeView Sender, SelectedEventArgs e);
	public delegate void OnGetNodeTextDelegate(GeniusTreeView Sender, NodeTextEventArgs e);
	
	/// <summary>
	/// delegate pour le custom paint
	/// </summary>
	public delegate void OnPaintNodeDelegate(GeniusTreeView Sender, PaintNodeEventArgs e);
	
	public delegate int  OnCompareNodeDelegate(INode A, INode B, int aDisplayColumn);
	public delegate IComparable  OnGetNodeValueForComparisonDelegate(INode A, int aDisplayColumn);
	public delegate void OnDrawDragNodeDelegate(GeniusTreeView Sender, DrawDragEventArgs e);
	public delegate void OnPrepareDragNodeDelegate(GeniusTreeView Sender, PrepareDragEventArgs e);
	public delegate void OnCanDragToDelegate(GeniusTreeView Sender, CanDragToEventArgs e);
	public delegate void OnDrawHeaderColDelegate(GeniusTreeView Sender, DrawHeaderColEventArgs e);
	/// <summary>
	/// méthode délégée liée à la création d'un éditeur 
	/// </summary>
	public delegate void OnCreateEditorDelegate(GeniusTreeView Sender, NodeEditorEventArgs e);

	/// <summary>
	/// méthode déléguée afin de recupérer l'index de l'image durant le paint <see cref="GeniusTreeView.OnGetImageIndex"/>
	/// </summary>
	public delegate void OnGetImageIndexDelegate(GeniusTreeView Sender, NodeImageIndexEventArgs e);

	public delegate void OnBeforeDeleteDelegate(GeniusTreeView Sender, NodeDeleteEventArgs e);

	public delegate void OnDrawingNodeTreeLinesDelegate(GeniusTreeView Sender, NodeDrawingTreeLinesEventArgs e);

	/// <summary>
	/// déléguée pour la création des HintWindow
	/// </summary>
	public delegate void OnCreateHintWindowDelegate(GeniusTreeView Sender, NodeHintWindowEventArgs e);
}

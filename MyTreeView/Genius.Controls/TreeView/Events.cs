using System;
using System.Drawing;
using Genius.Controls.TreeView;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	/// <summary>
	/// Classe de base pour les evènements du <see cref="GeniusTreeView"/>
	/// </summary>
	public class NodeEventArgs : EventArgs
	{
		private INode FNode;
		
		/// <summary>
		/// construteur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public NodeEventArgs(INode aNode) : base()
		{
			FNode = aNode;
		}

		internal void SetNode(INode n)
		{
			FNode = n;
		}
		/// <summary>
		/// noeud concerner par l'évènement en cours
		/// </summary>
		public INode Node
		{
			get
			{
				return FNode;
			}
		}
	}

	/// <summary>
	/// classe de base de gestion des évènements lors du mode grille
	/// </summary>
	public class NodeCellEventArgs : NodeEventArgs
	{
		private int FDisplayColumn;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="aDisplayColumn"></param>
		public NodeCellEventArgs(INode aNode, int aDisplayColumn) : base(aNode)
		{
			FDisplayColumn = aDisplayColumn;
		}

		/// <summary>
		/// Colonne concernée par l'évènement en cours
		/// </summary>
		public int DisplayColumn
		{
			get
			{
				return FDisplayColumn;
			}
		}
	}

	/// <summary>
	/// évènement lors de l'expand d'un noeud
	/// </summary>
	public class ExpandEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public ExpandEventArgs(INode aNode) : base(aNode)
		{
			CanExpand = true;
		}

		/// <summary>
		/// autorise ou pas l'expansion du noeud en cours
		/// </summary>
		public bool CanExpand;
	}

	/// <summary>
	/// évènement lors de replie d'un noeud
	/// </summary>
	public class CollapseEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public CollapseEventArgs(INode aNode) : base(aNode)
		{
			CanCollapse = true;
		}

		/// <summary>
		/// autosie ou pas le replie du noeuds en cours
		/// </summary>
		public bool CanCollapse;
	}

	/// <summary>
	/// <see cref="GeniusTreeView.OnBeforeCheck"/>
	/// </summary>
	public class CheckEventArgs : NodeEventArgs
	{
		/// <summary>
		/// construteur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public CheckEventArgs(INode aNode) : base(aNode)
		{
			CanCheck = true;
		}

		/// <summary>
		/// autorise ou pas le check sur le noeud courant
		/// </summary>
		public bool CanCheck;
	}

	/// <summary>
	/// évènement sur le "uncheck", <see cref="GeniusTreeView.OnBeforeUnCheck"/>
	/// </summary>
	public class UnCheckEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public UnCheckEventArgs(INode aNode) : base(aNode)
		{
			CanUnCheck = true;
		}

		/// <summary>
		/// peut-on décocher le noeud en cours
		/// </summary>
		public bool CanUnCheck;
	}

	/// <summary>
	/// voir <see cref="GeniusTreeView.OnBeforeEdit"/>
	/// </summary>
	public class CanEditEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		public CanEditEventArgs(INode aNode) : base(aNode)
		{
			CanEdit = true;
		}
		/// <summary>
		/// peuit on editer le noeud en cours
		/// </summary>
		public bool CanEdit;
	}
	
	/// <summary>
	/// <see cref="GeniusTreeView.OnBeforeSelect"/>
	/// </summary>
	public class CanSelectEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="colindex"></param>
		public CanSelectEventArgs(INode aNode, int colindex) : base(aNode)
		{
			CanSelect = true;
			DisplayColumn = colindex;
		}
		/// <summary>
		/// puis-je selectionner le noeud
		/// </summary>
		public bool CanSelect;
		/// <summary>
		/// la colonne que l'on tente de selectionné, en mode grille
		/// </summary>
		public int DisplayColumn;
	}

	/// <summary>
	/// <see cref="GeniusTreeView.OnBeforeUnSelect"/>
	/// </summary>
	public class CanUnSelectEventArgs : NodeEventArgs
	{
		/// <summary>
		/// constrcuteur par défaut, vous n'avez pas à instancier cette classe
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="colindex"></param>
		public CanUnSelectEventArgs(INode aNode, int colindex) : base(aNode)
		{
			CanUnSelect = true;
			DisplayColumn = colindex;
		}
		/// <summary>
		/// peut deselectioner le noeuds
		/// </summary>
		public bool CanUnSelect;
		/// <summary>
		/// la colonne concerné
		/// </summary>
		public int DisplayColumn;
	}

	/// <summary>
	/// 
	/// </summary>
	public class SelectedEventArgs : NodeEventArgs
	{
		private int FColumn;

		/// <summary>
		/// constructeur
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="colindex"></param>
		public SelectedEventArgs(INode aNode, int colindex) : base (aNode)
		{
			FColumn = colindex;
		}

		/// <summary>
		/// la display colonne concernée
		/// </summary>
		public int DisplayColumn
		{
			get
			{
				return FColumn;
			}
		}
	}

	/// <summary>
	/// arguments de l'évènement <see cref="GeniusTreeView.OnGetNodeText"/>
	/// </summary>
	public class NodeTextEventArgs : SelectedEventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="colindex"></param>
		/// <param name="defaultText"></param>
		public NodeTextEventArgs(INode aNode, int colindex, string defaultText) : base(aNode, colindex)
		{
			Text = defaultText;
		}

		/// <summary>
		/// le texte
		/// </summary>
		public string Text;
	}

	/// <summary>
	/// argument de l'évènement <see cref="GeniusTreeView.OnAfterCellPainting"/>, 
	/// <see cref="GeniusTreeView.OnBeforeCellPainting"/>, <see cref="GeniusTreeView.OnAfterPainting"/>,
	/// <see cref="GeniusTreeView.OnAfterNodePainting"/>,<see cref="GeniusTreeView.OnPaintNodeBackGround"/>,
	/// <see cref="GeniusTreeView.OnBeforeNodePainting"/>, <see cref="GeniusTreeView.OnBeforePainting"/>
	/// </summary>
	public class PaintNodeEventArgs : NodeEventArgs
	{
		private PaintInfo FInfo;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="paintinfo"></param>
		public PaintNodeEventArgs(PaintInfo paintinfo) : base(paintinfo.Node)
		{
			FInfo = paintinfo;
		}

		/// <summary>
		/// 
		/// </summary>
		public PaintInfo Info
		{
			get
			{
				return FInfo;
			}
		}
	}

	public class DrawDragEventArgs : EventArgs
	{
		public DrawDragEventArgs(Graphics g, Rectangle r )
		{
			graphics = g;
			Bounds = r;
			DefaultDrawing = true;
			Forbiden = false;
		}
		public Graphics graphics;
		public Rectangle Bounds;
		public bool DefaultDrawing;
		public bool Forbiden;
	}

	public class PrepareDragEventArgs : EventArgs
	{
		public PrepareDragEventArgs(Rectangle r )
		{
			Bounds = r;
		}
		public Rectangle Bounds;
	}

	public class CanDragToEventArgs : NodeEventArgs
	{
		private INode FDropNode;
		private DragPosition FPosition;
		
		public CanDragToEventArgs(INode n, INode aDrop, DragPosition aPosition) : base(n)
		{
			FDropNode = aDrop;
			FPosition = aPosition;
			CanDrop = true;
		}

		/// <summary>
		/// le noeud qui recoit le drop
		/// </summary>
		public INode DropNode
		{
			get
			{
				return FDropNode;
			}
		}

		public DragPosition Position
		{
			get
			{
				return FPosition;
			}
		}

		public bool CanDrop;
	}

	/// <summary>
	/// classe liée à la création d'un éditeur
	/// </summary>
	public class NodeEditorEventArgs : NodeEventArgs
	{
		private int FColumn;

		public NodeEditorEventArgs(INode aNode, int acol) : base(aNode)
		{
			Editor = null;
			FColumn = acol;
		}

		/// <summary>
		/// Placez votre propre éditeur
		/// </summary>
		public ITreeViewEdit Editor;

		/// <summary>
		/// indique la colonne en édition
		/// </summary>
		public int DisplayColumn
		{
			get
			{
				return FColumn;
			}
		}
	}

	public class DrawHeaderColEventArgs : EventArgs
	{
		public DrawHeaderColEventArgs(Graphics g, GeniusTreeViewColonne aCol, Rectangle rCol)
		{
			DefaultDrawing = true;
			graphics = g;
			RectCol = rCol;
			Col = aCol;
		}

		public bool DefaultDrawing;
		public Graphics graphics;
		public GeniusTreeViewColonne Col;
		public Rectangle RectCol;
	}

	/// <summary>
	/// type d'image demandée lors de l'évènement <see cref="GeniusTreeView.OnGetImageIndex"/>
	/// </summary>
	public enum ImageIndexType
	{
		/// <summary>
		/// image normal
		/// </summary>
		NormalImage, 
		/// <summary>
		/// image d'état
		/// </summary>
		StateImage
	};

	/// <summary>
	/// Classe associée à l'évènement <see cref="GeniusTreeView.OnGetImageIndex"/>
	/// </summary>
	public class NodeImageIndexEventArgs : NodeEventArgs
	{
		private ImageIndexType FImageIndexType;
		public NodeImageIndexEventArgs(INode n, ImageIndexType aImageType) : base(n)
		{
			FImageIndexType = aImageType;
		}

		public int ImageIndex;
		public ImageIndexType ImageIndexType
		{
			get
			{
				return FImageIndexType;
			}
		}
	}

	public class NodeGetHintEventArgs : NodeCellEventArgs
	{
		public NodeGetHintEventArgs(INode aNode, int aDisplayColumn) : base(aNode, aDisplayColumn)
		{
			
		}

		public string HintText;
	}

	/// <summary>
	/// argument pour l'événement <see cref="GeniusTreeView.OnBeforeDelete"/>
	/// </summary>
	public class NodeDeleteEventArgs : NodeEventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="n"></param>
		public NodeDeleteEventArgs(INode n) : base(n)
		{
			CanDelete = true;
		}

		/// <summary>
		/// peut-on uspprimer le noeud en cours
		/// </summary>
		public bool CanDelete;
	}

	/// <summary>
	/// classe utiliser lors de l'évènement <see cref="GeniusTreeView.OnInitEdit"/>, <see cref="GeniusTreeView.OnAfterEdit"/>
	/// </summary>
	public class EditEventArgs : NodeCellEventArgs
	{
		private ITreeViewEdit FEditor;
		/// <summary>
		/// constructeur
		/// </summary>
		/// <param name="n"></param>
		/// <param name="aDisplayColumn"></param>
		/// <param name="editor"></param>
		public EditEventArgs(INode n, int aDisplayColumn, ITreeViewEdit editor): base(n, aDisplayColumn)
		{
			FEditor = editor;
		}

		/// <summary>
		/// editeur en cours
		/// </summary>
		public ITreeViewEdit Editor
		{
			get
			{
				return FEditor;
			}
		}
	}

	public class NodeFontEventArgs : NodeCellEventArgs
	{
		public NodeFontEventArgs(INode n, int aDisplayColumn) : base(n, aDisplayColumn)
		{
			
		}
		public Font Font;
	}

	public class NodeDrawingTreeLinesEventArgs : NodeEventArgs
	{
		public NodeDrawingTreeLinesEventArgs(INode n) : base(n)
		{
			Draw = true;
		}

		public bool Draw;
	}

	/// <summary>
	/// EventArgs pour la création du Hint
	/// </summary>
	public class NodeHintWindowEventArgs : EventArgs
	{
		/// <summary>
		/// constructeur par défaut
		/// </summary>
		public NodeHintWindowEventArgs()
		{
			
		}
		/// <summary>
		/// user HintWindow
		/// </summary>
		public IHintWindow HintWindow;
	}
}
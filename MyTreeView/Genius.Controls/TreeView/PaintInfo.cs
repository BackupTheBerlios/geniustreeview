using System;
using System.Drawing;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls.TreeView
{

	/// <summary>
	/// enum�ration de type flags, pour <see cref="PaintInfo.CellGridLines"/>, 
	/// � remplir dans le <see cref="GeniusTreeView.OnBeforeCellPainting"/>
	/// </summary>
	[Flags]
	public enum Lines
	{
		/// <summary>
		/// aucune lignes
		/// </summary>
		None = 0x00,
		/// <summary>
		/// celle du haut
		/// </summary>
		Top  = 0x02,
		/// <summary>
		/// bord droit
		/// </summary>
		Right = 0x04,
		/// <summary>
		/// bord bas
		/// </summary>
		Bottom = 0x08,
		/// <summary>
		/// tous
		/// </summary>
		All = Right | Bottom | Top,
		/// <summary>
		/// bords verticaux
		/// </summary>
		Vertical = Right,
		/// <summary>
		/// bord horizontaux
		/// </summary>
		Horizontal = Bottom | Top
	}
	/// <summary>
	/// class d'information pour le paint, permet d'�changer des informations
	/// entre le user et le paint
	/// cette structure est utilis�e avec <see cref="GeniusTreeView.OnBeginPainting"/>,
	/// <see cref="GeniusTreeView.OnBeforeNodePainting"/>, <see cref="GeniusTreeView.OnPaintNodeBackGround"/>,
	/// <see cref="GeniusTreeView.OnBeforeCellPainting"/>, <see cref="GeniusTreeView.OnAfterCellPainting"/>,
	/// <see cref="GeniusTreeView.OnAfterNodePainting"/>,<see cref="GeniusTreeView.OnEndPainting"/>
	/// </summary>
	public class PaintInfo
	{
		internal Node FNode;
		internal Graphics FGraphics;
		/// <summary>
		/// les options de dessin du noeud en cours (<see cref="DrawingOption"/>)
		/// </summary>
		public DrawingOption DrawingOptions;
		/// <summary>
		/// la column � dessiner
		/// </summary>
		public int DisplayColumn;
		internal Rectangle FNodeRect;
		internal Rectangle FClipRect;
		/// <summary>
		/// couleur de fond
		/// </summary>
		public GeniusLinearGradientBrush BackColor;
		/// <summary>
		/// couleur du texte
		/// </summary>
		public GeniusLinearGradientBrush ForeColor;
		/// <summary>
		/// format du texte
		/// </summary>
		public StringFormat StringFormat;
		/// <summary>
		/// laisse t-on le dessin se faire par d�faut
		/// </summary>
		public bool DefaultDrawing;
		/// <summary>
		/// la font � utiliser
		/// </summary>
		public Font	Font;

		/// <summary>
		/// quelles sont les lignes � dessiner en mode grille ?
		/// </summary>
		public Lines CellGridLines;
		
		/// <summary>
		/// renvoi le rectangle du noeud pour le dessin
		/// </summary>
		public Rectangle NodeRect
		{
			get
			{
				return FNodeRect;
			}
		}

		/// <summary>
		/// le rectangle de clip
		/// </summary>
		public Rectangle ClipRect
		{
			get
			{
				return FClipRect;
			}
		}

		/// <summary>
		/// le constructeur par d�faut 
		/// </summary>
		public PaintInfo()
		{
		}

		/// <summary>
		/// le noeud en cours
		/// </summary>
		public INode Node
		{
			get
			{
				return FNode;
			}
		}

		/// <summary>
		/// le canevas � utiliser pour dessiner
		/// </summary>
		public Graphics graphics
		{
			get
			{
				return FGraphics;
			}
		}

		/*
		 * NodesTreeLines[] -> DrawLines et Node
		 */
	}
}

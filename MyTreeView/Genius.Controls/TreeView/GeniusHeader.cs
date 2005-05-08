using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{

	[Flags]
	enum HeaderStateEnum 
	{
		TackPending = 0x01, 
		Tracking = 0x02,
		DragPending = 0x04,
		Dragging    = 0x08
	}

	/// <summary>
	/// dicretion du tri
	/// </summary>
	public enum SortDirection
	{
		/// <summary>
		/// tri ascendant
		/// </summary>
		Ascending, 
		/// <summary>
		/// tri descendant
		/// </summary>
		Descending
	}
	/// <summary>
	/// classe représentant le header
	/// </summary>
	public class GeniusHeader : IDisposable
	{
		private Colonnes		FColonnes;
		private ArrayList		FDisplays;
		private int				FMainColumnIndex;
		private int				FClickIndex;
		internal GeniusTreeView	FTree;
		private Point			FDragStart;
		private Point			FDragDecal;
		//private Point			FTrackStart;
		private int				FTrackIndex;
		private int				FLeftTrackPos;
		private HeaderStateEnum FStates;
		private int				FDownIndex;
		private int				FHoverIndex;

		private int				FDragIndex;
		private int				FDropTarget;
		private bool			FDropBefore;
		private ImageList		FImageDropTarget;
		private Image			FDragImage;
		private DragForm		FDrag;

		private int				FSortColumn;
		private SortDirection	FSortDirection;
		private Font		FFont;
		private bool		FAllowDrag;
		private int			FFixedColumnCount;
		private int			FMainColumnDisplayIndex;
		private bool		FAllowSort;


		/// <summary>
		/// évènement paint sur le header
		/// </summary>
		public event OnDrawHeaderColDelegate	OnDrawHeaderCol;


		/// <summary>
		/// constructeur par défaut
		/// </summary>
		/// <param name="aTv"></param>
		public GeniusHeader(GeniusTreeView aTv)
		{
			FColonnes = new Colonnes(this);
			FDisplays = FColonnes.FDisplays;
			FImageDropTarget = new ImageList();
			FMainColumnIndex = Constantes.NoColumn;
			FClickIndex = Constantes.NoColumn;
			FDownIndex = Constantes.NoColumn;
			FDropTarget = Constantes.NoColumn;
			FDragIndex = Constantes.NoColumn;
			FHoverIndex = Constantes.NoColumn;
			FSortColumn = Constantes.NoColumn;
			FSortDirection = SortDirection.Ascending;
			FDropBefore = false;
			FAllowDrag = false;
			FAllowSort = true;
			FDragImage = null;
			FTree = aTv;
			FDragDecal = new Point(0,0);
			LoadDropTargetImage();
		}

		#region méthodes privées
		private void Exclude(ref HeaderStateEnum ens, HeaderStateEnum value)
		{
			ens &= (~value);
		}
		
		private void Include(ref HeaderStateEnum ens, HeaderStateEnum value)
		{
			ens |= value;
		}

		private bool Contains(HeaderStateEnum ens, HeaderStateEnum value)
		{
			return FTree.Contains((int)ens, (int)value);
		}

		
		#region gestion des images pour le drag
		private void LoadDropTargetImage()
		{
			Assembly a;

			string[] checks = new string[]{"Genius.Controls.Resources.dragtargetleft.bmp",
											  "Genius.Controls.Resources.dragtargetright.bmp",
											  "Genius.Controls.Resources.sortasc.bmp",
											  "Genius.Controls.Resources.sortdesc.bmp"			
										  };
			
			//On récupère l'assembly en cours d'exécution
			a = Assembly.GetExecutingAssembly();
			foreach(string check in checks)
			{
				Stream stm = a.GetManifestResourceStream(check);
				FImageDropTarget.Images.Add(new Bitmap(stm), Color.Fuchsia);
			}
		}
		
		private void DrawDropTarget(Graphics g, Point pt)
		{
			FImageDropTarget.Draw(g, pt, FDropBefore ? 0 : 1);
		}
		#endregion
		private bool DetermineSplitterIndex(Point p)
		{
			FTrackIndex = Constantes.NoColumn;
			if (this.Count > 0)
			{
				int splitPoint;

				if (p.X <= FixedColumnWidth && FixedColumnCount > 0)
				{
					int nbFixed = 0;
					splitPoint = 0;
					for (int i = 0; i < FDisplays.Count && nbFixed < FixedColumnCount; i++)
					{
						GeniusTreeViewColonne col = DisplayColonnes(i);
						if (col.Visible)
						{
							splitPoint += col.Width;
							nbFixed++;
						}
						if (col.Visible && col.AllowSize)
						{
							if (p.X < splitPoint+5 && p.X > splitPoint - 3)
							{
								FTrackIndex = i;
								FLeftTrackPos = splitPoint - col.Width;
								return true;
							}
						}				
					}
				}
				else
				{
					splitPoint = -FTree.OffsetX + FTree.RangeX;
					for (int i = FDisplays.Count-1; i >=0; i--)
					{
						GeniusTreeViewColonne col = DisplayColonnes(i);
						if (col.Visible && col.AllowSize)
						{
							if (p.X < splitPoint+5 && p.X > splitPoint - 3)
							{
								FTrackIndex = i;
								FLeftTrackPos = splitPoint - col.Width;
								return true;
							}
						}
						if (col.Visible)
							splitPoint -= col.Width;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// le nombre de colonne fixe à gauche
		/// </summary>
		[Description("Fixe le nombre de colonnes fixes")]
		[DefaultValue(0)]
		public int FixedColumnCount
		{
			get { return FFixedColumnCount; }
			set
			{
				FFixedColumnCount = value;
			}
		}

		/// <summary>
		/// retourne la largeur total des colonnes fixes
		/// </summary>
		[Browsable(false)]
		[Description("retourne la largeur total des colonnes fixes")]
		public int FixedColumnWidth
		{
			get
			{
				return GetWidthFromStart(FixedColumnCount-1);
			}
		}

		private int AdjustDownColumn(Point p)
		{
			int Result = Constantes.NoColumn;

			p.Y += FTree.HeaderHeight;
			Result = ColumnIndexAt(p.X);
			if (Result > Constantes.NoColumn && Result != FDownIndex)
			{
				if (FDownIndex > Constantes.NoColumn)
					Invalidate(this.DisplayColonnes(FDownIndex));
				if (Result > Constantes.NoColumn && !this.DisplayColonnes(Result).AllowClick)
				{
					FDownIndex = Constantes.NoColumn;
					return Result;
				}
				FDownIndex = Result;
				Invalidate(this.DisplayColonnes(FDownIndex));
			}
			return Result;
		}

		private bool AdjustDropTargetColumn(Point p)
		{
			int newIndex;
			bool newDropBefore = FDropBefore;

			p.Y += FTree.HeaderHeight;
			newIndex = this.ColumnIndexAt(p.X + FTree.OffsetX);
			if (newIndex > Constantes.NoColumn)
			{
				Rectangle rCol = this.ColumnRect(newIndex);
				newDropBefore = p.X <= (rCol.Right + rCol.Left) / 2;
			}
			
			if (newIndex != FDropTarget || FDropBefore != newDropBefore)
			{
				if (newIndex != FDropTarget && FDropTarget > Constantes.NoColumn)
					Invalidate(DisplayColonnes(FDropTarget));
				FDropTarget = newIndex;
				FDropBefore = newDropBefore;
				if (FDropTarget > Constantes.NoColumn)
					Invalidate(DisplayColonnes(FDropTarget));
				return true;
			}
			return false;
		}

		private bool GetNewIndex(Point p, ref int OldIndex)
		{
			int newIndex;

			p.Y += FTree.HeaderHeight;
			if (FixedColumnCount > 0 && p.X <= FixedColumnWidth)
			{
				newIndex = this.ColumnIndexAt(p.X);				
			}
			else
				newIndex = this.ColumnIndexAt(p.X + FTree.OffsetX);

			if (newIndex != OldIndex)
			{
				if (OldIndex > Constantes.NoColumn)
					Invalidate(DisplayColonnes(OldIndex));
				OldIndex = newIndex;
				if (OldIndex > Constantes.NoColumn)
					Invalidate(DisplayColonnes(OldIndex));
				return true;
			}
			return false;
		}

		private bool AdjustHoverColumn(Point p)
		{
			return GetNewIndex(p, ref FHoverIndex);
		}

		private Rectangle ColumnRect(int aColIndex)
		{
			Rectangle Result = new Rectangle(Left(aColIndex), 0, DisplayColonnes(aColIndex).Width, FTree.HeaderHeight);
			return Result;
		}

		private void PrepareDrag(Point aStart)
		{
			FDropTarget = Constantes.NoColumn;
			aStart = FTree.PointToClient(aStart);
			aStart.Y += FTree.HeaderHeight;
			FDragIndex = ColumnIndexAt(aStart.X);
			
			Rectangle rCol = ColumnRect(FDragIndex);
			FDragDecal.X = rCol.X - aStart.X;
			FDragDecal.Y = rCol.Y - aStart.Y;
			rCol.X = 0;
			if (FDrag != null)
			{
				FDrag.Dispose();
				FDrag = null;
			}
			FDragImage = new Bitmap(rCol.Width, rCol.Height);
			using (Graphics g = Graphics.FromImage(FDragImage))
			{
				InternalDrawHeaderCol(g, DisplayColonnes(FDragIndex), rCol);
			}
			FDrag = new DragForm(FDragImage, FTree);
			DragTo(Cursor.Position);
		}

		private void DragTo(Point p)
		{
			//Debug.WriteLine("droptarget :" + FDropTarget.ToString());
			FDrag.Forbiden =  FDropTarget == Constantes.NoColumn;
			if (!FDrag.Visible)
				FDrag.Show(p.X + FDragDecal.X, p.Y + FDragDecal.Y);
			else
				FDrag.Position(p.X + FDragDecal.X, p.Y + FDragDecal.Y);
		}

		private void EndDrag()
		{
			Dispose(ref FDragImage);
			if (FDrag != null)
			{
				FDrag.Dispose();
				FDrag = null;
			}
		}

		private void Dispose(ref Image aImg)
		{
			if (aImg != null)
			{
				aImg.Dispose();
				aImg = null;
			}
		}
		private void Dispose(ref Graphics agr)
		{
			if (agr != null)
			{
				agr.Dispose();
				agr = null;
			}
		}
		
		private void RecalcMainDisplayIndex()
		{
			if (FMainColumnIndex>= 0 && FMainColumnIndex < Count)
				FMainColumnDisplayIndex = FDisplays.IndexOf(this.FColonnes[FMainColumnIndex]);
			else
				FMainColumnDisplayIndex = -1;
		}
		#endregion

		#region méthodes interne
		internal bool HandleMessage(Message m)
		{
			Point p;
			switch ((Msgs)m.Msg)
			{
				case Msgs.WM_NCLBUTTONDOWN :
					if (!FTree.Focused)
						FTree.Focus();
					FTree.GetTimers.Stop(KindTimer.Edit);
					FDragStart = Cursor.Position;
					p = FTree.PointToClient(FDragStart);
					if (p.Y < 0)
					{
						if (DetermineSplitterIndex(p))
						{
							FTree.Cursor = System.Windows.Forms.Cursors.VSplit;
							//FTrackStart = p;
							Include(ref FStates, HeaderStateEnum.TackPending);
							FTree.Capture = true;
						}
						else
						{
							int HitIndex = AdjustDownColumn(p);
							if (HitIndex > Constantes.NoColumn)
							{
								if (AllowDrag && this.DisplayColonnes(HitIndex).AllowDrag)
								{
									Include(ref FStates, HeaderStateEnum.DragPending);
									FTree.Capture = true;
								}
								else if (!AllowSort)
									FDownIndex = Constantes.NoColumn;
							}
							//Debug.WriteLine(HitIndex);
							//Debug.WriteLine(FDownIndex);
						}
					}
					break;
				case Msgs.WM_LBUTTONUP :
				case Msgs.WM_NCLBUTTONUP :
					if (FStates != 0)
					{
						FTree.GetTimers.Stop(KindTimer.Edit);
						if (Contains(FStates, HeaderStateEnum.Dragging))
						{
							EndDrag();
							FTree.Capture = false;
							if (FDropTarget > Constantes.NoColumn && FDropTarget != FDragIndex)
							{
								if (FTree.SelectedColumn == FDragIndex)
									FTree.InternalSelectedColumn = FDropTarget;
								else if (FTree.SelectedColumn != Constantes.NoColumn)
								{
									if (FTree.SelectedColumn >= FDropTarget && FTree.SelectedColumn < FDragIndex)
										FTree.InternalSelectedColumn++;
									else if (FTree.SelectedColumn > FDragIndex && FTree.SelectedColumn <= FDropTarget )
										FTree.InternalSelectedColumn--;
								}
								if (FDropTarget > FDragIndex)
									FDropTarget--;
								if (FDropBefore)
									MoveColonneTo(FDragIndex, FDropTarget);
								else
									MoveColonneTo(FDragIndex, FDropTarget+1);

								FTree.InvalidateTree();
							}
							AdjustDropTargetColumn(new Point(-1,-1));
						}
						FStates = 0;
					}
					if (FDownIndex > Constantes.NoColumn && AllowSort)
					{
						int aDisplayCol = FDownIndex;
						int acol = this.DisplayIndexToIndex(FDownIndex);
						FDownIndex = Constantes.NoColumn;
						Invalidate(this.DisplayColonnes(acol));
						if (FSortColumn == acol)
						{
							if (FSortDirection == SortDirection.Ascending)
								FSortDirection = SortDirection.Descending;
							else
								FSortDirection = SortDirection.Ascending;
						}
						else
						{
							FSortDirection = SortDirection.Ascending;
						}
						FSortColumn = acol;
						FTree.SortTree(aDisplayCol, FSortDirection);
					}
					break;
				case Msgs.WM_MOUSELEAVE :
					break;
				case Msgs.WM_NCMOUSEMOVE :
					p = FTree.PointToClient(Cursor.Position);
					if (p.Y < 0)
					{
						if (DetermineSplitterIndex(p))
						{
							FTree.Cursor = System.Windows.Forms.Cursors.VSplit;
							m.Result = new IntPtr(1);
							return true;
						}
						AdjustHoverColumn(p);
					}
					break;
				case Msgs.WM_MOUSEMOVE :
					if (FStates != 0)
					{
						if (Contains(FStates, HeaderStateEnum.TackPending))
						{
							Exclude(ref FStates, HeaderStateEnum.TackPending);
							Include(ref FStates, HeaderStateEnum.Tracking);
							m.Result = new IntPtr(0);
							return true;
						}
						else if (Contains(FStates, HeaderStateEnum.Tracking))
						{
							FTree.Cursor = System.Windows.Forms.Cursors.VSplit;
							p = FTree.PointToClient(Cursor.Position);
							//Debug.WriteLine("Tracking : " + FTrackIndex.ToString() + " :" + (p.X - FLeftTrackPos).ToString());
							DisplayColonnes(FTrackIndex).Width = p.X - FLeftTrackPos; 
							FTree.UpdateHorizontalScrollBar();
							FTree.InvalidateTree();
							m.Result = new IntPtr(0);
							return true;
						}
						else if (Contains(FStates, HeaderStateEnum.DragPending))
						{
							p = Cursor.Position;
							if (Math.Abs(FDragStart.X - p.X) > 4 ||
								Math.Abs(FDragStart.Y - p.Y) > 4)
							{
								int aDownIndex = FDownIndex;
								FHoverIndex = Constantes.NoColumn;
								FDownIndex = Constantes.NoColumn;
								if (aDownIndex > Constantes.NoColumn)
									Invalidate(this.DisplayColonnes(aDownIndex));
									//Invalidate(this.FColonnes[aDownIndex]);
								PrepareDrag(FDragStart);
								Exclude(ref FStates, HeaderStateEnum.DragPending);
								Include(ref FStates, HeaderStateEnum.Dragging);
								m.Result = new IntPtr(1);
								return true;
							}
						}
						else if (Contains(FStates, HeaderStateEnum.Dragging))
						{
							p = FTree.PointToClient(Cursor.Position);
							//Debug.WriteLine("dragging : " + p.ToString());
							DragTo(Cursor.Position);
							AdjustDropTargetColumn(p);
							m.Result = IntPtr.Zero;
							return true;
						}
					}
					else
					{
						AdjustHoverColumn(new Point(-9999,-1));
					}
					break;
			}
			return false;
		}
		#endregion

		/// <summary>
		/// ajout d'une colonne dans le header
		/// </summary>
		/// <returns></returns>
		public GeniusTreeViewColonne Add()
		{
			GeniusTreeViewColonne n = new GeniusTreeViewColonne(this);
			AddInstance(n);
			return n;
		}

		private int AddInstance(GeniusTreeViewColonne aCol)
		{
			int Result = this.FColonnes.Add(aCol);
			return Result;
		}

		/// <summary>
		/// retourne la largeur total de toutes les colonnes
		/// précédentes ansi que aDisplayIndex
		/// </summary>
		/// <param name="aDisplayIndex"></param>
		/// <returns>la largeur totale des colonnes includant aDisplayIndex</returns>
		public int GetWidthFromStart(int aDisplayIndex)
		{
			int Result = 0;
			if (aDisplayIndex >= Count || aDisplayIndex < 0)
				return 0;
			for(int i=0; i <= aDisplayIndex; i++)
			{
				if (!DisplayColonnes(i).Visible)
					continue;
				Result += DisplayColonnes(i).Width;
			}
			return Result;			
		}

		/// <summary>
		/// renvoi la position gauche d'une colonne
		/// </summary>
		/// <param name="aDisplayIndex"></param>
		/// <returns></returns>
		public int Left(int aDisplayIndex)
		{
			return GetWidthFromStart(aDisplayIndex-1);
		}
		
		/// <summary>
		/// renvoi la position gauche d'une colonne
		/// </summary>
		/// <param name="aCol"></param>
		/// <returns></returns>
		public int Left(GeniusTreeViewColonne aCol)
		{
			return Left(FDisplays.IndexOf(aCol));
		}

		/// <summary>
		/// renvoi l'index d'une colonne
		/// </summary>
		/// <param name="aCol"></param>
		/// <returns></returns>
		public int IndexOf(GeniusTreeViewColonne aCol)
		{
			return this.FColonnes.IndexOf(aCol);
		}

		/// <summary>
		/// renvoi l'index visuel d'une colonne
		/// </summary>
		/// <param name="aCol"></param>
		/// <returns></returns>
		public int IndexOfDisplay(GeniusTreeViewColonne aCol)
		{
			return FDisplays.IndexOf(aCol);
		}

		/// <summary>
		/// quelle colonne sous x
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public GeniusTreeViewColonne ColumnAt(int x)
		{
			int startx = 0;
			int width;

			foreach (GeniusTreeViewColonne col in this.FDisplays)
			{
				if (!col.Visible)
					continue;
				width = col.Width;
				if (x >= startx && x <= startx+ width)
					return col;
				startx += width;
			}
			return null;
		}

		/// <summary>
		/// quelle index de colonne sous x
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public int ColumnIndexAt(int x)
		{
			int startx = 0;
			int width;

			for (int i=0; i < Count; i++)
			{
				if (!DisplayColonnes(i).Visible)
					continue;
				width = DisplayColonnes(i).Width;
				if (x >= startx && x <= startx+ width)
					return i;
				startx += width;
			}
			return Constantes.NoColumn;
		}

		/// <summary>
		/// renvoi la indexième colonne du tableau des DisplayColonnes
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public GeniusTreeViewColonne DisplayColonnes(int index)
		{
			return (GeniusTreeViewColonne)FDisplays[index];
		}

		/// <summary>
		/// retourne index d'une colonne par rapport à son index du tableau des 
		/// DisplayColonnes
		/// </summary>
		/// <param name="aDisplayIndex"></param>
		/// <returns></returns>
		public int DisplayIndexToIndex(int aDisplayIndex)
		{
			return IndexOf(DisplayColonnes(aDisplayIndex));
		}

		/// <summary>
		/// l'ensemble des colonnes visible, dans l'ordre des colonnes affichées
		/// </summary>
		[Browsable(false)]
		public ArrayList VisibleColumns
		{
			get
			{
				ArrayList Result = new ArrayList();
				foreach (GeniusTreeViewColonne col in FDisplays)
				{
					if (col.Visible)
						Result.Add(col);
				}
				return Result;
			}
		}

		/// <summary>
		/// liste des colonne affichés, identique aux colonnes à l'ordre près
		/// </summary>
		[Browsable(false)]
		public ArrayList Displays
		{
			get
			{
				return FDisplays;
			}
		}

		/// <summary>
		/// index de la colonne triée
		/// </summary>
		[Browsable(false)]
		public int SortColumn
		{
			get
			{
				return FSortColumn;
			}
		}

		[Browsable(false)]
		public SortDirection SortingDirection
		{
			get
			{
				return FSortDirection;
			}
		}

		[DefaultValue(null)]
		public Font Font
		{
			get
			{
				if (FFont == null)
					return FTree.Font;
				return FFont;
			}
			set
			{
				if (FFont != value)
				{
					FFont = value;
					Invalidate();
				}
			}
		}

		/// <summary>
		/// ajoute un tableau de colonne
		/// </summary>
		/// <param name="range"></param>
		public void AddRange(GeniusTreeViewColonne[] range)
		{
			foreach(GeniusTreeViewColonne col in range)
			{
				AddInstance(col);
			}
			RecalcMainDisplayIndex();
		}

		#region méthodes publiques
		internal void SetMainColumnIndex(int amain)
		{
			FMainColumnIndex = amain;
			RecalcMainDisplayIndex();
		}

		internal void SetSortColumn(int aCol, SortDirection aDirection)
		{
			FSortColumn = aCol;
			FSortDirection = aDirection;
		}

		[Browsable(false)]
		public int MainColumnIndex
		{
			get
			{
				return FMainColumnIndex;
			}
			set
			{
				SetMainColumnIndex(value);
			}
		}

		[Browsable(false)]
		public int MainColumnDisplayIndex
		{
			get
			{
				return FMainColumnDisplayIndex;
			}
		}

		[Browsable(false)]
		public int Width
		{
			get
			{
				int Result = 0;
				foreach(GeniusTreeViewColonne col in this.FColonnes)
				{
					if (col.Visible)
						Result += col.Width;
				}
				return Result;
			}
		}

		internal void SetClickIndex(int aclick)
		{
			FClickIndex = aclick;
		}

		[Browsable(false)]
		public int ClickIndex
		{
			get
			{
				return FClickIndex;
			}
		}

		[DefaultValue(false)]
		[Description("Permettre le drag and drop des colonnes")]
		public bool AllowDrag
		{
			get
			{
				return FAllowDrag;
			}
			set
			{
				FAllowDrag = value;
			}
		}

		[DefaultValue(true)]
		[Description("Permettre le tri des colonnes")]
		public bool AllowSort
		{
			get
			{
				return FAllowSort;
			}
			set
			{
				FAllowSort = value;
			}
		}

		public void SwapColonnes(int index1, int index2)
		{
			object aCol = FDisplays[index1];
			FDisplays[index1] = FDisplays[index2];
			FDisplays[index2] = aCol;
			RecalcMainDisplayIndex();
		}

		/// <summary>
		/// deplace une colonne, vers une position
		/// </summary>
		/// <param name="aDisplayStart">index de la colonne à déplacer, index visuel</param>
		/// <param name="aDisplayTo">index </param>
		public void MoveColonneTo(int aDisplayStart, int aDisplayTo)
		{
			if (aDisplayTo < 0 || aDisplayTo >= FDisplays.Count || aDisplayStart < 0 || aDisplayStart > FDisplays.Count)
				throw new Exception(String.Format("Impossible de déplacer la colonne {0}, index d'arrivé \"{1}\" invalide", aDisplayStart, aDisplayTo));
			object o = FDisplays[aDisplayStart];
			FDisplays.RemoveAt(aDisplayStart);
			if (aDisplayTo < aDisplayStart)
				FDisplays.Insert(aDisplayTo, o);
			else
			{
				if (aDisplayStart >= FDisplays.Count)
					FDisplays.Add(o);
				else
					FDisplays.Insert(aDisplayTo, o);
			}
			RecalcMainDisplayIndex();
		}
		#endregion

		#region IDisposable Members

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			FColonnes.Clear();
		}

		#endregion

		#region Paint
		#region dessin du header

		private void InternalDrawHeaderCol(Graphics g, GeniusTreeViewColonne aCol, Rectangle rCol)
		{
			bool defaultDrawing = true;
			if (OnDrawHeaderCol != null)
			{
				DrawHeaderColEventArgs args = new DrawHeaderColEventArgs(g, aCol, rCol);
				OnDrawHeaderCol(FTree, args);
				defaultDrawing = args.DefaultDrawing;
				
			}
			GeniusLinearGradientBrush colColor = (!aCol.HeadColor.IsEmpty) ? aCol.HeadColor : FTree.Colors.HeaderColor;
			using (Brush br = colColor.GetBrush(rCol))
			{
				g.FillRectangle(br, rCol);
				g.DrawLine(Pens.LightGray, rCol.Right-2, rCol.Top+4, rCol.Right-2, rCol.Bottom-4);
				g.DrawLine(Pens.WhiteSmoke, rCol.Right-1, rCol.Top+4, rCol.Right-1, rCol.Bottom-4);
			}
			StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
			sf.Trimming = StringTrimming.EllipsisCharacter;
			sf.Alignment = aCol.Alignment;
			sf.LineAlignment = aCol.VAlignment;
			if (aCol.ForeColor.IsEmpty)
				aCol.ForeColor = new GeniusLinearGradientBrush(Color.Black);
			using (Brush br = aCol.ForeColor.GetBrush(rCol))
			{
				g.DrawString(aCol.Text, aCol.Font, br, rCol, sf);
			}
			if (FDownIndex == IndexOfDisplay(aCol))
			{
				Rectangle rColBis = rCol;
				rColBis.Width -= 1;
				rColBis.Height -= 1;
				g.DrawRectangle(Pens.Black, rColBis);
			}
			/*
			//il faut trouver une solution quand on la souris quitte le treeview, afin d'effacer le hottrack
			//MouseLeave ne fonctionne pas
			if (FHoverIndex == IndexOfDisplay(aCol))
			{
				Rectangle rColBis = rCol;
				int h = rColBis.Height / 4;
				rColBis.Offset(0, rCol.Height - h);
				rColBis.Height = h;
				g.FillRectangle(Brushes.Beige, rColBis);
			}
			*/
			if (FSortColumn == IndexOf(aCol))
			{
				int y = rCol.Height / 2 - 8;
				int x ;
				
				x = rCol.Right - 20;
				FImageDropTarget.Draw(g, x, y, FSortDirection == SortDirection.Ascending ? 2 : 3);				
			}
			if (FDropTarget == IndexOfDisplay(aCol))
			{
				int y = rCol.Height / 2 - 8;
				int x ;
				
				if (FDropBefore)
					x = rCol.Left;
				else
					x = rCol.Right - 16;
				
				DrawDropTarget(g, new Point(x, y));
			}
		}

		private void DrawHeader(Graphics g, Rectangle r, int offsetx)
		{
			Rectangle rCol = r;
			int fixedWidth = FixedColumnWidth;
			rCol.Offset(offsetx, 0);
			int i = -1;
			foreach( GeniusTreeViewColonne col in FDisplays)
			{
				if (!col.Visible)
					continue;
				i++;
				rCol.Width = col.Width;
				if (r.IntersectsWith(rCol) && rCol.Width > 0 && i >= FixedColumnCount && rCol.Right > fixedWidth)
					InternalDrawHeaderCol(g, col, rCol);
				rCol.Offset(col.Width, 0);
			}
			if (FixedColumnCount > 0)
			{
				rCol = r;
				i = 0;
				foreach( GeniusTreeViewColonne col in FDisplays)
				{
					if (!col.Visible)
						continue;
					if (i < FixedColumnCount)
					{						
						rCol.Width = col.Width;
						if (r.IntersectsWith(rCol) && rCol.Width > 0)
							InternalDrawHeaderCol(g, col, rCol);
						rCol.Offset(col.Width, 0);
						i++;
					}
					else
						break;
				}
			}
		}
		#endregion

		internal void PaintHeader(Graphics g, Rectangle r, int offsetx)
		{
			Bitmap bmp = new Bitmap(r.Width, r.Height);
			using (Graphics gBmp = Graphics.FromImage(bmp))
			{
				using (Brush br = FTree.Colors.HeaderColor.GetBrush(r))
				{
					gBmp.FillRectangle(br, r);
				}
				DrawHeader(gBmp, r, offsetx);
				g.DrawImage(bmp, 0,0);
			}
		}

		internal void Redraw(bool updatescrolls)
		{
			if (updatescrolls)
			{
				FTree.UpdateScrollBars(false);
				FTree.Invalidate();
			}
			//FlagsSetWindowPos.SWP_FRAMECHANGED provoque un NC_CalcSize
			NativeMethods.SetWindowPos(FTree.Handle, IntPtr.Zero,0,0,0,0, FlagsSetWindowPos.SWP_FRAMECHANGED |
				FlagsSetWindowPos.SWP_NOMOVE |FlagsSetWindowPos.SWP_NOSIZE | FlagsSetWindowPos.SWP_NOZORDER | FlagsSetWindowPos.SWP_NOACTIVATE);
		}
		
		public void Invalidate()
		{
			Invalidate(FTree.Bounds);
		}

		public void Invalidate(Rectangle rBounds)
		{
			Rectangle r = rBounds;
			RECT RW;

			r.Height = FTree.HeaderHeight;
			r.Y = - r.Height;
			RW = NativeMethods.RectangleToRECT(r);
			NativeMethods.RedrawWindow(FTree.Handle, ref RW, IntPtr.Zero, FlagsRDW.RDW_FRAME | FlagsRDW.RDW_INVALIDATE |
				FlagsRDW.RDW_NOERASE); 
		}
		
		public void Invalidate(GeniusTreeViewColonne aCol)
		{
			int left = Left(aCol);
			Rectangle r = new Rectangle(left, 0, aCol.Width-10, FTree.HeaderHeight);
			Invalidate(r);
		}
		#endregion

		/// <summary>
		/// renvoi le nombre de colonnes
		/// </summary>
		[Browsable(false)]
		public int Count
		{
			get
			{
				return FColonnes.Count;
			}
		}

		/// <summary>
		/// renvoi la liste des colonnes définies
		/// </summary>
		[Browsable(false)]
		public Colonnes Colonnes
		{
			get
			{
				return FColonnes;
			}
		}

		/// <summary>
		/// renvoi la liste des colonnes dans l'ordre d'affichage
		/// </summary>
		/// <returns></returns>
		public GeniusTreeViewColonne[] GetDisplays()
		{
			return (GeniusTreeViewColonne[])FDisplays.ToArray(typeof(GeniusTreeViewColonne));
		}
	}
}

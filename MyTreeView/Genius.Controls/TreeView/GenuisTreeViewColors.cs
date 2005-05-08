#region Imports
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Genius.Controls.TreeView.Colors;
using Genius.Controls.TreeView.Converter;

#endregion

namespace Genius.Controls
{
	/// <summary>
	/// classe de gestion de l'aspect du <see cref="GeniusTreeView"/>
	/// Tous les <see cref="Pen"/>, <see cref="Brush"/>, sont disposés lors du Dispose de cette classe
	/// </summary>
	public class GenuisTreeViewColors : IDisposable
	{
		private Pen FLinesColor;
		private Pen FGridLinesColor;
		private Pen FSignaledColor;
		private Pen FFocusedRectangleColor;
		private Pen FUnFocusedRectangleColor;
		private GeniusLinearGradientBrush FHeaderColor;
		private GeniusLinearGradientBrush FSelectedColor;
		private GeniusLinearGradientBrush FTextColor;
		private GeniusLinearGradientBrush FSelectedTextColor; 
		private GeniusLinearGradientBrush FSelectedUnfocusedColor;

		public GenuisTreeViewColors()
		{
			LinesColor = new Pen(Color.Gray, 1);
			LinesColor.DashStyle = DashStyle.Dot;
			GridLinesColor = new Pen(Color.Gray, 1);
			HeaderColor = new GeniusLinearGradientBrush(Color.White, Color.LightGray, 90);			
			TextColor = new GeniusLinearGradientBrush(Color.Black);
			SelectedTextColor = new GeniusLinearGradientBrush(((SolidBrush)SystemBrushes.HighlightText).Color);
			SelectedColor = new GeniusLinearGradientBrush(((SolidBrush)SystemBrushes.Highlight).Color);
			SignaledColor = (Pen)Pens.Orange.Clone();
			SignaledColor.Width = 2;
			SignaledColor.EndCap = LineCap.Round;
			FSelectedUnfocusedColor =  new GeniusLinearGradientBrush((SystemBrushes.InactiveCaption as SolidBrush).Color);
			FFocusedRectangleColor = new Pen(SelectedTextColor.GetBrush(), 1);
			FFocusedRectangleColor.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			FUnFocusedRectangleColor = (Pen)((ICloneable)FFocusedRectangleColor).Clone();
		}

		private void Dispose(IDisposable color)
		{
			if (color != null)
				color.Dispose();
		}

		private GeniusPen PenToGeniusPen(Pen pen)
		{
			GeniusPen Result = new GeniusPen(pen.Color, pen.Width, pen.DashStyle);
			return Result;
		}

		[Description("Couleur des lignes de l'arbre reliant les noeuds")]
		[Browsable(false)]
		public Pen LinesColor
		{
			get
			{
				return FLinesColor;
			}
			set
			{
				FLinesColor = value;
			}
		}

		[Description("Couleur des lignes de la grille")]
		[Browsable(false)]
		public Pen GridLinesColor
		{
			get
			{
				return FGridLinesColor;
			}
			set
			{
				FGridLinesColor = value;
			}
		}

		[Description("couleur de l'entête de colonne")]
		public GeniusLinearGradientBrush HeaderColor
		{
			get
			{
				return FHeaderColor;
			}
			set
			{
				FHeaderColor = value;
			}
		}

		[Description("Couleur du Texte")]
		public GeniusLinearGradientBrush TextColor
		{
			get
			{
				return FTextColor;
			}
			set
			{
				FTextColor = value;
			}
		}

		[Description("Couleur du texte quand le noeud est selectionné")]
		public GeniusLinearGradientBrush SelectedTextColor
		{
			get
			{
				return FSelectedTextColor;
			}
			set
			{
				FSelectedTextColor = value;
			}
		}

		[Description("Couleur de fond quand le noeud est selectionné")]
		public GeniusLinearGradientBrush SelectedColor
		{
			get
			{
				return FSelectedColor;
			}
			set
			{
				FSelectedColor = value;
			}
		}

		[Description("Couleur de fond d'un noeud selectionné, non focusé")]
		public GeniusLinearGradientBrush SelectedUnfocusedColor
		{
			get
			{
				return FSelectedUnfocusedColor;
			}
			set
			{
				FSelectedUnfocusedColor = value;
			}
		}

		[Browsable(false)]
		public Pen	SignaledColor
		{
			get
			{
				return FSignaledColor;
			}
			set
			{
				FSignaledColor = value;
			}
		}

		[Description("Couleur Stylo pour dessiner le chemin des noeuds signalé")]
		public GeniusPen SignaledPenColor
		{
			get
			{
				return PenToGeniusPen(FSignaledColor);
			}
			set
			{
				FSignaledColor = new Pen(value.Color, value.Width);
				FSignaledColor.DashStyle = value.Style;
			}
		}

		/// <summary>
		/// stylo utilisé pour dessiné la selection focused,
		/// le rectangle autour de la selection
		/// </summary>
		[Browsable(false)]
		public Pen FocusedRectangleColor
		{
			get
			{
				return FFocusedRectangleColor;
			}
			set
			{
				FFocusedRectangleColor = value;
			}
		}

		/// <summary>
		/// <see cref="FocusedRectangleColor"/>
		/// </summary>
		[Description("Stylo utilisé pour la selection focused")]
		public GeniusPen FocusedRectanglePenColor
		{
			get
			{
				return PenToGeniusPen(FFocusedRectangleColor);
			}
			set
			{
				FFocusedRectangleColor	 = new Pen(value.Color, value.Width);
				FFocusedRectangleColor.DashStyle = value.Style;
			}
		}

		/// <summary>
		/// Stylo utilisé pour dessiner le focusRect quand le controle
		/// n'est pas "Focused"
		/// </summary>
		[Browsable(false)]
		public Pen UnFocusedRectangleColor
		{
			get
			{
				return FUnFocusedRectangleColor;
			}
			set
			{
				FUnFocusedRectangleColor = value;
			}
		}

		/// <summary>
		/// <see cref="UnFocusedRectangleColor"/>
		/// </summary>
		[Description("Stylo utilisé pour la selection focused, quand le treeview n'a plus le focus")]
		public GeniusPen UnFocusedRectanglePenColor
		{
			get
			{
				return PenToGeniusPen(FUnFocusedRectangleColor);
			}
			set
			{
				FUnFocusedRectangleColor = new Pen(value.Color, value.Width);
				FUnFocusedRectangleColor.DashStyle = value.Style;
			}
		}
		#region IDisposable Members

		/// <summary>
		/// dispose tous les objets "Couleurs"
		/// </summary>
		public void Dispose()
		{
			Dispose(LinesColor as IDisposable);
			Dispose(GridLinesColor as IDisposable);
			Dispose(HeaderColor as IDisposable);
			Dispose(TextColor as IDisposable);
			Dispose(SelectedTextColor as IDisposable);
			Dispose(SelectedColor as IDisposable);
			Dispose(SignaledColor as IDisposable);
			Dispose(FocusedRectangleColor as IDisposable);
			Dispose(UnFocusedRectangleColor as IDisposable);
		}

		#endregion
	}
}

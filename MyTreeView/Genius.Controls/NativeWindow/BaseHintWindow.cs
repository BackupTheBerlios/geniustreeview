using System;
using System.Drawing;
using System.Windows.Forms;
using Genius.Controls.TreeView;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls.NativeWindow
{
	using GeniusTreeView = Control;
	/// <summary>
	/// fen�tre de base pour la gestion des hint
	/// </summary>
	public class BaseHintWindow : BaseNativeWindow, IHintWindow
	{
		private int FDisplayColumn;
		private string FText;
		private Font FFont;
		private bool FCalculateSizeNeeded;
		private INode FNode;


		public BaseHintWindow(GeniusTreeView tv) : base(tv)
		{
			FFont = (Font)tv.Font.Clone();
			FCalculateSizeNeeded = false;
		}

		private void DoCalculateSize()
		{
			if (HandleCreated)
			{
				FCalculateSizeNeeded = false;
				using (Graphics g = Graphics.FromHwnd(this.Handle))
				{
					this.WindowSize = CalculateSize(g);
				}
			}
			else
			{
				FCalculateSizeNeeded = true;
			}
		}

		#region m�thodes prot�g�es
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == (int)Msgs.WM_NCHITTEST)
				m.Result = new IntPtr((int)HitTest.HTTRANSPARENT);
			else
				base.WndProc (ref m);
		}

		/// <summary>
		/// � surcharger par les descendant pour renvoyer la taille de la fen�tre de
		/// hint
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		protected virtual Size CalculateSize(Graphics g)
		{
			SizeF size = g.MeasureString(this.Text, this.Font);
			return size.ToSize() + new Size(2,2);	
		}

		/// <summary>
		/// � surcharger par les descendants, pour customiser le paint
		/// </summary>
		/// <param name="g"></param>
		protected override void DoPaint(Graphics g)
		{
			Rectangle r = this.ClientRectangle;
			g.FillRectangle(SystemBrushes.Info, r);
			g.DrawString(FText, FFont, Brushes.Black, r);
			r.Width -= 1;
			r.Height -=1;
			g.DrawRectangle(Pens.Black, r);
		}

		protected override void AfterHandleCreated()
		{
			if (FCalculateSizeNeeded)
				DoCalculateSize();
		}

		#endregion

		/// <summary>
		/// la font utilis�e pour le hint
		/// </summary>
		public Font Font
		{
			get
			{
				return FFont;
			}
			set
			{
				FFont = value;
			}
		}

		#region IHintWindow Members

		/// <summary>
		/// colonne en cours du display
		/// </summary>
		public int DisplayColumn
		{
			get
			{
				return FDisplayColumn;
			}
			set
			{
				FDisplayColumn = value;
			}
		}

		/// <summary>
		/// le texte � afficher dans le hint
		/// </summary>
		public string Text
		{
			get
			{
				return FText;
			}
			set
			{
				FText = value;
				DoCalculateSize();
				Invalidate();
			}
		}

		/// <summary>
		/// le noeud concerner par le hint
		/// </summary>
		public INode Node
		{
			get
			{
				return FNode;
			}
			set
			{
				FNode = value;
			}
		}

		/// <summary>
		/// afficher le hint
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public virtual void ShowHint(int x, int y)
		{
			Rectangle scr = Screen.FromHandle(this.Handle).WorkingArea;
			
			Position = new Point(x,y);
			Point pt = new Point(x, y);
			//recalcul de la position si le bas d�passe de l'�cran
			Rectangle rWindow = this.WindowRect;
			int ay = rWindow.Bottom;
			int ax = rWindow.Right;
			if (ay > scr.Bottom)
				pt.Y -= ay - scr.Bottom;
			if (ax > scr.Right)
				pt.X -= ax - scr.Right;
			Position = pt;
			if (!Visible)
				Visible = true;
		}

		/// <summary>
		/// cacher le hint
		/// </summary>
		public virtual void Hide()
		{
			Visible = false;
		}

		/// <summary>
		/// le hint est-il visible
		/// </summary>
		public bool IsVisible
		{
			get
			{
				return Visible;
			}
		}

		/// <summary>
		/// dispose du hint
		/// </summary>
		public override void Dispose()
		{
			if (FFont != null)
				FFont.Dispose();
			FFont = null;
			base.Dispose ();
		}

		#endregion
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	/// <summary>
	/// déléguée pour le paint du hint
	/// </summary>
	public delegate void OnPaintHintDelegate(HintPaintEventArgs e);

	/// <summary>
	/// déléguée pour la taille du hint
	/// </summary>
	public delegate void OnGetHintSizeDelegate(out Size aSizeHint);
	/// <summary>
	/// Summary description for HintForm.
	/// </summary>
	class HintForm : GeniusNativeWindow
	{
		private string FText;
		private Font	FFont;

		public Node NodeHint;
		public int NodeHintColumn;

		public HintForm(GeniusTreeView tv) : base(tv)
		{
			FFont = new Font(FTree.Font.FontFamily, 8.25f);
		}

		public event OnPaintHintDelegate OnPaintHint;
		public event OnGetHintSizeDelegate OnGetHintSize;

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == (int)Msgs.WM_NCHITTEST)
				m.Result = new IntPtr((int)HitTest.HTTRANSPARENT);
			else
				base.WndProc (ref m);
		}


		protected override void DoPaint(Graphics g)
		{
			if (OnPaintHint != null)
			{
				HintPaintEventArgs e = new HintPaintEventArgs(NodeHint, NodeHintColumn);
				e.Text = FText;
				e.graphics = g;
				OnPaintHint(e);
			}
			else
			{
				Rectangle r = this.ClientRectangle;
				g.FillRectangle(SystemBrushes.Info, r);
				g.DrawString(FText, FFont, Brushes.Black, r);
				r.Width -= 1;
				r.Height -=1;
				g.DrawRectangle(Pens.Black, r);
			}
		}
	
		public string Text
		{
			get
			{
				return FText;
			}
			set
			{
				if (value != FText)
				{
					FText = value;
					Size SizeHint = new Size(0,0);
					if (OnGetHintSize != null)
						OnGetHintSize(out SizeHint);
					else
					{
						using( Graphics g = Graphics.FromHwnd(this.Handle))
						{
							SizeF size = g.MeasureString(FText, this.FFont);
							SizeHint = new Size((int)size.Width + 2, (int)size.Height + 2);
						}
					}
					SetSize(SizeHint.Width, SizeHint.Height);
					Invalidate();
				}
			}
		}

		public override void Dispose()
		{
			base.Dispose ();
			FFont.Dispose();
		}
	}
}

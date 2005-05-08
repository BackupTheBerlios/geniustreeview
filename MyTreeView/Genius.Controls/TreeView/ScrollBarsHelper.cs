#region Imports
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
#endregion

namespace Genius.Controls
{
	/// <summary>
	/// classe Helper pour la gestion des scrollbar d'une fenêtre
	/// </summary>
	[EditorBrowsable()]
	public class ScrollBarsHelper
	{
		#region variables
		private Control FControl;
		private ScrollBars FScrollBars;
		private int FRangeX;
		private int FRangeY;
		private int FOffsetX;
		private int FOffsetY;
		private bool FScrollBarsAlwaysVisible;
		#endregion

		#region contructeurs
		public ScrollBarsHelper(Control aControl)
		{
			FControl = aControl;
			if (FControl != null)
				FControl.Resize +=new EventHandler(Control_Resize);
		}
		#endregion

		#region méthodes protégées
		protected int ScrollMask(bool value)
		{
			if (value)
				return NativeMethods.SIF_DISABLENOSCROLL;
			else
				return 0;
		}

		protected IntPtr Handle
		{
			get
			{
				return FControl.Handle;
			}
		}

		protected Rectangle ClientRectangle
		{
			get
			{
				return FControl.ClientRectangle;
			}
		}

		protected void UpdateScrollBars()
		{
			UpdateHorizontalScrollBar(true);
			UpdateVerticalScrollBar(true);
		}

		protected void UpdateHorizontalScrollBar(bool aRepaint)
		{
			if (FScrollBars == ScrollBars.Both || FScrollBars == ScrollBars.Horizontal)
			{
				SCROLLINFO info = new SCROLLINFO();
				info.fMask = NativeMethods.SIF_ALL;
				NativeMethods.GetScrollInfo(this.Handle, NativeMethods.SB_HORZ, ref info);
				int diffX = FRangeX - ClientRectangle.Width ;
				if (diffX > 0 || ScrollBarsAlwaysVisible)
				{
					info.nMin = 0;
					info.nMax = FRangeX;
					info.nPos = FOffsetX;
					info.nPage = Math.Max(0, ClientRectangle.Width + 1);
					info.fMask = NativeMethods.SIF_ALL | ScrollMask(ScrollBarsAlwaysVisible);
					NativeMethods.SetScrollInfo(this.Handle, NativeMethods.SB_HORZ, ref info, aRepaint);
					NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_HORZ, true);

					//recalcul de l'offset pour éviter
					if (FOffsetX > 0 && diffX - FOffsetX < 0)
						FOffsetX -= (FOffsetX-diffX);
					if (FOffsetX < 0)
						FOffsetX = 0;
				}
				else
				{
					info.nMin = 0;
					info.nMax = 0;
					info.nPos = 0;
					info.nPage = 0;
					NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_HORZ, false);
					NativeMethods.SetScrollInfo(this.Handle, NativeMethods.SB_HORZ, ref info, false);
				}
			}
			else
			{
				NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_HORZ, false);
			}
		}

		protected void UpdateVerticalScrollBar(bool aRepaint)
		{
			if (FScrollBars == ScrollBars.Both || FScrollBars == ScrollBars.Vertical)
			{
				SCROLLINFO info = new SCROLLINFO();
				info.fMask = NativeMethods.SIF_ALL;
				NativeMethods.GetScrollInfo(this.Handle, NativeMethods.SB_VERT, ref info);
				if (FRangeY > ClientRectangle.Height || ScrollBarsAlwaysVisible)
				{
					info.nMin = 0;
					info.nMax = FRangeY;
					info.nPos = FOffsetY;
					info.nPage = Math.Max(0, ClientRectangle.Height + 1);
					info.fMask = NativeMethods.SIF_ALL | ScrollMask(ScrollBarsAlwaysVisible);
					NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_VERT, true);
					NativeMethods.SetScrollInfo(this.Handle, NativeMethods.SB_VERT, ref info, aRepaint);
				}
				else
				{
					info.nMin = 0;
					info.nMax = 0;
					info.nPos = 0;
					info.nPage = 0;
					NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_VERT, false);
					NativeMethods.SetScrollInfo(this.Handle, NativeMethods.SB_VERT, ref info, false);
				}
			}
			else
			{
				NativeMethods.ShowScrollBar(this.Handle, NativeMethods.SB_VERT, false);
			}
		}
		#endregion

		#region Propriétés
		[Browsable(true)]
		public ScrollBars ScrollBars
		{
			get {return FScrollBars;}
			set 
			{
				if (FScrollBars != value)
				{
					FScrollBars = value;
				}
			}
		}

		[Browsable(true)]
		public int RangeX
		{
			get
			{
				return FRangeX;
			}
			set
			{
				if (FRangeX != value)
				{
					FRangeX = value;
					UpdateHorizontalScrollBar(true);
				}
			}
		}

		[Browsable(true)]
		public int RangeY
		{
			get
			{
				return FRangeY;
			}
			set
			{
				if (FRangeY != value)
				{
					FRangeY = value;
					UpdateVerticalScrollBar(true);
				}
			}
		}

		[Browsable(true)]
		public int OffsetX
		{
			get
			{
				return FOffsetX;
			}
			set
			{
				if (OffsetX != value)
				{
					FOffsetX = value;
					UpdateHorizontalScrollBar(true);
				}
			}
		}

		[Browsable(true)]
		public int OffsetY
		{
			get
			{
				return FOffsetY;
			}
			set
			{
				if (FOffsetY != value)
				{
					FOffsetY = value;
					UpdateVerticalScrollBar(true);
				}
			}
		}

		[Browsable(true)]
		public bool ScrollBarsAlwaysVisible
		{
			get 
			{
				return FScrollBarsAlwaysVisible;
			}
			set
			{
				if (FScrollBarsAlwaysVisible != value)
				{
					FScrollBarsAlwaysVisible = value;
					UpdateScrollBars();
				}
			}
		}

		public int VScrollPos
		{
			get
			{
				SCROLLINFO info = new SCROLLINFO();
				info.fMask = NativeMethods.SIF_TRACKPOS;
				NativeMethods.GetScrollInfo(this.Handle, NativeMethods.SB_VERT, ref info);
				return info.nTrackPos;
			}
		}
		#endregion

		private void Control_Resize(object sender, EventArgs e)
		{
			UpdateScrollBars();
			SynchroVScroll();
		}

		private void SynchroVScroll()
		{
			if (FScrollBars == ScrollBars.Both || FScrollBars == ScrollBars.Vertical)
			{
				SCROLLINFO info = new SCROLLINFO();
				info.fMask = NativeMethods.SIF_ALL;
				NativeMethods.GetScrollInfo(this.Handle, NativeMethods.SB_VERT, ref info);
				FOffsetY = info.nPos;
			}			
		}

		#region méthodes publiques
		public bool CanScrollDown()
		{
			return FRangeY - ClientRectangle.Height > FOffsetY;
		}

		public bool CanScrollUp()
		{
			return FOffsetY > 0;
		}

		public bool CanScrollLeft()
		{
			return FOffsetX > 0;
		}

		public bool CanScrollRight()
		{
			return FRangeX -ClientRectangle.Width > FOffsetX;
		}

		public void Update()
		{
			UpdateScrollBars();
		}

		#endregion
	}
}

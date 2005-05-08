using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Genius.Controls
{
	/// <summary>
	/// Summary description for NativeMethods.
	/// </summary>
	public class NativeMethods
	{
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern IntPtr GetDesktopWindow();		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern int DrawFrameControl(IntPtr hdc, ref RECT pRect, Int32 uType, Int32 uState);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern bool DrawFocusRect(IntPtr hdc, RECT pRect);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern int ShowScrollBar(IntPtr hWnd, int bar,  bool show);		
		
		//[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//static internal extern int GetScrollBarInfo(IntPtr hWnd, SystemObject id, ref SCROLLBARINFO sbi);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern int SetScrollInfo(IntPtr hwnd,  int bar, ref SCROLLINFO si, bool fRedraw);	


		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref RECT rcScroll, 
													ref RECT rcClip, IntPtr UpdateRegion, 
													ref RECT rcInvalidated, int flags);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static internal extern bool ScrollWindow(IntPtr hWnd, int dx, int dy, ref RECT rcScroll, ref RECT rcClip);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static int InvalidateRect(IntPtr hWnd,  ref RECT rc, int bErase);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static int InvalidateRect(IntPtr hWnd,  IntPtr rc, bool bErase);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, FlagsDCX flags);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static bool RedrawWindow(IntPtr hWnd, ref RECT rc, IntPtr hrgnUpdate, FlagsRDW flags);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static bool GetWindowRect(IntPtr hWnd, ref RECT rc);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static bool GetClientRect(IntPtr hWnd, ref RECT rc);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref RECT rc, uint nbpoints);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static IntPtr SetCursor(uint hCursor);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		internal extern static int GetRgnBox(IntPtr hrgn, ref RECT lprc);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
												int x, int y, int cx, int cy, FlagsSetWindowPos uflags);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal extern static int SetLayeredWindowAttributes(IntPtr hWnd, uint rgb, int alpha, int flags);
 

		internal static RECT RectangleToRECT(Rectangle rc)
		{
			RECT r;
			if (!rc.IsEmpty)
			{
				r.left = rc.Left;
				r.right = rc.Right;
				r.top = rc.Top;
				r.bottom = rc.Bottom;
				return r;
			}
			else
				return new RECT();
		}

		internal static int InvalidateRect(IntPtr hWnd,  Rectangle rc, int bErase)
		{
			RECT r = RectangleToRECT(rc);
			return InvalidateRect(hWnd, ref r, bErase);
		}
		
		internal static Rectangle RECTToRectangle(RECT rc)
		{
			return new Rectangle(rc.left, rc.top, rc.right - rc.left, rc.bottom-rc.top);
		}

		internal const int SIF_RANGE = 1;
		internal const int SIF_PAGE = 2;
		internal const int SIF_POS = 4;
		internal const int SIF_DISABLENOSCROLL = 8;
		internal const int SIF_TRACKPOS = 0x10;
		internal const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
		internal const int SB_HORZ = 0;
		internal const int SB_VERT = 1;
		internal const int SB_CTL = 2;
		internal const int SB_BOTH = 3;	
	}

	#region SCROLLINFO
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct SCROLLINFO
	{
		internal int cbSize;
		internal int fMask;
		internal int nMin;
		internal int nMax;
		internal int nPage;
		internal int nPos;
		internal int nTrackPos;
	}
	#endregion
	
	#region SCROLLBARINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	internal struct SCROLLBARINFO
	{
		internal int  cbSize;
		internal RECT  rcScrollBar;
		internal int   dxyLineButton;
		internal int   xyThumbTop;
		internal int   xyThumbBottom;
		internal int   reserved;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		internal int[] rgstate;
	}
	#endregion

	#region RECT
	[StructLayout(LayoutKind.Sequential)]
	internal struct RECT
	{
		internal int left;
		internal int top;
		internal int right;
		internal int bottom;
	}
	#endregion

	#region POINT
	[StructLayout(LayoutKind.Sequential)]
	internal struct POINT
	{
		internal int X;
		internal int Y;
	}
	#endregion

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	internal struct WINDOWPOS
	{
		int hwnd;
		int hwndInsertAfter;
		int x;
		int y;
		int cx;
		int cy;
		uint flags;
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	internal struct NCCalcSizeParams
	{
		public RECT rgrc0;
		public RECT rgrc1;
		public RECT rgrc2;
		public WINDOWPOS lppos;
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	internal struct WMNCPaint
	{
		int Msg;
		IntPtr RGN;
		int Unused;
		int Result;
	}

}

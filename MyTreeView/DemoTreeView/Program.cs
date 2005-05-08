using System;
using System.Windows.Forms;
using DemoTreeView;

namespace Genius.Controls
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	public class Program
	{
		[STAThread]
		static void Main()
		{
			Application.Run(new TabbedDemo());
			//Application.Run(new Form1());
		}
	}
}

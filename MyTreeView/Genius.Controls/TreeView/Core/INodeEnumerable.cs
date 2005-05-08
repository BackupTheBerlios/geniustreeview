using System;
using System.Collections;

namespace Genius.Controls.TreeView.Core
{
	/// <summary>
	/// interface d'énumération
	/// </summary>
	public interface INodeEnumerable
	{
		/// <summary>
		/// renvoi un <see cref="IEnumerable"/> sur l'ensemble des noeuds
		/// </summary>
		/// <returns></returns>
		IEnumerable GetNodes();
		/// <summary>
		/// renvoi l'ensemble des noeuds d'un niveau ou plus
		/// </summary>
		/// <param name="recurse"></param>
		/// <returns></returns>
		IEnumerable GetNodes(bool recurse);
		/// <summary>
		/// renvoi l'ensemble des noeuds visible d'un niveau ou plus
		/// </summary>
		/// <param name="recurse"></param>
		/// <returns></returns>
		IEnumerable GetVisibleNodes(bool recurse);
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls
{
	/// <summary>
	/// Summary description for ITreeViewEdit.
	/// </summary>
	public interface ITreeViewEdit
	{
		/// <summary>
		/// demarrage de l'�dition
		/// </summary>
		/// <param name="aNode">le noeud en cours d'�dition</param>
		/// <param name="aCol">la colonne en cours d'�dition</param>
		/// <param name="aRect">le rectangle d'�dition</param>
		void BeginEdit(INode aNode, int aCol, Rectangle aRect);
		/// <summary>
		/// fin de l'�dition
		/// </summary>
		void EndEdit();
		/// <summary>
		/// 
		/// </summary>
		void CancelEdit();
		/// <summary>
		/// l'impl�mentation de ITreeViewEDit, doit appeler cet event lors de l'appuie sur une touche
		/// car le TreeView g�re le Escape et Enter
		/// </summary>
		event KeyEventHandler OnKeyDown;
		/// <summary>
		/// le rectangle d'�dition
		/// </summary>
		Rectangle EditRect {get; set;}

		/// <summary>
		/// le control associ� � l'�dition
		/// </summary>
		Control Control {get;}

		object Value {get;}
	}
}

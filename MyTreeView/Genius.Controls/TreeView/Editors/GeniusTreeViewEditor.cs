using System;
using System.Drawing;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace Genius.Controls.TreeView.Editors
{
	/// <summary>
	/// classe de base pour l'�dition
	/// </summary>
	public abstract class GeniusTreeViewEditorBase : ITreeViewEdit
	{
		/// <summary>
		/// le noeud en cours d'�dition
		/// </summary>
		protected INode			FNode;
		/// <summary>
		/// la colonne en cours d'�dition
		/// </summary>
		protected int			FCol;
		/// <summary>
		/// le treeview
		/// </summary>
		protected GeniusTreeView FTreeView;

		/// <summary>
		/// Rectangle de l'�dition
		/// </summary>
		protected Rectangle FEditRect;

		/// <summary>
		/// Controle associ� � l'�dition
		/// </summary>
		protected Control FControl;

		/// <summary>
		/// constructeur par d�faut
		/// </summary>
		/// <param name="tv"></param>
		public GeniusTreeViewEditorBase(GeniusTreeView tv)
		{
			FTreeView = tv;
			FControl = CreateEditorControl();
			FControl.Visible = false;
			FControl.Parent = FTreeView;
		}
		#region ITreeViewEdit Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="aCol"></param>
		/// <param name="aRect"></param>
		public virtual void BeginEdit(INode aNode, int aCol, Rectangle aRect)
		{
			FNode = aNode;
			FCol = aCol;
			FEditRect = aRect;
			FControl.Bounds = aRect;			
			FControl.Text = FTreeView.GetNodeText(aNode, aCol);
			FControl.Show();
			FControl.Focus();
			FControl.Leave +=new EventHandler(FControl_Leave);
			FControl.KeyDown +=new KeyEventHandler(FControl_KeyDown);
		}

		/// <summary>
		/// fin d'�dition
		/// </summary>
		public virtual void EndEdit()
		{
			FControl.KeyDown -=new KeyEventHandler(FControl_KeyDown);
			FControl.Leave -= new EventHandler(FControl_Leave);
			FTreeView.Focus();
			FControl.Hide();
		}

		/// <summary>
		/// annulation de l'�dition
		/// </summary>
		public virtual void CancelEdit()
		{
			FControl.KeyDown -=new KeyEventHandler(FControl_KeyDown);
			FControl.Leave -= new EventHandler(FControl_Leave);
			FTreeView.Focus();
			FControl.Hide();
		}

		/// <summary>
		/// cet �v�nement sert au treeview
		/// </summary>
		public event System.Windows.Forms.KeyEventHandler OnKeyDown;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DoKeyDown(object sender, KeyEventArgs e)
		{
			if (OnKeyDown != null)
				OnKeyDown(sender, e);
		}

		/// <summary>
		/// Rectangle de la zone d'�dition
		/// </summary>
		public Rectangle EditRect
		{
			get
			{
				return FEditRect;
			}
			set
			{
				FEditRect = value;
				DoSetBounds(value);
			}
		}
		#endregion

		/// <summary>
		///	indique aux descendants qu'il doivent retailler leur zone d'�dition
		/// </summary>
		/// <param name="aRect"></param>
		protected void DoSetBounds(Rectangle aRect)
		{
			if (FControl != null)
				FControl.SetBounds(aRect.Left, aRect.Top,  aRect.Width, aRect.Height);
		}

		/// <summary>
		/// � d�river et cr�er votre control
		/// </summary>
		/// <returns></returns>
		protected abstract Control CreateEditorControl();

		/// <summary>
		/// valeur de l'�diteur
		/// </summary>
		public abstract object Value
		{
			get;
		}

		/// <summary>
		/// Control associ� � l'editeur
		/// </summary>
		public Control Control
		{
			get
			{
				return FControl;
			}
		}

		private void FControl_Leave(object sender, EventArgs e)
		{
			FTreeView.TryEndEdit();
		}

		private void FControl_KeyDown(object sender, KeyEventArgs e)
		{
			DoKeyDown(sender, e);
		}
	}
	/// <summary>
	/// classe d'exemple repr�sentant une impl�mentation  <see cref="ITreeViewEdit"/>
	/// </summary>
	public class GeniusTreeViewEditor : GeniusTreeViewEditorBase
	{	
		/// <summary>
		/// constructeur par d�faut
		/// </summary>
		/// <param name="tv"></param>
		public GeniusTreeViewEditor(GeniusTreeView tv): base(tv)
		{
		}

		/// <summary>
		/// cr�ation du textebox �ditor
		/// </summary>
		/// <returns></returns>
		protected override Control CreateEditorControl()
		{
			return new TreeViewTextBoxEditor();
		}


		/// <summary>
		/// d�marrage de l'�dition
		/// </summary>
		/// <param name="aNode"></param>
		/// <param name="aCol"></param>
		/// <param name="aRect"></param>
		public override void BeginEdit(INode aNode, int aCol, Rectangle aRect)
		{
			base.BeginEdit(aNode, aCol, aRect);
			(FControl as TreeViewTextBoxEditor).SelectAll();
		}

		/// <summary>
		/// valeur de l'editeur
		/// </summary>
		public override object Value
		{
			get
			{
				return (FControl as TreeViewTextBoxEditor).Text;
			}
		}
	}
}

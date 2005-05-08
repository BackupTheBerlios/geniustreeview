using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Genius.Controls.TreeView.Core;

namespace DemoTreeView.UserControl
{
	/// <summary>
	/// Summary description for TestEvents.
	/// </summary>
	public class TestEvents : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox edResult;
		private Genius.Controls.GeniusTreeView geniusTreeView1;
		private Genius.Controls.GeniusTreeViewColonne geniusTreeViewColonne1;
		private Genius.Controls.GeniusTreeViewColonne geniusTreeViewColonne2;
		private Genius.Controls.GeniusTreeViewColonne geniusTreeViewColonne3;
		private Genius.Controls.GeniusTreeViewColonne geniusTreeViewColonne4;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestEvents()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.edResult = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.geniusTreeView1 = new Genius.Controls.GeniusTreeView();
			this.geniusTreeViewColonne1 = new Genius.Controls.GeniusTreeViewColonne();
			this.geniusTreeViewColonne2 = new Genius.Controls.GeniusTreeViewColonne();
			this.geniusTreeViewColonne3 = new Genius.Controls.GeniusTreeViewColonne();
			this.geniusTreeViewColonne4 = new Genius.Controls.GeniusTreeViewColonne();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 192);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 112);
			this.panel1.TabIndex = 0;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.edResult);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 32);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(520, 80);
			this.panel4.TabIndex = 1;
			// 
			// edResult
			// 
			this.edResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.edResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edResult.Location = new System.Drawing.Point(0, 0);
			this.edResult.Multiline = true;
			this.edResult.Name = "edResult";
			this.edResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.edResult.Size = new System.Drawing.Size(520, 80);
			this.edResult.TabIndex = 0;
			this.edResult.Text = "";
			this.edResult.WordWrap = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(520, 32);
			this.panel3.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(8, 6);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Clear";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 189);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(520, 3);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.geniusTreeView1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(520, 189);
			this.panel2.TabIndex = 2;
			// GeniusTreeView By Pierrick Gourlain
			// 
			// geniusTreeView1
			// 
			this.geniusTreeView1.Alignment = System.Drawing.StringAlignment.Near;
			this.geniusTreeView1.AutoSort = false;
			this.geniusTreeView1.BackColor = System.Drawing.SystemColors.Window;
			this.geniusTreeView1.DefaultDrawingOption = Genius.Controls.DrawingOption.ShowGridLines;
			this.geniusTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.geniusTreeView1.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeView1.HeaderColonnes.AddRange(new Genius.Controls.GeniusTreeViewColonne[] {
																										 this.geniusTreeViewColonne1,
																										 this.geniusTreeViewColonne2,
																										 this.geniusTreeViewColonne3,
																										 this.geniusTreeViewColonne4});
			this.geniusTreeView1.KeysGridMode = false;
			this.geniusTreeView1.Location = new System.Drawing.Point(0, 0);
			this.geniusTreeView1.Name = "geniusTreeView1";
			this.geniusTreeView1.ShowHeader = true;
			this.geniusTreeView1.Size = new System.Drawing.Size(520, 189);
			this.geniusTreeView1.TabIndex = 0;
			this.geniusTreeView1.Text = "geniusTreeView1";
			this.geniusTreeView1.UseColumns = true;
			this.geniusTreeView1.OnBeforeCollapse += new Genius.Controls.OnCollapseDelegate(this.geniusTreeView1_OnBeforeCollapse);
			this.geniusTreeView1.OnAfterExpand += new Genius.Controls.OnNodeDelegate(this.geniusTreeView1_OnAfterExpand);
			this.geniusTreeView1.OnAfterSelect += new Genius.Controls.OnSelectedDelegate(this.geniusTreeView1_OnAfterSelect);
			this.geniusTreeView1.OnAfterCollapse += new Genius.Controls.OnNodeDelegate(this.geniusTreeView1_OnAfterCollapse);
			this.geniusTreeView1.OnGetNodeText += new Genius.Controls.OnGetNodeTextDelegate(this.geniusTreeView1_OnGetNodeText);
			this.geniusTreeView1.OnBeforeExpand += new Genius.Controls.OnExpandDelegate(this.geniusTreeView1_OnBeforeExpand);
			this.geniusTreeView1.OnBeforeSelect += new Genius.Controls.OnSelectDelegate(this.geniusTreeView1_OnBeforeSelect);
			// 
			// geniusTreeViewColonne1
			// 
			this.geniusTreeViewColonne1.BackColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne1.FontColonne = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne1.ForeColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Black, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne1.HeadColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne1.Text = "Colonne 0";
			// 
			// geniusTreeViewColonne2
			// 
			this.geniusTreeViewColonne2.BackColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne2.FontColonne = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne2.ForeColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Black, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne2.HeadColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne2.Text = "Colonne 1";
			// 
			// geniusTreeViewColonne3
			// 
			this.geniusTreeViewColonne3.BackColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne3.FontColonne = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne3.ForeColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Black, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne3.HeadColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne3.Text = "Colonne 2";
			// 
			// geniusTreeViewColonne4
			// 
			this.geniusTreeViewColonne4.BackColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne4.FontColonne = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.geniusTreeViewColonne4.ForeColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Black, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne4.HeadColor = new Genius.Controls.GeniusLinearGradientBrush(System.Drawing.Color.Empty, System.Drawing.Color.Empty, 0F);
			this.geniusTreeViewColonne4.Text = "Colonne 3";
			// 
			// TestEvents
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Name = "TestEvents";
			this.Size = new System.Drawing.Size(520, 304);
			this.Load += new System.EventHandler(this.TestEvents_Load);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void TestEvents_Load(object sender, System.EventArgs e)
		{
			geniusTreeView1.BeginUpdate();
			try
			{
				for (int i = 0; i < 20; i++)
				{
					INode aParent = geniusTreeView1.Add(null, "Node "  + i.ToString(), null);
					for(int j =0; j < 2;j++)
						geniusTreeView1.Add(aParent, "Child " + j.ToString(), null);
				}
			}
			finally
			{
				geniusTreeView1.EndUpdate();
			}
		}

		private void geniusTreeView1_OnGetNodeText(Genius.Controls.GeniusTreeView Sender, Genius.Controls.NodeTextEventArgs e)
		{
			if (e.DisplayColumn > 0)
			{
				e.Text = string.Format("Cell({0},{1})", e.DisplayColumn, e.Node.Index);
			}
		}

		private void geniusTreeView1_OnBeforeSelect(Genius.Controls.GeniusTreeView Sender, Genius.Controls.CanSelectEventArgs e)
		{
			edResult.AppendText(string.Format("OnBeforeSelect({0}, {1})\r\n", e.DisplayColumn, e.Node.Index));
		}

		private void geniusTreeView1_OnAfterSelect(Genius.Controls.GeniusTreeView Sender, Genius.Controls.SelectedEventArgs e)
		{
			edResult.AppendText(string.Format("OnAfterSelect({0}, {1})\r\n", e.DisplayColumn, e.Node.Index));		
		}

		private void geniusTreeView1_OnBeforeExpand(Genius.Controls.GeniusTreeView Sender, Genius.Controls.ExpandEventArgs e)
		{
			edResult.AppendText(string.Format("OnBeforeExpand({0})\r\n", e.Node.Index));				
		}

		private void geniusTreeView1_OnAfterExpand(Genius.Controls.GeniusTreeView Sender, Genius.Controls.NodeEventArgs e)
		{
			edResult.AppendText(string.Format("OnAfterExpand({0})\r\n", e.Node.Index));				
		}

		private void geniusTreeView1_OnBeforeCollapse(Genius.Controls.GeniusTreeView Sender, Genius.Controls.CollapseEventArgs e)
		{
			edResult.AppendText(string.Format("OnBeforeCollapse({0})\r\n", e.Node.Index));						
		}

		private void geniusTreeView1_OnAfterCollapse(Genius.Controls.GeniusTreeView Sender, Genius.Controls.NodeEventArgs e)
		{
			edResult.AppendText(string.Format("OnAfterCollapse({0})\r\n", e.Node.Index));
		}

		private void geniusTreeView1_OnBeforeUnSelect(Genius.Controls.GeniusTreeView Sender, Genius.Controls.CanUnSelectEventArgs e)
		{
			edResult.AppendText(string.Format("OnBeforeUnSelect({0})\r\n", e.Node.Index));
		}
	}
}

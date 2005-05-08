using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DemoTreeView.UserControl
{
	/// <summary>
	/// Summary description for DragAndDropDemoCtl.
	/// </summary>
	public class DragAndDropDemoCtl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Genius.Controls.GeniusTreeView gtvSource;
		private Genius.Controls.GeniusTreeView gtvDest;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DragAndDropDemoCtl()
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
			this.gtvSource = new Genius.Controls.GeniusTreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.gtvDest = new Genius.Controls.GeniusTreeView();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gtvSource);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 376);
			this.panel1.TabIndex = 0;
			// 
			// gtvSource
			// 
			this.gtvSource.Alignment = System.Drawing.StringAlignment.Near;
			this.gtvSource.AllowDrag = true;
			this.gtvSource.AutoSort = false;
			this.gtvSource.BackColor = System.Drawing.SystemColors.Window;
			this.gtvSource.DataSource = null;
			this.gtvSource.DefaultNodeHeight = 18;
			this.gtvSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gtvSource.FullRowSelect = false;
			this.gtvSource.Header.AllowDrag = false;
			this.gtvSource.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gtvSource.HeaderHeight = 20;
			this.gtvSource.ImageList = null;
			this.gtvSource.ImageStateList = null;
			this.gtvSource.Indentation = 16;
			this.gtvSource.KeysGridMode = false;
			this.gtvSource.Location = new System.Drawing.Point(0, 23);
			this.gtvSource.Name = "gtvSource";
			this.gtvSource.ShowHeader = false;
			this.gtvSource.Size = new System.Drawing.Size(224, 353);
			this.gtvSource.TabIndex = 1;
			this.gtvSource.Text = "geniusTreeView1";
			this.gtvSource.UseColumns = false;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(224, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 376);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.gtvDest);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(227, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(221, 376);
			this.panel2.TabIndex = 2;
			// 
			// gtvDest
			// 
			this.gtvDest.Alignment = System.Drawing.StringAlignment.Near;
			this.gtvDest.AllowDrag = true;
			this.gtvDest.AllowDrop = true;
			this.gtvDest.AutoSort = false;
			this.gtvDest.BackColor = System.Drawing.SystemColors.Window;
			this.gtvDest.DataSource = null;
			this.gtvDest.DefaultNodeHeight = 18;
			this.gtvDest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gtvDest.FullRowSelect = false;
			this.gtvDest.Header.AllowDrag = false;
			this.gtvDest.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gtvDest.HeaderHeight = 20;
			this.gtvDest.ImageList = null;
			this.gtvDest.ImageStateList = null;
			this.gtvDest.Indentation = 16;
			this.gtvDest.KeysGridMode = false;
			this.gtvDest.Location = new System.Drawing.Point(0, 23);
			this.gtvDest.Name = "gtvDest";
			this.gtvDest.ShowHeader = false;
			this.gtvDest.Size = new System.Drawing.Size(221, 353);
			this.gtvDest.TabIndex = 2;
			this.gtvDest.Text = "geniusTreeView2";
			this.gtvDest.UseColumns = false;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(221, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Destination";
			// 
			// DragAndDropDemoCtl
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Name = "DragAndDropDemoCtl";
			this.Size = new System.Drawing.Size(448, 376);
			this.Load += new System.EventHandler(this.DragAndDropDemoCtl_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DragAndDropDemoCtl_Load(object sender, System.EventArgs e)
		{
			gtvSource.BeginUpdate();
			try
			{
				for (int i = 0; i < 10; i++)
					for (int j =0; j < 10; j++)
					{
						gtvSource.Add(null, String.Format("Node_{0}.{1}", i, j), null);
					}
			}
			finally
			{
				gtvSource.EndUpdate();
			}
			gtvDest.Add(null, "DropHere", null);
		}
	}
}

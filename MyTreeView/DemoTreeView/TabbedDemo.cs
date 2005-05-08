using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Genius.Controls.Diagnostics;

namespace Genius.Controls
{
	/// <summary>
	/// Summary description for TabbedDemo.
	/// </summary>
	public class TabbedDemo : System.Windows.Forms.Form
	{
		private Genius.Controls.HorizontalTabs horizontalTabs1;
		private Genius.Controls.HorizontalTab horizontalTab1;
		private Genius.Controls.HorizontalTab horizontalTab2;
		private Genius.Controls.HorizontalTab horizontalTab3;
		private Genius.Controls.HorizontalTab horizontalTab4;
		private Genius.Controls.HorizontalTab horizontalTab5;
		private Genius.Controls.HorizontalTab horizontalTab6;
		private Genius.Controls.UserControl.StandardDemoCtl standardDemoCtl1;
		private Genius.Controls.UserControl.StandardDemo1Ctl standardDemo1Ctl1;
		private DemoTreeView.UserControl.DragAndDropDemoCtl dragAndDropDemoCtl1;
		private Genius.Controls.UserControl.CustomDrawDemoCtl customDrawDemoCtl1;
		private DemoTreeView.UserControl.FixedColumnDemoCtl fixedColumnDemoCtl1;
		private DemoTreeView.UserControl.TestEvents testEvents1;
		private Genius.Controls.HorizontalTab horizontalTab7;
		private DemoTreeView.UserControl.DemoEditors demoEditors1;
		private Genius.Controls.HorizontalTab horizontalTab8;
		private DemoTreeView.UserControl.TestChapter testChapter1;
		private Genius.Controls.HorizontalTab horizontalTab9;
		private DemoTreeView.UserControl.TestDrawingOptions testDrawingOptions1;
		private Genius.Controls.HorizontalTab horizontalTab10;
		private DemoTreeView.UserControl.TestDataSource testDataSource1;
		private DemoTreeView.UserControl.TestDataSource testDataSource2;
		private Genius.Controls.HorizontalTab horizontalTab11;
		private DemoTreeView.UserControl.TestVisibility testVisibility1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TabbedDemo()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TabbedDemo));
			this.horizontalTabs1 = new Genius.Controls.HorizontalTabs();
			this.horizontalTab1 = new Genius.Controls.HorizontalTab();
			this.standardDemoCtl1 = new Genius.Controls.UserControl.StandardDemoCtl();
			this.horizontalTab2 = new Genius.Controls.HorizontalTab();
			this.standardDemo1Ctl1 = new Genius.Controls.UserControl.StandardDemo1Ctl();
			this.horizontalTab3 = new Genius.Controls.HorizontalTab();
			this.dragAndDropDemoCtl1 = new DemoTreeView.UserControl.DragAndDropDemoCtl();
			this.horizontalTab4 = new Genius.Controls.HorizontalTab();
			this.customDrawDemoCtl1 = new Genius.Controls.UserControl.CustomDrawDemoCtl();
			this.horizontalTab5 = new Genius.Controls.HorizontalTab();
			this.fixedColumnDemoCtl1 = new DemoTreeView.UserControl.FixedColumnDemoCtl();
			this.horizontalTab6 = new Genius.Controls.HorizontalTab();
			this.testEvents1 = new DemoTreeView.UserControl.TestEvents();
			this.horizontalTab7 = new Genius.Controls.HorizontalTab();
			this.demoEditors1 = new DemoTreeView.UserControl.DemoEditors();
			this.horizontalTab8 = new Genius.Controls.HorizontalTab();
			this.testChapter1 = new DemoTreeView.UserControl.TestChapter();
			this.horizontalTab9 = new Genius.Controls.HorizontalTab();
			this.testDrawingOptions1 = new DemoTreeView.UserControl.TestDrawingOptions();
			this.horizontalTab10 = new Genius.Controls.HorizontalTab();
			this.testDataSource2 = new DemoTreeView.UserControl.TestDataSource();
			this.testDataSource1 = new DemoTreeView.UserControl.TestDataSource();
			this.horizontalTab11 = new Genius.Controls.HorizontalTab();
			this.testVisibility1 = new DemoTreeView.UserControl.TestVisibility();
			this.horizontalTabs1.SuspendLayout();
			this.horizontalTab1.SuspendLayout();
			this.horizontalTab2.SuspendLayout();
			this.horizontalTab3.SuspendLayout();
			this.horizontalTab4.SuspendLayout();
			this.horizontalTab5.SuspendLayout();
			this.horizontalTab6.SuspendLayout();
			this.horizontalTab7.SuspendLayout();
			this.horizontalTab8.SuspendLayout();
			this.horizontalTab9.SuspendLayout();
			this.horizontalTab10.SuspendLayout();
			this.horizontalTab11.SuspendLayout();
			this.SuspendLayout();
			// 
			// horizontalTabs1
			// 
			this.horizontalTabs1.Controls.Add(this.horizontalTab1);
			this.horizontalTabs1.Controls.Add(this.horizontalTab2);
			this.horizontalTabs1.Controls.Add(this.horizontalTab3);
			this.horizontalTabs1.Controls.Add(this.horizontalTab4);
			this.horizontalTabs1.Controls.Add(this.horizontalTab5);
			this.horizontalTabs1.Controls.Add(this.horizontalTab6);
			this.horizontalTabs1.Controls.Add(this.horizontalTab7);
			this.horizontalTabs1.Controls.Add(this.horizontalTab8);
			this.horizontalTabs1.Controls.Add(this.horizontalTab9);
			this.horizontalTabs1.Controls.Add(this.horizontalTab10);
			this.horizontalTabs1.Controls.Add(this.horizontalTab11);
			this.horizontalTabs1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTabs1.Location = new System.Drawing.Point(0, 0);
			this.horizontalTabs1.Margins = 5;
			this.horizontalTabs1.Name = "horizontalTabs1";
			this.horizontalTabs1.Orientation = Genius.Controls.TabOrientation.Left;
			this.horizontalTabs1.SelectedIndex = 10;
			this.horizontalTabs1.Size = new System.Drawing.Size(544, 446);
			this.horizontalTabs1.TabIndex = 0;
			this.horizontalTabs1.TabsHeight = 30;
			this.horizontalTabs1.TabsWidth = 85;
			this.horizontalTabs1.Text = "FixedColumn";
			// 
			// horizontalTab1
			// 
			this.horizontalTab1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab1.Controls.Add(this.standardDemoCtl1);
			this.horizontalTab1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab1.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab1.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab1.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab1.Name = "horizontalTab1";
			this.horizontalTab1.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab1.TabIndex = 0;
			this.horizontalTab1.Text = "standard";
			this.horizontalTab1.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// standardDemoCtl1
			// 
			this.standardDemoCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.standardDemoCtl1.Location = new System.Drawing.Point(0, 0);
			this.standardDemoCtl1.Name = "standardDemoCtl1";
			this.standardDemoCtl1.Size = new System.Drawing.Size(447, 434);
			this.standardDemoCtl1.TabIndex = 0;
			// 
			// horizontalTab2
			// 
			this.horizontalTab2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab2.Controls.Add(this.standardDemo1Ctl1);
			this.horizontalTab2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab2.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab2.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab2.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab2.Name = "horizontalTab2";
			this.horizontalTab2.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab2.TabIndex = 1;
			this.horizontalTab2.Text = "tree-listview demo";
			this.horizontalTab2.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// standardDemo1Ctl1
			// 
			this.standardDemo1Ctl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.standardDemo1Ctl1.Location = new System.Drawing.Point(0, 0);
			this.standardDemo1Ctl1.Name = "standardDemo1Ctl1";
			this.standardDemo1Ctl1.Size = new System.Drawing.Size(447, 434);
			this.standardDemo1Ctl1.TabIndex = 0;
			// 
			// horizontalTab3
			// 
			this.horizontalTab3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab3.Controls.Add(this.dragAndDropDemoCtl1);
			this.horizontalTab3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab3.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab3.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab3.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab3.Name = "horizontalTab3";
			this.horizontalTab3.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab3.TabIndex = 2;
			this.horizontalTab3.Text = "drag and drop";
			this.horizontalTab3.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// dragAndDropDemoCtl1
			// 
			this.dragAndDropDemoCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dragAndDropDemoCtl1.Location = new System.Drawing.Point(0, 0);
			this.dragAndDropDemoCtl1.Name = "dragAndDropDemoCtl1";
			this.dragAndDropDemoCtl1.Size = new System.Drawing.Size(447, 434);
			this.dragAndDropDemoCtl1.TabIndex = 0;
			// 
			// horizontalTab4
			// 
			this.horizontalTab4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab4.Controls.Add(this.customDrawDemoCtl1);
			this.horizontalTab4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab4.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab4.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab4.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab4.Name = "horizontalTab4";
			this.horizontalTab4.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab4.TabIndex = 3;
			this.horizontalTab4.Text = "CustomDraw";
			this.horizontalTab4.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// customDrawDemoCtl1
			// 
			this.customDrawDemoCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.customDrawDemoCtl1.Location = new System.Drawing.Point(0, 0);
			this.customDrawDemoCtl1.Name = "customDrawDemoCtl1";
			this.customDrawDemoCtl1.Size = new System.Drawing.Size(447, 434);
			this.customDrawDemoCtl1.TabIndex = 0;
			// 
			// horizontalTab5
			// 
			this.horizontalTab5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab5.Controls.Add(this.fixedColumnDemoCtl1);
			this.horizontalTab5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab5.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab5.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab5.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab5.Name = "horizontalTab5";
			this.horizontalTab5.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab5.TabIndex = 4;
			this.horizontalTab5.Text = "FixedColumns";
			this.horizontalTab5.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// fixedColumnDemoCtl1
			// 
			this.fixedColumnDemoCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fixedColumnDemoCtl1.Location = new System.Drawing.Point(0, 0);
			this.fixedColumnDemoCtl1.Name = "fixedColumnDemoCtl1";
			this.fixedColumnDemoCtl1.Size = new System.Drawing.Size(447, 434);
			this.fixedColumnDemoCtl1.TabIndex = 0;
			// 
			// horizontalTab6
			// 
			this.horizontalTab6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab6.Controls.Add(this.testEvents1);
			this.horizontalTab6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab6.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab6.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab6.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab6.Name = "horizontalTab6";
			this.horizontalTab6.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab6.TabIndex = 5;
			this.horizontalTab6.Text = "TestEvents";
			this.horizontalTab6.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// testEvents1
			// 
			this.testEvents1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testEvents1.Location = new System.Drawing.Point(0, 0);
			this.testEvents1.Name = "testEvents1";
			this.testEvents1.Size = new System.Drawing.Size(447, 434);
			this.testEvents1.TabIndex = 0;
			// 
			// horizontalTab7
			// 
			this.horizontalTab7.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab7.Controls.Add(this.demoEditors1);
			this.horizontalTab7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab7.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab7.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab7.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab7.Name = "horizontalTab7";
			this.horizontalTab7.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab7.TabIndex = 6;
			this.horizontalTab7.Text = "Editor Démo";
			this.horizontalTab7.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// demoEditors1
			// 
			this.demoEditors1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.demoEditors1.Location = new System.Drawing.Point(0, 0);
			this.demoEditors1.Name = "demoEditors1";
			this.demoEditors1.Size = new System.Drawing.Size(447, 434);
			this.demoEditors1.TabIndex = 0;
			// 
			// horizontalTab8
			// 
			this.horizontalTab8.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab8.Controls.Add(this.testChapter1);
			this.horizontalTab8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab8.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab8.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab8.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab8.Name = "horizontalTab8";
			this.horizontalTab8.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab8.TabIndex = 7;
			this.horizontalTab8.Text = "Chapter";
			this.horizontalTab8.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// testChapter1
			// 
			this.testChapter1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testChapter1.Location = new System.Drawing.Point(0, 0);
			this.testChapter1.Name = "testChapter1";
			this.testChapter1.Size = new System.Drawing.Size(447, 434);
			this.testChapter1.TabIndex = 0;
			// 
			// horizontalTab9
			// 
			this.horizontalTab9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab9.Controls.Add(this.testDrawingOptions1);
			this.horizontalTab9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab9.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab9.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab9.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab9.Name = "horizontalTab9";
			this.horizontalTab9.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab9.TabIndex = 8;
			this.horizontalTab9.Text = "DrawingOption";
			this.horizontalTab9.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// testDrawingOptions1
			// 
			this.testDrawingOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testDrawingOptions1.Location = new System.Drawing.Point(0, 0);
			this.testDrawingOptions1.Name = "testDrawingOptions1";
			this.testDrawingOptions1.Size = new System.Drawing.Size(447, 434);
			this.testDrawingOptions1.TabIndex = 0;
			// 
			// horizontalTab10
			// 
			this.horizontalTab10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab10.Controls.Add(this.testDataSource2);
			this.horizontalTab10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab10.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab10.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab10.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab10.Name = "horizontalTab10";
			this.horizontalTab10.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab10.TabIndex = 9;
			this.horizontalTab10.Text = "DataSource";
			this.horizontalTab10.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// testDataSource2
			// 
			this.testDataSource2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testDataSource2.Location = new System.Drawing.Point(0, 0);
			this.testDataSource2.Name = "testDataSource2";
			this.testDataSource2.Size = new System.Drawing.Size(447, 434);
			this.testDataSource2.TabIndex = 0;
			// 
			// testDataSource1
			// 
			this.testDataSource1.Location = new System.Drawing.Point(0, 0);
			this.testDataSource1.Name = "testDataSource1";
			this.testDataSource1.Size = new System.Drawing.Size(304, 320);
			this.testDataSource1.TabIndex = 0;
			// 
			// horizontalTab11
			// 
			this.horizontalTab11.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(241)), ((System.Byte)(229)));
			this.horizontalTab11.Controls.Add(this.testVisibility1);
			this.horizontalTab11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalTab11.HorizontalTextAlignement = System.Drawing.StringAlignment.Near;
			this.horizontalTab11.Location = new System.Drawing.Point(91, 6);
			this.horizontalTab11.Margins = new Genius.Controls.TabMargins(0, 0, 0, 0);
			this.horizontalTab11.Name = "horizontalTab11";
			this.horizontalTab11.Size = new System.Drawing.Size(447, 434);
			this.horizontalTab11.TabIndex = 10;
			this.horizontalTab11.Text = "test visibilité";
			this.horizontalTab11.VerticalTextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// testVisibility1
			// 
			this.testVisibility1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testVisibility1.Location = new System.Drawing.Point(0, 0);
			this.testVisibility1.Name = "testVisibility1";
			this.testVisibility1.Size = new System.Drawing.Size(447, 434);
			this.testVisibility1.TabIndex = 0;
			// 
			// TabbedDemo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(544, 446);
			this.Controls.Add(this.horizontalTabs1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TabbedDemo";
			this.Text = "TabbedDemo";
			this.horizontalTabs1.ResumeLayout(false);
			this.horizontalTab1.ResumeLayout(false);
			this.horizontalTab2.ResumeLayout(false);
			this.horizontalTab3.ResumeLayout(false);
			this.horizontalTab4.ResumeLayout(false);
			this.horizontalTab5.ResumeLayout(false);
			this.horizontalTab6.ResumeLayout(false);
			this.horizontalTab7.ResumeLayout(false);
			this.horizontalTab8.ResumeLayout(false);
			this.horizontalTab9.ResumeLayout(false);
			this.horizontalTab10.ResumeLayout(false);
			this.horizontalTab11.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

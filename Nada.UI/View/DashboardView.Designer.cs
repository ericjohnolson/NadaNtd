namespace Nada.UI.View.Demography
{
    partial class DashboardView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRecent = new System.Windows.Forms.Label();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.BackColor = System.Drawing.Color.White;
            this.c1SplitContainer1.CollapsingAreaColor = System.Drawing.Color.White;
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.Black;
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.FixedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1136, 800);
            this.c1SplitContainer1.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.c1SplitContainer1.SplitterWidth = 1;
            this.c1SplitContainer1.TabIndex = 0;
            this.c1SplitContainer1.UseParentVisualStyle = false;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.AutoScroll = true;
            this.c1SplitterPanel1.BackColor = System.Drawing.Color.White;
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.panel1);
            this.c1SplitterPanel1.Controls.Add(this.treeListView1);
            this.c1SplitterPanel1.Controls.Add(this.tableLayoutPanel1);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.KeepRelativeSize = false;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(279, 800);
            this.c1SplitterPanel1.SizeRatio = 25.265D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Width = 286;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.lblRecent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 31);
            this.panel1.TabIndex = 13;
            // 
            // lblRecent
            // 
            this.lblRecent.AutoSize = true;
            this.lblRecent.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecent.ForeColor = System.Drawing.Color.White;
            this.lblRecent.Location = new System.Drawing.Point(13, 5);
            this.lblRecent.Margin = new System.Windows.Forms.Padding(13, 5, 0, 5);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(133, 19);
            this.lblRecent.TabIndex = 7;
            this.lblRecent.Tag = "AdministrativeUnits";
            this.lblRecent.Text = "AdministrativeUnits";
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView1.FullRowSelect = true;
            this.treeListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.treeListView1.HighlightBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.treeListView1.HighlightForegroundColor = System.Drawing.Color.White;
            this.treeListView1.Location = new System.Drawing.Point(0, 37);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeListView1.MinimumSize = new System.Drawing.Size(250, 321);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(276, 681);
            this.treeListView1.TabIndex = 12;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.UseHyperlinks = true;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            this.treeListView1.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.treeListView1_HyperlinkClicked);
            this.treeListView1.DoubleClick += new System.EventHandler(this.treeListView1_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Hyperlink = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Tag = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 193;
            this.olvColumn1.WordWrap = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.c1SplitterPanel2.Height = 100;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(287, 0);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(849, 800);
            this.c1SplitterPanel2.SizeRatio = 100D;
            this.c1SplitterPanel2.TabIndex = 1;
            // 
            // DashboardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.c1SplitContainer1);
            this.Name = "DashboardView";
            this.Size = new System.Drawing.Size(1136, 800);
            this.Load += new System.EventHandler(this.DemographyView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRecent;

    }
}

namespace Nada.UI.View.Demography
{
    partial class DemographyView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1SplitButton1 = new C1.Win.C1Input.C1SplitButton();
            this.importSurvey = new C1.Win.C1Input.DropDownItem();
            this.IvmAlbIntervention = new C1.Win.C1Input.DropDownItem();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(207)))), ((int)(((byte)(223)))));
            this.c1SplitContainer1.CollapsingAreaColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(237)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(153)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.FixedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(156)))), ((int)(((byte)(184)))));
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(974, 693);
            this.c1SplitContainer1.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(156)))), ((int)(((byte)(184)))));
            this.c1SplitContainer1.TabIndex = 0;
            this.c1SplitContainer1.UseParentVisualStyle = false;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.AutoScroll = true;
            this.c1SplitterPanel1.BackColor = System.Drawing.Color.White;
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.tableLayoutPanel1);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.KeepRelativeSize = false;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(274, 693);
            this.c1SplitterPanel1.SizeRatio = 28.969D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Width = 281;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 688);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1SplitButton1);
            this.panel1.Controls.Add(this.c1Button2);
            this.panel1.Controls.Add(this.c1Button1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 16;
            // 
            // c1SplitButton1
            // 
            this.c1SplitButton1.Items.Add(this.importSurvey);
            this.c1SplitButton1.Items.Add(this.IvmAlbIntervention);
            this.c1SplitButton1.Location = new System.Drawing.Point(3, 3);
            this.c1SplitButton1.Name = "c1SplitButton1";
            this.c1SplitButton1.Size = new System.Drawing.Size(138, 23);
            this.c1SplitButton1.TabIndex = 13;
            this.c1SplitButton1.Tag = "Import";
            this.c1SplitButton1.Text = "Import";
            this.c1SplitButton1.UseVisualStyleBackColor = true;
            this.c1SplitButton1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            this.c1SplitButton1.DropDownItemClicked += new C1.Win.C1Input.DropDownItemClickedEventHandler(this.c1SplitButton1_DropDownItemClicked);
            // 
            // importSurvey
            // 
            this.importSurvey.Tag = "LfSentinelImport";
            this.importSurvey.Text = "LF Sentinel Site/Spot check survey";
            // 
            // IvmAlbIntervention
            // 
            this.IvmAlbIntervention.Tag = "IvmAlbIntervention";
            this.IvmAlbIntervention.Text = "IVM + ALB Intervention";
            // 
            // c1Button2
            // 
            this.c1Button2.Location = new System.Drawing.Point(4, 62);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Size = new System.Drawing.Size(137, 23);
            this.c1Button2.TabIndex = 15;
            this.c1Button2.Tag = "About";
            this.c1Button2.Text = "About";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Black;
            this.c1Button2.Click += new System.EventHandler(this.about_Click);
            // 
            // c1Button1
            // 
            this.c1Button1.Location = new System.Drawing.Point(4, 33);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Size = new System.Drawing.Size(137, 23);
            this.c1Button1.TabIndex = 14;
            this.c1Button1.Tag = "Reports";
            this.c1Button1.Text = "Reports";
            this.c1Button1.UseVisualStyleBackColor = true;
            this.c1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Black;
            this.c1Button1.Click += new System.EventHandler(this.reports_Click);
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.AllColumns.Add(this.olvColumn2);
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(0, 106);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeListView1.MinimumSize = new System.Drawing.Size(273, 279);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(274, 582);
            this.treeListView1.TabIndex = 12;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.UseHyperlinks = true;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            this.treeListView1.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.treeListView1_HyperlinkClicked);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 193;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ViewText";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Hyperlink = true;
            this.olvColumn2.Text = "";
            this.olvColumn2.Width = 73;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 100;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(285, 0);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(689, 693);
            this.c1SplitterPanel2.SizeRatio = 100D;
            this.c1SplitterPanel2.TabIndex = 1;
            // 
            // DemographyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.c1SplitContainer1);
            this.Name = "DemographyView";
            this.Size = new System.Drawing.Size(974, 693);
            this.Load += new System.EventHandler(this.DemographyView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1Input.C1SplitButton c1SplitButton1;
        private C1.Win.C1Input.DropDownItem importSurvey;
        private C1.Win.C1Input.DropDownItem IvmAlbIntervention;
        private C1.Win.C1Input.C1Button c1Button2;
        private C1.Win.C1Input.C1Button c1Button1;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;

    }
}

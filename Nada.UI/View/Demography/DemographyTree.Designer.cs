namespace Nada.UI.View.Demography
{
    partial class DemographyTree
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
            C1.Win.C1Tile.ImageElement imageElement3 = new C1.Win.C1Tile.ImageElement();
            C1.Win.C1Tile.PanelElement panelElement2 = new C1.Win.C1Tile.PanelElement();
            C1.Win.C1Tile.ImageElement imageElement4 = new C1.Win.C1Tile.ImageElement();
            C1.Win.C1Tile.TextElement textElement2 = new C1.Win.C1Tile.TextElement();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.c1TileControl1 = new C1.Win.C1Tile.C1TileControl();
            this.group1 = new C1.Win.C1Tile.Group();
            this.tile2 = new C1.Win.C1Tile.Tile();
            this.tile3 = new C1.Win.C1Tile.Tile();
            this.tile1 = new C1.Win.C1Tile.Tile();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.AllColumns.Add(this.olvColumn2);
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Location = new System.Drawing.Point(0, 100);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(342, 443);
            this.treeListView1.TabIndex = 0;
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
            // c1TileControl1
            // 
            this.c1TileControl1.AllowChecking = true;
            this.c1TileControl1.CellHeight = 80;
            this.c1TileControl1.CellWidth = 80;
            // 
            // 
            // 
            imageElement3.ImageSelector = C1.Win.C1Tile.ImageSelector.Symbol;
            imageElement3.SymbolSize = C1.Win.C1Tile.SymbolSize.Image48x48;
            panelElement2.Alignment = System.Drawing.ContentAlignment.BottomCenter;
            panelElement2.Children.Add(imageElement4);
            panelElement2.Children.Add(textElement2);
            panelElement2.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.c1TileControl1.DefaultTemplate.Elements.Add(imageElement3);
            this.c1TileControl1.DefaultTemplate.Elements.Add(panelElement2);
            this.c1TileControl1.Groups.Add(this.group1);
            this.c1TileControl1.Location = new System.Drawing.Point(-26, -37);
            this.c1TileControl1.Margin = new System.Windows.Forms.Padding(0);
            this.c1TileControl1.Name = "c1TileControl1";
            this.c1TileControl1.Padding = new System.Windows.Forms.Padding(0);
            this.c1TileControl1.Size = new System.Drawing.Size(308, 170);
            this.c1TileControl1.TabIndex = 3;
            this.c1TileControl1.TextSize = 0F;
            this.c1TileControl1.TextX = 0;
            this.c1TileControl1.TextY = 0;
            // 
            // group1
            // 
            this.group1.Name = "group1";
            this.group1.Tag = "";
            this.group1.Tiles.Add(this.tile3);
            this.group1.Tiles.Add(this.tile2);
            this.group1.Tiles.Add(this.tile1);
            // 
            // tile2
            // 
            this.tile2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.tile2.Name = "tile2";
            this.tile2.Symbol = C1.Win.C1Tile.TileSymbol.Week;
            this.tile2.Text = "Reports";
            this.tile2.Click += new System.EventHandler(this.tile2_Click);
            // 
            // tile3
            // 
            this.tile3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.tile3.Name = "tile3";
            this.tile3.Symbol = C1.Win.C1Tile.TileSymbol.OpenFile;
            this.tile3.Text = "Imports";
            this.tile3.Click += new System.EventHandler(this.tile3_Click);
            // 
            // tile1
            // 
            this.tile1.BackColor = System.Drawing.Color.DimGray;
            this.tile1.Name = "tile1";
            this.tile1.Symbol = C1.Win.C1Tile.TileSymbol.Settings;
            this.tile1.Text = "About";
            this.tile1.Click += new System.EventHandler(this.tile1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnlMenu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 543);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(207)))), ((int)(((byte)(223)))));
            this.pnlMenu.Controls.Add(this.c1TileControl1);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(342, 100);
            this.pnlMenu.TabIndex = 5;
            // 
            // DemographyTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DemographyTree";
            this.Size = new System.Drawing.Size(341, 543);
            this.Load += new System.EventHandler(this.DemographyTree_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private C1.Win.C1Tile.C1TileControl c1TileControl1;
        private C1.Win.C1Tile.Tile tile2;
        private C1.Win.C1Tile.Tile tile3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlMenu;
        private C1.Win.C1Tile.Group group1;
        private C1.Win.C1Tile.Tile tile1;
    }
}

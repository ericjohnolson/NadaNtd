namespace Nada.UI.View.Wizard
{
    partial class SplittingDemography
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lstAvailable = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lstSelected = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lblDestination = new Nada.UI.Controls.H3Label();
            this.lblSource = new Nada.UI.Controls.H3Label();
            this.btnDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.lstAvailable.AllColumns.Add(this.olvColumn1);
            this.lstAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.lstAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstAvailable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstAvailable.Location = new System.Drawing.Point(0, 28);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.ShowGroups = false;
            this.lstAvailable.Size = new System.Drawing.Size(304, 321);
            this.lstAvailable.TabIndex = 31;
            this.lstAvailable.UseCompatibleStateImageBehavior = false;
            this.lstAvailable.UseHyperlinks = true;
            this.lstAvailable.View = System.Windows.Forms.View.Details;
            this.lstAvailable.DoubleClick += new System.EventHandler(this.treeAvailable_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Tag = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 300;
            // 
            // lstSelected
            // 
            this.lstSelected.AllColumns.Add(this.olvColumn20);
            this.lstSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn20});
            this.lstSelected.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstSelected.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstSelected.Location = new System.Drawing.Point(385, 28);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.ShowGroups = false;
            this.lstSelected.Size = new System.Drawing.Size(304, 321);
            this.lstSelected.TabIndex = 30;
            this.lstSelected.UseCompatibleStateImageBehavior = false;
            this.lstSelected.UseHyperlinks = true;
            this.lstSelected.View = System.Windows.Forms.View.Details;
            this.lstSelected.DoubleClick += new System.EventHandler(this.treeSelected_DoubleClick);
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "Name";
            this.olvColumn20.CellPadding = null;
            this.olvColumn20.Tag = "Name";
            this.olvColumn20.Text = "Name";
            this.olvColumn20.Width = 300;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDestination.Location = new System.Drawing.Point(385, 5);
            this.lblDestination.Margin = new System.Windows.Forms.Padding(0);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(55, 18);
            this.lblDestination.TabIndex = 29;
            this.lblDestination.Tag = "Selected";
            this.lblDestination.Text = "Selected";
            this.lblDestination.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSource.Location = new System.Drawing.Point(0, 5);
            this.lblSource.Margin = new System.Windows.Forms.Padding(0);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(56, 18);
            this.lblSource.TabIndex = 28;
            this.lblSource.Tag = "Available";
            this.lblSource.Text = "Available";
            this.lblSource.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(310, 188);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselect.Size = new System.Drawing.Size(69, 29);
            this.btnDeselect.TabIndex = 26;
            this.btnDeselect.Values.Text = "<";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(310, 116);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelectAll.Size = new System.Drawing.Size(69, 29);
            this.btnSelectAll.TabIndex = 24;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(310, 152);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelect.Size = new System.Drawing.Size(69, 29);
            this.btnSelect.TabIndex = 25;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(310, 224);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselectAll.Size = new System.Drawing.Size(69, 29);
            this.btnDeselectAll.TabIndex = 27;
            this.btnDeselectAll.Values.Text = "<<";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // SplittingDemography
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lstAvailable);
            this.Controls.Add(this.lstSelected);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnDeselect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnDeselectAll);
            this.Name = "SplittingDemography";
            this.Size = new System.Drawing.Size(703, 363);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private BrightIdeasSoftware.ObjectListView lstAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.ObjectListView lstSelected;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
        private Controls.H3Label lblDestination;
        private Controls.H3Label lblSource;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselectAll;
    }
}

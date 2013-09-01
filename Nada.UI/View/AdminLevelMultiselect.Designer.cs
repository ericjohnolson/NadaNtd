namespace Nada.UI.View
{
    partial class AdminLevelMultiselect
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
            this.pnlSelector = new System.Windows.Forms.Panel();
            this.treeSelected = new BrightIdeasSoftware.TreeListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.treeAvailable = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLevels = new System.Windows.Forms.ComboBox();
            this.bsLevels = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSelector
            // 
            this.pnlSelector.Controls.Add(this.treeSelected);
            this.pnlSelector.Controls.Add(this.treeAvailable);
            this.pnlSelector.Controls.Add(this.label1);
            this.pnlSelector.Controls.Add(this.label2);
            this.pnlSelector.Controls.Add(this.btnDeselect);
            this.pnlSelector.Controls.Add(this.btnSelectAll);
            this.pnlSelector.Controls.Add(this.btnSelect);
            this.pnlSelector.Controls.Add(this.btnDeselectAll);
            this.pnlSelector.Location = new System.Drawing.Point(3, 34);
            this.pnlSelector.Name = "pnlSelector";
            this.pnlSelector.Size = new System.Drawing.Size(693, 332);
            this.pnlSelector.TabIndex = 1;
            // 
            // treeSelected
            // 
            this.treeSelected.AllColumns.Add(this.olvColumn3);
            this.treeSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3});
            this.treeSelected.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeSelected.Location = new System.Drawing.Point(379, 16);
            this.treeSelected.Name = "treeSelected";
            this.treeSelected.OwnerDraw = true;
            this.treeSelected.ShowGroups = false;
            this.treeSelected.Size = new System.Drawing.Size(296, 302);
            this.treeSelected.TabIndex = 1;
            this.treeSelected.UseCompatibleStateImageBehavior = false;
            this.treeSelected.UseHyperlinks = true;
            this.treeSelected.View = System.Windows.Forms.View.Details;
            this.treeSelected.VirtualMode = true;
            this.treeSelected.DoubleClick += new System.EventHandler(this.treeSelected_DoubleClick);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Name";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Name";
            this.olvColumn3.Width = 292;
            // 
            // treeAvailable
            // 
            this.treeAvailable.AllColumns.Add(this.olvColumn1);
            this.treeAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.treeAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeAvailable.Location = new System.Drawing.Point(12, 16);
            this.treeAvailable.Name = "treeAvailable";
            this.treeAvailable.OwnerDraw = true;
            this.treeAvailable.ShowGroups = false;
            this.treeAvailable.Size = new System.Drawing.Size(296, 302);
            this.treeAvailable.TabIndex = 0;
            this.treeAvailable.UseCompatibleStateImageBehavior = false;
            this.treeAvailable.UseHyperlinks = true;
            this.treeAvailable.View = System.Windows.Forms.View.Details;
            this.treeAvailable.VirtualMode = true;
            this.treeAvailable.DoubleClick += new System.EventHandler(this.treeAvailable_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 293;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Available";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Selected";
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(314, 171);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselect.Size = new System.Drawing.Size(59, 25);
            this.btnDeselect.TabIndex = 4;
            this.btnDeselect.Values.Text = "<";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(314, 109);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelectAll.Size = new System.Drawing.Size(59, 25);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(314, 140);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelect.Size = new System.Drawing.Size(59, 25);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(314, 202);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselectAll.Size = new System.Drawing.Size(59, 25);
            this.btnDeselectAll.TabIndex = 5;
            this.btnDeselectAll.Values.Text = "<<";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Level of implementation";
            // 
            // cbLevels
            // 
            this.cbLevels.DataSource = this.bsLevels;
            this.cbLevels.DisplayMember = "DisplayName";
            this.cbLevels.FormattingEnabled = true;
            this.cbLevels.Location = new System.Drawing.Point(134, 7);
            this.cbLevels.Name = "cbLevels";
            this.cbLevels.Size = new System.Drawing.Size(162, 21);
            this.cbLevels.TabIndex = 0;
            this.cbLevels.SelectedIndexChanged += new System.EventHandler(this.cbLevels_SelectedIndexChanged);
            // 
            // bsLevels
            // 
            this.bsLevels.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // AdminLevelMultiselect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbLevels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlSelector);
            this.Name = "AdminLevelMultiselect";
            this.Size = new System.Drawing.Size(706, 383);
            this.Load += new System.EventHandler(this.AdminLevelMultiselect_Load);
            this.pnlSelector.ResumeLayout(false);
            this.pnlSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselectAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLevels;
        private System.Windows.Forms.BindingSource bsLevels;
        private BrightIdeasSoftware.TreeListView treeSelected;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.TreeListView treeAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}

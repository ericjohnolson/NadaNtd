namespace Nada.UI.View
{
    partial class AdminLevelMultiselectAny
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
            this.lvSelected = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.treeAvailable = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbLevels = new System.Windows.Forms.ComboBox();
            this.bsLevels = new System.Windows.Forms.BindingSource(this.components);
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.pnlSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSelector
            // 
            this.pnlSelector.Controls.Add(this.lvSelected);
            this.pnlSelector.Controls.Add(this.h3Label3);
            this.pnlSelector.Controls.Add(this.h3Label2);
            this.pnlSelector.Controls.Add(this.treeAvailable);
            this.pnlSelector.Controls.Add(this.btnDeselect);
            this.pnlSelector.Controls.Add(this.btnSelectAll);
            this.pnlSelector.Controls.Add(this.btnSelect);
            this.pnlSelector.Controls.Add(this.btnDeselectAll);
            this.pnlSelector.Location = new System.Drawing.Point(3, 50);
            this.pnlSelector.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSelector.Name = "pnlSelector";
            this.pnlSelector.Size = new System.Drawing.Size(779, 383);
            this.pnlSelector.TabIndex = 1;
            // 
            // lvSelected
            // 
            this.lvSelected.AllColumns.Add(this.olvColumn20);
            this.lvSelected.AllColumns.Add(this.olvColumn2);
            this.lvSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn20,
            this.olvColumn2});
            this.lvSelected.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvSelected.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSelected.FullRowSelect = true;
            this.lvSelected.Location = new System.Drawing.Point(430, 28);
            this.lvSelected.Name = "lvSelected";
            this.lvSelected.ShowGroups = false;
            this.lvSelected.Size = new System.Drawing.Size(346, 348);
            this.lvSelected.TabIndex = 24;
            this.lvSelected.UseCompatibleStateImageBehavior = false;
            this.lvSelected.UseHyperlinks = true;
            this.lvSelected.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "Name";
            this.olvColumn20.CellPadding = null;
            this.olvColumn20.Tag = "Name";
            this.olvColumn20.Text = "Name";
            this.olvColumn20.Width = 212;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "LevelName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Tag = "LevelName";
            this.olvColumn2.Text = "LevelName";
            this.olvColumn2.Width = 122;
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(428, 7);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(55, 18);
            this.h3Label3.TabIndex = 23;
            this.h3Label3.Tag = "Selected";
            this.h3Label3.Text = "Selected";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(0, 6);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(56, 18);
            this.h3Label2.TabIndex = 20;
            this.h3Label2.Tag = "Available";
            this.h3Label2.Text = "Available";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // treeAvailable
            // 
            this.treeAvailable.AllColumns.Add(this.olvColumn1);
            this.treeAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.treeAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeAvailable.FullRowSelect = true;
            this.treeAvailable.Location = new System.Drawing.Point(0, 28);
            this.treeAvailable.Name = "treeAvailable";
            this.treeAvailable.OwnerDraw = true;
            this.treeAvailable.ShowGroups = false;
            this.treeAvailable.Size = new System.Drawing.Size(345, 348);
            this.treeAvailable.TabIndex = 0;
            this.treeAvailable.UseCompatibleStateImageBehavior = false;
            this.treeAvailable.UseHyperlinks = true;
            this.treeAvailable.View = System.Windows.Forms.View.Details;
            this.treeAvailable.VirtualMode = true;
            this.treeAvailable.CellClick += new System.EventHandler<BrightIdeasSoftware.CellClickEventArgs>(this.treeAvailable_CellClick);
            this.treeAvailable.DoubleClick += new System.EventHandler(this.treeAvailable_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 290;
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(352, 207);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselect.Size = new System.Drawing.Size(69, 29);
            this.btnDeselect.TabIndex = 4;
            this.btnDeselect.Values.Text = "<";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(352, 135);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelectAll.Size = new System.Drawing.Size(69, 29);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(352, 171);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelect.Size = new System.Drawing.Size(69, 29);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(352, 242);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselectAll.Size = new System.Drawing.Size(69, 29);
            this.btnDeselectAll.TabIndex = 5;
            this.btnDeselectAll.Values.Text = "<<";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // cbLevels
            // 
            this.cbLevels.DataSource = this.bsLevels;
            this.cbLevels.DisplayMember = "DisplayName";
            this.cbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevels.FormattingEnabled = true;
            this.cbLevels.Location = new System.Drawing.Point(3, 22);
            this.cbLevels.Name = "cbLevels";
            this.cbLevels.Size = new System.Drawing.Size(188, 23);
            this.cbLevels.TabIndex = 0;
            this.cbLevels.SelectedIndexChanged += new System.EventHandler(this.cbLevels_SelectedIndexChanged);
            // 
            // bsLevels
            // 
            this.bsLevels.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(138, 18);
            this.h3Label1.TabIndex = 19;
            this.h3Label1.Tag = "LevelImplementation";
            this.h3Label1.Text = "Level of implementation";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // AdminLevelMultiselectAny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.cbLevels);
            this.Controls.Add(this.pnlSelector);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AdminLevelMultiselectAny";
            this.Size = new System.Drawing.Size(782, 433);
            this.Load += new System.EventHandler(this.AdminLevelMultiselect_Load);
            this.pnlSelector.ResumeLayout(false);
            this.pnlSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSelector;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselectAll;
        private System.Windows.Forms.ComboBox cbLevels;
        private System.Windows.Forms.BindingSource bsLevels;
        private BrightIdeasSoftware.TreeListView treeAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private Controls.H3Label h3Label1;
        private Controls.H3Label h3Label2;
        private BrightIdeasSoftware.ObjectListView lvSelected;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private Controls.H3Label h3Label3;
    }
}

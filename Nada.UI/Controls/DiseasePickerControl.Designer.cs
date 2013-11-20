namespace Nada.UI.View
{
    partial class DiseasePickerControl
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
            this.lstAvailable = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lstSelected = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.btnDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lnkAddDisease = new Nada.UI.Controls.H3Link();
            this.bsLevels = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSelector
            // 
            this.pnlSelector.Controls.Add(this.lstAvailable);
            this.pnlSelector.Controls.Add(this.lstSelected);
            this.pnlSelector.Controls.Add(this.h3Label3);
            this.pnlSelector.Controls.Add(this.h3Label2);
            this.pnlSelector.Controls.Add(this.btnDeselect);
            this.pnlSelector.Controls.Add(this.btnSelectAll);
            this.pnlSelector.Controls.Add(this.btnSelect);
            this.pnlSelector.Controls.Add(this.btnDeselectAll);
            this.pnlSelector.Location = new System.Drawing.Point(0, 0);
            this.pnlSelector.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSelector.Name = "pnlSelector";
            this.pnlSelector.Size = new System.Drawing.Size(652, 219);
            this.pnlSelector.TabIndex = 1;
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
            this.lstAvailable.Size = new System.Drawing.Size(278, 176);
            this.lstAvailable.TabIndex = 23;
            this.lstAvailable.UseCompatibleStateImageBehavior = false;
            this.lstAvailable.UseHyperlinks = true;
            this.lstAvailable.View = System.Windows.Forms.View.Details;
            this.lstAvailable.DoubleClick += new System.EventHandler(this.treeAvailable_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DisplayName";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Tag = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 268;
            // 
            // lstSelected
            // 
            this.lstSelected.AllColumns.Add(this.olvColumn20);
            this.lstSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn20});
            this.lstSelected.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstSelected.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstSelected.Location = new System.Drawing.Point(349, 28);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.ShowGroups = false;
            this.lstSelected.Size = new System.Drawing.Size(278, 176);
            this.lstSelected.TabIndex = 22;
            this.lstSelected.UseCompatibleStateImageBehavior = false;
            this.lstSelected.UseHyperlinks = true;
            this.lstSelected.View = System.Windows.Forms.View.Details;
            this.lstSelected.DoubleClick += new System.EventHandler(this.treeSelected_DoubleClick);
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "DisplayName";
            this.olvColumn20.CellPadding = null;
            this.olvColumn20.Tag = "Name";
            this.olvColumn20.Text = "Name";
            this.olvColumn20.Width = 273;
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(349, 5);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(62, 16);
            this.h3Label3.TabIndex = 21;
            this.h3Label3.Tag = "Selected";
            this.h3Label3.Text = "Selected";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(0, 5);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(65, 16);
            this.h3Label2.TabIndex = 20;
            this.h3Label2.Tag = "Available";
            this.h3Label2.Text = "Available";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(284, 117);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselect.Size = new System.Drawing.Size(59, 25);
            this.btnDeselect.TabIndex = 4;
            this.btnDeselect.Values.Text = "<";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(284, 55);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelectAll.Size = new System.Drawing.Size(59, 25);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(284, 86);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelect.Size = new System.Drawing.Size(59, 25);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(284, 148);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselectAll.Size = new System.Drawing.Size(59, 25);
            this.btnDeselectAll.TabIndex = 5;
            this.btnDeselectAll.Values.Text = "<<";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // lnkAddDisease
            // 
            this.lnkAddDisease.AutoSize = true;
            this.lnkAddDisease.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkAddDisease.BackColor = System.Drawing.Color.Transparent;
            this.lnkAddDisease.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAddDisease.Location = new System.Drawing.Point(0, 219);
            this.lnkAddDisease.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lnkAddDisease.Name = "lnkAddDisease";
            this.lnkAddDisease.Size = new System.Drawing.Size(108, 16);
            this.lnkAddDisease.TabIndex = 2;
            this.lnkAddDisease.Tag = "AddDiseaseLink";
            this.lnkAddDisease.Text = "AddDiseaseLink";
            this.lnkAddDisease.ClickOverride += new System.Action(this.lnkAddDisease_ClickOverride);
            // 
            // bsLevels
            // 
            this.bsLevels.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // DiseasePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lnkAddDisease);
            this.Controls.Add(this.pnlSelector);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DiseasePickerControl";
            this.Size = new System.Drawing.Size(652, 255);
            this.Load += new System.EventHandler(this.DiseasePickerControl_Load);
            this.pnlSelector.ResumeLayout(false);
            this.pnlSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).EndInit();
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
        private System.Windows.Forms.BindingSource bsLevels;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label3;
        private Controls.H3Link lnkAddDisease;
        private BrightIdeasSoftware.ObjectListView lstAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.ObjectListView lstSelected;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
    }
}

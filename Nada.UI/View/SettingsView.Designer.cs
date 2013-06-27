namespace Nada.UI.View
{
    partial class SettingsView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvDiseases = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAddDisease = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSaveAdminLevels = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.grdAdminLevels = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnAdminLevel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSavePops = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.grdPopGroups = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnPopGroup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.levelNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adminLevelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.abbreviationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.popGroupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvDiseases)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAdminLevels)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPopGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popGroupBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(890, 519);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(147, 39);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "Settings.";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 54);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 462);
            this.panel1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(612, 352);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvDiseases);
            this.tabPage2.Controls.Add(this.btnAddDisease);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(604, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Diseases";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvDiseases
            // 
            this.lvDiseases.AllColumns.Add(this.olvColumn1);
            this.lvDiseases.AllColumns.Add(this.olvColumn2);
            this.lvDiseases.AllColumns.Add(this.olvColumn3);
            this.lvDiseases.AllColumns.Add(this.olvColumn4);
            this.lvDiseases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.lvDiseases.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDiseases.Location = new System.Drawing.Point(6, 6);
            this.lvDiseases.Name = "lvDiseases";
            this.lvDiseases.ShowGroups = false;
            this.lvDiseases.ShowHeaderInAllViews = false;
            this.lvDiseases.Size = new System.Drawing.Size(592, 125);
            this.lvDiseases.TabIndex = 2;
            this.lvDiseases.UseCompatibleStateImageBehavior = false;
            this.lvDiseases.UseHyperlinks = true;
            this.lvDiseases.View = System.Windows.Forms.View.Details;
            this.lvDiseases.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvDiseases_HyperlinkClicked);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DisplayName";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 111;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Code";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Code";
            // 
            // btnAddDisease
            // 
            this.btnAddDisease.Location = new System.Drawing.Point(536, 137);
            this.btnAddDisease.Name = "btnAddDisease";
            this.btnAddDisease.Size = new System.Drawing.Size(62, 25);
            this.btnAddDisease.TabIndex = 1;
            this.btnAddDisease.Values.Text = "Add";
            this.btnAddDisease.Click += new System.EventHandler(this.btnAddDisease_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSaveAdminLevels);
            this.tabPage1.Controls.Add(this.grdAdminLevels);
            this.tabPage1.Controls.Add(this.btnAdminLevel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(604, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Admin Levels";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSaveAdminLevels
            // 
            this.btnSaveAdminLevels.Location = new System.Drawing.Point(490, 137);
            this.btnSaveAdminLevels.Name = "btnSaveAdminLevels";
            this.btnSaveAdminLevels.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.btnSaveAdminLevels.Size = new System.Drawing.Size(108, 25);
            this.btnSaveAdminLevels.TabIndex = 7;
            this.btnSaveAdminLevels.Values.Text = "Save Changes";
            this.btnSaveAdminLevels.Click += new System.EventHandler(this.btnSaveAdminLevels_Click);
            // 
            // grdAdminLevels
            // 
            this.grdAdminLevels.AllowUserToAddRows = false;
            this.grdAdminLevels.AllowUserToDeleteRows = false;
            this.grdAdminLevels.AutoGenerateColumns = false;
            this.grdAdminLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAdminLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.levelNumberDataGridViewTextBoxColumn,
            this.displayNameDataGridViewTextBoxColumn1});
            this.grdAdminLevels.DataSource = this.adminLevelBindingSource;
            this.grdAdminLevels.Location = new System.Drawing.Point(6, 6);
            this.grdAdminLevels.Name = "grdAdminLevels";
            this.grdAdminLevels.Size = new System.Drawing.Size(592, 125);
            this.grdAdminLevels.TabIndex = 6;
            // 
            // btnAdminLevel
            // 
            this.btnAdminLevel.Location = new System.Drawing.Point(422, 137);
            this.btnAdminLevel.Name = "btnAdminLevel";
            this.btnAdminLevel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.btnAdminLevel.Size = new System.Drawing.Size(62, 25);
            this.btnAdminLevel.TabIndex = 3;
            this.btnAdminLevel.Values.Text = "Add";
            this.btnAdminLevel.Click += new System.EventHandler(this.btnAdminLevel_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSavePops);
            this.tabPage3.Controls.Add(this.grdPopGroups);
            this.tabPage3.Controls.Add(this.btnPopGroup);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(604, 326);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Population Groups";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSavePops
            // 
            this.btnSavePops.Location = new System.Drawing.Point(490, 137);
            this.btnSavePops.Name = "btnSavePops";
            this.btnSavePops.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.btnSavePops.Size = new System.Drawing.Size(108, 25);
            this.btnSavePops.TabIndex = 8;
            this.btnSavePops.Values.Text = "Save Changes";
            this.btnSavePops.Click += new System.EventHandler(this.btnSavePops_Click);
            // 
            // grdPopGroups
            // 
            this.grdPopGroups.AllowUserToAddRows = false;
            this.grdPopGroups.AllowUserToDeleteRows = false;
            this.grdPopGroups.AutoGenerateColumns = false;
            this.grdPopGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPopGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.abbreviationDataGridViewTextBoxColumn,
            this.displayNameDataGridViewTextBoxColumn});
            this.grdPopGroups.DataSource = this.popGroupBindingSource;
            this.grdPopGroups.Location = new System.Drawing.Point(6, 6);
            this.grdPopGroups.Name = "grdPopGroups";
            this.grdPopGroups.Size = new System.Drawing.Size(595, 125);
            this.grdPopGroups.TabIndex = 6;
            // 
            // btnPopGroup
            // 
            this.btnPopGroup.Location = new System.Drawing.Point(422, 137);
            this.btnPopGroup.Name = "btnPopGroup";
            this.btnPopGroup.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.btnPopGroup.Size = new System.Drawing.Size(62, 25);
            this.btnPopGroup.TabIndex = 3;
            this.btnPopGroup.Values.Text = "Add";
            this.btnPopGroup.Click += new System.EventHandler(this.btnPopGroup_Click);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "IsEnabledText";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Show";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "EditText";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.Hideable = false;
            this.olvColumn4.Hyperlink = true;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Edit";
            // 
            // levelNumberDataGridViewTextBoxColumn
            // 
            this.levelNumberDataGridViewTextBoxColumn.DataPropertyName = "LevelNumber";
            this.levelNumberDataGridViewTextBoxColumn.HeaderText = "Admin Level";
            this.levelNumberDataGridViewTextBoxColumn.Name = "levelNumberDataGridViewTextBoxColumn";
            // 
            // displayNameDataGridViewTextBoxColumn1
            // 
            this.displayNameDataGridViewTextBoxColumn1.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.displayNameDataGridViewTextBoxColumn1.Name = "displayNameDataGridViewTextBoxColumn1";
            // 
            // adminLevelBindingSource
            // 
            this.adminLevelBindingSource.DataSource = typeof(Nada.Model.AdminLevel);
            // 
            // abbreviationDataGridViewTextBoxColumn
            // 
            this.abbreviationDataGridViewTextBoxColumn.DataPropertyName = "Abbreviation";
            this.abbreviationDataGridViewTextBoxColumn.HeaderText = "Abbreviation";
            this.abbreviationDataGridViewTextBoxColumn.Name = "abbreviationDataGridViewTextBoxColumn";
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            // 
            // popGroupBindingSource
            // 
            this.popGroupBindingSource.DataSource = typeof(Nada.Model.PopGroup);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(890, 519);
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvDiseases)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAdminLevels)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPopGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popGroupBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddDisease;
        private BrightIdeasSoftware.ObjectListView lvDiseases;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAdminLevel;
        private System.Windows.Forms.TabPage tabPage3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPopGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grdAdminLevels;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grdPopGroups;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource adminLevelBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn abbreviationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource popGroupBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSaveAdminLevels;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSavePops;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}

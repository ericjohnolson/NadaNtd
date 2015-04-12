namespace Nada.UI.Controls
{
    partial class AdminLevelTypesControl
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
            this.lvLevels = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.partnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAggLevel = new Nada.UI.Controls.H3bLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lvLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvLevels
            // 
            this.lvLevels.AllColumns.Add(this.olvColumn3);
            this.lvLevels.AllColumns.Add(this.olvColumn2);
            this.lvLevels.AllColumns.Add(this.olvColumn1);
            this.lvLevels.AllColumns.Add(this.olvColumn14);
            this.lvLevels.AllColumns.Add(this.olvColumn4);
            this.lvLevels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn2,
            this.olvColumn1,
            this.olvColumn14,
            this.olvColumn4});
            this.lvLevels.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvLevels.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvLevels.Location = new System.Drawing.Point(0, 0);
            this.lvLevels.Name = "lvLevels";
            this.lvLevels.ShowGroups = false;
            this.lvLevels.Size = new System.Drawing.Size(583, 161);
            this.lvLevels.TabIndex = 6;
            this.lvLevels.UseCompatibleStateImageBehavior = false;
            this.lvLevels.UseHyperlinks = true;
            this.lvLevels.View = System.Windows.Forms.View.Details;
            this.lvLevels.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvLevels_HyperlinkClicked);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "LevelNumber";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Tag = "LevelNumber";
            this.olvColumn3.Text = "LevelNumber";
            this.olvColumn3.Width = 49;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "DisplayName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Tag = "Name";
            this.olvColumn2.Text = "Name";
            this.olvColumn2.Width = 200;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "IsAggText";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Width = 85;
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "EditText";
            this.olvColumn14.CellPadding = null;
            this.olvColumn14.Hyperlink = true;
            this.olvColumn14.IsEditable = false;
            this.olvColumn14.Text = "Edit";
            this.olvColumn14.Width = 51;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "ImportText";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.Hyperlink = true;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Import";
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(3, 168);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(160, 16);
            this.fieldLink1.TabIndex = 5;
            this.fieldLink1.Tag = "AddAdminLevelTypeLink";
            this.fieldLink1.Text = "AddAdminLevelTypeLink";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // partnerBindingSource
            // 
            this.partnerBindingSource.DataSource = typeof(Nada.Model.Partner);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblAggLevel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 187);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(131, 41);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // lblAggLevel
            // 
            this.lblAggLevel.AutoSize = true;
            this.lblAggLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblAggLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAggLevel.Location = new System.Drawing.Point(0, 15);
            this.lblAggLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblAggLevel.Name = "lblAggLevel";
            this.lblAggLevel.Size = new System.Drawing.Size(131, 16);
            this.lblAggLevel.TabIndex = 6;
            this.lblAggLevel.Tag = "SettingsAggLevelDesc";
            this.lblAggLevel.Text = "SettingsAggLevelDesc";
            this.lblAggLevel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // AdminLevelTypesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lvLevels);
            this.Controls.Add(this.fieldLink1);
            this.Name = "AdminLevelTypesControl";
            this.Size = new System.Drawing.Size(586, 231);
            this.Load += new System.EventHandler(this.AdminLevelTypesControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource partnerBindingSource;
        private FieldLink fieldLink1;
        private BrightIdeasSoftware.ObjectListView lvLevels;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private H3bLabel lblAggLevel;
        private BrightIdeasSoftware.OLVColumn olvColumn4;

    }
}

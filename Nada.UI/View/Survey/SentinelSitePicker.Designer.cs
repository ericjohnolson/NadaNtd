namespace Nada.UI.View
{
    partial class SentinelSitePicker
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsAdminLevel = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddNew = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lvChildren = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvChildren)).BeginInit();
            this.SuspendLayout();
            // 
            // bsAdminLevel
            // 
            this.bsAdminLevel.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(305, 215);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(90, 25);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Values.Text = "Add New...";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lvChildren
            // 
            this.lvChildren.AllColumns.Add(this.olvColumn2);
            this.lvChildren.AllColumns.Add(this.olvColumn14);
            this.lvChildren.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn14});
            this.lvChildren.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvChildren.Location = new System.Drawing.Point(15, 58);
            this.lvChildren.Name = "lvChildren";
            this.lvChildren.ShowGroups = false;
            this.lvChildren.ShowHeaderInAllViews = true;
            this.lvChildren.Size = new System.Drawing.Size(380, 144);
            this.lvChildren.TabIndex = 18;
            this.lvChildren.UseCompatibleStateImageBehavior = false;
            this.lvChildren.UseHyperlinks = true;
            this.lvChildren.View = System.Windows.Forms.View.Details;
            this.lvChildren.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvChildren_HyperlinkClicked);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "SiteName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Sentinel Site Name";
            this.olvColumn2.Width = 283;
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "SelectText";
            this.olvColumn14.CellPadding = null;
            this.olvColumn14.Hyperlink = true;
            this.olvColumn14.IsEditable = false;
            this.olvColumn14.Text = "Select";
            this.olvColumn14.Width = 81;
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(10, 12);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl1.TabIndex = 19;
            // 
            // SentinelSitePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(407, 252);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.lvChildren);
            this.Controls.Add(this.btnAddNew);
            this.Name = "SentinelSitePicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sentinel Sites";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvChildren)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsAdminLevel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private BrightIdeasSoftware.ObjectListView lvChildren;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private AdminLevelPickerControl adminLevelPickerControl1;
    }
}
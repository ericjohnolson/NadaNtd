namespace Nada.UI.View
{
    partial class PartnerList
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
            this.lvDistros = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAddNew = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.lvDistros)).BeginInit();
            this.SuspendLayout();
            // 
            // lvDistros
            // 
            this.lvDistros.AllColumns.Add(this.olvColumn2);
            this.lvDistros.AllColumns.Add(this.olvColumn14);
            this.lvDistros.AllColumns.Add(this.olvColumn1);
            this.lvDistros.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn14,
            this.olvColumn1});
            this.lvDistros.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDistros.Location = new System.Drawing.Point(16, 12);
            this.lvDistros.Name = "lvDistros";
            this.lvDistros.ShowGroups = false;
            this.lvDistros.Size = new System.Drawing.Size(380, 190);
            this.lvDistros.TabIndex = 0;
            this.lvDistros.UseCompatibleStateImageBehavior = false;
            this.lvDistros.UseHyperlinks = true;
            this.lvDistros.View = System.Windows.Forms.View.Details;
            this.lvDistros.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvDistros_HyperlinkClicked);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "DisplayName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Partner/Funder";
            this.olvColumn2.Width = 267;
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
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DeleteText";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Hyperlink = true;
            this.olvColumn1.Text = "Delete";
            this.olvColumn1.Width = 52;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(306, 208);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(90, 25);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Values.Text = "Add New...";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(224, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PartnerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(408, 254);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvDistros);
            this.Controls.Add(this.btnAddNew);
            this.Name = "PartnerList";
            this.Text = "Partners and Funders";
            this.Load += new System.EventHandler(this.DistributionMethodList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvDistros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvDistros;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
    }
}
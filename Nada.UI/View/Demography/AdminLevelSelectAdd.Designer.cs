namespace Nada.UI.View.Demography
{
    partial class AdminLevelSelectAdd
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
            this.treeAvailable = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lnkNew = new Nada.UI.Controls.H3Link();
            this.lblTitle = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).BeginInit();
            this.SuspendLayout();
            // 
            // treeAvailable
            // 
            this.treeAvailable.AllColumns.Add(this.olvColumn1);
            this.treeAvailable.AllColumns.Add(this.olvColumn2);
            this.treeAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.treeAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeAvailable.FullRowSelect = true;
            this.treeAvailable.Location = new System.Drawing.Point(19, 58);
            this.treeAvailable.Name = "treeAvailable";
            this.treeAvailable.OwnerDraw = true;
            this.treeAvailable.ShowGroups = false;
            this.treeAvailable.Size = new System.Drawing.Size(409, 336);
            this.treeAvailable.TabIndex = 51;
            this.treeAvailable.UseCompatibleStateImageBehavior = false;
            this.treeAvailable.UseHyperlinks = true;
            this.treeAvailable.View = System.Windows.Forms.View.Details;
            this.treeAvailable.VirtualMode = true;
            this.treeAvailable.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.treeAvailable_HyperlinkClicked);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Tag = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 290;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ViewText";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Hyperlink = true;
            this.olvColumn2.Tag = "Select";
            this.olvColumn2.Text = "Select";
            this.olvColumn2.Width = 76;
            // 
            // lnkNew
            // 
            this.lnkNew.AutoSize = true;
            this.lnkNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkNew.BackColor = System.Drawing.Color.Transparent;
            this.lnkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNew.Location = new System.Drawing.Point(19, 409);
            this.lnkNew.Margin = new System.Windows.Forms.Padding(0);
            this.lnkNew.Name = "lnkNew";
            this.lnkNew.Size = new System.Drawing.Size(96, 15);
            this.lnkNew.TabIndex = 50;
            this.lnkNew.Tag = "NewAdminLevel";
            this.lnkNew.Text = "NewAdminLevel";
            this.lnkNew.ClickOverride += new System.Action(this.lnkNew_ClickOverride);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(158, 28);
            this.lblTitle.TabIndex = 53;
            this.lblTitle.Tag = "SelectAdminLevel";
            this.lblTitle.Text = "SelectAdminLevel";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(7);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(452, 7);
            this.hr1.TabIndex = 52;
            // 
            // AdminLevelSelectAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 471);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.treeAvailable);
            this.Controls.Add(this.lnkNew);
            this.Name = "AdminLevelSelectAdd";
            this.Tag = "SelectAdminLevel";
            this.Text = "SelectAdminLevel";
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private Controls.H3Link lnkNew;
        private System.Windows.Forms.Label lblTitle;
        private Controls.HR hr1;
    }
}

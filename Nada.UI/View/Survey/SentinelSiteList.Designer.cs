namespace Nada.UI.View.Survey
{
    partial class SentinelSiteList
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.sentinelSiteListView = new BrightIdeasSoftware.ObjectListView();
            this.nameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.editColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.deleteColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.AddSiteLink = new Nada.UI.Controls.FieldLink();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteListView)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Location = new System.Drawing.Point(12, 9);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(155, 15);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Tag = "SentinelSitesForAdminUnit";
            this.headerLabel.Text = "SentinelSitesForAdminUnit";
            // 
            // sentinelSiteListView
            // 
            this.sentinelSiteListView.AllColumns.Add(this.nameColumn);
            this.sentinelSiteListView.AllColumns.Add(this.editColumn);
            this.sentinelSiteListView.AllColumns.Add(this.deleteColumn);
            this.sentinelSiteListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.editColumn,
            this.deleteColumn});
            this.sentinelSiteListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.sentinelSiteListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.sentinelSiteListView.Location = new System.Drawing.Point(25, 28);
            this.sentinelSiteListView.Name = "sentinelSiteListView";
            this.sentinelSiteListView.Size = new System.Drawing.Size(582, 343);
            this.sentinelSiteListView.TabIndex = 1;
            this.sentinelSiteListView.UseCompatibleStateImageBehavior = false;
            this.sentinelSiteListView.UseHyperlinks = true;
            this.sentinelSiteListView.View = System.Windows.Forms.View.Details;
            this.sentinelSiteListView.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.sentinelSiteListView_HyperlinkClicked);
            // 
            // nameColumn
            // 
            this.nameColumn.AspectName = "SiteName";
            this.nameColumn.CellPadding = null;
            this.nameColumn.Groupable = false;
            this.nameColumn.Tag = "SiteName";
            this.nameColumn.Text = "SiteName";
            this.nameColumn.Width = 438;
            // 
            // editColumn
            // 
            this.editColumn.AspectName = "EditText";
            this.editColumn.CellPadding = null;
            this.editColumn.Hyperlink = true;
            this.editColumn.IsEditable = false;
            this.editColumn.Tag = "Edit";
            this.editColumn.Text = "Edit";
            this.editColumn.Width = 67;
            // 
            // deleteColumn
            // 
            this.deleteColumn.AspectName = "DeleteText";
            this.deleteColumn.CellPadding = null;
            this.deleteColumn.Hyperlink = true;
            this.deleteColumn.Tag = "Delete";
            this.deleteColumn.Text = "Delete";
            this.deleteColumn.Width = 65;
            // 
            // AddSiteLink
            // 
            this.AddSiteLink.AutoSize = true;
            this.AddSiteLink.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddSiteLink.BackColor = System.Drawing.Color.Transparent;
            this.AddSiteLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddSiteLink.Location = new System.Drawing.Point(527, 9);
            this.AddSiteLink.Margin = new System.Windows.Forms.Padding(0);
            this.AddSiteLink.Name = "AddSiteLink";
            this.AddSiteLink.Size = new System.Drawing.Size(80, 16);
            this.AddSiteLink.TabIndex = 2;
            this.AddSiteLink.Tag = "AddNewSite";
            this.AddSiteLink.Text = "AddSiteLink";
            this.AddSiteLink.OnClick += new System.Action(this.AddSiteLink_OnClick);
            // 
            // SentinelSiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 400);
            this.Controls.Add(this.AddSiteLink);
            this.Controls.Add(this.sentinelSiteListView);
            this.Controls.Add(this.headerLabel);
            this.Name = "SentinelSiteList";
            this.Tag = "SentinelSiteList";
            this.Text = "SentinelSiteList";
            this.Load += new System.EventHandler(this.SentinelSiteList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private BrightIdeasSoftware.ObjectListView sentinelSiteListView;
        private BrightIdeasSoftware.OLVColumn nameColumn;
        private Controls.FieldLink AddSiteLink;
        private BrightIdeasSoftware.OLVColumn editColumn;
        private BrightIdeasSoftware.OLVColumn deleteColumn;


    }
}
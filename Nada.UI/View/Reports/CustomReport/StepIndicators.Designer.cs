namespace Nada.UI.View.Reports.CustomReport
{
    partial class StepIndicators
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
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeListView1.CheckBoxes = true;
            this.treeListView1.CheckedAspectName = "IsChecked";
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView1.FullRowSelect = true;
            this.treeListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.treeListView1.Location = new System.Drawing.Point(0, 0);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.treeListView1.MinimumSize = new System.Drawing.Size(250, 321);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.RowHeight = 30;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.ShowImagesOnSubItems = true;
            this.treeListView1.Size = new System.Drawing.Size(714, 372);
            this.treeListView1.TabIndex = 13;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.UseHyperlinks = true;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Tag = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 193;
            this.olvColumn1.WordWrap = true;
            // 
            // StepIndicators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.treeListView1);
            this.Name = "StepIndicators";
            this.Size = new System.Drawing.Size(714, 372);
            this.Load += new System.EventHandler(this.StepIndicators_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;

    }
}

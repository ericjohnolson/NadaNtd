namespace Nada.UI.View.Wizard
{
    partial class SplittingSource
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.treeAvailable = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bsImportOptions = new System.Windows.Forms.BindingSource(this.components);
            this.typeListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(46, 18);
            this.h3Label1.TabIndex = 21;
            this.h3Label1.Tag = "Source";
            this.h3Label1.Text = "Source";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsImportOptions;
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
            this.treeAvailable.Location = new System.Drawing.Point(3, 21);
            this.treeAvailable.Name = "treeAvailable";
            this.treeAvailable.OwnerDraw = true;
            this.treeAvailable.ShowGroups = false;
            this.treeAvailable.Size = new System.Drawing.Size(444, 292);
            this.treeAvailable.TabIndex = 49;
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
            // bsImportOptions
            // 
            this.bsImportOptions.DataSource = typeof(Nada.Model.Imports.ImportOptions);
            // 
            // typeListItemBindingSource
            // 
            this.typeListItemBindingSource.DataSource = typeof(Nada.Model.Imports.TypeListItem);
            // 
            // SplittingSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.treeAvailable);
            this.Controls.Add(this.h3Label1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "SplittingSource";
            this.Size = new System.Drawing.Size(450, 367);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsImportOptions;
        private System.Windows.Forms.BindingSource typeListItemBindingSource;
        private BrightIdeasSoftware.TreeListView treeAvailable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;

    }
}

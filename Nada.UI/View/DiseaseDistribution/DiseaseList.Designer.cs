namespace Nada.UI.View
{
    partial class DiseaseList
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
            this.lvDiseases = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.lvDiseases)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDiseases
            // 
            this.lvDiseases.AllColumns.Add(this.olvColumn2);
            this.lvDiseases.AllColumns.Add(this.olvColumn14);
            this.lvDiseases.AllColumns.Add(this.olvColumn1);
            this.lvDiseases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn14,
            this.olvColumn1});
            this.lvDiseases.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDiseases.Location = new System.Drawing.Point(26, 52);
            this.lvDiseases.Name = "lvDiseases";
            this.lvDiseases.ShowGroups = false;
            this.lvDiseases.ShowHeaderInAllViews = false;
            this.lvDiseases.Size = new System.Drawing.Size(380, 152);
            this.lvDiseases.TabIndex = 0;
            this.lvDiseases.UseCompatibleStateImageBehavior = false;
            this.lvDiseases.UseHyperlinks = true;
            this.lvDiseases.View = System.Windows.Forms.View.Details;
            this.lvDiseases.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvDistros_HyperlinkClicked);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "DisplayName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Diseases";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.label3.Location = new System.Drawing.Point(21, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 28);
            this.label3.TabIndex = 22;
            this.label3.Tag = "Diseases";
            this.label3.Text = "Diseases";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr1.Size = new System.Drawing.Size(433, 5);
            this.hr1.TabIndex = 21;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(220, 222);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(186, 31);
            this.tableLayoutPanel4.TabIndex = 41;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(3, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Tag = "Cancel";
            this.kryptonButton1.Values.Text = "Cancel";
            this.kryptonButton1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(106, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Tag = "Add";
            this.btnSave.Values.Text = "Add";
            this.btnSave.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DeleteText";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Hyperlink = true;
            this.olvColumn1.Text = "Delete";
            // 
            // DiseaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(433, 276);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.lvDiseases);
            this.Name = "DiseaseList";
            this.Tag = "Diseases";
            this.Text = "Diseases";
            this.Load += new System.EventHandler(this.DistributionMethodList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvDiseases)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvDiseases;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}
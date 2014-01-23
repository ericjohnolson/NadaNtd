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
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            ((System.ComponentModel.ISupportInitialize)(this.lvDistros)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.lvDistros.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvDistros.Location = new System.Drawing.Point(30, 60);
            this.lvDistros.Name = "lvDistros";
            this.lvDistros.ShowGroups = false;
            this.lvDistros.Size = new System.Drawing.Size(443, 219);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label3.Location = new System.Drawing.Point(24, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 28);
            this.label3.TabIndex = 45;
            this.label3.Tag = "Partners";
            this.label3.Text = "Partners";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(6);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(503, 6);
            this.hr1.TabIndex = 44;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.c1Button2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(29, 323);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(120, 33);
            this.tableLayoutPanel2.TabIndex = 50;
            // 
            // c1Button2
            // 
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.Location = new System.Drawing.Point(3, 3);
            this.c1Button2.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button2.Size = new System.Drawing.Size(90, 27);
            this.c1Button2.TabIndex = 4;
            this.c1Button2.Tag = "Done";
            this.c1Button2.Text = "Done";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(29, 291);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(100, 16);
            this.fieldLink1.TabIndex = 51;
            this.fieldLink1.Tag = "AddPartnerLink";
            this.fieldLink1.Text = "AddPartnerLink";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // PartnerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 379);
            this.Controls.Add(this.fieldLink1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.lvDistros);
            this.Name = "PartnerList";
            this.Tag = "Partners";
            this.Text = "Partners/Funders";
            this.Load += new System.EventHandler(this.DistributionMethodList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvDistros)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvDistros;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private C1.Win.C1Input.C1Button c1Button2;
        private Controls.FieldLink fieldLink1;
    }
}
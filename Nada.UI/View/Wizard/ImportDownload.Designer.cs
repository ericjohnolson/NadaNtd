namespace Nada.UI.View
{
    partial class ImportDownload
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.adminLevelMultiselect1 = new Nada.UI.View.AdminLevelMultiselect();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton4 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label3.Location = new System.Drawing.Point(18, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 28);
            this.label3.TabIndex = 20;
            this.label3.Tag = "Import";
            this.label3.Text = "Import";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(712, 5);
            this.hr1.TabIndex = 19;
            // 
            // adminLevelMultiselect1
            // 
            this.adminLevelMultiselect1.AutoSize = true;
            this.adminLevelMultiselect1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelMultiselect1.Location = new System.Drawing.Point(21, 49);
            this.adminLevelMultiselect1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelMultiselect1.Name = "adminLevelMultiselect1";
            this.adminLevelMultiselect1.Size = new System.Drawing.Size(671, 375);
            this.adminLevelMultiselect1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton4, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(23, 432);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(299, 31);
            this.tableLayoutPanel4.TabIndex = 21;
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.AutoSize = true;
            this.kryptonButton3.Location = new System.Drawing.Point(122, 3);
            this.kryptonButton3.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.kryptonButton3.MinimumSize = new System.Drawing.Size(78, 25);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton3.Size = new System.Drawing.Size(78, 25);
            this.kryptonButton3.TabIndex = 61;
            this.kryptonButton3.Tag = "UploadFile";
            this.kryptonButton3.Values.Text = "UploadFile";
            this.kryptonButton3.Click += new System.EventHandler(this.DoImport_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.AutoSize = true;
            this.kryptonButton2.Location = new System.Drawing.Point(3, 3);
            this.kryptonButton2.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.kryptonButton2.MinimumSize = new System.Drawing.Size(78, 25);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton2.Size = new System.Drawing.Size(101, 25);
            this.kryptonButton2.TabIndex = 1;
            this.kryptonButton2.Tag = "CreateImportFile";
            this.kryptonButton2.Values.Text = "CreateImportFile";
            this.kryptonButton2.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.AutoSize = true;
            this.kryptonButton4.Location = new System.Drawing.Point(218, 3);
            this.kryptonButton4.MinimumSize = new System.Drawing.Size(78, 25);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton4.Size = new System.Drawing.Size(78, 25);
            this.kryptonButton4.TabIndex = 61;
            this.kryptonButton4.Tag = "Cancel";
            this.kryptonButton4.Values.Text = "Cancel";
            this.kryptonButton4.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ImportDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(712, 487);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.adminLevelMultiselect1);
            this.Name = "ImportDownload";
            this.Tag = "Import";
            this.Text = "Import";
            this.Load += new System.EventHandler(this.ImportDemographyModal_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private AdminLevelMultiselect adminLevelMultiselect1;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton3;
    }
}
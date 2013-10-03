namespace Nada.UI.View
{
    partial class ReportIndicatorControl
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
            this.tblIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.loading1 = new Nada.UI.Controls.Loading();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblIndicators
            // 
            this.tblIndicators.AutoSize = true;
            this.tblIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblIndicators.ColumnCount = 7;
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.Location = new System.Drawing.Point(3, 68);
            this.tblIndicators.Name = "tblIndicators";
            this.tblIndicators.RowCount = 1;
            this.tblIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIndicators.Size = new System.Drawing.Size(30, 0);
            this.tblIndicators.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.loading1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tblIndicators, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(78, 71);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // loading1
            // 
            this.loading1.AutoSize = true;
            this.loading1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loading1.Location = new System.Drawing.Point(3, 3);
            this.loading1.Name = "loading1";
            this.loading1.Size = new System.Drawing.Size(72, 59);
            this.loading1.TabIndex = 1;
            // 
            // ReportIndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ReportIndicatorControl";
            this.Size = new System.Drawing.Size(84, 77);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblIndicators;
        private Controls.Loading loading1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}

namespace Nada.UI.Controls
{
    partial class StatCalculator
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hr4 = new Nada.UI.Controls.HR();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.tblIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.lblCalculating = new Nada.UI.Controls.H3bLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.hr4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tblIndicators, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCalculating, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(919, 75);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.DimGray;
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 12);
            this.hr4.Margin = new System.Windows.Forms.Padding(0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.DimGray;
            this.hr4.Size = new System.Drawing.Size(919, 1);
            this.hr4.TabIndex = 48;
            this.hr4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 27);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lblCustomIndicators, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.fieldLink1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(223, 21);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // lblCustomIndicators
            // 
            this.lblCustomIndicators.AutoSize = true;
            this.lblCustomIndicators.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomIndicators.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomIndicators.Location = new System.Drawing.Point(0, 0);
            this.lblCustomIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustomIndicators.Name = "lblCustomIndicators";
            this.lblCustomIndicators.Size = new System.Drawing.Size(159, 21);
            this.lblCustomIndicators.TabIndex = 19;
            this.lblCustomIndicators.Tag = "CalculatedStatistics";
            this.lblCustomIndicators.Text = "Calculated Statistics:";
            // 
            // fieldLink1
            // 
            this.fieldLink1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(159, 5);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(64, 16);
            this.fieldLink1.TabIndex = 20;
            this.fieldLink1.Tag = "Calculate";
            this.fieldLink1.Text = "Calculate";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // tblIndicators
            // 
            this.tblIndicators.AutoSize = true;
            this.tblIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblIndicators.ColumnCount = 3;
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.Location = new System.Drawing.Point(3, 52);
            this.tblIndicators.Name = "tblIndicators";
            this.tblIndicators.RowCount = 1;
            this.tblIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIndicators.Size = new System.Drawing.Size(23, 0);
            this.tblIndicators.TabIndex = 0;
            // 
            // lblCalculating
            // 
            this.lblCalculating.AutoSize = true;
            this.lblCalculating.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblCalculating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCalculating.Location = new System.Drawing.Point(0, 55);
            this.lblCalculating.Margin = new System.Windows.Forms.Padding(0);
            this.lblCalculating.Name = "lblCalculating";
            this.lblCalculating.Size = new System.Drawing.Size(119, 16);
            this.lblCalculating.TabIndex = 49;
            this.lblCalculating.Tag = "CalculatingStatistics";
            this.lblCalculating.Text = "CalculatingStatistics";
            this.lblCalculating.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // StatCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StatCalculator";
            this.Size = new System.Drawing.Size(919, 75);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HR hr4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.TableLayoutPanel tblIndicators;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private FieldLink fieldLink1;
        private H3bLabel lblCalculating;
    }
}

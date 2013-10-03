namespace Nada.UI.View.Reports.CustomReport
{
    partial class StepOptions
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
            this.diseasesControl1 = new Nada.UI.Controls.DiseasesControl();
            this.lbYears = new System.Windows.Forms.ListBox();
            this.h3Required2 = new Nada.UI.Controls.H3Required();
            this.lblDiseases = new Nada.UI.Controls.H3Required();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.cbAggregateBy = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblDiseases, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.h3Required2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.diseasesControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbYears, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(414, 135);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // diseasesControl1
            // 
            this.diseasesControl1.AutoSize = true;
            this.diseasesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diseasesControl1.Location = new System.Drawing.Point(207, 19);
            this.diseasesControl1.Name = "diseasesControl1";
            this.diseasesControl1.Size = new System.Drawing.Size(204, 113);
            this.diseasesControl1.TabIndex = 0;
            // 
            // lbYears
            // 
            this.lbYears.DisplayMember = "DisplayName";
            this.lbYears.FormattingEnabled = true;
            this.lbYears.Items.AddRange(new object[] {
            "2013",
            "2012",
            "2011",
            "2010",
            "2009",
            "2008",
            "2007",
            "2006",
            "2005",
            "2004",
            "2003",
            "2002",
            "2001",
            "2000",
            "1999",
            "1998",
            "1997",
            "1996",
            "1995",
            "1994",
            "1993",
            "1992",
            "1991",
            "1990"});
            this.lbYears.Location = new System.Drawing.Point(3, 19);
            this.lbYears.Name = "lbYears";
            this.lbYears.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbYears.Size = new System.Drawing.Size(198, 95);
            this.lbYears.TabIndex = 5;
            // 
            // h3Required2
            // 
            this.h3Required2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Required2.AutoSize = true;
            this.h3Required2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required2.Location = new System.Drawing.Point(0, 0);
            this.h3Required2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required2.Name = "h3Required2";
            this.h3Required2.Size = new System.Drawing.Size(57, 16);
            this.h3Required2.TabIndex = 6;
            this.h3Required2.TabStop = false;
            this.h3Required2.Tag = "Years";
            this.h3Required2.Text = "Years";
            this.h3Required2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lblDiseases
            // 
            this.lblDiseases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiseases.AutoSize = true;
            this.lblDiseases.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblDiseases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDiseases.Location = new System.Drawing.Point(204, 0);
            this.lblDiseases.Margin = new System.Windows.Forms.Padding(0);
            this.lblDiseases.Name = "lblDiseases";
            this.lblDiseases.Size = new System.Drawing.Size(79, 16);
            this.lblDiseases.TabIndex = 7;
            this.lblDiseases.TabStop = false;
            this.lblDiseases.Tag = "Diseases";
            this.lblDiseases.Text = "Diseases";
            this.lblDiseases.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cbAggregateBy, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.h3Label1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(420, 184);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 141);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(96, 16);
            this.h3Label1.TabIndex = 1;
            this.h3Label1.Tag = "AggregatedBy";
            this.h3Label1.Text = "AggregatedBy";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbAggregateBy
            // 
            this.cbAggregateBy.Enabled = false;
            this.cbAggregateBy.FormattingEnabled = true;
            this.cbAggregateBy.Items.AddRange(new object[] {
            "Not aggregated"});
            this.cbAggregateBy.Location = new System.Drawing.Point(3, 160);
            this.cbAggregateBy.Name = "cbAggregateBy";
            this.cbAggregateBy.Size = new System.Drawing.Size(180, 21);
            this.cbAggregateBy.TabIndex = 2;
            // 
            // StepOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "StepOptions";
            this.Size = new System.Drawing.Size(426, 190);
            this.Load += new System.EventHandler(this.StepOptions_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.DiseasesControl diseasesControl1;
        private System.Windows.Forms.ListBox lbYears;
        private Controls.H3Required lblDiseases;
        private Controls.H3Required h3Required2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.ComboBox cbAggregateBy;
    }
}

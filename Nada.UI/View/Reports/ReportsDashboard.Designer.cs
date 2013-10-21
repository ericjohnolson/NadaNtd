namespace Nada.UI.View.Reports
{
    partial class ReportsDashboard
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
            this.label21 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDash = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lnkCustom = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkTreatment = new System.Windows.Forms.LinkLabel();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.hrDisease = new Nada.UI.Controls.HR();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.label21.Location = new System.Drawing.Point(11, 12);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 30);
            this.label21.TabIndex = 58;
            this.label21.Tag = "Reports";
            this.label21.Text = "Reports";
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(3, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 61;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDash
            // 
            this.btnDash.AutoSize = true;
            this.btnDash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDash.FlatAppearance.BorderSize = 0;
            this.btnDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDash.Image = global::Nada.UI.Properties.Resources.button_dashboard;
            this.btnDash.Location = new System.Drawing.Point(55, 3);
            this.btnDash.Name = "btnDash";
            this.btnDash.Size = new System.Drawing.Size(46, 46);
            this.btnDash.TabIndex = 60;
            this.btnDash.UseVisualStyleBackColor = true;
            this.btnDash.Click += new System.EventHandler(this.btnDash_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(12, 61);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 21);
            this.label4.TabIndex = 62;
            this.label4.Tag = "AvailableReports";
            this.label4.Text = "Available reports";
            // 
            // lnkCustom
            // 
            this.lnkCustom.ActiveLinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkCustom.AutoSize = true;
            this.lnkCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCustom.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkCustom.LinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkCustom.Location = new System.Drawing.Point(94, 0);
            this.lnkCustom.Margin = new System.Windows.Forms.Padding(0);
            this.lnkCustom.Name = "lnkCustom";
            this.lnkCustom.Size = new System.Drawing.Size(50, 16);
            this.lnkCustom.TabIndex = 64;
            this.lnkCustom.TabStop = true;
            this.lnkCustom.Tag = "NewLink";
            this.lnkCustom.Text = "Open...";
            this.lnkCustom.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkCustom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCustom_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lnkCustom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.h3Label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(144, 16);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(94, 16);
            this.h3Label1.TabIndex = 0;
            this.h3Label1.Tag = "CustomReport";
            this.h3Label1.Text = "CustomReport";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lnkTreatment, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.h3Label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 129);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 16);
            this.tableLayoutPanel2.TabIndex = 66;
            // 
            // lnkTreatment
            // 
            this.lnkTreatment.ActiveLinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkTreatment.AutoSize = true;
            this.lnkTreatment.Enabled = false;
            this.lnkTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTreatment.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkTreatment.LinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkTreatment.Location = new System.Drawing.Point(110, 0);
            this.lnkTreatment.Margin = new System.Windows.Forms.Padding(0);
            this.lnkTreatment.Name = "lnkTreatment";
            this.lnkTreatment.Size = new System.Drawing.Size(50, 16);
            this.lnkTreatment.TabIndex = 64;
            this.lnkTreatment.TabStop = true;
            this.lnkTreatment.Tag = "NewLink";
            this.lnkTreatment.Text = "Open...";
            this.lnkTreatment.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(0, 0);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(110, 16);
            this.h3Label2.TabIndex = 0;
            this.h3Label2.Tag = "TreatmentReport";
            this.h3Label2.Text = "TreatmentReport";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // hrDisease
            // 
            this.hrDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrDisease.ForeColor = System.Drawing.Color.Gray;
            this.hrDisease.Location = new System.Drawing.Point(16, 90);
            this.hrDisease.Margin = new System.Windows.Forms.Padding(0);
            this.hrDisease.Name = "hrDisease";
            this.hrDisease.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrDisease.Size = new System.Drawing.Size(775, 1);
            this.hrDisease.TabIndex = 63;
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(5);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrTop.Size = new System.Drawing.Size(822, 5);
            this.hrTop.TabIndex = 57;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.btnHelp, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDash, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(694, 5);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(104, 52);
            this.tableLayoutPanel3.TabIndex = 67;
            // 
            // ReportsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.hrDisease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hrTop);
            this.Name = "ReportsDashboard";
            this.Size = new System.Drawing.Size(822, 704);
            this.Load += new System.EventHandler(this.ReportsDashboard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.HR hrTop;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnDash;
        private Controls.HR hrDisease;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkCustom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.LinkLabel lnkTreatment;
        private Controls.H3Label h3Label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}

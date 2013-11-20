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
            this.label4 = new System.Windows.Forms.Label();
            this.lnkCustom = new System.Windows.Forms.LinkLabel();
            this.tblCustom = new System.Windows.Forms.TableLayoutPanel();
            this.lblCustom = new Nada.UI.Controls.H3Label();
            this.hrDisease = new Nada.UI.Controls.HR();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDash = new System.Windows.Forms.Button();
            this.hr1 = new Nada.UI.Controls.HR();
            this.label1 = new System.Windows.Forms.Label();
            this.tblCmJrf = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblCmJrf = new Nada.UI.Controls.H3Label();
            this.tblCustom.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tblCmJrf.SuspendLayout();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label21.Location = new System.Drawing.Point(11, 12);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 30);
            this.label21.TabIndex = 58;
            this.label21.Tag = "Reports";
            this.label21.Text = "Reports";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
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
            // tblCustom
            // 
            this.tblCustom.AutoSize = true;
            this.tblCustom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblCustom.ColumnCount = 2;
            this.tblCustom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCustom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCustom.Controls.Add(this.lnkCustom, 1, 0);
            this.tblCustom.Controls.Add(this.lblCustom, 0, 0);
            this.tblCustom.Location = new System.Drawing.Point(16, 107);
            this.tblCustom.Name = "tblCustom";
            this.tblCustom.RowCount = 1;
            this.tblCustom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblCustom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblCustom.Size = new System.Drawing.Size(144, 16);
            this.tblCustom.TabIndex = 65;
            // 
            // lblCustom
            // 
            this.lblCustom.AutoSize = true;
            this.lblCustom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblCustom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCustom.Location = new System.Drawing.Point(0, 0);
            this.lblCustom.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustom.Name = "lblCustom";
            this.lblCustom.Size = new System.Drawing.Size(94, 16);
            this.lblCustom.TabIndex = 0;
            this.lblCustom.Tag = "CustomReport";
            this.lblCustom.Text = "CustomReport";
            this.lblCustom.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // hrDisease
            // 
            this.hrDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrDisease.ForeColor = System.Drawing.Color.Gray;
            this.hrDisease.Location = new System.Drawing.Point(16, 90);
            this.hrDisease.Margin = new System.Windows.Forms.Padding(0);
            this.hrDisease.Name = "hrDisease";
            this.hrDisease.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrDisease.Size = new System.Drawing.Size(775, 1);
            this.hrDisease.TabIndex = 63;
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(5);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
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
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(16, 195);
            this.hr1.Margin = new System.Windows.Forms.Padding(0);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(775, 1);
            this.hr1.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label1.Location = new System.Drawing.Point(12, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 21);
            this.label1.TabIndex = 68;
            this.label1.Tag = "AvailableExports";
            this.label1.Text = "Available exports";
            // 
            // tblCmJrf
            // 
            this.tblCmJrf.AutoSize = true;
            this.tblCmJrf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblCmJrf.ColumnCount = 2;
            this.tblCmJrf.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCmJrf.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCmJrf.Controls.Add(this.linkLabel1, 1, 0);
            this.tblCmJrf.Controls.Add(this.lblCmJrf, 0, 0);
            this.tblCmJrf.Location = new System.Drawing.Point(16, 209);
            this.tblCmJrf.Name = "tblCmJrf";
            this.tblCmJrf.RowCount = 1;
            this.tblCmJrf.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblCmJrf.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblCmJrf.Size = new System.Drawing.Size(120, 16);
            this.tblCmJrf.TabIndex = 70;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel1.Location = new System.Drawing.Point(70, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(50, 16);
            this.linkLabel1.TabIndex = 64;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "NewLink";
            this.linkLabel1.Text = "Open...";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCmJrf_LinkClicked);
            // 
            // lblCmJrf
            // 
            this.lblCmJrf.AutoSize = true;
            this.lblCmJrf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblCmJrf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCmJrf.Location = new System.Drawing.Point(0, 0);
            this.lblCmJrf.Margin = new System.Windows.Forms.Padding(0);
            this.lblCmJrf.Name = "lblCmJrf";
            this.lblCmJrf.Size = new System.Drawing.Size(70, 16);
            this.lblCmJrf.TabIndex = 0;
            this.lblCmJrf.Tag = "JrfCmNtds";
            this.lblCmJrf.Text = "JrfCmNtds";
            this.lblCmJrf.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // ReportsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblCmJrf);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tblCustom);
            this.Controls.Add(this.hrDisease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hrTop);
            this.Name = "ReportsDashboard";
            this.Size = new System.Drawing.Size(822, 704);
            this.Load += new System.EventHandler(this.ReportsDashboard_Load);
            this.tblCustom.ResumeLayout(false);
            this.tblCustom.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tblCmJrf.ResumeLayout(false);
            this.tblCmJrf.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tblCustom;
        private Controls.H3Label lblCustom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Controls.HR hr1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblCmJrf;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Controls.H3Label lblCmJrf;
    }
}

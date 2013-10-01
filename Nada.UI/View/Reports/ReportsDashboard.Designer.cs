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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.hrDisease = new Nada.UI.Controls.HR();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.btnHelp.Location = new System.Drawing.Point(691, 8);
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
            this.btnDash.Location = new System.Drawing.Point(743, 8);
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
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel1.Location = new System.Drawing.Point(94, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(50, 16);
            this.linkLabel1.TabIndex = 64;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "NewLink";
            this.linkLabel1.Text = "Open...";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.h3Label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 109);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(144, 16);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.linkLabel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.h3Label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 131);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 16);
            this.tableLayoutPanel2.TabIndex = 66;
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Enabled = false;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel2.Location = new System.Drawing.Point(110, 0);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(50, 16);
            this.linkLabel2.TabIndex = 64;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "NewLink";
            this.linkLabel2.Text = "Open...";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
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
            // hrDisease
            // 
            this.hrDisease.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hrDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrDisease.ForeColor = System.Drawing.Color.Gray;
            this.hrDisease.Location = new System.Drawing.Point(16, 91);
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
            // ReportsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.hrDisease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDash);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hrTop);
            this.Name = "ReportsDashboard";
            this.Size = new System.Drawing.Size(822, 704);
            this.Load += new System.EventHandler(this.ReportsDashboard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private Controls.H3Label h3Label2;
    }
}

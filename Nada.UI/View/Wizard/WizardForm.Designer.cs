namespace Nada.UI.View
{
    partial class WizardForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hrDisease = new Nada.UI.Controls.HR();
            this.lblStepTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.hrTop = new Nada.UI.Controls.HR();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrev = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnFinish = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnNext = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlContent, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(713, 537);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.hrDisease);
            this.panel1.Controls.Add(this.lblStepTitle);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.hrTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 82);
            this.panel1.TabIndex = 0;
            // 
            // hrDisease
            // 
            this.hrDisease.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hrDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrDisease.ForeColor = System.Drawing.Color.Gray;
            this.hrDisease.Location = new System.Drawing.Point(21, 79);
            this.hrDisease.Margin = new System.Windows.Forms.Padding(0);
            this.hrDisease.Name = "hrDisease";
            this.hrDisease.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.hrDisease.Size = new System.Drawing.Size(668, 1);
            this.hrDisease.TabIndex = 67;
            // 
            // lblStepTitle
            // 
            this.lblStepTitle.AutoSize = true;
            this.lblStepTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblStepTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStepTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.lblStepTitle.Location = new System.Drawing.Point(19, 49);
            this.lblStepTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblStepTitle.Name = "lblStepTitle";
            this.lblStepTitle.Size = new System.Drawing.Size(76, 21);
            this.lblStepTitle.TabIndex = 66;
            this.lblStepTitle.Tag = "";
            this.lblStepTitle.Text = "StepTitle";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(178)))), ((int)(((byte)(224)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 60;
            this.lblTitle.Tag = "CustomReportBuilder";
            this.lblTitle.Text = "CustomReportBuilder";
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
            this.hrTop.Size = new System.Drawing.Size(713, 5);
            this.hrTop.TabIndex = 59;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(20, 97);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(20, 15, 3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(690, 383);
            this.pnlContent.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.btnPrev, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnFinish, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnNext, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(417, 486);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 3, 20, 20);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(276, 31);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnPrev
            // 
            this.btnPrev.AutoSize = true;
            this.btnPrev.Location = new System.Drawing.Point(3, 3);
            this.btnPrev.MinimumSize = new System.Drawing.Size(78, 25);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnPrev.Size = new System.Drawing.Size(78, 25);
            this.btnPrev.TabIndex = 61;
            this.btnPrev.Tag = "Previous";
            this.btnPrev.Values.Text = "Previous";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.AutoSize = true;
            this.btnFinish.Location = new System.Drawing.Point(195, 3);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.btnFinish.MinimumSize = new System.Drawing.Size(78, 25);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnFinish.Size = new System.Drawing.Size(78, 25);
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Tag = "Finish";
            this.btnFinish.Values.Text = "Finish";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnNext
            // 
            this.btnNext.AutoSize = true;
            this.btnNext.Location = new System.Drawing.Point(99, 3);
            this.btnNext.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.btnNext.MinimumSize = new System.Drawing.Size(78, 25);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnNext.Size = new System.Drawing.Size(78, 25);
            this.btnNext.TabIndex = 61;
            this.btnNext.Tag = "Next";
            this.btnNext.Values.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // WizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(713, 537);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WizardForm";
            this.Tag = "CustomReport";
            this.Text = "CustomReport";
            this.Load += new System.EventHandler(this.ReportWizard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private Controls.HR hrTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPrev;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFinish;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnNext;
        private Controls.HR hrDisease;
        private System.Windows.Forms.Label lblStepTitle;
        private System.Windows.Forms.Panel pnlContent;
    }
}
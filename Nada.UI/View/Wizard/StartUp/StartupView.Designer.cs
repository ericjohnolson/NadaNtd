namespace Nada.UI.View
{
    partial class StartupView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tblTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblTasks = new System.Windows.Forms.TableLayoutPanel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tblTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnHelp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1050, 559);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(100, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(100, 3, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 67);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.tblTitle);
            this.panel2.Controls.Add(this.tblTasks);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 61);
            this.panel2.TabIndex = 63;
            // 
            // tblTitle
            // 
            this.tblTitle.AutoSize = true;
            this.tblTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTitle.ColumnCount = 2;
            this.tblTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTitle.Controls.Add(this.lblTitle, 1, 0);
            this.tblTitle.Location = new System.Drawing.Point(0, 3);
            this.tblTitle.Margin = new System.Windows.Forms.Padding(0, 3, 3, 20);
            this.tblTitle.Name = "tblTitle";
            this.tblTitle.RowCount = 1;
            this.tblTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitle.Size = new System.Drawing.Size(177, 32);
            this.tblTitle.TabIndex = 61;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(177, 32);
            this.lblTitle.TabIndex = 62;
            this.lblTitle.Tag = "GettingStarted";
            this.lblTitle.Text = "GettingStarted";
            // 
            // tblTasks
            // 
            this.tblTasks.AutoSize = true;
            this.tblTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTasks.ColumnCount = 3;
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblTasks.Location = new System.Drawing.Point(3, 58);
            this.tblTasks.Name = "tblTasks";
            this.tblTasks.RowCount = 1;
            this.tblTasks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblTasks.Size = new System.Drawing.Size(23, 0);
            this.tblTasks.TabIndex = 68;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(979, 3);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 44);
            this.btnHelp.TabIndex = 62;
            this.btnHelp.TabStop = false;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(6);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Size = new System.Drawing.Size(1050, 6);
            this.hrTop.TabIndex = 61;
            this.hrTop.TabStop = false;
            // 
            // StartupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.hrTop);
            this.Name = "StartupView";
            this.Size = new System.Drawing.Size(1050, 565);
            this.Load += new System.EventHandler(this.StartupView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tblTitle.ResumeLayout(false);
            this.tblTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.HR hrTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tblTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblTasks;
        private System.Windows.Forms.Button btnHelp;
    }
}

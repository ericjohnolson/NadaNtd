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
            this.label21 = new System.Windows.Forms.Label();
            this.tblTasks = new System.Windows.Forms.TableLayoutPanel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.hrTop = new Nada.UI.Controls.HR();
            this.hr1 = new Nada.UI.Controls.HR();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label21.Location = new System.Drawing.Point(9, 13);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 30);
            this.label21.TabIndex = 60;
            this.label21.Tag = "GettingStarted";
            this.label21.Text = "GettingStarted";
            // 
            // tblTasks
            // 
            this.tblTasks.AutoSize = true;
            this.tblTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTasks.ColumnCount = 3;
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTasks.Location = new System.Drawing.Point(14, 69);
            this.tblTasks.Name = "tblTasks";
            this.tblTasks.RowCount = 1;
            this.tblTasks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblTasks.Size = new System.Drawing.Size(20, 0);
            this.tblTasks.TabIndex = 68;
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(759, 6);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 62;
            this.btnHelp.TabStop = false;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(5);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Size = new System.Drawing.Size(900, 5);
            this.hrTop.TabIndex = 61;
            this.hrTop.TabStop = false;
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(5, 60);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(800, 1);
            this.hr1.TabIndex = 19;
            // 
            // StartupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblTasks);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hrTop);
            this.Controls.Add(this.hr1);
            this.Name = "StartupView";
            this.Size = new System.Drawing.Size(900, 490);
            this.Load += new System.EventHandler(this.StartupView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.HR hr1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label21;
        private Controls.HR hrTop;
        private System.Windows.Forms.TableLayoutPanel tblTasks;
    }
}

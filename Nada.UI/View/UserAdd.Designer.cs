namespace Nada.UI.View
{
    partial class UserAdd
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblPwd = new Nada.UI.Controls.H3Required();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.rolePickerControl1 = new Nada.UI.View.RolePickerControl();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkChangePassword = new Nada.UI.Controls.H3Link();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 459);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(599, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(82, 17);
            this.lblLastUpdated.Text = "Last Updated: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label3.Location = new System.Drawing.Point(24, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 28);
            this.label3.TabIndex = 24;
            this.label3.Tag = "User";
            this.label3.Text = "User";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(6);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(599, 6);
            this.hr1.TabIndex = 23;
            // 
            // tbUsername
            // 
            this.tbUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Username", true));
            this.tbUsername.Location = new System.Drawing.Point(3, 18);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(209, 21);
            this.tbUsername.TabIndex = 25;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Model.Member);
            // 
            // h3Required1
            // 
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(0, 0);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(78, 15);
            this.h3Required1.TabIndex = 43;
            this.h3Required1.Tag = "Username";
            this.h3Required1.Text = "Username";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSource1;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPwd.Location = new System.Drawing.Point(242, 0);
            this.lblPwd.Margin = new System.Windows.Forms.Padding(0);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(75, 15);
            this.lblPwd.TabIndex = 45;
            this.lblPwd.Tag = "Password";
            this.lblPwd.Text = "Password";
            this.lblPwd.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbPassword
            // 
            this.tbPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Password", true));
            this.tbPassword.Location = new System.Drawing.Point(245, 18);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '•';
            this.tbPassword.Size = new System.Drawing.Size(209, 21);
            this.tbPassword.TabIndex = 44;
            // 
            // rolePickerControl1
            // 
            this.rolePickerControl1.AutoSize = true;
            this.rolePickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rolePickerControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolePickerControl1.Location = new System.Drawing.Point(30, 133);
            this.rolePickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.rolePickerControl1.Name = "rolePickerControl1";
            this.rolePickerControl1.Size = new System.Drawing.Size(542, 236);
            this.rolePickerControl1.TabIndex = 46;
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(29, 117);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(40, 16);
            this.h3Label1.TabIndex = 47;
            this.h3Label1.Tag = "Roles";
            this.h3Label1.Text = "Roles";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.h3Required1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbUsername, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPwd, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lnkChangePassword, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 42);
            this.tableLayoutPanel1.TabIndex = 48;
            // 
            // lnkChangePassword
            // 
            this.lnkChangePassword.AutoSize = true;
            this.lnkChangePassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkChangePassword.BackColor = System.Drawing.Color.Transparent;
            this.lnkChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChangePassword.Location = new System.Drawing.Point(484, 21);
            this.lnkChangePassword.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lnkChangePassword.Name = "lnkChangePassword";
            this.lnkChangePassword.Size = new System.Drawing.Size(107, 15);
            this.lnkChangePassword.TabIndex = 46;
            this.lnkChangePassword.Tag = "ChangePassword";
            this.lnkChangePassword.Text = "ChangePassword";
            this.lnkChangePassword.ClickOverride += new System.Action(this.lnkChangePassword_ClickOverride);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.Controls.Add(this.c1Button1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.c1Button2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(30, 400);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(204, 33);
            this.tableLayoutPanel2.TabIndex = 50;
            // 
            // c1Button1
            // 
            this.c1Button1.AutoSize = true;
            this.c1Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button1.Location = new System.Drawing.Point(111, 3);
            this.c1Button1.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button1.Size = new System.Drawing.Size(90, 27);
            this.c1Button1.TabIndex = 3;
            this.c1Button1.Tag = "Cancel";
            this.c1Button1.Text = "Cancel";
            this.c1Button1.UseVisualStyleBackColor = true;
            this.c1Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // c1Button2
            // 
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.Location = new System.Drawing.Point(3, 3);
            this.c1Button2.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button2.Size = new System.Drawing.Size(90, 27);
            this.c1Button2.TabIndex = 4;
            this.c1Button2.Tag = "Save";
            this.c1Button2.Text = "Save";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button2.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // UserAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(599, 481);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.rolePickerControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "UserAdd";
            this.Tag = "User";
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserAdd_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastUpdated;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Controls.H3Required lblPwd;
        private System.Windows.Forms.TextBox tbPassword;
        private Controls.H3Label h3Label1;
        private RolePickerControl rolePickerControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Link lnkChangePassword;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private C1.Win.C1Input.C1Button c1Button1;
        private C1.Win.C1Input.C1Button c1Button2;
    }
}
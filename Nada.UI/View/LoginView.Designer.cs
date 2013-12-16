namespace Nada.UI.View
{
    partial class LoginView
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.btnAddNew = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbLanguages = new System.Windows.Forms.ComboBox();
            this.bsLanguages = new System.Windows.Forms.BindingSource(this.components);
            this.tbUid = new System.Windows.Forms.TextBox();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 291F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1080, 726);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.h3Label3);
            this.panel1.Controls.Add(this.h3Label2);
            this.panel1.Controls.Add(this.h3Label1);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.cbLanguages);
            this.panel1.Controls.Add(this.tbUid);
            this.panel1.Controls.Add(this.tbPwd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(295, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 285);
            this.panel1.TabIndex = 0;
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(71, 158);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(63, 18);
            this.h3Label3.TabIndex = 10;
            this.h3Label3.Tag = "Language";
            this.h3Label3.Text = "Language";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(71, 110);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(63, 18);
            this.h3Label2.TabIndex = 9;
            this.h3Label2.Tag = "Password";
            this.h3Label2.Text = "Password";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(71, 61);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(66, 18);
            this.h3Label1.TabIndex = 8;
            this.h3Label1.Tag = "Username";
            this.h3Label1.Text = "Username";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // btnAddNew
            // 
            this.btnAddNew.AutoSize = true;
            this.btnAddNew.Location = new System.Drawing.Point(71, 223);
            this.btnAddNew.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(91, 29);
            this.btnAddNew.TabIndex = 7;
            this.btnAddNew.Tag = "SignIn";
            this.btnAddNew.Values.Text = "Sign In";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // cbLanguages
            // 
            this.cbLanguages.DataSource = this.bsLanguages;
            this.cbLanguages.DisplayMember = "Name";
            this.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguages.FormattingEnabled = true;
            this.cbLanguages.Location = new System.Drawing.Point(71, 180);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.Size = new System.Drawing.Size(206, 23);
            this.cbLanguages.TabIndex = 2;
            this.cbLanguages.ValueMember = "IsoCode";
            this.cbLanguages.SelectedIndexChanged += new System.EventHandler(this.cbLanguages_SelectedIndexChanged);
            // 
            // bsLanguages
            // 
            this.bsLanguages.DataSource = typeof(Nada.Model.Language);
            // 
            // tbUid
            // 
            this.tbUid.Location = new System.Drawing.Point(71, 83);
            this.tbUid.Name = "tbUid";
            this.tbUid.Size = new System.Drawing.Size(206, 21);
            this.tbUid.TabIndex = 0;
            this.tbUid.Text = "admin";
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(71, 132);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.PasswordChar = '*';
            this.tbPwd.Size = new System.Drawing.Size(206, 21);
            this.tbPwd.TabIndex = 1;
            this.tbPwd.Text = "@ntd1one!";
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoginView";
            this.Size = new System.Drawing.Size(1080, 726);
            this.Load += new System.EventHandler(this.LoginView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbUid;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.ComboBox cbLanguages;
        private System.Windows.Forms.BindingSource bsLanguages;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private Controls.H3Label h3Label3;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label1;
    }
}

﻿namespace Nada.UI.View.Demography
{
    partial class AdminLevelView
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
            this.label3 = new System.Windows.Forms.Label();
            this.bsAdminLevel = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlLf = new System.Windows.Forms.Panel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hr4 = new Nada.UI.Controls.HR();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAdminLevel, "Name", true));
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 30);
            this.label3.TabIndex = 58;
            this.label3.Tag = "";
            this.label3.Text = "Admin Level Name";
            // 
            // bsAdminLevel
            // 
            this.bsAdminLevel.DataSource = typeof(Nada.Model.AdminLevel);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(15, 55);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(15, 10, 3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(482, 444);
            this.tabControl1.TabIndex = 59;
            this.tabControl1.Tag = "Leporsy";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.pnlLf);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(474, 418);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "LF";
            this.tabPage2.Text = "LF";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlLf
            // 
            this.pnlLf.AutoScroll = true;
            this.pnlLf.AutoSize = true;
            this.pnlLf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLf.Location = new System.Drawing.Point(3, 3);
            this.pnlLf.Name = "pnlLf";
            this.pnlLf.Size = new System.Drawing.Size(0, 0);
            this.pnlLf.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(474, 418);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Tag = "Leprosy";
            this.tabPage1.Text = "Leprosy";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.Controls.Add(this.hr4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.TabIndex = 60;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr4.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 0);
            this.hr4.Margin = new System.Windows.Forms.Padding(0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr4.Size = new System.Drawing.Size(500, 5);
            this.hr4.TabIndex = 57;
            // 
            // AdminLevelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "AdminLevelView";
            this.Size = new System.Drawing.Size(500, 500);
            this.Load += new System.EventHandler(this.AdminLevelView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsAdminLevel;
        private Controls.HR hr4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlLf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

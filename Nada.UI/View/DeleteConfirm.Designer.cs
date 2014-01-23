namespace Nada.UI.View
{
    partial class DeleteConfirm
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
            this.hr1 = new Nada.UI.Controls.HR();
            this.label3 = new System.Windows.Forms.Label();
            this.bsIndicator = new System.Windows.Forms.BindingSource(this.components);
            this.h3Label1 = new Nada.UI.Controls.H3bLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsIndicator)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this.hr1.Size = new System.Drawing.Size(274, 6);
            this.hr1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label3.Location = new System.Drawing.Point(26, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 28);
            this.label3.TabIndex = 14;
            this.label3.Tag = "Delete";
            this.label3.Text = "Delete";
            // 
            // bsIndicator
            // 
            this.bsIndicator.DataSource = typeof(Nada.Model.Indicator);
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(31, 50);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(183, 16);
            this.h3Label1.TabIndex = 15;
            this.h3Label1.Tag = "DeleteConfirmMessage";
            this.h3Label1.Text = "Are you sure you want to delete?";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(31, 118);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(25, 3, 25, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(204, 33);
            this.tableLayoutPanel2.TabIndex = 51;
            // 
            // c1Button1
            // 
            this.c1Button1.AutoSize = true;
            this.c1Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.c1Button1.Location = new System.Drawing.Point(111, 3);
            this.c1Button1.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button1.Size = new System.Drawing.Size(90, 27);
            this.c1Button1.TabIndex = 3;
            this.c1Button1.Tag = "No";
            this.c1Button1.Text = "No";
            this.c1Button1.UseVisualStyleBackColor = true;
            this.c1Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // c1Button2
            // 
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.c1Button2.Location = new System.Drawing.Point(3, 3);
            this.c1Button2.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button2.Size = new System.Drawing.Size(90, 27);
            this.c1Button2.TabIndex = 4;
            this.c1Button2.Tag = "Yes";
            this.c1Button2.Text = "Yes";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // DeleteConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(274, 174);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Name = "DeleteConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "Confirm";
            this.Text = "Confirm";
            this.Load += new System.EventHandler(this.DeleteConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsIndicator)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsIndicator;
        private Controls.HR hr1;
        private System.Windows.Forms.Label label3;
        private Controls.H3bLabel h3Label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private C1.Win.C1Input.C1Button c1Button1;
        private C1.Win.C1Input.C1Button c1Button2;
    }
}
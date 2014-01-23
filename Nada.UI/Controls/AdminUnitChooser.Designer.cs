namespace Nada.UI.View
{
    partial class AdminUnitChooser
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.h3Required1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.fieldLink1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(169, 19);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // h3Required1
            // 
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(2, 2);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(2);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(110, 15);
            this.h3Required1.TabIndex = 19;
            this.h3Required1.Tag = "ChooseLocation";
            this.h3Required1.Text = "ChooseLocation";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(114, 1);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(55, 16);
            this.fieldLink1.TabIndex = 18;
            this.fieldLink1.Tag = "LinkSelect";
            this.fieldLink1.Text = "Select...";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // AdminUnitChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AdminUnitChooser";
            this.Size = new System.Drawing.Size(175, 25);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.FieldLink fieldLink1;
        private Controls.H3Required h3Required1;
    }
}

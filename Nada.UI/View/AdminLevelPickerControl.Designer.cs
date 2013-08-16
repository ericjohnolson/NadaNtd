namespace Nada.UI.View
{
    partial class AdminLevelPickerControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkChooseAdminLevel = new System.Windows.Forms.LinkLabel();
            this.lblAdminLevel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Location";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lnkChooseAdminLevel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAdminLevel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(114, 18);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // lnkChooseAdminLevel
            // 
            this.lnkChooseAdminLevel.AutoSize = true;
            this.lnkChooseAdminLevel.Location = new System.Drawing.Point(59, 0);
            this.lnkChooseAdminLevel.Name = "lnkChooseAdminLevel";
            this.lnkChooseAdminLevel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lnkChooseAdminLevel.Size = new System.Drawing.Size(52, 18);
            this.lnkChooseAdminLevel.TabIndex = 8;
            this.lnkChooseAdminLevel.TabStop = true;
            this.lnkChooseAdminLevel.Text = "Choose...";
            this.lnkChooseAdminLevel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChooseAdminLevel_LinkClicked);
            // 
            // lblAdminLevel
            // 
            this.lblAdminLevel.AutoSize = true;
            this.lblAdminLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminLevel.Location = new System.Drawing.Point(3, 0);
            this.lblAdminLevel.Name = "lblAdminLevel";
            this.lblAdminLevel.Size = new System.Drawing.Size(50, 13);
            this.lblAdminLevel.TabIndex = 4;
            this.lblAdminLevel.Text = "Not Set";
            // 
            // AdminLevelPickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "AdminLevelPickerControl";
            this.Size = new System.Drawing.Size(123, 37);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.LinkLabel lnkChooseAdminLevel;
        private System.Windows.Forms.Label lblAdminLevel;
    }
}

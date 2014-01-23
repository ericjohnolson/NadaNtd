namespace Nada.UI.View.Wizard
{
    partial class ImportStepLists
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
            this.tblMetaData = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // tblMetaData
            // 
            this.tblMetaData.AutoSize = true;
            this.tblMetaData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblMetaData.ColumnCount = 2;
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMetaData.Location = new System.Drawing.Point(3, 3);
            this.tblMetaData.Margin = new System.Windows.Forms.Padding(3, 3, 20, 20);
            this.tblMetaData.Name = "tblMetaData";
            this.tblMetaData.RowCount = 1;
            this.tblMetaData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMetaData.Size = new System.Drawing.Size(20, 0);
            this.tblMetaData.TabIndex = 52;
            this.tblMetaData.Visible = false;
            // 
            // ImportStepLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblMetaData);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "ImportStepLists";
            this.Size = new System.Drawing.Size(43, 23);
            this.Load += new System.EventHandler(this.ImportStepLists_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMetaData;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}

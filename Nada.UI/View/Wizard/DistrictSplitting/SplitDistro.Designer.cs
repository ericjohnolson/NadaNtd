namespace Nada.UI.View.Wizard
{
    partial class SplitDistro
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
            this.tblNewUnits = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // tblNewUnits
            // 
            this.tblNewUnits.AutoSize = true;
            this.tblNewUnits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNewUnits.ColumnCount = 3;
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNewUnits.Location = new System.Drawing.Point(3, 3);
            this.tblNewUnits.Name = "tblNewUnits";
            this.tblNewUnits.RowCount = 1;
            this.tblNewUnits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNewUnits.Size = new System.Drawing.Size(0, 0);
            this.tblNewUnits.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SplitDistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblNewUnits);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "SplitDistro";
            this.Size = new System.Drawing.Size(6, 6);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblNewUnits;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}

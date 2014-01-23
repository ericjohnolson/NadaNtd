namespace Nada.UI.View.Wizard
{
    partial class SplittingDistro
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblSource = new Nada.UI.Controls.H3Label();
            this.tbPercent = new System.Windows.Forms.TextBox();
            this.pnlDash = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSource.Location = new System.Drawing.Point(0, 5);
            this.lblSource.Margin = new System.Windows.Forms.Padding(0);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(129, 18);
            this.lblSource.TabIndex = 28;
            this.lblSource.Tag = "PercentToRedistribute";
            this.lblSource.Text = "PercentToRedistribute";
            this.lblSource.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbPercent
            // 
            this.tbPercent.Location = new System.Drawing.Point(4, 27);
            this.tbPercent.Name = "tbPercent";
            this.tbPercent.Size = new System.Drawing.Size(121, 21);
            this.tbPercent.TabIndex = 30;
            // 
            // pnlDash
            // 
            this.pnlDash.AutoSize = true;
            this.pnlDash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDash.Location = new System.Drawing.Point(4, 75);
            this.pnlDash.Name = "pnlDash";
            this.pnlDash.Size = new System.Drawing.Size(0, 0);
            this.pnlDash.TabIndex = 31;
            // 
            // SplittingDistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlDash);
            this.Controls.Add(this.tbPercent);
            this.Controls.Add(this.lblSource);
            this.Name = "SplittingDistro";
            this.Size = new System.Drawing.Size(129, 78);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Controls.H3Label lblSource;
        private System.Windows.Forms.TextBox tbPercent;
        private System.Windows.Forms.Panel pnlDash;
    }
}

namespace Nada.UI.View.Reports
{
    partial class ExportStep
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
            this.h3bLabel1 = new Nada.UI.Controls.H3bLabel();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(4, 4);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(78, 16);
            this.h3bLabel1.TabIndex = 0;
            this.h3bLabel1.Tag = "ReportYear";
            this.h3bLabel1.Text = "ReportYear";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(4, 23);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(133, 20);
            this.tbYear.TabIndex = 1;
            // 
            // ExportStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.h3bLabel1);
            this.Name = "ExportStep";
            this.Size = new System.Drawing.Size(313, 200);
            this.Load += new System.EventHandler(this.ExportWorkingStep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Controls.H3bLabel h3bLabel1;
        private System.Windows.Forms.TextBox tbYear;


    }
}

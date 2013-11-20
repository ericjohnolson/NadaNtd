namespace Nada.UI.View.Wizard
{
    partial class StepDemoUpdateOptions
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
            this.lblUpdateComplete = new Nada.UI.Controls.H3bLabel();
            this.SuspendLayout();
            // 
            // lblUpdateComplete
            // 
            this.lblUpdateComplete.AutoSize = true;
            this.lblUpdateComplete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblUpdateComplete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpdateComplete.Location = new System.Drawing.Point(0, 0);
            this.lblUpdateComplete.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdateComplete.Name = "lblUpdateComplete";
            this.lblUpdateComplete.Size = new System.Drawing.Size(182, 16);
            this.lblUpdateComplete.TabIndex = 4;
            this.lblUpdateComplete.Tag = "GrowthRateHasBeenApplied";
            this.lblUpdateComplete.Text = "GrowthRateHasBeenApplied";
            this.lblUpdateComplete.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // StepDemoUpdateOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblUpdateComplete);
            this.Name = "StepDemoUpdateOptions";
            this.Size = new System.Drawing.Size(510, 127);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3bLabel lblUpdateComplete;

    }
}

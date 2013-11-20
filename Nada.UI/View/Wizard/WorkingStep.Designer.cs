namespace Nada.UI.View.Wizard
{
    partial class WorkingStep
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
            this.loading1 = new Nada.UI.Controls.Loading();
            this.SuspendLayout();
            // 
            // loading1
            // 
            this.loading1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loading1.AutoSize = true;
            this.loading1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loading1.Location = new System.Drawing.Point(16, 13);
            this.loading1.Name = "loading1";
            this.loading1.Size = new System.Drawing.Size(46, 59);
            this.loading1.StatusMessage = " ";
            this.loading1.TabIndex = 0;
            // 
            // WorkingStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.loading1);
            this.Name = "WorkingStep";
            this.Size = new System.Drawing.Size(313, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Loading loading1;
    }
}

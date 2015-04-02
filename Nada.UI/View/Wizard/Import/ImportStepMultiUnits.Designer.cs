namespace Nada.UI.View.Wizard
{
    partial class ImportStepMultiUnits
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
            this.lnkDownload = new Nada.UI.Controls.H3Link();
            this.lnkUpload = new Nada.UI.Controls.H3Link();
            this.SuspendLayout();
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkDownload.BackColor = System.Drawing.Color.Transparent;
            this.lnkDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownload.Location = new System.Drawing.Point(0, 3);
            this.lnkDownload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(134, 15);
            this.lnkDownload.TabIndex = 0;
            this.lnkDownload.Tag = "ImportOneAuPerSurvey";
            this.lnkDownload.Text = "ImportOneAuPerSurvey";
            this.lnkDownload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkDownload.ClickOverride += new System.Action(this.lnkImportOne_ClickOverride);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpload.Location = new System.Drawing.Point(0, 32);
            this.lnkUpload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(136, 15);
            this.lnkUpload.TabIndex = 1;
            this.lnkUpload.Tag = "ImportMultiAuPerSurvey";
            this.lnkUpload.Text = "ImportMultiAuPerSurvey";
            this.lnkUpload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkUpload.ClickOverride += new System.Action(this.lnkImportMulti_ClickOverride);
            // 
            // ImportStepMultiUnits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkDownload);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "ImportStepMultiUnits";
            this.Size = new System.Drawing.Size(136, 47);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Link lnkDownload;
        private Controls.H3Link lnkUpload;

    }
}

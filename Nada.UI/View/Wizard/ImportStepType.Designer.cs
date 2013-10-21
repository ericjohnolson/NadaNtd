namespace Nada.UI.View.Wizard
{
    partial class ImportStepType
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkDownload.BackColor = System.Drawing.Color.Transparent;
            this.lnkDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownload.Location = new System.Drawing.Point(0, 0);
            this.lnkDownload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(107, 16);
            this.lnkDownload.TabIndex = 0;
            this.lnkDownload.Tag = "CreateImportFile";
            this.lnkDownload.Text = "CreateImportFile";
            this.lnkDownload.ClickOverride += new System.Action(this.lnkDownload_ClickOverride);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpload.Location = new System.Drawing.Point(0, 25);
            this.lnkUpload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(75, 16);
            this.lnkUpload.TabIndex = 1;
            this.lnkUpload.Tag = "UploadFile";
            this.lnkUpload.Text = "UploadFile";
            this.lnkUpload.ClickOverride += new System.Action(this.lnkUpload_ClickOverride);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportStepType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkDownload);
            this.Margin = new System.Windows.Forms.Padding(20);
            this.Name = "ImportStepType";
            this.Size = new System.Drawing.Size(107, 41);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Link lnkDownload;
        private Controls.H3Link lnkUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}

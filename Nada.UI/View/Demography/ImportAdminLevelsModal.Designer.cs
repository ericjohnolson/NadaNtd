namespace Nada.UI.View.Demography
{
    partial class ImportAdminLevelsModal
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDownload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(76, 87);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(120, 25);
            this.btnUpload.TabIndex = 13;
            this.btnUpload.Values.Text = "Upload CSV";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(76, 46);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDownload.Size = new System.Drawing.Size(120, 25);
            this.btnDownload.TabIndex = 12;
            this.btnDownload.Values.Text = "Download CSV";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportAdminLevelsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 165);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDownload);
            this.Name = "ImportAdminLevelsModal";
            this.Text = "Import Regions";
            this.Load += new System.EventHandler(this.ImportAdminLevelsModal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpload;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
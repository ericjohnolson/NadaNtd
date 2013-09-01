namespace Nada.UI.View
{
    partial class ImportDownload
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnUpload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.adminLevelMultiselect1 = new Nada.UI.View.AdminLevelMultiselect();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(570, 396);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(138, 25);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Values.Text = "Create Import File";
            this.btnUpload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // adminLevelMultiselect1
            // 
            this.adminLevelMultiselect1.Location = new System.Drawing.Point(12, 7);
            this.adminLevelMultiselect1.Name = "adminLevelMultiselect1";
            this.adminLevelMultiselect1.Size = new System.Drawing.Size(706, 383);
            this.adminLevelMultiselect1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(488, 396);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ImportDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(722, 433);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.adminLevelMultiselect1);
            this.Controls.Add(this.btnUpload);
            this.Name = "ImportDownload";
            this.Text = "Download Import File";
            this.Load += new System.EventHandler(this.ImportDemographyModal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpload;
        private AdminLevelMultiselect adminLevelMultiselect1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
    }
}
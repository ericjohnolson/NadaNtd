namespace Nada.UI.View.Wizard.IndicatorManagement
{
    partial class IndStepType
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
            this.h3Link1 = new Nada.UI.Controls.H3Link();
            this.lnkUpload = new Nada.UI.Controls.H3Link();
            this.lnkDownload = new Nada.UI.Controls.H3Link();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // h3Link1
            // 
            this.h3Link1.AutoSize = true;
            this.h3Link1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Link1.BackColor = System.Drawing.Color.Transparent;
            this.h3Link1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h3Link1.Location = new System.Drawing.Point(0, 48);
            this.h3Link1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Link1.Name = "h3Link1";
            this.h3Link1.Size = new System.Drawing.Size(98, 15);
            this.h3Link1.TabIndex = 7;
            this.h3Link1.Tag = "CreateNewForm";
            this.h3Link1.Text = "Create new form";
            this.h3Link1.TextColor = System.Drawing.Color.RoyalBlue;
            this.h3Link1.Visible = false;
            this.h3Link1.ClickOverride += new System.Action(this.h3Link1_ClickOverride);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpload.Location = new System.Drawing.Point(0, 24);
            this.lnkUpload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(67, 15);
            this.lnkUpload.TabIndex = 9;
            this.lnkUpload.Tag = "UploadFile";
            this.lnkUpload.Text = "UploadFile";
            this.lnkUpload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkUpload.ClickOverride += new System.Action(this.lnkUpload_ClickOverride);
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
            this.lnkDownload.Size = new System.Drawing.Size(99, 15);
            this.lnkDownload.TabIndex = 8;
            this.lnkDownload.Tag = "CreateImportFile";
            this.lnkDownload.Text = "CreateImportFile";
            this.lnkDownload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkDownload.ClickOverride += new System.Action(this.lnkDownload_ClickOverride);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // IndStepType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkDownload);
            this.Controls.Add(this.h3Link1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "IndStepType";
            this.Size = new System.Drawing.Size(99, 63);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Link h3Link1;
        private Controls.H3Link lnkUpload;
        private Controls.H3Link lnkDownload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}

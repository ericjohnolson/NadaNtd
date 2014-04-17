namespace Nada.UI.Controls
{
    partial class DiseasesControl
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
            this.components = new System.ComponentModel.Container();
            this.lbDiseases = new System.Windows.Forms.ListBox();
            this.diseaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.diseaseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDiseases
            // 
            this.lbDiseases.DataSource = this.diseaseBindingSource;
            this.lbDiseases.DisplayMember = "DisplayName";
            this.lbDiseases.FormattingEnabled = true;
            this.lbDiseases.ItemHeight = 15;
            this.lbDiseases.Location = new System.Drawing.Point(3, 3);
            this.lbDiseases.Name = "lbDiseases";
            this.lbDiseases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbDiseases.Size = new System.Drawing.Size(230, 109);
            this.lbDiseases.TabIndex = 4;
            // 
            // diseaseBindingSource
            // 
            this.diseaseBindingSource.DataSource = typeof(Nada.Model.Diseases.Disease);
            // 
            // DiseasesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lbDiseases);
            this.Name = "DiseasesControl";
            this.Size = new System.Drawing.Size(236, 115);
            this.Load += new System.EventHandler(this.DiseasesControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.diseaseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbDiseases;
        private System.Windows.Forms.BindingSource diseaseBindingSource;

    }
}

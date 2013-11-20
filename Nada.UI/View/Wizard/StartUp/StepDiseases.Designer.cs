namespace Nada.UI.View.Wizard
{
    partial class StepDiseases
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
            this.diseasePickerControl1 = new Nada.UI.View.DiseasePickerControl();
            this.SuspendLayout();
            // 
            // diseasePickerControl1
            // 
            this.diseasePickerControl1.AutoSize = true;
            this.diseasePickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diseasePickerControl1.Location = new System.Drawing.Point(0, 0);
            this.diseasePickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.diseasePickerControl1.Name = "diseasePickerControl1";
            this.diseasePickerControl1.Size = new System.Drawing.Size(652, 255);
            this.diseasePickerControl1.TabIndex = 0;
            // 
            // StepDiseases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.diseasePickerControl1);
            this.Name = "StepDiseases";
            this.Size = new System.Drawing.Size(652, 255);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DiseasePickerControl diseasePickerControl1;



    }
}

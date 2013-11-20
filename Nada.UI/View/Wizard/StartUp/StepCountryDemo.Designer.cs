namespace Nada.UI.View.Wizard
{
    partial class StepCountryDemo
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
            this.countryDemographyView1 = new Nada.UI.View.Demography.CountryDemographyView();
            this.SuspendLayout();
            // 
            // countryDemographyView1
            // 
            this.countryDemographyView1.AutoSize = true;
            this.countryDemographyView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.countryDemographyView1.BackColor = System.Drawing.Color.White;
            this.countryDemographyView1.Location = new System.Drawing.Point(3, 3);
            this.countryDemographyView1.Name = "countryDemographyView1";
            this.countryDemographyView1.Size = new System.Drawing.Size(496, 488);
            this.countryDemographyView1.TabIndex = 0;
            // 
            // StepCountryDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.countryDemographyView1);
            this.Name = "StepCountryDemo";
            this.Size = new System.Drawing.Size(502, 494);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Demography.CountryDemographyView countryDemographyView1;


    }
}

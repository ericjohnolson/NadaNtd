namespace Nada.UI.View.Reports
{
    partial class GenericExportStep
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.indicatorControl1 = new Nada.UI.View.IndicatorControl();
            this.SuspendLayout();
            // 
            // indicatorControl1
            // 
            this.indicatorControl1.AllowCustom = false;
            this.indicatorControl1.AutoSize = true;
            this.indicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.indicatorControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.indicatorControl1.BackColor = System.Drawing.Color.White;
            this.indicatorControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorControl1.Location = new System.Drawing.Point(10, 0);
            this.indicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.indicatorControl1.Name = "indicatorControl1";
            this.indicatorControl1.Size = new System.Drawing.Size(52, 38);
            this.indicatorControl1.TabIndex = 0;
            this.indicatorControl1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // GenericExportStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.indicatorControl1);
            this.Name = "GenericExportStep";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 20, 25);
            this.Size = new System.Drawing.Size(82, 63);
            this.Load += new System.EventHandler(this.ExportWorkingStep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private IndicatorControl indicatorControl1;


    }
}

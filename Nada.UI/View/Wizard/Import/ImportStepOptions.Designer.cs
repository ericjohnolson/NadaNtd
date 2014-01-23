namespace Nada.UI.View.Wizard
{
    partial class ImportStepOptions
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
            this.adminLevelMultiselect1 = new Nada.UI.View.AdminLevelMultiselect();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // adminLevelMultiselect1
            // 
            this.adminLevelMultiselect1.AutoSize = true;
            this.adminLevelMultiselect1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelMultiselect1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLevelMultiselect1.Location = new System.Drawing.Point(0, 0);
            this.adminLevelMultiselect1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelMultiselect1.Name = "adminLevelMultiselect1";
            this.adminLevelMultiselect1.Size = new System.Drawing.Size(782, 433);
            this.adminLevelMultiselect1.TabIndex = 22;
            // 
            // ImportStepOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.adminLevelMultiselect1);
            this.Name = "ImportStepOptions";
            this.Size = new System.Drawing.Size(782, 433);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AdminLevelMultiselect adminLevelMultiselect1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

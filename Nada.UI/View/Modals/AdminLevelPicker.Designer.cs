namespace Nada.UI.View
{
    partial class AdminLevelPicker
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
            this.components = new System.ComponentModel.Container();
            this.bsAdminLevel = new System.Windows.Forms.BindingSource(this.components);
            this.demographyTree1 = new Nada.UI.View.Demography.DemographyTree();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // bsAdminLevel
            // 
            this.bsAdminLevel.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // demographyTree1
            // 
            this.demographyTree1.BackColor = System.Drawing.Color.White;
            this.demographyTree1.Location = new System.Drawing.Point(12, 12);
            this.demographyTree1.Name = "demographyTree1";
            this.demographyTree1.Size = new System.Drawing.Size(373, 426);
            this.demographyTree1.TabIndex = 0;
            // 
            // AdminLevelPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 450);
            this.Controls.Add(this.demographyTree1);
            this.Name = "AdminLevelPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Locations";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsAdminLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsAdminLevel;
        private Demography.DemographyTree demographyTree1;
    }
}
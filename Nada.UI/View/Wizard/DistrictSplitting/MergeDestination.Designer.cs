namespace Nada.UI.View.Wizard
{
    partial class MergeDestination
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bsImportOptions = new System.Windows.Forms.BindingSource(this.components);
            this.typeListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.adminUnitAdd1 = new Nada.UI.View.Demography.AdminUnitAdd();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsImportOptions;
            // 
            // bsImportOptions
            // 
            this.bsImportOptions.DataSource = typeof(Nada.Model.Imports.ImportOptions);
            // 
            // typeListItemBindingSource
            // 
            this.typeListItemBindingSource.DataSource = typeof(Nada.Model.Imports.TypeListItem);
            // 
            // adminUnitAdd1
            // 
            this.adminUnitAdd1.AutoSize = true;
            this.adminUnitAdd1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminUnitAdd1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminUnitAdd1.Location = new System.Drawing.Point(3, 3);
            this.adminUnitAdd1.Name = "adminUnitAdd1";
            this.adminUnitAdd1.OnClose = null;
            this.adminUnitAdd1.Size = new System.Drawing.Size(379, 371);
            this.adminUnitAdd1.StatusChanged = null;
            this.adminUnitAdd1.TabIndex = 0;
            // 
            // MergeDestination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.adminUnitAdd1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "MergeDestination";
            this.Size = new System.Drawing.Size(399, 398);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsImportOptions;
        private System.Windows.Forms.BindingSource typeListItemBindingSource;
        private Demography.AdminUnitAdd adminUnitAdd1;

    }
}

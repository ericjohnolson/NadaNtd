namespace Nada.UI.View.Wizard
{
    partial class StepDemoUpdateGrowthRate
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
            this.tbGrowthRate = new System.Windows.Forms.TextBox();
            this.bsVm = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.h3bLabel1 = new Nada.UI.Controls.H3bLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsVm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbGrowthRate
            // 
            this.tbGrowthRate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVm, "GrowthRate", true));
            this.tbGrowthRate.Location = new System.Drawing.Point(3, 69);
            this.tbGrowthRate.Name = "tbGrowthRate";
            this.tbGrowthRate.Size = new System.Drawing.Size(199, 21);
            this.tbGrowthRate.TabIndex = 1;
            // 
            // bsVm
            // 
            this.bsVm.DataSource = typeof(Nada.UI.ViewModel.DemoUpdateViewModel);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsVm;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSave.Size = new System.Drawing.Size(250, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Tag = "RemindLaterUpdateDemographyYear";
            this.btnSave.Values.Text = "RemindLaterUpdateDemographyYear";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(0, 47);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(72, 16);
            this.h3bLabel1.TabIndex = 0;
            this.h3bLabel1.Tag = "GrowthRate";
            this.h3bLabel1.Text = "GrowthRate";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // StepDemoUpdateGrowthRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbGrowthRate);
            this.Controls.Add(this.h3bLabel1);
            this.Name = "StepDemoUpdateGrowthRate";
            this.Size = new System.Drawing.Size(256, 93);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsVm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3bLabel h3bLabel1;
        private System.Windows.Forms.TextBox tbGrowthRate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsVm;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;


    }
}

namespace Nada.UI.View.Wizard
{
    partial class StepDdUpdateGrowthRate
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
            this.h3bLabel1 = new Nada.UI.Controls.H3Required();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.h3bLabel2 = new Nada.UI.Controls.H3Required();
            ((System.ComponentModel.ISupportInitialize)(this.bsVm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbGrowthRate
            // 
            this.tbGrowthRate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVm, "GrowthRate", true));
            this.tbGrowthRate.Location = new System.Drawing.Point(3, 22);
            this.tbGrowthRate.Name = "tbGrowthRate";
            this.tbGrowthRate.Size = new System.Drawing.Size(199, 21);
            this.tbGrowthRate.TabIndex = 1;
            // 
            // bsVm
            // 
            this.bsVm.DataSource = typeof(Nada.UI.ViewModel.DdUpdateViewModel);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsVm;
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(0, 0);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(84, 15);
            this.h3bLabel1.TabIndex = 0;
            this.h3bLabel1.Tag = "GrowthRate";
            this.h3bLabel1.Text = "GrowthRate";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbYear
            // 
            this.tbYear.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVm, "Year", true));
            this.tbYear.Location = new System.Drawing.Point(3, 64);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(199, 21);
            this.tbYear.TabIndex = 6;
            // 
            // h3bLabel2
            // 
            this.h3bLabel2.AutoSize = true;
            this.h3bLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel2.Location = new System.Drawing.Point(3, 46);
            this.h3bLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel2.Name = "h3bLabel2";
            this.h3bLabel2.Size = new System.Drawing.Size(102, 15);
            this.h3bLabel2.TabIndex = 5;
            this.h3bLabel2.Tag = "YearDistroData";
            this.h3bLabel2.Text = "YearDistroData";
            this.h3bLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // StepDdUpdateGrowthRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.h3bLabel2);
            this.Controls.Add(this.tbGrowthRate);
            this.Controls.Add(this.h3bLabel1);
            this.Name = "StepDdUpdateGrowthRate";
            this.Size = new System.Drawing.Size(386, 128);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsVm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Required h3bLabel1;
        private System.Windows.Forms.TextBox tbGrowthRate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsVm;
        private System.Windows.Forms.TextBox tbYear;
        private Controls.H3Required h3bLabel2;


    }
}

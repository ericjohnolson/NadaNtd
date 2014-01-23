namespace Nada.UI.View.Wizard
{
    partial class SplittingIntoNumber
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bsImportOptions = new System.Windows.Forms.BindingSource(this.components);
            this.lblHowMany = new Nada.UI.Controls.H3Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsImportOptions;
            // 
            // bsImportOptions
            // 
            this.bsImportOptions.DataSource = typeof(Nada.UI.View.Wizard.DistrictSplitting.SplittingOptions);
            // 
            // lblHowMany
            // 
            this.lblHowMany.AutoSize = true;
            this.lblHowMany.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblHowMany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHowMany.Location = new System.Drawing.Point(0, 0);
            this.lblHowMany.Margin = new System.Windows.Forms.Padding(0);
            this.lblHowMany.Name = "lblHowMany";
            this.lblHowMany.Size = new System.Drawing.Size(124, 18);
            this.lblHowMany.TabIndex = 50;
            this.lblHowMany.Tag = "HowManyAdminUnits";
            this.lblHowMany.Text = "HowManyAdminUnits";
            this.lblHowMany.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsImportOptions, "SplitIntoNumber", true));
            this.textBox1.Location = new System.Drawing.Point(3, 21);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 51;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblHowMany, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(133, 45);
            this.tableLayoutPanel1.TabIndex = 52;
            // 
            // SplittingIntoNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "SplittingIntoNumber";
            this.Size = new System.Drawing.Size(139, 51);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsImportOptions;
        private System.Windows.Forms.TextBox textBox1;
        private Controls.H3Label lblHowMany;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}

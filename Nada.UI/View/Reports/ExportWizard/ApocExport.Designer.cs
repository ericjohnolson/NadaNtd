namespace Nada.UI.View.Reports
{
    partial class ApocExport
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.h3bLabel1 = new Nada.UI.Controls.H3Required();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(5, 5);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(43, 15);
            this.h3bLabel1.TabIndex = 0;
            this.h3bLabel1.Tag = "Year";
            this.h3bLabel1.Text = "Year";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Model.ExportParams);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Year", true));
            this.textBox5.Location = new System.Drawing.Point(5, 23);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(230, 21);
            this.textBox5.TabIndex = 71;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // h3Required1
            // 
            this.h3Required1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(5, 47);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(117, 15);
            this.h3Required1.TabIndex = 75;
            this.h3Required1.TabStop = false;
            this.h3Required1.Tag = "RtiReportingLevel";
            this.h3Required1.Text = "RtiReportingLevel";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbTypes
            // 
            this.cbTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "AdminLevelType", true));
            this.cbTypes.DataSource = this.bindingSource2;
            this.cbTypes.DisplayMember = "DisplayName";
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(5, 68);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(230, 23);
            this.cbTypes.TabIndex = 74;
            // 
            // ApocExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.h3Required1);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.h3bLabel1);
            this.Name = "ApocExport";
            this.Size = new System.Drawing.Size(365, 284);
            this.Load += new System.EventHandler(this.ExportWorkingStep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Controls.H3Required h3bLabel1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.BindingSource bindingSource2;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.ComboBox cbTypes;


    }
}

namespace Nada.UI.View.Reports
{
    partial class JrfExportStep
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
            this.h3bLabel2 = new Nada.UI.Controls.H3Required();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.h3bLabel3 = new Nada.UI.Controls.H3Required();
            this.h3bLabel4 = new Nada.UI.Controls.H3Required();
            this.h3bLabel5 = new Nada.UI.Controls.H3Required();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
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
            this.bindingSource1.DataSource = typeof(Nada.Model.Exports.ExportJrfQuestions);
            // 
            // h3bLabel2
            // 
            this.h3bLabel2.AutoSize = true;
            this.h3bLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel2.Location = new System.Drawing.Point(5, 51);
            this.h3bLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel2.Name = "h3bLabel2";
            this.h3bLabel2.Size = new System.Drawing.Size(91, 15);
            this.h3bLabel2.TabIndex = 2;
            this.h3bLabel2.Tag = "JrfEndemicLf";
            this.h3bLabel2.Text = "JrfEndemicLf";
            this.h3bLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "JrfEndemicLf", true));
            this.comboBox1.DisplayMember = "DisplayName";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 71);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(230, 23);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.ValueMember = "TranslationKey";
            // 
            // h3bLabel3
            // 
            this.h3bLabel3.AutoSize = true;
            this.h3bLabel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel3.Location = new System.Drawing.Point(5, 97);
            this.h3bLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel3.Name = "h3bLabel3";
            this.h3bLabel3.Size = new System.Drawing.Size(117, 15);
            this.h3bLabel3.TabIndex = 4;
            this.h3bLabel3.Tag = "JrfEndemicOncho";
            this.h3bLabel3.Text = "JrfEndemicOncho";
            this.h3bLabel3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3bLabel4
            // 
            this.h3bLabel4.AutoSize = true;
            this.h3bLabel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel4.Location = new System.Drawing.Point(5, 143);
            this.h3bLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel4.Name = "h3bLabel4";
            this.h3bLabel4.Size = new System.Drawing.Size(99, 15);
            this.h3bLabel4.TabIndex = 6;
            this.h3bLabel4.Tag = "JrfEndemicSth";
            this.h3bLabel4.Text = "JrfEndemicSth";
            this.h3bLabel4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3bLabel5
            // 
            this.h3bLabel5.AutoSize = true;
            this.h3bLabel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel5.Location = new System.Drawing.Point(5, 189);
            this.h3bLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel5.Name = "h3bLabel5";
            this.h3bLabel5.Size = new System.Drawing.Size(102, 15);
            this.h3bLabel5.TabIndex = 8;
            this.h3bLabel5.Tag = "JrfEndemicSch";
            this.h3bLabel5.Text = "JrfEndemicSch";
            this.h3bLabel5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // comboBox2
            // 
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "JrfEndemicOncho", true));
            this.comboBox2.DisplayMember = "DisplayName";
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(5, 116);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(230, 23);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.ValueMember = "TranslationKey";
            // 
            // comboBox3
            // 
            this.comboBox3.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "JrfEndemicSth", true));
            this.comboBox3.DisplayMember = "DisplayName";
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(5, 162);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(230, 23);
            this.comboBox3.TabIndex = 10;
            this.comboBox3.ValueMember = "TranslationKey";
            // 
            // comboBox4
            // 
            this.comboBox4.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "JrfEndemicSch", true));
            this.comboBox4.DisplayMember = "DisplayName";
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(5, 208);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(230, 23);
            this.comboBox4.TabIndex = 11;
            this.comboBox4.ValueMember = "TranslationKey";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "JrfYearReporting", true));
            this.textBox5.Location = new System.Drawing.Point(5, 23);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(230, 21);
            this.textBox5.TabIndex = 71;
            // 
            // cbTypes
            // 
            this.cbTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "AdminLevelType", true));
            this.cbTypes.DataSource = this.bindingSource2;
            this.cbTypes.DisplayMember = "DisplayName";
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(5, 255);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(230, 23);
            this.cbTypes.TabIndex = 72;
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
            this.h3Required1.Location = new System.Drawing.Point(5, 234);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(117, 15);
            this.h3Required1.TabIndex = 73;
            this.h3Required1.TabStop = false;
            this.h3Required1.Tag = "RtiReportingLevel";
            this.h3Required1.Text = "RtiReportingLevel";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // JrfExportStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.h3Required1);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.h3bLabel5);
            this.Controls.Add(this.h3bLabel4);
            this.Controls.Add(this.h3bLabel3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.h3bLabel2);
            this.Controls.Add(this.h3bLabel1);
            this.Name = "JrfExportStep";
            this.Size = new System.Drawing.Size(365, 319);
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
        private Controls.H3Required h3bLabel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Controls.H3Required h3bLabel3;
        private Controls.H3Required h3bLabel4;
        private Controls.H3Required h3bLabel5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cbTypes;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.BindingSource bindingSource2;


    }
}

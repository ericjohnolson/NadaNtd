namespace Nada.UI.View.Demography
{
    partial class CountryView
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
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl2 = new Nada.UI.Controls.H3Required();
            this.lbl1 = new Nada.UI.Controls.H3Required();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.bsCountry = new System.Windows.Forms.BindingSource(this.components);
            this.cbMonths = new System.Windows.Forms.ComboBox();
            this.monthItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.AutoSize = true;
            this.tableLayoutPanel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel14.ColumnCount = 3;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel14.Controls.Add(this.lbl2, 2, 0);
            this.tableLayoutPanel14.Controls.Add(this.lbl1, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.tb1, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.cbMonths, 2, 1);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 3;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(577, 50);
            this.tableLayoutPanel14.TabIndex = 2;
            // 
            // lbl2
            // 
            this.lbl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl2.AutoSize = true;
            this.lbl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl2.Location = new System.Drawing.Point(299, 0);
            this.lbl2.Margin = new System.Windows.Forms.Padding(0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(167, 15);
            this.lbl2.TabIndex = 59;
            this.lbl2.TabStop = false;
            this.lbl2.Tag = "StartMonthOfReportingYear";
            this.lbl2.Text = "StartMonthOfReportingYear";
            this.lbl2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbl1
            // 
            this.lbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl1.AutoSize = true;
            this.lbl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl1.Location = new System.Drawing.Point(0, 0);
            this.lbl1.Margin = new System.Windows.Forms.Padding(0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(95, 15);
            this.lbl1.TabIndex = 58;
            this.lbl1.TabStop = false;
            this.lbl1.Tag = "CountryName";
            this.lbl1.Text = "CountryName";
            this.lbl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tb1
            // 
            this.tb1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCountry, "Name", true));
            this.tb1.Location = new System.Drawing.Point(3, 21);
            this.tb1.Margin = new System.Windows.Forms.Padding(3, 6, 17, 6);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(256, 21);
            this.tb1.TabIndex = 0;
            // 
            // bsCountry
            // 
            this.bsCountry.DataSource = typeof(Nada.Model.Country);
            // 
            // cbMonths
            // 
            this.cbMonths.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCountry, "MonthYearStarts", true));
            this.cbMonths.DataSource = this.monthItemBindingSource;
            this.cbMonths.DisplayMember = "Name";
            this.cbMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonths.FormattingEnabled = true;
            this.cbMonths.Location = new System.Drawing.Point(302, 21);
            this.cbMonths.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbMonths.Name = "cbMonths";
            this.cbMonths.Size = new System.Drawing.Size(250, 23);
            this.cbMonths.TabIndex = 1;
            this.cbMonths.ValueMember = "Id";
            // 
            // monthItemBindingSource
            // 
            this.monthItemBindingSource.DataSource = typeof(Nada.Globalization.MonthItem);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsCountry;
            // 
            // CountryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel14);
            this.Name = "CountryView";
            this.Size = new System.Drawing.Size(583, 56);
            this.Load += new System.EventHandler(this.CountryView_Load);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private Controls.H3Required lbl1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.BindingSource bsCountry;
        private System.Windows.Forms.ComboBox cbMonths;
        private Controls.H3Required lbl2;
        private System.Windows.Forms.BindingSource monthItemBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

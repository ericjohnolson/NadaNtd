namespace Nada.UI.View.Reports
{
    partial class RtiExport
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRtiLanguage = new Nada.UI.Controls.H3Required();
            this.cbLanguages = new System.Windows.Forms.ComboBox();
            this.bsLanguages = new System.Windows.Forms.BindingSource(this.components);
            this.h3Label2 = new Nada.UI.Controls.H3Required();
            this.h3Label3 = new Nada.UI.Controls.H3Required();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblRtiLanguage, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cbLanguages, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.h3Label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.h3Label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dtStart, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtEnd, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbTypes, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.h3Required1, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 184);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblRtiLanguage
            // 
            this.lblRtiLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRtiLanguage.AutoSize = true;
            this.lblRtiLanguage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblRtiLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRtiLanguage.Location = new System.Drawing.Point(0, 134);
            this.lblRtiLanguage.Margin = new System.Windows.Forms.Padding(0);
            this.lblRtiLanguage.Name = "lblRtiLanguage";
            this.lblRtiLanguage.Size = new System.Drawing.Size(124, 15);
            this.lblRtiLanguage.TabIndex = 8;
            this.lblRtiLanguage.TabStop = false;
            this.lblRtiLanguage.Tag = "RtiExportLanguage";
            this.lblRtiLanguage.Text = "RitExportLanguage";
            this.lblRtiLanguage.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbLanguages
            // 
            this.cbLanguages.DataSource = this.bsLanguages;
            this.cbLanguages.DisplayMember = "Name";
            this.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguages.FormattingEnabled = true;
            this.cbLanguages.Location = new System.Drawing.Point(3, 155);
            this.cbLanguages.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.Size = new System.Drawing.Size(251, 23);
            this.cbLanguages.TabIndex = 9;
            this.cbLanguages.ValueMember = "IsoCode";
            // 
            // bsLanguages
            // 
            this.bsLanguages.DataSource = typeof(Nada.Model.Language);
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(0, 0);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(70, 15);
            this.h3Label2.TabIndex = 10;
            this.h3Label2.Tag = "StartDate";
            this.h3Label2.Text = "StartDate";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(0, 42);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(67, 15);
            this.h3Label3.TabIndex = 11;
            this.h3Label3.Tag = "EndDate";
            this.h3Label3.Text = "EndDate";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // dtStart
            // 
            this.dtStart.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource2, "StartDate", true));
            this.dtStart.Location = new System.Drawing.Point(3, 18);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(251, 21);
            this.dtStart.TabIndex = 12;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(Nada.Model.Exports.RtiWorkbooksExporter);
            // 
            // dtEnd
            // 
            this.dtEnd.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource2, "EndDate", true));
            this.dtEnd.Location = new System.Drawing.Point(3, 60);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(251, 21);
            this.dtEnd.TabIndex = 13;
            // 
            // cbTypes
            // 
            this.cbTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource2, "AdminLevelType", true));
            this.cbTypes.DataSource = this.bindingSource1;
            this.cbTypes.DisplayMember = "DisplayName";
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(3, 105);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(251, 23);
            this.cbTypes.TabIndex = 8;
            // 
            // h3Required1
            // 
            this.h3Required1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(0, 84);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(117, 15);
            this.h3Required1.TabIndex = 7;
            this.h3Required1.TabStop = false;
            this.h3Required1.Tag = "RtiReportingLevel";
            this.h3Required1.Text = "RtiReportingLevel";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // RtiExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RtiExport";
            this.Size = new System.Drawing.Size(365, 250);
            this.Load += new System.EventHandler(this.RtiExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Required h3Label2;
        private Controls.H3Required h3Label3;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.ComboBox cbTypes;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private Controls.H3Required lblRtiLanguage;
        private System.Windows.Forms.ComboBox cbLanguages;
        private System.Windows.Forms.BindingSource bsLanguages;


    }
}

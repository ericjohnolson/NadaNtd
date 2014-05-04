﻿namespace Nada.UI.View.Reports.Standard
{
    partial class EliminationOptions
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.h3Label2 = new Nada.UI.Controls.H3Required();
            this.h3Label3 = new Nada.UI.Controls.H3Required();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.cbMonths = new System.Windows.Forms.ComboBox();
            this.monthItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAggLevel = new System.Windows.Forms.RadioButton();
            this.rbAggCountry = new System.Windows.Forms.RadioButton();
            this.h3Label1 = new Nada.UI.Controls.H3Required();
            this.h3Required3 = new Nada.UI.Controls.H3Required();
            this.h3Required2 = new Nada.UI.Controls.H3Required();
            this.diseasesControl1 = new Nada.UI.Controls.DiseasesControl();
            this.cbEliminationType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthItemBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.h3Label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.h3Label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dtStart, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtEnd, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbMonths, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.h3Required1, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 183);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 134);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.dtStart.Location = new System.Drawing.Point(3, 18);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(251, 21);
            this.dtStart.TabIndex = 12;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(3, 60);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(251, 21);
            this.dtEnd.TabIndex = 13;
            // 
            // cbMonths
            // 
            this.cbMonths.DataSource = this.monthItemBindingSource;
            this.cbMonths.DisplayMember = "Name";
            this.cbMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonths.FormattingEnabled = true;
            this.cbMonths.Location = new System.Drawing.Point(3, 105);
            this.cbMonths.Margin = new System.Windows.Forms.Padding(3, 6, 25, 6);
            this.cbMonths.Name = "cbMonths";
            this.cbMonths.Size = new System.Drawing.Size(251, 23);
            this.cbMonths.TabIndex = 8;
            this.cbMonths.ValueMember = "Id";
            // 
            // monthItemBindingSource
            // 
            this.monthItemBindingSource.DataSource = typeof(Nada.Globalization.MonthItem);
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
            this.h3Required1.Size = new System.Drawing.Size(167, 15);
            this.h3Required1.TabIndex = 7;
            this.h3Required1.TabStop = false;
            this.h3Required1.Tag = "StartMonthOfReportingYear";
            this.h3Required1.Text = "StartMonthOfReportingYear";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.h3Label1, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.h3Required3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.h3Required2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.diseasesControl1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbEliminationType, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(453, 400);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbAggLevel);
            this.panel1.Controls.Add(this.rbAggCountry);
            this.panel1.Location = new System.Drawing.Point(3, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 59);
            this.panel1.TabIndex = 4;
            // 
            // rbAggLevel
            // 
            this.rbAggLevel.AutoSize = true;
            this.rbAggLevel.Location = new System.Drawing.Point(3, 28);
            this.rbAggLevel.Name = "rbAggLevel";
            this.rbAggLevel.Size = new System.Drawing.Size(75, 19);
            this.rbAggLevel.TabIndex = 2;
            this.rbAggLevel.TabStop = true;
            this.rbAggLevel.Tag = "AggLevel";
            this.rbAggLevel.Text = "AggLevel";
            this.rbAggLevel.UseVisualStyleBackColor = true;
            // 
            // rbAggCountry
            // 
            this.rbAggCountry.AutoSize = true;
            this.rbAggCountry.Location = new System.Drawing.Point(3, 3);
            this.rbAggCountry.Name = "rbAggCountry";
            this.rbAggCountry.Size = new System.Drawing.Size(88, 19);
            this.rbAggCountry.TabIndex = 1;
            this.rbAggCountry.TabStop = true;
            this.rbAggCountry.Tag = "AggCountry";
            this.rbAggCountry.Text = "AggCountry";
            this.rbAggCountry.UseVisualStyleBackColor = true;
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 320);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(95, 15);
            this.h3Label1.TabIndex = 3;
            this.h3Label1.Tag = "AggregatedBy";
            this.h3Label1.Text = "AggregatedBy";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Required3
            // 
            this.h3Required3.AutoSize = true;
            this.h3Required3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required3.Location = new System.Drawing.Point(0, 0);
            this.h3Required3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required3.Name = "h3Required3";
            this.h3Required3.Size = new System.Drawing.Size(44, 15);
            this.h3Required3.TabIndex = 11;
            this.h3Required3.Tag = "Type";
            this.h3Required3.Text = "Type";
            this.h3Required3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Required2
            // 
            this.h3Required2.AutoSize = true;
            this.h3Required2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required2.Location = new System.Drawing.Point(0, 44);
            this.h3Required2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required2.Name = "h3Required2";
            this.h3Required2.Size = new System.Drawing.Size(73, 15);
            this.h3Required2.TabIndex = 11;
            this.h3Required2.Tag = "Diseases";
            this.h3Required2.Text = "Diseases";
            this.h3Required2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // diseasesControl1
            // 
            this.diseasesControl1.AutoSize = true;
            this.diseasesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diseasesControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseasesControl1.Location = new System.Drawing.Point(3, 62);
            this.diseasesControl1.Name = "diseasesControl1";
            this.diseasesControl1.Size = new System.Drawing.Size(236, 115);
            this.diseasesControl1.TabIndex = 12;
            // 
            // cbEliminationType
            // 
            this.cbEliminationType.FormattingEnabled = true;
            this.cbEliminationType.Location = new System.Drawing.Point(3, 18);
            this.cbEliminationType.Name = "cbEliminationType";
            this.cbEliminationType.Size = new System.Drawing.Size(254, 23);
            this.cbEliminationType.TabIndex = 13;
            this.cbEliminationType.SelectedIndexChanged += new System.EventHandler(this.cbEliminationType_SelectedIndexChanged);
            // 
            // EliminationOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "EliminationOptions";
            this.Size = new System.Drawing.Size(459, 406);
            this.Load += new System.EventHandler(this.StepOptions_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthItemBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.ComboBox cbMonths;
        private System.Windows.Forms.BindingSource monthItemBindingSource;
        private Controls.H3Required h3Label2;
        private Controls.H3Required h3Label3;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private Controls.H3Required h3Required3;
        private Controls.H3Required h3Required2;
        private Controls.DiseasesControl diseasesControl1;
        private System.Windows.Forms.ComboBox cbEliminationType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbAggLevel;
        private System.Windows.Forms.RadioButton rbAggCountry;
        private Controls.H3Required h3Label1;
    }
}
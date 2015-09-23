namespace Nada.UI.View.Reports.Standard
{
    partial class PersonsTreatedCoverageOptions
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
            this.cbReportTypeLabel = new Nada.UI.Controls.H3Required();
            this.cbReportTypes = new System.Windows.Forms.ComboBox();
            this.lbDiseasesLabel = new Nada.UI.Controls.H3Label();
            this.lbDiseases = new System.Windows.Forms.ListBox();
            this.diseaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbDrugPackageLabel = new Nada.UI.Controls.H3Label();
            this.lbDrugPackages = new System.Windows.Forms.ListBox();
            this.intvTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbAggreateByLabel = new Nada.UI.Controls.H3Required();
            this.cbAggregateBy = new System.Windows.Forms.ComboBox();
            this.personsTreatedCoverageReportOptionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.adminLevelTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbYear = new Nada.UI.Controls.H3Required();
            this.lbYears = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diseaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intvTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personsTreatedCoverageReportOptionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cbReportTypeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbReportTypes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDiseasesLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbDiseases, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbDrugPackageLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbDrugPackages, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbAggreateByLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbAggregateBy, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lbYear, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbYears, 0, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(528, 750);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cbReportTypeLabel
            // 
            this.cbReportTypeLabel.AutoSize = true;
            this.cbReportTypeLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbReportTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbReportTypeLabel.Location = new System.Drawing.Point(0, 0);
            this.cbReportTypeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.cbReportTypeLabel.Name = "cbReportTypeLabel";
            this.cbReportTypeLabel.Size = new System.Drawing.Size(44, 15);
            this.cbReportTypeLabel.TabIndex = 0;
            this.cbReportTypeLabel.Tag = "Type";
            this.cbReportTypeLabel.Text = "Type";
            this.cbReportTypeLabel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbReportTypes
            // 
            this.cbReportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportTypes.FormattingEnabled = true;
            this.cbReportTypes.Location = new System.Drawing.Point(3, 18);
            this.cbReportTypes.Name = "cbReportTypes";
            this.cbReportTypes.Size = new System.Drawing.Size(140, 23);
            this.cbReportTypes.TabIndex = 1;
            this.cbReportTypes.SelectedIndexChanged += new System.EventHandler(this.cbReportTypes_SelectedIndexChanged);
            // 
            // lbDiseasesLabel
            // 
            this.lbDiseasesLabel.AutoSize = true;
            this.lbDiseasesLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbDiseasesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDiseasesLabel.Location = new System.Drawing.Point(0, 51);
            this.lbDiseasesLabel.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lbDiseasesLabel.Name = "lbDiseasesLabel";
            this.lbDiseasesLabel.Size = new System.Drawing.Size(54, 16);
            this.lbDiseasesLabel.TabIndex = 2;
            this.lbDiseasesLabel.Tag = "Disease";
            this.lbDiseasesLabel.Text = "Disease";
            this.lbDiseasesLabel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbDiseases
            // 
            this.lbDiseases.DataSource = this.diseaseBindingSource;
            this.lbDiseases.DisplayMember = "DisplayName";
            this.lbDiseases.FormattingEnabled = true;
            this.lbDiseases.ItemHeight = 15;
            this.lbDiseases.Location = new System.Drawing.Point(3, 70);
            this.lbDiseases.Name = "lbDiseases";
            this.lbDiseases.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbDiseases.Size = new System.Drawing.Size(300, 169);
            this.lbDiseases.TabIndex = 3;
            this.lbDiseases.Tag = "";
            // 
            // diseaseBindingSource
            // 
            this.diseaseBindingSource.DataSource = typeof(Nada.Model.Diseases.Disease);
            // 
            // lbDrugPackageLabel
            // 
            this.lbDrugPackageLabel.AutoSize = true;
            this.lbDrugPackageLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbDrugPackageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDrugPackageLabel.Location = new System.Drawing.Point(0, 249);
            this.lbDrugPackageLabel.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lbDrugPackageLabel.Name = "lbDrugPackageLabel";
            this.lbDrugPackageLabel.Size = new System.Drawing.Size(85, 16);
            this.lbDrugPackageLabel.TabIndex = 4;
            this.lbDrugPackageLabel.Tag = "Drug Package";
            this.lbDrugPackageLabel.Text = "Drug Package";
            this.lbDrugPackageLabel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbDrugPackages
            // 
            this.lbDrugPackages.DataSource = this.intvTypeBindingSource;
            this.lbDrugPackages.DisplayMember = "IntvTypeName";
            this.lbDrugPackages.FormattingEnabled = true;
            this.lbDrugPackages.ItemHeight = 15;
            this.lbDrugPackages.Location = new System.Drawing.Point(3, 268);
            this.lbDrugPackages.Name = "lbDrugPackages";
            this.lbDrugPackages.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbDrugPackages.Size = new System.Drawing.Size(300, 169);
            this.lbDrugPackages.TabIndex = 5;
            this.lbDrugPackages.Tag = "";
            // 
            // intvTypeBindingSource
            // 
            this.intvTypeBindingSource.DataSource = typeof(Nada.Model.Intervention.IntvType);
            // 
            // cbAggreateByLabel
            // 
            this.cbAggreateByLabel.AutoSize = true;
            this.cbAggreateByLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbAggreateByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbAggreateByLabel.Location = new System.Drawing.Point(0, 447);
            this.cbAggreateByLabel.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.cbAggreateByLabel.Name = "cbAggreateByLabel";
            this.cbAggreateByLabel.Size = new System.Drawing.Size(91, 15);
            this.cbAggreateByLabel.TabIndex = 6;
            this.cbAggreateByLabel.Tag = "Aggregate By";
            this.cbAggreateByLabel.Text = "Aggregate By";
            this.cbAggreateByLabel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbAggregateBy
            // 
            this.cbAggregateBy.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.personsTreatedCoverageReportOptionsBindingSource, "DistrictType", true));
            this.cbAggregateBy.DataSource = this.adminLevelTypeBindingSource;
            this.cbAggregateBy.DisplayMember = "DisplayName";
            this.cbAggregateBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAggregateBy.FormattingEnabled = true;
            this.cbAggregateBy.Location = new System.Drawing.Point(3, 465);
            this.cbAggregateBy.Name = "cbAggregateBy";
            this.cbAggregateBy.Size = new System.Drawing.Size(121, 23);
            this.cbAggregateBy.TabIndex = 7;
            // 
            // personsTreatedCoverageReportOptionsBindingSource
            // 
            this.personsTreatedCoverageReportOptionsBindingSource.DataSource = typeof(Nada.Model.Reports.PersonsTreatedCoverageReportOptions);
            // 
            // adminLevelTypeBindingSource
            // 
            this.adminLevelTypeBindingSource.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbYear.Location = new System.Drawing.Point(0, 498);
            this.lbYear.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(43, 15);
            this.lbYear.TabIndex = 8;
            this.lbYear.Tag = "lbYear";
            this.lbYear.Text = "Year";
            this.lbYear.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbYears
            // 
            this.lbYears.FormattingEnabled = true;
            this.lbYears.ItemHeight = 15;
            this.lbYears.Location = new System.Drawing.Point(3, 516);
            this.lbYears.Name = "lbYears";
            this.lbYears.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbYears.Size = new System.Drawing.Size(120, 169);
            this.lbYears.TabIndex = 9;
            // 
            // PersonsTreatedCoverageOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PersonsTreatedCoverageOptions";
            this.Size = new System.Drawing.Size(528, 750);
            this.Load += new System.EventHandler(this.StepOptions_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diseaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intvTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personsTreatedCoverageReportOptionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Required cbReportTypeLabel;
        private System.Windows.Forms.ComboBox cbReportTypes;
        private Controls.H3Label lbDiseasesLabel;
        private System.Windows.Forms.ListBox lbDiseases;
        private Controls.H3Label lbDrugPackageLabel;
        private System.Windows.Forms.ListBox lbDrugPackages;
        private Controls.H3Required cbAggreateByLabel;
        private System.Windows.Forms.ComboBox cbAggregateBy;
        private Controls.H3Required lbYear;
        private System.Windows.Forms.ListBox lbYears;
        private System.Windows.Forms.BindingSource personsTreatedCoverageReportOptionsBindingSource;
        private System.Windows.Forms.BindingSource adminLevelTypeBindingSource;
        private System.Windows.Forms.BindingSource diseaseBindingSource;
        private System.Windows.Forms.BindingSource intvTypeBindingSource;
    }
}

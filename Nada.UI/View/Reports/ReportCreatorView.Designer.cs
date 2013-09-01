namespace Nada.UI.View.Reports
{
    partial class ReportCreatorView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.surveyIndicators = new Nada.UI.View.ReportIndicatorControl();
            this.intvIndicators = new Nada.UI.View.ReportIndicatorControl();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkGroupBy = new System.Windows.Forms.CheckBox();
            this.btnExportGrid = new System.Windows.Forms.Button();
            this.cbGroupGrid = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportChart = new System.Windows.Forms.Button();
            this.cbChartType = new System.Windows.Forms.ComboBox();
            this.reportIndicatorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlChartContainer = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveChart = new System.Windows.Forms.SaveFileDialog();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportIndicatorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(954, 551);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(946, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Indicator(s)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.surveyIndicators, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.intvIndicators, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.kryptonButton1, 0, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(612, 103);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Intervention Indicators";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Survey Indicators";
            // 
            // surveyIndicators
            // 
            this.surveyIndicators.AutoSize = true;
            this.surveyIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.surveyIndicators.Location = new System.Drawing.Point(0, 46);
            this.surveyIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.surveyIndicators.Name = "surveyIndicators";
            this.surveyIndicators.Size = new System.Drawing.Size(306, 23);
            this.surveyIndicators.TabIndex = 5;
            // 
            // intvIndicators
            // 
            this.intvIndicators.AutoSize = true;
            this.intvIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.intvIndicators.Location = new System.Drawing.Point(306, 46);
            this.intvIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.intvIndicators.Name = "intvIndicators";
            this.intvIndicators.Size = new System.Drawing.Size(306, 23);
            this.intvIndicators.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 27);
            this.btnSave.TabIndex = 21;
            this.btnSave.Values.Text = "Generate";
            this.btnSave.Click += new System.EventHandler(this.btnGen2_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(946, 525);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Data Table";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(946, 525);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkGroupBy);
            this.panel1.Controls.Add(this.btnExportGrid);
            this.panel1.Controls.Add(this.cbGroupGrid);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 42);
            this.panel1.TabIndex = 0;
            // 
            // chkGroupBy
            // 
            this.chkGroupBy.AutoSize = true;
            this.chkGroupBy.Location = new System.Drawing.Point(15, 12);
            this.chkGroupBy.Name = "chkGroupBy";
            this.chkGroupBy.Size = new System.Drawing.Size(70, 17);
            this.chkGroupBy.TabIndex = 2;
            this.chkGroupBy.Text = "Group By";
            this.chkGroupBy.UseVisualStyleBackColor = true;
            // 
            // btnExportGrid
            // 
            this.btnExportGrid.Location = new System.Drawing.Point(245, 8);
            this.btnExportGrid.Name = "btnExportGrid";
            this.btnExportGrid.Size = new System.Drawing.Size(75, 23);
            this.btnExportGrid.TabIndex = 4;
            this.btnExportGrid.Text = "Export";
            this.btnExportGrid.UseVisualStyleBackColor = true;
            this.btnExportGrid.Click += new System.EventHandler(this.btnExportGrid_Click);
            // 
            // cbGroupGrid
            // 
            this.cbGroupGrid.FormattingEnabled = true;
            this.cbGroupGrid.Items.AddRange(new object[] {
            "Country",
            "Region",
            "District",
            "Site"});
            this.cbGroupGrid.Location = new System.Drawing.Point(102, 10);
            this.cbGroupGrid.Name = "cbGroupGrid";
            this.cbGroupGrid.Size = new System.Drawing.Size(121, 21);
            this.cbGroupGrid.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(940, 471);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(946, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlChartContainer, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(940, 519);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnExportChart);
            this.panel2.Controls.Add(this.cbChartType);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(418, 42);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chart Type";
            // 
            // btnExportChart
            // 
            this.btnExportChart.Location = new System.Drawing.Point(224, 7);
            this.btnExportChart.Name = "btnExportChart";
            this.btnExportChart.Size = new System.Drawing.Size(75, 23);
            this.btnExportChart.TabIndex = 3;
            this.btnExportChart.Text = "Export";
            this.btnExportChart.UseVisualStyleBackColor = true;
            this.btnExportChart.Click += new System.EventHandler(this.btnExportChart_Click);
            // 
            // cbChartType
            // 
            this.cbChartType.DataSource = this.reportIndicatorBindingSource;
            this.cbChartType.DisplayMember = "Name";
            this.cbChartType.FormattingEnabled = true;
            this.cbChartType.Location = new System.Drawing.Point(81, 9);
            this.cbChartType.Name = "cbChartType";
            this.cbChartType.Size = new System.Drawing.Size(121, 21);
            this.cbChartType.TabIndex = 0;
            this.cbChartType.SelectedIndexChanged += new System.EventHandler(this.cbChartType_SelectedIndexChanged);
            // 
            // reportIndicatorBindingSource
            // 
            this.reportIndicatorBindingSource.DataSource = typeof(Nada.Model.Reports.ReportIndicator);
            // 
            // pnlChartContainer
            // 
            this.pnlChartContainer.AutoSize = true;
            this.pnlChartContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlChartContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartContainer.Location = new System.Drawing.Point(3, 51);
            this.pnlChartContainer.Name = "pnlChartContainer";
            this.pnlChartContainer.Size = new System.Drawing.Size(934, 465);
            this.pnlChartContainer.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(946, 525);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Map";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xlsx";
            this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
            // 
            // saveChart
            // 
            this.saveChart.Filter = "PNG Image (.png)|*.png";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(3, 72);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(131, 28);
            this.kryptonButton1.TabIndex = 22;
            this.kryptonButton1.Values.Text = "Generate";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // ReportCreatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ReportCreatorView";
            this.Size = new System.Drawing.Size(954, 551);
            this.Load += new System.EventHandler(this.ReportCreatorView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportIndicatorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportChart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChartType;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnExportGrid;
        private System.Windows.Forms.CheckBox chkGroupBy;
        private System.Windows.Forms.ComboBox cbGroupGrid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlChartContainer;
        private System.Windows.Forms.SaveFileDialog saveChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private ReportIndicatorControl surveyIndicators;
        private ReportIndicatorControl intvIndicators;
        private System.Windows.Forms.BindingSource reportIndicatorBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}

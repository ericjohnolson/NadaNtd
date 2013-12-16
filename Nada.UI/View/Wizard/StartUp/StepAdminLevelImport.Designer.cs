namespace Nada.UI.View.Wizard
{
    partial class StepAdminLevelImport
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
            this.tbYear = new System.Windows.Forms.TextBox();
            this.lblYear = new Nada.UI.Controls.H3Required();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.cbImportFor = new System.Windows.Forms.ComboBox();
            this.adminLevelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbImportFor = new Nada.UI.Controls.H3Required();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnImport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDownload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.loadingImport = new Nada.UI.Controls.Loading();
            this.tbRows = new System.Windows.Forms.TextBox();
            this.lblRows = new Nada.UI.Controls.H3Required();
            this.lblIsAggLevel = new Nada.UI.Controls.H3bLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelBindingSource)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tbYear, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblYear, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbStatus, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.cbImportFor, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbImportFor, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.tbRows, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblRows, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblIsAggLevel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 430);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(3, 159);
            this.tbYear.Margin = new System.Windows.Forms.Padding(3, 6, 23, 12);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(116, 21);
            this.tbYear.TabIndex = 5;
            this.tbYear.Visible = false;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblYear.Location = new System.Drawing.Point(0, 138);
            this.lblYear.Margin = new System.Windows.Forms.Padding(0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(140, 15);
            this.lblYear.TabIndex = 5;
            this.lblYear.Tag = "YearDemographyData";
            this.lblYear.Text = "YearDemographyData";
            this.lblYear.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblYear.Visible = false;
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(3, 274);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStatus.Size = new System.Drawing.Size(527, 153);
            this.tbStatus.TabIndex = 5;
            // 
            // cbImportFor
            // 
            this.cbImportFor.DataSource = this.adminLevelBindingSource;
            this.cbImportFor.DisplayMember = "Name";
            this.cbImportFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImportFor.FormattingEnabled = true;
            this.cbImportFor.Location = new System.Drawing.Point(3, 49);
            this.cbImportFor.Margin = new System.Windows.Forms.Padding(3, 6, 23, 12);
            this.cbImportFor.Name = "cbImportFor";
            this.cbImportFor.Size = new System.Drawing.Size(286, 23);
            this.cbImportFor.TabIndex = 5;
            this.cbImportFor.ValueMember = "Id";
            this.cbImportFor.Visible = false;
            // 
            // adminLevelBindingSource
            // 
            this.adminLevelBindingSource.DataSource = typeof(Nada.Model.AdminLevel);
            // 
            // tbImportFor
            // 
            this.tbImportFor.AutoSize = true;
            this.tbImportFor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbImportFor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbImportFor.Location = new System.Drawing.Point(0, 28);
            this.tbImportFor.Margin = new System.Windows.Forms.Padding(0);
            this.tbImportFor.Name = "tbImportFor";
            this.tbImportFor.Size = new System.Drawing.Size(109, 15);
            this.tbImportFor.TabIndex = 6;
            this.tbImportFor.Tag = "CreateImportFor";
            this.tbImportFor.Text = "CreateImportFor";
            this.tbImportFor.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbImportFor.Visible = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.btnImport, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnDownload, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.loadingImport, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 195);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(310, 73);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.AutoSize = true;
            this.btnImport.Location = new System.Drawing.Point(202, 12);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.btnImport.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnImport.Name = "btnImport";
            this.btnImport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnImport.Size = new System.Drawing.Size(105, 29);
            this.btnImport.TabIndex = 1;
            this.btnImport.Tag = "UploadImportFile";
            this.btnImport.Values.Text = "UploadImportFile";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSize = true;
            this.btnDownload.Location = new System.Drawing.Point(62, 12);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.btnDownload.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDownload.Size = new System.Drawing.Size(122, 29);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Tag = "DownloadImportFile";
            this.btnDownload.Values.Text = "DownloadImportFile";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // loadingImport
            // 
            this.loadingImport.AutoSize = true;
            this.loadingImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loadingImport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingImport.Location = new System.Drawing.Point(3, 3);
            this.loadingImport.Name = "loadingImport";
            this.loadingImport.Size = new System.Drawing.Size(53, 67);
            this.loadingImport.StatusMessage = " ";
            this.loadingImport.TabIndex = 2;
            this.loadingImport.Visible = false;
            // 
            // tbRows
            // 
            this.tbRows.Location = new System.Drawing.Point(3, 105);
            this.tbRows.Margin = new System.Windows.Forms.Padding(3, 6, 23, 12);
            this.tbRows.Name = "tbRows";
            this.tbRows.Size = new System.Drawing.Size(116, 21);
            this.tbRows.TabIndex = 3;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblRows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRows.Location = new System.Drawing.Point(0, 84);
            this.lblRows.Margin = new System.Windows.Forms.Padding(0);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(130, 15);
            this.lblRows.TabIndex = 2;
            this.lblRows.Tag = "NumberOfLocations";
            this.lblRows.Text = "NumberOfLocations";
            this.lblRows.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lblIsAggLevel
            // 
            this.lblIsAggLevel.AutoSize = true;
            this.lblIsAggLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblIsAggLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblIsAggLevel.Location = new System.Drawing.Point(0, 0);
            this.lblIsAggLevel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblIsAggLevel.Name = "lblIsAggLevel";
            this.lblIsAggLevel.Size = new System.Drawing.Size(67, 16);
            this.lblIsAggLevel.TabIndex = 8;
            this.lblIsAggLevel.Tag = "IsAggLevel";
            this.lblIsAggLevel.Text = "IsAggLevel";
            this.lblIsAggLevel.TextColor = System.Drawing.Color.Red;
            this.lblIsAggLevel.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xlsx";
            this.saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
            // 
            // StepAdminLevelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StepAdminLevelImport";
            this.Size = new System.Drawing.Size(539, 436);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminLevelBindingSource)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3Required lblRows;
        private System.Windows.Forms.TextBox tbRows;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownload;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.ComboBox cbImportFor;
        private Controls.H3Required tbImportFor;
        private Controls.Loading loadingImport;
        private System.Windows.Forms.BindingSource adminLevelBindingSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.H3bLabel lblIsAggLevel;
        private System.Windows.Forms.TextBox tbYear;
        private Controls.H3Required lblYear;



    }
}

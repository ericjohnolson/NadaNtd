namespace Nada.UI.View.Wizard
{
    partial class StepDdReview
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
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.adminLevelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnImport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDownload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.loadingImport = new Nada.UI.Controls.Loading();
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
            this.tableLayoutPanel1.Controls.Add(this.tbStatus, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 9);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 238);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(3, 82);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStatus.Size = new System.Drawing.Size(527, 153);
            this.tbStatus.TabIndex = 5;
            // 
            // adminLevelBindingSource
            // 
            this.adminLevelBindingSource.DataSource = typeof(Nada.Model.AdminLevel);
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
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
            this.Size = new System.Drawing.Size(539, 244);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownload;
        private System.Windows.Forms.TextBox tbStatus;
        private Controls.Loading loadingImport;
        private System.Windows.Forms.BindingSource adminLevelBindingSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;



    }
}

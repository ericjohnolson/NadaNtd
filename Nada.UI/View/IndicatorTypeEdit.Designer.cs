namespace Nada.UI.View
{
    partial class IndicatorTypeEdit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvIndicators = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tbName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pnlName = new System.Windows.Forms.Panel();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tblTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblDiseaseType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tblTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvIndicators
            // 
            this.lvIndicators.AllColumns.Add(this.olvColumn8);
            this.lvIndicators.AllColumns.Add(this.olvColumn9);
            this.lvIndicators.AllColumns.Add(this.olvColumn10);
            this.lvIndicators.AllColumns.Add(this.olvColumn2);
            this.lvIndicators.AllColumns.Add(this.olvColumn1);
            this.lvIndicators.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10,
            this.olvColumn2,
            this.olvColumn1});
            this.lvIndicators.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvIndicators.Location = new System.Drawing.Point(2, 47);
            this.lvIndicators.Name = "lvIndicators";
            this.lvIndicators.ShowGroups = false;
            this.lvIndicators.Size = new System.Drawing.Size(646, 166);
            this.lvIndicators.TabIndex = 1;
            this.lvIndicators.UseCompatibleStateImageBehavior = false;
            this.lvIndicators.UseHyperlinks = true;
            this.lvIndicators.View = System.Windows.Forms.View.Details;
            this.lvIndicators.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvIndicators_HyperlinkClicked);
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "DisplayName";
            this.olvColumn8.CellPadding = null;
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.Text = "Name";
            this.olvColumn8.Width = 243;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "DataType";
            this.olvColumn9.CellPadding = null;
            this.olvColumn9.IsEditable = false;
            this.olvColumn9.Text = "Type";
            this.olvColumn9.Width = 107;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "IsRequired";
            this.olvColumn10.CellPadding = null;
            this.olvColumn10.IsEditable = false;
            this.olvColumn10.Text = "Is Required";
            this.olvColumn10.Width = 72;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "IsDisabled";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Disabled";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "EditText";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Hyperlink = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Edit";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(0, 22);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(255, 21);
            this.tbName.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.pnlName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(27, 59);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(657, 327);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 292);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(206, 35);
            this.tableLayoutPanel4.TabIndex = 47;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(112, 3);
            this.btnCancel.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(91, 29);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Tag = "Cancel";
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.MinimumSize = new System.Drawing.Size(91, 29);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSave.Size = new System.Drawing.Size(91, 29);
            this.btnSave.TabIndex = 1;
            this.btnSave.Tag = "Save";
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // pnlName
            // 
            this.pnlName.AutoSize = true;
            this.pnlName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlName.Controls.Add(this.h3Label1);
            this.pnlName.Controls.Add(this.tbName);
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.Margin = new System.Windows.Forms.Padding(0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(280, 46);
            this.pnlName.TabIndex = 0;
            this.pnlName.Visible = false;
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(41, 18);
            this.h3Label1.TabIndex = 15;
            this.h3Label1.Tag = "Name";
            this.h3Label1.Text = "Name";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.fieldLink1);
            this.panel1.Controls.Add(this.lvIndicators);
            this.panel1.Controls.Add(this.lblCustomIndicators);
            this.panel1.Location = new System.Drawing.Point(3, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 216);
            this.panel1.TabIndex = 2;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(2, 25);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(154, 16);
            this.fieldLink1.TabIndex = 22;
            this.fieldLink1.Tag = "AddIndicatorLink";
            this.fieldLink1.Text = "Add/remove indicators >";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // lblCustomIndicators
            // 
            this.lblCustomIndicators.AutoSize = true;
            this.lblCustomIndicators.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomIndicators.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomIndicators.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.lblCustomIndicators.Location = new System.Drawing.Point(-2, 1);
            this.lblCustomIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustomIndicators.Name = "lblCustomIndicators";
            this.lblCustomIndicators.Size = new System.Drawing.Size(143, 21);
            this.lblCustomIndicators.TabIndex = 21;
            this.lblCustomIndicators.Text = "Custom Indicators";
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Size = new System.Drawing.Size(722, 6);
            this.hrTop.TabIndex = 16;
            // 
            // tblTitle
            // 
            this.tblTitle.AutoSize = true;
            this.tblTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTitle.ColumnCount = 2;
            this.tblTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTitle.Controls.Add(this.lblDiseaseType, 0, 0);
            this.tblTitle.Controls.Add(this.lblTitle, 1, 0);
            this.tblTitle.Location = new System.Drawing.Point(23, 15);
            this.tblTitle.Name = "tblTitle";
            this.tblTitle.RowCount = 1;
            this.tblTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitle.Size = new System.Drawing.Size(223, 30);
            this.tblTitle.TabIndex = 61;
            // 
            // lblDiseaseType
            // 
            this.lblDiseaseType.AutoSize = true;
            this.lblDiseaseType.BackColor = System.Drawing.Color.Transparent;
            this.lblDiseaseType.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiseaseType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.lblDiseaseType.Location = new System.Drawing.Point(0, 0);
            this.lblDiseaseType.Margin = new System.Windows.Forms.Padding(0);
            this.lblDiseaseType.Name = "lblDiseaseType";
            this.lblDiseaseType.Size = new System.Drawing.Size(45, 30);
            this.lblDiseaseType.TabIndex = 44;
            this.lblDiseaseType.Tag = "CM";
            this.lblDiseaseType.Text = "CM";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.lblTitle.Location = new System.Drawing.Point(45, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(178, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "DiseaseDistribution";
            // 
            // IndicatorTypeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblTitle);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.hrTop);
            this.Name = "IndicatorTypeEdit";
            this.Size = new System.Drawing.Size(722, 420);
            this.Tag = "SurveyInformation";
            this.Load += new System.EventHandler(this.IndicatorTypeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tblTitle.ResumeLayout(false);
            this.tblTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvIndicators;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.TextBox tbName;
        private Controls.HR hrTop;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel panel1;
        private Controls.FieldLink fieldLink1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tblTitle;
        private System.Windows.Forms.Label lblDiseaseType;
        private System.Windows.Forms.Label lblTitle;

    }
}
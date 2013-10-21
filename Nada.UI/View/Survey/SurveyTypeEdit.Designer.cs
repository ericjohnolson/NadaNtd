namespace Nada.UI.View.Survey
{
    partial class SurveyTypeEdit
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
            this.components = new System.ComponentModel.Container();
            this.lvIndicators = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tbName = new System.Windows.Forms.TextBox();
            this.bsSurveyType = new System.Windows.Forms.BindingSource(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pnlName = new System.Windows.Forms.Panel();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurveyType)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.lvIndicators.Location = new System.Drawing.Point(2, 37);
            this.lvIndicators.Name = "lvIndicators";
            this.lvIndicators.ShowGroups = false;
            this.lvIndicators.Size = new System.Drawing.Size(554, 144);
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
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurveyType, "SurveyTypeName", true));
            this.tbName.Location = new System.Drawing.Point(0, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 20);
            this.tbName.TabIndex = 0;
            // 
            // bsSurveyType
            // 
            this.bsSurveyType.DataSource = typeof(Nada.Model.Survey.SurveyType);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(187, 28);
            this.lblTitle.TabIndex = 17;
            this.lblTitle.Tag = "SurveyInformation";
            this.lblTitle.Text = "Survey Information";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 51);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(565, 283);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 252);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(178, 31);
            this.tableLayoutPanel4.TabIndex = 47;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(97, 3);
            this.btnCancel.MinimumSize = new System.Drawing.Size(78, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(78, 25);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Tag = "Cancel";
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.MinimumSize = new System.Drawing.Size(78, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSave.Size = new System.Drawing.Size(78, 25);
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
            this.pnlName.Size = new System.Drawing.Size(161, 42);
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
            this.h3Label1.Size = new System.Drawing.Size(45, 16);
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
            this.panel1.Location = new System.Drawing.Point(3, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 184);
            this.panel1.TabIndex = 2;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(2, 22);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(106, 12);
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
            this.lblCustomIndicators.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.lblCustomIndicators.Location = new System.Drawing.Point(-2, 1);
            this.lblCustomIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustomIndicators.Name = "lblCustomIndicators";
            this.lblCustomIndicators.Size = new System.Drawing.Size(143, 21);
            this.lblCustomIndicators.TabIndex = 21;
            this.lblCustomIndicators.Text = "Custom Indicators";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Size = new System.Drawing.Size(619, 5);
            this.hr1.TabIndex = 16;
            // 
            // SurveyTypeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.hr1);
            this.Name = "SurveyTypeEdit";
            this.Size = new System.Drawing.Size(619, 364);
            this.Tag = "SurveyInformation";
            this.Load += new System.EventHandler(this.SurveyTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurveyType)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.BindingSource bsSurveyType;
        private System.Windows.Forms.Label lblTitle;
        private Controls.HR hr1;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel panel1;
        private Controls.FieldLink fieldLink1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;

    }
}
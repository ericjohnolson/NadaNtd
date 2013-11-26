namespace Nada.UI.View.Process
{
    partial class ProcessTypeEdit
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlName = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.lvIndicators = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.bsProcessType = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.h3Required2 = new Nada.UI.Controls.H3Required();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProcessType)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 302);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(178, 31);
            this.tableLayoutPanel4.TabIndex = 48;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.h3Required2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbTypes, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 52);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(565, 333);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // pnlName
            // 
            this.pnlName.AutoSize = true;
            this.pnlName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlName.Controls.Add(this.tbName);
            this.pnlName.Controls.Add(this.h3Required1);
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.Margin = new System.Windows.Forms.Padding(0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(222, 49);
            this.pnlName.TabIndex = 0;
            this.pnlName.Visible = false;
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsProcessType, "TypeName", true));
            this.tbName.Location = new System.Drawing.Point(0, 19);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(219, 20);
            this.tbName.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.fieldLink1);
            this.panel1.Controls.Add(this.lvIndicators);
            this.panel1.Controls.Add(this.lblCustomIndicators);
            this.panel1.Location = new System.Drawing.Point(3, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 187);
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
            this.fieldLink1.Size = new System.Drawing.Size(154, 16);
            this.fieldLink1.TabIndex = 22;
            this.fieldLink1.Tag = "AddIndicatorLink";
            this.fieldLink1.Text = "Add/remove indicators >";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
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
            this.lvIndicators.Location = new System.Drawing.Point(2, 40);
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
            this.olvColumn8.Width = 258;
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
            // lblCustomIndicators
            // 
            this.lblCustomIndicators.AutoSize = true;
            this.lblCustomIndicators.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomIndicators.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomIndicators.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(44)))), ((int)(((byte)(27)))));
            this.lblCustomIndicators.Location = new System.Drawing.Point(-2, 1);
            this.lblCustomIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustomIndicators.Name = "lblCustomIndicators";
            this.lblCustomIndicators.Size = new System.Drawing.Size(143, 21);
            this.lblCustomIndicators.TabIndex = 21;
            this.lblCustomIndicators.Text = "Custom Indicators";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(44)))), ((int)(((byte)(27)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 28);
            this.lblTitle.TabIndex = 50;
            this.lblTitle.Tag = "ProcessIndicators";
            this.lblTitle.Text = "ProcessIndicators";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(44)))), ((int)(((byte)(27)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(44)))), ((int)(((byte)(27)))));
            this.hr1.Size = new System.Drawing.Size(617, 5);
            this.hr1.TabIndex = 49;
            // 
            // bsProcessType
            // 
            this.bsProcessType.DataSource = typeof(Nada.Model.Process.ProcessType);
            // 
            // h3Required1
            // 
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(0, 0);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(58, 16);
            this.h3Required1.TabIndex = 52;
            this.h3Required1.Tag = "Name";
            this.h3Required1.Text = "Name";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Required2
            // 
            this.h3Required2.AutoSize = true;
            this.h3Required2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required2.Location = new System.Drawing.Point(0, 49);
            this.h3Required2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required2.Name = "h3Required2";
            this.h3Required2.Size = new System.Drawing.Size(53, 16);
            this.h3Required2.TabIndex = 53;
            this.h3Required2.Tag = "Type";
            this.h3Required2.Text = "Type";
            this.h3Required2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbTypes
            // 
            this.cbTypes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsProcessType, "CategoryName", true));
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(3, 68);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(216, 21);
            this.cbTypes.TabIndex = 54;
            // 
            // ProcessTypeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.hr1);
            this.Name = "ProcessTypeEdit";
            this.Size = new System.Drawing.Size(617, 393);
            this.Tag = "ProcessIndicators";
            this.Load += new System.EventHandler(this.ProcessTypeView_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProcessType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsProcessType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Panel panel1;
        private Controls.FieldLink fieldLink1;
        private BrightIdeasSoftware.ObjectListView lvIndicators;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.Label lblTitle;
        private Controls.HR hr1;
        private Controls.H3Required h3Required1;
        private Controls.H3Required h3Required2;
        private System.Windows.Forms.ComboBox cbTypes;

    }
}
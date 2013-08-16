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
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkNewIndicator = new System.Windows.Forms.LinkLabel();
            this.lvIndicators = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.bsSurveyType = new System.Windows.Forms.BindingSource(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurveyType)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(547, 352);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 25);
            this.btnSave.TabIndex = 25;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonGroupBox2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(19, 101);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.panel2);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(587, 195);
            this.kryptonGroupBox2.TabIndex = 29;
            this.kryptonGroupBox2.Text = "Custom Indicators";
            this.kryptonGroupBox2.Values.Heading = "Custom Indicators";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lnkNewIndicator);
            this.panel2.Controls.Add(this.lvIndicators);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(585, 174);
            this.panel2.TabIndex = 1;
            // 
            // lnkNewIndicator
            // 
            this.lnkNewIndicator.AutoSize = true;
            this.lnkNewIndicator.Location = new System.Drawing.Point(3, 4);
            this.lnkNewIndicator.Name = "lnkNewIndicator";
            this.lnkNewIndicator.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lnkNewIndicator.Size = new System.Drawing.Size(60, 18);
            this.lnkNewIndicator.TabIndex = 9;
            this.lnkNewIndicator.TabStop = true;
            this.lnkNewIndicator.Text = "Add New...";
            this.lnkNewIndicator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNewIndicator_LinkClicked);
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
            this.lvIndicators.Location = new System.Drawing.Point(4, 25);
            this.lvIndicators.Name = "lvIndicators";
            this.lvIndicators.ShowGroups = false;
            this.lvIndicators.ShowHeaderInAllViews = false;
            this.lvIndicators.Size = new System.Drawing.Size(576, 144);
            this.lvIndicators.TabIndex = 5;
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
            this.olvColumn10.AspectName = "SortOrder";
            this.olvColumn10.CellPadding = null;
            this.olvColumn10.IsEditable = false;
            this.olvColumn10.Text = "Sort Order";
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
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurveyType, "SurveyTypeName", true));
            this.textBox3.Location = new System.Drawing.Point(19, 75);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(158, 20);
            this.textBox3.TabIndex = 28;
            // 
            // bsSurveyType
            // 
            this.bsSurveyType.DataSource = typeof(Nada.Model.Survey.SurveyType);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 39);
            this.label3.TabIndex = 26;
            this.label3.Text = "Survey Type";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(462, 352);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(69, 25);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SurveyTypeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(628, 389);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.kryptonGroupBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Name = "SurveyTypeEdit";
            this.Text = "Edit Survey Type";
            this.Load += new System.EventHandler(this.SurveyTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvIndicators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurveyType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lnkNewIndicator;
        private BrightIdeasSoftware.ObjectListView lvIndicators;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.BindingSource bsSurveyType;

    }
}
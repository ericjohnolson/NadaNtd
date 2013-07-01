namespace Nada.UI.View
{
    partial class CountryView
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bsCountry = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkEdit = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCountryName = new System.Windows.Forms.Label();
            this.lvDemos = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label3 = new System.Windows.Forms.Label();
            this.grpChildren = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnImportChildren = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnImportChildDemos = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lvChildren = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAddChild = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvDemos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpChildren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpChildren.Panel)).BeginInit();
            this.grpChildren.Panel.SuspendLayout();
            this.grpChildren.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvChildren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ISO Code:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCountry, "IsoCode", true));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(192, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "ISO";
            // 
            // bsCountry
            // 
            this.bsCountry.DataSource = typeof(Nada.Model.Country);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lnkEdit, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCountryName, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 51);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(254, 18);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lnkEdit
            // 
            this.lnkEdit.AutoSize = true;
            this.lnkEdit.Location = new System.Drawing.Point(226, 0);
            this.lnkEdit.Name = "lnkEdit";
            this.lnkEdit.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lnkEdit.Size = new System.Drawing.Size(25, 18);
            this.lnkEdit.TabIndex = 8;
            this.lnkEdit.TabStop = true;
            this.lnkEdit.Text = "Edit";
            this.lnkEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEdit_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Country Code:";
            // 
            // lblCountryName
            // 
            this.lblCountryName.AutoSize = true;
            this.lblCountryName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCountry, "CountryCode", true));
            this.lblCountryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountryName.Location = new System.Drawing.Point(83, 0);
            this.lblCountryName.Name = "lblCountryName";
            this.lblCountryName.Size = new System.Drawing.Size(41, 13);
            this.lblCountryName.TabIndex = 5;
            this.lblCountryName.Text = "CODE";
            // 
            // lvDemos
            // 
            this.lvDemos.AllColumns.Add(this.olvColumn5);
            this.lvDemos.AllColumns.Add(this.olvColumn6);
            this.lvDemos.AllColumns.Add(this.olvColumn7);
            this.lvDemos.AllColumns.Add(this.olvColumn8);
            this.lvDemos.AllColumns.Add(this.olvColumn9);
            this.lvDemos.AllColumns.Add(this.olvColumn10);
            this.lvDemos.AllColumns.Add(this.olvColumn1);
            this.lvDemos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10,
            this.olvColumn1});
            this.lvDemos.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDemos.Location = new System.Drawing.Point(7, 34);
            this.lvDemos.Name = "lvDemos";
            this.lvDemos.ShowGroups = false;
            this.lvDemos.ShowHeaderInAllViews = false;
            this.lvDemos.Size = new System.Drawing.Size(795, 145);
            this.lvDemos.TabIndex = 2;
            this.lvDemos.UseCompatibleStateImageBehavior = false;
            this.lvDemos.UseHyperlinks = true;
            this.lvDemos.View = System.Windows.Forms.View.Details;
            this.lvDemos.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvDemos_HyperlinkClicked);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "YearReporting";
            this.olvColumn5.CellPadding = null;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Text = "Year of Reporting";
            this.olvColumn5.Width = 96;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "YearCensus";
            this.olvColumn6.CellPadding = null;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "Year of Census";
            this.olvColumn6.Width = 90;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "YearProjections";
            this.olvColumn7.CellPadding = null;
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Text = "Year of Pop. Projections";
            this.olvColumn7.Width = 128;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "FemalePercent";
            this.olvColumn8.CellPadding = null;
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.Text = "% Females";
            this.olvColumn8.Width = 70;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "MalePercent";
            this.olvColumn9.CellPadding = null;
            this.olvColumn9.IsEditable = false;
            this.olvColumn9.Text = "% Males";
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "AdultsPercent";
            this.olvColumn10.CellPadding = null;
            this.olvColumn10.IsEditable = false;
            this.olvColumn10.Text = "% Adults";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "EditText";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Hyperlink = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Edit";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(663, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(138, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Values.Text = "Add Demo Survey";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCountry, "Name", true));
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "Country";
            // 
            // grpChildren
            // 
            this.grpChildren.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.grpChildren.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.grpChildren.Location = new System.Drawing.Point(10, 284);
            this.grpChildren.Name = "grpChildren";
            // 
            // grpChildren.Panel
            // 
            this.grpChildren.Panel.Controls.Add(this.panel2);
            this.grpChildren.Size = new System.Drawing.Size(807, 203);
            this.grpChildren.TabIndex = 17;
            this.grpChildren.Text = "Regions (Children)";
            this.grpChildren.Values.Heading = "Regions (Children)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnImportChildren);
            this.panel2.Controls.Add(this.btnImportChildDemos);
            this.panel2.Controls.Add(this.lvChildren);
            this.panel2.Controls.Add(this.btnAddChild);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(805, 182);
            this.panel2.TabIndex = 1;
            // 
            // btnImportChildren
            // 
            this.btnImportChildren.Location = new System.Drawing.Point(592, 4);
            this.btnImportChildren.Name = "btnImportChildren";
            this.btnImportChildren.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnImportChildren.Size = new System.Drawing.Size(109, 25);
            this.btnImportChildren.TabIndex = 7;
            this.btnImportChildren.Values.Text = "Import Regions";
            this.btnImportChildren.Click += new System.EventHandler(this.btnImportChildren_Click);
            // 
            // btnImportChildDemos
            // 
            this.btnImportChildDemos.Location = new System.Drawing.Point(436, 4);
            this.btnImportChildDemos.Name = "btnImportChildDemos";
            this.btnImportChildDemos.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnImportChildDemos.Size = new System.Drawing.Size(150, 25);
            this.btnImportChildDemos.TabIndex = 6;
            this.btnImportChildDemos.Values.Text = "Import Demo Surveys";
            this.btnImportChildDemos.Click += new System.EventHandler(this.btnImportChildDemos_Click);
            // 
            // lvChildren
            // 
            this.lvChildren.AllColumns.Add(this.olvColumn2);
            this.lvChildren.AllColumns.Add(this.olvColumn14);
            this.lvChildren.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn14});
            this.lvChildren.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvChildren.Location = new System.Drawing.Point(3, 35);
            this.lvChildren.Name = "lvChildren";
            this.lvChildren.ShowGroups = false;
            this.lvChildren.ShowHeaderInAllViews = false;
            this.lvChildren.Size = new System.Drawing.Size(799, 144);
            this.lvChildren.TabIndex = 5;
            this.lvChildren.UseCompatibleStateImageBehavior = false;
            this.lvChildren.UseHyperlinks = true;
            this.lvChildren.View = System.Windows.Forms.View.Details;
            this.lvChildren.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvChildren_HyperlinkClicked);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Name";
            this.olvColumn2.Width = 96;
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "ViewText";
            this.olvColumn14.CellPadding = null;
            this.olvColumn14.Hyperlink = true;
            this.olvColumn14.IsEditable = false;
            this.olvColumn14.Text = "View";
            // 
            // btnAddChild
            // 
            this.btnAddChild.Enabled = false;
            this.btnAddChild.Location = new System.Drawing.Point(707, 4);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(94, 25);
            this.btnAddChild.TabIndex = 4;
            this.btnAddChild.Values.Text = "Add Region";
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonGroupBox2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(10, 75);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.panel3);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(807, 203);
            this.kryptonGroupBox2.TabIndex = 16;
            this.kryptonGroupBox2.Text = "Demography";
            this.kryptonGroupBox2.Values.Heading = "Demography";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lvDemos);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(805, 182);
            this.panel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(146, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Value";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(60, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Value";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Summary";
            // 
            // CountryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpChildren);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.kryptonGroupBox2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "CountryView";
            this.Size = new System.Drawing.Size(855, 509);
            this.Load += new System.EventHandler(this.CountryView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvDemos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpChildren.Panel)).EndInit();
            this.grpChildren.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpChildren)).EndInit();
            this.grpChildren.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvChildren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvDemos;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource bsCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCountryName;
        private System.Windows.Forms.LinkLabel lnkEdit;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox grpChildren;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImportChildren;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImportChildDemos;
        private BrightIdeasSoftware.ObjectListView lvChildren;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddChild;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

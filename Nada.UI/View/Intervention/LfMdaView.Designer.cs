namespace Nada.UI.View.Intervention
{
    partial class LfMdaView
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
            this.label3 = new System.Windows.Forms.Label();
            this.bsType = new System.Windows.Forms.BindingSource(this.components);
            this.bsIntv = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customIndicatorControl1 = new Nada.UI.View.CustomIndicatorControl();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.distributionMethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkMeds = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbPartners = new System.Windows.Forms.ListBox();
            this.partnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbMeds = new System.Windows.Forms.ListBox();
            this.medicineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lnkDoImport = new System.Windows.Forms.LinkLabel();
            this.lnkCreateImport = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIntv)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distributionMethodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medicineBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsType, "IntvTypeName", true));
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 39);
            this.label3.TabIndex = 5;
            this.label3.Text = "Type Name";
            // 
            // bsType
            // 
            this.bsType.DataSource = typeof(Nada.Model.Intervention.IntvType);
            // 
            // bsIntv
            // 
            this.bsIntv.DataSource = typeof(Nada.Model.Intervention.PcMda);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsIntv, "IntvDate", true));
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 150);
            this.dateTimePicker1.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Value = new System.DateTime(2013, 8, 15, 0, 0, 0, 0);
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsIntv, "Notes", true));
            this.textBox10.Location = new System.Drawing.Point(6, 16);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(578, 70);
            this.textBox10.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Notes";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(6, 93);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 3;
            this.kryptonButton1.Values.Text = "Save";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.lblLastUpdate, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 124);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(136, 53);
            this.tableLayoutPanel4.TabIndex = 39;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsIntv, "UpdatedBy", true));
            this.lblLastUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdate.Location = new System.Drawing.Point(83, 0);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(50, 13);
            this.lblLastUpdate.TabIndex = 1;
            this.lblLastUpdate.Text = "Unsaved";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Last Updated:";
            // 
            // tblNotes
            // 
            this.tblNotes.AutoSize = true;
            this.tblNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNotes.ColumnCount = 1;
            this.tblNotes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNotes.Controls.Add(this.panel1, 0, 1);
            this.tblNotes.Controls.Add(this.customIndicatorControl1, 0, 0);
            this.tblNotes.Location = new System.Drawing.Point(1, 345);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 2;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.Size = new System.Drawing.Size(593, 209);
            this.tblNotes.TabIndex = 40;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.kryptonButton1);
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 180);
            this.panel1.TabIndex = 0;
            // 
            // customIndicatorControl1
            // 
            this.customIndicatorControl1.AutoSize = true;
            this.customIndicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customIndicatorControl1.Location = new System.Drawing.Point(0, 0);
            this.customIndicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.customIndicatorControl1.Name = "customIndicatorControl1";
            this.customIndicatorControl1.Size = new System.Drawing.Size(506, 23);
            this.customIndicatorControl1.TabIndex = 0;
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(7, 92);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl1.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Distribution Method";
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsIntv, "DistributionMethod", true));
            this.comboBox1.DataSource = this.distributionMethodBindingSource;
            this.comboBox1.DisplayMember = "DisplayName";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 189);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // distributionMethodBindingSource
            // 
            this.distributionMethodBindingSource.DataSource = typeof(Nada.Model.Intervention.DistributionMethod);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(113, 173);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(74, 13);
            this.linkLabel2.TabIndex = 44;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "add/remove...";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(61, 213);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(74, 13);
            this.linkLabel3.TabIndex = 9;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "add/remove...";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Partners";
            // 
            // linkMeds
            // 
            this.linkMeds.AutoSize = true;
            this.linkMeds.Location = new System.Drawing.Point(283, 213);
            this.linkMeds.Name = "linkMeds";
            this.linkMeds.Size = new System.Drawing.Size(74, 13);
            this.linkMeds.TabIndex = 10;
            this.linkMeds.TabStop = true;
            this.linkMeds.Text = "add/remove...";
            this.linkMeds.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMeds_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Medicines";
            // 
            // lbPartners
            // 
            this.lbPartners.DataSource = this.partnerBindingSource;
            this.lbPartners.DisplayMember = "DisplayName";
            this.lbPartners.FormattingEnabled = true;
            this.lbPartners.Location = new System.Drawing.Point(12, 229);
            this.lbPartners.Name = "lbPartners";
            this.lbPartners.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbPartners.Size = new System.Drawing.Size(198, 95);
            this.lbPartners.TabIndex = 3;
            // 
            // partnerBindingSource
            // 
            this.partnerBindingSource.DataSource = typeof(Nada.Model.Partner);
            // 
            // lbMeds
            // 
            this.lbMeds.DataSource = this.medicineBindingSource;
            this.lbMeds.DisplayMember = "DisplayName";
            this.lbMeds.FormattingEnabled = true;
            this.lbMeds.Location = new System.Drawing.Point(225, 229);
            this.lbMeds.Name = "lbMeds";
            this.lbMeds.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbMeds.Size = new System.Drawing.Size(199, 95);
            this.lbMeds.TabIndex = 4;
            // 
            // medicineBindingSource
            // 
            this.medicineBindingSource.DataSource = typeof(Nada.Model.Medicine);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lnkDoImport
            // 
            this.lnkDoImport.AutoSize = true;
            this.lnkDoImport.Location = new System.Drawing.Point(236, 48);
            this.lnkDoImport.Name = "lnkDoImport";
            this.lnkDoImport.Size = new System.Drawing.Size(97, 13);
            this.lnkDoImport.TabIndex = 8;
            this.lnkDoImport.TabStop = true;
            this.lnkDoImport.Text = "Upload import file...";
            this.lnkDoImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDoImport_LinkClicked);
            // 
            // lnkCreateImport
            // 
            this.lnkCreateImport.AutoSize = true;
            this.lnkCreateImport.Location = new System.Drawing.Point(136, 48);
            this.lnkCreateImport.Name = "lnkCreateImport";
            this.lnkCreateImport.Size = new System.Drawing.Size(94, 13);
            this.lnkCreateImport.TabIndex = 7;
            this.lnkCreateImport.TabStop = true;
            this.lnkCreateImport.Text = "Create import file...";
            this.lnkCreateImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateImport_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 48);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(123, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Add/remove indicators...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEdit_LinkClicked);
            // 
            // LfMdaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkDoImport);
            this.Controls.Add(this.lnkCreateImport);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lbMeds);
            this.Controls.Add(this.lbPartners);
            this.Controls.Add(this.linkMeds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.tblNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "LfMdaView";
            this.Size = new System.Drawing.Size(597, 557);
            this.Load += new System.EventHandler(this.base_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIntv)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distributionMethodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medicineBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label18;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.BindingSource bsIntv;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsType;
        private CustomIndicatorControl customIndicatorControl1;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource distributionMethodBindingSource;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkMeds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbPartners;
        private System.Windows.Forms.ListBox lbMeds;
        private System.Windows.Forms.BindingSource partnerBindingSource;
        private System.Windows.Forms.BindingSource medicineBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.LinkLabel lnkDoImport;
        private System.Windows.Forms.LinkLabel lnkCreateImport;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

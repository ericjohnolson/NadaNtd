namespace Nada.UI.View.Intervention
{
    partial class IntvBaseView
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
            this.label11 = new System.Windows.Forms.Label();
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.lnkCreateImport = new System.Windows.Forms.LinkLabel();
            this.lnkDoImport = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIntv)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.label3.TabIndex = 2;
            this.label3.Text = "Type Name";
            // 
            // bsType
            // 
            this.bsType.DataSource = typeof(Nada.Model.Intervention.IntvType);
            // 
            // bsIntv
            // 
            this.bsIntv.DataSource = typeof(Nada.Model.Intervention.IntvBase);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsIntv, "IntvDate", true));
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 138);
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
            this.textBox10.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 36;
            this.label18.Text = "Notes";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(6, 93);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 0;
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
            this.lblLastUpdate.TabIndex = 40;
            this.lblLastUpdate.Text = "Unsaved";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 4;
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
            this.tblNotes.Location = new System.Drawing.Point(1, 164);
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
            this.customIndicatorControl1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 48);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(123, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Add/remove indicators...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(7, 80);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl1.TabIndex = 0;
            // 
            // lnkCreateImport
            // 
            this.lnkCreateImport.AutoSize = true;
            this.lnkCreateImport.Location = new System.Drawing.Point(136, 48);
            this.lnkCreateImport.Name = "lnkCreateImport";
            this.lnkCreateImport.Size = new System.Drawing.Size(94, 13);
            this.lnkCreateImport.TabIndex = 4;
            this.lnkCreateImport.TabStop = true;
            this.lnkCreateImport.Text = "Create import file...";
            this.lnkCreateImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateImport_LinkClicked);
            // 
            // lnkDoImport
            // 
            this.lnkDoImport.AutoSize = true;
            this.lnkDoImport.Location = new System.Drawing.Point(236, 48);
            this.lnkDoImport.Name = "lnkDoImport";
            this.lnkDoImport.Size = new System.Drawing.Size(97, 13);
            this.lnkDoImport.TabIndex = 5;
            this.lnkDoImport.TabStop = true;
            this.lnkDoImport.Text = "Upload import file...";
            this.lnkDoImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDoImport_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // IntvBaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkDoImport);
            this.Controls.Add(this.lnkCreateImport);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.tblNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "IntvBaseView";
            this.Size = new System.Drawing.Size(597, 376);
            this.Load += new System.EventHandler(this.base_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIntv)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource bsType;
        private CustomIndicatorControl customIndicatorControl1;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private System.Windows.Forms.LinkLabel lnkCreateImport;
        private System.Windows.Forms.LinkLabel lnkDoImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

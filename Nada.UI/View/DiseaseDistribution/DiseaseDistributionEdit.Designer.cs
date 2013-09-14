namespace Nada.UI.View
{
    partial class DiseaseDistributionEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lnkDoImport = new System.Windows.Forms.LinkLabel();
            this.lnkCreateImport = new System.Windows.Forms.LinkLabel();
            this.lnkIndicators = new System.Windows.Forms.LinkLabel();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.bsDiseaseDistribution = new System.Windows.Forms.BindingSource(this.components);
            this.customIndicatorControl1 = new Nada.UI.View.CustomIndicatorControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tblNotes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDiseaseDistribution)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 92);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(323, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "Disease Distribution";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(86, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(69, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lnkDoImport
            // 
            this.lnkDoImport.AutoSize = true;
            this.lnkDoImport.Location = new System.Drawing.Point(245, 48);
            this.lnkDoImport.Name = "lnkDoImport";
            this.lnkDoImport.Size = new System.Drawing.Size(97, 13);
            this.lnkDoImport.TabIndex = 43;
            this.lnkDoImport.TabStop = true;
            this.lnkDoImport.Text = "Upload import file...";
            this.lnkDoImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDoImport_LinkClicked_1);
            // 
            // lnkCreateImport
            // 
            this.lnkCreateImport.AutoSize = true;
            this.lnkCreateImport.Location = new System.Drawing.Point(145, 48);
            this.lnkCreateImport.Name = "lnkCreateImport";
            this.lnkCreateImport.Size = new System.Drawing.Size(94, 13);
            this.lnkCreateImport.TabIndex = 42;
            this.lnkCreateImport.TabStop = true;
            this.lnkCreateImport.Text = "Create import file...";
            this.lnkCreateImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateImport_LinkClicked_1);
            // 
            // lnkIndicators
            // 
            this.lnkIndicators.AutoSize = true;
            this.lnkIndicators.Location = new System.Drawing.Point(16, 48);
            this.lnkIndicators.Name = "lnkIndicators";
            this.lnkIndicators.Size = new System.Drawing.Size(123, 13);
            this.lnkIndicators.TabIndex = 41;
            this.lnkIndicators.TabStop = true;
            this.lnkIndicators.Text = "Add/remove indicators...";
            this.lnkIndicators.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIndicators_LinkClicked);
            // 
            // tblNotes
            // 
            this.tblNotes.AutoSize = true;
            this.tblNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNotes.ColumnCount = 1;
            this.tblNotes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNotes.Controls.Add(this.panel1, 0, 1);
            this.tblNotes.Controls.Add(this.customIndicatorControl1, 0, 0);
            this.tblNotes.Location = new System.Drawing.Point(12, 76);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 2;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.Size = new System.Drawing.Size(593, 209);
            this.tblNotes.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 180);
            this.panel1.TabIndex = 0;
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
            this.lblLastUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
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
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDiseaseDistribution, "Notes", true));
            this.textBox10.Location = new System.Drawing.Point(6, 16);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(578, 70);
            this.textBox10.TabIndex = 0;
            // 
            // bsDiseaseDistribution
            // 
            this.bsDiseaseDistribution.DataSource = typeof(Nada.Model.DiseaseDistribution);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DiseaseDistributionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(706, 568);
            this.Controls.Add(this.lnkDoImport);
            this.Controls.Add(this.lnkCreateImport);
            this.Controls.Add(this.lnkIndicators);
            this.Controls.Add(this.tblNotes);
            this.Controls.Add(this.label3);
            this.Name = "DiseaseDistributionEdit";
            this.Text = "Edit Disease Distribution";
            this.Load += new System.EventHandler(this.DiseaseDistributionEdit_Load);
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDiseaseDistribution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.LinkLabel lnkDoImport;
        private System.Windows.Forms.LinkLabel lnkCreateImport;
        private System.Windows.Forms.LinkLabel lnkIndicators;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox10;
        private CustomIndicatorControl customIndicatorControl1;
        private System.Windows.Forms.BindingSource bsDiseaseDistribution;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}
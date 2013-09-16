namespace Nada.UI.View.Survey
{
    partial class SurveyBaseView
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
            this.bsSurvey = new System.Windows.Forms.BindingSource(this.components);
            this.sentinelSiteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.customIndicatorControl1 = new Nada.UI.View.CustomIndicatorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsType, "SurveyTypeName", true));
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(297, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Survey Type Name";
            // 
            // bsType
            // 
            this.bsType.DataSource = typeof(Nada.Model.Survey.SurveyType);
            // 
            // bsSurvey
            // 
            this.bsSurvey.DataSource = typeof(Nada.Model.Survey.LfMfPrevalence);
            // 
            // sentinelSiteBindingSource
            // 
            this.sentinelSiteBindingSource.DataSource = typeof(Nada.Model.Survey.SentinelSite);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Start date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsSurvey, "StartDate", true));
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 126);
            this.dateTimePicker1.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Value = new System.DateTime(2013, 8, 15, 0, 0, 0, 0);
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Notes", true));
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
            this.kryptonButton1.TabIndex = 1;
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
            this.lblLastUpdate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "UpdatedBy", true));
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
            this.tblNotes.Controls.Add(this.customIndicatorControl1, 0, 0);
            this.tblNotes.Controls.Add(this.panel1, 0, 1);
            this.tblNotes.Location = new System.Drawing.Point(1, 152);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 2;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.Size = new System.Drawing.Size(593, 227);
            this.tblNotes.TabIndex = 40;
            // 
            // customIndicatorControl1
            // 
            this.customIndicatorControl1.AutoSize = true;
            this.customIndicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customIndicatorControl1.Location = new System.Drawing.Point(0, 0);
            this.customIndicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.customIndicatorControl1.Name = "customIndicatorControl1";
            this.customIndicatorControl1.Size = new System.Drawing.Size(146, 41);
            this.customIndicatorControl1.TabIndex = 42;
            this.customIndicatorControl1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.kryptonButton1);
            this.panel1.Location = new System.Drawing.Point(3, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 180);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 39);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(114, 13);
            this.linkLabel1.TabIndex = 25;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Add/remove indicators";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(4, 69);
            this.adminLevelPickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(187, 39);
            this.adminLevelPickerControl1.TabIndex = 41;
            this.adminLevelPickerControl1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // SurveyBaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.tblNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "SurveyBaseView";
            this.Size = new System.Drawing.Size(597, 382);
            this.Load += new System.EventHandler(this.LfPrevalence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource bsSurvey;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource sentinelSiteBindingSource;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource bsType;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private CustomIndicatorControl customIndicatorControl1;
    }
}

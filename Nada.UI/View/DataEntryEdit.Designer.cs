﻿namespace Nada.UI.View.DiseaseDistribution
{
    partial class DataEntryEdit
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdminLevel = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.tblTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblDiseaseType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hr5 = new Nada.UI.Controls.HR();
            this.hr4 = new Nada.UI.Controls.HR();
            this.tblEdit = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.indicatorControl1 = new Nada.UI.View.IndicatorControl();
            this.statCalculator1 = new Nada.UI.Controls.StatCalculator();
            this.nadaLabel2 = new Nada.UI.Controls.H3Label();
            this.nadaLabel1 = new Nada.UI.Controls.H3Label();
            this.adminLevelPickerControl2 = new Nada.UI.View.AdminLevelPickerControl();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblTitle.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tblEdit.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAdminLevel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLocation, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 73);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 21);
            this.tableLayoutPanel2.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(77, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "*";
            // 
            // lblAdminLevel
            // 
            this.lblAdminLevel.AutoSize = true;
            this.lblAdminLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAdminLevel.Location = new System.Drawing.Point(90, 0);
            this.lblAdminLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblAdminLevel.Name = "lblAdminLevel";
            this.lblAdminLevel.Size = new System.Drawing.Size(104, 21);
            this.lblAdminLevel.TabIndex = 4;
            this.lblAdminLevel.Text = "Not selected";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(0, 0);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(77, 21);
            this.lblLocation.TabIndex = 16;
            this.lblLocation.Tag = "Location";
            this.lblLocation.Text = "Location:";
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
            this.tblTitle.Location = new System.Drawing.Point(13, 15);
            this.tblTitle.Name = "tblTitle";
            this.tblTitle.RowCount = 1;
            this.tblTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitle.Size = new System.Drawing.Size(205, 30);
            this.tblTitle.TabIndex = 60;
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
            this.lblDiseaseType.Size = new System.Drawing.Size(40, 30);
            this.lblDiseaseType.TabIndex = 44;
            this.lblDiseaseType.Tag = "EO";
            this.lblDiseaseType.Text = "EO";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "GenericDataEntry";
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(867, 8);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 59;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(6);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Size = new System.Drawing.Size(946, 6);
            this.hrTop.TabIndex = 56;
            this.hrTop.TabStop = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Location = new System.Drawing.Point(13, 8);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel6.TabIndex = 55;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.hr5, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.hr4, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.tblEdit, 0, 9);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.indicatorControl1, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.statCalculator1, 0, 6);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(12, 101);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 10;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(931, 424);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // hr5
            // 
            this.hr5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr5.ForeColor = System.Drawing.Color.Gray;
            this.hr5.Location = new System.Drawing.Point(3, 321);
            this.hr5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.hr5.Name = "hr5";
            this.hr5.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr5.Size = new System.Drawing.Size(920, 1);
            this.hr5.TabIndex = 57;
            this.hr5.TabStop = false;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(3, 96);
            this.hr4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr4.Size = new System.Drawing.Size(920, 1);
            this.hr4.TabIndex = 47;
            this.hr4.TabStop = false;
            // 
            // tblEdit
            // 
            this.tblEdit.AutoSize = true;
            this.tblEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblEdit.ColumnCount = 1;
            this.tblEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEdit.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tblEdit.Location = new System.Drawing.Point(3, 336);
            this.tblEdit.Name = "tblEdit";
            this.tblEdit.RowCount = 2;
            this.tblEdit.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEdit.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblEdit.Size = new System.Drawing.Size(210, 85);
            this.tblEdit.TabIndex = 7;
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
            this.tableLayoutPanel4.Controls.Add(this.c1Button2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.c1Button1, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(204, 79);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // c1Button2
            // 
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.Location = new System.Drawing.Point(111, 3);
            this.c1Button2.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button2.Size = new System.Drawing.Size(90, 27);
            this.c1Button2.TabIndex = 62;
            this.c1Button2.Tag = "Cancel";
            this.c1Button2.Text = "Cancel";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button2.Click += new System.EventHandler(this.cancel_Click);
            // 
            // c1Button1
            // 
            this.c1Button1.AutoSize = true;
            this.c1Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button1.Location = new System.Drawing.Point(3, 3);
            this.c1Button1.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button1.Size = new System.Drawing.Size(90, 27);
            this.c1Button1.TabIndex = 2;
            this.c1Button1.Tag = "Save";
            this.c1Button1.Text = "Save";
            this.c1Button1.UseVisualStyleBackColor = true;
            this.c1Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1Button1.Click += new System.EventHandler(this.save_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.Controls.Add(this.tbNotes, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.h3Label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 111);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(650, 136);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(3, 21);
            this.tbNotes.MaxLength = 255;
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbNotes.Size = new System.Drawing.Size(644, 112);
            this.tbNotes.TabIndex = 0;
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(0, 0);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(40, 18);
            this.h3Label3.TabIndex = 5;
            this.h3Label3.TabStop = false;
            this.h3Label3.Tag = "Notes";
            this.h3Label3.Text = "Notes";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // indicatorControl1
            // 
            this.indicatorControl1.AutoSize = true;
            this.indicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.indicatorControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.indicatorControl1.BackColor = System.Drawing.Color.White;
            this.indicatorControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorControl1.Location = new System.Drawing.Point(0, 6);
            this.indicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.indicatorControl1.Name = "indicatorControl1";
            this.indicatorControl1.Size = new System.Drawing.Size(923, 80);
            this.indicatorControl1.TabIndex = 58;
            this.indicatorControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            // 
            // statCalculator1
            // 
            this.statCalculator1.AutoSize = true;
            this.statCalculator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statCalculator1.BackColor = System.Drawing.Color.White;
            this.statCalculator1.Calc = null;
            this.statCalculator1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statCalculator1.Location = new System.Drawing.Point(3, 253);
            this.statCalculator1.Name = "statCalculator1";
            this.statCalculator1.Size = new System.Drawing.Size(925, 55);
            this.statCalculator1.TabIndex = 59;
            this.statCalculator1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            // 
            // nadaLabel2
            // 
            this.nadaLabel2.AutoSize = true;
            this.nadaLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel2.Location = new System.Drawing.Point(828, 80);
            this.nadaLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel2.Name = "nadaLabel2";
            this.nadaLabel2.Size = new System.Drawing.Size(12, 18);
            this.nadaLabel2.TabIndex = 46;
            this.nadaLabel2.TabStop = false;
            this.nadaLabel2.Text = "*";
            this.nadaLabel2.TextColor = System.Drawing.Color.Red;
            // 
            // nadaLabel1
            // 
            this.nadaLabel1.AutoSize = true;
            this.nadaLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel1.Location = new System.Drawing.Point(841, 76);
            this.nadaLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel1.Name = "nadaLabel1";
            this.nadaLabel1.Size = new System.Drawing.Size(58, 18);
            this.nadaLabel1.TabIndex = 45;
            this.nadaLabel1.TabStop = false;
            this.nadaLabel1.Tag = "Required";
            this.nadaLabel1.Text = "Required";
            this.nadaLabel1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // adminLevelPickerControl2
            // 
            this.adminLevelPickerControl2.AutoSize = true;
            this.adminLevelPickerControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLevelPickerControl2.Location = new System.Drawing.Point(0, 0);
            this.adminLevelPickerControl2.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl2.Name = "adminLevelPickerControl2";
            this.adminLevelPickerControl2.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl2.TabIndex = 0;
            this.adminLevelPickerControl2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // DataEntryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tblTitle);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.hrTop);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.nadaLabel2);
            this.Controls.Add(this.nadaLabel1);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "DataEntryEdit";
            this.Size = new System.Drawing.Size(946, 528);
            this.Load += new System.EventHandler(this.DataEntryEdit_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblTitle.ResumeLayout(false);
            this.tblTitle.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tblEdit.ResumeLayout(false);
            this.tblEdit.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tblEdit;
        private AdminLevelPickerControl adminLevelPickerControl2;
        private Controls.H3Label nadaLabel1;
        private Controls.H3Label nadaLabel2;
        private Controls.HR hr4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Controls.HR hrTop;
        private Controls.HR hr5;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox tbNotes;
        private Controls.H3Label h3Label3;
        private IndicatorControl indicatorControl1;
        private Controls.StatCalculator statCalculator1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDiseaseType;
        private System.Windows.Forms.TableLayoutPanel tblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdminLevel;
        private System.Windows.Forms.Label lblLocation;
        private C1.Win.C1Input.C1Button c1Button2;
        private C1.Win.C1Input.C1Button c1Button1;
    }
}
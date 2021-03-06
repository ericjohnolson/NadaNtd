﻿namespace Nada.UI.View
{
    partial class SettingsDashboard
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.c1Button3 = new C1.Win.C1Input.C1Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.countryDemographyView1 = new Nada.UI.View.Demography.CountryDemographyView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.h3bLabel1 = new Nada.UI.Controls.H3bLabel();
            this.adminLevelTypesControl1 = new Nada.UI.Controls.AdminLevelTypesControl();
            this.countryView1 = new Nada.UI.View.Demography.CountryView();
            this.pnlLf = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.diseasePickerControl1 = new Nada.UI.View.DiseasePickerControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lnkAddUser = new Nada.UI.Controls.H3Link();
            this.lvUsers = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn16 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn17 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn18 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.h3bLabel3 = new Nada.UI.Controls.H3bLabel();
            this.btnSaveLog = new C1.Win.C1Input.C1Button();
            this.h3bLabel2 = new Nada.UI.Controls.H3bLabel();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.label21 = new System.Windows.Forms.Label();
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvUsers)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "log";
            this.saveFileDialog1.FileName = "NationalDatabaseLog.log";
            this.saveFileDialog1.Filter = " (*.log)|*.log";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.c1Button1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.c1Button3, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(740, 630);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(204, 79);
            this.tableLayoutPanel4.TabIndex = 69;
            // 
            // c1Button1
            // 
            this.c1Button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.c1Button1.AutoSize = true;
            this.c1Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button1.Location = new System.Drawing.Point(111, 3);
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
            this.c1Button1.Click += new System.EventHandler(this.doSave_Click);
            // 
            // c1Button3
            // 
            this.c1Button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.c1Button3.AutoSize = true;
            this.c1Button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button3.Location = new System.Drawing.Point(3, 3);
            this.c1Button3.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button3.Name = "c1Button3";
            this.c1Button3.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button3.Size = new System.Drawing.Size(90, 27);
            this.c1Button3.TabIndex = 62;
            this.c1Button3.Tag = "Cancel";
            this.c1Button3.Text = "Cancel";
            this.c1Button3.UseVisualStyleBackColor = true;
            this.c1Button3.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            this.c1Button3.Click += new System.EventHandler(this.doCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(18, 67);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(17, 12, 3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(926, 557);
            this.tabControl1.TabIndex = 68;
            this.tabControl1.Tag = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.countryDemographyView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(918, 529);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Tag = "CountryStatistics";
            this.tabPage5.Text = "CountryStatistics";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // countryDemographyView1
            // 
            this.countryDemographyView1.AutoSize = true;
            this.countryDemographyView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.countryDemographyView1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.countryDemographyView1.BackColor = System.Drawing.Color.White;
            this.countryDemographyView1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryDemographyView1.Location = new System.Drawing.Point(13, 12);
            this.countryDemographyView1.Name = "countryDemographyView1";
            this.countryDemographyView1.Size = new System.Drawing.Size(575, 348);
            this.countryDemographyView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Controls.Add(this.countryView1);
            this.tabPage2.Controls.Add(this.pnlLf);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(918, 529);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "CountrySettings";
            this.tabPage2.Text = "CountrySettings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.h3bLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.adminLevelTypesControl1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 269);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(0, 0);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(138, 16);
            this.h3bLabel1.TabIndex = 2;
            this.h3bLabel1.Tag = "AdminLevelTypeEditAdd";
            this.h3bLabel1.Text = "AdminLevelTypeEditAdd";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // adminLevelTypesControl1
            // 
            this.adminLevelTypesControl1.AutoSize = true;
            this.adminLevelTypesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelTypesControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLevelTypesControl1.Location = new System.Drawing.Point(3, 19);
            this.adminLevelTypesControl1.Name = "adminLevelTypesControl1";
            this.adminLevelTypesControl1.Size = new System.Drawing.Size(586, 247);
            this.adminLevelTypesControl1.TabIndex = 1;
            // 
            // countryView1
            // 
            this.countryView1.AutoSize = true;
            this.countryView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.countryView1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.countryView1.BackColor = System.Drawing.Color.White;
            this.countryView1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryView1.Location = new System.Drawing.Point(8, 8);
            this.countryView1.Name = "countryView1";
            this.countryView1.Size = new System.Drawing.Size(305, 54);
            this.countryView1.TabIndex = 4;
            // 
            // pnlLf
            // 
            this.pnlLf.AutoScroll = true;
            this.pnlLf.AutoSize = true;
            this.pnlLf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLf.Location = new System.Drawing.Point(3, 3);
            this.pnlLf.Name = "pnlLf";
            this.pnlLf.Size = new System.Drawing.Size(0, 0);
            this.pnlLf.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.diseasePickerControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(918, 529);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Diseases";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // diseasePickerControl1
            // 
            this.diseasePickerControl1.AutoSize = true;
            this.diseasePickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diseasePickerControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseasePickerControl1.Location = new System.Drawing.Point(12, 3);
            this.diseasePickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.diseasePickerControl1.Name = "diseasePickerControl1";
            this.diseasePickerControl1.Size = new System.Drawing.Size(786, 444);
            this.diseasePickerControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lnkAddUser);
            this.tabPage1.Controls.Add(this.lvUsers);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(918, 529);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Users";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lnkAddUser
            // 
            this.lnkAddUser.AutoSize = true;
            this.lnkAddUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkAddUser.BackColor = System.Drawing.Color.Transparent;
            this.lnkAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAddUser.Location = new System.Drawing.Point(12, 11);
            this.lnkAddUser.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lnkAddUser.Name = "lnkAddUser";
            this.lnkAddUser.Size = new System.Drawing.Size(78, 15);
            this.lnkAddUser.TabIndex = 69;
            this.lnkAddUser.Tag = "AddUserLink";
            this.lnkAddUser.Text = "AddUserLink";
            this.lnkAddUser.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkAddUser.ClickOverride += new System.Action(this.lnkAddUser_ClickOverride);
            // 
            // lvUsers
            // 
            this.lvUsers.AllColumns.Add(this.olvColumn1);
            this.lvUsers.AllColumns.Add(this.olvColumn16);
            this.lvUsers.AllColumns.Add(this.olvColumn17);
            this.lvUsers.AllColumns.Add(this.olvColumn18);
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn16,
            this.olvColumn17,
            this.olvColumn18});
            this.lvUsers.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvUsers.Location = new System.Drawing.Point(12, 35);
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.ShowGroups = false;
            this.lvUsers.Size = new System.Drawing.Size(863, 459);
            this.lvUsers.TabIndex = 2;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.UseHyperlinks = true;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lvUsers_HyperlinkClicked);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Username";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Tag = "Username";
            this.olvColumn1.Text = "Username";
            this.olvColumn1.Width = 417;
            // 
            // olvColumn16
            // 
            this.olvColumn16.AspectName = "UpdatedBy";
            this.olvColumn16.CellPadding = null;
            this.olvColumn16.IsEditable = false;
            this.olvColumn16.Tag = "LastUpdate";
            this.olvColumn16.Text = "Last Update";
            this.olvColumn16.Width = 200;
            // 
            // olvColumn17
            // 
            this.olvColumn17.AspectName = "View";
            this.olvColumn17.CellPadding = null;
            this.olvColumn17.Hyperlink = true;
            this.olvColumn17.IsEditable = false;
            this.olvColumn17.Tag = "View";
            this.olvColumn17.Text = "View";
            this.olvColumn17.Width = 85;
            // 
            // olvColumn18
            // 
            this.olvColumn18.AspectName = "Delete";
            this.olvColumn18.CellPadding = null;
            this.olvColumn18.Hyperlink = true;
            this.olvColumn18.Tag = "Delete";
            this.olvColumn18.Text = "Delete";
            this.olvColumn18.Width = 71;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.h3bLabel3);
            this.tabPage4.Controls.Add(this.btnSaveLog);
            this.tabPage4.Controls.Add(this.h3bLabel2);
            this.tabPage4.Controls.Add(this.c1Button2);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(918, 529);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Database";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // h3bLabel3
            // 
            this.h3bLabel3.AutoSize = true;
            this.h3bLabel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel3.Location = new System.Drawing.Point(13, 13);
            this.h3bLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel3.Name = "h3bLabel3";
            this.h3bLabel3.Size = new System.Drawing.Size(152, 16);
            this.h3bLabel3.TabIndex = 8;
            this.h3bLabel3.Tag = "CreateCopyOfErrorLogFile";
            this.h3bLabel3.Text = "CreateCopyOfErrorLogFile";
            this.h3bLabel3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.AutoSize = true;
            this.btnSaveLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveLog.Location = new System.Drawing.Point(13, 42);
            this.btnSaveLog.MinimumSize = new System.Drawing.Size(90, 27);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.btnSaveLog.Size = new System.Drawing.Size(119, 27);
            this.btnSaveLog.TabIndex = 7;
            this.btnSaveLog.Tag = "RetrieveLogFile";
            this.btnSaveLog.Text = "RetrieveLogFile";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnSaveLog.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // h3bLabel2
            // 
            this.h3bLabel2.AutoSize = true;
            this.h3bLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel2.Location = new System.Drawing.Point(13, 81);
            this.h3bLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel2.Name = "h3bLabel2";
            this.h3bLabel2.Size = new System.Drawing.Size(158, 16);
            this.h3bLabel2.TabIndex = 6;
            this.h3bLabel2.Tag = "RevertToEmergencyBackup";
            this.h3bLabel2.Text = "RevertToEmergencyBackup";
            this.h3bLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // c1Button2
            // 
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.Location = new System.Drawing.Point(13, 110);
            this.c1Button2.MinimumSize = new System.Drawing.Size(90, 27);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.c1Button2.Size = new System.Drawing.Size(90, 27);
            this.c1Button2.TabIndex = 5;
            this.c1Button2.Tag = "Restore";
            this.c1Button2.Text = "Restore";
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            this.c1Button2.Click += new System.EventHandler(this.c1Button2_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.label21.Location = new System.Drawing.Point(13, 14);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 30);
            this.label21.TabIndex = 58;
            this.label21.Tag = "Settings";
            this.label21.Text = "Settings";
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(6);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Size = new System.Drawing.Size(1010, 6);
            this.hrTop.TabIndex = 57;
            // 
            // SettingsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hrTop);
            this.Name = "SettingsDashboard";
            this.Size = new System.Drawing.Size(1010, 812);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvUsers)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.HR hrTop;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlLf;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.H3bLabel h3bLabel1;
        private Controls.AdminLevelTypesControl adminLevelTypesControl1;
        private Demography.CountryView countryView1;
        private BrightIdeasSoftware.ObjectListView lvUsers;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn16;
        private BrightIdeasSoftware.OLVColumn olvColumn17;
        private BrightIdeasSoftware.OLVColumn olvColumn18;
        private System.Windows.Forms.TabPage tabPage3;
        private DiseasePickerControl diseasePickerControl1;
        private Controls.H3Link lnkAddUser;
        private System.Windows.Forms.TabPage tabPage4;
        private Controls.H3bLabel h3bLabel2;
        private C1.Win.C1Input.C1Button c1Button2;
        private Controls.H3bLabel h3bLabel3;
        private C1.Win.C1Input.C1Button btnSaveLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private C1.Win.C1Input.C1Button c1Button1;
        private C1.Win.C1Input.C1Button c1Button3;
        private System.Windows.Forms.TabPage tabPage5;
        private Demography.CountryDemographyView countryDemographyView1;
    }
}

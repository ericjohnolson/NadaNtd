namespace Nada.UI.View.Survey
{
    partial class LfMfPrevalenceView
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
            this.label1 = new System.Windows.Forms.Label();
            this.bsSurvey = new System.Windows.Forms.BindingSource(this.components);
            this.cbSiteType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSpotCheck = new System.Windows.Forms.Panel();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.tbLng = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSiteName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlSentinel = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSites = new System.Windows.Forms.ComboBox();
            this.sentinelSiteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lnkAddSite = new System.Windows.Forms.LinkLabel();
            this.adminLevelPickerControl2 = new Nada.UI.View.AdminLevelPickerControl();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tbExamined = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPositive = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbPercentPositive = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customIndicatorControl1 = new Nada.UI.View.CustomIndicatorControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlSpotCheck.SuspendLayout();
            this.pnlSentinel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsType, "SurveyTypeName", true));
            this.label3.Font = new System.Drawing.Font("Georgia", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(424, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "LF Prevalence Survey (MF)";
            // 
            // bsType
            // 
            this.bsType.DataSource = typeof(Nada.Model.Survey.SurveyType);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Timing type";
            // 
            // bsSurvey
            // 
            this.bsSurvey.DataSource = typeof(Nada.Model.Survey.LfMfPrevalence);
            // 
            // cbSiteType
            // 
            this.cbSiteType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "SiteType", true));
            this.cbSiteType.FormattingEnabled = true;
            this.cbSiteType.Items.AddRange(new object[] {
            "Sentinel",
            "Spot Check"});
            this.cbSiteType.Location = new System.Drawing.Point(10, 204);
            this.cbSiteType.Name = "cbSiteType";
            this.cbSiteType.Size = new System.Drawing.Size(166, 21);
            this.cbSiteType.TabIndex = 3;
            this.cbSiteType.SelectedIndexChanged += new System.EventHandler(this.cbSiteType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Type of site";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnlSpotCheck, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlSentinel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 166);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlSpotCheck
            // 
            this.pnlSpotCheck.AutoSize = true;
            this.pnlSpotCheck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSpotCheck.Controls.Add(this.adminLevelPickerControl1);
            this.pnlSpotCheck.Controls.Add(this.tbLng);
            this.pnlSpotCheck.Controls.Add(this.label7);
            this.pnlSpotCheck.Controls.Add(this.tbLat);
            this.pnlSpotCheck.Controls.Add(this.label6);
            this.pnlSpotCheck.Controls.Add(this.tbSiteName);
            this.pnlSpotCheck.Controls.Add(this.label5);
            this.pnlSpotCheck.Location = new System.Drawing.Point(3, 3);
            this.pnlSpotCheck.Name = "pnlSpotCheck";
            this.pnlSpotCheck.Size = new System.Drawing.Size(167, 160);
            this.pnlSpotCheck.TabIndex = 0;
            this.pnlSpotCheck.Visible = false;
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(0, 3);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl1.TabIndex = 0;
            // 
            // tbLng
            // 
            this.tbLng.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Lng", true));
            this.tbLng.Location = new System.Drawing.Point(6, 137);
            this.tbLng.Name = "tbLng";
            this.tbLng.Size = new System.Drawing.Size(158, 20);
            this.tbLng.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Longitude";
            // 
            // tbLat
            // 
            this.tbLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Lat", true));
            this.tbLat.Location = new System.Drawing.Point(6, 98);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(158, 20);
            this.tbLat.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Latitude";
            // 
            // tbSiteName
            // 
            this.tbSiteName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "SpotCheckName", true));
            this.tbSiteName.Location = new System.Drawing.Point(6, 59);
            this.tbSiteName.Name = "tbSiteName";
            this.tbSiteName.Size = new System.Drawing.Size(158, 20);
            this.tbSiteName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Site Name";
            // 
            // pnlSentinel
            // 
            this.pnlSentinel.AutoSize = true;
            this.pnlSentinel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSentinel.Controls.Add(this.label8);
            this.pnlSentinel.Controls.Add(this.cbSites);
            this.pnlSentinel.Controls.Add(this.lnkAddSite);
            this.pnlSentinel.Controls.Add(this.adminLevelPickerControl2);
            this.pnlSentinel.Location = new System.Drawing.Point(176, 3);
            this.pnlSentinel.Name = "pnlSentinel";
            this.pnlSentinel.Size = new System.Drawing.Size(161, 96);
            this.pnlSentinel.TabIndex = 1;
            this.pnlSentinel.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Site Name";
            // 
            // cbSites
            // 
            this.cbSites.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsSurvey, "SentinelSiteId", true));
            this.cbSites.DataSource = this.sentinelSiteBindingSource;
            this.cbSites.DisplayMember = "SiteName";
            this.cbSites.FormattingEnabled = true;
            this.cbSites.Location = new System.Drawing.Point(4, 59);
            this.cbSites.Name = "cbSites";
            this.cbSites.Size = new System.Drawing.Size(154, 21);
            this.cbSites.TabIndex = 1;
            this.cbSites.ValueMember = "Id";
            // 
            // sentinelSiteBindingSource
            // 
            this.sentinelSiteBindingSource.DataSource = typeof(Nada.Model.Survey.SentinelSite);
            // 
            // lnkAddSite
            // 
            this.lnkAddSite.AutoSize = true;
            this.lnkAddSite.Location = new System.Drawing.Point(1, 83);
            this.lnkAddSite.Name = "lnkAddSite";
            this.lnkAddSite.Size = new System.Drawing.Size(60, 13);
            this.lnkAddSite.TabIndex = 22;
            this.lnkAddSite.TabStop = true;
            this.lnkAddSite.Text = "Add New...";
            this.lnkAddSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAddSite_LinkClicked);
            // 
            // adminLevelPickerControl2
            // 
            this.adminLevelPickerControl2.AutoSize = true;
            this.adminLevelPickerControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl2.Location = new System.Drawing.Point(0, 0);
            this.adminLevelPickerControl2.Name = "adminLevelPickerControl2";
            this.adminLevelPickerControl2.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl2.TabIndex = 0;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "RoundsMda", true));
            this.textBox3.Location = new System.Drawing.Point(6, 16);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(158, 20);
            this.textBox3.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Number of rounds MDA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Date of survey";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsSurvey, "SurveyDate", true));
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 84);
            this.dateTimePicker1.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2013, 8, 15, 0, 0, 0, 0);
            // 
            // tbExamined
            // 
            this.tbExamined.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Examined", true));
            this.tbExamined.Location = new System.Drawing.Point(6, 58);
            this.tbExamined.Name = "tbExamined";
            this.tbExamined.Size = new System.Drawing.Size(158, 20);
            this.tbExamined.TabIndex = 1;
            this.tbExamined.Validated += new System.EventHandler(this.tbExamined_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Number of people examined";
            // 
            // tbPositive
            // 
            this.tbPositive.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Positive", true));
            this.tbPositive.Location = new System.Drawing.Point(6, 97);
            this.tbPositive.Name = "tbPositive";
            this.tbPositive.Size = new System.Drawing.Size(158, 20);
            this.tbPositive.TabIndex = 2;
            this.tbPositive.Validated += new System.EventHandler(this.tbPositive_Validated);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Number of people positive";
            // 
            // tbPercentPositive
            // 
            this.tbPercentPositive.Enabled = false;
            this.tbPercentPositive.Location = new System.Drawing.Point(6, 136);
            this.tbPercentPositive.Name = "tbPercentPositive";
            this.tbPercentPositive.Size = new System.Drawing.Size(158, 20);
            this.tbPercentPositive.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "% Positive";
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "MeanDensity", true));
            this.textBox7.Location = new System.Drawing.Point(6, 175);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(158, 20);
            this.textBox7.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Mean density";
            // 
            // textBox8
            // 
            this.textBox8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "MfCount", true));
            this.textBox8.Location = new System.Drawing.Point(6, 214);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(158, 20);
            this.textBox8.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 198);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "Count";
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "MfLoad", true));
            this.textBox9.Location = new System.Drawing.Point(6, 253);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(158, 20);
            this.textBox9.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 237);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Community MF Load";
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
            this.tblNotes.Controls.Add(this.panel1, 0, 3);
            this.tblNotes.Controls.Add(this.customIndicatorControl1, 0, 2);
            this.tblNotes.Controls.Add(this.panel2, 0, 1);
            this.tblNotes.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tblNotes.Location = new System.Drawing.Point(1, 231);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 4;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.Size = new System.Drawing.Size(593, 741);
            this.tblNotes.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.kryptonButton1);
            this.panel1.Location = new System.Drawing.Point(3, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 180);
            this.panel1.TabIndex = 0;
            // 
            // customIndicatorControl1
            // 
            this.customIndicatorControl1.AutoSize = true;
            this.customIndicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customIndicatorControl1.Location = new System.Drawing.Point(0, 532);
            this.customIndicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.customIndicatorControl1.Name = "customIndicatorControl1";
            this.customIndicatorControl1.Size = new System.Drawing.Size(506, 23);
            this.customIndicatorControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.textBox9);
            this.panel2.Controls.Add(this.tbExamined);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.textBox8);
            this.panel2.Controls.Add(this.tbPositive);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Controls.Add(this.tbPercentPositive);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Location = new System.Drawing.Point(3, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 354);
            this.panel2.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "AgeRange", true));
            this.textBox2.Location = new System.Drawing.Point(6, 331);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(158, 20);
            this.textBox2.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 315);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Age Range";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "SampleSize", true));
            this.textBox1.Location = new System.Drawing.Point(6, 292);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Sample Size";
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "TimingType", true));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Baseline",
            "Midterm",
            "Pre TAS"});
            this.comboBox1.Location = new System.Drawing.Point(13, 123);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(166, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 39);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(114, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Add/remove indicators";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // comboBox2
            // 
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "TestType", true));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "MF",
            "ICT"});
            this.comboBox2.Location = new System.Drawing.Point(13, 163);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(166, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 147);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 13);
            this.label20.TabIndex = 42;
            this.label20.Text = "Test type";
            // 
            // LfMfPrevalenceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tblNotes);
            this.Controls.Add(this.cbSiteType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "LfMfPrevalenceView";
            this.Size = new System.Drawing.Size(597, 975);
            this.Load += new System.EventHandler(this.LfPrevalence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlSpotCheck.ResumeLayout(false);
            this.pnlSpotCheck.PerformLayout();
            this.pnlSentinel.ResumeLayout(false);
            this.pnlSentinel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSiteType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlSpotCheck;
        private System.Windows.Forms.Panel pnlSentinel;
        private System.Windows.Forms.TextBox tbLng;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSiteName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox tbExamined;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbPositive;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbPercentPositive;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label18;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.BindingSource bsSurvey;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private System.Windows.Forms.Panel panel1;
        private CustomIndicatorControl customIndicatorControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private AdminLevelPickerControl adminLevelPickerControl2;
        private System.Windows.Forms.LinkLabel lnkAddSite;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSites;
        private System.Windows.Forms.BindingSource sentinelSiteBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource bsType;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label20;
    }
}

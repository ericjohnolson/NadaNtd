namespace Nada.UI.View
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
            this.hrTop = new Nada.UI.Controls.HR();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tblEditBottom = new System.Windows.Forms.TableLayoutPanel();
            this.hr4 = new Nada.UI.Controls.HR();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.indicatorControl1 = new Nada.UI.View.IndicatorControl();
            this.statCalculator1 = new Nada.UI.Controls.StatCalculator();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBottomSave = new C1.Win.C1Input.C1Button();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.hr5 = new Nada.UI.Controls.HR();
            this.validationControl = new Nada.UI.Controls.ValidationControl();
            this.nadaLabel2 = new Nada.UI.Controls.H3Label();
            this.nadaLabel1 = new Nada.UI.Controls.H3Label();
            this.adminLevelPickerControl2 = new Nada.UI.View.AdminLevelPickerControl();
            this.tblEditTop = new System.Windows.Forms.TableLayoutPanel();
            this.c1Button3 = new C1.Win.C1Input.C1Button();
            this.btnTopSave = new C1.Win.C1Input.C1Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tblReq = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblTitle.SuspendLayout();
            this.tblEditBottom.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblEditTop.SuspendLayout();
            this.tblReq.SuspendLayout();
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
            this.tblTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblTitle.Size = new System.Drawing.Size(205, 30);
            this.tblTitle.TabIndex = 60;
            // 
            // lblDiseaseType
            // 
            this.lblDiseaseType.AutoSize = true;
            this.lblDiseaseType.BackColor = System.Drawing.Color.Transparent;
            this.lblDiseaseType.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiseaseType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
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
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "GenericDataEntry";
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(6);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hrTop.Size = new System.Drawing.Size(953, 7);
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
            // tblEditBottom
            // 
            this.tblEditBottom.AutoSize = true;
            this.tblEditBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblEditBottom.ColumnCount = 1;
            this.tblEditBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEditBottom.Controls.Add(this.hr4, 0, 4);
            this.tblEditBottom.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tblEditBottom.Controls.Add(this.indicatorControl1, 0, 2);
            this.tblEditBottom.Controls.Add(this.statCalculator1, 0, 6);
            this.tblEditBottom.Controls.Add(this.tableLayoutPanel4, 0, 12);
            this.tblEditBottom.Controls.Add(this.hr5, 0, 11);
            this.tblEditBottom.Controls.Add(this.validationControl, 0, 9);
            this.tblEditBottom.Location = new System.Drawing.Point(12, 101);
            this.tblEditBottom.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.tblEditBottom.Name = "tblEditBottom";
            this.tblEditBottom.RowCount = 13;
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblEditBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditBottom.Size = new System.Drawing.Size(938, 638);
            this.tblEditBottom.TabIndex = 0;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(3, 109);
            this.hr4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hr4.Size = new System.Drawing.Size(920, 1);
            this.hr4.TabIndex = 47;
            this.hr4.TabStop = false;
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 124);
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
            this.indicatorControl1.AllowCustom = true;
            this.indicatorControl1.AutoSize = true;
            this.indicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.indicatorControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.indicatorControl1.BackColor = System.Drawing.Color.White;
            this.indicatorControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorControl1.Location = new System.Drawing.Point(0, 6);
            this.indicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.indicatorControl1.Name = "indicatorControl1";
            this.indicatorControl1.Size = new System.Drawing.Size(920, 93);
            this.indicatorControl1.TabIndex = 58;
            this.indicatorControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            // 
            // statCalculator1
            // 
            this.statCalculator1.AutoSize = true;
            this.statCalculator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statCalculator1.BackColor = System.Drawing.Color.White;
            this.statCalculator1.Calc = null;
            this.statCalculator1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statCalculator1.Location = new System.Drawing.Point(3, 266);
            this.statCalculator1.Name = "statCalculator1";
            this.statCalculator1.OnFocus = null;
            this.statCalculator1.Size = new System.Drawing.Size(919, 75);
            this.statCalculator1.TabIndex = 59;
            this.statCalculator1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btnBottomSave, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.c1Button2, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(731, 556);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(204, 79);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnBottomSave
            // 
            this.btnBottomSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBottomSave.AutoSize = true;
            this.btnBottomSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBottomSave.Location = new System.Drawing.Point(111, 3);
            this.btnBottomSave.MinimumSize = new System.Drawing.Size(90, 27);
            this.btnBottomSave.Name = "btnBottomSave";
            this.btnBottomSave.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.btnBottomSave.Size = new System.Drawing.Size(90, 27);
            this.btnBottomSave.TabIndex = 2;
            this.btnBottomSave.Tag = "Save";
            this.btnBottomSave.Text = "Save";
            this.btnBottomSave.UseVisualStyleBackColor = true;
            this.btnBottomSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnBottomSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnBottomSave.Click += new System.EventHandler(this.save_Click);
            // 
            // c1Button2
            // 
            this.c1Button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.c1Button2.AutoSize = true;
            this.c1Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button2.Location = new System.Drawing.Point(3, 3);
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
            // hr5
            // 
            this.hr5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hr5.ForeColor = System.Drawing.Color.Gray;
            this.hr5.Location = new System.Drawing.Point(3, 541);
            this.hr5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.hr5.Name = "hr5";
            this.hr5.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.hr5.Size = new System.Drawing.Size(920, 1);
            this.hr5.TabIndex = 57;
            this.hr5.TabStop = false;
            // 
            // validationControl
            // 
            this.validationControl.AutoSize = true;
            this.validationControl.BackColor = System.Drawing.Color.White;
            this.validationControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validationControl.Location = new System.Drawing.Point(3, 369);
            this.validationControl.Name = "validationControl";
            this.validationControl.OnFocus = null;
            this.validationControl.Size = new System.Drawing.Size(932, 159);
            this.validationControl.TabIndex = 60;
            this.validationControl.TextColor = System.Drawing.SystemColors.ControlText;
            this.validationControl.Validator = null;
            // 
            // nadaLabel2
            // 
            this.nadaLabel2.AutoSize = true;
            this.nadaLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel2.Location = new System.Drawing.Point(0, 0);
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
            this.nadaLabel1.Location = new System.Drawing.Point(12, 0);
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
            // tblEditTop
            // 
            this.tblEditTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tblEditTop.AutoSize = true;
            this.tblEditTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblEditTop.ColumnCount = 5;
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEditTop.Controls.Add(this.c1Button3, 2, 0);
            this.tblEditTop.Controls.Add(this.btnTopSave, 4, 0);
            this.tblEditTop.Controls.Add(this.btnHelp, 0, 0);
            this.tblEditTop.Location = new System.Drawing.Point(682, 8);
            this.tblEditTop.Margin = new System.Windows.Forms.Padding(0);
            this.tblEditTop.Name = "tblEditTop";
            this.tblEditTop.RowCount = 1;
            this.tblEditTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEditTop.Size = new System.Drawing.Size(268, 52);
            this.tblEditTop.TabIndex = 62;
            // 
            // c1Button3
            // 
            this.c1Button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.c1Button3.AutoSize = true;
            this.c1Button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.c1Button3.Location = new System.Drawing.Point(67, 12);
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
            this.c1Button3.Click += new System.EventHandler(this.cancel_Click);
            // 
            // btnTopSave
            // 
            this.btnTopSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnTopSave.AutoSize = true;
            this.btnTopSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTopSave.Location = new System.Drawing.Point(175, 12);
            this.btnTopSave.MinimumSize = new System.Drawing.Size(90, 27);
            this.btnTopSave.Name = "btnTopSave";
            this.btnTopSave.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.btnTopSave.Size = new System.Drawing.Size(90, 27);
            this.btnTopSave.TabIndex = 2;
            this.btnTopSave.Tag = "Save";
            this.btnTopSave.Text = "Save";
            this.btnTopSave.UseVisualStyleBackColor = true;
            this.btnTopSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnTopSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnTopSave.Click += new System.EventHandler(this.save_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(3, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 59;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tblReq
            // 
            this.tblReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tblReq.AutoSize = true;
            this.tblReq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblReq.ColumnCount = 2;
            this.tblReq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReq.Controls.Add(this.nadaLabel2, 0, 0);
            this.tblReq.Controls.Add(this.nadaLabel1, 1, 0);
            this.tblReq.Location = new System.Drawing.Point(877, 74);
            this.tblReq.Margin = new System.Windows.Forms.Padding(0);
            this.tblReq.Name = "tblReq";
            this.tblReq.RowCount = 1;
            this.tblReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblReq.Size = new System.Drawing.Size(70, 20);
            this.tblReq.TabIndex = 63;
            // 
            // DataEntryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblReq);
            this.Controls.Add(this.tblEditTop);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tblTitle);
            this.Controls.Add(this.hrTop);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tblEditBottom);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "DataEntryEdit";
            this.Size = new System.Drawing.Size(953, 742);
            this.Load += new System.EventHandler(this.DataEntryEdit_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblTitle.ResumeLayout(false);
            this.tblTitle.PerformLayout();
            this.tblEditBottom.ResumeLayout(false);
            this.tblEditBottom.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tblEditTop.ResumeLayout(false);
            this.tblEditTop.PerformLayout();
            this.tblReq.ResumeLayout(false);
            this.tblReq.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private AdminLevelPickerControl adminLevelPickerControl2;
        private Controls.H3Label nadaLabel1;
        private Controls.H3Label nadaLabel2;
        private Controls.HR hr4;
        private System.Windows.Forms.TableLayoutPanel tblEditBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Controls.HR hrTop;
        private Controls.HR hr5;
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
        private C1.Win.C1Input.C1Button btnBottomSave;
        private System.Windows.Forms.TableLayoutPanel tblEditTop;
        private C1.Win.C1Input.C1Button c1Button3;
        private C1.Win.C1Input.C1Button btnTopSave;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TableLayoutPanel tblReq;
        private Controls.ValidationControl validationControl;
    }
}

namespace Nada.UI.View.DiseaseDistribution
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.lblDiseaseType = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hr5 = new Nada.UI.Controls.HR();
            this.hr4 = new Nada.UI.Controls.HR();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.indicatorControl1 = new Nada.UI.View.IndicatorControl();
            this.statCalculator1 = new Nada.UI.Controls.StatCalculator();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tblTitle = new System.Windows.Forms.TableLayoutPanel();
            this.hrTop = new Nada.UI.Controls.HR();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.nadaLabel2 = new Nada.UI.Controls.H3Label();
            this.nadaLabel1 = new Nada.UI.Controls.H3Label();
            this.adminLevelPickerControl2 = new Nada.UI.View.AdminLevelPickerControl();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDash = new System.Windows.Forms.Button();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tblTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.lblTitle.Location = new System.Drawing.Point(45, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(178, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "DiseaseDistribution";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.Location = new System.Drawing.Point(3, 3);
            this.kryptonButton1.MinimumSize = new System.Drawing.Size(78, 25);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonButton1.Size = new System.Drawing.Size(78, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Tag = "Save";
            this.kryptonButton1.Values.Text = "Save";
            this.kryptonButton1.Click += new System.EventHandler(this.save_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton1, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(178, 71);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.AutoSize = true;
            this.kryptonButton2.Location = new System.Drawing.Point(97, 3);
            this.kryptonButton2.MinimumSize = new System.Drawing.Size(78, 25);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton2.Size = new System.Drawing.Size(78, 25);
            this.kryptonButton2.TabIndex = 1;
            this.kryptonButton2.Tag = "Cancel";
            this.kryptonButton2.Values.Text = "Cancel";
            this.kryptonButton2.Click += new System.EventHandler(this.cancel_Click);
            // 
            // tblNotes
            // 
            this.tblNotes.AutoSize = true;
            this.tblNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNotes.ColumnCount = 1;
            this.tblNotes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNotes.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tblNotes.Location = new System.Drawing.Point(3, 298);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 2;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.Size = new System.Drawing.Size(184, 77);
            this.tblNotes.TabIndex = 7;
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
            this.lblDiseaseType.Size = new System.Drawing.Size(45, 30);
            this.lblDiseaseType.TabIndex = 44;
            this.lblDiseaseType.Tag = "CM";
            this.lblDiseaseType.Text = "CM";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.hr5, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.hr4, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.tblNotes, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.indicatorControl1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.statCalculator1, 0, 5);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 100);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(806, 378);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // hr5
            // 
            this.hr5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr5.ForeColor = System.Drawing.Color.Gray;
            this.hr5.Location = new System.Drawing.Point(0, 285);
            this.hr5.Margin = new System.Windows.Forms.Padding(0);
            this.hr5.Name = "hr5";
            this.hr5.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr5.Size = new System.Drawing.Size(800, 1);
            this.hr5.TabIndex = 57;
            this.hr5.TabStop = false;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 82);
            this.hr4.Margin = new System.Windows.Forms.Padding(0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr4.Size = new System.Drawing.Size(800, 1);
            this.hr4.TabIndex = 47;
            this.hr4.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.h3Label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 95);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(559, 120);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(553, 98);
            this.textBox1.TabIndex = 0;
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(0, 0);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(44, 16);
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
            this.indicatorControl1.Location = new System.Drawing.Point(0, 5);
            this.indicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.indicatorControl1.Name = "indicatorControl1";
            this.indicatorControl1.Size = new System.Drawing.Size(803, 68);
            this.indicatorControl1.TabIndex = 58;
            this.indicatorControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            // 
            // statCalculator1
            // 
            this.statCalculator1.AutoSize = true;
            this.statCalculator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statCalculator1.BackColor = System.Drawing.Color.White;
            this.statCalculator1.Calc = null;
            this.statCalculator1.Location = new System.Drawing.Point(3, 221);
            this.statCalculator1.Name = "statCalculator1";
            this.statCalculator1.Size = new System.Drawing.Size(800, 52);
            this.statCalculator1.TabIndex = 59;
            this.statCalculator1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Location = new System.Drawing.Point(11, 7);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel6.TabIndex = 55;
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
            this.tblTitle.Location = new System.Drawing.Point(11, 14);
            this.tblTitle.Name = "tblTitle";
            this.tblTitle.RowCount = 1;
            this.tblTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitle.Size = new System.Drawing.Size(223, 30);
            this.tblTitle.TabIndex = 60;
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 0);
            this.hrTop.Margin = new System.Windows.Forms.Padding(5);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hrTop.Size = new System.Drawing.Size(819, 5);
            this.hrTop.TabIndex = 56;
            this.hrTop.TabStop = false;
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(10, 58);
            this.adminLevelPickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(200, 27);
            this.adminLevelPickerControl1.TabIndex = 5;
            this.adminLevelPickerControl1.TabStop = false;
            this.adminLevelPickerControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            // 
            // nadaLabel2
            // 
            this.nadaLabel2.AutoSize = true;
            this.nadaLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel2.Location = new System.Drawing.Point(710, 69);
            this.nadaLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel2.Name = "nadaLabel2";
            this.nadaLabel2.Size = new System.Drawing.Size(13, 16);
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
            this.nadaLabel1.Location = new System.Drawing.Point(721, 66);
            this.nadaLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel1.Name = "nadaLabel1";
            this.nadaLabel1.Size = new System.Drawing.Size(64, 16);
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
            this.adminLevelPickerControl2.Location = new System.Drawing.Point(0, 0);
            this.adminLevelPickerControl2.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl2.Name = "adminLevelPickerControl2";
            this.adminLevelPickerControl2.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl2.TabIndex = 0;
            this.adminLevelPickerControl2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(691, 8);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 59;
            this.btnHelp.TabStop = false;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDash
            // 
            this.btnDash.AutoSize = true;
            this.btnDash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDash.FlatAppearance.BorderSize = 0;
            this.btnDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDash.Image = global::Nada.UI.Properties.Resources.button_dashboard;
            this.btnDash.Location = new System.Drawing.Point(743, 8);
            this.btnDash.Name = "btnDash";
            this.btnDash.Size = new System.Drawing.Size(46, 46);
            this.btnDash.TabIndex = 57;
            this.btnDash.TabStop = false;
            this.btnDash.UseVisualStyleBackColor = true;
            this.btnDash.Click += new System.EventHandler(this.btnDash_Click);
            // 
            // DataEntryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblTitle);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDash);
            this.Controls.Add(this.hrTop);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.nadaLabel2);
            this.Controls.Add(this.nadaLabel1);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "DataEntryEdit";
            this.Size = new System.Drawing.Size(819, 481);
            this.Load += new System.EventHandler(this.DiseaseDistro_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tblTitle.ResumeLayout(false);
            this.tblTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private AdminLevelPickerControl adminLevelPickerControl2;
        private System.Windows.Forms.Label lblDiseaseType;
        private Controls.H3Label nadaLabel1;
        private Controls.H3Label nadaLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private Controls.HR hr4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Controls.HR hrTop;
        private Controls.HR hr5;
        private System.Windows.Forms.Button btnDash;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TableLayoutPanel tblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBox1;
        private Controls.H3Label h3Label3;
        private IndicatorControl indicatorControl1;
        private Controls.StatCalculator statCalculator1;
    }
}

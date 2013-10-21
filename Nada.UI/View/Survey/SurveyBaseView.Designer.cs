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
            this.bsSurvey = new System.Windows.Forms.BindingSource(this.components);
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDash = new System.Windows.Forms.Button();
            this.hr4 = new Nada.UI.Controls.HR();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hr5 = new Nada.UI.Controls.HR();
            this.customIndicatorControl1 = new Nada.UI.View.CustomIndicatorControl();
            this.hr2 = new Nada.UI.Controls.HR();
            this.tblNotes = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label5 = new Nada.UI.Controls.H3Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.nadaLabel2 = new Nada.UI.Controls.H3Label();
            this.nadaLabel1 = new Nada.UI.Controls.H3Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tblNotes.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsSurvey
            // 
            this.bsSurvey.DataSource = typeof(Nada.Model.Base.SurveyBase);
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Nada.UI.Properties.Resources.button_help;
            this.btnHelp.Location = new System.Drawing.Point(691, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 46);
            this.btnHelp.TabIndex = 67;
            this.btnHelp.TabStop = false;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(11, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(117, 30);
            this.lblTitle.TabIndex = 62;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "TypeName";
            // 
            // btnDash
            // 
            this.btnDash.AutoSize = true;
            this.btnDash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDash.FlatAppearance.BorderSize = 0;
            this.btnDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDash.Image = global::Nada.UI.Properties.Resources.button_dashboard;
            this.btnDash.Location = new System.Drawing.Point(743, 5);
            this.btnDash.Name = "btnDash";
            this.btnDash.Size = new System.Drawing.Size(46, 46);
            this.btnDash.TabIndex = 66;
            this.btnDash.TabStop = false;
            this.btnDash.UseVisualStyleBackColor = true;
            this.btnDash.Click += new System.EventHandler(this.btnDash_Click);
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr4.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 0);
            this.hr4.Margin = new System.Windows.Forms.Padding(5);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr4.Size = new System.Drawing.Size(813, 5);
            this.hr4.TabIndex = 65;
            this.hr4.TabStop = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.hr5, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.customIndicatorControl1, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.hr2, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tblNotes, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 97);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(800, 210);
            this.tableLayoutPanel5.TabIndex = 60;
            // 
            // hr5
            // 
            this.hr5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr5.ForeColor = System.Drawing.Color.Gray;
            this.hr5.Location = new System.Drawing.Point(0, 117);
            this.hr5.Margin = new System.Windows.Forms.Padding(0);
            this.hr5.Name = "hr5";
            this.hr5.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr5.Size = new System.Drawing.Size(800, 1);
            this.hr5.TabIndex = 57;
            // 
            // customIndicatorControl1
            // 
            this.customIndicatorControl1.AutoSize = true;
            this.customIndicatorControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customIndicatorControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.customIndicatorControl1.Location = new System.Drawing.Point(0, 67);
            this.customIndicatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.customIndicatorControl1.Name = "customIndicatorControl1";
            this.customIndicatorControl1.Size = new System.Drawing.Size(146, 41);
            this.customIndicatorControl1.TabIndex = 0;
            this.customIndicatorControl1.TabStop = false;
            this.customIndicatorControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            // 
            // hr2
            // 
            this.hr2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr2.ForeColor = System.Drawing.Color.Gray;
            this.hr2.Location = new System.Drawing.Point(0, 57);
            this.hr2.Margin = new System.Windows.Forms.Padding(0);
            this.hr2.Name = "hr2";
            this.hr2.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr2.Size = new System.Drawing.Size(800, 1);
            this.hr2.TabIndex = 50;
            // 
            // tblNotes
            // 
            this.tblNotes.AutoSize = true;
            this.tblNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNotes.ColumnCount = 1;
            this.tblNotes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNotes.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tblNotes.Location = new System.Drawing.Point(3, 130);
            this.tblNotes.Name = "tblNotes";
            this.tblNotes.RowCount = 2;
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNotes.Size = new System.Drawing.Size(212, 77);
            this.tblNotes.TabIndex = 6;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton1, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(206, 71);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(106, 3);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton2.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton2.TabIndex = 1;
            this.kryptonButton2.Tag = "Cancel";
            this.kryptonButton2.Values.Text = "Cancel";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(3, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Tag = "Save";
            this.kryptonButton1.Values.Text = "Save";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.h3Label2, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.h3Label5, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.dateTimePicker1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.dateTimePicker2, 2, 1);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(422, 42);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // h3Label2
            // 
            this.h3Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(0, 0);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(122, 16);
            this.h3Label2.TabIndex = 3;
            this.h3Label2.TabStop = false;
            this.h3Label2.Tag = "StartDateSurvey";
            this.h3Label2.Text = "Start date of survey";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label5
            // 
            this.h3Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Label5.AutoSize = true;
            this.h3Label5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label5.Location = new System.Drawing.Point(216, 0);
            this.h3Label5.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label5.Name = "h3Label5";
            this.h3Label5.Size = new System.Drawing.Size(119, 16);
            this.h3Label5.TabIndex = 6;
            this.h3Label5.TabStop = false;
            this.h3Label5.Tag = "EndDateSurvey";
            this.h3Label5.Text = "End date of survey";
            this.h3Label5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsSurvey, "StartDate", true));
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsSurvey, "EndDate", true));
            this.dateTimePicker2.Location = new System.Drawing.Point(219, 19);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(10, 55);
            this.adminLevelPickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(200, 27);
            this.adminLevelPickerControl1.TabIndex = 61;
            this.adminLevelPickerControl1.TabStop = false;
            this.adminLevelPickerControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            // 
            // nadaLabel2
            // 
            this.nadaLabel2.AutoSize = true;
            this.nadaLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel2.Location = new System.Drawing.Point(720, 66);
            this.nadaLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel2.Name = "nadaLabel2";
            this.nadaLabel2.Size = new System.Drawing.Size(13, 16);
            this.nadaLabel2.TabIndex = 64;
            this.nadaLabel2.TabStop = false;
            this.nadaLabel2.Text = "*";
            this.nadaLabel2.TextColor = System.Drawing.Color.Red;
            // 
            // nadaLabel1
            // 
            this.nadaLabel1.AutoSize = true;
            this.nadaLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nadaLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nadaLabel1.Location = new System.Drawing.Point(731, 63);
            this.nadaLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.nadaLabel1.Name = "nadaLabel1";
            this.nadaLabel1.Size = new System.Drawing.Size(64, 16);
            this.nadaLabel1.TabIndex = 63;
            this.nadaLabel1.TabStop = false;
            this.nadaLabel1.Tag = "Required";
            this.nadaLabel1.Text = "Required";
            this.nadaLabel1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsSurvey;
            // 
            // SurveyBaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDash);
            this.Controls.Add(this.hr4);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.nadaLabel2);
            this.Controls.Add(this.nadaLabel1);
            this.Name = "SurveyBaseView";
            this.Size = new System.Drawing.Size(813, 310);
            this.Load += new System.EventHandler(this.LfPrevalence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tblNotes.ResumeLayout(false);
            this.tblNotes.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsSurvey;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDash;
        private Controls.HR hr4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Controls.HR hr5;
        private CustomIndicatorControl customIndicatorControl1;
        private Controls.HR hr2;
        private System.Windows.Forms.TableLayoutPanel tblNotes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private Controls.H3Label nadaLabel2;
        private Controls.H3Label nadaLabel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

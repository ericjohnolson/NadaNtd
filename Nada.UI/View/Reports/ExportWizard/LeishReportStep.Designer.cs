namespace Nada.UI.View.Reports.ExportWizard
{
    partial class LeishReportStep
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
            this.lbYear = new Nada.UI.Controls.H3Required();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.questionDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbTotalAdmin2Vl = new Nada.UI.Controls.H3Required();
            this.lbTotalAdmin2Cl = new Nada.UI.Controls.H3Required();
            this.lbYearLncpEstablished = new Nada.UI.Controls.H3Required();
            this.lbRepUrlLncp = new Nada.UI.Controls.H3Required();
            this.lbYearLatestGuide = new Nada.UI.Controls.H3Required();
            this.lbIsNotifiable = new Nada.UI.Controls.H3Required();
            this.lbIsVectProg = new Nada.UI.Controls.H3Required();
            this.lbIsHostProg = new Nada.UI.Controls.H3Required();
            this.lbNumHealthFac = new Nada.UI.Controls.H3Required();
            this.lbIsTreatFree = new Nada.UI.Controls.H3Required();
            this.lbAntiMedInNml = new Nada.UI.Controls.H3Required();
            this.lbRelapseDefVl = new Nada.UI.Controls.H3Required();
            this.lbRelapseDefCl = new Nada.UI.Controls.H3Required();
            this.lbFailureDefVl = new Nada.UI.Controls.H3Required();
            this.lbFailureDefCl = new Nada.UI.Controls.H3Required();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.antiLeishMedCb = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.questionDataSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbYear.Location = new System.Drawing.Point(0, 0);
            this.lbYear.Margin = new System.Windows.Forms.Padding(0);
            this.lbYear.Name = "lbYear";
            this.lbYear.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbYear.Size = new System.Drawing.Size(43, 45);
            this.lbYear.TabIndex = 0;
            this.lbYear.TabStop = false;
            this.lbYear.Text = "Year";
            this.lbYear.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepYearReporting", true));
            this.textBox1.Location = new System.Drawing.Point(378, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(213, 21);
            this.textBox1.TabIndex = 1;
            // 
            // questionDataSource
            // 
            this.questionDataSource.DataSource = typeof(Nada.Model.Exports.LeishReportQuestions);
            // 
            // lbTotalAdmin2Vl
            // 
            this.lbTotalAdmin2Vl.AutoSize = true;
            this.lbTotalAdmin2Vl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbTotalAdmin2Vl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTotalAdmin2Vl.Location = new System.Drawing.Point(0, 45);
            this.lbTotalAdmin2Vl.Margin = new System.Windows.Forms.Padding(0);
            this.lbTotalAdmin2Vl.Name = "lbTotalAdmin2Vl";
            this.lbTotalAdmin2Vl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbTotalAdmin2Vl.Size = new System.Drawing.Size(174, 45);
            this.lbTotalAdmin2Vl.TabIndex = 2;
            this.lbTotalAdmin2Vl.TabStop = false;
            this.lbTotalAdmin2Vl.Tag = "LeishRepEndemicAdmin2Vl";
            this.lbTotalAdmin2Vl.Text = "LeishRepEndemicAdmin2Vl";
            this.lbTotalAdmin2Vl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbTotalAdmin2Cl
            // 
            this.lbTotalAdmin2Cl.AutoSize = true;
            this.lbTotalAdmin2Cl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbTotalAdmin2Cl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTotalAdmin2Cl.Location = new System.Drawing.Point(0, 90);
            this.lbTotalAdmin2Cl.Margin = new System.Windows.Forms.Padding(0);
            this.lbTotalAdmin2Cl.Name = "lbTotalAdmin2Cl";
            this.lbTotalAdmin2Cl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbTotalAdmin2Cl.Size = new System.Drawing.Size(176, 45);
            this.lbTotalAdmin2Cl.TabIndex = 3;
            this.lbTotalAdmin2Cl.TabStop = false;
            this.lbTotalAdmin2Cl.Tag = "LeishRepEndemicAdmin2Cl";
            this.lbTotalAdmin2Cl.Text = "LeishRepEndemicAdmin2Cl";
            this.lbTotalAdmin2Cl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbYearLncpEstablished
            // 
            this.lbYearLncpEstablished.AutoSize = true;
            this.lbYearLncpEstablished.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbYearLncpEstablished.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbYearLncpEstablished.Location = new System.Drawing.Point(0, 135);
            this.lbYearLncpEstablished.Margin = new System.Windows.Forms.Padding(0);
            this.lbYearLncpEstablished.Name = "lbYearLncpEstablished";
            this.lbYearLncpEstablished.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbYearLncpEstablished.Size = new System.Drawing.Size(176, 60);
            this.lbYearLncpEstablished.TabIndex = 4;
            this.lbYearLncpEstablished.TabStop = false;
            this.lbYearLncpEstablished.Tag = "LeishRepYearLncpEstablished";
            this.lbYearLncpEstablished.Text = "LeishRepYearLncpEstablished";
            this.lbYearLncpEstablished.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbRepUrlLncp
            // 
            this.lbRepUrlLncp.AutoSize = true;
            this.lbRepUrlLncp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbRepUrlLncp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbRepUrlLncp.Location = new System.Drawing.Point(0, 195);
            this.lbRepUrlLncp.Margin = new System.Windows.Forms.Padding(0);
            this.lbRepUrlLncp.Name = "lbRepUrlLncp";
            this.lbRepUrlLncp.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbRepUrlLncp.Size = new System.Drawing.Size(116, 45);
            this.lbRepUrlLncp.TabIndex = 5;
            this.lbRepUrlLncp.TabStop = false;
            this.lbRepUrlLncp.Tag = "LeishRepUrlLncp";
            this.lbRepUrlLncp.Text = "LeishRepUrlLncp";
            this.lbRepUrlLncp.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbYearLatestGuide
            // 
            this.lbYearLatestGuide.AutoSize = true;
            this.lbYearLatestGuide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbYearLatestGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbYearLatestGuide.Location = new System.Drawing.Point(0, 240);
            this.lbYearLatestGuide.Margin = new System.Windows.Forms.Padding(0);
            this.lbYearLatestGuide.Name = "lbYearLatestGuide";
            this.lbYearLatestGuide.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbYearLatestGuide.Size = new System.Drawing.Size(164, 45);
            this.lbYearLatestGuide.TabIndex = 6;
            this.lbYearLatestGuide.TabStop = false;
            this.lbYearLatestGuide.Tag = "LeishRepYearLatestGuide";
            this.lbYearLatestGuide.Text = "LeishRepYearLatestGuide";
            this.lbYearLatestGuide.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbIsNotifiable
            // 
            this.lbIsNotifiable.AutoSize = true;
            this.lbIsNotifiable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbIsNotifiable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbIsNotifiable.Location = new System.Drawing.Point(0, 285);
            this.lbIsNotifiable.Margin = new System.Windows.Forms.Padding(0);
            this.lbIsNotifiable.Name = "lbIsNotifiable";
            this.lbIsNotifiable.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbIsNotifiable.Size = new System.Drawing.Size(135, 45);
            this.lbIsNotifiable.TabIndex = 7;
            this.lbIsNotifiable.TabStop = false;
            this.lbIsNotifiable.Tag = "LeishRepIsNotifiable";
            this.lbIsNotifiable.Text = "LeishRepIsNotifiable";
            this.lbIsNotifiable.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbIsVectProg
            // 
            this.lbIsVectProg.AutoSize = true;
            this.lbIsVectProg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbIsVectProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbIsVectProg.Location = new System.Drawing.Point(0, 335);
            this.lbIsVectProg.Margin = new System.Windows.Forms.Padding(0);
            this.lbIsVectProg.Name = "lbIsVectProg";
            this.lbIsVectProg.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbIsVectProg.Size = new System.Drawing.Size(131, 45);
            this.lbIsVectProg.TabIndex = 8;
            this.lbIsVectProg.TabStop = false;
            this.lbIsVectProg.Tag = "LeishRepIsVectProg";
            this.lbIsVectProg.Text = "LeishRepIsVectProg";
            this.lbIsVectProg.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbIsHostProg
            // 
            this.lbIsHostProg.AutoSize = true;
            this.lbIsHostProg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbIsHostProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbIsHostProg.Location = new System.Drawing.Point(0, 385);
            this.lbIsHostProg.Margin = new System.Windows.Forms.Padding(0);
            this.lbIsHostProg.Name = "lbIsHostProg";
            this.lbIsHostProg.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbIsHostProg.Size = new System.Drawing.Size(135, 45);
            this.lbIsHostProg.TabIndex = 9;
            this.lbIsHostProg.TabStop = false;
            this.lbIsHostProg.Tag = "LeishRepIsHostProg";
            this.lbIsHostProg.Text = "LeishRepIsHostProg";
            this.lbIsHostProg.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbNumHealthFac
            // 
            this.lbNumHealthFac.AutoSize = true;
            this.lbNumHealthFac.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbNumHealthFac.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbNumHealthFac.Location = new System.Drawing.Point(0, 435);
            this.lbNumHealthFac.Margin = new System.Windows.Forms.Padding(0);
            this.lbNumHealthFac.Name = "lbNumHealthFac";
            this.lbNumHealthFac.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbNumHealthFac.Size = new System.Drawing.Size(156, 45);
            this.lbNumHealthFac.TabIndex = 10;
            this.lbNumHealthFac.TabStop = false;
            this.lbNumHealthFac.Tag = "LeishRepNumHealthFac";
            this.lbNumHealthFac.Text = "LeishRepNumHealthFac";
            this.lbNumHealthFac.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbIsTreatFree
            // 
            this.lbIsTreatFree.AutoSize = true;
            this.lbIsTreatFree.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbIsTreatFree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbIsTreatFree.Location = new System.Drawing.Point(0, 480);
            this.lbIsTreatFree.Margin = new System.Windows.Forms.Padding(0);
            this.lbIsTreatFree.Name = "lbIsTreatFree";
            this.lbIsTreatFree.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbIsTreatFree.Size = new System.Drawing.Size(136, 45);
            this.lbIsTreatFree.TabIndex = 11;
            this.lbIsTreatFree.TabStop = false;
            this.lbIsTreatFree.Tag = "LeishRepIsTreatFree";
            this.lbIsTreatFree.Text = "LeishRepIsTreatFree";
            this.lbIsTreatFree.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbAntiMedInNml
            // 
            this.lbAntiMedInNml.AutoSize = true;
            this.lbAntiMedInNml.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbAntiMedInNml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbAntiMedInNml.Location = new System.Drawing.Point(0, 530);
            this.lbAntiMedInNml.Margin = new System.Windows.Forms.Padding(0);
            this.lbAntiMedInNml.Name = "lbAntiMedInNml";
            this.lbAntiMedInNml.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbAntiMedInNml.Size = new System.Drawing.Size(149, 45);
            this.lbAntiMedInNml.TabIndex = 12;
            this.lbAntiMedInNml.TabStop = false;
            this.lbAntiMedInNml.Tag = "LeishRepAntiMedInNml";
            this.lbAntiMedInNml.Text = "LeishRepAntiMedInNml";
            this.lbAntiMedInNml.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbRelapseDefVl
            // 
            this.lbRelapseDefVl.AutoSize = true;
            this.lbRelapseDefVl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbRelapseDefVl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbRelapseDefVl.Location = new System.Drawing.Point(0, 575);
            this.lbRelapseDefVl.Margin = new System.Windows.Forms.Padding(0);
            this.lbRelapseDefVl.Name = "lbRelapseDefVl";
            this.lbRelapseDefVl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbRelapseDefVl.Size = new System.Drawing.Size(149, 45);
            this.lbRelapseDefVl.TabIndex = 13;
            this.lbRelapseDefVl.TabStop = false;
            this.lbRelapseDefVl.Tag = "LeishRepRelapseDefVl";
            this.lbRelapseDefVl.Text = "LeishRepRelapseDefVl";
            this.lbRelapseDefVl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbRelapseDefCl
            // 
            this.lbRelapseDefCl.AutoSize = true;
            this.lbRelapseDefCl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbRelapseDefCl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbRelapseDefCl.Location = new System.Drawing.Point(0, 620);
            this.lbRelapseDefCl.Margin = new System.Windows.Forms.Padding(0);
            this.lbRelapseDefCl.Name = "lbRelapseDefCl";
            this.lbRelapseDefCl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbRelapseDefCl.Size = new System.Drawing.Size(151, 45);
            this.lbRelapseDefCl.TabIndex = 14;
            this.lbRelapseDefCl.TabStop = false;
            this.lbRelapseDefCl.Tag = "LeishRepRelapseDefCl";
            this.lbRelapseDefCl.Text = "LeishRepRelapseDefCl";
            this.lbRelapseDefCl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbFailureDefVl
            // 
            this.lbFailureDefVl.AutoSize = true;
            this.lbFailureDefVl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbFailureDefVl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbFailureDefVl.Location = new System.Drawing.Point(0, 665);
            this.lbFailureDefVl.Margin = new System.Windows.Forms.Padding(0);
            this.lbFailureDefVl.Name = "lbFailureDefVl";
            this.lbFailureDefVl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbFailureDefVl.Size = new System.Drawing.Size(140, 45);
            this.lbFailureDefVl.TabIndex = 15;
            this.lbFailureDefVl.TabStop = false;
            this.lbFailureDefVl.Tag = "LeishRepFailureDefVl";
            this.lbFailureDefVl.Text = "LeishRepFailureDefVl";
            this.lbFailureDefVl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lbFailureDefCl
            // 
            this.lbFailureDefCl.AutoSize = true;
            this.lbFailureDefCl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbFailureDefCl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbFailureDefCl.Location = new System.Drawing.Point(0, 710);
            this.lbFailureDefCl.Margin = new System.Windows.Forms.Padding(0);
            this.lbFailureDefCl.Name = "lbFailureDefCl";
            this.lbFailureDefCl.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.lbFailureDefCl.Size = new System.Drawing.Size(142, 45);
            this.lbFailureDefCl.TabIndex = 16;
            this.lbFailureDefCl.TabStop = false;
            this.lbFailureDefCl.Tag = "LeishRepFailureDefCl";
            this.lbFailureDefCl.Text = "LeishRepFailureDefCl";
            this.lbFailureDefCl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepEndemicAdmin2Vl", true));
            this.textBox2.Location = new System.Drawing.Point(378, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(213, 21);
            this.textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepEndemicAdmin2Cl", true));
            this.textBox3.Location = new System.Drawing.Point(378, 93);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(213, 21);
            this.textBox3.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepYearLncpEstablished", true));
            this.textBox4.Location = new System.Drawing.Point(378, 138);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(213, 21);
            this.textBox4.TabIndex = 4;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepUrlLncp", true));
            this.textBox5.Location = new System.Drawing.Point(378, 198);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(213, 21);
            this.textBox5.TabIndex = 5;
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepYearLatestGuide", true));
            this.textBox6.Location = new System.Drawing.Point(378, 243);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(213, 21);
            this.textBox6.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.questionDataSource, "LeishRepIsNotifiable", true));
            this.checkBox1.Location = new System.Drawing.Point(378, 288);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.checkBox1.Size = new System.Drawing.Size(15, 44);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.questionDataSource, "LeishRepIsVectProg", true));
            this.checkBox2.Location = new System.Drawing.Point(378, 338);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.checkBox2.Size = new System.Drawing.Size(15, 44);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.questionDataSource, "LeishRepIsHostProg", true));
            this.checkBox3.Location = new System.Drawing.Point(378, 388);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.checkBox3.Size = new System.Drawing.Size(15, 44);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepNumHealthFac", true));
            this.textBox7.Location = new System.Drawing.Point(378, 438);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(213, 21);
            this.textBox7.TabIndex = 10;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.questionDataSource, "LeishRepIsTreatFree", true));
            this.checkBox4.Location = new System.Drawing.Point(378, 483);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.checkBox4.Size = new System.Drawing.Size(15, 44);
            this.checkBox4.TabIndex = 11;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepRelapseDefVl", true));
            this.textBox9.Location = new System.Drawing.Point(378, 578);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(213, 21);
            this.textBox9.TabIndex = 13;
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepRelapseDefCl", true));
            this.textBox10.Location = new System.Drawing.Point(378, 623);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(213, 21);
            this.textBox10.TabIndex = 14;
            // 
            // textBox11
            // 
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepFailureDefVl", true));
            this.textBox11.Location = new System.Drawing.Point(378, 668);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(213, 21);
            this.textBox11.TabIndex = 15;
            // 
            // textBox12
            // 
            this.textBox12.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.questionDataSource, "LeishRepFailureDefCl", true));
            this.textBox12.Location = new System.Drawing.Point(378, 713);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(213, 21);
            this.textBox12.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbYear, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalAdmin2Vl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalAdmin2Cl, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbYearLncpEstablished, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbRepUrlLncp, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbYearLatestGuide, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbIsNotifiable, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbIsVectProg, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lbIsHostProg, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbNumHealthFac, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lbIsTreatFree, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lbAntiMedInNml, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lbRelapseDefVl, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.lbRelapseDefCl, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.lbFailureDefVl, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.lbFailureDefCl, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.checkBox2, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.checkBox3, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.textBox7, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.checkBox4, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.textBox9, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.textBox10, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.textBox11, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.textBox12, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.antiLeishMedCb, 1, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 16;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 1200);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // antiLeishMedCb
            // 
            this.antiLeishMedCb.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.questionDataSource, "LeishRepAntiMedInNml", true));
            this.antiLeishMedCb.DisplayMember = "DisplayName";
            this.antiLeishMedCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.antiLeishMedCb.FormattingEnabled = true;
            this.antiLeishMedCb.Location = new System.Drawing.Point(378, 533);
            this.antiLeishMedCb.Name = "antiLeishMedCb";
            this.antiLeishMedCb.Size = new System.Drawing.Size(213, 23);
            this.antiLeishMedCb.TabIndex = 12;
            this.antiLeishMedCb.ValueMember = "DisplayName";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LeishReportStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LeishReportStep";
            this.Size = new System.Drawing.Size(750, 1200);
            this.Load += new System.EventHandler(this.LeishReportStep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionDataSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.H3Required lbYear;
        private System.Windows.Forms.TextBox textBox1;
        private Controls.H3Required lbTotalAdmin2Vl;
        private Controls.H3Required lbTotalAdmin2Cl;
        private Controls.H3Required lbYearLncpEstablished;
        private Controls.H3Required lbRepUrlLncp;
        private Controls.H3Required lbYearLatestGuide;
        private Controls.H3Required lbIsNotifiable;
        private Controls.H3Required lbIsVectProg;
        private Controls.H3Required lbIsHostProg;
        private Controls.H3Required lbNumHealthFac;
        private Controls.H3Required lbIsTreatFree;
        private Controls.H3Required lbAntiMedInNml;
        private Controls.H3Required lbRelapseDefVl;
        private Controls.H3Required lbRelapseDefCl;
        private Controls.H3Required lbFailureDefVl;
        private Controls.H3Required lbFailureDefCl;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource questionDataSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox antiLeishMedCb;

    }
}

namespace Nada.UI.View.Wizard
{
    partial class MergeYear
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblMessage = new Nada.UI.Controls.H3bLabel();
            this.rbtnCalendar = new System.Windows.Forms.RadioButton();
            this.rbtnCustom = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(109, 16);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Tag = "SplittingMergeYear";
            this.lblMessage.Text = "SplittingMergeYear";
            this.lblMessage.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // rbtnCalendar
            // 
            this.rbtnCalendar.AutoSize = true;
            this.rbtnCalendar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbtnCalendar.Checked = true;
            this.rbtnCalendar.Location = new System.Drawing.Point(3, 3);
            this.rbtnCalendar.Name = "rbtnCalendar";
            this.rbtnCalendar.Size = new System.Drawing.Size(100, 19);
            this.rbtnCalendar.TabIndex = 6;
            this.rbtnCalendar.TabStop = true;
            this.rbtnCalendar.Tag = "CalendarYear";
            this.rbtnCalendar.Text = "CalendarYear";
            this.rbtnCalendar.UseVisualStyleBackColor = true;
            // 
            // rbtnCustom
            // 
            this.rbtnCustom.AutoSize = true;
            this.rbtnCustom.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbtnCustom.Location = new System.Drawing.Point(3, 28);
            this.rbtnCustom.Name = "rbtnCustom";
            this.rbtnCustom.Size = new System.Drawing.Size(93, 19);
            this.rbtnCustom.TabIndex = 7;
            this.rbtnCustom.Tag = "CustomYear";
            this.rbtnCustom.Text = "CustomYear";
            this.rbtnCustom.UseVisualStyleBackColor = true;
            this.rbtnCustom.CheckedChanged += new System.EventHandler(this.rbtnCustom_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.rbtnCalendar);
            this.panel1.Controls.Add(this.rbtnCustom);
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 79);
            this.panel1.TabIndex = 8;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.bindingSource1;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.ValueMember = "Id";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Globalization.MonthItem);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(139, 101);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // MergeYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "MergeYear";
            this.Size = new System.Drawing.Size(145, 107);
            this.Load += new System.EventHandler(this.BackupForRedistricting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Controls.H3bLabel lblMessage;
        private System.Windows.Forms.RadioButton rbtnCalendar;
        private System.Windows.Forms.RadioButton rbtnCustom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource bindingSource1;


    }
}

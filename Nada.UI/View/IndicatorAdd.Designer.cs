﻿namespace Nada.UI.View
{
    partial class IndicatorAdd
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
            this.btnAddNew = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.bsIndicator = new System.Windows.Forms.BindingSource(this.components);
            this.tb1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(159, 231);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 25);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Values.Text = "Save";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tb3
            // 
            this.tb3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsIndicator, "SortOrder", true));
            this.tb3.Location = new System.Drawing.Point(27, 162);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(78, 20);
            this.tb3.TabIndex = 2;
            // 
            // bsIndicator
            // 
            this.bsIndicator.DataSource = typeof(Nada.Model.Indicator);
            // 
            // tb1
            // 
            this.tb1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsIndicator, "DisplayName", true));
            this.tb1.Location = new System.Drawing.Point(27, 71);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(207, 20);
            this.tb1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsIndicator, "IsDisabled", true));
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkBox1.Location = new System.Drawing.Point(27, 192);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 20);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Is Disabled";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsIndicator, "DataType", true));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Text",
            "Number",
            "Yes/No",
            "Date"});
            this.comboBox1.Location = new System.Drawing.Point(27, 116);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(207, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(68, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(27, 52);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(144, 16);
            this.h3Label1.TabIndex = 10;
            this.h3Label1.Tag = "CustomIndicatorName";
            this.h3Label1.Text = "Custom indicator name";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(27, 94);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(66, 16);
            this.h3Label2.TabIndex = 11;
            this.h3Label2.Tag = "DataType";
            this.h3Label2.Text = "Data type";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(27, 140);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(67, 16);
            this.h3Label3.TabIndex = 12;
            this.h3Label3.Tag = "SortOrder";
            this.h3Label3.Text = "Sort order";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Size = new System.Drawing.Size(264, 5);
            this.hr1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.label3.Location = new System.Drawing.Point(22, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 28);
            this.label3.TabIndex = 14;
            this.label3.Tag = "CustomIndicator";
            this.label3.Text = "Custom Indicator";
            // 
            // IndicatorAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(264, 284);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.h3Label3);
            this.Controls.Add(this.h3Label2);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tb3);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.btnAddNew);
            this.Name = "IndicatorAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "CustomIndicator";
            this.Text = "Custom Indicator";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsIndicator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.BindingSource bsIndicator;
        private System.Windows.Forms.ComboBox comboBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private Controls.H3Label h3Label1;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label3;
        private Controls.HR hr1;
        private System.Windows.Forms.Label label3;
    }
}
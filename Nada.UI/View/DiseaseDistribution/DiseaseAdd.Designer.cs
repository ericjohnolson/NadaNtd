namespace Nada.UI.View
{
    partial class DiseaseAdd
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 175);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(245, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(82, 17);
            this.lblLastUpdated.Text = "Last Updated: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.label3.Location = new System.Drawing.Point(21, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 28);
            this.label3.TabIndex = 24;
            this.label3.Tag = "Disease";
            this.label3.Text = "Disease";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(43)))), ((int)(((byte)(115)))));
            this.hr1.Size = new System.Drawing.Size(245, 5);
            this.hr1.TabIndex = 23;
            // 
            // tb1
            // 
            this.tb1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DisplayName", true));
            this.tb1.Location = new System.Drawing.Point(26, 70);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(196, 20);
            this.tb1.TabIndex = 25;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Model.Diseases.Disease);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(26, 127);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(186, 31);
            this.tableLayoutPanel4.TabIndex = 42;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(106, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Tag = "Cancel";
            this.kryptonButton1.Values.Text = "Cancel";
            this.kryptonButton1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Tag = "Save";
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // h3Required1
            // 
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(26, 51);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(58, 16);
            this.h3Required1.TabIndex = 43;
            this.h3Required1.Tag = "Name";
            this.h3Required1.Text = "Name";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSource1;
            // 
            // DiseaseAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(245, 197);
            this.Controls.Add(this.h3Required1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "DiseaseAdd";
            this.Tag = "Disease";
            this.Text = "Disease";
            this.Load += new System.EventHandler(this.DistributionMethodAdd_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastUpdated;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
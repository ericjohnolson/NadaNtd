namespace Nada.UI.View.Wizard
{
    partial class StepCountrySettings
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
            this.countryView1 = new Nada.UI.View.Demography.CountryView();
            this.adminLevelTypesControl1 = new Nada.UI.Controls.AdminLevelTypesControl();
            this.h3bLabel1 = new Nada.UI.Controls.H3bLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // countryView1
            // 
            this.countryView1.AutoSize = true;
            this.countryView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.countryView1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.countryView1.BackColor = System.Drawing.Color.White;
            this.countryView1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryView1.Location = new System.Drawing.Point(3, 3);
            this.countryView1.Name = "countryView1";
            this.countryView1.Size = new System.Drawing.Size(305, 54);
            this.countryView1.TabIndex = 0;
            // 
            // adminLevelTypesControl1
            // 
            this.adminLevelTypesControl1.AutoSize = true;
            this.adminLevelTypesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelTypesControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLevelTypesControl1.Location = new System.Drawing.Point(3, 19);
            this.adminLevelTypesControl1.Name = "adminLevelTypesControl1";
            this.adminLevelTypesControl1.Size = new System.Drawing.Size(586, 262);
            this.adminLevelTypesControl1.TabIndex = 1;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.h3bLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.adminLevelTypesControl1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 284);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // StepCountrySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.countryView1);
            this.Name = "StepCountrySettings";
            this.Size = new System.Drawing.Size(601, 350);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Demography.CountryView countryView1;
        private Controls.AdminLevelTypesControl adminLevelTypesControl1;
        private Controls.H3bLabel h3bLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}

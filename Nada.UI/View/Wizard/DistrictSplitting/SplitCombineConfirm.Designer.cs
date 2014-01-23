namespace Nada.UI.View.Wizard
{
    partial class SplitCombineConfirm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bsImportOptions = new System.Windows.Forms.BindingSource(this.components);
            this.typeListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblNewUnits = new System.Windows.Forms.TableLayoutPanel();
            this.h3bLabel3 = new Nada.UI.Controls.H3bLabel();
            this.h3bLabel1 = new Nada.UI.Controls.H3bLabel();
            this.h3bLabel2 = new Nada.UI.Controls.H3bLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).BeginInit();
            this.tblNewUnits.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsImportOptions;
            // 
            // bsImportOptions
            // 
            this.bsImportOptions.DataSource = typeof(Nada.Model.Imports.ImportOptions);
            // 
            // typeListItemBindingSource
            // 
            this.typeListItemBindingSource.DataSource = typeof(Nada.Model.Imports.TypeListItem);
            // 
            // tblNewUnits
            // 
            this.tblNewUnits.AutoSize = true;
            this.tblNewUnits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNewUnits.ColumnCount = 3;
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblNewUnits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblNewUnits.Controls.Add(this.h3bLabel3, 2, 0);
            this.tblNewUnits.Controls.Add(this.h3bLabel1, 0, 0);
            this.tblNewUnits.Controls.Add(this.h3bLabel2, 1, 0);
            this.tblNewUnits.Location = new System.Drawing.Point(3, 3);
            this.tblNewUnits.Name = "tblNewUnits";
            this.tblNewUnits.RowCount = 1;
            this.tblNewUnits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblNewUnits.Size = new System.Drawing.Size(349, 19);
            this.tblNewUnits.TabIndex = 0;
            // 
            // h3bLabel3
            // 
            this.h3bLabel3.AutoSize = true;
            this.h3bLabel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel3.Location = new System.Drawing.Point(229, 0);
            this.h3bLabel3.Margin = new System.Windows.Forms.Padding(0, 0, 20, 3);
            this.h3bLabel3.Name = "h3bLabel3";
            this.h3bLabel3.Size = new System.Drawing.Size(100, 16);
            this.h3bLabel3.TabIndex = 2;
            this.h3bLabel3.Tag = "ChildAdminUnits";
            this.h3bLabel3.Text = "ChildAdminUnits";
            this.h3bLabel3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3bLabel1
            // 
            this.h3bLabel1.AutoSize = true;
            this.h3bLabel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel1.Location = new System.Drawing.Point(0, 0);
            this.h3bLabel1.Margin = new System.Windows.Forms.Padding(0, 0, 20, 3);
            this.h3bLabel1.Name = "h3bLabel1";
            this.h3bLabel1.Size = new System.Drawing.Size(123, 16);
            this.h3bLabel1.TabIndex = 0;
            this.h3bLabel1.Tag = "NewAdminUnitName";
            this.h3bLabel1.Text = "NewAdminUnitName";
            this.h3bLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3bLabel2
            // 
            this.h3bLabel2.AutoSize = true;
            this.h3bLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel2.Location = new System.Drawing.Point(143, 0);
            this.h3bLabel2.Margin = new System.Windows.Forms.Padding(0, 0, 20, 3);
            this.h3bLabel2.Name = "h3bLabel2";
            this.h3bLabel2.Size = new System.Drawing.Size(66, 16);
            this.h3bLabel2.TabIndex = 1;
            this.h3bLabel2.Tag = "Population";
            this.h3bLabel2.Text = "Population";
            this.h3bLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // SplitCombineConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblNewUnits);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "SplitCombineConfirm";
            this.Size = new System.Drawing.Size(355, 25);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).EndInit();
            this.tblNewUnits.ResumeLayout(false);
            this.tblNewUnits.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsImportOptions;
        private System.Windows.Forms.BindingSource typeListItemBindingSource;
        private System.Windows.Forms.TableLayoutPanel tblNewUnits;
        private Controls.H3bLabel h3bLabel1;
        private Controls.H3bLabel h3bLabel2;
        private Controls.H3bLabel h3bLabel3;

    }
}

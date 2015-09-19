namespace Nada.UI.View.Wizard
{
    partial class ImportStepType
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
            this.lnkDownload = new Nada.UI.Controls.H3Link();
            this.lnkUpload = new Nada.UI.Controls.H3Link();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.h3Label1 = new Nada.UI.Controls.H3Required();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.bsImportOptions = new System.Windows.Forms.BindingSource(this.components);
            this.typeListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lnkValidate = new Nada.UI.Controls.H3Link();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkDownload.BackColor = System.Drawing.Color.Transparent;
            this.lnkDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownload.Location = new System.Drawing.Point(0, 57);
            this.lnkDownload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(99, 15);
            this.lnkDownload.TabIndex = 0;
            this.lnkDownload.Tag = "CreateImportFile";
            this.lnkDownload.Text = "CreateImportFile";
            this.lnkDownload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkDownload.ClickOverride += new System.Action(this.lnkDownload_ClickOverride);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpload.Location = new System.Drawing.Point(0, 105);
            this.lnkUpload.Margin = new System.Windows.Forms.Padding(0);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(67, 15);
            this.lnkUpload.TabIndex = 1;
            this.lnkUpload.Tag = "UploadFile";
            this.lnkUpload.Text = "UploadFile";
            this.lnkUpload.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkUpload.ClickOverride += new System.Action(this.lnkUpload_ClickOverride);
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(44, 15);
            this.h3Label1.TabIndex = 21;
            this.h3Label1.Tag = "Type";
            this.h3Label1.Text = "Type";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbTypes
            // 
            this.cbTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsImportOptions, "TypeId", true));
            this.cbTypes.DataSource = this.typeListItemBindingSource;
            this.cbTypes.DisplayMember = "Name";
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(3, 22);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(255, 23);
            this.cbTypes.TabIndex = 20;
            this.cbTypes.ValueMember = "Id";
            // 
            // bsImportOptions
            // 
            this.bsImportOptions.DataSource = typeof(Nada.Model.Imports.ImportOptions);
            // 
            // typeListItemBindingSource
            // 
            this.typeListItemBindingSource.DataSource = typeof(Nada.Model.Imports.TypeListItem);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // lnkValidate
            // 
            this.lnkValidate.AutoSize = true;
            this.lnkValidate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkValidate.BackColor = System.Drawing.Color.Transparent;
            this.lnkValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkValidate.Location = new System.Drawing.Point(0, 81);
            this.lnkValidate.Margin = new System.Windows.Forms.Padding(0);
            this.lnkValidate.Name = "lnkValidate";
            this.lnkValidate.Size = new System.Drawing.Size(169, 15);
            this.lnkValidate.TabIndex = 22;
            this.lnkValidate.Tag = "CheckUploadValidationErrors";
            this.lnkValidate.Text = "CheckUploadValidationErrors";
            this.lnkValidate.TextColor = System.Drawing.Color.RoyalBlue;
            this.lnkValidate.Visible = false;
            this.lnkValidate.ClickOverride += new System.Action(this.lnkValidate_ClickOverride);
            // 
            // ImportStepType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkValidate);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkDownload);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "ImportStepType";
            this.Size = new System.Drawing.Size(288, 120);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsImportOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeListItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Link lnkDownload;
        private Controls.H3Link lnkUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.H3Required h3Label1;
        private System.Windows.Forms.ComboBox cbTypes;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsImportOptions;
        private System.Windows.Forms.BindingSource typeListItemBindingSource;
        private Controls.H3Link lnkValidate;

    }
}

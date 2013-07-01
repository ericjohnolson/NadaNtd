namespace Nada.UI.View.Demography
{
    partial class ImportDemographyModal
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
            this.btnDownload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnUpload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lbAvailable = new System.Windows.Forms.ListBox();
            this.lbSelected = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSelector = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bsAvailable = new System.Windows.Forms.BindingSource(this.components);
            this.bsSelected = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(212, 259);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDownload.Size = new System.Drawing.Size(120, 25);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Values.Text = "Download CSV";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(338, 259);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(120, 25);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Values.Text = "Upload CSV";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lbAvailable
            // 
            this.lbAvailable.DataSource = this.bsAvailable;
            this.lbAvailable.DisplayMember = "Name";
            this.lbAvailable.FormattingEnabled = true;
            this.lbAvailable.Location = new System.Drawing.Point(12, 16);
            this.lbAvailable.Name = "lbAvailable";
            this.lbAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAvailable.Size = new System.Drawing.Size(185, 199);
            this.lbAvailable.TabIndex = 12;
            this.lbAvailable.ValueMember = "Id";
            // 
            // lbSelected
            // 
            this.lbSelected.DataSource = this.bsSelected;
            this.lbSelected.DisplayMember = "Name";
            this.lbSelected.FormattingEnabled = true;
            this.lbSelected.Location = new System.Drawing.Point(268, 16);
            this.lbSelected.Name = "lbSelected";
            this.lbSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelected.Size = new System.Drawing.Size(185, 199);
            this.lbSelected.TabIndex = 13;
            this.lbSelected.ValueMember = "Id";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(203, 57);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelectAll.Size = new System.Drawing.Size(59, 25);
            this.btnSelectAll.TabIndex = 14;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(203, 150);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselectAll.Size = new System.Drawing.Size(59, 25);
            this.btnDeselectAll.TabIndex = 15;
            this.btnDeselectAll.Values.Text = "<<";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(203, 88);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnSelect.Size = new System.Drawing.Size(59, 25);
            this.btnSelect.TabIndex = 16;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(203, 119);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnDeselect.Size = new System.Drawing.Size(59, 25);
            this.btnDeselect.TabIndex = 17;
            this.btnDeselect.Values.Text = "<";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Available";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Selected";
            // 
            // pnlSelector
            // 
            this.pnlSelector.Controls.Add(this.label1);
            this.pnlSelector.Controls.Add(this.label2);
            this.pnlSelector.Controls.Add(this.lbAvailable);
            this.pnlSelector.Controls.Add(this.lbSelected);
            this.pnlSelector.Controls.Add(this.btnDeselect);
            this.pnlSelector.Controls.Add(this.btnSelectAll);
            this.pnlSelector.Controls.Add(this.btnSelect);
            this.pnlSelector.Controls.Add(this.btnDeselectAll);
            this.pnlSelector.Location = new System.Drawing.Point(5, 12);
            this.pnlSelector.Name = "pnlSelector";
            this.pnlSelector.Size = new System.Drawing.Size(457, 231);
            this.pnlSelector.TabIndex = 20;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bsAvailable
            // 
            this.bsAvailable.DataSource = typeof(Nada.Model.AdminLevel);
            // 
            // bsSelected
            // 
            this.bsSelected.DataSource = typeof(Nada.Model.AdminLevel);
            // 
            // ImportDemographyModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(474, 296);
            this.Controls.Add(this.pnlSelector);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDownload);
            this.Name = "ImportDemographyModal";
            this.Text = "Import Demography";
            this.Load += new System.EventHandler(this.ImportDemographyModal_Load);
            this.pnlSelector.ResumeLayout(false);
            this.pnlSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownload;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpload;
        private System.Windows.Forms.ListBox lbAvailable;
        private System.Windows.Forms.ListBox lbSelected;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeselect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlSelector;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.BindingSource bsAvailable;
        private System.Windows.Forms.BindingSource bsSelected;
    }
}
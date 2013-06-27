namespace Nada.UI.View
{
    partial class DiseaseModal
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
            this.lblCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.translatedNamesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lnkAdd = new System.Windows.Forms.LinkLabel();
            this.cbShow = new System.Windows.Forms.CheckBox();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsDisease = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.translatedNamesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDisease)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(9, 13);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(32, 13);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Code";
            // 
            // tbCode
            // 
            this.tbCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDisease, "Code", true));
            this.tbCode.Location = new System.Drawing.Point(12, 29);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(103, 20);
            this.tbCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Translated Names";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 239);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AllowUserToAddRows = false;
            this.kryptonDataGridView1.AllowUserToDeleteRows = false;
            this.kryptonDataGridView1.AutoGenerateColumns = false;
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.languageDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.kryptonDataGridView1.DataSource = this.translatedNamesBindingSource;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(12, 86);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.Size = new System.Drawing.Size(291, 135);
            this.kryptonDataGridView1.TabIndex = 5;
            // 
            // translatedNamesBindingSource
            // 
            this.translatedNamesBindingSource.DataMember = "TranslatedNames";
            this.translatedNamesBindingSource.DataSource = this.bsDisease;
            // 
            // lnkAdd
            // 
            this.lnkAdd.AutoSize = true;
            this.lnkAdd.Location = new System.Drawing.Point(108, 62);
            this.lnkAdd.Name = "lnkAdd";
            this.lnkAdd.Size = new System.Drawing.Size(26, 13);
            this.lnkAdd.TabIndex = 6;
            this.lnkAdd.TabStop = true;
            this.lnkAdd.Text = "Add";
            this.lnkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdd_LinkClicked);
            // 
            // cbShow
            // 
            this.cbShow.AutoSize = true;
            this.cbShow.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsDisease, "IsEnabled", true));
            this.cbShow.Location = new System.Drawing.Point(174, 13);
            this.cbShow.Name = "cbShow";
            this.cbShow.Size = new System.Drawing.Size(59, 17);
            this.cbShow.TabIndex = 8;
            this.cbShow.Text = "Show?";
            this.cbShow.UseVisualStyleBackColor = true;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // bsDisease
            // 
            this.bsDisease.DataSource = typeof(Nada.Model.Disease);
            // 
            // DiseaseModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(315, 276);
            this.Controls.Add(this.cbShow);
            this.Controls.Add(this.lnkAdd);
            this.Controls.Add(this.kryptonDataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lblCode);
            this.Name = "DiseaseModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Disease";
            this.Load += new System.EventHandler(this.DiseaseModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.translatedNamesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDisease)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private System.Windows.Forms.BindingSource bsDisease;
        private System.Windows.Forms.BindingSource translatedNamesBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.LinkLabel lnkAdd;
        private System.Windows.Forms.CheckBox cbShow;
    }
}
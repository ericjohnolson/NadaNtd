namespace Nada.UI.View
{
    partial class TranslationModal
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bsTranslation = new System.Windows.Forms.BindingSource(this.components);
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.languageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.bsTranslation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Language";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Translated Text";
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTranslation, "Value", true));
            this.tbName.Location = new System.Drawing.Point(16, 79);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 20);
            this.tbName.TabIndex = 3;
            // 
            // bsTranslation
            // 
            this.bsTranslation.DataSource = typeof(Nada.Model.TranslatedValue);
            // 
            // cbLang
            // 
            this.cbLang.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsTranslation, "IsoCode", true));
            this.cbLang.DataSource = this.languageBindingSource;
            this.cbLang.DisplayMember = "Name";
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Location = new System.Drawing.Point(16, 30);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(158, 21);
            this.cbLang.TabIndex = 4;
            this.cbLang.ValueMember = "IsoCode";
            // 
            // languageBindingSource
            // 
            this.languageBindingSource.DataSource = typeof(Nada.Model.Language);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(97, 114);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Values.Text = "Add";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // TranslationModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(195, 151);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.cbLang);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TranslationModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Translation";
            this.Load += new System.EventHandler(this.TranslationModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsTranslation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox cbLang;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.BindingSource bsTranslation;
        private System.Windows.Forms.BindingSource languageBindingSource;
    }
}
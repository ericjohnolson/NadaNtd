namespace Nada.UI.View
{
    partial class PopGroupModal
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
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bsPopGroup = new System.Windows.Forms.BindingSource(this.components);
            this.tbAbbreviation = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsPopGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Abbreviation";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(111, 127);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Values.Text = "Add";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPopGroup, "DisplayName", true));
            this.tbName.Location = new System.Drawing.Point(30, 43);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 20);
            this.tbName.TabIndex = 6;
            // 
            // bsPopGroup
            // 
            this.bsPopGroup.DataSource = typeof(Nada.Model.PopGroup);
            // 
            // tbAbbreviation
            // 
            this.tbAbbreviation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPopGroup, "Abbreviation", true));
            this.tbAbbreviation.Location = new System.Drawing.Point(30, 91);
            this.tbAbbreviation.Name = "tbAbbreviation";
            this.tbAbbreviation.Size = new System.Drawing.Size(158, 20);
            this.tbAbbreviation.TabIndex = 7;
            // 
            // PopGroupModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(223, 179);
            this.Controls.Add(this.tbAbbreviation);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PopGroupModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Population Group";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsPopGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbAbbreviation;
        private System.Windows.Forms.BindingSource bsPopGroup;
    }
}
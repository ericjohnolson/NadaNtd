namespace Nada.UI.Controls
{
    partial class FundersControl
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
            this.partnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbPartners = new System.Windows.Forms.ListBox();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // partnerBindingSource
            // 
            this.partnerBindingSource.DataSource = typeof(Nada.Model.Partner);
            // 
            // lbPartners
            // 
            this.lbPartners.DataSource = this.partnerBindingSource;
            this.lbPartners.DisplayMember = "DisplayName";
            this.lbPartners.FormattingEnabled = true;
            this.lbPartners.Location = new System.Drawing.Point(3, 3);
            this.lbPartners.Name = "lbPartners";
            this.lbPartners.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbPartners.Size = new System.Drawing.Size(198, 95);
            this.lbPartners.TabIndex = 4;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(3, 101);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(69, 12);
            this.fieldLink1.TabIndex = 5;
            this.fieldLink1.Tag = "AddPartnerLink";
            this.fieldLink1.Text = "AddPartnerLink";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // FundersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.fieldLink1);
            this.Controls.Add(this.lbPartners);
            this.Name = "FundersControl";
            this.Size = new System.Drawing.Size(204, 113);
            this.Load += new System.EventHandler(this.FundersControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.partnerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource partnerBindingSource;
        private System.Windows.Forms.ListBox lbPartners;
        private FieldLink fieldLink1;

    }
}

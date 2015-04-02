namespace Nada.UI.View.Wizard
{
    partial class ImportStepMultiAuLabels
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
            this.h3Label1 = new Nada.UI.Controls.H3Required();
            this.fieldLink1 = new Nada.UI.Controls.H3Link();
            this.lbSurveyNames = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(0, 0);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(77, 15);
            this.h3Label1.TabIndex = 21;
            this.h3Label1.Tag = "SurveyName";
            this.h3Label1.Text = "SurveyName";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(3, 132);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(87, 15);
            this.fieldLink1.TabIndex = 23;
            this.fieldLink1.Tag = "AddSurveyLink";
            this.fieldLink1.Text = "AddSurveyLink";
            this.fieldLink1.TextColor = System.Drawing.Color.RoyalBlue;
            this.fieldLink1.ClickOverride += new System.Action(this.fieldLink1_ClickOverride);
            // 
            // lbSurveyNames
            // 
            this.lbSurveyNames.DisplayMember = "DisplayName";
            this.lbSurveyNames.FormattingEnabled = true;
            this.lbSurveyNames.ItemHeight = 15;
            this.lbSurveyNames.Location = new System.Drawing.Point(3, 18);
            this.lbSurveyNames.Name = "lbSurveyNames";
            this.lbSurveyNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSurveyNames.Size = new System.Drawing.Size(230, 109);
            this.lbSurveyNames.TabIndex = 22;
            // 
            // ImportStepMultiAuLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.fieldLink1);
            this.Controls.Add(this.lbSurveyNames);
            this.Controls.Add(this.h3Label1);
            this.Margin = new System.Windows.Forms.Padding(23);
            this.Name = "ImportStepMultiAuLabels";
            this.Size = new System.Drawing.Size(236, 147);
            this.Load += new System.EventHandler(this.ImportStepMultiAuLabels_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Required h3Label1;
        private Controls.H3Link fieldLink1;
        private System.Windows.Forms.ListBox lbSurveyNames;

    }
}

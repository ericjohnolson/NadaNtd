namespace Nada.UI.View.Reports.CustomReport
{
    partial class StepCategory
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
            this.lnkDistro = new Nada.UI.Controls.H3Link();
            this.lnkSurvey = new Nada.UI.Controls.H3Link();
            this.lnkIntv = new Nada.UI.Controls.H3Link();
            this.lnkTraining = new Nada.UI.Controls.H3Link();
            this.SuspendLayout();
            // 
            // lnkDistro
            // 
            this.lnkDistro.AutoSize = true;
            this.lnkDistro.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkDistro.BackColor = System.Drawing.Color.Transparent;
            this.lnkDistro.Enabled = false;
            this.lnkDistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDistro.Location = new System.Drawing.Point(0, 0);
            this.lnkDistro.Margin = new System.Windows.Forms.Padding(0);
            this.lnkDistro.Name = "lnkDistro";
            this.lnkDistro.Size = new System.Drawing.Size(128, 16);
            this.lnkDistro.TabIndex = 0;
            this.lnkDistro.Tag = "DiseaseDistribution";
            this.lnkDistro.Text = "Disease Distribution";
            this.lnkDistro.ClickOverride += new System.Action(this.lnkDistro_ClickOverride);
            // 
            // lnkSurvey
            // 
            this.lnkSurvey.AutoSize = true;
            this.lnkSurvey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkSurvey.BackColor = System.Drawing.Color.Transparent;
            this.lnkSurvey.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSurvey.Location = new System.Drawing.Point(0, 25);
            this.lnkSurvey.Margin = new System.Windows.Forms.Padding(0);
            this.lnkSurvey.Name = "lnkSurvey";
            this.lnkSurvey.Size = new System.Drawing.Size(57, 16);
            this.lnkSurvey.TabIndex = 1;
            this.lnkSurvey.Tag = "Surveys";
            this.lnkSurvey.Text = "Surveys";
            this.lnkSurvey.ClickOverride += new System.Action(this.lnkSurvey_ClickOverride);
            // 
            // lnkIntv
            // 
            this.lnkIntv.AutoSize = true;
            this.lnkIntv.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkIntv.BackColor = System.Drawing.Color.Transparent;
            this.lnkIntv.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkIntv.Location = new System.Drawing.Point(0, 51);
            this.lnkIntv.Margin = new System.Windows.Forms.Padding(0);
            this.lnkIntv.Name = "lnkIntv";
            this.lnkIntv.Size = new System.Drawing.Size(83, 16);
            this.lnkIntv.TabIndex = 2;
            this.lnkIntv.Tag = "Interventions";
            this.lnkIntv.Text = "Interventions";
            this.lnkIntv.ClickOverride += new System.Action(this.lnkIntv_ClickOverride);
            // 
            // lnkTraining
            // 
            this.lnkTraining.AutoSize = true;
            this.lnkTraining.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkTraining.BackColor = System.Drawing.Color.Transparent;
            this.lnkTraining.Enabled = false;
            this.lnkTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTraining.Location = new System.Drawing.Point(0, 76);
            this.lnkTraining.Margin = new System.Windows.Forms.Padding(0);
            this.lnkTraining.Name = "lnkTraining";
            this.lnkTraining.Size = new System.Drawing.Size(57, 16);
            this.lnkTraining.TabIndex = 3;
            this.lnkTraining.Tag = "Training";
            this.lnkTraining.Text = "Training";
            this.lnkTraining.ClickOverride += new System.Action(this.lnkTraining_ClickOverride);
            // 
            // StepCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkTraining);
            this.Controls.Add(this.lnkIntv);
            this.Controls.Add(this.lnkSurvey);
            this.Controls.Add(this.lnkDistro);
            this.Margin = new System.Windows.Forms.Padding(20);
            this.Name = "StepCategory";
            this.Size = new System.Drawing.Size(128, 92);
            this.Load += new System.EventHandler(this.StepCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Link lnkDistro;
        private Controls.H3Link lnkSurvey;
        private Controls.H3Link lnkIntv;
        private Controls.H3Link lnkTraining;

    }
}

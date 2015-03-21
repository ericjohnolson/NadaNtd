namespace Nada.UI.View.Reports.CustomReport
{
    partial class StepIndicators
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
            this.triStateTreeView1 = new RikTheVeggie.TriStateTreeView();
            this.SuspendLayout();
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView1.Location = new System.Drawing.Point(0, 0);
            this.triStateTreeView1.Name = "triStateTreeView1";
            this.triStateTreeView1.Size = new System.Drawing.Size(714, 372);
            this.triStateTreeView1.TabIndex = 14;
            this.triStateTreeView1.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Standard;
            // 
            // StepIndicators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.triStateTreeView1);
            this.Name = "StepIndicators";
            this.Size = new System.Drawing.Size(714, 372);
            this.Load += new System.EventHandler(this.StepIndicators_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RikTheVeggie.TriStateTreeView triStateTreeView1;


    }
}

namespace Nada.UI.View
{
    partial class CustomIndicatorControl
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
            this.tblIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.lblPlaceholder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tblIndicators
            // 
            this.tblIndicators.AutoSize = true;
            this.tblIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblIndicators.ColumnCount = 1;
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tblIndicators.Location = new System.Drawing.Point(3, 20);
            this.tblIndicators.Name = "tblIndicators";
            this.tblIndicators.RowCount = 1;
            this.tblIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIndicators.Size = new System.Drawing.Size(500, 0);
            this.tblIndicators.TabIndex = 0;
            // 
            // lblPlaceholder
            // 
            this.lblPlaceholder.AutoSize = true;
            this.lblPlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaceholder.Location = new System.Drawing.Point(3, 0);
            this.lblPlaceholder.Name = "lblPlaceholder";
            this.lblPlaceholder.Size = new System.Drawing.Size(156, 13);
            this.lblPlaceholder.TabIndex = 1;
            this.lblPlaceholder.Text = "Dynamic Indicators Placeholder";
            // 
            // CustomIndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblPlaceholder);
            this.Controls.Add(this.tblIndicators);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CustomIndicatorControl";
            this.Size = new System.Drawing.Size(506, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblIndicators;
        private System.Windows.Forms.Label lblPlaceholder;

    }
}

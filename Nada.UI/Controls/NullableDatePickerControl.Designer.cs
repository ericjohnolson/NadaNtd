namespace Nada.UI.Controls
{
    partial class NullableDatePickerControl
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
            this.h3Link1 = new Nada.UI.Controls.H3Link();
            this.nullableDateTimePicker1 = new Nada.UI.Controls.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // h3Link1
            // 
            this.h3Link1.AutoSize = true;
            this.h3Link1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Link1.BackColor = System.Drawing.Color.Transparent;
            this.h3Link1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h3Link1.Location = new System.Drawing.Point(200, 2);
            this.h3Link1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Link1.Name = "h3Link1";
            this.h3Link1.Size = new System.Drawing.Size(37, 15);
            this.h3Link1.TabIndex = 1;
            this.h3Link1.Tag = "Clear";
            this.h3Link1.Text = "Clear";
            this.h3Link1.ClickOverride += new System.Action(this.h3Link1_ClickOverride);
            this.h3Link1.Load += new System.EventHandler(this.h3Link1_Load);
            // 
            // nullableDateTimePicker1
            // 
            this.nullableDateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.nullableDateTimePicker1.Margin = new System.Windows.Forms.Padding(0);
            this.nullableDateTimePicker1.Name = "nullableDateTimePicker1";
            this.nullableDateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.nullableDateTimePicker1.TabIndex = 0;
            this.nullableDateTimePicker1.Value = new System.DateTime(2014, 3, 6, 14, 0, 1, 479);
            // 
            // NullableDatePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.h3Link1);
            this.Controls.Add(this.nullableDateTimePicker1);
            this.Name = "NullableDatePickerControl";
            this.Size = new System.Drawing.Size(237, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NullableDateTimePicker nullableDateTimePicker1;
        private H3Link h3Link1;
    }
}

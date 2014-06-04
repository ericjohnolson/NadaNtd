namespace Nada.UI.View.Wizard
{
    partial class StepDemoLevel
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
            this.h3Label1 = new Nada.UI.Controls.H3Required();
            this.cbLevels = new System.Windows.Forms.ComboBox();
            this.bsLevels = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.h3bLabel2 = new Nada.UI.Controls.H3Required();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).BeginInit();
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
            this.h3Label1.Size = new System.Drawing.Size(138, 18);
            this.h3Label1.TabIndex = 21;
            this.h3Label1.Tag = "LevelImplementation";
            this.h3Label1.Text = "Level of implementation";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // cbLevels
            // 
            this.cbLevels.DataSource = this.bsLevels;
            this.cbLevels.DisplayMember = "DisplayName";
            this.cbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevels.FormattingEnabled = true;
            this.cbLevels.Location = new System.Drawing.Point(3, 22);
            this.cbLevels.Name = "cbLevels";
            this.cbLevels.Size = new System.Drawing.Size(188, 23);
            this.cbLevels.TabIndex = 20;
            // 
            // bsLevels
            // 
            this.bsLevels.DataSource = typeof(Nada.Model.AdminLevelType);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 66);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(253, 21);
            this.dateTimePicker1.TabIndex = 70;
            // 
            // h3bLabel2
            // 
            this.h3bLabel2.AutoSize = true;
            this.h3bLabel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3bLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3bLabel2.Location = new System.Drawing.Point(3, 48);
            this.h3bLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.h3bLabel2.Name = "h3bLabel2";
            this.h3bLabel2.Size = new System.Drawing.Size(96, 15);
            this.h3bLabel2.TabIndex = 69;
            this.h3bLabel2.Tag = "DateReported";
            this.h3bLabel2.Text = "DateReported";
            this.h3bLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // StepDemoLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.h3bLabel2);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.cbLevels);
            this.Name = "StepDemoLevel";
            this.Size = new System.Drawing.Size(440, 272);
            this.Load += new System.EventHandler(this.ImportOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.H3Required h3Label1;
        private System.Windows.Forms.ComboBox cbLevels;
        private System.Windows.Forms.BindingSource bsLevels;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private Controls.H3Required h3bLabel2;



    }
}

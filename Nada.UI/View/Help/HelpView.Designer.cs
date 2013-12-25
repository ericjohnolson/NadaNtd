namespace Nada.UI.View
{
    partial class HelpView
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
            C1.Win.C1DynamicHelp.MapItem mapItem1 = new C1.Win.C1DynamicHelp.MapItem();
            C1.Win.C1DynamicHelp.MapItem mapItem2 = new C1.Win.C1DynamicHelp.MapItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.c1DynamicHelp1 = new C1.Win.C1DynamicHelp.C1DynamicHelp();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            mapItem1.HelpTopicTrigger = C1.Win.C1DynamicHelp.HelpTopicTrigger.Enter;
            mapItem1.ItemType = C1.Win.C1DynamicHelp.MapItemType.Static;
            mapItem1.ShowDefaultTopicFirst = true;
            mapItem1.UIElement = this.textBox1;
            mapItem1.Url = "IDH_Topic20.htm";
            mapItem1.UseDefaultTrigger = true;
            this.c1DynamicHelp1.SetHelpTopic(this.textBox1, mapItem1);
            this.textBox1.Location = new System.Drawing.Point(52, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mapItem2.HelpTopicTrigger = C1.Win.C1DynamicHelp.HelpTopicTrigger.Enter;
            mapItem2.ItemType = C1.Win.C1DynamicHelp.MapItemType.Static;
            mapItem2.ShowDefaultTopicFirst = true;
            mapItem2.UIElement = this.fieldLink1;
            mapItem2.Url = "IDH_Topic10.htm";
            mapItem2.UseDefaultTrigger = true;
            this.c1DynamicHelp1.SetHelpTopic(this.fieldLink1, mapItem2);
            this.fieldLink1.Location = new System.Drawing.Point(52, 116);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(64, 16);
            this.fieldLink1.TabIndex = 2;
            this.fieldLink1.Text = "fieldLink1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DynamicHelp1.SetHelpTopic(this.webBrowser1, null);
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(978, 693);
            this.webBrowser1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DynamicHelp1.SetHelpTopic(this.tabControl1, null);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 727);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.c1DynamicHelp1.SetHelpTopic(this.tabPage1, null);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(984, 699);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HTML";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fieldLink1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.c1DynamicHelp1);
            this.c1DynamicHelp1.SetHelpTopic(this.tabPage2, null);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(984, 699);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CHM";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // c1DynamicHelp1
            // 
            this.c1DynamicHelp1.HelpSource = "C:\\Development\\Nada\\NadaNtd\\Nada.UI\\View\\Help\\EricTest.chm";
            this.c1DynamicHelp1.Location = new System.Drawing.Point(404, 3);
            this.c1DynamicHelp1.Name = "c1DynamicHelp1";
            this.c1DynamicHelp1.Size = new System.Drawing.Size(577, 693);
            this.c1DynamicHelp1.TabIndex = 0;
            // 
            // HelpView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 727);
            this.Controls.Add(this.tabControl1);
            this.c1DynamicHelp1.SetHelpTopic(this, null);
            this.Name = "HelpView";
            this.Tag = "Help";
            this.Text = "Help";
            this.Load += new System.EventHandler(this.HelpView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private C1.Win.C1DynamicHelp.C1DynamicHelp c1DynamicHelp1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private Controls.FieldLink fieldLink1;
    }
}
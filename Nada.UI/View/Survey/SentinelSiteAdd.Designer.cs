namespace Nada.UI.View
{
    partial class SentinelSiteAdd
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
            this.btnAddNew = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tbLng = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSiteName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.bsSentinelSite = new System.Windows.Forms.BindingSource(this.components);
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(272, 279);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 25);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Values.Text = "Save";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tbLng
            // 
            this.tbLng.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Lng", true));
            this.tbLng.Location = new System.Drawing.Point(15, 147);
            this.tbLng.Name = "tbLng";
            this.tbLng.Size = new System.Drawing.Size(158, 20);
            this.tbLng.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Longitude";
            // 
            // tbLat
            // 
            this.tbLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Lat", true));
            this.tbLat.Location = new System.Drawing.Point(15, 108);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(158, 20);
            this.tbLat.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Latitude";
            // 
            // tbSiteName
            // 
            this.tbSiteName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "SiteName", true));
            this.tbSiteName.Location = new System.Drawing.Point(15, 25);
            this.tbSiteName.Name = "tbSiteName";
            this.tbSiteName.Size = new System.Drawing.Size(158, 20);
            this.tbSiteName.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Site Name";
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Notes", true));
            this.textBox10.Location = new System.Drawing.Point(15, 186);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(332, 87);
            this.textBox10.TabIndex = 39;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 170);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 38;
            this.label18.Text = "Notes";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(359, 22);
            this.statusStrip1.TabIndex = 40;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(82, 17);
            this.lblLastUpdated.Text = "Last Updated: ";
            // 
            // bsSentinelSite
            // 
            this.bsSentinelSite.DataSource = typeof(Nada.Model.Survey.SentinelSite);
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(11, 52);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(123, 37);
            this.adminLevelPickerControl1.TabIndex = 41;
            // 
            // SentinelSiteAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(359, 353);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tbLng);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbLat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSiteName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddNew);
            this.Name = "SentinelSiteAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sentinel Site";
            this.Load += new System.EventHandler(this.Modal_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private System.Windows.Forms.TextBox tbLng;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSiteName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.BindingSource bsSentinelSite;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastUpdated;
        private AdminLevelPickerControl adminLevelPickerControl1;
    }
}
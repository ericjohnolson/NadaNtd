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
            this.bsSentinelSite = new System.Windows.Forms.BindingSource(this.components);
            this.tbLat = new System.Windows.Forms.TextBox();
            this.tbSiteName = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.h3Label1 = new Nada.UI.Controls.H3Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.h3Label4 = new Nada.UI.Controls.H3Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(284, 325);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 25);
            this.btnAddNew.TabIndex = 6;
            this.btnAddNew.Values.Text = "Save";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tbLng
            // 
            this.tbLng.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Lng", true));
            this.tbLng.Location = new System.Drawing.Point(172, 168);
            this.tbLng.Name = "tbLng";
            this.tbLng.Size = new System.Drawing.Size(92, 20);
            this.tbLng.TabIndex = 3;
            // 
            // bsSentinelSite
            // 
            this.bsSentinelSite.DataSource = typeof(Nada.Model.Survey.SentinelSite);
            // 
            // tbLat
            // 
            this.tbLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Lat", true));
            this.tbLat.Location = new System.Drawing.Point(27, 168);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(92, 20);
            this.tbLat.TabIndex = 2;
            // 
            // tbSiteName
            // 
            this.tbSiteName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "SiteName", true));
            this.tbSiteName.Location = new System.Drawing.Point(27, 117);
            this.tbSiteName.Name = "tbSiteName";
            this.tbSiteName.Size = new System.Drawing.Size(158, 20);
            this.tbSiteName.TabIndex = 0;
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSentinelSite, "Notes", true));
            this.textBox10.Location = new System.Drawing.Point(27, 221);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(332, 87);
            this.textBox10.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(392, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(82, 17);
            this.lblLastUpdated.Text = "Last Updated: ";
            // 
            // adminLevelPickerControl1
            // 
            this.adminLevelPickerControl1.AutoSize = true;
            this.adminLevelPickerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.adminLevelPickerControl1.Location = new System.Drawing.Point(22, 50);
            this.adminLevelPickerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.adminLevelPickerControl1.Name = "adminLevelPickerControl1";
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(187, 39);
            this.adminLevelPickerControl1.TabIndex = 1;
            this.adminLevelPickerControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(188, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // h3Label1
            // 
            this.h3Label1.AutoSize = true;
            this.h3Label1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label1.Location = new System.Drawing.Point(27, 98);
            this.h3Label1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label1.Name = "h3Label1";
            this.h3Label1.Size = new System.Drawing.Size(68, 16);
            this.h3Label1.TabIndex = 18;
            this.h3Label1.Tag = "SiteName";
            this.h3Label1.Text = "Site name";
            this.h3Label1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.label3.Location = new System.Drawing.Point(22, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 28);
            this.label3.TabIndex = 20;
            this.label3.Tag = "SentinelSite";
            this.label3.Text = "Sentinel Site";
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 0);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
            this.hr1.Size = new System.Drawing.Size(392, 5);
            this.hr1.TabIndex = 19;
            // 
            // h3Label2
            // 
            this.h3Label2.AutoSize = true;
            this.h3Label2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label2.Location = new System.Drawing.Point(27, 149);
            this.h3Label2.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label2.Name = "h3Label2";
            this.h3Label2.Size = new System.Drawing.Size(55, 16);
            this.h3Label2.TabIndex = 21;
            this.h3Label2.Tag = "Latitude";
            this.h3Label2.Text = "Latitude";
            this.h3Label2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label3
            // 
            this.h3Label3.AutoSize = true;
            this.h3Label3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label3.Location = new System.Drawing.Point(172, 149);
            this.h3Label3.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label3.Name = "h3Label3";
            this.h3Label3.Size = new System.Drawing.Size(67, 16);
            this.h3Label3.TabIndex = 22;
            this.h3Label3.Tag = "Longitude";
            this.h3Label3.Text = "Longitude";
            this.h3Label3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // h3Label4
            // 
            this.h3Label4.AutoSize = true;
            this.h3Label4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Label4.Location = new System.Drawing.Point(27, 202);
            this.h3Label4.Margin = new System.Windows.Forms.Padding(0);
            this.h3Label4.Name = "h3Label4";
            this.h3Label4.Size = new System.Drawing.Size(44, 16);
            this.h3Label4.TabIndex = 23;
            this.h3Label4.Tag = "Notes";
            this.h3Label4.Text = "Notes";
            this.h3Label4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // SentinelSiteAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(392, 400);
            this.Controls.Add(this.h3Label4);
            this.Controls.Add(this.h3Label3);
            this.Controls.Add(this.h3Label2);
            this.Controls.Add(this.h3Label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.tbLng);
            this.Controls.Add(this.tbLat);
            this.Controls.Add(this.tbSiteName);
            this.Controls.Add(this.btnAddNew);
            this.Name = "SentinelSiteAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SentinelSite";
            this.Text = "Sentinel Site";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddNew;
        private System.Windows.Forms.TextBox tbLng;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.TextBox tbSiteName;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.BindingSource bsSentinelSite;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastUpdated;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private Controls.H3Label h3Label1;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label3;
        private Controls.H3Label h3Label4;
    }
}
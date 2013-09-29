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
            this.tbLng = new System.Windows.Forms.TextBox();
            this.bsSentinelSite = new System.Windows.Forms.BindingSource(this.components);
            this.tbLat = new System.Windows.Forms.TextBox();
            this.tbSiteName = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.adminLevelPickerControl1 = new Nada.UI.View.AdminLevelPickerControl();
            this.label3 = new System.Windows.Forms.Label();
            this.hr1 = new Nada.UI.Controls.HR();
            this.h3Label2 = new Nada.UI.Controls.H3Label();
            this.h3Label3 = new Nada.UI.Controls.H3Label();
            this.h3Label4 = new Nada.UI.Controls.H3Label();
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
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
            this.statusStrip1.Size = new System.Drawing.Size(387, 22);
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
            this.adminLevelPickerControl1.Size = new System.Drawing.Size(200, 39);
            this.adminLevelPickerControl1.TabIndex = 1;
            this.adminLevelPickerControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(95)))), ((int)(((byte)(39)))));
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
            this.hr1.Size = new System.Drawing.Size(387, 5);
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
            // h3Required1
            // 
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(27, 98);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(81, 16);
            this.h3Required1.TabIndex = 24;
            this.h3Required1.Tag = "SiteName";
            this.h3Required1.Text = "Site name";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsSentinelSite;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.kryptonButton1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(173, 327);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(186, 31);
            this.tableLayoutPanel4.TabIndex = 42;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(3, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Tag = "Cancel";
            this.kryptonButton1.Values.Text = "Cancel";
            this.kryptonButton1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(106, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Tag = "Save";
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // SentinelSiteAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 400);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.h3Required1);
            this.Controls.Add(this.h3Label4);
            this.Controls.Add(this.h3Label3);
            this.Controls.Add(this.h3Label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.adminLevelPickerControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.tbLng);
            this.Controls.Add(this.tbLat);
            this.Controls.Add(this.tbSiteName);
            this.Name = "SentinelSiteAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SentinelSite";
            this.Text = "Sentinel Site";
            this.Load += new System.EventHandler(this.Modal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsSentinelSite)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLng;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.TextBox tbSiteName;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.BindingSource bsSentinelSite;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastUpdated;
        private AdminLevelPickerControl adminLevelPickerControl1;
        private System.Windows.Forms.Label label3;
        private Controls.HR hr1;
        private Controls.H3Label h3Label2;
        private Controls.H3Label h3Label3;
        private Controls.H3Label h3Label4;
        private Controls.H3Required h3Required1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
    }
}
namespace Nada.UI.Controls
{
    partial class SentinelSitePickerControl
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
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.h3Required8 = new Nada.UI.Controls.H3Required();
            this.tbLng = new System.Windows.Forms.TextBox();
            this.bsSurvey = new System.Windows.Forms.BindingSource(this.components);
            this.h3Required1 = new Nada.UI.Controls.H3Required();
            this.lblLat = new Nada.UI.Controls.H3Label();
            this.lblLng = new Nada.UI.Controls.H3Label();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.cbSiteType = new System.Windows.Forms.ComboBox();
            this.tblSiteName = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSentinel = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.cbSites = new System.Windows.Forms.ComboBox();
            this.sentinelSiteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSpotCheckName = new System.Windows.Forms.Panel();
            this.tbSiteName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tblContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).BeginInit();
            this.tblSiteName.SuspendLayout();
            this.pnlSentinel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).BeginInit();
            this.pnlSpotCheckName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.AutoSize = true;
            this.tblContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblContainer.ColumnCount = 8;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblContainer.Controls.Add(this.h3Required8, 2, 0);
            this.tblContainer.Controls.Add(this.tbLng, 6, 1);
            this.tblContainer.Controls.Add(this.h3Required1, 0, 0);
            this.tblContainer.Controls.Add(this.lblLat, 4, 0);
            this.tblContainer.Controls.Add(this.lblLng, 6, 0);
            this.tblContainer.Controls.Add(this.tbLat, 4, 1);
            this.tblContainer.Controls.Add(this.cbSiteType, 0, 1);
            this.tblContainer.Controls.Add(this.tblSiteName, 2, 1);
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.Size = new System.Drawing.Size(869, 63);
            this.tblContainer.TabIndex = 1;
            // 
            // h3Required8
            // 
            this.h3Required8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Required8.AutoSize = true;
            this.h3Required8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required8.Location = new System.Drawing.Point(211, 3);
            this.h3Required8.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required8.Name = "h3Required8";
            this.h3Required8.Size = new System.Drawing.Size(75, 15);
            this.h3Required8.TabIndex = 57;
            this.h3Required8.TabStop = false;
            this.h3Required8.Tag = "SiteName";
            this.h3Required8.Text = "Site name";
            this.h3Required8.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbLng
            // 
            this.tbLng.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Lng", true));
            this.tbLng.Location = new System.Drawing.Point(753, 21);
            this.tbLng.Name = "tbLng";
            this.tbLng.Size = new System.Drawing.Size(90, 20);
            this.tbLng.TabIndex = 3;
            // 
            // bsSurvey
            // 
            this.bsSurvey.DataSource = typeof(Nada.Model.Base.SurveyBase);
            // 
            // h3Required1
            // 
            this.h3Required1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h3Required1.AutoSize = true;
            this.h3Required1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.h3Required1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.h3Required1.Location = new System.Drawing.Point(0, 3);
            this.h3Required1.Margin = new System.Windows.Forms.Padding(0);
            this.h3Required1.Name = "h3Required1";
            this.h3Required1.Size = new System.Drawing.Size(80, 15);
            this.h3Required1.TabIndex = 0;
            this.h3Required1.TabStop = false;
            this.h3Required1.Tag = "SiteType";
            this.h3Required1.Text = "Type of site";
            this.h3Required1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lblLat
            // 
            this.lblLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLat.AutoSize = true;
            this.lblLat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblLat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLat.Location = new System.Drawing.Point(642, 0);
            this.lblLat.Margin = new System.Windows.Forms.Padding(0);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(51, 18);
            this.lblLat.TabIndex = 4;
            this.lblLat.TabStop = false;
            this.lblLat.Tag = "Latitude";
            this.lblLat.Text = "Latitude";
            this.lblLat.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // lblLng
            // 
            this.lblLng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLng.AutoSize = true;
            this.lblLng.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblLng.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLng.Location = new System.Drawing.Point(750, 0);
            this.lblLng.Margin = new System.Windows.Forms.Padding(0);
            this.lblLng.Name = "lblLng";
            this.lblLng.Size = new System.Drawing.Size(62, 18);
            this.lblLng.TabIndex = 5;
            this.lblLng.TabStop = false;
            this.lblLng.Tag = "Longitude";
            this.lblLng.Text = "Longitude";
            this.lblLng.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // tbLat
            // 
            this.tbLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "Lat", true));
            this.tbLat.Location = new System.Drawing.Point(645, 21);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(90, 20);
            this.tbLat.TabIndex = 2;
            // 
            // cbSiteType
            // 
            this.cbSiteType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "SiteType", true));
            this.cbSiteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSiteType.FormattingEnabled = true;
            this.cbSiteType.Items.AddRange(new object[] {
            "Sentinel",
            "SpotCheck"});
            this.cbSiteType.Location = new System.Drawing.Point(3, 21);
            this.cbSiteType.Name = "cbSiteType";
            this.cbSiteType.Size = new System.Drawing.Size(193, 21);
            this.cbSiteType.TabIndex = 0;
            this.cbSiteType.SelectedValueChanged += new System.EventHandler(this.cbSiteType_SelectedIndexChanged);
            // 
            // tblSiteName
            // 
            this.tblSiteName.AutoSize = true;
            this.tblSiteName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblSiteName.ColumnCount = 2;
            this.tblSiteName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblSiteName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblSiteName.Controls.Add(this.pnlSentinel, 1, 0);
            this.tblSiteName.Controls.Add(this.pnlSpotCheckName, 0, 0);
            this.tblSiteName.Location = new System.Drawing.Point(211, 18);
            this.tblSiteName.Margin = new System.Windows.Forms.Padding(0);
            this.tblSiteName.Name = "tblSiteName";
            this.tblSiteName.RowCount = 1;
            this.tblSiteName.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSiteName.Size = new System.Drawing.Size(419, 45);
            this.tblSiteName.TabIndex = 1;
            // 
            // pnlSentinel
            // 
            this.pnlSentinel.Controls.Add(this.fieldLink1);
            this.pnlSentinel.Controls.Add(this.cbSites);
            this.pnlSentinel.Location = new System.Drawing.Point(210, 0);
            this.pnlSentinel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSentinel.Name = "pnlSentinel";
            this.pnlSentinel.Size = new System.Drawing.Size(209, 45);
            this.pnlSentinel.TabIndex = 1;
            this.pnlSentinel.Visible = false;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(8, 27);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(119, 16);
            this.fieldLink1.TabIndex = 23;
            this.fieldLink1.TabStop = false;
            this.fieldLink1.Tag = "AddEditDeleteSite";
            this.fieldLink1.Text = "AddEditDeleteSite";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // cbSites
            // 
            this.cbSites.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsSurvey, "SentinelSiteId", true));
            this.cbSites.DataSource = this.sentinelSiteBindingSource;
            this.cbSites.DisplayMember = "SiteName";
            this.cbSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSites.FormattingEnabled = true;
            this.cbSites.Location = new System.Drawing.Point(8, 3);
            this.cbSites.Name = "cbSites";
            this.cbSites.Size = new System.Drawing.Size(179, 21);
            this.cbSites.TabIndex = 0;
            this.cbSites.ValueMember = "Id";
            // 
            // sentinelSiteBindingSource
            // 
            this.sentinelSiteBindingSource.DataSource = typeof(Nada.Model.Survey.SentinelSite);
            // 
            // pnlSpotCheckName
            // 
            this.pnlSpotCheckName.Controls.Add(this.tbSiteName);
            this.pnlSpotCheckName.Location = new System.Drawing.Point(0, 0);
            this.pnlSpotCheckName.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSpotCheckName.Name = "pnlSpotCheckName";
            this.pnlSpotCheckName.Size = new System.Drawing.Size(210, 30);
            this.pnlSpotCheckName.TabIndex = 2;
            // 
            // tbSiteName
            // 
            this.tbSiteName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSurvey, "SpotCheckName", true));
            this.tbSiteName.Location = new System.Drawing.Point(3, 3);
            this.tbSiteName.Name = "tbSiteName";
            this.tbSiteName.Size = new System.Drawing.Size(179, 20);
            this.tbSiteName.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bsSurvey;
            // 
            // SentinelSitePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tblContainer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SentinelSitePickerControl";
            this.Size = new System.Drawing.Size(869, 63);
            this.Load += new System.EventHandler(this.SentinelSitePickerControl_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurvey)).EndInit();
            this.tblSiteName.ResumeLayout(false);
            this.pnlSentinel.ResumeLayout(false);
            this.pnlSentinel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sentinelSiteBindingSource)).EndInit();
            this.pnlSpotCheckName.ResumeLayout(false);
            this.pnlSpotCheckName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private H3Required h3Required8;
        private System.Windows.Forms.TextBox tbLng;
        private H3Required h3Required1;
        private H3Label lblLat;
        private H3Label lblLng;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.ComboBox cbSiteType;
        private System.Windows.Forms.TableLayoutPanel tblSiteName;
        private System.Windows.Forms.Panel pnlSentinel;
        private FieldLink fieldLink1;
        private System.Windows.Forms.ComboBox cbSites;
        private System.Windows.Forms.Panel pnlSpotCheckName;
        private System.Windows.Forms.TextBox tbSiteName;
        private System.Windows.Forms.BindingSource sentinelSiteBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bsSurvey;
    }
}

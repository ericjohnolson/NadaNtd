namespace Nada.UI.Controls
{
    partial class ValidationControl
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
            this.tlpContainer = new System.Windows.Forms.TableLayoutPanel();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tlpHeaderContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblValidationHeader = new System.Windows.Forms.Label();
            this.lnkValidateLink = new Nada.UI.Controls.FieldLink();
            this.tlpValidationResults = new System.Windows.Forms.TableLayoutPanel();
            this.loadingIndicator = new Nada.UI.Controls.Loading();
            this.tlpContainer.SuspendLayout();
            this.tlpHeaderContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpContainer
            // 
            this.tlpContainer.AutoSize = true;
            this.tlpContainer.ColumnCount = 1;
            this.tlpContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpContainer.Controls.Add(this.hr1, 0, 0);
            this.tlpContainer.Controls.Add(this.tlpHeaderContainer, 0, 1);
            this.tlpContainer.Controls.Add(this.tlpValidationResults, 0, 2);
            this.tlpContainer.Controls.Add(this.loadingIndicator, 0, 3);
            this.tlpContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpContainer.Name = "tlpContainer";
            this.tlpContainer.RowCount = 4;
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContainer.Size = new System.Drawing.Size(929, 132);
            this.tlpContainer.TabIndex = 0;
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.Gray;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(5, 5);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.Gray;
            this.hr1.Size = new System.Drawing.Size(919, 1);
            this.hr1.TabIndex = 0;
            // 
            // tlpHeaderContainer
            // 
            this.tlpHeaderContainer.ColumnCount = 2;
            this.tlpHeaderContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpHeaderContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpHeaderContainer.Controls.Add(this.lblValidationHeader, 0, 0);
            this.tlpHeaderContainer.Controls.Add(this.lnkValidateLink, 1, 0);
            this.tlpHeaderContainer.Location = new System.Drawing.Point(3, 23);
            this.tlpHeaderContainer.Name = "tlpHeaderContainer";
            this.tlpHeaderContainer.RowCount = 1;
            this.tlpHeaderContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpHeaderContainer.Size = new System.Drawing.Size(460, 27);
            this.tlpHeaderContainer.TabIndex = 1;
            // 
            // lblValidationHeader
            // 
            this.lblValidationHeader.AutoSize = true;
            this.lblValidationHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblValidationHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidationHeader.Location = new System.Drawing.Point(0, 0);
            this.lblValidationHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblValidationHeader.Name = "lblValidationHeader";
            this.lblValidationHeader.Size = new System.Drawing.Size(137, 21);
            this.lblValidationHeader.TabIndex = 20;
            this.lblValidationHeader.Tag = "ValidationHeader";
            this.lblValidationHeader.Text = "ValidationHeader";
            // 
            // lnkValidateLink
            // 
            this.lnkValidateLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkValidateLink.AutoSize = true;
            this.lnkValidateLink.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnkValidateLink.BackColor = System.Drawing.Color.Transparent;
            this.lnkValidateLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkValidateLink.Location = new System.Drawing.Point(137, 5);
            this.lnkValidateLink.Margin = new System.Windows.Forms.Padding(0);
            this.lnkValidateLink.Name = "lnkValidateLink";
            this.lnkValidateLink.Size = new System.Drawing.Size(323, 16);
            this.lnkValidateLink.TabIndex = 21;
            this.lnkValidateLink.Tag = "CheckForValidationErrorsLnk";
            this.lnkValidateLink.Text = "CheckForValidationErrorsLnk";
            this.lnkValidateLink.OnClick += new System.Action(this.lnkValidateLink_OnClick);
            // 
            // tlpValidationResults
            // 
            this.tlpValidationResults.AutoSize = true;
            this.tlpValidationResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpValidationResults.ColumnCount = 1;
            this.tlpValidationResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValidationResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpValidationResults.Location = new System.Drawing.Point(3, 56);
            this.tlpValidationResults.Name = "tlpValidationResults";
            this.tlpValidationResults.RowCount = 1;
            this.tlpValidationResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpValidationResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlpValidationResults.Size = new System.Drawing.Size(0, 0);
            this.tlpValidationResults.TabIndex = 2;
            // 
            // loadingIndicator
            // 
            this.loadingIndicator.AutoSize = true;
            this.loadingIndicator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loadingIndicator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingIndicator.Location = new System.Drawing.Point(3, 62);
            this.loadingIndicator.Name = "loadingIndicator";
            this.loadingIndicator.Size = new System.Drawing.Size(53, 67);
            this.loadingIndicator.StatusMessage = "";
            this.loadingIndicator.TabIndex = 3;
            this.loadingIndicator.Visible = false;
            // 
            // ValidationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpContainer);
            this.Name = "ValidationControl";
            this.Size = new System.Drawing.Size(932, 135);
            this.tlpContainer.ResumeLayout(false);
            this.tlpContainer.PerformLayout();
            this.tlpHeaderContainer.ResumeLayout(false);
            this.tlpHeaderContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpContainer;
        private HR hr1;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderContainer;
        private System.Windows.Forms.Label lblValidationHeader;
        private FieldLink lnkValidateLink;
        private System.Windows.Forms.TableLayoutPanel tlpValidationResults;
        private Loading loadingIndicator;
    }
}

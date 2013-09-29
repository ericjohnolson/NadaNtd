namespace Nada.UI.Controls
{
    partial class CollapsiblePanel
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tblCollapsiblePanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.hr1 = new Nada.UI.Controls.HR();
            this.tblHeader.SuspendLayout();
            this.tblCollapsiblePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(96, 21);
            this.lblHeader.TabIndex = 17;
            this.lblHeader.Tag = "";
            this.lblHeader.Text = "HeaderText";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Nada.UI.Properties.Resources.ExpanderMinusIcon16x16;
            this.button1.Location = new System.Drawing.Point(777, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tblHeader
            // 
            this.tblHeader.AutoSize = true;
            this.tblHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblHeader.ColumnCount = 2;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblHeader.Controls.Add(this.lblHeader, 0, 0);
            this.tblHeader.Controls.Add(this.button1, 1, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblHeader.Location = new System.Drawing.Point(3, 3);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(804, 28);
            this.tblHeader.TabIndex = 19;
            // 
            // tblCollapsiblePanel
            // 
            this.tblCollapsiblePanel.AutoSize = true;
            this.tblCollapsiblePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblCollapsiblePanel.ColumnCount = 1;
            this.tblCollapsiblePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCollapsiblePanel.Controls.Add(this.tblHeader, 0, 0);
            this.tblCollapsiblePanel.Controls.Add(this.pnlContent, 0, 2);
            this.tblCollapsiblePanel.Controls.Add(this.hr1, 0, 1);
            this.tblCollapsiblePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblCollapsiblePanel.Location = new System.Drawing.Point(0, 0);
            this.tblCollapsiblePanel.Name = "tblCollapsiblePanel";
            this.tblCollapsiblePanel.RowCount = 3;
            this.tblCollapsiblePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblCollapsiblePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCollapsiblePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblCollapsiblePanel.Size = new System.Drawing.Size(810, 260);
            this.tblCollapsiblePanel.TabIndex = 20;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 57);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(804, 200);
            this.pnlContent.TabIndex = 20;
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.Gray;
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(5, 39);
            this.hr1.Margin = new System.Windows.Forms.Padding(5);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.Gray;
            this.hr1.Size = new System.Drawing.Size(800, 1);
            this.hr1.TabIndex = 21;
            // 
            // CollapsiblePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tblCollapsiblePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CollapsiblePanel";
            this.Size = new System.Drawing.Size(810, 263);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblCollapsiblePanel.ResumeLayout(false);
            this.tblCollapsiblePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TableLayoutPanel tblCollapsiblePanel;
        private System.Windows.Forms.Panel pnlContent;
        private HR hr1;
    }
}

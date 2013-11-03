namespace Nada.UI.View
{
    partial class IndicatorControl
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
            this.tblIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hr4 = new Nada.UI.Controls.HR();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tblStaticIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.indicatorErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicatorErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // tblIndicators
            // 
            this.tblIndicators.AutoSize = true;
            this.tblIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblIndicators.ColumnCount = 4;
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblIndicators.Location = new System.Drawing.Point(3, 62);
            this.tblIndicators.Name = "tblIndicators";
            this.tblIndicators.RowCount = 1;
            this.tblIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIndicators.Size = new System.Drawing.Size(40, 0);
            this.tblIndicators.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.hr4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tblIndicators, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 65);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.DimGray;
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 14);
            this.hr4.Margin = new System.Windows.Forms.Padding(0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.DimGray;
            this.hr4.Size = new System.Drawing.Size(800, 1);
            this.hr4.TabIndex = 48;
            this.hr4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.fieldLink1);
            this.panel1.Controls.Add(this.lblCustomIndicators);
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 36);
            this.panel1.TabIndex = 0;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(4, 20);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(154, 16);
            this.fieldLink1.TabIndex = 20;
            this.fieldLink1.Tag = "AddIndicatorLink";
            this.fieldLink1.Text = "Add/remove indicators >";
            this.fieldLink1.OnClick += new System.Action(this.fieldLink1_OnClick);
            // 
            // lblCustomIndicators
            // 
            this.lblCustomIndicators.AutoSize = true;
            this.lblCustomIndicators.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomIndicators.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomIndicators.Location = new System.Drawing.Point(0, -1);
            this.lblCustomIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.lblCustomIndicators.Name = "lblCustomIndicators";
            this.lblCustomIndicators.Size = new System.Drawing.Size(143, 21);
            this.lblCustomIndicators.TabIndex = 19;
            this.lblCustomIndicators.Tag = "CustomIndicators";
            this.lblCustomIndicators.Text = "Custom Indicators";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.tblStaticIndicators);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(46, 4);
            this.panel2.TabIndex = 50;
            // 
            // tblStaticIndicators
            // 
            this.tblStaticIndicators.AutoSize = true;
            this.tblStaticIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblStaticIndicators.ColumnCount = 4;
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblStaticIndicators.Location = new System.Drawing.Point(3, 1);
            this.tblStaticIndicators.Name = "tblStaticIndicators";
            this.tblStaticIndicators.RowCount = 1;
            this.tblStaticIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblStaticIndicators.Size = new System.Drawing.Size(40, 0);
            this.tblStaticIndicators.TabIndex = 49;
            // 
            // indicatorErrors
            // 
            this.indicatorErrors.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicatorErrors.ContainerControl = this;
            // 
            // IndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IndicatorControl";
            this.Size = new System.Drawing.Size(803, 68);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicatorErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblIndicators;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Controls.FieldLink fieldLink1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.ErrorProvider indicatorErrors;
        private Controls.HR hr4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tblStaticIndicators;

    }
}

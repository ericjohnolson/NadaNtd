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
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblTopControls = new System.Windows.Forms.TableLayoutPanel();
            this.hr4 = new Nada.UI.Controls.HR();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fieldLink1 = new Nada.UI.Controls.FieldLink();
            this.lblCustomIndicators = new System.Windows.Forms.Label();
            this.tblIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tblStaticIndicators = new System.Windows.Forms.TableLayoutPanel();
            this.tblMetaData = new System.Windows.Forms.TableLayoutPanel();
            this.indicatorErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.tblContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicatorErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.AutoSize = true;
            this.tblContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblContainer.Controls.Add(this.tblTopControls, 0, 1);
            this.tblContainer.Controls.Add(this.hr4, 0, 4);
            this.tblContainer.Controls.Add(this.panel1, 0, 5);
            this.tblContainer.Controls.Add(this.tblIndicators, 0, 6);
            this.tblContainer.Controls.Add(this.panel2, 0, 2);
            this.tblContainer.Controls.Add(this.tblMetaData, 0, 0);
            this.tblContainer.Location = new System.Drawing.Point(3, 3);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 7;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.Size = new System.Drawing.Size(920, 95);
            this.tblContainer.TabIndex = 2;
            // 
            // tblTopControls
            // 
            this.tblTopControls.AutoSize = true;
            this.tblTopControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTopControls.ColumnCount = 1;
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblTopControls.Location = new System.Drawing.Point(3, 21);
            this.tblTopControls.Name = "tblTopControls";
            this.tblTopControls.RowCount = 1;
            this.tblTopControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblTopControls.Size = new System.Drawing.Size(0, 0);
            this.tblTopControls.TabIndex = 52;
            // 
            // hr4
            // 
            this.hr4.BackColor = System.Drawing.Color.DimGray;
            this.hr4.ForeColor = System.Drawing.Color.Gray;
            this.hr4.Location = new System.Drawing.Point(0, 40);
            this.hr4.Margin = new System.Windows.Forms.Padding(0);
            this.hr4.Name = "hr4";
            this.hr4.RuleColor = System.Drawing.Color.DimGray;
            this.hr4.Size = new System.Drawing.Size(920, 1);
            this.hr4.TabIndex = 48;
            this.hr4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.fieldLink1);
            this.panel1.Controls.Add(this.lblCustomIndicators);
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 39);
            this.panel1.TabIndex = 0;
            // 
            // fieldLink1
            // 
            this.fieldLink1.AutoSize = true;
            this.fieldLink1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldLink1.BackColor = System.Drawing.Color.Transparent;
            this.fieldLink1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLink1.Location = new System.Drawing.Point(5, 23);
            this.fieldLink1.Margin = new System.Windows.Forms.Padding(0);
            this.fieldLink1.Name = "fieldLink1";
            this.fieldLink1.Size = new System.Drawing.Size(154, 16);
            this.fieldLink1.TabIndex = 20;
            this.fieldLink1.Tag = "AddIndicatorLink";
            this.fieldLink1.Text = "Add/remove indicators >";
            this.fieldLink1.OnClick += new System.Action(this.AddRemoveIndicator_OnClick);
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
            // tblIndicators
            // 
            this.tblIndicators.AutoSize = true;
            this.tblIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblIndicators.ColumnCount = 4;
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblIndicators.Location = new System.Drawing.Point(3, 92);
            this.tblIndicators.Name = "tblIndicators";
            this.tblIndicators.RowCount = 1;
            this.tblIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIndicators.Size = new System.Drawing.Size(46, 0);
            this.tblIndicators.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.tblStaticIndicators);
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(52, 4);
            this.panel2.TabIndex = 50;
            // 
            // tblStaticIndicators
            // 
            this.tblStaticIndicators.AutoSize = true;
            this.tblStaticIndicators.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblStaticIndicators.ColumnCount = 4;
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblStaticIndicators.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblStaticIndicators.Location = new System.Drawing.Point(3, 1);
            this.tblStaticIndicators.Name = "tblStaticIndicators";
            this.tblStaticIndicators.RowCount = 1;
            this.tblStaticIndicators.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblStaticIndicators.Size = new System.Drawing.Size(46, 0);
            this.tblStaticIndicators.TabIndex = 49;
            // 
            // tblMetaData
            // 
            this.tblMetaData.AutoSize = true;
            this.tblMetaData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblMetaData.ColumnCount = 7;
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblMetaData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMetaData.Location = new System.Drawing.Point(3, 3);
            this.tblMetaData.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.tblMetaData.Name = "tblMetaData";
            this.tblMetaData.RowCount = 1;
            this.tblMetaData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMetaData.Size = new System.Drawing.Size(69, 0);
            this.tblMetaData.TabIndex = 51;
            this.tblMetaData.Visible = false;
            // 
            // indicatorErrors
            // 
            this.indicatorErrors.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicatorErrors.ContainerControl = this;
            // 
            // IndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblContainer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IndicatorControl";
            this.Size = new System.Drawing.Size(923, 98);
            this.Load += new System.EventHandler(this.IndicatorControl_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.Panel panel1;
        private Controls.FieldLink fieldLink1;
        private System.Windows.Forms.Label lblCustomIndicators;
        private System.Windows.Forms.ErrorProvider indicatorErrors;
        private Controls.HR hr4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tblStaticIndicators;
        private System.Windows.Forms.TableLayoutPanel tblMetaData;
        private System.Windows.Forms.TableLayoutPanel tblTopControls;

    }
}

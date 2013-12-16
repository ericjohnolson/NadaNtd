namespace Nada.UI.View.Reports.CustomReport
{
    partial class StepLocations
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNoLocations = new Nada.UI.Controls.H3bLabel();
            this.levelPicker = new Nada.UI.View.AdminLevelMultiselect();
            this.tblListAllLocations = new System.Windows.Forms.TableLayoutPanel();
            this.cbAllLocations = new System.Windows.Forms.CheckBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pickerAllLocations = new Nada.UI.View.AdminLevelMultiselectAny();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblListAllLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lblNoLocations, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.levelPicker, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tblListAllLocations, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(782, 907);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lblNoLocations
            // 
            this.lblNoLocations.AutoSize = true;
            this.lblNoLocations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblNoLocations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNoLocations.Location = new System.Drawing.Point(0, 0);
            this.lblNoLocations.Margin = new System.Windows.Forms.Padding(0);
            this.lblNoLocations.Name = "lblNoLocations";
            this.lblNoLocations.Size = new System.Drawing.Size(132, 16);
            this.lblNoLocations.TabIndex = 3;
            this.lblNoLocations.Tag = "NoLocationsByCountry";
            this.lblNoLocations.Text = "NoLocationsByCountry";
            this.lblNoLocations.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNoLocations.Visible = false;
            // 
            // levelPicker
            // 
            this.levelPicker.AutoSize = true;
            this.levelPicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.levelPicker.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelPicker.Location = new System.Drawing.Point(0, 474);
            this.levelPicker.Margin = new System.Windows.Forms.Padding(0);
            this.levelPicker.Name = "levelPicker";
            this.levelPicker.Size = new System.Drawing.Size(782, 433);
            this.levelPicker.TabIndex = 5;
            this.levelPicker.Visible = false;
            // 
            // tblListAllLocations
            // 
            this.tblListAllLocations.AutoSize = true;
            this.tblListAllLocations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblListAllLocations.ColumnCount = 2;
            this.tblListAllLocations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblListAllLocations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblListAllLocations.Controls.Add(this.cbAllLocations, 0, 0);
            this.tblListAllLocations.Controls.Add(this.pickerAllLocations, 0, 1);
            this.tblListAllLocations.Location = new System.Drawing.Point(0, 16);
            this.tblListAllLocations.Margin = new System.Windows.Forms.Padding(0);
            this.tblListAllLocations.Name = "tblListAllLocations";
            this.tblListAllLocations.RowCount = 2;
            this.tblListAllLocations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblListAllLocations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblListAllLocations.Size = new System.Drawing.Size(782, 458);
            this.tblListAllLocations.TabIndex = 6;
            this.tblListAllLocations.Visible = false;
            // 
            // cbAllLocations
            // 
            this.cbAllLocations.AutoSize = true;
            this.cbAllLocations.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource1, "IsAllLocations", true));
            this.cbAllLocations.Location = new System.Drawing.Point(3, 3);
            this.cbAllLocations.Name = "cbAllLocations";
            this.cbAllLocations.Size = new System.Drawing.Size(93, 19);
            this.cbAllLocations.TabIndex = 0;
            this.cbAllLocations.Tag = "AllLocations";
            this.cbAllLocations.Text = "AllLocations";
            this.cbAllLocations.UseVisualStyleBackColor = true;
            this.cbAllLocations.CheckedChanged += new System.EventHandler(this.cbAllLocations_CheckedChanged);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Nada.Model.Reports.ReportOptions);
            // 
            // pickerAllLocations
            // 
            this.pickerAllLocations.AutoSize = true;
            this.pickerAllLocations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pickerAllLocations.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickerAllLocations.Location = new System.Drawing.Point(0, 25);
            this.pickerAllLocations.Margin = new System.Windows.Forms.Padding(0);
            this.pickerAllLocations.Name = "pickerAllLocations";
            this.pickerAllLocations.Size = new System.Drawing.Size(782, 433);
            this.pickerAllLocations.TabIndex = 1;
            // 
            // StepLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "StepLocations";
            this.Size = new System.Drawing.Size(785, 910);
            this.Load += new System.EventHandler(this.StepLocations_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblListAllLocations.ResumeLayout(false);
            this.tblListAllLocations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private AdminLevelMultiselect levelPicker;
        private System.Windows.Forms.TableLayoutPanel tblListAllLocations;
        private System.Windows.Forms.CheckBox cbAllLocations;
        private AdminLevelMultiselectAny pickerAllLocations;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Controls.H3bLabel lblNoLocations;

    }
}

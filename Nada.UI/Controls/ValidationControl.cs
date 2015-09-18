﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.Base;
using Nada.Model;
using Nada.Globalization;

namespace Nada.UI.Controls
{
    public partial class ValidationControl : BaseControl
    {
        public ICustomValidator Validator { get; set; }
        public Action OnFocus { get; set; }
        public event Action OnValidate = () => { };

        public ValidationControl()
            : base()
        {
            InitializeComponent();
        }

        public void DoValidate(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData)
        {
            // Show the loading indicator
            loadingIndicator.Visible = true;
            // Run the validation on a background thread
            BackgroundWorker validationWorker = new BackgroundWorker();
            validationWorker.RunWorkerCompleted += validationWorker_RunWorkerCompleted;
            validationWorker.DoWork += validationWorker_DoWork;
            validationWorker.RunWorkerAsync(new ValidationPayload
            {
                Indicators = indicators,
                Values = values,
                MetaData = metaData
            });
        }

        private void validationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ValidationPayload payload = (ValidationPayload)e.Argument;

                e.Result = Validator.ValidateIndicators(payload.Indicators, payload.Values, payload.MetaData);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error validationWorker_DoWork. ", ex);
                throw;
            }
        }

        private void validationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.IsDisposed)
                return;

            // Get the validation results
            List<ValidationResult> validationResults = (List<ValidationResult>)e.Result;

            // Hide and clear the validation results container while it is being populated
            tlpValidationResults.Visible = false;
            this.SuspendLayout();
            tlpValidationResults.Controls.Clear();

            // Display each result
            foreach (ValidationResult result in validationResults)
            {
                // Add a new row
                int labelRowIndex = tlpValidationResults.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                int controlRowIndex = tlpValidationResults.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });

                // Add label
                tlpValidationResults.Controls.Add(
                    new H3bLabel {
                        Text = TranslationLookup.GetValue(result.ValidationRule.Indicator.DisplayName,
                        result.ValidationRule.Indicator.DisplayName),
                        Name = "ciLabel_" + result.ValidationRule.Indicator.DisplayName,
                        AutoSize = true
                    },
                    0, labelRowIndex);

                // Add val
                var label = new H3bLabel { 
                    Text = result.Message,
                    Name = "ciLabel_Val" + result.ValidationRule.Indicator.DisplayName,
                    AutoSize = true,
                    TextColor = result.IsSuccess ? Color.FromArgb(0, 160, 0) : Color.FromArgb(160, 0, 0)
                };
                label.MakeBold();
                tlpValidationResults.Controls.Add(label, 0, controlRowIndex);
            }

            this.ResumeLayout();
            tlpValidationResults.Visible = true;
            loadingIndicator.Visible = false;
        }

        private void lnkValidateLink_OnClick()
        {
            if (OnValidate != null)
                OnValidate();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return lblValidationHeader.ForeColor; }
            set
            {
                lblValidationHeader.ForeColor = value;
                hr1.BackColor = value;
            }
        }
    }

    public class ValidationPayload
    {
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> Values { get; set; }
        public List<KeyValuePair<string, string>> MetaData { get; set; }
    }
}

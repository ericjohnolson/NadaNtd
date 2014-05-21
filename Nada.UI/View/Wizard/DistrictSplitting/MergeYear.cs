using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using System.IO;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class MergeYear : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.Year; } }

        public MergeYear(RedistrictingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
            if (rbtnCalendar.Checked)
                options.YearStartMonth = 1;
            else
                options.YearStartMonth = (int)comboBox1.SelectedValue;

            OnSwitchStep(new MergingSources(options, Translations.SplitMergeSource));
        }
        public void DoFinish()
        {
        }

        private void BackupForRedistricting_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                lblMessage.SetMaxWidth(500);
                Localizer.TranslateControl(this);
                var months = GlobalizationUtil.GetAllMonths();
                foreach (var month in months)
                {
                    DateTime start = new DateTime(2000, month.Id, 1);
                    DateTime end = start.AddYears(1).AddDays(-1);
                    month.Name = start.ToString("MMM") + " - " + end.ToString("MMM");
                }
                bindingSource1.DataSource = months;
                comboBox1.DropDownWidth = BaseForm.GetDropdownWidth(months.Select(m => m.Name));
                comboBox1.SelectedValue = 1;
            }
        }

        private void rbtnCustom_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = rbtnCustom.Checked;
        }

    }
}

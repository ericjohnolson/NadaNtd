using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Globalization;
using Nada.UI.Base;
using Nada.Model.Repositories;

namespace Nada.UI.View.Demography
{
    public partial class CountryDemographyView : BaseControl
    {
        CountryDemography model = null;

        public CountryDemographyView()
            : base()
        {
            InitializeComponent();
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3Required3.SetMaxWidth(370);
                lblYear.SetMaxWidth(370);

                DemoRepository demo = new DemoRepository();
                model = demo.GetCountryLevelStatsRecent();
                CreateIncomeStatusDropdown(comboBox1);
                bsCountryDemo.DataSource = model;
            }
        }

        public CountryDemography GetDemo()
        {
            bsCountryDemo.EndEdit();
            return model;
        }

        public void DoValidate()
        {
            countryDemoErrors.DataSource = bsCountryDemo;
        }

        public void SetNewYear(double? growthRate, DateTime dateReported)
        {
            model.GrowthRate = growthRate;
            model.DateDemographyData = dateReported;
        }

        public bool IsValid()
        {
            foreach(Control cntrl in tableLayoutPanel14.Controls)
            {
                if (!string.IsNullOrEmpty(countryDemoErrors.GetError(cntrl)))
                    return false;
            }
            return true;
        }

        private void CreateIncomeStatusDropdown(ComboBox comboBox)
        {
            List<IndicatorDropdownValue> vals = new List<IndicatorDropdownValue>();
            vals.Add(new IndicatorDropdownValue { DisplayName = "", TranslationKey = "" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Low, TranslationKey = "Low" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Middle, TranslationKey = "Middle" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.High, TranslationKey = "High" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Unknown, TranslationKey = "Unknown" });
            comboBox.DataSource = vals;
            comboBox.DropDownWidth = BaseForm.GetDropdownWidth(vals.Select(a => a.DisplayName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Controls;
using Nada.UI.View;

namespace Nada.UI.ViewModel
{
    public class SurveyBaseVm : ViewModelBase, IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private SurveyBase model = null;
        private SurveyRepository r = null;
        private ICalcIndicators calc = null;
        private SentinelSitePickerControl sitePicker = null;

        public SurveyBaseVm(AdminLevel a, int typeId, ICalcIndicators c)
        {
            adminLevel = a;
            r = new SurveyRepository();
            model = r.CreateSurvey(typeId);
            calc = c;
        }

        public SurveyBaseVm(AdminLevel a, SurveyDetails s, ICalcIndicators c)
        {
            r = new SurveyRepository();
            this.model = r.GetById(s.Id);
            adminLevel = a;
            calc = c;
        }

        public bool CanEditTypeName { get { return false; } }
        public string StatusMessage { get { return model.UpdatedBy; } }
        public string Notes { get { return model.Notes; } }
        public string CalculatorTypeId { get { return model.TypeOfSurvey.Id.ToString(); } }
        public ICalcIndicators Calculator { get { return calc; } }
        public ICustomValidator Validator { get { return new SurveyCustomValidator(); } }
        public List<KeyValuePair<string, string>> MetaData { get; set; }
        public Dictionary<string, Indicator> Indicators { get { return model.TypeOfSurvey.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get { return model.TypeOfSurvey.IndicatorDropdownValues; } }
        public IndicatorEntityType EntityType { get { return IndicatorEntityType.Survey; } }
        public string Title { get { return model.TypeOfSurvey.SurveyTypeName; } }
        public string TypeTitle { get { return model.TypeOfSurvey.DiseaseType; } }
        public Color FormColor { get { return Color.FromArgb(52, 100, 160); } }

        public void AddSpecialControls(IndicatorControl cntrl)
        {
            if (model.TypeOfSurvey.Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) != null)
            {
                model.HasSentinelSite = true;
                sitePicker = cntrl.LoadSentinelSitePicker(FormColor);
                sitePicker.LoadModel(model);
            }
        }

        public bool Initialize()
        {
            if (model.TypeOfSurvey.HasMultipleLocations)
            {
                AdminLevelSelectForm selector = new AdminLevelSelectForm(new List<AdminLevel> { adminLevel }, this);
                if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    model.AdminLevels = selector.GetSelected();
                else
                    return false;
            }
            else
            {
                model.AdminLevels = new List<AdminLevel> { adminLevel };
            }
            return true;
        }

        public bool IsValid()
        {
            return model.IsValid();
        }

        public void DoSave(List<IndicatorValue> indicatorValues, string notes)
        {
            DoSave(indicatorValues, notes, true);
        }

        public void DoSave(List<IndicatorValue> indicatorValues, string notes, bool persist)
        {
            model.Notes = notes;
            model.IndicatorValues = ReconcileIndicators(model.IndicatorValues, indicatorValues);

            if (persist)
            {
                bool isNew = model.Id < 1;
                r.SaveSurvey(model, ApplicationData.Instance.GetUserId());
                if (isNew && r.CopySentinelSiteSurvey(model, ApplicationData.Instance.GetUserId()))
                    MessageBox.Show(Translations.SurveyCopiedMessage, Translations.SurveyCopiedTitle);
                MessageBox.Show(Translations.UpdateDdAfterSavingSurvey, Translations.UpdateDdAfterSavingSurveyTitle);
            }
        }


        private void selector_OnSave(List<AdminLevel> obj)
        {
            model.AdminLevels = obj;
        }

        public void DoSaveType(string name)
        {
            if (!string.IsNullOrEmpty(name))
                model.TypeOfSurvey.SurveyTypeName = name;
            int currentUser = ApplicationData.Instance.GetUserId();
            r.Save(model.TypeOfSurvey, currentUser);
        }

        public string LocationName
        {
            get
            {
                if (model.AdminLevels == null || model.AdminLevels.Count == 0)
                    return Translations.NA;
                if (model.AdminLevels.Count == 1)
                    return model.AdminLevels.First().Name;
                else
                    return String.Join(",", model.AdminLevels.Select(a => a.Name).ToArray());
            }
        }

        public AdminLevel Location
        {
            get
            {
                if (model.AdminLevels.Count == 1)
                    return model.AdminLevels.First();
                else
                    return new AdminLevel { Name = Translations.ManyLocations };
            }
        }
    }
}

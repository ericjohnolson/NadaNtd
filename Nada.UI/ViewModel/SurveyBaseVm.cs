using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;

namespace Nada.UI.ViewModel
{
    public class SurveyBaseVm : IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private SurveyBase model = null;
        private SurveyRepository r = null;
        private ICalcIndicators calc = null;

        public SurveyBaseVm(AdminLevel a, int typeId, ICalcIndicators c)
        {
            adminLevel = a;
            r = new SurveyRepository();
            model = r.CreateSurvey(typeId);
            model.AdminLevelId = adminLevel.Id;
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
        public ICalcIndicators Calculator { get { return calc; } }
        public AdminLevel Location { get { return adminLevel; } }
        public Dictionary<string, Indicator> Indicators { get { return model.TypeOfSurvey.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get { return model.TypeOfSurvey.IndicatorDropdownValues; } }
        public IndicatorEntityType EntityType { get { return IndicatorEntityType.Survey; } }
        public string Title { get { return model.TypeOfSurvey.SurveyTypeName; } }
        public string TypeTitle { get { return model.TypeOfSurvey.DiseaseType; } }
        public Color FormColor { get { return Color.FromArgb(197, 95, 39); } }

        public bool IsValid()
        {
            return model.IsValid();
        }

        public void DoSave(List<IndicatorValue> indicatorValues, string notes)
        {
            model.Notes = notes;
            model.IndicatorValues = indicatorValues;
            r.SaveSurvey(model, ApplicationData.Instance.GetUserId());
        }

        public void DoSaveType(string name)
        {
            model.TypeOfSurvey.SurveyTypeName = name;
            int currentUser = ApplicationData.Instance.GetUserId();
            r.Save(model.TypeOfSurvey, currentUser);
        }
    }
}

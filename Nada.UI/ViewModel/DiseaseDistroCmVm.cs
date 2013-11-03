using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.ViewModel
{
    public class DiseaseDistroCmVm : IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private DiseaseDistroCm model = null;
        private DiseaseRepository r = null;
        private ICalcIndicators calc = null;

        public DiseaseDistroCmVm(AdminLevel a, int did, ICalcIndicators c)
        {
            adminLevel = a;
            r = new DiseaseRepository();
            model = r.CreateCm((DiseaseType)did);
            model.AdminLevelId = adminLevel.Id;
            calc = c;
        }

        public DiseaseDistroCmVm(AdminLevel a, DiseaseDistroCm s, ICalcIndicators c)
        {
            r = new DiseaseRepository();
            this.model = s;
            adminLevel = a;
            calc = c;
        }

        public bool CanEditTypeName { get { return false; } }
        public string StatusMessage { get { return model.UpdatedBy; } }
        public ICalcIndicators Calculator { get { return calc; } }
        public AdminLevel Location { get { return adminLevel; } }
        public Dictionary<string, Indicator> Indicators { get { return model.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<KeyValuePair<int, string>> IndicatorDropdownValues { get { return model.IndicatorDropdownValues; } }
        public string Title { get { return model.Disease.DisplayName + " " + Translations.DiseaseDistribution; } }
        public string TypeTitle { get { return model.Disease.DiseaseType; } }
        public Color FormColor { get { return Color.FromArgb(86, 43, 115); } }

        public bool IsValid()
        {
            return model.IsValid();
        }

        public void DoSave(List<IndicatorValue> indicatorValues)
        {
            model.IndicatorValues = indicatorValues;
            r.Save(model, ApplicationData.Instance.GetUserId());
        }

        public void DoSaveType(string name)
        {
            int currentUser = ApplicationData.Instance.GetUserId();
            r.Save(model, currentUser);
        }
    }
}

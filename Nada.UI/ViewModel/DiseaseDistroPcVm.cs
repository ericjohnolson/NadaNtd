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
using Nada.UI.View;

namespace Nada.UI.ViewModel
{
    public class DiseaseDistroPcVm : IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private DiseaseDistroPc model = null;
        private DiseaseRepository r = null;
        private ICalcIndicators calc = null;

        public DiseaseDistroPcVm(AdminLevel a, int did, ICalcIndicators c)
        {
            adminLevel = a;
            r = new DiseaseRepository();
            model = r.Create((DiseaseType)did);
            model.AdminLevelId = adminLevel.Id;
            calc = c;
        }

        public DiseaseDistroPcVm(AdminLevel a, DiseaseDistroPc s, ICalcIndicators c)
        {
            r = new DiseaseRepository();
            this.model = s;
            adminLevel = a;
            calc = c;
        }

        public string LocationName { get { return adminLevel.Name; } }
        public bool CanEditTypeName { get { return false; } }
        public string StatusMessage { get { return model.UpdatedBy; } }
        public string Notes { get { return model.Notes; } }
        public string CalculatorTypeId { get { return model.Disease.Id.ToString(); } }
        public ICalcIndicators Calculator { get { return calc; } }
        public AdminLevel Location { get { return adminLevel; } }
        public IndicatorEntityType EntityType { get { return IndicatorEntityType.DiseaseDistribution; } }
        public List<KeyValuePair<string, string>> MetaData { get { return null; } }
        public Dictionary<string, Indicator> Indicators { get { return model.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get { return model.IndicatorDropdownValues; } }
        public string Title { get { return model.Disease.DisplayName + " " + Translations.DiseaseDistribution; } }
        public string TypeTitle { get { return model.Disease.DiseaseType; } }
        public Color FormColor { get { return Color.FromArgb(52, 100, 160); } }
        public void AddSpecialControls(IndicatorControl cntrl) { }

        public bool IsValid()
        {
            return model.IsValid();
        }

        public void DoSave(List<IndicatorValue> indicatorValues, string notes)
        {
            model.Notes = notes;
            model.IndicatorValues = indicatorValues;
            r.Save(model, ApplicationData.Instance.GetUserId());
        }

        public void DoSaveType(string name)
        {
            int currentUser = ApplicationData.Instance.GetUserId();
            r.SaveIndicators(model, model.Disease.Id, currentUser);
        }
    }
}

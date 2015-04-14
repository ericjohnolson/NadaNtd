using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.View;

namespace Nada.UI.ViewModel
{
    public class IntvBaseVm : ViewModelBase, IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private IntvBase model = null;
        private IntvRepository r = null;
        private ICalcIndicators calc = null;

        public IntvBaseVm(AdminLevel a, int typeId, ICalcIndicators c)
        {
            adminLevel = a;
            r = new IntvRepository();
            model = r.CreateIntv(typeId);
            model.AdminLevelId = adminLevel.Id;
            calc = c;
        }

        public IntvBaseVm(AdminLevel a, IntvDetails d, ICalcIndicators c)
        {
            r = new IntvRepository();
            this.model = r.GetById(d.Id);
            adminLevel = a;
            calc = c;
        }

        public string LocationName { get { return adminLevel.Name; } }
        public bool CanEditTypeName { get { return false; } }
        public string StatusMessage { get { return model.UpdatedBy; } }
        public string Notes { get { return model.Notes; } }
        public string CalculatorTypeId { get { return model.IntvType.Id.ToString(); } }
        public ICalcIndicators Calculator { get { return calc; } }
        public ICustomValidator Validator { get { return new IntvCustomValidator(); } }
        public AdminLevel Location { get { return adminLevel; } }
        public IndicatorEntityType EntityType { get { return IndicatorEntityType.Intervention; } }
        public List<KeyValuePair<string, string>> MetaData { get; set; }
        public Dictionary<string, Indicator> Indicators { get { return model.IntvType.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get { return model.IntvType.IndicatorDropdownValues; } }
        public string Title { get { return model.IntvType.IntvTypeName; } }
        public string TypeTitle { get { return model.IntvType.DiseaseType; } }
        public Color FormColor { get { return Color.FromArgb(52, 100, 160); } }
        public void AddSpecialControls(IndicatorControl cntrl) { }

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
                r.SaveBase(model, ApplicationData.Instance.GetUserId());
        }

        public void DoSaveType(string name)
        {
            if(!string.IsNullOrEmpty(name))
                model.IntvType.IntvTypeName = name;
            int currentUser = ApplicationData.Instance.GetUserId();
            r.Save(model.IntvType, currentUser);
        }
    }
}

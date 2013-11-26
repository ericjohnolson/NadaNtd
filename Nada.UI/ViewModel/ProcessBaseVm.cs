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
using Nada.Model.Process;
using Nada.UI.AppLogic;

namespace Nada.UI.ViewModel
{
    public class ProcessBaseVm : IDataEntryVm
    {
        private AdminLevel adminLevel = null;
        private ProcessBase model = null;
        private ProcessRepository r = null;
        private ICalcIndicators calc = null;

        public ProcessBaseVm(AdminLevel a, int typeId, ICalcIndicators c)
        {
            adminLevel = a;
            r = new ProcessRepository();
            model = r.Create(typeId);
            model.AdminLevelId = adminLevel.Id;
            calc = c;
        }

        public ProcessBaseVm(AdminLevel a, ProcessDetails s, ICalcIndicators c)
        {
            r = new ProcessRepository();
            this.model = r.GetById(s.Id);
            adminLevel = a;
            calc = c;
        }

        public bool CanEditTypeName { get { return false; } }
        public string StatusMessage { get { return model.UpdatedBy; } }
        public string Notes { get { return model.Notes; } }
        public ICalcIndicators Calculator { get { return calc; } }
        public AdminLevel Location { get { return adminLevel; } }
        public IndicatorEntityType EntityType { get { return IndicatorEntityType.Process; } }
        public Dictionary<string, Indicator> Indicators { get { return model.ProcessType.Indicators; } }
        public List<IndicatorValue> IndicatorValues { get { return model.IndicatorValues; } }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get { return model.ProcessType.IndicatorDropdownValues; } }
        public string Title { get { return model.ProcessType.TypeName; } }
        public string TypeTitle { get { return "";  } } //model.ProcessType.CategoryName;
        public Color FormColor { get { return Color.FromArgb(66, 44, 27); } }

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
            model.ProcessType.TypeName = name;
            int currentUser = ApplicationData.Instance.GetUserId();
            r.Save(model.ProcessType, currentUser);
        }
    }
}

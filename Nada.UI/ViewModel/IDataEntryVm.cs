using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nada.Model;
using Nada.UI.View;

namespace Nada.UI.ViewModel
{
    public interface IDataEntryVm
    {
        bool CanEditTypeName { get; }
        string StatusMessage { get; }
        string Title { get; }
        string TypeTitle { get; }
        string Notes { get; }
        string LocationName { get; }
        string CalculatorTypeId { get; }
        ICalcIndicators Calculator { get; }
        Color FormColor { get; }
        AdminLevel Location { get; }
        List<KeyValuePair<string, string>> MetaData { get; }
        Dictionary<string, Indicator> Indicators { get; }
        List<IndicatorValue> IndicatorValues { get; }
        List<IndicatorDropdownValue> IndicatorDropdownValues { get; }
        IndicatorEntityType EntityType { get; }

        bool IsValid();
        void AddSpecialControls(IndicatorControl cntrl);
        void DoSave(List<IndicatorValue> indicatorValues, string notes);

        void DoSaveType(string p);
    }

    public class ViewModelBase
    {
        protected List<IndicatorValue> ReconcileIndicators(List<IndicatorValue> existingValues, List<IndicatorValue> newValues)
        {
            List<IndicatorValue> indsToAdd = new List<IndicatorValue>();
            Dictionary<int, IndicatorValue> newDict = new Dictionary<int, IndicatorValue>();
            foreach (var val in newValues)
                newDict.Add(val.IndicatorId, val);

            foreach (var val in existingValues)
            {
                if (!newDict.ContainsKey(val.IndicatorId))
                    indsToAdd.Add(val);

                if (newDict.ContainsKey(val.IndicatorId) && val.DynamicValue == newDict[val.IndicatorId].DynamicValue && val.CalcByRedistrict)
                    newDict[val.IndicatorId].CalcByRedistrict = true;
            }

            newValues.AddRange(indsToAdd);
            return newValues;
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Diseases
{
    [Serializable]
    public class DiseaseDistroPc : NadaClass, IHaveDynamicIndicators, IHaveDynamicIndicatorValues, IDataErrorInfo
    {
        public DiseaseDistroPc()
        {
            IndicatorValues = new List<IndicatorValue>();
            IndicatorDropdownValues = new List<IndicatorDropdownValue>();
        }

        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public Nullable<int> YearOfReporting { get; set; }

        // indicators
        public string DiseaseEndemicity { get; set; }
        public string DiseaseTrichiasis { get; set; }
        public string TasObjective { get; set; }
        public string RoundsCurrentlyImplemented { get; set; }
        public string RoundsRecommendedByWho { get; set; }
        public Nullable<int> YearStoppingPcLower { get; set; }
        public Nullable<int> TrichiasisSurgeryBacklog { get; set; }
        public Nullable<int> TrichiasisSurgeryGoal { get; set; }
        public Nullable<int> TrachomaGoal { get; set; }
        public Nullable<int> Population { get; set; }
        public Nullable<int> YearStoppingPc { get; set; }
        public Nullable<int> YearPcStarted { get; set; }
        public Nullable<DateTime> TrachomaImpactYearMonth { get; set; }
        public Nullable<DateTime> TasYearMonth { get; set; }

        public void MapPropertiesToIndicators()
        {
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["YearStoppingPcLower"].Id, DynamicValue = YearStoppingPcLower.HasValue ? YearStoppingPcLower.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TrichiasisSurgeryBacklog"].Id, DynamicValue = TrichiasisSurgeryBacklog.HasValue ? TrichiasisSurgeryBacklog.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TrichiasisSurgeryGoal"].Id, DynamicValue = TrichiasisSurgeryGoal.HasValue ? TrichiasisSurgeryGoal.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TrachomaGoal"].Id, DynamicValue = TrachomaGoal.HasValue ? TrachomaGoal.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["Population"].Id, DynamicValue = Population.HasValue ? Population.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["YearStoppingPc"].Id, DynamicValue = YearStoppingPc.HasValue ? YearStoppingPc.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["YearPcStarted"].Id, DynamicValue = YearPcStarted.HasValue ? YearPcStarted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TrachomaImpactYearMonth"].Id, DynamicValue = TrachomaImpactYearMonth.HasValue ? TrachomaImpactYearMonth.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TasYearMonth"].Id, DynamicValue = TasYearMonth.HasValue ? TasYearMonth.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["DiseaseEndemicity"].Id, DynamicValue = DiseaseEndemicity });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["DiseaseTrichiasis"].Id, DynamicValue = DiseaseTrichiasis });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["TasObjective"].Id, DynamicValue = TasObjective });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["RoundsCurrentlyImplemented"].Id, DynamicValue = RoundsCurrentlyImplemented });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = Indicators["RoundsRecommendedByWho"].Id, DynamicValue = RoundsRecommendedByWho });
        }

        public void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            YearStoppingPcLower = inds["YearStoppingPcLower"].DynamicValue.ToNullable<int>();
            TrichiasisSurgeryBacklog = inds["TrichiasisSurgeryBacklog"].DynamicValue.ToNullable<int>();
            TrichiasisSurgeryGoal = inds["TrichiasisSurgeryGoal"].DynamicValue.ToNullable<int>();
            TrachomaGoal = inds["TrachomaGoal"].DynamicValue.ToNullable<int>();
            Population = inds["Population"].DynamicValue.ToNullable<int>();
            YearStoppingPc = inds["YearStoppingPc"].DynamicValue.ToNullable<int>();
            YearPcStarted = inds["YearPcStarted"].DynamicValue.ToNullable<int>();
            TrachomaImpactYearMonth = inds["TrachomaImpactYearMonth"].DynamicValue.ToNullable<DateTime>();
            TasYearMonth = inds["TasYearMonth"].DynamicValue.ToNullable<DateTime>();
            DiseaseEndemicity = inds["DiseaseEndemicity"].DynamicValue;
            DiseaseTrichiasis = inds["DiseaseTrichiasis"].DynamicValue;
            TasObjective = inds["TasObjective"].DynamicValue;
            RoundsCurrentlyImplemented = inds["RoundsCurrentlyImplemented"].DynamicValue;
            RoundsRecommendedByWho = inds["RoundsRecommendedByWho"].DynamicValue;
        }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "DiseaseEndemicity":
                        if (string.IsNullOrEmpty(DiseaseEndemicity))
                            error = Translations.Required;
                        break;
                    case "YearOfReporting":
                        if (!YearOfReporting.HasValue)
                            error = Translations.Required;
                        else if (YearOfReporting.Value > 2050 || YearOfReporting.Value < 1900)
                            error = Translations.ValidYear;
                        break;

                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion

    }
}

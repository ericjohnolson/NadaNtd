using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;

namespace Nada.Model.Intervention
{
    public class PcMda : IntvBase
    {
        public PcMda() : base()
        {
            Medicines = new List<Medicine>();
            Partners = new List<Partner>();
        }

        // List fields
        public List<Medicine> Medicines { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Disease> DiseasesTargeted { get; set; }

        // dynamic indicators
        public Nullable<int> RoundsPlannedYear { get; set; }
        public Nullable<int> RoundNumber { get; set; }
        public Nullable<int> NumAtRisk { get; set; }
        public Nullable<int> EligibleMalesTargeted { get; set; }
        public Nullable<int> EligibleFemalesTargeted { get; set; }
        public Nullable<int> NumEligibleTargeted { get; set; }
        public Nullable<int> NumTargetedOncho { get; set; }
        public Nullable<int> NumAdultsTargeted { get; set; }
        public Nullable<int> NumSacTargeted { get; set; }
        public Nullable<int> NumPsacTargeted { get; set; }
        public Nullable<int> NumTreatedOncho { get; set; }
        public Nullable<int> NumAdultsTreated { get; set; }
        public Nullable<int> NumSacTreated { get; set; }
        public Nullable<int> NumPsacTreated { get; set; }
        public Nullable<int> NumEligibleMalesTreated { get; set; }
        public Nullable<int> NumEligibleFemalesTreated { get; set; }
        public Nullable<int> NumEligibleTreated { get; set; }
        public Nullable<int> NumTreatedTeo { get; set; }
        public Nullable<int> NumTreatedZxPos { get; set; }
        public Nullable<int> NumTreatedZx { get; set; }
        public Nullable<int> NumSacReported { get; set; }
        public string StockOut { get; set; }
        public string StockOutDrug { get; set; }
        public string StockOutLength { get; set; }
        public Nullable<double> SacCoverage { get; set; }
        public Nullable<double> PsacCoverage { get; set; }
        public Nullable<double> MalesCoverage { get; set; }
        public Nullable<double> FemalesCoverage { get; set; }
        public Nullable<double> EpiCoverage { get; set; }
        public Nullable<double> ProgramCoverage { get; set; }


        public override void MapPropertiesToIndicators()
        {
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["CasualAgent"].Id, DynamicValue = CasualAgent });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["YearFirstRoundPc"].Id, DynamicValue = YearFirstRoundPc.HasValue ? YearFirstRoundPc.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["RoundsMda"].Id, DynamicValue = RoundsMda.HasValue ? RoundsMda.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Examined"].Id, DynamicValue = Examined.HasValue ? Examined.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Positive"].Id, DynamicValue = Positive.HasValue ? Positive.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["PercentPositive"].Id, DynamicValue = PercentPositive.HasValue ? PercentPositive.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MeanDensity"].Id, DynamicValue = MeanDensity.HasValue ? MeanDensity.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MfCount"].Id, DynamicValue = MfCount.HasValue ? MfCount.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MfLoad"].Id, DynamicValue = MfLoad.HasValue ? MfLoad.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["SampleSize"].Id, DynamicValue = SampleSize.HasValue ? SampleSize.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Sampled"].Id, DynamicValue = Sampled.HasValue ? Sampled.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Nonresponsive"].Id, DynamicValue = Nonresponsive.HasValue ? Nonresponsive.Value.ToString() : null });
            //IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["AgeRange"].Id, DynamicValue = AgeRange });
        }

        public override void MapIndicatorsToProperties()
        {
            //Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            //CasualAgent = inds["CasualAgent"].DynamicValue;
            //AgeRange = inds["AgeRange"].DynamicValue;
            //YearFirstRoundPc = inds["YearFirstRoundPc"].DynamicValue.ToNullable<int>();
            //RoundsMda = inds["RoundsMda"].DynamicValue.ToNullable<int>();
            //Examined = inds["Examined"].DynamicValue.ToNullable<int>();
            //Positive = inds["Positive"].DynamicValue.ToNullable<int>();
            //PercentPositive = inds["PercentPositive"].DynamicValue.ToNullable<double>();
            //MeanDensity = inds["MeanDensity"].DynamicValue.ToNullable<double>();
            //MfCount = inds["MfCount"].DynamicValue.ToNullable<int>();
            //MfLoad = inds["MfLoad"].DynamicValue.ToNullable<double>();
            //SampleSize = inds["SampleSize"].DynamicValue.ToNullable<int>();
            //Sampled = inds["Sampled"].DynamicValue.ToNullable<int>();
            //Nonresponsive = inds["Nonresponsive"].DynamicValue.ToNullable<int>();
        }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "NumAtRisk":
                        if (!NumAtRisk.HasValue || NumAtRisk.Value < 0)
                            error = Translations.Required;
                        break;
                    case "NumEligibleTargeted":
                        if (!NumEligibleTargeted.HasValue || NumEligibleTargeted.Value < 0)
                            error = Translations.Required;
                        break;
                    case "NumEligibleTreated":
                        if (!NumEligibleTreated.HasValue || NumEligibleTreated.Value < 0)
                            error = Translations.Required;
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

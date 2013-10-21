using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;

namespace Nada.Model.Intervention
{
    public class PcMda : IntvBase, IDataErrorInfo
    {
        public PcMda() : base()
        {
            Medicines = new List<Medicine>();
            Partners = new List<Partner>();
            DiseasesTargeted = new List<Disease>();
            StockOutValues = new List<string> { "Yes", "No", "NA" };
            StockOutDrugValues = new List<string> { "IVM", "ALB" };
            StockOutLengthValues = new List<string> { "days2", "days14", "weeks4" , "GreaterThanMonth", "NotResolved", "NA" };
        }

        // List fields
        public List<Medicine> Medicines { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Disease> DiseasesTargeted { get; set; }
        // Values
        public List<string> StockOutValues { get; set; }
        public List<string> StockOutDrugValues { get; set; }
        public List<string> StockOutLengthValues { get; set; }

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
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["RoundsPlannedYear"].Id, DynamicValue = RoundsPlannedYear.HasValue ? RoundsPlannedYear.Value.ToString() : null });

            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["RoundNumber"].Id, DynamicValue = RoundNumber.HasValue ? RoundNumber.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumAtRisk"].Id, DynamicValue = NumAtRisk.HasValue ? NumAtRisk.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["EligibleMalesTargeted"].Id, DynamicValue = EligibleMalesTargeted.HasValue ? EligibleMalesTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["EligibleFemalesTargeted"].Id, DynamicValue = EligibleFemalesTargeted.HasValue ? EligibleFemalesTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumEligibleTargeted"].Id, DynamicValue = NumEligibleTargeted.HasValue ? NumEligibleTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumTargetedOncho"].Id, DynamicValue = NumTargetedOncho.HasValue ? NumTargetedOncho.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumAdultsTargeted"].Id, DynamicValue = NumAdultsTargeted.HasValue ? NumAdultsTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumSacTargeted"].Id, DynamicValue = NumSacTargeted.HasValue ? NumSacTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumPsacTargeted"].Id, DynamicValue = NumPsacTargeted.HasValue ? NumPsacTargeted.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumTreatedOncho"].Id, DynamicValue = NumTreatedOncho.HasValue ? NumTreatedOncho.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumAdultsTreated"].Id, DynamicValue = NumAdultsTreated.HasValue ? NumAdultsTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumSacTreated"].Id, DynamicValue = NumSacTreated.HasValue ? NumSacTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumPsacTreated"].Id, DynamicValue = NumPsacTreated.HasValue ? NumPsacTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumEligibleMalesTreated"].Id, DynamicValue = NumEligibleMalesTreated.HasValue ? NumEligibleMalesTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumEligibleFemalesTreated"].Id, DynamicValue = NumEligibleFemalesTreated.HasValue ? NumEligibleFemalesTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumEligibleTreated"].Id, DynamicValue = NumEligibleTreated.HasValue ? NumEligibleTreated.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumTreatedTeo"].Id, DynamicValue = NumTreatedTeo.HasValue ? NumTreatedTeo.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumTreatedZxPos"].Id, DynamicValue = NumTreatedZxPos.HasValue ? NumTreatedZxPos.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumTreatedZx"].Id, DynamicValue = NumTreatedZx.HasValue ? NumTreatedZx.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["NumSacReported"].Id, DynamicValue = NumSacReported.HasValue ? NumSacReported.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["SacCoverage"].Id, DynamicValue = SacCoverage.HasValue ? SacCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["PsacCoverage"].Id, DynamicValue = PsacCoverage.HasValue ? PsacCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["MalesCoverage"].Id, DynamicValue = MalesCoverage.HasValue ? MalesCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["FemalesCoverage"].Id, DynamicValue = FemalesCoverage.HasValue ? FemalesCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["EpiCoverage"].Id, DynamicValue = EpiCoverage.HasValue ? EpiCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["ProgramCoverage"].Id, DynamicValue = ProgramCoverage.HasValue ? ProgramCoverage.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["StockOut"].Id, DynamicValue = StockOut });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["StockOutDrug"].Id, DynamicValue = StockOutDrug });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = IntvType.Indicators["StockOutLength"].Id, DynamicValue = StockOutLength });
        }

        public override void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            StockOut= inds["StockOut"].DynamicValue;
            StockOutDrug = inds["StockOutDrug"].DynamicValue;
            StockOutLength = inds["StockOutLength"].DynamicValue;
            RoundsPlannedYear = inds["RoundsPlannedYear"].DynamicValue.ToNullable<int>();
            RoundNumber = inds["RoundNumber"].DynamicValue.ToNullable<int>();
            NumAtRisk = inds["NumAtRisk"].DynamicValue.ToNullable<int>();
            EligibleMalesTargeted = inds["EligibleMalesTargeted"].DynamicValue.ToNullable<int>();
            EligibleFemalesTargeted = inds["EligibleFemalesTargeted"].DynamicValue.ToNullable<int>();
            NumEligibleTargeted = inds["NumEligibleTargeted"].DynamicValue.ToNullable<int>();
            NumTargetedOncho = inds["NumTargetedOncho"].DynamicValue.ToNullable<int>();
            NumAdultsTargeted = inds["NumAdultsTargeted"].DynamicValue.ToNullable<int>();
            NumSacTargeted = inds["NumSacTargeted"].DynamicValue.ToNullable<int>();
            NumPsacTargeted = inds["NumPsacTargeted"].DynamicValue.ToNullable<int>();
            NumTreatedOncho = inds["NumTreatedOncho"].DynamicValue.ToNullable<int>();
            NumAdultsTreated = inds["NumAdultsTreated"].DynamicValue.ToNullable<int>();
            NumSacTreated = inds["NumSacTreated"].DynamicValue.ToNullable<int>();
            NumPsacTreated = inds["NumPsacTreated"].DynamicValue.ToNullable<int>();
            NumEligibleMalesTreated = inds["NumEligibleMalesTreated"].DynamicValue.ToNullable<int>();
            NumEligibleFemalesTreated = inds["NumEligibleFemalesTreated"].DynamicValue.ToNullable<int>();
            NumEligibleTreated = inds["NumEligibleTreated"].DynamicValue.ToNullable<int>();
            NumTreatedTeo = inds["NumTreatedTeo"].DynamicValue.ToNullable<int>();
            NumTreatedZxPos = inds["NumTreatedZxPos"].DynamicValue.ToNullable<int>();
            NumTreatedZx = inds["NumTreatedZx"].DynamicValue.ToNullable<int>();
            NumSacReported = inds["NumSacReported"].DynamicValue.ToNullable<int>();
            SacCoverage = inds["SacCoverage"].DynamicValue.ToNullable<double>();
            PsacCoverage = inds["PsacCoverage"].DynamicValue.ToNullable<double>();
            MalesCoverage = inds["MalesCoverage"].DynamicValue.ToNullable<double>();
            FemalesCoverage = inds["FemalesCoverage"].DynamicValue.ToNullable<double>();
            EpiCoverage = inds["EpiCoverage"].DynamicValue.ToNullable<double>();
            ProgramCoverage = inds["ProgramCoverage"].DynamicValue.ToNullable<double>();
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

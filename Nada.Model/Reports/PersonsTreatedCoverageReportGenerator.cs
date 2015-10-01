using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nada.Model.Reports
{
    public abstract class PersonsTreatedCoverageBaseReportGenerator : BaseReportGenerator
    {
        protected DiseaseRepository DiseaseRepo = new DiseaseRepository();
        protected DemoRepository DemoRepo = new DemoRepository();
        protected IntvRepository IntvRepo = new IntvRepository();
        protected List<IntvType> IntvTypes { get; set; }
        protected List<Disease> Diseases { get; set; }
        protected Dictionary<int, double> RowToPopAtRisk = new Dictionary<int, double>();

        protected abstract List<IntvType> DetermineIntvTypes(PersonsTreatedCoverageReportOptions standardOpts);
        protected abstract List<IntvType> DetermineIntvTypes(List<Disease> diseases);
        protected abstract List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts);
        protected abstract List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts, List<IntvBase> interventions);

        protected void HydrateIntvTypes(List<IntvType> intvTypes)
        {
            // Get the indicators for each type
            for (int i = 0; i < intvTypes.Count; i++)
            {
                intvTypes[i] = IntvRepo.GetIntvType(intvTypes[i].Id);
            }
        }

        protected void AddIndicators(int id, string name, IHaveDynamicIndicators dd, string formName, string formTranslationKey,
            ReportOptions options)
        {
            if (dd.Indicators.ContainsKey(name) && options.SelectedIndicators.Count(i => i.Name == name && i.FormNameKey == formTranslationKey) < 1)
            {
                options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(id,
                    new KeyValuePair<string, Indicator>(name, dd.Indicators[name]), formName, formTranslationKey));
            }
        }

        protected void TryRemoveColumn(string p, DataTable dataTable)
        {
            if (dataTable.Columns.Contains(p))
                dataTable.Columns.Remove(p);
        }

        protected double GetColumnDouble(string col, DataTable dataTable, DataRow row)
        {
            double d = 0;
            if (dataTable.Columns.Contains(col))
                if (double.TryParse(row[col].ToString(), out d))
                    return d;
            return 0;
        }

        /// <summary>
        /// Aggregates data for a given indicator by round
        /// </summary>
        /// <param name="data">Collection that stores the data separated by rounds</param>
        /// <param name="row">The current row to check</param>
        /// <param name="col">The current column to check</param>
        /// <param name="commonColName">The common indicator name that is shared between all rounds</param>
        protected void AggregateRounds(Dictionary<string, ReportColumn> data, DataRow row, DataColumn col, string commonColName)
        {
            if (col.ColumnName.Contains(commonColName))
            {
                if (!data.ContainsKey(commonColName))
                {
                    ReportColumn reportCol = ReportColumn.ParseFactory(commonColName);
                    data.Add(commonColName, reportCol);
                }

                data[commonColName].AddValue(col.ColumnName, row[col.ColumnName].ToString());
            }
        }

        /// <summary>
        /// Copies the data from the source DataTable to the destination DataTable and places the data on the correspondn row
        /// 
        /// This method assumes that the source DataTable has the exact same rows as the destination Datatable
        /// </summary>
        /// <param name="source">Source DataTable</param>
        /// <param name="dest">Destination DataTable</param>
        protected void CopyDataTableToSameRows(DataTable source, DataTable dest)
        {
            foreach (DataColumn col in source.Columns)
            {
                dest.Columns.Add(new DataColumn(col.ColumnName));
            }
            for (int i = 0; i < dest.Rows.Count; i++)
            {
                DataRow row = dest.Rows[i];
                foreach (DataColumn col in source.Columns)
                {
                    row[col.ColumnName] = source.Rows[i][col.ColumnName];
                }
            }
        }

        /// <summary>
        /// Removes columns until the specified column is reached
        /// </summary>
        /// <param name="colName">The name of the column to stop at</param>
        protected void RemovePastColumn(DataTable table, string colName)
        {
            // Remove the columns until the specified column is removed
            while (table.Columns.Contains(colName))
            {
                table.Columns.RemoveAt(0);
            }
        }

        /// <summary>
        /// Clones a saved report so the report options can be re-used for another report.
        /// 
        /// Clears out the selected indicators since that is the only report option that needs to be changed for this report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        protected SavedReport CloneReport(SavedReport report)
        {
            SavedReport newReport = new SavedReport();
            newReport.ReportOptions = Util.DeepClone(report.ReportOptions);
            newReport.ReportOptions.SelectedIndicators = new List<ReportIndicator>();
            return newReport;
        }

        protected ReportResult RunDistributionReport(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            // Add the disease indicators to the report
            foreach (var disease in Diseases)
            {
                DiseaseDistroPc dd = DiseaseRepo.Create((DiseaseType)disease.Id);

                switch (disease.Id)
                {

                    case (int)DiseaseType.Lf:
                        AddIndicators(disease.Id, "DDLFPopulationAtRisk", dd, dd.Disease.DisplayName, dd.Disease.DisplayNameKey, report.ReportOptions);
                        break;
                    case (int)DiseaseType.Trachoma:
                        AddIndicators(disease.Id, "DDTraPopulationAtRisk", dd, dd.Disease.DisplayName, dd.Disease.DisplayNameKey, report.ReportOptions);
                        break;
                    case (int)DiseaseType.Oncho:
                        AddIndicators(disease.Id, "DDOnchoPopulationAtRisk", dd, dd.Disease.DisplayName, dd.Disease.DisplayNameKey, report.ReportOptions);
                        break;
                    case (int)DiseaseType.STH:
                        AddIndicators(disease.Id, "DDSTHPopulationAtRisk", dd, dd.Disease.DisplayName, dd.Disease.DisplayNameKey, report.ReportOptions);
                        break;
                    case (int)DiseaseType.Schisto:
                        AddIndicators(disease.Id, "DDSchistoPopulationAtRisk", dd, dd.Disease.DisplayName, dd.Disease.DisplayNameKey, report.ReportOptions);
                        break;
                    default:
                        break;
                }
            }

            // Run the report
            DistributionReportGenerator gen = new DistributionReportGenerator();
            ReportResult result = gen.Run(report);

            return result;
        }

        protected ReportResult RunIntvReport(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts, List<int> filteredIntvIds)
        {
            // Add all the relevant intervention indicators
            foreach (IntvType intvType in IntvTypes)
            {
                AddIndicators(intvType.Id, "PcIntvNumEligibleIndividualsTargeted", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvNumIndividualsTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvPsacTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvNumSacTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
            }

            // Report gen
            IntvReportGenerator gen = new IntvReportGenerator();
            // Set the IDs of the filtererd interventions
            if (filteredIntvIds != null && filteredIntvIds.Count > 0)
                gen.CmdTextOverride = DetermineInterventionSql(filteredIntvIds, report.ReportOptions);
            // Recent distro static classs
            RecentDistro recentDistro = RecentDistro.GetInstance(true /* instantiate */);
            recentDistro.Run(report.ReportOptions);
            // Run the report
            ReportResult result = gen.Run(report);
            // Clear the RecentDistro from memory
            RecentDistro.ClearInstance();

            return result;
        }

        protected void AggregateDistData(ReportResult result)
        {
            // See if the pop at risk columns should be combined
            bool CombineAtRiskCols = this.GetType() == typeof(PersonsTreatedCoverageDiseaseReportGenerator)
                || (Diseases.Count <= 1 && this.GetType() == typeof(PersonsTreatedCoverageDrugPackageReportGenerator));

            // Add the combined pop at risk column if necesssarsy
            if (CombineAtRiskCols)
                result.DataTableResults.Columns.Add(new DataColumn(TranslationLookup.GetValue("EliminationAtRisk")));

            // Aggregate each distribution row
            for (int i = 0; i < result.DataTableResults.Rows.Count; i++)
            {
                DataRow row = result.DataTableResults.Rows[i];

                double totalAtRisk = 0;
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDLFPopulationAtRisk") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDTraPopulationAtRisk") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDOnchoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDSTHPopulationAtRisk") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDSchistoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults, row);

                // Add the total pop at risk to the map
                RowToPopAtRisk.Add(i, totalAtRisk);

                // Add the total to the pop at risk column if necessary
                if (CombineAtRiskCols)
                    row[TranslationLookup.GetValue("EliminationAtRisk")] = totalAtRisk;
            }

            // Remove original columns that were just used for aggregation
            if (CombineAtRiskCols)
            {
                TryRemoveColumn(TranslationLookup.GetValue("DDLFPopulationAtRisk") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults);
                TryRemoveColumn(TranslationLookup.GetValue("DDTraPopulationAtRisk") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults);
                TryRemoveColumn(TranslationLookup.GetValue("DDOnchoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults);
                TryRemoveColumn(TranslationLookup.GetValue("DDSTHPopulationAtRisk") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults);
                TryRemoveColumn(TranslationLookup.GetValue("DDSchistoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults);
            }
        }

        protected DataTable AggregateIntvData(ReportResult result)
        {
            // Aggregate data into objects
            Dictionary<int, Dictionary<string, ReportColumn>> intvDataDict = new Dictionary<int, Dictionary<string, ReportColumn>>();
            for (int i = 0; i < result.DataTableResults.Rows.Count; i++)
            {
                Dictionary<string, ReportColumn> data = new Dictionary<string, ReportColumn>();
                DataRow row = result.DataTableResults.Rows[i];
                foreach (DataColumn col in result.DataTableResults.Columns)
                {
                    AggregateRounds(data, row, col, TranslationLookup.GetValue("PcIntvNumEligibleIndividualsTargeted"));
                    AggregateRounds(data, row, col, TranslationLookup.GetValue("PcIntvNumIndividualsTreated"));
                    AggregateRounds(data, row, col, TranslationLookup.GetValue("PcIntvPsacTreated"));
                    AggregateRounds(data, row, col, TranslationLookup.GetValue("PcIntvNumSacTreated"));
                }

                intvDataDict.Add(i, data);
            }
            DataTable intvDataTable = new DataTable();
            foreach (KeyValuePair<int, Dictionary<string, ReportColumn>> entry in intvDataDict)
            {
                // Instantiate a new row
                DataRow row = intvDataTable.NewRow();

                // Add the data columns
                foreach (KeyValuePair<string, ReportColumn> dataEntry in entry.Value)
                {
                    // Iterate through each aggregate column
                    foreach (KeyValuePair<int, double> roundEntry in dataEntry.Value.RoundValues)
                    {
                        // Determine the col name + round number string
                        string roundColName;
                        if (roundEntry.Key == -1)
                            roundColName = dataEntry.Value.ColumnName;
                        else
                            roundColName = string.Format("{0} - {1} {2}", dataEntry.Value.ColumnName, TranslationLookup.GetValue("Round"), roundEntry.Key);

                        // Add the column
                        if (!intvDataTable.Columns.Contains(roundColName))
                            intvDataTable.Columns.Add(new DataColumn(roundColName));
                        // Add the value
                        row[roundColName] = roundEntry.Value;

                        // Calculate epi and program coverage
                        if (dataEntry.Value.ColumnName == TranslationLookup.GetValue("PcIntvNumIndividualsTreated"))
                        {
                            CalculateEpi(row, entry.Key, roundEntry.Key, roundEntry.Value);
                            CalculateProgramCoverage(row, entry.Key, roundEntry.Key, roundEntry.Value, entry.Value);
                        }
                    }
                }

                // Add the row
                intvDataTable.Rows.Add(row);
            }

            return intvDataTable;
        }

        protected void CalculateEpi(DataRow row, int rowNumber, int round, double individualsTreated)
        {
            // Get the corresponding population at risk value
            if (RowToPopAtRisk != null && RowToPopAtRisk.ContainsKey(rowNumber))
            {
                double popAtRisk = RowToPopAtRisk[rowNumber];
                // Calculate the epi coverage
                double epiCoverage;
                if (individualsTreated > 0 && popAtRisk > 0)
                    epiCoverage = individualsTreated / popAtRisk;
                else
                    epiCoverage = 0;
                // Deteremine the column name
                string roundColName;
                if (round == -1)
                    roundColName = TranslationLookup.GetValue("EpiCoverage");
                else
                    roundColName = string.Format("{0} - {1} {2}", TranslationLookup.GetValue("EpiCoverage"), TranslationLookup.GetValue("Round"), round);
                // Add the column if it does not exist
                if (!row.Table.Columns.Contains(roundColName))
                    row.Table.Columns.Add(new DataColumn(roundColName));
                // Add the epi coverage value
                row[roundColName] = string.Format("{0:0.00}", epiCoverage * 100);
            }
        }

        protected void CalculateProgramCoverage(DataRow row, int rowNumber, int round, double individualsTreated, Dictionary<string, ReportColumn> rowData)
        {
            if (rowData.ContainsKey(TranslationLookup.GetValue("PcIntvNumEligibleIndividualsTargeted")))
            {
                // Get all the round data
                ReportColumn individualsTargetedRoundData = rowData[TranslationLookup.GetValue("PcIntvNumEligibleIndividualsTargeted")];
                // Look for data with the corresponding round
                if (individualsTargetedRoundData.RoundValues.ContainsKey(round))
                {
                    // Get the individuals targeted for the corresponding round
                    double individualsTargeted = individualsTargetedRoundData.RoundValues[round];
                    // Calculate the program coverage
                    double programCoverage;
                    if (individualsTreated > 0 && individualsTargeted > 0)
                        programCoverage = individualsTreated / individualsTargeted;
                    else
                        programCoverage = 0;
                    // Deteremine the column name
                    string roundColName;
                    if (round == -1)
                        roundColName = TranslationLookup.GetValue("ProgramCoverage");
                    else
                        roundColName = string.Format("{0} - {1} {2}", TranslationLookup.GetValue("ProgramCoverage"), TranslationLookup.GetValue("Round"), round);
                    // Add the column if it does not exist
                    if (!row.Table.Columns.Contains(roundColName))
                        row.Table.Columns.Add(new DataColumn(roundColName));
                    // Add the program coverage value
                    row[roundColName] = string.Format("{0:0.00}", programCoverage * 100);
                }
            }
        }

        protected List<int> DetermineInterventionsByDiseasesTargeted(List<IntvType> intvTypes, ReportOptions reportOptions, List<Disease> diseasesToFilterBy)
        {
            List<IntvBase> interventions = GetIntvsByReportOptions(intvTypes, reportOptions);

            // Filter the interventions based on whether or not they have the correct diseases targeted
            List<int> filteredIntvIds = new List<int>();
            foreach (IntvBase intv in interventions)
            {
                // Get the diseases targeted indicator
                IndicatorValue indVal = intv.IndicatorValues.Where(i => i.Indicator.DisplayName == "PcIntvDiseases").FirstOrDefault();
                if (indVal == null)
                    continue;

                // Get the value of the diseases targeted indicator
                string selectedDiseasesStr = indVal.DynamicValue;

                // Make sure the current intervention has one of the diseases targeted selected
                foreach (Disease disease in diseasesToFilterBy)
                {
                    if (selectedDiseasesStr.Contains(disease.DisplayNameKey))
                    {
                        filteredIntvIds.Add(intv.Id);
                        break;
                    }
                }
            }

            return filteredIntvIds;
        }

        protected List<IntvBase> GetIntvsByReportOptions(List<IntvType> intvTypes, ReportOptions reportOptions)
        {
            // Build a collection of ids for the intervention types
            List<int> intvTypeIds = new List<int>();
            foreach (IntvType type in intvTypes)
            {
                intvTypeIds.Add(type.Id);
            }

            // Build a collection of ids for the admin units
            List<int> adminUnitIds = new List<int>();
            foreach (AdminLevel level in reportOptions.SelectedAdminLevels)
            {
                adminUnitIds.Add(level.Id);
            }

            // Get all interventions that match the report options
            List<IntvBase> interventions = new List<IntvBase>();
            foreach (int year in reportOptions.Years)
            {
                DateTime startDate = new DateTime(year, 1, 1);
                DateTime yearEndDate = new DateTime(year, 1, 1).AddYears(1).AddDays(-1);
                List<IntvBase> interventionsForYear = IntvRepo.GetAllIntvInRangeByAdminUnit(intvTypeIds, startDate, yearEndDate, adminUnitIds);
                // Add them to the main collection
                interventions.AddRange(interventionsForYear);
            }

            return interventions;
        }

        public override ReportResult Run(SavedReport report)
        {
            PersonsTreatedCoverageReportOptions standardOpts = (PersonsTreatedCoverageReportOptions)report.StandardReportOptions;

            // Options
            report.ReportOptions.IsByLevelAggregation = true;
            report.ReportOptions.IsCountryAggregation = false;
            report.ReportOptions.IsNoAggregation = false;

            // Get all admin levels
            if (report.ReportOptions.IsAllLocations)
                report.ReportOptions.SelectedAdminLevels = DemoRepo.GetAdminLevelByLevel(standardOpts.DistrictType.LevelNumber).Where(a => a.LevelNumber == standardOpts.DistrictType.LevelNumber).ToList();

            List<int> filteredIntvIds;
            if (this.GetType() == typeof(PersonsTreatedCoverageDrugPackageReportGenerator))
            {
                // Determine intervention types
                IntvTypes = DetermineIntvTypes(standardOpts);
                // Get all the matching interventions based on the report options
                List<IntvBase> intvs = GetIntvsByReportOptions(IntvTypes, report.ReportOptions);
                // Determine the diseases
                Diseases = DetermineDiseases(standardOpts, intvs);
                // Drug package report does not need to be filtered
                filteredIntvIds = null;
            }
            else
            {
                // It is a disease report, so first set the diseases thats were selected
                Diseases = DetermineDiseases(standardOpts);
                // Deterine the interventions associated to the diseases
                IntvTypes = DetermineIntvTypes(Diseases);
                // Determine which interventions should be used in the report
                filteredIntvIds = DetermineInterventionsByDiseasesTargeted(standardOpts.AvailableDrugPackages, report.ReportOptions, Diseases);
            }

            // There was no data at this reporting level
            if (Diseases == null || IntvTypes == null || Diseases.Count < 1 || IntvTypes.Count < 1)
            {
                ReportResult result = new ReportResult();
                result.DataTableResults = new DataTable();
                return result;
            }

            // Run the distribution report for the disease dist related data
            ReportResult distReportResult = RunDistributionReport(CloneReport(report), standardOpts);
            // Aggregate the dist data
            AggregateDistData(distReportResult);

            // Run the intervention report
            ReportResult intvReportResult = RunIntvReport(CloneReport(report), standardOpts, filteredIntvIds);
            // Remove the district and year columns from the intervention report result
            RemovePastColumn(intvReportResult.DataTableResults, TranslationLookup.GetValue("Year"));
            // Aggregate the Report data
            DataTable intvDataTable = AggregateIntvData(intvReportResult);

            // Merge the results
            CopyDataTableToSameRows(intvDataTable, distReportResult.DataTableResults);

            distReportResult.ChartData = distReportResult.DataTableResults.Copy();
            distReportResult.DataTableResults.Columns.Remove(TranslationLookup.GetValue("Type"));
            return distReportResult;
        }

        protected string DetermineInterventionSql(List<int> filteredIntvIds, ReportOptions reportOptions)
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        [DateReported], 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName, 
                        InterventionTypes.ID as Tid,      
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.IsEditable, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue, 
                        InterventionIndicatorValues.MemoValue
                        FROM ((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN InterventionIndicatorValues on Interventions.Id = InterventionIndicatorValues.InterventionId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                        WHERE Interventions.IsDeleted = 0"
            + " AND Interventions.ID IN (" + String.Join(", ", filteredIntvIds.Select(i => i.ToString()).ToArray()) + ") "
            + " AND InterventionIndicators.Id in (" + String.Join(", ", reportOptions.SelectedIndicators.Select(s => s.ID.ToString()).ToArray())
            + ") AND InterventionTypes.ID in (" + String.Join(", ", reportOptions.SelectedIndicators.Select(i => i.TypeId.ToString()).Distinct().ToArray()) + ") "
            + ReportRepository.CreateYearFilter(reportOptions, "DateReported") + ReportRepository.CreateAdminFilter(reportOptions)
            + " ORDER BY IsEditable DESC, InterventionIndicators.SortOrder";
        }
    }

    public class PersonsTreatedCoverageDiseaseReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
        protected override List<IntvType> DetermineIntvTypes(List<Disease> diseases)
        {
            // Selected disease IDs
            List<int> diseaseIds = diseases.Select(d => d.Id).ToList();

            // Get all intervention typess that are associated to the selected diseases
            List<IntvType> intvTypes = IntvRepo.GetAllTypesByDiseases(diseaseIds);
            // Get the indicators for each type
            HydrateIntvTypes(intvTypes);

            return intvTypes;
        }

        protected override List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts)
        {
            return standardOpts.Diseases;
        }

        protected override List<IntvType> DetermineIntvTypes(PersonsTreatedCoverageReportOptions standardOpts)
        {
            throw new NotImplementedException();
        }

        protected override List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts, List<IntvBase> interventions)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonsTreatedCoverageDrugPackageReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
        protected override List<IntvType> DetermineIntvTypes(PersonsTreatedCoverageReportOptions standardOpts)
        {
            List<IntvType> intvTypes = standardOpts.DrugPackages;
            HydrateIntvTypes(intvTypes);
            return intvTypes;
        }

        protected override List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts, List<IntvBase> interventions)
        {
            // Will hold the diseases
            List<Disease> diseases = new List<Disease>();
            // Iterate through the interventions and add the targeted diseases to the collection if they are selected
            foreach (IntvBase intv in interventions)
            {
                IndicatorValue indicatorVal = intv.IndicatorValues.Where(ind => ind.Indicator.DisplayName == "PcIntvDiseases").FirstOrDefault();
                if (indicatorVal == null)
                    continue;

                // See if any of the available diseases are selected
                foreach (Disease disease in standardOpts.AvailableDiseases)
                {
                    if (indicatorVal.DynamicValue != null && indicatorVal.DynamicValue.Contains(disease.DisplayNameKey) && !diseases.Contains(disease))
                        diseases.Add(disease);
                }

                // No need to check any more interventions if all the diseases have been targeted
                if (diseases.Count >= standardOpts.AvailableDiseases.Count)
                {
                    return diseases;
                }
            }

            return diseases;
        }

        protected override List<IntvType> DetermineIntvTypes(List<Disease> diseases)
        {
            throw new NotImplementedException();
        }

        protected override List<Disease> DetermineDiseases(PersonsTreatedCoverageReportOptions standardOpts)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a column in a DataTable report with multiple rounds
    /// </summary>
    public class ReportColumn
    {
        /// <summary>
        /// The indicator name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Mapping of the round number to the value for that round
        /// </summary>
        public Dictionary<int, double> RoundValues { get; set; }

        public ReportColumn()
        {
            RoundValues = new Dictionary<int, double>();
        }

        public static ReportColumn ParseFactory(string colName)
        {
            ReportColumn col = new ReportColumn();
            col.ColumnName = colName;
            return col;
        }

        /// <summary>
        /// Given the indicator name in the form of [Indicator name] - Round n, will determine the
        /// round number and add the data to the corresponding round
        /// </summary>
        /// <param name="roundColName">The indicator name</param>
        /// <param name="value">The value</param>
        public void AddValue(string roundColName, string value)
        {
            int lastIndex = roundColName.LastIndexOf(TranslationLookup.GetValue("Round"));
            if (lastIndex >= 0)
            {
                string roundString = roundColName.Substring(lastIndex);
                string roundNumberString = Regex.Replace(roundString, @"[^\d]", "");
                int roundNumber = Int32.Parse(roundNumberString);

                if (!RoundValues.ContainsKey(roundNumber))
                    RoundValues.Add(roundNumber, 0);

                double appendVal = 0;
                double.TryParse(value, out appendVal);

                RoundValues[roundNumber] = RoundValues[roundNumber] + appendVal;
            }
            else if (lastIndex == -1) // There is no round number
            {
                double appendVal = 0;
                double.TryParse(value, out appendVal);

                if (!RoundValues.ContainsKey(-1))
                    RoundValues.Add(-1, 0);

                RoundValues[-1] = RoundValues[-1] + appendVal;
            }
        }
    }
}

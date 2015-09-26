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
        public DiseaseRepository DiseaseRepo = new DiseaseRepository();
        public DemoRepository DemoRepo = new DemoRepository();
        public IntvRepository IntvRepo = new IntvRepository();
        public List<IntvType> IntvTypes { get; set; }
        public List<Disease> Diseases { get; set; }

        public abstract List<IntvType> DetermineIntvTypes(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts);
        public abstract List<Disease> DetermineDiseases(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts);

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

        public ReportResult RunDistributionReport(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
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

        public ReportResult RunIntvReport(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            /*List<IntvBase> intvs = IntvRepo.GetAll(intvTypes.Select(i => i.Id).ToList(),
                report.ReportOptions.SelectedAdminLevels.Select(l => l.Id).ToList());

            // Get the disease DisplayName keys so that the values can be filtered
            List<string> diseaseKeys = DiseaseRepo.GetSelectedDiseases().Where(d => diseaseIds.Contains(d.Id)).Select(d => d.DisplayNameKey).ToList();

            // Get all interventions that have the PcIntvDiseases (diseases targeted) indicator and have the selected diseases selected in that indicator
            List<IntvBase> filteredIntvs = new List<IntvBase>();
            foreach (IntvBase intv in intvs)
            {
                if (intv.IndicatorValues.Where(ind => ind.Indicator.DisplayName == "PcIntvDiseases").ToList().Count > 0)
                {
                    IndicatorValue indicatorVal = intv.IndicatorValues.Where(ind => ind.Indicator.DisplayName == "PcIntvDiseases").FirstOrDefault();
                    if (diseaseKeys.Any(indicatorVal.DynamicValue.Contains))
                        filteredIntvs.Add(intv);
                }
            }

            // Generate a report with the intervention data
            foreach (IntvBase intv in filteredIntvs)
            {

            }*/

            // Add all the relevant intervention indicators
            foreach (IntvType intvType in IntvTypes)
            {
                AddIndicators(intvType.Id, "PcIntvNumEligibleIndividualsTargeted", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvNumIndividualsTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvPsacTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvNumSacTreated", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                AddIndicators(intvType.Id, "PcIntvProgramCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                if (intvType.Id == (int)StaticIntvType.IvmAlb)
                {
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvLfEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverageOfOncho", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.DecAlb)
                {
                    AddIndicators(intvType.Id, "PcIntvSthPsacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvLfEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.Ivm)
                {
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverageOfOncho", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.PzqAlb)
                {
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.PzqMbd)
                {
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.Pzq)
                {
                    AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.Alb)
                {
                    AddIndicators(intvType.Id, "PcIntvSthPsacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.Mbd)
                {
                    AddIndicators(intvType.Id, "PcIntvSthPsacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.Alb2)
                {
                    AddIndicators(intvType.Id, "PcIntvLfEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.IvmPzq)
                {
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverageOfOncho", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.IvmPzqAlb)
                {
                    AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvLfEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverageOfOncho", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                    AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                else if (intvType.Id == (int)StaticIntvType.ZithroTeo)
                {
                    AddIndicators(intvType.Id, "PcIntvTraEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, report.ReportOptions);
                }
                //AddIndicators(intvType.Id, "PcIntvSthPsacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvSthSacEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvSthEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvLfEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvSchSacEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvSchEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvTraEpi", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvOnchoEpiCoverageOfOncho", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
                //AddIndicators(intvType.Id, "PcIntvEpiCoverage", intvType, intvType.IntvTypeName, intvType.DisplayNameKey, options);
            }

            // Report gen
            IntvReportGenerator gen = new IntvReportGenerator();
            // Recent distro static classs
            RecentDistro recentDistro = RecentDistro.GetInstance(true /* instantiate */);
            recentDistro.Run(report.ReportOptions);
            // Run the report
            ReportResult result = gen.Run(report);
            // Clear the RecentDistro from memory
            RecentDistro.ClearInstance();

            return result;
        }

        public void AggregateDistData(ReportResult result)
        {
            // loop table, make sure column exists (with 0), sum other columns to good column and delete other columns if they have em.
            result.DataTableResults.Columns.Add(new DataColumn(Translations.EliminationAtRisk));
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                double totalAtRisk = 0;
                totalAtRisk += GetColumnDouble(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults, row);
                row[Translations.EliminationAtRisk] = totalAtRisk;
            }

            // Remove original columns that were just used for aggregation
            TryRemoveColumn(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults);
            TryRemoveColumn(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults);
            TryRemoveColumn(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults);
            TryRemoveColumn(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults);
            TryRemoveColumn(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults);
        }

        public DataTable AggregateIntvData(ReportResult result)
        {
            // Aggregate data into objects
            Dictionary<int, Dictionary<string, ReportColumn>> intvDataDict = new Dictionary<int, Dictionary<string, ReportColumn>>();
            for (int i = 0; i < result.DataTableResults.Rows.Count; i++)
            {
                Dictionary<string, ReportColumn> data = new Dictionary<string, ReportColumn>();
                DataRow row = result.DataTableResults.Rows[i];
                foreach (DataColumn col in result.DataTableResults.Columns)
                {
                    AggregateRounds(data, row, col, Translations.PcIntvNumEligibleIndividualsTargeted);
                    AggregateRounds(data, row, col, Translations.PcIntvNumIndividualsTreated);
                    AggregateRounds(data, row, col, Translations.PcIntvPsacTreated);
                    AggregateRounds(data, row, col, Translations.PcIntvNumSacTreated);
                    AggregateRounds(data, row, col, Translations.PcIntvProgramCoverage);

                    AggregateRounds(data, row, col, Translations.PcIntvSthPsacEpiCoverage);
                    AggregateRounds(data, row, col, Translations.PcIntvSthSacEpiCoverage);
                    AggregateRounds(data, row, col, Translations.PcIntvSthEpiCoverage);
                    AggregateRounds(data, row, col, Translations.PcIntvLfEpiCoverage);
                    AggregateRounds(data, row, col, Translations.PcIntvOnchoEpiCoverage);
                    AggregateRounds(data, row, col, Translations.PcIntvSchSacEpi);
                    AggregateRounds(data, row, col, Translations.PcIntvSchEpi);
                    AggregateRounds(data, row, col, Translations.PcIntvTraEpi);
                    //AggregateRounds(data, row, col, Translations.PcIntvOnchoEpiCoverageOfOncho);
                    AggregateRounds(data, row, col, Translations.PcIntvEpiCoverage);
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
                        string roundColName = string.Format("{0} - {1} {2}", dataEntry.Value.ColumnName, Translations.Round, roundEntry.Key);
                        // Add the column
                        if (!intvDataTable.Columns.Contains(roundColName))
                            intvDataTable.Columns.Add(new DataColumn(roundColName));
                        // Add the value
                        row[roundColName] = roundEntry.Value;
                    }
                }

                // Add the row
                intvDataTable.Rows.Add(row);
            }

            return intvDataTable;
        }

        public override ReportResult Run(SavedReport report)
        {
            PersonsTreatedCoverageReportOptions standardOpts = (PersonsTreatedCoverageReportOptions)report.StandardReportOptions;

            // Options
            report.ReportOptions.IsByLevelAggregation = true;
            report.ReportOptions.IsCountryAggregation = false;
            report.ReportOptions.IsNoAggregation = false;

            // Get all admin levels
            report.ReportOptions.SelectedAdminLevels = DemoRepo.GetAdminLevelByLevel(standardOpts.DistrictType.LevelNumber).Where(a => a.LevelNumber == standardOpts.DistrictType.LevelNumber).ToList();

            // Determine the diseases
            Diseases = DetermineDiseases(report, standardOpts);
            // Determine intervention types
            IntvTypes = DetermineIntvTypes(report, standardOpts);

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
            if (this.GetType() == typeof(PersonsTreatedCoverageDiseaseReportGenerator)
                || (Diseases.Count <= 1 && this.GetType() == typeof(PersonsTreatedCoverageDrugPackageReportGenerator)))
            {
                AggregateDistData(distReportResult);
            }

            // Run the intervention report
            ReportResult intvReportResult = RunIntvReport(CloneReport(report), standardOpts);
            // Remove the district and year columns from the intervention report result
            RemovePastColumn(intvReportResult.DataTableResults, Translations.Year);
            // Aggregate the Report data
            DataTable intvDataTable = AggregateIntvData(intvReportResult);

            // Merge the results
            CopyDataTableToSameRows(intvDataTable, distReportResult.DataTableResults);

            distReportResult.ChartData = distReportResult.DataTableResults.Copy();
            distReportResult.DataTableResults.Columns.Remove(Translations.Type);
            return distReportResult;
        }
    }

    public class PersonsTreatedCoverageDiseaseReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
        public override List<IntvType> DetermineIntvTypes(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            // Selected disease IDs
            List<int> diseaseIds = standardOpts.Diseases.Select(d => d.Id).ToList();

            // Get all intervention typess that are associated to the selected diseases
            List<IntvType> intvTypes = IntvRepo.GetAllTypesByDiseases(diseaseIds);
            // Get the indicators for each type
            HydrateIntvTypes(intvTypes);

            return intvTypes;
        }

        public override List<Disease> DetermineDiseases(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            return standardOpts.Diseases;
        }
    }

    public class PersonsTreatedCoverageDrugPackageReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
        public override List<IntvType> DetermineIntvTypes(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            List<IntvType> intvTypes = standardOpts.DrugPackages;
            HydrateIntvTypes(intvTypes);
            return intvTypes;
        }

        public override List<Disease> DetermineDiseases(SavedReport report, PersonsTreatedCoverageReportOptions standardOpts)
        {
            // Will hold the diseases
            List<Disease> diseases = new List<Disease>();
            // Get the interventions
            List<IntvBase> intvs = IntvRepo.GetAll(standardOpts.AvailableDrugPackages.Select(i => i.Id).ToList(),
                report.ReportOptions.SelectedAdminLevels.Select(l => l.Id).ToList());
            // Iterate through the interventions and add the targeted diseases to the collection if they are selected
            foreach (IntvBase intv in intvs)
            {
                IndicatorValue indicatorVal = intv.IndicatorValues.Where(ind => ind.Indicator.DisplayName == "PcIntvDiseases").FirstOrDefault();
                if (indicatorVal != null)
                {
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
            }

            return diseases;
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
            int lastIndex = roundColName.LastIndexOf(Translations.Round);
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
        }
    }
}

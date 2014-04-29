using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model.Reports
{
    public class BaseEliminationReport : BaseReportGenerator
    {
        protected void AddIndicators(int id, string name, DiseaseDistroPc dd, ReportOptions options)
        {
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(id,
                               new KeyValuePair<string, Indicator>(name, dd.Indicators[name])));
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
    }

    public class EliminationPersonsReportGenerator : BaseEliminationReport
    {
        public override ReportResult Run(SavedReport report)
        {
            EliminationReportOptions standardOpts = (EliminationReportOptions)report.StandardReportOptions;
            ReportOptions options = report.ReportOptions;
            DistributionReportGenerator gen = new DistributionReportGenerator();
            DiseaseRepository repo = new DiseaseRepository();
            foreach (var disease in standardOpts.Diseases)
            {
                DiseaseDistroPc dd = repo.Create((DiseaseType)disease.Id);

                switch (disease.Id)
                {

                    case (int)DiseaseType.Lf:
                        AddIndicators(disease.Id, "DDLFPopulationAtRisk", dd, options);
                        AddIndicators(disease.Id, "DDLFPopulationLivingInTheDistrictsThatAc", dd, options);
                        break;
                    case (int)DiseaseType.Trachoma:
                        AddIndicators(disease.Id, "DDTraPopulationAtRisk", dd, options);
                        AddIndicators(disease.Id, "DDTraPopulationLivingInAreasDistrict", dd, options);
                        break;
                    case (int)DiseaseType.Oncho:
                        AddIndicators(disease.Id, "DDOnchoPopulationAtRisk", dd, options);
                        AddIndicators(disease.Id, "DDOnchoPopulationLivingInTheDistrictsTha", dd, options);
                        break;
                    case (int)DiseaseType.STH:
                        AddIndicators(disease.Id, "DDSTHPopulationAtRisk", dd, options);
                        AddIndicators(disease.Id, "DDSTHPopulationLivingInTheDistrictsThatA", dd, options);
                        break;
                    case (int)DiseaseType.Schisto:
                        AddIndicators(disease.Id, "DDSchistoPopulationAtRisk", dd, options);
                        AddIndicators(disease.Id, "DDSchistoPopulationLivingInTheDistrictsT", dd, options);
                        break;
                    default:
                        break;
                }
            }

            ReportResult result = gen.Run(report);

            // loop table, make sure column exists (with 0), sum other columns to good column and delete other columns if they have em.
            result.DataTableResults.Columns.Add(new DataColumn(Translations.EliminationAtRisk));
            result.DataTableResults.Columns.Add(new DataColumn(Translations.EliminationLiving));
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                double totalAtRisk = 0, totalLiving = 0;
                totalAtRisk += GetColumnDouble(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults, row);
                totalLiving += GetColumnDouble(Translations.DDLFPopulationLivingInTheDistrictsThatAc + " - " + Translations.LF, result.DataTableResults, row);
                totalLiving += GetColumnDouble(Translations.DDTraPopulationLivingInAreasDistrict + " - " + Translations.Trachoma, result.DataTableResults, row);
                totalLiving += GetColumnDouble(Translations.DDSTHPopulationLivingInTheDistrictsThatA + " - " + Translations.STH, result.DataTableResults, row);
                totalLiving += GetColumnDouble(Translations.DDOnchoPopulationLivingInTheDistrictsTha + " - " + Translations.Oncho, result.DataTableResults, row);
                totalLiving += GetColumnDouble(Translations.DDSchistoPopulationLivingInTheDistrictsT + " - " + Translations.Schisto, result.DataTableResults, row);
                row[Translations.EliminationAtRisk] = totalAtRisk;
                row[Translations.EliminationLiving] = totalLiving;
            }

            TryRemoveColumn(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults);
            TryRemoveColumn(Translations.DDLFPopulationLivingInTheDistrictsThatAc + " - " + Translations.LF, result.DataTableResults);
            TryRemoveColumn(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults);
            TryRemoveColumn(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults);
            TryRemoveColumn(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults);
            TryRemoveColumn(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults);
            TryRemoveColumn(Translations.DDTraPopulationLivingInAreasDistrict + " - " + Translations.Trachoma, result.DataTableResults);
            TryRemoveColumn(Translations.DDSTHPopulationLivingInTheDistrictsThatA + " - " + Translations.STH, result.DataTableResults);
            TryRemoveColumn(Translations.DDOnchoPopulationLivingInTheDistrictsTha + " - " + Translations.Oncho, result.DataTableResults);
            TryRemoveColumn(Translations.DDSchistoPopulationLivingInTheDistrictsT + " - " + Translations.Schisto, result.DataTableResults);

            result.ChartData = result.DataTableResults.Copy();
            result.DataTableResults.Columns.Remove(Translations.Type);
            if (options.IsCountryAggregation)
                result.DataTableResults.Columns.RemoveAt(0);
            return result;
        }

    }

    public class EliminationDistrictsReportGenerator : BaseEliminationReport
    {
        public override ReportResult Run(SavedReport report)
        {
            EliminationReportOptions standardOpts = (EliminationReportOptions)report.StandardReportOptions;
            ReportOptions options = report.ReportOptions;
            DistributionReportGenerator gen = new DistributionReportGenerator();
            DiseaseRepository repo = new DiseaseRepository();
            DemoRepository demo = new DemoRepository();
            foreach (var disease in standardOpts.Diseases)
            {
                DiseaseDistroPc dd = repo.Create((DiseaseType)disease.Id);

                switch (disease.Id)
                {
                    case (int)DiseaseType.Lf:
                        AddIndicators(disease.Id, "DDLFDiseaseDistributionPcInterventions", dd, options);
                        break;
                    case (int)DiseaseType.Trachoma:
                        AddIndicators(disease.Id, "DDTraDiseaseDistributionPcInterventions", dd, options);
                        break;
                    case (int)DiseaseType.Oncho:
                        AddIndicators(disease.Id, "DDOnchoDiseaseDistributionPcInterventio", dd, options);
                        break;
                    case (int)DiseaseType.STH:
                        AddIndicators(disease.Id, "DDSTHDiseaseDistributionPcInterventions", dd, options);
                        break;
                    case (int)DiseaseType.Schisto:
                        AddIndicators(disease.Id, "DDSchistoDiseaseDistributionPcIntervent", dd, options);
                        break;
                    default:
                        break;
                }
            }

            // need to add the proper sumation stuff
            report.ReportOptions.IsByLevelAggregation = true;
            report.ReportOptions.IsCountryAggregation = false;
            report.ReportOptions.IsNoAggregation = false;
            report.ReportOptions.SelectedAdminLevels = demo.GetAdminLevelByLevel(standardOpts.DistrictType.LevelNumber).Where(a => a.LevelNumber == standardOpts.DistrictType.LevelNumber).ToList();

            // run report
            ReportResult result = gen.Run(report);

            Dictionary<string, DataRow> years = new Dictionary<string,DataRow>();
            DataTable summed = new DataTable();
            summed.Columns.Add(new DataColumn(Translations.Location));
            summed.Columns.Add(new DataColumn(Translations.Year, typeof(string)));
            summed.Columns.Add(new DataColumn(string.Format(Translations.EliminationEndemicDistricts, standardOpts.DistrictType.DisplayName), typeof(int)));
            summed.Columns.Add(new DataColumn(string.Format(Translations.EliminationStoppedDistricts, standardOpts.DistrictType.DisplayName), typeof(int)));
            
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                string endemicty = "";
                int totalEndemic = 0, totalStopped = 0;
                if (result.DataTableResults.Columns.Contains(Translations.DDLFDiseaseDistributionPcInterventions + " - " + Translations.LF))
                {
                    endemicty = row[Translations.DDLFDiseaseDistributionPcInterventions + " - " + Translations.LF].ToString();
                    if (endemicty == Translations.LfEnd1 || endemicty == Translations.LfEndPending)
                        totalEndemic++;
                    else if (endemicty == Translations.LfEnd100)
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(Translations.DDOnchoDiseaseDistributionPcInterventio + " - " + Translations.Oncho))
                {
                    endemicty = row[Translations.DDOnchoDiseaseDistributionPcInterventio + " - " + Translations.Oncho].ToString();
                    if (endemicty == Translations.Oncho1 || endemicty == Translations.OnchoPending)
                        totalEndemic++;
                    else if (endemicty == Translations.Oncho100)
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(Translations.DDSchistoDiseaseDistributionPcIntervent + " - " + Translations.Schisto))
                {
                    endemicty = row[Translations.DDSchistoDiseaseDistributionPcIntervent + " - " + Translations.Schisto].ToString();
                    if (endemicty == Translations.Sch1 || endemicty == Translations.Sch2 || endemicty == Translations.Sch2a || endemicty == Translations.Sch3 || endemicty == Translations.Sch3a
                        || endemicty == Translations.Sch3b || endemicty == Translations.Sch20 || endemicty == Translations.Sch30 || endemicty == Translations.Sch40
                        || endemicty == Translations.SchPending)
                        totalEndemic++;
                    else if (endemicty == Translations.Sch100)
                        totalStopped++;
                }

                if (result.DataTableResults.Columns.Contains(Translations.DDSTHDiseaseDistributionPcInterventions + " - " + Translations.STH))
                {
                    endemicty = row[Translations.DDSTHDiseaseDistributionPcInterventions + " - " + Translations.STH].ToString();
                    if (endemicty == Translations.Sth2 || endemicty == Translations.Sth3 || endemicty == Translations.Sth10 || endemicty == Translations.Sth20
                        || endemicty == Translations.Sth30 || endemicty == Translations.Sth40 || endemicty == Translations.SthPending)
                        totalEndemic++;
                    else if (endemicty == Translations.Sth100)
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(Translations.DDTraDiseaseDistributionPcInterventions + " - " + Translations.Trachoma))
                {
                    endemicty = row[Translations.DDTraDiseaseDistributionPcInterventions + " - " + Translations.Trachoma].ToString();
                    if (endemicty == Translations.Tra1 || endemicty == Translations.Tra4 || endemicty == Translations.Tra5
                        || endemicty == Translations.TraPending)
                        totalEndemic++;
                    else if (endemicty == Translations.Tra100)
                        totalStopped++;
                }

                if (!years.ContainsKey(row[Translations.Year].ToString()))
                {
                    DataRow dr = summed.NewRow();
                    dr[Translations.Year] = row[Translations.Year];
                    dr[string.Format(Translations.EliminationEndemicDistricts, standardOpts.DistrictType.DisplayName)] = totalEndemic;
                    dr[string.Format(Translations.EliminationStoppedDistricts, standardOpts.DistrictType.DisplayName)] = totalStopped;
                    years.Add(row[Translations.Year].ToString(), dr);
                    summed.Rows.Add(dr);
                }
                else
                {
                    years[row[Translations.Year].ToString()][string.Format(Translations.EliminationEndemicDistricts, standardOpts.DistrictType.DisplayName)] =
                        totalEndemic + (int)years[row[Translations.Year].ToString()][string.Format(Translations.EliminationEndemicDistricts, standardOpts.DistrictType.DisplayName)];
                    years[row[Translations.Year].ToString()][string.Format(Translations.EliminationStoppedDistricts, standardOpts.DistrictType.DisplayName)] =
                        totalStopped + (int)years[row[Translations.Year].ToString()][string.Format(Translations.EliminationStoppedDistricts, standardOpts.DistrictType.DisplayName)];
                }
            }

            result.DataTableResults = summed;
            result.ChartData = summed.Copy();
            report.ReportOptions.IsByLevelAggregation = false;
            report.ReportOptions.IsCountryAggregation = true; // to show only year

            return result;
        }
    }
}

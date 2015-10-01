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
            result.DataTableResults.Columns.Add(new DataColumn(TranslationLookup.GetValue("EliminationAtRisk")));
            result.DataTableResults.Columns.Add(new DataColumn(TranslationLookup.GetValue("EliminationLiving")));
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                double totalAtRisk = 0, totalLiving = 0;
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDLFPopulationAtRisk") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDTraPopulationAtRisk") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDOnchoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDSTHPopulationAtRisk") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(TranslationLookup.GetValue("DDSchistoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults, row);
                totalLiving += GetColumnDouble(TranslationLookup.GetValue("DDLFPopulationLivingInTheDistrictsThatAc") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults, row);
                totalLiving += GetColumnDouble(TranslationLookup.GetValue("DDTraPopulationLivingInAreasDistrict") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults, row);
                totalLiving += GetColumnDouble(TranslationLookup.GetValue("DDSTHPopulationLivingInTheDistrictsThatA") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults, row);
                totalLiving += GetColumnDouble(TranslationLookup.GetValue("DDOnchoPopulationLivingInTheDistrictsTha") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults, row);
                totalLiving += GetColumnDouble(TranslationLookup.GetValue("DDSchistoPopulationLivingInTheDistrictsT") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults, row);
                row[TranslationLookup.GetValue("EliminationAtRisk")] = totalAtRisk;
                row[TranslationLookup.GetValue("EliminationLiving")] = totalLiving;
            }

            TryRemoveColumn(TranslationLookup.GetValue("DDLFPopulationAtRisk") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDLFPopulationLivingInTheDistrictsThatAc") + " - " + TranslationLookup.GetValue("LF"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDTraPopulationAtRisk") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDOnchoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDSTHPopulationAtRisk") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDSchistoPopulationAtRisk") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDTraPopulationLivingInAreasDistrict") + " - " + TranslationLookup.GetValue("Trachoma"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDSTHPopulationLivingInTheDistrictsThatA") + " - " + TranslationLookup.GetValue("STH"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDOnchoPopulationLivingInTheDistrictsTha") + " - " + TranslationLookup.GetValue("Oncho"), result.DataTableResults);
            TryRemoveColumn(TranslationLookup.GetValue("DDSchistoPopulationLivingInTheDistrictsT") + " - " + TranslationLookup.GetValue("Schisto"), result.DataTableResults);

            result.ChartData = result.DataTableResults.Copy();
            result.DataTableResults.Columns.Remove(TranslationLookup.GetValue("Type"));
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
            summed.Columns.Add(new DataColumn(TranslationLookup.GetValue("Location")));
            summed.Columns.Add(new DataColumn(TranslationLookup.GetValue("Year"), typeof(string)));
            summed.Columns.Add(new DataColumn(string.Format(TranslationLookup.GetValue("EliminationEndemicDistricts"), standardOpts.DistrictType.DisplayName), typeof(int)));
            summed.Columns.Add(new DataColumn(string.Format(TranslationLookup.GetValue("EliminationStoppedDistricts"), standardOpts.DistrictType.DisplayName), typeof(int)));
            
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                string endemicty = "";
                int totalEndemic = 0, totalStopped = 0;
                if (result.DataTableResults.Columns.Contains(TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("LF")))
                {
                    endemicty = row[TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("LF")].ToString();
                    if (endemicty == TranslationLookup.GetValue("LfEnd1") || endemicty == TranslationLookup.GetValue("LfEndPending"))
                        totalEndemic++;
                    else if (endemicty == TranslationLookup.GetValue("LfEnd100"))
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + TranslationLookup.GetValue("Oncho")))
                {
                    endemicty = row[TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + TranslationLookup.GetValue("Oncho")].ToString();
                    if (endemicty == TranslationLookup.GetValue("Oncho1") || endemicty == TranslationLookup.GetValue("OnchoPending"))
                        totalEndemic++;
                    else if (endemicty == TranslationLookup.GetValue("Oncho100"))
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + TranslationLookup.GetValue("Schisto")))
                {
                    endemicty = row[TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + TranslationLookup.GetValue("Schisto")].ToString();
                    if (endemicty == TranslationLookup.GetValue("Sch1") || endemicty == TranslationLookup.GetValue("Sch2") || endemicty == TranslationLookup.GetValue("Sch2a") || endemicty == TranslationLookup.GetValue("Sch3") || endemicty == TranslationLookup.GetValue("Sch3a")
                        || endemicty == TranslationLookup.GetValue("Sch3b") || endemicty == TranslationLookup.GetValue("Sch20") || endemicty == TranslationLookup.GetValue("Sch30") || endemicty == TranslationLookup.GetValue("Sch40")
                        || endemicty == TranslationLookup.GetValue("SchPending"))
                        totalEndemic++;
                    else if (endemicty == TranslationLookup.GetValue("Sch100"))
                        totalStopped++;
                }

                if (result.DataTableResults.Columns.Contains(TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("STH")))
                {
                    endemicty = row[TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("STH")].ToString();
                    if (endemicty == TranslationLookup.GetValue("Sth2") || endemicty == TranslationLookup.GetValue("Sth3") || endemicty == TranslationLookup.GetValue("Sth10") || endemicty == TranslationLookup.GetValue("Sth20")
                        || endemicty == TranslationLookup.GetValue("Sth30") || endemicty == TranslationLookup.GetValue("Sth40") || endemicty == TranslationLookup.GetValue("SthPending"))
                        totalEndemic++;
                    else if (endemicty == TranslationLookup.GetValue("Sth100"))
                        totalStopped++;
                }
                if (result.DataTableResults.Columns.Contains(TranslationLookup.GetValue("DDTraDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("Trachoma")))
                {
                    endemicty = row[TranslationLookup.GetValue("DDTraDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("Trachoma")].ToString();
                    if (endemicty == TranslationLookup.GetValue("Tra1") || endemicty == TranslationLookup.GetValue("Tra4") || endemicty == TranslationLookup.GetValue("Tra5")
                        || endemicty == TranslationLookup.GetValue("TraPending"))
                        totalEndemic++;
                    else if (endemicty == TranslationLookup.GetValue("Tra100"))
                        totalStopped++;
                }

                if (!years.ContainsKey(row[TranslationLookup.GetValue("Year")].ToString()))
                {
                    DataRow dr = summed.NewRow();
                    dr[TranslationLookup.GetValue("Year")] = row[TranslationLookup.GetValue("Year")];
                    dr[string.Format(TranslationLookup.GetValue("EliminationEndemicDistricts"), standardOpts.DistrictType.DisplayName)] = totalEndemic;
                    dr[string.Format(TranslationLookup.GetValue("EliminationStoppedDistricts"), standardOpts.DistrictType.DisplayName)] = totalStopped;
                    years.Add(row[TranslationLookup.GetValue("Year")].ToString(), dr);
                    summed.Rows.Add(dr);
                }
                else
                {
                    years[row[TranslationLookup.GetValue("Year")].ToString()][string.Format(TranslationLookup.GetValue("EliminationEndemicDistricts"), standardOpts.DistrictType.DisplayName)] =
                        totalEndemic + (int)years[row[TranslationLookup.GetValue("Year")].ToString()][string.Format(TranslationLookup.GetValue("EliminationEndemicDistricts"), standardOpts.DistrictType.DisplayName)];
                    years[row[TranslationLookup.GetValue("Year")].ToString()][string.Format(TranslationLookup.GetValue("EliminationStoppedDistricts"), standardOpts.DistrictType.DisplayName)] =
                        totalStopped + (int)years[row[TranslationLookup.GetValue("Year")].ToString()][string.Format(TranslationLookup.GetValue("EliminationStoppedDistricts"), standardOpts.DistrictType.DisplayName)];
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

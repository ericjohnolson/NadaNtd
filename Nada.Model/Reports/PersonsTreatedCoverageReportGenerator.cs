using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nada.Model.Reports
{
    public class PersonsTreatedCoverageBaseReportGenerator : BaseReportGenerator
    {
        protected void AddIndicators(int id, string name, DiseaseDistroPc dd, ReportOptions options)
        {
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(id,
                               new KeyValuePair<string, Indicator>(name, dd.Indicators[name])));
        }

        protected void AddIndicators(int id, string name, IHaveDynamicIndicatorValues entity, ReportOptions options)
        {
            IndicatorValue indicatorVal = entity.IndicatorValues.Where(ind => ind.Indicator.DisplayName == name).FirstOrDefault();
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(id,
                               new KeyValuePair<string, Indicator>(name, indicatorVal.Indicator)));
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

    public class PersonsTreatedCoverageDiseaseReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
        public override ReportResult Run(SavedReport report)
        {
            PersonsTreatedCoverageReportOptions standardOpts = (PersonsTreatedCoverageReportOptions)report.StandardReportOptions;
            ReportOptions options = report.ReportOptions;
            DistributionReportGenerator gen = new DistributionReportGenerator();
            // Repos
            DiseaseRepository diseaseRepo = new DiseaseRepository();
            DemoRepository demoRepository = new DemoRepository();

            // Options
            report.ReportOptions.IsByLevelAggregation = true;
            report.ReportOptions.IsCountryAggregation = false;
            report.ReportOptions.IsNoAggregation = false;

            // Get all admin levels
            report.ReportOptions.SelectedAdminLevels = demoRepository.GetAdminLevelByLevel(standardOpts.DistrictType.LevelNumber).Where(a => a.LevelNumber == standardOpts.DistrictType.LevelNumber).ToList();
            
            // Add disease related information
            foreach (var disease in standardOpts.Diseases)
            {
                DiseaseDistroPc dd = diseaseRepo.Create((DiseaseType)disease.Id);

                switch (disease.Id)
                {

                    case (int)DiseaseType.Lf:
                        AddIndicators(disease.Id, "DDLFPopulationAtRisk", dd, options);
                        //AddIndicators(disease.Id, "DDLFPopulationLivingInTheDistrictsThatAc", dd, options);
                        break;
                    case (int)DiseaseType.Trachoma:
                        AddIndicators(disease.Id, "DDTraPopulationAtRisk", dd, options);
                        //AddIndicators(disease.Id, "DDTraPopulationLivingInAreasDistrict", dd, options);
                        break;
                    case (int)DiseaseType.Oncho:
                        AddIndicators(disease.Id, "DDOnchoPopulationAtRisk", dd, options);
                        //AddIndicators(disease.Id, "DDOnchoPopulationLivingInTheDistrictsTha", dd, options);
                        break;
                    case (int)DiseaseType.STH:
                        AddIndicators(disease.Id, "DDSTHPopulationAtRisk", dd, options);
                        //AddIndicators(disease.Id, "DDSTHPopulationLivingInTheDistrictsThatA", dd, options);
                        break;
                    case (int)DiseaseType.Schisto:
                        AddIndicators(disease.Id, "DDSchistoPopulationAtRisk", dd, options);
                        //AddIndicators(disease.Id, "DDSchistoPopulationLivingInTheDistrictsT", dd, options);
                        break;
                    default:
                        break;
                }
            }

            ReportResult result = gen.Run(report);

            // loop table, make sure column exists (with 0), sum other columns to good column and delete other columns if they have em.
            result.DataTableResults.Columns.Add(new DataColumn(Translations.EliminationAtRisk));
            //result.DataTableResults.Columns.Add(new DataColumn(Translations.EliminationLiving));
            foreach (DataRow row in result.DataTableResults.Rows)
            {
                double totalAtRisk = 0/*, totalLiving = 0*/;
                totalAtRisk += GetColumnDouble(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults, row);
                totalAtRisk += GetColumnDouble(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults, row);
                //totalLiving += GetColumnDouble(Translations.DDLFPopulationLivingInTheDistrictsThatAc + " - " + Translations.LF, result.DataTableResults, row);
                //totalLiving += GetColumnDouble(Translations.DDTraPopulationLivingInAreasDistrict + " - " + Translations.Trachoma, result.DataTableResults, row);
                //totalLiving += GetColumnDouble(Translations.DDSTHPopulationLivingInTheDistrictsThatA + " - " + Translations.STH, result.DataTableResults, row);
                //totalLiving += GetColumnDouble(Translations.DDOnchoPopulationLivingInTheDistrictsTha + " - " + Translations.Oncho, result.DataTableResults, row);
                //totalLiving += GetColumnDouble(Translations.DDSchistoPopulationLivingInTheDistrictsT + " - " + Translations.Schisto, result.DataTableResults, row);
                row[Translations.EliminationAtRisk] = totalAtRisk;
                //row[Translations.EliminationLiving] = totalLiving;
            }

            TryRemoveColumn(Translations.DDLFPopulationAtRisk + " - " + Translations.LF, result.DataTableResults);
            //TryRemoveColumn(Translations.DDLFPopulationLivingInTheDistrictsThatAc + " - " + Translations.LF, result.DataTableResults);
            TryRemoveColumn(Translations.DDTraPopulationAtRisk + " - " + Translations.Trachoma, result.DataTableResults);
            TryRemoveColumn(Translations.DDOnchoPopulationAtRisk + " - " + Translations.Oncho, result.DataTableResults);
            TryRemoveColumn(Translations.DDSTHPopulationAtRisk + " - " + Translations.STH, result.DataTableResults);
            TryRemoveColumn(Translations.DDSchistoPopulationAtRisk + " - " + Translations.Schisto, result.DataTableResults);
            //TryRemoveColumn(Translations.DDTraPopulationLivingInAreasDistrict + " - " + Translations.Trachoma, result.DataTableResults);
            //TryRemoveColumn(Translations.DDSTHPopulationLivingInTheDistrictsThatA + " - " + Translations.STH, result.DataTableResults);
            //TryRemoveColumn(Translations.DDOnchoPopulationLivingInTheDistrictsTha + " - " + Translations.Oncho, result.DataTableResults);
            //TryRemoveColumn(Translations.DDSchistoPopulationLivingInTheDistrictsT + " - " + Translations.Schisto, result.DataTableResults);


            // Add intervention related data
            IntvRepository intvRepo = new IntvRepository();

            // Selected disease IDs
            List<int> diseaseIds = standardOpts.Diseases.Select(d => d.Id).ToList();

            // Get all interventions that are associated to the selected diseases
            List<IntvType> intvTypes = intvRepo.GetAllTypesByDiseases(diseaseIds);
            List<IntvBase> intvs = intvRepo.GetAll(intvTypes.Select(i => i.Id).ToList(),
                report.ReportOptions.SelectedAdminLevels.Select(l => l.Id).ToList());

            // Get the disease DisplayName keys so that the values can be filtered
            List<string> diseaseKeys = diseaseRepo.GetSelectedDiseases().Where(d => diseaseIds.Contains(d.Id)).Select(d => d.DisplayNameKey).ToList();

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

            }

            result.ChartData = result.DataTableResults.Copy();
            result.DataTableResults.Columns.Remove(Translations.Type);
            return result;
        }
    }

    public class PersonsTreatedCoverageDrugPackageReportGenerator : PersonsTreatedCoverageBaseReportGenerator
    {
    }
}

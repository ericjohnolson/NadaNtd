using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nada.Model.Diseases
{
    public class RecentDistro
    {
        private SavedReport Report;
        private ReportResult Result;
        private Dictionary<int, Disease> Diseases;

        private DiseaseRepository DiseaseRepo;

        private string NameOfReportingAdminLevel;

        private static RecentDistro Instance;

        private RecentDistro()
        {
            DiseaseRepo = new DiseaseRepository();
            Diseases = new Dictionary<int, Disease>();
        }

        public static RecentDistro GetInstance(bool instantiate)
        {
            if (Instance == null && instantiate)
            {
                Instance = new RecentDistro();
            }
            return Instance;
        }

        public static void ClearInstance()
        {
            Instance = null;
        }

        public void Run(ReportOptions mainReportOptions)
        {
            SettingsRepository settingsRepo = new SettingsRepository();

            Report = new SavedReport();
            DistributionReportGenerator gen = new DistributionReportGenerator();

            Report.ReportOptions = new ReportOptions();
            Report.ReportOptions.SelectedIndicators = new List<ReportIndicator>();

            Report.ReportOptions.Years = Util.DeepClone(mainReportOptions.Years);
            Report.ReportOptions.SelectedAdminLevels = Util.DeepClone(mainReportOptions.SelectedAdminLevels);
            Report.ReportOptions.MonthYearStarts = 1;
            Report.ReportOptions.IsByLevelAggregation = true;
            Report.ReportOptions.IsCountryAggregation = false;
            Report.ReportOptions.IsNoAggregation = false;

            // Determine the name of the reporting admin level
            if (mainReportOptions.SelectedAdminLevels.Count > 0)
            {
                int levelNum = mainReportOptions.SelectedAdminLevels[0].LevelNumber;
                AdminLevelType adminLevelType = settingsRepo.GetAdminLevelTypeByLevel(levelNum);
                NameOfReportingAdminLevel = adminLevelType.DisplayName;
            }
            if (NameOfReportingAdminLevel == null)
                throw new ArgumentException("Could not determine reporting level");

            // STH
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.STH,
                        new KeyValuePair<string, Indicator>("DDSTHPopulationRequiringPc", new Indicator { Id = 141, DisplayName = "DDSTHPopulationRequiringPc" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.STH,
                        new KeyValuePair<string, Indicator>("DDSTHPsacAtRisk", new Indicator { Id = 142, DisplayName = "DDSTHPsacAtRisk" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.STH,
                        new KeyValuePair<string, Indicator>("DDSTHSacAtRisk", new Indicator { Id = 143, DisplayName = "DDSTHSacAtRisk" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.STH,
                        new KeyValuePair<string, Indicator>("DDSTHPopulationAtRisk", new Indicator { Id = 140, DisplayName = "DDSTHPopulationAtRisk" })));
            // LF
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Lf,
                    new KeyValuePair<string, Indicator>("DDLFPopulationAtRisk", new Indicator { Id = 98, DisplayName = "DDLFPopulationAtRisk" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Lf,
                        new KeyValuePair<string, Indicator>("DDLFPopulationRequiringPc", new Indicator { Id = 99, DisplayName = "DDLFPopulationRequiringPc" })));
            // Oncho
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Oncho,
                    new KeyValuePair<string, Indicator>("DDOnchoPopulationAtRisk", new Indicator { Id = 111, DisplayName = "DDOnchoPopulationAtRisk" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Oncho,
                        new KeyValuePair<string, Indicator>("DDOnchoPopulationRequiringPc", new Indicator { Id = 112, DisplayName = "DDOnchoPopulationRequiringPc" })));
            // Schisto
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Schisto,
                        new KeyValuePair<string, Indicator>("DDSchistoPopulationAtRisk", new Indicator { Id = 125, DisplayName = "DDSchistoPopulationAtRisk" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Schisto,
                        new KeyValuePair<string, Indicator>("DDSchistoPopulationRequiringPc", new Indicator { Id = 126, DisplayName = "DDSchistoPopulationRequiringPc" })));
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Schisto,
                        new KeyValuePair<string, Indicator>("DDSchistoSacAtRisk", new Indicator { Id = 127, DisplayName = "DDSchistoSacAtRisk" })));
            // Trachoma
            Report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)DiseaseType.Trachoma,
                    new KeyValuePair<string, Indicator>("DDTraPopulationAtRisk", new Indicator { Id = 161, DisplayName = "DDTraPopulationAtRisk" })));

            // Run the report
            Result = gen.Run(Report);
        }

        public string GetRecentDistroIndicator(int adminLevelId, string indicatorName, DiseaseType diseaseType, DateTime start, DateTime end, ref string errors)
        {
            // Get the corresponding disease
            Disease disease = null;
            if (!Diseases.ContainsKey((int)diseaseType))
            {
                disease = DiseaseRepo.GetDiseaseById((int)diseaseType);
                Diseases.Add((int)diseaseType, disease);
            }
            else
            {
                disease = Diseases[(int)diseaseType];
            }
            if (disease == null)
                return "";

            string colName = string.Format("{0} - {1}", TranslationLookup.GetValue(indicatorName), disease.DisplayName);
            // Make sure the column exists
            if (!Result.DataTableResults.Columns.Contains(colName))
                return "";

            // Get the corresponding AdminLevel
            AdminLevel adminLevel = Report.ReportOptions.SelectedAdminLevels.Where(a => a.Id == adminLevelId).FirstOrDefault();

            if (adminLevel == null)
                return "";

            // Find the corresponding row
            foreach (DataRow row in Result.DataTableResults.Rows)
            {
                // Get the row's admin level name and date range
                string rowAdminLevelName = row[NameOfReportingAdminLevel].ToString();
                string rowDateRange = row[Translations.Year].ToString();

                // Determine the date range that was passed in
                string requestedDateRange = string.Format("{0}-{1}", start.ToString("MMM yyyy"), end.ToString("MMM yyyy"));

                // The row matches if the admin level and date match
                if (rowAdminLevelName != adminLevel.Name || rowDateRange != requestedDateRange)
                    continue;
                else
                {
                    return row[colName].ToString();
                }
            }

            return "";
        }
    }
}

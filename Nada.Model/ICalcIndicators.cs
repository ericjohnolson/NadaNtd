using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Reports;
using Nada.Model.Repositories;

namespace Nada.Model
{
    /// <summary>
    /// Interface defining contract for calculation classes
    /// </summary>
    public interface ICalcIndicators
    {
        /// <summary>
        /// Calculates any indicators that belong in the meta data
        /// </summary>
        /// <param name="fields">The meta data collection of fields</param>
        /// <param name="adminLevel">The admin unit to calculate for</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of meta data calculations</returns>
        List<KeyValuePair<string, string>> GetMetaData(List<KeyValuePair<string, string>> fields, int adminLevel, DateTime start, DateTime end);

        /// <summary>
        /// Main method that begins the calculation process
        /// </summary>
        /// <param name="indicators">Indicators</param>
        /// <param name="indicatorValues">Values for the indicators</param>
        /// <param name="adminLevel">The admin unit to calculate for</param>
        /// <param name="typeId"></param>
        /// <param name="formTranslationKey">The form translation key</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of calculation results</returns>
        List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId, string formTranslationKey, DateTime start, DateTime end);

        /// <summary>
        /// Calculates each of the specified indicators
        /// </summary>
        /// <param name="fields">Indicators to calculate</param>
        /// <param name="relatedValues">Related indicator values used in the calculation</param>
        /// <param name="adminLevel">Admin unit to calculate for</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of calculation results</returns>
        List<KeyValuePair<string, string>> GetCalculatedValues(List<KeyValuePair<string, string>> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end);

        /// <summary>
        /// Calculates a single indicator
        /// </summary>
        /// <param name="formTranslationKey">The form translation key</param>
        /// <param name="field">Indicator being calculated</param>
        /// <param name="relatedValues">Related indicator values used in the calculation</param>
        /// <param name="demo">Demography data for the admin unit</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <param name="errors">Errors to be displayed and managed by the ReportGenerator</param>
        /// <returns>The indicator name and the calculated value</returns>
        KeyValuePair<string, string> GetCalculatedValue(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors);
        AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime start, DateTime end);
    }

    public class CalcBase : ICalcIndicators
    {
        protected DemoRepository demoRepo = new DemoRepository();

        /// <summary>
        /// Gets admin unit demography information for the specified admin unit between the two dates
        /// </summary>
        /// <param name="adminLevelId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime start, DateTime end)
        {
            return GetDemography(adminLevelId, start, end);
        }

        public virtual List<KeyValuePair<string, string>> GetMetaData(List<KeyValuePair<string, string>> fields, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Main method that begins the calculation process
        /// </summary>
        /// <param name="indicators">Indicators</param>
        /// <param name="indicatorValues">Values for the indicators</param>
        /// <param name="adminLevel">The admin unit to calculate for</param>
        /// <param name="typeId"></param>
        /// <param name="formTranslationKey">The form translation key</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of calculation results</returns>
        public virtual List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId, string formTranslationKey,  DateTime start, DateTime end)
        {
            var calculations = indicators.Values.Where(i => i.IsCalculated);
            if (calculations.Count() == 0)
                return new List<KeyValuePair<string, string>>();

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var val in indicatorValues)
                values.Add(formTranslationKey + val.Indicator.DisplayName, val.DynamicValue);

            return GetCalculatedValues(
                calculations.Select(i => new KeyValuePair<string, string>(formTranslationKey, i.DisplayName)).ToList(),
                values,
                adminLevel, start, end);
        }

        public virtual List<KeyValuePair<string, string>> GetCalculatedValues(List<KeyValuePair<string, string>> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public virtual KeyValuePair<string, string> GetCalculatedValue(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            return new KeyValuePair<string, string>();
        }

        public string GetPercentage(Dictionary<string, IndicatorValue> indicators, string numerator, string denominator)
        {
            double n, d;
            if (indicators.ContainsKey(numerator) && indicators.ContainsKey(denominator) &&
                double.TryParse(indicators[numerator].DynamicValue, out n) &&
                double.TryParse(indicators[denominator].DynamicValue, out d) && d != 0)
                return string.Format("{0:0.00}", n / d * 100);

            return Translations.NA;
        }

        public static string GetPercentage(string numerator, string denominator)
        {
            return GetPercentage(numerator, denominator, 100);
        }

        public static string GetPercentage(string numerator, string denominator, int multipler)
        {
            double n, d;
            if (!string.IsNullOrEmpty(numerator) && !string.IsNullOrEmpty(denominator) &&
                double.TryParse(numerator, out n) &&
                double.TryParse(denominator, out d) && d != 0)
                return string.Format("{0:0.00}", n / d * multipler);

            return Translations.NA;
        }

        public static string GetPercentageWithRatio(string numerator, string denominator)
        {
            StringBuilder builder = new StringBuilder(GetPercentage(numerator, denominator));
            builder.AppendFormat(" ({0}/{1})", numerator, denominator);
            return builder.ToString();
        }

        public string GetTotal(params string[] itemsToSum)
        {
            double total = 0;
            double parsed = 0;
            foreach (string v in itemsToSum)
                if (double.TryParse(v, out parsed))
                    total += parsed;

            return string.Format("{0:0.00}", total);
        }

        public string GetDifference(string val1, string val2)
        {
            double v1, v2;
            if (double.TryParse(val1, out v1) && double.TryParse(val2, out v2))
                return string.Format("{0:0.00}", v1 - v2);
            return Translations.NA;
        }

        public string GetDifferences(params string[] itemsToSubtract)
        {
            double total = 0;
            double parsed = 0;
            foreach (string v in itemsToSubtract)
                if (double.TryParse(v, out parsed))
                    total -= parsed;

            return string.Format("{0:0.00}", total);
        }

        public string GetValueOrDefault(string key, Dictionary<string, string> values)
        {
            if (values != null && values.ContainsKey(key))
                return values[key];
            return "";
        }

        #region Related Recent Objects
        protected DiseaseRepository drepo = new DiseaseRepository();
        protected AdminLevelDemography GetDemography(int adminLevelId, DateTime? start, DateTime? end)
        {
            return demoRepo.GetRecentDemography(adminLevelId, start, end);
        }

        protected Dictionary<string, AdminLevelIndicators> distroDict = new Dictionary<string, AdminLevelIndicators>();

        protected string GetRecentDistroIndicator(int adminLevelId, string indicatorName, DiseaseType diseaseType, DateTime start, DateTime end, ref string errors)
        {
            // If there is a RecentDistro instance, look for the value there
            RecentDistro recentDistro = RecentDistro.GetInstance(false /* don't instantiate */);
            if (recentDistro != null)
            {
                string storedVal = recentDistro.GetRecentDistroIndicator(adminLevelId, indicatorName, diseaseType, start, end, ref errors);
                if (storedVal != null)
                {
                    return storedVal;
                }
            }

            AdminLevelIndicators levelInds = null;
            string key = adminLevelId + start.ToShortDateString() + end.ToShortDateString() + diseaseType.ToString();
            if (distroDict.ContainsKey(key))
                levelInds = distroDict[key];
            else
            {
                ReportOptions options = new ReportOptions();
                DistributionReportGenerator gen = new DistributionReportGenerator();
                DiseaseRepository repo = new DiseaseRepository();
                DemoRepository demo = new DemoRepository();
                var disease = repo.GetDiseaseById((int)diseaseType);
                DiseaseDistroPc dd = repo.Create(diseaseType);
                if (diseaseType == DiseaseType.STH)
                {
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 141, DisplayName = "DDSTHPopulationRequiringPc" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 142, DisplayName = "DDSTHPsacAtRisk" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 143, DisplayName = "DDSTHSacAtRisk" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 140, DisplayName = "DDSTHPopulationAtRisk" })));
                }
                else if (diseaseType == DiseaseType.Lf)
                {
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                    new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 98, DisplayName = "DDLFPopulationAtRisk" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 99, DisplayName = "DDLFPopulationRequiringPc" })));
                }
                else if (diseaseType == DiseaseType.Oncho)
                {
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                    new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 111, DisplayName = "DDOnchoPopulationAtRisk" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 112, DisplayName = "DDOnchoPopulationRequiringPc" })));
                }
                else if (diseaseType == DiseaseType.Schisto)
                {
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 125, DisplayName = "DDSchistoPopulationAtRisk" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 126, DisplayName = "DDSchistoPopulationRequiringPc" })));
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                        new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 127, DisplayName = "DDSchistoSacAtRisk" })));
                }
                else if (diseaseType == DiseaseType.Trachoma)
                options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
                    new KeyValuePair<string, Indicator>(indicatorName, new Indicator { Id = 161, DisplayName = "DDTraPopulationAtRisk" })));

                options.StartDate = start;
                options.EndDate = end;
                options.MonthYearStarts = start.Month;
                var adminlevel = demo.GetAdminLevelById(adminLevelId);
                options.SelectedAdminLevels = new List<AdminLevel> { adminlevel };
                options.IsNoAggregation = false;
                options.IsByLevelAggregation = true;
                options.IsAllLocations = false;

                levelInds = gen.GetRecentDiseaseDistribution(options);
                levelInds.StartDate = start;
                levelInds.EndDate = end;
                levelInds.DiseaseName = disease.DisplayName;
            }
            if (levelInds.Indicators.ContainsKey(indicatorName) )
            {
                if(!string.IsNullOrEmpty(levelInds.Indicators[indicatorName].Value))
                    return levelInds.Indicators[indicatorName].Value.ToString();
                else
                    return "";
            }
            string error = string.Format(Translations.ReportsNoDdInDateRange, levelInds.Name, start.ToShortDateString(), end.ToShortDateString(), levelInds.DiseaseName) + Environment.NewLine;
            if (!errors.Contains(error))
                errors += error;
            return Translations.NA;
        }

        protected string GetIndicatorValue(DiseaseDistroPc dd, string indicatorName)
        {
            var indicator = dd.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == indicatorName);
            if (indicator == null)
                return Translations.NA;
            else
                return indicator.DynamicValue;
        }
        //protected string GetLfDd(int adminLevelId, string indicatorName, DateTime start, DateTime end, ref string errors)
        //{
        //    var lfdd = drepo.GetRecentDistro((int)DiseaseType.Lf, adminLevelId, start, end);
        //    return GetIndicatorValue(lfdd, indicatorName);
        //}
        //protected string GetSthDd(int adminLevelId, string indicatorName, DateTime start, DateTime end, ref string errors)
        //{
        //    var _sthdd = drepo.GetRecentDistro((int)DiseaseType.STH, adminLevelId, start, end);
        //    return GetIndicatorValue(_sthdd, indicatorName);
        //}
        //protected string GetSchDd(int adminLevelId, string indicatorName, DateTime start, DateTime end, ref string errors)
        //{
        //    var _schdd = drepo.GetRecentDistro((int)DiseaseType.Schisto, adminLevelId, start, end);
        //    return GetIndicatorValue(_schdd, indicatorName);
        //}
        //protected DiseaseDistroPc _onchodd = null;
        //protected string GetOnchoDd(int adminLevelId, string indicatorName, DateTime start, DateTime end, ref string errors)
        //{
        //    var _onchodd = drepo.GetRecentDistro((int)DiseaseType.Oncho, adminLevelId, start, end);
        //    return GetIndicatorValue(_onchodd, indicatorName);
        //}
        //protected DiseaseDistroPc _tradd = null;
        //protected string GetTrachomaDd(int adminLevelId, string indicatorName, DateTime start, DateTime end, ref string errors)
        //{
        //    var _tradd = drepo.GetRecentDistro((int)DiseaseType.Trachoma, adminLevelId, start, end);
        //    return GetIndicatorValue(_tradd, indicatorName);
        //}
        #endregion
    }


}

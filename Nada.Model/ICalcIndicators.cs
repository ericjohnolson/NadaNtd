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
    public interface ICalcIndicators
    {
        List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel, DateTime start, DateTime end);
        List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId, DateTime start, DateTime end);
        List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end);
        KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors);
        AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime start, DateTime end);
    }

    public class CalcBase : ICalcIndicators
    {
        protected DemoRepository demoRepo = new DemoRepository();

        public AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime start, DateTime end)
        {
            return GetDemography(adminLevelId, start, end);
        }

        public virtual List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public virtual List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId, DateTime start, DateTime end)
        {
            var calculations = indicators.Values.Where(i => i.IsCalculated);
            if (calculations.Count() == 0)
                return new List<KeyValuePair<string, string>>();

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var val in indicatorValues)
                values.Add(typeId + val.Indicator.DisplayName, val.DynamicValue);

            return GetCalculatedValues(
                calculations.Select(i => typeId + i.DisplayName).ToList(),
                values,
                adminLevel, start, end);
        }

        public virtual List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public virtual KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
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

        public string GetPercentage(string numerator, string denominator)
        {
            return GetPercentage(numerator, denominator, 100);
        }

        public string GetPercentage(string numerator, string denominator, int multipler)
        {
            double n, d;
            if (!string.IsNullOrEmpty(numerator) && !string.IsNullOrEmpty(denominator) &&
                double.TryParse(numerator, out n) &&
                double.TryParse(denominator, out d) && d != 0)
                return string.Format("{0:0.00}", n / d * multipler);

            return Translations.NA;
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

        public string GetValueOrDefault(string key, Dictionary<string, string> values)
        {
            if (values.ContainsKey(key))
                return values[key];
            return "";
        }

        #region Related Recent Objects
        protected DiseaseRepository drepo = new DiseaseRepository();
        protected AdminLevelDemography GetDemography(int adminLevelId, DateTime? start, DateTime? end)
        {
            return demoRepo.GetRecentDemography(adminLevelId, start, end);
        }

        
        protected string GetRecentDemographyIndicator(int adminLevelId, string indicatorName, DiseaseType diseaseType, DateTime start, DateTime end, ref string errors)
        {
            //SavedReport report = new SavedReport();
            //DistributionReportGenerator gen = new DistributionReportGenerator();
            //DiseaseRepository repo = new DiseaseRepository();
            //DemoRepository demo = new DemoRepository();
            //DiseaseDistroPc dd = repo.Create(diseaseType);
            //report.ReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator((int)diseaseType,
            //                   new KeyValuePair<string, Indicator>(indicatorName, dd.Indicators[indicatorName])));
            //report.ReportOptions.StartDate = start;
            //report.ReportOptions.EndDate = end;
            //report.ReportOptions.IsByLevelAggregation = true;
            //report.ReportOptions.IsCountryAggregation = false;
            //report.ReportOptions.IsNoAggregation = false;
            //report.ReportOptions.MonthYearStarts = 1;

            //var adminlevel = demo.GetAdminLevelById(adminLevelId);
            //report.ReportOptions.SelectedAdminLevels = new List<AdminLevel> {adminlevel};
            //ReportResult result = gen.Run(report);

            //string translatedIndicator = TranslationLookup.GetValue(indicatorName, indicatorName);
            //if (result.DataTableResults.Columns.Contains(translatedIndicator) && result.DataTableResults.Rows.Count > 1)
            //{
            //    return result.DataTableResults.Rows[1][translatedIndicator].ToString();
            //}
            //string error = string.Format(Translations.ReportsNoDdInDateRange, adminlevel.Name, start.ToShortDateString(), end.ToShortDateString(), dd.Disease.DisplayName) + Environment.NewLine;
            //if (!errors.Contains(error))
            //    errors += error;
            
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

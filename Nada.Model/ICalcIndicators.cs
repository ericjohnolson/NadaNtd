using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public interface ICalcIndicators
    {
        List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel);
        List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId);
        List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime? end);
        KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime? end);
        AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime end);
    }

    public class CalcBase : ICalcIndicators
    {
        protected  DemoRepository demoRepo = new DemoRepository();

        public AdminLevelDemography GetAdminLevelDemo(int adminLevelId, DateTime end)
        {
            return GetDemography(adminLevelId, end);
        }

        public virtual List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public virtual List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues, 
            int adminLevel, string typeId)
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
                adminLevel, null);
        }

        public virtual List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime? end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public virtual KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime? end)
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
            if(values.ContainsKey(key))
                return values[key];
            return "";
        }

        #region Related Recent Objects
        protected DiseaseRepository drepo = new DiseaseRepository();
        protected AdminLevelDemography GetDemography(int adminLevelId, DateTime? end)
        {
            return demoRepo.GetRecentDemography(adminLevelId, end);
        }

        protected string GetIndicatorValue(DiseaseDistroPc dd, string indicatorName)
        {
            var indicator = dd.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == indicatorName);
            if (indicator == null)
                return Translations.NA;
            else
                return indicator.DynamicValue;
        }
        protected DiseaseDistroPc _lfdd = null;
        protected DateTime _lfEnd = DateTime.MaxValue;
        protected DateTime _sthEnd = DateTime.MaxValue;
        protected DateTime _schEnd = DateTime.MaxValue;
        protected DateTime _tracEnd = DateTime.MaxValue;
        protected DateTime _onchoEnd = DateTime.MaxValue;
        protected string GetLfDd(int adminLevelId, string indicatorName, DateTime? end)
        {
            if (_lfdd == null || end.HasValue && end != _lfEnd || _lfEnd != DateTime.MaxValue)
            {
                _lfEnd = end.HasValue ? end.Value : DateTime.MaxValue;
                _lfdd = drepo.GetRecentDistro((int)DiseaseType.Lf, adminLevelId, end);
            }
            return GetIndicatorValue(_lfdd, indicatorName);
        }
        protected DiseaseDistroPc _sthdd = null;
        protected string GetSthDd(int adminLevelId, string indicatorName, DateTime? end)
        {
            if (_sthdd == null || end.HasValue && end != _sthEnd || _sthEnd != DateTime.MaxValue)
            {
                _sthEnd = end.HasValue ? end.Value : DateTime.MaxValue;
                _sthdd = drepo.GetRecentDistro((int)DiseaseType.STH, adminLevelId, end);
            }
            return GetIndicatorValue(_sthdd, indicatorName);
        }
        protected DiseaseDistroPc _schdd = null;
        protected string GetSchDd(int adminLevelId, string indicatorName, DateTime? end)
        {
            if (_schdd == null || end.HasValue && end != _schEnd || _schEnd != DateTime.MaxValue)
            {
                _schEnd = end.HasValue ? end.Value : DateTime.MaxValue;
                _schdd = drepo.GetRecentDistro((int)DiseaseType.Schisto, adminLevelId, end);
            }
            return GetIndicatorValue(_schdd, indicatorName);
        }
        protected DiseaseDistroPc _onchodd = null;
        protected string GetOnchoDd(int adminLevelId, string indicatorName, DateTime? end)
        {
            if (_onchodd == null|| end.HasValue && end != _onchoEnd || _onchoEnd != DateTime.MaxValue)
            {
                _onchoEnd = end.HasValue ? end.Value : DateTime.MaxValue;
                _onchodd = drepo.GetRecentDistro((int)DiseaseType.Oncho, adminLevelId, end);
            }
            return GetIndicatorValue(_onchodd, indicatorName);
        }
        protected DiseaseDistroPc _tradd = null;
        protected string GetTrachomaDd(int adminLevelId, string indicatorName, DateTime? end)
        {
            if (_tradd == null|| end.HasValue && end != _tracEnd || _tracEnd != DateTime.MaxValue)
            {
                _tracEnd = end.HasValue ? end.Value : DateTime.MaxValue;
                _tradd = drepo.GetRecentDistro((int)DiseaseType.Trachoma, adminLevelId, end);
            }
            return GetIndicatorValue(_tradd, indicatorName);
        }
        #endregion
    }


}

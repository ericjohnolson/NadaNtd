using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model
{

    [Serializable]
    public class IndicatorAggregateResult
    {
        public object NewValue { get; set; }
        public string Name { get; set; }
    }


    [Serializable]
    public class AdminLevelIndicators
    {
        public AdminLevelIndicators()
        {
            Children = new List<AdminLevelIndicators>();
            Indicators = new Dictionary<string, AggregateIndicator>();
        }
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public AdminLevelIndicators Parent { get; set; }
        public List<AdminLevelIndicators> Children { get; set; }
        public int LevelNumber { get; set; }
        public bool IsDistrict { get; set; }
        public Dictionary<string, AggregateIndicator> Indicators { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DiseaseName { get; set; }
        public int RedistrictIdForDaughter { get; set; }
        public int RedistrictIdForMother { get; set; }
        
    }


    [Serializable]
    public class AggregateIndicator
    {
        public int IndicatorId { get; set; }
        public int DataType { get; set; }
        public int AggType { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ColumnTypeName { get; set; }
        public int Year { get; set; }
        public int TypeId { get; set; }
        public bool IsCalcRelated { get; set; }
        public int FormId { get; set; }
        public string TypeName { get; set; }
        public DateTime ReportedDate { get; set; }
        public int EntityTypeId { get; set; }
    }


    [Serializable]
    public static class IndicatorAggregator
    {
        public static AggregateIndicator AggregateChildren(List<AdminLevelIndicators> list, string key, AggregateIndicator startResult, List<IndicatorDropdownValue> dropdownOptions)
        {
            AggregateIndicator result = startResult;
            foreach (var level in list)
            {
                if (level.Indicators.ContainsKey(key))
                {
                    result = Aggregate(level.Indicators[key],  result, dropdownOptions);
                }
                else
                    result = AggregateChildren(level.Children, key, result, dropdownOptions);
            }
            return result;
        }

        public static AggregateIndicator Aggregate(AggregateIndicator ind, AggregateIndicator existingValue, List<IndicatorDropdownValue> dropdownOptions)
        {
            AggregateIndicator result = existingValue == null ? ind : existingValue;

            if (existingValue == null)
                result.Value = ind.Value;
            else if (ind.DataType == (int)IndicatorAggType.None)
                result.Value = Translations.NA;
            else if (ind.AggType == (int)IndicatorAggType.Combine && ind.DataType == (int)IndicatorDataType.Dropdown)
                result.Value = existingValue + ", " + TranslationLookup.GetValue(ind.Value, ind.Value);
            else if (ind.AggType == (int)IndicatorAggType.Combine)
                result.Value = AggregateString(ind, existingValue);
            else if (ind.DataType == (int)IndicatorDataType.Number || ind.DataType == (int)IndicatorDataType.Month || ind.DataType == (int)IndicatorDataType.Year)
                result.Value = AggregateNumber(ind, existingValue);
            else if (ind.DataType == (int)IndicatorDataType.Date)
                result.Value = AggregateDate(ind, existingValue);
            else if (ind.DataType == (int)IndicatorDataType.Dropdown)
                result.Value = AggregateDropdown(ind, existingValue, dropdownOptions);
            else
                result.Value = AggregateString(ind, existingValue);

            return result;
        }

        public static AdminLevelDemography AggregateTree(AdminLevel node, double? growthRate)
        {
            if(node.Children.Count == 0)
                return node.CurrentDemography;

            if(growthRate.HasValue)
                node.CurrentDemography.GrowthRate = growthRate.Value;
            node.CurrentDemography.Pop0Month = 0;
            node.CurrentDemography.PopPsac = 0;
            node.CurrentDemography.Pop5yo = 0;
            node.CurrentDemography.PopAdult = 0;
            node.CurrentDemography.PopFemale = 0;
            node.CurrentDemography.PopMale = 0;
            node.CurrentDemography.TotalPopulation = 0;
            node.CurrentDemography.PopSac = 0;
            foreach (var child in node.Children)
            {
                AdminLevelDemography childDemo = AggregateTree(child, growthRate);
                //public int YearCensus { get; set; }
                //public Nullable<int> YearProjections { get; set; }
                //public double GrowthRate { get; set; }
                //public double PercentRural { get; set; }
                node.CurrentDemography.Pop0Month += childDemo.Pop0Month.HasValue ? childDemo.Pop0Month.Value : 0;
                node.CurrentDemography.PopPsac += childDemo.PopPsac.HasValue ? childDemo.PopPsac.Value : 0;
                node.CurrentDemography.Pop5yo += childDemo.Pop5yo.HasValue ? childDemo.Pop5yo.Value : 0;
                node.CurrentDemography.PopAdult += childDemo.PopAdult.HasValue ? childDemo.PopAdult.Value : 0;
                node.CurrentDemography.PopFemale += childDemo.PopFemale.HasValue ? childDemo.PopFemale.Value : 0;
                node.CurrentDemography.PopMale += childDemo.PopMale.HasValue ? childDemo.PopMale.Value : 0;
                node.CurrentDemography.TotalPopulation += childDemo.TotalPopulation.HasValue ? childDemo.TotalPopulation.Value : 0;
                node.CurrentDemography.PopSac += childDemo.PopSac.HasValue ? childDemo.PopSac.Value : 0;
            }
            return node.CurrentDemography;
        }

        private static string AggregateDate(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return DateTime.MinValue.ToString("MM/dd/yyyy");
            if (string.IsNullOrEmpty(ind1.Value))
                return DateTime.MinValue.ToString("MM/dd/yyyy");

            DateTime dt = DateTime.ParseExact(ind1.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime existing = DateTime.ParseExact(existingValue.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (dt >= (DateTime)existing)
                    return existing.ToString("MM/dd/yyyy");
                else
                    return dt.ToString("MM/dd/yyyy");
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (dt >= (DateTime)existing)
                    return dt.ToString("MM/dd/yyyy");
                else
                    return existing.ToString("MM/dd/yyyy");
            return dt.ToString("MM/dd/yyyy");
        }

        private static string AggregateString(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            if(string.IsNullOrEmpty(ind1.Value))
                return existingValue.Value;
            else if(string.IsNullOrEmpty(existingValue.Value))
                return ind1.Value;

            if (ind1.AggType == (int)IndicatorAggType.Combine)
                return existingValue.Value + ", " + ind1.Value;
            else if (ind1.AggType == (int)IndicatorAggType.None)
                return Translations.NA;
            return "Invalid Aggregation Rule or Data Type";
        }

        private static string AggregateNumber(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            double i1 = 0, i2 = 0;
            if (!Double.TryParse(ind1.Value, out i1) && !Double.TryParse(existingValue.Value, out i2))
                return "";
            if (!Double.TryParse(ind1.Value, out i1))
                return i2.ToString();
            if (!Double.TryParse(existingValue.Value, out i2))
                return i1.ToString();

            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return (i1 + i2).ToString();
            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (i1 >= i2)
                    return existingValue.Value;
                else
                    return ind1.Value;
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (i1 >= i2)
                    return ind1.Value;
                else
                    return existingValue.Value;
            return ind1.Value;
        }

        private static string AggregateDropdown(AggregateIndicator ind1, AggregateIndicator existingValue, List<IndicatorDropdownValue> dropdownOptions)
        {
            if (string.IsNullOrEmpty(ind1.Value) && string.IsNullOrEmpty(existingValue.Value))
                return "";
            if (string.IsNullOrEmpty(ind1.Value))
                return existingValue.Value;
            if (string.IsNullOrEmpty(existingValue.Value))
                return ind1.Value;

            var ind1option = dropdownOptions.FirstOrDefault(i => i.IndicatorId == ind1.IndicatorId && (int)i.EntityType == ind1.EntityTypeId
                && i.TranslationKey == ind1.Value);
            var ind2option = dropdownOptions.FirstOrDefault(i => i.IndicatorId == existingValue.IndicatorId && (int)i.EntityType == existingValue.EntityTypeId
                && i.TranslationKey == existingValue.Value);
            if(ind1option == null)
                return existingValue.Value;
            if(ind2option == null)
                return ind1.Value;

            if (ind1.AggType == (int)IndicatorAggType.Min)
            {
                if(ind1option.WeightedValue <= ind2option.WeightedValue)
                    return ind1.Value;
                else
                    return existingValue.Value;
            }
            if (ind1.AggType == (int)IndicatorAggType.Max)
            {
                if (ind1option.WeightedValue >= ind2option.WeightedValue)
                    return ind1.Value;
                else
                    return existingValue.Value;
            }

            return TranslationLookup.GetValue("NA", "NA"); ;
        }

    }

    [Serializable]
    public class IndicatorParser
    {
        public Dictionary<string, Indicator> Indicators { get; set; }
        protected Dictionary<string, Indicator> translatedIndicators = new Dictionary<string, Indicator>();
        protected List<Partner> partners = new List<Partner>();
        private List<IndicatorDropdownValue> ezs = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> eus = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> ess = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> subdistricts = new List<IndicatorDropdownValue>();
        protected List<MonthItem> months = new List<MonthItem>();
        
        public void LoadRelatedLists()
        {
            IntvRepository repo = new IntvRepository();
            partners = repo.GetPartners();
            months = GlobalizationUtil.GetAllMonths();
            SettingsRepository settings = new SettingsRepository();
            ezs = settings.GetEcologicalZones();
            eus = settings.GetEvaluationUnits();
            subdistricts = settings.GetEvalSubDistricts();
            ess = settings.GetEvalSites();
        }

        public object Parse(int dataTypeId, int indicatorId, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (dataTypeId == (int)IndicatorDataType.Number)
                return Double.Parse(value);
            else if (dataTypeId == (int)IndicatorDataType.Date)
                return DateTime.ParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToShortDateString();
            else if (dataTypeId == (int)IndicatorDataType.Partners)
            {
                List<string> values = new List<string>();
                foreach (string id in value.Split('|'))
                {
                    int pId = 0;
                    if (Int32.TryParse(id, out pId))
                    {
                        Partner partner = partners.FirstOrDefault(p => p.Id == pId);
                        if (partner != null)
                            values.Add(partner.DisplayName);
                    }
                }
                if (values.Count > 0)
                    return string.Join(Util.EnumerationDelinator, values.ToArray());
                else
                    return value;
            }
            else if (dataTypeId == (int)IndicatorDataType.EvaluationUnit)
            {
                int pId = 0;
                if (Int32.TryParse(value, out pId))
                {
                    var eu = eus.FirstOrDefault(v => v.Id == pId);
                    if (eu != null)
                        return eu.DisplayName;
                    else
                        return value;
                }
                else
                    return value;
            }
            else if (dataTypeId == (int)IndicatorDataType.EvaluationSite)
            {
                int pId = 0;
                if (Int32.TryParse(value, out pId))
                {
                    var es = ess.FirstOrDefault(v => v.Id == pId);
                    if (es != null)
                        return es.DisplayName;
                    else
                        return value;
                }
                else
                    return value;
            }
            else if (dataTypeId == (int)IndicatorDataType.EvalSubDistrict)
            {
                int pId = 0;
                if (Int32.TryParse(value, out pId))
                {
                    var sd = subdistricts.FirstOrDefault(v => v.Id == pId);
                    if (sd != null)
                        return sd.DisplayName;
                    else
                        return value;
                }
                else
                    return value;

            }
            else if (dataTypeId == (int)IndicatorDataType.EcologicalZone)
            {
                int pId = 0;
                if (Int32.TryParse(value, out pId))
                {
                    var ez = ezs.FirstOrDefault(v => v.Id == pId);
                    if (ez != null)
                        return ez.DisplayName;
                    else
                        return value;
                }
                else
                    return value;
            }
            else if (dataTypeId == (int)IndicatorDataType.Multiselect)
            {
                List<string> values = new List<string>();
                foreach (string key in value.Split('|'))
                    values.Add(TranslationLookup.GetValue(key, key));
                return string.Join(Util.EnumerationDelinator, values.ToArray());
            }
            else if (dataTypeId == (int)IndicatorDataType.Dropdown)
            {
                return TranslationLookup.GetValue(value, value);
            }
            else if (dataTypeId == (int)IndicatorDataType.Month)
            {
                var monthItem = months.FirstOrDefault(v => v.Id == Convert.ToInt32(value));
                if (monthItem != null)
                    return monthItem.Name;

            }
            else if (dataTypeId == (int)IndicatorDataType.DiseaseMultiselect)
            {
                List<string> values = new List<string>();
                foreach (string key in value.Split('|'))
                    values.Add(TranslationLookup.GetValue(key, key));
                
                return string.Join(Util.EnumerationDelinator, values.ToArray());
            }
            else if (dataTypeId == (int)IndicatorDataType.YesNo)
            {
                if (value == "0")
                    return TranslationLookup.GetValue("No", "No");
                else
                    return TranslationLookup.GetValue("Yes", "Yes");
            }
            else
                return value;

            return null;
        }

    }

}

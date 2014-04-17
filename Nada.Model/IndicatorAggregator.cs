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
    }


    [Serializable]
    public class AggregateIndicator
    {
        public int IndicatorId { get; set; }
        public int DataType { get; set; }
        public int AggType { get; set; }
        public int WeightedValue { get; set; }
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
    }


    [Serializable]
    public static class IndicatorAggregator
    {
        public static AggregateIndicator AggregateChildren(List<AdminLevelIndicators> list, string key, AggregateIndicator startResult)
        {
            AggregateIndicator aggregation = startResult;
            foreach (var level in list)
            {
                if (level.Indicators.ContainsKey(key))
                {
                    aggregation = Aggregate(level.Indicators[key], aggregation);
                }
                else
                    aggregation = AggregateChildren(level.Children, key, aggregation);
            }
            return aggregation;
        }

        public static AggregateIndicator Aggregate(AggregateIndicator ind, AggregateIndicator existingValue)
        {
            AggregateIndicator result = null;
            // THIS WAS AN ATTEMPT TO FIX THIS INDICATOR BY INDICATOR ISSUE ON THE REPORTS, not sure that is correct :(
            if (ind.ReportedDate > existingValue.ReportedDate)
                result = ind;
            else
                result = existingValue;

            if (existingValue == null)
                result.Value = ind.Value;
            else if (ind.DataType == (int)IndicatorAggType.None)
                result.Value = Translations.NA;
            else if (ind.DataType == (int)IndicatorAggType.Recent)
            {
                // already handled above
            }
            else if (ind.AggType == (int)IndicatorAggType.Combine && ind.DataType == (int)IndicatorDataType.Dropdown)
                result.Value = existingValue + ", " + TranslationLookup.GetValue(ind.Value, ind.Value);
            else if (ind.AggType == (int)IndicatorAggType.Combine)
                result.Value = existingValue + ", " + ind.Value;
            else if (ind.DataType == (int)IndicatorDataType.Number)
                result.Value = AggregateDouble(ind, existingValue);
            else if (ind.DataType == (int)IndicatorDataType.Date)
                result.Value = AggregateDate(ind, existingValue);
            else if (ind.DataType == (int)IndicatorDataType.Dropdown)
                result.Value = AggregateDropdown(ind, existingValue);
            else
                result.Value = AggregateString(ind, existingValue);

            return result;
            //if (val.DataType == (int)IndicatorDataType.Date)
            //    dic[adminLevelId].Indicators[indicatorKey].Value = val == null ? "" : ((DateTime)val).ToString("MM/dd/yyyy");
            //else
            //    dic[adminLevelId].Indicators[indicatorKey].Value = val == null ? "" : val.ToString();
        }

        public static object ParseValue(AggregateIndicator ind)
        {
            if (ind.DataType == (int)IndicatorDataType.Number)
                return Double.Parse(ind.Value);
            else if (ind.DataType == (int)IndicatorDataType.Date)
                return DateTime.ParseExact(ind.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                return ind.Value;
        }
        public static AdminLevelDemography AggregateTree(AdminLevel node)
        {
            if(node.Children.Count == 0)
                return node.CurrentDemography;

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
                AdminLevelDemography childDemo = AggregateTree(child);
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
                node.CurrentDemography.TotalPopulation += childDemo.TotalPopulation;
                node.CurrentDemography.PopSac += childDemo.PopSac;
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
            if (ind1.AggType == (int)IndicatorAggType.Combine)
                return existingValue.Value + ", " + ind1.Value;
            else if (ind1.AggType == (int)IndicatorAggType.None)
                return Translations.NA;
            return "Invalid Aggregation Rule or Data Type";
        }

        private static string AggregateDouble(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            double i1 = Double.Parse(ind1.Value);
            double i2 = Double.Parse(existingValue.Value);
            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return (i1 + i2).ToString();
            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (i1 >= i2)
                    return i2.ToString();
                else
                    return i1.ToString();
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (i1 >= i2)
                    return i1.ToString();
                else
                    return i2.ToString();
            return i1.ToString();
        }

        private static string AggregateDropdown(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            return ind1.Value;
            //var ind2 = (AggregateIndicator)existingValue;
            //if (ind1.AggType == (int)IndicatorAggType.Min)
            //    if (ind1.WeightedV)
            //        return (AggregateIndicator)existingValue;
            //    else
            //        return dt;
            //if (ind1.AggType == (int)IndicatorAggType.Max)
            //    if (dt >= (AggregateIndicator)existingValue)
            //        return dt;
            //    else
            //        return (AggregateIndicator)existingValue;

            //return dt;
        }

    }
}

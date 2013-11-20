using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class AggregateIndicator
    {
        public int IndicatorId { get; set; }
        public int DataType { get; set; }
        public int AggType { get; set; }
        public string Value { get; set; }
    }

    public static class IndicatorAggregator
    {
        public static object AggregateChildren(List<AdminLevelIndicators> list, string key, object startResult, Func<AggregateIndicator, object, object> customAggRule)
        {
            object aggregation = startResult;
            foreach (var level in list)
            {
                if (level.Indicators.ContainsKey(key))
                    aggregation = Aggregate(level.Indicators[key], aggregation, customAggRule);
                else
                    aggregation = AggregateChildren(level.Children, key, aggregation, customAggRule);
            }
            return aggregation;
        }

        public static object Aggregate(AggregateIndicator ind, object existingValue, Func<AggregateIndicator, object, object> customAggRule)
        {
            if (existingValue == null)
                return ind.Value;
            if (ind.AggType == (int)IndicatorAggType.Combine && ind.DataType == (int)IndicatorDataType.Dropdown)
                return existingValue + ", " + TranslationLookup.GetValue(ind.Value, ind.Value);
            if (ind.AggType == (int)IndicatorAggType.Combine)
                return existingValue + ", " + ind.Value;
            if (ind.DataType == (int)IndicatorDataType.Number)
                return AggregateDouble(ind, existingValue);
            if (ind.DataType == (int)IndicatorDataType.Date)
                return AggregateDate(ind, existingValue);
            if (ind.DataType == (int)IndicatorDataType.Dropdown)
                return customAggRule(ind, existingValue);

            return AggregateString(ind, existingValue);
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

        private static DateTime AggregateDate(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return DateTime.MinValue;

            DateTime dt = Convert.ToDateTime(ind1.Value);
            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (dt >= (DateTime)existingValue)
                    return (DateTime)existingValue;
                else
                    return dt;
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (dt >= (DateTime)existingValue)
                    return dt;
                else
                    return (DateTime)existingValue;
            return dt;
        }

        private static string AggregateString(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.AggType == (int)IndicatorAggType.Combine)
                return existingValue + ", " + ind1.Value;
            
            return "Invalid Aggregation Rule or Data Type";
        }

        private static double AggregateDouble(AggregateIndicator ind1, object existingValue)
        {
            double i1 = Double.Parse(ind1.Value);
            double i2 = Convert.ToDouble(existingValue);
            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return i1 + i2;
            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (i1 >= i2)
                    return i2;
                else
                    return i1;
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (i1 >= i2)
                    return i1;
                else
                    return i2;
            return i1;
        }

    }
}

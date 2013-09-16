using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Nada.Model
{
    public static class Util
    {
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }

        }

        public static Dictionary<string, Indicator> CreateIndicatorDictionary(IHaveDynamicIndicators i)
        {
            Dictionary<string, Indicator> indicators = new Dictionary<string, Indicator>();
            foreach (var indicator in i.Indicators)
            {
                indicators.Add(indicator.DisplayName, indicator);
            }
            return indicators;
        }

        public static Dictionary<string, IndicatorValue> CreateIndicatorValueDictionary(IHaveDynamicIndicatorValues i)
        {
            Dictionary<string, IndicatorValue> indicators = new Dictionary<string, IndicatorValue>();
            foreach (var val in i.IndicatorValues)
            {
                indicators.Add(val.Indicator.DisplayName, val);
            }
            return indicators;
        }
    }
}

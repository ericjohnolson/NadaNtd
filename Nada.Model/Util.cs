﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Nada.Globalization;

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
        
        public static Dictionary<string, IndicatorValue> CreateIndicatorValueDictionary(IHaveDynamicIndicatorValues i)
        {
            return CreateIndicatorValueDictionary(i.IndicatorValues);
        }
        public static Dictionary<string, IndicatorValue> CreateIndicatorValueDictionary(List<IndicatorValue> vals)
        {
            Dictionary<string, IndicatorValue> indicators = new Dictionary<string, IndicatorValue>();
            foreach (var val in vals)
            {
                indicators.Add(val.Indicator.DisplayName, val);
            }
            return indicators;
        }

        public static string GetAuditInfo(OleDbDataReader reader)
        {
            return Translations.AuditCreated + ": " + reader.GetValueOrDefault<string>("CreatedBy") + " on " + reader.GetValueOrDefault<DateTime>("CreatedAt").ToString("MM/dd/yyyy")
                   + ", " + Translations.AuditUpdated + ": " + reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy");
        }
        
        public static List<string> ProduceEnumeration(List<string> source)
        {
            List<string> enumerations = new List<string>();
            for (int i = 0; i < (1 << source.Count); i++)
                    enumerations.Add(string.Join(", ", constructSetFromBits(i).Select(n => source[n]).ToArray()));
            enumerations.RemoveAt(0);
            return enumerations;
        }

        private static IEnumerable<int> constructSetFromBits(int i)
        {
            for (int n = 0; i != 0; i /= 2, n++)
            {
                if ((i & 1) != 0)
                    yield return n;
            }
        }

    }
}

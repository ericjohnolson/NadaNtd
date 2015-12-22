using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Nada.Globalization;

namespace Nada.Model
{
    public static class Util
    {
        public static readonly int MaxRounds = 5;
        public static readonly int MaxExcelSheetNameLength = 31;
        public static readonly string EnumerationDelinator = " & ";
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

        public static string CleanFilename(string source)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(source, "");
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

        public static List<string> ProduceEnumeration(List<string> source)
        {
            List<string> enumerations = new List<string>();
            for (int i = 0; i < (1 << source.Count); i++)
                enumerations.Add(string.Join(EnumerationDelinator, ConstructSetFromBits(i).Select(n => source[n]).ToArray()));
            enumerations.RemoveAt(0);
            return enumerations;
        }

        private static IEnumerable<int> ConstructSetFromBits(int i)
        {
            for (int n = 0; i != 0; i /= 2, n++)
            {
                if ((i & 1) != 0)
                    yield return n;
            }
        }

        public static double ParseIndicatorDouble(Dictionary<string, IndicatorValue> inds, string name)
        {
            double val = -1;
            if (inds.ContainsKey(name))
                double.TryParse(inds[name].DynamicValue, out val);
            return val;
        }

        public static int ParseIndicatorInt(Dictionary<string, IndicatorValue> inds, string name)
        {
            int val = -1;
            if (inds.ContainsKey(name))
                int.TryParse(inds[name].DynamicValue, out val);
            return val;
        }

        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public static int GetYearReported(int month, DateTime dateReported)
        {
            if (dateReported.Month >= month)
                return dateReported.Year;
            else
                return dateReported.Year - 1;
        }

        public static bool HasInternetConnection()
        {
            var logger = new Logger();
            try
            {
                string myAddress = "www.google.com";
                IPAddress[] addresslist = Dns.GetHostAddresses(myAddress);

                if (addresslist[0].ToString().Length > 6)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                logger.Error("Exception checking HasInternetConnection", ex);
                return false;
            }

        }

        public static List<string> SplitCommonList(string val)
        {
            List<string> result = new List<string>();
            val = val.Replace(" " + TranslationLookup.GetValue("UtilSplitAnd") + " ", ",");
            foreach(var v in val.Split(','))
                result.Add(v.Trim());
            return result;
        }

        public static string TruncateStringForExcelSheetName(string str)
        {
            if (str.Length > MaxExcelSheetNameLength)
                str = str.Substring(0, MaxExcelSheetNameLength);
            return str;
        }


    }
}

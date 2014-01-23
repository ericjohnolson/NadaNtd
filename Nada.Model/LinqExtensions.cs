using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.ComponentModel;

namespace Nada.Model
{
    public static class NullSafeGetter
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.GetValueOrDefault<T>(ordinal);
        }

        public static double? GetNullableDouble(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            if (!row.IsDBNull(ordinal))
                return new Nullable<double>(Convert.ToDouble(row.GetValue(ordinal)));
            return null;
        }

        public static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Samples.AccessProviders;

namespace Nada.DA
{
    public static class OleDbUtil
    {
        public static OleDbParameter CreateDateTimeOleDbParameter(string parameterName, DateTime dt)
        {
            OleDbParameter p = new OleDbParameter(parameterName, OleDbType.DBTimeStamp);
            p.Direction = ParameterDirection.Input;
            p.Value = AccessConnectionHelper.RoundToSeconds(dt);
            return p;
        }

        public static OleDbParameter CreateNullableParam(string parameterName, Nullable<int> intValue)
        {
            if(intValue.HasValue)
                return new OleDbParameter(parameterName,    intValue.Value);
            return  new OleDbParameter(parameterName, DBNull.Value);
        }

        public static OleDbParameter CreateNullableParam(string parameterName, Nullable<double> doubleValue)
        {
            if (doubleValue.HasValue)
                return new OleDbParameter(parameterName, doubleValue.Value);
            return new OleDbParameter(parameterName, DBNull.Value);
        }

        public static OleDbParameter CreateNullableParam(string parameterName, Nullable<DateTime> val)
        {
            if (val.HasValue)
                CreateDateTimeOleDbParameter(parameterName, val.Value);
            return new OleDbParameter(parameterName, DBNull.Value);
        }

        public static OleDbParameter CreateNullableParam(string parameterName, string val)
        {
            if (!string.IsNullOrEmpty(val))
                return new OleDbParameter(parameterName, val);
            return new OleDbParameter(parameterName, DBNull.Value);
        }
    }
}

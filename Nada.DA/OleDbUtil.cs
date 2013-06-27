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
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.Model.Reports;

namespace Nada.Model.Repositories
{
    public class ReportRepository
    {
        public DataTable GetReportData(ReportIndicators settings)
        {
            var ds = new DataSet();
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(CreateReportSql(settings), connection);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return ds.Tables[0];
        }

        private string CreateReportSql(ReportIndicators settings)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select AdminLevels.DisplayName as AdminLevel, SurveyLfMf.TimingType, SurveyLfMf.TestType, SurveyLfMf.SiteType, SurveyLfMf.SurveyDate ");
            if (settings.ShowRoundsMda)
                sb.Append(", SurveyLfMf.RoundsMda");
            if (settings.ShowExamined)
                sb.Append(", SurveyLfMf.Examined");
            if (settings.ShowPositive)
                sb.Append(", SurveyLfMf.Positive");
            if (settings.ShowMeanDensity)
                sb.Append(", SurveyLfMf.MeanDensity");
            if (settings.ShowMfCount)
                sb.Append(", SurveyLfMf.MfCount");
            if (settings.ShowMfLoad)
                sb.Append(", SurveyLfMf.MfLoad");
            if (settings.ShowSampleSize)
                sb.Append(", SurveyLfMf.SampleSize");
            if (settings.ShowAgeRange)
                sb.Append(", SurveyLfMf.AgeRange");
            sb.Append(" FROM (SurveyLfMf INNER JOIN AdminLevels ON SurveyLfMf.AdminLevelId = AdminLevels.ID) ");

            return sb.ToString();
        }
    }
}

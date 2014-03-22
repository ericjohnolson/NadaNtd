using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Nada.Model.Base;

namespace Nada.Model.Reports
{
    public class SavedReport : NadaClass
    {
        public SavedReport()
        {
            ReportOptions = new ReportOptions();
        }
        public string DisplayName { get; set; }
        public string SerializedReportOptions { get; set; }
        public ReportOptions ReportOptions { get; set; }

        public void Deserialize()
        {
            var serializer = new JavaScriptSerializer();
            ReportOptions = serializer.Deserialize<ReportOptions>(SerializedReportOptions);
        }

        public void Serialize()
        {
            var serializer = new JavaScriptSerializer();
            SerializedReportOptions = serializer.Serialize(ReportOptions);

        }
    }
}

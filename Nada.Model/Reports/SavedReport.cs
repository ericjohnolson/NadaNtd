using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Reports
{
    public class SavedReport : NadaClass
    {
        private Logger log = new Logger();
        public SavedReport()
        {
            ReportOptions = new ReportOptions();
            TypeName = Translations.CustomReport;
        }
        public string DisplayName { get; set; }
        public string TypeName { get; set; }
        public ReportOptions ReportOptions { get; set; }
        public IStandardOptions StandardReportOptions { get; set; }

        public void Deserialize(string s)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                ReportOptions = serializer.Deserialize<ReportOptions>(s);

                switch (ReportOptions.EntityType)
                {
                    case IndicatorEntityType.DiseaseDistribution:
                        ReportOptions.ReportGenerator = new DistributionReportGenerator();
                        break;
                    case IndicatorEntityType.Intervention:
                        ReportOptions.ReportGenerator = new IntvReportGenerator();
                        break;
                    case IndicatorEntityType.Survey:
                        ReportOptions.ReportGenerator = new SurveyReportGenerator();
                        break;
                    case IndicatorEntityType.Process:
                        ReportOptions.ReportGenerator = new ProcessReportGenerator();
                        break;
                    case IndicatorEntityType.Demo:
                        ReportOptions.ReportGenerator = new DemoReportGenerator();
                        break;
                    case IndicatorEntityType.Sae:
                        ReportOptions.ReportGenerator = new BaseReportGenerator();
                        break;
                    default:
                        ReportOptions.ReportGenerator = new BaseReportGenerator();
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Serialize Report Error", ex);
            }
        }

        public string Serialize()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(ReportOptions);
            }
            catch (Exception ex)
            {
                log.Error("Serialize Report Error", ex);
            }

            return null;
        }
    }
}

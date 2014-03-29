using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using Nada.Model.Base;

namespace Nada.Model.Reports
{
    public class SavedReport : NadaClass
    {
        private Logger log = new Logger();
        public SavedReport()
        {
            ReportOptions = new ReportOptions();
        }
        public string DisplayName { get; set; }
        public ReportOptions ReportOptions { get; set; }

        public void Deserialize(string s)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                ReportOptions = serializer.Deserialize<ReportOptions>(s);

                switch (ReportOptions.EntityType)
                {
                    case IndicatorEntityType.DiseaseDistribution:
                        ReportOptions.ReportGenerator = new IntvReportGenerator();
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

        //public void Deserialize(byte[] b)
        //{
        //    try
        //    {
        //        using (var stream = new MemoryStream(b))
        //        {
        //            var formatter = new BinaryFormatter();
        //            stream.Seek(0, SeekOrigin.Begin);
        //            ReportOptions = (ReportOptions)formatter.Deserialize(stream);
        //        }
        //    }
        //catch (Exception ex)
        //{
        //    log.Error("Deserialize Report Error", ex);
        //}
        //}

        //public byte[] Serialize()
        //{
        //    try
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            var formatter = new BinaryFormatter();
        //            formatter.Serialize(stream, ReportOptions);
        //            stream.Flush();
        //            stream.Position = 0;
        //            return stream.ToArray();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Serialize Report Error", ex);
        //    }
        //    return null;
        //}
    }
}

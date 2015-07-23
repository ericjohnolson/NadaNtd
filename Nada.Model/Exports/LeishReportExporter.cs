using Nada.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Exports
{
    public class LeishReportExporter : ExporterBase, IExporter
    {
        public string ExportName
        {
            get { return Translations.LeishReport; }
        }

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
        {
            throw new NotImplementedException();
        }

    }
}

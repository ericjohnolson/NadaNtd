using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Repositories;

namespace Nada.Model.Diseases
{
    [Serializable]
    public class DiseaseDistroPc : NadaClass, IHaveDynamicIndicators, IHaveDynamicIndicatorValues, IDataErrorInfo
    {
        public DiseaseDistroPc()
        {
            IndicatorValues = new List<IndicatorValue>();
            IndicatorDropdownValues = new List<IndicatorDropdownValue>();
        }

        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public Nullable<DateTime> DateReported { get; set; }

        public void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            if (inds.ContainsKey("DateReported"))
                DateReported = Convert.ToDateTime(inds["DateReported"].DynamicValue);
        }

        public List<KeyValuePair<string, string>> GetDemographyStats()
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string,string>>();

            DemoRepository demo = new DemoRepository();
            int year = DateTime.Now.Year;
            if (DateReported.HasValue)
                year = DateReported.Value.Year;
            AdminLevelDemography d = demo.GetDemoByAdminLevelIdAndYear(AdminLevelId.Value, year);
            values.Add(new KeyValuePair<string,string>(Translations.DDLFTotalPopulation, d.TotalPopulation.HasValue ? d.TotalPopulation.Value.ToString() : Translations.NA));
            if(Disease.Id == (int)DiseaseType.Lf || Disease.Id == (int)DiseaseType.STH)
                values.Add(new KeyValuePair<string, string>(Translations.DDLFPsacPopulation, d.PopPsac.HasValue ? d.PopPsac.Value.ToString() : Translations.NA));
            if(Disease.Id == (int)DiseaseType.Lf || Disease.Id == (int)DiseaseType.STH || 
                Disease.Id == (int)DiseaseType.Schisto || Disease.Id == (int)DiseaseType.Trachoma)
            {
                values.Add(new KeyValuePair<string, string>(Translations.DDLFSacPopulation, d.PopSac.HasValue ? d.PopSac.Value.ToString() : Translations.NA));
                values.Add(new KeyValuePair<string, string>(Translations.DDLFAdultPopulation, d.PopAdult.HasValue ? d.PopAdult.Value.ToString() : Translations.NA));
            }

            return values;
        }


        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion

    }
}

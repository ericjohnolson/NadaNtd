using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public enum IndicatorDataType
    {
        Text = 1,
        Number = 2,
        YesNo = 3,
        Date = 4,
        Dropdown = 5
    }

    public enum StaticSurveyType
    {
        LfPrevalence = 1,
        LfTas = 2,
        LfMapping = 3,
        BuruliSurvey = 8
    }

    public enum StaticIntvType
    {
        IvmAlbMda = 1,
        LfLymphedemaMorbidity = 2,
        LfHydroceleMorbidity = 3
    }

    public enum DiseaseType
    {
        Lf = 3
    }
}

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
        Date = 4,
        YesNo = 3
    }

    public enum StaticSurveyType
    {
        LfPrevalence = 1,
        LfTas = 2,
        LfMapping = 3
    }

    public enum StaticIntvType
    {
        LfMda = 1,
        LfLymphedemaMorbidity = 2,
        LfHydroceleMorbidity = 3
    }
}

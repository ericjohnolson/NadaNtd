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

    public enum IndicatorAggType
    {
        Sum = 1,
        Min = 2,
        Max = 3,
        Combine = 4
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
        LfHydroceleMorbidity = 3,
        GuineaWormIntervention = 4,
        LeprosyIntervention = 5,
        HatIntervention =6,
        LeishIntervention = 7, 
        BuruliUlcerIntv = 8, 
        YawsIntervention = 9
    }

    public enum DiseaseType
    {
        Lf = 3,
        GuineaWorm = 6,
        Leprosy = 7,
        Hat = 8,
        Leish = 9,
        Buruli = 10, 
        Yaws = 11
    }
}

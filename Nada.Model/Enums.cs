﻿using System;
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
        Dropdown = 5,
        Multiselect = 6,
        Year = 7,
        Month = 8,
        Partners = 9,
        SentinelSite = 10,
        EcologicalZone = 11,
        EvaluationUnit = 12
    }

    public enum IndicatorAggType
    {
        Sum = 1,
        Min = 2,
        Max = 3,
        Combine = 4,
        None = 5
    }

    public enum IndicatorEntityType
    {
        DiseaseDistribution = 1,
        Intervention = 2,
        Survey = 3,
        Process = 4,
        EvaluationUnit = 5,
        EcologicalZone = 6,
    }

    public enum StaticSurveyType
    {
        LfPrevalence = 1,
        BuruliSurvey = 8,
        LfSentinel = 10,
        SchistoSentinel = 11,
        SthSentinel = 12
    }

    public enum StaticIntvType
    {
        IvmAlbMda = 1,
        LfLymphedemaMorbidity = 2,
        LfHydroceleMorbidity = 3,
        GuineaWormIntervention = 4,
        LeprosyIntervention = 5,
        HatIntervention = 6,
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
        Yaws = 11,
        Oncho = 4,
        STH = 5,
        Schisto = 12,
        Trachoma = 13,
        Chagas = 14,
        Dengue = 15,
        Rabies = 16,
        Echino = 17,
        Foodborne = 18,
        Taeniasis = 19,
        Custom = 20
    }
}

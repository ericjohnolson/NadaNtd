using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public enum IndicatorDataType
    {
        Text = 1, // text
        Number = 2, // number
        YesNo = 3, // no merge/agg
        Date = 4, // date
        Dropdown = 5, // weighted
        Multiselect = 6, // combine
        Year = 7, // number
        Month = 8, // number
        Partners = 9, // combine
        SentinelSite = 10, // no merge/agg
        EcologicalZone = 11, // no merge/agg
        EvaluationUnit = 12, // no merge/agg
        Calculated = 13, // no merge/agg
        EvalSubDistrict = 14, // no merge/agg
        LargeText = 15, // text
        EvaluationSite = 16, // no merge/agg
        DiseaseMultiselect = 17, // combine
        DrugPackageMulitselect = 18
    }

    public enum IndicatorAggType
    {
        Sum = 1,
        Min = 2,
        Max = 3,
        Combine = 4,
        None = 5
    }

    public enum RedistrictingRule
    {
        DefaultBlank = 1,
        Duplicate = 2,
        TBD = 49,
        SplitByPercent = 0
    }

    public enum MergingRule
    {
        DefaultBlank = 1,
        Min = 56, 
        Max = 55, 
        ListAll = 54,
        Sum = 57,
        WorstCase = 58, // Max number of weighting
        BestCase = 51, // min number of weighting
        LeaveBlank53 = 53,
        LeaveBlank59 = 59,
        TBD = 99,
        Average = 50,
        CaseFindingStrategy = 60
    }

    public enum RedistrictingRelationship
    {
        None = 0,
        Mother = 1,
        Daughter = 2
    }

    public enum IndicatorEntityType
    {
        DiseaseDistribution = 1,
        Intervention = 2,
        Survey = 3,
        Process = 4,
        EvaluationUnit = 5,
        EcologicalZone = 6,
        EvalSubDistrict = 7,
        EvalSite = 8,
        Demo = 9, 
        Sae = 10,
        Export = 11
    }

    public enum NewYearType
    {
        SameAsPrevious = 1,
        ApplyGrowthRate = 2
    }

    public enum StaticSurveyType
    {
        LfPrevalence = 1,
        BuruliSurvey = 8,
        LfSentinel = 10,
        SchistoSentinel = 11,
        SthSentinel = 12,
        LfMapping = 16,
        SchMapping = 17,
        SthMapping = 18,
        OnchoMapping = 19,
        OnchoAssessment = 13,
        LfTas = 15,
        TrachomaImpact = 14
    }

    public enum StaticIntvType
    {
        LfMorbidityManagement = 2,
        GuineaWormIntervention = 4,
        LeprosyIntervention = 5,
        HatIntervention = 6,
        LeishIntervention = 7,
        BuruliUlcerIntv = 8,
        YawsIntervention = 9,
        Ivm = 12,
        IvmAlb = 10,
        IvmPzq = 19,
        IvmPzqAlb = 20,
        DecAlb    = 11, 
	    PzqAlb    = 13, 
	    PzqMbd    = 14, 
	    Pzq       = 15, 
	    Alb       = 16, 
	    Mbd       = 17, 
	    Alb2      = 18, 
	    Zithro    = 21, 
	    Teo       = 22, 
	    ZithroTeo = 23 
    }

    public enum StaticProcessType
    {
        PcTraining = 7,
        SAEs = 9,
        SCM = 8
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

    public enum ExportTypeId
    {
        Jrf = 1,
        CmJrf = 2,
        Tas = 3,
        PcEpi = 4,
        RtiWorkbooks = 5
    }

}

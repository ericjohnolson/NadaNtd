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
        DrugPackageMulitselect = 18,
        Integer = 19 //number
    }

    public enum IndicatorAggType
    {
        Sum = 1,
        Min = 2, // best case
        Max = 3, // worst case
        Combine = 4, // List all
        None = 5, // if they  don't provide one
        Average = 6,
        Other = 99
    }

    public enum RedistrictingRule // Splitting Rule
    {
        DefaultBlank = 1,
        Duplicate = 2,
        Other = 49,
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
        Other = 53,
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
	    ZithroTeo = 23,
        TsSurgeries = 24,
        TrachomaFaceClean = 25,
        //LeishAnnual = 26, // Since these were added after initial release, there is no guarantee other databases match up
        //LeishMonthly = 27 // Since these were added after initial release, there is no guarantee other databases match up
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

    public enum ExcelCol
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11,
        L = 12,
        M = 13,
        N = 14,
        O = 15,
        P = 16,
        Q = 17,
        R = 18,
        S = 19,
        T = 20,
        U = 21,
        V = 22,
        W = 23,
        X = 24,
        Y = 25,
        Z = 26,
        AA = 27,
        AB = 28,
        AC = 29,
        AD = 30
    }

    public enum CommonAdminLevelTypesKeys
    {
        Country = 1
    }

}

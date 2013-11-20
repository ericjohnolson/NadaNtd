using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Demography;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.View.Demography;
using Nada.UI.View.DiseaseDistribution;
using Nada.UI.View.Intervention;
using Nada.UI.View.Survey;
using Nada.UI.ViewModel;

namespace Nada.UI.AppLogic
{

    public interface IFetchActivities
    {
        // Surveys
        List<SurveyDetails> GetSurveys();
        IView NewSurvey(SurveyType type);
        IView GetSurvey(SurveyDetails details);
        List<SurveyType> GetSurveyTypes();
        void Delete(SurveyDetails details, int userId);
        // Interventions
        List<IntvDetails> GetIntvs();
        IView NewIntv(IntvType type);
        IView GetIntv(IntvDetails details);
        List<IntvType> GetIntvTypes();
        void Delete(IntvDetails details, int userId);
        // DiseaseDistros
        List<Disease> GetDiseases();
        List<DiseaseDistroDetails> GetDiseaseDistros();
        IView NewDiseaseDistro(Disease disease);
        IView GetDiseaseDistro(DiseaseDistroDetails details);
        void Delete(DiseaseDistroDetails details, int userId);
        //Demo
        List<DemoDetails> GetDemography();
        IView GetDemo(DemoDetails details);
        IView NewDemo();
        void Delete(DemoDetails details, int userId);
    }

    public class ActivityFetcher : IFetchActivities
    {
        AdminLevel adminLevel = null;
        SurveyRepository surveys = new SurveyRepository();
        IntvRepository interventions = new IntvRepository();
        DiseaseRepository diseases = new DiseaseRepository();
        DemoRepository demos = new DemoRepository();

        public ActivityFetcher(AdminLevel adminLevel)
        {
            this.adminLevel = adminLevel;
        }

        #region Surveys
        public List<SurveyDetails> GetSurveys() { return surveys.GetAllForAdminLevel(adminLevel.Id); }
        public List<SurveyType> GetSurveyTypes() { return surveys.GetSurveyTypes(); }
        public void Delete(SurveyDetails details, int userId) { surveys.Delete(details, userId); }

        public IView NewSurvey(SurveyType type)
        {
            if (type.Id < 1)
                return null;
            else if (type.Id == (int)StaticSurveyType.LfPrevalence)
                return new LfMfPrevalenceView(adminLevel);
            else if (type.Id == (int)StaticSurveyType.BuruliSurvey)
                return new DataEntryEdit(new SurveyBaseVm(adminLevel, type.Id, new CalcBuruliSurvey()));

            return new DataEntryEdit(new SurveyBaseVm(adminLevel, type.Id, null));
        }

        public IView GetSurvey(SurveyDetails details)
        {
            if (details.TypeId == (int)StaticSurveyType.LfPrevalence)
            {
                var survey = surveys.GetLfMfPrevalenceSurvey(details.Id);
                return new LfMfPrevalenceView(survey);
            }
            else if (details.TypeId == (int)StaticSurveyType.BuruliSurvey)
                return new DataEntryEdit(new SurveyBaseVm(adminLevel, details, new CalcBuruliSurvey()));
            else
            {
                return new DataEntryEdit(new SurveyBaseVm(adminLevel, details, null));
            }
        }
        #endregion

        #region Interventions
        public List<IntvDetails> GetIntvs() { return interventions.GetAllForAdminLevel(adminLevel.Id); }
        public List<IntvType> GetIntvTypes() { return interventions.GetAllTypes(); }
        public void Delete(IntvDetails details, int userId) { interventions.Delete(details, userId); }

        public IView NewIntv(IntvType type)
        {
            if (type.Id < 1)
                return null;
            else if (type.Id == (int)StaticIntvType.IvmAlbMda)
                return new PcMdaView(adminLevel, type);
            if (type.Id == (int)StaticIntvType.GuineaWormIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcGuineaIntv()));
            if (type.Id == (int)StaticIntvType.BuruliUlcerIntv)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcBuruliIntv()));
            if (type.Id == (int)StaticIntvType.HatIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcHatIntv()));
            if (type.Id == (int)StaticIntvType.LeishIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcLeishIntv()));
            if (type.Id == (int)StaticIntvType.LeprosyIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcLeprosyIntv()));
            if (type.Id == (int)StaticIntvType.YawsIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcYawsIntv()));
           
            return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, null));
        }

        public IView GetIntv(IntvDetails details)
        {
            if (details.TypeId == (int)StaticIntvType.IvmAlbMda)
            {
                var mda = interventions.GetPcMda(details.Id);
                return new PcMdaView(mda);
            }
            if (details.TypeId == (int)StaticIntvType.GuineaWormIntervention)
                    return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcGuineaIntv()));
            if (details.TypeId == (int)StaticIntvType.BuruliUlcerIntv)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcBuruliIntv()));
            if (details.TypeId == (int)StaticIntvType.HatIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcHatIntv()));
            if (details.TypeId == (int)StaticIntvType.LeishIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcLeishIntv()));
            if (details.TypeId == (int)StaticIntvType.LeprosyIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcLeprosyIntv()));
            if (details.TypeId == (int)StaticIntvType.YawsIntervention)
                return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcYawsIntv()));

            return new DataEntryEdit(new IntvBaseVm(adminLevel, details, null));
        }

        #endregion

        #region DiseaseDistro
        public List<DiseaseDistroDetails> GetDiseaseDistros() { return diseases.GetAllForAdminLevel(adminLevel.Id); }
        public List<Disease> GetDiseases() { return diseases.GetSelectedDiseases(); }
        public void Delete(DiseaseDistroDetails details, int userId) { diseases.Delete(details, userId); }
        public IView NewDiseaseDistro(Disease type)
        {
            if (type.Id < 1)
                return null;
            if (type.Id == 7)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcLeprosyDistro()));
            if (type.Id == 8)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcHatDistro()));
            if (type.Id == 9)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcLeishDistro()));
            if (type.Id == 10)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcBuruliDistro()));
            if (type.Id == 11)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcYawsDistro()));

            if (type.DiseaseType == "CM")
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, null));

            return new DataEntryEdit(new DiseaseDistroPcVm(adminLevel, type.Id, null));
        }

        public IView GetDiseaseDistro(DiseaseDistroDetails details)
        {
            DiseaseDistroCm model = diseases.GetDiseaseDistributionCm(details.Id, details.TypeId);
            if (model.Disease.DiseaseType == "CM")
            {
                if (model.Disease.Id == 7)
                    return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcLeprosyDistro()));
                if (model.Disease.Id == 8)
                    return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcHatDistro()));
                if (model.Disease.Id == 9)
                    return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcLeishDistro()));
                if (model.Disease.Id == 10)
                    return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcBuruliDistro()));
                if (model.Disease.Id == 11)
                    return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcYawsDistro()));

                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, null));
            }

            DiseaseDistroPc modelPc = diseases.GetDiseaseDistribution(details.Id, details.TypeId);
            return new DataEntryEdit(new DiseaseDistroPcVm(adminLevel, modelPc, null));
        }

        #endregion

        #region Demo
        public List<DemoDetails> GetDemography() { return demos.GetAdminLevelDemography(adminLevel.Id); }
        public void Delete(DemoDetails details, int userId) { demos.Delete(details, userId); }
        public IView NewDemo()
        {
            return new DemographyView(adminLevel);
        }

        public IView GetDemo(DemoDetails details)
        {
            var demo = demos.GetDemoById(details.Id);
            return new DemographyView(demo, adminLevel);
        }

        #endregion
    }
}

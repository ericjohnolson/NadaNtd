using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.View.DiseaseDistribution;
using Nada.UI.View.Intervention;
using Nada.UI.View.Survey;
using Nada.UI.ViewModel;

namespace Nada.UI.AppLogic
{
    public static class DiseaseActivitiesFactory
    {
        public static IFetchDiseaseActivities GetForDisease(DiseaseType type, AdminLevel adminLevel)
        {
            DiseaseRepository repo = new DiseaseRepository();
            switch (type)
            {
                case DiseaseType.Lf:
                    return new LfActivityFetcher(adminLevel, repo.GetDiseaseById((int)type));
                default:
                    return new LfActivityFetcher(adminLevel, repo.GetDiseaseById((int)type));
            }
        }
    }

    public interface IFetchDiseaseActivities
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
    }

    public class LfActivityFetcher : IFetchDiseaseActivities
    {
        AdminLevel adminLevel = null;
        Disease disease = null;
        SurveyRepository surveys = new SurveyRepository();
        IntvRepository interventions = new IntvRepository();
        DiseaseRepository diseases = new DiseaseRepository();

        public LfActivityFetcher(AdminLevel adminLevel, Disease disease)
        {
            this.adminLevel = adminLevel;
            this.disease = disease;
        }

        #region Surveys
        public List<SurveyDetails> GetSurveys() { return surveys.GetAllForAdminLevel(adminLevel.Id); }
        public List<SurveyType> GetSurveyTypes() { return surveys.GetSurveyTypes(); }
        public void Delete(SurveyDetails details, int userId) { surveys.Delete(details, userId);  }

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
        public List<IntvType> GetIntvTypes() { return interventions.GetTypesByDisease(disease.Id); }
        public void Delete(IntvDetails details, int userId) { interventions.Delete(details, userId); }

        public IView NewIntv(IntvType type)
        {
            if (type.Id < 1)
                return null;
            else if(type.Id == (int)StaticIntvType.IvmAlbMda)
                return new PcMdaView(adminLevel, type);
            else
                return new IntvBaseView(type.Id, adminLevel);
        }

        public IView GetIntv(IntvDetails details)
        {
            if (details.TypeId == (int)StaticIntvType.IvmAlbMda)
            {
                var mda = interventions.GetPcMda(details.Id);
                return new PcMdaView(mda);
            }
            else
            {
                var intv = interventions.GetById(details.Id);
                return new IntvBaseView(intv);
            }
        }

        #endregion

        #region DiseaseDistro
        public List<DiseaseDistroDetails> GetDiseaseDistros() { return diseases.GetAllForAdminLevel(adminLevel.Id); }
        public List<Disease> GetDiseases() { return diseases.GetAllDiseases(); }
        public void Delete(DiseaseDistroDetails details, int userId) { diseases.Delete(details, userId); }
        public IView NewDiseaseDistro(Disease type) 
        {
            if (type.Id < 1)
                return null;
            if(type.Id == 7)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcLeprosyDistro()));
            if (type.Id == 8)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcHatDistro()));
            if (type.Id == 9)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcLeishDistro()));
            if (type.Id == 10)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcBuruliDistro()));
            if (type.Id == 11)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcYawsDistro()));

            if(type.DiseaseType == "CM")
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
    }
}

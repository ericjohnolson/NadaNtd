﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.View.Intervention;
using Nada.UI.View.Survey;

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
    }

    public class LfActivityFetcher : IFetchDiseaseActivities
    {
        AdminLevel adminLevel = null;
        Disease disease = null;
        SurveyRepository surveys = new SurveyRepository();
        IntvRepository interventions = new IntvRepository();

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
            if (type.Id == (int)StaticSurveyType.LfPrevalence)
                return new LfMfPrevalenceView(adminLevel);
            return null;
        }

        public IView GetSurvey(SurveyDetails details)
        {
            if (details.TypeId == (int)StaticSurveyType.LfPrevalence)
            {
                var survey = surveys.GetLfMfPrevalenceSurvey(details.Id);
                return new LfMfPrevalenceView(survey);
            }
            return null;
        }
        #endregion

        #region Interventions
        public List<IntvDetails> GetIntvs() { return interventions.GetAllForAdminLevel(adminLevel.Id); }
        public List<IntvType> GetIntvTypes() { return interventions.GetTypesByDisease(disease.Id); }
        public void Delete(IntvDetails details, int userId) { interventions.Delete(details, userId); }

        public IView NewIntv(IntvType type)
        {
            if (type.Id == (int)StaticIntvType.IvmAlbMda)
                return new PcMdaView(adminLevel);
            return null;
        }

        public IView GetIntv(IntvDetails details)
        {
            if (details.TypeId == (int)StaticIntvType.IvmAlbMda)
            {
                var mda = interventions.GetPcMda(details.Id);
                return new PcMdaView(mda);
            }
            return null;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Demography;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Process;
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
        // processes
        List<ProcessDetails> GetProcesses();
        List<ProcessType> GetProcessTypes();
        void Delete(ProcessDetails details, int userId);
        IView NewProcess(ProcessType type);
        IView GetProcess(ProcessDetails details);
    }

    public class ActivityFetcher : IFetchActivities
    {
        AdminLevel adminLevel = null;
        SurveyRepository surveys = new SurveyRepository();
        IntvRepository interventions = new IntvRepository();
        DiseaseRepository diseases = new DiseaseRepository();
        ProcessRepository processes = new ProcessRepository();
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
            
            SurveyBaseVm vm = new SurveyBaseVm(adminLevel, type.Id, new CalcSurvey());
            
            if (vm.Initialize())
                return new DataEntryEdit(vm);
            else
                return null;
        }

        public IView GetSurvey(SurveyDetails details)
        {
            return new DataEntryEdit(new SurveyBaseVm(adminLevel, details, new CalcSurvey()));
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
            return new DataEntryEdit(new IntvBaseVm(adminLevel, type.Id, new CalcIntv()));
        }

        public IView GetIntv(IntvDetails details)
        {
            return new DataEntryEdit(new IntvBaseVm(adminLevel, details, new CalcIntv()));
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

            if (type.DiseaseType == Translations.CM)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, type.Id, new CalcDistro()));

            return new DataEntryEdit(new DiseaseDistroPcVm(adminLevel, type.Id, new CalcDistro()));
        }

        public IView GetDiseaseDistro(DiseaseDistroDetails details)
        {
            DiseaseDistroCm model = diseases.GetDiseaseDistributionCm(details.Id, details.TypeId);
            if (model.Disease.DiseaseType == Translations.CM)
                return new DataEntryEdit(new DiseaseDistroCmVm(adminLevel, model, new CalcDistro()));
            
            DiseaseDistroPc modelPc = diseases.GetDiseaseDistribution(details.Id, details.TypeId);
            return new DataEntryEdit(new DiseaseDistroPcVm(adminLevel, modelPc, new CalcDistro()));
        }

        #endregion

        #region Demo
        public List<DemoDetails> GetDemography() { return demos.GetAdminLevelDemography(adminLevel.Id); }
        public void Delete(DemoDetails details, int userId)
        {
            SettingsRepository settings = new SettingsRepository();
            var type = settings.GetAdminLevelTypeByLevel(adminLevel.LevelNumber);
            demos.Delete(details, userId);
            if (type.IsAggregatingLevel)
                demos.AggregateUp(type, details.DateReported, userId, null);
        }
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

        #region Processes
        public List<ProcessDetails> GetProcesses() { return processes.GetAllForAdminLevel(adminLevel.Id); }
        public List<ProcessType> GetProcessTypes() { return processes.GetProcessTypes(); }
        public void Delete(ProcessDetails details, int userId) { processes.Delete(details, userId); }

        public IView NewProcess(ProcessType type)
        {
            if (type.Id < 1)
                return null;

            return new DataEntryEdit(new ProcessBaseVm(adminLevel, type.Id, null));
        }

        public IView GetProcess(ProcessDetails details)
        {
            return new DataEntryEdit(new ProcessBaseVm(adminLevel, details, null));
        }
        #endregion
    }
}

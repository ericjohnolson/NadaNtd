using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Process;
using Nada.Model.Repositories;
using Nada.Model.Survey;

namespace Nada.Model.Demography
{
    public class RedistrictingResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RedistrictingExpert
    {
        private Logger logger = new Logger();
        private DemoRepository demoRepo = new DemoRepository();
        private SurveyRepository surveyRepo = new SurveyRepository();
        private DiseaseRepository diseaseRepo = new DiseaseRepository();
        private IntvRepository intvRepo = new IntvRepository();
        private ProcessRepository processRepo = new ProcessRepository();
        private int userId = 0;
        public RedistrictingExpert()
        {
            userId = ApplicationData.Instance.GetUserId();
        }
        public RedistrictingResult Run(RedistrictingOptions options)
        {
            RedistrictingResult result = new RedistrictingResult();
            //return result;

            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (options.SplitType == SplittingType.Merge)
                        result = DoMerge(options, command, connection);
                    else if (options.SplitType == SplittingType.Split)
                        result = DoSplit(options, command, connection);
                    else
                        result = DoSplitCombine(options, command, connection);

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception ex)
                {
                    if (transWasStarted)
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    logger.Error("Exception occurred running redistricting (RedistrictingExpert:Run). " + options.ToString(), ex);
                    return new RedistrictingResult { ErrorMessage = TranslationLookup.GetValue("UnexpectedException", "UnexpectedException") + ex.Message, HasError = true };

                }
            }
            return result;
        }

        #region Split combine
        private RedistrictingResult DoSplitCombine(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            Dictionary<int, List<AdminLevelDemography>> demographyToMerge = new Dictionary<int, List<AdminLevelDemography>>();
            Dictionary<string, List<DiseaseDistroCm>> ddCmToMerge = new Dictionary<string, List<DiseaseDistroCm>>();
            Dictionary<string, List<DiseaseDistroPc>> ddPcToMerge = new Dictionary<string, List<DiseaseDistroPc>>();
            Dictionary<string, List<IntvBase>> intvToMerge = new Dictionary<string, List<IntvBase>>();
            Dictionary<string, List<ProcessBase>> trainingToMerge = new Dictionary<string, List<ProcessBase>>();
            List<SurveyBase> surveys = new List<SurveyBase>();
            var processesToCopyAll = new List<ProcessBase>();
            Dictionary<string, List<ProcessBase>> scmToMerge = new Dictionary<string, List<ProcessBase>>();

            int redistrictId = demoRepo.InsertRedistrictingRecord(command, connection, options, userId);
            foreach (var source in options.SplitDestinations)
            {
                demoRepo.InsertRedistrictUnit(command, connection, userId, source.Unit, redistrictId, RedistrictingRelationship.Mother, source.Percent, false);

                List<AdminLevelDemography> demos = new List<AdminLevelDemography>();
                List<DiseaseDistroCm> ddCms = new List<DiseaseDistroCm>();
                List<DiseaseDistroPc> ddPcs = new List<DiseaseDistroPc>();
                List<IntvBase> intvs = new List<IntvBase>();
                // WHAT TO DO ABOUT SURVEYS?
                // List<SurveyBase> survs = new List<SurveyBase>();
                List<ProcessBase> procs = new List<ProcessBase>();
                // split off portion
                SplitSourceToDestination(options, command, connection, redistrictId, options.MergeDestination, source.Unit, source.Percent,
                    demos, ddCms, ddPcs, intvs,  procs);
                AddToMergingDictionaries(options, demographyToMerge, ddCmToMerge, ddPcToMerge, intvToMerge, trainingToMerge, processesToCopyAll,
                    surveys, scmToMerge, demos, ddCms, ddPcs, intvs, new List<SurveyBase>(), procs);
                // adjust actual unit
                SplitSourceToDestination(options, command, connection, redistrictId, source.Unit, source.Unit, 100 - source.Percent,
                    null, null, null, null, null);
            }
            // do different for surveys

            var processesNoSaes = processesToCopyAll.Where(p => p.ProcessType.Id != (int)StaticProcessType.SAEs).ToList();
            MergeIntoNewUnit(options, command, connection, redistrictId, demographyToMerge, ddCmToMerge, ddPcToMerge, intvToMerge, trainingToMerge,
                processesNoSaes, surveys, scmToMerge);
            // The necessary ones have been moved, now they just need to be saved
            SaveSplitSaes(options.Saes, redistrictId, command, connection);

            return new RedistrictingResult();
        }
        #endregion

        #region Merge
        private RedistrictingResult DoMerge(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            int redistrictId = demoRepo.InsertRedistrictingRecord(command, connection, options, userId);

            Dictionary<int, List<AdminLevelDemography>> demographyToMerge = new Dictionary<int, List<AdminLevelDemography>>();
            Dictionary<string, List<DiseaseDistroCm>> ddCmToMerge = new Dictionary<string, List<DiseaseDistroCm>>();
            Dictionary<string, List<DiseaseDistroPc>> ddPcToMerge = new Dictionary<string, List<DiseaseDistroPc>>();
            Dictionary<string, List<IntvBase>> intvToMerge = new Dictionary<string, List<IntvBase>>();
            Dictionary<string, List<ProcessBase>> trainingToMerge = new Dictionary<string, List<ProcessBase>>();
            List<ProcessBase> processesToCopyAll = new List<ProcessBase>();
            List<SurveyBase> surveys = new List<SurveyBase>();
            Dictionary<string, List<ProcessBase>> scmToMerge = new Dictionary<string, List<ProcessBase>>();

            #region Get forms to merge
            foreach (var source in options.MergeSources)
            {
                List<AdminLevelDemography> demos = new List<AdminLevelDemography>();
                List<DiseaseDistroCm> ddCms = new List<DiseaseDistroCm>();
                List<DiseaseDistroPc> ddPcs = new List<DiseaseDistroPc>();
                List<IntvBase> intvs = new List<IntvBase>();
                List<SurveyBase> survs = new List<SurveyBase>();
                List<ProcessBase> procs = new List<ProcessBase>();
                demoRepo.InsertRedistrictUnit(command, connection, userId, source, redistrictId, RedistrictingRelationship.Mother, 100, true);
                // add all children to merge destination
                options.MergeDestination.Children.AddRange(demoRepo.GetAdminLevelChildren(source.Id));
                // Gather all the necessary forms
                List<DemoDetails> demoDetails = demoRepo.GetAdminLevelDemography(source.Id);
                foreach (var demoDetail in demoDetails)
                    demos.Add(demoRepo.GetDemoById(demoDetail.Id));
                List<DiseaseDistroDetails> ddDetails = diseaseRepo.GetAllForAdminLevel(source.Id);
                foreach (var dd in ddDetails.Where(d => d.DiseaseType == "CM"))
                    ddCms.Add(diseaseRepo.GetDiseaseDistributionCm(dd.Id, dd.TypeId));
                foreach (var dd in ddDetails.Where(d => d.DiseaseType == "PC"))
                    ddPcs.Add(diseaseRepo.GetDiseaseDistribution(dd.Id, dd.TypeId));
                List<IntvDetails> intvDeatils = intvRepo.GetAllForAdminLevel(source.Id);
                foreach (var i in intvDeatils)
                    intvs.Add(intvRepo.GetById(i.Id));
                List<SurveyDetails> survDeatils = surveyRepo.GetAllForAdminLevel(source.Id);
                foreach (var s in survDeatils)
                    survs.Add(surveyRepo.GetById(s.Id));
                List<ProcessDetails> procDetails = processRepo.GetAllForAdminLevel(source.Id);
                foreach (var p in procDetails)
                    procs.Add(processRepo.GetById(p.Id));
                // Determine which forms to merge
                AddToMergingDictionaries(options, demographyToMerge, ddCmToMerge, ddPcToMerge, intvToMerge,
                    trainingToMerge, processesToCopyAll, surveys, scmToMerge, demos, ddCms, ddPcs, intvs, survs, procs);
            }
            #endregion

            MergeIntoNewUnit(options, command, connection, redistrictId, demographyToMerge, ddCmToMerge, ddPcToMerge, intvToMerge,
                trainingToMerge, processesToCopyAll, surveys, scmToMerge);

            return new RedistrictingResult();
        }

        /// <summary>
        /// match forms to merge
        /// </summary>
        private void AddToMergingDictionaries(RedistrictingOptions options, Dictionary<int, List<AdminLevelDemography>> demographyToMerge,
            Dictionary<string, List<DiseaseDistroCm>> ddCmToMerge, Dictionary<string, List<DiseaseDistroPc>> ddPcToMerge, Dictionary<string, List<IntvBase>>
            intvToMerge, Dictionary<string, List<ProcessBase>> trainingToMerge, List<ProcessBase> processesToCopy, List<SurveyBase> surveys,
            Dictionary<string, List<ProcessBase>> scmToMerge, List<AdminLevelDemography> demos, List<DiseaseDistroCm> ddCms, List<DiseaseDistroPc>
            ddPcs, List<IntvBase> intvs, List<SurveyBase> survs, List<ProcessBase> procs)
        {
            //Demography forms – merge for the most recent in the year only
            Dictionary<int, AdminLevelDemography> recentDemo = new Dictionary<int, AdminLevelDemography>();
            foreach (var d in demos)
            {
                int year = Util.GetYearReported(options.YearStartMonth, d.DateDemographyData);
                if (!recentDemo.ContainsKey(year))
                    recentDemo.Add(year, d);
                else if (recentDemo.ContainsKey(year) && recentDemo[year].DateDemographyData < d.DateDemographyData)
                    recentDemo[year] = d;
            }
            foreach (int y in recentDemo.Keys)
            {
                if (demographyToMerge.ContainsKey(y))
                    demographyToMerge[y].Add(recentDemo[y]);
                else
                    demographyToMerge.Add(y, new List<AdminLevelDemography> { recentDemo[y] });
            }
            //DD forms – merge for the most recent in the year only (KEY: Year_Typeid)
            Dictionary<string, DiseaseDistroPc> recentPc = new Dictionary<string, DiseaseDistroPc>();
            foreach (var d in ddPcs)
            {
                string key = Util.GetYearReported(options.YearStartMonth, d.DateReported.Value).ToString() + "_" + d.Disease.Id;
                if (!recentPc.ContainsKey(key))
                    recentPc.Add(key, d);
                else if (recentPc.ContainsKey(key) && recentPc[key].DateReported < d.DateReported)
                    recentPc[key] = d;
            }
            foreach (string k in recentPc.Keys)
            {
                if (ddPcToMerge.ContainsKey(k))
                    ddPcToMerge[k].Add(recentPc[k]);
                else
                    ddPcToMerge.Add(k, new List<DiseaseDistroPc> { recentPc[k] });
            }
            Dictionary<string, DiseaseDistroCm> recentCm = new Dictionary<string, DiseaseDistroCm>();
            foreach (var d in ddCms)
            {
                string key = Util.GetYearReported(options.YearStartMonth, d.DateReported.Value).ToString() + "_" + d.Disease.Id;
                if (!recentCm.ContainsKey(key))
                    recentCm.Add(key, d);
                else if (recentCm.ContainsKey(key) && recentCm[key].DateReported < d.DateReported)
                    recentCm[key] = d;
            }
            foreach (string k in recentCm.Keys)
            {
                if (ddCmToMerge.ContainsKey(k))
                    ddCmToMerge[k].Add(recentCm[k]);
                else
                    ddCmToMerge.Add(k, new List<DiseaseDistroCm> { recentCm[k] });
            }

            //Interventions forms – merge for all in the year with the same round (KEY "YEAR_TYpeId_ROUND")
            foreach (var form in intvs)
            {
                string key = Util.GetYearReported(options.YearStartMonth, form.DateReported).ToString() + "_" + form.IntvType.Id;
                var roundInd = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "PcIntvRoundNumber");
                if (roundInd != null)
                    key += ("_" + roundInd.DynamicValue);
                if (intvToMerge.ContainsKey(key))
                    intvToMerge[key].Add(form);
                else
                    intvToMerge.Add(key, new List<IntvBase> { form });
            }
            // Surveys 
            surveys.AddRange(survs);
            //PC training - merge for all in the year with the same category (KEY "YEAR_CAT")
            foreach (var form in procs.Where(p => p.ProcessType.Id == (int)StaticProcessType.PcTraining))
            {
                string key = Util.GetYearReported(options.YearStartMonth, form.DateReported).ToString();
                var cat = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "PCTrainTrainingCategory");
                if (cat != null)
                    key += ("_" + cat.DynamicValue);
                if (!trainingToMerge.ContainsKey(key))
                    trainingToMerge.Add(key, new List<ProcessBase> { form });
                else
                    trainingToMerge[key].Add(form);
            }
            //SCM –merge for all in the year with the same drug and unit (KEY "YEAR_DRUG_UNIT")
            foreach (var form in procs.Where(p => p.ProcessType.Id == (int)StaticProcessType.SCM))
            {
                string key = Util.GetYearReported(options.YearStartMonth, form.DateReported).ToString();
                var drug = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "SCMDrug");
                if (drug != null)
                    key += ("_" + drug.DynamicValue);
                var unit = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "SCMUnit");
                if (unit != null)
                    key += ("_" + unit.DynamicValue);
                if (scmToMerge.ContainsKey(key))
                    scmToMerge[key].Add(form);
                else
                    scmToMerge.Add(key, new List<ProcessBase> { form });
            }
            //SAE – no merge - copy all separately to merged admin unit. 
            processesToCopy.AddRange(procs.Where(p => p.ProcessType.Id != (int)StaticProcessType.PcTraining && p.ProcessType.Id != (int)StaticProcessType.SCM));
        }

        /// <summary>
        /// After determining which forms to merge, perform the merge by saving the new files and running the merge rules
        /// </summary>
        private void MergeIntoNewUnit(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection, int redistrictId,
            Dictionary<int, List<AdminLevelDemography>> demographyToMerge, Dictionary<string, List<DiseaseDistroCm>> ddCmToMerge,
            Dictionary<string, List<DiseaseDistroPc>> ddPcToMerge, Dictionary<string, List<IntvBase>> intvToMerge, Dictionary<string,
            List<ProcessBase>> trainingToMerge, List<ProcessBase> processesToCopyAll, List<SurveyBase> surveys, Dictionary<string, List<ProcessBase>> scmToMerge)
        {
            demoRepo.InsertRedistrictUnit(command, connection, userId, options.MergeDestination, redistrictId, RedistrictingRelationship.Daughter, 0, false);
            foreach (var val in demographyToMerge.Values)
                MergeDemo(val, options.MergeDestination.Id, redistrictId, command, connection);
            foreach (var val in ddPcToMerge.Values)
                options.DistrosPc.Add(MergeDdPc(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in ddCmToMerge.Values)
                options.DistrosCm.Add(MergeDdCm(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in intvToMerge.Values)
                options.Intvs.Add(MergeIntv(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in trainingToMerge.Values)
                options.Processes.Add(MergeProcess(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in scmToMerge.Values)
                options.Processes.Add(MergeProcess(val, options.MergeDestination.Id, redistrictId, command, connection));
            MergeProcesses(options, processesToCopyAll, options.MergeDestination.Id, redistrictId, command, connection);
            MergeSurveys(options, surveys, options.MergeDestination, redistrictId, command, connection);

        }

        private void MergeDemo(List<AdminLevelDemography> details, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            AdminLevelDemography newDemography = null;
            foreach (var demography in details)
            {
                if (newDemography == null)
                {
                    newDemography = Util.DeepClone(demography);
                    newDemography.Id = 0;
                    newDemography.AdminLevelId = destId;
                }
                else
                {
                    newDemography.Pop0Month += demography.Pop0Month;
                    newDemography.PopPsac += demography.PopPsac;
                    newDemography.Pop5yo += demography.Pop5yo;
                    newDemography.PopAdult += demography.PopAdult;
                    newDemography.PopFemale += demography.PopFemale;
                    newDemography.PopMale += demography.PopMale;
                    newDemography.TotalPopulation += demography.TotalPopulation;
                    newDemography.PopSac += demography.PopSac;
                    newDemography.Notes = MergeNotes(newDemography.Notes, demography.Notes);
                }
            }

            // save
            demoRepo.SaveAdminDemography(command, connection, newDemography, userId);
            foreach (var d in details)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, d.Id, newDemography.Id, IndicatorEntityType.Demo);
        }

        private IntvBase MergeIntv(List<IntvBase> intvs, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            IntvBase newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in intvs)
            {
                if (newForm == null)
                {
                    newForm = Util.DeepClone(form);
                    newForm.Id = 0;
                    newForm.AdminLevelId = destId;
                    newForm.IsRedistricted = true;
                }
                else
                    newForm.Notes = MergeNotes(newForm.Notes, form.Notes);

                MergeIndicators(form.IndicatorValues, newInds, IndicatorEntityType.Intervention, newForm.IntvType.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            intvRepo.SaveIntvBase(command, connection, newForm, userId);
            foreach (var i in intvs)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.Intervention);
            return newForm;
        }

        private DiseaseDistroCm MergeDdCm(List<DiseaseDistroCm> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            DiseaseDistroCm newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in toMerge)
            {
                if (newForm == null)
                {
                    newForm = Util.DeepClone(form);
                    newForm.Id = 0;
                    newForm.AdminLevelId = destId;
                    newForm.IsRedistricted = true;
                }
                else
                    newForm.Notes = MergeNotes(newForm.Notes, form.Notes);
                MergeIndicators(form.IndicatorValues, newInds, IndicatorEntityType.DiseaseDistribution, newForm.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            diseaseRepo.SaveCm(newForm, userId, connection, command);
            foreach (var i in toMerge)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.DiseaseDistribution);
            return newForm;
        }

        private DiseaseDistroPc MergeDdPc(List<DiseaseDistroPc> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            DiseaseDistroPc newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in toMerge)
            {
                if (newForm == null)
                {
                    newForm = Util.DeepClone(form);
                    newForm.Id = 0;
                    newForm.AdminLevelId = destId;
                    newForm.IsRedistricted = true;
                }
                else
                    newForm.Notes = MergeNotes(newForm.Notes, form.Notes);

                MergeIndicators(form.IndicatorValues, newInds, IndicatorEntityType.DiseaseDistribution, newForm.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            diseaseRepo.SavePc(newForm, userId, connection, command);
            foreach (var i in toMerge)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.DiseaseDistribution);
            return newForm;
        }

        private ProcessBase MergeProcess(List<ProcessBase> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            ProcessBase newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in toMerge)
            {
                if (newForm == null)
                {
                    newForm = Util.DeepClone(form);
                    newForm.Id = 0;
                    newForm.AdminLevelId = destId;
                    newForm.IsRedistricted = true;
                }
                else
                    newForm.Notes = MergeNotes(newForm.Notes, form.Notes);

                MergeIndicators(form.IndicatorValues, newInds, IndicatorEntityType.Process, newForm.ProcessType.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            processRepo.Save(command, connection, newForm, userId);
            foreach (var i in toMerge)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.Process);
            return newForm;
        }

        private void MergeProcesses(RedistrictingOptions opts, List<ProcessBase> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            foreach (var oldForm in toMerge)
            {
                ProcessBase newForm = Util.DeepClone(oldForm);

                newForm.Id = 0;
                newForm.IsRedistricted = true;
                newForm.AdminLevelId = destId;
                processRepo.Save(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, oldForm.Id, newForm.Id, IndicatorEntityType.Process);
                opts.Processes.Add(newForm);
            }
        }

        private void MergeSurveys(RedistrictingOptions opts, List<SurveyBase> toMerge, AdminLevel dest, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {   
            foreach (var oldForm in toMerge)
            {
                SurveyBase newForm = Util.DeepClone(oldForm);
                // remove all but merge sources
                oldForm.AdminLevels.RemoveAll(a => !opts.MergeSources.Select(s => s.Id).Contains(a.Id));
                surveyRepo.SaveSurveyBase(command, connection, oldForm, userId);

                newForm.Id = 0;
                newForm.IsRedistricted = true;
                // remove all the old admin levels
                newForm.AdminLevels.RemoveAll(a => opts.MergeSources.Select(s => s.Id).Contains(a.Id));
                newForm.AdminLevels.Add(dest);
                surveyRepo.SaveSurveyBase(command, connection, newForm, userId);
                opts.Surveys.Add(newForm);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, oldForm.Id, newForm.Id, IndicatorEntityType.Survey);
            }
        }

        private void MergeIndicators(List<IndicatorValue> existing, Dictionary<int, IndicatorValue> newValues, IndicatorEntityType entityType,
            List<IndicatorDropdownValue> dropdownOptions)
        {
            foreach (var e in existing)
            {
                if (!newValues.ContainsKey(e.IndicatorId))
                {
                    newValues.Add(e.IndicatorId, e);
                    continue;
                }
                newValues[e.IndicatorId] = IndicatorMerger.Merge(e, newValues[e.IndicatorId], dropdownOptions, entityType);
            }
        }
        
        private string MergeNotes(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return "";
            if (string.IsNullOrEmpty(a))
                return b;
            if (string.IsNullOrEmpty(b))
                return a;
            return a + ". " + b;
        }
        #endregion

        #region Split
        private RedistrictingResult DoSplit(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            int redistrictId = demoRepo.InsertRedistrictingRecord(command, connection, options, userId);
            demoRepo.InsertRedistrictUnit(command, connection, userId, options.Source, redistrictId, RedistrictingRelationship.Mother, 0, true);
            foreach (var dest in options.SplitDestinations)
            {
                demoRepo.InsertRedistrictUnit(command, connection, userId, dest.Unit, redistrictId, RedistrictingRelationship.Daughter, dest.Percent, false);
                SplitSourceToDestination(options, command, connection, redistrictId, dest.Unit, options.Source, dest.Percent, null, null, null, null,
                    null);
            }
            // copy all surveys into new groups 
            List<SurveyDetails> surveys = surveyRepo.GetAllForAdminLevel(options.Source.Id);
            var newSites = SplitSentinelSites(options.Source, options.SplitDestinations, command, connection);
            foreach (var survey in surveys)
                options.Surveys.Add(SplitSurveys(survey, options.Source, options.SplitDestinations, redistrictId, command, connection, newSites));

            SaveSplitSaes(options.Saes, redistrictId, command, connection);
            return new RedistrictingResult();
        }

        private void SplitSourceToDestination(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection, int redistrictId, AdminLevel dest,
            AdminLevel source, double percent, List<AdminLevelDemography> demosToMerge, List<DiseaseDistroCm> ddCmMerge, List<DiseaseDistroPc> ddPcMerge,
            List<IntvBase> intvMerge, List<ProcessBase> procsMerge)
        {
            double percentMultiplier = (percent / 100);
            // split all demography
            List<DemoDetails> demoDetails = demoRepo.GetAdminLevelDemography(source.Id);
            foreach (var deet in demoDetails)
                SplitDemo(demosToMerge, deet, dest.Id, percentMultiplier, redistrictId, command, connection);
            // split all distros 
            List<DiseaseDistroDetails> dds = diseaseRepo.GetAllForAdminLevel(source.Id);
            foreach (var dd in dds)
                if (dd.DiseaseType == "PC")
                    options.DistrosPc.Add(SplitDdPc(ddPcMerge, dd, dest, percentMultiplier, redistrictId, command, connection));
                else
                    options.DistrosCm.Add(SplitDdCm(ddCmMerge, dd, dest, percentMultiplier, redistrictId, command, connection));
            // split all intvs 
            List<IntvDetails> intvs = intvRepo.GetAllForAdminLevel(source.Id);
            foreach (var intv in intvs)
                options.Intvs.Add(SplitIntv(intvMerge, intv, dest, percentMultiplier, redistrictId, command, connection));
            // split all processes 
            IEnumerable<ProcessDetails> processes = processRepo.GetAllForAdminLevel(source.Id).Where(i => i.TypeId != (int)StaticProcessType.SAEs);
            foreach (var process in processes)
                options.Processes.Add(SplitProcesses(procsMerge, process, dest, percentMultiplier, redistrictId, command, connection));

        }
        
        private AdminLevelDemography SplitDemo(List<AdminLevelDemography> toMerge, DemoDetails details, int destId, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var demography = demoRepo.GetDemoById(details.Id);

            // make new
            var newDemography = Util.DeepClone(demography);
            if (newDemography.AdminLevelId != destId)
            {
                newDemography.Id = 0;
                newDemography.AdminLevelId = destId;
            }
            newDemography.Pop0Month = demography.Pop0Month * multiplier;
            newDemography.PopPsac = demography.PopPsac * multiplier;
            newDemography.Pop5yo = demography.Pop5yo * multiplier;
            newDemography.PopAdult = demography.PopAdult * multiplier;
            newDemography.PopFemale = demography.PopFemale * multiplier;
            newDemography.PopMale = demography.PopMale * multiplier;
            newDemography.TotalPopulation = demography.TotalPopulation * multiplier;
            newDemography.PopSac = demography.PopSac * multiplier;

            // save
            if (toMerge == null)
            {
                demoRepo.SaveAdminDemography(command, connection, newDemography, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, demography.Id, newDemography.Id, IndicatorEntityType.Demo);
            }
            else
                toMerge.Add(newDemography);
            return newDemography;
        }

        private SurveyBase SplitSurveys(SurveyDetails details, AdminLevel source, List<AdminLevelAllocation> dests, int redistrictId, OleDbCommand command, 
            OleDbConnection connection, Dictionary<int, SentinelSite> newSites)
        {
            var oldSurvey = surveyRepo.GetById(details.Id);

            // make new
            var newForm = Util.DeepClone(oldSurvey);
            // save old with only original unit
            oldSurvey.Notes += string.Format(Translations.RedistrictSurveyNote, string.Join(", ", oldSurvey.AdminLevels.Select(s => s.Name).ToArray()));
            oldSurvey.AdminLevels = new List<AdminLevel> { source };
            surveyRepo.SaveSurveyBase(command, connection, oldSurvey, userId);

            // add new units
            // We used to make sure it didn't exist in the destination yet? dont get it, must be split combine... if (!newForm.AdminLevels.Select(a => a.Id).Contains(dest.Id))
            newForm.Id = 0;
            newForm.IsRedistricted = true;
            newForm.AdminLevels.RemoveAll(a => a.Id == source.Id);
            newForm.AdminLevels.AddRange(dests.Select(a => a.Unit));
            if (oldSurvey.SentinelSiteId.HasValue && oldSurvey.SentinelSiteId > 0)
            {
                if (!newSites.ContainsKey(oldSurvey.SentinelSiteId.Value))
                {  
                    var oldSite = surveyRepo.GetSiteById(oldSurvey.SentinelSiteId.Value);
                    CloneSite(source, dests, command, connection, newSites, oldSite);
                }
                newForm.SentinelSiteId = newSites[oldSurvey.SentinelSiteId.Value].Id;
            }

            surveyRepo.SaveSurveyBase(command, connection, newForm, userId);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, oldSurvey.Id, newForm.Id, IndicatorEntityType.Survey);
            
            return newForm;
        }

        private Dictionary<int, SentinelSite> SplitSentinelSites(AdminLevel source, List<AdminLevelAllocation> dests, OleDbCommand command, OleDbConnection connection)
        {
            Dictionary<int, SentinelSite> newSites = new Dictionary<int, SentinelSite>();
            var sites = surveyRepo.GetSitesForAdminLevel(new List<string> { source.Id.ToString() });
            foreach (var site in sites)
            {
                CloneSite(source, dests, command, connection, newSites, site);
            }
            return newSites;
        }

        private void CloneSite(AdminLevel source, List<AdminLevelAllocation> dests, OleDbCommand command, OleDbConnection connection, Dictionary<int, SentinelSite> newSites, SentinelSite site)
        {
            var newSite = Util.DeepClone(site);
            // remove other admin levels
            site.AdminLevels = new List<AdminLevel> { source };
            surveyRepo.Update(site, userId, connection, command);
            // update admin levels to new dests
            newSite.Id = 0;
            newSite.AdminLevels.RemoveAll(a => a.Id == source.Id);
            newSite.AdminLevels.AddRange(dests.Select(a => a.Unit));
            newSites.Add(site.Id, surveyRepo.Insert(newSite, userId, connection, command));
        }

        private DiseaseDistroPc SplitDdPc(List<DiseaseDistroPc> toMerge, DiseaseDistroDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var dd = diseaseRepo.GetDiseaseDistribution(details.Id, details.TypeId);
            var newForm = Util.DeepClone(dd);
            if (newForm.AdminLevelId != dest.Id)
            {
                newForm.Id = 0;
                newForm.IsRedistricted = true;
                newForm.AdminLevelId = dest.Id;
            }
            newForm.IndicatorValues = RedistributeIndicators(newForm.IndicatorValues, multiplier);
            if (toMerge == null)
            {
                diseaseRepo.SavePc(newForm, userId, connection, command);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, dd.Id, newForm.Id, IndicatorEntityType.DiseaseDistribution);
            }
            else
                toMerge.Add(newForm);
            return newForm;
        }

        private DiseaseDistroCm SplitDdCm(List<DiseaseDistroCm> toMerge, DiseaseDistroDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var dd = diseaseRepo.GetDiseaseDistributionCm(details.Id, details.TypeId);
            var newForm = Util.DeepClone(dd);
            if (newForm.AdminLevelId != dest.Id)
            {
                newForm.Id = 0;
                newForm.IsRedistricted = true;
                newForm.AdminLevelId = dest.Id;
            }
            newForm.IndicatorValues = RedistributeIndicators(newForm.IndicatorValues, multiplier);
            if (toMerge == null)
            {
                diseaseRepo.SaveCm(newForm, userId, connection, command);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, dd.Id, newForm.Id, IndicatorEntityType.DiseaseDistribution);
            }
            else
                toMerge.Add(newForm);
            return newForm;
        }

        private IntvBase SplitIntv(List<IntvBase> toMerge, IntvDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var intv = intvRepo.GetById(details.Id);
            var newForm = Util.DeepClone(intv);
            if (newForm.AdminLevelId != dest.Id)
            {
                newForm.Id = 0;
                newForm.IsRedistricted = true;
                newForm.AdminLevelId = dest.Id;
            }
            newForm.IndicatorValues = RedistributeIndicators(intv.IndicatorValues, multiplier);
            if (toMerge == null)
            {
                intvRepo.SaveIntvBase(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, intv.Id, newForm.Id, IndicatorEntityType.Intervention);
            }
            else
                toMerge.Add(newForm);
            return newForm;
        }

        private ProcessBase SplitProcesses(List<ProcessBase> toMerge, ProcessDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var process = processRepo.GetById(details.Id);
            var newForm = Util.DeepClone(process);
            if (newForm.AdminLevelId != dest.Id)
            {
                newForm.Id = 0;
                newForm.IsRedistricted = true;
                newForm.AdminLevelId = dest.Id;
            }
            newForm.IndicatorValues = RedistributeIndicators(process.IndicatorValues, multiplier);
            if (toMerge == null)
            {
                processRepo.Save(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, process.Id, newForm.Id, IndicatorEntityType.Process);
            }
            else
                toMerge.Add(newForm);
            return newForm;
        }
        
        private void SaveSplitSaes(List<ProcessBase> list, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            foreach (var sae in list)
            {
                var newForm = Util.DeepClone(sae);
                newForm.Id = 0;
                newForm.IsRedistricted = true;
                processRepo.Save(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, sae.Id, newForm.Id, IndicatorEntityType.Sae);
            }
        }

        private List<IndicatorValue> RedistributeIndicators(List<IndicatorValue> existing, double percentage)
        {
            List<IndicatorValue> newValues = new List<IndicatorValue>();
            foreach (var ind in existing)
                newValues.Add(IndicatorSplitter.Redistribute(ind, percentage));
            return newValues;
        }
        #endregion
    }


    [Serializable]
    public static class IndicatorSplitter
    {
        public static IndicatorValue Redistribute(IndicatorValue existingInd, double percentage)
        {
            IndicatorValue result = new IndicatorValue { CalcByRedistrict = true };
            result.Indicator = existingInd.Indicator;
            result.IndicatorId = existingInd.IndicatorId;
            if (existingInd.Indicator.RedistrictRuleId == (int)RedistrictingRule.Duplicate)
                result.DynamicValue = existingInd.DynamicValue;
            else if (existingInd.Indicator.RedistrictRuleId == (int)RedistrictingRule.SplitByPercent && existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Number)
                result.DynamicValue = SplitByPercent(existingInd, percentage);
            else // defaultblank/TBD
                result.DynamicValue = "";

            return result;
        }

        private static string SplitByPercent(IndicatorValue existingValue, double percentage)
        {
            double i1 = 0;
            if (!Double.TryParse(existingValue.DynamicValue, out i1))
                return "";

            return (i1 * percentage).ToString();
        }
    }


    [Serializable]
    public static class IndicatorMerger
    {
        public static IndicatorValue Merge(IndicatorValue existingInd, IndicatorValue newInd, List<IndicatorDropdownValue> dropdownOptions,
            IndicatorEntityType entityType)
        {
            newInd.CalcByRedistrict = true;
            newInd.Indicator = existingInd.Indicator;
            newInd.IndicatorId = existingInd.IndicatorId;
            if (existingInd.Indicator.MergeRuleId == (int)MergingRule.CaseFindingStrategy)
                newInd.DynamicValue = MergeCaseFindingStrategy(existingInd, newInd);
            else if ((existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Number || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Year || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Month)
                && (existingInd.Indicator.MergeRuleId == (int)MergingRule.Average || existingInd.Indicator.MergeRuleId == (int)MergingRule.Min ||
                existingInd.Indicator.MergeRuleId == (int)MergingRule.Max || existingInd.Indicator.MergeRuleId == (int)MergingRule.Sum))
                newInd.DynamicValue = MergeNumber(existingInd, newInd, existingInd.Indicator.MergeRuleId);
            else if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Date &&
                (existingInd.Indicator.MergeRuleId == (int)MergingRule.Min || existingInd.Indicator.MergeRuleId == (int)MergingRule.Max))
                newInd.DynamicValue = MergeDate(existingInd, newInd, existingInd.Indicator.MergeRuleId);
            else if (existingInd.Indicator.MergeRuleId == (int)MergingRule.ListAll)
                newInd.DynamicValue = Combine(existingInd, newInd);
            else if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Dropdown &&
                (existingInd.Indicator.MergeRuleId == (int)MergingRule.WorstCase || existingInd.Indicator.MergeRuleId == (int)MergingRule.BestCase || existingInd.Indicator.MergeRuleId == (int)MergingRule.Other))
                newInd.DynamicValue = MergeDropdown(existingInd, newInd, dropdownOptions, entityType);
            else //defaultblank/tbd/leaveblank53/leaveblank59
                newInd.DynamicValue = "";

            return newInd;
        }

        private static string MergeCaseFindingStrategy(IndicatorValue existingInd, IndicatorValue newInd)
        {
            if (string.IsNullOrEmpty(existingInd.DynamicValue) && string.IsNullOrEmpty(newInd.DynamicValue))
                return "";
            if (string.IsNullOrEmpty(existingInd.DynamicValue))
                return newInd.DynamicValue;
            if (string.IsNullOrEmpty(newInd.DynamicValue))
                return existingInd.DynamicValue;

            if (existingInd.DynamicValue == "Passive" || existingInd.DynamicValue == TranslationLookup.GetValue("Passive", "Passive") &&
                newInd.DynamicValue == "Passive" || newInd.DynamicValue == TranslationLookup.GetValue("Passive", "Passive"))
                return existingInd.DynamicValue;
            if (existingInd.DynamicValue == "Active" || existingInd.DynamicValue == TranslationLookup.GetValue("Active", "Active") &&
                newInd.DynamicValue == "Active" || newInd.DynamicValue == TranslationLookup.GetValue("Active", "Active"))
                return existingInd.DynamicValue;

            if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
                return "Combined";
            else
                return existingInd.DynamicValue + ", " + newInd.DynamicValue;
        }

        private static string MergeDate(IndicatorValue existingInd, IndicatorValue newInd, int ruleId)
        {
            if (string.IsNullOrEmpty(existingInd.DynamicValue) && string.IsNullOrEmpty(newInd.DynamicValue))
                return "";
            if (string.IsNullOrEmpty(existingInd.DynamicValue))
                return newInd.DynamicValue;
            if (string.IsNullOrEmpty(newInd.DynamicValue))
                return existingInd.DynamicValue;

            DateTime newDate = DateTime.ParseExact(newInd.DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime existing = DateTime.ParseExact(existingInd.DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            if (ruleId == (int)MergingRule.Max)
            {
                if (existing >= newDate)
                    return existing.ToString("MM/dd/yyyy");
                else
                    return newDate.ToString("MM/dd/yyyy");
            }
            else
            {
                if (newDate <= (DateTime)existing)
                    return newDate.ToString("MM/dd/yyyy");
                else
                    return existing.ToString("MM/dd/yyyy");
            }
        }

        private static string MergeNumber(IndicatorValue existingInd, IndicatorValue newInd, int ruleId)
        {
            double i1 = 0, i2 = 0;
            if (!Double.TryParse(existingInd.DynamicValue, out i1) && !Double.TryParse(newInd.DynamicValue, out i2))
                return "";
            if (!Double.TryParse(existingInd.DynamicValue, out i1))
                return newInd.DynamicValue;
            if (!Double.TryParse(newInd.DynamicValue, out i2))
                return existingInd.DynamicValue;

            if (ruleId == (int)MergingRule.Min)
            {
                if (i1 >= i2)
                    return newInd.DynamicValue;
                else
                    return existingInd.DynamicValue;
            }
            else if (ruleId == (int)MergingRule.Max)
            {
                if (i1 >= i2)
                    return existingInd.DynamicValue;
                else
                    return newInd.DynamicValue;
            }
            else if (ruleId == (int)MergingRule.Average)
            {
                if (i1 == 0)
                    return i2.ToString();
                if (i2 == 0)
                    return i1.ToString();
                return ((i1 + i2) / 2).ToString();
            }
            else // default sum
                return (i1 + i2).ToString();
        }

        private static string MergeDropdown(IndicatorValue existingInd, IndicatorValue newInd, List<IndicatorDropdownValue> dropdownOptions, IndicatorEntityType entityType)
        {
            if (string.IsNullOrEmpty(existingInd.DynamicValue) && string.IsNullOrEmpty(newInd.DynamicValue))
                return "";
            if (string.IsNullOrEmpty(newInd.DynamicValue))
                return existingInd.DynamicValue;
            if (string.IsNullOrEmpty(existingInd.DynamicValue))
                return newInd.DynamicValue;
            // Training category other rule
            if (existingInd.DynamicValue == newInd.DynamicValue)
                return newInd.DynamicValue;

            var ind1option = dropdownOptions.FirstOrDefault(i => i.IndicatorId == newInd.IndicatorId && i.EntityType == entityType
                && i.TranslationKey == newInd.DynamicValue);
            var ind2option = dropdownOptions.FirstOrDefault(i => i.IndicatorId == existingInd.IndicatorId && i.EntityType == entityType
                && i.TranslationKey == existingInd.DynamicValue);
            if (ind1option == null)
                return existingInd.DynamicValue;
            if (ind2option == null)
                return newInd.DynamicValue;

            if (newInd.Indicator.MergeRuleId == (int)MergingRule.BestCase)
            {
                if (ind1option.WeightedValue <= ind2option.WeightedValue)
                    return newInd.DynamicValue;
                else
                    return existingInd.DynamicValue;
            }
            if (newInd.Indicator.MergeRuleId == (int)MergingRule.WorstCase)
            {
                if (ind1option.WeightedValue >= ind2option.WeightedValue)
                    return newInd.DynamicValue;
                else
                    return existingInd.DynamicValue;
            }

            return TranslationLookup.GetValue("NA", "NA"); ;
        }

        private static string Combine(IndicatorValue existingInd, IndicatorValue newInd)
        {
            if (string.IsNullOrEmpty(existingInd.DynamicValue) && string.IsNullOrEmpty(newInd.DynamicValue))
                return null;
            if (string.IsNullOrEmpty(existingInd.DynamicValue))
                return newInd.DynamicValue;
            if (string.IsNullOrEmpty(newInd.DynamicValue))
                return existingInd.DynamicValue;

            if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Multiselect || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.DiseaseMultiselect ||
                existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Partners)
            {
                var vals = existingInd.DynamicValue.Split('|').ToList();
                var newVals = newInd.DynamicValue.Split('|');
                foreach (string newVal in newVals)
                    if (!vals.Contains(newVal))
                        vals.Add(newVal);

                return string.Join("|", vals.ToArray()); 
            }
            else
                return existingInd.DynamicValue + " " + newInd.DynamicValue;
        }
    }
}

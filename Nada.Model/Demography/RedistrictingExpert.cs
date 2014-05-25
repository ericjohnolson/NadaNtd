﻿using System;
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

        private RedistrictingResult DoSplitCombine(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            return new RedistrictingResult();
        }


        #region Merge
        private RedistrictingResult DoMerge(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            int redistrictId = demoRepo.InsertRedistrictingRecord(command, connection, options, userId);

            Dictionary<int, List<DemoDetails>> demographyToMerge = new Dictionary<int, List<DemoDetails>>();
            Dictionary<string, List<DiseaseDistroDetails>> ddToMerge = new Dictionary<string, List<DiseaseDistroDetails>>();
            Dictionary<string, List<IntvBase>> intvToMerge = new Dictionary<string, List<IntvBase>>();
            Dictionary<string, List<ProcessBase>> trainingToMerge = new Dictionary<string, List<ProcessBase>>();
            List<ProcessDetails> saes = new List<ProcessDetails>();
            List<SurveyDetails> surveys = new List<SurveyDetails>();
            Dictionary<string, List<ProcessBase>> scmToMerge = new Dictionary<string, List<ProcessBase>>();

            #region Get forms to merge
            foreach (var source in options.MergeSources)
            {
                demoRepo.InsertRedistrictUnit(command, connection, userId, source, redistrictId, RedistrictingRelationship.Mother, 0);
                //Demography forms – merge for the most recent in the year only
                Dictionary<int, DemoDetails> recentDemo = new Dictionary<int, DemoDetails>();
                List<DemoDetails> demos = demoRepo.GetAdminLevelDemography(source.Id);
                foreach (var d in demos)
                {
                    int year = Util.GetYearReported(options.YearStartMonth, d.DateReported);
                    if (!recentDemo.ContainsKey(year))
                        recentDemo.Add(year, d);
                    else if (recentDemo.ContainsKey(year) && recentDemo[year].DateReported < d.DateReported)
                        recentDemo[year] = d;
                }
                foreach (int y in recentDemo.Keys)
                {
                    if (demographyToMerge.ContainsKey(y))
                        demographyToMerge[y].Add(recentDemo[y]);
                    else
                        demographyToMerge.Add(y, new List<DemoDetails> { recentDemo[y] });
                }
                //DD forms – merge for the most recent in the year only (KEY: Year_Typeid)
                Dictionary<string, DiseaseDistroDetails> recentDd = new Dictionary<string, DiseaseDistroDetails>();
                List<DiseaseDistroDetails> dds = diseaseRepo.GetAllForAdminLevel(source.Id);
                foreach (var d in dds)
                {
                    string key = Util.GetYearReported(options.YearStartMonth, d.DateReported).ToString() + "_" + d.TypeId;
                    if (!recentDd.ContainsKey(key))
                        recentDd.Add(key, d);
                    else if (recentDd.ContainsKey(key) && recentDd[key].DateReported < d.DateReported)
                        recentDd[key] = d;
                }
                foreach (string k in recentDd.Keys)
                {
                    if (ddToMerge.ContainsKey(k))
                        ddToMerge[k].Add(recentDd[k]);
                    else
                        ddToMerge.Add(k, new List<DiseaseDistroDetails> { recentDd[k] });
                }
                //Interventions forms – merge for all in the year with the same round (KEY "YEAR_TYpeId_ROUND")
                List<IntvDetails> intvs = intvRepo.GetAllForAdminLevel(source.Id);
                foreach (var intv in intvs)
                {
                    string key = Util.GetYearReported(options.YearStartMonth, intv.DateReported).ToString() + "_" + intv.TypeId;
                    IntvBase form = intvRepo.GetById(intv.Id);
                    var roundInd = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "PcIntvRoundNumber");
                    if (roundInd != null)
                        key += ("_" + roundInd.DynamicValue);
                    if (intvToMerge.ContainsKey(key))
                        intvToMerge[key].Add(form);
                    else
                        intvToMerge.Add(key, new List<IntvBase> { form });
                }
                // Surveys 
                List<SurveyDetails> survs = surveyRepo.GetAllForAdminLevel(source.Id);
                surveys.AddRange(survs);
                //PC training - merge for all in the year with the same category (KEY "YEAR_CAT")
                List<ProcessDetails> allProcs = processRepo.GetAllForAdminLevel(source.Id);
                foreach (var proc in allProcs.Where(p => p.TypeId == (int)StaticProcessType.PcTraining))
                {
                    string key = Util.GetYearReported(options.YearStartMonth, proc.DateReported).ToString();
                    ProcessBase form = processRepo.GetById(proc.Id);
                    var cat = form.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "PCTrainTrainingCategory");
                    if (cat != null)
                        key += ("_" + cat.DynamicValue);
                    if (!trainingToMerge.ContainsKey(key))
                        trainingToMerge.Add(key, new List<ProcessBase> { form });
                    else
                        trainingToMerge[key].Add(form);
                }
                //SAE – no merge - copy all separately to merged admin unit. 
                saes.AddRange(allProcs.Where(p => p.TypeId == (int)StaticProcessType.SAEs));
                //SCM –merge for all in the year with the same drug and unit (KEY "YEAR_DRUG_UNIT")
                foreach (var proc in allProcs.Where(p => p.TypeId == (int)StaticProcessType.SCM))
                {
                    string key = Util.GetYearReported(options.YearStartMonth, proc.DateReported).ToString();
                    ProcessBase form = processRepo.GetById(proc.Id);
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
                //Surveys forms – TBD still
            }
            #endregion

            // Add into new unit
            demoRepo.InsertRedistrictUnit(command, connection, userId, options.MergeDestination, redistrictId, RedistrictingRelationship.Daughter, 0);
            foreach (var val in demographyToMerge.Values)
                MergeDemo(val,  options.MergeDestination.Id, redistrictId, command, connection);
            foreach (var val in ddToMerge.Values)
                if (val.First().DiseaseType == "PC")
                    options.DistrosPc.Add(MergeDdPc(val, options.MergeDestination.Id, redistrictId, command, connection));
                else
                    options.DistrosCm.Add(MergeDdCm(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in intvToMerge.Values)
                options.Intvs.Add(MergeIntv(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in trainingToMerge.Values)
                options.Processes.Add(MergeProcess(val, options.MergeDestination.Id, redistrictId, command, connection));
            foreach (var val in scmToMerge.Values)
                options.Processes.Add(MergeProcess(val, options.MergeDestination.Id, redistrictId, command, connection));
            CopyAllProcesses(saes, options.MergeDestination.Id, redistrictId, command, connection);
            CopyAllSurveys(surveys, options.MergeDestination, redistrictId, command, connection);

            return new RedistrictingResult();
        }

        private void MergeDemo(List<DemoDetails> details,  int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            AdminLevelDemography newDemography = null;
            foreach (var d in details)
            {
                var demography = demoRepo.GetDemoById(d.Id);
                if(newDemography == null)
                    newDemography = Util.DeepClone(demography);
                newDemography.Id = 0;
                newDemography.AdminLevelId = destId;
                newDemography.Pop0Month += demography.Pop0Month;
                newDemography.PopPsac += demography.PopPsac ;
                newDemography.Pop5yo += demography.Pop5yo ;
                newDemography.PopAdult += demography.PopAdult ;
                newDemography.PopFemale += demography.PopFemale ;
                newDemography.PopMale += demography.PopMale ;
                newDemography.TotalPopulation += demography.TotalPopulation ;
                newDemography.PopSac += demography.PopSac ;
            }

            // save
            demoRepo.SaveAdminDemography(command, connection, newDemography, userId);
            foreach(var d in details)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, d.Id, newDemography.Id, IndicatorEntityType.Demo);
        }

        private IntvBase MergeIntv(List<IntvBase> intvs, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            IntvBase newIntv = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var intv in intvs)
            {
                if (newIntv == null)
                    newIntv = Util.DeepClone(intv);
                // Do notes newSurvey.Notes
                newIntv.Id = 0;
                newIntv.AdminLevelId = destId;
                MergeIndicators(intv.IndicatorValues, newInds, IndicatorEntityType.Intervention, newIntv.IntvType.IndicatorDropdownValues);
            }
            newIntv.IndicatorValues = newInds.Values.ToList();
            // save
            intvRepo.SaveIntvBase(command, connection, newIntv, userId);
            foreach (var i in intvs)
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newIntv.Id, IndicatorEntityType.Intervention);
            return newIntv;
        }

        private DiseaseDistroCm MergeDdCm(List<DiseaseDistroDetails> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            DiseaseDistroCm newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in toMerge)
            {
                var oldForm = diseaseRepo.GetDiseaseDistributionCm(form.Id, form.TypeId);
                if (newForm == null)
                    newForm = Util.DeepClone(oldForm);

                newForm.Id = 0;
                newForm.AdminLevelId = destId;
                MergeIndicators(oldForm.IndicatorValues, newInds, IndicatorEntityType.DiseaseDistribution, newForm.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            diseaseRepo.SaveCm(newForm, userId, connection, command);
            foreach (var i in toMerge)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.DiseaseDistribution);
            return newForm;
        }

        private DiseaseDistroPc MergeDdPc(List<DiseaseDistroDetails> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            DiseaseDistroPc newForm = null;
            Dictionary<int, IndicatorValue> newInds = new Dictionary<int, IndicatorValue>();
            foreach (var form in toMerge)
            {
                var oldForm = diseaseRepo.GetDiseaseDistribution(form.Id, form.TypeId);
                if (newForm == null)
                    newForm = Util.DeepClone(oldForm);

                newForm.Id = 0;
                newForm.AdminLevelId = destId;
                MergeIndicators(oldForm.IndicatorValues, newInds, IndicatorEntityType.DiseaseDistribution, newForm.IndicatorDropdownValues);
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
                    newForm = Util.DeepClone(form);

                newForm.Id = 0;
                newForm.AdminLevelId = destId;
                MergeIndicators(form.IndicatorValues, newInds, IndicatorEntityType.Process, newForm.ProcessType.IndicatorDropdownValues);
            }
            newForm.IndicatorValues = newInds.Values.ToList();
            // save
            processRepo.Save(command, connection, newForm, userId);
            foreach (var i in toMerge)
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newForm.Id, IndicatorEntityType.Process);
            return newForm;
        }

        private void CopyAllProcesses(List<ProcessDetails> toMerge, int destId, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            foreach (var form in toMerge)
            {
                var oldForm = processRepo.GetById(form.Id);
                ProcessBase newForm = Util.DeepClone(oldForm);

                newForm.Id = 0;
                newForm.AdminLevelId = destId;
                processRepo.Save(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, form.Id, newForm.Id, IndicatorEntityType.Process);
            }
        }

        private void CopyAllSurveys(List<SurveyDetails> toMerge, AdminLevel dest, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            foreach (var form in toMerge)
            {
                var oldForm = surveyRepo.GetById(form.Id);
                SurveyBase newForm = Util.DeepClone(oldForm);

                newForm.Id = 0;
                newForm.AdminLevels = new List<AdminLevel> { dest };
                surveyRepo.SaveSurveyBase(command, connection, newForm, userId);
                demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, form.Id, newForm.Id, IndicatorEntityType.Survey);
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
        #endregion

        #region Split
        private RedistrictingResult DoSplit(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            int redistrictId = demoRepo.InsertRedistrictingRecord(command, connection, options, userId);
            demoRepo.InsertRedistrictUnit(command, connection, userId, options.Source, redistrictId, RedistrictingRelationship.Mother, 0);
            foreach (var dest in options.SplitDestinations)
            {
                demoRepo.InsertRedistrictUnit(command, connection, userId, dest.Unit, redistrictId, RedistrictingRelationship.Daughter, dest.Percent);
                double percentMultiplier = (dest.Percent / 100);
                // split all demography
                List<DemoDetails> demoDetails = demoRepo.GetAdminLevelDemography(options.Source.Id);
                foreach (var deet in demoDetails)
                    SplitDemo(deet, dest.Unit.Id, percentMultiplier, redistrictId, command, connection);
                // split all surveys 
                List<SurveyDetails> surveys = surveyRepo.GetAllForAdminLevel(options.Source.Id);
                foreach (var survey in surveys)
                    options.Surveys.Add(SplitSurveys(survey, dest.Unit, percentMultiplier, redistrictId, command, connection));
                // split all distros 
                List<DiseaseDistroDetails> dds = diseaseRepo.GetAllForAdminLevel(options.Source.Id);
                foreach (var dd in dds)
                    if (dd.DiseaseType == "PC")
                        options.DistrosPc.Add(SplitDdPc(dd, dest.Unit, percentMultiplier, redistrictId, command, connection));
                    else
                        options.DistrosCm.Add(SplitDdCm(dd, dest.Unit, percentMultiplier, redistrictId, command, connection));
                // split all intvs 
                List<IntvDetails> intvs = intvRepo.GetAllForAdminLevel(options.Source.Id);
                foreach (var intv in intvs)
                    options.Intvs.Add(SplitIntv(intv, dest.Unit, percentMultiplier, redistrictId, command, connection));
                // split all surveys 
                List<ProcessDetails> processes = processRepo.GetAllForAdminLevel(options.Source.Id);
                foreach (var process in processes)
                    options.Processes.Add(SplitProcesses(process, dest.Unit, percentMultiplier, redistrictId, command, connection));

            }
            return new RedistrictingResult();
        }

        private void SplitDemo(DemoDetails details, int destId, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var demography = demoRepo.GetDemoById(details.Id);

            // make new
            var newDemography = Util.DeepClone(demography);
            newDemography.Id = 0;
            newDemography.AdminLevelId = destId;
            newDemography.Pop0Month = demography.Pop0Month * multiplier;
            newDemography.PopPsac = demography.PopPsac * multiplier;
            newDemography.Pop5yo = demography.Pop5yo * multiplier;
            newDemography.PopAdult = demography.PopAdult * multiplier;
            newDemography.PopFemale = demography.PopFemale * multiplier;
            newDemography.PopMale = demography.PopMale * multiplier;
            newDemography.TotalPopulation = demography.TotalPopulation * multiplier;
            newDemography.PopSac = demography.PopSac * multiplier;

            // save
            demoRepo.SaveAdminDemography(command, connection, newDemography, userId);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, demography.Id, newDemography.Id, IndicatorEntityType.Demo);
        }

        private SurveyBase SplitSurveys(SurveyDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var survey = surveyRepo.GetById(details.Id);
            // make new
            var newSurvey = Util.DeepClone(survey);
            // Do notes newSurvey.Notes
            newSurvey.Id = 0;
            newSurvey.AdminLevels = new List<AdminLevel> { dest };
            newSurvey.IndicatorValues = RedistributeIndicators(survey.IndicatorValues, multiplier);
            // save
            surveyRepo.SaveSurveyBase(command, connection, newSurvey, userId);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, survey.Id, newSurvey.Id, IndicatorEntityType.Survey);
            return newSurvey;
        }

        private DiseaseDistroPc SplitDdPc(DiseaseDistroDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {

            var dd = diseaseRepo.GetDiseaseDistribution(details.Id, details.TypeId);
            var newDd = Util.DeepClone(dd);
            newDd.Id = 0;
            newDd.AdminLevelId = dest.Id;
            newDd.IndicatorValues = RedistributeIndicators(newDd.IndicatorValues, multiplier);
            diseaseRepo.SavePc(newDd, userId, connection, command);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, dd.Id, newDd.Id, IndicatorEntityType.DiseaseDistribution);
            return newDd;
        }

        private DiseaseDistroCm SplitDdCm(DiseaseDistroDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var dd = diseaseRepo.GetDiseaseDistributionCm(details.Id, details.TypeId);
            var newDd = Util.DeepClone(dd);
            newDd.Id = 0;
            newDd.AdminLevelId = dest.Id;
            newDd.IndicatorValues = RedistributeIndicators(newDd.IndicatorValues, multiplier);
            diseaseRepo.SaveCm(newDd, userId, connection, command);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, dd.Id, newDd.Id, IndicatorEntityType.DiseaseDistribution);
            return newDd;
        }


        private IntvBase SplitIntv(IntvDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var intv = intvRepo.GetById(details.Id);
            // make new
            var newIntv = Util.DeepClone(intv);
            // Do notes newSurvey.Notes
            newIntv.Id = 0;
            newIntv.AdminLevelId = dest.Id;
            newIntv.IndicatorValues = RedistributeIndicators(intv.IndicatorValues, multiplier);
            // save
            intvRepo.SaveIntvBase(command, connection, newIntv, userId);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, intv.Id, newIntv.Id, IndicatorEntityType.Intervention);
            return newIntv;
        }

        private ProcessBase SplitProcesses(ProcessDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
        {
            var process = processRepo.GetById(details.Id);
            // make new
            var newProcess = Util.DeepClone(process);
            // Do notes newSurvey.Notes
            newProcess.Id = 0;
            newProcess.AdminLevelId = dest.Id;
            newProcess.IndicatorValues = RedistributeIndicators(process.IndicatorValues, multiplier);
            // save
            processRepo.Save(command, connection, newProcess, userId);
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, process.Id, newProcess.Id, IndicatorEntityType.Process);
            return newProcess;
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


    //public enum MergingRule
    //{
    //    Min = 56,
    //    Max = 55,
    //    ListAll = 54,
    //    Sum = 57,
    //    WorstCase = 58, // Max number of weighting
    //    BestCase = 51, // min number of weighting
    //    Average = 50,
    //}
    [Serializable]
    public static class IndicatorMerger
    {
        public static IndicatorValue Merge(IndicatorValue existingInd, IndicatorValue newInd, List<IndicatorDropdownValue> dropdownOptions, 
            IndicatorEntityType entityType)
        {
            newInd.CalcByRedistrict = true;
            newInd.Indicator = existingInd.Indicator;
            newInd.IndicatorId = existingInd.IndicatorId;
            if ((existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Number || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Year || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Month) 
                && (existingInd.Indicator.MergeRuleId == (int)MergingRule.Average || existingInd.Indicator.MergeRuleId == (int)MergingRule.Min || 
                existingInd.Indicator.MergeRuleId == (int)MergingRule.Max || existingInd.Indicator.MergeRuleId == (int)MergingRule.Sum))
                newInd.DynamicValue = MergeNumber(existingInd, newInd, existingInd.Indicator.MergeRuleId);
            else if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Date &&
                (existingInd.Indicator.MergeRuleId == (int)MergingRule.Min || existingInd.Indicator.MergeRuleId == (int)MergingRule.Max))
                newInd.DynamicValue = MergeDate(existingInd, newInd, existingInd.Indicator.MergeRuleId);
            else if (existingInd.Indicator.MergeRuleId == (int)MergingRule.ListAll)
                newInd.DynamicValue = Combine(existingInd, newInd);
            else if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Dropdown &&
                (existingInd.Indicator.MergeRuleId == (int)MergingRule.WorstCase || existingInd.Indicator.MergeRuleId == (int)MergingRule.BestCase))
                newInd.DynamicValue = MergeDropdown(existingInd, newInd, dropdownOptions, entityType);
            else //defaultblank/tbd/leaveblank53/leaveblank59
                newInd.DynamicValue = "";

            return newInd;
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
                return ((i1 + i2)/2).ToString();
            }
            else
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

            if(existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Multiselect || existingInd.Indicator.DataTypeId == (int)IndicatorDataType.DiseaseMultiselect ||
                existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Partners)
                return existingInd.DynamicValue + "|" + newInd.DynamicValue;
            else
                return existingInd.DynamicValue + " " + newInd.DynamicValue;
        }
    }
}

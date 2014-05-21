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
            //foreach (var val in ddToMerge.Values)
            //    if (val.First().DiseaseType == "PC")
            //        options.DistrosPc.Add(SplitDdPc(dd, dest.Unit, percentMultiplier, redistrictId, command, connection));
            //    else
            //        options.DistrosCm.Add(SplitDdCm(dd, dest.Unit, percentMultiplier, redistrictId, command, connection));
            foreach (var val in intvToMerge.Values)
                options.Intvs.Add(MergeIntv(val, options.MergeDestination.Id, redistrictId, command, connection));
            //foreach (var val in trainingToMerge.Values)
            //    options.Processes.Add(SplitProcesses(process, dest.Unit, percentMultiplier, redistrictId, command, connection));
            //foreach (var val in scmToMerge.Values)
            //    options.Processes.Add(SplitProcesses(process, dest.Unit, percentMultiplier, redistrictId, command, connection));
            //foreach(var val in saes)
            //    blah

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
            foreach (var intv in intvs)
            {
                if (newIntv == null)
                    newIntv = Util.DeepClone(intv);
                // Do notes newSurvey.Notes
                newIntv.Id = 0;
                newIntv.AdminLevelId = destId;
                newIntv.IndicatorValues = MergeIndicators(intv.IndicatorValues, newIntv.IndicatorValues);
            }
            // save
            intvRepo.SaveIntvBase(command, connection, newIntv, userId);
            foreach (var i in intvs)
            demoRepo.InsertRedistrictForm(command, connection, userId, redistrictId, i.Id, newIntv.Id, IndicatorEntityType.Intervention);
            return newIntv;
        }

        private List<IndicatorValue> MergeIndicators(List<IndicatorValue> existing, List<IndicatorValue> newValues)
        {
            //foreach (var ind in existing)
            //    newValues.Add(IndicatorRedistributor.Redistribute(ind, percentage));
            return existing;
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
                newValues.Add(IndicatorRedistributor.Redistribute(ind, percentage));
            return newValues;
        }

        #endregion
    }

    [Serializable]
    public static class IndicatorRedistributor
    {
        public static IndicatorValue Redistribute(IndicatorValue existingInd, double percentage)
        {
            IndicatorValue result = new IndicatorValue { CalcByRedistrict = true };
            result.Indicator = existingInd.Indicator;
            result.IndicatorId = existingInd.IndicatorId;
            if (existingInd.Indicator.RedistrictRuleId == (int)RedistrictingRule.Duplicate)
                result.DynamicValue = existingInd.DynamicValue;
            else if (existingInd.Indicator.RedistrictRuleId == (int)RedistrictingRule.Blank)
                result.DynamicValue = "";
            else if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Number)
                result.DynamicValue = RedistributeDouble(existingInd, percentage);

            return result;
        }

        private static string RedistributeDouble(IndicatorValue existingValue, double percentage)
        {
            double i1 = 0;
            if (!Double.TryParse(existingValue.DynamicValue, out i1))
                return "";

            if (existingValue.Indicator.RedistrictRuleId == (int)RedistrictingRule.SplitByPercent)
                return (i1 * percentage).ToString();

            return i1.ToString();
        }

        public static object ParseValue(AggregateIndicator ind)
        {
            if (ind.Value == null)
                return "";

            if (ind.DataType == (int)IndicatorDataType.Number)
                return Double.Parse(ind.Value);
            else if (ind.DataType == (int)IndicatorDataType.Date)
                return DateTime.ParseExact(ind.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                return ind.Value;
        }

        private static string AggregateDate(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            if (ind1.AggType == (int)IndicatorAggType.Sum)
                return DateTime.MinValue.ToString("MM/dd/yyyy");
            if (string.IsNullOrEmpty(ind1.Value))
                return DateTime.MinValue.ToString("MM/dd/yyyy");

            DateTime dt = DateTime.ParseExact(ind1.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime existing = DateTime.ParseExact(existingValue.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            if (ind1.AggType == (int)IndicatorAggType.Min)
                if (dt >= (DateTime)existing)
                    return existing.ToString("MM/dd/yyyy");
                else
                    return dt.ToString("MM/dd/yyyy");
            if (ind1.AggType == (int)IndicatorAggType.Max)
                if (dt >= (DateTime)existing)
                    return dt.ToString("MM/dd/yyyy");
                else
                    return existing.ToString("MM/dd/yyyy");
            return dt.ToString("MM/dd/yyyy");
        }

        private static string AggregateString(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            if (ind1.AggType == (int)IndicatorAggType.Combine)
                return existingValue.Value + ", " + ind1.Value;
            else if (ind1.AggType == (int)IndicatorAggType.None)
                return Translations.NA;
            return "Invalid Aggregation Rule or Data Type";
        }



        private static string AggregateDropdown(AggregateIndicator ind1, AggregateIndicator existingValue)
        {
            return ind1.Value;
            //var ind2 = (AggregateIndicator)existingValue;
            //if (ind1.AggType == (int)IndicatorAggType.Min)
            //    if (ind1.WeightedV)
            //        return (AggregateIndicator)existingValue;
            //    else
            //        return dt;
            //if (ind1.AggType == (int)IndicatorAggType.Max)
            //    if (dt >= (AggregateIndicator)existingValue)
            //        return dt;
            //    else
            //        return (AggregateIndicator)existingValue;

            //return dt;
        }
    }
}

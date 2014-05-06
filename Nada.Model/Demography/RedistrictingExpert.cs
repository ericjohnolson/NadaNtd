using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
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
                    RedistributeDemography(deet, options.Source.Id, dest.Unit.Id, percentMultiplier, redistrictId, command, connection);
                // split all surveys 
                List<SurveyDetails> surveys = surveyRepo.GetAllForAdminLevel(options.Source.Id);
                foreach (var survey in surveys)
                    RedistributeSurvey(survey, dest.Unit, percentMultiplier, redistrictId, command, connection);
                
            }
            return new RedistrictingResult();
        }


        private RedistrictingResult DoSplitCombine(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            return new RedistrictingResult();
        }
        private RedistrictingResult DoMerge(RedistrictingOptions options, OleDbCommand command, OleDbConnection connection)
        {
            return new RedistrictingResult();
        }

        #region helpers
        private void RedistributeDemography(DemoDetails details, int sourceId, int destId, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
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

        private void RedistributeSurvey(SurveyDetails details, AdminLevel dest, double multiplier, int redistrictId, OleDbCommand command, OleDbConnection connection)
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
            IndicatorValue result = new IndicatorValue();
            result.Indicator = existingInd.Indicator;
            result.IndicatorId = existingInd.IndicatorId;
            if (existingInd.Indicator.RedistrictRuleId == (int)RedistrictingRule.None)
                result.DynamicValue = existingInd.DynamicValue;
            if (existingInd.Indicator.DataTypeId == (int)IndicatorDataType.Number)
                result.DynamicValue = RedistributeDouble(existingInd, percentage);

            //else if (ind.AggType == (int)IndicatorAggType.Combine && ind.DataType == (int)IndicatorDataType.Dropdown)
            //    result.Value = existingValue + ", " + TranslationLookup.GetValue(ind.Value, ind.Value);
            //else if (ind.AggType == (int)IndicatorAggType.Combine)
            //    result.Value = existingValue + ", " + ind.Value;
            //else if (ind.DataType == (int)IndicatorDataType.Number)
            //    result.Value = AggregateDouble(ind, existingValue);
            //else if (ind.DataType == (int)IndicatorDataType.Date)
            //    result.Value = AggregateDate(ind, existingValue);
            //else if (ind.DataType == (int)IndicatorDataType.Dropdown)
            //    result.Value = AggregateDropdown(ind, existingValue);
            //else
            //    result.Value = AggregateString(ind, existingValue);

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

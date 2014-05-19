using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Survey;

namespace Nada.Model.Repositories
{
    public class SurveyRepository : RepositoryBase
    {
        #region Surveys
        public List<SurveyDetails> GetAllForAdminLevel(int adminLevel)
        {
            List<SurveyDetails> surveys = new List<SurveyDetails>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Surveys.ID, 
                        SurveyTypes.SurveyTypeName, 
                        Diseases.DisplayName as DiseaseName, 
                        Diseases.DiseaseType, 
                        Surveys.SurveyTypeId, 
                        Surveys.DateReported,
                        Surveys.StartDate, 
                        Surveys.EndDate, 
                        Surveys.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName
                        FROM (((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN aspnet_Users on Surveys.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN Surveys_to_AdminLevels on Surveys.Id = Surveys_to_AdminLevels.SurveyId) 
                            INNER JOIN AdminLevels on AdminLevels.Id = Surveys_to_AdminLevels.AdminLevelId) 
                            INNER JOIN Diseases on SurveyTypes.DiseaseId = Diseases.ID) 
                        WHERE Surveys_to_AdminLevels.AdminLevelId=@AdminLevelId and Surveys.IsDeleted = 0 
                        ORDER BY Surveys.EndDate DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            surveys.Add(new SurveyDetails
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                TypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("SurveyTypeName"), reader.GetValueOrDefault<string>("SurveyTypeName")),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                TypeId = reader.GetValueOrDefault<int>("SurveyTypeId"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                DateReported = reader.GetValueOrDefault<DateTime>("DateReported"),
                                StartDate = reader.GetValueOrDefault<DateTime>("StartDate"),
                                EndDate = reader.GetValueOrDefault<DateTime>("EndDate"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                                UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString()

                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return surveys;
        }

        public List<SurveyBase> GetByTypeForDateRange(List<int> surveyTypes, DateTime start, DateTime end)
        {
            List<SurveyBase> surveys = new List<SurveyBase>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Surveys.ID 
                        FROM ((((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN aspnet_Users on Surveys.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN Surveys_to_AdminLevels on Surveys.Id = Surveys_to_AdminLevels.SurveyId) 
                            INNER JOIN AdminLevels on AdminLevels.Id = Surveys_to_AdminLevels.AdminLevelId) 
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.Id) 
                            INNER JOIN Diseases on SurveyTypes.DiseaseId = Diseases.ID) 
                        WHERE AdminLevelTypes.IsDistrict=-1 and Surveys.IsDeleted = 0 and Surveys.SurveyTypeId in (" 
                        + string.Join(",", surveyTypes.Select(i => i.ToString()).ToArray()) + ") " +
                        @" and Surveys.DateReported >= @StartDate and Surveys.DateReported <= @EndDate 
                        ORDER BY AdminLevels.DisplayName", connection);
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@StartDate", start));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@EndDate", end));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            surveys.Add(GetSurvey<SurveyBase>(command, connection, reader.GetValueOrDefault<int>("ID")));
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return surveys;
        }

        public void Delete(SurveyDetails survey, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE Surveys SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", survey.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public SurveyBase GetById(int id)
        {
            SurveyBase survey = null;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;
                    survey = GetSurvey<SurveyBase>(command, connection, id);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return survey;
        }

        public SurveyBase GetSurveyByAdminLevelYear(int adminLevel, int yearOfReporting)
        {
            if (yearOfReporting <= 0 || adminLevel <= 0)
                return null;
            int surveyId = -1;

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {

                    OleDbCommand command = null;
                    command = new OleDbCommand(@"Select Surveys.ID as sid FROM 
                        Surveys
                        WHERE Surveys.AdminLevelId=@adminlevelId AND Surveys.DateReported >= @StartDate AND Surveys.DateReported < @EndDate", connection);
                    command.Parameters.Add(new OleDbParameter("@adminlevelId", adminLevel));
                    command.Parameters.Add(new OleDbParameter("@StartDate", new DateTime(yearOfReporting, 1, 1)));
                    command.Parameters.Add(new OleDbParameter("@EndDate", new DateTime(yearOfReporting + 1, 1, 1)));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            surveyId = reader.GetValueOrDefault<int>("sid");
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                if (surveyId <= 0)
                    return null;

                return GetById(surveyId);
            }
        }

        public T GetSurvey<T>(OleDbCommand command, OleDbConnection connection, int surveyId) where T : SurveyBase
        {
            var survey = (T)Activator.CreateInstance(typeof(T));
            int surveyTypeId = 0;

            try
            {
                command = new OleDbCommand(@"Select Surveys.StartDate, Surveys.EndDate, Surveys.DateReported, 
                        SiteType, SpotCheckName, SpotCheckLat, SpotCheckLng, SentinelSiteId,
                        Surveys.Notes, Surveys.UpdatedById, Surveys.UpdatedAt, aspnet_Users.UserName, 
                        Surveys.SurveyTypeId, created.UserName as CreatedBy, Surveys.CreatedAt
                        FROM ((Surveys INNER JOIN aspnet_Users on Surveys.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on Surveys.CreatedById = created.UserId)
                        WHERE Surveys.ID=@id", connection);
                command.Parameters.Add(new OleDbParameter("@id", surveyId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        survey.Id = surveyId;
                        survey.StartDate = reader.GetValueOrDefault<DateTime>("StartDate");
                        survey.EndDate = reader.GetValueOrDefault<DateTime>("EndDate");
                        survey.DateReported = reader.GetValueOrDefault<DateTime>("DateReported");
                        survey.SiteType = reader.GetValueOrDefault<string>("SiteType");
                        survey.SpotCheckName = reader.GetValueOrDefault<string>("SpotCheckName");
                        survey.Lat = reader.GetValueOrDefault<Nullable<double>>("SpotCheckLat");
                        survey.Lng = reader.GetValueOrDefault<Nullable<double>>("SpotCheckLng");
                        survey.SentinelSiteId = reader.GetValueOrDefault<Nullable<int>>("SentinelSiteId");
                        survey.Notes = reader.GetValueOrDefault<string>("Notes");
                        survey.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                        survey.UpdatedBy = GetAuditInfo(reader);
                        surveyTypeId = reader.GetValueOrDefault<int>("SurveyTypeId");
                    }
                    reader.Close();
                }
                DemoRepository demo = new DemoRepository();
                command = new OleDbCommand(@"Select AdminLevelId FROM Surveys_to_AdminLevels WHERE SurveyId=@id", connection);
                command.Parameters.Add(new OleDbParameter("@id", surveyId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        survey.AdminLevels.Add(demo.GetAdminLevelById(reader.GetValueOrDefault<int>("AdminLevelId")));
                    }
                    reader.Close();
                }
                survey.TypeOfSurvey = GetSurveyType(surveyTypeId);
                GetSurveyIndicatorValues(connection, survey);
                survey.MapIndicatorsToProperties();
            }
            catch (Exception)
            {
                throw;
            }

            return survey;
        }

        public void Save(List<SurveyBase> import, int userId)
        {
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

                    foreach (var survey in import)
                        SaveSurveyBase(command, connection, survey, userId);

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
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
                    throw;
                }
            }
        }

        public void SaveSurvey(SurveyBase survey, int userId)
        {
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

                    SaveSurveyBase(command, connection, survey, userId);

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
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
                    throw;
                }
            }
        }

        public bool CopySentinelSiteSurvey(SurveyBase survey, int userId)
        {
            SurveyBase copy = null;
            if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping &&
                survey.IndicatorValues.FirstOrDefault(i => i.IndicatorId == 273).DynamicValue == Translations.YesSentinelSite)
            {
                copy = survey.CreateCopy((int)StaticSurveyType.LfSentinel, 104, 105, "LFSurNumberOfRoundsOfPcCompletedPriorToS", "LFSurSurveyTiming");
                copy.SentinelSiteId = ParseSentinelSite(survey, 272, 274, 275, userId);
            }
            else if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.SchMapping &&
                survey.IndicatorValues.FirstOrDefault(i => i.IndicatorId == 322).DynamicValue == Translations.YesSentinelSite)
            {
                copy = survey.CreateCopy((int)StaticSurveyType.SchistoSentinel, 151, 152, "SCHSurNumberOfRoundsOfPcCompletedPriorTo", "SCHSurSurveyTiming");
                copy.SentinelSiteId = ParseSentinelSite(survey, 321, 323, 324, userId);
            }
            else if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.SthMapping &&
                survey.IndicatorValues.FirstOrDefault(i => i.IndicatorId == 347).DynamicValue == Translations.YesSentinelSite)
            {
                copy = survey.CreateCopy((int)StaticSurveyType.SthSentinel, 173, 174, "STHSurNumberOfRoundsOfPcCompletedPriorTo", "STHSurSurveyTiming");
                copy.SentinelSiteId = ParseSentinelSite(survey, 346, 348, 349, userId);
            }
            else
                return false;

            MapDynamic(survey, copy, userId);
            SaveSurvey(copy, userId);
            return true;
        }

        private void MapDynamic(SurveyBase orig, SurveyBase copy, int userId)
        {
            var mappingDict = CreateMappingDictionary();
            foreach (var val in orig.IndicatorValues)
            {
                if (!mappingDict.ContainsKey(val.IndicatorId))
                    continue;
                Indicator ind = copy.TypeOfSurvey.Indicators.Values.FirstOrDefault(i => i.Id == mappingDict[val.IndicatorId]);
                if (ind == null)
                    continue;
                copy.IndicatorValues.Add(new IndicatorValue { IndicatorId = ind.Id, Indicator = ind, DynamicValue = val.DynamicValue });

                if (ind.CanAddValues)
                {
                    List<string> selectedValues = new List<string>();
                    if (ind.DataTypeId == (int)IndicatorDataType.Multiselect)
                        selectedValues = val.DynamicValue.Split('|').ToList();
                    else
                        selectedValues.Add(val.DynamicValue);

                    foreach (string v in selectedValues)
                    {
                        var ddVal = copy.TypeOfSurvey.IndicatorDropdownValues.FirstOrDefault(i => i.DisplayName == v && i.IndicatorId == ind.Id);
                        if (ddVal == null)
                            Save(new IndicatorDropdownValue { IndicatorId = ind.Id, DisplayName = v, EntityType = IndicatorEntityType.Survey }, userId);
                    }
                }
            }
        }

        private int ParseSentinelSite(SurveyBase copy, int nameId, int latId, int lngId, int userId)
        {
            var nameInd = copy.IndicatorValues.FirstOrDefault(i => i.IndicatorId == nameId);
            if (nameInd == null)
                return 0;
            var sites = GetSitesForAdminLevel(copy.AdminLevels.Select(a => a.Id.ToString()));
            var existingSite = sites.FirstOrDefault(i => i.SiteName == nameInd.DynamicValue);
            if (existingSite != null)
            {
                return existingSite.Id;
            }
            SentinelSite newSite = new SentinelSite { SiteName = nameInd.DynamicValue, AdminLevels = copy.AdminLevels };
            var latInd = copy.IndicatorValues.FirstOrDefault(i => i.IndicatorId == latId);
            double val = 0;
            if (latInd != null && Double.TryParse(latInd.DynamicValue, out val))
                newSite.Lat = val;
            var lngInd = copy.IndicatorValues.FirstOrDefault(i => i.IndicatorId == lngId);
            if (lngInd != null && Double.TryParse(lngInd.DynamicValue, out val))
                newSite.Lng = val;
            newSite = Insert(newSite, userId);
            return newSite.Id;
        }

        private Dictionary<int, int> CreateMappingDictionary()
        {
            return new Dictionary<int, int>
    	    {
                // LF
    		    {277, 101},
                {278, 102},
                {279, 106},
                {282, 107},
                {283, 108},
                {284, 109},
                {285, 110},
                {286, 111},
                {287, 112},
                {288, 113},
                {291, 114},
                {295, 116},
                {296, 117},
                {297, 118},
                {301, 148},
                {427, 421},
                // SCH
                {325, 149},
                {326, 153},
                {327, 154},
                {328, 155},
                {329, 156},
                {330, 157},
                {331, 158},
                {332, 159},
                {333, 160},
                {334, 161},
                {336, 163},
                {337, 164},
                {338, 165},
                {339, 166},
                {341, 168},
                {342, 169},
                {345, 170},
                {419, 417},
                {428, 422},
                // STH
                {350, 171},
                {351, 175},
                {352, 176},
                {353, 177},
                {354, 178},
                {355, 179},
                {356, 180},
                {357, 181},
                {358, 182},
                {359, 183},
                {361, 185},
                {362, 186},
                {363, 187},
                {364, 188},
                {366, 190},
                {367, 191},
                {368, 192},
                {369, 193},
                {371, 195},
                {372, 196},
                {373, 197},
                {374, 198},
                {376, 200},
                {377, 201},
                {380, 202},
                {420, 418},
                {429, 423},

    	    };
        }
        #endregion

        #region Survey Type
        public T CreateSurvey<T>(StaticSurveyType typeOfSurvey) where T : SurveyBase
        {
            var survey = (T)Activator.CreateInstance(typeof(T));
            SurveyType t = GetSurveyType((int)typeOfSurvey);
            survey.TypeOfSurvey = t;
            return survey;
        }

        public SurveyBase CreateSurvey(int typeId)
        {
            SurveyBase survey = new SurveyBase();
            survey.TypeOfSurvey = GetSurveyType(typeId);
            return survey;
        }

        public List<SurveyType> GetSurveyTypes()
        {
            List<SurveyType> types = new List<SurveyType>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SurveyTypes.ID, SurveyTypes.SurveyTypeName, Diseases.DisplayName, Diseases.DiseaseType 
                        FROM SurveyTypes INNER JOIN Diseases on Diseases.ID = SurveyTypes.DiseaseId
                        WHERE Diseases.IsSelected = yes", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("SurveyTypeName"),
                                    reader.GetValueOrDefault<string>("SurveyTypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("SurveyTypeName");

                            types.Add(new SurveyType
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                SurveyTypeName = name,
                                DiseaseType = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DiseaseType"),
                                    reader.GetValueOrDefault<string>("DiseaseType"))
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return types;
        }

        public SurveyType GetSurveyType(int id)
        {
            SurveyType survey = new SurveyType();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SurveyTypes.SurveyTypeName, HasMultipleLocations, SurveyTypes.UpdatedAt,
                        aspnet_users.UserName, SurveyTypes.CreatedAt, created.UserName as CreatedBy, SurveyTypes.DiseaseId, 
                        Diseases.DiseaseType, Diseases.DisplayName
                        FROM (((SurveyTypes INNER JOIN aspnet_Users on SurveyTypes.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on SurveyTypes.CreatedById = created.UserId)
                            INNER JOIN Diseases on Diseases.ID = SurveyTypes.DiseaseId)
                        WHERE SurveyTypes.ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("SurveyTypeName"),
                                    reader.GetValueOrDefault<string>("SurveyTypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("SurveyTypeName");


                            survey = new SurveyType
                            {
                                Id = id,
                                SurveyTypeName = name,
                                DiseaseId = reader.GetValueOrDefault<int>("DiseaseId"),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                HasMultipleLocations = reader.GetValueOrDefault<bool>("HasMultipleLocations"),
                                UpdatedBy = GetAuditInfo(reader)
                            };
                        }
                        reader.Close();
                    }

                    List<string> indicatorIds = new List<string>();
                    command = new OleDbCommand(@"Select 
                        SurveyIndicators.ID,   
                        SurveyIndicators.DataTypeId,
                        SurveyIndicators.DisplayName,
                        SurveyIndicators.IsRequired,
                        SurveyIndicators.IsDisabled,
                        SurveyIndicators.IsEditable,
                        SurveyIndicators.IsDisplayed,
                        SurveyIndicators.RedistrictRuleId,
                        IsCalculated,
                        CanAddValues,
                        SurveyIndicators.UpdatedAt, 
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType
                        FROM ((SurveyIndicators INNER JOIN aspnet_users ON SurveyIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON SurveyIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE SurveyTypeId=@SurveyTypeId AND IsDisabled=0 
                        ORDER BY SortOrder,SurveyIndicators.ID", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeId", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survey.Indicators.Add(reader.GetValueOrDefault<string>("DisplayName"),
                                new Indicator
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId"),
                                RedistrictRuleId = reader.GetValueOrDefault<int>("RedistrictRuleId"),
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString() + " by " +
                                    reader.GetValueOrDefault<string>("UserName"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                IsRequired = reader.GetValueOrDefault<bool>("IsRequired"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                IsDisplayed = reader.GetValueOrDefault<bool>("IsDisplayed"),
                                IsCalculated = reader.GetValueOrDefault<bool>("IsCalculated"),
                                CanAddValues = reader.GetValueOrDefault<bool>("CanAddValues"),
                                DataType = reader.GetValueOrDefault<string>("DataType")
                            });
                            indicatorIds.Add(reader.GetValueOrDefault<int>("ID").ToString());
                        }
                        reader.Close();
                    }

                    survey.IndicatorDropdownValues = GetIndicatorDropdownValues(connection, command, IndicatorEntityType.Survey, indicatorIds);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return survey;
        }

        public void Save(SurveyType model, int userId)
        {
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

                    if (model.Id > 0)
                        command = new OleDbCommand(@"UPDATE SurveyTypes SET SurveyTypeName=@SurveyTypeName, DiseaseId=@DiseaseId, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO SurveyTypes (SurveyTypeName, DiseaseId, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@SurveyTypeName, @DiseaseId, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeName", model.SurveyTypeName));
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", model.DiseaseId));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (model.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", model.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();

                    if (model.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM SurveyTypes", connection);
                        model.Id = (int)command.ExecuteScalar();
                        // Add year reported
                        command = new OleDbCommand(@"INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, AggTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, IsDisplayed, SortOrder,  UpdatedById, UpdatedAt) VALUES
                        (@SurveyTypeId, 4, 5, 'DateReported', -1, 0, 0, 0, -1, @UpdatedById, @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE SurveyIndicators SET SurveyTypeId=@SurveyTypeId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, IsRequired=@IsRequired, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", indicator.Id));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id <= 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@SurveyTypeId, @DataTypeId, @DisplayName, @IsRequired, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();

                        command = new OleDbCommand(@"SELECT Max(ID) FROM SurveyIndicators", connection);
                        indicator.Id = (int)command.ExecuteScalar();
                    }

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                    
                }
                catch (Exception)
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
                    throw;
                }
            }
        }
        #endregion

        #region Misc
        public List<SentinelSite> GetSitesForAdminLevel(IEnumerable<string> adminLevelIds)
        {
            List<SentinelSite> sites = new List<SentinelSite>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SentinelSites.ID, SentinelSites.SiteName, 
                        SentinelSites.Lat, SentinelSites.Lng, SentinelSites.Notes
                        FROM (SentinelSites INNER JOIN SentinelSites_to_AdminLevels ON SentinelSites_to_AdminLevels.SentinelSiteId = SentinelSites.ID)
                        WHERE SentinelSites_to_AdminLevels.AdminLevelId in (" + String.Join(", ", adminLevelIds.ToArray()) + @")
                        GROUP BY SentinelSites.ID, SentinelSites.SiteName, SentinelSites.Lat, SentinelSites.Lng, SentinelSites.Notes", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sites.Add(new SentinelSite
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                SiteName = reader.GetValueOrDefault<string>("SiteName"),
                                Lat = reader.GetNullableDouble("Lat"),
                                Lng = reader.GetNullableDouble("Lng"),
                                Notes = reader.GetValueOrDefault<string>("Notes")
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return sites;
        }

        public SentinelSite GetSiteById(int id)
        {
            SentinelSite site = new SentinelSite();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SentinelSites.ID, SentinelSites.SiteName, 
                        SentinelSites.Lat, SentinelSites.Lng, SentinelSites.Notes
                        FROM SentinelSites
                        WHERE SentinelSites.ID=@Id", connection);
                    command.Parameters.Add(new OleDbParameter("@Id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            site = new SentinelSite
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                SiteName = reader.GetValueOrDefault<string>("SiteName"),
                                Lat = reader.GetNullableDouble("Lat"),
                                Lng = reader.GetNullableDouble("Lng"),
                                Notes = reader.GetValueOrDefault<string>("Notes")
                            };
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return site;
        }

        public SentinelSite Insert(SentinelSite site, int updatedById)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Insert Into SentinelSites (SiteName, Lat, Lng,
                        Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@SiteName, @Lat, @Lng, @Notes, @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteName", site.SiteName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lat", site.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lng", site.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", site.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", updatedById));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@CreatedById", updatedById));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"SELECT Max(ID) FROM SentinelSites", connection);
                    site.Id = (int)command.ExecuteScalar();

                    foreach (var adminLevel in site.AdminLevels)
                    {
                        command = new OleDbCommand(@"Insert Into SentinelSites_to_AdminLevels (SentinelSiteId, AdminLevelId) VALUES
                        (@SentinelSiteId, @AdminLevelId)", connection);
                        command.Parameters.Add(new OleDbParameter("@SentinelSiteId", site.Id));
                        command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel.Id));
                        command.ExecuteNonQuery();
                    }
                    return site;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public SentinelSite Update(SentinelSite site, int updatedById)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE SentinelSites SET SiteName=@SiteName,
                        Lat=@Lat, Lng=@Lng, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@SiteName", site.SiteName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lat", site.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lng", site.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", site.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", updatedById));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", site.Id));
                    command.ExecuteNonQuery();
                    return site;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

     
        #endregion

        #region Private Methods
        public void SaveSurveyBase(OleDbCommand command, OleDbConnection connection, SurveyBase survey, int userId)
        {
            survey.MapIndicatorsToProperties();
         
            if (survey.Id > 0)
                command = new OleDbCommand(@"UPDATE Surveys SET SurveyTypeId=@SurveyTypeId, DateReported=@DateReported, StartDate=@StartDate,
                           EndDate=@EndDate, SiteType=@SiteType, SpotCheckName=@SpotCheckName, SpotCheckLat=@SpotCheckLat, SpotCheckLng=@SpotCheckLng,
                           SentinelSiteId=@SentinelSiteId, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO Surveys (SurveyTypeId, DateReported, StartDate, EndDate, 
                            SiteType, SpotCheckName, SpotCheckLat, SpotCheckLng, SentinelSiteId, Notes, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@SurveyTypeId, @DateReported, @StartDate, @EndDate, 
                            @SiteType, @SpotCheckName, @SpotCheckLat, @SpotCheckLng, @SentinelSiteId, @Notes, 
                            @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyTypeId", survey.TypeOfSurvey.Id));
            command.Parameters.Add(new OleDbParameter("@DateReported", survey.DateReported));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@StartDate", survey.StartDate));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@EndDate", survey.EndDate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteType", survey.SiteType));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckName", survey.SpotCheckName));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLat", survey.Lat));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLng", survey.Lng));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SentinelSiteId", survey.SentinelSiteId));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", survey.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (survey.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", survey.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }

            command.ExecuteNonQuery();

            if (survey.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM Surveys", connection);
                survey.Id = (int)command.ExecuteScalar();
            }

            // Save adminlevels
            command = new OleDbCommand(@"DELETE FROM Surveys_to_AdminLevels WHERE SurveyId=@SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            command.ExecuteNonQuery();
            foreach (var al in survey.AdminLevels)
            {
                command = new OleDbCommand(@"INSERT INTO Surveys_to_AdminLevels (SurveyId, AdminLevelId) values (@id, @AdminLevelId)", connection);
                command.Parameters.Add(new OleDbParameter("@id", survey.Id));
                command.Parameters.Add(new OleDbParameter("@AdminLevelId", al.Id));
                command.ExecuteNonQuery();
            }

            AddSurveyIndicatorValues(connection, survey, userId);
        }

        private void AddSurveyIndicatorValues(OleDbConnection connection, SurveyBase survey, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM SurveyIndicatorValues WHERE SurveyId=@SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in survey.IndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into SurveyIndicatorValues (IndicatorId, SurveyId, DynamicValue, UpdatedById, UpdatedAt, CalcByRedistrict) VALUES
                        (@IndicatorId, @SurveyId, @DynamicValue, @UpdatedById, @UpdatedAt, @CalcByRedistrict)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                command.Parameters.Add(OleDbUtil.CreateNullableParam("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.Parameters.Add(new OleDbParameter("@CalcByRedistrict", val.CalcByRedistrict));
                command.ExecuteNonQuery();
            }
        }

        private void GetSurveyIndicatorValues(OleDbConnection connection, SurveyBase survey)
        {
            OleDbCommand command = new OleDbCommand(@"Select 
                        SurveyIndicatorValues.ID,   
                        SurveyIndicatorValues.IndicatorId,
                        SurveyIndicatorValues.DynamicValue,
                        SurveyIndicators.DisplayName,
                        SurveyIndicatorValues.CalcByRedistrict
                        FROM SurveyIndicatorValues INNER JOIN SurveyIndicators on SurveyIndicatorValues.IndicatorId = SurveyIndicators.ID
                        WHERE SurveyIndicatorValues.SurveyId = @SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!survey.TypeOfSurvey.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                        continue;
                    survey.IndicatorValues.Add(new IndicatorValue
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"),
                        CalcByRedistrict = reader.GetValueOrDefault<bool>("CalcByRedistrict"),
                        Indicator = survey.TypeOfSurvey.Indicators[reader.GetValueOrDefault<string>("DisplayName")]
                    });
                }
                reader.Close();
            }
        }
        #endregion





    }
}

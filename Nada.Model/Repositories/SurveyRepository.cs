using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Model.Base;
using Nada.Model.Survey;

namespace Nada.Model.Repositories
{
    public class SurveyRepository
    {
        #region LF
        public void Insert(LfMfPrevalence survey, int userId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Insert Into SurveyLfMf (TimingType, TestType, SiteType, 
                        AdminLevelId, SpotCheckName, SpotCheckLat, SpotCheckLng, SentinelSiteId, RoundsMda, 
                        SurveyDate, Examined, Positive, MeanDensity, MfCount, MfLoad, SampleSize, AgeRange, Notes, UpdatedBy, UpdatedAt) VALUES
                        (@TimingType, @TestType, @SiteType, @AdminLevelId, @SpotCheckName, @SpotCheckLat, @SpotCheckLng, @SentinelSiteId, @RoundsMda, 
                        @SurveyDate, @Examined, @Positive, @MeanDensity, @MfCount, @MfLoad, @SampleSize, @AgeRange, @Notes, @UpdatedBy, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@TimingType", survey.TimingType));
                    command.Parameters.Add(new OleDbParameter("@TestType", survey.TestType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteType", survey.SiteType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", survey.AdminLevelId));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckName", survey.SpotCheckName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLat", survey.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLng", survey.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SentinelSiteId", survey.SentinelSiteId));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@RoundsMda", survey.RoundsMda));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@SurveyDate", survey.SurveyDate));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Examined", survey.Examined));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Positive", survey.Positive));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MeanDensity", survey.MeanDensity));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MfCount", survey.MfCount));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MfLoad", survey.MfLoad));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SampleSize", survey.SampleSize));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AgeRange", survey.AgeRange));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", survey.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"SELECT Max(ID) FROM SurveyLfMf", connection);
                    survey.Id = (int)command.ExecuteScalar();

                    AddSurveyIndicatorValues(connection, survey, userId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Update(LfMfPrevalence survey, int userId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE SurveyLfMf SET TimingType=@TimingType, TestType=@TestType,
                        SiteType=@SiteType, AdminLevelId=@AdminLevelId, SpotCheckName=@SpotCheckName, 
                        SpotCheckLat=@SpotCheckLat, SpotCheckLng=@SpotCheckLng, SentinelSiteId=@SentinelSiteId, 
                        RoundsMda=@RoundsMda, SurveyDate=@SurveyDate, Examined=@Examined, Positive=@Positive, 
                        MeanDensity=@MeanDensity, MfCount=@MfCount, MfLoad=@MfLoad, SampleSize=@SampleSize, 
                        AgeRange=@AgeRange, Notes=@Notes, UpdatedBy=@UpdatedBy, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@TimingType", survey.TimingType));
                    command.Parameters.Add(new OleDbParameter("@TestType", survey.TestType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteType", survey.SiteType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", survey.AdminLevelId));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckName", survey.SpotCheckName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLat", survey.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLng", survey.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SentinelSiteId", survey.SentinelSiteId));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@RoundsMda", survey.RoundsMda));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@SurveyDate", survey.SurveyDate));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Examined", survey.Examined));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Positive", survey.Positive));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MeanDensity", survey.MeanDensity));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MfCount", survey.MfCount));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@MfLoad", survey.MfLoad));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SampleSize", survey.SampleSize));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AgeRange", survey.AgeRange));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", survey.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", survey.Id));
                    command.ExecuteNonQuery();

                    AddSurveyIndicatorValues(connection, survey, userId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public LfMfPrevalence GetLfMfPrevalenceSurvey(int id)
        {
            var survey = CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SurveyLfMf.TimingType, SurveyLfMf.TestType, SurveyLfMf.SiteType, 
                        SurveyLfMf.AdminLevelId, SurveyLfMf.SpotCheckName, SurveyLfMf.SpotCheckLat, SurveyLfMf.SpotCheckLng, 
                        SurveyLfMf.SentinelSiteId, SurveyLfMf.RoundsMda, SurveyLfMf.SurveyDate, SurveyLfMf.Examined, 
                        SurveyLfMf.Positive, SurveyLfMf.MeanDensity, SurveyLfMf.MfCount, SurveyLfMf.MfLoad, SurveyLfMf.MfLoad, SurveyLfMf.AgeRange,
                        SurveyLfMf.Notes, SurveyLfMf.UpdatedBy, SurveyLfMf.UpdatedAt, aspnet_Users.UserName, AdminLevels.DisplayName, 
                        SentinelSites.SiteName
                        FROM (((SurveyLfMf INNER JOIN aspnet_Users on SurveyLfMf.UpdatedBy = aspnet_Users.UserId)
                            LEFT OUTER JOIN AdminLevels on SurveyLfMf.AdminLevelId = AdminLevels.ID) 
                            LEFT OUTER JOIN SentinelSites  on SurveyLfMf.SentinelSiteId = SentinelSites.ID) 
                        WHERE SurveyLfMf.ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            survey.Id = id;
                            survey.TimingType = reader.GetValueOrDefault<string>("TimingType");
                            survey.TestType = reader.GetValueOrDefault<string>("TestType");
                            survey.SiteType = reader.GetValueOrDefault<string>("SiteType");
                            survey.AdminLevelId = reader.GetValueOrDefault<Nullable<int>>("AdminLevelId");
                            survey.SpotCheckName = reader.GetValueOrDefault<string>("SpotCheckName");
                            survey.Lat = reader.GetNullableDouble("SpotCheckLat");
                            survey.Lng = reader.GetNullableDouble("SpotCheckLng");
                            survey.SentinelSiteId = reader.GetValueOrDefault<Nullable<int>>("SentinelSiteId");
                            survey.RoundsMda = reader.GetValueOrDefault<Nullable<int>>("RoundsMda");
                            survey.SurveyDate = reader.GetValueOrDefault<DateTime>("SurveyDate");
                            survey.Examined = reader.GetValueOrDefault<Nullable<int>>("Examined");
                            survey.Positive = reader.GetValueOrDefault<Nullable<int>>("Positive");
                            survey.MeanDensity = reader.GetNullableDouble("MeanDensity");
                            survey.MfCount = reader.GetValueOrDefault<Nullable<int>>("MfCount");
                            survey.MfLoad = reader.GetNullableDouble("MfLoad");
                            survey.SampleSize = reader.GetValueOrDefault<Nullable<int>>("SampleSize");
                            survey.AgeRange = reader.GetValueOrDefault<string>("AgeRange");
                            survey.Notes = reader.GetValueOrDefault<string>("Notes");
                            survey.SpotCheckAdminLevel = reader.GetValueOrDefault<string>("DisplayName");
                            survey.SentinelSiteName = reader.GetValueOrDefault<string>("SiteName");
                            survey.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                            survey.UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy");
                        }
                        reader.Close();
                    }

                    GetSurveyIndicatorValues(connection, survey);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return survey;
        }

        #endregion

        #region Survey Base
        public T CreateSurvey<T>(StaticSurveyType typeOfSurvey) where T : SurveyBase
        {
            var survey = (T)Activator.CreateInstance(typeof(T));
            SurveyType t = GetSurveyType((int)typeOfSurvey);
            survey.TypeOfSurvey = t;
            return survey;
        }

        public SurveyBase CreateSurvey(StaticSurveyType typeOfSurvey)
        {
            SurveyBase survey = new SurveyBase();
            survey.TypeOfSurvey = GetSurveyType((int)typeOfSurvey);
            return survey;
        }

        private void AddSurveyIndicatorValues(OleDbConnection connection, SurveyBase survey, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM SurveyIndicatorValues WHERE SurveyId=@SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in survey.CustomIndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into SurveyIndicatorValues (IndicatorId, SurveyId, DynamicValue, UpdatedById, UpdatedAt) VALUES
                        (@IndicatorId, @SurveyId, @DynamicValue, @UpdatedById, @UpdatedAt)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                command.Parameters.Add(new OleDbParameter("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.ExecuteNonQuery();
            }
        }

        private void GetSurveyIndicatorValues(OleDbConnection connection, SurveyBase survey)
        {
            OleDbCommand command = new OleDbCommand(@"Select 
                        SurveyIndicatorValues.ID,   
                        SurveyIndicatorValues.IndicatorId,
                        SurveyIndicatorValues.DynamicValue
                        FROM SurveyIndicatorValues
                        WHERE SurveyIndicatorValues.SurveyId = @SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    survey.CustomIndicatorValues.Add(new IndicatorValue
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        DynamicValue = reader.GetValueOrDefault<string>("DynamicValue")
                    });
                }
                reader.Close();
            }
        }

        public SurveyType GetSurveyType(int id)
        {
            SurveyType survey = new SurveyType();

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SurveyTypes.SurveyTypeName, SurveyTypes.UpdatedAt,
                        aspnet_users.UserName
                        FROM (SurveyTypes INNER JOIN aspnet_Users on SurveyTypes.UpdatedById = aspnet_Users.UserId)
                        WHERE SurveyTypes.ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            survey = new SurveyType
                            {
                                Id = id,
                                SurveyTypeName = reader.GetValueOrDefault<string>("SurveyTypeName"),
                                UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy")
                            };
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select 
                        SurveyIndicators.ID,   
                        SurveyIndicators.DataTypeId,
                        SurveyIndicators.DisplayName,
                        SurveyIndicators.SortOrder,
                        SurveyIndicators.IsDisabled,
                        SurveyIndicators.IsEditable,
                        SurveyIndicators.UpdatedAt, 
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType
                        FROM ((SurveyIndicators INNER JOIN aspnet_users ON SurveyIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON SurveyIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE SurveyTypeId=@SurveyTypeId AND IsDisabled=0 
                        ORDER BY SortOrder", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeId", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survey.Indicators.Add(new Indicator
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId"),
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy") + " by " +
                                    reader.GetValueOrDefault<string>("UserName"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                DataType = reader.GetValueOrDefault<string>("DataType")
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
            return survey;
        }

        public void SaveSurvey(SurveyBase survey, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (survey.Id > 0)
                        command = new OleDbCommand(@"UPDATE Surveys SET SurveyTypeId=@SurveyTypeId, AdminLevelId=@AdminLevelId, SurveyDate=@SurveyDate,
                           Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO Surveys (SurveyTypeId, AdminLevelId, SurveyDate, Notes, UpdatedById, 
                            UpdatedAt) values (@SurveyTypeId, @AdminLevelId, @SurveyDate, @Notes, @UpdatedById, @UpdatedAt)", connection); command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", survey.AdminLevelId));
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeId", survey.TypeOfSurvey.Id));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@SurveyDate", survey.SurveyDate));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", survey.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));               
                    if (survey.Id > 0) command.Parameters.Add(new OleDbParameter("@id", survey.Id));
                    command.ExecuteNonQuery();

                    if (survey.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM Surveys", connection);
                        survey.Id = (int)command.ExecuteScalar();
                    }
                    
                    AddSurveyIndicatorValues(connection, survey, userId);

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

        public void Save(SurveyType model, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
                        command = new OleDbCommand(@"UPDATE SurveyTypes SET SurveyTypeName=@SurveyTypeName, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO SurveyTypes SurveyTypeName, UpdatedById, 
                            UpdatedAt) values (@SurveyTypeName, @UpdatedById, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeName", model.SurveyTypeName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (model.Id > 0) command.Parameters.Add(new OleDbParameter("@id", model.Id));
                    command.ExecuteNonQuery();

                    if (model.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM SurveyTypes", connection);
                        model.Id = (int)command.ExecuteScalar();
                    }

                    foreach (var indicator in model.Indicators.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE SurveyIndicators SET SurveyTypeId=@SurveyTypeId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, SortOrder=@SortOrder, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@SortOrder", indicator.SortOrder));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", indicator.Id));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Where(i => i.Id <= 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, 
                        DisplayName, SortOrder, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@SurveyTypeId, @DataTypeId, @DisplayName, @SortOrder, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@SortOrder", indicator.SortOrder));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
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
        public SentinelSite Insert(SentinelSite site, int updatedById)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Insert Into SentinelSites (AdminLevelId, SiteName, Lat, Lng,
                        Notes, UpdatedById, UpdatedAt) VALUES
                        (@AdminLevelId, @SiteName, @Lat, @Lng, @Notes, @UpdatedById, @UpdatedAt)", connection);
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", site.AdminLevel.Id));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteName", site.SiteName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lat", site.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Lng", site.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", site.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", updatedById));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"SELECT Max(ID) FROM SentinelSites", connection);
                    site.Id = (int)command.ExecuteScalar();
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE SentinelSites SET AdminLevelId=@AdminLevelId, SiteName=@SiteName,
                        Lat=@Lat, Lng=@Lng, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", site.AdminLevel));
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

        public List<SentinelSite> GetSitesForAdminLevel(int adminLevelId)
        {
            List<SentinelSite> sites = new List<SentinelSite>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select SentinelSites.ID, SentinelSites.AdminLevelId, SentinelSites.SiteName, 
                        SentinelSites.Lat, SentinelSites.Lng, SentinelSites.Notes,  
                        SentinelSites.UpdatedAt, aspnet_users.UserName 
                        FROM (SentinelSites INNER JOIN aspnet_users ON SentinelSites.UpdatedById = aspnet_users.UserId)
                        WHERE AdminLevelId=@AdminLevelId", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevelId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sites.Add(new SentinelSite
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                AdminLevel = new AdminLevel { Id = reader.GetValueOrDefault<int>("AdminLevelId") },
                                SiteName = reader.GetValueOrDefault<string>("SiteName"),
                                Lat = reader.GetNullableDouble("Lat"),
                                Lng = reader.GetNullableDouble("Lng"),
                                Notes = reader.GetValueOrDefault<string>("Notes"),
                                UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy")
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
        #endregion

    }
}

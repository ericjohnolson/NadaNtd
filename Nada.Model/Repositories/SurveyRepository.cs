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
                        Surveys.YearReported,
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
                                TypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DiseaseName"), reader.GetValueOrDefault<string>("DisplayName")) + " " +
                                    TranslationLookup.GetValue(reader.GetValueOrDefault<string>("SurveyTypeName"), reader.GetValueOrDefault<string>("SurveyTypeName")),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                TypeId = reader.GetValueOrDefault<int>("SurveyTypeId"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                Year = reader.GetValueOrDefault<int>("YearReported"),
                                StartDate = reader.GetValueOrDefault<DateTime>("StartDate"),
                                EndDate = reader.GetValueOrDefault<DateTime>("EndDate"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
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

        public void Save(LfMfPrevalence survey, int userId)
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

                    // Add non dynamic Lf Mda specific fields
                    command = new OleDbCommand(@"DELETE FROM SurveyLfMf WHERE SurveyId=@SurveyId", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"Insert Into SurveyLfMf (SurveyId, TimingType, TestType, SiteType, 
                        SpotCheckName, SpotCheckLat, SpotCheckLng, SentinelSiteId) VALUES
                        (@SurveyId, @TimingType, @TestType, @SiteType,  @SpotCheckName, @SpotCheckLat, @SpotCheckLng, 
                        @SentinelSiteId)", connection);

                    command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                    command.Parameters.Add(new OleDbParameter("@TimingType", survey.TimingType));
                    command.Parameters.Add(new OleDbParameter("@TestType", survey.TestType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SiteType", survey.SiteType));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckName", survey.SpotCheckName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLat", survey.Lat));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SpotCheckLng", survey.Lng));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@SentinelSiteId", survey.SentinelSiteId));
                    command.ExecuteNonQuery();

                    // Save related lists
                    command = new OleDbCommand(@"DELETE FROM Surveys_to_Vectors WHERE SurveyId=@SurveyId", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                    command.ExecuteNonQuery();
                    foreach (var vector in survey.Vectors)
                    {
                        command = new OleDbCommand(@"INSERT INTO Surveys_to_Vectors (SurveyId, VectorId) values (@id, @VectorId)", connection);
                        command.Parameters.Add(new OleDbParameter("@id", survey.Id));
                        command.Parameters.Add(new OleDbParameter("@VectorId", vector.Id));
                        command.ExecuteNonQuery();
                    }

                    command = new OleDbCommand(@"DELETE FROM Surveys_to_Partners WHERE SurveyId=@SurveyId", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                    command.ExecuteNonQuery();
                    foreach (var partner in survey.Partners)
                    {
                        command = new OleDbCommand(@"INSERT INTO Surveys_to_Partners (SurveyId, PartnerId) values (@SurveyId, @PartnerId)", connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                        command.Parameters.Add(new OleDbParameter("@PartnerId", partner.Id));
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
                        WHERE Surveys.AdminLevelId=@adminlevelId AND Surveys.YearReported=@year", connection);
                    command.Parameters.Add(new OleDbParameter("@adminlevelId", adminLevel));
                    command.Parameters.Add(new OleDbParameter("@year", yearOfReporting));
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

        public LfMfPrevalence GetLfMfPrevalenceSurvey(int id)
        {
            LfMfPrevalence survey = null;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;

                    survey = GetSurvey<LfMfPrevalence>(command, connection, id);

                    command = new OleDbCommand(@"Select SurveyLfMf.TimingType, SurveyLfMf.TestType, SurveyLfMf.SiteType, 
                        SurveyLfMf.SpotCheckName, SurveyLfMf.SpotCheckLat, SurveyLfMf.SpotCheckLng, 
                        SurveyLfMf.SentinelSiteId
                        FROM SurveyLfMf 
                        WHERE SurveyLfMf.SurveyId=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            survey.TimingType = reader.GetValueOrDefault<string>("TimingType");
                            survey.TestType = reader.GetValueOrDefault<string>("TestType");
                            survey.SiteType = reader.GetValueOrDefault<string>("SiteType");
                            survey.SpotCheckName = reader.GetValueOrDefault<string>("SpotCheckName");
                            survey.Lat = reader.GetNullableDouble("SpotCheckLat");
                            survey.Lng = reader.GetNullableDouble("SpotCheckLng");
                            survey.SentinelSiteId = reader.GetValueOrDefault<Nullable<int>>("SentinelSiteId");
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select Vectors.ID, Vectors.DisplayName
                        FROM Vectors INNER JOIN Surveys_to_Vectors on Vectors.ID = Surveys_to_Vectors.VectorId
                        WHERE Surveys_to_Vectors.SurveyId=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survey.Vectors.Add(new Vector
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName")
                            });
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select Partners.ID, Partners.DisplayName
                        FROM Partners INNER JOIN Surveys_to_Partners on Partners.ID = Surveys_to_Partners.PartnerId
                        WHERE Surveys_to_Partners.SurveyId=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survey.Partners.Add(new Partner
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName")
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

        public T GetSurvey<T>(OleDbCommand command, OleDbConnection connection, int surveyId) where T : SurveyBase
        {
            var survey = (T)Activator.CreateInstance(typeof(T));
            int surveyTypeId = 0;

            try
            {
                command = new OleDbCommand(@"Select Surveys.StartDate, Surveys.EndDate, Surveys.YearReported, 
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
                        survey.Year = reader.GetValueOrDefault<int>("YearReported");
                        survey.SiteType = reader.GetValueOrDefault<string>("SiteType");
                        survey.SpotCheckName = reader.GetValueOrDefault<string>("SpotCheckName");
                        survey.Lat = reader.GetValueOrDefault<Nullable<double>>("SpotCheckLat");
                        survey.Lng = reader.GetValueOrDefault<Nullable<double>>("SpotCheckLng");
                        survey.SentinelSiteId = reader.GetValueOrDefault<Nullable<int>>("SentinelSiteId");
                        survey.Notes = reader.GetValueOrDefault<string>("Notes");
                        survey.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                        survey.UpdatedBy = Util.GetAuditInfo(reader);
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
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType")
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
                                UpdatedBy = Util.GetAuditInfo(reader)
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
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy") + " by " +
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
                        (@SurveyTypeId, 7, 5, 'SurveyYear', -1, 0, 0, 0, -1, @UpdatedById, @UpdatedAt)", connection);
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

        public List<Vector> GetVectors()
        {
            List<Vector> list = new List<Vector>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, UserName, UpdatedAt from Vectors
                        INNER JOIN aspnet_users on Vectors.UpdatedById = aspnet_users.userid WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Vector
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
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
            return list;
        }

        #endregion

        #region Private Methods
        private void SaveSurveyBase(OleDbCommand command, OleDbConnection connection, SurveyBase survey, int userId)
        {
            // Mapping year reported
            survey.MapIndicatorsToProperties();

            if (survey.Id > 0)
                command = new OleDbCommand(@"UPDATE Surveys SET SurveyTypeId=@SurveyTypeId, YearReported=@YearReported, StartDate=@StartDate,
                           EndDate=@EndDate, SiteType=@SiteType, SpotCheckName=@SpotCheckName, SpotCheckLat=@SpotCheckLat, SpotCheckLng=@SpotCheckLng,
                           SentinelSiteId=@SentinelSiteId, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO Surveys (SurveyTypeId, YearReported, StartDate, EndDate, 
                            SiteType, SpotCheckName, SpotCheckLat, SpotCheckLng, SentinelSiteId, Notes, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@SurveyTypeId, @YearReported, @StartDate, @EndDate, 
                            @SiteType, @SpotCheckName, @SpotCheckLat, @SpotCheckLng, @SentinelSiteId, @Notes, 
                            @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyTypeId", survey.TypeOfSurvey.Id));
            command.Parameters.Add(new OleDbParameter("@YearReported", survey.Year));
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
                command = new OleDbCommand(@"Insert Into SurveyIndicatorValues (IndicatorId, SurveyId, DynamicValue, UpdatedById, UpdatedAt) VALUES
                        (@IndicatorId, @SurveyId, @DynamicValue, @UpdatedById, @UpdatedAt)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
                command.Parameters.Add(OleDbUtil.CreateNullableParam("@DynamicValue", val.DynamicValue));
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
                        SurveyIndicatorValues.DynamicValue,
                        SurveyIndicators.DisplayName
                        FROM SurveyIndicatorValues INNER JOIN SurveyIndicators on SurveyIndicatorValues.IndicatorId = SurveyIndicators.ID
                        WHERE SurveyIndicatorValues.SurveyId = @SurveyId", connection);
            command.Parameters.Add(new OleDbParameter("@SurveyId", survey.Id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    survey.IndicatorValues.Add(new IndicatorValue
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"), 
                        Indicator = survey.TypeOfSurvey.Indicators[reader.GetValueOrDefault<string>("DisplayName")]
                    });
                }
                reader.Close();
            }
        }
        #endregion





    }
}

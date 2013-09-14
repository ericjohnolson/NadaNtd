using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;

namespace Nada.Model.Repositories
{
    public class DiseaseRepository
    {
        #region Disease Population
        public DiseasePopulation CreatePop(DiseaseType disease)
        {
            DiseasePopulation dd = new DiseasePopulation();
            dd.Disease = GetDiseaseById((int)disease, "en-US");
            dd.Indicators = GetIndicatorsForDisease(dd.Disease.Id, "DiseasePopulationIndicators");
            return dd;
        }

        public void Save(DiseasePopulation pop, int userId)
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

                    if (pop.Id > 0)
                        command = new OleDbCommand(@"UPDATE DiseasePopulations SET DiseaseId=@DiseaseId, AdminLevelId=@AdminLevelId,
                           YearReported=@YearReported, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO DiseasePopulations (DiseaseId, AdminLevelId, YearReported, Notes, UpdatedById, 
                            UpdatedAt) values (@DiseaseId, @AdminLevelId, @YearReported, @Notes, @UpdatedById, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", pop.Disease.Id));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", pop.AdminLevelId));
                    command.Parameters.Add(new OleDbParameter("@YearReported", pop.Year));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", pop.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (pop.Id > 0) command.Parameters.Add(new OleDbParameter("@id", pop.Id));
                    command.ExecuteNonQuery();

                    if (pop.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM DiseasePopulations", connection);
                        pop.Id = (int)command.ExecuteScalar();
                    }

                    AddIntvIndicatorValues(connection, pop, userId);

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

        public void SaveIndicators(DiseasePopulation model, int userId)
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

                    foreach (var indicator in model.Indicators.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE DiseasePopulationIndicators SET DiseaseId=@DiseaseId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, SortOrder=@SortOrder, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", model.Disease.Id));
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
                        command = new OleDbCommand(@"INSERT INTO DiseasePopulationIndicators (DiseaseId, DataTypeId, 
                        DisplayName, SortOrder, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@DiseaseId, @DataTypeId, @DisplayName, @SortOrder, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", model.Disease.Id));
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

        private void AddIntvIndicatorValues(OleDbConnection connection, DiseasePopulation pop, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM DiseasePopulationIndicatorValues WHERE DiseasePopulationId=@DiseasePopulationId", connection);
            command.Parameters.Add(new OleDbParameter("@DiseasePopulationId", pop.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in pop.CustomIndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into DiseasePopulationIndicatorValues (IndicatorId, DiseasePopulationId, DynamicValue, UpdatedById, UpdatedAt) VALUES
                        (@IndicatorId, @DiseasePopulationId, @DynamicValue, @UpdatedById, @UpdatedAt)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@DiseasePopulationId", pop.Id));
                command.Parameters.Add(new OleDbParameter("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.ExecuteNonQuery();
            }
        }
        #endregion
        
        #region Disease Distribution
        public DiseaseDistribution Create(DiseaseType disease)
        {
            DiseaseDistribution dd = new DiseaseDistribution();
            dd.Disease = GetDiseaseById((int)disease, "en-US");
            dd.Indicators = GetIndicatorsForDisease(dd.Disease.Id, "DiseaseDistributionIndicators");
            return dd;
        }

        public DiseaseDistribution GetDiseaseDistribution(int adminLevel, DiseaseType type)
        {
            DiseaseDistribution diseaseDistro = Create(type);
            diseaseDistro.AdminLevelId = adminLevel;

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select DiseaseDistributions.ID, DiseaseDistributions.Notes, DiseaseDistributions.UpdatedAt,
                        aspnet_users.UserName
                        FROM (DiseaseDistributions INNER JOIN aspnet_Users on DiseaseDistributions.UpdatedById = aspnet_Users.UserId)
                        WHERE DiseaseDistributions.AdminLevelId=@AdminLevelId and DiseaseDistributions.DiseaseId=@DiseaseId", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel));
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", (int)type));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            diseaseDistro.Id = reader.GetValueOrDefault<int>("ID");
                            diseaseDistro.Notes = reader.GetValueOrDefault<string>("Notes");
                            diseaseDistro.UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy");
                        }
                        reader.Close();
                    }

                    if (diseaseDistro.Id < 1)
                        return diseaseDistro;

                    command = new OleDbCommand(@"Select 
                        DiseaseDistributionIndicatorValues.ID,   
                        DiseaseDistributionIndicatorValues.IndicatorId,
                        DiseaseDistributionIndicatorValues.DynamicValue
                        FROM DiseaseDistributionIndicatorValues
                        WHERE DiseaseDistributionId=@DiseaseDistributionId", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", diseaseDistro.Id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            diseaseDistro.CustomIndicatorValues.Add(new IndicatorValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                                DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"),
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
            return diseaseDistro;
        }

        public void Save(DiseaseDistribution distro, int userId)
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

                    if (distro.Id > 0)
                        command = new OleDbCommand(@"UPDATE DiseaseDistributions SET DiseaseId=@DiseaseId, AdminLevelId=@AdminLevelId,
                           Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO DiseaseDistributions (DiseaseId, AdminLevelId, Notes, UpdatedById, 
                            UpdatedAt) values (@DiseaseId, @AdminLevelId, @Notes, @UpdatedById, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", distro.Disease.Id));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", distro.AdminLevelId));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", distro.Notes));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (distro.Id > 0) command.Parameters.Add(new OleDbParameter("@id", distro.Id));
                    command.ExecuteNonQuery();

                    if (distro.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM DiseaseDistributions", connection);
                        distro.Id = (int)command.ExecuteScalar();
                    }

                    AddIntvIndicatorValues(connection, distro, userId);

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

        public void SaveIndicators(DiseaseDistribution model, int userId)
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

                    foreach (var indicator in model.Indicators.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE DiseaseDistributionIndicators SET DiseaseId=@DiseaseId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, SortOrder=@SortOrder, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", model.Disease.Id));
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
                        command = new OleDbCommand(@"INSERT INTO DiseaseDistributionIndicators (DiseaseId, DataTypeId, 
                        DisplayName, SortOrder, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@DiseaseId, @DataTypeId, @DisplayName, @SortOrder, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", model.Disease.Id));
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

        private void AddIntvIndicatorValues(OleDbConnection connection, DiseaseDistribution distro, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM DiseaseDistributionIndicatorValues WHERE DiseaseDistributionId=@DiseaseDistributionId", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", distro.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in distro.CustomIndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into DiseaseDistributionIndicatorValues (IndicatorId, DiseaseDistributionId, DynamicValue, UpdatedById, UpdatedAt) VALUES
                        (@IndicatorId, @DiseaseDistributionId, @DynamicValue, @UpdatedById, @UpdatedAt)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", distro.Id));
                command.Parameters.Add(new OleDbParameter("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region Disease
        private List<Indicator> GetIndicatorsForDisease(int diseaseId, string table)
        {
            List<Indicator> indicators = new List<Indicator>();

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(string.Format(@"Select 
                        {0}.ID,   
                        DataTypeId,
                        DisplayName,
                        SortOrder,
                        IsDisabled,
                        IsEditable,
                        UpdatedAt, 
                        UserName,
                        DataType
                        FROM (({0} INNER JOIN aspnet_users ON {0}.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON {0}.DataTypeId = IndicatorDataTypes.ID)
                        WHERE DiseaseId=@DiseaseId AND IsDisabled=0 
                        ORDER BY SortOrder", table), connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            indicators.Add(new Indicator
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
            return indicators;
        }

        public List<Disease> GetAllDiseases(string lang)
        {
            List<Disease> diseases = new List<Disease>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select Diseases.ID, TranslationText, Code, IsEnabled 
                    from Diseases inner join Translations on (Diseases.TranslationId = Translations.TranslationId) 
                    where IsoCode = @lang order by Code";
                OleDbCommand command = new OleDbCommand(sql, connection);
                command.Parameters.Add(new OleDbParameter("@lang", lang));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        diseases.Add(new Disease
                        {
                            Code = reader.GetValueOrDefault<string>("Code"),
                            DisplayName = reader.GetValueOrDefault<string>("TranslationText"),
                            Id = reader.GetValueOrDefault<int>("ID"),
                            IsEnabled = reader.GetValueOrDefault<bool>("IsEnabled")
                        });
                    reader.Close();
                }
            }
            return diseases;
        }

        public Disease GetDiseaseById(int id, string lang)
        {
            Disease disease = new Disease();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select Diseases.ID, TranslationText, Code, Diseases.TranslationId, IsEnabled  
                    from Diseases inner join Translations on (Diseases.TranslationId = Translations.TranslationId) 
                    where (Diseases.ID = @did AND IsoCode = @lang)";
                OleDbCommand command = new OleDbCommand(sql, connection);
                command.Parameters.Add(new OleDbParameter("@did", id));
                command.Parameters.Add(new OleDbParameter("@lang", lang));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        disease = new Disease
                        {
                            Code = reader.GetValueOrDefault<string>("Code"),
                            DisplayName = reader.GetValueOrDefault<string>("TranslationText"),
                            Id = reader.GetValueOrDefault<int>("ID"),
                            TranslationId = reader.GetValueOrDefault<string>("TranslationId"),
                            IsEnabled = reader.GetValueOrDefault<bool>("IsEnabled")
                        };
                    }
                    reader.Close();
                }

                command = new OleDbCommand(@"Select Translations.IsoCode, TranslationText, DisplayName from Translations 
                    inner join Languages on (Languages.IsoCode = Translations.IsoCode) 
                    where TranslationId = @translationId", connection);
                command.Parameters.Add(new OleDbParameter("@translationId", disease.TranslationId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        disease.TranslatedNames.Add(new TranslatedValue
                        {
                            IsoCode = reader.GetValueOrDefault<string>("IsoCode"),
                            Value = reader.GetValueOrDefault<string>("TranslationText"),
                            Language = reader.GetValueOrDefault<string>("DisplayName")
                        });
                    reader.Close();
                }
            }
            return disease;
        }

        public void Insert(Disease disease, int byUserId)
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

                    // INSERT DISEASE
                    command = new OleDbCommand(@"INSERT INTO diseases (Code, TranslationId, IsEnabled, UpdatedBy, UpdatedAt) VALUES
                    (@code, @guid, @IsEnabled, @updatedby, @updatedat)", connection);
                    command.Parameters.Add(new OleDbParameter("@code", disease.Code));
                    command.Parameters.Add(new OleDbParameter("@guid", disease.TranslationId));
                    command.Parameters.Add(new OleDbParameter("@IsEnabled", disease.IsEnabled));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.ExecuteNonQuery();

                    // INSERT translations
                    SettingsRepository.InsertTranslations(disease.TranslatedNames, byUserId, disease.TranslationId, command, connection);

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

        public void Update(Disease disease, int byUserId)
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

                    // Update DISEASE
                    command = new OleDbCommand(@"UPDATE Diseases SET Code=@code, TranslationId=@guid, IsEnabled=@IsEnabled, UpdatedBy=@updatedby, 
                        UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@code", disease.Code));
                    command.Parameters.Add(new OleDbParameter("@guid", disease.TranslationId));
                    command.Parameters.Add(new OleDbParameter("@IsEnabled", disease.IsEnabled));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", disease.Id));
                    command.ExecuteNonQuery();

                    // Update DISEASE NAMES
                    command = new OleDbCommand(@"DELETE FROM translations WHERE TranslationId = @TranslationId", connection);
                    command.Parameters.Add(new OleDbParameter("@TranslationId", disease.TranslationId));
                    command.ExecuteNonQuery();

                    // INSERT translations
                    SettingsRepository.InsertTranslations(disease.TranslatedNames, byUserId, disease.TranslationId, command, connection);

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

    }
}

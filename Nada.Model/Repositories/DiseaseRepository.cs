using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Diseases;

namespace Nada.Model.Repositories
{
    public class DiseaseRepository : RepositoryBase
    {
        #region Disease Distribution
        public DiseaseDistroPc Create(DiseaseType disease)
        {
            DiseaseDistroPc dd = new DiseaseDistroPc();
            dd.Disease = GetDiseaseById((int)disease);
            GetIndicatorsForDisease(dd.Disease.Id, dd);
            return dd;
        }

        public DiseaseDistroCm CreateCm(DiseaseType disease)
        {
            DiseaseDistroCm dd = new DiseaseDistroCm();
            dd.Disease = GetDiseaseById((int)disease);
            GetIndicatorsForDisease(dd.Disease.Id, dd);
            return dd;
        }

        public void Delete(DiseaseDistroDetails distro, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE DiseaseDistributions SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", distro.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public List<DiseaseDistroDetails> GetAllForAdminLevel(int adminLevel)
        {
            List<DiseaseDistroDetails> distros = new List<DiseaseDistroDetails>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        DiseaseDistributions.ID, 
                        Diseases.DisplayName as DiseaseName, 
                        Diseases.DiseaseType as DiseaseType,
                        DiseaseDistributions.DiseaseId, 
                        DiseaseDistributions.DateReported, 
                        DiseaseDistributions.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName,
                        created.UserName as CreatedBy, DiseaseDistributions.CreatedAt
                        FROM ((((DiseaseDistributions INNER JOIN Diseases on DiseaseDistributions.DiseaseId = Diseases.ID)
                            INNER JOIN aspnet_Users on DiseaseDistributions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN aspnet_Users created on DiseaseDistributions.CreatedById = created.UserId)
                        WHERE DiseaseDistributions.AdminLevelId=@AdminLevelId and DiseaseDistributions.IsDeleted = 0
                        ORDER BY DiseaseDistributions.DateReported DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            distros.Add(new DiseaseDistroDetails
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                TypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DiseaseName"),
                                    reader.GetValueOrDefault<string>("DiseaseName")),
                                TypeId = reader.GetValueOrDefault<int>("DiseaseId"),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                DateReported = reader.GetValueOrDefault<DateTime>("DateReported"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                                UpdatedBy = GetAuditInfo(reader)

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
            return distros;
        }

        public DiseaseDistroPc GetDiseaseDistribution(int id, int typeId)
        {
            DiseaseDistroPc diseaseDistro = Create((DiseaseType)typeId);

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select DiseaseDistributions.ID, DiseaseDistributions.Notes, DiseaseDistributions.UpdatedAt,
                        DiseaseDistributions.DateReported, DiseaseDistributions.AdminLevelId, aspnet_users.UserName, created.UserName as CreatedBy, 
                        DiseaseDistributions.CreatedAt
                        FROM ((DiseaseDistributions INNER JOIN aspnet_Users on DiseaseDistributions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on DiseaseDistributions.CreatedById = created.UserId)
                        WHERE DiseaseDistributions.ID =@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            diseaseDistro.Id = reader.GetValueOrDefault<int>("ID");
                            diseaseDistro.Notes = reader.GetValueOrDefault<string>("Notes");
                            diseaseDistro.DateReported = reader.GetValueOrDefault<DateTime>("DateReported");
                            diseaseDistro.AdminLevelId = reader.GetValueOrDefault<int>("AdminLevelId");
                            diseaseDistro.UpdatedBy = GetAuditInfo(reader);
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select 
                        DiseaseDistributionIndicatorValues.ID,   
                        DiseaseDistributionIndicatorValues.IndicatorId,
                        DiseaseDistributionIndicatorValues.DynamicValue,
                        DiseaseDistributionIndicators.DisplayName,
                        DiseaseDistributionIndicatorValues.CalcByRedistrict
                        FROM DiseaseDistributionIndicatorValues inner join DiseaseDistributionIndicators on DiseaseDistributionIndicatorValues.IndicatorId = DiseaseDistributionIndicators.ID
                        WHERE DiseaseDistributionId=@DiseaseDistributionId", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", diseaseDistro.Id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!diseaseDistro.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                                continue;
                            diseaseDistro.IndicatorValues.Add(new IndicatorValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                                DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"),
                                CalcByRedistrict = reader.GetValueOrDefault<bool>("CalcByRedistrict"),
                                Indicator = diseaseDistro.Indicators[reader.GetValueOrDefault<string>("DisplayName")]
                            });
                        }
                        reader.Close();
                    }

                    diseaseDistro.MapIndicatorsToProperties();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return diseaseDistro;
        }
        
        public void Save(List<DiseaseDistroPc> import, int userId)
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

                    foreach(var distro in import)
                        SavePc(distro, userId, connection, command);

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

        public void Save(DiseaseDistroPc distro, int userId)
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
                    
                    SavePc(distro, userId, connection, command);

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

        public void SavePc(DiseaseDistroPc distro, int userId, OleDbConnection connection, OleDbCommand command)
        {
            distro.MapIndicatorsToProperties();
            distro.MapPropertiesToIndicators();
            if (distro.Id > 0)
                command = new OleDbCommand(@"UPDATE DiseaseDistributions SET DiseaseId=@DiseaseId, AdminLevelId=@AdminLevelId,
                           DateReported=@DateReported, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO DiseaseDistributions (DiseaseId, AdminLevelId, DateReported, Notes, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DiseaseId, @AdminLevelId, @DateReported, @Notes, @UpdatedById, 
                            @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", distro.Disease.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", distro.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateReported", distro.DateReported.Value));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", distro.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (distro.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", distro.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }
            command.ExecuteNonQuery();

            if (distro.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM DiseaseDistributions", connection);
                distro.Id = (int)command.ExecuteScalar();
            }

            AddIndicatorValues(connection, distro, distro.Id, userId);
        }

        public DiseaseDistroCm GetDiseaseDistributionCm(int id, int typeId)
        {
            DiseaseDistroCm diseaseDistro = CreateCm((DiseaseType)typeId);

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select DiseaseDistributions.ID, DiseaseDistributions.Notes, DiseaseDistributions.UpdatedAt,
                        DiseaseDistributions.DateReported, DiseaseDistributions.AdminLevelId, aspnet_users.UserName, created.UserName as CreatedBy,
                        DiseaseDistributions.CreatedAt
                        FROM ((DiseaseDistributions INNER JOIN aspnet_Users on DiseaseDistributions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on DiseaseDistributions.CreatedById = created.UserId)
                        WHERE DiseaseDistributions.ID =@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            diseaseDistro.Id = reader.GetValueOrDefault<int>("ID");
                            diseaseDistro.Notes = reader.GetValueOrDefault<string>("Notes");
                            diseaseDistro.DateReported = reader.GetValueOrDefault<DateTime>("DateReported");
                            diseaseDistro.AdminLevelId = reader.GetValueOrDefault<int>("AdminLevelId");
                            diseaseDistro.UpdatedBy = GetAuditInfo(reader);
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select 
                        DiseaseDistributionIndicatorValues.ID,   
                        DiseaseDistributionIndicatorValues.IndicatorId,
                        DiseaseDistributionIndicatorValues.DynamicValue,
                        DiseaseDistributionIndicators.DisplayName,
                        DiseaseDistributionIndicatorValues.CalcByRedistrict
                        FROM DiseaseDistributionIndicatorValues inner join DiseaseDistributionIndicators on DiseaseDistributionIndicatorValues.IndicatorId = DiseaseDistributionIndicators.ID
                        WHERE DiseaseDistributionId=@DiseaseDistributionId", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", diseaseDistro.Id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(!diseaseDistro.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                                continue;

                            diseaseDistro.IndicatorValues.Add(new IndicatorValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                                DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"),
                                CalcByRedistrict = reader.GetValueOrDefault<bool>("CalcByRedistrict"),
                                Indicator = diseaseDistro.Indicators[reader.GetValueOrDefault<string>("DisplayName")]
                            });
                        }
                        reader.Close();
                    }
                    diseaseDistro.MapIndicatorsToProperties();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return diseaseDistro;
        }

//        public DiseaseDistroCm GetDistroByAdminLevelYear(int adminLevel, int yearOfReporting, int diseaseType)
//        {
//            if (yearOfReporting <= 0 || adminLevel <= 0)
//                return null;
//            int did = -1;

//            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
//            using (connection)
//            {
//                connection.Open();
//                try
//                {
//                    OleDbCommand command = null;
//                    command = new OleDbCommand(@"Select DiseaseDistributions.ID as did FROM 
//                        ((DiseaseDistributions INNER JOIN DiseaseDistributionIndicatorValues on DiseaseDistributionIndicatorValues.DiseaseDistributionId = DiseaseDistributions.ID)
//                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
//                        WHERE DiseaseDistributions.AdminLevelId=@adminlevelId AND DiseaseDistributionIndicators.DisplayName = @yearName 
//                        AND DiseaseDistributionIndicatorValues.DynamicValue = @year", connection);
//                    command.Parameters.Add(new OleDbParameter("@adminlevelId", adminLevel));
//                    command.Parameters.Add(new OleDbParameter("@yearName", "DiseaseYear"));
//                    command.Parameters.Add(new OleDbParameter("@year", yearOfReporting));
//                    using (OleDbDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.HasRows)
//                        {
//                            reader.Read();
//                            did = reader.GetValueOrDefault<int>("did");
//                        }
//                        reader.Close();
//                    }
//                }
//                catch (Exception)
//                {
//                    throw;
//                }

//                if (did <= 0)
//                    return null;

//                return GetDiseaseDistributionCm(did, diseaseType);
//            }
//        }
        
        public void Save(List<DiseaseDistroCm> import, int userId)
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

                    foreach(var distro in import)
                        SaveCm(distro, userId, connection, command);

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
        public void Save(DiseaseDistroCm distro, int userId)
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

                    SaveCm(distro, userId, connection, command);

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

        public void SaveCm(DiseaseDistroCm distro, int userId, OleDbConnection connection, OleDbCommand command)
        {
            distro.MapIndicatorsToProperties();
            distro.MapPropertiesToIndicators();
            if (distro.Id > 0)
                command = new OleDbCommand(@"UPDATE DiseaseDistributions SET DiseaseId=@DiseaseId, AdminLevelId=@AdminLevelId,
                            Notes=@Notes, DateReported=@DateReported, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO DiseaseDistributions (DiseaseId, AdminLevelId,  Notes, DateReported, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DiseaseId, @AdminLevelId, @Notes, @DateReported, @UpdatedById, 
                            @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", distro.Disease.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", distro.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", distro.Notes));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateReported", distro.DateReported.Value));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (distro.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", distro.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }
            command.ExecuteNonQuery();

            if (distro.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM DiseaseDistributions", connection);
                distro.Id = (int)command.ExecuteScalar();
            }

            AddIndicatorValues(connection, distro, distro.Id, userId);
        }

        public void SaveIndicators(IHaveDynamicIndicators model, int diseaseId, int userId)
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

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE DiseaseDistributionIndicators SET DiseaseId=@DiseaseId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, IsRequired=@IsRequired, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
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
                        command = new OleDbCommand(@"INSERT INTO DiseaseDistributionIndicators (DiseaseId, DataTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@DiseaseId, @DataTypeId, @DisplayName, @IsRequired, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();

                        command = new OleDbCommand(@"SELECT Max(ID) FROM DiseaseDistributionIndicators", connection);
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

        private void AddIndicatorValues(OleDbConnection connection, IHaveDynamicIndicatorValues distro, int id, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM DiseaseDistributionIndicatorValues WHERE DiseaseDistributionId=@DiseaseDistributionId", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in distro.IndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into DiseaseDistributionIndicatorValues (IndicatorId, DiseaseDistributionId, DynamicValue, UpdatedById, UpdatedAt, CalcByRedistrict) VALUES
                        (@IndicatorId, @DiseaseDistributionId, @DynamicValue, @UpdatedById, @UpdatedAt, @CalcByRedistrict)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@DiseaseDistributionId", id));
                command.Parameters.Add(OleDbUtil.CreateNullableParam("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.Parameters.Add(new OleDbParameter("@CalcByRedistrict", val.CalcByRedistrict));
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region Disease
        private void GetIndicatorsForDisease(int diseaseId, IHaveDynamicIndicators entity)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                entity.Indicators = new Dictionary<string, Indicator>();
                List<string> indicatorIds = new List<string>();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        DiseaseDistributionIndicators.ID,   
                        DataTypeId,
                        DisplayName,
                        IsRequired,
                        IsDisabled,
                        IsEditable,
                        IsDisplayed,
                        IsCalculated,
                        IsMetaData,
                        CanAddValues,
                        UpdatedAt, 
                        UserName,
                        DataType, 
                        NewYearType,
                        RedistrictRuleId,
                        AggTypeId,
                        MergeRuleId
                        FROM ((DiseaseDistributionIndicators INNER JOIN aspnet_users ON DiseaseDistributionIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON DiseaseDistributionIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE DiseaseId=@DiseaseId AND IsDisabled=0 
                        ORDER BY IsEditable DESC, SortOrder, DiseaseDistributionIndicators.ID", connection);
                    command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entity.Indicators.Add(reader.GetValueOrDefault<string>("DisplayName"),
                                new Indicator
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId"),
                                RedistrictRuleId = reader.GetValueOrDefault<int>("RedistrictRuleId"),
                                MergeRuleId = reader.GetValueOrDefault<int>("MergeRuleId"),
                                AggRuleId = reader.GetValueOrDefault<int>("AggTypeId"),
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString() + " by " +
                                    reader.GetValueOrDefault<string>("UserName"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                IsRequired = reader.GetValueOrDefault<bool>("IsRequired"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                IsDisplayed = reader.GetValueOrDefault<bool>("IsDisplayed"),
                                IsCalculated = reader.GetValueOrDefault<bool>("IsCalculated"),
                                IsMetaData = reader.GetValueOrDefault<bool>("IsMetaData"),
                                CanAddValues = reader.GetValueOrDefault<bool>("CanAddValues"),
                                DataType = reader.GetValueOrDefault<string>("DataType"),
                                NewYearTypeId = reader.GetValueOrDefault<int>("NewYearType")
                            });
                            indicatorIds.Add(reader.GetValueOrDefault<int>("ID").ToString());
                        }
                        reader.Close();
                    }

                    entity.IndicatorDropdownValues = GetIndicatorDropdownValues(connection, command, IndicatorEntityType.DiseaseDistribution, indicatorIds);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public List<Disease> GetSelectedDiseases()
        {
            List<Disease> diseases = new List<Disease>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select Diseases.ID, DisplayName, DiseaseType, UserDefinedName 
                    from Diseases
                    where isdeleted = 0 and IsSelected = -1 and IsHidden = 0
                    ORDER BY Diseases.DisplayName";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Disease d = GetDiseaseFromReader(reader);
                        diseases.Add(d);
                    }
                    reader.Close();
                }
            }
            return diseases;
        }

        public List<Disease> GetAvailableDiseases()
        {
            List<Disease> diseases = new List<Disease>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select Diseases.ID, DisplayName, DiseaseType, UserDefinedName 
                    from Diseases
                    where isdeleted = 0 and IsHidden = 0
                    ORDER BY Diseases.DisplayName";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Disease d = GetDiseaseFromReader(reader);
                        diseases.Add(d);
                    }
                    reader.Close();
                }
            }
            return diseases;
        }

        public Disease GetDiseaseById(int id)
        {
            Disease disease = new Disease();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select Diseases.ID, DisplayName, DiseaseType, UserDefinedName, IsSelected, UpdatedAt, 
                    aspnet_Users.UserName, CreatedAt, created.UserName as CreatedBy
                    from ((Diseases INNER JOIN aspnet_Users on Diseases.UpdatedById = aspnet_Users.UserId)
                    INNER JOIN aspnet_users created on Diseases.CreatedById = created.UserId)
                    where Diseases.ID = @did";
                OleDbCommand command = new OleDbCommand(sql, connection);
                command.Parameters.Add(new OleDbParameter("@did", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        disease = GetDiseaseFromReader(reader); ;
                    }
                    reader.Close();
                }
            }
            return disease;
        }

        private Disease GetDiseaseFromReader(OleDbDataReader reader)
        {
            var d = new Disease
            {
                DisplayName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DisplayName"),
                        reader.GetValueOrDefault<string>("DisplayName")),
                UserDefinedName = reader.GetValueOrDefault<string>("UserDefinedName"),
                DiseaseType = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DiseaseType"),
                        reader.GetValueOrDefault<string>("DiseaseType")),
                Id = reader.GetValueOrDefault<int>("ID")
            };
            if (!string.IsNullOrEmpty(d.UserDefinedName))
                d.DisplayName = d.UserDefinedName;
            return d;
        }

        public void Delete(Disease disease, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand(@"UPDATE Diseases SET IsDeleted=1 WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", disease.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Save(Disease disease, int byUserId)
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

                    if (disease.Id > 0)
                        command = new OleDbCommand(@"UPDATE Diseases SET DisplayName=@DisplayName, UserDefinedName=@UserDefinedName, DiseaseType=@DiseaseType, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID = @id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO diseases (DisplayName, UserDefinedName, DiseaseType, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                            (@DisplayName, @UserDefinedName, @DiseaseType, @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DisplayName", disease.DisplayName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@UserDefinedName", disease.UserDefinedName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DiseaseType", disease.DiseaseType));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (disease.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", disease.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }
                    command.ExecuteNonQuery();

                    if (disease.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM Diseases", connection);
                        disease.Id = (int)command.ExecuteScalar();
                        // Insert Disease Distro Year Reported
                        command = new OleDbCommand(@"INSERT INTO DiseaseDistributionIndicators (DiseaseId, DisplayName, DataTypeId, AggTypeId, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, IsMetaData, CanAddValues, SortOrder, UpdatedById, UpdatedAt) VALUES
                            (@DiseaseId, 'DateReported', 4, 5, No, No, Yes, No, False, False, False, 1, @UpdatedById, @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", disease.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
                        // Insert Disease Distro Notes
                        command = new OleDbCommand(@"INSERT INTO DiseaseDistributionIndicators (DiseaseId, DisplayName, DataTypeId, AggTypeId, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, IsMetaData, CanAddValues, SortOrder, UpdatedById, UpdatedAt) VALUES
                            (@DiseaseId, 'Notes', 15, 4, No, No, No, Yes, False, False, False, 100000, @UpdatedById, @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", disease.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
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

        public void SaveSelectedDiseases(List<Disease> diseases, bool isSelected, int byUserId)
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

                    foreach (var d in diseases)
                    {
                        command = new OleDbCommand(@"UPDATE Diseases SET IsSelected=@IsSelected, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@IsSelected", isSelected));
                        command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", d.Id));
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


    }
}

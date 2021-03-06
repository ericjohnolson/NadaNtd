﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Process;

namespace Nada.Model.Repositories
{
    /// <summary>
    /// Performs database queries for Process entities
    /// </summary>
    public class ProcessRepository : RepositoryBase
    {
        #region processes
        public List<ProcessDetails> GetAllForAdminLevel(int adminLevel)
        {
            List<ProcessDetails> processes = new List<ProcessDetails>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        processes.ID, 
                        ProcessTypes.TypeName, 
                        processes.ProcessTypeId, 
                        processes.DateReported, 
                        processes.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName
                        FROM (((processes INNER JOIN ProcessTypes on processes.ProcessTypeId = ProcessTypes.ID)
                            INNER JOIN aspnet_Users on processes.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on processes.AdminLevelId = AdminLevels.ID) 
                        WHERE processes.AdminLevelId=@AdminLevelId and processes.IsDeleted = 0 
                        ORDER BY processes.DateReported DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            processes.Add(new ProcessDetails
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                TypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TypeName"), reader.GetValueOrDefault<string>("TypeName")),
                                TypeId = reader.GetValueOrDefault<int>("ProcessTypeId"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                DateReported = reader.GetValueOrDefault<DateTime>("DateReported"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                                UpdatedBy = GetAuditInfoUpdate(reader)

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
            return processes;
        }
        
        public ProcessBase Create(int typeId)
        {
            ProcessBase Process = new ProcessBase();
            Process.ProcessType = GetProcessType(typeId);
            return Process;
        }

        public void Delete(ProcessDetails Process, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE processes SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", Process.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public ProcessBase GetById(int id)
        {
            ProcessBase Process = null;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;
                    Process = GetProcess(command, connection, id);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Process;
        }

        public ProcessBase GetProcess(OleDbCommand command, OleDbConnection connection, int pId)
        {
            var process = new ProcessBase();
            int ptypeId = 0;

            try
            {
                command = new OleDbCommand(@"Select processes.AdminLevelId, processes.DateReported, SCMDrug, PCTrainTrainingCategory,
                        processes.Notes, processes.UpdatedById, processes.UpdatedAt, aspnet_Users.UserName, AdminLevels.DisplayName, 
                        processes.ProcessTypeId, created.UserName as CreatedBy, processes.CreatedAt
                        FROM (((processes INNER JOIN aspnet_Users on processes.UpdatedById = aspnet_Users.UserId)
                            LEFT OUTER JOIN AdminLevels on processes.AdminLevelId = AdminLevels.ID)
                            INNER JOIN aspnet_Users created on processes.CreatedById = created.UserId)
                        WHERE processes.ID=@id", connection);
                command.Parameters.Add(new OleDbParameter("@id", pId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        process.Id = pId;
                        process.AdminLevelId = reader.GetValueOrDefault<Nullable<int>>("AdminLevelId");
                        process.DateReported = reader.GetValueOrDefault<DateTime>("DateReported");
                        process.SCMDrug = reader.GetValueOrDefault<string>("SCMDrug");
                        process.PCTrainTrainingCategory = reader.GetValueOrDefault<string>("PCTrainTrainingCategory");
                        process.Notes = reader.GetValueOrDefault<string>("Notes");
                        process.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                        process.UpdatedBy = GetAuditInfo(reader);
                        ptypeId = reader.GetValueOrDefault<int>("ProcessTypeId");
                    }
                    reader.Close();
                }

                process.ProcessType = GetProcessType(ptypeId);
                GetProcessIndicatorValues(connection, process);
                process.MapIndicatorsToProperties();
            }
            catch (Exception)
            {
                throw;
            }

            return process;
        }

        public void Save(List<ProcessBase> import, int userId)
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

                    foreach(var process in import)
                        Save(command, connection, process, userId);

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
        public void Save(ProcessBase process, int userId)
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

                    Save(command, connection, process, userId);

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

        #region Process Type
        public List<ProcessType> GetProcessTypes()
        {
            List<ProcessType> types = new List<ProcessType>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ProcessTypes.ID, ProcessTypes.TypeName, MAX(Diseases.DiseaseType) as DiseaseType
                        FROM ((ProcessTypes INNER JOIN ProcessTypes_to_Diseases ON ProcessTypes.ID = ProcessTypes_to_Diseases.ProcessTypeId)
                            INNER JOIN Diseases ON Diseases.ID = ProcessTypes_to_Diseases.DiseaseId) 
                        WHERE Diseases.IsSelected = yes
                        GROUP BY ProcessTypes.ID, ProcessTypes.TypeName  ", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TypeName"),
                                    reader.GetValueOrDefault<string>("TypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("TypeName");
                                
                            types.Add(new ProcessType
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayNameKey = reader.GetValueOrDefault<string>("TypeName"),
                                TypeName = name,
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

        public ProcessType GetProcessType(int id)
        {
            ProcessType process = new ProcessType();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ProcessTypes.TypeName, Diseases.DiseaseType, ProcessTypes.UpdatedAt,
                        aspnet_users.UserName, ProcessTypes.CreatedAt, created.UserName as CreatedBy 
                        FROM ((((ProcessTypes INNER JOIN aspnet_Users on ProcessTypes.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on ProcessTypes.CreatedById = created.UserId)
                            INNER JOIN ProcessTypes_to_Diseases itod on ProcessTypes.ID = itod.ProcessTypeId)
                            INNER JOIN Diseases on itod.DiseaseId = Diseases.Id) 
                        WHERE ProcessTypes.ID=@id
                        GROUP BY ProcessTypes.TypeName, Diseases.DiseaseType, ProcessTypes.UpdatedAt,
                            aspnet_users.UserName, ProcessTypes.CreatedAt, created.UserName", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TypeName"),
                                    reader.GetValueOrDefault<string>("TypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("TypeName");
                                
                            process = new ProcessType
                            {
                                Id = id,
                                DisplayNameKey = reader.GetValueOrDefault<string>("TypeName"),
                                TypeName = name,
                                UpdatedBy = GetAuditInfo(reader)
                            };
                        }
                        reader.Close();
                    }

                    List<string> indicatorIds = new List<string>();
                    command = new OleDbCommand(@"Select 
                        ProcessIndicators.ID,   
                        ProcessIndicators.DataTypeId,
                        ProcessIndicators.DisplayName,
                        ProcessIndicators.IsRequired,
                        ProcessIndicators.IsDisabled,
                        ProcessIndicators.IsEditable,
                        ProcessIndicators.IsDisplayed,
                        ProcessIndicators.CanAddValues,
                        ProcessIndicators.UpdatedAt, 
                        ProcessIndicators.RedistrictRuleId,
                        ProcessIndicators.IsCalculated,
                        ProcessIndicators.SortOrder,
                        MergeRuleId,
                        AggTypeId,
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType
                        FROM ((ProcessIndicators INNER JOIN aspnet_users ON ProcessIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON ProcessIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE ProcessTypeId=@ProcessTypeId AND IsDisabled=0 
                        ORDER BY IsEditable DESC, SortOrder, ProcessIndicators.ID", connection);
                    command.Parameters.Add(new OleDbParameter("@ProcessTypeId", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(!process.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                            {
                                process.Indicators.Add(reader.GetValueOrDefault<string>("DisplayName"),
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
                                    CanAddValues = reader.GetValueOrDefault<bool>("CanAddValues"),
                                    DataType = reader.GetValueOrDefault<string>("DataType"),
                                    IsCalculated = reader.GetValueOrDefault<bool>("IsCalculated"),
                                    SortOrder = reader.GetValueOrDefault<int>("SortOrder")
                                });
                                indicatorIds.Add(reader.GetValueOrDefault<int>("ID").ToString());
                            }
                        }
                        reader.Close();
                    }

                    process.IndicatorDropdownValues = GetIndicatorDropdownValues(connection, command, IndicatorEntityType.Process, indicatorIds);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return process;
        }

        public void Save(ProcessType model, int userId)
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
                        command = new OleDbCommand(@"UPDATE ProcessTypes SET TypeName=@TypeName,  UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO ProcessTypes (TypeName,  UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@TypeName,  @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@TypeName", model.TypeName));
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
                        command = new OleDbCommand(@"SELECT Max(ID) FROM ProcessTypes", connection);
                        model.Id = (int)command.ExecuteScalar();

                        // When inserting, assign custom disease to type
                        command = new OleDbCommand(@"INSERT INTO ProcessTypes_to_Diseases (ProcessTypeId, DiseaseId
                            ) values (@ProcessTypeId, @DiseaseId)", connection);
                        command.Parameters.Add(new OleDbParameter("@ProcessTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", (int)DiseaseType.Custom));
                        command.ExecuteNonQuery();
                        // Add year reported
                        command = new OleDbCommand(@"INSERT INTO ProcessIndicators (ProcessTypeId, DataTypeId, AggTypeId,
                            DisplayName, IsRequired, IsDisabled, IsEditable, IsDisplayed, SortOrder, UpdatedById, UpdatedAt) VALUES
                            (@ProcessTypeId, 4, 5, 'DateReported', -1, 0, 0, 0, -1, @UpdatedById, 
                             @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@ProcessTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();

                        // Add notes
                        command = new OleDbCommand(@"INSERT INTO ProcessIndicators (ProcessTypeId, DataTypeId, AggTypeId,
                            DisplayName, IsRequired, IsDisabled, IsEditable, IsDisplayed, SortOrder, UpdatedById, UpdatedAt) VALUES
                            (@ProcessTypeId, 15, 4, 'Notes', 0, 0, 0, -1, 100000, @UpdatedById, 
                             @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@ProcessTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE ProcessIndicators SET ProcessTypeId=@ProcessTypeId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, IsRequired=@IsRequired, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, IsDisplayed=@IsDisplayed, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@ProcessTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@IsDisplayed", false));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", indicator.Id));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id <= 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"INSERT INTO ProcessIndicators (ProcessTypeId, DataTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, IsDisplayed, UpdatedById, UpdatedAt) VALUES
                        (@ProcessTypeId, @DataTypeId, @DisplayName, @IsRequired, @IsDisabled, @IsEditable, @IsDisplayed, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@ProcessTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@IsDisplayed", false));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();

                        command = new OleDbCommand(@"SELECT Max(ID) FROM ProcessIndicators", connection);
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
        
        #region Private Methods
        public void Save(OleDbCommand command, OleDbConnection connection, ProcessBase process, int userId)
        {
            // Mapping year reported
            process.MapIndicatorsToProperties();
            process.MapPropertiesToIndicators();

            if (process.Id > 0)
                command = new OleDbCommand(@"UPDATE processes SET ProcessTypeId=@ProcessTypeId, AdminLevelId=@AdminLevelId, DateReported=@DateReported,
                           SCMDrug=@SCMDrug, PCTrainTrainingCategory=@PCTrainTrainingCategory,
                           Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO processes (ProcessTypeId, AdminLevelId, DateReported, SCMDrug, PCTrainTrainingCategory,
                            Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) values (@ProcessTypeId, @AdminLevelId, @DateReported, @SCMDrug, 
                            @PCTrainTrainingCategory, @Notes, @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@ProcessTypeId", process.ProcessType.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", process.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateReported", process.DateReported));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@SCMDrug", process.SCMDrug));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PCTrainTrainingCategory", process.PCTrainTrainingCategory));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", process.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (process.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", process.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }

            command.ExecuteNonQuery();

            if (process.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM processes", connection);
                process.Id = (int)command.ExecuteScalar();
            }

            AddProcessIndicatorValues(connection, process, userId);
        }

        private void AddProcessIndicatorValues(OleDbConnection connection, ProcessBase Process, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM ProcessIndicatorValues WHERE ProcessId=@ProcessId", connection);
            command.Parameters.Add(new OleDbParameter("@ProcessId", Process.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in Process.IndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into ProcessIndicatorValues (IndicatorId, ProcessId, DynamicValue, MemoValue, UpdatedById, UpdatedAt, CalcByRedistrict) VALUES
                        (@IndicatorId, @ProcessId, @DynamicValue, @MemoValue, @UpdatedById, @UpdatedAt, @CalcByRedistrict)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@ProcessId", Process.Id));
                AddValueParam(command, val);
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.Parameters.Add(new OleDbParameter("@CalcByRedistrict", val.CalcByRedistrict));
                command.ExecuteNonQuery();
            }
        }

        private void GetProcessIndicatorValues(OleDbConnection connection, ProcessBase Process)
        {
            OleDbCommand command = new OleDbCommand(@"Select 
                        ProcessIndicatorValues.ID,   
                        ProcessIndicatorValues.IndicatorId,
                        ProcessIndicatorValues.DynamicValue,
                        ProcessIndicatorValues.MemoValue,
                        ProcessIndicators.DisplayName,
                        ProcessIndicatorValues.CalcByRedistrict
                        FROM ProcessIndicatorValues INNER JOIN ProcessIndicators on ProcessIndicatorValues.IndicatorId = ProcessIndicators.ID
                        WHERE ProcessIndicatorValues.ProcessId = @ProcessId", connection);
            command.Parameters.Add(new OleDbParameter("@ProcessId", Process.Id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!Process.ProcessType.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                        continue;
                    var ind = Process.ProcessType.Indicators[reader.GetValueOrDefault<string>("DisplayName")];
                    string val = reader.GetValueOrDefault<string>("DynamicValue");
                    if (ind.DataTypeId == (int)IndicatorDataType.LargeText && !string.IsNullOrEmpty(reader.GetValueOrDefault<string>("MemoValue")))
                        val = reader.GetValueOrDefault<string>("MemoValue");
                    Process.IndicatorValues.Add(new IndicatorValue
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        DynamicValue = val,
                        CalcByRedistrict = reader.GetValueOrDefault<bool>("CalcByRedistrict"),
                        Indicator = ind
                    });
                }
                reader.Close();
            }
        }
        #endregion
    }
}

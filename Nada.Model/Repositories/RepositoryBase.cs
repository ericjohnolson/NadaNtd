using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;

namespace Nada.Model.Repositories
{
    public class RepositoryBase
    {
        public List<IndicatorDropdownValue> GetIndicatorDropdownValues(OleDbConnection connection, OleDbCommand command, IndicatorEntityType type, List<string> ids)
        {
            List<IndicatorDropdownValue> values = new List<IndicatorDropdownValue>();
            if (ids.Count == 0)
                return values;
            command = new OleDbCommand(@"Select
                        ID,
                        IndicatorId,
                        DropdownValue,
                        TranslationKey,
                        SortOrder
                        FROM IndicatorDropdownValues
                        WHERE (EntityType=@EntityType AND IndicatorId in (" + String.Join(", ", ids.ToArray()) +
                        ")) ORDER BY SortOrder", connection);
            command.Parameters.Add(new OleDbParameter("@EntityType", (int)type));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetValueOrDefault<string>("DropdownValue");
                    string transKey = reader.GetValueOrDefault<string>("TranslationKey");
                    if (!string.IsNullOrEmpty(transKey))
                        name = TranslationLookup.GetValue(transKey);

                    values.Add(new IndicatorDropdownValue 
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        SortOrder = reader.GetValueOrDefault<int>("SortOrder"), 
                        DisplayName = name 
                    });
                }
                reader.Close();
            }
            return values;
        }

        public void Save(IndicatorDropdownValue model, int userid)
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
                        command = new OleDbCommand(@"UPDATE IndicatorDropdownValues SET DropdownValue=@DropdownValue, IndicatorId=@IndicatorId, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt, EntityType=@EntityType WHERE ID = @id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO IndicatorDropdownValues (DropdownValue, IndicatorId, UpdatedById, UpdatedAt,EntityType, CreatedById, CreatedAt) VALUES
                            (@DropdownValue, @IndicatorId, @UpdatedById, @UpdatedAt, @EntityType, @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DropdownValue", model.DisplayName));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@IndicatorId", model.IndicatorId));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userid));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@EntityType", (int)model.EntityType));
                    if (model.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", model.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userid));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }
                    command.ExecuteNonQuery();

                    if (model.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM IndicatorDropdownValues", connection);
                        model.Id = (int)command.ExecuteScalar();
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

        public string GetAuditInfo(OleDbDataReader reader)
        {
            return Translations.AuditCreated + ": " + reader.GetValueOrDefault<string>("CreatedBy") + " on " + reader.GetValueOrDefault<DateTime>("CreatedAt").ToShortDateString()
                   + ", " + Translations.AuditUpdated + ": " + reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString();
        }

        public string GetAuditInfoUpdate(OleDbDataReader reader)
        {
            return Translations.AuditUpdated + ": " + reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString();
        }
        
    }
}

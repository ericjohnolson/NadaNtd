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

        public List<Disease> GetAllDiseases(string lang)
        {
            List<Disease> diseases = new List<Disease>();
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString);
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

    }
}

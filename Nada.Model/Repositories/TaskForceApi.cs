using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Nada.Globalization;

namespace Nada.Model.Repositories
{
    public class TaskForceAdminUnit
    {
        public int LevelIndex { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskForceAdminUnit Parent { get; set; }
        public int NadaId { get; set; }
    }

    public class TfJsonDistrict
    {
        public int? admin0id { get; set; }
        public string admin0 { get; set; }
        public int? admin1id { get; set; }
        public string admin1 { get; set; }
        public int? admin2id { get; set; }
        public string admin2 { get; set; }
        public int? admin3id { get; set; }
        public string admin3 { get; set; }
    }

    public class TaskForceApiResult
    {
        public TaskForceApiResult()
        {
            Units = new List<TaskForceAdminUnit>();
        }
        public bool WasSuccessful { get; set; }
        public string ErrorMsg { get; set; }
        public List<TaskForceAdminUnit> Units { get; set; }
    }

    public class TaskForceApi
    {
        public List<TaskForceAdminUnit> GetAllCountries()
        {
            List<TaskForceAdminUnit> list = new List<TaskForceAdminUnit>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select TaskForceId, Name
                    FROM TaskForceCountries ORDER BY Name", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaskForceAdminUnit
                        {
                            Id = reader.GetValueOrDefault<int>("TaskForceId"),
                            Name = reader.GetValueOrDefault<string>("Name"),
                        });
                    }
                    reader.Close();
                }
            }
            return list;
        }

        public TaskForceApiResult GetAllDistricts(string countryName)
        {
            Dictionary<string, TaskForceAdminUnit> units = new Dictionary<string, TaskForceAdminUnit>();
            int pageSize = 100;
            int pageNo = 0;
            List<TfJsonDistrict> districts = new List<TfJsonDistrict> { new TfJsonDistrict() };

            while (districts.Count > 0)
            {
                string json = GetJson(string.Format("https://gtmp.linkssystem.org/api/districts?admin0={0}&limit={1}&offset={2}", HttpUtility.UrlEncode(countryName), pageSize, pageNo * pageSize));
                if (!string.IsNullOrEmpty(json))
                {

                    districts = DeserializeJson<List<TfJsonDistrict>>(json);
                    foreach (var d in districts)
                    {
                        if (d.admin1id.HasValue && !units.ContainsKey(d.admin1id.Value + "l1"))
                        {
                            units.Add(d.admin1id.Value + "l1",
                                new TaskForceAdminUnit
                                {
                                    LevelIndex = 0,
                                    Id = d.admin1id.Value,
                                    Name = d.admin1
                                });
                        }

                        if (d.admin2id.HasValue && !units.ContainsKey(d.admin2id.Value + "l2"))
                        {
                            units.Add(d.admin2id.Value + "l2",
                                new TaskForceAdminUnit
                                {
                                    LevelIndex = 1,
                                    Parent = units[d.admin1id.Value + "l1"],
                                    Id = d.admin2id.Value,
                                    Name = d.admin2
                                });
                        }

                        if (d.admin3id.HasValue && !units.ContainsKey(d.admin3id.Value + "l3"))
                        {
                            units.Add(d.admin3id.Value + "l3",
                                   new TaskForceAdminUnit
                                   {
                                       LevelIndex = 2,
                                       Parent = units[d.admin2id.Value + "l2"],
                                       Id = d.admin3id.Value,
                                       Name = d.admin3
                                   });
                        }
                    }
                }
                else
                    districts = new List<TfJsonDistrict>();
                pageNo++;
            }

            if(units.Count > 0)
                return new TaskForceApiResult { WasSuccessful = true, Units = units.Values.ToList() };
            return new TaskForceApiResult { WasSuccessful = false, ErrorMsg = TranslationLookup.GetValue("RtiTaskForceNoResults", "RtiTaskForceNoResults") };
        }

        public static string GetJson(string url)
        {
            using (WebClient proxy = new WebClient())
            {
                byte[] resultBytes = proxy.DownloadData((new Uri(url)));

                using (MemoryStream stream = new MemoryStream(resultBytes))
                {
                    StreamReader reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
        }

        public static T DeserializeJson<T>(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

    }
}

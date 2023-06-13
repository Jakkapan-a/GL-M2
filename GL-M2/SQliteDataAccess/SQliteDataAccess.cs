using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2.SQliteDataAccess
{
    public static class SQLiteDataAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return $"Data Source={System.IO.Directory.GetCurrentDirectory()}\\{ConfigurationManager.ConnectionStrings[id]}";
        }

        public static void Execute(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, parameters);
            }
        }

        public static string GetDateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static List<T> Query<T>(string sql, Dictionary<string, object> parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>(sql, parameters);
                return output.ToList();
            }
        }

        public static bool IsExist(string sql, Dictionary<string, object> parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query(sql, parameters).FirstOrDefault();
                return output != null;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2.SQliteDataAccess
{
    public class Models
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }


        /// <summary>
        /// Save model to database
        /// </summary>
        public void Save()
        {
            string sql = "INSERT INTO models (name, description, image, created_at, updated_at) VALUES (@name, @description, @image, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", this.name);
            parameters.Add("@description", this.description);
            parameters.Add("@image", this.image);
            parameters.Add("@created_at", SQLiteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQLiteDataAccess.GetDateTimeNow());
            SQLiteDataAccess.Execute(sql, parameters);
        }

        /// <summary>
        /// Update model to database
        /// </summary>
        public void Update()
        {

            string sql = "UPDATE models SET name = @name, description = @description, image = @image, updated_at = @updated_at WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            parameters.Add("@name", this.name);
            parameters.Add("@description", this.description);
            parameters.Add("@image", this.image);
            parameters.Add("@updated_at", SQLiteDataAccess.GetDateTimeNow());
            SQLiteDataAccess.Execute(sql, parameters);
        }

        /// <summary>
        /// Delete model from database
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE FROM models WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            SQLiteDataAccess.Execute(sql, parameters);

        }
    }
}

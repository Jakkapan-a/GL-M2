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

        /// <summary>
        /// Get all models from database
        /// </summary>
        /// <returns></returns>
        public static List<Models> GetAll()
        {
            string sql = "SELECT * FROM models ORDER BY id ASC LIMIT 3000";
            return SQLiteDataAccess.Query<Models>(sql);
        }

        /// <summary>
        /// Get model by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Models Get(int id)
        {
            string sql = "SELECT * FROM models WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            return SQLiteDataAccess.Query<Models>(sql, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Get model by start limit from database
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Models> Get(int start, int limit)
        {
            string sql = "SELECT * FROM models ORDER BY id DESC LIMIT @start, @limit";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@start", start);
            parameters.Add("@limit", limit);
            return SQLiteDataAccess.Query<Models>(sql, parameters);
        }

        /// <summary>
        /// Get model by name from database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Models> Get(string name)
        {
            string sql = "SELECT * FROM models WHERE name LIKE @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", "%"+name+"%");
            return SQLiteDataAccess.Query<Models>(sql, parameters);
        }
        public static Models GetByName(string name)
        {
            string sql = "SELECT * FROM models WHERE name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name );
            return SQLiteDataAccess.Query<Models>(sql, parameters).FirstOrDefault() as Models;
        }
        /// <summary>
        /// Get model by name and id from database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Models Get(string name, int id)
        {
            string sql = "SELECT * FROM models WHERE name = @name AND id != @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@id", id);
            return SQLiteDataAccess.Query<Models>(sql, parameters).FirstOrDefault();
        }

        /// <summary>
        /// IsExist bool by name from database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsExist(string name)
        {
            string sql = "SELECT * FROM models WHERE name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            return SQLiteDataAccess.Query<Models>(sql, parameters).Any();
        }

        /// <summary>
        /// IsImageExist bool by image from database return true if image exist
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>

        public static bool IsImageExist(string image)
        {
            string sql = "SELECT * FROM models WHERE image = @image";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@image", image);
            return SQLiteDataAccess.Query<Models>(sql, parameters).Any();
        }

        /// <summary>
        /// IsExist bool by name and id from database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsExist(string name, int id)
        {
            string sql = "SELECT * FROM models WHERE name = @name AND id != @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@id", id);
            return SQLiteDataAccess.Query<Models>(sql, parameters).Any();
        }

        /// <summary>
        /// Get last id from database
        /// </summary>
        /// <returns></returns>
        public static int GetLastId()
        {
            string sql = "SELECT id FROM models ORDER BY id DESC LIMIT 1";
            return SQLiteDataAccess.Query<Models>(sql).FirstOrDefault().id;
        }
    }
}

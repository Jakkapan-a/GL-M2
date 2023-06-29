using System.Collections.Generic;
using System.Linq;

namespace GL_M2.SQliteDataAccess
{
    public class Images
    {
        public int id { get; set; }
        public int model_id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public void Save(){
            string query = "INSERT INTO images (model_id, name, created_at, updated_at) VALUES (@model_id, @name, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@model_id", this.model_id);
            parameters.Add("@name", this.name);
            parameters.Add("@created_at", SQLiteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at",  SQLiteDataAccess.GetDateTimeNow());            
            SQLiteDataAccess.Execute(query, parameters);
        }

        public void Update(){
            string query = "UPDATE images SET model_id = @model_id, name = @name, updated_at = @updated_at WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            parameters.Add("@model_id", this.model_id);
            parameters.Add("@name", this.name);
            parameters.Add("@updated_at",  SQLiteDataAccess.GetDateTimeNow());            
            SQLiteDataAccess.Execute(query, parameters);
        }

        public void Delete(){
            string query = "DELETE FROM images WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            SQLiteDataAccess.Execute(query, parameters);
        }

        public static List<Images> All(){
            string query = "SELECT * FROM images ORDER BY id DESC LIMIT 100";
            return SQLiteDataAccess.Query<Images>(query);
        }

        public static Images Get(int id){
            string query = "SELECT * FROM images WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            return SQLiteDataAccess.Query<Images>(query, parameters).FirstOrDefault();
        }

        public static List<Images> GetByModelId(int model_id){
            string query = "SELECT * FROM images WHERE model_id = @model_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@model_id", model_id);
            return SQLiteDataAccess.Query<Images>(query, parameters);
        }

        public static bool IsImageExist(string name){
            string query = "SELECT * FROM images WHERE name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            return SQLiteDataAccess.Query<Images>(query, parameters).Any();
        }
    }
}

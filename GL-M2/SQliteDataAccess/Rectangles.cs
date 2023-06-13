using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2.SQliteDataAccess
{
    public class Rectangles
    {
        public int id { get; set; }
        public int model_id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        /// <summary>
        /// Save rectangle to database
        /// </summary>
        public void Save()
        {
            string sql = "INSERT INTO rectangles (model_id, x, y, width, height, created_at, updated_at) VALUES (@model_id, @x, @y, @width, @height, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@model_id", this.model_id);
            parameters.Add("@x", this.x);
            parameters.Add("@y", this.y);
            parameters.Add("@width", this.width);
            parameters.Add("@height", this.height);
            parameters.Add("@created_at", SQLiteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQLiteDataAccess.GetDateTimeNow());
            SQLiteDataAccess.Execute(sql, parameters);
        }

        /// <summary>
        /// Update rectangle to database
        /// </summary>
        public void Update()
        {
            string sql = "UPDATE rectangles SET model_id = @model_id, x = @x, y = @y, width = @width, height = @height, updated_at = @updated_at WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            parameters.Add("@model_id", this.model_id);
            parameters.Add("@x", this.x);
            parameters.Add("@y", this.y);
            parameters.Add("@width", this.width);
            parameters.Add("@height", this.height);
            parameters.Add("@updated_at", SQLiteDataAccess.GetDateTimeNow());
            SQLiteDataAccess.Execute(sql, parameters);
        }

        /// <summary>
        /// Delete rectangle from database
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE FROM rectangles WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", this.id);
            SQLiteDataAccess.Execute(sql, parameters);
        }
    }
}

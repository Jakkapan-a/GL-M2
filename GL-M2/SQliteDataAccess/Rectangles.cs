﻿using System;
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

        public Rectangles()
        {
            this.x = 0; 
            this.y = 0; 
            this.width = 0; 
            this.height = 0;
        }
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

        /// <summary>
        /// Delete rectangle from database
        /// </summary>
        public static void Delete(int id)
        {
            string sql = "DELETE FROM rectangles WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQLiteDataAccess.Execute(sql, parameters);
        }
        
        /// <summary>
        /// Get all rectangles from database
        /// </summary>
        /// <returns></returns>
        public static List<Rectangles> GetAll()
        {
            string sql = "SELECT * FROM rectangles ORDER BY id DESC LIMIT 100";
            return SQLiteDataAccess.Query<Rectangles>(sql);
        }

        /// <summary>
        /// Get rectangle by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Rectangles Get(int id)
        {
            string sql = "SELECT * FROM rectangles WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            return SQLiteDataAccess.Query<Rectangles>(sql, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Get rectangles by model_id from database
        /// </summary>
        /// <param name="model_id"></param>
        /// <returns></returns>
        public static List<Rectangles> GetByModelId(int model_id)
        {
            string sql = "SELECT * FROM rectangles WHERE model_id = @model_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@model_id", model_id);
            return SQLiteDataAccess.Query<Rectangles>(sql, parameters);
        }

        /// <summary>
        /// Get rectangles by model_id from database
        /// </summary>
        /// <param name="model_id"></param>
        /// <returns></returns>
        public static List<Rectangles> GetByModelIdAndDate(int model_id, string date)
        {
            string sql = "SELECT * FROM rectangles WHERE model_id = @model_id AND created_at LIKE @date";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@model_id", model_id);
            parameters.Add("@date", date + "%");
            return SQLiteDataAccess.Query<Rectangles>(sql, parameters);
        }
    }
}
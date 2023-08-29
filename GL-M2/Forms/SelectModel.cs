using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GL_M2.Forms
{
    public partial class SelectModel : Form
    {
        public SelectModel()
        {
            InitializeComponent();
        }

        private void SelectModel_Load(object sender, EventArgs e)
        {
            RandersDGV();
        }

        public delegate void SetNameDoubleClickHandler(string name);
        public event SetNameDoubleClickHandler OnSelect;


        private void RandersDGV(){
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("no", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("date", typeof(DateTime));

            var models = SQliteDataAccess.Models.GetAll();
            int no = 1;
            foreach (var model in models)
            {
                dt.Rows.Add(model.id, no, model.name, model.description, model.created_at);
                no++;
            }

            dgvModels.DataSource = dt;
            dgvModels.Columns["id"].Visible = false;
            

            // Set Header Text
            dgvModels.Columns["no"].HeaderText = "No";
            dgvModels.Columns["name"].HeaderText = "Name";
            dgvModels.Columns["description"].HeaderText = "Description";
            dgvModels.Columns["date"].HeaderText = "Date";
            
        }

        private void dgvModels_DoubleClick(object sender, EventArgs e)
        {
            if (dgvModels.SelectedRows.Count > 0)
            {
                OnSelect?.Invoke(dgvModels.SelectedRows[0].Cells["name"].Value.ToString());
                this.Close();
            }
        }
    }
}

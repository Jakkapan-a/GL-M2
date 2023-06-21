using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Rec = GL_M2.Forms.Rectangles;
namespace GL_M2.Forms
{
    public partial class Models : Form
    {
        public Models()
        {
            InitializeComponent();
           
        }

        private int id;
        private bool isRenderingTable  = false;
        private STATUS status;
        private void Models_Load(object sender, EventArgs e)
        {
            RenderTable();
            status = STATUS.NONE;
            Directory.CreateDirectory(Properties.Resources.path_image);
            // Get selected old row
            isRenderingTable = false;
            selectedRow = 1;
            SelectTableRow(1);
            SelectTableRow(0);
            selectedRow = -1;
        }

        private void SelectTableRow(int rowIndex)
        {
            if (rowIndex != -1 && rowIndex < dgvModels.Rows.Count && dgvModels.Rows.Count > 1)
            {
                dgvModels.Rows[rowIndex].Selected = true;
                dgvModels.CurrentCell = dgvModels.Rows[rowIndex].Cells[1];
            }
        }

        private void RenderTable()
        {
            var models = SQliteDataAccess.Models.GetAll();
            RenderDataTable(models);
        }

        private void RenderTable(string name)
        {
            var models = SQliteDataAccess.Models.Get(name);
            RenderDataTable(models);
        }

        private int selectedRow = -1;
        private void RenderDataTable(IEnumerable<SQliteDataAccess.Models> models)
        {
            isRenderingTable  = models.Count() == 1? false : true;
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("no", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("image", typeof(string));
            dt.Columns.Add("updated_at", typeof(DateTime));

            int i = 0;
            foreach (var model in models)
            {
                dt.Rows.Add(model.id, ++i, model.name, model.description, model.image, model.updated_at);
            }

            // Set header column name
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["no"].ColumnName = "No";
            dt.Columns["name"].ColumnName = "Name";
            dt.Columns["description"].ColumnName = "Description";
            dt.Columns["image"].ColumnName = "Image";
            dt.Columns["updated_at"].ColumnName = "Date";

            dgvModels.DataSource = dt;

            // Hide id column
            dgvModels.Columns["ID"].Visible = false;
            dgvModels.Columns["Image"].Visible = false;

            // Set column  No width 15%
            dgvModels.Columns["No"].Width = dgvModels.Width * 15 / 100;

            isRenderingTable  = false;

            // Get selected old row
            if (selectedRow != -1 && selectedRow < dgvModels.Rows.Count && dgvModels.Rows.Count > 0)
            {
                dgvModels.Rows[selectedRow].Selected = true;
                dgvModels.CurrentCell = dgvModels.Rows[selectedRow].Cells[1];
            }
            else if (dgvModels.Rows.Count > 0)
            {
                dgvModels.Rows[0].Selected = true;
                dgvModels.CurrentCell = dgvModels.Rows[0].Cells[1];
            }

        }

        private void dgvModels_SelectionChanged(object sender, EventArgs e)
        {
            if (isRenderingTable )
                return;

            // Get selected row id
            if (dgvModels.SelectedRows.Count > 0)
            {
                this.id = Convert.ToInt32(dgvModels.SelectedRows[0].Cells["ID"].Value);
                toolStripStatusLabel_id.Text = "ID :" + this.id;
                // Disable text
                txtModel.Enabled = false;
                txtDescription.Enabled = false;
                btnLoad.Enabled = false;
                // Set text
                txtModel.Text = dgvModels.SelectedRows[0].Cells["Name"].Value.ToString();
                txtDescription.Text = dgvModels.SelectedRows[0].Cells["Description"].Value.ToString();
                // Set image
                pgMaster.Image?.Dispose();
                if (File.Exists(Path.Combine(Properties.Resources.path_image, dgvModels.SelectedRows[0].Cells["Image"].Value.ToString())))
                {
                    using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, dgvModels.SelectedRows[0].Cells["Image"].Value.ToString()), FileMode.Open, FileAccess.Read))
                    {

                        pgMaster.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pgMaster.Image?.Dispose();
                    pgMaster.Image = null;

                }
                // Save selected row
                selectedRow = dgvModels.SelectedRows[0].Index;
            }

            status = STATUS.NONE;
        }

        private void InitiateInputFields()
        {
            // Enable text
            txtModel.Enabled = true;
            txtDescription.Enabled = true;
            btnLoad.Enabled = true;
            // Clear text
            txtModel.Text = "";
            txtDescription.Text = "";
            // Set focus
            this.ActiveControl = txtModel;
            txtModel.Focus();
        }

        private bool ValidateInputFields()
        {
            if (txtModel.Text == "")
            {
                ShowErrorMessage("Please enter model name");
                return false;
            }

            if (txtDescription.Text == "")
            {
                ShowErrorMessage("Please enter description");
                return false;
            }

            if (SQliteDataAccess.Models.IsExist(txtModel.Text.Trim(), this.id))
            {
                ShowErrorMessage("Model name already exist");
                return false;
            }

            return true;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.ActiveControl = txtModel;
            txtModel.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            pgMaster.Image?.Dispose();
            this.id = 0;
            toolStripStatusLabel_id.Text = "ID :" + this.id;
            InitiateInputFields();
            isImageChange = false;
            status = STATUS.NEW;
            // Clear selected row
            dgvModels.ClearSelection();
        }
        private bool isImageChange = false;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            isImageChange = false;
            // Enable text
            txtModel.Enabled = true;
            txtDescription.Enabled = true;
            btnLoad.Enabled = true;
            // Set focus
            this.ActiveControl = txtModel;
            txtModel.Focus();

            status = STATUS.EDIT;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate text
            if (!ValidateInputFields()) return;

            if (this.id == 0)
            {
                // New
                string filename = "";
                if (pgMaster.Image != null)
                {
                    filename = Guid.NewGuid().ToString() + ".jpg";
                    filename = filename.Replace("-", "_");
                    // Save image
                    pgMaster.Image.Save(Path.Combine(Properties.Resources.path_image, filename), ImageFormat.Jpeg);
                }

                SQliteDataAccess.Models model = new SQliteDataAccess.Models();
                model.name = txtModel.Text.Trim();
                model.description = txtDescription.Text.Trim();
                model.image = filename;
                model.Save();
                selectedRow++;
            }
            else if (this.id > 0)
            {
                // Update
                string filename = "";

                SQliteDataAccess.Models model = SQliteDataAccess.Models.Get(this.id);
                model.name = txtModel.Text.Trim();
                model.description = txtDescription.Text.Trim();

                if (pgMaster.Image != null && isImageChange)
                {
                    filename = Guid.NewGuid().ToString() + ".jpg";
                    filename = filename.Replace("-", "_");
                    // Save image
                    pgMaster.Image.Save(Path.Combine(Properties.Resources.path_image, filename), ImageFormat.Jpeg);
                    // Delete old image
                    if (File.Exists(Path.Combine(Properties.Resources.path_image, model.image)))
                    {
                        File.Delete(Path.Combine(Properties.Resources.path_image, model.image));
                    }
                    model.image = filename;
                }
                model.Update();
            }

            txtModel.Enabled = false;
            txtDescription.Enabled = false;
            btnLoad.Enabled = false;
            // Clear text
            txtModel.Text = "";
            txtDescription.Text = "";
            // Render table
            RenderTable();
        }

        private void pgMaster_ImageChanged(object sender, EventArgs e)
        {
            isImageChange = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                RenderTable();
                return;
            }
            RenderTable(txtSearch.Text.Trim());

            this.id = 0;
            toolStripStatusLabel_id.Text = "ID :" + this.id;

            // Clear text
            txtModel.Text = "";
            txtDescription.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validate ID
            if (this.id == 0)
            {
                MessageBox.Show("Please select model", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm delete
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete this model?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // Delete
                SQliteDataAccess.Models model = SQliteDataAccess.Models.Get(this.id);
                // Delete image
                if (File.Exists(Path.Combine(Properties.Resources.path_image, model.image)))
                {
                    File.Delete(Path.Combine(Properties.Resources.path_image, model.image));
                }
                // Clear text
                txtModel.Text = "";
                txtDescription.Text = "";
                // Render table
                model.Delete();
                RenderTable();
            }
        }

        private Forms.Rectangles rectangles;
        private void btnSet_Click(object sender, EventArgs e)
        {
            rectangles?.Dispose();
            if (this.id == 0)
            {
                MessageBox.Show("Please select model", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            rectangles = new Forms.Rectangles(this.id);
            rectangles.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Open file dialog image only
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Select image";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load image read only
                using (Stream stream = File.OpenRead(openFileDialog.FileName))
                {
                    pgMaster.Image = Image.FromStream(stream);
                }
            }
        }
    }
}
using GL_M2.Controls;
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
        private Main main;

        public Models(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        private int id;
        private bool isRenderingTable = false;
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

        private void SelectTableRow(int rowIndex, int columnIndex = 1)
        {
            if (rowIndex != -1 && rowIndex < dgvModels.Rows.Count && dgvModels.Rows.Count > columnIndex)
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
            isRenderingTable = models.Count() == 1 ? false : true;
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
            
            int oldSelectedRow = selectedRow;

            dgvModels.DataSource = dt;

            // Hide id column
            dgvModels.Columns["ID"].Visible = false;
            dgvModels.Columns["Image"].Visible = false;

            // Set column  No width 15%
            dgvModels.Columns["No"].Width = dgvModels.Width * 15 / 100;

            isRenderingTable = false;

            // Get selected old row
            SelectTableRow(selectedRow, 0);

        }

        private void dgvModels_SelectionChanged(object sender, EventArgs e)
        {
            if (isRenderingTable)
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
                if (File.Exists(Path.Combine(Properties.Resources.path_image, dgvModels.SelectedRows[0].Cells["Image"].Value.ToString())))
                {
                    using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, dgvModels.SelectedRows[0].Cells["Image"].Value.ToString()), FileMode.Open, FileAccess.Read))
                    {
                        pgMaster.Image = Image.FromStream(fs);
                        using (Bitmap bitmap = new Bitmap(pgMaster.Image.Width, pgMaster.Image.Height))
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.DrawImage(this.pgMaster.Image, 0, 0);

                            var rectangles = SQliteDataAccess.Rectangles.GetByModelId(this.id);

                            foreach (var rec in rectangles)
                            {
                                Color color = Properties.Settings.Default.point_color;
                                DrawRectangleToImage(bitmap, rec.x, rec.y, rec.width, rec.height, color);
                            }
                            UpdatePictureBoxImage(bitmap);
                        }
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
            btnSave.Text = "Save";
            status = STATUS.NONE;
        }
        private void DrawRectangleToImage(Bitmap bitmap, int x, int y, int width, int height, Color color, float penWidth = 2)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawRectangle(new Pen(color, penWidth), x, y, width, height);
            }
        }

        private void UpdatePictureBoxImage(Bitmap newImage)
        {
            pgMaster.Image?.Dispose();
            pgMaster.Image = (Image)newImage.Clone();
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
            pgMaster.Image?.Dispose();
            pgMaster.Image = null;
            // Clear selected row
            dgvModels.ClearSelection();


            btnSave.Text = "Save";
        }
        private bool isImageChange = false;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            isImageChange = false;
            // Enable text
            txtModel.Enabled = true;
            txtDescription.Enabled = true;
            btnLoad.Enabled = true;

            btnSave.Text = "Update";
            // Set focus
            this.ActiveControl = txtModel;
            txtModel.Focus();

            status = STATUS.EDIT;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate text
            if (!ValidateInputFields()) return;

            if (status == STATUS.NEW)
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

                if (images_clone != null)
                {
                    int model_last_id = SQliteDataAccess.Models.GetLastId();

                    if(model_last_id != 0)
                    {
                        foreach (var im in images_clone)
                        {
                            string filename_image = DateTime.Now.ToString("yyyyMMdd_hhmmss") + Guid.NewGuid().ToString() + ".jpg";
                            filename_image = filename_image.Replace("-", "_");
                            // Save image
                            im.Value.Save(Path.Combine(Properties.Resources.path_image, filename_image), ImageFormat.Jpeg);
                            SQliteDataAccess.Images img = new SQliteDataAccess.Images();
                            img.model_id = model_last_id;
                            img.name = filename_image;
                            img.Save();
                        }
                    }
                }
            }
            else if (status == STATUS.EDIT)
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

                    if (images_clone != null)
                    {
                        var images = SQliteDataAccess.Images.GetByModelId(model.id);
                        foreach(var img in images)
                        {
                            // Check image exist and move to recycle bin
                            if (File.Exists(Path.Combine(Properties.Resources.path_image, img.name)))
                            {
                                // Move to recycle bin
                                 Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(Path.Combine(Properties.Resources.path_image, img.name), Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                            }
                            // Delete image from database
                            img.Delete();
                        }

                        foreach (var im in images_clone)
                        {
                            string filename_image = DateTime.Now.ToString("yyyyMMdd_hhmmss")+Guid.NewGuid().ToString() + ".jpg";
                            filename_image = filename_image.Replace("-", "_");
                            // Save image
                            im.Value.Save(Path.Combine(Properties.Resources.path_image, filename_image), ImageFormat.Jpeg);
                            SQliteDataAccess.Images img = new SQliteDataAccess.Images();
                            img.model_id = this.id;
                            img.name = filename_image;
                            img.Save();
                        }
                    }
                }
                model.Update();


            }

            btnSave.Text = "Save";
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
            rectangles.FormClosed += Rectangles_FormClosed;
            rectangles.Show();
        }

        private void Rectangles_FormClosed(object sender, FormClosedEventArgs e)
        {
            //RenderTable();
            dgvModels_SelectionChanged(sender, e);
        }

        private Dictionary<int, Image> images_clone;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.main.image != null)
            {
                pgMaster.Image?.Dispose();
                pgMaster.Image = (Image)this.main.image.Clone();

                if (this.main.imagesQueue != null)
                {
                    images_clone = new Dictionary<int, Image>(this.main.imagesQueue.Count);
                    int no = 0;
                    foreach (var kv in this.main.imagesQueue)
                    {
                        images_clone.Add(no, (Image)kv.Clone());
                        no++;
                    }
                }
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = txtDescription;
                txtDescription.Focus();
            }
        }

        private Preview preview;
        private void btnPreview_Click(object sender, EventArgs e)
        {
            preview?.Dispose();
            if (pgMaster.Image == null) return;
            if (id < 1) return;
            var rectangles = SQliteDataAccess.Rectangles.GetByModelId(this.id);
            using (Bitmap bitmap = new Bitmap(pgMaster.Image.Width, pgMaster.Image.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                foreach (var r in rectangles)
                {
                    graphics.DrawImage(pgMaster.Image, r.x, r.y, r.width, r.height);
                    graphics.DrawRectangle(new Pen(Color.Red, 1), r.x, r.y, r.width, r.height);
                }

                preview = new Preview(bitmap);
                preview.Show();
            }
        }
    }
}
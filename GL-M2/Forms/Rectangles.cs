using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GL_M2.Forms
{
    public partial class Rectangles : Form
    {
        private int model_id;
        private int id;
        public Rectangles(int model_id)
        {
            InitializeComponent();
            this.model_id = model_id;
        }
        private Image image;
        List<SQliteDataAccess.Rectangles> rectangles;

        private int selectedRow = -1;
        private bool isRenderingTable = false;

        private void Rectangles_Load(object sender, EventArgs e)
        {
            //
            SQliteDataAccess.Models model = SQliteDataAccess.Models.Get(this.model_id);
            // Delete image
            if (!File.Exists(Path.Combine(Properties.Resources.path_image, model.image)))
            {
                this.Close();
                return;
            }

            using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, model.image), FileMode.Open, FileAccess.Read))
            {
                image = Image.FromStream(fs);
                scrollablePictureBox.Image = Image.FromStream(fs);
            }
            RenderTable();

            SelectTableRow(1);
            SelectTableRow(0);
        }
        private void SelectTableRow(int rowIndex)
        {
            if (rowIndex != -1 && rowIndex < dgvRectangles.Rows.Count && dgvRectangles.Rows.Count > 1)
            {
                dgvRectangles.Rows[rowIndex].Selected = true;
                dgvRectangles.CurrentCell = dgvRectangles.Rows[rowIndex].Cells[1];
            }
        }
        private void RenderTable()
        {
            //var models = SQliteDataAccess.Models.GetAll();
            rectangles = SQliteDataAccess.Rectangles.GetByModelId(this.model_id);
            RenderDataTable(rectangles);
        }

        private void RenderDataTable(IEnumerable<SQliteDataAccess.Rectangles> rectangles)
        {
            isRenderingTable = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("no", typeof(int));
            dt.Columns.Add("data", typeof(string));
            dt.Columns.Add("updated_at", typeof(DateTime));

            int i = 0;
            foreach (var rec in rectangles)
            {
                dt.Rows.Add(rec.id, ++i, getRectanglesName(rec), rec.updated_at);
            }

            // Set header column name
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["no"].ColumnName = "No";
            dt.Columns["data"].ColumnName = "Data";
            dt.Columns["updated_at"].ColumnName = "Date";

            dgvRectangles.DataSource = dt;

            // Hide id column
            dgvRectangles.Columns["ID"].Visible = false;
            // Set column  No width 15%
            dgvRectangles.Columns["No"].Width = dgvRectangles.Width * 15 / 100;

            isRenderingTable = false;

            // Get selected old row
            if (selectedRow != -1 && selectedRow < dgvRectangles.Rows.Count && dgvRectangles.Rows.Count > 0)
            {
                dgvRectangles.Rows[selectedRow].Selected = true;
                dgvRectangles.CurrentCell = dgvRectangles.Rows[selectedRow].Cells[1];
            }
            else if (dgvRectangles.Rows.Count > 0)
            {
                dgvRectangles.Rows[0].Selected = true;
                dgvRectangles.CurrentCell = dgvRectangles.Rows[0].Cells[1];
            }
        }

        private string getRectanglesName(SQliteDataAccess.Rectangles rectangles)
        {
            return "XYWH : (" + rectangles.x + ", " + rectangles.y + ", " + rectangles.width + ", " + rectangles.height + ")";

        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            npX.Enabled = true;
            npY.Enabled = true;
            npWidth.Enabled = true;
            npHeight.Enabled = true;
            btnSave.Enabled = true;

            id = 0;

            npX.Value = 0;
            npY.Value = 0;
            npWidth.Value = 10;
            npHeight.Value = 10;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            npX.Enabled = true;
            npY.Enabled = true;
            npWidth.Enabled = true;
            npHeight.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate
            if (npX.Value == 0 || npY.Value == 0 || npWidth.Value == 0 || npHeight.Value == 0)
            {
                MessageBox.Show("Please enter all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save
            SQliteDataAccess.Rectangles rectangles = new SQliteDataAccess.Rectangles();
            rectangles.model_id = this.model_id;
            rectangles.x = (int)npX.Value;
            rectangles.y = (int)npY.Value;
            rectangles.width = (int)npWidth.Value;
            rectangles.height = (int)npHeight.Value;

            if (id > 0)
            {
                rectangles.id = id;
                rectangles.Update();
            }
            else
            {
                rectangles.Save();
            }

            // Reload table
            RenderTable();

            npX.Enabled = false;
            npY.Enabled = false;
            npWidth.Enabled = false;
            npHeight.Enabled = false;
            btnSave.Enabled = false;
        }

        private void np_ValueChanged(object sender, EventArgs e)
        {

        }
        private void DrawRectanglesOnImage()
        {
            using (Bitmap bitmap = new Bitmap(scrollablePictureBox.Image.Width, scrollablePictureBox.Image.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(this.image, 0, 0);

                foreach (var rec in rectangles)
                {
                    Color color = rec.id == id ? Color.Blue : Color.Red;
                    DrawRectangleToImage(bitmap, rec.x, rec.y, rec.width, rec.height, color);
                }

                if (id == 0)
                {
                    DrawRectangleToImage(bitmap, (int)npX.Value, (int)npY.Value, (int)npWidth.Value, (int)npHeight.Value, Color.Yellow);
                }

                UpdatePictureBoxImage(bitmap);
            }
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
            scrollablePictureBox.Image?.Dispose();
            scrollablePictureBox.Image = (Image)newImage.Clone();
        }
        private void DrawRectangleToImage(int x, int y, int width, int height)
        {
            using (Bitmap bmp = new Bitmap(scrollablePictureBox.Image.Width, scrollablePictureBox.Image.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // draw the image onto the graphics
                    g.DrawImage(image, 0, 0);
                    g.DrawRectangle(new Pen(Color.Green, 2), x, y, width, height);
                }

                scrollablePictureBox.Image?.Dispose();
                scrollablePictureBox.Image = (Image)bmp.Clone();
            }

            scrollablePictureBox.Update();
        }

        private void DrawRectangleToImage(Bitmap bitmap, int x, int y, int width, int height)
        {
            if (bitmap == null) return;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawRectangle(new Pen(Color.Red, 2), x, y, width, height);
            }
        }
        private void dgvRectangles_SelectionChanged(object sender, EventArgs e)
        {
            if (isRenderingTable) return;
            if (dgvRectangles.SelectedRows.Count == 0) return;
            selectedRow = dgvRectangles.SelectedRows[0].Index;
            id = (int)dgvRectangles.SelectedRows[0].Cells["ID"].Value;
            SQliteDataAccess.Rectangles rectangles = SQliteDataAccess.Rectangles.Get(id);
            npX.Value = rectangles.x;
            npY.Value = rectangles.y;
            npWidth.Value = rectangles.width;
            npHeight.Value = rectangles.height;

            npX.Enabled = false;
            npY.Enabled = false;
            npWidth.Enabled = false;
            npHeight.Enabled = false;
            btnSave.Enabled = false;
        }
    }
}

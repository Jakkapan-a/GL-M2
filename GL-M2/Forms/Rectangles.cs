using OfficeOpenXml;
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
using System.Windows.Media;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

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
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, model.image), FileMode.Open, FileAccess.Read))
                {
                    image = Image.FromStream(fs);
                    scrollablePictureBox.Image = Image.FromStream(fs);
                }
            }
            catch
            {
                this.Close();
                return;
            }
            RenderTable();

            SelectTableRow(1);
            SelectTableRow(0);

            // Set default color
            cbPoint.SelectedValue = Properties.Settings.Default.point_color;
            cbCurrentPoint.SelectedValue = Properties.Settings.Default.current_point_color;
            cbNewPoint.SelectedValue = Properties.Settings.Default.new_point_color;

        }
        private void SelectTableRow(int rowIndex, int columnIndex = 1)
        {
            if (rowIndex != -1 && rowIndex < dgvRectangles.Rows.Count && dgvRectangles.Rows.Count > columnIndex)
            {
                dgvRectangles.Rows[rowIndex].Selected = true;
                dgvRectangles.CurrentCell = dgvRectangles.Rows[rowIndex].Cells[1];
            }
            else if (dgvRectangles.Rows.Count > 0)
            {
                dgvRectangles.Rows[0].Selected = true;
                dgvRectangles.CurrentCell = dgvRectangles.Rows[0].Cells[1];
            }
        }
        private void RenderTable()
        {
            rectangles = SQliteDataAccess.Rectangles.GetByModelId(this.model_id);
            RenderDataTable(rectangles);
        }

        private void RenderDataTable(IEnumerable<SQliteDataAccess.Rectangles> rectangles)
        {
            isRenderingTable = rectangles.Count() == 1 ? false : true;
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

            // Get selected row
            SelectTableRow(selectedRow, 0);
        }

        private string getRectanglesName(SQliteDataAccess.Rectangles rectangles)
        {
            return "X,Y,W,H :(" + rectangles.x + ", " + rectangles.y + ", " + rectangles.width + ", " + rectangles.height + ")";

        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            SetControlStates(true);
            id = 0;
            npX.Value = 0;
            npY.Value = 0;
            npWidth.Value = 10;
            npHeight.Value = 10;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetControlStates(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this rectangle?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SQliteDataAccess.Rectangles.Delete(id);
                    RenderTable();
                    SelectTableRow(selectedRow);
                    DrawRectanglesOnImage();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate
            if (!ValidateNumericInputs()) return;
            // Save
            var rectangle = CreateRectangle();

            if (id > 0)
            {
                rectangle.id = id;
                rectangle.Update();
            }
            else
            {
                rectangle.Save();
            }

            // Reload table
            RenderTable();
            SetControlStates(false);
            DrawRectanglesOnImage();
        }

        private bool ValidateNumericInputs()
        {
            if (npX.Value == 0 || npY.Value == 0 || npWidth.Value == 0 || npHeight.Value == 0)
            {
                MessageBox.Show("Please enter all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private SQliteDataAccess.Rectangles CreateRectangle()
        {
            return new SQliteDataAccess.Rectangles()
            {
                model_id = this.model_id,
                x = (int)npX.Value,
                y = (int)npY.Value,
                width = (int)npWidth.Value,
                height = (int)npHeight.Value
            };
        }

        private void SetControlStates(bool isEnabled)
        {
            npX.Enabled = isEnabled;
            npY.Enabled = isEnabled;
            // npWidth.Enabled = isEnabled;
            // npHeight.Enabled = isEnabled;
            btnSave.Enabled = isEnabled;
        }

        private void np_ValueChanged(object sender, EventArgs e)
        {
            DrawRectanglesOnImage();
        }
        private void DrawRectanglesOnImage()
        {
            using (Bitmap bitmap = new Bitmap(scrollablePictureBox.Image.Width, scrollablePictureBox.Image.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(this.image, 0, 0);

                if (rectangles.Count == 1)
                {
                    DrawRectangleToImage(bitmap, (int)npX.Value, (int)npY.Value, (int)npWidth.Value, (int)npHeight.Value, Properties.Settings.Default.current_point_color);
                }
                else
                {
                    foreach (var rec in rectangles)
                    {
                        Color color = rec.id == id ? Properties.Settings.Default.current_point_color : Properties.Settings.Default.point_color;
                        if (rec.id == id)
                        {
                            DrawRectangleToImage(bitmap, (int)npX.Value, (int)npY.Value, (int)npWidth.Value, (int)npHeight.Value, color);
                        }
                        else
                        {
                            DrawRectangleToImage(bitmap, rec.x, rec.y, rec.width, rec.height, color);
                        }
                    }
                }


                if (id == 0)
                {
                    DrawRectangleToImage(bitmap, (int)npX.Value, (int)npY.Value, (int)npWidth.Value, (int)npHeight.Value, Properties.Settings.Default.new_point_color);
                }

                // Get RGB value of pixel center of rectangle
                int x = (int)npX.Value + (int)npWidth.Value / 2;
                int y = (int)npY.Value + (int)npHeight.Value / 2;

                Color pixelColor = bitmap.GetPixel(x, y);
                pgColor.BackColor = pixelColor;

                lbR.BackColor = Color.FromArgb(pixelColor.R, 0, 0);
                lbG.BackColor = Color.FromArgb(0, pixelColor.G, 0);
                lbB.BackColor = Color.FromArgb(0, 0, pixelColor.B);
                // Invert text color for better visibility
                lbR.ForeColor = Color.FromArgb(255 - pixelColor.R, 255, 255);
                lbG.ForeColor = Color.FromArgb(255, 255 - pixelColor.G, 255);
                lbB.ForeColor = Color.FromArgb(255, 255, 255 - pixelColor.B);

                txtR.Text = pixelColor.R.ToString();
                txtG.Text = pixelColor.G.ToString();
                txtB.Text = pixelColor.B.ToString();

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
            toolStripStatusLabel_Id.Text = "ID : " + id;

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

        private void cbPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            switch (box.Name)
            {
                case "cbPoint":
                    Properties.Settings.Default.point_color = cbPoint.SelectedValue;
                    break;
                case "cbCurrentPoint":
                    Properties.Settings.Default.current_point_color = cbCurrentPoint.SelectedValue;
                    break;
                case "cbNewPoint":
                    Properties.Settings.Default.new_point_color = cbNewPoint.SelectedValue;
                    break;
            }
            Properties.Settings.Default.Save();
            DrawRectanglesOnImage();
        }

        private void scrollablePictureBox_Click(object sender, EventArgs e)
        {
            if(!npX.Enabled && !npY.Enabled)return;

            // Get X, Y of mouse click
            MouseEventArgs me = (MouseEventArgs)e;
            npX.Value = me.X-((int)npWidth.Value/2);
            npY.Value = me.Y - ((int)npHeight.Value / 2);
        }

        private Task task;
        string filePath = "";
        string sheetName = "";
        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            return;
            if (task != null && task.Status == TaskStatus.Running)
            {
                MessageBox.Show("Please wait for the current task to finish");
                return;
            }

            // Check picture box image
            if (scrollablePictureBox.Image == null)
            {
                MessageBox.Show("Please load an image first");
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Save an Excel File";
                sfd.FileName = "ColorData";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    sheetName = Path.GetFileNameWithoutExtension(filePath);
                }
                else
                {
                    return;
                }
            }
            task = Task.Run(() =>
            {
                int row = 1;

                int totalRows = scrollablePictureBox.Image.Height * scrollablePictureBox.Image.Width;
                int totalMaxRows = 500000;
                int totalSheets = (int)Math.Ceiling((double)totalRows / totalMaxRows);
                int totalRowsPerSheet = (int)Math.Ceiling((double)totalRows / totalSheets);
                int SheetNumb = 1;
                int Count = 0;
                
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    for (int height = 0; height < scrollablePictureBox.Image.Height; height++)
                    {
                        for (int width = 0; width < scrollablePictureBox.Image.Width; width++)
                        {
                            Color color = ((Bitmap)scrollablePictureBox.Image).GetPixel(width, height);

                            string hex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

                            if (Count == 0)
                            {
                                row = 1;
                                SaveColorToExcel(package, color, hex, width, height, row, sheetName + "_" + SheetNumb);
                            }
                            else
                            {
                                row++;
                                SaveColorToExcel(package, color, hex, width, height, row, sheetName + "_" + SheetNumb);
                            }
                            Count++;
                            if (Count == totalRowsPerSheet)
                            {
                                Count = 0;
                                SheetNumb++;
                            }
      
                            if (InvokeRequired)
                            {
                                Invoke(new Action(() =>
                                {
                                    toolStripProgressBar1.Value = (int)Math.Ceiling((double)(height * scrollablePictureBox.Image.Width + width) / totalRows * 100);
                                }));
                            }
                        }
                        Console.WriteLine("Numb" + Count);
                    }
                    // Save the changes
                    package.Save();
                }
            });
            */
        }

        private void SaveColorToExcel(ExcelPackage package, System.Drawing.Color color, string hex, int x, int y, int row , string sheetName)
        {
            // Check if the worksheet already exists
            var worksheet = package.Workbook.Worksheets[sheetName];
            int colX = 1;
            int colY = colX + 1;
            int colR = colY + 1;
            int colG = colR + 1;
            int colB = colG + 1;
            int colHex = colB + 1;

            if (worksheet == null)
            {
                worksheet = package.Workbook.Worksheets.Add(sheetName);

                // Define headers
                worksheet.Cells[1, colX].Value = "X";
                worksheet.Cells[1, colY].Value = "Y";
                worksheet.Cells[1, colR].Value = "R";
                worksheet.Cells[1, colG].Value = "G";
                worksheet.Cells[1, colB].Value = "B";
                worksheet.Cells[1, colHex].Value = "Hex";
            }

            // Apply the color to the cell
            worksheet.Cells[row, colX].Value = x.ToString();
            worksheet.Cells[row, colY].Value = y.ToString();
            worksheet.Cells[row, colR].Value = color.R.ToString();
            worksheet.Cells[row, colG].Value = color.G.ToString();
            worksheet.Cells[row, colB].Value = color.B.ToString();
            worksheet.Cells[row, colHex].Value = hex;
        }

    }
}

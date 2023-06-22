using DirectShowLib;
using GL_M2.Forms;
using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace GL_M2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitializeCapture();
        }
        private Bitmap bitmap = null;
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };
        private int driveindex = 0;
        private void Main_Load(object sender, EventArgs e)
        {

            btRefresh.PerformClick();
            RenderModels();
        }

        private int selectedRow = -1;
        private void RenderModels()
        {
            var models = SQliteDataAccess.Models.GetAll();
            if (models == null) return;
            // Clear all rows
            cbModels.Items.Clear();
            foreach ( var model in models )
            {
                cbModels.Items.Add(model.name);
            }

            SelectModelsRow(selectedRow);
        }

        private void SelectModelsRow(int rowIndex, int columnIndex = 1)
        { 
            if(rowIndex != -1 && rowIndex < cbModels.Items.Count)
            {
                cbModels.SelectedIndex = rowIndex;
                selectedRow = rowIndex;
            }else if(cbModels.Items.Count > 0)
            {
                cbModels.SelectedIndex = 0;
                selectedRow = 0;
            }
        }

            private void btRefresh_Click(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            RefreshComboBox(comboBoxCamera, videoDevices);
            RefreshComboBox(comboBoxBaud, this.baudList, baudList.Length - 1);
            RefreshComboBox(comboBoxCOMPort, SerialPort.GetPortNames(), SerialPort.GetPortNames().Length -1);
        }

        private void RefreshComboBox(ComboBox comboBox, object items, int defaultIndex = 0)
        {
            comboBox.Items.Clear();
            if (items is string[]) comboBox.Items.AddRange((string[])items);
            else if (items is List<DsDevice>) comboBox.Items.AddRange(((List<DsDevice>)items).Select(d => d.Name).ToArray());
            else comboBox.Items.AddRange((string[])items);

            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = defaultIndex;
        }

        private bool isStarted = false;
        private async void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input combobox
                if (comboBoxCamera.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                isStarted = !isStarted;
                if (isStarted)
                {
                    pgCam.Image?.Dispose();
                    btConnect.Text = "Connecting..";

                    pgCam.Image = Properties.Resources.Spinner_0_4s_800px;
                    await capture.StartAsync(comboBoxCamera.SelectedIndex);

                    btConnect.Text = "Disconnect";
                    //timerTest.Start();
                }
                else
                {
                    isStarted = false;
                    await capture.StopAsync();
                    btConnect.Text = "Connect";
                    //timerTest.Stop();
                }
            }catch(Exception ex)
            {
                await capture.StopAsync();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Forms.Models models;
        private void modelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            models?.Dispose();
            models = new Forms.Models(this);
            models.Show();
        }


        private SQliteDataAccess.Models model;
        private int model_id = -1;

        private List<SQliteDataAccess.Rectangles> rectangles;

        private void cbModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRow = cbModels.SelectedIndex;
            txtModels.Text = cbModels.SelectedItem.ToString();
            // Create new KeyEventArgs instance
            KeyEventArgs kea = new KeyEventArgs(Keys.Enter);
            txtModels_KeyDown(sender, kea);
        }

        private void txtModels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.KeyCode == Keys.Enter)
            {
                model = SQliteDataAccess.Models.GetByName(txtModels.Text.Trim());
                if(model == null)
                {
                    model_id = -1;
                    lbTitle.Text = "------------------------";
                    toolStripStatusLabel_Id.Text = $"Model ID: {model_id}";
                    MessageBox.Show("Model not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                model_id = model.id;
                rectangles = SQliteDataAccess.Rectangles.GetByModelId(model_id);
                toolStripStatusLabel_Id.Text = $"Model ID: {model_id}";
                lbTitle.Text = $"Model: {model.name}";
                rectangles = SQliteDataAccess.Rectangles.GetByModelId(model_id);
            }
        }

        private Options options;
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options?.Dispose();
            options = new Options();
            options.Show();
        }
    }
}

using DirectShowLib;
using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
       
        private List<Rectangle> rectangles;

        public Main()
        {
            InitializeComponent();
            InitializeCapture();         

            rectangles = new List<Rectangle>();
        }

      

        private Bitmap bitmap = null;
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };
        private int driveindex = 0;

        private void Main_Load(object sender, EventArgs e)
        {

            btRefresh.PerformClick();
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
                }
                else
                {
                    isStarted = false;
                    await capture.StopAsync();
                    btConnect.Text = "Connect";
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
            models = new Forms.Models();
            models.Show();
        }
    }
}

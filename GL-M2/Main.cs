using DirectShowLib;
using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private readonly TCapture capture;
        public Main()
        {
            InitializeComponent();
            capture = new TCapture();

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
            RefreshComboBox(comboBoxBaud, this.baudList, comboBoxBaud.Items.Count - 1);
            RefreshComboBox(comboBoxCOMPort, SerialPort.GetPortNames());
        }

        private void RefreshComboBox(ComboBox comboBox, object items, int defaultIndex = 0)
        {
            comboBox.Items.Clear();
            if (items is string[]) comboBox.Items.AddRange((string[])items);
            else if (items is List<DsDevice>) comboBox.Items.AddRange(((List<DsDevice>)items).Select(d => d.Name).ToArray());
            else comboBox.Items.AddRange((string[])items);

            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = defaultIndex;


        }

        private void btConnect_Click(object sender, EventArgs e)
        {

        }
    }
}

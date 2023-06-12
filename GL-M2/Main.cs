using DirectShowLib;
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
        public Main()
        {
            InitializeComponent();
        }
        private Bitmap bitmap = null;
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };
        private int driveindex = 0;

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            comboBoxCamera.Items.Clear();
            foreach (DsDevice device in videoDevices)
            {
                comboBoxCamera.Items.Add(device.Name);
            }


            if (comboBoxCamera.Items.Count > 0)
            {
                comboBoxCamera.SelectedIndex = 0;
            }

            comboBoxBaud.Items.Clear();
            comboBoxBaud.Items.AddRange(this.baudList);
            if (comboBoxBaud.Items.Count > 0)
                comboBoxBaud.SelectedIndex = comboBoxBaud.Items.Count - 1;

            comboBoxCOMPort.Items.Clear();
            comboBoxCOMPort.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxCOMPort.Items.Count > 0)
                comboBoxCOMPort.SelectedIndex = 0;
        }
    }
}

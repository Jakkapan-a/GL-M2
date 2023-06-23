using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace GL_M2
{
    partial class Main
    {
        private SerialPort serialPort;
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort();
            serialPort.DataReceived += Serial_DataReceived;
            serialPort.ErrorReceived += Serial_ErrorReceived;
        }

        public void SerialConnect()
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }

            this.serialPort.PortName = comboBoxCOMPort.SelectedItem.ToString();
            this.serialPort.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
            this.serialPort.Open();
            this.SerialCommand("conn");
            this.toolStripStatusConnect_SerialPort.Text = "Serial Connected";
            this.toolStripStatusConnect_SerialPort.ForeColor = Color.Green;

        }
        public void SerialClose()
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }
        }
        public void SerialCommand(SERIAL_STATUS serial) 
        {
            switch(serial)
            {
                case SERIAL_STATUS.NONE:
                    SerialCommand("4");
                    break;
                case SERIAL_STATUS.OK:
                    SerialCommand("1");
                    break;
                case SERIAL_STATUS.NG:
                    SerialCommand("2");
                    break;
            }
        }
        public void SerialCommand(string command)
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Write(">" + command + "<#");
                toolStripStatusSentData.Text = $"{DateTime.Now.ToString("HH:MM:ss")} Send : {command}";
            }
        }

        private string readDataSerial = string.Empty;
        private string dataSerialReceived = string.Empty;

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            readDataSerial = serialPort.ReadLine();
            DataReceived();
        }


        private void DataReceived()
        {
            if (InvokeRequired)
            {
                Invoke(new System.Action(() => DataReceived()));
                return;
            }

            this.dataSerialReceived += readDataSerial;
            if (dataSerialReceived.Contains(">") && dataSerialReceived.Contains("<"))
            {
                string data = this.dataSerialReceived.Replace("\r", string.Empty).Replace("\n", string.Empty);
                data = data.Substring(data.IndexOf(">") + 1, data.IndexOf("<") - data.IndexOf(">") - 1);
                this.dataSerialReceived = string.Empty;
                data = data.Replace(">", "").Replace("<", "");
                //toolStripStatusSentData.Text = "DATA :" + data;
            }
            else if (!dataSerialReceived.Contains(">"))
            {
                this.dataSerialReceived = string.Empty;
            }
        }

        private void Serial_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }
    }

    public enum SERIAL_STATUS
    {
        NONE,
        OK,
        NG,
    }
}

using GL_M2.Controls;
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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
           
        }

        private void Options_Load(object sender, EventArgs e)
        {
            cbColorOK.SelectedValue = Properties.Settings.Default.color_ok;
            cbColorNG.SelectedValue = Properties.Settings.Default.color_ng;
            npCircle_radius.Value = Properties.Settings.Default.circle_radius;
            npTriangle_length.Value = Properties.Settings.Default.triangle_length;
            npToggle_time.Value = Properties.Settings.Default.toggle_time;
            npTime_process.Value = Properties.Settings.Default.time_process;
            npPercent_check.Value = Properties.Settings.Default.percent_check;
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            switch (colorPicker.Name)
            {
                case "cbColorOK":
                    Properties.Settings.Default.color_ok = cbColorOK.SelectedValue;
                    break;
                case "cbColorNG":
                    Properties.Settings.Default.color_ng = cbColorNG.SelectedValue;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void nP_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            switch(numeric.Name)
            {
                case "npCircle_radius":
                    Properties.Settings.Default.circle_radius = (int)numeric.Value;
                    break;
                case "npTriangle_length":
                    Properties.Settings.Default.triangle_length = (int)numeric.Value;
                    break;
                case "npToggle_time":
                    Properties.Settings.Default.toggle_time = (int)numeric.Value;
                    break;
                case "npTime_process":
                    Properties.Settings.Default.time_process = (int)numeric.Value;
                    break;
                case "npPercent_check":
                    Properties.Settings.Default.percent_check = (int)numeric.Value;
                    break;
                case "npMedianBlur":
                    Properties.Settings.Default.medianBlur = (int)numeric.Value;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void cbIsMedianBlur_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Properties.Settings.Default.isMedianBlur = cb.Checked;
            Properties.Settings.Default.Save();
        }
       
    }
}

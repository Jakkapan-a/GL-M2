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
    public partial class Image_01 : Form
    {
        public Image_01()
        {
            InitializeComponent();
        }

        private void Image_01_Load(object sender, EventArgs e)
        {

        }

        private void ser45ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public List<Rectangle> rectangles = new List<Rectangle>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (scrollablePictureBox1.GetRect() == Rectangle.Empty) return;
            rectangles.Add(scrollablePictureBox1.GetRect());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rectangles.Clear();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {

        }

    }
}

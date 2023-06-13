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
        public Rectangles(int model_id)
        {
            InitializeComponent();
            this.model_id = model_id;
        }
        private Image image;
        private void Rectangles_Load(object sender, EventArgs e)
        {
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
            }
        }
    }
}

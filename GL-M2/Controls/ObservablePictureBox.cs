using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GL_M2.Controls
{
    public class ObservablePictureBox : PictureBox
    {
        public event EventHandler ImageChanged;
        public new Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
                if (this.ImageChanged != null)
                    this.ImageChanged(this, new EventArgs());
            }
        }
    }
}

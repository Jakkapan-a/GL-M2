using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2
{
    partial class Main
    {
        private void Capture_OnVideoStop()
        {
            pgCam.Image?.Dispose();
        }

        private void Capture_OnVideoStarted()
        {

        }

        private void Capture_OnFrameHeader(Bitmap bitmap)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(()=> Capture_OnFrameHeader(bitmap)));
                return;
            }

            pgCam.Image?.Dispose();
            pgCam.Image = (Image)bitmap.Clone();


        }
    }
}

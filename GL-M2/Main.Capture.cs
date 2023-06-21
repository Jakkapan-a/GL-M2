using GL_M2.Utilities;
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
        private TCapture capture;

        private void InitializeCapture()
        {
            capture = new TCapture();
            capture.OnFrameHeader += Capture_OnFrameHeader;
            capture.OnVideoStarted += Capture_OnVideoStarted;
            capture.OnVideoStop += Capture_OnVideoStop;
        }
        private void Capture_OnVideoStop()
        {
            pgCam.Image?.Dispose();
        }

        private void Capture_OnVideoStarted()
        {

        }
        public Image image;
        private void Capture_OnFrameHeader(Bitmap bitmap)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(()=> Capture_OnFrameHeader(bitmap)));
                return;
            }
            image?.Dispose();
            image = (Image)bitmap.Clone();
            using(Bitmap bmp = new Bitmap(image.Width, image.Height))
            {
                using(Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                    if(rectangles != null && rectangles.Count > 0){
                        foreach (var rectangle in rectangles)
                        {
                            g.DrawRectangle(new Pen(Properties.Settings.Default.point_color, 2), rectangle.x, rectangle.y, rectangle.width, rectangle.height);

                            /*
                                Code more....
                            */

                        }
                    }
                }

                pgCam.Image?.Dispose();
                pgCam.Image = (Image)bmp.Clone();
            }
        }
    }
}

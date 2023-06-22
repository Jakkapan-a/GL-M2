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
                Invoke(new Action(() => Capture_OnFrameHeader(bitmap)));
                return;
            }
            image?.Dispose();
            image = (Image)bitmap.Clone();
            using (Bitmap bmp = new Bitmap(image.Width, image.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                    if (rectangles != null && rectangles.Count > 0)
                    {
                        foreach (var rectangle in rectangles)
                        {
                            g.DrawRectangle(new Pen(Properties.Settings.Default.point_color, 2), rectangle.x, rectangle.y, rectangle.width, rectangle.height);

                            /*
                                Code more....
                            */
                            if (results.Any(x => x.id == rectangle.id))
                            {
                                STATUS result = results.Where(x => x.id == rectangle.id).Select(x => x.result).FirstOrDefault();
                                if (result == STATUS.OK)
                                {
                                    Console.WriteLine("OK");
                                }
                                else if (result == STATUS.NG)
                                {
                                    Console.WriteLine("NG");
                                }
                                else if (result == STATUS.PROCESSING) {                                   
                                    Console.WriteLine("PROCESSING");
                                }
                                else if (result == STATUS.NONE) {                                   
                                    Console.WriteLine("NONE");
                                }
                            }
                        }
                    }

                }
                //int sideLength = 100; // Adjust to desired side length
                //float halfLength = sideLength / 2.0f;

                //// Calculate the center of the bitmap
                //float centerX = bmp.Width / 2;
                //float centerY = bmp.Height / 2;

                //// Calculate the points of the triangle
                //PointF point1 = new PointF(centerX, centerY - halfLength * (float)Math.Sqrt(3) / 2);
                //PointF point2 = new PointF(centerX - halfLength, centerY + halfLength * (float)Math.Sqrt(3) /2);
                //PointF point3 = new PointF(centerX + halfLength, centerY + halfLength * (float)Math.Sqrt(3) / 2);
                //PointF[] points = { point1, point2, point3 };

                //using (Graphics g = Graphics.FromImage(bmp))
                //{
                //    g.DrawPolygon(Pens.Red, points);
                //}

                pgCam.Image?.Dispose();
                pgCam.Image = (Image)bmp.Clone();
            }
        }
    }
}

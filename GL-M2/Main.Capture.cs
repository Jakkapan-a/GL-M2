using GL_M2.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2
{
    partial class Main
    {
        private TCapture capture;
        private Stopwatch stopwatch;
        private Stopwatch stopwatchTest;

        private void InitializeCapture()
        {
            capture = new TCapture();
            capture.OnFrameHeader += Capture_OnFrameHeader;
            capture.OnVideoStarted += Capture_OnVideoStarted;
            capture.OnVideoStop += Capture_OnVideoStop;
            stopwatch = new Stopwatch();
            stopwatchTest = new Stopwatch();
        }
        private void Capture_OnVideoStop()
        {
            pgCam.Image?.Dispose();
        }

        private void Capture_OnVideoStarted()
        {
            stopwatch.Restart();
            stopwatchTest.Restart();
        }
        public Image image;
        private bool isToggleNG = false;

        private void Capture_OnFrameHeader(Bitmap bitmap)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Capture_OnFrameHeader(bitmap)));
                return;
            }

            UpdateImage(bitmap);
            DrawRectanglesAndManageStatus(bitmap);
            CheckTestElapsed();

        }

        private void UpdateImage(Bitmap bitmap)
        {
            image?.Dispose();
            image = (Image)bitmap.Clone();
        }

        private void DrawRectanglesAndManageStatus(Bitmap bitmap)
        {
            using (Bitmap bmp = new Bitmap(image.Width, image.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(bitmap, 0, 0, image.Width, image.Height);
                    if (rectangles != null && rectangles.Count > 0)
                    {
                        foreach (var rectangle in rectangles)
                        {
                            DrawRectangleAndCheckStatus(g, rectangle);
                        }
                    }
                }
                // Update picture
                pgCam.Image?.Dispose();
                pgCam.Image = (Image)bmp.Clone();
            }
        }


        private void DrawRectangleAndCheckStatus(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            /*
                Code more....
            */
            Color color = Color.Red;
            if (results.Any(x => x.id == rectangle.id))
            {
                STATUS result = results.Where(x => x.id == rectangle.id).Select(x => x.result).FirstOrDefault();
                if (result == STATUS.OK)
                {
                    DrawGreenCircle(g, rectangle);
                    color = Color.Green;
                }
                else if (result == STATUS.NG)
                {
                    DrawRedTriangle(g, rectangle);
                }
                else if (result == STATUS.PROCESSING)
                {
                    Console.WriteLine("PROCESSING");
                }
                else if (result == STATUS.NONE)
                {
                    Console.WriteLine("NONE");
                }
            }
            g.DrawRectangle(new Pen(color, 2), rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }
        private void DrawGreenCircle(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            //Console.WriteLine("OK");
            Pen pen = new Pen(Color.Green, 2);  // Choose your color and line width
            int radius = 50;  // Change this to change the radius of the circle
            g.DrawEllipse(pen, (rectangle.x + rectangle.width / 2) - radius, (rectangle.y + rectangle.height / 2) - radius, radius * 2, radius * 2);
        }
        private void DrawRedTriangle(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            int time = 500;
            if (stopwatch.ElapsedMilliseconds > time)
            {
                //Console.WriteLine("NG");
                DrawTriangle(g, Pens.Red, rectangle, time);
                if (stopwatch.ElapsedMilliseconds > time * 2)
                {
                    stopwatch.Restart();
                }
            }
        }
        private void DrawTriangle(Graphics g, Pen pen, SQliteDataAccess.Rectangles rectangle, int time)
        {
            int sideLength = 100; // Adjust to desired side length
            float halfLength = sideLength / 2.0f;

            // Calculate the center of the bitmap
            float centerX = rectangle.x + rectangle.width / 2;
            float centerY = rectangle.y + rectangle.height / 2;

            // Calculate the points of the triangle
            PointF point1 = new PointF(centerX, centerY - halfLength * (float)Math.Sqrt(3) / 2);
            PointF point2 = new PointF(centerX - halfLength, centerY + halfLength * (float)Math.Sqrt(3) / 2);
            PointF point3 = new PointF(centerX + halfLength, centerY + halfLength * (float)Math.Sqrt(3) / 2);
            PointF[] points = { point1, point2, point3 };
            g.DrawPolygon(pen, points);
        }

        private Image image_temp;
        private void CheckTestElapsed()
        {
            if (stopwatchTest.ElapsedMilliseconds > 500)
            {
                image_temp?.Dispose();
                image_temp = (Image)image.Clone();

                StartTest();
                stopwatchTest.Restart();
            }
        }
    }
}

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
        private bool isReset = true;

        private void DrawRectanglesAndManageStatus(Bitmap bitmap)
        {
            using (Bitmap bmp = new Bitmap(image.Width, image.Height))
            {
                DrawImageAndRectangles(bitmap, bmp);
                // Update picture
                UpdateImage(bmp);

                ManageStatus(bmp);
                if (reset == STATUS.STOPPED)
                {
                    Color color = bmp.GetPixel(20, 20);
                    int min = 20;
                    if(!isReset && color != null && color.R > min && color.G > min && color.B > min)
                    {
                        reset = STATUS.STARTING;
                        isReset = true;
                    }else
                    if (isReset)
                    {
                        if (color != null && color.R < min && color.G < min && color.B < min)
                        {
                            isReset = false;
                        }
                    }
                }


            }
        }

        private void DrawImageAndRectangles(Bitmap sourceBitmap, Bitmap targetBitmap)
        {
            using (Graphics g = Graphics.FromImage(targetBitmap))
            {
                g.DrawImage(sourceBitmap, 0, 0, image.Width, image.Height);
                DrawAllRectangles(g);
            }
        }
        private void DrawAllRectangles(Graphics g)
        {
            if (rectangles != null && rectangles.Count > 0)
            {
                foreach (var rectangle in rectangles)
                {
                    DrawRectangleAndCheckStatus(g, rectangle);
                }
            }
        }
        private void ManageStatus(Bitmap bmp)
        {
            if (reset == STATUS.STOPPED)
            {
                Color color = bmp.GetPixel(20, 20);
                ManageResetStatus(color);
            }
        }
        private void ManageResetStatus(Color color)
        {
            int min = 20;
            if (!isReset && IsColorAboveMin(color, min))
            {
                StartReset();
            }
            else if (isReset && !IsColorAboveMin(color, min))
            {
                StopReset();
            }
        }
        private bool IsColorAboveMin(Color color, int min) => color.R > min && color.G > min && color.B > min;
        

        private void StartReset()
        {
            reset = STATUS.STARTING;
            isReset = true;
        }

        private void StopReset()
        {
            isReset = false;
        }
        private void DrawRectangleAndCheckStatus(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            Color color = Properties.Settings.Default.color_ng;
            var res = results.FirstOrDefault(x => x.id == rectangle.id);
            if (res != null)
            {
                STATUS result = results.Where(x => x.id == rectangle.id).Select(x => x.result).FirstOrDefault();
                if (result == STATUS.OK)
                {
                    DrawGreenCircle(g, rectangle);
                    color = Properties.Settings.Default.color_ok;
                }
                else if (result == STATUS.NG)
                {
                    DrawRedTriangle(g, rectangle);
                }               
            }
            g.DrawRectangle(new Pen(color, 2), rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }
        private void DrawGreenCircle(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            //Console.WriteLine("OK");
            Pen pen = new Pen(Properties.Settings.Default.color_ok, 2);  // Choose your color and line width
            int radius = Properties.Settings.Default.circle_radius;  // Change this to change the radius of the circle
            g.DrawEllipse(pen, (rectangle.x + rectangle.width / 2) - radius, (rectangle.y + rectangle.height / 2) - radius, radius * 2, radius * 2);
        }
        private void DrawRedTriangle(Graphics g, SQliteDataAccess.Rectangles rectangle)
        {
            int time = Properties.Settings.Default.toggle_time;
            if (stopwatch.ElapsedMilliseconds > time)
            {
                //Console.WriteLine("NG");
                Pen pen = new Pen(Properties.Settings.Default.color_ng, 2);
                DrawTriangle(g, pen, rectangle, time);
                if (stopwatch.ElapsedMilliseconds > time * 2)
                {
                    stopwatch.Restart();
                }
            }
        }
        private void DrawTriangle(Graphics g, Pen pen, SQliteDataAccess.Rectangles rectangle, int time)
        {
            int sideLength = Properties.Settings.Default.triangle_length; // Adjust to desired side length
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
            if (stopwatchTest.ElapsedMilliseconds > Properties.Settings.Default.time_process)
            {
                image_temp?.Dispose();
                image_temp = (Image)image.Clone();

                StartTest();
                stopwatchTest.Restart();
            }
        }
    }
}

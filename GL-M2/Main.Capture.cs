﻿using GL_M2.Utilities;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
            if (InvokeRequired)
            {
                Invoke(new Action(() => Capture_OnVideoStop()));
                return;
            }
            pgCam.Image?.Dispose();
            pgCam.Image = null;
            // Black color
            lbStatus.Text = "Wait..";
            lbStatus.BackColor = Color.Yellow;
        }

        private void Capture_OnVideoStarted()
        {
            stopwatch.Restart();
            stopwatchTest.Restart();
        }
      
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
        public Image image;
        // public Dictionary<int, Image> images;
        public Queue<Image> imagesQueue;
        private int count_image = 0;
        public int count_images_limit = 10;
        private void UpdateImage(Bitmap bitmap)
        {
            image?.Dispose();
            //image = (Image)bitmap.Clone();
            image = new Bitmap(bitmap);


            //if(images == null)
            //{
            //    images = new Dictionary<int, Image>();
            //}

            if (imagesQueue == null)
            {
                imagesQueue = new Queue<Image>();
            }

            if (imagesQueue.Count < count_images_limit)
            {
                imagesQueue.Enqueue((Image)image.Clone());
            }
            else
            {
                imagesQueue.Dequeue()?.Dispose();
                imagesQueue.Enqueue((Image)image.Clone());
            }

            //if (count_image < count_images_limit)
            //{
            //    images.TryGetValue(count_image,out var oldImage);
            //    oldImage?.Dispose();
            //    images[count_image] = (Image)image.Clone();
            //    count_image++;
            //}
            //else
            //{
            //    count_image = 0;
            //}
        }
        private bool isReset = true;

        private void DrawRectanglesAndManageStatus(Bitmap bitmap)
        {
            using (Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height))
            {
                DrawImageAndRectangles(bitmap, bmp);
                // Update picture
                pgCam.Image?.Dispose();
                pgCam.Image = (Image)bmp.Clone();
                ManageStatus(bmp);
            }
        }

        private void DrawImageAndRectangles(Bitmap sourceBitmap, Bitmap targetBitmap)
        {
            try
            {
                using (Graphics g = Graphics.FromImage(targetBitmap))
                {
                    g.DrawImage(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height);
                    DrawAllRectangles(g);
                }
            }catch(Exception ex)
            {
                Console.WriteLine("DrawImageAndRectangles :" + ex.Message);
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
                int x = (int)bmp.Width/4;
                int y = (int)bmp.Height/4;

                Color color = bmp.GetPixel(x, y);
                Color color2 = bmp.GetPixel(x+10, y);
                //if(rectangles.Count > 1)
                //{
                //    x = rectangles[0].x;
                //    y = rectangles[0].y;
                //    color = bmp.GetPixel(x, y);

                //    x = rectangles[1].x;
                //    y = rectangles[1].y;
                //    color2 = bmp.GetPixel(x, y);
                //}
                ManageResetStatus(color, color2);
            }
        }
        private void ManageResetStatus(Color color, Color color2)
        {
            int min = 10;
            if (!isReset && IsColorAboveMin(color, min) && IsColorAboveMin(color2, min))
            {
                StartReset();
            }
            else if (isReset && !IsColorAboveMin(color, min) && !IsColorAboveMin(color2, min))
            {
                StopReset();
                UpdateDisplay();
            }
        }
        private bool IsColorAboveMin(Color color, int min) => color.R > min && color.G > min && color.B > min;
        

        private void StartReset()
        {
            reset = STATUS.STARTING;
            isReset = true;
            // RESET STATUS OF SENT SERIAL DATA
            test_result = SERIAL_STATUS.NONE;
        }

        private void StopReset()
        {
            isReset = false;
            foreach(var result in results)
            {
                result.result = STATUS.NONE;
            }
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
            try
            {
                g.DrawRectangle(new Pen(color, 2), rectangle.x, rectangle.y, rectangle.width, rectangle.height);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
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
        private SERIAL_STATUS test_result = SERIAL_STATUS.NONE;
        private void CheckTestElapsed()
        {
            if (stopwatchTest.ElapsedMilliseconds > Properties.Settings.Default.time_process)
            {
                image_temp?.Dispose();
                image_temp = (Image)image.Clone();
                StartTest();

                stopwatchTest.Restart();
                SerialCommand(test_result);
            }
        }
    }
}

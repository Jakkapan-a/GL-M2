﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GL_M2.Utilities;

namespace GL_M2
{
    partial class Main
    {
        private Task taskProcessTest;
        private void timerTest_Tick(object sender, EventArgs e)
        {

        }
        private void StartTest()
        {
            if (taskProcessTest != null && taskProcessTest.Status != TaskStatus.RanToCompletion)
            {
                return;
            }

            taskProcessTest = Task.Run(() => ProcessTest()).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    // Log error
                    Console.WriteLine(task.Exception.InnerException.Message);
                }
            });

        }

        private List<Result> results = new List<Result>();

        private void ProcessTest()
        {

            if (reset == STATUS.STOPPED) return;
            // Check if there is any rectangle
            if (rectangles == null || rectangles.Count == 0) return;
            //results.Clear();
            if(results.Count != rectangles.Count)
            {
                results.Clear();
            }
            using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, model.image), FileMode.Open, FileAccess.Read))
            {
                using (Image img = Image.FromStream(fs))
                {
                    ProcessBitmaps(img);
                }
            }
        }

        private void ProcessBitmaps(Image img)
        {
            using (Bitmap bmp_s = new Bitmap(image_temp.Width, image_temp.Height))
            {
                using (Bitmap bmp_m = new Bitmap(img.Width, img.Height))
                {
                    DrawToBitmaps(bmp_m, img, bmp_s);
                    ProcessRectangles(bmp_m, bmp_s);
                }
            }           
        }

        private void DrawToBitmaps(Bitmap bmp_m, Image img, Bitmap bmp_s)
        {
            // Draw image master to bitmap
            using (Graphics g = Graphics.FromImage(bmp_m))
            {
                g.DrawImage(img, 0, 0);
            }
            // Draw image slave to bitmap
            using (Graphics g = Graphics.FromImage(bmp_s))
            {
                g.DrawImage(this.image_temp, 0, 0);
            }
        }

        private void ProcessRectangles(Bitmap bmp_m, Bitmap bmp_s)
        {
            foreach (var r in rectangles)
            {
                ProcessRectangle(r, bmp_m, bmp_s);
            }
            if (InvokeRequired) 
            {
                Invoke(new Action(() => UpdateDisplay()));
            }
        }

        private STATUS reset = STATUS.NEW;
        private void UpdateDisplay()
        {
            int _total = results.Count; // results.Count(x => x.result == STATUS.NONE);
            if(_total == results.Count(x => x.result == STATUS.NONE))
            {
                // Black color
                lbStatus.Text = "Wait..";
                lbStatus.BackColor = Color.Yellow;
                //
            }
            else if(_total == results.Count(x=> x.result == STATUS.OK)){
                lbStatus.Text = "PASS";
                lbStatus.BackColor = Color.Green;
                // STOP
                reset = STATUS.STOPPED;
            }
            else
            {
                lbStatus.Text = "NG";
                lbStatus.BackColor = Color.Red;
            }


        }
        private void ProcessRectangle(SQliteDataAccess.Rectangles r, Bitmap bmp_m, Bitmap bmp_s)
        {
            // Get RGB value of pixel center of rectangle
            int x = (int)r.x + (int)r.width / 2;
            int y = (int)r.y + (int)r.height / 2;
            Color color = bmp_m.GetPixel(x, y);

            int total = CalcPct(255, Properties.Settings.Default.percent_check);
            int r_min = color.R - total < 0 ? 0 : color.R - total;
            int r_max = color.R + total > 255 ? 255 : color.R + total;
            int g_min = color.G - total < 0 ? 0 : color.G - total;
            int g_max = color.G + total > 255 ? 255 : color.G + total;
            int b_min = color.B - total < 0 ? 0 : color.B - total;
            int b_max = color.B + total > 255 ? 255 : color.B + total;

            // Get RGB value of pixel center of rectangle
            int x_slave = (int)r.x + (int)r.width / 2;
            int y_slave = (int)r.y + (int)r.height / 2;
            Color color_slave = bmp_s.GetPixel(x_slave, y_slave);

            UpdateResultList(r, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave);
        }

        private void UpdateResultList(SQliteDataAccess.Rectangles r, Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, Color color_slave)
        {
            int black = 20;
            var res = results.FirstOrDefault(re => re.id == r.id);
            if (res != null)
            {
                // Update result
                UpdateResult(res, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave, black);
            }
            else
            {
                // Add result to list
                AddNewResult(r, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave, black);
            }
        }

        private void UpdateResult(Result res, Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, Color color_slave, int black)
        {
            res.R = color.R;
            res.G = color.G;
            res.B = color.B;
            res.MinR = r_min;
            res.MinG = g_min;
            res.MinB = b_min;
            res.MaxR = r_max;
            res.MaxG = g_max;
            res.MaxB = b_max;
            res.Slave_R = color_slave.R;
            res.Slave_G = color_slave.G;
            res.Slave_B = color_slave.B;
            res.status = STATUS.FINISHED;
            res.result = color_slave.R < black && color_slave.G < black && color_slave.B < black ? STATUS.NONE : IsWithinRange(color_slave.R, r_min, r_max) && IsWithinRange(color_slave.G, g_min, g_max) && IsWithinRange(color_slave.B, b_min, b_max) ? STATUS.OK : STATUS.NG;
        }

        private void AddNewResult(SQliteDataAccess.Rectangles r, Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, Color color_slave, int black)
        {
            results.Add(new Result()
            {
                id = r.id,
                R = color.R,
                G = color.G,
                B = color.B,
                MinR = r_min,
                MinG = g_min,
                MinB = b_min,
                MaxR = r_max,
                MaxG = g_max,
                MaxB = b_max,
                Slave_R = color_slave.R,
                Slave_G = color_slave.G,
                Slave_B = color_slave.B,
                status = STATUS.FINISHED,
                result = color_slave.R < black && color_slave.G < black && color_slave.B < black ? STATUS.NONE : IsWithinRange(color_slave.R, r_min, r_max) && IsWithinRange(color_slave.G, g_min, g_max) && IsWithinRange(color_slave.B, b_min, b_max) ? STATUS.OK : STATUS.NG
            });
        }

        public int CalcPct(int total, double pct) => (int)Math.Round(total * pct / 100);
        public bool IsWithinRange(int input, int min, int max) => input >= min && input <= max;        
    }

    public class Result
    {
        public int id { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int MinR { get; set; }
        public int MinG { get; set; }
        public int MinB { get; set; }
        public int MaxR { get; set; }
        public int MaxG { get; set; }
        public int MaxB { get; set; }
        public int Slave_R { get; set; }
        public int Slave_G { get; set; }
        public int Slave_B { get; set; }
        public STATUS status { get; set; }
        public STATUS result { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GL_M2.Utilities;
using OpenCvSharp.Extensions;

namespace GL_M2
{
    partial class Main
    {
        private Task taskProcessTest;

        private int step_display = 0;
        private void timerTest_Tick(object sender, EventArgs e)
        {
            if (lbStatus.BackColor == System.Drawing.Color.Yellow)
            {
                step_display = (step_display + 1) % 4;
                lbStatus.Text = "Wait".PadRight(step_display + 4, '.');
            }
            else
            {
                step_display = 0;
            }
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
                    Console.WriteLine("Task : " + task.Exception.InnerException.Message);
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
            if (results.Count != rectangles.Count)
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

        private void DrawToBitmaps(Bitmap bmp, Image img)
        {
            // Draw image master to bitmap
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(img, 0, 0);
            }
        }


        private void ProcessRectangles(Bitmap bmp_m, Bitmap bmp_s)
        {
            try
            {
                foreach (var r in rectangles)
                {
                    ProcessRectangle(r, bmp_m, bmp_s);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ProcessRectangles :" + ex.Message);
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
            if (_total == results.Count(x => x.result == STATUS.NONE))
            {
                test_result = SERIAL_STATUS.NONE;
                // Black color
                lbStatus.Text = "Wait..";
                lbStatus.BackColor = System.Drawing.Color.Yellow;
                //
            }
            else if (_total == results.Count(x => x.result == STATUS.OK))
            {
                test_result = SERIAL_STATUS.OK;
                lbStatus.Text = "PASS";
                lbStatus.BackColor = System.Drawing.Color.Green;
                // STOP
                reset = STATUS.STOPPED;
            }
            else
            {
                test_result = SERIAL_STATUS.NG;
                lbStatus.Text = "NG";
                lbStatus.BackColor = System.Drawing.Color.Red;
            }
        }

        private void ProcessRectangle(SQliteDataAccess.Rectangles r, Bitmap bmp_m, Bitmap bmp_s)
        {
            //// Get RGB value of pixel center of rectangle
            //int x = (int)r.x + (int)r.width / 2;
            //int y = (int)r.y + (int)r.height / 2;
            System.Drawing.Color color = AverageColor(r, bmp_m);//bmp_m.GetPixel(x, y);


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

            if (x_slave < bmp_s.Width && y_slave < bmp_s.Height)
            {
                System.Drawing.Color color_slave = AverageColor(r, bmp_s); // bmp_s.GetPixel(x_slave, y_slave);

                UpdateResultList(r, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave, bmp_m, bmp_s);
            }
        }

        private System.Drawing.Color AverageColor(SQliteDataAccess.Rectangles r, Bitmap bmp)
        {
            long totalR = 0, totalG = 0, totalB = 0;
            int count = 0;

            for (int i = (int)r.x; i < (int)r.x + (int)r.width; i++)
            {
                for (int j = (int)r.y; j < (int)r.y + (int)r.height; j++)
                {
                    System.Drawing.Color color = bmp.GetPixel(i, j);
                    totalR += color.R;
                    totalG += color.G;
                    totalB += color.B;
                    count++;
                }
            }

            int avgR = (int)(totalR / count);
            int avgG = (int)(totalG / count);
            int avgB = (int)(totalB / count);
            System.Drawing.Color averageColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
            return averageColor;
        }

        private void UpdateResultList(SQliteDataAccess.Rectangles r, System.Drawing.Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, System.Drawing.Color color_slave,Bitmap bitmapMaster, Bitmap bitmapSlove)
        {
            int black = 20;
            var res = results.FirstOrDefault(re => re.id == r.id);
            if (res != null)
            {
                // Update result
                UpdateResult(res, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave, black);

                if (res.result == STATUS.NG || res.result == STATUS.NONE)
                {
                    STATUS _result = STATUS.NG;
                    foreach (var img in images_data)
                    {
                        if (!File.Exists(Path.Combine(Properties.Resources.path_image, img.name)))
                            continue;
                        //using (FileStream fs = new FileStream(Path.Combine(Properties.Resources.path_image, img.name), FileMode.Open, FileAccess.Read))
                        //{
                        //    using(Image m = Image.FromStream(fs))
                        //    {
                        //        using(Bitmap bm = new Bitmap(m.Width, m.Height))
                        //        {
                        //            DrawToBitmaps(bm, m);

                        //            if (!Properties.Settings.Default.isColorDistortion)
                        //            {
                        //                System.Drawing.Color color_ms = AverageColor(r, bm);
                        //                _result = GetStatus(color_slave, color_ms);
                        //            }
                        //            else
                        //            {
                        //                _result = GetStatusColorDistortion(bitmapMaster,bitmapSlove);
                        //            }
                        //        }
                        //    }
                        //}


                        if (!Properties.Settings.Default.isColorDistortion)
                        {
                            System.Drawing.Color color_ms = AverageColor(r, bitmapMaster);
                            _result = GetStatus(color_slave, color_ms);
                        }
                        else
                        {
                            _result = GetStatus(bitmapMaster, bitmapSlove);
                        }

                        if (_result == STATUS.OK)
                        {                          
                            res.result = _result;
                            break;
                        }
                    }
                }
            }
            else
            {
                // Add result to list
                AddNewResult(r, color, r_min, r_max, g_min, g_max, b_min, b_max, color_slave, black);
            }
        }

        

        private void UpdateResult(Result res, System.Drawing.Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, System.Drawing.Color color_slave, int black)
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
            //res.result = color_slave.R < black && color_slave.G < black && color_slave.B < black ? STATUS.NONE : IsWithinRange(color_slave.R, r_min, r_max) && IsWithinRange(color_slave.G, g_min, g_max) && IsWithinRange(color_slave.B, b_min, b_max) ? STATUS.OK : STATUS.NG;
            //res.result = GetStatus(color_slave, color);

        }

        private void AddNewResult(SQliteDataAccess.Rectangles r, System.Drawing.Color color, int r_min, int r_max, int g_min, int g_max, int b_min, int b_max, System.Drawing.Color color_slave, int black)
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
                //result = color_slave.R < black && color_slave.G < black && color_slave.B < black ? STATUS.NONE : IsWithinRange(color_slave.R, r_min, r_max) && IsWithinRange(color_slave.G, g_min, g_max) && IsWithinRange(color_slave.B, b_min, b_max) ? STATUS.OK : STATUS.NG
                result = STATUS.NG // GetStatus(color_slave, color)
            });
        }

        private string[] color_name_s;
        private string[] color_name_m;

        private STATUS GetStatus(System.Drawing.Color color_slave, System.Drawing.Color color)
        {
            color_name_s = _colorName.Name(_colorName.RgbToHex(color_slave.R, color_slave.G, color_slave.B));

            if (color_name_s[3].ToLower() == "black")
            {
                return STATUS.NONE;
            }
            color_name_m = _colorName.Name(_colorName.RgbToHex(color.R, color.G, color.B));

            if (color_name_s[3].ToLower() == color_name_m[3].ToLower())
            {
                return STATUS.OK;
            }

            if (color_name_m[3].ToLower().Contains(color_name_s[3].ToLower()) || color_name_s[3].ToLower().Contains(color_name_m[3].ToLower()))
            {
                return STATUS.OK;
            }

            //Console.WriteLine("Master " + color_name_m[3] + " == " + color_name_s[3] + ", HEX " + color_name_s[0]);
            return STATUS.NG;

        }
        private STATUS GetStatus(Bitmap bitmapMaster, Bitmap bitmapSlove)
        {
            var distortion = ColorDistortion(bitmapMaster, bitmapSlove);

            if(distortion < Properties.Settings.Default.percent_check)
            {
                return STATUS.OK;
            }
            return STATUS.NG;
        }
        public int CalcPct(int total, double pct) => (int)Math.Round(total * pct / 100);
        public bool IsWithinRange(int input, int min, int max) => input >= min && input <= max;

        public double ColorDistortion(Bitmap imgBitmap1, Bitmap imgBitmap2)
        {
            using (OpenCvSharp.Mat img1 = OpenCvSharp.Extensions.BitmapConverter.ToMat(imgBitmap1))
            using (OpenCvSharp.Mat img2 = BitmapConverter.ToMat(imgBitmap2))
            using (OpenCvSharp.Mat diff = new OpenCvSharp.Mat())
            {
                OpenCvSharp.Cv2.Absdiff(img1, img2, diff); // คำนวณความแตกต่างระหว่างสองภาพ

                double distortion = OpenCvSharp.Cv2.Norm(diff, OpenCvSharp.NormTypes.L1); // L1 norm หรือแบบ absolute sum
                return distortion;
            }
        }
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

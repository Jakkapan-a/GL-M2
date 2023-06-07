using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace GL_M2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private Bitmap bitmap = null;

        private void Main_Load(object sender, EventArgs e)
        {
          

        }

        private void DrawLine(ref Bitmap bitmap, int startX = 0, int startY = 720 , int length = 1000, int degree = 50, string colorName = "Red")
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {

                startX = startX + 30;
                Color color = Color.FromName(colorName);
                // Line 1
                using (Pen pen = new Pen(color, 1))
                {
                    int length2 = length;
                    double angle = degree * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX = startX + (int)(length2 * Math.Cos(angle));
                    int endY = startY - (int)(length2 * Math.Sin(angle)); //

                    g.DrawLine(pen, new Point(startX, startY), new Point(endX, endY));
                }
                // Line 2
                using (Pen pen = new Pen(color, 1))
                {
                    int startX2 = bitmap.Width - startX;
                    int startY2 = startY;
                    double angle = (360 - degree) * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX = startX2 - (int)(length * Math.Cos(angle));
                    int endY = startY2 + (int)(length * Math.Sin(angle)); //

                    g.DrawLine(pen, new Point(startX2, startY2), new Point(endX, endY));
                }

                // Line 3
                using (Pen pen = new Pen(color, 1))
                {
                    int length1 = 200;
                    double angle = degree * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX = startX + (int)(length1 * Math.Cos(angle));
                    int endY = startY - (int)(length1 * Math.Sin(angle)); //

                    int startX2 = bitmap.Width - startX;
                    int startY2 = startY;
                    double angle2 = (360 - degree) * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX2 = startX2 - (int)(length1 * Math.Cos(angle2));
                    int endY2 = startY2 + (int)(length1 * Math.Sin(angle2)); //

                    g.DrawLine(pen, new Point(endX, endY), new Point(endX2, endY2));
                }

                // Line 4
                using (Pen pen = new Pen(Color.Green, 1))
                {
                    // 1
                    int length1 = 365;
                    double angle = degree * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX = startX + (int)(length1 * Math.Cos(angle));
                    int endY = startY - (int)(length1 * Math.Sin(angle)); //
                    // 2
                    int startX2 = bitmap.Width - startX;
                    int startY2 = startY;
                    double angle2 = (360 - degree) * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX2 = startX2 - (int)(length1 * Math.Cos(angle2));
                    int endY2 = startY2 + (int)(length1 * Math.Sin(angle2)); //

                    g.DrawLine(pen, new Point(endX, endY), new Point(endX2, endY2));
                }
            }
        }

        private void DrawLineR(ref Bitmap bitmap, int startX = 0, int startY = 720, int length = 1000, int degree = 50, string colorName = "Red")
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Color color = Color.FromName(colorName);
                using (Pen pen = new Pen(color, 1))
                {
                    double angle = (360- degree) * Math.PI / 180; // แปลงมุมจากองศาเป็นเรเดียน
                    int endX = startX - (int)(length * Math.Cos(angle));
                    int endY = startY + (int)(length * Math.Sin(angle)); //

                    g.DrawLine(pen, new Point(startX, startY), new Point(endX, endY));
                }
            }
        }
       

        private bool isTimer = false;
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isTimer = !isTimer;
            if (isTimer)
            {
                timer1.Start();
                toolStripStatusLabel1.Text = "Running...";
            }
            else
            {
                timer1.Stop();
                toolStripStatusLabel1.Text = "Stop...";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bitmap?.Dispose();
            int width = 1280;
            int hight = 720;
            int bottom = 10;
            int destances = 300;
            bitmap = new Bitmap(width, hight);
            int degree = 70;

            DrawLine(ref bitmap, destances, (hight - bottom), 500, degree: degree, colorName: "Red");
            //DrawLine(ref bitmap, 200, 500,500, degree: 90, colorName: "Red");
            //DrawLineR(ref bitmap, (width - destances), (hight - bottom), 200, degree: degree, colorName: "Red");

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bitmap;
        }

        #region Old
        private void DrawCircle(ref Bitmap bitmap, Color color)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, 1))
                {
                    // ระบุตำแหน่งและขนาดของวงกลม
                    // ในกรณีนี้, เราจะวาดวงกลมตรงกลางภาพ (คำนวณจากความกว้างและความสูงของ bitmap) และมีขนาดเส้นผ่าศูนย์กลาง 150 pixels
                    int x = (bitmap.Width - 150) / 2;
                    int y = (bitmap.Height - 150) / 2;
                    int diameter = 150;

                    // วาดวงกลม
                    g.DrawEllipse(pen, x, y, diameter, diameter);
                }
            }
        }
        private void DrawSafetyCross(ref Bitmap bitmap, int x, int y, int size, string colorHex = "#000000")
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // แปลงสตริง hex เป็น Color และสร้าง Pen
                //Color color = ColorTranslator.FromHtml(colorHex);
                Color color = Color.Green;
                using (Pen pen = new Pen(color, 1))
                {
                    // คำนวณตำแหน่งของแต่ละจุด
                    Point top = new Point(x + size / 2, y);
                    Point bottom = new Point(x + size / 2, y + size);
                    Point left = new Point(x, y + size / 2);
                    Point right = new Point(x + size, y + size / 2);

                    // วาดสองเส้นตรงที่แตกต่างทางที่ข้ามกัน
                    g.DrawLine(pen, top, bottom);
                    g.DrawLine(pen, left, right);
                }
            }
        }
        private void DrawSafetyCross(ref Bitmap bitmap, int x, int y, int size, string colorHex = "#000000", int lineWidth = 2, int gapSize = 20)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // แปลงสตริง hex เป็น Color และสร้าง Pen
                //Color color = ColorTranslator.FromHtml(colorHex);
                Color color = Color.Green;

                using (Pen pen = new Pen(color, lineWidth))
                {
                    // คำนวณตำแหน่งของแต่ละจุด
                    Point top1 = new Point(x + size / 2, y);
                    Point top2 = new Point(x + size / 2, y + size / 2 - gapSize / 2);
                    Point bottom1 = new Point(x + size / 2, y + size / 2 + gapSize / 2);
                    Point bottom2 = new Point(x + size / 2, y + size);
                    Point left1 = new Point(x, y + size / 2);
                    Point left2 = new Point(x + size / 2 - gapSize / 2, y + size / 2);
                    Point right1 = new Point(x + size / 2 + gapSize / 2, y + size / 2);
                    Point right2 = new Point(x + size, y + size / 2);

                    // วาดสี่เส้นตรงที่แตกต่างทางที่ข้ามกัน แต่ยังไม่ไปถึงจุดกึ่งกลางของ "+"
                    g.DrawLine(pen, top1, top2);
                    g.DrawLine(pen, bottom1, bottom2);
                    g.DrawLine(pen, left1, left2);
                    g.DrawLine(pen, right1, right2);
                }
            }
        }

#endregion
    }
}

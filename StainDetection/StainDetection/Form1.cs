using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;



namespace StainDetection
{
    public partial class Form1 : Form
    {
        Image myImage;
        Image<Gray, Byte> detImage;
        int rioX = 0;
        int rioY = 0;
        int trackbar1v = 0;
        int trackbar2v = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {

                myImage = Image.FromFile(file.FileName);
                //myImage = new Image<Bgr, byte>((Bitmap)image);
                pictureBox1.Image = myImage;
                Image<Gray, byte> oldImg = new Image<Gray, byte>((Bitmap)myImage);

               /* pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer org;
                org = new ImageViewer(oldImg, "Original image");
                org.Show();*/
            }
        }
        private const int Height = 480,Width = 640;
        private void button2_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>((Bitmap)myImage);
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            
            //11*11高斯平滑3
            
           // detImg._EqualizeHist();
           // detImg._GammaCorrect(1.3d);
            //detImg = detImg.SmoothGaussian(15);
           // detImg = detImg.Canny(1, 1);

            //detImg = detImg.SmoothGaussian(15);
            //detImg = detImg.SmoothGaussian(15);

            //canny1.2
            //detImg = detImg.Canny(trackbar1v, trackbar2v);
            
            //二直化
           // Emgu.CV.CvInvoke.cvThreshold(detImg, detImg, 125, 255, THRESH.CV_THRESH_BINARY);
           /* Bitmap score = new Bitmap(Width +1, Height+1);
            //CvInvoke.cvIntegral(detImg, score,  null,  null);
            int maxX = 0, maxY = 0;
            int max = 0;
            for (int y = 0; y < Height - 41; y++)
            {
                for (int x = 0; x < Width - 41; x++)
                {

                   // Color gray ;
                    int all = 0,a = 0,b = 0,c =0,d = 0;
                    if ((int)detImg.Bitmap.GetPixel(x + 40, y + 40).R > 0)
                        a = 1;
                    if ((int)detImg.Bitmap.GetPixel(x, y).R == 255)
                        b = 1;
                    if ((int)detImg.Bitmap.GetPixel(x + 40, y).R == 255)
                        c = 1;
                    if ((int)detImg.Bitmap.GetPixel(x, y + 40).R == 255)
                        d = 1;
                    all = a + b - c - d;
                    score.SetPixel(x, y, Color.FromArgb(Math.Abs(all), 0, 0));
                    //score.SetPixel(x, y, 1); //= cvmGet(sumMat, y + 40, x + 40) + cvmGet(sumMat, y, x) - cvmGet(sumMat, y, x + 40) - cvmGet(sumMat, y + 40, x);
                    if ((int)score.GetPixel(x,y).R < max)
                    {
                        max = (int)score.GetPixel(x, y).R;
                        maxX = x;
                        maxY = y;
                    }
                }
            }
            PointF point = new PointF(maxX,maxY);
            PointF point2 = new PointF(maxX+40, maxY+40);

             LineSegment2D line = new LineSegment2D(Point.Round(point), Point.Round(point2));
            detImg.Draw(line ,new Gray(255), 5);*/

            
            pictureBox2.Image = detImg.Bitmap;
            detImage = detImg;
            /*pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            det = new ImageViewer(detImg, "Detection results");
            det.Show();*/
        }
        Image<Gray, Byte> canimg = new Image<Gray, Byte>(Height, Width);
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackbar1v = trackBar1.Value;
            label1.Text = "" + trackBar1.Value;
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(Width, Height);
            oldImg.Bitmap = detImage.Bitmap;
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg.Bitmap = oldImg.Bitmap;
            //canny
            //Emgu.CV.CvInvoke.cvThreshold(detImg, detImg, sss, 255, THRESH.CV_THRESH_BINARY);
            detImg = detImg.Canny(trackbar1v, trackbar2v);
            pictureBox2.Image = detImg.Bitmap;
            canimg = detImg;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            trackbar2v = trackBar2.Value;
            label2.Text = "" + trackBar2.Value;
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(Width, Height);
            oldImg.Bitmap = detImage.Bitmap;
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg.Bitmap = oldImg.Bitmap;
            //canny
            //Emgu.CV.CvInvoke.cvThreshold(detImg, detImg, sss, 255, THRESH.CV_THRESH_BINARY);
            detImg = detImg.Canny(trackbar1v, trackbar2v);
            pictureBox2.Image = detImg.Bitmap;
            canimg = detImg;
        }
        Image<Gray, Byte> gauimg = new Image<Gray, Byte>(Height, Width);
        //gauimg = detImage;
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = "" + ((trackBar3.Value*2)+3);
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(Width, Height);
            oldImg = detImage;
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            //11*11高斯平滑
            detImg = detImg.SmoothGaussian(3 + trackBar3.Value*2);
            pictureBox2.Image = detImg.Bitmap;
            gauimg = detImg;
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label3.Text = "" + ((trackBar3.Value * 2) + 3);
            label9.Text = "" + trackBar4.Value;
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);
            
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            //11*11高斯平滑
            for (int i = 1; i <= trackBar4.Value;i++ )
                detImg = detImg.SmoothGaussian(3 + trackBar3.Value * 2);
            pictureBox2.Image = detImg.Bitmap;
            gauimg = detImg;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = gauimg.Bitmap;
            Initialization();
            detImage = gauimg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = canimg.Bitmap;
            Initialization();
            detImage = canimg;
        }
        void Initialization()
        {
            trackBar3.Value = 0;
            trackBar4.Value = 1;
            trackbar1v = 0;
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackbar2v = 0;
            trackBar6.Value = 0;
            trackBar5.Value = 0;
            label1.Text = "0";
            label6.Text = "0";
            label3.Text = "0";
            label9.Text = "1";
            label4.Text = "0";
            label5.Text = "0";
        }
        Image<Gray, Byte> gamimg = new Image<Gray, Byte>(Height, Width);
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);
            
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            label14.Text = "" + (0.01 * trackBar5.Value);
            Bitmap GammagrayImage = new Bitmap(Width, Height);
            Bitmap oldImage = (Bitmap)this.pictureBox1.Image;
            Color pixel;
            for (int x = 0; x < Width; x++)
            {
               for (int y = 0; y < Height; y++)
                {
                    pixel = oldImage.GetPixel(x, y);
                    double c = 100, gray = 0, r, g, b, logray, R = 0.01;
                    c = trackBar6.Value;
                    R = 0.01 * trackBar5.Value;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    gray = (r + g + b) / 3;
                    logray = c * (Math.Pow(gray, R));//c*Math.Log(1 + gray);
                    if (logray > 255)
                        logray = 255;
                    if (logray < 255)
                        logray = 0;
                    GammagrayImage.SetPixel(x, y, Color.FromArgb((int)logray, (int)logray, (int)logray));
                   
                }
            }
            this.pictureBox2.Image = GammagrayImage;
            gamimg.Bitmap = GammagrayImage;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = gamimg.Bitmap;
            Initialization();
            detImage = gamimg;
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);

            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            label15.Text = "" + trackBar6.Value;
            Bitmap GammagrayImage = new Bitmap(Width, Height);
           
            Color pixel;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    pixel = oldImg.Bitmap.GetPixel(x, y);
                    double c = 100, gray = 0, r, g, b, logray, R = 0.01;
                    c = trackBar6.Value;
                    R = 0.01 * trackBar5.Value;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    gray = (r + g + b) / 3;
                    logray = c * (Math.Pow(gray, R));//c*Math.Log(1 + gray);
                    if (logray > 255)
                        logray = 255;
                    if (logray < 255)
                        logray = 0;
                    GammagrayImage.SetPixel(x, y, Color.FromArgb((int)logray, (int)logray, (int)logray));
                    
                }
            }
            this.pictureBox2.Image = GammagrayImage;
            gamimg.Bitmap = GammagrayImage;
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            rioX = trackBar7.Value;
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            PointF point = new PointF(rioX, rioY);
            PointF point2 = new PointF(rioX, rioY + 15);
            PointF point3 = new PointF(rioX + 15, rioY);
            PointF point4 = new PointF(rioX + 15, rioY + 15);
            LineSegment2D line = new LineSegment2D(Point.Round(point), Point.Round(point2));
            LineSegment2D line2 = new LineSegment2D(Point.Round(point), Point.Round(point3));
            LineSegment2D line3 = new LineSegment2D(Point.Round(point2), Point.Round(point4));
            LineSegment2D line4 = new LineSegment2D(Point.Round(point3), Point.Round(point4));
            detImg.Draw(line, new Gray(255), 1);
            detImg.Draw(line2, new Gray(255), 1);
            detImg.Draw(line3, new Gray(255), 1);
            detImg.Draw(line4, new Gray(255), 1);
            textBox1.Text = " ";
            for (int y = rioY+1; y < rioY + 15; y++)
            {
                textBox1.Text += y + ".   ";
                for (int x = rioX+1; x < rioX + 15; x++)
                {

                    textBox1.Text += "   " + detImg.Bitmap.GetPixel(x, y).R;
                   // textBox1.Text += "" + Emgu.CV.CvInvoke.
                    //textBox1.Text += "" + Emgu.CV.CvInvoke.cvGetReal2D(detImg, x, y);
                }
                textBox1.Text +="\r\n";

            }
            label16.Text = "" + trackBar7.Value;
                    pictureBox2.Image = detImg.Bitmap;
           
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            rioY = trackBar8.Value;
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            PointF point = new PointF(rioX, rioY);
            PointF point2 = new PointF(rioX, rioY + 15);
            PointF point3 = new PointF(rioX + 15, rioY);
            PointF point4 = new PointF(rioX + 15, rioY + 15);
            LineSegment2D line = new LineSegment2D(Point.Round(point), Point.Round(point2));
            LineSegment2D line2 = new LineSegment2D(Point.Round(point), Point.Round(point3));
            LineSegment2D line3 = new LineSegment2D(Point.Round(point2), Point.Round(point4));
            LineSegment2D line4 = new LineSegment2D(Point.Round(point3), Point.Round(point4));
            detImg.Draw(line, new Gray(255), 1);
            detImg.Draw(line2, new Gray(255), 1);
            detImg.Draw(line3, new Gray(255), 1);
            detImg.Draw(line4, new Gray(255), 1);
            textBox1.Text = " ";
            for (int y = rioY +1; y < rioY + 15; y++)
            {
                textBox1.Text += y + ".   ";
                for (int x = rioX+1; x < rioX + 15; x++)
                {
                    textBox1.Text += "   " + detImg.Bitmap.GetPixel(x, y).R;
                    textBox1.Text += "" + Emgu.CV.CvInvoke.cvGetReal2D(detImg, x, y);
                }
                textBox1.Text += "\r\n";
            }
            label17.Text = "" + trackBar8.Value;
            pictureBox2.Image = detImg.Bitmap;
           
        }

        private void trackBar7_DragDrop(object sender, DragEventArgs e)
        {
           
        }
        int sss = 0;
        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            Image<Gray, Byte> oldImg = new Image<Gray, Byte>(detImage.Bitmap);
            Image<Gray, Byte> detImg = new Image<Gray, Byte>(Width, Height);
            detImg = oldImg;
            label18.Text = "" + trackBar9.Value;
            sss = trackBar9.Value;
            Emgu.CV.CvInvoke.cvThreshold(detImg, detImg, trackBar9.Value, 255, THRESH.CV_THRESH_BINARY);
            pictureBox2.Image = detImg.Bitmap;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
        

        
    }
}

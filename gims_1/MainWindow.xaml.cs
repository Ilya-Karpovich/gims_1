using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace gims_1
{
    public partial class MainWindow : Window
    {
        string path;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory=Directory.GetCurrentDirectory();
            ofd.ShowDialog();
            path= ofd.FileName;
            imageBox.Source = new BitmapImage(new Uri($"{path}"));
        }

        private void refactorImgBtn_Click(object sender, RoutedEventArgs e)
        {
            Bitmap btm = new Bitmap(path);
            Bitmap newBtm = new Bitmap(btm.Width, btm.Height);
            PixelFilter filter = new PixelFilter();
            filter.SetBitmap(btm);
            filter.Initialize();

            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    filter.SetPixelWindow(i, j);
                    newBtm.SetPixel(i, j, filter.Filter());
                }
            }
            string curFile = Directory.GetCurrentDirectory()+"//image";
            btm.Dispose();
            newBtm.Save(curFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            newBtm.Dispose();


        }
    }

    public class Pixel
    {
        public int red;
        public int green;
        public int blue;
        public Pixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public Pixel()
        {
        }
    }
    public class PixelFilter
    {
        Pixel [,] px = new Pixel[3, 3];
        Bitmap bmp;

        public void SetBitmap(Bitmap bmp)
        {
            this.bmp = bmp;
        }
        public void Initialize()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    px[i, j]= new Pixel();

                }
            }
        }
        public void SetPixelWindow(int x, int y)
        {
            this.Clear();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (x + i < bmp.Width && y + j < bmp.Height)
                    {
                        px[i, j].red = bmp.GetPixel(x + i, y + j).R;
                        px[i, j].green = bmp.GetPixel(x + i, y + j).G;
                        px[i, j].blue = bmp.GetPixel(x + i, y + j).B;
                    }
                }
            }
        }

        public void Clear()
        {
            for (int i = 0;i < 3;i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    px[i, j].red = 0;
                    px[i, j].green = 0;
                    px[i, j].blue = 0;
                }
            }
        }

        public System.Drawing.Color Filter()
        {
            int red=0;
            int green=0;
            int blue=0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0;j < 3; j++)
                {
                    if (i != 1 && j != 1)
                    {
                        red += px[i, j].red;
                        green += px[i, j].green;
                        blue += px[i, j].blue;
                    }
                }
            }

            red /=8 ; green /= 8; blue /= 8;
            int brightnes=red + green + blue;
            int xbrightnes = px[1, 1].red + px[1, 1].green + px[1, 1].blue;

            if (xbrightnes > brightnes)
            {
                return System.Drawing.Color.FromArgb(red, green, blue);
            }
            else
            {
                return System.Drawing.Color.FromArgb((byte)px[1, 1].red, (byte)px[1, 1].green, (byte)px[1, 1].blue);
            }

        }
    }
}

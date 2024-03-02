using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace gims_1
{
    public partial class MainWindow : Window
    {
        string path="";
        List<NamedBitmapImage> images = new List<NamedBitmapImage>();
        private int imageListPosition = 0;
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
            images.Add(new NamedBitmapImage(new BitmapImage(new Uri($"{path}")), "Исходная фотография"));
            imageBox.Source = images[0].Image;
            imageName.Content = images[0].Name;

        }

        private void refactorImgBtn_Click(object sender, RoutedEventArgs e)
        {
            if (path == "")
            {
                return;
            }

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
            
            btm.Dispose();
            images.Add(new NamedBitmapImage(NamedBitmapImage.BitmapToBitmapImage(newBtm), "Фото обработанное от шумов"));
            newBtm.Dispose();


        }

        private void kontrastImgBtn_Click(object sender, RoutedEventArgs e)
        {
            if (path == "")
            {
                return;
            }
            LinearKontur linearKontur = new LinearKontur();
            KontrastGistogram kontrastGistogram = new KontrastGistogram();
            kontrastGistogram.SetBitmap(path);
            kontrastGistogram.MakeGistogram();
            linearKontur.SetKontrast(kontrastGistogram.GetKontrast());
            linearKontur.SetBitmap(new Bitmap(path));
            List<NamedBitmap> photo=linearKontur.GetKontures();
            for (int i=0;i<photo.Count; i++)
            {
                images.Add(new NamedBitmapImage(NamedBitmapImage.BitmapToBitmapImage(photo[i].bitmap), photo[i].name));
            }

        }

        private void gistogramBtn_Click(object sender, RoutedEventArgs e)
        {
            KontrastGistogram kontrastGistogram = new KontrastGistogram();
            kontrastGistogram.SetBitmap(path);
            kontrastGistogram.MakeGistogram();
            Gistogram gistogram = new Gistogram(kontrastGistogram.brightnes, kontrastGistogram.GetKontrast());
            gistogram.Show();
        }

        private void prevImage_Click(object sender, RoutedEventArgs e)
        {
            if (imageListPosition - 1  >= 0)
            {
                
                imageListPosition--;
                imageBox.Source = images[imageListPosition].Image;
                imageName.Content = images[imageListPosition].Name;
            }
        }

        private void nextImage_Click(object sender, RoutedEventArgs e)
        {
            if (imageListPosition + 1 <= images.Count - 1)
            {
                imageListPosition++;
                imageBox.Source = images[imageListPosition].Image;
                imageName.Content = images[imageListPosition].Name;
            }
        }
    }
}

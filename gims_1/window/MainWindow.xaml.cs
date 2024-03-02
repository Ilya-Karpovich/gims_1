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
        List <string> methodName = new List<string>() {"Курсовая маска (Север)","Метод двух разностей",
            "Оператор Кирша","Курсовая маска (Северо-Запад)","Оператор Собела (1)","Курсовая маска (Северо-Восток)",
            "Оператор Лапласа (Н13)","Курсовая маска (Юг)","Оператор Робертса (2)","Курсовая маска (Юго-Запад)",
            "Метод разностей по столбцам","Курсовая маска (Юго-Восток)","Метод разностей по строкам",
            "Оператор Лапласа (Н14)","Курсовая маска (Восток)","Оператор Лапласа (Н15)","Оператор Собела (2)",
            "Курсовая маска (Запад)","Оператор Уоллиса","Оператор Робертса (1)","Оператор Превитта","Оператор Шарра",
            "Оператор Робинсона"};
        public MainWindow()
        {
            InitializeComponent();
            for (int i=0;i< methodName.Count; i++)
            {
                methodList.Items.Add(methodName[i]);
            }
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
            string curFile = Directory.GetCurrentDirectory()+"//image";
            btm.Dispose();
            newBtm.Save(curFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            newBtm.Dispose();


        }

        private void kontrastImgBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gistogramBtn_Click(object sender, RoutedEventArgs e)
        {
            Gistogram gistogram = new Gistogram();
            gistogram.Show();
        }
    }
}

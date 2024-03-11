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
        List<string> methodName = new List<string>() {"Курсовая маска (Север)","Метод двух разностей",
            "Оператор Кирша","Курсовая маска (Северо-Запад)","Оператор Собела (1)","Курсовая маска (Северо-Восток)",
            "Оператор Лапласа (Н13)","Курсовая маска (Юг)","Оператор Робертса (2)","Курсовая маска (Юго-Запад)",
            "Метод разностей по столбцам","Курсовая маска (Юго-Восток)","Метод разностей по строкам",
            "Оператор Лапласа (Н14)","Курсовая маска (Восток)","Оператор Лапласа (Н15)","Оператор Собела (2)",
            "Курсовая маска (Запад)","Оператор Уоллиса","Оператор Робертса (1)","Оператор Превитта","Оператор Шарра",
            "Оператор Робинсона"};


        string path ="";
        int kontrast = 0;
        int maxLight = 0, minLight = 0;
        List<NamedBitmapImage> images = new List<NamedBitmapImage>();
        private int imageListPosition = 0;
        public MainWindow()
        {
            InitializeComponent();
            for (int i=0;i< methodName.Count; i++)
            {
                methNameLB.Items.Add(methodName[i]);
            }
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
            KontrastGistogram kontrastGistogram = new KontrastGistogram();
            kontrastGistogram.SetBitmap(path);
            kontrastGistogram.MakeGistogram();
            this.kontrast=kontrastGistogram.GetKontrast();
            this.maxLight = kontrastGistogram.Max();
            this.minLight = kontrastGistogram.Min();
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
            if (porog.Text == "")
            {
                linearKontur.SetKontrast(kontrast);
                linearKontur.SetLight(maxLight, minLight);
            }
            else
            {
                string porKontr=porog.Text;
                try
                {
                    int kont = int.Parse(porKontr);
                    linearKontur.SetKontrast(kont);
                    linearKontur.SetLight(maxLight, minLight);
                }
                catch
                {
                    MessageBox.Show("Некорректное значение порoга!!!","Error!!!");
                    return;
                }
            }
            linearKontur.SetBitmap(new Bitmap(path));
            switch (methNameLB.SelectedIndex)
            {
                //"Курсовая маска (Север)"
                case 0:
                    images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                    break;
                //"Метод двух разностей"
                //case 1:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Кирша"
                //case 2:  
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Курсовая маска (Северо-Запад)"
                case 3:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.northWest).bitmap), linearKontur.northWest.Name));
                    break;
                //"Оператор Собела (1)"
                //case 4:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Курсовая маска (Северо-Восток)"
                case 5:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.northEast).bitmap), linearKontur.northEast.Name));
                    break;
                //"Оператор Лапласа (Н13)"
                case 6:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeLaplasKonturing(linearKontur.laplasH13).bitmap), linearKontur.laplasH13.Name));
                    break;
                //"Курсовая маска (Юг)"
                case 7:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.south).bitmap), linearKontur.south.Name));
                    break;
                //"Оператор Робертса (2)",
                //case 8:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Курсовая маска (Юго-Запад)",
                case 9:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.southWest).bitmap), linearKontur.southWest.Name));
                    break;
                //"Метод разностей по столбцам",
                //case 10:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Курсовая маска (Юго-Восток)",
                case 11:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.southEast).bitmap), linearKontur.southEast.Name));
                    break;
                //"Метод разностей по строкам",
                //case 12:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Лапласа (Н14)",
                case 13:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeLaplasKonturing(linearKontur.laplasH14).bitmap), linearKontur.laplasH14.Name));
                    break;
                //"Курсовая маска (Восток)",
                case 14:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.east).bitmap), linearKontur.east.Name));
                    break;
                //"Оператор Лапласа (Н15)",
                case 15:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeLaplasKonturing(linearKontur.laplasH15).bitmap), linearKontur.laplasH15.Name));
                    break;
                //"Оператор Собела (2)",
                //case 16:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Курсовая маска (Запад)",
                case 17:
                                        images.Add(new NamedBitmapImage
                        (NamedBitmapImage.BitmapToBitmapImage
                        (linearKontur.MakeKonturing(linearKontur.west).bitmap), linearKontur.west.Name));
                    break;
                //"Оператор Уоллиса",
                //case 18:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Робертса (1)",
                //case 19:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Превитта",
                //case 20:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Шарра",
                //case 21:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                //"Оператор Робинсона"
                //case 22:
                //                        images.Add(new NamedBitmapImage
                //        (NamedBitmapImage.BitmapToBitmapImage
                //        (linearKontur.MakeKonturing(linearKontur.north).bitmap), linearKontur.north.Name));
                //    break;
                default:
                    MessageBox.Show("Метод еще не реализован!!!", "Error!!!");
                    break;

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

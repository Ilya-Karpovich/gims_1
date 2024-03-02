using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gims_1
{
    /// <summary>
    /// Логика взаимодействия для Gistogram.xaml
    /// </summary>
    public partial class Gistogram : Window
    {
        List<int> cord;
        public Gistogram(List <int> gistCord)
        {
            this.cord= gistCord;
            InitializeComponent();
        }

        private void ReduseDimension()
        {
            int dimMax = cord.Max();
            for (int i=0; i<cord.Count; i++)
            {
                cord[i] /= (dimMax/350+5);
            }
        }
        private void showBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ReduseDimension();
            Polyline polyline = new Polyline();
            polyline.Points = new PointCollection();
            for (int i= 0; i < cord.Count; i++)
            {
                polyline.Points.Add(new Point((i+20), (350-cord[i])));
            }
            polyline.Stroke = Brushes.Black;
            gisCanvas.Children.Add(polyline);
        }
    }
}

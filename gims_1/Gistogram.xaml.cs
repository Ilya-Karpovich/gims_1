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
        private List<int> cord;
        private double kontr;
        public Gistogram(List <int> gistCord, double kontr)
        {
            this.cord= gistCord;
            this.kontr= kontr;
            InitializeComponent();
            this.ReduseDimension();
            Polyline polyline = new Polyline();
            polyline.Points = new PointCollection();
            for (int i = 0; i < cord.Count; i++)
            {
                polyline.Points.Add(new Point((i + 20), (350 - cord[i])));
            }
            polyline.Stroke = Brushes.Black;
            gisCanvas.Children.Add(polyline);
            kontrLabel.Content = $"Контрасность: {this.kontr}";
        }

        private void ReduseDimension()
        {
            int dimMax = cord.Max();
            for (int i=0; i<cord.Count; i++)
            {
                cord[i] /= (dimMax/350+5);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NonLinearKontur : Kontur
{
    private int kontrast;
    private double MaxLight=1, MinLight=0;


    public double Normalize(int num)
    {
        double temp = (num - this.MinLight) * (255 / this.MaxLight - this.MinLight);
        if (temp > 0)
            return temp;
        else
            return 0;
    }
}

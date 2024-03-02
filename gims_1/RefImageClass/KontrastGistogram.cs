using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KontrastGistogram
{
    private Bitmap bmp;
    private List<int> brightnes = new List<int> (256);
    public void SetBitmap(string path)
    {
        this.bmp = new Bitmap(path);
    }
    public void MakeGistogram()
    {
        for (int i=0; i<this.bmp.Width; i++)
        {
            for (int j=0;j<this.bmp.Height; j++)
            {
                this.brightnes[(this.bmp.GetPixel(i,j).R+this.bmp.GetPixel(i,j).G+ this.bmp.GetPixel(i, j).B)/3]++;
            }
        }
        bmp.Dispose();
    }
    public double GetKontrast()
    {
        double kontrast = 0;

        return kontrast;
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NonLinearKontur : Kontur
{
    private int kontrast;
    private double MaxLight=1, MinLight=0;


    public Pixel[,] px = new Pixel[3, 3];
    public Bitmap bmp;
    int[,] mask = new int[3, 3] { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
    int porog = 49;

    public Kontur()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                px[i, j] = new Pixel();

            }
        }
    }

    public void SetBitmap(Bitmap btm)
    {
        this.bmp = btm;
    }

    public void Clear()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                px[i, j].red = 0;
                px[i, j].green = 0;
                px[i, j].blue = 0;
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
    public System.Drawing.Color SetColor(int px)
    {
        if (px < this.porog)
        {
            return System.Drawing.Color.White;
        }
        else
        {
            return System.Drawing.Color.Black;
        }
    }
    public int CalkulateKontur(int x, int y)
    {
        this.SetPixelWindow(x, y);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                this.px[i, j].CalculateGrey();
            }
        }
        int X = (px[0, 2].grey + px[2, 1].grey + px[2, 2].grey) - (px[0, 0].grey + px[1, 0].grey + px[2, 0].grey);
        int Y = (px[0, 0].grey + px[0, 1].grey + px[0, 2].grey) - (px[2, 0].grey + px[2, 1].grey + px[2, 2].grey);
        int Rez = (int)Math.Sqrt(X * X + Y * Y);
        return Rez;
    }
    public double Normalize(int num)
    {
        double temp = (num - this.MinLight) * (255 / this.MaxLight - this.MinLight);
        if (temp > 0)
            return temp;
        else
            return 0;
    }
}

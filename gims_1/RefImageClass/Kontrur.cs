using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Kontur
{
    public Pixel[,] px = new Pixel[3, 3];
    public Bitmap bmp;

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

}


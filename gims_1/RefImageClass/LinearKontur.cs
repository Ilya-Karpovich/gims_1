using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinearKontur : Kontur
{
    private double kontrast;
    //Курсовые маски
    public int[,] north=new int[3, 3] { {1, 1, 1}, { 1, -2, 1 }, { -1, -1, -1 } };
    public int[,] northEast = new int[3, 3] { { 1, 1, 1 }, { -1, -2, 1 }, { -1, -1, 1 } };
    public int[,] east = new int[3, 3] { { -1, 1, 1 }, { -1, -2, 1 }, { -1, 1, 1 } };
    public int[,] southEast = new int[3, 3] { { -1, -1, 1 }, { -1, -2, 1 }, { 1, 1, 1 } };
    public int[,] south = new int[3, 3] { { -1, -1, -1 }, { 1, -2, 1 }, { 1, 1, 1 } };
    public int[,] southWest = new int[3, 3] { { 1, -1, -1 }, { 1, -2, -1 }, { 1, 1, 1 } };
    public int[,] west = new int[3, 3] { { 1, 1, -1 }, { 1, -2, -1 }, { 1, 1, -1 } };
    public int[,] northWest = new int[3, 3] { { 1, 1, 1 }, { 1, -2, -1 }, { 1, -1, -1 } };

    //Операторы Лапласа
    public int[,] laplasH13 = new int[3, 3] { {0,-1,0}, {-1,4,-1}, {0,-1,0} };
    public int[,] laplasH14 = new int[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
    public int[,] laplasH15 = new int[3, 3] { { 1, -2, 1 }, { -2, 4, -2 }, { 1, -2, 1 } };

    public void SetKontrast(double kontr)
    {
        this.kontrast = kontr;
    }

    public System.Drawing.Color KursMaskKonturing()
    {
        int red = 0;
        int green = 0;
        int blue = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i != 1 && j != 1)
                {
                    red += px[i, j].red;
                    green += px[i, j].green;
                    blue += px[i, j].blue;
                }
            }
        }

        red /= 8; green /= 8; blue /= 8;
        int brightnes = red + green + blue;
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

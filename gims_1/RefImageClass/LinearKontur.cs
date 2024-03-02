using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinearKontur : Kontur
{
    private int kontrast;

    //Курсовые маски
    public Mask north=new Mask("Курсовая маска (Север)", new int[3, 3] { {1, 1, 1}, { 1, -2, 1 }, { -1, -1, -1 } });
    public Mask northEast = new Mask("Курсовая маска (Северо-Запад)",new int[3, 3] { { 1, 1, 1 }, { -1, -2, 1 }, { -1, -1, 1 } });
    public Mask east = new Mask("Курсовая маска (Восток)",new int[3, 3] { { -1, 1, 1 }, { -1, -2, 1 }, { -1, 1, 1 } });
    public Mask southEast = new Mask("Курсовая маска (Юго-Восток)", new int[3, 3] { { -1, -1, 1 }, { -1, -2, 1 }, { 1, 1, 1 } });
    public Mask south =new Mask ("Курсовая маска (Юг)",new int[3, 3] { { -1, -1, -1 }, { 1, -2, 1 }, { 1, 1, 1 } });
    public Mask southWest = new Mask("Курсовая маска (Юго-Запад)",new int[3, 3] { { 1, -1, -1 }, { 1, -2, -1 }, { 1, 1, 1 } });
    public Mask west = new Mask("Курсовая маска (Запад)",new int[3, 3] { { 1, 1, -1 }, { 1, -2, -1 }, { 1, 1, -1 } });
    public Mask northWest =new Mask("Курсовая маска (Северо-Запад)", new int[3, 3] { { 1, 1, 1 }, { 1, -2, -1 }, { 1, -1, -1 } });

    //Операторы Лапласа
    public Mask laplasH13 = new Mask ("Оператор Лапласа (Н13)", new int[3, 3] { {0,-1,0}, {-1,4,-1}, {0,-1,0} });
    public Mask laplasH14 = new Mask("Оператор Лапласа (Н14)", new int[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } });
    public Mask laplasH15 = new Mask("Оператор Лапласа (Н15)", new int[3, 3] { { 1, -2, 1 }, { -2, 4, -2 }, { 1, -2, 1 } });

    public void SetKontrast(int kontr)
    {
        this.kontrast = kontr;
    }
    public System.Drawing.Color SetColor(int px, int kontr)
    {
        if (px > kontr)
        {
            return System.Drawing.Color.Black;
        }
        else
        {
            return System.Drawing.Color.White;
        }
    }

    int CalkulateKontur(int x, int y, Mask mask)
    {
        this.SetPixelWindow(x, y);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                this.px[i, j].CalculateGrey();
            }
        }
        int sumOfGrey=0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0;j < 3; j++)
            {
                px[i, j].grey *= mask.KonturingMask[i, j];
                if (i != 1 && j != 1)
                {
                    sumOfGrey += px[i, j].grey;
                }
            }
        }
        px[1, 1].grey -= sumOfGrey;
        return px[1, 1].grey;
    }

    public Bitmap MakeKonturing( Mask mask)
    {
        Bitmap bitmap=new Bitmap(bmp.Width, bmp.Height);
        for (int i = 0;i < bmp.Width;i++)
        {
            for (int j=0;j < bmp.Height; j++)
            {
                bitmap.SetPixel(i, j, SetColor(CalkulateKontur(i, j, mask), this.kontrast));
            }
        }
        return bitmap;
    }
}

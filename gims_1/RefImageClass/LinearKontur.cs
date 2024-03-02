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
    private Mask north=new Mask("Курсовая маска (Север)", new int[3, 3] { {1, 1, 1}, { 1, -2, 1 }, { -1, -1, -1 } });
    private Mask northEast = new Mask("Курсовая маска (Северо-Запад)",new int[3, 3] { { 1, 1, 1 }, { -1, -2, 1 }, { -1, -1, 1 } });
    private Mask east = new Mask("Курсовая маска (Восток)",new int[3, 3] { { -1, 1, 1 }, { -1, -2, 1 }, { -1, 1, 1 } });
    private Mask southEast = new Mask("Курсовая маска (Юго-Восток)", new int[3, 3] { { -1, -1, 1 }, { -1, -2, 1 }, { 1, 1, 1 } });
    private Mask south =new Mask ("Курсовая маска (Юг)",new int[3, 3] { { -1, -1, -1 }, { 1, -2, 1 }, { 1, 1, 1 } });
    private Mask southWest = new Mask("Курсовая маска (Юго-Запад)",new int[3, 3] { { 1, -1, -1 }, { 1, -2, -1 }, { 1, 1, 1 } });
    private Mask west = new Mask("Курсовая маска (Запад)",new int[3, 3] { { 1, 1, -1 }, { 1, -2, -1 }, { 1, 1, -1 } });
    private Mask northWest =new Mask("Курсовая маска (Северо-Запад)", new int[3, 3] { { 1, 1, 1 }, { 1, -2, -1 }, { 1, -1, -1 } });

    //Операторы Лапласа
    private Mask laplasH13 = new Mask ("Оператор Лапласа (Н13)", new int[3, 3] { {0,-1,0}, {-1,4,-1}, {0,-1,0} });
    private Mask laplasH14 = new Mask("Оператор Лапласа (Н14)", new int[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } });
    private Mask laplasH15 = new Mask("Оператор Лапласа (Н15)", new int[3, 3] { { 1, -2, 1 }, { -2, 4, -2 }, { 1, -2, 1 } });

    public void SetKontrast(int kontr)
    {
        this.kontrast = kontr;
    }
    private System.Drawing.Color SetColor(int px, int kontr)
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
        this.Clear();
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

    private NamedBitmap MakeKonturing(Mask mask)
    {
        Bitmap bitmap=new Bitmap(bmp.Width, bmp.Height);
        for (int i = 0;i < bmp.Width;i++)
        {
            for (int j=0;j < bmp.Height; j++)
            {
                bitmap.SetPixel(i, j, SetColor(CalkulateKontur(i, j, mask), this.kontrast));
            }
        }
        NamedBitmap nBmp = new NamedBitmap(bitmap, mask.Name);
        return nBmp;
    }

    public List<NamedBitmap> GetKontures()
    {
        List<NamedBitmap > list = new List<NamedBitmap>();

        list.Add(MakeKonturing(north));
        list.Add(MakeKonturing(northEast));
        list.Add(MakeKonturing(east));
        list.Add(MakeKonturing(southEast));
        list.Add(MakeKonturing(south));
        list.Add(MakeKonturing(west));
        list.Add(MakeKonturing(southWest));
        list.Add(MakeKonturing(northWest));
        list.Add(MakeKonturing(laplasH13));
        list.Add(MakeKonturing(laplasH14));
        list.Add(MakeKonturing(laplasH15));

        return list;
    }

}

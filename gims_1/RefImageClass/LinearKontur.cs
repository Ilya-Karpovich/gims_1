using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinearKontur : Kontur
{
    private int kontrast;
    private int MaxLight, MinLight;

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

    public void SetLight(int max, int min)
    {
        this.MaxLight = max;
        this.MinLight = min;
    }
    private System.Drawing.Color SetColor(int px, int kontr)
    {
        if (px < kontr )
        {
            return System.Drawing.Color.White;
        }
        else
        {
            return System.Drawing.Color.Black;
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
        int sumOfMinusGrey=0, sumOfPlusGrey=0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0;j < 3; j++)
            {
                px[i, j].grey *= mask.KonturingMask[i, j];
                if (mask.KonturingMask[i, j] < 0)
                {
                    sumOfMinusGrey += px[i, j].grey;
                }
                else
                {
                    sumOfPlusGrey += px[i, j].grey;
                }
            }
        }
        return (sumOfMinusGrey+sumOfPlusGrey);
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

    int CalkulateLaplasKontur(int x, int y, Mask mask)
    {
        this.SetPixelWindow(x, y);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                this.px[i, j].CalculateGrey();
            }
        }
        int sumOfMinusGrey = 0, sumOfPlusGrey = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                px[i, j].grey *= mask.KonturingMask[i, j];
                if (mask.KonturingMask[i, j] < 0)
                {
                    sumOfMinusGrey += px[i, j].grey;
                }
                else
                {
                    sumOfPlusGrey += px[i, j].grey;
                }
            }
        }
        int normalize = this.Normalize((sumOfMinusGrey + sumOfPlusGrey), this.MaxLight, this.MinLight);
        return normalize;
    }

    private NamedBitmap MakeLaplasKonturing(Mask mask)
    {
        Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height);
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                bitmap.SetPixel(i, j, SetColor(CalkulateLaplasKontur(i, j, mask), this.kontrast));
            }
        }
        NamedBitmap nBmp = new NamedBitmap(bitmap, mask.Name);
        return nBmp;
    }


    public List<NamedBitmap> GetKontures()
    {
        List<NamedBitmap > list = new List<NamedBitmap>();

        //list.Add(MakeKonturing(north));
        //list.Add(MakeKonturing(northEast));
        //list.Add(MakeKonturing(east));
        //list.Add(MakeKonturing(southEast));
        //list.Add(MakeKonturing(south));
        //list.Add(MakeKonturing(west));
        //list.Add(MakeKonturing(southWest));
        //list.Add(MakeKonturing(northWest));
        list.Add(MakeLaplasKonturing(laplasH13));
        list.Add(MakeLaplasKonturing(laplasH14));
        list.Add(MakeLaplasKonturing(laplasH15));

        return list;
    }

    public int Normalize(int num, int max, int min)
    {
        int temp = (num - min) * (255 / max - min);
        if (temp > 0)
            return temp;
        else
            return 0;
    }
}

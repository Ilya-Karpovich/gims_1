using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KontrastGistogram
{
    private Bitmap bmp;
    public List<int> brightnes = new List<int>(256);
    public void SetList()
    {
        for (int i = 0; i < brightnes.Capacity; i++)
        {
            brightnes.Add(0);
        }
    }
    public void SetBitmap(string path)
    {
        this.bmp = new Bitmap(path);
        this.SetList();
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
    public int GetKontrast()
    {

        int gistStart=0, gistEnd=0;
        for (int i =0; i < this.brightnes.Count; i++)
        {
            if (brightnes[i] != 0 && gistStart == 0)
            {
                gistStart = i;
                gistEnd = gistEnd + ((this.brightnes.Count - i) / 5);
                break;
            }
        }
        int kontrast = 0;   
        for (int i=gistStart; i < gistEnd; i++)
        {
            if ((this.brightnes[i] >= kontrast))
            {
                kontrast= i;
            }
        }
        return kontrast;
    }

    public int Max()
    {
        int max = 0;
        for (int i = 0; i < this.brightnes.Count; i++)
        {
            if (this.brightnes[i] != 0)
            {
                max = i;
            }
        }
        return max;
    }
    public int Min()
    {
        int min = 0;
        for (int i = (this.brightnes.Count-1); i > -1 ; i--)
        {
            if (this.brightnes[i] != 0)
            {
                min = i;
            }
        }
        return min;
    }

}

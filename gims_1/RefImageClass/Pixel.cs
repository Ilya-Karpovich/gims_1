using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pixel
{
    public int red;
    public int green;
    public int blue;
    public int grey;
    public Pixel(byte red, byte green, byte blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
    public Pixel()
    {
    }
    public void CalculateGrey()
    {
        this.grey=(this.red+this.green+this.blue)/3;
    }
}

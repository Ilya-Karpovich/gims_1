using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NamedBitmap
{
    Bitmap bitmap;
    string name;
    public NamedBitmap(Bitmap bitmap, string name)
    {
        this.bitmap = bitmap;
        this.name = name;
    }
}

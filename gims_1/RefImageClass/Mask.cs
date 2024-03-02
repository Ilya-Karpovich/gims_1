using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Mask
{
    public string Name;
    public int[,] KonturingMask;
    public Mask(string name, int[,] konturingMask)
    {
        Name = name;
        KonturingMask = konturingMask;
    }
}

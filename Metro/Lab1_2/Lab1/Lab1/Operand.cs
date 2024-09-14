using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class Operand
{
    public int I { get; set; }
    public string Name { get; set; }
    public int F2i { get; set; }
    public Operand(int I, string Name, int F2i)
    {
        this.I = I;
        this.Name = Name;
        this.F2i = F2i;
    }
}

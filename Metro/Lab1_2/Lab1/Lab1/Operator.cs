using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class Operator
{
    public int J { get; set; }
    public string Name { get; set; }
    public int F1j { get; set; }
    public Operator (int J, string Name, int F1j)
    {
        this.J = J;
        this.Name = Name;
        this.F1j = F1j;
    }
}

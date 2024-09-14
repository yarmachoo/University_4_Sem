using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public static class ConvertString
{
    public static IEnumerable<string> Convert(IEnumerable<int> j, IEnumerable<string> op,IEnumerable<int> fj)
    {
        List<string> res = new List<string>();

        for (int i = 0; i<op.Count(); i++) 
        {
            res.Add(String.Concat(j.ElementAt(i).ToString(), " ", op.ElementAt(i), "\t", fj.ElementAt(i).ToString()));
        }
        return res;
    }
}

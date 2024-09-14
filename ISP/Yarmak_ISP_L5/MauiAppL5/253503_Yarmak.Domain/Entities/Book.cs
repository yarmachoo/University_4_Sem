using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Domain.Entities;
public class Book: Entity
{ 
    public string Name { get; set; }
    public string Description { get; set; }
    public double Rate { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}

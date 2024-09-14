using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Domain.Entities;
public class Author: Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Book> Books {  get; set; }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Commands;

public class UpdateBookCommand:IAddOrUpdateBookRequest
{
    public Book Book { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands;

public class AddAuthorCommand:IAddAuthorRequest
{
    public Author Author { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Queries
{
    public record GetBookByIdQuery(int Id) : IRequest<Book> { }    
}

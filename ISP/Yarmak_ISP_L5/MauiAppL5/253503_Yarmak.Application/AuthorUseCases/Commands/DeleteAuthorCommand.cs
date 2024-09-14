using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands;
public record DeleteAuthorCommand(Author Author):IRequest
{

}

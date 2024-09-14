using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands
{
    public interface IAddAuthorRequest: IRequest
    {
        Author Author { get; set; }
    }
}

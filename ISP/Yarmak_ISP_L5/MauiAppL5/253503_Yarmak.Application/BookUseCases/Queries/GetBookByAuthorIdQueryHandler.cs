using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Queries;
public class GetBookByAuthorIdQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetBookByAuthorIdQuery, IEnumerable<Book>>
{
    public async Task<IEnumerable<Book>> Handle(GetBookByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork
            .BookRepository
            .ListAsync(x => x.AuthorId.Equals(request.Id), cancellationToken);
    }
}

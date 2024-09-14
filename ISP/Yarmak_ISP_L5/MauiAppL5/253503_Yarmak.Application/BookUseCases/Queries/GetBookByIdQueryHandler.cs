using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Queries
{
    public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetBookByIdQuery, Book>
    {
        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork
                .BookRepository
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}

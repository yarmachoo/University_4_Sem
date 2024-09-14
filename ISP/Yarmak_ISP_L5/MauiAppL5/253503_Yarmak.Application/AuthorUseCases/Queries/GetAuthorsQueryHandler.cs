using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Queries
{
    public class GetAuthorsQueryHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
    {
        public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.AuthorRepository.ListAllAsync(cancellationToken);
        }
    }
}

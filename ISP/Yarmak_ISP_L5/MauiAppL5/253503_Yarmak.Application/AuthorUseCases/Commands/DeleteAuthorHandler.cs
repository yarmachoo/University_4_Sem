using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands
{
    public class DeleteAuthorHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<DeleteAuthorCommand>
    {
        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.AuthorRepository.DeleteAsync(request.Author, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Commands
{
    public class DeleteBookHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<DeleteBookCommand>
    {
        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BookRepository.DeleteAsync(request.Book, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}

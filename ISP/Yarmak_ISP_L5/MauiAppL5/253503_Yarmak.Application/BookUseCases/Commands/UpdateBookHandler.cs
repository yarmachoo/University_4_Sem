using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Commands;
public class UpdateBookHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateBookCommand>
{
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BookRepository.UpdateAsync(request.Book, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}

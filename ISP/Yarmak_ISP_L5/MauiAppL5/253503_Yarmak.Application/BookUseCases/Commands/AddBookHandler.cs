using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.BookUseCases.Commands;

public class AddBookHandler(IUnitOfWork unitOfWork) : 
    
    IRequestHandler<AddBookCommand>
{
    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BookRepository.AddAsync(request.Book, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}



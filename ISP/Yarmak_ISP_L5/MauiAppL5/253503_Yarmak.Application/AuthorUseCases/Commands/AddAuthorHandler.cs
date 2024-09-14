using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands;

public class AddAuthorHandler(IUnitOfWork unitOfWork) : 
    IRequestHandler<AddAuthorCommand>
{
    public async Task Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AuthorRepository.AddAsync(request.Author, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
